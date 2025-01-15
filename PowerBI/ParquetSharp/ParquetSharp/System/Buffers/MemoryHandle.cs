using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x020000E6 RID: 230
	internal struct MemoryHandle : IDisposable
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x00022FDC File Offset: 0x000211DC
		[CLSCompliant(false)]
		public unsafe MemoryHandle(void* pointer, GCHandle handle = default(GCHandle), IPinnable pinnable = null)
		{
			this._pointer = pointer;
			this._handle = handle;
			this._pinnable = pinnable;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00022FF4 File Offset: 0x000211F4
		[CLSCompliant(false)]
		public unsafe void* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00022FFC File Offset: 0x000211FC
		public void Dispose()
		{
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
			if (this._pinnable != null)
			{
				this._pinnable.Unpin();
				this._pinnable = null;
			}
			this._pointer = null;
		}

		// Token: 0x04000262 RID: 610
		private unsafe void* _pointer;

		// Token: 0x04000263 RID: 611
		private GCHandle _handle;

		// Token: 0x04000264 RID: 612
		private IPinnable _pinnable;
	}
}
