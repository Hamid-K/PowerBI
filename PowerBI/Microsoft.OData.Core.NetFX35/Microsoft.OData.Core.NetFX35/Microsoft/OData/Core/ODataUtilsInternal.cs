using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020001A5 RID: 421
	internal static class ODataUtilsInternal
	{
		// Token: 0x06000FC8 RID: 4040 RVA: 0x00036320 File Offset: 0x00034520
		internal static void SetODataVersion(ODataMessage message, ODataMessageWriterSettings settings)
		{
			string text = ODataUtils.ODataVersionToString(settings.Version.Value);
			message.SetHeader("OData-Version", text);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00036350 File Offset: 0x00034550
		internal static ODataVersion GetODataVersion(ODataMessage message, ODataVersion defaultVersion)
		{
			string header = message.GetHeader("OData-Version");
			string text = header;
			if (!string.IsNullOrEmpty(text))
			{
				return ODataUtils.StringToODataVersion(text);
			}
			return defaultVersion;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0003637C File Offset: 0x0003457C
		internal static bool IsPayloadKindSupported(ODataPayloadKind payloadKind, bool inRequest)
		{
			switch (payloadKind)
			{
			case ODataPayloadKind.Feed:
			case ODataPayloadKind.EntityReferenceLinks:
			case ODataPayloadKind.Collection:
			case ODataPayloadKind.ServiceDocument:
			case ODataPayloadKind.MetadataDocument:
			case ODataPayloadKind.Error:
			case ODataPayloadKind.IndividualProperty:
			case ODataPayloadKind.Delta:
			case ODataPayloadKind.Asynchronous:
				return !inRequest;
			case ODataPayloadKind.Entry:
			case ODataPayloadKind.Property:
			case ODataPayloadKind.EntityReferenceLink:
			case ODataPayloadKind.Value:
			case ODataPayloadKind.BinaryValue:
			case ODataPayloadKind.Batch:
				return true;
			case ODataPayloadKind.Parameter:
				return inRequest;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataUtilsInternal_IsPayloadKindSupported_UnreachableCodePath));
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000363EC File Offset: 0x000345EC
		internal static IEnumerable<T> ConcatEnumerables<T>(IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
		{
			if (enumerable1 == null)
			{
				return enumerable2;
			}
			if (enumerable2 == null)
			{
				return enumerable1;
			}
			return Enumerable.Concat<T>(enumerable1, enumerable2);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000363FF File Offset: 0x000345FF
		internal static bool IsNullable(this IEdmTypeReference type)
		{
			return type == null || type.IsNullable;
		}
	}
}
