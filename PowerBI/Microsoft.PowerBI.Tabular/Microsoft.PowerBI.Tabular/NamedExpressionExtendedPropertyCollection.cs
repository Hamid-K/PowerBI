using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200008A RID: 138
	[CompatibilityRequirement("1400")]
	public sealed class NamedExpressionExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, NamedExpression>
	{
		// Token: 0x0600083D RID: 2109 RVA: 0x0004704C File Offset: 0x0004524C
		internal NamedExpressionExtendedPropertyCollection(NamedExpression parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00047059 File Offset: 0x00045259
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, NamedExpression> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
