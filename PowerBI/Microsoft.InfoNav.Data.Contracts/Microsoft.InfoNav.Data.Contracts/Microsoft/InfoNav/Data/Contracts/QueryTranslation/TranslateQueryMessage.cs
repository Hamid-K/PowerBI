using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000B4 RID: 180
	[DataContract(Name = "TranslateQueryMessage")]
	public sealed class TranslateQueryMessage
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000B8A2 File Offset: 0x00009AA2
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000B8AA File Offset: 0x00009AAA
		[DataMember(Name = "Code", IsRequired = true, Order = 0)]
		public string Code { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000B8B3 File Offset: 0x00009AB3
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0000B8BB File Offset: 0x00009ABB
		[DataMember(Name = "Message", IsRequired = true, Order = 10)]
		public string Message { get; set; }
	}
}
