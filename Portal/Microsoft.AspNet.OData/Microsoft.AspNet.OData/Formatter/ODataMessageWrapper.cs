using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000195 RID: 405
	internal class ODataMessageWrapper : IODataRequestMessageAsync, IODataRequestMessage, IODataResponseMessageAsync, IODataResponseMessage, IODataPayloadUriConverter, IContainerProvider, IDisposable
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x000345EA File Offset: 0x000327EA
		public ODataMessageWrapper()
			: this(null, null)
		{
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000345F4 File Offset: 0x000327F4
		public ODataMessageWrapper(Stream stream)
			: this(stream, null)
		{
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000345FE File Offset: 0x000327FE
		public ODataMessageWrapper(Stream stream, Dictionary<string, string> headers)
			: this(stream, headers, null)
		{
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00034609 File Offset: 0x00032809
		public ODataMessageWrapper(Stream stream, Dictionary<string, string> headers, IDictionary<string, string> contentIdMapping)
		{
			this._stream = stream;
			if (headers != null)
			{
				this._headers = headers;
			}
			else
			{
				this._headers = new Dictionary<string, string>();
			}
			this._contentIdMapping = contentIdMapping ?? new Dictionary<string, string>();
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0003463F File Offset: 0x0003283F
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this._headers;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		public string Method
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		public Uri Url
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		public int StatusCode
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00034647 File Offset: 0x00032847
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x0003464F File Offset: 0x0003284F
		public IServiceProvider Container { get; set; }

		// Token: 0x06000D2A RID: 3370 RVA: 0x00034658 File Offset: 0x00032858
		public string GetHeader(string headerName)
		{
			string text;
			if (this._headers.TryGetValue(headerName, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00034678 File Offset: 0x00032878
		public Stream GetStream()
		{
			return this._stream;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00034680 File Offset: 0x00032880
		public Task<Stream> GetStreamAsync()
		{
			TaskCompletionSource<Stream> taskCompletionSource = new TaskCompletionSource<Stream>();
			taskCompletionSource.SetResult(this._stream);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00034698 File Offset: 0x00032898
		public void SetHeader(string headerName, string headerValue)
		{
			this._headers[headerName] = headerValue;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000346A8 File Offset: 0x000328A8
		public Uri ConvertPayloadUri(Uri baseUri, Uri payloadUri)
		{
			if (payloadUri == null)
			{
				throw new ArgumentNullException("payloadUri");
			}
			string originalString = payloadUri.OriginalString;
			if (ODataMessageWrapper.ContentIdReferencePattern.IsMatch(originalString))
			{
				return new Uri(ContentIdHelpers.ResolveContentId(originalString, this._contentIdMapping), UriKind.RelativeOrAbsolute);
			}
			return null;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000346F1 File Offset: 0x000328F1
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000346FA File Offset: 0x000328FA
		protected void Dispose(bool disposing)
		{
			if (disposing && this._stream != null)
			{
				this._stream.Dispose();
			}
		}

		// Token: 0x040003CC RID: 972
		private Stream _stream;

		// Token: 0x040003CD RID: 973
		private Dictionary<string, string> _headers;

		// Token: 0x040003CE RID: 974
		private IDictionary<string, string> _contentIdMapping;

		// Token: 0x040003CF RID: 975
		private static readonly Regex ContentIdReferencePattern = new Regex("\\$\\d", RegexOptions.Compiled);
	}
}
