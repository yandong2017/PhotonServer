using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
 
namespace ServerGame
{
    class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public ClientPeer(InitRequest ir) : base(ir) { }

        //该客户端断开连接
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {

        }

        //该客户端出操作请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            /*****解析客户端请求数据*****/
            ReadRequestData(operationRequest);
            /****************************/

            /*****向客户端发送响应*****/
            OperationResponse operationResponse = new OperationResponse();
            operationResponse.OperationCode = 0;
            //数据包
            operationResponse.Parameters = new Dictionary<byte, object> { { 1, "数据提交成功" } };
            //向客户端发送请求响应
            SendOperationResponse(operationResponse, sendParameters);
            /**************************/

            /*****向客户端发送事件*****/
            EventData eventData = new EventData(0, new Dictionary<byte, object> { { 1, "服务器事件" } });
            SendEvent(eventData, new SendParameters());
            /**************************/
        }

        private void ReadRequestData(OperationRequest operationRequest)
        {
            int operationCode = operationRequest.OperationCode;
            switch (operationCode)
            {
                case 0:
                    Dictionary<byte, object> requestData = operationRequest.Parameters;
                    object value1 = requestData.FirstOrDefault(q => q.Key == 1).Value;
                    object value2 = requestData.FirstOrDefault(q => q.Key == 2).Value;

                    ServerGame.LOG.Warn("数据一：" + value1 + "数据二：" + value2);
                    break;
                default:
                    break;
            }
        }
    }
}

