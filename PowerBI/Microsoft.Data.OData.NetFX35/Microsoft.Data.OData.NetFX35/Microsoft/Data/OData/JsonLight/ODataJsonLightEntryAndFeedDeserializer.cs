using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200018D RID: 397
	internal sealed class ODataJsonLightEntryAndFeedDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x06000AE8 RID: 2792 RVA: 0x00024D0E File Offset: 0x00022F0E
		internal ODataJsonLightEntryAndFeedDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.annotationGroupDeserializer = new JsonLightAnnotationGroupDeserializer(jsonLightInputContext);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00024D24 File Offset: 0x00022F24
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

		// Token: 0x06000AEA RID: 2794 RVA: 0x00024D9C File Offset: 0x00022F9C
		internal void ReadFeedContentEnd()
		{
			base.JsonReader.ReadEndArray();
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00024DAC File Offset: 0x00022FAC
		internal void ReadEntryTypeName(IODataJsonLightReaderEntryState entryState)
		{
			if (base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("odata.type", base.JsonReader.GetPropertyName()) == 0)
			{
				if (!string.IsNullOrEmpty(entryState.Entry.TypeName))
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EntryTypeAlreadySpecified);
				}
				base.JsonReader.Read();
				entryState.Entry.TypeName = base.ReadODataTypeAnnotationValue();
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00024EE4 File Offset: 0x000230E4
		internal ODataJsonLightReaderNavigationLinkInfo ReadEntryContent(IODataJsonLightReaderEntryState entryState)
		{
			ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
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

		// Token: 0x06000AED RID: 2797 RVA: 0x00024F5C File Offset: 0x0002315C
		internal void ValidateEntryMetadata(IODataJsonLightReaderEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			if (entry != null)
			{
				IEdmEntityType entityType = entryState.EntityType;
				if (!base.ReadingResponse && base.Model.HasDefaultStream(entityType) && entry.MediaResource == null)
				{
					ODataStreamReferenceValue mediaResource = entry.MediaResource;
					ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
					this.SetEntryMediaResource(entryState, mediaResource);
				}
				bool flag = base.UseDefaultFormatBehavior || base.UseServerFormatBehavior;
				ValidationUtils.ValidateEntryMetadataResource(entry, entityType, base.Model, flag);
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00025100 File Offset: 0x00023300
		internal void ReadTopLevelFeedAnnotations(ODataFeed feed, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool forFeedStart, bool readAllFeedProperties)
		{
			bool buffering = false;
			try
			{
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					bool foundValueProperty = false;
					if (!forFeedStart && readAllFeedProperties)
					{
						duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(true, base.JsonLightInputContext.ReadingResponse);
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
							throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
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

		// Token: 0x06000AEF RID: 2799 RVA: 0x00025200 File Offset: 0x00023400
		internal object ReadEntryPropertyAnnotationValue(string propertyAnnotationName)
		{
			string text;
			if (base.TryReadODataTypeAnnotationValue(propertyAnnotationName, out text))
			{
				return text;
			}
			if (propertyAnnotationName != null)
			{
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000a8e-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(9);
					dictionary.Add("odata.navigationLinkUrl", 0);
					dictionary.Add("odata.associationLinkUrl", 1);
					dictionary.Add("odata.nextLink", 2);
					dictionary.Add("odata.mediaEditLink", 3);
					dictionary.Add("odata.mediaReadLink", 4);
					dictionary.Add("odata.mediaETag", 5);
					dictionary.Add("odata.mediaContentType", 6);
					dictionary.Add("odata.bind", 7);
					dictionary.Add("odata.deltaLink", 8);
					<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000a8e-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000a8e-1.TryGetValue(propertyAnnotationName, ref num))
				{
					switch (num)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
						return base.ReadAndValidateAnnotationStringValueAsUri(propertyAnnotationName);
					case 5:
					case 6:
						return base.ReadAndValidateAnnotationStringValue(propertyAnnotationName);
					case 7:
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

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000253A0 File Offset: 0x000235A0
		internal void ApplyAnnotationGroupIfPresent(IODataJsonLightReaderEntryState entryState)
		{
			ODataJsonLightAnnotationGroup odataJsonLightAnnotationGroup = this.annotationGroupDeserializer.ReadAnnotationGroup(new Func<string, object>(this.ReadEntryPropertyAnnotationValue), (string annotationName, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker) => this.ReadEntryInstanceAnnotation(annotationName, false, false, duplicatePropertyNamesChecker));
			this.ApplyAnnotationGroup(entryState, odataJsonLightAnnotationGroup);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000253DC File Offset: 0x000235DC
		internal object ReadEntryInstanceAnnotation(string annotationName, bool anyPropertyFound, bool typeAnnotationFound, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (annotationName != null)
			{
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000a90-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(11);
					dictionary.Add("odata.type", 0);
					dictionary.Add("odata.id", 1);
					dictionary.Add("odata.etag", 2);
					dictionary.Add("odata.editLink", 3);
					dictionary.Add("odata.readLink", 4);
					dictionary.Add("odata.mediaEditLink", 5);
					dictionary.Add("odata.mediaReadLink", 6);
					dictionary.Add("odata.mediaContentType", 7);
					dictionary.Add("odata.mediaETag", 8);
					dictionary.Add("odata.annotationGroup", 9);
					dictionary.Add("odata.annotationGroupReference", 10);
					<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000a90-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000a90-1.TryGetValue(annotationName, ref num))
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
						return base.ReadAndValidateAnnotationStringValue(annotationName);
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
					case 9:
					case 10:
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EncounteredAnnotationGroupInUnexpectedPosition);
					}
				}
			}
			ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
			return this.ReadCustomInstanceAnnotationValue(duplicatePropertyNamesChecker, annotationName);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00025548 File Offset: 0x00023748
		internal void ApplyEntryInstanceAnnotation(IODataJsonLightReaderEntryState entryState, string annotationName, object annotationValue)
		{
			ODataEntry entry = entryState.Entry;
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			if (annotationName != null)
			{
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000a91-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(11);
					dictionary.Add("odata.type", 0);
					dictionary.Add("odata.id", 1);
					dictionary.Add("odata.etag", 2);
					dictionary.Add("odata.editLink", 3);
					dictionary.Add("odata.readLink", 4);
					dictionary.Add("odata.mediaEditLink", 5);
					dictionary.Add("odata.mediaReadLink", 6);
					dictionary.Add("odata.mediaContentType", 7);
					dictionary.Add("odata.mediaETag", 8);
					dictionary.Add("odata.annotationGroup", 9);
					dictionary.Add("odata.annotationGroupReference", 10);
					<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000a91-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000a91-1.TryGetValue(annotationName, ref num))
				{
					switch (num)
					{
					case 0:
						entry.TypeName = (string)annotationValue;
						goto IL_01C1;
					case 1:
						entry.Id = (string)annotationValue;
						goto IL_01C1;
					case 2:
						entry.ETag = (string)annotationValue;
						goto IL_01C1;
					case 3:
						entry.EditLink = (Uri)annotationValue;
						goto IL_01C1;
					case 4:
						entry.ReadLink = (Uri)annotationValue;
						goto IL_01C1;
					case 5:
						ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
						mediaResource.EditLink = (Uri)annotationValue;
						goto IL_01C1;
					case 6:
						ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
						mediaResource.ReadLink = (Uri)annotationValue;
						goto IL_01C1;
					case 7:
						ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
						mediaResource.ContentType = (string)annotationValue;
						goto IL_01C1;
					case 8:
						ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
						mediaResource.ETag = (string)annotationValue;
						goto IL_01C1;
					case 9:
					case 10:
						goto IL_01C1;
					}
				}
			}
			ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
			entry.InstanceAnnotations.Add(new ODataInstanceAnnotation(annotationName, annotationValue.ToODataValue()));
			IL_01C1:
			if (mediaResource != null && entry.MediaResource == null)
			{
				this.SetEntryMediaResource(entryState, mediaResource);
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002572C File Offset: 0x0002392C
		internal object ReadCustomInstanceAnnotationValue(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, string name)
		{
			Dictionary<string, object> odataPropertyAnnotations = duplicatePropertyNamesChecker.GetODataPropertyAnnotations(name);
			string text = null;
			object obj;
			if (odataPropertyAnnotations != null && odataPropertyAnnotations.TryGetValue("odata.type", ref obj))
			{
				text = (string)obj;
			}
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfValueTerm(name, base.Model);
			return base.ReadNonEntityValue(text, edmTypeReference, null, null, false, false, false, name);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002577C File Offset: 0x0002397C
		internal void ReadAndApplyFeedInstanceAnnotationValue(string annotationName, ODataFeed feed, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (annotationName != null)
			{
				if (annotationName == "odata.count")
				{
					feed.Count = new long?(base.ReadAndValidateAnnotationStringValueAsLong("odata.count"));
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
			object obj = this.ReadCustomInstanceAnnotationValue(duplicatePropertyNamesChecker, annotationName);
			feed.InstanceAnnotations.Add(new ODataInstanceAnnotation(annotationName, obj.ToODataValue()));
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00025818 File Offset: 0x00023A18
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
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithWrongType(propertyName, type.ODataFullName()));
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

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000258F0 File Offset: 0x00023AF0
		internal void ReadNextLinkAnnotationAtFeedEnd(ODataFeed feed, ODataJsonLightReaderNavigationLinkInfo expandedNavigationLinkInfo, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (expandedNavigationLinkInfo != null)
			{
				this.ReadExpandedFeedAnnotationsAtFeedEnd(feed, expandedNavigationLinkInfo);
				return;
			}
			bool flag = base.JsonReader is ReorderingJsonReader;
			this.ReadTopLevelFeedAnnotations(feed, duplicatePropertyNamesChecker, false, flag);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00025924 File Offset: 0x00023B24
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
						if (key == "odata.navigationLinkUrl")
						{
							odataNavigationLink.Url = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.associationLinkUrl")
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

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00025A2C File Offset: 0x00023C2C
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
						if (key == "odata.navigationLinkUrl")
						{
							odataNavigationLink.Url = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.associationLinkUrl")
						{
							odataNavigationLink.AssociationLinkUrl = (Uri)keyValuePair.Value;
							continue;
						}
					}
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key));
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateExpandedEntryLinkInfo(odataNavigationLink, navigationProperty);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00025B20 File Offset: 0x00023D20
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
						if (key == "odata.navigationLinkUrl")
						{
							odataNavigationLink.Url = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.associationLinkUrl")
						{
							odataNavigationLink.AssociationLinkUrl = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.nextLink")
						{
							odataFeed.NextPageLink = (Uri)keyValuePair.Value;
							continue;
						}
						if (!(key == "odata.deltaLink"))
						{
						}
					}
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key));
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateExpandedFeedLinkInfo(odataNavigationLink, navigationProperty, odataFeed);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00025C54 File Offset: 0x00023E54
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

		// Token: 0x06000AFB RID: 2811 RVA: 0x00025D70 File Offset: 0x00023F70
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

		// Token: 0x06000AFC RID: 2812 RVA: 0x00025E70 File Offset: 0x00024070
		private static ODataProperty AddEntryProperty(IODataJsonLightReaderEntryState entryState, string propertyName, object propertyValue)
		{
			ODataProperty odataProperty = new ODataProperty
			{
				Name = propertyName,
				Value = propertyValue
			};
			entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
			ODataEntry entry = entryState.Entry;
			entry.Properties = entry.Properties.ConcatToReadOnlyEnumerable("Properties", odataProperty);
			return odataProperty;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00025EC0 File Offset: 0x000240C0
		private void ReadExpandedFeedAnnotationsAtFeedEnd(ODataFeed feed, ODataJsonLightReaderNavigationLinkInfo expandedNavigationLinkInfo)
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
				if ((text3 = text2) != null)
				{
					if (!(text3 == "odata.nextLink"))
					{
						if (!(text3 == "odata.count") && !(text3 == "odata.deltaLink"))
						{
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

		// Token: 0x06000AFE RID: 2814 RVA: 0x00025FB0 File Offset: 0x000241B0
		private void ApplyAnnotationGroup(IODataJsonLightReaderEntryState entryState, ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (annotationGroup != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in annotationGroup.Annotations)
				{
					string key = keyValuePair.Key;
					object value = keyValuePair.Value;
					string text;
					string text2;
					if (ODataJsonLightDeserializer.TryParsePropertyAnnotation(key, out text, out text2))
					{
						entryState.DuplicatePropertyNamesChecker.AddODataPropertyAnnotation(text, text2, value);
					}
					else
					{
						this.ApplyEntryInstanceAnnotation(entryState, key, value);
					}
				}
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00026034 File Offset: 0x00024234
		private void SetEntryMediaResource(IODataJsonLightReaderEntryState entryState, ODataStreamReferenceValue mediaResource)
		{
			ODataEntry entry = entryState.Entry;
			ODataEntityMetadataBuilder entityMetadataBuilderForReader = base.MetadataContext.GetEntityMetadataBuilderForReader(entryState);
			mediaResource.SetMetadataBuilder(entityMetadataBuilderForReader, null);
			entry.MediaResource = mediaResource;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00026064 File Offset: 0x00024264
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
					this.ValidateExpandedNavigationLinkPropertyValue(new bool?(flag));
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

		// Token: 0x06000B01 RID: 2817 RVA: 0x00026138 File Offset: 0x00024338
		private void ReadEntryDataProperty(IODataJsonLightReaderEntryState entryState, IEdmProperty edmProperty, string propertyTypeName)
		{
			ODataNullValueBehaviorKind odataNullValueBehaviorKind = (base.ReadingResponse ? ODataNullValueBehaviorKind.Default : base.Model.NullValueReadBehaviorKind(edmProperty));
			object obj = base.ReadNonEntityValue(propertyTypeName, edmProperty.Type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, false, false, edmProperty.Name);
			if (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null)
			{
				ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, edmProperty.Name, obj);
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00026190 File Offset: 0x00024390
		private void InnerReadOpenUndeclaredProperty(IODataJsonLightReaderEntryState entryState, IEdmStructuredType owningStructuredType, string propertyName, bool propertyWithValue)
		{
			if (!propertyWithValue)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_OpenPropertyWithoutValue(propertyName));
			}
			bool flag = false;
			string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(entryState.DuplicatePropertyNamesChecker, propertyName);
			string text2 = base.TryReadOrPeekPayloadType(entryState.DuplicatePropertyNamesChecker, propertyName, flag);
			EdmTypeKind edmTypeKind;
			IEdmType edmType = ReaderValidationUtils.ResolvePayloadTypeName(base.Model, null, text2, EdmTypeKind.Complex, base.MessageReaderSettings.ReaderBehavior, base.Version, out edmTypeKind);
			IEdmTypeReference edmTypeReference = null;
			if (!string.IsNullOrEmpty(text2) && edmType != null)
			{
				EdmTypeKind edmTypeKind2;
				SerializationTypeNameAnnotation serializationTypeNameAnnotation;
				edmTypeReference = ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, null, null, text2, base.Model, base.MessageReaderSettings, base.Version, new Func<EdmTypeKind>(base.GetNonEntityValueKind), out edmTypeKind2, out serializationTypeNameAnnotation);
			}
			object obj = null;
			bool flag2 = ODataJsonLightPropertyAndValueDeserializer.IsKnownValueTypeForOpenEntityOrComplex(base.JsonReader.NodeType, base.JsonReader.Value, text2, edmTypeReference);
			bool flag5;
			if (flag2)
			{
				bool flag3 = true;
				if (ODataJsonReaderCoreUtils.TryReadNullValue(base.JsonReader, base.JsonLightInputContext, edmTypeReference, flag3, propertyName))
				{
					bool flag4 = false;
					if (flag4)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue("odata.null", "true"));
					}
					obj = null;
				}
				else
				{
					obj = base.ReadNonEntityValue(text, null, null, null, true, false, false, propertyName);
				}
				flag5 = true;
			}
			else if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				if (!string.IsNullOrEmpty(text2) && edmTypeReference == null)
				{
					throw new ODataException(Strings.ValidationUtils_UnrecognizedTypeName(text2));
				}
				if (string.IsNullOrEmpty(text2) && (base.JsonReader.NodeType == JsonNodeType.StartObject || base.JsonReader.NodeType == JsonNodeType.StartArray))
				{
					throw new ODataException(Strings.ReaderValidationUtils_ValueWithoutType);
				}
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.ODataFullName()));
			}
			else if (base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				StringBuilder stringBuilder = new StringBuilder();
				base.JsonReader.SkipValue(stringBuilder);
				obj = new ODataUntypedValue
				{
					RawJson = stringBuilder.ToString()
				};
				flag5 = true;
			}
			else
			{
				base.JsonReader.SkipValue();
				flag5 = false;
			}
			if (flag5)
			{
				ValidationUtils.ValidateOpenPropertyValue(propertyName, obj, base.MessageReaderSettings.UndeclaredPropertyBehaviorKinds);
				ODataProperty odataProperty = ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, obj);
				bool flag6 = obj is ODataUntypedValue;
				if (flag6)
				{
					ODataJsonLightPropertyAndValueDeserializer.TryAttachRawAnnotationSetToPropertyValue(entryState.DuplicatePropertyNamesChecker, odataProperty);
				}
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000263AC File Offset: 0x000245AC
		private ODataJsonLightReaderNavigationLinkInfo ReadUndeclaredProperty(IODataJsonLightReaderEntryState entryState, string propertyName, bool propertyWithValue)
		{
			if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty) && entryState.EntityType.IsOpen)
			{
				this.InnerReadOpenUndeclaredProperty(entryState, entryState.EntityType, propertyName, propertyWithValue);
				return null;
			}
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(propertyName);
			if (odataPropertyAnnotations != null)
			{
				object obj;
				if (odataPropertyAnnotations.TryGetValue("odata.navigationLinkUrl", ref obj) || odataPropertyAnnotations.TryGetValue("odata.associationLinkUrl", ref obj))
				{
					if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
					{
						throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
					}
					ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = ODataJsonLightEntryAndFeedDeserializer.ReadDeferredNavigationLink(entryState, propertyName, null);
					entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataJsonLightReaderNavigationLinkInfo.NavigationLink);
					if (propertyWithValue)
					{
						if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
						{
							throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
						}
						this.ValidateExpandedNavigationLinkPropertyValue(default(bool?));
						base.JsonReader.SkipValue();
					}
					return odataJsonLightReaderNavigationLinkInfo;
				}
				else if (odataPropertyAnnotations.TryGetValue("odata.mediaEditLink", ref obj) || odataPropertyAnnotations.TryGetValue("odata.mediaReadLink", ref obj) || odataPropertyAnnotations.TryGetValue("odata.mediaContentType", ref obj) || odataPropertyAnnotations.TryGetValue("odata.mediaETag", ref obj))
				{
					if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
					{
						throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
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
				this.InnerReadOpenUndeclaredProperty(entryState, entryState.EntityType, propertyName, propertyWithValue);
				return null;
			}
			if (!propertyWithValue)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithUnknownType(propertyName));
			}
			if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
			}
			ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(entryState.DuplicatePropertyNamesChecker, propertyName);
			if (base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				bool flag = false;
				object obj2 = base.InnerReadNonOpenUndeclaredProperty(entryState.DuplicatePropertyNamesChecker, propertyName, flag);
				ODataProperty odataProperty = ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, obj2);
				ODataJsonLightPropertyAndValueDeserializer.TryAttachRawAnnotationSetToPropertyValue(entryState.DuplicatePropertyNamesChecker, odataProperty);
			}
			else
			{
				base.JsonReader.SkipValue();
			}
			return null;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000265E8 File Offset: 0x000247E8
		private ODataStreamReferenceValue ReadStreamPropertyValue(IODataJsonLightReaderEntryState entryState, string streamPropertyName)
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_StreamPropertyInRequest);
			}
			ODataVersionChecker.CheckStreamReferenceProperty(base.Version);
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
						if (key == "odata.mediaETag")
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
			ODataEntityMetadataBuilder entityMetadataBuilderForReader = base.MetadataContext.GetEntityMetadataBuilderForReader(entryState);
			odataStreamReferenceValue.SetMetadataBuilder(entityMetadataBuilderForReader, streamPropertyName);
			return odataStreamReferenceValue;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00026730 File Offset: 0x00024930
		private void ReadSingleOperationValue(IODataJsonOperationsDeserializerContext readerContext, IODataJsonLightReaderEntryState entryState, string metadataReferencePropertyName, bool insideArray)
		{
			if (readerContext.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(metadataReferencePropertyName, readerContext.JsonReader.NodeType));
			}
			readerContext.JsonReader.ReadStartObject();
			ODataOperation odataOperation = this.CreateODataOperationAndAddToEntry(readerContext, entryState, metadataReferencePropertyName);
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
				string text = readerContext.JsonReader.ReadPropertyName();
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

		// Token: 0x06000B06 RID: 2822 RVA: 0x000268B0 File Offset: 0x00024AB0
		private void SetMetadataBuilder(IODataJsonLightReaderEntryState entryState, ODataOperation operation)
		{
			ODataEntityMetadataBuilder entityMetadataBuilderForReader = base.MetadataContext.GetEntityMetadataBuilderForReader(entryState);
			operation.SetMetadataBuilder(entityMetadataBuilderForReader, base.MetadataUriParseResult.MetadataDocumentUri);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000268DC File Offset: 0x00024ADC
		private ODataOperation CreateODataOperationAndAddToEntry(IODataJsonOperationsDeserializerContext readerContext, IODataJsonLightReaderEntryState entryState, string metadataReferencePropertyName)
		{
			string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(base.MetadataUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IEnumerable<IEdmFunctionImport> enumerable = base.JsonLightInputContext.Model.ResolveFunctionImports(uriFragmentFromMetadataReferencePropertyName);
			IEdmFunctionImport edmFunctionImport = null;
			bool flag = false;
			foreach (IEdmFunctionImport edmFunctionImport2 in enumerable)
			{
				string text;
				if (base.JsonLightInputContext.Model.TryGetODataAnnotation(edmFunctionImport2, "HttpMethod", out text))
				{
					flag = true;
				}
				else if (edmFunctionImport == null)
				{
					edmFunctionImport = edmFunctionImport2;
				}
			}
			if (edmFunctionImport != null)
			{
				bool flag2;
				ODataOperation odataOperation = ODataJsonLightUtils.CreateODataOperation(base.MetadataUriParseResult.MetadataDocumentUri, metadataReferencePropertyName, edmFunctionImport, out flag2);
				if (flag2)
				{
					readerContext.AddActionToEntry((ODataAction)odataOperation);
				}
				else
				{
					readerContext.AddFunctionToEntry((ODataFunction)odataOperation);
				}
				return odataOperation;
			}
			if (flag)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_FunctionImportIsNotActionOrFunction(uriFragmentFromMetadataReferencePropertyName));
			}
			return null;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000269BC File Offset: 0x00024BBC
		private void ReadMetadataReferencePropertyValue(IODataJsonLightReaderEntryState entryState, string metadataReferencePropertyName)
		{
			this.ValidateCanReadMetadataReferenceProperty();
			ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(base.MetadataUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
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

		// Token: 0x06000B09 RID: 2825 RVA: 0x00026A33 File Offset: 0x00024C33
		private void ValidateCanReadMetadataReferenceProperty()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_MetadataReferencePropertyInRequest);
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00026A48 File Offset: 0x00024C48
		private void ValidateExpandedNavigationLinkPropertyValue(bool? isCollection)
		{
			JsonNodeType nodeType = base.JsonReader.NodeType;
			if (nodeType == JsonNodeType.StartArray)
			{
				if (isCollection == false)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue(nodeType));
				}
			}
			else
			{
				if ((nodeType != JsonNodeType.PrimitiveValue || base.JsonReader.Value != null) && nodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadNavigationPropertyValue);
				}
				if (isCollection == true)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue(nodeType));
				}
			}
		}

		// Token: 0x04000419 RID: 1049
		private readonly JsonLightAnnotationGroupDeserializer annotationGroupDeserializer;

		// Token: 0x0200018E RID: 398
		private sealed class OperationsDeserializerContext : IODataJsonOperationsDeserializerContext
		{
			// Token: 0x06000B0C RID: 2828 RVA: 0x00026AD5 File Offset: 0x00024CD5
			public OperationsDeserializerContext(ODataEntry entry, ODataJsonLightEntryAndFeedDeserializer jsonLightEntryAndFeedDeserializer)
			{
				this.entry = entry;
				this.jsonLightEntryAndFeedDeserializer = jsonLightEntryAndFeedDeserializer;
			}

			// Token: 0x1700029C RID: 668
			// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00026AEB File Offset: 0x00024CEB
			public JsonReader JsonReader
			{
				get
				{
					return this.jsonLightEntryAndFeedDeserializer.JsonReader;
				}
			}

			// Token: 0x06000B0E RID: 2830 RVA: 0x00026AF8 File Offset: 0x00024CF8
			public Uri ProcessUriFromPayload(string uriFromPayload)
			{
				return this.jsonLightEntryAndFeedDeserializer.ProcessUriFromPayload(uriFromPayload);
			}

			// Token: 0x06000B0F RID: 2831 RVA: 0x00026B06 File Offset: 0x00024D06
			public void AddActionToEntry(ODataAction action)
			{
				this.entry.AddAction(action);
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x00026B14 File Offset: 0x00024D14
			public void AddFunctionToEntry(ODataFunction function)
			{
				this.entry.AddFunction(function);
			}

			// Token: 0x0400041A RID: 1050
			private ODataEntry entry;

			// Token: 0x0400041B RID: 1051
			private ODataJsonLightEntryAndFeedDeserializer jsonLightEntryAndFeedDeserializer;
		}
	}
}
