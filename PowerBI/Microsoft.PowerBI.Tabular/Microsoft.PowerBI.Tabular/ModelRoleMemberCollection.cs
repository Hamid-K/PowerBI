using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000084 RID: 132
	public sealed class ModelRoleMemberCollection : NamedMetadataObjectCollection<ModelRoleMember, ModelRole>
	{
		// Token: 0x060007E2 RID: 2018 RVA: 0x00042F59 File Offset: 0x00041159
		internal ModelRoleMemberCollection(ModelRole parent, IEqualityComparer<string> comparer)
			: base(ObjectType.RoleMembership, parent, comparer, true)
		{
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00042F66 File Offset: 0x00041166
		private protected override void CompareWith(MetadataObjectCollection<ModelRoleMember, ModelRole> other, CopyContext context, IList<ModelRoleMember> removedItems, IList<ModelRoleMember> addedItems, IList<KeyValuePair<ModelRoleMember, ModelRoleMember>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ModelRoleMember>(removedItems, addedItems, matchedItems, ModelRoleMember.CompareRoleMembershipType);
		}
	}
}
