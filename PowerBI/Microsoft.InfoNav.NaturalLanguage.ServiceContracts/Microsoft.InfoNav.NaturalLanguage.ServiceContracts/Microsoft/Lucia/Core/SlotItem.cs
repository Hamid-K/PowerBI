using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000D5 RID: 213
	[DataContract(Name = "SlotItem", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class SlotItem : PhrasingTemplateItem
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000082DA File Offset: 0x000064DA
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x000082E2 File Offset: 0x000064E2
		[DataMember(IsRequired = true, Order = 1)]
		public string Name { get; set; }
	}
}
