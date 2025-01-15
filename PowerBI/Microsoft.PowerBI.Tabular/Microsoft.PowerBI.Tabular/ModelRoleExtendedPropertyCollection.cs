using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000081 RID: 129
	[CompatibilityRequirement("1400")]
	public sealed class ModelRoleExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, ModelRole>
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x00041ABF File Offset: 0x0003FCBF
		internal ModelRoleExtendedPropertyCollection(ModelRole parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00041ACC File Offset: 0x0003FCCC
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, ModelRole> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
