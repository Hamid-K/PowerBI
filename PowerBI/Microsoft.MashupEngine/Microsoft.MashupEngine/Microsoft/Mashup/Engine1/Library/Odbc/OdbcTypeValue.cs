using System;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200067D RID: 1661
	internal static class OdbcTypeValue
	{
		// Token: 0x06003430 RID: 13360 RVA: 0x000A7A14 File Offset: 0x000A5C14
		public static TypeValue New(Odbc32.SQL_TYPE sqlType, string typeName, string defaultValue, int? numberPrecisionRadix, long? columnSize, int? scale, bool? unsigned, bool? isNullable, string remarks)
		{
			OdbcTypeMap odbcTypeMap = OdbcTypeMap.FromSqlType(sqlType);
			TypeValue typeValue = odbcTypeMap.TypeValue;
			if (unsigned != null)
			{
				if (unsigned.Value)
				{
					typeValue = (odbcTypeMap.GetUnsigned() ?? odbcTypeMap).TypeValue;
				}
			}
			else if (odbcTypeMap.TypeValue.TypeKind == ValueKind.Number)
			{
				typeValue = TypeValue.Number;
			}
			if (isNullable == null || isNullable.Value)
			{
				typeValue = typeValue.Nullable;
			}
			TypeFacets typeFacets = OdbcTypeValue.GetTypeFacets(sqlType, typeName, defaultValue, numberPrecisionRadix, columnSize, scale, unsigned);
			typeValue = typeValue.NewFacets(typeFacets);
			if (!string.IsNullOrEmpty(remarks))
			{
				RecordValue recordValue = RecordValue.New(OdbcTypeValue.descriptionKeys, new Value[] { TextValue.New(remarks) });
				recordValue = typeValue.MetaValue.Concatenate(recordValue).AsRecord;
				typeValue = typeValue.NewMeta(recordValue).AsType;
			}
			return typeValue;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000A7AE0 File Offset: 0x000A5CE0
		private static TypeFacets GetTypeFacets(Odbc32.SQL_TYPE sqlType, string typeName, string defaultValue, int? numberPrecisionRadix, long? columnSize, int? scale, bool? unsigned)
		{
			TypeFacets typeFacets = TypeFacets.None;
			int? maxSize = OdbcTypeValue.GetMaxSize(columnSize);
			int? num;
			if (sqlType != Odbc32.SQL_TYPE.SS_XML)
			{
				switch (sqlType)
				{
				case Odbc32.SQL_TYPE.WLONGVARCHAR:
				case Odbc32.SQL_TYPE.WVARCHAR:
				case Odbc32.SQL_TYPE.VARCHAR:
					break;
				case Odbc32.SQL_TYPE.WCHAR:
				case Odbc32.SQL_TYPE.CHAR:
					num = maxSize;
					typeFacets = TypeFacets.NewText((num != null) ? new long?((long)num.GetValueOrDefault()) : null, new bool?(false), null);
					goto IL_01FA;
				case Odbc32.SQL_TYPE.BIT:
				case Odbc32.SQL_TYPE.LONGVARCHAR:
				case Odbc32.SQL_TYPE.UNKNOWN:
				case Odbc32.SQL_TYPE.DATETIME:
				case Odbc32.SQL_TYPE.INTERVAL:
				case Odbc32.SQL_TYPE.TIMESTAMP:
					goto IL_01FA;
				case Odbc32.SQL_TYPE.TINYINT:
					typeFacets = OdbcTypeValue.TinyInt;
					goto IL_01FA;
				case Odbc32.SQL_TYPE.BIGINT:
					if (unsigned == null)
					{
						goto IL_01FA;
					}
					if (unsigned.Value)
					{
						typeFacets = OdbcTypeValue.UnsignedBigInt;
						goto IL_01FA;
					}
					typeFacets = OdbcTypeValue.SignedBigInt;
					goto IL_01FA;
				case Odbc32.SQL_TYPE.LONGVARBINARY:
				case Odbc32.SQL_TYPE.VARBINARY:
					num = maxSize;
					typeFacets = TypeFacets.NewBinary((num != null) ? new long?((long)num.GetValueOrDefault()) : null, new bool?(true), null);
					goto IL_01FA;
				case Odbc32.SQL_TYPE.BINARY:
					num = maxSize;
					typeFacets = TypeFacets.NewBinary((num != null) ? new long?((long)num.GetValueOrDefault()) : null, new bool?(false), null);
					goto IL_01FA;
				case Odbc32.SQL_TYPE.NUMERIC:
				case Odbc32.SQL_TYPE.DECIMAL:
					if (numberPrecisionRadix != null && maxSize != null)
					{
						typeFacets = TypeFacets.NewNumeric(numberPrecisionRadix, maxSize, scale, null);
						goto IL_01FA;
					}
					goto IL_01FA;
				case Odbc32.SQL_TYPE.INTEGER:
					typeFacets = OdbcTypeValue.Integer;
					goto IL_01FA;
				case Odbc32.SQL_TYPE.SMALLINT:
					typeFacets = OdbcTypeValue.SmallInt;
					goto IL_01FA;
				case Odbc32.SQL_TYPE.FLOAT:
				case Odbc32.SQL_TYPE.REAL:
					if (numberPrecisionRadix != null && maxSize != null)
					{
						typeFacets = TypeFacets.NewNumeric(numberPrecisionRadix, maxSize, null, null);
						goto IL_01FA;
					}
					goto IL_01FA;
				case Odbc32.SQL_TYPE.DOUBLE:
					typeFacets = OdbcTypeValue.Double;
					goto IL_01FA;
				default:
					goto IL_01FA;
				}
			}
			num = maxSize;
			typeFacets = TypeFacets.NewText((num != null) ? new long?((long)num.GetValueOrDefault()) : null, new bool?(true), null);
			IL_01FA:
			return typeFacets.AddNative(typeName, defaultValue, null);
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000A7CF0 File Offset: 0x000A5EF0
		private static int? GetMaxSize(long? size)
		{
			if (size != null)
			{
				long? num = size;
				long num2 = 2147483647L;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					num = size;
					num2 = 0L;
					if ((num.GetValueOrDefault() > num2) & (num != null))
					{
						return new int?((int)size.Value);
					}
				}
			}
			return null;
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000A7D53 File Offset: 0x000A5F53
		private static TypeFacets NewWholeNumberFacets(int precision)
		{
			return TypeFacets.NewNumeric(new int?(10), new int?(precision), new int?(0), null);
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000A7D70 File Offset: 0x000A5F70
		private static TypeFacets NewFloatFacets(int precision)
		{
			return TypeFacets.NewNumeric(new int?(2), new int?(precision), null, null);
		}

		// Token: 0x04001761 RID: 5985
		public static readonly TypeFacets SignedBigInt = OdbcTypeValue.NewWholeNumberFacets(19);

		// Token: 0x04001762 RID: 5986
		public static readonly TypeFacets UnsignedBigInt = OdbcTypeValue.NewWholeNumberFacets(20);

		// Token: 0x04001763 RID: 5987
		public static readonly TypeFacets Integer = OdbcTypeValue.NewWholeNumberFacets(10);

		// Token: 0x04001764 RID: 5988
		public static readonly TypeFacets SmallInt = OdbcTypeValue.NewWholeNumberFacets(5);

		// Token: 0x04001765 RID: 5989
		public static readonly TypeFacets TinyInt = OdbcTypeValue.NewWholeNumberFacets(3);

		// Token: 0x04001766 RID: 5990
		public static readonly TypeFacets Double = OdbcTypeValue.NewFloatFacets(53);

		// Token: 0x04001767 RID: 5991
		private static readonly Keys descriptionKeys = Keys.New("Documentation.FieldDescription");
	}
}
