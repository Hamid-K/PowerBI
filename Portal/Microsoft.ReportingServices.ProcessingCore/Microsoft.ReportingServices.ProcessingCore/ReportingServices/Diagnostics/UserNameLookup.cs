using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x020005BE RID: 1470
	public static class UserNameLookup
	{
		// Token: 0x06005337 RID: 21303 RVA: 0x0015E47C File Offset: 0x0015C67C
		public static string LookupUsername(ExtendedNameFormat usernameformat)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			int capacity = stringBuilder.Capacity;
			if (UserNameLookup.GetUserNameEx(usernameformat, stringBuilder, ref capacity) != 0)
			{
				return stringBuilder.ToString();
			}
			throw new Exception(string.Format("Cannot resolve name for user {0} in format {1}", WindowsIdentity.GetCurrent().Name, Enum.GetName(typeof(ExtendedNameFormat), usernameformat)));
		}

		// Token: 0x06005338 RID: 21304
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int GetUserNameEx(ExtendedNameFormat nameFormat, StringBuilder userName, ref int userNameSize);
	}
}
