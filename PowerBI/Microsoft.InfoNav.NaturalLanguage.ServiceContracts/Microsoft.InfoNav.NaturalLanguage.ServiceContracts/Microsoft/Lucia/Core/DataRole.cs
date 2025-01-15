using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000149 RID: 329
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataRole
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0000B58B File Offset: 0x0000978B
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0000B593 File Offset: 0x00009793
		[DataMember(IsRequired = true, Order = 10)]
		public string Name { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0000B59C File Offset: 0x0000979C
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0000B5A4 File Offset: 0x000097A4
		[DataMember(IsRequired = true, Order = 20)]
		public int Projection { get; set; }
	}
}
