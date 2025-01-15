using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001A4 RID: 420
	public class ODataMetadataSerializer : ODataSerializer
	{
		// Token: 0x06000DEC RID: 3564 RVA: 0x00037D0B File Offset: 0x00035F0B
		public ODataMetadataSerializer()
			: base(ODataPayloadKind.MetadataDocument)
		{
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00037D15 File Offset: 0x00035F15
		public override void WriteObject(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)
		{
			if (messageWriter == null)
			{
				throw Error.ArgumentNull("messageWriter");
			}
			messageWriter.WriteMetadataDocument();
		}
	}
}
