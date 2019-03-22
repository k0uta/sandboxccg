using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    [System.Serializable]
    public struct CardEntry
    {
        public CardModel card;
        public int quantity;
    }

    [CreateAssetMenuAttribute(fileName = "Deck", menuName = "AutoCCG/Deck")]
    public class DeckModel : ScriptableObject
    {
        public List<CardEntry> entries;
    }

}