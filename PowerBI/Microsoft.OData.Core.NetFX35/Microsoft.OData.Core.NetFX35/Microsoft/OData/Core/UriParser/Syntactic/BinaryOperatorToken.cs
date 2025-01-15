using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000270 RID: 624
	internal sealed class BinaryOperatorToken : QueryToken
	{
		// Token: 0x060015CC RID: 5580 RVA: 0x0004C038 File Offset: 0x0004A238
		public BinaryOperatorToken(BinaryOperatorKind operatorKind, QueryToken left, QueryToken right)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(left, "left");
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(right, "right");
			this.operatorKind = operatorKind;
			this.left = left;
			this.right = right;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0004C06B File Offset: 0x0004A26B
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.BinaryOperator;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x0004C06E File Offset: 0x0004A26E
		public BinaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x0004C076 File Offset: 0x0004A276
		public QueryToken Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0004C07E File Offset: 0x0004A27E
		public QueryToken Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0004C086 File Offset: 0x0004A286
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400090C RID: 2316
		private readonly BinaryOperatorKind operatorKind;

		// Token: 0x0400090D RID: 2317
		private readonly QueryToken left;

		// Token: 0x0400090E RID: 2318
		private readonly QueryToken right;
	}
}
