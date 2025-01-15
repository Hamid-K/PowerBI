using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000280 RID: 640
	internal sealed class SelectToken : QueryToken
	{
		// Token: 0x06001636 RID: 5686 RVA: 0x0004C5BB File Offset: 0x0004A7BB
		public SelectToken(IEnumerable<PathSegmentToken> properties)
		{
			this.properties = ((properties != null) ? new ReadOnlyEnumerableForUriParser<PathSegmentToken>(properties) : new ReadOnlyEnumerableForUriParser<PathSegmentToken>(new List<PathSegmentToken>()));
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x0004C5DE File Offset: 0x0004A7DE
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Select;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0004C5E2 File Offset: 0x0004A7E2
		public IEnumerable<PathSegmentToken> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0004C5EA File Offset: 0x0004A7EA
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000936 RID: 2358
		private readonly IEnumerable<PathSegmentToken> properties;
	}
}
