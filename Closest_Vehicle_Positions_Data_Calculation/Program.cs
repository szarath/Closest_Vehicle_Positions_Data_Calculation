using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Closest_Vehicle_Positions_Data_Calculation
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatchtotal = Stopwatch.StartNew();
            

            string dataFilePath = "VehiclePositions.dat";
            var positionsToFind = new List<Position>
            {
                new Position(34.544909, -102.100843),
                new Position(32.345544, -99.123124),
                new Position(33.234235, -100.214124),
                new Position(35.195739, -95.348899),
                new Position(31.895839, -97.789573),
                new Position(32.895839, -101.789573),
                new Position(34.115839, -100.225732),
                new Position(32.335839, -99.992232),
                new Position(33.535339, -94.792232),
                new Position(32.234235, -100.222222)
            };
            var stopwatchdata = Stopwatch.StartNew();
            var vehiclePositionService = new BinaryVehiclePositionService(dataFilePath);
            stopwatchdata.Stop();
            Console.WriteLine($"Data file read execution time: {stopwatchdata.ElapsedMilliseconds} ms");


            var stopwatchfind = Stopwatch.StartNew();
            var closestVehiclePositions = vehiclePositionService.FindClosestPositions(positionsToFind);
            stopwatchfind.Stop();
            Console.WriteLine($"Closest position calculation execution time: {stopwatchfind.ElapsedMilliseconds} ms");
            //foreach (var closestPosition in closestVehiclePositions)
            //{
            //    Console.WriteLine($"Closest vehicle position for {closestPosition.Item1.Latitude},{closestPosition.Item1.Longitude} is {closestPosition.Item2.Latitude},{closestPosition.Item2.Longitude} with distance {closestPosition.Item3}");
            //}
            stopwatchtotal.Stop();
            Console.WriteLine($"Total execution time: {stopwatchtotal.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }
    }

}