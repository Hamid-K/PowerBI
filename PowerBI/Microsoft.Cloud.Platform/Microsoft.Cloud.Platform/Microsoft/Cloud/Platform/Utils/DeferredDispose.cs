using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001E1 RID: 481
	public sealed class DeferredDispose : IDisposable
	{
		// Token: 0x06000C88 RID: 3208 RVA: 0x0002BD54 File Offset: 0x00029F54
		public DeferredDispose([NotNull] Action onDispose)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(onDispose, "onDispose");
			this.m_onDispose = onDispose;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002BD6E File Offset: 0x00029F6E
		public DeferredDispose([NotNull] Action onInit, [NotNull] Action onDispose)
			: this(onDispose)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(onInit, "onInit");
			onInit();
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002BD88 File Offset: 0x00029F88
		public DeferredDispose([NotNull] Action<bool> onDispose, bool fireVerboseEvents)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action<bool>>(onDispose, "onDispose");
			ExtendedDiagnostics.EnsureArgumentNotNull<bool>(fireVerboseEvents, "fireVerboseEvents");
			this.m_onDisposeWithoutVerboseEvents = onDispose;
			this.m_fireVerboseEvents = fireVerboseEvents;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002BDB4 File Offset: 0x00029FB4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002BDBD File Offset: 0x00029FBD
		private void Dispose(bool disposing)
		{
			if (disposing && !this.m_isDisposed)
			{
				this.m_isDisposed = true;
				if (this.m_onDisposeWithoutVerboseEvents != null)
				{
					this.m_onDisposeWithoutVerboseEvents(this.m_fireVerboseEvents);
					return;
				}
				this.m_onDispose();
			}
		}

		// Token: 0x040004CE RID: 1230
		private readonly Action m_onDispose;

		// Token: 0x040004CF RID: 1231
		private bool m_isDisposed;

		// Token: 0x040004D0 RID: 1232
		private readonly Action<bool> m_onDisposeWithoutVerboseEvents;

		// Token: 0x040004D1 RID: 1233
		private bool m_fireVerboseEvents;
	}
}
