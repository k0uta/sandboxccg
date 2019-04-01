using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class PerformSkillsForPhase : FsmStateAction
    {
        public Phase targetPhase;

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();

            boardController.player.GetComponent<PlayerModel>().RpcPerformSkillsForPhase(targetPhase);

            Finish();
        }
    }

}
