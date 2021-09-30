using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(roomId);
                if (roomDetails != null)
            {
                var allImages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
               
                _db.HotelRoomImages.RemoveRange(allImages);
                _db.HotelRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async  Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string checkInDateSTR,string checkOutDateSTR)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRooms = _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>
                    (_db.HotelRooms.Include(x => x.HotelRoomImages));
                if (!string.IsNullOrEmpty(checkInDateSTR) && !string.IsNullOrEmpty(checkOutDateSTR))
                {
                    foreach (HotelRoomDTO hotelRoom in hotelRooms)
                    {
                        hotelRoom.IsBooked = await IsRoomBooked(hotelRoom.Id, checkInDateSTR, checkOutDateSTR);
                    }
                }
                return hotelRooms;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDateSTR, string checkOutDateSTR)
        {
            try {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom,HotelRoomDTO>
                    ( await _db.HotelRooms.Include(x=>x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));
                if(!string.IsNullOrEmpty(checkInDateSTR)&& !string.IsNullOrEmpty(checkOutDateSTR))
                {
                    hotelRoom.IsBooked = await IsRoomBooked(roomId, checkInDateSTR, checkOutDateSTR);
                }
                return hotelRoom;
            } catch
            {
                return null;
            }
        }
        public async Task<bool> IsRoomBooked(int RoomId, string checkInDateSTR, string checkOutDateSTR)
        {
            try
            {
                if (!string.IsNullOrEmpty(checkOutDateSTR) && !string.IsNullOrEmpty(checkInDateSTR))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDateSTR, "MM/dd/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDateSTR, "MM/dd/yyyy", null);
                    var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == RoomId && x.IsPaymentSuccessful &&
            //check if the checkin date that user wants does not fall in between any dates for that room
            ((checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckIndate)
            //check if the checkin date that user wants does not fall in between any dates for that room
            || (checkOutDate.Date > x.CheckIndate.Date && checkInDate.Date <= x.CheckIndate.Date)
            )).FirstOrDefaultAsync();
                    if (existingBooking != null)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId=0)
        {
            try
            {
                if (roomId == 0)
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>
                        (await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));
                    return hotelRoom;
                }
                else
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>
                        (await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                        && x.Id!=roomId));
                    return hotelRoom;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    //valid
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
