using System;
using System.Collections;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001A9 RID: 425
	public class ODataCollectionSerializer : ODataEdmTypeSerializer
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x00037F1E File Offset: 0x0003611E
		public ODataCollectionSerializer(ODataSerializerProvider serializerProvider)
			: base(ODataPayloadKind.Collection, serializerProvider)
		{
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00037F28 File Offset: 0x00036128
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
			IEdmTypeReference edmType = writeContext.GetEdmType(graph, type);
			IEdmTypeReference elementType = ODataCollectionSerializer.GetElementType(edmType);
			ODataCollectionWriter odataCollectionWriter = messageWriter.CreateODataCollectionWriter(elementType);
			this.WriteCollection(odataCollectionWriter, graph, edmType.AsCollection(), writeContext);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00037F7C File Offset: 0x0003617C
		public sealed override ODataValue CreateODataValue(object graph, IEdmTypeReference expectedType, ODataSerializerContext writeContext)
		{
			IEnumerable enumerable = graph as IEnumerable;
			if (enumerable == null && graph != null)
			{
				throw Error.Argument("graph", SRResources.ArgumentMustBeOfType, new object[] { typeof(IEnumerable).Name });
			}
			if (expectedType == null)
			{
				throw Error.ArgumentNull("expectedType");
			}
			IEdmTypeReference elementType = ODataCollectionSerializer.GetElementType(expectedType);
			return this.CreateODataCollectionValue(enumerable, elementType, writeContext);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00037FDC File Offset: 0x000361DC
		public void WriteCollection(ODataCollectionWriter writer, object graph, IEdmTypeReference collectionType, ODataSerializerContext writeContext)
		{
			if (writer == null)
			{
				throw Error.ArgumentNull("writer");
			}
			ODataCollectionStart odataCollectionStart = new ODataCollectionStart
			{
				Name = writeContext.RootElementName
			};
			if (writeContext.Request != null)
			{
				if (writeContext.InternalRequest.Context.NextLink != null)
				{
					odataCollectionStart.NextPageLink = writeContext.InternalRequest.Context.NextLink;
				}
				else if (writeContext.InternalRequest.Context.QueryOptions != null)
				{
					SkipTokenHandler skipTokenHandler = writeContext.QueryOptions.Context.GetSkipTokenHandler();
					odataCollectionStart.NextPageLink = skipTokenHandler.GenerateNextPageLink(writeContext.InternalRequest.RequestUri, writeContext.InternalRequest.Context.PageSize, null, writeContext);
				}
				if (writeContext.InternalRequest.Context.TotalCount != null)
				{
					odataCollectionStart.Count = writeContext.InternalRequest.Context.TotalCount;
				}
			}
			writer.WriteStart(odataCollectionStart);
			if (graph != null)
			{
				ODataCollectionValue odataCollectionValue = this.CreateODataValue(graph, collectionType, writeContext) as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					foreach (object obj in odataCollectionValue.Items)
					{
						writer.WriteItem(obj);
					}
				}
			}
			writer.WriteEnd();
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00038138 File Offset: 0x00036338
		public virtual ODataCollectionValue CreateODataCollectionValue(IEnumerable enumerable, IEdmTypeReference elementType, ODataSerializerContext writeContext)
		{
			if (writeContext == null)
			{
				throw Error.ArgumentNull("writeContext");
			}
			if (elementType == null)
			{
				throw Error.ArgumentNull("elementType");
			}
			ArrayList arrayList = new ArrayList();
			if (enumerable != null)
			{
				ODataEdmTypeSerializer odataEdmTypeSerializer = null;
				foreach (object obj in enumerable)
				{
					if (obj == null)
					{
						if (!elementType.IsNullable)
						{
							throw new SerializationException(SRResources.NullElementInCollection);
						}
						arrayList.Add(null);
					}
					else
					{
						IEdmTypeReference edmType = writeContext.GetEdmType(obj, obj.GetType());
						odataEdmTypeSerializer = odataEdmTypeSerializer ?? base.SerializerProvider.GetEdmTypeSerializer(edmType);
						if (odataEdmTypeSerializer == null)
						{
							throw new SerializationException(Error.Format(SRResources.TypeCannotBeSerialized, new object[] { edmType.FullName() }));
						}
						arrayList.Add(odataEdmTypeSerializer.CreateODataValue(obj, edmType, writeContext).GetInnerValue());
					}
				}
			}
			string text = "Collection(" + elementType.FullName() + ")";
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			odataCollectionValue.Items = arrayList.Cast<object>();
			odataCollectionValue.TypeName = text;
			ODataCollectionSerializer.AddTypeNameAnnotationAsNeeded(odataCollectionValue, writeContext.MetadataLevel);
			return odataCollectionValue;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00038270 File Offset: 0x00036470
		internal override ODataProperty CreateProperty(object graph, IEdmTypeReference expectedType, string elementName, ODataSerializerContext writeContext)
		{
			ODataValue odataValue = this.CreateODataValue(graph, expectedType, writeContext);
			if (odataValue != null)
			{
				return new ODataProperty
				{
					Name = elementName,
					Value = odataValue
				};
			}
			return null;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x000382A0 File Offset: 0x000364A0
		protected internal static void AddTypeNameAnnotationAsNeeded(ODataCollectionValue value, ODataMetadataLevel metadataLevel)
		{
			if (ODataCollectionSerializer.ShouldAddTypeNameAnnotation(metadataLevel))
			{
				string text;
				if (ODataCollectionSerializer.ShouldSuppressTypeNameSerialization(metadataLevel))
				{
					text = null;
				}
				else
				{
					text = value.TypeName;
				}
				value.TypeAnnotation = new ODataTypeAnnotation(text);
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003702C File Offset: 0x0003522C
		internal static bool ShouldAddTypeNameAnnotation(ODataMetadataLevel metadataLevel)
		{
			if (metadataLevel != ODataMetadataLevel.MinimalMetadata)
			{
				if (metadataLevel - ODataMetadataLevel.FullMetadata > 1)
				{
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003703C File Offset: 0x0003523C
		internal static bool ShouldSuppressTypeNameSerialization(ODataMetadataLevel metadataLevel)
		{
			return metadataLevel != ODataMetadataLevel.FullMetadata && metadataLevel == ODataMetadataLevel.NoMetadata;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x000382D4 File Offset: 0x000364D4
		private static IEdmTypeReference GetElementType(IEdmTypeReference feedType)
		{
			if (feedType.IsCollection())
			{
				return feedType.AsCollection().ElementType();
			}
			throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
			{
				typeof(ODataResourceSetSerializer).Name,
				feedType.FullName()
			}));
		}
	}
}
