using System;
using Microsoft.ReportingServices.ProcessingRenderingCommon;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000C1 RID: 193
	public sealed class DataSource
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x00012938 File Offset: 0x00010B38
		internal DataSource()
		{
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00012940 File Offset: 0x00010B40
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00012948 File Offset: 0x00010B48
		[PotentialPiiMaskWhenLogging]
		public string Name { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00012951 File Offset: 0x00010B51
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00012959 File Offset: 0x00010B59
		public string DataSourceReference { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00012962 File Offset: 0x00010B62
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0001296A File Offset: 0x00010B6A
		[PotentialPiiMaskWhenLogging]
		public string ConnectionString { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00012973 File Offset: 0x00010B73
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0001297B File Offset: 0x00010B7B
		public string DataExtension { get; set; }
	}
}
