using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoCCG
{
    public class BattlegroundsCardView : MonoBehaviour
    {
        public CardView cardView;

        public BattlegroundsCardModel battlegroundsCardModel;

        public TextMeshProUGUI amountText;

        private void OnEnable()
        {
            amountText.alpha = 0f;
        }

        public void SetBattlegroundsCard(BattlegroundsCardModel battlegroundsCardModel)
        {
            this.battlegroundsCardModel = battlegroundsCardModel;
            battlegroundsCardModel.battlegroundsCardView = this;
            cardView.SetCard(battlegroundsCardModel.cardModel);
            UpdateView();
        }

        public void UpdateView()
        {
            cardView.UpdateView();
            cardView.SetCardLife(battlegroundsCardModel.cardModel.life - battlegroundsCardModel.damageReceived);
            cardView.SetCardMana(battlegroundsCardModel.currentMana);
        }

        public Sequence GetValueIncrementSequence(int amount, float duration)
        {
            var color = amount < 0 ? Color.red : Color.green;
            return GetValueIncrementSequence(amount, duration, color);
        }

        public Sequence GetValueIncrementSequence(int amount, float duration, Color color)
        {
            var damageSequence = DOTween.Sequence();
            
            if (amount == 0)
            {
                return damageSequence;
            }

            damageSequence.Append(transform.DOPunchPosition(new Vector3(4f, 0f), duration));

            damageSequence.Insert(0, cardView.GetComponent<Image>().DOColor(color, damageSequence.Duration() / 2f).SetLoops(2, LoopType.Yoyo));

            amountText.color = color;
            amountText.alpha = 1f;
            amountText.text = amount.ToString();

            amountText.transform.localPosition = Vector3.zero;

            var amountSequence = DOTween.Sequence();

            amountSequence.Append(amountText.transform.DOLocalMoveY(10f, duration));

            amountSequence.Insert(0, amountText.DOFade(0f, duration));

            damageSequence.Insert(0, amountSequence);

            return damageSequence;
        }
    }
}