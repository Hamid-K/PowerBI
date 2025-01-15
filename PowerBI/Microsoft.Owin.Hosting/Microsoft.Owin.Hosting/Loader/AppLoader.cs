using System;
using System.Collections.Generic;
using System.Linq;
using Owin;

namespace Microsoft.Owin.Hosting.Loader
{
	// Token: 0x02000023 RID: 35
	public class AppLoader : IAppLoader
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004150 File Offset: 0x00002350
		public AppLoader(IEnumerable<IAppLoaderFactory> providers)
		{
			if (providers == null)
			{
				throw new ArgumentNullException("providers");
			}
			this._providers = providers;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004170 File Offset: 0x00002370
		public virtual Action<IAppBuilder> Load(string appName, IList<string> errors)
		{
			Func<string, IList<string>, Action<IAppBuilder>> chain = this._providers.Aggregate((string arg, IList<string> arg2) => null, (Func<string, IList<string>, Action<IAppBuilder>> next, IAppLoaderFactory provider) => provider.Create(next));
			return chain(appName, errors);
		}

		// Token: 0x0400003C RID: 60
		private readonly IEnumerable<IAppLoaderFactory> _providers;
	}
}
