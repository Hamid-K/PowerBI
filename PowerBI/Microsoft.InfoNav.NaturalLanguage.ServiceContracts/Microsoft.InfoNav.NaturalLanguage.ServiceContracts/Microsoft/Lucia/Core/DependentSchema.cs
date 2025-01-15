using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200007D RID: 125
	[DataContract(Name = "DependentSchema", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DependentSchema
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00005394 File Offset: 0x00003594
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000539C File Offset: 0x0000359C
		[DataMember(Name = "i", IsRequired = true, Order = 10)]
		public string Id { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000053A5 File Offset: 0x000035A5
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000053AD File Offset: 0x000035AD
		[DataMember(Name = "t", IsRequired = true, Order = 20)]
		public DateTime LastUpdatedTime { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000053B6 File Offset: 0x000035B6
		// (set) Token: 0x0600023B RID: 571 RVA: 0x000053BE File Offset: 0x000035BE
		[DataMember(Name = "b", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string Binding { get; set; }
	}
}
