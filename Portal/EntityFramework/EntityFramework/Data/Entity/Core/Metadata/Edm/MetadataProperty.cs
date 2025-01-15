using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DC RID: 1244
	public class MetadataProperty : MetadataItem
	{
		// Token: 0x06003DD0 RID: 15824 RVA: 0x000CD65D File Offset: 0x000CB85D
		internal MetadataProperty()
		{
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000CD665 File Offset: 0x000CB865
		internal MetadataProperty(string name, TypeUsage typeUsage, object value)
		{
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			this._name = name;
			this._value = value;
			this._typeUsage = typeUsage;
			this._propertyKind = PropertyKind.Extended;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000CD695 File Offset: 0x000CB895
		internal MetadataProperty(string name, EdmType edmType, bool isCollectionType, object value)
		{
			this._name = name;
			this._value = value;
			if (isCollectionType)
			{
				this._typeUsage = TypeUsage.Create(edmType.GetCollectionType());
			}
			else
			{
				this._typeUsage = TypeUsage.Create(edmType);
			}
			this._propertyKind = PropertyKind.System;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000CD6D5 File Offset: 0x000CB8D5
		private MetadataProperty(string name, object value)
		{
			this._name = name;
			this._value = value;
			this._propertyKind = PropertyKind.Extended;
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06003DD4 RID: 15828 RVA: 0x000CD6F2 File Offset: 0x000CB8F2
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataProperty;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x000CD6F6 File Offset: 0x000CB8F6
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06003DD6 RID: 15830 RVA: 0x000CD6FE File Offset: 0x000CB8FE
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x000CD708 File Offset: 0x000CB908
		// (set) Token: 0x06003DD8 RID: 15832 RVA: 0x000CD731 File Offset: 0x000CB931
		[MetadataProperty(typeof(object), false)]
		public virtual object Value
		{
			get
			{
				MetadataPropertyValue metadataPropertyValue = this._value as MetadataPropertyValue;
				if (metadataPropertyValue != null)
				{
					return metadataPropertyValue.GetValue();
				}
				return this._value;
			}
			set
			{
				Check.NotNull<object>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._value = value;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x000CD74C File Offset: 0x000CB94C
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000CD754 File Offset: 0x000CB954
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x000CD764 File Offset: 0x000CB964
		public virtual PropertyKind PropertyKind
		{
			get
			{
				return this._propertyKind;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06003DDC RID: 15836 RVA: 0x000CD76C File Offset: 0x000CB96C
		public bool IsAnnotation
		{
			get
			{
				return this.PropertyKind == PropertyKind.Extended && this.TypeUsage == null;
			}
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000CD782 File Offset: 0x000CB982
		public static MetadataProperty Create(string name, TypeUsage typeUsage, object value)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			MetadataProperty metadataProperty = new MetadataProperty(name, typeUsage, value);
			metadataProperty.SetReadOnly();
			return metadataProperty;
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000CD7AA File Offset: 0x000CB9AA
		public static MetadataProperty CreateAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			return new MetadataProperty(name, value);
		}

		// Token: 0x0400150F RID: 5391
		private readonly string _name;

		// Token: 0x04001510 RID: 5392
		private readonly PropertyKind _propertyKind;

		// Token: 0x04001511 RID: 5393
		private object _value;

		// Token: 0x04001512 RID: 5394
		private readonly TypeUsage _typeUsage;
	}
}
