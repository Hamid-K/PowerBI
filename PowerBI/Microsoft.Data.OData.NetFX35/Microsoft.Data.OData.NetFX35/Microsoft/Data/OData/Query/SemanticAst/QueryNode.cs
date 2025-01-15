using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000017 RID: 23
	public abstract class QueryNode : ODataAnnotatable
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000086 RID: 134
		public abstract QueryNodeKind Kind { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000036DF File Offset: 0x000018DF
		internal virtual InternalQueryNodeKind InternalKind
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000036E6 File Offset: 0x000018E6
		public virtual T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
