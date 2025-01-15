using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001B4 RID: 436
	internal static class DeserializationHelpers
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x0003AE48 File Offset: 0x00039048
		internal static void ApplyProperty(ODataProperty property, IEdmStructuredTypeReference resourceType, object resource, ODataDeserializerProvider deserializerProvider, ODataDeserializerContext readContext)
		{
			IEdmProperty edmProperty = resourceType.FindProperty(property.Name);
			bool flag = false;
			string text = property.Name;
			if (edmProperty != null)
			{
				text = EdmLibHelpers.GetClrPropertyName(edmProperty, readContext.Model);
			}
			else
			{
				IEdmStructuredType edmStructuredType = resourceType.StructuredDefinition();
				flag = edmStructuredType != null && edmStructuredType.IsOpen;
			}
			if (!flag && edmProperty == null)
			{
				throw new ODataException(Error.Format(SRResources.CannotDeserializeUnknownProperty, new object[] { property.Name, resourceType.Definition }));
			}
			IEdmTypeReference edmTypeReference = ((edmProperty != null) ? edmProperty.Type : null);
			EdmTypeKind edmTypeKind;
			object obj = DeserializationHelpers.ConvertValue(property.Value, ref edmTypeReference, deserializerProvider, readContext, out edmTypeKind);
			if (flag)
			{
				DeserializationHelpers.SetDynamicProperty(resource, resourceType, edmTypeKind, text, obj, edmTypeReference, readContext.Model);
				return;
			}
			DeserializationHelpers.SetDeclaredProperty(resource, edmTypeKind, text, obj, edmProperty, readContext);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0003AF0C File Offset: 0x0003910C
		internal static void SetDynamicProperty(object resource, IEdmStructuredTypeReference resourceType, EdmTypeKind propertyKind, string propertyName, object propertyValue, IEdmTypeReference propertyType, IEdmModel model)
		{
			if (propertyKind == EdmTypeKind.Collection && propertyValue.GetType() != typeof(EdmComplexObjectCollection) && propertyValue.GetType() != typeof(EdmEnumObjectCollection))
			{
				DeserializationHelpers.SetDynamicCollectionProperty(resource, propertyName, propertyValue, propertyType.AsCollection(), resourceType.StructuredDefinition(), model);
				return;
			}
			DeserializationHelpers.SetDynamicProperty(resource, propertyName, propertyValue, resourceType.StructuredDefinition(), model);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0003AF77 File Offset: 0x00039177
		internal static void SetDeclaredProperty(object resource, EdmTypeKind propertyKind, string propertyName, object propertyValue, IEdmProperty edmProperty, ODataDeserializerContext readContext)
		{
			if (propertyKind == EdmTypeKind.Collection)
			{
				DeserializationHelpers.SetCollectionProperty(resource, edmProperty, propertyValue, propertyName);
				return;
			}
			if (!readContext.IsUntyped && propertyKind == EdmTypeKind.Primitive)
			{
				propertyValue = EdmPrimitiveHelpers.ConvertPrimitiveValue(propertyValue, DeserializationHelpers.GetPropertyType(resource, propertyName));
			}
			DeserializationHelpers.SetProperty(resource, propertyName, propertyValue);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003AFAC File Offset: 0x000391AC
		internal static void SetCollectionProperty(object resource, IEdmProperty edmProperty, object value, string propertyName)
		{
			DeserializationHelpers.SetCollectionProperty(resource, propertyName, edmProperty.Type.AsCollection(), value, false);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003AFC4 File Offset: 0x000391C4
		internal static void SetCollectionProperty(object resource, string propertyName, IEdmCollectionTypeReference edmPropertyType, object value, bool clearCollection)
		{
			if (value != null)
			{
				IEnumerable enumerable = value as IEnumerable;
				Type type = resource.GetType();
				Type propertyType = DeserializationHelpers.GetPropertyType(resource, propertyName);
				Type type2;
				if (!TypeHelper.IsCollection(propertyType, out type2))
				{
					throw new SerializationException(Error.Format(SRResources.PropertyIsNotCollection, new object[] { propertyType.FullName, propertyName, type.FullName }));
				}
				IEnumerable enumerable2;
				if (DeserializationHelpers.CanSetProperty(resource, propertyName) && CollectionDeserializationHelpers.TryCreateInstance(propertyType, edmPropertyType, type2, out enumerable2))
				{
					enumerable.AddToCollection(enumerable2, type2, type, propertyName, propertyType);
					if (propertyType.IsArray)
					{
						enumerable2 = CollectionDeserializationHelpers.ToArray(enumerable2, type2);
					}
					DeserializationHelpers.SetProperty(resource, propertyName, enumerable2);
					return;
				}
				enumerable2 = DeserializationHelpers.GetProperty(resource, propertyName) as IEnumerable;
				if (enumerable2 == null)
				{
					throw new SerializationException(Error.Format(SRResources.CannotAddToNullCollection, new object[] { propertyName, type.FullName }));
				}
				if (clearCollection)
				{
					enumerable2.Clear(propertyName, type);
				}
				enumerable.AddToCollection(enumerable2, type2, type, propertyName, propertyType);
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003B0B0 File Offset: 0x000392B0
		internal static void SetDynamicCollectionProperty(object resource, string propertyName, object value, IEdmCollectionTypeReference edmPropertyType, IEdmStructuredType structuredType, IEdmModel model)
		{
			IEnumerable enumerable = value as IEnumerable;
			Type type = resource.GetType();
			Type clrType = EdmLibHelpers.GetClrType(edmPropertyType.ElementType(), model);
			Type type2 = typeof(ICollection<>).MakeGenericType(new Type[] { clrType });
			IEnumerable enumerable2;
			if (CollectionDeserializationHelpers.TryCreateInstance(type2, edmPropertyType, clrType, out enumerable2))
			{
				enumerable.AddToCollection(enumerable2, clrType, type, propertyName, type2);
				DeserializationHelpers.SetDynamicProperty(resource, propertyName, enumerable2, structuredType, model);
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0003B118 File Offset: 0x00039318
		internal static void SetProperty(object resource, string propertyName, object value)
		{
			IDelta delta = resource as IDelta;
			if (delta == null)
			{
				resource.GetType().GetProperty(propertyName).SetValue(resource, value, null);
				return;
			}
			delta.TrySetPropertyValue(propertyName, value);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003B150 File Offset: 0x00039350
		internal static void SetDynamicProperty(object resource, string propertyName, object value, IEdmStructuredType structuredType, IEdmModel model)
		{
			IDelta delta = resource as IDelta;
			if (delta != null)
			{
				delta.TrySetPropertyValue(propertyName, value);
				return;
			}
			PropertyInfo dynamicPropertyDictionary = EdmLibHelpers.GetDynamicPropertyDictionary(structuredType, model);
			if (dynamicPropertyDictionary == null)
			{
				return;
			}
			object value2 = dynamicPropertyDictionary.GetValue(resource);
			IDictionary<string, object> dictionary;
			if (value2 == null)
			{
				if (!dynamicPropertyDictionary.CanWrite)
				{
					throw Error.InvalidOperation(SRResources.CannotSetDynamicPropertyDictionary, new object[]
					{
						propertyName,
						resource.GetType().FullName
					});
				}
				dictionary = new Dictionary<string, object>();
				dynamicPropertyDictionary.SetValue(resource, dictionary);
			}
			else
			{
				dictionary = (IDictionary<string, object>)value2;
			}
			if (dictionary.ContainsKey(propertyName))
			{
				throw Error.InvalidOperation(SRResources.DuplicateDynamicPropertyNameFound, new object[]
				{
					propertyName,
					structuredType.FullTypeName()
				});
			}
			dictionary.Add(propertyName, value);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003B200 File Offset: 0x00039400
		internal static object ConvertValue(object oDataValue, ref IEdmTypeReference propertyType, ODataDeserializerProvider deserializerProvider, ODataDeserializerContext readContext, out EdmTypeKind typeKind)
		{
			if (oDataValue == null)
			{
				typeKind = EdmTypeKind.None;
				return null;
			}
			ODataEnumValue odataEnumValue = oDataValue as ODataEnumValue;
			if (odataEnumValue != null)
			{
				typeKind = EdmTypeKind.Enum;
				return DeserializationHelpers.ConvertEnumValue(odataEnumValue, ref propertyType, deserializerProvider, readContext);
			}
			ODataCollectionValue odataCollectionValue = oDataValue as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				typeKind = EdmTypeKind.Collection;
				return DeserializationHelpers.ConvertCollectionValue(odataCollectionValue, ref propertyType, deserializerProvider, readContext);
			}
			ODataUntypedValue odataUntypedValue = oDataValue as ODataUntypedValue;
			if (odataUntypedValue != null)
			{
				if (odataUntypedValue.RawValue.StartsWith("[", StringComparison.Ordinal) || odataUntypedValue.RawValue.StartsWith("{", StringComparison.Ordinal))
				{
					throw new ODataException(Error.Format(SRResources.InvalidODataUntypedValue, new object[] { odataUntypedValue.RawValue }));
				}
				oDataValue = DeserializationHelpers.ConvertPrimitiveValue(odataUntypedValue.RawValue);
			}
			typeKind = EdmTypeKind.Primitive;
			return oDataValue;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003B2A8 File Offset: 0x000394A8
		internal static Type GetPropertyType(object resource, string propertyName)
		{
			IDelta delta = resource as IDelta;
			if (delta != null)
			{
				Type type;
				delta.TryGetPropertyType(propertyName, out type);
				return type;
			}
			PropertyInfo property = resource.GetType().GetProperty(propertyName);
			if (!(property == null))
			{
				return property.PropertyType;
			}
			return null;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0003B2EC File Offset: 0x000394EC
		private static bool CanSetProperty(object resource, string propertyName)
		{
			if (resource is IDelta)
			{
				return true;
			}
			PropertyInfo property = resource.GetType().GetProperty(propertyName);
			return property != null && property.GetSetMethod() != null;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0003B328 File Offset: 0x00039528
		private static object GetProperty(object resource, string propertyName)
		{
			IDelta delta = resource as IDelta;
			if (delta != null)
			{
				object obj;
				delta.TryGetPropertyValue(propertyName, out obj);
				return obj;
			}
			return resource.GetType().GetProperty(propertyName).GetValue(resource, null);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003B360 File Offset: 0x00039560
		private static object ConvertCollectionValue(ODataCollectionValue collection, ref IEdmTypeReference propertyType, ODataDeserializerProvider deserializerProvider, ODataDeserializerContext readContext)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference;
			if (propertyType == null)
			{
				string collectionElementTypeName = DeserializationHelpers.GetCollectionElementTypeName(collection.TypeName, false);
				edmCollectionTypeReference = new EdmCollectionTypeReference(new EdmCollectionType(readContext.Model.FindType(collectionElementTypeName).ToEdmTypeReference(false)));
				propertyType = edmCollectionTypeReference;
			}
			else
			{
				edmCollectionTypeReference = propertyType as IEdmCollectionTypeReference;
			}
			return deserializerProvider.GetEdmTypeDeserializer(edmCollectionTypeReference).ReadInline(collection, edmCollectionTypeReference, readContext);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003B3B8 File Offset: 0x000395B8
		private static object ConvertPrimitiveValue(string value)
		{
			if (string.CompareOrdinal(value, "null") == 0)
			{
				return null;
			}
			int num;
			if (int.TryParse(value, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
			{
				return num;
			}
			decimal num2;
			if (decimal.TryParse(value, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out num2))
			{
				return num2;
			}
			double num3;
			if (double.TryParse(value, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num3))
			{
				return num3;
			}
			if (!value.StartsWith("\"", StringComparison.Ordinal) || !value.EndsWith("\"", StringComparison.Ordinal))
			{
				throw new ODataException(Error.Format(SRResources.InvalidODataUntypedValue, new object[] { value }));
			}
			return value.Substring(1, value.Length - 2);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003B464 File Offset: 0x00039664
		private static object ConvertEnumValue(ODataEnumValue enumValue, ref IEdmTypeReference propertyType, ODataDeserializerProvider deserializerProvider, ODataDeserializerContext readContext)
		{
			IEdmEnumTypeReference edmEnumTypeReference;
			if (propertyType == null)
			{
				edmEnumTypeReference = new EdmEnumTypeReference(readContext.Model.FindType(enumValue.TypeName) as IEdmEnumType, true);
				propertyType = edmEnumTypeReference;
			}
			else
			{
				edmEnumTypeReference = propertyType.AsEnum();
			}
			return deserializerProvider.GetEdmTypeDeserializer(edmEnumTypeReference).ReadInline(enumValue, propertyType, readContext);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003B4B0 File Offset: 0x000396B0
		internal static string GetCollectionElementTypeName(string typeName, bool isNested)
		{
			int length = "Collection".Length;
			if (typeName == null || !typeName.StartsWith("Collection(", StringComparison.Ordinal) || typeName[typeName.Length - 1] != ')' || typeName.Length == length + 2)
			{
				return null;
			}
			if (isNested)
			{
				throw new ODataException(Error.Format(SRResources.NestedCollectionsNotSupported, new object[] { typeName }));
			}
			string text = typeName.Substring(length + 1, typeName.Length - (length + 2));
			DeserializationHelpers.GetCollectionElementTypeName(text, true);
			return text;
		}
	}
}
