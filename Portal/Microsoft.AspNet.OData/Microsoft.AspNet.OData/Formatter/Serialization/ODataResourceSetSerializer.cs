using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001AB RID: 427
	public class ODataResourceSetSerializer : ODataEdmTypeSerializer
	{
		// Token: 0x06000E2F RID: 3631 RVA: 0x00039B12 File Offset: 0x00037D12
		public ODataResourceSetSerializer(ODataSerializerProvider serializerProvider)
			: base(ODataPayloadKind.ResourceSet, serializerProvider)
		{
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00039B1C File Offset: 0x00037D1C
		public override void WriteObject(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)
		{
			if (messageWriter == null)
			{
				throw Error.ArgumentNull("messageWriter");
			}
			if (writeContext == null)
			{
				throw Error.ArgumentNull("writeContext");
			}
			IEdmEntitySetBase edmEntitySetBase = writeContext.NavigationSource as IEdmEntitySetBase;
			IEdmTypeReference edmType = writeContext.GetEdmType(graph, type);
			IEdmStructuredTypeReference resourceType = ODataResourceSetSerializer.GetResourceType(edmType);
			ODataWriter odataWriter = messageWriter.CreateODataResourceSetWriter(edmEntitySetBase, resourceType.StructuredDefinition());
			this.WriteObjectInline(graph, edmType, odataWriter, writeContext);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00039B80 File Offset: 0x00037D80
		public override void WriteObjectInline(object graph, IEdmTypeReference expectedType, ODataWriter writer, ODataSerializerContext writeContext)
		{
			if (writer == null)
			{
				throw Error.ArgumentNull("writer");
			}
			if (writeContext == null)
			{
				throw Error.ArgumentNull("writeContext");
			}
			if (expectedType == null)
			{
				throw Error.ArgumentNull("expectedType");
			}
			if (graph == null)
			{
				throw new SerializationException(Error.Format(SRResources.CannotSerializerNull, new object[] { "ResourceSet" }));
			}
			IEnumerable enumerable = graph as IEnumerable;
			if (enumerable == null)
			{
				throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
				{
					base.GetType().Name,
					graph.GetType().FullName
				}));
			}
			this.WriteResourceSet(enumerable, expectedType, writer, writeContext);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00039C20 File Offset: 0x00037E20
		private void WriteResourceSet(IEnumerable enumerable, IEdmTypeReference resourceSetType, ODataWriter writer, ODataSerializerContext writeContext)
		{
			IEdmStructuredTypeReference resourceType = ODataResourceSetSerializer.GetResourceType(resourceSetType);
			ODataResourceSet odataResourceSet = this.CreateResourceSet(enumerable, resourceSetType.AsCollection(), writeContext);
			Func<object, Uri> nextLinkGenerator = ODataResourceSetSerializer.GetNextLinkGenerator(odataResourceSet, enumerable, writeContext);
			if (odataResourceSet == null)
			{
				throw new SerializationException(Error.Format(SRResources.CannotSerializerNull, new object[] { "ResourceSet" }));
			}
			if (!(writeContext.NavigationSource is IEdmEntitySetBase))
			{
				odataResourceSet.SetSerializationInfo(new ODataResourceSerializationInfo
				{
					IsFromCollection = true,
					NavigationSourceEntityTypeName = resourceType.FullName(),
					NavigationSourceKind = EdmNavigationSourceKind.UnknownEntitySet,
					NavigationSourceName = null
				});
			}
			ODataEdmTypeSerializer edmTypeSerializer = base.SerializerProvider.GetEdmTypeSerializer(resourceType);
			if (edmTypeSerializer == null)
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeSerialized, new object[] { resourceType.FullName() }));
			}
			odataResourceSet.NextPageLink = null;
			writer.WriteStart(odataResourceSet);
			object obj = null;
			foreach (object obj2 in enumerable)
			{
				obj = obj2;
				if (obj2 == null || obj2 is NullEdmComplexObject)
				{
					if (resourceType.IsEntity())
					{
						throw new SerializationException(SRResources.NullElementInCollection);
					}
					writer.WriteStart(null);
					writer.WriteEnd();
				}
				else
				{
					edmTypeSerializer.WriteObjectInline(obj2, resourceType, writer, writeContext);
				}
			}
			odataResourceSet.NextPageLink = nextLinkGenerator(obj);
			writer.WriteEnd();
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00039D80 File Offset: 0x00037F80
		public virtual ODataResourceSet CreateResourceSet(IEnumerable resourceSetInstance, IEdmCollectionTypeReference resourceSetType, ODataSerializerContext writeContext)
		{
			ODataResourceSet odataResourceSet = new ODataResourceSet
			{
				TypeName = resourceSetType.FullName()
			};
			IEdmStructuredTypeReference edmStructuredTypeReference = ODataResourceSetSerializer.GetResourceType(resourceSetType).AsStructured();
			if (writeContext.NavigationSource != null && edmStructuredTypeReference.IsEntity())
			{
				ResourceSetContext resourceSetContext = ResourceSetContext.Create(writeContext, resourceSetInstance);
				IEdmEntityType edmEntityType = edmStructuredTypeReference.AsEntity().EntityDefinition();
				IEnumerable<IEdmOperation> availableOperationsBoundToCollection = writeContext.Model.GetAvailableOperationsBoundToCollection(edmEntityType);
				foreach (ODataOperation odataOperation in this.CreateODataOperations(availableOperationsBoundToCollection, resourceSetContext, writeContext))
				{
					ODataAction odataAction = odataOperation as ODataAction;
					if (odataAction != null)
					{
						odataResourceSet.AddAction(odataAction);
					}
					else
					{
						odataResourceSet.AddFunction((ODataFunction)odataOperation);
					}
				}
			}
			if (writeContext.ExpandedResource == null)
			{
				PageResult pageResult = resourceSetInstance as PageResult;
				if (pageResult != null)
				{
					odataResourceSet.Count = pageResult.Count;
					odataResourceSet.NextPageLink = pageResult.NextPageLink;
				}
				else if (writeContext.Request != null)
				{
					odataResourceSet.NextPageLink = writeContext.InternalRequest.Context.NextLink;
					odataResourceSet.DeltaLink = writeContext.InternalRequest.Context.DeltaLink;
					long? totalCount = writeContext.InternalRequest.Context.TotalCount;
					if (totalCount != null)
					{
						odataResourceSet.Count = new long?(totalCount.Value);
					}
				}
			}
			else
			{
				ICountOptionCollection countOptionCollection = resourceSetInstance as ICountOptionCollection;
				if (countOptionCollection != null && countOptionCollection.TotalCount != null)
				{
					odataResourceSet.Count = countOptionCollection.TotalCount;
				}
			}
			return odataResourceSet;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00039F10 File Offset: 0x00038110
		internal static Func<object, Uri> GetNextLinkGenerator(ODataResourceSetBase resourceSet, IEnumerable resourceSetInstance, ODataSerializerContext writeContext)
		{
			if (resourceSet != null && resourceSet.NextPageLink != null)
			{
				Uri defaultUri = resourceSet.NextPageLink;
				return (object obj) => defaultUri;
			}
			if (writeContext.ExpandedResource == null)
			{
				if (writeContext.InternalRequest != null && writeContext.QueryContext != null)
				{
					SkipTokenHandler handler = writeContext.QueryContext.GetSkipTokenHandler();
					return (object obj) => handler.GenerateNextPageLink(writeContext.InternalRequest.RequestUri, writeContext.InternalRequest.Context.PageSize, obj, writeContext);
				}
			}
			else
			{
				ITruncatedCollection truncatedCollection = resourceSetInstance as ITruncatedCollection;
				if (truncatedCollection != null && truncatedCollection.IsTruncated)
				{
					return (object obj) => ODataResourceSetSerializer.GetNestedNextPageLink(writeContext, truncatedCollection.PageSize, obj);
				}
			}
			return (object obj) => null;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003A00C File Offset: 0x0003820C
		public virtual ODataOperation CreateODataOperation(IEdmOperation operation, ResourceSetContext resourceSetContext, ODataSerializerContext writeContext)
		{
			if (operation == null)
			{
				throw Error.ArgumentNull("operation");
			}
			if (resourceSetContext == null)
			{
				throw Error.ArgumentNull("resourceSetContext");
			}
			if (writeContext == null)
			{
				throw Error.ArgumentNull("writeContext");
			}
			ODataMetadataLevel metadataLevel = writeContext.MetadataLevel;
			IEdmModel model = writeContext.Model;
			if (metadataLevel != ODataMetadataLevel.FullMetadata)
			{
				return null;
			}
			OperationLinkBuilder operationLinkBuilder = model.GetOperationLinkBuilder(operation);
			if (operationLinkBuilder == null)
			{
				return null;
			}
			Uri uri = operationLinkBuilder.BuildLink(resourceSetContext);
			if (uri == null)
			{
				return null;
			}
			Uri uri2 = new Uri(new Uri(writeContext.InternalUrlHelper.CreateODataLink(new ODataPathSegment[] { MetadataSegment.Instance })), "#" + operation.FullName());
			ODataOperation odataOperation;
			if (operation is IEdmAction)
			{
				odataOperation = new ODataAction();
			}
			else
			{
				odataOperation = new ODataFunction();
			}
			odataOperation.Metadata = uri2;
			ODataResourceSerializer.EmitTitle(model, operation, odataOperation);
			if (metadataLevel == ODataMetadataLevel.FullMetadata || !operationLinkBuilder.FollowsConventions)
			{
				odataOperation.Target = uri;
			}
			return odataOperation;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003A0EC File Offset: 0x000382EC
		private IEnumerable<ODataOperation> CreateODataOperations(IEnumerable<IEdmOperation> operations, ResourceSetContext resourceSetContext, ODataSerializerContext writeContext)
		{
			foreach (IEdmOperation edmOperation in operations)
			{
				ODataOperation odataOperation = this.CreateODataOperation(edmOperation, resourceSetContext, writeContext);
				if (odataOperation != null)
				{
					yield return odataOperation;
				}
			}
			IEnumerator<IEdmOperation> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003A114 File Offset: 0x00038314
		private static Uri GetNestedNextPageLink(ODataSerializerContext writeContext, int pageSize, object obj)
		{
			IEdmNavigationSource navigationSource = writeContext.ExpandedResource.NavigationSource;
			Uri uri = writeContext.Model.GetNavigationSourceLinkBuilder(navigationSource).BuildNavigationLink(writeContext.ExpandedResource, writeContext.NavigationProperty);
			Uri uri2 = ODataResourceSetSerializer.GenerateQueryFromExpandedItem(writeContext, uri);
			SkipTokenHandler skipTokenHandler = null;
			if (writeContext.QueryContext != null)
			{
				skipTokenHandler = writeContext.QueryContext.GetSkipTokenHandler();
			}
			if (!(uri2 != null))
			{
				return null;
			}
			if (skipTokenHandler != null)
			{
				return skipTokenHandler.GenerateNextPageLink(uri2, pageSize, obj, writeContext);
			}
			return GetNextPageHelper.GetNextPageLink(uri2, pageSize, null, null);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003A18C File Offset: 0x0003838C
		private static Uri GenerateQueryFromExpandedItem(ODataSerializerContext writeContext, Uri navigationLink)
		{
			IWebApiUrlHelper internalUrlHelper = writeContext.InternalUrlHelper;
			if (internalUrlHelper == null)
			{
				return navigationLink;
			}
			Uri uri = new Uri(internalUrlHelper.CreateODataLink(writeContext.InternalRequest.Context.RouteName, writeContext.InternalRequest.PathHandler, new List<ODataPathSegment>()));
			ODataUri odataUri = new ODataUriParser(writeContext.Model, uri, navigationLink).ParseUri();
			odataUri.SelectAndExpand = writeContext.SelectExpandClause;
			if (writeContext.CurrentExpandedSelectItem != null)
			{
				odataUri.OrderBy = writeContext.CurrentExpandedSelectItem.OrderByOption;
				odataUri.Filter = writeContext.CurrentExpandedSelectItem.FilterOption;
				odataUri.Skip = writeContext.CurrentExpandedSelectItem.SkipOption;
				odataUri.Top = writeContext.CurrentExpandedSelectItem.TopOption;
				if (writeContext.CurrentExpandedSelectItem.CountOption != null && writeContext.CurrentExpandedSelectItem.CountOption != null)
				{
					odataUri.QueryCount = new bool?(writeContext.CurrentExpandedSelectItem.CountOption.Value);
				}
				ExpandedNavigationSelectItem expandedNavigationSelectItem = writeContext.CurrentExpandedSelectItem as ExpandedNavigationSelectItem;
				if (expandedNavigationSelectItem != null)
				{
					odataUri.SelectAndExpand = expandedNavigationSelectItem.SelectAndExpand;
				}
			}
			ODataUrlKeyDelimiter odataUrlKeyDelimiter = ((writeContext.InternalRequest.Options.UrlKeyDelimiter == ODataUrlKeyDelimiter.Slash) ? ODataUrlKeyDelimiter.Slash : ODataUrlKeyDelimiter.Parentheses);
			return odataUri.BuildUri(odataUrlKeyDelimiter);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003A2D4 File Offset: 0x000384D4
		private static IEdmStructuredTypeReference GetResourceType(IEdmTypeReference resourceSetType)
		{
			if (resourceSetType.IsCollection())
			{
				IEdmTypeReference edmTypeReference = resourceSetType.AsCollection().ElementType();
				if (edmTypeReference.IsEntity() || edmTypeReference.IsComplex())
				{
					return edmTypeReference.AsStructured();
				}
			}
			throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
			{
				typeof(ODataResourceSetSerializer).Name,
				resourceSetType.FullName()
			}));
		}

		// Token: 0x040003FF RID: 1023
		private const string ResourceSet = "ResourceSet";
	}
}
