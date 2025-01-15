using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004EF RID: 1263
	public sealed class ReferentialConstraint : MetadataItem
	{
		// Token: 0x06003EBD RID: 16061 RVA: 0x000D0C70 File Offset: 0x000CEE70
		public ReferentialConstraint(RelationshipEndMember fromRole, RelationshipEndMember toRole, IEnumerable<EdmProperty> fromProperties, IEnumerable<EdmProperty> toProperties)
		{
			Check.NotNull<RelationshipEndMember>(fromRole, "fromRole");
			Check.NotNull<RelationshipEndMember>(toRole, "toRole");
			Check.NotNull<IEnumerable<EdmProperty>>(fromProperties, "fromProperties");
			Check.NotNull<IEnumerable<EdmProperty>>(toProperties, "toProperties");
			this._fromRole = fromRole;
			this._toRole = toRole;
			this._fromProperties = new ReadOnlyMetadataCollection<EdmProperty>(new MetadataCollection<EdmProperty>(fromProperties));
			this._toProperties = new ReadOnlyMetadataCollection<EdmProperty>(new MetadataCollection<EdmProperty>(toProperties));
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x000D0CE5 File Offset: 0x000CEEE5
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.ReferentialConstraint;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06003EBF RID: 16063 RVA: 0x000D0CE9 File Offset: 0x000CEEE9
		internal override string Identity
		{
			get
			{
				return this.FromRole.Name + "_" + this.ToRole.Name;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x000D0D0B File Offset: 0x000CEF0B
		// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x000D0D13 File Offset: 0x000CEF13
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember FromRole
		{
			get
			{
				return this._fromRole;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._fromRole = value;
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x000D0D22 File Offset: 0x000CEF22
		// (set) Token: 0x06003EC3 RID: 16067 RVA: 0x000D0D2A File Offset: 0x000CEF2A
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember ToRole
		{
			get
			{
				return this._toRole;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._toRole = value;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x000D0D39 File Offset: 0x000CEF39
		internal AssociationEndMember PrincipalEnd
		{
			get
			{
				return (AssociationEndMember)this.FromRole;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x000D0D46 File Offset: 0x000CEF46
		internal AssociationEndMember DependentEnd
		{
			get
			{
				return (AssociationEndMember)this.ToRole;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x000D0D53 File Offset: 0x000CEF53
		[MetadataProperty(BuiltInTypeKind.EdmProperty, true)]
		public ReadOnlyMetadataCollection<EdmProperty> FromProperties
		{
			get
			{
				if (!base.IsReadOnly && this._fromProperties.Count == 0)
				{
					this._fromRole.GetEntityType().KeyMembers.Each(delegate(EdmMember p)
					{
						this._fromProperties.Source.Add((EdmProperty)p);
					});
				}
				return this._fromProperties;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06003EC7 RID: 16071 RVA: 0x000D0D91 File Offset: 0x000CEF91
		[MetadataProperty(BuiltInTypeKind.EdmProperty, true)]
		public ReadOnlyMetadataCollection<EdmProperty> ToProperties
		{
			get
			{
				return this._toProperties;
			}
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x000D0D99 File Offset: 0x000CEF99
		public override string ToString()
		{
			return this.FromRole.Name + "_" + this.ToRole.Name;
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000D0DBC File Offset: 0x000CEFBC
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				this.FromProperties.Source.SetReadOnly();
				this.ToProperties.Source.SetReadOnly();
				base.SetReadOnly();
				RelationshipEndMember fromRole = this.FromRole;
				if (fromRole != null)
				{
					fromRole.SetReadOnly();
				}
				RelationshipEndMember toRole = this.ToRole;
				if (toRole != null)
				{
					toRole.SetReadOnly();
				}
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x000D0E1C File Offset: 0x000CF01C
		internal string BuildConstraintExceptionMessage()
		{
			string name = this.FromProperties.First<EdmProperty>().DeclaringType.Name;
			string name2 = this.ToProperties.First<EdmProperty>().DeclaringType.Name;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < this.FromProperties.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
					stringBuilder2.Append(", ");
				}
				stringBuilder.Append(name).Append('.').Append(this.FromProperties[i]);
				stringBuilder2.Append(name2).Append('.').Append(this.ToProperties[i]);
			}
			return Strings.RelationshipManager_InconsistentReferentialConstraintProperties(stringBuilder.ToString(), stringBuilder2.ToString());
		}

		// Token: 0x0400156C RID: 5484
		private RelationshipEndMember _fromRole;

		// Token: 0x0400156D RID: 5485
		private RelationshipEndMember _toRole;

		// Token: 0x0400156E RID: 5486
		private readonly ReadOnlyMetadataCollection<EdmProperty> _fromProperties;

		// Token: 0x0400156F RID: 5487
		private readonly ReadOnlyMetadataCollection<EdmProperty> _toProperties;
	}
}
