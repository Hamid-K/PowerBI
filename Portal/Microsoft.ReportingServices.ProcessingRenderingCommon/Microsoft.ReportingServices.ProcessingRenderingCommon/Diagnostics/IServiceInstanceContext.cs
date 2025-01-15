using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000069 RID: 105
	public interface IServiceInstanceContext
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000305 RID: 773
		string CatalogConnectionString { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000306 RID: 774
		Guid ServiceInstanceId { get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000307 RID: 775
		RunningApplication RunningApplication { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000308 RID: 776
		string TempFilesDirectoryPath { get; }
	}
}
