using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C45 RID: 7237
	public class ChannelMessenger : IMessenger, IDisposable
	{
		// Token: 0x0600B48E RID: 46222 RVA: 0x00249CC0 File Offset: 0x00247EC0
		public ChannelMessenger(IMessageHandlers baseHandlers, IMessageChannel baseChannel, bool evenSide)
		{
			this.handlers = new ChannelMessenger.ChannelMessageHandlers(baseHandlers);
			this.handlers.AddHandler<ChannelMessenger.MessageWithUnknownChannel>(new Action<IMessageChannel, ChannelMessenger.MessageWithUnknownChannel>(this.OnMessageWithUnknownChannel));
			this.baseChannel = baseChannel;
			this.channels = new Dictionary<long, ChannelMessenger.MessageChannel>();
			this.readerExitedEvent = new ManualResetEvent(true);
			this.disposedEvent = new ManualResetEvent(false);
			this.nextChannelId = ((!evenSide) ? 1L : 0L);
		}

		// Token: 0x17002D25 RID: 11557
		// (get) Token: 0x0600B48F RID: 46223 RVA: 0x00249D36 File Offset: 0x00247F36
		public IMessageSerializer Serializer
		{
			get
			{
				return this.baseChannel.Messenger.Serializer;
			}
		}

		// Token: 0x17002D26 RID: 11558
		// (get) Token: 0x0600B490 RID: 46224 RVA: 0x00249D48 File Offset: 0x00247F48
		public IMessageHandlers Handlers
		{
			get
			{
				return this.handlers;
			}
		}

		// Token: 0x0600B491 RID: 46225 RVA: 0x00249D50 File Offset: 0x00247F50
		public IMessageChannel CreateChannel()
		{
			object obj = this.syncRoot;
			IMessageChannel messageChannel;
			lock (obj)
			{
				if (this.nextChannelId > 9223372036854775805L)
				{
					throw new MessageChannelException("Channel IDs have been exhausted.");
				}
				long num = this.nextChannelId;
				this.nextChannelId += 2L;
				messageChannel = this.CreateChannel(num);
			}
			return messageChannel;
		}

		// Token: 0x0600B492 RID: 46226 RVA: 0x00249DC8 File Offset: 0x00247FC8
		public void Dispose()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.disposedEvent != null)
				{
					this.disposedEvent.Set();
					ChannelMessenger.MessageChannel[] array = this.channels.Values.ToArray<ChannelMessenger.MessageChannel>();
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Dispose();
					}
					this.handlers.RemoveHandler<ChannelMessenger.MessageWithUnknownChannel>();
					this.disposedEvent = null;
					this.channels = null;
					this.baseChannel = null;
					this.handlers = null;
				}
				if (this.haveReaderThread)
				{
					throw new MessageChannelException("ChannelMessenger was disposed with an active reader.");
				}
			}
		}

		// Token: 0x0600B493 RID: 46227 RVA: 0x00249E78 File Offset: 0x00248078
		private void Post(ChannelMessenger.MessageChannel channel, Message message)
		{
			WaitHandle waitHandle = null;
			object obj = this.syncRoot;
			lock (obj)
			{
				if (!channel.CanPost)
				{
					waitHandle = channel.ClearToPost;
				}
			}
			if (waitHandle != null)
			{
				waitHandle.WaitOne();
			}
			this.PostWithoutFlowControl(channel, message);
		}

		// Token: 0x0600B494 RID: 46228 RVA: 0x00249ED8 File Offset: 0x002480D8
		private void PostWithoutFlowControl(ChannelMessenger.MessageChannel channel, Message message)
		{
			message.Prepare();
			this.baseChannel.Post(new ChannelMessenger.MessageWithChannel
			{
				ChannelId = channel.ChannelId,
				Message = message
			});
		}

		// Token: 0x0600B495 RID: 46229 RVA: 0x00249F04 File Offset: 0x00248104
		private ChannelMessenger.MessageWithChannel Read(ChannelMessenger.MessageChannel channel)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (channel.ReadExceptionCtor != null)
				{
					throw channel.ReadExceptionCtor();
				}
				channel.ReadExceptionCtor = ChannelMessenger.readReenteredExceptionCtor;
			}
			bool flag2 = false;
			try
			{
				do
				{
					obj = this.syncRoot;
					lock (obj)
					{
						if (channel.HasMessage)
						{
							ChannelMessenger.MessageWithChannel messageWithChannel = channel.Messages.Dequeue();
							if (!channel.SenderCanPost && channel.Messages.Count <= 2)
							{
								this.SetSenderCanPost(channel, true);
							}
							return messageWithChannel;
						}
						if (!flag2)
						{
							if (!this.haveReaderThread)
							{
								this.haveReaderThread = true;
								flag2 = true;
								this.readerExitedEvent.Reset();
							}
							else
							{
								channel.MessageAvailable.Reset();
							}
						}
					}
					if (flag2)
					{
						Message message = this.baseChannel.Read();
						ChannelMessenger.MessageWithChannel messageWithChannel2 = message as ChannelMessenger.MessageWithChannel;
						if (messageWithChannel2 == null)
						{
							messageWithChannel2 = new ChannelMessenger.MessageWithChannel
							{
								ChannelId = -1L,
								Message = message
							};
						}
						obj = this.syncRoot;
						lock (obj)
						{
							ChannelMessenger.CanPostMessage canPostMessage = messageWithChannel2.Message as ChannelMessenger.CanPostMessage;
							if (canPostMessage != null)
							{
								ChannelMessenger.MessageChannel messageChannel;
								if (this.channels.TryGetValue(messageWithChannel2.ChannelId, out messageChannel))
								{
									if (canPostMessage.CanPost)
									{
										messageChannel.ClearToPost.Set();
									}
									else
									{
										messageChannel.ClearToPost.Reset();
									}
								}
								messageWithChannel2 = null;
							}
							else if (messageWithChannel2.ChannelId != channel.ChannelId && messageWithChannel2.ChannelId != -1L)
							{
								ChannelMessenger.MessageChannel messageChannel2;
								if (this.channels.TryGetValue(messageWithChannel2.ChannelId, out messageChannel2))
								{
									messageChannel2.Messages.Enqueue(messageWithChannel2);
									messageChannel2.MessageAvailable.Set();
									messageWithChannel2 = null;
									if (messageChannel2.Messages.Count >= 8 && messageChannel2.SenderCanPost)
									{
										this.SetSenderCanPost(messageChannel2, false);
									}
								}
								else
								{
									this.CreateChannel(messageWithChannel2.ChannelId);
								}
							}
							if (messageWithChannel2 != null)
							{
								return messageWithChannel2;
							}
							continue;
						}
					}
				}
				while (WaitHandle.WaitAny(new WaitHandle[] { this.disposedEvent, this.readerExitedEvent, channel.MessageAvailable }) != 0);
				throw new MessageChannelException("Message channel was disposed with an active reader.");
			}
			finally
			{
				obj = this.syncRoot;
				lock (obj)
				{
					if (flag2)
					{
						this.haveReaderThread = false;
						this.readerExitedEvent.Set();
					}
					if (channel.ReadExceptionCtor == ChannelMessenger.readReenteredExceptionCtor)
					{
						channel.ReadExceptionCtor = null;
					}
				}
			}
			ChannelMessenger.MessageWithChannel messageWithChannel3;
			return messageWithChannel3;
		}

		// Token: 0x0600B496 RID: 46230 RVA: 0x0024A208 File Offset: 0x00248408
		private ChannelMessenger.MessageChannel CreateChannel(long channelId)
		{
			ChannelMessenger.MessageChannel messageChannel = new ChannelMessenger.MessageChannel(this, channelId);
			this.channels.Add(channelId, messageChannel);
			return messageChannel;
		}

		// Token: 0x0600B497 RID: 46231 RVA: 0x0024A22C File Offset: 0x0024842C
		private ChannelMessenger.MessageChannel OpenChannel(long channelId)
		{
			object obj = this.syncRoot;
			ChannelMessenger.MessageChannel messageChannel;
			lock (obj)
			{
				messageChannel = this.channels[channelId];
			}
			return messageChannel;
		}

		// Token: 0x0600B498 RID: 46232 RVA: 0x0024A274 File Offset: 0x00248474
		private void DisposeChannel(ChannelMessenger.MessageChannel channel)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag2 = channel.ReadExceptionCtor == ChannelMessenger.readReenteredExceptionCtor;
				channel.ReadExceptionCtor = () => new ObjectDisposedException(base.GetType().FullName);
				channel.DisposeEvents();
				this.channels.Remove(channel.ChannelId);
				if (flag2)
				{
					throw new MessageChannelException("Message channel was disposed with an active reader.");
				}
				if (channel.HasMessage)
				{
					throw new MessageChannelException("Message channel was disposed while containing unread messages.");
				}
			}
		}

		// Token: 0x0600B499 RID: 46233 RVA: 0x0024A308 File Offset: 0x00248508
		private void SetSenderCanPost(ChannelMessenger.MessageChannel channel, bool senderCanPost)
		{
			channel.SenderCanPost = senderCanPost;
			this.PostWithoutFlowControl(channel, new ChannelMessenger.CanPostMessage
			{
				CanPost = senderCanPost
			});
		}

		// Token: 0x0600B49A RID: 46234 RVA: 0x0024A324 File Offset: 0x00248524
		private void OnMessageWithUnknownChannel(IMessageChannel baseChannel, ChannelMessenger.MessageWithUnknownChannel messageWithUnknownChannel)
		{
			ChannelMessenger.MessageWithChannel message = messageWithUnknownChannel.Message;
			if (message.ChannelId != -1L)
			{
				ChannelMessenger.MessageChannel messageChannel = this.OpenChannel(message.ChannelId);
				try
				{
					this.handlers.Dispatch(messageChannel, message.Message);
					if (messageChannel.OwnershipTaken)
					{
						messageChannel = null;
					}
					return;
				}
				finally
				{
					if (messageChannel != null)
					{
						messageChannel.Dispose();
					}
				}
			}
			this.handlers.Dispatch(baseChannel, message.Message);
		}

		// Token: 0x04005BCA RID: 23498
		private const int stopSendingThreshold = 8;

		// Token: 0x04005BCB RID: 23499
		private const int startSendingThreshold = 2;

		// Token: 0x04005BCC RID: 23500
		private const long noChannelId = -1L;

		// Token: 0x04005BCD RID: 23501
		private static readonly Func<Exception> readReenteredExceptionCtor = () => new MessageChannelException("ChannelMessenger.Read was re-entered.");

		// Token: 0x04005BCE RID: 23502
		private readonly object syncRoot = new object();

		// Token: 0x04005BCF RID: 23503
		private ChannelMessenger.ChannelMessageHandlers handlers;

		// Token: 0x04005BD0 RID: 23504
		private IMessageChannel baseChannel;

		// Token: 0x04005BD1 RID: 23505
		private Dictionary<long, ChannelMessenger.MessageChannel> channels;

		// Token: 0x04005BD2 RID: 23506
		private ManualResetEvent readerExitedEvent;

		// Token: 0x04005BD3 RID: 23507
		private ManualResetEvent disposedEvent;

		// Token: 0x04005BD4 RID: 23508
		private long nextChannelId;

		// Token: 0x04005BD5 RID: 23509
		private bool haveReaderThread;

		// Token: 0x02001C46 RID: 7238
		private class MessageChannel : IMessageChannel, IDisposable
		{
			// Token: 0x0600B49D RID: 46237 RVA: 0x0024A3C5 File Offset: 0x002485C5
			public MessageChannel(ChannelMessenger messenger, long channelId)
			{
				this.messenger = messenger;
				this.channelId = channelId;
				this.syncRoot = this.messenger.syncRoot;
				this.senderCanPost = true;
			}

			// Token: 0x17002D27 RID: 11559
			// (get) Token: 0x0600B49E RID: 46238 RVA: 0x0024A3F4 File Offset: 0x002485F4
			public bool OwnershipTaken
			{
				get
				{
					object obj = this.syncRoot;
					bool flag2;
					lock (obj)
					{
						flag2 = this.ownershipTaken;
					}
					return flag2;
				}
			}

			// Token: 0x17002D28 RID: 11560
			// (get) Token: 0x0600B49F RID: 46239 RVA: 0x0024A438 File Offset: 0x00248638
			public IMessenger Messenger
			{
				get
				{
					return this.messenger;
				}
			}

			// Token: 0x0600B4A0 RID: 46240 RVA: 0x0024A440 File Offset: 0x00248640
			public void Post(Message message)
			{
				this.messenger.Post(this, message);
			}

			// Token: 0x0600B4A1 RID: 46241 RVA: 0x0024A450 File Offset: 0x00248650
			public Message Read()
			{
				ChannelMessenger.MessageWithChannel messageWithChannel = this.messenger.Read(this);
				if (messageWithChannel.ChannelId == this.channelId)
				{
					return messageWithChannel.Message;
				}
				return new ChannelMessenger.MessageWithUnknownChannel
				{
					Message = messageWithChannel
				};
			}

			// Token: 0x0600B4A2 RID: 46242 RVA: 0x0024A48C File Offset: 0x0024868C
			public void TakeOwnership()
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.ownershipTaken = true;
				}
			}

			// Token: 0x0600B4A3 RID: 46243 RVA: 0x0024A4D0 File Offset: 0x002486D0
			public void Dispose()
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.messenger != null)
					{
						this.messenger.DisposeChannel(this);
						this.messenger = null;
					}
				}
			}

			// Token: 0x17002D29 RID: 11561
			// (get) Token: 0x0600B4A4 RID: 46244 RVA: 0x0024A528 File Offset: 0x00248728
			public long ChannelId
			{
				get
				{
					return this.channelId;
				}
			}

			// Token: 0x17002D2A RID: 11562
			// (get) Token: 0x0600B4A5 RID: 46245 RVA: 0x0024A530 File Offset: 0x00248730
			// (set) Token: 0x0600B4A6 RID: 46246 RVA: 0x0024A538 File Offset: 0x00248738
			public Func<Exception> ReadExceptionCtor
			{
				get
				{
					return this.readExceptionCtor;
				}
				set
				{
					this.readExceptionCtor = value;
				}
			}

			// Token: 0x17002D2B RID: 11563
			// (get) Token: 0x0600B4A7 RID: 46247 RVA: 0x0024A541 File Offset: 0x00248741
			public bool HasMessage
			{
				get
				{
					return this.messages != null && this.messages.Count > 0;
				}
			}

			// Token: 0x17002D2C RID: 11564
			// (get) Token: 0x0600B4A8 RID: 46248 RVA: 0x0024A55B File Offset: 0x0024875B
			public ManualResetEvent MessageAvailable
			{
				get
				{
					if (this.messageAvailable == null)
					{
						this.messageAvailable = new ManualResetEvent(false);
					}
					return this.messageAvailable;
				}
			}

			// Token: 0x17002D2D RID: 11565
			// (get) Token: 0x0600B4A9 RID: 46249 RVA: 0x0024A577 File Offset: 0x00248777
			public Queue<ChannelMessenger.MessageWithChannel> Messages
			{
				get
				{
					if (this.messages == null)
					{
						this.messages = new Queue<ChannelMessenger.MessageWithChannel>();
					}
					return this.messages;
				}
			}

			// Token: 0x17002D2E RID: 11566
			// (get) Token: 0x0600B4AA RID: 46250 RVA: 0x0024A592 File Offset: 0x00248792
			public bool CanPost
			{
				get
				{
					return this.clearToPost == null || this.clearToPost.WaitOne(0);
				}
			}

			// Token: 0x17002D2F RID: 11567
			// (get) Token: 0x0600B4AB RID: 46251 RVA: 0x0024A5AA File Offset: 0x002487AA
			public ManualResetEvent ClearToPost
			{
				get
				{
					if (this.clearToPost == null)
					{
						this.clearToPost = new ManualResetEvent(false);
					}
					return this.clearToPost;
				}
			}

			// Token: 0x17002D30 RID: 11568
			// (get) Token: 0x0600B4AC RID: 46252 RVA: 0x0024A5C6 File Offset: 0x002487C6
			// (set) Token: 0x0600B4AD RID: 46253 RVA: 0x0024A5CE File Offset: 0x002487CE
			public bool SenderCanPost
			{
				get
				{
					return this.senderCanPost;
				}
				set
				{
					this.senderCanPost = value;
				}
			}

			// Token: 0x0600B4AE RID: 46254 RVA: 0x0024A5D7 File Offset: 0x002487D7
			public void DisposeEvents()
			{
				if (this.messageAvailable != null)
				{
					this.messageAvailable.Close();
				}
				if (this.clearToPost != null)
				{
					this.clearToPost.Close();
				}
			}

			// Token: 0x04005BD6 RID: 23510
			private readonly object syncRoot;

			// Token: 0x04005BD7 RID: 23511
			private readonly long channelId;

			// Token: 0x04005BD8 RID: 23512
			private ChannelMessenger messenger;

			// Token: 0x04005BD9 RID: 23513
			private Func<Exception> readExceptionCtor;

			// Token: 0x04005BDA RID: 23514
			private Queue<ChannelMessenger.MessageWithChannel> messages;

			// Token: 0x04005BDB RID: 23515
			private ManualResetEvent messageAvailable;

			// Token: 0x04005BDC RID: 23516
			private ManualResetEvent clearToPost;

			// Token: 0x04005BDD RID: 23517
			private bool senderCanPost;

			// Token: 0x04005BDE RID: 23518
			private bool ownershipTaken;
		}

		// Token: 0x02001C47 RID: 7239
		private class ChannelMessageHandlers : MessageHandlers
		{
			// Token: 0x0600B4AF RID: 46255 RVA: 0x0024A5FF File Offset: 0x002487FF
			public ChannelMessageHandlers(IMessageHandlers baseHandlers)
			{
				this.baseHandlers = baseHandlers;
			}

			// Token: 0x0600B4B0 RID: 46256 RVA: 0x0024A60E File Offset: 0x0024880E
			protected override bool TryDispatch(IMessageChannel channel, Message message)
			{
				if (base.TryDispatch(channel, message))
				{
					return true;
				}
				this.baseHandlers.Dispatch(channel, message);
				return true;
			}

			// Token: 0x04005BDF RID: 23519
			private readonly IMessageHandlers baseHandlers;
		}

		// Token: 0x02001C48 RID: 7240
		private sealed class MessageWithChannel : UnbufferedMessage
		{
			// Token: 0x17002D31 RID: 11569
			// (get) Token: 0x0600B4B1 RID: 46257 RVA: 0x0024A62A File Offset: 0x0024882A
			// (set) Token: 0x0600B4B2 RID: 46258 RVA: 0x0024A632 File Offset: 0x00248832
			public long ChannelId { get; set; }

			// Token: 0x17002D32 RID: 11570
			// (get) Token: 0x0600B4B3 RID: 46259 RVA: 0x0024A63B File Offset: 0x0024883B
			// (set) Token: 0x0600B4B4 RID: 46260 RVA: 0x0024A643 File Offset: 0x00248843
			public Message Message { get; set; }

			// Token: 0x0600B4B5 RID: 46261 RVA: 0x0024A64C File Offset: 0x0024884C
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt64(this.ChannelId);
				base.Serializer.Serialize(writer, this.Message);
			}

			// Token: 0x0600B4B6 RID: 46262 RVA: 0x0024A66C File Offset: 0x0024886C
			public override void Deserialize(BinaryReader reader)
			{
				this.ChannelId = reader.ReadInt64();
				this.Message = base.Serializer.Deserialize(reader);
			}
		}

		// Token: 0x02001C49 RID: 7241
		private sealed class MessageWithUnknownChannel : UnbufferedMessage
		{
			// Token: 0x17002D33 RID: 11571
			// (get) Token: 0x0600B4B8 RID: 46264 RVA: 0x0024A694 File Offset: 0x00248894
			// (set) Token: 0x0600B4B9 RID: 46265 RVA: 0x0024A69C File Offset: 0x0024889C
			public ChannelMessenger.MessageWithChannel Message { get; set; }

			// Token: 0x0600B4BA RID: 46266 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override void Serialize(BinaryWriter writer)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600B4BB RID: 46267 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override void Deserialize(BinaryReader reader)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02001C4A RID: 7242
		private sealed class CanPostMessage : UnbufferedMessage
		{
			// Token: 0x17002D34 RID: 11572
			// (get) Token: 0x0600B4BD RID: 46269 RVA: 0x0024A6A5 File Offset: 0x002488A5
			// (set) Token: 0x0600B4BE RID: 46270 RVA: 0x0024A6AD File Offset: 0x002488AD
			public bool CanPost { get; set; }

			// Token: 0x0600B4BF RID: 46271 RVA: 0x0024A6B6 File Offset: 0x002488B6
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteBool(this.CanPost);
			}

			// Token: 0x0600B4C0 RID: 46272 RVA: 0x0024A6C4 File Offset: 0x002488C4
			public override void Deserialize(BinaryReader reader)
			{
				this.CanPost = reader.ReadBool();
			}
		}
	}
}
