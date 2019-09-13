using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPlayerObject {
    public BoltEntity character;
    public BoltConnection connection;

    public bool IsServer => connection == null;
    public bool IsClient => connection != null;

    public void Spawn() {
        if (!character) {
            character = BoltNetwork.Instantiate(BoltPrefabs.BoltPlayer, RandomPosition(), Quaternion.identity);

            if (IsServer) {
                character.TakeControl();
            } else {
                character.AssignControl(connection);
            }
        }

        character.transform.position = RandomPosition();
    }

    Vector3 RandomPosition() => new Vector3(
        Random.Range(-32f, +32f),
        32f,
        Random.Range(-32f, +32f)
    );
}
