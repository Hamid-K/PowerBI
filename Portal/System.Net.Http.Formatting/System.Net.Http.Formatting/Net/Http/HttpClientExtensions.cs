using System;
using System.ComponentModel;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000014 RID: 20
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpClientExtensions
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002D5E File Offset: 0x00000F5E
		public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
		{
			return client.PostAsJsonAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D6D File Offset: 0x00000F6D
		public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D7D File Offset: 0x00000F7D
		public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value)
		{
			return client.PostAsJsonAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D8C File Offset: 0x00000F8C
		public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D9C File Offset: 0x00000F9C
		public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, string requestUri, T value)
		{
			return client.PostAsXmlAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DAB File Offset: 0x00000FAB
		public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, new XmlMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DBB File Offset: 0x00000FBB
		public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value)
		{
			return client.PostAsXmlAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DCA File Offset: 0x00000FCA
		public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, new XmlMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002DDA File Offset: 0x00000FDA
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter)
		{
			return client.PostAsync(requestUri, value, formatter, CancellationToken.None);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002DEA File Offset: 0x00000FEA
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, formatter, null, cancellationToken);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, string mediaType)
		{
			return client.PostAsync(requestUri, value, formatter, mediaType, CancellationToken.None);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E0A File Offset: 0x0000100A
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, string mediaType, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, formatter, ObjectContent.BuildHeaderValue(mediaType), cancellationToken);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E20 File Offset: 0x00001020
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, CancellationToken cancellationToken)
		{
			if (client == null)
			{
				throw Error.ArgumentNull("client");
			}
			ObjectContent<T> objectContent = new ObjectContent<T>(value, formatter, mediaType);
			return client.PostAsync(requestUri, objectContent, cancellationToken);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E4F File Offset: 0x0000104F
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter)
		{
			return client.PostAsync(requestUri, value, formatter, CancellationToken.None);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E5F File Offset: 0x0000105F
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, formatter, null, cancellationToken);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E6D File Offset: 0x0000106D
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, string mediaType)
		{
			return client.PostAsync(requestUri, value, formatter, mediaType, CancellationToken.None);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E7F File Offset: 0x0000107F
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, string mediaType, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, formatter, ObjectContent.BuildHeaderValue(mediaType), cancellationToken);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E94 File Offset: 0x00001094
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, CancellationToken cancellationToken)
		{
			if (client == null)
			{
				throw Error.ArgumentNull("client");
			}
			ObjectContent<T> objectContent = new ObjectContent<T>(value, formatter, mediaType);
			return client.PostAsync(requestUri, objectContent, cancellationToken);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EC3 File Offset: 0x000010C3
		public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
		{
			return client.PutAsJsonAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002ED2 File Offset: 0x000010D2
		public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002EE2 File Offset: 0x000010E2
		public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value)
		{
			return client.PutAsJsonAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002EF1 File Offset: 0x000010F1
		public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F01 File Offset: 0x00001101
		public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, string requestUri, T value)
		{
			return client.PutAsXmlAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F10 File Offset: 0x00001110
		public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, new XmlMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F20 File Offset: 0x00001120
		public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value)
		{
			return client.PutAsXmlAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F2F File Offset: 0x0000112F
		public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, new XmlMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F3F File Offset: 0x0000113F
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter)
		{
			return client.PutAsync(requestUri, value, formatter, CancellationToken.None);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F4F File Offset: 0x0000114F
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, formatter, null, cancellationToken);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002F5D File Offset: 0x0000115D
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, string mediaType)
		{
			return client.PutAsync(requestUri, value, formatter, mediaType, CancellationToken.None);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F6F File Offset: 0x0000116F
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, string mediaType, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, formatter, ObjectContent.BuildHeaderValue(mediaType), cancellationToken);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F84 File Offset: 0x00001184
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, CancellationToken cancellationToken)
		{
			if (client == null)
			{
				throw Error.ArgumentNull("client");
			}
			ObjectContent<T> objectContent = new ObjectContent<T>(value, formatter, mediaType);
			return client.PutAsync(requestUri, objectContent, cancellationToken);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002FB3 File Offset: 0x000011B3
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter)
		{
			return client.PutAsync(requestUri, value, formatter, CancellationToken.None);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002FC3 File Offset: 0x000011C3
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, formatter, null, cancellationToken);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002FD1 File Offset: 0x000011D1
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, string mediaType)
		{
			return client.PutAsync(requestUri, value, formatter, mediaType, CancellationToken.None);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002FE3 File Offset: 0x000011E3
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, string mediaType, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, formatter, ObjectContent.BuildHeaderValue(mediaType), cancellationToken);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002FF8 File Offset: 0x000011F8
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, CancellationToken cancellationToken)
		{
			if (client == null)
			{
				throw Error.ArgumentNull("client");
			}
			ObjectContent<T> objectContent = new ObjectContent<T>(value, formatter, mediaType);
			return client.PutAsync(requestUri, objectContent, cancellationToken);
		}
	}
}
