
using RESPECT.CachingData;
using RESPECT.Models;

namespace RESPECT.ViewModel
{
    class TransferViewModel
    {
        public TransferViewModel()
        {
            CachingData.CurrentData.UpdateData();
        }

        public async void GeneratePoints(int IdReceiver, int IdRoom, int value)
        {
            CachingData.CurrentData.UpdateData();

            int id = CachingData.CurrentData.Server.Points.Count + 1;

            for (int i = 0; i < CachingData.CurrentData.Server.Points.Count; i++)
            {
                if (i != CachingData.CurrentData.Server.Points[i].Id)
                {
                    id = i;
                    break;
                }
            }

            await CachingData.CurrentData.Server.AddPoints(new Models.Points()
            {
                Id = id,
                RoomId = IdRoom,
                UserId = IdReceiver,
                Value = value
            });

            CachingData.CurrentData.UpdateData();
        }

        public async void GivePoints(int IdReceiver, int IdSender, int IdRoom, int value, string description = "")
        {
            CachingData.CurrentData.UpdateData();


            foreach (var item in CachingData.CurrentData.Server.Points)
            {
                if (item.RoomId == IdRoom && IdReceiver == item.UserId)
                {
                    item.Value = item.Value + value;

                    await CachingData.CurrentData.Server.UpdatePoints(item);

                    int nid = CachingData.CurrentData.Server.Notifications.Count + 1;
                    for (int i = 0; i < CachingData.CurrentData.Server.Notifications.Count; i++)
                    {
                        if (i != CachingData.CurrentData.Server.Notifications[i].Id)
                        {
                            nid = i;
                            break;
                        }
                    }

                    await CachingData.CurrentData.Server.AddNotification(new Notification()
                    {
                        Id = nid,
                        IdReceiver = IdReceiver,
                        IdRoom = IdRoom,
                        IdSender = IdSender,
                        Value = value,
                        Description = description
                    });
                }
            }

            CachingData.CurrentData.UpdateData();
        }

        public async void GivePoints(int IdReceiver, int IdRoom, int value, string description = "")
        {
            CurrentData.UpdateData();

            bool flagToTransfer = false;

            foreach (var item in CurrentData.Server.Points)
            {
                if (item.RoomId == IdRoom && CurrentData.CurrentUser.Id == item.UserId)
                {
                    if (item.Value >= value)
                    {
                        item.Value -= value;

                        await CurrentData.Server.UpdatePoints(item);
                    }

                    flagToTransfer = true;
                }
            }

            if (flagToTransfer)
            {
                foreach (var item in CurrentData.Server.Points)
                {
                    if (item.RoomId == IdRoom && IdReceiver == item.UserId)
                    {
                        item.Value = item.Value + value;

                        await CurrentData.Server.UpdatePoints(item);

                        int nid = CurrentData.Server.Notifications.Count + 1;
                        for (int i = 0; i < CurrentData.Server.Notifications.Count; i++)
                        {
                            if (i != CurrentData.Server.Notifications[i].Id)
                            {
                                nid = i;
                                break;
                            }
                        }

                        await CurrentData.Server.AddNotification(new Notification()
                        {
                            Id = nid,
                            IdReceiver = IdReceiver,
                            IdRoom = IdRoom,
                            IdSender = CurrentData.CurrentUser.Id,
                            Value = value,
                            Description = description
                        });
                    }
                }
            }
            else
            {
                throw new System.Exception("Недостаточо очков у пользователя");
            }

            CurrentData.UpdateData();
        }

        public async void PickUpPoints(int IdReceiver, int IdSender, int IdRoom, int value, string description = "")
        {
            CurrentData.UpdateData();
            Points oldVal = new Points();

            foreach (var item in CachingData.CurrentData.Server.Points)
            {
                if (item.RoomId == IdRoom && IdReceiver == item.UserId)
                {
                    item.Value = item.Value - value;

                    await CachingData.CurrentData.Server.UpdatePoints(item);

                    int nid = CachingData.CurrentData.Server.Notifications.Count + 1;
                    for (int i = 0; i < CachingData.CurrentData.Server.Notifications.Count; i++)
                    {
                        if (i != CachingData.CurrentData.Server.Notifications[i].Id)
                        {
                            nid = i;
                            break;
                        }
                    }

                    await CachingData.CurrentData.Server.AddNotification(new Notification()
                    {
                        Id = nid,
                        IdReceiver = IdReceiver,
                        IdRoom = IdRoom,
                        IdSender = IdSender,
                        Value = value,
                        Description = description
                    });
                }
            }

            CachingData.CurrentData.UpdateData();
        }

        public async void ResetPoints(int IdUser, int IdRoom)
        {
            CachingData.CurrentData.UpdateData();

            foreach (var item in CachingData.CurrentData.Server.Points)
            {
                if (item.RoomId == IdRoom && IdUser == item.UserId)
                {
                    item.Value = 1000;

                    await CachingData.CurrentData.Server.UpdatePoints(item);
                }
            }

            CachingData.CurrentData.UpdateData();
        }

        public async void GiveRoots(int IdUser, int IdRoom)
        {
            CachingData.CurrentData.UpdateData();

            foreach (var item in CachingData.CurrentData.Server.Points)
            {
                if (item.RoomId == IdRoom && IdUser == item.UserId)
                {
                    item.Value = 500000;

                    await CachingData.CurrentData.Server.UpdatePoints(item);
                }
            }

            CachingData.CurrentData.UpdateData();
        }
    }
}
