using System;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000066 RID: 102
	public sealed class DecimalLogicalType : LogicalType
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x0000A588 File Offset: 0x00008788
		internal DecimalLogicalType(IntPtr handle)
			: base(handle)
		{
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000A594 File Offset: 0x00008794
		public int Precision
		{
			get
			{
				return ExceptionInfo.Return<int>(this.Handle, new ExceptionInfo.GetFunction<int>(DecimalLogicalType.DecimalLogicalType_Precision));
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000A5B0 File Offset: 0x000087B0
		public int Scale
		{
			get
			{
				return ExceptionInfo.Return<int>(this.Handle, new ExceptionInfo.GetFunction<int>(DecimalLogicalType.DecimalLogicalType_Scale));
			}
		}

		// Token: 0x060002BA RID: 698
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr DecimalLogicalType_Precision(IntPtr logicalType, out int precision);

		// Token: 0x060002BB RID: 699
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr DecimalLogicalType_Scale(IntPtr logicalType, out int scale);
	}
}
