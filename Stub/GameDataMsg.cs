using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class MsgGamePlayerData : MsgBase {
	public MsgGamePlayerData() { protoName = "MsgGamePlayerData"; }
	public PlayerData data=new PlayerData();
}
public class MsgAllPlayerChat : MsgBase {
	public MsgAllPlayerChat() { protoName = "MsgAllPlayerChat"; }
	public string id = "";
	public string data = "";
}
public class MsgAddItem : MsgBase {
    public MsgAddItem() { protoName = "MsgAddItem"; }
    public ItemData data = new ItemData();
}

public class MsgPlayersList : MsgBase {
    public MsgPlayersList() { protoName = "MsgPlayersList"; }
    public List<ActorData> players = new List<ActorData>();
}
public class MsgAddNetPlayer : MsgBase {
    public MsgAddNetPlayer() { protoName = "MsgAddNetPlayer"; }
    public ActorData actorData=new ActorData();
}
public class MsgRemoveNetPlayer : MsgBase {
    public MsgRemoveNetPlayer() { protoName = "MsgRemoveNetPlayer"; }
    public string id = "";
}


