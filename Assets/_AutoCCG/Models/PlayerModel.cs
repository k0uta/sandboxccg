﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace AutoCCG
{
    public class PlayerModel : NetworkBehaviour
    {
        [SyncVar]
        public int currentGold;

        [SyncVar]
        public int currentHealth;

        public PlayerView playerView;

        public HandModel handModel;

        public ShopController shopController;

        public HandController handController;

        public BattlegroundsModel battlegroundsModel;

        [SyncVar]
        public string playerName;

        public DeckModel catsDeck;

        public DeckModel dogsDeck;

        void Start()
        {
            StartCoroutine(StartPlayerCoroutine());
        }

        IEnumerator StartPlayerCoroutine()
        {
            yield return new WaitUntil(() => GameObject.FindObjectOfType<BoardView>() != null);
            var boardView = GameObject.FindObjectOfType<BoardView>();
            if (isLocalPlayer)
            {
                playerView = boardView.playerView;

                var shopView = GameObject.FindObjectOfType<ShopView>();
                shopController.shopView = shopView;
                shopView.restockCostText.text = string.Format("{0}g", shopController.restockPrice);

                this.gameObject.tag = "Player";
            }
            else
            {
                playerView = boardView.enemyView;
                this.gameObject.tag = "Enemy";
            }

            GetComponentInChildren<HandModel>().handView = playerView.handView;
            battlegroundsModel.battlegroundsView = playerView.battlegroundsView;
        }

        public override void OnStartServer()
        {
            //var boardController = GameObject.FindObjectOfType<BoardController>();
            //boardController.AddPlayer(this.gameObject);
            StartCoroutine(StartServerCoroutine());
        }

        IEnumerator StartServerCoroutine()
        {
            yield return new WaitUntil(() => GameObject.FindObjectOfType<BoardController>() != null);
            var boardController = GameObject.FindObjectOfType<BoardController>();
            boardController.AddPlayer(this.gameObject);
        }

        public void Update()
        {
            if (!playerView)
            {
                return;
            }
            playerView.goldText.text = string.Format("Gold\n{0}", currentGold);
            playerView.healthText.text = string.Format("Health\n{0}", currentHealth);
            playerView.handSizeText.text = string.Format("Hand\n{0}/{1}", handModel.cards.Count, handModel.cardLimit);

            playerView.playerNameText.text = string.Format("{0} Hand", playerName);
        }

        public bool Pay(int cost)
        {
            if (cost > currentGold)
            {
                return false;
            }

            currentGold -= cost;

            return true;
        }

        public void GiveGold(int amount)
        {
            currentGold += amount;
        }

        public void ApplyDamage(int damageAmount)
        {
            currentHealth -= damageAmount;

            // Should this rule be here?
            currentGold += damageAmount;
        }

        [ClientRpc]
        public void RpcSetCurrentTurn(int currentTurn)
        {
            shopController.CurrentTurn = currentTurn;
        }

        [Command]
        public void CmdPayForRestock()
        {
            if (!Pay(shopController.restockPrice))
            {
                return;
            }

            int seed = Random.Range(int.MinValue, int.MaxValue);
            RpcRestockShop(seed);
        }

        [Command]
        public void CmdRestock()
        {
            int seed = Random.Range(int.MinValue, int.MaxValue);
            RpcRestockShop(seed);
        }

        [ClientRpc]
        public void RpcRestockShop(int seed)
        {
            shopController.Restock(seed);
        }

        [Command]
        public void CmdBuyCardFromId(int cardId)
        {
            var card = shopController.cards[cardId];
            if (handModel.IsFull() || !Pay(card.cost))
            {
                return;
            }

            RpcBuyShopCard(cardId);
        }

        [ClientRpc]
        public void RpcBuyShopCard(int cardId)
        {
            var card = shopController.cards[cardId];

            card.owner = this;
            handModel.AddCard(card);

            shopController.RemoveCardById(cardId);
        }

        [Command]
        public void CmdAddCardToBattlegroundsQueue(int handCardId)
        {
            RpcAddCardToBattlegroundsQueue(handCardId);
        }

        [ClientRpc]
        void RpcAddCardToBattlegroundsQueue(int handCardId)
        {
            var handCard = playerView.handView.cards[handCardId];
            handController.AddCardToBattlegroundsQueue(handCard);
        }

        [Command]
        public void CmdRemoveCardToBattlegroundsQueue(int handCardId)
        {
            RpcRemoveCardToBattlegroundsQueue(handCardId);
        }

        [ClientRpc]
        void RpcRemoveCardToBattlegroundsQueue(int handCardId)
        {
            var handCard = playerView.handView.cards[handCardId];
            handController.RemoveCardFromBattlegroundsQueue(handCard);
        }

        [ClientRpc]
        public void RpcGiveGold(int amount)
        {
            GiveGold(amount);
        }

        [ClientRpc]
        public void RpcSendCardsToBattlegrounds()
        {
            handController.SendQueueToBattlegrounds();
        }

        [ClientRpc]
        public void RpcClearBattlegrounds()
        {
            battlegroundsModel.ClearBattlegrounds();
            handController.EnableBattlegroundsQueueCards();
        }

        [ClientRpc]
        public void RpcPerformSkillsForPhase(Phase phase)
        {
            StartCoroutine(PerformSkillsForPhase(phase));
        }

        IEnumerator PerformSkillsForPhase(Phase phase)
        {
            var players = GameObject.FindObjectsOfType<PlayerModel>();

            foreach (var player in players)
            {
                player.CreateBattlegroundsPhaseActions(phase);
            }

            foreach (var player in players)
            {
                yield return player.CheckAndRemoveDeadCards();
            }

            yield return ActionStackModel.GetInstance().PerformPhaseActionQueue(phase);

            battlegroundsModel.battlegroundsView.UpdateCardsView();
            battlegroundsModel.enemyBattlegrounds.battlegroundsView.UpdateCardsView();

            if (ShouldPerformCardCleanup(players))
            {
                StartCoroutine(PerformSkillsForPhase(Phase.CardCleanup));
            }
            else
            {
                if (isServer)
                {
                    GameObject.FindObjectOfType<BoardController>().GetComponent<PlayMakerFSM>().Fsm.Event("FINISHED");
                }
            }
        }

        bool ShouldPerformCardCleanup(PlayerModel[] players)
        {
            foreach (var player in players)
            {
                if (player.battlegroundsModel.HasDeadCards())
                {
                    return true;
                }
            }

            return false;
        }

        public void CreateBattlegroundsPhaseActions(Phase phase)
        {
            var battlegroundsCards = battlegroundsModel.battlegroundsCards;
            foreach (var card in battlegroundsCards)
            {
                card.PerformSkillsForPhase(phase);
                // TODO: Move to some place mana regen?
                if (phase == Phase.CombatEnd)
                {
                    card.currentMana++;
                    card.currentMana = Mathf.Min(card.currentMana, card.cardModel.mana);
                    card.CleanupTokenSkills();
                    card.battlegroundsCardView.UpdateView();
                }
            }
        }

        public IEnumerator CheckAndRemoveDeadCards()
        {
            var battlegroundsCards = battlegroundsModel.battlegroundsCards;

            for (int i = battlegroundsCards.Count - 1; i >= 0; i--)
            {
                var card = battlegroundsCards[i];
                if (card.IsDead())
                {
                    yield return battlegroundsModel.RemoveBattlegroundsCard(card);

                    // Not cool
                    handController.RemoveCardFromBattlegroundsQueue(handController.battlegroundsQueueCards[i]);

                    handModel.RemoveCard(card.cardModel);
                }
            }
        }

        [ClientRpc]
        public void RpcSyncBattlegroundsAndDeck(DeckNames deckName)
        {
            switch (deckName)
            {
                case DeckNames.Cats:
                    shopController.SetDeck(catsDeck);
                    break;
                case DeckNames.Dogs:
                    shopController.SetDeck(dogsDeck);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(deckName), deckName, null);
            }
            if (isLocalPlayer)
            {
                return;
            }

            var localPlayerBattlegrounds = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BattlegroundsModel>();
            localPlayerBattlegrounds.enemyBattlegrounds = battlegroundsModel;
            battlegroundsModel.enemyBattlegrounds = localPlayerBattlegrounds;
        }
    }
}