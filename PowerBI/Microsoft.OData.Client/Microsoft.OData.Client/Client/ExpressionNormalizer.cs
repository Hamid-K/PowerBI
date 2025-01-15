using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x0200009D RID: 157
	internal class ExpressionNormalizer : DataServiceALinqExpressionVisitor
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00011BC9 File Offset: 0x0000FDC9
		private ExpressionNormalizer(Dictionary<Expression, Expression> normalizerRewrites)
		{
			this.normalizerRewrites = normalizerRewrites;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		internal Dictionary<Expression, Expression> NormalizerRewrites
		{
			get
			{
				return this.normalizerRewrites;
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		internal static Expression Normalize(Expression expression, Dictionary<Expression, Expression> rewrites)
		{
			ExpressionNormalizer expressionNormalizer = new ExpressionNormalizer(rewrites);
			return expressionNormalizer.Visit(expression);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00011C10 File Offset: 0x0000FE10
		internal override Expression VisitBinary(BinaryExpression b)
		{
			BinaryExpression binaryExpression = (BinaryExpression)base.VisitBinary(b);
			if (binaryExpression.NodeType == ExpressionType.Equal)
			{
				Expression expression = ExpressionNormalizer.UnwrapObjectConvert(binaryExpression.Left);
				Expression expression2 = ExpressionNormalizer.UnwrapObjectConvert(binaryExpression.Right);
				if (expression != binaryExpression.Left || expression2 != binaryExpression.Right)
				{
					binaryExpression = ExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, expression, expression2);
				}
			}
			ExpressionNormalizer.Pattern pattern;
			if (this._patterns.TryGetValue(binaryExpression.Left, out pattern) && pattern.Kind == ExpressionNormalizer.PatternKind.Compare && ExpressionNormalizer.IsConstantZero(binaryExpression.Right))
			{
				ExpressionNormalizer.ComparePattern comparePattern = (ExpressionNormalizer.ComparePattern)pattern;
				BinaryExpression binaryExpression2;
				if (ExpressionNormalizer.TryCreateRelationalOperator(binaryExpression.NodeType, comparePattern.Left, comparePattern.Right, out binaryExpression2))
				{
					binaryExpression = binaryExpression2;
				}
			}
			this.RecordRewrite(b, binaryExpression);
			return binaryExpression;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00011CC4 File Offset: 0x0000FEC4
		internal override Expression VisitUnary(UnaryExpression u)
		{
			UnaryExpression unaryExpression = (UnaryExpression)base.VisitUnary(u);
			Expression expression = unaryExpression;
			this.RecordRewrite(u, expression);
			if ((unaryExpression.NodeType == ExpressionType.Convert || unaryExpression.NodeType == ExpressionType.TypeAs) && unaryExpression.Type.IsAssignableFrom(unaryExpression.Operand.Type) && ((!PrimitiveType.IsKnownNullableType(unaryExpression.Operand.Type) && !PrimitiveType.IsKnownNullableType(unaryExpression.Type)) || unaryExpression.Operand.Type == unaryExpression.Type) && (!ClientTypeUtil.TypeOrElementTypeIsEntity(unaryExpression.Operand.Type) || !ProjectionAnalyzer.IsCollectionProducingExpression(unaryExpression.Operand)) && !unaryExpression.Operand.Type.IsEnum())
			{
				expression = unaryExpression.Operand;
			}
			return expression;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00011D88 File Offset: 0x0000FF88
		private static Expression UnwrapObjectConvert(Expression input)
		{
			if (input.NodeType == ExpressionType.Constant && input.Type == typeof(object))
			{
				ConstantExpression constantExpression = (ConstantExpression)input;
				if (constantExpression.Value != null && constantExpression.Value.GetType() != typeof(object))
				{
					return Expression.Constant(constantExpression.Value, constantExpression.Value.GetType());
				}
			}
			while (ExpressionType.Convert == input.NodeType && typeof(object) == input.Type)
			{
				input = ((UnaryExpression)input).Operand;
			}
			return input;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00011E27 File Offset: 0x00010027
		private static bool IsConstantZero(Expression expression)
		{
			return expression.NodeType == ExpressionType.Constant && ((ConstantExpression)expression).Value.Equals(0);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00011E4C File Offset: 0x0001004C
		internal override Expression VisitMethodCall(MethodCallExpression call)
		{
			Expression expression = this.VisitMethodCallNoRewrite(call);
			this.RecordRewrite(call, expression);
			return expression;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00011E6C File Offset: 0x0001006C
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Large switch is necessary")]
		internal Expression VisitMethodCallNoRewrite(MethodCallExpression call)
		{
			MethodCallExpression methodCallExpression = (MethodCallExpression)base.VisitMethodCall(call);
			if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name.StartsWith("op_", StringComparison.Ordinal))
			{
				if (methodCallExpression.Arguments.Count == 2)
				{
					string text = methodCallExpression.Method.Name;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 1915672496U)
					{
						if (num <= 1195761148U)
						{
							if (num != 835846267U)
							{
								if (num != 906583475U)
								{
									if (num == 1195761148U)
									{
										if (text == "op_GreaterThan")
										{
											return Expression.GreaterThan(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
										}
									}
								}
								else if (text == "op_Addition")
								{
									return Expression.Add(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
								}
							}
							else if (text == "op_BitwiseAnd")
							{
								return Expression.And(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
							}
						}
						else if (num <= 1258540185U)
						{
							if (num != 1234170120U)
							{
								if (num == 1258540185U)
								{
									if (text == "op_LessThan")
									{
										return Expression.LessThan(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
									}
								}
							}
							else if (text == "op_LessThanOrEqual")
							{
								return Expression.LessThanOrEqual(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
							}
						}
						else if (num != 1516143579U)
						{
							if (num == 1915672496U)
							{
								if (text == "op_Division")
								{
									return Expression.Divide(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
								}
							}
						}
						else if (text == "op_Equality")
						{
							return Expression.Equal(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
						}
					}
					else if (num <= 2459852411U)
					{
						if (num != 2366795836U)
						{
							if (num != 2429678952U)
							{
								if (num == 2459852411U)
								{
									if (text == "op_GreaterThanOrEqual")
									{
										return Expression.GreaterThanOrEqual(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
									}
								}
							}
							else if (text == "op_Modulus")
							{
								return Expression.Modulo(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
							}
						}
						else if (text == "op_ExclusiveOr")
						{
							return Expression.ExclusiveOr(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
						}
					}
					else if (num <= 3279419199U)
					{
						if (num != 2958252495U)
						{
							if (num == 3279419199U)
							{
								if (text == "op_Subtraction")
								{
									return Expression.Subtract(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
								}
							}
						}
						else if (text == "op_Multiply")
						{
							return Expression.Multiply(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
						}
					}
					else if (num != 3492550567U)
					{
						if (num == 3794317784U)
						{
							if (text == "op_Inequality")
							{
								return Expression.NotEqual(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
							}
						}
					}
					else if (text == "op_BitwiseOr")
					{
						return Expression.Or(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], methodCallExpression.Method);
					}
				}
				if (methodCallExpression.Arguments.Count == 1)
				{
					string text = methodCallExpression.Method.Name;
					if (text == "op_UnaryNegation")
					{
						return Expression.Negate(methodCallExpression.Arguments[0], methodCallExpression.Method);
					}
					if (text == "op_UnaryPlus")
					{
						return Expression.UnaryPlus(methodCallExpression.Arguments[0], methodCallExpression.Method);
					}
					if (text == "op_Explicit" || text == "op_Implicit")
					{
						return Expression.Convert(methodCallExpression.Arguments[0], methodCallExpression.Type, methodCallExpression.Method);
					}
					if (text == "op_OnesComplement" || text == "op_False")
					{
						return Expression.Not(methodCallExpression.Arguments[0], methodCallExpression.Method);
					}
				}
			}
			if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name == "Equals" && methodCallExpression.Arguments.Count > 1)
			{
				return Expression.Equal(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1], false, methodCallExpression.Method);
			}
			if (!methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name == "Equals" && methodCallExpression.Arguments.Count > 0)
			{
				return ExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, methodCallExpression.Object, methodCallExpression.Arguments[0]);
			}
			if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name == "CompareString" && methodCallExpression.Method.DeclaringType.FullName == "Microsoft.VisualBasic.CompilerServices.Operators")
			{
				return this.CreateCompareExpression(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1]);
			}
			if (!methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name == "CompareTo" && methodCallExpression.Arguments.Count == 1 && methodCallExpression.Method.ReturnType == typeof(int))
			{
				return this.CreateCompareExpression(methodCallExpression.Object, methodCallExpression.Arguments[0]);
			}
			if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.Name == "Compare" && methodCallExpression.Arguments.Count > 1 && methodCallExpression.Method.ReturnType == typeof(int))
			{
				return this.CreateCompareExpression(methodCallExpression.Arguments[0], methodCallExpression.Arguments[1]);
			}
			MethodCallExpression methodCallExpression2 = ExpressionNormalizer.NormalizePredicateArgument(methodCallExpression);
			methodCallExpression2 = ExpressionNormalizer.NormalizeSelectWithTypeCast(methodCallExpression2);
			return ExpressionNormalizer.NormalizeEnumerableSource(methodCallExpression2);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00012590 File Offset: 0x00010790
		private static MethodCallExpression NormalizeEnumerableSource(MethodCallExpression callExpression)
		{
			MethodInfo method = callExpression.Method;
			SequenceMethod sequenceMethod;
			if (ReflectionUtil.TryIdentifySequenceMethod(callExpression.Method, out sequenceMethod) && (ReflectionUtil.IsAnyAllMethod(sequenceMethod) || sequenceMethod == SequenceMethod.OfType))
			{
				Expression expression = callExpression.Arguments[0];
				while (ExpressionType.Convert == expression.NodeType)
				{
					expression = ((UnaryExpression)expression).Operand;
				}
				if (expression != callExpression.Arguments[0])
				{
					if (sequenceMethod == SequenceMethod.Any || sequenceMethod == SequenceMethod.OfType)
					{
						return Expression.Call(method, expression);
					}
					return Expression.Call(method, expression, callExpression.Arguments[1]);
				}
			}
			return callExpression;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001261C File Offset: 0x0001081C
		private static MethodCallExpression NormalizePredicateArgument(MethodCallExpression callExpression)
		{
			int num;
			Expression expression;
			MethodCallExpression methodCallExpression;
			if (ExpressionNormalizer.HasPredicateArgument(callExpression, out num) && ExpressionNormalizer.TryMatchCoalescePattern(callExpression.Arguments[num], out expression))
			{
				List<Expression> list = new List<Expression>(callExpression.Arguments);
				list[num] = expression;
				methodCallExpression = Expression.Call(callExpression.Object, callExpression.Method, list);
			}
			else
			{
				methodCallExpression = callExpression;
			}
			return methodCallExpression;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00012674 File Offset: 0x00010874
		private static bool HasPredicateArgument(MethodCallExpression callExpression, out int argumentOrdinal)
		{
			argumentOrdinal = 0;
			bool flag = false;
			SequenceMethod sequenceMethod;
			if (2 <= callExpression.Arguments.Count && ReflectionUtil.TryIdentifySequenceMethod(callExpression.Method, out sequenceMethod))
			{
				if (sequenceMethod <= SequenceMethod.WhereOrdinal)
				{
					if (sequenceMethod != SequenceMethod.Where && sequenceMethod != SequenceMethod.WhereOrdinal)
					{
						return flag;
					}
				}
				else
				{
					switch (sequenceMethod)
					{
					case SequenceMethod.TakeWhile:
					case SequenceMethod.TakeWhileOrdinal:
					case SequenceMethod.SkipWhile:
					case SequenceMethod.SkipWhileOrdinal:
						break;
					case SequenceMethod.Skip:
						return flag;
					default:
						switch (sequenceMethod)
						{
						case SequenceMethod.FirstPredicate:
						case SequenceMethod.FirstOrDefaultPredicate:
						case SequenceMethod.LastPredicate:
						case SequenceMethod.LastOrDefaultPredicate:
						case SequenceMethod.SinglePredicate:
						case SequenceMethod.SingleOrDefaultPredicate:
							break;
						case SequenceMethod.FirstOrDefault:
						case SequenceMethod.Last:
						case SequenceMethod.LastOrDefault:
						case SequenceMethod.Single:
						case SequenceMethod.SingleOrDefault:
							return flag;
						default:
							switch (sequenceMethod)
							{
							case SequenceMethod.AnyPredicate:
							case SequenceMethod.All:
							case SequenceMethod.CountPredicate:
							case SequenceMethod.LongCountPredicate:
								break;
							case SequenceMethod.Count:
							case SequenceMethod.LongCount:
								return flag;
							default:
								return flag;
							}
							break;
						}
						break;
					}
				}
				argumentOrdinal = 1;
				flag = true;
			}
			return flag;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00012734 File Offset: 0x00010934
		private static bool TryMatchCoalescePattern(Expression expression, out Expression normalized)
		{
			normalized = null;
			bool flag = false;
			if (expression.NodeType == ExpressionType.Quote)
			{
				UnaryExpression unaryExpression = (UnaryExpression)expression;
				if (ExpressionNormalizer.TryMatchCoalescePattern(unaryExpression.Operand, out normalized))
				{
					flag = true;
					normalized = Expression.Quote(normalized);
				}
			}
			else if (expression.NodeType == ExpressionType.Lambda)
			{
				LambdaExpression lambdaExpression = (LambdaExpression)expression;
				if (lambdaExpression.Body.NodeType == ExpressionType.Coalesce && lambdaExpression.Body.Type == typeof(bool))
				{
					BinaryExpression binaryExpression = (BinaryExpression)lambdaExpression.Body;
					if (binaryExpression.Right.NodeType == ExpressionType.Constant && false.Equals(((ConstantExpression)binaryExpression.Right).Value))
					{
						normalized = Expression.Lambda(lambdaExpression.Type, Expression.Convert(binaryExpression.Left, typeof(bool)), lambdaExpression.Parameters);
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00012818 File Offset: 0x00010A18
		private static MethodCallExpression NormalizeSelectWithTypeCast(MethodCallExpression callExpression)
		{
			Type type;
			if (ExpressionNormalizer.TryMatchSelectWithConvert(callExpression, out type))
			{
				MethodInfo method = callExpression.Method.DeclaringType.GetMethod("Cast", true, true);
				if (method != null && method.IsGenericMethodDefinition && ReflectionUtil.IsSequenceMethod(method, SequenceMethod.Cast))
				{
					MethodInfo methodInfo = method.MakeGenericMethod(new Type[] { type });
					return Expression.Call(methodInfo, callExpression.Arguments[0]);
				}
			}
			return callExpression;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00012886 File Offset: 0x00010A86
		private static bool TryMatchSelectWithConvert(MethodCallExpression callExpression, out Type convertType)
		{
			convertType = null;
			return ReflectionUtil.IsSequenceMethod(callExpression.Method, SequenceMethod.Select) && ExpressionNormalizer.TryMatchConvertSingleArgument(callExpression.Arguments[1], out convertType);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000128B0 File Offset: 0x00010AB0
		private static bool TryMatchConvertSingleArgument(Expression expression, out Type convertType)
		{
			convertType = null;
			expression = ((expression.NodeType == ExpressionType.Quote) ? ((UnaryExpression)expression).Operand : expression);
			if (expression.NodeType == ExpressionType.Lambda)
			{
				LambdaExpression lambdaExpression = (LambdaExpression)expression;
				if (lambdaExpression.Parameters.Count == 1 && lambdaExpression.Body.NodeType == ExpressionType.Convert)
				{
					UnaryExpression unaryExpression = (UnaryExpression)lambdaExpression.Body;
					if (unaryExpression.Operand == lambdaExpression.Parameters[0])
					{
						convertType = unaryExpression.Type;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00012933 File Offset: 0x00010B33
		private static bool RelationalOperatorPlaceholder<TLeft, TRight>(TLeft left, TRight right)
		{
			return left == right;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00012944 File Offset: 0x00010B44
		private static BinaryExpression CreateRelationalOperator(ExpressionType op, Expression left, Expression right)
		{
			BinaryExpression binaryExpression;
			ExpressionNormalizer.TryCreateRelationalOperator(op, left, right, out binaryExpression);
			return binaryExpression;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00012960 File Offset: 0x00010B60
		private static bool TryCreateRelationalOperator(ExpressionType op, Expression left, Expression right, out BinaryExpression result)
		{
			MethodInfo methodInfo = ExpressionNormalizer.s_relationalOperatorPlaceholderMethod.MakeGenericMethod(new Type[] { left.Type, right.Type });
			switch (op)
			{
			case ExpressionType.Equal:
				result = Expression.Equal(left, right, false, methodInfo);
				return true;
			case ExpressionType.ExclusiveOr:
			case ExpressionType.Invoke:
			case ExpressionType.Lambda:
			case ExpressionType.LeftShift:
				break;
			case ExpressionType.GreaterThan:
				result = Expression.GreaterThan(left, right, false, methodInfo);
				return true;
			case ExpressionType.GreaterThanOrEqual:
				result = Expression.GreaterThanOrEqual(left, right, false, methodInfo);
				return true;
			case ExpressionType.LessThan:
				result = Expression.LessThan(left, right, false, methodInfo);
				return true;
			case ExpressionType.LessThanOrEqual:
				result = Expression.LessThanOrEqual(left, right, false, methodInfo);
				return true;
			default:
				if (op == ExpressionType.NotEqual)
				{
					result = Expression.NotEqual(left, right, false, methodInfo);
					return true;
				}
				break;
			}
			result = null;
			return false;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00012A18 File Offset: 0x00010C18
		private Expression CreateCompareExpression(Expression left, Expression right)
		{
			Expression expression = Expression.Condition(ExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, left, right), Expression.Constant(0), Expression.Condition(ExpressionNormalizer.CreateRelationalOperator(ExpressionType.GreaterThan, left, right), Expression.Constant(1), Expression.Constant(-1)));
			this._patterns[expression] = new ExpressionNormalizer.ComparePattern(left, right);
			return expression;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00012A77 File Offset: 0x00010C77
		private void RecordRewrite(Expression source, Expression rewritten)
		{
			if (source != rewritten)
			{
				this.NormalizerRewrites.Add(rewritten, source);
			}
		}

		// Token: 0x0400021C RID: 540
		private const bool LiftToNull = false;

		// Token: 0x0400021D RID: 541
		private readonly Dictionary<Expression, ExpressionNormalizer.Pattern> _patterns = new Dictionary<Expression, ExpressionNormalizer.Pattern>(ReferenceEqualityComparer<Expression>.Instance);

		// Token: 0x0400021E RID: 542
		private readonly Dictionary<Expression, Expression> normalizerRewrites;

		// Token: 0x0400021F RID: 543
		private static readonly MethodInfo s_relationalOperatorPlaceholderMethod = typeof(ExpressionNormalizer).GetMethod("RelationalOperatorPlaceholder", false, true);

		// Token: 0x0200017C RID: 380
		private abstract class Pattern
		{
			// Token: 0x1700035C RID: 860
			// (get) Token: 0x06000DCA RID: 3530
			internal abstract ExpressionNormalizer.PatternKind Kind { get; }
		}

		// Token: 0x0200017D RID: 381
		private enum PatternKind
		{
			// Token: 0x04000743 RID: 1859
			Compare
		}

		// Token: 0x0200017E RID: 382
		private sealed class ComparePattern : ExpressionNormalizer.Pattern
		{
			// Token: 0x06000DCC RID: 3532 RVA: 0x0002F975 File Offset: 0x0002DB75
			internal ComparePattern(Expression left, Expression right)
			{
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x1700035D RID: 861
			// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00015066 File Offset: 0x00013266
			internal override ExpressionNormalizer.PatternKind Kind
			{
				get
				{
					return ExpressionNormalizer.PatternKind.Compare;
				}
			}

			// Token: 0x04000744 RID: 1860
			internal readonly Expression Left;

			// Token: 0x04000745 RID: 1861
			internal readonly Expression Right;
		}
	}
}
