using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Deck", menuName = "CCG/Deck")]
public class DeckModel : ScriptableObject
{
    public List<CardModel> cards = new List<CardModel>();
}
