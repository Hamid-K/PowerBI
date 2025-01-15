using System;
using System.Globalization;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200065E RID: 1630
	internal abstract class DataTypeUtility
	{
		// Token: 0x06005A8A RID: 23178 RVA: 0x00174618 File Offset: 0x00172818
		internal static bool IsSpatial(DataAggregate.DataTypeCode typeCode)
		{
			return typeCode == DataAggregate.DataTypeCode.SqlGeography || typeCode == DataAggregate.DataTypeCode.SqlGeometry;
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x00174626 File Offset: 0x00172826
		internal static bool IsNumeric(DataAggregate.DataTypeCode typeCode)
		{
			return typeCode - DataAggregate.DataTypeCode.Int16 <= 8 || typeCode - DataAggregate.DataTypeCode.Single <= 2;
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x00174638 File Offset: 0x00172838
		internal static bool IsFloat(DataAggregate.DataTypeCode typeCode)
		{
			return typeCode - DataAggregate.DataTypeCode.Single <= 1;
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x00174644 File Offset: 0x00172844
		internal static bool IsSigned(DataAggregate.DataTypeCode typeCode)
		{
			return typeCode - DataAggregate.DataTypeCode.Int16 <= 2 || typeCode == DataAggregate.DataTypeCode.SByte;
		}

		// Token: 0x06005A8E RID: 23182 RVA: 0x00174654 File Offset: 0x00172854
		internal static bool IsUnsigned(DataAggregate.DataTypeCode typeCode)
		{
			return typeCode - DataAggregate.DataTypeCode.UInt16 <= 3;
		}

		// Token: 0x06005A8F RID: 23183 RVA: 0x0017465F File Offset: 0x0017285F
		internal static bool Is32BitOrLess(DataAggregate.DataTypeCode typeCode)
		{
			switch (typeCode)
			{
			case DataAggregate.DataTypeCode.Int16:
			case DataAggregate.DataTypeCode.Int32:
			case DataAggregate.DataTypeCode.UInt16:
			case DataAggregate.DataTypeCode.UInt32:
			case DataAggregate.DataTypeCode.Byte:
			case DataAggregate.DataTypeCode.SByte:
				return true;
			}
			return false;
		}

		// Token: 0x06005A90 RID: 23184 RVA: 0x0017468E File Offset: 0x0017288E
		internal static bool Is64BitOrLess(DataAggregate.DataTypeCode typeCode)
		{
			return typeCode - DataAggregate.DataTypeCode.Int16 <= 7;
		}

		// Token: 0x06005A91 RID: 23185 RVA: 0x0017469C File Offset: 0x0017289C
		internal static double ConvertToDouble(DataAggregate.DataTypeCode typeCode, object data)
		{
			switch (typeCode)
			{
			case DataAggregate.DataTypeCode.Int16:
				return (double)((short)data);
			case DataAggregate.DataTypeCode.Int32:
				return (double)((int)data);
			case DataAggregate.DataTypeCode.Int64:
				return (double)((long)data);
			case DataAggregate.DataTypeCode.UInt16:
				return (double)((ushort)data);
			case DataAggregate.DataTypeCode.UInt32:
				return (uint)data;
			case DataAggregate.DataTypeCode.UInt64:
				return (ulong)data;
			case DataAggregate.DataTypeCode.Byte:
				return (double)((byte)data);
			case DataAggregate.DataTypeCode.SByte:
				return (double)((sbyte)data);
			case DataAggregate.DataTypeCode.TimeSpan:
				return (double)((TimeSpan)data).Ticks;
			case DataAggregate.DataTypeCode.Single:
				return (double)((float)data);
			case DataAggregate.DataTypeCode.Double:
				return (double)data;
			case DataAggregate.DataTypeCode.Decimal:
				return Convert.ToDouble((decimal)data);
			}
			Global.Tracer.Assert(false);
			throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x0017476C File Offset: 0x0017296C
		internal static int ConvertToInt32(DataAggregate.DataTypeCode typeCode, object data, out bool valid)
		{
			valid = true;
			switch (typeCode)
			{
			case DataAggregate.DataTypeCode.Int16:
				return (int)((short)data);
			case DataAggregate.DataTypeCode.Int32:
				return (int)data;
			case DataAggregate.DataTypeCode.Int64:
				if ((long)data <= 2147483647L && (long)data >= -2147483648L)
				{
					return (int)data;
				}
				break;
			case DataAggregate.DataTypeCode.UInt16:
				return (int)((ushort)data);
			case DataAggregate.DataTypeCode.UInt32:
				if ((uint)data <= 2147483647U)
				{
					return (int)data;
				}
				break;
			case DataAggregate.DataTypeCode.UInt64:
				if ((ulong)data <= 2147483647UL)
				{
					return (int)data;
				}
				break;
			case DataAggregate.DataTypeCode.Byte:
				return (int)((byte)data);
			case DataAggregate.DataTypeCode.SByte:
				return (int)((sbyte)data);
			}
			valid = false;
			return 0;
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x0017481C File Offset: 0x00172A1C
		internal static string ConvertToInvariantString(object o)
		{
			if (o == null)
			{
				return null;
			}
			string text = null;
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			try
			{
				text = o.ToString();
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
			return text;
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x00174870 File Offset: 0x00172A70
		internal static Type GetNumericTypeFromDataTypeCode(DataAggregate.DataTypeCode typeCode)
		{
			switch (typeCode)
			{
			case DataAggregate.DataTypeCode.Int16:
				return typeof(short);
			case DataAggregate.DataTypeCode.Int32:
				return typeof(int);
			case DataAggregate.DataTypeCode.Int64:
				return typeof(long);
			case DataAggregate.DataTypeCode.UInt16:
				return typeof(ushort);
			case DataAggregate.DataTypeCode.UInt32:
				return typeof(uint);
			case DataAggregate.DataTypeCode.UInt64:
				return typeof(ulong);
			case DataAggregate.DataTypeCode.Single:
				return typeof(float);
			case DataAggregate.DataTypeCode.Double:
				return typeof(double);
			case DataAggregate.DataTypeCode.Decimal:
				return typeof(decimal);
			}
			return null;
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x00174920 File Offset: 0x00172B20
		internal static DataAggregate.DataTypeCode CommonNumericDenominator(DataAggregate.DataTypeCode x, DataAggregate.DataTypeCode y)
		{
			if (!DataTypeUtility.IsNumeric(x) || !DataTypeUtility.IsNumeric(y))
			{
				return DataAggregate.DataTypeCode.Null;
			}
			if (x == y)
			{
				return x;
			}
			if (DataTypeUtility.IsSigned(x) && DataTypeUtility.IsSigned(y))
			{
				if (DataAggregate.DataTypeCode.Int64 == x || DataAggregate.DataTypeCode.Int64 == y)
				{
					return DataAggregate.DataTypeCode.Int64;
				}
				return DataAggregate.DataTypeCode.Int32;
			}
			else if (DataTypeUtility.IsUnsigned(x) && DataTypeUtility.IsUnsigned(y))
			{
				if (DataAggregate.DataTypeCode.UInt64 == x || DataAggregate.DataTypeCode.UInt64 == y)
				{
					return DataAggregate.DataTypeCode.UInt64;
				}
				return DataAggregate.DataTypeCode.UInt32;
			}
			else
			{
				if (DataTypeUtility.IsFloat(x) && DataTypeUtility.IsFloat(y))
				{
					return DataAggregate.DataTypeCode.Double;
				}
				if (DataTypeUtility.IsSigned(x) && DataTypeUtility.IsUnsigned(y))
				{
					return DataTypeUtility.CommonDataTypeSignedUnsigned(x, y);
				}
				if (DataTypeUtility.IsUnsigned(x) && DataTypeUtility.IsSigned(y))
				{
					return DataTypeUtility.CommonDataTypeSignedUnsigned(y, x);
				}
				if ((DataTypeUtility.Is32BitOrLess(x) && DataTypeUtility.IsFloat(y)) || (DataTypeUtility.Is32BitOrLess(y) && DataTypeUtility.IsFloat(x)))
				{
					return DataAggregate.DataTypeCode.Double;
				}
				if ((DataTypeUtility.Is64BitOrLess(x) && DataAggregate.DataTypeCode.Decimal == y) || (DataTypeUtility.Is64BitOrLess(y) && DataAggregate.DataTypeCode.Decimal == x))
				{
					return DataAggregate.DataTypeCode.Decimal;
				}
				return DataAggregate.DataTypeCode.Null;
			}
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x00174A04 File Offset: 0x00172C04
		internal static bool IsNumericLessThanZero(object value, DataAggregate.DataTypeCode dataType)
		{
			if (dataType == DataAggregate.DataTypeCode.Int32)
			{
				return (int)value < 0;
			}
			if (dataType == DataAggregate.DataTypeCode.Double)
			{
				return (double)value < 0.0;
			}
			if (dataType == DataAggregate.DataTypeCode.Single)
			{
				return (float)value < 0f;
			}
			if (dataType == DataAggregate.DataTypeCode.Decimal)
			{
				return (decimal)value < 0m;
			}
			if (dataType == DataAggregate.DataTypeCode.Int16)
			{
				return (short)value < 0;
			}
			if (dataType == DataAggregate.DataTypeCode.Int64)
			{
				return (long)value < 0L;
			}
			if (dataType == DataAggregate.DataTypeCode.UInt16)
			{
				return (ushort)value < 0;
			}
			if (dataType == DataAggregate.DataTypeCode.UInt32)
			{
				return (uint)value < 0U;
			}
			if (dataType == DataAggregate.DataTypeCode.UInt64)
			{
				return (ulong)value < 0UL;
			}
			if (dataType == DataAggregate.DataTypeCode.Byte)
			{
				return (byte)value < 0;
			}
			return dataType == DataAggregate.DataTypeCode.SByte && (sbyte)value < 0;
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x00174AC7 File Offset: 0x00172CC7
		private static DataAggregate.DataTypeCode CommonDataTypeSignedUnsigned(DataAggregate.DataTypeCode signed, DataAggregate.DataTypeCode unsigned)
		{
			Global.Tracer.Assert(DataTypeUtility.IsSigned(signed) && DataTypeUtility.IsUnsigned(unsigned), "(IsSigned(signed) && IsUnsigned(unsigned))");
			if (DataAggregate.DataTypeCode.UInt64 == unsigned)
			{
				return DataAggregate.DataTypeCode.Null;
			}
			if (DataAggregate.DataTypeCode.UInt32 == unsigned)
			{
				return DataAggregate.DataTypeCode.Int64;
			}
			if (DataAggregate.DataTypeCode.Int64 == signed)
			{
				return DataAggregate.DataTypeCode.Int64;
			}
			return DataAggregate.DataTypeCode.Int32;
		}
	}
}
