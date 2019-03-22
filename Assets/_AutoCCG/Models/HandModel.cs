using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class HandModel : MonoBehaviour
    {
        public List<CardModel> cards;

        public HandView handView;

        public int cardLimit;

        public BattlegroundsController battlegroundController;

        public void AddCard(CardModel card)
        {
            var newCard = Instantiate(card);
            cards.Add(newCard);
            handView.SpawnCard(newCard);
        }

        public bool IsFull()
        {
            return cards.Count >= cardLimit;
        }

        public void RemoveCard(CardModel card)
        {
            cards.Remove(card);
            handView.RemoveCard(card);
        }
    }

}