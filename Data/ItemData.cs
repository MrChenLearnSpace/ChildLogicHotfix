using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json;
public class ItemData {
    [JsonIgnore]
    public ObjectId Id;
    public string ownerId = "";
    public string itemId = "";
    public int count= 0;
    public int level= 0;
    public int exp= 0;
    public int totalExp= 0;
    public int promoteLevel= 0;
    public bool locked= false;
    public int refinement= 0;
    public int mainPropld= 0;
    public int equipCharacter= 0;
    public List<int> affixes;
    public List<int> appendPropldList;//附加属性
}



