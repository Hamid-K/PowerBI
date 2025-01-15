using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000435 RID: 1077
	[SuppressUnmanagedCodeSecurity]
	internal abstract class WaitableTimer : WaitHandle
	{
		// Token: 0x06002592 RID: 9618 RVA: 0x000732BC File Offset: 0x000714BC
		public WaitableTimer()
		{
			SafeWaitHandle safeWaitHandle = NativeMethods.CreateWaitableTimer(IntPtr.Zero, false, null);
			if (safeWaitHandle.IsInvalid)
			{
				int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
				Marshal.ThrowExceptionForHR(hrforLastWin32Error);
			}
			base.SafeWaitHandle = safeWaitHandle;
			this.m_waitCallback = null;
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x000732FE File Offset: 0x000714FE
		// (set) Token: 0x06002594 RID: 9620 RVA: 0x00073306 File Offset: 0x00071506
		public WaitOrTimerCallback WaitCallback
		{
			get
			{
				return this.m_waitCallback;
			}
			set
			{
				ReleaseAssert.IsTrue(this.m_waitCallback == null);
				this.m_waitCallback = value;
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x00073320 File Offset: 0x00071520
		public virtual void SetTimer(ulong dueTime)
		{
			if (!NativeMethods.SetWaitableTimer(base.SafeWaitHandle, ref dueTime, 0, IntPtr.Zero, IntPtr.Zero, false))
			{
				ReleaseAssert.IsTrue(false);
				int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
				Marshal.ThrowExceptionForHR(hrforLastWin32Error);
			}
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0007335C File Offset: 0x0007155C
		public virtual void CancelTimer()
		{
			if (!NativeMethods.CancelWaitableTimer(base.SafeWaitHandle))
			{
				int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
				ReleaseAssert.IsTrue(false);
				Marshal.ThrowExceptionForHR(hrforLastWin32Error);
			}
		}

		// Token: 0x040016B4 RID: 5812
		private WaitOrTimerCallback m_waitCallback;
	}
}
