using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class HandModel : MonoBehaviour
    {
        public List<CardModel> cards;

        public HandView handView;

        public int cardLimit;

        void Start()
        {
            handView = transform.parent.GetComponent<PlayerModel>().playerView.handView;
        }

        public void AddCard(CardModel card)
        {
            cards.Add(card);
            handView.SpawnCard(card);
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