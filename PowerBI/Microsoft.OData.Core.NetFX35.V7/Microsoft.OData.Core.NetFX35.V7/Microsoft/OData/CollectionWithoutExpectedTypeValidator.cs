using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000009 RID: 9
	internal sealed class CollectionWithoutExpectedTypeValidator
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000027DB File Offset: 0x000009DB
		internal CollectionWithoutExpectedTypeValidator(string itemTypeNameFromCollection)
		{
			if (itemTypeNameFromCollection != null)
			{
				this.itemTypeName = CollectionWithoutExpectedTypeValidator.GetItemTypeFullName(itemTypeNameFromCollection);
				this.itemTypeKind = CollectionWithoutExpectedTypeValidator.ComputeExpectedTypeKind(this.itemTypeName, out this.primitiveItemType);
				this.itemTypeDerivedFromCollectionValue = true;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002810 File Offset: 0x00000A10
		internal string ItemTypeNameFromCollection
		{
			get
			{
				if (!this.itemTypeDerivedFromCollectionValue)
				{
					return null;
				}
				return this.itemTypeName;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002822 File Offset: 0x00000A22
		internal EdmTypeKind ItemTypeKindFromCollection
		{
			get
			{
				if (!this.itemTypeDerivedFromCollectionValue)
				{
					return EdmTypeKind.None;
				}
				return this.itemTypeKind;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002834 File Offset: 0x00000A34
		internal void ValidateCollectionItem(string collectionItemTypeName, EdmTypeKind collectionItemTypeKind)
		{
			if (collectionItemTypeKind != EdmTypeKind.Primitive && collectionItemTypeKind != EdmTypeKind.Enum && collectionItemTypeKind != EdmTypeKind.Complex)
			{
				throw new ODataException(Strings.CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind(collectionItemTypeKind));
			}
			if (this.itemTypeDerivedFromCollectionValue)
			{
				collectionItemTypeName = collectionItemTypeName ?? this.itemTypeName;
				this.ValidateCollectionItemTypeNameAndKind(collectionItemTypeName, collectionItemTypeKind);
				return;
			}
			if (this.itemTypeKind == EdmTypeKind.None)
			{
				this.itemTypeKind = ((collectionItemTypeName == null) ? collectionItemTypeKind : CollectionWithoutExpectedTypeValidator.ComputeExpectedTypeKind(collectionItemTypeName, out this.primitiveItemType));
				if (collectionItemTypeName == null)
				{
					this.itemTypeKind = collectionItemTypeKind;
					if (this.itemTypeKind == EdmTypeKind.Primitive)
					{
						this.itemTypeName = "Edm.String";
						this.primitiveItemType = EdmCoreModel.Instance.GetString(false).PrimitiveDefinition();
					}
					else
					{
						this.itemTypeName = null;
						this.primitiveItemType = null;
					}
				}
				else
				{
					this.itemTypeKind = CollectionWithoutExpectedTypeValidator.ComputeExpectedTypeKind(collectionItemTypeName, out this.primitiveItemType);
					this.itemTypeName = collectionItemTypeName;
				}
			}
			if (collectionItemTypeName == null && collectionItemTypeKind == EdmTypeKind.Primitive)
			{
				collectionItemTypeName = "Edm.String";
			}
			this.ValidateCollectionItemTypeNameAndKind(collectionItemTypeName, collectionItemTypeKind);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002914 File Offset: 0x00000B14
		private static EdmTypeKind ComputeExpectedTypeKind(string typeName, out IEdmPrimitiveType primitiveItemType)
		{
			IEdmSchemaType edmSchemaType = EdmCoreModel.Instance.FindDeclaredType(typeName);
			if (edmSchemaType != null)
			{
				primitiveItemType = (IEdmPrimitiveType)edmSchemaType;
				return EdmTypeKind.Primitive;
			}
			primitiveItemType = null;
			return EdmTypeKind.Complex;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002940 File Offset: 0x00000B40
		private static string GetItemTypeFullName(string typeName)
		{
			IEdmSchemaType edmSchemaType = EdmCoreModel.Instance.FindDeclaredType(typeName);
			if (edmSchemaType != null)
			{
				return edmSchemaType.FullName();
			}
			return typeName;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002964 File Offset: 0x00000B64
		private void ValidateCollectionItemTypeNameAndKind(string collectionItemTypeName, EdmTypeKind collectionItemTypeKind)
		{
			if (this.itemTypeKind != collectionItemTypeKind)
			{
				throw new ODataException(Strings.CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind(collectionItemTypeKind, this.itemTypeKind));
			}
			if (this.itemTypeKind == EdmTypeKind.Primitive)
			{
				if (!string.IsNullOrEmpty(this.itemTypeName) && this.itemTypeName.Equals(collectionItemTypeName, 5))
				{
					return;
				}
				if (this.primitiveItemType.IsSpatial())
				{
					EdmPrimitiveTypeKind primitiveTypeKind = EdmCoreModel.Instance.GetPrimitiveTypeKind(collectionItemTypeName);
					IEdmPrimitiveType primitiveType = EdmCoreModel.Instance.GetPrimitiveType(primitiveTypeKind);
					if (this.itemTypeDerivedFromCollectionValue)
					{
						if (this.primitiveItemType.IsAssignableFrom(primitiveType))
						{
							return;
						}
					}
					else
					{
						IEdmPrimitiveType commonBaseType = this.primitiveItemType.GetCommonBaseType(primitiveType);
						if (commonBaseType != null)
						{
							this.primitiveItemType = commonBaseType;
							this.itemTypeName = commonBaseType.FullTypeName();
							return;
						}
					}
				}
				throw new ODataException(Strings.CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName(collectionItemTypeName, this.itemTypeName));
			}
			else
			{
				if (string.CompareOrdinal(this.itemTypeName, collectionItemTypeName) != 0)
				{
					throw new ODataException(Strings.CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName(collectionItemTypeName, this.itemTypeName));
				}
				return;
			}
		}

		// Token: 0x0400001C RID: 28
		private readonly bool itemTypeDerivedFromCollectionValue;

		// Token: 0x0400001D RID: 29
		private string itemTypeName;

		// Token: 0x0400001E RID: 30
		private IEdmPrimitiveType primitiveItemType;

		// Token: 0x0400001F RID: 31
		private EdmTypeKind itemTypeKind;
	}
}
