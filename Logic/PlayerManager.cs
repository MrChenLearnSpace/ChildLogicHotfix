
using ServerCore.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildLogicHotfix {
    public class PlayerManager {
        public static Dictionary<string, Player> players = new Dictionary<string, Player>();
        public static void Reflash() {
            players.Clear();
            List<Conn> clients = ServNet.instance.clients;
            for (int i = 0; i < clients.Count; i++) {
                clients[i].player = new Player(clients[i].player.id, clients[i]);
                clients[i].player.GetPlayerData();
                players.Add(clients[i].player.id, (Player)clients[i].player);
            }
        }
        public static void Print() {
            foreach (string id in players.Keys) {
                Console.WriteLine(id+ " 连接 [" + players[id].client.GetAdress() + "]");
            }
        }
        public static void SavePlayers() {
            foreach (string id in players.Keys) {
                players[id].SavePlayer();
            }
        }
    }
}