namespace AutoCCG
{
    public class BattlegroundsCardModel
    {
        public CardModel cardModel;

        public int damageReceived;

        public BattlegroundsCardModel(CardModel cardModel)
        {
            this.cardModel = cardModel;
            damageReceived = 0;
        }

        public void ApplyDamage(int damage)
        {
            damageReceived += damage;
        }

        public bool IsDead()
        {
            return damageReceived >= cardModel.life;
        }
    }
}