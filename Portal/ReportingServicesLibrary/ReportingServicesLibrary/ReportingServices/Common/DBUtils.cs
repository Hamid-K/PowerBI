using System;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000355 RID: 853
	internal class DBUtils
	{
		// Token: 0x06001C2E RID: 7214 RVA: 0x000025F4 File Offset: 0x000007F4
		private DBUtils()
		{
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000720C0 File Offset: 0x000702C0
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

		// Token: 0x06001C30 RID: 7216 RVA: 0x00072104 File Offset: 0x00070304
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

		// Token: 0x06001C31 RID: 7217 RVA: 0x0007217C File Offset: 0x0007037C
		private static IDbCommand WrapCommand(ICommandWrapperFactory wrapper, SqlCommand toWrap)
		{
			IDbCommand dbCommand = toWrap;
			if (wrapper != null)
			{
				dbCommand = wrapper.WrapSqlCommand(toWrap);
			}
			return dbCommand;
		}

		// Token: 0x04000B9E RID: 2974
		public const string SP2DBVersion = "C.0.6.54";

		// Token: 0x04000B9F RID: 2975
		public const string YukonRTMDBVersion = "C.0.8.40";

		// Token: 0x04000BA0 RID: 2976
		public const string YukonSP1DBVersion = "C.0.8.43";

		// Token: 0x04000BA1 RID: 2977
		public const string YukonSP2DBVersion = "C.0.8.54";

		// Token: 0x04000BA2 RID: 2978
		public const string KatmaiCTP5DBVersion = "C.0.9.18";

		// Token: 0x04000BA3 RID: 2979
		public const string KatmaiCTP6DBVersion = "C.0.9.34";

		// Token: 0x04000BA4 RID: 2980
		public const string KatmaiRTMDBVersion = "C.0.9.45";

		// Token: 0x04000BA5 RID: 2981
		public const string SP2SQLVersion = "SQL Server 2000 SP2";

		// Token: 0x04000BA6 RID: 2982
		public const string YukonRTMSQLVersion = "SQL Server 2005 RTM";

		// Token: 0x04000BA7 RID: 2983
		public const string YukonSP1SQLVersion = "SQL Server 2005 SP1";

		// Token: 0x04000BA8 RID: 2984
		public const string YukonSP2SQLVersion = "SQL Server 2005 SP2";

		// Token: 0x04000BA9 RID: 2985
		public const string KatmaiCTP5SQLVersion = "SQL Server 2008 CTP5";

		// Token: 0x04000BAA RID: 2986
		public const string KatmaiCTP6SQLVersion = "SQL Server 2008 CTP6";

		// Token: 0x04000BAB RID: 2987
		public const string KatmaiRTMSQLVersion = "SQL Server 2008";
	}
}
