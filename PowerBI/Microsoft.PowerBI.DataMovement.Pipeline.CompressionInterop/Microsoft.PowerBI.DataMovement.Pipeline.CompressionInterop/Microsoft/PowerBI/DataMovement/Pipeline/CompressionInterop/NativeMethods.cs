using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.CompressionInterop
{
	// Token: 0x02000007 RID: 7
	internal static class NativeMethods
	{
		// Token: 0x06000007 RID: 7
		[DllImport("Microsoft.PowerBI.DataMovement.Pipeline.XPress9.dll", CallingConvention = CallingConvention.Cdecl)]
		internal unsafe static extern int Compress(byte* inBuffer, uint inBufferSize, byte* outBuffer, uint* outBufferSize, uint level);

		// Token: 0x06000008 RID: 8
		[DllImport("Microsoft.PowerBI.DataMovement.Pipeline.XPress9.dll", CallingConvention = CallingConvention.Cdecl)]
		internal unsafe static extern int Decompress(byte* inBuffer, uint inBufferSize, uint originalSize, byte* outBuffer, uint level);

		// Token: 0x06000009 RID: 9
		[DllImport("Microsoft.PowerBI.DataMovement.Pipeline.XPress9.dll", CallingConvention = CallingConvention.Cdecl)]
		internal unsafe static extern byte* GetErrorMessage();

		// Token: 0x04000011 RID: 17
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private const string c_xPress9Dll = "Microsoft.PowerBI.DataMovement.Pipeline.XPress9.dll";
	}
}
