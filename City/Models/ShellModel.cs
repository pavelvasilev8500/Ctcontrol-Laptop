using ClassesLibrary.Client;
using System.Threading;
using ClassesLibrary.ServerWork;
using ClassesLibrary.SystemControls;

namespace City.Models
{
    class ShellModel
    {
        private string ClientId { get; set; }
        private string SystemUri { get; } = Properties.Settings.Default.SystemUri;
        private string ClientUri { get; } = Properties.Settings.Default.ClientUri;
        private string StatusUri { get; } = Properties.Settings.Default.StatusUri;

        CancellationTokenSource cts = new CancellationTokenSource();
        public ShellModel(string id)
        {
            ClientId = id;
            ThreadController();
        }

        private void ThreadController()
        {
            Thread systemdata = new Thread(() =>
            {
                GetSystemData();
            });
            Thread clientdata = new Thread(() =>
            {
                PutClientData();
            });
            Thread checkdbrecord = new Thread(() =>
            {
                CheckDBRecord();
            });
            systemdata.Name = "GetSystemDataThread";
            clientdata.Name = "GetClientDataThread";
            checkdbrecord.Name = "CheckDbRecordThread";
            if (Get.GetId(SystemUri, ClientId))
            {
                systemdata.Start();
            }
            else
            {
                PostSystemData();
                systemdata.Start();
            }
            if (Get.GetId(ClientUri, ClientId))
            {
                clientdata.Start();
            }
            else
            {
                PostClientData();
                clientdata.Start();
            }
            if (Get.GetId(StatusUri, ClientId))
            {
                PutStatusData(true);
            }
            else
            {
                PostStatusData(true);
            }
            checkdbrecord.Start();
        }

        private void GetSystemData()
        {
            do
            {
                if (Get.GetSystemData(SystemUri, ClientId).Item1)
                {
                    PutSystemData();
                    PutStatusData(false);
                    SystemControl.halt(false, false);
                }
                else if (Get.GetSystemData(SystemUri, ClientId).Item2)
                {
                    PutSystemData();
                    PutStatusData(false);
                    SystemControl.halt(true, false);
                }
                else if (Get.GetSystemData(SystemUri, ClientId).Item3)
                {
                    PutSystemData();
                    PutStatusData(false);
                    SystemControl.Sleep(false, false, false);
                    Thread.Sleep(10000);
                    PutStatusData(true);
                }
                Thread.Sleep(1000);
            }
            while (!cts.IsCancellationRequested);
        }

        private void PostSystemData()
        {
            Post.PostData(SystemUri, CreateJson.CreateDataJson(new ClassesLibrary.DataModels.SystemDataModel(), ClientId));
        }

        private void PostClientData()
        {
            Post.PostData(ClientUri, CreateJson.CreateDataJson(new ClassesLibrary.DataModels.ClientDataModel(), ClientId));
        }

        private void PostStatusData(bool status)
        {
            Post.PostData(StatusUri, CreateJson.CreateDataJson(new ClassesLibrary.DataModels.StatusDataModel(), ClientId, status));
        }

        private void PutSystemData()
        {
            Put.PutData(SystemUri, ClientId, CreateJson.CreateDataJson(new ClassesLibrary.DataModels.SystemDataModel(), ClientId));
        }

        private void PutClientData()
        {
            do
            {
                Put.PutData(ClientUri, ClientId, CreateJson.CreateDataJson(new ClassesLibrary.DataModels.ClientDataModel(), ClientId));
                Thread.Sleep(2000);
            }
            while (!cts.IsCancellationRequested);
        }

        private void PutStatusData(bool status)
        {
            Put.PutData(StatusUri, ClientId, CreateJson.CreateDataJson(new ClassesLibrary.DataModels.StatusDataModel(), ClientId, status));
        }

        private void CheckDBRecord()
        {
            do
            {
                if (!Get.GetId(SystemUri, ClientId))
                    PostSystemData();
                if (!Get.GetId(ClientUri, ClientId))
                    PostClientData();
                if (!Get.GetId(StatusUri, ClientId))
                    PostStatusData(true);
                Thread.Sleep(5000);
            }
            while (!cts.IsCancellationRequested);
        }
    }
}
