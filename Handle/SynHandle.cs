using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildLogicHotfix {
    public partial class HandlePlayerMsg {
        public void MsgSyncTransform(IPlayer iplayer, ProtocolBase protoBase) {
            Player player = iplayer as Player;
            if (player == null) return;

            ProtocolBytes protocolBytes = (ProtocolBytes)protoBase;
            MsgSyncTransform msgSyncTransform = new MsgSyncTransform();
            msgSyncTransform = (MsgSyncTransform)msgSyncTransform.Decode(protocolBytes);
            player.data.pos = msgSyncTransform.pos;
            player.data.rot = msgSyncTransform.rot;
            foreach (var player1 in PlayerManager.players.Values) {
                if(player1.id== player.id)  continue;
                player1.SendAsync(protocolBytes);
            }
        }
        public void MsgSyncAnimator(IPlayer iplayer, ProtocolBase protoBase) {
            Player player = iplayer as Player;
            if (player == null) return;
            foreach (var player1 in PlayerManager.players.Values) {
                if (player1.id == player.id) continue;
                player1.SendAsync(protoBase);
            }
        }
    }
}
