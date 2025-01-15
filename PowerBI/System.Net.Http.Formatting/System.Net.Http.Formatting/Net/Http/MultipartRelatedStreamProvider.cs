using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200000C RID: 12
	public class MultipartRelatedStreamProvider : MultipartStreamProvider
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002861 File Offset: 0x00000A61
		public HttpContent RootContent
		{
			get
			{
				if (this._rootContent == null)
				{
					this._rootContent = MultipartRelatedStreamProvider.FindRootContent(this._parent, base.Contents);
				}
				return this._rootContent;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002888 File Offset: 0x00000A88
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (parent == null)
			{
				throw Error.ArgumentNull("parent");
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			if (this._parent == null)
			{
				this._parent = parent;
			}
			return new MemoryStream();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028BC File Offset: 0x00000ABC
		private static HttpContent FindRootContent(HttpContent parent, IEnumerable<HttpContent> children)
		{
			NameValueHeaderValue nameValueHeaderValue = MultipartRelatedStreamProvider.FindMultipartRelatedParameter(parent, "Start");
			if (nameValueHeaderValue == null)
			{
				return children.FirstOrDefault<HttpContent>();
			}
			string startValue = FormattingUtilities.UnquoteToken(nameValueHeaderValue.Value);
			return children.FirstOrDefault(delegate(HttpContent content)
			{
				IEnumerable<string> enumerable;
				return content.Headers.TryGetValues("Content-ID", out enumerable) && string.Equals(FormattingUtilities.UnquoteToken(enumerable.ElementAt(0)), startValue, StringComparison.OrdinalIgnoreCase);
			});
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002908 File Offset: 0x00000B08
		private static NameValueHeaderValue FindMultipartRelatedParameter(HttpContent content, string parameterName)
		{
			if (content == null)
			{
				return null;
			}
			MediaTypeHeaderValue contentType = content.Headers.ContentType;
			if (contentType == null || !content.IsMimeMultipartContent("related"))
			{
				return null;
			}
			return contentType.Parameters.FirstOrDefault((NameValueHeaderValue nvp) => string.Equals(nvp.Name, parameterName, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x04000015 RID: 21
		private const string RelatedSubType = "related";

		// Token: 0x04000016 RID: 22
		private const string ContentID = "Content-ID";

		// Token: 0x04000017 RID: 23
		private const string StartParameter = "Start";

		// Token: 0x04000018 RID: 24
		private HttpContent _rootContent;

		// Token: 0x04000019 RID: 25
		private HttpContent _parent;
	}
}
