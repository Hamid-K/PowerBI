using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000068 RID: 104
	internal sealed class ODataAtomWriter : ODataWriterCore
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x000102A5 File Offset: 0x0000E4A5
		internal ODataAtomWriter(ODataAtomOutputContext atomOutputContext, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool writingFeed)
			: base(atomOutputContext, navigationSource, entityType, writingFeed, false, null)
		{
			this.atomOutputContext = atomOutputContext;
			this.atomEntryAndFeedSerializer = new ODataAtomEntryAndFeedSerializer(this.atomOutputContext);
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000102DC File Offset: 0x0000E4DC
		private ODataAtomWriter.AtomEntryScope CurrentEntryScope
		{
			get
			{
				return base.CurrentScope as ODataAtomWriter.AtomEntryScope;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000102F8 File Offset: 0x0000E4F8
		private ODataAtomWriter.AtomFeedScope CurrentFeedScope
		{
			get
			{
				return base.CurrentScope as ODataAtomWriter.AtomFeedScope;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00010312 File Offset: 0x0000E512
		protected override void VerifyNotDisposed()
		{
			this.atomOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001031F File Offset: 0x0000E51F
		protected override void FlushSynchronously()
		{
			this.atomOutputContext.Flush();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001032C File Offset: 0x0000E52C
		protected override void StartPayload()
		{
			this.atomEntryAndFeedSerializer.WritePayloadStart();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00010339 File Offset: 0x0000E539
		protected override void EndPayload()
		{
			this.atomEntryAndFeedSerializer.WritePayloadEnd();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00010348 File Offset: 0x0000E548
		protected override void StartEntry(ODataEntry entry)
		{
			this.CheckAndWriteParentNavigationLinkStartForInlineElement();
			if (entry == null)
			{
				return;
			}
			this.atomOutputContext.XmlWriter.WriteStartElement("", "entry", "http://www.w3.org/2005/Atom");
			if (base.IsTopLevel)
			{
				this.atomEntryAndFeedSerializer.WriteBaseUriAndDefaultNamespaceAttributes();
				this.atomEntryAndFeedSerializer.TryWriteEntryContextUri(this.CurrentEntryScope.GetOrCreateTypeContext(this.atomOutputContext.Model, this.atomOutputContext.WritingResponse));
			}
			string etag = entry.ETag;
			if (etag != null)
			{
				ODataAtomWriterUtils.WriteETag(this.atomOutputContext.XmlWriter, etag);
			}
			ODataAtomWriter.AtomEntryScope currentEntryScope = this.CurrentEntryScope;
			AtomEntryMetadata atomEntryMetadata = entry.Atom();
			Uri id = entry.Id;
			bool isTransient = entry.IsTransient;
			if (id != null)
			{
				this.atomEntryAndFeedSerializer.WriteEntryId(id, isTransient);
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
				if (readLink != editLink)
				{
					this.atomEntryAndFeedSerializer.WriteEntryReadLink(readLink, atomEntryMetadata);
				}
				currentEntryScope.SetWrittenElement(ODataAtomWriter.AtomElement.ReadLink);
			}
			this.WriteInstanceAnnotations(entry.InstanceAnnotations, currentEntryScope.InstanceAnnotationWriteTracker);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000104A0 File Offset: 0x0000E6A0
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "The coupling is intentional here.")]
		protected override void EndEntry(ODataEntry entry)
		{
			if (entry == null)
			{
				this.CheckAndWriteParentNavigationLinkEndForInlineElement();
				return;
			}
			IEdmEntityType entryEntityType = base.EntryEntityType;
			EntryPropertiesValueCache entryPropertiesValueCache = new EntryPropertiesValueCache(entry);
			ODataAtomWriter.AtomEntryScope currentEntryScope = this.CurrentEntryScope;
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(currentEntryScope);
			AtomEntryMetadata atomEntryMetadata = entry.Atom();
			if (!currentEntryScope.IsElementWritten(ODataAtomWriter.AtomElement.Id))
			{
				bool isTransient = entry.IsTransient;
				this.atomEntryAndFeedSerializer.WriteEntryId(entry.Id, isTransient);
			}
			Uri editLink = entry.EditLink;
			if (editLink != null && !currentEntryScope.IsElementWritten(ODataAtomWriter.AtomElement.EditLink))
			{
				this.atomEntryAndFeedSerializer.WriteEntryEditLink(editLink, atomEntryMetadata);
			}
			Uri readLink = entry.ReadLink;
			if (readLink != null && readLink != editLink && !currentEntryScope.IsElementWritten(ODataAtomWriter.AtomElement.ReadLink))
			{
				this.atomEntryAndFeedSerializer.WriteEntryReadLink(readLink, atomEntryMetadata);
			}
			this.atomEntryAndFeedSerializer.WriteEntryMetadata(atomEntryMetadata, this.updatedTime);
			IEnumerable<ODataProperty> entryStreamProperties = entryPropertiesValueCache.EntryStreamProperties;
			if (entryStreamProperties != null)
			{
				foreach (ODataProperty odataProperty in entryStreamProperties)
				{
					this.atomEntryAndFeedSerializer.WriteStreamProperty(odataProperty, entryEntityType, base.DuplicatePropertyNamesChecker, projectedPropertiesAnnotation);
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
			this.WriteEntryContent(entry, entryEntityType, entryPropertiesValueCache, projectedPropertiesAnnotation);
			this.WriteInstanceAnnotations(entry.InstanceAnnotations, currentEntryScope.InstanceAnnotationWriteTracker);
			this.atomOutputContext.XmlWriter.WriteEndElement();
			this.CheckAndWriteParentNavigationLinkEndForInlineElement();
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000106B0 File Offset: 0x0000E8B0
		protected override void StartFeed(ODataFeed feed)
		{
			if (feed.Id == null)
			{
				throw new ODataException(Strings.ODataAtomWriter_FeedsMustHaveNonEmptyId);
			}
			this.CheckAndWriteParentNavigationLinkStartForInlineElement();
			this.atomOutputContext.XmlWriter.WriteStartElement("", "feed", "http://www.w3.org/2005/Atom");
			if (base.IsTopLevel)
			{
				this.atomEntryAndFeedSerializer.WriteBaseUriAndDefaultNamespaceAttributes();
				this.atomEntryAndFeedSerializer.TryWriteFeedContextUri(this.CurrentFeedScope.GetOrCreateTypeContext(this.atomOutputContext.Model, this.atomOutputContext.WritingResponse));
				if (feed.Count != null)
				{
					this.atomEntryAndFeedSerializer.WriteCount(feed.Count.Value);
				}
			}
			bool flag;
			this.atomEntryAndFeedSerializer.WriteFeedMetadata(feed, this.updatedTime, out flag);
			this.CurrentFeedScope.AuthorWritten = flag;
			this.WriteFeedInstanceAnnotations(feed, this.CurrentFeedScope);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00010790 File Offset: 0x0000E990
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

		// Token: 0x06000458 RID: 1112 RVA: 0x00010813 File Offset: 0x0000EA13
		protected override void WriteDeferredNavigationLink(ODataNavigationLink navigationLink)
		{
			this.WriteNavigationLinkStart(navigationLink, null);
			this.WriteNavigationLinkEnd();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00010823 File Offset: 0x0000EA23
		protected override void StartNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00010825 File Offset: 0x0000EA25
		protected override void EndNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00010827 File Offset: 0x0000EA27
		protected override void WriteEntityReferenceInNavigationLinkContent(ODataNavigationLink parentNavigationLink, ODataEntityReferenceLink entityReferenceLink)
		{
			this.WriteNavigationLinkStart(parentNavigationLink, entityReferenceLink.Url);
			this.WriteNavigationLinkEnd();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001083C File Offset: 0x0000EA3C
		protected override ODataWriterCore.FeedScope CreateFeedScope(ODataFeed feed, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataAtomWriter.AtomFeedScope(feed, navigationSource, entityType, skipWriting, selectedProperties, odataUri);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001084C File Offset: 0x0000EA4C
		protected override ODataWriterCore.EntryScope CreateEntryScope(ODataEntry entry, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataAtomWriter.AtomEntryScope(entry, base.GetEntrySerializationInfo(entry), navigationSource, entityType, skipWriting, this.atomOutputContext.WritingResponse, this.atomOutputContext.MessageWriterSettings.WriterBehavior, selectedProperties, odataUri);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00010894 File Offset: 0x0000EA94
		private void WriteInstanceAnnotations(IEnumerable<ODataInstanceAnnotation> instanceAnnotations, InstanceAnnotationWriteTracker tracker)
		{
			IEnumerable<AtomInstanceAnnotation> enumerable = Enumerable.Select<ODataInstanceAnnotation, AtomInstanceAnnotation>(instanceAnnotations, (ODataInstanceAnnotation instanceAnnotation) => AtomInstanceAnnotation.CreateFrom(instanceAnnotation, null));
			this.atomEntryAndFeedSerializer.WriteInstanceAnnotations(enumerable, tracker);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000108D2 File Offset: 0x0000EAD2
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

		// Token: 0x06000460 RID: 1120 RVA: 0x00010908 File Offset: 0x0000EB08
		private void WriteEntryContent(ODataEntry entry, IEdmEntityType entryType, EntryPropertiesValueCache propertiesValueCache, ProjectedPropertiesAnnotation projectedProperties)
		{
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			if (mediaResource == null)
			{
				this.atomOutputContext.XmlWriter.WriteStartElement("", "content", "http://www.w3.org/2005/Atom");
				this.atomOutputContext.XmlWriter.WriteAttributeString("type", "application/xml");
				this.atomEntryAndFeedSerializer.WriteProperties(entryType, propertiesValueCache.EntryProperties, false, new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesStart), new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesEnd), base.DuplicatePropertyNamesChecker, projectedProperties);
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
			this.atomEntryAndFeedSerializer.WriteProperties(entryType, propertiesValueCache.EntryProperties, false, new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesStart), new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesEnd), base.DuplicatePropertyNamesChecker, projectedProperties);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00010A7C File Offset: 0x0000EC7C
		private void CheckAndWriteParentNavigationLinkStartForInlineElement()
		{
			ODataNavigationLink parentNavigationLink = base.ParentNavigationLink;
			if (parentNavigationLink != null)
			{
				this.WriteNavigationLinkStart(parentNavigationLink, null);
				this.atomOutputContext.XmlWriter.WriteStartElement("m", "inline", "http://docs.oasis-open.org/odata/ns/metadata");
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00010ABC File Offset: 0x0000ECBC
		private void CheckAndWriteParentNavigationLinkEndForInlineElement()
		{
			ODataNavigationLink parentNavigationLink = base.ParentNavigationLink;
			if (parentNavigationLink != null)
			{
				this.atomOutputContext.XmlWriter.WriteEndElement();
				this.WriteNavigationLinkEnd();
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00010AE9 File Offset: 0x0000ECE9
		private void WriteNavigationLinkStart(ODataNavigationLink navigationLink, Uri navigationLinkUrlOverride)
		{
			WriterValidationUtils.ValidateNavigationLinkHasCardinality(navigationLink);
			WriterValidationUtils.ValidateNavigationLinkUrlPresent(navigationLink);
			this.atomEntryAndFeedSerializer.WriteNavigationLinkStart(navigationLink, navigationLinkUrlOverride);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00010B04 File Offset: 0x0000ED04
		private void WriteNavigationLinkEnd()
		{
			this.atomOutputContext.XmlWriter.WriteEndElement();
		}

		// Token: 0x040001FC RID: 508
		private readonly string updatedTime = ODataAtomConvert.ToAtomString(DateTimeOffset.UtcNow);

		// Token: 0x040001FD RID: 509
		private readonly ODataAtomOutputContext atomOutputContext;

		// Token: 0x040001FE RID: 510
		private readonly ODataAtomEntryAndFeedSerializer atomEntryAndFeedSerializer;

		// Token: 0x02000069 RID: 105
		private enum AtomElement
		{
			// Token: 0x04000201 RID: 513
			Id = 1,
			// Token: 0x04000202 RID: 514
			ReadLink,
			// Token: 0x04000203 RID: 515
			EditLink = 4
		}

		// Token: 0x0200006A RID: 106
		private sealed class AtomFeedScope : ODataWriterCore.FeedScope
		{
			// Token: 0x06000466 RID: 1126 RVA: 0x00010B16 File Offset: 0x0000ED16
			internal AtomFeedScope(ODataFeed feed, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(feed, navigationSource, entityType, skipWriting, selectedProperties, odataUri)
			{
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000467 RID: 1127 RVA: 0x00010B27 File Offset: 0x0000ED27
			// (set) Token: 0x06000468 RID: 1128 RVA: 0x00010B2F File Offset: 0x0000ED2F
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

			// Token: 0x04000204 RID: 516
			private bool authorWritten;
		}

		// Token: 0x0200006B RID: 107
		private sealed class AtomEntryScope : ODataWriterCore.EntryScope
		{
			// Token: 0x06000469 RID: 1129 RVA: 0x00010B38 File Offset: 0x0000ED38
			internal AtomEntryScope(ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, bool writingResponse, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(entry, serializationInfo, navigationSource, entityType, skipWriting, writingResponse, writerBehavior, selectedProperties, odataUri, true)
			{
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x00010B5B File Offset: 0x0000ED5B
			internal void SetWrittenElement(ODataAtomWriter.AtomElement atomElement)
			{
				this.alreadyWrittenElements |= (int)atomElement;
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x00010B6B File Offset: 0x0000ED6B
			internal bool IsElementWritten(ODataAtomWriter.AtomElement atomElement)
			{
				return (this.alreadyWrittenElements & (int)atomElement) == (int)atomElement;
			}

			// Token: 0x04000205 RID: 517
			private int alreadyWrittenElements;
		}
	}
}
