using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000057 RID: 87
	internal sealed class ODataAtomReader : ODataReaderCore
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000CFA1 File Offset: 0x0000B1A1
		internal ODataAtomReader(ODataAtomInputContext atomInputContext, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, bool readingFeed)
			: base(atomInputContext, readingFeed, false, null)
		{
			this.atomInputContext = atomInputContext;
			this.atomEntryAndFeedDeserializer = new ODataAtomEntryAndFeedDeserializer(atomInputContext);
			base.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Start, null, navigationSource, expectedEntityType, null));
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000CFD1 File Offset: 0x0000B1D1
		private IODataAtomReaderEntryState CurrentEntryState
		{
			get
			{
				return (IODataAtomReaderEntryState)base.CurrentScope;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000CFDE File Offset: 0x0000B1DE
		private IODataAtomReaderFeedState CurrentFeedState
		{
			get
			{
				return (IODataAtomReaderFeedState)base.CurrentScope;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000CFEB File Offset: 0x0000B1EB
		protected override bool ReadAtStartImplementation()
		{
			this.atomEntryAndFeedDeserializer.ReadPayloadStart();
			if (base.ReadingFeed)
			{
				this.ReadFeedStart();
				return true;
			}
			this.ReadEntryStart();
			return true;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000D00F File Offset: 0x0000B20F
		protected override bool ReadAtFeedStartImplementation()
		{
			if (this.atomEntryAndFeedDeserializer.XmlReader.NodeType == 15 || this.CurrentFeedState.FeedElementEmpty)
			{
				this.ReplaceScopeToFeedEnd();
			}
			else
			{
				this.ReadEntryStart();
			}
			return true;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000D044 File Offset: 0x0000B244
		protected override bool ReadAtFeedEndImplementation()
		{
			bool isTopLevel = base.IsTopLevel;
			bool flag = this.atomEntryAndFeedDeserializer.IsReaderOnInlineEndElement();
			if (!flag)
			{
				this.atomEntryAndFeedDeserializer.ReadFeedEnd();
			}
			base.PopScope(ODataReaderState.FeedEnd);
			bool flag2;
			if (isTopLevel)
			{
				this.atomEntryAndFeedDeserializer.ReadPayloadEnd();
				this.ReplaceScope(ODataReaderState.Completed);
				flag2 = false;
			}
			else
			{
				this.atomEntryAndFeedDeserializer.ReadNavigationLinkContentAfterExpansion(flag);
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		protected override bool ReadAtEntryStartImplementation()
		{
			if (base.CurrentEntry == null)
			{
				this.EndEntry();
			}
			else if (this.atomEntryAndFeedDeserializer.XmlReader.NodeType == 15 || this.CurrentEntryState.EntryElementEmpty)
			{
				this.EndEntry();
			}
			else if (this.atomInputContext.UseServerApiBehavior)
			{
				ODataAtomReaderNavigationLinkDescriptor odataAtomReaderNavigationLinkDescriptor = this.atomEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
				if (odataAtomReaderNavigationLinkDescriptor == null)
				{
					this.EndEntry();
				}
				else
				{
					this.StartNavigationLink(odataAtomReaderNavigationLinkDescriptor);
				}
			}
			else
			{
				this.StartNavigationLink(this.CurrentEntryState.FirstNavigationLinkDescriptor);
			}
			return true;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D138 File Offset: 0x0000B338
		protected override bool ReadAtEntryEndImplementation()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			bool flag = base.CurrentEntry == null;
			base.PopScope(ODataReaderState.EntryEnd);
			if (!flag)
			{
				this.atomEntryAndFeedDeserializer.ReadEntryEnd();
			}
			bool flag2 = true;
			if (isTopLevel)
			{
				this.atomEntryAndFeedDeserializer.ReadPayloadEnd();
				this.ReplaceScope(ODataReaderState.Completed);
				flag2 = false;
			}
			else if (isExpandedLinkContent)
			{
				this.atomEntryAndFeedDeserializer.ReadNavigationLinkContentAfterExpansion(flag);
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
			}
			else if (this.atomEntryAndFeedDeserializer.ReadFeedContent(this.CurrentFeedState, base.IsExpandedLinkContent))
			{
				this.ReadEntryStart();
			}
			else
			{
				this.ReplaceScopeToFeedEnd();
			}
			return flag2;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		protected override bool ReadAtNavigationLinkStartImplementation()
		{
			ODataNavigationLink currentNavigationLink = base.CurrentNavigationLink;
			IODataAtomReaderEntryState iodataAtomReaderEntryState = (IODataAtomReaderEntryState)base.LinkParentEntityScope;
			ODataAtomReader.AtomScope atomScope = (ODataAtomReader.AtomScope)base.CurrentScope;
			IEdmNavigationProperty navigationProperty = atomScope.NavigationProperty;
			if (this.atomEntryAndFeedDeserializer.XmlReader.IsEmptyElement)
			{
				this.ReadAtNonExpandedNavigationLinkStart();
			}
			else
			{
				this.atomEntryAndFeedDeserializer.XmlReader.Read();
				ODataAtomDeserializerExpandedNavigationLinkContent odataAtomDeserializerExpandedNavigationLinkContent = this.atomEntryAndFeedDeserializer.ReadNavigationLinkContentBeforeExpansion();
				if (odataAtomDeserializerExpandedNavigationLinkContent != ODataAtomDeserializerExpandedNavigationLinkContent.None && navigationProperty == null && this.atomInputContext.Model.IsUserModel() && this.atomInputContext.MessageReaderSettings.ReportUndeclaredLinkProperties)
				{
					if (this.atomInputContext.MessageReaderSettings.IgnoreUndeclaredValueProperties)
					{
						this.atomEntryAndFeedDeserializer.SkipNavigationLinkContentOnExpansion();
						this.ReadAtNonExpandedNavigationLinkStart();
						return true;
					}
					throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(currentNavigationLink.Name, base.LinkParentEntityScope.EntityType.FullTypeName()));
				}
				else
				{
					switch (odataAtomDeserializerExpandedNavigationLinkContent)
					{
					case ODataAtomDeserializerExpandedNavigationLinkContent.None:
						this.ReadAtNonExpandedNavigationLinkStart();
						break;
					case ODataAtomDeserializerExpandedNavigationLinkContent.Empty:
						if (currentNavigationLink.IsCollection == true)
						{
							ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataAtomReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(true));
							this.EnterScope(ODataReaderState.FeedStart, new ODataFeed(), base.CurrentEntityType);
							this.CurrentFeedState.FeedElementEmpty = true;
						}
						else
						{
							currentNavigationLink.IsCollection = new bool?(false);
							ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataAtomReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(false));
							this.EnterScope(ODataReaderState.EntryStart, null, base.CurrentEntityType);
						}
						break;
					case ODataAtomDeserializerExpandedNavigationLinkContent.Entry:
						if (currentNavigationLink.IsCollection == true || (navigationProperty != null && navigationProperty.Type.IsCollection()))
						{
							throw new ODataException(Strings.ODataAtomReader_ExpandedEntryInFeedNavigationLink);
						}
						currentNavigationLink.IsCollection = new bool?(false);
						ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataAtomReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(false));
						this.ReadEntryStart();
						break;
					case ODataAtomDeserializerExpandedNavigationLinkContent.Feed:
						if (currentNavigationLink.IsCollection == false)
						{
							throw new ODataException(Strings.ODataAtomReader_ExpandedFeedInEntryNavigationLink);
						}
						currentNavigationLink.IsCollection = new bool?(true);
						ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataAtomReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(true));
						this.ReadFeedStart();
						break;
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataAtomReader_ReadAtNavigationLinkStartImplementation));
					}
				}
			}
			return true;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D420 File Offset: 0x0000B620
		protected override bool ReadAtNavigationLinkEndImplementation()
		{
			this.atomEntryAndFeedDeserializer.ReadNavigationLinkEnd();
			base.PopScope(ODataReaderState.NavigationLinkEnd);
			ODataAtomReaderNavigationLinkDescriptor odataAtomReaderNavigationLinkDescriptor = this.atomEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
			if (odataAtomReaderNavigationLinkDescriptor == null)
			{
				this.EndEntry();
			}
			else
			{
				this.StartNavigationLink(odataAtomReaderNavigationLinkDescriptor);
			}
			return true;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D464 File Offset: 0x0000B664
		protected override bool ReadAtEntityReferenceLink()
		{
			base.PopScope(ODataReaderState.EntityReferenceLink);
			this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
			return true;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D478 File Offset: 0x0000B678
		private void ReadFeedStart()
		{
			ODataFeed odataFeed = new ODataFeed();
			this.atomEntryAndFeedDeserializer.ReadFeedStart();
			this.EnterScope(ODataReaderState.FeedStart, odataFeed, base.CurrentEntityType);
			if (this.atomEntryAndFeedDeserializer.XmlReader.IsEmptyElement)
			{
				this.CurrentFeedState.FeedElementEmpty = true;
				return;
			}
			this.atomEntryAndFeedDeserializer.XmlReader.Read();
			this.atomEntryAndFeedDeserializer.ReadFeedContent(this.CurrentFeedState, base.IsExpandedLinkContent);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		private void ReadEntryStart()
		{
			ODataEntry odataEntry = ReaderUtils.CreateNewEntry();
			this.atomEntryAndFeedDeserializer.ReadEntryStart(odataEntry);
			this.EnterScope(ODataReaderState.EntryStart, odataEntry, base.CurrentEntityType);
			ODataAtomReader.AtomScope atomScope = (ODataAtomReader.AtomScope)base.CurrentScope;
			atomScope.DuplicatePropertyNamesChecker = this.atomInputContext.CreateDuplicatePropertyNamesChecker();
			string text = this.atomEntryAndFeedDeserializer.FindTypeName();
			base.ApplyEntityTypeNameFromPayload(text);
			if (base.CurrentFeedValidator != null)
			{
				base.CurrentFeedValidator.ValidateEntry(base.CurrentEntityType);
			}
			if (this.atomEntryAndFeedDeserializer.XmlReader.IsEmptyElement)
			{
				this.CurrentEntryState.EntryElementEmpty = true;
				return;
			}
			this.atomEntryAndFeedDeserializer.XmlReader.Read();
			if (this.atomInputContext.UseServerApiBehavior)
			{
				this.CurrentEntryState.FirstNavigationLinkDescriptor = null;
				return;
			}
			this.CurrentEntryState.FirstNavigationLinkDescriptor = this.atomEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		private void EndEntry()
		{
			IODataAtomReaderEntryState currentEntryState = this.CurrentEntryState;
			ODataEntry entry = currentEntryState.Entry;
			if (entry != null)
			{
				if (currentEntryState.AtomEntryMetadata != null)
				{
					entry.SetAnnotation<AtomEntryMetadata>(currentEntryState.AtomEntryMetadata);
				}
				IEdmEntityType entityType = currentEntryState.EntityType;
				if (currentEntryState.MediaLinkEntry == null && entityType != null && entityType.HasStream)
				{
					ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(currentEntryState);
				}
				bool flag = this.atomInputContext.UseDefaultFormatBehavior || this.atomInputContext.UseServerFormatBehavior;
				ValidationUtils.ValidateEntryMetadataResource(entry, entityType, this.atomInputContext.Model, flag);
			}
			base.EndEntry(new ODataAtomReader.AtomScope(ODataReaderState.EntryEnd, this.Item, base.CurrentEntityType));
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D66C File Offset: 0x0000B86C
		private void StartNavigationLink(ODataAtomReaderNavigationLinkDescriptor navigationLinkDescriptor)
		{
			IEdmEntityType edmEntityType = null;
			if (navigationLinkDescriptor.NavigationProperty != null)
			{
				IEdmTypeReference type = navigationLinkDescriptor.NavigationProperty.Type;
				if (!type.IsCollection())
				{
					if (navigationLinkDescriptor.NavigationLink.IsCollection == true)
					{
						throw new ODataException(Strings.ODataAtomReader_FeedNavigationLinkForResourceReferenceProperty(navigationLinkDescriptor.NavigationLink.Name));
					}
					navigationLinkDescriptor.NavigationLink.IsCollection = new bool?(false);
					edmEntityType = type.AsEntity().EntityDefinition();
				}
				else
				{
					if (navigationLinkDescriptor.NavigationLink.IsCollection == null)
					{
						navigationLinkDescriptor.NavigationLink.IsCollection = new bool?(true);
					}
					edmEntityType = type.AsCollection().ElementType().AsEntity()
						.EntityDefinition();
				}
			}
			this.EnterScope(ODataReaderState.NavigationLinkStart, navigationLinkDescriptor.NavigationLink, edmEntityType);
			((ODataAtomReader.AtomScope)base.CurrentScope).NavigationProperty = navigationLinkDescriptor.NavigationProperty;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D750 File Offset: 0x0000B950
		private void ReadAtNonExpandedNavigationLinkStart()
		{
			ODataNavigationLink currentNavigationLink = base.CurrentNavigationLink;
			IODataAtomReaderEntryState iodataAtomReaderEntryState = (IODataAtomReaderEntryState)base.LinkParentEntityScope;
			ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataAtomReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, false, currentNavigationLink.IsCollection);
			if (!this.atomInputContext.ReadingResponse)
			{
				this.EnterScope(ODataReaderState.EntityReferenceLink, new ODataEntityReferenceLink
				{
					Url = currentNavigationLink.Url
				}, null);
				return;
			}
			ODataAtomReader.AtomScope atomScope = (ODataAtomReader.AtomScope)base.CurrentScope;
			IEdmNavigationProperty navigationProperty = atomScope.NavigationProperty;
			if (currentNavigationLink.IsCollection == false && navigationProperty != null && navigationProperty.Type.IsCollection())
			{
				throw new ODataException(Strings.ODataAtomReader_DeferredEntryInFeedNavigationLink);
			}
			this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D801 File Offset: 0x0000BA01
		private void EnterScope(ODataReaderState state, ODataItem item, IEdmEntityType expectedEntityType)
		{
			base.EnterScope(new ODataAtomReader.AtomScope(state, item, expectedEntityType));
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D811 File Offset: 0x0000BA11
		private void ReplaceScope(ODataReaderState state)
		{
			base.ReplaceScope(new ODataAtomReader.AtomScope(state, this.Item, base.CurrentEntityType));
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D82C File Offset: 0x0000BA2C
		private void ReplaceScopeToFeedEnd()
		{
			IODataAtomReaderFeedState currentFeedState = this.CurrentFeedState;
			ODataFeed currentFeed = base.CurrentFeed;
			if (this.atomInputContext.MessageReaderSettings.EnableAtomMetadataReading)
			{
				currentFeed.SetAnnotation<AtomFeedMetadata>(currentFeedState.AtomFeedMetadata);
			}
			this.ReplaceScope(ODataReaderState.FeedEnd);
		}

		// Token: 0x040001A9 RID: 425
		private readonly ODataAtomInputContext atomInputContext;

		// Token: 0x040001AA RID: 426
		private ODataAtomEntryAndFeedDeserializer atomEntryAndFeedDeserializer;

		// Token: 0x02000058 RID: 88
		private sealed class AtomScope : ODataReaderCore.Scope, IODataAtomReaderEntryState, IODataAtomReaderFeedState
		{
			// Token: 0x06000396 RID: 918 RVA: 0x0000D86C File Offset: 0x0000BA6C
			internal AtomScope(ODataReaderState state, ODataItem item, IEdmEntityType expectedEntityType)
				: base(state, item, null, expectedEntityType, null)
			{
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000397 RID: 919 RVA: 0x0000D879 File Offset: 0x0000BA79
			// (set) Token: 0x06000398 RID: 920 RVA: 0x0000D886 File Offset: 0x0000BA86
			public bool ElementEmpty
			{
				get
				{
					return (this.atomScopeState & ODataAtomReader.AtomScope.AtomScopeStateBitMask.EmptyElement) == ODataAtomReader.AtomScope.AtomScopeStateBitMask.EmptyElement;
				}
				set
				{
					if (value)
					{
						this.atomScopeState |= ODataAtomReader.AtomScope.AtomScopeStateBitMask.EmptyElement;
						return;
					}
					this.atomScopeState &= ~ODataAtomReader.AtomScope.AtomScopeStateBitMask.EmptyElement;
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06000399 RID: 921 RVA: 0x0000D8A9 File Offset: 0x0000BAA9
			// (set) Token: 0x0600039A RID: 922 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
			public bool? MediaLinkEntry
			{
				get
				{
					return this.mediaLinkEntry;
				}
				set
				{
					if (this.mediaLinkEntry != null && this.mediaLinkEntry.Value != value)
					{
						throw new ODataException(Strings.ODataAtomReader_MediaLinkEntryMismatch);
					}
					this.mediaLinkEntry = value;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x0600039B RID: 923 RVA: 0x0000D907 File Offset: 0x0000BB07
			// (set) Token: 0x0600039C RID: 924 RVA: 0x0000D90F File Offset: 0x0000BB0F
			public ODataAtomReaderNavigationLinkDescriptor FirstNavigationLinkDescriptor { get; set; }

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x0600039D RID: 925 RVA: 0x0000D918 File Offset: 0x0000BB18
			// (set) Token: 0x0600039E RID: 926 RVA: 0x0000D920 File Offset: 0x0000BB20
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; set; }

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x0600039F RID: 927 RVA: 0x0000D929 File Offset: 0x0000BB29
			// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000D931 File Offset: 0x0000BB31
			public IEdmNavigationProperty NavigationProperty { get; set; }

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000D93A File Offset: 0x0000BB3A
			ODataEntry IODataAtomReaderEntryState.Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000D947 File Offset: 0x0000BB47
			IEdmEntityType IODataAtomReaderEntryState.EntityType
			{
				get
				{
					return base.EntityType;
				}
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000D94F File Offset: 0x0000BB4F
			// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000D957 File Offset: 0x0000BB57
			bool IODataAtomReaderEntryState.EntryElementEmpty
			{
				get
				{
					return this.ElementEmpty;
				}
				set
				{
					this.ElementEmpty = value;
				}
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000D960 File Offset: 0x0000BB60
			// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000D969 File Offset: 0x0000BB69
			bool IODataAtomReaderEntryState.HasReadLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasReadLink);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasReadLink);
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000D973 File Offset: 0x0000BB73
			// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000D97C File Offset: 0x0000BB7C
			bool IODataAtomReaderEntryState.HasEditLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasEditLink);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasEditLink);
				}
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000D986 File Offset: 0x0000BB86
			// (set) Token: 0x060003AA RID: 938 RVA: 0x0000D993 File Offset: 0x0000BB93
			bool IODataAtomReaderEntryState.HasEditMediaLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasEditMediaLink);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasEditMediaLink);
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x060003AB RID: 939 RVA: 0x0000D9A1 File Offset: 0x0000BBA1
			// (set) Token: 0x060003AC RID: 940 RVA: 0x0000D9AA File Offset: 0x0000BBAA
			bool IODataAtomReaderEntryState.HasId
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasId);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasId);
				}
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x060003AD RID: 941 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
			// (set) Token: 0x060003AE RID: 942 RVA: 0x0000D9BE File Offset: 0x0000BBBE
			bool IODataAtomReaderEntryState.HasContent
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasContent);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasContent);
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x060003AF RID: 943 RVA: 0x0000D9C9 File Offset: 0x0000BBC9
			// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000D9D3 File Offset: 0x0000BBD3
			bool IODataAtomReaderEntryState.HasTypeNameCategory
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasTypeNameCategory);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasTypeNameCategory);
				}
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000D9DE File Offset: 0x0000BBDE
			// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
			bool IODataAtomReaderEntryState.HasProperties
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasProperties);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasProperties);
				}
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000D9F3 File Offset: 0x0000BBF3
			// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000DA00 File Offset: 0x0000BC00
			bool IODataAtomReaderFeedState.HasCount
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasCount);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasCount);
				}
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000DA0E File Offset: 0x0000BC0E
			// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000DA1B File Offset: 0x0000BC1B
			bool IODataAtomReaderFeedState.HasNextPageLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasNextPageLinkInFeed);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasNextPageLinkInFeed);
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000DA29 File Offset: 0x0000BC29
			// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000DA36 File Offset: 0x0000BC36
			bool IODataAtomReaderFeedState.HasReadLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasReadLinkInFeed);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasReadLinkInFeed);
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000DA44 File Offset: 0x0000BC44
			// (set) Token: 0x060003BA RID: 954 RVA: 0x0000DA51 File Offset: 0x0000BC51
			bool IODataAtomReaderFeedState.HasDeltaLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasDeltaLink);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasDeltaLink);
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x060003BB RID: 955 RVA: 0x0000DA5F File Offset: 0x0000BC5F
			AtomEntryMetadata IODataAtomReaderEntryState.AtomEntryMetadata
			{
				get
				{
					if (this.atomEntryMetadata == null)
					{
						this.atomEntryMetadata = AtomMetadataReaderUtils.CreateNewAtomEntryMetadata();
					}
					return this.atomEntryMetadata;
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x060003BC RID: 956 RVA: 0x0000DA7A File Offset: 0x0000BC7A
			AtomFeedMetadata IODataAtomReaderFeedState.AtomFeedMetadata
			{
				get
				{
					if (this.atomFeedMetadata == null)
					{
						this.atomFeedMetadata = AtomMetadataReaderUtils.CreateNewAtomFeedMetadata();
					}
					return this.atomFeedMetadata;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x060003BD RID: 957 RVA: 0x0000DA95 File Offset: 0x0000BC95
			ODataFeed IODataAtomReaderFeedState.Feed
			{
				get
				{
					return (ODataFeed)base.Item;
				}
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060003BE RID: 958 RVA: 0x0000DAA2 File Offset: 0x0000BCA2
			// (set) Token: 0x060003BF RID: 959 RVA: 0x0000DAAA File Offset: 0x0000BCAA
			bool IODataAtomReaderFeedState.FeedElementEmpty
			{
				get
				{
					return this.ElementEmpty;
				}
				set
				{
					this.ElementEmpty = value;
				}
			}

			// Token: 0x060003C0 RID: 960 RVA: 0x0000DAB3 File Offset: 0x0000BCB3
			private void SetAtomScopeState(bool value, ODataAtomReader.AtomScope.AtomScopeStateBitMask bitMask)
			{
				if (value)
				{
					this.atomScopeState |= bitMask;
					return;
				}
				this.atomScopeState &= ~bitMask;
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x0000DAD6 File Offset: 0x0000BCD6
			private bool GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask bitMask)
			{
				return (this.atomScopeState & bitMask) == bitMask;
			}

			// Token: 0x040001AB RID: 427
			private bool? mediaLinkEntry;

			// Token: 0x040001AC RID: 428
			private ODataAtomReader.AtomScope.AtomScopeStateBitMask atomScopeState;

			// Token: 0x040001AD RID: 429
			private AtomEntryMetadata atomEntryMetadata;

			// Token: 0x040001AE RID: 430
			private AtomFeedMetadata atomFeedMetadata;

			// Token: 0x02000059 RID: 89
			[Flags]
			private enum AtomScopeStateBitMask
			{
				// Token: 0x040001B3 RID: 435
				None = 0,
				// Token: 0x040001B4 RID: 436
				EmptyElement = 1,
				// Token: 0x040001B5 RID: 437
				HasReadLink = 2,
				// Token: 0x040001B6 RID: 438
				HasEditLink = 4,
				// Token: 0x040001B7 RID: 439
				HasId = 8,
				// Token: 0x040001B8 RID: 440
				HasContent = 16,
				// Token: 0x040001B9 RID: 441
				HasTypeNameCategory = 32,
				// Token: 0x040001BA RID: 442
				HasProperties = 64,
				// Token: 0x040001BB RID: 443
				HasCount = 128,
				// Token: 0x040001BC RID: 444
				HasNextPageLinkInFeed = 256,
				// Token: 0x040001BD RID: 445
				HasReadLinkInFeed = 512,
				// Token: 0x040001BE RID: 446
				HasEditMediaLink = 1024,
				// Token: 0x040001BF RID: 447
				HasDeltaLink = 2048
			}
		}
	}
}
