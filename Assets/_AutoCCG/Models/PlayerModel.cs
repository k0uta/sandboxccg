using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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
            }

            GetComponentInChildren<HandModel>().handView = playerView.handView;
            GetComponentInChildren<BattlegroundsModel>().battlegroundsView = playerView.battlegroundsView;
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
            if(!playerView)
            {
                return;
            }
            playerView.goldText.text = string.Format("Gold\n{0}", currentGold);
            playerView.healthText.text = string.Format("Health\n{0}", currentHealth);
            playerView.handSizeText.text = string.Format("Hand\n{0}/{1}", handModel.cards.Count, handModel.cardLimit);
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
        public void RpcCheckAndRemoveDeadCard(int battlegroundCardId)
        {
            var battlegrounds = this.GetComponentInChildren<BattlegroundsModel>();
            var battlegroundsCards = battlegrounds.battlegroundsCards;
            var handModel = this.GetComponentInChildren<HandModel>();
            var handController = this.GetComponentInChildren<HandController>();

            var battlegroundsCard = battlegroundsCards[battlegroundCardId];
            if (battlegroundsCard.IsDead())
            {
                battlegrounds.RemoveBattlegroundsCard(battlegroundsCard);

                // Not cool
                handController.RemoveCardFromBattlegroundsQueue(handController.battlegroundsQueueCards[battlegroundCardId]);

                handModel.RemoveCard(battlegroundsCard.cardModel);
            }
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
        public void RpcApplyFrontCardDanage(int damage)
        {
            var battlegroundsModel = GetComponentInChildren<BattlegroundsModel>();
            var frontCard = battlegroundsModel.battlegroundsCards[0];

            frontCard.ApplyDamage(damage);

            battlegroundsModel.battlegroundsView.UpdateCardsView();
        }

        [ClientRpc]
        public void RpcClearBattlegrounds()
        {
            battlegroundsModel.ClearBattlegrounds();
            handController.EnableBattlegroundsQueueCards();
        }
    }
}