using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000142 RID: 322
	[DataContract(Name = "Term", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class UtteranceTerm
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0000B48D File Offset: 0x0000968D
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0000B495 File Offset: 0x00009695
		[DataMember(IsRequired = true, Order = 1)]
		public int StartCharIndex { get; set; }
	}
}
