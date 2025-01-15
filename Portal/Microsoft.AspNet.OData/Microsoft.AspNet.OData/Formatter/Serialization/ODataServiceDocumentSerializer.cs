using System;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001AD RID: 429
	public class ODataServiceDocumentSerializer : ODataSerializer
	{
		// Token: 0x06000E44 RID: 3652 RVA: 0x0003A72B File Offset: 0x0003892B
		public ODataServiceDocumentSerializer()
			: base(ODataPayloadKind.ServiceDocument)
		{
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0003A734 File Offset: 0x00038934
		public override void WriteObject(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)
		{
			if (messageWriter == null)
			{
				throw Error.ArgumentNull("messageWriter");
			}
			if (graph == null)
			{
				throw Error.ArgumentNull("graph");
			}
			ODataServiceDocument odataServiceDocument = graph as ODataServiceDocument;
			if (odataServiceDocument == null)
			{
				throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
				{
					base.GetType().Name,
					type.Name
				}));
			}
			messageWriter.WriteServiceDocument(odataServiceDocument);
		}
	}
}
