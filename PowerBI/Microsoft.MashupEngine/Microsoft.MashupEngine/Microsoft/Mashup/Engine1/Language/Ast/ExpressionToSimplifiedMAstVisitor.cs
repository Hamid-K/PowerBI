using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018A9 RID: 6313
	internal class ExpressionToSimplifiedMAstVisitor : ExpressionToMAstVisitor
	{
		// Token: 0x0600A0BC RID: 41148 RVA: 0x002149DA File Offset: 0x00212BDA
		public new static RecordValue ToMAst(IExpression expression)
		{
			return new ExpressionToSimplifiedMAstVisitor().VisitExpression(expression);
		}

		// Token: 0x0600A0BD RID: 41149 RVA: 0x002149E8 File Offset: 0x00212BE8
		protected override RecordValue VisitExpression(IExpression expression)
		{
			ExpressionKind kind = expression.Kind;
			switch (kind)
			{
			case ExpressionKind.Binary:
				return this.VisitBinary((IBinaryExpression)expression);
			case ExpressionKind.Constant:
				return this.VisitConstant((IConstantExpression)expression);
			case ExpressionKind.ElementAccess:
			case ExpressionKind.Exports:
			case ExpressionKind.Function:
			case ExpressionKind.Identifier:
				break;
			case ExpressionKind.FieldAccess:
				return this.VisitFieldAccess((IFieldAccessExpression)expression);
			case ExpressionKind.If:
				return this.VisitIf((IIfExpression)expression);
			case ExpressionKind.Invocation:
				return this.VisitInvocation((IInvocationExpression)expression);
			default:
				if (kind == ExpressionKind.NotImplemented)
				{
					return this.VisitNotImplemented((INotImplementedExpression)expression);
				}
				if (kind == ExpressionKind.Unary)
				{
					return this.VisitUnary((IUnaryExpression)expression);
				}
				break;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600A0BE RID: 41150 RVA: 0x00214A94 File Offset: 0x00212C94
		protected override RecordValue VisitBinary(IBinaryExpression binary)
		{
			switch (binary.Operator)
			{
			case BinaryOperator2.Add:
			case BinaryOperator2.Subtract:
			case BinaryOperator2.Multiply:
			case BinaryOperator2.Divide:
			case BinaryOperator2.GreaterThan:
			case BinaryOperator2.LessThan:
			case BinaryOperator2.GreaterThanOrEquals:
			case BinaryOperator2.LessThanOrEquals:
			case BinaryOperator2.Equals:
			case BinaryOperator2.NotEquals:
			case BinaryOperator2.And:
			case BinaryOperator2.Or:
			case BinaryOperator2.Concatenate:
			case BinaryOperator2.Coalesce:
				return base.VisitBinary(binary);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600A0BF RID: 41151 RVA: 0x00214B08 File Offset: 0x00212D08
		protected override RecordValue VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			if (!fieldAccess.IsOptional)
			{
				return RecordValue.New(SimplifiedMAst.FieldAccessKeys, new Value[]
				{
					MAst.FieldAccessKind,
					TextValue.New(fieldAccess.MemberName),
					this.VisitExpression(fieldAccess.Expression)
				});
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600A0C0 RID: 41152 RVA: 0x00214B60 File Offset: 0x00212D60
		protected override RecordValue VisitUnary(IUnaryExpression unary)
		{
			UnaryOperator2 @operator = unary.Operator;
			if (@operator <= UnaryOperator2.Negative)
			{
				return base.VisitUnary(unary);
			}
			throw new NotSupportedException();
		}
	}
}
