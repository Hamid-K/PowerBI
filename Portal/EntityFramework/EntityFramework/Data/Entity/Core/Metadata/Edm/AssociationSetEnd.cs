using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000488 RID: 1160
	public sealed class AssociationSetEnd : MetadataItem
	{
		// Token: 0x06003993 RID: 14739 RVA: 0x000BD98F File Offset: 0x000BBB8F
		internal AssociationSetEnd(EntitySet entitySet, AssociationSet parentSet, AssociationEndMember endMember)
		{
			this._entitySet = Check.NotNull<EntitySet>(entitySet, "entitySet");
			this._parentSet = Check.NotNull<AssociationSet>(parentSet, "parentSet");
			this._endMember = Check.NotNull<AssociationEndMember>(endMember, "endMember");
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x000BD9CA File Offset: 0x000BBBCA
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationSetEnd;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06003995 RID: 14741 RVA: 0x000BD9CD File Offset: 0x000BBBCD
		[MetadataProperty(BuiltInTypeKind.AssociationSet, false)]
		public AssociationSet ParentAssociationSet
		{
			get
			{
				return this._parentSet;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x000BD9D5 File Offset: 0x000BBBD5
		[MetadataProperty(BuiltInTypeKind.AssociationEndMember, false)]
		public AssociationEndMember CorrespondingAssociationEndMember
		{
			get
			{
				return this._endMember;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06003997 RID: 14743 RVA: 0x000BD9DD File Offset: 0x000BBBDD
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this.CorrespondingAssociationEndMember.Name;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x000BD9EA File Offset: 0x000BBBEA
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		[Obsolete("This property is going away, please use the Name property instead")]
		public string Role
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x000BD9F2 File Offset: 0x000BBBF2
		[MetadataProperty(BuiltInTypeKind.EntitySet, false)]
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600399A RID: 14746 RVA: 0x000BD9FA File Offset: 0x000BBBFA
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000BDA02 File Offset: 0x000BBC02
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				AssociationSet parentAssociationSet = this.ParentAssociationSet;
				if (parentAssociationSet != null)
				{
					parentAssociationSet.SetReadOnly();
				}
				AssociationEndMember correspondingAssociationEndMember = this.CorrespondingAssociationEndMember;
				if (correspondingAssociationEndMember != null)
				{
					correspondingAssociationEndMember.SetReadOnly();
				}
				EntitySet entitySet = this.EntitySet;
				if (entitySet != null)
				{
					entitySet.SetReadOnly();
				}
			}
		}

		// Token: 0x04001310 RID: 4880
		private readonly EntitySet _entitySet;

		// Token: 0x04001311 RID: 4881
		private readonly AssociationSet _parentSet;

		// Token: 0x04001312 RID: 4882
		private readonly AssociationEndMember _endMember;
	}
}
