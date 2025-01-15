using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BF2 RID: 7154
	public static class ExceptionLoggingService
	{
		// Token: 0x0600B29D RID: 45725 RVA: 0x00245C9C File Offset: 0x00243E9C
		public static void LogIgnoredException(this IEngineHost host, Exception e)
		{
			if (host != null)
			{
				IExceptionLoggingService exceptionLoggingService = host.QueryService<IExceptionLoggingService>();
				if (exceptionLoggingService != null)
				{
					exceptionLoggingService.LogException(e);
				}
			}
		}
	}
}
