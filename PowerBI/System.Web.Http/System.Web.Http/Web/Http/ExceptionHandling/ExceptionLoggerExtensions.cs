using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000DB RID: 219
	public static class ExceptionLoggerExtensions
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x0000E688 File Offset: 0x0000C888
		public static Task LogAsync(this IExceptionLogger logger, ExceptionContext context, CancellationToken cancellationToken)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionLoggerContext exceptionLoggerContext = new ExceptionLoggerContext(context);
			return logger.LogAsync(exceptionLoggerContext, cancellationToken);
		}
	}
}
