using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x0200000B RID: 11
	internal sealed class LifetimeManager : IDisposable
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022BF File Offset: 0x000004BF
		[NullableContext(1)]
		internal LifetimeManager(Action disposeAction)
		{
			if (disposeAction == null)
			{
				throw new ArgumentNullException("disposeAction");
			}
			this.m_disposeAction = disposeAction;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022DD File Offset: 0x000004DD
		public void Dispose()
		{
			if (!this.m_disposed)
			{
				this.m_disposeAction();
				this.m_disposed = true;
			}
		}

		// Token: 0x04000016 RID: 22
		[Nullable(1)]
		private readonly Action m_disposeAction;

		// Token: 0x04000017 RID: 23
		private bool m_disposed;
	}
}
