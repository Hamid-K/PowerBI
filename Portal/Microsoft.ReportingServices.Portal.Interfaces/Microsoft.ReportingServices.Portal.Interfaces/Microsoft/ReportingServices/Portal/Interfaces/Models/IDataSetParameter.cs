using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x0200009E RID: 158
	public interface IDataSetParameter
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600051E RID: 1310
		// (set) Token: 0x0600051F RID: 1311
		string Name { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000520 RID: 1312
		// (set) Token: 0x06000521 RID: 1313
		string DefaultValue { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000522 RID: 1314
		// (set) Token: 0x06000523 RID: 1315
		bool Nullable { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000524 RID: 1316
		// (set) Token: 0x06000525 RID: 1317
		string DataType { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000526 RID: 1318
		// (set) Token: 0x06000527 RID: 1319
		bool IsExpression { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000528 RID: 1320
		// (set) Token: 0x06000529 RID: 1321
		bool IsMultiValued { get; set; }
	}
}
