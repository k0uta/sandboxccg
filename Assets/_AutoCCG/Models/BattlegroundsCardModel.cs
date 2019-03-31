using System;
using System.Collections.Generic;

namespace AutoCCG
{
    public class BattlegroundsCardModel
    {
        public CardModel cardModel;

        public int damageReceived;

        public int currentMana;

        public BattlegroundsModel playerBattlegrounds;

        public List<Func<int, int>> damageFormulas = new List<Func<int, int>>();

        public BattlegroundsCardModel(CardModel cardModel)
        {
            this.cardModel = cardModel;
            damageReceived = 0;
            currentMana = 0;
        }

        public void ApplyDamage(int damage)
        {
            int totalDamage = damage;

            foreach (var damageFormula in damageFormulas)
            {
                totalDamage = damageFormula(totalDamage);
            }

            damageReceived += totalDamage;
        }

        public void HealDamage(int healAmount)
        {
            damageReceived -= healAmount;
            if (damageReceived < 0)
            {
                damageReceived = 0;
            }
        }

        public bool IsDead()
        {
            return damageReceived >= cardModel.life;
        }

        public List<BattlegroundsCardModel> GetTargets(Target target)
        {
            var targets = new List<BattlegroundsCardModel>();
            if (target == Target.Self)
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
                if (skill.CanBePerformed(this))
                {
                    var skillActions = skill.CreateSkillActions(this);
                    ActionStackModel.GetInstance().actionQueue.AddRange(skillActions);
                }
            }
        }
    }
}