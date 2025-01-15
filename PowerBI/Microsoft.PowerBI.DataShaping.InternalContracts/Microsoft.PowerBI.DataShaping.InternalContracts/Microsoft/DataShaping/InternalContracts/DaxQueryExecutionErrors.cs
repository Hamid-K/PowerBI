using System;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000015 RID: 21
	internal static class DaxQueryExecutionErrors
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002A84 File Offset: 0x00000C84
		internal static DaxQueryExecutionException CreateRowCountNotSupported(int maxRowCount, int columnCount)
		{
			string text = StringUtil.FormatInvariant("More than {0} rows have been encountered. Only {0} rows are allowed given the number of columns returned which is {1}.", new object[] { maxRowCount, columnCount });
			return new DaxQueryExecutionException("DaxRowCountNotSupported", text, ErrorSource.User);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002AC0 File Offset: 0x00000CC0
		internal static DaxQueryExecutionException CreateByteCountNotSupported(int maxByteCount)
		{
			string text = StringUtil.FormatInvariant("More than {0} bytes have been encountered. Only {0} bytes are allowed.", new object[] { maxByteCount });
			return new DaxQueryExecutionException("DaxByteCountNotSupported", text, ErrorSource.User);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002AF4 File Offset: 0x00000CF4
		internal static DaxQueryExecutionException CreateResultsetCountNotSupported()
		{
			string text = "More than 1 result set has been encountered. Only 1 result set is supported.";
			return new DaxQueryExecutionException("DaxResultsetCountNotSupported", text, ErrorSource.User);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B14 File Offset: 0x00000D14
		internal static DaxQueryExecutionException CreateDataTypeNotSupported(Type type)
		{
			string text = StringUtil.FormatInvariant("Encountered a value of type {0}. This type is not supported.", new object[] { type });
			return new DaxQueryExecutionException("DaxDataTypeNotSupported", text, ErrorSource.User);
		}
	}
}
