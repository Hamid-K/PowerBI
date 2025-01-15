using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000DD RID: 221
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to keep the logic together for better readability.")]
	internal sealed class ODataJsonLightEntryAndFeedDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x0001C686 File Offset: 0x0001A886
		internal ODataJsonLightEntryAndFeedDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001C690 File Offset: 0x0001A890
		internal void ReadFeedContentStart()
		{
			if (base.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadFeedContentStart(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartArray();
			if (base.JsonReader.NodeType != JsonNodeType.EndArray && base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_InvalidNodeTypeForItemsInFeed(base.JsonReader.NodeType));
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001C708 File Offset: 0x0001A908
		internal void ReadFeedContentEnd()
		{
			base.JsonReader.ReadEndArray();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001C718 File Offset: 0x0001A918
		internal void ReadEntryTypeName(IODataJsonLightReaderEntryState entryState)
		{
			if (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string propertyName = base.JsonReader.GetPropertyName();
				if (string.CompareOrdinal('@' + "odata.type", propertyName) == 0 || base.CompareSimplifiedODataAnnotation("@type", propertyName))
				{
					base.JsonReader.Read();
					entryState.Entry.TypeName = base.ReadODataTypeAnnotationValue();
				}
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001C850 File Offset: 0x0001AA50
		internal ODataJsonLightReaderNavigationLinkInfo ReadEntryContent(IODataJsonLightReaderEntryState entryState)
		{
			ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ReadPropertyCustomAnnotationValue = new Func<DuplicatePropertyNamesChecker, string, object>(base.ReadCustomInstanceAnnotationValue);
				base.ProcessProperty(entryState.DuplicatePropertyNamesChecker, new Func<string, object>(this.ReadEntryPropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						break;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						entryState.AnyPropertyFound = true;
						navigationLinkInfo = this.ReadEntryPropertyWithValue(entryState, propertyName);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						entryState.AnyPropertyFound = true;
						navigationLinkInfo = this.ReadEntryPropertyWithoutValue(entryState, propertyName);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
					{
						object obj = this.ReadEntryInstanceAnnotation(propertyName, entryState.AnyPropertyFound, true, entryState.DuplicatePropertyNamesChecker);
						this.ApplyEntryInstanceAnnotation(entryState, propertyName, obj);
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						this.ReadMetadataReferencePropertyValue(entryState, propertyName);
						break;
					default:
						return;
					}
				});
				if (navigationLinkInfo != null)
				{
					break;
				}
			}
			return navigationLinkInfo;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
		internal void ValidateEntryMetadata(IODataJsonLightReaderEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			if (entry != null)
			{
				IEdmEntityType entityType = entryState.EntityType;
				if (!base.ReadingResponse && entityType.HasStream && entry.MediaResource == null)
				{
					ODataStreamReferenceValue mediaResource = entry.MediaResource;
					ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
					this.SetEntryMediaResource(entryState, mediaResource);
				}
				bool flag = base.UseDefaultFormatBehavior || base.UseServerFormatBehavior;
				ValidationUtils.ValidateEntryMetadataResource(entry, entityType, base.Model, flag);
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001CAA8 File Offset: 0x0001ACA8
		internal void ReadTopLevelFeedAnnotations(ODataFeedBase feed, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool forFeedStart, bool readAllFeedProperties)
		{
			bool buffering = false;
			try
			{
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					bool foundValueProperty = false;
					if (!forFeedStart && readAllFeedProperties)
					{
						duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(true, base.JsonLightInputContext.ReadingResponse, !base.JsonLightInputContext.MessageReaderSettings.EnableFullValidation);
					}
					base.ProcessProperty(duplicatePropertyNamesChecker, new Func<string, object>(base.ReadTypePropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParseResult, string propertyName)
					{
						switch (propertyParseResult)
						{
						case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
							if (string.CompareOrdinal("value", propertyName) != 0)
							{
								throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyInTopLevelFeed(propertyName, "value"));
							}
							if (readAllFeedProperties)
							{
								this.JsonReader.StartBuffering();
								buffering = true;
								this.JsonReader.SkipValue();
								return;
							}
							foundValueProperty = true;
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyAnnotationInTopLevelFeed(propertyName));
						case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
							if (forFeedStart || !readAllFeedProperties)
							{
								this.ReadAndApplyFeedInstanceAnnotationValue(propertyName, feed, duplicatePropertyNamesChecker);
								return;
							}
							this.JsonReader.SkipValue();
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
							if (!(feed is ODataFeed))
							{
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
							}
							this.ReadMetadataReferencePropertyValue((ODataFeed)feed, propertyName);
							return;
						default:
							throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightEntryAndFeedDeserializer_ReadTopLevelFeedAnnotations));
						}
					});
					if (foundValueProperty)
					{
						return;
					}
				}
			}
			finally
			{
				if (buffering)
				{
					base.JsonReader.StopBuffering();
				}
			}
			if (forFeedStart && !readAllFeedProperties)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_ExpectedFeedPropertyNotFound("value"));
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		internal object ReadEntryPropertyAnnotationValue(string propertyAnnotationName)
		{
			string text;
			if (base.TryReadODataTypeAnnotationValue(propertyAnnotationName, out text))
			{
				return text;
			}
			if (propertyAnnotationName != null)
			{
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60007fc-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(11);
					dictionary.Add("odata.navigationLink", 0);
					dictionary.Add("odata.associationLink", 1);
					dictionary.Add("odata.nextLink", 2);
					dictionary.Add("odata.mediaEditLink", 3);
					dictionary.Add("odata.mediaReadLink", 4);
					dictionary.Add("odata.context", 5);
					dictionary.Add("odata.count", 6);
					dictionary.Add("odata.mediaEtag", 7);
					dictionary.Add("odata.mediaContentType", 8);
					dictionary.Add("odata.bind", 9);
					dictionary.Add("odata.deltaLink", 10);
					<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60007fc-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60007fc-1.TryGetValue(propertyAnnotationName, ref num))
				{
					switch (num)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
						return base.ReadAndValidateAnnotationStringValueAsUri(propertyAnnotationName);
					case 6:
						return base.ReadAndValidateAnnotationAsLongForIeee754Compatible(propertyAnnotationName);
					case 7:
					case 8:
						return base.ReadAndValidateAnnotationStringValue(propertyAnnotationName);
					case 9:
					{
						if (base.JsonReader.NodeType != JsonNodeType.StartArray)
						{
							return new ODataEntityReferenceLink
							{
								Url = base.ReadAndValidateAnnotationStringValueAsUri("odata.bind")
							};
						}
						LinkedList<ODataEntityReferenceLink> linkedList = new LinkedList<ODataEntityReferenceLink>();
						base.JsonReader.Read();
						while (base.JsonReader.NodeType != JsonNodeType.EndArray)
						{
							linkedList.AddLast(new ODataEntityReferenceLink
							{
								Url = base.ReadAndValidateAnnotationStringValueAsUri("odata.bind")
							});
						}
						base.JsonReader.Read();
						if (linkedList.Count == 0)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EmptyBindArray("odata.bind"));
						}
						return linkedList;
					}
					}
				}
			}
			throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyAnnotationName));
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001CD84 File Offset: 0x0001AF84
		internal object ReadEntryInstanceAnnotation(string annotationName, bool anyPropertyFound, bool typeAnnotationFound, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (annotationName != null)
			{
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60007fd-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(9);
					dictionary.Add("odata.type", 0);
					dictionary.Add("odata.id", 1);
					dictionary.Add("odata.etag", 2);
					dictionary.Add("odata.editLink", 3);
					dictionary.Add("odata.readLink", 4);
					dictionary.Add("odata.mediaEditLink", 5);
					dictionary.Add("odata.mediaReadLink", 6);
					dictionary.Add("odata.mediaContentType", 7);
					dictionary.Add("odata.mediaEtag", 8);
					<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60007fd-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60007fd-1.TryGetValue(annotationName, ref num))
				{
					switch (num)
					{
					case 0:
						if (!typeAnnotationFound)
						{
							return base.ReadODataTypeAnnotationValue();
						}
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EntryTypeAnnotationNotFirst);
					case 1:
						if (anyPropertyFound)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EntryInstanceAnnotationPrecededByProperty(annotationName));
						}
						return base.ReadAnnotationStringValueAsUri(annotationName);
					case 2:
						if (anyPropertyFound)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EntryInstanceAnnotationPrecededByProperty(annotationName));
						}
						return base.ReadAndValidateAnnotationStringValue(annotationName);
					case 3:
					case 4:
					case 5:
					case 6:
						return base.ReadAndValidateAnnotationStringValueAsUri(annotationName);
					case 7:
					case 8:
						return base.ReadAndValidateAnnotationStringValue(annotationName);
					}
				}
			}
			ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
			return base.ReadCustomInstanceAnnotationValue(duplicatePropertyNamesChecker, annotationName);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001CEBC File Offset: 0x0001B0BC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "The casts aren't actually being done multiple times, since they occur in different cases of the switch statement.")]
		internal void ApplyEntryInstanceAnnotation(IODataJsonLightReaderEntryState entryState, string annotationName, object annotationValue)
		{
			ODataEntry entry = entryState.Entry;
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			if (annotationName != null)
			{
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60007fe-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(9);
					dictionary.Add("odata.type", 0);
					dictionary.Add("odata.id", 1);
					dictionary.Add("odata.etag", 2);
					dictionary.Add("odata.editLink", 3);
					dictionary.Add("odata.readLink", 4);
					dictionary.Add("odata.mediaEditLink", 5);
					dictionary.Add("odata.mediaReadLink", 6);
					dictionary.Add("odata.mediaContentType", 7);
					dictionary.Add("odata.mediaEtag", 8);
					<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60007fe-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60007fe-1.TryGetValue(annotationName, ref num))
				{
					switch (num)
					{
					case 0:
						entry.TypeName = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName((string)annotationValue));
						goto IL_01B5;
					case 1:
						if (annotationValue == null)
						{
							entry.IsTransient = true;
							goto IL_01B5;
						}
						entry.Id = (Uri)annotationValue;
						goto IL_01B5;
					case 2:
						entry.ETag = (string)annotationValue;
						goto IL_01B5;
					case 3:
						entry.EditLink = (Uri)annotationValue;
						goto IL_01B5;
					case 4:
						entry.ReadLink = (Uri)annotationValue;
						goto IL_01B5;
					case 5:
						ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
						mediaResource.EditLink = (Uri)annotationValue;
						goto IL_01B5;
					case 6:
						ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
						mediaResource.ReadLink = (Uri)annotationValue;
						goto IL_01B5;
					case 7:
						ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
						mediaResource.ContentType = (string)annotationValue;
						goto IL_01B5;
					case 8:
						ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
						mediaResource.ETag = (string)annotationValue;
						goto IL_01B5;
					}
				}
			}
			ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
			entry.InstanceAnnotations.Add(new ODataInstanceAnnotation(annotationName, annotationValue.ToODataValue()));
			IL_01B5:
			if (mediaResource != null && entry.MediaResource == null)
			{
				this.SetEntryMediaResource(entryState, mediaResource);
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001D094 File Offset: 0x0001B294
		internal void ReadAndApplyFeedInstanceAnnotationValue(string annotationName, ODataFeedBase feed, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (annotationName != null)
			{
				if (annotationName == "odata.count")
				{
					feed.Count = new long?(base.ReadAndValidateAnnotationAsLongForIeee754Compatible("odata.count"));
					return;
				}
				if (annotationName == "odata.nextLink")
				{
					feed.NextPageLink = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
					return;
				}
				if (annotationName == "odata.deltaLink")
				{
					feed.DeltaLink = base.ReadAndValidateAnnotationStringValueAsUri("odata.deltaLink");
					return;
				}
			}
			ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
			object obj = base.ReadCustomInstanceAnnotationValue(duplicatePropertyNamesChecker, annotationName);
			feed.InstanceAnnotations.Add(new ODataInstanceAnnotation(annotationName, obj.ToODataValue()));
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001D130 File Offset: 0x0001B330
		internal ODataJsonLightReaderNavigationLinkInfo ReadEntryPropertyWithoutValue(IODataJsonLightReaderEntryState entryState, string propertyName)
		{
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = null;
			IEdmEntityType entityType = entryState.EntityType;
			IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(propertyName, entityType);
			if (edmProperty != null)
			{
				IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
				if (edmNavigationProperty != null)
				{
					if (base.ReadingResponse)
					{
						odataJsonLightReaderNavigationLinkInfo = ODataJsonLightEntryAndFeedDeserializer.ReadDeferredNavigationLink(entryState, propertyName, edmNavigationProperty);
					}
					else
					{
						odataJsonLightReaderNavigationLinkInfo = (edmNavigationProperty.Type.IsCollection() ? ODataJsonLightEntryAndFeedDeserializer.ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(entryState, edmNavigationProperty, false) : ODataJsonLightEntryAndFeedDeserializer.ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(entryState, edmNavigationProperty, false));
						if (!odataJsonLightReaderNavigationLinkInfo.HasEntityReferenceLink)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink(propertyName, "odata.bind"));
						}
					}
					entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataJsonLightReaderNavigationLinkInfo.NavigationLink);
				}
				else
				{
					IEdmTypeReference type = edmProperty.Type;
					if (!type.IsStream())
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithWrongType(propertyName, type.FullName()));
					}
					ODataStreamReferenceValue odataStreamReferenceValue = this.ReadStreamPropertyValue(entryState, propertyName);
					ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, edmProperty.Name, odataStreamReferenceValue);
				}
			}
			else
			{
				odataJsonLightReaderNavigationLinkInfo = this.ReadUndeclaredProperty(entryState, propertyName, false);
			}
			return odataJsonLightReaderNavigationLinkInfo;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001D208 File Offset: 0x0001B408
		internal void ReadNextLinkAnnotationAtFeedEnd(ODataFeedBase feed, ODataJsonLightReaderNavigationLinkInfo expandedNavigationLinkInfo, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (expandedNavigationLinkInfo != null)
			{
				this.ReadExpandedFeedAnnotationsAtFeedEnd(feed, expandedNavigationLinkInfo);
				return;
			}
			bool flag = base.JsonReader is ReorderingJsonReader;
			this.ReadTopLevelFeedAnnotations(feed, duplicatePropertyNamesChecker, false, flag);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001D23C File Offset: 0x0001B43C
		private static ODataJsonLightReaderNavigationLinkInfo ReadDeferredNavigationLink(IODataJsonLightReaderEntryState entryState, string navigationPropertyName, IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationPropertyName,
				IsCollection = ((navigationProperty == null) ? default(bool?) : new bool?(navigationProperty.Type.IsCollection()))
			};
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(odataNavigationLink.Name);
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) != null)
					{
						if (key == "odata.navigationLink")
						{
							odataNavigationLink.Url = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.associationLink")
						{
							odataNavigationLink.AssociationLinkUrl = (Uri)keyValuePair.Value;
							continue;
						}
					}
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedDeferredLinkPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key));
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateDeferredLinkInfo(odataNavigationLink, navigationProperty);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001D344 File Offset: 0x0001B544
		private static ODataJsonLightReaderNavigationLinkInfo ReadExpandedEntryNavigationLink(IODataJsonLightReaderEntryState entryState, IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(false)
			};
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(odataNavigationLink.Name);
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) != null)
					{
						if (key == "odata.navigationLink")
						{
							odataNavigationLink.Url = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.associationLink")
						{
							odataNavigationLink.AssociationLinkUrl = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.context")
						{
							odataNavigationLink.ContextUrl = (Uri)keyValuePair.Value;
							continue;
						}
					}
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key));
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateExpandedEntryLinkInfo(odataNavigationLink, navigationProperty);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001D460 File Offset: 0x0001B660
		private static ODataJsonLightReaderNavigationLinkInfo ReadExpandedFeedNavigationLink(IODataJsonLightReaderEntryState entryState, IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(true)
			};
			ODataFeed odataFeed = new ODataFeed();
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(odataNavigationLink.Name);
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) != null)
					{
						if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x6000804-1 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
							dictionary.Add("odata.navigationLink", 0);
							dictionary.Add("odata.associationLink", 1);
							dictionary.Add("odata.nextLink", 2);
							dictionary.Add("odata.count", 3);
							dictionary.Add("odata.context", 4);
							dictionary.Add("odata.deltaLink", 5);
							<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x6000804-1 = dictionary;
						}
						int num;
						if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x6000804-1.TryGetValue(key, ref num))
						{
							switch (num)
							{
							case 0:
								odataNavigationLink.Url = (Uri)keyValuePair.Value;
								continue;
							case 1:
								odataNavigationLink.AssociationLinkUrl = (Uri)keyValuePair.Value;
								continue;
							case 2:
								odataFeed.NextPageLink = (Uri)keyValuePair.Value;
								continue;
							case 3:
								odataFeed.Count = (long?)keyValuePair.Value;
								continue;
							case 4:
								odataNavigationLink.ContextUrl = (Uri)keyValuePair.Value;
								continue;
							}
						}
					}
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key));
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateExpandedFeedLinkInfo(odataNavigationLink, navigationProperty, odataFeed);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001D624 File Offset: 0x0001B824
		private static ODataJsonLightReaderNavigationLinkInfo ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(IODataJsonLightReaderEntryState entryState, IEdmNavigationProperty navigationProperty, bool isExpanded)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(false)
			};
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(odataNavigationLink.Name);
			ODataEntityReferenceLink odataEntityReferenceLink = null;
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) == null || !(key == "odata.bind"))
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key, "odata.bind"));
					}
					LinkedList<ODataEntityReferenceLink> linkedList = keyValuePair.Value as LinkedList<ODataEntityReferenceLink>;
					if (linkedList != null)
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_ArrayValueForSingletonBindPropertyAnnotation(odataNavigationLink.Name, "odata.bind"));
					}
					if (isExpanded)
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_SingletonNavigationPropertyWithBindingAndValue(odataNavigationLink.Name, "odata.bind"));
					}
					odataEntityReferenceLink = (ODataEntityReferenceLink)keyValuePair.Value;
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateSingletonEntityReferenceLinkInfo(odataNavigationLink, navigationProperty, odataEntityReferenceLink, isExpanded);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001D740 File Offset: 0x0001B940
		private static ODataJsonLightReaderNavigationLinkInfo ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(IODataJsonLightReaderEntryState entryState, IEdmNavigationProperty navigationProperty, bool isExpanded)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(true)
			};
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(odataNavigationLink.Name);
			LinkedList<ODataEntityReferenceLink> linkedList = null;
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) == null || !(key == "odata.bind"))
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key, "odata.bind"));
					}
					ODataEntityReferenceLink odataEntityReferenceLink = keyValuePair.Value as ODataEntityReferenceLink;
					if (odataEntityReferenceLink != null)
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_StringValueForCollectionBindPropertyAnnotation(odataNavigationLink.Name, "odata.bind"));
					}
					linkedList = (LinkedList<ODataEntityReferenceLink>)keyValuePair.Value;
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateCollectionEntityReferenceLinksInfo(odataNavigationLink, navigationProperty, linkedList, isExpanded);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001D840 File Offset: 0x0001BA40
		private static void AddEntryProperty(IODataJsonLightReaderEntryState entryState, string propertyName, object propertyValue)
		{
			ODataProperty odataProperty = new ODataProperty
			{
				Name = propertyName,
				Value = propertyValue
			};
			Dictionary<string, object> customPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetCustomPropertyAnnotations(propertyName);
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
			entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
			ODataEntry entry = entryState.Entry;
			entry.Properties = entry.Properties.ConcatToReadOnlyEnumerable("Properties", odataProperty);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001D908 File Offset: 0x0001BB08
		private void ReadExpandedFeedAnnotationsAtFeedEnd(ODataFeedBase feed, ODataJsonLightReaderNavigationLinkInfo expandedNavigationLinkInfo)
		{
			string text;
			string text2;
			while (base.JsonReader.NodeType == JsonNodeType.Property && ODataJsonLightDeserializer.TryParsePropertyAnnotation(base.JsonReader.GetPropertyName(), out text, out text2) && string.CompareOrdinal(text, expandedNavigationLinkInfo.NavigationLink.Name) == 0)
			{
				if (!base.ReadingResponse)
				{
					throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation(text, text2));
				}
				base.JsonReader.Read();
				string text3;
				if ((text3 = base.CompleteSimplifiedODataAnnotation(text2)) != null)
				{
					if (!(text3 == "odata.nextLink"))
					{
						if (!(text3 == "odata.count"))
						{
							if (!(text3 == "odata.deltaLink"))
							{
							}
						}
						else
						{
							if (feed.Count != null)
							{
								throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_DuplicateExpandedFeedAnnotation("odata.count", expandedNavigationLinkInfo.NavigationLink.Name));
							}
							feed.Count = new long?(base.ReadAndValidateAnnotationAsLongForIeee754Compatible("odata.count"));
							continue;
						}
					}
					else
					{
						if (feed.NextPageLink != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_DuplicateExpandedFeedAnnotation("odata.nextLink", expandedNavigationLinkInfo.NavigationLink.Name));
						}
						feed.NextPageLink = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
						continue;
					}
				}
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedPropertyAnnotationAfterExpandedFeed(text2, expandedNavigationLinkInfo.NavigationLink.Name));
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001DA48 File Offset: 0x0001BC48
		private void SetEntryMediaResource(IODataJsonLightReaderEntryState entryState, ODataStreamReferenceValue mediaResource)
		{
			ODataEntry entry = entryState.Entry;
			ODataEntityMetadataBuilder entityMetadataBuilderForReader = base.MetadataContext.GetEntityMetadataBuilderForReader(entryState, base.JsonLightInputContext.MessageReaderSettings.UseKeyAsSegment);
			mediaResource.SetMetadataBuilder(entityMetadataBuilderForReader, null);
			entry.MediaResource = mediaResource;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001DA88 File Offset: 0x0001BC88
		private ODataJsonLightReaderNavigationLinkInfo ReadEntryPropertyWithValue(IODataJsonLightReaderEntryState entryState, string propertyName)
		{
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = null;
			IEdmEntityType entityType = entryState.EntityType;
			IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(propertyName, entityType);
			if (edmProperty != null)
			{
				IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
				if (edmNavigationProperty != null)
				{
					bool flag = edmNavigationProperty.Type.IsCollection();
					this.ValidateExpandedNavigationLinkPropertyValue(new bool?(flag), edmNavigationProperty.Name);
					if (flag)
					{
						odataJsonLightReaderNavigationLinkInfo = (base.ReadingResponse ? ODataJsonLightEntryAndFeedDeserializer.ReadExpandedFeedNavigationLink(entryState, edmNavigationProperty) : ODataJsonLightEntryAndFeedDeserializer.ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(entryState, edmNavigationProperty, true));
					}
					else
					{
						odataJsonLightReaderNavigationLinkInfo = (base.ReadingResponse ? ODataJsonLightEntryAndFeedDeserializer.ReadExpandedEntryNavigationLink(entryState, edmNavigationProperty) : ODataJsonLightEntryAndFeedDeserializer.ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(entryState, edmNavigationProperty, true));
					}
					entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataJsonLightReaderNavigationLinkInfo.NavigationLink);
				}
				else
				{
					IEdmTypeReference type = edmProperty.Type;
					if (type.IsStream())
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_StreamPropertyWithValue(propertyName));
					}
					this.ReadEntryDataProperty(entryState, edmProperty, ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(entryState.DuplicatePropertyNamesChecker, propertyName));
				}
			}
			else
			{
				odataJsonLightReaderNavigationLinkInfo = this.ReadUndeclaredProperty(entryState, propertyName, true);
			}
			return odataJsonLightReaderNavigationLinkInfo;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001DB64 File Offset: 0x0001BD64
		private void ReadEntryDataProperty(IODataJsonLightReaderEntryState entryState, IEdmProperty edmProperty, string propertyTypeName)
		{
			ODataNullValueBehaviorKind odataNullValueBehaviorKind = (base.ReadingResponse ? ODataNullValueBehaviorKind.Default : base.Model.NullValueReadBehaviorKind(edmProperty));
			object obj = base.ReadNonEntityValue(propertyTypeName, edmProperty.Type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, false, false, edmProperty.Name, default(bool?));
			if (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null)
			{
				ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, edmProperty.Name, obj);
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
		private void ReadOpenProperty(IODataJsonLightReaderEntryState entryState, string propertyName, bool propertyWithValue)
		{
			if (!propertyWithValue)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_OpenPropertyWithoutValue(propertyName));
			}
			string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(entryState.DuplicatePropertyNamesChecker, propertyName);
			object obj = base.ReadNonEntityValue(text, null, null, null, true, false, false, propertyName, new bool?(true));
			ValidationUtils.ValidateOpenPropertyValue(propertyName, obj);
			ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, obj);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001DC14 File Offset: 0x0001BE14
		private ODataJsonLightReaderNavigationLinkInfo ReadUndeclaredProperty(IODataJsonLightReaderEntryState entryState, string propertyName, bool propertyWithValue)
		{
			if (!base.MessageReaderSettings.ReportUndeclaredLinkProperties && entryState.EntityType.IsOpen)
			{
				this.ReadOpenProperty(entryState, propertyName, propertyWithValue);
				return null;
			}
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(propertyName);
			if (odataPropertyAnnotations != null)
			{
				object obj;
				if (odataPropertyAnnotations.TryGetValue("odata.navigationLink", ref obj) || odataPropertyAnnotations.TryGetValue("odata.associationLink", ref obj))
				{
					if (!base.MessageReaderSettings.ReportUndeclaredLinkProperties)
					{
						throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.FullTypeName()));
					}
					ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = ODataJsonLightEntryAndFeedDeserializer.ReadDeferredNavigationLink(entryState, propertyName, null);
					entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataJsonLightReaderNavigationLinkInfo.NavigationLink);
					if (propertyWithValue)
					{
						if (!base.MessageReaderSettings.IgnoreUndeclaredValueProperties)
						{
							throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.FullTypeName()));
						}
						this.ValidateExpandedNavigationLinkPropertyValue(default(bool?), propertyName);
						base.JsonReader.SkipValue();
					}
					return odataJsonLightReaderNavigationLinkInfo;
				}
				else if (odataPropertyAnnotations.TryGetValue("odata.mediaEditLink", ref obj) || odataPropertyAnnotations.TryGetValue("odata.mediaReadLink", ref obj) || odataPropertyAnnotations.TryGetValue("odata.mediaContentType", ref obj) || odataPropertyAnnotations.TryGetValue("odata.mediaEtag", ref obj))
				{
					if (!base.MessageReaderSettings.ReportUndeclaredLinkProperties)
					{
						throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.FullTypeName()));
					}
					if (propertyWithValue)
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_StreamPropertyWithValue(propertyName));
					}
					ODataStreamReferenceValue odataStreamReferenceValue = this.ReadStreamPropertyValue(entryState, propertyName);
					ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, odataStreamReferenceValue);
					return null;
				}
			}
			if (entryState.EntityType.IsOpen)
			{
				this.ReadOpenProperty(entryState, propertyName, propertyWithValue);
				return null;
			}
			if (!propertyWithValue)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithUnknownType(propertyName));
			}
			if (!base.MessageReaderSettings.IgnoreUndeclaredValueProperties)
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.FullTypeName()));
			}
			ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(entryState.DuplicatePropertyNamesChecker, propertyName);
			this.ReadOpenProperty(entryState, propertyName, propertyWithValue);
			return null;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001DDE4 File Offset: 0x0001BFE4
		private ODataStreamReferenceValue ReadStreamPropertyValue(IODataJsonLightReaderEntryState entryState, string streamPropertyName)
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_StreamPropertyInRequest);
			}
			ODataStreamReferenceValue odataStreamReferenceValue = new ODataStreamReferenceValue();
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(streamPropertyName);
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) != null)
					{
						if (key == "odata.mediaEditLink")
						{
							odataStreamReferenceValue.EditLink = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.mediaReadLink")
						{
							odataStreamReferenceValue.ReadLink = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.mediaEtag")
						{
							odataStreamReferenceValue.ETag = (string)keyValuePair.Value;
							continue;
						}
						if (key == "odata.mediaContentType")
						{
							odataStreamReferenceValue.ContentType = (string)keyValuePair.Value;
							continue;
						}
					}
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedStreamPropertyAnnotation(streamPropertyName, keyValuePair.Key));
				}
			}
			ODataEntityMetadataBuilder entityMetadataBuilderForReader = base.MetadataContext.GetEntityMetadataBuilderForReader(entryState, base.JsonLightInputContext.MessageReaderSettings.UseKeyAsSegment);
			odataStreamReferenceValue.SetMetadataBuilder(entityMetadataBuilderForReader, streamPropertyName);
			return odataStreamReferenceValue;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001DF30 File Offset: 0x0001C130
		private void ReadSingleOperationValue(IODataJsonOperationsDeserializerContext readerContext, IODataJsonLightReaderEntryState entryState, string metadataReferencePropertyName, bool insideArray)
		{
			if (readerContext.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(metadataReferencePropertyName, readerContext.JsonReader.NodeType));
			}
			readerContext.JsonReader.ReadStartObject();
			ODataOperation odataOperation = this.CreateODataOperationAndAddToEntry(readerContext, metadataReferencePropertyName);
			if (odataOperation == null)
			{
				while (readerContext.JsonReader.NodeType == JsonNodeType.Property)
				{
					readerContext.JsonReader.ReadPropertyName();
					readerContext.JsonReader.SkipValue();
				}
				readerContext.JsonReader.ReadEndObject();
				return;
			}
			while (readerContext.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = ODataAnnotationNames.RemoveAnnotationPrefix(readerContext.JsonReader.ReadPropertyName());
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "title"))
					{
						if (text2 == "target")
						{
							if (odataOperation.Target != null)
							{
								throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_MultipleOptionalPropertiesInOperation(text, metadataReferencePropertyName));
							}
							string text3 = readerContext.JsonReader.ReadStringValue("target");
							ODataJsonLightValidationUtils.ValidateOperationPropertyValueIsNotNull(text3, text, metadataReferencePropertyName);
							odataOperation.Target = readerContext.ProcessUriFromPayload(text3);
							continue;
						}
					}
					else
					{
						if (odataOperation.Title != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_MultipleOptionalPropertiesInOperation(text, metadataReferencePropertyName));
						}
						string text4 = readerContext.JsonReader.ReadStringValue("title");
						ODataJsonLightValidationUtils.ValidateOperationPropertyValueIsNotNull(text4, text, metadataReferencePropertyName);
						odataOperation.Title = text4;
						continue;
					}
				}
				readerContext.JsonReader.SkipValue();
			}
			if (odataOperation.Target == null && insideArray)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_OperationMissingTargetProperty(metadataReferencePropertyName));
			}
			readerContext.JsonReader.ReadEndObject();
			this.SetMetadataBuilder(entryState, odataOperation);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001E0B4 File Offset: 0x0001C2B4
		private void ReadSingleOperationValue(ODataFeed feed, string metadataReferencePropertyName, bool insideArray)
		{
			if (base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(metadataReferencePropertyName, base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartObject();
			ODataOperation odataOperation = this.CreateODataOperationAndAddToFeed(feed, metadataReferencePropertyName);
			if (odataOperation == null)
			{
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					base.JsonReader.ReadPropertyName();
					base.JsonReader.SkipValue();
				}
				base.JsonReader.ReadEndObject();
				return;
			}
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = ODataAnnotationNames.RemoveAnnotationPrefix(base.JsonReader.ReadPropertyName());
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "title"))
					{
						if (text2 == "target")
						{
							if (odataOperation.Target != null)
							{
								throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_MultipleOptionalPropertiesInOperation(text, metadataReferencePropertyName));
							}
							string text3 = base.JsonReader.ReadStringValue("target");
							ODataJsonLightValidationUtils.ValidateOperationPropertyValueIsNotNull(text3, text, metadataReferencePropertyName);
							odataOperation.Target = base.ProcessUriFromPayload(text3);
							continue;
						}
					}
					else
					{
						if (odataOperation.Title != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_MultipleOptionalPropertiesInOperation(text, metadataReferencePropertyName));
						}
						string text4 = base.JsonReader.ReadStringValue("title");
						ODataJsonLightValidationUtils.ValidateOperationPropertyValueIsNotNull(text4, text, metadataReferencePropertyName);
						odataOperation.Title = text4;
						continue;
					}
				}
				base.JsonReader.SkipValue();
			}
			if (odataOperation.Target == null && insideArray)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_OperationMissingTargetProperty(metadataReferencePropertyName));
			}
			base.JsonReader.ReadEndObject();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001E230 File Offset: 0x0001C430
		private void SetMetadataBuilder(IODataJsonLightReaderEntryState entryState, ODataOperation operation)
		{
			ODataEntityMetadataBuilder entityMetadataBuilderForReader = base.MetadataContext.GetEntityMetadataBuilderForReader(entryState, base.JsonLightInputContext.MessageReaderSettings.UseKeyAsSegment);
			operation.SetMetadataBuilder(entityMetadataBuilderForReader, base.ContextUriParseResult.MetadataDocumentUri);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001E26C File Offset: 0x0001C46C
		private ODataOperation CreateODataOperationAndAddToEntry(IODataJsonOperationsDeserializerContext readerContext, string metadataReferencePropertyName)
		{
			string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IEdmOperation edmOperation = Enumerable.FirstOrDefault<IEdmOperation>(base.JsonLightInputContext.Model.ResolveOperations(uriFragmentFromMetadataReferencePropertyName));
			if (edmOperation == null)
			{
				return null;
			}
			bool flag;
			ODataOperation odataOperation = ODataJsonLightUtils.CreateODataOperation(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName, edmOperation, out flag);
			if (flag)
			{
				readerContext.AddActionToEntry((ODataAction)odataOperation);
			}
			else
			{
				readerContext.AddFunctionToEntry((ODataFunction)odataOperation);
			}
			return odataOperation;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001E2DC File Offset: 0x0001C4DC
		private ODataOperation CreateODataOperationAndAddToFeed(ODataFeed feed, string metadataReferencePropertyName)
		{
			string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IEdmOperation edmOperation = Enumerable.FirstOrDefault<IEdmOperation>(base.JsonLightInputContext.Model.ResolveOperations(uriFragmentFromMetadataReferencePropertyName));
			if (edmOperation == null)
			{
				return null;
			}
			bool flag;
			ODataOperation odataOperation = ODataJsonLightUtils.CreateODataOperation(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName, edmOperation, out flag);
			if (flag)
			{
				feed.AddAction((ODataAction)odataOperation);
			}
			else
			{
				feed.AddFunction((ODataFunction)odataOperation);
			}
			return odataOperation;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001E34C File Offset: 0x0001C54C
		private void ReadMetadataReferencePropertyValue(IODataJsonLightReaderEntryState entryState, string metadataReferencePropertyName)
		{
			this.ValidateCanReadMetadataReferenceProperty();
			ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IODataJsonOperationsDeserializerContext iodataJsonOperationsDeserializerContext = new ODataJsonLightEntryAndFeedDeserializer.OperationsDeserializerContext(entryState.Entry, this);
			bool flag = false;
			if (iodataJsonOperationsDeserializerContext.JsonReader.NodeType == JsonNodeType.StartArray)
			{
				iodataJsonOperationsDeserializerContext.JsonReader.ReadStartArray();
				flag = true;
			}
			do
			{
				this.ReadSingleOperationValue(iodataJsonOperationsDeserializerContext, entryState, metadataReferencePropertyName, flag);
			}
			while (flag && iodataJsonOperationsDeserializerContext.JsonReader.NodeType != JsonNodeType.EndArray);
			if (flag)
			{
				iodataJsonOperationsDeserializerContext.JsonReader.ReadEndArray();
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001E3C4 File Offset: 0x0001C5C4
		private void ReadMetadataReferencePropertyValue(ODataFeed feed, string metadataReferencePropertyName)
		{
			this.ValidateCanReadMetadataReferenceProperty();
			ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			bool flag = false;
			if (base.JsonReader.NodeType == JsonNodeType.StartArray)
			{
				base.JsonReader.ReadStartArray();
				flag = true;
			}
			do
			{
				this.ReadSingleOperationValue(feed, metadataReferencePropertyName, flag);
			}
			while (flag && base.JsonReader.NodeType != JsonNodeType.EndArray);
			if (flag)
			{
				base.JsonReader.ReadEndArray();
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001E42D File Offset: 0x0001C62D
		private void ValidateCanReadMetadataReferenceProperty()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_MetadataReferencePropertyInRequest);
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001E444 File Offset: 0x0001C644
		private void ValidateExpandedNavigationLinkPropertyValue(bool? isCollection, string propertyName)
		{
			JsonNodeType nodeType = base.JsonReader.NodeType;
			if (nodeType == JsonNodeType.StartArray)
			{
				if (isCollection == false)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue(nodeType, propertyName));
				}
			}
			else
			{
				if ((nodeType != JsonNodeType.PrimitiveValue || base.JsonReader.Value != null) && nodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadNavigationPropertyValue(propertyName));
				}
				if (isCollection == true)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue(nodeType, propertyName));
				}
			}
		}

		// Token: 0x020000DF RID: 223
		private sealed class OperationsDeserializerContext : IODataJsonOperationsDeserializerContext
		{
			// Token: 0x06000850 RID: 2128 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
			public OperationsDeserializerContext(ODataEntry entry, ODataJsonLightEntryAndFeedDeserializer jsonLightEntryAndFeedDeserializer)
			{
				this.entry = entry;
				this.jsonLightEntryAndFeedDeserializer = jsonLightEntryAndFeedDeserializer;
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001E4EA File Offset: 0x0001C6EA
			public JsonReader JsonReader
			{
				get
				{
					return this.jsonLightEntryAndFeedDeserializer.JsonReader;
				}
			}

			// Token: 0x06000852 RID: 2130 RVA: 0x0001E4F7 File Offset: 0x0001C6F7
			public Uri ProcessUriFromPayload(string uriFromPayload)
			{
				return this.jsonLightEntryAndFeedDeserializer.ProcessUriFromPayload(uriFromPayload);
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x0001E505 File Offset: 0x0001C705
			public void AddActionToEntry(ODataAction action)
			{
				this.entry.AddAction(action);
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x0001E513 File Offset: 0x0001C713
			public void AddFunctionToEntry(ODataFunction function)
			{
				this.entry.AddFunction(function);
			}

			// Token: 0x04000384 RID: 900
			private ODataEntry entry;

			// Token: 0x04000385 RID: 901
			private ODataJsonLightEntryAndFeedDeserializer jsonLightEntryAndFeedDeserializer;
		}
	}
}
