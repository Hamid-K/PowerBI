using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000033 RID: 51
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class BindingInfoCollection : NamedMetadataObjectCollection<BindingInfo, Model>
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00007B1F File Offset: 0x00005D1F
		internal BindingInfoCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.BindingInfo, parent, comparer, true)
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007B2C File Offset: 0x00005D2C
		private protected override void CompareWith(MetadataObjectCollection<BindingInfo, Model> other, CopyContext context, IList<BindingInfo> removedItems, IList<BindingInfo> addedItems, IList<KeyValuePair<BindingInfo, BindingInfo>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<BindingInfo>(removedItems, addedItems, matchedItems, BindingInfo.CompareBindingInfoType);
		}
	}
}
