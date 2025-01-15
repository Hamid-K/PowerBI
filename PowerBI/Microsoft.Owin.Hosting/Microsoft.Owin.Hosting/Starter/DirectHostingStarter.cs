using System;
using Microsoft.Owin.Hosting.Engine;

namespace Microsoft.Owin.Hosting.Starter
{
	// Token: 0x0200000F RID: 15
	public class DirectHostingStarter : IHostingStarter
	{
		// Token: 0x06000058 RID: 88 RVA: 0x000032E3 File Offset: 0x000014E3
		public DirectHostingStarter(IHostingEngine engine)
		{
			this._engine = engine;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000032F2 File Offset: 0x000014F2
		public virtual IDisposable Start(StartOptions options)
		{
			return this._engine.Start(new StartContext(options));
		}

		// Token: 0x0400002C RID: 44
		private readonly IHostingEngine _engine;
	}
}
