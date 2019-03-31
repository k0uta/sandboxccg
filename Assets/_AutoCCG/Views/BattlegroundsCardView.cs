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
            cardView.SetCardLife(battlegroundsCardModel.cardModel.life - battlegroundsCardModel.damageReceived);
            cardView.UpdateCardSkills();
        }
    }
}