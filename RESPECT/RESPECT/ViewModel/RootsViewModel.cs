
using RESPECT.CachingData;
using RESPECT.Models;

namespace RESPECT.ViewModel
{
    class RootsViewModel
    {
        public bool IsModerator(Room currentRoom)
        {
            CurrentData.UpdateData();
            int userId = CurrentData.CurrentUser.Id;

            foreach (var item in CurrentData.Server.Points)
            {
                if (item.UserId == userId && item.RoomId == currentRoom.Id)
                {
                    if (item.Value == 500000)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsMember(Room currentRoom)
        {
            CurrentData.UpdateData();
            int userId = CurrentData.CurrentUser.Id;

            foreach (var item in CurrentData.Server.Points)
            {
                if (item.UserId == userId && item.RoomId == currentRoom.Id)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCurrentUser(User profile_user)
        {
            if (CachingData.CurrentData.CurrentUser.Id == profile_user.Id)
            {
                return true;
            }

            return false;
        }
    }
}
