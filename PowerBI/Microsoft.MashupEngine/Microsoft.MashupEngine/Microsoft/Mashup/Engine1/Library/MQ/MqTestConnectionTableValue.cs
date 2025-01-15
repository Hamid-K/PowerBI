using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200093B RID: 2363
	internal sealed class MqTestConnectionTableValue : DelegatingTableValue
	{
		// Token: 0x0600434D RID: 17229 RVA: 0x000E3397 File Offset: 0x000E1597
		public MqTestConnectionTableValue(IEngineHost engineHost, MqConnectionParameters connectionParameters, TableValue tableValue)
			: base(tableValue)
		{
			this.engineHost = engineHost;
			this.connectionParameters = connectionParameters;
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x000E33B0 File Offset: 0x000E15B0
		public override void TestConnection()
		{
			QueueManager queueManager = null;
			Queue queue = null;
			try
			{
				queueManager = QueueManager.New(this.connectionParameters);
				queueManager.Connect();
				queue = new Queue(this.connectionParameters.Queue, queueManager);
				queue.OpenForReceive();
			}
			catch (MqException ex)
			{
				throw MqExceptionHandler.ProcessMqException(this.engineHost, ex, this.connectionParameters.Resource);
			}
			finally
			{
				if (queue != null)
				{
					queue.Close();
				}
				if (queueManager != null)
				{
					queueManager.Disconnect();
				}
			}
		}

		// Token: 0x04002351 RID: 9041
		private readonly IEngineHost engineHost;

		// Token: 0x04002352 RID: 9042
		private readonly MqConnectionParameters connectionParameters;
	}
}
