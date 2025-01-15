using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AA RID: 170
	internal sealed class ProgressiveReportCounters : IDisposable
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000107B8 File Offset: 0x0000E9B8
		public static ProgressiveReportCounters Current
		{
			get
			{
				return ProgressiveReportCounters.s_current;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000107BF File Offset: 0x0000E9BF
		public void Init(string instanceName, bool resetCounters)
		{
			this.Init(new RSMonitor(instanceName, RSTrace.CatalogTrace), resetCounters);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000107D4 File Offset: 0x0000E9D4
		public void Init(ICounterProvider provider, bool resetCounters)
		{
			if (!ProgressiveReportCounters.s_initialized)
			{
				object obj = ProgressiveReportCounters.s_sync;
				lock (obj)
				{
					if (!ProgressiveReportCounters.s_initialized)
					{
						this.ClearCounters();
						this.InitInternal(provider, resetCounters);
						ProgressiveReportCounters.s_initialized = true;
					}
				}
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00010830 File Offset: 0x0000EA30
		private void InitInternal(ICounterProvider provider, bool resetCounters)
		{
			if (ProcessingContext.Configuration.CurrentApplication == RunningApplication.Unknown)
			{
				throw new InternalCatalogException("ProgressiveReportCounters class being used and diaganostic dll was not initialized correctly.");
			}
			this.m_reportRequestsActive = provider.GetCounterNumberOfItems("ReportServer.Power View", "Report Requests Active", resetCounters);
			this.m_countersList.Add(this.m_reportRequestsActive);
			this.m_reportRequestsTotal = provider.GetCounterRatePerSecond("ReportServer.Power View", "Report Requests Total", "Report Requests/sec", resetCounters);
			this.m_countersList.Add(this.m_reportRequestsTotal);
			this.m_reportRequestsAverageDuration = provider.GetCounterAverageCount("ReportServer.Power View", "Report Requests Average Duration (ms)", "Report Requests Average Duration base", resetCounters);
			this.m_countersList.Add(this.m_reportRequestsAverageDuration);
			this.m_modelRequestsActive = provider.GetCounterNumberOfItems("ReportServer.Power View", "Model Requests Active", resetCounters);
			this.m_countersList.Add(this.m_modelRequestsActive);
			this.m_modelRequestsTotal = provider.GetCounterRatePerSecond("ReportServer.Power View", "Model Requests Total", "Model Requests/sec", resetCounters);
			this.m_countersList.Add(this.m_modelRequestsTotal);
			this.m_modelRequestsAverageDuration = provider.GetCounterAverageCount("ReportServer.Power View", "Model Requests Average Duration (ms)", "Model Requests Average Duration base", resetCounters);
			this.m_countersList.Add(this.m_modelRequestsAverageDuration);
			this.m_queryRequestsActive = provider.GetCounterNumberOfItems("ReportServer.Power View", "Query Requests Active", resetCounters);
			this.m_countersList.Add(this.m_queryRequestsActive);
			this.m_queryRequestsTotal = provider.GetCounterRatePerSecond("ReportServer.Power View", "Query Requests Total", "Query Requests/sec", resetCounters);
			this.m_countersList.Add(this.m_queryRequestsTotal);
			this.m_queryRequestsAverageDuration = provider.GetCounterAverageCount("ReportServer.Power View", "Query Requests Average Duration (ms)", "Query Requests Average Duration base", resetCounters);
			this.m_countersList.Add(this.m_queryRequestsAverageDuration);
			this.m_failuresTotal = provider.GetCounterRatePerSecond("ReportServer.Power View", "Failures Total", "Failures/sec", resetCounters);
			this.m_countersList.Add(this.m_failuresTotal);
			this.m_sessionsActive = provider.GetCounterNumberOfItems("ReportServer.Power View", "Sessions Active", resetCounters);
			this.m_countersList.Add(this.m_sessionsActive);
			this.m_newSessionTotal = provider.GetCounterRatePerSecond("ReportServer.Power View", "Requests in New Sessions Total", "Requests in New Sessions/sec", resetCounters);
			this.m_countersList.Add(this.m_newSessionTotal);
			this.m_existingSessionTotal = provider.GetCounterRatePerSecond("ReportServer.Power View", "Requests in Existing Sessions Total", "Requests in Existing Sessions/sec", resetCounters);
			this.m_countersList.Add(this.m_existingSessionTotal);
			this.m_allRequestsTotal = provider.GetCounterRatePerSecond("ReportServer.Power View", "Requests Total", "Requests/sec", resetCounters);
			this.m_countersList.Add(this.m_allRequestsTotal);
			this.m_dataSourceConnectionFailuresTotal = provider.GetCounterRatePerSecond("ReportServer.Power View", "Data Source Connection Failures Total", "Data Source Connection Failures/sec", resetCounters);
			this.m_countersList.Add(this.m_dataSourceConnectionFailuresTotal);
			this.m_threadsActive = provider.GetCounterNumberOfItems("ReportServer.Power View", "Threads Active", resetCounters);
			this.m_countersList.Add(this.m_threadsActive);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00010B0D File Offset: 0x0000ED0D
		public int Count
		{
			get
			{
				return this.m_countersList.Count;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00010B1A File Offset: 0x0000ED1A
		public ICounter ReportRequestsActive
		{
			get
			{
				return this.m_reportRequestsActive;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00010B22 File Offset: 0x0000ED22
		public ICounter ReportRequestsTotal
		{
			get
			{
				return this.m_reportRequestsTotal;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00010B2A File Offset: 0x0000ED2A
		public ICounter ReportRequestsAverageDuration
		{
			get
			{
				return this.m_reportRequestsAverageDuration;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00010B32 File Offset: 0x0000ED32
		public ICounter ModelRequestsActive
		{
			get
			{
				return this.m_modelRequestsActive;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00010B3A File Offset: 0x0000ED3A
		public ICounter ModelRequestsTotal
		{
			get
			{
				return this.m_modelRequestsTotal;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00010B42 File Offset: 0x0000ED42
		public ICounter ModelRequestsAverageDuration
		{
			get
			{
				return this.m_modelRequestsAverageDuration;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00010B4A File Offset: 0x0000ED4A
		public ICounter QueryRequestsActive
		{
			get
			{
				return this.m_queryRequestsActive;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00010B52 File Offset: 0x0000ED52
		public ICounter QueryRequestsTotal
		{
			get
			{
				return this.m_queryRequestsTotal;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00010B5A File Offset: 0x0000ED5A
		public ICounter QueryRequestsAverageDuration
		{
			get
			{
				return this.m_queryRequestsAverageDuration;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00010B62 File Offset: 0x0000ED62
		public ICounter FailuresTotal
		{
			get
			{
				return this.m_failuresTotal;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00010B6A File Offset: 0x0000ED6A
		public ICounter ActiveSession
		{
			get
			{
				return this.m_sessionsActive;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00010B72 File Offset: 0x0000ED72
		public ICounter NewSessionTotal
		{
			get
			{
				return this.m_newSessionTotal;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00010B7A File Offset: 0x0000ED7A
		public ICounter ExistingSessionTotal
		{
			get
			{
				return this.m_existingSessionTotal;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00010B82 File Offset: 0x0000ED82
		public ICounter AllRequestsTotal
		{
			get
			{
				return this.m_allRequestsTotal;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00010B8A File Offset: 0x0000ED8A
		public ICounter DataSourceConnectionFailuresTotal
		{
			get
			{
				return this.m_dataSourceConnectionFailuresTotal;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00010B92 File Offset: 0x0000ED92
		public ICounter ActiveThreads
		{
			get
			{
				return this.m_threadsActive;
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00010B9A File Offset: 0x0000ED9A
		private void ClearCounters()
		{
			this.m_countersList.ForEach(delegate(IDisposable counter)
			{
				if (counter != null)
				{
					counter.Dispose();
				}
			});
			this.m_countersList.Clear();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00010BD1 File Offset: 0x0000EDD1
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ClearCounters();
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00010BDC File Offset: 0x0000EDDC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04000305 RID: 773
		private static readonly ProgressiveReportCounters s_current = new ProgressiveReportCounters();

		// Token: 0x04000306 RID: 774
		private static bool s_initialized;

		// Token: 0x04000307 RID: 775
		private static readonly object s_sync = new object();

		// Token: 0x04000308 RID: 776
		private List<IDisposable> m_countersList = new List<IDisposable>();

		// Token: 0x04000309 RID: 777
		private ICounter m_reportRequestsActive;

		// Token: 0x0400030A RID: 778
		private ICounter m_reportRequestsTotal;

		// Token: 0x0400030B RID: 779
		private ICounter m_reportRequestsAverageDuration;

		// Token: 0x0400030C RID: 780
		private ICounter m_modelRequestsActive;

		// Token: 0x0400030D RID: 781
		private ICounter m_modelRequestsTotal;

		// Token: 0x0400030E RID: 782
		private ICounter m_modelRequestsAverageDuration;

		// Token: 0x0400030F RID: 783
		private ICounter m_queryRequestsActive;

		// Token: 0x04000310 RID: 784
		private ICounter m_queryRequestsAverageDuration;

		// Token: 0x04000311 RID: 785
		private ICounter m_queryRequestsTotal;

		// Token: 0x04000312 RID: 786
		private ICounter m_failuresTotal;

		// Token: 0x04000313 RID: 787
		private ICounter m_sessionsActive;

		// Token: 0x04000314 RID: 788
		private ICounter m_newSessionTotal;

		// Token: 0x04000315 RID: 789
		private ICounter m_existingSessionTotal;

		// Token: 0x04000316 RID: 790
		private ICounter m_allRequestsTotal;

		// Token: 0x04000317 RID: 791
		private ICounter m_dataSourceConnectionFailuresTotal;

		// Token: 0x04000318 RID: 792
		private ICounter m_threadsActive;
	}
}
