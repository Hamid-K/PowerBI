using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000D8 RID: 216
	internal sealed class DiagnosticsLifetimeManager : IDisposable
	{
		// Token: 0x060010C8 RID: 4296 RVA: 0x0004645C File Offset: 0x0004465C
		[NullableContext(1)]
		internal DiagnosticsLifetimeManager(Action disposeAction)
		{
			RuntimeChecks.CheckValue(disposeAction, "disposeAction");
			this.m_disposeAction = disposeAction;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00046476 File Offset: 0x00044676
		public void Dispose()
		{
			if (!this.m_disposed)
			{
				this.m_disposeAction();
				this.m_disposed = true;
			}
		}

		// Token: 0x04000364 RID: 868
		[Nullable(1)]
		private readonly Action m_disposeAction;

		// Token: 0x04000365 RID: 869
		private bool m_disposed;
	}
}
