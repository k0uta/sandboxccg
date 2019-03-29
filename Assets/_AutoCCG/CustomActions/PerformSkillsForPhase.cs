using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class PerformSkillsForPhase : FsmStateAction
    {
        public CardSkillPhase phase;

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();

            var playerModel = boardController.player.GetComponent<PlayerModel>();
            var enemyModel = boardController.enemy.GetComponent<PlayerModel>();

            playerModel.RpcPerformBattlegroundsPhaseSkills(phase);
            enemyModel.RpcPerformBattlegroundsPhaseSkills(phase);

            Finish();
        }
    }

}
