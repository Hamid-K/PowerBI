using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BD5 RID: 3029
	public class MqClientRuntimeAdministration
	{
		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06005E50 RID: 24144 RVA: 0x00180CC5 File Offset: 0x0017EEC5
		// (set) Token: 0x06005E51 RID: 24145 RVA: 0x00180CCD File Offset: 0x0017EECD
		public PoolingInformation PoolingInformation { get; private set; }

		// Token: 0x06005E52 RID: 24146 RVA: 0x00180CD8 File Offset: 0x0017EED8
		public static MqClientRuntimeAdministration GetAdministration()
		{
			if (MqClientRuntimeAdministration.runtimeInstance != null)
			{
				return MqClientRuntimeAdministration.runtimeInstance;
			}
			object obj = MqClientRuntimeAdministration.lockObject;
			MqClientRuntimeAdministration mqClientRuntimeAdministration;
			lock (obj)
			{
				if (MqClientRuntimeAdministration.runtimeInstance != null)
				{
					mqClientRuntimeAdministration = MqClientRuntimeAdministration.runtimeInstance;
				}
				else
				{
					MqClientRuntimeAdministration.runtimeInstance = MqClientRuntimeAdministration.InternalConstructor();
					mqClientRuntimeAdministration = MqClientRuntimeAdministration.runtimeInstance;
				}
			}
			return mqClientRuntimeAdministration;
		}

		// Token: 0x06005E53 RID: 24147 RVA: 0x00180D40 File Offset: 0x0017EF40
		private MqClientRuntimeAdministration(Dictionary<string, QueueManagerInformation> aliasToQueueManager, Dictionary<string, QueueInformation> aliasToQueue, PoolingInformation pooling, AdministrationTracePoint administrationTracePoint)
		{
			this.aliasToQueue = aliasToQueue;
			this.aliasToQueueManager = aliasToQueueManager;
			this.PoolingInformation = pooling;
			this.tracePoint = administrationTracePoint;
		}

		// Token: 0x06005E54 RID: 24148 RVA: 0x00180D68 File Offset: 0x0017EF68
		public QueueInformation GetQueue(string alias)
		{
			QueueInformation queueInformation = null;
			if (this.aliasToQueue.TryGetValue(alias, out queueInformation))
			{
				return queueInformation;
			}
			return null;
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x00180D8C File Offset: 0x0017EF8C
		public QueueManagerInformation GetQueueManager(string alias)
		{
			QueueManagerInformation queueManagerInformation = null;
			if (this.aliasToQueueManager.TryGetValue(alias, out queueManagerInformation))
			{
				return queueManagerInformation;
			}
			return null;
		}

		// Token: 0x06005E56 RID: 24150 RVA: 0x00180DB0 File Offset: 0x0017EFB0
		private static MqClientRuntimeAdministration InternalConstructor()
		{
			AdministrationTracePoint administrationTracePoint = new AdministrationTracePoint(new MqTraceContainer());
			MqClientAdministrationInfoAccessor mqClientAdministrationInfoAccessor = new MqClientAdministrationInfoAccessor(administrationTracePoint);
			Dictionary<string, QueueManagerInformation> dictionary = new Dictionary<string, QueueManagerInformation>();
			Dictionary<string, QueueInformation> dictionary2 = new Dictionary<string, QueueInformation>();
			foreach (QueueManagerInformation queueManagerInformation in mqClientAdministrationInfoAccessor.QueueManagers)
			{
				dictionary.Add(queueManagerInformation.Alias, queueManagerInformation);
			}
			foreach (QueueInformation queueInformation in mqClientAdministrationInfoAccessor.Queues)
			{
				dictionary2.Add(queueInformation.Alias, queueInformation);
			}
			if (administrationTracePoint.IsEnabled(TraceFlags.Verbose))
			{
				if (mqClientAdministrationInfoAccessor.QueueManagers.Count == 0)
				{
					administrationTracePoint.Trace(TraceFlags.Verbose, "No Queue Manager Aliases");
				}
				else
				{
					administrationTracePoint.Trace(TraceFlags.Verbose, "Queue Managers");
					foreach (QueueManagerInformation queueManagerInformation2 in mqClientAdministrationInfoAccessor.QueueManagers)
					{
						administrationTracePoint.Trace(TraceFlags.Verbose, "    " + queueManagerInformation2.ToString());
					}
				}
				if (mqClientAdministrationInfoAccessor.Queues.Count == 0)
				{
					administrationTracePoint.Trace(TraceFlags.Verbose, "No Queue Aliases");
				}
				else
				{
					administrationTracePoint.Trace(TraceFlags.Verbose, "Queues:");
					foreach (QueueInformation queueInformation2 in mqClientAdministrationInfoAccessor.Queues)
					{
						administrationTracePoint.Trace(TraceFlags.Verbose, "    " + queueInformation2.ToString());
					}
				}
				administrationTracePoint.Trace(TraceFlags.Verbose, "Pooling:");
				administrationTracePoint.Trace(TraceFlags.Verbose, "    " + mqClientAdministrationInfoAccessor.Pooling.QueueManagerBehavior.ToString());
			}
			return new MqClientRuntimeAdministration(dictionary, dictionary2, mqClientAdministrationInfoAccessor.Pooling, administrationTracePoint);
		}

		// Token: 0x04004FC6 RID: 20422
		private static MqClientRuntimeAdministration runtimeInstance = null;

		// Token: 0x04004FC7 RID: 20423
		private static object lockObject = new object();

		// Token: 0x04004FC8 RID: 20424
		private Dictionary<string, QueueManagerInformation> aliasToQueueManager;

		// Token: 0x04004FC9 RID: 20425
		private Dictionary<string, QueueInformation> aliasToQueue;

		// Token: 0x04004FCB RID: 20427
		private AdministrationTracePoint tracePoint;
	}
}
