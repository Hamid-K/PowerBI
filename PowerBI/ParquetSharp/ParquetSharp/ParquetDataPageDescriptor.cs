using System;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000079 RID: 121
	[StructLayout(LayoutKind.Sequential)]
	public class ParquetDataPageDescriptor : IDisposable
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000C968 File Offset: 0x0000AB68
		public int AllocatedBytes
		{
			get
			{
				return this.AllocatedUnits * 4;
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C974 File Offset: 0x0000AB74
		public ParquetDataPageDescriptor()
		{
			this.Data = Marshal.AllocHGlobal(33554432);
			this.AllocatedUnits = 8388608;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000C998 File Offset: 0x0000AB98
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000C9A8 File Offset: 0x0000ABA8
		private void ReleaseUnmanagedResources()
		{
			if (this.Data != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.Data);
				this.Data = IntPtr.Zero;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
		private void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.ReleaseUnmanagedResources();
			this.disposed = true;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		~ParquetDataPageDescriptor()
		{
			this.ReleaseUnmanagedResources();
		}

		// Token: 0x040000DE RID: 222
		private const int PageSizeElements = 8388608;

		// Token: 0x040000DF RID: 223
		public IntPtr Data;

		// Token: 0x040000E0 RID: 224
		public int AllocatedUnits;

		// Token: 0x040000E1 RID: 225
		private bool disposed;
	}
}
