using System;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001A6 RID: 422
	public class ODataEntityReferenceLinkSerializer : ODataSerializer
	{
		// Token: 0x06000DF0 RID: 3568 RVA: 0x00037DA5 File Offset: 0x00035FA5
		public ODataEntityReferenceLinkSerializer()
			: base(ODataPayloadKind.EntityReferenceLink)
		{
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00037DB0 File Offset: 0x00035FB0
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
			if (graph != null)
			{
				ODataEntityReferenceLink odataEntityReferenceLink = graph as ODataEntityReferenceLink;
				if (odataEntityReferenceLink == null)
				{
					Uri uri = graph as Uri;
					if (uri == null)
					{
						throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
						{
							base.GetType().Name,
							graph.GetType().FullName
						}));
					}
					odataEntityReferenceLink = new ODataEntityReferenceLink
					{
						Url = uri
					};
				}
				messageWriter.WriteEntityReferenceLink(odataEntityReferenceLink);
			}
		}
	}
}
