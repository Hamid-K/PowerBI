using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Internal;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000010 RID: 16
	public class PushStreamContent : HttpContent
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002A17 File Offset: 0x00000C17
		public PushStreamContent(Action<Stream, HttpContent, TransportContext> onStreamAvailable)
			: this(PushStreamContent.Taskify(onStreamAvailable), null)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A26 File Offset: 0x00000C26
		public PushStreamContent(Func<Stream, HttpContent, TransportContext, Task> onStreamAvailable)
			: this(onStreamAvailable, null)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A30 File Offset: 0x00000C30
		public PushStreamContent(Action<Stream, HttpContent, TransportContext> onStreamAvailable, string mediaType)
			: this(PushStreamContent.Taskify(onStreamAvailable), new MediaTypeHeaderValue(mediaType))
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A44 File Offset: 0x00000C44
		public PushStreamContent(Func<Stream, HttpContent, TransportContext, Task> onStreamAvailable, string mediaType)
			: this(onStreamAvailable, new MediaTypeHeaderValue(mediaType))
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A53 File Offset: 0x00000C53
		public PushStreamContent(Action<Stream, HttpContent, TransportContext> onStreamAvailable, MediaTypeHeaderValue mediaType)
			: this(PushStreamContent.Taskify(onStreamAvailable), mediaType)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A62 File Offset: 0x00000C62
		public PushStreamContent(Func<Stream, HttpContent, TransportContext, Task> onStreamAvailable, MediaTypeHeaderValue mediaType)
		{
			if (onStreamAvailable == null)
			{
				throw Error.ArgumentNull("onStreamAvailable");
			}
			this._onStreamAvailable = onStreamAvailable;
			base.Headers.ContentType = mediaType ?? MediaTypeConstants.ApplicationOctetStreamMediaType;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A94 File Offset: 0x00000C94
		private static Func<Stream, HttpContent, TransportContext, Task> Taskify(Action<Stream, HttpContent, TransportContext> onStreamAvailable)
		{
			if (onStreamAvailable == null)
			{
				throw Error.ArgumentNull("onStreamAvailable");
			}
			return delegate(Stream stream, HttpContent content, TransportContext transportContext)
			{
				onStreamAvailable(stream, content, transportContext);
				return TaskHelpers.Completed();
			};
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AC0 File Offset: 0x00000CC0
		protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			TaskCompletionSource<bool> serializeToStreamTask = new TaskCompletionSource<bool>();
			Stream stream2 = new PushStreamContent.CompleteTaskOnCloseStream(stream, serializeToStreamTask);
			await this._onStreamAvailable(stream2, this, context);
			await serializeToStreamTask.Task;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B15 File Offset: 0x00000D15
		protected override bool TryComputeLength(out long length)
		{
			length = -1L;
			return false;
		}

		// Token: 0x0400001D RID: 29
		private readonly Func<Stream, HttpContent, TransportContext, Task> _onStreamAvailable;

		// Token: 0x02000067 RID: 103
		internal class CompleteTaskOnCloseStream : DelegatingStream
		{
			// Token: 0x060003A0 RID: 928 RVA: 0x0000D147 File Offset: 0x0000B347
			public CompleteTaskOnCloseStream(Stream innerStream, TaskCompletionSource<bool> serializeToStreamTask)
				: base(innerStream)
			{
				this._serializeToStreamTask = serializeToStreamTask;
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x0000D157 File Offset: 0x0000B357
			public override void Close()
			{
				this._serializeToStreamTask.TrySetResult(true);
			}

			// Token: 0x0400014A RID: 330
			private TaskCompletionSource<bool> _serializeToStreamTask;
		}
	}
}
