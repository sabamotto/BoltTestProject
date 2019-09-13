using System.Collections;
using System.Collections.Generic;

public static class BoltPlayerObjectRegistry {
    static readonly List<BoltPlayerObject> players = new List<BoltPlayerObject>();
    public static IEnumerable<BoltPlayerObject> AllPlayers => players;
    public static BoltPlayerObject ServerPlayer => players.Find(player => player.IsServer);

    static BoltPlayerObject CreatePlayer(BoltConnection connection) {
        var player = new BoltPlayerObject();
        player.connection = connection;

        if (player.connection != null) {
            player.connection.UserData = player;
        }

        players.Add(player);

        return player;
    }

    public static BoltPlayerObject CreateServerPlayer() {
        return CreatePlayer(null);
    }

    public static BoltPlayerObject CreateClientPlayer(BoltConnection connection) {
        return CreatePlayer(connection);
    }

    public static BoltPlayerObject GetBoltPlayer(BoltConnection connection) {
        if (connection = null) return ServerPlayer;
        return (BoltPlayerObject)connection.UserData;
    }
}
