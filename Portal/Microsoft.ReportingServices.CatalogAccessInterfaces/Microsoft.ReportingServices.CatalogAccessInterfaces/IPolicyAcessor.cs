using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000012 RID: 18
	public interface IPolicyAcessor
	{
		// Token: 0x060000D8 RID: 216
		Task<IEnumerable<PolicyEntity>> GetItemPolicies(Guid itemId, int authType);
	}
}
