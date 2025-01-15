using System;
using System.Runtime.Serialization;
using System.Web.Http;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x0200019A RID: 410
	public class ODataErrorSerializer : ODataSerializer
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x00036793 File Offset: 0x00034993
		internal static bool IsHttpError(object error)
		{
			return error is HttpError;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003679E File Offset: 0x0003499E
		internal static ODataError CreateODataError(object error)
		{
			return (error as HttpError).CreateODataError();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x000367AB File Offset: 0x000349AB
		public ODataErrorSerializer()
			: base(ODataPayloadKind.Error)
		{
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000367B8 File Offset: 0x000349B8
		public override void WriteObject(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)
		{
			if (graph == null)
			{
				throw Error.ArgumentNull("graph");
			}
			if (messageWriter == null)
			{
				throw Error.ArgumentNull("messageWriter");
			}
			ODataError odataError = graph as ODataError;
			if (odataError == null)
			{
				if (!ODataErrorSerializer.IsHttpError(graph))
				{
					throw new SerializationException(Error.Format(SRResources.ErrorTypeMustBeODataErrorOrHttpError, new object[] { graph.GetType().FullName }));
				}
				odataError = ODataErrorSerializer.CreateODataError(graph);
			}
			bool flag = odataError.InnerError != null;
			messageWriter.WriteError(odataError, flag);
		}
	}
}
