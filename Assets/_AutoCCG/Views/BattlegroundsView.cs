using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class BattlegroundsView : MonoBehaviour
    {
        public List<BattlegroundsCardView> battlegroundsCards;

        public GameObject cardPrefab;

        public Transform cardArea;

        public bool reverseOrientation;

        public void SpawnBattlegroundsCard(BattlegroundsCardModel battlegroundsCardModel)
        {
            var battlegroundsCard = GameObject.Instantiate(cardPrefab, cardArea);

            if (reverseOrientation)
            {
                battlegroundsCard.transform.SetAsFirstSibling();
            }

            var battlegroundsCardView = battlegroundsCard.GetComponent<BattlegroundsCardView>();

            battlegroundsCardView.SetBattlegroundsCard(battlegroundsCardModel);

            this.battlegroundsCards.Add(battlegroundsCardView);
        }

        public void UpdateCardsView()
        {
            foreach (var battlegroundsCard in battlegroundsCards)
            {
                battlegroundsCard.UpdateView();
            }
        }

        public void RemoveBattlegroundsCard(BattlegroundsCardModel battlegroundsCardModel)
        {
            var battlegroundsCardView = battlegroundsCards.Find(battlegroundsCard => battlegroundsCard.battlegroundsCardModel == battlegroundsCardModel);
            battlegroundsCards.Remove(battlegroundsCardView);
            Destroy(battlegroundsCardView.gameObject);
        }
    }
}