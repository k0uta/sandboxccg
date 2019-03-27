using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;

namespace AutoCCG
{

    public class NetworkLobbyHook : LobbyHook
    {
        public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
        {
            LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
            PlayerModel playerModel = gamePlayer.GetComponent<PlayerModel>();

            playerModel.playerName = lobby.playerName;
        }
    }
}