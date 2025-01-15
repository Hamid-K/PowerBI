using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000085 RID: 133
	[CompatibilityRequirement("1400")]
	public sealed class ModelRoleMemberExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, ModelRoleMember>
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x00042F84 File Offset: 0x00041184
		internal ModelRoleMemberExtendedPropertyCollection(ModelRoleMember parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00042F91 File Offset: 0x00041191
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, ModelRoleMember> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
