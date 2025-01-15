using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x020001A2 RID: 418
	internal sealed class ODataJsonLightServiceDocumentDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x0002A43B File Offset: 0x0002863B
		internal ODataJsonLightServiceDocumentDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002A444 File Offset: 0x00028644
		internal ODataWorkspace ReadServiceDocument()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.ReadPayloadStart(ODataPayloadKind.ServiceDocument, duplicatePropertyNamesChecker, false, false);
			ODataWorkspace odataWorkspace = this.ReadServiceDocumentImplementation(duplicatePropertyNamesChecker);
			base.ReadPayloadEnd(false);
			return odataWorkspace;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002A5A0 File Offset: 0x000287A0
		private ODataWorkspace ReadServiceDocumentImplementation(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClass5 CS$<>8__locals1 = new ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClass5();
			CS$<>8__locals1.<>4__this = this;
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClass5 CS$<>8__locals2 = CS$<>8__locals1;
			List<ODataResourceCollectionInfo>[] array = new List<ODataResourceCollectionInfo>[1];
			CS$<>8__locals2.collections = array;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				Func<string, object> func = delegate(string annotationName)
				{
					throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument(annotationName, "value"));
				};
				base.ProcessProperty(duplicatePropertyNamesChecker, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
					{
						if (string.CompareOrdinal("value", propertyName) != 0)
						{
							throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument(propertyName, "value"));
						}
						if (CS$<>8__locals1.collections[0] != null)
						{
							throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument("value"));
						}
						CS$<>8__locals1.collections[0] = new List<ODataResourceCollectionInfo>();
						CS$<>8__locals1.<>4__this.JsonReader.ReadStartArray();
						DuplicatePropertyNamesChecker duplicatePropertyNamesChecker2 = CS$<>8__locals1.<>4__this.CreateDuplicatePropertyNamesChecker();
						while (CS$<>8__locals1.<>4__this.JsonReader.NodeType != JsonNodeType.EndArray)
						{
							ODataResourceCollectionInfo odataResourceCollectionInfo = CS$<>8__locals1.<>4__this.ReadResourceCollection(duplicatePropertyNamesChecker2);
							CS$<>8__locals1.collections[0].Add(odataResourceCollectionInfo);
							duplicatePropertyNamesChecker2.Clear();
						}
						CS$<>8__locals1.<>4__this.JsonReader.ReadEndArray();
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument(propertyName, "value"));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						CS$<>8__locals1.<>4__this.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						return;
					}
				});
			}
			if (CS$<>8__locals1.collections[0] == null)
			{
				throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument("value"));
			}
			base.JsonReader.ReadEndObject();
			return new ODataWorkspace
			{
				Collections = new ReadOnlyEnumerable<ODataResourceCollectionInfo>(CS$<>8__locals1.collections[0])
			};
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002A76C File Offset: 0x0002896C
		private ODataResourceCollectionInfo ReadResourceCollection(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClassc CS$<>8__locals1 = new ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClassc();
			CS$<>8__locals1.<>4__this = this;
			base.JsonReader.ReadStartObject();
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClassc CS$<>8__locals2 = CS$<>8__locals1;
			string[] array = new string[1];
			CS$<>8__locals2.entitySetName = array;
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClassc CS$<>8__locals3 = CS$<>8__locals1;
			string[] array2 = new string[1];
			CS$<>8__locals3.entitySetUrl = array2;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				Func<string, object> func = delegate(string annotationName)
				{
					throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInResourceCollection(annotationName));
				};
				base.ProcessProperty(duplicatePropertyNamesChecker, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						if (string.CompareOrdinal("name", propertyName) == 0)
						{
							if (CS$<>8__locals1.entitySetName[0] != null)
							{
								throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInResourceCollection("name"));
							}
							CS$<>8__locals1.entitySetName[0] = CS$<>8__locals1.<>4__this.JsonReader.ReadStringValue();
							return;
						}
						else
						{
							if (string.CompareOrdinal("url", propertyName) != 0)
							{
								throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInResourceCollection(propertyName, "name", "url"));
							}
							if (CS$<>8__locals1.entitySetUrl[0] != null)
							{
								throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInResourceCollection("url"));
							}
							CS$<>8__locals1.entitySetUrl[0] = CS$<>8__locals1.<>4__this.JsonReader.ReadStringValue();
							ValidationUtils.ValidateResourceCollectionInfoUrl(CS$<>8__locals1.entitySetUrl[0]);
							return;
						}
						break;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInResourceCollection(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						CS$<>8__locals1.<>4__this.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						return;
					}
				});
			}
			if (string.IsNullOrEmpty(CS$<>8__locals1.entitySetName[0]))
			{
				throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInResourceCollection("name"));
			}
			if (string.IsNullOrEmpty(CS$<>8__locals1.entitySetUrl[0]))
			{
				throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInResourceCollection("url"));
			}
			ODataResourceCollectionInfo odataResourceCollectionInfo = new ODataResourceCollectionInfo
			{
				Url = base.ProcessUriFromPayload(CS$<>8__locals1.entitySetUrl[0]),
				Name = CS$<>8__locals1.entitySetName[0]
			};
			base.JsonReader.ReadEndObject();
			return odataResourceCollectionInfo;
		}
	}
}
