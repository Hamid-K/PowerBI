using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000F3 RID: 243
	internal sealed class ODataJsonLightReader : ODataReaderCoreAsync
	{
		// Token: 0x06000942 RID: 2370 RVA: 0x00021970 File Offset: 0x0001FB70
		internal ODataJsonLightReader(ODataJsonLightInputContext jsonLightInputContext, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, bool readingFeed, bool readingParameter = false, bool readingDelta = false, IODataReaderWriterListener listener = null)
			: base(jsonLightInputContext, readingFeed, readingDelta, listener)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightEntryAndFeedDeserializer = new ODataJsonLightEntryAndFeedDeserializer(jsonLightInputContext);
			this.readingParameter = readingParameter;
			this.topLevelScope = new ODataJsonLightReader.JsonLightTopLevelScope(navigationSource, expectedEntityType);
			base.EnterScope(this.topLevelScope);
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x000219BE File Offset: 0x0001FBBE
		private IODataJsonLightReaderEntryState CurrentEntryState
		{
			get
			{
				return (IODataJsonLightReaderEntryState)base.CurrentScope;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000219CB File Offset: 0x0001FBCB
		private ODataJsonLightReader.JsonLightFeedScope CurrentJsonLightFeedScope
		{
			get
			{
				return (ODataJsonLightReader.JsonLightFeedScope)base.CurrentScope;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x000219D8 File Offset: 0x0001FBD8
		private ODataJsonLightReader.JsonLightNavigationLinkScope CurrentJsonLightNavigationLinkScope
		{
			get
			{
				return (ODataJsonLightReader.JsonLightNavigationLinkScope)base.CurrentScope;
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000219E8 File Offset: 0x0001FBE8
		protected override bool ReadAtStartImplementation()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			ODataPayloadKind odataPayloadKind = (base.ReadingFeed ? ODataPayloadKind.Feed : ODataPayloadKind.Entry);
			this.jsonLightEntryAndFeedDeserializer.ReadPayloadStart(odataPayloadKind, duplicatePropertyNamesChecker, base.IsReadingNestedPayload, false);
			return this.ReadAtStartImplementationSynchronously(duplicatePropertyNamesChecker);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00021A29 File Offset: 0x0001FC29
		protected override bool ReadAtFeedStartImplementation()
		{
			return this.ReadAtFeedStartImplementationSynchronously();
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00021A31 File Offset: 0x0001FC31
		protected override bool ReadAtFeedEndImplementation()
		{
			return this.ReadAtFeedEndImplementationSynchronously();
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00021A39 File Offset: 0x0001FC39
		protected override bool ReadAtEntryStartImplementation()
		{
			return this.ReadAtEntryStartImplementationSynchronously();
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00021A41 File Offset: 0x0001FC41
		protected override bool ReadAtEntryEndImplementation()
		{
			return this.ReadAtEntryEndImplementationSynchronously();
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00021A49 File Offset: 0x0001FC49
		protected override bool ReadAtNavigationLinkStartImplementation()
		{
			return this.ReadAtNavigationLinkStartImplementationSynchronously();
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00021A51 File Offset: 0x0001FC51
		protected override bool ReadAtNavigationLinkEndImplementation()
		{
			return this.ReadAtNavigationLinkEndImplementationSynchronously();
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00021A59 File Offset: 0x0001FC59
		protected override bool ReadAtEntityReferenceLink()
		{
			return this.ReadAtEntityReferenceLinkSynchronously();
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00021A64 File Offset: 0x0001FC64
		private bool ReadAtStartImplementationSynchronously(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (this.jsonLightInputContext.ReadingResponse && !base.IsReadingNestedPayload)
			{
				ReaderValidationUtils.ValidateFeedOrEntryContextUri(this.jsonLightEntryAndFeedDeserializer.ContextUriParseResult, base.CurrentScope, true);
			}
			string text = ((this.jsonLightEntryAndFeedDeserializer.ContextUriParseResult == null) ? null : this.jsonLightEntryAndFeedDeserializer.ContextUriParseResult.SelectQueryOption);
			SelectedPropertiesNode selectedPropertiesNode = SelectedPropertiesNode.Create(text);
			if (base.ReadingFeed)
			{
				ODataFeed odataFeed = new ODataFeed();
				this.topLevelScope.DuplicatePropertyNamesChecker = duplicatePropertyNamesChecker;
				bool flag = this.jsonLightInputContext.JsonReader is ReorderingJsonReader;
				if (!base.IsReadingNestedPayload)
				{
					this.jsonLightEntryAndFeedDeserializer.ReadTopLevelFeedAnnotations(odataFeed, duplicatePropertyNamesChecker, true, flag);
				}
				this.ReadFeedStart(odataFeed, selectedPropertiesNode);
				return true;
			}
			this.ReadEntryStart(duplicatePropertyNamesChecker, selectedPropertiesNode);
			return true;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00021B20 File Offset: 0x0001FD20
		private bool ReadAtFeedStartImplementationSynchronously()
		{
			JsonNodeType nodeType = this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType;
			if (nodeType != JsonNodeType.StartObject)
			{
				if (nodeType != JsonNodeType.EndArray)
				{
					throw new ODataException(Strings.ODataJsonReader_CannotReadEntriesOfFeed(this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType));
				}
				this.ReadFeedEnd();
			}
			else
			{
				this.ReadEntryStart(null, this.CurrentJsonLightFeedScope.SelectedProperties);
			}
			return true;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00021B88 File Offset: 0x0001FD88
		private bool ReadAtFeedEndImplementationSynchronously()
		{
			bool isTopLevel = base.IsTopLevel;
			base.PopScope(ODataReaderState.FeedEnd);
			if (base.IsReadingNestedPayload && isTopLevel)
			{
				this.ReplaceScope(ODataReaderState.Completed);
				return false;
			}
			if (isTopLevel)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				this.jsonLightEntryAndFeedDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
				this.ReplaceScope(ODataReaderState.Completed);
				return false;
			}
			this.ReadExpandedNavigationLinkEnd(true);
			return true;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00021BF0 File Offset: 0x0001FDF0
		private bool ReadAtEntryStartImplementationSynchronously()
		{
			if (base.CurrentEntry == null)
			{
				this.EndEntry();
			}
			else if (this.jsonLightInputContext.UseServerApiBehavior)
			{
				ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = this.jsonLightEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
				if (odataJsonLightReaderNavigationLinkInfo != null)
				{
					this.StartNavigationLink(odataJsonLightReaderNavigationLinkInfo);
				}
				else
				{
					this.EndEntry();
				}
			}
			else if (this.CurrentEntryState.FirstNavigationLinkInfo != null)
			{
				this.StartNavigationLink(this.CurrentEntryState.FirstNavigationLinkInfo);
			}
			else
			{
				this.EndEntry();
			}
			return true;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00021C68 File Offset: 0x0001FE68
		private bool ReadAtEntryEndImplementationSynchronously()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			base.PopScope(ODataReaderState.EntryEnd);
			this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
			JsonNodeType nodeType = this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType;
			bool flag = true;
			if (isTopLevel)
			{
				this.jsonLightEntryAndFeedDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
				this.ReplaceScope(ODataReaderState.Completed);
				flag = false;
			}
			else if (isExpandedLinkContent)
			{
				this.ReadExpandedNavigationLinkEnd(false);
			}
			else
			{
				JsonNodeType jsonNodeType = nodeType;
				if (jsonNodeType != JsonNodeType.StartObject)
				{
					if (jsonNodeType != JsonNodeType.EndArray)
					{
						throw new ODataException(Strings.ODataJsonReader_CannotReadEntriesOfFeed(this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType));
					}
					this.ReadFeedEnd();
				}
				else
				{
					this.ReadEntryStart(null, this.CurrentJsonLightFeedScope.SelectedProperties);
				}
			}
			return flag;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00021D28 File Offset: 0x0001FF28
		private bool ReadAtNavigationLinkStartImplementationSynchronously()
		{
			ODataNavigationLink currentNavigationLink = base.CurrentNavigationLink;
			IODataJsonLightReaderEntryState iodataJsonLightReaderEntryState = (IODataJsonLightReaderEntryState)base.LinkParentEntityScope;
			if (this.jsonLightInputContext.ReadingResponse)
			{
				if (iodataJsonLightReaderEntryState.ProcessingMissingProjectedNavigationLinks)
				{
					this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
				}
				else if (!this.jsonLightEntryAndFeedDeserializer.JsonReader.IsOnValueNode())
				{
					ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataJsonLightReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, false, currentNavigationLink.IsCollection);
					iodataJsonLightReaderEntryState.NavigationPropertiesRead.Add(currentNavigationLink.Name);
					this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
				}
				else if (!currentNavigationLink.IsCollection.Value)
				{
					ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataJsonLightReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(false));
					this.ReadExpandedEntryStart(currentNavigationLink);
				}
				else
				{
					ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataJsonLightReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(true));
					ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo = this.CurrentJsonLightNavigationLinkScope.NavigationLinkInfo;
					ODataJsonLightReader.JsonLightEntryScope jsonLightEntryScope = (ODataJsonLightReader.JsonLightEntryScope)base.LinkParentEntityScope;
					SelectedPropertiesNode selectedProperties = jsonLightEntryScope.SelectedProperties;
					this.ReadFeedStart(navigationLinkInfo.ExpandedFeed, selectedProperties.GetSelectedPropertiesForNavigationProperty(jsonLightEntryScope.EntityType, currentNavigationLink.Name));
				}
			}
			else
			{
				ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo2 = this.CurrentJsonLightNavigationLinkScope.NavigationLinkInfo;
				ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataJsonLightReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, navigationLinkInfo2.IsExpanded, currentNavigationLink.IsCollection);
				this.ReadNextNavigationLinkContentItemInRequest();
			}
			return true;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00021E60 File Offset: 0x00020060
		private bool ReadAtNavigationLinkEndImplementationSynchronously()
		{
			base.PopScope(ODataReaderState.NavigationLinkEnd);
			IODataJsonLightReaderEntryState currentEntryState = this.CurrentEntryState;
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo;
			if (this.jsonLightInputContext.ReadingResponse && currentEntryState.ProcessingMissingProjectedNavigationLinks)
			{
				odataJsonLightReaderNavigationLinkInfo = currentEntryState.Entry.MetadataBuilder.GetNextUnprocessedNavigationLink();
			}
			else
			{
				odataJsonLightReaderNavigationLinkInfo = this.jsonLightEntryAndFeedDeserializer.ReadEntryContent(currentEntryState);
			}
			if (odataJsonLightReaderNavigationLinkInfo == null)
			{
				this.EndEntry();
			}
			else
			{
				this.StartNavigationLink(odataJsonLightReaderNavigationLinkInfo);
			}
			return true;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00021EC5 File Offset: 0x000200C5
		private bool ReadAtEntityReferenceLinkSynchronously()
		{
			base.PopScope(ODataReaderState.EntityReferenceLink);
			this.ReadNextNavigationLinkContentItemInRequest();
			return true;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00021ED5 File Offset: 0x000200D5
		private void ReadFeedStart(ODataFeed feed, SelectedPropertiesNode selectedProperties)
		{
			this.jsonLightEntryAndFeedDeserializer.ReadFeedContentStart();
			base.EnterScope(new ODataJsonLightReader.JsonLightFeedScope(feed, base.CurrentNavigationSource, base.CurrentEntityType, selectedProperties, base.CurrentScope.ODataUri));
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00021F08 File Offset: 0x00020108
		private void ReadFeedEnd()
		{
			this.jsonLightEntryAndFeedDeserializer.ReadFeedContentEnd();
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = null;
			ODataJsonLightReader.JsonLightNavigationLinkScope jsonLightNavigationLinkScope = (ODataJsonLightReader.JsonLightNavigationLinkScope)base.ExpandedLinkContentParentScope;
			if (jsonLightNavigationLinkScope != null)
			{
				odataJsonLightReaderNavigationLinkInfo = jsonLightNavigationLinkScope.NavigationLinkInfo;
			}
			if (!base.IsReadingNestedPayload)
			{
				this.jsonLightEntryAndFeedDeserializer.ReadNextLinkAnnotationAtFeedEnd(base.CurrentFeed, odataJsonLightReaderNavigationLinkInfo, this.topLevelScope.DuplicatePropertyNamesChecker);
			}
			this.ReplaceScope(ODataReaderState.FeedEnd);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00021F64 File Offset: 0x00020164
		private void ReadExpandedEntryStart(ODataNavigationLink navigationLink)
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
			{
				base.EnterScope(new ODataJsonLightReader.JsonLightEntryScope(ODataReaderState.EntryStart, null, base.CurrentNavigationSource, base.CurrentEntityType, null, null, base.CurrentScope.ODataUri));
				return;
			}
			ODataJsonLightReader.JsonLightEntryScope jsonLightEntryScope = (ODataJsonLightReader.JsonLightEntryScope)base.LinkParentEntityScope;
			SelectedPropertiesNode selectedProperties = jsonLightEntryScope.SelectedProperties;
			this.ReadEntryStart(null, selectedProperties.GetSelectedPropertiesForNavigationProperty(jsonLightEntryScope.EntityType, navigationLink.Name));
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00021FD8 File Offset: 0x000201D8
		private void ReadEntryStart(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties)
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.StartObject)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
			}
			if (base.ReadingFeed || base.IsExpandedLinkContent)
			{
				string text = this.jsonLightEntryAndFeedDeserializer.ReadContextUriAnnotation(ODataPayloadKind.Entry, duplicatePropertyNamesChecker, false);
				if (text != null)
				{
					text = UriUtils.UriToString(this.jsonLightEntryAndFeedDeserializer.ProcessUriFromPayload(text));
					ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(this.jsonLightEntryAndFeedDeserializer.Model, text, ODataPayloadKind.Entry, this.jsonLightEntryAndFeedDeserializer.MessageReaderSettings.ReaderBehavior, this.jsonLightInputContext.ReadingResponse);
					if (this.jsonLightInputContext.ReadingResponse && odataJsonLightContextUriParseResult != null)
					{
						ReaderValidationUtils.ValidateFeedOrEntryContextUri(odataJsonLightContextUriParseResult, base.CurrentScope, false);
					}
				}
			}
			this.StartEntry(duplicatePropertyNamesChecker, selectedProperties);
			this.jsonLightEntryAndFeedDeserializer.ReadEntryTypeName(this.CurrentEntryState);
			base.ApplyEntityTypeNameFromPayload(base.CurrentEntry.TypeName);
			if (base.CurrentFeedValidator != null)
			{
				base.CurrentFeedValidator.ValidateEntry(base.CurrentEntityType);
			}
			if (base.CurrentEntityType != null)
			{
				base.CurrentEntry.SetAnnotation<ODataTypeAnnotation>(new ODataTypeAnnotation(base.CurrentNavigationSource, base.CurrentEntityType));
			}
			if (this.jsonLightInputContext.UseServerApiBehavior)
			{
				this.CurrentEntryState.FirstNavigationLinkInfo = null;
				return;
			}
			this.CurrentEntryState.FirstNavigationLinkInfo = this.jsonLightEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00022128 File Offset: 0x00020328
		private void ReadExpandedNavigationLinkEnd(bool isCollection)
		{
			base.CurrentNavigationLink.IsCollection = new bool?(isCollection);
			IODataJsonLightReaderEntryState iodataJsonLightReaderEntryState = (IODataJsonLightReaderEntryState)base.LinkParentEntityScope;
			iodataJsonLightReaderEntryState.NavigationPropertiesRead.Add(base.CurrentNavigationLink.Name);
			this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00022170 File Offset: 0x00020370
		private void ReadNextNavigationLinkContentItemInRequest()
		{
			ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo = this.CurrentJsonLightNavigationLinkScope.NavigationLinkInfo;
			if (navigationLinkInfo.HasEntityReferenceLink)
			{
				base.EnterScope(new ODataReaderCore.Scope(ODataReaderState.EntityReferenceLink, navigationLinkInfo.ReportEntityReferenceLink(), null, null, base.CurrentScope.ODataUri));
				return;
			}
			if (!navigationLinkInfo.IsExpanded)
			{
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
				return;
			}
			if (navigationLinkInfo.NavigationLink.IsCollection == true)
			{
				SelectedPropertiesNode entireSubtree = SelectedPropertiesNode.EntireSubtree;
				this.ReadFeedStart(new ODataFeed(), entireSubtree);
				return;
			}
			this.ReadExpandedEntryStart(navigationLinkInfo.NavigationLink);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00022201 File Offset: 0x00020401
		private void StartEntry(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties)
		{
			base.EnterScope(new ODataJsonLightReader.JsonLightEntryScope(ODataReaderState.EntryStart, ReaderUtils.CreateNewEntry(), base.CurrentNavigationSource, base.CurrentEntityType, duplicatePropertyNamesChecker ?? this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker(), selectedProperties, base.CurrentScope.ODataUri));
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002223C File Offset: 0x0002043C
		private void StartNavigationLink(ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo)
		{
			ODataNavigationLink navigationLink = navigationLinkInfo.NavigationLink;
			IEdmNavigationProperty navigationProperty = navigationLinkInfo.NavigationProperty;
			IEdmEntityType edmEntityType = null;
			if (navigationProperty != null)
			{
				IEdmTypeReference type = navigationProperty.Type;
				edmEntityType = (type.IsCollection() ? type.AsCollection().ElementType().AsEntity()
					.EntityDefinition() : type.AsEntity().EntityDefinition());
			}
			if (this.jsonLightInputContext.ReadingResponse && !base.IsReadingNestedPayload)
			{
				ODataEntityMetadataBuilder entityMetadataBuilderForReader = this.jsonLightEntryAndFeedDeserializer.MetadataContext.GetEntityMetadataBuilderForReader(this.CurrentEntryState, this.jsonLightInputContext.MessageReaderSettings.UseKeyAsSegment);
				navigationLink.MetadataBuilder = entityMetadataBuilderForReader;
			}
			IEdmNavigationSource edmNavigationSource = ((base.CurrentNavigationSource == null || navigationProperty == null) ? null : base.CurrentNavigationSource.FindNavigationTarget(navigationProperty));
			ODataUri odataUri = null;
			if (navigationLinkInfo.NavigationLink.ContextUrl != null)
			{
				ODataPath path = ODataJsonLightContextUriParser.Parse(this.jsonLightEntryAndFeedDeserializer.Model, UriUtils.UriToString(navigationLinkInfo.NavigationLink.ContextUrl), navigationLinkInfo.NavigationLink.IsCollection.GetValueOrDefault() ? ODataPayloadKind.Feed : ODataPayloadKind.Entry, this.jsonLightEntryAndFeedDeserializer.MessageReaderSettings.ReaderBehavior, this.jsonLightEntryAndFeedDeserializer.JsonLightInputContext.ReadingResponse).Path;
				odataUri = new ODataUri
				{
					Path = path
				};
			}
			base.EnterScope(new ODataJsonLightReader.JsonLightNavigationLinkScope(navigationLinkInfo, edmNavigationSource, edmEntityType, odataUri));
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002238B File Offset: 0x0002058B
		private void ReplaceScope(ODataReaderState state)
		{
			base.ReplaceScope(new ODataReaderCore.Scope(state, this.Item, base.CurrentNavigationSource, base.CurrentEntityType, base.CurrentScope.ODataUri));
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000223B8 File Offset: 0x000205B8
		private void EndEntry()
		{
			IODataJsonLightReaderEntryState currentEntryState = this.CurrentEntryState;
			if (base.CurrentEntry != null && !base.IsReadingNestedPayload)
			{
				ODataEntityMetadataBuilder entityMetadataBuilderForReader = this.jsonLightEntryAndFeedDeserializer.MetadataContext.GetEntityMetadataBuilderForReader(this.CurrentEntryState, this.jsonLightInputContext.MessageReaderSettings.UseKeyAsSegment);
				if (entityMetadataBuilderForReader != base.CurrentEntry.MetadataBuilder)
				{
					foreach (string text in this.CurrentEntryState.NavigationPropertiesRead)
					{
						entityMetadataBuilderForReader.MarkNavigationLinkProcessed(text);
					}
					ODataConventionalEntityMetadataBuilder odataConventionalEntityMetadataBuilder = entityMetadataBuilderForReader as ODataConventionalEntityMetadataBuilder;
					if (odataConventionalEntityMetadataBuilder != null)
					{
						odataConventionalEntityMetadataBuilder.ODataUri = base.CurrentScope.ODataUri;
					}
					base.CurrentEntry.MetadataBuilder = entityMetadataBuilderForReader;
				}
			}
			this.jsonLightEntryAndFeedDeserializer.ValidateEntryMetadata(currentEntryState);
			if (this.jsonLightInputContext.ReadingResponse && base.CurrentEntry != null)
			{
				ODataJsonLightReaderNavigationLinkInfo nextUnprocessedNavigationLink = base.CurrentEntry.MetadataBuilder.GetNextUnprocessedNavigationLink();
				if (nextUnprocessedNavigationLink != null)
				{
					this.CurrentEntryState.ProcessingMissingProjectedNavigationLinks = true;
					this.StartNavigationLink(nextUnprocessedNavigationLink);
					return;
				}
			}
			base.EndEntry(new ODataJsonLightReader.JsonLightEntryScope(ODataReaderState.EntryEnd, (ODataEntry)this.Item, base.CurrentNavigationSource, base.CurrentEntityType, this.CurrentEntryState.DuplicatePropertyNamesChecker, this.CurrentEntryState.SelectedProperties, base.CurrentScope.ODataUri));
		}

		// Token: 0x040003B3 RID: 947
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x040003B4 RID: 948
		private readonly ODataJsonLightEntryAndFeedDeserializer jsonLightEntryAndFeedDeserializer;

		// Token: 0x040003B5 RID: 949
		private readonly ODataJsonLightReader.JsonLightTopLevelScope topLevelScope;

		// Token: 0x040003B6 RID: 950
		private readonly bool readingParameter;

		// Token: 0x020000F4 RID: 244
		private sealed class JsonLightTopLevelScope : ODataReaderCore.Scope
		{
			// Token: 0x06000960 RID: 2400 RVA: 0x0002251C File Offset: 0x0002071C
			internal JsonLightTopLevelScope(IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
				: base(ODataReaderState.Start, null, navigationSource, expectedEntityType, null)
			{
			}

			// Token: 0x17000203 RID: 515
			// (get) Token: 0x06000961 RID: 2401 RVA: 0x00022529 File Offset: 0x00020729
			// (set) Token: 0x06000962 RID: 2402 RVA: 0x00022531 File Offset: 0x00020731
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; set; }
		}

		// Token: 0x020000F5 RID: 245
		private sealed class JsonLightEntryScope : ODataReaderCore.Scope, IODataJsonLightReaderEntryState
		{
			// Token: 0x06000963 RID: 2403 RVA: 0x0002253A File Offset: 0x0002073A
			internal JsonLightEntryScope(ODataReaderState readerState, ODataEntry entry, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(readerState, entry, navigationSource, expectedEntityType, odataUri)
			{
				this.DuplicatePropertyNamesChecker = duplicatePropertyNamesChecker;
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06000964 RID: 2404 RVA: 0x00022559 File Offset: 0x00020759
			// (set) Token: 0x06000965 RID: 2405 RVA: 0x00022561 File Offset: 0x00020761
			public ODataEntityMetadataBuilder MetadataBuilder { get; set; }

			// Token: 0x17000205 RID: 517
			// (get) Token: 0x06000966 RID: 2406 RVA: 0x0002256A File Offset: 0x0002076A
			// (set) Token: 0x06000967 RID: 2407 RVA: 0x00022572 File Offset: 0x00020772
			public bool AnyPropertyFound { get; set; }

			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06000968 RID: 2408 RVA: 0x0002257B File Offset: 0x0002077B
			// (set) Token: 0x06000969 RID: 2409 RVA: 0x00022583 File Offset: 0x00020783
			public ODataJsonLightReaderNavigationLinkInfo FirstNavigationLinkInfo { get; set; }

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x0600096A RID: 2410 RVA: 0x0002258C File Offset: 0x0002078C
			// (set) Token: 0x0600096B RID: 2411 RVA: 0x00022594 File Offset: 0x00020794
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; private set; }

			// Token: 0x17000208 RID: 520
			// (get) Token: 0x0600096C RID: 2412 RVA: 0x0002259D File Offset: 0x0002079D
			// (set) Token: 0x0600096D RID: 2413 RVA: 0x000225A5 File Offset: 0x000207A5
			public SelectedPropertiesNode SelectedProperties { get; private set; }

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x0600096E RID: 2414 RVA: 0x000225B0 File Offset: 0x000207B0
			public List<string> NavigationPropertiesRead
			{
				get
				{
					List<string> list;
					if ((list = this.navigationPropertiesRead) == null)
					{
						list = (this.navigationPropertiesRead = new List<string>());
					}
					return list;
				}
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x0600096F RID: 2415 RVA: 0x000225D5 File Offset: 0x000207D5
			// (set) Token: 0x06000970 RID: 2416 RVA: 0x000225DD File Offset: 0x000207DD
			public bool ProcessingMissingProjectedNavigationLinks { get; set; }

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06000971 RID: 2417 RVA: 0x000225E6 File Offset: 0x000207E6
			ODataEntry IODataJsonLightReaderEntryState.Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06000972 RID: 2418 RVA: 0x000225F3 File Offset: 0x000207F3
			IEdmEntityType IODataJsonLightReaderEntryState.EntityType
			{
				get
				{
					return base.EntityType;
				}
			}

			// Token: 0x040003B8 RID: 952
			private List<string> navigationPropertiesRead;
		}

		// Token: 0x020000F6 RID: 246
		private sealed class JsonLightFeedScope : ODataReaderCore.Scope
		{
			// Token: 0x06000973 RID: 2419 RVA: 0x000225FB File Offset: 0x000207FB
			internal JsonLightFeedScope(ODataFeed feed, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataReaderState.FeedStart, feed, navigationSource, expectedEntityType, odataUri)
			{
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x06000974 RID: 2420 RVA: 0x00022611 File Offset: 0x00020811
			// (set) Token: 0x06000975 RID: 2421 RVA: 0x00022619 File Offset: 0x00020819
			public SelectedPropertiesNode SelectedProperties { get; private set; }
		}

		// Token: 0x020000F7 RID: 247
		private sealed class JsonLightNavigationLinkScope : ODataReaderCore.Scope
		{
			// Token: 0x06000976 RID: 2422 RVA: 0x00022622 File Offset: 0x00020822
			internal JsonLightNavigationLinkScope(ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, ODataUri odataUri)
				: base(ODataReaderState.NavigationLinkStart, navigationLinkInfo.NavigationLink, navigationSource, expectedEntityType, odataUri)
			{
				this.NavigationLinkInfo = navigationLinkInfo;
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x06000977 RID: 2423 RVA: 0x0002263C File Offset: 0x0002083C
			// (set) Token: 0x06000978 RID: 2424 RVA: 0x00022644 File Offset: 0x00020844
			public ODataJsonLightReaderNavigationLinkInfo NavigationLinkInfo { get; private set; }
		}
	}
}
