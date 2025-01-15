using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000181 RID: 385
	public sealed class SystemToken : PathSegmentToken
	{
		// Token: 0x06000FD8 RID: 4056 RVA: 0x0002C2A5 File Offset: 0x0002A4A5
		public SystemToken(string identifier, PathSegmentToken nextToken)
			: base(nextToken)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0002C2C1 File Offset: 0x0002A4C1
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00002500 File Offset: 0x00000700
		public override bool IsNamespaceOrContainerQualified()
		{
			return false;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0002C2C9 File Offset: 0x0002A4C9
		public override T Accept<T>(IPathSegmentTokenVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0002C2DE File Offset: 0x0002A4DE
		public override void Accept(IPathSegmentTokenVisitor visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x040007FD RID: 2045
		private readonly string identifier;
	}
}
