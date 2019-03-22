using UnityEngine;

namespace AutoCCG
{
    public class PlayerModel : MonoBehaviour
    {
        public int gold;

        public int currentHealth;

        public int baseHealth;

        public HandModel handModel;

        void Start()
        {
            currentHealth = baseHealth;
        }

        public bool Pay(int cost)
        {
            if (cost > gold)
            {
                return false;
            }

            gold -= cost;

            return true;
        }

        public void GiveGold(int amount)
        {
            gold += amount;
        }

        public void ApplyDamage(int damageAmount)
        {
            currentHealth -= damageAmount;

            // Should this rule be here?
            gold += damageAmount;
        }
    }
}