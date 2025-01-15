using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000093 RID: 147
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This class does not own the BufferingReadStream instance.")]
	internal abstract class ODataMessage
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x0000CB32 File Offset: 0x0000AD32
		protected ODataMessage(bool writing, bool enableMessageStreamDisposal, long maxMessageSize)
		{
			this.writing = writing;
			this.enableMessageStreamDisposal = enableMessageStreamDisposal;
			this.maxMessageSize = maxMessageSize;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600053B RID: 1339
		public abstract IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000CB4F File Offset: 0x0000AD4F
		protected internal BufferingReadStream BufferingReadStream
		{
			get
			{
				return this.bufferingReadStream;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000CB57 File Offset: 0x0000AD57
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0000CB5F File Offset: 0x0000AD5F
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

		// Token: 0x0600053F RID: 1343
		public abstract string GetHeader(string headerName);

		// Token: 0x06000540 RID: 1344
		public abstract void SetHeader(string headerName, string headerValue);

		// Token: 0x06000541 RID: 1345
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Intentionally a method.")]
		public abstract Stream GetStream();

		// Token: 0x06000542 RID: 1346
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Intentionally a method.")]
		public abstract Task<Stream> GetStreamAsync();

		// Token: 0x06000543 RID: 1347
		internal abstract TInterface QueryInterface<TInterface>() where TInterface : class;

		// Token: 0x06000544 RID: 1348 RVA: 0x0000CB68 File Offset: 0x0000AD68
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
			Stream stream = messageStreamFunc();
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

		// Token: 0x06000545 RID: 1349 RVA: 0x0000CC28 File Offset: 0x0000AE28
		protected internal Task<Stream> GetStreamAsync(Func<Task<Stream>> streamFuncAsync, bool isRequest)
		{
			if (!this.writing)
			{
				Stream stream = this.TryGetBufferingReadStream();
				if (stream != null)
				{
					return TaskUtils.GetCompletedTask<Stream>(stream);
				}
			}
			Task<Stream> task = streamFuncAsync();
			ODataMessage.ValidateMessageStreamTask(task, isRequest);
			task = task.FollowOnSuccessWith(delegate(Task<Stream> streamTask)
			{
				Stream stream2 = streamTask.Result;
				ODataMessage.ValidateMessageStream(stream2, isRequest);
				bool flag = !this.writing && this.maxMessageSize > 0L;
				if (!this.enableMessageStreamDisposal && flag)
				{
					stream2 = MessageStreamWrapper.CreateNonDisposingStreamWithMaxSize(stream2, this.maxMessageSize);
				}
				else if (!this.enableMessageStreamDisposal)
				{
					stream2 = MessageStreamWrapper.CreateNonDisposingStream(stream2);
				}
				else if (flag)
				{
					stream2 = MessageStreamWrapper.CreateStreamWithMaxSize(stream2, this.maxMessageSize);
				}
				return stream2;
			});
			if (!this.writing)
			{
				task = task.FollowOnSuccessWithTask((Task<Stream> streamTask) => BufferedReadStream.BufferStreamAsync(streamTask.Result)).FollowOnSuccessWith((Task<BufferedReadStream> streamTask) => streamTask.Result);
				if (this.useBufferingReadStream == true)
				{
					task = task.FollowOnSuccessWith(delegate(Task<Stream> streamTask)
					{
						Stream result = streamTask.Result;
						this.bufferingReadStream = new BufferingReadStream(result);
						return this.bufferingReadStream;
					});
				}
			}
			return task;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000CD0F File Offset: 0x0000AF0F
		protected void VerifyCanSetHeader()
		{
			if (!this.writing)
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000CD24 File Offset: 0x0000AF24
		private static void ValidateMessageStream(Stream stream, bool isRequest)
		{
			if (stream == null)
			{
				string text = (isRequest ? Strings.ODataRequestMessage_MessageStreamIsNull : Strings.ODataResponseMessage_MessageStreamIsNull);
				throw new ODataException(text);
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		private static void ValidateMessageStreamTask(Task<Stream> streamTask, bool isRequest)
		{
			if (streamTask == null)
			{
				string text = (isRequest ? Strings.ODataRequestMessage_StreamTaskIsNull : Strings.ODataResponseMessage_StreamTaskIsNull);
				throw new ODataException(text);
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000CD74 File Offset: 0x0000AF74
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

		// Token: 0x04000238 RID: 568
		private readonly bool writing;

		// Token: 0x04000239 RID: 569
		private readonly bool enableMessageStreamDisposal;

		// Token: 0x0400023A RID: 570
		private readonly long maxMessageSize;

		// Token: 0x0400023B RID: 571
		private bool? useBufferingReadStream;

		// Token: 0x0400023C RID: 572
		private BufferingReadStream bufferingReadStream;
	}
}
