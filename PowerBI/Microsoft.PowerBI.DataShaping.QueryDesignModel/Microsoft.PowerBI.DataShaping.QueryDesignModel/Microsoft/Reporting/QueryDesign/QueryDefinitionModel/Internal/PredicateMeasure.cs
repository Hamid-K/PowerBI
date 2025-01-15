using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000F3 RID: 243
	internal sealed class PredicateMeasure : Measure, IJoinPredicate
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x00023D61 File Offset: 0x00021F61
		internal PredicateMeasure(QueryExpression expression, string name)
			: base(expression, name)
		{
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00023D6B File Offset: 0x00021F6B
		bool IJoinPredicate.IsAnchored
		{
			get
			{
				return Measure.IsMeasureExpressionAnchored(base.Expression);
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00023D78 File Offset: 0x00021F78
		QueryExpression IJoinPredicate.ToPredicateExpression()
		{
			return Measure.CreateJoinPredicateExpressionForMeasureExpression(base.Expression);
		}
	}
}
