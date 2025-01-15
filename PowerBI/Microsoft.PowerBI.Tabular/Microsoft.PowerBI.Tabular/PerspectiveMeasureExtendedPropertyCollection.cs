using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000A0 RID: 160
	[CompatibilityRequirement("1400")]
	public sealed class PerspectiveMeasureExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, PerspectiveMeasure>
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x00052420 File Offset: 0x00050620
		internal PerspectiveMeasureExtendedPropertyCollection(PerspectiveMeasure parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0005242D File Offset: 0x0005062D
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, PerspectiveMeasure> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
