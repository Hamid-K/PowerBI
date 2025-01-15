using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x0200002F RID: 47
	internal sealed class CollectionWithoutExpectedTypeValidator
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00004857 File Offset: 0x00002A57
		internal CollectionWithoutExpectedTypeValidator(string itemTypeNameFromCollection)
		{
			if (itemTypeNameFromCollection != null)
			{
				this.itemTypeName = CollectionWithoutExpectedTypeValidator.GetItemTypeFullName(itemTypeNameFromCollection);
				this.itemTypeKind = CollectionWithoutExpectedTypeValidator.ComputeExpectedTypeKind(this.itemTypeName, out this.primitiveItemType);
				this.itemTypeDerivedFromCollectionValue = true;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000488C File Offset: 0x00002A8C
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000489E File Offset: 0x00002A9E
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

		// Token: 0x060001B0 RID: 432 RVA: 0x000048B0 File Offset: 0x00002AB0
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

		// Token: 0x060001B1 RID: 433 RVA: 0x00004990 File Offset: 0x00002B90
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

		// Token: 0x060001B2 RID: 434 RVA: 0x000049BC File Offset: 0x00002BBC
		private static string GetItemTypeFullName(string typeName)
		{
			IEdmSchemaType edmSchemaType = EdmCoreModel.Instance.FindDeclaredType(typeName);
			if (edmSchemaType != null)
			{
				return edmSchemaType.FullName();
			}
			return typeName;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000049E0 File Offset: 0x00002BE0
		private void ValidateCollectionItemTypeNameAndKind(string collectionItemTypeName, EdmTypeKind collectionItemTypeKind)
		{
			if (this.itemTypeKind != collectionItemTypeKind)
			{
				throw new ODataException(Strings.CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind(collectionItemTypeKind, this.itemTypeKind));
			}
			if (this.itemTypeKind == EdmTypeKind.Primitive)
			{
				if (!string.IsNullOrEmpty(this.itemTypeName) && this.itemTypeName.Equals(collectionItemTypeName, StringComparison.OrdinalIgnoreCase))
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

		// Token: 0x04000089 RID: 137
		private readonly bool itemTypeDerivedFromCollectionValue;

		// Token: 0x0400008A RID: 138
		private string itemTypeName;

		// Token: 0x0400008B RID: 139
		private IEdmPrimitiveType primitiveItemType;

		// Token: 0x0400008C RID: 140
		private EdmTypeKind itemTypeKind;
	}
}
