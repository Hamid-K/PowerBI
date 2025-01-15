using System;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x0200004D RID: 77
	[NLogConfigurationItem]
	[ThreadAgnostic]
	[ThreadSafe]
	[AppDomainFixedOutput]
	public class NLogViewerParameterInfo
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00012AE5 File Offset: 0x00010CE5
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x00012AED File Offset: 0x00010CED
		[RequiredParameter]
		public string Name { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00012AF6 File Offset: 0x00010CF6
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x00012AFE File Offset: 0x00010CFE
		[RequiredParameter]
		public Layout Layout { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00012B07 File Offset: 0x00010D07
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x00012B0F File Offset: 0x00010D0F
		public bool IncludeEmptyValue { get; set; } = true;
	}
}
