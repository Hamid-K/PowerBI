using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000E7 RID: 231
	public interface IDsvStatisticsProvider
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000BEC RID: 3052
		// (remove) Token: 0x06000BED RID: 3053
		event EventHandler<ProgressEventArgs> Progress;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000BEE RID: 3054
		// (remove) Token: 0x06000BEF RID: 3055
		event EventHandler<LogEventArgs> Log;

		// Token: 0x06000BF0 RID: 3056
		void Fill(DataSourceView dataSourceView, IDsvItemFilter filter, bool overwrite, ICancelEvent cancel);
	}
}
