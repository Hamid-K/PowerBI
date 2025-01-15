using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CF2 RID: 7410
	internal class MessageBasedInputStream : ChunkedInputStream
	{
		// Token: 0x0600B902 RID: 47362 RVA: 0x00258072 File Offset: 0x00256272
		public MessageBasedInputStream(IMessageChannel channel, ExceptionHandler exceptionHandler)
		{
			this.channel = channel;
			this.exceptionHandler = exceptionHandler;
		}

		// Token: 0x17002DC6 RID: 11718
		// (get) Token: 0x0600B903 RID: 47363 RVA: 0x00258088 File Offset: 0x00256288
		public bool WriterWasClosed
		{
			get
			{
				return this.writerWasClosed;
			}
		}

		// Token: 0x0600B904 RID: 47364 RVA: 0x00258090 File Offset: 0x00256290
		protected override byte[] ReadNextChunk()
		{
			if (this.channel == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.WriterWasClosed)
			{
				return new byte[0];
			}
			byte[] array;
			try
			{
				if (this.exception != null)
				{
					throw this.exception.ToCallbackException();
				}
				array = this.ReadNextChunkAndCheckIfClosed();
			}
			catch (Exception ex)
			{
				this.exception = this.exception ?? ex;
				this.exceptionHandler(ex, false);
				throw;
			}
			return array;
		}

		// Token: 0x0600B905 RID: 47365 RVA: 0x00258118 File Offset: 0x00256318
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.channel != null)
			{
				try
				{
					while (!this.WriterWasClosed)
					{
						try
						{
							this.ReadNextChunkAndCheckIfClosed();
						}
						catch (Exception ex)
						{
							if (!this.exceptionHandler(ex, true))
							{
								throw;
							}
						}
					}
				}
				finally
				{
					this.channel = null;
				}
			}
		}

		// Token: 0x0600B906 RID: 47366 RVA: 0x00258188 File Offset: 0x00256388
		private byte[] ReadNextChunkAndCheckIfClosed()
		{
			byte[] array;
			try
			{
				byte[] chunk = this.channel.WaitFor<MessageBasedOutputStream.BinaryChunkMessage>().Chunk;
				if (chunk.Length == 0)
				{
					this.writerWasClosed = true;
				}
				array = chunk;
			}
			catch (MessageChannelException)
			{
				this.writerWasClosed = true;
				throw;
			}
			return array;
		}

		// Token: 0x04005E2E RID: 24110
		private IMessageChannel channel;

		// Token: 0x04005E2F RID: 24111
		private ExceptionHandler exceptionHandler;

		// Token: 0x04005E30 RID: 24112
		private Exception exception;

		// Token: 0x04005E31 RID: 24113
		private bool writerWasClosed;
	}
}
