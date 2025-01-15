using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000DE RID: 222
	public interface IExceptionHandler
	{
		// Token: 0x060005BF RID: 1471
		Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken);
	}
}
