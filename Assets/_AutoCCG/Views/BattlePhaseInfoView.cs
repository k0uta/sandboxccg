using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace AutoCCG
{
    public class BattlePhaseInfoView : MonoBehaviour
    {
        public TextMeshProUGUI phaseText;

        CanvasGroup canvasGroup;

        public static BattlePhaseInfoView instance;

        public static BattlePhaseInfoView GetInstance()
        {
            return instance;
        }

        private void OnEnable()
        {
            instance = this;
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }

        public IEnumerator AnimatePhaseText(Phase phase)
        {
            phaseText.text = phase.ToString().ToSentenceCase();

            this.transform.localScale = Vector3.zero;
            this.canvasGroup.alpha = 1;

            var sequence = DOTween.Sequence();

            var scaleTween = this.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
            sequence.Append(scaleTween);

            var fadeTween = canvasGroup.DOFade(0, 0.5f);
            sequence.Append(fadeTween);

            yield return sequence.Play().WaitForCompletion();
        }
    }
}
