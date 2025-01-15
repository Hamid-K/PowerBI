using System;
using System.Threading.Tasks;
using System.Web.Cors;

namespace Microsoft.Owin.Cors
{
	// Token: 0x02000003 RID: 3
	public class CorsOptions
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002304 File Offset: 0x00000504
		public static CorsOptions AllowAll
		{
			get
			{
				CorsOptions corsOptions = new CorsOptions();
				CorsPolicyProvider corsPolicyProvider = new CorsPolicyProvider();
				corsPolicyProvider.PolicyResolver = (IOwinRequest context) => Task.FromResult<CorsPolicy>(new CorsPolicy
				{
					AllowAnyHeader = true,
					AllowAnyMethod = true,
					AllowAnyOrigin = true,
					SupportsCredentials = true
				});
				corsOptions.PolicyProvider = corsPolicyProvider;
				return corsOptions;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000233B File Offset: 0x0000053B
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002343 File Offset: 0x00000543
		public ICorsPolicyProvider PolicyProvider { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000234C File Offset: 0x0000054C
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002354 File Offset: 0x00000554
		public ICorsEngine CorsEngine { get; set; }
	}
}
