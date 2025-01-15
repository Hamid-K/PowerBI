using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000061 RID: 97
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class LogicalType : IDisposable, IEquatable<LogicalType>
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000A180 File Offset: 0x00008380
		protected LogicalType(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(LogicalType.LogicalType_Free));
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000A1B0 File Offset: 0x000083B0
		public LogicalTypeEnum Type
		{
			get
			{
				return ExceptionInfo.Return<LogicalTypeEnum>(this.Handle, new ExceptionInfo.GetFunction<LogicalTypeEnum>(LogicalType.LogicalType_Type));
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A1CC File Offset: 0x000083CC
		[NullableContext(2)]
		public bool Equals(LogicalType other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.Handle.IntPtr == IntPtr.Zero || other.Handle.IntPtr == IntPtr.Zero)
			{
				return false;
			}
			bool flag = ExceptionInfo.Return<IntPtr, bool>(this.Handle, other.Handle.IntPtr, new ExceptionInfo.GetFunction<IntPtr, bool>(LogicalType.LogicalType_Equals));
			GC.KeepAlive(other.Handle);
			return flag;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A248 File Offset: 0x00008448
		public override string ToString()
		{
			return ExceptionInfo.ReturnString(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(LogicalType.LogicalType_ToString), new Action<IntPtr>(LogicalType.LogicalType_ToString_Free));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A270 File Offset: 0x00008470
		public static LogicalType String()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_String)));
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A288 File Offset: 0x00008488
		public static LogicalType Map()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_Map)));
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public static LogicalType List()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_List)));
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A2B8 File Offset: 0x000084B8
		public static LogicalType Enum()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_Enum)));
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A2D0 File Offset: 0x000084D0
		public static LogicalType Decimal(int precision, int scale = 0)
		{
			return LogicalType.Create(ExceptionInfo.Return<int, int, IntPtr>(precision, scale, new ExceptionInfo.GetAction<int, int, IntPtr>(LogicalType.LogicalType_Decimal)));
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A2EC File Offset: 0x000084EC
		public static LogicalType Date()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_Date)));
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A304 File Offset: 0x00008504
		public static LogicalType Time(bool isAdjustedToUtc, TimeUnit timeUnit)
		{
			return LogicalType.Create(ExceptionInfo.Return<bool, TimeUnit, IntPtr>(isAdjustedToUtc, timeUnit, new ExceptionInfo.GetAction<bool, TimeUnit, IntPtr>(LogicalType.LogicalType_Time)));
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A320 File Offset: 0x00008520
		public static LogicalType Timestamp(bool isAdjustedToUtc, TimeUnit timeUnit)
		{
			return LogicalType.Create(ExceptionInfo.Return<bool, TimeUnit, bool, IntPtr>(isAdjustedToUtc, timeUnit, false, new ExceptionInfo.GetAction<bool, TimeUnit, bool, IntPtr>(LogicalType.LogicalType_Timestamp)));
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A33C File Offset: 0x0000853C
		public static LogicalType Timestamp(bool isAdjustedToUtc, TimeUnit timeUnit, bool forceSetConvertedType)
		{
			return LogicalType.Create(ExceptionInfo.Return<bool, TimeUnit, bool, IntPtr>(isAdjustedToUtc, timeUnit, forceSetConvertedType, new ExceptionInfo.GetAction<bool, TimeUnit, bool, IntPtr>(LogicalType.LogicalType_Timestamp)));
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A358 File Offset: 0x00008558
		public static LogicalType Interval()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_Interval)));
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A370 File Offset: 0x00008570
		public static LogicalType Int(int bitWidth, bool isSigned)
		{
			return LogicalType.Create(ExceptionInfo.Return<int, bool, IntPtr>(bitWidth, isSigned, new ExceptionInfo.GetAction<int, bool, IntPtr>(LogicalType.LogicalType_Int)));
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A38C File Offset: 0x0000858C
		public static LogicalType Null()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_Null)));
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public static LogicalType Json()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_JSON)));
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A3BC File Offset: 0x000085BC
		public static LogicalType Bson()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_BSON)));
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A3D4 File Offset: 0x000085D4
		public static LogicalType Uuid()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_UUID)));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A3EC File Offset: 0x000085EC
		public static LogicalType None()
		{
			return LogicalType.Create(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(LogicalType.LogicalType_None)));
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A404 File Offset: 0x00008604
		internal static LogicalType Create(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			LogicalTypeEnum logicalTypeEnum = ExceptionInfo.Return<LogicalTypeEnum>(handle, new ExceptionInfo.GetFunction<LogicalTypeEnum>(LogicalType.LogicalType_Type));
			LogicalType logicalType;
			switch (logicalTypeEnum)
			{
			case LogicalTypeEnum.String:
				logicalType = new StringLogicalType(handle);
				break;
			case LogicalTypeEnum.Map:
				logicalType = new MapLogicalType(handle);
				break;
			case LogicalTypeEnum.List:
				logicalType = new ListLogicalType(handle);
				break;
			case LogicalTypeEnum.Enum:
				logicalType = new EnumLogicalType(handle);
				break;
			case LogicalTypeEnum.Decimal:
				logicalType = new DecimalLogicalType(handle);
				break;
			case LogicalTypeEnum.Date:
				logicalType = new DateLogicalType(handle);
				break;
			case LogicalTypeEnum.Time:
				logicalType = new TimeLogicalType(handle);
				break;
			case LogicalTypeEnum.Timestamp:
				logicalType = new TimestampLogicalType(handle);
				break;
			case LogicalTypeEnum.Interval:
				logicalType = new IntervalLogicalType(handle);
				break;
			case LogicalTypeEnum.Int:
				logicalType = new IntLogicalType(handle);
				break;
			case LogicalTypeEnum.Nil:
				logicalType = new NullLogicalType(handle);
				break;
			case LogicalTypeEnum.Json:
				logicalType = new JsonLogicalType(handle);
				break;
			case LogicalTypeEnum.Bson:
				logicalType = new BsonLogicalType(handle);
				break;
			case LogicalTypeEnum.Uuid:
				logicalType = new UuidLogicalType(handle);
				break;
			case LogicalTypeEnum.None:
				logicalType = new NoneLogicalType(handle);
				break;
			default:
				throw new ArgumentOutOfRangeException(string.Format("unknown logical type {0}", logicalTypeEnum));
			}
			return logicalType;
		}

		// Token: 0x0600029F RID: 671
		[DllImport("ParquetSharpNative")]
		private static extern void LogicalType_Free(IntPtr logicalType);

		// Token: 0x060002A0 RID: 672
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Type(IntPtr logicalType, out LogicalTypeEnum type);

		// Token: 0x060002A1 RID: 673
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Equals(IntPtr left, IntPtr right, [MarshalAs(UnmanagedType.I1)] out bool equals);

		// Token: 0x060002A2 RID: 674
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_ToString(IntPtr logicalType, out IntPtr toString);

		// Token: 0x060002A3 RID: 675
		[DllImport("ParquetSharpNative")]
		private static extern void LogicalType_ToString_Free(IntPtr toString);

		// Token: 0x060002A4 RID: 676
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_String(out IntPtr logicalType);

		// Token: 0x060002A5 RID: 677
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Map(out IntPtr logicalType);

		// Token: 0x060002A6 RID: 678
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_List(out IntPtr logicalType);

		// Token: 0x060002A7 RID: 679
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Enum(out IntPtr logicalType);

		// Token: 0x060002A8 RID: 680
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Decimal(int precision, int scale, out IntPtr logicalType);

		// Token: 0x060002A9 RID: 681
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Date(out IntPtr logicalType);

		// Token: 0x060002AA RID: 682
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Time([MarshalAs(UnmanagedType.I1)] bool isAdjustedToUtc, TimeUnit timeUnit, out IntPtr logicalType);

		// Token: 0x060002AB RID: 683
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Timestamp([MarshalAs(UnmanagedType.I1)] bool isAdjustedToUtc, TimeUnit timeUnit, bool forceSetConvertedType, out IntPtr logicalType);

		// Token: 0x060002AC RID: 684
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Interval(out IntPtr logicalType);

		// Token: 0x060002AD RID: 685
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Int(int bitWidth, [MarshalAs(UnmanagedType.I1)] bool isSigned, out IntPtr logicalType);

		// Token: 0x060002AE RID: 686
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_Null(out IntPtr logicalType);

		// Token: 0x060002AF RID: 687
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_JSON(out IntPtr logicalType);

		// Token: 0x060002B0 RID: 688
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_BSON(out IntPtr logicalType);

		// Token: 0x060002B1 RID: 689
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_UUID(out IntPtr logicalType);

		// Token: 0x060002B2 RID: 690
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LogicalType_None(out IntPtr logicalType);

		// Token: 0x040000D2 RID: 210
		internal readonly ParquetHandle Handle;
	}
}
