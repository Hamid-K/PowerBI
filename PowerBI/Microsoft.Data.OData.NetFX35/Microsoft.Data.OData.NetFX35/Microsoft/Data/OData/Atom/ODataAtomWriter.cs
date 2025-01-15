using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200029A RID: 666
	internal sealed class ODataAtomWriter : ODataWriterCore
	{
		// Token: 0x06001538 RID: 5432 RVA: 0x0004D71C File Offset: 0x0004B91C
		internal ODataAtomWriter(ODataAtomOutputContext atomOutputContext, IEdmEntitySet entitySet, IEdmEntityType entityType, bool writingFeed)
			: base(atomOutputContext, entitySet, entityType, writingFeed)
		{
			this.atomOutputContext = atomOutputContext;
			if (this.atomOutputContext.MessageWriterSettings.AtomStartEntryXmlCustomizationCallback != null)
			{
				this.atomOutputContext.InitializeWriterCustomization();
			}
			this.atomEntryAndFeedSerializer = new ODataAtomEntryAndFeedSerializer(this.atomOutputContext);
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0004D77C File Offset: 0x0004B97C
		private ODataAtomWriter.AtomEntryScope CurrentEntryScope
		{
			get
			{
				return base.CurrentScope as ODataAtomWriter.AtomEntryScope;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0004D798 File Offset: 0x0004B998
		private ODataAtomWriter.AtomFeedScope CurrentFeedScope
		{
			get
			{
				return base.CurrentScope as ODataAtomWriter.AtomFeedScope;
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0004D7B2 File Offset: 0x0004B9B2
		protected override void VerifyNotDisposed()
		{
			this.atomOutputContext.VerifyNotDisposed();
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0004D7BF File Offset: 0x0004B9BF
		protected override void FlushSynchronously()
		{
			this.atomOutputContext.Flush();
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0004D7CC File Offset: 0x0004B9CC
		protected override void StartPayload()
		{
			this.atomEntryAndFeedSerializer.WritePayloadStart();
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0004D7D9 File Offset: 0x0004B9D9
		protected override void EndPayload()
		{
			this.atomEntryAndFeedSerializer.WritePayloadEnd();
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0004D7E8 File Offset: 0x0004B9E8
		protected override void StartEntry(ODataEntry entry)
		{
			this.CheckAndWriteParentNavigationLinkStartForInlineElement();
			if (entry == null)
			{
				return;
			}
			this.StartEntryXmlCustomization(entry);
			this.atomOutputContext.XmlWriter.WriteStartElement("", "entry", "http://www.w3.org/2005/Atom");
			if (base.IsTopLevel)
			{
				this.atomEntryAndFeedSerializer.WriteBaseUriAndDefaultNamespaceAttributes();
			}
			string etag = entry.ETag;
			if (etag != null)
			{
				ODataAtomWriterUtils.WriteETag(this.atomOutputContext.XmlWriter, etag);
			}
			ODataAtomWriter.AtomEntryScope currentEntryScope = this.CurrentEntryScope;
			AtomEntryMetadata atomEntryMetadata = entry.Atom();
			string id = entry.Id;
			if (id != null)
			{
				this.atomEntryAndFeedSerializer.WriteEntryId(id);
				currentEntryScope.SetWrittenElement(ODataAtomWriter.AtomElement.Id);
			}
			string entryTypeNameForWriting = this.atomOutputContext.TypeNameOracle.GetEntryTypeNameForWriting(entry);
			this.atomEntryAndFeedSerializer.WriteEntryTypeName(entryTypeNameForWriting, atomEntryMetadata);
			Uri editLink = entry.EditLink;
			if (editLink != null)
			{
				this.atomEntryAndFeedSerializer.WriteEntryEditLink(editLink, atomEntryMetadata);
				currentEntryScope.SetWrittenElement(ODataAtomWriter.AtomElement.EditLink);
			}
			Uri readLink = entry.ReadLink;
			if (readLink != null)
			{
				this.atomEntryAndFeedSerializer.WriteEntryReadLink(readLink, atomEntryMetadata);
				currentEntryScope.SetWrittenElement(ODataAtomWriter.AtomElement.ReadLink);
			}
			this.WriteInstanceAnnotations(entry.InstanceAnnotations, currentEntryScope.InstanceAnnotationWriteTracker);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0004D900 File Offset: 0x0004BB00
		protected override void EndEntry(ODataEntry entry)
		{
			if (entry == null)
			{
				this.CheckAndWriteParentNavigationLinkEndForInlineElement();
				return;
			}
			IEdmEntityType entryEntityType = base.EntryEntityType;
			EntryPropertiesValueCache entryPropertiesValueCache = new EntryPropertiesValueCache(entry);
			ODataEntityPropertyMappingCache odataEntityPropertyMappingCache = this.atomOutputContext.Model.EnsureEpmCache(entryEntityType, int.MaxValue);
			if (odataEntityPropertyMappingCache != null)
			{
				EpmWriterUtils.CacheEpmProperties(entryPropertiesValueCache, odataEntityPropertyMappingCache.EpmSourceTree);
			}
			ODataAtomWriter.AtomEntryScope currentEntryScope = this.CurrentEntryScope;
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(currentEntryScope);
			AtomEntryMetadata atomEntryMetadata = entry.Atom();
			if (!currentEntryScope.IsElementWritten(ODataAtomWriter.AtomElement.Id))
			{
				this.atomEntryAndFeedSerializer.WriteEntryId(entry.Id);
			}
			Uri editLink = entry.EditLink;
			if (editLink != null && !currentEntryScope.IsElementWritten(ODataAtomWriter.AtomElement.EditLink))
			{
				this.atomEntryAndFeedSerializer.WriteEntryEditLink(editLink, atomEntryMetadata);
			}
			Uri readLink = entry.ReadLink;
			if (readLink != null && !currentEntryScope.IsElementWritten(ODataAtomWriter.AtomElement.ReadLink))
			{
				this.atomEntryAndFeedSerializer.WriteEntryReadLink(readLink, atomEntryMetadata);
			}
			AtomEntryMetadata atomEntryMetadata2 = null;
			if (odataEntityPropertyMappingCache != null)
			{
				ODataVersionChecker.CheckEntityPropertyMapping(this.atomOutputContext.Version, entryEntityType, this.atomOutputContext.Model);
				atomEntryMetadata2 = EpmSyndicationWriter.WriteEntryEpm(odataEntityPropertyMappingCache.EpmTargetTree, entryPropertiesValueCache, entryEntityType.ToTypeReference().AsEntity(), this.atomOutputContext);
			}
			this.atomEntryAndFeedSerializer.WriteEntryMetadata(atomEntryMetadata, atomEntryMetadata2, this.updatedTime);
			IEnumerable<ODataProperty> entryStreamProperties = entryPropertiesValueCache.EntryStreamProperties;
			if (entryStreamProperties != null)
			{
				foreach (ODataProperty odataProperty in entryStreamProperties)
				{
					this.atomEntryAndFeedSerializer.WriteStreamProperty(odataProperty, entryEntityType, base.DuplicatePropertyNamesChecker, projectedPropertiesAnnotation);
				}
			}
			IEnumerable<ODataAssociationLink> associationLinks = entry.AssociationLinks;
			if (associationLinks != null)
			{
				foreach (ODataAssociationLink odataAssociationLink in associationLinks)
				{
					this.atomEntryAndFeedSerializer.WriteAssociationLink(odataAssociationLink, entryEntityType, base.DuplicatePropertyNamesChecker, projectedPropertiesAnnotation);
				}
			}
			IEnumerable<ODataAction> actions = entry.Actions;
			if (actions != null)
			{
				foreach (ODataAction odataAction in actions)
				{
					ValidationUtils.ValidateOperationNotNull(odataAction, true);
					this.atomEntryAndFeedSerializer.WriteOperation(odataAction);
				}
			}
			IEnumerable<ODataFunction> functions = entry.Functions;
			if (functions != null)
			{
				foreach (ODataFunction odataFunction in functions)
				{
					ValidationUtils.ValidateOperationNotNull(odataFunction, false);
					this.atomEntryAndFeedSerializer.WriteOperation(odataFunction);
				}
			}
			this.WriteEntryContent(entry, entryEntityType, entryPropertiesValueCache, (odataEntityPropertyMappingCache == null) ? null : odataEntityPropertyMappingCache.EpmSourceTree.Root, projectedPropertiesAnnotation);
			if (odataEntityPropertyMappingCache != null)
			{
				EpmCustomWriter.WriteEntryEpm(this.atomOutputContext.XmlWriter, odataEntityPropertyMappingCache.EpmTargetTree, entryPropertiesValueCache, entryEntityType.ToTypeReference().AsEntity(), this.atomOutputContext);
			}
			this.WriteInstanceAnnotations(entry.InstanceAnnotations, currentEntryScope.InstanceAnnotationWriteTracker);
			this.atomOutputContext.XmlWriter.WriteEndElement();
			this.EndEntryXmlCustomization(entry);
			this.CheckAndWriteParentNavigationLinkEndForInlineElement();
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0004DC04 File Offset: 0x0004BE04
		protected override void StartFeed(ODataFeed feed)
		{
			if (string.IsNullOrEmpty(feed.Id))
			{
				throw new ODataException(Strings.ODataAtomWriter_FeedsMustHaveNonEmptyId);
			}
			this.CheckAndWriteParentNavigationLinkStartForInlineElement();
			this.atomOutputContext.XmlWriter.WriteStartElement("", "feed", "http://www.w3.org/2005/Atom");
			if (base.IsTopLevel)
			{
				this.atomEntryAndFeedSerializer.WriteBaseUriAndDefaultNamespaceAttributes();
				if (feed.Count != null)
				{
					this.atomEntryAndFeedSerializer.WriteCount(feed.Count.Value, false);
				}
			}
			bool flag;
			this.atomEntryAndFeedSerializer.WriteFeedMetadata(feed, this.updatedTime, out flag);
			this.CurrentFeedScope.AuthorWritten = flag;
			this.WriteFeedInstanceAnnotations(feed, this.CurrentFeedScope);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0004DCB8 File Offset: 0x0004BEB8
		protected override void EndFeed(ODataFeed feed)
		{
			ODataAtomWriter.AtomFeedScope currentFeedScope = this.CurrentFeedScope;
			if (!currentFeedScope.AuthorWritten && currentFeedScope.EntryCount == 0)
			{
				this.atomEntryAndFeedSerializer.WriteFeedDefaultAuthor();
			}
			this.WriteFeedInstanceAnnotations(feed, currentFeedScope);
			this.atomEntryAndFeedSerializer.WriteFeedNextPageLink(feed);
			if (base.IsTopLevel)
			{
				if (this.atomOutputContext.WritingResponse)
				{
					this.atomEntryAndFeedSerializer.WriteFeedDeltaLink(feed);
				}
			}
			else
			{
				base.ValidateNoDeltaLinkForExpandedFeed(feed);
			}
			this.atomOutputContext.XmlWriter.WriteEndElement();
			this.CheckAndWriteParentNavigationLinkEndForInlineElement();
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0004DD3B File Offset: 0x0004BF3B
		protected override void WriteDeferredNavigationLink(ODataNavigationLink navigationLink)
		{
			this.WriteNavigationLinkStart(navigationLink, null);
			this.WriteNavigationLinkEnd();
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0004DD4B File Offset: 0x0004BF4B
		protected override void StartNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0004DD4D File Offset: 0x0004BF4D
		protected override void EndNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0004DD4F File Offset: 0x0004BF4F
		protected override void WriteEntityReferenceInNavigationLinkContent(ODataNavigationLink parentNavigationLink, ODataEntityReferenceLink entityReferenceLink)
		{
			this.WriteNavigationLinkStart(parentNavigationLink, entityReferenceLink.Url);
			this.WriteNavigationLinkEnd();
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0004DD64 File Offset: 0x0004BF64
		protected override ODataWriterCore.FeedScope CreateFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataAtomWriter.AtomFeedScope(feed, entitySet, entityType, skipWriting, selectedProperties);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0004DD72 File Offset: 0x0004BF72
		protected override ODataWriterCore.EntryScope CreateEntryScope(ODataEntry entry, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataAtomWriter.AtomEntryScope(entry, base.GetEntrySerializationInfo(entry), entitySet, entityType, skipWriting, this.atomOutputContext.WritingResponse, this.atomOutputContext.MessageWriterSettings.WriterBehavior, selectedProperties);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0004DDAC File Offset: 0x0004BFAC
		private void WriteInstanceAnnotations(IEnumerable<ODataInstanceAnnotation> instanceAnnotations, InstanceAnnotationWriteTracker tracker)
		{
			IEnumerable<AtomInstanceAnnotation> enumerable = Enumerable.Select<ODataInstanceAnnotation, AtomInstanceAnnotation>(instanceAnnotations, (ODataInstanceAnnotation instanceAnnotation) => AtomInstanceAnnotation.CreateFrom(instanceAnnotation, null));
			this.atomEntryAndFeedSerializer.WriteInstanceAnnotations(enumerable, tracker);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0004DDEA File Offset: 0x0004BFEA
		private void WriteFeedInstanceAnnotations(ODataFeed feed, ODataAtomWriter.AtomFeedScope currentFeedScope)
		{
			if (base.IsTopLevel)
			{
				this.WriteInstanceAnnotations(feed.InstanceAnnotations, currentFeedScope.InstanceAnnotationWriteTracker);
				return;
			}
			if (feed.InstanceAnnotations.Count > 0)
			{
				throw new ODataException(Strings.ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedFeed);
			}
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0004DE20 File Offset: 0x0004C020
		private void WriteEntryContent(ODataEntry entry, IEdmEntityType entryType, EntryPropertiesValueCache propertiesValueCache, EpmSourcePathSegment rootSourcePathSegment, ProjectedPropertiesAnnotation projectedProperties)
		{
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			if (mediaResource == null)
			{
				this.atomOutputContext.XmlWriter.WriteStartElement("", "content", "http://www.w3.org/2005/Atom");
				this.atomOutputContext.XmlWriter.WriteAttributeString("type", "application/xml");
				this.atomEntryAndFeedSerializer.WriteProperties(entryType, propertiesValueCache.EntryProperties, false, new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesStart), new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesEnd), base.DuplicatePropertyNamesChecker, propertiesValueCache, rootSourcePathSegment, projectedProperties);
				this.atomOutputContext.XmlWriter.WriteEndElement();
				return;
			}
			WriterValidationUtils.ValidateStreamReferenceValue(mediaResource, true);
			this.atomEntryAndFeedSerializer.WriteEntryMediaEditLink(mediaResource);
			if (mediaResource.ReadLink != null)
			{
				this.atomOutputContext.XmlWriter.WriteStartElement("", "content", "http://www.w3.org/2005/Atom");
				this.atomOutputContext.XmlWriter.WriteAttributeString("type", mediaResource.ContentType);
				this.atomOutputContext.XmlWriter.WriteAttributeString("src", this.atomEntryAndFeedSerializer.UriToUrlAttributeValue(mediaResource.ReadLink));
				this.atomOutputContext.XmlWriter.WriteEndElement();
			}
			this.atomEntryAndFeedSerializer.WriteProperties(entryType, propertiesValueCache.EntryProperties, false, new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesStart), new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesEnd), base.DuplicatePropertyNamesChecker, propertiesValueCache, rootSourcePathSegment, projectedProperties);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0004DF98 File Offset: 0x0004C198
		private void CheckAndWriteParentNavigationLinkStartForInlineElement()
		{
			ODataNavigationLink parentNavigationLink = base.ParentNavigationLink;
			if (parentNavigationLink != null)
			{
				this.WriteNavigationLinkStart(parentNavigationLink, null);
				this.atomOutputContext.XmlWriter.WriteStartElement("m", "inline", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0004DFD8 File Offset: 0x0004C1D8
		private void CheckAndWriteParentNavigationLinkEndForInlineElement()
		{
			ODataNavigationLink parentNavigationLink = base.ParentNavigationLink;
			if (parentNavigationLink != null)
			{
				this.atomOutputContext.XmlWriter.WriteEndElement();
				this.WriteNavigationLinkEnd();
			}
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0004E005 File Offset: 0x0004C205
		private void WriteNavigationLinkStart(ODataNavigationLink navigationLink, Uri navigationLinkUrlOverride)
		{
			WriterValidationUtils.ValidateNavigationLinkHasCardinality(navigationLink);
			WriterValidationUtils.ValidateNavigationLinkUrlPresent(navigationLink);
			this.atomEntryAndFeedSerializer.WriteNavigationLinkStart(navigationLink, navigationLinkUrlOverride);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0004E020 File Offset: 0x0004C220
		private void WriteNavigationLinkEnd()
		{
			this.atomOutputContext.XmlWriter.WriteEndElement();
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0004E034 File Offset: 0x0004C234
		private void StartEntryXmlCustomization(ODataEntry entry)
		{
			if (this.atomOutputContext.MessageWriterSettings.AtomStartEntryXmlCustomizationCallback != null)
			{
				XmlWriter xmlWriter = this.atomOutputContext.MessageWriterSettings.AtomStartEntryXmlCustomizationCallback.Invoke(entry, this.atomOutputContext.XmlWriter);
				if (xmlWriter != null)
				{
					if (object.ReferenceEquals(this.atomOutputContext.XmlWriter, xmlWriter))
					{
						throw new ODataException(Strings.ODataAtomWriter_StartEntryXmlCustomizationCallbackReturnedSameInstance);
					}
				}
				else
				{
					xmlWriter = this.atomOutputContext.XmlWriter;
				}
				this.atomOutputContext.PushCustomWriter(xmlWriter);
			}
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0004E0B0 File Offset: 0x0004C2B0
		private void EndEntryXmlCustomization(ODataEntry entry)
		{
			if (this.atomOutputContext.MessageWriterSettings.AtomStartEntryXmlCustomizationCallback != null)
			{
				XmlWriter xmlWriter = this.atomOutputContext.PopCustomWriter();
				if (!object.ReferenceEquals(this.atomOutputContext.XmlWriter, xmlWriter))
				{
					this.atomOutputContext.MessageWriterSettings.AtomEndEntryXmlCustomizationCallback.Invoke(entry, xmlWriter, this.atomOutputContext.XmlWriter);
				}
			}
		}

		// Token: 0x04000919 RID: 2329
		private readonly string updatedTime = ODataAtomConvert.ToAtomString(DateTimeOffset.UtcNow);

		// Token: 0x0400091A RID: 2330
		private readonly ODataAtomOutputContext atomOutputContext;

		// Token: 0x0400091B RID: 2331
		private readonly ODataAtomEntryAndFeedSerializer atomEntryAndFeedSerializer;

		// Token: 0x0200029B RID: 667
		private enum AtomElement
		{
			// Token: 0x0400091E RID: 2334
			Id = 1,
			// Token: 0x0400091F RID: 2335
			ReadLink,
			// Token: 0x04000920 RID: 2336
			EditLink = 4
		}

		// Token: 0x0200029C RID: 668
		private sealed class AtomFeedScope : ODataWriterCore.FeedScope
		{
			// Token: 0x06001553 RID: 5459 RVA: 0x0004E110 File Offset: 0x0004C310
			internal AtomFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
				: base(feed, entitySet, entityType, skipWriting, selectedProperties)
			{
			}

			// Token: 0x17000487 RID: 1159
			// (get) Token: 0x06001554 RID: 5460 RVA: 0x0004E11F File Offset: 0x0004C31F
			// (set) Token: 0x06001555 RID: 5461 RVA: 0x0004E127 File Offset: 0x0004C327
			internal bool AuthorWritten
			{
				get
				{
					return this.authorWritten;
				}
				set
				{
					this.authorWritten = value;
				}
			}

			// Token: 0x04000921 RID: 2337
			private bool authorWritten;
		}

		// Token: 0x0200029D RID: 669
		private sealed class AtomEntryScope : ODataWriterCore.EntryScope
		{
			// Token: 0x06001556 RID: 5462 RVA: 0x0004E130 File Offset: 0x0004C330
			internal AtomEntryScope(ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, bool writingResponse, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties)
				: base(entry, serializationInfo, entitySet, entityType, skipWriting, writingResponse, writerBehavior, selectedProperties)
			{
			}

			// Token: 0x06001557 RID: 5463 RVA: 0x0004E150 File Offset: 0x0004C350
			internal void SetWrittenElement(ODataAtomWriter.AtomElement atomElement)
			{
				this.alreadyWrittenElements |= (int)atomElement;
			}

			// Token: 0x06001558 RID: 5464 RVA: 0x0004E160 File Offset: 0x0004C360
			internal bool IsElementWritten(ODataAtomWriter.AtomElement atomElement)
			{
				return (this.alreadyWrittenElements & (int)atomElement) == (int)atomElement;
			}

			// Token: 0x04000922 RID: 2338
			private int alreadyWrittenElements;
		}
	}
}
