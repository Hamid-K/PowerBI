using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FFE RID: 8190
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal abstract class SafeHandleWithBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600C795 RID: 51093 RVA: 0x0000EDD3 File Offset: 0x0000CFD3
		protected SafeHandleWithBuffer()
			: base(true)
		{
		}

		// Token: 0x17003057 RID: 12375
		// (get) Token: 0x0600C796 RID: 51094 RVA: 0x0027B6D9 File Offset: 0x002798D9
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero && this.m_dataBuffer == IntPtr.Zero;
			}
		}

		// Token: 0x17003058 RID: 12376
		// (get) Token: 0x0600C797 RID: 51095 RVA: 0x0027B6FF File Offset: 0x002798FF
		// (set) Token: 0x0600C798 RID: 51096 RVA: 0x0027B707 File Offset: 0x00279907
		internal IntPtr DataBuffer
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.m_dataBuffer;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this.m_dataBuffer = value;
			}
		}

		// Token: 0x0600C799 RID: 51097 RVA: 0x0027B710 File Offset: 0x00279910
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual bool ReleaseBuffer()
		{
			Marshal.FreeCoTaskMem(this.m_dataBuffer);
			return true;
		}

		// Token: 0x0600C79A RID: 51098 RVA: 0x0027B720 File Offset: 0x00279920
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected sealed override bool ReleaseHandle()
		{
			bool flag = false;
			if (this.handle != IntPtr.Zero)
			{
				flag = this.ReleaseNativeHandle();
			}
			if (this.m_dataBuffer != IntPtr.Zero)
			{
				flag &= this.ReleaseBuffer();
			}
			return flag;
		}

		// Token: 0x0600C79B RID: 51099
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseNativeHandle();

		// Token: 0x040065F3 RID: 26099
		private IntPtr m_dataBuffer;
	}
}
