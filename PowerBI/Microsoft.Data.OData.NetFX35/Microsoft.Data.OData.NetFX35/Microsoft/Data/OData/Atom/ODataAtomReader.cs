using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200022A RID: 554
	internal sealed class ODataAtomReader : ODataReaderCore
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x0003E398 File Offset: 0x0003C598
		internal ODataAtomReader(ODataAtomInputContext atomInputContext, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType, bool readingFeed)
			: base(atomInputContext, readingFeed, null)
		{
			this.atomInputContext = atomInputContext;
			this.atomEntryAndFeedDeserializer = new ODataAtomEntryAndFeedDeserializer(atomInputContext);
			if (this.atomInputContext.MessageReaderSettings.AtomEntryXmlCustomizationCallback != null)
			{
				this.atomInputContext.InitializeReaderCustomization();
				this.atomEntryAndFeedDeserializersStack = new Stack<ODataAtomEntryAndFeedDeserializer>();
				this.atomEntryAndFeedDeserializersStack.Push(this.atomEntryAndFeedDeserializer);
			}
			base.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Start, null, entitySet, expectedEntityType));
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x0003E40A File Offset: 0x0003C60A
		private IODataAtomReaderEntryState CurrentEntryState
		{
			get
			{
				return (IODataAtomReaderEntryState)base.CurrentScope;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0003E417 File Offset: 0x0003C617
		private IODataAtomReaderFeedState CurrentFeedState
		{
			get
			{
				return (IODataAtomReaderFeedState)base.CurrentScope;
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0003E424 File Offset: 0x0003C624
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

		// Token: 0x0600108F RID: 4239 RVA: 0x0003E448 File Offset: 0x0003C648
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

		// Token: 0x06001090 RID: 4240 RVA: 0x0003E47C File Offset: 0x0003C67C
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

		// Token: 0x06001091 RID: 4241 RVA: 0x0003E4E4 File Offset: 0x0003C6E4
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

		// Token: 0x06001092 RID: 4242 RVA: 0x0003E570 File Offset: 0x0003C770
		protected override bool ReadAtEntryEndImplementation()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			bool flag = base.CurrentEntry == null;
			base.PopScope(ODataReaderState.EntryEnd);
			if (!flag)
			{
				bool flag2 = false;
				if (this.atomInputContext.MessageReaderSettings.AtomEntryXmlCustomizationCallback != null)
				{
					XmlReader xmlReader = this.atomInputContext.PopCustomReader();
					if (!object.ReferenceEquals(this.atomInputContext.XmlReader, xmlReader))
					{
						flag2 = true;
						this.atomEntryAndFeedDeserializersStack.Pop();
						this.atomEntryAndFeedDeserializer = this.atomEntryAndFeedDeserializersStack.Peek();
					}
				}
				if (!flag2)
				{
					this.atomEntryAndFeedDeserializer.ReadEntryEnd();
				}
			}
			bool flag3 = true;
			if (isTopLevel)
			{
				this.atomEntryAndFeedDeserializer.ReadPayloadEnd();
				this.ReplaceScope(ODataReaderState.Completed);
				flag3 = false;
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
			return flag3;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0003E660 File Offset: 0x0003C860
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
				if (odataAtomDeserializerExpandedNavigationLinkContent != ODataAtomDeserializerExpandedNavigationLinkContent.None && navigationProperty == null && this.atomInputContext.Model.IsUserModel() && this.atomInputContext.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
				{
					if (this.atomInputContext.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty))
					{
						this.atomEntryAndFeedDeserializer.SkipNavigationLinkContentOnExpansion();
						this.ReadAtNonExpandedNavigationLinkStart();
						return true;
					}
					throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(currentNavigationLink.Name, base.LinkParentEntityScope.EntityType.ODataFullName()));
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

		// Token: 0x06001094 RID: 4244 RVA: 0x0003E8B4 File Offset: 0x0003CAB4
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

		// Token: 0x06001095 RID: 4245 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
		protected override bool ReadAtEntityReferenceLink()
		{
			base.PopScope(ODataReaderState.EntityReferenceLink);
			this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
			return true;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0003E90C File Offset: 0x0003CB0C
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

		// Token: 0x06001097 RID: 4247 RVA: 0x0003E980 File Offset: 0x0003CB80
		private void ReadEntryStart()
		{
			ODataEntry odataEntry = ReaderUtils.CreateNewEntry();
			if (this.atomInputContext.MessageReaderSettings.AtomEntryXmlCustomizationCallback != null)
			{
				this.atomEntryAndFeedDeserializer.VerifyEntryStart();
				Uri xmlBaseUri = this.atomInputContext.XmlReader.XmlBaseUri;
				XmlReader xmlReader = this.atomInputContext.MessageReaderSettings.AtomEntryXmlCustomizationCallback.Invoke(odataEntry, this.atomInputContext.XmlReader, this.atomInputContext.XmlReader.ParentXmlBaseUri);
				if (xmlReader != null)
				{
					if (object.ReferenceEquals(this.atomInputContext.XmlReader, xmlReader))
					{
						throw new ODataException(Strings.ODataAtomReader_EntryXmlCustomizationCallbackReturnedSameInstance);
					}
					this.atomInputContext.PushCustomReader(xmlReader, xmlBaseUri);
					this.atomEntryAndFeedDeserializer = new ODataAtomEntryAndFeedDeserializer(this.atomInputContext);
					this.atomEntryAndFeedDeserializersStack.Push(this.atomEntryAndFeedDeserializer);
				}
				else
				{
					this.atomInputContext.PushCustomReader(this.atomInputContext.XmlReader, null);
				}
			}
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
			ODataEntityPropertyMappingCache odataEntityPropertyMappingCache = this.atomInputContext.Model.EnsureEpmCache(this.CurrentEntryState.EntityType, int.MaxValue);
			if (odataEntityPropertyMappingCache != null)
			{
				atomScope.CachedEpm = odataEntityPropertyMappingCache;
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

		// Token: 0x06001098 RID: 4248 RVA: 0x0003EB58 File Offset: 0x0003CD58
		private void EndEntry()
		{
			IODataAtomReaderEntryState currentEntryState = this.CurrentEntryState;
			ODataEntry entry = currentEntryState.Entry;
			if (entry != null)
			{
				if (currentEntryState.CachedEpm != null)
				{
					ODataAtomReader.AtomScope atomScope = (ODataAtomReader.AtomScope)base.CurrentScope;
					if (atomScope.HasAtomEntryMetadata)
					{
						EpmSyndicationReader.ReadEntryEpm(currentEntryState, this.atomInputContext);
					}
					if (atomScope.HasEpmCustomReaderValueCache)
					{
						EpmCustomReader.ReadEntryEpm(currentEntryState, this.atomInputContext);
					}
				}
				if (currentEntryState.AtomEntryMetadata != null)
				{
					entry.SetAnnotation<AtomEntryMetadata>(currentEntryState.AtomEntryMetadata);
				}
				IEdmEntityType entityType = currentEntryState.EntityType;
				if (currentEntryState.MediaLinkEntry == null && entityType != null && this.atomInputContext.Model.HasDefaultStream(entityType))
				{
					ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(currentEntryState, true);
				}
				bool flag = this.atomInputContext.UseDefaultFormatBehavior || this.atomInputContext.UseServerFormatBehavior;
				ValidationUtils.ValidateEntryMetadataResource(entry, entityType, this.atomInputContext.Model, flag);
			}
			base.EndEntry(new ODataAtomReader.AtomScope(ODataReaderState.EntryEnd, this.Item, base.CurrentEntityType));
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0003EC48 File Offset: 0x0003CE48
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

		// Token: 0x0600109A RID: 4250 RVA: 0x0003ED2C File Offset: 0x0003CF2C
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

		// Token: 0x0600109B RID: 4251 RVA: 0x0003EDDD File Offset: 0x0003CFDD
		private void EnterScope(ODataReaderState state, ODataItem item, IEdmEntityType expectedEntityType)
		{
			base.EnterScope(new ODataAtomReader.AtomScope(state, item, expectedEntityType));
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0003EDED File Offset: 0x0003CFED
		private void ReplaceScope(ODataReaderState state)
		{
			base.ReplaceScope(new ODataAtomReader.AtomScope(state, this.Item, base.CurrentEntityType));
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0003EE08 File Offset: 0x0003D008
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

		// Token: 0x04000660 RID: 1632
		private readonly ODataAtomInputContext atomInputContext;

		// Token: 0x04000661 RID: 1633
		private ODataAtomEntryAndFeedDeserializer atomEntryAndFeedDeserializer;

		// Token: 0x04000662 RID: 1634
		private Stack<ODataAtomEntryAndFeedDeserializer> atomEntryAndFeedDeserializersStack;

		// Token: 0x0200022B RID: 555
		private sealed class AtomScope : ODataReaderCore.Scope, IODataAtomReaderEntryState, IODataAtomReaderFeedState
		{
			// Token: 0x0600109E RID: 4254 RVA: 0x0003EE48 File Offset: 0x0003D048
			internal AtomScope(ODataReaderState state, ODataItem item, IEdmEntityType expectedEntityType)
				: base(state, item, null, expectedEntityType)
			{
			}

			// Token: 0x170003BB RID: 955
			// (get) Token: 0x0600109F RID: 4255 RVA: 0x0003EE54 File Offset: 0x0003D054
			// (set) Token: 0x060010A0 RID: 4256 RVA: 0x0003EE61 File Offset: 0x0003D061
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

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0003EE84 File Offset: 0x0003D084
			// (set) Token: 0x060010A2 RID: 4258 RVA: 0x0003EE8C File Offset: 0x0003D08C
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

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0003EEDF File Offset: 0x0003D0DF
			// (set) Token: 0x060010A4 RID: 4260 RVA: 0x0003EEE7 File Offset: 0x0003D0E7
			public ODataAtomReaderNavigationLinkDescriptor FirstNavigationLinkDescriptor { get; set; }

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0003EEF0 File Offset: 0x0003D0F0
			// (set) Token: 0x060010A6 RID: 4262 RVA: 0x0003EEF8 File Offset: 0x0003D0F8
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; set; }

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0003EF01 File Offset: 0x0003D101
			// (set) Token: 0x060010A8 RID: 4264 RVA: 0x0003EF09 File Offset: 0x0003D109
			public ODataEntityPropertyMappingCache CachedEpm { get; set; }

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0003EF12 File Offset: 0x0003D112
			public bool HasEpmCustomReaderValueCache
			{
				get
				{
					return this.epmCustomReaderValueCache != null;
				}
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x060010AA RID: 4266 RVA: 0x0003EF20 File Offset: 0x0003D120
			public bool HasAtomEntryMetadata
			{
				get
				{
					return this.atomEntryMetadata != null;
				}
			}

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x060010AB RID: 4267 RVA: 0x0003EF2E File Offset: 0x0003D12E
			// (set) Token: 0x060010AC RID: 4268 RVA: 0x0003EF36 File Offset: 0x0003D136
			public IEdmNavigationProperty NavigationProperty { get; set; }

			// Token: 0x170003C3 RID: 963
			// (get) Token: 0x060010AD RID: 4269 RVA: 0x0003EF3F File Offset: 0x0003D13F
			ODataEntry IODataAtomReaderEntryState.Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x060010AE RID: 4270 RVA: 0x0003EF4C File Offset: 0x0003D14C
			IEdmEntityType IODataAtomReaderEntryState.EntityType
			{
				get
				{
					return base.EntityType;
				}
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x060010AF RID: 4271 RVA: 0x0003EF54 File Offset: 0x0003D154
			// (set) Token: 0x060010B0 RID: 4272 RVA: 0x0003EF5C File Offset: 0x0003D15C
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

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0003EF65 File Offset: 0x0003D165
			// (set) Token: 0x060010B2 RID: 4274 RVA: 0x0003EF6E File Offset: 0x0003D16E
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

			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0003EF78 File Offset: 0x0003D178
			// (set) Token: 0x060010B4 RID: 4276 RVA: 0x0003EF81 File Offset: 0x0003D181
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

			// Token: 0x170003C8 RID: 968
			// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0003EF8B File Offset: 0x0003D18B
			// (set) Token: 0x060010B6 RID: 4278 RVA: 0x0003EF98 File Offset: 0x0003D198
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

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0003EFA6 File Offset: 0x0003D1A6
			// (set) Token: 0x060010B8 RID: 4280 RVA: 0x0003EFAF File Offset: 0x0003D1AF
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

			// Token: 0x170003CA RID: 970
			// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0003EFB9 File Offset: 0x0003D1B9
			// (set) Token: 0x060010BA RID: 4282 RVA: 0x0003EFC3 File Offset: 0x0003D1C3
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

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x060010BB RID: 4283 RVA: 0x0003EFCE File Offset: 0x0003D1CE
			// (set) Token: 0x060010BC RID: 4284 RVA: 0x0003EFD8 File Offset: 0x0003D1D8
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

			// Token: 0x170003CC RID: 972
			// (get) Token: 0x060010BD RID: 4285 RVA: 0x0003EFE3 File Offset: 0x0003D1E3
			// (set) Token: 0x060010BE RID: 4286 RVA: 0x0003EFED File Offset: 0x0003D1ED
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

			// Token: 0x170003CD RID: 973
			// (get) Token: 0x060010BF RID: 4287 RVA: 0x0003EFF8 File Offset: 0x0003D1F8
			// (set) Token: 0x060010C0 RID: 4288 RVA: 0x0003F005 File Offset: 0x0003D205
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

			// Token: 0x170003CE RID: 974
			// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0003F013 File Offset: 0x0003D213
			// (set) Token: 0x060010C2 RID: 4290 RVA: 0x0003F020 File Offset: 0x0003D220
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

			// Token: 0x170003CF RID: 975
			// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0003F02E File Offset: 0x0003D22E
			// (set) Token: 0x060010C4 RID: 4292 RVA: 0x0003F03B File Offset: 0x0003D23B
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

			// Token: 0x170003D0 RID: 976
			// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0003F049 File Offset: 0x0003D249
			// (set) Token: 0x060010C6 RID: 4294 RVA: 0x0003F056 File Offset: 0x0003D256
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

			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0003F064 File Offset: 0x0003D264
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

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0003F080 File Offset: 0x0003D280
			EpmCustomReaderValueCache IODataAtomReaderEntryState.EpmCustomReaderValueCache
			{
				get
				{
					EpmCustomReaderValueCache epmCustomReaderValueCache;
					if ((epmCustomReaderValueCache = this.epmCustomReaderValueCache) == null)
					{
						epmCustomReaderValueCache = (this.epmCustomReaderValueCache = new EpmCustomReaderValueCache());
					}
					return epmCustomReaderValueCache;
				}
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0003F0A5 File Offset: 0x0003D2A5
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

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x060010CA RID: 4298 RVA: 0x0003F0C0 File Offset: 0x0003D2C0
			ODataFeed IODataAtomReaderFeedState.Feed
			{
				get
				{
					return (ODataFeed)base.Item;
				}
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x060010CB RID: 4299 RVA: 0x0003F0CD File Offset: 0x0003D2CD
			// (set) Token: 0x060010CC RID: 4300 RVA: 0x0003F0D5 File Offset: 0x0003D2D5
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

			// Token: 0x060010CD RID: 4301 RVA: 0x0003F0DE File Offset: 0x0003D2DE
			private void SetAtomScopeState(bool value, ODataAtomReader.AtomScope.AtomScopeStateBitMask bitMask)
			{
				if (value)
				{
					this.atomScopeState |= bitMask;
					return;
				}
				this.atomScopeState &= ~bitMask;
			}

			// Token: 0x060010CE RID: 4302 RVA: 0x0003F101 File Offset: 0x0003D301
			private bool GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask bitMask)
			{
				return (this.atomScopeState & bitMask) == bitMask;
			}

			// Token: 0x04000663 RID: 1635
			private bool? mediaLinkEntry;

			// Token: 0x04000664 RID: 1636
			private ODataAtomReader.AtomScope.AtomScopeStateBitMask atomScopeState;

			// Token: 0x04000665 RID: 1637
			private AtomEntryMetadata atomEntryMetadata;

			// Token: 0x04000666 RID: 1638
			private AtomFeedMetadata atomFeedMetadata;

			// Token: 0x04000667 RID: 1639
			private EpmCustomReaderValueCache epmCustomReaderValueCache;

			// Token: 0x0200022C RID: 556
			[Flags]
			private enum AtomScopeStateBitMask
			{
				// Token: 0x0400066D RID: 1645
				None = 0,
				// Token: 0x0400066E RID: 1646
				EmptyElement = 1,
				// Token: 0x0400066F RID: 1647
				HasReadLink = 2,
				// Token: 0x04000670 RID: 1648
				HasEditLink = 4,
				// Token: 0x04000671 RID: 1649
				HasId = 8,
				// Token: 0x04000672 RID: 1650
				HasContent = 16,
				// Token: 0x04000673 RID: 1651
				HasTypeNameCategory = 32,
				// Token: 0x04000674 RID: 1652
				HasProperties = 64,
				// Token: 0x04000675 RID: 1653
				HasCount = 128,
				// Token: 0x04000676 RID: 1654
				HasNextPageLinkInFeed = 256,
				// Token: 0x04000677 RID: 1655
				HasReadLinkInFeed = 512,
				// Token: 0x04000678 RID: 1656
				HasEditMediaLink = 1024,
				// Token: 0x04000679 RID: 1657
				HasDeltaLink = 2048
			}
		}
	}
}
