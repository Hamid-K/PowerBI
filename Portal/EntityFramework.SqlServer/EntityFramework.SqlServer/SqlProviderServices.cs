using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.SqlGen;
using System.Data.Entity.SqlServer.Utilities;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000010 RID: 16
	public sealed class SqlProviderServices : DbProviderServices
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00005098 File Offset: 0x00003298
		private SqlProviderServices()
		{
			base.AddDependencyResolver(new SingletonDependencyResolver<IDbConnectionFactory>(new LocalDbConnectionFactory()));
			base.AddDependencyResolver(new ExecutionStrategyResolver<DefaultSqlExecutionStrategy>("System.Data.SqlClient", null, () => new DefaultSqlExecutionStrategy()));
			base.AddDependencyResolver(new SingletonDependencyResolver<Func<MigrationSqlGenerator>>(() => new SqlServerMigrationSqlGenerator(), "System.Data.SqlClient"));
			base.AddDependencyResolver(new SingletonDependencyResolver<TableExistenceChecker>(new SqlTableExistenceChecker(), "System.Data.SqlClient"));
			base.AddDependencyResolver(new SingletonDependencyResolver<DbSpatialServices>(SqlSpatialServices.Instance, delegate(object k)
			{
				if (k == null)
				{
					return true;
				}
				DbProviderInfo dbProviderInfo = k as DbProviderInfo;
				return dbProviderInfo != null && dbProviderInfo.ProviderInvariantName == "System.Data.SqlClient" && SqlProviderServices.SupportsSpatial(dbProviderInfo.ProviderManifestToken);
			}));
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005169 File Offset: 0x00003369
		public static SqlProviderServices Instance
		{
			get
			{
				return SqlProviderServices._providerInstance;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005170 File Offset: 0x00003370
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00005177 File Offset: 0x00003377
		public static string SqlServerTypesAssemblyName { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000517F File Offset: 0x0000337F
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00005186 File Offset: 0x00003386
		public static bool TruncateDecimalsToScale
		{
			get
			{
				return SqlProviderServices._truncateDecimalsToScale;
			}
			set
			{
				SqlProviderServices._truncateDecimalsToScale = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000518E File Offset: 0x0000338E
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00005195 File Offset: 0x00003395
		public static bool UseScopeIdentity
		{
			get
			{
				return SqlProviderServices._useScopeIdentity;
			}
			set
			{
				SqlProviderServices._useScopeIdentity = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000519D File Offset: 0x0000339D
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000051A4 File Offset: 0x000033A4
		public static bool UseRowNumberOrderingInOffsetQueries
		{
			get
			{
				return SqlProviderServices._useRowNumberOrderingInOffsetQueries;
			}
			set
			{
				SqlProviderServices._useRowNumberOrderingInOffsetQueries = value;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000051AC File Offset: 0x000033AC
		public override void RegisterInfoMessageHandler(DbConnection connection, Action<string> handler)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<Action<string>>(handler, "handler");
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection == null)
			{
				throw new ArgumentException(Strings.Mapping_Provider_WrongConnectionType(typeof(SqlConnection)));
			}
			sqlConnection.InfoMessage += delegate(object _, SqlInfoMessageEventArgs e)
			{
				if (!string.IsNullOrWhiteSpace(e.Message))
				{
					handler(e.Message);
				}
			};
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005214 File Offset: 0x00003414
		protected override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			Check.NotNull<DbProviderManifest>(providerManifest, "providerManifest");
			Check.NotNull<DbCommandTree>(commandTree, "commandTree");
			DbCommand dbCommand = SqlProviderServices.CreateCommand(providerManifest, commandTree);
			return this.CreateCommandDefinition(dbCommand);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005248 File Offset: 0x00003448
		protected override DbCommand CloneDbCommand(DbCommand fromDbCommand)
		{
			Check.NotNull<DbCommand>(fromDbCommand, "fromDbCommand");
			SqlCommand sqlCommand = fromDbCommand as SqlCommand;
			if (sqlCommand == null)
			{
				return base.CloneDbCommand(fromDbCommand);
			}
			SqlCommand sqlCommand2 = new SqlCommand();
			sqlCommand2.CommandText = sqlCommand.CommandText;
			sqlCommand2.CommandTimeout = sqlCommand.CommandTimeout;
			sqlCommand2.CommandType = sqlCommand.CommandType;
			sqlCommand2.Connection = sqlCommand.Connection;
			sqlCommand2.Transaction = sqlCommand.Transaction;
			sqlCommand2.UpdatedRowSource = sqlCommand.UpdatedRowSource;
			foreach (object obj in sqlCommand.Parameters)
			{
				ICloneable cloneable = obj as ICloneable;
				sqlCommand2.Parameters.Add((cloneable == null) ? obj : cloneable.Clone());
			}
			return sqlCommand2;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005328 File Offset: 0x00003528
		private static DbCommand CreateCommand(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			SqlProviderManifest sqlProviderManifest = providerManifest as SqlProviderManifest;
			if (sqlProviderManifest == null)
			{
				throw new ArgumentException(Strings.Mapping_Provider_WrongManifestType(typeof(SqlProviderManifest)));
			}
			SqlVersion sqlVersion = sqlProviderManifest.SqlVersion;
			SqlCommand sqlCommand = new SqlCommand();
			List<SqlParameter> list;
			CommandType commandType;
			HashSet<string> hashSet;
			sqlCommand.CommandText = SqlGenerator.GenerateSql(commandTree, sqlVersion, out list, out commandType, out hashSet);
			sqlCommand.CommandType = commandType;
			EdmFunction edmFunction = null;
			if (commandTree.CommandTreeKind == DbCommandTreeKind.Function)
			{
				edmFunction = ((DbFunctionCommandTree)commandTree).EdmFunction;
			}
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in commandTree.Parameters)
			{
				FunctionParameter functionParameter;
				SqlParameter sqlParameter;
				if (edmFunction != null && edmFunction.Parameters.TryGetValue(keyValuePair.Key, false, out functionParameter))
				{
					sqlParameter = SqlProviderServices.CreateSqlParameter(functionParameter.Name, functionParameter.TypeUsage, functionParameter.Mode, DBNull.Value, false, sqlVersion);
				}
				else
				{
					TypeUsage typeUsage = ((hashSet != null && hashSet.Contains(keyValuePair.Key)) ? keyValuePair.Value.ForceNonUnicode() : keyValuePair.Value);
					sqlParameter = SqlProviderServices.CreateSqlParameter(keyValuePair.Key, typeUsage, ParameterMode.In, DBNull.Value, false, sqlVersion);
				}
				sqlCommand.Parameters.Add(sqlParameter);
			}
			if (list != null && 0 < list.Count)
			{
				if (commandTree.CommandTreeKind != DbCommandTreeKind.Delete && commandTree.CommandTreeKind != DbCommandTreeKind.Insert && commandTree.CommandTreeKind != DbCommandTreeKind.Update)
				{
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1017));
				}
				foreach (SqlParameter sqlParameter2 in list)
				{
					sqlCommand.Parameters.Add(sqlParameter2);
				}
			}
			return sqlCommand;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000054EC File Offset: 0x000036EC
		protected override void SetDbParameterValue(DbParameter parameter, TypeUsage parameterType, object value)
		{
			Check.NotNull<DbParameter>(parameter, "parameter");
			Check.NotNull<TypeUsage>(parameterType, "parameterType");
			value = SqlProviderServices.EnsureSqlParameterValue(value);
			if (parameterType.IsPrimitiveType(PrimitiveTypeKind.String) || parameterType.IsPrimitiveType(PrimitiveTypeKind.Binary))
			{
				if (SqlProviderServices.GetParameterSize(parameterType, (parameter.Direction & ParameterDirection.Output) == ParameterDirection.Output) != null)
				{
					parameter.Value = value;
					return;
				}
				int size = parameter.Size;
				parameter.Size = 0;
				parameter.Value = value;
				if (size > -1)
				{
					if (parameter.Size < size)
					{
						parameter.Size = size;
						return;
					}
				}
				else
				{
					int nonMaxLength = SqlProviderServices.GetNonMaxLength(((SqlParameter)parameter).SqlDbType);
					if (parameter.Size < nonMaxLength)
					{
						parameter.Size = nonMaxLength;
						return;
					}
					if (parameter.Size > nonMaxLength)
					{
						parameter.Size = -1;
						return;
					}
				}
			}
			else
			{
				parameter.Value = value;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000055B8 File Offset: 0x000037B8
		protected override string GetDbProviderManifestToken(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			if (string.IsNullOrEmpty(DbInterception.Dispatch.Connection.GetConnectionString(connection, new DbInterceptionContext())))
			{
				throw new ArgumentException(Strings.UnableToDetermineStoreVersion);
			}
			string providerManifestToken = null;
			try
			{
				SqlProviderServices.UsingConnection(connection, delegate(DbConnection conn)
				{
					providerManifestToken = SqlProviderServices.QueryForManifestToken(conn);
				});
				return providerManifestToken;
			}
			catch
			{
			}
			try
			{
				this.UsingMasterConnection(connection, delegate(DbConnection conn)
				{
					providerManifestToken = SqlProviderServices.QueryForManifestToken(conn);
				});
				return providerManifestToken;
			}
			catch
			{
			}
			return "2008";
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005668 File Offset: 0x00003868
		private static string QueryForManifestToken(DbConnection conn)
		{
			SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
			ServerType serverType = ((sqlVersion >= SqlVersion.Sql11) ? SqlVersionUtils.GetServerType(conn) : ServerType.OnPremises);
			return SqlVersionUtils.GetVersionHint(sqlVersion, serverType);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005690 File Offset: 0x00003890
		protected override DbProviderManifest GetDbProviderManifest(string versionHint)
		{
			if (string.IsNullOrEmpty(versionHint))
			{
				throw new ArgumentException(Strings.UnableToDetermineStoreVersion);
			}
			return this._providerManifests.GetOrAdd(versionHint, (string s) => new SqlProviderManifest(s));
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000056D0 File Offset: 0x000038D0
		protected override DbSpatialDataReader GetDbSpatialDataReader(DbDataReader fromReader, string versionHint)
		{
			SqlDataReader sqlDataReader = fromReader as SqlDataReader;
			if (sqlDataReader == null)
			{
				throw new ProviderIncompatibleException(Strings.SqlProvider_NeedSqlDataReader(fromReader.GetType()));
			}
			if (!SqlProviderServices.SupportsSpatial(versionHint))
			{
				return null;
			}
			return new SqlSpatialDataReader(base.GetSpatialServices(new DbProviderInfo("System.Data.SqlClient", versionHint)), new SqlDataReaderWrapper(sqlDataReader));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000571E File Offset: 0x0000391E
		[Obsolete("Return DbSpatialServices from the GetService method. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.")]
		protected override DbSpatialServices DbGetSpatialServices(string versionHint)
		{
			if (!SqlProviderServices.SupportsSpatial(versionHint))
			{
				return null;
			}
			return SqlSpatialServices.Instance;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000572F File Offset: 0x0000392F
		private static bool SupportsSpatial(string versionHint)
		{
			if (string.IsNullOrEmpty(versionHint))
			{
				throw new ArgumentException(Strings.UnableToDetermineStoreVersion);
			}
			return SqlVersionUtils.GetSqlVersion(versionHint) >= SqlVersion.Sql10;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005754 File Offset: 0x00003954
		internal static SqlParameter CreateSqlParameter(string name, TypeUsage type, ParameterMode mode, object value, bool preventTruncation, SqlVersion version)
		{
			value = SqlProviderServices.EnsureSqlParameterValue(value);
			SqlParameter sqlParameter = new SqlParameter(name, value);
			ParameterDirection parameterDirection = SqlProviderServices.ParameterModeToParameterDirection(mode);
			if (sqlParameter.Direction != parameterDirection)
			{
				sqlParameter.Direction = parameterDirection;
			}
			bool flag = mode > ParameterMode.In;
			int? num;
			byte? b;
			byte? b2;
			string text;
			SqlDbType sqlDbType = SqlProviderServices.GetSqlDbType(type, flag, version, out num, out b, out b2, out text);
			if (sqlParameter.SqlDbType != sqlDbType)
			{
				sqlParameter.SqlDbType = sqlDbType;
			}
			if (sqlDbType == SqlDbType.Udt)
			{
				sqlParameter.UdtTypeName = text;
			}
			if (num != null)
			{
				if (flag || sqlParameter.Size != num.Value)
				{
					if (preventTruncation && num.Value != -1)
					{
						sqlParameter.Size = Math.Max(sqlParameter.Size, num.Value);
					}
					else
					{
						sqlParameter.Size = num.Value;
					}
				}
			}
			else
			{
				PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)type.EdmType).PrimitiveTypeKind;
				if (primitiveTypeKind == PrimitiveTypeKind.String)
				{
					sqlParameter.Size = SqlProviderServices.GetDefaultStringMaxLength(version, sqlDbType);
				}
				else if (primitiveTypeKind == PrimitiveTypeKind.Binary)
				{
					sqlParameter.Size = SqlProviderServices.GetDefaultBinaryMaxLength(version);
				}
			}
			if (b != null && (flag || (sqlParameter.Precision != b.Value && SqlProviderServices._truncateDecimalsToScale)))
			{
				sqlParameter.Precision = b.Value;
			}
			if (b2 != null && (flag || (sqlParameter.Scale != b2.Value && SqlProviderServices._truncateDecimalsToScale)))
			{
				sqlParameter.Scale = b2.Value;
			}
			bool flag2 = type.IsNullable();
			if (flag || flag2 != sqlParameter.IsNullable)
			{
				sqlParameter.IsNullable = flag2;
			}
			return sqlParameter;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000058E3 File Offset: 0x00003AE3
		private static ParameterDirection ParameterModeToParameterDirection(ParameterMode mode)
		{
			switch (mode)
			{
			case ParameterMode.In:
				return ParameterDirection.Input;
			case ParameterMode.Out:
				return ParameterDirection.Output;
			case ParameterMode.InOut:
				return ParameterDirection.InputOutput;
			case ParameterMode.ReturnValue:
				return ParameterDirection.ReturnValue;
			default:
				return (ParameterDirection)0;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005908 File Offset: 0x00003B08
		internal static object EnsureSqlParameterValue(object value)
		{
			if (value != null && value != DBNull.Value && value.GetType().IsClass())
			{
				DbGeography dbGeography = value as DbGeography;
				if (dbGeography != null)
				{
					value = SqlTypesAssemblyLoader.DefaultInstance.GetSqlTypesAssembly().ConvertToSqlTypesGeography(dbGeography);
				}
				else
				{
					DbGeometry dbGeometry = value as DbGeometry;
					if (dbGeometry != null)
					{
						value = SqlTypesAssemblyLoader.DefaultInstance.GetSqlTypesAssembly().ConvertToSqlTypesGeometry(dbGeometry);
					}
					else
					{
						HierarchyId hierarchyId = value as HierarchyId;
						if (hierarchyId != null)
						{
							value = SqlTypesAssemblyLoader.DefaultInstance.GetSqlTypesAssembly().ConvertToSqlTypesHierarchyId(hierarchyId);
						}
					}
				}
			}
			return value;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000598C File Offset: 0x00003B8C
		private static SqlDbType GetSqlDbType(TypeUsage type, bool isOutParam, SqlVersion version, out int? size, out byte? precision, out byte? scale, out string udtName)
		{
			PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)type.EdmType).PrimitiveTypeKind;
			size = null;
			precision = null;
			scale = null;
			udtName = null;
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				size = SqlProviderServices.GetParameterSize(type, isOutParam);
				return SqlProviderServices.GetBinaryDbType(type);
			case PrimitiveTypeKind.Boolean:
				return SqlDbType.Bit;
			case PrimitiveTypeKind.Byte:
				return SqlDbType.TinyInt;
			case PrimitiveTypeKind.DateTime:
				if (!SqlVersionUtils.IsPreKatmai(version))
				{
					precision = SqlProviderServices.GetKatmaiDateTimePrecision(type, isOutParam);
					return SqlDbType.DateTime2;
				}
				return SqlDbType.DateTime;
			case PrimitiveTypeKind.Decimal:
				precision = SqlProviderServices.GetParameterPrecision(type, null);
				scale = SqlProviderServices.GetScale(type);
				return SqlDbType.Decimal;
			case PrimitiveTypeKind.Double:
				return SqlDbType.Float;
			case PrimitiveTypeKind.Guid:
				return SqlDbType.UniqueIdentifier;
			case PrimitiveTypeKind.Single:
				return SqlDbType.Real;
			case PrimitiveTypeKind.SByte:
				return SqlDbType.SmallInt;
			case PrimitiveTypeKind.Int16:
				return SqlDbType.SmallInt;
			case PrimitiveTypeKind.Int32:
				return SqlDbType.Int;
			case PrimitiveTypeKind.Int64:
				return SqlDbType.BigInt;
			case PrimitiveTypeKind.String:
				size = SqlProviderServices.GetParameterSize(type, isOutParam);
				return SqlProviderServices.GetStringDbType(type);
			case PrimitiveTypeKind.Time:
				if (!SqlVersionUtils.IsPreKatmai(version))
				{
					precision = SqlProviderServices.GetKatmaiDateTimePrecision(type, isOutParam);
				}
				return SqlDbType.Time;
			case PrimitiveTypeKind.DateTimeOffset:
				if (!SqlVersionUtils.IsPreKatmai(version))
				{
					precision = SqlProviderServices.GetKatmaiDateTimePrecision(type, isOutParam);
				}
				return SqlDbType.DateTimeOffset;
			case PrimitiveTypeKind.Geometry:
				udtName = "geometry";
				return SqlDbType.Udt;
			case PrimitiveTypeKind.Geography:
				udtName = "geography";
				return SqlDbType.Udt;
			case PrimitiveTypeKind.HierarchyId:
				udtName = "hierarchyid";
				return SqlDbType.Udt;
			}
			return SqlDbType.Variant;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005B24 File Offset: 0x00003D24
		private static int? GetParameterSize(TypeUsage type, bool isOutParam)
		{
			Facet facet;
			if (type.Facets.TryGetValue("MaxLength", false, out facet) && facet.Value != null)
			{
				if (facet.IsUnbounded)
				{
					return new int?(-1);
				}
				return (int?)facet.Value;
			}
			else
			{
				if (isOutParam)
				{
					return new int?(-1);
				}
				return null;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005B7C File Offset: 0x00003D7C
		private static int GetNonMaxLength(SqlDbType type)
		{
			int num = -1;
			if (type == SqlDbType.NChar || type == SqlDbType.NVarChar)
			{
				num = 4000;
			}
			else if (type == SqlDbType.Char || type == SqlDbType.VarChar || type == SqlDbType.Binary || type == SqlDbType.VarBinary)
			{
				num = 8000;
			}
			return num;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005BB8 File Offset: 0x00003DB8
		private static int GetDefaultStringMaxLength(SqlVersion version, SqlDbType type)
		{
			int num;
			if (version < SqlVersion.Sql9)
			{
				if (type == SqlDbType.NChar || type == SqlDbType.NVarChar)
				{
					num = 4000;
				}
				else
				{
					num = 8000;
				}
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005BE8 File Offset: 0x00003DE8
		private static int GetDefaultBinaryMaxLength(SqlVersion version)
		{
			int num;
			if (version < SqlVersion.Sql9)
			{
				num = 8000;
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005C08 File Offset: 0x00003E08
		private static byte? GetKatmaiDateTimePrecision(TypeUsage type, bool isOutParam)
		{
			byte? b = (isOutParam ? new byte?((byte)7) : null);
			return SqlProviderServices.GetParameterPrecision(type, b);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005C34 File Offset: 0x00003E34
		private static byte? GetParameterPrecision(TypeUsage type, byte? defaultIfUndefined)
		{
			byte b;
			if (type.TryGetPrecision(out b))
			{
				return new byte?(b);
			}
			return defaultIfUndefined;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005C54 File Offset: 0x00003E54
		private static byte? GetScale(TypeUsage type)
		{
			byte b;
			if (type.TryGetScale(out b))
			{
				return new byte?(b);
			}
			return null;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005C7C File Offset: 0x00003E7C
		private static SqlDbType GetStringDbType(TypeUsage type)
		{
			SqlDbType sqlDbType;
			if (type.EdmType.Name.ToLowerInvariant() == "xml")
			{
				sqlDbType = SqlDbType.Xml;
			}
			else
			{
				bool flag;
				if (!type.TryGetIsUnicode(out flag))
				{
					flag = true;
				}
				if (type.IsFixedLength())
				{
					sqlDbType = (flag ? SqlDbType.NChar : SqlDbType.Char);
				}
				else
				{
					sqlDbType = (flag ? SqlDbType.NVarChar : SqlDbType.VarChar);
				}
			}
			return sqlDbType;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005CD4 File Offset: 0x00003ED4
		private static SqlDbType GetBinaryDbType(TypeUsage type)
		{
			if (!type.IsFixedLength())
			{
				return SqlDbType.VarBinary;
			}
			return SqlDbType.Binary;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005CE2 File Offset: 0x00003EE2
		protected override string DbCreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<string>(providerManifestToken, "providerManifestToken");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			return SqlProviderServices.CreateObjectsScript(SqlVersionUtils.GetSqlVersion(providerManifestToken), storeItemCollection);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005D08 File Offset: 0x00003F08
		protected override void DbCreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			SqlConnection requiredSqlConnection = SqlProviderUtilities.GetRequiredSqlConnection(connection);
			string text;
			string text2;
			string text3;
			SqlProviderServices.GetOrGenerateDatabaseNameAndGetFileNames(requiredSqlConnection, out text, out text2, out text3);
			string text4 = SqlDdlBuilder.CreateDatabaseScript(text, text2, text3);
			SqlVersion sqlVersion = this.CreateDatabaseFromScript(commandTimeout, requiredSqlConnection, text4);
			try
			{
				SqlConnection.ClearPool(requiredSqlConnection);
				string setDatabaseOptionsScript = SqlDdlBuilder.SetDatabaseOptionsScript(sqlVersion, text);
				if (!string.IsNullOrEmpty(setDatabaseOptionsScript))
				{
					this.UsingMasterConnection(requiredSqlConnection, delegate(DbConnection conn)
					{
						using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, setDatabaseOptionsScript, commandTimeout))
						{
							DbInterception.Dispatch.Command.NonQuery(dbCommand, new DbCommandInterceptionContext());
						}
					});
				}
				string createObjectsScript = SqlProviderServices.CreateObjectsScript(sqlVersion, storeItemCollection);
				if (!string.IsNullOrWhiteSpace(createObjectsScript))
				{
					SqlProviderServices.UsingConnection(requiredSqlConnection, delegate(DbConnection conn)
					{
						using (DbCommand dbCommand2 = SqlProviderServices.CreateCommand(conn, createObjectsScript, commandTimeout))
						{
							DbInterception.Dispatch.Command.NonQuery(dbCommand2, new DbCommandInterceptionContext());
						}
					});
				}
			}
			catch (Exception ex)
			{
				try
				{
					this.DropDatabase(requiredSqlConnection, commandTimeout, text);
				}
				catch (Exception ex2)
				{
					throw new InvalidOperationException(Strings.SqlProvider_IncompleteCreateDatabase, new AggregateException(Strings.SqlProvider_IncompleteCreateDatabaseAggregate, new Exception[] { ex, ex2 }));
				}
				throw;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005E24 File Offset: 0x00004024
		private static void GetOrGenerateDatabaseNameAndGetFileNames(SqlConnection sqlConnection, out string databaseName, out string dataFileName, out string logFileName)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(DbInterception.Dispatch.Connection.GetConnectionString(sqlConnection, new DbInterceptionContext()));
			string attachDBFilename = sqlConnectionStringBuilder.AttachDBFilename;
			if (string.IsNullOrEmpty(attachDBFilename))
			{
				dataFileName = null;
				logFileName = null;
			}
			else
			{
				dataFileName = SqlProviderServices.GetMdfFileName(attachDBFilename);
				logFileName = SqlProviderServices.GetLdfFileName(dataFileName);
			}
			if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog))
			{
				databaseName = sqlConnectionStringBuilder.InitialCatalog;
				return;
			}
			if (dataFileName != null)
			{
				databaseName = SqlProviderServices.GenerateDatabaseName(dataFileName);
				return;
			}
			throw new InvalidOperationException(Strings.SqlProvider_DdlGeneration_MissingInitialCatalog);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005EA2 File Offset: 0x000040A2
		private static string GetLdfFileName(string dataFileName)
		{
			return Path.Combine(new FileInfo(dataFileName).Directory.FullName, Path.GetFileNameWithoutExtension(dataFileName) + "_log.ldf");
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005ECC File Offset: 0x000040CC
		private static string GenerateDatabaseName(string mdfFileName)
		{
			char[] array = Path.GetFileNameWithoutExtension(mdfFileName.ToUpper(CultureInfo.InvariantCulture)).ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (!char.IsLetterOrDigit(array[i]))
				{
					array[i] = '_';
				}
			}
			string text = new string(array);
			text = ((text.Length > 30) ? text.Substring(0, 30) : text);
			return string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
			{
				text,
				Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005F5D File Offset: 0x0000415D
		private static string GetMdfFileName(string attachDBFile)
		{
			return DbProviderServices.ExpandDataDirectory(attachDBFile);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005F68 File Offset: 0x00004168
		internal SqlVersion CreateDatabaseFromScript(int? commandTimeout, DbConnection sqlConnection, string createDatabaseScript)
		{
			SqlVersion sqlVersion = (SqlVersion)0;
			this.UsingMasterConnection(sqlConnection, delegate(DbConnection conn)
			{
				using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, createDatabaseScript, commandTimeout))
				{
					DbInterception.Dispatch.Command.NonQuery(dbCommand, new DbCommandInterceptionContext());
				}
				sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
			});
			return sqlVersion;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005FAC File Offset: 0x000041AC
		protected override bool DbDatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			return this.DbDatabaseExists(connection, commandTimeout, new Lazy<StoreItemCollection>(() => storeItemCollection));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005FE0 File Offset: 0x000041E0
		protected override bool DbDatabaseExists(DbConnection connection, int? commandTimeout, Lazy<StoreItemCollection> storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<Lazy<StoreItemCollection>>(storeItemCollection, "storeItemCollection");
			if (connection.State == ConnectionState.Open)
			{
				return true;
			}
			SqlConnection requiredSqlConnection = SqlProviderUtilities.GetRequiredSqlConnection(connection);
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(DbInterception.Dispatch.Connection.GetConnectionString(requiredSqlConnection, new DbInterceptionContext()));
			if (string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog) && string.IsNullOrEmpty(sqlConnectionStringBuilder.AttachDBFilename))
			{
				throw new InvalidOperationException(Strings.SqlProvider_DdlGeneration_MissingInitialCatalog);
			}
			if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog) && this.CheckDatabaseExists(requiredSqlConnection, commandTimeout, sqlConnectionStringBuilder.InitialCatalog))
			{
				return true;
			}
			if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.AttachDBFilename))
			{
				try
				{
					SqlProviderServices.UsingConnection(requiredSqlConnection, delegate(DbConnection con)
					{
					});
					return true;
				}
				catch (SqlException ex)
				{
					if (!string.IsNullOrEmpty(sqlConnectionStringBuilder.InitialCatalog))
					{
						return this.CheckDatabaseExists(requiredSqlConnection, commandTimeout, sqlConnectionStringBuilder.InitialCatalog);
					}
					string fileName = SqlProviderServices.GetMdfFileName(sqlConnectionStringBuilder.AttachDBFilename);
					bool databaseDoesNotExistInSysTables = false;
					this.UsingMasterConnection(requiredSqlConnection, delegate(DbConnection conn)
					{
						SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
						string text = SqlDdlBuilder.CreateCountDatabasesBasedOnFileNameScript(fileName, sqlVersion == SqlVersion.Sql8);
						using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, text, commandTimeout))
						{
							int num = (int)DbInterception.Dispatch.Command.Scalar(dbCommand, new DbCommandInterceptionContext());
							databaseDoesNotExistInSysTables = num == 0;
						}
					});
					if (databaseDoesNotExistInSysTables)
					{
						return false;
					}
					throw new InvalidOperationException(Strings.SqlProvider_DdlGeneration_CannotTellIfDatabaseExists, ex);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006140 File Offset: 0x00004340
		private bool CheckDatabaseExists(SqlConnection sqlConnection, int? commandTimeout, string databaseName)
		{
			bool databaseExists = false;
			this.UsingMasterConnection(sqlConnection, delegate(DbConnection conn)
			{
				string text = SqlDdlBuilder.CreateDatabaseExistsScript(databaseName);
				using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, text, commandTimeout))
				{
					databaseExists = (int)DbInterception.Dispatch.Command.Scalar(dbCommand, new DbCommandInterceptionContext()) >= 1;
				}
			});
			return databaseExists;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006184 File Offset: 0x00004384
		protected override void DbDeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			SqlConnection requiredSqlConnection = SqlProviderUtilities.GetRequiredSqlConnection(connection);
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(DbInterception.Dispatch.Connection.GetConnectionString(requiredSqlConnection, new DbInterceptionContext()));
			string initialCatalog = sqlConnectionStringBuilder.InitialCatalog;
			string attachDBFilename = sqlConnectionStringBuilder.AttachDBFilename;
			if (!string.IsNullOrEmpty(initialCatalog))
			{
				this.DropDatabase(requiredSqlConnection, commandTimeout, initialCatalog);
				return;
			}
			if (!string.IsNullOrEmpty(attachDBFilename))
			{
				string fullFileName = SqlProviderServices.GetMdfFileName(attachDBFilename);
				List<string> databaseNames = new List<string>();
				this.UsingMasterConnection(requiredSqlConnection, delegate(DbConnection conn)
				{
					SqlVersion sqlVersion = SqlVersionUtils.GetSqlVersion(conn);
					string text2 = SqlDdlBuilder.CreateGetDatabaseNamesBasedOnFileNameScript(fullFileName, sqlVersion == SqlVersion.Sql8);
					DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, text2, commandTimeout);
					using (DbDataReader dbDataReader = DbInterception.Dispatch.Command.Reader(dbCommand, new DbCommandInterceptionContext()))
					{
						while (dbDataReader.Read())
						{
							databaseNames.Add(dbDataReader.GetString(0));
						}
					}
				});
				if (databaseNames.Count > 0)
				{
					using (List<string>.Enumerator enumerator = databaseNames.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							this.DropDatabase(requiredSqlConnection, commandTimeout, text);
						}
						return;
					}
				}
				throw new InvalidOperationException(Strings.SqlProvider_DdlGeneration_CannotDeleteDatabaseNoInitialCatalog);
			}
			throw new InvalidOperationException(Strings.SqlProvider_DdlGeneration_MissingInitialCatalog);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000062A4 File Offset: 0x000044A4
		private void DropDatabase(SqlConnection sqlConnection, int? commandTimeout, string databaseName)
		{
			SqlConnection.ClearAllPools();
			string dropDatabaseScript = SqlDdlBuilder.DropDatabaseScript(databaseName);
			try
			{
				this.UsingMasterConnection(sqlConnection, delegate(DbConnection conn)
				{
					using (DbCommand dbCommand = SqlProviderServices.CreateCommand(conn, dropDatabaseScript, commandTimeout))
					{
						DbInterception.Dispatch.Command.NonQuery(dbCommand, new DbCommandInterceptionContext());
					}
				});
			}
			catch (SqlException ex)
			{
				using (IEnumerator enumerator = ex.Errors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((SqlError)enumerator.Current).Number == 5120)
						{
							return;
						}
					}
				}
				throw;
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006344 File Offset: 0x00004544
		private static string CreateObjectsScript(SqlVersion version, StoreItemCollection storeItemCollection)
		{
			return SqlDdlBuilder.CreateObjectsScript(storeItemCollection, version != SqlVersion.Sql8);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006354 File Offset: 0x00004554
		private static DbCommand CreateCommand(DbConnection sqlConnection, string commandText, int? commandTimeout)
		{
			if (string.IsNullOrEmpty(commandText))
			{
				commandText = Environment.NewLine;
			}
			DbCommand dbCommand = sqlConnection.CreateCommand();
			dbCommand.CommandText = commandText;
			if (commandTimeout != null)
			{
				dbCommand.CommandTimeout = commandTimeout.Value;
			}
			return dbCommand;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006398 File Offset: 0x00004598
		private static void UsingConnection(DbConnection sqlConnection, Action<DbConnection> act)
		{
			DbInterceptionContext interceptionContext = new DbInterceptionContext();
			string holdConnectionString = DbInterception.Dispatch.Connection.GetConnectionString(sqlConnection, interceptionContext);
			DbProviderServices.GetExecutionStrategy(sqlConnection, "System.Data.SqlClient").Execute(delegate
			{
				bool flag = DbInterception.Dispatch.Connection.GetState(sqlConnection, interceptionContext) == ConnectionState.Closed;
				if (flag)
				{
					if (DbInterception.Dispatch.Connection.GetState(sqlConnection, new DbInterceptionContext()) == ConnectionState.Closed && !DbInterception.Dispatch.Connection.GetConnectionString(sqlConnection, interceptionContext).Equals(holdConnectionString, StringComparison.Ordinal))
					{
						DbInterception.Dispatch.Connection.SetConnectionString(sqlConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(holdConnectionString));
					}
					DbInterception.Dispatch.Connection.Open(sqlConnection, interceptionContext);
				}
				try
				{
					act(sqlConnection);
				}
				finally
				{
					if (flag && DbInterception.Dispatch.Connection.GetState(sqlConnection, interceptionContext) == ConnectionState.Open)
					{
						DbInterception.Dispatch.Connection.Close(sqlConnection, interceptionContext);
						if (!DbInterception.Dispatch.Connection.GetConnectionString(sqlConnection, interceptionContext).Equals(holdConnectionString, StringComparison.Ordinal))
						{
							DbInterception.Dispatch.Connection.SetConnectionString(sqlConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(holdConnectionString));
						}
					}
				}
			});
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006408 File Offset: 0x00004608
		private void UsingMasterConnection(DbConnection sqlConnection, Action<DbConnection> act)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(DbInterception.Dispatch.Connection.GetConnectionString(sqlConnection, new DbInterceptionContext()))
			{
				InitialCatalog = "master",
				AttachDBFilename = string.Empty
			};
			try
			{
				using (DbConnection dbConnection = this.CloneDbConnection(sqlConnection))
				{
					DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(sqlConnectionStringBuilder.ConnectionString));
					SqlProviderServices.UsingConnection(dbConnection, act);
				}
			}
			catch (SqlException ex)
			{
				if (!sqlConnectionStringBuilder.IntegratedSecurity && (string.IsNullOrEmpty(sqlConnectionStringBuilder.UserID) || string.IsNullOrEmpty(sqlConnectionStringBuilder.Password)))
				{
					throw new InvalidOperationException(Strings.SqlProvider_CredentialsMissingForMasterConnection, ex);
				}
				throw;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000064D0 File Offset: 0x000046D0
		public override DbConnection CloneDbConnection(DbConnection connection, DbProviderFactory factory)
		{
			ICloneable cloneable = connection as ICloneable;
			if (cloneable != null)
			{
				return (DbConnection)cloneable.Clone();
			}
			return base.CloneDbConnection(connection, factory);
		}

		// Token: 0x04000017 RID: 23
		public const string ProviderInvariantName = "System.Data.SqlClient";

		// Token: 0x04000018 RID: 24
		private ConcurrentDictionary<string, SqlProviderManifest> _providerManifests = new ConcurrentDictionary<string, SqlProviderManifest>();

		// Token: 0x04000019 RID: 25
		private static readonly SqlProviderServices _providerInstance = new SqlProviderServices();

		// Token: 0x0400001A RID: 26
		private static bool _truncateDecimalsToScale = true;

		// Token: 0x0400001B RID: 27
		private static bool _useScopeIdentity = true;

		// Token: 0x0400001C RID: 28
		private static bool _useRowNumberOrderingInOffsetQueries = true;
	}
}
