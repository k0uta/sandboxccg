using AutoCCG;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class RemoveDeadCards : FsmStateAction
    {
        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();

            PurgeDeadCards(boardController.player);
            PurgeDeadCards(boardController.enemy);

            Finish();
        }

        void PurgeDeadCards(GameObject targetPlayer)
        {
            var battlegrounds = targetPlayer.GetComponentInChildren<BattlegroundsModel>();
            var battlegroundsCards = battlegrounds.battlegroundsCards;
            var handModel = targetPlayer.GetComponentInChildren<HandModel>();
            var handController = targetPlayer.GetComponentInChildren<HandController>();

            for (int i = battlegroundsCards.Count - 1; i >= 0; i--)
            {
                var battlegroundsCard = battlegroundsCards[i];
                if (battlegroundsCard.IsDead())
                {
                    battlegrounds.RemoveBattlegroundsCard(battlegroundsCard);

                    // Not cool
                    handController.RemoveCardFromBattlegroundsQueue(handController.battlegroundsQueueCards[i]);

                    handModel.RemoveCard(battlegroundsCard.cardModel);
                }
            }
        }
    }

}
