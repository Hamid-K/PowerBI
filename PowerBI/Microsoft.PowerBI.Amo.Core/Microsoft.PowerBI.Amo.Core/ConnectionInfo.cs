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
using Microsoft.AnalysisServices.Authentication;
using Microsoft.AnalysisServices.Extensions;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Redirection;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000032 RID: 50
	public sealed class ConnectionInfo
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00005DC8 File Offset: 0x00003FC8
		static ConnectionInfo()
		{
			ClientHostingManager.UpdateProcessWithUserInterfaceStatus(new Func<bool>(WindowsRuntimeHelper.IsProcessWithUserInterface));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005EB7 File Offset: 0x000040B7
		public ConnectionInfo(string connectionString)
		{
			this.SetConnectionString(connectionString);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005EEC File Offset: 0x000040EC
		internal ConnectionInfo()
		{
			this.ResetFields();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005F20 File Offset: 0x00004120
		internal ConnectionInfo(ConnectionInfo connectionInfo)
		{
			ConnectionInfo.CopyConnectionInfo(connectionInfo, this);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005F55 File Offset: 0x00004155
		public ConnectionType ConnectionType
		{
			get
			{
				return this.connectionType;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005F5D File Offset: 0x0000415D
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005F65 File Offset: 0x00004165
		internal void SetServer(string value)
		{
			this.server = ConnectionInfo.Trim(value);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005F73 File Offset: 0x00004173
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005F7B File Offset: 0x0000417B
		internal void SetInstanceName(string value)
		{
			this.instanceName = ConnectionInfo.Trim(value);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005F89 File Offset: 0x00004189
		public string Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005F91 File Offset: 0x00004191
		internal void SetPort(string value)
		{
			this.port = ConnectionInfo.Trim(value);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005F9F File Offset: 0x0000419F
		public string Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005FA7 File Offset: 0x000041A7
		public string Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005FAF File Offset: 0x000041AF
		internal void SetCatalog(string value)
		{
			this.catalog = value;
			this.InsertKeyValueIntoHash("Catalog", value);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005FC4 File Offset: 0x000041C4
		public string UserID
		{
			get
			{
				return this.userID;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005FCC File Offset: 0x000041CC
		public string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005FD4 File Offset: 0x000041D4
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00005FDC File Offset: 0x000041DC
		public ProtocolFormat ProtocolFormat
		{
			get
			{
				return this.protocolFormat;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005FE4 File Offset: 0x000041E4
		public TransportCompression TransportCompression
		{
			get
			{
				return this.transportCompression;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005FEC File Offset: 0x000041EC
		public int CompressionLevel
		{
			get
			{
				return this.compressionLevel;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005FF4 File Offset: 0x000041F4
		public IntegratedSecurity IntegratedSecurity
		{
			get
			{
				return this.integratedSecurity;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00005FFC File Offset: 0x000041FC
		public string EncryptionPassword
		{
			get
			{
				return this.encryptionPassword;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00006004 File Offset: 0x00004204
		public ImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.impersonationLevel;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000600C File Offset: 0x0000420C
		public string Sspi
		{
			get
			{
				return this.sspi;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00006014 File Offset: 0x00004214
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000601C File Offset: 0x0000421C
		public HttpChannelHandling HttpHandling { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00006025 File Offset: 0x00004225
		public bool UseExistingFile
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.UseExistingFile);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00006033 File Offset: 0x00004233
		public Encoding CharacterEncoding
		{
			get
			{
				return this.characterEncoding;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000603B File Offset: 0x0000423B
		public int PacketSize
		{
			get
			{
				return this.packetSize;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00006043 File Offset: 0x00004243
		public string ClientCertificateThumbprint
		{
			get
			{
				return this.clientCertificateThumbprint;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000604B File Offset: 0x0000424B
		public string Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00006053 File Offset: 0x00004253
		public string AuthenticationScheme
		{
			get
			{
				return this.authenticationScheme;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000605B File Offset: 0x0000425B
		public string ExtAuthInfo
		{
			get
			{
				return this.extAuthInfo;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006063 File Offset: 0x00004263
		public string IdentityProvider
		{
			get
			{
				return this.identityProvider;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000606B File Offset: 0x0000426B
		public string BypassAuthorization
		{
			get
			{
				return this.bypassAuthorization;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006073 File Offset: 0x00004273
		public string RestrictCatalog
		{
			get
			{
				return this.restrictCatalog;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000607B File Offset: 0x0000427B
		public string AccessMode
		{
			get
			{
				return this.accessMode;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006083 File Offset: 0x00004283
		public string RestrictUser
		{
			get
			{
				return this.restrictUser;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000608B File Offset: 0x0000428B
		public string RestrictRoles
		{
			get
			{
				return this.restrictRoles;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006093 File Offset: 0x00004293
		public AsAzureRedirection AsAzureRedirection
		{
			get
			{
				return this.asAzureRedirection;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000609B File Offset: 0x0000429B
		public bool DedicatedAdminConnection
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.DedicatedAdminConnection);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000060AA File Offset: 0x000042AA
		public string ContextualIdentity
		{
			get
			{
				return this.contextualIdentity;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000060B2 File Offset: 0x000042B2
		public Guid ConnectionActivityId
		{
			get
			{
				return this.connectionActivityId;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000060BA File Offset: 0x000042BA
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000060C2 File Offset: 0x000042C2
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000060CA File Offset: 0x000042CA
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000060D3 File Offset: 0x000042D3
		public ListDictionary ExtendedProperties
		{
			get
			{
				return this.extendedProperties;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000060DB File Offset: 0x000042DB
		[Obsolete("Deprecated!")]
		public bool AllowPrompt
		{
			get
			{
				return this.allowPrompt;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000060E3 File Offset: 0x000042E3
		internal AsInstanceType AsInstanceType
		{
			get
			{
				return this.asInstanceType;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000060EB File Offset: 0x000042EB
		internal bool IsAsAzure
		{
			get
			{
				return this.asInstanceType == AsInstanceType.AsAzure;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000060F6 File Offset: 0x000042F6
		internal bool IsPbiPremiumInternal
		{
			get
			{
				return this.asInstanceType == AsInstanceType.PbiPremiumInternal;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006101 File Offset: 0x00004301
		internal bool IsPbiPremiumXmlaEp
		{
			get
			{
				return this.asInstanceType == AsInstanceType.PbiPremiumXmlaEp;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000610C File Offset: 0x0000430C
		internal bool IsPbiPremiumXmlaEpWithPowerBIEmbedToken
		{
			get
			{
				return this.asInstanceType == AsInstanceType.PbiPremiumXmlaEp && string.Compare(this.identityProvider, "PowerBIEmbed", StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000612D File Offset: 0x0000432D
		internal bool IsPbiPremiumXmlaEpWithDataverseEmbedToken
		{
			get
			{
				return this.asInstanceType == AsInstanceType.PbiPremiumXmlaEp && string.Compare(this.identityProvider, "Dataverse", StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000614E File Offset: 0x0000434E
		internal bool IsPbiDataset
		{
			get
			{
				return this.asInstanceType == AsInstanceType.PbiDataset;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006159 File Offset: 0x00004359
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006161 File Offset: 0x00004361
		internal bool IsLinkReference { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000616C File Offset: 0x0000436C
		internal bool IsPaaSInfrastructure
		{
			get
			{
				AsInstanceType asInstanceType = this.asInstanceType;
				return asInstanceType - AsInstanceType.AsAzure <= 2;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000618C File Offset: 0x0000438C
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
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000061F6 File Offset: 0x000043F6
		internal AuthenticationHandle AuthHandle
		{
			get
			{
				return this.authHandle;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000061FE File Offset: 0x000043FE
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00006206 File Offset: 0x00004406
		internal string RoutingToken { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000620F File Offset: 0x0000440F
		internal string SessionID
		{
			get
			{
				return this.sessionID;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006217 File Offset: 0x00004417
		internal string ApplicationName
		{
			get
			{
				return (string)this.ExtendedProperties["SspropInitAppName"];
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006230 File Offset: 0x00004430
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000625C File Offset: 0x0000445C
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006285 File Offset: 0x00004485
		internal string ServiceToServiceToken
		{
			get
			{
				return this.serviceToServiceToken;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000628D File Offset: 0x0000448D
		internal Guid SourceCapacityObjectId
		{
			get
			{
				return this.sourceCapacityObjectId;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006295 File Offset: 0x00004495
		internal string ServicePrincipalProfileId
		{
			get
			{
				return this.servicePrincipalProfileId;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000629D File Offset: 0x0000449D
		internal IntendedUsage IntendedUsage
		{
			get
			{
				return this.intendedUsage;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000062A5 File Offset: 0x000044A5
		internal string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000062AD File Offset: 0x000044AD
		internal string LinkReferenceRawDataSource
		{
			get
			{
				return this.linkReferenceRawDataSource;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000062B5 File Offset: 0x000044B5
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000062BD File Offset: 0x000044BD
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000062C6 File Offset: 0x000044C6
		internal bool AllowAutoRedirect
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.AllowAutoRedirect);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000062D8 File Offset: 0x000044D8
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000062E0 File Offset: 0x000044E0
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000062E9 File Offset: 0x000044E9
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000062F1 File Offset: 0x000044F1
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000062FA File Offset: 0x000044FA
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006302 File Offset: 0x00004502
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000630B File Offset: 0x0000450B
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00006313 File Offset: 0x00004513
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000631C File Offset: 0x0000451C
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00006324 File Offset: 0x00004524
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000632D File Offset: 0x0000452D
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00006335 File Offset: 0x00004535
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000633E File Offset: 0x0000453E
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00006348 File Offset: 0x00004548
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006391 File Offset: 0x00004591
		internal string TransientModelMode
		{
			get
			{
				return this.transientModelMode;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006399 File Offset: 0x00004599
		internal bool UseAadTokenInPublicXmlaEP
		{
			get
			{
				return PbiPremiumAuthenticationHandle.UseAadTokenInPublicXmlaEP;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000063A0 File Offset: 0x000045A0
		internal UserIdentityType UserIdentity
		{
			get
			{
				return this.userIdentity;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000063A8 File Offset: 0x000045A8
		internal bool IsEmbedded
		{
			get
			{
				return string.Compare(this.server, "$Embedded$", StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000063BE File Offset: 0x000045BE
		internal bool IsSspiAnonymous
		{
			get
			{
				return string.Compare(this.sspi, "Anonymous", StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000063D4 File Offset: 0x000045D4
		internal bool IsServerLocal
		{
			get
			{
				return this.server == ".";
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000063E6 File Offset: 0x000045E6
		internal bool IsForRedirector
		{
			get
			{
				return this.redirectorAddress != null;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000063F1 File Offset: 0x000045F1
		internal bool IsForSqlBrowser
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.UsedForSqlBrowser);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006404 File Offset: 0x00004604
		internal bool IsBinarySupported
		{
			get
			{
				AsInstanceType asInstanceType = this.asInstanceType;
				return asInstanceType - AsInstanceType.PbiPremiumInternal <= 1;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006421 File Offset: 0x00004621
		internal bool RestrictedClient
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.RestrictedClient);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000642F File Offset: 0x0000462F
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00006441 File Offset: 0x00004641
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006455 File Offset: 0x00004655
		internal bool IsScaleOutAutoSyncEnabled
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ScaleOutAutoSync);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006467 File Offset: 0x00004667
		internal bool AllowDelegation
		{
			get
			{
				return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.AllowDelegation);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006479 File Offset: 0x00004679
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00006481 File Offset: 0x00004681
		internal bool UseEU { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000648A File Offset: 0x0000468A
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00006492 File Offset: 0x00004692
		internal bool IsLightweightConnection { get; set; }

		// Token: 0x0600013D RID: 317 RVA: 0x0000649C File Offset: 0x0000469C
		internal static void ValidateSpecifiedEffectiveUserName(string effectiveUserName)
		{
			SecurityIdentifier user = WindowsIdentity.GetCurrent().User;
			SecurityIdentifier securityIdentifier = (SecurityIdentifier)new NTAccount(effectiveUserName).Translate(typeof(SecurityIdentifier));
			if (user.CompareTo(securityIdentifier) != 0)
			{
				throw new ConnectionException(XmlaSR.ConnectionString_LinkFileDupEffectiveUsername);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000064E1 File Offset: 0x000046E1
		internal static bool IsBism(string address)
		{
			return (ConnectivityHelper.IsHttpUri(address) || ConnectivityHelper.IsHttpsUri(address)) && new Uri(address).AbsolutePath.EndsWith(".bism", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006510 File Offset: 0x00004710
		internal void HandleAsAzureRedirection(IConnectivityOwner owner)
		{
			if (this.AsAzureRedirection == AsAzureRedirection.Disabled)
			{
				return;
			}
			Microsoft.AnalysisServices.Authentication.AuthenticationManager.EnableStrongSecurityProtocols();
			Uri effectiveDataSource = this.GetEffectiveDataSource();
			RedirectionWorkspaceInfo redirectionWorkspaceInfo;
			ConnectionAccessMode connectionAccessMode;
			if (!this.ShouldRedirectAsAzureToPowerBIWorkspace(effectiveDataSource, out redirectionWorkspaceInfo, out connectionAccessMode))
			{
				return;
			}
			if (((string.IsNullOrEmpty(this.UserID) && !string.IsNullOrEmpty(this.Password)) || (owner != null && owner.AccessToken.IsValid)) && string.Compare(this.identityProvider, "PowerBIEmbed", StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new ConnectionException(AuthenticationSR.Exception_RedirectionTokenAsPasswordIsNotSupported);
			}
			if (this.IsAccessTokenMissing(owner))
			{
				throw new ConnectionException(RuntimeSR.NeedRefreshableToken);
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

		// Token: 0x06000140 RID: 320 RVA: 0x00006638 File Offset: 0x00004838
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

		// Token: 0x06000141 RID: 321 RVA: 0x00006834 File Offset: 0x00004A34
		internal bool CanBuildExternalAuthenticationHandle(IConnectivityOwner owner)
		{
			return (owner != null && owner.AccessToken.IsValid) || (this.UserID == null && this.Password != null);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006869 File Offset: 0x00004A69
		internal void BuildExternalAuthenticationHandle(IConnectivityOwner owner)
		{
			if (!this.TryBuildExternalAuthenticationHandle(owner, out this.authHandle))
			{
				throw new ConnectionException(RuntimeSR.NeedRefreshableToken);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006885 File Offset: 0x00004A85
		internal void RestrictConnectionString()
		{
			this.options.Enable(ConnectionInfo.ConnectionOptions.RestrictConnectionString);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006898 File Offset: 0x00004A98
		internal bool IsSchannelSspi()
		{
			return string.Compare(this.sspi, "Schannel", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.sspi, "Microsoft Unified Security Protocol Provider", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000068C3 File Offset: 0x00004AC3
		internal bool IsLinkFile()
		{
			return (ConnectivityHelper.IsHttpUri(this.server) || ConnectivityHelper.IsHttpsUri(this.server)) && new Uri(this.server).AbsolutePath.EndsWith(".bism", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006900 File Offset: 0x00004B00
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
					throw new ConnectionException(XmlaSR.InternalError);
				}
			}
			else
			{
				this.GetRemoteLinkFileMetadata(this.server, out text, out text2, out flag);
				flag2 = false;
			}
			if (flag2)
			{
				throw new ConnectionException(XmlaSR.ConnectionString_LinkFileParseError(4096), null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new ConnectionException(XmlaSR.ConnectionString_LinkFileMissingServer, null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
			}
			if (ConnectivityHelper.HasUriProtocolScheme(text, "link"))
			{
				throw new ConnectionException(XmlaSR.ConnectionString_LinkFileInvalidServer, null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
			}
			if (ConnectivityHelper.IsHttpConnection(text) && new Uri(text).AbsolutePath.EndsWith(".bism", StringComparison.OrdinalIgnoreCase))
			{
				throw new ConnectionException(XmlaSR.ConnectionString_LinkFileInvalidServer, null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
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

		// Token: 0x06000147 RID: 327 RVA: 0x00006A10 File Offset: 0x00004C10
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

		// Token: 0x06000148 RID: 328 RVA: 0x00006A94 File Offset: 0x00004C94
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

		// Token: 0x06000149 RID: 329 RVA: 0x00006C38 File Offset: 0x00004E38
		internal ConnectionInfo CloneForTraceChannel()
		{
			ConnectionInfo connectionInfo = new ConnectionInfo();
			ConnectionInfo.CopyConnectionInfo(this, connectionInfo);
			connectionInfo.timeout = 0;
			connectionInfo.HttpHandling = HttpChannelHandling.WebRequestBased;
			connectionInfo.IsLightweightConnection = true;
			return connectionInfo;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006C68 File Offset: 0x00004E68
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

		// Token: 0x0600014B RID: 331 RVA: 0x00006CDC File Offset: 0x00004EDC
		internal string GetRedirectorUrlForRedirect(string databaseId, bool specificVersion)
		{
			if (specificVersion)
			{
				return this.redirectorAddress + "/?DatabaseId=" + databaseId + "&SpecificVersion=true";
			}
			return this.redirectorAddress + "/?DatabaseId=" + databaseId;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006D0C File Offset: 0x00004F0C
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

		// Token: 0x0600014D RID: 333 RVA: 0x00006D80 File Offset: 0x00004F80
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

		// Token: 0x0600014E RID: 334 RVA: 0x00006DB4 File Offset: 0x00004FB4
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

		// Token: 0x0600014F RID: 335 RVA: 0x00006E05 File Offset: 0x00005005
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

		// Token: 0x06000150 RID: 336 RVA: 0x00006E30 File Offset: 0x00005030
		private bool IsAccessTokenMissing(IConnectivityOwner owner)
		{
			return this.options.IsEnabled(ConnectionInfo.ConnectionOptions.AccessTokenRequired) && !owner.AccessToken.IsValid;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006E64 File Offset: 0x00005064
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
			destinationInfo.allowPrompt = sourceInfo.allowPrompt;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000719C File Offset: 0x0000539C
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

		// Token: 0x06000153 RID: 339 RVA: 0x00007288 File Offset: 0x00005488
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

		// Token: 0x06000154 RID: 340 RVA: 0x000072BC File Offset: 0x000054BC
		private static bool IsExtendedProperties(string key)
		{
			return string.Compare(key, "Extended Properties", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000072D0 File Offset: 0x000054D0
		private static void RemoveDataSourceAccessMode(ref string dataSource, string accessMode, int index)
		{
			if (index + accessMode.Length < dataSource.Length && dataSource[index + accessMode.Length] == '&')
			{
				dataSource = dataSource.Remove(index, accessMode.Length + 1);
				return;
			}
			dataSource = dataSource.Remove(index - 1);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007320 File Offset: 0x00005520
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

		// Token: 0x06000157 RID: 343 RVA: 0x00007361 File Offset: 0x00005561
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

		// Token: 0x06000158 RID: 344 RVA: 0x0000737B File Offset: 0x0000557B
		private static bool IsLocalCubeFile(string path)
		{
			return -1 == path.IndexOfAny(Path.GetInvalidPathChars()) && string.Compare(".cub", Path.GetExtension(path), StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000073A1 File Offset: 0x000055A1
		private static bool IsEmbeddedPath(string path)
		{
			return string.Compare(path, "$Embedded$", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000073B4 File Offset: 0x000055B4
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

		// Token: 0x0600015B RID: 347 RVA: 0x00007400 File Offset: 0x00005600
		private static string GetRestrictedConnectionString(string cs)
		{
			StringBuilder stringBuilder = new StringBuilder(cs.Length);
			foreach (KeyValuePair<string, string> keyValuePair in ConnectivityHelper.ParseKeyValueSet(cs))
			{
				ConnectionInfo.AppendRestrictedConnectionStringProperty(stringBuilder, keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000746C File Offset: 0x0000566C
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00007500 File Offset: 0x00005700
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007540 File Offset: 0x00005740
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

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00007580 File Offset: 0x00005780
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

		// Token: 0x06000160 RID: 352 RVA: 0x000075AC File Offset: 0x000057AC
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

		// Token: 0x06000161 RID: 353 RVA: 0x00007614 File Offset: 0x00005814
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
			this.allowPrompt = false;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000077E8 File Offset: 0x000059E8
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
				}
				if (!this.extendedProperties.Contains("ClientProcessID") && this.extendedProperties.Contains("SspropInitAppName"))
				{
					int id = Process.GetCurrentProcess().Id;
					this.extendedProperties["ClientProcessID"] = FormattersHelpers.ConvertToXml(id);
				}
			}
			catch
			{
				ConnectionInfo.CopyConnectionInfo(connectionInfo, this);
				throw;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007CCC File Offset: 0x00005ECC
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

		// Token: 0x06000164 RID: 356 RVA: 0x00007EA4 File Offset: 0x000060A4
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
					goto IL_0164;
				case IntegratedSecurity.ClaimsToken:
					goto IL_0164;
				case IntegratedSecurity.Unspecified:
					this.integratedSecurity = IntegratedSecurity.ClaimsToken;
					goto IL_0164;
				}
				if (this.IsPbiPremiumInternal)
				{
					throw new ArgumentException(XmlaSR.Authentication_PbiDedicated_OnlyClaimsTokenSupported);
				}
				throw new ArgumentException(XmlaSR.Authentication_AsAzure_OnlySspiOrClaimsTokenSupported);
				IL_0164:
				if (this.IsAsAzure || this.IsPbiPremiumInternal)
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
				if (!ClientHostingManager.IsProcessWithUserInterface)
				{
					if (this.password == null && this.options.IsDisabled(ConnectionInfo.ConnectionOptions.UseADTranslation))
					{
						this.options.Enable(ConnectionInfo.ConnectionOptions.AccessTokenRequired);
					}
					this.allowPrompt = false;
					return;
				}
				this.allowPrompt = true;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000080AC File Offset: 0x000062AC
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

		// Token: 0x06000166 RID: 358 RVA: 0x00008168 File Offset: 0x00006368
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

		// Token: 0x06000167 RID: 359 RVA: 0x000081B8 File Offset: 0x000063B8
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

		// Token: 0x06000168 RID: 360 RVA: 0x000084B4 File Offset: 0x000066B4
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

		// Token: 0x06000169 RID: 361 RVA: 0x0000852D File Offset: 0x0000672D
		private Guid GetEffectiveActivityId()
		{
			if (this.ConnectionActivityId != Guid.Empty)
			{
				return this.ConnectionActivityId;
			}
			return this.ClientActivityID;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000854E File Offset: 0x0000674E
		private Uri GetEffectiveDataSource()
		{
			if (!this.options.IsEnabled(ConnectionInfo.ConnectionOptions.ResolvedLinkFile))
			{
				return new Uri(this.dataSource);
			}
			return new Uri(this.server);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000857C File Offset: 0x0000677C
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
				httpWebRequest.UserAgent = "AMO";
				using (Stream responseStream = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream())
				{
					string text;
					ASLinkFile.LoadFromStream(responseStream, out server, out database, out text, out isDelegationAllowed);
				}
			}
			catch (WebException ex)
			{
				throw new ConnectionException(XmlaSR.ConnectionString_LinkFileDownloadError(dataSource), ex);
			}
			catch (XmlException ex2)
			{
				throw new ConnectionException(XmlaSR.ConnectionString_LinkFileParseError(4096), ex2);
			}
			catch (XmlSchemaException ex3)
			{
				throw new ConnectionException(XmlaSR.ConnectionString_LinkFileParseError(4096), ex3);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000865C File Offset: 0x0000685C
		private void ResolveHTTPConnectionPropertiesForPaaSInfrastructure(IConnectivityOwner owner, ref Uri dataSourceUri, bool acquireAADToken, bool returnCloudConnectionAuthenticationProperties, bool isRedirectedWorkspace, string tenantId, out string paasCoreServerName, out CloudConnectionAuthenticationProperties cloudConnectionAuthenticationProperties)
		{
			paasCoreServerName = null;
			cloudConnectionAuthenticationProperties = null;
			Guid effectiveActivityId = this.GetEffectiveActivityId();
			Microsoft.AnalysisServices.Authentication.AuthenticationManager.EnableStrongSecurityProtocols();
			TimeoutUtils.TimeLeft timeLeft = TimeoutUtils.TimeLeft.FromSeconds(this.ConnectTimeout);
			string text = (this.IsPbiPremiumInternal ? this.Catalog : null);
			if (this.IsPbiPremiumInternal || this.IsAsAzure)
			{
				if (!isRedirectedWorkspace)
				{
					AsPaasEndpointInfo asPaasEndpointInfo = ASAzureUtility.ResolvePaaSConnectionEndpointDetail(this.AsInstanceType, dataSourceUri, this.PaaSInfrastructureServerName, text, false, ref timeLeft, delegate(bool isOnDispose)
					{
						throw new ConnectionException(XmlaSR.XmlaClient_ConnectTimedOut);
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
						throw new ConnectionException(XmlaSR.XmlaClient_PbiPublicXmlaWithEmbedToken_UserId_Prop_NotSupported);
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
							throw new ConnectionException(XmlaSR.XmlaClient_PbiPremium_WorkspaceNotFound(text7, text8));
						}
						if (resolvePbiWorkspaceErrorReason != ResolvePbiWorkspaceErrorReason.WorkspaceNameDuplicated)
						{
							throw new ConnectionException(XmlaSR.InternalError + text7);
						}
						throw new ConnectionException(XmlaSR.XmlaClient_PbiPremium_WorkspaceNameDuplicated(text7, text8));
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
								throw new ConnectionException(XmlaSR.XmlaClient_PbiPublicXmla_DatasetNotSpecified);
							}
							if (this.IsPbiPremiumXmlaEpWithPowerBIEmbedToken)
							{
								throw new ConnectionException(XmlaSR.XmlaClient_PbiPublicXmlaWithEmbedToken_ToPBIShared_NotSupported);
							}
							string text10;
							ArtifactCapacityState artifactCapacityState2;
							ResolveDatabaseNameErrorReason resolveDatabaseNameErrorReason2;
							if (!PbiPremiumAuthenticationHandle.TryGetDatabaseName(dataSourceUri.Authority, text3, text6, this.Catalog, authenticationHandle, effectiveActivityId.Equals(Guid.Empty) ? string.Empty : effectiveActivityId.ToString(), out text10, out artifactCapacityState2, out resolveDatabaseNameErrorReason2, out text7))
							{
								if (resolveDatabaseNameErrorReason2 == ResolveDatabaseNameErrorReason.DatabaseNotFound)
								{
									throw new ConnectionException(XmlaSR.XmlaClient_PbiPublicXmla_DatasetNotFound(this.Catalog, text7));
								}
								if (resolveDatabaseNameErrorReason2 != ResolveDatabaseNameErrorReason.DatabaseNameDuplicated)
								{
									throw new ConnectionException(XmlaSR.InternalError + text7);
								}
								throw new ConnectionException(XmlaSR.XmlaClient_PbiPublicXmla_DatasetNameDuplicated(this.Catalog, text7));
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
								throw new ConnectionException(XmlaSR.XmlaClient_ConnectTimedOut);
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008B94 File Offset: 0x00006D94
		private bool ParseShortcutForm()
		{
			if (!this.connectionString.Contains("="))
			{
				this.dataSource = this.connectionString;
				this.ParseDataSourceAccessMode(ref this.dataSource);
				this.ExtractDataSourceParts(this.dataSource, null);
				return true;
			}
			return false;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008BD0 File Offset: 0x00006DD0
		private void HandleKeyValueDuringConnectionStringParsing(string key, string value)
		{
			if (this.CheckAndSetDataSource(key, value))
			{
				return;
			}
			if (this.CheckAndSetLocation(key, value))
			{
				return;
			}
			if (this.CheckAndSetUserID(key, value))
			{
				return;
			}
			if (this.CheckAndSetPassword(key, value))
			{
				return;
			}
			if (this.CheckAndSetProtectionLevel(key, value))
			{
				return;
			}
			if (this.CheckAndSetIntegratedSecurity(key, value))
			{
				return;
			}
			if (this.CheckAndSetConnectTimeout(key, value))
			{
				return;
			}
			if (this.CheckAndSetAutoSyncPeriod(key, value))
			{
				return;
			}
			if (this.CheckAndSetConnectTo(key, value))
			{
				return;
			}
			if (this.CheckAndSetProtocolFormat(key, value))
			{
				return;
			}
			if (this.CheckAndSetTransportCompression(key, value))
			{
				return;
			}
			if (this.CheckAndSetCompressionLevel(key, value))
			{
				return;
			}
			if (this.CheckAndSetEncryptionPassword(key, value))
			{
				return;
			}
			if (this.CheckAndSetImpersonationLevel(key, value))
			{
				return;
			}
			if (this.CheckAndSetRestrictedClient(key, value))
			{
				return;
			}
			if (this.CheckAndSetSspi(key, value))
			{
				return;
			}
			if (this.CheckAndSetHttpHandling(key, value))
			{
				return;
			}
			if (this.CheckAndSetUseExistingFile(key, value))
			{
				return;
			}
			if (this.CheckAndSetCharacterEncoding(key, value))
			{
				return;
			}
			if (this.CheckAndSetUseEncryptionForData(key, value))
			{
				return;
			}
			if (this.CheckAndSetPacketSize(key, value))
			{
				return;
			}
			if (this.CheckAndSetPersistSecurityInfo(key, value))
			{
				return;
			}
			if (this.CheckAndSetSessionID(key, value))
			{
				return;
			}
			if (this.CheckAndSetDataSourceVersion(key, value))
			{
				return;
			}
			if (this.CheckAndSetClientCertificateThumbprint(key, value))
			{
				return;
			}
			if (this.CheckAndSetUserIdentity(key, value))
			{
				return;
			}
			if (this.CheckAndSetExtendedProperties(key, value))
			{
				return;
			}
			if (this.CheckAndSetCertificate(key, value))
			{
				return;
			}
			if (this.CheckAndSetTokenCacheMode(key, value))
			{
				return;
			}
			if (this.CheckAndSetUseAdalCache(key, value))
			{
				return;
			}
			if (this.CheckAndSetApplyAuxiliaryPermission(key, value))
			{
				return;
			}
			if (this.CheckAndSetAuxiliaryPermissionOwner(key, value))
			{
				return;
			}
			if (this.CheckAndSetServiceToServiceToken(key, value))
			{
				return;
			}
			if (this.CheckAndSetTransientModelMode(key, value))
			{
				return;
			}
			if (this.CheckAndSetAuthenticationScheme(key, value))
			{
				return;
			}
			if (this.CheckAndSetExtAuthInfo(key, value))
			{
				return;
			}
			if (this.CheckAndSetIdentityProvider(key, value))
			{
				return;
			}
			if (this.CheckAndSetBypassAuthorization(key, value))
			{
				return;
			}
			if (this.CheckAndSetRestrictCatalog(key, value))
			{
				return;
			}
			if (this.CheckAndSetAccessMode(key, value))
			{
				return;
			}
			if (this.CheckAndSetRestrictUser(key, value))
			{
				return;
			}
			if (this.CheckAndSetRestrictRoles(key, value))
			{
				return;
			}
			if (this.CheckAndSetIntendedUsage(key, value))
			{
				return;
			}
			if (this.CheckAndSetDedicatedAdminConnection(key, value))
			{
				return;
			}
			if (this.CheckAndSetContextualIdentity(key, value))
			{
				return;
			}
			if (this.CheckAndSetConnectionActivityId(key, value))
			{
				return;
			}
			if (this.CheckAndSetAsAzureRedirection(key, value))
			{
				return;
			}
			if (this.CheckAndSetScaleOutAutoSync(key, value))
			{
				return;
			}
			if (this.CheckAndSetSourceCapacityObjectId(key, value))
			{
				return;
			}
			if (this.CheckAndSetServicePrincipalProfileId(key, value))
			{
				return;
			}
			if (this.CheckAndSetBypassBuildPermission(key, value))
			{
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
			this.InsertKeyValueIntoHash(key, value);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008E74 File Offset: 0x00007074
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

		// Token: 0x06000170 RID: 368 RVA: 0x00008EF4 File Offset: 0x000070F4
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

		// Token: 0x06000171 RID: 369 RVA: 0x00008F40 File Offset: 0x00007140
		private bool CheckAndSetLocation(string key, string value)
		{
			if (string.Compare(key, "Location", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.location = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008F60 File Offset: 0x00007160
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

		// Token: 0x06000173 RID: 371 RVA: 0x00008FC1 File Offset: 0x000071C1
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

		// Token: 0x06000174 RID: 372 RVA: 0x00008FF0 File Offset: 0x000071F0
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

		// Token: 0x06000175 RID: 373 RVA: 0x00009030 File Offset: 0x00007230
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

		// Token: 0x06000176 RID: 374 RVA: 0x000090D0 File Offset: 0x000072D0
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

		// Token: 0x06000177 RID: 375 RVA: 0x00009174 File Offset: 0x00007374
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

		// Token: 0x06000178 RID: 376 RVA: 0x000092BC File Offset: 0x000074BC
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

		// Token: 0x06000179 RID: 377 RVA: 0x00009348 File Offset: 0x00007548
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

		// Token: 0x0600017A RID: 378 RVA: 0x000093D0 File Offset: 0x000075D0
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

		// Token: 0x0600017B RID: 379 RVA: 0x0000945C File Offset: 0x0000765C
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

		// Token: 0x0600017C RID: 380 RVA: 0x0000951C File Offset: 0x0000771C
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

		// Token: 0x0600017D RID: 381 RVA: 0x00009594 File Offset: 0x00007794
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

		// Token: 0x0600017E RID: 382 RVA: 0x00009624 File Offset: 0x00007824
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

		// Token: 0x0600017F RID: 383 RVA: 0x000096A8 File Offset: 0x000078A8
		private bool CheckAndSetEncryptionPassword(string key, string value)
		{
			if (string.Compare(key, "Encryption Password", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.encryptionPassword = value;
				return true;
			}
			return false;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000096C4 File Offset: 0x000078C4
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

		// Token: 0x06000181 RID: 385 RVA: 0x00009757 File Offset: 0x00007957
		private bool CheckAndSetRestrictedClient(string key, string value)
		{
			if (string.Compare(key, "Restricted Client", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.RestrictedClient, ConnectionInfo.ParseBool(value));
				return true;
			}
			return false;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000977D File Offset: 0x0000797D
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

		// Token: 0x06000183 RID: 387 RVA: 0x000097AC File Offset: 0x000079AC
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

		// Token: 0x06000184 RID: 388 RVA: 0x00009820 File Offset: 0x00007A20
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

		// Token: 0x06000185 RID: 389 RVA: 0x00009894 File Offset: 0x00007A94
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

		// Token: 0x06000186 RID: 390 RVA: 0x00009934 File Offset: 0x00007B34
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

		// Token: 0x06000187 RID: 391 RVA: 0x000099D4 File Offset: 0x00007BD4
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

		// Token: 0x06000188 RID: 392 RVA: 0x00009A80 File Offset: 0x00007C80
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

		// Token: 0x06000189 RID: 393 RVA: 0x00009AB4 File Offset: 0x00007CB4
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

		// Token: 0x0600018A RID: 394 RVA: 0x00009B3C File Offset: 0x00007D3C
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

		// Token: 0x0600018B RID: 395 RVA: 0x00009C10 File Offset: 0x00007E10
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

		// Token: 0x0600018C RID: 396 RVA: 0x00009CFF File Offset: 0x00007EFF
		private bool CheckAndSetApplyAuxiliaryPermission(string key, string value)
		{
			if (string.Compare(key, "ApplyAuxiliaryPermission", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.ApplyAuxiliaryPermission, ConnectionInfo.ParseBool(value));
				return true;
			}
			return false;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00009D29 File Offset: 0x00007F29
		private bool CheckAndSetAuxiliaryPermissionOwner(string key, string value)
		{
			if (string.Compare(key, "AuxiliaryPermissionOwner", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.auxiliaryPermissionOwner = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00009D48 File Offset: 0x00007F48
		private bool CheckAndSetServiceToServiceToken(string key, string value)
		{
			if (string.Compare(key, "ServiceToServiceToken", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.serviceToServiceToken = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009D68 File Offset: 0x00007F68
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

		// Token: 0x06000190 RID: 400 RVA: 0x00009DBC File Offset: 0x00007FBC
		private bool CheckAndSetClientCertificateThumbprint(string key, string value)
		{
			if (string.Compare(key, "Client Certificate Thumbprint", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.clientCertificateThumbprint = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009DDB File Offset: 0x00007FDB
		private bool CheckAndSetExtendedProperties(string key, string value)
		{
			if (ConnectionInfo.IsExtendedProperties(key))
			{
				this.innerConnectionString = value;
				return true;
			}
			return false;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009DF0 File Offset: 0x00007FF0
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

		// Token: 0x06000193 RID: 403 RVA: 0x00009E42 File Offset: 0x00008042
		private bool CheckAndSetSessionID(string key, string value)
		{
			if (string.Compare(key, "SessionID", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.sessionID = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009E61 File Offset: 0x00008061
		private bool CheckAndSetCertificate(string key, string value)
		{
			if (string.Compare(key, "Certificate", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.certificate = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009E80 File Offset: 0x00008080
		private bool CheckAndSetAuthenticationScheme(string key, string value)
		{
			if (string.Compare(key, "Authentication Scheme", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.authenticationScheme = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009E9F File Offset: 0x0000809F
		private bool CheckAndSetExtAuthInfo(string key, string value)
		{
			if (string.Compare(key, "Ext Auth Info", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.extAuthInfo = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009EBE File Offset: 0x000080BE
		private bool CheckAndSetIdentityProvider(string key, string value)
		{
			if (string.Compare(key, "Identity Provider", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.identityProvider = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009EDD File Offset: 0x000080DD
		private bool CheckAndSetBypassAuthorization(string key, string value)
		{
			if (string.Compare(key, "Bypass Authorization", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.bypassAuthorization = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009EFC File Offset: 0x000080FC
		private bool CheckAndSetRestrictCatalog(string key, string value)
		{
			if (string.Compare(key, "Restrict Catalog", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.restrictCatalog = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009F1B File Offset: 0x0000811B
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

		// Token: 0x0600019B RID: 411 RVA: 0x00009F46 File Offset: 0x00008146
		private bool CheckAndSetRestrictUser(string key, string value)
		{
			if (string.Compare(key, "Restrict User", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.restrictUser = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009F65 File Offset: 0x00008165
		private bool CheckAndSetRestrictRoles(string key, string value)
		{
			if (!key.Equals("Restrict Roles", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			this.restrictRoles = ConnectionInfo.Trim(value);
			return true;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009F84 File Offset: 0x00008184
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

		// Token: 0x0600019E RID: 414 RVA: 0x0000A016 File Offset: 0x00008216
		private bool CheckAndSetDedicatedAdminConnection(string key, string value)
		{
			if (string.Compare(key, "DedicatedAdminConnection", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.DedicatedAdminConnection, ConnectionInfo.ParseBool(value));
				return true;
			}
			return false;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000A040 File Offset: 0x00008240
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

		// Token: 0x060001A0 RID: 416 RVA: 0x0000A0CC File Offset: 0x000082CC
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

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A100 File Offset: 0x00008300
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

		// Token: 0x060001A2 RID: 418 RVA: 0x0000A160 File Offset: 0x00008360
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

		// Token: 0x060001A3 RID: 419 RVA: 0x0000A1D9 File Offset: 0x000083D9
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

		// Token: 0x060001A4 RID: 420 RVA: 0x0000A20B File Offset: 0x0000840B
		private bool CheckAndSetServicePrincipalProfileId(string key, string value)
		{
			if (string.Compare(key, "SPN Profile", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.servicePrincipalProfileId = ConnectionInfo.Trim(value);
				return true;
			}
			return false;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000A22A File Offset: 0x0000842A
		private bool CheckAndSetBypassBuildPermission(string key, string value)
		{
			if (string.Compare(key, "BypassBuildPermission", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.options.Update(ConnectionInfo.ConnectionOptions.BypassBuildPermission, ConnectionInfo.ParseBool(value));
				return true;
			}
			return false;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000A254 File Offset: 0x00008454
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

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A2B3 File Offset: 0x000084B3
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

		// Token: 0x060001A8 RID: 424 RVA: 0x0000A2DE File Offset: 0x000084DE
		[Conditional("_AS_ADOMD")]
		private void SetOriginalConnectionStringValue(string key, string value)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A2E0 File Offset: 0x000084E0
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

		// Token: 0x060001AA RID: 426 RVA: 0x0000A344 File Offset: 0x00008544
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
					authenticationHandle2 = Microsoft.AnalysisServices.Authentication.AuthenticationManager.Authenticate(new AuthenticationOptions(owner)
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

		// Token: 0x040000EE RID: 238
		private const string LogicalActivityContextClient = "AnalysisServices.ClientActivityId";

		// Token: 0x040000EF RID: 239
		private const string LogicalActivityContextCurrent = "AnalysisServices.CurrentActivityId";

		// Token: 0x040000F0 RID: 240
		internal const int DefaultInstancePort = 2383;

		// Token: 0x040000F1 RID: 241
		private const int SqlBrowserPort = 2382;

		// Token: 0x040000F2 RID: 242
		private static MDXMLAPropInfo PropInfo = new MDXMLAPropInfo();

		// Token: 0x040000F3 RID: 243
		internal const string Localhost = "localhost";

		// Token: 0x040000F4 RID: 244
		internal const string Embedded = "$Embedded$";

		// Token: 0x040000F5 RID: 245
		private static readonly IList<string> ppeEndpoints = new List<string> { "biazure-int-edog-redirect.analysis-df.windows.net", "onebox-redirect.analysis.windows-int.net" };

		// Token: 0x040000F6 RID: 246
		private const string PbiExternalDatabaseNamePrefix = "sobe_wowvirtualserver-";

		// Token: 0x040000F7 RID: 247
		private const string AnalyzeInExcelPath = "xmla";

		// Token: 0x040000F8 RID: 248
		private const string AnalyzeInExcelQueryFormat = "vs=sobe_wowvirtualserver&db={0}";

		// Token: 0x040000F9 RID: 249
		internal const int TimeoutDefault = 0;

		// Token: 0x040000FA RID: 250
		internal const int ConnectTimeoutDefault = 60;

		// Token: 0x040000FB RID: 251
		internal const uint AutoSyncPeriodDefault = 10000U;

		// Token: 0x040000FC RID: 252
		private const ProtectionLevel ProtectionLevelDefault = ProtectionLevel.Privacy;

		// Token: 0x040000FD RID: 253
		private const int CompressionLevelDefault = 0;

		// Token: 0x040000FE RID: 254
		private const SafetyOptions SafetyOptionDefault = SafetyOptions.Default;

		// Token: 0x040000FF RID: 255
		private const IntegratedSecurity IntegratedSecurityDefault = IntegratedSecurity.Unspecified;

		// Token: 0x04000100 RID: 256
		private const ImpersonationLevel ImpersonationLevelDefault = ImpersonationLevel.Impersonate;

		// Token: 0x04000101 RID: 257
		private const string SspiDefault = "Negotiate";

		// Token: 0x04000102 RID: 258
		private const string SspiSchannel = "Schannel";

		// Token: 0x04000103 RID: 259
		private const string SspiUni = "Microsoft Unified Security Protocol Provider";

		// Token: 0x04000104 RID: 260
		private const int PacketSizeDefault = 4096;

		// Token: 0x04000105 RID: 261
		private const int PacketSizeMinValue = 512;

		// Token: 0x04000106 RID: 262
		private const int PacketSizeMaxValue = 32767;

		// Token: 0x04000107 RID: 263
		private const int MaxCharsInLinkFile = 4096;

		// Token: 0x04000108 RID: 264
		private const string LinkFileExtension = ".bism";

		// Token: 0x04000109 RID: 265
		private const string LinkFileXmlNamespace = "http://schemas.microsoft.com/analysisservices/linkfile";

		// Token: 0x0400010A RID: 266
		private const string LinkFileMainElement = "ASLinkFile";

		// Token: 0x0400010B RID: 267
		private const string LinkFileServerElement = "Server";

		// Token: 0x0400010C RID: 268
		private const string LinkFileDatabaseElement = "Database";

		// Token: 0x0400010D RID: 269
		private const string LinkFileDelegationAttribute = "allowDelegation";

		// Token: 0x0400010E RID: 270
		private const string LinkFileSchema = "<?xml version='1.0' encoding='utf-8'?> \r\n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' targetNamespace='http://schemas.microsoft.com/analysisservices/linkfile' elementFormDefault='qualified'>\r\n    <xs:element name='ASLinkFile'>\r\n                    <xs:complexType>\r\n                        <xs:all>\r\n                            <xs:element name='Server' type='xs:string'/>\r\n                            <xs:element name='Database' type='xs:string' />\r\n                            <xs:element name='Description' type='xs:string' minOccurs='0'/>\r\n                        </xs:all>\r\n                        <xs:attribute name='allowDelegation' type='xs:boolean' default='false'/>\r\n                    </xs:complexType>\r\n    </xs:element>\r\n</xs:schema>";

		// Token: 0x0400010F RID: 271
		private string connectionString;

		// Token: 0x04000110 RID: 272
		private ConnectionInfo.ConnectionOptionsWrapper options = new ConnectionInfo.ConnectionOptionsWrapper(ConnectionInfo.ConnectionOptions.Default);

		// Token: 0x04000111 RID: 273
		private ConnectionType connectionType;

		// Token: 0x04000112 RID: 274
		private AsInstanceType asInstanceType;

		// Token: 0x04000113 RID: 275
		private string dataSource;

		// Token: 0x04000114 RID: 276
		private string linkReferenceRawDataSource;

		// Token: 0x04000115 RID: 277
		private string location;

		// Token: 0x04000116 RID: 278
		private string server;

		// Token: 0x04000117 RID: 279
		private string instanceName;

		// Token: 0x04000118 RID: 280
		private string port;

		// Token: 0x04000119 RID: 281
		private string userID;

		// Token: 0x0400011A RID: 282
		private string password;

		// Token: 0x0400011B RID: 283
		private AuthenticationHandle authHandle;

		// Token: 0x0400011C RID: 284
		private int timeout;

		// Token: 0x0400011D RID: 285
		private int connectTimeout;

		// Token: 0x0400011E RID: 286
		private uint autoSyncPeriod;

		// Token: 0x0400011F RID: 287
		private string catalog;

		// Token: 0x04000120 RID: 288
		private ProtectionLevel protectionLevel;

		// Token: 0x04000121 RID: 289
		private ConnectTo connectTo;

		// Token: 0x04000122 RID: 290
		private SafetyOptions safetyOptions;

		// Token: 0x04000123 RID: 291
		private ProtocolFormat protocolFormat;

		// Token: 0x04000124 RID: 292
		private TransportCompression transportCompression;

		// Token: 0x04000125 RID: 293
		private int compressionLevel;

		// Token: 0x04000126 RID: 294
		private IntegratedSecurity integratedSecurity;

		// Token: 0x04000127 RID: 295
		private string encryptionPassword;

		// Token: 0x04000128 RID: 296
		private ImpersonationLevel impersonationLevel;

		// Token: 0x04000129 RID: 297
		private string sspi;

		// Token: 0x0400012A RID: 298
		private Encoding characterEncoding;

		// Token: 0x0400012B RID: 299
		private int packetSize;

		// Token: 0x0400012C RID: 300
		private string innerConnectionString;

		// Token: 0x0400012D RID: 301
		private ListDictionary extendedProperties = new ListDictionary();

		// Token: 0x0400012E RID: 302
		private string restrictedConnectionString;

		// Token: 0x0400012F RID: 303
		private string sessionID;

		// Token: 0x04000130 RID: 304
		private string clientCertificateThumbprint;

		// Token: 0x04000131 RID: 305
		private Guid autoGeneratedActivityID = Guid.NewGuid();

		// Token: 0x04000132 RID: 306
		private UserIdentityType userIdentity;

		// Token: 0x04000133 RID: 307
		private string pbiPremiumTenant;

		// Token: 0x04000134 RID: 308
		private string pbiPremiumWorkspaceName;

		// Token: 0x04000135 RID: 309
		private string pbiPremiumWorkspaceObjectId;

		// Token: 0x04000136 RID: 310
		private string pbipWorkloadResourceMoniker;

		// Token: 0x04000137 RID: 311
		private string pbipCoreServiceRoutingHint;

		// Token: 0x04000138 RID: 312
		private bool skipConnectionStringReconversion;

		// Token: 0x04000139 RID: 313
		private string transientModelMode;

		// Token: 0x0400013A RID: 314
		private string contextualIdentity;

		// Token: 0x0400013B RID: 315
		private Guid connectionActivityId;

		// Token: 0x0400013C RID: 316
		private string auxiliaryPermissionOwner;

		// Token: 0x0400013D RID: 317
		private string serviceToServiceToken;

		// Token: 0x0400013E RID: 318
		private Guid sourceCapacityObjectId;

		// Token: 0x0400013F RID: 319
		private string servicePrincipalProfileId;

		// Token: 0x04000140 RID: 320
		private string certificate;

		// Token: 0x04000141 RID: 321
		private string authenticationScheme;

		// Token: 0x04000142 RID: 322
		private string extAuthInfo;

		// Token: 0x04000143 RID: 323
		private string identityProvider;

		// Token: 0x04000144 RID: 324
		private string bypassAuthorization;

		// Token: 0x04000145 RID: 325
		private string restrictCatalog;

		// Token: 0x04000146 RID: 326
		private string accessMode;

		// Token: 0x04000147 RID: 327
		private string restrictUser;

		// Token: 0x04000148 RID: 328
		private string restrictRoles;

		// Token: 0x04000149 RID: 329
		private IntendedUsage intendedUsage;

		// Token: 0x0400014A RID: 330
		private AsAzureRedirection asAzureRedirection;

		// Token: 0x0400014B RID: 331
		private ConnectionAccessMode pbipAccessMode;

		// Token: 0x0400014C RID: 332
		private string dataSourceVersion;

		// Token: 0x0400014D RID: 333
		private string redirectorAddress;

		// Token: 0x0400014E RID: 334
		private string sandboxPath;

		// Token: 0x0400014F RID: 335
		private bool isUseAdalCacheSpecifiedOnConnectionString;

		// Token: 0x04000150 RID: 336
		private string paasInfrastructureServerName;

		// Token: 0x04000151 RID: 337
		private bool allowPrompt;

		// Token: 0x04000152 RID: 338
		private const string TimeoutPropertyName = "Timeout";

		// Token: 0x04000153 RID: 339
		private const string DataSourcePropertyName = "Data Source";

		// Token: 0x04000154 RID: 340
		private const string UserIDPropertyName = "User ID";

		// Token: 0x04000155 RID: 341
		private const string PasswordPropertyName = "Password";

		// Token: 0x04000156 RID: 342
		private const string ProtectionLevelPropertyName = "Protection Level";

		// Token: 0x04000157 RID: 343
		private const string ConnectTimeoutPropertyName = "Connect Timeout";

		// Token: 0x04000158 RID: 344
		private const string AutoSyncPeriodPropertyName = "Auto Synch Period";

		// Token: 0x04000159 RID: 345
		private const string ProviderPropertyName = "Provider";

		// Token: 0x0400015A RID: 346
		private const string DataSourceInfoPropertyName = "DataSourceInfo";

		// Token: 0x0400015B RID: 347
		private const string CatalogPropertyName = "Catalog";

		// Token: 0x0400015C RID: 348
		private const string IntegratedSecurityPropertyName = "Integrated Security";

		// Token: 0x0400015D RID: 349
		private const string ConnectToPropertyName = "ConnectTo";

		// Token: 0x0400015E RID: 350
		private const string SafetyOptionsPropertyName = "Safety Options";

		// Token: 0x0400015F RID: 351
		private const string ProtocolFormatPropertyName = "Protocol Format";

		// Token: 0x04000160 RID: 352
		private const string TransportCompressionPropertyName = "Transport Compression";

		// Token: 0x04000161 RID: 353
		private const string CompressionLevelPropertyName = "Compression Level";

		// Token: 0x04000162 RID: 354
		private const string EncryptionPasswordPropertyName = "Encryption Password";

		// Token: 0x04000163 RID: 355
		private const string ImpersonationLevelPropertyName = "Impersonation Level";

		// Token: 0x04000164 RID: 356
		private const string SspiPropertyName = "SSPI";

		// Token: 0x04000165 RID: 357
		private const string HttpHandlingPropertyName = "HttpChannelHandling";

		// Token: 0x04000166 RID: 358
		private const string UseExistingFilePropertyName = "UseExistingFile";

		// Token: 0x04000167 RID: 359
		private const string CharacterEncodingPropertyName = "Character Encoding";

		// Token: 0x04000168 RID: 360
		private const string UseEncryptionForDataPropertyName = "Use Encryption for Data";

		// Token: 0x04000169 RID: 361
		private const string PacketSizePropertyName = "Packet Size";

		// Token: 0x0400016A RID: 362
		private const string ExtendedPropertiesPropertyName = "Extended Properties";

		// Token: 0x0400016B RID: 363
		private const string LcidPropertyName = "LocaleIdentifier";

		// Token: 0x0400016C RID: 364
		private const string LocationPropertyName = "Location";

		// Token: 0x0400016D RID: 365
		private const string RestrictedClientPropertyName = "Restricted Client";

		// Token: 0x0400016E RID: 366
		private const string PersistSecurityInfoName = "Persist Security Info";

		// Token: 0x0400016F RID: 367
		private const string SessionIDPropertyName = "SessionID";

		// Token: 0x04000170 RID: 368
		private const string ClientProcessIDPropertyName = "ClientProcessID";

		// Token: 0x04000171 RID: 369
		private const string ApplicationNameXmlaPropertyName = "SspropInitAppName";

		// Token: 0x04000172 RID: 370
		private const string ClientCertificateThumbprintPropertyName = "Client Certificate Thumbprint";

		// Token: 0x04000173 RID: 371
		private const string UserIdentityPropertyName = "User Identity";

		// Token: 0x04000174 RID: 372
		private const string DataSourceVersionPropertyName = "Data Source Version";

		// Token: 0x04000175 RID: 373
		private const string CertificatePropertyName = "Certificate";

		// Token: 0x04000176 RID: 374
		private const string AuthenticationSchemePropertyName = "Authentication Scheme";

		// Token: 0x04000177 RID: 375
		private const string ExtAuthInfoPropertyName = "Ext Auth Info";

		// Token: 0x04000178 RID: 376
		private const string IdentityProviderPropertyName = "Identity Provider";

		// Token: 0x04000179 RID: 377
		private const string BypassAuthorizationPropertyName = "Bypass Authorization";

		// Token: 0x0400017A RID: 378
		private const string RestrictCatalogPropertyName = "Restrict Catalog";

		// Token: 0x0400017B RID: 379
		private const string AccessModePropertyName = "Access Mode";

		// Token: 0x0400017C RID: 380
		private const string UseAdalCachePropertyName = "UseADALCache";

		// Token: 0x0400017D RID: 381
		private const string TokenCacheModePropertyName = "Token Cache Mode";

		// Token: 0x0400017E RID: 382
		private const string ApplyAuxiliaryPermissionPropertyName = "ApplyAuxiliaryPermission";

		// Token: 0x0400017F RID: 383
		private const string AuxiliaryPermissionOwnerPropertyName = "AuxiliaryPermissionOwner";

		// Token: 0x04000180 RID: 384
		private const string ServiceToServiceTokenPropertyName = "ServiceToServiceToken";

		// Token: 0x04000181 RID: 385
		private const string TransientModelModePropertyName = "Transient Model";

		// Token: 0x04000182 RID: 386
		private const string RestrictUserPropertyName = "Restrict User";

		// Token: 0x04000183 RID: 387
		private const string RestrictRolesPropertyName = "Restrict Roles";

		// Token: 0x04000184 RID: 388
		private const string DedicatedAdminConnectionPropertyName = "DedicatedAdminConnection";

		// Token: 0x04000185 RID: 389
		private const string ContextualIdentityPropertyName = "ContextualIdentity";

		// Token: 0x04000186 RID: 390
		internal const string EffectiveUserNamePropertyName = "EffectiveUserName";

		// Token: 0x04000187 RID: 391
		private const string IntendedUsagePropertyName = "Intended Usage";

		// Token: 0x04000188 RID: 392
		private const string ConnectionActivityIdPropertyName = "ConnectionActivityId";

		// Token: 0x04000189 RID: 393
		private const string AsAzureRedirectionPropertyName = "AsAzureRedirection";

		// Token: 0x0400018A RID: 394
		private const string ScaleOutAutoSyncPropertyName = "Scale Out Auto Sync";

		// Token: 0x0400018B RID: 395
		private const string SourceCapacityObjectIdPropertyName = "SourceCapacityObjectId";

		// Token: 0x0400018C RID: 396
		private const string ServicePrincipalProfileIdPropertyName = "SPN Profile";

		// Token: 0x0400018D RID: 397
		private const string BypassBuildPermissionPropertyName = "BypassBuildPermission";

		// Token: 0x0400018E RID: 398
		private static string[] DeprecatedPropertyNames = new string[] { "External Tenant Id", "External User Id", "External Service Domain Name", "External Certificate Thumbprint" };

		// Token: 0x0400018F RID: 399
		private static string[] DatasourcePropertyNames = new string[] { "Data Source", "DataSource", "DSN" };

		// Token: 0x04000190 RID: 400
		private static string[] UserIDPropertyNames = new string[] { "User ID", "UID" };

		// Token: 0x04000191 RID: 401
		private static string[] PasswordPropertyNames = new string[] { "Password", "PWD" };

		// Token: 0x04000192 RID: 402
		private static string[] CatalogPropertyNames = new string[] { "Initial Catalog", "Catalog", "Database" };

		// Token: 0x04000193 RID: 403
		private const string ProtectionLevelNone = "NONE";

		// Token: 0x04000194 RID: 404
		private const string ProtectionLevelConnect = "CONNECT";

		// Token: 0x04000195 RID: 405
		private const string ProtectionLevelIntegrity = "PKT INTEGRITY";

		// Token: 0x04000196 RID: 406
		private const string ProtectionLevelPrivacy = "PKT PRIVACY";

		// Token: 0x04000197 RID: 407
		private const string IntegratedSecuritySspi = "SSPI";

		// Token: 0x04000198 RID: 408
		private const string IntegratedSecurityBasic = "Basic";

		// Token: 0x04000199 RID: 409
		private const string IntegratedSecurityFederated = "Federated";

		// Token: 0x0400019A RID: 410
		private const string IntegratedSecurityClaimsToken = "ClaimsToken";

		// Token: 0x0400019B RID: 411
		internal const string IdentityProviderMsoID = "MsoID";

		// Token: 0x0400019C RID: 412
		private const string IdentityProviderPowerBIEmbed = "PowerBIEmbed";

		// Token: 0x0400019D RID: 413
		private const string IdentityProviderDataverse = "Dataverse";

		// Token: 0x0400019E RID: 414
		private const string UserIdentityDefault = "DEFAULT";

		// Token: 0x0400019F RID: 415
		private const string UserIdentityWindows = "Windows Identity";

		// Token: 0x040001A0 RID: 416
		private const string UserIdentitySharePointPrincipal = "SharePoint Principal";

		// Token: 0x040001A1 RID: 417
		private const string ConnectToDefault = "DEFAULT";

		// Token: 0x040001A2 RID: 418
		private const string ConnectToShiloh = "8.0";

		// Token: 0x040001A3 RID: 419
		private const string ConnectToYukon = "9.0";

		// Token: 0x040001A4 RID: 420
		private const string ConnectToKatmai = "10.0";

		// Token: 0x040001A5 RID: 421
		private const string ConnectToDenali = "11.0";

		// Token: 0x040001A6 RID: 422
		private const string ConnectToSQL14 = "12.0";

		// Token: 0x040001A7 RID: 423
		private const string ConnectToSQL15 = "13.0";

		// Token: 0x040001A8 RID: 424
		private const string ConnectToSQL17 = "14.0";

		// Token: 0x040001A9 RID: 425
		private const string ConnectToSSAS18 = "15.0";

		// Token: 0x040001AA RID: 426
		private const string ConnectToSSAS21 = "16.0";

		// Token: 0x040001AB RID: 427
		private const string AutoSyncPeriodNull = "NULL";

		// Token: 0x040001AC RID: 428
		private const string ProtocolFormatDefault = "Default";

		// Token: 0x040001AD RID: 429
		private const string ProtocolFormatXml = "XML";

		// Token: 0x040001AE RID: 430
		private const string ProtocolFormatBinary = "Binary";

		// Token: 0x040001AF RID: 431
		private const string TransportCompressionDefault = "Default";

		// Token: 0x040001B0 RID: 432
		private const string TransportCompressionNone = "None";

		// Token: 0x040001B1 RID: 433
		private const string TransportCompressionCompressed = "Compressed";

		// Token: 0x040001B2 RID: 434
		private const string TransportCompressionGzip = "Gzip";

		// Token: 0x040001B3 RID: 435
		private const string ImpersonationLevelAnonymous = "Anonymous";

		// Token: 0x040001B4 RID: 436
		private const string ImpersonationLevelIdentify = "Identify";

		// Token: 0x040001B5 RID: 437
		private const string ImpersonationLevelImpersonate = "Impersonate";

		// Token: 0x040001B6 RID: 438
		private const string ImpersonationLevelDelegate = "Delegate";

		// Token: 0x040001B7 RID: 439
		private const string HttpHandlingDefault = "Default";

		// Token: 0x040001B8 RID: 440
		private const string HttpHandlingWebRequestBased = "WebRequestBased";

		// Token: 0x040001B9 RID: 441
		private const string HttpHandlingPreferHttpClient = "PreferHttpClient";

		// Token: 0x040001BA RID: 442
		private const string CharacterEncodingDefault = "Default";

		// Token: 0x040001BB RID: 443
		internal const string IntendedUsageDefault = "default";

		// Token: 0x040001BC RID: 444
		internal const string IntendedUsageProcessing = "processing";

		// Token: 0x040001BD RID: 445
		internal const string IntendedUsageInteractiveProcessing = "interactiveprocessing";

		// Token: 0x040001BE RID: 446
		internal const string IntendedUsageBackgroundQuery = "backgroundquery";

		// Token: 0x040001BF RID: 447
		internal const string MwcRoutingScenarioForProcessing = "Processing";

		// Token: 0x040001C0 RID: 448
		internal const string TransientModelModeEnabled = "Enabled";

		// Token: 0x040001C1 RID: 449
		internal const string TransientModelModeDisabled = "Disabled";

		// Token: 0x040001C2 RID: 450
		private const string AccessModeReadOnly = "readonly";

		// Token: 0x040001C3 RID: 451
		private const string AccessModeReadWrite = "readwrite";

		// Token: 0x02000167 RID: 359
		[Flags]
		private enum ConnectionOptions
		{
			// Token: 0x04000B77 RID: 2935
			None = 0,
			// Token: 0x04000B78 RID: 2936
			RestrictedClient = 1,
			// Token: 0x04000B79 RID: 2937
			UseExistingFile = 2,
			// Token: 0x04000B7A RID: 2938
			UseEncryptionForData = 4,
			// Token: 0x04000B7B RID: 2939
			PersistSecurityInfo = 8,
			// Token: 0x04000B7C RID: 2940
			DedicatedAdminConnection = 16,
			// Token: 0x04000B7D RID: 2941
			TokenCacheInDefaultMode = 64,
			// Token: 0x04000B7E RID: 2942
			TokenCacheExplicitMode = 128,
			// Token: 0x04000B7F RID: 2943
			ScaleOutAutoSync = 2048,
			// Token: 0x04000B80 RID: 2944
			ProtectionLevelWasSet = 4096,
			// Token: 0x04000B81 RID: 2945
			UseEncryptionForDataWasSet = 8192,
			// Token: 0x04000B82 RID: 2946
			PasswordPresent = 16384,
			// Token: 0x04000B83 RID: 2947
			AllowAutoRedirect = 32768,
			// Token: 0x04000B84 RID: 2948
			UseADTranslation = 65536,
			// Token: 0x04000B85 RID: 2949
			DisableSingleSignOn = 131072,
			// Token: 0x04000B86 RID: 2950
			AccessTokenRequired = 262144,
			// Token: 0x04000B87 RID: 2951
			UsedForSqlBrowser = 1048576,
			// Token: 0x04000B88 RID: 2952
			RestrictConnectionString = 2097152,
			// Token: 0x04000B89 RID: 2953
			RevertToProcessAccountForConnection = 4194304,
			// Token: 0x04000B8A RID: 2954
			AllowDelegation = 8388608,
			// Token: 0x04000B8B RID: 2955
			ResolvedLinkFile = 16777216,
			// Token: 0x04000B8C RID: 2956
			ResolvedAsAzureRedirection = 33554432,
			// Token: 0x04000B8D RID: 2957
			ApplyAuxiliaryPermission = 67108864,
			// Token: 0x04000B8E RID: 2958
			BypassBuildPermission = 134217728,
			// Token: 0x04000B8F RID: 2959
			CloneForInstanceLookupMask = 12311,
			// Token: 0x04000B90 RID: 2960
			Default = 32832
		}

		// Token: 0x02000168 RID: 360
		private struct ConnectionOptionsWrapper
		{
			// Token: 0x060011D9 RID: 4569 RVA: 0x0003F05A File Offset: 0x0003D25A
			public ConnectionOptionsWrapper(ConnectionInfo.ConnectionOptions value)
			{
				this.value = value;
			}

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x060011DA RID: 4570 RVA: 0x0003F063 File Offset: 0x0003D263
			public ConnectionInfo.ConnectionOptions Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x060011DB RID: 4571 RVA: 0x0003F06B File Offset: 0x0003D26B
			public bool IsEnabled(ConnectionInfo.ConnectionOptions options)
			{
				return (this.value & options) == options;
			}

			// Token: 0x060011DC RID: 4572 RVA: 0x0003F078 File Offset: 0x0003D278
			public bool IsDisabled(ConnectionInfo.ConnectionOptions options)
			{
				return (this.value & options) == ConnectionInfo.ConnectionOptions.None;
			}

			// Token: 0x060011DD RID: 4573 RVA: 0x0003F085 File Offset: 0x0003D285
			public bool IsPartialyEnabled(ConnectionInfo.ConnectionOptions options)
			{
				return (this.value & options) > ConnectionInfo.ConnectionOptions.None;
			}

			// Token: 0x060011DE RID: 4574 RVA: 0x0003F092 File Offset: 0x0003D292
			public bool IsPartialyDisabled(ConnectionInfo.ConnectionOptions options)
			{
				return (this.value & options) != options;
			}

			// Token: 0x060011DF RID: 4575 RVA: 0x0003F0A2 File Offset: 0x0003D2A2
			public ConnectionInfo.ConnectionOptions Enable(ConnectionInfo.ConnectionOptions options)
			{
				return this.Update(options, true);
			}

			// Token: 0x060011E0 RID: 4576 RVA: 0x0003F0AC File Offset: 0x0003D2AC
			public ConnectionInfo.ConnectionOptions Disable(ConnectionInfo.ConnectionOptions options)
			{
				return this.Update(options, false);
			}

			// Token: 0x060011E1 RID: 4577 RVA: 0x0003F0B6 File Offset: 0x0003D2B6
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

			// Token: 0x060011E2 RID: 4578 RVA: 0x0003F0E0 File Offset: 0x0003D2E0
			public void Reset()
			{
				this.value = ConnectionInfo.ConnectionOptions.Default;
			}

			// Token: 0x060011E3 RID: 4579 RVA: 0x0003F0ED File Offset: 0x0003D2ED
			public ConnectionInfo.ConnectionOptionsWrapper CloneForInstanceLookup()
			{
				return new ConnectionInfo.ConnectionOptionsWrapper((this.value & ConnectionInfo.ConnectionOptions.CloneForInstanceLookupMask) | ConnectionInfo.ConnectionOptions.UsedForSqlBrowser);
			}

			// Token: 0x060011E4 RID: 4580 RVA: 0x0003F108 File Offset: 0x0003D308
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

			// Token: 0x060011E5 RID: 4581 RVA: 0x0003F14D File Offset: 0x0003D34D
			public override int GetHashCode()
			{
				return this.value.GetHashCode();
			}

			// Token: 0x060011E6 RID: 4582 RVA: 0x0003F160 File Offset: 0x0003D360
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

			// Token: 0x060011E7 RID: 4583 RVA: 0x0003F1EB File Offset: 0x0003D3EB
			public static implicit operator ConnectionInfo.ConnectionOptions(ConnectionInfo.ConnectionOptionsWrapper wrapper)
			{
				return wrapper.value;
			}

			// Token: 0x060011E8 RID: 4584 RVA: 0x0003F1F3 File Offset: 0x0003D3F3
			public static explicit operator ConnectionInfo.ConnectionOptionsWrapper(ConnectionInfo.ConnectionOptions options)
			{
				return new ConnectionInfo.ConnectionOptionsWrapper(options);
			}

			// Token: 0x04000B91 RID: 2961
			private ConnectionInfo.ConnectionOptions value;
		}

		// Token: 0x02000169 RID: 361
		private sealed class RefreshableExternalAuthenticationHandle : AuthenticationHandle
		{
			// Token: 0x060011E9 RID: 4585 RVA: 0x0003F1FC File Offset: 0x0003D3FC
			public RefreshableExternalAuthenticationHandle(IConnectivityOwner owner, string authenticationScheme)
				: base(AuthenticationEndpoint.Unknown, null, null)
			{
				this.authenticationScheme = authenticationScheme;
				this.owner = owner;
				this.refreshByTime = Microsoft.AnalysisServices.Authentication.AuthenticationManager.CalculateAccessTokenRefreshBy(owner.AccessToken.ExpirationTime);
			}

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x060011EA RID: 4586 RVA: 0x0003F239 File Offset: 0x0003D439
			public override string Principal
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x060011EB RID: 4587 RVA: 0x0003F240 File Offset: 0x0003D440
			public override string AuthenticationScheme
			{
				get
				{
					return this.authenticationScheme;
				}
			}

			// Token: 0x060011EC RID: 4588 RVA: 0x0003F248 File Offset: 0x0003D448
			public override string GetAccessToken()
			{
				if (this.refreshByTime < DateTimeOffset.Now)
				{
					this.owner.RefreshAccessToken();
					this.refreshByTime = Microsoft.AnalysisServices.Authentication.AuthenticationManager.CalculateAccessTokenRefreshBy(this.owner.AccessToken.ExpirationTime);
				}
				return this.owner.AccessToken.Token;
			}

			// Token: 0x060011ED RID: 4589 RVA: 0x0003F2A3 File Offset: 0x0003D4A3
			public override long GetRefreshByTimeAsFileTime()
			{
				return this.refreshByTime.ToFileTime();
			}

			// Token: 0x04000B92 RID: 2962
			private readonly string authenticationScheme;

			// Token: 0x04000B93 RID: 2963
			private readonly IConnectivityOwner owner;

			// Token: 0x04000B94 RID: 2964
			private DateTimeOffset refreshByTime;
		}
	}
}
