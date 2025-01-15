using System;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000283 RID: 643
	internal sealed class SystemToken : PathSegmentToken
	{
		// Token: 0x06001644 RID: 5700 RVA: 0x0004C649 File Offset: 0x0004A849
		public SystemToken(string identifier, PathSegmentToken nextToken)
			: base(nextToken)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0004C664 File Offset: 0x0004A864
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0004C66C File Offset: 0x0004A86C
		public override bool IsNamespaceOrContainerQualified()
		{
			return false;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0004C66F File Offset: 0x0004A86F
		public override T Accept<T>(IPathSegmentTokenVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0004C683 File Offset: 0x0004A883
		public override void Accept(IPathSegmentTokenVisitor visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x04000939 RID: 2361
		private readonly string identifier;
	}
}
