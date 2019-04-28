using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class BattlegroundsCardModel
    {
        public CardModel cardModel;

        public int damageReceived;

        public int currentMana;

        public BattlegroundsModel playerBattlegrounds;

        public BattlegroundsCardView battlegroundsCardView;

        public List<Func<int, int>> damageFormulas = new List<Func<int, int>>();

        public BattlegroundsCardModel(CardModel cardModel)
        {
            this.cardModel = cardModel;
            damageReceived = 0;
            currentMana = 0;
        }

        public int ApplyDamage(int damage)
        {
            int totalDamage = damage;

            foreach (var damageFormula in damageFormulas)
            {
                totalDamage = damageFormula(totalDamage);
            }

            totalDamage = Mathf.Max(0, totalDamage);

            damageReceived += totalDamage;

            return totalDamage;
        }

        public int HealDamage(int healAmount)
        {
            damageReceived -= healAmount;
            if (damageReceived < 0)
            {
                damageReceived = 0;
            }

            return healAmount;
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
                    var skillAction = skill.CreateAction(this);
                    ActionStackModel.GetInstance().actionQueue.Add(skillAction);
                }
            }
        }

        public void CleanupTokenSkills()
        {
            var tokenSkills = cardModel.cardSkills.RemoveAll(EmptyTokenSkill);
        }

        bool EmptyTokenSkill(CardSkillModel cardSkill)
        {
            var cardTokenSkill = cardSkill as CardTokenSkillModel;

            return cardTokenSkill != null && cardTokenSkill.count <= 0;
        }

        public PlayerModel GetTargetPlayer(TargetPlayer target)
        {
            if (target == TargetPlayer.Player)
            {
                return cardModel.owner;
            } else
            {
                return playerBattlegrounds.enemyBattlegrounds.battlegroundsCards[0].cardModel.owner;
            }
        }

        public List<BattlegroundsCardModel> GetArea(Area area, TargetPlayer targetPlayer = TargetPlayer.Player)
        {
            switch (area)
            {
                case Area.Self:
                {
                    var list = new List<BattlegroundsCardModel> {this};
                    return list;
                }
                case Area.Owned:
                {
                    var list = new List<BattlegroundsCardModel>();
                    foreach (var battlegroundsCard in playerBattlegrounds.battlegroundsCards)
                    {
                        list.Add(battlegroundsCard);
                    }

                    foreach (var card in cardModel.owner.handModel.cards)
                    {
                        if (list.Exists((battlegroundsCardModel) => battlegroundsCardModel.cardModel == card)) continue;
                    
                        var battlegroundsCard = new BattlegroundsCardModel(card);
                        list.Add(battlegroundsCard);
                    }
                    return list;
                }
                default:
                    return playerBattlegrounds.GetArea(area, targetPlayer);
            }
        }
    }
}