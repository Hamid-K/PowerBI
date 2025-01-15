using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core
{
	// Token: 0x02000070 RID: 112
	internal sealed class CollectionWithoutExpectedTypeValidator
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x0001129B File Offset: 0x0000F49B
		internal CollectionWithoutExpectedTypeValidator(string itemTypeNameFromCollection)
		{
			if (itemTypeNameFromCollection != null)
			{
				this.itemTypeName = CollectionWithoutExpectedTypeValidator.GetItemTypeFullName(itemTypeNameFromCollection);
				this.itemTypeKind = CollectionWithoutExpectedTypeValidator.ComputeExpectedTypeKind(this.itemTypeName, out this.primitiveItemType);
				this.itemTypeDerivedFromCollectionValue = true;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000112D0 File Offset: 0x0000F4D0
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

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000112E2 File Offset: 0x0000F4E2
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

		// Token: 0x06000499 RID: 1177 RVA: 0x000112F4 File Offset: 0x0000F4F4
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

		// Token: 0x0600049A RID: 1178 RVA: 0x000113D4 File Offset: 0x0000F5D4
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

		// Token: 0x0600049B RID: 1179 RVA: 0x00011400 File Offset: 0x0000F600
		private static string GetItemTypeFullName(string typeName)
		{
			IEdmSchemaType edmSchemaType = EdmCoreModel.Instance.FindDeclaredType(typeName);
			if (edmSchemaType != null)
			{
				return edmSchemaType.FullName();
			}
			return typeName;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00011424 File Offset: 0x0000F624
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

		// Token: 0x0400020B RID: 523
		private readonly bool itemTypeDerivedFromCollectionValue;

		// Token: 0x0400020C RID: 524
		private string itemTypeName;

		// Token: 0x0400020D RID: 525
		private IEdmPrimitiveType primitiveItemType;

		// Token: 0x0400020E RID: 526
		private EdmTypeKind itemTypeKind;
	}
}
