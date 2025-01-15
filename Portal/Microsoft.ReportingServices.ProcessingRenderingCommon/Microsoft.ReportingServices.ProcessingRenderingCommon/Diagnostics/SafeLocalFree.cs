using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000027 RID: 39
	internal sealed class SafeLocalFree : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000122 RID: 290 RVA: 0x000056EF File Offset: 0x000038EF
		private SafeLocalFree()
			: base(true)
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000056F8 File Offset: 0x000038F8
		private SafeLocalFree(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005701 File Offset: 0x00003901
		internal static SafeLocalFree LocalAlloc(int cb)
		{
			return SafeLocalFree.LocalAlloc(0, cb);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000570C File Offset: 0x0000390C
		internal static SafeLocalFree LocalAlloc(int flags, int cb)
		{
			SafeLocalFree safeLocalFree = NativeMemoryMethods.LocalAlloc(flags, (UIntPtr)((ulong)((long)cb)));
			if (safeLocalFree.IsInvalid)
			{
				safeLocalFree.SetHandleAsInvalid();
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return safeLocalFree;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005741 File Offset: 0x00003941
		protected override bool ReleaseHandle()
		{
			return NativeMemoryMethods.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x04000097 RID: 151
		internal const int LMEM_FIXED = 0;

		// Token: 0x04000098 RID: 152
		internal const int LMEM_ZEROINIT = 64;

		// Token: 0x04000099 RID: 153
		internal const int LPTR = 64;

		// Token: 0x0400009A RID: 154
		private const int NULL = 0;

		// Token: 0x0400009B RID: 155
		internal static readonly SafeLocalFree Zero = new SafeLocalFree(false);
	}
}
