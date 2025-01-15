using System;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Json;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008BA RID: 2234
	internal static class JsonLightMetadata
	{
		// Token: 0x06003FD2 RID: 16338 RVA: 0x000D3D7C File Offset: 0x000D1F7C
		internal static bool TryGetMetadataUri(Stream stream, out Uri metadata)
		{
			using (StreamReader streamReader = new StreamReader(stream))
			{
				if (JsonLightMetadata.TryGetMetadataUri(streamReader, out metadata))
				{
					return true;
				}
			}
			metadata = null;
			return false;
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x000D3DC0 File Offset: 0x000D1FC0
		internal static bool TryGetMetadataUri(StreamReader reader, out Uri metadata)
		{
			JsonTokenizer jsonTokenizer = new JsonTokenizer(reader, false, false, null);
			if (jsonTokenizer.GetNextToken() == JsonToken.RecordStart && jsonTokenizer.GetNextToken() == JsonToken.RecordKey && jsonTokenizer.GetTokenText() == "odata.metadata" && jsonTokenizer.GetNextToken() == JsonToken.String && Uri.TryCreate(jsonTokenizer.GetTokenText(), UriKind.RelativeOrAbsolute, out metadata))
			{
				metadata = new UriBuilder(metadata)
				{
					Fragment = null
				}.Uri;
				return true;
			}
			metadata = null;
			return false;
		}
	}
}
