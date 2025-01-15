using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x0200000A RID: 10
	public interface IDocumentRetriever
	{
		// Token: 0x06000036 RID: 54
		Task<string> GetDocumentAsync(string address, CancellationToken cancel);
	}
}
