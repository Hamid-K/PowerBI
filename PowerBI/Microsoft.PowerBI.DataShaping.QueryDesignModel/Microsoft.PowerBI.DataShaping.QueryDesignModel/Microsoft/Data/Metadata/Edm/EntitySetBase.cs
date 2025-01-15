using System;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000088 RID: 136
	public abstract class EntitySetBase : MetadataItem
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x00017E44 File Offset: 0x00016044
		internal EntitySetBase(string name, string schema, string table, string definingQuery, EntityTypeBase entityType)
		{
			EntityUtil.GenericCheckArgumentNull<EntityTypeBase>(entityType, "entityType");
			EntityUtil.CheckStringArgument(name, "name");
			this._name = name;
			this._schema = schema;
			this._table = table;
			this._definingQuery = definingQuery;
			this.ElementType = entityType;
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00017E94 File Offset: 0x00016094
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntitySetBase;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00017E97 File Offset: 0x00016097
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00017E9F File Offset: 0x0001609F
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00017EA7 File Offset: 0x000160A7
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		internal string DefiningQuery
		{
			get
			{
				return this._definingQuery;
			}
			set
			{
				this._definingQuery = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00017EB0 File Offset: 0x000160B0
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x00017EB8 File Offset: 0x000160B8
		internal string CachedProviderSql
		{
			get
			{
				return this._cachedProviderSql;
			}
			set
			{
				this._cachedProviderSql = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00017EC1 File Offset: 0x000160C1
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00017EC9 File Offset: 0x000160C9
		public EntityContainer EntityContainer
		{
			get
			{
				return this._entityContainer;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00017ED1 File Offset: 0x000160D1
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x00017ED9 File Offset: 0x000160D9
		[MetadataProperty(BuiltInTypeKind.EntityTypeBase, false)]
		public EntityTypeBase ElementType
		{
			get
			{
				return this._elementType;
			}
			internal set
			{
				EntityUtil.GenericCheckArgumentNull<EntityTypeBase>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._elementType = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00017EF4 File Offset: 0x000160F4
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		internal string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00017EFC File Offset: 0x000160FC
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		internal string Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00017F04 File Offset: 0x00016104
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00017F0C File Offset: 0x0001610C
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

		// Token: 0x06000A15 RID: 2581 RVA: 0x00017F37 File Offset: 0x00016137
		internal void ChangeEntityContainerWithoutCollectionFixup(EntityContainer newEntityContainer)
		{
			this._entityContainer = newEntityContainer;
		}

		// Token: 0x0400081F RID: 2079
		private EntityContainer _entityContainer;

		// Token: 0x04000820 RID: 2080
		private string _name;

		// Token: 0x04000821 RID: 2081
		private EntityTypeBase _elementType;

		// Token: 0x04000822 RID: 2082
		private string _table;

		// Token: 0x04000823 RID: 2083
		private string _schema;

		// Token: 0x04000824 RID: 2084
		private string _definingQuery;

		// Token: 0x04000825 RID: 2085
		private string _cachedProviderSql;
	}
}
