using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000E1 RID: 225
	internal class EmptyExceptionHandler : IExceptionHandler
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x0000E850 File Offset: 0x0000CA50
		public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			return TaskHelpers.Completed();
		}
	}
}
