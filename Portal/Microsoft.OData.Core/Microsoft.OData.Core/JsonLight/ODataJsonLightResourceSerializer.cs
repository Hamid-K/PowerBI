using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000244 RID: 580
	internal sealed class ODataJsonLightResourceSerializer : ODataJsonLightPropertySerializer
	{
		// Token: 0x0600194F RID: 6479 RVA: 0x0004A8F6 File Offset: 0x00048AF6
		internal ODataJsonLightResourceSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext, true)
		{
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x0004A900 File Offset: 0x00048B00
		private Uri MetadataDocumentBaseUri
		{
			get
			{
				return base.JsonLightOutputContext.MessageWriterSettings.MetadataDocumentUri;
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0004A914 File Offset: 0x00048B14
		internal void WriteResourceSetStartMetadataProperties(ODataResourceSet resourceSet, string propertyName, string expectedResourceTypeName, bool isUndeclared)
		{
			string resourceSetTypeNameForWriting = base.JsonLightOutputContext.TypeNameOracle.GetResourceSetTypeNameForWriting(expectedResourceTypeName, resourceSet, isUndeclared);
			if (resourceSetTypeNameForWriting != null && !resourceSetTypeNameForWriting.Contains("Edm.Untyped"))
			{
				if (propertyName == null)
				{
					base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(resourceSetTypeNameForWriting, false);
					return;
				}
				base.ODataAnnotationWriter.WriteODataTypePropertyAnnotation(propertyName, resourceSetTypeNameForWriting);
			}
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0004A964 File Offset: 0x00048B64
		internal void WriteResourceStartMetadataProperties(IODataJsonLightWriterResourceState resourceState)
		{
			ODataResourceBase resource = resourceState.Resource;
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
			if (resourceTypeNameForWriting != null && !string.Equals(resourceTypeNameForWriting, "Edm.Untyped", StringComparison.Ordinal))
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

		// Token: 0x06001953 RID: 6483 RVA: 0x0004AA80 File Offset: 0x00048C80
		internal void WriteResourceMetadataProperties(IODataJsonLightWriterResourceState resourceState)
		{
			ODataResourceBase resource = resourceState.Resource;
			Uri editLink = resource.EditLink;
			if (editLink != null && !resourceState.EditLinkWritten)
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.editLink");
				base.JsonWriter.WriteValue(base.UriToString((resource.HasNonComputedEditLink || !editLink.IsAbsoluteUri) ? editLink : this.MetadataDocumentBaseUri.MakeRelativeUri(editLink)));
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

		// Token: 0x06001954 RID: 6484 RVA: 0x0004AC94 File Offset: 0x00048E94
		internal void WriteResourceEndMetadataProperties(IODataJsonLightWriterResourceState resourceState, IDuplicatePropertyNameChecker duplicatePropertyNameChecker)
		{
			ODataResourceBase resource = resourceState.Resource;
			for (ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo = resource.MetadataBuilder.GetNextUnprocessedNavigationLink(); odataJsonLightReaderNestedResourceInfo != null; odataJsonLightReaderNestedResourceInfo = resource.MetadataBuilder.GetNextUnprocessedNavigationLink())
			{
				odataJsonLightReaderNestedResourceInfo.NestedResourceInfo.MetadataBuilder = resource.MetadataBuilder;
				this.WriteNavigationLinkMetadata(odataJsonLightReaderNestedResourceInfo.NestedResourceInfo, duplicatePropertyNameChecker);
			}
			for (ODataProperty odataProperty = resource.MetadataBuilder.GetNextUnprocessedStreamProperty(); odataProperty != null; odataProperty = resource.MetadataBuilder.GetNextUnprocessedStreamProperty())
			{
				base.WriteProperty(odataProperty, resourceState.ResourceType, false, duplicatePropertyNameChecker, null);
			}
			IEnumerable<ODataAction> actions = resource.Actions;
			if (actions != null && actions.Any<ODataAction>())
			{
				this.WriteOperations(actions.Cast<ODataOperation>(), true);
			}
			IEnumerable<ODataFunction> functions = resource.Functions;
			if (functions != null && functions.Any<ODataFunction>())
			{
				this.WriteOperations(functions.Cast<ODataOperation>(), false);
			}
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0004AD54 File Offset: 0x00048F54
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

		// Token: 0x06001956 RID: 6486 RVA: 0x0004ADC0 File Offset: 0x00048FC0
		internal void WriteNestedResourceInfoContextUrl(ODataNestedResourceInfo nestedResourceInfo, ODataContextUrlInfo contextUrlInfo)
		{
			base.WriteContextUriProperty(ODataPayloadKind.Resource, () => contextUrlInfo, null, nestedResourceInfo.Name);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0004ADF8 File Offset: 0x00048FF8
		internal void WriteOperations(IEnumerable<ODataOperation> operations, bool isAction)
		{
			IEnumerable<IGrouping<string, ODataOperation>> enumerable = operations.GroupBy(delegate(ODataOperation o)
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

		// Token: 0x06001958 RID: 6488 RVA: 0x0004AE68 File Offset: 0x00049068
		internal ODataContextUrlInfo WriteDeltaContextUri(ODataResourceTypeContext typeContext, ODataDeltaKind kind, ODataContextUrlInfo parentContextUrlInfo = null)
		{
			ODataUri odataUri = base.JsonLightOutputContext.MessageWriterSettings.ODataUri;
			return base.WriteContextUriProperty(ODataPayloadKind.Delta, () => ODataContextUrlInfo.Create(typeContext, this.MessageWriterSettings.Version ?? ODataVersion.V4, kind, odataUri), parentContextUrlInfo, null);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0004AEBC File Offset: 0x000490BC
		internal ODataContextUrlInfo WriteResourceContextUri(ODataResourceTypeContext typeContext, ODataContextUrlInfo parentContextUrlInfo = null)
		{
			ODataUri odataUri = base.JsonLightOutputContext.MessageWriterSettings.ODataUri;
			return base.WriteContextUriProperty(ODataPayloadKind.Resource, () => ODataContextUrlInfo.Create(typeContext, this.MessageWriterSettings.Version ?? ODataVersion.V4, true, odataUri), parentContextUrlInfo, null);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0004AF08 File Offset: 0x00049108
		internal ODataContextUrlInfo WriteResourceSetContextUri(ODataResourceTypeContext typeContext)
		{
			ODataUri odataUri = base.JsonLightOutputContext.MessageWriterSettings.ODataUri;
			return base.WriteContextUriProperty(ODataPayloadKind.ResourceSet, () => ODataContextUrlInfo.Create(typeContext, this.MessageWriterSettings.Version ?? ODataVersion.V4, false, odataUri), null, null);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0004AF54 File Offset: 0x00049154
		private void WriteAssociationLink(string propertyName, Uri associationLinkUrl)
		{
			base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.associationLink");
			base.JsonWriter.WriteValue(base.UriToString(associationLinkUrl));
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0004AF7C File Offset: 0x0004917C
		private string GetOperationMetadataString(ODataOperation operation)
		{
			string text = UriUtils.UriToString(operation.Metadata);
			if (this.MetadataDocumentBaseUri == null)
			{
				return operation.Metadata.Fragment;
			}
			return "#" + ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(this.MetadataDocumentBaseUri, text);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0004AFC5 File Offset: 0x000491C5
		private string GetOperationTargetUriString(ODataOperation operation)
		{
			if (!(operation.Target == null))
			{
				return base.UriToString(operation.Target);
			}
			return null;
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0004AFE4 File Offset: 0x000491E4
		private void ValidateOperationMetadataGroup(IGrouping<string, ODataOperation> operations)
		{
			if (operations.Count<ODataOperation>() > 1)
			{
				if (operations.Any((ODataOperation o) => o.Target == null))
				{
					throw new ODataException(Strings.ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustSpecifyTarget(operations.Key));
				}
			}
			foreach (IGrouping<string, ODataOperation> grouping in operations.GroupBy(new Func<ODataOperation, string>(this.GetOperationTargetUriString)))
			{
				if (grouping.Count<ODataOperation>() > 1)
				{
					throw new ODataException(Strings.ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget(operations.Key, grouping.Key));
				}
			}
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0004B098 File Offset: 0x00049298
		private void WriteOperationMetadataGroup(IGrouping<string, ODataOperation> operations)
		{
			this.ValidateOperationMetadataGroup(operations);
			base.JsonLightOutputContext.JsonWriter.WriteName(operations.Key);
			bool flag = operations.Count<ODataOperation>() > 1;
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

		// Token: 0x06001960 RID: 6496 RVA: 0x0004B130 File Offset: 0x00049330
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
