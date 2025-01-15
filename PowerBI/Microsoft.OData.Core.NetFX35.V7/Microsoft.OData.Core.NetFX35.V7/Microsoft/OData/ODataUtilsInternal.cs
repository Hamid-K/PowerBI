using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200009F RID: 159
	internal static class ODataUtilsInternal
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x000104D8 File Offset: 0x0000E6D8
		internal static void SetODataVersion(ODataMessage message, ODataMessageWriterSettings settings)
		{
			string text = ODataUtils.ODataVersionToString(settings.Version.Value);
			message.SetHeader("OData-Version", text);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00010508 File Offset: 0x0000E708
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

		// Token: 0x06000610 RID: 1552 RVA: 0x00010534 File Offset: 0x0000E734
		internal static bool IsPayloadKindSupported(ODataPayloadKind payloadKind, bool inRequest)
		{
			switch (payloadKind)
			{
			case ODataPayloadKind.ResourceSet:
			case ODataPayloadKind.EntityReferenceLinks:
			case ODataPayloadKind.Collection:
			case ODataPayloadKind.ServiceDocument:
			case ODataPayloadKind.MetadataDocument:
			case ODataPayloadKind.Error:
			case ODataPayloadKind.IndividualProperty:
			case ODataPayloadKind.Delta:
			case ODataPayloadKind.Asynchronous:
				return !inRequest;
			case ODataPayloadKind.Resource:
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

		// Token: 0x06000611 RID: 1553 RVA: 0x000105A2 File Offset: 0x0000E7A2
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

		// Token: 0x06000612 RID: 1554 RVA: 0x000105B5 File Offset: 0x0000E7B5
		internal static bool IsNullable(this IEdmTypeReference type)
		{
			return type == null || type.IsNullable;
		}
	}
}
