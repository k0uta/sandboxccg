using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    [CreateAssetMenuAttribute(fileName = "Card", menuName = "AutoCCG/Card")]
    public class CardModel : ScriptableObject
    {
        public string title;

        public Sprite sprite;

        public int attack;

        public int life;

        public int mana;

        public int cost;

        public PlayerModel owner;
        
        public List<CardSkillModel> cardSkills;
    }
}