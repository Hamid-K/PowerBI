using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000C4 RID: 196
	internal sealed class ODataJsonLightDeltaReader : ODataDeltaReader
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x00019575 File Offset: 0x00017775
		public ODataJsonLightDeltaReader(ODataJsonLightInputContext jsonLightInputContext, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightEntryAndFeedDeserializer = new ODataJsonLightEntryAndFeedDeserializer(jsonLightInputContext);
			this.topLevelScope = new ODataJsonLightDeltaReader.JsonLightTopLevelScope(navigationSource, expectedEntityType);
			this.EnterScope(this.topLevelScope);
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x000195B4 File Offset: 0x000177B4
		public override ODataDeltaReaderState State
		{
			get
			{
				this.jsonLightInputContext.VerifyNotDisposed();
				return this.CurrentScope.State;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x000195CC File Offset: 0x000177CC
		public override ODataReaderState SubState
		{
			get
			{
				this.jsonLightInputContext.VerifyNotDisposed();
				if (this.State != ODataDeltaReaderState.ExpandedNavigationProperty)
				{
					return ODataReaderState.Start;
				}
				return this.CurrentJsonLightExpandedNavigationPropertyScope.SubState;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x000195F0 File Offset: 0x000177F0
		public override ODataItem Item
		{
			get
			{
				this.jsonLightInputContext.VerifyNotDisposed();
				if (this.State != ODataDeltaReaderState.ExpandedNavigationProperty)
				{
					return this.CurrentScope.Item;
				}
				return this.CurrentJsonLightExpandedNavigationPropertyScope.Item;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001961E File Offset: 0x0001781E
		private bool IsTopLevel
		{
			get
			{
				return this.scopes.Count <= 2;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00019631 File Offset: 0x00017831
		private ODataJsonLightDeltaReader.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00019640 File Offset: 0x00017840
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001965F File Offset: 0x0001785F
		private IEdmEntityType CurrentEntityType
		{
			get
			{
				return this.scopes.Peek().EntityType;
			}
			set
			{
				this.scopes.Peek().EntityType = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00019674 File Offset: 0x00017874
		private IEdmNavigationSource CurrentNavigationSource
		{
			get
			{
				return this.scopes.Peek().NavigationSource;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00019693 File Offset: 0x00017893
		private IODataJsonLightReaderEntryState CurrentDeltaEntryState
		{
			get
			{
				return (IODataJsonLightReaderEntryState)this.CurrentScope;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x000196A0 File Offset: 0x000178A0
		private ODataJsonLightDeltaReader.JsonLightExpandedNavigationPropertyScope CurrentJsonLightExpandedNavigationPropertyScope
		{
			get
			{
				return (ODataJsonLightDeltaReader.JsonLightExpandedNavigationPropertyScope)this.CurrentScope;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x000196AD File Offset: 0x000178AD
		private ODataJsonLightDeltaReader.JsonLightDeltaFeedScope CurrentJsonLightDeltaFeedScope
		{
			get
			{
				return (ODataJsonLightDeltaReader.JsonLightDeltaFeedScope)this.CurrentScope;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000196BA File Offset: 0x000178BA
		private ODataEntry CurrentDeltaEntry
		{
			get
			{
				return (ODataEntry)this.Item;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x000196C7 File Offset: 0x000178C7
		private ODataDeltaDeletedEntry CurrentDeltaDeletedEntry
		{
			get
			{
				return (ODataDeltaDeletedEntry)this.Item;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x000196D4 File Offset: 0x000178D4
		private ODataDeltaLink CurrentDeltaLink
		{
			get
			{
				return (ODataDeltaLink)this.Item;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x000196E1 File Offset: 0x000178E1
		private ODataDeltaDeletedLink CurrentDeltaDeletedLink
		{
			get
			{
				return (ODataDeltaDeletedLink)this.Item;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000196EE File Offset: 0x000178EE
		private ODataDeltaFeed CurrentDeltaFeed
		{
			get
			{
				return (ODataDeltaFeed)this.Item;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000196FB File Offset: 0x000178FB
		public override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00019716 File Offset: 0x00017916
		private void EnterScope(ODataJsonLightDeltaReader.Scope scope)
		{
			this.scopes.Push(scope);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00019724 File Offset: 0x00017924
		private void ReplaceScope(ODataJsonLightDeltaReader.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00019739 File Offset: 0x00017939
		private void ReplaceScope(ODataDeltaReaderState state)
		{
			this.ReplaceScope(new ODataJsonLightDeltaReader.Scope(state, this.Item, this.CurrentNavigationSource, this.CurrentEntityType, this.CurrentScope.ODataUri));
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00019764 File Offset: 0x00017964
		private void PopScope(ODataDeltaReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00019772 File Offset: 0x00017972
		private void VerifyCanRead(bool synchronousCall)
		{
			this.jsonLightInputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataDeltaReaderState.Exception || this.State == ODataDeltaReaderState.Completed)
			{
				throw new ODataException(Strings.ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000197AF File Offset: 0x000179AF
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.jsonLightInputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000197CC File Offset: 0x000179CC
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		private T InterceptException<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action.Invoke();
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					this.EnterScope(ODataJsonLightDeltaReader.CreateExceptionScope());
				}
				throw;
			}
			return t;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001980C File Offset: 0x00017A0C
		private Uri ReadUriValue()
		{
			return new Uri(this.ReadStringValue(), 0);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001981A File Offset: 0x00017A1A
		private string ReadStringValue()
		{
			return this.jsonLightEntryAndFeedDeserializer.JsonReader.ReadStringValue();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001982C File Offset: 0x00017A2C
		private bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00019834 File Offset: 0x00017A34
		private bool ReadImplementation()
		{
			bool flag;
			switch (this.State)
			{
			case ODataDeltaReaderState.Start:
				flag = this.ReadAtStartImplementation();
				break;
			case ODataDeltaReaderState.DeltaFeedStart:
				flag = this.ReadAtDeltaFeedStartImplementation();
				break;
			case ODataDeltaReaderState.FeedEnd:
				flag = this.ReadAtFeedEndImplementation();
				break;
			case ODataDeltaReaderState.DeltaEntryStart:
				flag = this.ReadAtDeltaEntryStartImplementation();
				break;
			case ODataDeltaReaderState.DeltaEntryEnd:
				flag = this.ReadAtDeltaEntryEndImplementation();
				break;
			case ODataDeltaReaderState.DeltaDeletedEntry:
			case ODataDeltaReaderState.DeltaLink:
			case ODataDeltaReaderState.DeltaDeletedLink:
				this.scopes.Pop();
				flag = this.ReadAtDeltaFeedStartImplementation();
				break;
			case ODataDeltaReaderState.Exception:
			case ODataDeltaReaderState.Completed:
				throw new ODataException(Strings.ODataReaderCore_NoReadCallsAllowed(this.State));
			case ODataDeltaReaderState.ExpandedNavigationProperty:
				flag = this.ReadAtExpandedNavigationPropertyImplementation();
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataReaderCore_ReadImplementation));
			}
			return flag;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000198F0 File Offset: 0x00017AF0
		private void PreReadAtStartImplementation(out DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00019900 File Offset: 0x00017B00
		private bool ReadAtStartImplementation()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
			this.PreReadAtStartImplementation(out duplicatePropertyNamesChecker);
			this.jsonLightEntryAndFeedDeserializer.ReadPayloadStart(ODataPayloadKind.Delta, duplicatePropertyNamesChecker, false, false);
			return this.ReadAtStartImplementationSynchronously(duplicatePropertyNamesChecker);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001992C File Offset: 0x00017B2C
		private bool ReadAtDeltaFeedStartImplementation()
		{
			return this.ReadAtDeltaFeedStartImplementationSynchronously();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00019934 File Offset: 0x00017B34
		private bool ReadAtFeedEndImplementation()
		{
			return this.ReadAtFeedEndImplementationSynchronously();
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001993C File Offset: 0x00017B3C
		private bool ReadAtDeltaEntryStartImplementation()
		{
			return this.ReadAtDeltaEntryStartImplementationSynchronously();
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00019944 File Offset: 0x00017B44
		private bool ReadAtDeltaEntryEndImplementation()
		{
			return this.ReadAtDeltaEntryEndImplementationSynchronously();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001994C File Offset: 0x00017B4C
		private bool ReadAtExpandedNavigationPropertyImplementation()
		{
			return this.ReadAtExpandedNavigationPropertyImplementationSynchronously();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00019954 File Offset: 0x00017B54
		private bool ReadAtStartImplementationSynchronously(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			string text = ((this.jsonLightEntryAndFeedDeserializer.ContextUriParseResult == null) ? null : this.jsonLightEntryAndFeedDeserializer.ContextUriParseResult.SelectQueryOption);
			SelectedPropertiesNode selectedPropertiesNode = SelectedPropertiesNode.Create(text);
			this.topLevelScope.DuplicatePropertyNamesChecker = duplicatePropertyNamesChecker;
			bool flag = this.jsonLightInputContext.JsonReader is ReorderingJsonReader;
			ODataDeltaFeed odataDeltaFeed = new ODataDeltaFeed();
			this.jsonLightEntryAndFeedDeserializer.ReadTopLevelFeedAnnotations(odataDeltaFeed, duplicatePropertyNamesChecker, true, flag);
			this.ReadDeltaFeedStart(odataDeltaFeed, selectedPropertiesNode);
			return true;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000199C8 File Offset: 0x00017BC8
		private bool ReadAtDeltaFeedStartImplementationSynchronously()
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
				this.ReadDeltaStart(null, this.CurrentJsonLightDeltaFeedScope.SelectedProperties);
			}
			return true;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00019A2D File Offset: 0x00017C2D
		private bool ReadAtFeedEndImplementationSynchronously()
		{
			this.PopScope(ODataDeltaReaderState.FeedEnd);
			this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
			this.jsonLightEntryAndFeedDeserializer.ReadPayloadEnd(false);
			this.ReplaceScope(ODataDeltaReaderState.Completed);
			return false;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00019A5C File Offset: 0x00017C5C
		private bool ReadAtDeltaEntryStartImplementationSynchronously()
		{
			this.jsonLightEntryAndFeedDeserializer.ReadEntryTypeName(this.CurrentDeltaEntryState);
			this.ReadDeltaEntryId();
			this.ApplyEntityTypeNameFromPayload(this.CurrentDeltaEntry.TypeName);
			if (this.CurrentEntityType != null)
			{
				this.CurrentDeltaEntry.SetAnnotation<ODataTypeAnnotation>(new ODataTypeAnnotation(this.CurrentNavigationSource, this.CurrentEntityType));
			}
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo;
			do
			{
				odataJsonLightReaderNavigationLinkInfo = this.jsonLightEntryAndFeedDeserializer.ReadEntryContent(this.CurrentDeltaEntryState);
				if (odataJsonLightReaderNavigationLinkInfo == null)
				{
					goto IL_0094;
				}
			}
			while (!odataJsonLightReaderNavigationLinkInfo.IsExpanded);
			this.EnterScope(new ODataJsonLightDeltaReader.JsonLightExpandedNavigationPropertyScope(odataJsonLightReaderNavigationLinkInfo, this.CurrentNavigationSource, this.CurrentEntityType, this.CurrentScope.ODataUri, this.jsonLightInputContext));
			return true;
			IL_0094:
			this.EndDeltaEntry(new ODataJsonLightDeltaReader.JsonLightDeltaEntryScope(ODataDeltaReaderState.DeltaEntryEnd, this.Item, this.CurrentNavigationSource, this.CurrentEntityType, this.CurrentDeltaEntryState.DuplicatePropertyNamesChecker, this.CurrentDeltaEntryState.SelectedProperties, this.CurrentScope.ODataUri));
			return true;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00019B3D File Offset: 0x00017D3D
		private void EndDeltaEntry(ODataJsonLightDeltaReader.Scope scope)
		{
			this.PopScope(ODataDeltaReaderState.DeltaEntryStart);
			this.EnterScope(scope);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00019B4D File Offset: 0x00017D4D
		private bool ReadAtDeltaEntryEndImplementationSynchronously()
		{
			this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
			this.PopScope(ODataDeltaReaderState.DeltaEntryEnd);
			return this.ReadAtDeltaFeedStartImplementationSynchronously();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00019B6D File Offset: 0x00017D6D
		private bool ReadAtExpandedNavigationPropertyImplementationSynchronously()
		{
			if (this.SubState == ODataReaderState.Completed)
			{
				this.PopScope(ODataDeltaReaderState.ExpandedNavigationProperty);
				return true;
			}
			if (this.SubState == ODataReaderState.Exception)
			{
				this.EnterScope(ODataJsonLightDeltaReader.CreateExceptionScope());
				return false;
			}
			this.CurrentJsonLightExpandedNavigationPropertyScope.ExpandedNavigationPropertyReader.Read();
			return true;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00019BAB File Offset: 0x00017DAB
		private void ReadAtDeltaDeletedEntryImplementationSynchronously()
		{
			this.ReadDeltaDeletedEntryId();
			this.ReadDeltaDeletedEntryReason();
			this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00019BCA File Offset: 0x00017DCA
		private void ReadAtDeltaLinkImplementationSynchronously()
		{
			this.ReadDeltaLinkSource();
			this.ReadDeltaLinkRelationship();
			this.ReadDeltaLinkTarget();
			this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00019BEF File Offset: 0x00017DEF
		private void ReadAtDeltaDeletedLinkImplementationSynchronously()
		{
			this.ReadDeltaDeletedLinkSource();
			this.ReadDeltaDeletedLinkRelationship();
			this.ReadDeltaDeletedLinkTarget();
			this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00019C14 File Offset: 0x00017E14
		private void ReadDeltaFeedStart(ODataDeltaFeed feed, SelectedPropertiesNode selectedProperties)
		{
			this.jsonLightEntryAndFeedDeserializer.ReadFeedContentStart();
			this.EnterScope(new ODataJsonLightDeltaReader.JsonLightDeltaFeedScope(feed, this.CurrentNavigationSource, this.CurrentEntityType, selectedProperties, this.CurrentScope.ODataUri));
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00019C45 File Offset: 0x00017E45
		private void ReadFeedEnd()
		{
			this.jsonLightEntryAndFeedDeserializer.ReadFeedContentEnd();
			this.jsonLightEntryAndFeedDeserializer.ReadNextLinkAnnotationAtFeedEnd(this.CurrentDeltaFeed, null, this.topLevelScope.DuplicatePropertyNamesChecker);
			this.ReplaceScope(ODataDeltaReaderState.FeedEnd);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00019C78 File Offset: 0x00017E78
		private void ReadDeltaStart(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties)
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.StartObject)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
			}
			ODataDeltaKind odataDeltaKind = ODataDeltaKind.Entry;
			IEdmEntityType edmEntityType = null;
			string text = this.jsonLightEntryAndFeedDeserializer.ReadContextUriAnnotation(ODataPayloadKind.Delta, duplicatePropertyNamesChecker, false);
			if (!string.IsNullOrEmpty(text))
			{
				ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(this.jsonLightInputContext.Model, text, ODataPayloadKind.Delta, ODataReaderBehavior.DefaultBehavior, true);
				odataDeltaKind = odataJsonLightContextUriParseResult.DeltaKind;
				edmEntityType = odataJsonLightContextUriParseResult.EdmType as IEdmEntityType;
			}
			switch (odataDeltaKind)
			{
			case ODataDeltaKind.Entry:
				this.StartDeltaEntry(ODataDeltaReaderState.DeltaEntryStart, duplicatePropertyNamesChecker, selectedProperties, edmEntityType);
				return;
			case ODataDeltaKind.DeletedEntry:
				this.StartDeltaEntry(ODataDeltaReaderState.DeltaDeletedEntry, duplicatePropertyNamesChecker, selectedProperties, null);
				this.ReadAtDeltaDeletedEntryImplementationSynchronously();
				return;
			case ODataDeltaKind.Link:
				this.StartDeltaLink(ODataDeltaReaderState.DeltaLink, duplicatePropertyNamesChecker, selectedProperties);
				this.ReadAtDeltaLinkImplementationSynchronously();
				return;
			case ODataDeltaKind.DeletedLink:
				this.StartDeltaLink(ODataDeltaReaderState.DeltaDeletedLink, duplicatePropertyNamesChecker, selectedProperties);
				this.ReadAtDeltaDeletedLinkImplementationSynchronously();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00019D4C File Offset: 0x00017F4C
		private void ReadDeltaEntryId()
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal('@' + "odata.id", this.jsonLightEntryAndFeedDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				this.CurrentDeltaEntry.Id = this.jsonLightEntryAndFeedDeserializer.ReadEntryInstanceAnnotation("odata.id", false, false, this.CurrentDeltaEntryState.DuplicatePropertyNamesChecker) as Uri;
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00019DD4 File Offset: 0x00017FD4
		private void ReadDeltaDeletedEntryId()
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("id", this.jsonLightEntryAndFeedDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				this.CurrentDeltaDeletedEntry.Id = this.ReadStringValue();
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00019E34 File Offset: 0x00018034
		private void ReadDeltaDeletedEntryReason()
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("reason", this.jsonLightEntryAndFeedDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				string text = this.ReadStringValue();
				if (string.CompareOrdinal(text, "changed") == 0)
				{
					this.CurrentDeltaDeletedEntry.Reason = new DeltaDeletedEntryReason?(DeltaDeletedEntryReason.Changed);
					return;
				}
				if (string.CompareOrdinal(text, "deleted") == 0)
				{
					this.CurrentDeltaDeletedEntry.Reason = new DeltaDeletedEntryReason?(DeltaDeletedEntryReason.Deleted);
					return;
				}
				this.CurrentDeltaDeletedEntry.Reason = default(DeltaDeletedEntryReason?);
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00019EE0 File Offset: 0x000180E0
		private void ReadDeltaLinkSource()
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("source", this.jsonLightEntryAndFeedDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				this.CurrentDeltaLink.Source = this.ReadUriValue();
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00019F40 File Offset: 0x00018140
		private void ReadDeltaLinkRelationship()
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("relationship", this.jsonLightEntryAndFeedDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				this.CurrentDeltaLink.Relationship = this.ReadStringValue();
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00019FA0 File Offset: 0x000181A0
		private void ReadDeltaLinkTarget()
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("target", this.jsonLightEntryAndFeedDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				this.CurrentDeltaLink.Target = this.ReadUriValue();
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001A000 File Offset: 0x00018200
		private void ReadDeltaDeletedLinkSource()
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("source", this.jsonLightEntryAndFeedDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				this.CurrentDeltaDeletedLink.Source = this.ReadUriValue();
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001A060 File Offset: 0x00018260
		private void ReadDeltaDeletedLinkRelationship()
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("relationship", this.jsonLightEntryAndFeedDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				this.CurrentDeltaDeletedLink.Relationship = this.ReadStringValue();
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001A0C0 File Offset: 0x000182C0
		private void ReadDeltaDeletedLinkTarget()
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("target", this.jsonLightEntryAndFeedDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				this.CurrentDeltaDeletedLink.Target = this.ReadUriValue();
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001A120 File Offset: 0x00018320
		private void StartDeltaEntry(ODataDeltaReaderState state, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties, IEdmEntityType entityTypeFromContextUri = null)
		{
			this.EnterScope(new ODataJsonLightDeltaReader.JsonLightDeltaEntryScope(state, ODataJsonLightDeltaReader.CreateNewDeltaEntry(state), this.CurrentNavigationSource, entityTypeFromContextUri ?? this.CurrentEntityType, duplicatePropertyNamesChecker ?? this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker(), selectedProperties, this.CurrentScope.ODataUri));
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001A16D File Offset: 0x0001836D
		private void StartDeltaLink(ODataDeltaReaderState state, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties)
		{
			this.EnterScope(new ODataJsonLightDeltaReader.JsonLightDeltaLinkScope(state, ODataJsonLightDeltaReader.CreateNewDeltaLink(state), this.CurrentNavigationSource, this.CurrentEntityType, this.CurrentScope.ODataUri));
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001A19C File Offset: 0x0001839C
		private void ApplyEntityTypeNameFromPayload(string entityTypeNameFromPayload)
		{
			EdmTypeKind edmTypeKind;
			SerializationTypeNameAnnotation serializationTypeNameAnnotation;
			IEdmEntityTypeReference edmEntityTypeReference = (IEdmEntityTypeReference)ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.Entity, null, this.CurrentEntityType.ToTypeReference(), entityTypeNameFromPayload, this.jsonLightInputContext.Model, this.jsonLightInputContext.MessageReaderSettings, () => EdmTypeKind.Entity, out edmTypeKind, out serializationTypeNameAnnotation);
			IEdmEntityType edmEntityType = null;
			ODataEntry currentDeltaEntry = this.CurrentDeltaEntry;
			if (edmEntityTypeReference != null)
			{
				edmEntityType = edmEntityTypeReference.EntityDefinition();
				currentDeltaEntry.TypeName = edmEntityType.FullTypeName();
				if (serializationTypeNameAnnotation != null)
				{
					currentDeltaEntry.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
				}
			}
			else if (entityTypeNameFromPayload != null)
			{
				currentDeltaEntry.TypeName = entityTypeNameFromPayload;
			}
			this.CurrentEntityType = edmEntityType;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001A23C File Offset: 0x0001843C
		private static ODataItem CreateNewDeltaEntry(ODataDeltaReaderState state)
		{
			if (state == ODataDeltaReaderState.DeltaEntryStart)
			{
				return new ODataEntry
				{
					Properties = new ReadOnlyEnumerable<ODataProperty>()
				};
			}
			if (state == ODataDeltaReaderState.DeltaDeletedEntry)
			{
				return new ODataDeltaDeletedEntry(null, DeltaDeletedEntryReason.Deleted);
			}
			return null;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001A26D File Offset: 0x0001846D
		private static ODataDeltaLinkBase CreateNewDeltaLink(ODataDeltaReaderState state)
		{
			if (state == ODataDeltaReaderState.DeltaLink)
			{
				return new ODataDeltaLink(null, null, null);
			}
			if (state == ODataDeltaReaderState.DeltaDeletedLink)
			{
				return new ODataDeltaDeletedLink(null, null, null);
			}
			return null;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001A28A File Offset: 0x0001848A
		private static ODataJsonLightDeltaReader.Scope CreateExceptionScope()
		{
			return new ODataJsonLightDeltaReader.Scope(ODataDeltaReaderState.Exception, null, null, null, null);
		}

		// Token: 0x0400033C RID: 828
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x0400033D RID: 829
		private readonly ODataJsonLightEntryAndFeedDeserializer jsonLightEntryAndFeedDeserializer;

		// Token: 0x0400033E RID: 830
		private readonly ODataJsonLightDeltaReader.JsonLightTopLevelScope topLevelScope;

		// Token: 0x0400033F RID: 831
		private readonly Stack<ODataJsonLightDeltaReader.Scope> scopes = new Stack<ODataJsonLightDeltaReader.Scope>();

		// Token: 0x020000C5 RID: 197
		private class Scope
		{
			// Token: 0x06000750 RID: 1872 RVA: 0x0001A296 File Offset: 0x00018496
			public Scope(ODataDeltaReaderState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, ODataUri odataUri)
			{
				this.state = state;
				this.item = item;
				this.EntityType = expectedEntityType;
				this.NavigationSource = navigationSource;
				this.odataUri = odataUri;
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001A2C3 File Offset: 0x000184C3
			public ODataDeltaReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001A2CB File Offset: 0x000184CB
			public ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001A2D3 File Offset: 0x000184D3
			public ODataUri ODataUri
			{
				get
				{
					return this.odataUri;
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001A2DB File Offset: 0x000184DB
			// (set) Token: 0x06000755 RID: 1877 RVA: 0x0001A2E3 File Offset: 0x000184E3
			public IEdmNavigationSource NavigationSource { get; private set; }

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001A2EC File Offset: 0x000184EC
			// (set) Token: 0x06000757 RID: 1879 RVA: 0x0001A2F4 File Offset: 0x000184F4
			public IEdmEntityType EntityType { get; set; }

			// Token: 0x04000341 RID: 833
			private readonly ODataDeltaReaderState state;

			// Token: 0x04000342 RID: 834
			private readonly ODataItem item;

			// Token: 0x04000343 RID: 835
			private readonly ODataUri odataUri;
		}

		// Token: 0x020000C6 RID: 198
		private sealed class JsonLightDeltaFeedScope : ODataJsonLightDeltaReader.Scope
		{
			// Token: 0x06000758 RID: 1880 RVA: 0x0001A2FD File Offset: 0x000184FD
			public JsonLightDeltaFeedScope(ODataDeltaFeed feed, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataDeltaReaderState.DeltaFeedStart, feed, navigationSource, expectedEntityType, odataUri)
			{
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001A313 File Offset: 0x00018513
			// (set) Token: 0x0600075A RID: 1882 RVA: 0x0001A31B File Offset: 0x0001851B
			public SelectedPropertiesNode SelectedProperties { get; private set; }
		}

		// Token: 0x020000C7 RID: 199
		private sealed class JsonLightDeltaLinkScope : ODataJsonLightDeltaReader.Scope
		{
			// Token: 0x0600075B RID: 1883 RVA: 0x0001A324 File Offset: 0x00018524
			public JsonLightDeltaLinkScope(ODataDeltaReaderState state, ODataDeltaLinkBase link, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, ODataUri odataUri)
				: base(state, link, navigationSource, expectedEntityType, odataUri)
			{
			}
		}

		// Token: 0x020000C8 RID: 200
		private sealed class JsonLightDeltaEntryScope : ODataJsonLightDeltaReader.Scope, IODataJsonLightReaderEntryState
		{
			// Token: 0x0600075C RID: 1884 RVA: 0x0001A333 File Offset: 0x00018533
			public JsonLightDeltaEntryScope(ODataDeltaReaderState readerState, ODataItem entry, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(readerState, entry, navigationSource, expectedEntityType, odataUri)
			{
				this.DuplicatePropertyNamesChecker = duplicatePropertyNamesChecker;
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001A352 File Offset: 0x00018552
			// (set) Token: 0x0600075E RID: 1886 RVA: 0x0001A35A File Offset: 0x0001855A
			public ODataEntityMetadataBuilder MetadataBuilder { get; set; }

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001A363 File Offset: 0x00018563
			// (set) Token: 0x06000760 RID: 1888 RVA: 0x0001A36B File Offset: 0x0001856B
			public bool AnyPropertyFound { get; set; }

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001A374 File Offset: 0x00018574
			// (set) Token: 0x06000762 RID: 1890 RVA: 0x0001A37C File Offset: 0x0001857C
			public ODataJsonLightReaderNavigationLinkInfo FirstNavigationLinkInfo { get; set; }

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001A385 File Offset: 0x00018585
			// (set) Token: 0x06000764 RID: 1892 RVA: 0x0001A38D File Offset: 0x0001858D
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; private set; }

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001A396 File Offset: 0x00018596
			// (set) Token: 0x06000766 RID: 1894 RVA: 0x0001A39E File Offset: 0x0001859E
			public SelectedPropertiesNode SelectedProperties { get; private set; }

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001A3A8 File Offset: 0x000185A8
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

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001A3CD File Offset: 0x000185CD
			// (set) Token: 0x06000769 RID: 1897 RVA: 0x0001A3D5 File Offset: 0x000185D5
			public bool ProcessingMissingProjectedNavigationLinks { get; set; }

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001A3DE File Offset: 0x000185DE
			ODataEntry IODataJsonLightReaderEntryState.Entry
			{
				get
				{
					return base.Item as ODataEntry;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001A3EB File Offset: 0x000185EB
			IEdmEntityType IODataJsonLightReaderEntryState.EntityType
			{
				get
				{
					return base.EntityType;
				}
			}

			// Token: 0x04000347 RID: 839
			private List<string> navigationPropertiesRead;
		}

		// Token: 0x020000C9 RID: 201
		private sealed class JsonLightExpandedNavigationPropertyScope : ODataJsonLightDeltaReader.Scope
		{
			// Token: 0x0600076C RID: 1900 RVA: 0x0001A3F4 File Offset: 0x000185F4
			public JsonLightExpandedNavigationPropertyScope(ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo, IEdmNavigationSource parentNavigationSource, IEdmEntityType parentEntityType, ODataUri odataUri, ODataJsonLightInputContext jsonLightInputContext)
				: base(ODataDeltaReaderState.ExpandedNavigationProperty, null, parentNavigationSource, parentEntityType, odataUri)
			{
				IEdmNavigationSource edmNavigationSource = parentNavigationSource.FindNavigationTarget(navigationLinkInfo.NavigationProperty);
				IEdmEntityType edmEntityType = navigationLinkInfo.NavigationProperty.ToEntityType();
				bool flag = navigationLinkInfo.NavigationProperty.Type.IsCollection();
				this.expandedNavigationPropertyReader = new ODataJsonLightReader(jsonLightInputContext, edmNavigationSource, edmEntityType, flag, false, true, null);
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001A44B File Offset: 0x0001864B
			public ODataReaderState SubState
			{
				get
				{
					return this.expandedNavigationPropertyReader.State;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001A458 File Offset: 0x00018658
			public new ODataItem Item
			{
				get
				{
					return this.expandedNavigationPropertyReader.Item;
				}
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x0600076F RID: 1903 RVA: 0x0001A465 File Offset: 0x00018665
			public ODataReader ExpandedNavigationPropertyReader
			{
				get
				{
					return this.expandedNavigationPropertyReader;
				}
			}

			// Token: 0x0400034E RID: 846
			private readonly ODataReader expandedNavigationPropertyReader;
		}

		// Token: 0x020000CA RID: 202
		private sealed class JsonLightTopLevelScope : ODataJsonLightDeltaReader.Scope
		{
			// Token: 0x06000770 RID: 1904 RVA: 0x0001A46D File Offset: 0x0001866D
			public JsonLightTopLevelScope(IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
				: base(ODataDeltaReaderState.Start, null, navigationSource, expectedEntityType, null)
			{
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001A47A File Offset: 0x0001867A
			// (set) Token: 0x06000772 RID: 1906 RVA: 0x0001A482 File Offset: 0x00018682
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; set; }
		}
	}
}
