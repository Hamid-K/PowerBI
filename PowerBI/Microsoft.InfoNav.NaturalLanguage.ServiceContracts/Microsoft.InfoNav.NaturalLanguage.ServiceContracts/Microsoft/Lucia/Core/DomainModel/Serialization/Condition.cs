using System;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001C8 RID: 456
	public sealed class Condition
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x000124D7 File Offset: 0x000106D7
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x000124DF File Offset: 0x000106DF
		[JsonProperty(Required = Required.Always)]
		public RoleReference Target { get; set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x000124E8 File Offset: 0x000106E8
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x000124F0 File Offset: 0x000106F0
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Aggregation Aggregation { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000124F9 File Offset: 0x000106F9
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00012501 File Offset: 0x00010701
		[JsonProperty(Required = Required.Always, DefaultValueHandling = DefaultValueHandling.Include)]
		public ConditionOperator Operator { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0001250A File Offset: 0x0001070A
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00012512 File Offset: 0x00010712
		[JsonProperty(Required = Required.AllowNull, DefaultValueHandling = DefaultValueHandling.Include)]
		public Value Value { get; set; }
	}
}
