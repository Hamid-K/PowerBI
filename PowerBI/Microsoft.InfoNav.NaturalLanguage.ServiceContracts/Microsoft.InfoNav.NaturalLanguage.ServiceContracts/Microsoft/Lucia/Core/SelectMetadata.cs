using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000BD RID: 189
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class SelectMetadata
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00006FAD File Offset: 0x000051AD
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x00006FB5 File Offset: 0x000051B5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Restatement { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00006FBE File Offset: 0x000051BE
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00006FC6 File Offset: 0x000051C6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public SemanticType SemanticType { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00006FCF File Offset: 0x000051CF
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00006FD7 File Offset: 0x000051D7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public bool IsGroup { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00006FE0 File Offset: 0x000051E0
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00006FE8 File Offset: 0x000051E8
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 35)]
		public bool IsExplicitSortBy { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00006FF1 File Offset: 0x000051F1
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00006FF9 File Offset: 0x000051F9
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public QueryExpressionMetadata DefaultLabel { get; set; }
	}
}
