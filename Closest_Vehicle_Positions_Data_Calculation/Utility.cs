using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Closest_Vehicle_Positions_Data_Calculation
{
    public static class Utility
    {
        //This code reads a null-terminated string from a BinaryReader object.
        /// <summary>
        /// Reads a null-terminated string from a BinaryReader
        /// </summary>
        /// <param name="reader">The BinaryReader to read from</param>
        /// <param name="encoding">The encoding to use when reading the string</param>
        /// <returns>The string read from the BinaryReader</returns>
        public static string ReadNullTerminatedString(this BinaryReader reader, Encoding encoding)
        {
            //Create a list of bytes to store the string
            var buffer = new List<byte>();

            //Read a byte from the BinaryReader
            byte b;
            //Loop until the byte is equal to 0 (null-terminated)
            while ((b = reader.ReadByte()) != 0)
            {
                //Add the byte to the list
                buffer.Add(b);
            }

            //Return the string from the list of bytes
            return encoding.GetString(buffer.ToArray());
        }
    }
}
