using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000076 RID: 118
	public sealed class AssociationType : RelationshipType
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x00014F6B File Offset: 0x0001316B
		internal AssociationType(string name, string namespaceName, bool foreignKey, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
			this._referentialConstraints = new ReadOnlyMetadataCollection<ReferentialConstraint>(new MetadataCollection<ReferentialConstraint>());
			this._isForeignKey = foreignKey;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00014F8E File Offset: 0x0001318E
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationType;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00014F94 File Offset: 0x00013194
		public ReadOnlyMetadataCollection<AssociationEndMember> AssociationEndMembers
		{
			get
			{
				if (this._associationEndMembers == null)
				{
					ReadOnlyMetadataCollection<EdmMember> members = base.Members;
					Predicate<EdmMember> predicate;
					if ((predicate = AssociationType.<>O.<0>__IsAssociationEndMember) == null)
					{
						predicate = (AssociationType.<>O.<0>__IsAssociationEndMember = new Predicate<EdmMember>(Helper.IsAssociationEndMember));
					}
					Interlocked.CompareExchange<FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember>>(ref this._associationEndMembers, new FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember>(members, predicate), null);
				}
				return this._associationEndMembers;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00014FE2 File Offset: 0x000131E2
		[MetadataProperty(BuiltInTypeKind.ReferentialConstraint, true)]
		public ReadOnlyMetadataCollection<ReferentialConstraint> ReferentialConstraints
		{
			get
			{
				return this._referentialConstraints;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00014FEA File Offset: 0x000131EA
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00014FF2 File Offset: 0x000131F2
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00014FF4 File Offset: 0x000131F4
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.ReferentialConstraints.Source.SetReadOnly();
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00015015 File Offset: 0x00013215
		internal void AddReferentialConstraint(ReferentialConstraint referentialConstraint)
		{
			this.ReferentialConstraints.Source.Add(referentialConstraint);
		}

		// Token: 0x04000735 RID: 1845
		private readonly ReadOnlyMetadataCollection<ReferentialConstraint> _referentialConstraints;

		// Token: 0x04000736 RID: 1846
		private FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember> _associationEndMembers;

		// Token: 0x04000737 RID: 1847
		private readonly bool _isForeignKey;

		// Token: 0x020002B0 RID: 688
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000FA0 RID: 4000
			public static Predicate<EdmMember> <0>__IsAssociationEndMember;
		}
	}
}
