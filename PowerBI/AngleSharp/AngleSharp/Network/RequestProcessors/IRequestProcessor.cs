using System;
using System.Threading.Tasks;

namespace AngleSharp.Network.RequestProcessors
{
	// Token: 0x020000A3 RID: 163
	internal interface IRequestProcessor
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004D0 RID: 1232
		IDownload Download { get; }

		// Token: 0x060004D1 RID: 1233
		Task ProcessAsync(ResourceRequest request);
	}
}
