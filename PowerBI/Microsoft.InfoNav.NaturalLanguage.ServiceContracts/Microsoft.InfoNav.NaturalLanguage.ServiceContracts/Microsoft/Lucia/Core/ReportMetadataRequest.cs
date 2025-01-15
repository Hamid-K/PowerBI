using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000C4 RID: 196
	[DataContract(Name = "ReportMetadataRequest", Namespace = "http://schemas.microsoft.com/sqlbi/2015/09/ReportMetadataService")]
	public sealed class ReportMetadataRequest
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000710C File Offset: 0x0000530C
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x00007114 File Offset: 0x00005314
		[DataMember(IsRequired = true, Order = 10)]
		public string ReportId { get; set; }
	}
}
