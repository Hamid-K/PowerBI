using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000CF RID: 207
	[DataContract]
	public sealed class DataShapeExpressionsAxisGrouping
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000C22F File Offset: 0x0000A42F
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0000C237 File Offset: 0x0000A437
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public IList<DataShapeExpressionsAxisGroupingKey> Keys { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000C240 File Offset: 0x0000A440
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0000C248 File Offset: 0x0000A448
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 20)]
		public string Member { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000C251 File Offset: 0x0000A451
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0000C259 File Offset: 0x0000A459
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string SubtotalMember { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000C262 File Offset: 0x0000A462
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0000C26A File Offset: 0x0000A46A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public IList<string> RestartIdentities { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000C273 File Offset: 0x0000A473
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000C27B File Offset: 0x0000A47B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public DataShapeExpressionsAxisGroupingAggregates Aggregates { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000C284 File Offset: 0x0000A484
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000C28C File Offset: 0x0000A48C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public string SynchronizationIndex { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000C295 File Offset: 0x0000A495
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0000C29D File Offset: 0x0000A49D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public DataShapeExpressionsAxisGrouping SynchronizedGroup { get; set; }
	}
}
