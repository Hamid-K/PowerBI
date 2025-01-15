using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D71 RID: 7537
	public class StreamMessenger : IMessenger, IDisposable
	{
		// Token: 0x0600BB3B RID: 47931 RVA: 0x0025EAA8 File Offset: 0x0025CCA8
		public StreamMessenger()
		{
			this.channelLock = new object();
			this.readLock = new object();
			this.postLock = new object();
			this.fallbackEncoding = (Encoding)Encoding.UTF8.Clone();
			this.fallbackEncoding.EncoderFallback = new EncoderReplacementFallback("\ufffd");
			this.serializer = new MessageSerializer();
			this.handlers = new MessageHandlers();
		}

		// Token: 0x17002E39 RID: 11833
		// (set) Token: 0x0600BB3C RID: 47932 RVA: 0x0025EB1C File Offset: 0x0025CD1C
		public Stream InputStream
		{
			set
			{
				this.reader = new BinaryReader(value);
			}
		}

		// Token: 0x17002E3A RID: 11834
		// (set) Token: 0x0600BB3D RID: 47933 RVA: 0x0025EB2A File Offset: 0x0025CD2A
		public Stream OutputStream
		{
			set
			{
				this.writer = new BinaryWriter(value, this.fallbackEncoding);
			}
		}

		// Token: 0x17002E3B RID: 11835
		// (get) Token: 0x0600BB3E RID: 47934 RVA: 0x0025EB3E File Offset: 0x0025CD3E
		public IMessageSerializer Serializer
		{
			get
			{
				return this.serializer;
			}
		}

		// Token: 0x17002E3C RID: 11836
		// (get) Token: 0x0600BB3F RID: 47935 RVA: 0x0025EB46 File Offset: 0x0025CD46
		public IMessageHandlers Handlers
		{
			get
			{
				return this.handlers;
			}
		}

		// Token: 0x0600BB40 RID: 47936 RVA: 0x0025EB50 File Offset: 0x0025CD50
		public IMessageChannel CreateChannel()
		{
			object obj = this.channelLock;
			IMessageChannel messageChannel;
			lock (obj)
			{
				if (this.channel != null)
				{
					throw new InvalidOperationException("Only a single message channel is supported.");
				}
				this.channel = new StreamMessenger.MessageChannel(this);
				messageChannel = this.channel;
			}
			return messageChannel;
		}

		// Token: 0x0600BB41 RID: 47937 RVA: 0x0025EBB4 File Offset: 0x0025CDB4
		private void DisposeChannel()
		{
			object obj = this.channelLock;
			lock (obj)
			{
				this.channel = null;
			}
		}

		// Token: 0x0600BB42 RID: 47938 RVA: 0x0025EBF8 File Offset: 0x0025CDF8
		private void Post(Message message)
		{
			message.Prepare();
			object obj = this.postLock;
			lock (obj)
			{
				if (this.postExceptionCtor != null)
				{
					throw this.postExceptionCtor();
				}
				this.postExceptionCtor = StreamMessenger.postReenteredExceptionCtor;
				try
				{
					this.serializer.Serialize(this.writer, message);
					this.writer.Flush();
				}
				catch (Exception ex)
				{
					using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("StreamMessenger/Post", null, TraceEventType.Information, null))
					{
						this.postExceptionCtor = StreamMessenger.channelExceptionCtor;
						SafeExceptions.TraceIsSafeException(hostTrace, ex);
						throw;
					}
				}
				finally
				{
					if (this.postExceptionCtor == StreamMessenger.postReenteredExceptionCtor)
					{
						this.postExceptionCtor = null;
					}
				}
			}
		}

		// Token: 0x0600BB43 RID: 47939 RVA: 0x0025ECE0 File Offset: 0x0025CEE0
		private Message Read()
		{
			object obj = this.readLock;
			lock (obj)
			{
				if (this.readExceptionCtor != null)
				{
					throw this.readExceptionCtor();
				}
				this.readExceptionCtor = StreamMessenger.readReenteredExceptionCtor;
			}
			Message message;
			try
			{
				message = this.serializer.Deserialize(this.reader);
			}
			catch (Exception ex)
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("StreamMessenger/Read", null, TraceEventType.Information, null))
				{
					obj = this.readLock;
					lock (obj)
					{
						this.readExceptionCtor = StreamMessenger.channelExceptionCtor;
					}
					SafeExceptions.TraceIsSafeException(hostTrace, ex);
					throw;
				}
			}
			finally
			{
				obj = this.readLock;
				lock (obj)
				{
					if (this.readExceptionCtor == StreamMessenger.readReenteredExceptionCtor)
					{
						this.readExceptionCtor = null;
					}
				}
			}
			return message;
		}

		// Token: 0x0600BB44 RID: 47940 RVA: 0x0025EE14 File Offset: 0x0025D014
		public void Dispose()
		{
			object obj = this.readLock;
			lock (obj)
			{
				if (this.readExceptionCtor == StreamMessenger.readReenteredExceptionCtor)
				{
					throw new InvalidOperationException("StreamMessenger was disposed with an active reader.");
				}
			}
			obj = this.channelLock;
			lock (obj)
			{
				this.reader = null;
				this.writer = null;
				this.channel = null;
			}
		}

		// Token: 0x04005F53 RID: 24403
		private static readonly Func<Exception> readReenteredExceptionCtor = () => new InvalidOperationException("StreamMessenger.Read was re-entered.");

		// Token: 0x04005F54 RID: 24404
		private static readonly Func<Exception> postReenteredExceptionCtor = () => new InvalidOperationException("StreamMessenger.Post was re-entered.");

		// Token: 0x04005F55 RID: 24405
		private static readonly Func<Exception> channelExceptionCtor = () => new MessageChannelException("Error reading from or writing to stream.");

		// Token: 0x04005F56 RID: 24406
		private readonly object channelLock;

		// Token: 0x04005F57 RID: 24407
		private readonly object readLock;

		// Token: 0x04005F58 RID: 24408
		private readonly object postLock;

		// Token: 0x04005F59 RID: 24409
		private readonly Encoding fallbackEncoding;

		// Token: 0x04005F5A RID: 24410
		private readonly MessageSerializer serializer;

		// Token: 0x04005F5B RID: 24411
		private readonly MessageHandlers handlers;

		// Token: 0x04005F5C RID: 24412
		private BinaryReader reader;

		// Token: 0x04005F5D RID: 24413
		private BinaryWriter writer;

		// Token: 0x04005F5E RID: 24414
		private Func<Exception> readExceptionCtor;

		// Token: 0x04005F5F RID: 24415
		private Func<Exception> postExceptionCtor;

		// Token: 0x04005F60 RID: 24416
		private StreamMessenger.MessageChannel channel;

		// Token: 0x02001D72 RID: 7538
		private sealed class MessageChannel : IMessageChannel, IDisposable
		{
			// Token: 0x0600BB46 RID: 47942 RVA: 0x0025EEF8 File Offset: 0x0025D0F8
			public MessageChannel(StreamMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x17002E3D RID: 11837
			// (get) Token: 0x0600BB47 RID: 47943 RVA: 0x0025EF07 File Offset: 0x0025D107
			public IMessenger Messenger
			{
				get
				{
					return this.messenger;
				}
			}

			// Token: 0x0600BB48 RID: 47944 RVA: 0x0025EF0F File Offset: 0x0025D10F
			public void Post(Message message)
			{
				this.messenger.Post(message);
			}

			// Token: 0x0600BB49 RID: 47945 RVA: 0x0025EF1D File Offset: 0x0025D11D
			public Message Read()
			{
				return this.messenger.Read();
			}

			// Token: 0x0600BB4A RID: 47946 RVA: 0x0025EF2A File Offset: 0x0025D12A
			public void TakeOwnership()
			{
				throw new InvalidOperationException("Only a single message channel is supported.");
			}

			// Token: 0x0600BB4B RID: 47947 RVA: 0x0025EF36 File Offset: 0x0025D136
			public void Dispose()
			{
				if (this.messenger != null)
				{
					this.messenger.DisposeChannel();
					this.messenger = null;
				}
			}

			// Token: 0x04005F61 RID: 24417
			private StreamMessenger messenger;
		}
	}
}
