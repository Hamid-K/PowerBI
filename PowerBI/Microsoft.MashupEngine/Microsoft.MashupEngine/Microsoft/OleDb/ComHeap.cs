using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001E85 RID: 7813
	internal class ComHeap : IDisposable
	{
		// Token: 0x0600C108 RID: 49416 RVA: 0x0026D398 File Offset: 0x0026B598
		~ComHeap()
		{
			this.Dispose(false);
		}

		// Token: 0x0600C109 RID: 49417 RVA: 0x0026D3C8 File Offset: 0x0026B5C8
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

		// Token: 0x0600C10A RID: 49418 RVA: 0x0026D420 File Offset: 0x0026B620
		public unsafe void* AllocArray(int length, int elementSize)
		{
			long num = (long)length * (long)elementSize;
			if (num > 2147483647L || num < 0L)
			{
				throw new InvalidOperationException();
			}
			return this.Alloc((int)num);
		}

		// Token: 0x0600C10B RID: 49419 RVA: 0x0026D450 File Offset: 0x0026B650
		public unsafe char* AllocString(string value)
		{
			char* ptr = (char*)this.AllocArray(value.Length + 1, 2);
			for (int i = 0; i < value.Length; i++)
			{
				ptr[i] = value[i];
			}
			ptr[value.Length] = '\0';
			return ptr;
		}

		// Token: 0x0600C10C RID: 49420 RVA: 0x0026D49C File Offset: 0x0026B69C
		public unsafe byte* AllocBinary(byte[] value)
		{
			byte* ptr = (byte*)this.Alloc(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				ptr[i] = value[i];
			}
			return ptr;
		}

		// Token: 0x0600C10D RID: 49421 RVA: 0x0026D4CC File Offset: 0x0026B6CC
		public unsafe char* AllocBSTR(string value)
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

		// Token: 0x0600C10E RID: 49422 RVA: 0x0026D523 File Offset: 0x0026B723
		public void Commit()
		{
			this.allocs = null;
			this.bstrs = null;
		}

		// Token: 0x0600C10F RID: 49423 RVA: 0x0026D533 File Offset: 0x0026B733
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600C110 RID: 49424 RVA: 0x0026D53C File Offset: 0x0026B73C
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

		// Token: 0x04006183 RID: 24963
		private List<IntPtr> allocs;

		// Token: 0x04006184 RID: 24964
		private List<IntPtr> bstrs;
	}
}
