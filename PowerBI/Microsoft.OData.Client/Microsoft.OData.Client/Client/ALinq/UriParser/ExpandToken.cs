using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000120 RID: 288
	public sealed class ExpandToken : QueryToken
	{
		// Token: 0x06000C01 RID: 3073 RVA: 0x0002CC9A File Offset: 0x0002AE9A
		public ExpandToken(params ExpandTermToken[] expandTerms)
			: this(expandTerms)
		{
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002CCA3 File Offset: 0x0002AEA3
		public ExpandToken(IEnumerable<ExpandTermToken> expandTerms)
		{
			this.expandTerms = new ReadOnlyEnumerableForUriParser<ExpandTermToken>(expandTerms ?? new ExpandTermToken[0]);
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002CCC1 File Offset: 0x0002AEC1
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Expand;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0002CCC5 File Offset: 0x0002AEC5
		public IEnumerable<ExpandTermToken> ExpandTerms
		{
			get
			{
				return this.expandTerms;
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002CCCD File Offset: 0x0002AECD
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000664 RID: 1636
		private readonly IEnumerable<ExpandTermToken> expandTerms;
	}
}
