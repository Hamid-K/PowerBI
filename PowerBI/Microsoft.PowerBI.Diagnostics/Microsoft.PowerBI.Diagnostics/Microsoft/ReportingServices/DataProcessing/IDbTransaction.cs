using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200002B RID: 43
	public interface IDbTransaction : IDisposable
	{
		// Token: 0x060000B1 RID: 177
		void Commit();

		// Token: 0x060000B2 RID: 178
		void Rollback();
	}
}
