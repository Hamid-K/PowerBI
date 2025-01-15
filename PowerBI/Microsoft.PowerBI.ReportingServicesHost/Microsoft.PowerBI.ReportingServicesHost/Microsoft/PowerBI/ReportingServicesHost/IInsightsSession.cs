using System;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200003F RID: 63
	public interface IInsightsSession : IDisposable
	{
		// Token: 0x0600015F RID: 351
		Task<string> ExecuteAnalysisAsync(string request);

		// Token: 0x06000160 RID: 352
		void CancelAnalysis(string request);

		// Token: 0x06000161 RID: 353
		void NotifyModelChanged(InsightsSessionModelChangedArgs modelChangedArgs);
	}
}
