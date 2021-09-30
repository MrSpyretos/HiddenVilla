using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IRoomOrderDetailsRepository
    {
        public Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details);
        public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(int id);
        public Task<IEnumerable<RoomOrderDetailsDTO>> GetAllRoomOrderDetail();
        public Task<RoomOrderDetailsDTO> GetRoomOrderDetails(int roomOrderId);
        public Task<bool> UpdateOrderDetails(int RoomOrderId, string status);
    }

}
