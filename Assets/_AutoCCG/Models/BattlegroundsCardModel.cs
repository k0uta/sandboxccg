using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoCCG
{
    public class BattlegroundsCardModel
    {
        public CardModel cardModel;

        public int damageReceived;

        public int currentMana;

        public BattlegroundsModel playerBattlegrounds;

        public List<CardActionModel> actionQueue = new List<CardActionModel>();

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
                    var skillActions = skill.CreateSkillActions(this);
                    actionQueue.AddRange(skillActions);
                }
            }
        }

        public void PerformPhaseActionQueue(Phase phase)
        {
            var phaseActionsQueue = actionQueue.FindAll((cardAction) => cardAction.phase == phase);

            foreach (var cardAction in phaseActionsQueue)
            {
                cardAction.PerformAction();
                actionQueue.Remove(cardAction);
            }
        }
    }
}