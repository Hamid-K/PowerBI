using System;
using System.Web.Http.ExceptionHandling;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000020 RID: 32
	public sealed class WebApiExceptionLogger : ExceptionLogger
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000408E File Offset: 0x0000228E
		public override void Log(ExceptionLoggerContext context)
		{
			Logger.Error("Unhandled error in the Web API. Exception: {0}", new object[] { context.Exception });
		}
	}
}
