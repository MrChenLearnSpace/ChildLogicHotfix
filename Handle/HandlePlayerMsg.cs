using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Newtonsoft.Json;
using ServerCore;

namespace ChildLogicHotfix {
    //处理角色消息 ，具体是登录成功后的逻辑,比如:强化装备、打副本
    public partial class HandlePlayerMsg {
       
        public void MsgGamePlayerData(IPlayer iplayer, ProtocolBase protoBase) {
            // if (iplayer == null) return;
            Player player = iplayer as Player;
            if (player == null) return;
           
            var mondb = ((Mongo)DataMgr.instance.database).database;
            FilterDefinition<ItemData> filterDefinition = Builders<ItemData>.Filter.Eq("ownerId", player.id);
            player.data.itemDatas =mondb.GetCollection<ItemData>("items").Find(filterDefinition).ToList();
            string data = JsonConvert.SerializeObject(player.data);
            MsgGamePlayerData msgGamePlayerData = new MsgGamePlayerData();
            msgGamePlayerData.data = player.data;
            ProtocolBytes protocolGetPlayerData = msgGamePlayerData.Encode();
            //protocolGetPlayerData.AddString("MsgGetPlayerData");
            //protocolGetPlayerData.AddString(data);
            player.client.SendAsync(protocolGetPlayerData);
        }
        public void MsgAddItem(IPlayer iplayer, ProtocolBase protocolBase) {
            Player player = iplayer as Player;
            if (player == null) return;
            var mondb = ((Mongo)DataMgr.instance.database).database.GetCollection<ItemData>("items");
            ProtocolBytes protocolBytes = protocolBase as ProtocolBytes;
            MsgAddItem msgAddItem = new MsgAddItem();
            msgAddItem = msgAddItem.Decode(protocolBytes) as MsgAddItem;
            if (msgAddItem == null) return;
            msgAddItem.data.ownerId = player.id;
            mondb.InsertOne(msgAddItem.data);
        }
        public void MsgGetAchieve(IPlayer iplayer, ProtocolBase protocolBase) {

            //Player player = iplayer as Player;
            //if (player == null) {
            //    Console.WriteLine("Player is null");
            //    return;
            //}
            ////  ProtocolBytes protocolBytes = protocolBase as ProtocolBytes;
            //MsgGetAchieve msgGetAchieve = new MsgGetAchieve();

            //msgGetAchieve.win = player.data.winNum;
            //msgGetAchieve.lost = player.data.lostNum;
            //protocolBase = msgGetAchieve.Encode();
            //player.SendAsync(protocolBase);

        }
    }
}
