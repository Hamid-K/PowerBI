using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment.Cryptography;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;

namespace Microsoft.BIServer.HostingEnvironment.ManagementAdapter
{
	// Token: 0x0200002C RID: 44
	public interface IManagementService
	{
		// Token: 0x06000139 RID: 313
		Task<Crypto> GetCatalogCrypto();

		// Token: 0x0600013A RID: 314
		Task<Dictionary<ConfigSettings, bool>> GetConfigSwitches();
	}
}
