using System;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000068 RID: 104
	public sealed class TimeLogicalType : LogicalType
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000A5D8 File Offset: 0x000087D8
		internal TimeLogicalType(IntPtr handle)
			: base(handle)
		{
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000A5E4 File Offset: 0x000087E4
		public bool IsAdjustedToUtc
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(TimeLogicalType.TimeLogicalType_IsAdjustedToUtc));
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000A600 File Offset: 0x00008800
		public TimeUnit TimeUnit
		{
			get
			{
				return ExceptionInfo.Return<TimeUnit>(this.Handle, new ExceptionInfo.GetFunction<TimeUnit>(TimeLogicalType.TimeLogicalType_TimeUnit));
			}
		}

		// Token: 0x060002C0 RID: 704
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr TimeLogicalType_IsAdjustedToUtc(IntPtr logicalType, [MarshalAs(UnmanagedType.I1)] out bool isAdjustedToUtc);

		// Token: 0x060002C1 RID: 705
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr TimeLogicalType_TimeUnit(IntPtr logicalType, out TimeUnit timeUnit);
	}
}
