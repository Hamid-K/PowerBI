using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CE7 RID: 3303
	internal static class CubeExpressionExtensions
	{
		// Token: 0x0600599E RID: 22942 RVA: 0x0013997A File Offset: 0x00137B7A
		public static bool TryGetConstant(this CubeExpression expression, out ConstantCubeExpression constant)
		{
			constant = expression as ConstantCubeExpression;
			return constant != null;
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x0013998C File Offset: 0x00137B8C
		public static bool TryGetConstant(this CubeExpression expression, out Value value)
		{
			ConstantCubeExpression constantCubeExpression;
			if (expression.TryGetConstant(out constantCubeExpression))
			{
				value = constantCubeExpression.Value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x001399B1 File Offset: 0x00137BB1
		public static bool TryGetIdentifier(this CubeExpression expression, out IdentifierCubeExpression identifier)
		{
			identifier = expression as IdentifierCubeExpression;
			return identifier != null;
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x001399C0 File Offset: 0x00137BC0
		public static bool TryGetBinary(this CubeExpression expression, BinaryOperator2 op, out CubeExpression left, out CubeExpression right)
		{
			BinaryCubeExpression binaryCubeExpression = expression as BinaryCubeExpression;
			if (binaryCubeExpression != null && binaryCubeExpression.Operator == op)
			{
				left = binaryCubeExpression.Left;
				right = binaryCubeExpression.Right;
				return true;
			}
			left = null;
			right = null;
			return false;
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x001399FC File Offset: 0x00137BFC
		public static bool TryGetConstantFilter(this CubeExpression expression, out CubeExpression expr, out ConstantCubeExpression constant)
		{
			BinaryOperator2 binaryOperator;
			return expression.TryGetConstantFilter(out binaryOperator, out expr, out constant) && binaryOperator == BinaryOperator2.Equals;
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x00139A1C File Offset: 0x00137C1C
		public static bool TryGetConstantFilter(this CubeExpression expression, out BinaryOperator2 op, out CubeExpression expr, out ConstantCubeExpression constant)
		{
			BinaryCubeExpression binaryCubeExpression = expression as BinaryCubeExpression;
			if (binaryCubeExpression != null)
			{
				op = binaryCubeExpression.Operator;
				if (binaryCubeExpression.Right.TryGetConstant(out constant))
				{
					expr = binaryCubeExpression.Left;
					return true;
				}
				if (binaryCubeExpression.Left.TryGetConstant(out constant) && op.TrySwapOperands(out op))
				{
					expr = binaryCubeExpression.Right;
					return true;
				}
			}
			op = BinaryOperator2.Add;
			expr = null;
			constant = null;
			return false;
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x00139A80 File Offset: 0x00137C80
		public static bool TryGetInvocation(this CubeExpression expression, out FunctionValue function, out IList<CubeExpression> args)
		{
			InvocationCubeExpression invocationCubeExpression = expression as InvocationCubeExpression;
			Value value;
			if (invocationCubeExpression != null && invocationCubeExpression.Function.TryGetConstant(out value) && value.IsFunction)
			{
				function = value.AsFunction;
				args = invocationCubeExpression.Arguments;
				return true;
			}
			function = null;
			args = null;
			return false;
		}

		// Token: 0x060059A5 RID: 22949 RVA: 0x00139AC8 File Offset: 0x00137CC8
		public static bool TryGetInvocation(this CubeExpression expression, FunctionValue function, int argCount, out IList<CubeExpression> args)
		{
			FunctionValue functionValue;
			if (expression.TryGetInvocation(out functionValue, out args) && functionValue.Equals(function) && argCount == args.Count)
			{
				return true;
			}
			args = null;
			return false;
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x00139AFC File Offset: 0x00137CFC
		public static CubeExpression And(this CubeExpression left, CubeExpression right)
		{
			if (ConstantCubeExpression.False.Equals(left) || ConstantCubeExpression.False.Equals(right))
			{
				return ConstantCubeExpression.False;
			}
			if (left == null || left.Equals(ConstantCubeExpression.True))
			{
				return right;
			}
			if (right == null || right.Equals(ConstantCubeExpression.True))
			{
				return left;
			}
			if (left.Equals(right))
			{
				return left;
			}
			return new BinaryCubeExpression(BinaryOperator2.And, left, right);
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x00139B64 File Offset: 0x00137D64
		public static CubeExpression Or(this CubeExpression left, CubeExpression right)
		{
			if (ConstantCubeExpression.True.Equals(left) || ConstantCubeExpression.True.Equals(right))
			{
				return ConstantCubeExpression.True;
			}
			if (left == null || left.Equals(ConstantCubeExpression.False))
			{
				return right;
			}
			if (right == null || right.Equals(ConstantCubeExpression.False))
			{
				return left;
			}
			if (left.Equals(right))
			{
				return left;
			}
			return new BinaryCubeExpression(BinaryOperator2.Or, left, right);
		}

		// Token: 0x060059A8 RID: 22952 RVA: 0x00139BCC File Offset: 0x00137DCC
		public static QueryCubeExpression PushFiltersToSubSelect(this QueryCubeExpression expression)
		{
			if (expression.Filter == null)
			{
				return expression;
			}
			return new QueryCubeExpression(new QueryCubeExpression(expression.From, expression.DimensionAttributes, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, expression.Filter, EmptyArray<CubeSortOrder>.Instance, RowRange.All), expression.DimensionAttributes, expression.Properties, expression.Measures, expression.MeasureProperties, null, expression.Sort, expression.RowRange);
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x00139C3D File Offset: 0x00137E3D
		public static IList<IdentifierCubeExpression> GetReferences(this CubeExpression expression)
		{
			return new CubeExpressionExtensions.ReferenceVisitor().GetReferences(expression);
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x00139C4A File Offset: 0x00137E4A
		public static QueryCubeExpression PropagateMeasuresDownwards(this QueryCubeExpression expression)
		{
			return CubeExpressionExtensions.PropagateMeasuresDownwards(expression, new HashSet<IdentifierCubeExpression>());
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x00139C57 File Offset: 0x00137E57
		public static QueryCubeExpression Flatten(this QueryCubeExpression query)
		{
			return CubeExpressionExtensions.Flatten(query, true);
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x00139C60 File Offset: 0x00137E60
		private static QueryCubeExpression Flatten(QueryCubeExpression query, bool isTop)
		{
			if (query.From.Kind == CubeExpressionKind.Identifier)
			{
				return query;
			}
			QueryCubeExpression queryCubeExpression = CubeExpressionExtensions.Flatten((QueryCubeExpression)query.From, false);
			if (isTop || query.FiltersMeasures())
			{
				return new QueryCubeExpression(queryCubeExpression, query.DimensionAttributes, query.Properties, query.Measures, query.MeasureProperties, query.Filter, query.Sort, query.RowRange);
			}
			HashSet<IdentifierCubeExpression> hashSet = new HashSet<IdentifierCubeExpression>(query.DimensionAttributes.Concat(queryCubeExpression.DimensionAttributes));
			HashSet<IdentifierCubeExpression> hashSet2 = new HashSet<IdentifierCubeExpression>(query.Properties.Concat(queryCubeExpression.Properties));
			HashSet<IdentifierCubeExpression> hashSet3 = new HashSet<IdentifierCubeExpression>(query.Measures.Concat(queryCubeExpression.Measures));
			HashSet<IdentifierCubeExpression> hashSet4 = new HashSet<IdentifierCubeExpression>(query.MeasureProperties.Concat(queryCubeExpression.MeasureProperties));
			CubeExpression cubeExpression = query.Filter;
			if (queryCubeExpression.Filter != null)
			{
				if (cubeExpression == null)
				{
					cubeExpression = queryCubeExpression.Filter;
				}
				else
				{
					cubeExpression = new BinaryCubeExpression(BinaryOperator2.And, cubeExpression, queryCubeExpression.Filter);
				}
			}
			return new QueryCubeExpression(queryCubeExpression.From, hashSet.ToArray<IdentifierCubeExpression>(), hashSet2.ToArray<IdentifierCubeExpression>(), hashSet3.ToArray<IdentifierCubeExpression>(), hashSet4.ToArray<IdentifierCubeExpression>(), cubeExpression, EmptyArray<CubeSortOrder>.Instance, query.RowRange);
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x00139D88 File Offset: 0x00137F88
		public static bool FiltersMeasures(this QueryCubeExpression query)
		{
			if (query.Filter == null || query.Measures.Count == 0)
			{
				return false;
			}
			HashSet<IdentifierCubeExpression> hashSet = new HashSet<IdentifierCubeExpression>(query.Measures);
			return query.Filter.GetReferences().FirstOrDefault(new Func<IdentifierCubeExpression, bool>(hashSet.Contains)) != null;
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x00139DD8 File Offset: 0x00137FD8
		private static QueryCubeExpression PropagateMeasuresDownwards(QueryCubeExpression queryExpression, HashSet<IdentifierCubeExpression> measures)
		{
			measures.UnionWith(queryExpression.Measures);
			IList<IdentifierCubeExpression> list = measures.ToArray<IdentifierCubeExpression>();
			CubeExpression cubeExpression = queryExpression.From;
			if (cubeExpression.Kind == CubeExpressionKind.Query)
			{
				cubeExpression = CubeExpressionExtensions.PropagateMeasuresDownwards((QueryCubeExpression)queryExpression.From, measures);
			}
			return new QueryCubeExpression(cubeExpression, queryExpression.DimensionAttributes, queryExpression.Properties, list, queryExpression.MeasureProperties, queryExpression.Filter, queryExpression.Sort, queryExpression.RowRange);
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x00139E45 File Offset: 0x00138045
		public static QueryCubeExpression PropagateDimensionAttributesDownwards(this QueryCubeExpression expression)
		{
			return (QueryCubeExpression)CubeExpressionExtensions.PropagateDimensionAttributesDownwards(expression, new HashSet<IdentifierCubeExpression>());
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x00139E58 File Offset: 0x00138058
		private static CubeExpression PropagateDimensionAttributesDownwards(CubeExpression expression, HashSet<IdentifierCubeExpression> dimensionAttributes)
		{
			if (expression.Kind != CubeExpressionKind.Query)
			{
				return expression;
			}
			QueryCubeExpression queryCubeExpression = (QueryCubeExpression)expression;
			if (queryCubeExpression.FiltersMeasures())
			{
				return new QueryCubeExpression(CubeExpressionExtensions.PropagateDimensionAttributesDownwards(queryCubeExpression.From, new HashSet<IdentifierCubeExpression>(queryCubeExpression.DimensionAttributes)), queryCubeExpression.DimensionAttributes, queryCubeExpression.Properties, queryCubeExpression.Measures, queryCubeExpression.MeasureProperties, queryCubeExpression.Filter, queryCubeExpression.Sort, queryCubeExpression.RowRange);
			}
			dimensionAttributes.UnionWith(queryCubeExpression.DimensionAttributes);
			IList<IdentifierCubeExpression> list = dimensionAttributes.ToArray<IdentifierCubeExpression>();
			return new QueryCubeExpression(CubeExpressionExtensions.PropagateDimensionAttributesDownwards(queryCubeExpression.From, dimensionAttributes), list, queryCubeExpression.Properties, queryCubeExpression.Measures, queryCubeExpression.MeasureProperties, queryCubeExpression.Filter, queryCubeExpression.Sort, queryCubeExpression.RowRange);
		}

		// Token: 0x02000CE8 RID: 3304
		private sealed class ReferenceVisitor : CubeExpressionVisitor
		{
			// Token: 0x060059B1 RID: 22961 RVA: 0x00139F11 File Offset: 0x00138111
			public IList<IdentifierCubeExpression> GetReferences(CubeExpression expression)
			{
				this.identifiers = new HashSet<string>();
				this.identifierExprs = new List<IdentifierCubeExpression>();
				this.Visit(expression);
				IList<IdentifierCubeExpression> list = this.identifierExprs;
				this.identifierExprs = null;
				this.identifiers = null;
				return list;
			}

			// Token: 0x060059B2 RID: 22962 RVA: 0x00139F45 File Offset: 0x00138145
			protected override CubeExpression VisitIdentifier(IdentifierCubeExpression identifier)
			{
				if (this.identifiers.Add(identifier.Identifier))
				{
					this.identifierExprs.Add(identifier);
				}
				return identifier;
			}

			// Token: 0x0400322B RID: 12843
			private List<IdentifierCubeExpression> identifierExprs;

			// Token: 0x0400322C RID: 12844
			private HashSet<string> identifiers;
		}
	}
}
