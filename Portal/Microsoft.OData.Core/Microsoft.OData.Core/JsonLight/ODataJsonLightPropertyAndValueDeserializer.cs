using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200024C RID: 588
	internal class ODataJsonLightPropertyAndValueDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06001A04 RID: 6660 RVA: 0x0004B1CC File Offset: 0x000493CC
		internal ODataJsonLightPropertyAndValueDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0004CDB0 File Offset: 0x0004AFB0
		internal ODataProperty ReadTopLevelProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			base.ReadPayloadStart(ODataPayloadKind.Property, propertyAndAnnotationCollector, false, false);
			ODataProperty odataProperty = this.ReadTopLevelPropertyImplementation(expectedPropertyTypeReference, propertyAndAnnotationCollector);
			base.ReadPayloadEnd(false);
			return odataProperty;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0004CDE0 File Offset: 0x0004AFE0
		internal Task<ODataProperty> ReadTopLevelPropertyAsync(IEdmTypeReference expectedPropertyTypeReference)
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			return base.ReadPayloadStartAsync(ODataPayloadKind.Property, propertyAndAnnotationCollector, false, false).FollowOnSuccessWith(delegate(Task t)
			{
				ODataProperty odataProperty = this.ReadTopLevelPropertyImplementation(expectedPropertyTypeReference, propertyAndAnnotationCollector);
				this.ReadPayloadEnd(false);
				return odataProperty;
			});
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0004CE30 File Offset: 0x0004B030
		internal object ReadNonEntityValue(string payloadTypeName, IEdmTypeReference expectedValueTypeReference, PropertyAndAnnotationCollector propertyAndAnnotationCollector, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool isTopLevelPropertyValue, bool insideResourceValue, string propertyName, bool? isDynamicProperty = null)
		{
			return this.ReadNonEntityValueImplementation(payloadTypeName, expectedValueTypeReference, propertyAndAnnotationCollector, collectionValidator, validateNullValue, isTopLevelPropertyValue, insideResourceValue, propertyName, isDynamicProperty);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0004CE54 File Offset: 0x0004B054
		internal object ReadCustomInstanceAnnotationValue(PropertyAndAnnotationCollector propertyAndAnnotationCollector, string name)
		{
			string text = null;
			object obj;
			if (propertyAndAnnotationCollector.GetODataPropertyAnnotations(name).TryGetValue("odata.type", out obj))
			{
				text = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName((string)obj));
			}
			return this.ReadODataOrCustomInstanceAnnotationValue(name, text);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0004CE94 File Offset: 0x0004B094
		internal object ReadODataOrCustomInstanceAnnotationValue(string annotationName, string odataType)
		{
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfTerm(annotationName, base.Model);
			return this.ReadNonEntityValueImplementation(odataType, edmTypeReference, null, null, false, false, false, annotationName, null);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0004CEC8 File Offset: 0x0004B0C8
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
							edmTypeReference = primitiveTypeResolver(jsonReaderValue, payloadTypeName);
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

		// Token: 0x06001A0B RID: 6667 RVA: 0x0004D084 File Offset: 0x0004B284
		protected string TryReadOrPeekPayloadType(PropertyAndAnnotationCollector propertyAndAnnotationCollector, string propertyName, bool insideResourceValue)
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
					bool flag2 = this.TryReadPayloadTypeFromObject(propertyAndAnnotationCollector, insideResourceValue, out text2);
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

		// Token: 0x06001A0C RID: 6668 RVA: 0x0004D0F4 File Offset: 0x0004B2F4
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
				edmTypeReference = this.ReaderValidator.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, null, null, null, text2, base.Model, new Func<EdmTypeKind>(this.GetNonEntityValueKind), out edmTypeKind2, out odataTypeAnnotation);
			}
			edmTypeReference = ODataJsonLightPropertyAndValueDeserializer.ResolveUntypedType(base.JsonReader.NodeType, base.JsonReader.Value, text2, edmTypeReference, base.MessageReaderSettings.PrimitiveTypeResolver, base.MessageReaderSettings.ReadUntypedAsString, !base.MessageReaderSettings.ThrowIfTypeConflictsWithMetadata);
			if (edmTypeReference.ToStructuredType() != null)
			{
				bool flag2 = edmTypeReference.IsCollection();
				ODataJsonLightPropertyAndValueDeserializer.ValidateExpandedNestedResourceInfoPropertyValue(base.JsonReader, new bool?(flag2), propertyName);
				ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo;
				if (flag2)
				{
					odataJsonLightReaderNestedResourceInfo = (base.ReadingResponse ? ODataJsonLightPropertyAndValueDeserializer.ReadExpandedResourceSetNestedResourceInfo(resourceState, null, edmTypeReference.ToStructuredType(), propertyName, false) : ODataJsonLightPropertyAndValueDeserializer.ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(resourceState, null, propertyName, true));
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
				obj = this.ReadNonEntityValueImplementation(text, edmTypeReference, null, null, false, isTopLevelPropertyValue, flag, propertyName, null);
			}
			else
			{
				obj = base.JsonReader.ReadAsUntypedOrNullValue();
			}
			ODataJsonLightPropertyAndValueDeserializer.AddResourceProperty(resourceState, propertyName, obj);
			return null;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0004D294 File Offset: 0x0004B494
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

		// Token: 0x06001A0E RID: 6670 RVA: 0x0004D320 File Offset: 0x0004B520
		protected static ODataJsonLightReaderNestedResourceInfo ReadNonExpandedResourceSetNestedResourceInfo(IODataJsonLightReaderResourceState resourceState, IEdmStructuralProperty collectionProperty, IEdmStructuredType nestedResourceType, string propertyName)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = propertyName,
				IsCollection = new bool?(true),
				IsComplex = true
			};
			ODataResourceSet odataResourceSet = ODataJsonLightPropertyAndValueDeserializer.CreateCollectionResourceSet(resourceState, propertyName);
			return ODataJsonLightReaderNestedResourceInfo.CreateResourceSetReaderNestedResourceInfo(odataNestedResourceInfo, collectionProperty, nestedResourceType, odataResourceSet);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0004D360 File Offset: 0x0004B560
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

		// Token: 0x06001A10 RID: 6672 RVA: 0x0004D3B8 File Offset: 0x0004B5B8
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

		// Token: 0x06001A11 RID: 6673 RVA: 0x0004D4E4 File Offset: 0x0004B6E4
		protected static ODataJsonLightReaderNestedResourceInfo ReadExpandedResourceSetNestedResourceInfo(IODataJsonLightReaderResourceState resourceState, IEdmNavigationProperty navigationProperty, IEdmStructuredType propertyType, string propertyName, bool isDeltaResourceSet)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = propertyName,
				IsCollection = new bool?(true)
			};
			ODataResourceSetBase odataResourceSetBase;
			if (isDeltaResourceSet)
			{
				odataResourceSetBase = new ODataDeltaResourceSet();
			}
			else
			{
				odataResourceSetBase = new ODataResourceSet();
			}
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
						odataResourceSetBase.Count = (long?)keyValuePair.Value;
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
								odataResourceSetBase.NextPageLink = (Uri)keyValuePair.Value;
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
							odataResourceSetBase.TypeName = (string)keyValuePair.Value;
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
			return ODataJsonLightReaderNestedResourceInfo.CreateResourceSetReaderNestedResourceInfo(odataNestedResourceInfo, navigationProperty, propertyType, odataResourceSetBase);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0004D708 File Offset: 0x0004B908
		protected static ODataJsonLightReaderNestedResourceInfo ReadStreamCollectionNestedResourceInfo(IODataJsonLightReaderResourceState resourceState, IEdmStructuralProperty collectionProperty, string propertyName, IEdmType elementType)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = propertyName,
				IsCollection = new bool?(true),
				IsComplex = false
			};
			ODataResourceSet odataResourceSet = ODataJsonLightPropertyAndValueDeserializer.CreateCollectionResourceSet(resourceState, propertyName);
			return ODataJsonLightReaderNestedResourceInfo.CreateResourceSetReaderNestedResourceInfo(odataNestedResourceInfo, collectionProperty, elementType, odataResourceSet);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0004D748 File Offset: 0x0004B948
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

		// Token: 0x06001A14 RID: 6676 RVA: 0x0004D848 File Offset: 0x0004BA48
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

		// Token: 0x06001A15 RID: 6677 RVA: 0x0004D928 File Offset: 0x0004BB28
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
			if (text != null)
			{
				DerivedTypeValidator derivedTypeValidator = propertyAndAnnotationCollector.GetDerivedTypeValidator(propertyName);
				if (derivedTypeValidator != null)
				{
					derivedTypeValidator.ValidateResourceType(text);
				}
			}
			return text;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0004D9A8 File Offset: 0x0004BBA8
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
			ODataResourceBase resource = resourceState.Resource;
			resource.Properties = resource.Properties.ConcatToReadOnlyEnumerable("Properties", odataProperty);
			return odataProperty;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0004DA68 File Offset: 0x0004BC68
		protected static void AttachODataAnnotations(IODataJsonLightReaderResourceState resourceState, string propertyName, ODataProperty property)
		{
			foreach (KeyValuePair<string, object> keyValuePair in ((propertyName.Length == 0) ? resourceState.PropertyAndAnnotationCollector.GetODataScopeAnnotation() : resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(propertyName)))
			{
				if (string.Equals(keyValuePair.Key, "odata.type", StringComparison.Ordinal) || string.Equals(keyValuePair.Key, "@type", StringComparison.Ordinal))
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

		// Token: 0x06001A18 RID: 6680 RVA: 0x0004DB64 File Offset: 0x0004BD64
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

		// Token: 0x06001A19 RID: 6681 RVA: 0x0004DB84 File Offset: 0x0004BD84
		protected string ReadODataTypeAnnotationValue()
		{
			string text = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName(base.JsonReader.ReadStringValue()));
			if (text == null)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(text));
			}
			return text;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0004DBB8 File Offset: 0x0004BDB8
		protected object ReadTypePropertyAnnotationValue(string propertyAnnotationName)
		{
			string text;
			if (this.TryReadODataTypeAnnotationValue(propertyAnnotationName, out text))
			{
				return text;
			}
			throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyAnnotationName));
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0004DBE0 File Offset: 0x0004BDE0
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

		// Token: 0x06001A1C RID: 6684 RVA: 0x0004DC08 File Offset: 0x0004BE08
		private static ODataResourceSet CreateCollectionResourceSet(IODataJsonLightReaderResourceState resourceState, string propertyName)
		{
			ODataResourceSet odataResourceSet = new ODataResourceSet();
			foreach (KeyValuePair<string, object> keyValuePair in resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(propertyName))
			{
				string key = keyValuePair.Key;
				if (!(key == "odata.nextLink"))
				{
					if (!(key == "odata.count"))
					{
						if (!(key == "odata.type"))
						{
							throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedComplexCollectionPropertyAnnotation(propertyName, keyValuePair.Key));
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
			return odataResourceSet;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0004DCE4 File Offset: 0x0004BEE4
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

		// Token: 0x06001A1E RID: 6686 RVA: 0x0004DD34 File Offset: 0x0004BF34
		private ODataProperty ReadTopLevelPropertyImplementation(IEdmTypeReference expectedPropertyTypeReference, PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			expectedPropertyTypeReference = this.UpdateExpectedTypeBasedOnContextUri(expectedPropertyTypeReference);
			object propertyValue = ODataJsonLightPropertyAndValueDeserializer.missingPropertyValue;
			Collection<ODataInstanceAnnotation> customInstanceAnnotations = new Collection<ODataInstanceAnnotation>();
			if (this.IsTopLevel6xNullValue())
			{
				this.ReaderValidator.ValidateNullValue(expectedPropertyTypeReference, true, null, null);
				this.ValidateNoPropertyInNullPayload(propertyAndAnnotationCollector);
				propertyValue = null;
			}
			else
			{
				string payloadTypeName = null;
				if (this.ReadingResourceProperty(propertyAndAnnotationCollector, expectedPropertyTypeReference, out payloadTypeName))
				{
					propertyValue = this.ReadNonEntityValue(payloadTypeName, expectedPropertyTypeReference, propertyAndAnnotationCollector, null, true, true, true, null, null);
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
							if (this.JsonReader.NodeType == JsonNodeType.Property)
							{
								this.JsonReader.Read();
							}
							switch (propertyParsingResult)
							{
							case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
								return;
							case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
								if (string.CompareOrdinal("value", propertyName) == 0)
								{
									propertyValue = this.ReadNonEntityValue(payloadTypeName, expectedPropertyTypeReference, null, null, true, true, false, propertyName, null);
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

		// Token: 0x06001A1F RID: 6687 RVA: 0x0004DF08 File Offset: 0x0004C108
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

		// Token: 0x06001A20 RID: 6688 RVA: 0x0004DF84 File Offset: 0x0004C184
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
				object obj = this.ReadNonEntityValueImplementation(null, edmTypeReference, propertyAndAnnotationCollector, collectionWithoutExpectedTypeValidator, true, false, false, null, null);
				ValidationUtils.ValidateCollectionItem(obj, edmTypeReference.IsNullable());
				list.Add(obj);
			}
			base.JsonReader.ReadEndArray();
			odataCollectionValue.Items = new ReadOnlyEnumerable<object>(list);
			this.DecreaseRecursionDepth();
			return odataCollectionValue;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0004E044 File Offset: 0x0004C244
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

		// Token: 0x06001A22 RID: 6690 RVA: 0x0004E09C File Offset: 0x0004C29C
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

		// Token: 0x06001A23 RID: 6691 RVA: 0x0004E174 File Offset: 0x0004C374
		private object ReadEnumValue(bool insideJsonObjectValue, IEdmEnumTypeReference expectedValueTypeReference, bool validateNullValue, string propertyName)
		{
			if (insideJsonObjectValue)
			{
				throw new ODataException(Strings.JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName(JsonNodeType.PrimitiveValue, JsonNodeType.StartObject, propertyName));
			}
			string text = base.JsonReader.ReadStringValue();
			return new ODataEnumValue(text, expectedValueTypeReference.FullName());
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0004E1B8 File Offset: 0x0004C3B8
		private ODataResourceValue ReadResourceValue(bool insideJsonObjectValue, bool insideResourceValue, string propertyName, IEdmStructuredTypeReference structuredTypeReference, string payloadTypeName, PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			if (!insideJsonObjectValue && !insideResourceValue)
			{
				if (base.JsonReader.NodeType != JsonNodeType.StartObject)
				{
					string text = ((structuredTypeReference != null) ? structuredTypeReference.FullName() : payloadTypeName);
					throw new ODataException(string.Format(CultureInfo.InvariantCulture, "The property with name '{0}' was found with a value node of type '{1}'; however, a resource value of type '{2}' was expected.", new object[]
					{
						propertyName,
						base.JsonReader.NodeType,
						text
					}));
				}
				base.JsonReader.Read();
			}
			return this.ReadResourceValue(structuredTypeReference, payloadTypeName, propertyAndAnnotationCollector);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0004E238 File Offset: 0x0004C438
		private ODataResourceValue ReadResourceValue(IEdmStructuredTypeReference structuredTypeReference, string payloadTypeName, PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			this.IncreaseRecursionDepth();
			ODataResourceValue resourceValue = new ODataResourceValue();
			resourceValue.TypeName = ((structuredTypeReference != null) ? structuredTypeReference.FullName() : payloadTypeName);
			if (structuredTypeReference != null)
			{
				resourceValue.TypeAnnotation = new ODataTypeAnnotation(resourceValue.TypeName);
			}
			List<ODataProperty> properties = new List<ODataProperty>();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ReadPropertyCustomAnnotationValue = new Func<PropertyAndAnnotationCollector, string, object>(this.ReadCustomInstanceAnnotationValue);
				base.ProcessProperty(propertyAndAnnotationCollector, new Func<string, object>(this.ReadTypePropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					if (this.JsonReader.NodeType == JsonNodeType.Property)
					{
						this.JsonReader.Read();
					}
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						break;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
					{
						ODataProperty odataProperty = new ODataProperty();
						odataProperty.Name = propertyName;
						IEdmProperty edmProperty = null;
						if (structuredTypeReference != null)
						{
							edmProperty = ReaderValidationUtils.ValidatePropertyDefined(propertyName, structuredTypeReference.StructuredDefinition(), this.MessageReaderSettings.ThrowOnUndeclaredPropertyForNonOpenType);
						}
						ODataNullValueBehaviorKind odataNullValueBehaviorKind = ((this.ReadingResponse || edmProperty == null) ? ODataNullValueBehaviorKind.Default : this.Model.NullValueReadBehaviorKind(edmProperty));
						object obj = this.ReadNonEntityValueImplementation(ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(propertyAndAnnotationCollector, propertyName), (edmProperty == null) ? null : edmProperty.Type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, false, false, propertyName, new bool?(edmProperty == null));
						if (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null)
						{
							propertyAndAnnotationCollector.CheckForDuplicatePropertyNames(odataProperty);
							odataProperty.Value = obj;
							IEnumerable<KeyValuePair<string, object>> customPropertyAnnotations = propertyAndAnnotationCollector.GetCustomPropertyAnnotations(propertyName);
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
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ResourceValuePropertyAnnotationWithoutProperty(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						if (string.CompareOrdinal("odata.type", propertyName) == 0)
						{
							throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ResourceTypeAnnotationNotFirst);
						}
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
					{
						ODataAnnotationNames.ValidateIsCustomAnnotationName(propertyName);
						object obj2 = this.ReadCustomInstanceAnnotationValue(propertyAndAnnotationCollector, propertyName);
						resourceValue.InstanceAnnotations.Add(new ODataInstanceAnnotation(propertyName, obj2.ToODataValue()));
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
			resourceValue.Properties = new ReadOnlyEnumerable<ODataProperty>(properties);
			this.DecreaseRecursionDepth();
			return resourceValue;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0004E334 File Offset: 0x0004C534
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "No easy way to refactor.")]
		private object ReadNonEntityValueImplementation(string payloadTypeName, IEdmTypeReference expectedTypeReference, PropertyAndAnnotationCollector propertyAndAnnotationCollector, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool isTopLevelPropertyValue, bool insideResourceValue, string propertyName, bool? isDynamicProperty = null)
		{
			bool flag = base.JsonReader.NodeType == JsonNodeType.StartObject;
			bool flag2 = false;
			if (flag || insideResourceValue)
			{
				if (propertyAndAnnotationCollector == null)
				{
					propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
				}
				else
				{
					propertyAndAnnotationCollector.Reset();
				}
				if (!insideResourceValue)
				{
					string text;
					flag2 = this.TryReadPayloadTypeFromObject(propertyAndAnnotationCollector, insideResourceValue, out text);
					if (flag2)
					{
						payloadTypeName = text;
					}
				}
			}
			EdmTypeKind edmTypeKind;
			ODataTypeAnnotation odataTypeAnnotation;
			IEdmTypeReference edmTypeReference = this.ReaderValidator.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, null, null, expectedTypeReference, payloadTypeName, base.Model, new Func<EdmTypeKind>(this.GetNonEntityValueKind), out edmTypeKind, out odataTypeAnnotation);
			if (edmTypeKind == EdmTypeKind.Untyped || edmTypeKind == EdmTypeKind.None)
			{
				edmTypeReference = ODataJsonLightPropertyAndValueDeserializer.ResolveUntypedType(base.JsonReader.NodeType, base.JsonReader.Value, payloadTypeName, expectedTypeReference, base.MessageReaderSettings.PrimitiveTypeResolver, base.MessageReaderSettings.ReadUntypedAsString, !base.MessageReaderSettings.ThrowIfTypeConflictsWithMetadata);
				edmTypeKind = edmTypeReference.TypeKind();
			}
			object obj;
			if (ODataJsonReaderCoreUtils.TryReadNullValue(base.JsonReader, base.JsonLightInputContext, edmTypeReference, validateNullValue, propertyName, isDynamicProperty))
			{
				if (base.JsonLightInputContext.MessageReaderSettings.ThrowIfTypeConflictsWithMetadata && validateNullValue && edmTypeReference != null && !edmTypeReference.IsNullable && (edmTypeKind != EdmTypeKind.Collection || isDynamicProperty != true))
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
					goto IL_0256;
				}
				case EdmTypeKind.Entity:
				case EdmTypeKind.Complex:
				{
					IEdmStructuredTypeReference edmStructuredTypeReference = ((edmTypeReference == null) ? null : edmTypeReference.AsStructured());
					obj = this.ReadResourceValue(flag, insideResourceValue, propertyName, edmStructuredTypeReference, payloadTypeName, propertyAndAnnotationCollector);
					goto IL_0256;
				}
				case EdmTypeKind.Collection:
				{
					IEdmCollectionTypeReference edmCollectionTypeReference = ValidationUtils.ValidateCollectionType(edmTypeReference);
					if (flag)
					{
						throw new ODataException(Strings.JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName(JsonNodeType.StartArray, JsonNodeType.StartObject, propertyName));
					}
					obj = this.ReadCollectionValue(edmCollectionTypeReference, payloadTypeName, odataTypeAnnotation);
					goto IL_0256;
				}
				case EdmTypeKind.Enum:
				{
					IEdmEnumTypeReference edmEnumTypeReference = ((edmTypeReference == null) ? null : edmTypeReference.AsEnum());
					obj = this.ReadEnumValue(flag, edmEnumTypeReference, validateNullValue, propertyName);
					goto IL_0256;
				}
				case EdmTypeKind.TypeDefinition:
					obj = this.ReadTypeDefinitionValue(flag, expectedTypeReference.AsTypeDefinition(), validateNullValue, propertyName);
					goto IL_0256;
				case EdmTypeKind.Untyped:
					obj = base.JsonReader.ReadAsUntypedOrNullValue();
					goto IL_0256;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightPropertyAndValueDeserializer_ReadPropertyValue));
				IL_0256:
				if (collectionValidator != null)
				{
					string payloadTypeName2 = ODataJsonLightReaderUtils.GetPayloadTypeName(obj);
					collectionValidator.ValidateCollectionItem(payloadTypeName2, edmTypeKind);
				}
			}
			return obj;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0004E5B0 File Offset: 0x0004C7B0
		private bool TryReadPayloadTypeFromObject(PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool insideResourceValue, out string payloadTypeName)
		{
			bool flag = false;
			payloadTypeName = null;
			if (!insideResourceValue)
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

		// Token: 0x06001A28 RID: 6696 RVA: 0x0004E5F8 File Offset: 0x0004C7F8
		private bool ReadingResourceProperty(PropertyAndAnnotationCollector propertyAndAnnotationCollector, IEdmTypeReference expectedPropertyTypeReference, out string payloadTypeName)
		{
			payloadTypeName = null;
			bool flag = false;
			if (expectedPropertyTypeReference != null)
			{
				flag = expectedPropertyTypeReference.IsStructured();
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
					flag = edmType2.IsODataComplexTypeKind() || edmType2.IsODataEntityTypeKind();
				}
			}
			return flag;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0004E678 File Offset: 0x0004C878
		private bool IsTopLevel6xNullValue()
		{
			bool flag = base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("@odata.null", base.JsonReader.GetPropertyName()) == 0;
			if (flag)
			{
				base.JsonReader.ReadNext();
				object obj = base.JsonReader.ReadPrimitiveValue();
				if (!(obj is bool) || !(bool)obj)
				{
					throw new ODataException(Strings.ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation("odata.null", "true"));
				}
			}
			return flag;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
		private void ValidateNoPropertyInNullPayload(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			Func<string, object> func = delegate(string annotationName)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation(annotationName));
			};
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(propertyAndAnnotationCollector, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					if (base.JsonReader.NodeType == JsonNodeType.Property)
					{
						base.JsonReader.Read();
					}
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

		// Token: 0x06001A2B RID: 6699 RVA: 0x0004E741 File Offset: 0x0004C941
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0004E75E File Offset: 0x0004C95E
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0000239D File Offset: 0x0000059D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This is needed in DEBUG build.")]
		private void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x04000B4D RID: 2893
		private static readonly object missingPropertyValue = new object();

		// Token: 0x04000B4E RID: 2894
		private int recursionDepth;
	}
}
