using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000EC RID: 236
	internal class ActionFilterResult : IHttpActionResult
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x0000FADE File Offset: 0x0000DCDE
		public ActionFilterResult(HttpActionBinding binding, HttpActionContext context, ServicesContainer services, IActionFilter[] filters)
		{
			this._binding = binding;
			this._context = context;
			this._services = services;
			this._filters = filters;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000FB04 File Offset: 0x0000DD04
		public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			await this._binding.ExecuteBindingAsync(this._context, cancellationToken);
			ActionFilterResult.ActionInvoker actionInvoker = new ActionFilterResult.ActionInvoker(this._context, cancellationToken, this._services);
			HttpResponseMessage httpResponseMessage;
			if (this._filters.Length == 0)
			{
				httpResponseMessage = await actionInvoker.InvokeActionAsync();
			}
			else
			{
				Func<ActionFilterResult.ActionInvoker, Task<HttpResponseMessage>> func = (ActionFilterResult.ActionInvoker innerInvoker) => innerInvoker.InvokeActionAsync();
				httpResponseMessage = await ActionFilterResult.InvokeActionWithActionFilters<ActionFilterResult.ActionInvoker>(this._context, cancellationToken, this._filters, func, actionInvoker)();
			}
			return httpResponseMessage;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000FB54 File Offset: 0x0000DD54
		public static Func<Task<HttpResponseMessage>> InvokeActionWithActionFilters(HttpActionContext actionContext, CancellationToken cancellationToken, IActionFilter[] filters, Func<Task<HttpResponseMessage>> innerAction)
		{
			Func<Task<HttpResponseMessage>> func = innerAction;
			Func<Func<Task<HttpResponseMessage>>, IActionFilter, Func<Task<HttpResponseMessage>>> <>9__0;
			for (int i = filters.Length - 1; i >= 0; i--)
			{
				IActionFilter actionFilter = filters[i];
				Func<Func<Task<HttpResponseMessage>>, IActionFilter, Func<Task<HttpResponseMessage>>> func2;
				if ((func2 = <>9__0) == null)
				{
					func2 = (<>9__0 = (Func<Task<HttpResponseMessage>> continuation, IActionFilter innerFilter) => () => innerFilter.ExecuteActionFilterAsync(actionContext, cancellationToken, continuation));
				}
				func = func2(func, actionFilter);
			}
			return func;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
		private static Func<Task<HttpResponseMessage>> InvokeActionWithActionFilters<T>(HttpActionContext actionContext, CancellationToken cancellationToken, IActionFilter[] filters, Func<T, Task<HttpResponseMessage>> innerAction, T state)
		{
			return ActionFilterResult.InvokeActionWithActionFilters(actionContext, cancellationToken, filters, () => innerAction(state));
		}

		// Token: 0x0400016C RID: 364
		private readonly HttpActionBinding _binding;

		// Token: 0x0400016D RID: 365
		private readonly HttpActionContext _context;

		// Token: 0x0400016E RID: 366
		private readonly ServicesContainer _services;

		// Token: 0x0400016F RID: 367
		private readonly IActionFilter[] _filters;

		// Token: 0x020001EC RID: 492
		private struct ActionInvoker
		{
			// Token: 0x06000B83 RID: 2947 RVA: 0x0001DA22 File Offset: 0x0001BC22
			public ActionInvoker(HttpActionContext context, CancellationToken cancellationToken, ServicesContainer controllerServices)
			{
				this._context = context;
				this._cancellationToken = cancellationToken;
				this._controllerServices = controllerServices;
			}

			// Token: 0x06000B84 RID: 2948 RVA: 0x0001DA39 File Offset: 0x0001BC39
			public Task<HttpResponseMessage> InvokeActionAsync()
			{
				return this._controllerServices.GetActionInvoker().InvokeActionAsync(this._context, this._cancellationToken);
			}

			// Token: 0x040003E6 RID: 998
			private readonly HttpActionContext _context;

			// Token: 0x040003E7 RID: 999
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040003E8 RID: 1000
			private readonly ServicesContainer _controllerServices;
		}
	}
}
