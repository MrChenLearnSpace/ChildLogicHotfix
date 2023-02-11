using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ServerCore;
using ServerCore.net;
using MongoDB.Driver;


namespace ChildLogicHotfix {
    public class Player : IPlayer {
        
        public PlayerData data;
        public PlayerTempData tempData;

        public string Id { get => id; set => id =value; }
        public Conn Connect { get => client; set => client =value; }

        //public int Id { get; set; }
        public Player() { }
        public Player(string id, Conn conn) {
            this.id = id;
            this.client = conn;
            tempData = new PlayerTempData();
        }
        public override void Send(ProtocolBase protocol) {
            if (client == null)
                return;
            client.Send(protocol);
            //ServNet.instance.Send(conn, protocol);
        }
        public void SendAsync(ProtocolBase protocol) {
            if (client == null) return;
            client.SendAsync(protocol);
        }

        public override bool Logout() {
            //ServNet.instance.handlePlayerEvent.OnLogout(this);

            //CodeLoader.GetInstance().FindFunRun("MMONetworkServer.Logic.HandlePlayerEvent", "OnLogout", new object[] { this });
            HandlePlayerEvent.OnLogout(this);
            if (!SavePlayer())
                return false;
            client.player = null;
            client.Close();
            return true;
        }
        public override bool SavePlayer() {
            try {
                if (!DataMgr.instance.IsSafeStr(id))
                    return false;
                FilterDefinition<PlayerData> filter = Builders<PlayerData>.Filter.Eq("id", id);
                ReplaceOneResult result = ((Mongo)DataMgr.instance.database).database.GetCollection<PlayerData>("players").ReplaceOne(filter, data);
                return result.IsAcknowledged;
            }
            catch (Exception ex) {
                Console.WriteLine("[Player] SavePlayer " + ex.Message);
                return false;
            }
            //LogicManager logic = new LogicManager();
            //string buff = LogicManager.Serialize(data);
            //return DataMgr.instance.SavePlayerStream(id, buff, client.GetAdress());
        }

        public override bool GetPlayerData() {
            try {
                if (!DataMgr.instance.IsSafeStr(id))
                    return false;
                FilterDefinition<PlayerData> filter = Builders<PlayerData>.Filter.Eq("id", id);
                List<   PlayerData> playerList = ((Mongo)DataMgr.instance.database).database.GetCollection<PlayerData>("players").Find(filter).ToList();
                if (playerList.Count != 1) {
                    Console.WriteLine("[Player] GetPlayerData Count = " + playerList.Count);
                    return false;
                }
                data = playerList[0];
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine("[Player] GetPlayerData " + ex.Message);
                return false;
            }
        }

    }
}
