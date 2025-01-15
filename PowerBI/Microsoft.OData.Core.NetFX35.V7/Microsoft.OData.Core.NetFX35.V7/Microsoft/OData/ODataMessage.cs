using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x0200006D RID: 109
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This class does not own the BufferingReadStream instance.")]
	internal abstract class ODataMessage
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0000A78E File Offset: 0x0000898E
		protected ODataMessage(bool writing, bool enableMessageStreamDisposal, long maxMessageSize)
		{
			this.writing = writing;
			this.enableMessageStreamDisposal = enableMessageStreamDisposal;
			this.maxMessageSize = maxMessageSize;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600037F RID: 895
		public abstract IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000A7AB File Offset: 0x000089AB
		protected internal BufferingReadStream BufferingReadStream
		{
			get
			{
				return this.bufferingReadStream;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000A7B3 File Offset: 0x000089B3
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000A7BB File Offset: 0x000089BB
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

		// Token: 0x06000383 RID: 899
		public abstract string GetHeader(string headerName);

		// Token: 0x06000384 RID: 900
		public abstract void SetHeader(string headerName, string headerValue);

		// Token: 0x06000385 RID: 901
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Intentionally a method.")]
		public abstract Stream GetStream();

		// Token: 0x06000386 RID: 902
		internal abstract TInterface QueryInterface<TInterface>() where TInterface : class;

		// Token: 0x06000387 RID: 903 RVA: 0x0000A7C4 File Offset: 0x000089C4
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
			if (!this.enableMessageStreamDisposal && flag)
			{
				stream = MessageStreamWrapper.CreateNonDisposingStreamWithMaxSize(stream, this.maxMessageSize);
			}
			else if (!this.enableMessageStreamDisposal)
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

		// Token: 0x06000388 RID: 904 RVA: 0x0000A883 File Offset: 0x00008A83
		protected void VerifyCanSetHeader()
		{
			if (!this.writing)
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000A898 File Offset: 0x00008A98
		private static void ValidateMessageStream(Stream stream, bool isRequest)
		{
			if (stream == null)
			{
				string text = (isRequest ? Strings.ODataRequestMessage_MessageStreamIsNull : Strings.ODataResponseMessage_MessageStreamIsNull);
				throw new ODataException(text);
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000A8C0 File Offset: 0x00008AC0
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

		// Token: 0x040001D7 RID: 471
		private readonly bool writing;

		// Token: 0x040001D8 RID: 472
		private readonly bool enableMessageStreamDisposal;

		// Token: 0x040001D9 RID: 473
		private readonly long maxMessageSize;

		// Token: 0x040001DA RID: 474
		private bool? useBufferingReadStream;

		// Token: 0x040001DB RID: 475
		private BufferingReadStream bufferingReadStream;
	}
}
