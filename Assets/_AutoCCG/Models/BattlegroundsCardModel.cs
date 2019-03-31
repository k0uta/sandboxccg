using System.Collections.Generic;
using System.Linq;

namespace AutoCCG
{
    public class BattlegroundsCardModel
    {
        public CardModel cardModel;

        public int damageReceived;

        public BattlegroundsModel playerBattlegrounds;

        public BattlegroundsCardModel(CardModel cardModel)
        {
            this.cardModel = cardModel;
            damageReceived = 0;
        }

        public void ApplyDamage(int damage)
        {
            damageReceived += damage;
        }

        public void HealDamage(int damage)
        {
            damageReceived -= damage;
        }

        public bool IsDead()
        {
            return damageReceived >= cardModel.life;
        }

        public List<BattlegroundsCardModel> GetTargets(Target target)
        {
            var targets = new List<BattlegroundsCardModel>();
            if(target == Target.Self)
            {
                targets.Add(this);
            }

            return targets;
        }

        public void PerformSkillsForPhase(Phase phase)
        {
            var phaseSkills = cardModel.cardSkills.FindAll((cardSkill) => cardSkill.phase == phase);

            // TODO: Should trigger all? Maybe add priority?
            foreach (var skill in phaseSkills)
            {
                if(skill.CanBePerformed(this))
                {
                    skill.PerformSkill(this);
                }
            }
        }
    }
}