using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Globalization;
using System.Reflection;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200006D RID: 109
	[DefaultProperty("Data Source")]
	[TypeConverter(typeof(SqlConnectionStringBuilder.SqlConnectionStringBuilderConverter))]
	public sealed class SqlConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x060009AD RID: 2477 RVA: 0x00019994 File Offset: 0x00017B94
		private static string[] CreateValidKeywords()
		{
			string[] array = new string[44];
			array[29] = "Application Intent";
			array[24] = "Application Name";
			array[2] = "AttachDbFilename";
			array[12] = "Pool Blocking Period";
			array[36] = "Command Timeout";
			array[15] = "Connect Timeout";
			array[25] = "Current Language";
			array[0] = "Data Source";
			array[16] = "Encrypt";
			array[17] = "Host Name In Certificate";
			array[18] = "Server Certificate";
			array[8] = "Enlist";
			array[1] = "Failover Partner";
			array[3] = "Initial Catalog";
			array[4] = "Integrated Security";
			array[20] = "Load Balance Timeout";
			array[11] = "Max Pool Size";
			array[10] = "Min Pool Size";
			array[13] = "Multiple Active Result Sets";
			array[30] = "Multi Subnet Failover";
			array[21] = "Packet Size";
			array[7] = "Password";
			array[5] = "Persist Security Info";
			array[9] = "Pooling";
			array[14] = "Replication";
			array[28] = "Transaction Binding";
			array[19] = "Trust Server Certificate";
			array[22] = "Type System Version";
			array[6] = "User ID";
			array[27] = "User Instance";
			array[26] = "Workstation ID";
			array[31] = "Connect Retry Count";
			array[32] = "Connect Retry Interval";
			array[23] = "Authentication";
			array[33] = "Column Encryption Setting";
			array[34] = "Enclave Attestation Url";
			array[35] = "Attestation Protocol";
			array[37] = "IP Address Preference";
			array[38] = "Server SPN";
			array[39] = "Failover Partner SPN";
			array[40] = "Connection Reset";
			array[41] = "Network Library";
			array[42] = "Context Connection";
			array[43] = "Transparent Network IP Resolution";
			return array;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00019B30 File Offset: 0x00017D30
		private static Dictionary<string, SqlConnectionStringBuilder.Keywords> CreateKeywordsDictionary()
		{
			return new Dictionary<string, SqlConnectionStringBuilder.Keywords>(77, StringComparer.OrdinalIgnoreCase)
			{
				{
					"Application Intent",
					SqlConnectionStringBuilder.Keywords.ApplicationIntent
				},
				{
					"Application Name",
					SqlConnectionStringBuilder.Keywords.ApplicationName
				},
				{
					"AttachDbFilename",
					SqlConnectionStringBuilder.Keywords.AttachDBFilename
				},
				{
					"Pool Blocking Period",
					SqlConnectionStringBuilder.Keywords.PoolBlockingPeriod
				},
				{
					"Command Timeout",
					SqlConnectionStringBuilder.Keywords.CommandTimeout
				},
				{
					"Connect Timeout",
					SqlConnectionStringBuilder.Keywords.ConnectTimeout
				},
				{
					"Current Language",
					SqlConnectionStringBuilder.Keywords.CurrentLanguage
				},
				{
					"Data Source",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"Encrypt",
					SqlConnectionStringBuilder.Keywords.Encrypt
				},
				{
					"Enlist",
					SqlConnectionStringBuilder.Keywords.Enlist
				},
				{
					"Failover Partner",
					SqlConnectionStringBuilder.Keywords.FailoverPartner
				},
				{
					"Host Name In Certificate",
					SqlConnectionStringBuilder.Keywords.HostNameInCertificate
				},
				{
					"Server Certificate",
					SqlConnectionStringBuilder.Keywords.ServerCertificate
				},
				{
					"Initial Catalog",
					SqlConnectionStringBuilder.Keywords.InitialCatalog
				},
				{
					"Integrated Security",
					SqlConnectionStringBuilder.Keywords.IntegratedSecurity
				},
				{
					"Load Balance Timeout",
					SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout
				},
				{
					"Multiple Active Result Sets",
					SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets
				},
				{
					"Max Pool Size",
					SqlConnectionStringBuilder.Keywords.MaxPoolSize
				},
				{
					"Min Pool Size",
					SqlConnectionStringBuilder.Keywords.MinPoolSize
				},
				{
					"Multi Subnet Failover",
					SqlConnectionStringBuilder.Keywords.MultiSubnetFailover
				},
				{
					"Packet Size",
					SqlConnectionStringBuilder.Keywords.PacketSize
				},
				{
					"Password",
					SqlConnectionStringBuilder.Keywords.Password
				},
				{
					"Persist Security Info",
					SqlConnectionStringBuilder.Keywords.PersistSecurityInfo
				},
				{
					"Pooling",
					SqlConnectionStringBuilder.Keywords.Pooling
				},
				{
					"Replication",
					SqlConnectionStringBuilder.Keywords.Replication
				},
				{
					"Transaction Binding",
					SqlConnectionStringBuilder.Keywords.TransactionBinding
				},
				{
					"Trust Server Certificate",
					SqlConnectionStringBuilder.Keywords.TrustServerCertificate
				},
				{
					"Type System Version",
					SqlConnectionStringBuilder.Keywords.TypeSystemVersion
				},
				{
					"User ID",
					SqlConnectionStringBuilder.Keywords.UserID
				},
				{
					"User Instance",
					SqlConnectionStringBuilder.Keywords.UserInstance
				},
				{
					"Workstation ID",
					SqlConnectionStringBuilder.Keywords.WorkstationID
				},
				{
					"Connect Retry Count",
					SqlConnectionStringBuilder.Keywords.ConnectRetryCount
				},
				{
					"Connect Retry Interval",
					SqlConnectionStringBuilder.Keywords.ConnectRetryInterval
				},
				{
					"Authentication",
					SqlConnectionStringBuilder.Keywords.Authentication
				},
				{
					"Column Encryption Setting",
					SqlConnectionStringBuilder.Keywords.ColumnEncryptionSetting
				},
				{
					"Enclave Attestation Url",
					SqlConnectionStringBuilder.Keywords.EnclaveAttestationUrl
				},
				{
					"Attestation Protocol",
					SqlConnectionStringBuilder.Keywords.AttestationProtocol
				},
				{
					"IP Address Preference",
					SqlConnectionStringBuilder.Keywords.IPAddressPreference
				},
				{
					"Server SPN",
					SqlConnectionStringBuilder.Keywords.ServerSPN
				},
				{
					"Failover Partner SPN",
					SqlConnectionStringBuilder.Keywords.FailoverPartnerSPN
				},
				{
					"Connection Reset",
					SqlConnectionStringBuilder.Keywords.ConnectionReset
				},
				{
					"Context Connection",
					SqlConnectionStringBuilder.Keywords.ContextConnection
				},
				{
					"Transparent Network IP Resolution",
					SqlConnectionStringBuilder.Keywords.TransparentNetworkIPResolution
				},
				{
					"Network Library",
					SqlConnectionStringBuilder.Keywords.NetworkLibrary
				},
				{
					"net",
					SqlConnectionStringBuilder.Keywords.NetworkLibrary
				},
				{
					"network",
					SqlConnectionStringBuilder.Keywords.NetworkLibrary
				},
				{
					"transparentnetworkipresolution",
					SqlConnectionStringBuilder.Keywords.TransparentNetworkIPResolution
				},
				{
					"ipaddresspreference",
					SqlConnectionStringBuilder.Keywords.IPAddressPreference
				},
				{
					"app",
					SqlConnectionStringBuilder.Keywords.ApplicationName
				},
				{
					"applicationintent",
					SqlConnectionStringBuilder.Keywords.ApplicationIntent
				},
				{
					"extended properties",
					SqlConnectionStringBuilder.Keywords.AttachDBFilename
				},
				{
					"hostnameincertificate",
					SqlConnectionStringBuilder.Keywords.HostNameInCertificate
				},
				{
					"servercertificate",
					SqlConnectionStringBuilder.Keywords.ServerCertificate
				},
				{
					"initial file name",
					SqlConnectionStringBuilder.Keywords.AttachDBFilename
				},
				{
					"connection timeout",
					SqlConnectionStringBuilder.Keywords.ConnectTimeout
				},
				{
					"connectretrycount",
					SqlConnectionStringBuilder.Keywords.ConnectRetryCount
				},
				{
					"connectretryinterval",
					SqlConnectionStringBuilder.Keywords.ConnectRetryInterval
				},
				{
					"timeout",
					SqlConnectionStringBuilder.Keywords.ConnectTimeout
				},
				{
					"language",
					SqlConnectionStringBuilder.Keywords.CurrentLanguage
				},
				{
					"addr",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"address",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"multipleactiveresultsets",
					SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets
				},
				{
					"multisubnetfailover",
					SqlConnectionStringBuilder.Keywords.MultiSubnetFailover
				},
				{
					"network address",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"poolblockingperiod",
					SqlConnectionStringBuilder.Keywords.PoolBlockingPeriod
				},
				{
					"server",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"database",
					SqlConnectionStringBuilder.Keywords.InitialCatalog
				},
				{
					"trusted_connection",
					SqlConnectionStringBuilder.Keywords.IntegratedSecurity
				},
				{
					"trustservercertificate",
					SqlConnectionStringBuilder.Keywords.TrustServerCertificate
				},
				{
					"connection lifetime",
					SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout
				},
				{
					"pwd",
					SqlConnectionStringBuilder.Keywords.Password
				},
				{
					"persistsecurityinfo",
					SqlConnectionStringBuilder.Keywords.PersistSecurityInfo
				},
				{
					"uid",
					SqlConnectionStringBuilder.Keywords.UserID
				},
				{
					"user",
					SqlConnectionStringBuilder.Keywords.UserID
				},
				{
					"wsid",
					SqlConnectionStringBuilder.Keywords.WorkstationID
				},
				{
					"ServerSPN",
					SqlConnectionStringBuilder.Keywords.ServerSPN
				},
				{
					"FailoverPartnerSPN",
					SqlConnectionStringBuilder.Keywords.FailoverPartnerSPN
				}
			};
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00019F1F File Offset: 0x0001811F
		private static bool ConvertToBoolean(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToBoolean(value);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00019F27 File Offset: 0x00018127
		private static int ConvertToInt32(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToInt32(value);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00019F2F File Offset: 0x0001812F
		private static bool ConvertToIntegratedSecurity(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToIntegratedSecurity(value);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00019F37 File Offset: 0x00018137
		private static SqlAuthenticationMethod ConvertToAuthenticationType(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToAuthenticationType(keyword, value);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00019F40 File Offset: 0x00018140
		private static string ConvertToString(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToString(value);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00019F48 File Offset: 0x00018148
		private static ApplicationIntent ConvertToApplicationIntent(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToApplicationIntent(keyword, value);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00019F51 File Offset: 0x00018151
		private static SqlConnectionColumnEncryptionSetting ConvertToColumnEncryptionSetting(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToColumnEncryptionSetting(keyword, value);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00019F5A File Offset: 0x0001815A
		private static SqlConnectionAttestationProtocol ConvertToAttestationProtocol(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToAttestationProtocol(keyword, value);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00019F63 File Offset: 0x00018163
		private static SqlConnectionEncryptOption ConvertToSqlConnectionEncryptOption(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToSqlConnectionEncryptOption(keyword, value);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00019F6C File Offset: 0x0001816C
		private static SqlConnectionIPAddressPreference ConvertToIPAddressPreference(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToIPAddressPreference(keyword, value);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00019F75 File Offset: 0x00018175
		private static PoolBlockingPeriod ConvertToPoolBlockingPeriod(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToPoolBlockingPeriod(keyword, value);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00019F80 File Offset: 0x00018180
		private object GetAt(SqlConnectionStringBuilder.Keywords index)
		{
			switch (index)
			{
			case SqlConnectionStringBuilder.Keywords.DataSource:
				return this.DataSource;
			case SqlConnectionStringBuilder.Keywords.FailoverPartner:
				return this.FailoverPartner;
			case SqlConnectionStringBuilder.Keywords.AttachDBFilename:
				return this.AttachDBFilename;
			case SqlConnectionStringBuilder.Keywords.InitialCatalog:
				return this.InitialCatalog;
			case SqlConnectionStringBuilder.Keywords.IntegratedSecurity:
				return this.IntegratedSecurity;
			case SqlConnectionStringBuilder.Keywords.PersistSecurityInfo:
				return this.PersistSecurityInfo;
			case SqlConnectionStringBuilder.Keywords.UserID:
				return this.UserID;
			case SqlConnectionStringBuilder.Keywords.Password:
				return this.Password;
			case SqlConnectionStringBuilder.Keywords.Enlist:
				return this.Enlist;
			case SqlConnectionStringBuilder.Keywords.Pooling:
				return this.Pooling;
			case SqlConnectionStringBuilder.Keywords.MinPoolSize:
				return this.MinPoolSize;
			case SqlConnectionStringBuilder.Keywords.MaxPoolSize:
				return this.MaxPoolSize;
			case SqlConnectionStringBuilder.Keywords.PoolBlockingPeriod:
				return this.PoolBlockingPeriod;
			case SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets:
				return this.MultipleActiveResultSets;
			case SqlConnectionStringBuilder.Keywords.Replication:
				return this.Replication;
			case SqlConnectionStringBuilder.Keywords.ConnectTimeout:
				return this.ConnectTimeout;
			case SqlConnectionStringBuilder.Keywords.Encrypt:
				return this.Encrypt;
			case SqlConnectionStringBuilder.Keywords.HostNameInCertificate:
				return this.HostNameInCertificate;
			case SqlConnectionStringBuilder.Keywords.ServerCertificate:
				return this.ServerCertificate;
			case SqlConnectionStringBuilder.Keywords.TrustServerCertificate:
				return this.TrustServerCertificate;
			case SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout:
				return this.LoadBalanceTimeout;
			case SqlConnectionStringBuilder.Keywords.PacketSize:
				return this.PacketSize;
			case SqlConnectionStringBuilder.Keywords.TypeSystemVersion:
				return this.TypeSystemVersion;
			case SqlConnectionStringBuilder.Keywords.Authentication:
				return this.Authentication;
			case SqlConnectionStringBuilder.Keywords.ApplicationName:
				return this.ApplicationName;
			case SqlConnectionStringBuilder.Keywords.CurrentLanguage:
				return this.CurrentLanguage;
			case SqlConnectionStringBuilder.Keywords.WorkstationID:
				return this.WorkstationID;
			case SqlConnectionStringBuilder.Keywords.UserInstance:
				return this.UserInstance;
			case SqlConnectionStringBuilder.Keywords.TransactionBinding:
				return this.TransactionBinding;
			case SqlConnectionStringBuilder.Keywords.ApplicationIntent:
				return this.ApplicationIntent;
			case SqlConnectionStringBuilder.Keywords.MultiSubnetFailover:
				return this.MultiSubnetFailover;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryCount:
				return this.ConnectRetryCount;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryInterval:
				return this.ConnectRetryInterval;
			case SqlConnectionStringBuilder.Keywords.ColumnEncryptionSetting:
				return this.ColumnEncryptionSetting;
			case SqlConnectionStringBuilder.Keywords.EnclaveAttestationUrl:
				return this.EnclaveAttestationUrl;
			case SqlConnectionStringBuilder.Keywords.AttestationProtocol:
				return this.AttestationProtocol;
			case SqlConnectionStringBuilder.Keywords.CommandTimeout:
				return this.CommandTimeout;
			case SqlConnectionStringBuilder.Keywords.IPAddressPreference:
				return this.IPAddressPreference;
			case SqlConnectionStringBuilder.Keywords.ServerSPN:
				return this.ServerSPN;
			case SqlConnectionStringBuilder.Keywords.FailoverPartnerSPN:
				return this.FailoverPartnerSPN;
			case SqlConnectionStringBuilder.Keywords.ConnectionReset:
				return this.ConnectionReset;
			case SqlConnectionStringBuilder.Keywords.NetworkLibrary:
				return this.NetworkLibrary;
			case SqlConnectionStringBuilder.Keywords.ContextConnection:
				return this.ContextConnection;
			case SqlConnectionStringBuilder.Keywords.TransparentNetworkIPResolution:
				return this.TransparentNetworkIPResolution;
			default:
				throw this.UnsupportedKeyword(SqlConnectionStringBuilder.s_validKeywords[(int)index]);
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001A20C File Offset: 0x0001840C
		private SqlConnectionStringBuilder.Keywords GetIndex(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			SqlConnectionStringBuilder.Keywords keywords;
			if (SqlConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords))
			{
				return keywords;
			}
			throw this.UnsupportedKeyword(keyword);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001A23C File Offset: 0x0001843C
		private void Reset(SqlConnectionStringBuilder.Keywords index)
		{
			switch (index)
			{
			case SqlConnectionStringBuilder.Keywords.DataSource:
				this._dataSource = "";
				return;
			case SqlConnectionStringBuilder.Keywords.FailoverPartner:
				this._failoverPartner = "";
				return;
			case SqlConnectionStringBuilder.Keywords.AttachDBFilename:
				this._attachDBFilename = "";
				return;
			case SqlConnectionStringBuilder.Keywords.InitialCatalog:
				this._initialCatalog = "";
				return;
			case SqlConnectionStringBuilder.Keywords.IntegratedSecurity:
				this._integratedSecurity = false;
				return;
			case SqlConnectionStringBuilder.Keywords.PersistSecurityInfo:
				this._persistSecurityInfo = false;
				return;
			case SqlConnectionStringBuilder.Keywords.UserID:
				this._userID = "";
				return;
			case SqlConnectionStringBuilder.Keywords.Password:
				this._password = "";
				return;
			case SqlConnectionStringBuilder.Keywords.Enlist:
				this._enlist = true;
				return;
			case SqlConnectionStringBuilder.Keywords.Pooling:
				this._pooling = true;
				return;
			case SqlConnectionStringBuilder.Keywords.MinPoolSize:
				this._minPoolSize = 0;
				return;
			case SqlConnectionStringBuilder.Keywords.MaxPoolSize:
				this._maxPoolSize = 100;
				return;
			case SqlConnectionStringBuilder.Keywords.PoolBlockingPeriod:
				this._poolBlockingPeriod = PoolBlockingPeriod.Auto;
				return;
			case SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets:
				this._multipleActiveResultSets = false;
				return;
			case SqlConnectionStringBuilder.Keywords.Replication:
				this._replication = false;
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectTimeout:
				this._connectTimeout = 15;
				return;
			case SqlConnectionStringBuilder.Keywords.Encrypt:
				this._encrypt = DbConnectionStringDefaults.Encrypt;
				return;
			case SqlConnectionStringBuilder.Keywords.HostNameInCertificate:
				this._hostNameInCertificate = "";
				return;
			case SqlConnectionStringBuilder.Keywords.ServerCertificate:
				this._serverCertificate = "";
				return;
			case SqlConnectionStringBuilder.Keywords.TrustServerCertificate:
				this._trustServerCertificate = false;
				return;
			case SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout:
				this._loadBalanceTimeout = 0;
				return;
			case SqlConnectionStringBuilder.Keywords.PacketSize:
				this._packetSize = 8000;
				return;
			case SqlConnectionStringBuilder.Keywords.TypeSystemVersion:
				this._typeSystemVersion = "Latest";
				return;
			case SqlConnectionStringBuilder.Keywords.Authentication:
				this._authentication = DbConnectionStringDefaults.Authentication;
				return;
			case SqlConnectionStringBuilder.Keywords.ApplicationName:
				this._applicationName = "Framework Microsoft SqlClient Data Provider";
				return;
			case SqlConnectionStringBuilder.Keywords.CurrentLanguage:
				this._currentLanguage = "";
				return;
			case SqlConnectionStringBuilder.Keywords.WorkstationID:
				this._workstationID = "";
				return;
			case SqlConnectionStringBuilder.Keywords.UserInstance:
				this._userInstance = false;
				return;
			case SqlConnectionStringBuilder.Keywords.TransactionBinding:
				this._transactionBinding = "Implicit Unbind";
				return;
			case SqlConnectionStringBuilder.Keywords.ApplicationIntent:
				this._applicationIntent = ApplicationIntent.ReadWrite;
				return;
			case SqlConnectionStringBuilder.Keywords.MultiSubnetFailover:
				this._multiSubnetFailover = false;
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryCount:
				this._connectRetryCount = 1;
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryInterval:
				this._connectRetryInterval = 10;
				return;
			case SqlConnectionStringBuilder.Keywords.ColumnEncryptionSetting:
				this._columnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Disabled;
				return;
			case SqlConnectionStringBuilder.Keywords.EnclaveAttestationUrl:
				this._enclaveAttestationUrl = "";
				return;
			case SqlConnectionStringBuilder.Keywords.AttestationProtocol:
				this._attestationProtocol = SqlConnectionAttestationProtocol.NotSpecified;
				return;
			case SqlConnectionStringBuilder.Keywords.CommandTimeout:
				this._commandTimeout = 30;
				return;
			case SqlConnectionStringBuilder.Keywords.IPAddressPreference:
				this._ipAddressPreference = SqlConnectionIPAddressPreference.IPv4First;
				return;
			case SqlConnectionStringBuilder.Keywords.ServerSPN:
				this._serverSPN = "";
				return;
			case SqlConnectionStringBuilder.Keywords.FailoverPartnerSPN:
				this._failoverPartnerSPN = "";
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectionReset:
				this._connectionReset = true;
				return;
			case SqlConnectionStringBuilder.Keywords.NetworkLibrary:
				this._networkLibrary = "";
				return;
			case SqlConnectionStringBuilder.Keywords.ContextConnection:
				this._contextConnection = false;
				return;
			case SqlConnectionStringBuilder.Keywords.TransparentNetworkIPResolution:
				this._transparentNetworkIPResolution = DbConnectionStringDefaults.TransparentNetworkIPResolution;
				return;
			default:
				throw this.UnsupportedKeyword(SqlConnectionStringBuilder.s_validKeywords[(int)index]);
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001A4C9 File Offset: 0x000186C9
		private void SetValue(string keyword, bool value)
		{
			base[keyword] = value.ToString();
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001A4D9 File Offset: 0x000186D9
		private void SetValue(string keyword, int value)
		{
			base[keyword] = value.ToString(null);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001A4EA File Offset: 0x000186EA
		private void SetValue(string keyword, string value)
		{
			ADP.CheckArgumentNull(value, keyword);
			base[keyword] = value;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001A4FB File Offset: 0x000186FB
		private void SetApplicationIntentValue(ApplicationIntent value)
		{
			base["Application Intent"] = DbConnectionStringBuilderUtil.ApplicationIntentToString(value);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001A50E File Offset: 0x0001870E
		private void SetColumnEncryptionSettingValue(SqlConnectionColumnEncryptionSetting value)
		{
			base["Column Encryption Setting"] = DbConnectionStringBuilderUtil.ColumnEncryptionSettingToString(value);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001A521 File Offset: 0x00018721
		private void SetAttestationProtocolValue(SqlConnectionAttestationProtocol value)
		{
			base["Attestation Protocol"] = DbConnectionStringBuilderUtil.AttestationProtocolToString(value);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001A534 File Offset: 0x00018734
		private void SetSqlConnectionEncryptionValue(SqlConnectionEncryptOption value)
		{
			base["Encrypt"] = value.ToString();
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001A547 File Offset: 0x00018747
		private void SetIPAddressPreferenceValue(SqlConnectionIPAddressPreference value)
		{
			base["IP Address Preference"] = DbConnectionStringBuilderUtil.IPAddressPreferenceToString(value);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001A55A File Offset: 0x0001875A
		private void SetAuthenticationValue(SqlAuthenticationMethod value)
		{
			base["Authentication"] = DbConnectionStringBuilderUtil.AuthenticationTypeToString(value);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001A56D File Offset: 0x0001876D
		private void SetPoolBlockingPeriodValue(PoolBlockingPeriod value)
		{
			base["Pool Blocking Period"] = DbConnectionStringBuilderUtil.PoolBlockingPeriodToString(value);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001A580 File Offset: 0x00018780
		private Exception UnsupportedKeyword(string keyword)
		{
			return ADP.KeywordNotSupported(keyword);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001A588 File Offset: 0x00018788
		public SqlConnectionStringBuilder()
			: this(null)
		{
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001A594 File Offset: 0x00018794
		public SqlConnectionStringBuilder(string connectionString)
		{
			if (!string.IsNullOrEmpty(connectionString))
			{
				base.ConnectionString = connectionString;
			}
		}

		// Token: 0x170006CE RID: 1742
		public override object this[string keyword]
		{
			get
			{
				return this.GetAt(this.GetIndex(keyword));
			}
			set
			{
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				switch (this.GetIndex(keyword))
				{
				case SqlConnectionStringBuilder.Keywords.DataSource:
					this.DataSource = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.FailoverPartner:
					this.FailoverPartner = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.AttachDBFilename:
					this.AttachDBFilename = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.InitialCatalog:
					this.InitialCatalog = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.IntegratedSecurity:
					this.IntegratedSecurity = SqlConnectionStringBuilder.ConvertToIntegratedSecurity(value);
					return;
				case SqlConnectionStringBuilder.Keywords.PersistSecurityInfo:
					this.PersistSecurityInfo = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.UserID:
					this.UserID = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Password:
					this.Password = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Enlist:
					this.Enlist = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Pooling:
					this.Pooling = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.MinPoolSize:
					this.MinPoolSize = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.MaxPoolSize:
					this.MaxPoolSize = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.PoolBlockingPeriod:
					this.PoolBlockingPeriod = SqlConnectionStringBuilder.ConvertToPoolBlockingPeriod(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets:
					this.MultipleActiveResultSets = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Replication:
					this.Replication = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectTimeout:
					this.ConnectTimeout = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Encrypt:
					this.Encrypt = SqlConnectionStringBuilder.ConvertToSqlConnectionEncryptOption(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.HostNameInCertificate:
					this.HostNameInCertificate = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ServerCertificate:
					this.ServerCertificate = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TrustServerCertificate:
					this.TrustServerCertificate = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout:
					this.LoadBalanceTimeout = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.PacketSize:
					this.PacketSize = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TypeSystemVersion:
					this.TypeSystemVersion = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Authentication:
					this.Authentication = SqlConnectionStringBuilder.ConvertToAuthenticationType(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.ApplicationName:
					this.ApplicationName = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.CurrentLanguage:
					this.CurrentLanguage = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.WorkstationID:
					this.WorkstationID = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.UserInstance:
					this.UserInstance = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TransactionBinding:
					this.TransactionBinding = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ApplicationIntent:
					this.ApplicationIntent = SqlConnectionStringBuilder.ConvertToApplicationIntent(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.MultiSubnetFailover:
					this.MultiSubnetFailover = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectRetryCount:
					this.ConnectRetryCount = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectRetryInterval:
					this.ConnectRetryInterval = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ColumnEncryptionSetting:
					this.ColumnEncryptionSetting = SqlConnectionStringBuilder.ConvertToColumnEncryptionSetting(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.EnclaveAttestationUrl:
					this.EnclaveAttestationUrl = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.AttestationProtocol:
					this.AttestationProtocol = SqlConnectionStringBuilder.ConvertToAttestationProtocol(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.CommandTimeout:
					this.CommandTimeout = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.IPAddressPreference:
					this.IPAddressPreference = SqlConnectionStringBuilder.ConvertToIPAddressPreference(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.ServerSPN:
					this.ServerSPN = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.FailoverPartnerSPN:
					this.FailoverPartnerSPN = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectionReset:
					this.ConnectionReset = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.NetworkLibrary:
					this.NetworkLibrary = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ContextConnection:
					this.ContextConnection = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TransparentNetworkIPResolution:
					this.TransparentNetworkIPResolution = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				default:
					throw this.UnsupportedKeyword(keyword);
				}
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0001AA11 File Offset: 0x00018C11
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x0001AA19 File Offset: 0x00018C19
		[DisplayName("Application Intent")]
		[ResCategory("Initialization")]
		[ResDescription("Declares the application workload type when connecting to a server.")]
		[RefreshProperties(RefreshProperties.All)]
		public ApplicationIntent ApplicationIntent
		{
			get
			{
				return this._applicationIntent;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidApplicationIntentValue(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(ApplicationIntent), (int)value);
				}
				this.SetApplicationIntentValue(value);
				this._applicationIntent = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0001AA42 File Offset: 0x00018C42
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x0001AA4A File Offset: 0x00018C4A
		[DisplayName("Application Name")]
		[ResCategory("Context")]
		[ResDescription("The name of the application.")]
		[RefreshProperties(RefreshProperties.All)]
		public string ApplicationName
		{
			get
			{
				return this._applicationName;
			}
			set
			{
				this.SetValue("Application Name", value);
				this._applicationName = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0001AA5F File Offset: 0x00018C5F
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0001AA67 File Offset: 0x00018C67
		[DisplayName("AttachDbFilename")]
		[ResCategory("Source")]
		[ResDescription("The name of the primary file, including the full path name, of an attachable database.")]
		[Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RefreshProperties(RefreshProperties.All)]
		public string AttachDBFilename
		{
			get
			{
				return this._attachDBFilename;
			}
			set
			{
				this.SetValue("AttachDbFilename", value);
				this._attachDBFilename = value;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0001AA7C File Offset: 0x00018C7C
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x0001AA84 File Offset: 0x00018C84
		[DisplayName("Command Timeout")]
		[ResCategory("Initialization")]
		[ResDescription("Time to wait for command to execute.")]
		[RefreshProperties(RefreshProperties.All)]
		public int CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidConnectionOptionValue("Command Timeout");
				}
				this.SetValue("Command Timeout", value);
				this._commandTimeout = value;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		[DisplayName("Connect Timeout")]
		[ResCategory("Initialization")]
		[ResDescription("The length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error.")]
		[RefreshProperties(RefreshProperties.All)]
		public int ConnectTimeout
		{
			get
			{
				return this._connectTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidConnectionOptionValue("Connect Timeout");
				}
				this.SetValue("Connect Timeout", value);
				this._connectTimeout = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001AAD4 File Offset: 0x00018CD4
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0001AADC File Offset: 0x00018CDC
		[DisplayName("Current Language")]
		[ResCategory("Initialization")]
		[ResDescription("The SQL Server Language record name.")]
		[RefreshProperties(RefreshProperties.All)]
		public string CurrentLanguage
		{
			get
			{
				return this._currentLanguage;
			}
			set
			{
				this.SetValue("Current Language", value);
				this._currentLanguage = value;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0001AAF1 File Offset: 0x00018CF1
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x0001AAF9 File Offset: 0x00018CF9
		[DisplayName("Data Source")]
		[ResCategory("Source")]
		[ResDescription("Indicates the name of the data source to connect to.")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(SqlConnectionStringBuilder.SqlDataSourceConverter))]
		public string DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				this.SetValue("Data Source", value);
				this._dataSource = value;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0001AB0E File Offset: 0x00018D0E
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x0001AB16 File Offset: 0x00018D16
		[DisplayName("Server SPN")]
		[ResCategory("Source")]
		[ResDescription("The service principal name (SPN) of the server.")]
		[RefreshProperties(RefreshProperties.All)]
		public string ServerSPN
		{
			get
			{
				return this._serverSPN;
			}
			set
			{
				this.SetValue("Server SPN", value);
				this._serverSPN = value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0001AB2B File Offset: 0x00018D2B
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x0001AB34 File Offset: 0x00018D34
		[DisplayName("Encrypt")]
		[ResCategory("Security")]
		[ResDescription("When true, SQL Server uses SSL encryption for all data sent between the client and server if the server has a certificate installed.")]
		[RefreshProperties(RefreshProperties.All)]
		public SqlConnectionEncryptOption Encrypt
		{
			get
			{
				return this._encrypt;
			}
			set
			{
				SqlConnectionEncryptOption sqlConnectionEncryptOption = value ?? DbConnectionStringDefaults.Encrypt;
				this.SetSqlConnectionEncryptionValue(sqlConnectionEncryptOption);
				this._encrypt = sqlConnectionEncryptOption;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0001AB5A File Offset: 0x00018D5A
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x0001AB62 File Offset: 0x00018D62
		[DisplayName("Host Name In Certificate")]
		[ResCategory("Security")]
		[ResDescription("The hostname to be expected in the server's certificate when encryption is negotiated, if it's different from the default value derived from Addr/Address/Server.")]
		[RefreshProperties(RefreshProperties.All)]
		public string HostNameInCertificate
		{
			get
			{
				return this._hostNameInCertificate;
			}
			set
			{
				this.SetValue("Host Name In Certificate", value);
				this._hostNameInCertificate = value;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0001AB77 File Offset: 0x00018D77
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x0001AB7F File Offset: 0x00018D7F
		[DisplayName("Server Certificate")]
		[ResCategory("Security")]
		[ResDescription("The path to a certificate file to match against the SQL Server TLS/SSL certificate.")]
		[RefreshProperties(RefreshProperties.All)]
		public string ServerCertificate
		{
			get
			{
				return this._serverCertificate;
			}
			set
			{
				this.SetValue("Server Certificate", value);
				this._serverCertificate = value;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001AB94 File Offset: 0x00018D94
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0001AB9C File Offset: 0x00018D9C
		[DisplayName("Column Encryption Setting")]
		[ResCategory("Security")]
		[ResDescription("Default column encryption setting for all the commands on the connection.")]
		[RefreshProperties(RefreshProperties.All)]
		public SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting
		{
			get
			{
				return this._columnEncryptionSetting;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidColumnEncryptionSetting(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(SqlConnectionColumnEncryptionSetting), (int)value);
				}
				this.SetColumnEncryptionSettingValue(value);
				this._columnEncryptionSetting = value;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0001ABC5 File Offset: 0x00018DC5
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0001ABCD File Offset: 0x00018DCD
		[DisplayName("Enclave Attestation Url")]
		[ResCategory("Security")]
		[ResDescription("Specifies an endpoint of an enclave attestation service, which will be used to verify whether the enclave, configured in the SQL Server instance for computations on database columns encrypted using Always Encrypted, is valid and secure.")]
		[RefreshProperties(RefreshProperties.All)]
		public string EnclaveAttestationUrl
		{
			get
			{
				return this._enclaveAttestationUrl;
			}
			set
			{
				this.SetValue("Enclave Attestation Url", value);
				this._enclaveAttestationUrl = value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001ABE2 File Offset: 0x00018DE2
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0001ABEA File Offset: 0x00018DEA
		[DisplayName("Attestation Protocol")]
		[ResCategory("Security")]
		[ResDescription("Specifies an attestation protocol for its corresponding enclave attestation service.")]
		[RefreshProperties(RefreshProperties.All)]
		public SqlConnectionAttestationProtocol AttestationProtocol
		{
			get
			{
				return this._attestationProtocol;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidAttestationProtocol(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(SqlConnectionAttestationProtocol), (int)value);
				}
				this.SetAttestationProtocolValue(value);
				this._attestationProtocol = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0001AC13 File Offset: 0x00018E13
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0001AC1B File Offset: 0x00018E1B
		[DisplayName("IP Address Preference")]
		[ResCategory("Security")]
		[ResDescription("Specifies an IP address preference when connecting to SQL instances.")]
		[RefreshProperties(RefreshProperties.All)]
		public SqlConnectionIPAddressPreference IPAddressPreference
		{
			get
			{
				return this._ipAddressPreference;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidIPAddressPreference(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(SqlConnectionIPAddressPreference), (int)value);
				}
				this.SetIPAddressPreferenceValue(value);
				this._ipAddressPreference = value;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0001AC44 File Offset: 0x00018E44
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x0001AC4C File Offset: 0x00018E4C
		[DisplayName("Trust Server Certificate")]
		[ResCategory("Security")]
		[ResDescription("When true (and encrypt=true), SQL Server uses SSL encryption for all data sent between the client and server without validating the server certificate.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool TrustServerCertificate
		{
			get
			{
				return this._trustServerCertificate;
			}
			set
			{
				this.SetValue("Trust Server Certificate", value);
				this._trustServerCertificate = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0001AC61 File Offset: 0x00018E61
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x0001AC69 File Offset: 0x00018E69
		[DisplayName("Enlist")]
		[ResCategory("Pooling")]
		[ResDescription("Sessions in a Component Services (or MTS, if you are using Microsoft Windows NT) environment should automatically be enlisted in a global transaction where required.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Enlist
		{
			get
			{
				return this._enlist;
			}
			set
			{
				this.SetValue("Enlist", value);
				this._enlist = value;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0001AC7E File Offset: 0x00018E7E
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0001AC86 File Offset: 0x00018E86
		[DisplayName("Failover Partner")]
		[ResCategory("Source")]
		[ResDescription("The name or network address of the instance of SQL Server that acts as a failover partner.")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(SqlConnectionStringBuilder.SqlDataSourceConverter))]
		public string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
			set
			{
				this.SetValue("Failover Partner", value);
				this._failoverPartner = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0001AC9B File Offset: 0x00018E9B
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0001ACA3 File Offset: 0x00018EA3
		[DisplayName("Failover Partner SPN")]
		[ResCategory("Source")]
		[ResDescription("The service principal name (SPN) of the failover partner.")]
		[RefreshProperties(RefreshProperties.All)]
		public string FailoverPartnerSPN
		{
			get
			{
				return this._failoverPartnerSPN;
			}
			set
			{
				this.SetValue("Failover Partner SPN", value);
				this._failoverPartnerSPN = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0001ACB8 File Offset: 0x00018EB8
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0001ACC0 File Offset: 0x00018EC0
		[DisplayName("Initial Catalog")]
		[ResCategory("Source")]
		[ResDescription("The name of the initial catalog or database in the data source.")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(SqlConnectionStringBuilder.SqlInitialCatalogConverter))]
		public string InitialCatalog
		{
			get
			{
				return this._initialCatalog;
			}
			set
			{
				this.SetValue("Initial Catalog", value);
				this._initialCatalog = value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x0001ACDD File Offset: 0x00018EDD
		[DisplayName("Integrated Security")]
		[ResCategory("Security")]
		[ResDescription("Whether the connection is to be a secure connection or not.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool IntegratedSecurity
		{
			get
			{
				return this._integratedSecurity;
			}
			set
			{
				this.SetValue("Integrated Security", value);
				this._integratedSecurity = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0001ACF2 File Offset: 0x00018EF2
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x0001ACFA File Offset: 0x00018EFA
		[DisplayName("Authentication")]
		[ResCategory("Security")]
		[ResDescription("Specifies the method of authenticating with SQL Server.")]
		[RefreshProperties(RefreshProperties.All)]
		public SqlAuthenticationMethod Authentication
		{
			get
			{
				return this._authentication;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidAuthenticationTypeValue(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(SqlAuthenticationMethod), (int)value);
				}
				this.SetAuthenticationValue(value);
				this._authentication = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0001AD23 File Offset: 0x00018F23
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x0001AD2B File Offset: 0x00018F2B
		[DisplayName("Load Balance Timeout")]
		[ResCategory("Pooling")]
		[ResDescription("The minimum amount of time (in seconds) for this connection to live in the pool before being destroyed.")]
		[RefreshProperties(RefreshProperties.All)]
		public int LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidConnectionOptionValue("Load Balance Timeout");
				}
				this.SetValue("Load Balance Timeout", value);
				this._loadBalanceTimeout = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0001AD4F File Offset: 0x00018F4F
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x0001AD57 File Offset: 0x00018F57
		[DisplayName("Max Pool Size")]
		[ResCategory("Pooling")]
		[ResDescription("The maximum number of connections allowed in the pool.")]
		[RefreshProperties(RefreshProperties.All)]
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
			set
			{
				if (value < 1)
				{
					throw ADP.InvalidConnectionOptionValue("Max Pool Size");
				}
				this.SetValue("Max Pool Size", value);
				this._maxPoolSize = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0001AD7B File Offset: 0x00018F7B
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x0001AD83 File Offset: 0x00018F83
		[DisplayName("Connect Retry Count")]
		[ResCategory("Connection Resiliency")]
		[ResDescription("Number of attempts to restore connection.")]
		[RefreshProperties(RefreshProperties.All)]
		public int ConnectRetryCount
		{
			get
			{
				return this._connectRetryCount;
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw ADP.InvalidConnectionOptionValue("Connect Retry Count");
				}
				this.SetValue("Connect Retry Count", value);
				this._connectRetryCount = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0001ADAF File Offset: 0x00018FAF
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x0001ADB7 File Offset: 0x00018FB7
		[DisplayName("Connect Retry Interval")]
		[ResCategory("Connection Resiliency")]
		[ResDescription("Delay between attempts to restore connection.")]
		[RefreshProperties(RefreshProperties.All)]
		public int ConnectRetryInterval
		{
			get
			{
				return this._connectRetryInterval;
			}
			set
			{
				if (value < 1 || value > 60)
				{
					throw ADP.InvalidConnectionOptionValue("Connect Retry Interval");
				}
				this.SetValue("Connect Retry Interval", value);
				this._connectRetryInterval = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0001ADE0 File Offset: 0x00018FE0
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		[DisplayName("Min Pool Size")]
		[ResCategory("Pooling")]
		[ResDescription("The minimum number of connections allowed in the pool.")]
		[RefreshProperties(RefreshProperties.All)]
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidConnectionOptionValue("Min Pool Size");
				}
				this.SetValue("Min Pool Size", value);
				this._minPoolSize = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0001AE0C File Offset: 0x0001900C
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0001AE14 File Offset: 0x00019014
		[DisplayName("Multiple Active Result Sets")]
		[ResCategory("Advanced")]
		[ResDescription("When true, multiple result sets can be returned and read from one connection.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool MultipleActiveResultSets
		{
			get
			{
				return this._multipleActiveResultSets;
			}
			set
			{
				this.SetValue("Multiple Active Result Sets", value);
				this._multipleActiveResultSets = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0001AE29 File Offset: 0x00019029
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0001AE31 File Offset: 0x00019031
		[DisplayName("Multi Subnet Failover")]
		[ResCategory("Source")]
		[ResDescription("If your application is connecting to a high-availability, disaster recovery (AlwaysOn) availability group (AG) on different subnets, MultiSubnetFailover=Yes configures SqlConnection to provide faster detection of and connection to the (currently) active server.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool MultiSubnetFailover
		{
			get
			{
				return this._multiSubnetFailover;
			}
			set
			{
				this.SetValue("Multi Subnet Failover", value);
				this._multiSubnetFailover = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0001AE46 File Offset: 0x00019046
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0001AE4E File Offset: 0x0001904E
		[DisplayName("Packet Size")]
		[ResCategory("Advanced")]
		[ResDescription("Size in bytes of the network packets used to communicate with an instance of SQL Server.")]
		[RefreshProperties(RefreshProperties.All)]
		public int PacketSize
		{
			get
			{
				return this._packetSize;
			}
			set
			{
				if (value < 512 || 32768 < value)
				{
					throw SQL.InvalidPacketSizeValue();
				}
				this.SetValue("Packet Size", value);
				this._packetSize = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0001AE79 File Offset: 0x00019079
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0001AE81 File Offset: 0x00019081
		[DisplayName("Password")]
		[ResCategory("Security")]
		[ResDescription("Indicates the password to be used when connecting to the data source.")]
		[PasswordPropertyText(true)]
		[RefreshProperties(RefreshProperties.All)]
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this.SetValue("Password", value);
				this._password = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0001AE96 File Offset: 0x00019096
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0001AE9E File Offset: 0x0001909E
		[DisplayName("Persist Security Info")]
		[ResCategory("Security")]
		[ResDescription("When false, security-sensitive information, such as the password, is not returned as part of the connection if the connection is open or has ever been in an open state.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool PersistSecurityInfo
		{
			get
			{
				return this._persistSecurityInfo;
			}
			set
			{
				this.SetValue("Persist Security Info", value);
				this._persistSecurityInfo = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0001AEB3 File Offset: 0x000190B3
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0001AEBB File Offset: 0x000190BB
		[DisplayName("Pool Blocking Period")]
		[ResCategory("Pooling")]
		[ResDescription("Defines the blocking period behavior for a connection pool.")]
		[RefreshProperties(RefreshProperties.All)]
		public PoolBlockingPeriod PoolBlockingPeriod
		{
			get
			{
				return this._poolBlockingPeriod;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidPoolBlockingPeriodValue(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(PoolBlockingPeriod), (int)value);
				}
				this.SetPoolBlockingPeriodValue(value);
				this._poolBlockingPeriod = value;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0001AEE4 File Offset: 0x000190E4
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0001AEEC File Offset: 0x000190EC
		[DisplayName("Pooling")]
		[ResCategory("Pooling")]
		[ResDescription("When true, the connection object is drawn from the appropriate pool, or if necessary, is created and added to the appropriate pool.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Pooling
		{
			get
			{
				return this._pooling;
			}
			set
			{
				this.SetValue("Pooling", value);
				this._pooling = value;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0001AF01 File Offset: 0x00019101
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0001AF09 File Offset: 0x00019109
		[DisplayName("Replication")]
		[ResCategory("Replication")]
		[ResDescription("Used by SQL Server in Replication.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Replication
		{
			get
			{
				return this._replication;
			}
			set
			{
				this.SetValue("Replication", value);
				this._replication = value;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0001AF1E File Offset: 0x0001911E
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0001AF26 File Offset: 0x00019126
		[DisplayName("Transaction Binding")]
		[ResCategory("Advanced")]
		[ResDescription("Indicates binding behavior of connection to a System.Transactions Transaction when enlisted.")]
		[RefreshProperties(RefreshProperties.All)]
		public string TransactionBinding
		{
			get
			{
				return this._transactionBinding;
			}
			set
			{
				this.SetValue("Transaction Binding", value);
				this._transactionBinding = value;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0001AF3B File Offset: 0x0001913B
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0001AF43 File Offset: 0x00019143
		[DisplayName("Type System Version")]
		[ResCategory("Advanced")]
		[ResDescription("Indicates which server type system the provider will expose through the DataReader.")]
		[RefreshProperties(RefreshProperties.All)]
		public string TypeSystemVersion
		{
			get
			{
				return this._typeSystemVersion;
			}
			set
			{
				this.SetValue("Type System Version", value);
				this._typeSystemVersion = value;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0001AF58 File Offset: 0x00019158
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0001AF60 File Offset: 0x00019160
		[DisplayName("User ID")]
		[ResCategory("Security")]
		[ResDescription("Indicates the user ID to be used when connecting to the data source.")]
		[RefreshProperties(RefreshProperties.All)]
		public string UserID
		{
			get
			{
				return this._userID;
			}
			set
			{
				this.SetValue("User ID", value);
				this._userID = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0001AF75 File Offset: 0x00019175
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0001AF7D File Offset: 0x0001917D
		[DisplayName("User Instance")]
		[ResCategory("Source")]
		[ResDescription("Indicates whether the connection will be re-directed to connect to an instance of SQL Server running under the user's account.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool UserInstance
		{
			get
			{
				return this._userInstance;
			}
			set
			{
				this.SetValue("User Instance", value);
				this._userInstance = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0001AF92 File Offset: 0x00019192
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0001AF9A File Offset: 0x0001919A
		[DisplayName("Workstation ID")]
		[ResCategory("Context")]
		[ResDescription("The name of the workstation connecting to SQL Server.")]
		[RefreshProperties(RefreshProperties.All)]
		public string WorkstationID
		{
			get
			{
				return this._workstationID;
			}
			set
			{
				this.SetValue("Workstation ID", value);
				this._workstationID = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public override bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0001AFAF File Offset: 0x000191AF
		public override ICollection Keys
		{
			get
			{
				return new ReadOnlyCollection<string>(SqlConnectionStringBuilder.s_validKeywords);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0001AFBC File Offset: 0x000191BC
		public override ICollection Values
		{
			get
			{
				object[] array = new object[SqlConnectionStringBuilder.s_validKeywords.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetAt((SqlConnectionStringBuilder.Keywords)i);
				}
				return new ReadOnlyCollection<object>(array);
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001AFF4 File Offset: 0x000191F4
		public override void Clear()
		{
			base.Clear();
			for (int i = 0; i < SqlConnectionStringBuilder.s_validKeywords.Length; i++)
			{
				this.Reset((SqlConnectionStringBuilder.Keywords)i);
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001B020 File Offset: 0x00019220
		public override bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return SqlConnectionStringBuilder.s_keywords.ContainsKey(keyword);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001B038 File Offset: 0x00019238
		public override bool Remove(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			SqlConnectionStringBuilder.Keywords keywords;
			if (SqlConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords) && base.Remove(SqlConnectionStringBuilder.s_validKeywords[(int)keywords]))
			{
				this.Reset(keywords);
				return true;
			}
			return false;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001B078 File Offset: 0x00019278
		public override bool ShouldSerialize(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			SqlConnectionStringBuilder.Keywords keywords;
			return SqlConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords) && base.ShouldSerialize(SqlConnectionStringBuilder.s_validKeywords[(int)keywords]);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001B0B0 File Offset: 0x000192B0
		public override bool TryGetValue(string keyword, out object value)
		{
			SqlConnectionStringBuilder.Keywords keywords;
			if (SqlConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords))
			{
				value = this.GetAt(keywords);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0001B0DB File Offset: 0x000192DB
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0001B0E3 File Offset: 0x000192E3
		[Browsable(false)]
		[DisplayName("Connection Reset")]
		[Obsolete("ConnectionReset has been deprecated. SqlConnection will ignore the 'connection reset' keyword and always reset the connection.")]
		[ResCategory("Pooling")]
		[ResDescription("When true, indicates the connection state is reset when removed from the pool.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool ConnectionReset
		{
			get
			{
				return this._connectionReset;
			}
			set
			{
				this.SetValue("Connection Reset", value);
				this._connectionReset = value;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0001B0F8 File Offset: 0x000192F8
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x0001B100 File Offset: 0x00019300
		[DisplayName("Context Connection")]
		[Obsolete("ContextConnection has been deprecated. SqlConnection will ignore the 'Context Connection' keyword.")]
		[ResCategory("Source")]
		[ResDescription("When true, indicates the connection should be from the Sql Server context.  Available only when running in the Sql Server process.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool ContextConnection
		{
			get
			{
				return this._contextConnection;
			}
			set
			{
				this.SetValue("Context Connection", value);
				this._contextConnection = value;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0001B115 File Offset: 0x00019315
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x0001B11D File Offset: 0x0001931D
		[DisplayName("Transparent Network IP Resolution")]
		[ResCategory("Source")]
		[ResDescription("If your application connects to different networks, TransparentNetworkIPResolution=Yes configures SqlConnection to provide transparent connection resolution to the currently active server, independently of the network IP topology.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool TransparentNetworkIPResolution
		{
			get
			{
				return this._transparentNetworkIPResolution;
			}
			set
			{
				this.SetValue("Transparent Network IP Resolution", value);
				this._transparentNetworkIPResolution = value;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0001B132 File Offset: 0x00019332
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x0001B13C File Offset: 0x0001933C
		[DisplayName("Network Library")]
		[ResCategory("Advanced")]
		[ResDescription("The network library used to establish a connection to an instance of SQL Server.")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(SqlConnectionStringBuilder.NetworkLibraryConverter))]
		public string NetworkLibrary
		{
			get
			{
				return this._networkLibrary;
			}
			set
			{
				if (value != null)
				{
					string text = value.Trim().ToLower(CultureInfo.InvariantCulture);
					if (text != null)
					{
						int length = text.Length;
						if (length == 8)
						{
							char c = text[4];
							string text2;
							if (c <= 'g')
							{
								if (c != 'a')
								{
									if (c != 'g')
									{
										goto IL_0138;
									}
									if (!(text == "dbmsgnet"))
									{
										goto IL_0138;
									}
									text2 = "dbmsgnet";
								}
								else
								{
									if (!(text == "dbmsadsn"))
									{
										goto IL_0138;
									}
									text2 = "dbmsadsn";
								}
							}
							else if (c != 'l')
							{
								switch (c)
								{
								case 'p':
									if (!(text == "dbnmpntw"))
									{
										goto IL_0138;
									}
									text2 = "dbnmpntw";
									break;
								case 'q':
								case 't':
								case 'u':
									goto IL_0138;
								case 'r':
									if (!(text == "dbmsrpcn"))
									{
										goto IL_0138;
									}
									text2 = "dbmsrpcn";
									break;
								case 's':
									if (!(text == "dbmsspxn"))
									{
										if (!(text == "dbmssocn"))
										{
											goto IL_0138;
										}
										text2 = "dbmssocn";
									}
									else
									{
										text2 = "dbmsspxn";
									}
									break;
								case 'v':
									if (!(text == "dbmsvinn"))
									{
										goto IL_0138;
									}
									text2 = "dbmsvinn";
									break;
								default:
									goto IL_0138;
								}
							}
							else
							{
								if (!(text == "dbmslpcn"))
								{
									goto IL_0138;
								}
								text2 = "dbmslpcn";
							}
							value = text2;
							goto IL_0146;
						}
					}
					IL_0138:
					throw ADP.InvalidConnectionOptionValue("Network Library");
				}
				IL_0146:
				this.SetValue("Network Library", value);
				this._networkLibrary = value;
			}
		}

		// Token: 0x040001CF RID: 463
		internal const int KeywordsCount = 44;

		// Token: 0x040001D0 RID: 464
		private static readonly string[] s_validKeywords = SqlConnectionStringBuilder.CreateValidKeywords();

		// Token: 0x040001D1 RID: 465
		private static readonly Dictionary<string, SqlConnectionStringBuilder.Keywords> s_keywords = SqlConnectionStringBuilder.CreateKeywordsDictionary();

		// Token: 0x040001D2 RID: 466
		private ApplicationIntent _applicationIntent;

		// Token: 0x040001D3 RID: 467
		private string _applicationName = "Framework Microsoft SqlClient Data Provider";

		// Token: 0x040001D4 RID: 468
		private string _attachDBFilename = "";

		// Token: 0x040001D5 RID: 469
		private string _currentLanguage = "";

		// Token: 0x040001D6 RID: 470
		private string _dataSource = "";

		// Token: 0x040001D7 RID: 471
		private string _failoverPartner = "";

		// Token: 0x040001D8 RID: 472
		private string _initialCatalog = "";

		// Token: 0x040001D9 RID: 473
		private string _password = "";

		// Token: 0x040001DA RID: 474
		private string _transactionBinding = "Implicit Unbind";

		// Token: 0x040001DB RID: 475
		private string _typeSystemVersion = "Latest";

		// Token: 0x040001DC RID: 476
		private string _userID = "";

		// Token: 0x040001DD RID: 477
		private string _workstationID = "";

		// Token: 0x040001DE RID: 478
		private int _commandTimeout = 30;

		// Token: 0x040001DF RID: 479
		private int _connectTimeout = 15;

		// Token: 0x040001E0 RID: 480
		private int _loadBalanceTimeout;

		// Token: 0x040001E1 RID: 481
		private int _maxPoolSize = 100;

		// Token: 0x040001E2 RID: 482
		private int _minPoolSize;

		// Token: 0x040001E3 RID: 483
		private int _packetSize = 8000;

		// Token: 0x040001E4 RID: 484
		private int _connectRetryCount = 1;

		// Token: 0x040001E5 RID: 485
		private int _connectRetryInterval = 10;

		// Token: 0x040001E6 RID: 486
		private SqlConnectionEncryptOption _encrypt = DbConnectionStringDefaults.Encrypt;

		// Token: 0x040001E7 RID: 487
		private string _hostNameInCertificate = "";

		// Token: 0x040001E8 RID: 488
		private string _serverCertificate = "";

		// Token: 0x040001E9 RID: 489
		private bool _trustServerCertificate;

		// Token: 0x040001EA RID: 490
		private bool _enlist = true;

		// Token: 0x040001EB RID: 491
		private bool _integratedSecurity;

		// Token: 0x040001EC RID: 492
		private bool _multipleActiveResultSets;

		// Token: 0x040001ED RID: 493
		private bool _multiSubnetFailover;

		// Token: 0x040001EE RID: 494
		private bool _persistSecurityInfo;

		// Token: 0x040001EF RID: 495
		private PoolBlockingPeriod _poolBlockingPeriod;

		// Token: 0x040001F0 RID: 496
		private bool _pooling = true;

		// Token: 0x040001F1 RID: 497
		private bool _replication;

		// Token: 0x040001F2 RID: 498
		private bool _userInstance;

		// Token: 0x040001F3 RID: 499
		private SqlAuthenticationMethod _authentication = DbConnectionStringDefaults.Authentication;

		// Token: 0x040001F4 RID: 500
		private SqlConnectionColumnEncryptionSetting _columnEncryptionSetting;

		// Token: 0x040001F5 RID: 501
		private string _enclaveAttestationUrl = "";

		// Token: 0x040001F6 RID: 502
		private SqlConnectionAttestationProtocol _attestationProtocol;

		// Token: 0x040001F7 RID: 503
		private SqlConnectionIPAddressPreference _ipAddressPreference;

		// Token: 0x040001F8 RID: 504
		private string _serverSPN = "";

		// Token: 0x040001F9 RID: 505
		private string _failoverPartnerSPN = "";

		// Token: 0x040001FA RID: 506
		private bool _connectionReset = true;

		// Token: 0x040001FB RID: 507
		private bool _contextConnection;

		// Token: 0x040001FC RID: 508
		private bool _transparentNetworkIPResolution = DbConnectionStringDefaults.TransparentNetworkIPResolution;

		// Token: 0x040001FD RID: 509
		private string _networkLibrary = "";

		// Token: 0x020001C6 RID: 454
		private enum Keywords
		{
			// Token: 0x040013BE RID: 5054
			DataSource,
			// Token: 0x040013BF RID: 5055
			FailoverPartner,
			// Token: 0x040013C0 RID: 5056
			AttachDBFilename,
			// Token: 0x040013C1 RID: 5057
			InitialCatalog,
			// Token: 0x040013C2 RID: 5058
			IntegratedSecurity,
			// Token: 0x040013C3 RID: 5059
			PersistSecurityInfo,
			// Token: 0x040013C4 RID: 5060
			UserID,
			// Token: 0x040013C5 RID: 5061
			Password,
			// Token: 0x040013C6 RID: 5062
			Enlist,
			// Token: 0x040013C7 RID: 5063
			Pooling,
			// Token: 0x040013C8 RID: 5064
			MinPoolSize,
			// Token: 0x040013C9 RID: 5065
			MaxPoolSize,
			// Token: 0x040013CA RID: 5066
			PoolBlockingPeriod,
			// Token: 0x040013CB RID: 5067
			MultipleActiveResultSets,
			// Token: 0x040013CC RID: 5068
			Replication,
			// Token: 0x040013CD RID: 5069
			ConnectTimeout,
			// Token: 0x040013CE RID: 5070
			Encrypt,
			// Token: 0x040013CF RID: 5071
			HostNameInCertificate,
			// Token: 0x040013D0 RID: 5072
			ServerCertificate,
			// Token: 0x040013D1 RID: 5073
			TrustServerCertificate,
			// Token: 0x040013D2 RID: 5074
			LoadBalanceTimeout,
			// Token: 0x040013D3 RID: 5075
			PacketSize,
			// Token: 0x040013D4 RID: 5076
			TypeSystemVersion,
			// Token: 0x040013D5 RID: 5077
			Authentication,
			// Token: 0x040013D6 RID: 5078
			ApplicationName,
			// Token: 0x040013D7 RID: 5079
			CurrentLanguage,
			// Token: 0x040013D8 RID: 5080
			WorkstationID,
			// Token: 0x040013D9 RID: 5081
			UserInstance,
			// Token: 0x040013DA RID: 5082
			TransactionBinding,
			// Token: 0x040013DB RID: 5083
			ApplicationIntent,
			// Token: 0x040013DC RID: 5084
			MultiSubnetFailover,
			// Token: 0x040013DD RID: 5085
			ConnectRetryCount,
			// Token: 0x040013DE RID: 5086
			ConnectRetryInterval,
			// Token: 0x040013DF RID: 5087
			ColumnEncryptionSetting,
			// Token: 0x040013E0 RID: 5088
			EnclaveAttestationUrl,
			// Token: 0x040013E1 RID: 5089
			AttestationProtocol,
			// Token: 0x040013E2 RID: 5090
			CommandTimeout,
			// Token: 0x040013E3 RID: 5091
			IPAddressPreference,
			// Token: 0x040013E4 RID: 5092
			ServerSPN,
			// Token: 0x040013E5 RID: 5093
			FailoverPartnerSPN,
			// Token: 0x040013E6 RID: 5094
			ConnectionReset,
			// Token: 0x040013E7 RID: 5095
			NetworkLibrary,
			// Token: 0x040013E8 RID: 5096
			ContextConnection,
			// Token: 0x040013E9 RID: 5097
			TransparentNetworkIPResolution,
			// Token: 0x040013EA RID: 5098
			KeywordsCount
		}

		// Token: 0x020001C7 RID: 455
		private sealed class SqlInitialCatalogConverter : StringConverter
		{
			// Token: 0x06001D98 RID: 7576 RVA: 0x0007A205 File Offset: 0x00078405
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return this.GetStandardValuesSupportedInternal(context);
			}

			// Token: 0x06001D99 RID: 7577 RVA: 0x0007A210 File Offset: 0x00078410
			private bool GetStandardValuesSupportedInternal(ITypeDescriptorContext context)
			{
				bool flag = false;
				if (context != null)
				{
					SqlConnectionStringBuilder sqlConnectionStringBuilder = context.Instance as SqlConnectionStringBuilder;
					if (sqlConnectionStringBuilder != null && 0 < sqlConnectionStringBuilder.DataSource.Length && (sqlConnectionStringBuilder.IntegratedSecurity || 0 < sqlConnectionStringBuilder.UserID.Length))
					{
						flag = true;
					}
				}
				return flag;
			}

			// Token: 0x06001D9A RID: 7578 RVA: 0x0001996E File Offset: 0x00017B6E
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x06001D9B RID: 7579 RVA: 0x0007A258 File Offset: 0x00078458
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				if (this.GetStandardValuesSupportedInternal(context))
				{
					List<string> list = new List<string>();
					try
					{
						SqlConnectionStringBuilder sqlConnectionStringBuilder = (SqlConnectionStringBuilder)context.Instance;
						using (SqlConnection sqlConnection = new SqlConnection())
						{
							sqlConnection.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
							sqlConnection.Open();
							DataTable schema = sqlConnection.GetSchema("DATABASES");
							foreach (object obj in schema.Rows)
							{
								DataRow dataRow = (DataRow)obj;
								string text = (string)dataRow["database_name"];
								list.Add(text);
							}
						}
					}
					catch (SqlException ex)
					{
						ADP.TraceExceptionWithoutRethrow(ex);
					}
					return new TypeConverter.StandardValuesCollection(list);
				}
				return null;
			}
		}

		// Token: 0x020001C8 RID: 456
		internal sealed class SqlConnectionStringBuilderConverter : ExpandableObjectConverter
		{
			// Token: 0x06001D9D RID: 7581 RVA: 0x0007A348 File Offset: 0x00078548
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06001D9E RID: 7582 RVA: 0x0007A368 File Offset: 0x00078568
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType)
				{
					SqlConnectionStringBuilder sqlConnectionStringBuilder = value as SqlConnectionStringBuilder;
					if (sqlConnectionStringBuilder != null)
					{
						return this.ConvertToInstanceDescriptor(sqlConnectionStringBuilder);
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x06001D9F RID: 7583 RVA: 0x0007A3B4 File Offset: 0x000785B4
			private InstanceDescriptor ConvertToInstanceDescriptor(SqlConnectionStringBuilder options)
			{
				Type[] array = new Type[] { typeof(string) };
				object[] array2 = new object[] { options.ConnectionString };
				ConstructorInfo constructor = typeof(SqlConnectionStringBuilder).GetConstructor(array);
				return new InstanceDescriptor(constructor, array2);
			}
		}

		// Token: 0x020001C9 RID: 457
		private sealed class SqlDataSourceConverter : StringConverter
		{
			// Token: 0x06001DA1 RID: 7585 RVA: 0x0000EBAD File Offset: 0x0000CDAD
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x06001DA2 RID: 7586 RVA: 0x0001996E File Offset: 0x00017B6E
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x06001DA3 RID: 7587 RVA: 0x0007A400 File Offset: 0x00078600
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				TypeConverter.StandardValuesCollection standardValuesCollection = this._standardValues;
				if (this._standardValues == null)
				{
					DataTable dataSources = SqlClientFactory.Instance.CreateDataSourceEnumerator().GetDataSources();
					string text = typeof(SqlDataSourceEnumerator).GetField("ServerName", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null).ToString();
					string text2 = typeof(SqlDataSourceEnumerator).GetField("InstanceName", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null).ToString();
					DataColumn dataColumn = dataSources.Columns[text];
					DataColumn dataColumn2 = dataSources.Columns[text2];
					DataRowCollection rows = dataSources.Rows;
					string[] array = new string[rows.Count];
					for (int i = 0; i < array.Length; i++)
					{
						string text3 = rows[i][dataColumn] as string;
						string text4 = rows[i][dataColumn2] as string;
						if (text4 == null || text4.Length == 0 || "MSSQLSERVER" == text4)
						{
							array[i] = text3;
						}
						else
						{
							array[i] = text3 + "\\" + text4;
						}
					}
					Array.Sort<string>(array);
					standardValuesCollection = new TypeConverter.StandardValuesCollection(array);
					this._standardValues = standardValuesCollection;
				}
				return standardValuesCollection;
			}

			// Token: 0x040013EB RID: 5099
			private TypeConverter.StandardValuesCollection _standardValues;
		}

		// Token: 0x020001CA RID: 458
		private sealed class NetworkLibraryConverter : TypeConverter
		{
			// Token: 0x06001DA5 RID: 7589 RVA: 0x0007A534 File Offset: 0x00078734
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06001DA6 RID: 7590 RVA: 0x0007A554 File Offset: 0x00078754
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text == null)
				{
					return base.ConvertFrom(context, culture, value);
				}
				text = text.Trim();
				if (StringComparer.OrdinalIgnoreCase.Equals(text, "Named Pipes (DBNMPNTW)"))
				{
					return "dbnmpntw";
				}
				if (StringComparer.OrdinalIgnoreCase.Equals(text, "Shared Memory (DBMSLPCN)"))
				{
					return "dbmslpcn";
				}
				if (StringComparer.OrdinalIgnoreCase.Equals(text, "TCP/IP (DBMSSOCN)"))
				{
					return "dbmssocn";
				}
				if (StringComparer.OrdinalIgnoreCase.Equals(text, "VIA (DBMSGNET)"))
				{
					return "dbmsgnet";
				}
				return text;
			}

			// Token: 0x06001DA7 RID: 7591 RVA: 0x0007A5DD File Offset: 0x000787DD
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(string) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06001DA8 RID: 7592 RVA: 0x0007A5FC File Offset: 0x000787FC
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				string text = value as string;
				if (text != null && destinationType == typeof(string))
				{
					string text2 = text.Trim().ToLower(CultureInfo.InvariantCulture);
					string text3;
					if (!(text2 == "dbnmpntw"))
					{
						if (!(text2 == "dbmslpcn"))
						{
							if (!(text2 == "dbmssocn"))
							{
								if (!(text2 == "dbmsgnet"))
								{
									text3 = text;
								}
								else
								{
									text3 = "VIA (DBMSGNET)";
								}
							}
							else
							{
								text3 = "TCP/IP (DBMSSOCN)";
							}
						}
						else
						{
							text3 = "Shared Memory (DBMSLPCN)";
						}
					}
					else
					{
						text3 = "Named Pipes (DBNMPNTW)";
					}
					return text3;
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x06001DA9 RID: 7593 RVA: 0x0000EBAD File Offset: 0x0000CDAD
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x06001DAA RID: 7594 RVA: 0x0001996E File Offset: 0x00017B6E
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x06001DAB RID: 7595 RVA: 0x0007A69C File Offset: 0x0007889C
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				TypeConverter.StandardValuesCollection standardValuesCollection = this._standardValues;
				if (standardValuesCollection == null)
				{
					string[] array = new string[] { "Named Pipes (DBNMPNTW)", "Shared Memory (DBMSLPCN)", "TCP/IP (DBMSSOCN)", "VIA (DBMSGNET)" };
					standardValuesCollection = new TypeConverter.StandardValuesCollection(array);
					this._standardValues = standardValuesCollection;
				}
				return standardValuesCollection;
			}

			// Token: 0x040013EC RID: 5100
			private const string NamedPipes = "Named Pipes (DBNMPNTW)";

			// Token: 0x040013ED RID: 5101
			private const string SharedMemory = "Shared Memory (DBMSLPCN)";

			// Token: 0x040013EE RID: 5102
			private const string TCPIP = "TCP/IP (DBMSSOCN)";

			// Token: 0x040013EF RID: 5103
			private const string VIA = "VIA (DBMSGNET)";

			// Token: 0x040013F0 RID: 5104
			private TypeConverter.StandardValuesCollection _standardValues;
		}
	}
}
