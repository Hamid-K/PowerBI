using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000A4 RID: 164
	[NullableContext(1)]
	[Nullable(0)]
	public class Buffer : IDisposable
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x00011388 File Offset: 0x0000F588
		public Buffer(IntPtr data, long size)
			: this(Buffer.Make(data, size))
		{
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00011398 File Offset: 0x0000F598
		internal Buffer(IntPtr handle)
		{
			this.handle = new ParquetHandle(handle, new Action<IntPtr>(Buffer.Buffer_Free));
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000113B8 File Offset: 0x0000F5B8
		public long Capacity
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(Buffer.Buffer_Capacity));
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public IntPtr Data
		{
			get
			{
				return ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(Buffer.Buffer_Data));
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x000113F0 File Offset: 0x0000F5F0
		public long Size
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(Buffer.Buffer_Size));
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0001140C File Offset: 0x0000F60C
		internal ParquetHandle Handle
		{
			get
			{
				if (this.handle == null)
				{
					throw new ObjectDisposedException("Buffer");
				}
				return this.handle;
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001142C File Offset: 0x0000F62C
		public void Dispose()
		{
			if (this.handle != null)
			{
				this.handle.Dispose();
				this.handle = null;
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001144C File Offset: 0x0000F64C
		public byte[] ToArray()
		{
			byte[] array = new byte[this.Size];
			Marshal.Copy(this.Data, array, 0, array.Length);
			return array;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001147C File Offset: 0x0000F67C
		internal ParquetHandle Consume()
		{
			if (this.handle == null)
			{
				throw new ObjectDisposedException("Buffer");
			}
			ParquetHandle parquetHandle = this.handle;
			this.handle = null;
			return parquetHandle;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000114A4 File Offset: 0x0000F6A4
		private static IntPtr Make(IntPtr data, long size)
		{
			IntPtr intPtr;
			ExceptionInfo.Check(Buffer.Buffer_MakeFromPointer(data, size, out intPtr));
			return intPtr;
		}

		// Token: 0x06000500 RID: 1280
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Buffer_MakeFromPointer(IntPtr data, long size, out IntPtr buffer);

		// Token: 0x06000501 RID: 1281
		[DllImport("ParquetSharpNative")]
		private static extern void Buffer_Free(IntPtr buffer);

		// Token: 0x06000502 RID: 1282
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Buffer_Capacity(IntPtr buffer, out long capacity);

		// Token: 0x06000503 RID: 1283
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Buffer_Data(IntPtr buffer, out IntPtr data);

		// Token: 0x06000504 RID: 1284
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Buffer_Size(IntPtr buffer, out long size);

		// Token: 0x04000171 RID: 369
		[Nullable(2)]
		private ParquetHandle handle;
	}
}
