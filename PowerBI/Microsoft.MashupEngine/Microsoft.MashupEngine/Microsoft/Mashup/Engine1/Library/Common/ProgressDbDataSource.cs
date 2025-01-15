using System;
using System.Data.Common;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001107 RID: 4359
	internal static class ProgressDbDataSource
	{
		// Token: 0x06007201 RID: 29185 RVA: 0x00187E19 File Offset: 0x00186019
		public static string GetDataSource(DbConnection connection)
		{
			return ProgressDbDataSource.GetDataSource(connection.DataSource, connection.Database);
		}

		// Token: 0x06007202 RID: 29186 RVA: 0x00187E2C File Offset: 0x0018602C
		public static string GetDataSource(string dataSource, string database)
		{
			if (database.Length > 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", dataSource, database);
			}
			return dataSource;
		}
	}
}
