using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Sql
{
	// Token: 0x020003D7 RID: 983
	internal class SystemSqlClient : ISqlClient
	{
		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x0005E98B File Offset: 0x0005CB8B
		public DbProviderFactory ProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x0005E992 File Offset: 0x0005CB92
		public string ProviderName
		{
			get
			{
				return "System.Data.SqlClient";
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x0005E999 File Offset: 0x0005CB99
		public bool SupportsAad
		{
			get
			{
				return this.accessTokenProperty != null;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x0005E9A7 File Offset: 0x0005CBA7
		public bool SupportsMultiSubnetFailover
		{
			get
			{
				return this.supportsMultiSubnetFailover;
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0005E9AF File Offset: 0x0005CBAF
		public void ClearAllPools()
		{
			SqlConnection.ClearAllPools();
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0005E9B6 File Offset: 0x0005CBB6
		public void ClearPool(DbConnection dbConnection)
		{
			SqlConnection sqlConnection = dbConnection as SqlConnection;
			if (sqlConnection == null)
			{
				throw new InvalidOperationException();
			}
			SqlConnection.ClearPool(sqlConnection);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0005E9CC File Offset: 0x0005CBCC
		public ISqlBulkCopy CreateBulkCopy(DbConnection connection, bool keepIdentity, DbTransaction transaction)
		{
			return new SystemSqlClient.SqlBulkCopyWrapper(connection, keepIdentity, transaction);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000020FA File Offset: 0x000002FA
		public SqlClassification[][] GetClassifications(DbDataReader reader)
		{
			return null;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0005E9D6 File Offset: 0x0005CBD6
		public void SetAccessToken(DbConnection connection, string accessToken)
		{
			if (!(connection is SqlConnection) || !this.SupportsAad)
			{
				throw new InvalidOperationException();
			}
			this.accessTokenProperty.SetValue(connection, accessToken, null);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x0005E9FC File Offset: 0x0005CBFC
		public bool TryGetErrorInfo(DbException exception, bool wasEncrypted, out SqlErrorInfo errorInfo)
		{
			SqlException ex = exception as SqlException;
			if (ex != null)
			{
				errorInfo = this.CreateSqlErrorInfo(ex, wasEncrypted);
				return true;
			}
			errorInfo = default(SqlErrorInfo);
			return false;
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0005EA2C File Offset: 0x0005CC2C
		public bool InitializeAadDiscovery()
		{
			Assembly assembly = typeof(SqlConnection).Assembly;
			Type type = assembly.GetType("System.Data.SqlClient.SqlAuthenticationProvider");
			Type type2 = assembly.GetType("System.Data.SqlClient.SqlAuthenticationMethod");
			Type type3 = assembly.GetType("System.Data.SqlClient.SqlAuthenticationParameters");
			Type type4 = assembly.GetType("System.Data.SqlClient.SqlAuthenticationToken");
			if (type == null || type2 == null || type3 == null || type4 == null)
			{
				return false;
			}
			Assembly assembly2 = typeof(string).Assembly;
			Type type5 = assembly2.GetType("System.Threading.Tasks.TaskCompletionSource`1");
			Type type6 = assembly2.GetType("System.Threading.Tasks.Task`1");
			if (type5 == null || type6 == null)
			{
				return false;
			}
			MethodInfo method = type.GetMethod("SetProvider", new Type[] { type2, type });
			if (method == null)
			{
				return false;
			}
			AssemblyName assemblyName = new AssemblyName("SqlClientInterop");
			TypeBuilder typeBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run).DefineDynamicModule("SqlClientInterop").DefineType("MashupAuthenticationProvider", TypeAttributes.Public, type);
			ILGenerator ilgenerator = typeBuilder.DefineMethod("IsSupported", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(bool), new Type[] { type2 }).GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldc_I4_1);
			ilgenerator.Emit(OpCodes.Ret);
			Type type7 = type5.MakeGenericType(new Type[] { type4 });
			ilgenerator = typeBuilder.DefineMethod("AcquireTokenAsync", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, type6.MakeGenericType(new Type[] { type4 }), new Type[] { type3 }).GetILGenerator();
			ilgenerator.DeclareLocal(type7);
			ilgenerator.DeclareLocal(typeof(SqlAadAuthException));
			ilgenerator.Emit(OpCodes.Newobj, type7.GetConstructor(Type.EmptyTypes));
			ilgenerator.Emit(OpCodes.Stloc_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Call, type3.GetProperty("Authority").GetGetMethod());
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Call, type3.GetProperty("Resource").GetGetMethod());
			ilgenerator.Emit(OpCodes.Newobj, typeof(SqlAadAuthException).GetConstructor(new Type[]
			{
				typeof(string),
				typeof(string)
			}));
			ilgenerator.Emit(OpCodes.Stloc_1);
			ilgenerator.Emit(OpCodes.Ldloc_0);
			ilgenerator.Emit(OpCodes.Ldloc_1);
			ilgenerator.Emit(OpCodes.Call, type7.GetMethod("SetException", new Type[] { typeof(Exception) }));
			ilgenerator.Emit(OpCodes.Ldloc_0);
			ilgenerator.Emit(OpCodes.Call, type7.GetProperty("Task").GetGetMethod());
			ilgenerator.Emit(OpCodes.Ret);
			object obj = typeBuilder.CreateType().GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
			object obj2 = method.Invoke(null, new object[] { 4, obj });
			return obj2 is bool && (bool)obj2;
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0005ED5C File Offset: 0x0005CF5C
		void ISqlClient.AddInfoMessageListener(IEngineHost engineHost, DbConnection connection, string connectionId)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.InfoMessage += delegate(object sender, SqlInfoMessageEventArgs eventArgs)
				{
					SqlClientHelper.TraceSqlInfoMessage(engineHost, eventArgs.Message, connectionId);
				};
			}
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0005ED99 File Offset: 0x0005CF99
		bool ISqlClient.TryGetClientConnectionId(DbConnection connection, out string clientConnectionId)
		{
			if ((connection is SqlConnection) & (this.clientConnectionIdProperty != null))
			{
				clientConnectionId = this.clientConnectionIdProperty.GetValue(connection, null).ToString();
				return true;
			}
			clientConnectionId = null;
			return false;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0005EDD0 File Offset: 0x0005CFD0
		void ISqlClient.TraceClientConnectionId(IHostTrace trace, DbException exception)
		{
			if ((exception is SqlException) & (this.clientConnectionIdPropertyOnException != null))
			{
				string text = this.clientConnectionIdPropertyOnException.GetValue(exception, null).ToString();
				trace.Add("ConnectionId", text, false);
			}
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0005EE18 File Offset: 0x0005D018
		void ISqlClient.TraceRequestIds(IHostTrace trace, DbException exception)
		{
			if ((exception is SqlException) & (this.sqlExceptionErrors != null))
			{
				foreach (object obj in (this.sqlExceptionErrors.GetValue(exception, null) as SqlErrorCollection))
				{
					SqlError sqlError = (SqlError)obj;
					SqlClientHelper.GenerateRequestIdTrace(trace, sqlError.Message);
				}
			}
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x0005EE9C File Offset: 0x0005D09C
		private SqlErrorInfo CreateSqlErrorInfo(SqlException sqlException, bool wasEncrypted)
		{
			return new SqlErrorInfo
			{
				Class = sqlException.Class,
				Number = sqlException.Number,
				State = sqlException.State
			};
		}

		// Token: 0x04000D40 RID: 3392
		private const int activeDirectoryInteractive = 4;

		// Token: 0x04000D41 RID: 3393
		private readonly PropertyInfo accessTokenProperty = typeof(SqlConnection).GetProperty("AccessToken");

		// Token: 0x04000D42 RID: 3394
		private readonly PropertyInfo clientConnectionIdProperty = typeof(SqlConnection).GetProperty("ClientConnectionId");

		// Token: 0x04000D43 RID: 3395
		private readonly PropertyInfo clientConnectionIdPropertyOnException = typeof(SqlException).GetProperty("ClientConnectionId");

		// Token: 0x04000D44 RID: 3396
		private readonly PropertyInfo sqlExceptionErrors = typeof(SqlException).GetProperty("Errors");

		// Token: 0x04000D45 RID: 3397
		private bool supportsMultiSubnetFailover = typeof(SqlConnectionStringBuilder).GetProperty("MultiSubnetFailover") != null;

		// Token: 0x020003D8 RID: 984
		private sealed class SqlBulkCopyWrapper : ISqlBulkCopy, IDisposable
		{
			// Token: 0x06002237 RID: 8759 RVA: 0x0005EF78 File Offset: 0x0005D178
			public SqlBulkCopyWrapper(DbConnection connection, bool keepIdentity, DbTransaction transaction)
			{
				this.syncRoot = new object();
				SqlBulkCopyOptions sqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.FireTriggers;
				if (keepIdentity)
				{
					sqlBulkCopyOptions |= SqlBulkCopyOptions.KeepIdentity;
				}
				this.bulkCopy = new SqlBulkCopy((SqlConnection)connection, sqlBulkCopyOptions, (SqlTransaction)transaction);
				this.bulkCopy.SqlRowsCopied += this.SqlRowsCopiedCallback;
			}

			// Token: 0x17000E74 RID: 3700
			// (get) Token: 0x06002238 RID: 8760 RVA: 0x0005EFCF File Offset: 0x0005D1CF
			// (set) Token: 0x06002239 RID: 8761 RVA: 0x0005EFDC File Offset: 0x0005D1DC
			public int BatchSize
			{
				get
				{
					return this.bulkCopy.BatchSize;
				}
				set
				{
					this.bulkCopy.BatchSize = value;
				}
			}

			// Token: 0x17000E75 RID: 3701
			// (get) Token: 0x0600223A RID: 8762 RVA: 0x0005EFEA File Offset: 0x0005D1EA
			// (set) Token: 0x0600223B RID: 8763 RVA: 0x0005EFF7 File Offset: 0x0005D1F7
			public int BulkCopyTimeout
			{
				get
				{
					return this.bulkCopy.BulkCopyTimeout;
				}
				set
				{
					this.bulkCopy.BulkCopyTimeout = value;
				}
			}

			// Token: 0x17000E76 RID: 3702
			// (get) Token: 0x0600223C RID: 8764 RVA: 0x0005F005 File Offset: 0x0005D205
			// (set) Token: 0x0600223D RID: 8765 RVA: 0x0005F012 File Offset: 0x0005D212
			public string DestinationTableName
			{
				get
				{
					return this.bulkCopy.DestinationTableName;
				}
				set
				{
					this.bulkCopy.DestinationTableName = value;
				}
			}

			// Token: 0x17000E77 RID: 3703
			// (get) Token: 0x0600223E RID: 8766 RVA: 0x0005F020 File Offset: 0x0005D220
			// (set) Token: 0x0600223F RID: 8767 RVA: 0x0005F02D File Offset: 0x0005D22D
			public int NotifyAfter
			{
				get
				{
					return this.bulkCopy.NotifyAfter;
				}
				set
				{
					this.bulkCopy.NotifyAfter = value;
				}
			}

			// Token: 0x17000E78 RID: 3704
			// (get) Token: 0x06002240 RID: 8768 RVA: 0x0005F03B File Offset: 0x0005D23B
			// (set) Token: 0x06002241 RID: 8769 RVA: 0x0005F043 File Offset: 0x0005D243
			public Action<long> RowsCopied
			{
				get
				{
					return this.rowsCopiedCallback;
				}
				set
				{
					this.rowsCopiedCallback = value;
				}
			}

			// Token: 0x06002242 RID: 8770 RVA: 0x0005F04C File Offset: 0x0005D24C
			public void AddColumnMapping(string key, string value)
			{
				this.bulkCopy.ColumnMappings.Add(key, value);
			}

			// Token: 0x06002243 RID: 8771 RVA: 0x0005F061 File Offset: 0x0005D261
			public void Dispose()
			{
				((IDisposable)this.bulkCopy).Dispose();
			}

			// Token: 0x06002244 RID: 8772 RVA: 0x0005F06E File Offset: 0x0005D26E
			public void WriteToServer(IDataReader reader)
			{
				this.bulkCopy.WriteToServer(reader);
			}

			// Token: 0x06002245 RID: 8773 RVA: 0x0005F07C File Offset: 0x0005D27C
			private void SqlRowsCopiedCallback(object sender, SqlRowsCopiedEventArgs eventArgs)
			{
				Action<long> action = this.rowsCopiedCallback;
				if (action == null)
				{
					return;
				}
				action(eventArgs.RowsCopied);
			}

			// Token: 0x04000D46 RID: 3398
			private readonly object syncRoot;

			// Token: 0x04000D47 RID: 3399
			private readonly SqlBulkCopy bulkCopy;

			// Token: 0x04000D48 RID: 3400
			private Action<long> rowsCopiedCallback;
		}
	}
}
