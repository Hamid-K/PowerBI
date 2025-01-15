using System;
using System.Text;

namespace Microsoft.Mashup.DeltaLake
{
	// Token: 0x02001F08 RID: 7944
	internal static class Base85
	{
		// Token: 0x06010BA1 RID: 68513 RVA: 0x0039A7AB File Offset: 0x003989AB
		public static string EncodeDelta(Guid guid)
		{
			byte[] array = guid.ToByteArray();
			Array.Reverse(array, 0, 4);
			Array.Reverse(array, 4, 2);
			Array.Reverse(array, 6, 2);
			return Base85.Encode(array);
		}

		// Token: 0x06010BA2 RID: 68514 RVA: 0x0039A7D4 File Offset: 0x003989D4
		public static string Encode(byte[] bytes)
		{
			byte[] array = new byte[bytes.Length / 4 * 5];
			int num = 0;
			for (int i = 0; i < bytes.Length; i += 4)
			{
				long num2 = (long)(((int)bytes[i] << 24) | ((int)bytes[i + 1] << 16) | ((int)bytes[i + 2] << 8) | (int)bytes[i + 3]) & (long)((ulong)(-1));
				array[num] = Base85.encodeMap[(int)(num2 / 52200625L)];
				num2 %= 52200625L;
				array[num + 1] = Base85.encodeMap[(int)(num2 / 614125L)];
				num2 %= 614125L;
				array[num + 2] = Base85.encodeMap[(int)(num2 / 7225L)];
				num2 %= 7225L;
				array[num + 3] = Base85.encodeMap[(int)(num2 / 85L)];
				array[num + 4] = Base85.encodeMap[(int)(num2 % 85L)];
				num += 5;
			}
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x06010BA3 RID: 68515 RVA: 0x0039A8AC File Offset: 0x00398AAC
		public static Guid DecodeDelta(string encodedString)
		{
			byte[] array = Base85.Decode(encodedString);
			Array.Reverse(array, 0, 4);
			Array.Reverse(array, 4, 2);
			Array.Reverse(array, 6, 2);
			return new Guid(array);
		}

		// Token: 0x06010BA4 RID: 68516 RVA: 0x0039A8D4 File Offset: 0x00398AD4
		public static byte[] Decode(string encodedString)
		{
			char[] array = encodedString.ToCharArray();
			byte[] array2 = new byte[encodedString.Length / 5 * 4];
			byte[] array3 = new byte[128];
			for (int i = 0; i < Base85.encodeMap.Length; i++)
			{
				array3[(int)Base85.encodeMap[i]] = (byte)i;
			}
			int num = 0;
			while (array2.Length - num / 5 * 4 >= 4)
			{
				long num2 = 0L;
				long num3 = 1L;
				for (int j = 0; j < 5; j++)
				{
					num2 += (long)((ulong)array3[(int)(array[num + j] & '\u007f')] * 52200625UL / (ulong)num3);
					num3 *= 85L;
				}
				byte[] bytes = BitConverter.GetBytes((int)num2);
				Array.Reverse(bytes);
				bytes.CopyTo(array2, num / 5 * 4);
				num += 5;
			}
			return array2;
		}

		// Token: 0x0400643F RID: 25663
		private const long Base = 85L;

		// Token: 0x04006440 RID: 25664
		private const long Base2ndPower = 7225L;

		// Token: 0x04006441 RID: 25665
		private const long Base3rdPower = 614125L;

		// Token: 0x04006442 RID: 25666
		private const long Base4thPower = 52200625L;

		// Token: 0x04006443 RID: 25667
		private static readonly byte[] encodeMap = Encoding.ASCII.GetBytes("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?&<>()[]{}@%$#");
	}
}
