using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000D9 RID: 217
	internal interface IProjectionBinding
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060005A9 RID: 1449
		// (set) Token: 0x060005AA RID: 1450
		string Value { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060005AB RID: 1451
		// (set) Token: 0x060005AC RID: 1452
		DynamicFormatBinding DynamicFormat { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060005AD RID: 1453
		// (set) Token: 0x060005AE RID: 1454
		List<string> Subtotal { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060005AF RID: 1455
		// (set) Token: 0x060005B0 RID: 1456
		List<string> Min { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060005B1 RID: 1457
		// (set) Token: 0x060005B2 RID: 1458
		List<string> Max { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060005B3 RID: 1459
		// (set) Token: 0x060005B4 RID: 1460
		List<string> Count { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060005B5 RID: 1461
		// (set) Token: 0x060005B6 RID: 1462
		List<DynamicFormatBinding> SubtotalDynamicFormat { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060005B7 RID: 1463
		// (set) Token: 0x060005B8 RID: 1464
		List<AggregateDescriptor> Aggregates { get; set; }
	}
}
