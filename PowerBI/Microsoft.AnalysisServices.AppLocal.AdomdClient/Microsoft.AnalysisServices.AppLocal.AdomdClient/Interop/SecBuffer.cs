using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x0200013A RID: 314
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SecBuffer
	{
		// Token: 0x06000FEC RID: 4076 RVA: 0x000366F3 File Offset: 0x000348F3
		public SecBuffer(int cbBuffer, int BufferType, IntPtr pvBuffer)
		{
			this.cbBuffer = cbBuffer;
			this.BufferType = BufferType;
			this.pvBuffer = pvBuffer;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003670A File Offset: 0x0003490A
		public SecBuffer(IntPtr ptr)
		{
			this.cbBuffer = Marshal.ReadInt32(ptr, 0);
			this.BufferType = Marshal.ReadInt32(ptr, 4);
			this.pvBuffer = Marshal.ReadIntPtr(ptr, 8);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00036733 File Offset: 0x00034933
		public void CopyTo(IntPtr ptr)
		{
			Marshal.WriteInt32(ptr, 0, this.cbBuffer);
			Marshal.WriteInt32(ptr, 4, this.BufferType);
			Marshal.WriteIntPtr(ptr, 8, this.pvBuffer);
		}

		// Token: 0x04000AE4 RID: 2788
		public static readonly int Size = Marshal.SizeOf(typeof(SecBuffer));

		// Token: 0x04000AE5 RID: 2789
		public int cbBuffer;

		// Token: 0x04000AE6 RID: 2790
		public int BufferType;

		// Token: 0x04000AE7 RID: 2791
		public IntPtr pvBuffer;
	}
}
