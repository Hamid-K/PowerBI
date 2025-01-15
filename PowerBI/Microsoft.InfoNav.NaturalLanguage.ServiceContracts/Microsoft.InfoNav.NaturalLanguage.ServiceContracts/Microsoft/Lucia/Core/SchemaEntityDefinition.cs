using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000C7 RID: 199
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class SchemaEntityDefinition
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000718A File Offset: 0x0000538A
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00007192 File Offset: 0x00005392
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<string> Terms { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000719B File Offset: 0x0000539B
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x000071A3 File Offset: 0x000053A3
		[DataMember(IsRequired = true, Order = 20)]
		public string TextDefinition { get; set; }
	}
}
