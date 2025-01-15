using System;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000019 RID: 25
	internal static class TimeZoneUtil
	{
		// Token: 0x0600005D RID: 93
		[DllImport("Kernel32.dll")]
		internal static extern void GetTimeZoneInformation(ref TIME_ZONE_INFORMATION_Introp st);
	}
}
