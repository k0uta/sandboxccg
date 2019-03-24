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

            for (int i = battlegroundsCards.Count - 1; i >= 0; i--)
            {
                targetPlayer.GetComponent<PlayerModel>().RpcCheckAndRemoveDeadCard(i);
            }
        }
    }

}
