using System;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000923 RID: 2339
	public class MqLock
	{
		// Token: 0x060042B6 RID: 17078 RVA: 0x000E0AC3 File Offset: 0x000DECC3
		public MqLock(Queue queue)
		{
			this.queue = queue;
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x000E0AD4 File Offset: 0x000DECD4
		public void Unlock()
		{
			if (this.queue != null && this.queue.State == QueueState.OpenReceive)
			{
				try
				{
					ReceiveOptions receiveOptions = new ReceiveOptions
					{
						Timeout = 0,
						Wait = false
					};
					receiveOptions.Options |= ReceiveOption.Unlock;
					this.queue.Receive(receiveOptions);
				}
				catch (MqException)
				{
				}
			}
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x000E0B40 File Offset: 0x000DED40
		public void Destroy()
		{
			if (this.queue != null && this.queue.State == QueueState.OpenReceive)
			{
				ReceiveOptions receiveOptions = new ReceiveOptions
				{
					Timeout = 0,
					Wait = false
				};
				receiveOptions.Options |= ReceiveOption.MessageUnderCursor;
				receiveOptions.TruncationSize = 0;
				this.queue.Receive(receiveOptions);
			}
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x000E0B9D File Offset: 0x000DED9D
		public void Close()
		{
			if (this.queue != null)
			{
				this.queue.Close();
			}
		}

		// Token: 0x04002315 RID: 8981
		private readonly Queue queue;
	}
}
