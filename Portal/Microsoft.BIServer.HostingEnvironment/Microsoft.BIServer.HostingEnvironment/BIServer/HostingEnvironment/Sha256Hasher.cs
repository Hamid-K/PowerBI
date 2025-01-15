using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200001A RID: 26
	public static class Sha256Hasher
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00003CEC File Offset: 0x00001EEC
		public static string GetSHA256Hash(string input)
		{
			if (input == null)
			{
				input = string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				foreach (byte b in sha256Cng.ComputeHash(Encoding.UTF8.GetBytes(input)))
				{
					stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
				}
			}
			return stringBuilder.ToString();
		}
	}
}
