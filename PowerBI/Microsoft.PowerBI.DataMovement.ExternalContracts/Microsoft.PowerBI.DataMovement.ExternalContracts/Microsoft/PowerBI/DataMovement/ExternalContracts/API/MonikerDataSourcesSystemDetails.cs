using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000062 RID: 98
	[DataContract]
	public class MonikerDataSourcesSystemDetails
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000446F File Offset: 0x0000266F
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00004477 File Offset: 0x00002677
		[DataMember(Name = "dataSourceType", Order = 0)]
		public DatasourceType DataSourceType { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00004480 File Offset: 0x00002680
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00004488 File Offset: 0x00002688
		[DataMember(Name = "connectionDetails", Order = 10)]
		public string ConnectionDetails { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00004491 File Offset: 0x00002691
		// (set) Token: 0x060002CE RID: 718 RVA: 0x00004499 File Offset: 0x00002699
		[DataMember(Name = "dataSourceId", Order = 20)]
		public long DataSourceId { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000044A2 File Offset: 0x000026A2
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x000044AA File Offset: 0x000026AA
		[DataMember(Name = "dataSourceReference", Order = 30)]
		public string DataSourceReference { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x000044B3 File Offset: 0x000026B3
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x000044BB File Offset: 0x000026BB
		[DataMember(Name = "dataSourceObjectId", Order = 40)]
		public Guid DataSourceObjectId { get; set; }
	}
}
