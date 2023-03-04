using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MsgSyncTransform : MsgBase {
    public MsgSyncTransform() { protoName = "MsgSyncTransform"; }
    //服务端回
    public Vec3 pos=new Vec3();
    public Vec3 rot=new Vec3();
}
public class MsgSyncAnimator : MsgBase {
    public MsgSyncAnimator() { protoName = "MsgSyncAnimator"; }
    public string id = "";
    public AnimationStatusMessager messager = new AnimationStatusMessager();

}

public class AnimationStatusMessager {
    public int stateHash;      // if non-zero, then Play() this animation, skipping transitions
    public float normalizedTime;
    public ProtocolBytes parameters = new ProtocolBytes();
}

public class AnimationTriggerMessage {

}
