using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;

namespace Owin
{
	// Token: 0x02000002 RID: 2
	public static class AppBuilderUseExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static IAppBuilder Use<T>(this IAppBuilder app, params object[] args)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			return app.Use(typeof(T), args);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002071 File Offset: 0x00000271
		public static void Run(this IAppBuilder app, Func<IOwinContext, Task> handler)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			app.Use(new object[] { handler });
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A0 File Offset: 0x000002A0
		public static IAppBuilder Use(this IAppBuilder app, Func<IOwinContext, Func<Task>, Task> handler)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			return app.Use(new object[] { handler });
		}
	}
}
