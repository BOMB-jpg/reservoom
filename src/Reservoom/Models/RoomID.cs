using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Models
{
    public class RoomID
    {
        public int FloorNumber { get; }
        public int RoomNumber { get; }

        public RoomID(int floorNumber, int roomNumber)
        {
            FloorNumber = floorNumber;
            RoomNumber = roomNumber;
        }

        public override string ToString()
        {
            return $"{FloorNumber}_{RoomNumber}";
        }
     //重写了equals方法   用来查看 楼层号和房间号是否匹配
        public override bool Equals(object obj)
        {
            return obj is RoomID roomID
                //类型模式匹配  obj 是否是Roomid 的实例并转为roomId
                //也就是说如果是类型匹配成功   就把obj的值输入到roomId中就可以了
                &&
                FloorNumber == roomID.FloorNumber &&
                RoomNumber == roomID.RoomNumber;
            //也就是需要满足三个条件
        }
      //这里也将hashCode进行了重写
        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
          //HashCode.Combine 方法用于将多个值组合成一个哈希码。它接受多个参数，并根据这些参数的值生成一个哈希码。  
        }

        public static bool operator ==(RoomID roomID1, RoomID roomID2)
            //  这个方法是指两个变量进行等于比较操作返回的bool值
        {
            //
            if(roomID1 is null && roomID2 is null)
            {
                return true;
            }
        

            return !(roomID1 is null) && roomID1.Equals(roomID2);
        }

        public static bool operator !=(RoomID roomID1, RoomID roomID2)
        {
            return !(roomID1 == roomID2);
        }

    }
}
