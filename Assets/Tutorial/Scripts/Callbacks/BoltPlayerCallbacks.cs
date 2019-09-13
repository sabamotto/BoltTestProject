using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt.AdvancedTutorial;

[BoltGlobalBehaviour("Level2")]
public class BoltPlayerCallbacks : Bolt.GlobalEventListener {
    public override void SceneLoadLocalDone(string scene) {
        PlayerCamera.Instantiate();
    }

    public override void ControlOfEntityGained(BoltEntity entity) {
        PlayerCamera.instance.SetTarget(entity);
    }
}
