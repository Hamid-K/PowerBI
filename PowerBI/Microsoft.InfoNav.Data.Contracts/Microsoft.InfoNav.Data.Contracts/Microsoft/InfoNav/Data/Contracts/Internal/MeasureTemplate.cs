using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001F3 RID: 499
	[DataContract(Name = "measureTemplate", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class MeasureTemplate
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0001A736 File Offset: 0x00018936
		// (set) Token: 0x06000D90 RID: 3472 RVA: 0x0001A73E File Offset: 0x0001893E
		[DataMember(Name = "version", IsRequired = true, Order = 0)]
		public int Version { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0001A747 File Offset: 0x00018947
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x0001A74F File Offset: 0x0001894F
		[DataMember(Name = "daxTemplateName", IsRequired = true, Order = 1)]
		public string DaxTemplateName { get; set; }
	}
}
