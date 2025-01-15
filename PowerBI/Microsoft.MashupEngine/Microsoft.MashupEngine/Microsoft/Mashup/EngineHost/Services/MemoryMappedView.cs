using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A17 RID: 6679
	internal class MemoryMappedView : IDisposable
	{
		// Token: 0x0600A901 RID: 43265 RVA: 0x0022F7C7 File Offset: 0x0022D9C7
		public MemoryMappedView(SafeHandle fileMappingHandle, ulong offset, uint length)
		{
			this.length = length;
			if (length > 0U)
			{
				this.viewHandle = new MemoryMappedView.SafeViewHandle();
				this.viewHandle.Map(fileMappingHandle, offset, length);
			}
		}

		// Token: 0x17002AF4 RID: 10996
		// (get) Token: 0x0600A902 RID: 43266 RVA: 0x0022F7F3 File Offset: 0x0022D9F3
		public uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x0600A903 RID: 43267 RVA: 0x0022F7FB File Offset: 0x0022D9FB
		public void Dispose()
		{
			if (this.viewHandle != null)
			{
				this.viewHandle.Dispose();
				this.viewHandle = null;
			}
		}

		// Token: 0x0600A904 RID: 43268 RVA: 0x0022F818 File Offset: 0x0022DA18
		public void Read(int offset, int count, byte[] buffer, int bufferOffset)
		{
			if ((long)(offset + count) > (long)((ulong)this.length) || bufferOffset + count > buffer.Length || offset < 0 || count < 0 || bufferOffset < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			Marshal.Copy(MemoryMappedView.OffsetPointer(this.viewHandle.Handle, offset), buffer, bufferOffset, count);
		}

		// Token: 0x0600A905 RID: 43269 RVA: 0x0022F868 File Offset: 0x0022DA68
		public void Write(int offset, int count, byte[] buffer, int bufferOffset)
		{
			if ((long)(offset + count) > (long)((ulong)this.length) || bufferOffset + count > buffer.Length || offset < 0 || count < 0 || bufferOffset < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			IntPtr intPtr = MemoryMappedView.OffsetPointer(this.viewHandle.Handle, offset);
			Marshal.Copy(buffer, bufferOffset, intPtr, count);
		}

		// Token: 0x0600A906 RID: 43270 RVA: 0x0022F8BC File Offset: 0x0022DABC
		private static IntPtr OffsetPointer(IntPtr value, int offset)
		{
			int size = IntPtr.Size;
			if (size == 4)
			{
				return new IntPtr((int)value + offset);
			}
			if (size != 8)
			{
				throw new InvalidOperationException();
			}
			return new IntPtr((long)value + (long)offset);
		}

		// Token: 0x0600A907 RID: 43271
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		// Token: 0x0600A908 RID: 43272
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr MapViewOfFile(SafeHandle hFileMappingObject, FileMappingAccess dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

		// Token: 0x040057F4 RID: 22516
		private readonly uint length;

		// Token: 0x040057F5 RID: 22517
		private MemoryMappedView.SafeViewHandle viewHandle;

		// Token: 0x02001A18 RID: 6680
		private class SafeViewHandle : SafeHandle
		{
			// Token: 0x0600A909 RID: 43273 RVA: 0x0022F8FB File Offset: 0x0022DAFB
			public SafeViewHandle()
				: base(IntPtr.Zero, true)
			{
			}

			// Token: 0x17002AF5 RID: 10997
			// (get) Token: 0x0600A90A RID: 43274 RVA: 0x0022F909 File Offset: 0x0022DB09
			public IntPtr Handle
			{
				get
				{
					if (this.handle == IntPtr.Zero)
					{
						throw new InvalidOperationException();
					}
					return this.handle;
				}
			}

			// Token: 0x0600A90B RID: 43275 RVA: 0x0022F92C File Offset: 0x0022DB2C
			public void Map(SafeHandle safeHandle, ulong offset, uint length)
			{
				if (this.handle != IntPtr.Zero)
				{
					throw new InvalidOperationException("Handle is not null.", new Win32Exception());
				}
				uint num = (uint)(offset >> 32);
				uint num2 = (uint)offset;
				IntPtr intPtr = MemoryMappedView.MapViewOfFile(safeHandle, FileMappingAccess.FILE_MAP_ALL_ACCESS, num, num2, length);
				if (intPtr == IntPtr.Zero)
				{
					throw new InvalidOperationException("MapViewOfFile failed.", new Win32Exception());
				}
				base.SetHandle(intPtr);
			}

			// Token: 0x17002AF6 RID: 10998
			// (get) Token: 0x0600A90C RID: 43276 RVA: 0x0022F994 File Offset: 0x0022DB94
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			// Token: 0x0600A90D RID: 43277 RVA: 0x0022F9A6 File Offset: 0x0022DBA6
			protected override bool ReleaseHandle()
			{
				return MemoryMappedView.UnmapViewOfFile(this.handle);
			}
		}
	}
}
