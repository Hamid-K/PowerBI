using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Internal;
using System.Net.Http.Properties;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000002 RID: 2
	public class ByteRangeStreamContent : HttpContent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ByteRangeStreamContent(Stream content, RangeHeaderValue range, string mediaType)
			: this(content, range, new MediaTypeHeaderValue(mediaType), 4096)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002065 File Offset: 0x00000265
		public ByteRangeStreamContent(Stream content, RangeHeaderValue range, string mediaType, int bufferSize)
			: this(content, range, new MediaTypeHeaderValue(mediaType), bufferSize)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002077 File Offset: 0x00000277
		public ByteRangeStreamContent(Stream content, RangeHeaderValue range, MediaTypeHeaderValue mediaType)
			: this(content, range, mediaType, 4096)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002088 File Offset: 0x00000288
		public ByteRangeStreamContent(Stream content, RangeHeaderValue range, MediaTypeHeaderValue mediaType, int bufferSize)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			if (!content.CanSeek)
			{
				throw Error.Argument("content", Resources.ByteRangeStreamNotSeekable, new object[] { typeof(ByteRangeStreamContent).Name });
			}
			if (range == null)
			{
				throw Error.ArgumentNull("range");
			}
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			if (bufferSize < 1)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("bufferSize", bufferSize, 1);
			}
			if (!range.Unit.Equals("bytes", StringComparison.OrdinalIgnoreCase))
			{
				throw Error.Argument("range", Resources.ByteRangeStreamContentNotBytesRange, new object[] { range.Unit, "bytes" });
			}
			try
			{
				if (range.Ranges.Count <= 1)
				{
					if (range.Ranges.Count == 1)
					{
						try
						{
							ByteRangeStream byteRangeStream = new ByteRangeStream(content, range.Ranges.First<RangeItemHeaderValue>());
							this._byteRangeContent = new StreamContent(byteRangeStream, bufferSize);
							this._byteRangeContent.Headers.ContentType = mediaType;
							this._byteRangeContent.Headers.ContentRange = byteRangeStream.ContentRange;
							goto IL_0222;
						}
						catch (ArgumentOutOfRangeException)
						{
							ContentRangeHeaderValue contentRangeHeaderValue = new ContentRangeHeaderValue(content.Length);
							string text = Error.Format(Resources.ByteRangeStreamNoOverlap, new object[] { range.ToString() });
							throw new InvalidByteRangeException(contentRangeHeaderValue, text);
						}
					}
					throw Error.Argument("range", Resources.ByteRangeStreamContentNoRanges, new object[0]);
				}
				MultipartContent multipartContent = new MultipartContent("byteranges");
				this._byteRangeContent = multipartContent;
				foreach (RangeItemHeaderValue rangeItemHeaderValue in range.Ranges)
				{
					try
					{
						ByteRangeStream byteRangeStream2 = new ByteRangeStream(content, rangeItemHeaderValue);
						multipartContent.Add(new StreamContent(byteRangeStream2, bufferSize)
						{
							Headers = 
							{
								ContentType = mediaType,
								ContentRange = byteRangeStream2.ContentRange
							}
						});
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
				if (!multipartContent.Any<HttpContent>())
				{
					ContentRangeHeaderValue contentRangeHeaderValue2 = new ContentRangeHeaderValue(content.Length);
					string text2 = Error.Format(Resources.ByteRangeStreamNoneOverlap, new object[] { range.ToString() });
					throw new InvalidByteRangeException(contentRangeHeaderValue2, text2);
				}
				IL_0222:
				this._byteRangeContent.Headers.CopyTo(base.Headers);
				this._content = content;
				this._start = content.Position;
			}
			catch
			{
				if (this._byteRangeContent != null)
				{
					this._byteRangeContent.Dispose();
				}
				throw;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000235C File Offset: 0x0000055C
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			this._content.Position = this._start;
			return this._byteRangeContent.CopyToAsync(stream);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000237C File Offset: 0x0000057C
		protected override bool TryComputeLength(out long length)
		{
			long? contentLength = this._byteRangeContent.Headers.ContentLength;
			if (contentLength != null)
			{
				length = contentLength.Value;
				return true;
			}
			length = -1L;
			return false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000023B3 File Offset: 0x000005B3
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._disposed)
			{
				this._byteRangeContent.Dispose();
				this._content.Dispose();
				this._disposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000001 RID: 1
		private const string SupportedRangeUnit = "bytes";

		// Token: 0x04000002 RID: 2
		private const string ByteRangesContentSubtype = "byteranges";

		// Token: 0x04000003 RID: 3
		private const int DefaultBufferSize = 4096;

		// Token: 0x04000004 RID: 4
		private const int MinBufferSize = 1;

		// Token: 0x04000005 RID: 5
		private readonly Stream _content;

		// Token: 0x04000006 RID: 6
		private readonly long _start;

		// Token: 0x04000007 RID: 7
		private readonly HttpContent _byteRangeContent;

		// Token: 0x04000008 RID: 8
		private bool _disposed;
	}
}
