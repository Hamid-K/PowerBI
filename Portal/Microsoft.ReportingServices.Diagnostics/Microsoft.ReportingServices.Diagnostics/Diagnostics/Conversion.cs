using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000079 RID: 121
	internal static class Conversion
	{
		// Token: 0x060003DC RID: 988 RVA: 0x0001030E File Offset: 0x0000E50E
		public static byte UInt32ToLowestByte(uint toConvert)
		{
			return (byte)(toConvert & 255U);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00010318 File Offset: 0x0000E518
		public static byte UInt32ToLowerByte(uint toConvert)
		{
			return (byte)((toConvert & 65280U) >> 8);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00010324 File Offset: 0x0000E524
		public static byte UInt32ToHigherByte(uint toConvert)
		{
			return (byte)((toConvert & 16711680U) >> 16);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00010331 File Offset: 0x0000E531
		public static byte UInt32ToHighestByte(uint toConvert)
		{
			return (byte)((toConvert & 4278190080U) >> 24);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00010340 File Offset: 0x0000E540
		public static ushort BytesToUInt16(byte low, byte high)
		{
			ushort num = (ushort)(high << 8);
			return (ushort)(low & byte.MaxValue) | (num & 65280);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00010368 File Offset: 0x0000E568
		public static uint BytesToUInt32(byte lowest, byte lower, byte higher, byte highest)
		{
			uint num = (uint)((uint)highest << 24);
			uint num2 = (uint)((uint)higher << 16);
			uint num3 = (uint)((uint)lower << 8);
			return (uint)(lowest & byte.MaxValue) | (num3 & 65280U) | (num2 & 16711680U) | (num & 4278190080U);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000103A8 File Offset: 0x0000E5A8
		public static uint ReadInt32(Stream store)
		{
			byte[] array = new byte[4];
			if (store.Read(array, 0, 4) != 4)
			{
				throw new InternalCatalogException("Could not read 4 bytes buffer");
			}
			return Conversion.BytesToUInt32(array[0], array[1], array[2], array[3]);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000103E4 File Offset: 0x0000E5E4
		public static void WriteUInt32(Stream store, uint dataValue)
		{
			store.Write(new byte[]
			{
				Conversion.UInt32ToLowestByte(dataValue),
				Conversion.UInt32ToLowerByte(dataValue),
				Conversion.UInt32ToHigherByte(dataValue),
				Conversion.UInt32ToHighestByte(dataValue)
			}, 0, 4);
		}
	}
}
