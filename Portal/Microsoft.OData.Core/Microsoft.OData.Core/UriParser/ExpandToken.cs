using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BC RID: 444
	public sealed class ExpandToken : QueryToken
	{
		// Token: 0x060014AA RID: 5290 RVA: 0x0003BE82 File Offset: 0x0003A082
		public ExpandToken(params ExpandTermToken[] expandTerms)
			: this(expandTerms)
		{
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0003BE8B File Offset: 0x0003A08B
		public ExpandToken(IEnumerable<ExpandTermToken> expandTerms)
		{
			this.expandTerms = new ReadOnlyEnumerableForUriParser<ExpandTermToken>(expandTerms ?? new ExpandTermToken[0]);
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0003B4BD File Offset: 0x000396BD
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Expand;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0003BEA9 File Offset: 0x0003A0A9
		public IEnumerable<ExpandTermToken> ExpandTerms
		{
			get
			{
				return this.expandTerms;
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0003BEB1 File Offset: 0x0003A0B1
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000909 RID: 2313
		private readonly IEnumerable<ExpandTermToken> expandTerms;
	}
}
