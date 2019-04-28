using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoCCG
{
    public class BattlegroundsModel : MonoBehaviour
    {
        public BattlegroundsView battlegroundsView;

        public List<BattlegroundsCardModel> battlegroundsCards;
        
        public List<BattlegroundsCardModel> deadCards;

        public BattlegroundsModel enemyBattlegrounds;

        void Start()
        {
            //battlegroundsView = transform.parent.GetComponent<PlayerModel>().playerView.battlegroundsView;
        }

        public void CreateBattlegroundsCards(List<CardModel> cards)
        {
            battlegroundsCards = new List<BattlegroundsCardModel>();
            deadCards = new List<BattlegroundsCardModel>();
            foreach (var cardModel in cards)
            {
                var battlegroundsCard = new BattlegroundsCardModel(cardModel);
                battlegroundsCards.Add(battlegroundsCard);
                battlegroundsCard.playerBattlegrounds = this;
                battlegroundsView.SpawnBattlegroundsCard(battlegroundsCard);
            }
        }

        public IEnumerator RemoveBattlegroundsCard(BattlegroundsCardModel battlegroundsCard)
        {
            battlegroundsCards.Remove(battlegroundsCard);
            deadCards.Add(battlegroundsCard);
            yield return battlegroundsView.RemoveBattlegroundsCard(battlegroundsCard);
        }

        public void ClearBattlegrounds()
        {
            battlegroundsCards.Clear();
            deadCards.Clear();
            battlegroundsView.Clear();
        }

        public List<BattlegroundsCardModel> GetArea(Area area)
        {
            switch (area)
            {
                case Area.Frontline:
                    return battlegroundsCards.Count > 0 ? battlegroundsCards.GetRange(0, 1) : null;
                case Area.Backline:
                    return battlegroundsCards.Count > 1 ? battlegroundsCards.GetRange(1, battlegroundsCards.Count - 1) : null;
                case Area.Battlegrounds:
                    return battlegroundsCards;
                case Area.LastCard:
                    return battlegroundsCards.Count > 0 ? battlegroundsCards.GetRange(battlegroundsCards.Count - 1, 1) : null;
                default:
                    throw new KeyNotFoundException();
            }
        }

        public List<BattlegroundsCardModel> GetArea(Area area, TargetPlayer targetPlayer)
        {
            switch (targetPlayer)
            {
                case TargetPlayer.Player:
                    return GetArea(area);
                case TargetPlayer.Enemy:
                    return enemyBattlegrounds.GetArea(area);
                default:
                    throw new KeyNotFoundException();
            }
        }

        public bool HasDeadCards()
        {
            foreach (var card in battlegroundsCards)
            {
                if (card.IsDead())
                {
                    return true;
                }
            }

            return false;
        }
    }
}