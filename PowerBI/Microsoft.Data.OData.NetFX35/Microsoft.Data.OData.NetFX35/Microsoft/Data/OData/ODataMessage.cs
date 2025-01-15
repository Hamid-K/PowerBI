using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x0200025F RID: 607
	internal abstract class ODataMessage
	{
		// Token: 0x060012EA RID: 4842 RVA: 0x00047416 File Offset: 0x00045616
		protected ODataMessage(bool writing, bool disableMessageStreamDisposal, long maxMessageSize)
		{
			this.writing = writing;
			this.disableMessageStreamDisposal = disableMessageStreamDisposal;
			this.maxMessageSize = maxMessageSize;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060012EB RID: 4843
		public abstract IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00047433 File Offset: 0x00045633
		protected internal BufferingReadStream BufferingReadStream
		{
			get
			{
				return this.bufferingReadStream;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x0004743B File Offset: 0x0004563B
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x00047443 File Offset: 0x00045643
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

		// Token: 0x060012EF RID: 4847
		public abstract string GetHeader(string headerName);

		// Token: 0x060012F0 RID: 4848
		public abstract void SetHeader(string headerName, string headerValue);

		// Token: 0x060012F1 RID: 4849
		public abstract Stream GetStream();

		// Token: 0x060012F2 RID: 4850
		internal abstract TInterface QueryInterface<TInterface>() where TInterface : class;

		// Token: 0x060012F3 RID: 4851 RVA: 0x0004744C File Offset: 0x0004564C
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

		// Token: 0x060012F4 RID: 4852 RVA: 0x00047504 File Offset: 0x00045704
		protected void VerifyCanSetHeader()
		{
			if (!this.writing)
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0004751C File Offset: 0x0004571C
		private static void ValidateMessageStream(Stream stream, bool isRequest)
		{
			if (stream == null)
			{
				string text = (isRequest ? Strings.ODataRequestMessage_MessageStreamIsNull : Strings.ODataResponseMessage_MessageStreamIsNull);
				throw new ODataException(text);
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00047544 File Offset: 0x00045744
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

		// Token: 0x04000713 RID: 1811
		private readonly bool writing;

		// Token: 0x04000714 RID: 1812
		private readonly bool disableMessageStreamDisposal;

		// Token: 0x04000715 RID: 1813
		private readonly long maxMessageSize;

		// Token: 0x04000716 RID: 1814
		private bool? useBufferingReadStream;

		// Token: 0x04000717 RID: 1815
		private BufferingReadStream bufferingReadStream;
	}
}
