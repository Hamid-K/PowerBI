using System;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.Azure.Common.DataContracts;
using Microsoft.PowerBI.ContentProviders;

namespace Microsoft.AnalysisServices.Azure.Common.DirectQuery
{
	// Token: 0x02000154 RID: 340
	public interface IContentProviderDirectQuery : IContentProviderInProcess, IContentProvider
	{
		// Token: 0x060011E0 RID: 4576
		Task<bool> TestConnectionAsync(DataAccessInfo dataSourceInfo);

		// Token: 0x060011E1 RID: 4577
		Task<DirectQueryPackageContentInfo> GetPackageContentInfoAsync(DataAccessInfo dataSourceInfo);
	}
}
