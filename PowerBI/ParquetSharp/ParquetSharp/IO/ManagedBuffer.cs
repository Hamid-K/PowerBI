using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000A8 RID: 168
	public static class ManagedBuffer
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x000115F0 File Offset: 0x0000F7F0
		[NullableContext(1)]
		public static Buffer New(byte[] buffer, long? size = null)
		{
			GCHandle gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			Buffer buffer2;
			try
			{
				IntPtr intPtr;
				ExceptionInfo.Check(ManagedBuffer.Buffer_Make(gchandle.AddrOfPinnedObject(), size ?? ((long)buffer.Length), GCHandle.ToIntPtr(gchandle), in ManagedBuffer.vTable, out intPtr));
				buffer2 = new Buffer(intPtr);
			}
			catch
			{
				gchandle.Free();
				throw;
			}
			return buffer2;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00011668 File Offset: 0x0000F868
		private static void DeleteWrapper(IntPtr thisHandle)
		{
			GCHandle.FromIntPtr(thisHandle).Free();
		}

		// Token: 0x06000515 RID: 1301
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Buffer_Make(IntPtr data, long size, IntPtr thisHandle, in ManagedBuffer.VTable vTable, out IntPtr buffer);

		// Token: 0x04000172 RID: 370
		private static readonly ManagedBuffer.VTable vTable = new ManagedBuffer.VTable
		{
			Delete = new ManagedBuffer.DeleteCallback(ManagedBuffer.DeleteWrapper)
		};

		// Token: 0x02000135 RID: 309
		// (Invoke) Token: 0x060009DB RID: 2523
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate void DeleteCallback(IntPtr thisHandle);

		// Token: 0x02000136 RID: 310
		internal struct VTable
		{
			// Token: 0x04000320 RID: 800
			[Nullable(1)]
			public ManagedBuffer.DeleteCallback Delete;
		}
	}
}
