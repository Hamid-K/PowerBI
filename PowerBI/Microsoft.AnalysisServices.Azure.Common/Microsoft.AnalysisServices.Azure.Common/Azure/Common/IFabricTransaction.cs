using System;
using System.Threading.Tasks;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000084 RID: 132
	public interface IFabricTransaction : IDisposable
	{
		// Token: 0x060004F1 RID: 1265
		Task<long> CommitAsync();
	}
}
