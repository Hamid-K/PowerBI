using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001DE RID: 478
	public class DisposeController : IDisposable
	{
		// Token: 0x06000C7C RID: 3196 RVA: 0x0002BC8D File Offset: 0x00029E8D
		public DisposeController(IDisposable disposable)
		{
			this.m_disposable = disposable;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002BC9C File Offset: 0x00029E9C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002BCA5 File Offset: 0x00029EA5
		public void PreventDispose()
		{
			this.m_disposable = null;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0000E568 File Offset: 0x0000C768
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0000E568 File Offset: 0x0000C768
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002BCAE File Offset: 0x00029EAE
		private void Dispose(bool disposing)
		{
			if (disposing && this.m_disposable != null)
			{
				this.m_disposable.Dispose();
			}
			this.m_disposable = null;
		}

		// Token: 0x040004CC RID: 1228
		private IDisposable m_disposable;
	}
}
