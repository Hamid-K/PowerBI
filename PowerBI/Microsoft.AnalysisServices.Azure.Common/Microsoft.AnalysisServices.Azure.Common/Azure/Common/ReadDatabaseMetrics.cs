using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000064 RID: 100
	[DataContract]
	public sealed class ReadDatabaseMetrics
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x00010443 File Offset: 0x0000E643
		public ReadDatabaseMetrics(string databaseName, double popularityIndex, DateTime updateTime)
		{
			this.DatabaseName = databaseName;
			this.PopularityIndex = popularityIndex;
			this.UpdateTime = updateTime;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00010460 File Offset: 0x0000E660
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00010468 File Offset: 0x0000E668
		[DataMember]
		public string DatabaseName { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00010471 File Offset: 0x0000E671
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x00010479 File Offset: 0x0000E679
		[DataMember]
		public double PopularityIndex { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00010482 File Offset: 0x0000E682
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0001048A File Offset: 0x0000E68A
		[DataMember]
		public DateTime UpdateTime { get; set; }
	}
}
