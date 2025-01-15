using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Transactions;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000033 RID: 51
	[Target("Database")]
	public class DatabaseTarget : Target, IInstallable
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x0000B698 File Offset: 0x00009898
		public DatabaseTarget()
		{
			this.InstallDdlCommands = new List<DatabaseCommandInfo>();
			this.UninstallDdlCommands = new List<DatabaseCommandInfo>();
			this.DBProvider = "sqlserver";
			this.DBHost = ".";
			this.ConnectionStringsSettings = global::System.Configuration.ConfigurationManager.ConnectionStrings;
			this.CommandType = CommandType.Text;
			base.OptimizeBufferReuse = base.GetType() == typeof(DatabaseTarget);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000B714 File Offset: 0x00009914
		public DatabaseTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000B723 File Offset: 0x00009923
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000B72B File Offset: 0x0000992B
		[RequiredParameter]
		[DefaultValue("sqlserver")]
		public string DBProvider { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000B734 File Offset: 0x00009934
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0000B73C File Offset: 0x0000993C
		public string ConnectionStringName { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000B745 File Offset: 0x00009945
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0000B74D File Offset: 0x0000994D
		public Layout ConnectionString { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000B756 File Offset: 0x00009956
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x0000B75E File Offset: 0x0000995E
		public Layout InstallConnectionString { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000B767 File Offset: 0x00009967
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x0000B76F File Offset: 0x0000996F
		[ArrayParameter(typeof(DatabaseCommandInfo), "install-command")]
		public IList<DatabaseCommandInfo> InstallDdlCommands { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000B778 File Offset: 0x00009978
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0000B780 File Offset: 0x00009980
		[ArrayParameter(typeof(DatabaseCommandInfo), "uninstall-command")]
		public IList<DatabaseCommandInfo> UninstallDdlCommands { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000B789 File Offset: 0x00009989
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0000B791 File Offset: 0x00009991
		[DefaultValue(false)]
		public bool KeepConnection { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000B79A File Offset: 0x0000999A
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000B7A2 File Offset: 0x000099A2
		[Obsolete("Value will be ignored as logging code always executes outside of a transaction. Marked obsolete on NLog 4.0 and it will be removed in NLog 6.")]
		public bool? UseTransactions { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000B7AB File Offset: 0x000099AB
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0000B7B3 File Offset: 0x000099B3
		public Layout DBHost { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000B7BC File Offset: 0x000099BC
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x0000B7C4 File Offset: 0x000099C4
		public Layout DBUserName { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000B7CD File Offset: 0x000099CD
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x0000B7D5 File Offset: 0x000099D5
		public Layout DBPassword { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0000B7DE File Offset: 0x000099DE
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0000B7E6 File Offset: 0x000099E6
		public Layout DBDatabase { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000B7EF File Offset: 0x000099EF
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0000B7F7 File Offset: 0x000099F7
		[RequiredParameter]
		public Layout CommandText { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000B800 File Offset: 0x00009A00
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0000B808 File Offset: 0x00009A08
		[DefaultValue(CommandType.Text)]
		public CommandType CommandType { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000B811 File Offset: 0x00009A11
		[ArrayParameter(typeof(DatabaseParameterInfo), "parameter")]
		public IList<DatabaseParameterInfo> Parameters { get; } = new List<DatabaseParameterInfo>();

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000B819 File Offset: 0x00009A19
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0000B821 File Offset: 0x00009A21
		internal DbProviderFactory ProviderFactory { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000B82A File Offset: 0x00009A2A
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x0000B832 File Offset: 0x00009A32
		internal ConnectionStringSettingsCollection ConnectionStringsSettings { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000B83B File Offset: 0x00009A3B
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0000B843 File Offset: 0x00009A43
		internal Type ConnectionType { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000B84C File Offset: 0x00009A4C
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0000B876 File Offset: 0x00009A76
		private IPropertyTypeConverter PropertyTypeConverter
		{
			get
			{
				IPropertyTypeConverter propertyTypeConverter;
				if ((propertyTypeConverter = this._propertyTypeConverter) == null)
				{
					propertyTypeConverter = (this._propertyTypeConverter = ConfigurationItemFactory.Default.PropertyTypeConverter);
				}
				return propertyTypeConverter;
			}
			set
			{
				this._propertyTypeConverter = value;
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000B87F File Offset: 0x00009A7F
		public void Install(InstallationContext installationContext)
		{
			this.RunInstallCommands(installationContext, this.InstallDdlCommands);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000B88E File Offset: 0x00009A8E
		public void Uninstall(InstallationContext installationContext)
		{
			this.RunInstallCommands(installationContext, this.UninstallDdlCommands);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		public bool? IsInstalled(InstallationContext installationContext)
		{
			return null;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		internal IDbConnection OpenConnection(string connectionString)
		{
			IDbConnection dbConnection;
			if (this.ProviderFactory != null)
			{
				dbConnection = this.ProviderFactory.CreateConnection();
			}
			else
			{
				dbConnection = (IDbConnection)Activator.CreateInstance(this.ConnectionType);
			}
			if (dbConnection == null)
			{
				throw new NLogRuntimeException("Creation of connection failed");
			}
			dbConnection.ConnectionString = connectionString;
			dbConnection.Open();
			return dbConnection;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000B908 File Offset: 0x00009B08
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (this.UseTransactions != null)
			{
				InternalLogger.Warn<string>("DatabaseTarget(Name={0}): UseTransactions property is obsolete and will not be used - will be removed in NLog 6", base.Name);
			}
			bool flag = false;
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.ConnectionStringName))
			{
				ConnectionStringSettings connectionStringSettings = this.ConnectionStringsSettings[this.ConnectionStringName];
				if (connectionStringSettings == null)
				{
					throw new NLogConfigurationException("Connection string '" + this.ConnectionStringName + "' is not declared in <connectionStrings /> section.");
				}
				string connectionString = connectionStringSettings.ConnectionString;
				if (!string.IsNullOrEmpty((connectionString != null) ? connectionString.Trim() : null))
				{
					this.ConnectionString = SimpleLayout.Escape(connectionStringSettings.ConnectionString.Trim());
				}
				string providerName = connectionStringSettings.ProviderName;
				text = ((providerName != null) ? providerName.Trim() : null) ?? string.Empty;
			}
			if (this.ConnectionString != null)
			{
				text = this.InitConnectionString(text);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = this.GetProviderNameFromDbProviderFactories(text);
			}
			if (!string.IsNullOrEmpty(text))
			{
				flag = this.InitProviderFactory(text);
			}
			if (!flag)
			{
				try
				{
					this.SetConnectionType();
					if (this.ConnectionType == null)
					{
						InternalLogger.Warn<string, string>("DatabaseTarget(Name={0}): No ConnectionType created from DBProvider={1}", base.Name, this.DBProvider);
					}
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "DatabaseTarget(Name={0}): Failed to create ConnectionType from DBProvider={1}", new object[] { base.Name, this.DBProvider });
					throw;
				}
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000BA68 File Offset: 0x00009C68
		private string InitConnectionString(string providerName)
		{
			try
			{
				string text = this.BuildConnectionString(LogEventInfo.CreateNullEvent());
				DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
				{
					ConnectionString = text
				};
				object obj;
				if (dbConnectionStringBuilder.TryGetValue("provider connection string", out obj))
				{
					object obj2;
					if (dbConnectionStringBuilder.TryGetValue("provider", out obj2))
					{
						string text2 = obj2.ToString();
						providerName = ((text2 != null) ? text2.Trim() : null) ?? string.Empty;
					}
					this.ConnectionString = SimpleLayout.Escape(obj.ToString());
				}
			}
			catch (Exception ex)
			{
				if (!string.IsNullOrEmpty(this.ConnectionStringName))
				{
					InternalLogger.Warn(ex, "DatabaseTarget(Name={0}): DbConnectionStringBuilder failed to parse '{1}' ConnectionString", new object[] { base.Name, this.ConnectionStringName });
				}
				else
				{
					InternalLogger.Warn(ex, "DatabaseTarget(Name={0}): DbConnectionStringBuilder failed to parse ConnectionString", new object[] { base.Name });
				}
			}
			return providerName;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000BB44 File Offset: 0x00009D44
		private bool InitProviderFactory(string providerName)
		{
			bool flag;
			try
			{
				this.ProviderFactory = DbProviderFactories.GetFactory(providerName);
				flag = true;
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "DatabaseTarget(Name={0}): DbProviderFactories failed to get factory from ProviderName={1}", new object[] { base.Name, providerName });
				throw;
			}
			return flag;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000BB94 File Offset: 0x00009D94
		private string GetProviderNameFromDbProviderFactories(string providerName)
		{
			string dbprovider = this.DBProvider;
			string text = ((dbprovider != null) ? dbprovider.Trim() : null) ?? string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				foreach (object obj in DbProviderFactories.GetFactoryClasses().Rows)
				{
					string text2 = (string)((DataRow)obj)["InvariantName"];
					if (string.Equals(text2, text, StringComparison.OrdinalIgnoreCase))
					{
						providerName = text2;
						break;
					}
				}
			}
			return providerName;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000BC30 File Offset: 0x00009E30
		private void SetConnectionType()
		{
			string text = this.DBProvider.ToUpperInvariant();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1904873583U)
			{
				if (num <= 487347587U)
				{
					if (num != 118456485U)
					{
						if (num != 487347587U)
						{
							goto IL_0181;
						}
						if (!(text == "SYSTEM.DATA.SQLCLIENT"))
						{
							goto IL_0181;
						}
						goto IL_0113;
					}
					else if (!(text == "ODBC"))
					{
						goto IL_0181;
					}
				}
				else if (num != 1102819296U)
				{
					if (num != 1904873583U)
					{
						goto IL_0181;
					}
					if (!(text == "OLEDB"))
					{
						goto IL_0181;
					}
					Assembly assembly = typeof(IDbConnection).GetAssembly();
					this.ConnectionType = assembly.GetType("System.Data.OleDb.OleDbConnection", true, true);
					return;
				}
				else if (!(text == "SYSTEM.DATA.ODBC"))
				{
					goto IL_0181;
				}
				Assembly assembly2 = typeof(IDbConnection).GetAssembly();
				this.ConnectionType = assembly2.GetType("System.Data.Odbc.OdbcConnection", true, true);
				return;
			}
			if (num <= 2293860891U)
			{
				if (num != 1947209383U)
				{
					if (num != 2293860891U)
					{
						goto IL_0181;
					}
					if (!(text == "MSSQL"))
					{
						goto IL_0181;
					}
				}
				else if (!(text == "MICROSOFT"))
				{
					goto IL_0181;
				}
			}
			else if (num != 2359868734U)
			{
				if (num != 3987388052U)
				{
					goto IL_0181;
				}
				if (!(text == "SQLSERVER"))
				{
					goto IL_0181;
				}
			}
			else if (!(text == "MSDE"))
			{
				goto IL_0181;
			}
			IL_0113:
			Assembly assembly3 = typeof(IDbConnection).GetAssembly();
			this.ConnectionType = assembly3.GetType("System.Data.SqlClient.SqlConnection", true, true);
			return;
			IL_0181:
			this.ConnectionType = Type.GetType(this.DBProvider, true, true);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000BDD1 File Offset: 0x00009FD1
		protected override void CloseTarget()
		{
			this.PropertyTypeConverter = null;
			base.CloseTarget();
			InternalLogger.Trace<string>("DatabaseTarget(Name={0}): Close connection because of CloseTarget", base.Name);
			this.CloseConnection();
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		protected override void Write(LogEventInfo logEvent)
		{
			try
			{
				this.WriteEventToDatabase(logEvent, this.BuildConnectionString(logEvent));
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "DatabaseTarget(Name={0}): Error when writing to database.", new object[] { base.Name });
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				InternalLogger.Trace<string>("DatabaseTarget(Name={0}): Close connection because of error", base.Name);
				this.CloseConnection();
				throw;
			}
			finally
			{
				if (!this.KeepConnection)
				{
					InternalLogger.Trace<string>("DatabaseTarget(Name={0}): Close connection (KeepConnection = false).", base.Name);
					this.CloseConnection();
				}
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000BE90 File Offset: 0x0000A090
		[Obsolete("Instead override Write(IList<AsyncLogEventInfo> logEvents. Marked obsolete on NLog 4.5")]
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			this.Write(logEvents);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000BE9C File Offset: 0x0000A09C
		protected override void Write(IList<AsyncLogEventInfo> logEvents)
		{
			if (this._buildConnectionStringDelegate == null)
			{
				this._buildConnectionStringDelegate = (AsyncLogEventInfo l) => this.BuildConnectionString(l.LogEvent);
			}
			SortHelpers.ReadOnlySingleBucketDictionary<string, IList<AsyncLogEventInfo>> readOnlySingleBucketDictionary = logEvents.BucketSort(this._buildConnectionStringDelegate);
			try
			{
				foreach (KeyValuePair<string, IList<AsyncLogEventInfo>> keyValuePair in readOnlySingleBucketDictionary)
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						AsyncLogEventInfo asyncLogEventInfo = keyValuePair.Value[i];
						try
						{
							this.WriteEventToDatabase(asyncLogEventInfo.LogEvent, keyValuePair.Key);
							asyncLogEventInfo.Continuation(null);
						}
						catch (Exception ex)
						{
							InternalLogger.Error(ex, "DatabaseTarget(Name={0}): Error when writing to database.", new object[] { base.Name });
							if (ex.MustBeRethrownImmediately())
							{
								throw;
							}
							InternalLogger.Trace<string>("DatabaseTarget(Name={0}): Close connection because of exception", base.Name);
							this.CloseConnection();
							asyncLogEventInfo.Continuation(ex);
							if (ex.MustBeRethrown())
							{
								throw;
							}
						}
					}
				}
			}
			finally
			{
				if (!this.KeepConnection)
				{
					InternalLogger.Trace<string>("DatabaseTarget(Name={0}): Close connection because of KeepConnection=false", base.Name);
					this.CloseConnection();
				}
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		private void WriteEventToDatabase(LogEventInfo logEvent, string connectionString)
		{
			string text = base.RenderLogEvent(this.CommandText, logEvent);
			InternalLogger.Trace<string, CommandType, string>("DatabaseTarget(Name={0}): Executing {1}: {2}", base.Name, this.CommandType, text);
			using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
			{
				this.EnsureConnectionOpen(connectionString);
				using (IDbCommand dbCommand = this.CreateDbCommandWithParameters(logEvent, this.CommandType, text, this.Parameters))
				{
					int num = dbCommand.ExecuteNonQuery();
					InternalLogger.Trace<string, int>("DatabaseTarget(Name={0}): Finished execution, result = {1}", base.Name, num);
				}
				transactionScope.Complete();
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000C098 File Offset: 0x0000A298
		private IDbCommand CreateDbCommandWithParameters(LogEventInfo logEvent, CommandType commandType, string dbCommandText, IList<DatabaseParameterInfo> databaseParameterInfos)
		{
			IDbCommand dbCommand = this._activeConnection.CreateCommand();
			dbCommand.CommandType = commandType;
			dbCommand.CommandText = dbCommandText;
			for (int i = 0; i < databaseParameterInfos.Count; i++)
			{
				DatabaseParameterInfo databaseParameterInfo = databaseParameterInfos[i];
				IDbDataParameter dbDataParameter = this.CreateDatabaseParameter(dbCommand, databaseParameterInfo);
				object databaseParameterValue = this.GetDatabaseParameterValue(logEvent, databaseParameterInfo);
				dbDataParameter.Value = databaseParameterValue;
				dbCommand.Parameters.Add(dbDataParameter);
				InternalLogger.Trace<string, object, DbType>("  DatabaseTarget: Parameter: '{0}' = '{1}' ({2})", dbDataParameter.ParameterName, dbDataParameter.Value, dbDataParameter.DbType);
			}
			return dbCommand;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000C120 File Offset: 0x0000A320
		protected string BuildConnectionString(LogEventInfo logEvent)
		{
			if (this.ConnectionString != null)
			{
				return base.RenderLogEvent(this.ConnectionString, logEvent);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Server=");
			stringBuilder.Append(base.RenderLogEvent(this.DBHost, logEvent));
			stringBuilder.Append(";");
			if (this.DBUserName == null)
			{
				stringBuilder.Append("Trusted_Connection=SSPI;");
			}
			else
			{
				stringBuilder.Append("User id=");
				stringBuilder.Append(base.RenderLogEvent(this.DBUserName, logEvent));
				stringBuilder.Append(";Password=");
				stringBuilder.Append(base.RenderLogEvent(this.DBPassword, logEvent));
				stringBuilder.Append(";");
			}
			if (this.DBDatabase != null)
			{
				stringBuilder.Append("Database=");
				stringBuilder.Append(base.RenderLogEvent(this.DBDatabase, logEvent));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000C208 File Offset: 0x0000A408
		private void EnsureConnectionOpen(string connectionString)
		{
			if (this._activeConnection != null && this._activeConnectionString != connectionString)
			{
				InternalLogger.Trace<string>("DatabaseTarget(Name={0}): Close connection because of opening new.", base.Name);
				this.CloseConnection();
			}
			if (this._activeConnection != null)
			{
				return;
			}
			InternalLogger.Trace<string>("DatabaseTarget(Name={0}): Open connection.", base.Name);
			this._activeConnection = this.OpenConnection(connectionString);
			this._activeConnectionString = connectionString;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000C26E File Offset: 0x0000A46E
		private void CloseConnection()
		{
			this._activeConnectionString = null;
			if (this._activeConnection != null)
			{
				this._activeConnection.Close();
				this._activeConnection.Dispose();
				this._activeConnection = null;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000C29C File Offset: 0x0000A49C
		private void RunInstallCommands(InstallationContext installationContext, IEnumerable<DatabaseCommandInfo> commands)
		{
			LogEventInfo logEventInfo = installationContext.CreateLogEvent();
			try
			{
				foreach (DatabaseCommandInfo databaseCommandInfo in commands)
				{
					string connectionStringFromCommand = this.GetConnectionStringFromCommand(databaseCommandInfo, logEventInfo);
					if (this.ConnectionType == null)
					{
						this.SetConnectionType();
					}
					this.EnsureConnectionOpen(connectionStringFromCommand);
					string text = base.RenderLogEvent(databaseCommandInfo.Text, logEventInfo);
					installationContext.Trace("DatabaseTarget(Name={0}) - Executing {1} '{2}'", new object[] { base.Name, databaseCommandInfo.CommandType, text });
					using (IDbCommand dbCommand = this.CreateDbCommandWithParameters(logEventInfo, databaseCommandInfo.CommandType, text, databaseCommandInfo.Parameters))
					{
						try
						{
							dbCommand.ExecuteNonQuery();
						}
						catch (Exception ex)
						{
							if (ex.MustBeRethrownImmediately())
							{
								throw;
							}
							if (!databaseCommandInfo.IgnoreFailures && !installationContext.IgnoreFailures)
							{
								installationContext.Error(ex.Message, new object[0]);
								throw;
							}
							installationContext.Warning(ex.Message, new object[0]);
						}
					}
				}
			}
			finally
			{
				InternalLogger.Trace<string>("DatabaseTarget(Name={0}): Close connection after install.", base.Name);
				this.CloseConnection();
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000C42C File Offset: 0x0000A62C
		private string GetConnectionStringFromCommand(DatabaseCommandInfo commandInfo, LogEventInfo logEvent)
		{
			string text;
			if (commandInfo.ConnectionString != null)
			{
				text = base.RenderLogEvent(commandInfo.ConnectionString, logEvent);
			}
			else if (this.InstallConnectionString != null)
			{
				text = base.RenderLogEvent(this.InstallConnectionString, logEvent);
			}
			else
			{
				text = this.BuildConnectionString(logEvent);
			}
			return text;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000C474 File Offset: 0x0000A674
		protected virtual IDbDataParameter CreateDatabaseParameter(IDbCommand command, DatabaseParameterInfo parameterInfo)
		{
			IDbDataParameter dbDataParameter = command.CreateParameter();
			dbDataParameter.Direction = ParameterDirection.Input;
			if (parameterInfo.Name != null)
			{
				dbDataParameter.ParameterName = parameterInfo.Name;
			}
			if (parameterInfo.Size != 0)
			{
				dbDataParameter.Size = parameterInfo.Size;
			}
			if (parameterInfo.Precision != 0)
			{
				dbDataParameter.Precision = parameterInfo.Precision;
			}
			if (parameterInfo.Scale != 0)
			{
				dbDataParameter.Scale = parameterInfo.Scale;
			}
			try
			{
				if (!parameterInfo.SetDbType(dbDataParameter))
				{
					InternalLogger.Warn<string, string>("  DatabaseTarget: Parameter: '{0}' - Failed to assign DbType={1}", parameterInfo.Name, parameterInfo.DbType);
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				InternalLogger.Error(ex, "  DatabaseTarget: Parameter: '{0}' - Failed to assign DbType={1}", new object[] { parameterInfo.Name, parameterInfo.DbType });
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
			return dbDataParameter;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000C54C File Offset: 0x0000A74C
		protected internal virtual object GetDatabaseParameterValue(LogEventInfo logEvent, DatabaseParameterInfo parameterInfo)
		{
			Type parameterType = parameterInfo.ParameterType;
			if (string.IsNullOrEmpty(parameterInfo.Format) && parameterType == typeof(string))
			{
				return base.RenderLogEvent(parameterInfo.Layout, logEvent) ?? string.Empty;
			}
			IFormatProvider dbParameterCulture = this.GetDbParameterCulture(logEvent, parameterInfo);
			object obj;
			if (this.TryGetConvertedRawValue(logEvent, parameterInfo, parameterType, dbParameterCulture, out obj))
			{
				return obj ?? DatabaseTarget.CreateDefaultValue(parameterType);
			}
			object obj2;
			try
			{
				InternalLogger.Trace<string, string>("  DatabaseTarget: Attempt to convert layout value for '{0}' into {1}", parameterInfo.Name, (parameterType != null) ? parameterType.Name : null);
				string text = base.RenderLogEvent(parameterInfo.Layout, logEvent);
				if (string.IsNullOrEmpty(text))
				{
					obj2 = DatabaseTarget.CreateDefaultValue(parameterType);
				}
				else
				{
					obj2 = this.PropertyTypeConverter.Convert(text, parameterType, parameterInfo.Format, dbParameterCulture) ?? DBNull.Value;
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				InternalLogger.Warn(ex, "  DatabaseTarget: Failed to convert layout value for '{0}' into {1}", new object[]
				{
					parameterInfo.Name,
					(parameterType != null) ? parameterType.Name : null
				});
				if (ex.MustBeRethrown())
				{
					throw;
				}
				obj2 = DatabaseTarget.CreateDefaultValue(parameterType);
			}
			return obj2;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000C678 File Offset: 0x0000A878
		private bool TryGetConvertedRawValue(LogEventInfo logEvent, DatabaseParameterInfo parameterInfo, Type dbParameterType, IFormatProvider dbParameterCulture, out object value)
		{
			object obj;
			if (parameterInfo.Layout.TryGetRawValue(logEvent, out obj))
			{
				try
				{
					InternalLogger.Trace<string, string>("  DatabaseTarget: Attempt to convert raw value for '{0}' into {1}", parameterInfo.Name, (dbParameterType != null) ? dbParameterType.Name : null);
					if (obj == DBNull.Value)
					{
						value = obj;
						return true;
					}
					value = this.PropertyTypeConverter.Convert(obj, dbParameterType, parameterInfo.Format, dbParameterCulture);
					return true;
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
					InternalLogger.Warn(ex, "  DatabaseTarget: Failed to convert raw value for '{0}' into {1}", new object[]
					{
						parameterInfo.Name,
						(dbParameterType != null) ? dbParameterType.Name : null
					});
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			value = null;
			return false;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000C738 File Offset: 0x0000A938
		private static object CreateDefaultValue(Type dbParameterType)
		{
			if (dbParameterType == typeof(string))
			{
				return string.Empty;
			}
			if (dbParameterType.IsValueType())
			{
				return Activator.CreateInstance(dbParameterType);
			}
			return DBNull.Value;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000C768 File Offset: 0x0000A968
		private IFormatProvider GetDbParameterCulture(LogEventInfo logEvent, DatabaseParameterInfo parameterInfo)
		{
			IFormatProvider culture = parameterInfo.Culture;
			IFormatProvider formatProvider;
			if ((formatProvider = culture) == null && (formatProvider = logEvent.FormatProvider) == null)
			{
				LoggingConfiguration loggingConfiguration = base.LoggingConfiguration;
				if (loggingConfiguration == null)
				{
					return null;
				}
				formatProvider = loggingConfiguration.DefaultCultureInfo;
			}
			return formatProvider;
		}

		// Token: 0x040000A4 RID: 164
		private IDbConnection _activeConnection;

		// Token: 0x040000A5 RID: 165
		private string _activeConnectionString;

		// Token: 0x040000B8 RID: 184
		private IPropertyTypeConverter _propertyTypeConverter;

		// Token: 0x040000B9 RID: 185
		private SortHelpers.KeySelector<AsyncLogEventInfo, string> _buildConnectionStringDelegate;
	}
}
