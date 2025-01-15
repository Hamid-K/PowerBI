using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000028 RID: 40
	public struct MemoryHandle : IDisposable
	{
		// Token: 0x060001DA RID: 474 RVA: 0x0000B77A File Offset: 0x0000997A
		[CLSCompliant(false)]
		public unsafe MemoryHandle(void* pointer, GCHandle handle = default(GCHandle), IPinnable pinnable = null)
		{
			this._pointer = pointer;
			this._handle = handle;
			this._pinnable = pinnable;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000B791 File Offset: 0x00009991
		[CLSCompliant(false)]
		public unsafe void* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000B799 File Offset: 0x00009999
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

		// Token: 0x0400008A RID: 138
		private unsafe void* _pointer;

		// Token: 0x0400008B RID: 139
		private GCHandle _handle;

		// Token: 0x0400008C RID: 140
		private IPinnable _pinnable;
	}
}
