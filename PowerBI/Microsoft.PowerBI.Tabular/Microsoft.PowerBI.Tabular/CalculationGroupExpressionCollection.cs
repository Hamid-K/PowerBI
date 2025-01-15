using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200003A RID: 58
	[CompatibilityRequirement("1605")]
	internal sealed class CalculationGroupExpressionCollection : MetadataObjectCollection<CalculationGroupExpression, CalculationGroup>
	{
		// Token: 0x06000182 RID: 386 RVA: 0x0000B5E5 File Offset: 0x000097E5
		internal CalculationGroupExpressionCollection(CalculationGroup parent)
			: base(ObjectType.CalculationExpression, parent)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000B5F0 File Offset: 0x000097F0
		private protected override void CompareWith(MetadataObjectCollection<CalculationGroupExpression, CalculationGroup> other, CopyContext context, IList<CalculationGroupExpression> removedItems, IList<CalculationGroupExpression> addedItems, IList<KeyValuePair<CalculationGroupExpression, CalculationGroupExpression>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<CalculationGroupExpression>(this, other, context, removedItems, addedItems, matchedItems, CalculationGroupExpressionCollection.isEquivalentCalculationGroupExpression);
		}

		// Token: 0x040000D8 RID: 216
		private static Func<CalculationGroupExpression, CalculationGroupExpression, bool> isEquivalentCalculationGroupExpression = (CalculationGroupExpression item1, CalculationGroupExpression item2) => item1.body.SelectionMode == item2.body.SelectionMode;
	}
}
