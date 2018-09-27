using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerGame
{
    public class ServerGame : Photon.SocketServer.ApplicationBase
    {
        //单例模式
        public static ILogger LOG = LogManager.GetCurrentClassLogger();
        //当有客户端接入时候调用
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ClientPeer(initRequest);
        }
        //当框架启动时候调用
        protected override void Setup()
        {
            //设置配置文件属性
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(Path.Combine(this.ApplicationRootPath, "ServerGame"), "log");//设置日志文件存储目录

            //日志配置文件
            FileInfo logConfigFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (logConfigFileInfo.Exists)//配置文件存在
            {
                //设置Photon日志插件为Log4Next
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                //Log4Next这个插件读取配置文件
                XmlConfigurator.ConfigureAndWatch(logConfigFileInfo);
            }

            LOG.Info("服务器初始化完成");
        }

        //当框架停止时候调用
        protected override void TearDown()
        {
        }
    }


}
