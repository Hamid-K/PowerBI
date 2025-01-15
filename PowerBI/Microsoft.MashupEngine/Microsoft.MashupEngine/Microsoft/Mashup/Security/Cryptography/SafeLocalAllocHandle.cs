using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02002000 RID: 8192
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeLocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600C79F RID: 51103 RVA: 0x0000EDD3 File Offset: 0x0000CFD3
		private SafeLocalAllocHandle()
			: base(true)
		{
		}

		// Token: 0x0600C7A0 RID: 51104
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr hMem);

		// Token: 0x0600C7A1 RID: 51105 RVA: 0x0027B774 File Offset: 0x00279974
		internal unsafe T Read<T>(int offset) where T : struct
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			T t;
			try
			{
				base.DangerousAddRef(ref flag);
				t = (T)((object)Marshal.PtrToStructure(new IntPtr((void*)((byte*)this.handle.ToPointer() + offset)), typeof(T)));
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return t;
		}

		// Token: 0x0600C7A2 RID: 51106 RVA: 0x0027B7D4 File Offset: 0x002799D4
		protected override bool ReleaseHandle()
		{
			return SafeLocalAllocHandle.LocalFree(this.handle) == IntPtr.Zero;
		}
	}
}
