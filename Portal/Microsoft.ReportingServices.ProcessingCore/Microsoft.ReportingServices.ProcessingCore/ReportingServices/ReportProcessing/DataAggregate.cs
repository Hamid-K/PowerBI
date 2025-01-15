using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000666 RID: 1638
	internal abstract class DataAggregate
	{
		// Token: 0x06005ABC RID: 23228
		internal abstract void Init();

		// Token: 0x06005ABD RID: 23229
		internal abstract void Update(object[] expressions, IErrorContext iErrorContext);

		// Token: 0x06005ABE RID: 23230
		internal abstract object Result();

		// Token: 0x06005ABF RID: 23231 RVA: 0x00175650 File Offset: 0x00173850
		internal static DataAggregate.DataTypeCode GetTypeCode(object o)
		{
			bool flag;
			return DataAggregate.GetTypeCode(o, true, out flag);
		}

		// Token: 0x06005AC0 RID: 23232 RVA: 0x00175668 File Offset: 0x00173868
		internal static DataAggregate.DataTypeCode GetTypeCode(object o, bool throwException, out bool valid)
		{
			valid = true;
			if (o is string)
			{
				return DataAggregate.DataTypeCode.String;
			}
			if (o is int)
			{
				return DataAggregate.DataTypeCode.Int32;
			}
			if (o is double)
			{
				return DataAggregate.DataTypeCode.Double;
			}
			if (o == null || DBNull.Value == o)
			{
				return DataAggregate.DataTypeCode.Null;
			}
			if (o is ushort)
			{
				return DataAggregate.DataTypeCode.UInt16;
			}
			if (o is short)
			{
				return DataAggregate.DataTypeCode.Int16;
			}
			if (o is long)
			{
				return DataAggregate.DataTypeCode.Int64;
			}
			if (o is decimal)
			{
				return DataAggregate.DataTypeCode.Decimal;
			}
			if (o is uint)
			{
				return DataAggregate.DataTypeCode.UInt32;
			}
			if (o is ulong)
			{
				return DataAggregate.DataTypeCode.UInt64;
			}
			if (o is byte)
			{
				return DataAggregate.DataTypeCode.Byte;
			}
			if (o is sbyte)
			{
				return DataAggregate.DataTypeCode.SByte;
			}
			if (o is DateTime)
			{
				return DataAggregate.DataTypeCode.DateTime;
			}
			if (o is DateTimeOffset)
			{
				return DataAggregate.DataTypeCode.DateTimeOffset;
			}
			if (o is char)
			{
				return DataAggregate.DataTypeCode.Char;
			}
			if (o is bool)
			{
				return DataAggregate.DataTypeCode.Boolean;
			}
			if (o is TimeSpan)
			{
				return DataAggregate.DataTypeCode.TimeSpan;
			}
			if (o is float)
			{
				return DataAggregate.DataTypeCode.Single;
			}
			if (o is byte[])
			{
				return DataAggregate.DataTypeCode.ByteArray;
			}
			valid = false;
			if (throwException)
			{
				throw new InvalidOperationException();
			}
			return DataAggregate.DataTypeCode.Null;
		}

		// Token: 0x06005AC1 RID: 23233 RVA: 0x00175750 File Offset: 0x00173950
		protected static bool IsNull(DataAggregate.DataTypeCode typeCode)
		{
			return typeCode == DataAggregate.DataTypeCode.Null;
		}

		// Token: 0x06005AC2 RID: 23234 RVA: 0x00175756 File Offset: 0x00173956
		protected static bool IsVariant(DataAggregate.DataTypeCode typeCode)
		{
			return DataAggregate.DataTypeCode.ByteArray != typeCode;
		}

		// Token: 0x06005AC3 RID: 23235 RVA: 0x00175760 File Offset: 0x00173960
		protected static void ConvertToDoubleOrDecimal(DataAggregate.DataTypeCode numericType, object numericData, out DataAggregate.DataTypeCode doubleOrDecimalType, out object doubleOrDecimalData)
		{
			if (DataAggregate.DataTypeCode.Decimal == numericType)
			{
				doubleOrDecimalType = DataAggregate.DataTypeCode.Decimal;
				doubleOrDecimalData = numericData;
				return;
			}
			doubleOrDecimalType = DataAggregate.DataTypeCode.Double;
			doubleOrDecimalData = DataTypeUtility.ConvertToDouble(numericType, numericData);
		}

		// Token: 0x06005AC4 RID: 23236 RVA: 0x00175784 File Offset: 0x00173984
		protected static object Add(DataAggregate.DataTypeCode xType, object x, DataAggregate.DataTypeCode yType, object y)
		{
			if (DataAggregate.DataTypeCode.Double == xType && DataAggregate.DataTypeCode.Double == yType)
			{
				return (double)x + (double)y;
			}
			if (DataAggregate.DataTypeCode.Decimal == xType && DataAggregate.DataTypeCode.Decimal == yType)
			{
				return (decimal)x + (decimal)y;
			}
			Global.Tracer.Assert(false);
			throw new InvalidOperationException();
		}

		// Token: 0x06005AC5 RID: 23237 RVA: 0x001757E0 File Offset: 0x001739E0
		protected static object Square(DataAggregate.DataTypeCode xType, object x)
		{
			if (DataAggregate.DataTypeCode.Double == xType)
			{
				return (double)x * (double)x;
			}
			if (DataAggregate.DataTypeCode.Decimal == xType)
			{
				return (decimal)x * (decimal)x;
			}
			Global.Tracer.Assert(false);
			throw new InvalidOperationException();
		}

		// Token: 0x02000C91 RID: 3217
		internal enum DataTypeCode
		{
			// Token: 0x04004CE3 RID: 19683
			Null,
			// Token: 0x04004CE4 RID: 19684
			String,
			// Token: 0x04004CE5 RID: 19685
			Char,
			// Token: 0x04004CE6 RID: 19686
			Boolean,
			// Token: 0x04004CE7 RID: 19687
			Int16,
			// Token: 0x04004CE8 RID: 19688
			Int32,
			// Token: 0x04004CE9 RID: 19689
			Int64,
			// Token: 0x04004CEA RID: 19690
			UInt16,
			// Token: 0x04004CEB RID: 19691
			UInt32,
			// Token: 0x04004CEC RID: 19692
			UInt64,
			// Token: 0x04004CED RID: 19693
			Byte,
			// Token: 0x04004CEE RID: 19694
			SByte,
			// Token: 0x04004CEF RID: 19695
			TimeSpan,
			// Token: 0x04004CF0 RID: 19696
			DateTime,
			// Token: 0x04004CF1 RID: 19697
			Single,
			// Token: 0x04004CF2 RID: 19698
			Double,
			// Token: 0x04004CF3 RID: 19699
			Decimal,
			// Token: 0x04004CF4 RID: 19700
			ByteArray,
			// Token: 0x04004CF5 RID: 19701
			DateTimeOffset,
			// Token: 0x04004CF6 RID: 19702
			SqlGeography,
			// Token: 0x04004CF7 RID: 19703
			SqlGeometry
		}
	}
}
