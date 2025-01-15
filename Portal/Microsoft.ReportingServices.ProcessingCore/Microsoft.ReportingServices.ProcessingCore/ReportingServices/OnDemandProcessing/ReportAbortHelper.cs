using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007E3 RID: 2019
	internal class ReportAbortHelper : AbortHelper, IDisposable
	{
		// Token: 0x06007153 RID: 29011 RVA: 0x001D777D File Offset: 0x001D597D
		internal ReportAbortHelper(IJobContext jobContext, bool enforceSingleAbortException)
			: base(jobContext, enforceSingleAbortException, true)
		{
			this.m_reportStatus = new Hashtable();
		}

		// Token: 0x06007154 RID: 29012 RVA: 0x001D7794 File Offset: 0x001D5994
		protected override ProcessingStatus GetStatus(string uniqueName)
		{
			ProcessingStatus status = base.Status;
			if (uniqueName == null)
			{
				return status;
			}
			if (status != ProcessingStatus.Success)
			{
				return status;
			}
			Global.Tracer.Assert(this.m_reportStatus.ContainsKey(uniqueName), "(m_reportStatus.ContainsKey(uniqueName))");
			return (ProcessingStatus)this.m_reportStatus[uniqueName];
		}

		// Token: 0x06007155 RID: 29013 RVA: 0x001D77E0 File Offset: 0x001D59E0
		protected override void SetStatus(ProcessingStatus newStatus, string uniqueName)
		{
			if (uniqueName == null)
			{
				base.Status = newStatus;
				return;
			}
			Hashtable hashtable = Hashtable.Synchronized(this.m_reportStatus);
			Global.Tracer.Assert(hashtable.ContainsKey(uniqueName), "(reportStatus.ContainsKey(uniqueName))");
			hashtable[uniqueName] = newStatus;
		}

		// Token: 0x06007156 RID: 29014 RVA: 0x001D7828 File Offset: 0x001D5A28
		internal override void AddSubreportInstanceOrSharedDataSet(string uniqueName)
		{
			Hashtable hashtable = Hashtable.Synchronized(this.m_reportStatus);
			if (!hashtable.ContainsKey(uniqueName))
			{
				hashtable.Add(uniqueName, ProcessingStatus.Success);
			}
		}

		// Token: 0x04003A68 RID: 14952
		private Hashtable m_reportStatus;
	}
}
