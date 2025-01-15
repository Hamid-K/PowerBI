using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007E6 RID: 2022
	internal class DataShapeAbortHelper : DataShapeAbortHelperBase, IDataShapeAbortHelper, IAbortHelper, IDisposable
	{
		// Token: 0x06007160 RID: 29024 RVA: 0x001D7A08 File Offset: 0x001D5C08
		public DataShapeAbortHelper(DataShapeQueryAbortHelper parentAbortHelper)
			: base(null, true)
		{
			this.m_parentAbortHelper = parentAbortHelper;
		}

		// Token: 0x06007161 RID: 29025 RVA: 0x001D7A19 File Offset: 0x001D5C19
		public void ThrowIfAborted(CancelationTrigger cancelationTrigger)
		{
			this.ThrowIfAborted(cancelationTrigger, null);
		}

		// Token: 0x06007162 RID: 29026 RVA: 0x001D7A23 File Offset: 0x001D5C23
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_parentAbortHelper.RemoveDataShapeAbortHelper(this);
			}
			base.Dispose(disposing);
		}

		// Token: 0x04003A6A RID: 14954
		private readonly DataShapeQueryAbortHelper m_parentAbortHelper;
	}
}
