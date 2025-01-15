using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Network;
using AngleSharp.Services.Media;

namespace AngleSharp.Services
{
	// Token: 0x02000035 RID: 53
	public interface IResourceService<TResource> where TResource : IResourceInfo
	{
		// Token: 0x06000139 RID: 313
		bool SupportsType(string mimeType);

		// Token: 0x0600013A RID: 314
		Task<TResource> CreateAsync(IResponse response, CancellationToken cancel);
	}
}
