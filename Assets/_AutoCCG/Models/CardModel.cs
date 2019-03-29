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

        public int cost;

        public bool ranged;

        public List<CardSkillModel> cardSkills;

        public int GetDamage(bool isFrontLiner)
        {
            int damage = 0;

            if(isFrontLiner || ranged)
            {
                damage = attack;
            }

            return damage;
        }
    }
}