using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x0200026F RID: 623
	internal sealed class AnyToken : LambdaToken
	{
		// Token: 0x060015C9 RID: 5577 RVA: 0x0004C020 File Offset: 0x0004A220
		public AnyToken(QueryToken expression, string parameter, QueryToken parent)
			: base(expression, parameter, parent)
		{
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0004C02B File Offset: 0x0004A22B
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Any;
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0004C02F File Offset: 0x0004A22F
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
