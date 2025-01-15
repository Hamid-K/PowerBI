using System;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C9A RID: 7322
	public class ErrorTranslatingMessenger : IMessenger, IDisposable
	{
		// Token: 0x0600B617 RID: 46615 RVA: 0x0024F706 File Offset: 0x0024D906
		public ErrorTranslatingMessenger(IMessenger messenger, Func<Exception, Exception> translateException)
		{
			this.messenger = messenger;
			this.translateException = translateException;
		}

		// Token: 0x17002D75 RID: 11637
		// (get) Token: 0x0600B618 RID: 46616 RVA: 0x0024F71C File Offset: 0x0024D91C
		public IMessageSerializer Serializer
		{
			get
			{
				return this.messenger.Serializer;
			}
		}

		// Token: 0x17002D76 RID: 11638
		// (get) Token: 0x0600B619 RID: 46617 RVA: 0x0024F729 File Offset: 0x0024D929
		public IMessageHandlers Handlers
		{
			get
			{
				return this.messenger.Handlers;
			}
		}

		// Token: 0x0600B61A RID: 46618 RVA: 0x0024F736 File Offset: 0x0024D936
		public IMessageChannel CreateChannel()
		{
			return new ErrorTranslatingMessenger.MessageChannel(this, this.messenger.CreateChannel());
		}

		// Token: 0x0600B61B RID: 46619 RVA: 0x0024F749 File Offset: 0x0024D949
		public void Dispose()
		{
			this.messenger.Dispose();
		}

		// Token: 0x04005D04 RID: 23812
		private readonly IMessenger messenger;

		// Token: 0x04005D05 RID: 23813
		private readonly Func<Exception, Exception> translateException;

		// Token: 0x02001C9B RID: 7323
		private sealed class MessageChannel : IMessageChannel, IDisposable
		{
			// Token: 0x0600B61C RID: 46620 RVA: 0x0024F756 File Offset: 0x0024D956
			public MessageChannel(ErrorTranslatingMessenger messenger, IMessageChannel messageChannel)
			{
				this.messenger = messenger;
				this.messageChannel = messageChannel;
			}

			// Token: 0x17002D77 RID: 11639
			// (get) Token: 0x0600B61D RID: 46621 RVA: 0x0024F76C File Offset: 0x0024D96C
			public IMessenger Messenger
			{
				get
				{
					return this.messenger;
				}
			}

			// Token: 0x0600B61E RID: 46622 RVA: 0x0024F774 File Offset: 0x0024D974
			public void Post(Message message)
			{
				try
				{
					this.messageChannel.Post(message);
				}
				catch (MessageChannelException)
				{
					throw;
				}
				catch (Exception ex)
				{
					using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ErrorTranslatingMessenger/Post", null, TraceEventType.Information, null))
					{
						if (SafeExceptions.TraceIsSafeException(hostTrace, ex))
						{
							Exception ex2 = this.messenger.translateException(ex);
							if (ex2 != ex)
							{
								throw ex2;
							}
						}
						throw;
					}
				}
			}

			// Token: 0x0600B61F RID: 46623 RVA: 0x0024F7F8 File Offset: 0x0024D9F8
			public Message Read()
			{
				Message message;
				try
				{
					message = this.messageChannel.Read();
				}
				catch (MessageChannelException)
				{
					throw;
				}
				catch (Exception ex)
				{
					using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ErrorTranslatingMessenger/Read", null, TraceEventType.Information, null))
					{
						if (SafeExceptions.TraceIsSafeException(hostTrace, ex))
						{
							Exception ex2 = this.messenger.translateException(ex);
							if (ex2 != ex)
							{
								throw ex2;
							}
						}
						throw;
					}
				}
				return message;
			}

			// Token: 0x0600B620 RID: 46624 RVA: 0x0024F87C File Offset: 0x0024DA7C
			public void TakeOwnership()
			{
				this.messageChannel.TakeOwnership();
			}

			// Token: 0x0600B621 RID: 46625 RVA: 0x0024F889 File Offset: 0x0024DA89
			public void Dispose()
			{
				this.messageChannel.Dispose();
			}

			// Token: 0x04005D06 RID: 23814
			private readonly ErrorTranslatingMessenger messenger;

			// Token: 0x04005D07 RID: 23815
			private readonly IMessageChannel messageChannel;
		}
	}
}
