using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000235 RID: 565
	internal sealed class ODataVerboseJsonEntryAndFeedDeserializer : ODataVerboseJsonPropertyAndValueDeserializer
	{
		// Token: 0x06001115 RID: 4373 RVA: 0x00040144 File Offset: 0x0003E344
		internal ODataVerboseJsonEntryAndFeedDeserializer(ODataVerboseJsonInputContext verboseJsonInputContext)
			: base(verboseJsonInputContext)
		{
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00040150 File Offset: 0x0003E350
		internal void ReadFeedStart(ODataFeed feed, bool isResultsWrapperExpected, bool isExpandedLinkContent)
		{
			if (isResultsWrapperExpected)
			{
				base.JsonReader.ReadNext();
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = base.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal("results", text) == 0)
					{
						goto IL_004C;
					}
					this.ReadFeedProperty(feed, text, isExpandedLinkContent);
				}
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_ExpectedFeedResultsPropertyNotFound);
			}
			IL_004C:
			if (base.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotReadFeedContentStart(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartArray();
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000401E0 File Offset: 0x0003E3E0
		internal void ReadFeedEnd(ODataFeed feed, bool readResultsWrapper, bool isExpandedLinkContent)
		{
			if (readResultsWrapper)
			{
				base.JsonReader.ReadEndArray();
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = base.JsonReader.ReadPropertyName();
					this.ReadFeedProperty(feed, text, isExpandedLinkContent);
				}
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00040220 File Offset: 0x0003E420
		internal void ReadEntryStart()
		{
			if (base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonReader_CannotReadEntryStart(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadNext();
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00040258 File Offset: 0x0003E458
		internal void ReadEntryMetadataPropertyValue(IODataVerboseJsonReaderEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			base.JsonReader.ReadStartObject();
			ODataStreamReferenceValue odataStreamReferenceValue = null;
			ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertyBitMask = ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.None;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x60010a0-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(11);
						dictionary.Add("uri", 0);
						dictionary.Add("id", 1);
						dictionary.Add("etag", 2);
						dictionary.Add("type", 3);
						dictionary.Add("media_src", 4);
						dictionary.Add("edit_media", 5);
						dictionary.Add("content_type", 6);
						dictionary.Add("media_etag", 7);
						dictionary.Add("actions", 8);
						dictionary.Add("functions", 9);
						dictionary.Add("properties", 10);
						<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x60010a0-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x60010a0-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.ReadUriMetadataProperty(entry, ref metadataPropertyBitMask);
							continue;
						case 1:
							this.ReadIdMetadataProperty(entry, ref metadataPropertyBitMask);
							continue;
						case 2:
							this.ReadETagMetadataProperty(entry, ref metadataPropertyBitMask);
							continue;
						case 3:
							base.JsonReader.SkipValue();
							continue;
						case 4:
							this.ReadMediaSourceMetadataProperty(ref metadataPropertyBitMask, ref odataStreamReferenceValue);
							continue;
						case 5:
							this.ReadEditMediaMetadataProperty(ref metadataPropertyBitMask, ref odataStreamReferenceValue);
							continue;
						case 6:
							this.ReadContentTypeMetadataProperty(ref metadataPropertyBitMask, ref odataStreamReferenceValue);
							continue;
						case 7:
							this.ReadMediaETagMetadataProperty(ref metadataPropertyBitMask, ref odataStreamReferenceValue);
							continue;
						case 8:
							this.ReadActionsMetadataProperty(entry, ref metadataPropertyBitMask);
							continue;
						case 9:
							this.ReadFunctionsMetadataProperty(entry, ref metadataPropertyBitMask);
							continue;
						case 10:
							this.ReadPropertiesMetadataProperty(entryState, ref metadataPropertyBitMask);
							continue;
						}
					}
				}
				base.JsonReader.SkipValue();
			}
			entry.MediaResource = odataStreamReferenceValue;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00040424 File Offset: 0x0003E624
		internal void ValidateEntryMetadata(IODataVerboseJsonReaderEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			IEdmEntityType entityType = entryState.EntityType;
			if (base.Model.HasDefaultStream(entityType) && entry.MediaResource == null)
			{
				ODataStreamReferenceValue odataStreamReferenceValue = null;
				ODataVerboseJsonReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref odataStreamReferenceValue);
				entry.MediaResource = odataStreamReferenceValue;
			}
			bool useDefaultFormatBehavior = base.UseDefaultFormatBehavior;
			ValidationUtils.ValidateEntryMetadataResource(entry, entityType, base.Model, useDefaultFormatBehavior);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0004047C File Offset: 0x0003E67C
		internal ODataNavigationLink ReadEntryContent(IODataVerboseJsonReaderEntryState entryState, out IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = null;
			navigationProperty = null;
			IEdmEntityType entityType = entryState.EntityType;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("__metadata", text) == 0)
				{
					if (entryState.MetadataPropertyFound)
					{
						throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesInEntryValue);
					}
					entryState.MetadataPropertyFound = true;
					base.JsonReader.SkipValue();
				}
				else
				{
					if (!ValidationUtils.IsValidPropertyName(text))
					{
						base.JsonReader.SkipValue();
						continue;
					}
					IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(text, entityType);
					if (edmProperty != null)
					{
						navigationProperty = edmProperty as IEdmNavigationProperty;
						if (navigationProperty != null)
						{
							if (this.ShouldEntryPropertyBeSkipped())
							{
								base.JsonReader.SkipValue();
							}
							else
							{
								bool flag = navigationProperty.Type.IsCollection();
								odataNavigationLink = new ODataNavigationLink
								{
									Name = text,
									IsCollection = new bool?(flag)
								};
								this.ValidateNavigationLinkPropertyValue(flag);
								entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataNavigationLink);
							}
						}
						else
						{
							this.ReadEntryProperty(entryState, edmProperty);
						}
					}
					else
					{
						odataNavigationLink = this.ReadUndeclaredProperty(entryState, text);
					}
				}
				if (odataNavigationLink != null)
				{
					break;
				}
			}
			return odataNavigationLink;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00040590 File Offset: 0x0003E790
		internal void ReadDeferredNavigationLink(ODataNavigationLink navigationLink)
		{
			base.JsonReader.ReadStartObject();
			base.JsonReader.ReadPropertyName();
			base.JsonReader.ReadStartObject();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("uri", text) == 0)
				{
					if (navigationLink.Url != null)
					{
						throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleUriPropertiesInDeferredLink);
					}
					string text2 = base.JsonReader.ReadStringValue("uri");
					if (text2 == null)
					{
						throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_DeferredLinkUriCannotBeNull);
					}
					navigationLink.Url = base.ProcessUriFromPayload(text2);
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			if (navigationLink.Url == null)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_DeferredLinkMissingUri);
			}
			base.JsonReader.ReadEndObject();
			base.JsonReader.ReadEndObject();
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0004066C File Offset: 0x0003E86C
		internal ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			base.JsonReader.ReadStartObject();
			base.JsonReader.ReadPropertyName();
			base.JsonReader.ReadStartObject();
			ODataEntityReferenceLink odataEntityReferenceLink = new ODataEntityReferenceLink();
			ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertyBitMask = ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.None;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("uri", text) == 0)
				{
					ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertyBitMask, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Uri, "uri");
					string text2 = base.JsonReader.ReadStringValue("uri");
					ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text2, "uri");
					odataEntityReferenceLink.Url = base.ProcessUriFromPayload(text2);
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			base.JsonReader.ReadEndObject();
			base.JsonReader.ReadEndObject();
			return odataEntityReferenceLink;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00040728 File Offset: 0x0003E928
		internal bool IsDeferredLink(bool navigationLinkFound)
		{
			JsonNodeType jsonNodeType = base.JsonReader.NodeType;
			if (jsonNodeType == JsonNodeType.PrimitiveValue)
			{
				if (base.JsonReader.Value == null)
				{
					return false;
				}
				if (navigationLinkFound)
				{
					throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotReadNavigationPropertyValue);
				}
				return false;
			}
			else
			{
				if (jsonNodeType == JsonNodeType.StartArray)
				{
					return false;
				}
				base.JsonReader.StartBuffering();
				bool flag;
				try
				{
					base.JsonReader.ReadStartObject();
					jsonNodeType = base.JsonReader.NodeType;
					if (jsonNodeType == JsonNodeType.EndObject)
					{
						flag = false;
					}
					else
					{
						string text = base.JsonReader.ReadPropertyName();
						if (string.CompareOrdinal("__deferred", text) != 0)
						{
							flag = false;
						}
						else
						{
							base.JsonReader.SkipValue();
							flag = base.JsonReader.NodeType == JsonNodeType.EndObject;
						}
					}
				}
				finally
				{
					base.JsonReader.StopBuffering();
				}
				return flag;
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000407EC File Offset: 0x0003E9EC
		internal bool IsEntityReferenceLink()
		{
			JsonNodeType jsonNodeType = base.JsonReader.NodeType;
			if (jsonNodeType != JsonNodeType.StartObject)
			{
				return false;
			}
			base.JsonReader.StartBuffering();
			bool flag;
			try
			{
				base.JsonReader.ReadStartObject();
				jsonNodeType = base.JsonReader.NodeType;
				if (jsonNodeType == JsonNodeType.EndObject)
				{
					flag = false;
				}
				else
				{
					bool flag2 = false;
					while (base.JsonReader.NodeType == JsonNodeType.Property)
					{
						string text = base.JsonReader.ReadPropertyName();
						if (string.CompareOrdinal("__metadata", text) != 0)
						{
							return false;
						}
						if (base.JsonReader.NodeType != JsonNodeType.StartObject)
						{
							return false;
						}
						base.JsonReader.ReadStartObject();
						while (base.JsonReader.NodeType == JsonNodeType.Property)
						{
							string text2 = base.JsonReader.ReadPropertyName();
							if (string.CompareOrdinal("uri", text2) == 0)
							{
								flag2 = true;
							}
							base.JsonReader.SkipValue();
						}
						base.JsonReader.ReadEndObject();
					}
					flag = flag2;
				}
			}
			finally
			{
				base.JsonReader.StopBuffering();
			}
			return flag;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000408F4 File Offset: 0x0003EAF4
		private static void AddEntryProperty(IODataVerboseJsonReaderEntryState entryState, string propertyName, object propertyValue)
		{
			ODataProperty odataProperty = new ODataProperty
			{
				Name = propertyName,
				Value = propertyValue
			};
			entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
			ODataEntry entry = entryState.Entry;
			entry.Properties = entry.Properties.ConcatToReadOnlyEnumerable("Properties", odataProperty);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00040944 File Offset: 0x0003EB44
		private void ReadFeedProperty(ODataFeed feed, string propertyName, bool isExpandedLinkContent)
		{
			switch (ODataVerboseJsonReaderUtils.DetermineFeedPropertyKind(propertyName))
			{
			case ODataVerboseJsonReaderUtils.FeedPropertyKind.Unsupported:
				base.JsonReader.SkipValue();
				return;
			case ODataVerboseJsonReaderUtils.FeedPropertyKind.Count:
				if (base.ReadingResponse && base.Version >= ODataVersion.V2 && !isExpandedLinkContent)
				{
					string text = base.JsonReader.ReadStringValue("__count");
					ODataVerboseJsonReaderUtils.ValidateFeedProperty(text, "__count");
					long num = (long)ODataVerboseJsonReaderUtils.ConvertValue(text, EdmCoreModel.Instance.GetInt64(false), base.MessageReaderSettings, base.Version, true, propertyName);
					feed.Count = new long?(num);
					return;
				}
				base.JsonReader.SkipValue();
				return;
			case ODataVerboseJsonReaderUtils.FeedPropertyKind.Results:
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleFeedResultsPropertiesFound);
			case ODataVerboseJsonReaderUtils.FeedPropertyKind.NextPageLink:
				if (base.ReadingResponse && base.Version >= ODataVersion.V2)
				{
					string text2 = base.JsonReader.ReadStringValue("__next");
					ODataVerboseJsonReaderUtils.ValidateFeedProperty(text2, "__next");
					feed.NextPageLink = base.ProcessUriFromPayload(text2);
					return;
				}
				base.JsonReader.SkipValue();
				return;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataVerboseJsonEntryAndFeedDeserializer_ReadFeedProperty));
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00040A54 File Offset: 0x0003EC54
		private void ReadEntryProperty(IODataVerboseJsonReaderEntryState entryState, IEdmProperty edmProperty)
		{
			ODataNullValueBehaviorKind odataNullValueBehaviorKind = (base.ReadingResponse ? ODataNullValueBehaviorKind.Default : base.Model.NullValueReadBehaviorKind(edmProperty));
			IEdmTypeReference type = edmProperty.Type;
			object obj = (type.IsStream() ? this.ReadStreamPropertyValue() : base.ReadNonEntityValue(type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, edmProperty.Name));
			if (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null)
			{
				ODataVerboseJsonEntryAndFeedDeserializer.AddEntryProperty(entryState, edmProperty.Name, obj);
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00040AB8 File Offset: 0x0003ECB8
		private void ReadOpenProperty(IODataVerboseJsonReaderEntryState entryState, string propertyName)
		{
			object obj = base.ReadNonEntityValue(null, null, null, true, propertyName);
			ValidationUtils.ValidateOpenPropertyValue(propertyName, obj, base.MessageReaderSettings.UndeclaredPropertyBehaviorKinds);
			ODataVerboseJsonEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, obj);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00040AEC File Offset: 0x0003ECEC
		private ODataNavigationLink ReadUndeclaredProperty(IODataVerboseJsonReaderEntryState entryState, string propertyName)
		{
			if (entryState.EntityType.IsOpen && this.ShouldEntryPropertyBeSkipped())
			{
				base.JsonReader.SkipValue();
				return null;
			}
			bool flag = false;
			bool flag2 = false;
			if (base.JsonReader.NodeType == JsonNodeType.StartObject)
			{
				base.JsonReader.StartBuffering();
				base.JsonReader.Read();
				if (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = base.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal(text, "__deferred") == 0)
					{
						flag2 = true;
					}
					else if (string.CompareOrdinal(text, "__mediaresource") == 0)
					{
						flag = true;
					}
					base.JsonReader.SkipValue();
					if (base.JsonReader.NodeType != JsonNodeType.EndObject)
					{
						flag = false;
						flag2 = false;
					}
				}
				base.JsonReader.StopBuffering();
			}
			if (flag || flag2)
			{
				if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
				{
					throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
				}
			}
			else
			{
				if (entryState.EntityType.IsOpen)
				{
					this.ReadOpenProperty(entryState, propertyName);
					return null;
				}
				if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty))
				{
					throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
				}
			}
			if (flag2)
			{
				ODataNavigationLink odataNavigationLink = new ODataNavigationLink
				{
					Name = propertyName,
					IsCollection = default(bool?)
				};
				entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataNavigationLink);
				return odataNavigationLink;
			}
			if (flag)
			{
				object obj = this.ReadStreamPropertyValue();
				ODataVerboseJsonEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, obj);
				return null;
			}
			base.JsonReader.SkipValue();
			return null;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00040C64 File Offset: 0x0003EE64
		private ODataStreamReferenceValue ReadStreamPropertyValue()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_StreamPropertyInRequest);
			}
			ODataVersionChecker.CheckStreamReferenceProperty(base.Version);
			base.JsonReader.ReadStartObject();
			if (base.JsonReader.NodeType != JsonNodeType.Property)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotParseStreamReference);
			}
			string text = base.JsonReader.ReadPropertyName();
			if (string.CompareOrdinal("__mediaresource", text) != 0)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotParseStreamReference);
			}
			ODataStreamReferenceValue odataStreamReferenceValue = this.ReadStreamReferenceValue();
			if (base.JsonReader.NodeType != JsonNodeType.EndObject)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotParseStreamReference);
			}
			base.JsonReader.Read();
			return odataStreamReferenceValue;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00040D08 File Offset: 0x0003EF08
		private void ReadUriMetadataProperty(ODataEntry entry, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Uri, "uri");
			string text = base.JsonReader.ReadStringValue("uri");
			if (text != null)
			{
				ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "uri");
				entry.EditLink = base.ProcessUriFromPayload(text);
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00040D50 File Offset: 0x0003EF50
		private void ReadIdMetadataProperty(ODataEntry entry, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Id, "id");
			string text = base.JsonReader.ReadStringValue("id");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "id");
			entry.Id = text;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00040DA4 File Offset: 0x0003EFA4
		private void ReadETagMetadataProperty(ODataEntry entry, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.ETag, "etag");
			string text = base.JsonReader.ReadStringValue("etag");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "etag");
			entry.ETag = text;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00040DF4 File Offset: 0x0003EFF4
		private void ReadMediaSourceMetadataProperty(ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField, ref ODataStreamReferenceValue mediaResource)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.MediaUri, "media_src");
			ODataVerboseJsonReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
			string text = base.JsonReader.ReadStringValue("media_src");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "media_src");
			mediaResource.ReadLink = base.ProcessUriFromPayload(text);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00040E54 File Offset: 0x0003F054
		private void ReadEditMediaMetadataProperty(ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField, ref ODataStreamReferenceValue mediaResource)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.EditMedia, "edit_media");
			ODataVerboseJsonReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
			string text = base.JsonReader.ReadStringValue("edit_media");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "edit_media");
			mediaResource.EditLink = base.ProcessUriFromPayload(text);
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00040EB4 File Offset: 0x0003F0B4
		private void ReadContentTypeMetadataProperty(ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField, ref ODataStreamReferenceValue mediaResource)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.ContentType, "content_type");
			ODataVerboseJsonReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
			string text = base.JsonReader.ReadStringValue("content_type");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "content_type");
			mediaResource.ContentType = text;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00040F0C File Offset: 0x0003F10C
		private void ReadMediaETagMetadataProperty(ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField, ref ODataStreamReferenceValue mediaResource)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.MediaETag, "media_etag");
			ODataVerboseJsonReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
			string text = base.JsonReader.ReadStringValue("media_etag");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "media_etag");
			mediaResource.ETag = text;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00040F64 File Offset: 0x0003F164
		private void ReadActionsMetadataProperty(ODataEntry entry, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			if (base.MessageReaderSettings.MaxProtocolVersion >= ODataVersion.V3 && base.ReadingResponse)
			{
				ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Actions, "actions");
				this.ReadOperationsMetadata(entry, true);
				return;
			}
			base.JsonReader.SkipValue();
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00040FA0 File Offset: 0x0003F1A0
		private void ReadFunctionsMetadataProperty(ODataEntry entry, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			if (base.MessageReaderSettings.MaxProtocolVersion >= ODataVersion.V3 && base.ReadingResponse)
			{
				ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Functions, "functions");
				this.ReadOperationsMetadata(entry, false);
				return;
			}
			base.JsonReader.SkipValue();
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00040FDC File Offset: 0x0003F1DC
		private void ReadPropertiesMetadataProperty(IODataVerboseJsonReaderEntryState entryState, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			if (!base.ReadingResponse || base.MessageReaderSettings.MaxProtocolVersion < ODataVersion.V3)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Properties, "properties");
			if (base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_PropertyInEntryMustHaveObjectValue("properties", base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartObject();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				ValidationUtils.ValidateAssociationLinkName(text);
				ReaderValidationUtils.ValidateNavigationPropertyDefined(text, entryState.EntityType, base.MessageReaderSettings);
				base.JsonReader.ReadStartObject();
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text2 = base.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal(text2, "associationuri") == 0)
					{
						string text3 = base.JsonReader.ReadStringValue("associationuri");
						ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text3, "associationuri");
						ODataAssociationLink odataAssociationLink = new ODataAssociationLink
						{
							Name = text,
							Url = base.ProcessUriFromPayload(text3)
						};
						ValidationUtils.ValidateAssociationLink(odataAssociationLink);
						ReaderUtils.CheckForDuplicateAssociationLinkAndUpdateNavigationLink(entryState.DuplicatePropertyNamesChecker, odataAssociationLink);
						entryState.Entry.AddAssociationLink(odataAssociationLink);
					}
					else
					{
						base.JsonReader.SkipValue();
					}
				}
				base.JsonReader.ReadEndObject();
			}
			base.JsonReader.ReadEndObject();
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00041148 File Offset: 0x0003F348
		private void ReadOperationsMetadata(ODataEntry entry, bool isActions)
		{
			IODataJsonOperationsDeserializerContext iodataJsonOperationsDeserializerContext = new ODataVerboseJsonEntryAndFeedDeserializer.OperationsDeserializerContext(entry, this);
			string text = (isActions ? "actions" : "functions");
			if (iodataJsonOperationsDeserializerContext.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(text, iodataJsonOperationsDeserializerContext.JsonReader.NodeType));
			}
			iodataJsonOperationsDeserializerContext.JsonReader.ReadStartObject();
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			while (iodataJsonOperationsDeserializerContext.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text2 = iodataJsonOperationsDeserializerContext.JsonReader.ReadPropertyName();
				if (hashSet.Contains(text2))
				{
					throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_RepeatMetadataValue(text, text2));
				}
				hashSet.Add(text2);
				if (iodataJsonOperationsDeserializerContext.JsonReader.NodeType != JsonNodeType.StartArray)
				{
					throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_MetadataMustHaveArrayValue(text2, iodataJsonOperationsDeserializerContext.JsonReader.NodeType, text));
				}
				iodataJsonOperationsDeserializerContext.JsonReader.ReadStartArray();
				if (iodataJsonOperationsDeserializerContext.JsonReader.NodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationMetadataArrayExpectedAnObject(text2, iodataJsonOperationsDeserializerContext.JsonReader.NodeType, text));
				}
				Uri uri = this.ResolveUri(text2);
				while (iodataJsonOperationsDeserializerContext.JsonReader.NodeType == JsonNodeType.StartObject)
				{
					iodataJsonOperationsDeserializerContext.JsonReader.ReadStartObject();
					ODataOperation odataOperation;
					if (isActions)
					{
						odataOperation = new ODataAction();
					}
					else
					{
						odataOperation = new ODataFunction();
					}
					odataOperation.Metadata = uri;
					while (iodataJsonOperationsDeserializerContext.JsonReader.NodeType == JsonNodeType.Property)
					{
						string text3 = iodataJsonOperationsDeserializerContext.JsonReader.ReadPropertyName();
						string text4;
						if ((text4 = text3) != null)
						{
							if (!(text4 == "title"))
							{
								if (text4 == "target")
								{
									if (odataOperation.Target != null)
									{
										throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_MultipleTargetPropertiesInOperation(text2, text));
									}
									string text5 = iodataJsonOperationsDeserializerContext.JsonReader.ReadStringValue("target");
									ReaderValidationUtils.ValidateOperationProperty(text5, text3, text2, text);
									odataOperation.Target = iodataJsonOperationsDeserializerContext.ProcessUriFromPayload(text5);
									continue;
								}
							}
							else
							{
								if (odataOperation.Title != null)
								{
									throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_MultipleOptionalPropertiesInOperation(text3, text2, text));
								}
								string text6 = iodataJsonOperationsDeserializerContext.JsonReader.ReadStringValue("title");
								ReaderValidationUtils.ValidateOperationProperty(text6, text3, text2, text);
								odataOperation.Title = text6;
								continue;
							}
						}
						iodataJsonOperationsDeserializerContext.JsonReader.SkipValue();
					}
					if (odataOperation.Target == null)
					{
						throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationMissingTargetProperty(text2, text));
					}
					iodataJsonOperationsDeserializerContext.JsonReader.ReadEndObject();
					if (isActions)
					{
						iodataJsonOperationsDeserializerContext.AddActionToEntry((ODataAction)odataOperation);
					}
					else
					{
						iodataJsonOperationsDeserializerContext.AddFunctionToEntry((ODataFunction)odataOperation);
					}
				}
				iodataJsonOperationsDeserializerContext.JsonReader.ReadEndArray();
			}
			iodataJsonOperationsDeserializerContext.JsonReader.ReadEndObject();
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000413D4 File Offset: 0x0003F5D4
		private ODataStreamReferenceValue ReadStreamReferenceValue()
		{
			base.JsonReader.ReadStartObject();
			ODataStreamReferenceValue odataStreamReferenceValue = new ODataStreamReferenceValue();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "edit_media"))
					{
						if (!(text2 == "media_src"))
						{
							if (!(text2 == "content_type"))
							{
								if (text2 == "media_etag")
								{
									if (odataStreamReferenceValue.ETag != null)
									{
										throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesForStreamProperty("media_etag"));
									}
									string text3 = base.JsonReader.ReadStringValue("media_etag");
									ODataVerboseJsonReaderUtils.ValidateMediaResourceStringProperty(text3, "media_etag");
									odataStreamReferenceValue.ETag = text3;
									continue;
								}
							}
							else
							{
								if (odataStreamReferenceValue.ContentType != null)
								{
									throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesForStreamProperty("content_type"));
								}
								string text4 = base.JsonReader.ReadStringValue("content_type");
								ODataVerboseJsonReaderUtils.ValidateMediaResourceStringProperty(text4, "content_type");
								odataStreamReferenceValue.ContentType = text4;
								continue;
							}
						}
						else
						{
							if (odataStreamReferenceValue.ReadLink != null)
							{
								throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesForStreamProperty("media_src"));
							}
							string text5 = base.JsonReader.ReadStringValue("media_src");
							ODataVerboseJsonReaderUtils.ValidateMediaResourceStringProperty(text5, "media_src");
							odataStreamReferenceValue.ReadLink = base.ProcessUriFromPayload(text5);
							continue;
						}
					}
					else
					{
						if (odataStreamReferenceValue.EditLink != null)
						{
							throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesForStreamProperty("edit_media"));
						}
						string text6 = base.JsonReader.ReadStringValue("edit_media");
						ODataVerboseJsonReaderUtils.ValidateMediaResourceStringProperty(text6, "edit_media");
						odataStreamReferenceValue.EditLink = base.ProcessUriFromPayload(text6);
						continue;
					}
				}
				base.JsonReader.SkipValue();
			}
			base.JsonReader.ReadEndObject();
			return odataStreamReferenceValue;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00041590 File Offset: 0x0003F790
		private Uri ResolveUri(string uriFromPayload)
		{
			Uri uri = new Uri(uriFromPayload, 0);
			Uri uri2 = base.VerboseJsonInputContext.ResolveUri(base.MessageReaderSettings.BaseUri, uri);
			if (uri2 != null)
			{
				return uri2;
			}
			return uri;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000415CC File Offset: 0x0003F7CC
		private void ValidateNavigationLinkPropertyValue(bool isCollection)
		{
			JsonNodeType nodeType = base.JsonReader.NodeType;
			if (nodeType == JsonNodeType.StartArray)
			{
				if (!isCollection)
				{
					throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue(nodeType));
				}
			}
			else if (nodeType == JsonNodeType.PrimitiveValue && base.JsonReader.Value == null)
			{
				if (isCollection)
				{
					throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue(nodeType));
				}
			}
			else if (nodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotReadNavigationPropertyValue);
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00041631 File Offset: 0x0003F831
		private bool ShouldEntryPropertyBeSkipped()
		{
			return !base.ReadingResponse && base.UseServerFormatBehavior && this.IsDeferredLink(false);
		}

		// Token: 0x02000236 RID: 566
		private sealed class OperationsDeserializerContext : IODataJsonOperationsDeserializerContext
		{
			// Token: 0x06001135 RID: 4405 RVA: 0x0004164C File Offset: 0x0003F84C
			public OperationsDeserializerContext(ODataEntry entry, ODataVerboseJsonEntryAndFeedDeserializer verboseJsonEntryAndFeedDeserializer)
			{
				this.entry = entry;
				this.verboseJsonEntryAndFeedDeserializer = verboseJsonEntryAndFeedDeserializer;
			}

			// Token: 0x170003DE RID: 990
			// (get) Token: 0x06001136 RID: 4406 RVA: 0x00041662 File Offset: 0x0003F862
			public JsonReader JsonReader
			{
				get
				{
					return this.verboseJsonEntryAndFeedDeserializer.JsonReader;
				}
			}

			// Token: 0x06001137 RID: 4407 RVA: 0x0004166F File Offset: 0x0003F86F
			public Uri ProcessUriFromPayload(string uriFromPayload)
			{
				return this.verboseJsonEntryAndFeedDeserializer.ProcessUriFromPayload(uriFromPayload);
			}

			// Token: 0x06001138 RID: 4408 RVA: 0x0004167D File Offset: 0x0003F87D
			public void AddActionToEntry(ODataAction action)
			{
				this.entry.AddAction(action);
			}

			// Token: 0x06001139 RID: 4409 RVA: 0x0004168B File Offset: 0x0003F88B
			public void AddFunctionToEntry(ODataFunction function)
			{
				this.entry.AddFunction(function);
			}

			// Token: 0x0400068F RID: 1679
			private ODataEntry entry;

			// Token: 0x04000690 RID: 1680
			private ODataVerboseJsonEntryAndFeedDeserializer verboseJsonEntryAndFeedDeserializer;
		}
	}
}
