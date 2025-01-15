using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001FF RID: 511
	internal sealed class ODataJsonLightCollectionDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x060013C9 RID: 5065 RVA: 0x00038926 File Offset: 0x00036B26
		internal ODataJsonLightCollectionDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0003893C File Offset: 0x00036B3C
		internal ODataCollectionStart ReadCollectionStart(PropertyAndAnnotationCollector collectionStartPropertyAndAnnotationCollector, bool isReadingNestedPayload, IEdmTypeReference expectedItemTypeReference, out IEdmTypeReference actualItemTypeReference)
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
					base.ProcessProperty(collectionStartPropertyAndAnnotationCollector, new Func<string, object>(base.ReadTypePropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
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
							string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(collectionStartPropertyAndAnnotationCollector, propertyName);
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
								ODataTypeAnnotation odataTypeAnnotation;
								actualItemTypeRef = this.ReaderValidator.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, default(bool?), null, expectedItemTypeReference, collectionItemTypeName, this.Model, func, out edmTypeKind, out odataTypeAnnotation);
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
							if (!ODataJsonLightCollectionDeserializer.IsValidODataAnnotationOfCollection(propertyName))
							{
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
							}
							this.JsonReader.SkipValue();
							return;
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

		// Token: 0x060013CB RID: 5067 RVA: 0x00038A30 File Offset: 0x00036C30
		internal object ReadCollectionItem(IEdmTypeReference expectedItemTypeReference, CollectionWithoutExpectedTypeValidator collectionValidator)
		{
			return base.ReadNonEntityValue(null, expectedItemTypeReference, this.propertyAndAnnotationCollector, collectionValidator, true, false, false, null, default(bool?));
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00038A5C File Offset: 0x00036C5C
		internal void ReadCollectionEnd(bool isReadingNestedPayload)
		{
			base.JsonReader.ReadEndArray();
			if (!isReadingNestedPayload)
			{
				PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					base.ProcessProperty(propertyAndAnnotationCollector, new Func<string, object>(base.ReadTypePropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
					{
						switch (propertyParsingResult)
						{
						case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
							throw new ODataException(Strings.ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd(propertyName));
						case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
							if (!ODataJsonLightCollectionDeserializer.IsValidODataAnnotationOfCollection(propertyName))
							{
								throw new ODataException(Strings.ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd(propertyName));
							}
							base.JsonReader.SkipValue();
							return;
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

		// Token: 0x060013CD RID: 5069 RVA: 0x00038AB8 File Offset: 0x00036CB8
		private static bool IsValidODataAnnotationOfCollection(string propertyName)
		{
			return string.CompareOrdinal("odata.count", propertyName) == 0 || string.CompareOrdinal("odata.nextLink", propertyName) == 0;
		}

		// Token: 0x040009FB RID: 2555
		private readonly PropertyAndAnnotationCollector propertyAndAnnotationCollector;
	}
}
