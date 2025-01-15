using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200021F RID: 543
	public abstract class SingleValueNode : QueryNode
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060013B0 RID: 5040
		public abstract IEdmTypeReference TypeReference { get; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00048A53 File Offset: 0x00046C53
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}
	}
}
