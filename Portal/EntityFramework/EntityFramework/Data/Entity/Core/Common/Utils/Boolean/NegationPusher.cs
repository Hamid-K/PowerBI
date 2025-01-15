using System;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000618 RID: 1560
	internal static class NegationPusher
	{
		// Token: 0x06004BA6 RID: 19366 RVA: 0x0010AE50 File Offset: 0x00109050
		internal static BoolExpr<DomainConstraint<T_Variable, T_Element>> EliminateNot<T_Variable, T_Element>(BoolExpr<DomainConstraint<T_Variable, T_Element>> expression)
		{
			return expression.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element>.Instance);
		}

		// Token: 0x02000C53 RID: 3155
		private class NonNegatedTreeVisitor<T_Identifier> : BasicVisitor<T_Identifier>
		{
			// Token: 0x06006A85 RID: 27269 RVA: 0x0016C118 File Offset: 0x0016A318
			protected NonNegatedTreeVisitor()
			{
			}

			// Token: 0x06006A86 RID: 27270 RVA: 0x0016C120 File Offset: 0x0016A320
			internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
			{
				return expression.Child.Accept<BoolExpr<T_Identifier>>(NegationPusher.NegatedTreeVisitor<T_Identifier>.Instance);
			}

			// Token: 0x040030D1 RID: 12497
			internal static readonly NegationPusher.NonNegatedTreeVisitor<T_Identifier> Instance = new NegationPusher.NonNegatedTreeVisitor<T_Identifier>();
		}

		// Token: 0x02000C54 RID: 3156
		private class NegatedTreeVisitor<T_Identifier> : Visitor<T_Identifier, BoolExpr<T_Identifier>>
		{
			// Token: 0x06006A88 RID: 27272 RVA: 0x0016C13E File Offset: 0x0016A33E
			protected NegatedTreeVisitor()
			{
			}

			// Token: 0x06006A89 RID: 27273 RVA: 0x0016C146 File Offset: 0x0016A346
			internal override BoolExpr<T_Identifier> VisitTrue(TrueExpr<T_Identifier> expression)
			{
				return FalseExpr<T_Identifier>.Value;
			}

			// Token: 0x06006A8A RID: 27274 RVA: 0x0016C14D File Offset: 0x0016A34D
			internal override BoolExpr<T_Identifier> VisitFalse(FalseExpr<T_Identifier> expression)
			{
				return TrueExpr<T_Identifier>.Value;
			}

			// Token: 0x06006A8B RID: 27275 RVA: 0x0016C154 File Offset: 0x0016A354
			internal override BoolExpr<T_Identifier> VisitTerm(TermExpr<T_Identifier> expression)
			{
				return new NotExpr<T_Identifier>(expression);
			}

			// Token: 0x06006A8C RID: 27276 RVA: 0x0016C15C File Offset: 0x0016A35C
			internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
			{
				return expression.Child.Accept<BoolExpr<T_Identifier>>(NegationPusher.NonNegatedTreeVisitor<T_Identifier>.Instance);
			}

			// Token: 0x06006A8D RID: 27277 RVA: 0x0016C16E File Offset: 0x0016A36E
			internal override BoolExpr<T_Identifier> VisitAnd(AndExpr<T_Identifier> expression)
			{
				return new OrExpr<T_Identifier>(expression.Children.Select((BoolExpr<T_Identifier> child) => child.Accept<BoolExpr<T_Identifier>>(this)));
			}

			// Token: 0x06006A8E RID: 27278 RVA: 0x0016C18C File Offset: 0x0016A38C
			internal override BoolExpr<T_Identifier> VisitOr(OrExpr<T_Identifier> expression)
			{
				return new AndExpr<T_Identifier>(expression.Children.Select((BoolExpr<T_Identifier> child) => child.Accept<BoolExpr<T_Identifier>>(this)));
			}

			// Token: 0x040030D2 RID: 12498
			internal static readonly NegationPusher.NegatedTreeVisitor<T_Identifier> Instance = new NegationPusher.NegatedTreeVisitor<T_Identifier>();
		}

		// Token: 0x02000C55 RID: 3157
		private class NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element> : NegationPusher.NonNegatedTreeVisitor<DomainConstraint<T_Variable, T_Element>>
		{
			// Token: 0x06006A92 RID: 27282 RVA: 0x0016C1C8 File Offset: 0x0016A3C8
			private NonNegatedDomainConstraintTreeVisitor()
			{
			}

			// Token: 0x06006A93 RID: 27283 RVA: 0x0016C1D0 File Offset: 0x0016A3D0
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> VisitNot(NotExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				return expression.Child.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(NegationPusher.NegatedDomainConstraintTreeVisitor<T_Variable, T_Element>.Instance);
			}

			// Token: 0x040030D3 RID: 12499
			internal new static readonly NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element> Instance = new NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element>();
		}

		// Token: 0x02000C56 RID: 3158
		private class NegatedDomainConstraintTreeVisitor<T_Variable, T_Element> : NegationPusher.NegatedTreeVisitor<DomainConstraint<T_Variable, T_Element>>
		{
			// Token: 0x06006A95 RID: 27285 RVA: 0x0016C1EE File Offset: 0x0016A3EE
			private NegatedDomainConstraintTreeVisitor()
			{
			}

			// Token: 0x06006A96 RID: 27286 RVA: 0x0016C1F6 File Offset: 0x0016A3F6
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> VisitNot(NotExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				return expression.Child.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element>.Instance);
			}

			// Token: 0x06006A97 RID: 27287 RVA: 0x0016C208 File Offset: 0x0016A408
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> VisitTerm(TermExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				return new TermExpr<DomainConstraint<T_Variable, T_Element>>(expression.Identifier.InvertDomainConstraint());
			}

			// Token: 0x040030D4 RID: 12500
			internal new static readonly NegationPusher.NegatedDomainConstraintTreeVisitor<T_Variable, T_Element> Instance = new NegationPusher.NegatedDomainConstraintTreeVisitor<T_Variable, T_Element>();
		}
	}
}
