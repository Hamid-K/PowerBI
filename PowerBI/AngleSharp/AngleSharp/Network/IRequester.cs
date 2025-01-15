using System;
using System.Threading;
using System.Threading.Tasks;

namespace AngleSharp.Network
{
	// Token: 0x02000094 RID: 148
	public interface IRequester
	{
		// Token: 0x06000473 RID: 1139
		bool SupportsProtocol(string protocol);

		// Token: 0x06000474 RID: 1140
		Task<IResponse> RequestAsync(IRequest request, CancellationToken cancel);
	}
}
