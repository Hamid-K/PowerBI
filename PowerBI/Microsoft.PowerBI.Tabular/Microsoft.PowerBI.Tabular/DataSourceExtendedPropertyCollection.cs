using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000056 RID: 86
	[CompatibilityRequirement("1400")]
	public sealed class DataSourceExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, DataSource>
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x00020267 File Offset: 0x0001E467
		internal DataSourceExtendedPropertyCollection(DataSource parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00020274 File Offset: 0x0001E474
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, DataSource> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
