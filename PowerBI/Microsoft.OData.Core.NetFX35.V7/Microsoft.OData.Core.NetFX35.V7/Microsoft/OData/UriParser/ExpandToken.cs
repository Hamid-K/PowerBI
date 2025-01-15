using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000170 RID: 368
	public sealed class ExpandToken : QueryToken
	{
		// Token: 0x06000F7E RID: 3966 RVA: 0x0002BEB5 File Offset: 0x0002A0B5
		public ExpandToken(IEnumerable<ExpandTermToken> expandTerms)
		{
			this.expandTerms = new ReadOnlyEnumerableForUriParser<ExpandTermToken>(expandTerms ?? new ExpandTermToken[0]);
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0002BED3 File Offset: 0x0002A0D3
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Expand;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0002BED7 File Offset: 0x0002A0D7
		public IEnumerable<ExpandTermToken> ExpandTerms
		{
			get
			{
				return this.expandTerms;
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0002BEDF File Offset: 0x0002A0DF
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007C6 RID: 1990
		private readonly IEnumerable<ExpandTermToken> expandTerms;
	}
}
