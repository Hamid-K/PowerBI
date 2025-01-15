using System;
using System.Threading;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000048 RID: 72
	internal sealed class ExtensionWrapper : IDisposable
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00008B2B File Offset: 0x00006D2B
		public ExtensionWrapper(IExtension iProvider)
		{
			this.m_provider = iProvider;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00008B3A File Offset: 0x00006D3A
		public IExtension Extender
		{
			get
			{
				return this.m_provider;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008B42 File Offset: 0x00006D42
		public void Dispose()
		{
			Interlocked.Decrement(ref this.m_numUsers);
			this.CheckIfInvalid();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008B56 File Offset: 0x00006D56
		internal void MarkAsInvalid()
		{
			this.m_disposeWhenAllRelease = true;
			this.CheckIfInvalid();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008B65 File Offset: 0x00006D65
		internal void HandToUser()
		{
			Interlocked.Increment(ref this.m_numUsers);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008B73 File Offset: 0x00006D73
		private void CheckIfInvalid()
		{
			if (this.m_numUsers == 0 && this.m_disposeWhenAllRelease)
			{
				if (this.m_provider is IDisposable)
				{
					((IDisposable)this.m_provider).Dispose();
				}
				this.m_provider = null;
			}
		}

		// Token: 0x04000104 RID: 260
		private IExtension m_provider;

		// Token: 0x04000105 RID: 261
		private int m_numUsers;

		// Token: 0x04000106 RID: 262
		private bool m_disposeWhenAllRelease;
	}
}
