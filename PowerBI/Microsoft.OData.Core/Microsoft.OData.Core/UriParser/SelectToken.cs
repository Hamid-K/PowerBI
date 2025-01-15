using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001CB RID: 459
	public sealed class SelectToken : QueryToken
	{
		// Token: 0x06001514 RID: 5396 RVA: 0x0003C43F File Offset: 0x0003A63F
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

		// Token: 0x06001515 RID: 5397 RVA: 0x0003C472 File Offset: 0x0003A672
		public SelectToken(IEnumerable<SelectTermToken> selectTerms)
		{
			this.selectTerms = ((selectTerms != null) ? new ReadOnlyEnumerableForUriParser<SelectTermToken>(selectTerms) : new ReadOnlyEnumerableForUriParser<SelectTermToken>(new SelectTermToken[0]));
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x00038716 File Offset: 0x00036916
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Select;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0003C496 File Offset: 0x0003A696
		public IEnumerable<PathSegmentToken> Properties
		{
			get
			{
				return this.selectTerms.Select((SelectTermToken e) => e.PathToProperty);
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0003C4C2 File Offset: 0x0003A6C2
		public IEnumerable<SelectTermToken> SelectTerms
		{
			get
			{
				return this.selectTerms;
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0003C4CA File Offset: 0x0003A6CA
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400092C RID: 2348
		private readonly IEnumerable<SelectTermToken> selectTerms;
	}
}
