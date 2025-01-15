using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000489 RID: 1161
	public class AssociationType : RelationshipType
	{
		// Token: 0x0600399D RID: 14749 RVA: 0x000BDA57 File Offset: 0x000BBC57
		internal AssociationType(string name, string namespaceName, bool foreignKey, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
			this._referentialConstraints = new ReadOnlyMetadataCollection<ReferentialConstraint>(new MetadataCollection<ReferentialConstraint>());
			this._isForeignKey = foreignKey;
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000BDA83 File Offset: 0x000BBC83
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationType;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x000BDA86 File Offset: 0x000BBC86
		public ReadOnlyMetadataCollection<AssociationEndMember> AssociationEndMembers
		{
			get
			{
				if (this._associationEndMembers == null)
				{
					Interlocked.CompareExchange<FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember>>(ref this._associationEndMembers, new FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember>(this.KeyMembers, new Predicate<EdmMember>(Helper.IsAssociationEndMember)), null);
				}
				return this._associationEndMembers;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x000BDABA File Offset: 0x000BBCBA
		// (set) Token: 0x060039A1 RID: 14753 RVA: 0x000BDAC8 File Offset: 0x000BBCC8
		public ReferentialConstraint Constraint
		{
			get
			{
				return this.ReferentialConstraints.SingleOrDefault<ReferentialConstraint>();
			}
			set
			{
				Check.NotNull<ReferentialConstraint>(value, "value");
				Util.ThrowIfReadOnly(this);
				ReferentialConstraint constraint = this.Constraint;
				if (constraint != null)
				{
					this.ReferentialConstraints.Source.Remove(constraint);
				}
				this.AddReferentialConstraint(value);
				this._isForeignKey = true;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060039A2 RID: 14754 RVA: 0x000BDB11 File Offset: 0x000BBD11
		// (set) Token: 0x060039A3 RID: 14755 RVA: 0x000BDB23 File Offset: 0x000BBD23
		internal AssociationEndMember SourceEnd
		{
			get
			{
				return this.KeyMembers.FirstOrDefault<EdmMember>() as AssociationEndMember;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (this.KeyMembers.Count == 0)
				{
					base.AddKeyMember(value);
					return;
				}
				this.SetKeyMember(0, value);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000BDB48 File Offset: 0x000BBD48
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x000BDB5B File Offset: 0x000BBD5B
		internal AssociationEndMember TargetEnd
		{
			get
			{
				return this.KeyMembers.ElementAtOrDefault(1) as AssociationEndMember;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (this.KeyMembers.Count == 1)
				{
					base.AddKeyMember(value);
					return;
				}
				this.SetKeyMember(1, value);
			}
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000BDB84 File Offset: 0x000BBD84
		private void SetKeyMember(int index, AssociationEndMember member)
		{
			EdmMember edmMember = this.KeyMembers.Source[index];
			int num = base.Members.IndexOf(edmMember);
			if (num >= 0)
			{
				base.Members.Source[num] = member;
			}
			this.KeyMembers.Source[index] = member;
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x000BDBD8 File Offset: 0x000BBDD8
		[MetadataProperty(BuiltInTypeKind.ReferentialConstraint, true)]
		public ReadOnlyMetadataCollection<ReferentialConstraint> ReferentialConstraints
		{
			get
			{
				return this._referentialConstraints;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x000BDBE0 File Offset: 0x000BBDE0
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000BDBE8 File Offset: 0x000BBDE8
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x000BDBEA File Offset: 0x000BBDEA
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.ReferentialConstraints.Source.SetReadOnly();
			}
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x000BDC0B File Offset: 0x000BBE0B
		internal void AddReferentialConstraint(ReferentialConstraint referentialConstraint)
		{
			this.ReferentialConstraints.Source.Add(referentialConstraint);
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000BDC20 File Offset: 0x000BBE20
		public static AssociationType Create(string name, string namespaceName, bool foreignKey, DataSpace dataSpace, AssociationEndMember sourceEnd, AssociationEndMember targetEnd, ReferentialConstraint constraint, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			AssociationType associationType = new AssociationType(name, namespaceName, foreignKey, dataSpace);
			if (sourceEnd != null)
			{
				associationType.SourceEnd = sourceEnd;
			}
			if (targetEnd != null)
			{
				associationType.TargetEnd = targetEnd;
			}
			if (constraint != null)
			{
				associationType.AddReferentialConstraint(constraint);
			}
			if (metadataProperties != null)
			{
				associationType.AddMetadataProperties(metadataProperties);
			}
			associationType.SetReadOnly();
			return associationType;
		}

		// Token: 0x04001313 RID: 4883
		internal volatile int Index = -1;

		// Token: 0x04001314 RID: 4884
		private readonly ReadOnlyMetadataCollection<ReferentialConstraint> _referentialConstraints;

		// Token: 0x04001315 RID: 4885
		private FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember> _associationEndMembers;

		// Token: 0x04001316 RID: 4886
		private bool _isForeignKey;
	}
}
