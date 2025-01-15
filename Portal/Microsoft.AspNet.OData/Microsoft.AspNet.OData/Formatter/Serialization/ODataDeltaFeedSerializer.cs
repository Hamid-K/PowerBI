using System;
using System.Collections;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x0200019B RID: 411
	public class ODataDeltaFeedSerializer : ODataEdmTypeSerializer
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x00036830 File Offset: 0x00034A30
		public ODataDeltaFeedSerializer(ODataSerializerProvider serializerProvider)
			: base(ODataPayloadKind.Delta, serializerProvider)
		{
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0003683C File Offset: 0x00034A3C
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
			if (graph == null)
			{
				throw new SerializationException(Error.Format(SRResources.CannotSerializerNull, new object[] { "deltafeed" }));
			}
			IEdmEntitySetBase edmEntitySetBase = writeContext.NavigationSource as IEdmEntitySetBase;
			if (edmEntitySetBase == null)
			{
				throw new SerializationException(SRResources.EntitySetMissingDuringSerialization);
			}
			IEdmTypeReference edmType = writeContext.GetEdmType(graph, type);
			IEdmEntityTypeReference edmEntityTypeReference = ODataDeltaFeedSerializer.GetResourceType(edmType).AsEntity();
			ODataWriter odataWriter = messageWriter.CreateODataDeltaResourceSetWriter(edmEntitySetBase, edmEntityTypeReference.EntityDefinition());
			this.WriteDeltaFeedInline(graph, edmType, odataWriter, writeContext);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000368D4 File Offset: 0x00034AD4
		public virtual void WriteDeltaFeedInline(object graph, IEdmTypeReference expectedType, ODataWriter writer, ODataSerializerContext writeContext)
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
				throw new SerializationException(Error.Format(SRResources.CannotSerializerNull, new object[] { "deltafeed" }));
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
			this.WriteFeed(enumerable, expectedType, writer, writeContext);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00036974 File Offset: 0x00034B74
		private void WriteFeed(IEnumerable enumerable, IEdmTypeReference feedType, ODataWriter writer, ODataSerializerContext writeContext)
		{
			IEdmStructuredTypeReference resourceType = ODataDeltaFeedSerializer.GetResourceType(feedType);
			if (resourceType.IsComplex())
			{
				ODataResourceSet odataResourceSet = new ODataResourceSet
				{
					TypeName = feedType.FullName()
				};
				writer.WriteStart(odataResourceSet);
				ODataResourceSerializer odataResourceSerializer = base.SerializerProvider.GetEdmTypeSerializer(resourceType) as ODataResourceSerializer;
				if (odataResourceSerializer == null)
				{
					throw new SerializationException(Error.Format(SRResources.TypeCannotBeSerialized, new object[] { resourceType.FullName() }));
				}
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						odataResourceSerializer.WriteDeltaObjectInline(obj, resourceType, writer, writeContext);
					}
					goto IL_0212;
				}
			}
			ODataDeltaResourceSet odataDeltaResourceSet = this.CreateODataDeltaFeed(enumerable, feedType.AsCollection(), writeContext);
			if (odataDeltaResourceSet == null)
			{
				throw new SerializationException(Error.Format(SRResources.CannotSerializerNull, new object[] { "deltafeed" }));
			}
			Func<object, Uri> nextLinkGenerator = ODataDeltaFeedSerializer.GetNextLinkGenerator(odataDeltaResourceSet, enumerable, writeContext);
			odataDeltaResourceSet.NextPageLink = null;
			writer.WriteStart(odataDeltaResourceSet);
			object obj2 = null;
			foreach (object obj3 in enumerable)
			{
				if (obj3 == null)
				{
					throw new SerializationException(SRResources.NullElementInCollection);
				}
				obj2 = obj3;
				IEdmChangedObject edmChangedObject = obj3 as IEdmChangedObject;
				if (edmChangedObject == null)
				{
					throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
					{
						base.GetType().Name,
						enumerable.GetType().FullName
					}));
				}
				switch (edmChangedObject.DeltaKind)
				{
				case EdmDeltaEntityKind.Entry:
				{
					ODataResourceSerializer odataResourceSerializer2 = base.SerializerProvider.GetEdmTypeSerializer(resourceType) as ODataResourceSerializer;
					if (odataResourceSerializer2 == null)
					{
						throw new SerializationException(Error.Format(SRResources.TypeCannotBeSerialized, new object[] { resourceType.FullName() }));
					}
					odataResourceSerializer2.WriteDeltaObjectInline(obj3, resourceType, writer, writeContext);
					break;
				}
				case EdmDeltaEntityKind.DeletedEntry:
					this.WriteDeltaDeletedEntry(obj3, writer, writeContext);
					break;
				case EdmDeltaEntityKind.DeletedLinkEntry:
					this.WriteDeltaDeletedLink(obj3, writer, writeContext);
					break;
				case EdmDeltaEntityKind.LinkEntry:
					this.WriteDeltaLink(obj3, writer, writeContext);
					break;
				}
			}
			odataDeltaResourceSet.NextPageLink = nextLinkGenerator(obj2);
			IL_0212:
			writer.WriteEnd();
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00036BB8 File Offset: 0x00034DB8
		internal static Func<object, Uri> GetNextLinkGenerator(ODataDeltaResourceSet deltaFeed, IEnumerable enumerable, ODataSerializerContext writeContext)
		{
			return ODataResourceSetSerializer.GetNextLinkGenerator(deltaFeed, enumerable, writeContext);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00036BC4 File Offset: 0x00034DC4
		public virtual ODataDeltaResourceSet CreateODataDeltaFeed(IEnumerable feedInstance, IEdmCollectionTypeReference feedType, ODataSerializerContext writeContext)
		{
			ODataDeltaResourceSet odataDeltaResourceSet = new ODataDeltaResourceSet();
			if (writeContext.ExpandedResource == null)
			{
				PageResult pageResult = feedInstance as PageResult;
				if (pageResult != null)
				{
					odataDeltaResourceSet.Count = pageResult.Count;
					odataDeltaResourceSet.NextPageLink = pageResult.NextPageLink;
				}
				else if (writeContext.Request != null)
				{
					odataDeltaResourceSet.NextPageLink = writeContext.InternalRequest.Context.NextLink;
					odataDeltaResourceSet.DeltaLink = writeContext.InternalRequest.Context.DeltaLink;
					long? totalCount = writeContext.InternalRequest.Context.TotalCount;
					if (totalCount != null)
					{
						odataDeltaResourceSet.Count = new long?(totalCount.Value);
					}
				}
			}
			return odataDeltaResourceSet;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00036C68 File Offset: 0x00034E68
		public virtual void WriteDeltaDeletedEntry(object graph, ODataWriter writer, ODataSerializerContext writeContext)
		{
			EdmDeltaDeletedEntityObject edmDeltaDeletedEntityObject = graph as EdmDeltaDeletedEntityObject;
			if (edmDeltaDeletedEntityObject == null)
			{
				throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
				{
					base.GetType().Name,
					graph.GetType().FullName
				}));
			}
			ODataDeletedResource odataDeletedResource = new ODataDeletedResource(ODataDeltaFeedSerializer.StringToUri(edmDeltaDeletedEntityObject.Id), edmDeltaDeletedEntityObject.Reason);
			if (edmDeltaDeletedEntityObject.NavigationSource != null)
			{
				ODataResourceSerializationInfo odataResourceSerializationInfo = new ODataResourceSerializationInfo
				{
					NavigationSourceName = edmDeltaDeletedEntityObject.NavigationSource.Name
				};
				odataDeletedResource.SetSerializationInfo(odataResourceSerializationInfo);
			}
			if (odataDeletedResource != null)
			{
				writer.WriteStart(odataDeletedResource);
				writer.WriteEnd();
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00036D00 File Offset: 0x00034F00
		public virtual void WriteDeltaDeletedLink(object graph, ODataWriter writer, ODataSerializerContext writeContext)
		{
			EdmDeltaDeletedLink edmDeltaDeletedLink = graph as EdmDeltaDeletedLink;
			if (edmDeltaDeletedLink == null)
			{
				throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
				{
					base.GetType().Name,
					graph.GetType().FullName
				}));
			}
			ODataDeltaDeletedLink odataDeltaDeletedLink = new ODataDeltaDeletedLink(edmDeltaDeletedLink.Source, edmDeltaDeletedLink.Target, edmDeltaDeletedLink.Relationship);
			if (odataDeltaDeletedLink != null)
			{
				writer.WriteDeltaDeletedLink(odataDeltaDeletedLink);
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00036D6C File Offset: 0x00034F6C
		public virtual void WriteDeltaLink(object graph, ODataWriter writer, ODataSerializerContext writeContext)
		{
			EdmDeltaLink edmDeltaLink = graph as EdmDeltaLink;
			if (edmDeltaLink == null)
			{
				throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
				{
					base.GetType().Name,
					graph.GetType().FullName
				}));
			}
			ODataDeltaLink odataDeltaLink = new ODataDeltaLink(edmDeltaLink.Source, edmDeltaLink.Target, edmDeltaLink.Relationship);
			if (odataDeltaLink != null)
			{
				writer.WriteDeltaLink(odataDeltaLink);
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00036DD8 File Offset: 0x00034FD8
		private static IEdmStructuredTypeReference GetResourceType(IEdmTypeReference feedType)
		{
			if (feedType.IsCollection())
			{
				IEdmTypeReference edmTypeReference = feedType.AsCollection().ElementType();
				if (edmTypeReference.IsEntity() || edmTypeReference.IsComplex())
				{
					return edmTypeReference.AsStructured();
				}
			}
			throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
			{
				typeof(ODataResourceSetSerializer).Name,
				feedType.FullName()
			}));
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00036E40 File Offset: 0x00035040
		internal static Uri StringToUri(string uriString)
		{
			Uri uri = null;
			try
			{
				uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
			}
			catch (FormatException)
			{
				uri = new Uri(uriString, UriKind.Relative);
			}
			return uri;
		}

		// Token: 0x040003EA RID: 1002
		private const string DeltaFeed = "deltafeed";
	}
}
