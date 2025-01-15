using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Storage;
using Microsoft.Data.SqlClient;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200001F RID: 31
	public class RsCatalog
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x000047EB File Offset: 0x000029EB
		public RsCatalog(string instanceName, string reportServerDatabaseName)
		{
			this._conn = new MeteredSqlConnection(RsCatalog.GenerateMasterConnectionStringFromInstanceId(instanceName));
			this._reportServerDatabaseName = reportServerDatabaseName;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000480B File Offset: 0x00002A0B
		public void CreateCatalog()
		{
			Logger.Info("Dropping Existing catalog database", Array.Empty<object>());
			this.ExecuteScript("DropReportServer");
			Logger.Info("Creating catalog database", Array.Empty<object>());
			this.ExecuteScript("ReportServer");
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004844 File Offset: 0x00002A44
		private void ExecuteScript(string fileName)
		{
			string text = this.GetCatalogScriptContents(fileName);
			string text2 = this._reportServerDatabaseName + "TempDB";
			text = text.Replace("N'ReportServer'", string.Format("N'{0}'", this._reportServerDatabaseName));
			text = text.Replace("N'ReportServerTempDB'", string.Format("N'{0}'", text2));
			text = text.Replace("[ReportServer]", string.Format("[{0}]", this._reportServerDatabaseName));
			text = text.Replace("[ReportServerTempDB]", string.Format("[{0}]", text2));
			this._conn.ExecuteBatchScript(text);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000048DC File Offset: 0x00002ADC
		public bool CheckIfCatalogExists()
		{
			Logger.Info("Checking if catalog database exists.", Array.Empty<object>());
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("DbName", "ReportServer");
			return this._conn.ExecuteScalar<int>("select count(*) from [master].[sys].[databases] where name = @DbName", dictionary) != 0;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004924 File Offset: 0x00002B24
		private string GetCatalogScriptContents(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = string.Format("{0}.Scripts.{1}.sql", executingAssembly.GetName().Name, fileName);
			string text2;
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text))
			{
				if (manifestResourceStream == null)
				{
					throw new MissingManifestResourceException(text);
				}
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					text2 = streamReader.ReadToEnd();
				}
			}
			return text2;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000049A8 File Offset: 0x00002BA8
		private static string GenerateMasterConnectionStringFromInstanceId(string sqlInstance)
		{
			return new SqlConnectionStringBuilder
			{
				DataSource = sqlInstance,
				InitialCatalog = "master",
				IntegratedSecurity = true,
				ApplicationName = "Report Server",
				Encrypt = false
			}.ToString();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000049E4 File Offset: 0x00002BE4
		public void CreateUserAndGrantRoles(AccountCredentials accountCredentials)
		{
			string text = string.Format("\r\n            IF NOT EXISTS \r\n                (SELECT name  \r\n                 FROM master.sys.server_principals\r\n                 WHERE name = '{0}')\r\n            BEGIN\r\n                CREATE LOGIN [{0}] from windows\r\n            END\r\n        ", accountCredentials.DomainUser);
			this._conn.ExecuteNonQuery(text, null);
			this.AddUserAndRoleForDatabase(this._reportServerDatabaseName, accountCredentials);
			this.AddUserAndRoleForDatabase(this._reportServerDatabaseName + "TempDB", accountCredentials);
			this.AddUserAndRoleForDatabase("master", accountCredentials);
			this.AddUserAndRoleForDatabase("msdb", accountCredentials);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004A4C File Offset: 0x00002C4C
		private void AddUserAndRoleForDatabase(string databaseName, AccountCredentials accountCredentials)
		{
			string text = "use " + databaseName;
			this._conn.ExecuteNonQuery(text, null);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("LoginName", accountCredentials.DomainUser);
			string text2 = this._conn.ExecuteScalar<string>("\r\n            select sysusers.name as DbUser from sys.sysusers\r\n            join sys.syslogins on sysusers.sid = syslogins.sid\r\n            where syslogins.name = @LoginName\r\n        ", dictionary);
			if (text2 == null)
			{
				text = string.Format("\r\n            IF NOT EXISTS\r\n                (SELECT name\r\n                 FROM sys.database_principals\r\n                 WHERE name = '{0}')\r\n            BEGIN\r\n                CREATE USER [{0}] FOR LOGIN [{0}] \r\n            END\r\n        ", accountCredentials.DomainUser);
				this._conn.ExecuteNonQuery(text, null);
				text2 = accountCredentials.DomainUser;
			}
			if (text2 != "dbo")
			{
				this.AddRole("RSExecRole", text2);
				this.AddRole("db_owner", text2);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004AE8 File Offset: 0x00002CE8
		private void AddRole(string roleName, string memberName)
		{
			string text = string.Format("EXEC sys.sp_addrolemember '{0}', '{1}'", roleName, memberName);
			this._conn.ExecuteNonQuery(text, null);
		}

		// Token: 0x040000CB RID: 203
		private const string MasterDb = "master";

		// Token: 0x040000CC RID: 204
		private const string ReportServerApplication = "Report Server";

		// Token: 0x040000CD RID: 205
		private const string CheckForCatalogDatabaseQuery = "select count(*) from [master].[sys].[databases] where name = @DbName";

		// Token: 0x040000CE RID: 206
		private const string CreateLoginStatement = "\r\n            IF NOT EXISTS \r\n                (SELECT name  \r\n                 FROM master.sys.server_principals\r\n                 WHERE name = '{0}')\r\n            BEGIN\r\n                CREATE LOGIN [{0}] from windows\r\n            END\r\n        ";

		// Token: 0x040000CF RID: 207
		private const string CheckForExistingUserInDatabaseQuery = "\r\n            select sysusers.name as DbUser from sys.sysusers\r\n            join sys.syslogins on sysusers.sid = syslogins.sid\r\n            where syslogins.name = @LoginName\r\n        ";

		// Token: 0x040000D0 RID: 208
		private const string CreateUserStatement = "\r\n            IF NOT EXISTS\r\n                (SELECT name\r\n                 FROM sys.database_principals\r\n                 WHERE name = '{0}')\r\n            BEGIN\r\n                CREATE USER [{0}] FOR LOGIN [{0}] \r\n            END\r\n        ";

		// Token: 0x040000D1 RID: 209
		private const string AddRoleMemberStatement = "EXEC sys.sp_addrolemember '{0}', '{1}'";

		// Token: 0x040000D2 RID: 210
		private readonly ISqlAccess _conn;

		// Token: 0x040000D3 RID: 211
		private string _reportServerDatabaseName;
	}
}
