using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x0200013D RID: 317
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This class does not own the BufferingReadStream instance.")]
	internal abstract class ODataMessage
	{
		// Token: 0x06000C09 RID: 3081 RVA: 0x0002D508 File Offset: 0x0002B708
		protected ODataMessage(bool writing, bool disableMessageStreamDisposal, long maxMessageSize)
		{
			this.writing = writing;
			this.disableMessageStreamDisposal = disableMessageStreamDisposal;
			this.maxMessageSize = maxMessageSize;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000C0A RID: 3082
		public abstract IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0002D525 File Offset: 0x0002B725
		protected internal BufferingReadStream BufferingReadStream
		{
			get
			{
				return this.bufferingReadStream;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0002D52D File Offset: 0x0002B72D
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0002D535 File Offset: 0x0002B735
		protected internal bool? UseBufferingReadStream
		{
			get
			{
				return this.useBufferingReadStream;
			}
			set
			{
				this.useBufferingReadStream = value;
			}
		}

		// Token: 0x06000C0E RID: 3086
		public abstract string GetHeader(string headerName);

		// Token: 0x06000C0F RID: 3087
		public abstract void SetHeader(string headerName, string headerValue);

		// Token: 0x06000C10 RID: 3088
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Intentionally a method.")]
		public abstract Stream GetStream();

		// Token: 0x06000C11 RID: 3089
		internal abstract TInterface QueryInterface<TInterface>() where TInterface : class;

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002D540 File Offset: 0x0002B740
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "We don't own the underlying stream, so we should not dispose the wrapper.")]
		protected internal Stream GetStream(Func<Stream> messageStreamFunc, bool isRequest)
		{
			if (!this.writing)
			{
				BufferingReadStream bufferingReadStream = this.TryGetBufferingReadStream();
				if (bufferingReadStream != null)
				{
					return bufferingReadStream;
				}
			}
			Stream stream = messageStreamFunc.Invoke();
			ODataMessage.ValidateMessageStream(stream, isRequest);
			bool flag = !this.writing && this.maxMessageSize > 0L;
			if (this.disableMessageStreamDisposal && flag)
			{
				stream = MessageStreamWrapper.CreateNonDisposingStreamWithMaxSize(stream, this.maxMessageSize);
			}
			else if (this.disableMessageStreamDisposal)
			{
				stream = MessageStreamWrapper.CreateNonDisposingStream(stream);
			}
			else if (flag)
			{
				stream = MessageStreamWrapper.CreateStreamWithMaxSize(stream, this.maxMessageSize);
			}
			if (!this.writing && this.useBufferingReadStream == true)
			{
				this.bufferingReadStream = new BufferingReadStream(stream);
				stream = this.bufferingReadStream;
			}
			return stream;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002D5F8 File Offset: 0x0002B7F8
		protected void VerifyCanSetHeader()
		{
			if (!this.writing)
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002D610 File Offset: 0x0002B810
		private static void ValidateMessageStream(Stream stream, bool isRequest)
		{
			if (stream == null)
			{
				string text = (isRequest ? Strings.ODataRequestMessage_MessageStreamIsNull : Strings.ODataResponseMessage_MessageStreamIsNull);
				throw new ODataException(text);
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002D638 File Offset: 0x0002B838
		private BufferingReadStream TryGetBufferingReadStream()
		{
			if (this.bufferingReadStream == null)
			{
				return null;
			}
			BufferingReadStream bufferingReadStream = this.bufferingReadStream;
			if (this.bufferingReadStream.IsBuffering)
			{
				this.bufferingReadStream.ResetStream();
			}
			else
			{
				this.bufferingReadStream = null;
			}
			return bufferingReadStream;
		}

		// Token: 0x04000500 RID: 1280
		private readonly bool writing;

		// Token: 0x04000501 RID: 1281
		private readonly bool disableMessageStreamDisposal;

		// Token: 0x04000502 RID: 1282
		private readonly long maxMessageSize;

		// Token: 0x04000503 RID: 1283
		private bool? useBufferingReadStream;

		// Token: 0x04000504 RID: 1284
		private BufferingReadStream bufferingReadStream;
	}
}
