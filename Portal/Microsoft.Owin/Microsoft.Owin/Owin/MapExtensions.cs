using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Mapping;

namespace Owin
{
	// Token: 0x02000004 RID: 4
	public static class MapExtensions
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public static IAppBuilder Map(this IAppBuilder app, string pathMatch, Action<IAppBuilder> configuration)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (pathMatch == null)
			{
				throw new ArgumentNullException("pathMatch");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (!string.IsNullOrEmpty(pathMatch) && pathMatch.EndsWith("/", StringComparison.Ordinal))
			{
				throw new ArgumentException(Resources.Exception_PathMustNotEndWithSlash, "pathMatch");
			}
			return app.Map(new PathString(pathMatch), configuration);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000213C File Offset: 0x0000033C
		public static IAppBuilder Map(this IAppBuilder app, PathString pathMatch, Action<IAppBuilder> configuration)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (pathMatch.HasValue && pathMatch.Value.EndsWith("/", StringComparison.Ordinal))
			{
				throw new ArgumentException(Resources.Exception_PathMustNotEndWithSlash, "pathMatch");
			}
			MapOptions options = new MapOptions
			{
				PathMatch = pathMatch
			};
			IAppBuilder result = app.Use(new object[] { options });
			IAppBuilder branch = app.New();
			configuration(branch);
			options.Branch = (Func<IDictionary<string, object>, Task>)branch.Build(typeof(Func<IDictionary<string, object>, Task>));
			return result;
		}
	}
}
