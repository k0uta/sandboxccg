using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class RestockShopsAndIncrementGold : FsmStateAction
    {
        public FsmInt goldIncrement;

        public FsmInt currentTurn;

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();
            var playerModel = boardController.player.GetComponentInChildren<PlayerModel>();
            var enemyModel = boardController.enemy.GetComponentInChildren<PlayerModel>();

            playerModel.RpcSetCurrentTurn(currentTurn.Value);
            enemyModel.RpcSetCurrentTurn(currentTurn.Value);

            playerModel.CmdRestock();
            enemyModel.CmdRestock();

            playerModel.RpcGiveGold(goldIncrement.Value);
            enemyModel.RpcGiveGold(goldIncrement.Value);

            Finish();
        }
    }

}
