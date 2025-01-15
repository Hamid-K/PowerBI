using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000035 RID: 53
	internal sealed class FormulaFilterConditionTransform : IFilterConditionVisitor<IRdmQueryExpression>
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000077E4 File Offset: 0x000059E4
		public static IFilterCondition<Formula> GetResult(IFilterCondition<IRdmQueryExpression> filterCondition)
		{
			FormulaFilterConditionTransform formulaFilterConditionTransform = new FormulaFilterConditionTransform();
			filterCondition.Accept(formulaFilterConditionTransform);
			return formulaFilterConditionTransform._result;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007804 File Offset: 0x00005A04
		public void Visit(UnaryFilterCondition<IRdmQueryExpression> filterCondition)
		{
			Formula formula = QueryExpressionAnalyzer.CreateFormula(filterCondition.Expression);
			this._result = new UnaryFilterCondition<Formula>(formula, filterCondition.Not);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007830 File Offset: 0x00005A30
		public void Visit(BinaryFilterCondition<IRdmQueryExpression> filterCondition)
		{
			Formula formula = QueryExpressionAnalyzer.CreateFormula(filterCondition.LeftExpression);
			PrimitiveValue rightExpression = filterCondition.RightExpression;
			this._result = new BinaryFilterCondition<Formula>(formula, filterCondition.Not, filterCondition.Operator, rightExpression);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000786C File Offset: 0x00005A6C
		public void Visit(CompoundFilterCondition<IRdmQueryExpression> filterCondition)
		{
			List<IFilterCondition<Formula>> list = new List<IFilterCondition<Formula>>();
			foreach (IFilterCondition<IRdmQueryExpression> filterCondition2 in filterCondition.Conditions)
			{
				IFilterCondition<Formula> result = FormulaFilterConditionTransform.GetResult(filterCondition2);
				list.Add(result);
			}
			this._result = new CompoundFilterCondition<Formula>(filterCondition.Operator, list);
		}

		// Token: 0x040000C0 RID: 192
		private IFilterCondition<Formula> _result;
	}
}
