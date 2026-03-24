using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

public static class BinaryReaderExtensions {
    public static uint ReadUInt32BE(this BinaryReader br) {
      Span<byte> bytes = stackalloc byte[4];
      br.Read(bytes);
      return BinaryPrimitives.ReadUInt32BigEndian(bytes);
    }

    public static int ReadInt32BE(this BinaryReader br) {
      Span<byte> bytes = stackalloc byte[4];
      br.Read(bytes);
      return BinaryPrimitives.ReadInt32BigEndian(bytes);
    }

    public static ushort ReadUInt16BE(this BinaryReader br) {
      Span<byte> bytes = stackalloc byte[2];
      br.Read(bytes);
      return BinaryPrimitives.ReadUInt16BigEndian(bytes);
    }

    public static short ReadInt16BE(this BinaryReader br) {
      Span<byte> bytes = stackalloc byte[2];
      br.Read(bytes);
      return BinaryPrimitives.ReadInt16BigEndian(bytes);
    }

    public static float ReadSingleBE(this BinaryReader br) {
      Span<byte> bytes = stackalloc byte[4];
      br.Read(bytes);
      if (BitConverter.IsLittleEndian) {
        bytes.Reverse();
      }
      return BitConverter.ToSingle(bytes);
    }

    public static string ReadNullTerminatedString(this BinaryReader br) {
      StringBuilder sb = new StringBuilder();

      byte b = br.ReadByte();
      while (b != 0) {
        sb.Append((char)b);
        b = br.ReadByte();
      }

      return sb.ToString();
    }
  }