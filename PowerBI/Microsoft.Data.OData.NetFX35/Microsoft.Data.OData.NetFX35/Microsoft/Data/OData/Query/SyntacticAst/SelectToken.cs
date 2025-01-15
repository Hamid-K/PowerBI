using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000B7 RID: 183
	internal sealed class SelectToken : QueryToken
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x0000EABA File Offset: 0x0000CCBA
		public SelectToken(IEnumerable<PathSegmentToken> properties)
		{
			this.properties = ((properties != null) ? new ReadOnlyEnumerableForUriParser<PathSegmentToken>(properties) : new ReadOnlyEnumerableForUriParser<PathSegmentToken>(new List<PathSegmentToken>()));
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000EADD File Offset: 0x0000CCDD
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Select;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000EAE1 File Offset: 0x0000CCE1
		public IEnumerable<PathSegmentToken> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000EAE9 File Offset: 0x0000CCE9
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000180 RID: 384
		private readonly IEnumerable<PathSegmentToken> properties;
	}
}
