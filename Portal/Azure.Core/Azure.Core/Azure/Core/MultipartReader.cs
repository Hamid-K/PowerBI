using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core
{
	// Token: 0x02000050 RID: 80
	internal class MultipartReader
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00007767 File Offset: 0x00005967
		public MultipartReader(string boundary, Stream stream)
			: this(boundary, stream, 4096)
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007778 File Offset: 0x00005978
		public MultipartReader(string boundary, Stream stream, int bufferSize)
		{
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (bufferSize < boundary.Length + 8)
			{
				throw new ArgumentOutOfRangeException("bufferSize", bufferSize, "Insufficient buffer space, the buffer must be larger than the boundary: " + boundary);
			}
			this._stream = new BufferedReadStream(stream, bufferSize);
			this._boundary = new MultipartBoundary(boundary, false);
			this._currentStream = new MultipartReaderStream(this._stream, this._boundary)
			{
				LengthLimit = new long?((long)this.HeadersLengthLimit)
			};
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000782B File Offset: 0x00005A2B
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00007833 File Offset: 0x00005A33
		public int HeadersCountLimit { get; set; } = 16;

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000783C File Offset: 0x00005A3C
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00007844 File Offset: 0x00005A44
		public int HeadersLengthLimit { get; set; } = 16384;

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000784D File Offset: 0x00005A4D
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00007855 File Offset: 0x00005A55
		public long? BodyLengthLimit { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000785E File Offset: 0x00005A5E
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00007866 File Offset: 0x00005A66
		public bool ExpectBoundariesWithCRLF { get; set; } = true;

		// Token: 0x0600026D RID: 621 RVA: 0x00007870 File Offset: 0x00005A70
		public async Task<MultipartSection> ReadNextSectionAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			await this._currentStream.DrainAsync(cancellationToken).ConfigureAwait(false);
			MultipartSection multipartSection;
			if (this._currentStream.FinalBoundaryFound)
			{
				await this._stream.DrainAsync(new long?((long)this.HeadersLengthLimit), cancellationToken).ConfigureAwait(false);
				multipartSection = null;
			}
			else
			{
				Dictionary<string, string[]> dictionary = await this.ReadHeadersAsync(cancellationToken).ConfigureAwait(false);
				this._boundary.ExpectLeadingCrlf = this.ExpectBoundariesWithCRLF;
				this._currentStream = new MultipartReaderStream(this._stream, this._boundary)
				{
					LengthLimit = this.BodyLengthLimit
				};
				long? num = (this._stream.CanSeek ? new long?(this._stream.Position) : null);
				multipartSection = new MultipartSection
				{
					Headers = dictionary,
					Body = this._currentStream,
					BaseStreamOffset = num
				};
			}
			return multipartSection;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000078BC File Offset: 0x00005ABC
		private Task<Dictionary<string, string[]>> ReadHeadersAsync(CancellationToken cancellationToken)
		{
			MultipartReader.<ReadHeadersAsync>d__25 <ReadHeadersAsync>d__;
			<ReadHeadersAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Dictionary<string, string[]>>.Create();
			<ReadHeadersAsync>d__.<>4__this = this;
			<ReadHeadersAsync>d__.cancellationToken = cancellationToken;
			<ReadHeadersAsync>d__.<>1__state = -1;
			<ReadHeadersAsync>d__.<>t__builder.Start<MultipartReader.<ReadHeadersAsync>d__25>(ref <ReadHeadersAsync>d__);
			return <ReadHeadersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400010B RID: 267
		public const int DefaultHeadersCountLimit = 16;

		// Token: 0x0400010C RID: 268
		public const int DefaultHeadersLengthLimit = 16384;

		// Token: 0x0400010D RID: 269
		private const int DefaultBufferSize = 4096;

		// Token: 0x0400010E RID: 270
		private readonly BufferedReadStream _stream;

		// Token: 0x0400010F RID: 271
		private readonly MultipartBoundary _boundary;

		// Token: 0x04000110 RID: 272
		private MultipartReaderStream _currentStream;
	}
}
