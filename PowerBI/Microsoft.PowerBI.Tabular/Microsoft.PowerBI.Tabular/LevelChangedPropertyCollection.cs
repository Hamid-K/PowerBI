using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200006F RID: 111
	[CompatibilityRequirement("1567")]
	public sealed class LevelChangedPropertyCollection : MetadataObjectCollection<ChangedProperty, Level>
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x0002F3C8 File Offset: 0x0002D5C8
		internal LevelChangedPropertyCollection(Level parent)
			: base(ObjectType.ChangedProperty, parent)
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002F3D3 File Offset: 0x0002D5D3
		private protected override void CompareWith(MetadataObjectCollection<ChangedProperty, Level> other, CopyContext context, IList<ChangedProperty> removedItems, IList<ChangedProperty> addedItems, IList<KeyValuePair<ChangedProperty, ChangedProperty>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ChangedProperty>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentChangedProperty);
		}
	}
}
