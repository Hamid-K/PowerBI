using System;
using System.Data;
using System.Globalization;

namespace Microsoft.Data.Sql
{
	// Token: 0x0200015C RID: 348
	internal static class SqlDataSourceEnumeratorUtil
	{
		// Token: 0x06001A59 RID: 6745 RVA: 0x0006BEF4 File Offset: 0x0006A0F4
		internal static DataTable PrepareDataTable()
		{
			return new DataTable("SqlDataSources")
			{
				Locale = CultureInfo.InvariantCulture,
				Columns = 
				{
					{
						"ServerName",
						typeof(string)
					},
					{
						"InstanceName",
						typeof(string)
					},
					{
						"IsClustered",
						typeof(string)
					},
					{
						"Version",
						typeof(string)
					}
				}
			};
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0006BF84 File Offset: 0x0006A184
		internal static DataTable SetColumnsReadOnly(this DataTable dataTable)
		{
			foreach (object obj in dataTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				dataColumn.ReadOnly = true;
			}
			return dataTable;
		}

		// Token: 0x04000AA5 RID: 2725
		internal const string ServerNameCol = "ServerName";

		// Token: 0x04000AA6 RID: 2726
		internal const string InstanceNameCol = "InstanceName";

		// Token: 0x04000AA7 RID: 2727
		internal const string IsClusteredCol = "IsClustered";

		// Token: 0x04000AA8 RID: 2728
		internal const string VersionNameCol = "Version";

		// Token: 0x04000AA9 RID: 2729
		internal const string Version = "Version:";

		// Token: 0x04000AAA RID: 2730
		internal const string Clustered = "Clustered:";

		// Token: 0x04000AAB RID: 2731
		internal static readonly int s_versionLength = "Version:".Length;

		// Token: 0x04000AAC RID: 2732
		internal static readonly int s_clusteredLength = "Clustered:".Length;

		// Token: 0x04000AAD RID: 2733
		internal static readonly string[] s_endOfServerInstanceDelimiter_Managed = new string[] { ";;" };

		// Token: 0x04000AAE RID: 2734
		internal const char EndOfServerInstanceDelimiter_Native = '\0';

		// Token: 0x04000AAF RID: 2735
		internal const char InstanceKeysDelimiter = ';';

		// Token: 0x04000AB0 RID: 2736
		internal const char ServerNamesAndInstanceDelimiter = '\\';
	}
}
