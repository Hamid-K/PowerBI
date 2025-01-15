using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000185 RID: 389
	[DataContract(Name = "DataQueryRequest", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataQueryRequest
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00014975 File Offset: 0x00012B75
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0001497D File Offset: 0x00012B7D
		[DataMember(Name = "Query", IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public DataQuery Query { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00014986 File Offset: 0x00012B86
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0001498E File Offset: 0x00012B8E
		[DataMember(Name = "CacheKey", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string CacheKey { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00014997 File Offset: 0x00012B97
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x0001499F File Offset: 0x00012B9F
		[DataMember(Name = "CacheOptions", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public CacheOptions? CacheOptions { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x000149A8 File Offset: 0x00012BA8
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x000149B0 File Offset: 0x00012BB0
		[DataMember(Name = "QueryId", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string QueryId { get; set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x000149B9 File Offset: 0x00012BB9
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x000149C1 File Offset: 0x00012BC1
		[DataMember(Name = "RequestPriority", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public RequestPriority? RequestPriority { get; set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x000149CA File Offset: 0x00012BCA
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x000149D2 File Offset: 0x00012BD2
		[DataMember(Name = "ApplicationContext", IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public ApplicationContext ApplicationContext { get; set; }
	}
}
