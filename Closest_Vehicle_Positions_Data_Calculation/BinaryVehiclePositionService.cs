using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Closest_Vehicle_Positions_Data_Calculation
{
    public class BinaryVehiclePositionService : IVehiclePositionService
    {
        private readonly string _dataFilePath;
        private readonly List<VehiclePosition> _vehiclePositions;

        public BinaryVehiclePositionService(string dataFilePath)
        {
            _dataFilePath = dataFilePath;
            _vehiclePositions = LoadVehiclePositionsFromBinaryFile();
        }

        /// <summary>
        /// Loads vehicle positions from a binary file.
        /// </summary>
        /// <returns>A list of vehicle positions.</returns>
        private List<VehiclePosition> LoadVehiclePositionsFromBinaryFile()
        {
            // Create a list to store the vehicle positions
            var vehiclePositions = new List<VehiclePosition>();

            // Open a file stream to read the binary file
            using (var fileStream = new FileStream(_dataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan))
            {
                // Create a binary reader to read the file stream
                using (var reader = new BinaryReader(fileStream))
                {
                    // Read the file until the end of the stream
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        // Create a new vehicle position object
                        var position = new VehiclePosition
                        {
                            PositionId = reader.ReadInt32(),
                            VehicleRegistration = Utility.ReadNullTerminatedString(reader, Encoding.ASCII),
                            Latitude = reader.ReadSingle(),
                            Longitude = reader.ReadSingle(),
                            RecordedTimeUTC = reader.ReadUInt64()
                        };

                        // Add the vehicle position to the list
                        vehiclePositions.Add(position);
                    }
                }
            }

            // Return the list of vehicle positions
            return vehiclePositions;
        }

        // This method iterates through a list of positions and finds the closest vehicle position for each one.
        // It then calculates the distance between the two positions and adds the result to a list of tuples.
        // The list of tuples contains the original position, the closest vehicle position, and the distance between them.
        /// <summary>
        /// Finds the closest vehicle position to a given position.
        /// </summary>
        /// <param name="positionToFind">The position to find the closest vehicle position for.</param>
        /// <returns>The closest vehicle position to the given position.</returns>
        public List<(Position, VehiclePosition, double)> FindClosestPositions(List<Position> positionsToFind)
        {
            // Create a list to store the results
            var results = new List<(Position, VehiclePosition, double)>();

            // Iterate through the list of positions
            foreach (var positionToFind in positionsToFind)
            {
                // Find the closest vehicle position
                var closestPosition = FindClosestVehiclePosition(positionToFind);

                // If a closest position was found
                if (closestPosition != null)
                {
                    // Calculate the distance between the two positions
                    var distance = CalculateDistance(positionToFind, closestPosition);

                    // Add the result to the list of tuples
                    results.Add((positionToFind, closestPosition, distance));
                }
            }

            // Return the list of tuples
            return results;
        }

        //This function finds the closest VehiclePosition to the given Position
        /// <summary>
        /// Finds the closest VehiclePosition to the given Position.
        /// </summary>
        /// <param name="positionToFind">The Position to find the closest VehiclePosition to.</param>
        /// <returns>The closest VehiclePosition to the given Position.</returns>
        private VehiclePosition FindClosestVehiclePosition(Position positionToFind)
        {
            //Initialize minDistance to the maximum possible double value
            var minDistance = double.MaxValue;
            //Initialize closestVehiclePosition to the default value
            var closestVehiclePosition = default(VehiclePosition);

            //Loop through all VehiclePositions
            foreach (var vehiclePosition in _vehiclePositions)
            {
                //Calculate the distance between the given Position and the current VehiclePosition
                var distance = CalculateDistance(positionToFind, vehiclePosition);
                //If the distance is less than the current minDistance
                if (distance < minDistance)
                {
                    //Set minDistance to the current distance
                    minDistance = distance;
                    //Set closestVehiclePosition to the current VehiclePosition
                    closestVehiclePosition = vehiclePosition;
                }
            }

            //Return the closest VehiclePosition
            return closestVehiclePosition;
        }

        // This function calculates the distance between two positions using the Haversine formula.
        // It takes two Position objects as parameters and returns the distance in kilometers.
        /// <summary>
        /// Calculates the distance between two positions using the Haversine formula.
        /// </summary>
        /// <param name="position1">The first position.</param>
        /// <param name="position2">The second position.</param>
        /// <returns>The distance between the two positions in kilometers.</returns>
        private static double CalculateDistance(Position position1, VehiclePosition position2)
        {
            // Earth's radius in km
            const double earthRadius = 6371;

            // Convert the latitude and longitude of both positions to radians
            var lat1Rad = DegreesToRadians(position1.Latitude);
            var lat2Rad = DegreesToRadians(position2.Latitude);

            // Calculate the difference between the two latitudes and longitudes
            var deltaLat = DegreesToRadians(position2.Latitude - position1.Latitude);
            var deltaLng = DegreesToRadians(position2.Longitude - position1.Longitude);

            // Calculate the Haversine formula
            var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) + Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Sin(deltaLng / 2) * Math.Sin(deltaLng / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Return the distance in kilometers
            return earthRadius * c;
        }

        //This code converts degrees to radians
        /// <summary>
        /// Converts a given angle in degrees to radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        private static double DegreesToRadians(double degrees)
        {
            //Multiply degrees by pi and divide by 180 to convert to radians
            return degrees * Math.PI / 180;
        }
    }
}
