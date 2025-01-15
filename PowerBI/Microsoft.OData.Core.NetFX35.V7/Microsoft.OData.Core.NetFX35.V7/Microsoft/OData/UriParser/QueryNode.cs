using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000153 RID: 339
	public abstract class QueryNode
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000EDB RID: 3803
		public abstract QueryNodeKind Kind { get; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0000FA90 File Offset: 0x0000DC90
		internal virtual InternalQueryNodeKind InternalKind
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
