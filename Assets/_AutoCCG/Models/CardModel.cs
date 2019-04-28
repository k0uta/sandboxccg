using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    [CreateAssetMenuAttribute(fileName = "Card", menuName = "AutoCCG/Card")]
    public class CardModel : ScriptableObject
    {
        public string title;

        public Sprite sprite;

        [SerializeField]
        private int attack;

        [SerializeField]
        private int life;

        public int mana;

        public int cost;

        [HideInInspector] public PlayerModel owner;

        [HideInInspector]
        public delegate void CardEvent();

        [HideInInspector] public event CardEvent UpdateEvent;

        public List<CardSkillModel> cardSkills;

        public int Attack
        {
            get => attack;
            set
            {
                attack = value;
                Update();
            }
        }

        public int Life
        {
            get => life;
            set
            {
                life = value;
                Update();
            }
        }

        private void Update()
        {
            UpdateEvent?.Invoke();
        }
    }
}