#pragma warning disable CS8601
using MongoDB.Driver;
using Newtonsoft.Json;
using ServerCore;
using ServerCore.net;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChildLogicHotfix {
    public class LogicManager {
        public static  void Init() {
            
            DataMgr dataMgr = DataMgr.GetInstance();
            dataMgr.database = new Mongo();
            dataMgr.Connect("child", "127.0.0.1", "27017", "root", "");
            ServNet servNet =new ServNet();
            servNet.HandleDllName = "ChildLogicHotfix";
            servNet.Start("127.0.0.1", 33333);
            servNet.heartBeatTime= 5;
            servNet.displayConsole = new List<string>() {
                "MsgHeatBeat","MsgSyncTransform",
            };


        }


        // public static bool isActive = false;
        #region 数据库全局
        public static string Serialize(PlayerData playerData) {


            try {

                return JsonConvert.SerializeObject(playerData);
            }
            catch (Exception e) {
                Console.WriteLine("[DataMgr]CreatePlayer 序列化" + e.Message);
                return "";
            }
        }
        public static bool UnSerialize(string playerStream, ref PlayerData playerdata) {
            try {
                playerdata = JsonConvert.DeserializeObject<PlayerData>(playerStream);
                return true;
            }
            catch (SerializationException e) {
                Console.WriteLine("[DataMgr]GetPlayerData 反序列化" + e.Message);
                return false;
            }
        }
        public static bool CreatePlayer(string id, Conn conn) {
            try {
                if (!DataMgr.instance.IsSafeStr(id))
                    return false;
                PlayerData playerData = new PlayerData();

                Mongo mongo = DataMgr.instance.database as Mongo;
                if (mongo == null)
                    return false;
                IMongoCollection<PlayerData> collection = mongo.database.GetCollection<PlayerData>("players");
                FilterDefinition<PlayerData> filterDefinition = Builders<PlayerData>.Filter.Eq("id", id);
                List<PlayerData> list= collection.Find(filterDefinition).ToList();
                if(list.Count>0) {
                    Console.WriteLine("CreatePlayer Fail " + id + " is already exist.");
                    return false;
                }
                playerData.id = id;
                IMongoCollection<AccountData> account = mongo.database.GetCollection<AccountData>("accounts");
                FilterDefinition<AccountData> accountFilter = Builders<AccountData>.Filter.Eq("id", id);
                UpdateDefinition<AccountData> updateDefinition = Builders<AccountData>.Update.Set("ip", conn.GetAdress());
                account.UpdateOne(accountFilter, updateDefinition);
                //playerData.ip = conn.GetAdress();
                collection.InsertOne(playerData);

                return true;
            }
            catch (Exception ex) {
                Console.WriteLine("[Player] CreatePlayer " + ex.Message);
                return false;
            }
        }
        /* public bool KickOff(string id, ProtocolBase proto) {
             Conn[] conns = ServNet.instance.clients;
             for (int i = 0; i < conns.Length; i++) {
                 if (!conns[i].isUse) continue;
                 if (conns[i].player == null) continue;
                 if (conns[i].player == null) continue;
                 if (conns[i].player.GetId() == id) {
                     lock (conns[i].player) {
                         if (proto != null) {
                             conns[i].player.Send(proto);
                         }
                         return conns[i].player.Logout();
                     }
                 }
             }
             return true;
         }*/
        #endregion
        #region debug部分
        public  void RuntimeDataReflash() {
            PlayerManager.Reflash();
            //PlayerManager.Print();
            //ServNet.instance.Print();
        }
        public void MsgAddItem() {
            MsgAddItem msgAddItem = new MsgAddItem();
            msgAddItem.data.ownerId = "1";
            msgAddItem.data.itemId = "0001";
            var mondb = ((Mongo)DataMgr.instance.database).database.GetCollection<ItemData>("items");
            mondb.InsertOne(msgAddItem.data);
        }

            #endregion
        }
}
