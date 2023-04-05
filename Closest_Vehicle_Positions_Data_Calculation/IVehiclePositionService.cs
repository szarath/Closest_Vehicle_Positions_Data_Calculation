using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Closest_Vehicle_Positions_Data_Calculation
{

    public interface IVehiclePositionService
    {
        List<(Position, VehiclePosition, double)> FindClosestPositions(List<Position> positionsToFind);
    }


}
