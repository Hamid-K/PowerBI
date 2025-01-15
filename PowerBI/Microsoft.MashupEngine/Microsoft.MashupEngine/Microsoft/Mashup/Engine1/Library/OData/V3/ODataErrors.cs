using System;
using Microsoft.Data.OData;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008C2 RID: 2242
	internal static class ODataErrors
	{
		// Token: 0x06004026 RID: 16422 RVA: 0x000D60D0 File Offset: 0x000D42D0
		public static ValueException FeedContainsNoServiceUri(Uri uri, ODataPayloadKind payloadKind)
		{
			string text = uri.ToString();
			return ValueException.NewDataFormatError<Message2>(Strings.ODataFeedContainsNoServiceUri(text, payloadKind), TextValue.New(text), null);
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x000D60FC File Offset: 0x000D42FC
		public static ValueException QueryTooLong(IEngineHost engineHost, Uri uri, string resourceKind)
		{
			return DataSourceException.NewDataSourceError<Message0>(engineHost, Strings.ODataQueryTooLong, Resource.New(resourceKind, uri.AbsoluteUri), null, null);
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x000D6117 File Offset: 0x000D4317
		public static ValueException BadRequest(IEngineHost engineHost, string message, Uri uri, string resourceKind)
		{
			return DataSourceException.NewDataSourceError<Message3>(engineHost, Strings.RequestFailedWithStatusCode(resourceKind, 400, message), Resource.New(resourceKind, uri.AbsoluteUri), null, null);
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000D6140 File Offset: 0x000D4340
		public static ValueException UnsupportedUri(Uri uri)
		{
			string text = uri.ToString();
			return ValueException.NewDataFormatError<Message1>(Strings.ODataUnsupportedUri(text), TextValue.New(text), null);
		}
	}
}
