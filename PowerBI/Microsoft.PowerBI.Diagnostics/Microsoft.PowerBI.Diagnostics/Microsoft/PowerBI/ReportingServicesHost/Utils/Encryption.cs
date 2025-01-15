using System;
using System.Security;

namespace Microsoft.PowerBI.ReportingServicesHost.Utils
{
	// Token: 0x0200003A RID: 58
	public static class Encryption
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003B70 File Offset: 0x00001D70
		public static SecureString ConvertToSecureString(string data)
		{
			SecureString secureString = new SecureString();
			if (data != null)
			{
				foreach (char c in data)
				{
					secureString.AppendChar(c);
				}
				return secureString;
			}
			return null;
		}
	}
}
