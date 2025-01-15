using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A49 RID: 2633
	public static class CompressionExtensions
	{
		// Token: 0x0600493D RID: 18749 RVA: 0x000F539C File Offset: 0x000F359C
		public static void AdjustForCompression(this WebRequest request)
		{
			MashupHttpWebRequest mashupHttpWebRequest = request as MashupHttpWebRequest;
			if (mashupHttpWebRequest != null && mashupHttpWebRequest.AutomaticDecompression != DecompressionMethods.None)
			{
				if (mashupHttpWebRequest.Headers["Accept-Encoding"] == null)
				{
					int automaticDecompression = (int)mashupHttpWebRequest.AutomaticDecompression;
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < RequestHeaders.CompressionTypes.Count; i++)
					{
						if ((automaticDecompression & (1 << i)) != 0)
						{
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append(RequestHeaders.CompressionTypes[i]);
						}
					}
					mashupHttpWebRequest.Headers["Accept-Encoding"] = stringBuilder.ToString();
				}
				mashupHttpWebRequest.AutomaticDecompression = DecompressionMethods.None;
			}
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x000F5444 File Offset: 0x000F3644
		public static Stream GetDecompressedResponseStream(this WebResponse response)
		{
			Stream responseStream = response.GetResponseStream();
			string text = response.Headers["Content-Encoding"];
			Stream stream = responseStream.CreateDecompressStream(text);
			if (responseStream != stream)
			{
				response.Headers["Content-Encoding"] = null;
			}
			return stream;
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x000F5485 File Offset: 0x000F3685
		public static Stream CreateDecompressStream(this Stream responseStream, string encoding)
		{
			if (RequestHeaders.IsGzip(encoding))
			{
				return Compression.Decompress(CompressionKind.GZip, responseStream);
			}
			if (RequestHeaders.IsDeflate(encoding))
			{
				return Compression.Decompress(CompressionKind.Deflate, responseStream);
			}
			return responseStream;
		}
	}
}
