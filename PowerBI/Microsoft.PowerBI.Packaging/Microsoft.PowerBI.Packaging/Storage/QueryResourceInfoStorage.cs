using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200004D RID: 77
	[DataContract]
	public class QueryResourceInfoStorage
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000756A File Offset: 0x0000576A
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00007572 File Offset: 0x00005772
		[DataMember(IsRequired = true)]
		public string SourceType { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000757B File Offset: 0x0000577B
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00007583 File Offset: 0x00005783
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string SourceTypeDescription { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000758C File Offset: 0x0000578C
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00007594 File Offset: 0x00005794
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string SourceName { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000759D File Offset: 0x0000579D
		// (set) Token: 0x06000236 RID: 566 RVA: 0x000075A5 File Offset: 0x000057A5
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public bool? IsDirectQuery { get; set; }
	}
}
