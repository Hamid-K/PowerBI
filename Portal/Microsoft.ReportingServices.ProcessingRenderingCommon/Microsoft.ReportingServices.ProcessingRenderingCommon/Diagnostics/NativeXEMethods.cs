using System;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000023 RID: 35
	internal class NativeXEMethods
	{
		// Token: 0x06000115 RID: 277
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode)]
		public static extern void AlterXESessions();

		// Token: 0x04000096 RID: 150
		private const string ReportingServiceExe = "ReportingServicesService.exe";
	}
}
