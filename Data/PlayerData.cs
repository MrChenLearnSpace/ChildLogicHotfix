using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
[Serializable]
public class PlayerData : ActorData {
    public Properties properties=new Properties();
    [BsonIgnore]
    public List<ItemData> itemDatas= new List<ItemData>();
    public PlayerData() {
        pos.x = -232;
        pos.z = -237;
    }

}
public class ActorData  {
    public string id = "";
    public Vec3 pos = new Vec3();
    public Vec3 rot = new Vec3();
}
public class Properties {
    public float Maxhp = 1000;
    public float hp = 1;
    public float atk = 0;//attack
    public float dft = 0;//defend
    public float attRange = 1;
    
}
//public class WeaponData {
//    public string name ="";
//    public float hp = 0;
//    public float atk =0;
//    public float dft = 0;
//}   
//public class FoodData {
//    public string name = "";
//    public float hp = 0;
//    public float atk = 0;
//    public float dft = 0;

//}

public class Vec3 {
    public float x = 0;
    public float y = 0;
    public float z = 0;
}

public class AccountData {
    public string id = "";
    public string pw = "";
    public string ip = "";
    public long banStartTime;
    public long banEndTime;
    public bool isBanned;
}



