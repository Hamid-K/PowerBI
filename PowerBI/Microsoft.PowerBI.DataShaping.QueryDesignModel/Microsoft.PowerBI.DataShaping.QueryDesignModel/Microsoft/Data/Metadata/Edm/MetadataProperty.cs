using System;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200009C RID: 156
	public sealed class MetadataProperty : MetadataItem
	{
		// Token: 0x06000B01 RID: 2817 RVA: 0x0001B137 File Offset: 0x00019337
		internal MetadataProperty(string name, TypeUsage typeUsage, object value)
		{
			EntityUtil.GenericCheckArgumentNull<TypeUsage>(typeUsage, "typeUsage");
			this._name = name;
			this._value = value;
			this._typeUsage = typeUsage;
			this._propertyKind = PropertyKind.Extended;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0001B168 File Offset: 0x00019368
		internal MetadataProperty(string name, EdmType edmType, bool isCollectionType, object value)
		{
			EntityUtil.CheckArgumentNull<EdmType>(edmType, "edmType");
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

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0001B1BF File Offset: 0x000193BF
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataProperty;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0001B1C3 File Offset: 0x000193C3
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0001B1CB File Offset: 0x000193CB
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0001B1D4 File Offset: 0x000193D4
		[MetadataProperty(typeof(object), false)]
		public object Value
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
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0001B1FD File Offset: 0x000193FD
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001B205 File Offset: 0x00019405
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0001B215 File Offset: 0x00019415
		public PropertyKind PropertyKind
		{
			get
			{
				return this._propertyKind;
			}
		}

		// Token: 0x04000867 RID: 2151
		private string _name;

		// Token: 0x04000868 RID: 2152
		private PropertyKind _propertyKind;

		// Token: 0x04000869 RID: 2153
		private object _value;

		// Token: 0x0400086A RID: 2154
		private TypeUsage _typeUsage;
	}
}
