using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x0200000B RID: 11
	public class StaticConfigurationManager<T> : BaseConfigurationManager, IConfigurationManager<T> where T : class
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002844 File Offset: 0x00000A44
		public StaticConfigurationManager(T configuration)
		{
			if (configuration == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentNullException("configuration", LogHelper.FormatInvariant("IDX20000: The parameter '{0}' cannot be a 'null' or an empty object.", new object[] { LogHelper.MarkAsNonPII("configuration") })));
			}
			this._configuration = configuration;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002893 File Offset: 0x00000A93
		public Task<T> GetConfigurationAsync(CancellationToken cancel)
		{
			return Task.FromResult<T>(this._configuration);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028A0 File Offset: 0x00000AA0
		public override Task<BaseConfiguration> GetBaseConfigurationAsync(CancellationToken cancel)
		{
			return Task.FromResult<BaseConfiguration>(this._configuration as BaseConfiguration);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028B7 File Offset: 0x00000AB7
		public override void RequestRefresh()
		{
		}

		// Token: 0x0400001F RID: 31
		private T _configuration;
	}
}
