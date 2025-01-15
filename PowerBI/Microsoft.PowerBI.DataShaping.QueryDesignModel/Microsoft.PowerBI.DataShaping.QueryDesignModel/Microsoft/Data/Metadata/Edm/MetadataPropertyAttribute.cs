using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200009D RID: 157
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	internal sealed class MetadataPropertyAttribute : Attribute
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x0001B21D File Offset: 0x0001941D
		internal MetadataPropertyAttribute(BuiltInTypeKind builtInTypeKind, bool isCollectionType)
			: this(MetadataItem.GetBuiltInType(builtInTypeKind), isCollectionType)
		{
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0001B22C File Offset: 0x0001942C
		internal MetadataPropertyAttribute(PrimitiveTypeKind primitiveTypeKind, bool isCollectionType)
			: this(MetadataItem.EdmProviderManifest.GetPrimitiveType(primitiveTypeKind), isCollectionType)
		{
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001B240 File Offset: 0x00019440
		internal MetadataPropertyAttribute(Type type, bool isCollection)
			: this(ClrComplexType.CreateReadonlyClrComplexType(type, type.Namespace ?? string.Empty, type.Name), isCollection)
		{
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001B264 File Offset: 0x00019464
		private MetadataPropertyAttribute(EdmType type, bool isCollectionType)
		{
			this._type = type;
			this._isCollectionType = isCollectionType;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0001B27A File Offset: 0x0001947A
		internal EdmType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0001B282 File Offset: 0x00019482
		internal bool IsCollectionType
		{
			get
			{
				return this._isCollectionType;
			}
		}

		// Token: 0x0400086B RID: 2155
		private readonly EdmType _type;

		// Token: 0x0400086C RID: 2156
		private readonly bool _isCollectionType;
	}
}
