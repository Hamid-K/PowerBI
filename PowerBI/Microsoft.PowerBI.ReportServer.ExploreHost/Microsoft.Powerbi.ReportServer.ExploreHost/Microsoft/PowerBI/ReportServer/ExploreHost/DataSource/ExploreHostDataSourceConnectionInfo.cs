using System;
using Microsoft.PowerBI.ReportingServicesHost;

namespace Microsoft.PowerBI.ReportServer.ExploreHost.DataSource
{
	// Token: 0x02000022 RID: 34
	internal sealed class ExploreHostDataSourceConnectionInfo
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003FE0 File Offset: 0x000021E0
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003FE8 File Offset: 0x000021E8
		public ExploreHostDataSourceInfo DataSourceInfo { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003FF1 File Offset: 0x000021F1
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00003FF9 File Offset: 0x000021F9
		public RSDataSourceConnection RsDataSourceConnection { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004002 File Offset: 0x00002202
		// (set) Token: 0x060000ED RID: 237 RVA: 0x0000400A File Offset: 0x0000220A
		public IASConnectionInfo ASConnectionInfo { get; private set; }

		// Token: 0x060000EE RID: 238 RVA: 0x00004013 File Offset: 0x00002213
		public ExploreHostDataSourceConnectionInfo(ExploreHostDataSourceInfo dataSourceInfo, RSDataSourceConnection rsDataSourceConnection, IASConnectionInfo asConnectionInfo)
		{
			this.DataSourceInfo = dataSourceInfo;
			this.RsDataSourceConnection = rsDataSourceConnection;
			this.ASConnectionInfo = asConnectionInfo;
		}
	}
}
