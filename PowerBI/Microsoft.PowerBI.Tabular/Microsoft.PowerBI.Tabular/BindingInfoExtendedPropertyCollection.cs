using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000034 RID: 52
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class BindingInfoExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, BindingInfo>
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00007B4A File Offset: 0x00005D4A
		internal BindingInfoExtendedPropertyCollection(BindingInfo parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007B57 File Offset: 0x00005D57
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, BindingInfo> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
