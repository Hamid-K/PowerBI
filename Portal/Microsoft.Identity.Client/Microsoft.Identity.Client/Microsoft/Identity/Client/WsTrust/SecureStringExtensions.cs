using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Identity.Client.WsTrust
{
	// Token: 0x020001BA RID: 442
	internal static class SecureStringExtensions
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x00043004 File Offset: 0x00041204
		public static char[] PasswordToCharArray(this SecureString secureString)
		{
			if (secureString == null)
			{
				return null;
			}
			char[] array = new char[secureString.Length];
			IntPtr intPtr = Marshal.SecureStringToCoTaskMemUnicode(secureString);
			for (int i = 0; i < secureString.Length; i++)
			{
				array[i] = (char)Marshal.ReadInt16(intPtr, i * 2);
			}
			Marshal.ZeroFreeCoTaskMemUnicode(intPtr);
			return array;
		}
	}
}
