using System;
using System.Collections.Generic;
using Microsoft.Owin.Hosting.Builder;
using Owin;
using Owin.Loader;

namespace Microsoft.Owin.Hosting.Loader
{
	// Token: 0x02000024 RID: 36
	public class AppLoaderFactory : IAppLoaderFactory
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x000041CF File Offset: 0x000023CF
		public AppLoaderFactory(IAppActivator activator)
		{
			this._activator = activator;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000041DE File Offset: 0x000023DE
		public virtual int Order
		{
			get
			{
				return -100;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000041E4 File Offset: 0x000023E4
		public virtual Func<string, IList<string>, Action<IAppBuilder>> Create(Func<string, IList<string>, Action<IAppBuilder>> nextLoader)
		{
			DefaultLoader loader = new DefaultLoader(nextLoader, new Func<Type, object>(this._activator.Activate));
			return new Func<string, IList<string>, Action<IAppBuilder>>(loader.Load);
		}

		// Token: 0x0400003D RID: 61
		private readonly IAppActivator _activator;
	}
}
