using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E2C RID: 7724
	public sealed class EmptyProxy : IRemoteServiceProxy, IEngineHost, IDisposable
	{
		// Token: 0x0600BE32 RID: 48690 RVA: 0x000020FD File Offset: 0x000002FD
		private EmptyProxy()
		{
		}

		// Token: 0x0600BE33 RID: 48691 RVA: 0x0026786C File Offset: 0x00265A6C
		T IEngineHost.QueryService<T>()
		{
			return default(T);
		}

		// Token: 0x0600BE34 RID: 48692 RVA: 0x0000336E File Offset: 0x0000156E
		void IDisposable.Dispose()
		{
		}

		// Token: 0x040060E6 RID: 24806
		public static readonly IRemoteServiceProxy Instance = new EmptyProxy();
	}
}
