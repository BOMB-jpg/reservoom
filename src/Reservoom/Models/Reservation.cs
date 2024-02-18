using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Models
{
    public class Reservation
    {

        //为什么属性都是get属性  因为在对象创建后就不在改变  getter是适用于只读  而不在用于修改所以不需要
        public RoomID RoomID { get; }
        public string Username { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
//subtract方法是TimeSpan结构体的
        public TimeSpan Length => EndTime.Subtract(StartTime);

        public Reservation(RoomID roomID, string username, DateTime startTime, DateTime endTime)
        {
            RoomID = roomID;
            Username = username;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
