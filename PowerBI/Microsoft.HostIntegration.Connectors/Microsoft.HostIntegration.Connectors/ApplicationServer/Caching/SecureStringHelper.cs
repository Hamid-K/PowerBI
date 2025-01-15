using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000381 RID: 897
	internal static class SecureStringHelper
	{
		// Token: 0x06001F9D RID: 8093 RVA: 0x00060574 File Offset: 0x0005E774
		public static string GetUnSecureString(SecureString secureString)
		{
			if (secureString == null)
			{
				throw new ArgumentNullException("secureString");
			}
			IntPtr intPtr = IntPtr.Zero;
			string text;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
				text = Marshal.PtrToStringUni(intPtr);
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
			}
			return text;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000605C0 File Offset: 0x0005E7C0
		public static SecureString GetSecureString(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			SecureString secureString = new SecureString();
			foreach (char c in input)
			{
				secureString.AppendChar(c);
			}
			secureString.MakeReadOnly();
			return secureString;
		}
	}
}
