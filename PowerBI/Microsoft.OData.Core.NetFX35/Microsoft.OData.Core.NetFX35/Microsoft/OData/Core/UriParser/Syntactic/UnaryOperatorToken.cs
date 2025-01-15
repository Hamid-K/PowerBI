using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000284 RID: 644
	internal sealed class UnaryOperatorToken : QueryToken
	{
		// Token: 0x06001649 RID: 5705 RVA: 0x0004C697 File Offset: 0x0004A897
		public UnaryOperatorToken(UnaryOperatorKind operatorKind, QueryToken operand)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(operand, "operand");
			this.operatorKind = operatorKind;
			this.operand = operand;
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0004C6B8 File Offset: 0x0004A8B8
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.UnaryOperator;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0004C6BB File Offset: 0x0004A8BB
		public UnaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0004C6C3 File Offset: 0x0004A8C3
		public QueryToken Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0004C6CB File Offset: 0x0004A8CB
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400093A RID: 2362
		private readonly UnaryOperatorKind operatorKind;

		// Token: 0x0400093B RID: 2363
		private readonly QueryToken operand;
	}
}
