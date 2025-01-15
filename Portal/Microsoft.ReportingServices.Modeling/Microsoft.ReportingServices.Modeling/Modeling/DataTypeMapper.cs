using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000047 RID: 71
	public static class DataTypeMapper
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000A906 File Offset: 0x00008B06
		public static DataType? TranslateClrType(Type type)
		{
			return DataTypeMapper.PerformAction<DataType?>(type, DataTypeMapper.TranslateClrTypeAction.Instance);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A914 File Offset: 0x00008B14
		public static Type TranslateDataType(DataType dataType)
		{
			switch (dataType)
			{
			case DataType.String:
				return typeof(string);
			case DataType.Integer:
				return typeof(long);
			case DataType.Decimal:
				return typeof(decimal);
			case DataType.Float:
				return typeof(double);
			case DataType.Boolean:
				return typeof(bool);
			case DataType.DateTime:
				return typeof(DateTime);
			case DataType.Binary:
				return typeof(byte[]);
			case DataType.EntityKey:
				return typeof(EntityKey);
			case DataType.Null:
				return null;
			case DataType.Time:
				return typeof(TimeSpan);
			default:
				throw new InternalModelingException("Unexpected data type: " + dataType.ToString());
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A9D4 File Offset: 0x00008BD4
		public static bool IsNumeric(Type type)
		{
			DataType? dataType = DataTypeMapper.TranslateClrType(type);
			DataType? dataType2 = dataType;
			DataType dataType3 = DataType.Integer;
			if (!((dataType2.GetValueOrDefault() == dataType3) & (dataType2 != null)))
			{
				dataType2 = dataType;
				dataType3 = DataType.Decimal;
				if (!((dataType2.GetValueOrDefault() == dataType3) & (dataType2 != null)))
				{
					dataType2 = dataType;
					dataType3 = DataType.Float;
					return (dataType2.GetValueOrDefault() == dataType3) & (dataType2 != null);
				}
			}
			return true;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000AA30 File Offset: 0x00008C30
		public static T PerformAction<T>(Type type, IMappedClrTypeAction<T> action)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return action.ForBoolean();
			case TypeCode.Char:
				return action.ForChar();
			case TypeCode.SByte:
				return action.ForSByte();
			case TypeCode.Byte:
				return action.ForByte();
			case TypeCode.Int16:
				return action.ForInt16();
			case TypeCode.UInt16:
				return action.ForUInt16();
			case TypeCode.Int32:
				return action.ForInt32();
			case TypeCode.UInt32:
				return action.ForUInt32();
			case TypeCode.Int64:
				return action.ForInt64();
			case TypeCode.UInt64:
				return action.ForUInt64();
			case TypeCode.Single:
				return action.ForSingle();
			case TypeCode.Double:
				return action.ForDouble();
			case TypeCode.Decimal:
				return action.ForDecimal();
			case TypeCode.DateTime:
				return action.ForDateTime();
			case TypeCode.String:
				return action.ForString();
			}
			if (type == typeof(Guid))
			{
				return action.ForGuid();
			}
			if (type == typeof(byte[]))
			{
				return action.ForByteArray();
			}
			if (type == typeof(EntityKey))
			{
				return action.ForEntityKey();
			}
			if (type == typeof(DateTimeOffset))
			{
				return action.ForDateTimeOffset();
			}
			if (type == typeof(TimeSpan))
			{
				return action.ForTimeSpan();
			}
			return action.ForUnknown();
		}

		// Token: 0x02000124 RID: 292
		private sealed class TranslateClrTypeAction : IMappedClrTypeAction<DataType?>
		{
			// Token: 0x06000DB5 RID: 3509 RVA: 0x0002D13A File Offset: 0x0002B33A
			private TranslateClrTypeAction()
			{
			}

			// Token: 0x06000DB6 RID: 3510 RVA: 0x0002D142 File Offset: 0x0002B342
			public DataType? ForString()
			{
				return new DataType?(DataType.String);
			}

			// Token: 0x06000DB7 RID: 3511 RVA: 0x0002D14A File Offset: 0x0002B34A
			public DataType? ForChar()
			{
				return new DataType?(DataType.String);
			}

			// Token: 0x06000DB8 RID: 3512 RVA: 0x0002D152 File Offset: 0x0002B352
			public DataType? ForGuid()
			{
				return new DataType?(DataType.String);
			}

			// Token: 0x06000DB9 RID: 3513 RVA: 0x0002D15A File Offset: 0x0002B35A
			public DataType? ForInt64()
			{
				return new DataType?(DataType.Integer);
			}

			// Token: 0x06000DBA RID: 3514 RVA: 0x0002D162 File Offset: 0x0002B362
			public DataType? ForInt32()
			{
				return new DataType?(DataType.Integer);
			}

			// Token: 0x06000DBB RID: 3515 RVA: 0x0002D16A File Offset: 0x0002B36A
			public DataType? ForUInt32()
			{
				return new DataType?(DataType.Integer);
			}

			// Token: 0x06000DBC RID: 3516 RVA: 0x0002D172 File Offset: 0x0002B372
			public DataType? ForInt16()
			{
				return new DataType?(DataType.Integer);
			}

			// Token: 0x06000DBD RID: 3517 RVA: 0x0002D17A File Offset: 0x0002B37A
			public DataType? ForUInt16()
			{
				return new DataType?(DataType.Integer);
			}

			// Token: 0x06000DBE RID: 3518 RVA: 0x0002D182 File Offset: 0x0002B382
			public DataType? ForByte()
			{
				return new DataType?(DataType.Integer);
			}

			// Token: 0x06000DBF RID: 3519 RVA: 0x0002D18A File Offset: 0x0002B38A
			public DataType? ForSByte()
			{
				return new DataType?(DataType.Integer);
			}

			// Token: 0x06000DC0 RID: 3520 RVA: 0x0002D192 File Offset: 0x0002B392
			public DataType? ForDecimal()
			{
				return new DataType?(DataType.Decimal);
			}

			// Token: 0x06000DC1 RID: 3521 RVA: 0x0002D19A File Offset: 0x0002B39A
			public DataType? ForUInt64()
			{
				return new DataType?(DataType.Decimal);
			}

			// Token: 0x06000DC2 RID: 3522 RVA: 0x0002D1A2 File Offset: 0x0002B3A2
			public DataType? ForDouble()
			{
				return new DataType?(DataType.Float);
			}

			// Token: 0x06000DC3 RID: 3523 RVA: 0x0002D1AA File Offset: 0x0002B3AA
			public DataType? ForSingle()
			{
				return new DataType?(DataType.Float);
			}

			// Token: 0x06000DC4 RID: 3524 RVA: 0x0002D1B2 File Offset: 0x0002B3B2
			public DataType? ForDateTime()
			{
				return new DataType?(DataType.DateTime);
			}

			// Token: 0x06000DC5 RID: 3525 RVA: 0x0002D1BA File Offset: 0x0002B3BA
			public DataType? ForDateTimeOffset()
			{
				return new DataType?(DataType.DateTime);
			}

			// Token: 0x06000DC6 RID: 3526 RVA: 0x0002D1C2 File Offset: 0x0002B3C2
			public DataType? ForTimeSpan()
			{
				return new DataType?(DataType.Time);
			}

			// Token: 0x06000DC7 RID: 3527 RVA: 0x0002D1CB File Offset: 0x0002B3CB
			public DataType? ForBoolean()
			{
				return new DataType?(DataType.Boolean);
			}

			// Token: 0x06000DC8 RID: 3528 RVA: 0x0002D1D3 File Offset: 0x0002B3D3
			public DataType? ForByteArray()
			{
				return new DataType?(DataType.Binary);
			}

			// Token: 0x06000DC9 RID: 3529 RVA: 0x0002D1DB File Offset: 0x0002B3DB
			public DataType? ForEntityKey()
			{
				return new DataType?(DataType.EntityKey);
			}

			// Token: 0x06000DCA RID: 3530 RVA: 0x0002D1E4 File Offset: 0x0002B3E4
			public DataType? ForUnknown()
			{
				return null;
			}

			// Token: 0x040005B8 RID: 1464
			internal static readonly DataTypeMapper.TranslateClrTypeAction Instance = new DataTypeMapper.TranslateClrTypeAction();
		}
	}
}
