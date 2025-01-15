using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200023E RID: 574
	internal static class RDLUtil
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x0002DAB9 File Offset: 0x0002BCB9
		internal static bool IsIntegerType(TypeCode typeCode)
		{
			return typeCode - global::System.TypeCode.Byte <= 6;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0002DAC4 File Offset: 0x0002BCC4
		internal static bool IsNumericType(TypeCode typeCode)
		{
			return RDLUtil.IsIntegerType(typeCode) || typeCode - global::System.TypeCode.Single <= 2;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0002DADC File Offset: 0x0002BCDC
		public static DataTypes? ConvertToDataType(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case global::System.TypeCode.DBNull:
			case global::System.TypeCode.String:
				return new DataTypes?(DataTypes.String);
			case global::System.TypeCode.Boolean:
				return new DataTypes?(DataTypes.Boolean);
			case global::System.TypeCode.Byte:
			case global::System.TypeCode.Int16:
			case global::System.TypeCode.UInt16:
			case global::System.TypeCode.Int32:
			case global::System.TypeCode.UInt32:
				return new DataTypes?(DataTypes.Integer);
			case global::System.TypeCode.Int64:
			case global::System.TypeCode.UInt64:
			case global::System.TypeCode.Single:
			case global::System.TypeCode.Double:
			case global::System.TypeCode.Decimal:
				return new DataTypes?(DataTypes.Float);
			case global::System.TypeCode.DateTime:
				return new DataTypes?(DataTypes.DateTime);
			}
			return null;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0002DB6C File Offset: 0x0002BD6C
		internal static bool ConvertToBoolean(object value)
		{
			string text = value as string;
			if (value == null || value is DBNull || (text != null && text == ""))
			{
				return false;
			}
			if (value is int)
			{
				return (int)value > 0;
			}
			return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0002DBBA File Offset: 0x0002BDBA
		internal static byte ConvertToByte(object value)
		{
			if (value == null || value is DBNull || (value is string && (string)value == ""))
			{
				return 0;
			}
			return Convert.ToByte(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0002DBF0 File Offset: 0x0002BDF0
		internal static DateTime ConvertToDateTime(object value)
		{
			if (value == null || value is DBNull || (value is string && (string)value == ""))
			{
				DateTime dateTime = default(DateTime);
				return dateTime;
			}
			DateTime dateTime2;
			try
			{
				dateTime2 = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				DateTime dateTime = default(DateTime);
				return dateTime;
			}
			return dateTime2;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0002DC5C File Offset: 0x0002BE5C
		public static decimal ConvertToDecimal(object value)
		{
			if (value is DBNull)
			{
				return 0m;
			}
			decimal num;
			try
			{
				num = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return 0m;
			}
			return num;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0002DCA4 File Offset: 0x0002BEA4
		internal static double ConvertToDouble(object value)
		{
			if (value is DBNull)
			{
				return 0.0;
			}
			double num;
			try
			{
				num = Convert.ToDouble(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return 0.0;
			}
			return num;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0002DCF4 File Offset: 0x0002BEF4
		internal static short ConvertToInt16(object value)
		{
			if (value is DBNull)
			{
				return 0;
			}
			short num;
			try
			{
				num = Convert.ToInt16(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return 0;
			}
			return num;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0002DD34 File Offset: 0x0002BF34
		internal static int ConvertToInt32(object value)
		{
			if (value is DBNull)
			{
				return 0;
			}
			int num;
			try
			{
				num = Convert.ToInt32(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return 0;
			}
			return num;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0002DD74 File Offset: 0x0002BF74
		internal static long ConvertToInt64(object value)
		{
			if (value is DBNull)
			{
				return 0L;
			}
			long num;
			try
			{
				num = Convert.ToInt64(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return 0L;
			}
			return num;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0002DDB4 File Offset: 0x0002BFB4
		internal static float ConvertToSingle(object value)
		{
			if (value is DBNull)
			{
				return 0f;
			}
			float num;
			try
			{
				num = Convert.ToSingle(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return 0f;
			}
			return num;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0002DDFC File Offset: 0x0002BFFC
		public static CultureInfo GetFormatProvider(bool useUserCulture)
		{
			if (!useUserCulture)
			{
				return CultureInfo.InvariantCulture;
			}
			return CultureInfo.CurrentCulture;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0002DE0C File Offset: 0x0002C00C
		public static string ObjectToString(object value, bool useUserCulture)
		{
			IFormatProvider formatProvider = RDLUtil.GetFormatProvider(useUserCulture);
			if (value == null)
			{
				return null;
			}
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				if (dateTime.TimeOfDay == TimeSpan.Zero)
				{
					if (useUserCulture)
					{
						return dateTime.ToString("d", formatProvider);
					}
					return dateTime.ToString("yyyy'-'MM'-'dd", formatProvider);
				}
				else
				{
					if (dateTime.Date <= DateTime.MinValue || dateTime.Date >= DateTime.MaxValue)
					{
						return dateTime.ToString("T", formatProvider);
					}
					if (useUserCulture)
					{
						return dateTime.ToString("G", formatProvider);
					}
					return dateTime.ToString("yyyy'-'MM'-'dd HH':'mm':'ss", formatProvider);
				}
			}
			else
			{
				if (useUserCulture && (value is float || value is double || value is decimal))
				{
					return ((IFormattable)value).ToString("#,0.#############", formatProvider);
				}
				return Convert.ToString(value, formatProvider);
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0002DEF0 File Offset: 0x0002C0F0
		public static object DefaultDataType(DataTypes dataType)
		{
			switch (dataType)
			{
			case DataTypes.String:
				return string.Empty;
			case DataTypes.Boolean:
				return false;
			case DataTypes.DateTime:
				return DateTime.MinValue;
			case DataTypes.Integer:
				return 0;
			case DataTypes.Float:
				return 0.0;
			default:
				return null;
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0002DF48 File Offset: 0x0002C148
		internal static bool IsNarrowingConversion(Type fromType, Type toType)
		{
			if (fromType == typeof(int))
			{
				return toType == typeof(ushort) || toType == typeof(short) || toType == typeof(sbyte) || toType == typeof(byte) || toType == typeof(bool);
			}
			if (fromType == typeof(double))
			{
				return toType == typeof(float) || toType == typeof(ulong) || toType == typeof(long) || toType == typeof(uint) || toType == typeof(int) || toType == typeof(ushort) || toType == typeof(short) || toType == typeof(sbyte) || toType == typeof(byte) || toType == typeof(bool);
			}
			return fromType == typeof(decimal) && (toType == typeof(double) || toType == typeof(float) || toType == typeof(ulong) || toType == typeof(long) || toType == typeof(uint) || toType == typeof(int) || toType == typeof(ushort) || toType == typeof(short) || toType == typeof(sbyte) || toType == typeof(byte) || toType == typeof(bool));
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0002E184 File Offset: 0x0002C384
		internal static TypeCode TypeCode(string typeCode)
		{
			if (!string.IsNullOrEmpty(typeCode))
			{
				string text = typeCode.ToUpperInvariant();
				if (text != null)
				{
					switch (text.Length)
					{
					case 11:
						if (!(text == "SYSTEM.BYTE"))
						{
							return global::System.TypeCode.String;
						}
						break;
					case 12:
					{
						char c = text[10];
						if (c != '1')
						{
							if (c != '3')
							{
								if (c != '6')
								{
									return global::System.TypeCode.String;
								}
								if (!(text == "SYSTEM.INT64"))
								{
									return global::System.TypeCode.String;
								}
							}
							else if (!(text == "SYSTEM.INT32"))
							{
								return global::System.TypeCode.String;
							}
						}
						else if (!(text == "SYSTEM.INT16"))
						{
							return global::System.TypeCode.String;
						}
						break;
					}
					case 13:
					{
						char c = text[11];
						if (c <= '3')
						{
							if (c != '1')
							{
								if (c != '3')
								{
									return global::System.TypeCode.String;
								}
								if (!(text == "SYSTEM.UINT32"))
								{
									return global::System.TypeCode.String;
								}
							}
							else if (!(text == "SYSTEM.UINT16"))
							{
								return global::System.TypeCode.String;
							}
						}
						else if (c != '6')
						{
							if (c != 'L')
							{
								if (c != 'N')
								{
									return global::System.TypeCode.String;
								}
								if (!(text == "SYSTEM.STRING"))
								{
									return global::System.TypeCode.String;
								}
								return global::System.TypeCode.String;
							}
							else
							{
								if (!(text == "SYSTEM.SINGLE") && !(text == "SYSTEM.DOUBLE"))
								{
									return global::System.TypeCode.String;
								}
								return global::System.TypeCode.Double;
							}
						}
						else if (!(text == "SYSTEM.UINT64"))
						{
							return global::System.TypeCode.String;
						}
						break;
					}
					case 14:
					{
						char c = text[7];
						if (c != 'B')
						{
							if (c != 'D')
							{
								return global::System.TypeCode.String;
							}
							if (!(text == "SYSTEM.DECIMAL"))
							{
								return global::System.TypeCode.String;
							}
							return global::System.TypeCode.Decimal;
						}
						else
						{
							if (!(text == "SYSTEM.BOOLEAN"))
							{
								return global::System.TypeCode.String;
							}
							return global::System.TypeCode.Boolean;
						}
						break;
					}
					case 15:
						if (!(text == "SYSTEM.DATETIME"))
						{
							return global::System.TypeCode.String;
						}
						return global::System.TypeCode.DateTime;
					default:
						return global::System.TypeCode.String;
					}
					return global::System.TypeCode.Int32;
				}
			}
			return global::System.TypeCode.String;
		}
	}
}
