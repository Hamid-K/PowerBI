using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007E5 RID: 2021
	internal class DataShapeQueryAbortHelper : DataShapeAbortHelperBase, IDataShapeQueryAbortHelper, IDataShapeAbortHelper, IAbortHelper, IDisposable
	{
		// Token: 0x0600715B RID: 29019 RVA: 0x001D78AB File Offset: 0x001D5AAB
		public DataShapeQueryAbortHelper(IJobContext jobContext, bool enforceSingleAbortException)
			: base(jobContext, enforceSingleAbortException)
		{
		}

		// Token: 0x0600715C RID: 29020 RVA: 0x001D78C0 File Offset: 0x001D5AC0
		public void ThrowIfAborted(CancelationTrigger cancelationTrigger)
		{
			this.ThrowIfAborted(cancelationTrigger, null);
		}

		// Token: 0x0600715D RID: 29021 RVA: 0x001D78CC File Offset: 0x001D5ACC
		public override bool Abort(ProcessingStatus status)
		{
			bool flag = true;
			List<IDataShapeAbortHelper> abortHelpers = this.m_abortHelpers;
			bool flag3;
			lock (abortHelpers)
			{
				foreach (IDataShapeAbortHelper dataShapeAbortHelper in this.m_abortHelpers)
				{
					flag &= dataShapeAbortHelper.Abort(status);
				}
				flag3 = flag & base.Abort(status);
			}
			return flag3;
		}

		// Token: 0x0600715E RID: 29022 RVA: 0x001D795C File Offset: 0x001D5B5C
		public IDataShapeAbortHelper CreateDataShapeAbortHelper()
		{
			DataShapeAbortHelper dataShapeAbortHelper = new DataShapeAbortHelper(this);
			List<IDataShapeAbortHelper> abortHelpers = this.m_abortHelpers;
			lock (abortHelpers)
			{
				this.m_abortHelpers.Add(dataShapeAbortHelper);
				ProcessingStatus status = this.GetStatus(null);
				if (status != ProcessingStatus.Success)
				{
					dataShapeAbortHelper.Abort(status);
				}
			}
			return dataShapeAbortHelper;
		}

		// Token: 0x0600715F RID: 29023 RVA: 0x001D79C0 File Offset: 0x001D5BC0
		public void RemoveDataShapeAbortHelper(IDataShapeAbortHelper helperToBeRemoved)
		{
			List<IDataShapeAbortHelper> abortHelpers = this.m_abortHelpers;
			lock (abortHelpers)
			{
				this.m_abortHelpers.Remove(helperToBeRemoved);
			}
		}

		// Token: 0x04003A69 RID: 14953
		private readonly List<IDataShapeAbortHelper> m_abortHelpers = new List<IDataShapeAbortHelper>();
	}
}
