using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000057 RID: 87
	[DataContract]
	public sealed class DbLoadMetrics
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000FF71 File Offset: 0x0000E171
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000FF79 File Offset: 0x0000E179
		[DataMember]
		public string DdName { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000FF82 File Offset: 0x0000E182
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000FF8A File Offset: 0x0000E18A
		[DataMember]
		public int CpuUsage { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000FF93 File Offset: 0x0000E193
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000FF9B File Offset: 0x0000E19B
		[DataMember]
		public int MemoryUsageInMBs { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000FFAC File Offset: 0x0000E1AC
		[DataMember]
		public DateTime Timestamp { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000FFB5 File Offset: 0x0000E1B5
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000FFBD File Offset: 0x0000E1BD
		[DataMember]
		public DatabaseType DatabaseType { get; set; }

		// Token: 0x06000471 RID: 1137 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "DBName = {0} MemoryUsage {1} Timestamp {2} Database Type {3}", new object[] { this.DdName, this.MemoryUsageInMBs, this.Timestamp, this.DatabaseType });
		}
	}
}
