using System;
using System.Linq;
using System.Text;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000016 RID: 22
	public static class ChecksumUtility
	{
		// Token: 0x06000061 RID: 97 RVA: 0x000032A0 File Offset: 0x000014A0
		public static SkuInfo ConvertRegistryFormatToSkuInfo(string instanceId, byte[] checksumBytes)
		{
			string text = SkuCrypto.Decrypt(ChecksumUtility.DecodeBytesToCharacters(checksumBytes));
			return new SkuInfo(instanceId, text);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032C0 File Offset: 0x000014C0
		public static byte[] ConvertSkuInfoToRegistryFormat(SkuInfo info)
		{
			string text = SkuCrypto.Encrypt(info.GetStringRepresentation());
			return ChecksumUtility.ConvertToRegistryFormat(ChecksumUtility.EncodeBytesAsCharacters(text), text);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032E8 File Offset: 0x000014E8
		private static string EncodeBytesAsCharacters(string encrypted)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in encrypted)
			{
				string text = string.Format("{0:x2}", (int)c);
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003338 File Offset: 0x00001538
		private static string DecodeBytesToCharacters(byte[] checksumBytes)
		{
			string text = ChecksumUtility.ConvertFromRegistryFormat(checksumBytes);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length - 1; i += 2)
			{
				int num = Convert.ToInt32(text.Substring(i, 2), 16);
				stringBuilder.Append((char)num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003384 File Offset: 0x00001584
		private static byte[] ConvertToRegistryFormat(string encoded, string encrypted)
		{
			byte[] array = encoded.Select((char c) => (byte)c).ToArray<byte>();
			byte[] array2 = (from c in string.Format("{0:d2}", encrypted.Length)
				select (byte)c).ToArray<byte>();
			byte[] array3 = new byte[1];
			return array2.Concat(array).Concat(array3).ToArray<byte>();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003414 File Offset: 0x00001614
		private static string ConvertFromRegistryFormat(byte[] checksumBytes)
		{
			byte[] array = checksumBytes.Skip(2).ToArray<byte>();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append((char)b);
			}
			return stringBuilder.ToString();
		}
	}
}
