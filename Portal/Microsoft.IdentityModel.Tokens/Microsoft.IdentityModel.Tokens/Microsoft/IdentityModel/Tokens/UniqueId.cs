using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000191 RID: 401
	public static class UniqueId
	{
		// Token: 0x06001211 RID: 4625 RVA: 0x00043297 File Offset: 0x00041497
		public static string CreateUniqueId()
		{
			return UniqueId.optimizedNcNamePrefix + UniqueId.GetNextId();
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000432A8 File Offset: 0x000414A8
		public static string CreateUniqueId(string prefix)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				throw LogHelper.LogArgumentNullException("prefix");
			}
			return prefix + UniqueId.reusableUuid + "-" + UniqueId.GetNextId();
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000432D2 File Offset: 0x000414D2
		public static string CreateRandomId()
		{
			return "_" + UniqueId.GetRandomUuid();
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x000432E3 File Offset: 0x000414E3
		public static string CreateRandomId(string prefix)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				throw LogHelper.LogArgumentNullException("prefix");
			}
			return prefix + UniqueId.GetRandomUuid();
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00043303 File Offset: 0x00041503
		public static Uri CreateRandomUri()
		{
			return new Uri("urn:uuid:" + UniqueId.GetRandomUuid());
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004331C File Offset: 0x0004151C
		private static string GetNextId()
		{
			string text;
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				byte[] array = new byte[16];
				randomNumberGenerator.GetBytes(array);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", array[i]);
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00043394 File Offset: 0x00041594
		private static string GetRandomUuid()
		{
			return Guid.NewGuid().ToString("D");
		}

		// Token: 0x040006DD RID: 1757
		private const int RandomSaltSize = 16;

		// Token: 0x040006DE RID: 1758
		private const string NcNamePrefix = "_";

		// Token: 0x040006DF RID: 1759
		private const string UuidUriPrefix = "urn:uuid:";

		// Token: 0x040006E0 RID: 1760
		private static readonly string reusableUuid = UniqueId.GetRandomUuid();

		// Token: 0x040006E1 RID: 1761
		private static readonly string optimizedNcNamePrefix = "_" + UniqueId.reusableUuid + "-";
	}
}
