using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x0200012F RID: 303
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SecBuffer
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x00038FF7 File Offset: 0x000371F7
		public SecBuffer(int cbBuffer, int BufferType, IntPtr pvBuffer)
		{
			this.cbBuffer = cbBuffer;
			this.BufferType = BufferType;
			this.pvBuffer = pvBuffer;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0003900E File Offset: 0x0003720E
		public SecBuffer(IntPtr ptr)
		{
			this.cbBuffer = Marshal.ReadInt32(ptr, 0);
			this.BufferType = Marshal.ReadInt32(ptr, 4);
			this.pvBuffer = Marshal.ReadIntPtr(ptr, 8);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00039037 File Offset: 0x00037237
		public void CopyTo(IntPtr ptr)
		{
			Marshal.WriteInt32(ptr, 0, this.cbBuffer);
			Marshal.WriteInt32(ptr, 4, this.BufferType);
			Marshal.WriteIntPtr(ptr, 8, this.pvBuffer);
		}

		// Token: 0x04000A9D RID: 2717
		public static readonly int Size = Marshal.SizeOf(typeof(SecBuffer));

		// Token: 0x04000A9E RID: 2718
		public int cbBuffer;

		// Token: 0x04000A9F RID: 2719
		public int BufferType;

		// Token: 0x04000AA0 RID: 2720
		public IntPtr pvBuffer;
	}
}
