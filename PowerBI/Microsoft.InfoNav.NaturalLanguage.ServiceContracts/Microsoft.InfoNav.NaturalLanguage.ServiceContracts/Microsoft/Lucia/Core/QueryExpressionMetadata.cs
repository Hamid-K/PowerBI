using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000BE RID: 190
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryExpressionMetadata
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000700A File Offset: 0x0000520A
		// (set) Token: 0x060003DB RID: 987 RVA: 0x00007012 File Offset: 0x00005212
		[DataMember(IsRequired = true, Order = 10)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000701B File Offset: 0x0000521B
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00007023 File Offset: 0x00005223
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public SemanticType SemanticType { get; set; }
	}
}
