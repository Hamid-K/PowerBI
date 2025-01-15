using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007E4 RID: 2020
	internal abstract class DataShapeAbortHelperBase : AbortHelper
	{
		// Token: 0x06007157 RID: 29015 RVA: 0x001D7857 File Offset: 0x001D5A57
		public DataShapeAbortHelperBase(IJobContext jobContext, bool enforceSingleAbortException)
			: base(jobContext, enforceSingleAbortException, false)
		{
		}

		// Token: 0x06007158 RID: 29016 RVA: 0x001D7862 File Offset: 0x001D5A62
		protected override ProcessingStatus GetStatus(string uniqueName)
		{
			Global.Tracer.Assert(uniqueName == null, "Data shape processing does not support sub-units.");
			return base.Status;
		}

		// Token: 0x06007159 RID: 29017 RVA: 0x001D787D File Offset: 0x001D5A7D
		protected override void SetStatus(ProcessingStatus newStatus, string uniqueName)
		{
			Global.Tracer.Assert(uniqueName == null, "Data shape processing does not support sub-units.");
			base.Status = newStatus;
		}

		// Token: 0x0600715A RID: 29018 RVA: 0x001D7899 File Offset: 0x001D5A99
		internal override void AddSubreportInstanceOrSharedDataSet(string uniqueName)
		{
			Global.Tracer.Assert(false, "Data shape processing does nto support sub-units.");
		}
	}
}
