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
            battlegroundsCardModel.battlegroundsCardView = this;
            cardView.SetCard(battlegroundsCardModel.cardModel);
            UpdateView();
        }

        public void UpdateView()
        {
            cardView.SetCardLife(battlegroundsCardModel.cardModel.life - battlegroundsCardModel.damageReceived);
            cardView.SetCardMana(battlegroundsCardModel.currentMana);
            cardView.UpdateCardSkills();
        }
    }
}