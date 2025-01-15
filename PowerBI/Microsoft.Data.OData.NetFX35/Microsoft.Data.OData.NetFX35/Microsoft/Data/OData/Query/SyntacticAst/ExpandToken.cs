using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000092 RID: 146
	internal sealed class ExpandToken : QueryToken
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000BA40 File Offset: 0x00009C40
		public ExpandToken(IEnumerable<ExpandTermToken> expandTerms)
		{
			this.expandTerms = new ReadOnlyEnumerableForUriParser<ExpandTermToken>(expandTerms ?? ((IEnumerable<ExpandTermToken>)new ExpandTermToken[0]));
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000BA63 File Offset: 0x00009C63
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Expand;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000BA67 File Offset: 0x00009C67
		public IEnumerable<ExpandTermToken> ExpandTerms
		{
			get
			{
				return this.expandTerms;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000BA6F File Offset: 0x00009C6F
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000106 RID: 262
		private readonly IEnumerable<ExpandTermToken> expandTerms;
	}
}
