using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000EF RID: 239
	[DataContract]
	[JsonConverter(typeof(ExecutionEventConverter))]
	public sealed class ExecutionEvent
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000D37C File Offset: 0x0000B57C
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0000D384 File Offset: 0x0000B584
		[DataMember(IsRequired = true, Order = 0)]
		public string Id { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0000D38D File Offset: 0x0000B58D
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0000D395 File Offset: 0x0000B595
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string ParentId { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0000D39E File Offset: 0x0000B59E
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x0000D3A6 File Offset: 0x0000B5A6
		[DataMember(IsRequired = true, Order = 20)]
		public string Name { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000D3AF File Offset: 0x0000B5AF
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0000D3B7 File Offset: 0x0000B5B7
		[DataMember(IsRequired = true, Order = 30)]
		public string Component { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0000D3C8 File Offset: 0x0000B5C8
		[DataMember(IsRequired = true, Order = 40)]
		public DateTime Start { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0000D3D1 File Offset: 0x0000B5D1
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x0000D3D9 File Offset: 0x0000B5D9
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public DateTime? End { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0000D3E2 File Offset: 0x0000B5E2
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x0000D3EA File Offset: 0x0000B5EA
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public Dictionary<string, object> Metrics
		{
			get
			{
				return this._metrics;
			}
			set
			{
				this._metrics = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0000D3F3 File Offset: 0x0000B5F3
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x0000D3FB File Offset: 0x0000B5FB
		public string MetricsRawJson
		{
			get
			{
				return this._metricsRawJson;
			}
			set
			{
				this._metricsRawJson = value;
			}
		}

		// Token: 0x040002AB RID: 683
		private Dictionary<string, object> _metrics;

		// Token: 0x040002AC RID: 684
		private string _metricsRawJson;
	}
}
