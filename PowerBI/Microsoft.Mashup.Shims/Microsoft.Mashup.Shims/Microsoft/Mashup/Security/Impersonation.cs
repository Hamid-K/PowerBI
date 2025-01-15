using System;
using System.Security.Principal;

namespace Microsoft.Mashup.Security
{
	// Token: 0x0200000E RID: 14
	public static class Impersonation
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000222C File Offset: 0x0000042C
		public static void RunAsProcessUser(Action action)
		{
			using (WindowsIdentity.Impersonate(IntPtr.Zero))
			{
				action();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002268 File Offset: 0x00000468
		public static T RunAsProcessUser<T>(Func<T> func)
		{
			T t;
			using (WindowsIdentity.Impersonate(IntPtr.Zero))
			{
				t = func();
			}
			return t;
		}
	}
}
