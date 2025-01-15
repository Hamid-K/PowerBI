using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Formatting.Parsers;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200001F RID: 31
	internal class MimeBodyPart : IDisposable
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00004468 File Offset: 0x00002668
		public MimeBodyPart(MultipartStreamProvider streamProvider, int maxBodyPartHeaderSize, HttpContent parentContent)
		{
			this._streamProvider = streamProvider;
			this._parentContent = parentContent;
			this.Segments = new List<ArraySegment<byte>>(2);
			this._headers = FormattingUtilities.CreateEmptyContentHeaders();
			this.HeaderParser = new InternetMessageFormatHeaderParser(this._headers, maxBodyPartHeaderSize, true);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000044A8 File Offset: 0x000026A8
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000044B0 File Offset: 0x000026B0
		public InternetMessageFormatHeaderParser HeaderParser { get; private set; }

		// Token: 0x060000DF RID: 223 RVA: 0x000044B9 File Offset: 0x000026B9
		public HttpContent GetCompletedHttpContent()
		{
			if (this._content == null)
			{
				return null;
			}
			this._headers.CopyTo(this._content.Headers);
			return this._content;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000044E1 File Offset: 0x000026E1
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000044E9 File Offset: 0x000026E9
		public List<ArraySegment<byte>> Segments { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000044F2 File Offset: 0x000026F2
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000044FA File Offset: 0x000026FA
		public bool IsComplete { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004503 File Offset: 0x00002703
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000450B File Offset: 0x0000270B
		public bool IsFinal { get; set; }

		// Token: 0x060000E6 RID: 230 RVA: 0x00004514 File Offset: 0x00002714
		public async Task WriteSegment(ArraySegment<byte> segment, CancellationToken cancellationToken)
		{
			await this.GetOutputStream().WriteAsync(segment.Array, segment.Offset, segment.Count, cancellationToken);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000456C File Offset: 0x0000276C
		private Stream GetOutputStream()
		{
			if (this._outputStream == null)
			{
				try
				{
					this._outputStream = this._streamProvider.GetStream(this._parentContent, this._headers);
				}
				catch (Exception ex)
				{
					throw Error.InvalidOperation(ex, Resources.ReadAsMimeMultipartStreamProviderException, new object[] { this._streamProvider.GetType().Name });
				}
				if (this._outputStream == null)
				{
					throw Error.InvalidOperation(Resources.ReadAsMimeMultipartStreamProviderNull, new object[]
					{
						this._streamProvider.GetType().Name,
						MimeBodyPart._streamType.Name
					});
				}
				if (!this._outputStream.CanWrite)
				{
					throw Error.InvalidOperation(Resources.ReadAsMimeMultipartStreamProviderReadOnly, new object[]
					{
						this._streamProvider.GetType().Name,
						MimeBodyPart._streamType.Name
					});
				}
				this._content = new StreamContent(this._outputStream);
			}
			return this._outputStream;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004668 File Offset: 0x00002868
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004677 File Offset: 0x00002877
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.CleanupOutputStream();
				this.CleanupHttpContent();
				this._parentContent = null;
				this.HeaderParser = null;
				this.Segments.Clear();
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000046A1 File Offset: 0x000028A1
		private void CleanupHttpContent()
		{
			if (!this.IsComplete && this._content != null)
			{
				this._content.Dispose();
			}
			this._content = null;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000046C8 File Offset: 0x000028C8
		private void CleanupOutputStream()
		{
			if (this._outputStream != null)
			{
				MemoryStream memoryStream = this._outputStream as MemoryStream;
				if (memoryStream != null)
				{
					memoryStream.Position = 0L;
				}
				else
				{
					this._outputStream.Close();
				}
				this._outputStream = null;
			}
		}

		// Token: 0x0400004F RID: 79
		private static readonly Type _streamType = typeof(Stream);

		// Token: 0x04000050 RID: 80
		private Stream _outputStream;

		// Token: 0x04000051 RID: 81
		private MultipartStreamProvider _streamProvider;

		// Token: 0x04000052 RID: 82
		private HttpContent _parentContent;

		// Token: 0x04000053 RID: 83
		private HttpContent _content;

		// Token: 0x04000054 RID: 84
		private HttpContentHeaders _headers;
	}
}
