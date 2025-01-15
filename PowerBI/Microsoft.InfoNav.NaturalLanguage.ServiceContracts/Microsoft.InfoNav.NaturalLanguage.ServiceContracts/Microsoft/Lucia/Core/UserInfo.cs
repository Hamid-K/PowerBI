using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000140 RID: 320
	[DataContract(Name = "UserInfo", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class UserInfo
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0000B426 File Offset: 0x00009626
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0000B42E File Offset: 0x0000962E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string DisplayName { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0000B437 File Offset: 0x00009637
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0000B43F File Offset: 0x0000963F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string EmailAddress { get; set; }
	}
}
