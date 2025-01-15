using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001DC RID: 476
	[DataContract(Name = "binSize", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class BinSize
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0001913A File Offset: 0x0001733A
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00019142 File Offset: 0x00017342
		[DataMember(Name = "value", IsRequired = true, Order = 0)]
		public double Value { get; set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0001914B File Offset: 0x0001734B
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x00019153 File Offset: 0x00017353
		[DataMember(Name = "unit", IsRequired = true, Order = 1)]
		public ConceptualBinUnit Unit { get; set; }
	}
}
