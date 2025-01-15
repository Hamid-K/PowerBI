using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200020B RID: 523
	internal sealed class ODataJsonLightResourceSerializer : ODataJsonLightPropertySerializer
	{
		// Token: 0x060014E4 RID: 5348 RVA: 0x0003DD66 File Offset: 0x0003BF66
		internal ODataJsonLightResourceSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext, true)
		{
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0003DD70 File Offset: 0x0003BF70
		private Uri MetadataDocumentBaseUri
		{
			get
			{
				return base.JsonLightOutputContext.MessageWriterSettings.MetadataDocumentUri;
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0003DD84 File Offset: 0x0003BF84
		internal void WriteResourceSetStartMetadataProperties(ODataResourceSet resourceSet, string propertyName, string expectedResourceTypeName, bool isUndeclared)
		{
			string resourceSetTypeNameForForWriting = base.JsonLightOutputContext.TypeNameOracle.GetResourceSetTypeNameForForWriting(expectedResourceTypeName, resourceSet, isUndeclared);
			if (resourceSetTypeNameForForWriting != null && !resourceSetTypeNameForForWriting.Contains("Edm.Untyped"))
			{
				if (propertyName == null)
				{
					base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(resourceSetTypeNameForForWriting, false);
					return;
				}
				base.ODataAnnotationWriter.WriteODataTypePropertyAnnotation(propertyName, resourceSetTypeNameForForWriting);
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0003DDD4 File Offset: 0x0003BFD4
		internal void WriteResourceStartMetadataProperties(IODataJsonLightWriterResourceState resourceState)
		{
			ODataResource resource = resourceState.Resource;
			string text;
			if (base.WritingResponse)
			{
				text = resourceState.GetOrCreateTypeContext(base.WritingResponse).ExpectedResourceTypeName;
			}
			else if (resourceState.ResourceTypeFromMetadata == null)
			{
				text = ((resourceState.SerializationInfo == null) ? null : resourceState.SerializationInfo.ExpectedTypeName);
			}
			else
			{
				text = resourceState.ResourceTypeFromMetadata.FullTypeName();
			}
			string resourceTypeNameForWriting = base.JsonLightOutputContext.TypeNameOracle.GetResourceTypeNameForWriting(text, resource, resourceState.IsUndeclared);
			if (resourceTypeNameForWriting != null && !string.Equals(resourceTypeNameForWriting, "Edm.Untyped", 4))
			{
				base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(resourceTypeNameForWriting, false);
			}
			Uri uri;
			if (resource.MetadataBuilder.TryGetIdForSerialization(out uri))
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.id");
				if (uri != null && !resource.HasNonComputedId)
				{
					uri = this.MetadataDocumentBaseUri.MakeRelativeUri(uri);
				}
				base.JsonWriter.WriteValue((uri == null) ? null : base.UriToString(uri));
			}
			string etag = resource.ETag;
			if (etag != null)
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.etag");
				base.JsonWriter.WriteValue(etag);
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0003DEF0 File Offset: 0x0003C0F0
		internal void WriteResourceMetadataProperties(IODataJsonLightWriterResourceState resourceState)
		{
			ODataResource resource = resourceState.Resource;
			Uri editLink = resource.EditLink;
			if (editLink != null && !resourceState.EditLinkWritten)
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.editLink");
				base.JsonWriter.WriteValue(base.UriToString(resource.HasNonComputedEditLink ? editLink : this.MetadataDocumentBaseUri.MakeRelativeUri(editLink)));
				resourceState.EditLinkWritten = true;
			}
			Uri readLink = resource.ReadLink;
			if (readLink != null && readLink != editLink && !resourceState.ReadLinkWritten)
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.readLink");
				base.JsonWriter.WriteValue(base.UriToString(resource.HasNonComputedReadLink ? readLink : this.MetadataDocumentBaseUri.MakeRelativeUri(readLink)));
				resourceState.ReadLinkWritten = true;
			}
			ODataStreamReferenceValue mediaResource = resource.MediaResource;
			if (mediaResource != null)
			{
				Uri editLink2 = mediaResource.EditLink;
				if (editLink2 != null && !resourceState.MediaEditLinkWritten)
				{
					base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.mediaEditLink");
					base.JsonWriter.WriteValue(base.UriToString(mediaResource.HasNonComputedEditLink ? editLink2 : this.MetadataDocumentBaseUri.MakeRelativeUri(editLink2)));
					resourceState.MediaEditLinkWritten = true;
				}
				Uri readLink2 = mediaResource.ReadLink;
				if (readLink2 != null && readLink2 != editLink2 && !resourceState.MediaReadLinkWritten)
				{
					base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.mediaReadLink");
					base.JsonWriter.WriteValue(base.UriToString(mediaResource.HasNonComputedReadLink ? readLink2 : this.MetadataDocumentBaseUri.MakeRelativeUri(readLink2)));
					resourceState.MediaReadLinkWritten = true;
				}
				string contentType = mediaResource.ContentType;
				if (contentType != null && !resourceState.MediaContentTypeWritten)
				{
					base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.mediaContentType");
					base.JsonWriter.WriteValue(contentType);
					resourceState.MediaContentTypeWritten = true;
				}
				string etag = mediaResource.ETag;
				if (etag != null && !resourceState.MediaETagWritten)
				{
					base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.mediaEtag");
					base.JsonWriter.WriteValue(etag);
					resourceState.MediaETagWritten = true;
				}
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0003E0FC File Offset: 0x0003C2FC
		internal void WriteResourceEndMetadataProperties(IODataJsonLightWriterResourceState resourceState, IDuplicatePropertyNameChecker duplicatePropertyNameChecker)
		{
			ODataResource resource = resourceState.Resource;
			for (ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo = resource.MetadataBuilder.GetNextUnprocessedNavigationLink(); odataJsonLightReaderNestedResourceInfo != null; odataJsonLightReaderNestedResourceInfo = resource.MetadataBuilder.GetNextUnprocessedNavigationLink())
			{
				odataJsonLightReaderNestedResourceInfo.NestedResourceInfo.MetadataBuilder = resource.MetadataBuilder;
				this.WriteNavigationLinkMetadata(odataJsonLightReaderNestedResourceInfo.NestedResourceInfo, duplicatePropertyNameChecker);
			}
			IEnumerable<ODataAction> actions = resource.Actions;
			if (actions != null && Enumerable.Any<ODataAction>(actions))
			{
				this.WriteOperations(Enumerable.Cast<ODataOperation>(actions), true);
			}
			IEnumerable<ODataFunction> functions = resource.Functions;
			if (functions != null && Enumerable.Any<ODataFunction>(functions))
			{
				this.WriteOperations(Enumerable.Cast<ODataOperation>(functions), false);
			}
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0003E18C File Offset: 0x0003C38C
		internal void WriteNavigationLinkMetadata(ODataNestedResourceInfo nestedResourceInfo, IDuplicatePropertyNameChecker duplicatePropertyNameChecker)
		{
			Uri url = nestedResourceInfo.Url;
			string name = nestedResourceInfo.Name;
			Uri associationLinkUrl = nestedResourceInfo.AssociationLinkUrl;
			if (associationLinkUrl != null)
			{
				duplicatePropertyNameChecker.ValidatePropertyOpenForAssociationLink(name);
				this.WriteAssociationLink(nestedResourceInfo.Name, associationLinkUrl);
			}
			if (url != null)
			{
				base.ODataAnnotationWriter.WritePropertyAnnotationName(name, "odata.navigationLink");
				base.JsonWriter.WriteValue(base.UriToString(url));
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
		internal void WriteNestedResourceInfoContextUrl(ODataNestedResourceInfo nestedResourceInfo, ODataContextUrlInfo contextUrlInfo)
		{
			base.WriteContextUriProperty(ODataPayloadKind.Resource, () => contextUrlInfo, null, nestedResourceInfo.Name);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0003E230 File Offset: 0x0003C430
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

		// Token: 0x060014ED RID: 5357 RVA: 0x0003E2A0 File Offset: 0x0003C4A0
		internal ODataContextUrlInfo WriteDeltaContextUri(ODataResourceTypeContext typeContext, ODataDeltaKind kind, ODataContextUrlInfo parentContextUrlInfo = null)
		{
			ODataUri odataUri = base.JsonLightOutputContext.MessageWriterSettings.ODataUri;
			return base.WriteContextUriProperty(ODataPayloadKind.Delta, () => ODataContextUrlInfo.Create(typeContext, kind, odataUri), parentContextUrlInfo, null);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0003E2F0 File Offset: 0x0003C4F0
		internal ODataContextUrlInfo WriteResourceContextUri(ODataResourceTypeContext typeContext, ODataContextUrlInfo parentContextUrlInfo = null)
		{
			ODataUri odataUri = base.JsonLightOutputContext.MessageWriterSettings.ODataUri;
			return base.WriteContextUriProperty(ODataPayloadKind.Resource, () => ODataContextUrlInfo.Create(typeContext, true, odataUri), parentContextUrlInfo, null);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0003E338 File Offset: 0x0003C538
		internal ODataContextUrlInfo WriteResourceSetContextUri(ODataResourceTypeContext typeContext)
		{
			ODataUri odataUri = base.JsonLightOutputContext.MessageWriterSettings.ODataUri;
			return base.WriteContextUriProperty(ODataPayloadKind.ResourceSet, () => ODataContextUrlInfo.Create(typeContext, false, odataUri), null, null);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0003E37D File Offset: 0x0003C57D
		private void WriteAssociationLink(string propertyName, Uri associationLinkUrl)
		{
			base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.associationLink");
			base.JsonWriter.WriteValue(base.UriToString(associationLinkUrl));
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0003E3A4 File Offset: 0x0003C5A4
		private string GetOperationMetadataString(ODataOperation operation)
		{
			string text = UriUtils.UriToString(operation.Metadata);
			if (this.MetadataDocumentBaseUri == null)
			{
				return operation.Metadata.Fragment;
			}
			return "#" + ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(this.MetadataDocumentBaseUri, text);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0003E3ED File Offset: 0x0003C5ED
		private string GetOperationTargetUriString(ODataOperation operation)
		{
			if (!(operation.Target == null))
			{
				return base.UriToString(operation.Target);
			}
			return null;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0003E40C File Offset: 0x0003C60C
		private void ValidateOperationMetadataGroup(IGrouping<string, ODataOperation> operations)
		{
			if (Enumerable.Count<ODataOperation>(operations) > 1)
			{
				if (Enumerable.Any<ODataOperation>(operations, (ODataOperation o) => o.Target == null))
				{
					throw new ODataException(Strings.ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustSpecifyTarget(operations.Key));
				}
			}
			foreach (IGrouping<string, ODataOperation> grouping in Enumerable.GroupBy<ODataOperation, string>(operations, new Func<ODataOperation, string>(this.GetOperationTargetUriString)))
			{
				if (Enumerable.Count<ODataOperation>(grouping) > 1)
				{
					throw new ODataException(Strings.ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget(operations.Key, grouping.Key));
				}
			}
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0003E4C0 File Offset: 0x0003C6C0
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

		// Token: 0x060014F5 RID: 5365 RVA: 0x0003E558 File Offset: 0x0003C758
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
