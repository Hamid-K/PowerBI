using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x0200008A RID: 138
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Statistics : IDisposable
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000E410 File Offset: 0x0000C610
		[NullableContext(2)]
		internal static Statistics Create(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			ParquetHandle parquetHandle = new ParquetHandle(handle, new Action<IntPtr>(Statistics.Statistics_Free));
			Statistics statistics2;
			try
			{
				PhysicalType physicalType = ExceptionInfo.Return<PhysicalType>(handle, new ExceptionInfo.GetFunction<PhysicalType>(Statistics.Statistics_Physical_Type));
				Statistics statistics;
				switch (physicalType)
				{
				case PhysicalType.Boolean:
					statistics = new Statistics<bool>(parquetHandle);
					break;
				case PhysicalType.Int32:
					statistics = new Statistics<int>(parquetHandle);
					break;
				case PhysicalType.Int64:
					statistics = new Statistics<long>(parquetHandle);
					break;
				case PhysicalType.Int96:
					statistics = new Statistics<Int96>(parquetHandle);
					break;
				case PhysicalType.Float:
					statistics = new Statistics<float>(parquetHandle);
					break;
				case PhysicalType.Double:
					statistics = new Statistics<double>(parquetHandle);
					break;
				case PhysicalType.ByteArray:
					statistics = new Statistics<ByteArray>(parquetHandle);
					break;
				case PhysicalType.FixedLenByteArray:
					statistics = new Statistics<FixedLenByteArray>(parquetHandle);
					break;
				default:
					throw new NotSupportedException(string.Format("Physical type {0} is not supported", physicalType));
				}
				statistics2 = statistics;
			}
			catch
			{
				parquetHandle.Dispose();
				throw;
			}
			return statistics2;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000E518 File Offset: 0x0000C718
		internal Statistics(ParquetHandle handle)
		{
			this.Handle = handle;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000E528 File Offset: 0x0000C728
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000E538 File Offset: 0x0000C738
		public long DistinctCount
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(Statistics.Statistics_Distinct_Count));
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000E554 File Offset: 0x0000C754
		public bool HasMinMax
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(Statistics.Statistics_HasMinMax));
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000E570 File Offset: 0x0000C770
		public long NullCount
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(Statistics.Statistics_Null_Count));
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000E58C File Offset: 0x0000C78C
		public long NumValues
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(Statistics.Statistics_Num_Values));
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		public PhysicalType PhysicalType
		{
			get
			{
				return ExceptionInfo.Return<PhysicalType>(this.Handle, new ExceptionInfo.GetFunction<PhysicalType>(Statistics.Statistics_Physical_Type));
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003C6 RID: 966
		public abstract object MinUntyped { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003C7 RID: 967
		public abstract object MaxUntyped { get; }

		// Token: 0x060003C8 RID: 968
		[DllImport("ParquetSharpNative")]
		private static extern void Statistics_Free(IntPtr statistics);

		// Token: 0x060003C9 RID: 969
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Statistics_Distinct_Count(IntPtr statistics, out long distinctCount);

		// Token: 0x060003CA RID: 970
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Statistics_HasMinMax(IntPtr statistics, [MarshalAs(UnmanagedType.I1)] out bool hasMinMax);

		// Token: 0x060003CB RID: 971
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Statistics_Null_Count(IntPtr statistics, out long nullCount);

		// Token: 0x060003CC RID: 972
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Statistics_Num_Values(IntPtr statistics, out long numValues);

		// Token: 0x060003CD RID: 973
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Statistics_Physical_Type(IntPtr statistics, out PhysicalType physicalType);

		// Token: 0x060003CE RID: 974
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Min_Bool(IntPtr statistics, [MarshalAs(UnmanagedType.I1)] out bool min);

		// Token: 0x060003CF RID: 975
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Min_Int32(IntPtr statistics, out int min);

		// Token: 0x060003D0 RID: 976
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Min_Int64(IntPtr statistics, out long min);

		// Token: 0x060003D1 RID: 977
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Min_Int96(IntPtr statistics, out Int96 min);

		// Token: 0x060003D2 RID: 978
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Min_Float(IntPtr statistics, out float min);

		// Token: 0x060003D3 RID: 979
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Min_Double(IntPtr statistics, out double min);

		// Token: 0x060003D4 RID: 980
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Min_ByteArray(IntPtr statistics, out ByteArray min);

		// Token: 0x060003D5 RID: 981
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Min_FLBA(IntPtr statistics, out FixedLenByteArray min);

		// Token: 0x060003D6 RID: 982
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Max_Bool(IntPtr statistics, [MarshalAs(UnmanagedType.I1)] out bool max);

		// Token: 0x060003D7 RID: 983
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Max_Int32(IntPtr statistics, out int max);

		// Token: 0x060003D8 RID: 984
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Max_Int64(IntPtr statistics, out long max);

		// Token: 0x060003D9 RID: 985
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Max_Int96(IntPtr statistics, out Int96 max);

		// Token: 0x060003DA RID: 986
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Max_Float(IntPtr statistics, out float max);

		// Token: 0x060003DB RID: 987
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Max_Double(IntPtr statistics, out double max);

		// Token: 0x060003DC RID: 988
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Max_ByteArray(IntPtr statistics, out ByteArray max);

		// Token: 0x060003DD RID: 989
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedStatistics_Max_FLBA(IntPtr statistics, out FixedLenByteArray max);

		// Token: 0x0400011C RID: 284
		internal readonly ParquetHandle Handle;
	}
}
