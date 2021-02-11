using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Minecraft.Server {
    public static class DataTypes {
#region VarInts
        public static int readVarInt(NetworkStream stream) {
            int numRead = 0;
            int result = 0;
            byte read;

            do {
                read = (byte) stream.ReadByte();
                int value = (read & 0b01111111);

                result |= (value << (7 * numRead));
                numRead++;
                if (numRead > 5) {
                    throw new Exception("VarInt is too big");
                }
            } while ((read & 0b10000000) != 0);

            return result;
        }

        public static long readVarLong(NetworkStream stream) {
            int numRead = 0;
            long result = 0;
            byte read;

            do {
                read = (byte)stream.ReadByte();
                int value = (read & 0b01111111);

                result |= (value << (7 * numRead));
                numRead++;
                if (numRead > 10) {
                    throw new Exception("VarLong is too big");
                }
            } while ((read & 0b10000000) != 0);

            return result;
        }

        public static void writeVarInt(NetworkStream stream, int value) {
            do {
                byte temp = (byte) (value & 0b01111111);
                value = (int)(((uint) value) >> 7);
                if (value != 0) {
                    temp |= 0b10000000;
                }
                stream.WriteByte(temp);
            } while (value != 0);
        }
        
        public static void writeVarLong(NetworkStream stream, long value) {
            do {
                byte temp = (byte) (value & 0b01111111);
                value = (long)(((ulong) value) >> 7);
                if (value != 0) {
                    temp |= 0b10000000;
                }
                stream.WriteByte(temp);
            } while (value != 0);
        }
#endregion
    }
}
