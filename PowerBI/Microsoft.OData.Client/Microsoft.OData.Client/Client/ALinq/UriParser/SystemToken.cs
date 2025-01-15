using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000139 RID: 313
	public sealed class SystemToken : PathSegmentToken
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002D3D7 File Offset: 0x0002B5D7
		public SystemToken(string identifier, PathSegmentToken nextToken)
			: base(nextToken)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0002D3F3 File Offset: 0x0002B5F3
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00015066 File Offset: 0x00013266
		public override bool IsNamespaceOrContainerQualified()
		{
			return false;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002D3FB File Offset: 0x0002B5FB
		public override T Accept<T>(IPathSegmentTokenVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0002D410 File Offset: 0x0002B610
		public override void Accept(IPathSegmentTokenVisitor visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x040006AB RID: 1707
		private readonly string identifier;
	}
}
