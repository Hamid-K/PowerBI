using System;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000E6 RID: 230
	internal sealed class SqlDebugContext : IDisposable
	{
		// Token: 0x06001133 RID: 4403 RVA: 0x0003F719 File Offset: 0x0003D919
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0003F728 File Offset: 0x0003D928
		private void Dispose(bool disposing)
		{
			if (this.pMemMap != IntPtr.Zero)
			{
				NativeMethods.UnmapViewOfFile(this.pMemMap);
				this.pMemMap = IntPtr.Zero;
			}
			if (this.hMemMap != IntPtr.Zero)
			{
				NativeMethods.CloseHandle(this.hMemMap);
				this.hMemMap = IntPtr.Zero;
			}
			this.active = false;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0003F790 File Offset: 0x0003D990
		~SqlDebugContext()
		{
			this.Dispose(false);
		}

		// Token: 0x04000729 RID: 1833
		internal uint pid;

		// Token: 0x0400072A RID: 1834
		internal uint tid;

		// Token: 0x0400072B RID: 1835
		internal bool active;

		// Token: 0x0400072C RID: 1836
		internal IntPtr pMemMap = ADP.s_ptrZero;

		// Token: 0x0400072D RID: 1837
		internal IntPtr hMemMap = ADP.s_ptrZero;

		// Token: 0x0400072E RID: 1838
		internal uint dbgpid;

		// Token: 0x0400072F RID: 1839
		internal bool fOption;

		// Token: 0x04000730 RID: 1840
		internal string machineName;

		// Token: 0x04000731 RID: 1841
		internal string sdiDllName;

		// Token: 0x04000732 RID: 1842
		internal byte[] data;
	}
}
