using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000219 RID: 537
	internal sealed class ODataJsonLightServiceDocumentDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x060015DD RID: 5597 RVA: 0x0003E5F4 File Offset: 0x0003C7F4
		internal ODataJsonLightServiceDocumentDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00042C94 File Offset: 0x00040E94
		internal ODataServiceDocument ReadServiceDocument()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			base.ReadPayloadStart(ODataPayloadKind.ServiceDocument, propertyAndAnnotationCollector, false, false);
			ODataServiceDocument odataServiceDocument = this.ReadServiceDocumentImplementation(propertyAndAnnotationCollector);
			base.ReadPayloadEnd(false);
			return odataServiceDocument;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00042CC4 File Offset: 0x00040EC4
		private ODataServiceDocument ReadServiceDocumentImplementation(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			List<ODataServiceDocumentElement>[] serviceDocumentElements = new List<ODataServiceDocumentElement>[1];
			Action<ODataJsonLightDeserializer.PropertyParsingResult, string> <>9__1;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				Func<string, object> func = delegate(string annotationName)
				{
					throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument(annotationName, "value"));
				};
				Func<string, object> func2 = func;
				Action<ODataJsonLightDeserializer.PropertyParsingResult, string> action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
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
							if (serviceDocumentElements[0] != null)
							{
								throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument("value"));
							}
							serviceDocumentElements[0] = new List<ODataServiceDocumentElement>();
							this.JsonReader.ReadStartArray();
							PropertyAndAnnotationCollector propertyAndAnnotationCollector2 = this.CreatePropertyAndAnnotationCollector();
							while (this.JsonReader.NodeType != JsonNodeType.EndArray)
							{
								ODataServiceDocumentElement odataServiceDocumentElement = this.ReadServiceDocumentElement(propertyAndAnnotationCollector2);
								if (odataServiceDocumentElement != null)
								{
									serviceDocumentElements[0].Add(odataServiceDocumentElement);
								}
								propertyAndAnnotationCollector2.Reset();
							}
							this.JsonReader.ReadEndArray();
							return;
						}
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
							throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty(propertyName));
						case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
							throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument(propertyName, "value"));
						case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
							this.JsonReader.SkipValue();
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
							throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
						default:
							return;
						}
					});
				}
				base.ProcessProperty(propertyAndAnnotationCollector, func2, action);
			}
			if (serviceDocumentElements[0] == null)
			{
				throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument("value"));
			}
			base.JsonReader.ReadEndObject();
			return new ODataServiceDocument
			{
				EntitySets = new ReadOnlyEnumerable<ODataEntitySetInfo>(Enumerable.ToList<ODataEntitySetInfo>(Enumerable.OfType<ODataEntitySetInfo>(serviceDocumentElements[0]))),
				FunctionImports = new ReadOnlyEnumerable<ODataFunctionImportInfo>(Enumerable.ToList<ODataFunctionImportInfo>(Enumerable.OfType<ODataFunctionImportInfo>(serviceDocumentElements[0]))),
				Singletons = new ReadOnlyEnumerable<ODataSingletonInfo>(Enumerable.ToList<ODataSingletonInfo>(Enumerable.OfType<ODataSingletonInfo>(serviceDocumentElements[0])))
			};
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00042DC4 File Offset: 0x00040FC4
		private ODataServiceDocumentElement ReadServiceDocumentElement(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			base.JsonReader.ReadStartObject();
			string[] name = new string[1];
			string[] url = new string[1];
			string[] kind = new string[1];
			string[] title = new string[1];
			Action<ODataJsonLightDeserializer.PropertyParsingResult, string> <>9__1;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				Func<string, object> func = delegate(string annotationName)
				{
					throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocumentElement(annotationName));
				};
				Func<string, object> func2 = func;
				Action<ODataJsonLightDeserializer.PropertyParsingResult, string> action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
					{
						switch (propertyParsingResult)
						{
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
							if (string.CompareOrdinal("name", propertyName) == 0)
							{
								if (name[0] != null)
								{
									throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement("name"));
								}
								name[0] = this.JsonReader.ReadStringValue();
								return;
							}
							else if (string.CompareOrdinal("url", propertyName) == 0)
							{
								if (url[0] != null)
								{
									throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement("url"));
								}
								url[0] = this.JsonReader.ReadStringValue();
								ValidationUtils.ValidateServiceDocumentElementUrl(url[0]);
								return;
							}
							else if (string.CompareOrdinal("kind", propertyName) == 0)
							{
								if (kind[0] != null)
								{
									throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement("kind"));
								}
								kind[0] = this.JsonReader.ReadStringValue();
								return;
							}
							else
							{
								if (string.CompareOrdinal("title", propertyName) != 0)
								{
									throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocumentElement(propertyName, "name", "url"));
								}
								if (title[0] != null)
								{
									throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement("title"));
								}
								title[0] = this.JsonReader.ReadStringValue();
								return;
							}
							break;
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
							throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty(propertyName));
						case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
							throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocumentElement(propertyName));
						case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
							this.JsonReader.SkipValue();
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
							throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
						default:
							return;
						}
					});
				}
				base.ProcessProperty(propertyAndAnnotationCollector, func2, action);
			}
			if (string.IsNullOrEmpty(name[0]))
			{
				throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement("name"));
			}
			if (string.IsNullOrEmpty(url[0]))
			{
				throw new ODataException(Strings.ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement("url"));
			}
			ODataServiceDocumentElement odataServiceDocumentElement = null;
			if (kind[0] != null)
			{
				if (kind[0].Equals("EntitySet", 4))
				{
					odataServiceDocumentElement = new ODataEntitySetInfo();
				}
				else if (kind[0].Equals("FunctionImport", 4))
				{
					odataServiceDocumentElement = new ODataFunctionImportInfo();
				}
				else if (kind[0].Equals("Singleton", 4))
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
				odataServiceDocumentElement.Url = base.ProcessUriFromPayload(url[0]);
				odataServiceDocumentElement.Name = name[0];
				odataServiceDocumentElement.Title = title[0];
			}
			base.JsonReader.ReadEndObject();
			return odataServiceDocumentElement;
		}
	}
}
