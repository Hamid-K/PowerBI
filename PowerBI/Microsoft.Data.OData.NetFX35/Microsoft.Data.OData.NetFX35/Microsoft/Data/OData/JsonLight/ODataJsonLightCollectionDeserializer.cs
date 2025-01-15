using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000182 RID: 386
	internal sealed class ODataJsonLightCollectionDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x0002378B File Offset: 0x0002198B
		internal ODataJsonLightCollectionDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002391C File Offset: 0x00021B1C
		internal ODataCollectionStart ReadCollectionStart(DuplicatePropertyNamesChecker collectionStartDuplicatePropertyNamesChecker, bool isReadingNestedPayload, IEdmTypeReference expectedItemTypeReference, out IEdmTypeReference actualItemTypeReference)
		{
			actualItemTypeReference = expectedItemTypeReference;
			ODataCollectionStart collectionStart = null;
			if (isReadingNestedPayload)
			{
				collectionStart = new ODataCollectionStart
				{
					Name = null
				};
			}
			else
			{
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					IEdmTypeReference actualItemTypeRef = expectedItemTypeReference;
					base.ProcessProperty(collectionStartDuplicatePropertyNamesChecker, new Func<string, object>(base.ReadTypePropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
					{
						switch (propertyParsingResult)
						{
						case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						{
							if (string.CompareOrdinal("value", propertyName) != 0)
							{
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName(propertyName, "value"));
							}
							string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(collectionStartDuplicatePropertyNamesChecker, propertyName);
							if (text != null)
							{
								string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(text);
								if (collectionItemTypeName == null)
								{
									throw new ODataException(Strings.ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName(text));
								}
								Func<EdmTypeKind> func = delegate
								{
									throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightCollectionDeserializer_ReadCollectionStart_TypeKindFromPayloadFunc));
								};
								EdmTypeKind edmTypeKind;
								SerializationTypeNameAnnotation serializationTypeNameAnnotation;
								actualItemTypeRef = ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, null, expectedItemTypeReference, collectionItemTypeName, this.Model, this.MessageReaderSettings, this.Version, func, out edmTypeKind, out serializationTypeNameAnnotation);
							}
							collectionStart = new ODataCollectionStart
							{
								Name = null
							};
							return;
						}
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
							throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty(propertyName));
						case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
							throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
						case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
							this.JsonReader.SkipValue();
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
							throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
						default:
							throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightCollectionDeserializer_ReadCollectionStart));
						}
					});
					actualItemTypeReference = actualItemTypeRef;
				}
				if (collectionStart == null)
				{
					throw new ODataException(Strings.ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound("value"));
				}
			}
			if (base.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart(base.JsonReader.NodeType));
			}
			return collectionStart;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00023A08 File Offset: 0x00021C08
		internal object ReadCollectionItem(IEdmTypeReference expectedItemTypeReference, CollectionWithoutExpectedTypeValidator collectionValidator)
		{
			return base.ReadNonEntityValue(null, expectedItemTypeReference, this.duplicatePropertyNamesChecker, collectionValidator, true, false, false, null);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00023A88 File Offset: 0x00021C88
		internal void ReadCollectionEnd(bool isReadingNestedPayload)
		{
			base.JsonReader.ReadEndArray();
			if (!isReadingNestedPayload)
			{
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					base.ProcessProperty(duplicatePropertyNamesChecker, new Func<string, object>(base.ReadTypePropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
					{
						switch (propertyParsingResult)
						{
						case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
							throw new ODataException(Strings.ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd(propertyName));
						case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
							base.JsonReader.SkipValue();
							return;
						default:
							throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightCollectionDeserializer_ReadCollectionEnd));
						}
					});
				}
				base.JsonReader.ReadEndObject();
			}
		}

		// Token: 0x04000401 RID: 1025
		private readonly DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
	}
}
