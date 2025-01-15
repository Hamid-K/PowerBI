using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x0200006B RID: 107
	internal class ODataPropertyConverter
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0000DDE4 File Offset: 0x0000BFE4
		internal ODataPropertyConverter(RequestInfo requestInfo)
		{
			this.requestInfo = requestInfo;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		internal IEnumerable<ODataProperty> PopulateProperties(object resource, string serverTypeName, IEnumerable<ClientPropertyAnnotation> properties)
		{
			List<ODataProperty> list = new List<ODataProperty>();
			IEnumerable<ClientPropertyAnnotation> enumerable = properties.Where((ClientPropertyAnnotation p) => !p.IsComplex && !p.IsComplexCollection);
			foreach (ClientPropertyAnnotation clientPropertyAnnotation in enumerable)
			{
				object value = clientPropertyAnnotation.GetValue(resource);
				ODataValue odataValue;
				if (this.TryConvertPropertyValue(clientPropertyAnnotation, value, serverTypeName, null, out odataValue))
				{
					list.Add(new ODataProperty
					{
						Name = clientPropertyAnnotation.PropertyName,
						Value = odataValue
					});
					this.AddTypeAnnotationNotDeclaredOnServer(serverTypeName, clientPropertyAnnotation, odataValue);
				}
			}
			return list;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		internal ODataResourceWrapper CreateODataResourceWrapperForComplex(Type complexType, object instance, string propertyName, HashSet<object> visitedComplexTypeObjects)
		{
			ClientEdmModel model = this.requestInfo.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(complexType);
			if (instance == null)
			{
				return new ODataResourceWrapper
				{
					Resource = null
				};
			}
			if (visitedComplexTypeObjects == null)
			{
				visitedComplexTypeObjects = new HashSet<object>(ReferenceEqualityComparer<object>.Instance);
			}
			else if (visitedComplexTypeObjects.Contains(instance))
			{
				if (propertyName != null)
				{
					throw Error.InvalidOperation(Strings.Serializer_LoopsNotAllowedInComplexTypes(propertyName));
				}
				throw Error.InvalidOperation(Strings.Serializer_LoopsNotAllowedInNonPropertyComplexTypes(clientTypeAnnotation.ElementTypeName));
			}
			visitedComplexTypeObjects.Add(instance);
			ODataResource odataResource = new ODataResource
			{
				TypeName = clientTypeAnnotation.ElementTypeName
			};
			string serverTypeName = this.requestInfo.GetServerTypeName(clientTypeAnnotation);
			odataResource.TypeAnnotation = new ODataTypeAnnotation(serverTypeName);
			odataResource.Properties = this.PopulateProperties(instance, serverTypeName, clientTypeAnnotation.PropertiesToSerialize(), visitedComplexTypeObjects);
			ODataResourceWrapper odataResourceWrapper = new ODataResourceWrapper
			{
				Resource = odataResource,
				Instance = instance
			};
			odataResourceWrapper.NestedResourceInfoWrappers = this.PopulateNestedComplexProperties(instance, serverTypeName, clientTypeAnnotation.PropertiesToSerialize(), visitedComplexTypeObjects);
			visitedComplexTypeObjects.Remove(instance);
			return odataResourceWrapper;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000DF94 File Offset: 0x0000C194
		internal IEnumerable<ODataNestedResourceInfoWrapper> PopulateNestedComplexProperties(object resource, string serverTypeName, IEnumerable<ClientPropertyAnnotation> properties, HashSet<object> visitedComplexTypeObjects)
		{
			List<ODataNestedResourceInfoWrapper> list = new List<ODataNestedResourceInfoWrapper>();
			IEnumerable<ClientPropertyAnnotation> enumerable = properties.Where((ClientPropertyAnnotation p) => p.IsComplex || p.IsComplexCollection);
			foreach (ClientPropertyAnnotation clientPropertyAnnotation in enumerable)
			{
				object value = clientPropertyAnnotation.GetValue(resource);
				ODataItemWrapper odataItemWrapper;
				if (this.TryConvertPropertyToResourceOrResourceSet(clientPropertyAnnotation, value, serverTypeName, visitedComplexTypeObjects, out odataItemWrapper))
				{
					list.Add(new ODataNestedResourceInfoWrapper
					{
						NestedResourceInfo = new ODataNestedResourceInfo
						{
							Name = clientPropertyAnnotation.PropertyName,
							IsCollection = new bool?(clientPropertyAnnotation.IsComplexCollection)
						},
						NestedResourceOrResourceSet = odataItemWrapper
					});
				}
			}
			return list;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E058 File Offset: 0x0000C258
		internal ODataResource CreateODataEntry(Type entityType, object value, params ClientPropertyAnnotation[] properties)
		{
			if (value == null)
			{
				return null;
			}
			ClientEdmModel model = this.requestInfo.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(value.GetType());
			ODataResource odataResource = new ODataResource
			{
				TypeName = clientTypeAnnotation.ElementTypeName
			};
			string serverTypeName = this.requestInfo.GetServerTypeName(clientTypeAnnotation);
			odataResource.TypeAnnotation = new ODataTypeAnnotation(serverTypeName);
			odataResource.Properties = this.PopulateProperties(value, serverTypeName, properties.Any<ClientPropertyAnnotation>() ? properties : clientTypeAnnotation.PropertiesToSerialize(), null);
			return odataResource;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		internal ODataResourceWrapper CreateODataResourceWrapper(Type entityType, object value, params ClientPropertyAnnotation[] properties)
		{
			if (value == null)
			{
				return null;
			}
			ClientEdmModel model = this.requestInfo.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(value.GetType());
			ODataResource odataResource = new ODataResource
			{
				TypeName = clientTypeAnnotation.ElementTypeName
			};
			string serverTypeName = this.requestInfo.GetServerTypeName(clientTypeAnnotation);
			odataResource.TypeAnnotation = new ODataTypeAnnotation(serverTypeName);
			odataResource.Properties = this.PopulateProperties(value, serverTypeName, properties.Any<ClientPropertyAnnotation>() ? properties : clientTypeAnnotation.PropertiesToSerialize(), null);
			ODataResourceWrapper odataResourceWrapper = new ODataResourceWrapper
			{
				Resource = odataResource
			};
			odataResourceWrapper.NestedResourceInfoWrappers = this.PopulateNestedComplexProperties(value, serverTypeName, properties.Any<ClientPropertyAnnotation>() ? properties : clientTypeAnnotation.PropertiesToSerialize(), null);
			return odataResourceWrapper;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E184 File Offset: 0x0000C384
		internal IEnumerable<ODataResource> CreateODataEntries(Type entityType, object value)
		{
			IEnumerable enumerable = value as IEnumerable;
			List<ODataResource> list = new List<ODataResource>();
			if (enumerable != null)
			{
				list.AddRange(from object o in enumerable
					select this.CreateODataEntry(entityType, o, new ClientPropertyAnnotation[0]));
			}
			return list;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		internal ODataEnumValue CreateODataEnumValue(Type enumClrType, object value, bool isCollectionItem)
		{
			ClientEdmModel model = this.requestInfo.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(enumClrType);
			if (value == null)
			{
				return null;
			}
			return new ODataEnumValue(ClientTypeUtil.GetEnumValuesString(value.ToString(), enumClrType), clientTypeAnnotation.ElementTypeName);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E214 File Offset: 0x0000C414
		internal ODataCollectionValue CreateODataCollection(Type collectionItemType, string propertyName, object value, HashSet<object> visitedComplexTypeObjects, bool isDynamicProperty, bool setTypeAnnotation = true)
		{
			WebUtil.ValidateCollection(collectionItemType, value, propertyName, isDynamicProperty);
			PrimitiveType primitiveType;
			bool flag = PrimitiveType.TryGetPrimitiveType(collectionItemType, out primitiveType);
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			IEnumerable enumerable = (IEnumerable)value;
			string text;
			string text2;
			if (flag)
			{
				text = ClientConvert.GetEdmType(Nullable.GetUnderlyingType(collectionItemType) ?? collectionItemType);
				if (enumerable != null)
				{
					odataCollectionValue.Items = Util.GetEnumerable<object>(enumerable, delegate(object val)
					{
						if (val == null)
						{
							return null;
						}
						WebUtil.ValidatePrimitiveCollectionItem(val, propertyName, collectionItemType);
						return ODataPropertyConverter.ConvertPrimitiveValueToRecognizedODataType(val, collectionItemType);
					});
				}
				text2 = text;
			}
			else
			{
				Type collectionItemTypeTmp = Nullable.GetUnderlyingType(collectionItemType) ?? collectionItemType;
				text = this.requestInfo.ResolveNameFromType(collectionItemType);
				if (enumerable != null)
				{
					odataCollectionValue.Items = Util.GetEnumerable<ODataValue>(enumerable, delegate(object val)
					{
						if (val == null)
						{
							return new ODataEnumValue(null, collectionItemType.FullName);
						}
						return new ODataEnumValue(ClientTypeUtil.GetEnumValuesString(val.ToString(), collectionItemTypeTmp), collectionItemType.FullName);
					});
				}
				text2 = collectionItemType.FullName;
			}
			odataCollectionValue.TypeName = ODataPropertyConverter.GetCollectionName(text2);
			if (setTypeAnnotation)
			{
				string collectionName = ODataPropertyConverter.GetCollectionName(text);
				odataCollectionValue.TypeAnnotation = new ODataTypeAnnotation(collectionName);
			}
			return odataCollectionValue;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E354 File Offset: 0x0000C554
		internal ODataResourceSetWrapper CreateODataResourceSetWrapperForComplexCollection(Type collectionItemType, string propertyName, object value, HashSet<object> visitedComplexTypeObjects, bool isDynamicProperty, bool setTypeAnnotation = true)
		{
			WebUtil.ValidateCollection(collectionItemType, value, propertyName, isDynamicProperty);
			ODataResourceSet odataResourceSet = new ODataResourceSet();
			ODataResourceSetWrapper odataResourceSetWrapper = new ODataResourceSetWrapper
			{
				ResourceSet = odataResourceSet
			};
			string text = this.requestInfo.ResolveNameFromType(collectionItemType);
			IEnumerable enumerable = (IEnumerable)value;
			if (enumerable != null)
			{
				odataResourceSetWrapper.Resources = Util.GetEnumerable<ODataResourceWrapper>(enumerable, delegate(object val)
				{
					if (val == null)
					{
						return null;
					}
					WebUtil.ValidateComplexCollectionItem(val, propertyName, collectionItemType);
					return this.CreateODataResourceWrapperForComplex(val.GetType(), val, propertyName, visitedComplexTypeObjects);
				});
			}
			if (setTypeAnnotation)
			{
				string collectionName = ODataPropertyConverter.GetCollectionName(text);
				odataResourceSet.TypeAnnotation = new ODataTypeAnnotation(collectionName);
			}
			return odataResourceSetWrapper;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		internal static object ConvertPrimitiveValueToRecognizedODataType(object propertyValue, Type propertyType)
		{
			if (propertyValue == null)
			{
				return null;
			}
			PrimitiveType primitiveType;
			PrimitiveType.TryGetPrimitiveType(propertyType, out primitiveType);
			if (propertyType == typeof(char) || propertyType == typeof(char[]) || propertyType == typeof(Type) || propertyType == typeof(Uri) || propertyType == typeof(XDocument) || propertyType == typeof(XElement))
			{
				return primitiveType.TypeConverter.ToString(propertyValue);
			}
			if (propertyType == typeof(DateTime))
			{
				return PlatformHelper.ConvertDateTimeToDateTimeOffset((DateTime)propertyValue);
			}
			if (propertyType.FullName == "System.Data.Linq.Binary")
			{
				return ((BinaryTypeConverter)primitiveType.TypeConverter).ToArray(propertyValue);
			}
			if (primitiveType.EdmTypeName == null)
			{
				throw new NotSupportedException(Strings.ALinq_CantCastToUnsupportedPrimitive(propertyType.Name));
			}
			return propertyValue;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		private static string GetCollectionName(string itemTypeName)
		{
			if (itemTypeName != null)
			{
				return EdmLibraryExtensions.GetCollectionTypeName(itemTypeName);
			}
			return null;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E4FE File Offset: 0x0000C6FE
		private static ODataValue CreateODataPrimitivePropertyValue(ClientPropertyAnnotation property, object propertyValue)
		{
			if (propertyValue == null)
			{
				return new ODataNullValue();
			}
			propertyValue = ODataPropertyConverter.ConvertPrimitiveValueToRecognizedODataType(propertyValue, property.PropertyType);
			return new ODataPrimitiveValue(propertyValue);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000E520 File Offset: 0x0000C720
		private IEnumerable<ODataProperty> PopulateProperties(object resource, string serverTypeName, IEnumerable<ClientPropertyAnnotation> properties, HashSet<object> visitedComplexTypeObjects)
		{
			List<ODataProperty> list = new List<ODataProperty>();
			IEnumerable<ClientPropertyAnnotation> enumerable = properties.Where((ClientPropertyAnnotation p) => !p.IsComplex && !p.IsComplexCollection);
			foreach (ClientPropertyAnnotation clientPropertyAnnotation in enumerable)
			{
				object value = clientPropertyAnnotation.GetValue(resource);
				ODataValue odataValue;
				if (this.TryConvertPropertyValue(clientPropertyAnnotation, value, serverTypeName, visitedComplexTypeObjects, out odataValue))
				{
					list.Add(new ODataProperty
					{
						Name = clientPropertyAnnotation.PropertyName,
						Value = odataValue
					});
				}
			}
			return list;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		private bool TryConvertPropertyValue(ClientPropertyAnnotation property, object propertyValue, string serverTypeName, HashSet<object> visitedComplexTypeObjects, out ODataValue odataValue)
		{
			if (property.IsKnownType)
			{
				odataValue = ODataPropertyConverter.CreateODataPrimitivePropertyValue(property, propertyValue);
				return true;
			}
			if (property.IsEnumType)
			{
				string text;
				if (propertyValue == null)
				{
					text = null;
				}
				else
				{
					text = ClientTypeUtil.GetEnumValuesString(propertyValue.ToString(), property.PropertyType);
				}
				string text2 = this.requestInfo.ResolveNameFromType(property.PropertyType);
				odataValue = new ODataEnumValue(text, text2);
				return true;
			}
			if (property.IsPrimitiveOrEnumOrComplexCollection)
			{
				odataValue = this.CreateODataCollectionPropertyValue(property, propertyValue, serverTypeName, visitedComplexTypeObjects);
				return true;
			}
			odataValue = null;
			return false;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000E645 File Offset: 0x0000C845
		private bool TryConvertPropertyToResourceOrResourceSet(ClientPropertyAnnotation property, object propertyValue, string serverTypeName, HashSet<object> visitedComplexTypeObjects, out ODataItemWrapper odataItem)
		{
			if (property.IsComplexCollection)
			{
				odataItem = this.CreateODataComplexCollectionPropertyResourceSet(property, propertyValue, serverTypeName, visitedComplexTypeObjects);
				return true;
			}
			if (property.IsComplex)
			{
				odataItem = this.CreateODataComplexPropertyResource(property, propertyValue, visitedComplexTypeObjects);
				return true;
			}
			odataItem = null;
			return false;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000E67C File Offset: 0x0000C87C
		private ODataResourceWrapper CreateODataComplexPropertyResource(ClientPropertyAnnotation property, object propertyValue, HashSet<object> visitedComplexTypeObjects)
		{
			Type type = (property.IsComplexCollection ? property.PrimitiveOrComplexCollectionItemType : property.PropertyType);
			if (propertyValue != null && type != propertyValue.GetType())
			{
				type = propertyValue.GetType();
			}
			return this.CreateODataResourceWrapperForComplex(type, propertyValue, property.PropertyName, visitedComplexTypeObjects);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		private ODataCollectionValue CreateODataCollectionPropertyValue(ClientPropertyAnnotation property, object propertyValue, string serverTypeName, HashSet<object> visitedComplexTypeObjects)
		{
			bool flag = this.requestInfo.TypeResolver.ShouldWriteClientTypeForOpenServerProperty(property.EdmProperty, serverTypeName);
			return this.CreateODataCollection(property.PrimitiveOrComplexCollectionItemType, property.PropertyName, propertyValue, visitedComplexTypeObjects, flag, true);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000E704 File Offset: 0x0000C904
		private ODataResourceSetWrapper CreateODataComplexCollectionPropertyResourceSet(ClientPropertyAnnotation property, object propertyValue, string serverTypeName, HashSet<object> visitedComplexTypeObjects)
		{
			bool flag = this.requestInfo.TypeResolver.ShouldWriteClientTypeForOpenServerProperty(property.EdmProperty, serverTypeName);
			return this.CreateODataResourceSetWrapperForComplexCollection(property.PrimitiveOrComplexCollectionItemType, property.PropertyName, propertyValue, visitedComplexTypeObjects, flag, true);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E740 File Offset: 0x0000C940
		private void AddTypeAnnotationNotDeclaredOnServer(string serverTypeName, ClientPropertyAnnotation property, ODataValue odataValue)
		{
			ODataPrimitiveValue odataPrimitiveValue = odataValue as ODataPrimitiveValue;
			if (odataPrimitiveValue == null)
			{
				return;
			}
			if (this.requestInfo.TypeResolver.ShouldWriteClientTypeForOpenServerProperty(property.EdmProperty, serverTypeName) && !JsonSharedUtils.ValueTypeMatchesJsonType(odataPrimitiveValue, property.EdmProperty.Type.AsPrimitive()))
			{
				odataPrimitiveValue.TypeAnnotation = new ODataTypeAnnotation(property.EdmProperty.Type.FullName());
			}
		}

		// Token: 0x0400012C RID: 300
		private readonly RequestInfo requestInfo;
	}
}
