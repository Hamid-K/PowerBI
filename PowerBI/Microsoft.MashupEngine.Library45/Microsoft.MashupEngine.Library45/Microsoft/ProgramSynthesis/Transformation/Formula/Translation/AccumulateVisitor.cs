using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017DC RID: 6108
	internal class AccumulateVisitor : IFormulaVisitor
	{
		// Token: 0x0600C967 RID: 51559 RVA: 0x002B236C File Offset: 0x002B056C
		private AccumulateVisitor(Action<FormulaExpression> accumulator, AccumulateDirection direction)
		{
			this._accumulator = accumulator;
			this._direction = direction;
		}

		// Token: 0x0600C968 RID: 51560 RVA: 0x002B2382 File Offset: 0x002B0582
		public static void Accumulate(FormulaExpression expression, Action<FormulaExpression> accumulator, AccumulateDirection direction = AccumulateDirection.BottomUp)
		{
			new AccumulateVisitor(accumulator, direction).Visit(expression);
		}

		// Token: 0x0600C969 RID: 51561 RVA: 0x002B2391 File Offset: 0x002B0591
		public static void AccumulateBottomUp(FormulaExpression expression, Action<FormulaExpression> accumulator)
		{
			AccumulateVisitor.Accumulate(expression, accumulator, AccumulateDirection.BottomUp);
		}

		// Token: 0x0600C96A RID: 51562 RVA: 0x002B239B File Offset: 0x002B059B
		public static void AccumulateTopDown(FormulaExpression expression, Action<FormulaExpression> accumulator)
		{
			AccumulateVisitor.Accumulate(expression, accumulator, AccumulateDirection.TopDown);
		}

		// Token: 0x0600C96B RID: 51563 RVA: 0x002B23A8 File Offset: 0x002B05A8
		public void Visit(FormulaExpression node)
		{
			if (this._direction == AccumulateDirection.BottomUp)
			{
				foreach (FormulaExpression formulaExpression in node.Children)
				{
					formulaExpression.Accept(this);
				}
				this._accumulator(node);
			}
			if (this._direction != AccumulateDirection.TopDown)
			{
				return;
			}
			this._accumulator(node);
			foreach (FormulaExpression formulaExpression2 in node.Children)
			{
				formulaExpression2.Accept(this);
			}
		}

		// Token: 0x04004F1D RID: 20253
		private readonly Action<FormulaExpression> _accumulator;

		// Token: 0x04004F1E RID: 20254
		private readonly AccumulateDirection _direction;
	}
}
