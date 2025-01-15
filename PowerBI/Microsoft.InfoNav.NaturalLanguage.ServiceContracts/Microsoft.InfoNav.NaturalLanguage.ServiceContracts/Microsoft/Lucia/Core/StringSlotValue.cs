using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000DC RID: 220
	[DataContract(Name = "StringSlotValue", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class StringSlotValue : FixedSlotValue
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000083AB File Offset: 0x000065AB
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x000083B3 File Offset: 0x000065B3
		[DataMember(IsRequired = true, Order = 1)]
		public string Value { get; set; }
	}
}
