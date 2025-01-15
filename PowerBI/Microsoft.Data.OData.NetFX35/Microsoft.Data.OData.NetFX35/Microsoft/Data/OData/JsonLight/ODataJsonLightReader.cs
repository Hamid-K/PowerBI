using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200019C RID: 412
	internal sealed class ODataJsonLightReader : ODataReaderCoreAsync
	{
		// Token: 0x06000BD8 RID: 3032 RVA: 0x000294AD File Offset: 0x000276AD
		internal ODataJsonLightReader(ODataJsonLightInputContext jsonLightInputContext, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType, bool readingFeed, IODataReaderWriterListener listener)
			: base(jsonLightInputContext, readingFeed, listener)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightEntryAndFeedDeserializer = new ODataJsonLightEntryAndFeedDeserializer(jsonLightInputContext);
			this.topLevelScope = new ODataJsonLightReader.JsonLightTopLevelScope(entitySet, expectedEntityType);
			base.EnterScope(this.topLevelScope);
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x000294E6 File Offset: 0x000276E6
		private IODataJsonLightReaderEntryState CurrentEntryState
		{
			get
			{
				return (IODataJsonLightReaderEntryState)base.CurrentScope;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x000294F3 File Offset: 0x000276F3
		private ODataJsonLightReader.JsonLightFeedScope CurrentJsonLightFeedScope
		{
			get
			{
				return (ODataJsonLightReader.JsonLightFeedScope)base.CurrentScope;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00029500 File Offset: 0x00027700
		private ODataJsonLightReader.JsonLightNavigationLinkScope CurrentJsonLightNavigationLinkScope
		{
			get
			{
				return (ODataJsonLightReader.JsonLightNavigationLinkScope)base.CurrentScope;
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00029510 File Offset: 0x00027710
		protected override bool ReadAtStartImplementation()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			ODataPayloadKind odataPayloadKind = (base.ReadingFeed ? ODataPayloadKind.Feed : ODataPayloadKind.Entry);
			this.jsonLightEntryAndFeedDeserializer.ReadPayloadStart(odataPayloadKind, duplicatePropertyNamesChecker, base.IsReadingNestedPayload, false);
			return this.ReadAtStartImplementationSynchronously(duplicatePropertyNamesChecker);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00029551 File Offset: 0x00027751
		protected override bool ReadAtFeedStartImplementation()
		{
			return this.ReadAtFeedStartImplementationSynchronously();
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00029559 File Offset: 0x00027759
		protected override bool ReadAtFeedEndImplementation()
		{
			return this.ReadAtFeedEndImplementationSynchronously();
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00029561 File Offset: 0x00027761
		protected override bool ReadAtEntryStartImplementation()
		{
			return this.ReadAtEntryStartImplementationSynchronously();
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00029569 File Offset: 0x00027769
		protected override bool ReadAtEntryEndImplementation()
		{
			return this.ReadAtEntryEndImplementationSynchronously();
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00029571 File Offset: 0x00027771
		protected override bool ReadAtNavigationLinkStartImplementation()
		{
			return this.ReadAtNavigationLinkStartImplementationSynchronously();
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00029579 File Offset: 0x00027779
		protected override bool ReadAtNavigationLinkEndImplementation()
		{
			return this.ReadAtNavigationLinkEndImplementationSynchronously();
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00029581 File Offset: 0x00027781
		protected override bool ReadAtEntityReferenceLink()
		{
			return this.ReadAtEntityReferenceLinkSynchronously();
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002958C File Offset: 0x0002778C
		private bool ReadAtStartImplementationSynchronously(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (this.jsonLightInputContext.ReadingResponse)
			{
				ReaderValidationUtils.ValidateFeedOrEntryMetadataUri(this.jsonLightEntryAndFeedDeserializer.MetadataUriParseResult, base.CurrentScope);
			}
			string text = ((this.jsonLightEntryAndFeedDeserializer.MetadataUriParseResult == null) ? null : this.jsonLightEntryAndFeedDeserializer.MetadataUriParseResult.SelectQueryOption);
			SelectedPropertiesNode selectedPropertiesNode = SelectedPropertiesNode.Create(text);
			if (base.ReadingFeed)
			{
				ODataFeed odataFeed = new ODataFeed();
				this.topLevelScope.DuplicatePropertyNamesChecker = duplicatePropertyNamesChecker;
				bool flag = this.jsonLightInputContext.JsonReader is ReorderingJsonReader;
				this.jsonLightEntryAndFeedDeserializer.ReadTopLevelFeedAnnotations(odataFeed, duplicatePropertyNamesChecker, true, flag);
				this.ReadFeedStart(odataFeed, selectedPropertiesNode);
				return true;
			}
			this.ReadEntryStart(duplicatePropertyNamesChecker, selectedPropertiesNode);
			return true;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00029634 File Offset: 0x00027834
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

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002969C File Offset: 0x0002789C
		private bool ReadAtFeedEndImplementationSynchronously()
		{
			bool isTopLevel = base.IsTopLevel;
			base.PopScope(ODataReaderState.FeedEnd);
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

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000296F0 File Offset: 0x000278F0
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

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00029768 File Offset: 0x00027968
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

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00029828 File Offset: 0x00027A28
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

		// Token: 0x06000BEA RID: 3050 RVA: 0x00029960 File Offset: 0x00027B60
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

		// Token: 0x06000BEB RID: 3051 RVA: 0x000299C5 File Offset: 0x00027BC5
		private bool ReadAtEntityReferenceLinkSynchronously()
		{
			base.PopScope(ODataReaderState.EntityReferenceLink);
			this.ReadNextNavigationLinkContentItemInRequest();
			return true;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000299D5 File Offset: 0x00027BD5
		private void ReadFeedStart(ODataFeed feed, SelectedPropertiesNode selectedProperties)
		{
			this.jsonLightEntryAndFeedDeserializer.ReadFeedContentStart();
			base.EnterScope(new ODataJsonLightReader.JsonLightFeedScope(feed, base.CurrentEntitySet, base.CurrentEntityType, selectedProperties));
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000299FC File Offset: 0x00027BFC
		private void ReadFeedEnd()
		{
			this.jsonLightEntryAndFeedDeserializer.ReadFeedContentEnd();
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = null;
			ODataJsonLightReader.JsonLightNavigationLinkScope jsonLightNavigationLinkScope = (ODataJsonLightReader.JsonLightNavigationLinkScope)base.ExpandedLinkContentParentScope;
			if (jsonLightNavigationLinkScope != null)
			{
				odataJsonLightReaderNavigationLinkInfo = jsonLightNavigationLinkScope.NavigationLinkInfo;
			}
			this.jsonLightEntryAndFeedDeserializer.ReadNextLinkAnnotationAtFeedEnd(base.CurrentFeed, odataJsonLightReaderNavigationLinkInfo, this.topLevelScope.DuplicatePropertyNamesChecker);
			this.ReplaceScope(ODataReaderState.FeedEnd);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00029A50 File Offset: 0x00027C50
		private void ReadExpandedEntryStart(ODataNavigationLink navigationLink)
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
			{
				base.EnterScope(new ODataJsonLightReader.JsonLightEntryScope(ODataReaderState.EntryStart, null, base.CurrentEntitySet, base.CurrentEntityType, null, null));
				return;
			}
			ODataJsonLightReader.JsonLightEntryScope jsonLightEntryScope = (ODataJsonLightReader.JsonLightEntryScope)base.LinkParentEntityScope;
			SelectedPropertiesNode selectedProperties = jsonLightEntryScope.SelectedProperties;
			this.ReadEntryStart(null, selectedProperties.GetSelectedPropertiesForNavigationProperty(jsonLightEntryScope.EntityType, navigationLink.Name));
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00029AB8 File Offset: 0x00027CB8
		private void ReadEntryStart(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties)
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.StartObject)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
			}
			this.StartEntry(duplicatePropertyNamesChecker, selectedProperties);
			if (this.jsonLightInputContext.JsonReader.NodeType == JsonNodeType.Property)
			{
				this.jsonLightEntryAndFeedDeserializer.ApplyAnnotationGroupIfPresent(this.CurrentEntryState);
			}
			this.jsonLightEntryAndFeedDeserializer.ReadEntryTypeName(this.CurrentEntryState);
			base.ApplyEntityTypeNameFromPayload(base.CurrentEntry.TypeName);
			if (base.CurrentFeedValidator != null)
			{
				base.CurrentFeedValidator.ValidateEntry(base.CurrentEntityType);
			}
			if (base.CurrentEntityType != null)
			{
				base.CurrentEntry.SetAnnotation<ODataTypeAnnotation>(new ODataTypeAnnotation(base.CurrentEntitySet, base.CurrentEntityType));
			}
			if (this.jsonLightInputContext.UseServerApiBehavior)
			{
				this.CurrentEntryState.FirstNavigationLinkInfo = null;
				return;
			}
			this.CurrentEntryState.FirstNavigationLinkInfo = this.jsonLightEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00029BAC File Offset: 0x00027DAC
		private void ReadExpandedNavigationLinkEnd(bool isCollection)
		{
			base.CurrentNavigationLink.IsCollection = new bool?(isCollection);
			IODataJsonLightReaderEntryState iodataJsonLightReaderEntryState = (IODataJsonLightReaderEntryState)base.LinkParentEntityScope;
			iodataJsonLightReaderEntryState.NavigationPropertiesRead.Add(base.CurrentNavigationLink.Name);
			this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00029BF4 File Offset: 0x00027DF4
		private void ReadNextNavigationLinkContentItemInRequest()
		{
			ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo = this.CurrentJsonLightNavigationLinkScope.NavigationLinkInfo;
			if (navigationLinkInfo.HasEntityReferenceLink)
			{
				base.EnterScope(new ODataReaderCore.Scope(ODataReaderState.EntityReferenceLink, navigationLinkInfo.ReportEntityReferenceLink(), null, null));
				return;
			}
			if (!navigationLinkInfo.IsExpanded)
			{
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
				return;
			}
			if (navigationLinkInfo.NavigationLink.IsCollection == true)
			{
				SelectedPropertiesNode selectedPropertiesNode = SelectedPropertiesNode.Create(null);
				this.ReadFeedStart(new ODataFeed(), selectedPropertiesNode);
				return;
			}
			this.ReadExpandedEntryStart(navigationLinkInfo.NavigationLink);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00029C7B File Offset: 0x00027E7B
		private void StartEntry(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties)
		{
			base.EnterScope(new ODataJsonLightReader.JsonLightEntryScope(ODataReaderState.EntryStart, ReaderUtils.CreateNewEntry(), base.CurrentEntitySet, base.CurrentEntityType, duplicatePropertyNamesChecker ?? this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker(), selectedProperties));
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00029CAC File Offset: 0x00027EAC
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
			if (this.jsonLightInputContext.ReadingResponse)
			{
				ODataAssociationLink odataAssociationLink = new ODataAssociationLink
				{
					Name = navigationLink.Name
				};
				if (navigationLink.AssociationLinkUrl != null)
				{
					odataAssociationLink.Url = navigationLink.AssociationLinkUrl;
				}
				base.CurrentEntry.AddAssociationLink(odataAssociationLink);
				ODataEntityMetadataBuilder entityMetadataBuilderForReader = this.jsonLightEntryAndFeedDeserializer.MetadataContext.GetEntityMetadataBuilderForReader(this.CurrentEntryState);
				navigationLink.SetMetadataBuilder(entityMetadataBuilderForReader);
				odataAssociationLink.SetMetadataBuilder(entityMetadataBuilderForReader);
			}
			IEdmEntitySet edmEntitySet = ((navigationProperty == null) ? null : base.CurrentEntitySet.FindNavigationTarget(navigationProperty));
			base.EnterScope(new ODataJsonLightReader.JsonLightNavigationLinkScope(navigationLinkInfo, edmEntitySet, edmEntityType));
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00029D97 File Offset: 0x00027F97
		private void ReplaceScope(ODataReaderState state)
		{
			base.ReplaceScope(new ODataReaderCore.Scope(state, this.Item, base.CurrentEntitySet, base.CurrentEntityType));
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00029DB8 File Offset: 0x00027FB8
		private void EndEntry()
		{
			IODataJsonLightReaderEntryState currentEntryState = this.CurrentEntryState;
			if (currentEntryState.DuplicatePropertyNamesChecker != null)
			{
				foreach (string text in currentEntryState.DuplicatePropertyNamesChecker.GetAllUnprocessedProperties())
				{
					currentEntryState.AnyPropertyFound = true;
					ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = this.jsonLightEntryAndFeedDeserializer.ReadEntryPropertyWithoutValue(currentEntryState, text);
					currentEntryState.DuplicatePropertyNamesChecker.MarkPropertyAsProcessed(text);
					if (odataJsonLightReaderNavigationLinkInfo != null)
					{
						this.StartNavigationLink(odataJsonLightReaderNavigationLinkInfo);
						return;
					}
				}
			}
			if (base.CurrentEntry != null)
			{
				ODataEntityMetadataBuilder entityMetadataBuilderForReader = this.jsonLightEntryAndFeedDeserializer.MetadataContext.GetEntityMetadataBuilderForReader(this.CurrentEntryState);
				if (entityMetadataBuilderForReader != base.CurrentEntry.MetadataBuilder)
				{
					foreach (string text2 in this.CurrentEntryState.NavigationPropertiesRead)
					{
						entityMetadataBuilderForReader.MarkNavigationLinkProcessed(text2);
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
			base.EndEntry(new ODataJsonLightReader.JsonLightEntryScope(ODataReaderState.EntryEnd, (ODataEntry)this.Item, base.CurrentEntitySet, base.CurrentEntityType, this.CurrentEntryState.DuplicatePropertyNamesChecker, this.CurrentEntryState.SelectedProperties));
		}

		// Token: 0x04000440 RID: 1088
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000441 RID: 1089
		private readonly ODataJsonLightEntryAndFeedDeserializer jsonLightEntryAndFeedDeserializer;

		// Token: 0x04000442 RID: 1090
		private readonly ODataJsonLightReader.JsonLightTopLevelScope topLevelScope;

		// Token: 0x0200019D RID: 413
		private sealed class JsonLightTopLevelScope : ODataReaderCore.Scope
		{
			// Token: 0x06000BF6 RID: 3062 RVA: 0x00029F50 File Offset: 0x00028150
			internal JsonLightTopLevelScope(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
				: base(ODataReaderState.Start, null, entitySet, expectedEntityType)
			{
			}

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00029F5C File Offset: 0x0002815C
			// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x00029F64 File Offset: 0x00028164
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; set; }
		}

		// Token: 0x0200019E RID: 414
		private sealed class JsonLightEntryScope : ODataReaderCore.Scope, IODataJsonLightReaderEntryState
		{
			// Token: 0x06000BF9 RID: 3065 RVA: 0x00029F6D File Offset: 0x0002816D
			internal JsonLightEntryScope(ODataReaderState readerState, ODataEntry entry, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties)
				: base(readerState, entry, entitySet, expectedEntityType)
			{
				this.DuplicatePropertyNamesChecker = duplicatePropertyNamesChecker;
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x170002B6 RID: 694
			// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00029F8A File Offset: 0x0002818A
			// (set) Token: 0x06000BFB RID: 3067 RVA: 0x00029F92 File Offset: 0x00028192
			public ODataEntityMetadataBuilder MetadataBuilder { get; set; }

			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00029F9B File Offset: 0x0002819B
			// (set) Token: 0x06000BFD RID: 3069 RVA: 0x00029FA3 File Offset: 0x000281A3
			public bool AnyPropertyFound { get; set; }

			// Token: 0x170002B8 RID: 696
			// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00029FAC File Offset: 0x000281AC
			// (set) Token: 0x06000BFF RID: 3071 RVA: 0x00029FB4 File Offset: 0x000281B4
			public ODataJsonLightReaderNavigationLinkInfo FirstNavigationLinkInfo { get; set; }

			// Token: 0x170002B9 RID: 697
			// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00029FBD File Offset: 0x000281BD
			// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00029FC5 File Offset: 0x000281C5
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; private set; }

			// Token: 0x170002BA RID: 698
			// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00029FCE File Offset: 0x000281CE
			// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00029FD6 File Offset: 0x000281D6
			public SelectedPropertiesNode SelectedProperties { get; private set; }

			// Token: 0x170002BB RID: 699
			// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00029FE0 File Offset: 0x000281E0
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

			// Token: 0x170002BC RID: 700
			// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002A005 File Offset: 0x00028205
			// (set) Token: 0x06000C06 RID: 3078 RVA: 0x0002A00D File Offset: 0x0002820D
			public bool ProcessingMissingProjectedNavigationLinks { get; set; }

			// Token: 0x170002BD RID: 701
			// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002A016 File Offset: 0x00028216
			ODataEntry IODataJsonLightReaderEntryState.Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x170002BE RID: 702
			// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002A023 File Offset: 0x00028223
			IEdmEntityType IODataJsonLightReaderEntryState.EntityType
			{
				get
				{
					return base.EntityType;
				}
			}

			// Token: 0x04000444 RID: 1092
			private List<string> navigationPropertiesRead;
		}

		// Token: 0x0200019F RID: 415
		private sealed class JsonLightFeedScope : ODataReaderCore.Scope
		{
			// Token: 0x06000C09 RID: 3081 RVA: 0x0002A02B File Offset: 0x0002822B
			internal JsonLightFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType, SelectedPropertiesNode selectedProperties)
				: base(ODataReaderState.FeedStart, feed, entitySet, expectedEntityType)
			{
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x170002BF RID: 703
			// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0002A03F File Offset: 0x0002823F
			// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0002A047 File Offset: 0x00028247
			public SelectedPropertiesNode SelectedProperties { get; private set; }
		}

		// Token: 0x020001A0 RID: 416
		private sealed class JsonLightNavigationLinkScope : ODataReaderCore.Scope
		{
			// Token: 0x06000C0C RID: 3084 RVA: 0x0002A050 File Offset: 0x00028250
			internal JsonLightNavigationLinkScope(ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
				: base(ODataReaderState.NavigationLinkStart, navigationLinkInfo.NavigationLink, entitySet, expectedEntityType)
			{
				this.NavigationLinkInfo = navigationLinkInfo;
			}

			// Token: 0x170002C0 RID: 704
			// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0002A068 File Offset: 0x00028268
			// (set) Token: 0x06000C0E RID: 3086 RVA: 0x0002A070 File Offset: 0x00028270
			public ODataJsonLightReaderNavigationLinkInfo NavigationLinkInfo { get; private set; }
		}
	}
}
