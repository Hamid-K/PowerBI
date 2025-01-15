using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000B9 RID: 185
	internal class ODataJsonLightPropertyAndValueDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x00016D83 File Offset: 0x00014F83
		internal ODataJsonLightPropertyAndValueDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00016D8C File Offset: 0x00014F8C
		internal ODataProperty ReadTopLevelProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.ReadPayloadStart(ODataPayloadKind.Property, duplicatePropertyNamesChecker, false, false);
			ODataProperty odataProperty = this.ReadTopLevelPropertyImplementation(expectedPropertyTypeReference, duplicatePropertyNamesChecker);
			base.ReadPayloadEnd(false);
			return odataProperty;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00016DBC File Offset: 0x00014FBC
		internal object ReadNonEntityValue(string payloadTypeName, IEdmTypeReference expectedValueTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool isTopLevelPropertyValue, bool insideComplexValue, string propertyName, bool? isDynamicProperty = null)
		{
			return this.ReadNonEntityValueImplementation(payloadTypeName, expectedValueTypeReference, duplicatePropertyNamesChecker, collectionValidator, validateNullValue, isTopLevelPropertyValue, insideComplexValue, propertyName, isDynamicProperty);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00016DE0 File Offset: 0x00014FE0
		internal object ReadCustomInstanceAnnotationValue(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, string name)
		{
			Dictionary<string, object> odataPropertyAnnotations = duplicatePropertyNamesChecker.GetODataPropertyAnnotations(name);
			string text = null;
			object obj;
			if (odataPropertyAnnotations != null && odataPropertyAnnotations.TryGetValue("odata.type", ref obj))
			{
				text = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName((string)obj));
			}
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfValueTerm(name, base.Model);
			return this.ReadNonEntityValueImplementation(text, edmTypeReference, null, null, false, false, false, name, default(bool?));
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00016E44 File Offset: 0x00015044
		protected static string ValidateDataPropertyTypeNameAnnotation(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, string propertyName)
		{
			Dictionary<string, object> odataPropertyAnnotations = duplicatePropertyNamesChecker.GetODataPropertyAnnotations(propertyName);
			string text = null;
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					if (string.CompareOrdinal(keyValuePair.Key, "odata.type") != 0)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation(propertyName, keyValuePair.Key));
					}
					text = (string)keyValuePair.Value;
				}
			}
			return text;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00016ECC File Offset: 0x000150CC
		protected bool TryReadODataTypeAnnotationValue(string annotationName, out string value)
		{
			if (string.CompareOrdinal(annotationName, "odata.type") == 0)
			{
				value = this.ReadODataTypeAnnotationValue();
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00016EEC File Offset: 0x000150EC
		protected string ReadODataTypeAnnotationValue()
		{
			string text = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName(base.JsonReader.ReadStringValue()));
			if (text == null)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(text));
			}
			return text;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00016F20 File Offset: 0x00015120
		protected object ReadTypePropertyAnnotationValue(string propertyAnnotationName)
		{
			string text;
			if (this.TryReadODataTypeAnnotationValue(propertyAnnotationName, out text))
			{
				return text;
			}
			throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyAnnotationName));
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00016F48 File Offset: 0x00015148
		private bool TryReadODataTypeAnnotation(out string payloadTypeName)
		{
			payloadTypeName = null;
			bool flag = false;
			string propertyName = base.JsonReader.GetPropertyName();
			if (string.CompareOrdinal(propertyName, '@' + "odata.type") == 0 || base.CompareSimplifiedODataAnnotation("@type", propertyName))
			{
				base.JsonReader.ReadNext();
				payloadTypeName = this.ReadODataTypeAnnotationValue();
				flag = true;
			}
			return flag;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00017130 File Offset: 0x00015330
		private ODataProperty ReadTopLevelPropertyImplementation(IEdmTypeReference expectedPropertyTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			expectedPropertyTypeReference = this.UpdateExpectedTypeBasedOnContextUri(expectedPropertyTypeReference);
			object propertyValue = ODataJsonLightPropertyAndValueDeserializer.missingPropertyValue;
			Collection<ODataInstanceAnnotation> customInstanceAnnotations = new Collection<ODataInstanceAnnotation>();
			if (this.IsTopLevelNullValue())
			{
				ReaderValidationUtils.ValidateNullValue(base.Model, expectedPropertyTypeReference, base.MessageReaderSettings, true, null, default(bool?));
				this.ValidateNoPropertyInNullPayload(duplicatePropertyNamesChecker);
				propertyValue = null;
			}
			else
			{
				string payloadTypeName = null;
				if (this.ReadingComplexProperty(duplicatePropertyNamesChecker, expectedPropertyTypeReference, out payloadTypeName))
				{
					propertyValue = this.ReadNonEntityValue(payloadTypeName, expectedPropertyTypeReference, duplicatePropertyNamesChecker, null, true, true, true, null, default(bool?));
				}
				else
				{
					bool isReordering = base.JsonReader is ReorderingJsonReader;
					Func<string, object> func = delegate(string annotationName)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation(annotationName));
					};
					while (base.JsonReader.NodeType == JsonNodeType.Property)
					{
						base.ProcessProperty(duplicatePropertyNamesChecker, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
						{
							switch (propertyParsingResult)
							{
							case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
								return;
							case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
								if (string.CompareOrdinal("value", propertyName) == 0)
								{
									propertyValue = this.ReadNonEntityValue(payloadTypeName, expectedPropertyTypeReference, null, null, true, true, false, propertyName, default(bool?));
									return;
								}
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName(propertyName, "value"));
							case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty(propertyName));
							case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
								if (string.CompareOrdinal("odata.type", propertyName) != 0)
								{
									throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
								}
								if (isReordering)
								{
									this.JsonReader.SkipValue();
									return;
								}
								if (!object.ReferenceEquals(ODataJsonLightPropertyAndValueDeserializer.missingPropertyValue, propertyValue))
								{
									throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty("odata.type", "value"));
								}
								payloadTypeName = this.ReadODataTypeAnnotationValue();
								return;
							case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
							{
								ODataAnnotationNames.ValidateIsCustomAnnotationName(propertyName);
								object obj = this.ReadCustomInstanceAnnotationValue(duplicatePropertyNamesChecker, propertyName);
								customInstanceAnnotations.Add(new ODataInstanceAnnotation(propertyName, obj.ToODataValue()));
								return;
							}
							case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
							default:
								return;
							}
						});
					}
					if (object.ReferenceEquals(ODataJsonLightPropertyAndValueDeserializer.missingPropertyValue, propertyValue))
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload);
					}
				}
			}
			ODataProperty odataProperty = new ODataProperty
			{
				Name = null,
				Value = propertyValue,
				InstanceAnnotations = customInstanceAnnotations
			};
			base.JsonReader.Read();
			return odataProperty;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00017300 File Offset: 0x00015500
		private IEdmTypeReference UpdateExpectedTypeBasedOnContextUri(IEdmTypeReference expectedPropertyTypeReference)
		{
			if (base.ContextUriParseResult == null || base.ContextUriParseResult.EdmType == null)
			{
				return expectedPropertyTypeReference;
			}
			IEdmType edmType = base.ContextUriParseResult.EdmType;
			if (expectedPropertyTypeReference != null && !expectedPropertyTypeReference.Definition.IsAssignableFrom(edmType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_TypeInContextUriDoesNotMatchExpectedType(UriUtils.UriToString(base.ContextUriParseResult.ContextUri), edmType.FullTypeName(), expectedPropertyTypeReference.FullName()));
			}
			bool flag = true;
			if (expectedPropertyTypeReference != null)
			{
				flag = expectedPropertyTypeReference.IsNullable;
			}
			return edmType.ToTypeReference(flag);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001737C File Offset: 0x0001557C
		private ODataCollectionValue ReadCollectionValue(IEdmCollectionTypeReference collectionValueTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation)
		{
			this.IncreaseRecursionDepth();
			base.JsonReader.ReadStartArray();
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			odataCollectionValue.TypeName = ((collectionValueTypeReference != null) ? collectionValueTypeReference.FullName() : payloadTypeName);
			if (serializationTypeNameAnnotation != null)
			{
				odataCollectionValue.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
			}
			if (collectionValueTypeReference != null)
			{
				odataCollectionValue.SetAnnotation<ODataTypeAnnotation>(new ODataTypeAnnotation(collectionValueTypeReference));
			}
			List<object> list = new List<object>();
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			IEdmTypeReference edmTypeReference = null;
			if (collectionValueTypeReference != null)
			{
				edmTypeReference = collectionValueTypeReference.CollectionDefinition().ElementType;
			}
			CollectionWithoutExpectedTypeValidator collectionWithoutExpectedTypeValidator = null;
			while (base.JsonReader.NodeType != JsonNodeType.EndArray)
			{
				object obj = this.ReadNonEntityValueImplementation(null, edmTypeReference, duplicatePropertyNamesChecker, collectionWithoutExpectedTypeValidator, true, false, false, null, default(bool?));
				ValidationUtils.ValidateCollectionItem(obj, edmTypeReference.IsNullable());
				list.Add(obj);
			}
			base.JsonReader.ReadEndArray();
			odataCollectionValue.Items = new ReadOnlyEnumerable(list);
			this.DecreaseRecursionDepth();
			return odataCollectionValue;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001744C File Offset: 0x0001564C
		private object ReadTypeDefinitionValue(bool insideJsonObjectValue, IEdmTypeDefinitionReference expectedValueTypeReference, bool validateNullValue, string propertyName)
		{
			object obj = this.ReadPrimitiveValue(insideJsonObjectValue, expectedValueTypeReference.AsPrimitive(), validateNullValue, propertyName);
			object obj2;
			try
			{
				obj2 = base.Model.GetPrimitiveValueConverter(expectedValueTypeReference).ConvertFromUnderlyingType(obj);
			}
			catch (OverflowException)
			{
				throw new ODataException(Strings.EdmLibraryExtensions_ValueOverflowForUnderlyingType(obj, expectedValueTypeReference.FullName()));
			}
			return obj2;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000174A4 File Offset: 0x000156A4
		private object ReadPrimitiveValue(bool insideJsonObjectValue, IEdmPrimitiveTypeReference expectedValueTypeReference, bool validateNullValue, string propertyName)
		{
			object obj;
			if (expectedValueTypeReference != null && expectedValueTypeReference.IsSpatial())
			{
				obj = ODataJsonReaderCoreUtils.ReadSpatialValue(base.JsonReader, insideJsonObjectValue, base.JsonLightInputContext, expectedValueTypeReference, validateNullValue, this.recursionDepth, propertyName);
			}
			else
			{
				if (insideJsonObjectValue)
				{
					throw new ODataException(Strings.JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName(JsonNodeType.PrimitiveValue, JsonNodeType.StartObject, propertyName));
				}
				obj = base.JsonReader.ReadPrimitiveValue();
				if (expectedValueTypeReference != null)
				{
					if ((expectedValueTypeReference.IsDecimal() || expectedValueTypeReference.IsInt64()) && obj != null && ((obj is string) ^ base.JsonReader.IsIeee754Compatible))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_ConflictBetweenInputFormatAndParameter(expectedValueTypeReference.FullName()));
					}
					obj = ODataJsonLightReaderUtils.ConvertValue(obj, expectedValueTypeReference, base.MessageReaderSettings, validateNullValue, propertyName, base.JsonLightInputContext.PayloadValueConverter);
				}
				else if (obj is decimal)
				{
					return Convert.ToDouble((decimal)obj);
				}
			}
			return obj;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001757C File Offset: 0x0001577C
		private object ReadEnumValue(bool insideJsonObjectValue, IEdmEnumTypeReference expectedValueTypeReference, bool validateNullValue, string propertyName)
		{
			if (insideJsonObjectValue)
			{
				throw new ODataException(Strings.JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName(JsonNodeType.PrimitiveValue, JsonNodeType.StartObject, propertyName));
			}
			string text = base.JsonReader.ReadStringValue();
			return new ODataEnumValue(text, expectedValueTypeReference.FullName());
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000175C0 File Offset: 0x000157C0
		private ODataComplexValue ReadComplexValue(bool insideJsonObjectValue, bool insideComplexValue, string propertyName, IEdmComplexTypeReference complexValueTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (!insideJsonObjectValue && !insideComplexValue)
			{
				if (base.JsonReader.NodeType != JsonNodeType.StartObject)
				{
					string text = ((complexValueTypeReference != null) ? complexValueTypeReference.FullName() : payloadTypeName);
					throw new ODataException(string.Format(CultureInfo.InvariantCulture, "The property with name '{0}' was found with a value node of type '{1}'; however, a complex value of type '{2}' was expected.", new object[]
					{
						propertyName,
						base.JsonReader.NodeType,
						text
					}));
				}
				base.JsonReader.Read();
			}
			return this.ReadComplexValue(complexValueTypeReference, payloadTypeName, serializationTypeNameAnnotation, duplicatePropertyNamesChecker);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00017868 File Offset: 0x00015A68
		private ODataComplexValue ReadComplexValue(IEdmComplexTypeReference complexValueTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			this.IncreaseRecursionDepth();
			ODataComplexValue complexValue = new ODataComplexValue();
			complexValue.TypeName = ((complexValueTypeReference != null) ? complexValueTypeReference.FullName() : payloadTypeName);
			if (serializationTypeNameAnnotation != null)
			{
				complexValue.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
			}
			if (complexValueTypeReference != null)
			{
				complexValue.SetAnnotation<ODataTypeAnnotation>(new ODataTypeAnnotation(complexValueTypeReference));
			}
			List<ODataProperty> properties = new List<ODataProperty>();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ReadPropertyCustomAnnotationValue = new Func<DuplicatePropertyNamesChecker, string, object>(this.ReadCustomInstanceAnnotationValue);
				base.ProcessProperty(duplicatePropertyNamesChecker, new Func<string, object>(this.ReadTypePropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						break;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
					{
						ODataProperty odataProperty = new ODataProperty();
						odataProperty.Name = propertyName;
						IEdmProperty edmProperty = null;
						bool flag = false;
						if (complexValueTypeReference != null)
						{
							edmProperty = ReaderValidationUtils.ValidateValuePropertyDefined(propertyName, complexValueTypeReference.ComplexDefinition(), this.MessageReaderSettings, out flag);
						}
						if (flag && (this.JsonReader.NodeType == JsonNodeType.StartObject || this.JsonReader.NodeType == JsonNodeType.StartArray))
						{
							this.JsonReader.SkipValue();
							return;
						}
						ODataNullValueBehaviorKind odataNullValueBehaviorKind = ((this.ReadingResponse || edmProperty == null) ? ODataNullValueBehaviorKind.Default : this.Model.NullValueReadBehaviorKind(edmProperty));
						object obj = this.ReadNonEntityValueImplementation(ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(duplicatePropertyNamesChecker, propertyName), (edmProperty == null) ? null : edmProperty.Type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, false, false, propertyName, new bool?(edmProperty == null));
						if (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null)
						{
							duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
							odataProperty.Value = obj;
							Dictionary<string, object> customPropertyAnnotations = duplicatePropertyNamesChecker.GetCustomPropertyAnnotations(propertyName);
							if (customPropertyAnnotations != null)
							{
								foreach (KeyValuePair<string, object> keyValuePair in customPropertyAnnotations)
								{
									if (keyValuePair.Value != null)
									{
										odataProperty.InstanceAnnotations.Add(new ODataInstanceAnnotation(keyValuePair.Key, keyValuePair.Value.ToODataValue()));
									}
								}
							}
							properties.Add(odataProperty);
							return;
						}
						break;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ComplexValuePropertyAnnotationWithoutProperty(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						if (string.CompareOrdinal("odata.type", propertyName) == 0)
						{
							throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ComplexTypeAnnotationNotFirst);
						}
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
					{
						ODataAnnotationNames.ValidateIsCustomAnnotationName(propertyName);
						object obj2 = this.ReadCustomInstanceAnnotationValue(duplicatePropertyNamesChecker, propertyName);
						complexValue.InstanceAnnotations.Add(new ODataInstanceAnnotation(propertyName, obj2.ToODataValue()));
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						return;
					}
				});
			}
			base.JsonReader.ReadEndObject();
			complexValue.Properties = new ReadOnlyEnumerable<ODataProperty>(properties);
			this.DecreaseRecursionDepth();
			return complexValue;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00017978 File Offset: 0x00015B78
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "No easy way to refactor.")]
		private object ReadNonEntityValueImplementation(string payloadTypeName, IEdmTypeReference expectedTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool isTopLevelPropertyValue, bool insideComplexValue, string propertyName, bool? isDynamicProperty = null)
		{
			bool flag = base.JsonReader.NodeType == JsonNodeType.StartObject;
			bool flag2 = !insideComplexValue && payloadTypeName != null;
			bool flag3 = false;
			if (flag || insideComplexValue)
			{
				if (duplicatePropertyNamesChecker == null)
				{
					duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
				}
				else
				{
					duplicatePropertyNamesChecker.Clear();
				}
				if (!insideComplexValue)
				{
					string text;
					flag3 = this.TryReadPayloadTypeFromObject(duplicatePropertyNamesChecker, insideComplexValue, out text);
					if (flag3)
					{
						payloadTypeName = text;
					}
				}
			}
			EdmTypeKind edmTypeKind;
			SerializationTypeNameAnnotation serializationTypeNameAnnotation;
			IEdmTypeReference edmTypeReference = ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, null, expectedTypeReference, payloadTypeName, base.Model, base.MessageReaderSettings, new Func<EdmTypeKind>(this.GetNonEntityValueKind), out edmTypeKind, out serializationTypeNameAnnotation);
			object obj;
			if (ODataJsonReaderCoreUtils.TryReadNullValue(base.JsonReader, base.JsonLightInputContext, edmTypeReference, validateNullValue, propertyName, isDynamicProperty))
			{
				if (isTopLevelPropertyValue)
				{
					throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue("odata.null", "true"));
				}
				if (validateNullValue && edmTypeReference != null && !edmTypeReference.IsNullable && (edmTypeKind != EdmTypeKind.Collection || isDynamicProperty != true))
				{
					throw new ODataException(Strings.ReaderValidationUtils_NullNamedValueForNonNullableType(propertyName, edmTypeReference.FullName()));
				}
				obj = null;
			}
			else
			{
				switch (edmTypeKind)
				{
				case EdmTypeKind.Primitive:
				{
					IEdmPrimitiveTypeReference edmPrimitiveTypeReference = ((edmTypeReference == null) ? null : edmTypeReference.AsPrimitive());
					if (flag3)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue("odata.type"));
					}
					obj = this.ReadPrimitiveValue(flag, edmPrimitiveTypeReference, validateNullValue, propertyName);
					goto IL_0211;
				}
				case EdmTypeKind.Complex:
					if (flag2)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation("odata.type"));
					}
					obj = this.ReadComplexValue(flag, insideComplexValue, propertyName, (edmTypeReference == null) ? null : edmTypeReference.AsComplex(), payloadTypeName, serializationTypeNameAnnotation, duplicatePropertyNamesChecker);
					goto IL_0211;
				case EdmTypeKind.Collection:
				{
					IEdmCollectionTypeReference edmCollectionTypeReference = ValidationUtils.ValidateCollectionType(edmTypeReference);
					if (flag)
					{
						throw new ODataException(Strings.JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName(JsonNodeType.StartArray, JsonNodeType.StartObject, propertyName));
					}
					obj = this.ReadCollectionValue(edmCollectionTypeReference, payloadTypeName, serializationTypeNameAnnotation);
					goto IL_0211;
				}
				case EdmTypeKind.Enum:
				{
					IEdmEnumTypeReference edmEnumTypeReference = ((edmTypeReference == null) ? null : edmTypeReference.AsEnum());
					obj = this.ReadEnumValue(flag, edmEnumTypeReference, validateNullValue, propertyName);
					goto IL_0211;
				}
				case EdmTypeKind.TypeDefinition:
					obj = this.ReadTypeDefinitionValue(flag, expectedTypeReference.AsTypeDefinition(), validateNullValue, propertyName);
					goto IL_0211;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightPropertyAndValueDeserializer_ReadPropertyValue));
				IL_0211:
				if (collectionValidator != null)
				{
					string payloadTypeName2 = ODataJsonLightReaderUtils.GetPayloadTypeName(obj);
					collectionValidator.ValidateCollectionItem(payloadTypeName2, edmTypeKind);
				}
			}
			return obj;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00017BB0 File Offset: 0x00015DB0
		private bool TryReadPayloadTypeFromObject(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool insideComplexValue, out string payloadTypeName)
		{
			bool flag = false;
			payloadTypeName = null;
			if (!insideComplexValue)
			{
				base.JsonReader.ReadStartObject();
			}
			if (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				flag = this.TryReadODataTypeAnnotation(out payloadTypeName);
				if (flag)
				{
					duplicatePropertyNamesChecker.MarkPropertyAsProcessed("odata.type");
				}
			}
			return flag;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00017BF8 File Offset: 0x00015DF8
		private bool ReadingComplexProperty(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, IEdmTypeReference expectedPropertyTypeReference, out string payloadTypeName)
		{
			payloadTypeName = null;
			bool flag = false;
			if (expectedPropertyTypeReference != null)
			{
				flag = expectedPropertyTypeReference.IsComplex();
			}
			if (base.JsonReader.NodeType == JsonNodeType.Property && this.TryReadODataTypeAnnotation(out payloadTypeName))
			{
				duplicatePropertyNamesChecker.MarkPropertyAsProcessed("odata.type");
				IEdmType edmType = null;
				if (expectedPropertyTypeReference != null)
				{
					edmType = expectedPropertyTypeReference.Definition;
				}
				EdmTypeKind edmTypeKind = EdmTypeKind.None;
				IEdmType edmType2 = MetadataUtils.ResolveTypeNameForRead(base.Model, edmType, payloadTypeName, base.MessageReaderSettings.ReaderBehavior, out edmTypeKind);
				if (edmType2 != null)
				{
					flag = edmType2.IsODataComplexTypeKind();
				}
			}
			return flag;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00017C6C File Offset: 0x00015E6C
		private bool IsTopLevelNullValue()
		{
			bool flag = base.ContextUriParseResult != null && base.ContextUriParseResult.IsNullProperty;
			bool flag2 = base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal('@' + "odata.null", base.JsonReader.GetPropertyName()) == 0;
			if (flag2)
			{
				base.JsonReader.ReadNext();
				object obj = base.JsonReader.ReadPrimitiveValue();
				if (!(obj is bool) || !(bool)obj)
				{
					throw new ODataException(Strings.ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation("odata.null", "true"));
				}
			}
			return flag || flag2;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00017D88 File Offset: 0x00015F88
		private void ValidateNoPropertyInNullPayload(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			Func<string, object> func = delegate(string annotationName)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation(annotationName));
			};
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(duplicatePropertyNamesChecker, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						base.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						return;
					}
				});
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00017DE0 File Offset: 0x00015FE0
		private EdmTypeKind GetNonEntityValueKind()
		{
			JsonNodeType nodeType = base.JsonReader.NodeType;
			if (nodeType == JsonNodeType.StartArray)
			{
				return EdmTypeKind.Collection;
			}
			if (nodeType == JsonNodeType.PrimitiveValue)
			{
				return EdmTypeKind.Primitive;
			}
			return EdmTypeKind.Complex;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00017E06 File Offset: 0x00016006
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00017E23 File Offset: 0x00016023
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017E33 File Offset: 0x00016033
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The this is needed in DEBUG build.")]
		private void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x0400031E RID: 798
		private static readonly object missingPropertyValue = new object();

		// Token: 0x0400031F RID: 799
		private int recursionDepth;
	}
}
