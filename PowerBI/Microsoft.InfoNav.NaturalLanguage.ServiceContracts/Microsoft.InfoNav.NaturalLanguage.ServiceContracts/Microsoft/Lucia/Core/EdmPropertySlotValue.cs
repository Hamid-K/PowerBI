using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000DB RID: 219
	[DataContract(Name = "EdmPropertySlotValue", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class EdmPropertySlotValue : FixedSlotValue
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00008381 File Offset: 0x00006581
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00008389 File Offset: 0x00006589
		[DataMember(IsRequired = true, Order = 1)]
		public string EntitySet { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00008392 File Offset: 0x00006592
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000839A File Offset: 0x0000659A
		[DataMember(IsRequired = true, Order = 2)]
		public string Property { get; set; }
	}
}
