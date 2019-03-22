using UnityEngine;

namespace AutoCCG
{
    public class BattlegroundsCardView : MonoBehaviour
    {
        public CardView cardView;

        public BattlegroundsCardModel battlegroundsCardModel;

        public void SetBattlegroundsCard(BattlegroundsCardModel battlegroundsCardModel)
        {
            this.battlegroundsCardModel = battlegroundsCardModel;
            cardView.SetCard(battlegroundsCardModel.cardModel);
        }

        public void UpdateView()
        {
            cardView.cardLife.text = string.Format("Life\n{0}", battlegroundsCardModel.cardModel.life - battlegroundsCardModel.damageReceived);
        }
    }
}