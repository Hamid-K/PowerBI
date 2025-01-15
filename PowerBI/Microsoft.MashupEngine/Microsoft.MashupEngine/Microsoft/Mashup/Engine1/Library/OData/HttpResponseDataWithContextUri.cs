using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Json;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000729 RID: 1833
	internal class HttpResponseDataWithContextUri : HttpResponseData
	{
		// Token: 0x06003690 RID: 13968 RVA: 0x000ADE8C File Offset: 0x000AC08C
		private HttpResponseDataWithContextUri(string contentType, long contentLength, int statusCode, Dictionary<string, string> headers, Uri responseUri, Stream responseStream, string contextUri)
			: base(contentType, contentLength, statusCode, headers, responseUri, responseStream)
		{
			this.contextUri = contextUri;
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06003691 RID: 13969 RVA: 0x000ADEA5 File Offset: 0x000AC0A5
		public string ContextUri
		{
			get
			{
				return this.contextUri;
			}
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000ADEB0 File Offset: 0x000AC0B0
		public static HttpResponseDataWithContextUri FixupRelativeMetadataUrl(HttpResponseData response)
		{
			int i = 0;
			byte[] array = new byte[16384];
			while (i < array.Length)
			{
				int num = response.Stream.Read(array, i, array.Length - i);
				if (num == 0)
				{
					break;
				}
				i += num;
			}
			ArrayBuilder<Stream> arrayBuilder = new ArrayBuilder<Stream>(3);
			string text = null;
			int num2 = 0;
			using (StreamReader streamReader = new StreamReader(new MemoryStream(array, 0, i)))
			{
				JsonTokenizer jsonTokenizer = new JsonTokenizer(streamReader, false, false, null);
				if (jsonTokenizer.GetNextToken() == JsonToken.RecordStart && jsonTokenizer.GetNextToken() == JsonToken.RecordKey && jsonTokenizer.GetTokenText() == "@odata.context" && jsonTokenizer.GetNextToken() == JsonToken.String)
				{
					string tokenText = jsonTokenizer.GetTokenText();
					Uri uri;
					if (Uri.TryCreate(tokenText, UriKind.RelativeOrAbsolute, out uri))
					{
						if (uri.IsAbsoluteUri)
						{
							text = uri.AbsoluteUri;
						}
						else
						{
							Uri uri2 = response.ResponseUri;
							if (tokenText == "$metadata" && !uri2.AbsoluteUri.EndsWith("/", StringComparison.Ordinal))
							{
								uri2 = new Uri(uri2.AbsoluteUri + "/");
							}
							if (Uri.TryCreate(uri2, tokenText, out uri))
							{
								text = uri.AbsoluteUri;
								string text2 = "{\"@odata.context\":" + JsonFormatter.FormatString(text);
								byte[] bytes = Encoding.UTF8.GetBytes(text2);
								arrayBuilder.Add(new MemoryStream(bytes));
								int inputPosition = jsonTokenizer.GetInputPosition();
								arrayBuilder.Add(new MemoryStream(array, inputPosition, i - inputPosition));
								num2 = bytes.Length - inputPosition;
							}
						}
					}
				}
			}
			if (num2 == 0)
			{
				arrayBuilder.Add(new MemoryStream(array, 0, i));
			}
			long num3 = response.ContentLength;
			if (num3 > 0L)
			{
				num3 += (long)num2;
			}
			arrayBuilder.Add(response.Stream);
			return new HttpResponseDataWithContextUri(response.ContentType, num3, response.StatusCode, response.Headers, response.ResponseUri, StreamExtensions.Concat(arrayBuilder.ToArray()), text);
		}

		// Token: 0x04001C09 RID: 7177
		private const int metadataFixupBodySize = 16384;

		// Token: 0x04001C0A RID: 7178
		private readonly string contextUri;
	}
}
