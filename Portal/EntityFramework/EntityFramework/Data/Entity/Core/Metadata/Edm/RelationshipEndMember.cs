using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F1 RID: 1265
	public abstract class RelationshipEndMember : EdmMember
	{
		// Token: 0x06003ED3 RID: 16083 RVA: 0x000D0FCF File Offset: 0x000CF1CF
		internal RelationshipEndMember(string name, RefType endRefType, RelationshipMultiplicity multiplicity)
			: base(name, TypeUsage.Create(endRefType, new FacetValues
			{
				Nullable = new bool?(false)
			}))
		{
			this._relationshipMultiplicity = multiplicity;
			this._deleteBehavior = OperationAction.None;
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06003ED4 RID: 16084 RVA: 0x000D1002 File Offset: 0x000CF202
		// (set) Token: 0x06003ED5 RID: 16085 RVA: 0x000D100A File Offset: 0x000CF20A
		[MetadataProperty(BuiltInTypeKind.OperationAction, true)]
		public OperationAction DeleteBehavior
		{
			get
			{
				return this._deleteBehavior;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._deleteBehavior = value;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06003ED6 RID: 16086 RVA: 0x000D1019 File Offset: 0x000CF219
		// (set) Token: 0x06003ED7 RID: 16087 RVA: 0x000D1021 File Offset: 0x000CF221
		[MetadataProperty(BuiltInTypeKind.RelationshipMultiplicity, false)]
		public RelationshipMultiplicity RelationshipMultiplicity
		{
			get
			{
				return this._relationshipMultiplicity;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._relationshipMultiplicity = value;
			}
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x000D1030 File Offset: 0x000CF230
		public EntityType GetEntityType()
		{
			if (this.TypeUsage == null)
			{
				return null;
			}
			return (EntityType)((RefType)this.TypeUsage.EdmType).ElementType;
		}

		// Token: 0x04001571 RID: 5489
		private OperationAction _deleteBehavior;

		// Token: 0x04001572 RID: 5490
		private RelationshipMultiplicity _relationshipMultiplicity;
	}
}
