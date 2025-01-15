using System;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000005 RID: 5
	public interface IServiceScope : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8
		IServiceProvider ServiceProvider { get; }
	}
}
