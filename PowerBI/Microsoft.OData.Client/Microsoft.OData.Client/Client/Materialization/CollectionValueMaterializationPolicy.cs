using System;
using System.Collections;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x020000F9 RID: 249
	internal class CollectionValueMaterializationPolicy : MaterializationPolicy
	{
		// Token: 0x06000A89 RID: 2697 RVA: 0x000277B6 File Offset: 0x000259B6
		internal CollectionValueMaterializationPolicy(IODataMaterializerContext context, PrimitiveValueMaterializationPolicy primitivePolicy)
		{
			this.materializerContext = context;
			this.primitiveValueMaterializationPolicy = primitivePolicy;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x000277CC File Offset: 0x000259CC
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x000277D4 File Offset: 0x000259D4
		internal InstanceAnnotationMaterializationPolicy InstanceAnnotationMaterializationPolicy
		{
			get
			{
				return this.instanceAnnotationMaterializationPolicy;
			}
			set
			{
				this.instanceAnnotationMaterializationPolicy = value;
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000277E0 File Offset: 0x000259E0
		internal object CreateCollectionPropertyInstance(ODataProperty collectionProperty, Type userCollectionType)
		{
			ODataCollectionValue odataCollectionValue = collectionProperty.Value as ODataCollectionValue;
			ClientTypeAnnotation collectionClientType = this.materializerContext.ResolveTypeForMaterialization(userCollectionType, odataCollectionValue.TypeName);
			return this.CreateCollectionInstance(collectionClientType.EdmTypeReference as IEdmCollectionTypeReference, collectionClientType.ElementType, () => Strings.AtomMaterializer_NoParameterlessCtorForCollectionProperty(collectionProperty.Name, collectionClientType.ElementTypeName));
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00027850 File Offset: 0x00025A50
		internal object CreateCollectionInstance(IEdmCollectionTypeReference edmCollectionTypeReference, Type clientCollectionType)
		{
			return this.CreateCollectionInstance(edmCollectionTypeReference, clientCollectionType, () => Strings.AtomMaterializer_MaterializationTypeError(clientCollectionType.FullName));
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00027884 File Offset: 0x00025A84
		internal void ApplyCollectionDataValues(ODataProperty collectionProperty, object collectionInstance, Type collectionItemType, Action<object, object> addValueToBackingICollectionInstance, bool isElementNullable)
		{
			ODataCollectionValue odataCollectionValue = collectionProperty.Value as ODataCollectionValue;
			this.ApplyCollectionDataValues(odataCollectionValue.Items, odataCollectionValue.TypeName, collectionInstance, collectionItemType, addValueToBackingICollectionInstance, isElementNullable);
			collectionProperty.SetMaterializedValue(collectionInstance);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000278BC File Offset: 0x00025ABC
		internal void ApplyCollectionDataValues(IEnumerable items, string wireTypeName, object collectionInstance, Type collectionItemType, Action<object, object> addValueToBackingICollectionInstance, bool isElementNullable)
		{
			if (items != null)
			{
				bool flag = PrimitiveType.IsKnownNullableType(collectionItemType);
				foreach (object obj in items)
				{
					if (!isElementNullable && obj == null)
					{
						throw Error.InvalidOperation(Strings.Collection_NullCollectionItemsNotSupported);
					}
					ODataEnumValue odataEnumValue;
					if (flag)
					{
						if (obj is ODataCollectionValue)
						{
							throw Error.InvalidOperation(Strings.Collection_CollectionTypesInCollectionOfPrimitiveTypesNotAllowed);
						}
						object obj2 = this.primitiveValueMaterializationPolicy.MaterializePrimitiveDataValueCollectionElement(collectionItemType, wireTypeName, obj);
						addValueToBackingICollectionInstance(collectionInstance, obj2);
					}
					else if ((odataEnumValue = obj as ODataEnumValue) != null)
					{
						object obj3 = EnumValueMaterializationPolicy.MaterializeODataEnumValue(collectionItemType, odataEnumValue);
						addValueToBackingICollectionInstance(collectionInstance, obj3);
					}
					else
					{
						if (obj != null)
						{
							throw Error.InvalidOperation(Strings.Collection_PrimitiveTypesInCollectionOfComplexTypesNotAllowed);
						}
						addValueToBackingICollectionInstance(collectionInstance, null);
					}
				}
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002799C File Offset: 0x00025B9C
		private object CreateCollectionInstance(IEdmCollectionTypeReference edmCollectionTypeReference, Type clientCollectionType, Func<string> error)
		{
			if (ClientTypeUtil.IsDataServiceCollection(clientCollectionType))
			{
				throw Error.InvalidOperation(Strings.AtomMaterializer_DataServiceCollectionNotSupportedForNonEntities);
			}
			object obj;
			try
			{
				obj = this.CreateNewInstance(edmCollectionTypeReference, clientCollectionType);
			}
			catch (MissingMethodException ex)
			{
				throw Error.InvalidOperation(error(), ex);
			}
			return obj;
		}

		// Token: 0x0400060C RID: 1548
		private readonly IODataMaterializerContext materializerContext;

		// Token: 0x0400060D RID: 1549
		private PrimitiveValueMaterializationPolicy primitiveValueMaterializationPolicy;

		// Token: 0x0400060E RID: 1550
		private InstanceAnnotationMaterializationPolicy instanceAnnotationMaterializationPolicy;
	}
}
