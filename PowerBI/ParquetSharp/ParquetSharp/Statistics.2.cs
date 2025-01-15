using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200008B RID: 139
	public sealed class Statistics<[IsUnmanaged] TValue> : Statistics where TValue : struct
	{
		// Token: 0x060003DE RID: 990 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		[NullableContext(1)]
		internal Statistics(ParquetHandle handle)
			: base(handle)
		{
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		[Nullable(1)]
		public override object MinUntyped
		{
			[NullableContext(1)]
			get
			{
				return this.Min;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		[Nullable(1)]
		public override object MaxUntyped
		{
			[NullableContext(1)]
			get
			{
				return this.Max;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000E5F0 File Offset: 0x0000C7F0
		public TValue Min
		{
			get
			{
				Type typeFromHandle = typeof(TValue);
				if (typeFromHandle == typeof(bool))
				{
					return (TValue)((object)ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(Statistics.TypedStatistics_Min_Bool)));
				}
				if (typeFromHandle == typeof(int))
				{
					return (TValue)((object)ExceptionInfo.Return<int>(this.Handle, new ExceptionInfo.GetFunction<int>(Statistics.TypedStatistics_Min_Int32)));
				}
				if (typeFromHandle == typeof(long))
				{
					return (TValue)((object)ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(Statistics.TypedStatistics_Min_Int64)));
				}
				if (typeFromHandle == typeof(Int96))
				{
					return (TValue)((object)ExceptionInfo.Return<Int96>(this.Handle, new ExceptionInfo.GetFunction<Int96>(Statistics.TypedStatistics_Min_Int96)));
				}
				if (typeFromHandle == typeof(float))
				{
					return (TValue)((object)ExceptionInfo.Return<float>(this.Handle, new ExceptionInfo.GetFunction<float>(Statistics.TypedStatistics_Min_Float)));
				}
				if (typeFromHandle == typeof(double))
				{
					return (TValue)((object)ExceptionInfo.Return<double>(this.Handle, new ExceptionInfo.GetFunction<double>(Statistics.TypedStatistics_Min_Double)));
				}
				if (typeFromHandle == typeof(ByteArray))
				{
					return (TValue)((object)ExceptionInfo.Return<ByteArray>(this.Handle, new ExceptionInfo.GetFunction<ByteArray>(Statistics.TypedStatistics_Min_ByteArray)));
				}
				if (typeFromHandle == typeof(FixedLenByteArray))
				{
					return (TValue)((object)ExceptionInfo.Return<FixedLenByteArray>(this.Handle, new ExceptionInfo.GetFunction<FixedLenByteArray>(Statistics.TypedStatistics_Min_FLBA)));
				}
				throw new NotSupportedException(string.Format("type {0} is not supported", typeFromHandle));
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
		public TValue Max
		{
			get
			{
				Type typeFromHandle = typeof(TValue);
				if (typeFromHandle == typeof(bool))
				{
					return (TValue)((object)ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(Statistics.TypedStatistics_Max_Bool)));
				}
				if (typeFromHandle == typeof(int))
				{
					return (TValue)((object)ExceptionInfo.Return<int>(this.Handle, new ExceptionInfo.GetFunction<int>(Statistics.TypedStatistics_Max_Int32)));
				}
				if (typeFromHandle == typeof(long))
				{
					return (TValue)((object)ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(Statistics.TypedStatistics_Max_Int64)));
				}
				if (typeFromHandle == typeof(Int96))
				{
					return (TValue)((object)ExceptionInfo.Return<Int96>(this.Handle, new ExceptionInfo.GetFunction<Int96>(Statistics.TypedStatistics_Max_Int96)));
				}
				if (typeFromHandle == typeof(float))
				{
					return (TValue)((object)ExceptionInfo.Return<float>(this.Handle, new ExceptionInfo.GetFunction<float>(Statistics.TypedStatistics_Max_Float)));
				}
				if (typeFromHandle == typeof(double))
				{
					return (TValue)((object)ExceptionInfo.Return<double>(this.Handle, new ExceptionInfo.GetFunction<double>(Statistics.TypedStatistics_Max_Double)));
				}
				if (typeFromHandle == typeof(ByteArray))
				{
					return (TValue)((object)ExceptionInfo.Return<ByteArray>(this.Handle, new ExceptionInfo.GetFunction<ByteArray>(Statistics.TypedStatistics_Max_ByteArray)));
				}
				if (typeFromHandle == typeof(FixedLenByteArray))
				{
					return (TValue)((object)ExceptionInfo.Return<FixedLenByteArray>(this.Handle, new ExceptionInfo.GetFunction<FixedLenByteArray>(Statistics.TypedStatistics_Max_FLBA)));
				}
				throw new NotSupportedException(string.Format("type {0} is not supported", typeFromHandle));
			}
		}
	}
}
