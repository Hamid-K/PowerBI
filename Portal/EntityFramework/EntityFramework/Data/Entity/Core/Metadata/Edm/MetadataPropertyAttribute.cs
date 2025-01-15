using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DD RID: 1245
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	internal sealed class MetadataPropertyAttribute : Attribute
	{
		// Token: 0x06003DDF RID: 15839 RVA: 0x000CD7BF File Offset: 0x000CB9BF
		internal MetadataPropertyAttribute(BuiltInTypeKind builtInTypeKind, bool isCollectionType)
			: this(MetadataItem.GetBuiltInType(builtInTypeKind), isCollectionType)
		{
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x000CD7CE File Offset: 0x000CB9CE
		internal MetadataPropertyAttribute(PrimitiveTypeKind primitiveTypeKind, bool isCollectionType)
			: this(MetadataItem.EdmProviderManifest.GetPrimitiveType(primitiveTypeKind), isCollectionType)
		{
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x000CD7E2 File Offset: 0x000CB9E2
		internal MetadataPropertyAttribute(Type type, bool isCollection)
			: this(ClrComplexType.CreateReadonlyClrComplexType(type, type.NestingNamespace() ?? string.Empty, type.Name), isCollection)
		{
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x000CD806 File Offset: 0x000CBA06
		private MetadataPropertyAttribute(EdmType type, bool isCollectionType)
		{
			this._type = type;
			this._isCollectionType = isCollectionType;
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06003DE3 RID: 15843 RVA: 0x000CD81C File Offset: 0x000CBA1C
		internal EdmType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x000CD824 File Offset: 0x000CBA24
		internal bool IsCollectionType
		{
			get
			{
				return this._isCollectionType;
			}
		}

		// Token: 0x04001513 RID: 5395
		private readonly EdmType _type;

		// Token: 0x04001514 RID: 5396
		private readonly bool _isCollectionType;
	}
}
