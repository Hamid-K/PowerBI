using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000C6 RID: 198
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class SchemaAddition
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00007160 File Offset: 0x00005360
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00007168 File Offset: 0x00005368
		[DataMember(IsRequired = false, Order = 10)]
		public string CacheKey { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00007171 File Offset: 0x00005371
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00007179 File Offset: 0x00005379
		[DataMember(IsRequired = true, Order = 20)]
		public Dictionary<string, SchemaEntityDefinition> EntityDefinitions { get; set; }
	}
}
