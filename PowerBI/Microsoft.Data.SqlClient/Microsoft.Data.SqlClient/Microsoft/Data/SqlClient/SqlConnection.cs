using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.Common;
using Microsoft.Data.ProviderBase;
using Microsoft.Data.SqlClient.Server;
using Microsoft.SqlServer.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000E3 RID: 227
	[DefaultEvent("InfoMessage")]
	[DesignerCategory("")]
	public sealed class SqlConnection : DbConnection, ICloneable
	{
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x0003C584 File Offset: 0x0003A784
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x0003C58C File Offset: 0x0003A78C
		internal bool ForceNewConnection { get; set; }

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0003C595 File Offset: 0x0003A795
		internal bool HasColumnEncryptionKeyStoreProvidersRegistered
		{
			get
			{
				return this._customColumnEncryptionKeyStoreProviders != null && this._customColumnEncryptionKeyStoreProviders.Count > 0;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x0003C5AF File Offset: 0x0003A7AF
		[DefaultValue(null)]
		[ResCategory("Data")]
		[ResDescription("Dictionary object containing SQL Server names and their trusted column master key paths.")]
		public static IDictionary<string, IList<string>> ColumnEncryptionTrustedMasterKeyPaths
		{
			get
			{
				return SqlConnection._ColumnEncryptionTrustedMasterKeyPaths;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0003C5B6 File Offset: 0x0003A7B6
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x0003C5BD File Offset: 0x0003A7BD
		[DefaultValue(null)]
		[ResCategory("Data")]
		[ResDescription("Defines whether query metadata caching is enabled.")]
		public static bool ColumnEncryptionQueryMetadataCacheEnabled
		{
			get
			{
				return SqlConnection._ColumnEncryptionQueryMetadataCacheEnabled;
			}
			set
			{
				SqlConnection._ColumnEncryptionQueryMetadataCacheEnabled = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0003C5C5 File Offset: 0x0003A7C5
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x0003C5CC File Offset: 0x0003A7CC
		[DefaultValue(null)]
		[ResCategory("Data")]
		[ResDescription("Defines the time-to-live of entries in the column encryption key cache.")]
		public static TimeSpan ColumnEncryptionKeyCacheTtl
		{
			get
			{
				return SqlConnection._ColumnEncryptionKeyCacheTtl;
			}
			set
			{
				SqlConnection._ColumnEncryptionKeyCacheTtl = value;
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0003C5D4 File Offset: 0x0003A7D4
		public static void RegisterColumnEncryptionKeyStoreProviders(IDictionary<string, SqlColumnEncryptionKeyStoreProvider> customProviders)
		{
			SqlConnection.ValidateCustomProviders(customProviders);
			object obj = SqlConnection.s_globalCustomColumnEncryptionKeyProvidersLock;
			lock (obj)
			{
				if (SqlConnection.s_globalCustomColumnEncryptionKeyStoreProviders != null)
				{
					throw SQL.CanOnlyCallOnce();
				}
				foreach (SqlColumnEncryptionKeyStoreProvider sqlColumnEncryptionKeyStoreProvider in customProviders.Values)
				{
					sqlColumnEncryptionKeyStoreProvider.ColumnEncryptionKeyCacheTtl = new TimeSpan?(new TimeSpan(0L));
				}
				Dictionary<string, SqlColumnEncryptionKeyStoreProvider> dictionary = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>(customProviders, StringComparer.OrdinalIgnoreCase);
				SqlConnection.s_globalCustomColumnEncryptionKeyStoreProviders = dictionary;
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0003C67C File Offset: 0x0003A87C
		public void RegisterColumnEncryptionKeyStoreProvidersOnConnection(IDictionary<string, SqlColumnEncryptionKeyStoreProvider> customProviders)
		{
			SqlConnection.ValidateCustomProviders(customProviders);
			Dictionary<string, SqlColumnEncryptionKeyStoreProvider> dictionary = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>(customProviders, StringComparer.OrdinalIgnoreCase);
			this._customColumnEncryptionKeyStoreProviders = dictionary;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0003C6A4 File Offset: 0x0003A8A4
		private static void ValidateCustomProviders(IDictionary<string, SqlColumnEncryptionKeyStoreProvider> customProviders)
		{
			if (customProviders == null)
			{
				throw SQL.NullCustomKeyStoreProviderDictionary();
			}
			foreach (string text in customProviders.Keys)
			{
				if (string.IsNullOrWhiteSpace(text))
				{
					throw SQL.EmptyProviderName();
				}
				if (text.StartsWith("MSSQL_", StringComparison.InvariantCultureIgnoreCase))
				{
					throw SQL.InvalidCustomKeyStoreProviderName(text, "MSSQL_");
				}
				if (customProviders[text] == null)
				{
					throw SQL.NullProviderValue(text);
				}
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0003C72C File Offset: 0x0003A92C
		internal static bool TryGetSystemColumnEncryptionKeyStoreProvider(string keyStoreName, out SqlColumnEncryptionKeyStoreProvider provider)
		{
			return SqlConnection.s_systemColumnEncryptionKeyStoreProviders.TryGetValue(keyStoreName, out provider);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0003C73C File Offset: 0x0003A93C
		internal bool TryGetColumnEncryptionKeyStoreProvider(string providerName, out SqlColumnEncryptionKeyStoreProvider columnKeyStoreProvider)
		{
			if (this.HasColumnEncryptionKeyStoreProvidersRegistered)
			{
				return this._customColumnEncryptionKeyStoreProviders.TryGetValue(providerName, out columnKeyStoreProvider);
			}
			object obj = SqlConnection.s_globalCustomColumnEncryptionKeyProvidersLock;
			bool flag2;
			lock (obj)
			{
				if (SqlConnection.s_globalCustomColumnEncryptionKeyStoreProviders == null)
				{
					columnKeyStoreProvider = null;
					flag2 = false;
				}
				else
				{
					flag2 = SqlConnection.s_globalCustomColumnEncryptionKeyStoreProviders.TryGetValue(providerName, out columnKeyStoreProvider);
				}
			}
			return flag2;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0003C7A8 File Offset: 0x0003A9A8
		internal static List<string> GetColumnEncryptionSystemKeyStoreProvidersNames()
		{
			return SqlConnection.s_systemColumnEncryptionKeyStoreProviders.Keys.ToList<string>();
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0003C7BC File Offset: 0x0003A9BC
		internal List<string> GetColumnEncryptionCustomKeyStoreProvidersNames()
		{
			if (this._customColumnEncryptionKeyStoreProviders != null && this._customColumnEncryptionKeyStoreProviders.Count > 0)
			{
				return this._customColumnEncryptionKeyStoreProviders.Keys.ToList<string>();
			}
			if (SqlConnection.s_globalCustomColumnEncryptionKeyStoreProviders != null)
			{
				return SqlConnection.s_globalCustomColumnEncryptionKeyStoreProviders.Keys.ToList<string>();
			}
			return new List<string>();
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0003C80C File Offset: 0x0003AA0C
		private bool IsProviderRetriable
		{
			get
			{
				return SqlConfigurableRetryFactory.IsRetriable(this.RetryLogicProvider);
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0003C819 File Offset: 0x0003AA19
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x0003C834 File Offset: 0x0003AA34
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SqlRetryLogicBaseProvider RetryLogicProvider
		{
			get
			{
				if (this._retryLogicProvider == null)
				{
					this._retryLogicProvider = SqlConfigurableRetryLogicManager.ConnectionProvider;
				}
				return this._retryLogicProvider;
			}
			set
			{
				this._retryLogicProvider = value;
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0003C83D File Offset: 0x0003AA3D
		public SqlConnection(string connectionString)
			: this(connectionString, null)
		{
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0003C848 File Offset: 0x0003AA48
		public SqlConnection(string connectionString, SqlCredential credential)
			: this()
		{
			this.ConnectionString = connectionString;
			if (credential != null)
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				if (this.UsesClearUserIdOrPassword(sqlConnectionString))
				{
					throw ADP.InvalidMixedArgumentOfSecureAndClearCredential();
				}
				if (this.UsesIntegratedSecurity(sqlConnectionString))
				{
					throw ADP.InvalidMixedArgumentOfSecureCredentialAndIntegratedSecurity();
				}
				if (this.UsesContextConnection(sqlConnectionString))
				{
					throw ADP.InvalidMixedArgumentOfSecureCredentialAndContextConnection();
				}
				if (this.UsesActiveDirectoryIntegrated(sqlConnectionString))
				{
					throw SQL.SettingCredentialWithIntegratedArgument();
				}
				if (this.UsesActiveDirectoryInteractive(sqlConnectionString))
				{
					throw SQL.SettingCredentialWithInteractiveArgument();
				}
				if (this.UsesActiveDirectoryDeviceCodeFlow(sqlConnectionString))
				{
					throw SQL.SettingCredentialWithDeviceFlowArgument();
				}
				if (this.UsesActiveDirectoryManagedIdentity(sqlConnectionString))
				{
					throw SQL.SettingCredentialWithNonInteractiveArgument("Active Directory Managed Identity");
				}
				if (this.UsesActiveDirectoryMSI(sqlConnectionString))
				{
					throw SQL.SettingCredentialWithNonInteractiveArgument("Active Directory MSI");
				}
				if (this.UsesActiveDirectoryDefault(sqlConnectionString))
				{
					throw SQL.SettingCredentialWithNonInteractiveArgument("Active Directory Default");
				}
				this.Credential = credential;
			}
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0003C914 File Offset: 0x0003AB14
		private SqlConnection(SqlConnection connection)
		{
			this._reconnectLock = new object();
			this._originalConnectionId = Guid.Empty;
			this.ObjectID = Interlocked.Increment(ref SqlConnection._objectTypeCount);
			base..ctor();
			GC.SuppressFinalize(this);
			this.CopyFrom(connection);
			this._connectionString = connection._connectionString;
			if (connection._credential != null)
			{
				SecureString secureString = connection._credential.Password.Copy();
				secureString.MakeReadOnly();
				this._credential = new SqlCredential(connection._credential.UserId, secureString);
			}
			this._accessToken = connection._accessToken;
			this._serverCertificateValidationCallback = connection._serverCertificateValidationCallback;
			this._clientCertificateRetrievalCallback = connection._clientCertificateRetrievalCallback;
			this._originalNetworkAddressInfo = connection._originalNetworkAddressInfo;
			this.CacheConnectionStringProperties();
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0003C9D4 File Offset: 0x0003ABD4
		private void CacheConnectionStringProperties()
		{
			SqlConnectionString sqlConnectionString = this.ConnectionOptions as SqlConnectionString;
			if (sqlConnectionString != null)
			{
				this._connectRetryCount = sqlConnectionString.ConnectRetryCount;
				if (this._connectRetryCount == 1 && ADP.IsAzureSynapseOnDemandEndpoint(sqlConnectionString.DataSource))
				{
					this._connectRetryCount = 5;
					return;
				}
				if (this._connectRetryCount == 1 && ADP.IsAzureSqlServerEndpoint(sqlConnectionString.DataSource))
				{
					this._connectRetryCount = 2;
				}
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x0003CA37 File Offset: 0x0003AC37
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x0003CA40 File Offset: 0x0003AC40
		[DefaultValue(false)]
		[ResCategory("Data")]
		[ResDescription("Collect statistics for this connection.")]
		public bool StatisticsEnabled
		{
			get
			{
				return this._collectstats;
			}
			set
			{
				if (this.IsContextConnection)
				{
					if (value)
					{
						throw SQL.NotAvailableOnContextConnection();
					}
				}
				else
				{
					if (value)
					{
						if (ConnectionState.Open == this.State)
						{
							if (this._statistics == null)
							{
								this._statistics = new SqlStatistics();
								this._statistics._openTimestamp = ADP.TimerCurrent();
							}
							this.Parser.Statistics = this._statistics;
						}
					}
					else if (this._statistics != null && ConnectionState.Open == this.State)
					{
						TdsParser parser = this.Parser;
						parser.Statistics = null;
						this._statistics._closeTimestamp = ADP.TimerCurrent();
					}
					this._collectstats = value;
				}
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x0003CAD6 File Offset: 0x0003ACD6
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x0003CADE File Offset: 0x0003ACDE
		internal bool AsyncCommandInProgress
		{
			get
			{
				return this._AsyncCommandInProgress;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._AsyncCommandInProgress = value;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0003CAE8 File Offset: 0x0003ACE8
		internal bool IsContextConnection
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				return this.UsesContextConnection(sqlConnectionString);
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0003CB08 File Offset: 0x0003AD08
		internal bool IsColumnEncryptionSettingEnabled
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				return sqlConnectionString != null && sqlConnectionString.ColumnEncryptionSetting == SqlConnectionColumnEncryptionSetting.Enabled;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0003CB30 File Offset: 0x0003AD30
		internal string EnclaveAttestationUrl
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				return sqlConnectionString.EnclaveAttestationUrl;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x0003CB50 File Offset: 0x0003AD50
		internal SqlConnectionAttestationProtocol AttestationProtocol
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				return sqlConnectionString.AttestationProtocol;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0003CB6F File Offset: 0x0003AD6F
		internal SqlConnectionIPAddressPreference iPAddressPreference
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).IPAddressPreference;
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0003CB81 File Offset: 0x0003AD81
		private bool UsesContextConnection(SqlConnectionString opt)
		{
			return opt != null && opt.ContextConnection;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0003CB8E File Offset: 0x0003AD8E
		private bool UsesActiveDirectoryIntegrated(SqlConnectionString opt)
		{
			return opt != null && opt.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0003CB9E File Offset: 0x0003AD9E
		private bool UsesActiveDirectoryInteractive(SqlConnectionString opt)
		{
			return opt != null && opt.Authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0003CBAE File Offset: 0x0003ADAE
		private bool UsesActiveDirectoryDeviceCodeFlow(SqlConnectionString opt)
		{
			return opt != null && opt.Authentication == SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0003CBBE File Offset: 0x0003ADBE
		private bool UsesActiveDirectoryManagedIdentity(SqlConnectionString opt)
		{
			return opt != null && opt.Authentication == SqlAuthenticationMethod.ActiveDirectoryManagedIdentity;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0003CBCE File Offset: 0x0003ADCE
		private bool UsesActiveDirectoryMSI(SqlConnectionString opt)
		{
			return opt != null && opt.Authentication == SqlAuthenticationMethod.ActiveDirectoryMSI;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0003CBDE File Offset: 0x0003ADDE
		private bool UsesActiveDirectoryDefault(SqlConnectionString opt)
		{
			return opt != null && opt.Authentication == SqlAuthenticationMethod.ActiveDirectoryDefault;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0003CBEF File Offset: 0x0003ADEF
		private bool UsesAuthentication(SqlConnectionString opt)
		{
			return opt != null && opt.Authentication > SqlAuthenticationMethod.NotSpecified;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0003CBFF File Offset: 0x0003ADFF
		private bool UsesIntegratedSecurity(SqlConnectionString opt)
		{
			return opt != null && opt.IntegratedSecurity;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003CC0C File Offset: 0x0003AE0C
		private bool UsesClearUserIdOrPassword(SqlConnectionString opt)
		{
			bool flag = false;
			if (opt != null)
			{
				flag = !ADP.IsEmpty(opt.UserID) || !ADP.IsEmpty(opt.Password);
			}
			return flag;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0003CC3E File Offset: 0x0003AE3E
		private bool UsesCertificate(SqlConnectionString opt)
		{
			return opt != null && opt.UsesCertificate;
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0003CC4B File Offset: 0x0003AE4B
		internal SqlConnectionString.TransactionBindingEnum TransactionBinding
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TransactionBinding;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0003CC5D File Offset: 0x0003AE5D
		internal SqlConnectionString.TypeSystem TypeSystem
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TypeSystemVersion;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0003CC6F File Offset: 0x0003AE6F
		internal Version TypeSystemAssemblyVersion
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TypeSystemAssemblyVersion;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x0003CC81 File Offset: 0x0003AE81
		internal PoolBlockingPeriod PoolBlockingPeriod
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).PoolBlockingPeriod;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0003CC93 File Offset: 0x0003AE93
		internal int ConnectRetryInterval
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).ConnectRetryInterval;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0003CCA5 File Offset: 0x0003AEA5
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0003CCAC File Offset: 0x0003AEAC
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x0003CCE8 File Offset: 0x0003AEE8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("Access token to use for authentication.")]
		public string AccessToken
		{
			get
			{
				string text = this._accessToken;
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.UserConnectionOptions;
				if (this.InnerConnection.ShouldHidePassword && sqlConnectionString != null && !sqlConnectionString.PersistSecurityInfo)
				{
					text = null;
				}
				return text;
			}
			set
			{
				if (!this.InnerConnection.AllowSetConnectionString)
				{
					throw ADP.OpenConnectionPropertySet("AccessToken", this.InnerConnection.State);
				}
				if (value != null)
				{
					this.CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken((SqlConnectionString)this.ConnectionOptions);
				}
				this._accessToken = value;
				this.ConnectionString_Set(new SqlConnectionPoolKey(this._connectionString, this._credential, this._accessToken, this._serverCertificateValidationCallback, this._clientCertificateRetrievalCallback, this._originalNetworkAddressInfo));
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0003CD64 File Offset: 0x0003AF64
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("Current connection timeout value, 'Connect Timeout=X' in the ConnectionString.")]
		public int CommandTimeout
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				if (sqlConnectionString == null)
				{
					return 30;
				}
				return sqlConnectionString.CommandTimeout;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x0003CD89 File Offset: 0x0003AF89
		// (set) Token: 0x060010BB RID: 4283 RVA: 0x0003CD94 File Offset: 0x0003AF94
		[DefaultValue("")]
		[RecommendedAsConfigurable(true)]
		[SettingsBindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("Data")]
		[ResDescription("Information used to connect to a DataSource, such as 'Data Source=x;Initial Catalog=x;Integrated Security=SSPI'.")]
		public override string ConnectionString
		{
			get
			{
				return this.ConnectionString_Get();
			}
			set
			{
				if (this._credential != null || this._accessToken != null)
				{
					SqlConnectionString sqlConnectionString = new SqlConnectionString(value);
					if (this._credential != null)
					{
						if (this.UsesActiveDirectoryIntegrated(sqlConnectionString))
						{
							throw SQL.SettingIntegratedWithCredential();
						}
						if (this.UsesActiveDirectoryInteractive(sqlConnectionString))
						{
							throw SQL.SettingInteractiveWithCredential();
						}
						if (this.UsesActiveDirectoryDeviceCodeFlow(sqlConnectionString))
						{
							throw SQL.SettingDeviceFlowWithCredential();
						}
						if (this.UsesActiveDirectoryManagedIdentity(sqlConnectionString))
						{
							throw SQL.SettingNonInteractiveWithCredential("Active Directory Managed Identity");
						}
						if (this.UsesActiveDirectoryMSI(sqlConnectionString))
						{
							throw SQL.SettingNonInteractiveWithCredential("Active Directory MSI");
						}
						if (this.UsesActiveDirectoryDefault(sqlConnectionString))
						{
							throw SQL.SettingNonInteractiveWithCredential("Active Directory Default");
						}
						this.CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential(sqlConnectionString);
					}
					else if (this._accessToken != null)
					{
						this.CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken(sqlConnectionString);
					}
				}
				this.ConnectionString_Set(new SqlConnectionPoolKey(value, this._credential, this._accessToken, this._serverCertificateValidationCallback, this._clientCertificateRetrievalCallback, this._originalNetworkAddressInfo));
				this._connectionString = value;
				this.CacheConnectionStringProperties();
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0003CE7C File Offset: 0x0003B07C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("Current connection timeout value, 'Connect Timeout=X' in the ConnectionString.")]
		public override int ConnectionTimeout
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				if (sqlConnectionString == null)
				{
					return 15;
				}
				return sqlConnectionString.ConnectTimeout;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0003CEA4 File Offset: 0x0003B0A4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("Current SQL Server database, 'Initial Catalog=X' in the connection string.")]
		public override string Database
		{
			get
			{
				SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
				string text;
				if (sqlInternalConnection != null)
				{
					text = sqlInternalConnection.CurrentDatabase;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					text = ((sqlConnectionString != null) ? sqlConnectionString.InitialCatalog : "");
				}
				return text;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0003CEE8 File Offset: 0x0003B0E8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal string SQLDNSCachingSupportedState
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				string text;
				if (sqlInternalConnectionTds != null)
				{
					text = (sqlInternalConnectionTds.IsSQLDNSCachingSupported ? "true" : "false");
				}
				else
				{
					text = "innerConnection is null!";
				}
				return text;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0003CF24 File Offset: 0x0003B124
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal string SQLDNSCachingSupportedStateBeforeRedirect
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				string text;
				if (sqlInternalConnectionTds != null)
				{
					text = (sqlInternalConnectionTds.IsDNSCachingBeforeRedirectSupported ? "true" : "false");
				}
				else
				{
					text = "innerConnection is null!";
				}
				return text;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0003CF60 File Offset: 0x0003B160
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("Current SqlServer that the connection is opened to, 'Data Source=X' in the connection string.")]
		public override string DataSource
		{
			get
			{
				SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
				string text;
				if (sqlInternalConnection != null)
				{
					text = sqlInternalConnection.CurrentDataSource;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					text = ((sqlConnectionString != null) ? sqlConnectionString.DataSource : "");
				}
				return text;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0003CFA4 File Offset: 0x0003B1A4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("Data")]
		[ResDescription("Network packet size, 'Packet Size=x' in the connection string.")]
		public int PacketSize
		{
			get
			{
				if (this.IsContextConnection)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				int num;
				if (sqlInternalConnectionTds != null)
				{
					num = sqlInternalConnectionTds.PacketSize;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					num = ((sqlConnectionString != null) ? sqlConnectionString.PacketSize : 8000);
				}
				return num;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x0003CFF8 File Offset: 0x0003B1F8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("Data")]
		[ResDescription("A guid to represent the physical connection.")]
		public Guid ClientConnectionId
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					return sqlInternalConnectionTds.ClientConnectionId;
				}
				Task currentReconnectionTask = this._currentReconnectionTask;
				DbConnectionClosedPreviouslyOpened dbConnectionClosedPreviouslyOpened = this.InnerConnection as DbConnectionClosedPreviouslyOpened;
				if ((currentReconnectionTask != null && !currentReconnectionTask.IsCompleted) || dbConnectionClosedPreviouslyOpened != null)
				{
					return this._originalConnectionId;
				}
				return Guid.Empty;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0003D048 File Offset: 0x0003B248
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("Version of the SQL Server accessed by the SqlConnection.")]
		public override string ServerVersion
		{
			get
			{
				return this.GetOpenConnection().ServerVersion;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0003D058 File Offset: 0x0003B258
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("Server Process Id (SPID) of the active connection.")]
		public int ServerProcessId
		{
			get
			{
				if (!(this.State.Equals(ConnectionState.Open) | this.State.Equals(ConnectionState.Executing) | this.State.Equals(ConnectionState.Fetching)))
				{
					return 0;
				}
				return this.GetOpenTdsConnection().ServerProcessId;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0003D0C4 File Offset: 0x0003B2C4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("The ConnectionState indicating whether the connection is open or closed.")]
		public override ConnectionState State
		{
			get
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return ConnectionState.Open;
				}
				return this.InnerConnection.State;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0003D0F0 File Offset: 0x0003B2F0
		internal SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0003D0F8 File Offset: 0x0003B2F8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("Data")]
		[ResDescription("Workstation Id, 'Workstation ID=x' in the connection string.")]
		public string WorkstationId
		{
			get
			{
				if (this.IsContextConnection)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				string text = ((sqlConnectionString != null) ? sqlConnectionString.WorkstationId : null);
				if (text == null)
				{
					text = Environment.MachineName;
				}
				return text;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0003D138 File Offset: 0x0003B338
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x0003D174 File Offset: 0x0003B374
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("User Id and secure password to use for authentication.")]
		public SqlCredential Credential
		{
			get
			{
				SqlCredential sqlCredential = this._credential;
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.UserConnectionOptions;
				if (this.InnerConnection.ShouldHidePassword && sqlConnectionString != null && !sqlConnectionString.PersistSecurityInfo)
				{
					sqlCredential = null;
				}
				return sqlCredential;
			}
			set
			{
				if (!this.InnerConnection.AllowSetConnectionString)
				{
					throw ADP.OpenConnectionPropertySet("Credential", this.InnerConnection.State);
				}
				if (value != null)
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					if (this.UsesActiveDirectoryIntegrated(sqlConnectionString))
					{
						throw SQL.SettingCredentialWithIntegratedInvalid();
					}
					if (this.UsesActiveDirectoryInteractive(sqlConnectionString))
					{
						throw SQL.SettingCredentialWithInteractiveInvalid();
					}
					if (this.UsesActiveDirectoryDeviceCodeFlow(sqlConnectionString))
					{
						throw SQL.SettingCredentialWithDeviceFlowInvalid();
					}
					if (this.UsesActiveDirectoryManagedIdentity(sqlConnectionString))
					{
						throw SQL.SettingCredentialWithNonInteractiveInvalid("Active Directory Managed Identity");
					}
					if (this.UsesActiveDirectoryMSI(sqlConnectionString))
					{
						throw SQL.SettingCredentialWithNonInteractiveInvalid("Active Directory MSI");
					}
					if (this.UsesActiveDirectoryDefault(sqlConnectionString))
					{
						throw SQL.SettingCredentialWithNonInteractiveInvalid("Active Directory Default");
					}
					this.CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential(sqlConnectionString);
					if (this._accessToken != null)
					{
						throw ADP.InvalidMixedUsageOfCredentialAndAccessToken();
					}
				}
				this._credential = value;
				this.ConnectionString_Set(new SqlConnectionPoolKey(this._connectionString, this._credential, this._accessToken, this._serverCertificateValidationCallback, this._clientCertificateRetrievalCallback, this._originalNetworkAddressInfo));
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003D26A File Offset: 0x0003B46A
		private void CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential(SqlConnectionString connectionOptions)
		{
			if (this.UsesClearUserIdOrPassword(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfSecureAndClearCredential();
			}
			if (this.UsesIntegratedSecurity(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity();
			}
			if (this.UsesContextConnection(connectionOptions))
			{
				throw ADP.InvalidMixedArgumentOfSecureCredentialAndContextConnection();
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0003D29C File Offset: 0x0003B49C
		private void CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken(SqlConnectionString connectionOptions)
		{
			if (this.UsesClearUserIdOrPassword(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndUserIDPassword();
			}
			if (this.UsesIntegratedSecurity(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndIntegratedSecurity();
			}
			if (this.UsesContextConnection(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndContextConnection();
			}
			if (this.UsesAuthentication(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndAuthentication();
			}
			if (this._credential != null)
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndCredential();
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060010CC RID: 4300 RVA: 0x0003D2F3 File Offset: 0x0003B4F3
		// (remove) Token: 0x060010CD RID: 4301 RVA: 0x0003D306 File Offset: 0x0003B506
		[ResCategory("InfoMessage")]
		[ResDescription("Event triggered when messages arrive from the DataSource.")]
		public event SqlInfoMessageEventHandler InfoMessage
		{
			add
			{
				base.Events.AddHandler(SqlConnection.EventInfoMessage, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlConnection.EventInfoMessage, value);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0003D319 File Offset: 0x0003B519
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x0003D321 File Offset: 0x0003B521
		public bool FireInfoMessageEventOnUserErrors
		{
			get
			{
				return this._fireInfoMessageEventOnUserErrors;
			}
			set
			{
				this._fireInfoMessageEventOnUserErrors = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0003D32A File Offset: 0x0003B52A
		internal int ReconnectCount
		{
			get
			{
				return this._reconnectCount;
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003D332 File Offset: 0x0003B532
		public new SqlTransaction BeginTransaction()
		{
			return this.BeginTransaction(global::System.Data.IsolationLevel.Unspecified, null);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003D33C File Offset: 0x0003B53C
		public new SqlTransaction BeginTransaction(global::System.Data.IsolationLevel iso)
		{
			return this.BeginTransaction(iso, null);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003D346 File Offset: 0x0003B546
		public SqlTransaction BeginTransaction(string transactionName)
		{
			return this.BeginTransaction(global::System.Data.IsolationLevel.Unspecified, transactionName);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003D350 File Offset: 0x0003B550
		protected override DbTransaction BeginDbTransaction(global::System.Data.IsolationLevel isolationLevel)
		{
			DbTransaction dbTransaction2;
			using (TryEventScope.Create<int, int>("<prov.SqlConnection.BeginDbTransaction|API> {0}, isolationLevel={1}", this.ObjectID, (int)isolationLevel))
			{
				DbTransaction dbTransaction = this.BeginTransaction(isolationLevel);
				GC.KeepAlive(this);
				dbTransaction2 = dbTransaction;
			}
			return dbTransaction2;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003D39C File Offset: 0x0003B59C
		public SqlTransaction BeginTransaction(global::System.Data.IsolationLevel iso, string transactionName)
		{
			this.WaitForPendingReconnection();
			SqlStatistics sqlStatistics = null;
			long num = SqlClientEventSource.Log.TryScopeEnterEvent<int, int, string>("<sc.SqlConnection.BeginTransaction|API> {0}, iso={1}, transactionName='{2}'", this.ObjectID, (int)iso, transactionName);
			SqlTransaction sqlTransaction2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				bool flag = true;
				SqlTransaction sqlTransaction;
				do
				{
					sqlTransaction = this.GetOpenConnection().BeginSqlTransaction(iso, transactionName, flag);
					flag = false;
				}
				while (sqlTransaction.InternalTransaction.ConnectionHasBeenRestored);
				GC.KeepAlive(this);
				sqlTransaction2 = sqlTransaction;
			}
			finally
			{
				SqlClientEventSource.Log.TryScopeLeaveEvent(num);
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return sqlTransaction2;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003D424 File Offset: 0x0003B624
		public override void ChangeDatabase(string database)
		{
			SqlStatistics sqlStatistics = null;
			this.RepairInnerConnection();
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlConnection.ChangeDatabase|API|Correlation> ObjectID{0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this);
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.InnerConnection.ChangeDatabase(database);
			}
			catch (OutOfMemoryException ex)
			{
				this.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003D4DC File Offset: 0x0003B6DC
		public static void ClearAllPools()
		{
			new SqlClientPermission(PermissionState.Unrestricted).Demand();
			SqlConnectionFactory.SingletonInstance.ClearAllPools();
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003D4F4 File Offset: 0x0003B6F4
		public static void ClearPool(SqlConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			DbConnectionOptions userConnectionOptions = connection.UserConnectionOptions;
			if (userConnectionOptions != null)
			{
				userConnectionOptions.DemandPermission();
				if (connection.IsContextConnection)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				SqlConnectionFactory.SingletonInstance.ClearPool(connection);
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003D538 File Offset: 0x0003B738
		object ICloneable.Clone()
		{
			return new SqlConnection(this);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003D54D File Offset: 0x0003B74D
		private void CloseInnerConnection()
		{
			this._originalConnectionId = this.ClientConnectionId;
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003D570 File Offset: 0x0003B770
		public override void Close()
		{
			using (TryEventScope.Create<int>("<sc.SqlConnection.Close|API> {0}", this.ObjectID))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlConnection.Close|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
				try
				{
					SqlStatistics sqlStatistics = null;
					TdsParser tdsParser = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this);
						sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
						Task currentReconnectionTask = this._currentReconnectionTask;
						if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
						{
							CancellationTokenSource reconnectionCancellationSource = this._reconnectionCancellationSource;
							if (reconnectionCancellationSource != null)
							{
								reconnectionCancellationSource.Cancel();
							}
							AsyncHelper.WaitForCompletion(currentReconnectionTask, 0, null, false);
							if (this.State != ConnectionState.Open)
							{
								this.OnStateChange(DbConnectionInternal.StateChangeClosed);
							}
						}
						this.CancelOpenAndWait();
						this.CloseInnerConnection();
						GC.SuppressFinalize(this);
						if (this.Statistics != null)
						{
							this._statistics._closeTimestamp = ADP.TimerCurrent();
						}
					}
					catch (OutOfMemoryException ex)
					{
						this.Abort(ex);
						throw;
					}
					catch (StackOverflowException ex2)
					{
						this.Abort(ex2);
						throw;
					}
					catch (ThreadAbortException ex3)
					{
						this.Abort(ex3);
						SqlInternalConnection.BestEffortCleanup(tdsParser);
						throw;
					}
					finally
					{
						SqlStatistics.StopTimer(sqlStatistics);
						if (this._lastIdentity != null)
						{
							this._lastIdentity.Dispose();
						}
					}
				}
				finally
				{
					SqlDebugContext sdc = this._sdc;
					this._sdc = null;
					if (sdc != null)
					{
						sdc.Dispose();
					}
				}
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0003D730 File Offset: 0x0003B930
		public new SqlCommand CreateCommand()
		{
			return new SqlCommand(null, this);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0003D73C File Offset: 0x0003B93C
		private void DisposeMe(bool disposing)
		{
			this._credential = null;
			this._accessToken = null;
			if (!disposing)
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null && !sqlInternalConnectionTds.ConnectionOptions.Pooling)
				{
					TdsParser parser = sqlInternalConnectionTds.Parser;
					if (parser != null && parser._physicalStateObj != null)
					{
						parser._physicalStateObj.DecrementPendingCallbacks(false);
					}
				}
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0003D795 File Offset: 0x0003B995
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			if (this.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			this.EnlistDistributedTransactionHelper(transaction);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0003D7AC File Offset: 0x0003B9AC
		public override void Open()
		{
			this.Open(SqlConnectionOverrides.None);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003D7B8 File Offset: 0x0003B9B8
		private bool TryOpenWithRetry(TaskCompletionSource<DbConnectionInternal> retry, SqlConnectionOverrides overrides)
		{
			return this.RetryLogicProvider.Execute<bool>(this, () => this.TryOpen(retry, overrides));
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003D7F8 File Offset: 0x0003B9F8
		public void Open(SqlConnectionOverrides overrides)
		{
			using (TryEventScope.Create<int, ActivityCorrelator.ActivityId>("<sc.SqlConnection.Open|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlConnection.Open|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
				if (this.StatisticsEnabled)
				{
					if (this._statistics == null)
					{
						this._statistics = new SqlStatistics();
					}
					else
					{
						this._statistics.ContinueOnNewConnection();
					}
				}
				SqlStatistics sqlStatistics = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					if (!(this.IsProviderRetriable ? this.TryOpenWithRetry(null, overrides) : this.TryOpen(null, overrides)))
					{
						throw ADP.InternalError(ADP.InternalErrorCode.SynchronousConnectReturnedPending);
					}
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0003D8C4 File Offset: 0x0003BAC4
		internal void RegisterWaitingForReconnect(Task waitingTask)
		{
			if (((SqlConnectionString)this.ConnectionOptions).MARS)
			{
				return;
			}
			Interlocked.CompareExchange<Task>(ref this._asyncWaitingForReconnection, waitingTask, null);
			if (this._asyncWaitingForReconnection != waitingTask)
			{
				throw SQL.MARSUnsupportedOnConnection();
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003D8F8 File Offset: 0x0003BAF8
		private async Task ReconnectAsync(int timeout)
		{
			try
			{
				long commandTimeoutExpiration = 0L;
				if (timeout > 0)
				{
					commandTimeoutExpiration = ADP.TimerCurrent() + ADP.TimerFromSeconds(timeout);
				}
				CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
				this._reconnectionCancellationSource = cancellationTokenSource;
				CancellationToken ctoken = cancellationTokenSource.Token;
				int retryCount = this._connectRetryCount;
				for (int attempt = 0; attempt < retryCount; attempt++)
				{
					if (ctoken.IsCancellationRequested)
					{
						SqlClientEventSource.Log.TryTraceEvent<Guid>("<sc.SqlConnection.ReconnectAsync|INFO> Original ClientConnectionID: {0} - reconnection cancelled.", this._originalConnectionId);
						return;
					}
					try
					{
						this._impersonateIdentity = this._lastIdentity;
						try
						{
							this.ForceNewConnection = true;
							await this.OpenAsync(ctoken).ConfigureAwait(false);
							this._reconnectCount++;
						}
						finally
						{
							this._impersonateIdentity = null;
							this.ForceNewConnection = false;
						}
						SqlClientEventSource.Log.TryTraceEvent<Guid, Guid>("<sc.SqlConnection.ReconnectIfNeeded|INFO> Reconnection succeeded.  ClientConnectionID {0} -> {1}", this._originalConnectionId, this.ClientConnectionId);
						return;
					}
					catch (SqlException ex)
					{
						SqlClientEventSource.Log.TryTraceEvent<Guid, string>("<sc.SqlConnection.ReconnectAsyncINFO> Original ClientConnectionID {0} - reconnection attempt failed error {1}", this._originalConnectionId, ex.Message);
						if (attempt == retryCount - 1)
						{
							SqlClientEventSource.Log.TryTraceEvent<Guid>("<sc.SqlConnection.ReconnectAsync|INFO> Original ClientConnectionID {0} - give up reconnection", this._originalConnectionId);
							throw SQL.CR_AllAttemptsFailed(ex, this._originalConnectionId);
						}
						if (timeout > 0 && ADP.TimerRemaining(commandTimeoutExpiration) < ADP.TimerFromSeconds(this.ConnectRetryInterval))
						{
							throw SQL.CR_NextAttemptWillExceedQueryTimeout(ex, this._originalConnectionId);
						}
					}
					await Task.Delay(1000 * this.ConnectRetryInterval, ctoken).ConfigureAwait(false);
				}
				ctoken = default(CancellationToken);
			}
			finally
			{
				this._recoverySessionData = null;
				this._suppressStateChangeForReconnection = false;
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0003D944 File Offset: 0x0003BB44
		internal Task ValidateAndReconnect(Action beforeDisconnect, int timeout)
		{
			Task task = this._currentReconnectionTask;
			while (task != null && task.IsCompleted)
			{
				Interlocked.CompareExchange<Task>(ref this._currentReconnectionTask, null, task);
				task = this._currentReconnectionTask;
			}
			if (task == null)
			{
				if (this._connectRetryCount > 0)
				{
					SqlInternalConnectionTds openTdsConnection = this.GetOpenTdsConnection();
					if (openTdsConnection._sessionRecoveryAcknowledged)
					{
						TdsParserStateObject physicalStateObj = openTdsConnection.Parser._physicalStateObj;
						if (!physicalStateObj.ValidateSNIConnection())
						{
							if (openTdsConnection.Parser._sessionPool != null && openTdsConnection.Parser._sessionPool.ActiveSessionsCount > 0)
							{
								if (beforeDisconnect != null)
								{
									beforeDisconnect();
								}
								this.OnError(SQL.CR_UnrecoverableClient(this.ClientConnectionId), true, null);
							}
							SessionData currentSessionData = openTdsConnection.CurrentSessionData;
							if (currentSessionData._unrecoverableStatesCount == 0)
							{
								bool flag = false;
								object reconnectLock = this._reconnectLock;
								lock (reconnectLock)
								{
									openTdsConnection.CheckEnlistedTransactionBinding();
									task = this._currentReconnectionTask;
									if (task == null)
									{
										if (currentSessionData._unrecoverableStatesCount == 0)
										{
											this._originalConnectionId = this.ClientConnectionId;
											SqlClientEventSource.Log.TryTraceEvent<Guid>("<sc.SqlConnection.ReconnectIfNeeded|INFO> Connection ClientConnectionID {0} is invalid, reconnecting", this._originalConnectionId);
											this._recoverySessionData = currentSessionData;
											if (beforeDisconnect != null)
											{
												beforeDisconnect();
											}
											try
											{
												this._suppressStateChangeForReconnection = true;
												openTdsConnection.DoomThisConnection();
											}
											catch (SqlException)
											{
											}
											task = Task.Run(() => this.ReconnectAsync(timeout));
											this._currentReconnectionTask = task;
										}
									}
									else
									{
										flag = true;
									}
								}
								if (flag && beforeDisconnect != null)
								{
									beforeDisconnect();
								}
							}
							else
							{
								if (beforeDisconnect != null)
								{
									beforeDisconnect();
								}
								this.OnError(SQL.CR_UnrecoverableServer(this.ClientConnectionId), true, null);
							}
						}
					}
				}
			}
			else if (beforeDisconnect != null)
			{
				beforeDisconnect();
			}
			return task;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0003DB10 File Offset: 0x0003BD10
		private void WaitForPendingReconnection()
		{
			Task currentReconnectionTask = this._currentReconnectionTask;
			if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
			{
				AsyncHelper.WaitForCompletion(currentReconnectionTask, 0, null, false);
			}
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0003DB38 File Offset: 0x0003BD38
		private void CancelOpenAndWait()
		{
			Tuple<TaskCompletionSource<DbConnectionInternal>, Task> currentCompletion = this._currentCompletion;
			if (currentCompletion != null)
			{
				currentCompletion.Item1.TrySetCanceled();
				((IAsyncResult)currentCompletion.Item2).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0003DB6C File Offset: 0x0003BD6C
		private Task InternalOpenWithRetryAsync(CancellationToken cancellationToken)
		{
			return this.RetryLogicProvider.ExecuteAsync(this, () => this.InternalOpenAsync(cancellationToken), cancellationToken);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0003DBAB File Offset: 0x0003BDAB
		public override Task OpenAsync(CancellationToken cancellationToken)
		{
			if (!this.IsProviderRetriable)
			{
				return this.InternalOpenAsync(cancellationToken);
			}
			return this.InternalOpenWithRetryAsync(cancellationToken);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0003DBC4 File Offset: 0x0003BDC4
		private Task InternalOpenAsync(CancellationToken cancellationToken)
		{
			long num = SqlClientEventSource.Log.TryPoolerScopeEnterEvent<int>("<sc.SqlConnection.OpenAsync|API> {0}", this.ObjectID);
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlConnection.OpenAsync|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			Task task;
			try
			{
				if (this.StatisticsEnabled)
				{
					if (this._statistics == null)
					{
						this._statistics = new SqlStatistics();
					}
					else
					{
						this._statistics.ContinueOnNewConnection();
					}
				}
				SqlStatistics sqlStatistics = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					Transaction currentTransaction = ADP.GetCurrentTransaction();
					TaskCompletionSource<DbConnectionInternal> completion = new TaskCompletionSource<DbConnectionInternal>(currentTransaction);
					TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
					if (cancellationToken.IsCancellationRequested)
					{
						taskCompletionSource.SetCanceled();
						task = taskCompletionSource.Task;
					}
					else if (this.IsContextConnection)
					{
						taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.NotAvailableOnContextConnection()));
						task = taskCompletionSource.Task;
					}
					else
					{
						bool flag;
						try
						{
							flag = this.TryOpen(completion, SqlConnectionOverrides.None);
						}
						catch (Exception ex)
						{
							taskCompletionSource.SetException(ex);
							return taskCompletionSource.Task;
						}
						if (flag)
						{
							taskCompletionSource.SetResult(null);
							task = taskCompletionSource.Task;
						}
						else
						{
							CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
							if (cancellationToken.CanBeCanceled)
							{
								cancellationTokenRegistration = cancellationToken.Register(delegate
								{
									completion.TrySetCanceled();
								});
							}
							SqlConnection.OpenAsyncRetry openAsyncRetry = new SqlConnection.OpenAsyncRetry(this, completion, taskCompletionSource, cancellationTokenRegistration);
							this._currentCompletion = new Tuple<TaskCompletionSource<DbConnectionInternal>, Task>(completion, taskCompletionSource.Task);
							completion.Task.ContinueWith(new Action<Task<DbConnectionInternal>>(openAsyncRetry.Retry), TaskScheduler.Default);
							task = taskCompletionSource.Task;
						}
					}
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryPoolerScopeLeaveEvent(num);
			}
			return task;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0003DDC4 File Offset: 0x0003BFC4
		private bool TryOpen(TaskCompletionSource<DbConnectionInternal> retry, SqlConnectionOverrides overrides = SqlConnectionOverrides.None)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
			bool flag = false;
			this._applyTransientFaultHandling = !overrides.HasFlag(SqlConnectionOverrides.OpenWithoutRetry) && sqlConnectionString != null && sqlConnectionString.ConnectRetryCount > 0;
			if (sqlConnectionString != null && (sqlConnectionString.Authentication == SqlAuthenticationMethod.SqlPassword || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryPassword || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryServicePrincipal) && (!sqlConnectionString._hasUserIdKeyword || !sqlConnectionString._hasPasswordKeyword) && this._credential == null)
			{
				throw SQL.CredentialsNotProvided(sqlConnectionString.Authentication);
			}
			if (this._impersonateIdentity != null)
			{
				using (WindowsIdentity currentWindowsIdentity = DbConnectionPoolIdentity.GetCurrentWindowsIdentity())
				{
					if (this._impersonateIdentity.User == currentWindowsIdentity.User)
					{
						flag = this.TryOpenInner(retry);
						goto IL_0109;
					}
					using (this._impersonateIdentity.Impersonate())
					{
						flag = this.TryOpenInner(retry);
						goto IL_0109;
					}
				}
			}
			if (this.UsesIntegratedSecurity(sqlConnectionString) || this.UsesCertificate(sqlConnectionString) || this.UsesActiveDirectoryIntegrated(sqlConnectionString))
			{
				this._lastIdentity = DbConnectionPoolIdentity.GetCurrentWindowsIdentity();
			}
			else
			{
				this._lastIdentity = null;
			}
			flag = this.TryOpenInner(retry);
			IL_0109:
			this._applyTransientFaultHandling = sqlConnectionString != null && sqlConnectionString.ConnectRetryCount > 0;
			return flag;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0003DF0C File Offset: 0x0003C10C
		private bool TryOpenInner(TaskCompletionSource<DbConnectionInternal> retry)
		{
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (this.ForceNewConnection)
				{
					if (!this.InnerConnection.TryReplaceConnection(this, this.ConnectionFactory, retry, this.UserConnectionOptions))
					{
						return false;
					}
				}
				else if (!this.InnerConnection.TryOpenConnection(this, this.ConnectionFactory, retry, this.UserConnectionOptions))
				{
					return false;
				}
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this);
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds == null)
				{
					SqlInternalConnectionSmi sqlInternalConnectionSmi = this.InnerConnection as SqlInternalConnectionSmi;
					sqlInternalConnectionSmi.AutomaticEnlistment();
				}
				else
				{
					if (!sqlInternalConnectionTds.ConnectionOptions.Pooling)
					{
						GC.ReRegisterForFinalize(this);
					}
					if (this.StatisticsEnabled)
					{
						this._statistics._openTimestamp = ADP.TimerCurrent();
						sqlInternalConnectionTds.Parser.Statistics = this._statistics;
					}
					else
					{
						sqlInternalConnectionTds.Parser.Statistics = null;
						this._statistics = null;
					}
					this.CompleteOpen();
				}
			}
			catch (OutOfMemoryException ex)
			{
				this.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			return true;
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0003E044 File Offset: 0x0003C244
		internal bool HasLocalTransaction
		{
			get
			{
				return this.GetOpenConnection().HasLocalTransaction;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0003E054 File Offset: 0x0003C254
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				return (currentReconnectionTask == null || currentReconnectionTask.IsCompleted) && this.GetOpenConnection().HasLocalTransactionFromAPI;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0003E080 File Offset: 0x0003C280
		internal bool Is2000
		{
			get
			{
				return this._currentReconnectionTask != null || this.GetOpenConnection().Is2000;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0003E097 File Offset: 0x0003C297
		internal bool Is2005OrNewer
		{
			get
			{
				return this._currentReconnectionTask != null || this.GetOpenConnection().Is2005OrNewer;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0003E0AE File Offset: 0x0003C2AE
		internal bool Is2008OrNewer
		{
			get
			{
				return this._currentReconnectionTask != null || this.GetOpenConnection().Is2008OrNewer;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0003E0C8 File Offset: 0x0003C2C8
		internal TdsParser Parser
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.GetOpenConnection() as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds == null)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				return sqlInternalConnectionTds.Parser;
			}
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0003E0F0 File Offset: 0x0003C2F0
		internal void ValidateConnectionForExecute(string method, SqlCommand command)
		{
			Task asyncWaitingForReconnection = this._asyncWaitingForReconnection;
			if (asyncWaitingForReconnection != null)
			{
				if (!asyncWaitingForReconnection.IsCompleted)
				{
					throw SQL.MARSUnsupportedOnConnection();
				}
				Interlocked.CompareExchange<Task>(ref this._asyncWaitingForReconnection, null, asyncWaitingForReconnection);
			}
			if (this._currentReconnectionTask != null)
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return;
				}
			}
			SqlInternalConnection openConnection = this.GetOpenConnection(method);
			openConnection.ValidateConnectionForExecute(command);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0003E14D File Offset: 0x0003C34D
		internal static string FixupDatabaseTransactionName(string name)
		{
			if (!ADP.IsEmpty(name))
			{
				return SqlServerEscapeHelper.EscapeIdentifier(name);
			}
			return name;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0003E160 File Offset: 0x0003C360
		internal void OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction)
		{
			if (breakConnection && ConnectionState.Open == this.State)
			{
				if (wrapCloseInAction != null)
				{
					int capturedCloseCount = this._closeCount;
					Action action = delegate
					{
						if (capturedCloseCount == this._closeCount)
						{
							SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlConnection.OnError|INFO> {0}, Connection broken.", this.ObjectID);
							this.Close();
						}
					};
					wrapCloseInAction(action);
				}
				else
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlConnection.OnError|INFO> {0}, Connection broken.", this.ObjectID);
					this.Close();
				}
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
			this.OnInfoMessage(new SqlInfoMessageEventArgs(exception));
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0003E1E0 File Offset: 0x0003C3E0
		private void CompleteOpen()
		{
			if (!this.GetOpenConnection().Is2005OrNewer && Debugger.IsAttached)
			{
				bool flag = false;
				try
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
					flag = true;
				}
				catch (SecurityException ex)
				{
					ADP.TraceExceptionWithoutRethrow(ex);
				}
				if (flag)
				{
					this.CheckSQLDebugOnConnect();
				}
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0003E234 File Offset: 0x0003C434
		internal SqlInternalConnection GetOpenConnection()
		{
			SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
			if (sqlInternalConnection == null)
			{
				throw ADP.ClosedConnectionError();
			}
			return sqlInternalConnection;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0003E258 File Offset: 0x0003C458
		internal SqlInternalConnection GetOpenConnection(string method)
		{
			DbConnectionInternal innerConnection = this.InnerConnection;
			SqlInternalConnection sqlInternalConnection = innerConnection as SqlInternalConnection;
			if (sqlInternalConnection == null)
			{
				throw ADP.OpenConnectionRequired(method, innerConnection.State);
			}
			return sqlInternalConnection;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0003E284 File Offset: 0x0003C484
		internal SqlInternalConnectionTds GetOpenTdsConnection()
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds == null)
			{
				throw ADP.ClosedConnectionError();
			}
			return sqlInternalConnectionTds;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0003E2A8 File Offset: 0x0003C4A8
		internal SqlInternalConnectionTds GetOpenTdsConnection(string method)
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds == null)
			{
				throw ADP.OpenConnectionRequired(method, this.InnerConnection.State);
			}
			return sqlInternalConnectionTds;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0003E2D8 File Offset: 0x0003C4D8
		internal void OnInfoMessage(SqlInfoMessageEventArgs imevent)
		{
			bool flag;
			this.OnInfoMessage(imevent, out flag);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0003E2F0 File Offset: 0x0003C4F0
		internal void OnInfoMessage(SqlInfoMessageEventArgs imevent, out bool notified)
		{
			string text = ((imevent != null) ? imevent.Message : "");
			SqlClientEventSource.Log.TryTraceEvent<int, string>("<sc.SqlConnection.OnInfoMessage|API|INFO> {0}, Message='{1}'", this.ObjectID, text);
			SqlInfoMessageEventHandler sqlInfoMessageEventHandler = (SqlInfoMessageEventHandler)base.Events[SqlConnection.EventInfoMessage];
			if (sqlInfoMessageEventHandler != null)
			{
				notified = true;
				try
				{
					sqlInfoMessageEventHandler(this, imevent);
					return;
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(ex))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(ex);
					return;
				}
			}
			notified = false;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0003E370 File Offset: 0x0003C570
		private void CheckSQLDebugOnConnect()
		{
			uint currentProcessId = (uint)SafeNativeMethods.GetCurrentProcessId();
			string text;
			if (ADP.s_isPlatformNT5)
			{
				text = "Global\\SqlClientSSDebug";
			}
			else
			{
				text = "SqlClientSSDebug";
			}
			text += currentProcessId.ToString(CultureInfo.InvariantCulture);
			IntPtr intPtr = NativeMethods.OpenFileMappingA(4, false, text);
			if (ADP.s_ptrZero != intPtr)
			{
				IntPtr intPtr2 = NativeMethods.MapViewOfFile(intPtr, 4, 0, 0, IntPtr.Zero);
				if (ADP.s_ptrZero != intPtr2)
				{
					SqlDebugContext sqlDebugContext = new SqlDebugContext();
					sqlDebugContext.hMemMap = intPtr;
					sqlDebugContext.pMemMap = intPtr2;
					sqlDebugContext.pid = currentProcessId;
					this.CheckSQLDebug(sqlDebugContext);
					this._sdc = sqlDebugContext;
				}
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0003E40C File Offset: 0x0003C60C
		internal void CheckSQLDebug()
		{
			if (this._sdc != null)
			{
				this.CheckSQLDebug(this._sdc);
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0003E424 File Offset: 0x0003C624
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private void CheckSQLDebug(SqlDebugContext sdc)
		{
			uint currentThreadId = (uint)AppDomain.GetCurrentThreadId();
			SqlConnection.RefreshMemoryMappedData(sdc);
			if (!sdc.active && sdc.fOption)
			{
				sdc.active = true;
				sdc.tid = currentThreadId;
				try
				{
					this.IssueSQLDebug(1U, sdc.machineName, sdc.pid, sdc.dbgpid, sdc.sdiDllName, sdc.data);
					sdc.tid = 0U;
				}
				catch
				{
					sdc.active = false;
					throw;
				}
			}
			if (sdc.active)
			{
				if (!sdc.fOption)
				{
					sdc.Dispose();
					this.IssueSQLDebug(0U, null, 0U, 0U, null, null);
					return;
				}
				if (sdc.tid != currentThreadId)
				{
					sdc.tid = currentThreadId;
					try
					{
						this.IssueSQLDebug(2U, null, sdc.pid, sdc.tid, null, null);
					}
					catch
					{
						sdc.tid = 0U;
						throw;
					}
				}
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0003E504 File Offset: 0x0003C704
		private void IssueSQLDebug(uint option, string machineName, uint pid, uint id, string sdiDllName, byte[] data)
		{
			if (this.GetOpenConnection().Is2005OrNewer)
			{
				return;
			}
			SqlCommand sqlCommand = new SqlCommand("sp_sdidebug", this);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlParameter sqlParameter = new SqlParameter(null, SqlDbType.VarChar, TdsEnums.SQLDEBUG_MODE_NAMES[(int)option].Length);
			sqlParameter.Value = TdsEnums.SQLDEBUG_MODE_NAMES[(int)option];
			sqlCommand.Parameters.Add(sqlParameter);
			if (option == 1U)
			{
				sqlParameter = new SqlParameter(null, SqlDbType.VarChar, sdiDllName.Length);
				sqlParameter.Value = sdiDllName;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter(null, SqlDbType.VarChar, machineName.Length);
				sqlParameter.Value = machineName;
				sqlCommand.Parameters.Add(sqlParameter);
			}
			if (option != 0U)
			{
				sqlParameter = new SqlParameter(null, SqlDbType.Int);
				sqlParameter.Value = pid;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter(null, SqlDbType.Int);
				sqlParameter.Value = id;
				sqlCommand.Parameters.Add(sqlParameter);
			}
			if (option == 1U)
			{
				sqlParameter = new SqlParameter(null, SqlDbType.VarBinary, (data != null) ? data.Length : 0);
				sqlParameter.Value = data;
				sqlCommand.Parameters.Add(sqlParameter);
			}
			sqlCommand.ExecuteNonQuery();
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0003E628 File Offset: 0x0003C828
		public static void ChangePassword(string connectionString, string newPassword)
		{
			using (TryEventScope.Create("<sc.SqlConnection.ChangePassword|API>", "ChangePassword"))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<ActivityCorrelator.ActivityId>("<sc.SqlConnection.ChangePassword|API|Correlation> ActivityID {0}", ActivityCorrelator.Current);
				if (ADP.IsEmpty(connectionString))
				{
					throw SQL.ChangePasswordArgumentMissing("connectionString");
				}
				if (ADP.IsEmpty(newPassword))
				{
					throw SQL.ChangePasswordArgumentMissing("newPassword");
				}
				if (128 < newPassword.Length)
				{
					throw ADP.InvalidArgumentLength("newPassword", 128);
				}
				SqlConnectionPoolKey sqlConnectionPoolKey = new SqlConnectionPoolKey(connectionString, null, null, null, null, null);
				SqlConnectionString sqlConnectionString = SqlConnectionFactory.FindSqlConnectionOptions(sqlConnectionPoolKey);
				if (sqlConnectionString.IntegratedSecurity || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated)
				{
					throw SQL.ChangePasswordConflictsWithSSPI();
				}
				if (!ADP.IsEmpty(sqlConnectionString.AttachDBFilename))
				{
					throw SQL.ChangePasswordUseOfUnallowedKey("AttachDbFilename");
				}
				if (sqlConnectionString.ContextConnection)
				{
					throw SQL.ChangePasswordUseOfUnallowedKey("Context Connection");
				}
				PermissionSet permissionSet = sqlConnectionString.CreatePermissionSet();
				permissionSet.Demand();
				SqlConnection.ChangePassword(connectionString, sqlConnectionString, null, newPassword, null);
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0003E724 File Offset: 0x0003C924
		public static void ChangePassword(string connectionString, SqlCredential credential, SecureString newSecurePassword)
		{
			using (TryEventScope.Create("<sc.SqlConnection.ChangePassword|API>", "ChangePassword"))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<ActivityCorrelator.ActivityId>("<sc.SqlConnection.ChangePassword|API|Correlation> ActivityID {0}", ActivityCorrelator.Current);
				if (ADP.IsEmpty(connectionString))
				{
					throw SQL.ChangePasswordArgumentMissing("connectionString");
				}
				if (credential == null)
				{
					throw SQL.ChangePasswordArgumentMissing("credential");
				}
				if (newSecurePassword == null || newSecurePassword.Length == 0)
				{
					throw SQL.ChangePasswordArgumentMissing("newSecurePassword");
				}
				if (!newSecurePassword.IsReadOnly())
				{
					throw ADP.MustBeReadOnly("newSecurePassword");
				}
				if (128 < newSecurePassword.Length)
				{
					throw ADP.InvalidArgumentLength("newSecurePassword", 128);
				}
				SqlConnectionPoolKey sqlConnectionPoolKey = new SqlConnectionPoolKey(connectionString, credential, null, null, null, null);
				SqlConnectionString sqlConnectionString = SqlConnectionFactory.FindSqlConnectionOptions(sqlConnectionPoolKey);
				if (!ADP.IsEmpty(sqlConnectionString.UserID) || !ADP.IsEmpty(sqlConnectionString.Password))
				{
					throw ADP.InvalidMixedArgumentOfSecureAndClearCredential();
				}
				if (sqlConnectionString.IntegratedSecurity || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated)
				{
					throw SQL.ChangePasswordConflictsWithSSPI();
				}
				if (!ADP.IsEmpty(sqlConnectionString.AttachDBFilename))
				{
					throw SQL.ChangePasswordUseOfUnallowedKey("AttachDbFilename");
				}
				if (sqlConnectionString.ContextConnection)
				{
					throw SQL.ChangePasswordUseOfUnallowedKey("Context Connection");
				}
				PermissionSet permissionSet = sqlConnectionString.CreatePermissionSet();
				permissionSet.Demand();
				SqlConnection.ChangePassword(connectionString, sqlConnectionString, credential, null, newSecurePassword);
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0003E870 File Offset: 0x0003CA70
		private static void ChangePassword(string connectionString, SqlConnectionString connectionOptions, SqlCredential credential, string newPassword, SecureString newSecurePassword)
		{
			using (SqlInternalConnectionTds sqlInternalConnectionTds = new SqlInternalConnectionTds(null, connectionOptions, credential, null, newPassword, newSecurePassword, false, null, null, null, null, null, null, null, false))
			{
				if (!sqlInternalConnectionTds.Is2005OrNewer)
				{
					throw SQL.ChangePasswordRequires2005();
				}
			}
			SqlConnectionPoolKey sqlConnectionPoolKey = new SqlConnectionPoolKey(connectionString, credential, null, null, null, null);
			SqlConnectionFactory.SingletonInstance.ClearPool(sqlConnectionPoolKey);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0003E8D4 File Offset: 0x0003CAD4
		internal void RegisterForConnectionCloseNotification<T>(ref Task<T> outerTask, object value, int tag)
		{
			outerTask = outerTask.ContinueWith<Task<T>>(delegate(Task<T> task)
			{
				this.RemoveWeakReference(value);
				return task;
			}, TaskScheduler.Default).Unwrap<T>();
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0003E914 File Offset: 0x0003CB14
		private static void RefreshMemoryMappedData(SqlDebugContext sdc)
		{
			MEMMAP memmap = (MEMMAP)Marshal.PtrToStructure(sdc.pMemMap, typeof(MEMMAP));
			sdc.dbgpid = memmap.dbgpid;
			sdc.fOption = memmap.fOption == 1U;
			Encoding encoding = Encoding.GetEncoding(1252);
			sdc.machineName = encoding.GetString(memmap.rgbMachineName, 0, memmap.rgbMachineName.Length);
			sdc.sdiDllName = encoding.GetString(memmap.rgbDllName, 0, memmap.rgbDllName.Length);
			sdc.data = memmap.rgbData;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0003E9A4 File Offset: 0x0003CBA4
		public void ResetStatistics()
		{
			if (this.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			if (this.Statistics != null)
			{
				this.Statistics.Reset();
				if (ConnectionState.Open == this.State)
				{
					this._statistics._openTimestamp = ADP.TimerCurrent();
				}
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0003E9E0 File Offset: 0x0003CBE0
		public IDictionary RetrieveStatistics()
		{
			if (this.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			if (this.Statistics != null)
			{
				this.UpdateStatistics();
				return this.Statistics.GetDictionary();
			}
			return new SqlStatistics().GetDictionary();
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0003EA14 File Offset: 0x0003CC14
		private void UpdateStatistics()
		{
			if (ConnectionState.Open == this.State)
			{
				this._statistics._closeTimestamp = ADP.TimerCurrent();
			}
			this.Statistics.UpdateStatistics();
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0003EA3C File Offset: 0x0003CC3C
		public IDictionary<string, object> RetrieveInternalInfo()
		{
			return new Dictionary<string, object>
			{
				{ "SQLDNSCachingSupportedState", this.SQLDNSCachingSupportedState },
				{ "SQLDNSCachingSupportedStateBeforeRedirect", this.SQLDNSCachingSupportedStateBeforeRedirect }
			};
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0003EA74 File Offset: 0x0003CC74
		private Assembly ResolveTypeAssembly(AssemblyName asmRef, bool throwOnError)
		{
			if (string.Compare(asmRef.Name, "Microsoft.SqlServer.Types", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (asmRef.Version != this.TypeSystemAssemblyVersion && SqlClientEventSource.Log.IsTraceEnabled())
				{
					SqlClientEventSource.Log.TryTraceEvent<Version, Version>("<sc.SqlConnection.ResolveTypeAssembly> SQL CLR type version change: Server sent {0}, client will instantiate {1}", asmRef.Version, this.TypeSystemAssemblyVersion);
				}
				asmRef.Version = this.TypeSystemAssemblyVersion;
			}
			Assembly assembly;
			try
			{
				assembly = Assembly.Load(asmRef);
			}
			catch (Exception ex)
			{
				if (throwOnError || !ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				assembly = null;
			}
			return assembly;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0003EB08 File Offset: 0x0003CD08
		internal void CheckGetExtendedUDTInfo(SqlMetaDataPriv metaData, bool fThrow)
		{
			SqlMetaDataUdt udt = metaData.udt;
			if (((udt != null) ? udt.Type : null) == null)
			{
				metaData.udt.Type = Type.GetType(metaData.udt.AssemblyQualifiedName, (AssemblyName asmRef) => this.ResolveTypeAssembly(asmRef, fThrow), null, fThrow);
				if (fThrow && metaData.udt.Type == null)
				{
					throw SQL.UDTUnexpectedResult(metaData.udt.AssemblyQualifiedName);
				}
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0003EBA0 File Offset: 0x0003CDA0
		internal object GetUdtValue(object value, SqlMetaDataPriv metaData, bool returnDBNull)
		{
			if (returnDBNull && ADP.IsNull(value))
			{
				return DBNull.Value;
			}
			if (ADP.IsNull(value))
			{
				SqlMetaDataUdt udt = metaData.udt;
				Type type = ((udt != null) ? udt.Type : null);
				return type.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, new object[0], CultureInfo.InvariantCulture);
			}
			MemoryStream memoryStream = new MemoryStream((byte[])value);
			Stream stream = memoryStream;
			SqlMetaDataUdt udt2 = metaData.udt;
			return SerializationHelperSql9.Deserialize(stream, (udt2 != null) ? udt2.Type : null);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0003EC24 File Offset: 0x0003CE24
		internal byte[] GetBytes(object o)
		{
			Format format = Format.Native;
			int num = 0;
			return this.GetBytes(o, out format, out num);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0003EC40 File Offset: 0x0003CE40
		internal byte[] GetBytes(object o, out Format format, out int maxSize)
		{
			SqlUdtInfo infoFromType = AssemblyCache.GetInfoFromType(o.GetType());
			maxSize = infoFromType.MaxByteSize;
			format = infoFromType.SerializationFormat;
			if (maxSize < -1 || maxSize >= 65535)
			{
				Type type = o.GetType();
				throw new InvalidOperationException(((type != null) ? type.ToString() : null) + ": invalid Size");
			}
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream((maxSize < 0) ? 0 : maxSize))
			{
				SerializationHelperSql9.Serialize(memoryStream, o);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003ECD4 File Offset: 0x0003CED4
		public SqlConnection()
		{
			this._reconnectLock = new object();
			this._originalConnectionId = Guid.Empty;
			this.ObjectID = Interlocked.Increment(ref SqlConnection._objectTypeCount);
			base..ctor();
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0003ED14 File Offset: 0x0003CF14
		private void CopyFrom(SqlConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			this._userConnectionOptions = connection.UserConnectionOptions;
			this._poolGroup = connection.PoolGroup;
			if (DbConnectionClosedNeverOpened.SingletonInstance == connection._innerConnection)
			{
				this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				return;
			}
			this._innerConnection = DbConnectionClosedPreviouslyOpened.SingletonInstance;
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0003ED68 File Offset: 0x0003CF68
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0003ED70 File Offset: 0x0003CF70
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return SqlConnection._connectionFactory;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0003ED78 File Offset: 0x0003CF78
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				DbConnectionPoolGroup poolGroup = this.PoolGroup;
				if (poolGroup == null)
				{
					return null;
				}
				return poolGroup.ConnectionOptions;
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003ED98 File Offset: 0x0003CF98
		private string ConnectionString_Get()
		{
			SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionHelper.ConnectionString_Get|API> {0}", this.ObjectID);
			bool shouldHidePassword = this.InnerConnection.ShouldHidePassword;
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			if (userConnectionOptions == null)
			{
				return "";
			}
			return userConnectionOptions.UsersConnectionString(shouldHidePassword);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003EDE0 File Offset: 0x0003CFE0
		private void ConnectionString_Set(string value)
		{
			DbConnectionPoolKey dbConnectionPoolKey = new DbConnectionPoolKey(value);
			this.ConnectionString_Set(dbConnectionPoolKey);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0003EDFC File Offset: 0x0003CFFC
		private void ConnectionString_Set(DbConnectionPoolKey key)
		{
			DbConnectionOptions dbConnectionOptions = null;
			DbConnectionPoolGroup connectionPoolGroup = this.ConnectionFactory.GetConnectionPoolGroup(key, null, ref dbConnectionOptions);
			DbConnectionInternal innerConnection = this.InnerConnection;
			bool flag = innerConnection.AllowSetConnectionString;
			if (flag)
			{
				flag = this.SetInnerConnectionFrom(DbConnectionClosedBusy.SingletonInstance, innerConnection);
				if (flag)
				{
					this._userConnectionOptions = dbConnectionOptions;
					this._poolGroup = connectionPoolGroup;
					this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				}
			}
			if (!flag)
			{
				throw ADP.OpenConnectionPropertySet("ConnectionString", innerConnection.State);
			}
			if (SqlClientEventSource.Log.IsTraceEnabled())
			{
				SqlClientEventSource.Log.TraceEvent<int, string>("<prov.DbConnectionHelper.ConnectionString_Set|API> {0}, '{1}'", this.ObjectID, dbConnectionOptions.UsersConnectionStringForTrace());
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0003EE90 File Offset: 0x0003D090
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0003EE98 File Offset: 0x0003D098
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x0003EEA0 File Offset: 0x0003D0A0
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0003EEA9 File Offset: 0x0003D0A9
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0003EEB4 File Offset: 0x0003D0B4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Abort(Exception e)
		{
			DbConnectionInternal innerConnection = this._innerConnection;
			if (ConnectionState.Open == innerConnection.State)
			{
				Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, DbConnectionClosedPreviouslyOpened.SingletonInstance, innerConnection);
				innerConnection.DoomThisConnection();
			}
			if (e is OutOfMemoryException)
			{
				SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionHelper.Abort|RES|INFO|CPOOL> {0}, Aborting operation due to asynchronous exception: {'OutOfMemory'}", this.ObjectID);
				return;
			}
			SqlClientEventSource.Log.TryTraceEvent<int, Exception>("<prov.DbConnectionHelper.Abort|RES|INFO|CPOOL> {0}, Aborting operation due to asynchronous exception: {1}", this.ObjectID, e);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0003EF1D File Offset: 0x0003D11D
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0003EF2C File Offset: 0x0003D12C
		protected override DbCommand CreateDbCommand()
		{
			DbCommand dbCommand2;
			using (TryEventScope.Create<int>("<prov.DbConnectionHelper.CreateDbCommand|API> {0}", this.ObjectID))
			{
				DbProviderFactory providerFactory = this.ConnectionFactory.ProviderFactory;
				DbCommand dbCommand = providerFactory.CreateCommand();
				dbCommand.Connection = this;
				dbCommand2 = dbCommand;
			}
			return dbCommand2;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0003EF84 File Offset: 0x0003D184
		private static CodeAccessPermission CreateExecutePermission()
		{
			DBDataPermission dbdataPermission = (DBDataPermission)SqlConnectionFactory.SingletonInstance.ProviderFactory.CreatePermission(PermissionState.None);
			dbdataPermission.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
			return dbdataPermission;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003EFB9 File Offset: 0x0003D1B9
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._userConnectionOptions = null;
				this._poolGroup = null;
				this.Close();
			}
			this.DisposeMe(disposing);
			base.Dispose(disposing);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0003EFE0 File Offset: 0x0003D1E0
		private void RepairInnerConnection()
		{
			this.WaitForPendingReconnection();
			if (this._connectRetryCount == 0)
			{
				return;
			}
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds != null)
			{
				sqlInternalConnectionTds.ValidateConnectionForExecute(null);
				sqlInternalConnectionTds.GetSessionAndReconnectIfNeeded(this, 0);
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0003F01C File Offset: 0x0003D21C
		private void EnlistDistributedTransactionHelper(ITransaction transaction)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(SqlConnection.ExecutePermission);
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.Demand();
			SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionHelper.EnlistDistributedTransactionHelper|RES|TRAN> {0}, Connection enlisting in a transaction.", this.ObjectID);
			Transaction transaction2 = null;
			if (transaction != null)
			{
				transaction2 = TransactionInterop.GetTransactionFromDtcTransaction((IDtcTransaction)transaction);
			}
			this.RepairInnerConnection();
			this.InnerConnection.EnlistTransaction(transaction2);
			GC.KeepAlive(this);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0003F090 File Offset: 0x0003D290
		public override void EnlistTransaction(Transaction transaction)
		{
			SqlConnection.ExecutePermission.Demand();
			SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionHelper.EnlistTransaction|RES|TRAN> {0}, Connection enlisting in a transaction.", this.ObjectID);
			DbConnectionInternal innerConnection = this.InnerConnection;
			Transaction enlistedTransaction = innerConnection.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				if (enlistedTransaction.Equals(transaction))
				{
					return;
				}
				if (enlistedTransaction.TransactionInformation.Status == global::System.Transactions.TransactionStatus.Active)
				{
					throw ADP.TransactionPresent();
				}
			}
			this.RepairInnerConnection();
			this.InnerConnection.EnlistTransaction(transaction);
			GC.KeepAlive(this);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0003F108 File Offset: 0x0003D308
		private DbMetaDataFactory GetMetaDataFactory(DbConnectionInternal internalConnection)
		{
			return this.ConnectionFactory.GetMetaDataFactory(this._poolGroup, internalConnection);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0003F11C File Offset: 0x0003D31C
		internal DbMetaDataFactory GetMetaDataFactoryInternal(DbConnectionInternal internalConnection)
		{
			return this.GetMetaDataFactory(internalConnection);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0003F125 File Offset: 0x0003D325
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0003F133 File Offset: 0x0003D333
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0003F13D File Offset: 0x0003D33D
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			SqlConnection.ExecutePermission.Demand();
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0003F163 File Offset: 0x0003D363
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0003F174 File Offset: 0x0003D374
		internal void PermissionDemand()
		{
			DbConnectionPoolGroup poolGroup = this.PoolGroup;
			DbConnectionOptions dbConnectionOptions = ((poolGroup != null) ? poolGroup.ConnectionOptions : null);
			if (dbConnectionOptions == null || dbConnectionOptions.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			userConnectionOptions.DemandPermission();
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0003F1B3 File Offset: 0x0003D3B3
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0003F1C4 File Offset: 0x0003D3C4
		internal void SetInnerConnectionEvent(DbConnectionInternal to)
		{
			ConnectionState connectionState = this._innerConnection.State & ConnectionState.Open;
			ConnectionState connectionState2 = to.State & ConnectionState.Open;
			if (connectionState != connectionState2 && connectionState2 == ConnectionState.Closed)
			{
				this._closeCount++;
			}
			this._innerConnection = to;
			if (connectionState == ConnectionState.Closed && ConnectionState.Open == connectionState2)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeOpen);
				return;
			}
			if (ConnectionState.Open == connectionState && connectionState2 == ConnectionState.Closed)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeClosed);
				return;
			}
			if (connectionState != connectionState2)
			{
				this.OnStateChange(new StateChangeEventArgs(connectionState, connectionState2));
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0003F23C File Offset: 0x0003D43C
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0003F25B File Offset: 0x0003D45B
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0003F264 File Offset: 0x0003D464
		[Conditional("DEBUG")]
		internal static void VerifyExecutePermission()
		{
			try
			{
				SqlConnection.ExecutePermission.Demand();
			}
			catch (SecurityException)
			{
				throw;
			}
		}

		// Token: 0x040006E6 RID: 1766
		internal bool _suppressStateChangeForReconnection;

		// Token: 0x040006E7 RID: 1767
		private static readonly object EventInfoMessage = new object();

		// Token: 0x040006E8 RID: 1768
		private static Dictionary<string, SqlColumnEncryptionKeyStoreProvider> s_systemColumnEncryptionKeyStoreProviders = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>(3, StringComparer.OrdinalIgnoreCase)
		{
			{
				"MSSQL_CERTIFICATE_STORE",
				new SqlColumnEncryptionCertificateStoreProvider()
			},
			{
				"MSSQL_CNG_STORE",
				new SqlColumnEncryptionCngProvider()
			},
			{
				"MSSQL_CSP_PROVIDER",
				new SqlColumnEncryptionCspProvider()
			}
		};

		// Token: 0x040006E9 RID: 1769
		private static IReadOnlyDictionary<string, SqlColumnEncryptionKeyStoreProvider> s_globalCustomColumnEncryptionKeyStoreProviders;

		// Token: 0x040006EA RID: 1770
		private IReadOnlyDictionary<string, SqlColumnEncryptionKeyStoreProvider> _customColumnEncryptionKeyStoreProviders;

		// Token: 0x040006EB RID: 1771
		private static readonly object s_globalCustomColumnEncryptionKeyProvidersLock = new object();

		// Token: 0x040006EC RID: 1772
		private static readonly ConcurrentDictionary<string, IList<string>> _ColumnEncryptionTrustedMasterKeyPaths = new ConcurrentDictionary<string, IList<string>>(4 * Environment.ProcessorCount, 1, StringComparer.OrdinalIgnoreCase);

		// Token: 0x040006ED RID: 1773
		private static bool _ColumnEncryptionQueryMetadataCacheEnabled = true;

		// Token: 0x040006EE RID: 1774
		private static TimeSpan _ColumnEncryptionKeyCacheTtl = TimeSpan.FromHours(2.0);

		// Token: 0x040006EF RID: 1775
		private SqlDebugContext _sdc;

		// Token: 0x040006F0 RID: 1776
		private bool _AsyncCommandInProgress;

		// Token: 0x040006F1 RID: 1777
		internal SqlStatistics _statistics;

		// Token: 0x040006F2 RID: 1778
		private bool _collectstats;

		// Token: 0x040006F3 RID: 1779
		private bool _fireInfoMessageEventOnUserErrors;

		// Token: 0x040006F4 RID: 1780
		private Tuple<TaskCompletionSource<DbConnectionInternal>, Task> _currentCompletion;

		// Token: 0x040006F5 RID: 1781
		private SqlCredential _credential;

		// Token: 0x040006F6 RID: 1782
		private string _connectionString;

		// Token: 0x040006F7 RID: 1783
		private int _connectRetryCount;

		// Token: 0x040006F8 RID: 1784
		private string _accessToken;

		// Token: 0x040006F9 RID: 1785
		private object _reconnectLock;

		// Token: 0x040006FA RID: 1786
		internal Task _currentReconnectionTask;

		// Token: 0x040006FB RID: 1787
		private Task _asyncWaitingForReconnection;

		// Token: 0x040006FC RID: 1788
		private Guid _originalConnectionId;

		// Token: 0x040006FD RID: 1789
		private CancellationTokenSource _reconnectionCancellationSource;

		// Token: 0x040006FE RID: 1790
		internal SessionData _recoverySessionData;

		// Token: 0x040006FF RID: 1791
		internal WindowsIdentity _lastIdentity;

		// Token: 0x04000700 RID: 1792
		internal WindowsIdentity _impersonateIdentity;

		// Token: 0x04000701 RID: 1793
		private int _reconnectCount;

		// Token: 0x04000702 RID: 1794
		private ServerCertificateValidationCallback _serverCertificateValidationCallback;

		// Token: 0x04000703 RID: 1795
		private ClientCertificateRetrievalCallback _clientCertificateRetrievalCallback;

		// Token: 0x04000704 RID: 1796
		private SqlClientOriginalNetworkAddressInfo _originalNetworkAddressInfo;

		// Token: 0x04000705 RID: 1797
		private SqlRetryLogicBaseProvider _retryLogicProvider;

		// Token: 0x04000706 RID: 1798
		internal bool _applyTransientFaultHandling;

		// Token: 0x04000707 RID: 1799
		private static readonly DbConnectionFactory _connectionFactory = SqlConnectionFactory.SingletonInstance;

		// Token: 0x04000708 RID: 1800
		internal static readonly CodeAccessPermission ExecutePermission = SqlConnection.CreateExecutePermission();

		// Token: 0x04000709 RID: 1801
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x0400070A RID: 1802
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x0400070B RID: 1803
		private DbConnectionInternal _innerConnection;

		// Token: 0x0400070C RID: 1804
		private int _closeCount;

		// Token: 0x0400070D RID: 1805
		private static int _objectTypeCount;

		// Token: 0x0400070E RID: 1806
		internal readonly int ObjectID;

		// Token: 0x02000230 RID: 560
		private class OpenAsyncRetry
		{
			// Token: 0x06001E7E RID: 7806 RVA: 0x0007CFA1 File Offset: 0x0007B1A1
			public OpenAsyncRetry(SqlConnection parent, TaskCompletionSource<DbConnectionInternal> retry, TaskCompletionSource<object> result, CancellationTokenRegistration registration)
			{
				this._parent = parent;
				this._retry = retry;
				this._result = result;
				this._registration = registration;
			}

			// Token: 0x06001E7F RID: 7807 RVA: 0x0007CFC8 File Offset: 0x0007B1C8
			internal void Retry(Task<DbConnectionInternal> retryTask)
			{
				SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlConnection.OpenAsyncRetry|Info> {0}", this._parent.ObjectID);
				this._registration.Dispose();
				try
				{
					SqlStatistics sqlStatistics = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						sqlStatistics = SqlStatistics.StartTimer(this._parent.Statistics);
						if (retryTask.IsFaulted)
						{
							Exception innerException = retryTask.Exception.InnerException;
							this._parent.CloseInnerConnection();
							this._parent._currentCompletion = null;
							this._result.SetException(retryTask.Exception.InnerException);
						}
						else if (retryTask.IsCanceled)
						{
							this._parent.CloseInnerConnection();
							this._parent._currentCompletion = null;
							this._result.SetCanceled();
						}
						else
						{
							DbConnectionInternal innerConnection = this._parent.InnerConnection;
							bool flag2;
							lock (innerConnection)
							{
								flag2 = this._parent.TryOpen(this._retry, SqlConnectionOverrides.None);
							}
							if (flag2)
							{
								this._parent._currentCompletion = null;
								this._result.SetResult(null);
							}
							else
							{
								this._parent.CloseInnerConnection();
								this._parent._currentCompletion = null;
								this._result.SetException(ADP.ExceptionWithStackTrace(ADP.InternalError(ADP.InternalErrorCode.CompletedConnectReturnedPending)));
							}
						}
					}
					finally
					{
						SqlStatistics.StopTimer(sqlStatistics);
					}
				}
				catch (Exception ex)
				{
					this._parent.CloseInnerConnection();
					this._parent._currentCompletion = null;
					this._result.SetException(ex);
				}
			}

			// Token: 0x04001622 RID: 5666
			private SqlConnection _parent;

			// Token: 0x04001623 RID: 5667
			private TaskCompletionSource<DbConnectionInternal> _retry;

			// Token: 0x04001624 RID: 5668
			private TaskCompletionSource<object> _result;

			// Token: 0x04001625 RID: 5669
			private CancellationTokenRegistration _registration;
		}
	}
}
