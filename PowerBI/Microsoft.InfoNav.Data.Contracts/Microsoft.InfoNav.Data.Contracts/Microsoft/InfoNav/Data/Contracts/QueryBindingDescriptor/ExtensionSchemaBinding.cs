using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000D8 RID: 216
	[DataContract]
	public sealed class ExtensionSchemaBinding
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000C4CA File Offset: 0x0000A6CA
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000C4D2 File Offset: 0x0000A6D2
		[DataMember(IsRequired = true, Order = 1)]
		public string Name { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000C4DB File Offset: 0x0000A6DB
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000C4E3 File Offset: 0x0000A6E3
		[DataMember(IsRequired = false, Order = 2)]
		public IList<ExtensionEntityBinding> Entities { get; set; }
	}
}
