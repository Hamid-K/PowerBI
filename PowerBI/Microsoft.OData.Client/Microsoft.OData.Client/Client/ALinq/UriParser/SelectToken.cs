using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000137 RID: 311
	public sealed class SelectToken : QueryToken
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x0002D30B File Offset: 0x0002B50B
		public SelectToken(IEnumerable<PathSegmentToken> properties)
		{
			IEnumerable<SelectTermToken> enumerable;
			if (properties != null)
			{
				enumerable = properties.Select((PathSegmentToken e) => new SelectTermToken(e));
			}
			else
			{
				enumerable = null;
			}
			this..ctor(enumerable);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002D33E File Offset: 0x0002B53E
		public SelectToken(IEnumerable<SelectTermToken> selectTerms)
		{
			this.selectTerms = ((selectTerms != null) ? new ReadOnlyEnumerableForUriParser<SelectTermToken>(selectTerms) : new ReadOnlyEnumerableForUriParser<SelectTermToken>(new SelectTermToken[0]));
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002D362 File Offset: 0x0002B562
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Select;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0002D366 File Offset: 0x0002B566
		public IEnumerable<PathSegmentToken> Properties
		{
			get
			{
				return this.selectTerms.Select((SelectTermToken e) => e.PathToProperty);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002D392 File Offset: 0x0002B592
		public IEnumerable<SelectTermToken> SelectTerms
		{
			get
			{
				return this.selectTerms;
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002D39A File Offset: 0x0002B59A
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040006A9 RID: 1705
		private readonly IEnumerable<SelectTermToken> selectTerms;
	}
}
