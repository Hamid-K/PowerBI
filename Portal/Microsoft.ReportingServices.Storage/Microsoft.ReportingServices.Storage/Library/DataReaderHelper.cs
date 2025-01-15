using System;
using System.Data;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003B RID: 59
	internal static class DataReaderHelper
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00009C28 File Offset: 0x00007E28
		internal static byte[] ReadAllBytes(IDataRecord row, int i)
		{
			long bytes = row.GetBytes(i, 0L, null, 0, 0);
			if (bytes > 2147483647L)
			{
				throw new InternalCatalogException("Byte count exceeds Int32.MaxValue: " + bytes.ToString(CultureInfo.InvariantCulture));
			}
			long num = 0L;
			byte[] array = new byte[bytes];
			while (num != bytes)
			{
				long bytes2 = row.GetBytes(i, num, array, (int)num, (int)(bytes - num));
				num += bytes2;
				RSTrace.CatalogTrace.Assert(num <= bytes, "totalBytesRead");
			}
			return array;
		}
	}
}
