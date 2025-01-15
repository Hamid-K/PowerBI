using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000D6 RID: 214
	[DataContract]
	public sealed class ExtensionEntityBinding
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000C454 File Offset: 0x0000A654
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0000C45C File Offset: 0x0000A65C
		[DataMember(IsRequired = true, Order = 1)]
		public string Name { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000C465 File Offset: 0x0000A665
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0000C46D File Offset: 0x0000A66D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string NativeQueryName { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000C476 File Offset: 0x0000A676
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0000C47E File Offset: 0x0000A67E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public IList<ExtensionMeasureBinding> Measures { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000C487 File Offset: 0x0000A687
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0000C48F File Offset: 0x0000A68F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public IList<ExtensionColumnBinding> Columns { get; set; }
	}
}
