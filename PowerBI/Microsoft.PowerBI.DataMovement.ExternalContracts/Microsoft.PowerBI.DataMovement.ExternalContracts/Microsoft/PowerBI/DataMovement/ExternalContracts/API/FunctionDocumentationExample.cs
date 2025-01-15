using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200004B RID: 75
	[DataContract]
	public class FunctionDocumentationExample
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00003D64 File Offset: 0x00001F64
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00003D6C File Offset: 0x00001F6C
		[DataMember(Name = "Description", Order = 0)]
		public string Description { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00003D75 File Offset: 0x00001F75
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00003D7D File Offset: 0x00001F7D
		[DataMember(Name = "Code", Order = 10)]
		public string Code { get; set; }
	}
}
