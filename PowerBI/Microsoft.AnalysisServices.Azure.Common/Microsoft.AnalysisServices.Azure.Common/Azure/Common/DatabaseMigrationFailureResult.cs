using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200004F RID: 79
	[DataContract]
	public class DatabaseMigrationFailureResult
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public DatabaseMigrationFailureResult()
		{
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000FAF2 File Offset: 0x0000DCF2
		public DatabaseMigrationFailureResult(DatabaseMigrationMoniker databaseMigrationMoniker, DatabaseMigrationFailureType migrationFailureType)
		{
			this.DatabaseMoniker = databaseMigrationMoniker.Moniker;
			this.DatabaseMigrationMoniker = databaseMigrationMoniker;
			this.MigrationFailureType = migrationFailureType;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000FB14 File Offset: 0x0000DD14
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000FB1C File Offset: 0x0000DD1C
		[DataMember]
		public DatabaseMoniker DatabaseMoniker { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000FB25 File Offset: 0x0000DD25
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x0000FB2D File Offset: 0x0000DD2D
		[DataMember]
		public DatabaseMigrationMoniker DatabaseMigrationMoniker { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000FB36 File Offset: 0x0000DD36
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0000FB3E File Offset: 0x0000DD3E
		[DataMember]
		public DatabaseMigrationFailureType MigrationFailureType { get; set; }
	}
}
