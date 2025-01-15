using System;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000007 RID: 7
	public static class ExceptionUtils
	{
		// Token: 0x0600003F RID: 63 RVA: 0x0000396C File Offset: 0x00001B6C
		public static Exception GetInnerMostException(Exception ex)
		{
			Exception ex2 = ex;
			while (ex2.InnerException != null)
			{
				ex2 = ex2.InnerException;
			}
			return ex2;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000398D File Offset: 0x00001B8D
		public static bool TraceAndHandleException(string TraceIdentifier, Exception ex)
		{
			ExceptionUtils.TraceException(TraceIdentifier, ex);
			return true;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003997 File Offset: 0x00001B97
		public static void TraceException(string TraceIdentifier, Exception ex)
		{
			ExceptionUtils.GetInnerMostException(ex);
			Logger.Error(ex.Message, Array.Empty<object>());
		}
	}
}
