using System;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004A8 RID: 1192
	public abstract class EdmMember : MetadataItem, INamedDataModelItem
	{
		// Token: 0x06003A89 RID: 14985 RVA: 0x000C1538 File Offset: 0x000BF738
		internal EdmMember()
		{
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000C1540 File Offset: 0x000BF740
		internal EdmMember(string name, TypeUsage memberTypeUsage)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(memberTypeUsage, "memberTypeUsage");
			this._name = name;
			this._typeUsage = memberTypeUsage;
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003A8B RID: 14987 RVA: 0x000C156E File Offset: 0x000BF76E
		string INamedDataModelItem.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000C1576 File Offset: 0x000BF776
		internal override string Identity
		{
			get
			{
				return this._identity ?? this.Name;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003A8D RID: 14989 RVA: 0x000C1588 File Offset: 0x000BF788
		// (set) Token: 0x06003A8E RID: 14990 RVA: 0x000C1590 File Offset: 0x000BF790
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				Check.NotEmpty(value, "value");
				Util.ThrowIfReadOnly(this);
				if (!string.Equals(this._name, value, StringComparison.Ordinal))
				{
					string identity = this.Identity;
					this._name = value;
					if (this._declaringType != null)
					{
						if (this._declaringType.Members.Except(new EdmMember[] { this }).Any((EdmMember c) => string.Equals(this.Identity, c.Identity, StringComparison.Ordinal)))
						{
							this._identity = this._declaringType.Members.Select((EdmMember i) => i.Identity).Uniquify(this.Identity);
						}
						this._declaringType.NotifyItemIdentityChanged(this, identity);
					}
				}
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003A8F RID: 14991 RVA: 0x000C1651 File Offset: 0x000BF851
		public virtual StructuralType DeclaringType
		{
			get
			{
				return this._declaringType;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x000C1659 File Offset: 0x000BF859
		// (set) Token: 0x06003A91 RID: 14993 RVA: 0x000C1661 File Offset: 0x000BF861
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public virtual TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
			protected set
			{
				Check.NotNull<TypeUsage>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._typeUsage = value;
			}
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x000C167C File Offset: 0x000BF87C
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x000C1684 File Offset: 0x000BF884
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				string identity = this._identity;
				this._identity = this.Name;
				if (this._declaringType != null && identity != null && !string.Equals(identity, this._identity, StringComparison.Ordinal))
				{
					this._declaringType.NotifyItemIdentityChanged(this, identity);
				}
			}
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000C16D9 File Offset: 0x000BF8D9
		internal void ChangeDeclaringTypeWithoutCollectionFixup(StructuralType newDeclaringType)
		{
			this._declaringType = newDeclaringType;
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06003A95 RID: 14997 RVA: 0x000C16E4 File Offset: 0x000BF8E4
		public bool IsStoreGeneratedComputed
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && (StoreGeneratedPattern)facet.Value == StoreGeneratedPattern.Computed;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06003A96 RID: 14998 RVA: 0x000C171C File Offset: 0x000BF91C
		public bool IsStoreGeneratedIdentity
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && (StoreGeneratedPattern)facet.Value == StoreGeneratedPattern.Identity;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06003A97 RID: 14999 RVA: 0x000C1754 File Offset: 0x000BF954
		internal virtual bool IsPrimaryKeyColumn
		{
			get
			{
				EntityTypeBase entityTypeBase = this._declaringType as EntityTypeBase;
				return entityTypeBase != null && entityTypeBase.KeyMembers.Contains(this);
			}
		}

		// Token: 0x04001422 RID: 5154
		private StructuralType _declaringType;

		// Token: 0x04001423 RID: 5155
		private TypeUsage _typeUsage;

		// Token: 0x04001424 RID: 5156
		private string _name;

		// Token: 0x04001425 RID: 5157
		private string _identity;
	}
}
