using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class SetupGame : FsmStateAction
    {
        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();
            var playerModel = boardController.player.GetComponentInChildren<PlayerModel>();
            var enemyModel = boardController.enemy.GetComponentInChildren<PlayerModel>();

            playerModel.RpcSyncBattlegroundsAndDeck(DeckNames.Cats);
            enemyModel.RpcSyncBattlegroundsAndDeck(DeckNames.Dogs);

            Finish();
        }
    }

}
