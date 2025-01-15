using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D3E RID: 3390
	internal sealed class QueryExpressionTranslator
	{
		// Token: 0x06005B13 RID: 23315 RVA: 0x0013DFB4 File Offset: 0x0013C1B4
		public QueryExpressionTranslator(ICubeMetadataProvider metadata)
		{
			this.handlers = new Dictionary<Value, Func<InvocationQueryExpression, CubeExpression>>
			{
				{
					CubeModule.Cube.AttributeMemberId,
					new Func<InvocationQueryExpression, CubeExpression>(this.VisitAttributeMemberId)
				},
				{
					CubeModule.Cube.AttributeMemberProperty,
					new Func<InvocationQueryExpression, CubeExpression>(this.VisitAttributeMemberProperty)
				},
				{
					CubeModule.Cube.PropertyKey,
					new Func<InvocationQueryExpression, CubeExpression>(this.VisitPropertyKey)
				},
				{
					CubeModule.Cube.MeasureProperty,
					new Func<InvocationQueryExpression, CubeExpression>(this.VisitMeasureProperty)
				},
				{
					Library.List.Contains,
					new Func<InvocationQueryExpression, CubeExpression>(this.VisitListContains)
				},
				{
					Library._Value.NullableEquals,
					new Func<InvocationQueryExpression, CubeExpression>(this.VisitValueNullableEquals)
				},
				{
					Library._Value.Equals,
					new Func<InvocationQueryExpression, CubeExpression>(this.VisitValueEquals)
				},
				{
					Library.Text.Contains,
					new Func<InvocationQueryExpression, CubeExpression>(this.VisitTextContainsOrStartsWith)
				},
				{
					Library.Text.StartsWith,
					new Func<InvocationQueryExpression, CubeExpression>(this.VisitTextContainsOrStartsWith)
				}
			};
			this.metadata = metadata;
		}

		// Token: 0x06005B14 RID: 23316 RVA: 0x0013E0A8 File Offset: 0x0013C2A8
		public CubeExpression Translate(QueryExpression expression)
		{
			return this.Visit(expression);
		}

		// Token: 0x06005B15 RID: 23317 RVA: 0x0013E0B4 File Offset: 0x0013C2B4
		private CubeExpression Visit(QueryExpression expression)
		{
			switch (expression.Kind)
			{
			case QueryExpressionKind.Binary:
				return this.VisitBinary((BinaryQueryExpression)expression);
			case QueryExpressionKind.Constant:
				return this.VisitConstant((ConstantQueryExpression)expression);
			case QueryExpressionKind.ColumnAccess:
				return this.VisitColumnAccess((ColumnAccessQueryExpression)expression);
			case QueryExpressionKind.If:
				return this.VisitIf((IfQueryExpression)expression);
			case QueryExpressionKind.Invocation:
				return this.VisitInvocation((InvocationQueryExpression)expression);
			case QueryExpressionKind.Unary:
				return this.VisitUnary((UnaryQueryExpression)expression);
			default:
				return null;
			}
		}

		// Token: 0x06005B16 RID: 23318 RVA: 0x0013E138 File Offset: 0x0013C338
		private BinaryQueryExpression ReduceBinary(BinaryQueryExpression binary)
		{
			BinaryOperator2 binaryOperator = binary.Operator;
			QueryExpression queryExpression = binary.Left;
			QueryExpression queryExpression2 = binary.Right;
			QueryExpression queryExpression5;
			ConstantQueryExpression constantQueryExpression;
			Value value3;
			if (binaryOperator - BinaryOperator2.GreaterThan > 5)
			{
				if (binaryOperator == BinaryOperator2.And)
				{
					QueryExpression queryExpression3;
					BinaryOperator2 binaryOperator2;
					Value value;
					QueryExpression queryExpression4;
					BinaryOperator2 binaryOperator3;
					Value value2;
					if (QueryExpressionTranslator.TryGetConstantComparison(queryExpression, out queryExpression3, out binaryOperator2, out value) && QueryExpressionTranslator.TryGetConstantComparison(queryExpression2, out queryExpression4, out binaryOperator3, out value2) && queryExpression3.Equals(queryExpression4) && !value.Equals(value2))
					{
						if (binaryOperator2 == BinaryOperator2.Equals && binaryOperator3 == BinaryOperator2.NotEquals)
						{
							queryExpression = queryExpression3;
							queryExpression2 = new ConstantQueryExpression(value);
							binaryOperator = binaryOperator2;
						}
						else if (binaryOperator2 == BinaryOperator2.NotEquals && binaryOperator3 == BinaryOperator2.Equals)
						{
							queryExpression = queryExpression4;
							queryExpression2 = new ConstantQueryExpression(value2);
							binaryOperator = binaryOperator3;
						}
					}
				}
			}
			else if ((QueryExpressionTranslator.TryGetDefaultIfNullTest(queryExpression, out queryExpression5, out constantQueryExpression) && queryExpression2.TryGetConstant(out value3)) || (QueryExpressionTranslator.TryGetDefaultIfNullTest(queryExpression2, out queryExpression5, out constantQueryExpression) && queryExpression.TryGetConstant(out value3)))
			{
				Value value4 = PartialEvaluationAlgebra.Binary(binaryOperator, constantQueryExpression.Value, value3);
				if (value4.IsLogical && !value4.AsBoolean)
				{
					queryExpression = new BinaryQueryExpression(BinaryOperator2.NotEquals, queryExpression5, ConstantQueryExpression.Null);
					queryExpression2 = new BinaryQueryExpression(binaryOperator, queryExpression5, new ConstantQueryExpression(value3));
					binaryOperator = BinaryOperator2.And;
				}
			}
			if (binaryOperator == binary.Operator && queryExpression == binary.Left && queryExpression2 == binary.Right)
			{
				return binary;
			}
			return this.ReduceBinary(new BinaryQueryExpression(binaryOperator, queryExpression, queryExpression2));
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x0013E27C File Offset: 0x0013C47C
		private CubeExpression VisitBinary(BinaryQueryExpression binary)
		{
			binary = this.ReduceBinary(binary);
			CubeExpression cubeExpression = this.Visit(binary.Left);
			CubeExpression cubeExpression2 = this.Visit(binary.Right);
			if (cubeExpression != null && cubeExpression2 != null)
			{
				return new BinaryCubeExpression(binary.Operator, cubeExpression, cubeExpression2);
			}
			return null;
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x0013E2C1 File Offset: 0x0013C4C1
		private IdentifierCubeExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
		{
			return this.metadata.GetIdentifier(columnAccess.Column);
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x0013E2D4 File Offset: 0x0013C4D4
		private ConstantCubeExpression VisitConstant(ConstantQueryExpression constant)
		{
			return new ConstantCubeExpression(constant.Value);
		}

		// Token: 0x06005B1A RID: 23322 RVA: 0x0013E2E4 File Offset: 0x0013C4E4
		private CubeExpression VisitIf(IfQueryExpression @if)
		{
			CubeExpression cubeExpression = this.Visit(@if.Condition);
			CubeExpression cubeExpression2 = this.Visit(@if.TrueCase);
			CubeExpression cubeExpression3 = this.Visit(@if.FalseCase);
			if (cubeExpression != null && cubeExpression2 != null && cubeExpression3 != null)
			{
				return new IfCubeExpression(cubeExpression, cubeExpression2, cubeExpression3);
			}
			return null;
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x0013E32C File Offset: 0x0013C52C
		private CubeExpression VisitInvocation(InvocationQueryExpression invocation)
		{
			CubeExpression cubeExpression = this.Visit(invocation.Function);
			if (cubeExpression.Kind == CubeExpressionKind.Constant)
			{
				ConstantCubeExpression constantCubeExpression = (ConstantCubeExpression)cubeExpression;
				Func<InvocationQueryExpression, CubeExpression> func;
				if (this.handlers.TryGetValue(constantCubeExpression.Value, out func))
				{
					return func(invocation);
				}
			}
			CubeExpression[] array = new CubeExpression[invocation.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Visit(invocation.Arguments[i]);
				if (array[i] == null)
				{
					return null;
				}
			}
			return new InvocationCubeExpression(cubeExpression, array);
		}

		// Token: 0x06005B1C RID: 23324 RVA: 0x0013E3BC File Offset: 0x0013C5BC
		private CubeExpression VisitUnary(UnaryQueryExpression unary)
		{
			CubeExpression cubeExpression = this.Visit(unary.Expression);
			if (cubeExpression != null && unary.Operator == UnaryOperator2.Not)
			{
				if (cubeExpression.Kind == CubeExpressionKind.Binary)
				{
					BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)cubeExpression;
					BinaryOperator2? binaryOperator = null;
					switch (binaryCubeExpression.Operator)
					{
					case BinaryOperator2.GreaterThan:
						binaryOperator = new BinaryOperator2?(BinaryOperator2.LessThanOrEquals);
						break;
					case BinaryOperator2.LessThan:
						binaryOperator = new BinaryOperator2?(BinaryOperator2.GreaterThanOrEquals);
						break;
					case BinaryOperator2.GreaterThanOrEquals:
						binaryOperator = new BinaryOperator2?(BinaryOperator2.LessThan);
						break;
					case BinaryOperator2.LessThanOrEquals:
						binaryOperator = new BinaryOperator2?(BinaryOperator2.GreaterThan);
						break;
					case BinaryOperator2.Equals:
						binaryOperator = new BinaryOperator2?(BinaryOperator2.NotEquals);
						break;
					case BinaryOperator2.NotEquals:
						binaryOperator = new BinaryOperator2?(BinaryOperator2.Equals);
						break;
					}
					if (binaryOperator != null)
					{
						return new BinaryCubeExpression(binaryOperator.Value, binaryCubeExpression.Left, binaryCubeExpression.Right);
					}
				}
				return QueryExpressionTranslator.NewNot(cubeExpression);
			}
			return null;
		}

		// Token: 0x06005B1D RID: 23325 RVA: 0x0013E490 File Offset: 0x0013C690
		private CubeExpression VisitAttributeMemberId(InvocationQueryExpression invocation)
		{
			if (invocation.Arguments.Count == 1)
			{
				ColumnAccessQueryExpression columnAccessQueryExpression = invocation.Arguments[0] as ColumnAccessQueryExpression;
				if (columnAccessQueryExpression != null)
				{
					IdentifierCubeExpression identifierCubeExpression = this.VisitColumnAccess(columnAccessQueryExpression);
					if (this.metadata.IsDimensionAttribute(identifierCubeExpression))
					{
						return this.metadata.GetProperty(identifierCubeExpression, CubePropertyKind.UniqueId, null);
					}
				}
			}
			return null;
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x0013E4E8 File Offset: 0x0013C6E8
		private CubeExpression VisitAttributeMemberProperty(InvocationQueryExpression invocation)
		{
			if (invocation.Arguments.Count == 2)
			{
				ColumnAccessQueryExpression columnAccessQueryExpression = invocation.Arguments[0] as ColumnAccessQueryExpression;
				ConstantQueryExpression constantQueryExpression = invocation.Arguments[1] as ConstantQueryExpression;
				if (columnAccessQueryExpression != null && constantQueryExpression != null)
				{
					IdentifierCubeExpression identifierCubeExpression = this.VisitColumnAccess(columnAccessQueryExpression);
					if (this.metadata.IsDimensionAttribute(identifierCubeExpression) && constantQueryExpression.Value.IsText)
					{
						return this.metadata.GetProperty(identifierCubeExpression, CubePropertyKind.UserDefined, constantQueryExpression.Value.AsString);
					}
				}
			}
			return null;
		}

		// Token: 0x06005B1F RID: 23327 RVA: 0x0013E56C File Offset: 0x0013C76C
		private CubeExpression VisitPropertyKey(InvocationQueryExpression invocation)
		{
			if (invocation.Arguments.Count == 1)
			{
				ColumnAccessQueryExpression columnAccessQueryExpression = invocation.Arguments[0] as ColumnAccessQueryExpression;
				if (columnAccessQueryExpression != null)
				{
					IdentifierCubeExpression identifierCubeExpression = this.VisitColumnAccess(columnAccessQueryExpression);
					IdentifierCubeExpression identifierCubeExpression2;
					if (this.metadata.IsProperty(identifierCubeExpression) && this.metadata.TryGetPropertyKey(identifierCubeExpression, out identifierCubeExpression2))
					{
						return identifierCubeExpression2;
					}
				}
			}
			return null;
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x0013E5C8 File Offset: 0x0013C7C8
		private CubeExpression VisitMeasureProperty(InvocationQueryExpression invocation)
		{
			if (invocation.Arguments.Count == 2)
			{
				ColumnAccessQueryExpression columnAccessQueryExpression = invocation.Arguments[0] as ColumnAccessQueryExpression;
				ConstantQueryExpression constantQueryExpression = invocation.Arguments[1] as ConstantQueryExpression;
				if (columnAccessQueryExpression != null && constantQueryExpression != null)
				{
					IdentifierCubeExpression identifierCubeExpression = this.VisitColumnAccess(columnAccessQueryExpression);
					if (this.metadata.IsMeasure(identifierCubeExpression) && constantQueryExpression.Value.IsText)
					{
						return this.metadata.GetMeasureProperty(identifierCubeExpression, constantQueryExpression.Value.AsString);
					}
				}
			}
			return null;
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x0013E64C File Offset: 0x0013C84C
		private CubeExpression VisitListContains(InvocationQueryExpression invocation)
		{
			Value value;
			if (invocation.Arguments.Count == 2 && invocation.Arguments[0].TryGetConstant(out value) && value.IsList)
			{
				CubeExpression cubeExpression = this.Visit(invocation.Arguments[1]);
				if (cubeExpression != null)
				{
					CubeExpression cubeExpression2 = null;
					foreach (IValueReference valueReference in value.AsList)
					{
						Value value2 = valueReference.Value;
						CubeExpression cubeExpression3 = new BinaryCubeExpression(BinaryOperator2.Equals, cubeExpression, new ConstantCubeExpression(value2));
						if (cubeExpression2 == null)
						{
							cubeExpression2 = cubeExpression3;
						}
						else
						{
							cubeExpression2 = new BinaryCubeExpression(BinaryOperator2.Or, cubeExpression2, cubeExpression3);
						}
					}
					return cubeExpression2;
				}
			}
			return null;
		}

		// Token: 0x06005B22 RID: 23330 RVA: 0x0013E704 File Offset: 0x0013C904
		private CubeExpression VisitValueEquals(InvocationQueryExpression invocation)
		{
			if (invocation.Arguments.Count == 2 || invocation.Arguments.Count == 3)
			{
				CubeExpression cubeExpression = this.Visit(invocation.Arguments[0]);
				CubeExpression cubeExpression2 = this.Visit(invocation.Arguments[1]);
				CubeExpression cubeExpression3 = this.VisitOptionalArgument(invocation.Arguments, 2);
				if (cubeExpression != null && cubeExpression2 != null)
				{
					return QueryExpressionTranslator.NewEquals(cubeExpression, cubeExpression2, cubeExpression3);
				}
			}
			return null;
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x0013E774 File Offset: 0x0013C974
		private CubeExpression VisitValueNullableEquals(InvocationQueryExpression invocation)
		{
			if (invocation.Arguments.Count == 2 || invocation.Arguments.Count == 3)
			{
				QueryExpression queryExpression = invocation.Arguments[0];
				CubeExpression cubeExpression = this.Visit(invocation.Arguments[1]);
				QueryExpression queryExpression2;
				ConstantQueryExpression constantQueryExpression;
				if (QueryExpressionTranslator.TryGetDefaultIfNullTest(queryExpression, out queryExpression2, out constantQueryExpression))
				{
					ConstantCubeExpression constantCubeExpression = cubeExpression as ConstantCubeExpression;
					if (constantCubeExpression != null && !constantCubeExpression.Value.IsNull && !constantCubeExpression.Value.Equals(constantQueryExpression.Value))
					{
						queryExpression = queryExpression2;
					}
				}
				CubeExpression cubeExpression2 = this.Visit(queryExpression);
				CubeExpression cubeExpression3 = this.VisitOptionalArgument(invocation.Arguments, 2);
				if (cubeExpression2 != null && cubeExpression != null)
				{
					return QueryExpressionTranslator.NewEquals(cubeExpression2, cubeExpression, cubeExpression3);
				}
			}
			return null;
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x0013E828 File Offset: 0x0013CA28
		private CubeExpression VisitTextContainsOrStartsWith(InvocationQueryExpression invocation)
		{
			if (invocation.Arguments.Count == 2)
			{
				CubeExpression cubeExpression = this.Visit(invocation.Function);
				CubeExpression cubeExpression2 = this.Visit(QueryExpressionTranslator.EnsureNullMapsToEmptyText(invocation.Arguments[0]));
				CubeExpression cubeExpression3 = this.Visit(invocation.Arguments[1]);
				if (cubeExpression != null && cubeExpression2 != null && cubeExpression3 != null)
				{
					return new InvocationCubeExpression(cubeExpression, new CubeExpression[] { cubeExpression2, cubeExpression3 });
				}
			}
			return null;
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x0013E89A File Offset: 0x0013CA9A
		private CubeExpression VisitOptionalArgument(IList<QueryExpression> arguments, int index)
		{
			if (index < arguments.Count)
			{
				return this.Visit(arguments[index]);
			}
			return null;
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x0013E8B4 File Offset: 0x0013CAB4
		private static CubeExpression NewNot(CubeExpression expr)
		{
			return new BinaryCubeExpression(BinaryOperator2.Equals, expr, ConstantCubeExpression.False);
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x0013E8C4 File Offset: 0x0013CAC4
		private static CubeExpression NewEquals(CubeExpression left, CubeExpression right, CubeExpression precisionExpr)
		{
			Precision precision = QueryExpressionTranslator.GetPrecision(precisionExpr);
			if (precision == Precision.Double)
			{
				return new BinaryCubeExpression(BinaryOperator2.Equals, left, right);
			}
			if (precision == Precision.Decimal)
			{
				return new InvocationCubeExpression(new ConstantCubeExpression(Library._Value.Equals), new CubeExpression[] { left, right, precisionExpr });
			}
			return null;
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x0013E914 File Offset: 0x0013CB14
		private static Precision GetPrecision(CubeExpression expr)
		{
			if (expr == null)
			{
				return Precision.Double;
			}
			if (expr.Kind != CubeExpressionKind.Constant)
			{
				return null;
			}
			Value value = ((ConstantCubeExpression)expr).Value;
			if (!value.IsNull)
			{
				return Library.PrecisionEnum.Type.GetValue(value.AsNumber);
			}
			return Precision.Double;
		}

		// Token: 0x06005B29 RID: 23337 RVA: 0x0013E960 File Offset: 0x0013CB60
		private static QueryExpression EnsureNullMapsToEmptyText(QueryExpression queryExpr)
		{
			QueryExpression queryExpression;
			ConstantQueryExpression constantQueryExpression;
			if (QueryExpressionTranslator.TryGetDefaultIfNullTest(queryExpr, out queryExpression, out constantQueryExpression) && constantQueryExpression.Value.IsText && constantQueryExpression.Value.AsText.Length == 0)
			{
				return queryExpression;
			}
			return queryExpr;
		}

		// Token: 0x06005B2A RID: 23338 RVA: 0x0013E99C File Offset: 0x0013CB9C
		private static bool TryGetConstantComparison(QueryExpression queryExpr, out QueryExpression expr, out BinaryOperator2 op, out Value value)
		{
			BinaryQueryExpression binaryQueryExpression = queryExpr as BinaryQueryExpression;
			if (binaryQueryExpression != null)
			{
				BinaryOperator2 @operator = binaryQueryExpression.Operator;
				if (@operator - BinaryOperator2.GreaterThan <= 5)
				{
					op = binaryQueryExpression.Operator;
					if (binaryQueryExpression.Left.TryGetConstant(out value))
					{
						expr = binaryQueryExpression.Right;
						return true;
					}
					if (binaryQueryExpression.Right.TryGetConstant(out value))
					{
						expr = binaryQueryExpression.Left;
						return true;
					}
				}
			}
			expr = null;
			op = BinaryOperator2.Add;
			value = null;
			return false;
		}

		// Token: 0x06005B2B RID: 23339 RVA: 0x0013EA04 File Offset: 0x0013CC04
		private static bool TryGetDefaultIfNullTest(QueryExpression queryExpr, out QueryExpression expr, out ConstantQueryExpression defaultExpr)
		{
			IfQueryExpression ifQueryExpression = queryExpr as IfQueryExpression;
			BinaryOperator2 binaryOperator;
			Value value;
			if (ifQueryExpression != null && QueryExpressionTranslator.TryGetConstantComparison(ifQueryExpression.Condition, out expr, out binaryOperator, out value) && binaryOperator == BinaryOperator2.NotEquals && value.IsNull && expr.Equals(ifQueryExpression.TrueCase))
			{
				defaultExpr = ifQueryExpression.FalseCase as ConstantQueryExpression;
				if (defaultExpr != null)
				{
					return true;
				}
			}
			expr = null;
			defaultExpr = null;
			return false;
		}

		// Token: 0x040032D7 RID: 13015
		private readonly Dictionary<Value, Func<InvocationQueryExpression, CubeExpression>> handlers;

		// Token: 0x040032D8 RID: 13016
		private readonly ICubeMetadataProvider metadata;
	}
}
