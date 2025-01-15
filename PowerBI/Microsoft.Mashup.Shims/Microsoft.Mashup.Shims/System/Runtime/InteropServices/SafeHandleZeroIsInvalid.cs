using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200000C RID: 12
	public abstract class SafeHandleZeroIsInvalid : SafeHandle
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002205 File Offset: 0x00000405
		protected SafeHandleZeroIsInvalid(bool ownsHandle)
			: base(IntPtr.Zero, ownsHandle)
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002213 File Offset: 0x00000413
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
