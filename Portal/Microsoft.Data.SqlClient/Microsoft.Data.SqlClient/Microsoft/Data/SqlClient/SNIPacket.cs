using System;
using System.Runtime.InteropServices;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000B6 RID: 182
	internal sealed class SNIPacket : SafeHandle
	{
		// Token: 0x06000CE2 RID: 3298 RVA: 0x00027637 File Offset: 0x00025837
		internal SNIPacket(SafeHandle sniHandle)
			: base(IntPtr.Zero, true)
		{
			SNINativeMethodWrapper.SNIPacketAllocate(sniHandle, SNINativeMethodWrapper.IOType.WRITE, ref this.handle);
			if (IntPtr.Zero == this.handle)
			{
				throw SQL.SNIPacketAllocationFailure();
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0002743C File Offset: 0x0002563C
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002766C File Offset: 0x0002586C
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SNINativeMethodWrapper.SNIPacketRelease(handle);
			}
			return true;
		}
	}
}
