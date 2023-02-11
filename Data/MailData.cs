using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class MailData {
    [JsonIgnore]
    public ObjectId Id;
    public string ownerUid = "";
    public MailContent mailContent = new MailContent();
    public List<ItemMailData> itemMailDatas= new List<ItemMailData>();
    public long sendTime;
    public long expireTime;//截止日期
    public int importance;
    public bool isRead;
    public bool isAttachmentGot;
    public int stateValue;
}
public class MailContent {
    public string _t = "";
    public string title = "";
    public string content = "";
    public string sender = "";

}
public class ItemMailData {
    public string _t = "MailItem";
    public string itemId = "";
    public int itemCount = 0;
    public int itemLevel = 0;
    
}
