using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x0200001D RID: 29
	public sealed class GlobalExceptionHandlerMiddleware : OwinMiddleware
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000359D File Offset: 0x0000179D
		public GlobalExceptionHandlerMiddleware(OwinMiddleware next, Action<Exception> handler)
			: base(next)
		{
			this._handler = handler;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000035B0 File Offset: 0x000017B0
		public override async Task Invoke(IOwinContext context)
		{
			try
			{
				await base.Next.Invoke(context);
			}
			catch (Exception ex)
			{
				AggregateException ex2 = ex as AggregateException;
				if (ex2 != null)
				{
					IEnumerator<Exception> enumerator = ex2.Flatten().InnerExceptions.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							Exception ex3 = enumerator.Current;
							this._handler(ex3);
						}
						goto IL_00DB;
					}
					finally
					{
						int num;
						if (num < 0 && enumerator != null)
						{
							enumerator.Dispose();
						}
					}
				}
				this._handler(ex);
				IL_00DB:;
			}
		}

		// Token: 0x04000054 RID: 84
		private readonly Action<Exception> _handler;
	}
}
