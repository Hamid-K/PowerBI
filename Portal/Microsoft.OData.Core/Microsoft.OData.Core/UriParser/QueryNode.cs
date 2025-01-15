using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019F RID: 415
	public abstract class QueryNode
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001400 RID: 5120
		public abstract QueryNodeKind Kind { get; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x000032BD File Offset: 0x000014BD
		internal virtual InternalQueryNodeKind InternalKind
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
