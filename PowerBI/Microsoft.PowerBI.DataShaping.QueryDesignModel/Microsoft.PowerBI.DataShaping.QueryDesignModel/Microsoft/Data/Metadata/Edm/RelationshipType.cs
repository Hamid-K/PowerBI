using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000AD RID: 173
	public abstract class RelationshipType : EntityTypeBase
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x0001D3D2 File Offset: 0x0001B5D2
		internal RelationshipType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0001D3E0 File Offset: 0x0001B5E0
		public ReadOnlyMetadataCollection<RelationshipEndMember> RelationshipEndMembers
		{
			get
			{
				if (this._relationshipEndMembers == null)
				{
					ReadOnlyMetadataCollection<EdmMember> members = base.Members;
					Predicate<EdmMember> predicate;
					if ((predicate = RelationshipType.<>O.<0>__IsRelationshipEndMember) == null)
					{
						predicate = (RelationshipType.<>O.<0>__IsRelationshipEndMember = new Predicate<EdmMember>(Helper.IsRelationshipEndMember));
					}
					FilteredReadOnlyMetadataCollection<RelationshipEndMember, EdmMember> filteredReadOnlyMetadataCollection = new FilteredReadOnlyMetadataCollection<RelationshipEndMember, EdmMember>(members, predicate);
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<RelationshipEndMember>>(ref this._relationshipEndMembers, filteredReadOnlyMetadataCollection, null);
				}
				return this._relationshipEndMembers;
			}
		}

		// Token: 0x040008AD RID: 2221
		private ReadOnlyMetadataCollection<RelationshipEndMember> _relationshipEndMembers;

		// Token: 0x020002C5 RID: 709
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000FDC RID: 4060
			public static Predicate<EdmMember> <0>__IsRelationshipEndMember;
		}
	}
}
