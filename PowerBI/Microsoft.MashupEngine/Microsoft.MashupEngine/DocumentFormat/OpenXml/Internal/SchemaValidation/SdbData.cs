using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x020030DB RID: 12507
	internal abstract class SdbData
	{
		// Token: 0x17009876 RID: 39030
		// (get) Token: 0x0601B2A7 RID: 111271
		public abstract int DataSize { get; }

		// Token: 0x0601B2A8 RID: 111272
		public abstract byte[] GetBytes();

		// Token: 0x0601B2A9 RID: 111273
		public abstract void LoadFromBytes(byte[] value, int startIndex);

		// Token: 0x0601B2AA RID: 111274 RVA: 0x0036F900 File Offset: 0x0036DB00
		protected byte[] GetBytes(params byte[][] fieldvalues)
		{
			byte[] array = new byte[this.DataSize];
			int num = 0;
			foreach (byte[] array2 in fieldvalues)
			{
				array2.CopyTo(array, num);
				num += array2.Length;
			}
			return array;
		}

		// Token: 0x0601B2AB RID: 111275 RVA: 0x0036F944 File Offset: 0x0036DB44
		public static int LoadInt(byte[] bytes, ref int startIndex)
		{
			int num = BitConverter.ToInt32(bytes, startIndex);
			startIndex += 4;
			return num;
		}

		// Token: 0x0601B2AC RID: 111276 RVA: 0x0036F964 File Offset: 0x0036DB64
		public static ushort LoadSdbIndex(byte[] bytes, ref int startIndex)
		{
			ushort num = BitConverter.ToUInt16(bytes, startIndex);
			startIndex += 2;
			return num;
		}

		// Token: 0x0601B2AD RID: 111277 RVA: 0x0036F984 File Offset: 0x0036DB84
		public static ushort LoadUInt16(byte[] bytes, ref int startIndex)
		{
			ushort num = BitConverter.ToUInt16(bytes, startIndex);
			startIndex += 2;
			return num;
		}

		// Token: 0x0601B2AE RID: 111278 RVA: 0x0036F9A1 File Offset: 0x0036DBA1
		public static byte LoadByte(byte[] bytes, ref int startIndex)
		{
			startIndex++;
			return bytes[startIndex - 1];
		}

		// Token: 0x0400B3FB RID: 46075
		public const ushort InvalidId = 65535;

		// Token: 0x0400B3FC RID: 46076
		public const int MaxSdbIndex = 65535;
	}
}
