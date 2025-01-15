using System;
using System.Collections.Generic;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x02000469 RID: 1129
	[Obsolete("This API is deprecated.")]
	public class PerformanceCountersBufferConfiguration : DiagnosticDataBufferConfiguration
	{
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002752 RID: 10066 RVA: 0x00077A80 File Offset: 0x00075C80
		public IList<PerformanceCounterConfiguration> DataSources
		{
			get
			{
				return new List<PerformanceCounterConfiguration>();
			}
		}
	}
}
