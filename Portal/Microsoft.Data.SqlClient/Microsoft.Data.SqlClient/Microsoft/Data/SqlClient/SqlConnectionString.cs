using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200006C RID: 108
	internal sealed class SqlConnectionString : DbConnectionOptions
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x000181D4 File Offset: 0x000163D4
		internal SqlConnectionString(string connectionString)
			: base(connectionString, SqlConnectionString.GetParseSynonyms())
		{
			bool inProc = InOutOfProcHelper.InProc;
			this._integratedSecurity = base.ConvertValueToIntegratedSecurity();
			this._poolBlockingPeriod = this.ConvertValueToPoolBlockingPeriod();
			this._encrypt = this.ConvertValueToSqlConnectionEncrypt();
			this._enlist = base.ConvertValueToBoolean("Enlist", true);
			this._mars = base.ConvertValueToBoolean("Multiple Active Result Sets", false);
			this._persistSecurityInfo = base.ConvertValueToBoolean("Persist Security Info", false);
			this._pooling = base.ConvertValueToBoolean("Pooling", true);
			this._replication = base.ConvertValueToBoolean("Replication", false);
			this._userInstance = base.ConvertValueToBoolean("User Instance", false);
			this._multiSubnetFailover = base.ConvertValueToBoolean("Multi Subnet Failover", false);
			this._commandTimeout = base.ConvertValueToInt32("Command Timeout", 30);
			this._connectTimeout = base.ConvertValueToInt32("Connect Timeout", 15);
			this._loadBalanceTimeout = base.ConvertValueToInt32("Load Balance Timeout", 0);
			this._maxPoolSize = base.ConvertValueToInt32("Max Pool Size", 100);
			this._minPoolSize = base.ConvertValueToInt32("Min Pool Size", 0);
			this._packetSize = base.ConvertValueToInt32("Packet Size", 8000);
			this._connectRetryCount = base.ConvertValueToInt32("Connect Retry Count", 1);
			this._connectRetryInterval = base.ConvertValueToInt32("Connect Retry Interval", 10);
			this._applicationIntent = this.ConvertValueToApplicationIntent();
			this._applicationName = base.ConvertValueToString("Application Name", "Framework Microsoft SqlClient Data Provider");
			this._attachDBFileName = base.ConvertValueToString("AttachDbFilename", "");
			this._currentLanguage = base.ConvertValueToString("Current Language", "");
			this._dataSource = base.ConvertValueToString("Data Source", "");
			this._localDBInstance = LocalDBAPI.GetLocalDbInstanceNameFromServerName(this._dataSource);
			this._failoverPartner = base.ConvertValueToString("Failover Partner", "");
			this._initialCatalog = base.ConvertValueToString("Initial Catalog", "");
			this._password = base.ConvertValueToString("Password", "");
			this._trustServerCertificate = base.ConvertValueToBoolean("Trust Server Certificate", false);
			this._authType = this.ConvertValueToAuthenticationType();
			this._columnEncryptionSetting = this.ConvertValueToColumnEncryptionSetting();
			this._enclaveAttestationUrl = base.ConvertValueToString("Enclave Attestation Url", "");
			this._attestationProtocol = this.ConvertValueToAttestationProtocol();
			this._ipAddressPreference = this.ConvertValueToIPAddressPreference();
			this._hostNameInCertificate = base.ConvertValueToString("Host Name In Certificate", "");
			this._serverCertificate = base.ConvertValueToString("Server Certificate", "");
			this._serverSPN = base.ConvertValueToString("Server SPN", "");
			this._failoverPartnerSPN = base.ConvertValueToString("Failover Partner SPN", "");
			string text = base.ConvertValueToString("Type System Version", null);
			string text2 = base.ConvertValueToString("Transaction Binding", null);
			this._userID = base.ConvertValueToString("User ID", "");
			this._workstationId = base.ConvertValueToString("Workstation ID", null);
			if (this._loadBalanceTimeout < 0)
			{
				throw ADP.InvalidConnectionOptionValue("Load Balance Timeout");
			}
			if (this._connectTimeout < 0)
			{
				throw ADP.InvalidConnectionOptionValue("Connect Timeout");
			}
			if (this._commandTimeout < 0)
			{
				throw ADP.InvalidConnectionOptionValue("Command Timeout");
			}
			if (this._maxPoolSize < 1)
			{
				throw ADP.InvalidConnectionOptionValue("Max Pool Size");
			}
			if (this._minPoolSize < 0)
			{
				throw ADP.InvalidConnectionOptionValue("Min Pool Size");
			}
			if (this._maxPoolSize < this._minPoolSize)
			{
				throw ADP.InvalidMinMaxPoolSizeValues();
			}
			if (this._packetSize < 512 || 32768 < this._packetSize)
			{
				throw SQL.InvalidPacketSizeValue();
			}
			this._connectionReset = base.ConvertValueToBoolean("Connection Reset", true);
			this._contextConnection = base.ConvertValueToBoolean("Context Connection", false);
			this._encrypt = this.ConvertValueToSqlConnectionEncrypt();
			this._enlist = base.ConvertValueToBoolean("Enlist", ADP.s_isWindowsNT);
			this._transparentNetworkIPResolution = base.ConvertValueToBoolean("Transparent Network IP Resolution", SqlConnectionString.DEFAULT.TransparentNetworkIPResolution);
			this._networkLibrary = base.ConvertValueToString("Network Library", null);
			if (this._contextConnection)
			{
				if (!inProc)
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				foreach (KeyValuePair<string, string> keyValuePair in base.Parsetable)
				{
					if (keyValuePair.Key != "Context Connection" && keyValuePair.Key != "Type System Version")
					{
						throw SQL.ContextAllowsLimitedKeywords();
					}
				}
			}
			if (this._encrypt == SqlConnectionEncryptOption.Optional)
			{
				object obj = ADP.LocalMachineRegistryValue("Software\\Microsoft\\MSSQLServer\\Client\\SuperSocketNetLib", "Encrypt");
				if (obj is int)
				{
					int num = (int)obj;
					if (num == 1)
					{
						this._encrypt = SqlConnectionEncryptOption.Mandatory;
					}
				}
			}
			if (this._networkLibrary != null)
			{
				string text3 = this._networkLibrary.Trim().ToLower(CultureInfo.InvariantCulture);
				Hashtable hashtable = SqlConnectionString.NetlibMapping();
				if (!hashtable.ContainsKey(text3))
				{
					throw ADP.InvalidConnectionOptionValue("Network Library");
				}
				this._networkLibrary = (string)hashtable[text3];
			}
			else
			{
				this._networkLibrary = "";
			}
			this.ValidateValueLength(this._applicationName, 128, "Application Name");
			this.ValidateValueLength(this._currentLanguage, 128, "Current Language");
			this.ValidateValueLength(this._dataSource, 128, "Data Source");
			this.ValidateValueLength(this._failoverPartner, 128, "Failover Partner");
			this.ValidateValueLength(this._initialCatalog, 128, "Initial Catalog");
			this.ValidateValueLength(this._password, 128, "Password");
			this.ValidateValueLength(this._userID, 128, "User ID");
			if (this._workstationId != null)
			{
				this.ValidateValueLength(this._workstationId, 128, "Workstation ID");
			}
			if (!string.Equals("", this._failoverPartner, StringComparison.OrdinalIgnoreCase))
			{
				if (this._multiSubnetFailover)
				{
					throw SQL.MultiSubnetFailoverWithFailoverPartner(false, null);
				}
				if (string.Equals("", this._initialCatalog, StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.MissingConnectionOptionValue("Failover Partner", "Initial Catalog");
				}
			}
			string text4 = null;
			this._expandedAttachDBFilename = DbConnectionOptions.ExpandDataDirectory("AttachDbFilename", this._attachDBFileName, ref text4);
			if (this._expandedAttachDBFilename != null)
			{
				if (0 <= this._expandedAttachDBFilename.IndexOf('|'))
				{
					throw ADP.InvalidConnectionOptionValue("AttachDbFilename");
				}
				this.ValidateValueLength(this._expandedAttachDBFilename, 260, "AttachDbFilename");
				if (this._localDBInstance == null)
				{
					string dataSource = this._dataSource;
					string networkLibrary = this._networkLibrary;
					TdsParserStaticMethods.AliasRegistryLookup(ref dataSource, ref networkLibrary);
					SqlConnectionString.VerifyLocalHostAndFixup(ref dataSource, true, false);
				}
			}
			else
			{
				if (0 <= this._attachDBFileName.IndexOf('|'))
				{
					throw ADP.InvalidConnectionOptionValue("AttachDbFilename");
				}
				this.ValidateValueLength(this._attachDBFileName, 260, "AttachDbFilename");
			}
			this._typeSystemAssemblyVersion = SqlConnectionString.s_constTypeSystemAsmVersion10;
			if (this._userInstance && !string.IsNullOrEmpty(this._failoverPartner))
			{
				throw SQL.UserInstanceFailoverNotCompatible();
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "Latest";
			}
			if (text.Equals("Latest", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.Latest;
			}
			else if (text.Equals("SQL Server 2000", StringComparison.OrdinalIgnoreCase))
			{
				if (this._contextConnection)
				{
					throw SQL.ContextAllowsOnlyTypeSystem2005();
				}
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2000;
			}
			else if (text.Equals("SQL Server 2005", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2005;
			}
			else if (text.Equals("SQL Server 2008", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.Latest;
			}
			else
			{
				if (!text.Equals("SQL Server 2012", StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.InvalidConnectionOptionValue("Type System Version");
				}
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2012;
				this._typeSystemAssemblyVersion = SqlConnectionString.s_constTypeSystemAsmVersion11;
			}
			if (string.IsNullOrEmpty(text2))
			{
				text2 = "Implicit Unbind";
			}
			if (text2.Equals("Implicit Unbind", StringComparison.OrdinalIgnoreCase))
			{
				this._transactionBinding = SqlConnectionString.TransactionBindingEnum.ImplicitUnbind;
			}
			else
			{
				if (!text2.Equals("Explicit Unbind", StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.InvalidConnectionOptionValue("Transaction Binding");
				}
				this._transactionBinding = SqlConnectionString.TransactionBindingEnum.ExplicitUnbind;
			}
			if (this._applicationIntent == ApplicationIntent.ReadOnly && !string.IsNullOrEmpty(this._failoverPartner))
			{
				throw SQL.ROR_FailoverNotSupportedConnString();
			}
			if (this._connectRetryCount < 0 || this._connectRetryCount > 255)
			{
				throw ADP.InvalidConnectRetryCountValue();
			}
			if (this._connectRetryInterval < 1 || this._connectRetryInterval > 60)
			{
				throw ADP.InvalidConnectRetryIntervalValue();
			}
			if (this.Authentication != SqlAuthenticationMethod.NotSpecified && this._integratedSecurity)
			{
				throw SQL.AuthenticationAndIntegratedSecurity();
			}
			if (this.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated && this._hasPasswordKeyword)
			{
				throw SQL.IntegratedWithPassword();
			}
			if (this.Authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive && this._hasPasswordKeyword)
			{
				throw SQL.InteractiveWithPassword();
			}
			if (this.Authentication == SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow && (this._hasUserIdKeyword || this._hasPasswordKeyword))
			{
				throw SQL.DeviceFlowWithUsernamePassword();
			}
			if (this.Authentication == SqlAuthenticationMethod.ActiveDirectoryManagedIdentity && this._hasPasswordKeyword)
			{
				throw SQL.NonInteractiveWithPassword("Active Directory Managed Identity");
			}
			if (this.Authentication == SqlAuthenticationMethod.ActiveDirectoryMSI && this._hasPasswordKeyword)
			{
				throw SQL.NonInteractiveWithPassword("Active Directory MSI");
			}
			if (this.Authentication == SqlAuthenticationMethod.ActiveDirectoryDefault && this._hasPasswordKeyword)
			{
				throw SQL.NonInteractiveWithPassword("Active Directory Default");
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00018AD8 File Offset: 0x00016CD8
		internal SqlConnectionString(SqlConnectionString connectionOptions, string dataSource, bool userInstance, bool? setEnlistValue)
			: base(connectionOptions)
		{
			this._integratedSecurity = connectionOptions._integratedSecurity;
			this._encrypt = connectionOptions._encrypt;
			if (setEnlistValue != null)
			{
				this._enlist = setEnlistValue.Value;
			}
			else
			{
				this._enlist = connectionOptions._enlist;
			}
			this._mars = connectionOptions._mars;
			this._persistSecurityInfo = connectionOptions._persistSecurityInfo;
			this._pooling = connectionOptions._pooling;
			this._replication = connectionOptions._replication;
			this._userInstance = userInstance;
			this._commandTimeout = connectionOptions._commandTimeout;
			this._connectTimeout = connectionOptions._connectTimeout;
			this._loadBalanceTimeout = connectionOptions._loadBalanceTimeout;
			this._poolBlockingPeriod = connectionOptions._poolBlockingPeriod;
			this._maxPoolSize = connectionOptions._maxPoolSize;
			this._minPoolSize = connectionOptions._minPoolSize;
			this._multiSubnetFailover = connectionOptions._multiSubnetFailover;
			this._packetSize = connectionOptions._packetSize;
			this._applicationName = connectionOptions._applicationName;
			this._attachDBFileName = connectionOptions._attachDBFileName;
			this._currentLanguage = connectionOptions._currentLanguage;
			this._dataSource = dataSource;
			this._localDBInstance = LocalDBAPI.GetLocalDbInstanceNameFromServerName(this._dataSource);
			this._failoverPartner = connectionOptions._failoverPartner;
			this._initialCatalog = connectionOptions._initialCatalog;
			this._password = connectionOptions._password;
			this._userID = connectionOptions._userID;
			this._workstationId = connectionOptions._workstationId;
			this._expandedAttachDBFilename = connectionOptions._expandedAttachDBFilename;
			this._typeSystemVersion = connectionOptions._typeSystemVersion;
			this._transactionBinding = connectionOptions._transactionBinding;
			this._applicationIntent = connectionOptions._applicationIntent;
			this._connectRetryCount = connectionOptions._connectRetryCount;
			this._connectRetryInterval = connectionOptions._connectRetryInterval;
			this._authType = connectionOptions._authType;
			this._columnEncryptionSetting = connectionOptions._columnEncryptionSetting;
			this._enclaveAttestationUrl = connectionOptions._enclaveAttestationUrl;
			this._attestationProtocol = connectionOptions._attestationProtocol;
			this._serverSPN = connectionOptions._serverSPN;
			this._failoverPartnerSPN = connectionOptions._failoverPartnerSPN;
			this._hostNameInCertificate = connectionOptions._hostNameInCertificate;
			this._connectionReset = connectionOptions._connectionReset;
			this._contextConnection = connectionOptions._contextConnection;
			this._transparentNetworkIPResolution = connectionOptions._transparentNetworkIPResolution;
			this._networkLibrary = connectionOptions._networkLibrary;
			this._typeSystemAssemblyVersion = connectionOptions._typeSystemAssemblyVersion;
			this.ValidateValueLength(this._dataSource, 128, "Data Source");
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00018D25 File Offset: 0x00016F25
		internal bool IntegratedSecurity
		{
			get
			{
				return this._integratedSecurity;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal bool Asynchronous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal bool ConnectionReset
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00018D2D File Offset: 0x00016F2D
		internal SqlConnectionEncryptOption Encrypt
		{
			get
			{
				return this._encrypt;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00018D35 File Offset: 0x00016F35
		internal string HostNameInCertificate
		{
			get
			{
				return this._hostNameInCertificate;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00018D3D File Offset: 0x00016F3D
		internal bool TrustServerCertificate
		{
			get
			{
				return this._trustServerCertificate;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00018D45 File Offset: 0x00016F45
		public string ServerCertificate
		{
			get
			{
				return this._serverCertificate;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00018D4D File Offset: 0x00016F4D
		internal bool Enlist
		{
			get
			{
				return this._enlist;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00018D55 File Offset: 0x00016F55
		internal bool MARS
		{
			get
			{
				return this._mars;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00018D5D File Offset: 0x00016F5D
		internal bool MultiSubnetFailover
		{
			get
			{
				return this._multiSubnetFailover;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00018D65 File Offset: 0x00016F65
		internal SqlAuthenticationMethod Authentication
		{
			get
			{
				return this._authType;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00018D6D File Offset: 0x00016F6D
		internal SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting
		{
			get
			{
				return this._columnEncryptionSetting;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00018D75 File Offset: 0x00016F75
		internal string EnclaveAttestationUrl
		{
			get
			{
				return this._enclaveAttestationUrl;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00018D7D File Offset: 0x00016F7D
		internal SqlConnectionAttestationProtocol AttestationProtocol
		{
			get
			{
				return this._attestationProtocol;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00018D85 File Offset: 0x00016F85
		internal SqlConnectionIPAddressPreference IPAddressPreference
		{
			get
			{
				return this._ipAddressPreference;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00018D8D File Offset: 0x00016F8D
		internal bool PersistSecurityInfo
		{
			get
			{
				return this._persistSecurityInfo;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00018D95 File Offset: 0x00016F95
		internal bool Pooling
		{
			get
			{
				return this._pooling;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00018D9D File Offset: 0x00016F9D
		internal bool Replication
		{
			get
			{
				return this._replication;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00018DA5 File Offset: 0x00016FA5
		internal bool UserInstance
		{
			get
			{
				return this._userInstance;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00018DAD File Offset: 0x00016FAD
		internal int CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00018DB5 File Offset: 0x00016FB5
		internal int ConnectTimeout
		{
			get
			{
				return this._connectTimeout;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00018DBD File Offset: 0x00016FBD
		internal int LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00018DC5 File Offset: 0x00016FC5
		internal int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00018DCD File Offset: 0x00016FCD
		internal int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00018DD5 File Offset: 0x00016FD5
		internal int PacketSize
		{
			get
			{
				return this._packetSize;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00018DDD File Offset: 0x00016FDD
		internal int ConnectRetryCount
		{
			get
			{
				return this._connectRetryCount;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00018DE5 File Offset: 0x00016FE5
		internal int ConnectRetryInterval
		{
			get
			{
				return this._connectRetryInterval;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00018DED File Offset: 0x00016FED
		internal ApplicationIntent ApplicationIntent
		{
			get
			{
				return this._applicationIntent;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00018DF5 File Offset: 0x00016FF5
		internal string ApplicationName
		{
			get
			{
				return this._applicationName;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00018DFD File Offset: 0x00016FFD
		internal string AttachDBFilename
		{
			get
			{
				return this._attachDBFileName;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00018E05 File Offset: 0x00017005
		internal string CurrentLanguage
		{
			get
			{
				return this._currentLanguage;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00018E0D File Offset: 0x0001700D
		internal string DataSource
		{
			get
			{
				return this._dataSource;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00018E15 File Offset: 0x00017015
		internal string LocalDBInstance
		{
			get
			{
				return this._localDBInstance;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00018E1D File Offset: 0x0001701D
		internal string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00018E25 File Offset: 0x00017025
		internal string InitialCatalog
		{
			get
			{
				return this._initialCatalog;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00018E2D File Offset: 0x0001702D
		internal string Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00018E35 File Offset: 0x00017035
		internal string UserID
		{
			get
			{
				return this._userID;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00018E3D File Offset: 0x0001703D
		internal string WorkstationId
		{
			get
			{
				return this._workstationId;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00018E45 File Offset: 0x00017045
		internal PoolBlockingPeriod PoolBlockingPeriod
		{
			get
			{
				return this._poolBlockingPeriod;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00018E4D File Offset: 0x0001704D
		internal string ServerSPN
		{
			get
			{
				return this._serverSPN;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00018E55 File Offset: 0x00017055
		internal string FailoverPartnerSPN
		{
			get
			{
				return this._failoverPartnerSPN;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00018E5D File Offset: 0x0001705D
		internal SqlConnectionString.TypeSystem TypeSystemVersion
		{
			get
			{
				return this._typeSystemVersion;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00018E65 File Offset: 0x00017065
		internal Version TypeSystemAssemblyVersion
		{
			get
			{
				return this._typeSystemAssemblyVersion;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00018E6D File Offset: 0x0001706D
		internal SqlConnectionString.TransactionBindingEnum TransactionBinding
		{
			get
			{
				return this._transactionBinding;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00018E75 File Offset: 0x00017075
		internal bool EnforceLocalHost
		{
			get
			{
				return this._expandedAttachDBFilename != null && this._localDBInstance == null;
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00018E8A File Offset: 0x0001708A
		protected internal override string Expand()
		{
			if (this._expandedAttachDBFilename != null)
			{
				return base.ExpandKeyword("AttachDbFilename", this._expandedAttachDBFilename);
			}
			return base.Expand();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00018EAC File Offset: 0x000170AC
		private static bool CompareHostName(ref string host, string name, bool fixup)
		{
			bool flag = false;
			if (host.Equals(name, StringComparison.OrdinalIgnoreCase))
			{
				if (fixup)
				{
					host = ".";
				}
				flag = true;
			}
			else if (host.StartsWith(name + "\\", StringComparison.OrdinalIgnoreCase))
			{
				if (fixup)
				{
					host = "." + host.Substring(name.Length);
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00018F08 File Offset: 0x00017108
		internal static Dictionary<string, string> GetParseSynonyms()
		{
			Dictionary<string, string> dictionary = SqlConnectionString.s_sqlClientSynonyms;
			if (dictionary == null)
			{
				int num = 77;
				dictionary = new Dictionary<string, string>(num, StringComparer.OrdinalIgnoreCase)
				{
					{ "Application Intent", "Application Intent" },
					{ "Application Name", "Application Name" },
					{ "AttachDbFilename", "AttachDbFilename" },
					{ "Pool Blocking Period", "Pool Blocking Period" },
					{ "Command Timeout", "Command Timeout" },
					{ "Connect Timeout", "Connect Timeout" },
					{ "Connection Reset", "Connection Reset" },
					{ "Context Connection", "Context Connection" },
					{ "Current Language", "Current Language" },
					{ "Data Source", "Data Source" },
					{ "Encrypt", "Encrypt" },
					{ "Enlist", "Enlist" },
					{ "Failover Partner", "Failover Partner" },
					{ "Host Name In Certificate", "Host Name In Certificate" },
					{ "Server Certificate", "Server Certificate" },
					{ "Initial Catalog", "Initial Catalog" },
					{ "Integrated Security", "Integrated Security" },
					{ "Load Balance Timeout", "Load Balance Timeout" },
					{ "Multiple Active Result Sets", "Multiple Active Result Sets" },
					{ "Max Pool Size", "Max Pool Size" },
					{ "Min Pool Size", "Min Pool Size" },
					{ "Multi Subnet Failover", "Multi Subnet Failover" },
					{ "Network Library", "Network Library" },
					{ "Packet Size", "Packet Size" },
					{ "Password", "Password" },
					{ "Persist Security Info", "Persist Security Info" },
					{ "Pooling", "Pooling" },
					{ "Replication", "Replication" },
					{ "Trust Server Certificate", "Trust Server Certificate" },
					{ "Transaction Binding", "Transaction Binding" },
					{ "Type System Version", "Type System Version" },
					{ "Column Encryption Setting", "Column Encryption Setting" },
					{ "Enclave Attestation Url", "Enclave Attestation Url" },
					{ "Attestation Protocol", "Attestation Protocol" },
					{ "User ID", "User ID" },
					{ "User Instance", "User Instance" },
					{ "Workstation ID", "Workstation ID" },
					{ "Connect Retry Count", "Connect Retry Count" },
					{ "Connect Retry Interval", "Connect Retry Interval" },
					{ "Authentication", "Authentication" },
					{ "IP Address Preference", "IP Address Preference" },
					{ "Server SPN", "Server SPN" },
					{ "Failover Partner SPN", "Failover Partner SPN" },
					{ "app", "Application Name" },
					{ "applicationintent", "Application Intent" },
					{ "extended properties", "AttachDbFilename" },
					{ "hostnameincertificate", "Host Name In Certificate" },
					{ "servercertificate", "Server Certificate" },
					{ "initial file name", "AttachDbFilename" },
					{ "connectretrycount", "Connect Retry Count" },
					{ "connectretryinterval", "Connect Retry Interval" },
					{ "connection timeout", "Connect Timeout" },
					{ "timeout", "Connect Timeout" },
					{ "language", "Current Language" },
					{ "addr", "Data Source" },
					{ "address", "Data Source" },
					{ "multipleactiveresultsets", "Multiple Active Result Sets" },
					{ "multisubnetfailover", "Multi Subnet Failover" },
					{ "network address", "Data Source" },
					{ "poolblockingperiod", "Pool Blocking Period" },
					{ "server", "Data Source" },
					{ "database", "Initial Catalog" },
					{ "trusted_connection", "Integrated Security" },
					{ "connection lifetime", "Load Balance Timeout" },
					{ "net", "Network Library" },
					{ "network", "Network Library" },
					{ "pwd", "Password" },
					{ "persistsecurityinfo", "Persist Security Info" },
					{ "trustservercertificate", "Trust Server Certificate" },
					{ "uid", "User ID" },
					{ "user", "User ID" },
					{ "wsid", "Workstation ID" },
					{ "ServerSPN", "Server SPN" },
					{ "FailoverPartnerSPN", "Failover Partner SPN" },
					{ "Transparent Network IP Resolution", "Transparent Network IP Resolution" },
					{ "transparentnetworkipresolution", "Transparent Network IP Resolution" },
					{ "ipaddresspreference", "IP Address Preference" }
				};
				Interlocked.CompareExchange<Dictionary<string, string>>(ref SqlConnectionString.s_sqlClientSynonyms, dictionary, null);
			}
			return dictionary;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00019410 File Offset: 0x00017610
		internal string ObtainWorkstationId()
		{
			string text = this.WorkstationId;
			if (text == null)
			{
				text = ADP.MachineName();
				this.ValidateValueLength(text, 128, "Workstation ID");
			}
			return text;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001943F File Offset: 0x0001763F
		private void ValidateValueLength(string value, int limit, string key)
		{
			if (limit < value.Length)
			{
				throw ADP.InvalidConnectionOptionValueLength(key, limit);
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00019454 File Offset: 0x00017654
		internal static void VerifyLocalHostAndFixup(ref string host, bool enforceLocalHost, bool fixup)
		{
			if (string.IsNullOrEmpty(host))
			{
				if (fixup)
				{
					host = ".";
					return;
				}
			}
			else if (!SqlConnectionString.CompareHostName(ref host, ".", fixup) && !SqlConnectionString.CompareHostName(ref host, "(local)", fixup))
			{
				string computerNameDnsFullyQualified = SqlConnectionString.GetComputerNameDnsFullyQualified();
				if (!SqlConnectionString.CompareHostName(ref host, computerNameDnsFullyQualified, fixup))
				{
					int num = computerNameDnsFullyQualified.IndexOf('.');
					if ((num <= 0 || !SqlConnectionString.CompareHostName(ref host, computerNameDnsFullyQualified.Substring(0, num), fixup)) && enforceLocalHost)
					{
						throw ADP.InvalidConnectionOptionValue("AttachDbFilename");
					}
				}
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000194D0 File Offset: 0x000176D0
		private static string GetComputerNameDnsFullyQualified()
		{
			string text3;
			try
			{
				string text = "." + IPGlobalProperties.GetIPGlobalProperties().DomainName;
				string text2 = Dns.GetHostName();
				if (text != "." && !text2.EndsWith(text))
				{
					text2 += text;
				}
				text3 = text2;
			}
			catch (SocketException)
			{
				text3 = Environment.MachineName;
			}
			return text3;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00019534 File Offset: 0x00017734
		internal ApplicationIntent ConvertValueToApplicationIntent()
		{
			string text;
			if (!base.TryGetParsetableValue("Application Intent", out text))
			{
				return ApplicationIntent.ReadWrite;
			}
			ApplicationIntent applicationIntent;
			try
			{
				applicationIntent = DbConnectionStringBuilderUtil.ConvertToApplicationIntent("Application Intent", text);
			}
			catch (FormatException ex)
			{
				throw ADP.InvalidConnectionOptionValue("Application Intent", ex);
			}
			catch (OverflowException ex2)
			{
				throw ADP.InvalidConnectionOptionValue("Application Intent", ex2);
			}
			return applicationIntent;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00019598 File Offset: 0x00017798
		internal SqlAuthenticationMethod ConvertValueToAuthenticationType()
		{
			string text;
			if (!base.TryGetParsetableValue("Authentication", out text))
			{
				return SqlConnectionString.DEFAULT.Authentication;
			}
			SqlAuthenticationMethod sqlAuthenticationMethod;
			try
			{
				sqlAuthenticationMethod = DbConnectionStringBuilderUtil.ConvertToAuthenticationType("Authentication", text);
			}
			catch (FormatException ex)
			{
				throw ADP.InvalidConnectionOptionValue("Authentication", ex);
			}
			catch (OverflowException ex2)
			{
				throw ADP.InvalidConnectionOptionValue("Authentication", ex2);
			}
			return sqlAuthenticationMethod;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00019600 File Offset: 0x00017800
		internal SqlConnectionColumnEncryptionSetting ConvertValueToColumnEncryptionSetting()
		{
			string text;
			if (!base.TryGetParsetableValue("Column Encryption Setting", out text))
			{
				return SqlConnectionColumnEncryptionSetting.Disabled;
			}
			SqlConnectionColumnEncryptionSetting sqlConnectionColumnEncryptionSetting;
			try
			{
				sqlConnectionColumnEncryptionSetting = DbConnectionStringBuilderUtil.ConvertToColumnEncryptionSetting("Column Encryption Setting", text);
			}
			catch (FormatException ex)
			{
				throw ADP.InvalidConnectionOptionValue("Column Encryption Setting", ex);
			}
			catch (OverflowException ex2)
			{
				throw ADP.InvalidConnectionOptionValue("Column Encryption Setting", ex2);
			}
			return sqlConnectionColumnEncryptionSetting;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00019664 File Offset: 0x00017864
		internal SqlConnectionAttestationProtocol ConvertValueToAttestationProtocol()
		{
			string text;
			if (!base.TryGetParsetableValue("Attestation Protocol", out text))
			{
				return SqlConnectionString.DEFAULT.AttestationProtocol;
			}
			SqlConnectionAttestationProtocol sqlConnectionAttestationProtocol;
			try
			{
				sqlConnectionAttestationProtocol = DbConnectionStringBuilderUtil.ConvertToAttestationProtocol("Attestation Protocol", text);
			}
			catch (FormatException ex)
			{
				throw ADP.InvalidConnectionOptionValue("Attestation Protocol", ex);
			}
			catch (OverflowException ex2)
			{
				throw ADP.InvalidConnectionOptionValue("Attestation Protocol", ex2);
			}
			return sqlConnectionAttestationProtocol;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000196CC File Offset: 0x000178CC
		internal SqlConnectionIPAddressPreference ConvertValueToIPAddressPreference()
		{
			string text;
			if (!base.TryGetParsetableValue("IP Address Preference", out text))
			{
				return SqlConnectionString.DEFAULT.IpAddressPreference;
			}
			SqlConnectionIPAddressPreference sqlConnectionIPAddressPreference;
			try
			{
				sqlConnectionIPAddressPreference = DbConnectionStringBuilderUtil.ConvertToIPAddressPreference("IP Address Preference", text);
			}
			catch (FormatException ex)
			{
				throw ADP.InvalidConnectionOptionValue("IP Address Preference", ex);
			}
			catch (OverflowException ex2)
			{
				throw ADP.InvalidConnectionOptionValue("IP Address Preference", ex2);
			}
			return sqlConnectionIPAddressPreference;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00019734 File Offset: 0x00017934
		internal PoolBlockingPeriod ConvertValueToPoolBlockingPeriod()
		{
			string text;
			if (!base.TryGetParsetableValue("Pool Blocking Period", out text))
			{
				return PoolBlockingPeriod.Auto;
			}
			PoolBlockingPeriod poolBlockingPeriod;
			try
			{
				poolBlockingPeriod = DbConnectionStringBuilderUtil.ConvertToPoolBlockingPeriod("Pool Blocking Period", text);
			}
			catch (Exception ex) when (ex is FormatException || ex is OverflowException)
			{
				throw ADP.InvalidConnectionOptionValue("Pool Blocking Period", ex);
			}
			return poolBlockingPeriod;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000197A8 File Offset: 0x000179A8
		internal SqlConnectionEncryptOption ConvertValueToSqlConnectionEncrypt()
		{
			string text;
			if (!base.TryGetParsetableValue("Encrypt", out text))
			{
				return SqlConnectionString.DEFAULT.Encrypt;
			}
			SqlConnectionEncryptOption sqlConnectionEncryptOption;
			try
			{
				sqlConnectionEncryptOption = DbConnectionStringBuilderUtil.ConvertToSqlConnectionEncryptOption("Encrypt", text);
			}
			catch (FormatException ex)
			{
				throw ADP.InvalidConnectionOptionValue("Encrypt", ex);
			}
			catch (OverflowException ex2)
			{
				throw ADP.InvalidConnectionOptionValue("Encrypt", ex2);
			}
			return sqlConnectionEncryptOption;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00019810 File Offset: 0x00017A10
		internal static Hashtable NetlibMapping()
		{
			Hashtable hashtable = SqlConnectionString.s_netlibMapping;
			if (hashtable == null)
			{
				hashtable = new Hashtable(8)
				{
					{ "dbmssocn", "tcp" },
					{ "dbnmpntw", "np" },
					{ "dbmsrpcn", "rpc" },
					{ "dbmsvinn", "bv" },
					{ "dbmsadsn", "adsp" },
					{ "dbmsspxn", "spx" },
					{ "dbmsgnet", "via" },
					{ "dbmslpcn", "lpc" }
				};
				SqlConnectionString.s_netlibMapping = hashtable;
			}
			return hashtable;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000198B8 File Offset: 0x00017AB8
		internal static bool ValidProtocol(string protocol)
		{
			return protocol == "tcp" || protocol == "np" || protocol == "via" || protocol == "lpc";
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00019900 File Offset: 0x00017B00
		protected internal override PermissionSet CreatePermissionSet()
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new SqlClientPermission(this));
			return permissionSet;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00019924 File Offset: 0x00017B24
		internal SqlConnectionEncryptOption ConvertValueToEncrypt()
		{
			SqlConnectionEncryptOption sqlConnectionEncryptOption = ((!base.Parsetable.ContainsKey("Authentication")) ? SqlConnectionString.DEFAULT.Encrypt : SqlConnectionEncryptOption.Mandatory);
			return this.ConvertValueToSqlConnectionEncrypt();
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00019956 File Offset: 0x00017B56
		internal bool ContextConnection
		{
			get
			{
				return this._contextConnection;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0001995E File Offset: 0x00017B5E
		internal bool TransparentNetworkIPResolution
		{
			get
			{
				return this._transparentNetworkIPResolution;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00019966 File Offset: 0x00017B66
		internal string NetworkLibrary
		{
			get
			{
				return this._networkLibrary;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000021D8 File Offset: 0x000003D8
		internal string Certificate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0001996E File Offset: 0x00017B6E
		internal bool UsesCertificate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400019B RID: 411
		internal const int SynonymCount = 33;

		// Token: 0x0400019C RID: 412
		private static Dictionary<string, string> s_sqlClientSynonyms;

		// Token: 0x0400019D RID: 413
		private readonly bool _integratedSecurity;

		// Token: 0x0400019E RID: 414
		private readonly SqlConnectionEncryptOption _encrypt;

		// Token: 0x0400019F RID: 415
		private readonly bool _trustServerCertificate;

		// Token: 0x040001A0 RID: 416
		private readonly bool _enlist;

		// Token: 0x040001A1 RID: 417
		private readonly bool _mars;

		// Token: 0x040001A2 RID: 418
		private readonly bool _persistSecurityInfo;

		// Token: 0x040001A3 RID: 419
		private readonly PoolBlockingPeriod _poolBlockingPeriod;

		// Token: 0x040001A4 RID: 420
		private readonly bool _pooling;

		// Token: 0x040001A5 RID: 421
		private readonly bool _replication;

		// Token: 0x040001A6 RID: 422
		private readonly bool _userInstance;

		// Token: 0x040001A7 RID: 423
		private readonly bool _multiSubnetFailover;

		// Token: 0x040001A8 RID: 424
		private readonly SqlAuthenticationMethod _authType;

		// Token: 0x040001A9 RID: 425
		private readonly SqlConnectionColumnEncryptionSetting _columnEncryptionSetting;

		// Token: 0x040001AA RID: 426
		private readonly string _enclaveAttestationUrl;

		// Token: 0x040001AB RID: 427
		private readonly SqlConnectionAttestationProtocol _attestationProtocol;

		// Token: 0x040001AC RID: 428
		private readonly SqlConnectionIPAddressPreference _ipAddressPreference;

		// Token: 0x040001AD RID: 429
		private readonly int _commandTimeout;

		// Token: 0x040001AE RID: 430
		private readonly int _connectTimeout;

		// Token: 0x040001AF RID: 431
		private readonly int _loadBalanceTimeout;

		// Token: 0x040001B0 RID: 432
		private readonly int _maxPoolSize;

		// Token: 0x040001B1 RID: 433
		private readonly int _minPoolSize;

		// Token: 0x040001B2 RID: 434
		private readonly int _packetSize;

		// Token: 0x040001B3 RID: 435
		private readonly int _connectRetryCount;

		// Token: 0x040001B4 RID: 436
		private readonly int _connectRetryInterval;

		// Token: 0x040001B5 RID: 437
		private readonly ApplicationIntent _applicationIntent;

		// Token: 0x040001B6 RID: 438
		private readonly string _applicationName;

		// Token: 0x040001B7 RID: 439
		private readonly string _attachDBFileName;

		// Token: 0x040001B8 RID: 440
		private readonly string _currentLanguage;

		// Token: 0x040001B9 RID: 441
		private readonly string _dataSource;

		// Token: 0x040001BA RID: 442
		private readonly string _localDBInstance;

		// Token: 0x040001BB RID: 443
		private readonly string _failoverPartner;

		// Token: 0x040001BC RID: 444
		private readonly string _initialCatalog;

		// Token: 0x040001BD RID: 445
		private readonly string _password;

		// Token: 0x040001BE RID: 446
		private readonly string _userID;

		// Token: 0x040001BF RID: 447
		private readonly string _hostNameInCertificate;

		// Token: 0x040001C0 RID: 448
		private readonly string _serverCertificate;

		// Token: 0x040001C1 RID: 449
		private readonly string _serverSPN;

		// Token: 0x040001C2 RID: 450
		private readonly string _failoverPartnerSPN;

		// Token: 0x040001C3 RID: 451
		private readonly string _workstationId;

		// Token: 0x040001C4 RID: 452
		private readonly SqlConnectionString.TransactionBindingEnum _transactionBinding;

		// Token: 0x040001C5 RID: 453
		private readonly SqlConnectionString.TypeSystem _typeSystemVersion;

		// Token: 0x040001C6 RID: 454
		private readonly Version _typeSystemAssemblyVersion;

		// Token: 0x040001C7 RID: 455
		private static readonly Version s_constTypeSystemAsmVersion10 = new Version("10.0.0.0");

		// Token: 0x040001C8 RID: 456
		private static readonly Version s_constTypeSystemAsmVersion11 = new Version("11.0.0.0");

		// Token: 0x040001C9 RID: 457
		private readonly string _expandedAttachDBFilename;

		// Token: 0x040001CA RID: 458
		private static Hashtable s_netlibMapping;

		// Token: 0x040001CB RID: 459
		private readonly bool _connectionReset;

		// Token: 0x040001CC RID: 460
		private readonly bool _contextConnection;

		// Token: 0x040001CD RID: 461
		private readonly bool _transparentNetworkIPResolution;

		// Token: 0x040001CE RID: 462
		private readonly string _networkLibrary;

		// Token: 0x020001BE RID: 446
		internal static class DEFAULT
		{
			// Token: 0x0400132E RID: 4910
			internal const ApplicationIntent ApplicationIntent = ApplicationIntent.ReadWrite;

			// Token: 0x0400132F RID: 4911
			internal const string Application_Name = "Framework Microsoft SqlClient Data Provider";

			// Token: 0x04001330 RID: 4912
			internal const string AttachDBFilename = "";

			// Token: 0x04001331 RID: 4913
			internal const int Command_Timeout = 30;

			// Token: 0x04001332 RID: 4914
			internal const int Connect_Timeout = 15;

			// Token: 0x04001333 RID: 4915
			internal const string Current_Language = "";

			// Token: 0x04001334 RID: 4916
			internal const string Data_Source = "";

			// Token: 0x04001335 RID: 4917
			internal static readonly SqlConnectionEncryptOption Encrypt = DbConnectionStringDefaults.Encrypt;

			// Token: 0x04001336 RID: 4918
			internal const string HostNameInCertificate = "";

			// Token: 0x04001337 RID: 4919
			internal const string ServerCertificate = "";

			// Token: 0x04001338 RID: 4920
			internal const bool Enlist = true;

			// Token: 0x04001339 RID: 4921
			internal const string FailoverPartner = "";

			// Token: 0x0400133A RID: 4922
			internal const string Initial_Catalog = "";

			// Token: 0x0400133B RID: 4923
			internal const bool Integrated_Security = false;

			// Token: 0x0400133C RID: 4924
			internal const int Load_Balance_Timeout = 0;

			// Token: 0x0400133D RID: 4925
			internal const bool MARS = false;

			// Token: 0x0400133E RID: 4926
			internal const int Max_Pool_Size = 100;

			// Token: 0x0400133F RID: 4927
			internal const int Min_Pool_Size = 0;

			// Token: 0x04001340 RID: 4928
			internal const bool MultiSubnetFailover = false;

			// Token: 0x04001341 RID: 4929
			internal const int Packet_Size = 8000;

			// Token: 0x04001342 RID: 4930
			internal const string Password = "";

			// Token: 0x04001343 RID: 4931
			internal const bool Persist_Security_Info = false;

			// Token: 0x04001344 RID: 4932
			internal const PoolBlockingPeriod PoolBlockingPeriod = PoolBlockingPeriod.Auto;

			// Token: 0x04001345 RID: 4933
			internal const bool Pooling = true;

			// Token: 0x04001346 RID: 4934
			internal const bool TrustServerCertificate = false;

			// Token: 0x04001347 RID: 4935
			internal const string Type_System_Version = "Latest";

			// Token: 0x04001348 RID: 4936
			internal const string User_ID = "";

			// Token: 0x04001349 RID: 4937
			internal const bool User_Instance = false;

			// Token: 0x0400134A RID: 4938
			internal const bool Replication = false;

			// Token: 0x0400134B RID: 4939
			internal const int Connect_Retry_Count = 1;

			// Token: 0x0400134C RID: 4940
			internal const int Connect_Retry_Interval = 10;

			// Token: 0x0400134D RID: 4941
			internal const string EnclaveAttestationUrl = "";

			// Token: 0x0400134E RID: 4942
			internal const SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Disabled;

			// Token: 0x0400134F RID: 4943
			internal static readonly SqlAuthenticationMethod Authentication = DbConnectionStringDefaults.Authentication;

			// Token: 0x04001350 RID: 4944
			internal static readonly SqlConnectionAttestationProtocol AttestationProtocol = SqlConnectionAttestationProtocol.NotSpecified;

			// Token: 0x04001351 RID: 4945
			internal static readonly SqlConnectionIPAddressPreference IpAddressPreference = SqlConnectionIPAddressPreference.IPv4First;

			// Token: 0x04001352 RID: 4946
			internal const string ServerSPN = "";

			// Token: 0x04001353 RID: 4947
			internal const string FailoverPartnerSPN = "";

			// Token: 0x04001354 RID: 4948
			internal static readonly bool TransparentNetworkIPResolution = DbConnectionStringDefaults.TransparentNetworkIPResolution;

			// Token: 0x04001355 RID: 4949
			internal const bool Connection_Reset = true;

			// Token: 0x04001356 RID: 4950
			internal const bool Context_Connection = false;

			// Token: 0x04001357 RID: 4951
			internal const string Network_Library = "";
		}

		// Token: 0x020001BF RID: 447
		internal static class KEY
		{
			// Token: 0x04001358 RID: 4952
			internal const string ApplicationIntent = "Application Intent";

			// Token: 0x04001359 RID: 4953
			internal const string Application_Name = "Application Name";

			// Token: 0x0400135A RID: 4954
			internal const string AttachDBFilename = "AttachDbFilename";

			// Token: 0x0400135B RID: 4955
			internal const string PoolBlockingPeriod = "Pool Blocking Period";

			// Token: 0x0400135C RID: 4956
			internal const string ColumnEncryptionSetting = "Column Encryption Setting";

			// Token: 0x0400135D RID: 4957
			internal const string EnclaveAttestationUrl = "Enclave Attestation Url";

			// Token: 0x0400135E RID: 4958
			internal const string AttestationProtocol = "Attestation Protocol";

			// Token: 0x0400135F RID: 4959
			internal const string IPAddressPreference = "IP Address Preference";

			// Token: 0x04001360 RID: 4960
			internal const string Command_Timeout = "Command Timeout";

			// Token: 0x04001361 RID: 4961
			internal const string Connect_Timeout = "Connect Timeout";

			// Token: 0x04001362 RID: 4962
			internal const string Connection_Reset = "Connection Reset";

			// Token: 0x04001363 RID: 4963
			internal const string Context_Connection = "Context Connection";

			// Token: 0x04001364 RID: 4964
			internal const string Current_Language = "Current Language";

			// Token: 0x04001365 RID: 4965
			internal const string Data_Source = "Data Source";

			// Token: 0x04001366 RID: 4966
			internal const string Encrypt = "Encrypt";

			// Token: 0x04001367 RID: 4967
			internal const string HostNameInCertificate = "Host Name In Certificate";

			// Token: 0x04001368 RID: 4968
			internal const string ServerCertificate = "Server Certificate";

			// Token: 0x04001369 RID: 4969
			internal const string Enlist = "Enlist";

			// Token: 0x0400136A RID: 4970
			internal const string FailoverPartner = "Failover Partner";

			// Token: 0x0400136B RID: 4971
			internal const string Initial_Catalog = "Initial Catalog";

			// Token: 0x0400136C RID: 4972
			internal const string Integrated_Security = "Integrated Security";

			// Token: 0x0400136D RID: 4973
			internal const string Load_Balance_Timeout = "Load Balance Timeout";

			// Token: 0x0400136E RID: 4974
			internal const string MARS = "Multiple Active Result Sets";

			// Token: 0x0400136F RID: 4975
			internal const string Max_Pool_Size = "Max Pool Size";

			// Token: 0x04001370 RID: 4976
			internal const string Min_Pool_Size = "Min Pool Size";

			// Token: 0x04001371 RID: 4977
			internal const string MultiSubnetFailover = "Multi Subnet Failover";

			// Token: 0x04001372 RID: 4978
			internal const string Network_Library = "Network Library";

			// Token: 0x04001373 RID: 4979
			internal const string Packet_Size = "Packet Size";

			// Token: 0x04001374 RID: 4980
			internal const string Password = "Password";

			// Token: 0x04001375 RID: 4981
			internal const string Persist_Security_Info = "Persist Security Info";

			// Token: 0x04001376 RID: 4982
			internal const string Pooling = "Pooling";

			// Token: 0x04001377 RID: 4983
			internal const string TransactionBinding = "Transaction Binding";

			// Token: 0x04001378 RID: 4984
			internal const string TrustServerCertificate = "Trust Server Certificate";

			// Token: 0x04001379 RID: 4985
			internal const string Type_System_Version = "Type System Version";

			// Token: 0x0400137A RID: 4986
			internal const string User_ID = "User ID";

			// Token: 0x0400137B RID: 4987
			internal const string User_Instance = "User Instance";

			// Token: 0x0400137C RID: 4988
			internal const string Workstation_Id = "Workstation ID";

			// Token: 0x0400137D RID: 4989
			internal const string Replication = "Replication";

			// Token: 0x0400137E RID: 4990
			internal const string Connect_Retry_Count = "Connect Retry Count";

			// Token: 0x0400137F RID: 4991
			internal const string Connect_Retry_Interval = "Connect Retry Interval";

			// Token: 0x04001380 RID: 4992
			internal const string Authentication = "Authentication";

			// Token: 0x04001381 RID: 4993
			internal const string Server_SPN = "Server SPN";

			// Token: 0x04001382 RID: 4994
			internal const string Failover_Partner_SPN = "Failover Partner SPN";

			// Token: 0x04001383 RID: 4995
			internal const string TransparentNetworkIPResolution = "Transparent Network IP Resolution";
		}

		// Token: 0x020001C0 RID: 448
		private static class SYNONYM
		{
			// Token: 0x04001384 RID: 4996
			internal const string IPADDRESSPREFERENCE = "ipaddresspreference";

			// Token: 0x04001385 RID: 4997
			internal const string APPLICATIONINTENT = "applicationintent";

			// Token: 0x04001386 RID: 4998
			internal const string APP = "app";

			// Token: 0x04001387 RID: 4999
			internal const string EXTENDED_PROPERTIES = "extended properties";

			// Token: 0x04001388 RID: 5000
			internal const string INITIAL_FILE_NAME = "initial file name";

			// Token: 0x04001389 RID: 5001
			internal const string CONNECTION_TIMEOUT = "connection timeout";

			// Token: 0x0400138A RID: 5002
			internal const string TIMEOUT = "timeout";

			// Token: 0x0400138B RID: 5003
			internal const string LANGUAGE = "language";

			// Token: 0x0400138C RID: 5004
			internal const string ADDR = "addr";

			// Token: 0x0400138D RID: 5005
			internal const string ADDRESS = "address";

			// Token: 0x0400138E RID: 5006
			internal const string SERVER = "server";

			// Token: 0x0400138F RID: 5007
			internal const string NETWORK_ADDRESS = "network address";

			// Token: 0x04001390 RID: 5008
			internal const string HOSTNAMEINCERTIFICATE = "hostnameincertificate";

			// Token: 0x04001391 RID: 5009
			internal const string SERVERCERTIFICATE = "servercertificate";

			// Token: 0x04001392 RID: 5010
			internal const string DATABASE = "database";

			// Token: 0x04001393 RID: 5011
			internal const string TRUSTED_CONNECTION = "trusted_connection";

			// Token: 0x04001394 RID: 5012
			internal const string CONNECTRETRYCOUNT = "connectretrycount";

			// Token: 0x04001395 RID: 5013
			internal const string CONNECTRETRYINTERVAL = "connectretryinterval";

			// Token: 0x04001396 RID: 5014
			internal const string Connection_Lifetime = "connection lifetime";

			// Token: 0x04001397 RID: 5015
			internal const string MULTIPLEACTIVERESULTSETS = "multipleactiveresultsets";

			// Token: 0x04001398 RID: 5016
			internal const string MULTISUBNETFAILOVER = "multisubnetfailover";

			// Token: 0x04001399 RID: 5017
			internal const string NET = "net";

			// Token: 0x0400139A RID: 5018
			internal const string NETWORK = "network";

			// Token: 0x0400139B RID: 5019
			internal const string POOLBLOCKINGPERIOD = "poolblockingperiod";

			// Token: 0x0400139C RID: 5020
			internal const string Pwd = "pwd";

			// Token: 0x0400139D RID: 5021
			internal const string PERSISTSECURITYINFO = "persistsecurityinfo";

			// Token: 0x0400139E RID: 5022
			internal const string TRUSTSERVERCERTIFICATE = "trustservercertificate";

			// Token: 0x0400139F RID: 5023
			internal const string UID = "uid";

			// Token: 0x040013A0 RID: 5024
			internal const string User = "user";

			// Token: 0x040013A1 RID: 5025
			internal const string WSID = "wsid";

			// Token: 0x040013A2 RID: 5026
			internal const string ServerSPN = "ServerSPN";

			// Token: 0x040013A3 RID: 5027
			internal const string FailoverPartnerSPN = "FailoverPartnerSPN";

			// Token: 0x040013A4 RID: 5028
			internal const string TRANSPARENTNETWORKIPRESOLUTION = "transparentnetworkipresolution";
		}

		// Token: 0x020001C1 RID: 449
		internal enum TypeSystem
		{
			// Token: 0x040013A6 RID: 5030
			Latest = 2008,
			// Token: 0x040013A7 RID: 5031
			SQLServer2000 = 2000,
			// Token: 0x040013A8 RID: 5032
			SQLServer2005 = 2005,
			// Token: 0x040013A9 RID: 5033
			SQLServer2008 = 2008,
			// Token: 0x040013AA RID: 5034
			SQLServer2012 = 2012
		}

		// Token: 0x020001C2 RID: 450
		internal static class TYPESYSTEMVERSION
		{
			// Token: 0x040013AB RID: 5035
			internal const string Latest = "Latest";

			// Token: 0x040013AC RID: 5036
			internal const string SQL_Server_2000 = "SQL Server 2000";

			// Token: 0x040013AD RID: 5037
			internal const string SQL_Server_2005 = "SQL Server 2005";

			// Token: 0x040013AE RID: 5038
			internal const string SQL_Server_2008 = "SQL Server 2008";

			// Token: 0x040013AF RID: 5039
			internal const string SQL_Server_2012 = "SQL Server 2012";
		}

		// Token: 0x020001C3 RID: 451
		internal enum TransactionBindingEnum
		{
			// Token: 0x040013B1 RID: 5041
			ImplicitUnbind,
			// Token: 0x040013B2 RID: 5042
			ExplicitUnbind
		}

		// Token: 0x020001C4 RID: 452
		internal static class TRANSACTIONBINDING
		{
			// Token: 0x040013B3 RID: 5043
			internal const string ImplicitUnbind = "Implicit Unbind";

			// Token: 0x040013B4 RID: 5044
			internal const string ExplicitUnbind = "Explicit Unbind";
		}

		// Token: 0x020001C5 RID: 453
		internal static class NETLIB
		{
			// Token: 0x040013B5 RID: 5045
			internal const string AppleTalk = "dbmsadsn";

			// Token: 0x040013B6 RID: 5046
			internal const string BanyanVines = "dbmsvinn";

			// Token: 0x040013B7 RID: 5047
			internal const string IPXSPX = "dbmsspxn";

			// Token: 0x040013B8 RID: 5048
			internal const string Multiprotocol = "dbmsrpcn";

			// Token: 0x040013B9 RID: 5049
			internal const string NamedPipes = "dbnmpntw";

			// Token: 0x040013BA RID: 5050
			internal const string SharedMemory = "dbmslpcn";

			// Token: 0x040013BB RID: 5051
			internal const string TCPIP = "dbmssocn";

			// Token: 0x040013BC RID: 5052
			internal const string VIA = "dbmsgnet";
		}
	}
}
