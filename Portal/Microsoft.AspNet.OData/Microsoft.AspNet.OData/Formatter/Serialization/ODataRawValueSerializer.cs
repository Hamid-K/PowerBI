using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001A1 RID: 417
	public class ODataRawValueSerializer : ODataSerializer
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x0003726D File Offset: 0x0003546D
		public ODataRawValueSerializer()
			: base(ODataPayloadKind.Value)
		{
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00037278 File Offset: 0x00035478
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
			if (TypeHelper.IsEnum(graph.GetType()))
			{
				messageWriter.WriteValue(graph.ToString());
				return;
			}
			messageWriter.WriteValue(ODataPrimitiveSerializer.ConvertUnsupportedPrimitives(graph));
		}
	}
}
