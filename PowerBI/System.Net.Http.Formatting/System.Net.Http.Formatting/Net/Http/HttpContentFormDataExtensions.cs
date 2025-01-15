using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000004 RID: 4
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpContentFormDataExtensions
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000023F8 File Offset: 0x000005F8
		public static bool IsFormData(this HttpContent content)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			MediaTypeHeaderValue contentType = content.Headers.ContentType;
			return contentType != null && string.Equals("application/x-www-form-urlencoded", contentType.MediaType, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002435 File Offset: 0x00000635
		public static Task<NameValueCollection> ReadAsFormDataAsync(this HttpContent content)
		{
			return content.ReadAsFormDataAsync(CancellationToken.None);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002444 File Offset: 0x00000644
		public static Task<NameValueCollection> ReadAsFormDataAsync(this HttpContent content, CancellationToken cancellationToken)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			MediaTypeFormatter[] array = new MediaTypeFormatter[]
			{
				new FormUrlEncodedMediaTypeFormatter()
			};
			return HttpContentFormDataExtensions.ReadAsAsyncCore(content, array, cancellationToken);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002478 File Offset: 0x00000678
		private static async Task<NameValueCollection> ReadAsAsyncCore(HttpContent content, MediaTypeFormatter[] formatters, CancellationToken cancellationToken)
		{
			FormDataCollection formDataCollection = await content.ReadAsAsync(formatters, cancellationToken);
			return (formDataCollection == null) ? null : formDataCollection.ReadAsNameValueCollection();
		}

		// Token: 0x04000009 RID: 9
		private const string ApplicationFormUrlEncoded = "application/x-www-form-urlencoded";
	}
}
