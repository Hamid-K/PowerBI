using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000006 RID: 6
	internal static class MultipartFormDataStreamProviderHelper
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000025B4 File Offset: 0x000007B4
		public static bool IsFileContent(HttpContent parent, HttpContentHeaders headers)
		{
			if (parent == null)
			{
				throw Error.ArgumentNull("parent");
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;
			if (contentDisposition == null)
			{
				throw Error.InvalidOperation(Resources.MultipartFormDataStreamProviderNoContentDisposition, new object[] { "Content-Disposition" });
			}
			return !string.IsNullOrEmpty(contentDisposition.FileName);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002610 File Offset: 0x00000810
		public static async Task ReadFormDataAsync(Collection<HttpContent> contents, NameValueCollection formData, CancellationToken cancellationToken)
		{
			foreach (HttpContent httpContent in contents)
			{
				ContentDispositionHeaderValue contentDisposition = httpContent.Headers.ContentDisposition;
				if (string.IsNullOrEmpty(contentDisposition.FileName))
				{
					string formFieldName = FormattingUtilities.UnquoteToken(contentDisposition.Name) ?? string.Empty;
					cancellationToken.ThrowIfCancellationRequested();
					string text = await httpContent.ReadAsStringAsync();
					formData.Add(formFieldName, text);
					formFieldName = null;
				}
			}
			IEnumerator<HttpContent> enumerator = null;
		}
	}
}
