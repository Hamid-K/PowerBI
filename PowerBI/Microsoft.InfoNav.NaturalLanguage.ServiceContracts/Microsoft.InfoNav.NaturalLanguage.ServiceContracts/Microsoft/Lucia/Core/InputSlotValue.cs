using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000DD RID: 221
	[DataContract(Name = "InputSlotValue", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class InputSlotValue : SlotValue
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000083C4 File Offset: 0x000065C4
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x000083CC File Offset: 0x000065CC
		[DataMember(IsRequired = true, Order = 1)]
		public InputType InputType { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x000083D5 File Offset: 0x000065D5
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x000083DD File Offset: 0x000065DD
		[DataMember(IsRequired = false, Order = 2, EmitDefaultValue = false)]
		public string Value { get; set; }
	}
}
