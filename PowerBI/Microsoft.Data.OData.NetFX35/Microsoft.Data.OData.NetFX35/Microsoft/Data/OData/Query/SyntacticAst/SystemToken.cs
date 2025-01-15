using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000096 RID: 150
	internal sealed class SystemToken : PathSegmentToken
	{
		// Token: 0x06000388 RID: 904 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public SystemToken(string identifier, PathSegmentToken nextToken)
			: base(nextToken)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000BB27 File Offset: 0x00009D27
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000BB2F File Offset: 0x00009D2F
		public override bool IsNamespaceOrContainerQualified()
		{
			return false;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000BB32 File Offset: 0x00009D32
		public override T Accept<T>(IPathSegmentTokenVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000BB46 File Offset: 0x00009D46
		public override void Accept(IPathSegmentTokenVisitor visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<IPathSegmentTokenVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0400010A RID: 266
		private readonly string identifier;
	}
}
