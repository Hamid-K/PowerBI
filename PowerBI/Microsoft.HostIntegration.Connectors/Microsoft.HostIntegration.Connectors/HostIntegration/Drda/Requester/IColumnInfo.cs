using System;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x020008FD RID: 2301
	public interface IColumnInfo
	{
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06004889 RID: 18569
		string ColumnName { get; }

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x0600488A RID: 18570
		DrdaClientType DrdaType { get; }

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x0600488B RID: 18571
		int Length { get; }

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x0600488C RID: 18572
		short Precision { get; }

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x0600488D RID: 18573
		short Scale { get; }

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x0600488E RID: 18574
		bool IsNullable { get; }

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x0600488F RID: 18575
		bool IsLob { get; }

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06004890 RID: 18576
		string BaseTable { get; }

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06004891 RID: 18577
		string Schema { get; }

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06004892 RID: 18578
		string Catalog { get; }

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06004893 RID: 18579
		bool IsKey { get; }

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06004894 RID: 18580
		short GeneratedIdType { get; }
	}
}
