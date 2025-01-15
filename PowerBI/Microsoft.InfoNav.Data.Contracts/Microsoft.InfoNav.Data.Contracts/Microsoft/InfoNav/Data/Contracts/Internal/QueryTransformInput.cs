using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002DD RID: 733
	[DataContract(Name = "TransformInput", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryTransformInput
	{
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0002BF52 File Offset: 0x0002A152
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x0002BF5A File Offset: 0x0002A15A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public List<QueryExpressionContainer> Parameters { get; set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0002BF63 File Offset: 0x0002A163
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x0002BF6B File Offset: 0x0002A16B
		[DataMember(IsRequired = true, Order = 2)]
		public QueryTransformTable Table { get; set; }
	}
}
