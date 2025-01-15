using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000BF RID: 191
	internal static class ODataUtilsInternal
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x00013DD0 File Offset: 0x00011FD0
		internal static void SetODataVersion(ODataMessage message, ODataMessageWriterSettings settings)
		{
			string text = ODataUtils.ODataVersionToString(settings.Version.Value);
			message.SetHeader("OData-Version", text);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00013E00 File Offset: 0x00012000
		internal static ODataVersion GetODataVersion(ODataMessage message, ODataVersion defaultVersion)
		{
			string header = message.GetHeader("OData-Version");
			if (!string.IsNullOrEmpty(header))
			{
				return ODataUtils.StringToODataVersion(header);
			}
			return defaultVersion;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00013E2C File Offset: 0x0001202C
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

		// Token: 0x0600086C RID: 2156 RVA: 0x00013E9A File Offset: 0x0001209A
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
			return enumerable1.Concat(enumerable2);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00013EAD File Offset: 0x000120AD
		internal static bool IsNullable(this IEdmTypeReference type)
		{
			return type == null || type.IsNullable;
		}
	}
}
