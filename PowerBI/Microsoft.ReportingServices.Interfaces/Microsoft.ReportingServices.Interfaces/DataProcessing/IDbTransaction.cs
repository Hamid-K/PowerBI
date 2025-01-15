using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000019 RID: 25
	public interface IDbTransaction : IDisposable
	{
		// Token: 0x0600003B RID: 59
		void Commit();

		// Token: 0x0600003C RID: 60
		void Rollback();
	}
}
