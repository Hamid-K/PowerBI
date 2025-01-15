using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions.Internal;

namespace System.Linq.Expressions
{
	// Token: 0x02000054 RID: 84
	internal abstract class EntityExpressionVisitor
	{
		// Token: 0x060001FC RID: 508 RVA: 0x000085F8 File Offset: 0x000067F8
		internal virtual Expression Visit(Expression exp)
		{
			if (exp == null)
			{
				return exp;
			}
			switch (exp.NodeType)
			{
			case (ExpressionType)(-1):
				return this.VisitExtension(exp);
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.And:
			case ExpressionType.AndAlso:
			case ExpressionType.ArrayIndex:
			case ExpressionType.Coalesce:
			case ExpressionType.Divide:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.LeftShift:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
			case ExpressionType.Or:
			case ExpressionType.OrElse:
			case ExpressionType.Power:
			case ExpressionType.RightShift:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				return this.VisitBinary((BinaryExpression)exp);
			case ExpressionType.ArrayLength:
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.Quote:
			case ExpressionType.TypeAs:
				return this.VisitUnary((UnaryExpression)exp);
			case ExpressionType.Call:
				return this.VisitMethodCall((MethodCallExpression)exp);
			case ExpressionType.Conditional:
				return this.VisitConditional((ConditionalExpression)exp);
			case ExpressionType.Constant:
				return this.VisitConstant((ConstantExpression)exp);
			case ExpressionType.Equal:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.NotEqual:
				return this.VisitComparison((BinaryExpression)exp);
			case ExpressionType.Invoke:
				return this.VisitInvocation((InvocationExpression)exp);
			case ExpressionType.Lambda:
				return this.VisitLambda((LambdaExpression)exp);
			case ExpressionType.ListInit:
				return this.VisitListInit((ListInitExpression)exp);
			case ExpressionType.MemberAccess:
				return this.VisitMemberAccess((MemberExpression)exp);
			case ExpressionType.MemberInit:
				return this.VisitMemberInit((MemberInitExpression)exp);
			case ExpressionType.New:
				return this.VisitNew((NewExpression)exp);
			case ExpressionType.NewArrayInit:
			case ExpressionType.NewArrayBounds:
				return this.VisitNewArray((NewArrayExpression)exp);
			case ExpressionType.Parameter:
				return this.VisitParameter((ParameterExpression)exp);
			case ExpressionType.TypeIs:
				return this.VisitTypeIs((TypeBinaryExpression)exp);
			default:
				throw Error.UnhandledExpressionType(exp.NodeType);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000087B0 File Offset: 0x000069B0
		internal virtual MemberBinding VisitBinding(MemberBinding binding)
		{
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				return this.VisitMemberAssignment((MemberAssignment)binding);
			case MemberBindingType.MemberBinding:
				return this.VisitMemberMemberBinding((MemberMemberBinding)binding);
			case MemberBindingType.ListBinding:
				return this.VisitMemberListBinding((MemberListBinding)binding);
			default:
				throw Error.UnhandledBindingType(binding.BindingType);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000880C File Offset: 0x00006A0C
		internal virtual ElementInit VisitElementInitializer(ElementInit initializer)
		{
			ReadOnlyCollection<Expression> readOnlyCollection = this.VisitExpressionList(initializer.Arguments);
			if (readOnlyCollection != initializer.Arguments)
			{
				return Expression.ElementInit(initializer.AddMethod, readOnlyCollection);
			}
			return initializer;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008840 File Offset: 0x00006A40
		internal virtual Expression VisitUnary(UnaryExpression u)
		{
			Expression expression = this.Visit(u.Operand);
			if (expression != u.Operand)
			{
				return Expression.MakeUnary(u.NodeType, expression, u.Type, u.Method);
			}
			return u;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008880 File Offset: 0x00006A80
		internal virtual Expression VisitBinary(BinaryExpression b)
		{
			Expression expression = this.Visit(b.Left);
			Expression expression2 = this.Visit(b.Right);
			Expression expression3 = this.Visit(b.Conversion);
			if (expression == b.Left && expression2 == b.Right && expression3 == b.Conversion)
			{
				return b;
			}
			if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
			{
				return Expression.Coalesce(expression, expression2, expression3 as LambdaExpression);
			}
			return Expression.MakeBinary(b.NodeType, expression, expression2, b.IsLiftedToNull, b.Method);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008909 File Offset: 0x00006B09
		internal virtual Expression VisitComparison(BinaryExpression expression)
		{
			return this.VisitBinary(EntityExpressionVisitor.RemoveUnnecessaryConverts(expression));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008918 File Offset: 0x00006B18
		internal virtual Expression VisitTypeIs(TypeBinaryExpression b)
		{
			Expression expression = this.Visit(b.Expression);
			if (expression != b.Expression)
			{
				return Expression.TypeIs(expression, b.TypeOperand);
			}
			return b;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008949 File Offset: 0x00006B49
		internal virtual Expression VisitConstant(ConstantExpression c)
		{
			return c;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000894C File Offset: 0x00006B4C
		internal virtual Expression VisitConditional(ConditionalExpression c)
		{
			Expression expression = this.Visit(c.Test);
			Expression expression2 = this.Visit(c.IfTrue);
			Expression expression3 = this.Visit(c.IfFalse);
			if (expression != c.Test || expression2 != c.IfTrue || expression3 != c.IfFalse)
			{
				return Expression.Condition(expression, expression2, expression3);
			}
			return c;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000089A5 File Offset: 0x00006BA5
		internal virtual Expression VisitParameter(ParameterExpression p)
		{
			return p;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000089A8 File Offset: 0x00006BA8
		internal virtual Expression VisitMemberAccess(MemberExpression m)
		{
			Expression expression = this.Visit(m.Expression);
			if (expression != m.Expression)
			{
				return Expression.MakeMemberAccess(expression, m.Member);
			}
			return m;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000089DC File Offset: 0x00006BDC
		internal virtual Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression expression = this.Visit(m.Object);
			IEnumerable<Expression> enumerable = this.VisitExpressionList(m.Arguments);
			if (expression != m.Object || enumerable != m.Arguments)
			{
				return Expression.Call(expression, m.Method, enumerable);
			}
			return m;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008A24 File Offset: 0x00006C24
		internal virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
		{
			List<Expression> list = null;
			int i = 0;
			int count = original.Count;
			while (i < count)
			{
				Expression expression = this.Visit(original[i]);
				if (list != null)
				{
					list.Add(expression);
				}
				else if (expression != original[i])
				{
					list = new List<Expression>(count);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(expression);
				}
				i++;
			}
			if (list != null)
			{
				return list.ToReadOnlyCollection<Expression>();
			}
			return original;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008AA4 File Offset: 0x00006CA4
		internal virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
		{
			Expression expression = this.Visit(assignment.Expression);
			if (expression != assignment.Expression)
			{
				return Expression.Bind(assignment.Member, expression);
			}
			return assignment;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008AD8 File Offset: 0x00006CD8
		internal virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			IEnumerable<MemberBinding> enumerable = this.VisitBindingList(binding.Bindings);
			if (enumerable != binding.Bindings)
			{
				return Expression.MemberBind(binding.Member, enumerable);
			}
			return binding;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008B0C File Offset: 0x00006D0C
		internal virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
		{
			IEnumerable<ElementInit> enumerable = this.VisitElementInitializerList(binding.Initializers);
			if (enumerable != binding.Initializers)
			{
				return Expression.ListBind(binding.Member, enumerable);
			}
			return binding;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008B40 File Offset: 0x00006D40
		internal virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
		{
			List<MemberBinding> list = null;
			int i = 0;
			int count = original.Count;
			while (i < count)
			{
				MemberBinding memberBinding = this.VisitBinding(original[i]);
				if (list != null)
				{
					list.Add(memberBinding);
				}
				else if (memberBinding != original[i])
				{
					list = new List<MemberBinding>(count);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(memberBinding);
				}
				i++;
			}
			if (list != null)
			{
				return list;
			}
			return original;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008BB8 File Offset: 0x00006DB8
		internal virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
		{
			List<ElementInit> list = null;
			int i = 0;
			int count = original.Count;
			while (i < count)
			{
				ElementInit elementInit = this.VisitElementInitializer(original[i]);
				if (list != null)
				{
					list.Add(elementInit);
				}
				else if (elementInit != original[i])
				{
					list = new List<ElementInit>(count);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(elementInit);
				}
				i++;
			}
			if (list != null)
			{
				return list;
			}
			return original;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008C30 File Offset: 0x00006E30
		internal virtual Expression VisitLambda(LambdaExpression lambda)
		{
			Expression expression = this.Visit(lambda.Body);
			if (expression != lambda.Body)
			{
				return Expression.Lambda(lambda.Type, expression, lambda.Parameters);
			}
			return lambda;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008C68 File Offset: 0x00006E68
		internal virtual NewExpression VisitNew(NewExpression nex)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(nex.Arguments);
			if (enumerable == nex.Arguments)
			{
				return nex;
			}
			if (nex.Members != null)
			{
				return Expression.New(nex.Constructor, enumerable, nex.Members);
			}
			return Expression.New(nex.Constructor, enumerable);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008CB4 File Offset: 0x00006EB4
		internal virtual Expression VisitMemberInit(MemberInitExpression init)
		{
			NewExpression newExpression = this.VisitNew(init.NewExpression);
			IEnumerable<MemberBinding> enumerable = this.VisitBindingList(init.Bindings);
			if (newExpression != init.NewExpression || enumerable != init.Bindings)
			{
				return Expression.MemberInit(newExpression, enumerable);
			}
			return init;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008CF8 File Offset: 0x00006EF8
		internal virtual Expression VisitListInit(ListInitExpression init)
		{
			NewExpression newExpression = this.VisitNew(init.NewExpression);
			IEnumerable<ElementInit> enumerable = this.VisitElementInitializerList(init.Initializers);
			if (newExpression != init.NewExpression || enumerable != init.Initializers)
			{
				return Expression.ListInit(newExpression, enumerable);
			}
			return init;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008D3C File Offset: 0x00006F3C
		internal virtual Expression VisitNewArray(NewArrayExpression na)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(na.Expressions);
			if (enumerable == na.Expressions)
			{
				return na;
			}
			if (na.NodeType == ExpressionType.NewArrayInit)
			{
				return Expression.NewArrayInit(na.Type.GetElementType(), enumerable);
			}
			return Expression.NewArrayBounds(na.Type.GetElementType(), enumerable);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008D90 File Offset: 0x00006F90
		internal virtual Expression VisitInvocation(InvocationExpression iv)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(iv.Arguments);
			Expression expression = this.Visit(iv.Expression);
			if (enumerable != iv.Arguments || expression != iv.Expression)
			{
				return Expression.Invoke(expression, enumerable);
			}
			return iv;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008DD2 File Offset: 0x00006FD2
		internal virtual Expression VisitExtension(Expression ext)
		{
			return ext;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008DD5 File Offset: 0x00006FD5
		internal static Expression Visit(Expression exp, Func<Expression, Func<Expression, Expression>, Expression> visit)
		{
			return new EntityExpressionVisitor.BasicExpressionVisitor(visit).Visit(exp);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008DE4 File Offset: 0x00006FE4
		private static BinaryExpression RemoveUnnecessaryConverts(BinaryExpression expression)
		{
			if (expression.Method != null || expression.Left.Type != expression.Right.Type)
			{
				return expression;
			}
			ExpressionType nodeType = expression.Left.NodeType;
			if (nodeType != ExpressionType.Constant)
			{
				if (nodeType == ExpressionType.Convert)
				{
					UnaryExpression unaryExpression = (UnaryExpression)expression.Left;
					ExpressionType nodeType2 = expression.Right.NodeType;
					if (nodeType2 != ExpressionType.Constant)
					{
						if (nodeType2 == ExpressionType.Convert)
						{
							UnaryExpression unaryExpression2 = (UnaryExpression)expression.Right;
							if (EntityExpressionVisitor.CanRemoveConverts(unaryExpression, unaryExpression2))
							{
								return EntityExpressionVisitor.MakeBinaryExpression(expression.NodeType, unaryExpression.Operand, unaryExpression2.Operand);
							}
						}
					}
					else
					{
						ConstantExpression constantExpression = (ConstantExpression)expression.Right;
						if (EntityExpressionVisitor.TryConvertConstant(ref constantExpression, unaryExpression.Operand.Type))
						{
							return EntityExpressionVisitor.MakeBinaryExpression(expression.NodeType, unaryExpression.Operand, constantExpression);
						}
					}
				}
			}
			else
			{
				ConstantExpression constantExpression2 = (ConstantExpression)expression.Left;
				if (expression.Right.NodeType == ExpressionType.Convert)
				{
					UnaryExpression unaryExpression3 = (UnaryExpression)expression.Right;
					if (EntityExpressionVisitor.TryConvertConstant(ref constantExpression2, unaryExpression3.Operand.Type))
					{
						return EntityExpressionVisitor.MakeBinaryExpression(expression.NodeType, constantExpression2, unaryExpression3.Operand);
					}
				}
			}
			return expression;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008F1C File Offset: 0x0000711C
		private static bool CanRemoveConverts(UnaryExpression leftConvert, UnaryExpression rightConvert)
		{
			if (leftConvert.Method != null || rightConvert.Method != null)
			{
				return false;
			}
			if (Type.GetTypeCode(leftConvert.Type) != TypeCode.Int32)
			{
				return false;
			}
			TypeCode typeCode = Type.GetTypeCode(leftConvert.Operand.Type);
			return typeCode - TypeCode.Byte <= 1 && leftConvert.Operand.Type == rightConvert.Operand.Type;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008F8C File Offset: 0x0000718C
		private static bool TryConvertConstant(ref ConstantExpression constant, Type type)
		{
			if (Type.GetTypeCode(constant.Type) != TypeCode.Int32)
			{
				return false;
			}
			int num = (int)constant.Value;
			TypeCode typeCode = Type.GetTypeCode(type);
			if (typeCode != TypeCode.Byte)
			{
				if (typeCode == TypeCode.Int16)
				{
					if (num >= -32768 && num <= 32767)
					{
						constant = Expression.Constant((short)num);
						return true;
					}
				}
			}
			else if (num >= 0 && num <= 255)
			{
				constant = Expression.Constant((byte)num);
				return true;
			}
			return false;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009008 File Offset: 0x00007208
		private static BinaryExpression MakeBinaryExpression(ExpressionType expressionType, Expression left, Expression right)
		{
			if (left.Type.IsEnum)
			{
				left = Expression.Convert(left, left.Type.GetEnumUnderlyingType());
			}
			if (right.Type.IsEnum)
			{
				right = Expression.Convert(right, right.Type.GetEnumUnderlyingType());
			}
			return Expression.MakeBinary(expressionType, left, right);
		}

		// Token: 0x040000A8 RID: 168
		internal const ExpressionType CustomExpression = (ExpressionType)(-1);

		// Token: 0x020006FE RID: 1790
		private sealed class BasicExpressionVisitor : EntityExpressionVisitor
		{
			// Token: 0x06005473 RID: 21619 RVA: 0x0012FBC8 File Offset: 0x0012DDC8
			internal BasicExpressionVisitor(Func<Expression, Func<Expression, Expression>, Expression> visit)
			{
				Func<Expression, Func<Expression, Expression>, Expression> func = visit;
				if (visit == null && (func = EntityExpressionVisitor.BasicExpressionVisitor.<>c.<>9__1_0) == null)
				{
					func = (EntityExpressionVisitor.BasicExpressionVisitor.<>c.<>9__1_0 = (Expression exp, Func<Expression, Expression> baseVisit) => baseVisit(exp));
				}
				this._visit = func;
			}

			// Token: 0x06005474 RID: 21620 RVA: 0x0012FBFA File Offset: 0x0012DDFA
			internal override Expression Visit(Expression exp)
			{
				return this._visit(exp, new Func<Expression, Expression>(base.Visit));
			}

			// Token: 0x04001E33 RID: 7731
			private readonly Func<Expression, Func<Expression, Expression>, Expression> _visit;
		}
	}
}
