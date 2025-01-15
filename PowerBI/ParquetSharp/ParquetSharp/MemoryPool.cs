using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000076 RID: 118
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MemoryPool
	{
		// Token: 0x06000308 RID: 776 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		public static MemoryPool GetDefaultMemoryPool()
		{
			return new MemoryPool(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(MemoryPool.MemoryPool_Default_Memory_Pool)));
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000C8BC File Offset: 0x0000AABC
		public long BytesAllocated
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(MemoryPool.MemoryPool_Bytes_Allocated));
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		public long MaxMemory
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(MemoryPool.MemoryPool_Max_Memory));
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public string BackendName
		{
			get
			{
				return ExceptionInfo.ReturnString(this._handle, new ExceptionInfo.GetFunction<IntPtr>(MemoryPool.MemoryPool_Backend_Name), new Action<IntPtr>(MemoryPool.MemoryPool_Backend_Name_Free));
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C91C File Offset: 0x0000AB1C
		private MemoryPool(IntPtr handle)
		{
			this._handle = handle;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000C92C File Offset: 0x0000AB2C
		public void ReleaseUnused()
		{
			ExceptionInfo.Check(MemoryPool.MemoryPool_Release_Unused(this._handle));
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000C940 File Offset: 0x0000AB40
		public bool TestAvailableMemory(long testMemory)
		{
			return ExceptionInfo.Return<long, bool>(this._handle, testMemory, new ExceptionInfo.GetFunction<long, bool>(MemoryPool.MemoryPool_Test_Available_Memory));
		}

		// Token: 0x0600030F RID: 783
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr MemoryPool_Default_Memory_Pool(out IntPtr memoryPool);

		// Token: 0x06000310 RID: 784
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr MemoryPool_Bytes_Allocated(IntPtr memoryPool, out long bytesAllocated);

		// Token: 0x06000311 RID: 785
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr MemoryPool_Max_Memory(IntPtr memoryPool, out long maxMemory);

		// Token: 0x06000312 RID: 786
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr MemoryPool_Backend_Name(IntPtr memoryPool, out IntPtr backendName);

		// Token: 0x06000313 RID: 787
		[DllImport("ParquetSharpNative")]
		private static extern void MemoryPool_Backend_Name_Free(IntPtr backendName);

		// Token: 0x06000314 RID: 788
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr MemoryPool_Release_Unused(IntPtr memoryPool);

		// Token: 0x06000315 RID: 789
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr MemoryPool_Test_Available_Memory(IntPtr backendName, long testMemory, out bool available);

		// Token: 0x040000D9 RID: 217
		private readonly IntPtr _handle;
	}
}
