using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Mashup.Security;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000028 RID: 40
	public static class SecurityUtils
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000053D4 File Offset: 0x000035D4
		public static byte[] HashContent(Stream contentStream)
		{
			contentStream = contentStream ?? new MemoryStream(new byte[0]);
			byte[] array = new byte[4096];
			int num = 0;
			byte[] hash;
			using (contentStream)
			{
				using (HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider())
				{
					int num2;
					while ((num2 = contentStream.Read(array, 0, array.Length)) > 0)
					{
						num += num2;
						hashAlgorithm.TransformBlock(array, 0, num2, array, 0);
					}
					hashAlgorithm.TransformFinalBlock(array, 0, num2);
					hash = hashAlgorithm.Hash;
				}
			}
			return hash;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005478 File Offset: 0x00003678
		public static byte[] EncryptBytes(byte[] plainContents, string additionalEntropy)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(additionalEntropy);
			return UserProtectedDataServices.Protect(plainContents, bytes);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005498 File Offset: 0x00003698
		public static byte[] DecryptBytes(byte[] permissionBytes, string additionalEntropy)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(additionalEntropy);
			return UserProtectedDataServices.Unprotect(permissionBytes, bytes);
		}
	}
}
