using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000040 RID: 64
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	internal sealed class CalendarColumnReferenceCollection : NamedMetadataObjectCollection<CalendarColumnReference, TimeUnitColumnAssociation>
	{
		// Token: 0x06000229 RID: 553 RVA: 0x0000F5C7 File Offset: 0x0000D7C7
		internal CalendarColumnReferenceCollection(TimeUnitColumnAssociation parent, IEqualityComparer<string> comparer)
			: base(ObjectType.CalendarColumnReference, parent, comparer, true)
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		private protected override void CompareWith(MetadataObjectCollection<CalendarColumnReference, TimeUnitColumnAssociation> other, CopyContext context, IList<CalendarColumnReference> removedItems, IList<CalendarColumnReference> addedItems, IList<KeyValuePair<CalendarColumnReference, CalendarColumnReference>> matchedItems)
		{
			Utils.CompareLinkedObjectCollections<CalendarColumnReference>(this, other, context, true, removedItems, addedItems, matchedItems);
		}
	}
}
