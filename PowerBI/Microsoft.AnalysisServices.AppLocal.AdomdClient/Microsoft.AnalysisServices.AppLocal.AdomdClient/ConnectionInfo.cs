using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using Microsoft.AnalysisServices.AdomdClient.Authentication;
using Microsoft.AnalysisServices.AdomdClient.Extensions;
using Microsoft.AnalysisServices.AdomdClient.Hosting;
using Microsoft.AnalysisServices.AdomdClient.Redirection;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200001A RID: 26
	internal sealed class ConnectionInfo
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000027AC File Offset: 0x000009AC
		static ConnectionInfo()
		{
			ClientHostingManager.UpdateProcessWithUserInterfaceStatus(new Func<bool>(WindowsRuntimeHelper.IsProcessWithUserInterface));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028C8 File Offset: 0x00000AC8
		public ConnectionInfo(string connectionString)
		{
			this.SetConnectionString(connectionString);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002908 File Offset: 0x00000B08
		internal ConnectionInfo()
		{
			this.ResetFields();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002947 File Offset: 0x00000B47
		internal ConnectionInfo(ConnectionInfo connectionInfo)
		{
			ConnectionInfo.CopyConnectionInfo(connectionInfo, this);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002987 File Offset: 0x00000B87
		public ConnectionType ConnectionType
		{
			get
			{
				return this.connectionType;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000298F File Offset: 0x00000B8F
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002997 File Offset: 0x00000B97
		internal void SetServer(string value)
		{
			this.server = ConnectionInfo.Trim(value);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000029A5 File Offset: 0x00000BA5
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029AD File Offset: 0x00000BAD
		internal void SetInstanceName(string value)
		{
			this.instanceName = ConnectionInfo.Trim(value);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000029BB File Offset: 0x00000BBB
		public string Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000029C3 File Offset: 0x00000BC3
		internal void SetPort(string value)
		{
			this.port = ConnectionInfo.Trim(value);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000029D1 File Offset: 0x00000BD1
		public string Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000029D9 File Offset: 0x00000BD9
		public string Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029E1 File Offset: 0x00000BE1
		internal void SetCatalog(string value)
		{
			this.catalog = value;
			this.InsertKeyValueIntoHash("Catalog", value);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000029F6 File Offset: 0x00000BF6
		public string UserID
		{
			get
			{
				return this.userID;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000029FE File Offset: 0x00000BFE
		public string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002A06 File Offset: 0x00000C06
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002A0E File Offset: 0x00000C0E
		public ProtocolFormat ProtocolFormat
		{
			get
			{
				return this.protocolFormat;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A16 File Offset: 0x00000C16
		public TransportCompression TransportCompression
		{
			get
			{
				return this.transportCompression;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002A1E File Offset: 0x00000C1E
		public int CompressionLevel
		{
			get
			{
				return this.compressionLevel;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002A26 File Offset: 0x00000C26
		public IntegratedSecurity IntegratedSecurity
		{
			get
			{
				return this.integratedSecurity;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002A2E File Offset: 0x00000C2E
		public string EncryptionPassword
		{
			get
			{
				return this.encryptionPassword;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002A36 File Offset: 0x00000C36
		public ImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.impersonationLevel;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002A3E File Offset: 0x00000C3E
		public string Sspi
		{
			get
			{
				return this.sspi;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002A46 File Offset: 0x00000C46
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002A4E File Offset: 0x00000C4E
		public HttpChannelHandling HttpHandling { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002A57 File Offset: 0x00000C57
		public bool UseExistingFile
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.UseExistingFile);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002A65 File Offset: 0x00000C65
		public Encoding CharacterEncoding
		{
			get
			{
				return this.characterEncoding;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002A6D File Offset: 0x00000C6D
		public int PacketSize
		{
			get
			{
				return this.packetSize;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002A75 File Offset: 0x00000C75
		public string ClientCertificateThumbprint
		{
			get
			{
				return this.clientCertificateThumbprint;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002A7D File Offset: 0x00000C7D
		public string Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002A85 File Offset: 0x00000C85
		public string AuthenticationScheme
		{
			get
			{
				return this.authenticationScheme;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002A8D File Offset: 0x00000C8D
		public string ExtAuthInfo
		{
			get
			{
				return this.extAuthInfo;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002A95 File Offset: 0x00000C95
		public string IdentityProvider
		{
			get
			{
				return this.identityProvider;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002A9D File Offset: 0x00000C9D
		public string BypassAuthorization
		{
			get
			{
				return this.bypassAuthorization;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002AA5 File Offset: 0x00000CA5
		public string RestrictCatalog
		{
			get
			{
				return this.restrictCatalog;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002AAD File Offset: 0x00000CAD
		public string AccessMode
		{
			get
			{
				return this.accessMode;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002AB5 File Offset: 0x00000CB5
		public string RestrictUser
		{
			get
			{
				return this.restrictUser;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002ABD File Offset: 0x00000CBD
		public string RestrictRoles
		{
			get
			{
				return this.restrictRoles;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002AC5 File Offset: 0x00000CC5
		public AsAzureRedirection AsAzureRedirection
		{
			get
			{
				return this.asAzureRedirection;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002ACD File Offset: 0x00000CCD
		public bool DedicatedAdminConnection
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.DedicatedAdminConnection);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002ADC File Offset: 0x00000CDC
		public string ContextualIdentity
		{
			get
			{
				return this.contextualIdentity;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public Guid ConnectionActivityId
		{
			get
			{
				return this.connectionActivityId;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002AEC File Offset: 0x00000CEC
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002AF4 File Offset: 0x00000CF4
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002AFC File Offset: 0x00000CFC
		public int ConnectTimeout
		{
			get
			{
				return this.connectTimeout;
			}
			set
			{
				this.connectTimeout = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002B05 File Offset: 0x00000D05
		public ListDictionary ExtendedProperties
		{
			get
			{
				return this.extendedProperties;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002B0D File Offset: 0x00000D0D
		internal AsInstanceType AsInstanceType
		{
			get
			{
				return this.asInstanceType;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002B15 File Offset: 0x00000D15
		internal bool IsAsAzure
		{
			get
			{
				return this.asInstanceType == AsInstanceType.AsAzure;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002B20 File Offset: 0x00000D20
		internal bool IsPbiPremiumInternal
		{
			get
			{
				return this.asInstanceType == AsInstanceType.PbiPremiumInternal;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002B2B File Offset: 0x00000D2B
		internal bool IsPbiPremiumXmlaEp
		{
			get
			{
				return this.asInstanceType == AsInstanceType.PbiPremiumXmlaEp;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002B36 File Offset: 0x00000D36
		internal bool IsPbiPremiumXmlaEpWithPowerBIEmbedToken
		{
			get
			{
				return this.asInstanceType == AsInstanceType.PbiPremiumXmlaEp && string.Compare(this.identityProvider, "PowerBIEmbed", StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002B57 File Offset: 0x00000D57
		internal bool IsPbiPremiumXmlaEpWithDataverseEmbedToken
		{
			get
			{
				return this.asInstanceType == AsInstanceType.PbiPremiumXmlaEp && string.Compare(this.identityProvider, "Dataverse", StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002B78 File Offset: 0x00000D78
		internal bool IsPbiDataset
		{
			get
			{
				return this.asInstanceType == AsInstanceType.PbiDataset;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002B83 File Offset: 0x00000D83
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002B8B File Offset: 0x00000D8B
		internal bool IsLinkReference { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002B94 File Offset: 0x00000D94
		internal bool IsPaaSInfrastructure
		{
			get
			{
				AsInstanceType asInstanceType = this.asInstanceType;
				return asInstanceType - AsInstanceType.AsAzure <= 2 || this.IsWorkloadDirectConnection;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002BB8 File Offset: 0x00000DB8
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002C22 File Offset: 0x00000E22
		internal string ConnectionString
		{
			get
			{
				if (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.RestrictConnectionString) && this.options.IsDisabled(ConnectionInfo.ConnectionOptions.PersistSecurityInfo) && this.options.IsEnabled(ConnectionInfo.ConnectionOptions.PasswordPresent))
				{
					if (string.IsNullOrEmpty(this.restrictedConnectionString))
					{
						this.restrictedConnectionString = ConnectionInfo.GetRestrictedConnectionString(this.connectionString);
					}
					return this.restrictedConnectionString;
				}
				return this.connectionString;
			}
			set
			{
				this.SetConnectionString(value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002C2B File Offset: 0x00000E2B
		internal AuthenticationHandle AuthHandle
		{
			get
			{
				return this.authHandle;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002C33 File Offset: 0x00000E33
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002C3B File Offset: 0x00000E3B
		internal string RoutingToken { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002C44 File Offset: 0x00000E44
		internal string SessionID
		{
			get
			{
				return this.sessionID;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002C4C File Offset: 0x00000E4C
		internal string ApplicationName
		{
			get
			{
				return (string)this.ExtendedProperties["SspropInitAppName"];
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002C64 File Offset: 0x00000E64
		internal Guid ClientActivityID
		{
			get
			{
				Guid clientActivityIDInThreadContext = this.ClientActivityIDInThreadContext;
				if (clientActivityIDInThreadContext != Guid.Empty)
				{
					return clientActivityIDInThreadContext;
				}
				return this.CurrentActivityID;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002C90 File Offset: 0x00000E90
		internal Guid CurrentActivityID
		{
			get
			{
				Guid currentActivityIDInThreadContext = this.CurrentActivityIDInThreadContext;
				if (currentActivityIDInThreadContext != Guid.Empty)
				{
					return currentActivityIDInThreadContext;
				}
				return this.ActivityIDInThreadContext;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002CB9 File Offset: 0x00000EB9
		internal string ServiceToServiceToken
		{
			get
			{
				return this.serviceToServiceToken;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002CC1 File Offset: 0x00000EC1
		internal Guid SourceCapacityObjectId
		{
			get
			{
				return this.sourceCapacityObjectId;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002CC9 File Offset: 0x00000EC9
		internal string ServicePrincipalProfileId
		{
			get
			{
				return this.servicePrincipalProfileId;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002CD1 File Offset: 0x00000ED1
		internal IntendedUsage IntendedUsage
		{
			get
			{
				return this.intendedUsage;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002CD9 File Offset: 0x00000ED9
		internal string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002CE1 File Offset: 0x00000EE1
		internal string LinkReferenceRawDataSource
		{
			get
			{
				return this.linkReferenceRawDataSource;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002CE9 File Offset: 0x00000EE9
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002CF1 File Offset: 0x00000EF1
		internal string DataSourceVersion
		{
			get
			{
				return this.dataSourceVersion;
			}
			set
			{
				this.dataSourceVersion = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002CFA File Offset: 0x00000EFA
		internal bool AllowAutoRedirect
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.AllowAutoRedirect);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002D0C File Offset: 0x00000F0C
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002D14 File Offset: 0x00000F14
		internal string PaaSInfrastructureServerName
		{
			get
			{
				return this.paasInfrastructureServerName;
			}
			set
			{
				this.paasInfrastructureServerName = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002D1D File Offset: 0x00000F1D
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002D25 File Offset: 0x00000F25
		internal string PbiPremiumWorkspaceName
		{
			get
			{
				return this.pbiPremiumWorkspaceName;
			}
			set
			{
				this.pbiPremiumWorkspaceName = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002D2E File Offset: 0x00000F2E
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002D36 File Offset: 0x00000F36
		internal string PbiPremiumWorkspaceObjectId
		{
			get
			{
				return this.pbiPremiumWorkspaceObjectId;
			}
			set
			{
				this.pbiPremiumWorkspaceObjectId = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002D3F File Offset: 0x00000F3F
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002D47 File Offset: 0x00000F47
		internal string PbipWorkloadResourceMoniker
		{
			get
			{
				return this.pbipWorkloadResourceMoniker;
			}
			set
			{
				this.pbipWorkloadResourceMoniker = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002D50 File Offset: 0x00000F50
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002D58 File Offset: 0x00000F58
		internal string PbipCoreServiceRoutingHint
		{
			get
			{
				return this.pbipCoreServiceRoutingHint;
			}
			set
			{
				this.pbipCoreServiceRoutingHint = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002D61 File Offset: 0x00000F61
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002D69 File Offset: 0x00000F69
		internal string PbiPremiumTenant
		{
			get
			{
				return this.pbiPremiumTenant;
			}
			set
			{
				this.pbiPremiumTenant = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002D72 File Offset: 0x00000F72
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002D7C File Offset: 0x00000F7C
		internal ConnectionAccessMode PbipAccessMode
		{
			get
			{
				return this.pbipAccessMode;
			}
			private set
			{
				switch (this.pbipAccessMode)
				{
				case ConnectionAccessMode.Default:
					this.pbipAccessMode = value;
					return;
				case ConnectionAccessMode.ReadWrite:
					if (value != ConnectionAccessMode.Default)
					{
						this.pbipAccessMode = value;
						return;
					}
					break;
				case ConnectionAccessMode.ReadOnly:
					if (value == ConnectionAccessMode.RestrictedReadOnly)
					{
						this.pbipAccessMode = value;
					}
					break;
				case ConnectionAccessMode.RestrictedReadOnly:
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002DC5 File Offset: 0x00000FC5
		internal string TransientModelMode
		{
			get
			{
				return this.transientModelMode;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002DCD File Offset: 0x00000FCD
		internal bool UseAadTokenInPublicXmlaEP
		{
			get
			{
				return PbiPremiumAuthenticationHandle.UseAadTokenInPublicXmlaEP || this.IsInternalTestInfrastructure;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002DDE File Offset: 0x00000FDE
		internal UserIdentityType UserIdentity
		{
			get
			{
				return this.userIdentity;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002DE6 File Offset: 0x00000FE6
		internal bool IsEmbedded
		{
			get
			{
				return string.Compare(this.server, "$Embedded$", StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002DFC File Offset: 0x00000FFC
		internal bool IsSspiAnonymous
		{
			get
			{
				return string.Compare(this.sspi, "Anonymous", StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002E12 File Offset: 0x00001012
		internal bool IsServerLocal
		{
			get
			{
				return this.server == ".";
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002E24 File Offset: 0x00001024
		internal bool IsForRedirector
		{
			get
			{
				return this.redirectorAddress != null;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002E2F File Offset: 0x0000102F
		internal bool IsForSqlBrowser
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.UsedForSqlBrowser);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002E44 File Offset: 0x00001044
		internal bool IsBinarySupported
		{
			get
			{
				AsInstanceType asInstanceType = this.asInstanceType;
				return asInstanceType - AsInstanceType.PbiPremiumInternal <= 1 || this.IsWorkloadDirectConnection || this.IsInternalTestInfrastructure;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002E70 File Offset: 0x00001070
		internal bool RestrictedClient
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.RestrictedClient);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002E7E File Offset: 0x0000107E
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00002E90 File Offset: 0x00001090
		internal bool RevertToProcessAccountForConnection
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.RevertToProcessAccountForConnection);
			}
			set
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.RevertToProcessAccountForConnection, value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002EA4 File Offset: 0x000010A4
		internal bool IsScaleOutAutoSyncEnabled
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ScaleOutAutoSync);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002EB6 File Offset: 0x000010B6
		internal bool AllowDelegation
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.AllowDelegation);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002EC8 File Offset: 0x000010C8
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002ED0 File Offset: 0x000010D0
		internal bool UseEU { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002ED9 File Offset: 0x000010D9
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002EE1 File Offset: 0x000010E1
		internal bool IsLightweightConnection { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002EEA File Offset: 0x000010EA
		internal uint AutoSyncPeriod
		{
			get
			{
				return this.autoSyncPeriod;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002EF2 File Offset: 0x000010F2
		internal ConnectTo ConnectTo
		{
			get
			{
				return this.connectTo;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002EFA File Offset: 0x000010FA
		internal SafetyOptions SafetyOptions
		{
			get
			{
				if (this.safetyOptions != SafetyOptions.Default)
				{
					return this.safetyOptions;
				}
				return SafetyOptions.Safe;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002F0C File Offset: 0x0000110C
		internal bool IsInternalPaaSInfrastructure
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.InternalPaaSInfrastructure);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002F1E File Offset: 0x0000111E
		internal bool IsInternalTestInfrastructure
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.InternalTestInfrastructure);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002F30 File Offset: 0x00001130
		internal bool IsWorkloadDirectConnection
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.WorkloadDirectConnection);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002F42 File Offset: 0x00001142
		internal string PBIPVirtualServiceName
		{
			get
			{
				return this.pbipVirtualServiceName;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002F4A File Offset: 0x0000114A
		internal string ContextualIdentityKey
		{
			get
			{
				return this.contextualIdentityKey;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002F52 File Offset: 0x00001152
		internal string ContextualIdentityType
		{
			get
			{
				return this.contextualIdentityType;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002F5A File Offset: 0x0000115A
		internal ListDictionary OriginalConnStringProps
		{
			get
			{
				return this.originalConnStringProps;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002F64 File Offset: 0x00001164
		internal static void ValidateSpecifiedEffectiveUserName(string effectiveUserName)
		{
			SecurityIdentifier user = WindowsIdentity.GetCurrent().User;
			SecurityIdentifier securityIdentifier = (SecurityIdentifier)new NTAccount(effectiveUserName).Translate(typeof(SecurityIdentifier));
			if (user.CompareTo(securityIdentifier) != 0)
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionString_LinkFileDupEffectiveUsername, null);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002FB2 File Offset: 0x000011B2
		internal static bool IsBism(string address)
		{
			return (ConnectivityHelper.IsHttpUri(address) || ConnectivityHelper.IsHttpsUri(address)) && new Uri(address).AbsolutePath.EndsWith(".bism", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002FE0 File Offset: 0x000011E0
		internal static ConnectionInfo GetModifiedConnectionInfo(ConnectionInfo info, string dataSource)
		{
			ConnectionInfo connectionInfo = new ConnectionInfo(info);
			connectionInfo.SetServer(dataSource);
			connectionInfo.SetInstanceName(null);
			connectionInfo.SetPort(null);
			connectionInfo.connectionType = ConnectionType.LocalCube;
			connectionInfo.connectTo = ConnectTo.SSAS21;
			connectionInfo.dataSource = dataSource;
			connectionInfo.catalog = string.Empty;
			if (connectionInfo.ExtendedProperties.Contains("Catalog"))
			{
				connectionInfo.ExtendedProperties.Remove("Catalog");
			}
			return connectionInfo;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003050 File Offset: 0x00001250
		internal void HandleAsAzureRedirection(IConnectivityOwner owner)
		{
			if (this.AsAzureRedirection == AsAzureRedirection.Disabled)
			{
				return;
			}
			Microsoft.AnalysisServices.AdomdClient.Authentication.AuthenticationManager.EnableStrongSecurityProtocols();
			Uri effectiveDataSource = this.GetEffectiveDataSource();
			RedirectionWorkspaceInfo redirectionWorkspaceInfo;
			ConnectionAccessMode connectionAccessMode;
			if (!this.ShouldRedirectAsAzureToPowerBIWorkspace(effectiveDataSource, out redirectionWorkspaceInfo, out connectionAccessMode))
			{
				return;
			}
			if (((string.IsNullOrEmpty(this.UserID) && !string.IsNullOrEmpty(this.Password)) || (owner != null && owner.AccessToken.IsValid)) && string.Compare(this.identityProvider, "PowerBIEmbed", StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new AdomdConnectionException(AuthenticationSR.Exception_RedirectionTokenAsPasswordIsNotSupported, null);
			}
			if (this.IsAccessTokenMissing(owner))
			{
				throw new AdomdConnectionException(RuntimeSR.NeedRefreshableToken, null);
			}
			AuthenticationHandle authenticationHandle = this.AcquireToken(owner, redirectionWorkspaceInfo.AasInstance, redirectionWorkspaceInfo.TenantId, true);
			string text = (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ApplyAuxiliaryPermission) ? this.ServiceToServiceToken : null);
			Guid effectiveActivityId = this.GetEffectiveActivityId();
			string asAzureRedirectedWorkspace = PbiPremiumAuthenticationHandle.GetAsAzureRedirectedWorkspace(redirectionWorkspaceInfo.PbiEndpoint, redirectionWorkspaceInfo.AasInstance, authenticationHandle, effectiveActivityId.ToString(), text);
			this.identityProvider = null;
			this.PaaSInfrastructureServerName = null;
			this.ExtractDataSourceParts(asAzureRedirectedWorkspace, null);
			this.ValidateAndCompleteConnectionConfiguration(owner);
			this.options.Enable(ConnectionInfo.ConnectionOptions.ResolvedAsAzureRedirection);
			this.PbipAccessMode = connectionAccessMode;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000318C File Offset: 0x0000138C
		internal void ExtractDataSourceParts(string dataSource, string location)
		{
			string empty = string.Empty;
			if (dataSource == "*")
			{
				this.SetServer("*");
				this.connectionType = ConnectionType.LocalServer;
				return;
			}
			if (ConnectionInfo.IsEmbeddedPath(dataSource))
			{
				if (string.IsNullOrEmpty(this.Location))
				{
					throw new ArgumentException(XmlaSR.ConnectionString_DataSourceNotSpecified);
				}
				this.SetServer("$Embedded$");
				this.connectionType = ConnectionType.LocalCube;
				return;
			}
			else
			{
				if (ConnectionInfo.IsLocalCubeFile(dataSource))
				{
					this.SetServer(dataSource);
					this.connectionType = ConnectionType.LocalCube;
					return;
				}
				if (ConnectivityHelper.IsHttpConnection(dataSource))
				{
					Uri uri = new Uri(dataSource);
					if (!uri.AbsolutePath.EndsWith(".bism", StringComparison.OrdinalIgnoreCase))
					{
						this.connectionType = ConnectionType.Http;
						if (uri.AbsolutePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) || uri.AbsolutePath.EndsWith(".xlsb", StringComparison.OrdinalIgnoreCase) || uri.AbsolutePath.EndsWith(".xlsm", StringComparison.OrdinalIgnoreCase))
						{
							string text;
							uri = new Uri(XmlaHttpUtility.ExtractSessionFromUrl(dataSource, out text));
							if (!string.IsNullOrEmpty(uri.Query))
							{
								throw new ArgumentException(XmlaSR.ConnectionString_DataSourceTypeDoesntSupportQuery);
							}
							dataSource = uri.GetLeftPart(UriPartial.Path);
							if (SPConnectivityManager.IsWorkbookInLegacyFarm(dataSource))
							{
								this.connectionType = ConnectionType.Wcf;
							}
							this.SetCatalog(null);
							this.sandboxPath = uri.AbsolutePath;
							if (ConnectivityHelper.IsHttpsUri(dataSource))
							{
								this.redirectorAddress = uri.GetLeftPart(UriPartial.Authority) + "/_vti_bin/PowerPivot/secureredirector.svc";
							}
							else
							{
								this.redirectorAddress = uri.GetLeftPart(UriPartial.Authority) + "/_vti_bin/PowerPivot/redirector.svc";
							}
						}
					}
					this.SetServer(dataSource);
					return;
				}
				string text2;
				string text3;
				string text4;
				ConnectionInfo.ExtractDataSourceStringParts(dataSource, out text2, out text3, out text4);
				if (!string.IsNullOrEmpty(location) && !location.Equals(dataSource))
				{
					string text5;
					string text6;
					string text7;
					ConnectionInfo.ExtractDataSourceStringParts(location, out text5, out text6, out text7);
					this.location = text5.Trim();
					if (string.IsNullOrEmpty(text4))
					{
						text4 = text7;
					}
					if (string.IsNullOrEmpty(text3))
					{
						text3 = text6;
					}
				}
				this.SetServer(text2);
				this.SetPort(text3);
				this.SetInstanceName(text4);
				this.connectionType = ConnectionType.Native;
				if (string.IsNullOrEmpty(this.server))
				{
					throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
				}
				return;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003388 File Offset: 0x00001588
		internal bool CanBuildExternalAuthenticationHandle(IConnectivityOwner owner)
		{
			return (owner != null && owner.AccessToken.IsValid) || (this.UserID == null && this.Password != null);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000033C0 File Offset: 0x000015C0
		internal void BuildExternalAuthenticationHandle(IConnectivityOwner owner)
		{
			if (!this.TryBuildExternalAuthenticationHandle(owner, out this.authHandle))
			{
				throw new AdomdConnectionException(RuntimeSR.NeedRefreshableToken, null);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000033F0 File Offset: 0x000015F0
		internal void RestrictConnectionString()
		{
			this.options.Enable(ConnectionInfo.ConnectionOptions.RestrictConnectionString);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003403 File Offset: 0x00001603
		internal bool IsSchannelSspi()
		{
			return string.Compare(this.sspi, "Schannel", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.sspi, "Microsoft Unified Security Protocol Provider", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000342E File Offset: 0x0000162E
		internal bool IsLinkFile()
		{
			return (ConnectivityHelper.IsHttpUri(this.server) || ConnectivityHelper.IsHttpsUri(this.server)) && new Uri(this.server).AbsolutePath.EndsWith(".bism", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000346C File Offset: 0x0000166C
		internal void ResolveLinkFileDataSource(IConnectivityOwner owner)
		{
			this.options.Disable(ConnectionInfo.ConnectionOptions.RevertToProcessAccountForConnection | ConnectionInfo.ConnectionOptions.AllowDelegation);
			string text;
			string text2;
			bool flag;
			bool flag2;
			if (this.connectionType == ConnectionType.Wcf)
			{
				if (!SPConnectivityManager.TryGetLinkFileMetadataInLegacyFarm(this.server, out text, out text2, out flag, out flag2))
				{
					throw new AdomdConnectionException(XmlaSR.InternalError, null);
				}
			}
			else
			{
				this.GetRemoteLinkFileMetadata(this.server, out text, out text2, out flag);
				flag2 = false;
			}
			if (flag2)
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionString_LinkFileParseError(4096), null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionString_LinkFileMissingServer, null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
			}
			if (ConnectivityHelper.HasUriProtocolScheme(text, "link"))
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionString_LinkFileInvalidServer, null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
			}
			if (ConnectivityHelper.IsHttpConnection(text) && new Uri(text).AbsolutePath.EndsWith(".bism", StringComparison.OrdinalIgnoreCase))
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionString_LinkFileInvalidServer, null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
			}
			this.options.Enable(ConnectionInfo.ConnectionOptions.ResolvedLinkFile);
			if (flag)
			{
				this.options.Enable(ConnectionInfo.ConnectionOptions.AllowDelegation);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				this.SetCatalog(text2);
			}
			this.ExtractDataSourceParts(text, null);
			this.ValidateAndCompleteConnectionConfiguration(owner);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003588 File Offset: 0x00001788
		internal void TryAddEffectiveUserName()
		{
			if (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.AllowDelegation) && this.ConnectionType != ConnectionType.Wcf)
			{
				if (!this.ExtendedProperties.Contains("EffectiveUserName"))
				{
					this.ExtendedProperties.Add("EffectiveUserName", WindowsIdentity.GetCurrent().Name);
				}
				else
				{
					ConnectionInfo.ValidateSpecifiedEffectiveUserName(this.ExtendedProperties["EffectiveUserName"].ToString());
				}
				this.options.Enable(ConnectionInfo.ConnectionOptions.RevertToProcessAccountForConnection);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000360C File Offset: 0x0000180C
		internal ConnectionInfo CloneForInstanceLookup()
		{
			ConnectionInfo connectionInfo = new ConnectionInfo();
			connectionInfo.server = this.server;
			if (this.IsSchannelSspi() && this.location != null)
			{
				connectionInfo.server = this.location;
			}
			connectionInfo.instanceName = null;
			connectionInfo.port = ((this.Port == null) ? 2382.ToString(CultureInfo.InvariantCulture) : this.Port);
			connectionInfo.location = this.location;
			connectionInfo.userID = this.userID;
			connectionInfo.password = this.password;
			connectionInfo.timeout = this.timeout;
			connectionInfo.connectTimeout = this.connectTimeout;
			connectionInfo.autoSyncPeriod = this.autoSyncPeriod;
			connectionInfo.catalog = null;
			connectionInfo.protectionLevel = this.protectionLevel;
			connectionInfo.connectTo = this.connectTo;
			connectionInfo.safetyOptions = this.safetyOptions;
			connectionInfo.protocolFormat = this.protocolFormat;
			connectionInfo.transportCompression = this.transportCompression;
			connectionInfo.compressionLevel = this.compressionLevel;
			connectionInfo.integratedSecurity = this.integratedSecurity;
			connectionInfo.encryptionPassword = this.encryptionPassword;
			connectionInfo.impersonationLevel = this.impersonationLevel;
			connectionInfo.sspi = "Negotiate";
			connectionInfo.HttpHandling = this.HttpHandling;
			connectionInfo.characterEncoding = this.characterEncoding;
			connectionInfo.packetSize = this.packetSize;
			connectionInfo.innerConnectionString = null;
			connectionInfo.IsLightweightConnection = false;
			connectionInfo.contextualIdentity = this.contextualIdentity;
			connectionInfo.connectionActivityId = this.connectionActivityId;
			connectionInfo.sourceCapacityObjectId = this.sourceCapacityObjectId;
			connectionInfo.servicePrincipalProfileId = this.servicePrincipalProfileId;
			connectionInfo.options = this.options.CloneForInstanceLookup();
			return connectionInfo;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000037B0 File Offset: 0x000019B0
		internal ConnectionInfo CloneForTraceChannel()
		{
			ConnectionInfo connectionInfo = new ConnectionInfo();
			ConnectionInfo.CopyConnectionInfo(this, connectionInfo);
			connectionInfo.timeout = 0;
			connectionInfo.HttpHandling = HttpChannelHandling.WebRequestBased;
			connectionInfo.IsLightweightConnection = true;
			return connectionInfo;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000037E0 File Offset: 0x000019E0
		internal string GetRedirectorUrlForDatabase()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append(this.redirectorAddress);
			stringBuilder.Append("/?DataSource=");
			stringBuilder.Append(Uri.EscapeDataString(this.sandboxPath));
			if (!string.IsNullOrEmpty(this.dataSourceVersion))
			{
				stringBuilder.Append("&DataSourceVersion=");
				stringBuilder.Append(Uri.EscapeDataString(this.dataSourceVersion));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003854 File Offset: 0x00001A54
		internal string GetRedirectorUrlForRedirect(string databaseId, bool specificVersion)
		{
			if (specificVersion)
			{
				return this.redirectorAddress + "/?DatabaseId=" + databaseId + "&SpecificVersion=true";
			}
			return this.redirectorAddress + "/?DatabaseId=" + databaseId;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003884 File Offset: 0x00001A84
		internal Uri DatasourceUriInitializeHelper()
		{
			if (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ResolvedAsAzureRedirection))
			{
				return new Uri(this.server);
			}
			if (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ResolvedLinkFile))
			{
				return new Uri(this.server);
			}
			if (this.IsPbiPremiumXmlaEp)
			{
				return new Uri(this.dataSource);
			}
			if (!this.IsForRedirector)
			{
				return new Uri(this.server);
			}
			return null;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000038F8 File Offset: 0x00001AF8
		internal string AcquireAADTokenAndResolveHTTPConnectionPropertiesForPaaSInfrastructure(IConnectivityOwner owner, ref Uri dataSourceUri)
		{
			if (this.IsAccessTokenMissing(owner))
			{
				throw new InvalidOperationException(RuntimeSR.NeedRefreshableToken);
			}
			string text;
			CloudConnectionAuthenticationProperties cloudConnectionAuthenticationProperties;
			this.ResolveHTTPConnectionPropertiesForPaaSInfrastructure(owner, ref dataSourceUri, true, false, false, null, out text, out cloudConnectionAuthenticationProperties);
			return text;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000392C File Offset: 0x00001B2C
		internal CloudConnectionAuthenticationProperties GetCloudConnectionAuthenticationPropertiesForPaaSInfrastructure(IConnectivityOwner owner)
		{
			Uri effectiveDataSource = this.GetEffectiveDataSource();
			Uri uri = this.DatasourceUriInitializeHelper();
			RedirectionWorkspaceInfo redirectionWorkspaceInfo;
			ConnectionAccessMode connectionAccessMode;
			bool flag = this.ShouldRedirectAsAzureToPowerBIWorkspace(effectiveDataSource, out redirectionWorkspaceInfo, out connectionAccessMode);
			string text;
			CloudConnectionAuthenticationProperties cloudConnectionAuthenticationProperties;
			this.ResolveHTTPConnectionPropertiesForPaaSInfrastructure(owner, ref uri, false, true, flag, redirectionWorkspaceInfo.TenantId, out text, out cloudConnectionAuthenticationProperties);
			if (flag)
			{
				cloudConnectionAuthenticationProperties.ResourceId = redirectionWorkspaceInfo.PbiResourceId;
			}
			return cloudConnectionAuthenticationProperties;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000397D File Offset: 0x00001B7D
		internal int GetTokensCacheUsage()
		{
			if (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.UseADTranslation))
			{
				return 2;
			}
			if (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.TokenCacheInDefaultMode))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000039A8 File Offset: 0x00001BA8
		private bool IsAccessTokenMissing(IConnectivityOwner owner)
		{
			return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.AccessTokenRequired) && !owner.AccessToken.IsValid;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000039DC File Offset: 0x00001BDC
		private static void CopyConnectionInfo(ConnectionInfo sourceInfo, ConnectionInfo destinationInfo)
		{
			destinationInfo.connectionString = sourceInfo.connectionString;
			destinationInfo.options = sourceInfo.options;
			destinationInfo.connectionType = sourceInfo.connectionType;
			destinationInfo.asInstanceType = sourceInfo.asInstanceType;
			destinationInfo.dataSource = sourceInfo.dataSource;
			destinationInfo.linkReferenceRawDataSource = sourceInfo.linkReferenceRawDataSource;
			destinationInfo.location = sourceInfo.location;
			destinationInfo.server = sourceInfo.server;
			destinationInfo.instanceName = sourceInfo.instanceName;
			destinationInfo.port = sourceInfo.port;
			destinationInfo.userID = sourceInfo.userID;
			destinationInfo.password = sourceInfo.password;
			destinationInfo.timeout = sourceInfo.timeout;
			destinationInfo.connectTimeout = sourceInfo.connectTimeout;
			destinationInfo.autoSyncPeriod = sourceInfo.autoSyncPeriod;
			destinationInfo.catalog = sourceInfo.catalog;
			destinationInfo.protectionLevel = sourceInfo.protectionLevel;
			destinationInfo.connectTo = sourceInfo.connectTo;
			destinationInfo.safetyOptions = sourceInfo.safetyOptions;
			destinationInfo.protocolFormat = sourceInfo.protocolFormat;
			destinationInfo.transportCompression = sourceInfo.transportCompression;
			destinationInfo.compressionLevel = sourceInfo.compressionLevel;
			destinationInfo.integratedSecurity = sourceInfo.integratedSecurity;
			destinationInfo.identityProvider = sourceInfo.identityProvider;
			destinationInfo.encryptionPassword = sourceInfo.encryptionPassword;
			destinationInfo.impersonationLevel = sourceInfo.ImpersonationLevel;
			destinationInfo.sspi = sourceInfo.sspi;
			destinationInfo.HttpHandling = sourceInfo.HttpHandling;
			destinationInfo.characterEncoding = sourceInfo.characterEncoding;
			destinationInfo.packetSize = sourceInfo.packetSize;
			destinationInfo.innerConnectionString = sourceInfo.innerConnectionString;
			destinationInfo.extendedProperties = ConnectionInfo.CloneListDictionary(sourceInfo.extendedProperties);
			destinationInfo.restrictedConnectionString = sourceInfo.restrictedConnectionString;
			destinationInfo.sessionID = sourceInfo.sessionID;
			destinationInfo.dataSourceVersion = sourceInfo.dataSourceVersion;
			destinationInfo.redirectorAddress = sourceInfo.redirectorAddress;
			destinationInfo.sandboxPath = sourceInfo.sandboxPath;
			destinationInfo.UseEU = sourceInfo.UseEU;
			destinationInfo.IsLightweightConnection = sourceInfo.IsLightweightConnection;
			destinationInfo.clientCertificateThumbprint = sourceInfo.clientCertificateThumbprint;
			destinationInfo.userIdentity = sourceInfo.userIdentity;
			destinationInfo.certificate = sourceInfo.certificate;
			destinationInfo.authenticationScheme = sourceInfo.authenticationScheme;
			destinationInfo.extAuthInfo = sourceInfo.extAuthInfo;
			destinationInfo.identityProvider = sourceInfo.identityProvider;
			destinationInfo.bypassAuthorization = sourceInfo.bypassAuthorization;
			destinationInfo.restrictCatalog = sourceInfo.restrictCatalog;
			destinationInfo.accessMode = sourceInfo.accessMode;
			destinationInfo.restrictUser = sourceInfo.restrictUser;
			destinationInfo.restrictRoles = sourceInfo.restrictRoles;
			destinationInfo.intendedUsage = sourceInfo.intendedUsage;
			destinationInfo.asAzureRedirection = sourceInfo.asAzureRedirection;
			destinationInfo.pbipAccessMode = sourceInfo.pbipAccessMode;
			destinationInfo.RoutingToken = sourceInfo.RoutingToken;
			destinationInfo.auxiliaryPermissionOwner = sourceInfo.auxiliaryPermissionOwner;
			destinationInfo.serviceToServiceToken = sourceInfo.serviceToServiceToken;
			destinationInfo.paasInfrastructureServerName = sourceInfo.paasInfrastructureServerName;
			destinationInfo.pbiPremiumTenant = sourceInfo.pbiPremiumTenant;
			destinationInfo.pbiPremiumWorkspaceName = sourceInfo.pbiPremiumWorkspaceName;
			destinationInfo.pbiPremiumWorkspaceObjectId = sourceInfo.pbiPremiumWorkspaceObjectId;
			destinationInfo.contextualIdentity = sourceInfo.contextualIdentity;
			destinationInfo.connectionActivityId = sourceInfo.connectionActivityId;
			destinationInfo.pbipWorkloadResourceMoniker = sourceInfo.pbipWorkloadResourceMoniker;
			destinationInfo.pbipCoreServiceRoutingHint = sourceInfo.pbipCoreServiceRoutingHint;
			destinationInfo.sourceCapacityObjectId = sourceInfo.sourceCapacityObjectId;
			destinationInfo.servicePrincipalProfileId = sourceInfo.servicePrincipalProfileId;
			destinationInfo.pbipVirtualServiceName = sourceInfo.pbipVirtualServiceName;
			destinationInfo.contextualIdentityKey = sourceInfo.contextualIdentityKey;
			destinationInfo.contextualIdentityType = sourceInfo.contextualIdentityType;
			destinationInfo.originalConnStringProps = ConnectionInfo.CloneListDictionary(sourceInfo.originalConnStringProps);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003D3C File Offset: 0x00001F3C
		private static void ExtractDataSourceStringParts(string dataSource, out string theServer, out string thePort, out string theInstance)
		{
			theServer = string.Empty;
			thePort = string.Empty;
			theInstance = string.Empty;
			string[] array = dataSource.Split(new char[] { '\\' });
			if (array.Length > 2)
			{
				throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
			}
			if (array.Length == 2)
			{
				theInstance = array[1].Trim();
				if (theInstance.Length == 0)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
				}
			}
			dataSource = array[0].Trim();
			array = dataSource.Split(new char[] { ':' });
			if (array.Length == 2 || array.Length == 9)
			{
				thePort = array[array.Length - 1].Trim();
				if (string.IsNullOrEmpty(thePort))
				{
					throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
				}
				int num = dataSource.LastIndexOf(':');
				theServer = dataSource.Substring(0, num).Trim();
				return;
			}
			else
			{
				if (array.Length > 9)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
				}
				thePort = string.Empty;
				theServer = dataSource;
				return;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003E28 File Offset: 0x00002028
		private static bool IsPassword(string key)
		{
			int i = 0;
			int num = ConnectionInfo.PasswordPropertyNames.Length;
			while (i < num)
			{
				if (string.Compare(key, ConnectionInfo.PasswordPropertyNames[i], StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003E5C File Offset: 0x0000205C
		private static bool IsExtendedProperties(string key)
		{
			return string.Compare(key, "Extended Properties", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003E70 File Offset: 0x00002070
		private static void RemoveDataSourceAccessMode(ref string dataSource, string accessMode, int index)
		{
			if (index + accessMode.Length < dataSource.Length && dataSource[index + accessMode.Length] == '&')
			{
				dataSource = dataSource.Remove(index, accessMode.Length + 1);
				return;
			}
			dataSource = dataSource.Remove(index - 1);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003EC0 File Offset: 0x000020C0
		private static bool ParseBool(string value)
		{
			value = ConnectionInfo.Trim(value);
			bool flag;
			if (ConvertHelper.TryParseBool(value, true, out flag))
			{
				return flag;
			}
			return value.StartsWith("y", StringComparison.OrdinalIgnoreCase) || value.StartsWith("t", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003F01 File Offset: 0x00002101
		private static string Trim(string str)
		{
			if (str == null)
			{
				return null;
			}
			str = str.Trim();
			if (str.Length == 0)
			{
				return null;
			}
			return str;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003F1B File Offset: 0x0000211B
		private static bool IsLocalCubeFile(string path)
		{
			return -1 == path.IndexOfAny(Path.GetInvalidPathChars()) && string.Compare(".cub", Path.GetExtension(path), StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003F41 File Offset: 0x00002141
		private static bool IsEmbeddedPath(string path)
		{
			return string.Compare(path, "$Embedded$", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003F54 File Offset: 0x00002154
		private static string GetApplicationIdFromIdentityProvider(string identityProvider)
		{
			if (string.Compare(identityProvider, "AADPPE", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "AADPPE";
			}
			string[] array = ((identityProvider != null) ? identityProvider.Split(new char[] { ',' }) : null);
			if (array != null && array.Length >= 2)
			{
				return array[1].Trim();
			}
			return null;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003FA0 File Offset: 0x000021A0
		private static string GetRestrictedConnectionString(string cs)
		{
			StringBuilder stringBuilder = new StringBuilder(cs.Length);
			foreach (KeyValuePair<string, string> keyValuePair in ConnectivityHelper.ParseKeyValueSet(cs))
			{
				ConnectionInfo.AppendRestrictedConnectionStringProperty(stringBuilder, keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000400C File Offset: 0x0000220C
		private static void AppendRestrictedConnectionStringProperty(StringBuilder cs, string property, string value)
		{
			if (!ConnectionInfo.IsPassword(property))
			{
				if (ConnectionInfo.IsExtendedProperties(property))
				{
					if (cs.Length > 0)
					{
						cs.Append(';');
					}
					cs.AppendFormat(CultureInfo.InvariantCulture, "{0}='{1}'", property, ConnectionInfo.GetRestrictedConnectionString(value).Replace("'", "''"));
					return;
				}
				if (!string.IsNullOrEmpty(value))
				{
					if (cs.Length > 0)
					{
						cs.Append(';');
					}
					cs.AppendFormat(CultureInfo.InvariantCulture, "{0}='{1}'", property, value.Replace("'", "''"));
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000040A0 File Offset: 0x000022A0
		private Guid ClientActivityIDInThreadContext
		{
			get
			{
				try
				{
					object obj = CallContext.LogicalGetData("AnalysisServices.ClientActivityId");
					if (obj != null)
					{
						return (Guid)obj;
					}
				}
				catch
				{
				}
				return Guid.Empty;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000040E0 File Offset: 0x000022E0
		private Guid CurrentActivityIDInThreadContext
		{
			get
			{
				try
				{
					object obj = CallContext.LogicalGetData("AnalysisServices.CurrentActivityId");
					if (obj != null)
					{
						return (Guid)obj;
					}
				}
				catch
				{
				}
				return Guid.Empty;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004120 File Offset: 0x00002320
		private Guid ActivityIDInThreadContext
		{
			get
			{
				if (Trace.CorrelationManager.ActivityId != Guid.Empty)
				{
					return Trace.CorrelationManager.ActivityId;
				}
				return this.autoGeneratedActivityID;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000414C File Offset: 0x0000234C
		private static ListDictionary CloneListDictionary(ListDictionary original)
		{
			ListDictionary listDictionary = null;
			if (original != null)
			{
				listDictionary = new ListDictionary();
				foreach (object obj in original.Keys)
				{
					listDictionary[obj] = original[obj];
				}
			}
			return listDictionary;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000041B4 File Offset: 0x000023B4
		private void ResetFields()
		{
			this.options.Reset();
			this.asInstanceType = AsInstanceType.Other;
			this.dataSource = null;
			this.linkReferenceRawDataSource = null;
			this.location = null;
			this.server = null;
			this.instanceName = null;
			this.port = null;
			this.userID = null;
			this.password = null;
			this.timeout = 0;
			this.connectTimeout = 60;
			this.autoSyncPeriod = 10000U;
			this.catalog = null;
			this.protectionLevel = ProtectionLevel.Privacy;
			this.connectTo = ConnectTo.Default;
			this.safetyOptions = SafetyOptions.Default;
			this.protocolFormat = ProtocolFormat.Default;
			this.transportCompression = TransportCompression.Default;
			this.compressionLevel = 0;
			this.integratedSecurity = IntegratedSecurity.Unspecified;
			this.encryptionPassword = null;
			this.impersonationLevel = ImpersonationLevel.Impersonate;
			this.sspi = "Negotiate";
			this.HttpHandling = HttpChannelHandling.Default;
			this.characterEncoding = Encoding.UTF8;
			this.packetSize = 4096;
			this.innerConnectionString = null;
			this.IsLightweightConnection = false;
			this.sessionID = null;
			this.restrictedConnectionString = null;
			this.dataSourceVersion = null;
			this.redirectorAddress = null;
			this.sandboxPath = null;
			this.clientCertificateThumbprint = null;
			this.userIdentity = UserIdentityType.WindowsIdentity;
			this.certificate = null;
			this.authenticationScheme = null;
			this.extAuthInfo = null;
			this.identityProvider = null;
			this.bypassAuthorization = null;
			this.restrictCatalog = null;
			this.accessMode = null;
			this.restrictUser = null;
			this.restrictRoles = null;
			this.intendedUsage = IntendedUsage.Default;
			this.asAzureRedirection = AsAzureRedirection.Default;
			this.pbipAccessMode = ConnectionAccessMode.Default;
			this.RoutingToken = null;
			this.auxiliaryPermissionOwner = null;
			this.serviceToServiceToken = null;
			this.paasInfrastructureServerName = null;
			this.contextualIdentity = null;
			this.connectionActivityId = Guid.Empty;
			this.extendedProperties.Clear();
			this.transientModelMode = null;
			this.skipConnectionStringReconversion = false;
			this.sourceCapacityObjectId = Guid.Empty;
			this.servicePrincipalProfileId = null;
			this.contextualIdentityKey = null;
			this.contextualIdentityType = null;
			this.pbipVirtualServiceName = null;
			this.originalConnStringProps.Clear();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000043A0 File Offset: 0x000025A0
		private void SetConnectionString(string cs)
		{
			if (cs == null)
			{
				throw new ArgumentNullException("cs");
			}
			cs = cs.Trim();
			if (cs.Length == 0)
			{
				throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
			}
			ConnectionInfo connectionInfo = new ConnectionInfo(this);
			try
			{
				this.ResetFields();
				this.connectionString = cs;
				if (!this.ParseShortcutForm())
				{
					using (IEnumerator<KeyValuePair<string, string>> enumerator = ConnectivityHelper.ParseKeyValueSet(cs).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, string> keyValuePair = enumerator.Current;
							this.HandleKeyValueDuringConnectionStringParsing(keyValuePair.Key, keyValuePair.Value);
						}
						goto IL_00CE;
					}
					IL_0085:
					string text = this.innerConnectionString;
					this.innerConnectionString = null;
					foreach (KeyValuePair<string, string> keyValuePair2 in ConnectivityHelper.ParseKeyValueSet(text))
					{
						this.HandleKeyValueDuringConnectionStringParsing(keyValuePair2.Key, keyValuePair2.Value);
					}
					IL_00CE:
					if (this.innerConnectionString != null)
					{
						goto IL_0085;
					}
					if (this.dataSource == null)
					{
						this.dataSource = this.location;
					}
					if (this.dataSource == null)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_DataSourceNotSpecified);
					}
					this.ExtractDataSourceParts(this.dataSource, this.location);
				}
				if (string.Compare(this.server, "(local)", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.server, "local", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.server = ".";
				}
				if (ConnectivityHelper.HasUriProtocolScheme(this.server, "link"))
				{
					this.IsLinkReference = true;
					this.server = ASAzureUtility.ResolveServerBasedOnLinkReference(this.server);
					this.linkReferenceRawDataSource = this.dataSource;
					this.dataSource = this.server;
				}
				if (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.UseEncryptionForData))
				{
					this.protectionLevel = ProtectionLevel.Privacy;
				}
				else if (this.options.IsDisabled(ConnectionInfo.ConnectionOptions.ProtectionLevelWasSet))
				{
					if (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.UseEncryptionForDataWasSet))
					{
						this.protectionLevel = ProtectionLevel.Connection;
					}
					else if (this.connectionType == ConnectionType.Http && !ConnectivityHelper.IsHttpsConnection(this.server) && !ConnectivityHelper.IsPaaSInfrastructureConnection(this.server))
					{
						this.protectionLevel = ProtectionLevel.Connection;
					}
				}
				if (this.connectionType == ConnectionType.Http)
				{
					Uri uri = new Uri(this.dataSource);
					if (Uri.UriSchemeHttp.Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase) || Uri.UriSchemeHttps.Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase))
					{
						string text2 = uri.Query.Trim();
						if (text2.Length > 1 && text2[0] == '?')
						{
							text2 = text2.Remove(0, 1);
							string[] array = text2.Split(new char[] { '&', ';' });
							if (array != null)
							{
								string[] array2 = array;
								for (int i = 0; i < array2.Length; i++)
								{
									string[] array3 = array2[i].Trim().Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
									if (array3.Length == 2)
									{
										string text3 = array3[0].Trim();
										string text4 = array3[1].Trim();
										if ("integratedsecurity".Equals(text3, StringComparison.OrdinalIgnoreCase) && "federated".Equals(text4, StringComparison.OrdinalIgnoreCase))
										{
											this.integratedSecurity = IntegratedSecurity.Federated;
										}
										else if ("identityprovider".Equals(text3, StringComparison.OrdinalIgnoreCase))
										{
											if (!"msoid".Equals(text4, StringComparison.OrdinalIgnoreCase))
											{
												throw new ArgumentException(XmlaSR.ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated);
											}
											this.identityProvider = text4;
										}
										else if ("userid".Equals(text3, StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(this.userID))
										{
											this.userID = text4;
										}
										else if ("password".Equals(text3, StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(this.password))
										{
											this.password = text4;
										}
										else if ("location".Equals(text3, StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(this.location))
										{
											this.location = text4;
										}
									}
								}
							}
						}
					}
				}
				if (this.connectionType != ConnectionType.Http || !new Uri(this.server).AbsolutePath.EndsWith(".bism", StringComparison.OrdinalIgnoreCase))
				{
					this.ValidateAndCompleteConnectionConfiguration(null);
				}
				if (this.userIdentity == UserIdentityType.SharePointPrincipal)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("User Identity", "SharePoint Principal"));
				}
				if (!this.extendedProperties.Contains("LocaleIdentifier"))
				{
					this.extendedProperties["LocaleIdentifier"] = FormattersHelpers.ConvertToXml(CultureInfo.CurrentCulture.LCID);
					this.SetOriginalConnectionStringValue("LocaleIdentifier", FormattersHelpers.ConvertToXml(CultureInfo.CurrentCulture.LCID));
				}
				if (!this.extendedProperties.Contains("ClientProcessID") && this.extendedProperties.Contains("SspropInitAppName"))
				{
					int id = Process.GetCurrentProcess().Id;
					this.extendedProperties["ClientProcessID"] = FormattersHelpers.ConvertToXml(id);
					this.SetOriginalConnectionStringValue("ClientProcessID", FormattersHelpers.ConvertToXml(id));
				}
			}
			catch
			{
				ConnectionInfo.CopyConnectionInfo(connectionInfo, this);
				throw;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000048B8 File Offset: 0x00002AB8
		private void ValidateAndCompleteConnectionConfiguration(IConnectivityOwner owner)
		{
			if (this.connectionType == ConnectionType.Native)
			{
				if (this.integratedSecurity != IntegratedSecurity.Unspecified && this.integratedSecurity != IntegratedSecurity.Sspi)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_InvalidIntegratedSecurityForNative(this.integratedSecurity.ToString()));
				}
			}
			else if (this.connectionType == ConnectionType.Http)
			{
				if (this.integratedSecurity != IntegratedSecurity.Unspecified && this.integratedSecurity != IntegratedSecurity.Federated && this.integratedSecurity != IntegratedSecurity.ClaimsToken && (this.integratedSecurity != IntegratedSecurity.Sspi || this.userID != null || this.password != null))
				{
					throw new ArgumentException(XmlaSR.ConnectionString_InvalidIntegratedSecurityForHttpOrHttps(this.integratedSecurity.ToString()));
				}
				if (this.integratedSecurity == IntegratedSecurity.Federated && string.IsNullOrEmpty(this.identityProvider))
				{
					throw new ArgumentException(XmlaSR.ConnectionString_MissingIdentityProviderForIntegratedSecurityFederated);
				}
				if ((this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ApplyAuxiliaryPermission) || this.options.IsEnabled(ConnectionInfo.ConnectionOptions.BypassBuildPermission)) && string.IsNullOrEmpty(this.serviceToServiceToken))
				{
					throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
				}
				this.HandlePaaSInfrastructureConnection(owner);
				if (ConnectivityHelper.IsHttpsConnection(this.server))
				{
					ProtectionLevel protectionLevel = this.protectionLevel;
					if (protectionLevel <= ProtectionLevel.Integrity)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_InvalidProtectionLevelForHttps(this.protectionLevel.ToString()));
					}
					if (this.impersonationLevel == ImpersonationLevel.Anonymous)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_InvalidImpersonationLevelForHttps(ImpersonationLevel.Anonymous.ToString()));
					}
				}
				else
				{
					ProtectionLevel protectionLevel = this.protectionLevel;
					if (protectionLevel == ProtectionLevel.None || protectionLevel - ProtectionLevel.Integrity <= 1)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_InvalidProtectionLevelForHttp(this.protectionLevel.ToString()));
					}
					if (this.impersonationLevel == ImpersonationLevel.Anonymous || this.impersonationLevel == ImpersonationLevel.Identify)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_InvalidImpersonationLevelForHttp(this.impersonationLevel.ToString()));
					}
				}
			}
			if (this.redirectorAddress == null && this.dataSourceVersion != null)
			{
				throw new ArgumentException(XmlaSR.ConnectionString_PropertyNotApplicableWithTheDataSourceType("Data Source Version"));
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004A90 File Offset: 0x00002C90
		private void HandlePaaSInfrastructureConnection(IConnectivityOwner owner)
		{
			this.asInstanceType = ConnectivityHelper.GetAsInstanceType(this.server);
			switch (this.asInstanceType)
			{
			case AsInstanceType.PbiPremiumInternal:
				this.ValidateConnectionPropsInPBIInternalConnection();
				this.pbipWorkloadResourceMoniker = this.catalog;
				break;
			case AsInstanceType.PbiPremiumXmlaEp:
				if (this.IsPbiPremiumXmlaEpWithDataverseEmbedToken)
				{
					throw new ArgumentException(XmlaSR.XmlaClient_PbiPublicXmla_UnsupportedIdentityProvider("Dataverse"));
				}
				ASAzureUtility.ExtractPbiPublicXmlaTenantAndWorkspace(this.server, out this.pbiPremiumTenant, out this.pbiPremiumWorkspaceName);
				if (this.IsPbiPremiumXmlaEpWithPowerBIEmbedToken && string.IsNullOrEmpty(this.Catalog))
				{
					throw new ArgumentException(XmlaSR.XmlaClient_PbiPublicXmlaWithEmbedToken_Missing_InitialCatalog);
				}
				break;
			case AsInstanceType.PbiDataset:
				if (this.AuthHandle == null)
				{
					this.BuildExternalAuthenticationHandle(owner);
				}
				this.ValidateAndSetPbiDatasourceAndLocation(owner);
				break;
			default:
				if (this.IsWorkloadDirectConnection)
				{
					this.ValidateConnectionPropsInPBIInternalConnection();
					this.pbipWorkloadResourceMoniker = this.catalog;
				}
				break;
			}
			if (!this.IsPbiPremiumXmlaEp && !string.IsNullOrEmpty(this.ServicePrincipalProfileId))
			{
				throw new ArgumentException(XmlaSR.ConnectionString_SPN_Profile_Not_Supported);
			}
			if (this.IsPaaSInfrastructure)
			{
				switch (this.integratedSecurity)
				{
				case IntegratedSecurity.Sspi:
					if (!this.IsAsAzure && !this.IsPbiPremiumXmlaEp)
					{
						throw new ArgumentException(XmlaSR.Authentication_PbiDedicated_OnlyClaimsTokenSupported);
					}
					if (this.isUseAdalCacheSpecifiedOnConnectionString)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_AsAzure_SspiAndUseAdalCacheTogetherNotSupported);
					}
					this.integratedSecurity = IntegratedSecurity.ClaimsToken;
					this.options.Enable(ConnectionInfo.ConnectionOptions.UseADTranslation);
					goto IL_0188;
				case IntegratedSecurity.ClaimsToken:
					goto IL_0188;
				case IntegratedSecurity.Unspecified:
					this.integratedSecurity = IntegratedSecurity.ClaimsToken;
					goto IL_0188;
				}
				if (this.IsPbiPremiumInternal || this.IsWorkloadDirectConnection)
				{
					throw new ArgumentException(XmlaSR.Authentication_PbiDedicated_OnlyClaimsTokenSupported);
				}
				throw new ArgumentException(XmlaSR.Authentication_AsAzure_OnlySspiOrClaimsTokenSupported);
				IL_0188:
				if (this.IsAsAzure || (this.IsPbiPremiumInternal && !this.IsWorkloadDirectConnection))
				{
					string text;
					if (!ASAzureUtility.DataSourceUriWithOnlyServerName(this.server, out text))
					{
						throw new NotSupportedException(XmlaSR.ConnectionString_AsAzure_DataSourcePathMoreThanOneSegment);
					}
					this.server = ASAzureUtility.ConstructAsAzureSecureServerConnUri(this.server);
					this.paasInfrastructureServerName = text;
				}
				this.options.Disable(ConnectionInfo.ConnectionOptions.AllowAutoRedirect);
				if (!ClientHostingManager.IsProcessWithUserInterface && this.password == null && this.options.IsDisabled(ConnectionInfo.ConnectionOptions.UseADTranslation))
				{
					this.options.Enable(ConnectionInfo.ConnectionOptions.AccessTokenRequired);
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004CB4 File Offset: 0x00002EB4
		private bool ShouldRedirectAsAzureToPowerBIWorkspace(Uri dataSource, out RedirectionWorkspaceInfo redirectionWorkspaceInfo, out ConnectionAccessMode accessMode)
		{
			redirectionWorkspaceInfo = default(RedirectionWorkspaceInfo);
			accessMode = ConnectionAccessMode.Default;
			if (!this.IsAsAzure || this.AsAzureRedirection == AsAzureRedirection.Disabled)
			{
				return false;
			}
			Uri uri;
			if (dataSource.AbsolutePath.EndsWith(":rw", StringComparison.InvariantCultureIgnoreCase))
			{
				uri = dataSource.RemovePathSuffix(":rw");
				accessMode = ConnectionAccessMode.ReadWrite;
			}
			else if (dataSource.AbsolutePath.EndsWith(":r", StringComparison.InvariantCultureIgnoreCase))
			{
				uri = dataSource.RemovePathSuffix(":r");
				accessMode = ConnectionAccessMode.ReadOnly;
			}
			else
			{
				uri = dataSource;
			}
			RedirectionInformation redirectionInformation;
			if (!RedirectionInformation.TryGetRedirectionInfo(uri, out redirectionInformation))
			{
				return false;
			}
			string text;
			if (!ASAzureUtility.IsAsAzureRedirectedToPowerBIWorkspace(redirectionInformation.PbiEndpoint, uri.ToString(), this.GetEffectiveActivityId(), out text))
			{
				return false;
			}
			redirectionWorkspaceInfo = new RedirectionWorkspaceInfo(text, uri.ToString(), redirectionInformation.PbiEndpoint, redirectionInformation.PbiResourceId);
			return true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004D70 File Offset: 0x00002F70
		private void ValidateConnectionPropsInPBIInternalConnection()
		{
			if (string.IsNullOrEmpty(this.catalog))
			{
				throw new ArgumentException(XmlaSR.ConnectionString_PbiDedicated_MissingInitialCatalog);
			}
			if (string.IsNullOrEmpty(this.restrictCatalog) || !this.restrictCatalog.Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(XmlaSR.ConnectionString_PbiDedicated_MissingRestrictCatalog);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004DC0 File Offset: 0x00002FC0
		private void ValidateAndSetPbiDatasourceAndLocation(IConnectivityOwner owner)
		{
			Uri uri = this.GetEffectiveDataSource();
			if ("pbiazure".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase))
			{
				uri = new UriBuilder(uri)
				{
					Scheme = Uri.UriSchemeHttps
				}.Uri;
			}
			if (string.IsNullOrEmpty(this.location))
			{
				AuthenticationInformation.ValidateResourceForAuthentication(uri);
				Uri v3DiscoveryUri = ConnectivityHelper.GetV3DiscoveryUri();
				if (!AsPaasHelper.ClusterResolutionEndpoint.IsTrustedClusterResolutionEndpoint(uri) && !ASAzureUtility.AuthenticatePowerBIBackendEndpoint(v3DiscoveryUri, uri, TimeoutUtils.TimeLeft.FromSeconds(this.ConnectTimeout), this.asInstanceType))
				{
					throw new ArgumentException(XmlaSR.ConnectionString_Untrusted_Endpoint);
				}
				Uri uri2 = new UriBuilder(uri)
				{
					Path = "spglobalservice/GetOrInsertClusterUrisByTenantLocation"
				}.Uri;
				AuthenticationHandle authenticationHandle = this.authHandle;
				this.location = new UriBuilder(ASAzureUtility.ResolvePowerBICluster(uri2, (authenticationHandle != null) ? authenticationHandle.GetAccessToken() : null, TimeoutUtils.TimeLeft.FromSeconds(this.ConnectTimeout), this.asInstanceType))
				{
					Path = "xmla",
					Query = string.Format("vs=sobe_wowvirtualserver&db={0}", this.catalog.Replace("sobe_wowvirtualserver-", string.Empty))
				}.Uri.AbsoluteUri;
			}
			else
			{
				Uri uri3 = new Uri(this.location);
				IList<string> list = new List<string>(new Uri(AuthenticationInformation.FindMatchingAuthenticationInformation(AuthenticationEndpoint.AadV1, null, uri, false).ResourceId).Host.Split(new char[] { '.' }));
				IList<string> list2 = new List<string>(uri3.Host.Split(new char[] { '.' }));
				if (list2.Count < list.Count)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_Untrusted_Endpoint);
				}
				for (int i = 1; i <= list.Count; i++)
				{
					if (!list[list.Count - i].Equals(list2[list2.Count - i], StringComparison.OrdinalIgnoreCase) && !ConnectionInfo.ppeEndpoints.Contains(uri3.Host))
					{
						throw new ArgumentException(XmlaSR.ConnectionString_Untrusted_Endpoint);
					}
				}
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uri3.Query);
				if (nameValueCollection["db"] == null || nameValueCollection["vs"] == null)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_PbiDataset_Missing_Metadata((nameValueCollection["db"] == null) ? "db" : "vs"));
				}
			}
			string applicationIdFromIdentityProvider = ConnectionInfo.GetApplicationIdFromIdentityProvider(this.IdentityProvider);
			this.skipConnectionStringReconversion = false;
			if (AsPaasHelper.AixlToPublicXmlaConversion.IsAixlToPublicXmlaConversionAllowedForApp(applicationIdFromIdentityProvider, this.options.IsEnabled(ConnectionInfo.ConnectionOptions.BypassBuildPermission), !string.IsNullOrEmpty(this.ServicePrincipalProfileId)))
			{
				string text = HttpUtility.ParseQueryString(this.location)["db"];
				string host = new Uri(this.location).Host;
				ExternalAuthenticationHandle externalAuthenticationHandle = (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.BypassBuildPermission) ? new ExternalAuthenticationHandle(this.ServiceToServiceToken, AuthenticationHandle.ConvertIdentityProviderToTokenScheme(null)) : null);
				this.skipConnectionStringReconversion = this.TryConvertAnalyzeInExcelToPublicXmla(owner, host, text, externalAuthenticationHandle);
			}
			if (!this.skipConnectionStringReconversion)
			{
				this.dataSource = this.location;
				this.server = this.location;
			}
			this.options.Disable(ConnectionInfo.ConnectionOptions.ResolvedLinkFile);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000050BC File Offset: 0x000032BC
		private bool TryConvertAnalyzeInExcelToPublicXmla(IConnectivityOwner owner, string pbiApiBaseUrl, string datasetObjectId, ExternalAuthenticationHandle s2sHandle)
		{
			string text;
			string text2;
			string text3;
			ArtifactCapacityState artifactCapacityState;
			if (!PbiPremiumAuthenticationHandle.TryGetDatasetDetailsForAnalyzeInExcel(pbiApiBaseUrl, datasetObjectId, this.authHandle, s2sHandle, Guid.NewGuid().ToString(), out text, out text2, out text3, out artifactCapacityState) || artifactCapacityState != ArtifactCapacityState.AssignedToCapacity)
			{
				return false;
			}
			string text4 = string.Format(CultureInfo.InvariantCulture, "powerbi://{0}/v1.0/myorg/{1}", text3, text2);
			this.dataSource = text4;
			this.server = text4;
			this.SetCatalog(text);
			this.HandlePaaSInfrastructureConnection(owner);
			this.PbipAccessMode = ConnectionAccessMode.RestrictedReadOnly;
			return true;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005135 File Offset: 0x00003335
		private Guid GetEffectiveActivityId()
		{
			if (this.ConnectionActivityId != Guid.Empty)
			{
				return this.ConnectionActivityId;
			}
			return this.ClientActivityID;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005156 File Offset: 0x00003356
		private Uri GetEffectiveDataSource()
		{
			if (!this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ResolvedLinkFile))
			{
				return new Uri(this.dataSource);
			}
			return new Uri(this.server);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005184 File Offset: 0x00003384
		private void GetRemoteLinkFileMetadata(string dataSource, out string server, out string database, out bool isDelegationAllowed)
		{
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(dataSource);
				ICredentials credentials2;
				if (this.userID != null)
				{
					ICredentials credentials = new NetworkCredential(this.userID, this.password);
					credentials2 = credentials;
				}
				else
				{
					credentials2 = CredentialCache.DefaultCredentials;
				}
				httpWebRequest.Credentials = credentials2;
				httpWebRequest.UserAgent = "ADOMD.NET";
				using (Stream responseStream = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream())
				{
					string text;
					ASLinkFile.LoadFromStream(responseStream, out server, out database, out text, out isDelegationAllowed);
				}
			}
			catch (WebException ex)
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionString_LinkFileDownloadError(dataSource), ex);
			}
			catch (XmlException ex2)
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionString_LinkFileParseError(4096), ex2);
			}
			catch (XmlSchemaException ex3)
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionString_LinkFileParseError(4096), ex3);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005264 File Offset: 0x00003464
		private void ResolveHTTPConnectionPropertiesForPaaSInfrastructure(IConnectivityOwner owner, ref Uri dataSourceUri, bool acquireAADToken, bool returnCloudConnectionAuthenticationProperties, bool isRedirectedWorkspace, string tenantId, out string paasCoreServerName, out CloudConnectionAuthenticationProperties cloudConnectionAuthenticationProperties)
		{
			paasCoreServerName = null;
			cloudConnectionAuthenticationProperties = null;
			Guid effectiveActivityId = this.GetEffectiveActivityId();
			Microsoft.AnalysisServices.AdomdClient.Authentication.AuthenticationManager.EnableStrongSecurityProtocols();
			TimeoutUtils.TimeLeft timeLeft = TimeoutUtils.TimeLeft.FromSeconds(this.ConnectTimeout);
			string text = (this.IsPbiPremiumInternal ? this.Catalog : null);
			if (this.IsPbiPremiumInternal || this.IsAsAzure)
			{
				if (!isRedirectedWorkspace)
				{
					AsPaasEndpointInfo asPaasEndpointInfo = ASAzureUtility.ResolvePaaSConnectionEndpointDetail(this.AsInstanceType, dataSourceUri, this.PaaSInfrastructureServerName, text, false, ref timeLeft, delegate(bool isOnDispose)
					{
						throw new AdomdConnectionException(XmlaSR.XmlaClient_ConnectTimedOut, null);
					}, effectiveActivityId);
					this.ConnectTimeout = timeLeft.TimeSec;
					if (acquireAADToken)
					{
						this.authHandle = this.AcquireToken(owner, dataSourceUri.AbsoluteUri, asPaasEndpointInfo.TenantId, false);
					}
					dataSourceUri = asPaasEndpointInfo.Cluster;
					paasCoreServerName = asPaasEndpointInfo.Server;
					tenantId = asPaasEndpointInfo.TenantId;
				}
			}
			else if (this.IsPbiPremiumXmlaEp)
			{
				tenantId = this.PbiPremiumTenant;
				if (acquireAADToken)
				{
					if (this.IsPbiPremiumXmlaEpWithPowerBIEmbedToken && (this.UserID != null || this.Password == null))
					{
						throw new AdomdConnectionException(XmlaSR.XmlaClient_PbiPublicXmlaWithEmbedToken_UserId_Prop_NotSupported, null);
					}
					AuthenticationHandle authenticationHandle = this.AcquireToken(owner, dataSourceUri.AbsoluteUri, tenantId, false);
					string text2 = ((this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ApplyAuxiliaryPermission) || this.options.IsEnabled(ConnectionInfo.ConnectionOptions.BypassBuildPermission)) ? this.ServiceToServiceToken : null);
					string text3;
					string text4;
					string text5;
					ResolvePbiWorkspaceErrorReason resolvePbiWorkspaceErrorReason;
					string text6;
					WorkspaceCapacitySkuType201606 workspaceCapacitySkuType;
					string text7;
					if (!PbiPremiumAuthenticationHandle.TryResolvePbiWorkspace(dataSourceUri.Authority, this.PbiPremiumWorkspaceName, authenticationHandle, effectiveActivityId.Equals(Guid.Empty) ? string.Empty : effectiveActivityId.ToString(), text2, this.ServicePrincipalProfileId, out text3, out text4, out text5, out resolvePbiWorkspaceErrorReason, out text6, out workspaceCapacitySkuType, out text7))
					{
						string text8 = ClientHostingManager.MarkAsRestrictedInformation(this.PbiPremiumWorkspaceName ?? "null", InfoRestrictionType.CCON);
						if (resolvePbiWorkspaceErrorReason == ResolvePbiWorkspaceErrorReason.WorkspaceNotFound)
						{
							throw new AdomdConnectionException(XmlaSR.XmlaClient_PbiPremium_WorkspaceNotFound(text7, text8), null);
						}
						if (resolvePbiWorkspaceErrorReason != ResolvePbiWorkspaceErrorReason.WorkspaceNameDuplicated)
						{
							throw new AdomdConnectionException(XmlaSR.InternalError + text7, null);
						}
						throw new AdomdConnectionException(XmlaSR.XmlaClient_PbiPremium_WorkspaceNameDuplicated(text7, text8), null);
					}
					else
					{
						bool flag = !string.IsNullOrEmpty(this.ServicePrincipalProfileId);
						bool flag2 = workspaceCapacitySkuType == WorkspaceCapacitySkuType201606.Shared && string.IsNullOrEmpty(text5) && !this.skipConnectionStringReconversion && !flag;
						string text9;
						ArtifactCapacityState artifactCapacityState;
						ResolveDatabaseNameErrorReason resolveDatabaseNameErrorReason;
						if (!this.skipConnectionStringReconversion && !flag2 && !string.IsNullOrEmpty(this.Catalog) && !this.IsPbiPremiumXmlaEpWithPowerBIEmbedToken && !flag && PbiPremiumAuthenticationHandle.TryGetDatabaseName(dataSourceUri.Authority, text3, text6, this.Catalog, authenticationHandle, effectiveActivityId.Equals(Guid.Empty) ? string.Empty : effectiveActivityId.ToString(), out text9, out artifactCapacityState, out resolveDatabaseNameErrorReason, out text7))
						{
							flag2 = artifactCapacityState == ArtifactCapacityState.NotAssignedToCapacity;
						}
						if (flag2)
						{
							if (string.IsNullOrEmpty(this.Catalog))
							{
								throw new AdomdConnectionException(XmlaSR.XmlaClient_PbiPublicXmla_DatasetNotSpecified, null);
							}
							if (this.IsPbiPremiumXmlaEpWithPowerBIEmbedToken)
							{
								throw new AdomdConnectionException(XmlaSR.XmlaClient_PbiPublicXmlaWithEmbedToken_ToPBIShared_NotSupported, null);
							}
							string text10;
							ArtifactCapacityState artifactCapacityState2;
							ResolveDatabaseNameErrorReason resolveDatabaseNameErrorReason2;
							if (!PbiPremiumAuthenticationHandle.TryGetDatabaseName(dataSourceUri.Authority, text3, text6, this.Catalog, authenticationHandle, effectiveActivityId.Equals(Guid.Empty) ? string.Empty : effectiveActivityId.ToString(), out text10, out artifactCapacityState2, out resolveDatabaseNameErrorReason2, out text7))
							{
								if (resolveDatabaseNameErrorReason2 == ResolveDatabaseNameErrorReason.DatabaseNotFound)
								{
									throw new AdomdConnectionException(XmlaSR.XmlaClient_PbiPublicXmla_DatasetNotFound(this.Catalog, text7), null);
								}
								if (resolveDatabaseNameErrorReason2 != ResolveDatabaseNameErrorReason.DatabaseNameDuplicated)
								{
									throw new AdomdConnectionException(XmlaSR.InternalError + text7, null);
								}
								throw new AdomdConnectionException(XmlaSR.XmlaClient_PbiPublicXmla_DatasetNameDuplicated(this.Catalog, text7), null);
							}
							else
							{
								this.asInstanceType = AsInstanceType.PbiDataset;
								this.authHandle = authenticationHandle;
								this.SetCatalog(string.Format("{0}{1}", "sobe_wowvirtualserver-", text10));
								this.dataSource = new UriBuilder(dataSourceUri)
								{
									Scheme = Uri.UriSchemeHttps
								}.Uri.GetLeftPart(UriPartial.Authority);
								this.location = null;
								this.options.Disable(ConnectionInfo.ConnectionOptions.ResolvedLinkFile);
								this.ValidateAndSetPbiDatasourceAndLocation(owner);
								dataSourceUri = new Uri(this.dataSource);
							}
						}
						else
						{
							AuxiliaryPermissionInfo auxiliaryPermissionInfo = new AuxiliaryPermissionInfo
							{
								ApplyAuxiliaryPermission = this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ApplyAuxiliaryPermission),
								AuxiliaryPermissionOwner = (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ApplyAuxiliaryPermission) ? this.auxiliaryPermissionOwner : null),
								BypassBuildPermission = this.options.IsEnabled(ConnectionInfo.ConnectionOptions.BypassBuildPermission),
								IntendedUsage = (int)this.IntendedUsage,
								SourceCapacityObjectId = ((this.sourceCapacityObjectId != Guid.Empty) ? this.sourceCapacityObjectId.ToString() : null),
								ServicePrincipalProfileId = this.ServicePrincipalProfileId
							};
							if (auxiliaryPermissionInfo.RequireServiceToServiceToken)
							{
								auxiliaryPermissionInfo.ServiceToServiceToken = this.ServiceToServiceToken;
							}
							auxiliaryPermissionInfo.Validate();
							this.authHandle = (this.UseAadTokenInPublicXmlaEP ? authenticationHandle : new PbiPremiumAuthenticationHandle(authenticationHandle, dataSourceUri.Authority, text3, text5, this.Catalog, auxiliaryPermissionInfo));
							this.PaaSInfrastructureServerName = text5;
							this.PbiPremiumWorkspaceObjectId = text3;
							AsPaasEndpointInfo asPaasEndpointInfo2 = ASAzureUtility.ResolvePaaSConnectionEndpointDetail(this.AsInstanceType, new Uri(ASAzureUtility.ConstructPbiPremiumServerConnUri(text4)), this.PaaSInfrastructureServerName, null, true, ref timeLeft, delegate(bool isOnDispose)
							{
								throw new AdomdConnectionException(XmlaSR.XmlaClient_ConnectTimedOut, null);
							}, effectiveActivityId);
							this.ConnectTimeout = timeLeft.TimeSec;
							dataSourceUri = asPaasEndpointInfo2.Cluster;
							paasCoreServerName = asPaasEndpointInfo2.Server;
						}
					}
				}
			}
			if (returnCloudConnectionAuthenticationProperties)
			{
				cloudConnectionAuthenticationProperties = Microsoft.AnalysisServices.AdomdClient.Authentication.AuthenticationManager.GetCloudConnectionAuthenticationProperties(this.IdentityProvider, dataSourceUri.AbsoluteUri, tenantId, this.UserID);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000580D File Offset: 0x00003A0D
		private bool ParseShortcutForm()
		{
			return false;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005810 File Offset: 0x00003A10
		private void HandleKeyValueDuringConnectionStringParsing(string key, string value)
		{
			if (this.CheckAndSetDataSource(key, value))
			{
				this.SetOriginalConnectionStringValue("Data Source", value);
				return;
			}
			if (this.CheckAndSetLocation(key, value))
			{
				this.SetOriginalConnectionStringValue("Location", value);
				return;
			}
			if (this.CheckAndSetUserID(key, value))
			{
				this.SetOriginalConnectionStringValue("User ID", value);
				return;
			}
			if (this.CheckAndSetPassword(key, value))
			{
				this.SetOriginalConnectionStringValue("Password", value);
				return;
			}
			if (this.CheckAndSetProtectionLevel(key, value))
			{
				this.SetOriginalConnectionStringValue("Protection Level", value);
				return;
			}
			if (this.CheckAndSetIntegratedSecurity(key, value))
			{
				this.SetOriginalConnectionStringValue("Integrated Security", value);
				return;
			}
			if (this.CheckAndSetConnectTimeout(key, value))
			{
				this.SetOriginalConnectionStringValue("Connect Timeout", value);
				return;
			}
			if (this.CheckAndSetAutoSyncPeriod(key, value))
			{
				this.SetOriginalConnectionStringValue("Auto Synch Period", value);
				return;
			}
			if (this.CheckAndSetConnectTo(key, value))
			{
				this.SetOriginalConnectionStringValue("ConnectTo", value);
				return;
			}
			if (this.CheckAndSetProtocolFormat(key, value))
			{
				this.SetOriginalConnectionStringValue("Protocol Format", value);
				return;
			}
			if (this.CheckAndSetTransportCompression(key, value))
			{
				this.SetOriginalConnectionStringValue("Transport Compression", value);
				return;
			}
			if (this.CheckAndSetCompressionLevel(key, value))
			{
				this.SetOriginalConnectionStringValue("Compression Level", value);
				return;
			}
			if (this.CheckAndSetEncryptionPassword(key, value))
			{
				this.SetOriginalConnectionStringValue("Encryption Password", value);
				return;
			}
			if (this.CheckAndSetImpersonationLevel(key, value))
			{
				this.SetOriginalConnectionStringValue("Impersonation Level", value);
				return;
			}
			if (this.CheckAndSetRestrictedClient(key, value))
			{
				this.SetOriginalConnectionStringValue("Restricted Client", value);
				return;
			}
			if (this.CheckAndSetSspi(key, value))
			{
				this.SetOriginalConnectionStringValue("SSPI", value);
				return;
			}
			if (this.CheckAndSetHttpHandling(key, value))
			{
				this.SetOriginalConnectionStringValue("HttpChannelHandling", value);
				return;
			}
			if (this.CheckAndSetUseExistingFile(key, value))
			{
				this.SetOriginalConnectionStringValue("UseExistingFile", value);
				return;
			}
			if (this.CheckAndSetCharacterEncoding(key, value))
			{
				this.SetOriginalConnectionStringValue("Character Encoding", value);
				return;
			}
			if (this.CheckAndSetUseEncryptionForData(key, value))
			{
				this.SetOriginalConnectionStringValue("Use Encryption for Data", value);
				return;
			}
			if (this.CheckAndSetPacketSize(key, value))
			{
				this.SetOriginalConnectionStringValue("Packet Size", value);
				return;
			}
			if (this.CheckAndSetPersistSecurityInfo(key, value))
			{
				this.SetOriginalConnectionStringValue("Persist Security Info", value);
				return;
			}
			if (this.CheckAndSetSessionID(key, value))
			{
				this.SetOriginalConnectionStringValue("SessionID", value);
				return;
			}
			if (this.CheckAndSetDataSourceVersion(key, value))
			{
				this.SetOriginalConnectionStringValue("Data Source Version", value);
				return;
			}
			if (this.CheckAndSetClientCertificateThumbprint(key, value))
			{
				this.SetOriginalConnectionStringValue("Client Certificate Thumbprint", value);
				return;
			}
			if (this.CheckAndSetUserIdentity(key, value))
			{
				this.SetOriginalConnectionStringValue("User Identity", value);
				return;
			}
			if (this.CheckAndSetExtendedProperties(key, value))
			{
				this.SetOriginalConnectionStringValue("Extended Properties", value);
				return;
			}
			if (this.CheckAndSetCertificate(key, value))
			{
				this.SetOriginalConnectionStringValue("Certificate", value);
				return;
			}
			if (this.CheckAndSetTokenCacheMode(key, value))
			{
				this.SetOriginalConnectionStringValue("Token Cache Mode", value);
				return;
			}
			if (this.CheckAndSetUseAdalCache(key, value))
			{
				this.SetOriginalConnectionStringValue("UseADALCache", value);
				return;
			}
			if (this.CheckAndSetApplyAuxiliaryPermission(key, value))
			{
				this.SetOriginalConnectionStringValue("ApplyAuxiliaryPermission", value);
				return;
			}
			if (this.CheckAndSetAuxiliaryPermissionOwner(key, value))
			{
				this.SetOriginalConnectionStringValue("AuxiliaryPermissionOwner", value);
				return;
			}
			if (this.CheckAndSetServiceToServiceToken(key, value))
			{
				this.SetOriginalConnectionStringValue("ServiceToServiceToken", value);
				return;
			}
			if (this.CheckAndSetTransientModelMode(key, value))
			{
				this.SetOriginalConnectionStringValue("Transient Model", value);
				return;
			}
			if (this.CheckAndSetIsInternalPaaSInfrastructure(key, value))
			{
				this.SetOriginalConnectionStringValue("IsInternalPaaSInfrastructure", value);
				return;
			}
			if (this.CheckAndSetIsInternalTestInfrastructure(key, value))
			{
				this.SetOriginalConnectionStringValue("IsInternalTestInfrastructure", value);
				return;
			}
			if (this.CheckAndSetPbipCoreServiceRoutingHint(key, value))
			{
				this.SetOriginalConnectionStringValue("PbipCoreServiceRoutingHint", value);
				return;
			}
			if (this.CheckAndSetWorkloadDirectConnection(key, value))
			{
				this.SetOriginalConnectionStringValue("WorkloadDirectConnection", value);
				return;
			}
			if (this.CheckAndSetPBIPVirtualServiceName(key, value))
			{
				this.SetOriginalConnectionStringValue("PBIPVirtualServiceName", value);
				return;
			}
			if (this.CheckAndSetContextualIdentityKey(key, value))
			{
				this.SetOriginalConnectionStringValue("ContextualIdentityKey", value);
				return;
			}
			if (this.CheckAndSetContextualIdentityType(key, value))
			{
				this.SetOriginalConnectionStringValue("ContextualIdentityType", value);
				return;
			}
			if (this.CheckAndSetAuthenticationScheme(key, value))
			{
				this.SetOriginalConnectionStringValue("Authentication Scheme", value);
				return;
			}
			if (this.CheckAndSetExtAuthInfo(key, value))
			{
				this.SetOriginalConnectionStringValue("Ext Auth Info", value);
				return;
			}
			if (this.CheckAndSetIdentityProvider(key, value))
			{
				this.SetOriginalConnectionStringValue("Identity Provider", value);
				return;
			}
			if (this.CheckAndSetBypassAuthorization(key, value))
			{
				this.SetOriginalConnectionStringValue("Bypass Authorization", value);
				return;
			}
			if (this.CheckAndSetRestrictCatalog(key, value))
			{
				this.SetOriginalConnectionStringValue("Restrict Catalog", value);
				return;
			}
			if (this.CheckAndSetAccessMode(key, value))
			{
				this.SetOriginalConnectionStringValue("Access Mode", value);
				return;
			}
			if (this.CheckAndSetRestrictUser(key, value))
			{
				this.SetOriginalConnectionStringValue("Restrict User", value);
				return;
			}
			if (this.CheckAndSetRestrictRoles(key, value))
			{
				this.SetOriginalConnectionStringValue("Restrict Roles", value);
				return;
			}
			if (this.CheckAndSetIntendedUsage(key, value))
			{
				this.SetOriginalConnectionStringValue("Intended Usage", value);
				return;
			}
			if (this.CheckAndSetDedicatedAdminConnection(key, value))
			{
				this.SetOriginalConnectionStringValue("DedicatedAdminConnection", value);
				return;
			}
			if (this.CheckAndSetContextualIdentity(key, value))
			{
				this.SetOriginalConnectionStringValue("ContextualIdentity", value);
				return;
			}
			if (this.CheckAndSetConnectionActivityId(key, value))
			{
				this.SetOriginalConnectionStringValue("ConnectionActivityId", value);
				return;
			}
			if (this.CheckAndSetAsAzureRedirection(key, value))
			{
				this.SetOriginalConnectionStringValue("AsAzureRedirection", value);
				return;
			}
			if (this.CheckAndSetScaleOutAutoSync(key, value))
			{
				this.SetOriginalConnectionStringValue("Scale Out Auto Sync", value);
				return;
			}
			if (this.CheckAndSetSourceCapacityObjectId(key, value))
			{
				this.SetOriginalConnectionStringValue("SourceCapacityObjectId", value);
				return;
			}
			if (this.CheckAndSetServicePrincipalProfileId(key, value))
			{
				this.SetOriginalConnectionStringValue("SPN Profile", value);
				return;
			}
			if (this.CheckAndSetBypassBuildPermission(key, value))
			{
				this.SetOriginalConnectionStringValue("BypassBuildPermission", value);
				return;
			}
			for (int i = 0; i < ConnectionInfo.DeprecatedPropertyNames.Length; i++)
			{
				if (string.Compare(key, ConnectionInfo.DeprecatedPropertyNames[i], StringComparison.OrdinalIgnoreCase) == 0)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
				}
			}
			if (this.CheckAndSetSafetyOptions(key, value))
			{
				key = "Safety Options";
			}
			else if (this.CheckAndSetCatalog(key, value))
			{
				key = "Catalog";
			}
			else
			{
				this.CheckAndSetTimeout(key, value);
			}
			this.SetOriginalConnectionStringValue(key, value);
			this.InsertKeyValueIntoHash(key, value);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005DC0 File Offset: 0x00003FC0
		private void InsertKeyValueIntoHash(string propName, string propValue)
		{
			string text;
			if (ConnectionInfo.PropInfo.Properties.ContainsKey(propName))
			{
				if (!(string.Empty != ConnectionInfo.PropInfo.Properties[propName].ToString()))
				{
					return;
				}
				text = ConnectionInfo.PropInfo.Properties[propName].ToString();
			}
			else
			{
				if (!XmlReader.IsName(propName))
				{
					throw new ArgumentException(XmlaSR.ConnectionString_InvalidPropertyNameFormat(propName));
				}
				text = propName;
			}
			this.extendedProperties[text] = propValue;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005E40 File Offset: 0x00004040
		private bool CheckAndSetDataSource(string key, string value)
		{
			int i = 0;
			int num = ConnectionInfo.DatasourcePropertyNames.Length;
			while (i < num)
			{
				if (string.Compare(key, ConnectionInfo.DatasourcePropertyNames[i], StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.dataSource = ConnectionInfo.Trim(value);
					this.ParseDataSourceAccessMode(ref this.dataSource);
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005E8C File Offset: 0x0000408C
		private bool CheckAndSetLocation(string key, string value)
		{
			if (string.Compare(key, "Location", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.location = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005EAC File Offset: 0x000040AC
		private bool CheckAndSetUserID(string key, string value)
		{
			int i = 0;
			int num = ConnectionInfo.UserIDPropertyNames.Length;
			while (i < num)
			{
				if (string.Compare(key, ConnectionInfo.UserIDPropertyNames[i], StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.userID = ConnectionInfo.Trim(value);
					if (this.userID == null && !string.IsNullOrEmpty(value))
					{
						this.options.Enable(ConnectionInfo.ConnectionOptions.DisableSingleSignOn);
					}
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005F0D File Offset: 0x0000410D
		private bool CheckAndSetPassword(string key, string value)
		{
			if (ConnectionInfo.IsPassword(key))
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.password = value;
					this.options.Enable(ConnectionInfo.ConnectionOptions.PasswordPresent);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005F3C File Offset: 0x0000413C
		private bool CheckAndSetCatalog(string key, string value)
		{
			int i = 0;
			int num = ConnectionInfo.CatalogPropertyNames.Length;
			while (i < num)
			{
				if (string.Compare(key, ConnectionInfo.CatalogPropertyNames[i], StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.catalog = ConnectionInfo.Trim(value);
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005F7C File Offset: 0x0000417C
		private bool CheckAndSetProtectionLevel(string key, string value)
		{
			if (string.Compare(key, "Protection Level", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (string.Compare(value, "NONE", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.protectionLevel = ProtectionLevel.None;
				}
				else if (string.Compare(value, "CONNECT", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.protectionLevel = ProtectionLevel.Connection;
				}
				else if (string.Compare(value, "PKT INTEGRITY", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.protectionLevel = ProtectionLevel.Integrity;
				}
				else
				{
					if (string.Compare(value, "PKT PRIVACY", StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Protection Level", value));
					}
					this.protectionLevel = ProtectionLevel.Privacy;
				}
				this.options.Enable(ConnectionInfo.ConnectionOptions.ProtectionLevelWasSet);
				return true;
			}
			return false;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000601C File Offset: 0x0000421C
		private bool CheckAndSetIntegratedSecurity(string key, string value)
		{
			if (string.Compare(key, "Integrated Security", StringComparison.OrdinalIgnoreCase) == 0)
			{
				value = ConnectionInfo.Trim(value);
				if (value == null)
				{
					this.integratedSecurity = IntegratedSecurity.Unspecified;
				}
				else if (string.Compare(value, "SSPI", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.integratedSecurity = IntegratedSecurity.Sspi;
				}
				else if (string.Compare(value, "Basic", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.integratedSecurity = IntegratedSecurity.Basic;
				}
				else if (string.Compare(value, "Federated", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.integratedSecurity = IntegratedSecurity.Federated;
				}
				else
				{
					if (string.Compare(value, "ClaimsToken", StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Integrated Security", value));
					}
					this.integratedSecurity = IntegratedSecurity.ClaimsToken;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000060C0 File Offset: 0x000042C0
		private bool CheckAndSetConnectTo(string key, string value)
		{
			if (string.Compare(key, "ConnectTo", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (string.IsNullOrEmpty(value))
				{
					this.connectTo = ConnectTo.Default;
				}
				else
				{
					value = ConnectionInfo.Trim(value);
					if (string.Compare(value, "DEFAULT", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.connectTo = ConnectTo.Default;
					}
					else
					{
						if (string.Compare(value, "8.0", StringComparison.OrdinalIgnoreCase) == 0)
						{
							throw new ArgumentException(XmlaSR.ConnectionString_ShilohIsNoLongerSupported("ConnectTo"));
						}
						if (string.Compare(value, "9.0", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.connectTo = ConnectTo.Yukon;
						}
						else if (string.Compare(value, "10.0", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.connectTo = ConnectTo.Katmai;
						}
						else if (string.Compare(value, "11.0", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.connectTo = ConnectTo.Denali;
						}
						else if (string.Compare(value, "12.0", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.connectTo = ConnectTo.SQL14;
						}
						else if (string.Compare(value, "13.0", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.connectTo = ConnectTo.SQL15;
						}
						else if (string.Compare(value, "14.0", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.connectTo = ConnectTo.SQL17;
						}
						else if (string.Compare(value, "15.0", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.connectTo = ConnectTo.SSAS18;
						}
						else
						{
							if (string.Compare(value, "16.0", StringComparison.OrdinalIgnoreCase) != 0)
							{
								throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("ConnectTo", value));
							}
							this.connectTo = ConnectTo.SSAS21;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006208 File Offset: 0x00004408
		private bool CheckAndSetTimeout(string key, string value)
		{
			if (string.Compare(key, "Timeout", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (!string.IsNullOrEmpty(value))
				{
					try
					{
						this.timeout = int.Parse(value, CultureInfo.InvariantCulture);
						return true;
					}
					catch (FormatException ex)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Timeout", value), ex);
					}
					catch (OverflowException ex2)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Timeout", value), ex2);
					}
					return true;
				}
				this.timeout = 0;
				return true;
			}
			return false;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006294 File Offset: 0x00004494
		private bool CheckAndSetConnectTimeout(string key, string value)
		{
			if (string.Compare(key, "Connect Timeout", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (string.IsNullOrEmpty(value))
				{
					this.connectTimeout = 60;
				}
				else
				{
					try
					{
						this.connectTimeout = int.Parse(value, CultureInfo.InvariantCulture);
					}
					catch (FormatException ex)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Connect Timeout", value), ex);
					}
					catch (OverflowException ex2)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Connect Timeout", value), ex2);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000631C File Offset: 0x0000451C
		private bool CheckAndSetAutoSyncPeriod(string key, string value)
		{
			if (string.Compare(key, "Auto Synch Period", StringComparison.OrdinalIgnoreCase) == 0)
			{
				try
				{
					if (string.Compare(value, "NULL", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.autoSyncPeriod = 0U;
					}
					else
					{
						this.autoSyncPeriod = uint.Parse(value, CultureInfo.InvariantCulture);
					}
					return true;
				}
				catch (FormatException ex)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Auto Synch Period", value), ex);
				}
				catch (OverflowException ex2)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Auto Synch Period", value), ex2);
				}
				return false;
			}
			return false;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000063A8 File Offset: 0x000045A8
		private bool CheckAndSetSafetyOptions(string key, string value)
		{
			if (string.Compare(key, "Safety Options", StringComparison.OrdinalIgnoreCase) == 0)
			{
				uint num = 3U;
				try
				{
					num = uint.Parse(value, CultureInfo.InvariantCulture);
				}
				catch (FormatException ex)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Safety Options", value), ex);
				}
				catch (OverflowException ex2)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Safety Options", value), ex2);
				}
				switch (num)
				{
				case 0U:
					this.safetyOptions = SafetyOptions.Default;
					break;
				case 1U:
					this.safetyOptions = SafetyOptions.All;
					break;
				case 2U:
					this.safetyOptions = SafetyOptions.Safe;
					break;
				case 3U:
					this.safetyOptions = SafetyOptions.None;
					break;
				default:
					throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Safety Options", value));
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006468 File Offset: 0x00004668
		private bool CheckAndSetProtocolFormat(string key, string value)
		{
			if (string.Compare(key, "Protocol Format", StringComparison.OrdinalIgnoreCase) == 0)
			{
				value = ConnectionInfo.Trim(value);
				if (string.Compare(value, "Default", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.protocolFormat = ProtocolFormat.Default;
				}
				else if (string.Compare(value, "XML", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.protocolFormat = ProtocolFormat.Xml;
				}
				else
				{
					if (string.Compare(value, "Binary", StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue(key, value));
					}
					this.protocolFormat = ProtocolFormat.Binary;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000064E0 File Offset: 0x000046E0
		private bool CheckAndSetTransportCompression(string key, string value)
		{
			if (string.Compare(key, "Transport Compression", StringComparison.OrdinalIgnoreCase) == 0)
			{
				value = ConnectionInfo.Trim(value);
				if (string.Compare(value, "Default", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.transportCompression = TransportCompression.Default;
				}
				else if (string.Compare(value, "None", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.transportCompression = TransportCompression.None;
				}
				else if (string.Compare(value, "Compressed", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.transportCompression = TransportCompression.Compressed;
				}
				else
				{
					if (string.Compare(value, "Gzip", StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue(key, value));
					}
					this.transportCompression = TransportCompression.Gzip;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006570 File Offset: 0x00004770
		private bool CheckAndSetCompressionLevel(string key, string value)
		{
			if (string.Compare(key, "Compression Level", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			int num;
			try
			{
				num = int.Parse(value, CultureInfo.InvariantCulture);
			}
			catch (FormatException ex)
			{
				throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue(key, value), ex);
			}
			catch (OverflowException ex2)
			{
				throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue(key, value), ex2);
			}
			if (num < 0 || num > 9)
			{
				throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue(key, value));
			}
			this.compressionLevel = num;
			return true;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000065F4 File Offset: 0x000047F4
		private bool CheckAndSetEncryptionPassword(string key, string value)
		{
			if (string.Compare(key, "Encryption Password", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.encryptionPassword = value;
				return true;
			}
			return false;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006610 File Offset: 0x00004810
		private bool CheckAndSetImpersonationLevel(string key, string value)
		{
			if (string.Compare(key, "Impersonation Level", StringComparison.OrdinalIgnoreCase) == 0)
			{
				value = ConnectionInfo.Trim(value);
				if (string.Compare(value, "Anonymous", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.impersonationLevel = ImpersonationLevel.Anonymous;
				}
				else if (string.Compare(value, "Identify", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.impersonationLevel = ImpersonationLevel.Identify;
				}
				else if (string.Compare(value, "Impersonate", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.impersonationLevel = ImpersonationLevel.Impersonate;
				}
				else
				{
					if (string.Compare(value, "Delegate", StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Impersonation Level", value));
					}
					this.impersonationLevel = ImpersonationLevel.Delegate;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000066A3 File Offset: 0x000048A3
		private bool CheckAndSetRestrictedClient(string key, string value)
		{
			if (string.Compare(key, "Restricted Client", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.RestrictedClient, ConnectionInfo.ParseBool(value));
				return true;
			}
			return false;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000066C9 File Offset: 0x000048C9
		private bool CheckAndSetSspi(string key, string value)
		{
			if (string.Compare(key, "SSPI", StringComparison.OrdinalIgnoreCase) == 0)
			{
				value = ConnectionInfo.Trim(value);
				this.sspi = ((value == null) ? "Negotiate" : value);
				return true;
			}
			return false;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000066F8 File Offset: 0x000048F8
		private bool CheckAndSetHttpHandling(string key, string value)
		{
			if (string.Compare(key, "HttpChannelHandling", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (string.Compare(value, "Default", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.HttpHandling = HttpChannelHandling.Default;
				}
				else if (string.Compare(value, "WebRequestBased", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.HttpHandling = HttpChannelHandling.WebRequestBased;
				}
				else
				{
					if (string.Compare(value, "PreferHttpClient", StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("HttpChannelHandling", value));
					}
					this.HttpHandling = HttpChannelHandling.PreferHttpClient;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000676C File Offset: 0x0000496C
		private bool CheckAndSetUseExistingFile(string key, string value)
		{
			if (string.Compare(key, "UseExistingFile", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (string.IsNullOrEmpty(value) || string.Compare(value, bool.FalseString, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.options.Disable(ConnectionInfo.ConnectionOptions.UseExistingFile);
				}
				else
				{
					if (string.Compare(value, bool.TrueString, StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("UseExistingFile", value));
					}
					this.options.Enable(ConnectionInfo.ConnectionOptions.UseExistingFile);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000067E0 File Offset: 0x000049E0
		private bool CheckAndSetCharacterEncoding(string key, string value)
		{
			if (string.Compare(key, "Character Encoding", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (string.IsNullOrEmpty(value))
				{
					this.characterEncoding = Encoding.UTF8;
				}
				else if (string.Compare(value, "Default", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.characterEncoding = Encoding.UTF8;
				}
				else
				{
					try
					{
						this.characterEncoding = Encoding.GetEncoding(value);
					}
					catch (NotSupportedException ex)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Character Encoding", value), ex);
					}
					catch (ArgumentException ex2)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Character Encoding", value), ex2);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006880 File Offset: 0x00004A80
		private bool CheckAndSetUseEncryptionForData(string key, string value)
		{
			if (string.Compare(key, "Use Encryption for Data", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = ConnectionInfo.Trim(value);
					if (string.Compare(value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.options.Enable(ConnectionInfo.ConnectionOptions.UseEncryptionForData);
					}
					else
					{
						if (string.Compare(value, bool.FalseString, StringComparison.OrdinalIgnoreCase) != 0)
						{
							throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Use Encryption for Data", value));
						}
						this.options.Disable(ConnectionInfo.ConnectionOptions.UseEncryptionForData);
					}
					this.options.Enable(ConnectionInfo.ConnectionOptions.UseEncryptionForDataWasSet);
				}
				else
				{
					this.options.Disable(ConnectionInfo.ConnectionOptions.UseEncryptionForData | ConnectionInfo.ConnectionOptions.UseEncryptionForDataWasSet);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006920 File Offset: 0x00004B20
		private bool CheckAndSetPacketSize(string key, string value)
		{
			if (string.Compare(key, "Packet Size", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (!string.IsNullOrEmpty(value))
				{
					int num;
					try
					{
						num = int.Parse(value, CultureInfo.InvariantCulture);
					}
					catch (FormatException ex)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Packet Size", value), ex);
					}
					catch (OverflowException ex2)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Packet Size", value), ex2);
					}
					if (num < 512 || num > 32767)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Packet Size", value));
					}
					this.packetSize = num;
				}
				else
				{
					this.packetSize = 4096;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000069CC File Offset: 0x00004BCC
		private bool CheckAndSetDataSourceVersion(string key, string value)
		{
			if (!"Data Source Version".Equals(key, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Data Source Version", value));
			}
			this.dataSourceVersion = value;
			return true;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006A00 File Offset: 0x00004C00
		private bool CheckAndSetUserIdentity(string key, string value)
		{
			if (string.Compare(key, "User Identity", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (string.IsNullOrEmpty(value))
				{
					this.userIdentity = UserIdentityType.WindowsIdentity;
				}
				else if (string.Compare(value, "DEFAULT", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.userIdentity = UserIdentityType.WindowsIdentity;
				}
				else if (string.Compare(value, "Windows Identity", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.userIdentity = UserIdentityType.WindowsIdentity;
				}
				else
				{
					if (string.Compare(value, "SharePoint Principal", StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("User Identity", value));
					}
					this.userIdentity = UserIdentityType.SharePointPrincipal;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006A88 File Offset: 0x00004C88
		private bool CheckAndSetTokenCacheMode(string key, string value)
		{
			if (string.Compare(key, "Token Cache Mode", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Token Cache Mode", value));
			}
			TokenCacheMode tokenCacheMode;
			if (!Enum.TryParse<TokenCacheMode>(value, true, out tokenCacheMode))
			{
				int num;
				if (!int.TryParse(value, out num) || num < 0 || num > 2)
				{
					throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Token Cache Mode", value));
				}
				tokenCacheMode = (TokenCacheMode)num;
			}
			switch (tokenCacheMode)
			{
			case TokenCacheMode.Default:
				this.options.Enable(ConnectionInfo.ConnectionOptions.TokenCacheInDefaultMode);
				this.options.Disable(ConnectionInfo.ConnectionOptions.TokenCacheExplicitMode);
				break;
			case TokenCacheMode.Disabled:
				this.options.Disable(ConnectionInfo.ConnectionOptions.TokenCacheInDefaultMode | ConnectionInfo.ConnectionOptions.TokenCacheExplicitMode);
				break;
			case TokenCacheMode.Enabled:
				this.options.Disable(ConnectionInfo.ConnectionOptions.TokenCacheInDefaultMode);
				this.options.Enable(ConnectionInfo.ConnectionOptions.TokenCacheExplicitMode);
				break;
			}
			return true;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006B5C File Offset: 0x00004D5C
		private bool CheckAndSetUseAdalCache(string key, string value)
		{
			if (string.Compare(key, "UseADALCache", StringComparison.OrdinalIgnoreCase) == 0)
			{
				value = ConnectionInfo.Trim(value);
				if (value == null)
				{
					this.options.Enable(ConnectionInfo.ConnectionOptions.TokenCacheInDefaultMode);
					this.options.Disable(ConnectionInfo.ConnectionOptions.TokenCacheExplicitMode | ConnectionInfo.ConnectionOptions.UseADTranslation);
				}
				else
				{
					int num;
					if (!int.TryParse(value, out num))
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("UseADALCache", value));
					}
					switch (num)
					{
					case 0:
						this.options.Disable(ConnectionInfo.ConnectionOptions.TokenCacheInDefaultMode | ConnectionInfo.ConnectionOptions.TokenCacheExplicitMode | ConnectionInfo.ConnectionOptions.UseADTranslation);
						break;
					case 1:
						this.options.Enable(ConnectionInfo.ConnectionOptions.TokenCacheInDefaultMode);
						this.options.Disable(ConnectionInfo.ConnectionOptions.TokenCacheExplicitMode | ConnectionInfo.ConnectionOptions.UseADTranslation);
						break;
					case 2:
						this.options.Disable(ConnectionInfo.ConnectionOptions.TokenCacheInDefaultMode | ConnectionInfo.ConnectionOptions.TokenCacheExplicitMode);
						this.options.Enable(ConnectionInfo.ConnectionOptions.UseADTranslation);
						break;
					default:
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("UseADALCache", value));
					}
				}
				this.isUseAdalCacheSpecifiedOnConnectionString = true;
				return true;
			}
			return false;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006C4B File Offset: 0x00004E4B
		private bool CheckAndSetApplyAuxiliaryPermission(string key, string value)
		{
			if (string.Compare(key, "ApplyAuxiliaryPermission", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.ApplyAuxiliaryPermission, ConnectionInfo.ParseBool(value));
				return true;
			}
			return false;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006C75 File Offset: 0x00004E75
		private bool CheckAndSetAuxiliaryPermissionOwner(string key, string value)
		{
			if (string.Compare(key, "AuxiliaryPermissionOwner", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.auxiliaryPermissionOwner = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006C94 File Offset: 0x00004E94
		private bool CheckAndSetServiceToServiceToken(string key, string value)
		{
			if (string.Compare(key, "ServiceToServiceToken", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.serviceToServiceToken = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006CB4 File Offset: 0x00004EB4
		private bool CheckAndSetTransientModelMode(string key, string value)
		{
			if (string.Compare(key, "Transient Model", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			value = ConnectionInfo.Trim(value);
			if (string.Compare(value, "Enabled", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(value, "Disabled", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
			}
			this.transientModelMode = value;
			return true;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006D08 File Offset: 0x00004F08
		private bool CheckAndSetIsInternalPaaSInfrastructure(string key, string value)
		{
			if (string.Compare(key, "IsInternalPaaSInfrastructure", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(key, "IsInternalASAzure", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.InternalPaaSInfrastructure, ConnectionInfo.ParseBool(value));
				return true;
			}
			return false;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006D40 File Offset: 0x00004F40
		private bool CheckAndSetIsInternalTestInfrastructure(string key, string value)
		{
			if (string.Compare(key, "IsInternalTestInfrastructure", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.InternalTestInfrastructure, ConnectionInfo.ParseBool(value));
				return true;
			}
			return false;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006D6A File Offset: 0x00004F6A
		private bool CheckAndSetPbipCoreServiceRoutingHint(string key, string value)
		{
			if (string.Compare(key, "PbipCoreServiceRoutingHint", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.pbipCoreServiceRoutingHint = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006D8C File Offset: 0x00004F8C
		private bool CheckAndSetWorkloadDirectConnection(string key, string value)
		{
			if (string.Compare(key, "WorkloadDirectConnection", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (string.Compare(ConnectionInfo.Trim(value), bool.TrueString, StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(ConnectionInfo.Trim(value), bool.FalseString, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("WorkloadDirectConnection", value));
			}
			this.options.Update(ConnectionInfo.ConnectionOptions.WorkloadDirectConnection, string.Compare(ConnectionInfo.Trim(value), bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0);
			return true;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006E06 File Offset: 0x00005006
		private bool CheckAndSetPBIPVirtualServiceName(string key, string value)
		{
			if (string.Compare(key, "PBIPVirtualServiceName", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.pbipVirtualServiceName = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006E25 File Offset: 0x00005025
		private bool CheckAndSetContextualIdentityKey(string key, string value)
		{
			if (string.Compare(key, "ContextualIdentityKey", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.contextualIdentityKey = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006E44 File Offset: 0x00005044
		private bool CheckAndSetContextualIdentityType(string key, string value)
		{
			if (string.Compare(key, "ContextualIdentityType", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			value = ConnectionInfo.Trim(value);
			if (string.Compare(value, "bearertoken", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(value, "embedtoken", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
			}
			this.contextualIdentityType = value;
			return true;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006E98 File Offset: 0x00005098
		private bool CheckAndSetClientCertificateThumbprint(string key, string value)
		{
			if (string.Compare(key, "Client Certificate Thumbprint", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.clientCertificateThumbprint = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006EB7 File Offset: 0x000050B7
		private bool CheckAndSetExtendedProperties(string key, string value)
		{
			if (ConnectionInfo.IsExtendedProperties(key))
			{
				this.innerConnectionString = value;
				return true;
			}
			return false;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006ECC File Offset: 0x000050CC
		private bool CheckAndSetPersistSecurityInfo(string key, string value)
		{
			if (string.Compare(key, "Persist Security Info", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			value = ConnectionInfo.Trim(value);
			bool flag;
			if (value == null || !bool.TryParse(value, out flag))
			{
				throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Persist Security Info", value));
			}
			this.options.Update(ConnectionInfo.ConnectionOptions.PersistSecurityInfo, flag);
			return true;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006F1E File Offset: 0x0000511E
		private bool CheckAndSetSessionID(string key, string value)
		{
			if (string.Compare(key, "SessionID", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.sessionID = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006F3D File Offset: 0x0000513D
		private bool CheckAndSetCertificate(string key, string value)
		{
			if (string.Compare(key, "Certificate", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.certificate = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006F5C File Offset: 0x0000515C
		private bool CheckAndSetAuthenticationScheme(string key, string value)
		{
			if (string.Compare(key, "Authentication Scheme", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.authenticationScheme = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006F7B File Offset: 0x0000517B
		private bool CheckAndSetExtAuthInfo(string key, string value)
		{
			if (string.Compare(key, "Ext Auth Info", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.extAuthInfo = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006F9A File Offset: 0x0000519A
		private bool CheckAndSetIdentityProvider(string key, string value)
		{
			if (string.Compare(key, "Identity Provider", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.identityProvider = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006FB9 File Offset: 0x000051B9
		private bool CheckAndSetBypassAuthorization(string key, string value)
		{
			if (string.Compare(key, "Bypass Authorization", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.bypassAuthorization = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006FD8 File Offset: 0x000051D8
		private bool CheckAndSetRestrictCatalog(string key, string value)
		{
			if (string.Compare(key, "Restrict Catalog", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.restrictCatalog = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006FF7 File Offset: 0x000051F7
		private bool CheckAndSetAccessMode(string key, string value)
		{
			if (string.Compare(key, "Access Mode", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.accessMode = ConnectionInfo.Trim(value);
				this.SetConnectionAccessMode(this.accessMode);
				return true;
			}
			return false;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007022 File Offset: 0x00005222
		private bool CheckAndSetRestrictUser(string key, string value)
		{
			if (string.Compare(key, "Restrict User", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.restrictUser = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007041 File Offset: 0x00005241
		private bool CheckAndSetRestrictRoles(string key, string value)
		{
			if (!key.Equals("Restrict Roles", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			this.restrictRoles = ConnectionInfo.Trim(value);
			return true;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007060 File Offset: 0x00005260
		private bool CheckAndSetIntendedUsage(string key, string value)
		{
			if (string.Compare(key, "Intended Usage", StringComparison.OrdinalIgnoreCase) == 0)
			{
				string text = ConnectionInfo.Trim(value);
				if (string.Equals(text, "default", StringComparison.InvariantCultureIgnoreCase))
				{
					this.intendedUsage = IntendedUsage.Default;
				}
				else if (string.Equals(text, "processing", StringComparison.InvariantCultureIgnoreCase))
				{
					this.intendedUsage = IntendedUsage.ScheduledProcessing;
				}
				else if (string.Equals(text, "interactiveprocessing", StringComparison.InvariantCultureIgnoreCase))
				{
					this.intendedUsage = IntendedUsage.InteractiveProcessing;
				}
				else
				{
					if (!string.Equals(text, "backgroundquery", StringComparison.InvariantCultureIgnoreCase))
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Intended Usage", value));
					}
					this.intendedUsage = IntendedUsage.BackgroundQuery;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000070F2 File Offset: 0x000052F2
		private bool CheckAndSetDedicatedAdminConnection(string key, string value)
		{
			if (string.Compare(key, "DedicatedAdminConnection", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.DedicatedAdminConnection, ConnectionInfo.ParseBool(value));
				return true;
			}
			return false;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000711C File Offset: 0x0000531C
		private bool CheckAndSetContextualIdentity(string key, string value)
		{
			if (string.Compare(key, "ContextualIdentity", StringComparison.OrdinalIgnoreCase) == 0)
			{
				foreach (char c in value)
				{
					if (c != '+' && c != '/' && c != '=' && c != '.' && c != '-' && c != '_' && (c > 'z' || c < 'a') && (c > 'Z' || c < 'A') && (c > '9' || c < '0'))
					{
						throw new ArgumentException(XmlaSR.ConnectionString_Invalid);
					}
				}
				this.contextualIdentity = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000071A8 File Offset: 0x000053A8
		private bool CheckAndSetConnectionActivityId(string key, string value)
		{
			if (string.Compare(key, "ConnectionActivityId", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (!Guid.TryParse(value, out this.connectionActivityId))
			{
				throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("ConnectionActivityId", value));
			}
			return true;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000071DC File Offset: 0x000053DC
		private bool CheckAndSetAsAzureRedirection(string key, string value)
		{
			if (string.Compare(key, "AsAzureRedirection", StringComparison.OrdinalIgnoreCase) == 0)
			{
				try
				{
					this.asAzureRedirection = (AsAzureRedirection)Enum.Parse(typeof(AsAzureRedirection), value, true);
					return true;
				}
				catch
				{
					throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("AsAzureRedirection", value));
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000723C File Offset: 0x0000543C
		private bool CheckAndSetScaleOutAutoSync(string key, string value)
		{
			if (string.Compare(key, "Scale Out Auto Sync", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (string.IsNullOrEmpty(value) || string.Compare(value, bool.FalseString, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.options.Disable(ConnectionInfo.ConnectionOptions.ScaleOutAutoSync);
				}
				else
				{
					if (string.Compare(value, bool.TrueString, StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Scale Out Auto Sync", value));
					}
					this.options.Enable(ConnectionInfo.ConnectionOptions.ScaleOutAutoSync);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000072B5 File Offset: 0x000054B5
		private bool CheckAndSetSourceCapacityObjectId(string key, string value)
		{
			if (string.Compare(key, "SourceCapacityObjectId", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (!Guid.TryParse(value, out this.sourceCapacityObjectId))
			{
				throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("SourceCapacityObjectId", value));
			}
			return true;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000072E7 File Offset: 0x000054E7
		private bool CheckAndSetServicePrincipalProfileId(string key, string value)
		{
			if (string.Compare(key, "SPN Profile", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.servicePrincipalProfileId = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007306 File Offset: 0x00005506
		private bool CheckAndSetBypassBuildPermission(string key, string value)
		{
			if (string.Compare(key, "BypassBuildPermission", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.BypassBuildPermission, ConnectionInfo.ParseBool(value));
				return true;
			}
			return false;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007330 File Offset: 0x00005530
		private void ParseDataSourceAccessMode(ref string dataSource)
		{
			if (ConnectivityHelper.IsPbiPremiumConnection(dataSource))
			{
				int num;
				if ((num = ConnectivityHelper.UriIndexOfQuery(dataSource, "readonly")) != -1)
				{
					this.PbipAccessMode = ConnectionAccessMode.ReadOnly;
					ConnectionInfo.RemoveDataSourceAccessMode(ref dataSource, "readonly", num);
					return;
				}
				if ((num = ConnectivityHelper.UriIndexOfQuery(dataSource, "readwrite")) != -1)
				{
					this.PbipAccessMode = ConnectionAccessMode.ReadWrite;
					ConnectionInfo.RemoveDataSourceAccessMode(ref dataSource, "readwrite", num);
				}
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000738F File Offset: 0x0000558F
		private void SetConnectionAccessMode(string accessMode)
		{
			if (accessMode == "readonly")
			{
				this.PbipAccessMode = ConnectionAccessMode.ReadOnly;
				return;
			}
			if (accessMode == "readwrite")
			{
				this.PbipAccessMode = ConnectionAccessMode.ReadWrite;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000073BA File Offset: 0x000055BA
		[Conditional("_AS_ADOMD")]
		private void SetOriginalConnectionStringValue(string key, string value)
		{
			this.originalConnStringProps[key] = value;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000073CC File Offset: 0x000055CC
		private bool TryBuildExternalAuthenticationHandle(IConnectivityOwner owner, out AuthenticationHandle handle)
		{
			if (owner != null && owner.AccessToken.IsValid)
			{
				handle = new ConnectionInfo.RefreshableExternalAuthenticationHandle(owner, AuthenticationHandle.ConvertIdentityProviderToTokenScheme(this.identityProvider));
				return true;
			}
			if (this.UserID == null && this.Password != null)
			{
				handle = new ExternalAuthenticationHandle(this.Password, AuthenticationHandle.ConvertIdentityProviderToTokenScheme(this.identityProvider));
				return true;
			}
			handle = null;
			return false;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007430 File Offset: 0x00005630
		private AuthenticationHandle AcquireToken(IConnectivityOwner owner, string resource, string tenantId, bool isForAsAzureRedirection)
		{
			AuthenticationHandle authenticationHandle;
			if (this.TryBuildExternalAuthenticationHandle(owner, out authenticationHandle))
			{
				return authenticationHandle;
			}
			AuthenticationHandle authenticationHandle2;
			using (AuthenticationTracer.StartScope("ConnectionInfo.AcquireToken"))
			{
				try
				{
					AuthenticationTracer.TraceInformation("ConnectionInfo.AcquireToken - resource:'{0}', dataSource:'{1}', identityProvider:'{2}', tenantId:'{3}', userId:'{4}', TokenCacheMode:{5}, UseADTranslation:{6}, DisableSingleSignOn={7}, isForAsAzureRedirection:{8}", new object[]
					{
						resource,
						this.DataSource,
						this.IdentityProvider,
						tenantId,
						this.UserID,
						this.options.IsEnabled(ConnectionInfo.ConnectionOptions.TokenCacheInDefaultMode) ? "Default" : (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.TokenCacheExplicitMode) ? "Enabled" : "Disabled"),
						this.options.IsEnabled(ConnectionInfo.ConnectionOptions.UseADTranslation),
						this.options.IsEnabled(ConnectionInfo.ConnectionOptions.DisableSingleSignOn),
						isForAsAzureRedirection
					});
					SingleSignOnMode singleSignOnMode;
					if (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.UseADTranslation))
					{
						singleSignOnMode = SingleSignOnMode.Mandatory;
					}
					else if (this.options.IsEnabled(ConnectionInfo.ConnectionOptions.DisableSingleSignOn) || (!string.IsNullOrEmpty(this.UserID) && !string.IsNullOrEmpty(this.Password)))
					{
						singleSignOnMode = SingleSignOnMode.Disabled;
					}
					else
					{
						singleSignOnMode = SingleSignOnMode.Supported;
					}
					AuthenticationInformation authenticationInformation;
					authenticationHandle2 = Microsoft.AnalysisServices.AdomdClient.Authentication.AuthenticationManager.Authenticate(new AuthenticationOptions(owner)
					{
						UseTokenCache = this.options.IsPartialyEnabled(ConnectionInfo.ConnectionOptions.TokenCacheInDefaultMode | ConnectionInfo.ConnectionOptions.TokenCacheExplicitMode),
						SsoMode = singleSignOnMode,
						HasServicePrincipalProfile = !string.IsNullOrEmpty(this.ServicePrincipalProfileId)
					}, this.IdentityProvider, resource, tenantId, this.UserID, this.Password, null, null, isForAsAzureRedirection, out authenticationInformation);
				}
				catch (Exception ex)
				{
					AuthenticationTracer.TraceError("ConnectionInfo.AcquireToken failed; Exception: {0}", new object[] { ex });
					throw;
				}
			}
			return authenticationHandle2;
		}

		// Token: 0x04000099 RID: 153
		private const string LogicalActivityContextClient = "AnalysisServices.ClientActivityId";

		// Token: 0x0400009A RID: 154
		private const string LogicalActivityContextCurrent = "AnalysisServices.CurrentActivityId";

		// Token: 0x0400009B RID: 155
		internal const int DefaultInstancePort = 2383;

		// Token: 0x0400009C RID: 156
		private const int SqlBrowserPort = 2382;

		// Token: 0x0400009D RID: 157
		private static MDXMLAPropInfo PropInfo = new MDXMLAPropInfo();

		// Token: 0x0400009E RID: 158
		internal const string Localhost = "localhost";

		// Token: 0x0400009F RID: 159
		internal const string Embedded = "$Embedded$";

		// Token: 0x040000A0 RID: 160
		private static readonly IList<string> ppeEndpoints = new List<string> { "biazure-int-edog-redirect.analysis-df.windows.net", "onebox-redirect.analysis.windows-int.net" };

		// Token: 0x040000A1 RID: 161
		private const string PbiExternalDatabaseNamePrefix = "sobe_wowvirtualserver-";

		// Token: 0x040000A2 RID: 162
		private const string AnalyzeInExcelPath = "xmla";

		// Token: 0x040000A3 RID: 163
		private const string AnalyzeInExcelQueryFormat = "vs=sobe_wowvirtualserver&db={0}";

		// Token: 0x040000A4 RID: 164
		internal const int TimeoutDefault = 0;

		// Token: 0x040000A5 RID: 165
		internal const int ConnectTimeoutDefault = 60;

		// Token: 0x040000A6 RID: 166
		internal const uint AutoSyncPeriodDefault = 10000U;

		// Token: 0x040000A7 RID: 167
		private const ProtectionLevel ProtectionLevelDefault = ProtectionLevel.Privacy;

		// Token: 0x040000A8 RID: 168
		private const int CompressionLevelDefault = 0;

		// Token: 0x040000A9 RID: 169
		private const SafetyOptions SafetyOptionDefault = SafetyOptions.Default;

		// Token: 0x040000AA RID: 170
		private const IntegratedSecurity IntegratedSecurityDefault = IntegratedSecurity.Unspecified;

		// Token: 0x040000AB RID: 171
		private const ImpersonationLevel ImpersonationLevelDefault = ImpersonationLevel.Impersonate;

		// Token: 0x040000AC RID: 172
		private const string SspiDefault = "Negotiate";

		// Token: 0x040000AD RID: 173
		private const string SspiSchannel = "Schannel";

		// Token: 0x040000AE RID: 174
		private const string SspiUni = "Microsoft Unified Security Protocol Provider";

		// Token: 0x040000AF RID: 175
		private const int PacketSizeDefault = 4096;

		// Token: 0x040000B0 RID: 176
		private const int PacketSizeMinValue = 512;

		// Token: 0x040000B1 RID: 177
		private const int PacketSizeMaxValue = 32767;

		// Token: 0x040000B2 RID: 178
		private const string ProviderPropertyDefaultValue = "MSOLAP.2";

		// Token: 0x040000B3 RID: 179
		private const int MaxCharsInLinkFile = 4096;

		// Token: 0x040000B4 RID: 180
		private const string LinkFileExtension = ".bism";

		// Token: 0x040000B5 RID: 181
		private const string LinkFileXmlNamespace = "http://schemas.microsoft.com/analysisservices/linkfile";

		// Token: 0x040000B6 RID: 182
		private const string LinkFileMainElement = "ASLinkFile";

		// Token: 0x040000B7 RID: 183
		private const string LinkFileServerElement = "Server";

		// Token: 0x040000B8 RID: 184
		private const string LinkFileDatabaseElement = "Database";

		// Token: 0x040000B9 RID: 185
		private const string LinkFileDelegationAttribute = "allowDelegation";

		// Token: 0x040000BA RID: 186
		private const string LinkFileSchema = "<?xml version='1.0' encoding='utf-8'?> \r\n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' targetNamespace='http://schemas.microsoft.com/analysisservices/linkfile' elementFormDefault='qualified'>\r\n    <xs:element name='ASLinkFile'>\r\n                    <xs:complexType>\r\n                        <xs:all>\r\n                            <xs:element name='Server' type='xs:string'/>\r\n                            <xs:element name='Database' type='xs:string' />\r\n                            <xs:element name='Description' type='xs:string' minOccurs='0'/>\r\n                        </xs:all>\r\n                        <xs:attribute name='allowDelegation' type='xs:boolean' default='false'/>\r\n                    </xs:complexType>\r\n    </xs:element>\r\n</xs:schema>";

		// Token: 0x040000BB RID: 187
		private string connectionString;

		// Token: 0x040000BC RID: 188
		private ConnectionInfo.ConnectionOptionsWrapper options = new ConnectionInfo.ConnectionOptionsWrapper(ConnectionInfo.ConnectionOptions.Default);

		// Token: 0x040000BD RID: 189
		private ConnectionType connectionType;

		// Token: 0x040000BE RID: 190
		private AsInstanceType asInstanceType;

		// Token: 0x040000BF RID: 191
		private string dataSource;

		// Token: 0x040000C0 RID: 192
		private string linkReferenceRawDataSource;

		// Token: 0x040000C1 RID: 193
		private string location;

		// Token: 0x040000C2 RID: 194
		private string server;

		// Token: 0x040000C3 RID: 195
		private string instanceName;

		// Token: 0x040000C4 RID: 196
		private string port;

		// Token: 0x040000C5 RID: 197
		private string userID;

		// Token: 0x040000C6 RID: 198
		private string password;

		// Token: 0x040000C7 RID: 199
		private AuthenticationHandle authHandle;

		// Token: 0x040000C8 RID: 200
		private int timeout;

		// Token: 0x040000C9 RID: 201
		private int connectTimeout;

		// Token: 0x040000CA RID: 202
		private uint autoSyncPeriod;

		// Token: 0x040000CB RID: 203
		private string catalog;

		// Token: 0x040000CC RID: 204
		private ProtectionLevel protectionLevel;

		// Token: 0x040000CD RID: 205
		private ConnectTo connectTo;

		// Token: 0x040000CE RID: 206
		private SafetyOptions safetyOptions;

		// Token: 0x040000CF RID: 207
		private ProtocolFormat protocolFormat;

		// Token: 0x040000D0 RID: 208
		private TransportCompression transportCompression;

		// Token: 0x040000D1 RID: 209
		private int compressionLevel;

		// Token: 0x040000D2 RID: 210
		private IntegratedSecurity integratedSecurity;

		// Token: 0x040000D3 RID: 211
		private string encryptionPassword;

		// Token: 0x040000D4 RID: 212
		private ImpersonationLevel impersonationLevel;

		// Token: 0x040000D5 RID: 213
		private string sspi;

		// Token: 0x040000D6 RID: 214
		private Encoding characterEncoding;

		// Token: 0x040000D7 RID: 215
		private int packetSize;

		// Token: 0x040000D8 RID: 216
		private string innerConnectionString;

		// Token: 0x040000D9 RID: 217
		private ListDictionary extendedProperties = new ListDictionary();

		// Token: 0x040000DA RID: 218
		private string restrictedConnectionString;

		// Token: 0x040000DB RID: 219
		private string sessionID;

		// Token: 0x040000DC RID: 220
		private string clientCertificateThumbprint;

		// Token: 0x040000DD RID: 221
		private Guid autoGeneratedActivityID = Guid.NewGuid();

		// Token: 0x040000DE RID: 222
		private UserIdentityType userIdentity;

		// Token: 0x040000DF RID: 223
		private string pbiPremiumTenant;

		// Token: 0x040000E0 RID: 224
		private string pbiPremiumWorkspaceName;

		// Token: 0x040000E1 RID: 225
		private string pbiPremiumWorkspaceObjectId;

		// Token: 0x040000E2 RID: 226
		private string pbipWorkloadResourceMoniker;

		// Token: 0x040000E3 RID: 227
		private string pbipCoreServiceRoutingHint;

		// Token: 0x040000E4 RID: 228
		private bool skipConnectionStringReconversion;

		// Token: 0x040000E5 RID: 229
		private string pbipVirtualServiceName;

		// Token: 0x040000E6 RID: 230
		private string contextualIdentityKey;

		// Token: 0x040000E7 RID: 231
		private string contextualIdentityType;

		// Token: 0x040000E8 RID: 232
		private string transientModelMode;

		// Token: 0x040000E9 RID: 233
		private string contextualIdentity;

		// Token: 0x040000EA RID: 234
		private Guid connectionActivityId;

		// Token: 0x040000EB RID: 235
		private string auxiliaryPermissionOwner;

		// Token: 0x040000EC RID: 236
		private string serviceToServiceToken;

		// Token: 0x040000ED RID: 237
		private Guid sourceCapacityObjectId;

		// Token: 0x040000EE RID: 238
		private string servicePrincipalProfileId;

		// Token: 0x040000EF RID: 239
		private string certificate;

		// Token: 0x040000F0 RID: 240
		private string authenticationScheme;

		// Token: 0x040000F1 RID: 241
		private string extAuthInfo;

		// Token: 0x040000F2 RID: 242
		private string identityProvider;

		// Token: 0x040000F3 RID: 243
		private string bypassAuthorization;

		// Token: 0x040000F4 RID: 244
		private string restrictCatalog;

		// Token: 0x040000F5 RID: 245
		private string accessMode;

		// Token: 0x040000F6 RID: 246
		private string restrictUser;

		// Token: 0x040000F7 RID: 247
		private string restrictRoles;

		// Token: 0x040000F8 RID: 248
		private IntendedUsage intendedUsage;

		// Token: 0x040000F9 RID: 249
		private AsAzureRedirection asAzureRedirection;

		// Token: 0x040000FA RID: 250
		private ConnectionAccessMode pbipAccessMode;

		// Token: 0x040000FB RID: 251
		private ListDictionary originalConnStringProps = new ListDictionary();

		// Token: 0x040000FC RID: 252
		private string dataSourceVersion;

		// Token: 0x040000FD RID: 253
		private string redirectorAddress;

		// Token: 0x040000FE RID: 254
		private string sandboxPath;

		// Token: 0x040000FF RID: 255
		private bool isUseAdalCacheSpecifiedOnConnectionString;

		// Token: 0x04000100 RID: 256
		private string paasInfrastructureServerName;

		// Token: 0x04000101 RID: 257
		private const string TimeoutPropertyName = "Timeout";

		// Token: 0x04000102 RID: 258
		private const string DataSourcePropertyName = "Data Source";

		// Token: 0x04000103 RID: 259
		private const string UserIDPropertyName = "User ID";

		// Token: 0x04000104 RID: 260
		private const string PasswordPropertyName = "Password";

		// Token: 0x04000105 RID: 261
		private const string ProtectionLevelPropertyName = "Protection Level";

		// Token: 0x04000106 RID: 262
		private const string ConnectTimeoutPropertyName = "Connect Timeout";

		// Token: 0x04000107 RID: 263
		private const string AutoSyncPeriodPropertyName = "Auto Synch Period";

		// Token: 0x04000108 RID: 264
		private const string ProviderPropertyName = "Provider";

		// Token: 0x04000109 RID: 265
		private const string DataSourceInfoPropertyName = "DataSourceInfo";

		// Token: 0x0400010A RID: 266
		private const string CatalogPropertyName = "Catalog";

		// Token: 0x0400010B RID: 267
		private const string IntegratedSecurityPropertyName = "Integrated Security";

		// Token: 0x0400010C RID: 268
		private const string ConnectToPropertyName = "ConnectTo";

		// Token: 0x0400010D RID: 269
		private const string SafetyOptionsPropertyName = "Safety Options";

		// Token: 0x0400010E RID: 270
		private const string ProtocolFormatPropertyName = "Protocol Format";

		// Token: 0x0400010F RID: 271
		private const string TransportCompressionPropertyName = "Transport Compression";

		// Token: 0x04000110 RID: 272
		private const string CompressionLevelPropertyName = "Compression Level";

		// Token: 0x04000111 RID: 273
		private const string EncryptionPasswordPropertyName = "Encryption Password";

		// Token: 0x04000112 RID: 274
		private const string ImpersonationLevelPropertyName = "Impersonation Level";

		// Token: 0x04000113 RID: 275
		private const string SspiPropertyName = "SSPI";

		// Token: 0x04000114 RID: 276
		private const string HttpHandlingPropertyName = "HttpChannelHandling";

		// Token: 0x04000115 RID: 277
		private const string UseExistingFilePropertyName = "UseExistingFile";

		// Token: 0x04000116 RID: 278
		private const string CharacterEncodingPropertyName = "Character Encoding";

		// Token: 0x04000117 RID: 279
		private const string UseEncryptionForDataPropertyName = "Use Encryption for Data";

		// Token: 0x04000118 RID: 280
		private const string PacketSizePropertyName = "Packet Size";

		// Token: 0x04000119 RID: 281
		private const string ExtendedPropertiesPropertyName = "Extended Properties";

		// Token: 0x0400011A RID: 282
		private const string LcidPropertyName = "LocaleIdentifier";

		// Token: 0x0400011B RID: 283
		private const string LocationPropertyName = "Location";

		// Token: 0x0400011C RID: 284
		private const string RestrictedClientPropertyName = "Restricted Client";

		// Token: 0x0400011D RID: 285
		private const string PersistSecurityInfoName = "Persist Security Info";

		// Token: 0x0400011E RID: 286
		private const string SessionIDPropertyName = "SessionID";

		// Token: 0x0400011F RID: 287
		private const string ClientProcessIDPropertyName = "ClientProcessID";

		// Token: 0x04000120 RID: 288
		private const string ApplicationNameXmlaPropertyName = "SspropInitAppName";

		// Token: 0x04000121 RID: 289
		private const string ClientCertificateThumbprintPropertyName = "Client Certificate Thumbprint";

		// Token: 0x04000122 RID: 290
		private const string UserIdentityPropertyName = "User Identity";

		// Token: 0x04000123 RID: 291
		private const string DataSourceVersionPropertyName = "Data Source Version";

		// Token: 0x04000124 RID: 292
		private const string CertificatePropertyName = "Certificate";

		// Token: 0x04000125 RID: 293
		private const string AuthenticationSchemePropertyName = "Authentication Scheme";

		// Token: 0x04000126 RID: 294
		private const string ExtAuthInfoPropertyName = "Ext Auth Info";

		// Token: 0x04000127 RID: 295
		private const string IdentityProviderPropertyName = "Identity Provider";

		// Token: 0x04000128 RID: 296
		private const string BypassAuthorizationPropertyName = "Bypass Authorization";

		// Token: 0x04000129 RID: 297
		private const string RestrictCatalogPropertyName = "Restrict Catalog";

		// Token: 0x0400012A RID: 298
		private const string AccessModePropertyName = "Access Mode";

		// Token: 0x0400012B RID: 299
		private const string UseAdalCachePropertyName = "UseADALCache";

		// Token: 0x0400012C RID: 300
		private const string TokenCacheModePropertyName = "Token Cache Mode";

		// Token: 0x0400012D RID: 301
		private const string ApplyAuxiliaryPermissionPropertyName = "ApplyAuxiliaryPermission";

		// Token: 0x0400012E RID: 302
		private const string AuxiliaryPermissionOwnerPropertyName = "AuxiliaryPermissionOwner";

		// Token: 0x0400012F RID: 303
		private const string ServiceToServiceTokenPropertyName = "ServiceToServiceToken";

		// Token: 0x04000130 RID: 304
		private const string WorkloadDirectConnectionPropertyName = "WorkloadDirectConnection";

		// Token: 0x04000131 RID: 305
		private const string PBIPVirtualServiceNamePropertyName = "PBIPVirtualServiceName";

		// Token: 0x04000132 RID: 306
		private const string IsInternalPaaSInfrastructurePropertyName = "IsInternalPaaSInfrastructure";

		// Token: 0x04000133 RID: 307
		private const string IsInternalASAzurePropertyName = "IsInternalASAzure";

		// Token: 0x04000134 RID: 308
		private const string IsInternalTestInfrastructurePropertyName = "IsInternalTestInfrastructure";

		// Token: 0x04000135 RID: 309
		private const string PbipCoreServiceRoutingHintPropertyName = "PbipCoreServiceRoutingHint";

		// Token: 0x04000136 RID: 310
		private const string ContextualIdentityKeyPropertyName = "ContextualIdentityKey";

		// Token: 0x04000137 RID: 311
		private const string ContextualIdentityTypePropertyName = "ContextualIdentityType";

		// Token: 0x04000138 RID: 312
		private const string TransientModelModePropertyName = "Transient Model";

		// Token: 0x04000139 RID: 313
		private const string RestrictUserPropertyName = "Restrict User";

		// Token: 0x0400013A RID: 314
		private const string RestrictRolesPropertyName = "Restrict Roles";

		// Token: 0x0400013B RID: 315
		private const string DedicatedAdminConnectionPropertyName = "DedicatedAdminConnection";

		// Token: 0x0400013C RID: 316
		private const string ContextualIdentityPropertyName = "ContextualIdentity";

		// Token: 0x0400013D RID: 317
		internal const string EffectiveUserNamePropertyName = "EffectiveUserName";

		// Token: 0x0400013E RID: 318
		private const string IntendedUsagePropertyName = "Intended Usage";

		// Token: 0x0400013F RID: 319
		private const string ConnectionActivityIdPropertyName = "ConnectionActivityId";

		// Token: 0x04000140 RID: 320
		private const string AsAzureRedirectionPropertyName = "AsAzureRedirection";

		// Token: 0x04000141 RID: 321
		private const string ScaleOutAutoSyncPropertyName = "Scale Out Auto Sync";

		// Token: 0x04000142 RID: 322
		private const string SourceCapacityObjectIdPropertyName = "SourceCapacityObjectId";

		// Token: 0x04000143 RID: 323
		private const string ServicePrincipalProfileIdPropertyName = "SPN Profile";

		// Token: 0x04000144 RID: 324
		private const string BypassBuildPermissionPropertyName = "BypassBuildPermission";

		// Token: 0x04000145 RID: 325
		private static string[] DeprecatedPropertyNames = new string[] { "External Tenant Id", "External User Id", "External Service Domain Name", "External Certificate Thumbprint" };

		// Token: 0x04000146 RID: 326
		private static string[] DatasourcePropertyNames = new string[] { "Data Source", "DataSource", "DSN" };

		// Token: 0x04000147 RID: 327
		private static string[] UserIDPropertyNames = new string[] { "User ID", "UID" };

		// Token: 0x04000148 RID: 328
		private static string[] PasswordPropertyNames = new string[] { "Password", "PWD" };

		// Token: 0x04000149 RID: 329
		private static string[] CatalogPropertyNames = new string[] { "Initial Catalog", "Catalog", "Database" };

		// Token: 0x0400014A RID: 330
		private static string[] PropertyNamesToIgnore = new string[] { "Provider", "ConnectTo", "DataSourceInfo" };

		// Token: 0x0400014B RID: 331
		private static readonly ConnectionInfo.XmlaPropsKnownByIXMLA XMLAPropertiesKnownByIXMLA = new ConnectionInfo.XmlaPropsKnownByIXMLA();

		// Token: 0x0400014C RID: 332
		private const string ProtectionLevelNone = "NONE";

		// Token: 0x0400014D RID: 333
		private const string ProtectionLevelConnect = "CONNECT";

		// Token: 0x0400014E RID: 334
		private const string ProtectionLevelIntegrity = "PKT INTEGRITY";

		// Token: 0x0400014F RID: 335
		private const string ProtectionLevelPrivacy = "PKT PRIVACY";

		// Token: 0x04000150 RID: 336
		private const string IntegratedSecuritySspi = "SSPI";

		// Token: 0x04000151 RID: 337
		private const string IntegratedSecurityBasic = "Basic";

		// Token: 0x04000152 RID: 338
		private const string IntegratedSecurityFederated = "Federated";

		// Token: 0x04000153 RID: 339
		private const string IntegratedSecurityClaimsToken = "ClaimsToken";

		// Token: 0x04000154 RID: 340
		internal const string IdentityProviderMsoID = "MsoID";

		// Token: 0x04000155 RID: 341
		private const string IdentityProviderPowerBIEmbed = "PowerBIEmbed";

		// Token: 0x04000156 RID: 342
		private const string IdentityProviderDataverse = "Dataverse";

		// Token: 0x04000157 RID: 343
		private const string UserIdentityDefault = "DEFAULT";

		// Token: 0x04000158 RID: 344
		private const string UserIdentityWindows = "Windows Identity";

		// Token: 0x04000159 RID: 345
		private const string UserIdentitySharePointPrincipal = "SharePoint Principal";

		// Token: 0x0400015A RID: 346
		private const string ConnectToDefault = "DEFAULT";

		// Token: 0x0400015B RID: 347
		private const string ConnectToShiloh = "8.0";

		// Token: 0x0400015C RID: 348
		private const string ConnectToYukon = "9.0";

		// Token: 0x0400015D RID: 349
		private const string ConnectToKatmai = "10.0";

		// Token: 0x0400015E RID: 350
		private const string ConnectToDenali = "11.0";

		// Token: 0x0400015F RID: 351
		private const string ConnectToSQL14 = "12.0";

		// Token: 0x04000160 RID: 352
		private const string ConnectToSQL15 = "13.0";

		// Token: 0x04000161 RID: 353
		private const string ConnectToSQL17 = "14.0";

		// Token: 0x04000162 RID: 354
		private const string ConnectToSSAS18 = "15.0";

		// Token: 0x04000163 RID: 355
		private const string ConnectToSSAS21 = "16.0";

		// Token: 0x04000164 RID: 356
		private const string AutoSyncPeriodNull = "NULL";

		// Token: 0x04000165 RID: 357
		private const string ProtocolFormatDefault = "Default";

		// Token: 0x04000166 RID: 358
		private const string ProtocolFormatXml = "XML";

		// Token: 0x04000167 RID: 359
		private const string ProtocolFormatBinary = "Binary";

		// Token: 0x04000168 RID: 360
		private const string TransportCompressionDefault = "Default";

		// Token: 0x04000169 RID: 361
		private const string TransportCompressionNone = "None";

		// Token: 0x0400016A RID: 362
		private const string TransportCompressionCompressed = "Compressed";

		// Token: 0x0400016B RID: 363
		private const string TransportCompressionGzip = "Gzip";

		// Token: 0x0400016C RID: 364
		private const string ImpersonationLevelAnonymous = "Anonymous";

		// Token: 0x0400016D RID: 365
		private const string ImpersonationLevelIdentify = "Identify";

		// Token: 0x0400016E RID: 366
		private const string ImpersonationLevelImpersonate = "Impersonate";

		// Token: 0x0400016F RID: 367
		private const string ImpersonationLevelDelegate = "Delegate";

		// Token: 0x04000170 RID: 368
		private const string HttpHandlingDefault = "Default";

		// Token: 0x04000171 RID: 369
		private const string HttpHandlingWebRequestBased = "WebRequestBased";

		// Token: 0x04000172 RID: 370
		private const string HttpHandlingPreferHttpClient = "PreferHttpClient";

		// Token: 0x04000173 RID: 371
		private const string CharacterEncodingDefault = "Default";

		// Token: 0x04000174 RID: 372
		internal const string IntendedUsageDefault = "default";

		// Token: 0x04000175 RID: 373
		internal const string IntendedUsageProcessing = "processing";

		// Token: 0x04000176 RID: 374
		internal const string IntendedUsageInteractiveProcessing = "interactiveprocessing";

		// Token: 0x04000177 RID: 375
		internal const string IntendedUsageBackgroundQuery = "backgroundquery";

		// Token: 0x04000178 RID: 376
		internal const string MwcRoutingScenarioForProcessing = "Processing";

		// Token: 0x04000179 RID: 377
		internal const string TransientModelModeEnabled = "Enabled";

		// Token: 0x0400017A RID: 378
		internal const string TransientModelModeDisabled = "Disabled";

		// Token: 0x0400017B RID: 379
		private const string AccessModeReadOnly = "readonly";

		// Token: 0x0400017C RID: 380
		private const string AccessModeReadWrite = "readwrite";

		// Token: 0x0400017D RID: 381
		private const string ContextualIdentityTypeBearerToken = "bearertoken";

		// Token: 0x0400017E RID: 382
		private const string ContextualIdentityTypeEmbedToken = "embedtoken";

		// Token: 0x0200016A RID: 362
		private class XmlaPropsKnownByIXMLA
		{
			// Token: 0x06001138 RID: 4408 RVA: 0x0003C694 File Offset: 0x0003A894
			internal XmlaPropsKnownByIXMLA()
			{
				this.hash = new Dictionary<string, int>(ConnectionInfo.XmlaPropsKnownByIXMLA.knownWritableProperties.Length, StringComparer.OrdinalIgnoreCase);
				foreach (string text in ConnectionInfo.XmlaPropsKnownByIXMLA.knownWritableProperties)
				{
					this.hash[text] = 1;
				}
			}

			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x06001139 RID: 4409 RVA: 0x0003C6E3 File Offset: 0x0003A8E3
			internal Dictionary<string, int> Known
			{
				get
				{
					return this.hash;
				}
			}

			// Token: 0x04000BB6 RID: 2998
			private Dictionary<string, int> hash;

			// Token: 0x04000BB7 RID: 2999
			private static string[] knownWritableProperties = new string[]
			{
				"Content", "Format", "AxisFormat", "BeginRange", "EndRange", "Cube", "DataSourceInfo", "Timeout", "UserName", "Password",
				"LocaleIdentifier", "CachePolicy", "CompareCaseSensitiveStringFlags", "CompareCaseNotSensitiveStringFlags", "ReadOnlySession", "SecuredCellValue", "MDXCompatibility", "MDXUniqueNameStyle", "DbpropMsmdMDXCompatibility", "DbpropMsmdMDXUniqueNameStyle",
				"CacheRatio", "NonEmptyThreshold", "Roles", "Catalog"
			};
		}

		// Token: 0x0200016B RID: 363
		[Flags]
		private enum ConnectionOptions
		{
			// Token: 0x04000BB9 RID: 3001
			None = 0,
			// Token: 0x04000BBA RID: 3002
			RestrictedClient = 1,
			// Token: 0x04000BBB RID: 3003
			UseExistingFile = 2,
			// Token: 0x04000BBC RID: 3004
			UseEncryptionForData = 4,
			// Token: 0x04000BBD RID: 3005
			PersistSecurityInfo = 8,
			// Token: 0x04000BBE RID: 3006
			DedicatedAdminConnection = 16,
			// Token: 0x04000BBF RID: 3007
			TokenCacheInDefaultMode = 64,
			// Token: 0x04000BC0 RID: 3008
			TokenCacheExplicitMode = 128,
			// Token: 0x04000BC1 RID: 3009
			InternalPaaSInfrastructure = 256,
			// Token: 0x04000BC2 RID: 3010
			InternalTestInfrastructure = 512,
			// Token: 0x04000BC3 RID: 3011
			WorkloadDirectConnection = 1024,
			// Token: 0x04000BC4 RID: 3012
			ScaleOutAutoSync = 2048,
			// Token: 0x04000BC5 RID: 3013
			ProtectionLevelWasSet = 4096,
			// Token: 0x04000BC6 RID: 3014
			UseEncryptionForDataWasSet = 8192,
			// Token: 0x04000BC7 RID: 3015
			PasswordPresent = 16384,
			// Token: 0x04000BC8 RID: 3016
			AllowAutoRedirect = 32768,
			// Token: 0x04000BC9 RID: 3017
			UseADTranslation = 65536,
			// Token: 0x04000BCA RID: 3018
			DisableSingleSignOn = 131072,
			// Token: 0x04000BCB RID: 3019
			AccessTokenRequired = 262144,
			// Token: 0x04000BCC RID: 3020
			UsedForSqlBrowser = 1048576,
			// Token: 0x04000BCD RID: 3021
			RestrictConnectionString = 2097152,
			// Token: 0x04000BCE RID: 3022
			RevertToProcessAccountForConnection = 4194304,
			// Token: 0x04000BCF RID: 3023
			AllowDelegation = 8388608,
			// Token: 0x04000BD0 RID: 3024
			ResolvedLinkFile = 16777216,
			// Token: 0x04000BD1 RID: 3025
			ResolvedAsAzureRedirection = 33554432,
			// Token: 0x04000BD2 RID: 3026
			ApplyAuxiliaryPermission = 67108864,
			// Token: 0x04000BD3 RID: 3027
			BypassBuildPermission = 134217728,
			// Token: 0x04000BD4 RID: 3028
			CloneForInstanceLookupMask = 12311,
			// Token: 0x04000BD5 RID: 3029
			Default = 32832
		}

		// Token: 0x0200016C RID: 364
		private struct ConnectionOptionsWrapper
		{
			// Token: 0x0600113B RID: 4411 RVA: 0x0003C7D4 File Offset: 0x0003A9D4
			public ConnectionOptionsWrapper(ConnectionInfo.ConnectionOptions value)
			{
				this.value = value;
			}

			// Token: 0x17000630 RID: 1584
			// (get) Token: 0x0600113C RID: 4412 RVA: 0x0003C7DD File Offset: 0x0003A9DD
			public ConnectionInfo.ConnectionOptions Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x0600113D RID: 4413 RVA: 0x0003C7E5 File Offset: 0x0003A9E5
			public bool IsEnabled(ConnectionInfo.ConnectionOptions options)
			{
				return (this.value & options) == options;
			}

			// Token: 0x0600113E RID: 4414 RVA: 0x0003C7F2 File Offset: 0x0003A9F2
			public bool IsDisabled(ConnectionInfo.ConnectionOptions options)
			{
				return (this.value & options) == ConnectionInfo.ConnectionOptions.None;
			}

			// Token: 0x0600113F RID: 4415 RVA: 0x0003C7FF File Offset: 0x0003A9FF
			public bool IsPartialyEnabled(ConnectionInfo.ConnectionOptions options)
			{
				return (this.value & options) > ConnectionInfo.ConnectionOptions.None;
			}

			// Token: 0x06001140 RID: 4416 RVA: 0x0003C80C File Offset: 0x0003AA0C
			public bool IsPartialyDisabled(ConnectionInfo.ConnectionOptions options)
			{
				return (this.value & options) != options;
			}

			// Token: 0x06001141 RID: 4417 RVA: 0x0003C81C File Offset: 0x0003AA1C
			public ConnectionInfo.ConnectionOptions Enable(ConnectionInfo.ConnectionOptions options)
			{
				return this.Update(options, true);
			}

			// Token: 0x06001142 RID: 4418 RVA: 0x0003C826 File Offset: 0x0003AA26
			public ConnectionInfo.ConnectionOptions Disable(ConnectionInfo.ConnectionOptions options)
			{
				return this.Update(options, false);
			}

			// Token: 0x06001143 RID: 4419 RVA: 0x0003C830 File Offset: 0x0003AA30
			public ConnectionInfo.ConnectionOptions Update(ConnectionInfo.ConnectionOptions options, bool isEnabled)
			{
				if (isEnabled)
				{
					this.value |= options;
				}
				else
				{
					this.value &= ~options;
				}
				return this.value;
			}

			// Token: 0x06001144 RID: 4420 RVA: 0x0003C85A File Offset: 0x0003AA5A
			public void Reset()
			{
				this.value = ConnectionInfo.ConnectionOptions.Default;
			}

			// Token: 0x06001145 RID: 4421 RVA: 0x0003C867 File Offset: 0x0003AA67
			public ConnectionInfo.ConnectionOptionsWrapper CloneForInstanceLookup()
			{
				return new ConnectionInfo.ConnectionOptionsWrapper((this.value & ConnectionInfo.ConnectionOptions.CloneForInstanceLookupMask) | ConnectionInfo.ConnectionOptions.UsedForSqlBrowser);
			}

			// Token: 0x06001146 RID: 4422 RVA: 0x0003C880 File Offset: 0x0003AA80
			public override bool Equals(object obj)
			{
				if (obj is ConnectionInfo.ConnectionOptionsWrapper)
				{
					ConnectionInfo.ConnectionOptionsWrapper connectionOptionsWrapper = (ConnectionInfo.ConnectionOptionsWrapper)obj;
					return this.value == connectionOptionsWrapper.value;
				}
				if (obj is ConnectionInfo.ConnectionOptions)
				{
					ConnectionInfo.ConnectionOptions connectionOptions = (ConnectionInfo.ConnectionOptions)obj;
					return this.value == connectionOptions;
				}
				return false;
			}

			// Token: 0x06001147 RID: 4423 RVA: 0x0003C8C5 File Offset: 0x0003AAC5
			public override int GetHashCode()
			{
				return this.value.GetHashCode();
			}

			// Token: 0x06001148 RID: 4424 RVA: 0x0003C8D8 File Offset: 0x0003AAD8
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < 32; i++)
				{
					int num = 1 << i;
					if ((this.value & (ConnectionInfo.ConnectionOptions)num) != ConnectionInfo.ConnectionOptions.None)
					{
						ConnectionInfo.ConnectionOptions connectionOptions = (ConnectionInfo.ConnectionOptions)num;
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(" | ");
						}
						stringBuilder.Append(connectionOptions.ToString());
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.AppendFormat("[{0}]", (int)this.value);
				return stringBuilder.ToString();
			}

			// Token: 0x06001149 RID: 4425 RVA: 0x0003C963 File Offset: 0x0003AB63
			public static implicit operator ConnectionInfo.ConnectionOptions(ConnectionInfo.ConnectionOptionsWrapper wrapper)
			{
				return wrapper.value;
			}

			// Token: 0x0600114A RID: 4426 RVA: 0x0003C96B File Offset: 0x0003AB6B
			public static explicit operator ConnectionInfo.ConnectionOptionsWrapper(ConnectionInfo.ConnectionOptions options)
			{
				return new ConnectionInfo.ConnectionOptionsWrapper(options);
			}

			// Token: 0x04000BD6 RID: 3030
			private ConnectionInfo.ConnectionOptions value;
		}

		// Token: 0x0200016D RID: 365
		private sealed class RefreshableExternalAuthenticationHandle : AuthenticationHandle
		{
			// Token: 0x0600114B RID: 4427 RVA: 0x0003C974 File Offset: 0x0003AB74
			public RefreshableExternalAuthenticationHandle(IConnectivityOwner owner, string authenticationScheme)
				: base(AuthenticationEndpoint.Unknown, null, null)
			{
				this.authenticationScheme = authenticationScheme;
				this.owner = owner;
				this.refreshByTime = Microsoft.AnalysisServices.AdomdClient.Authentication.AuthenticationManager.CalculateAccessTokenRefreshBy(owner.AccessToken.ExpirationTime);
			}

			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x0600114C RID: 4428 RVA: 0x0003C9B1 File Offset: 0x0003ABB1
			public override string Principal
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x0600114D RID: 4429 RVA: 0x0003C9B8 File Offset: 0x0003ABB8
			public override string AuthenticationScheme
			{
				get
				{
					return this.authenticationScheme;
				}
			}

			// Token: 0x0600114E RID: 4430 RVA: 0x0003C9C0 File Offset: 0x0003ABC0
			public override string GetAccessToken()
			{
				if (this.refreshByTime < DateTimeOffset.Now)
				{
					this.owner.RefreshAccessToken();
					this.refreshByTime = Microsoft.AnalysisServices.AdomdClient.Authentication.AuthenticationManager.CalculateAccessTokenRefreshBy(this.owner.AccessToken.ExpirationTime);
				}
				return this.owner.AccessToken.Token;
			}

			// Token: 0x0600114F RID: 4431 RVA: 0x0003CA1B File Offset: 0x0003AC1B
			public override long GetRefreshByTimeAsFileTime()
			{
				return this.refreshByTime.ToFileTime();
			}

			// Token: 0x04000BD7 RID: 3031
			private readonly string authenticationScheme;

			// Token: 0x04000BD8 RID: 3032
			private readonly IConnectivityOwner owner;

			// Token: 0x04000BD9 RID: 3033
			private DateTimeOffset refreshByTime;
		}
	}
}
