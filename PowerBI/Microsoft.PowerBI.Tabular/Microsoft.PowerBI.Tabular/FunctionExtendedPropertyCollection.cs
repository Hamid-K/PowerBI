using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000060 RID: 96
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Internal")]
	public sealed class FunctionExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Function>
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x00026C7D File Offset: 0x00024E7D
		internal FunctionExtendedPropertyCollection(Function parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00026C8A File Offset: 0x00024E8A
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Function> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
