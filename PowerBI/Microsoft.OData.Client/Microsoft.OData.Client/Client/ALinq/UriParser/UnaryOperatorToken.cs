using System;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200013A RID: 314
	public sealed class UnaryOperatorToken : QueryToken
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x0002D425 File Offset: 0x0002B625
		public UnaryOperatorToken(UnaryOperatorKind operatorKind, QueryToken operand)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(operand, "operand");
			this.operatorKind = operatorKind;
			this.operand = operand;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0002D447 File Offset: 0x0002B647
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.UnaryOperator;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0002D44A File Offset: 0x0002B64A
		public UnaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002D452 File Offset: 0x0002B652
		public QueryToken Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002D45A File Offset: 0x0002B65A
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040006AC RID: 1708
		private readonly UnaryOperatorKind operatorKind;

		// Token: 0x040006AD RID: 1709
		private readonly QueryToken operand;
	}
}
