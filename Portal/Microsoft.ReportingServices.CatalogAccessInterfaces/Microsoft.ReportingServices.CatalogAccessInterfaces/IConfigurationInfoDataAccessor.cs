using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000011 RID: 17
	public interface IConfigurationInfoDataAccessor : IDisposable
	{
		// Token: 0x060000D7 RID: 215
		Task<IDictionary<string, string>> GetConfigInfoValuesAsync();
	}
}
