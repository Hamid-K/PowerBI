using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000DF RID: 223
	public interface IExceptionLogger
	{
		// Token: 0x060005C0 RID: 1472
		Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken);
	}
}
