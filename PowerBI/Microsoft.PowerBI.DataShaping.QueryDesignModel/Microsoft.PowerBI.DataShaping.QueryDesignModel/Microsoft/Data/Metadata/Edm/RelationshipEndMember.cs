using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000AA RID: 170
	public abstract class RelationshipEndMember : EdmMember
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x0001D33A File Offset: 0x0001B53A
		internal RelationshipEndMember(string name, RefType endRefType, RelationshipMultiplicity multiplicity)
			: base(name, TypeUsage.Create(endRefType, new FacetValues
			{
				Nullable = new bool?(false)
			}))
		{
			this._relationshipMultiplicity = multiplicity;
			this._deleteBehavior = OperationAction.None;
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0001D36D File Offset: 0x0001B56D
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x0001D375 File Offset: 0x0001B575
		[MetadataProperty(BuiltInTypeKind.OperationAction, true)]
		public OperationAction DeleteBehavior
		{
			get
			{
				return this._deleteBehavior;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this._deleteBehavior = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0001D384 File Offset: 0x0001B584
		[MetadataProperty(BuiltInTypeKind.RelationshipMultiplicity, false)]
		public RelationshipMultiplicity RelationshipMultiplicity
		{
			get
			{
				return this._relationshipMultiplicity;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0001D38C File Offset: 0x0001B58C
		public EntityType GetEntityType()
		{
			if (base.TypeUsage == null)
			{
				return null;
			}
			return (EntityType)((RefType)base.TypeUsage.EdmType).ElementType;
		}

		// Token: 0x040008A7 RID: 2215
		private OperationAction _deleteBehavior;

		// Token: 0x040008A8 RID: 2216
		private RelationshipMultiplicity _relationshipMultiplicity;
	}
}
