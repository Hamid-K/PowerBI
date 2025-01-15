using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000E1 RID: 225
	internal sealed class ODataJsonLightEntryAndFeedSerializer : ODataJsonLightPropertySerializer
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x0001EBC6 File Offset: 0x0001CDC6
		internal ODataJsonLightEntryAndFeedSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext, true)
		{
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0001EBD0 File Offset: 0x0001CDD0
		private Uri MetadataDocumentBaseUri
		{
			get
			{
				return base.JsonLightOutputContext.MessageWriterSettings.MetadataDocumentUri;
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		internal void WriteEntryStartMetadataProperties(IODataJsonLightWriterEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			string entryTypeNameForWriting = base.JsonLightOutputContext.TypeNameOracle.GetEntryTypeNameForWriting(entryState.GetOrCreateTypeContext(base.Model, base.WritingResponse).ExpectedEntityTypeName, entry);
			if (entryTypeNameForWriting != null)
			{
				base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(entryTypeNameForWriting);
			}
			Uri uri;
			if (entry.MetadataBuilder.TryGetIdForSerialization(out uri))
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.id");
				if (uri != null && !entry.HasNonComputedId)
				{
					uri = this.MetadataDocumentBaseUri.MakeRelativeUri(uri);
				}
				base.JsonWriter.WriteValue((uri == null) ? null : base.UriToString(uri));
			}
			string etag = entry.ETag;
			if (etag != null)
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.etag");
				base.JsonWriter.WriteValue(etag);
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001ECB4 File Offset: 0x0001CEB4
		internal void WriteEntryMetadataProperties(IODataJsonLightWriterEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			Uri editLink = entry.EditLink;
			if (editLink != null && !entryState.EditLinkWritten)
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.editLink");
				base.JsonWriter.WriteValue(base.UriToString(entry.HasNonComputedEditLink ? editLink : this.MetadataDocumentBaseUri.MakeRelativeUri(editLink)));
				entryState.EditLinkWritten = true;
			}
			Uri readLink = entry.ReadLink;
			if (readLink != null && readLink != editLink && !entryState.ReadLinkWritten)
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.readLink");
				base.JsonWriter.WriteValue(base.UriToString(entry.HasNonComputedReadLink ? readLink : this.MetadataDocumentBaseUri.MakeRelativeUri(readLink)));
				entryState.ReadLinkWritten = true;
			}
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			if (mediaResource != null)
			{
				Uri editLink2 = mediaResource.EditLink;
				if (editLink2 != null && !entryState.MediaEditLinkWritten)
				{
					base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.mediaEditLink");
					base.JsonWriter.WriteValue(base.UriToString(mediaResource.HasNonComputedEditLink ? editLink2 : this.MetadataDocumentBaseUri.MakeRelativeUri(editLink2)));
					entryState.MediaEditLinkWritten = true;
				}
				Uri readLink2 = mediaResource.ReadLink;
				if (readLink2 != null && readLink2 != editLink2 && !entryState.MediaReadLinkWritten)
				{
					base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.mediaReadLink");
					base.JsonWriter.WriteValue(base.UriToString(mediaResource.HasNonComputedReadLink ? readLink2 : this.MetadataDocumentBaseUri.MakeRelativeUri(readLink2)));
					entryState.MediaReadLinkWritten = true;
				}
				string contentType = mediaResource.ContentType;
				if (contentType != null && !entryState.MediaContentTypeWritten)
				{
					base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.mediaContentType");
					base.JsonWriter.WriteValue(contentType);
					entryState.MediaContentTypeWritten = true;
				}
				string etag = mediaResource.ETag;
				if (etag != null && !entryState.MediaETagWritten)
				{
					base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.mediaEtag");
					base.JsonWriter.WriteValue(etag);
					entryState.MediaETagWritten = true;
				}
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001EEC0 File Offset: 0x0001D0C0
		internal void WriteEntryEndMetadataProperties(IODataJsonLightWriterEntryState entryState, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			ODataEntry entry = entryState.Entry;
			for (ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = entry.MetadataBuilder.GetNextUnprocessedNavigationLink(); odataJsonLightReaderNavigationLinkInfo != null; odataJsonLightReaderNavigationLinkInfo = entry.MetadataBuilder.GetNextUnprocessedNavigationLink())
			{
				odataJsonLightReaderNavigationLinkInfo.NavigationLink.MetadataBuilder = entry.MetadataBuilder;
				this.WriteNavigationLinkMetadata(odataJsonLightReaderNavigationLinkInfo.NavigationLink, duplicatePropertyNamesChecker);
			}
			IEnumerable<ODataAction> actions = entry.Actions;
			if (actions != null && Enumerable.Any<ODataAction>(actions))
			{
				this.WriteOperations(Enumerable.Cast<ODataOperation>(actions), true);
			}
			IEnumerable<ODataFunction> functions = entry.Functions;
			if (functions != null && Enumerable.Any<ODataFunction>(functions))
			{
				this.WriteOperations(Enumerable.Cast<ODataOperation>(functions), false);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001EF50 File Offset: 0x0001D150
		internal void WriteNavigationLinkMetadata(ODataNavigationLink navigationLink, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			Uri url = navigationLink.Url;
			string name = navigationLink.Name;
			Uri associationLinkUrl = navigationLink.AssociationLinkUrl;
			if (associationLinkUrl != null)
			{
				duplicatePropertyNamesChecker.CheckForDuplicateAssociationLinkNames(name, null);
				this.WriteAssociationLink(navigationLink.Name, associationLinkUrl);
			}
			if (url != null)
			{
				base.ODataAnnotationWriter.WritePropertyAnnotationName(name, "odata.navigationLink");
				base.JsonWriter.WriteValue(base.UriToString(url));
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001EFD0 File Offset: 0x0001D1D0
		internal void WriteNavigationLinkContextUrl(ODataNavigationLink navigationLink, ODataContextUrlInfo contextUrlInfo)
		{
			base.WriteContextUriProperty(ODataPayloadKind.Entry, () => contextUrlInfo, null, navigationLink.Name);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001F068 File Offset: 0x0001D268
		internal void WriteOperations(IEnumerable<ODataOperation> operations, bool isAction)
		{
			IEnumerable<IGrouping<string, ODataOperation>> enumerable = Enumerable.GroupBy<ODataOperation, string>(operations, delegate(ODataOperation o)
			{
				ValidationUtils.ValidateOperationNotNull(o, isAction);
				this.WriterValidator.ValidateCanWriteOperation(o, this.JsonLightOutputContext.WritingResponse);
				ODataJsonLightValidationUtils.ValidateOperation(this.MetadataDocumentBaseUri, o);
				return this.GetOperationMetadataString(o);
			});
			foreach (IGrouping<string, ODataOperation> grouping in enumerable)
			{
				this.WriteOperationMetadataGroup(grouping);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001F0FC File Offset: 0x0001D2FC
		internal ODataContextUrlInfo WriteDeltaContextUri(ODataFeedAndEntryTypeContext typeContext, ODataDeltaKind kind, ODataContextUrlInfo parentContextUrlInfo = null)
		{
			ODataUri odataUri = base.JsonLightOutputContext.MessageWriterSettings.ODataUri;
			return base.WriteContextUriProperty(ODataPayloadKind.Delta, () => ODataContextUrlInfo.Create(typeContext, kind, odataUri), parentContextUrlInfo, null);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001F168 File Offset: 0x0001D368
		internal void WriteEntryContextUri(ODataFeedAndEntryTypeContext typeContext, ODataContextUrlInfo parentContextUrlInfo = null)
		{
			ODataUri odataUri = base.JsonLightOutputContext.MessageWriterSettings.ODataUri;
			base.WriteContextUriProperty(ODataPayloadKind.Entry, () => ODataContextUrlInfo.Create(typeContext, true, odataUri), parentContextUrlInfo, null);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001F1CC File Offset: 0x0001D3CC
		internal ODataContextUrlInfo WriteFeedContextUri(ODataFeedAndEntryTypeContext typeContext)
		{
			ODataUri odataUri = base.JsonLightOutputContext.MessageWriterSettings.ODataUri;
			return base.WriteContextUriProperty(ODataPayloadKind.Feed, () => ODataContextUrlInfo.Create(typeContext, false, odataUri), null, null);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001F211 File Offset: 0x0001D411
		private void WriteAssociationLink(string propertyName, Uri associationLinkUrl)
		{
			base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.associationLink");
			base.JsonWriter.WriteValue(base.UriToString(associationLinkUrl));
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001F238 File Offset: 0x0001D438
		private string GetOperationMetadataString(ODataOperation operation)
		{
			string text = UriUtils.UriToString(operation.Metadata);
			if (this.MetadataDocumentBaseUri == null)
			{
				return operation.Metadata.Fragment;
			}
			return '#' + ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(this.MetadataDocumentBaseUri, text);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001F283 File Offset: 0x0001D483
		private string GetOperationTargetUriString(ODataOperation operation)
		{
			if (!(operation.Target == null))
			{
				return base.UriToString(operation.Target);
			}
			return null;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
		private void ValidateOperationMetadataGroup(IGrouping<string, ODataOperation> operations)
		{
			if (Enumerable.Count<ODataOperation>(operations) > 1)
			{
				if (Enumerable.Any<ODataOperation>(operations, (ODataOperation o) => o.Target == null))
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustSpecifyTarget(operations.Key));
				}
			}
			foreach (IGrouping<string, ODataOperation> grouping in Enumerable.GroupBy<ODataOperation, string>(operations, new Func<ODataOperation, string>(this.GetOperationTargetUriString)))
			{
				if (Enumerable.Count<ODataOperation>(grouping) > 1)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget(operations.Key, grouping.Key));
				}
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001F364 File Offset: 0x0001D564
		private void WriteOperationMetadataGroup(IGrouping<string, ODataOperation> operations)
		{
			this.ValidateOperationMetadataGroup(operations);
			base.JsonLightOutputContext.JsonWriter.WriteName(operations.Key);
			bool flag = Enumerable.Count<ODataOperation>(operations) > 1;
			if (flag)
			{
				base.JsonLightOutputContext.JsonWriter.StartArrayScope();
			}
			foreach (ODataOperation odataOperation in operations)
			{
				this.WriteOperation(odataOperation);
			}
			if (flag)
			{
				base.JsonLightOutputContext.JsonWriter.EndArrayScope();
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		private void WriteOperation(ODataOperation operation)
		{
			base.JsonLightOutputContext.JsonWriter.StartObjectScope();
			if (operation.Title != null)
			{
				base.JsonLightOutputContext.JsonWriter.WriteName("title");
				base.JsonLightOutputContext.JsonWriter.WriteValue(operation.Title);
			}
			if (operation.Target != null)
			{
				string operationTargetUriString = this.GetOperationTargetUriString(operation);
				base.JsonLightOutputContext.JsonWriter.WriteName("target");
				base.JsonLightOutputContext.JsonWriter.WriteValue(operationTargetUriString);
			}
			base.JsonLightOutputContext.JsonWriter.EndObjectScope();
		}
	}
}
