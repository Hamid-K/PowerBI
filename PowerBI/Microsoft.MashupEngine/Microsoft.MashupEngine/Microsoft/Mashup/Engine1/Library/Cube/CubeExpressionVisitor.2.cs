using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CF4 RID: 3316
	internal abstract class CubeExpressionVisitor : CubeExpressionVisitor<CubeExpression, CubeSortOrder>
	{
		// Token: 0x060059F6 RID: 23030 RVA: 0x0013A926 File Offset: 0x00138B26
		protected override CubeSortOrder NewSortOrder(CubeExpression expression, bool ascending)
		{
			return new CubeSortOrder(expression, ascending);
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x0013A92F File Offset: 0x00138B2F
		protected override CubeExpression NewConstant(Value constant)
		{
			return new ConstantCubeExpression(constant);
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected override CubeExpression NewIdentifier(IdentifierCubeExpression identifier)
		{
			return identifier;
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x0013A937 File Offset: 0x00138B37
		protected override CubeExpression NewIf(CubeExpression condition, CubeExpression trueCase, CubeExpression falseCase)
		{
			return new IfCubeExpression(condition, trueCase, falseCase);
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x0013A941 File Offset: 0x00138B41
		protected override CubeExpression NewInvocation(CubeExpression function, CubeExpression[] arguments)
		{
			return new InvocationCubeExpression(function, arguments);
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x0013A94A File Offset: 0x00138B4A
		protected override CubeExpression NewBinary(BinaryOperator2 op, CubeExpression left, CubeExpression right)
		{
			return new BinaryCubeExpression(op, left, right);
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x0013A954 File Offset: 0x00138B54
		protected override CubeExpression NewQuery(CubeExpression from, IList<IdentifierCubeExpression> dimensionAttributes, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> measureProperties, CubeExpression filter, CubeSortOrder[] sortOrders, RowRange rowRange)
		{
			return new QueryCubeExpression(from, dimensionAttributes, properties, measures, measureProperties, filter, sortOrders, rowRange);
		}
	}
}
