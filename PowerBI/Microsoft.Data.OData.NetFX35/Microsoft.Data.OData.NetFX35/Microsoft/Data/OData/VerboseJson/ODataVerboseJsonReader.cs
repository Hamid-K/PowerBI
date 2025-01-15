using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x0200020C RID: 524
	internal sealed class ODataVerboseJsonReader : ODataReaderCore
	{
		// Token: 0x06000F20 RID: 3872 RVA: 0x000378F0 File Offset: 0x00035AF0
		internal ODataVerboseJsonReader(ODataVerboseJsonInputContext verboseJsonInputContext, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType, bool readingFeed, IODataReaderWriterListener listener)
			: base(verboseJsonInputContext, readingFeed, listener)
		{
			this.verboseJsonInputContext = verboseJsonInputContext;
			this.verboseJsonEntryAndFeedDeserializer = new ODataVerboseJsonEntryAndFeedDeserializer(verboseJsonInputContext);
			if (!this.verboseJsonInputContext.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonReader_ParsingWithoutMetadata);
			}
			base.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Start, null, entitySet, expectedEntityType));
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00037947 File Offset: 0x00035B47
		private IODataVerboseJsonReaderEntryState CurrentEntryState
		{
			get
			{
				return (IODataVerboseJsonReaderEntryState)base.CurrentScope;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00037954 File Offset: 0x00035B54
		private ODataVerboseJsonReader.JsonScope CurrentJsonScope
		{
			get
			{
				return (ODataVerboseJsonReader.JsonScope)base.CurrentScope;
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00037961 File Offset: 0x00035B61
		protected override bool ReadAtStartImplementation()
		{
			this.verboseJsonEntryAndFeedDeserializer.ReadPayloadStart(base.IsReadingNestedPayload);
			if (base.ReadingFeed)
			{
				this.ReadFeedStart(false);
				return true;
			}
			this.ReadEntryStart();
			return true;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003798C File Offset: 0x00035B8C
		protected override bool ReadAtFeedStartImplementation()
		{
			JsonNodeType nodeType = this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType;
			if (nodeType != JsonNodeType.StartObject)
			{
				if (nodeType != JsonNodeType.EndArray)
				{
					throw new ODataException(Strings.ODataJsonReader_CannotReadEntriesOfFeed(this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType));
				}
				this.verboseJsonEntryAndFeedDeserializer.ReadFeedEnd(base.CurrentFeed, this.CurrentJsonScope.FeedHasResultsWrapper, base.IsExpandedLinkContent);
				this.ReplaceScope(ODataReaderState.FeedEnd);
			}
			else
			{
				this.ReadEntryStart();
			}
			return true;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00037A08 File Offset: 0x00035C08
		protected override bool ReadAtFeedEndImplementation()
		{
			bool isTopLevel = base.IsTopLevel;
			base.PopScope(ODataReaderState.FeedEnd);
			bool flag;
			if (isTopLevel)
			{
				this.verboseJsonEntryAndFeedDeserializer.JsonReader.Read();
				this.verboseJsonEntryAndFeedDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
				this.ReplaceScope(ODataReaderState.Completed);
				flag = false;
			}
			else
			{
				if (this.verboseJsonInputContext.ReadingResponse)
				{
					this.verboseJsonEntryAndFeedDeserializer.JsonReader.Read();
					this.ReadExpandedNavigationLinkEnd(true);
				}
				else
				{
					this.ReadExpandedCollectionNavigationLinkContentInRequest();
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00037A84 File Offset: 0x00035C84
		protected override bool ReadAtEntryStartImplementation()
		{
			if (base.CurrentEntry == null)
			{
				this.EndEntry();
			}
			else if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.EndObject)
			{
				this.EndEntry();
			}
			else if (this.verboseJsonInputContext.UseServerApiBehavior)
			{
				IEdmNavigationProperty edmNavigationProperty;
				ODataNavigationLink odataNavigationLink = this.verboseJsonEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState, out edmNavigationProperty);
				if (odataNavigationLink != null)
				{
					this.StartNavigationLink(odataNavigationLink, edmNavigationProperty);
				}
				else
				{
					this.EndEntry();
				}
			}
			else
			{
				this.StartNavigationLink(this.CurrentEntryState.FirstNavigationLink, this.CurrentEntryState.FirstNavigationProperty);
			}
			return true;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00037B10 File Offset: 0x00035D10
		protected override bool ReadAtEntryEndImplementation()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			base.PopScope(ODataReaderState.EntryEnd);
			this.verboseJsonEntryAndFeedDeserializer.JsonReader.Read();
			JsonNodeType nodeType = this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType;
			bool flag = true;
			if (isTopLevel)
			{
				this.verboseJsonEntryAndFeedDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
				this.ReplaceScope(ODataReaderState.Completed);
				flag = false;
			}
			else if (isExpandedLinkContent)
			{
				this.ReadExpandedNavigationLinkEnd(false);
			}
			else if (this.CurrentJsonScope.FeedInExpandedNavigationLinkInRequest)
			{
				this.ReadExpandedCollectionNavigationLinkContentInRequest();
			}
			else
			{
				JsonNodeType jsonNodeType = nodeType;
				if (jsonNodeType != JsonNodeType.StartObject)
				{
					if (jsonNodeType != JsonNodeType.EndArray)
					{
						throw new ODataException(Strings.ODataJsonReader_CannotReadEntriesOfFeed(this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType));
					}
					this.verboseJsonEntryAndFeedDeserializer.ReadFeedEnd(base.CurrentFeed, this.CurrentJsonScope.FeedHasResultsWrapper, base.IsExpandedLinkContent);
					this.ReplaceScope(ODataReaderState.FeedEnd);
				}
				else
				{
					this.ReadEntryStart();
				}
			}
			return flag;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00037C00 File Offset: 0x00035E00
		protected override bool ReadAtNavigationLinkStartImplementation()
		{
			ODataNavigationLink currentNavigationLink = base.CurrentNavigationLink;
			IODataVerboseJsonReaderEntryState iodataVerboseJsonReaderEntryState = (IODataVerboseJsonReaderEntryState)base.LinkParentEntityScope;
			if (this.verboseJsonInputContext.ReadingResponse && this.verboseJsonEntryAndFeedDeserializer.IsDeferredLink(true))
			{
				ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataVerboseJsonReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, false, currentNavigationLink.IsCollection);
				this.verboseJsonEntryAndFeedDeserializer.ReadDeferredNavigationLink(currentNavigationLink);
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
			}
			else if (!currentNavigationLink.IsCollection.Value)
			{
				if (!this.verboseJsonInputContext.ReadingResponse && this.verboseJsonEntryAndFeedDeserializer.IsEntityReferenceLink())
				{
					ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataVerboseJsonReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, false, new bool?(false));
					ODataEntityReferenceLink odataEntityReferenceLink = this.verboseJsonEntryAndFeedDeserializer.ReadEntityReferenceLink();
					this.EnterScope(ODataReaderState.EntityReferenceLink, odataEntityReferenceLink, null);
				}
				else
				{
					ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataVerboseJsonReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(false));
					if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
					{
						this.EnterScope(ODataReaderState.EntryStart, null, base.CurrentEntityType);
					}
					else
					{
						this.ReadEntryStart();
					}
				}
			}
			else
			{
				ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataVerboseJsonReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(true));
				if (this.verboseJsonInputContext.ReadingResponse)
				{
					this.ReadFeedStart(true);
				}
				else
				{
					if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType != JsonNodeType.StartObject && this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType != JsonNodeType.StartArray)
					{
						throw new ODataException(Strings.ODataJsonReader_CannotReadFeedStart(this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType));
					}
					bool flag = this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.StartObject;
					this.verboseJsonEntryAndFeedDeserializer.ReadFeedStart(new ODataFeed(), flag, true);
					this.CurrentJsonScope.FeedHasResultsWrapper = flag;
					this.ReadExpandedCollectionNavigationLinkContentInRequest();
				}
			}
			return true;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00037DB0 File Offset: 0x00035FB0
		protected override bool ReadAtNavigationLinkEndImplementation()
		{
			base.PopScope(ODataReaderState.NavigationLinkEnd);
			IEdmNavigationProperty edmNavigationProperty;
			ODataNavigationLink odataNavigationLink = this.verboseJsonEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState, out edmNavigationProperty);
			if (odataNavigationLink == null)
			{
				this.EndEntry();
			}
			else
			{
				this.StartNavigationLink(odataNavigationLink, edmNavigationProperty);
			}
			return true;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00037DEC File Offset: 0x00035FEC
		protected override bool ReadAtEntityReferenceLink()
		{
			base.PopScope(ODataReaderState.EntityReferenceLink);
			if (base.CurrentNavigationLink.IsCollection == true)
			{
				this.ReadExpandedCollectionNavigationLinkContentInRequest();
			}
			else
			{
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
			}
			return true;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00037E34 File Offset: 0x00036034
		private void ReadFeedStart(bool isExpandedLinkContent)
		{
			ODataFeed odataFeed = new ODataFeed();
			if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType != JsonNodeType.StartObject && this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonReader_CannotReadFeedStart(this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType));
			}
			bool flag = this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.StartObject;
			this.verboseJsonEntryAndFeedDeserializer.ReadFeedStart(odataFeed, flag, isExpandedLinkContent);
			this.EnterScope(ODataReaderState.FeedStart, odataFeed, base.CurrentEntityType);
			this.CurrentJsonScope.FeedHasResultsWrapper = flag;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00037ECC File Offset: 0x000360CC
		private void ReadExpandedCollectionNavigationLinkContentInRequest()
		{
			if (this.verboseJsonEntryAndFeedDeserializer.IsEntityReferenceLink())
			{
				if (this.State == ODataReaderState.FeedStart)
				{
					this.ReplaceScope(ODataReaderState.FeedEnd);
					return;
				}
				this.CurrentJsonScope.ExpandedNavigationLinkInRequestHasContent = true;
				ODataEntityReferenceLink odataEntityReferenceLink = this.verboseJsonEntryAndFeedDeserializer.ReadEntityReferenceLink();
				this.EnterScope(ODataReaderState.EntityReferenceLink, odataEntityReferenceLink, null);
				return;
			}
			else if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.EndArray || this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.EndObject)
			{
				if (this.State == ODataReaderState.FeedStart)
				{
					this.verboseJsonEntryAndFeedDeserializer.ReadFeedEnd(base.CurrentFeed, this.CurrentJsonScope.FeedHasResultsWrapper, true);
					this.ReplaceScope(ODataReaderState.FeedEnd);
					return;
				}
				if (!this.CurrentJsonScope.ExpandedNavigationLinkInRequestHasContent)
				{
					this.CurrentJsonScope.ExpandedNavigationLinkInRequestHasContent = true;
					this.EnterScope(ODataReaderState.FeedStart, new ODataFeed(), base.CurrentEntityType);
					this.CurrentJsonScope.FeedInExpandedNavigationLinkInRequest = true;
					return;
				}
				if (this.CurrentJsonScope.FeedHasResultsWrapper)
				{
					ODataFeed odataFeed = new ODataFeed();
					this.verboseJsonEntryAndFeedDeserializer.ReadFeedEnd(odataFeed, true, true);
				}
				this.verboseJsonEntryAndFeedDeserializer.JsonReader.Read();
				this.ReadExpandedNavigationLinkEnd(true);
				return;
			}
			else
			{
				if (this.State != ODataReaderState.FeedStart)
				{
					this.CurrentJsonScope.ExpandedNavigationLinkInRequestHasContent = true;
					this.EnterScope(ODataReaderState.FeedStart, new ODataFeed(), base.CurrentEntityType);
					this.CurrentJsonScope.FeedInExpandedNavigationLinkInRequest = true;
					return;
				}
				if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonReader_CannotReadEntriesOfFeed(this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType));
				}
				this.ReadEntryStart();
				return;
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00038050 File Offset: 0x00036250
		private void ReadEntryStart()
		{
			this.verboseJsonEntryAndFeedDeserializer.ReadEntryStart();
			this.StartEntry();
			this.ReadEntryMetadata();
			if (this.verboseJsonInputContext.UseServerApiBehavior)
			{
				this.CurrentEntryState.FirstNavigationLink = null;
				this.CurrentEntryState.FirstNavigationProperty = null;
				return;
			}
			IEdmNavigationProperty edmNavigationProperty;
			this.CurrentEntryState.FirstNavigationLink = this.verboseJsonEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState, out edmNavigationProperty);
			this.CurrentEntryState.FirstNavigationProperty = edmNavigationProperty;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x000380C4 File Offset: 0x000362C4
		private void ReadEntryMetadata()
		{
			this.verboseJsonEntryAndFeedDeserializer.JsonReader.StartBuffering();
			bool flag = false;
			while (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = this.verboseJsonEntryAndFeedDeserializer.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal(text, "__metadata") == 0)
				{
					flag = true;
					break;
				}
				this.verboseJsonEntryAndFeedDeserializer.JsonReader.SkipValue();
			}
			string text2 = null;
			object obj = null;
			if (flag)
			{
				obj = this.verboseJsonEntryAndFeedDeserializer.JsonReader.BookmarkCurrentPosition();
				text2 = this.verboseJsonEntryAndFeedDeserializer.ReadTypeNameFromMetadataPropertyValue();
			}
			base.ApplyEntityTypeNameFromPayload(text2);
			if (base.CurrentFeedValidator != null)
			{
				base.CurrentFeedValidator.ValidateEntry(base.CurrentEntityType);
			}
			if (flag)
			{
				this.verboseJsonEntryAndFeedDeserializer.JsonReader.MoveToBookmark(obj);
				this.verboseJsonEntryAndFeedDeserializer.ReadEntryMetadataPropertyValue(this.CurrentEntryState);
			}
			this.verboseJsonEntryAndFeedDeserializer.JsonReader.StopBuffering();
			this.verboseJsonEntryAndFeedDeserializer.ValidateEntryMetadata(this.CurrentEntryState);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000381B4 File Offset: 0x000363B4
		private void ReadExpandedNavigationLinkEnd(bool isCollection)
		{
			base.CurrentNavigationLink.IsCollection = new bool?(isCollection);
			this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000381CE File Offset: 0x000363CE
		private void StartEntry()
		{
			this.EnterScope(ODataReaderState.EntryStart, ReaderUtils.CreateNewEntry(), base.CurrentEntityType);
			this.CurrentJsonScope.DuplicatePropertyNamesChecker = this.verboseJsonInputContext.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000381F8 File Offset: 0x000363F8
		private void StartNavigationLink(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			IEdmEntityType edmEntityType = null;
			if (navigationProperty != null)
			{
				IEdmTypeReference type = navigationProperty.Type;
				edmEntityType = (type.IsCollection() ? type.AsCollection().ElementType().AsEntity()
					.EntityDefinition() : type.AsEntity().EntityDefinition());
			}
			this.EnterScope(ODataReaderState.NavigationLinkStart, navigationLink, edmEntityType);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00038245 File Offset: 0x00036445
		private void EnterScope(ODataReaderState state, ODataItem item, IEdmEntityType expectedEntityType)
		{
			base.EnterScope(new ODataVerboseJsonReader.JsonScope(state, item, expectedEntityType));
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00038255 File Offset: 0x00036455
		private void ReplaceScope(ODataReaderState state)
		{
			base.ReplaceScope(new ODataVerboseJsonReader.JsonScope(state, this.Item, base.CurrentEntityType));
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003826F File Offset: 0x0003646F
		private void EndEntry()
		{
			base.EndEntry(new ODataVerboseJsonReader.JsonScope(ODataReaderState.EntryEnd, this.Item, base.CurrentEntityType));
		}

		// Token: 0x040005E4 RID: 1508
		private readonly ODataVerboseJsonInputContext verboseJsonInputContext;

		// Token: 0x040005E5 RID: 1509
		private readonly ODataVerboseJsonEntryAndFeedDeserializer verboseJsonEntryAndFeedDeserializer;

		// Token: 0x0200020E RID: 526
		private sealed class JsonScope : ODataReaderCore.Scope, IODataVerboseJsonReaderEntryState
		{
			// Token: 0x06000F3E RID: 3902 RVA: 0x00038289 File Offset: 0x00036489
			internal JsonScope(ODataReaderState state, ODataItem item, IEdmEntityType expectedEntityType)
				: base(state, item, null, expectedEntityType)
			{
			}

			// Token: 0x1700035C RID: 860
			// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00038295 File Offset: 0x00036495
			// (set) Token: 0x06000F40 RID: 3904 RVA: 0x0003829D File Offset: 0x0003649D
			public bool MetadataPropertyFound { get; set; }

			// Token: 0x1700035D RID: 861
			// (get) Token: 0x06000F41 RID: 3905 RVA: 0x000382A6 File Offset: 0x000364A6
			// (set) Token: 0x06000F42 RID: 3906 RVA: 0x000382AE File Offset: 0x000364AE
			public ODataNavigationLink FirstNavigationLink { get; set; }

			// Token: 0x1700035E RID: 862
			// (get) Token: 0x06000F43 RID: 3907 RVA: 0x000382B7 File Offset: 0x000364B7
			// (set) Token: 0x06000F44 RID: 3908 RVA: 0x000382BF File Offset: 0x000364BF
			public IEdmNavigationProperty FirstNavigationProperty { get; set; }

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000382C8 File Offset: 0x000364C8
			// (set) Token: 0x06000F46 RID: 3910 RVA: 0x000382D0 File Offset: 0x000364D0
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; set; }

			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06000F47 RID: 3911 RVA: 0x000382D9 File Offset: 0x000364D9
			// (set) Token: 0x06000F48 RID: 3912 RVA: 0x000382E1 File Offset: 0x000364E1
			public bool FeedInExpandedNavigationLinkInRequest { get; set; }

			// Token: 0x17000361 RID: 865
			// (get) Token: 0x06000F49 RID: 3913 RVA: 0x000382EA File Offset: 0x000364EA
			// (set) Token: 0x06000F4A RID: 3914 RVA: 0x000382F2 File Offset: 0x000364F2
			public bool FeedHasResultsWrapper { get; set; }

			// Token: 0x17000362 RID: 866
			// (get) Token: 0x06000F4B RID: 3915 RVA: 0x000382FB File Offset: 0x000364FB
			// (set) Token: 0x06000F4C RID: 3916 RVA: 0x00038303 File Offset: 0x00036503
			public bool ExpandedNavigationLinkInRequestHasContent { get; set; }

			// Token: 0x17000363 RID: 867
			// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0003830C File Offset: 0x0003650C
			ODataEntry IODataVerboseJsonReaderEntryState.Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x17000364 RID: 868
			// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00038319 File Offset: 0x00036519
			IEdmEntityType IODataVerboseJsonReaderEntryState.EntityType
			{
				get
				{
					return base.EntityType;
				}
			}
		}
	}
}
