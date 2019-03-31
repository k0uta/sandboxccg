using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class SyncBoardVariables : FsmStateAction
    {
        [Tooltip("The board view")]
        public BoardView boardView;

        public BoardPhase phase;

        public FsmInt phaseSeconds;

        public FsmInt currentTurn;

        public FsmInt maxTurns;

        public FsmString gameWinner;

        public FsmBool everyFrame;

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            boardView.RpcUpdateVariables(phase, phaseSeconds.Value, currentTurn.Value, maxTurns.Value, gameWinner.Value);

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            boardView.RpcUpdateVariables(phase, phaseSeconds.Value, currentTurn.Value, maxTurns.Value, gameWinner.Value);
        }
    }

}
