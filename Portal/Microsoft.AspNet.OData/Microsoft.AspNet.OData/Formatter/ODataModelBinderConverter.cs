using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x0200018A RID: 394
	public static class ODataModelBinderConverter
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x00033024 File Offset: 0x00031224
		public static object Convert(object graph, IEdmTypeReference edmTypeReference, Type clrType, string parameterName, ODataDeserializerContext readContext, IServiceProvider requestContainer)
		{
			if (graph == null || graph is ODataNullValue)
			{
				return null;
			}
			ODataCollectionValue odataCollectionValue = graph as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return ODataModelBinderConverter.ConvertCollection(odataCollectionValue, edmTypeReference, clrType, parameterName, readContext, requestContainer);
			}
			ODataEnumValue odataEnumValue = graph as ODataEnumValue;
			if (odataEnumValue != null)
			{
				IEdmEnumTypeReference edmEnumTypeReference = edmTypeReference.AsEnum();
				return ((ODataEnumDeserializer)ServiceProviderServiceExtensions.GetRequiredService<ODataDeserializerProvider>(requestContainer).GetEdmTypeDeserializer(edmEnumTypeReference)).ReadInline(odataEnumValue, edmEnumTypeReference, readContext);
			}
			if (edmTypeReference.IsPrimitive())
			{
				ConstantNode constantNode = graph as ConstantNode;
				return EdmPrimitiveHelpers.ConvertPrimitiveValue((constantNode != null) ? constantNode.Value : graph, clrType);
			}
			return ODataModelBinderConverter.ConvertResourceOrResourceSet(graph, edmTypeReference, readContext);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000330B0 File Offset: 0x000312B0
		internal static object ConvertTo(string valueString, Type type)
		{
			if (valueString == null)
			{
				return null;
			}
			if (TypeHelper.IsNullable(type) && string.Equals(valueString, "null", StringComparison.Ordinal))
			{
				return null;
			}
			if (TypeHelper.IsEnum(type))
			{
				string[] array = valueString.Split(new char[] { '\'' }, StringSplitOptions.None);
				if (array.Length == 3 && string.IsNullOrEmpty(array[2]))
				{
					valueString = array[1];
				}
				Type underlyingTypeOrSelf = TypeHelper.GetUnderlyingTypeOrSelf(type);
				object[] array2 = new object[]
				{
					valueString,
					Enum.ToObject(underlyingTypeOrSelf, 0)
				};
				if (!(bool)ODataModelBinderConverter.EnumTryParseMethod.MakeGenericMethod(new Type[] { underlyingTypeOrSelf }).Invoke(null, array2))
				{
					throw Error.InvalidOperation(SRResources.ModelBinderUtil_ValueCannotBeEnum, new object[] { valueString, type.Name });
				}
				return array2[1];
			}
			else
			{
				if (type == typeof(Date) || type == typeof(Date?))
				{
					EdmCoreModel instance = EdmCoreModel.Instance;
					IEdmPrimitiveTypeReference edmPrimitiveTypeReferenceOrNull = EdmLibHelpers.GetEdmPrimitiveTypeReferenceOrNull(type);
					return ODataUriUtils.ConvertFromUriLiteral(valueString, ODataVersion.V4, instance, edmPrimitiveTypeReferenceOrNull);
				}
				object obj;
				try
				{
					obj = ODataUriUtils.ConvertFromUriLiteral(valueString, ODataVersion.V4);
				}
				catch
				{
					if (type == typeof(string))
					{
						return valueString;
					}
					throw;
				}
				bool flag;
				EdmLibHelpers.IsNonstandardEdmPrimitive(type, out flag);
				if (flag)
				{
					return EdmPrimitiveHelpers.ConvertPrimitiveValue(obj, type);
				}
				type = Nullable.GetUnderlyingType(type) ?? type;
				return global::System.Convert.ChangeType(obj, type, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00033214 File Offset: 0x00031414
		private static object ConvertCollection(ODataCollectionValue collectionValue, IEdmTypeReference edmTypeReference, Type clrType, string parameterName, ODataDeserializerContext readContext, IServiceProvider requestContainer)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = edmTypeReference as IEdmCollectionTypeReference;
			object obj = ((ODataCollectionDeserializer)ServiceProviderServiceExtensions.GetRequiredService<ODataDeserializerProvider>(requestContainer).GetEdmTypeDeserializer(edmCollectionTypeReference)).ReadInline(collectionValue, edmCollectionTypeReference, readContext);
			if (obj == null)
			{
				return null;
			}
			IEnumerable enumerable = obj as IEnumerable;
			Type type;
			if (!TypeHelper.IsCollection(clrType, out type))
			{
				throw new ODataException(string.Format(CultureInfo.InvariantCulture, SRResources.ParameterTypeIsNotCollection, new object[] { parameterName, clrType }));
			}
			IEnumerable enumerable2;
			if (CollectionDeserializationHelpers.TryCreateInstance(clrType, edmCollectionTypeReference, type, out enumerable2))
			{
				enumerable.AddToCollection(enumerable2, type, parameterName, clrType);
				if (clrType.IsArray)
				{
					enumerable2 = CollectionDeserializationHelpers.ToArray(enumerable2, type);
				}
				return enumerable2;
			}
			return null;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000332AC File Offset: 0x000314AC
		private static object ConvertResourceOrResourceSet(object oDataValue, IEdmTypeReference edmTypeReference, ODataDeserializerContext readContext)
		{
			string text = oDataValue as string;
			if (edmTypeReference.IsNullable && string.Equals(text, "null", StringComparison.Ordinal))
			{
				return null;
			}
			IWebApiRequestMessage internalRequest = readContext.InternalRequest;
			ODataMessageReaderSettings readerSettings = internalRequest.ReaderSettings;
			object obj;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(text)))
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (ODataMessageReader odataMessageReader = new ODataMessageReader(new ODataMessageWrapper(memoryStream, null, internalRequest.ODataContentIdMapping), readerSettings, readContext.Model))
				{
					if (edmTypeReference.IsCollection())
					{
						obj = ODataModelBinderConverter.ConvertResourceSet(odataMessageReader, edmTypeReference, readContext);
					}
					else
					{
						obj = ODataModelBinderConverter.ConvertResource(odataMessageReader, edmTypeReference, readContext);
					}
				}
			}
			return obj;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00033374 File Offset: 0x00031574
		private static object ConvertResourceSet(ODataMessageReader oDataMessageReader, IEdmTypeReference edmTypeReference, ODataDeserializerContext readContext)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = edmTypeReference.AsCollection();
			EdmEntitySet edmEntitySet = null;
			if (edmCollectionTypeReference.ElementType().IsEntity())
			{
				edmEntitySet = new EdmEntitySet(readContext.Model.EntityContainer, "temp", edmCollectionTypeReference.ElementType().AsEntity().EntityDefinition());
			}
			ODataResourceSetWrapper odataResourceSetWrapper = oDataMessageReader.CreateODataUriParameterResourceSetReader(edmEntitySet, edmCollectionTypeReference.ElementType().AsStructured().StructuredDefinition()).ReadResourceOrResourceSet() as ODataResourceSetWrapper;
			IEnumerable enumerable = ((ODataResourceSetDeserializer)readContext.InternalRequest.DeserializerProvider.GetEdmTypeDeserializer(edmCollectionTypeReference)).ReadInline(odataResourceSetWrapper, edmCollectionTypeReference, readContext) as IEnumerable;
			if (enumerable == null)
			{
				return null;
			}
			IEnumerable enumerable2 = enumerable;
			if (edmCollectionTypeReference.ElementType().IsEntity())
			{
				enumerable2 = ODataModelBinderConverter.CovertResourceSetIds(enumerable, odataResourceSetWrapper, edmCollectionTypeReference, readContext);
			}
			if (readContext.IsUntyped)
			{
				return enumerable2.ConvertToEdmObject(edmCollectionTypeReference);
			}
			Type clrType = EdmLibHelpers.GetClrType(edmCollectionTypeReference.ElementType(), readContext.Model);
			return ODataModelBinderConverter.CastMethodInfo.MakeGenericMethod(new Type[] { clrType }).Invoke(null, new object[] { enumerable2 }) as IEnumerable;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00033474 File Offset: 0x00031674
		private static object ConvertResource(ODataMessageReader oDataMessageReader, IEdmTypeReference edmTypeReference, ODataDeserializerContext readContext)
		{
			EdmEntitySet edmEntitySet = null;
			if (edmTypeReference.IsEntity())
			{
				IEdmEntityTypeReference edmEntityTypeReference = edmTypeReference.AsEntity();
				edmEntitySet = new EdmEntitySet(readContext.Model.EntityContainer, "temp", edmEntityTypeReference.EntityDefinition());
			}
			ODataResourceWrapper odataResourceWrapper = oDataMessageReader.CreateODataUriParameterResourceReader(edmEntitySet, edmTypeReference.ToStructuredType()).ReadResourceOrResourceSet() as ODataResourceWrapper;
			object obj = ((ODataResourceDeserializer)readContext.InternalRequest.DeserializerProvider.GetEdmTypeDeserializer(edmTypeReference)).ReadInline(odataResourceWrapper, edmTypeReference, readContext);
			if (edmTypeReference.IsEntity())
			{
				IEdmEntityTypeReference edmEntityTypeReference2 = edmTypeReference.AsEntity();
				return ODataModelBinderConverter.CovertResourceId(obj, odataResourceWrapper.Resource, edmEntityTypeReference2, readContext);
			}
			return obj;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00033506 File Offset: 0x00031706
		private static IEnumerable CovertResourceSetIds(IEnumerable sources, ODataResourceSetWrapper resourceSet, IEdmCollectionTypeReference collectionType, ODataDeserializerContext readContext)
		{
			IEdmEntityTypeReference entityTypeReference = collectionType.ElementType().AsEntity();
			int i = 0;
			foreach (object obj in sources)
			{
				object obj2 = ODataModelBinderConverter.CovertResourceId(obj, resourceSet.Resources[i].Resource, entityTypeReference, readContext);
				int num = i;
				i = num + 1;
				yield return obj2;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0003352C File Offset: 0x0003172C
		private static object CovertResourceId(object source, ODataResource resource, IEdmEntityTypeReference entityTypeReference, ODataDeserializerContext readContext)
		{
			if (resource.Id == null || resource.Properties.Any<ODataProperty>())
			{
				return source;
			}
			IWebApiRequestMessage internalRequest = readContext.InternalRequest;
			IWebApiUrlHelper internalUrlHelper = readContext.InternalUrlHelper;
			DefaultODataPathHandler defaultODataPathHandler = new DefaultODataPathHandler();
			string text = internalUrlHelper.CreateODataLink(internalRequest.Context.RouteName, internalRequest.PathHandler, new List<ODataPathSegment>());
			IEnumerable<KeyValuePair<string, object>> keys = ODataModelBinderConverter.GetKeys(defaultODataPathHandler, text, resource.Id, internalRequest.RequestContainer);
			IList<IEdmStructuralProperty> list = entityTypeReference.Key().ToList<IEdmStructuralProperty>();
			if (list.Count == 1 && keys.Count<KeyValuePair<string, object>>() == 1)
			{
				object value = keys.First<KeyValuePair<string, object>>().Value;
				DeserializationHelpers.SetDeclaredProperty(source, EdmTypeKind.Primitive, list[0].Name, value, list[0], readContext);
				return source;
			}
			IDictionary<string, object> dictionary = keys.ToDictionary((KeyValuePair<string, object> e) => e.Key, (KeyValuePair<string, object> e) => e.Value);
			foreach (IEdmStructuralProperty edmStructuralProperty in list)
			{
				object obj;
				if (dictionary.TryGetValue(edmStructuralProperty.Name, out obj))
				{
					DeserializationHelpers.SetDeclaredProperty(source, EdmTypeKind.Primitive, edmStructuralProperty.Name, obj, edmStructuralProperty, readContext);
				}
			}
			return source;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00033694 File Offset: 0x00031894
		private static IEnumerable<KeyValuePair<string, object>> GetKeys(DefaultODataPathHandler pathHandler, string serviceRoot, Uri uri, IServiceProvider requestContainer)
		{
			KeySegment keySegment = pathHandler.Parse(serviceRoot, uri.ToString(), requestContainer).Segments.OfType<KeySegment>().Last<KeySegment>();
			if (keySegment == null)
			{
				throw Error.InvalidOperation(SRResources.EntityReferenceMustHasKeySegment, new object[] { uri });
			}
			return keySegment.Keys;
		}

		// Token: 0x040003B3 RID: 947
		private static readonly MethodInfo EnumTryParseMethod = typeof(Enum).GetMethods().Single((MethodInfo m) => m.Name == "TryParse" && m.GetParameters().Length == 2);

		// Token: 0x040003B4 RID: 948
		private static readonly MethodInfo CastMethodInfo = typeof(Enumerable).GetMethod("Cast");
	}
}
