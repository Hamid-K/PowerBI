using System;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.PowerBI.ReportServer.ExploreHost.Logging;

namespace Microsoft.PowerBI.ReportServer.WebApi.Logging
{
	// Token: 0x02000033 RID: 51
	internal class ReportServerLogger : IReportServerLogger
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x0000495F File Offset: 0x00002B5F
		public void Error(string formatString, params object[] formatParams)
		{
			Logger.Error(formatString, formatParams);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004968 File Offset: 0x00002B68
		public void Info(string formatString, params object[] formatParams)
		{
			Logger.Info(formatString, formatParams);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004971 File Offset: 0x00002B71
		public void Trace(string formatString, params object[] formatParams)
		{
			Logger.Trace(formatString, formatParams);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000497A File Offset: 0x00002B7A
		public void Warning(string formatString, params object[] formatParams)
		{
			Logger.Warning(formatString, formatParams);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004983 File Offset: 0x00002B83
		public LogLevel LogLevel
		{
			get
			{
				if (Logger.IsTraceEnabled())
				{
					return LogLevel.Trace;
				}
				if (Logger.IsInfoEnabled())
				{
					return LogLevel.Info;
				}
				if (Logger.IsWarnEnabled())
				{
					return LogLevel.Warn;
				}
				return LogLevel.Error;
			}
		}
	}
}
