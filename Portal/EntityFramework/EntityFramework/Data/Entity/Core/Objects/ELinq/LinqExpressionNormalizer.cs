using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000463 RID: 1123
	internal class LinqExpressionNormalizer : EntityExpressionVisitor
	{
		// Token: 0x06003740 RID: 14144 RVA: 0x000B32AC File Offset: 0x000B14AC
		internal override Expression VisitBinary(BinaryExpression b)
		{
			b = (BinaryExpression)base.VisitBinary(b);
			if (b.NodeType == ExpressionType.Equal)
			{
				Expression expression = LinqExpressionNormalizer.UnwrapObjectConvert(b.Left);
				Expression expression2 = LinqExpressionNormalizer.UnwrapObjectConvert(b.Right);
				if (expression != b.Left || expression2 != b.Right)
				{
					b = LinqExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, expression, expression2);
				}
			}
			LinqExpressionNormalizer.Pattern pattern;
			if (this._patterns.TryGetValue(b.Left, out pattern) && pattern.Kind == LinqExpressionNormalizer.PatternKind.Compare && LinqExpressionNormalizer.IsConstantZero(b.Right))
			{
				LinqExpressionNormalizer.ComparePattern comparePattern = (LinqExpressionNormalizer.ComparePattern)pattern;
				BinaryExpression binaryExpression;
				if (LinqExpressionNormalizer.TryCreateRelationalOperator(b.NodeType, comparePattern.Left, comparePattern.Right, out binaryExpression))
				{
					b = binaryExpression;
				}
			}
			return b;
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000B3358 File Offset: 0x000B1558
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

		// Token: 0x06003742 RID: 14146 RVA: 0x000B33F7 File Offset: 0x000B15F7
		private static bool IsConstantZero(Expression expression)
		{
			return expression.NodeType == ExpressionType.Constant && ((ConstantExpression)expression).Value.Equals(0);
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000B341C File Offset: 0x000B161C
		internal override Expression VisitMethodCall(MethodCallExpression m)
		{
			m = (MethodCallExpression)base.VisitMethodCall(m);
			if (m.Method.IsStatic)
			{
				if (m.Method.Name.StartsWith("op_", StringComparison.Ordinal))
				{
					if (m.Arguments.Count == 2)
					{
						string text = m.Method.Name;
						if (text != null)
						{
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
													return Expression.GreaterThan(m.Arguments[0], m.Arguments[1], false, m.Method);
												}
											}
										}
										else if (text == "op_Addition")
										{
											return Expression.Add(m.Arguments[0], m.Arguments[1], m.Method);
										}
									}
									else if (text == "op_BitwiseAnd")
									{
										return Expression.And(m.Arguments[0], m.Arguments[1], m.Method);
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
												return Expression.LessThan(m.Arguments[0], m.Arguments[1], false, m.Method);
											}
										}
									}
									else if (text == "op_LessThanOrEqual")
									{
										return Expression.LessThanOrEqual(m.Arguments[0], m.Arguments[1], false, m.Method);
									}
								}
								else if (num != 1516143579U)
								{
									if (num == 1915672496U)
									{
										if (text == "op_Division")
										{
											return Expression.Divide(m.Arguments[0], m.Arguments[1], m.Method);
										}
									}
								}
								else if (text == "op_Equality")
								{
									return Expression.Equal(m.Arguments[0], m.Arguments[1], false, m.Method);
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
												return Expression.GreaterThanOrEqual(m.Arguments[0], m.Arguments[1], false, m.Method);
											}
										}
									}
									else if (text == "op_Modulus")
									{
										return Expression.Modulo(m.Arguments[0], m.Arguments[1], m.Method);
									}
								}
								else if (text == "op_ExclusiveOr")
								{
									return Expression.ExclusiveOr(m.Arguments[0], m.Arguments[1], m.Method);
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
											return Expression.Subtract(m.Arguments[0], m.Arguments[1], m.Method);
										}
									}
								}
								else if (text == "op_Multiply")
								{
									return Expression.Multiply(m.Arguments[0], m.Arguments[1], m.Method);
								}
							}
							else if (num != 3492550567U)
							{
								if (num == 3794317784U)
								{
									if (text == "op_Inequality")
									{
										return Expression.NotEqual(m.Arguments[0], m.Arguments[1], false, m.Method);
									}
								}
							}
							else if (text == "op_BitwiseOr")
							{
								return Expression.Or(m.Arguments[0], m.Arguments[1], m.Method);
							}
						}
					}
					if (m.Arguments.Count == 1)
					{
						string text = m.Method.Name;
						if (text != null)
						{
							if (text == "op_UnaryNegation")
							{
								return Expression.Negate(m.Arguments[0], m.Method);
							}
							if (text == "op_UnaryPlus")
							{
								return Expression.UnaryPlus(m.Arguments[0], m.Method);
							}
							if (text == "op_Explicit" || text == "op_Implicit")
							{
								return Expression.Convert(m.Arguments[0], m.Type, m.Method);
							}
							if (text == "op_OnesComplement" || text == "op_False")
							{
								return Expression.Not(m.Arguments[0], m.Method);
							}
						}
					}
				}
				if (m.Method.Name == "Equals" && m.Arguments.Count > 1)
				{
					return Expression.Equal(m.Arguments[0], m.Arguments[1], false, m.Method);
				}
				if (m.Method.Name == "CompareString" && (m.Method.DeclaringType.FullName == "Microsoft.VisualBasic.CompilerServices.Operators" || m.Method.DeclaringType.FullName == "Microsoft.VisualBasic.CompilerServices.EmbeddedOperators"))
				{
					return this.CreateCompareExpression(m.Arguments[0], m.Arguments[1]);
				}
				if (m.Method.Name == "Compare" && m.Arguments.Count > 1 && m.Method.ReturnType == typeof(int))
				{
					return this.CreateCompareExpression(m.Arguments[0], m.Arguments[1]);
				}
			}
			else
			{
				if (m.Method.Name == "Equals" && m.Arguments.Count > 0)
				{
					Type parameterType = m.Method.GetParameters()[0].ParameterType;
					if (parameterType != typeof(DbGeography) && parameterType != typeof(DbGeometry))
					{
						return LinqExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, m.Object, m.Arguments[0]);
					}
				}
				if (m.Method.Name == "CompareTo" && m.Arguments.Count == 1 && m.Method.ReturnType == typeof(int))
				{
					return this.CreateCompareExpression(m.Object, m.Arguments[0]);
				}
				if (m.Method.Name == "Contains" && m.Arguments.Count == 1)
				{
					Type declaringType = m.Method.DeclaringType;
					MethodInfo methodInfo;
					if (declaringType.IsGenericType() && declaringType.GetGenericTypeDefinition() == typeof(List<>) && ReflectionUtil.TryLookupMethod(SequenceMethod.Contains, out methodInfo))
					{
						return Expression.Call(methodInfo.MakeGenericMethod(declaringType.GetGenericArguments()), m.Object, m.Arguments[0]);
					}
				}
			}
			return LinqExpressionNormalizer.NormalizePredicateArgument(m);
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000B3BD8 File Offset: 0x000B1DD8
		private static MethodCallExpression NormalizePredicateArgument(MethodCallExpression callExpression)
		{
			int num;
			Expression expression;
			MethodCallExpression methodCallExpression;
			if (LinqExpressionNormalizer.HasPredicateArgument(callExpression, out num) && LinqExpressionNormalizer.TryMatchCoalescePattern(callExpression.Arguments[num], out expression))
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

		// Token: 0x06003745 RID: 14149 RVA: 0x000B3C30 File Offset: 0x000B1E30
		private static bool HasPredicateArgument(MethodCallExpression callExpression, out int argumentOrdinal)
		{
			argumentOrdinal = 0;
			bool flag = false;
			SequenceMethod sequenceMethod;
			if (2 <= callExpression.Arguments.Count && ReflectionUtil.TryIdentifySequenceMethod(callExpression.Method, out sequenceMethod))
			{
				if (sequenceMethod <= SequenceMethod.TakeWhileOrdinal)
				{
					if (sequenceMethod > SequenceMethod.WhereOrdinal && sequenceMethod - SequenceMethod.TakeWhile > 1)
					{
						return flag;
					}
				}
				else if (sequenceMethod - SequenceMethod.SkipWhile > 1)
				{
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
				}
				argumentOrdinal = 1;
				flag = true;
			}
			return flag;
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000B3CDC File Offset: 0x000B1EDC
		private static bool TryMatchCoalescePattern(Expression expression, out Expression normalized)
		{
			normalized = null;
			bool flag = false;
			if (expression.NodeType == ExpressionType.Quote)
			{
				if (LinqExpressionNormalizer.TryMatchCoalescePattern(((UnaryExpression)expression).Operand, out normalized))
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

		// Token: 0x06003747 RID: 14151 RVA: 0x000B3DBD File Offset: 0x000B1FBD
		private static bool RelationalOperatorPlaceholder<TLeft, TRight>(TLeft left, TRight right)
		{
			return left == right;
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000B3DD0 File Offset: 0x000B1FD0
		private static BinaryExpression CreateRelationalOperator(ExpressionType op, Expression left, Expression right)
		{
			BinaryExpression binaryExpression;
			LinqExpressionNormalizer.TryCreateRelationalOperator(op, left, right, out binaryExpression);
			return binaryExpression;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000B3DEC File Offset: 0x000B1FEC
		private static bool TryCreateRelationalOperator(ExpressionType op, Expression left, Expression right, out BinaryExpression result)
		{
			MethodInfo methodInfo = LinqExpressionNormalizer.RelationalOperatorPlaceholderMethod.MakeGenericMethod(new Type[] { left.Type, right.Type });
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

		// Token: 0x0600374A RID: 14154 RVA: 0x000B3EA4 File Offset: 0x000B20A4
		private Expression CreateCompareExpression(Expression left, Expression right)
		{
			Expression expression = Expression.Condition(LinqExpressionNormalizer.CreateRelationalOperator(ExpressionType.Equal, left, right), Expression.Constant(0), Expression.Condition(LinqExpressionNormalizer.CreateRelationalOperator(ExpressionType.GreaterThan, left, right), Expression.Constant(1), Expression.Constant(-1)));
			this._patterns[expression] = new LinqExpressionNormalizer.ComparePattern(left, right);
			return expression;
		}

		// Token: 0x04001211 RID: 4625
		private const bool LiftToNull = false;

		// Token: 0x04001212 RID: 4626
		private readonly Dictionary<Expression, LinqExpressionNormalizer.Pattern> _patterns = new Dictionary<Expression, LinqExpressionNormalizer.Pattern>();

		// Token: 0x04001213 RID: 4627
		internal static readonly MethodInfo RelationalOperatorPlaceholderMethod = typeof(LinqExpressionNormalizer).GetOnlyDeclaredMethod("RelationalOperatorPlaceholder");

		// Token: 0x02000AAA RID: 2730
		private abstract class Pattern
		{
			// Token: 0x170010C0 RID: 4288
			// (get) Token: 0x06006292 RID: 25234
			internal abstract LinqExpressionNormalizer.PatternKind Kind { get; }
		}

		// Token: 0x02000AAB RID: 2731
		private enum PatternKind
		{
			// Token: 0x04002B47 RID: 11079
			Compare
		}

		// Token: 0x02000AAC RID: 2732
		private sealed class ComparePattern : LinqExpressionNormalizer.Pattern
		{
			// Token: 0x06006294 RID: 25236 RVA: 0x00156661 File Offset: 0x00154861
			internal ComparePattern(Expression left, Expression right)
			{
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x170010C1 RID: 4289
			// (get) Token: 0x06006295 RID: 25237 RVA: 0x00156677 File Offset: 0x00154877
			internal override LinqExpressionNormalizer.PatternKind Kind
			{
				get
				{
					return LinqExpressionNormalizer.PatternKind.Compare;
				}
			}

			// Token: 0x04002B48 RID: 11080
			internal readonly Expression Left;

			// Token: 0x04002B49 RID: 11081
			internal readonly Expression Right;
		}
	}
}
