using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000C9 RID: 201
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class TimeUnitColumnAssociationCollection : MetadataObjectCollection<TimeUnitColumnAssociation, Calendar>
	{
		// Token: 0x06000CB3 RID: 3251 RVA: 0x0006A3EC File Offset: 0x000685EC
		internal TimeUnitColumnAssociationCollection(Calendar parent)
			: base(ObjectType.TimeUnitColumnAssociation, parent)
		{
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0006A3F7 File Offset: 0x000685F7
		private protected override void CompareWith(MetadataObjectCollection<TimeUnitColumnAssociation, Calendar> other, CopyContext context, IList<TimeUnitColumnAssociation> removedItems, IList<TimeUnitColumnAssociation> addedItems, IList<KeyValuePair<TimeUnitColumnAssociation, TimeUnitColumnAssociation>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<TimeUnitColumnAssociation>(this, other, context, removedItems, addedItems, matchedItems, TimeUnitColumnAssociationCollection.isEquivalentTimeUnitColumnAssociation);
		}

		// Token: 0x0400018C RID: 396
		private static Func<TimeUnitColumnAssociation, TimeUnitColumnAssociation, bool> isEquivalentTimeUnitColumnAssociation = (TimeUnitColumnAssociation item1, TimeUnitColumnAssociation item2) => item1.body.TimeUnit == item2.body.TimeUnit;
	}
}
