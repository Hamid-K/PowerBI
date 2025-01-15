using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Diagnostics;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000097 RID: 151
	[NullableContext(1)]
	[Nullable(0)]
	internal class LoggingPolicy : HttpPipelinePolicy
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x0000EC6E File Offset: 0x0000CE6E
		public LoggingPolicy(bool logContent, int maxLength, HttpMessageSanitizer sanitizer, [Nullable(2)] string assemblyName)
		{
			this._sanitizer = sanitizer;
			this._logContent = logContent;
			this._maxLength = maxLength;
			this._assemblyName = assemblyName;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000EC93 File Offset: 0x0000CE93
		public override void Process(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			if (!LoggingPolicy.s_eventSource.IsEnabled())
			{
				HttpPipelinePolicy.ProcessNext(message, pipeline);
				return;
			}
			this.ProcessAsync(message, pipeline, false).EnsureCompleted();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000ECB7 File Offset: 0x0000CEB7
		public override ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			if (!LoggingPolicy.s_eventSource.IsEnabled())
			{
				return HttpPipelinePolicy.ProcessNextAsync(message, pipeline);
			}
			return this.ProcessAsync(message, pipeline, true);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		private ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			LoggingPolicy.<ProcessAsync>d__9 <ProcessAsync>d__;
			<ProcessAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<ProcessAsync>d__.<>4__this = this;
			<ProcessAsync>d__.message = message;
			<ProcessAsync>d__.pipeline = pipeline;
			<ProcessAsync>d__.async = async;
			<ProcessAsync>d__.<>1__state = -1;
			<ProcessAsync>d__.<>t__builder.Start<LoggingPolicy.<ProcessAsync>d__9>(ref <ProcessAsync>d__);
			return <ProcessAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040001FA RID: 506
		private const double RequestTooLongTime = 3.0;

		// Token: 0x040001FB RID: 507
		private static readonly AzureCoreEventSource s_eventSource = AzureCoreEventSource.Singleton;

		// Token: 0x040001FC RID: 508
		private readonly bool _logContent;

		// Token: 0x040001FD RID: 509
		private readonly int _maxLength;

		// Token: 0x040001FE RID: 510
		private readonly HttpMessageSanitizer _sanitizer;

		// Token: 0x040001FF RID: 511
		[Nullable(2)]
		private readonly string _assemblyName;

		// Token: 0x02000126 RID: 294
		[Nullable(0)]
		private class LoggingStream : ReadOnlyStream
		{
			// Token: 0x0600080D RID: 2061 RVA: 0x0001DA82 File Offset: 0x0001BC82
			public LoggingStream(string requestId, LoggingPolicy.ContentEventSourceWrapper eventSourceWrapper, int maxLoggedBytes, Stream originalStream, bool error, [Nullable(2)] Encoding textEncoding)
			{
				this._requestId = requestId;
				this._eventSourceWrapper = eventSourceWrapper;
				this._maxLoggedBytes = maxLoggedBytes;
				this._originalStream = originalStream;
				this._error = error;
				this._textEncoding = textEncoding;
			}

			// Token: 0x0600080E RID: 2062 RVA: 0x0001DAB7 File Offset: 0x0001BCB7
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this._originalStream.Seek(offset, origin);
			}

			// Token: 0x0600080F RID: 2063 RVA: 0x0001DAC8 File Offset: 0x0001BCC8
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num2;
				int num = (num2 = this._originalStream.Read(buffer, offset, count));
				this.DecrementLength(ref num2);
				this.LogBuffer(buffer, offset, num2);
				return num;
			}

			// Token: 0x06000810 RID: 2064 RVA: 0x0001DAF6 File Offset: 0x0001BCF6
			private void LogBuffer(byte[] buffer, int offset, int count)
			{
				if (count == 0)
				{
					return;
				}
				this._eventSourceWrapper.Log(this._requestId, this._error, buffer, offset, count, this._textEncoding, new int?(this._blockNumber));
				this._blockNumber++;
			}

			// Token: 0x06000811 RID: 2065 RVA: 0x0001DB38 File Offset: 0x0001BD38
			public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				int num2;
				int num = (num2 = await this._originalStream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false));
				this.DecrementLength(ref num2);
				this.LogBuffer(buffer, offset, num2);
				return num;
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000812 RID: 2066 RVA: 0x0001DB9C File Offset: 0x0001BD9C
			public override bool CanRead
			{
				get
				{
					return this._originalStream.CanRead;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001DBA9 File Offset: 0x0001BDA9
			public override bool CanSeek
			{
				get
				{
					return this._originalStream.CanSeek;
				}
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001DBB6 File Offset: 0x0001BDB6
			public override long Length
			{
				get
				{
					return this._originalStream.Length;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001DBC3 File Offset: 0x0001BDC3
			// (set) Token: 0x06000816 RID: 2070 RVA: 0x0001DBD0 File Offset: 0x0001BDD0
			public override long Position
			{
				get
				{
					return this._originalStream.Position;
				}
				set
				{
					this._originalStream.Position = value;
				}
			}

			// Token: 0x06000817 RID: 2071 RVA: 0x0001DBDE File Offset: 0x0001BDDE
			public override void Close()
			{
				this._originalStream.Close();
			}

			// Token: 0x06000818 RID: 2072 RVA: 0x0001DBEB File Offset: 0x0001BDEB
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				this._originalStream.Dispose();
			}

			// Token: 0x06000819 RID: 2073 RVA: 0x0001DC00 File Offset: 0x0001BE00
			private void DecrementLength(ref int count)
			{
				int num = Math.Min(count, this._maxLoggedBytes);
				count = num;
				this._maxLoggedBytes -= count;
			}

			// Token: 0x04000459 RID: 1113
			private readonly string _requestId;

			// Token: 0x0400045A RID: 1114
			private readonly LoggingPolicy.ContentEventSourceWrapper _eventSourceWrapper;

			// Token: 0x0400045B RID: 1115
			private int _maxLoggedBytes;

			// Token: 0x0400045C RID: 1116
			private readonly Stream _originalStream;

			// Token: 0x0400045D RID: 1117
			private readonly bool _error;

			// Token: 0x0400045E RID: 1118
			[Nullable(2)]
			private readonly Encoding _textEncoding;

			// Token: 0x0400045F RID: 1119
			private int _blockNumber;
		}

		// Token: 0x02000127 RID: 295
		[Nullable(0)]
		private readonly struct ContentEventSourceWrapper
		{
			// Token: 0x0600081A RID: 2074 RVA: 0x0001DC2D File Offset: 0x0001BE2D
			public ContentEventSourceWrapper(AzureCoreEventSource eventSource, bool logContent, int maxLength, CancellationToken cancellationToken)
			{
				this._eventSource = (logContent ? eventSource : null);
				this._maxLength = maxLength;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x0001DC4C File Offset: 0x0001BE4C
			[NullableContext(2)]
			public async ValueTask LogAsync([Nullable(1)] string requestId, bool isError, Stream stream, Encoding textEncoding, bool async)
			{
				LoggingPolicy.ContentEventSourceWrapper.EventType eventType = LoggingPolicy.ContentEventSourceWrapper.ResponseOrError(isError);
				if (stream != null && this.IsEnabled(eventType))
				{
					byte[] array = await this.FormatAsync(stream, async).ConfigureAwait(false).EnsureCompleted(async);
					this.Log(requestId, eventType, array, textEncoding, null);
				}
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
			[NullableContext(2)]
			public async ValueTask LogAsync([Nullable(1)] string requestId, RequestContent content, Encoding textEncoding, bool async)
			{
				LoggingPolicy.ContentEventSourceWrapper.EventType eventType = LoggingPolicy.ContentEventSourceWrapper.EventType.Request;
				if (content != null && this.IsEnabled(eventType))
				{
					byte[] array = await this.FormatAsync(content, async).ConfigureAwait(false).EnsureCompleted(async);
					this.Log(requestId, eventType, array, textEncoding, null);
				}
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x0001DD2C File Offset: 0x0001BF2C
			public void Log(string requestId, bool isError, byte[] buffer, int offset, int length, [Nullable(2)] Encoding textEncoding, int? block = null)
			{
				LoggingPolicy.ContentEventSourceWrapper.EventType eventType = LoggingPolicy.ContentEventSourceWrapper.ResponseOrError(isError);
				if (buffer == null || !this.IsEnabled(eventType))
				{
					return;
				}
				int num = Math.Min(length, this._maxLength);
				byte[] array;
				if (length == num && offset == 0)
				{
					array = buffer;
				}
				else
				{
					array = new byte[num];
					Array.Copy(buffer, offset, array, 0, num);
				}
				this.Log(requestId, eventType, array, textEncoding, block);
			}

			// Token: 0x0600081E RID: 2078 RVA: 0x0001DD87 File Offset: 0x0001BF87
			public bool IsEnabled(bool isError)
			{
				return this.IsEnabled(LoggingPolicy.ContentEventSourceWrapper.ResponseOrError(isError));
			}

			// Token: 0x0600081F RID: 2079 RVA: 0x0001DD95 File Offset: 0x0001BF95
			private bool IsEnabled(LoggingPolicy.ContentEventSourceWrapper.EventType errorResponse)
			{
				return this._eventSource != null && (this._eventSource.IsEnabled(EventLevel.Informational, EventKeywords.All) || (errorResponse == LoggingPolicy.ContentEventSourceWrapper.EventType.ErrorResponse && this._eventSource.IsEnabled(EventLevel.Warning, EventKeywords.All)));
			}

			// Token: 0x06000820 RID: 2080 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
			[return: Nullable(new byte[] { 0, 1 })]
			private async ValueTask<byte[]> FormatAsync(RequestContent requestContent, bool async)
			{
				byte[] array;
				using (LoggingPolicy.ContentEventSourceWrapper.MaxLengthStream memoryStream = new LoggingPolicy.ContentEventSourceWrapper.MaxLengthStream(this._maxLength))
				{
					if (async)
					{
						await requestContent.WriteToAsync(memoryStream, this._cancellationToken).ConfigureAwait(false);
					}
					else
					{
						requestContent.WriteTo(memoryStream, this._cancellationToken);
					}
					array = memoryStream.ToArray();
				}
				return array;
			}

			// Token: 0x06000821 RID: 2081 RVA: 0x0001DE20 File Offset: 0x0001C020
			[return: Nullable(new byte[] { 0, 1 })]
			private async ValueTask<byte[]> FormatAsync(Stream content, bool async)
			{
				byte[] array;
				using (LoggingPolicy.ContentEventSourceWrapper.MaxLengthStream memoryStream = new LoggingPolicy.ContentEventSourceWrapper.MaxLengthStream(this._maxLength))
				{
					if (async)
					{
						await content.CopyToAsync(memoryStream, 8192, this._cancellationToken).ConfigureAwait(false);
					}
					else
					{
						content.CopyTo(memoryStream);
					}
					content.Seek(0L, SeekOrigin.Begin);
					array = memoryStream.ToArray();
				}
				return array;
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x0001DE78 File Offset: 0x0001C078
			private void Log(string requestId, LoggingPolicy.ContentEventSourceWrapper.EventType eventType, byte[] bytes, [Nullable(2)] Encoding textEncoding, int? block = null)
			{
				AzureCoreEventSource eventSource = this._eventSource;
				switch (eventType)
				{
				case LoggingPolicy.ContentEventSourceWrapper.EventType.Request:
					eventSource.RequestContent(requestId, bytes, textEncoding);
					return;
				case LoggingPolicy.ContentEventSourceWrapper.EventType.Response:
					if (block != null)
					{
						eventSource.ResponseContentBlock(requestId, block.Value, bytes, textEncoding);
						return;
					}
					eventSource.ResponseContent(requestId, bytes, textEncoding);
					return;
				case LoggingPolicy.ContentEventSourceWrapper.EventType.ErrorResponse:
					if (block != null)
					{
						eventSource.ErrorResponseContentBlock(requestId, block.Value, bytes, textEncoding);
						return;
					}
					eventSource.ErrorResponseContent(requestId, bytes, textEncoding);
					return;
				default:
					return;
				}
			}

			// Token: 0x06000823 RID: 2083 RVA: 0x0001DEF7 File Offset: 0x0001C0F7
			private static LoggingPolicy.ContentEventSourceWrapper.EventType ResponseOrError(bool isError)
			{
				if (!isError)
				{
					return LoggingPolicy.ContentEventSourceWrapper.EventType.Response;
				}
				return LoggingPolicy.ContentEventSourceWrapper.EventType.ErrorResponse;
			}

			// Token: 0x04000460 RID: 1120
			private const int CopyBufferSize = 8192;

			// Token: 0x04000461 RID: 1121
			[Nullable(2)]
			private readonly AzureCoreEventSource _eventSource;

			// Token: 0x04000462 RID: 1122
			private readonly int _maxLength;

			// Token: 0x04000463 RID: 1123
			private readonly CancellationToken _cancellationToken;

			// Token: 0x02000170 RID: 368
			[NullableContext(0)]
			private enum EventType
			{
				// Token: 0x0400058C RID: 1420
				Request,
				// Token: 0x0400058D RID: 1421
				Response,
				// Token: 0x0400058E RID: 1422
				ErrorResponse
			}

			// Token: 0x02000171 RID: 369
			[Nullable(0)]
			private class MaxLengthStream : MemoryStream
			{
				// Token: 0x06000940 RID: 2368 RVA: 0x000239DA File Offset: 0x00021BDA
				public MaxLengthStream(int maxLength)
				{
					this._bytesLeft = maxLength;
				}

				// Token: 0x06000941 RID: 2369 RVA: 0x000239E9 File Offset: 0x00021BE9
				public override void Write(byte[] buffer, int offset, int count)
				{
					this.DecrementLength(ref count);
					if (count > 0)
					{
						base.Write(buffer, offset, count);
					}
				}

				// Token: 0x06000942 RID: 2370 RVA: 0x00023A00 File Offset: 0x00021C00
				public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
				{
					if (count <= 0)
					{
						return Task.CompletedTask;
					}
					return base.WriteAsync(buffer, offset, count, cancellationToken);
				}

				// Token: 0x06000943 RID: 2371 RVA: 0x00023A18 File Offset: 0x00021C18
				private void DecrementLength(ref int count)
				{
					int num = Math.Min(count, this._bytesLeft);
					count = num;
					this._bytesLeft -= count;
				}

				// Token: 0x0400058F RID: 1423
				private int _bytesLeft;
			}
		}
	}
}
