using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000029 RID: 41
	internal sealed class SafeCoTaskMem : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00005863 File Offset: 0x00003A63
		private SafeCoTaskMem()
			: base(true)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000586C File Offset: 0x00003A6C
		private SafeCoTaskMem(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005878 File Offset: 0x00003A78
		internal SafeCoTaskMem Alloc(int cb)
		{
			SafeCoTaskMem safeCoTaskMem = NativeMemoryMethods.CoTaskMemAlloc(cb);
			if (safeCoTaskMem.IsInvalid)
			{
				safeCoTaskMem.SetHandleAsInvalid();
				throw new OutOfMemoryException();
			}
			return safeCoTaskMem;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000058A1 File Offset: 0x00003AA1
		protected override bool ReleaseHandle()
		{
			NativeMemoryMethods.CoTaskMemFree(this.handle);
			return true;
		}
	}
}
