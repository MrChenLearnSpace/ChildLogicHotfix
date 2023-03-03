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
