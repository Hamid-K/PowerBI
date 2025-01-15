using System;
using System.Data;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200080D RID: 2061
	public interface IColumnSchema
	{
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x0600410E RID: 16654
		bool AllowDBNull { get; }

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x0600410F RID: 16655
		// (set) Token: 0x06004110 RID: 16656
		string ColumnName { get; set; }

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06004111 RID: 16657
		SqlDbType SqlDbType { get; }

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06004112 RID: 16658
		bool IsAutoIncrement { get; }

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06004113 RID: 16659
		string SchemaName { get; }

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06004114 RID: 16660
		// (set) Token: 0x06004115 RID: 16661
		int SqlLength { get; set; }

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06004116 RID: 16662
		// (set) Token: 0x06004117 RID: 16663
		int SqlPrecision { get; set; }

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06004118 RID: 16664
		// (set) Token: 0x06004119 RID: 16665
		int SqlScale { get; set; }

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x0600411A RID: 16666
		string TableName { get; }

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x0600411B RID: 16667
		// (set) Token: 0x0600411C RID: 16668
		bool IsCharDatetime { get; set; }

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x0600411D RID: 16669
		// (set) Token: 0x0600411E RID: 16670
		bool IsCharDatetimeSet { get; set; }

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x0600411F RID: 16671
		// (set) Token: 0x06004120 RID: 16672
		int DrdaType { get; set; }
	}
}
