using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000929 RID: 2345
	public abstract class MqOperation<T> : IDisposable
	{
		// Token: 0x060042CD RID: 17101 RVA: 0x000E126D File Offset: 0x000DF46D
		protected MqOperation(IMqConfig<T> config)
		{
			this.config = config;
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x000E127C File Offset: 0x000DF47C
		protected QueueManager QueueManager
		{
			get
			{
				if (this.queueManager == null)
				{
					QueueManager.Pooling.Pool = false;
					this.queueManager = QueueManager.New(this.config.ConnectionParameters);
				}
				return this.queueManager;
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x000E12AD File Offset: 0x000DF4AD
		// (set) Token: 0x060042D0 RID: 17104 RVA: 0x000E12B5 File Offset: 0x000DF4B5
		protected Queue Queue { get; set; }

		// Token: 0x060042D1 RID: 17105 RVA: 0x000E12C0 File Offset: 0x000DF4C0
		protected void Initialize()
		{
			try
			{
				this.QueueManager.Connect();
				this.InitializeOperation();
			}
			catch (MqException ex)
			{
				this.faulted = true;
				throw this.config.WrapException(ex, true);
			}
		}

		// Token: 0x060042D2 RID: 17106
		protected abstract void InitializeOperation();

		// Token: 0x060042D3 RID: 17107
		public abstract IEnumerable<T> Execute();

		// Token: 0x060042D4 RID: 17108 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void DisposeInternal()
		{
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000E1308 File Offset: 0x000DF508
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.DisposeInternal();
			if (this.Queue != null)
			{
				this.Queue.Close();
			}
			if (this.QueueManager != null)
			{
				this.QueueManager.Disconnect();
			}
			this.disposed = true;
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000E1346 File Offset: 0x000DF546
		public static MqOperation<T> NewBrowseOperation(IMqConfig<T> config)
		{
			return new MqOperation<T>.MqGetOperation.BrowseOperation(config);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x000E134E File Offset: 0x000DF54E
		public static MqOperation<T> NewGetOperation(IMqConfig<T> config)
		{
			return new MqOperation<T>.MqGetOperation.GetOperation(config);
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x000E1356 File Offset: 0x000DF556
		internal static MqOperation<T> NewClearOperation(IMqConfig<T> config)
		{
			return new MqOperation<T>.MqGetOperation.ClearOperation(config);
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x000E135E File Offset: 0x000DF55E
		public static MqOperation<T> NewPutOperation(IMqConfig<T> config, IEnumerable<SendMessage> messages)
		{
			return new MqOperation<T>.MqPutOperation(config, messages);
		}

		// Token: 0x04002324 RID: 8996
		private QueueManager queueManager;

		// Token: 0x04002325 RID: 8997
		private bool disposed;

		// Token: 0x04002326 RID: 8998
		protected bool faulted;

		// Token: 0x04002327 RID: 8999
		protected readonly IMqConfig<T> config;

		// Token: 0x0200092A RID: 2346
		private abstract class MqGetOperation : MqOperation<T>
		{
			// Token: 0x060042DA RID: 17114 RVA: 0x000E1367 File Offset: 0x000DF567
			private MqGetOperation(IMqConfig<T> config)
				: base(config)
			{
			}

			// Token: 0x17001549 RID: 5449
			// (get) Token: 0x060042DB RID: 17115
			protected abstract bool LockMessages { get; }

			// Token: 0x1700154A RID: 5450
			// (get) Token: 0x060042DC RID: 17116 RVA: 0x00002139 File Offset: 0x00000339
			protected virtual bool BrowseMessages
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060042DD RID: 17117 RVA: 0x000E1378 File Offset: 0x000DF578
			private ReceiveOptions GetReceiveOptions(int timeout = 0)
			{
				bool flag = true;
				ReceiveOptions receiveOptions = new ReceiveOptions();
				if (this.BrowseMessages)
				{
					receiveOptions.Options |= ReceiveOption.BrowseNext;
				}
				if (this.LockMessages)
				{
					receiveOptions.Options |= ReceiveOption.Lock;
				}
				if (!this.BrowseMessages && !this.LockMessages)
				{
					receiveOptions.TruncationSize = 0;
					flag = false;
				}
				if (this.config.Filters != null)
				{
					foreach (KeyValuePair<MqColumn, object> keyValuePair in this.config.Filters)
					{
						MqColumn key = keyValuePair.Key;
						if (key <= MqColumn.CorrelationId)
						{
							if (key != MqColumn.MessageId)
							{
								if (key == MqColumn.CorrelationId)
								{
									receiveOptions.Correlator = (byte[])keyValuePair.Value;
									receiveOptions.MatchOptions |= MatchOption.Correlator;
								}
							}
							else
							{
								receiveOptions.MessageId = (byte[])keyValuePair.Value;
								receiveOptions.MatchOptions |= MatchOption.MessageId;
							}
						}
						else
						{
							switch (key)
							{
							case MqColumn.GroupId:
								receiveOptions.GroupId = (byte[])keyValuePair.Value;
								receiveOptions.MatchOptions |= MatchOption.GroupId;
								break;
							case MqColumn.LogicalSequenceNumber:
								receiveOptions.SequenceNumber = (int)keyValuePair.Value;
								receiveOptions.MatchOptions |= MatchOption.SequenceNumber;
								break;
							case MqColumn.MessageType:
								break;
							case MqColumn.Offset:
								receiveOptions.Offset = (int)keyValuePair.Value;
								receiveOptions.MatchOptions |= MatchOption.Offset;
								break;
							default:
								if (key == MqColumn.MessageToken)
								{
									flag = false;
									receiveOptions.Token = (byte[])keyValuePair.Value;
									receiveOptions.MatchOptions |= MatchOption.Token;
								}
								break;
							}
						}
					}
				}
				if (this.receiveOptions == null && flag)
				{
					receiveOptions.Timeout = timeout;
					receiveOptions.Wait = timeout > 0;
				}
				return receiveOptions;
			}

			// Token: 0x060042DE RID: 17118 RVA: 0x000E1570 File Offset: 0x000DF770
			private Queue OpenQueueForReceive()
			{
				Queue queue = new Queue(this.config.ConnectionParameters.Queue, base.QueueManager);
				queue.Options |= OpenOption.Shared | OpenOption.Browse | OpenOption.FailOnQuiesce;
				queue.OpenForReceive();
				return queue;
			}

			// Token: 0x060042DF RID: 17119 RVA: 0x000E15A5 File Offset: 0x000DF7A5
			public override IEnumerable<T> Execute()
			{
				base.Initialize();
				int value = this.config.Options.GetValue(MqFunctionOption.Timeout, 0);
				this.receiveOptions = this.GetReceiveOptions(value);
				T t;
				while (this.TryReceive(out t))
				{
					if (t == null)
					{
						yield break;
					}
					yield return t;
				}
				yield break;
			}

			// Token: 0x060042E0 RID: 17120 RVA: 0x000E15B8 File Offset: 0x000DF7B8
			private bool TryReceive(out T message)
			{
				message = default(T);
				if (this.config.BatchSize != null)
				{
					long num = this.cursorPosition + 1L;
					long? batchSize = this.config.BatchSize;
					if ((num >= batchSize.GetValueOrDefault()) & (batchSize != null))
					{
						return false;
					}
				}
				if (this.cursorPosition == 0L)
				{
					this.receiveOptions = this.GetReceiveOptions(0);
				}
				bool flag;
				try
				{
					ReceiveMessage receiveMessage = this.TryReceiveBody();
					message = this.config.TransformMessage(receiveMessage);
					flag = true;
				}
				catch (Exception ex)
				{
					MqException ex2 = ex as MqException;
					if (ex2 != null && ex2.ReasonCode == 2033)
					{
						flag = false;
					}
					else
					{
						this.faulted = true;
						Exception ex3;
						if (!this.config.TryWrapException(ex, true, out ex3))
						{
							throw;
						}
						throw ex3;
					}
				}
				return flag;
			}

			// Token: 0x060042E1 RID: 17121
			protected abstract ReceiveMessage TryReceiveBody();

			// Token: 0x04002329 RID: 9001
			private long cursorPosition = -1L;

			// Token: 0x0400232A RID: 9002
			private ReceiveOptions receiveOptions;

			// Token: 0x0200092B RID: 2347
			public sealed class BrowseOperation : MqOperation<T>.MqGetOperation
			{
				// Token: 0x060042E2 RID: 17122 RVA: 0x000E1690 File Offset: 0x000DF890
				public BrowseOperation(IMqConfig<T> config)
					: base(config)
				{
				}

				// Token: 0x1700154B RID: 5451
				// (get) Token: 0x060042E3 RID: 17123 RVA: 0x00002105 File Offset: 0x00000305
				protected override bool LockMessages
				{
					get
					{
						return false;
					}
				}

				// Token: 0x060042E4 RID: 17124 RVA: 0x000E1699 File Offset: 0x000DF899
				protected override void InitializeOperation()
				{
					base.Queue = base.OpenQueueForReceive();
				}

				// Token: 0x060042E5 RID: 17125 RVA: 0x000E16A7 File Offset: 0x000DF8A7
				protected override ReceiveMessage TryReceiveBody()
				{
					ReceiveMessage receiveMessage = base.Queue.Receive(this.receiveOptions);
					this.cursorPosition += 1L;
					return receiveMessage;
				}
			}

			// Token: 0x0200092C RID: 2348
			public sealed class GetOperation : MqOperation<T>.MqGetOperation
			{
				// Token: 0x060042E6 RID: 17126 RVA: 0x000E1690 File Offset: 0x000DF890
				public GetOperation(IMqConfig<T> config)
					: base(config)
				{
				}

				// Token: 0x1700154C RID: 5452
				// (get) Token: 0x060042E7 RID: 17127 RVA: 0x00002139 File Offset: 0x00000339
				protected override bool LockMessages
				{
					get
					{
						return true;
					}
				}

				// Token: 0x060042E8 RID: 17128 RVA: 0x000E16C9 File Offset: 0x000DF8C9
				protected override void InitializeOperation()
				{
					this.messageLocks = new List<MqLock>();
				}

				// Token: 0x060042E9 RID: 17129 RVA: 0x000E16D8 File Offset: 0x000DF8D8
				protected override ReceiveMessage TryReceiveBody()
				{
					Queue queue = base.OpenQueueForReceive();
					ReceiveMessage receiveMessage = queue.Receive(this.receiveOptions);
					this.cursorPosition += 1L;
					this.messageLocks.Add(new MqLock(queue));
					return receiveMessage;
				}

				// Token: 0x060042EA RID: 17130 RVA: 0x000E1718 File Offset: 0x000DF918
				protected override void DisposeInternal()
				{
					if (this.messageLocks == null)
					{
						return;
					}
					foreach (MqLock mqLock in this.messageLocks)
					{
						try
						{
							if (!this.faulted)
							{
								mqLock.Destroy();
							}
							else
							{
								mqLock.Unlock();
							}
						}
						catch (MqException)
						{
							if (!this.faulted)
							{
								mqLock.Unlock();
							}
						}
						finally
						{
							mqLock.Close();
						}
					}
					this.messageLocks = null;
				}

				// Token: 0x0400232B RID: 9003
				private List<MqLock> messageLocks;
			}

			// Token: 0x0200092D RID: 2349
			public sealed class ClearOperation : MqOperation<T>.MqGetOperation
			{
				// Token: 0x060042EB RID: 17131 RVA: 0x000E1690 File Offset: 0x000DF890
				public ClearOperation(IMqConfig<T> config)
					: base(config)
				{
				}

				// Token: 0x1700154D RID: 5453
				// (get) Token: 0x060042EC RID: 17132 RVA: 0x00002105 File Offset: 0x00000305
				protected override bool LockMessages
				{
					get
					{
						return false;
					}
				}

				// Token: 0x1700154E RID: 5454
				// (get) Token: 0x060042ED RID: 17133 RVA: 0x00002105 File Offset: 0x00000305
				protected override bool BrowseMessages
				{
					get
					{
						return false;
					}
				}

				// Token: 0x060042EE RID: 17134 RVA: 0x000E1699 File Offset: 0x000DF899
				protected override void InitializeOperation()
				{
					base.Queue = base.OpenQueueForReceive();
				}

				// Token: 0x060042EF RID: 17135 RVA: 0x000E17BC File Offset: 0x000DF9BC
				protected override ReceiveMessage TryReceiveBody()
				{
					base.Queue.Receive(this.receiveOptions);
					this.cursorPosition += 1L;
					return ReceiveMessage.EmptyMessage;
				}
			}
		}

		// Token: 0x0200092F RID: 2351
		private sealed class MqPutOperation : MqOperation<T>
		{
			// Token: 0x060042F8 RID: 17144 RVA: 0x000E18DF File Offset: 0x000DFADF
			public MqPutOperation(IMqConfig<T> config, IEnumerable<SendMessage> messages)
				: base(config)
			{
				this.messages = messages;
			}

			// Token: 0x060042F9 RID: 17145 RVA: 0x000E18EF File Offset: 0x000DFAEF
			protected override void InitializeOperation()
			{
				base.Queue = new Queue(this.config.ConnectionParameters.Queue, base.QueueManager);
				base.Queue.OpenForSend();
			}

			// Token: 0x060042FA RID: 17146 RVA: 0x000E191D File Offset: 0x000DFB1D
			public override IEnumerable<T> Execute()
			{
				base.Initialize();
				foreach (SendMessage sendMessage in this.messages)
				{
					SendOptions sendOptions = new SendOptions();
					if (sendMessage.Correlator == null)
					{
						sendOptions.Options |= SendOption.NewCorrelationId;
					}
					if (sendMessage.MessageId == null)
					{
						sendOptions.Options |= SendOption.NewMessageId;
					}
					sendMessage.Options = sendOptions;
					if (sendMessage.Ccsid > 0 && sendMessage.Ccsid != 1208)
					{
						Value value = Utilities.ValueFromBytes(sendMessage.Data, Value.Null, TypeValue.Text, 1208);
						sendMessage.Data = Utilities.BytesFromString(value.AsString, null, new int?(sendMessage.Ccsid));
					}
					T t;
					try
					{
						base.Queue.Send(sendMessage);
						t = this.config.TransformMessage(sendMessage);
					}
					catch (Exception ex)
					{
						Exception ex2;
						if (!this.config.TryWrapException(ex, false, out ex2))
						{
							throw;
						}
						t = this.config.TransformException(ex2);
					}
					yield return t;
				}
				IEnumerator<SendMessage> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x04002330 RID: 9008
			private readonly IEnumerable<SendMessage> messages;
		}
	}
}
