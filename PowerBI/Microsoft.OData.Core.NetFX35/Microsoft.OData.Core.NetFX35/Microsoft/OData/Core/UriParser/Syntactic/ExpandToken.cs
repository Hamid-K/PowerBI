using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000276 RID: 630
	internal sealed class ExpandToken : QueryToken
	{
		// Token: 0x060015F6 RID: 5622 RVA: 0x0004C26C File Offset: 0x0004A46C
		public ExpandToken(IEnumerable<ExpandTermToken> expandTerms)
		{
			this.expandTerms = new ReadOnlyEnumerableForUriParser<ExpandTermToken>(expandTerms ?? ((IEnumerable<ExpandTermToken>)new ExpandTermToken[0]));
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0004C28F File Offset: 0x0004A48F
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Expand;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0004C293 File Offset: 0x0004A493
		public IEnumerable<ExpandTermToken> ExpandTerms
		{
			get
			{
				return this.expandTerms;
			}
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x0004C29B File Offset: 0x0004A49B
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400091F RID: 2335
		private readonly IEnumerable<ExpandTermToken> expandTerms;
	}
}
