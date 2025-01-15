using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200021E RID: 542
	public abstract class QueryNode : ODataAnnotatable
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060013AC RID: 5036
		public abstract QueryNodeKind Kind { get; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00048A3D File Offset: 0x00046C3D
		internal virtual InternalQueryNodeKind InternalKind
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00048A44 File Offset: 0x00046C44
		public virtual T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
