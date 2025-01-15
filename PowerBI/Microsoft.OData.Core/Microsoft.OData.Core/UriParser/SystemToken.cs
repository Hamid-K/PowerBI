using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001CE RID: 462
	public sealed class SystemToken : PathSegmentToken
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x0003C51A File Offset: 0x0003A71A
		public SystemToken(string identifier, PathSegmentToken nextToken)
			: base(nextToken)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x0003C536 File Offset: 0x0003A736
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00002390 File Offset: 0x00000590
		public override bool IsNamespaceOrContainerQualified()
		{
			return false;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0003C53E File Offset: 0x0003A73E
		public override T Accept<T>(IPathSegmentTokenVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0003C553 File Offset: 0x0003A753
		public override void Accept(IPathSegmentTokenVisitor visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0400092F RID: 2351
		private readonly string identifier;
	}
}
