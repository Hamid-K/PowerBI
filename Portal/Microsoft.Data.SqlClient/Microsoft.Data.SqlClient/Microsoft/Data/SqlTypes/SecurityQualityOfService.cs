using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Data.SqlTypes
{
	// Token: 0x02000019 RID: 25
	internal class SecurityQualityOfService : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x0000C12C File Offset: 0x0000A32C
		public SecurityQualityOfService(UnsafeNativeMethods.SecurityImpersonationLevel impersonationLevel, bool effectiveOnly, bool dynamicTrackingMode)
			: base(true)
		{
			this.Initialize(impersonationLevel, effectiveOnly, dynamicTrackingMode);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0000C13E File Offset: 0x0000A33E
		protected override bool ReleaseHandle()
		{
			if (this.m_hQos.IsAllocated)
			{
				this.m_hQos.Free();
			}
			this.handle = IntPtr.Zero;
			return true;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0000C164 File Offset: 0x0000A364
		internal void Initialize(UnsafeNativeMethods.SecurityImpersonationLevel impersonationLevel, bool effectiveOnly, bool dynamicTrackingMode)
		{
			this.m_qos.length = (uint)Marshal.SizeOf(typeof(UnsafeNativeMethods.SECURITY_QUALITY_OF_SERVICE));
			this.m_qos.impersonationLevel = (int)impersonationLevel;
			this.m_qos.effectiveOnly = ((effectiveOnly > false) ? 1 : 0);
			this.m_qos.contextDynamicTrackingMode = ((dynamicTrackingMode > false) ? 1 : 0);
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.m_hQos = GCHandle.Alloc(this.m_qos, GCHandleType.Pinned);
				intPtr = this.m_hQos.AddrOfPinnedObject();
				if (intPtr != IntPtr.Zero)
				{
					base.SetHandle(intPtr);
				}
			}
		}

		// Token: 0x04000044 RID: 68
		private UnsafeNativeMethods.SECURITY_QUALITY_OF_SERVICE m_qos;

		// Token: 0x04000045 RID: 69
		private GCHandle m_hQos;
	}
}
