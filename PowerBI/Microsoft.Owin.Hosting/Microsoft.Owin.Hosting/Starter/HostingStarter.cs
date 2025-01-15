using System;

namespace Microsoft.Owin.Hosting.Starter
{
	// Token: 0x02000012 RID: 18
	public class HostingStarter : IHostingStarter
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000359A File Offset: 0x0000179A
		public HostingStarter(IHostingStarterFactory hostingStarterFactory)
		{
			this._hostingStarterFactory = hostingStarterFactory;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000035AC File Offset: 0x000017AC
		public virtual IDisposable Start(StartOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			string boot;
			options.Settings.TryGetValue("boot", out boot);
			IHostingStarter hostingStarter = this._hostingStarterFactory.Create(boot);
			return hostingStarter.Start(options);
		}

		// Token: 0x04000030 RID: 48
		private readonly IHostingStarterFactory _hostingStarterFactory;
	}
}
