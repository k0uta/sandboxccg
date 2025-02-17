﻿using DG.Tweening;
using System.Collections;
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

        private List<BattlegroundsCardView> removedCards = new List<BattlegroundsCardView>();

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

        public IEnumerator RemoveBattlegroundsCard(BattlegroundsCardModel battlegroundsCardModel)
        {
            var battlegroundsCardView = battlegroundsCards.Find(battlegroundsCard => battlegroundsCard.battlegroundsCardModel == battlegroundsCardModel);
            battlegroundsCards.Remove(battlegroundsCardView);
            removedCards.Add(battlegroundsCardView);

            var sequence = DOTween.Sequence();
            sequence.Append(battlegroundsCardView.transform.DOLocalMoveY(30f, 1f));
            sequence.Insert(0, battlegroundsCardView.GetComponent<CanvasGroup>().DOFade(0f, sequence.Duration()));

            yield return sequence.Play().WaitForCompletion();

            battlegroundsCardView.gameObject.SetActive(false);
        }

        public void ClearRemovedCards()
        {
            foreach (var card in removedCards)
            {
                Destroy(card.gameObject);
            }
            removedCards.Clear();
        }

        public void Clear()
        {
            ClearRemovedCards();
            foreach (var card in battlegroundsCards)
            {
                Destroy(card.gameObject);
            }
            battlegroundsCards.Clear();
        }
    }
}