using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Filters
{
	// Token: 0x020000D0 RID: 208
	public interface IExceptionFilter : IFilter
	{
		// Token: 0x0600057B RID: 1403
		Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken);
	}
}
