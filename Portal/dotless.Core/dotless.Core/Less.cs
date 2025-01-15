using System;
using dotless.Core.configuration;

namespace dotless.Core
{
	// Token: 0x02000009 RID: 9
	public static class Less
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000029EF File Offset: 0x00000BEF
		public static string Parse(string less)
		{
			return Less.Parse(less, DotlessConfiguration.GetDefault());
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000029FC File Offset: 0x00000BFC
		public static string Parse(string less, DotlessConfiguration config)
		{
			if (config.Web)
			{
				throw new Exception("Please use dotless.Core.LessWeb.Parse for web applications. This makes sure all web features are available.");
			}
			return new EngineFactory(config).GetEngine().TransformToCss(less, null);
		}
	}
}
