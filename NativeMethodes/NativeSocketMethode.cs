namespace NativeMethodes
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class NativeSocketMethode
    {
        [DllImport("Ws2_32.dll")]
        public static extern int connect(IntPtr socketHandle, ref sockaddr Address, ref int Addresslen);
        [DllImport("Ws2_32.dll")]
        public static extern int getpeername(IntPtr s, ref sockaddr Address, ref int namelen);
        [DllImport("ws2_32.dll")]
        public static extern IntPtr inet_ntoa(in_addr a);
        [DllImport("ws2_32.dll")]
        public static extern ushort ntohs(ushort netshort);
        [DllImport("Ws2_32.dll")]
        public static extern int recv(IntPtr socketHandle, IntPtr buf, int Buffercount, int socketFlags);
        [DllImport("Ws2_32.dll")]
        public static extern int send(IntPtr socketHandle, IntPtr buf, int count, int socketFlags);

        public enum AddressFamily
        {
            AppleTalk = 0x11,
            BlueTooth = 0x20,
            InterNetworkv4 = 2,
            InterNetworkv6 = 0x17,
            Ipx = 4,
            Irda = 0x1a,
            NetBios = 0x11,
            Unknown = 0
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet=CharSet.Unicode, SetLastError=true)]
        public delegate int DConnect(IntPtr socketHandle, ref NativeSocketMethode.sockaddr Address, ref int Addresslen);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet=CharSet.Unicode, SetLastError=true)]
        public delegate int Drecv(IntPtr socketHandle, IntPtr buf, int Buffercount, int socketFlags);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet=CharSet.Unicode, SetLastError=true)]
        public delegate int Dsend(IntPtr socketHandle, IntPtr buf, int count, int socketFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct in_addr
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
            public byte[] sin_addr;
        }

        public enum ProtocolType
        {
            BlueTooth = 3,
            ReliableMulticast = 0x71,
            Tcp = 6,
            Udp = 0x11
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct sockaddr
        {
            public short sin_family;
            public ushort sin_port;
            public NativeSocketMethode.in_addr sin_addr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
            public byte[] sin_zero;
        }

        public enum SocketType
        {
            Unknown,
            Stream,
            DGram,
            Raw,
            Rdm,
            SeqPacket
        }
    }
}

