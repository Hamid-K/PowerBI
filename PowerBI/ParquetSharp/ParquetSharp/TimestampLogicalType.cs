using System;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000069 RID: 105
	public sealed class TimestampLogicalType : LogicalType
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000A61C File Offset: 0x0000881C
		internal TimestampLogicalType(IntPtr handle)
			: base(handle)
		{
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000A628 File Offset: 0x00008828
		public bool IsAdjustedToUtc
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(TimestampLogicalType.TimestampLogicalType_IsAdjustedToUtc));
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000A644 File Offset: 0x00008844
		public bool ForceSetConvertedType
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(TimestampLogicalType.TimestampLogicalType_ForceSetConvertedType));
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000A660 File Offset: 0x00008860
		public bool IsFromConvertedType
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(TimestampLogicalType.TimestampLogicalType_IsFromConvertedType));
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000A67C File Offset: 0x0000887C
		public TimeUnit TimeUnit
		{
			get
			{
				return ExceptionInfo.Return<TimeUnit>(this.Handle, new ExceptionInfo.GetFunction<TimeUnit>(TimestampLogicalType.TimestampLogicalType_TimeUnit));
			}
		}

		// Token: 0x060002C7 RID: 711
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr TimestampLogicalType_IsAdjustedToUtc(IntPtr logicalType, [MarshalAs(UnmanagedType.I1)] out bool isAdjustedToUtc);

		// Token: 0x060002C8 RID: 712
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr TimestampLogicalType_ForceSetConvertedType(IntPtr logicalType, [MarshalAs(UnmanagedType.I1)] out bool forceSetConvertedType);

		// Token: 0x060002C9 RID: 713
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr TimestampLogicalType_IsFromConvertedType(IntPtr logicalType, [MarshalAs(UnmanagedType.I1)] out bool isFromConvertedType);

		// Token: 0x060002CA RID: 714
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr TimestampLogicalType_TimeUnit(IntPtr logicalType, out TimeUnit timeUnit);
	}
}
