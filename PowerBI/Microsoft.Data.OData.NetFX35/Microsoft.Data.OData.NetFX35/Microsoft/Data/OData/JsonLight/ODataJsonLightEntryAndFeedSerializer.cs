using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000190 RID: 400
	internal sealed class ODataJsonLightEntryAndFeedSerializer : ODataJsonLightPropertySerializer
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x000271BF File Offset: 0x000253BF
		internal ODataJsonLightEntryAndFeedSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext)
		{
			this.annotationGroups = new Dictionary<string, ODataJsonLightAnnotationGroup>(StringComparer.Ordinal);
			this.metadataUriBuilder = jsonLightOutputContext.CreateMetadataUriBuilder();
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x000271E4 File Offset: 0x000253E4
		private Uri MetadataDocumentBaseUri
		{
			get
			{
				return this.metadataUriBuilder.BaseUri;
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000271F4 File Offset: 0x000253F4
		internal void WriteAnnotationGroup(ODataEntry entry)
		{
			ODataJsonLightAnnotationGroup annotation = entry.GetAnnotation<ODataJsonLightAnnotationGroup>();
			if (annotation == null)
			{
				return;
			}
			if (!base.JsonLightOutputContext.WritingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_AnnotationGroupInRequest);
			}
			string name = annotation.Name;
			if (string.IsNullOrEmpty(name))
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_AnnotationGroupWithoutName);
			}
			ODataJsonLightAnnotationGroup odataJsonLightAnnotationGroup;
			if (!this.annotationGroups.TryGetValue(name, ref odataJsonLightAnnotationGroup))
			{
				base.JsonWriter.WriteName("odata.annotationGroup");
				base.JsonWriter.StartObjectScope();
				base.JsonWriter.WriteName("name");
				base.JsonWriter.WritePrimitiveValue(name, base.JsonLightOutputContext.Version);
				if (annotation.Annotations != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in annotation.Annotations)
					{
						string key = keyValuePair.Key;
						if (key.Length == 0)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberWithoutName(annotation.Name));
						}
						if (!ODataJsonLightReaderUtils.IsAnnotationProperty(key))
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberMustBeAnnotation(key, annotation.Name));
						}
						base.JsonWriter.WriteName(key);
						object value = keyValuePair.Value;
						string text = value as string;
						if (text == null && value != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberWithInvalidValue(key, annotation.Name, value.GetType().FullName));
						}
						base.JsonWriter.WritePrimitiveValue(text, base.JsonLightOutputContext.Version);
					}
				}
				base.JsonWriter.EndObjectScope();
				this.annotationGroups.Add(name, annotation);
				return;
			}
			if (!object.ReferenceEquals(odataJsonLightAnnotationGroup, annotation))
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedSerializer_DuplicateAnnotationGroup(name));
			}
			base.JsonWriter.WriteName("odata.annotationGroupReference");
			base.JsonWriter.WritePrimitiveValue(name, base.JsonLightOutputContext.Version);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000273D4 File Offset: 0x000255D4
		internal void WriteEntryStartMetadataProperties(IODataJsonLightWriterEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			string entryTypeNameForWriting = base.JsonLightOutputContext.TypeNameOracle.GetEntryTypeNameForWriting(entryState.GetOrCreateTypeContext(base.Model, base.WritingResponse).ExpectedEntityTypeName, entry);
			if (entryTypeNameForWriting != null)
			{
				base.JsonWriter.WriteName("odata.type");
				base.JsonWriter.WriteValue(entryTypeNameForWriting);
			}
			string id = entry.Id;
			if (id != null)
			{
				base.JsonWriter.WriteName("odata.id");
				base.JsonWriter.WriteValue(id);
			}
			string etag = entry.ETag;
			if (etag != null)
			{
				base.JsonWriter.WriteName("odata.etag");
				base.JsonWriter.WriteValue(etag);
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002747C File Offset: 0x0002567C
		internal void WriteEntryMetadataProperties(IODataJsonLightWriterEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			Uri editLink = entry.EditLink;
			if (editLink != null && !entryState.EditLinkWritten)
			{
				base.JsonWriter.WriteName("odata.editLink");
				base.JsonWriter.WriteValue(base.UriToString(editLink));
				entryState.EditLinkWritten = true;
			}
			Uri readLink = entry.ReadLink;
			if (readLink != null && !entryState.ReadLinkWritten)
			{
				base.JsonWriter.WriteName("odata.readLink");
				base.JsonWriter.WriteValue(base.UriToString(readLink));
				entryState.ReadLinkWritten = true;
			}
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			if (mediaResource != null)
			{
				Uri editLink2 = mediaResource.EditLink;
				if (editLink2 != null && !entryState.MediaEditLinkWritten)
				{
					base.JsonWriter.WriteName("odata.mediaEditLink");
					base.JsonWriter.WriteValue(base.UriToString(editLink2));
					entryState.MediaEditLinkWritten = true;
				}
				Uri readLink2 = mediaResource.ReadLink;
				if (readLink2 != null && !entryState.MediaReadLinkWritten)
				{
					base.JsonWriter.WriteName("odata.mediaReadLink");
					base.JsonWriter.WriteValue(base.UriToString(readLink2));
					entryState.MediaReadLinkWritten = true;
				}
				string contentType = mediaResource.ContentType;
				if (contentType != null && !entryState.MediaContentTypeWritten)
				{
					base.JsonWriter.WriteName("odata.mediaContentType");
					base.JsonWriter.WriteValue(contentType);
					entryState.MediaContentTypeWritten = true;
				}
				string etag = mediaResource.ETag;
				if (etag != null && !entryState.MediaETagWritten)
				{
					base.JsonWriter.WriteName("odata.mediaETag");
					base.JsonWriter.WriteValue(etag);
					entryState.MediaETagWritten = true;
				}
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00027618 File Offset: 0x00025818
		internal void WriteEntryEndMetadataProperties(IODataJsonLightWriterEntryState entryState, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			ODataEntry entry = entryState.Entry;
			for (ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = entry.MetadataBuilder.GetNextUnprocessedNavigationLink(); odataJsonLightReaderNavigationLinkInfo != null; odataJsonLightReaderNavigationLinkInfo = entry.MetadataBuilder.GetNextUnprocessedNavigationLink())
			{
				odataJsonLightReaderNavigationLinkInfo.NavigationLink.SetMetadataBuilder(entry.MetadataBuilder);
				this.WriteNavigationLinkMetadata(odataJsonLightReaderNavigationLinkInfo.NavigationLink, duplicatePropertyNamesChecker);
			}
			IEnumerable<ODataAction> actions = entry.Actions;
			if (actions != null)
			{
				this.WriteOperations(Enumerable.Cast<ODataOperation>(actions), true);
			}
			IEnumerable<ODataFunction> functions = entry.Functions;
			if (functions != null)
			{
				this.WriteOperations(Enumerable.Cast<ODataOperation>(functions), false);
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00027698 File Offset: 0x00025898
		internal void WriteNavigationLinkMetadata(ODataNavigationLink navigationLink, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			Uri url = navigationLink.Url;
			string name = navigationLink.Name;
			if (url != null)
			{
				base.JsonWriter.WritePropertyAnnotationName(name, "odata.navigationLinkUrl");
				base.JsonWriter.WriteValue(base.UriToString(url));
			}
			Uri associationLinkUrl = navigationLink.AssociationLinkUrl;
			if (associationLinkUrl != null)
			{
				duplicatePropertyNamesChecker.CheckForDuplicateAssociationLinkNames(new ODataAssociationLink
				{
					Name = name
				});
				this.WriteAssociationLink(navigationLink.Name, associationLinkUrl);
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00027768 File Offset: 0x00025968
		internal void WriteOperations(IEnumerable<ODataOperation> operations, bool isAction)
		{
			IEnumerable<IGrouping<string, ODataOperation>> enumerable = Enumerable.GroupBy<ODataOperation, string>(operations, delegate(ODataOperation o)
			{
				ValidationUtils.ValidateOperationNotNull(o, isAction);
				WriterValidationUtils.ValidateCanWriteOperation(o, this.JsonLightOutputContext.WritingResponse);
				ODataJsonLightValidationUtils.ValidateOperation(this.MetadataDocumentBaseUri, o);
				return this.GetOperationMetadataString(o);
			});
			foreach (IGrouping<string, ODataOperation> grouping in enumerable)
			{
				this.WriteOperationMetadataGroup(grouping);
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000277D8 File Offset: 0x000259D8
		internal void TryWriteEntryMetadataUri(ODataFeedAndEntryTypeContext typeContext)
		{
			Uri uri;
			if (this.metadataUriBuilder.TryBuildEntryMetadataUri(typeContext, out uri))
			{
				base.WriteMetadataUriProperty(uri);
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000277FC File Offset: 0x000259FC
		internal void TryWriteFeedMetadataUri(ODataFeedAndEntryTypeContext typeContext)
		{
			Uri uri;
			if (this.metadataUriBuilder.TryBuildFeedMetadataUri(typeContext, out uri))
			{
				base.WriteMetadataUriProperty(uri);
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00027820 File Offset: 0x00025A20
		private void WriteAssociationLink(string propertyName, Uri associationLinkUrl)
		{
			base.JsonWriter.WritePropertyAnnotationName(propertyName, "odata.associationLinkUrl");
			base.JsonWriter.WriteValue(base.UriToString(associationLinkUrl));
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00027848 File Offset: 0x00025A48
		private string GetOperationMetadataString(ODataOperation operation)
		{
			string text = UriUtilsCommon.UriToString(operation.Metadata);
			if (this.MetadataDocumentBaseUri == null)
			{
				return operation.Metadata.Fragment;
			}
			return '#' + ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(this.MetadataDocumentBaseUri, text);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00027893 File Offset: 0x00025A93
		private string GetOperationTargetUriString(ODataOperation operation)
		{
			if (!(operation.Target == null))
			{
				return base.UriToString(operation.Target);
			}
			return null;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000278C0 File Offset: 0x00025AC0
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

		// Token: 0x06000B29 RID: 2857 RVA: 0x00027974 File Offset: 0x00025B74
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

		// Token: 0x06000B2A RID: 2858 RVA: 0x00027A0C File Offset: 0x00025C0C
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

		// Token: 0x0400041D RID: 1053
		private readonly Dictionary<string, ODataJsonLightAnnotationGroup> annotationGroups;

		// Token: 0x0400041E RID: 1054
		private readonly ODataJsonLightMetadataUriBuilder metadataUriBuilder;
	}
}
