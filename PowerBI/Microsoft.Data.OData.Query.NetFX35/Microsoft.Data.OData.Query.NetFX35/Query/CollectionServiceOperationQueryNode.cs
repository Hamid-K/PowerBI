using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000012 RID: 18
	public sealed class CollectionServiceOperationQueryNode : CollectionQueryNode
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000356F File Offset: 0x0000176F
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00003577 File Offset: 0x00001777
		public IEdmFunctionImport ServiceOperation { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003580 File Offset: 0x00001780
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003588 File Offset: 0x00001788
		public IEnumerable<QueryNode> Parameters { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003591 File Offset: 0x00001791
		public override IEdmTypeReference ItemType
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000035A8 File Offset: 0x000017A8
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.CollectionServiceOperation;
			}
		}
	}
}
