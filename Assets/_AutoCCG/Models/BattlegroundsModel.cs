using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class BattlegroundsModel : MonoBehaviour
    {
        public BattlegroundsView battlegroundsView;

        public List<BattlegroundsCardModel> battlegroundsCards;

        void Start()
        {
            battlegroundsView = transform.parent.GetComponent<PlayerModel>().playerView.battlegroundsView;
        }

        public void CreateBattlegroundsCards(List<CardModel> cards)
        {
            battlegroundsCards = new List<BattlegroundsCardModel>();
            foreach (var cardModel in cards)
            {
                var battlegroundsCard = new BattlegroundsCardModel(cardModel);
                battlegroundsCards.Add(battlegroundsCard);
                battlegroundsView.SpawnBattlegroundsCard(battlegroundsCard);
            }
        }

        public void RemoveBattlegroundsCard(BattlegroundsCardModel battlegroundsCard)
        {
            battlegroundsCards.Remove(battlegroundsCard);
            battlegroundsView.RemoveBattlegroundsCard(battlegroundsCard);
        }

        public void ClearBattlegrounds()
        {
            for (int i = battlegroundsCards.Count - 1; i >= 0; i--)
            {
                RemoveBattlegroundsCard(battlegroundsCards[i]);
            }
        }
    }
}