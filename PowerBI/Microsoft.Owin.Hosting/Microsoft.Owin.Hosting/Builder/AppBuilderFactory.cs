using System;
using Microsoft.Owin.Builder;
using Owin;

namespace Microsoft.Owin.Hosting.Builder
{
	// Token: 0x0200002B RID: 43
	public class AppBuilderFactory : IAppBuilderFactory
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00004ADD File Offset: 0x00002CDD
		public virtual IAppBuilder Create()
		{
			return new AppBuilder();
		}
	}
}
