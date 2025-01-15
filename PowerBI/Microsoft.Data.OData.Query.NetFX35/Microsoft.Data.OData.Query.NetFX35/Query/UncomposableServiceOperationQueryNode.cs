using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000052 RID: 82
	public sealed class UncomposableServiceOperationQueryNode : QueryNode
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000ACE7 File Offset: 0x00008EE7
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000ACEF File Offset: 0x00008EEF
		public IEdmFunctionImport ServiceOperation { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000AD00 File Offset: 0x00008F00
		public IEnumerable<QueryNode> Parameters { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000AD09 File Offset: 0x00008F09
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.UncomposableServiceOperation;
			}
		}
	}
}
