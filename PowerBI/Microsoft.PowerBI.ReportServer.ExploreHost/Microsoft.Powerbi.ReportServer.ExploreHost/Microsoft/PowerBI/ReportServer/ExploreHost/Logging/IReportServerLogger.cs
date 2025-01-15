using System;

namespace Microsoft.PowerBI.ReportServer.ExploreHost.Logging
{
	// Token: 0x0200001E RID: 30
	public interface IReportServerLogger
	{
		// Token: 0x060000D0 RID: 208
		void Error(string formatString, params object[] formatParams);

		// Token: 0x060000D1 RID: 209
		void Info(string formatString, params object[] formatParams);

		// Token: 0x060000D2 RID: 210
		void Warning(string formatString, params object[] formatParams);

		// Token: 0x060000D3 RID: 211
		void Trace(string formatString, params object[] formatParams);

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D4 RID: 212
		LogLevel LogLevel { get; }
	}
}
