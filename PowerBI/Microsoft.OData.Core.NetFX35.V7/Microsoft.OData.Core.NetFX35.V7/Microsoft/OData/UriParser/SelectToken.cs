using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017E RID: 382
	public sealed class SelectToken : QueryToken
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x0002C22A File Offset: 0x0002A42A
		public SelectToken(IEnumerable<PathSegmentToken> properties)
		{
			this.properties = ((properties != null) ? new ReadOnlyEnumerableForUriParser<PathSegmentToken>(properties) : new ReadOnlyEnumerableForUriParser<PathSegmentToken>(new List<PathSegmentToken>()));
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00028DE2 File Offset: 0x00026FE2
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Select;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0002C24D File Offset: 0x0002A44D
		public IEnumerable<PathSegmentToken> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0002C255 File Offset: 0x0002A455
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007FA RID: 2042
		private readonly IEnumerable<PathSegmentToken> properties;
	}
}
