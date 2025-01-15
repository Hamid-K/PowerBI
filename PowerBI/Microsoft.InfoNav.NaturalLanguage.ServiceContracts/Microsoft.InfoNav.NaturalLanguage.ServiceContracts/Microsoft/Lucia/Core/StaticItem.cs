using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000D4 RID: 212
	[DataContract(Name = "StaticItem", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class StaticItem : PhrasingTemplateItem
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000082C1 File Offset: 0x000064C1
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x000082C9 File Offset: 0x000064C9
		[DataMember(IsRequired = true, Order = 1)]
		public string DisplayText { get; set; }
	}
}
