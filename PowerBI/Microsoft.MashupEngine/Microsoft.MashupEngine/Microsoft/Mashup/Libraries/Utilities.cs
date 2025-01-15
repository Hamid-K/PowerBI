using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Mashup.Security;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020D8 RID: 8408
	internal static class Utilities
	{
		// Token: 0x0600CE11 RID: 52753 RVA: 0x0028FCCC File Offset: 0x0028DECC
		public static string CreateHash(params string[] parts)
		{
			string text2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					foreach (string text in parts)
					{
						binaryWriter.Write(text);
					}
					memoryStream.Position = 0L;
					text2 = Utilities.CreateHash(memoryStream);
				}
			}
			return text2;
		}

		// Token: 0x0600CE12 RID: 52754 RVA: 0x0028FD48 File Offset: 0x0028DF48
		public static string CreateHash(byte[] source)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(source))
			{
				text = Utilities.CreateHash(memoryStream);
			}
			return text;
		}

		// Token: 0x0600CE13 RID: 52755 RVA: 0x0028FD80 File Offset: 0x0028DF80
		public static string CreateHash(Stream stream)
		{
			string text;
			using (HashAlgorithm hashAlgorithm = SHA256CryptoProvider.Create())
			{
				text = Convert.ToBase64String(hashAlgorithm.ComputeHash(stream)).Replace('/', '$');
			}
			return text;
		}

		// Token: 0x0600CE14 RID: 52756 RVA: 0x0028FDC8 File Offset: 0x0028DFC8
		public static bool IsSafeException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is AccessViolationException) && !(e is SEHException) && !typeof(SecurityException).IsAssignableFrom(e.GetType());
		}
	}
}
