using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000213 RID: 531
	internal class ODataJsonLightPropertyAndValueDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x0600155F RID: 5471 RVA: 0x0003E5F4 File Offset: 0x0003C7F4
		internal ODataJsonLightPropertyAndValueDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0003F7C4 File Offset: 0x0003D9C4
		internal ODataProperty ReadTopLevelProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			base.ReadPayloadStart(ODataPayloadKind.Property, propertyAndAnnotationCollector, false, false);
			ODataProperty odataProperty = this.ReadTopLevelPropertyImplementation(expectedPropertyTypeReference, propertyAndAnnotationCollector);
			base.ReadPayloadEnd(false);
			return odataProperty;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
		internal object ReadNonEntityValue(string payloadTypeName, IEdmTypeReference expectedValueTypeReference, PropertyAndAnnotationCollector propertyAndAnnotationCollector, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool isTopLevelPropertyValue, bool insideComplexValue, string propertyName, bool? isDynamicProperty = null)
		{
			return this.ReadNonEntityValueImplementation(payloadTypeName, expectedValueTypeReference, propertyAndAnnotationCollector, collectionValidator, validateNullValue, isTopLevelPropertyValue, insideComplexValue, propertyName, isDynamicProperty);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0003F818 File Offset: 0x0003DA18
		internal object ReadCustomInstanceAnnotationValue(PropertyAndAnnotationCollector propertyAndAnnotationCollector, string name)
		{
			string text = null;
			object obj;
			if (propertyAndAnnotationCollector.GetODataPropertyAnnotations(name).TryGetValue("odata.type", ref obj))
			{
				text = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName((string)obj));
			}
			return this.ReadODataOrCustomInstanceAnnotationValue(name, text);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0003F858 File Offset: 0x0003DA58
		internal object ReadODataOrCustomInstanceAnnotationValue(string annotationName, string odataType)
		{
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfTerm(annotationName, base.Model);
			return this.ReadNonEntityValueImplementation(odataType, edmTypeReference, null, null, false, false, false, annotationName, default(bool?));
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0003F88C File Offset: 0x0003DA8C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Each code path casts to bool at most one time, and only if needed.")]
		internal static IEdmTypeReference ResolveUntypedType(JsonNodeType jsonReaderNodeType, object jsonReaderValue, string payloadTypeName, IEdmTypeReference payloadTypeReference, Func<object, string, IEdmTypeReference> primitiveTypeResolver, bool readUntypedAsString, bool generateTypeIfMissing)
		{
			if (payloadTypeReference != null && (payloadTypeReference.TypeKind() != EdmTypeKind.Untyped || readUntypedAsString))
			{
				return payloadTypeReference;
			}
			if (readUntypedAsString)
			{
				if (jsonReaderNodeType == JsonNodeType.PrimitiveValue && jsonReaderValue is bool)
				{
					return EdmCoreModel.Instance.GetBoolean(true);
				}
				return EdmCoreModel.Instance.GetUntyped();
			}
			else if (jsonReaderNodeType != JsonNodeType.StartObject)
			{
				if (jsonReaderNodeType != JsonNodeType.StartArray)
				{
					if (jsonReaderNodeType == JsonNodeType.PrimitiveValue)
					{
						IEdmTypeReference edmTypeReference;
						if (primitiveTypeResolver != null)
						{
							edmTypeReference = primitiveTypeResolver.Invoke(jsonReaderValue, payloadTypeName);
							if (edmTypeReference != null)
							{
								return edmTypeReference;
							}
						}
						if (jsonReaderValue == null)
						{
							if (payloadTypeName != null)
							{
								string text;
								string text2;
								bool flag;
								TypeUtils.ParseQualifiedTypeName(payloadTypeName, out text, out text2, out flag);
								edmTypeReference = new EdmUntypedStructuredType(text, text2).ToTypeReference(true);
								if (!flag)
								{
									return edmTypeReference;
								}
								return new EdmCollectionType(edmTypeReference).ToTypeReference(true);
							}
							else
							{
								edmTypeReference = EdmCoreModel.Instance.GetString(true);
							}
						}
						else if (jsonReaderValue is bool)
						{
							edmTypeReference = EdmCoreModel.Instance.GetBoolean(true);
						}
						else if (jsonReaderValue is string)
						{
							edmTypeReference = EdmCoreModel.Instance.GetString(true);
						}
						else
						{
							edmTypeReference = EdmCoreModel.Instance.GetDecimal(true);
						}
						if (payloadTypeName != null)
						{
							string text;
							string text2;
							bool flag;
							TypeUtils.ParseQualifiedTypeName(payloadTypeName, out text, out text2, out flag);
							if (flag)
							{
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_CollectionTypeNotExpected(payloadTypeName));
							}
							edmTypeReference = new EdmTypeDefinition(text, text2, edmTypeReference.PrimitiveKind()).ToTypeReference(true);
						}
						return edmTypeReference;
					}
					return EdmCoreModel.Instance.GetUntyped();
				}
				else
				{
					if (payloadTypeName == null || !generateTypeIfMissing)
					{
						return new EdmCollectionType(new EdmUntypedStructuredType().ToTypeReference(true)).ToTypeReference(true);
					}
					string text;
					string text2;
					bool flag;
					TypeUtils.ParseQualifiedTypeName(payloadTypeName, out text, out text2, out flag);
					if (!flag)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_CollectionTypeExpected(payloadTypeName));
					}
					return new EdmCollectionType(new EdmUntypedStructuredType(text, text2).ToTypeReference(true)).ToTypeReference(true);
				}
			}
			else
			{
				if (payloadTypeName == null || !generateTypeIfMissing)
				{
					return new EdmUntypedStructuredType().ToTypeReference(true);
				}
				string text;
				string text2;
				bool flag;
				TypeUtils.ParseQualifiedTypeName(payloadTypeName, out text, out text2, out flag);
				if (flag)
				{
					throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_CollectionTypeNotExpected(payloadTypeName));
				}
				return new EdmUntypedStructuredType(text, text2).ToTypeReference(true);
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0003FA48 File Offset: 0x0003DC48
		protected string TryReadOrPeekPayloadType(PropertyAndAnnotationCollector propertyAndAnnotationCollector, string propertyName, bool insideComplexValue)
		{
			string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(propertyAndAnnotationCollector, propertyName);
			bool flag = base.JsonReader.NodeType == JsonNodeType.StartObject;
			if (string.IsNullOrEmpty(text) && flag)
			{
				try
				{
					base.JsonReader.StartBuffering();
					propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
					string text2;
					bool flag2 = this.TryReadPayloadTypeFromObject(propertyAndAnnotationCollector, insideComplexValue, out text2);
					if (flag2)
					{
						text = text2;
					}
				}
				finally
				{
					base.JsonReader.StopBuffering();
				}
			}
			return text;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0003FAB8 File Offset: 0x0003DCB8
		protected ODataJsonLightReaderNestedResourceInfo InnerReadUndeclaredProperty(IODataJsonLightReaderResourceState resourceState, string propertyName, bool isTopLevelPropertyValue)
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = resourceState.PropertyAndAnnotationCollector;
			bool flag = false;
			string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(propertyAndAnnotationCollector, propertyName);
			string text2 = this.TryReadOrPeekPayloadType(propertyAndAnnotationCollector, propertyName, flag);
			EdmTypeKind edmTypeKind;
			IEdmType edmType = ReaderValidationUtils.ResolvePayloadTypeName(base.Model, null, text2, EdmTypeKind.Complex, base.MessageReaderSettings.ClientCustomTypeResolver, out edmTypeKind);
			IEdmTypeReference edmTypeReference = null;
			if (!string.IsNullOrEmpty(text2) && edmType != null)
			{
				EdmTypeKind edmTypeKind2;
				ODataTypeAnnotation odataTypeAnnotation;
				edmTypeReference = this.ReaderValidator.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, default(bool?), null, null, text2, base.Model, new Func<EdmTypeKind>(this.GetNonEntityValueKind), out edmTypeKind2, out odataTypeAnnotation);
			}
			edmTypeReference = ODataJsonLightPropertyAndValueDeserializer.ResolveUntypedType(base.JsonReader.NodeType, base.JsonReader.Value, text2, edmTypeReference, base.MessageReaderSettings.PrimitiveTypeResolver, base.MessageReaderSettings.ReadUntypedAsString, !base.MessageReaderSettings.ThrowIfTypeConflictsWithMetadata);
			if (edmTypeReference.ToStructuredType() != null)
			{
				bool flag2 = edmTypeReference.IsCollection();
				ODataJsonLightPropertyAndValueDeserializer.ValidateExpandedNestedResourceInfoPropertyValue(base.JsonReader, new bool?(flag2), propertyName);
				ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo;
				if (flag2)
				{
					odataJsonLightReaderNestedResourceInfo = (base.ReadingResponse ? ODataJsonLightPropertyAndValueDeserializer.ReadExpandedResourceSetNestedResourceInfo(resourceState, null, edmTypeReference.ToStructuredType(), propertyName) : ODataJsonLightPropertyAndValueDeserializer.ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(resourceState, null, propertyName, true));
				}
				else
				{
					odataJsonLightReaderNestedResourceInfo = (base.ReadingResponse ? ODataJsonLightPropertyAndValueDeserializer.ReadExpandedResourceNestedResourceInfo(resourceState, null, propertyName, edmTypeReference.ToStructuredType(), base.MessageReaderSettings) : ODataJsonLightPropertyAndValueDeserializer.ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(resourceState, null, propertyName, true));
				}
				resourceState.PropertyAndAnnotationCollector.ValidatePropertyUniquenessOnNestedResourceInfoStart(odataJsonLightReaderNestedResourceInfo.NestedResourceInfo);
				return odataJsonLightReaderNestedResourceInfo;
			}
			object obj;
			if (!(edmTypeReference is IEdmUntypedTypeReference))
			{
				obj = this.ReadNonEntityValueImplementation(text, edmTypeReference, null, null, false, isTopLevelPropertyValue, flag, propertyName, default(bool?));
			}
			else
			{
				obj = base.JsonReader.ReadAsUntypedOrNullValue();
			}
			ODataJsonLightPropertyAndValueDeserializer.AddResourceProperty(resourceState, propertyName, obj);
			return null;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0003FC58 File Offset: 0x0003DE58
		protected static void ValidateExpandedNestedResourceInfoPropertyValue(IJsonReader jsonReader, bool? isCollection, string propertyName)
		{
			JsonNodeType nodeType = jsonReader.NodeType;
			if (nodeType == JsonNodeType.StartArray)
			{
				if (isCollection == false)
				{
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_CannotReadSingletonNestedResource(nodeType, propertyName));
				}
			}
			else
			{
				if ((nodeType != JsonNodeType.PrimitiveValue || jsonReader.Value != null) && nodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_CannotReadNestedResource(propertyName));
				}
				if (isCollection == true)
				{
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_CannotReadCollectionNestedResource(nodeType, propertyName));
				}
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0003FCE4 File Offset: 0x0003DEE4
		protected static ODataJsonLightReaderNestedResourceInfo ReadNonExpandedResourceSetNestedResourceInfo(IODataJsonLightReaderResourceState resourceState, IEdmStructuralProperty collectionProperty, IEdmStructuredType nestedResourceType, string propertyName)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = propertyName,
				IsCollection = new bool?(true),
				IsComplex = true
			};
			ODataResourceSet odataResourceSet = new ODataResourceSet();
			foreach (KeyValuePair<string, object> keyValuePair in resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(odataNestedResourceInfo.Name))
			{
				string key = keyValuePair.Key;
				if (!(key == "odata.nextLink"))
				{
					if (!(key == "odata.count"))
					{
						if (!(key == "odata.type"))
						{
							throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedComplexCollectionPropertyAnnotation(odataNestedResourceInfo.Name, keyValuePair.Key));
						}
						odataResourceSet.TypeName = (string)keyValuePair.Value;
					}
					else
					{
						odataResourceSet.Count = (long?)keyValuePair.Value;
					}
				}
				else
				{
					odataResourceSet.NextPageLink = (Uri)keyValuePair.Value;
				}
			}
			return ODataJsonLightReaderNestedResourceInfo.CreateResourceSetReaderNestedResourceInfo(odataNestedResourceInfo, collectionProperty, nestedResourceType, odataResourceSet);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0003FDF4 File Offset: 0x0003DFF4
		protected static ODataJsonLightReaderNestedResourceInfo ReadNonExpandedResourceNestedResourceInfo(IODataJsonLightReaderResourceState resourceState, IEdmStructuralProperty complexProperty, IEdmStructuredType nestedResourceType, string propertyName)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = propertyName,
				IsCollection = new bool?(false),
				IsComplex = true
			};
			if (ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(resourceState.PropertyAndAnnotationCollector, odataNestedResourceInfo.Name) != null)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation("odata.type"));
			}
			return ODataJsonLightReaderNestedResourceInfo.CreateResourceReaderNestedResourceInfo(odataNestedResourceInfo, complexProperty, nestedResourceType);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0003FE4C File Offset: 0x0003E04C
		protected static ODataJsonLightReaderNestedResourceInfo ReadExpandedResourceNestedResourceInfo(IODataJsonLightReaderResourceState resourceState, IEdmNavigationProperty navigationProperty, string propertyName, IEdmStructuredType propertyType, ODataMessageReaderSettings messageReaderSettings)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = propertyName,
				IsCollection = new bool?(false)
			};
			foreach (KeyValuePair<string, object> keyValuePair in resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(odataNestedResourceInfo.Name))
			{
				string key = keyValuePair.Key;
				if (!(key == "odata.navigationLink"))
				{
					if (!(key == "odata.associationLink"))
					{
						if (!(key == "odata.context"))
						{
							if (!(key == "odata.type"))
							{
								if (messageReaderSettings.ThrowOnUndeclaredPropertyForNonOpenType)
								{
									throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation(odataNestedResourceInfo.Name, keyValuePair.Key));
								}
							}
							else
							{
								odataNestedResourceInfo.TypeAnnotation = new ODataTypeAnnotation((string)keyValuePair.Value);
							}
						}
						else
						{
							odataNestedResourceInfo.ContextUrl = (Uri)keyValuePair.Value;
						}
					}
					else
					{
						odataNestedResourceInfo.AssociationLinkUrl = (Uri)keyValuePair.Value;
					}
				}
				else
				{
					odataNestedResourceInfo.Url = (Uri)keyValuePair.Value;
				}
			}
			return ODataJsonLightReaderNestedResourceInfo.CreateResourceReaderNestedResourceInfo(odataNestedResourceInfo, navigationProperty, propertyType);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0003FF78 File Offset: 0x0003E178
		protected static ODataJsonLightReaderNestedResourceInfo ReadExpandedResourceSetNestedResourceInfo(IODataJsonLightReaderResourceState resourceState, IEdmNavigationProperty navigationProperty, IEdmStructuredType propertyType, string propertyName)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = propertyName,
				IsCollection = new bool?(true)
			};
			ODataResourceSet odataResourceSet = new ODataResourceSet();
			foreach (KeyValuePair<string, object> keyValuePair in resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(odataNestedResourceInfo.Name))
			{
				string key = keyValuePair.Key;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
				if (num <= 1661917678U)
				{
					if (num != 270005791U)
					{
						if (num != 1208232771U)
						{
							if (num == 1661917678U)
							{
								if (!(key == "odata.deltaLink"))
								{
								}
							}
						}
						else if (key == "odata.context")
						{
							odataNestedResourceInfo.ContextUrl = (Uri)keyValuePair.Value;
							continue;
						}
					}
					else if (key == "odata.count")
					{
						odataResourceSet.Count = (long?)keyValuePair.Value;
						continue;
					}
				}
				else if (num <= 3438618421U)
				{
					if (num != 1895014474U)
					{
						if (num == 3438618421U)
						{
							if (key == "odata.nextLink")
							{
								odataResourceSet.NextPageLink = (Uri)keyValuePair.Value;
								continue;
							}
						}
					}
					else if (key == "odata.navigationLink")
					{
						odataNestedResourceInfo.Url = (Uri)keyValuePair.Value;
						continue;
					}
				}
				else if (num != 3528111475U)
				{
					if (num == 4075776568U)
					{
						if (key == "odata.type")
						{
							odataResourceSet.TypeName = (string)keyValuePair.Value;
							continue;
						}
					}
				}
				else if (key == "odata.associationLink")
				{
					odataNestedResourceInfo.AssociationLinkUrl = (Uri)keyValuePair.Value;
					continue;
				}
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation(odataNestedResourceInfo.Name, keyValuePair.Key));
			}
			return ODataJsonLightReaderNestedResourceInfo.CreateResourceSetReaderNestedResourceInfo(odataNestedResourceInfo, navigationProperty, propertyType, odataResourceSet);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00040190 File Offset: 0x0003E390
		protected static ODataJsonLightReaderNestedResourceInfo ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(IODataJsonLightReaderResourceState resourceState, IEdmNavigationProperty navigationProperty, string propertyName, bool isExpanded)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = propertyName,
				IsCollection = new bool?(false)
			};
			ODataEntityReferenceLink odataEntityReferenceLink = null;
			foreach (KeyValuePair<string, object> keyValuePair in resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(odataNestedResourceInfo.Name))
			{
				string key = keyValuePair.Key;
				if (!(key == "odata.bind"))
				{
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation(odataNestedResourceInfo.Name, keyValuePair.Key, "odata.bind"));
				}
				LinkedList<ODataEntityReferenceLink> linkedList = keyValuePair.Value as LinkedList<ODataEntityReferenceLink>;
				if (linkedList != null)
				{
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_ArrayValueForSingletonBindPropertyAnnotation(odataNestedResourceInfo.Name, "odata.bind"));
				}
				if (isExpanded)
				{
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_SingletonNavigationPropertyWithBindingAndValue(odataNestedResourceInfo.Name, "odata.bind"));
				}
				odataEntityReferenceLink = (ODataEntityReferenceLink)keyValuePair.Value;
			}
			return ODataJsonLightReaderNestedResourceInfo.CreateSingletonEntityReferenceLinkInfo(odataNestedResourceInfo, navigationProperty, odataEntityReferenceLink, isExpanded);
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00040290 File Offset: 0x0003E490
		protected static ODataJsonLightReaderNestedResourceInfo ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(IODataJsonLightReaderResourceState resourceState, IEdmNavigationProperty navigationProperty, string propertyName, bool isExpanded)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = propertyName,
				IsCollection = new bool?(true)
			};
			LinkedList<ODataEntityReferenceLink> linkedList = null;
			foreach (KeyValuePair<string, object> keyValuePair in resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(odataNestedResourceInfo.Name))
			{
				string key = keyValuePair.Key;
				if (!(key == "odata.bind"))
				{
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation(odataNestedResourceInfo.Name, keyValuePair.Key, "odata.bind"));
				}
				ODataEntityReferenceLink odataEntityReferenceLink = keyValuePair.Value as ODataEntityReferenceLink;
				if (odataEntityReferenceLink != null)
				{
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_StringValueForCollectionBindPropertyAnnotation(odataNestedResourceInfo.Name, "odata.bind"));
				}
				linkedList = (LinkedList<ODataEntityReferenceLink>)keyValuePair.Value;
			}
			return ODataJsonLightReaderNestedResourceInfo.CreateCollectionEntityReferenceLinksInfo(odataNestedResourceInfo, navigationProperty, linkedList, isExpanded);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00040370 File Offset: 0x0003E570
		protected static string ValidateDataPropertyTypeNameAnnotation(PropertyAndAnnotationCollector propertyAndAnnotationCollector, string propertyName)
		{
			string text = null;
			foreach (KeyValuePair<string, object> keyValuePair in propertyAndAnnotationCollector.GetODataPropertyAnnotations(propertyName))
			{
				if (string.CompareOrdinal(keyValuePair.Key, "odata.type") == 0)
				{
					text = (string)keyValuePair.Value;
				}
			}
			return text;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000403DC File Offset: 0x0003E5DC
		protected static ODataProperty AddResourceProperty(IODataJsonLightReaderResourceState resourceState, string propertyName, object propertyValue)
		{
			ODataProperty odataProperty = new ODataProperty
			{
				Name = propertyName,
				Value = propertyValue
			};
			ODataJsonLightPropertyAndValueDeserializer.AttachODataAnnotations(resourceState, propertyName, odataProperty);
			foreach (KeyValuePair<string, object> keyValuePair in resourceState.PropertyAndAnnotationCollector.GetCustomPropertyAnnotations(propertyName))
			{
				if (keyValuePair.Value != null)
				{
					odataProperty.InstanceAnnotations.Add(new ODataInstanceAnnotation(keyValuePair.Key, keyValuePair.Value.ToODataValue()));
				}
			}
			resourceState.PropertyAndAnnotationCollector.CheckForDuplicatePropertyNames(odataProperty);
			ODataResource resource = resourceState.Resource;
			resource.Properties = resource.Properties.ConcatToReadOnlyEnumerable("Properties", odataProperty);
			return odataProperty;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0004049C File Offset: 0x0003E69C
		protected static void AttachODataAnnotations(IODataJsonLightReaderResourceState resourceState, string propertyName, ODataProperty property)
		{
			foreach (KeyValuePair<string, object> keyValuePair in ((propertyName.Length == 0) ? resourceState.PropertyAndAnnotationCollector.GetODataScopeAnnotation() : resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(propertyName)))
			{
				if (string.Equals(keyValuePair.Key, "odata.type", 4) || string.Equals(keyValuePair.Key, "@type", 4))
				{
					property.TypeAnnotation = new ODataTypeAnnotation(ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName((string)keyValuePair.Value)));
				}
				else
				{
					Uri uri;
					ODataValue odataValue = (((uri = keyValuePair.Value as Uri) != null) ? new ODataPrimitiveValue(uri.OriginalString) : keyValuePair.Value.ToODataValue());
					property.InstanceAnnotations.Add(new ODataInstanceAnnotation(keyValuePair.Key, odataValue, true));
				}
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00040598 File Offset: 0x0003E798
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

		// Token: 0x06001572 RID: 5490 RVA: 0x000405B8 File Offset: 0x0003E7B8
		protected string ReadODataTypeAnnotationValue()
		{
			string text = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName(base.JsonReader.ReadStringValue()));
			if (text == null)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(text));
			}
			return text;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x000405EC File Offset: 0x0003E7EC
		protected object ReadTypePropertyAnnotationValue(string propertyAnnotationName)
		{
			string text;
			if (this.TryReadODataTypeAnnotationValue(propertyAnnotationName, out text))
			{
				return text;
			}
			throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyAnnotationName));
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00040614 File Offset: 0x0003E814
		protected EdmTypeKind GetNonEntityValueKind()
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

		// Token: 0x06001575 RID: 5493 RVA: 0x0004063C File Offset: 0x0003E83C
		private bool TryReadODataTypeAnnotation(out string payloadTypeName)
		{
			payloadTypeName = null;
			bool flag = false;
			string propertyName = base.JsonReader.GetPropertyName();
			if (string.CompareOrdinal(propertyName, "@odata.type") == 0 || base.CompareSimplifiedODataAnnotation("@type", propertyName))
			{
				base.JsonReader.ReadNext();
				payloadTypeName = this.ReadODataTypeAnnotationValue();
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0004068C File Offset: 0x0003E88C
		private ODataProperty ReadTopLevelPropertyImplementation(IEdmTypeReference expectedPropertyTypeReference, PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			expectedPropertyTypeReference = this.UpdateExpectedTypeBasedOnContextUri(expectedPropertyTypeReference);
			object propertyValue = ODataJsonLightPropertyAndValueDeserializer.missingPropertyValue;
			Collection<ODataInstanceAnnotation> customInstanceAnnotations = new Collection<ODataInstanceAnnotation>();
			string payloadTypeName = null;
			if (this.ReadingComplexProperty(propertyAndAnnotationCollector, expectedPropertyTypeReference, out payloadTypeName))
			{
				propertyValue = this.ReadNonEntityValue(payloadTypeName, expectedPropertyTypeReference, propertyAndAnnotationCollector, null, true, true, true, null, default(bool?));
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
					base.ProcessProperty(propertyAndAnnotationCollector, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
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
							if (ODataJsonLightPropertyAndValueDeserializer.missingPropertyValue != propertyValue)
							{
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty("odata.type", "value"));
							}
							payloadTypeName = this.ReadODataTypeAnnotationValue();
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						{
							ODataAnnotationNames.ValidateIsCustomAnnotationName(propertyName);
							object obj = this.ReadCustomInstanceAnnotationValue(propertyAndAnnotationCollector, propertyName);
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
				if (ODataJsonLightPropertyAndValueDeserializer.missingPropertyValue == propertyValue)
				{
					throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload);
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

		// Token: 0x06001577 RID: 5495 RVA: 0x000407F0 File Offset: 0x0003E9F0
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

		// Token: 0x06001578 RID: 5496 RVA: 0x0004086C File Offset: 0x0003EA6C
		private ODataCollectionValue ReadCollectionValue(IEdmCollectionTypeReference collectionValueTypeReference, string payloadTypeName, ODataTypeAnnotation typeAnnotation)
		{
			this.IncreaseRecursionDepth();
			base.JsonReader.ReadStartArray();
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			odataCollectionValue.TypeName = ((collectionValueTypeReference != null) ? collectionValueTypeReference.FullName() : payloadTypeName);
			if (typeAnnotation != null)
			{
				odataCollectionValue.TypeAnnotation = typeAnnotation;
			}
			List<object> list = new List<object>();
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			IEdmTypeReference edmTypeReference = null;
			if (collectionValueTypeReference != null)
			{
				edmTypeReference = collectionValueTypeReference.CollectionDefinition().ElementType;
			}
			CollectionWithoutExpectedTypeValidator collectionWithoutExpectedTypeValidator = null;
			while (base.JsonReader.NodeType != JsonNodeType.EndArray)
			{
				object obj = this.ReadNonEntityValueImplementation(null, edmTypeReference, propertyAndAnnotationCollector, collectionWithoutExpectedTypeValidator, true, false, false, null, default(bool?));
				ValidationUtils.ValidateCollectionItem(obj, edmTypeReference.IsNullable());
				list.Add(obj);
			}
			base.JsonReader.ReadEndArray();
			odataCollectionValue.Items = new ReadOnlyEnumerable<object>(list);
			this.DecreaseRecursionDepth();
			return odataCollectionValue;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0004092C File Offset: 0x0003EB2C
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

		// Token: 0x0600157A RID: 5498 RVA: 0x00040984 File Offset: 0x0003EB84
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

		// Token: 0x0600157B RID: 5499 RVA: 0x00040A5C File Offset: 0x0003EC5C
		private object ReadEnumValue(bool insideJsonObjectValue, IEdmEnumTypeReference expectedValueTypeReference, bool validateNullValue, string propertyName)
		{
			if (insideJsonObjectValue)
			{
				throw new ODataException(Strings.JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName(JsonNodeType.PrimitiveValue, JsonNodeType.StartObject, propertyName));
			}
			string text = base.JsonReader.ReadStringValue();
			return new ODataEnumValue(text, expectedValueTypeReference.FullName());
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00040AA0 File Offset: 0x0003ECA0
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "No easy way to refactor.")]
		private object ReadNonEntityValueImplementation(string payloadTypeName, IEdmTypeReference expectedTypeReference, PropertyAndAnnotationCollector propertyAndAnnotationCollector, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool isTopLevelPropertyValue, bool insideComplexValue, string propertyName, bool? isDynamicProperty = null)
		{
			bool flag = base.JsonReader.NodeType == JsonNodeType.StartObject;
			bool flag2 = false;
			if (flag || insideComplexValue)
			{
				if (propertyAndAnnotationCollector == null)
				{
					propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
				}
				else
				{
					propertyAndAnnotationCollector.Reset();
				}
				if (!insideComplexValue)
				{
					string text;
					flag2 = this.TryReadPayloadTypeFromObject(propertyAndAnnotationCollector, insideComplexValue, out text);
					if (flag2)
					{
						payloadTypeName = text;
					}
				}
			}
			EdmTypeKind edmTypeKind;
			ODataTypeAnnotation odataTypeAnnotation;
			IEdmTypeReference edmTypeReference = this.ReaderValidator.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, default(bool?), null, expectedTypeReference, payloadTypeName, base.Model, new Func<EdmTypeKind>(this.GetNonEntityValueKind), out edmTypeKind, out odataTypeAnnotation);
			if (edmTypeKind == EdmTypeKind.Untyped || edmTypeKind == EdmTypeKind.None)
			{
				edmTypeReference = ODataJsonLightPropertyAndValueDeserializer.ResolveUntypedType(base.JsonReader.NodeType, base.JsonReader.Value, payloadTypeName, expectedTypeReference, base.MessageReaderSettings.PrimitiveTypeResolver, base.MessageReaderSettings.ReadUntypedAsString, !base.MessageReaderSettings.ThrowIfTypeConflictsWithMetadata);
				edmTypeKind = edmTypeReference.TypeKind();
			}
			object obj;
			if (ODataJsonReaderCoreUtils.TryReadNullValue(base.JsonReader, base.JsonLightInputContext, edmTypeReference, validateNullValue, propertyName, isDynamicProperty))
			{
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
					if (flag2)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue("odata.type"));
					}
					obj = this.ReadPrimitiveValue(flag, edmPrimitiveTypeReference, validateNullValue, propertyName);
					goto IL_021F;
				}
				case EdmTypeKind.Collection:
				{
					IEdmCollectionTypeReference edmCollectionTypeReference = ValidationUtils.ValidateCollectionType(edmTypeReference);
					if (flag)
					{
						throw new ODataException(Strings.JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName(JsonNodeType.StartArray, JsonNodeType.StartObject, propertyName));
					}
					obj = this.ReadCollectionValue(edmCollectionTypeReference, payloadTypeName, odataTypeAnnotation);
					goto IL_021F;
				}
				case EdmTypeKind.Enum:
				{
					IEdmEnumTypeReference edmEnumTypeReference = ((edmTypeReference == null) ? null : edmTypeReference.AsEnum());
					obj = this.ReadEnumValue(flag, edmEnumTypeReference, validateNullValue, propertyName);
					goto IL_021F;
				}
				case EdmTypeKind.TypeDefinition:
					obj = this.ReadTypeDefinitionValue(flag, expectedTypeReference.AsTypeDefinition(), validateNullValue, propertyName);
					goto IL_021F;
				case EdmTypeKind.Untyped:
					obj = base.JsonReader.ReadAsUntypedOrNullValue();
					goto IL_021F;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightPropertyAndValueDeserializer_ReadPropertyValue));
				IL_021F:
				if (collectionValidator != null)
				{
					string payloadTypeName2 = ODataJsonLightReaderUtils.GetPayloadTypeName(obj);
					collectionValidator.ValidateCollectionItem(payloadTypeName2, edmTypeKind);
				}
			}
			return obj;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00040CE8 File Offset: 0x0003EEE8
		private bool TryReadPayloadTypeFromObject(PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool insideComplexValue, out string payloadTypeName)
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
					propertyAndAnnotationCollector.MarkPropertyAsProcessed("odata.type");
				}
			}
			return flag;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00040D30 File Offset: 0x0003EF30
		private bool ReadingComplexProperty(PropertyAndAnnotationCollector propertyAndAnnotationCollector, IEdmTypeReference expectedPropertyTypeReference, out string payloadTypeName)
		{
			payloadTypeName = null;
			bool flag = false;
			if (expectedPropertyTypeReference != null)
			{
				flag = expectedPropertyTypeReference.IsComplex();
			}
			if (base.JsonReader.NodeType == JsonNodeType.Property && this.TryReadODataTypeAnnotation(out payloadTypeName))
			{
				propertyAndAnnotationCollector.MarkPropertyAsProcessed("odata.type");
				IEdmType edmType = null;
				if (expectedPropertyTypeReference != null)
				{
					edmType = expectedPropertyTypeReference.Definition;
				}
				EdmTypeKind edmTypeKind = EdmTypeKind.None;
				IEdmType edmType2 = MetadataUtils.ResolveTypeNameForRead(base.Model, edmType, payloadTypeName, base.MessageReaderSettings.ClientCustomTypeResolver, out edmTypeKind);
				if (edmType2 != null)
				{
					flag = edmType2.IsODataComplexTypeKind();
				}
			}
			return flag;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00040DA3 File Offset: 0x0003EFA3
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00040DC0 File Offset: 0x0003EFC0
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0000250D File Offset: 0x0000070D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This is needed in DEBUG build.")]
		private void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x04000A30 RID: 2608
		private static readonly object missingPropertyValue = new object();

		// Token: 0x04000A31 RID: 2609
		private int recursionDepth;
	}
}
