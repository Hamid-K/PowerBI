using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000035 RID: 53
	internal class ComHeap : IDisposable
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x000059C0 File Offset: 0x00003BC0
		~ComHeap()
		{
			this.Dispose(false);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000059F0 File Offset: 0x00003BF0
		public unsafe void* Alloc(int size)
		{
			if (this.allocs == null)
			{
				this.allocs = new List<IntPtr>();
			}
			this.allocs.Add(IntPtr.Zero);
			IntPtr intPtr = Marshal.AllocCoTaskMem(size);
			this.allocs[this.allocs.Count - 1] = intPtr;
			return intPtr.ToPointer();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005A48 File Offset: 0x00003C48
		public unsafe void* AllocArray(int length, int elementSize)
		{
			long num = (long)length * (long)elementSize;
			if (num > 2147483647L || num < 0L)
			{
				throw new InvalidOperationException();
			}
			return this.Alloc((int)num);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005A78 File Offset: 0x00003C78
		public unsafe char* AllocString([global::System.Runtime.CompilerServices.Nullable(1)] string value)
		{
			char* ptr = (char*)this.AllocArray(value.Length + 1, 2);
			for (int i = 0; i < value.Length; i++)
			{
				ptr[i] = value[i];
			}
			ptr[value.Length] = '\0';
			return ptr;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public unsafe byte* AllocBinary([global::System.Runtime.CompilerServices.Nullable(1)] byte[] value)
		{
			byte* ptr = (byte*)this.Alloc(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				ptr[i] = value[i];
			}
			return ptr;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public unsafe char* AllocBSTR([global::System.Runtime.CompilerServices.Nullable(1)] string value)
		{
			if (this.bstrs == null)
			{
				this.bstrs = new List<IntPtr>();
			}
			this.bstrs.Add(IntPtr.Zero);
			IntPtr intPtr = Marshal.StringToBSTR(value);
			this.bstrs[this.bstrs.Count - 1] = intPtr;
			return (char*)intPtr.ToPointer();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005B4B File Offset: 0x00003D4B
		public void Commit()
		{
			this.allocs = null;
			this.bstrs = null;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005B5B File Offset: 0x00003D5B
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005B64 File Offset: 0x00003D64
		private void Dispose(bool disposing)
		{
			if (this.allocs != null)
			{
				for (int i = 0; i < this.allocs.Count; i++)
				{
					Marshal.FreeCoTaskMem(this.allocs[i]);
				}
				this.allocs = null;
			}
			if (this.bstrs != null)
			{
				for (int j = 0; j < this.bstrs.Count; j++)
				{
					Marshal.FreeBSTR(this.bstrs[j]);
				}
				this.bstrs = null;
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000072 RID: 114
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private List<IntPtr> allocs;

		// Token: 0x04000073 RID: 115
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private List<IntPtr> bstrs;
	}
}
