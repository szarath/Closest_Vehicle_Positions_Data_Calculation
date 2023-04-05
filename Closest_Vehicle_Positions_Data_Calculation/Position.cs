using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Closest_Vehicle_Positions_Data_Calculation
{

    public class Position
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }


}
