using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001A7 RID: 423
	public abstract class ODataEdmTypeSerializer : ODataSerializer
	{
		// Token: 0x06000DF2 RID: 3570 RVA: 0x00037E3D File Offset: 0x0003603D
		protected ODataEdmTypeSerializer(ODataPayloadKind payloadKind)
			: base(payloadKind)
		{
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00037E46 File Offset: 0x00036046
		protected ODataEdmTypeSerializer(ODataPayloadKind payloadKind, ODataSerializerProvider serializerProvider)
			: this(payloadKind)
		{
			if (serializerProvider == null)
			{
				throw Error.ArgumentNull("serializerProvider");
			}
			this.SerializerProvider = serializerProvider;
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00037E64 File Offset: 0x00036064
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x00037E6C File Offset: 0x0003606C
		public ODataSerializerProvider SerializerProvider { get; private set; }

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00037E75 File Offset: 0x00036075
		public virtual void WriteObjectInline(object graph, IEdmTypeReference expectedType, ODataWriter writer, ODataSerializerContext writeContext)
		{
			throw Error.NotSupported(SRResources.WriteObjectInlineNotSupported, new object[] { base.GetType().Name });
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00037E95 File Offset: 0x00036095
		public virtual ODataValue CreateODataValue(object graph, IEdmTypeReference expectedType, ODataSerializerContext writeContext)
		{
			throw Error.NotSupported(SRResources.CreateODataValueNotSupported, new object[] { base.GetType().Name });
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00037EB5 File Offset: 0x000360B5
		internal virtual ODataProperty CreateProperty(object graph, IEdmTypeReference expectedType, string elementName, ODataSerializerContext writeContext)
		{
			return new ODataProperty
			{
				Name = elementName,
				Value = this.CreateODataValue(graph, expectedType, writeContext)
			};
		}
	}
}
