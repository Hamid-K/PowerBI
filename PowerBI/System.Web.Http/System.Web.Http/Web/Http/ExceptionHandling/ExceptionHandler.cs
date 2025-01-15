using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000D9 RID: 217
	public abstract class ExceptionHandler : IExceptionHandler
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
		Task IExceptionHandler.HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			if (!this.ShouldHandle(context))
			{
				return TaskHelpers.Completed();
			}
			return this.HandleAsync(context, cancellationToken);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000E5EE File Offset: 0x0000C7EE
		public virtual Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			this.Handle(context);
			return TaskHelpers.Completed();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00005744 File Offset: 0x00003944
		public virtual void Handle(ExceptionHandlerContext context)
		{
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		public virtual bool ShouldHandle(ExceptionHandlerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return context.ExceptionContext.CatchBlock.IsTopLevel;
		}
	}
}
