using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "Level2")]
public class BoltServerCallbacks : Bolt.GlobalEventListener {
    private void Awake() {
        BoltPlayerObjectRegistry.CreateServerPlayer();
    }

    public override void Connected(BoltConnection connection) {
        BoltPlayerObjectRegistry.CreateClientPlayer(connection);
    }

    public override void SceneLoadLocalDone(string scene) {
        //BoltNetwork.Instantiate(BoltPrefabs.BoltPlayer);
        BoltPlayerObjectRegistry.ServerPlayer.Spawn();
    }

    public override void SceneLoadRemoteDone(BoltConnection connection) {
        //BoltNetwork.Instantiate(BoltPrefabs.BoltPlayer);
        BoltPlayerObjectRegistry.GetBoltPlayer(connection).Spawn();
    }
}
