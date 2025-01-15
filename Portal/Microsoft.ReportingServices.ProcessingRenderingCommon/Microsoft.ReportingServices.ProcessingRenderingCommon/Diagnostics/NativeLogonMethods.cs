using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000025 RID: 37
	internal class NativeLogonMethods
	{
		// Token: 0x06000119 RID: 281
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out NativeLogonMethods.SafeUserToken hToken);

		// Token: 0x020000DF RID: 223
		internal sealed class SafeUserToken : SafeHandle
		{
			// Token: 0x0600079E RID: 1950 RVA: 0x00014474 File Offset: 0x00012674
			public SafeUserToken()
				: base(IntPtr.Zero, true)
			{
			}

			// Token: 0x170002CB RID: 715
			// (get) Token: 0x0600079F RID: 1951 RVA: 0x00014482 File Offset: 0x00012682
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x00014494 File Offset: 0x00012694
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			protected override bool ReleaseHandle()
			{
				return NativeLogonMethods.SafeUserToken.CloseHandle(this.handle);
			}

			// Token: 0x060007A1 RID: 1953
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			private static extern bool CloseHandle(IntPtr hToken);
		}
	}
}
