﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class RoomOrderDetails
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string StripeSessionId { get; set; }
        [Required]
        public DateTime CheckIndate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }
        [Required]
        public double TotalCost { get; set; }
        [Required]
        public int RoomId { get; set; }
        public bool IsPaymentSuccessful { get; set; } = false;
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        [ForeignKey("RoomId")]
        public HotelRoom HotelRoom { get; set; }
        public string Status { get; set; }
    }
}
