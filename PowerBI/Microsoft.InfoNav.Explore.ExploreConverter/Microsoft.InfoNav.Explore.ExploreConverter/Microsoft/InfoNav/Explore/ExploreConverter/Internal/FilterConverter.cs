using System;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000034 RID: 52
	internal static class FilterConverter
	{
		// Token: 0x0600017A RID: 378 RVA: 0x000076D0 File Offset: 0x000058D0
		public static PVFilter ConvertFilter(IFilterCondition<Formula> filterCondition)
		{
			if (filterCondition is CompoundFilterCondition<Formula>)
			{
				CompoundFilterCondition<Formula> compoundFilterCondition = filterCondition as CompoundFilterCondition<Formula>;
				PVFilter pvfilter = new PVFilter();
				pvfilter.Type = "Compound";
				pvfilter.Operator = compoundFilterCondition.Operator.ToString();
				pvfilter.FilterConditions = compoundFilterCondition.Conditions.Select((IFilterCondition<Formula> condition) => FilterConverter.ConvertFilter(condition)).ToList<PVFilter>();
				return pvfilter;
			}
			if (filterCondition is BinaryFilterCondition<Formula>)
			{
				BinaryFilterCondition<Formula> binaryFilterCondition = filterCondition as BinaryFilterCondition<Formula>;
				return new PVFilter
				{
					Type = "Binary",
					LeftExpression = binaryFilterCondition.LeftExpression,
					RightExpression = new FilterValue(binaryFilterCondition.RightExpression),
					Operator = binaryFilterCondition.Operator.ToString(),
					Not = binaryFilterCondition.Not
				};
			}
			UnaryFilterCondition<Formula> unaryFilterCondition = filterCondition as UnaryFilterCondition<Formula>;
			return new PVFilter
			{
				Type = "Unary",
				Not = unaryFilterCondition.Not,
				LeftExpression = unaryFilterCondition.Expression
			};
		}
	}
}
