using System;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F5 RID: 1269
	public abstract class RelationshipType : EntityTypeBase
	{
		// Token: 0x06003EDE RID: 16094 RVA: 0x000D10F0 File Offset: 0x000CF2F0
		internal RelationshipType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003EDF RID: 16095 RVA: 0x000D10FC File Offset: 0x000CF2FC
		public ReadOnlyMetadataCollection<RelationshipEndMember> RelationshipEndMembers
		{
			get
			{
				if (this._relationshipEndMembers == null)
				{
					FilteredReadOnlyMetadataCollection<RelationshipEndMember, EdmMember> filteredReadOnlyMetadataCollection = new FilteredReadOnlyMetadataCollection<RelationshipEndMember, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsRelationshipEndMember));
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<RelationshipEndMember>>(ref this._relationshipEndMembers, filteredReadOnlyMetadataCollection, null);
				}
				return this._relationshipEndMembers;
			}
		}

		// Token: 0x04001577 RID: 5495
		private ReadOnlyMetadataCollection<RelationshipEndMember> _relationshipEndMembers;
	}
}
