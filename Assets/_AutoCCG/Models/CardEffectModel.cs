using UnityEngine;

namespace AutoCCG
{
    public enum CardEffectTrigger
    {
        TurnStart,
        CardDeath,
        BattleOver,
        CombatStart
    }
    [CreateAssetMenuAttribute(fileName = "Effect", menuName = "AutoCCG/Effect")]
    public class CardEffectModel : ScriptableObject
    {
        public Sprite sprite;

        public string description;

        public CardEffectTrigger trigger;

        
    }
}