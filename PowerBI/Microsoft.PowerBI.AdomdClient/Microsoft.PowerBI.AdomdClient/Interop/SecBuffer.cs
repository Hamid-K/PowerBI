using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x0200013A RID: 314
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SecBuffer
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x000363C3 File Offset: 0x000345C3
		public SecBuffer(int cbBuffer, int BufferType, IntPtr pvBuffer)
		{
			this.cbBuffer = cbBuffer;
			this.BufferType = BufferType;
			this.pvBuffer = pvBuffer;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000363DA File Offset: 0x000345DA
		public SecBuffer(IntPtr ptr)
		{
			this.cbBuffer = Marshal.ReadInt32(ptr, 0);
			this.BufferType = Marshal.ReadInt32(ptr, 4);
			this.pvBuffer = Marshal.ReadIntPtr(ptr, 8);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00036403 File Offset: 0x00034603
		public void CopyTo(IntPtr ptr)
		{
			Marshal.WriteInt32(ptr, 0, this.cbBuffer);
			Marshal.WriteInt32(ptr, 4, this.BufferType);
			Marshal.WriteIntPtr(ptr, 8, this.pvBuffer);
		}

		// Token: 0x04000AD7 RID: 2775
		public static readonly int Size = Marshal.SizeOf(typeof(SecBuffer));

		// Token: 0x04000AD8 RID: 2776
		public int cbBuffer;

		// Token: 0x04000AD9 RID: 2777
		public int BufferType;

		// Token: 0x04000ADA RID: 2778
		public IntPtr pvBuffer;
	}
}
