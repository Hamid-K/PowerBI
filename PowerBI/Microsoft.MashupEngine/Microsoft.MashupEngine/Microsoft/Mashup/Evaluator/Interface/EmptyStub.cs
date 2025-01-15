using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E2B RID: 7723
	public sealed class EmptyStub : IRemoteServiceStub, IDisposable
	{
		// Token: 0x0600BE2F RID: 48687 RVA: 0x000020FD File Offset: 0x000002FD
		private EmptyStub()
		{
		}

		// Token: 0x0600BE30 RID: 48688 RVA: 0x0000336E File Offset: 0x0000156E
		void IDisposable.Dispose()
		{
		}

		// Token: 0x040060E5 RID: 24805
		public static readonly IRemoteServiceStub Instance = new EmptyStub();
	}
}
