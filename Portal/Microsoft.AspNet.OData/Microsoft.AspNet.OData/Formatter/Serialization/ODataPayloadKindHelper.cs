using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001A5 RID: 421
	internal static class ODataPayloadKindHelper
	{
		// Token: 0x06000DEE RID: 3566 RVA: 0x00037D2C File Offset: 0x00035F2C
		public static bool IsDefined(ODataPayloadKind payloadKind)
		{
			return payloadKind == ODataPayloadKind.Batch || payloadKind == ODataPayloadKind.BinaryValue || payloadKind == ODataPayloadKind.Collection || payloadKind == ODataPayloadKind.EntityReferenceLink || payloadKind == ODataPayloadKind.EntityReferenceLinks || payloadKind == ODataPayloadKind.Resource || payloadKind == ODataPayloadKind.Error || payloadKind == ODataPayloadKind.ResourceSet || payloadKind == ODataPayloadKind.MetadataDocument || payloadKind == ODataPayloadKind.Parameter || payloadKind == ODataPayloadKind.Property || payloadKind == ODataPayloadKind.ServiceDocument || payloadKind == ODataPayloadKind.Value || payloadKind == ODataPayloadKind.IndividualProperty || payloadKind == ODataPayloadKind.Delta || payloadKind == ODataPayloadKind.Asynchronous || payloadKind == ODataPayloadKind.Unsupported;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00037D89 File Offset: 0x00035F89
		public static void Validate(ODataPayloadKind payloadKind, string parameterName)
		{
			if (!ODataPayloadKindHelper.IsDefined(payloadKind))
			{
				throw Error.InvalidEnumArgument(parameterName, (int)payloadKind, typeof(ODataPayloadKind));
			}
		}
	}
}
