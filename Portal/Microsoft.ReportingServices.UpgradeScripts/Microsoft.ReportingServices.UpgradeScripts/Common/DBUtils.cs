using System;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000007 RID: 7
	internal class DBUtils
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002170 File Offset: 0x00000370
		private DBUtils()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000024E8 File Offset: 0x000006E8
		public static bool ApplyScript(string connectionString, string script)
		{
			bool flag = false;
			SqlConnection sqlConnection = null;
			try
			{
				sqlConnection = new SqlConnection(connectionString);
				sqlConnection.Open();
				flag = DBUtils.ApplyScript(sqlConnection, script, null);
			}
			finally
			{
				sqlConnection.Close();
			}
			return flag;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000252C File Offset: 0x0000072C
		public static bool ApplyScript(SqlConnection conn, string script, ICommandWrapperFactory commandWrapper)
		{
			string[] array = Regex.Split(script, "^\\s*GO\\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Trim().Length > 0)
				{
					using (IDbCommand dbCommand = DBUtils.WrapCommand(commandWrapper, new SqlCommand(array[i], conn)))
					{
						dbCommand.CommandTimeout = 1800;
						dbCommand.ExecuteNonQuery();
					}
				}
			}
			return true;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000025A4 File Offset: 0x000007A4
		private static IDbCommand WrapCommand(ICommandWrapperFactory wrapper, SqlCommand toWrap)
		{
			IDbCommand dbCommand = toWrap;
			if (wrapper != null)
			{
				dbCommand = wrapper.WrapSqlCommand(toWrap);
			}
			return dbCommand;
		}

		// Token: 0x04000035 RID: 53
		public const string SP2DBVersion = "C.0.6.54";

		// Token: 0x04000036 RID: 54
		public const string YukonRTMDBVersion = "C.0.8.40";

		// Token: 0x04000037 RID: 55
		public const string YukonSP1DBVersion = "C.0.8.43";

		// Token: 0x04000038 RID: 56
		public const string YukonSP2DBVersion = "C.0.8.54";

		// Token: 0x04000039 RID: 57
		public const string KatmaiCTP5DBVersion = "C.0.9.18";

		// Token: 0x0400003A RID: 58
		public const string KatmaiCTP6DBVersion = "C.0.9.34";

		// Token: 0x0400003B RID: 59
		public const string KatmaiRTMDBVersion = "C.0.9.45";

		// Token: 0x0400003C RID: 60
		public const string SP2SQLVersion = "SQL Server 2000 SP2";

		// Token: 0x0400003D RID: 61
		public const string YukonRTMSQLVersion = "SQL Server 2005 RTM";

		// Token: 0x0400003E RID: 62
		public const string YukonSP1SQLVersion = "SQL Server 2005 SP1";

		// Token: 0x0400003F RID: 63
		public const string YukonSP2SQLVersion = "SQL Server 2005 SP2";

		// Token: 0x04000040 RID: 64
		public const string KatmaiCTP5SQLVersion = "SQL Server 2008 CTP5";

		// Token: 0x04000041 RID: 65
		public const string KatmaiCTP6SQLVersion = "SQL Server 2008 CTP6";

		// Token: 0x04000042 RID: 66
		public const string KatmaiRTMSQLVersion = "SQL Server 2008";
	}
}
