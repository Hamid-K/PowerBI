using System;
using System.Data;

namespace Microsoft.Mashup.Engine1.Library.Sql
{
	// Token: 0x020003AF RID: 943
	internal interface ISqlBulkCopy : IDisposable
	{
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x0600210C RID: 8460
		// (set) Token: 0x0600210D RID: 8461
		int BulkCopyTimeout { get; set; }

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x0600210E RID: 8462
		// (set) Token: 0x0600210F RID: 8463
		string DestinationTableName { get; set; }

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06002110 RID: 8464
		// (set) Token: 0x06002111 RID: 8465
		int BatchSize { get; set; }

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06002112 RID: 8466
		// (set) Token: 0x06002113 RID: 8467
		int NotifyAfter { get; set; }

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06002114 RID: 8468
		// (set) Token: 0x06002115 RID: 8469
		Action<long> RowsCopied { get; set; }

		// Token: 0x06002116 RID: 8470
		void AddColumnMapping(string key, string value);

		// Token: 0x06002117 RID: 8471
		void WriteToServer(IDataReader reader);
	}
}
