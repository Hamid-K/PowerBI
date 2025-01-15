using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace System.Web.Http.Owin.ExceptionHandling
{
	// Token: 0x02000017 RID: 23
	internal class EmptyExceptionLogger : IExceptionLogger
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000039A1 File Offset: 0x00001BA1
		public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
			return TaskHelpers.Completed();
		}
	}
}
