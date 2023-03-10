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
                playerData.ip = conn.GetAdress();
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
        public void MsgAddWeaponData() {
            WeaponData weaponData = new WeaponData();
            weaponData.name = "aaa";
            weaponData.atk = 50;
            weaponData.dft = 3;
            weaponData.hp = 20;
            
            PlayerManager.players["1"].data.weapons.Add(weaponData);

        }

            #endregion
        }
}
