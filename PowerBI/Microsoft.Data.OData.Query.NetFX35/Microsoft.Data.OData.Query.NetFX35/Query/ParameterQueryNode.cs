using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200004A RID: 74
	public sealed class ParameterQueryNode : SingleValueQueryNode
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000A874 File Offset: 0x00008A74
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000A87C File Offset: 0x00008A7C
		public IEdmTypeReference ParameterType { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000A885 File Offset: 0x00008A85
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.ParameterType;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A88D File Offset: 0x00008A8D
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.Parameter;
			}
		}
	}
}
