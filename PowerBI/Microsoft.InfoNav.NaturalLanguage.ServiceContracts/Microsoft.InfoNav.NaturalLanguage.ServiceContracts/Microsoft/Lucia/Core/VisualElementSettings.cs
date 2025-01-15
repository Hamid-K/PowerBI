using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200014C RID: 332
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class VisualElementSettings
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000B669 File Offset: 0x00009869
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0000B671 File Offset: 0x00009871
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public bool Totals { get; set; }
	}
}
