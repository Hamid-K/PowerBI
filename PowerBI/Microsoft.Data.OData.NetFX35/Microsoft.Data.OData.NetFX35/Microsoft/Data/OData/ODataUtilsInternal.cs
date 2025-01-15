using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x02000299 RID: 665
	internal static class ODataUtilsInternal
	{
		// Token: 0x06001532 RID: 5426 RVA: 0x0004D5D8 File Offset: 0x0004B7D8
		internal static Version ToDataServiceVersion(this ODataVersion version)
		{
			switch (version)
			{
			case ODataVersion.V1:
				return new Version(1, 0);
			case ODataVersion.V2:
				return new Version(2, 0);
			case ODataVersion.V3:
				return new Version(3, 0);
			default:
			{
				string text = Strings.General_InternalError(InternalErrorCodes.ODataUtilsInternal_ToDataServiceVersion_UnreachableCodePath);
				throw new ODataException(text);
			}
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0004D628 File Offset: 0x0004B828
		internal static void SetDataServiceVersion(ODataMessage message, ODataMessageWriterSettings settings)
		{
			string text = ODataUtils.ODataVersionToString(settings.Version.Value) + ";";
			message.SetHeader("DataServiceVersion", text);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0004D660 File Offset: 0x0004B860
		internal static ODataVersion GetDataServiceVersion(ODataMessage message, ODataVersion defaultVersion)
		{
			string header = message.GetHeader("DataServiceVersion");
			string text = header;
			if (!string.IsNullOrEmpty(text))
			{
				return ODataUtils.StringToODataVersion(text);
			}
			return defaultVersion;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0004D68C File Offset: 0x0004B88C
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

		// Token: 0x06001536 RID: 5430 RVA: 0x0004D6F0 File Offset: 0x0004B8F0
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

		// Token: 0x06001537 RID: 5431 RVA: 0x0004D703 File Offset: 0x0004B903
		internal static SelectedPropertiesNode SelectedProperties(this ODataMetadataDocumentUri metadataDocumentUri)
		{
			if (metadataDocumentUri == null)
			{
				return SelectedPropertiesNode.Create(null);
			}
			return SelectedPropertiesNode.Create(metadataDocumentUri.SelectClause);
		}
	}
}
