using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E52 RID: 3666
	internal class QueryExpressionToCubeExpressionVisitor : QueryExpressionVisitor2<CubeExpression>
	{
		// Token: 0x06006285 RID: 25221 RVA: 0x001523E5 File Offset: 0x001505E5
		public QueryExpressionToCubeExpressionVisitor(IList<IdentifierCubeExpression> columns)
		{
			this.columns = columns;
		}

		// Token: 0x06006286 RID: 25222 RVA: 0x0013A94A File Offset: 0x00138B4A
		protected override CubeExpression NewBinary(BinaryOperator2 op, CubeExpression left, CubeExpression right)
		{
			return new BinaryCubeExpression(op, left, right);
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x001523F4 File Offset: 0x001505F4
		protected override CubeExpression NewColumnAccess(int column)
		{
			return this.columns[column];
		}

		// Token: 0x06006288 RID: 25224 RVA: 0x0013A92F File Offset: 0x00138B2F
		protected override CubeExpression NewConstant(Value value)
		{
			return new ConstantCubeExpression(value);
		}

		// Token: 0x06006289 RID: 25225 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override CubeExpression NewArgumentAccess()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x0013A937 File Offset: 0x00138B37
		protected override CubeExpression NewIf(CubeExpression condition, CubeExpression trueCase, CubeExpression falseCase)
		{
			return new IfCubeExpression(condition, trueCase, falseCase);
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x00152402 File Offset: 0x00150602
		protected override CubeExpression NewInvocation(CubeExpression function, IList<CubeExpression> arguments)
		{
			return new InvocationCubeExpression(function, arguments.ToArray<CubeExpression>());
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override CubeExpression NewUnary(UnaryOperator2 op, CubeExpression operand)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040035AD RID: 13741
		private readonly IList<IdentifierCubeExpression> columns;
	}
}
