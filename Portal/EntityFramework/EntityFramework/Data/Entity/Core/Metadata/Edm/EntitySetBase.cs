using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B9 RID: 1209
	public abstract class EntitySetBase : MetadataItem, INamedDataModelItem
	{
		// Token: 0x06003BBF RID: 15295 RVA: 0x000C64B9 File Offset: 0x000C46B9
		internal EntitySetBase()
		{
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000C64C4 File Offset: 0x000C46C4
		internal EntitySetBase(string name, string schema, string table, string definingQuery, EntityTypeBase entityType)
		{
			Check.NotNull<EntityTypeBase>(entityType, "entityType");
			Check.NotEmpty(name, "name");
			this._name = name;
			this._schema = schema;
			this._table = table;
			this._definingQuery = definingQuery;
			this.ElementType = entityType;
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x000C6515 File Offset: 0x000C4715
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntitySetBase;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x000C6518 File Offset: 0x000C4718
		string INamedDataModelItem.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x000C6520 File Offset: 0x000C4720
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06003BC4 RID: 15300 RVA: 0x000C6528 File Offset: 0x000C4728
		// (set) Token: 0x06003BC5 RID: 15301 RVA: 0x000C6530 File Offset: 0x000C4730
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string DefiningQuery
		{
			get
			{
				return this._definingQuery;
			}
			internal set
			{
				Check.NotEmpty(value, "value");
				Util.ThrowIfReadOnly(this);
				this._definingQuery = value;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06003BC6 RID: 15302 RVA: 0x000C654B File Offset: 0x000C474B
		// (set) Token: 0x06003BC7 RID: 15303 RVA: 0x000C6554 File Offset: 0x000C4754
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
					if (this._entityContainer != null)
					{
						this._entityContainer.NotifyItemIdentityChanged(this, identity);
					}
				}
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x000C65A5 File Offset: 0x000C47A5
		public virtual EntityContainer EntityContainer
		{
			get
			{
				return this._entityContainer;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06003BC9 RID: 15305 RVA: 0x000C65AD File Offset: 0x000C47AD
		// (set) Token: 0x06003BCA RID: 15306 RVA: 0x000C65B5 File Offset: 0x000C47B5
		[MetadataProperty(BuiltInTypeKind.EntityTypeBase, false)]
		public EntityTypeBase ElementType
		{
			get
			{
				return this._elementType;
			}
			internal set
			{
				Check.NotNull<EntityTypeBase>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._elementType = value;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06003BCB RID: 15307 RVA: 0x000C65D0 File Offset: 0x000C47D0
		// (set) Token: 0x06003BCC RID: 15308 RVA: 0x000C65D8 File Offset: 0x000C47D8
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Table
		{
			get
			{
				return this._table;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._table = value;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06003BCD RID: 15309 RVA: 0x000C65E7 File Offset: 0x000C47E7
		// (set) Token: 0x06003BCE RID: 15310 RVA: 0x000C65EF File Offset: 0x000C47EF
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._schema = value;
			}
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x000C65FE File Offset: 0x000C47FE
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x000C6608 File Offset: 0x000C4808
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				EntityTypeBase elementType = this.ElementType;
				if (elementType != null)
				{
					elementType.SetReadOnly();
				}
			}
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x000C6633 File Offset: 0x000C4833
		internal void ChangeEntityContainerWithoutCollectionFixup(EntityContainer newEntityContainer)
		{
			this._entityContainer = newEntityContainer;
		}

		// Token: 0x04001496 RID: 5270
		private EntityContainer _entityContainer;

		// Token: 0x04001497 RID: 5271
		private string _name;

		// Token: 0x04001498 RID: 5272
		private EntityTypeBase _elementType;

		// Token: 0x04001499 RID: 5273
		private string _table;

		// Token: 0x0400149A RID: 5274
		private string _schema;

		// Token: 0x0400149B RID: 5275
		private string _definingQuery;
	}
}
