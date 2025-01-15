using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000051 RID: 81
	public sealed class SingleValueServiceOperationQueryNode : SingleValueQueryNode
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000ACA3 File Offset: 0x00008EA3
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000ACAB File Offset: 0x00008EAB
		public IEdmFunctionImport ServiceOperation { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000ACB4 File Offset: 0x00008EB4
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public IEnumerable<QueryNode> Parameters { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000ACC5 File Offset: 0x00008EC5
		public override IEdmTypeReference TypeReference
		{
			get
			{
				if (this.ServiceOperation == null)
				{
					return null;
				}
				return this.ServiceOperation.ReturnType;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000ACDC File Offset: 0x00008EDC
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.SingleValueServiceOperation;
			}
		}
	}
}
