using System;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000075 RID: 117
	public sealed class AssociationSetEnd : MetadataItem
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x00014EA2 File Offset: 0x000130A2
		internal AssociationSetEnd(EntitySet entitySet, AssociationSet parentSet, AssociationEndMember endMember)
		{
			this._entitySet = EntityUtil.GenericCheckArgumentNull<EntitySet>(entitySet, "entitySet");
			this._parentSet = EntityUtil.GenericCheckArgumentNull<AssociationSet>(parentSet, "parentSet");
			this._endMember = EntityUtil.GenericCheckArgumentNull<AssociationEndMember>(endMember, "endMember");
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00014EDD File Offset: 0x000130DD
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationSetEnd;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00014EE0 File Offset: 0x000130E0
		[MetadataProperty(BuiltInTypeKind.AssociationSet, false)]
		public AssociationSet ParentAssociationSet
		{
			get
			{
				return this._parentSet;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00014EE8 File Offset: 0x000130E8
		[MetadataProperty(BuiltInTypeKind.AssociationEndMember, false)]
		public AssociationEndMember CorrespondingAssociationEndMember
		{
			get
			{
				return this._endMember;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00014EF0 File Offset: 0x000130F0
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this.CorrespondingAssociationEndMember.Name;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00014EFD File Offset: 0x000130FD
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		[Obsolete("This property is going away, please use the Name property instead")]
		public string Role
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00014F05 File Offset: 0x00013105
		[MetadataProperty(BuiltInTypeKind.EntitySet, false)]
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00014F0D File Offset: 0x0001310D
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00014F15 File Offset: 0x00013115
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00014F20 File Offset: 0x00013120
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

		// Token: 0x04000732 RID: 1842
		private readonly EntitySet _entitySet;

		// Token: 0x04000733 RID: 1843
		private readonly AssociationSet _parentSet;

		// Token: 0x04000734 RID: 1844
		private readonly AssociationEndMember _endMember;
	}
}
