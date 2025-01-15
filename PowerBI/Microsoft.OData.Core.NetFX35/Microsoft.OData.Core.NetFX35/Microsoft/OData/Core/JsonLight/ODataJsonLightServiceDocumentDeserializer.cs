using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.Json;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000FB RID: 251
	internal sealed class ODataJsonLightServiceDocumentDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x000228FB File Offset: 0x00020AFB
		internal ODataJsonLightServiceDocumentDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00022904 File Offset: 0x00020B04
		internal ODataServiceDocument ReadServiceDocument()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.ReadPayloadStart(ODataPayloadKind.ServiceDocument, duplicatePropertyNamesChecker, false, false);
			ODataServiceDocument odataServiceDocument = this.ReadServiceDocumentImplementation(duplicatePropertyNamesChecker);
			base.ReadPayloadEnd(false);
			return odataServiceDocument;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00022A64 File Offset: 0x00020C64
		private ODataServiceDocument ReadServiceDocumentImplementation(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClass5 CS$<>8__locals1 = new ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClass5();
			CS$<>8__locals1.<>4__this = this;
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClass5 CS$<>8__locals2 = CS$<>8__locals1;
			List<ODataServiceDocumentElement>[] array = new List<ODataServiceDocumentElement>[1];
			CS$<>8__locals2.serviceDocumentElements = array;
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
						if (CS$<>8__locals1.serviceDocumentElements[0] != null)
						{
							throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument("value"));
						}
						CS$<>8__locals1.serviceDocumentElements[0] = new List<ODataServiceDocumentElement>();
						CS$<>8__locals1.<>4__this.JsonReader.ReadStartArray();
						DuplicatePropertyNamesChecker duplicatePropertyNamesChecker2 = CS$<>8__locals1.<>4__this.CreateDuplicatePropertyNamesChecker();
						while (CS$<>8__locals1.<>4__this.JsonReader.NodeType != JsonNodeType.EndArray)
						{
							ODataServiceDocumentElement odataServiceDocumentElement = CS$<>8__locals1.<>4__this.ReadServiceDocumentElement(duplicatePropertyNamesChecker2);
							if (odataServiceDocumentElement != null)
							{
								CS$<>8__locals1.serviceDocumentElements[0].Add(odataServiceDocumentElement);
							}
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
			if (CS$<>8__locals1.serviceDocumentElements[0] == null)
			{
				throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument("value"));
			}
			base.JsonReader.ReadEndObject();
			return new ODataServiceDocument
			{
				EntitySets = new ReadOnlyEnumerable<ODataEntitySetInfo>(Enumerable.ToList<ODataEntitySetInfo>(Enumerable.OfType<ODataEntitySetInfo>(CS$<>8__locals1.serviceDocumentElements[0]))),
				FunctionImports = new ReadOnlyEnumerable<ODataFunctionImportInfo>(Enumerable.ToList<ODataFunctionImportInfo>(Enumerable.OfType<ODataFunctionImportInfo>(CS$<>8__locals1.serviceDocumentElements[0]))),
				Singletons = new ReadOnlyEnumerable<ODataSingletonInfo>(Enumerable.ToList<ODataSingletonInfo>(Enumerable.OfType<ODataSingletonInfo>(CS$<>8__locals1.serviceDocumentElements[0])))
			};
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00022CF4 File Offset: 0x00020EF4
		private ODataServiceDocumentElement ReadServiceDocumentElement(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClassb CS$<>8__locals1 = new ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClassb();
			CS$<>8__locals1.<>4__this = this;
			base.JsonReader.ReadStartObject();
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClassb CS$<>8__locals2 = CS$<>8__locals1;
			string[] array = new string[1];
			CS$<>8__locals2.name = array;
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClassb CS$<>8__locals3 = CS$<>8__locals1;
			string[] array2 = new string[1];
			CS$<>8__locals3.url = array2;
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClassb CS$<>8__locals4 = CS$<>8__locals1;
			string[] array3 = new string[1];
			CS$<>8__locals4.kind = array3;
			ODataJsonLightServiceDocumentDeserializer.<>c__DisplayClassb CS$<>8__locals5 = CS$<>8__locals1;
			string[] array4 = new string[1];
			CS$<>8__locals5.title = array4;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				Func<string, object> func = delegate(string annotationName)
				{
					throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocumentElement(annotationName));
				};
				base.ProcessProperty(duplicatePropertyNamesChecker, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						if (string.CompareOrdinal("name", propertyName) == 0)
						{
							if (CS$<>8__locals1.name[0] != null)
							{
								throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement("name"));
							}
							CS$<>8__locals1.name[0] = CS$<>8__locals1.<>4__this.JsonReader.ReadStringValue();
							return;
						}
						else if (string.CompareOrdinal("url", propertyName) == 0)
						{
							if (CS$<>8__locals1.url[0] != null)
							{
								throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement("url"));
							}
							CS$<>8__locals1.url[0] = CS$<>8__locals1.<>4__this.JsonReader.ReadStringValue();
							ValidationUtils.ValidateServiceDocumentElementUrl(CS$<>8__locals1.url[0]);
							return;
						}
						else if (string.CompareOrdinal("kind", propertyName) == 0)
						{
							if (CS$<>8__locals1.kind[0] != null)
							{
								throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement("kind"));
							}
							CS$<>8__locals1.kind[0] = CS$<>8__locals1.<>4__this.JsonReader.ReadStringValue();
							return;
						}
						else
						{
							if (string.CompareOrdinal("title", propertyName) != 0)
							{
								throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocumentElement(propertyName, "name", "url"));
							}
							if (CS$<>8__locals1.title[0] != null)
							{
								throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement("title"));
							}
							CS$<>8__locals1.title[0] = CS$<>8__locals1.<>4__this.JsonReader.ReadStringValue();
							return;
						}
						break;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocumentElement(propertyName));
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
			if (string.IsNullOrEmpty(CS$<>8__locals1.name[0]))
			{
				throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement("name"));
			}
			if (string.IsNullOrEmpty(CS$<>8__locals1.url[0]))
			{
				throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement("url"));
			}
			ODataServiceDocumentElement odataServiceDocumentElement = null;
			if (CS$<>8__locals1.kind[0] != null)
			{
				if (CS$<>8__locals1.kind[0].Equals("EntitySet", 4))
				{
					odataServiceDocumentElement = new ODataEntitySetInfo();
				}
				else if (CS$<>8__locals1.kind[0].Equals("FunctionImport", 4))
				{
					odataServiceDocumentElement = new ODataFunctionImportInfo();
				}
				else if (CS$<>8__locals1.kind[0].Equals("Singleton", 4))
				{
					odataServiceDocumentElement = new ODataSingletonInfo();
				}
			}
			else
			{
				odataServiceDocumentElement = new ODataEntitySetInfo();
			}
			if (odataServiceDocumentElement != null)
			{
				odataServiceDocumentElement.Url = base.ProcessUriFromPayload(CS$<>8__locals1.url[0]);
				odataServiceDocumentElement.Name = CS$<>8__locals1.name[0];
				odataServiceDocumentElement.Title = CS$<>8__locals1.title[0];
			}
			base.JsonReader.ReadEndObject();
			return odataServiceDocumentElement;
		}
	}
}
