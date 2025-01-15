using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000120 RID: 288
	public class RSAEncryption : IObfuscation, IDisposable
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x0001AC50 File Offset: 0x00018E50
		public RSAEncryption(RSACryptoServiceProvider rsaProvider, int keyIdentifier)
		{
			if (rsaProvider == null)
			{
				throw new ArgumentNullException("rsaProvider");
			}
			this.rsaProvider = rsaProvider;
			this.keyIdentifier = keyIdentifier;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001AC74 File Offset: 0x00018E74
		public int GetKeyIdentifier()
		{
			return this.keyIdentifier;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001AC7C File Offset: 0x00018E7C
		public string ComputePiiReplacement(byte[] piiData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.rsaProvider.KeySize / 8 - 42;
			if (num <= 0)
			{
				throw new InvalidOperationException("Invalid block length per encryption: " + num);
			}
			int num2 = -1;
			bool flag = false;
			while (!flag)
			{
				num2++;
				bool flag2 = num2 == 0;
				int num3 = num2 * num;
				int num4 = num;
				if (num3 + num4 >= piiData.Length)
				{
					num4 = piiData.Length - num3;
					flag = true;
				}
				byte[] array;
				if (flag2 && flag)
				{
					array = piiData;
				}
				else
				{
					array = new byte[num4];
					Buffer.BlockCopy(piiData, num3, array, 0, num4);
				}
				byte[] array2 = this.rsaProvider.Encrypt(array, true);
				if (!flag2)
				{
					stringBuilder.Append(';');
				}
				stringBuilder.Append(ObfuscationUtility.SerializeByteArray(array2));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001AD3E File Offset: 0x00018F3E
		public string DecryptObfuscatedData(string obfuscatedData)
		{
			return ObfuscationUtility.GetStringFromBytes(this.DecryptObfuscatedDataAsByteArray(obfuscatedData));
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001AD4C File Offset: 0x00018F4C
		public byte[] DecryptObfuscatedDataAsByteArray(string obfuscatedData)
		{
			List<byte> list = new List<byte>();
			string[] array = obfuscatedData.Split(new char[] { ';' });
			for (int i = 0; i < array.Length; i++)
			{
				byte[] array2 = ObfuscationUtility.DeserializeByteArray(array[i]);
				byte[] array3 = this.rsaProvider.Decrypt(array2, true);
				list.AddRange(array3);
			}
			return list.ToArray();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001A384 File Offset: 0x00018584
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x040002C0 RID: 704
		private const bool UseOaepPadding = true;

		// Token: 0x040002C1 RID: 705
		private const int MaximumBlockIterations = 8;

		// Token: 0x040002C2 RID: 706
		private const char BlockSeparator = ';';

		// Token: 0x040002C3 RID: 707
		private readonly RSACryptoServiceProvider rsaProvider;

		// Token: 0x040002C4 RID: 708
		private readonly int keyIdentifier;
	}
}
