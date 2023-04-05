using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Closest_Vehicle_Positions_Data_Calculation
{

    public class VehiclePosition
    {
        public Int32 PositionId { get; set; }
        public string? VehicleRegistration { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public UInt64 RecordedTimeUTC { get; set; }
    }


}
