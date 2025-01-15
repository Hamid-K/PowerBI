using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x02000008 RID: 8
	public interface IConfigurationRetriever<T>
	{
		// Token: 0x06000034 RID: 52
		Task<T> GetConfigurationAsync(string address, IDocumentRetriever retriever, CancellationToken cancel);
	}
}
