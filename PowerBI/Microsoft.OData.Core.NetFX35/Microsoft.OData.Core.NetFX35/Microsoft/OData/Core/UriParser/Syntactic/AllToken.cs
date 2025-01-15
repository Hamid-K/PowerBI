using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x0200026E RID: 622
	internal sealed class AllToken : LambdaToken
	{
		// Token: 0x060015C6 RID: 5574 RVA: 0x0004C008 File Offset: 0x0004A208
		public AllToken(QueryToken expression, string parameter, QueryToken parent)
			: base(expression, parameter, parent)
		{
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0004C013 File Offset: 0x0004A213
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.All;
			}
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0004C017 File Offset: 0x0004A217
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
