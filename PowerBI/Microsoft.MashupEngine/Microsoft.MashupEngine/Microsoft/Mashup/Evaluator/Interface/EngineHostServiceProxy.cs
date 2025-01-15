using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E2D RID: 7725
	public sealed class EngineHostServiceProxy : IRemoteServiceProxy, IEngineHost, IDisposable
	{
		// Token: 0x0600BE36 RID: 48694 RVA: 0x0026788E File Offset: 0x00265A8E
		public EngineHostServiceProxy(IEngineHost engineHost)
		{
			this.engineHost = engineHost;
		}

		// Token: 0x0600BE37 RID: 48695 RVA: 0x0026789D File Offset: 0x00265A9D
		T IEngineHost.QueryService<T>()
		{
			return this.engineHost.QueryService<T>();
		}

		// Token: 0x0600BE38 RID: 48696 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x040060E7 RID: 24807
		private readonly IEngineHost engineHost;
	}
}
