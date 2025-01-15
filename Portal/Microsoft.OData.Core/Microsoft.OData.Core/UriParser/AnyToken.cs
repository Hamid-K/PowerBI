using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B6 RID: 438
	public sealed class AnyToken : LambdaToken
	{
		// Token: 0x06001482 RID: 5250 RVA: 0x0003BC41 File Offset: 0x00039E41
		public AnyToken(QueryToken expression, string parameter, QueryToken parent)
			: base(expression, parameter, parent)
		{
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00038C13 File Offset: 0x00036E13
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Any;
			}
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0003BC55 File Offset: 0x00039E55
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
