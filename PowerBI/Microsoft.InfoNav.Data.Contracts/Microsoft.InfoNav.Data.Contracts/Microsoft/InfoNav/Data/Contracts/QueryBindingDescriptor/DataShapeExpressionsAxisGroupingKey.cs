using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000D1 RID: 209
	[DataContract]
	public sealed class DataShapeExpressionsAxisGroupingKey
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public ConceptualPropertyReference Source { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000C2E9 File Offset: 0x0000A4E9
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000C2F1 File Offset: 0x0000A4F1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public int? Select { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000C2FA File Offset: 0x0000A4FA
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0000C302 File Offset: 0x0000A502
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string Calc { get; set; }
	}
}
