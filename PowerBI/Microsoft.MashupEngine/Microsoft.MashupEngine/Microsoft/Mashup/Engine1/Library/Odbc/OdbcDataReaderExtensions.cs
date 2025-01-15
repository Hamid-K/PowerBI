using System;
using System.Data;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005DA RID: 1498
	internal static class OdbcDataReaderExtensions
	{
		// Token: 0x06002EA2 RID: 11938 RVA: 0x0008E232 File Offset: 0x0008C432
		public static string GetStringOrNull(this IDataReader reader, int ordinal)
		{
			if (reader.FieldCount <= ordinal)
			{
				return null;
			}
			return reader[ordinal] as string;
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x0008E24C File Offset: 0x0008C44C
		public static int? GetNullableInt32(this IDataReader reader, int ordinal)
		{
			if (reader.FieldCount <= ordinal)
			{
				return null;
			}
			object obj = reader[ordinal];
			if (obj == DBNull.Value)
			{
				return null;
			}
			return new int?(Convert.ToInt32(obj, CultureInfo.InvariantCulture));
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x0008E298 File Offset: 0x0008C498
		public static OdbcNumberPrecisionRadix? GetNumberPrecisionRadix(this IDataReader reader, int ordinal)
		{
			int? nullableInt = reader.GetNullableInt32(ordinal);
			if (nullableInt != null)
			{
				return new OdbcNumberPrecisionRadix?((OdbcNumberPrecisionRadix)nullableInt.Value);
			}
			return null;
		}
	}
}
