using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200004C RID: 76
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	internal sealed class SafeUnmanagedMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0000FD80 File Offset: 0x0000DF80
		internal SafeUnmanagedMemoryHandle()
			: base(true)
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000FD89 File Offset: 0x0000DF89
		internal SafeUnmanagedMemoryHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000FD99 File Offset: 0x0000DF99
		protected override bool ReleaseHandle()
		{
			if (this.handle != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.handle);
				this.handle = IntPtr.Zero;
				return true;
			}
			return false;
		}
	}
}
