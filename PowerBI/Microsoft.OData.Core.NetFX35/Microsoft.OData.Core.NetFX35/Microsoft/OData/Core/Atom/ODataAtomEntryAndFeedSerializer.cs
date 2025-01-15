using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000043 RID: 67
	internal sealed class ODataAtomEntryAndFeedSerializer : ODataAtomPropertyAndValueSerializer
	{
		// Token: 0x06000278 RID: 632 RVA: 0x000098EC File Offset: 0x00007AEC
		internal ODataAtomEntryAndFeedSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
			this.atomEntryMetadataSerializer = new ODataAtomEntryMetadataSerializer(atomOutputContext);
			this.atomFeedMetadataSerializer = new ODataAtomFeedMetadataSerializer(atomOutputContext);
			this.contextUriBuilder = atomOutputContext.CreateContextUriBuilder();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009919 File Offset: 0x00007B19
		internal void WriteEntryPropertiesStart()
		{
			base.XmlWriter.WriteStartElement("m", "properties", "http://docs.oasis-open.org/odata/ns/metadata");
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009935 File Offset: 0x00007B35
		internal void WriteEntryPropertiesEnd()
		{
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00009944 File Offset: 0x00007B44
		internal void WriteEntryTypeName(string typeName, AtomEntryMetadata entryMetadata)
		{
			if (typeName != null)
			{
				AtomCategoryMetadata atomCategoryMetadata = ODataAtomWriterMetadataUtils.MergeCategoryMetadata((entryMetadata == null) ? null : entryMetadata.CategoryWithTypeName, typeName, "http://docs.oasis-open.org/odata/ns/scheme");
				this.atomEntryMetadataSerializer.WriteCategory(atomCategoryMetadata);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009978 File Offset: 0x00007B78
		internal void WriteEntryMetadata(AtomEntryMetadata entryMetadata, string updatedTime)
		{
			this.atomEntryMetadataSerializer.WriteEntryMetadata(entryMetadata, updatedTime);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009988 File Offset: 0x00007B88
		[SuppressMessage("DataWeb.Usage", "AC0010", Justification = "Usage of OriginalString is safe in this context")]
		internal void WriteEntryId(Uri entryId, bool isTransient)
		{
			string text;
			if (isTransient)
			{
				text = AtomUtils.GetTransientId();
			}
			else
			{
				text = ((entryId == null) ? null : entryId.OriginalString);
			}
			base.WriteElementWithTextContent("", "id", "http://www.w3.org/2005/Atom", text);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000099CC File Offset: 0x00007BCC
		internal void WriteEntryReadLink(Uri readLink, AtomEntryMetadata entryMetadata)
		{
			AtomLinkMetadata atomLinkMetadata = ((entryMetadata == null) ? null : entryMetadata.SelfLink);
			this.WriteReadOrEditLink(readLink, atomLinkMetadata, "self");
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000099F4 File Offset: 0x00007BF4
		internal void WriteEntryEditLink(Uri editLink, AtomEntryMetadata entryMetadata)
		{
			AtomLinkMetadata atomLinkMetadata = ((entryMetadata == null) ? null : entryMetadata.EditLink);
			this.WriteReadOrEditLink(editLink, atomLinkMetadata, "edit");
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009A1C File Offset: 0x00007C1C
		internal void WriteEntryMediaEditLink(ODataStreamReferenceValue mediaResource)
		{
			Uri editLink = mediaResource.EditLink;
			if (editLink != null)
			{
				AtomStreamReferenceMetadata annotation = mediaResource.GetAnnotation<AtomStreamReferenceMetadata>();
				AtomLinkMetadata atomLinkMetadata = ((annotation == null) ? null : annotation.EditLink);
				AtomLinkMetadata atomLinkMetadata2 = ODataAtomWriterMetadataUtils.MergeLinkMetadata(atomLinkMetadata, "edit-media", editLink, null, null);
				this.atomEntryMetadataSerializer.WriteAtomLink(atomLinkMetadata2, mediaResource.ETag);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009A70 File Offset: 0x00007C70
		internal void WriteAssociationLink(string navigationPropertyName, Uri associationLinkUrl, AtomLinkMetadata associationLinkMetadata)
		{
			string text = AtomUtils.ComputeODataAssociationLinkRelation(navigationPropertyName);
			AtomLinkMetadata atomLinkMetadata = ODataAtomWriterMetadataUtils.MergeLinkMetadata(associationLinkMetadata, text, associationLinkUrl, navigationPropertyName, "application/xml");
			this.atomEntryMetadataSerializer.WriteAtomLink(atomLinkMetadata, null);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009AA4 File Offset: 0x00007CA4
		internal void WriteNavigationLinkStart(ODataNavigationLink navigationLink, Uri navigationLinkUrlOverride)
		{
			if (navigationLink.AssociationLinkUrl != null)
			{
				this.WriteAssociationLink(navigationLink.Name, navigationLink.AssociationLinkUrl, null);
			}
			base.XmlWriter.WriteStartElement("", "link", "http://www.w3.org/2005/Atom");
			string text = AtomUtils.ComputeODataNavigationLinkRelation(navigationLink);
			string text2 = AtomUtils.ComputeODataNavigationLinkType(navigationLink);
			string name = navigationLink.Name;
			Uri uri = navigationLinkUrlOverride ?? navigationLink.Url;
			AtomLinkMetadata annotation = navigationLink.GetAnnotation<AtomLinkMetadata>();
			AtomLinkMetadata atomLinkMetadata = ODataAtomWriterMetadataUtils.MergeLinkMetadata(annotation, text, uri, name, text2);
			this.atomEntryMetadataSerializer.WriteAtomLinkAttributes(atomLinkMetadata, null);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009B30 File Offset: 0x00007D30
		internal void WriteFeedMetadata(ODataFeed feed, string updatedTime, out bool authorWritten)
		{
			AtomFeedMetadata annotation = feed.GetAnnotation<AtomFeedMetadata>();
			if (annotation == null)
			{
				base.WriteElementWithTextContent("", "id", "http://www.w3.org/2005/Atom", UriUtils.UriToString(feed.Id));
				base.WriteEmptyElement("", "title", "http://www.w3.org/2005/Atom");
				base.WriteElementWithTextContent("", "updated", "http://www.w3.org/2005/Atom", updatedTime);
				authorWritten = false;
				return;
			}
			this.atomFeedMetadataSerializer.WriteFeedMetadata(annotation, feed, updatedTime, out authorWritten);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009BA5 File Offset: 0x00007DA5
		internal void WriteFeedDefaultAuthor()
		{
			this.atomFeedMetadataSerializer.WriteEmptyAuthor();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009BC0 File Offset: 0x00007DC0
		internal void WriteFeedNextPageLink(ODataFeed feed)
		{
			Uri nextPageLink = feed.NextPageLink;
			if (nextPageLink != null)
			{
				this.WriteFeedLink(feed, "next", nextPageLink, delegate(AtomFeedMetadata feedMetadata)
				{
					if (feedMetadata != null)
					{
						return feedMetadata.NextPageLink;
					}
					return null;
				});
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009C48 File Offset: 0x00007E48
		internal void WriteFeedDeltaLink(ODataFeed feed)
		{
			Uri deltaLink = feed.DeltaLink;
			if (deltaLink != null)
			{
				this.WriteFeedLink(feed, "http://docs.oasis-open.org/odata/ns/delta", deltaLink, delegate(AtomFeedMetadata feedMetadata)
				{
					if (feedMetadata != null)
					{
						return Enumerable.FirstOrDefault<AtomLinkMetadata>(feedMetadata.Links, (AtomLinkMetadata link) => link.Relation == "http://docs.oasis-open.org/odata/ns/delta");
					}
					return null;
				});
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009C90 File Offset: 0x00007E90
		internal void WriteFeedLink(ODataFeed feed, string relation, Uri href, Func<AtomFeedMetadata, AtomLinkMetadata> getLinkMetadata)
		{
			AtomFeedMetadata annotation = feed.GetAnnotation<AtomFeedMetadata>();
			AtomLinkMetadata atomLinkMetadata = ODataAtomWriterMetadataUtils.MergeLinkMetadata(getLinkMetadata.Invoke(annotation), relation, href, null, null);
			this.atomFeedMetadataSerializer.WriteAtomLink(atomLinkMetadata, null);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009CC4 File Offset: 0x00007EC4
		internal void WriteStreamProperty(ODataProperty streamProperty, IEdmEntityType owningType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			WriterValidationUtils.ValidatePropertyNotNull(streamProperty);
			string name = streamProperty.Name;
			if (projectedProperties.ShouldSkipProperty(name))
			{
				return;
			}
			WriterValidationUtils.ValidatePropertyName(name);
			duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(streamProperty);
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(streamProperty.Name, owningType, true);
			WriterValidationUtils.ValidateStreamReferenceProperty(streamProperty, edmProperty, base.WritingResponse);
			ODataStreamReferenceValue odataStreamReferenceValue = (ODataStreamReferenceValue)streamProperty.Value;
			WriterValidationUtils.ValidateStreamReferenceValue(odataStreamReferenceValue, false);
			if (owningType != null && owningType.IsOpen && edmProperty == null)
			{
				ValidationUtils.ValidateOpenPropertyValue(streamProperty.Name, odataStreamReferenceValue);
			}
			AtomStreamReferenceMetadata annotation = odataStreamReferenceValue.GetAnnotation<AtomStreamReferenceMetadata>();
			string contentType = odataStreamReferenceValue.ContentType;
			string name2 = streamProperty.Name;
			Uri readLink = odataStreamReferenceValue.ReadLink;
			if (readLink != null)
			{
				string text = AtomUtils.ComputeStreamPropertyRelation(streamProperty, false);
				AtomLinkMetadata atomLinkMetadata = ((annotation == null) ? null : annotation.SelfLink);
				AtomLinkMetadata atomLinkMetadata2 = ODataAtomWriterMetadataUtils.MergeLinkMetadata(atomLinkMetadata, text, readLink, name2, contentType);
				this.atomEntryMetadataSerializer.WriteAtomLink(atomLinkMetadata2, null);
			}
			Uri editLink = odataStreamReferenceValue.EditLink;
			if (editLink != null)
			{
				string text2 = AtomUtils.ComputeStreamPropertyRelation(streamProperty, true);
				AtomLinkMetadata atomLinkMetadata3 = ((annotation == null) ? null : annotation.EditLink);
				AtomLinkMetadata atomLinkMetadata4 = ODataAtomWriterMetadataUtils.MergeLinkMetadata(atomLinkMetadata3, text2, editLink, name2, contentType);
				this.atomEntryMetadataSerializer.WriteAtomLink(atomLinkMetadata4, odataStreamReferenceValue.ETag);
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009DEC File Offset: 0x00007FEC
		internal void WriteOperation(ODataOperation operation)
		{
			WriterValidationUtils.ValidateCanWriteOperation(operation, base.WritingResponse);
			ValidationUtils.ValidateOperationMetadataNotNull(operation);
			ValidationUtils.ValidateOperationTargetNotNull(operation);
			string text;
			if (operation is ODataAction)
			{
				text = "action";
			}
			else
			{
				text = "function";
			}
			base.XmlWriter.WriteStartElement("m", text, "http://docs.oasis-open.org/odata/ns/metadata");
			string text2 = base.UriToUrlAttributeValue(operation.Metadata, false);
			base.XmlWriter.WriteAttributeString("metadata", text2);
			if (operation.Title != null)
			{
				base.XmlWriter.WriteAttributeString("title", operation.Title);
			}
			string text3 = base.UriToUrlAttributeValue(operation.Target);
			base.XmlWriter.WriteAttributeString("target", text3);
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009EA4 File Offset: 0x000080A4
		internal void TryWriteEntryContextUri(ODataFeedAndEntryTypeContext typeContext)
		{
			ODataUri odataUri = base.AtomOutputContext.MessageWriterSettings.ODataUri;
			base.WriteContextUriProperty(this.contextUriBuilder.BuildContextUri(ODataPayloadKind.Entry, ODataContextUrlInfo.Create(typeContext, true, odataUri)));
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009EDC File Offset: 0x000080DC
		internal void TryWriteFeedContextUri(ODataFeedAndEntryTypeContext typeContext)
		{
			ODataUri odataUri = base.AtomOutputContext.MessageWriterSettings.ODataUri;
			base.WriteContextUriProperty(this.contextUriBuilder.BuildContextUri(ODataPayloadKind.Feed, ODataContextUrlInfo.Create(typeContext, false, odataUri)));
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009F14 File Offset: 0x00008114
		private void WriteReadOrEditLink(Uri link, AtomLinkMetadata linkMetadata, string linkRelation)
		{
			if (link != null)
			{
				AtomLinkMetadata atomLinkMetadata = ODataAtomWriterMetadataUtils.MergeLinkMetadata(linkMetadata, linkRelation, link, null, null);
				this.atomEntryMetadataSerializer.WriteAtomLink(atomLinkMetadata, null);
			}
		}

		// Token: 0x04000165 RID: 357
		private readonly ODataContextUriBuilder contextUriBuilder;

		// Token: 0x04000166 RID: 358
		private readonly ODataAtomEntryMetadataSerializer atomEntryMetadataSerializer;

		// Token: 0x04000167 RID: 359
		private readonly ODataAtomFeedMetadataSerializer atomFeedMetadataSerializer;
	}
}
