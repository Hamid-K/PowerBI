using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Cis.Eventing.Listeners
{
	// Token: 0x02000495 RID: 1173
	internal class ActivityGCWorker
	{
		// Token: 0x060028A0 RID: 10400 RVA: 0x0007B730 File Offset: 0x00079930
		public ActivityGCWorker(Dictionary<string, Dictionary<string, DateTime>> OperationEventsDict, Dictionary<string, int> OperationEventsReference)
		{
			ActivityGCWorker.timeCB = new TimerCallback(this.Refresh);
			ActivityGCWorker.timer = new Timer(ActivityGCWorker.timeCB, null, this.callbackStartDelay, this.callbackInterval);
			this.operationEvents = OperationEventsDict;
			this.operationReferences = OperationEventsReference;
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x0007B794 File Offset: 0x00079994
		public void SetListener(RDEventMonitoringAgentListener listener)
		{
			this.maListener = listener;
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x0007B7A0 File Offset: 0x000799A0
		public void Refresh(object state)
		{
			lock (this)
			{
				if (ActivityGCWorker.inCallback)
				{
					return;
				}
				ActivityGCWorker.inCallback = true;
			}
			try
			{
				List<string> list = new List<string>();
				lock (this.operationEvents)
				{
					foreach (string text in this.operationEvents.Keys)
					{
						Dictionary<string, DateTime> dictionary2 = this.operationEvents[text];
						DateTime dateTime = DateTime.FromFileTimeUtc(1L);
						foreach (DateTime dateTime2 in dictionary2.Values)
						{
							if (dateTime2 > dateTime)
							{
								dateTime = dateTime2;
							}
						}
						if (DateTime.UtcNow - dateTime > TimeSpan.FromHours(1.0))
						{
							list.Add(text);
							this.maListener.LogDanglingE2ETraceEvent(text);
						}
					}
				}
				foreach (string text2 in list)
				{
					try
					{
						lock (this.operationEvents)
						{
							this.operationEvents[text2].Clear();
							this.operationEvents.Remove(text2);
						}
						lock (this.operationReferences)
						{
							this.operationReferences.Remove(text2);
						}
					}
					catch
					{
					}
				}
			}
			catch (Exception)
			{
				lock (this)
				{
					ActivityGCWorker.inCallback = false;
				}
			}
			lock (this)
			{
				ActivityGCWorker.inCallback = false;
			}
		}

		// Token: 0x040017D8 RID: 6104
		private int callbackStartDelay = 300000;

		// Token: 0x040017D9 RID: 6105
		private int callbackInterval = 300000;

		// Token: 0x040017DA RID: 6106
		private static TimerCallback timeCB;

		// Token: 0x040017DB RID: 6107
		private static Timer timer;

		// Token: 0x040017DC RID: 6108
		private static bool inCallback = false;

		// Token: 0x040017DD RID: 6109
		private Dictionary<string, Dictionary<string, DateTime>> operationEvents;

		// Token: 0x040017DE RID: 6110
		private Dictionary<string, int> operationReferences;

		// Token: 0x040017DF RID: 6111
		private RDEventMonitoringAgentListener maListener;
	}
}
