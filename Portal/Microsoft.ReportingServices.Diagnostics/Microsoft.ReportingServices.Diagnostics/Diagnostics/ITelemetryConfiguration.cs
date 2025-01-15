using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000055 RID: 85
	internal interface ITelemetryConfiguration
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060002AF RID: 687
		bool IsEnabled { get; }

		// Token: 0x060002B0 RID: 688
		bool IsInternal();

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060002B1 RID: 689
		bool IsPublicBuild { get; }

		// Token: 0x060002B2 RID: 690
		string GetSHA256Hash(string input);
	}
}
