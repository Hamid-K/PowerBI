using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Data.SqlTypes
{
	// Token: 0x02000018 RID: 24
	internal class UnicodeString : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x0000C043 File Offset: 0x0000A243
		public UnicodeString(string path)
			: base(true)
		{
			this.Initialize(path);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0000C053 File Offset: 0x0000A253
		protected override bool ReleaseHandle()
		{
			if (this.handle == IntPtr.Zero)
			{
				return true;
			}
			Marshal.FreeHGlobal(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0000C080 File Offset: 0x0000A280
		private void Initialize(string path)
		{
			UnsafeNativeMethods.UNICODE_STRING unicode_STRING;
			unicode_STRING.length = (ushort)(path.Length * 2);
			unicode_STRING.maximumLength = (ushort)(path.Length * 2);
			unicode_STRING.buffer = path;
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<UnsafeNativeMethods.UNICODE_STRING>(unicode_STRING));
				if (intPtr != IntPtr.Zero)
				{
					base.SetHandle(intPtr);
				}
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr2 = base.DangerousGetHandle();
				Marshal.StructureToPtr<UnsafeNativeMethods.UNICODE_STRING>(unicode_STRING, intPtr2, false);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}
	}
}
