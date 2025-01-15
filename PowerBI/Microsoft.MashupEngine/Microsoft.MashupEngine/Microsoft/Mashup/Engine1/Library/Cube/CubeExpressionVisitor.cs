using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CF3 RID: 3315
	internal abstract class CubeExpressionVisitor<T, S>
	{
		// Token: 0x060059E7 RID: 23015 RVA: 0x0013A70C File Offset: 0x0013890C
		protected virtual T Visit(CubeExpression expression)
		{
			switch (expression.Kind)
			{
			case CubeExpressionKind.Constant:
				return this.VisitConstant((ConstantCubeExpression)expression);
			case CubeExpressionKind.Identifier:
				return this.VisitIdentifier((IdentifierCubeExpression)expression);
			case CubeExpressionKind.Binary:
				return this.VisitBinary((BinaryCubeExpression)expression);
			case CubeExpressionKind.Query:
				return this.VisitQuery((QueryCubeExpression)expression);
			case CubeExpressionKind.Invocation:
				return this.VisitInvocation((InvocationCubeExpression)expression);
			case CubeExpressionKind.If:
				return this.VisitIf((IfCubeExpression)expression);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x0013A793 File Offset: 0x00138993
		protected virtual T VisitConstant(ConstantCubeExpression constant)
		{
			return this.NewConstant(constant.Value);
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x0013A7A1 File Offset: 0x001389A1
		protected virtual T VisitIdentifier(IdentifierCubeExpression identifier)
		{
			return this.NewIdentifier(identifier);
		}

		// Token: 0x060059EA RID: 23018 RVA: 0x0013A7AC File Offset: 0x001389AC
		protected virtual T VisitIf(IfCubeExpression @if)
		{
			T t = this.Visit(@if.Condition);
			T t2 = this.Visit(@if.TrueCase);
			T t3 = this.Visit(@if.FalseCase);
			return this.NewIf(t, t2, t3);
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x0013A7EC File Offset: 0x001389EC
		protected virtual T VisitInvocation(InvocationCubeExpression invocation)
		{
			T t = this.Visit(invocation.Function);
			T[] array = new T[invocation.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Visit(invocation.Arguments[i]);
			}
			return this.NewInvocation(t, array);
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x0013A848 File Offset: 0x00138A48
		protected virtual T VisitBinary(BinaryCubeExpression binary)
		{
			T t = this.Visit(binary.Left);
			T t2 = this.Visit(binary.Right);
			return this.NewBinary(binary.Operator, t, t2);
		}

		// Token: 0x060059ED RID: 23021 RVA: 0x0013A880 File Offset: 0x00138A80
		protected virtual T VisitQuery(QueryCubeExpression query)
		{
			T t = this.Visit(query.From);
			T t2 = this.Visit(query.Filter);
			S[] array = new S[query.Sort.Count];
			for (int i = 0; i < array.Length; i++)
			{
				T t3 = this.Visit(query.Sort[i].Expression);
				array[i] = this.NewSortOrder(t3, query.Sort[i].Ascending);
			}
			return this.NewQuery(t, query.DimensionAttributes, query.Properties, query.Measures, query.MeasureProperties, t2, array, query.RowRange);
		}

		// Token: 0x060059EE RID: 23022
		protected abstract S NewSortOrder(T expression, bool ascending);

		// Token: 0x060059EF RID: 23023
		protected abstract T NewConstant(Value constant);

		// Token: 0x060059F0 RID: 23024
		protected abstract T NewIdentifier(IdentifierCubeExpression identifier);

		// Token: 0x060059F1 RID: 23025
		protected abstract T NewIf(T condition, T trueCase, T falseCase);

		// Token: 0x060059F2 RID: 23026
		protected abstract T NewInvocation(T function, T[] arguments);

		// Token: 0x060059F3 RID: 23027
		protected abstract T NewBinary(BinaryOperator2 op, T left, T right);

		// Token: 0x060059F4 RID: 23028
		protected abstract T NewQuery(T from, IList<IdentifierCubeExpression> dimensionAttributes, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> measureProperties, T filter, S[] sortOrders, RowRange rowRange);
	}
}
