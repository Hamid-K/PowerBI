using System;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x0200006B RID: 107
	public sealed class IntLogicalType : LogicalType
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000A6A4 File Offset: 0x000088A4
		internal IntLogicalType(IntPtr handle)
			: base(handle)
		{
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A6B0 File Offset: 0x000088B0
		public int BitWidth
		{
			get
			{
				return ExceptionInfo.Return<int>(this.Handle, new ExceptionInfo.GetFunction<int>(IntLogicalType.IntLogicalType_BitWidth));
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000A6CC File Offset: 0x000088CC
		public bool IsSigned
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(IntLogicalType.IntLogicalType_IsSigned));
			}
		}

		// Token: 0x060002CF RID: 719
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr IntLogicalType_BitWidth(IntPtr logicalType, out int bitWidth);

		// Token: 0x060002D0 RID: 720
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr IntLogicalType_IsSigned(IntPtr logicalType, [MarshalAs(UnmanagedType.I1)] out bool isSigned);
	}
}
