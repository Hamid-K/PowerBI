using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000C8 RID: 200
	[DataContract]
	public sealed class AuxiliarySelectBinding : IProjectionBinding
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000BF0F File Offset: 0x0000A10F
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x0000BF17 File Offset: 0x0000A117
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public int? Depth { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000BF20 File Offset: 0x0000A120
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0000BF28 File Offset: 0x0000A128
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public int? SecondaryDepth { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000BF31 File Offset: 0x0000A131
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0000BF39 File Offset: 0x0000A139
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string Value { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000BF42 File Offset: 0x0000A142
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0000BF4A File Offset: 0x0000A14A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public List<string> Subtotal { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000BF53 File Offset: 0x0000A153
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0000BF5B File Offset: 0x0000A15B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public List<string> Min { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000BF64 File Offset: 0x0000A164
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0000BF6C File Offset: 0x0000A16C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public List<string> Max { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000BF75 File Offset: 0x0000A175
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0000BF7D File Offset: 0x0000A17D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 90)]
		public List<string> Count { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000BF86 File Offset: 0x0000A186
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x0000BF8E File Offset: 0x0000A18E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 100)]
		public DynamicFormatBinding DynamicFormat { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000BF97 File Offset: 0x0000A197
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0000BF9F File Offset: 0x0000A19F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 110)]
		public List<DynamicFormatBinding> SubtotalDynamicFormat { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 120)]
		public List<AggregateDescriptor> Aggregates { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000BFB9 File Offset: 0x0000A1B9
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x0000BFC1 File Offset: 0x0000A1C1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 130)]
		public List<SelectIdentityKey> GroupKeys { get; set; }
	}
}
