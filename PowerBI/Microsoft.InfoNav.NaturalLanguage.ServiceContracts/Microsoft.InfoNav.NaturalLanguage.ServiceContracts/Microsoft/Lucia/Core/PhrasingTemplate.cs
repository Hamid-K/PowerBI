using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000D2 RID: 210
	[DataContract(Name = "PhrasingTemplate", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class PhrasingTemplate
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x00008284 File Offset: 0x00006484
		public PhrasingTemplate()
		{
			this.SlotValues = new Dictionary<string, SlotValue>();
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00008297 File Offset: 0x00006497
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000829F File Offset: 0x0000649F
		[DataMember(IsRequired = true, Order = 1)]
		public PhrasingType PhrasingType { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x000082A8 File Offset: 0x000064A8
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x000082B0 File Offset: 0x000064B0
		[DataMember(IsRequired = true, Order = 2)]
		public IDictionary<string, SlotValue> SlotValues { get; set; }
	}
}
