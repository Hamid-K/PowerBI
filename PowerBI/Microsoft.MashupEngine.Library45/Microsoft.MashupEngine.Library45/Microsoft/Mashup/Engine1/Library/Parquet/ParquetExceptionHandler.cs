using System;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F35 RID: 7989
	internal static class ParquetExceptionHandler
	{
		// Token: 0x06010CB8 RID: 68792 RVA: 0x0039D71C File Offset: 0x0039B91C
		public static T Invoke<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action();
			}
			catch (ParquetException ex)
			{
				throw ParquetExceptionHandler.GetParquetValueException(ex);
			}
			return t;
		}

		// Token: 0x06010CB9 RID: 68793 RVA: 0x0039D74C File Offset: 0x0039B94C
		public static ValueException GetParquetValueException(ParquetException exception)
		{
			return ValueException.NewDataFormatError(Strings.DataSourceExceptionMessage("Parquet", exception.Message), Value.Null, exception);
		}
	}
}
