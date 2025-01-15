using System;
using System.Security;

namespace Azure.Identity
{
	// Token: 0x0200007F RID: 127
	internal static class StringExtensions
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public static SecureString ToSecureString(this string plainString)
		{
			if (plainString == null)
			{
				return null;
			}
			SecureString secureString = new SecureString();
			foreach (char c in plainString.ToCharArray())
			{
				secureString.AppendChar(c);
			}
			return secureString;
		}
	}
}
