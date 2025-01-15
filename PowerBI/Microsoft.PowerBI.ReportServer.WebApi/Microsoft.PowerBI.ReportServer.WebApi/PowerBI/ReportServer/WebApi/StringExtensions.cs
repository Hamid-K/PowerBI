using System;
using System.Security;

namespace Microsoft.PowerBI.ReportServer.WebApi
{
	// Token: 0x0200000D RID: 13
	public static class StringExtensions
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002C48 File Offset: 0x00000E48
		public static SecureString ToSecureString(this string source)
		{
			if (string.IsNullOrWhiteSpace(source))
			{
				return null;
			}
			SecureString secureString = new SecureString();
			foreach (char c in source.ToCharArray())
			{
				secureString.AppendChar(c);
			}
			return secureString;
		}
	}
}
