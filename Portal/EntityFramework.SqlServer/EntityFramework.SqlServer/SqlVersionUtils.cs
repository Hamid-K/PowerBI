using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer.Resources;
using System.Globalization;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200001A RID: 26
	internal static class SqlVersionUtils
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		internal static SqlVersion GetSqlVersion(DbConnection connection)
		{
			int num = int.Parse(DbInterception.Dispatch.Connection.GetServerVersion(connection, new DbInterceptionContext()).Substring(0, 2), CultureInfo.InvariantCulture);
			if (num >= 11)
			{
				return SqlVersion.Sql11;
			}
			if (num == 10)
			{
				return SqlVersion.Sql10;
			}
			if (num == 9)
			{
				return SqlVersion.Sql9;
			}
			return SqlVersion.Sql8;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000E904 File Offset: 0x0000CB04
		internal static ServerType GetServerType(DbConnection connection)
		{
			ServerType serverType;
			using (DbCommand dbCommand = connection.CreateCommand())
			{
				dbCommand.CommandText = "select cast(serverproperty('EngineEdition') as int)";
				using (DbDataReader dbDataReader = DbInterception.Dispatch.Command.Reader(dbCommand, new DbCommandInterceptionContext()))
				{
					dbDataReader.Read();
					serverType = ((dbDataReader.GetInt32(0) == 5) ? ServerType.Cloud : ServerType.OnPremises);
				}
			}
			return serverType;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000E984 File Offset: 0x0000CB84
		internal static string GetVersionHint(SqlVersion version, ServerType serverType)
		{
			if (serverType == ServerType.Cloud)
			{
				return "2012.Azure";
			}
			if (version <= SqlVersion.Sql9)
			{
				if (version == SqlVersion.Sql8)
				{
					return "2000";
				}
				if (version == SqlVersion.Sql9)
				{
					return "2005";
				}
			}
			else
			{
				if (version == SqlVersion.Sql10)
				{
					return "2008";
				}
				if (version == SqlVersion.Sql11)
				{
					return "2012";
				}
			}
			throw new ArgumentException(Strings.UnableToDetermineStoreVersion);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		internal static SqlVersion GetSqlVersion(string versionHint)
		{
			if (!string.IsNullOrEmpty(versionHint) && versionHint != null)
			{
				if (versionHint == "2000")
				{
					return SqlVersion.Sql8;
				}
				if (versionHint == "2005")
				{
					return SqlVersion.Sql9;
				}
				if (versionHint == "2008")
				{
					return SqlVersion.Sql10;
				}
				if (versionHint == "2012")
				{
					return SqlVersion.Sql11;
				}
				if (versionHint == "2012.Azure")
				{
					return SqlVersion.Sql11;
				}
			}
			throw new ArgumentException(Strings.UnableToDetermineStoreVersion);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000EA50 File Offset: 0x0000CC50
		internal static bool IsPreKatmai(SqlVersion sqlVersion)
		{
			return sqlVersion == SqlVersion.Sql8 || sqlVersion == SqlVersion.Sql9;
		}
	}
}
