using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes.TypeMappings
{
	// Token: 0x02000AB7 RID: 2743
	public static class TSqlTypeMappings
	{
		// Token: 0x060044E0 RID: 17632 RVA: 0x000D79DC File Offset: 0x000D5BDC
		public static string ToTSqlType(this IRichDataType richType)
		{
			switch (richType.Kind)
			{
			case DataKind.Numeric:
				return TSqlTypeMappings.InferSqlNumericType(richType) ?? "varchar";
			case DataKind.Date:
			case DataKind.Time:
			case DataKind.DateTime:
				return TSqlTypeMappings.InferSqlDateTime(richType) ?? "varchar";
			case DataKind.Boolean:
				return "bit";
			default:
				return "varchar";
			}
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x000D7A38 File Offset: 0x000D5C38
		private static string InferSqlDateTime(IRichDataType richType)
		{
			RichDateType richDateType = richType as RichDateType;
			if (richDateType == null)
			{
				return null;
			}
			if (richDateType.HasDateTime)
			{
				return "datetime2";
			}
			if (richDateType.HasDate)
			{
				return "date";
			}
			if (richDateType.HasTime)
			{
				return "time";
			}
			return null;
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x000D7A7C File Offset: 0x000D5C7C
		private static string InferSqlNumericType(IRichDataType richType)
		{
			RichNumericType richNumericType = richType as RichNumericType;
			if (richNumericType == null)
			{
				return null;
			}
			if (richNumericType.MinValue.HasValue && richNumericType.MaxValue.HasValue)
			{
				BigInteger value = richNumericType.MinValue.Value;
				BigInteger value2 = richNumericType.MaxValue.Value;
				if (value >= 0L && value2 <= 255L)
				{
					return "tinyint";
				}
				if (value >= -32768L && value2 <= 32767L)
				{
					return "smallint";
				}
				if (value >= -2147483648L && value2 <= 2147483647L)
				{
					return "int";
				}
				if (value >= -9223372036854775808L && value2 <= 9223372036854775807L)
				{
					return "bigint";
				}
				return null;
			}
			else
			{
				if (!richNumericType.Precision.HasValue || !richNumericType.Scale.HasValue)
				{
					return "float(53)";
				}
				if (richNumericType.Precision.Value > 38)
				{
					return null;
				}
				return FormattableString.Invariant(FormattableStringFactory.Create("decimal({0}, {1})", new object[]
				{
					richNumericType.Precision.Value,
					richNumericType.Scale.Value
				}));
			}
		}
	}
}
