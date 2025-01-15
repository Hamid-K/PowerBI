using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012AB RID: 4779
	internal class ConstantFoldingVisitor : TypeflowVisitor, IExpressionEnvironment, ITypeflowEnvironment
	{
		// Token: 0x06007D73 RID: 32115 RVA: 0x001ADED4 File Offset: 0x001AC0D4
		protected ConstantFoldingVisitor()
		{
		}

		// Token: 0x06007D74 RID: 32116 RVA: 0x001ADEDC File Offset: 0x001AC0DC
		public static IExpression Fold(IFunctionExpression function, TypeValue[] parameterTypes)
		{
			return new ConstantFoldingVisitor().VisitFunction(function, parameterTypes);
		}

		// Token: 0x06007D75 RID: 32117 RVA: 0x001ADEEA File Offset: 0x001AC0EA
		public static IExpression Fold(IExpression node)
		{
			return new ConstantFoldingVisitor().VisitExpression(node);
		}

		// Token: 0x06007D76 RID: 32118 RVA: 0x001ADEF8 File Offset: 0x001AC0F8
		private bool IsLogicalOrNull(IExpression expression)
		{
			TypeValue type = base.GetType(expression);
			return type.TypeKind == ValueKind.Logical || type.TypeKind == ValueKind.Null;
		}

		// Token: 0x06007D77 RID: 32119 RVA: 0x001ADF24 File Offset: 0x001AC124
		protected override IExpression VisitBinary(IBinaryExpression node)
		{
			node = (IBinaryExpression)base.VisitBinary(node);
			Value value = null;
			Value value2 = null;
			IExpression expression;
			if (this.TryCombineConstants(node.Operator, node.Left, node.Right, out expression))
			{
				return expression;
			}
			if (node.Right.TryGetConstant(out value2))
			{
				if (this.TryFoldInequality(node, node.Operator, node.Left, value2, false, out expression))
				{
					return expression;
				}
				if (this.IsLogicalOrNull(node.Left) && this.TryFoldLogical(node, value2, node.Left, out expression))
				{
					return expression;
				}
			}
			else if (node.Left.TryGetConstant(out value))
			{
				if (node.Operator == BinaryOperator2.Coalesce)
				{
					if (!value.IsNull)
					{
						return node.Left;
					}
					return node.Right;
				}
				else if (this.TryFoldInequality(node, node.Operator.SwapOperands(), node.Right, value, false, out expression) || this.TryFoldLogical(node, value, node.Right, out expression))
				{
					return expression;
				}
			}
			BinaryOperator2 @operator = node.Operator;
			if (@operator == BinaryOperator2.Add || @operator == BinaryOperator2.Multiply || @operator == BinaryOperator2.Concatenate)
			{
				if (node.Right.Kind == ExpressionKind.Binary && ((IBinaryExpression)node.Right).Operator == node.Operator && this.TryCombineConstants(node.Operator, node.Left, ((IBinaryExpression)node.Right).Left, out expression))
				{
					return BinaryExpressionSyntaxNode.New(node.Operator, expression, ((IBinaryExpression)node.Right).Right, TokenRange.Null);
				}
				if (node.Left.Kind == ExpressionKind.Binary && ((IBinaryExpression)node.Left).Operator == node.Operator && this.TryCombineConstants(node.Operator, ((IBinaryExpression)node.Left).Right, node.Right, out expression))
				{
					return BinaryExpressionSyntaxNode.New(node.Operator, ((IBinaryExpression)node.Left).Left, expression, TokenRange.Null);
				}
			}
			return node;
		}

		// Token: 0x06007D78 RID: 32120 RVA: 0x001AE100 File Offset: 0x001AC300
		private bool TryCombineConstants(BinaryOperator2 op, IExpression leftExpr, IExpression rightExpr, out IExpression combined)
		{
			Value value = null;
			Value value2 = null;
			if (leftExpr.TryGetConstant(out value) && rightExpr.TryGetConstant(out value2))
			{
				Value value3 = PartialEvaluationAlgebra.Binary(op, value, value2);
				if (value3 != null)
				{
					combined = this.Constant(value3);
					return true;
				}
			}
			combined = null;
			return false;
		}

		// Token: 0x06007D79 RID: 32121 RVA: 0x001AE144 File Offset: 0x001AC344
		private bool TryFoldLogical(IBinaryExpression node, Value value, IExpression other, out IExpression result)
		{
			BinaryOperator2 @operator = node.Operator;
			if (@operator != BinaryOperator2.And)
			{
				if (@operator == BinaryOperator2.Or)
				{
					if (value.Equals(LogicalValue.True))
					{
						result = ConstantExpressionSyntaxNode.True;
						return true;
					}
					if (value.Equals(LogicalValue.False) && this.IsLogicalOrNull(other))
					{
						result = other;
						return true;
					}
				}
			}
			else
			{
				if (value.Equals(LogicalValue.False))
				{
					result = ConstantExpressionSyntaxNode.False;
					return true;
				}
				if (value.Equals(LogicalValue.True) && this.IsLogicalOrNull(other))
				{
					result = other;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06007D7A RID: 32122 RVA: 0x001AE1D0 File Offset: 0x001AC3D0
		private bool TryFoldInequality(IExpression node, BinaryOperator2 op, IExpression left, Value right, bool nullableEquality, out IExpression result)
		{
			IExpression expression;
			IExpression expression2;
			Value value;
			if (op - BinaryOperator2.GreaterThan <= 5 && this.TryGetTemporalComparison(left, right, out expression, out expression2, out value))
			{
				if (right.Kind == ValueKind.Date && value.Kind != ValueKind.Date)
				{
					IExpression expression3 = ConstantExpressionSyntaxNode.New(value.Add(DurationValue.OneDay));
					switch (op)
					{
					case BinaryOperator2.GreaterThan:
						result = BinaryExpressionSyntaxNode.New(BinaryOperator2.GreaterThanOrEquals, expression, expression3, node.Range);
						break;
					case BinaryOperator2.LessThan:
					case BinaryOperator2.GreaterThanOrEquals:
						result = BinaryExpressionSyntaxNode.New(op, expression, expression2, node.Range);
						break;
					case BinaryOperator2.LessThanOrEquals:
						result = BinaryExpressionSyntaxNode.New(BinaryOperator2.LessThan, expression, expression3, node.Range);
						break;
					case BinaryOperator2.Equals:
						result = BinaryExpressionSyntaxNode.New(BinaryOperator2.And, BinaryExpressionSyntaxNode.New(BinaryOperator2.GreaterThanOrEquals, expression, expression2, node.Range), BinaryExpressionSyntaxNode.New(BinaryOperator2.LessThan, expression, expression3, node.Range), node.Range);
						break;
					case BinaryOperator2.NotEquals:
						result = BinaryExpressionSyntaxNode.New(BinaryOperator2.Or, BinaryExpressionSyntaxNode.New(BinaryOperator2.LessThan, expression, expression2, node.Range), BinaryExpressionSyntaxNode.New(BinaryOperator2.GreaterThanOrEquals, expression, expression3, node.Range), node.Range);
						break;
					default:
						throw new InvalidOperationException();
					}
				}
				else if (value.Kind == ValueKind.Date && right.Kind == ValueKind.DateTime && right.AsDateTime.AsClrDateTime.TimeOfDay != TimeSpan.Zero)
				{
					result = null;
					switch (op)
					{
					case BinaryOperator2.GreaterThan:
					case BinaryOperator2.LessThanOrEquals:
						break;
					case BinaryOperator2.LessThan:
						op = BinaryOperator2.LessThanOrEquals;
						break;
					case BinaryOperator2.GreaterThanOrEquals:
						op = BinaryOperator2.GreaterThan;
						break;
					case BinaryOperator2.Equals:
						if (nullableEquality)
						{
							result = null;
							return false;
						}
						result = ConstantExpressionSyntaxNode.False;
						break;
					case BinaryOperator2.NotEquals:
						if (nullableEquality)
						{
							result = null;
							return false;
						}
						result = ConstantExpressionSyntaxNode.True;
						break;
					default:
						throw new InvalidOperationException();
					}
					result = result ?? BinaryExpressionSyntaxNode.New(op, expression, expression2, node.Range);
				}
				else if (op == BinaryOperator2.Equals && nullableEquality)
				{
					result = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(Library._Value.NullableEquals), expression, expression2, node.Range);
				}
				else
				{
					result = BinaryExpressionSyntaxNode.New(op, expression, expression2, node.Range);
				}
				TypeValue typeValue = OperatorTypeflowModels.Binary(op, base.GetType(expression), base.GetType(expression2));
				base.SetType(result, typeValue);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06007D7B RID: 32123 RVA: 0x001AE408 File Offset: 0x001AC608
		protected override IExpression VisitInvocation(IInvocationExpression invocation)
		{
			invocation = (IInvocationExpression)base.VisitInvocation(invocation);
			Value value;
			if (invocation.Function.TryGetConstant(out value) && value.IsFunction)
			{
				IFunctionIdentity functionIdentity = value.AsFunction.FunctionIdentity;
				Value value2;
				if (functionIdentity.Equals(Library._Value.As.FunctionIdentity) && invocation.Arguments.Count == 2 && invocation.Arguments[1].TryGetConstant(out value2) && value2.IsType)
				{
					TypeValue asType = value2.AsType;
					if (base.GetType(invocation.Arguments[0]).IsCompatibleWith(asType))
					{
						return invocation.Arguments[0];
					}
					base.SetType(invocation, asType);
				}
				else
				{
					if (functionIdentity.Equals(Library.List.Contains) && invocation.Arguments.Count == 2 && invocation.Arguments[0].TryGetConstant(out value2) && value2.IsList)
					{
						IExpression expression = invocation.Arguments[1];
						IExpression expression2 = null;
						List<IValueReference> list = new List<IValueReference>(value2.AsList.Count);
						foreach (IValueReference valueReference in value2.AsList)
						{
							IExpression expression3;
							IExpression expression4;
							Value value3;
							if (!this.TryGetTemporalComparison(expression, valueReference.Value, out expression3, out expression4, out value3) || (valueReference.Value.Kind == ValueKind.Date && value3.Kind != ValueKind.Date) || (expression2 != null && !ConstantFoldingVisitor.AreSame(expression2, expression3)))
							{
								return invocation;
							}
							expression2 = expression2 ?? expression3;
							if (value3.Kind != ValueKind.Date || valueReference.Value.Kind != ValueKind.DateTime || valueReference.Value.AsDateTime.AsClrDateTime.TimeOfDay == TimeSpan.Zero)
							{
								list.Add(value3);
							}
						}
						if (list.Count == 0)
						{
							return ConstantExpressionSyntaxNode.False;
						}
						return new InvocationExpressionSyntaxNode2(invocation.Function, ConstantExpressionSyntaxNode.New(ListValue.New(list)), expression2);
					}
					if ((functionIdentity == Library._Value.Equals || functionIdentity == Library._Value.NullableEquals) && (invocation.Arguments.Count == 2 || invocation.Arguments.Count == 3))
					{
						bool flag = functionIdentity == Library._Value.NullableEquals;
						IExpression expression5;
						if ((invocation.Arguments[0].TryGetConstant(out value2) && this.TryFoldInequality(invocation, BinaryOperator2.Equals, invocation.Arguments[1], value2, flag, out expression5)) || (invocation.Arguments[1].TryGetConstant(out value2) && this.TryFoldInequality(invocation, BinaryOperator2.Equals, invocation.Arguments[0], value2, flag, out expression5)))
						{
							return expression5;
						}
					}
				}
			}
			return invocation;
		}

		// Token: 0x06007D7C RID: 32124 RVA: 0x001AE6D4 File Offset: 0x001AC8D4
		protected override IExpression VisitUnary(IUnaryExpression node)
		{
			node = (IUnaryExpression)base.VisitUnary(node);
			Value value;
			if (node.Expression.TryGetConstant(out value))
			{
				Value value2 = PartialEvaluationAlgebra.Unary(node.Operator, value);
				if (value2 != null)
				{
					return this.Constant(value2);
				}
			}
			if (node.Operator == UnaryOperator2.Positive)
			{
				TypeValue type = base.GetType(node.Expression);
				if (type.TypeKind == ValueKind.Null || type.TypeKind == ValueKind.Number || type.TypeKind == ValueKind.Duration)
				{
					return node.Expression;
				}
			}
			return node;
		}

		// Token: 0x06007D7D RID: 32125 RVA: 0x001AE750 File Offset: 0x001AC950
		protected override IExpression VisitElementAccess(IElementAccessExpression node)
		{
			node = (IElementAccessExpression)base.VisitElementAccess(node);
			Value value;
			Value value2;
			if (node.Collection.TryGetConstant(out value) && node.Key.TryGetConstant(out value2))
			{
				try
				{
					return this.Constant(value[value2]);
				}
				catch (RuntimeException)
				{
				}
				return node;
			}
			return node;
		}

		// Token: 0x06007D7E RID: 32126 RVA: 0x001AE7B0 File Offset: 0x001AC9B0
		protected override IExpression VisitList(IListExpression node)
		{
			node = (IListExpression)base.VisitList(node);
			Value[] constants = this.GetConstants(node.Members);
			if (constants != null)
			{
				return this.Constant(ListValue.New(constants));
			}
			return node;
		}

		// Token: 0x06007D7F RID: 32127 RVA: 0x001AE7EC File Offset: 0x001AC9EC
		protected override IExpression VisitFieldAccess(IFieldAccessExpression node)
		{
			node = (IFieldAccessExpression)base.VisitFieldAccess(node);
			Value value;
			if (node.Expression.TryGetConstant(out value) && value.IsRecord)
			{
				RecordValue asRecord = value.AsRecord;
				try
				{
					if (asRecord.TryGetValue(node.MemberName, out value))
					{
						return this.Constant(value);
					}
					if (node.IsOptional)
					{
						return ConstantExpressionSyntaxNode.Null;
					}
				}
				catch (RuntimeException)
				{
				}
			}
			TypeValue type = base.GetType(node.Expression);
			if (type.IsRecordType && node.IsOptional)
			{
				Value value2;
				if (type.AsRecordType.Fields.TryGetValue(node.MemberName, out value2))
				{
					Value value3;
					if (value2.TryGetValue("Optional", out value3) && value3.IsLogical && !value3.AsBoolean)
					{
						return new RequiredFieldAccessExpressionSyntaxNode(node.Expression, node.MemberName);
					}
				}
				else if (!type.AsRecordType.Open)
				{
					return ConstantExpressionSyntaxNode.Null;
				}
			}
			return node;
		}

		// Token: 0x06007D80 RID: 32128 RVA: 0x001AE8F8 File Offset: 0x001ACAF8
		protected override IExpression VisitRecord(IRecordExpression node)
		{
			node = (IRecordExpression)base.VisitRecord(node);
			Value[] array = new Value[node.Members.Count];
			KeysBuilder keysBuilder = new KeysBuilder(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				Value value;
				if (!node.Members[i].Value.TryGetConstant(out value))
				{
					return node;
				}
				array[i] = value;
				if (!keysBuilder.Union(node.Members[i].Name))
				{
					return node;
				}
			}
			return this.Constant(RecordValue.New(keysBuilder.ToKeys(), array));
		}

		// Token: 0x06007D81 RID: 32129 RVA: 0x001AE998 File Offset: 0x001ACB98
		protected override IExpression VisitIf(IIfExpression node)
		{
			node = (IIfExpression)base.VisitIf(node);
			Value value;
			if (!node.Condition.TryGetConstant(out value) || !value.IsLogical)
			{
				return node;
			}
			if (!value.AsBoolean)
			{
				return node.FalseCase;
			}
			return node.TrueCase;
		}

		// Token: 0x06007D82 RID: 32130 RVA: 0x001AE9E4 File Offset: 0x001ACBE4
		protected Value[] GetConstants(IList<IExpression> expressions)
		{
			Value[] array = new Value[expressions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				Value value;
				if (!expressions[i].TryGetConstant(out value))
				{
					return null;
				}
				array[i] = value;
			}
			return array;
		}

		// Token: 0x06007D83 RID: 32131 RVA: 0x001AEA24 File Offset: 0x001ACC24
		protected IExpression Reduce(IInvocationExpression invocation)
		{
			Value value;
			if (!invocation.Function.TryGetConstant(out value) || !value.IsFunction)
			{
				return invocation;
			}
			IInvocationRewriter invocationRewriter;
			IExpression expression;
			if (value.AsFunction.TryGetAs<IInvocationRewriter>(out invocationRewriter) && invocationRewriter.TryRewriteInvocation(invocation, this, out expression))
			{
				return this.VisitExpression(expression);
			}
			IList<IExpression> arguments = invocation.Arguments;
			if (this.TryExpandToProductOrSum(value, arguments, out expression))
			{
				return expression;
			}
			IExpression expression2;
			Keys keys;
			IExpression expression3;
			Keys keys2;
			if (value.Equals(Library._Value.Equals) && ConstantFoldingVisitor.TryGetRecordAndFields(arguments[0], 2147483647, out expression2, out keys) && ConstantFoldingVisitor.TryGetRecordAndFields(arguments[1], 2147483647, out expression3, out keys2) && keys.Length == keys2.Length)
			{
				IExpression expression4 = null;
				for (int i = 0; i < keys.Length; i++)
				{
					string text = keys[i];
					if (!keys2.Contains(text))
					{
						expression4 = null;
						break;
					}
					Identifier identifier = Identifier.New(text);
					IExpression expression5 = BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, new RequiredFieldAccessExpressionSyntaxNode(expression2, identifier), new RequiredFieldAccessExpressionSyntaxNode(expression3, identifier), TokenRange.Null);
					if (expression4 != null)
					{
						expression4 = BinaryExpressionSyntaxNode.New(BinaryOperator2.And, expression4, expression5, TokenRange.Null);
					}
					else
					{
						expression4 = expression5;
					}
				}
				if (expression4 != null)
				{
					return this.VisitExpression(expression4);
				}
			}
			return invocation;
		}

		// Token: 0x06007D84 RID: 32132 RVA: 0x001AEB61 File Offset: 0x001ACD61
		private IExpression Constant(Value value)
		{
			return base.SetType(ConstantExpressionSyntaxNode.New(value), value.Type);
		}

		// Token: 0x06007D85 RID: 32133 RVA: 0x001AEB75 File Offset: 0x001ACD75
		private bool IsNullable(IExpression expression)
		{
			return base.GetType(expression).IsNullable;
		}

		// Token: 0x06007D86 RID: 32134 RVA: 0x001AEB84 File Offset: 0x001ACD84
		private bool TryExpandToProductOrSum(Value function, IList<IExpression> arguments, out IExpression expanded)
		{
			expanded = null;
			if (!function.Equals(Library.List.Sum) && !function.Equals(Library.List.Product))
			{
				return false;
			}
			if (arguments.Count != 1 && arguments.Count != 2)
			{
				return false;
			}
			IListExpression listExpression;
			if (!this.TryAsListExpression(arguments[0], 10, out listExpression))
			{
				return false;
			}
			IList<IExpression> members = listExpression.Members;
			if (members.Count == 0)
			{
				expanded = ConstantExpressionSyntaxNode.Null;
				return true;
			}
			bool flag = function.Equals(Library.List.Sum);
			IExpression expression = this.Constant(flag ? NumberValue.Zero : NumberValue.One);
			Func<IExpression, IExpression, IExpression> func;
			if (arguments.Count == 1)
			{
				BinaryOperator2 op = (flag ? BinaryOperator2.Add : BinaryOperator2.Multiply);
				func = (IExpression a, IExpression b) => BinaryExpressionSyntaxNode.New(op, a, b, TokenRange.Null);
			}
			else
			{
				FunctionValue fn = (flag ? Library._Value.Add : Library._Value.Multiply);
				func = (IExpression a, IExpression b) => new InvocationExpressionSyntaxNodeN(this.Constant(fn), new IExpression[]
				{
					a,
					b,
					arguments[1]
				});
			}
			IExpression expression2 = null;
			IExpression expression3 = null;
			bool flag2 = false;
			foreach (IExpression expression4 in members)
			{
				Value value;
				IExpression expression5;
				IExpression expression6;
				if (expression4.TryGetConstant(out value))
				{
					if (value.IsNull)
					{
						continue;
					}
					expression5 = null;
					expression6 = expression4;
					flag2 = true;
				}
				else
				{
					expression5 = BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, expression4, ConstantExpressionSyntaxNode.Null, TokenRange.Null);
					expression6 = new IfExpressionSyntaxNode(expression5, expression, expression4, TokenRange.Null);
				}
				if (expression3 == null)
				{
					expression3 = expression6;
				}
				else
				{
					expression3 = func(expression3, expression6);
				}
				if (expression2 == null || expression5 == null)
				{
					expression2 = expression5 ?? expression2;
				}
				else
				{
					expression2 = BinaryExpressionSyntaxNode.New(BinaryOperator2.And, expression2, expression5, TokenRange.Null);
				}
			}
			expanded = expression3 ?? ConstantExpressionSyntaxNode.Null;
			if (!flag2)
			{
				expanded = new IfExpressionSyntaxNode(expression2, ConstantExpressionSyntaxNode.Null, expression3, TokenRange.Null);
			}
			return true;
		}

		// Token: 0x06007D87 RID: 32135 RVA: 0x001AED7C File Offset: 0x001ACF7C
		protected IExpression Reduce(IBinaryExpression binary)
		{
			BinaryOperator2 @operator = binary.Operator;
			if (@operator - BinaryOperator2.GreaterThan <= 5)
			{
				Value value;
				IExpression expression;
				Value value2;
				bool flag;
				if (binary.Right.TryGetConstant(out value) && ConstantFoldingVisitor.TryGetOffset(binary.Left, out expression, out value2, out flag) && value.IsNumber && value2.IsNumber)
				{
					BinaryOperator2 binaryOperator = binary.Operator;
					value = value.Subtract(value2);
					if (flag)
					{
						binaryOperator = binaryOperator.Negate();
						value = value.Negate();
					}
					binary = BinaryExpressionSyntaxNode.New(flag ? binary.Operator.Negate() : binary.Operator, expression, this.Constant(value), binary.Range);
				}
				else if (binary.Left.TryGetConstant(out value) && ConstantFoldingVisitor.TryGetOffset(binary.Right, out expression, out value2, out flag) && value.IsNumber && value2.IsNumber)
				{
					BinaryOperator2 binaryOperator2 = binary.Operator;
					value = value.Subtract(value2);
					if (flag)
					{
						binaryOperator2 = binaryOperator2.Negate();
						value = value.Negate();
					}
					binary = BinaryExpressionSyntaxNode.New(flag ? binary.Operator.Negate() : binary.Operator, this.Constant(value), expression, binary.Range);
				}
			}
			IExpression expression2;
			if (ConstantFoldingVisitor.TryReduceTextPositionOf(binary, BinaryOperator2.GreaterThanOrEquals, ConstantFoldingVisitor.textContains, out expression2))
			{
				return expression2;
			}
			if (ConstantFoldingVisitor.TryReduceTextPositionOf(binary, BinaryOperator2.Equals, ConstantFoldingVisitor.textStartsWith, out expression2))
			{
				return expression2;
			}
			if (ConstantFoldingVisitor.TryReduceIfInequality(binary, out expression2))
			{
				return expression2;
			}
			return binary;
		}

		// Token: 0x06007D88 RID: 32136 RVA: 0x001AEED4 File Offset: 0x001AD0D4
		private bool TryGetTemporalComparison(IExpression left, Value rightValue, out IExpression newLeft, out IExpression newRight, out Value newRightValue)
		{
			FunctionValue functionValue;
			IExpression expression;
			if (ConstantFoldingVisitor.TryGetInvocation(left, out functionValue, out expression) && ConstantFoldingVisitor.IsTemporalConversionFunction(rightValue.Kind, functionValue))
			{
				TypeValue type = base.GetType(expression);
				FunctionValue functionValue2;
				if (ConstantFoldingVisitor.IsTemporalConversionSupported(rightValue.Kind, type.TypeKind) && ConstantFoldingVisitor.TryGetTemporalConversionFunction(type.TypeKind, out functionValue2))
				{
					newRightValue = functionValue2.Invoke(rightValue);
					newLeft = base.SetType(expression, type);
					newRight = base.SetType(new ConstantExpressionSyntaxNode(newRightValue), newRightValue.Type);
					return true;
				}
			}
			newLeft = null;
			newRight = null;
			newRightValue = null;
			return false;
		}

		// Token: 0x06007D89 RID: 32137 RVA: 0x001AEF60 File Offset: 0x001AD160
		public bool TryAsListExpression(IExpression expression, int maxArguments, out IListExpression listExpression)
		{
			ExpressionKind kind = expression.Kind;
			if (kind != ExpressionKind.Constant)
			{
				IExpression record;
				IExpression expression2;
				Keys keys;
				if (kind != ExpressionKind.Invocation)
				{
					if (kind == ExpressionKind.List && ((IListExpression)expression).Members.Count <= maxArguments)
					{
						listExpression = (IListExpression)expression;
						return true;
					}
				}
				else if (this.TryGetInvokeFieldValues((IInvocationExpression)expression, out expression2) && ConstantFoldingVisitor.TryGetRecordAndFields(expression2, maxArguments, out record, out keys) && record.Kind != ExpressionKind.Invocation)
				{
					listExpression = new ListExpressionSyntaxNode(keys.Select((string fld) => new RequiredFieldAccessExpressionSyntaxNode(record, fld)).ToList<IExpression>());
					return true;
				}
			}
			else
			{
				Value value = ((IConstantExpression)expression).Value;
				if (value.IsList && value.AsList.IsBuffered && value.AsList.Count <= maxArguments)
				{
					listExpression = new ListExpressionSyntaxNode(value.AsList.Select((IValueReference c) => ConstantExpressionSyntaxNode.New(c.Value)).ToList<IExpression>());
				}
			}
			listExpression = null;
			return false;
		}

		// Token: 0x06007D8A RID: 32138 RVA: 0x001AF074 File Offset: 0x001AD274
		private bool TryGetInvokeFieldValues(IInvocationExpression invocation, out IExpression record)
		{
			Value value;
			if (invocation.Function.TryGetConstant(out value) && value.IsFunction && value.AsFunction.FunctionIdentity.Equals(Library.Record.FieldValues) && invocation.Arguments.Count == 1)
			{
				record = invocation.Arguments[0];
				return true;
			}
			record = null;
			return false;
		}

		// Token: 0x06007D8B RID: 32139 RVA: 0x001AF0D1 File Offset: 0x001AD2D1
		IExpression IExpressionEnvironment.SetType(IExpression expression, TypeValue type)
		{
			return base.SetType(expression, type);
		}

		// Token: 0x06007D8C RID: 32140 RVA: 0x001AF0DC File Offset: 0x001AD2DC
		private static bool TryGetOffset(IExpression expression, out IExpression exprBeingOffset, out Value offset, out bool negateOperator)
		{
			negateOperator = false;
			if (expression.Kind == ExpressionKind.Binary)
			{
				IBinaryExpression binaryExpression = (IBinaryExpression)expression;
				bool flag = false;
				if (binaryExpression.Left.TryGetConstant(out offset) && offset.IsNumber)
				{
					exprBeingOffset = binaryExpression.Right;
				}
				else
				{
					if (!binaryExpression.Right.TryGetConstant(out offset) || !offset.IsNumber)
					{
						goto IL_007B;
					}
					exprBeingOffset = binaryExpression.Left;
					flag = true;
				}
				BinaryOperator2 @operator = binaryExpression.Operator;
				if (@operator == BinaryOperator2.Add)
				{
					return true;
				}
				if (@operator == BinaryOperator2.Subtract)
				{
					if (flag)
					{
						offset = offset.Negate();
					}
					else
					{
						negateOperator = true;
					}
					return true;
				}
			}
			IL_007B:
			exprBeingOffset = null;
			offset = null;
			return false;
		}

		// Token: 0x06007D8D RID: 32141 RVA: 0x001AF16C File Offset: 0x001AD36C
		private static bool TryReduceTextPositionOf(IBinaryExpression binary, BinaryOperator2 op, IConstantExpression function, out IExpression expression)
		{
			Value value;
			IList<IExpression> list;
			if (binary.Operator == op && binary.Right.TryGetConstant(out value) && value.IsNumber && value.AsNumber.Equals(NumberValue.Zero) && binary.Left.TryGetInvocation(Library.Text.PositionOf, 2, out list))
			{
				expression = new InvocationExpressionSyntaxNodeN(function, list, TokenRange.Null);
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x06007D8E RID: 32142 RVA: 0x001AF1D4 File Offset: 0x001AD3D4
		private static bool TryReduceIfInequality(IBinaryExpression binary, out IExpression expression)
		{
			IIfExpression ifExpression = binary.Left as IIfExpression;
			Value value;
			Value value2;
			Value value3;
			if (ifExpression != null && ifExpression.TrueCase.TryGetConstant(out value) && ifExpression.FalseCase.TryGetConstant(out value2) && binary.Right.TryGetConstant(out value3))
			{
				Value value4 = PartialEvaluationAlgebra.Binary(binary.Operator, value, value3);
				Value value5 = PartialEvaluationAlgebra.Binary(binary.Operator, value2, value3);
				if (value4.IsLogical && value5.IsLogical)
				{
					if (value4.AsBoolean)
					{
						if (value5.AsBoolean)
						{
							expression = ConstantExpressionSyntaxNode.True;
						}
						else
						{
							expression = ifExpression.Condition;
						}
					}
					else if (value5.AsBoolean)
					{
						expression = UnaryExpressionSyntaxNode.New(UnaryOperator2.Not, ifExpression.Condition, binary.Range);
					}
					else
					{
						expression = ConstantExpressionSyntaxNode.False;
					}
					return true;
				}
			}
			expression = null;
			return false;
		}

		// Token: 0x06007D8F RID: 32143 RVA: 0x001AF2A8 File Offset: 0x001AD4A8
		private static bool TryGetRecordAndFields(IExpression expression, int maxArguments, out IExpression record, out Keys fields)
		{
			Value value;
			if (expression.TryGetConstant(out value) && value.IsRecord && value.AsRecord.Count <= maxArguments)
			{
				record = expression;
				fields = value.AsRecord.Keys;
				return true;
			}
			IList<IExpression> list;
			if (expression.TryGetInvocation(Library.Record.SelectFields, 2, out list) && list[1].TryGetConstant(out value) && value.IsList && value.AsList.IsBuffered && value.AsList.Count <= maxArguments)
			{
				string[] array = new string[value.AsList.Count];
				for (int i = 0; i < array.Length; i++)
				{
					Value value2 = value.AsList[i];
					if (!value2.IsText)
					{
						array = null;
						break;
					}
					array[i] = value2.AsString;
				}
				if (array != null)
				{
					record = list[0];
					fields = Keys.New(array);
					return true;
				}
			}
			record = null;
			fields = null;
			return false;
		}

		// Token: 0x06007D90 RID: 32144 RVA: 0x001AF390 File Offset: 0x001AD590
		private static bool TryGetInvocation(IExpression expression, out FunctionValue function, out IExpression arg)
		{
			IInvocationExpression invocationExpression = expression as IInvocationExpression;
			Value value;
			if (invocationExpression != null && invocationExpression.Arguments.Count == 1 && invocationExpression.Function.TryGetConstant(out value) && value.IsFunction)
			{
				function = value.AsFunction;
				arg = invocationExpression.Arguments[0];
				return true;
			}
			function = null;
			arg = null;
			return false;
		}

		// Token: 0x06007D91 RID: 32145 RVA: 0x001AF3EC File Offset: 0x001AD5EC
		private static bool IsTemporalConversionFunction(ValueKind kind, FunctionValue function)
		{
			FunctionValue functionValue;
			if (!function.TryGetAs<FunctionValue>(out functionValue))
			{
				functionValue = null;
			}
			switch (kind)
			{
			case ValueKind.Date:
				return functionValue is Library.Date.FromFunctionValue;
			case ValueKind.DateTime:
				return functionValue is Library.DateTime.FromFunctionValue;
			case ValueKind.DateTimeZone:
				return functionValue is Library.DateTimeZone.FromFunctionValue;
			default:
				return false;
			}
		}

		// Token: 0x06007D92 RID: 32146 RVA: 0x001AF43A File Offset: 0x001AD63A
		private static bool IsTemporalConversionSupported(ValueKind kind, ValueKind fromKind)
		{
			if (kind - ValueKind.Date > 1)
			{
				if (kind == ValueKind.DateTimeZone)
				{
					if (fromKind == ValueKind.DateTime)
					{
						return true;
					}
					if (fromKind == ValueKind.DateTimeZone)
					{
						return true;
					}
				}
			}
			else
			{
				switch (fromKind)
				{
				case ValueKind.Date:
					return true;
				case ValueKind.DateTime:
					return true;
				case ValueKind.DateTimeZone:
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007D93 RID: 32147 RVA: 0x001AF474 File Offset: 0x001AD674
		private static bool TryGetTemporalConversionFunction(ValueKind kind, out FunctionValue function)
		{
			switch (kind)
			{
			case ValueKind.Date:
				function = new Library.Date.FromFunctionValue(EngineHost.Empty, ConstantFoldingVisitor.InvariantCulture.Instance);
				return true;
			case ValueKind.DateTime:
				function = new Library.DateTime.FromFunctionValue(EngineHost.Empty, ConstantFoldingVisitor.InvariantCulture.Instance);
				return true;
			case ValueKind.DateTimeZone:
				function = new Library.DateTimeZone.FromFunctionValue(EngineHost.Empty, ConstantFoldingVisitor.InvariantCulture.Instance);
				return true;
			default:
				function = null;
				return false;
			}
		}

		// Token: 0x06007D94 RID: 32148 RVA: 0x001AF4D4 File Offset: 0x001AD6D4
		private static bool AreSame(IExpression expr1, IExpression expr2)
		{
			if (expr1.Kind == ExpressionKind.FieldAccess && expr2.Kind == ExpressionKind.FieldAccess)
			{
				IFieldAccessExpression fieldAccessExpression = (IFieldAccessExpression)expr1;
				IFieldAccessExpression fieldAccessExpression2 = (IFieldAccessExpression)expr2;
				return fieldAccessExpression.MemberName == fieldAccessExpression2.MemberName && fieldAccessExpression.IsOptional == fieldAccessExpression2.IsOptional && ConstantFoldingVisitor.AreSame(fieldAccessExpression.Expression, fieldAccessExpression2.Expression);
			}
			return expr1 == expr2;
		}

		// Token: 0x04004518 RID: 17688
		private static readonly IConstantExpression textContains = new ConstantExpressionSyntaxNode(Library.Text.Contains);

		// Token: 0x04004519 RID: 17689
		private static readonly IConstantExpression textStartsWith = new ConstantExpressionSyntaxNode(Library.Text.StartsWith);

		// Token: 0x0400451A RID: 17690
		private const int MaxListSumOrProductArgumentsToExpand = 10;

		// Token: 0x020012AC RID: 4780
		private class InvariantCulture : ICulture
		{
			// Token: 0x06007D96 RID: 32150 RVA: 0x000020FD File Offset: 0x000002FD
			private InvariantCulture()
			{
			}

			// Token: 0x17002219 RID: 8729
			// (get) Token: 0x06007D97 RID: 32151 RVA: 0x001AF55A File Offset: 0x001AD75A
			public string Name
			{
				get
				{
					return "";
				}
			}

			// Token: 0x1700221A RID: 8730
			// (get) Token: 0x06007D98 RID: 32152 RVA: 0x00018E68 File Offset: 0x00017068
			public CultureInfo Value
			{
				get
				{
					return CultureInfo.InvariantCulture;
				}
			}

			// Token: 0x0400451B RID: 17691
			public static readonly ICulture Instance = new ConstantFoldingVisitor.InvariantCulture();
		}
	}
}
