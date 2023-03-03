
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
        public static void AddPlayer(string id, Player player) {

            if (!players.ContainsKey(id)) {
                players.Add(id, player);
            }
            else {
                players[id] = player;
            }
            MsgAddNetPlayer msgAddNetPlayer = new MsgAddNetPlayer();
            msgAddNetPlayer.actorData = player.data;
            ProtocolBytes protocolBytes = msgAddNetPlayer.Encode();
            foreach (Player player1 in players.Values) {
                player1.SendAsync(protocolBytes);

            }
        }
        public static void RemovePlayer(string id) {
            
                if (players.ContainsKey(id)) {
                    players.Remove(id);
                }
                MsgRemoveNetPlayer msgRemoveNetPlayer = new MsgRemoveNetPlayer();
                ProtocolBytes protocolBytes = msgRemoveNetPlayer.Encode();
                foreach (Player player1 in players.Values) {
                    player1.SendAsync(protocolBytes);
                }
            
        }
    }
}