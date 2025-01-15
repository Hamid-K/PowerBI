using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Mapping;

namespace Owin
{
	// Token: 0x02000005 RID: 5
	public static class MapWhenExtensions
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021DC File Offset: 0x000003DC
		public static IAppBuilder MapWhen(this IAppBuilder app, Func<IOwinContext, bool> predicate, Action<IAppBuilder> configuration)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			MapWhenOptions options = new MapWhenOptions
			{
				Predicate = predicate
			};
			IAppBuilder result = app.Use(new object[] { options });
			IAppBuilder branch = app.New();
			configuration(branch);
			options.Branch = (Func<IDictionary<string, object>, Task>)branch.Build(typeof(Func<IDictionary<string, object>, Task>));
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000225C File Offset: 0x0000045C
		public static IAppBuilder MapWhenAsync(this IAppBuilder app, Func<IOwinContext, Task<bool>> predicate, Action<IAppBuilder> configuration)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			MapWhenOptions options = new MapWhenOptions
			{
				PredicateAsync = predicate
			};
			IAppBuilder result = app.Use(new object[] { options });
			IAppBuilder branch = app.New();
			configuration(branch);
			options.Branch = (Func<IDictionary<string, object>, Task>)branch.Build(typeof(Func<IDictionary<string, object>, Task>));
			return result;
		}
	}
}
