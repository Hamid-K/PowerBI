using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E4 RID: 1252
	public sealed class NavigationProperty : EdmMember
	{
		// Token: 0x06003E5F RID: 15967 RVA: 0x000CFEA7 File Offset: 0x000CE0A7
		internal NavigationProperty(string name, TypeUsage typeUsage)
			: base(name, typeUsage)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			this._accessor = new NavigationPropertyAccessor(name);
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x000CFED5 File Offset: 0x000CE0D5
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.NavigationProperty;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06003E61 RID: 15969 RVA: 0x000CFED9 File Offset: 0x000CE0D9
		// (set) Token: 0x06003E62 RID: 15970 RVA: 0x000CFEE1 File Offset: 0x000CE0E1
		[MetadataProperty(BuiltInTypeKind.RelationshipType, false)]
		public RelationshipType RelationshipType { get; internal set; }

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06003E63 RID: 15971 RVA: 0x000CFEEA File Offset: 0x000CE0EA
		// (set) Token: 0x06003E64 RID: 15972 RVA: 0x000CFEF2 File Offset: 0x000CE0F2
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember ToEndMember { get; internal set; }

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06003E65 RID: 15973 RVA: 0x000CFEFB File Offset: 0x000CE0FB
		// (set) Token: 0x06003E66 RID: 15974 RVA: 0x000CFF03 File Offset: 0x000CE103
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember FromEndMember { get; internal set; }

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x000CFF0C File Offset: 0x000CE10C
		internal AssociationType Association
		{
			get
			{
				return (AssociationType)this.RelationshipType;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06003E68 RID: 15976 RVA: 0x000CFF19 File Offset: 0x000CE119
		internal AssociationEndMember ResultEnd
		{
			get
			{
				return (AssociationEndMember)this.ToEndMember;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x000CFF26 File Offset: 0x000CE126
		internal NavigationPropertyAccessor Accessor
		{
			get
			{
				return this._accessor;
			}
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000CFF30 File Offset: 0x000CE130
		public IEnumerable<EdmProperty> GetDependentProperties()
		{
			AssociationType associationType = (AssociationType)this.RelationshipType;
			if (associationType.ReferentialConstraints.Count > 0)
			{
				ReferentialConstraint referentialConstraint = associationType.ReferentialConstraints[0];
				if (referentialConstraint.ToRole.EdmEquals(this.FromEndMember))
				{
					ReadOnlyMetadataCollection<EdmMember> keyMembers = referentialConstraint.FromRole.GetEntityType().KeyMembers;
					List<EdmProperty> list = new List<EdmProperty>(keyMembers.Count);
					for (int i = 0; i < keyMembers.Count; i++)
					{
						list.Add(referentialConstraint.ToProperties[referentialConstraint.FromProperties.IndexOf((EdmProperty)keyMembers[i])]);
					}
					return new ReadOnlyCollection<EdmProperty>(list);
				}
			}
			return Enumerable.Empty<EdmProperty>();
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x000CFFE4 File Offset: 0x000CE1E4
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly && this.ToEndMember != null && this.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One)
			{
				this.TypeUsage = this.TypeUsage.ShallowCopy(new Facet[] { Facet.Create(MetadataItem.NullableFacetDescription, false) });
			}
			base.SetReadOnly();
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x000D0040 File Offset: 0x000CE240
		public static NavigationProperty Create(string name, TypeUsage typeUsage, RelationshipType relationshipType, RelationshipEndMember from, RelationshipEndMember to, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			NavigationProperty navigationProperty = new NavigationProperty(name, typeUsage);
			navigationProperty.RelationshipType = relationshipType;
			navigationProperty.FromEndMember = from;
			navigationProperty.ToEndMember = to;
			if (metadataProperties != null)
			{
				navigationProperty.AddMetadataProperties(metadataProperties);
			}
			navigationProperty.SetReadOnly();
			return navigationProperty;
		}

		// Token: 0x04001525 RID: 5413
		internal const string RelationshipTypeNamePropertyName = "RelationshipType";

		// Token: 0x04001526 RID: 5414
		internal const string ToEndMemberNamePropertyName = "ToEndMember";

		// Token: 0x04001527 RID: 5415
		private readonly NavigationPropertyAccessor _accessor;
	}
}
