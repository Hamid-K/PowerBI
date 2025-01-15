using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Data.Serialization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Lineage;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Sql
{
	// Token: 0x020003B1 RID: 945
	internal class SqlEnvironment : DbEnvironment
	{
		// Token: 0x0600211B RID: 8475 RVA: 0x000593F0 File Offset: 0x000575F0
		public SqlEnvironment(IEngineHost host, IResource resource, string server, string database, Value options, IDataSourceLocation location, DbTransactionInfo transactionInfo = null)
			: base(host, resource, "Microsoft SQL", server, database, options, location, transactionInfo)
		{
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x00059414 File Offset: 0x00057614
		public static SqlEnvironment Create(IEngineHost host, string server, string database, Value options, IDataSourceLocation location)
		{
			IResource resource = DatabaseResource.New("SQL", server, database);
			IExtensibilityService extensibilityService = host.QueryService<IExtensibilityService>();
			if (extensibilityService != null)
			{
				Value value;
				if (options.IsRecord && options.AsRecord.TryGetValue("LegacyExtension", out value) && value.AsBoolean)
				{
					resource = extensibilityService.CurrentResource;
				}
				location = null;
			}
			return new SqlEnvironment(host, resource, server, database, options, location, null);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00059473 File Offset: 0x00057673
		public static DbEnvironment CreateForFolding(IEngineHost engineHost, IResource resource, string server, string database, string serverVersion, int? engineEdition)
		{
			return new SqlEnvironment(engineHost, resource, server, database, Value.Null, null, null)
			{
				ServerMetadata = new SqlEnvironment.SqlServerMetadata
				{
					Version = server,
					EngineEdition = engineEdition,
					IsSaas = false
				}
			};
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x000594A6 File Offset: 0x000576A6
		private SqlEnvironment.SqlBatchSchemaLoader BatchSchemaLoader
		{
			get
			{
				if (this.batchSchemaLoader == null)
				{
					this.batchSchemaLoader = new SqlEnvironment.SqlBatchSchemaLoader(this);
				}
				return this.batchSchemaLoader;
			}
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000594C4 File Offset: 0x000576C4
		public override bool OtherCanFoldToThis(EnvironmentBase other)
		{
			if (base.OtherCanFoldToThis(other))
			{
				return true;
			}
			SqlEnvironment sqlEnvironment = other as SqlEnvironment;
			return sqlEnvironment != null && !(base.Server != sqlEnvironment.Server) && base.UserOptions.GetBool("EnableCrossDatabaseFolding", false) && sqlEnvironment.UserOptions.GetBool("EnableCrossDatabaseFolding", false);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00059526 File Offset: 0x00057726
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			SqlAstExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00059530 File Offset: 0x00057730
		protected override void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			SqlAstExpressionChecker.CheckStatement(expression, cursor, this);
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x0005953A File Offset: 0x0005773A
		protected override string ProviderName
		{
			get
			{
				return SqlEnvironment.SqlClient.ProviderName;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x00059546 File Offset: 0x00057746
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return SqlModule.OptionRecord;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x00002139 File Offset: 0x00000339
		public override bool SupportsNativeQueryFolding
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x00002139 File Offset: 0x00000339
		public override bool SupportsNativeQueryTypePreservation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x0005954D File Offset: 0x0005774D
		public override bool PrefetchMetadata
		{
			get
			{
				return !this.IsTrident && base.PrefetchMetadata;
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x0005955F File Offset: 0x0005775F
		public override int InsertBatchSize
		{
			get
			{
				return 1000;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x00059566 File Offset: 0x00057766
		public override int BulkInsertMinimumSize
		{
			get
			{
				if (!base.UserOptions.GetBool("EnableBulkInsert", true))
				{
					return -1;
				}
				return this.InsertBatchSize;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x00059584 File Offset: 0x00057784
		public byte[] ContextInfo
		{
			get
			{
				byte[] array = null;
				object obj;
				if (base.UserOptions.TryGetValue("ContextInfo", out obj))
				{
					array = (byte[])obj;
				}
				if (array != null && array.Length > 128)
				{
					throw ValueException.NewDataSourceError<Message2>(DataSourceException.DataSourceMessage("Microsoft SQL", Microsoft.Mashup.Engine1.Strings.SqlContextInfoValueTooLong(PiiFree.New(128))), BinaryValue.New(array), null);
				}
				return array;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x000595E8 File Offset: 0x000577E8
		public bool MultiSubnetFailover
		{
			get
			{
				if (!base.UserOptions.GetBool("MultiSubnetFailover", false))
				{
					return false;
				}
				if (!SqlEnvironment.SqlClient.SupportsMultiSubnetFailover)
				{
					throw ValueException.NewDataSourceError<Message2>(DataSourceException.DataSourceMessage("Microsoft SQL", Microsoft.Mashup.Engine1.Strings.MultiSubnetFailover_DotNetFourFive), Value.Null, null);
				}
				return true;
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00059637 File Offset: 0x00057837
		protected override DbProviderFactory CreateDbProviderFactory()
		{
			return SqlEnvironment.SqlClient.ProviderFactory;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00059643 File Offset: 0x00057843
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return SqlAstCreator.Create(expression, cursor, this);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00059650 File Offset: 0x00057850
		protected override RetryBehavior RetryAfterSqlError(DbException error)
		{
			if (base.TransactionInfo != null)
			{
				try
				{
					base.TransactionInfo.Rollback();
				}
				catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					using (IHostTrace hostTrace = base.Tracer.CreateTrace("RetryAfterSqlError", TraceEventType.Information))
					{
						base.TraceException(hostTrace, ex);
					}
				}
				return new RetryBehavior(false, DbEnvironment.RetryDelay);
			}
			return SqlEnvironment.RetryAfterSqlErrorStatic(error, this.WasEncrypted);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000596E8 File Offset: 0x000578E8
		public static RetryBehavior RetryAfterSqlErrorStatic(DbException error, bool wasEncrypted = false)
		{
			TimeSpan timeSpan = DbEnvironment.RetryDelay;
			bool flag = false;
			int? num = null;
			SqlErrorInfo sqlErrorInfo;
			if (SqlEnvironment.sqlClient.TryGetErrorInfo(error, wasEncrypted, out sqlErrorInfo) && (SqlEnvironment.RetryableConnectionErrors.Contains(sqlErrorInfo.Number) || SqlEnvironment.RetryableSqlErrors.Contains(sqlErrorInfo.Number)))
			{
				num = new int?(sqlErrorInfo.Number);
				flag = true;
				TimeSpan timeSpan2;
				if (SqlEnvironment.RetryAfterBySQLErrorCodes.TryGetValue(sqlErrorInfo.Number, out timeSpan2) && timeSpan2 > timeSpan)
				{
					timeSpan = timeSpan2;
				}
			}
			return new RetryBehavior(flag, timeSpan, num);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x00059774 File Offset: 0x00057974
		private bool IsFatalSqlException(DbException e)
		{
			SqlErrorInfo sqlErrorInfo;
			return SqlEnvironment.SqlClient.TryGetErrorInfo(e, this.WasEncrypted, out sqlErrorInfo) && SqlEnvironment.FatalSqlErrors.Contains(sqlErrorInfo.Number);
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000597A8 File Offset: 0x000579A8
		protected override void FinalizeOnRetry()
		{
			SqlEnvironment.FinalizeOnRetryStatic();
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000597AF File Offset: 0x000579AF
		internal static bool IsMDateTimeCompatibleType(SqlDataType columnType)
		{
			return columnType == SqlDataType.Date || columnType == SqlDataType.DateTime || columnType == SqlDataType.DateTime2;
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000597CC File Offset: 0x000579CC
		public static void FinalizeOnRetryStatic()
		{
			SqlEnvironment.SqlClient.ClearAllPools();
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000597D8 File Offset: 0x000579D8
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException dbException)
		{
			SqlErrorInfo sqlErrorInfo;
			if (SqlEnvironment.sqlClient.TryGetErrorInfo(dbException, this.WasEncrypted, out sqlErrorInfo))
			{
				ResourceExceptionKind resourceExceptionKind = SqlEnvironment.ClassifySqlError(sqlErrorInfo);
				if (resourceExceptionKind != ResourceExceptionKind.None)
				{
					return resourceExceptionKind;
				}
			}
			return ResourceExceptionKind.None;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x00059808 File Offset: 0x00057A08
		public override IList<RecordKeyDefinition> GetDbExceptionDetails(DbException dbException)
		{
			IList<RecordKeyDefinition> details = DbExceptionInfo.GetDetails(dbException);
			SqlErrorInfo sqlErrorInfo;
			if (SqlEnvironment.SqlClient.TryGetErrorInfo(dbException, this.WasEncrypted, out sqlErrorInfo))
			{
				details.Add(new RecordKeyDefinition("Number", NumberValue.New(sqlErrorInfo.Number), TypeValue.Number));
				details.Add(new RecordKeyDefinition("Class", NumberValue.New((int)sqlErrorInfo.Class), TypeValue.Number));
				details.Add(new RecordKeyDefinition("State", NumberValue.New((int)sqlErrorInfo.State), TypeValue.Number));
			}
			return details;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00059894 File Offset: 0x00057A94
		public override TableValue GetDirectQueryCapabilities()
		{
			if (this.capabilities == null)
			{
				List<Value> list = new List<Value>();
				list.Add(CapabilityModule.NewCapability("Core", Value.Null));
				list.Add(CapabilityModule.NewCapability("LiteralCount", NumberValue.New(2100)));
				list.AddRange(DbEnvironment.Capabilities.TableFunctions.Select((string tableFunction) => CapabilityModule.NewCapability(tableFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.DateFunctions.Select((string dateFunction) => CapabilityModule.NewCapability(dateFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.NumericFunctions.Select((string numericFunction) => CapabilityModule.NewCapability(numericFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.StringFunctions.Select((string stringFunction) => CapabilityModule.NewCapability(stringFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.ListFunctions.Select((string listFunction) => CapabilityModule.NewCapability(listFunction, Value.Null)).Cast<Value>());
				TableTypeValue asTableType = CapabilityModule.DirectQueryCapabilities.From.Type.AsFunctionType.ReturnType.AsTableType;
				this.capabilities = ListValue.New(list.ToArray()).ToTable(asTableType);
			}
			return this.capabilities;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00059A1C File Offset: 0x00057C1C
		protected override ConnectionInfo CreateConnectionInfo()
		{
			ConnectionInfo connectionInfo = base.CreateConnectionInfo();
			if (base.TransactionInfo != null)
			{
				string text = base.TransactionInfo.Identity;
				if (connectionInfo.AlternateIdentity != null)
				{
					text = connectionInfo.AlternateIdentity + "##" + text;
				}
				connectionInfo = new ConnectionInfo(connectionInfo.ConnectionString, text, connectionInfo.Impersonate, connectionInfo.RequireEncryption);
			}
			return connectionInfo;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00059A80 File Offset: 0x00057C80
		public override DbConnection CreateDbConnection()
		{
			DbConnection dbConnection = base.CreateDbConnection();
			if (this.aadCredential != null)
			{
				this.aadCredential = this.aadCredential.RefreshTokenAsNeeded(base.Host, this.Resource, false);
				string text = SqlEnvironment.accessTokenCache.Intern(this.aadCredential.AccessTokenForResource(null));
				SqlEnvironment.SqlClient.SetAccessToken(dbConnection, text);
				dbConnection = new SqlEnvironment.RetryableConnection(this, dbConnection, base.Credentials.HasRefreshableCredential());
			}
			if (this.ContextInfo != null)
			{
				dbConnection = new SqlEnvironment.SetContextInfoConnection(dbConnection, this.ContextInfo);
			}
			if (base.Host.GetConfigurationProperty("MashupFlight_EnableSqlSoftCancel", false))
			{
				dbConnection = CancelingDbConnection.New(this, dbConnection);
			}
			if (base.Host.GetConfigurationProperty("MashupFlight_DisableSqlConnectionPooling", false))
			{
				using (IHostTrace hostTrace = base.Tracer.CreateTrace("CreateDbConnection/DisableConnectionPooling", TraceEventType.Information))
				{
					hostTrace.Add("disable", true, false);
					SqlEnvironment.ConnectionPoolScope.Register(base.Host.QueryService<ILifetimeService>(), dbConnection);
				}
			}
			return dbConnection;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00059B88 File Offset: 0x00057D88
		private bool IsInvalidCredentials(DbException dbException)
		{
			return this.GetResourceExceptionKind(dbException) == ResourceExceptionKind.InvalidCredentials;
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00059B94 File Offset: 0x00057D94
		private bool IsRetryableConnectionFailure(DbException dbException)
		{
			SqlErrorInfo sqlErrorInfo;
			return SqlEnvironment.sqlClient.TryGetErrorInfo(dbException, this.WasEncrypted, out sqlErrorInfo) && SqlEnvironment.RetryableConnectionErrors.Contains(sqlErrorInfo.Number);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x00059BC8 File Offset: 0x00057DC8
		public override Exception ProcessDbException(DbException exception)
		{
			if (this.IsCapacitySqlErrors(exception))
			{
				return DataSourceException.NewCapacityExceededException<Message0>(base.Host, this.Resource, new Message0(exception.Message), exception);
			}
			return base.ProcessDbException(exception);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00059BF8 File Offset: 0x00057DF8
		private bool IsCapacitySqlErrors(DbException dbException)
		{
			SqlErrorInfo sqlErrorInfo;
			return SqlEnvironment.sqlClient.TryGetErrorInfo(dbException, this.WasEncrypted, out sqlErrorInfo) && SqlEnvironment.CapacitySqlErrors.Contains(sqlErrorInfo.Number);
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00059C30 File Offset: 0x00057E30
		protected override TracingDbConnection TraceConnection(DbConnection connection)
		{
			if (base.IsServerMetadataSet && this.IsDataverse)
			{
				IEvaluationConstants evaluationConstants = base.Host.QueryService<IEvaluationConstants>();
				string text = ((evaluationConstants != null) ? evaluationConstants.CorrelationId : null);
				if (!string.IsNullOrEmpty(text))
				{
					connection = new SqlEnvironment.CorrelatingSqlConnection(connection, text);
				}
			}
			SqlEnvironment.TracingSqlDbConnection tracingSqlDbConnection = new SqlEnvironment.TracingSqlDbConnection(base.Host, base.Tracer, connection, new Action<IHostTrace>(this.AdditionalCommandTraces), base.ConnectionInfo.RequireEncryption);
			string connectionId = tracingSqlDbConnection.ConnectionId;
			return tracingSqlDbConnection;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x00059CAA File Offset: 0x00057EAA
		protected override DbEnvironment.DbServerMetadata LoadServerMetadata()
		{
			return this.ConvertDbExceptions<SqlEnvironment.SqlServerMetadata>(delegate
			{
				SqlEnvironment.SqlServerMetadata sqlServerMetadata;
				using (new ProgressRequest(base.HostProgressService))
				{
					using (DbConnection dbConnection = base.CreateConnection())
					{
						dbConnection.Open(this);
						using (DbCommand dbCommand = dbConnection.CreateCommand())
						{
							dbCommand.CommandText = "SELECT\r\n@@version _VERSION,\r\nCAST(SERVERPROPERTY('EngineEdition') as VARCHAR(4)) _EDITION,\r\nCASE WHEN EXISTS (SELECT * FROM sys.extended_properties WHERE [name] = N'isSaaSMetadata' AND [value] = '1') THEN 1 ELSE 0 END _IS_SAAS,\r\nCASE WHEN EXISTS (SELECT * FROM sys.types WHERE name = 'char' AND collation_name LIKE '%UTF8%') THEN 1 ELSE 0 END _UTF8_COLLATION";
							using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
							{
								dbDataReader.Read();
								int? num = null;
								int num2;
								if (!dbDataReader.IsDBNull(1) && int.TryParse(dbDataReader.GetString(1).Trim(), out num2))
								{
									num = new int?(num2);
								}
								int? num3 = num;
								int num4 = 11;
								if (((num3.GetValueOrDefault() == num4) & (num3 != null)) && dbDataReader.GetInt32(3) == 1)
								{
									num = new int?(-1);
								}
								sqlServerMetadata = new SqlEnvironment.SqlServerMetadata
								{
									Version = dbDataReader.GetString(0).Trim(),
									EngineEdition = num,
									IsSaas = (dbDataReader.GetInt32(2) == 1)
								};
							}
						}
					}
				}
				return sqlServerMetadata;
			});
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00059CBE File Offset: 0x00057EBE
		protected override DbEnvironment.DbServerMetadata LoadServerMetadataFromStream(Stream s)
		{
			SqlEnvironment.SqlServerMetadata sqlServerMetadata = new SqlEnvironment.SqlServerMetadata();
			sqlServerMetadata.Deserialize(s);
			return sqlServerMetadata;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00059CCC File Offset: 0x00057ECC
		public override bool TraceException(IHostTrace trace, Exception exception, TraceEventType severity)
		{
			DbException ex = exception as DbException;
			if (ex != null)
			{
				SqlErrorInfo sqlErrorInfo;
				if (SqlEnvironment.sqlClient.TryGetErrorInfo(ex, this.WasEncrypted, out sqlErrorInfo))
				{
					trace.Add("Number", sqlErrorInfo.Number, false);
					trace.Add("Class", sqlErrorInfo.Class, false);
					trace.Add("State", sqlErrorInfo.State, false);
					trace.Add("ResourceExceptionKind", this.GetResourceExceptionKind(ex), false);
					trace.TracePossibleJwtExpirationWithAudience(this.aadCredential);
				}
				SqlEnvironment.sqlClient.TraceClientConnectionId(trace, ex);
				SqlEnvironment.sqlClient.TraceRequestIds(trace, ex);
			}
			return base.TraceException(trace, exception, severity);
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00059D88 File Offset: 0x00057F88
		protected override void AdditionalCommandTraces(IHostTrace trace)
		{
			if (base.IsServerMetadataSet)
			{
				SqlEnvironment.SqlServerMetadata sqlServerMetadata = base.ServerMetadata as SqlEnvironment.SqlServerMetadata;
				int? num = ((sqlServerMetadata != null) ? sqlServerMetadata.EngineEdition : null);
				if (num != null)
				{
					trace.Add("EngineEdition", num, false);
				}
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x00059DD8 File Offset: 0x00057FD8
		private bool WasEncrypted
		{
			get
			{
				return base.Credentials.Any((IResourceCredential c) => c is EncryptedConnectionAdornment);
			}
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x00059E04 File Offset: 0x00058004
		public static SqlSettings CreateSettings(string serverVersion, int? engineEdition)
		{
			if (engineEdition != null)
			{
				int valueOrDefault = engineEdition.GetValueOrDefault();
				if (valueOrDefault == -1)
				{
					return SqlEnvironment.tridentSettings;
				}
				if (valueOrDefault == 1000)
				{
					return SqlEnvironment.dataverseSettings;
				}
			}
			if (serverVersion == null)
			{
				return SqlEnvironment.sql2005Settings;
			}
			if (serverVersion.Contains("Parallel Data Warehouse"))
			{
				return SqlEnvironment.pdwSettings;
			}
			if (serverVersion.Contains("SQL Azure"))
			{
				return SqlEnvironment.azureSettings;
			}
			Match match = SqlEnvironment.versionMatcher.Match(serverVersion);
			int num;
			if (!match.Success || !int.TryParse(match.Groups[1].Value, out num))
			{
				num = 0;
			}
			switch (num)
			{
			case 10:
				return SqlEnvironment.sql2008Settings;
			case 11:
			case 12:
				return SqlEnvironment.sql2012Settings;
			case 13:
				return SqlEnvironment.sql2016Settings;
			case 14:
				return SqlEnvironment.sql2017Settings;
			case 15:
				return SqlEnvironment.sql2019Settings;
			default:
				if (num >= 10)
				{
					return SqlEnvironment.sql2019Settings;
				}
				return SqlEnvironment.sql2005Settings;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x00059EEC File Offset: 0x000580EC
		private int? EngineEdition
		{
			get
			{
				SqlEnvironment.SqlServerMetadata sqlServerMetadata = base.ServerMetadata as SqlEnvironment.SqlServerMetadata;
				if (sqlServerMetadata == null)
				{
					return null;
				}
				return sqlServerMetadata.EngineEdition;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x00059F17 File Offset: 0x00058117
		private bool IsSaas
		{
			get
			{
				SqlEnvironment.SqlServerMetadata sqlServerMetadata = base.ServerMetadata as SqlEnvironment.SqlServerMetadata;
				return sqlServerMetadata != null && sqlServerMetadata.IsSaas;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x00059F30 File Offset: 0x00058130
		public bool IsSynapseSqlOnDemand
		{
			get
			{
				int? engineEdition = this.EngineEdition;
				int num = 11;
				return (engineEdition.GetValueOrDefault() == num) & (engineEdition != null);
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x00059F5C File Offset: 0x0005815C
		public bool IsTrident
		{
			get
			{
				int? engineEdition = this.EngineEdition;
				int num = -1;
				return (engineEdition.GetValueOrDefault() == num) & (engineEdition != null);
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x00059F84 File Offset: 0x00058184
		public bool IsDataverse
		{
			get
			{
				int? engineEdition = this.EngineEdition;
				int num = 1000;
				return (engineEdition.GetValueOrDefault() == num) & (engineEdition != null);
			}
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x00059FB0 File Offset: 0x000581B0
		protected override SqlSettings LoadSqlSettings()
		{
			return SqlEnvironment.CreateSettings(base.ServerVersion, this.EngineEdition);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00059FC4 File Offset: 0x000581C4
		private static ResourceExceptionKind ClassifySqlError(SqlErrorInfo error)
		{
			int number = error.Number;
			if (number <= 18304)
			{
				if (number <= -2146762481)
				{
					if (number == -2146893022)
					{
						return ResourceExceptionKind.ServerNameMismatch;
					}
					if (number != -2146893019 && number != -2146762481)
					{
						goto IL_00DD;
					}
				}
				else if (number <= 4060)
				{
					if (number != 20)
					{
						if (number != 4060)
						{
							goto IL_00DD;
						}
						goto IL_00D9;
					}
				}
				else
				{
					if (number != 18301 && number != 18304)
					{
						goto IL_00DD;
					}
					goto IL_00D9;
				}
				return ResourceExceptionKind.SecureConnectionFailed;
			}
			if (number <= 18487)
			{
				if (number <= 18452)
				{
					if (number != 18307 && number != 18452)
					{
						goto IL_00DD;
					}
				}
				else if (number != 18456 && number != 18487)
				{
					goto IL_00DD;
				}
			}
			else if (number <= 28034)
			{
				if (number != 21672 && number != 28034)
				{
					goto IL_00DD;
				}
			}
			else if (number - 28047 > 1 && number != 40607)
			{
				goto IL_00DD;
			}
			IL_00D9:
			return ResourceExceptionKind.InvalidCredentials;
			IL_00DD:
			return ResourceExceptionKind.None;
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x0005A0B1 File Offset: 0x000582B1
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return SqlEnvironment.searchableTypes;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x0005A0B8 File Offset: 0x000582B8
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				return SqlEnvironment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0005A0BF File Offset: 0x000582BF
		public override bool? IsVariableLengthType(string dataType)
		{
			return new bool?(SqlEnvironment.variableLengthTypes.Contains(dataType));
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x0005A0D4 File Offset: 0x000582D4
		protected override bool TryGetDataTypeValue(DataColumnCollection columns, DataRow schemaRow, out TypeValue clrDataType, out bool isSearchable)
		{
			bool flag = base.TryGetDataTypeValue(columns, schemaRow, out clrDataType, out isSearchable);
			if (flag && this.IncludeDataverseFieldCaptions)
			{
				string stringSchemaColumnOrNull = DbEnvironment.GetStringSchemaColumnOrNull(columns, schemaRow, "FIELD_CAPTION");
				if (stringSchemaColumnOrNull != null)
				{
					RecordValue recordValue = RecordValue.New(Keys.New("Documentation.FieldCaption"), new Value[] { TextValue.New(stringSchemaColumnOrNull) });
					clrDataType = BinaryOperator.AddMeta.Invoke(clrDataType, recordValue).AsType;
				}
			}
			return flag;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0005A13C File Offset: 0x0005833C
		public override DataTable LoadSchemas(DbConnection connection)
		{
			return base.LoadData("Schemas", connection, "select SCHEMA_NAME from [INFORMATION_SCHEMA].[SCHEMATA]");
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0005A150 File Offset: 0x00058350
		public override DataTable LoadDatabaseInformation(DbConnection connection)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "select cast(e.value as {0}) [DESCRIPTION]\r\nfrom {1} e\r\nwhere e.major_id = 0 and e.minor_id = 0 and e.class = 0 and e.name = 'MS_Description'", this.GetNVarCharMaxString(), this.ExtendedPropertiesTable);
			return base.LoadData("DatabaseInformation", connection, text);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0005A188 File Offset: 0x00058388
		protected override DataTable GetTables(SchemaItem? itemFilter)
		{
			DataTable tables = base.GetTables(itemFilter);
			if (itemFilter == null)
			{
				this.BatchSchemaLoader.SetTables(tables);
			}
			return tables;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0005A1B4 File Offset: 0x000583B4
		public override DataTable LoadTables(DbConnection connection, string schemaFilter, string tableFilter)
		{
			string text = ((!string.IsNullOrEmpty(schemaFilter)) ? ("t.[TABLE_SCHEMA] = " + base.SqlSettings.QuoteNationalStringLiteral(schemaFilter)) : "1=1");
			string text2 = ((!string.IsNullOrEmpty(tableFilter)) ? ("t.[TABLE_NAME] = " + base.SqlSettings.QuoteNationalStringLiteral(tableFilter)) : "1=1");
			string text3 = string.Format(CultureInfo.InvariantCulture, "select t.[TABLE_CATALOG], t.[TABLE_SCHEMA], t.[TABLE_NAME], t.[TABLE_TYPE], tv.create_date [CREATED_DATE], tv.modify_date [MODIFIED_DATE], cast(e.value as {0}) [DESCRIPTION]\r\nfrom [INFORMATION_SCHEMA].[TABLES] t\r\njoin sys.schemas s on s.name = t.[TABLE_SCHEMA]\r\njoin sys.objects tv on tv.name = t.[TABLE_NAME] and tv.schema_id = s.schema_id and tv.parent_object_id = 0\r\nleft outer join {1} e on tv.object_id = e.major_id and e.minor_id = 0 and e.class = 1 and e.name = 'MS_Description'\r\nwhere {2} and {3}", new object[]
			{
				this.GetNVarCharMaxString(),
				this.ExtendedPropertiesTable,
				text,
				text2
			});
			return base.LoadData("Tables", connection, text3);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0005A254 File Offset: 0x00058454
		public override DataTable LoadProcedures(DbConnection connection, string schemaFilter, string procedureFilter)
		{
			string text = ((!string.IsNullOrEmpty(schemaFilter)) ? ("r.[ROUTINE_SCHEMA] = " + base.SqlSettings.QuoteNationalStringLiteral(schemaFilter)) : "1=1");
			string text2 = ((!string.IsNullOrEmpty(procedureFilter)) ? ("r.[ROUTINE_NAME] = " + base.SqlSettings.QuoteNationalStringLiteral(procedureFilter)) : "1=1");
			string text3 = string.Format(CultureInfo.InvariantCulture, "select r.[ROUTINE_SCHEMA], r.[ROUTINE_NAME], r.[ROUTINE_TYPE], p.create_date [CREATED_DATE], p.modify_date [MODIFIED_DATE], cast(e.value as {0}) [DESCRIPTION]\r\nfrom [INFORMATION_SCHEMA].[ROUTINES] r\r\njoin sys.schemas s on s.name = r.[ROUTINE_SCHEMA]\r\njoin sys.objects p on p.name = r.[ROUTINE_NAME] and p.schema_id = s.schema_id and p.parent_object_id = 0\r\nleft outer join {1} e on p.object_id = e.major_id and e.minor_id = 0 and e.class = 1 and e.name = 'MS_Description'\r\nwhere {2} and {3}", new object[]
			{
				this.GetNVarCharMaxString(),
				this.ExtendedPropertiesTable,
				text,
				text2
			});
			return base.LoadData("Procedures", connection, text3);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0005A2F4 File Offset: 0x000584F4
		protected override DataTable GetColumns(string schema, string table)
		{
			return base.GetSchemaTable((DbConnection connection) => this.DbService.LoadColumns(connection, schema, table), false, "Columns", new string[] { schema, table });
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0005A34C File Offset: 0x0005854C
		public override DataTable LoadColumns(DbConnection connection, string schema, string table)
		{
			return this.BatchSchemaLoader.LoadColumns(connection, new SchemaItem(schema, table, string.Empty));
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0005A368 File Offset: 0x00058568
		protected override DataTable GetIndexes(string schema, string table)
		{
			return base.GetSchemaTable((DbConnection connection) => this.DbService.LoadIndexes(connection, schema, table), false, "Indexes", new string[] { schema, table });
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0005A3C0 File Offset: 0x000585C0
		public override DataTable LoadIndexes(DbConnection connection, string schema, string table)
		{
			return this.BatchSchemaLoader.LoadIndexes(connection, new SchemaItem(schema, table, string.Empty));
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0005A3DC File Offset: 0x000585DC
		protected override DataTable GetForeignKeysParent(string schema, string table)
		{
			return base.GetSchemaTable((DbConnection connection) => this.DbService.LoadForeignKeysParent(connection, schema, table), false, "ForeignKeysParent", new string[] { schema, table });
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0005A434 File Offset: 0x00058634
		public override DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			return this.BatchSchemaLoader.LoadForeignKeysParent(connection, new SchemaItem(schema, table, string.Empty));
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0005A450 File Offset: 0x00058650
		protected override DataTable GetForeignKeysChild(string schema, string table)
		{
			return base.GetSchemaTable((DbConnection connection) => this.DbService.LoadForeignKeysChild(connection, schema, table), false, "ForeignKeysChild", new string[] { schema, table });
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0005A4A8 File Offset: 0x000586A8
		public override DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			return this.BatchSchemaLoader.LoadForeignKeysChild(connection, new SchemaItem(schema, table, string.Empty));
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0005A4C4 File Offset: 0x000586C4
		public override DataTable LoadProcedureColumns(DbConnection connection, string schema, string procedure)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "select\r\n    c.name [COLUMN_NAME],\r\n    cast(c.column_id as bigint) [ORDINAL_POSITION],\r\n    c.is_nullable [IS_NULLABLE],\r\n    {0}\r\nfrom sys.objects o join sys.schemas s on s.schema_id = o.schema_id\r\njoin sys.columns c on o.object_id = c.object_id\r\njoin sys.types t on c.user_type_id = t.user_type_id\r\nleft join sys.types t_system on t.system_type_id = t_system.user_type_id\r\nleft join {1} e on o.object_id = e.major_id and c.column_id = e.minor_id and e.class = 1 and e.name = 'MS_Description'\r\nwhere s.name = {{0}} and o.name = {{1}}", this.GetColumnInfoPartialQuery("t", "t_system", "c", "e"), this.ExtendedPropertiesTable);
			return base.LoadData("ProcedureColumns", connection, text, new string[] { schema, procedure });
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0005A51C File Offset: 0x0005871C
		public override DataTable LoadProcedureParameters(DbConnection connection, string schema, string procedure)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "select\r\n    p.name [PARAMETER_NAME],\r\n    cast(p.parameter_id as bigint) [ORDINAL_POSITION],\r\n    {0}\r\nfrom sys.objects o join sys.schemas s on s.schema_id = o.schema_id\r\njoin sys.parameters p on o.object_id = p.object_id\r\njoin sys.types t on p.user_type_id = t.user_type_id\r\nleft join sys.types t_system on t.system_type_id = t_system.user_type_id\r\nleft join {1} e on o.object_id = e.major_id and p.parameter_id = e.minor_id and e.class = 2 and e.name = 'MS_Description'\r\nwhere s.name = {{0}} and o.name = {{1}}", this.GetColumnInfoPartialQuery("t", "t_system", "p", "e"), this.ExtendedPropertiesTable);
			return base.LoadData("ProcedureParameters", connection, text, new string[] { schema, procedure });
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0005A574 File Offset: 0x00058774
		public override DataTable LoadIdentityColumns(DbConnection connection, string schema, string table)
		{
			return base.LoadData("IdentityColumns", connection, "select c.name [COLUMN_NAME], cast(c.column_id as bigint) [ORDINAL_POSITION]\r\nfrom sys.objects as o\r\njoin sys.schemas s on s.schema_id = o.schema_id\r\njoin sys.columns as c on o.object_id = c.object_id\r\nwhere s.name = {0} and o.name = {1} and c.is_identity = 1;", new string[] { schema, table });
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0005A598 File Offset: 0x00058798
		public override DataTable LoadResourceInformation(DbConnection connection, string schema, string table)
		{
			object obj;
			DataTable dataTable;
			if (base.SqlSettings.AdditionalSettings.TryGetValue("PartitionStatsTable", out obj))
			{
				string text = (string)obj;
				string text2 = string.Format(CultureInfo.InvariantCulture, "select cast(sum(p.reserved_page_count) * 8 as nvarchar) + N' KB' reserved\r\nfrom {0} p\r\njoin sys.objects o on p.object_id = o.object_id\r\njoin sys.schemas s on o.schema_id = s.schema_id\r\nwhere s.name = {{0}} and o.name = {{1}}\r\ngroup by s.name, o.name", text);
				dataTable = base.LoadData("ResourceInformation", connection, text2, new string[] { schema, table });
			}
			else
			{
				dataTable = base.LoadData("ResourceInformation", connection, "sp_spaceused {0};", new string[] { new SchemaItem(base.SqlSettings.QuoteIdentifier(schema), base.SqlSettings.QuoteIdentifier(table), null).Identifier });
			}
			DataTable dataTable2 = new DataTable();
			dataTable2.Locale = CultureInfo.InvariantCulture;
			dataTable2.Columns.Add("TOTAL_BYTES", typeof(long));
			foreach (object obj2 in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				object byteCountOrDBNull = SqlEnvironment.GetByteCountOrDBNull(DbEnvironment.GetStringSchemaColumnOrNull(dataTable.Columns, dataRow, "reserved"));
				dataTable2.Rows.Add(new object[] { byteCountOrDBNull });
			}
			return dataTable2;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0005A6EC File Offset: 0x000588EC
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			return new SqlEnvironment.SqlConnectionStringBuilder(this);
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x0005A6F4 File Offset: 0x000588F4
		private string ExtendedPropertiesTable
		{
			get
			{
				if (base.SqlSettings.SupportsExtendedProperties)
				{
					return "sys.extended_properties";
				}
				return "(select null major_id, null minor_id, null class, null name, null value)";
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x0005A70E File Offset: 0x0005890E
		private bool IncludeDataverseFieldCaptions
		{
			get
			{
				return this.IsDataverse && base.UserOptions.GetBool("IncludeFieldCaptions", false);
			}
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0005A72C File Offset: 0x0005892C
		private string GetNVarCharMaxString()
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				ScriptWriter scriptWriter = new ScriptWriter(stringWriter, base.SqlSettings);
				(this.IsTrident ? SqlEnvironment.tridentVarcharType : SqlDataType.NVarChar).WriteCreateScript(scriptWriter);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0005A790 File Offset: 0x00058990
		private static object GetByteCountOrDBNull(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				s = s.ToUpperInvariant();
				if (s.EndsWith(" KB", StringComparison.Ordinal))
				{
					s = s.Substring(0, s.Length - " KB".Length);
					long num;
					if (long.TryParse(s, out num))
					{
						return num * 1024L;
					}
				}
			}
			return DBNull.Value;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0005A7F4 File Offset: 0x000589F4
		public override SqlDataType GetSqlScalarType(TypeValue type)
		{
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				if (this.IsTrident)
				{
					return SqlEnvironment.tridentTimeType;
				}
				break;
			case ValueKind.Date:
				if (base.SqlSettings.GetSetting<bool>("SupportsDateTime2", true))
				{
					return SqlDataType.Date;
				}
				break;
			case ValueKind.DateTime:
				if (this.IsTrident)
				{
					return SqlEnvironment.tridentDateTimeType;
				}
				break;
			case ValueKind.Duration:
				return null;
			case ValueKind.Number:
				if (type.Equals(TypeValue.Byte))
				{
					return SqlDataType.TinyInt;
				}
				if (type.Equals(TypeValue.Int8))
				{
					return SqlDataType.SmallInt;
				}
				if (type.Equals(TypeValue.Int16))
				{
					return SqlDataType.SmallInt;
				}
				if (type.Equals(TypeValue.Int32))
				{
					return SqlDataType.Int;
				}
				if (type.Equals(TypeValue.Int64))
				{
					return SqlDataType.BigInt;
				}
				if (type.Equals(TypeValue.Single))
				{
					return SqlDataType.Real;
				}
				if (type.Equals(TypeValue.Decimal))
				{
					if (type.Facets.NumericPrecision == null || type.Facets.NumericScale == null)
					{
						return DbEnvironment.DecimalType;
					}
					return new SqlDataType(type, SqlLanguageStrings.DecimalSqlString);
				}
				else
				{
					if (!type.Equals(TypeValue.Currency))
					{
						return SqlDataType.Float;
					}
					if (!this.IsTrident)
					{
						return SqlDataType.Money;
					}
					return SqlEnvironment.tridentCurrencyType;
				}
				break;
			case ValueKind.Text:
				if (this.IsTrident)
				{
					bool? flag = type.Facets.IsVariableLength;
					bool flag2 = false;
					return new SqlDataType(type, ((flag.GetValueOrDefault() == flag2) & (flag != null)) ? SqlLanguageStrings.CharSqlString : SqlLanguageStrings.VarCharSqlString);
				}
				if (type.Facets.MaxLength != null)
				{
					bool? flag = type.Facets.IsVariableLength;
					bool flag2 = false;
					return new SqlDataType(type, ((flag.GetValueOrDefault() == flag2) & (flag != null)) ? SqlLanguageStrings.NCharSqlString : SqlLanguageStrings.NVarCharSqlString);
				}
				break;
			case ValueKind.Binary:
				if (type.Facets.MaxLength != null)
				{
					bool? flag = type.Facets.IsVariableLength;
					bool flag2 = false;
					return new SqlDataType(type, ((flag.GetValueOrDefault() == flag2) & (flag != null)) ? SqlLanguageStrings.BinarySqlString : SqlLanguageStrings.VarBinarySqlString);
				}
				break;
			}
			return base.GetSqlScalarType(type);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0005AA38 File Offset: 0x00058C38
		public override bool TryGetBulkCopy(string schema, string table, out IBulkCopy bulkCopy)
		{
			if (this.IsTrident)
			{
				bulkCopy = null;
				return false;
			}
			bulkCopy = new SqlEnvironment.SqlBulkCopyWrapper(this, schema, table);
			return true;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0005AA52 File Offset: 0x00058C52
		public override bool TryCreateTransactedEnvironment(DbTransactionInfo transaction, out DbEnvironment environment)
		{
			environment = new SqlEnvironment(base.Host, this.Resource, base.Server, base.Database, base.OptionsRecord, base.Location, transaction);
			return true;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0005AA84 File Offset: 0x00058C84
		protected override bool TryGetExpression(out IExpression expression)
		{
			if (base.TransactionInfo == null && base.Database != null)
			{
				expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(new SqlModule.DatabaseFunctionValue(base.Host)), new ConstantExpressionSyntaxNode(TextValue.New(base.Server)), new ConstantExpressionSyntaxNode(TextValue.New(base.Database)));
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0005AADE File Offset: 0x00058CDE
		public override bool SupportsSkip(TableTypeValue type)
		{
			return !this.IsDataverse && base.SupportsSkip(type);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0005AAF1 File Offset: 0x00058CF1
		public override bool SupportsPaging(TableTypeValue type)
		{
			return !this.IsDataverse && base.SupportsPaging(type);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0005AB04 File Offset: 0x00058D04
		public override TableValue CreateCatalogTableValue(IEngineHost host, string schema)
		{
			TableValue tableValue = base.CreateCatalogTableValue(host, schema);
			return base.CreateUpdatableCatalogTableValue(tableValue, schema);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0005AB24 File Offset: 0x00058D24
		protected override TableValue CreateHierarchicalNavigationTableValue()
		{
			TableValue tableValue = base.CreateHierarchicalNavigationTableValue();
			return base.CreateUpdatableHierarchicalNavigationTableValue(tableValue);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0005AB3F File Offset: 0x00058D3F
		public override bool TryGetSchemaItemFromName(string name, out SchemaItem item)
		{
			if (!name.Contains('.'))
			{
				item = new SchemaItem("dbo", name, null);
				return true;
			}
			item = default(SchemaItem);
			return false;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0005AB68 File Offset: 0x00058D68
		protected override bool TryLookupDatabaseItem(SchemaItem item, out SchemaItem adjustedItem, out Value data)
		{
			adjustedItem = item;
			if (base.IsItemCatalogLoaded || !this.IsTrident)
			{
				data = null;
				return false;
			}
			DataTable schemaTable = base.GetSchemaTable((DbConnection connection) => this.LoadDatabaseItem(connection, item), true, "Item", new string[] { item.Schema, item.Item });
			if (schemaTable.Rows.Count == 1)
			{
				DataRow dataRow = schemaTable.Rows[0];
				bool? booleanSchemaColumnOrNull = DbEnvironment.GetBooleanSchemaColumnOrNull(schemaTable.Columns, dataRow, "HAS_CHILD_RELATION");
				bool? booleanSchemaColumnOrNull2 = DbEnvironment.GetBooleanSchemaColumnOrNull(schemaTable.Columns, dataRow, "HAS_PARENT_RELATION");
				if (this.CreateRelationships)
				{
					bool? flag = booleanSchemaColumnOrNull;
					bool flag2 = false;
					if ((flag.GetValueOrDefault() == flag2) & (flag != null))
					{
						flag = booleanSchemaColumnOrNull2;
						flag2 = false;
						if ((flag.GetValueOrDefault() == flag2) & (flag != null))
						{
							goto IL_00F7;
						}
					}
					data = null;
					return false;
				}
				IL_00F7:
				string schemaColumn = DbEnvironment.GetSchemaColumn<string>(dataRow, "OBJECT_TYPE");
				DateTime? dateTimeSchemaColumnOrNull = DbEnvironment.GetDateTimeSchemaColumnOrNull(schemaTable.Columns, dataRow, "CREATED_DATE");
				DateTime? dateTimeSchemaColumnOrNull2 = DbEnvironment.GetDateTimeSchemaColumnOrNull(schemaTable.Columns, dataRow, "MODIFIED_DATE");
				string stringSchemaColumnOrNull = DbEnvironment.GetStringSchemaColumnOrNull(schemaTable.Columns, dataRow, "DESCRIPTION");
				bool flag3 = false;
				string text = schemaColumn.TrimEnd(Array.Empty<char>());
				string text2;
				if (!(text == "U"))
				{
					if (!(text == "V"))
					{
						if (!(text == "IF") && !(text == "TF") && !(text == "FT"))
						{
							if (!(text == "P"))
							{
								text2 = null;
							}
							else
							{
								text2 = (base.Host.AreActionsAvailable() ? "ParameterizedAction" : null);
								flag3 = true;
							}
						}
						else
						{
							text2 = "Function";
							flag3 = true;
						}
					}
					else
					{
						text2 = "View";
					}
				}
				else
				{
					text2 = "Table";
				}
				if (text2 != null)
				{
					adjustedItem = new SchemaItem(item.Schema, item.Item, text2);
					TypeValue typeValue;
					if (flag3)
					{
						typeValue = base.CreateFunctionType(item.Schema, item.Item, dateTimeSchemaColumnOrNull, dateTimeSchemaColumnOrNull2, stringSchemaColumnOrNull);
					}
					else
					{
						typeValue = base.CreateTableType(item.Schema, item.Item, flag3, dateTimeSchemaColumnOrNull, dateTimeSchemaColumnOrNull2, stringSchemaColumnOrNull);
					}
					data = base.CreateItem(adjustedItem, typeValue);
					return true;
				}
			}
			data = Value.Null;
			return true;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0005ADE8 File Offset: 0x00058FE8
		private DataTable LoadDatabaseItem(DbConnection connection, SchemaItem item)
		{
			string text = "sys.foreign_keys";
			if (base.Host.GetConfigurationProperty("MashupFlight_EnableNewSqlDatabaseItemQuery", false))
			{
				text = "sys.foreign_key_columns";
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "select o.type [OBJECT_TYPE], o.create_date [CREATED_DATE], o.modify_date [MODIFIED_DATE], cast(e.value as {0}) [DESCRIPTION],\r\ncase when exists (select * from {4} fk where fk.parent_object_id = o.object_id) then 1 else 0 end [HAS_CHILD_RELATION],\r\ncase when exists (select * from {4} fk where fk.referenced_object_id = o.object_id) then 1 else 0 end [HAS_PARENT_RELATION]\r\nfrom sys.objects o\r\ninner join sys.schemas s on s.schema_id = o.schema_id\r\nleft outer join {1} e on o.object_id = e.major_id and e.minor_id = 0 and e.class = 1 and e.name = 'MS_Description'\r\nwhere s.name = {2} and o.name = {3}", new object[]
			{
				this.GetNVarCharMaxString(),
				this.ExtendedPropertiesTable,
				base.SqlSettings.QuoteNationalStringLiteral(item.Schema),
				base.SqlSettings.QuoteNationalStringLiteral(item.Item),
				text
			});
			return base.LoadData("DatabaseItem", connection, text2);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0005AE81 File Offset: 0x00059081
		public override DbDataReaderWithTableSchema WrapDbDataReader(DbDataReaderWithTableSchema reader)
		{
			return new FatalSqlExceptionDetectingDataReaderWithTableSchema(reader, new Func<DbException, bool>(this.IsFatalSqlException));
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0005AE95 File Offset: 0x00059095
		public override DbEnvironment.DbReaderWrapper CreateReaderWrapper(string cacheKey, bool forNativeQuery)
		{
			return new SqlEnvironment.SqlReaderWrapper(this, base.MetadataCache, cacheKey, forNativeQuery);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0005AEA5 File Offset: 0x000590A5
		private string GetColumnInfoPartialQuery(string typeTableAlias, string systemTypeTableAlias, string columnTableAlias, string extendedPropertiesTableAlias)
		{
			return string.Format(CultureInfo.InvariantCulture, "case when ({0}.is_user_defined = 0 and {0}.name is not null) then {0}.name when ({2}.system_type_id = 240 or {0}.name is null) then 'udt' else {1}.name end [DATA_TYPE],\r\n    case when ({2}.system_type_id in (59, 62)) then 2 when ({2}.system_type_id in (48, 52, 56, 60, 104, 106, 108, 122, 127)) then 10 else null end [NUMERIC_PRECISION_RADIX],\r\n    {2}.precision [NUMERIC_PRECISION],\r\n    case when ({2}.system_type_id in (59, 62)) then null else {2}.scale end [NUMERIC_SCALE],\r\n    case when ({2}.system_type_id in (40, 41, 42, 43, 58, 61)) then {2}.scale else null end [DATETIME_PRECISION],\r\n    case when ({2}.system_type_id in (231, 239)) then floor({2}.max_length / 2) when ({2}.system_type_id in (165, 167, 173, 175)) then {2}.max_length else null end [CHARACTER_MAXIMUM_LENGTH],\r\n    cast({3}.value as {4}) [DESCRIPTION]", new object[]
			{
				typeTableAlias,
				systemTypeTableAlias,
				columnTableAlias,
				extendedPropertiesTableAlias,
				this.GetNVarCharMaxString()
			});
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x0005AED8 File Offset: 0x000590D8
		public static ISqlClient SqlClient
		{
			get
			{
				if (SqlEnvironment.sqlClient == null)
				{
					DbProviderFactory dbProviderFactory;
					if (FxVersionDetector.InstalledFxBuild >= 394802 && DbEnvironment.privateProviderManager.Value.TryCreateFactory(EngineHost.Empty, "Microsoft.Data.Mashup.SqlClient", out dbProviderFactory, false))
					{
						try
						{
							string directoryName = Path.GetDirectoryName(dbProviderFactory.GetType().Assembly.Location);
							string text = ((IntPtr.Size == 8) ? "Microsoft.Data.SqlClient.SNI.x64.dll" : "Microsoft.Data.SqlClient.SNI.x86.dll");
							string text2 = Path.Combine(directoryName, text);
							SqlEnvironment.sqlClient = dbProviderFactory as ISqlClient;
							string text3 = Path.Combine(directoryName, "Microsoft.Identity.Client.dll");
							DbEnvironment.privateProviderManager.Value.LoadAssembly(EngineHost.Empty, text3);
							SqlEnvironment.sniLibrary = DynamicLinkLibrary.LoadLibrary(text2);
						}
						catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
						{
						}
					}
					if (SqlEnvironment.sqlClient == null)
					{
						SqlEnvironment.sqlClient = new SystemSqlClient();
					}
				}
				return SqlEnvironment.sqlClient;
			}
		}

		// Token: 0x04000C80 RID: 3200
		public const string FrameworkProviderName = "System.Data.SqlClient";

		// Token: 0x04000C81 RID: 3201
		public const string MicrosoftProviderName = "Microsoft.Data.Mashup.SqlClient";

		// Token: 0x04000C82 RID: 3202
		public const string DataSourceName = "Microsoft SQL";

		// Token: 0x04000C83 RID: 3203
		public const int EncryptionFailure = -2146893019;

		// Token: 0x04000C84 RID: 3204
		private static readonly Regex versionMatcher = new Regex("([0-9]+)\\.[0-9]+\\.[0-9]+", RegexOptions.CultureInvariant);

		// Token: 0x04000C85 RID: 3205
		private static readonly SqlEnvironment.AccessTokenCache accessTokenCache = new SqlEnvironment.AccessTokenCache();

		// Token: 0x04000C86 RID: 3206
		private const int SqlServer2008MajorVersion = 10;

		// Token: 0x04000C87 RID: 3207
		private const int SqlServer2012MajorVersion = 11;

		// Token: 0x04000C88 RID: 3208
		private const int SqlServer2014MajorVersion = 12;

		// Token: 0x04000C89 RID: 3209
		private const int SqlServer2016MajorVersion = 13;

		// Token: 0x04000C8A RID: 3210
		private const int SqlServer2017MajorVersion = 14;

		// Token: 0x04000C8B RID: 3211
		private const int SqlServer2019MajorVersion = 15;

		// Token: 0x04000C8C RID: 3212
		private const int SynapseSQLODEngineEditon = 11;

		// Token: 0x04000C8D RID: 3213
		private const int DataverseEngineEdition = 1000;

		// Token: 0x04000C8E RID: 3214
		private const int TridentEngineEdition = -1;

		// Token: 0x04000C8F RID: 3215
		private const int DefaultCommandTimeoutInSeconds = 600;

		// Token: 0x04000C90 RID: 3216
		private const int MaxContextInfoLength = 128;

		// Token: 0x04000C91 RID: 3217
		private const int DbNotAvailable = 40613;

		// Token: 0x04000C92 RID: 3218
		private const int ExcessiveMemoryUsage = 40553;

		// Token: 0x04000C93 RID: 3219
		private const int ExcessiveTempDbUsage = 40551;

		// Token: 0x04000C94 RID: 3220
		private const int ExcessiveTxLogUsage = 40552;

		// Token: 0x04000C95 RID: 3221
		private const int LongRunningTx = 40549;

		// Token: 0x04000C96 RID: 3222
		private const int ProcessingError = 40197;

		// Token: 0x04000C97 RID: 3223
		private const int QuotaReached = 40544;

		// Token: 0x04000C98 RID: 3224
		private const int ServiceBusy = 40501;

		// Token: 0x04000C99 RID: 3225
		private const int TooManyLocks = 40550;

		// Token: 0x04000C9A RID: 3226
		private const int SerializableDeadlock = 1205;

		// Token: 0x04000C9B RID: 3227
		private const int SnapshotDeadlock = 3960;

		// Token: 0x04000C9C RID: 3228
		private const int ResourceLimit1 = 10928;

		// Token: 0x04000C9D RID: 3229
		private const int ResourceLimit2 = 10929;

		// Token: 0x04000C9E RID: 3230
		private const int DependencyFailure = 41301;

		// Token: 0x04000C9F RID: 3231
		private const int DuplicateUpdate = 41302;

		// Token: 0x04000CA0 RID: 3232
		private const int RepeatableReadFail = 41305;

		// Token: 0x04000CA1 RID: 3233
		private const int SerializableFail = 41325;

		// Token: 0x04000CA2 RID: 3234
		private const int ExceededDependencies = 41839;

		// Token: 0x04000CA3 RID: 3235
		private const int NotEnoughResources = 49918;

		// Token: 0x04000CA4 RID: 3236
		private const int SubscriptionBusy1 = 49919;

		// Token: 0x04000CA5 RID: 3237
		private const int SubscriptionBusy2 = 49920;

		// Token: 0x04000CA6 RID: 3238
		private const int ErrorDuringLogin = 64;

		// Token: 0x04000CA7 RID: 3239
		private const int SemaphoreExpired = 121;

		// Token: 0x04000CA8 RID: 3240
		private const int PreLoginError = 233;

		// Token: 0x04000CA9 RID: 3241
		private const int ConnectionTimeoutExpired = 258;

		// Token: 0x04000CAA RID: 3242
		private const int TransportLevelRecv = 10053;

		// Token: 0x04000CAB RID: 3243
		private const int TransportLevelSend = 10054;

		// Token: 0x04000CAC RID: 3244
		private const int ConnectionTimeoutErr = 10060;

		// Token: 0x04000CAD RID: 3245
		private const int DatabaseUnavailable = 40613;

		// Token: 0x04000CAE RID: 3246
		private const int SynapseSqlPoolIsWarmingUp = 42109;

		// Token: 0x04000CAF RID: 3247
		private const int DataWareHouseWarmingUp = 9756;

		// Token: 0x04000CB0 RID: 3248
		private const int NotEnoughCapacity = 70001;

		// Token: 0x04000CB1 RID: 3249
		private const int ConnectionTimeoutPostLogin = -2;

		// Token: 0x04000CB2 RID: 3250
		private const int CapacityThrottlingError = 24801;

		// Token: 0x04000CB3 RID: 3251
		private const int CapacityPausedError = 24802;

		// Token: 0x04000CB4 RID: 3252
		private static readonly HashSet<int> FatalSqlErrors = new HashSet<int> { 121, 10053, 10054 };

		// Token: 0x04000CB5 RID: 3253
		private static readonly HashSet<int> RetryableConnectionErrors = new HashSet<int> { 64, 121, 233, 258, 10053, 10054, 10060, 40613, 42109, -2 };

		// Token: 0x04000CB6 RID: 3254
		private static readonly HashSet<int> RetryableSqlErrors = new HashSet<int>
		{
			40197, 40501, 40544, 40549, 40550, 40551, 40552, 40553, 1205, 3960,
			40613, 10928, 10929, 41301, 41302, 41305, 41325, 41839, 49918, 49919,
			49920, 9756, 70001
		};

		// Token: 0x04000CB7 RID: 3255
		private static readonly HashSet<int> CapacitySqlErrors = new HashSet<int> { 24801, 24802 };

		// Token: 0x04000CB8 RID: 3256
		private static readonly Dictionary<int, TimeSpan> RetryAfterBySQLErrorCodes = new Dictionary<int, TimeSpan>
		{
			{
				40613,
				TimeSpan.FromSeconds(0.5)
			},
			{
				40501,
				TimeSpan.FromSeconds(10.0)
			},
			{
				42109,
				TimeSpan.FromSeconds(10.0)
			},
			{
				9756,
				TimeSpan.FromSeconds(10.0)
			}
		};

		// Token: 0x04000CB9 RID: 3257
		private static readonly HashSet<string> TypesWithLength = new HashSet<string>
		{
			SqlLanguageStrings.CharSqlString.String,
			SqlLanguageStrings.NCharSqlString.String,
			SqlLanguageStrings.BinarySqlString.String,
			SqlLanguageStrings.VarCharSqlString.String,
			SqlLanguageStrings.NVarCharSqlString.String,
			SqlLanguageStrings.VarBinarySqlString.String
		};

		// Token: 0x04000CBA RID: 3258
		private static readonly SqlDataType tridentVarcharType = new SqlDataType(TypeValue.Text.NewFacets(TypeFacets.NewText(new long?(8000L), new bool?(true), null)), SqlLanguageStrings.VarCharSqlString);

		// Token: 0x04000CBB RID: 3259
		private static readonly SqlDataType tridentDateTimeType = new SqlDataType(TypeValue.DateTime.NewFacets(TypeFacets.NewDateTime(new int?(6), null)), SqlLanguageStrings.DateTime2SqlString);

		// Token: 0x04000CBC RID: 3260
		private static readonly SqlDataType tridentTimeType = new SqlDataType(TypeValue.Time.NewFacets(TypeFacets.NewDateTime(new int?(6), null)), SqlLanguageStrings.TimeSqlString);

		// Token: 0x04000CBD RID: 3261
		private static readonly SqlDataType tridentCurrencyType = new SqlDataType(TypeValue.Decimal.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(20), new int?(4), null)), SqlLanguageStrings.DecimalSqlString);

		// Token: 0x04000CBE RID: 3262
		private static readonly SqlSettings sql2008Settings = new SqlSettings
		{
			MaxIdentifierLength = 128,
			QuoteIdentifier = new Func<string, string>(DbEnvironment.BracketQuoteIdentifier),
			DateTimePrefix = "convert(datetime2, '",
			DateTimeSuffix = "')",
			DatePrefix = "convert(date, '",
			DateSuffix = "')",
			PagingStrategy = PagingStrategy.TopAndRowCount,
			EmptyRowInsertStrategy = EmptyRowInsertStrategy.SqlServer,
			SupportsCaseExpression = true,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsTableRotationFunctions = true,
			SupportsStoredFunctions = true,
			SupportsStoredProcedures = true,
			SupportsExtendedProperties = true,
			SupportsOutputClause = true,
			SupportsViewCreation = true,
			TypesWithLength = SqlEnvironment.TypesWithLength,
			UseMaxTypes = true,
			MaxTypesLiteral = (string typeName) => new ConstantSqlString("max"),
			AdditionalSettings = new Dictionary<string, object>
			{
				{ "SupportsClassification", false },
				{ "SupportsDateTime2", true },
				{ "SupportsDateFromParts", false }
			}
		};

		// Token: 0x04000CBF RID: 3263
		private static readonly SqlSettings azureSettings = new SqlSettings
		{
			MaxIdentifierLength = 128,
			QuoteIdentifier = new Func<string, string>(DbEnvironment.BracketQuoteIdentifier),
			DateTimePrefix = "convert(datetime2, '",
			DateTimeSuffix = "')",
			DatePrefix = "convert(date, '",
			DateSuffix = "')",
			PagingStrategy = PagingStrategy.TopAndRowCount,
			EmptyRowInsertStrategy = EmptyRowInsertStrategy.SqlServer,
			SupportsCaseExpression = true,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsTableRotationFunctions = true,
			SupportsStoredFunctions = true,
			SupportsStoredProcedures = true,
			SupportsExtendedProperties = false,
			SupportsOutputClause = true,
			SupportsViewCreation = true,
			TypesWithLength = SqlEnvironment.TypesWithLength,
			UseMaxTypes = true,
			MaxTypesLiteral = (string typeName) => new ConstantSqlString("max"),
			AdditionalSettings = new Dictionary<string, object>
			{
				{ "PartitionStatsTable", "sys.dm_db_partition_stats" },
				{ "SupportsClassification", true },
				{ "SupportsDateTime2", true },
				{ "SupportsTrimFrom", true },
				{ "HasGeneratedColumns", true },
				{ "SupportsDateFromParts", true }
			}
		};

		// Token: 0x04000CC0 RID: 3264
		private static readonly SqlSettings pdwSettings = new SqlSettings
		{
			MaxIdentifierLength = 128,
			QuoteIdentifier = new Func<string, string>(DbEnvironment.BracketQuoteIdentifier),
			DateTimePrefix = "convert(datetime2, '",
			DateTimeSuffix = "')",
			DatePrefix = "convert(date, '",
			DateSuffix = "')",
			PagingStrategy = PagingStrategy.TopAndRowCount,
			EmptyRowInsertStrategy = EmptyRowInsertStrategy.SqlServer,
			SupportsCaseExpression = true,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsTableRotationFunctions = true,
			SupportsStoredFunctions = true,
			SupportsStoredProcedures = true,
			SupportsExtendedProperties = false,
			SupportsViewCreation = true,
			TypesWithLength = SqlEnvironment.TypesWithLength,
			UseMaxTypes = true,
			MaxTypesLiteral = delegate(string typeName)
			{
				if (!string.Equals(typeName, SqlLanguageStrings.NCharSqlString.String, StringComparison.OrdinalIgnoreCase) && !string.Equals(typeName, SqlLanguageStrings.NVarCharSqlString.String, StringComparison.OrdinalIgnoreCase))
				{
					return new ConstantSqlString("8000");
				}
				return new ConstantSqlString("4000");
			},
			AdditionalSettings = new Dictionary<string, object>
			{
				{ "PartitionStatsTable", "sys.dm_pdw_nodes_db_partition_stats" },
				{ "SupportsClassification", true },
				{ "SupportsDateTime2", true },
				{ "SupportsTrimFrom", true },
				{ "SupportsDateFromParts", true }
			}
		};

		// Token: 0x04000CC1 RID: 3265
		private static readonly SqlSettings sql2005Settings = new SqlSettings
		{
			MaxIdentifierLength = 128,
			QuoteIdentifier = new Func<string, string>(DbEnvironment.BracketQuoteIdentifier),
			DateTimePrefix = "convert(datetime, '",
			DateTimeSuffix = "')",
			DatePrefix = "convert(date, '",
			DateSuffix = "')",
			PagingStrategy = PagingStrategy.TopAndRowCount,
			EmptyRowInsertStrategy = EmptyRowInsertStrategy.SqlServer,
			SupportsCaseExpression = true,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsTableRotationFunctions = true,
			SupportsStoredFunctions = true,
			SupportsStoredProcedures = true,
			SupportsExtendedProperties = true,
			SupportsOutputClause = true,
			SupportsViewCreation = true,
			TypesWithLength = SqlEnvironment.TypesWithLength,
			UseMaxTypes = true,
			MaxTypesLiteral = (string typeName) => new ConstantSqlString("max"),
			AdditionalSettings = new Dictionary<string, object>
			{
				{ "SupportsClassification", false },
				{ "SupportsDateTime2", false },
				{ "SupportsDateFromParts", false }
			}
		};

		// Token: 0x04000CC2 RID: 3266
		private static readonly SqlSettings sql2012Settings = SqlEnvironment.sql2008Settings.Clone().AddSetting("SupportsDateFromParts", true);

		// Token: 0x04000CC3 RID: 3267
		private static readonly SqlSettings sql2016Settings = SqlEnvironment.sql2012Settings.Clone().AddSetting("HasGeneratedColumns", true);

		// Token: 0x04000CC4 RID: 3268
		private static readonly SqlSettings sql2017Settings = SqlEnvironment.sql2016Settings.Clone().AddSetting("SupportsTrimFrom", true);

		// Token: 0x04000CC5 RID: 3269
		private static readonly SqlSettings sql2019Settings = SqlEnvironment.sql2017Settings.Clone().AddSetting("SupportsClassification", true);

		// Token: 0x04000CC6 RID: 3270
		private static readonly SqlSettings dataverseSettings = SqlEnvironment.sql2008Settings.Clone().With(delegate(SqlSettings settings)
		{
			settings.SupportsTableRotationFunctions = false;
		});

		// Token: 0x04000CC7 RID: 3271
		private static readonly SqlSettings tridentSettings = SqlEnvironment.pdwSettings.Clone().With(delegate(SqlSettings settings)
		{
			settings.AdditionalSettings.Remove("PartitionStatsTable");
			settings.TypesWithLength.Add(SqlLanguageStrings.DateTime2SqlString.String);
			settings.TypesWithLength.Add(SqlLanguageStrings.TimeSqlString.String);
		});

		// Token: 0x04000CC8 RID: 3272
		public const string SqlMetadataCommandText = "SELECT\r\n@@version _VERSION,\r\nCAST(SERVERPROPERTY('EngineEdition') as VARCHAR(4)) _EDITION,\r\nCASE WHEN EXISTS (SELECT * FROM sys.extended_properties WHERE [name] = N'isSaaSMetadata' AND [value] = '1') THEN 1 ELSE 0 END _IS_SAAS,\r\nCASE WHEN EXISTS (SELECT * FROM sys.types WHERE name = 'char' AND collation_name LIKE '%UTF8%') THEN 1 ELSE 0 END _UTF8_COLLATION";

		// Token: 0x04000CC9 RID: 3273
		private static ISqlClient sqlClient;

		// Token: 0x04000CCA RID: 3274
		private SqlEnvironment.SqlBatchSchemaLoader batchSchemaLoader;

		// Token: 0x04000CCB RID: 3275
		private TableValue capabilities;

		// Token: 0x04000CCC RID: 3276
		private OAuthCredential aadCredential;

		// Token: 0x04000CCD RID: 3277
		private static readonly HashSet<string> bulkInsertableTypes = new HashSet<string>
		{
			"smallint", "int", "real", "float", "money", "smallmoney", "bit", "tinyint", "bigint", "binary",
			"image", "text", "ntext", "decimal", "numeric", "datetime", "smalldatetime", "xml", "varchar", "char",
			"nchar", "nvarchar", "varbinary", "date", "time", "datetime2", "datetimeoffset"
		};

		// Token: 0x04000CCE RID: 3278
		private static readonly HashSet<string> searchableTypes = new HashSet<string>
		{
			"smallint", "int", "real", "float", "money", "smallmoney", "bit", "tinyint", "bigint", "timestamp",
			"binary", "decimal", "numeric", "datetime", "smalldatetime", "sql_variant", "varchar", "char", "nchar", "nvarchar",
			"varbinary", "uniqueidentifier", "date", "time", "datetime2", "datetimeoffset", "hierarchyid", "geometry", "geography", "udt"
		};

		// Token: 0x04000CCF RID: 3279
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMapping = new Dictionary<string, TypeValue>
		{
			{
				"smallint",
				TypeValue.Int16
			},
			{
				"int",
				TypeValue.Int32
			},
			{
				"real",
				TypeValue.Single
			},
			{
				"float",
				TypeValue.Double
			},
			{
				"money",
				TypeValue.Currency
			},
			{
				"smallmoney",
				TypeValue.Currency
			},
			{
				"bit",
				TypeValue.Logical
			},
			{
				"tinyint",
				TypeValue.Byte
			},
			{
				"bigint",
				TypeValue.Int64
			},
			{
				"timestamp",
				TypeValue.Binary
			},
			{
				"binary",
				TypeValue.Binary
			},
			{
				"image",
				TypeValue.Binary
			},
			{
				"text",
				TypeValue.Text
			},
			{
				"ntext",
				TypeValue.Text
			},
			{
				"decimal",
				TypeValue.Decimal
			},
			{
				"numeric",
				TypeValue.Decimal
			},
			{
				"datetime",
				TypeValue.DateTime
			},
			{
				"smalldatetime",
				TypeValue.DateTime
			},
			{
				"sql_variant",
				TypeValue.Any
			},
			{
				"xml",
				TypeValue.Text
			},
			{
				"varchar",
				TypeValue.Text
			},
			{
				"char",
				TypeValue.Text
			},
			{
				"nchar",
				TypeValue.Text
			},
			{
				"nvarchar",
				TypeValue.Text
			},
			{
				"varbinary",
				TypeValue.Binary
			},
			{
				"uniqueidentifier",
				TypeValue.Guid
			},
			{
				"date",
				TypeValue.Date
			},
			{
				"time",
				TypeValue.Time
			},
			{
				"datetime2",
				TypeValue.DateTime
			},
			{
				"datetimeoffset",
				TypeValue.DateTimeZone
			},
			{
				"hierarchyid",
				TypeValue.SerializedText
			},
			{
				"geometry",
				SerializedTextTypeValue.SerializedGeometryType
			},
			{
				"geography",
				SerializedTextTypeValue.SerializedGeographyType
			},
			{
				"udt",
				TypeValue.SerializedText
			}
		};

		// Token: 0x04000CD0 RID: 3280
		private static readonly HashSet<string> variableLengthTypes = new HashSet<string> { "image", "text", "ntext", "varchar", "nvarchar", "varbinary", "xml" };

		// Token: 0x04000CD1 RID: 3281
		private static SafeHandle sniLibrary;

		// Token: 0x020003B2 RID: 946
		public static class CapabilityFlags
		{
			// Token: 0x04000CD2 RID: 3282
			public const string HasGeneratedColumns = "HasGeneratedColumns";

			// Token: 0x04000CD3 RID: 3283
			public const string PartitionStatsTable = "PartitionStatsTable";

			// Token: 0x04000CD4 RID: 3284
			public const string SupportsClassification = "SupportsClassification";

			// Token: 0x04000CD5 RID: 3285
			public const string SupportsDateTime2 = "SupportsDateTime2";

			// Token: 0x04000CD6 RID: 3286
			public const string SupportsTrimFrom = "SupportsTrimFrom";

			// Token: 0x04000CD7 RID: 3287
			public const string SupportsDateFromParts = "SupportsDateFromParts";
		}

		// Token: 0x020003B3 RID: 947
		private class SqlReaderWrapper : DbEnvironment.DbReaderWrapper
		{
			// Token: 0x06002175 RID: 8565 RVA: 0x0005BF40 File Offset: 0x0005A140
			public SqlReaderWrapper(SqlEnvironment environment, IPersistentObjectCache metadataCache, string cacheKey, bool forNativeQuery)
			{
				this.environment = environment;
				this.metadataCache = metadataCache;
				this.forNativeQuery = forNativeQuery;
				ITraitTrackingService traitTrackingService = this.environment.Host.QueryService<ITraitTrackingService>();
				if (cacheKey != null && traitTrackingService != null)
				{
					this.cacheKey = PersistentCacheKey.SqlClassification.Qualify(this.environment.ConnectionInfo.CacheKey, cacheKey);
					object obj;
					if (this.metadataCache.TryGetValue(this.cacheKey, new Func<Stream, object>(SqlEnvironment.SqlReaderWrapper.DeserializeClassifications), out obj) && obj is SqlClassification[][])
					{
						this.RecordClassifications(traitTrackingService, (SqlClassification[][])obj);
						this.skipRecording = true;
					}
				}
			}

			// Token: 0x06002176 RID: 8566 RVA: 0x0005BFE9 File Offset: 0x0005A1E9
			public override DbDataReaderWithTableSchema Wrap(DbDataReaderWithTableSchema reader)
			{
				if (!this.skipRecording)
				{
					this.RecordClassification(reader);
					this.skipRecording = true;
				}
				if (!this.forNativeQuery)
				{
					reader = SqlEnvironment.SqlReader.GetDataReader(reader);
				}
				return reader;
			}

			// Token: 0x06002177 RID: 8567 RVA: 0x0005C014 File Offset: 0x0005A214
			private void RecordClassification(DbDataReaderWithTableSchema reader)
			{
				ITraitTrackingService traitTrackingService = this.environment.Host.QueryService<ITraitTrackingService>();
				DbDataReader dbDataReader = DelegatingDbDataReaderWithTableSchema.Unwrap(reader);
				SqlClassification[][] classifications = SqlEnvironment.SqlClient.GetClassifications(dbDataReader);
				if (classifications != null)
				{
					if (this.cacheKey != null)
					{
						this.metadataCache.CommitValue(this.cacheKey, new Action<Stream, object>(SqlEnvironment.SqlReaderWrapper.SerializeClassifications), classifications);
					}
					if (traitTrackingService != null)
					{
						this.RecordClassifications(traitTrackingService, classifications);
					}
				}
			}

			// Token: 0x06002178 RID: 8568 RVA: 0x0005C080 File Offset: 0x0005A280
			private void RecordClassifications(ITraitTrackingService traitTrackingService, SqlClassification[][] classifications)
			{
				Dictionary<string, ProtectionInformation> dictionary = null;
				if (classifications.Any((SqlClassification[] c) => c.Length != 0))
				{
					IMipService mipService = this.environment.Host.QueryService<IMipService>();
					ProtectionInformation[] array = ((mipService != null) ? mipService.GetClassifications(this.metadataCache, this.environment.Resource) : null);
					if (array != null)
					{
						dictionary = new Dictionary<string, ProtectionInformation>(array.Length);
						foreach (ProtectionInformation protectionInformation in array)
						{
							dictionary[protectionInformation.Id] = protectionInformation;
						}
					}
				}
				foreach (SqlClassification[] array3 in classifications)
				{
					foreach (SqlClassification sqlClassification in array3)
					{
						RecordValue recordValue = RecordValue.New(LineageModule.TraitsKeys, new Value[]
						{
							SqlEnvironment.SqlReaderWrapper.sqlProvider,
							SqlEnvironment.SqlReaderWrapper.sqlClassificationIdentifier,
							RecordValue.New(SqlEnvironment.SqlReaderWrapper.classificationTraitKeys, new Value[]
							{
								TextValue.NewOrNull(sqlClassification.LabelId),
								TextValue.NewOrNull(sqlClassification.LabelName),
								TextValue.NewOrNull(sqlClassification.InformationTypeId),
								TextValue.NewOrNull(sqlClassification.InformationTypeName)
							})
						});
						traitTrackingService.AddTrait(recordValue);
						ProtectionInformation protectionInformation2;
						if (dictionary != null && sqlClassification.LabelId != null && dictionary.TryGetValue(sqlClassification.LabelId, out protectionInformation2) && protectionInformation2.Id != null)
						{
							traitTrackingService.AddTrait(InformationProtectionTraits.CreateTrait(protectionInformation2));
						}
					}
				}
			}

			// Token: 0x06002179 RID: 8569 RVA: 0x0005C1FE File Offset: 0x0005A3FE
			private static object DeserializeClassifications(Stream stream)
			{
				return new BinaryReader(stream).ReadArray((BinaryReader r) => r.ReadArray(new Func<BinaryReader, SqlClassification>(SqlEnvironment.SqlReaderWrapper.ReadSqlClassification)));
			}

			// Token: 0x0600217A RID: 8570 RVA: 0x0005C22A File Offset: 0x0005A42A
			private static void SerializeClassifications(Stream stream, object value)
			{
				new BinaryWriter(stream).WriteArray((SqlClassification[][])value, delegate(BinaryWriter w, SqlClassification[] a)
				{
					w.WriteArray(a, new Action<BinaryWriter, SqlClassification>(SqlEnvironment.SqlReaderWrapper.WriteSqlClassification));
				});
			}

			// Token: 0x0600217B RID: 8571 RVA: 0x0005C25C File Offset: 0x0005A45C
			private static SqlClassification ReadSqlClassification(BinaryReader reader)
			{
				return new SqlClassification
				{
					LabelId = reader.ReadNullableString(),
					LabelName = reader.ReadNullableString(),
					InformationTypeId = reader.ReadNullableString(),
					InformationTypeName = reader.ReadNullableString()
				};
			}

			// Token: 0x0600217C RID: 8572 RVA: 0x0005C293 File Offset: 0x0005A493
			private static void WriteSqlClassification(BinaryWriter writer, SqlClassification sqlClassification)
			{
				writer.WriteNullableString(sqlClassification.LabelId);
				writer.WriteNullableString(sqlClassification.LabelName);
				writer.WriteNullableString(sqlClassification.InformationTypeId);
				writer.WriteNullableString(sqlClassification.InformationTypeName);
			}

			// Token: 0x04000CD8 RID: 3288
			private static readonly Keys classificationTraitKeys = Keys.New("LabelId", "LabelName", "InformationTypeId", "InformationTypeName");

			// Token: 0x04000CD9 RID: 3289
			private static readonly TextValue sqlProvider = TextValue.New("SQL");

			// Token: 0x04000CDA RID: 3290
			private static readonly TextValue sqlClassificationIdentifier = TextValue.New("Classification");

			// Token: 0x04000CDB RID: 3291
			private readonly SqlEnvironment environment;

			// Token: 0x04000CDC RID: 3292
			private readonly IPersistentObjectCache metadataCache;

			// Token: 0x04000CDD RID: 3293
			private readonly string cacheKey;

			// Token: 0x04000CDE RID: 3294
			private readonly bool forNativeQuery;

			// Token: 0x04000CDF RID: 3295
			private bool skipRecording;
		}

		// Token: 0x020003B5 RID: 949
		private sealed class SqlBatchSchemaLoader : SingleQueueBatchSchemaLoader
		{
			// Token: 0x06002183 RID: 8579 RVA: 0x0005C33F File Offset: 0x0005A53F
			public SqlBatchSchemaLoader(SqlEnvironment environment)
				: base(environment, false, environment.PrefetchMetadata)
			{
				this.sqlEnvironment = environment;
			}

			// Token: 0x06002184 RID: 8580 RVA: 0x0005C358 File Offset: 0x0005A558
			protected override string GetColumnsQuery(SchemaItem[] items)
			{
				string text = "or c.generated_always_type > 0";
				string text2 = (this.sqlEnvironment.IsTrident ? "convert(nvarchar, null)" : "d.definition");
				string text3 = (this.sqlEnvironment.IsTrident ? string.Empty : "left join sys.default_constraints d on d.object_id = c.default_object_id");
				string text4 = (this.sqlEnvironment.IsTrident ? "convert(nvarchar, null)" : "cc.definition");
				string text5 = (this.sqlEnvironment.IsTrident ? string.Empty : "left join sys.computed_columns cc on c.object_id = cc.object_id and c.column_id = cc.column_id");
				return string.Format(CultureInfo.InvariantCulture, "select\r\n    s.name [TABLE_SCHEMA],\r\n    o.name [TABLE_NAME],\r\n    c.name [COLUMN_NAME],\r\n    cast(c.column_id as bigint) [ORDINAL_POSITION],\r\n    c.is_nullable [IS_NULLABLE],\r\n    {0},\r\n    {4} [COLUMN_DEFAULT],\r\n    {5} [COLUMN_EXPRESSION],\r\n    case when c.is_identity = 1 or c.is_computed = 1 or t.system_type_id = 189 {1} then 0 else 1 end [IS_WRITABLE],\r\n    {3} FIELD_CAPTION\r\nfrom sys.objects o\r\njoin sys.schemas s on s.schema_id = o.schema_id\r\njoin sys.columns c on o.object_id = c.object_id\r\nleft join sys.types t on c.user_type_id = t.user_type_id\r\nleft join sys.types t_system on t.system_type_id = t_system.user_type_id\r\n{6}\r\n{7}\r\nleft join {2} e on o.object_id = e.major_id and c.column_id = e.minor_id and e.class = 1 and e.name = 'MS_Description'\r\nwhere ", new object[]
				{
					this.sqlEnvironment.GetColumnInfoPartialQuery("t", "t_system", "c", "e"),
					this.sqlEnvironment.SqlSettings.GetSetting<bool>("HasGeneratedColumns", false) ? text : "",
					this.sqlEnvironment.ExtendedPropertiesTable,
					this.sqlEnvironment.IncludeDataverseFieldCaptions ? "c.DisplayName" : "null",
					text2,
					text4,
					text3,
					text5
				}) + base.GenerateClause("s.name", "o.name", items) + ";";
			}

			// Token: 0x06002185 RID: 8581 RVA: 0x0005C484 File Offset: 0x0005A684
			protected override string GetForeignKeyQuery(SchemaItem[] items)
			{
				if (!this.sqlEnvironment.IsSaas)
				{
					return this.GetStandardForeignKeyQuery(items);
				}
				return this.GetSaasForeignKeyQuery(items, true);
			}

			// Token: 0x06002186 RID: 8582 RVA: 0x0005C4A4 File Offset: 0x0005A6A4
			private string GetSaasForeignKeyQuery(SchemaItem[] items, bool withOrdering = true)
			{
				return string.Concat(new string[]
				{
					"select FK_NAME, ORDINAL, TABLE_SCHEMA_1, TABLE_NAME_1, TABLE_COLUMN_1, TABLE_SCHEMA_2, TABLE_NAME_2, TABLE_COLUMN_2 from [metadata].[fn_relationships]() where ",
					base.GenerateClause("TABLE_SCHEMA_1", "TABLE_NAME_1", items),
					global::System.Environment.NewLine,
					"union",
					global::System.Environment.NewLine,
					"select FK_NAME, ORDINAL, TABLE_SCHEMA_1, TABLE_NAME_1, TABLE_COLUMN_1, TABLE_SCHEMA_2, TABLE_NAME_2, TABLE_COLUMN_2 from [metadata].[fn_relationships]() where ",
					base.GenerateClause("TABLE_SCHEMA_2", "TABLE_NAME_2", items),
					global::System.Environment.NewLine,
					withOrdering ? "order by ORDINAL, TABLE_COLUMN_1, TABLE_COLUMN_2" : ""
				});
			}

			// Token: 0x06002187 RID: 8583 RVA: 0x0005C528 File Offset: 0x0005A728
			private string GetStandardForeignKeyQuery(SchemaItem[] items)
			{
				return string.Concat(new string[]
				{
					"select\r\n    convert(nvarchar, fk.object_id) [FK_NAME], cast(f.constraint_column_id as bigint) [ORDINAL],\r\n    s1.name [TABLE_SCHEMA_1], o1.name [TABLE_NAME_1], c1.name [PK_COLUMN_NAME_1],\r\n    s2.name [TABLE_SCHEMA_2], o2.name [TABLE_NAME_2], c2.name [PK_COLUMN_NAME_2],\r\n    f.constraint_object_id, f.constraint_column_id\r\nfrom sys.foreign_key_columns f join sys.foreign_keys fk on f.constraint_object_id = fk.object_id\r\njoin sys.objects o1 on o1.object_id = f.parent_object_id join sys.schemas s1 on s1.schema_id = o1.schema_id\r\njoin sys.objects o2 on o2.object_id = f.referenced_object_id join sys.schemas s2 on s2.schema_id = o2.schema_id\r\njoin sys.columns c1 on c1.object_id = o1.object_id and c1.column_id = f.parent_column_id\r\njoin sys.columns c2 on c2.object_id = o2.object_id and c2.column_id = f.referenced_column_id\r\nwhere ",
					base.GenerateClause("s1.name", "o1.name", items),
					global::System.Environment.NewLine,
					"union",
					global::System.Environment.NewLine,
					"select\r\n    convert(nvarchar, fk.object_id) [FK_NAME], cast(f.constraint_column_id as bigint) [ORDINAL],\r\n    s1.name [TABLE_SCHEMA_1], o1.name [TABLE_NAME_1], c1.name [PK_COLUMN_NAME_1],\r\n    s2.name [TABLE_SCHEMA_2], o2.name [TABLE_NAME_2], c2.name [PK_COLUMN_NAME_2],\r\n    f.constraint_object_id, f.constraint_column_id\r\nfrom sys.foreign_key_columns f join sys.foreign_keys fk on f.constraint_object_id = fk.object_id\r\njoin sys.objects o1 on o1.object_id = f.parent_object_id join sys.schemas s1 on s1.schema_id = o1.schema_id\r\njoin sys.objects o2 on o2.object_id = f.referenced_object_id join sys.schemas s2 on s2.schema_id = o2.schema_id\r\njoin sys.columns c1 on c1.object_id = o1.object_id and c1.column_id = f.parent_column_id\r\njoin sys.columns c2 on c2.object_id = o2.object_id and c2.column_id = f.referenced_column_id\r\nwhere ",
					base.GenerateClause("s2.name", "o2.name", items),
					global::System.Environment.NewLine,
					"order by f.constraint_object_id, f.constraint_column_id;"
				});
			}

			// Token: 0x06002188 RID: 8584 RVA: 0x0005C5A1 File Offset: 0x0005A7A1
			protected override string GetIndexesQuery(SchemaItem[] items)
			{
				if (this.sqlEnvironment.IsSaas)
				{
					return this.GetSaasIndexesQuery(items);
				}
				return this.GetStandardIndexesQuery(items, true);
			}

			// Token: 0x06002189 RID: 8585 RVA: 0x0005C5C0 File Offset: 0x0005A7C0
			private string GetSaasIndexesQuery(SchemaItem[] items)
			{
				string saasForeignKeyQuery = this.GetSaasForeignKeyQuery(items, false);
				string standardIndexesQuery = this.GetStandardIndexesQuery(items, false);
				return string.Concat(new string[] { "select [R].[TABLE_SCHEMA], [R].[TABLE_NAME], [R].[INDEX_NAME], [R].[COLUMN_NAME], [R].[ORDINAL_POSITION], [R].[PRIMARY_KEY] from ((\r\nselect\r\n  [F].[TABLE_SCHEMA_2] collate Latin1_General_100_CI_AS as [TABLE_SCHEMA],\r\n  [F].[TABLE_NAME_2] collate Latin1_General_100_CI_AS as [TABLE_NAME],\r\n  [F].[FK_NAME] collate Latin1_General_100_CI_AS as [INDEX_NAME],\r\n  [F].[TABLE_COLUMN_2] collate Latin1_General_100_CI_AS as [COLUMN_NAME],\r\n  [F].[ORDINAL] as [ORDINAL_POSITION],\r\n  cast(1 as bit) as [PRIMARY_KEY]\r\nfrom (", saasForeignKeyQuery, ") as [F]) union (", standardIndexesQuery, ")) as [R]\r\norder by [R].[TABLE_SCHEMA], [R].[TABLE_NAME], [R].[INDEX_NAME];" });
			}

			// Token: 0x0600218A RID: 8586 RVA: 0x0005C60A File Offset: 0x0005A80A
			private string GetStandardIndexesQuery(SchemaItem[] items, bool withOrdering = true)
			{
				return "select s.name [TABLE_SCHEMA], o.name [TABLE_NAME], i.name [INDEX_NAME], cc.name [COLUMN_NAME], cast(ic.key_ordinal as bigint) [ORDINAL_POSITION], i.is_primary_key [PRIMARY_KEY]\r\nfrom sys.objects o join sys.schemas s on s.schema_id = o.schema_id\r\njoin sys.indexes as i on i.object_id = o.object_id\r\njoin sys.index_columns as ic on ic.object_id = i.object_id and ic.index_id = i.index_id\r\njoin sys.columns as cc on ic.column_id = cc.column_id and ic.object_id = cc.object_id\r\nwhere (i.is_primary_key = 1 or i.is_unique_constraint = 1 or i.is_unique = 1) and o.type in ('U', 'V') and ic.key_ordinal <> 0\r\n      and " + base.GenerateClause("s.name", "o.name", items) + (withOrdering ? "\r\norder by i.name, s.name, o.name;" : "");
			}

			// Token: 0x04000CE4 RID: 3300
			private readonly SqlEnvironment sqlEnvironment;
		}

		// Token: 0x020003B6 RID: 950
		private sealed class SqlConnectionStringBuilder : ConnectionStringResourceCredentialDispatcher
		{
			// Token: 0x0600218B RID: 8587 RVA: 0x0005C636 File Offset: 0x0005A836
			public SqlConnectionStringBuilder(SqlEnvironment environment)
				: base(environment.Host, environment.Resource)
			{
				this.environment = environment;
			}

			// Token: 0x17000E54 RID: 3668
			// (get) Token: 0x0600218C RID: 8588 RVA: 0x0005C651 File Offset: 0x0005A851
			protected override string UserNameKey
			{
				get
				{
					return "User Id";
				}
			}

			// Token: 0x17000E55 RID: 3669
			// (get) Token: 0x0600218D RID: 8589 RVA: 0x00047C8A File Offset: 0x00045E8A
			protected override string PasswordKey
			{
				get
				{
					return "Password";
				}
			}

			// Token: 0x17000E56 RID: 3670
			// (get) Token: 0x0600218E RID: 8590 RVA: 0x00047C91 File Offset: 0x00045E91
			protected override string ServerKey
			{
				get
				{
					return "Data Source";
				}
			}

			// Token: 0x17000E57 RID: 3671
			// (get) Token: 0x0600218F RID: 8591 RVA: 0x000020FA File Offset: 0x000002FA
			protected override string PortKey
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000E58 RID: 3672
			// (get) Token: 0x06002190 RID: 8592 RVA: 0x0005C658 File Offset: 0x0005A858
			protected override string DatabaseKey
			{
				get
				{
					return "Initial Catalog";
				}
			}

			// Token: 0x17000E59 RID: 3673
			// (get) Token: 0x06002191 RID: 8593 RVA: 0x00047C9F File Offset: 0x00045E9F
			protected override string IntegratedSecurityKey
			{
				get
				{
					return "Integrated Security";
				}
			}

			// Token: 0x17000E5A RID: 3674
			// (get) Token: 0x06002192 RID: 8594 RVA: 0x0005C65F File Offset: 0x0005A85F
			protected override string EncryptKey
			{
				get
				{
					return "Encrypt";
				}
			}

			// Token: 0x17000E5B RID: 3675
			// (get) Token: 0x06002193 RID: 8595 RVA: 0x00047CAD File Offset: 0x00045EAD
			protected override object AuthenticationTypeValue
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E5C RID: 3676
			// (get) Token: 0x06002194 RID: 8596 RVA: 0x0005C666 File Offset: 0x0005A866
			protected override string PortSeparator
			{
				get
				{
					return ",";
				}
			}

			// Token: 0x17000E5D RID: 3677
			// (get) Token: 0x06002195 RID: 8597 RVA: 0x0005C66D File Offset: 0x0005A86D
			protected override string ConnectionTimeoutKey
			{
				get
				{
					return "Connect Timeout";
				}
			}

			// Token: 0x17000E5E RID: 3678
			// (get) Token: 0x06002196 RID: 8598 RVA: 0x0005C674 File Offset: 0x0005A874
			protected override int? DefaultConnectionTimeout
			{
				get
				{
					if (!this.environment.IsSynapseSqlOnDemand)
					{
						return base.DefaultConnectionTimeout;
					}
					return new int?(300);
				}
			}

			// Token: 0x06002197 RID: 8599 RVA: 0x0005C694 File Offset: 0x0005A894
			protected override bool ApplyEncryptedCredentialAdornment(EncryptedConnectionAdornment credential)
			{
				if (credential.RequireEncryption)
				{
					this.builder[this.EncryptKey] = true;
					this.builder["TrustServerCertificate"] = false;
				}
				string configurationProperty = this.environment.Host.GetConfigurationProperty("SqlTrustedServers", null);
				if (!string.IsNullOrEmpty(configurationProperty))
				{
					string text = ((string)this.builder[this.ServerKey]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
					if (this.IsServerTrusted(text, configurationProperty))
					{
						this.builder["TrustServerCertificate"] = true;
					}
				}
				return true;
			}

			// Token: 0x06002198 RID: 8600 RVA: 0x0005C748 File Offset: 0x0005A948
			private bool IsServerTrusted(string server, string trustedServers)
			{
				foreach (string text in from s in trustedServers.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
					select s.Trim())
				{
					if (string.Equals(server, text, StringComparison.OrdinalIgnoreCase) || (text.Contains('*') && Regex.IsMatch(server, "^" + Regex.Escape(text).Replace("\\*", ".*") + "$", RegexOptions.IgnoreCase)))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06002199 RID: 8601 RVA: 0x0005C7E2 File Offset: 0x0005A9E2
			protected override bool Apply(OAuthCredential credential)
			{
				if (!SqlEnvironment.SqlClient.SupportsAad)
				{
					throw DataSourceException.NewDataSourceError<Message0>(base.Host, Microsoft.Mashup.Engine1.Strings.Sql_Aad_DotNetFourSix, base.Resource, null, null);
				}
				base.SetOAuthIdentity(credential);
				this.environment.aadCredential = credential;
				return true;
			}

			// Token: 0x0600219A RID: 8602 RVA: 0x0005C820 File Offset: 0x0005AA20
			protected override void AddOptions()
			{
				this.builder["Application Name"] = "Mashup Engine";
				object obj;
				if (this.environment.Host.TryGetConfigurationProperty("HostContext", out obj))
				{
					DbConnectionStringBuilder builder = this.builder;
					DbConnectionStringBuilder dbConnectionStringBuilder = builder;
					string text = "Application Name";
					object obj2 = builder["Application Name"];
					dbConnectionStringBuilder[text] = ((obj2 != null) ? obj2.ToString() : null) + string.Format(CultureInfo.InvariantCulture, " ({0})", obj);
				}
				if (this.environment.MultiSubnetFailover)
				{
					this.builder["MultiSubnetFailover"] = "True";
					this.builder["ApplicationIntent"] = (this.environment.Host.IsActionPermitted(base.Resource) ? "ReadWrite" : "ReadOnly");
				}
			}

			// Token: 0x04000CE5 RID: 3301
			private readonly SqlEnvironment environment;
		}

		// Token: 0x020003B8 RID: 952
		private static class SqlReader
		{
			// Token: 0x0600219E RID: 8606 RVA: 0x0005C904 File Offset: 0x0005AB04
			public static DbDataReaderWithTableSchema GetDataReader(DbDataReaderWithTableSchema reader)
			{
				TableSchema tableSchema;
				if (SqlEnvironment.SqlReader.TryProcessSchemaTable(reader.Schema, (SchemaColumn row) => string.Equals("uniqueidentifier", row.DataTypeName, StringComparison.OrdinalIgnoreCase), SqlEnvironment.SqlReader.changeColumnsDataReader, out tableSchema))
				{
					return TypeConvertingDbDataReader.New(reader, reader.Schema, tableSchema, SqlEnvironment.SqlReader.typeConversionsDataReader);
				}
				return reader;
			}

			// Token: 0x0600219F RID: 8607 RVA: 0x0005C958 File Offset: 0x0005AB58
			public static IPageReader GetPageReader(IPageReader pageReader)
			{
				TableSchema tableSchema;
				if (SqlEnvironment.SqlReader.TryProcessSchemaTable(pageReader.Schema, (SchemaColumn row) => row.DataType == typeof(Guid), SqlEnvironment.SqlReader.changeColumnsPageReader, out tableSchema))
				{
					return TypeConvertingPageReader.New(pageReader, tableSchema, SqlEnvironment.SqlReader.typeConversionsPageReader);
				}
				return pageReader;
			}

			// Token: 0x060021A0 RID: 8608 RVA: 0x0005C9A6 File Offset: 0x0005ABA6
			private static void AddGuidValue(object value, Column column)
			{
				column.AddValue(SqlEnvironment.SqlReader.ConvertGuidValue(value));
			}

			// Token: 0x060021A1 RID: 8609 RVA: 0x0005C9B4 File Offset: 0x0005ABB4
			private static bool TryProcessSchemaTable(TableSchema originalSchema, Func<SchemaColumn, bool> needChange, Action<SchemaColumn> applyChange, out TableSchema schema)
			{
				schema = null;
				List<int> list = new List<int>();
				for (int i = 0; i < originalSchema.ColumnCount; i++)
				{
					if (needChange(originalSchema.GetColumn(i)))
					{
						list.Add(i);
					}
				}
				if (list.Count == 0)
				{
					return false;
				}
				schema = originalSchema.Copy();
				foreach (int num in list)
				{
					SchemaColumn column = schema.GetColumn(num);
					if (column.Ordinal != null)
					{
						applyChange(column);
					}
				}
				return true;
			}

			// Token: 0x060021A2 RID: 8610 RVA: 0x0005CA64 File Offset: 0x0005AC64
			private static object ConvertGuidValue(object guidValue)
			{
				if (guidValue != DBNull.Value && guidValue != null)
				{
					return ((Guid)guidValue).ToString().ToUpperInvariant();
				}
				return guidValue;
			}

			// Token: 0x04000CE8 RID: 3304
			private static readonly TypeConversion[] typeConversionsDataReader = new TypeConversion[]
			{
				new TypeConversion(typeof(Guid), new Type[] { typeof(string) }, new SqlEnvironment.SqlReader.GuidToStringValueConversion())
			};

			// Token: 0x04000CE9 RID: 3305
			private static readonly Action<SchemaColumn> changeColumnsDataReader = delegate(SchemaColumn column)
			{
				column.DataType = typeof(string);
				column.DataTypeName = SqlLanguageStrings.NCharSqlString.String;
				column.ColumnSize = new long?(36L);
				column.ProviderType = new int?(23);
			};

			// Token: 0x04000CEA RID: 3306
			private static readonly TypeConversion[] typeConversionsPageReader = new TypeConversion[]
			{
				new TypeConversion(typeof(Guid), new Type[] { typeof(string) }, new ColumnConversion(typeof(string), new Action<object, Column>(SqlEnvironment.SqlReader.AddGuidValue)))
			};

			// Token: 0x04000CEB RID: 3307
			private static readonly Action<SchemaColumn> changeColumnsPageReader = delegate(SchemaColumn column)
			{
				column.DataType = typeof(string);
			};

			// Token: 0x020003B9 RID: 953
			private sealed class GuidToStringValueConversion : ValueConversion
			{
				// Token: 0x17000E5F RID: 3679
				// (get) Token: 0x060021A4 RID: 8612 RVA: 0x0005CB4F File Offset: 0x0005AD4F
				public override Type ResultType
				{
					get
					{
						return typeof(string);
					}
				}

				// Token: 0x060021A5 RID: 8613 RVA: 0x0005CB5B File Offset: 0x0005AD5B
				public override object GetValue(DbDataReader reader, int ordinal)
				{
					return SqlEnvironment.SqlReader.ConvertGuidValue(reader.GetValue(ordinal));
				}

				// Token: 0x060021A6 RID: 8614 RVA: 0x0005CB69 File Offset: 0x0005AD69
				public override object GetValue(object[] inputArray, int index)
				{
					return SqlEnvironment.SqlReader.ConvertGuidValue(inputArray[index]);
				}

				// Token: 0x060021A7 RID: 8615 RVA: 0x0005CB73 File Offset: 0x0005AD73
				public override string GetString(DbDataReader reader, int ordinal)
				{
					return (string)this.GetValue(reader, ordinal);
				}
			}
		}

		// Token: 0x020003BB RID: 955
		internal sealed class SqlBulkCopyWrapper : IBulkCopy
		{
			// Token: 0x060021AF RID: 8623 RVA: 0x0005CC1D File Offset: 0x0005AE1D
			public SqlBulkCopyWrapper(SqlEnvironment environment, string targetSchema, string targetTable)
			{
				this.environment = environment;
				this.targetSchema = targetSchema;
				this.targetTable = targetTable;
			}

			// Token: 0x17000E60 RID: 3680
			// (get) Token: 0x060021B0 RID: 8624 RVA: 0x0005CC3C File Offset: 0x0005AE3C
			internal string DestinationTableName
			{
				get
				{
					string text;
					using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
					{
						ScriptWriter scriptWriter = new ScriptWriter(stringWriter, this.environment.SqlSettings);
						new TableReference((this.targetSchema != null) ? Alias.NewNativeAlias(this.targetSchema) : null, Alias.NewNativeAlias(this.targetTable), this.environment.UserOptions.GetBool("EnableCrossDatabaseFolding", false) ? Alias.NewNativeAlias(this.environment.Database) : null).WriteCreateScript(scriptWriter);
						text = stringWriter.ToString();
					}
					return text;
				}
			}

			// Token: 0x060021B1 RID: 8625 RVA: 0x0005CCE4 File Offset: 0x0005AEE4
			public bool TryCopyFrom(IPageReader reader, out long rowsAffected)
			{
				HashSet<string> hashSet = this.environment.ConvertDbExceptions<HashSet<string>>(delegate
				{
					HashSet<string> hashSet2 = new HashSet<string>();
					foreach (object obj in this.environment.GetColumns(this.targetSchema, this.targetTable).Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string stringSchemaColumn = DbEnvironment.GetStringSchemaColumn(dataRow, "COLUMN_NAME");
						string stringSchemaColumn2 = DbEnvironment.GetStringSchemaColumn(dataRow, "DATA_TYPE");
						if (SqlEnvironment.bulkInsertableTypes.Contains(stringSchemaColumn2))
						{
							hashSet2.Add(stringSchemaColumn);
						}
					}
					return hashSet2;
				});
				foreach (SchemaColumn schemaColumn in reader.Schema)
				{
					if (!hashSet.Contains(schemaColumn.Name))
					{
						rowsAffected = 0L;
						return false;
					}
				}
				rowsAffected = this.CopyFrom(reader);
				return true;
			}

			// Token: 0x060021B2 RID: 8626 RVA: 0x0005CD64 File Offset: 0x0005AF64
			private long CopyFrom(IPageReader reader)
			{
				return this.environment.ConvertDbExceptions<long>(delegate
				{
					Keys identityColumns = this.environment.GetIdentityColumnNames(this.targetSchema, this.targetTable);
					bool flag = reader.Schema.Any((SchemaColumn sc) => identityColumns.Contains(sc.Name));
					reader = TypeConvertingPageReader.New(reader, this.TranslateSchemaTable(this.environment.GetColumns(this.targetSchema, this.targetTable)), SqlEnvironment.SqlBulkCopyWrapper.typeConversions);
					ConnectionInfo connectionInfo = this.environment.CreateConnectionInfo();
					DbTransaction dbTransaction = ((this.environment.TransactionInfo != null) ? this.environment.TransactionInfo.Transaction : null);
					long rowsCopied2;
					using (DbConnection dbConnection = this.environment.CreateConnection())
					{
						using (HostResourcePermissionService.WaitForGovernedHandle(this.environment.Host, this.environment.Resource))
						{
							using (connectionInfo.Impersonate())
							{
								dbConnection.ConnectionString = connectionInfo.ConnectionString;
								dbConnection.Open();
								try
								{
									using (ISqlBulkCopy sqlBulkCopy = SqlEnvironment.SqlClient.CreateBulkCopy(DbEnvironment.GetUnwrappedConnection(dbConnection), flag, DbEnvironment.GetUnwrappedTransaction(dbTransaction)))
									{
										IHostProgress hostProgress = ProgressService.GetHostProgress(this.environment.Host, this.environment.Resource.Kind, ProgressDbDataSource.GetDataSource(dbConnection));
										long rowsCopied = 0L;
										sqlBulkCopy.BulkCopyTimeout = this.environment.CommandTimeout.GetValueOrDefault(600);
										sqlBulkCopy.DestinationTableName = this.DestinationTableName;
										sqlBulkCopy.BatchSize = 10000;
										sqlBulkCopy.NotifyAfter = 10000;
										sqlBulkCopy.RowsCopied = delegate(long rows)
										{
											rowsCopied = rows;
										};
										foreach (SchemaColumn schemaColumn in reader.Schema)
										{
											sqlBulkCopy.AddColumnMapping(schemaColumn.Name, schemaColumn.Name);
										}
										using (EngineContext.Leave())
										{
											using (IPageReader pageReader = reader.LeaveEngineContext<IPageReader>())
											{
												using (IDataReader dataReader = new PageReaderDataReader(pageReader))
												{
													sqlBulkCopy.WriteToServer(dataReader);
												}
											}
										}
										using (DbCommand dbCommand = dbConnection.CreateCommand())
										{
											dbCommand.CommandText = "select rowcount_big()";
											long num = (long)dbCommand.ExecuteScalar();
											if (num != 10000L)
											{
												rowsCopied += num;
											}
											hostProgress.RecordRowsWritten(rowsCopied);
											rowsCopied2 = rowsCopied;
										}
									}
								}
								finally
								{
									dbConnection.Close();
								}
							}
						}
					}
					return rowsCopied2;
				});
			}

			// Token: 0x060021B3 RID: 8627 RVA: 0x0005CD9C File Offset: 0x0005AF9C
			private TableSchema TranslateSchemaTable(DataTable schemaTable)
			{
				TableSchema tableSchema = new TableSchema(schemaTable.Rows.Count);
				for (int i = 0; i < schemaTable.Rows.Count; i++)
				{
					string text = (string)schemaTable.Rows[i]["COLUMN_NAME"];
					string text2 = (string)schemaTable.Rows[i]["DATA_TYPE"];
					bool flag = (bool)schemaTable.Rows[i]["IS_NULLABLE"];
					Type type = this.environment.NativeToClrTypeMapping[text2].NonNullable.ToClrType();
					tableSchema.AddColumn(text, type, flag);
				}
				return tableSchema;
			}

			// Token: 0x060021B4 RID: 8628 RVA: 0x0005CE58 File Offset: 0x0005B058
			private static void AddDate(object value, Column column)
			{
				column.AddValue(((Date)value).DateTime);
			}

			// Token: 0x060021B5 RID: 8629 RVA: 0x0005CE80 File Offset: 0x0005B080
			private static void AddTime(object value, Column column)
			{
				column.AddValue(((Time)value).TimeSpan);
			}

			// Token: 0x060021B6 RID: 8630 RVA: 0x0005CEA8 File Offset: 0x0005B0A8
			private static void AddCurrency(object value, Column column)
			{
				column.AddValue(((Currency)value).Value);
			}

			// Token: 0x060021B7 RID: 8631 RVA: 0x0005CECE File Offset: 0x0005B0CE
			private static void AddTruncatedDouble(object value, Column column)
			{
				column.AddValue(Math.Truncate((double)value));
			}

			// Token: 0x060021B8 RID: 8632 RVA: 0x0005CEE6 File Offset: 0x0005B0E6
			private static void AddTruncatedFloat(object value, Column column)
			{
				column.AddValue(Math.Truncate((double)((float)value)));
			}

			// Token: 0x060021B9 RID: 8633 RVA: 0x0005CEFF File Offset: 0x0005B0FF
			private static void AddTruncatedDecimal(object value, Column column)
			{
				column.AddValue(Math.Truncate((decimal)value));
			}

			// Token: 0x060021BA RID: 8634 RVA: 0x0005CF18 File Offset: 0x0005B118
			private static void AddTruncatedNumber(object value, Column column)
			{
				SqlEnvironment.SqlBulkCopyWrapper.AddTruncatedDecimal(((Number)value).ToDecimal(), column);
			}

			// Token: 0x04000CEF RID: 3311
			private const int batchSize = 10000;

			// Token: 0x04000CF0 RID: 3312
			private static readonly Type[] integralTypes = new Type[]
			{
				typeof(long),
				typeof(int),
				typeof(short),
				typeof(sbyte),
				typeof(byte)
			};

			// Token: 0x04000CF1 RID: 3313
			private static readonly TypeConversion[] typeConversions = new TypeConversion[]
			{
				new TypeConversion(typeof(Date), null, new ColumnConversion(typeof(DateTime), new Action<object, Column>(SqlEnvironment.SqlBulkCopyWrapper.AddDate))),
				new TypeConversion(typeof(Time), null, new ColumnConversion(typeof(TimeSpan), new Action<object, Column>(SqlEnvironment.SqlBulkCopyWrapper.AddTime))),
				new TypeConversion(typeof(Currency), null, new ColumnConversion(typeof(decimal), new Action<object, Column>(SqlEnvironment.SqlBulkCopyWrapper.AddCurrency))),
				new TypeConversion(typeof(double), SqlEnvironment.SqlBulkCopyWrapper.integralTypes, new ColumnConversion(typeof(double), new Action<object, Column>(SqlEnvironment.SqlBulkCopyWrapper.AddTruncatedDouble))),
				new TypeConversion(typeof(float), SqlEnvironment.SqlBulkCopyWrapper.integralTypes, new ColumnConversion(typeof(float), new Action<object, Column>(SqlEnvironment.SqlBulkCopyWrapper.AddTruncatedFloat))),
				new TypeConversion(typeof(decimal), SqlEnvironment.SqlBulkCopyWrapper.integralTypes, new ColumnConversion(typeof(decimal), new Action<object, Column>(SqlEnvironment.SqlBulkCopyWrapper.AddTruncatedDecimal))),
				new TypeConversion(typeof(Number), null, new ColumnConversion(typeof(decimal), new Action<object, Column>(SqlEnvironment.SqlBulkCopyWrapper.AddTruncatedNumber)))
			};

			// Token: 0x04000CF2 RID: 3314
			private readonly SqlEnvironment environment;

			// Token: 0x04000CF3 RID: 3315
			private readonly string targetSchema;

			// Token: 0x04000CF4 RID: 3316
			private readonly string targetTable;
		}

		// Token: 0x020003BF RID: 959
		private sealed class SetContextInfoConnection : DelegatingDbConnection
		{
			// Token: 0x060021C3 RID: 8643 RVA: 0x0005D5D4 File Offset: 0x0005B7D4
			public SetContextInfoConnection(DbConnection connection, byte[] contextInfo)
				: base(connection)
			{
				this.contextInfo = contextInfo;
			}

			// Token: 0x060021C4 RID: 8644 RVA: 0x0005D5E4 File Offset: 0x0005B7E4
			public override void Open()
			{
				base.Open();
				try
				{
					using (DbCommand dbCommand = this.CreateDbCommand())
					{
						dbCommand.CommandText = "SET CONTEXT_INFO @ContextInfo;";
						DbParameter dbParameter = dbCommand.CreateParameter();
						dbParameter.ParameterName = "@ContextInfo";
						dbParameter.DbType = DbType.Binary;
						dbParameter.Value = this.contextInfo;
						dbCommand.Parameters.Add(dbParameter);
						dbCommand.ExecuteNonQuery();
					}
				}
				catch
				{
					base.Close();
					throw;
				}
			}

			// Token: 0x04000CF9 RID: 3321
			private readonly byte[] contextInfo;
		}

		// Token: 0x020003C0 RID: 960
		private sealed class CorrelatingSqlConnection : DelegatingDbConnection
		{
			// Token: 0x060021C5 RID: 8645 RVA: 0x0005D678 File Offset: 0x0005B878
			public CorrelatingSqlConnection(DbConnection connection, string correlationId)
				: base(connection)
			{
				this.correlationId = correlationId;
			}

			// Token: 0x060021C6 RID: 8646 RVA: 0x0005D688 File Offset: 0x0005B888
			protected override DbCommand CreateDbCommand()
			{
				DbCommand dbCommand = base.CreateDbCommand();
				DbParameter dbParameter = dbCommand.CreateParameter();
				dbParameter.ParameterName = "@tds_client_correlation_id";
				dbParameter.DbType = DbType.String;
				dbParameter.Value = this.correlationId;
				dbCommand.Parameters.Add(dbParameter);
				return dbCommand;
			}

			// Token: 0x04000CFA RID: 3322
			private readonly string correlationId;
		}

		// Token: 0x020003C1 RID: 961
		private sealed class TracingSqlDbConnection : TracingDbConnection
		{
			// Token: 0x060021C7 RID: 8647 RVA: 0x0005D6CE File Offset: 0x0005B8CE
			public TracingSqlDbConnection(IEngineHost engineHost, Tracer tracer, DbConnection connection, Action<IHostTrace> additionalCommandTraces = null, bool requireEncryption = false)
				: base(tracer, connection, additionalCommandTraces, requireEncryption)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060021C8 RID: 8648 RVA: 0x0005D6E4 File Offset: 0x0005B8E4
			protected override string GetClientConnectionId(DbConnection connection)
			{
				string text;
				if (connection.State == ConnectionState.Open && SqlEnvironment.SqlClient.TryGetClientConnectionId(DbEnvironment.GetUnwrappedConnection(connection), out text))
				{
					return text;
				}
				return "<unopened>";
			}

			// Token: 0x060021C9 RID: 8649 RVA: 0x0005D715 File Offset: 0x0005B915
			public override void Open()
			{
				base.Open();
				SqlEnvironment.SqlClient.AddInfoMessageListener(this.engineHost, DbEnvironment.GetUnwrappedConnection(base.InnerConnection), this.GetClientConnectionId(base.InnerConnection));
			}

			// Token: 0x04000CFB RID: 3323
			private readonly IEngineHost engineHost;
		}

		// Token: 0x020003C2 RID: 962
		private sealed class RetryableConnection : DelegatingDbConnection
		{
			// Token: 0x060021CA RID: 8650 RVA: 0x0005D744 File Offset: 0x0005B944
			public RetryableConnection(SqlEnvironment environment, DbConnection connection, bool hasRefreshableCredential)
				: base(connection)
			{
				this.environment = environment;
				this.hasRefreshableCredential = hasRefreshableCredential;
			}

			// Token: 0x060021CB RID: 8651 RVA: 0x0005D75C File Offset: 0x0005B95C
			public override void Open()
			{
				try
				{
					base.Open();
				}
				catch (DbException ex) when (this.ShouldRetryOpen(ex))
				{
					using (IHostTrace hostTrace = this.environment.Tracer.CreateTrace("RetryableConnection/Open", TraceEventType.Information))
					{
						this.environment.TraceException(hostTrace, ex);
					}
					base.Open();
				}
			}

			// Token: 0x060021CC RID: 8652 RVA: 0x0005D7E0 File Offset: 0x0005B9E0
			private bool ShouldRetryOpen(DbException exception)
			{
				if (this.hasRefreshableCredential && this.environment.IsInvalidCredentials(exception))
				{
					return this.TryRefreshAadCredentials();
				}
				if (this.environment.IsRetryableConnectionFailure(exception))
				{
					SqlEnvironment.SqlClient.ClearPool(DbEnvironment.GetUnwrappedConnection(base.InnerConnection));
				}
				return false;
			}

			// Token: 0x060021CD RID: 8653 RVA: 0x0005D830 File Offset: 0x0005BA30
			private bool TryRefreshAadCredentials()
			{
				OAuthCredential aadCredential = this.environment.aadCredential;
				if (string.IsNullOrEmpty((aadCredential != null) ? aadCredential.RefreshToken : null))
				{
					return false;
				}
				string accessToken = aadCredential.AccessToken;
				bool flag2;
				using (IHostTrace hostTrace = this.environment.Tracer.CreateTrace("RetryableConnection/Aad/TryRefreshCredential", TraceEventType.Information))
				{
					try
					{
						this.environment.aadCredential = aadCredential.RefreshTokenAsNeeded(this.environment.Host, this.environment.Resource, true);
						bool flag = accessToken != this.environment.aadCredential.AccessToken;
						hostTrace.Add("RefreshSuccessful", flag, false);
						if (flag)
						{
							string text = SqlEnvironment.accessTokenCache.Intern(this.environment.aadCredential.AccessTokenForResource(null));
							SqlEnvironment.SqlClient.SetAccessToken(DbEnvironment.GetUnwrappedConnection(base.InnerConnection), text);
						}
						flag2 = flag;
					}
					catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
					{
						hostTrace.Add("RefreshSuccessful", false, false);
						hostTrace.Add(ex, true);
						flag2 = false;
					}
				}
				return flag2;
			}

			// Token: 0x04000CFC RID: 3324
			private readonly SqlEnvironment environment;

			// Token: 0x04000CFD RID: 3325
			private readonly bool hasRefreshableCredential;
		}

		// Token: 0x020003C3 RID: 963
		private sealed class ConnectionPoolScope : IDisposable
		{
			// Token: 0x060021CE RID: 8654 RVA: 0x0005D96C File Offset: 0x0005BB6C
			public static void Register(ILifetimeService lifetimeService, DbConnection connection)
			{
				SqlEnvironment.ConnectionPoolScope connectionPoolScope = new SqlEnvironment.ConnectionPoolScope(lifetimeService, connection);
				lifetimeService.Register(connectionPoolScope);
			}

			// Token: 0x060021CF RID: 8655 RVA: 0x0005D988 File Offset: 0x0005BB88
			private ConnectionPoolScope(ILifetimeService lifetimeService, DbConnection connection)
			{
				this.lifetimeService = lifetimeService;
				this.connection = DbEnvironment.GetUnwrappedConnection(connection);
			}

			// Token: 0x060021D0 RID: 8656 RVA: 0x0005D9A3 File Offset: 0x0005BBA3
			public void Dispose()
			{
				if (this.connection != null)
				{
					SqlEnvironment.SqlClient.ClearPool(this.connection);
					this.lifetimeService.Unregister(this);
					this.connection = null;
				}
			}

			// Token: 0x04000CFE RID: 3326
			private readonly ILifetimeService lifetimeService;

			// Token: 0x04000CFF RID: 3327
			private DbConnection connection;
		}

		// Token: 0x020003C4 RID: 964
		internal sealed class AccessTokenCache
		{
			// Token: 0x060021D1 RID: 8657 RVA: 0x0005D9D0 File Offset: 0x0005BBD0
			public AccessTokenCache()
				: this(TimeSpan.FromMinutes(10.0))
			{
			}

			// Token: 0x060021D2 RID: 8658 RVA: 0x0005D9E6 File Offset: 0x0005BBE6
			public AccessTokenCache(TimeSpan maxTokenAge)
			{
				this.cache = new LruCache<string, KeyValuePair<string, DateTime>>(new Func<bool>(this.TrimOldTokens), null);
				this.maxTokenAge = maxTokenAge;
			}

			// Token: 0x17000E61 RID: 3681
			// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0005DA0D File Offset: 0x0005BC0D
			public int Count
			{
				get
				{
					return this.cache.Count;
				}
			}

			// Token: 0x060021D4 RID: 8660 RVA: 0x0005DA1C File Offset: 0x0005BC1C
			public string Intern(string accessToken)
			{
				KeyValuePair<string, DateTime> keyValuePair;
				if (this.cache.TryGetValue(accessToken, out keyValuePair))
				{
					return keyValuePair.Key;
				}
				this.cache.Add(accessToken, new KeyValuePair<string, DateTime>(accessToken, DateTime.UtcNow));
				return accessToken;
			}

			// Token: 0x060021D5 RID: 8661 RVA: 0x0005DA5C File Offset: 0x0005BC5C
			private bool TrimOldTokens()
			{
				KeyValuePair<string, KeyValuePair<string, DateTime>>? oldest = this.cache.Oldest;
				return oldest != null && oldest.Value.Value.Value + this.maxTokenAge < DateTime.UtcNow;
			}

			// Token: 0x04000D00 RID: 3328
			private readonly LruCache<string, KeyValuePair<string, DateTime>> cache;

			// Token: 0x04000D01 RID: 3329
			private readonly TimeSpan maxTokenAge;
		}

		// Token: 0x020003C5 RID: 965
		protected class SqlServerMetadata : DbEnvironment.DbServerMetadata
		{
			// Token: 0x17000E62 RID: 3682
			// (get) Token: 0x060021D6 RID: 8662 RVA: 0x0005DAAC File Offset: 0x0005BCAC
			// (set) Token: 0x060021D7 RID: 8663 RVA: 0x0005DAB4 File Offset: 0x0005BCB4
			public int? EngineEdition { get; set; }

			// Token: 0x17000E63 RID: 3683
			// (get) Token: 0x060021D8 RID: 8664 RVA: 0x0005DABD File Offset: 0x0005BCBD
			// (set) Token: 0x060021D9 RID: 8665 RVA: 0x0005DAC5 File Offset: 0x0005BCC5
			public bool IsSaas { get; set; }

			// Token: 0x060021DA RID: 8666 RVA: 0x0005DACE File Offset: 0x0005BCCE
			protected override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteNullableInt(this.EngineEdition);
				writer.WriteBool(this.IsSaas);
			}

			// Token: 0x060021DB RID: 8667 RVA: 0x0005DAEF File Offset: 0x0005BCEF
			protected override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.EngineEdition = reader.ReadNullableInt();
				this.IsSaas = reader.ReadBool();
			}
		}
	}
}
