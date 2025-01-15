using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000FA RID: 250
	internal sealed class TdsParser
	{
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0004FF17 File Offset: 0x0004E117
		private static Task CompletedTask
		{
			get
			{
				if (TdsParser.completedTask == null)
				{
					TdsParser.completedTask = Task.FromResult<object>(null);
				}
				return TdsParser.completedTask;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0004FF30 File Offset: 0x0004E130
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0004FF38 File Offset: 0x0004E138
		private bool ClientOSEncryptionSupport
		{
			get
			{
				return SNILoadHandle.SingletonInstance.ClientOSEncryptionSupport;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0004FF44 File Offset: 0x0004E144
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x0004FF4C File Offset: 0x0004E14C
		internal bool IsColumnEncryptionSupported
		{
			get
			{
				return this._serverSupportsColumnEncryption;
			}
			set
			{
				this._serverSupportsColumnEncryption = value;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0004FF55 File Offset: 0x0004E155
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x0004FF5D File Offset: 0x0004E15D
		internal byte TceVersionSupported { get; set; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0004FF66 File Offset: 0x0004E166
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x0004FF6E File Offset: 0x0004E16E
		internal bool AreEnclaveRetriesSupported { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0004FF77 File Offset: 0x0004E177
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x0004FF7F File Offset: 0x0004E17F
		internal string EnclaveType { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0004FF88 File Offset: 0x0004E188
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x0004FF90 File Offset: 0x0004E190
		internal bool isTcpProtocol { get; set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0004FF99 File Offset: 0x0004E199
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x0004FFA1 File Offset: 0x0004E1A1
		internal string FQDNforDNSCache { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0004FFAA File Offset: 0x0004E1AA
		internal bool IsDataClassificationEnabled
		{
			get
			{
				return this.DataClassificationVersion != 0;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0004FFB5 File Offset: 0x0004E1B5
		// (set) Token: 0x06001472 RID: 5234 RVA: 0x0004FFBD File Offset: 0x0004E1BD
		internal int DataClassificationVersion { get; set; }

		// Token: 0x06001473 RID: 5235 RVA: 0x0004FFC8 File Offset: 0x0004E1C8
		internal TdsParser(bool MARS, bool fAsynchronous)
		{
			this._fMARS = MARS;
			this._physicalStateObj = new TdsParserStateObject(this);
			this.DataClassificationVersion = 0;
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00050026 File Offset: 0x0004E226
		internal SqlInternalConnectionTds Connection
		{
			get
			{
				return this._connHandler;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x00050030 File Offset: 0x0004E230
		private static bool EnableTruncateSwitch
		{
			get
			{
				bool flag = AppContext.TryGetSwitch("Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal", out flag) && flag;
				return flag;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00050051 File Offset: 0x0004E251
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00050059 File Offset: 0x0004E259
		internal SqlInternalTransaction CurrentTransaction
		{
			get
			{
				return this._currentTransaction;
			}
			set
			{
				if ((this._currentTransaction == null && value != null) || (this._currentTransaction != null && value == null))
				{
					this._currentTransaction = value;
				}
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00050078 File Offset: 0x0004E278
		internal int DefaultLCID
		{
			get
			{
				return this._defaultLCID;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00050080 File Offset: 0x0004E280
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x00050088 File Offset: 0x0004E288
		internal EncryptionOptions EncryptionOptions
		{
			get
			{
				return this._encryptionOption;
			}
			set
			{
				this._encryptionOption = value;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x00050091 File Offset: 0x0004E291
		internal bool Is2005OrNewer
		{
			get
			{
				return this._is2005;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00050099 File Offset: 0x0004E299
		internal bool Is2008OrNewer
		{
			get
			{
				return this._is2008;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x000500A1 File Offset: 0x0004E2A1
		internal bool MARSOn
		{
			get
			{
				return this._fMARS;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x000500A9 File Offset: 0x0004E2A9
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x000500B1 File Offset: 0x0004E2B1
		internal SqlInternalTransaction PendingTransaction
		{
			get
			{
				return this._pendingTransaction;
			}
			set
			{
				this._pendingTransaction = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x000500BA File Offset: 0x0004E2BA
		internal string Server
		{
			get
			{
				return this._server;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x000500C2 File Offset: 0x0004E2C2
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x000500CA File Offset: 0x0004E2CA
		internal TdsParserState State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x000500D3 File Offset: 0x0004E2D3
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x000500DB File Offset: 0x0004E2DB
		internal SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
			set
			{
				this._statistics = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x000500E4 File Offset: 0x0004E2E4
		private bool IncludeTraceHeader
		{
			get
			{
				return this._is2012 && SqlClientEventSource.Log.IsEnabled();
			}
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x000500FC File Offset: 0x0004E2FC
		internal int IncrementNonTransactedOpenResultCount()
		{
			return Interlocked.Increment(ref this._nonTransactedOpenResultCount);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00050116 File Offset: 0x0004E316
		internal void DecrementNonTransactedOpenResultCount()
		{
			Interlocked.Decrement(ref this._nonTransactedOpenResultCount);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00050124 File Offset: 0x0004E324
		internal void ProcessPendingAck(TdsParserStateObject stateObj)
		{
			if (stateObj._attentionSent)
			{
				this.ProcessAttention(stateObj);
			}
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00050138 File Offset: 0x0004E338
		internal void Connect(ServerInfo serverInfo, SqlInternalConnectionTds connHandler, bool ignoreSniOpenTimeout, long timerExpire, SqlConnectionString connectionOptions, bool withFailover, bool isFirstTransparentAttempt, ServerCertificateValidationCallback serverCallback, ClientCertificateRetrievalCallback clientCallback, bool useOriginalAddressInfo, bool disableTnir)
		{
			SqlConnectionEncryptOption sqlConnectionEncryptOption = connectionOptions.Encrypt;
			bool flag = sqlConnectionEncryptOption == SqlConnectionEncryptOption.Strict;
			bool flag2 = connectionOptions.TrustServerCertificate;
			bool integratedSecurity = connectionOptions.IntegratedSecurity;
			SqlAuthenticationMethod authentication = connectionOptions.Authentication;
			string certificate = connectionOptions.Certificate;
			string hostNameInCertificate = connectionOptions.HostNameInCertificate;
			string serverCertificate = connectionOptions.ServerCertificate;
			if (this._state != TdsParserState.Closed)
			{
				return;
			}
			this._connHandler = connHandler;
			this._loginWithFailover = withFailover;
			this._connHandler.IsSQLDNSCachingSupported = false;
			uint status = SNILoadHandle.SingletonInstance.Status;
			if (status != 0U)
			{
				this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
				this._physicalStateObj.Dispose();
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			if (connHandler.ConnectionOptions.LocalDBInstance != null)
			{
				LocalDBAPI.CreateLocalDBInstance(connHandler.ConnectionOptions.LocalDBInstance);
				if (sqlConnectionEncryptOption == SqlConnectionEncryptOption.Mandatory)
				{
					sqlConnectionEncryptOption = SqlConnectionEncryptOption.Optional;
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Encryption will be disabled as target server is a SQL Local DB instance.");
				}
			}
			if (integratedSecurity || authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated)
			{
				this.LoadSSPILibrary();
				if (!string.IsNullOrEmpty(serverInfo.ServerSPN))
				{
					byte[] bytes = Encoding.Unicode.GetBytes(serverInfo.ServerSPN);
					Trace.Assert(bytes.Length <= SNINativeMethodWrapper.SniMaxComposedSpnLength, "The provided SPN length exceeded the buffer size.");
					this._sniSpnBuffer = bytes;
					SqlClientEventSource.Log.TryTraceEvent<string>("<sc.TdsParser.Connect|SEC> Server SPN `{0}` from the connection string is used.", serverInfo.ServerSPN);
				}
				else
				{
					this._sniSpnBuffer = new byte[SNINativeMethodWrapper.SniMaxComposedSpnLength];
				}
				SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> SSPI or Active Directory Authentication Library for SQL Server based integrated authentication");
			}
			else
			{
				this._sniSpnBuffer = null;
				switch (authentication)
				{
				case SqlAuthenticationMethod.SqlPassword:
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> SQL Password authentication");
					break;
				case SqlAuthenticationMethod.ActiveDirectoryPassword:
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Active Directory Password authentication");
					break;
				case SqlAuthenticationMethod.ActiveDirectoryIntegrated:
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Active Directory Integrated authentication");
					break;
				case SqlAuthenticationMethod.ActiveDirectoryInteractive:
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Active Directory Interactive authentication");
					break;
				case SqlAuthenticationMethod.ActiveDirectoryServicePrincipal:
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Active Directory Service Principal authentication");
					break;
				case SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow:
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Active Directory Device Code Flow authentication");
					break;
				case SqlAuthenticationMethod.ActiveDirectoryManagedIdentity:
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Active Directory Managed Identity authentication");
					break;
				case SqlAuthenticationMethod.ActiveDirectoryMSI:
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Active Directory MSI authentication");
					break;
				case SqlAuthenticationMethod.ActiveDirectoryDefault:
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Active Directory Default authentication");
					break;
				default:
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> SQL authentication");
					break;
				}
			}
			if (flag)
			{
				flag2 = false;
			}
			byte[] array = null;
			this._connHandler.TimeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			this._connHandler.TimeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.InitializeConnection);
			bool multiSubnetFailover = this._connHandler.ConnectionOptions.MultiSubnetFailover;
			TransparentNetworkResolutionState transparentNetworkResolutionState;
			if (this._connHandler.ConnectionOptions.TransparentNetworkIPResolution && !disableTnir)
			{
				if (isFirstTransparentAttempt)
				{
					transparentNetworkResolutionState = TransparentNetworkResolutionState.SequentialMode;
				}
				else
				{
					transparentNetworkResolutionState = TransparentNetworkResolutionState.ParallelMode;
				}
			}
			else
			{
				transparentNetworkResolutionState = TransparentNetworkResolutionState.DisabledMode;
			}
			int connectTimeout = this._connHandler.ConnectionOptions.ConnectTimeout;
			this.FQDNforDNSCache = serverInfo.ResolvedServerName;
			int num = this.FQDNforDNSCache.IndexOf(",");
			if (num != -1)
			{
				this.FQDNforDNSCache = this.FQDNforDNSCache.Substring(0, num);
			}
			this._physicalStateObj.CreatePhysicalSNIHandle(serverInfo.ExtendedServerName, ignoreSniOpenTimeout, timerExpire, out array, this._sniSpnBuffer, false, true, multiSubnetFailover, transparentNetworkResolutionState, connectTimeout, this._connHandler.ConnectionOptions.IPAddressPreference, this.FQDNforDNSCache, hostNameInCertificate);
			if (this._physicalStateObj.Status != 0U)
			{
				this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
				this._physicalStateObj.Dispose();
				SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|ERR|SEC> Login failure");
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			this._server = serverInfo.ResolvedServerName;
			if (connHandler.PoolGroupProviderInfo != null)
			{
				connHandler.PoolGroupProviderInfo.AliasCheck((serverInfo.PreRoutingServerName == null) ? serverInfo.ResolvedServerName : serverInfo.PreRoutingServerName);
			}
			this._state = TdsParserState.OpenNotLoggedIn;
			this._physicalStateObj.SniContext = SniContext.Snix_PreLoginBeforeSuccessfulWrite;
			this._physicalStateObj.TimeoutTime = timerExpire;
			bool flag3 = false;
			this._connHandler.TimeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.InitializeConnection);
			this._connHandler.TimeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.SendPreLoginHandshake);
			uint num2 = SNINativeMethodWrapper.SniGetConnectionId(this._physicalStateObj.Handle, ref this._connHandler._clientConnectionId);
			this.AssignPendingDNSInfo(serverInfo.UserProtocol, this.FQDNforDNSCache);
			if (!this.ClientOSEncryptionSupport)
			{
				if (sqlConnectionEncryptOption != SqlConnectionEncryptOption.Optional)
				{
					this._physicalStateObj.AddError(new SqlError(20, 0, 20, this._server, SQLMessage.EncryptionNotSupportedByClient(), "", 0, null));
					this._physicalStateObj.Dispose();
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				this._encryptionOption = EncryptionOptions.NOT_SUP;
			}
			SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Sending prelogin handshake");
			this.SendPreLoginHandshake(array, sqlConnectionEncryptOption, integratedSecurity, !string.IsNullOrEmpty(certificate), useOriginalAddressInfo, serverCertificate, serverCallback, clientCallback);
			this._connHandler.TimeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.SendPreLoginHandshake);
			this._connHandler.TimeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake);
			this._physicalStateObj.SniContext = SniContext.Snix_PreLogin;
			SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Consuming prelogin handshake");
			PreLoginHandshakeStatus preLoginHandshakeStatus = this.ConsumePreLoginHandshake(authentication, sqlConnectionEncryptOption, flag2, integratedSecurity, serverCallback, clientCallback, out flag3, out this._connHandler._fedAuthRequired, flag, serverCertificate);
			if (preLoginHandshakeStatus == PreLoginHandshakeStatus.InstanceFailure)
			{
				SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Prelogin handshake unsuccessful. Reattempting prelogin handshake");
				this._physicalStateObj.Dispose();
				this._physicalStateObj.SniContext = SniContext.Snix_Connect;
				this._physicalStateObj.CreatePhysicalSNIHandle(serverInfo.ExtendedServerName, ignoreSniOpenTimeout, timerExpire, out array, this._sniSpnBuffer, true, true, multiSubnetFailover, transparentNetworkResolutionState, connectTimeout, this._connHandler.ConnectionOptions.IPAddressPreference, serverInfo.ResolvedServerName, hostNameInCertificate);
				if (this._physicalStateObj.Status != 0U)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|ERR|SEC> Login failure");
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				uint num3 = SNINativeMethodWrapper.SniGetConnectionId(this._physicalStateObj.Handle, ref this._connHandler._clientConnectionId);
				SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Sending prelogin handshake");
				this.AssignPendingDNSInfo(serverInfo.UserProtocol, this.FQDNforDNSCache);
				this.SendPreLoginHandshake(array, sqlConnectionEncryptOption, integratedSecurity, !string.IsNullOrEmpty(certificate), useOriginalAddressInfo, serverCertificate, serverCallback, clientCallback);
				preLoginHandshakeStatus = this.ConsumePreLoginHandshake(authentication, sqlConnectionEncryptOption, flag2, integratedSecurity, serverCallback, clientCallback, out flag3, out this._connHandler._fedAuthRequired, flag, serverCertificate);
				if (preLoginHandshakeStatus == PreLoginHandshakeStatus.InstanceFailure)
				{
					SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|ERR|SEC> Prelogin handshake unsuccessful. Login failure");
					throw SQL.InstanceFailure();
				}
			}
			SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.Connect|SEC> Prelogin handshake successful");
			if (this._fMARS && flag3)
			{
				this._sessionPool = new TdsParserSessionPool(this);
				return;
			}
			this._fMARS = false;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x000507C8 File Offset: 0x0004E9C8
		internal void AssignPendingDNSInfo(string userProtocol, string DNSCacheKey)
		{
			ushort num = 0;
			string empty = string.Empty;
			this.isTcpProtocol = false;
			SNINativeMethodWrapper.ProviderEnum providerEnum = SNINativeMethodWrapper.ProviderEnum.INVALID_PROV;
			if (string.IsNullOrEmpty(userProtocol))
			{
				uint num2 = SNINativeMethodWrapper.SniGetProviderNumber(this._physicalStateObj.Handle, ref providerEnum);
				this.isTcpProtocol = providerEnum == SNINativeMethodWrapper.ProviderEnum.TCP_PROV;
			}
			else if (userProtocol == "tcp")
			{
				this.isTcpProtocol = true;
			}
			if (this.isTcpProtocol)
			{
				uint num2 = SNINativeMethodWrapper.SniGetConnectionPort(this._physicalStateObj.Handle, ref num);
				num2 = SNINativeMethodWrapper.SniGetConnectionIPString(this._physicalStateObj.Handle, ref empty);
				this._connHandler.pendingSQLDNSObject = new SQLDNSInfo(DNSCacheKey, null, null, num.ToString());
				IPAddress ipaddress;
				if (IPAddress.TryParse(empty, out ipaddress))
				{
					if (AddressFamily.InterNetwork == ipaddress.AddressFamily)
					{
						this._connHandler.pendingSQLDNSObject.AddrIPv4 = empty;
						return;
					}
					if (AddressFamily.InterNetworkV6 == ipaddress.AddressFamily)
					{
						this._connHandler.pendingSQLDNSObject.AddrIPv6 = empty;
						return;
					}
				}
			}
			else
			{
				this._connHandler.pendingSQLDNSObject = null;
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x000508BC File Offset: 0x0004EABC
		internal void RemoveEncryption()
		{
			uint num = SNINativeMethodWrapper.SNIRemoveProvider(this._physicalStateObj.Handle, SNINativeMethodWrapper.ProviderEnum.SSL_PROV);
			if (num != 0U)
			{
				this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			try
			{
			}
			finally
			{
				this._physicalStateObj.ClearAllWritePackets();
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00050924 File Offset: 0x0004EB24
		internal void EnableMars()
		{
			if (this._fMARS)
			{
				this._pMarsPhysicalConObj = this._physicalStateObj;
				uint num = 0U;
				uint num2 = 0U;
				num = SNINativeMethodWrapper.SNIAddProvider(this._pMarsPhysicalConObj.Handle, SNINativeMethodWrapper.ProviderEnum.SMUX_PROV, ref num2);
				if (num != 0U)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				IntPtr zero = IntPtr.Zero;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this._pMarsPhysicalConObj.IncrementPendingCallbacks();
					num = SNINativeMethodWrapper.SNIReadAsync(this._pMarsPhysicalConObj.Handle, ref zero);
					if (zero != IntPtr.Zero)
					{
						SNINativeMethodWrapper.SNIPacketRelease(zero);
					}
				}
				if (997U != num)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				this._physicalStateObj = this.CreateSession();
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00050A14 File Offset: 0x0004EC14
		internal TdsParserStateObject CreateSession()
		{
			TdsParserStateObject tdsParserStateObject = new TdsParserStateObject(this, this._pMarsPhysicalConObj.Handle, true);
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParser.CreateSession|ADV> {0} created session {1}", this.ObjectID, tdsParserStateObject.ObjectID);
			return tdsParserStateObject;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00050A50 File Offset: 0x0004EC50
		internal TdsParserStateObject GetSession(object owner)
		{
			TdsParserStateObject tdsParserStateObject;
			if (this.MARSOn)
			{
				tdsParserStateObject = this._sessionPool.GetSession(owner);
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParser.GetSession|ADV> {0} getting session {1} from pool", this.ObjectID, tdsParserStateObject.ObjectID);
			}
			else
			{
				tdsParserStateObject = this._physicalStateObj;
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParser.GetSession|ADV> {0} getting physical session {1}", this.ObjectID, tdsParserStateObject.ObjectID);
			}
			return tdsParserStateObject;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00050AB4 File Offset: 0x0004ECB4
		internal void PutSession(TdsParserStateObject session)
		{
			if (this.MARSOn)
			{
				this._sessionPool.PutSession(session);
				return;
			}
			if (this._state == TdsParserState.Closed || this._state == TdsParserState.Broken)
			{
				this._physicalStateObj.SniContext = SniContext.Snix_Close;
				this._physicalStateObj.Dispose();
				return;
			}
			this._physicalStateObj.Owner = null;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00050B0C File Offset: 0x0004ED0C
		internal void BestEffortCleanup()
		{
			this._state = TdsParserState.Broken;
			TdsParserStateObject physicalStateObj = this._physicalStateObj;
			if (physicalStateObj != null)
			{
				SNIHandle handle = physicalStateObj.Handle;
				if (handle != null)
				{
					handle.Dispose();
				}
			}
			if (this._fMARS)
			{
				TdsParserSessionPool sessionPool = this._sessionPool;
				if (sessionPool != null)
				{
					sessionPool.BestEffortCleanup();
				}
				TdsParserStateObject pMarsPhysicalConObj = this._pMarsPhysicalConObj;
				if (pMarsPhysicalConObj != null)
				{
					SNIHandle handle2 = pMarsPhysicalConObj.Handle;
					if (handle2 != null)
					{
						handle2.Dispose();
					}
				}
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00050B70 File Offset: 0x0004ED70
		private void SendPreLoginHandshake(byte[] instanceName, SqlConnectionEncryptOption encrypt, bool integratedSecurity, bool clientCertificate, bool useCtaip, string serverCertificate, ServerCertificateValidationCallback serverCallback, ClientCertificateRetrievalCallback clientCallback)
		{
			if (encrypt == SqlConnectionEncryptOption.Strict)
			{
				uint num = 16387U;
				this.EnableSsl(num, encrypt, integratedSecurity, serverCertificate, serverCallback, clientCallback);
				this._encryptionOption = EncryptionOptions.NOT_SUP;
			}
			this._physicalStateObj._outputMessageType = 18;
			int num2 = 36;
			byte[] array = new byte[1059];
			int num3 = 0;
			for (int i = 0; i < 7; i++)
			{
				int num4 = 0;
				this._physicalStateObj.WriteByte((byte)i);
				this._physicalStateObj.WriteByte((byte)((num2 & 65280) >> 8));
				this._physicalStateObj.WriteByte((byte)(num2 & 255));
				switch (i)
				{
				case 0:
				{
					Version assemblyVersion = ADP.GetAssemblyVersion();
					array[num3++] = (byte)(assemblyVersion.Major & 255);
					array[num3++] = (byte)(assemblyVersion.Minor & 255);
					array[num3++] = (byte)((assemblyVersion.Build & 65280) >> 8);
					array[num3++] = (byte)(assemblyVersion.Build & 255);
					array[num3++] = (byte)(assemblyVersion.Revision & 255);
					array[num3++] = (byte)((assemblyVersion.Revision & 65280) >> 8);
					num2 += 6;
					num4 = 6;
					break;
				}
				case 1:
					if (this._encryptionOption == EncryptionOptions.NOT_SUP)
					{
						array[num3] = 2;
					}
					else
					{
						if (encrypt == SqlConnectionEncryptOption.Mandatory)
						{
							array[num3] = 1;
							this._encryptionOption = EncryptionOptions.ON;
						}
						else
						{
							array[num3] = 0;
							this._encryptionOption = EncryptionOptions.OFF;
						}
						if (clientCertificate)
						{
							byte[] array2 = array;
							int num5 = num3;
							array2[num5] |= 128;
							this._encryptionOption |= EncryptionOptions.CLIENT_CERT;
						}
					}
					if (useCtaip)
					{
						byte[] array3 = array;
						int num6 = num3;
						array3[num6] |= 64;
						this._encryptionOption |= EncryptionOptions.CTAIP;
					}
					num3++;
					num2++;
					num4 = 1;
					break;
				case 2:
				{
					int num7 = 0;
					while (instanceName[num7] != 0)
					{
						array[num3] = instanceName[num7];
						num3++;
						num7++;
					}
					array[num3] = 0;
					num3++;
					num7++;
					num2 += num7;
					num4 = num7;
					break;
				}
				case 3:
				{
					int currentThreadIdForTdsLoginOnly = TdsParserStaticMethods.GetCurrentThreadIdForTdsLoginOnly();
					array[num3++] = (byte)(((ulong)(-16777216) & (ulong)((long)currentThreadIdForTdsLoginOnly)) >> 24);
					array[num3++] = (byte)((16711680 & currentThreadIdForTdsLoginOnly) >> 16);
					array[num3++] = (byte)((65280 & currentThreadIdForTdsLoginOnly) >> 8);
					array[num3++] = (byte)(255 & currentThreadIdForTdsLoginOnly);
					num2 += 4;
					num4 = 4;
					break;
				}
				case 4:
					array[num3++] = ((this._fMARS > false) ? 1 : 0);
					num2++;
					num4++;
					break;
				case 5:
				{
					byte[] array4 = this._connHandler._clientConnectionId.ToByteArray();
					Buffer.BlockCopy(array4, 0, array, num3, 16);
					num3 += 16;
					num2 += 16;
					num4 = 16;
					ActivityCorrelator.ActivityId activityId = ActivityCorrelator.Next();
					array4 = activityId.Id.ToByteArray();
					Buffer.BlockCopy(array4, 0, array, num3, 16);
					num3 += 16;
					array[num3++] = (byte)(255U & activityId.Sequence);
					array[num3++] = (byte)((65280U & activityId.Sequence) >> 8);
					array[num3++] = (byte)((16711680U & activityId.Sequence) >> 16);
					array[num3++] = (byte)((4278190080U & activityId.Sequence) >> 24);
					int num8 = 20;
					num2 += num8;
					num4 += num8;
					SqlClientEventSource.Log.TryTraceEvent<Guid, ActivityCorrelator.ActivityId>("<sc.TdsParser.SendPreLoginHandshake|INFO> ClientConnectionID {0}, ActivityID {1}", this._connHandler._clientConnectionId, activityId);
					break;
				}
				case 6:
					array[num3++] = 1;
					num2++;
					num4++;
					break;
				}
				this._physicalStateObj.WriteByte((byte)((num4 & 65280) >> 8));
				this._physicalStateObj.WriteByte((byte)(num4 & 255));
			}
			this._physicalStateObj.WriteByte(byte.MaxValue);
			this._physicalStateObj.WriteByteArray(array, num3, 0, true, null);
			this._physicalStateObj.WritePacket(1, false);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00050F54 File Offset: 0x0004F154
		private void EnableSsl(uint info, SqlConnectionEncryptOption encrypt, bool integratedSecurity, string serverCertificate, ServerCertificateValidationCallback serverCallback, ClientCertificateRetrievalCallback clientCallback)
		{
			if (encrypt && !integratedSecurity)
			{
				info |= 16U;
			}
			SNINativeMethodWrapper.AuthProviderInfo authProviderInfo = default(SNINativeMethodWrapper.AuthProviderInfo);
			authProviderInfo.flags = info;
			authProviderInfo.tlsFirst = encrypt == SqlConnectionEncryptOption.Strict;
			authProviderInfo.certId = null;
			authProviderInfo.certHash = false;
			authProviderInfo.clientCertificateCallbackContext = IntPtr.Zero;
			authProviderInfo.clientCertificateCallback = null;
			authProviderInfo.serverCertFileName = (string.IsNullOrEmpty(serverCertificate) ? null : serverCertificate);
			if ((this._encryptionOption & EncryptionOptions.CLIENT_CERT) != EncryptionOptions.OFF)
			{
				string certificate = this._connHandler.ConnectionOptions.Certificate;
				if (certificate.StartsWith("subject:", StringComparison.OrdinalIgnoreCase))
				{
					authProviderInfo.certId = certificate.Substring(8);
				}
				else if (certificate.StartsWith("sha1:", StringComparison.OrdinalIgnoreCase))
				{
					authProviderInfo.certId = certificate.Substring(5);
					authProviderInfo.certHash = true;
				}
				if (clientCallback != null)
				{
					authProviderInfo.clientCertificateCallbackContext = clientCallback;
					authProviderInfo.clientCertificateCallback = TdsParser._clientCertificateCallback;
				}
			}
			uint num = SNINativeMethodWrapper.SNIAddProvider(this._physicalStateObj.Handle, SNINativeMethodWrapper.ProviderEnum.SSL_PROV, authProviderInfo);
			if (num != 0U)
			{
				this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			uint num2;
			num = SNINativeMethodWrapper.SNIWaitForSSLHandshakeToComplete(this._physicalStateObj.Handle, this._physicalStateObj.GetTimeoutRemaining(), out num2);
			if (num != 0U)
			{
				this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			string protocolWarning = SslProtocolsHelper.GetProtocolWarning(num2);
			if (!string.IsNullOrEmpty(protocolWarning))
			{
				if (!encrypt && LocalAppContextSwitches.SuppressInsecureTLSWarning)
				{
					SqlClientEventSource.Log.TryTraceEvent<string, string, SqlClientLogger.LogLevel, string>("<sc|{0}|{1}|{2}>{3}", "TdsParser", "EnableSsl", SqlClientLogger.LogLevel.Warning, protocolWarning);
				}
				else
				{
					this._logger.LogWarning("TdsParser", "EnableSsl", protocolWarning);
				}
			}
			if (serverCallback != null)
			{
				X509Certificate2 x509Certificate = null;
				num = SNINativeMethodWrapper.SNISecGetServerCertificate(this._physicalStateObj.Handle, ref x509Certificate);
				if (num != 0U)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				if (!serverCallback(x509Certificate))
				{
					throw SQL.InvalidServerCertificate();
				}
			}
			this._physicalStateObj.ClearAllWritePackets();
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00051184 File Offset: 0x0004F384
		private PreLoginHandshakeStatus ConsumePreLoginHandshake(SqlAuthenticationMethod authType, SqlConnectionEncryptOption encrypt, bool trustServerCert, bool integratedSecurity, ServerCertificateValidationCallback serverCallback, ClientCertificateRetrievalCallback clientCallback, out bool marsCapable, out bool fedAuthRequired, bool tlsFirst, string serverCertificateFilename)
		{
			marsCapable = this._fMARS;
			fedAuthRequired = false;
			bool flag = false;
			if (!this._physicalStateObj.TryReadNetworkPacket())
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			if (this._physicalStateObj._inBytesRead == 0)
			{
				this._physicalStateObj.AddError(new SqlError(0, 0, 20, this._server, SQLMessage.PreloginError(), "", 0, null));
				this._physicalStateObj.Dispose();
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			if (!this._physicalStateObj.TryProcessHeader())
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			if (this._physicalStateObj._inBytesPacket > 32768 || this._physicalStateObj._inBytesPacket <= 0)
			{
				throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
			}
			byte[] array = new byte[this._physicalStateObj._inBytesPacket];
			if (!this._physicalStateObj.TryReadByteArray(array, array.Length))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			if (array[0] == 170)
			{
				throw SQL.InvalidSQLServerVersionUnknown();
			}
			int num = 0;
			int num2 = (int)array[num++];
			bool flag2 = false;
			bool flag3 = false;
			while (num2 != 255)
			{
				switch (num2)
				{
				case 0:
				{
					int num3 = ((int)array[num++] << 8) | (int)array[num++];
					int num4 = ((int)array[num++] << 8) | (int)array[num++];
					byte b = array[num3];
					byte b2 = array[num3 + 1];
					int num5 = ((int)array[num3 + 2] << 8) | (int)array[num3 + 3];
					flag = b >= 9;
					if (!flag)
					{
						marsCapable = false;
					}
					break;
				}
				case 1:
					if (tlsFirst)
					{
						num += 4;
					}
					else
					{
						int num3 = ((int)array[num++] << 8) | (int)array[num++];
						int num4 = ((int)array[num++] << 8) | (int)array[num++];
						EncryptionOptions encryptionOptions = (EncryptionOptions)array[num3];
						flag2 = (encryptionOptions & EncryptionOptions.OPTIONS_MASK) != EncryptionOptions.NOT_SUP;
						EncryptionOptions encryptionOptions2 = this._encryptionOption & EncryptionOptions.OPTIONS_MASK;
						if (encryptionOptions2 != EncryptionOptions.OFF)
						{
							if (encryptionOptions2 != EncryptionOptions.NOT_SUP)
							{
								if ((encryptionOptions & EncryptionOptions.OPTIONS_MASK) == EncryptionOptions.NOT_SUP)
								{
									this._physicalStateObj.AddError(new SqlError(20, 0, 20, this._server, SQLMessage.EncryptionNotSupportedByServer(), "", 0, null));
									this._physicalStateObj.Dispose();
									this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
								}
							}
							else if ((encryptionOptions & EncryptionOptions.OPTIONS_MASK) == EncryptionOptions.REQ)
							{
								this._physicalStateObj.AddError(new SqlError(20, 0, 20, this._server, SQLMessage.EncryptionNotSupportedByClient(), "", 0, null));
								this._physicalStateObj.Dispose();
								this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
							}
						}
						else if ((encryptionOptions & EncryptionOptions.OPTIONS_MASK) == EncryptionOptions.OFF)
						{
							this._encryptionOption = EncryptionOptions.LOGIN | (this._encryptionOption & (EncryptionOptions)(-64));
						}
						else if ((encryptionOptions & EncryptionOptions.OPTIONS_MASK) == EncryptionOptions.REQ)
						{
							this._encryptionOption = EncryptionOptions.ON | (this._encryptionOption & (EncryptionOptions)(-64));
						}
						flag3 = (encryptionOptions & EncryptionOptions.CTAIP) > EncryptionOptions.OFF;
					}
					break;
				case 2:
				{
					int num3 = ((int)array[num++] << 8) | (int)array[num++];
					int num4 = ((int)array[num++] << 8) | (int)array[num++];
					byte b3 = 1;
					byte b4 = array[num3];
					if (b4 == b3)
					{
						return PreLoginHandshakeStatus.InstanceFailure;
					}
					break;
				}
				case 3:
					num += 4;
					break;
				case 4:
				{
					int num3 = ((int)array[num++] << 8) | (int)array[num++];
					int num4 = ((int)array[num++] << 8) | (int)array[num++];
					marsCapable = array[num3] > 0;
					break;
				}
				case 5:
					num += 4;
					break;
				case 6:
				{
					int num3 = ((int)array[num++] << 8) | (int)array[num++];
					int num4 = ((int)array[num++] << 8) | (int)array[num++];
					if (array[num3] != 0 && array[num3] != 1)
					{
						SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.TdsParser.ConsumePreLoginHandshake|ERR> {0}, Server sent an unexpected value for FedAuthRequired PreLogin Option. Value was {1}.", this.ObjectID, (int)array[num3]);
						throw SQL.ParsingErrorValue(ParsingErrorState.FedAuthRequiredPreLoginResponseInvalidValue, (int)array[num3]);
					}
					if ((this._connHandler.ConnectionOptions != null && this._connHandler.ConnectionOptions.Authentication != SqlAuthenticationMethod.NotSpecified) || this._connHandler._accessTokenInBytes != null)
					{
						fedAuthRequired = array[num3] == 1;
					}
					break;
				}
				default:
					num += 4;
					break;
				}
				if (num >= array.Length)
				{
					break;
				}
				num2 = (int)array[num++];
			}
			if ((this._encryptionOption & EncryptionOptions.CTAIP) != EncryptionOptions.OFF && !flag3)
			{
				this._physicalStateObj.AddError(new SqlError(21, 0, 20, this._server, SQLMessage.CTAIPNotSupportedByServer(), "", 0, null));
				this._physicalStateObj.Dispose();
				this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
			}
			if ((this._encryptionOption & EncryptionOptions.OPTIONS_MASK) == EncryptionOptions.ON || (this._encryptionOption & EncryptionOptions.OPTIONS_MASK) == EncryptionOptions.LOGIN)
			{
				if (!flag2)
				{
					this._physicalStateObj.AddError(new SqlError(20, 0, 20, this._server, SQLMessage.EncryptionNotSupportedByServer(), "", 0, null));
					this._physicalStateObj.Dispose();
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				if (serverCallback != null)
				{
					trustServerCert = true;
				}
				bool flag4 = (this._encryptionOption == EncryptionOptions.ON && !trustServerCert) || ((authType != SqlAuthenticationMethod.NotSpecified || this._connHandler._accessTokenInBytes != null) && !trustServerCert);
				uint num6 = ((flag4 > false) ? 1U : 0U) | ((flag && (this._encryptionOption & EncryptionOptions.CLIENT_CERT) == EncryptionOptions.OFF) ? 2U : 0U);
				this.EnableSsl(num6, encrypt, integratedSecurity, serverCertificateFilename, serverCallback, clientCallback);
			}
			return PreLoginHandshakeStatus.Successful;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0005169C File Offset: 0x0004F89C
		internal void Deactivate(bool connectionIsDoomed)
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.TdsParser.Deactivate|ADV> {0} deactivating", this.ObjectID);
			if (SqlClientEventSource.Log.IsStateDumpEnabled())
			{
				SqlClientEventSource.Log.StateDumpEvent<int, string>("<sc.TdsParser.Deactivate|STATE> {0}, {1}", this.ObjectID, this.TraceString());
			}
			if (this.MARSOn)
			{
				this._sessionPool.Deactivate();
			}
			if (!connectionIsDoomed && this._physicalStateObj != null)
			{
				if (this._physicalStateObj._pendingData)
				{
					this.DrainData(this._physicalStateObj);
				}
				if (this._physicalStateObj.HasOpenResult)
				{
					this._physicalStateObj.DecrementOpenResultCount();
				}
			}
			SqlInternalTransaction currentTransaction = this.CurrentTransaction;
			if (currentTransaction != null && currentTransaction.HasParentTransaction)
			{
				currentTransaction.CloseFromConnection();
			}
			this.Statistics = null;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00051754 File Offset: 0x0004F954
		internal void Disconnect()
		{
			if (this._sessionPool != null)
			{
				this._sessionPool.Dispose();
			}
			if (this._state != TdsParserState.Closed)
			{
				this._state = TdsParserState.Closed;
				try
				{
					if (!this._physicalStateObj.HasOwner)
					{
						this._physicalStateObj.SniContext = SniContext.Snix_Close;
						this._physicalStateObj.Dispose();
					}
					else
					{
						this._physicalStateObj.DecrementPendingCallbacks(false);
					}
					if (this._pMarsPhysicalConObj != null)
					{
						this._pMarsPhysicalConObj.Dispose();
					}
				}
				finally
				{
					this._pMarsPhysicalConObj = null;
				}
			}
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x000517E4 File Offset: 0x0004F9E4
		private void FireInfoMessageEvent(SqlConnection connection, TdsParserStateObject stateObj, SqlError error)
		{
			string text = null;
			if (this._state == TdsParserState.OpenLoggedIn)
			{
				text = this._connHandler.ServerVersion;
			}
			SqlException ex = SqlException.CreateException(new SqlErrorCollection { error }, text, this._connHandler, null);
			bool flag;
			connection.OnInfoMessage(new SqlInfoMessageEventArgs(ex), out flag);
			if (flag)
			{
				stateObj._syncOverAsync = true;
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0005183C File Offset: 0x0004FA3C
		internal void DisconnectTransaction(SqlInternalTransaction internalTransaction)
		{
			if (this._currentTransaction != null && this._currentTransaction == internalTransaction)
			{
				this._currentTransaction = null;
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00051858 File Offset: 0x0004FA58
		internal void RollbackOrphanedAPITransactions()
		{
			SqlInternalTransaction currentTransaction = this.CurrentTransaction;
			if (currentTransaction != null && currentTransaction.HasParentTransaction && currentTransaction.IsOrphaned)
			{
				currentTransaction.CloseFromConnection();
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00051888 File Offset: 0x0004FA88
		internal void ThrowExceptionAndWarning(TdsParserStateObject stateObj, bool callerHasConnectionLock = false, bool asyncClose = false)
		{
			SqlException ex = null;
			bool flag;
			SqlErrorCollection fullErrorAndWarningCollection = stateObj.GetFullErrorAndWarningCollection(out flag);
			if (fullErrorAndWarningCollection.Count == 0)
			{
				SqlClientEventSource.Log.TryTraceEvent<int>("<sc.TdsParser.ThrowExceptionAndWarning|ERR> Potential multi-threaded misuse of connection, unexpectedly empty warnings/errors under lock {0}", this.ObjectID);
			}
			flag &= this._state > TdsParserState.Closed;
			if (flag)
			{
				if (this._state == TdsParserState.OpenNotLoggedIn && (this._connHandler.ConnectionOptions.TransparentNetworkIPResolution || this._connHandler.ConnectionOptions.MultiSubnetFailover || this._loginWithFailover) && fullErrorAndWarningCollection.Count == 1 && (fullErrorAndWarningCollection[0].Number == -2 || (long)fullErrorAndWarningCollection[0].Number == 258L))
				{
					flag = false;
					this.Disconnect();
				}
				else
				{
					this._state = TdsParserState.Broken;
				}
			}
			if (fullErrorAndWarningCollection != null && fullErrorAndWarningCollection.Count > 0)
			{
				string text = null;
				if (this._state == TdsParserState.OpenLoggedIn)
				{
					text = this._connHandler.ServerVersion;
				}
				ex = SqlException.CreateException(fullErrorAndWarningCollection, text, this._connHandler, null);
				if (ex.Procedure == "InitSSPIPackage")
				{
					ex._doNotReconnect = true;
				}
			}
			if (ex != null)
			{
				if (flag)
				{
					TaskCompletionSource<object> networkPacketTaskSource = stateObj._networkPacketTaskSource;
					if (networkPacketTaskSource != null)
					{
						networkPacketTaskSource.TrySetException(ADP.ExceptionWithStackTrace(ex));
					}
				}
				if (asyncClose)
				{
					SqlInternalConnectionTds connHandler = this._connHandler;
					Action<Action> action = delegate(Action closeAction)
					{
						Task.Factory.StartNew(delegate
						{
							connHandler._parserLock.Wait(false);
							connHandler.ThreadHasParserLockForClose = true;
							try
							{
								closeAction();
							}
							finally
							{
								connHandler.ThreadHasParserLockForClose = false;
								connHandler._parserLock.Release();
							}
						});
					};
					this._connHandler.OnError(ex, flag, action);
					return;
				}
				bool threadHasParserLockForClose = this._connHandler.ThreadHasParserLockForClose;
				if (callerHasConnectionLock)
				{
					this._connHandler.ThreadHasParserLockForClose = true;
				}
				try
				{
					this._connHandler.OnError(ex, flag, null);
				}
				finally
				{
					if (callerHasConnectionLock)
					{
						this._connHandler.ThreadHasParserLockForClose = threadHasParserLockForClose;
					}
				}
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00051A34 File Offset: 0x0004FC34
		internal SqlError ProcessSNIError(TdsParserStateObject stateObj)
		{
			SNINativeMethodWrapper.SNI_Error sni_Error = default(SNINativeMethodWrapper.SNI_Error);
			SNINativeMethodWrapper.SNIGetLastError(out sni_Error);
			if (sni_Error.sniError != 0U)
			{
				switch (sni_Error.sniError)
				{
				case 47U:
					throw SQL.MultiSubnetFailoverWithMoreThan64IPs();
				case 48U:
					throw SQL.MultiSubnetFailoverWithInstanceSpecified();
				case 49U:
					throw SQL.MultiSubnetFailoverWithNonTcpProtocol();
				}
			}
			string text = (string.IsNullOrEmpty(sni_Error.errorMessage) ? string.Empty : sni_Error.errorMessage);
			string @string = StringsHelper.GetString(Enum.GetName(typeof(SniContext), stateObj.SniContext), Array.Empty<object>());
			string text2 = string.Format("SNI_PN{0}", (int)sni_Error.provider);
			string string2 = StringsHelper.GetString(text2, Array.Empty<object>());
			uint num = sni_Error.nativeError;
			if (sni_Error.sniError == 0U)
			{
				int num2 = text.IndexOf(':');
				if (0 <= num2)
				{
					int num3 = text.Length;
					num3 -= 2;
					num2 += 2;
					num3 -= num2;
					if (num3 > 0)
					{
						text = text.Substring(num2, num3);
					}
				}
			}
			else
			{
				text = SQL.GetSNIErrorMessage((int)sni_Error.sniError);
				if (sni_Error.sniError == 50U)
				{
					text += LocalDBAPI.GetLocalDBMessage((int)sni_Error.nativeError);
					num = 0U;
				}
			}
			text = string.Format("{0} (provider: {1}, error: {2} - {3})", new object[]
			{
				@string,
				string2,
				(int)sni_Error.sniError,
				text
			});
			return new SqlError((int)sni_Error.nativeError, 0, 20, this._server, text, sni_Error.function, (int)sni_Error.lineNumber, num, null);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00051BB4 File Offset: 0x0004FDB4
		internal void CheckResetConnection(TdsParserStateObject stateObj)
		{
			if (this._fResetConnection && !stateObj._fResetConnectionSent)
			{
				try
				{
					if (this._fMARS && !stateObj._fResetEventOwned)
					{
						stateObj._fResetEventOwned = this._resetConnectionEvent.WaitOne(stateObj.GetTimeoutRemaining(), false);
						if (stateObj._fResetEventOwned && stateObj.TimeoutHasExpired)
						{
							stateObj._fResetEventOwned = !this._resetConnectionEvent.Set();
							stateObj.TimeoutTime = 0L;
						}
						if (!stateObj._fResetEventOwned)
						{
							stateObj.ResetBuffer();
							stateObj.AddError(new SqlError(-2, 0, 11, this._server, this._connHandler.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
							this.ThrowExceptionAndWarning(stateObj, true, false);
						}
					}
					if (this._fResetConnection)
					{
						if (this._fPreserveTransaction)
						{
							stateObj._outBuff[1] = stateObj._outBuff[1] | 16;
						}
						else
						{
							stateObj._outBuff[1] = stateObj._outBuff[1] | 8;
						}
						if (!this._fMARS)
						{
							this._fResetConnection = false;
							this._fPreserveTransaction = false;
						}
						else
						{
							stateObj._fResetConnectionSent = true;
						}
					}
					else if (this._fMARS && stateObj._fResetEventOwned)
					{
						stateObj._fResetEventOwned = !this._resetConnectionEvent.Set();
					}
				}
				catch (Exception)
				{
					if (this._fMARS && stateObj._fResetEventOwned)
					{
						stateObj._fResetConnectionSent = false;
						stateObj._fResetEventOwned = !this._resetConnectionEvent.Set();
					}
					throw;
				}
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00051D68 File Offset: 0x0004FF68
		internal byte[] SerializeShort(int v, TdsParserStateObject stateObj)
		{
			if (stateObj._bShortBytes == null)
			{
				stateObj._bShortBytes = new byte[2];
			}
			byte[] bShortBytes = stateObj._bShortBytes;
			int num = 0;
			bShortBytes[num++] = (byte)(v & 255);
			bShortBytes[num++] = (byte)((v >> 8) & 255);
			return bShortBytes;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00051DB4 File Offset: 0x0004FFB4
		internal void WriteShort(int v, TdsParserStateObject stateObj)
		{
			if (stateObj._outBytesUsed + 2 > stateObj._outBuff.Length)
			{
				stateObj.WriteByte((byte)(v & 255));
				stateObj.WriteByte((byte)((v >> 8) & 255));
				return;
			}
			stateObj._outBuff[stateObj._outBytesUsed] = (byte)(v & 255);
			stateObj._outBuff[stateObj._outBytesUsed + 1] = (byte)((v >> 8) & 255);
			stateObj._outBytesUsed += 2;
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00051E2E File Offset: 0x0005002E
		internal void WriteUnsignedShort(ushort us, TdsParserStateObject stateObj)
		{
			this.WriteShort((int)((short)us), stateObj);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00051E39 File Offset: 0x00050039
		internal byte[] SerializeUnsignedInt(uint i, TdsParserStateObject stateObj)
		{
			return this.SerializeInt((int)i, stateObj);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00051E43 File Offset: 0x00050043
		internal void WriteUnsignedInt(uint i, TdsParserStateObject stateObj)
		{
			this.WriteInt((int)i, stateObj);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00051E50 File Offset: 0x00050050
		internal byte[] SerializeInt(int v, TdsParserStateObject stateObj)
		{
			if (stateObj._bIntBytes == null)
			{
				stateObj._bIntBytes = new byte[4];
			}
			int num = 0;
			byte[] bIntBytes = stateObj._bIntBytes;
			bIntBytes[num++] = (byte)(v & 255);
			bIntBytes[num++] = (byte)((v >> 8) & 255);
			bIntBytes[num++] = (byte)((v >> 16) & 255);
			bIntBytes[num++] = (byte)((v >> 24) & 255);
			return bIntBytes;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00051EC0 File Offset: 0x000500C0
		internal void WriteInt(int v, TdsParserStateObject stateObj)
		{
			if (stateObj._outBytesUsed + 4 > stateObj._outBuff.Length)
			{
				for (int i = 0; i < 32; i += 8)
				{
					stateObj.WriteByte((byte)((v >> i) & 255));
				}
				return;
			}
			stateObj._outBuff[stateObj._outBytesUsed] = (byte)(v & 255);
			stateObj._outBuff[stateObj._outBytesUsed + 1] = (byte)((v >> 8) & 255);
			stateObj._outBuff[stateObj._outBytesUsed + 2] = (byte)((v >> 16) & 255);
			stateObj._outBuff[stateObj._outBytesUsed + 3] = (byte)((v >> 24) & 255);
			stateObj._outBytesUsed += 4;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00051F70 File Offset: 0x00050170
		internal byte[] SerializeFloat(float v)
		{
			if (float.IsInfinity(v) || float.IsNaN(v))
			{
				throw ADP.ParameterValueOutOfRange(v.ToString());
			}
			return BitConverter.GetBytes(v);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00051F98 File Offset: 0x00050198
		internal void WriteFloat(float v, TdsParserStateObject stateObj)
		{
			byte[] bytes = BitConverter.GetBytes(v);
			stateObj.WriteByteArray(bytes, bytes.Length, 0, true, null);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00051FBC File Offset: 0x000501BC
		internal byte[] SerializeLong(long v, TdsParserStateObject stateObj)
		{
			int num = 0;
			if (stateObj._bLongBytes == null)
			{
				stateObj._bLongBytes = new byte[8];
			}
			byte[] bLongBytes = stateObj._bLongBytes;
			bLongBytes[num++] = (byte)(v & 255L);
			bLongBytes[num++] = (byte)((v >> 8) & 255L);
			bLongBytes[num++] = (byte)((v >> 16) & 255L);
			bLongBytes[num++] = (byte)((v >> 24) & 255L);
			bLongBytes[num++] = (byte)((v >> 32) & 255L);
			bLongBytes[num++] = (byte)((v >> 40) & 255L);
			bLongBytes[num++] = (byte)((v >> 48) & 255L);
			bLongBytes[num++] = (byte)((v >> 56) & 255L);
			return bLongBytes;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0005207C File Offset: 0x0005027C
		internal void WriteLong(long v, TdsParserStateObject stateObj)
		{
			if (stateObj._outBytesUsed + 8 > stateObj._outBuff.Length)
			{
				for (int i = 0; i < 64; i += 8)
				{
					stateObj.WriteByte((byte)((v >> i) & 255L));
				}
				return;
			}
			stateObj._outBuff[stateObj._outBytesUsed] = (byte)(v & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 1] = (byte)((v >> 8) & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 2] = (byte)((v >> 16) & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 3] = (byte)((v >> 24) & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 4] = (byte)((v >> 32) & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 5] = (byte)((v >> 40) & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 6] = (byte)((v >> 48) & 255L);
			stateObj._outBuff[stateObj._outBytesUsed + 7] = (byte)((v >> 56) & 255L);
			stateObj._outBytesUsed += 8;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000521A0 File Offset: 0x000503A0
		internal byte[] SerializePartialLong(long v, int length)
		{
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = (byte)((v >> i * 8) & 255L);
			}
			return array;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000521D4 File Offset: 0x000503D4
		internal void WritePartialLong(long v, int length, TdsParserStateObject stateObj)
		{
			if (stateObj._outBytesUsed + length > stateObj._outBuff.Length)
			{
				for (int i = 0; i < length * 8; i += 8)
				{
					stateObj.WriteByte((byte)((v >> i) & 255L));
				}
				return;
			}
			for (int j = 0; j < length; j++)
			{
				stateObj._outBuff[stateObj._outBytesUsed + j] = (byte)((v >> j * 8) & 255L);
			}
			stateObj._outBytesUsed += length;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0005224F File Offset: 0x0005044F
		internal void WriteUnsignedLong(ulong uv, TdsParserStateObject stateObj)
		{
			this.WriteLong((long)uv, stateObj);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00052259 File Offset: 0x00050459
		internal byte[] SerializeDouble(double v)
		{
			if (double.IsInfinity(v) || double.IsNaN(v))
			{
				throw ADP.ParameterValueOutOfRange(v.ToString());
			}
			return BitConverter.GetBytes(v);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00052280 File Offset: 0x00050480
		internal void WriteDouble(double v, TdsParserStateObject stateObj)
		{
			byte[] bytes = BitConverter.GetBytes(v);
			stateObj.WriteByteArray(bytes, bytes.Length, 0, true, null);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x000522A2 File Offset: 0x000504A2
		internal void PrepareResetConnection(bool preserveTransaction)
		{
			this._fResetConnection = true;
			this._fPreserveTransaction = preserveTransaction;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x000522B8 File Offset: 0x000504B8
		internal bool RunReliably(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag;
			try
			{
				flag = this.Run(runBehavior, cmdHandler, dataStream, bulkCopyHandler, stateObj);
			}
			catch (OutOfMemoryException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			return flag;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00052330 File Offset: 0x00050530
		internal bool Run(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj)
		{
			bool syncOverAsync = stateObj._syncOverAsync;
			bool flag3;
			try
			{
				stateObj._syncOverAsync = true;
				bool flag2;
				bool flag = this.TryRun(runBehavior, cmdHandler, dataStream, bulkCopyHandler, stateObj, out flag2);
				flag3 = flag2;
			}
			finally
			{
				stateObj._syncOverAsync = syncOverAsync;
			}
			return flag3;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0005237C File Offset: 0x0005057C
		internal static bool IsValidTdsToken(byte token)
		{
			return token == 170 || token == 171 || token == 173 || token == 227 || token == 172 || token == 121 || token == 160 || token == 161 || token == 162 || token == 163 || token == 129 || token == 136 || token == 164 || token == 165 || token == 169 || token == 211 || token == 209 || token == 210 || token == 253 || token == 254 || token == byte.MaxValue || token == 57 || token == 237 || token == 124 || token == 120 || token == 237 || token == 174 || token == 228 || token == 238;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0005248C File Offset: 0x0005068C
		internal bool TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, out bool dataReady)
		{
			if (TdsParserState.Broken == this.State || this.State == TdsParserState.Closed)
			{
				dataReady = true;
				return true;
			}
			dataReady = false;
			byte b;
			for (;;)
			{
				if (stateObj.IsTimeoutStateExpired)
				{
					runBehavior = RunBehavior.Attention;
				}
				if (TdsParserState.Broken == this.State || this.State == TdsParserState.Closed)
				{
					goto IL_0A16;
				}
				if (!stateObj._accumulateInfoEvents && stateObj._pendingInfoEvents != null)
				{
					if (RunBehavior.Clean != (RunBehavior.Clean & runBehavior))
					{
						SqlConnection sqlConnection = null;
						if (this._connHandler != null)
						{
							sqlConnection = this._connHandler.Connection;
						}
						if (sqlConnection != null && sqlConnection.FireInfoMessageEventOnUserErrors)
						{
							using (List<SqlError>.Enumerator enumerator = stateObj._pendingInfoEvents.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									SqlError sqlError = enumerator.Current;
									this.FireInfoMessageEvent(sqlConnection, stateObj, sqlError);
								}
								goto IL_0123;
							}
						}
						foreach (SqlError sqlError2 in stateObj._pendingInfoEvents)
						{
							stateObj.AddWarning(sqlError2);
						}
					}
					IL_0123:
					stateObj._pendingInfoEvents = null;
				}
				if (!stateObj.TryReadByte(out b))
				{
					break;
				}
				if (!TdsParser.IsValidTdsToken(b))
				{
					goto Block_14;
				}
				int num;
				if (!this.TryGetTokenLength(b, stateObj, out num))
				{
					return false;
				}
				if (b <= 210)
				{
					if (b <= 129)
					{
						if (b != 121)
						{
							if (b == 129)
							{
								if (num != 65535)
								{
									_SqlMetaDataSet sqlMetaDataSet;
									if (!this.TryProcessMetaData(num, stateObj, out sqlMetaDataSet, (cmdHandler != null) ? cmdHandler.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting))
									{
										return false;
									}
									stateObj._cleanupMetaData = sqlMetaDataSet;
								}
								else if (cmdHandler != null)
								{
									stateObj._cleanupMetaData = cmdHandler.MetaData;
								}
								byte b2;
								if (!stateObj.TryPeekByte(out b2))
								{
									return false;
								}
								if (163 == b2)
								{
									byte b3;
									if (!stateObj.TryReadByte(out b3))
									{
										return false;
									}
									SensitivityClassification sensitivityClassification;
									if (!this.TryProcessDataClassification(stateObj, out sensitivityClassification))
									{
										return false;
									}
									if (dataStream != null && !dataStream.TrySetSensitivityClassification(sensitivityClassification))
									{
										return false;
									}
									if (!stateObj.TryPeekByte(out b2))
									{
										return false;
									}
								}
								if (dataStream != null)
								{
									if (!dataStream.TrySetMetaData(stateObj._cleanupMetaData, 164 == b2 || 165 == b2))
									{
										return false;
									}
								}
								else if (bulkCopyHandler != null)
								{
									bulkCopyHandler.SetMetaData(stateObj._cleanupMetaData);
								}
							}
						}
						else
						{
							int num2;
							if (!stateObj.TryReadInt32(out num2))
							{
								return false;
							}
							if (cmdHandler != null)
							{
								cmdHandler.OnReturnStatus(num2);
							}
						}
					}
					else if (b != 136)
					{
						switch (b)
						{
						case 162:
							if (!this.TryProcessResColSrcs(stateObj, num))
							{
								return false;
							}
							break;
						case 163:
						case 166:
						case 167:
						case 168:
							break;
						case 164:
							if (dataStream != null)
							{
								MultiPartTableName[] array;
								if (!this.TryProcessTableName(num, stateObj, out array))
								{
									return false;
								}
								dataStream.TableNames = array;
							}
							else if (!stateObj.TrySkipBytes(num))
							{
								return false;
							}
							break;
						case 165:
							if (dataStream != null)
							{
								_SqlMetaDataSet sqlMetaDataSet2;
								if (!this.TryProcessColInfo(dataStream.MetaData, dataStream, stateObj, out sqlMetaDataSet2))
								{
									return false;
								}
								if (!dataStream.TrySetMetaData(sqlMetaDataSet2, false))
								{
									return false;
								}
								dataStream.BrowseModeInfoConsumed = true;
							}
							else if (!stateObj.TrySkipBytes(num))
							{
								return false;
							}
							break;
						case 169:
							if (!stateObj.TrySkipBytes(num))
							{
								return false;
							}
							break;
						case 170:
						case 171:
						{
							if (b == 170)
							{
								stateObj._errorTokenReceived = true;
							}
							SqlError sqlError3;
							if (!this.TryProcessError(b, stateObj, out sqlError3))
							{
								return false;
							}
							if (b == 171 && stateObj._accumulateInfoEvents)
							{
								if (stateObj._pendingInfoEvents == null)
								{
									stateObj._pendingInfoEvents = new List<SqlError>();
								}
								stateObj._pendingInfoEvents.Add(sqlError3);
								stateObj._syncOverAsync = true;
							}
							else if (RunBehavior.Clean != (RunBehavior.Clean & runBehavior))
							{
								SqlConnection sqlConnection2 = null;
								if (this._connHandler != null)
								{
									sqlConnection2 = this._connHandler.Connection;
								}
								if (sqlConnection2 != null && sqlConnection2.FireInfoMessageEventOnUserErrors && sqlError3.Class <= 16)
								{
									this.FireInfoMessageEvent(sqlConnection2, stateObj, sqlError3);
								}
								else if (sqlError3.Class < 11)
								{
									stateObj.AddWarning(sqlError3);
								}
								else if (sqlError3.Class < 20)
								{
									stateObj.AddError(sqlError3);
									if (dataStream != null && !dataStream.IsInitialized)
									{
										runBehavior = RunBehavior.UntilDone;
									}
								}
								else
								{
									stateObj.AddError(sqlError3);
									runBehavior = RunBehavior.UntilDone;
								}
							}
							else if (sqlError3.Class >= 20)
							{
								stateObj.AddError(sqlError3);
							}
							break;
						}
						case 172:
						{
							SqlReturnValue sqlReturnValue;
							if (!this.TryProcessReturnValue(num, stateObj, out sqlReturnValue, (cmdHandler != null) ? cmdHandler.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting))
							{
								return false;
							}
							if (cmdHandler != null)
							{
								cmdHandler.OnReturnValue(sqlReturnValue, stateObj);
							}
							break;
						}
						case 173:
						{
							SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.TryRun|SEC> Received login acknowledgement token");
							SqlLoginAck sqlLoginAck;
							if (!this.TryProcessLoginAck(stateObj, out sqlLoginAck))
							{
								return false;
							}
							this._connHandler.OnLoginAck(sqlLoginAck);
							break;
						}
						case 174:
							if (!this.TryProcessFeatureExtAck(stateObj))
							{
								return false;
							}
							break;
						default:
							if (b - 209 <= 1)
							{
								if (b == 210)
								{
									if (!stateObj.TryStartNewRow(true, stateObj._cleanupMetaData.Length))
									{
										return false;
									}
								}
								else if (!stateObj.TryStartNewRow(false, 0))
								{
									return false;
								}
								if (bulkCopyHandler != null)
								{
									if (!this.TryProcessRow(stateObj._cleanupMetaData, bulkCopyHandler.CreateRowBuffer(), bulkCopyHandler.CreateIndexMap(), stateObj))
									{
										return false;
									}
								}
								else if (RunBehavior.ReturnImmediately != (RunBehavior.ReturnImmediately & runBehavior))
								{
									if (!this.TrySkipRow(stateObj._cleanupMetaData, stateObj))
									{
										return false;
									}
								}
								else
								{
									dataReady = true;
								}
								if (this._statistics != null)
								{
									this._statistics.WaitForDoneAfterRow = true;
								}
							}
							break;
						}
					}
					else
					{
						stateObj.CloneCleanupAltMetaDataSetArray();
						if (stateObj._cleanupAltMetaDataSetArray == null)
						{
							stateObj._cleanupAltMetaDataSetArray = new _SqlMetaDataSetCollection();
						}
						_SqlMetaDataSet sqlMetaDataSet3;
						if (!this.TryProcessAltMetaData(num, stateObj, out sqlMetaDataSet3))
						{
							return false;
						}
						stateObj._cleanupAltMetaDataSetArray.SetAltMetaData(sqlMetaDataSet3);
						if (dataStream != null)
						{
							byte b4;
							if (!stateObj.TryPeekByte(out b4))
							{
								return false;
							}
							if (!dataStream.TrySetAltMetaDataSet(sqlMetaDataSet3, 136 != b4))
							{
								return false;
							}
						}
					}
				}
				else if (b <= 228)
				{
					if (b != 211)
					{
						if (b != 227)
						{
							if (b == 228)
							{
								if (!this.TryProcessSessionState(stateObj, num, this._connHandler._currentSessionData))
								{
									return false;
								}
							}
						}
						else
						{
							stateObj._syncOverAsync = true;
							SqlEnvChange next;
							if (!this.TryProcessEnvChange(num, stateObj, out next))
							{
								return false;
							}
							while (next != null)
							{
								if (!this.Connection.IgnoreEnvChange)
								{
									switch (next._type)
									{
									case 8:
									case 11:
										this._currentTransaction = this._pendingTransaction;
										this._pendingTransaction = null;
										if (this._currentTransaction != null)
										{
											this._currentTransaction.TransactionId = next._newLongValue;
										}
										else
										{
											TransactionType transactionType = ((8 == next._type) ? TransactionType.LocalFromTSQL : TransactionType.Distributed);
											this._currentTransaction = new SqlInternalTransaction(this._connHandler, transactionType, null, next._newLongValue);
										}
										if (this._statistics != null && !this._statisticsIsInTransaction)
										{
											this._statistics.SafeIncrement(ref this._statistics._transactions);
										}
										this._statisticsIsInTransaction = true;
										this._retainedTransactionId = 0L;
										goto IL_06C4;
									case 9:
									case 12:
									case 17:
										this._retainedTransactionId = 0L;
										break;
									case 10:
										break;
									case 13:
									case 14:
									case 15:
									case 16:
										goto IL_06B7;
									default:
										goto IL_06B7;
									}
									if (this._currentTransaction != null)
									{
										if (9 == next._type)
										{
											this._currentTransaction.Completed(TransactionState.Committed);
										}
										else if (10 == next._type)
										{
											if (this._currentTransaction.IsDistributed && this._currentTransaction.IsActive)
											{
												this._retainedTransactionId = next._oldLongValue;
											}
											this._currentTransaction.Completed(TransactionState.Aborted);
										}
										else
										{
											this._currentTransaction.Completed(TransactionState.Unknown);
										}
										this._currentTransaction = null;
									}
									this._statisticsIsInTransaction = false;
									goto IL_06C4;
									IL_06B7:
									this._connHandler.OnEnvChange(next);
								}
								IL_06C4:
								SqlEnvChange sqlEnvChange = next;
								next = next._next;
								sqlEnvChange.Clear();
							}
						}
					}
					else
					{
						if (!stateObj.TryStartNewRow(false, 0))
						{
							return false;
						}
						if (RunBehavior.ReturnImmediately != (RunBehavior.ReturnImmediately & runBehavior))
						{
							ushort num3;
							if (!stateObj.TryReadUInt16(out num3))
							{
								return false;
							}
							if (!this.TrySkipRow(stateObj._cleanupAltMetaDataSetArray.GetAltMetaData((int)num3), stateObj))
							{
								return false;
							}
						}
						else
						{
							dataReady = true;
						}
					}
				}
				else if (b != 237)
				{
					if (b != 238)
					{
						if (b - 253 <= 2)
						{
							if (!this.TryProcessDone(cmdHandler, dataStream, ref runBehavior, stateObj))
							{
								return false;
							}
							if (b == 254 && cmdHandler != null)
							{
								if (cmdHandler.IsDescribeParameterEncryptionRPCCurrentlyInProgress)
								{
									cmdHandler.OnDoneDescribeParameterEncryptionProc(stateObj);
								}
								else
								{
									cmdHandler.OnDoneProc();
								}
							}
						}
					}
					else
					{
						this._connHandler._federatedAuthenticationInfoReceived = true;
						SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.TryRun|SEC> Received federated authentication info token");
						SqlFedAuthInfo sqlFedAuthInfo;
						if (!this.TryProcessFedAuthInfo(stateObj, num, out sqlFedAuthInfo))
						{
							return false;
						}
						this._connHandler.OnFedAuthInfo(sqlFedAuthInfo);
					}
				}
				else
				{
					stateObj._syncOverAsync = true;
					this.ProcessSSPI(num);
				}
				if ((!stateObj._pendingData || RunBehavior.ReturnImmediately == (RunBehavior.ReturnImmediately & runBehavior)) && (stateObj._pendingData || !stateObj._attentionSent || stateObj._attentionReceived))
				{
					goto IL_0A16;
				}
			}
			return false;
			Block_14:
			this._state = TdsParserState.Broken;
			this._connHandler.BreakConnection();
			SqlClientEventSource.Log.TryTraceEvent<int>("<sc.TdsParser.Run|ERR> Potential multi-threaded misuse of connection, unexpected TDS token found {0}", this.ObjectID);
			throw SQL.ParsingErrorToken(ParsingErrorState.InvalidTdsTokenReceived, (int)b);
			IL_0A16:
			if (!stateObj._pendingData && this.CurrentTransaction != null)
			{
				this.CurrentTransaction.Activate();
			}
			if (stateObj._attentionReceived)
			{
				SpinWait.SpinUntil(() => !stateObj._attentionSending);
				if (stateObj._attentionSent)
				{
					stateObj._attentionSent = false;
					stateObj._attentionReceived = false;
					if (RunBehavior.Clean != (RunBehavior.Clean & runBehavior) && !stateObj.IsTimeoutStateExpired)
					{
						stateObj.AddError(new SqlError(0, 0, 11, this._server, SQLMessage.OperationCancelled(), "", 0, null));
					}
				}
			}
			if (stateObj.HasErrorOrWarning)
			{
				this.ThrowExceptionAndWarning(stateObj, false, false);
			}
			return true;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00052F88 File Offset: 0x00051188
		private bool TryProcessEnvChange(int tokenLength, TdsParserStateObject stateObj, out SqlEnvChange sqlEnvChange)
		{
			int num = 0;
			SqlEnvChange sqlEnvChange2 = null;
			SqlEnvChange sqlEnvChange3 = null;
			sqlEnvChange = null;
			while (tokenLength > num)
			{
				SqlEnvChange sqlEnvChange4 = new SqlEnvChange();
				if (!stateObj.TryReadByte(out sqlEnvChange4._type))
				{
					return false;
				}
				if (sqlEnvChange2 == null)
				{
					sqlEnvChange2 = sqlEnvChange4;
					sqlEnvChange3 = sqlEnvChange4;
				}
				else
				{
					sqlEnvChange3._next = sqlEnvChange4;
					sqlEnvChange3 = sqlEnvChange4;
				}
				switch (sqlEnvChange4._type)
				{
				case 1:
				case 2:
					if (!this.TryReadTwoStringFields(sqlEnvChange4, stateObj))
					{
						return false;
					}
					break;
				case 3:
					if (!this.TryReadTwoStringFields(sqlEnvChange4, stateObj))
					{
						return false;
					}
					if (sqlEnvChange4._newValue == "iso_1")
					{
						this._defaultCodePage = 1252;
						this._defaultEncoding = Encoding.GetEncoding(this._defaultCodePage);
					}
					else
					{
						string text = sqlEnvChange4._newValue.Substring(2);
						this._defaultCodePage = int.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
						this._defaultEncoding = Encoding.GetEncoding(this._defaultCodePage);
					}
					break;
				case 4:
				{
					if (!this.TryReadTwoStringFields(sqlEnvChange4, stateObj))
					{
						throw SQL.SynchronousCallMayNotPend();
					}
					int num2 = int.Parse(sqlEnvChange4._newValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
					if (this._physicalStateObj.SetPacketSize(num2))
					{
						this._physicalStateObj.ClearAllWritePackets();
						uint num3 = (uint)num2;
						uint num4 = SNINativeMethodWrapper.SNISetInfo(this._physicalStateObj.Handle, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_BUFSIZE, ref num3);
					}
					break;
				}
				case 5:
					if (!this.TryReadTwoStringFields(sqlEnvChange4, stateObj))
					{
						return false;
					}
					this._defaultLCID = int.Parse(sqlEnvChange4._newValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
					break;
				case 6:
					if (!this.TryReadTwoStringFields(sqlEnvChange4, stateObj))
					{
						return false;
					}
					break;
				case 7:
				{
					byte b;
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					sqlEnvChange4._newLength = (int)b;
					if (sqlEnvChange4._newLength == 5)
					{
						if (!this.TryProcessCollation(stateObj, out sqlEnvChange4._newCollation))
						{
							return false;
						}
						this._defaultCollation = sqlEnvChange4._newCollation;
						this._defaultLCID = sqlEnvChange4._newCollation.LCID;
						if (sqlEnvChange4._newCollation.IsUTF8)
						{
							this._defaultEncoding = Encoding.UTF8;
						}
						else
						{
							int codePage = this.GetCodePage(sqlEnvChange4._newCollation, stateObj);
							if (codePage != this._defaultCodePage)
							{
								this._defaultCodePage = codePage;
								this._defaultEncoding = Encoding.GetEncoding(this._defaultCodePage);
							}
						}
					}
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					sqlEnvChange4._oldLength = b;
					if (sqlEnvChange4._oldLength == 5 && !this.TryProcessCollation(stateObj, out sqlEnvChange4._oldCollation))
					{
						return false;
					}
					sqlEnvChange4._length = 3 + sqlEnvChange4._newLength + (int)sqlEnvChange4._oldLength;
					break;
				}
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
				case 17:
				{
					byte b;
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					sqlEnvChange4._newLength = (int)b;
					if (sqlEnvChange4._newLength > 0)
					{
						if (!stateObj.TryReadInt64(out sqlEnvChange4._newLongValue))
						{
							return false;
						}
					}
					else
					{
						sqlEnvChange4._newLongValue = 0L;
					}
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					sqlEnvChange4._oldLength = b;
					if (sqlEnvChange4._oldLength > 0)
					{
						if (!stateObj.TryReadInt64(out sqlEnvChange4._oldLongValue))
						{
							return false;
						}
					}
					else
					{
						sqlEnvChange4._oldLongValue = 0L;
					}
					sqlEnvChange4._length = 3 + sqlEnvChange4._newLength + (int)sqlEnvChange4._oldLength;
					break;
				}
				case 13:
					if (!this.TryReadTwoStringFields(sqlEnvChange4, stateObj))
					{
						return false;
					}
					break;
				case 15:
				{
					if (!stateObj.TryReadInt32(out sqlEnvChange4._newLength))
					{
						return false;
					}
					sqlEnvChange4._newBinValue = new byte[sqlEnvChange4._newLength];
					if (!stateObj.TryReadByteArray(sqlEnvChange4._newBinValue, sqlEnvChange4._newLength))
					{
						return false;
					}
					byte b;
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					sqlEnvChange4._oldLength = b;
					sqlEnvChange4._length = 5 + sqlEnvChange4._newLength;
					break;
				}
				case 16:
				case 18:
					if (!this.TryReadTwoBinaryFields(sqlEnvChange4, stateObj))
					{
						return false;
					}
					break;
				case 19:
					if (!this.TryReadTwoStringFields(sqlEnvChange4, stateObj))
					{
						return false;
					}
					break;
				case 20:
				{
					ushort num5;
					if (!stateObj.TryReadUInt16(out num5))
					{
						return false;
					}
					sqlEnvChange4._newLength = (int)num5;
					byte b2;
					if (!stateObj.TryReadByte(out b2))
					{
						return false;
					}
					ushort num6;
					if (!stateObj.TryReadUInt16(out num6))
					{
						return false;
					}
					ushort num7;
					if (!stateObj.TryReadUInt16(out num7))
					{
						return false;
					}
					string text2;
					if (!stateObj.TryReadString((int)num7, out text2))
					{
						return false;
					}
					sqlEnvChange4._newRoutingInfo = new RoutingInfo(b2, num6, text2);
					ushort num8;
					if (!stateObj.TryReadUInt16(out num8))
					{
						return false;
					}
					if (!stateObj.TrySkipBytes((int)num8))
					{
						return false;
					}
					sqlEnvChange4._length = sqlEnvChange4._newLength + (int)num8 + 5;
					break;
				}
				}
				num += sqlEnvChange4._length;
			}
			sqlEnvChange = sqlEnvChange2;
			return true;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00053408 File Offset: 0x00051608
		private bool TryReadTwoBinaryFields(SqlEnvChange env, TdsParserStateObject stateObj)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			env._newLength = (int)b;
			env._newBinValue = ArrayPool<byte>.Shared.Rent(env._newLength);
			env._newBinRented = true;
			if (!stateObj.TryReadByteArray(env._newBinValue, env._newLength))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			env._oldLength = b;
			env._oldBinValue = ArrayPool<byte>.Shared.Rent((int)env._oldLength);
			env._oldBinRented = true;
			if (!stateObj.TryReadByteArray(env._oldBinValue, (int)env._oldLength))
			{
				return false;
			}
			env._length = 3 + env._newLength + (int)env._oldLength;
			return true;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x000534C4 File Offset: 0x000516C4
		private bool TryReadTwoStringFields(SqlEnvChange env, TdsParserStateObject stateObj)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			string text;
			if (!stateObj.TryReadString((int)b, out text))
			{
				return false;
			}
			byte b2;
			if (!stateObj.TryReadByte(out b2))
			{
				return false;
			}
			string text2;
			if (!stateObj.TryReadString((int)b2, out text2))
			{
				return false;
			}
			env._newLength = (int)b;
			env._newValue = text;
			env._oldLength = b2;
			env._oldValue = text2;
			env._length = 3 + env._newLength * 2 + (int)(env._oldLength * 2);
			return true;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0005353C File Offset: 0x0005173C
		private bool TryProcessDone(SqlCommand cmd, SqlDataReader reader, ref RunBehavior run, TdsParserStateObject stateObj)
		{
			if (LocalAppContextSwitches.MakeReadAsyncBlocking)
			{
				stateObj._syncOverAsync = true;
			}
			ushort num;
			if (!stateObj.TryReadUInt16(out num))
			{
				return false;
			}
			ushort num2;
			if (!stateObj.TryReadUInt16(out num2))
			{
				return false;
			}
			int num4;
			if (this._is2005)
			{
				long num3;
				if (!stateObj.TryReadInt64(out num3))
				{
					return false;
				}
				num4 = (int)num3;
			}
			else
			{
				if (!stateObj.TryReadInt32(out num4))
				{
					return false;
				}
				if (this._state == TdsParserState.OpenNotLoggedIn && stateObj._inBytesRead > stateObj._inBytesUsed)
				{
					byte b;
					if (!stateObj.TryPeekByte(out b))
					{
						return false;
					}
					if (b == 0 && !stateObj.TryReadInt32(out num4))
					{
						return false;
					}
				}
			}
			if (32 == (num & 32))
			{
				stateObj._attentionReceived = true;
			}
			if (cmd != null && 16 == (num & 16))
			{
				if (num2 != 193)
				{
					if (cmd.IsDescribeParameterEncryptionRPCCurrentlyInProgress)
					{
						cmd.RowsAffectedByDescribeParameterEncryption = num4;
					}
					else
					{
						cmd.InternalRecordsAffected = num4;
					}
				}
				if (stateObj._receivedColMetaData || num2 != 193)
				{
					cmd.OnStatementCompleted(num4);
				}
			}
			stateObj._receivedColMetaData = false;
			if (2 == (2 & num) && stateObj.ErrorCount == 0 && !stateObj._errorTokenReceived && RunBehavior.Clean != (RunBehavior.Clean & run))
			{
				stateObj.AddError(new SqlError(0, 0, 11, this._server, SQLMessage.SevereError(), "", 0, null));
				if (reader != null && !reader.IsInitialized)
				{
					run = RunBehavior.UntilDone;
				}
			}
			if (256 == (256 & num) && RunBehavior.Clean != (RunBehavior.Clean & run))
			{
				stateObj.AddError(new SqlError(0, 0, 20, this._server, SQLMessage.SevereError(), "", 0, null));
				if (reader != null && !reader.IsInitialized)
				{
					run = RunBehavior.UntilDone;
				}
			}
			this.ProcessSqlStatistics(num2, num, num4);
			if (1 != (num & 1))
			{
				stateObj._errorTokenReceived = false;
				if (stateObj._inBytesUsed >= stateObj._inBytesRead)
				{
					stateObj._pendingData = false;
				}
			}
			if (!stateObj._pendingData && stateObj._hasOpenResult)
			{
				stateObj.DecrementOpenResultCount();
			}
			return true;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00053708 File Offset: 0x00051908
		private void ProcessSqlStatistics(ushort curCmd, ushort status, int count)
		{
			if (this._statistics != null)
			{
				if (this._statistics.WaitForDoneAfterRow)
				{
					this._statistics.SafeIncrement(ref this._statistics._sumResultSets);
					this._statistics.WaitForDoneAfterRow = false;
				}
				if (16 != (status & 16))
				{
					count = 0;
				}
				if (curCmd <= 193)
				{
					if (curCmd == 32)
					{
						this._statistics.SafeIncrement(ref this._statistics._cursorOpens);
						return;
					}
					if (curCmd != 193)
					{
						return;
					}
					this._statistics.SafeIncrement(ref this._statistics._selectCount);
					this._statistics.SafeAdd(ref this._statistics._selectRows, (long)count);
					return;
				}
				else
				{
					if (curCmd - 195 > 2)
					{
						switch (curCmd)
						{
						case 210:
							this._statisticsIsInTransaction = false;
							return;
						case 211:
							return;
						case 212:
							if (!this._statisticsIsInTransaction)
							{
								this._statistics.SafeIncrement(ref this._statistics._transactions);
							}
							this._statisticsIsInTransaction = true;
							return;
						case 213:
							this._statisticsIsInTransaction = false;
							return;
						default:
							if (curCmd != 279)
							{
								return;
							}
							break;
						}
					}
					this._statistics.SafeIncrement(ref this._statistics._iduCount);
					this._statistics.SafeAdd(ref this._statistics._iduRows, (long)count);
					if (!this._statisticsIsInTransaction)
					{
						this._statistics.SafeIncrement(ref this._statistics._transactions);
						return;
					}
				}
			}
			else
			{
				switch (curCmd)
				{
				case 210:
				case 213:
					this._statisticsIsInTransaction = false;
					break;
				case 211:
					break;
				case 212:
					this._statisticsIsInTransaction = true;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x000538A8 File Offset: 0x00051AA8
		private bool TryProcessFeatureExtAck(TdsParserStateObject stateObj)
		{
			byte b;
			while (stateObj.TryReadByte(out b))
			{
				if (b != 255)
				{
					uint num;
					if (!stateObj.TryReadUInt32(out num))
					{
						return false;
					}
					byte[] array = new byte[num];
					if (num > 0U && !stateObj.TryReadByteArray(array, checked((int)num)))
					{
						return false;
					}
					this._connHandler.OnFeatureExtAck((int)b, array);
				}
				if (b == 255)
				{
					if (this._connHandler._cleanSQLDNSCaching)
					{
						bool flag = SQLFallbackDNSCache.Instance.DeleteDNSInfo(this.FQDNforDNSCache);
					}
					if (this._connHandler.IsSQLDNSCachingSupported && this._connHandler.pendingSQLDNSObject != null && !SQLFallbackDNSCache.Instance.IsDuplicate(this._connHandler.pendingSQLDNSObject))
					{
						bool flag = SQLFallbackDNSCache.Instance.AddDNSInfo(this._connHandler.pendingSQLDNSObject);
						this._connHandler.pendingSQLDNSObject = null;
					}
					if (this.Connection.RoutingInfo == null && this._connHandler.ConnectionOptions.ColumnEncryptionSetting == SqlConnectionColumnEncryptionSetting.Enabled && !this.IsColumnEncryptionSupported)
					{
						throw SQL.TceNotSupported();
					}
					if (this.Connection.RoutingInfo == null)
					{
						SqlConnectionAttestationProtocol attestationProtocol = this._connHandler.ConnectionOptions.AttestationProtocol;
						if (this.TceVersionSupported < 2)
						{
							if (!string.IsNullOrWhiteSpace(this._connHandler.ConnectionOptions.EnclaveAttestationUrl) && attestationProtocol != SqlConnectionAttestationProtocol.NotSpecified)
							{
								throw SQL.EnclaveComputationsNotSupported();
							}
							if (!string.IsNullOrWhiteSpace(this._connHandler.ConnectionOptions.EnclaveAttestationUrl))
							{
								throw SQL.AttestationURLNotSupported();
							}
							if (this._connHandler.ConnectionOptions.AttestationProtocol != SqlConnectionAttestationProtocol.NotSpecified)
							{
								throw SQL.AttestationProtocolNotSupported();
							}
						}
						if (!string.IsNullOrWhiteSpace(this._connHandler.ConnectionOptions.EnclaveAttestationUrl) || attestationProtocol == SqlConnectionAttestationProtocol.None)
						{
							if (string.IsNullOrWhiteSpace(this.EnclaveType))
							{
								throw SQL.EnclaveTypeNotReturned();
							}
							if (attestationProtocol != SqlConnectionAttestationProtocol.NotSpecified && !this.IsValidAttestationProtocol(attestationProtocol, this.EnclaveType))
							{
								throw SQL.AttestationProtocolNotSupportEnclaveType(attestationProtocol.ToString(), this.EnclaveType);
							}
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00053A88 File Offset: 0x00051C88
		private bool IsValidAttestationProtocol(SqlConnectionAttestationProtocol attestationProtocol, string enclaveType)
		{
			string text = enclaveType.ToUpper();
			if (!(text == "VBS"))
			{
				if (!(text == "SGX"))
				{
					throw SQL.EnclaveTypeNotSupported(enclaveType);
				}
				if (attestationProtocol != SqlConnectionAttestationProtocol.AAS)
				{
					return false;
				}
			}
			else if (attestationProtocol != SqlConnectionAttestationProtocol.AAS && attestationProtocol != SqlConnectionAttestationProtocol.HGS && attestationProtocol != SqlConnectionAttestationProtocol.None)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00053AD4 File Offset: 0x00051CD4
		private bool TryReadByteString(TdsParserStateObject stateObj, out string value)
		{
			value = string.Empty;
			byte b;
			return stateObj.TryReadByte(out b) && stateObj.TryReadString((int)b, out value);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00053B01 File Offset: 0x00051D01
		private bool TryReadSensitivityLabel(TdsParserStateObject stateObj, out string label, out string id)
		{
			label = string.Empty;
			id = string.Empty;
			return this.TryReadByteString(stateObj, out label) && this.TryReadByteString(stateObj, out id);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00053B01 File Offset: 0x00051D01
		private bool TryReadSensitivityInformationType(TdsParserStateObject stateObj, out string informationType, out string id)
		{
			informationType = string.Empty;
			id = string.Empty;
			return this.TryReadByteString(stateObj, out informationType) && this.TryReadByteString(stateObj, out id);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00053B2C File Offset: 0x00051D2C
		private bool TryProcessDataClassification(TdsParserStateObject stateObj, out SensitivityClassification sensitivityClassification)
		{
			if (this.DataClassificationVersion == 0)
			{
				throw SQL.ParsingError(ParsingErrorState.DataClassificationNotExpected);
			}
			sensitivityClassification = null;
			ushort num;
			if (!stateObj.TryReadUInt16(out num))
			{
				return false;
			}
			List<Label> list = new List<Label>((int)num);
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				string text;
				string text2;
				if (!this.TryReadSensitivityLabel(stateObj, out text, out text2))
				{
					return false;
				}
				list.Add(new Label(text, text2));
			}
			ushort num3;
			if (!stateObj.TryReadUInt16(out num3))
			{
				return false;
			}
			List<InformationType> list2 = new List<InformationType>((int)num3);
			for (ushort num4 = 0; num4 < num3; num4 += 1)
			{
				string text3;
				string text4;
				if (!this.TryReadSensitivityInformationType(stateObj, out text3, out text4))
				{
					return false;
				}
				list2.Add(new InformationType(text3, text4));
			}
			int num5 = -1;
			if (this.DataClassificationVersion > 1 && (!stateObj.TryReadInt32(out num5) || !Enum.IsDefined(typeof(SensitivityRank), num5)))
			{
				return false;
			}
			ushort num6;
			if (!stateObj.TryReadUInt16(out num6))
			{
				return false;
			}
			List<ColumnSensitivity> list3 = new List<ColumnSensitivity>((int)num6);
			for (ushort num7 = 0; num7 < num6; num7 += 1)
			{
				ushort num8;
				if (!stateObj.TryReadUInt16(out num8))
				{
					return false;
				}
				List<SensitivityProperty> list4 = new List<SensitivityProperty>((int)num8);
				for (ushort num9 = 0; num9 < num8; num9 += 1)
				{
					ushort num10;
					if (!stateObj.TryReadUInt16(out num10))
					{
						return false;
					}
					Label label = null;
					if (num10 != 65535)
					{
						if ((int)num10 >= list.Count)
						{
							throw SQL.ParsingError(ParsingErrorState.DataClassificationInvalidLabelIndex);
						}
						label = list[(int)num10];
					}
					ushort num11;
					if (!stateObj.TryReadUInt16(out num11))
					{
						return false;
					}
					InformationType informationType = null;
					if (num11 != 65535)
					{
						if ((int)num11 >= list2.Count)
						{
							throw SQL.ParsingError(ParsingErrorState.DataClassificationInvalidInformationTypeIndex);
						}
						informationType = list2[(int)num11];
					}
					int num12 = -1;
					if (this.DataClassificationVersion > 1 && (!stateObj.TryReadInt32(out num12) || !Enum.IsDefined(typeof(SensitivityRank), num12)))
					{
						return false;
					}
					list4.Add(new SensitivityProperty(label, informationType, (SensitivityRank)num12));
				}
				list3.Add(new ColumnSensitivity(list4));
			}
			sensitivityClassification = new SensitivityClassification(list, list2, list3, (SensitivityRank)num5);
			return true;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00053D20 File Offset: 0x00051F20
		private bool TryProcessResColSrcs(TdsParserStateObject stateObj, int tokenLength)
		{
			return stateObj.TrySkipBytes(tokenLength);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00053D30 File Offset: 0x00051F30
		private bool TryProcessSessionState(TdsParserStateObject stateObj, int length, SessionData sdata)
		{
			if (length < 5)
			{
				throw SQL.ParsingErrorLength(ParsingErrorState.SessionStateLengthTooShort, length);
			}
			uint num;
			if (!stateObj.TryReadUInt32(out num))
			{
				return false;
			}
			if (num == 4294967295U)
			{
				this._connHandler.DoNotPoolThisConnection();
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			if (b > 1)
			{
				throw SQL.ParsingErrorStatus(ParsingErrorState.SessionStateInvalidStatus, (int)b);
			}
			bool flag = b > 0;
			length -= 5;
			while (length > 0)
			{
				byte b2;
				if (!stateObj.TryReadByte(out b2))
				{
					return false;
				}
				byte b3;
				if (!stateObj.TryReadByte(out b3))
				{
					return false;
				}
				int num2;
				if (b3 < 255)
				{
					num2 = (int)b3;
				}
				else if (!stateObj.TryReadInt32(out num2))
				{
					return false;
				}
				byte[] array = null;
				SessionStateRecord[] delta = sdata._delta;
				checked
				{
					lock (delta)
					{
						if (sdata._delta[(int)b2] == null)
						{
							array = new byte[num2];
							sdata._delta[(int)b2] = new SessionStateRecord
							{
								_version = num,
								_dataLength = num2,
								_data = array,
								_recoverable = flag
							};
							sdata._deltaDirty = true;
							if (!flag)
							{
								sdata._unrecoverableStatesCount += 1;
							}
						}
						else if (sdata._delta[(int)b2]._version <= num)
						{
							SessionStateRecord sessionStateRecord = sdata._delta[(int)b2];
							sessionStateRecord._version = num;
							sessionStateRecord._dataLength = num2;
							if (sessionStateRecord._recoverable != flag)
							{
								if (flag)
								{
									unchecked
									{
										sdata._unrecoverableStatesCount -= 1;
									}
								}
								else
								{
									sdata._unrecoverableStatesCount += 1;
								}
								sessionStateRecord._recoverable = flag;
							}
							array = sessionStateRecord._data;
							if (array.Length < num2)
							{
								array = new byte[num2];
								sessionStateRecord._data = array;
							}
						}
					}
					if (array != null)
					{
						if (!stateObj.TryReadByteArray(array, num2))
						{
							return false;
						}
					}
					else if (!stateObj.TrySkipBytes(num2))
					{
						return false;
					}
				}
				if (b3 < 255)
				{
					length -= 2 + num2;
				}
				else
				{
					length -= 6 + num2;
				}
			}
			return true;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00053F20 File Offset: 0x00052120
		private unsafe bool TryProcessLoginAck(TdsParserStateObject stateObj, out SqlLoginAck sqlLoginAck)
		{
			SqlLoginAck sqlLoginAck2 = new SqlLoginAck();
			sqlLoginAck = null;
			if (!stateObj.TrySkipBytes(1))
			{
				return false;
			}
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)4], 4);
			Span<byte> span2 = span;
			if (!stateObj.TryReadByteArray(span2, span2.Length))
			{
				return false;
			}
			sqlLoginAck2.tdsVersion = (uint)(((((((int)(*span2[0]) << 8) | (int)(*span2[1])) << 8) | (int)(*span2[2])) << 8) | (int)(*span2[3]));
			uint num = sqlLoginAck2.tdsVersion & 4278255615U;
			uint num2 = (sqlLoginAck2.tdsVersion >> 16) & 255U;
			if (num <= 1895825409U)
			{
				if (num != 117440512U)
				{
					if (num != 134217728U)
					{
						if (num == 1895825409U)
						{
							if (num2 != 0U)
							{
								throw SQL.InvalidTDSVersion();
							}
							this._is2000SP1 = true;
							goto IL_0141;
						}
					}
					else
					{
						if (num2 != 0U)
						{
							throw SQL.InvalidTDSVersion();
						}
						this._is2022 = true;
						goto IL_0141;
					}
				}
				else
				{
					if (num2 == 0U)
					{
						goto IL_0141;
					}
					if (num2 == 1U)
					{
						this._is2000 = true;
						goto IL_0141;
					}
					throw SQL.InvalidTDSVersion();
				}
			}
			else if (num != 1912602626U)
			{
				if (num != 1929379843U)
				{
					if (num == 1946157060U)
					{
						if (num2 != 0U)
						{
							throw SQL.InvalidTDSVersion();
						}
						this._is2012 = true;
						goto IL_0141;
					}
				}
				else
				{
					if (num2 != 11U)
					{
						throw SQL.InvalidTDSVersion();
					}
					this._is2008 = true;
					goto IL_0141;
				}
			}
			else
			{
				if (num2 != 9U)
				{
					throw SQL.InvalidTDSVersion();
				}
				this._is2005 = true;
				goto IL_0141;
			}
			throw SQL.InvalidTDSVersion();
			IL_0141:
			this._is2012 |= this._is2022;
			this._is2008 |= this._is2012;
			this._is2005 |= this._is2008;
			this._is2000SP1 |= this._is2005;
			this._is2000 |= this._is2000SP1;
			sqlLoginAck2.isVersion8 = this._is2000;
			stateObj._outBytesUsed = stateObj._outputHeaderLen;
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			if (!stateObj.TryReadString((int)b, out sqlLoginAck2.programName))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out sqlLoginAck2.majorVersion))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out sqlLoginAck2.minorVersion))
			{
				return false;
			}
			byte b2;
			if (!stateObj.TryReadByte(out b2))
			{
				return false;
			}
			byte b3;
			if (!stateObj.TryReadByte(out b3))
			{
				return false;
			}
			sqlLoginAck2.buildNum = (short)(((int)b2 << 8) + (int)b3);
			this._state = TdsParserState.OpenLoggedIn;
			if (this._is2005 && this._fMARS)
			{
				this._resetConnectionEvent = new AutoResetEvent(true);
			}
			if (this._connHandler.ConnectionOptions.UserInstance && ADP.IsEmpty(this._connHandler.InstanceName))
			{
				stateObj.AddError(new SqlError(0, 0, 20, this.Server, SQLMessage.UserInstanceFailure(), "", 0, null));
				this.ThrowExceptionAndWarning(stateObj, false, false);
			}
			sqlLoginAck = sqlLoginAck2;
			return true;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x000541C0 File Offset: 0x000523C0
		private bool TryProcessFedAuthInfo(TdsParserStateObject stateObj, int tokenLen, out SqlFedAuthInfo sqlFedAuthInfo)
		{
			sqlFedAuthInfo = null;
			SqlFedAuthInfo sqlFedAuthInfo2 = new SqlFedAuthInfo();
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.TdsParser.TryProcessFedAuthInfo> FEDAUTHINFO token stream length = {0}", tokenLen);
			if (tokenLen < 4)
			{
				SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.TryProcessFedAuthInfo|ERR> FEDAUTHINFO token stream length too short for CountOfInfoIDs.");
				throw SQL.ParsingErrorLength(ParsingErrorState.FedAuthInfoLengthTooShortForCountOfInfoIds, tokenLen);
			}
			uint num;
			if (!stateObj.TryReadUInt32(out num))
			{
				SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.TryProcessFedAuthInfo|ERR> Failed to read CountOfInfoIDs in FEDAUTHINFO token stream.");
				throw SQL.ParsingError(ParsingErrorState.FedAuthInfoFailedToReadCountOfInfoIds);
			}
			tokenLen -= 4;
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<string>("<sc.TdsParser.TryProcessFedAuthInfo|ADV> CountOfInfoIDs = {0}", num.ToString(CultureInfo.InvariantCulture));
			}
			if (tokenLen <= 0)
			{
				SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.TryProcessFedAuthInfo|ERR> FEDAUTHINFO token stream is not long enough to contain the data it claims to.");
				throw SQL.ParsingErrorLength(ParsingErrorState.FedAuthInfoLengthTooShortForData, tokenLen);
			}
			byte[] array = new byte[tokenLen];
			int num2 = 0;
			bool flag = stateObj.TryReadByteArray(array, tokenLen, out num2);
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				SqlClientEventSource.Log.AdvancedTraceEvent<string>("<sc.TdsParser.TryProcessFedAuthInfo|ADV> Read rest of FEDAUTHINFO token stream: {0}", BitConverter.ToString(array, 0, num2));
			}
			if (!flag || num2 != tokenLen)
			{
				SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.TdsParser.TryProcessFedAuthInfo|ERR> Failed to read FEDAUTHINFO token stream. Attempted to read {0} bytes, actually read {1}", tokenLen, num2);
				throw SQL.ParsingError(ParsingErrorState.FedAuthInfoFailedToReadTokenStream);
			}
			uint num3 = checked(num * 9U);
			for (uint num4 = 0U; num4 < num; num4 += 1U)
			{
				checked
				{
					uint num5 = num4 * 9U;
					byte b = array[(int)num5];
					uint num6 = BitConverter.ToUInt32(array, (int)(num5 + 1U));
					uint num7 = BitConverter.ToUInt32(array, (int)(num5 + 5U));
					if (SqlClientEventSource.Log.IsAdvancedTraceOn())
					{
						SqlClientEventSource.Log.AdvancedTraceEvent<byte, string, string>("<sc.TdsParser.TryProcessFedAuthInfo> FedAuthInfoOpt: ID={0}, DataLen={1}, Offset={2}", b, num6.ToString(CultureInfo.InvariantCulture), num7.ToString(CultureInfo.InvariantCulture));
					}
					num7 -= 4U;
					if (num7 < num3 || unchecked((ulong)num7 >= (ulong)((long)tokenLen)))
					{
						SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.TryProcessFedAuthInfo|ERR> FedAuthInfoDataOffset points to an invalid location.");
						throw SQL.ParsingErrorOffset(ParsingErrorState.FedAuthInfoInvalidOffset, (int)num7);
					}
					string @string;
					try
					{
						@string = Encoding.Unicode.GetString(array, (int)num7, (int)num6);
					}
					catch (ArgumentOutOfRangeException ex)
					{
						SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.TryProcessFedAuthInfo|ERR> Failed to read FedAuthInfoData.");
						throw SQL.ParsingError(ParsingErrorState.FedAuthInfoFailedToReadData, ex);
					}
					catch (ArgumentException ex2)
					{
						SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.TryProcessFedAuthInfo|ERR> FedAuthInfoData is not in unicode format.");
						throw SQL.ParsingError(ParsingErrorState.FedAuthInfoDataNotUnicode, ex2);
					}
					SqlClientEventSource.Log.TryAdvancedTraceEvent<string>("<sc.TdsParser.TryProcessFedAuthInfo|ADV> FedAuthInfoData: {0}", @string);
					TdsEnums.FedAuthInfoId fedAuthInfoId = (TdsEnums.FedAuthInfoId)b;
					if (fedAuthInfoId != TdsEnums.FedAuthInfoId.Stsurl)
					{
						if (fedAuthInfoId == TdsEnums.FedAuthInfoId.Spn)
						{
							sqlFedAuthInfo2.spn = @string;
						}
						else
						{
							SqlClientEventSource.Log.TryAdvancedTraceEvent<byte>("<sc.TdsParser.TryProcessFedAuthInfo|ADV> Ignoring unknown federated authentication info option: {0}", b);
						}
					}
					else
					{
						sqlFedAuthInfo2.stsurl = @string;
					}
				}
			}
			SqlClientEventSource.Log.TryTraceEvent<SqlFedAuthInfo>("<sc.TdsParser.TryProcessFedAuthInfo> Processed FEDAUTHINFO token stream: {0}", sqlFedAuthInfo2);
			if (string.IsNullOrWhiteSpace(sqlFedAuthInfo2.stsurl) || string.IsNullOrWhiteSpace(sqlFedAuthInfo2.spn))
			{
				SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.TryProcessFedAuthInfo|ERR> FEDAUTHINFO token stream does not contain both STSURL and SPN.");
				throw SQL.ParsingError(ParsingErrorState.FedAuthInfoDoesNotContainStsurlAndSpn);
			}
			sqlFedAuthInfo = sqlFedAuthInfo2;
			return true;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0005445C File Offset: 0x0005265C
		internal bool TryProcessError(byte token, TdsParserStateObject stateObj, out SqlError error)
		{
			error = null;
			int num;
			if (!stateObj.TryReadInt32(out num))
			{
				return false;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			byte b2;
			if (!stateObj.TryReadByte(out b2))
			{
				return false;
			}
			ushort num2;
			if (!stateObj.TryReadUInt16(out num2))
			{
				return false;
			}
			string text;
			if (!stateObj.TryReadString((int)num2, out text))
			{
				return false;
			}
			byte b3;
			if (!stateObj.TryReadByte(out b3))
			{
				return false;
			}
			string server;
			if (b3 == 0)
			{
				server = this._server;
			}
			else if (!stateObj.TryReadString((int)b3, out server))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out b3))
			{
				return false;
			}
			string text2;
			if (!stateObj.TryReadString((int)b3, out text2))
			{
				return false;
			}
			int num3;
			if (this._is2005)
			{
				if (!stateObj.TryReadInt32(out num3))
				{
					return false;
				}
			}
			else
			{
				ushort num4;
				if (!stateObj.TryReadUInt16(out num4))
				{
					return false;
				}
				num3 = (int)num4;
				if (this._state == TdsParserState.OpenNotLoggedIn)
				{
					byte b4;
					if (!stateObj.TryPeekByte(out b4))
					{
						return false;
					}
					if (b4 == 0)
					{
						ushort num5;
						if (!stateObj.TryReadUInt16(out num5))
						{
							return false;
						}
						num3 = (num3 << 16) + (int)num5;
					}
				}
			}
			error = new SqlError(num, b, b2, this._server, text, text2, num3, null);
			return true;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00054554 File Offset: 0x00052754
		internal bool TryProcessReturnValue(int length, TdsParserStateObject stateObj, out SqlReturnValue returnValue, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
		{
			returnValue = null;
			SqlReturnValue sqlReturnValue = new SqlReturnValue();
			sqlReturnValue.length = length;
			if (this._is2005 && !stateObj.TryReadUInt16(out sqlReturnValue.parmIndex))
			{
				return false;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			sqlReturnValue.parameter = null;
			if (b > 0 && !stateObj.TryReadString((int)b, out sqlReturnValue.parameter))
			{
				return false;
			}
			byte b2;
			if (!stateObj.TryReadByte(out b2))
			{
				return false;
			}
			uint num;
			if (this.Is2005OrNewer)
			{
				if (!stateObj.TryReadUInt32(out num))
				{
					return false;
				}
			}
			else
			{
				ushort num2;
				if (!stateObj.TryReadUInt16(out num2))
				{
					return false;
				}
				num = (uint)num2;
			}
			byte b3;
			if (!stateObj.TryReadByte(out b3))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out b3))
			{
				return false;
			}
			if (this._serverSupportsColumnEncryption)
			{
				sqlReturnValue.isEncrypted = 8 == (b3 & 8);
			}
			byte b4;
			if (!stateObj.TryReadByte(out b4))
			{
				return false;
			}
			int num3;
			if (b4 == 241)
			{
				num3 = 65535;
			}
			else if (this.IsVarTimeTds(b4))
			{
				num3 = 0;
			}
			else if (b4 == 40)
			{
				num3 = 3;
			}
			else if (!this.TryGetTokenLength(b4, stateObj, out num3))
			{
				return false;
			}
			sqlReturnValue.metaType = MetaType.GetSqlDataType((int)b4, num, num3);
			sqlReturnValue.type = sqlReturnValue.metaType.SqlDbType;
			if (this._is2000)
			{
				sqlReturnValue.tdsType = sqlReturnValue.metaType.NullableType;
				sqlReturnValue.IsNullable = true;
				if (num3 == 65535)
				{
					sqlReturnValue.metaType = MetaType.GetMaxMetaTypeFromMetaType(sqlReturnValue.metaType);
				}
			}
			else
			{
				if (sqlReturnValue.metaType.NullableType == b4)
				{
					sqlReturnValue.IsNullable = true;
				}
				sqlReturnValue.tdsType = b4;
			}
			if (sqlReturnValue.type == SqlDbType.Decimal)
			{
				if (!stateObj.TryReadByte(out sqlReturnValue.precision))
				{
					return false;
				}
				if (!stateObj.TryReadByte(out sqlReturnValue.scale))
				{
					return false;
				}
			}
			if (sqlReturnValue.metaType.IsVarTime && !stateObj.TryReadByte(out sqlReturnValue.scale))
			{
				return false;
			}
			if (b4 == 240 && !this.TryProcessUDTMetaData(sqlReturnValue, stateObj))
			{
				return false;
			}
			if (sqlReturnValue.type == SqlDbType.Xml)
			{
				byte b5;
				if (!stateObj.TryReadByte(out b5))
				{
					return false;
				}
				if ((b5 & 1) != 0)
				{
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					if (sqlReturnValue.xmlSchemaCollection == null)
					{
						sqlReturnValue.xmlSchemaCollection = new SqlMetaDataXmlSchemaCollection();
					}
					if (b != 0 && !stateObj.TryReadString((int)b, out sqlReturnValue.xmlSchemaCollection.Database))
					{
						return false;
					}
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					if (b != 0 && !stateObj.TryReadString((int)b, out sqlReturnValue.xmlSchemaCollection.OwningSchema))
					{
						return false;
					}
					short num4;
					if (!stateObj.TryReadInt16(out num4))
					{
						return false;
					}
					if (num4 != 0 && !stateObj.TryReadString((int)num4, out sqlReturnValue.xmlSchemaCollection.Name))
					{
						return false;
					}
				}
			}
			else if (this._is2000 && sqlReturnValue.metaType.IsCharType)
			{
				if (!this.TryProcessCollation(stateObj, out sqlReturnValue.collation))
				{
					return false;
				}
				if (sqlReturnValue.collation.IsUTF8)
				{
					sqlReturnValue.encoding = Encoding.UTF8;
				}
				else
				{
					int codePage = this.GetCodePage(sqlReturnValue.collation, stateObj);
					if (codePage == this._defaultCodePage)
					{
						sqlReturnValue.codePage = this._defaultCodePage;
						sqlReturnValue.encoding = this._defaultEncoding;
					}
					else
					{
						sqlReturnValue.codePage = codePage;
						sqlReturnValue.encoding = Encoding.GetEncoding(sqlReturnValue.codePage);
					}
				}
			}
			if (this._serverSupportsColumnEncryption && sqlReturnValue.isEncrypted && !this.TryProcessTceCryptoMetadata(stateObj, sqlReturnValue, null, columnEncryptionSetting, true))
			{
				return false;
			}
			bool flag = false;
			ulong num5;
			if (!this.TryProcessColumnHeaderNoNBC(sqlReturnValue, stateObj, out flag, out num5))
			{
				return false;
			}
			int num6 = ((num5 > 2147483647UL) ? int.MaxValue : ((int)num5));
			if (sqlReturnValue.metaType.IsPlp)
			{
				num6 = int.MaxValue;
			}
			if (flag)
			{
				TdsParser.GetNullSqlValue(sqlReturnValue.value, sqlReturnValue, SqlCommandColumnEncryptionSetting.Disabled, this._connHandler);
			}
			else if (!this.TryReadSqlValue(sqlReturnValue.value, sqlReturnValue, num6, stateObj, SqlCommandColumnEncryptionSetting.Disabled, null, null))
			{
				return false;
			}
			returnValue = sqlReturnValue;
			return true;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x000548F8 File Offset: 0x00052AF8
		internal bool TryProcessTceCryptoMetadata(TdsParserStateObject stateObj, SqlMetaDataPriv col, SqlTceCipherInfoTable cipherTable, SqlCommandColumnEncryptionSetting columnEncryptionSetting, bool isReturnValue)
		{
			ushort num = 0;
			if (cipherTable != null)
			{
				if (!stateObj.TryReadUInt16(out num))
				{
					return false;
				}
				if ((int)num >= cipherTable.Size)
				{
					SqlClientEventSource.Log.TryTraceEvent<ushort, int>("<sc.TdsParser.TryProcessTceCryptoMetadata|TCE> Incorrect ordinal received {0}, max tab size: {1}", num, cipherTable.Size);
					throw SQL.ParsingErrorValue(ParsingErrorState.TceInvalidOrdinalIntoCipherInfoTable, (int)num);
				}
			}
			uint num2;
			if (!stateObj.TryReadUInt32(out num2))
			{
				return false;
			}
			col.baseTI = new SqlMetaDataPriv();
			if (!this.TryProcessTypeInfo(stateObj, col.baseTI, num2))
			{
				return false;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			string text = null;
			if (b == 0)
			{
				byte b2;
				if (!stateObj.TryReadByte(out b2))
				{
					return false;
				}
				if (!stateObj.TryReadString((int)b2, out text))
				{
					return false;
				}
			}
			byte b3;
			if (!stateObj.TryReadByte(out b3))
			{
				return false;
			}
			byte b4;
			if (!stateObj.TryReadByte(out b4))
			{
				return false;
			}
			if (columnEncryptionSetting == SqlCommandColumnEncryptionSetting.Enabled || (columnEncryptionSetting == SqlCommandColumnEncryptionSetting.ResultSetOnly && !isReturnValue) || (columnEncryptionSetting == SqlCommandColumnEncryptionSetting.UseConnectionSetting && this._connHandler != null && this._connHandler.ConnectionOptions != null && this._connHandler.ConnectionOptions.ColumnEncryptionSetting == SqlConnectionColumnEncryptionSetting.Enabled))
			{
				col.cipherMD = new SqlCipherMetadata((cipherTable != null) ? cipherTable[(int)num] : null, num, b, text, b3, b4);
			}
			else
			{
				col.isEncrypted = false;
			}
			return true;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00054A10 File Offset: 0x00052C10
		internal bool TryProcessCollation(TdsParserStateObject stateObj, out SqlCollation collation)
		{
			uint num;
			if (!stateObj.TryReadUInt32(out num))
			{
				collation = null;
				return false;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				collation = null;
				return false;
			}
			if (SqlCollation.Equals(this._cachedCollation, num, b))
			{
				collation = this._cachedCollation;
			}
			else
			{
				collation = new SqlCollation(num, b);
				this._cachedCollation = collation;
			}
			return true;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00054A68 File Offset: 0x00052C68
		private void WriteCollation(SqlCollation collation, TdsParserStateObject stateObj)
		{
			if (collation == null)
			{
				this._physicalStateObj.WriteByte(0);
				return;
			}
			this._physicalStateObj.WriteByte(5);
			this.WriteUnsignedInt(collation._info, this._physicalStateObj);
			this._physicalStateObj.WriteByte(collation._sortId);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00054AB4 File Offset: 0x00052CB4
		internal int GetCodePage(SqlCollation collation, TdsParserStateObject stateObj)
		{
			int num = 0;
			if (collation._sortId != 0)
			{
				num = (int)TdsEnums.CODE_PAGE_FROM_SORT_ID[(int)collation._sortId];
			}
			else
			{
				int num2 = collation.LCID;
				bool flag = false;
				try
				{
					num = CultureInfo.GetCultureInfo(num2).TextInfo.ANSICodePage;
					flag = true;
				}
				catch (ArgumentException ex)
				{
					ADP.TraceExceptionWithoutRethrow(ex);
				}
				if (!flag || num == 0)
				{
					CultureInfo cultureInfo = null;
					if (num2 <= 66578)
					{
						if (num2 <= 2087)
						{
							if (num2 == 1087)
							{
								goto IL_00ED;
							}
							if (num2 != 2087)
							{
								goto IL_00F3;
							}
							goto IL_00D1;
						}
						else if (num2 != 66564 && num2 - 66577 > 1)
						{
							goto IL_00F3;
						}
					}
					else if (num2 <= 68612)
					{
						if (num2 != 67588 && num2 != 68612)
						{
							goto IL_00F3;
						}
					}
					else if (num2 != 69636 && num2 != 70660)
					{
						goto IL_00F3;
					}
					num2 &= 16383;
					try
					{
						cultureInfo = new CultureInfo(num2);
						flag = true;
						goto IL_00F3;
					}
					catch (ArgumentException ex2)
					{
						ADP.TraceExceptionWithoutRethrow(ex2);
						goto IL_00F3;
					}
					IL_00D1:
					try
					{
						cultureInfo = new CultureInfo(1063);
						flag = true;
						goto IL_00F3;
					}
					catch (ArgumentException ex3)
					{
						ADP.TraceExceptionWithoutRethrow(ex3);
						goto IL_00F3;
					}
					IL_00ED:
					num = 1251;
					IL_00F3:
					if (!flag)
					{
						this.ThrowUnsupportedCollationEncountered(stateObj);
					}
					if (cultureInfo != null)
					{
						num = cultureInfo.TextInfo.ANSICodePage;
					}
				}
			}
			return num;
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00054BF8 File Offset: 0x00052DF8
		internal void DrainData(TdsParserStateObject stateObj)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				try
				{
					SqlDataReader.SharedState readerState = stateObj._readerState;
					if (readerState != null && readerState._dataReady)
					{
						_SqlMetaDataSet cleanupMetaData = stateObj._cleanupMetaData;
						if (stateObj._partialHeaderBytesRead > 0 && !stateObj.TryProcessHeader())
						{
							throw SQL.SynchronousCallMayNotPend();
						}
						if (readerState._nextColumnHeaderToRead == 0)
						{
							if (!stateObj.Parser.TrySkipRow(stateObj._cleanupMetaData, stateObj))
							{
								throw SQL.SynchronousCallMayNotPend();
							}
						}
						else
						{
							if (readerState._nextColumnDataToRead < readerState._nextColumnHeaderToRead)
							{
								if (readerState._nextColumnHeaderToRead > 0 && cleanupMetaData[readerState._nextColumnHeaderToRead - 1].metaType.IsPlp)
								{
									ulong num;
									if (stateObj._longlen != 0UL && !this.TrySkipPlpValue(18446744073709551615UL, stateObj, out num))
									{
										throw SQL.SynchronousCallMayNotPend();
									}
								}
								else if (0L < readerState._columnDataBytesRemaining && !stateObj.TrySkipLongBytes(readerState._columnDataBytesRemaining))
								{
									throw SQL.SynchronousCallMayNotPend();
								}
							}
							if (!stateObj.Parser.TrySkipRow(cleanupMetaData, readerState._nextColumnHeaderToRead, stateObj))
							{
								throw SQL.SynchronousCallMayNotPend();
							}
						}
					}
					this.Run(RunBehavior.Clean, null, null, null, stateObj);
				}
				catch
				{
					this._connHandler.DoomThisConnection();
					throw;
				}
			}
			catch (OutOfMemoryException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00054D68 File Offset: 0x00052F68
		internal void ThrowUnsupportedCollationEncountered(TdsParserStateObject stateObj)
		{
			stateObj.AddError(new SqlError(0, 0, 11, this._server, SQLMessage.CultureIdError(), "", 0, null));
			if (stateObj != null)
			{
				this.DrainData(stateObj);
				stateObj._pendingData = false;
			}
			this.ThrowExceptionAndWarning(stateObj, false, false);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00054DB0 File Offset: 0x00052FB0
		internal bool TryProcessAltMetaData(int cColumns, TdsParserStateObject stateObj, out _SqlMetaDataSet metaData)
		{
			metaData = null;
			_SqlMetaDataSet sqlMetaDataSet = new _SqlMetaDataSet(cColumns, null);
			if (!stateObj.TryReadUInt16(out sqlMetaDataSet.id))
			{
				return false;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			while (b > 0)
			{
				if (!stateObj.TrySkipBytes(2))
				{
					return false;
				}
				b -= 1;
			}
			for (int i = 0; i < cColumns; i++)
			{
				_SqlMetaData sqlMetaData = sqlMetaDataSet[i];
				if (!stateObj.TryReadByte(out sqlMetaData.op))
				{
					return false;
				}
				if (!stateObj.TryReadUInt16(out sqlMetaData.operand))
				{
					return false;
				}
				if (!this.TryCommonProcessMetaData(stateObj, sqlMetaData, null, false, SqlCommandColumnEncryptionSetting.Disabled))
				{
					return false;
				}
				if (ADP.IsEmpty(sqlMetaData.column))
				{
					byte op = sqlMetaData.op;
					if (op != 9)
					{
						switch (op)
						{
						case 48:
							sqlMetaData.column = "stdev";
							break;
						case 49:
							sqlMetaData.column = "stdevp";
							break;
						case 50:
							sqlMetaData.column = "var";
							break;
						case 51:
							sqlMetaData.column = "varp";
							break;
						default:
							switch (op)
							{
							case 75:
								sqlMetaData.column = "cnt";
								break;
							case 77:
								sqlMetaData.column = "sum";
								break;
							case 79:
								sqlMetaData.column = "avg";
								break;
							case 81:
								sqlMetaData.column = "min";
								break;
							case 82:
								sqlMetaData.column = "max";
								break;
							case 83:
								sqlMetaData.column = "any";
								break;
							case 86:
								sqlMetaData.column = "noop";
								break;
							}
							break;
						}
					}
					else
					{
						sqlMetaData.column = "cntb";
					}
				}
			}
			metaData = sqlMetaDataSet;
			return true;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00054F5C File Offset: 0x0005315C
		internal bool TryReadCipherInfoEntry(TdsParserStateObject stateObj, out SqlTceCipherInfoEntry entry)
		{
			byte b = 0;
			entry = new SqlTceCipherInfoEntry(0);
			int num;
			if (!stateObj.TryReadInt32(out num))
			{
				return false;
			}
			int num2;
			if (!stateObj.TryReadInt32(out num2))
			{
				return false;
			}
			int num3;
			if (!stateObj.TryReadInt32(out num3))
			{
				return false;
			}
			byte[] array = new byte[8];
			if (!stateObj.TryReadByteArray(array, 8))
			{
				return false;
			}
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			for (int i = 0; i < (int)b; i++)
			{
				ushort num4;
				if (!stateObj.TryReadUInt16(out num4))
				{
					return false;
				}
				int num5 = (int)num4;
				byte[] array2 = new byte[num5];
				if (!stateObj.TryReadByteArray(array2, num5))
				{
					return false;
				}
				byte b2;
				if (!stateObj.TryReadByte(out b2))
				{
					return false;
				}
				num5 = (int)b2;
				string text;
				if (!stateObj.TryReadString(num5, out text))
				{
					return false;
				}
				if (!stateObj.TryReadUInt16(out num4))
				{
					return false;
				}
				num5 = (int)num4;
				string text2;
				if (!stateObj.TryReadString(num5, out text2))
				{
					return false;
				}
				byte b3;
				if (!stateObj.TryReadByte(out b3))
				{
					return false;
				}
				num5 = (int)b3;
				string text3;
				if (!stateObj.TryReadString(num5, out text3))
				{
					return false;
				}
				entry.Add(array2, num, num2, num3, array, text2, text, text3);
			}
			return true;
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00055070 File Offset: 0x00053270
		internal bool TryProcessCipherInfoTable(TdsParserStateObject stateObj, out SqlTceCipherInfoTable cipherTable)
		{
			short num = 0;
			cipherTable = null;
			if (!stateObj.TryReadInt16(out num))
			{
				return false;
			}
			if (num != 0)
			{
				SqlTceCipherInfoTable sqlTceCipherInfoTable = new SqlTceCipherInfoTable((int)num);
				for (int i = 0; i < (int)num; i++)
				{
					SqlTceCipherInfoEntry sqlTceCipherInfoEntry;
					if (!this.TryReadCipherInfoEntry(stateObj, out sqlTceCipherInfoEntry))
					{
						return false;
					}
					sqlTceCipherInfoTable[i] = sqlTceCipherInfoEntry;
				}
				cipherTable = sqlTceCipherInfoTable;
			}
			return true;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x000550C0 File Offset: 0x000532C0
		internal bool TryProcessMetaData(int cColumns, TdsParserStateObject stateObj, out _SqlMetaDataSet metaData, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
		{
			SqlTceCipherInfoTable sqlTceCipherInfoTable = null;
			if (this._serverSupportsColumnEncryption && !this.TryProcessCipherInfoTable(stateObj, out sqlTceCipherInfoTable))
			{
				metaData = null;
				return false;
			}
			_SqlMetaDataSet sqlMetaDataSet = new _SqlMetaDataSet(cColumns, sqlTceCipherInfoTable);
			for (int i = 0; i < cColumns; i++)
			{
				if (!this.TryCommonProcessMetaData(stateObj, sqlMetaDataSet[i], sqlTceCipherInfoTable, true, columnEncryptionSetting))
				{
					metaData = null;
					return false;
				}
			}
			metaData = sqlMetaDataSet;
			return true;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00055118 File Offset: 0x00053318
		private bool IsVarTimeTds(byte tdsType)
		{
			return tdsType == 41 || tdsType == 42 || tdsType == 43;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0005512C File Offset: 0x0005332C
		private bool TryProcessTypeInfo(TdsParserStateObject stateObj, SqlMetaDataPriv col, uint userType)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			if (b == 241)
			{
				col.length = 65535;
			}
			else if (this.IsVarTimeTds(b))
			{
				col.length = 0;
			}
			else if (b == 40)
			{
				col.length = 3;
			}
			else if (!this.TryGetTokenLength(b, stateObj, out col.length))
			{
				return false;
			}
			col.metaType = MetaType.GetSqlDataType((int)b, userType, col.length);
			col.type = col.metaType.SqlDbType;
			if (this._is2000)
			{
				col.tdsType = (col.IsNullable ? col.metaType.NullableType : col.metaType.TDSType);
			}
			else
			{
				col.tdsType = b;
			}
			if (this._is2005)
			{
				if (240 == b && !this.TryProcessUDTMetaData(col, stateObj))
				{
					return false;
				}
				if (col.length == 65535)
				{
					col.metaType = MetaType.GetMaxMetaTypeFromMetaType(col.metaType);
					col.length = int.MaxValue;
					if (b == 241)
					{
						byte b2;
						if (!stateObj.TryReadByte(out b2))
						{
							return false;
						}
						if ((b2 & 1) != 0)
						{
							byte b3;
							if (!stateObj.TryReadByte(out b3))
							{
								return false;
							}
							if (col.xmlSchemaCollection == null)
							{
								col.xmlSchemaCollection = new SqlMetaDataXmlSchemaCollection();
							}
							if (b3 != 0 && !stateObj.TryReadString((int)b3, out col.xmlSchemaCollection.Database))
							{
								return false;
							}
							if (!stateObj.TryReadByte(out b3))
							{
								return false;
							}
							if (b3 != 0 && !stateObj.TryReadString((int)b3, out col.xmlSchemaCollection.OwningSchema))
							{
								return false;
							}
							short num;
							if (!stateObj.TryReadInt16(out num))
							{
								return false;
							}
							if (b3 != 0 && !stateObj.TryReadString((int)num, out col.xmlSchemaCollection.Name))
							{
								return false;
							}
						}
					}
				}
			}
			if (col.type == SqlDbType.Decimal)
			{
				if (!stateObj.TryReadByte(out col.precision))
				{
					return false;
				}
				if (!stateObj.TryReadByte(out col.scale))
				{
					return false;
				}
			}
			if (col.metaType.IsVarTime)
			{
				if (!stateObj.TryReadByte(out col.scale))
				{
					return false;
				}
				switch (col.metaType.SqlDbType)
				{
				case SqlDbType.Time:
					col.length = MetaType.GetTimeSizeFromScale(col.scale);
					break;
				case SqlDbType.DateTime2:
					col.length = 3 + MetaType.GetTimeSizeFromScale(col.scale);
					break;
				case SqlDbType.DateTimeOffset:
					col.length = 5 + MetaType.GetTimeSizeFromScale(col.scale);
					break;
				}
			}
			if (this._is2000 && col.metaType.IsCharType && b != 241)
			{
				if (!this.TryProcessCollation(stateObj, out col.collation))
				{
					return false;
				}
				if (col.collation.IsUTF8)
				{
					col.encoding = Encoding.UTF8;
				}
				else
				{
					int codePage = this.GetCodePage(col.collation, stateObj);
					if (codePage == this._defaultCodePage)
					{
						col.codePage = this._defaultCodePage;
						col.encoding = this._defaultEncoding;
					}
					else
					{
						col.codePage = codePage;
						col.encoding = Encoding.GetEncoding(col.codePage);
					}
				}
			}
			return true;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00055414 File Offset: 0x00053614
		private bool TryCommonProcessMetaData(TdsParserStateObject stateObj, _SqlMetaData col, SqlTceCipherInfoTable cipherTable, bool fColMD, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
		{
			uint num;
			if (this.Is2005OrNewer)
			{
				if (!stateObj.TryReadUInt32(out num))
				{
					return false;
				}
			}
			else
			{
				ushort num2;
				if (!stateObj.TryReadUInt16(out num2))
				{
					return false;
				}
				num = (uint)num2;
			}
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			col.Updatability = (byte)((b & 11) >> 2);
			col.IsNullable = 1 == (b & 1);
			col.IsIdentity = 16 == (b & 16);
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			col.IsColumnSet = 4 == (b & 4);
			if (fColMD && this._serverSupportsColumnEncryption)
			{
				col.isEncrypted = 8 == (b & 8);
			}
			if (!this.TryProcessTypeInfo(stateObj, col, num))
			{
				return false;
			}
			if (col.metaType.IsLong && !col.metaType.IsPlp)
			{
				if (this._is2005)
				{
					int num3 = 65535;
					if (!this.TryProcessOneTable(stateObj, ref num3, out col.multiPartTableName))
					{
						return false;
					}
				}
				else
				{
					ushort num4;
					if (!stateObj.TryReadUInt16(out num4))
					{
						return false;
					}
					string text;
					if (!stateObj.TryReadString((int)num4, out text))
					{
						return false;
					}
					col.multiPartTableName = new MultiPartTableName(text);
				}
			}
			if (fColMD && this._serverSupportsColumnEncryption && col.isEncrypted && cipherTable != null && !this.TryProcessTceCryptoMetadata(stateObj, col, cipherTable, columnEncryptionSetting, false))
			{
				return false;
			}
			byte b2;
			if (!stateObj.TryReadByte(out b2))
			{
				return false;
			}
			if (!stateObj.TryReadString((int)b2, out col.column))
			{
				return false;
			}
			stateObj._receivedColMetaData = true;
			return true;
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00055564 File Offset: 0x00053764
		private bool TryProcessUDTMetaData(SqlMetaDataPriv metaData, TdsParserStateObject stateObj)
		{
			ushort num;
			if (!stateObj.TryReadUInt16(out num))
			{
				return false;
			}
			metaData.length = (int)num;
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			if (metaData.udt == null)
			{
				metaData.udt = new SqlMetaDataUdt();
			}
			return (b == 0 || stateObj.TryReadString((int)b, out metaData.udt.DatabaseName)) && stateObj.TryReadByte(out b) && (b == 0 || stateObj.TryReadString((int)b, out metaData.udt.SchemaName)) && stateObj.TryReadByte(out b) && (b == 0 || stateObj.TryReadString((int)b, out metaData.udt.TypeName)) && stateObj.TryReadUInt16(out num) && (num == 0 || stateObj.TryReadString((int)num, out metaData.udt.AssemblyQualifiedName));
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0005562C File Offset: 0x0005382C
		private void WriteUDTMetaData(object value, string database, string schema, string type, TdsParserStateObject stateObj)
		{
			if (ADP.IsEmpty(database))
			{
				stateObj.WriteByte(0);
			}
			else
			{
				stateObj.WriteByte((byte)database.Length);
				this.WriteString(database, stateObj, true);
			}
			if (ADP.IsEmpty(schema))
			{
				stateObj.WriteByte(0);
			}
			else
			{
				stateObj.WriteByte((byte)schema.Length);
				this.WriteString(schema, stateObj, true);
			}
			if (ADP.IsEmpty(type))
			{
				stateObj.WriteByte(0);
				return;
			}
			stateObj.WriteByte((byte)type.Length);
			this.WriteString(type, stateObj, true);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x000556BC File Offset: 0x000538BC
		internal bool TryProcessTableName(int length, TdsParserStateObject stateObj, out MultiPartTableName[] multiPartTableNames)
		{
			int num = 0;
			MultiPartTableName[] array = new MultiPartTableName[1];
			while (length > 0)
			{
				MultiPartTableName multiPartTableName;
				if (!this.TryProcessOneTable(stateObj, ref length, out multiPartTableName))
				{
					multiPartTableNames = null;
					return false;
				}
				if (num == 0)
				{
					array[num] = multiPartTableName;
				}
				else
				{
					MultiPartTableName[] array2 = new MultiPartTableName[array.Length + 1];
					Array.Copy(array, 0, array2, 0, array.Length);
					array2[array.Length] = multiPartTableName;
					array = array2;
				}
				num++;
			}
			multiPartTableNames = array;
			return true;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00055724 File Offset: 0x00053924
		private bool TryProcessOneTable(TdsParserStateObject stateObj, ref int length, out MultiPartTableName multiPartTableName)
		{
			multiPartTableName = default(MultiPartTableName);
			MultiPartTableName multiPartTableName2;
			if (this._is2000SP1)
			{
				multiPartTableName2 = default(MultiPartTableName);
				byte b;
				if (!stateObj.TryReadByte(out b))
				{
					return false;
				}
				length--;
				if (b == 4)
				{
					ushort num;
					if (!stateObj.TryReadUInt16(out num))
					{
						return false;
					}
					length -= 2;
					string text;
					if (!stateObj.TryReadString((int)num, out text))
					{
						return false;
					}
					multiPartTableName2.ServerName = text;
					b -= 1;
					length -= (int)(num * 2);
				}
				if (b == 3)
				{
					ushort num;
					if (!stateObj.TryReadUInt16(out num))
					{
						return false;
					}
					length -= 2;
					string text;
					if (!stateObj.TryReadString((int)num, out text))
					{
						return false;
					}
					multiPartTableName2.CatalogName = text;
					length -= (int)(num * 2);
					b -= 1;
				}
				if (b == 2)
				{
					ushort num;
					if (!stateObj.TryReadUInt16(out num))
					{
						return false;
					}
					length -= 2;
					string text;
					if (!stateObj.TryReadString((int)num, out text))
					{
						return false;
					}
					multiPartTableName2.SchemaName = text;
					length -= (int)(num * 2);
					b -= 1;
				}
				if (b == 1)
				{
					ushort num;
					if (!stateObj.TryReadUInt16(out num))
					{
						return false;
					}
					length -= 2;
					string text;
					if (!stateObj.TryReadString((int)num, out text))
					{
						return false;
					}
					multiPartTableName2.TableName = text;
					length -= (int)(num * 2);
					b -= 1;
				}
			}
			else
			{
				ushort num;
				if (!stateObj.TryReadUInt16(out num))
				{
					return false;
				}
				length -= 2;
				string text;
				if (!stateObj.TryReadString((int)num, out text))
				{
					return false;
				}
				string text2 = text;
				length -= (int)(num * 2);
				multiPartTableName2 = new MultiPartTableName(MultipartIdentifier.ParseMultipartIdentifier(text2, "[\"", "]\"", Strings.SQL_TDSParserTableName, false));
			}
			multiPartTableName = multiPartTableName2;
			return true;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00055890 File Offset: 0x00053A90
		private bool TryProcessColInfo(_SqlMetaDataSet columns, SqlDataReader reader, TdsParserStateObject stateObj, out _SqlMetaDataSet metaData)
		{
			metaData = null;
			for (int i = 0; i < columns.Length; i++)
			{
				_SqlMetaData sqlMetaData = columns[i];
				byte b;
				if (!stateObj.TryReadByte(out b))
				{
					return false;
				}
				if (!stateObj.TryReadByte(out sqlMetaData.tableNum))
				{
					return false;
				}
				byte b2;
				if (!stateObj.TryReadByte(out b2))
				{
					return false;
				}
				sqlMetaData.IsDifferentName = 32 == (b2 & 32);
				sqlMetaData.IsExpression = 4 == (b2 & 4);
				sqlMetaData.IsKey = 8 == (b2 & 8);
				sqlMetaData.IsHidden = 16 == (b2 & 16);
				if (sqlMetaData.IsDifferentName)
				{
					byte b3;
					if (!stateObj.TryReadByte(out b3))
					{
						return false;
					}
					if (!stateObj.TryReadString((int)b3, out sqlMetaData.baseColumn))
					{
						return false;
					}
				}
				if (reader.TableNames != null && sqlMetaData.tableNum > 0)
				{
					sqlMetaData.multiPartTableName = reader.TableNames[(int)(sqlMetaData.tableNum - 1)];
				}
				if (sqlMetaData.IsExpression)
				{
					sqlMetaData.Updatability = 0;
				}
			}
			metaData = columns;
			return true;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00055980 File Offset: 0x00053B80
		internal bool TryProcessColumnHeader(SqlMetaDataPriv col, TdsParserStateObject stateObj, int columnOrdinal, out bool isNull, out ulong length)
		{
			if (stateObj.IsNullCompressionBitSet(columnOrdinal))
			{
				isNull = true;
				length = 0UL;
				return true;
			}
			return this.TryProcessColumnHeaderNoNBC(col, stateObj, out isNull, out length);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x000559A4 File Offset: 0x00053BA4
		private bool TryProcessColumnHeaderNoNBC(SqlMetaDataPriv col, TdsParserStateObject stateObj, out bool isNull, out ulong length)
		{
			if (col.metaType.IsLong && !col.metaType.IsPlp)
			{
				byte b;
				if (!stateObj.TryReadByte(out b))
				{
					isNull = false;
					length = 0UL;
					return false;
				}
				if (b == 0)
				{
					isNull = true;
					length = 0UL;
					return true;
				}
				if (!stateObj.TrySkipBytes((int)b))
				{
					isNull = false;
					length = 0UL;
					return false;
				}
				if (!stateObj.TrySkipBytes(8))
				{
					isNull = false;
					length = 0UL;
					return false;
				}
				isNull = false;
				return this.TryGetDataLength(col, stateObj, out length);
			}
			else
			{
				ulong num;
				if (!this.TryGetDataLength(col, stateObj, out num))
				{
					isNull = false;
					length = 0UL;
					return false;
				}
				isNull = this.IsNull(col.metaType, num);
				length = (isNull ? 0UL : num);
				return true;
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00055A54 File Offset: 0x00053C54
		internal bool TryGetAltRowId(TdsParserStateObject stateObj, out int id)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				id = 0;
				return false;
			}
			if (!stateObj.TryStartNewRow(false, 0))
			{
				id = 0;
				return false;
			}
			ushort num;
			if (!stateObj.TryReadUInt16(out num))
			{
				id = 0;
				return false;
			}
			id = (int)num;
			return true;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00055A94 File Offset: 0x00053C94
		private bool TryProcessRow(_SqlMetaDataSet columns, object[] buffer, int[] map, TdsParserStateObject stateObj)
		{
			SqlBuffer sqlBuffer = new SqlBuffer();
			for (int i = 0; i < columns.Length; i++)
			{
				_SqlMetaData sqlMetaData = columns[i];
				bool flag;
				ulong num;
				if (!this.TryProcessColumnHeader(sqlMetaData, stateObj, i, out flag, out num))
				{
					return false;
				}
				if (flag)
				{
					TdsParser.GetNullSqlValue(sqlBuffer, sqlMetaData, SqlCommandColumnEncryptionSetting.Disabled, this._connHandler);
					buffer[map[i]] = sqlBuffer.SqlValue;
				}
				else
				{
					if (!this.TryReadSqlValue(sqlBuffer, sqlMetaData, sqlMetaData.metaType.IsPlp ? 2147483647 : ((int)num), stateObj, SqlCommandColumnEncryptionSetting.Disabled, sqlMetaData.column, null))
					{
						return false;
					}
					buffer[map[i]] = sqlBuffer.SqlValue;
					if (stateObj._longlen != 0UL)
					{
						throw new SqlTruncateException(StringsHelper.GetString(Strings.SqlMisc_TruncationMaxDataMessage, Array.Empty<object>()));
					}
				}
				sqlBuffer.Clear();
			}
			return true;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00055B54 File Offset: 0x00053D54
		internal static bool ShouldHonorTceForRead(SqlCommandColumnEncryptionSetting columnEncryptionSetting, SqlInternalConnectionTds connection)
		{
			switch (columnEncryptionSetting)
			{
			case SqlCommandColumnEncryptionSetting.Enabled:
				return true;
			case SqlCommandColumnEncryptionSetting.ResultSetOnly:
				return true;
			case SqlCommandColumnEncryptionSetting.Disabled:
				return false;
			default:
				return connection != null && connection.ConnectionOptions != null && connection.ConnectionOptions.ColumnEncryptionSetting == SqlConnectionColumnEncryptionSetting.Enabled;
			}
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00055B90 File Offset: 0x00053D90
		internal static object GetNullSqlValue(SqlBuffer nullVal, SqlMetaDataPriv md, SqlCommandColumnEncryptionSetting columnEncryptionSetting, SqlInternalConnectionTds connection)
		{
			SqlDbType sqlDbType = md.type;
			if (sqlDbType == SqlDbType.VarBinary && md.isEncrypted && TdsParser.ShouldHonorTceForRead(columnEncryptionSetting, connection))
			{
				sqlDbType = md.baseTI.type;
			}
			switch (sqlDbType)
			{
			case SqlDbType.BigInt:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Int64);
				break;
			case SqlDbType.Binary:
			case SqlDbType.Image:
			case SqlDbType.VarBinary:
			case SqlDbType.Udt:
				nullVal.SqlBinary = SqlBinary.Null;
				break;
			case SqlDbType.Bit:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Boolean);
				break;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NText:
			case SqlDbType.NVarChar:
			case SqlDbType.Text:
			case SqlDbType.VarChar:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.String);
				break;
			case SqlDbType.DateTime:
			case SqlDbType.SmallDateTime:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.DateTime);
				break;
			case SqlDbType.Decimal:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Decimal);
				break;
			case SqlDbType.Float:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Double);
				break;
			case SqlDbType.Int:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Int32);
				break;
			case SqlDbType.Money:
			case SqlDbType.SmallMoney:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Money);
				break;
			case SqlDbType.Real:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Single);
				break;
			case SqlDbType.UniqueIdentifier:
				nullVal.SqlGuid = SqlGuid.Null;
				break;
			case SqlDbType.SmallInt:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Int16);
				break;
			case SqlDbType.Timestamp:
				if (!LocalAppContextSwitches.LegacyRowVersionNullBehavior)
				{
					nullVal.SetToNullOfType(SqlBuffer.StorageType.SqlBinary);
				}
				break;
			case SqlDbType.TinyInt:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Byte);
				break;
			case SqlDbType.Variant:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Empty);
				break;
			case SqlDbType.Xml:
				nullVal.SqlCachedBuffer = SqlCachedBuffer.Null;
				break;
			case SqlDbType.Date:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Date);
				break;
			case SqlDbType.Time:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.Time);
				break;
			case SqlDbType.DateTime2:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.DateTime2);
				break;
			case SqlDbType.DateTimeOffset:
				nullVal.SetToNullOfType(SqlBuffer.StorageType.DateTimeOffset);
				break;
			}
			return nullVal;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00055D40 File Offset: 0x00053F40
		internal bool TrySkipRow(_SqlMetaDataSet columns, TdsParserStateObject stateObj)
		{
			return this.TrySkipRow(columns, 0, stateObj);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00055D4C File Offset: 0x00053F4C
		internal bool TrySkipRow(_SqlMetaDataSet columns, int startCol, TdsParserStateObject stateObj)
		{
			for (int i = startCol; i < columns.Length; i++)
			{
				_SqlMetaData sqlMetaData = columns[i];
				if (!this.TrySkipValue(sqlMetaData, i, stateObj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00055D80 File Offset: 0x00053F80
		internal bool TrySkipValue(SqlMetaDataPriv md, int columnOrdinal, TdsParserStateObject stateObj)
		{
			if (stateObj.IsNullCompressionBitSet(columnOrdinal))
			{
				return true;
			}
			if (md.metaType.IsPlp)
			{
				ulong num;
				if (!this.TrySkipPlpValue(18446744073709551615UL, stateObj, out num))
				{
					return false;
				}
			}
			else if (md.metaType.IsLong)
			{
				byte b;
				if (!stateObj.TryReadByte(out b))
				{
					return false;
				}
				if (b != 0)
				{
					if (!stateObj.TrySkipBytes((int)(b + 8)))
					{
						return false;
					}
					int num2;
					if (!this.TryGetTokenLength(md.tdsType, stateObj, out num2))
					{
						return false;
					}
					if (!stateObj.TrySkipBytes(num2))
					{
						return false;
					}
				}
			}
			else
			{
				int num3;
				if (!this.TryGetTokenLength(md.tdsType, stateObj, out num3))
				{
					return false;
				}
				if (!this.IsNull(md.metaType, (ulong)((long)num3)) && !stateObj.TrySkipBytes(num3))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00055E2A File Offset: 0x0005402A
		private bool IsNull(MetaType mt, ulong length)
		{
			if (mt.IsPlp)
			{
				return ulong.MaxValue == length;
			}
			return (65535UL == length && !mt.IsLong) || (length == 0UL && !mt.IsCharType && !mt.IsBinType);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00055E64 File Offset: 0x00054064
		private bool TryReadSqlStringValue(SqlBuffer value, byte type, int length, Encoding encoding, bool isPlp, TdsParserStateObject stateObj)
		{
			if (type <= 99)
			{
				if (type <= 39)
				{
					if (type != 35 && type != 39)
					{
						return true;
					}
				}
				else if (type != 47)
				{
					if (type != 99)
					{
						return true;
					}
					goto IL_007E;
				}
			}
			else if (type <= 175)
			{
				if (type != 167 && type != 175)
				{
					return true;
				}
			}
			else
			{
				if (type != 231 && type != 239)
				{
					return true;
				}
				goto IL_007E;
			}
			if (encoding == null)
			{
				encoding = this._defaultEncoding;
			}
			string text;
			if (!stateObj.TryReadStringWithEncoding(length, encoding, isPlp, out text))
			{
				return false;
			}
			value.SetToString(text);
			return true;
			IL_007E:
			string text2 = null;
			if (isPlp)
			{
				char[] array = null;
				if (!this.TryReadPlpUnicodeChars(ref array, 0, length >> 1, stateObj, out length))
				{
					return false;
				}
				if (length > 0)
				{
					text2 = new string(array, 0, length);
				}
				else
				{
					text2 = "";
				}
			}
			else if (!stateObj.TryReadString(length >> 1, out text2))
			{
				return false;
			}
			value.SetToString(text2);
			return true;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00055F3C File Offset: 0x0005413C
		internal bool DeserializeUnencryptedValue(SqlBuffer value, byte[] unencryptedBytes, SqlMetaDataPriv md, TdsParserStateObject stateObj, byte normalizationVersion)
		{
			if (normalizationVersion != 1)
			{
				throw SQL.UnsupportedNormalizationVersion(normalizationVersion);
			}
			byte tdsType = md.baseTI.tdsType;
			int num = unencryptedBytes.Length;
			int length = md.baseTI.length;
			byte scale = md.baseTI.scale;
			if (tdsType <= 165)
			{
				if (tdsType <= 111)
				{
					switch (tdsType)
					{
					case 34:
					case 37:
					case 45:
						goto IL_02D2;
					case 35:
					case 39:
					case 47:
						goto IL_0383;
					case 36:
						value.SqlGuid = SqlTypeWorkarounds.SqlGuidCtor(unencryptedBytes, true);
						return true;
					case 38:
					case 48:
					case 50:
					case 52:
					case 56:
						goto IL_016D;
					case 40:
						value.SetToDate(unencryptedBytes);
						return true;
					case 41:
						value.SetToTime(unencryptedBytes, 7, scale);
						return true;
					case 42:
						value.SetToDateTime2(unencryptedBytes, 7, scale);
						return true;
					case 43:
						value.SetToDateTimeOffset(unencryptedBytes, 7, scale);
						return true;
					case 44:
					case 46:
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
						goto IL_0457;
					case 58:
						goto IL_0262;
					case 59:
						break;
					case 60:
						goto IL_022B;
					case 61:
						goto IL_0297;
					case 62:
						goto IL_020D;
					default:
						switch (tdsType)
						{
						case 99:
							goto IL_03E2;
						case 100:
						case 101:
						case 102:
						case 103:
						case 105:
						case 107:
							goto IL_0457;
						case 104:
							goto IL_016D;
						case 106:
						case 108:
						{
							int num2 = 0;
							byte b = unencryptedBytes[num2++];
							bool flag = 1 == b;
							int[] array;
							int num3;
							checked
							{
								num--;
								array = new int[4];
								num3 = num >> 2;
							}
							for (int i = 0; i < num3; i++)
							{
								array[i] = BitConverter.ToInt32(unencryptedBytes, num2);
								num2 += 4;
							}
							value.SetToDecimal(md.baseTI.precision, md.baseTI.scale, flag, array);
							return true;
						}
						case 109:
							if (num != 4)
							{
								goto IL_020D;
							}
							break;
						case 110:
							goto IL_022B;
						case 111:
							if (num == 4)
							{
								goto IL_0262;
							}
							goto IL_0297;
						default:
							goto IL_0457;
						}
						break;
					}
					if (unencryptedBytes.Length != 4)
					{
						return false;
					}
					float num4 = BitConverter.ToSingle(unencryptedBytes, 0);
					value.Single = num4;
					return true;
					IL_020D:
					if (unencryptedBytes.Length != 8)
					{
						return false;
					}
					double num5 = BitConverter.ToDouble(unencryptedBytes, 0);
					value.Double = num5;
					return true;
					IL_0262:
					if (unencryptedBytes.Length != 4)
					{
						return false;
					}
					ushort num6 = (ushort)(((int)unencryptedBytes[1] << 8) + (int)unencryptedBytes[0]);
					ushort num7 = (ushort)(((int)unencryptedBytes[3] << 8) + (int)unencryptedBytes[2]);
					value.SetToDateTime((int)num6, (int)num7 * SqlDateTime.SQLTicksPerMinute);
					return true;
					IL_0297:
					if (unencryptedBytes.Length != 8)
					{
						return false;
					}
					int num8 = BitConverter.ToInt32(unencryptedBytes, 0);
					uint num9 = BitConverter.ToUInt32(unencryptedBytes, 4);
					value.SetToDateTime(num8, (int)num9);
					return true;
				}
				else
				{
					if (tdsType == 122)
					{
						goto IL_022B;
					}
					if (tdsType != 127)
					{
						if (tdsType != 165)
						{
							goto IL_0457;
						}
						goto IL_02D2;
					}
				}
				IL_016D:
				if (unencryptedBytes.Length != 8)
				{
					return false;
				}
				long num10 = BitConverter.ToInt64(unencryptedBytes, 0);
				if (tdsType == 50 || tdsType == 104)
				{
					value.Boolean = num10 != 0L;
					return true;
				}
				if (tdsType == 48 || length == 1)
				{
					value.Byte = (byte)num10;
					return true;
				}
				if (tdsType == 52 || length == 2)
				{
					value.Int16 = (short)num10;
					return true;
				}
				if (tdsType == 56 || length == 4)
				{
					value.Int32 = (int)num10;
					return true;
				}
				value.Int64 = num10;
				return true;
				IL_022B:
				if (unencryptedBytes.Length != 8)
				{
					return false;
				}
				int num11 = BitConverter.ToInt32(unencryptedBytes, 0);
				uint num12 = BitConverter.ToUInt32(unencryptedBytes, 4);
				long num13 = ((long)num11 << 32) + (long)((ulong)num12);
				value.SetToMoney(num13);
				return true;
			}
			else if (tdsType <= 173)
			{
				if (tdsType == 167)
				{
					goto IL_0383;
				}
				if (tdsType != 173)
				{
					goto IL_0457;
				}
			}
			else
			{
				if (tdsType == 175)
				{
					goto IL_0383;
				}
				if (tdsType != 231 && tdsType != 239)
				{
					goto IL_0457;
				}
				goto IL_03E2;
			}
			IL_02D2:
			if (tdsType == 45 || tdsType == 173)
			{
				byte[] array2 = new byte[md.baseTI.length];
				Buffer.BlockCopy(unencryptedBytes, 0, array2, 0, unencryptedBytes.Length);
				unencryptedBytes = array2;
			}
			value.SqlBinary = SqlTypeWorkarounds.SqlBinaryCtor(unencryptedBytes, true);
			return true;
			IL_0383:
			Encoding encoding = md.baseTI.encoding;
			if (encoding == null)
			{
				encoding = this._defaultEncoding;
			}
			if (encoding == null)
			{
				this.ThrowUnsupportedCollationEncountered(stateObj);
			}
			string text = encoding.GetString(unencryptedBytes, 0, num);
			if (tdsType == 47 || tdsType == 175)
			{
				text = text.PadRight(md.baseTI.length);
			}
			value.SetToString(text);
			return true;
			IL_03E2:
			string text2 = Encoding.Unicode.GetString(unencryptedBytes, 0, num);
			if (tdsType == 239)
			{
				text2 = text2.PadRight(md.baseTI.length / 2);
			}
			value.SetToString(text2);
			return true;
			IL_0457:
			MetaType metaType = md.baseTI.metaType;
			if (metaType == null)
			{
				metaType = MetaType.GetSqlDataType((int)tdsType, 0U, num);
			}
			throw SQL.UnsupportedDatatypeEncryption(metaType.TypeName);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000563CC File Offset: 0x000545CC
		internal bool TryReadSqlValue(SqlBuffer value, SqlMetaDataPriv md, int length, TdsParserStateObject stateObj, SqlCommandColumnEncryptionSetting columnEncryptionOverride, string columnName, SqlCommand command = null)
		{
			bool isPlp = md.metaType.IsPlp;
			byte tdsType = md.tdsType;
			if (isPlp)
			{
				length = int.MaxValue;
			}
			if (tdsType <= 165)
			{
				if (tdsType <= 99)
				{
					switch (tdsType)
					{
					case 34:
					case 37:
					case 45:
						break;
					case 35:
					case 39:
					case 47:
						goto IL_01CC;
					case 36:
					case 38:
					case 44:
					case 46:
						goto IL_020F;
					case 40:
					case 41:
					case 42:
					case 43:
						if (!this.TryReadSqlDateTime(value, tdsType, length, md.scale, stateObj))
						{
							return false;
						}
						return true;
					default:
						if (tdsType != 99)
						{
							goto IL_020F;
						}
						goto IL_01CC;
					}
				}
				else if (tdsType != 106 && tdsType != 108)
				{
					if (tdsType != 165)
					{
						goto IL_020F;
					}
				}
				else
				{
					if (!this.TryReadSqlDecimal(value, length, md.precision, md.scale, stateObj))
					{
						return false;
					}
					return true;
				}
			}
			else if (tdsType <= 173)
			{
				if (tdsType == 167)
				{
					goto IL_01CC;
				}
				if (tdsType != 173)
				{
					goto IL_020F;
				}
			}
			else
			{
				if (tdsType == 175 || tdsType == 231)
				{
					goto IL_01CC;
				}
				switch (tdsType)
				{
				case 239:
					goto IL_01CC;
				case 240:
					break;
				case 241:
				{
					SqlCachedBuffer sqlCachedBuffer;
					if (!SqlCachedBuffer.TryCreate(md, this, stateObj, out sqlCachedBuffer))
					{
						return false;
					}
					value.SqlCachedBuffer = sqlCachedBuffer;
					return true;
				}
				default:
					goto IL_020F;
				}
			}
			byte[] array = null;
			if (isPlp)
			{
				int num;
				if (!stateObj.TryReadPlpBytes(ref array, 0, length, out num))
				{
					return false;
				}
			}
			else
			{
				array = new byte[length];
				if (!stateObj.TryReadByteArray(array, length))
				{
					return false;
				}
			}
			if (md.isEncrypted && (columnEncryptionOverride == SqlCommandColumnEncryptionSetting.Enabled || columnEncryptionOverride == SqlCommandColumnEncryptionSetting.ResultSetOnly || (columnEncryptionOverride == SqlCommandColumnEncryptionSetting.UseConnectionSetting && this._connHandler != null && this._connHandler.ConnectionOptions != null && this._connHandler.ConnectionOptions.ColumnEncryptionSetting == SqlConnectionColumnEncryptionSetting.Enabled)))
			{
				try
				{
					byte[] array2 = SqlSecurityUtility.DecryptWithKey(array, md.cipherMD, this._connHandler.Connection, command);
					if (array2 != null)
					{
						this.DeserializeUnencryptedValue(value, array2, md, stateObj, md.NormalizationRuleVersion);
					}
					return true;
				}
				catch (Exception ex)
				{
					if (stateObj != null)
					{
						stateObj._pendingData = false;
					}
					throw SQL.ColumnDecryptionFailed(columnName, null, ex);
				}
			}
			value.SqlBinary = SqlTypeWorkarounds.SqlBinaryCtor(array, true);
			return true;
			IL_01CC:
			if (!this.TryReadSqlStringValue(value, tdsType, length, md.encoding, isPlp, stateObj))
			{
				return false;
			}
			return true;
			IL_020F:
			if (!this.TryReadSqlValueInternal(value, tdsType, length, stateObj))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00056608 File Offset: 0x00054808
		private unsafe bool TryReadSqlDateTime(SqlBuffer value, byte tdsType, int length, byte scale, TdsParserStateObject stateObj)
		{
			Span<byte> span2;
			if (length <= 16)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)16], 16);
				span2 = span;
			}
			else
			{
				span2 = new byte[length];
			}
			Span<byte> span3 = span2;
			if (!stateObj.TryReadByteArray(span3, length))
			{
				return false;
			}
			ReadOnlySpan<byte> readOnlySpan = span3.Slice(0, length);
			switch (tdsType)
			{
			case 40:
				value.SetToDate(readOnlySpan);
				break;
			case 41:
				value.SetToTime(readOnlySpan, scale, scale);
				break;
			case 42:
				value.SetToDateTime2(readOnlySpan, scale, scale);
				break;
			case 43:
				value.SetToDateTimeOffset(readOnlySpan, scale, scale);
				break;
			}
			return true;
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x000566A0 File Offset: 0x000548A0
		internal bool TryReadSqlValueInternal(SqlBuffer value, byte tdsType, int length, TdsParserStateObject stateObj)
		{
			if (tdsType <= 104)
			{
				byte b;
				if (tdsType <= 62)
				{
					switch (tdsType)
					{
					case 34:
					case 37:
						goto IL_028B;
					case 35:
						return true;
					case 36:
					{
						byte[] array = this._tempGuidBytes;
						if (array == null)
						{
							array = new byte[16];
						}
						if (!stateObj.TryReadByteArray(array, length))
						{
							return false;
						}
						value.Guid = new Guid(array);
						this._tempGuidBytes = array;
						return true;
					}
					case 38:
						if (length != 1)
						{
							if (length == 2)
							{
								goto IL_011F;
							}
							if (length == 4)
							{
								goto IL_0138;
							}
							goto IL_0151;
						}
						break;
					default:
						switch (tdsType)
						{
						case 45:
							goto IL_028B;
						case 46:
						case 47:
						case 49:
						case 51:
						case 53:
						case 54:
						case 55:
						case 57:
							return true;
						case 48:
							break;
						case 50:
							goto IL_00DC;
						case 52:
							goto IL_011F;
						case 56:
							goto IL_0138;
						case 58:
							goto IL_01F7;
						case 59:
							goto IL_016E;
						case 60:
							goto IL_01A6;
						case 61:
							goto IL_0226;
						case 62:
							goto IL_0188;
						default:
							return true;
						}
						break;
					}
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					value.Byte = b;
					return true;
					IL_011F:
					short num;
					if (!stateObj.TryReadInt16(out num))
					{
						return false;
					}
					value.Int16 = num;
					return true;
					IL_0138:
					int num2;
					if (!stateObj.TryReadInt32(out num2))
					{
						return false;
					}
					value.Int32 = num2;
					return true;
				}
				else if (tdsType != 98)
				{
					if (tdsType != 104)
					{
						return true;
					}
				}
				else
				{
					if (!this.TryReadSqlVariant(value, length, stateObj))
					{
						return false;
					}
					return true;
				}
				IL_00DC:
				if (!stateObj.TryReadByte(out b))
				{
					return false;
				}
				value.Boolean = b > 0;
				return true;
			}
			else if (tdsType <= 122)
			{
				switch (tdsType)
				{
				case 109:
					if (length == 4)
					{
						goto IL_016E;
					}
					goto IL_0188;
				case 110:
					if (length != 4)
					{
						goto IL_01A6;
					}
					break;
				case 111:
					if (length == 4)
					{
						goto IL_01F7;
					}
					goto IL_0226;
				default:
					if (tdsType != 122)
					{
						return true;
					}
					break;
				}
				int num2;
				if (!stateObj.TryReadInt32(out num2))
				{
					return false;
				}
				value.SetToMoney((long)num2);
				return true;
			}
			else if (tdsType != 127)
			{
				if (tdsType != 165 && tdsType != 173)
				{
					return true;
				}
				goto IL_028B;
			}
			IL_0151:
			long num3;
			if (!stateObj.TryReadInt64(out num3))
			{
				return false;
			}
			value.Int64 = num3;
			return true;
			IL_016E:
			float num4;
			if (!stateObj.TryReadSingle(out num4))
			{
				return false;
			}
			value.Single = num4;
			return true;
			IL_0188:
			double num5;
			if (!stateObj.TryReadDouble(out num5))
			{
				return false;
			}
			value.Double = num5;
			return true;
			IL_01A6:
			int num6;
			if (!stateObj.TryReadInt32(out num6))
			{
				return false;
			}
			uint num7;
			if (!stateObj.TryReadUInt32(out num7))
			{
				return false;
			}
			long num8 = ((long)num6 << 32) + (long)((ulong)num7);
			value.SetToMoney(num8);
			return true;
			IL_01F7:
			ushort num9;
			if (!stateObj.TryReadUInt16(out num9))
			{
				return false;
			}
			ushort num10;
			if (!stateObj.TryReadUInt16(out num10))
			{
				return false;
			}
			value.SetToDateTime((int)num9, (int)num10 * SqlDateTime.SQLTicksPerMinute);
			return true;
			IL_0226:
			int num11;
			if (!stateObj.TryReadInt32(out num11))
			{
				return false;
			}
			uint num12;
			if (!stateObj.TryReadUInt32(out num12))
			{
				return false;
			}
			value.SetToDateTime(num11, (int)num12);
			return true;
			IL_028B:
			byte[] array2 = new byte[length];
			if (!stateObj.TryReadByteArray(array2, length))
			{
				return false;
			}
			value.SqlBinary = SqlTypeWorkarounds.SqlBinaryCtor(array2, true);
			return true;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00056974 File Offset: 0x00054B74
		internal bool TryReadSqlVariant(SqlBuffer value, int lenTotal, TdsParserStateObject stateObj)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			ushort num = 0;
			byte b2;
			if (!stateObj.TryReadByte(out b2))
			{
				return false;
			}
			MetaType sqlDataType = MetaType.GetSqlDataType((int)b, 0U, 0);
			byte propBytes = sqlDataType.PropBytes;
			int num2 = (int)(2 + b2);
			int num3 = lenTotal - num2;
			if (b <= 127)
			{
				if (b <= 106)
				{
					switch (b)
					{
					case 36:
					case 48:
					case 50:
					case 52:
					case 56:
					case 58:
					case 59:
					case 60:
					case 61:
					case 62:
						goto IL_0121;
					case 37:
					case 38:
					case 39:
					case 44:
					case 45:
					case 46:
					case 47:
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
						return true;
					case 40:
						if (!this.TryReadSqlDateTime(value, b, num3, 0, stateObj))
						{
							return false;
						}
						return true;
					case 41:
					case 42:
					case 43:
					{
						byte b3;
						if (!stateObj.TryReadByte(out b3))
						{
							return false;
						}
						if (b2 > propBytes && !stateObj.TrySkipBytes((int)(b2 - propBytes)))
						{
							return false;
						}
						if (!this.TryReadSqlDateTime(value, b, num3, b3, stateObj))
						{
							return false;
						}
						return true;
					}
					default:
						if (b != 106)
						{
							return true;
						}
						break;
					}
				}
				else if (b != 108)
				{
					if (b != 122 && b != 127)
					{
						return true;
					}
					goto IL_0121;
				}
				byte b4;
				if (!stateObj.TryReadByte(out b4))
				{
					return false;
				}
				byte b5;
				if (!stateObj.TryReadByte(out b5))
				{
					return false;
				}
				if (b2 > propBytes && !stateObj.TrySkipBytes((int)(b2 - propBytes)))
				{
					return false;
				}
				if (!this.TryReadSqlDecimal(value, 17, b4, b5, stateObj))
				{
					return false;
				}
				return true;
			}
			else
			{
				if (b <= 173)
				{
					if (b != 165)
					{
						if (b == 167)
						{
							goto IL_0192;
						}
						if (b != 173)
						{
							return true;
						}
					}
					if (!stateObj.TryReadUInt16(out num))
					{
						return false;
					}
					if (b2 > propBytes && !stateObj.TrySkipBytes((int)(b2 - propBytes)))
					{
						return false;
					}
					goto IL_0121;
				}
				else if (b != 175 && b != 231 && b != 239)
				{
					return true;
				}
				IL_0192:
				SqlCollation sqlCollation;
				if (!this.TryProcessCollation(stateObj, out sqlCollation))
				{
					return false;
				}
				if (!stateObj.TryReadUInt16(out num))
				{
					return false;
				}
				if (b2 > propBytes && !stateObj.TrySkipBytes((int)(b2 - propBytes)))
				{
					return false;
				}
				Encoding encoding = Encoding.GetEncoding(this.GetCodePage(sqlCollation, stateObj));
				if (!this.TryReadSqlStringValue(value, b, num3, encoding, false, stateObj))
				{
					return false;
				}
				return true;
			}
			IL_0121:
			if (!this.TryReadSqlValueInternal(value, b, num3, stateObj))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00056BA4 File Offset: 0x00054DA4
		internal Task WriteSqlVariantValue(object value, int length, int offset, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			if (ADP.IsNull(value))
			{
				this.WriteInt(0, stateObj);
				this.WriteInt(0, stateObj);
				return null;
			}
			MetaType metaType = MetaType.GetMetaTypeFromValue(value, true);
			if (108 == metaType.TDSType && 8 == length)
			{
				metaType = MetaType.GetMetaTypeFromValue(new SqlMoney((decimal)value), true);
			}
			if (metaType.IsAnsiType)
			{
				length = this.GetEncodingCharLength((string)value, length, 0, this._defaultEncoding);
			}
			this.WriteInt((int)(2 + metaType.PropBytes) + length, stateObj);
			this.WriteInt((int)(2 + metaType.PropBytes) + length, stateObj);
			stateObj.WriteByte(metaType.TDSType);
			stateObj.WriteByte(metaType.PropBytes);
			byte tdstype = metaType.TDSType;
			if (tdstype <= 62)
			{
				if (tdstype <= 41)
				{
					if (tdstype != 36)
					{
						if (tdstype == 41)
						{
							stateObj.WriteByte(metaType.Scale);
							this.WriteTime((TimeSpan)value, metaType.Scale, length, stateObj);
						}
					}
					else
					{
						byte[] array = ((Guid)value).ToByteArray();
						stateObj.WriteByteArray(array, length, 0, true, null);
					}
				}
				else if (tdstype != 43)
				{
					switch (tdstype)
					{
					case 48:
						stateObj.WriteByte((byte)value);
						break;
					case 50:
						if ((bool)value)
						{
							stateObj.WriteByte(1);
						}
						else
						{
							stateObj.WriteByte(0);
						}
						break;
					case 52:
						this.WriteShort((int)((short)value), stateObj);
						break;
					case 56:
						this.WriteInt((int)value, stateObj);
						break;
					case 59:
						this.WriteFloat((float)value, stateObj);
						break;
					case 60:
						this.WriteCurrency((decimal)value, 8, stateObj);
						break;
					case 61:
					{
						TdsDateTime tdsDateTime = MetaType.FromDateTime((DateTime)value, 8);
						this.WriteInt(tdsDateTime.days, stateObj);
						this.WriteInt(tdsDateTime.time, stateObj);
						break;
					}
					case 62:
						this.WriteDouble((double)value, stateObj);
						break;
					}
				}
				else
				{
					stateObj.WriteByte(metaType.Scale);
					this.WriteDateTimeOffset((DateTimeOffset)value, metaType.Scale, length, stateObj);
				}
			}
			else if (tdstype <= 127)
			{
				if (tdstype != 108)
				{
					if (tdstype == 127)
					{
						this.WriteLong((long)value, stateObj);
					}
				}
				else
				{
					stateObj.WriteByte(metaType.Precision);
					stateObj.WriteByte((byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16));
					this.WriteDecimal((decimal)value, stateObj);
				}
			}
			else
			{
				if (tdstype == 165)
				{
					byte[] array2 = (byte[])value;
					this.WriteShort(length, stateObj);
					return stateObj.WriteByteArray(array2, length, offset, canAccumulate, null);
				}
				if (tdstype == 167)
				{
					string text = (string)value;
					this.WriteUnsignedInt(this._defaultCollation._info, stateObj);
					stateObj.WriteByte(this._defaultCollation._sortId);
					this.WriteShort(length, stateObj);
					return this.WriteEncodingChar(text, this._defaultEncoding, stateObj, canAccumulate);
				}
				if (tdstype == 231)
				{
					string text2 = (string)value;
					this.WriteUnsignedInt(this._defaultCollation._info, stateObj);
					stateObj.WriteByte(this._defaultCollation._sortId);
					this.WriteShort(length, stateObj);
					length >>= 1;
					return this.WriteString(text2, length, offset, stateObj, canAccumulate);
				}
			}
			return null;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00056F38 File Offset: 0x00055138
		internal Task WriteSqlVariantDataRowValue(object value, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			if (value == null || DBNull.Value == value)
			{
				this.WriteInt(0, stateObj);
				return null;
			}
			MetaType metaTypeFromValue = MetaType.GetMetaTypeFromValue(value, true);
			int num = 0;
			if (metaTypeFromValue.IsAnsiType)
			{
				num = this.GetEncodingCharLength((string)value, num, 0, this._defaultEncoding);
			}
			byte tdstype = metaTypeFromValue.TDSType;
			if (tdstype <= 62)
			{
				if (tdstype <= 41)
				{
					if (tdstype != 36)
					{
						if (tdstype == 41)
						{
							this.WriteSqlVariantHeader(8, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
							stateObj.WriteByte(metaTypeFromValue.Scale);
							this.WriteTime((TimeSpan)value, metaTypeFromValue.Scale, 5, stateObj);
						}
					}
					else
					{
						byte[] array = ((Guid)value).ToByteArray();
						num = array.Length;
						this.WriteSqlVariantHeader(18, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						stateObj.WriteByteArray(array, num, 0, true, null);
					}
				}
				else if (tdstype != 43)
				{
					switch (tdstype)
					{
					case 48:
						this.WriteSqlVariantHeader(3, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						stateObj.WriteByte((byte)value);
						break;
					case 50:
						this.WriteSqlVariantHeader(3, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						if ((bool)value)
						{
							stateObj.WriteByte(1);
						}
						else
						{
							stateObj.WriteByte(0);
						}
						break;
					case 52:
						this.WriteSqlVariantHeader(4, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteShort((int)((short)value), stateObj);
						break;
					case 56:
						this.WriteSqlVariantHeader(6, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteInt((int)value, stateObj);
						break;
					case 59:
						this.WriteSqlVariantHeader(6, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteFloat((float)value, stateObj);
						break;
					case 60:
						this.WriteSqlVariantHeader(10, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteCurrency((decimal)value, 8, stateObj);
						break;
					case 61:
					{
						TdsDateTime tdsDateTime = MetaType.FromDateTime((DateTime)value, 8);
						this.WriteSqlVariantHeader(10, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteInt(tdsDateTime.days, stateObj);
						this.WriteInt(tdsDateTime.time, stateObj);
						break;
					}
					case 62:
						this.WriteSqlVariantHeader(10, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteDouble((double)value, stateObj);
						break;
					}
				}
				else
				{
					this.WriteSqlVariantHeader(13, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
					stateObj.WriteByte(metaTypeFromValue.Scale);
					this.WriteDateTimeOffset((DateTimeOffset)value, metaTypeFromValue.Scale, 10, stateObj);
				}
			}
			else if (tdstype <= 127)
			{
				if (tdstype != 108)
				{
					if (tdstype == 127)
					{
						this.WriteSqlVariantHeader(10, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
						this.WriteLong((long)value, stateObj);
					}
				}
				else
				{
					this.WriteSqlVariantHeader(21, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
					stateObj.WriteByte(metaTypeFromValue.Precision);
					stateObj.WriteByte((byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16));
					this.WriteDecimal((decimal)value, stateObj);
				}
			}
			else
			{
				if (tdstype == 165)
				{
					byte[] array2 = (byte[])value;
					num = array2.Length;
					this.WriteSqlVariantHeader(4 + num, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
					this.WriteShort(num, stateObj);
					return stateObj.WriteByteArray(array2, num, 0, canAccumulate, null);
				}
				if (tdstype == 167)
				{
					string text = (string)value;
					num = text.Length;
					this.WriteSqlVariantHeader(9 + num, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
					this.WriteUnsignedInt(this._defaultCollation._info, stateObj);
					stateObj.WriteByte(this._defaultCollation._sortId);
					this.WriteShort(num, stateObj);
					return this.WriteEncodingChar(text, this._defaultEncoding, stateObj, canAccumulate);
				}
				if (tdstype == 231)
				{
					string text2 = (string)value;
					num = text2.Length * 2;
					this.WriteSqlVariantHeader(9 + num, metaTypeFromValue.TDSType, metaTypeFromValue.PropBytes, stateObj);
					this.WriteUnsignedInt(this._defaultCollation._info, stateObj);
					stateObj.WriteByte(this._defaultCollation._sortId);
					this.WriteShort(num, stateObj);
					num >>= 1;
					return this.WriteString(text2, num, 0, stateObj, canAccumulate);
				}
			}
			return null;
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x000573AF File Offset: 0x000555AF
		internal void WriteSqlVariantHeader(int length, byte tdstype, byte propbytes, TdsParserStateObject stateObj)
		{
			this.WriteInt(length, stateObj);
			stateObj.WriteByte(tdstype);
			stateObj.WriteByte(propbytes);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000573CC File Offset: 0x000555CC
		internal void WriteSqlVariantDateTime2(DateTime value, TdsParserStateObject stateObj)
		{
			SmiMetaData defaultDateTime = SmiMetaData.DefaultDateTime2;
			this.WriteSqlVariantHeader((int)(defaultDateTime.MaxLength + 3L), 42, 1, stateObj);
			stateObj.WriteByte(defaultDateTime.Scale);
			this.WriteDateTime2(value, defaultDateTime.Scale, (int)defaultDateTime.MaxLength, stateObj);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00057414 File Offset: 0x00055614
		internal void WriteSqlVariantDate(DateTime value, TdsParserStateObject stateObj)
		{
			SmiMetaData defaultDate = SmiMetaData.DefaultDate;
			this.WriteSqlVariantHeader((int)(defaultDate.MaxLength + 2L), 40, 0, stateObj);
			this.WriteDate(value, stateObj);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00057443 File Offset: 0x00055643
		private byte[] SerializeSqlMoney(SqlMoney value, int length, TdsParserStateObject stateObj)
		{
			return this.SerializeCurrency(value.Value, length, stateObj);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00057454 File Offset: 0x00055654
		private void WriteSqlMoney(SqlMoney value, int length, TdsParserStateObject stateObj)
		{
			int[] bits = decimal.GetBits(value.Value);
			bool flag = (bits[3] & int.MinValue) != 0;
			long num = (long)(((ulong)bits[1] << 32) | (ulong)bits[0]);
			if (flag)
			{
				num = -num;
			}
			if (length != 4)
			{
				this.WriteInt((int)(num >> 32), stateObj);
				this.WriteInt((int)num, stateObj);
				return;
			}
			decimal value2 = value.Value;
			if (value2 < TdsEnums.SQL_SMALL_MONEY_MIN || value2 > TdsEnums.SQL_SMALL_MONEY_MAX)
			{
				throw SQL.MoneyOverflow(value2.ToString(CultureInfo.InvariantCulture));
			}
			this.WriteInt((int)num, stateObj);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x000574E8 File Offset: 0x000556E8
		private byte[] SerializeCurrency(decimal value, int length, TdsParserStateObject stateObj)
		{
			SqlMoney sqlMoney = new SqlMoney(value);
			int[] bits = decimal.GetBits(sqlMoney.Value);
			bool flag = (bits[3] & int.MinValue) != 0;
			long num = (long)(((ulong)bits[1] << 32) | (ulong)bits[0]);
			if (flag)
			{
				num = -num;
			}
			if (length == 4)
			{
				if (value < TdsEnums.SQL_SMALL_MONEY_MIN || value > TdsEnums.SQL_SMALL_MONEY_MAX)
				{
					throw SQL.MoneyOverflow(value.ToString(CultureInfo.InvariantCulture));
				}
				length = 8;
			}
			if (stateObj._bLongBytes == null)
			{
				stateObj._bLongBytes = new byte[8];
			}
			byte[] bLongBytes = stateObj._bLongBytes;
			int num2 = 0;
			byte[] array = this.SerializeInt((int)(num >> 32), stateObj);
			Buffer.BlockCopy(array, 0, bLongBytes, num2, 4);
			num2 += 4;
			array = this.SerializeInt((int)num, stateObj);
			Buffer.BlockCopy(array, 0, bLongBytes, num2, 4);
			return bLongBytes;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x000575B8 File Offset: 0x000557B8
		private void WriteCurrency(decimal value, int length, TdsParserStateObject stateObj)
		{
			SqlMoney sqlMoney = new SqlMoney(value);
			int[] bits = decimal.GetBits(sqlMoney.Value);
			bool flag = (bits[3] & int.MinValue) != 0;
			long num = (long)(((ulong)bits[1] << 32) | (ulong)bits[0]);
			if (flag)
			{
				num = -num;
			}
			if (length != 4)
			{
				this.WriteInt((int)(num >> 32), stateObj);
				this.WriteInt((int)num, stateObj);
				return;
			}
			if (value < TdsEnums.SQL_SMALL_MONEY_MIN || value > TdsEnums.SQL_SMALL_MONEY_MAX)
			{
				throw SQL.MoneyOverflow(value.ToString(CultureInfo.InvariantCulture));
			}
			this.WriteInt((int)num, stateObj);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0005764C File Offset: 0x0005584C
		private byte[] SerializeDate(DateTime value)
		{
			long num = (long)value.Subtract(DateTime.MinValue).Days;
			return this.SerializePartialLong(num, 3);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00057678 File Offset: 0x00055878
		private void WriteDate(DateTime value, TdsParserStateObject stateObj)
		{
			long num = (long)value.Subtract(DateTime.MinValue).Days;
			this.WritePartialLong(num, 3, stateObj);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x000576A4 File Offset: 0x000558A4
		private byte[] SerializeTime(TimeSpan value, byte scale, int length)
		{
			if (0L > value.Ticks || value.Ticks >= 864000000000L)
			{
				throw SQL.TimeOverflow(value.ToString());
			}
			long num = value.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			num *= TdsEnums.TICKS_FROM_SCALE[(int)scale];
			length = 5;
			return this.SerializePartialLong(num, length);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00057708 File Offset: 0x00055908
		private void WriteTime(TimeSpan value, byte scale, int length, TdsParserStateObject stateObj)
		{
			if (0L > value.Ticks || value.Ticks >= 864000000000L)
			{
				throw SQL.TimeOverflow(value.ToString());
			}
			long num = value.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			this.WritePartialLong(num, length, stateObj);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00057760 File Offset: 0x00055960
		private byte[] SerializeDateTime2(DateTime value, byte scale, int length)
		{
			long num = value.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			num *= TdsEnums.TICKS_FROM_SCALE[(int)scale];
			length = 8;
			byte[] array = new byte[length];
			int num2 = 0;
			byte[] array2 = this.SerializePartialLong(num, length - 3);
			Buffer.BlockCopy(array2, 0, array, num2, length - 3);
			num2 += length - 3;
			array2 = this.SerializeDate(value);
			Buffer.BlockCopy(array2, 0, array, num2, 3);
			return array;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000577CC File Offset: 0x000559CC
		private void WriteDateTime2(DateTime value, byte scale, int length, TdsParserStateObject stateObj)
		{
			long num = value.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			this.WritePartialLong(num, length - 3, stateObj);
			this.WriteDate(value, stateObj);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00057808 File Offset: 0x00055A08
		private byte[] SerializeDateTimeOffset(DateTimeOffset value, byte scale, int length)
		{
			int num = 0;
			byte[] array = this.SerializeDateTime2(value.UtcDateTime, scale, length - 2);
			length = array.Length + 2;
			byte[] array2 = new byte[length];
			Buffer.BlockCopy(array, 0, array2, num, length - 2);
			num += length - 2;
			short num2 = (short)value.Offset.TotalMinutes;
			array2[num++] = (byte)(num2 & 255);
			array2[num++] = (byte)((num2 >> 8) & 255);
			return array2;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0005787C File Offset: 0x00055A7C
		private void WriteDateTimeOffset(DateTimeOffset value, byte scale, int length, TdsParserStateObject stateObj)
		{
			this.WriteDateTime2(value.UtcDateTime, scale, length - 2, stateObj);
			short num = (short)value.Offset.TotalMinutes;
			stateObj.WriteByte((byte)(num & 255));
			stateObj.WriteByte((byte)((num >> 8) & 255));
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x000578D0 File Offset: 0x00055AD0
		private bool TryReadSqlDecimal(SqlBuffer value, int length, byte precision, byte scale, TdsParserStateObject stateObj)
		{
			byte b;
			if (!stateObj.TryReadByte(out b))
			{
				return false;
			}
			bool flag = 1 == b;
			checked
			{
				length--;
				int[] array;
				if (!this.TryReadDecimalBits(length, stateObj, out array))
				{
					return false;
				}
				value.SetToDecimal(precision, scale, flag, array);
				return true;
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00057910 File Offset: 0x00055B10
		private bool TryReadDecimalBits(int length, TdsParserStateObject stateObj, out int[] bits)
		{
			bits = stateObj._decimalBits;
			if (bits == null)
			{
				bits = new int[4];
			}
			else
			{
				for (int i = 0; i < bits.Length; i++)
				{
					bits[i] = 0;
				}
			}
			int num = length >> 2;
			for (int i = 0; i < num; i++)
			{
				if (!stateObj.TryReadInt32(out bits[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0005796C File Offset: 0x00055B6C
		internal static SqlDecimal AdjustSqlDecimalScale(SqlDecimal d, int newScale)
		{
			if ((int)d.Scale != newScale)
			{
				bool flag = !TdsParser.EnableTruncateSwitch;
				return SqlDecimal.AdjustScale(d, newScale - (int)d.Scale, flag);
			}
			return d;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000579A0 File Offset: 0x00055BA0
		internal static decimal AdjustDecimalScale(decimal value, int newScale)
		{
			int num = (decimal.GetBits(value)[3] & 16711680) >> 16;
			if (newScale != num)
			{
				bool flag = !TdsParser.EnableTruncateSwitch;
				SqlDecimal sqlDecimal = new SqlDecimal(value);
				sqlDecimal = SqlDecimal.AdjustScale(sqlDecimal, newScale - num, flag);
				return sqlDecimal.Value;
			}
			return value;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000579E8 File Offset: 0x00055BE8
		internal byte[] SerializeSqlDecimal(SqlDecimal d, TdsParserStateObject stateObj)
		{
			if (stateObj._bDecimalBytes == null)
			{
				stateObj._bDecimalBytes = new byte[17];
			}
			byte[] bDecimalBytes = stateObj._bDecimalBytes;
			int num = 0;
			if (d.IsPositive)
			{
				bDecimalBytes[num++] = 1;
			}
			else
			{
				bDecimalBytes[num++] = 0;
			}
			uint num2;
			uint num3;
			uint num4;
			uint num5;
			SqlTypeWorkarounds.SqlDecimalExtractData(d, out num2, out num3, out num4, out num5);
			byte[] array = this.SerializeUnsignedInt(num2, stateObj);
			Buffer.BlockCopy(array, 0, bDecimalBytes, num, 4);
			num += 4;
			array = this.SerializeUnsignedInt(num3, stateObj);
			Buffer.BlockCopy(array, 0, bDecimalBytes, num, 4);
			num += 4;
			array = this.SerializeUnsignedInt(num4, stateObj);
			Buffer.BlockCopy(array, 0, bDecimalBytes, num, 4);
			num += 4;
			array = this.SerializeUnsignedInt(num5, stateObj);
			Buffer.BlockCopy(array, 0, bDecimalBytes, num, 4);
			return bDecimalBytes;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00057AA0 File Offset: 0x00055CA0
		internal void WriteSqlDecimal(SqlDecimal d, TdsParserStateObject stateObj)
		{
			if (d.IsPositive)
			{
				stateObj.WriteByte(1);
			}
			else
			{
				stateObj.WriteByte(0);
			}
			uint num;
			uint num2;
			uint num3;
			uint num4;
			SqlTypeWorkarounds.SqlDecimalExtractData(d, out num, out num2, out num3, out num4);
			this.WriteUnsignedInt(num, stateObj);
			this.WriteUnsignedInt(num2, stateObj);
			this.WriteUnsignedInt(num3, stateObj);
			this.WriteUnsignedInt(num4, stateObj);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00057AF4 File Offset: 0x00055CF4
		private byte[] SerializeDecimal(decimal value, TdsParserStateObject stateObj)
		{
			int[] bits = decimal.GetBits(value);
			if (stateObj._bDecimalBytes == null)
			{
				stateObj._bDecimalBytes = new byte[17];
			}
			byte[] bDecimalBytes = stateObj._bDecimalBytes;
			int num = 0;
			if ((ulong)(-2147483648) == (ulong)((long)bits[3] & (long)((ulong)(-2147483648))))
			{
				bDecimalBytes[num++] = 0;
			}
			else
			{
				bDecimalBytes[num++] = 1;
			}
			byte[] array = this.SerializeInt(bits[0], stateObj);
			Buffer.BlockCopy(array, 0, bDecimalBytes, num, 4);
			num += 4;
			array = this.SerializeInt(bits[1], stateObj);
			Buffer.BlockCopy(array, 0, bDecimalBytes, num, 4);
			num += 4;
			array = this.SerializeInt(bits[2], stateObj);
			Buffer.BlockCopy(array, 0, bDecimalBytes, num, 4);
			num += 4;
			array = this.SerializeInt(0, stateObj);
			Buffer.BlockCopy(array, 0, bDecimalBytes, num, 4);
			return bDecimalBytes;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00057BAC File Offset: 0x00055DAC
		private void WriteDecimal(decimal value, TdsParserStateObject stateObj)
		{
			stateObj._decimalBits = decimal.GetBits(value);
			if ((ulong)(-2147483648) == (ulong)((long)stateObj._decimalBits[3] & (long)((ulong)(-2147483648))))
			{
				stateObj.WriteByte(0);
			}
			else
			{
				stateObj.WriteByte(1);
			}
			this.WriteInt(stateObj._decimalBits[0], stateObj);
			this.WriteInt(stateObj._decimalBits[1], stateObj);
			this.WriteInt(stateObj._decimalBits[2], stateObj);
			this.WriteInt(0, stateObj);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00057C22 File Offset: 0x00055E22
		private void WriteIdentifier(string s, TdsParserStateObject stateObj)
		{
			if (s != null)
			{
				stateObj.WriteByte(checked((byte)s.Length));
				this.WriteString(s, stateObj, true);
				return;
			}
			stateObj.WriteByte(0);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00057C46 File Offset: 0x00055E46
		private void WriteIdentifierWithShortLength(string s, TdsParserStateObject stateObj)
		{
			if (s != null)
			{
				this.WriteShort((int)(checked((short)s.Length)), stateObj);
				this.WriteString(s, stateObj, true);
				return;
			}
			this.WriteShort(0, stateObj);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00057C6C File Offset: 0x00055E6C
		private Task WriteString(string s, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			return this.WriteString(s, s.Length, 0, stateObj, canAccumulate);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00057C80 File Offset: 0x00055E80
		internal byte[] SerializeCharArray(char[] carr, int length, int offset)
		{
			int num = 2 * length;
			byte[] array = new byte[num];
			TdsParser.CopyCharsToBytes(carr, offset, array, 0, length);
			return array;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00057CA4 File Offset: 0x00055EA4
		internal Task WriteCharArray(char[] carr, int length, int offset, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			int num = 2 * length;
			if (num < stateObj._outBuff.Length - stateObj._outBytesUsed)
			{
				TdsParser.CopyCharsToBytes(carr, offset, stateObj._outBuff, stateObj._outBytesUsed, length);
				stateObj._outBytesUsed += num;
				return null;
			}
			if (stateObj._bTmp == null || stateObj._bTmp.Length < num)
			{
				stateObj._bTmp = new byte[num];
			}
			TdsParser.CopyCharsToBytes(carr, offset, stateObj._bTmp, 0, length);
			return stateObj.WriteByteArray(stateObj._bTmp, num, 0, canAccumulate, null);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00057D38 File Offset: 0x00055F38
		internal byte[] SerializeString(string s, int length, int offset)
		{
			int num = 2 * length;
			byte[] array = new byte[num];
			TdsParser.CopyStringToBytes(s, offset, array, 0, length);
			return array;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00057D5C File Offset: 0x00055F5C
		internal Task WriteString(string s, int length, int offset, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			int num = 2 * length;
			if (num < stateObj._outBuff.Length - stateObj._outBytesUsed)
			{
				TdsParser.CopyStringToBytes(s, offset, stateObj._outBuff, stateObj._outBytesUsed, length);
				stateObj._outBytesUsed += num;
				return null;
			}
			if (stateObj._bTmp == null || stateObj._bTmp.Length < num)
			{
				stateObj._bTmp = new byte[num];
			}
			TdsParser.CopyStringToBytes(s, offset, stateObj._bTmp, 0, length);
			return stateObj.WriteByteArray(stateObj._bTmp, num, 0, canAccumulate, null);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00057DED File Offset: 0x00055FED
		private static void CopyCharsToBytes(char[] source, int sourceOffset, byte[] dest, int destOffset, int charLength)
		{
			Buffer.BlockCopy(source, sourceOffset, dest, destOffset, charLength * 2);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00057DFC File Offset: 0x00055FFC
		private static void CopyStringToBytes(string source, int sourceOffset, byte[] dest, int destOffset, int charLength)
		{
			Encoding.Unicode.GetBytes(source, sourceOffset, charLength, dest, destOffset);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00057E0F File Offset: 0x0005600F
		private Task WriteEncodingChar(string s, Encoding encoding, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			return this.WriteEncodingChar(s, s.Length, 0, encoding, stateObj, canAccumulate);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00057E24 File Offset: 0x00056024
		private byte[] SerializeEncodingChar(string s, int numChars, int offset, Encoding encoding)
		{
			if (encoding == null)
			{
				encoding = this._defaultEncoding;
			}
			char[] array = s.ToCharArray(offset, numChars);
			byte[] array2 = new byte[encoding.GetByteCount(array, 0, array.Length)];
			encoding.GetBytes(array, 0, array.Length, array2, 0);
			return array2;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00057E6C File Offset: 0x0005606C
		private Task WriteEncodingChar(string s, int numChars, int offset, Encoding encoding, TdsParserStateObject stateObj, bool canAccumulate = true)
		{
			if (encoding == null)
			{
				encoding = this._defaultEncoding;
			}
			int num = stateObj._outBuff.Length - stateObj._outBytesUsed;
			if (numChars <= num && encoding.GetMaxByteCount(numChars) <= num)
			{
				int bytes = encoding.GetBytes(s, offset, numChars, stateObj._outBuff, stateObj._outBytesUsed);
				stateObj._outBytesUsed += bytes;
				return null;
			}
			char[] array = s.ToCharArray(offset, numChars);
			byte[] bytes2 = encoding.GetBytes(array, 0, numChars);
			return stateObj.WriteByteArray(bytes2, bytes2.Length, 0, canAccumulate, null);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00057EF4 File Offset: 0x000560F4
		internal int GetEncodingCharLength(string value, int numChars, int charOffset, Encoding encoding)
		{
			if (string.IsNullOrEmpty(value))
			{
				return 0;
			}
			if (encoding == null)
			{
				if (this._defaultEncoding == null)
				{
					this.ThrowUnsupportedCollationEncountered(null);
				}
				encoding = this._defaultEncoding;
			}
			char[] array = value.ToCharArray(charOffset, numChars);
			return encoding.GetByteCount(array, 0, numChars);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00057F3C File Offset: 0x0005613C
		internal bool TryGetDataLength(SqlMetaDataPriv colmeta, TdsParserStateObject stateObj, out ulong length)
		{
			if (this._is2005 && colmeta.metaType.IsPlp)
			{
				return stateObj.TryReadPlpLength(true, out length);
			}
			int num;
			if (!this.TryGetTokenLength(colmeta.tdsType, stateObj, out num))
			{
				length = 0UL;
				return false;
			}
			length = (ulong)((long)num);
			return true;
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00057F84 File Offset: 0x00056184
		internal bool TryGetTokenLength(byte token, TdsParserStateObject stateObj, out int tokenLength)
		{
			if (token == 174)
			{
				tokenLength = -1;
				return true;
			}
			if (token != 228 && token != 238)
			{
				if (this._is2005)
				{
					if (token == 240)
					{
						tokenLength = -1;
						return true;
					}
					if (token == 172)
					{
						tokenLength = -1;
						return true;
					}
					if (token == 241)
					{
						ushort num;
						if (!stateObj.TryReadUInt16(out num))
						{
							tokenLength = 0;
							return false;
						}
						tokenLength = (int)num;
						return true;
					}
				}
				int num2 = (int)(token & 48);
				if (num2 <= 16)
				{
					if (num2 != 0)
					{
						if (num2 != 16)
						{
							goto IL_00E1;
						}
						tokenLength = 0;
						return true;
					}
				}
				else if (num2 != 32)
				{
					if (num2 == 48)
					{
						tokenLength = (1 << ((token & 12) >> 2)) & 255;
						return true;
					}
					goto IL_00E1;
				}
				if ((token & 128) != 0)
				{
					ushort num3;
					if (!stateObj.TryReadUInt16(out num3))
					{
						tokenLength = 0;
						return false;
					}
					tokenLength = (int)num3;
					return true;
				}
				else
				{
					if ((token & 12) == 0)
					{
						return stateObj.TryReadInt32(out tokenLength);
					}
					byte b;
					if (!stateObj.TryReadByte(out b))
					{
						tokenLength = 0;
						return false;
					}
					tokenLength = (int)b;
					return true;
				}
				IL_00E1:
				tokenLength = 0;
				return true;
			}
			return stateObj.TryReadInt32(out tokenLength);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00058078 File Offset: 0x00056278
		private void ProcessAttention(TdsParserStateObject stateObj)
		{
			if (this._state == TdsParserState.Closed || this._state == TdsParserState.Broken)
			{
				return;
			}
			stateObj.StoreErrorAndWarningForAttention();
			try
			{
				this.Run(RunBehavior.Attention, null, null, null, stateObj);
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(ex);
				this._state = TdsParserState.Broken;
				this._connHandler.BreakConnection();
				throw;
			}
			stateObj.RestoreErrorAndWarningAfterAttention();
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x000580E8 File Offset: 0x000562E8
		private static int StateValueLength(int dataLen)
		{
			if (dataLen >= 255)
			{
				return dataLen + 5;
			}
			return dataLen + 1;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x000580FC File Offset: 0x000562FC
		internal int WriteSessionRecoveryFeatureRequest(SessionData reconnectData, bool write)
		{
			int num = 1;
			if (write)
			{
				this._physicalStateObj.WriteByte(1);
			}
			if (reconnectData == null)
			{
				if (write)
				{
					this.WriteInt(0, this._physicalStateObj);
				}
				num += 4;
			}
			else
			{
				int num2 = 0;
				num2 += 1 + 2 * TdsParserStaticMethods.NullAwareStringLength(reconnectData._initialDatabase);
				num2 += 1 + 2 * TdsParserStaticMethods.NullAwareStringLength(reconnectData._initialLanguage);
				num2 += ((reconnectData._initialCollation == null) ? 1 : 6);
				for (int i = 0; i < 256; i++)
				{
					if (reconnectData._initialState[i] != null)
					{
						num2 += 1 + TdsParser.StateValueLength(reconnectData._initialState[i].Length);
					}
				}
				int num3 = 0;
				num3 += 1 + 2 * ((reconnectData._initialDatabase == reconnectData._database) ? 0 : TdsParserStaticMethods.NullAwareStringLength(reconnectData._database));
				num3 += 1 + 2 * ((reconnectData._initialLanguage == reconnectData._language) ? 0 : TdsParserStaticMethods.NullAwareStringLength(reconnectData._language));
				num3 += ((reconnectData._collation != null && !SqlCollation.Equals(reconnectData._collation, reconnectData._initialCollation)) ? 6 : 1);
				bool[] array = new bool[256];
				for (int j = 0; j < 256; j++)
				{
					if (reconnectData._delta[j] != null)
					{
						array[j] = true;
						if (reconnectData._initialState[j] != null && reconnectData._initialState[j].Length == reconnectData._delta[j]._dataLength)
						{
							array[j] = false;
							for (int k = 0; k < reconnectData._delta[j]._dataLength; k++)
							{
								if (reconnectData._initialState[j][k] != reconnectData._delta[j]._data[k])
								{
									array[j] = true;
									break;
								}
							}
						}
						if (array[j])
						{
							num3 += 1 + TdsParser.StateValueLength(reconnectData._delta[j]._dataLength);
						}
					}
				}
				if (write)
				{
					this.WriteInt(8 + num2 + num3, this._physicalStateObj);
					this.WriteInt(num2, this._physicalStateObj);
					this.WriteIdentifier(reconnectData._initialDatabase, this._physicalStateObj);
					this.WriteCollation(reconnectData._initialCollation, this._physicalStateObj);
					this.WriteIdentifier(reconnectData._initialLanguage, this._physicalStateObj);
					for (int l = 0; l < 256; l++)
					{
						if (reconnectData._initialState[l] != null)
						{
							this._physicalStateObj.WriteByte((byte)l);
							if (reconnectData._initialState[l].Length < 255)
							{
								this._physicalStateObj.WriteByte((byte)reconnectData._initialState[l].Length);
							}
							else
							{
								this._physicalStateObj.WriteByte(byte.MaxValue);
								this.WriteInt(reconnectData._initialState[l].Length, this._physicalStateObj);
							}
							this._physicalStateObj.WriteByteArray(reconnectData._initialState[l], reconnectData._initialState[l].Length, 0, true, null);
						}
					}
					this.WriteInt(num3, this._physicalStateObj);
					this.WriteIdentifier((reconnectData._database != reconnectData._initialDatabase) ? reconnectData._database : null, this._physicalStateObj);
					this.WriteCollation(SqlCollation.Equals(reconnectData._initialCollation, reconnectData._collation) ? null : reconnectData._collation, this._physicalStateObj);
					this.WriteIdentifier((reconnectData._language != reconnectData._initialLanguage) ? reconnectData._language : null, this._physicalStateObj);
					for (int m = 0; m < 256; m++)
					{
						if (array[m])
						{
							this._physicalStateObj.WriteByte((byte)m);
							if (reconnectData._delta[m]._dataLength < 255)
							{
								this._physicalStateObj.WriteByte((byte)reconnectData._delta[m]._dataLength);
							}
							else
							{
								this._physicalStateObj.WriteByte(byte.MaxValue);
								this.WriteInt(reconnectData._delta[m]._dataLength, this._physicalStateObj);
							}
							this._physicalStateObj.WriteByteArray(reconnectData._delta[m]._data, reconnectData._delta[m]._dataLength, 0, true, null);
						}
					}
				}
				num += num2 + num3 + 12;
			}
			return num;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00058524 File Offset: 0x00056724
		internal int WriteFedAuthFeatureRequest(FederatedAuthenticationFeatureExtensionData fedAuthFeatureData, bool write)
		{
			int num = 0;
			TdsEnums.FedAuthLibrary libraryType = fedAuthFeatureData.libraryType;
			if (libraryType != TdsEnums.FedAuthLibrary.SecurityToken)
			{
				if (libraryType == TdsEnums.FedAuthLibrary.MSAL)
				{
					num = 2;
				}
			}
			else
			{
				num = 5 + fedAuthFeatureData.accessToken.Length;
			}
			int num2 = num + 5;
			if (write)
			{
				this._physicalStateObj.WriteByte(2);
				byte b = 0;
				TdsEnums.FedAuthLibrary libraryType2 = fedAuthFeatureData.libraryType;
				if (libraryType2 != TdsEnums.FedAuthLibrary.SecurityToken)
				{
					if (libraryType2 == TdsEnums.FedAuthLibrary.MSAL)
					{
						b |= 4;
					}
				}
				else
				{
					b |= 2;
				}
				b |= ((fedAuthFeatureData.fedAuthRequiredPreLoginResponse > false) ? 1 : 0);
				this.WriteInt(num, this._physicalStateObj);
				this._physicalStateObj.WriteByte(b);
				TdsEnums.FedAuthLibrary libraryType3 = fedAuthFeatureData.libraryType;
				if (libraryType3 != TdsEnums.FedAuthLibrary.SecurityToken)
				{
					if (libraryType3 == TdsEnums.FedAuthLibrary.MSAL)
					{
						byte b2 = 0;
						switch (fedAuthFeatureData.authentication)
						{
						case SqlAuthenticationMethod.ActiveDirectoryPassword:
							b2 = 1;
							break;
						case SqlAuthenticationMethod.ActiveDirectoryIntegrated:
							b2 = 2;
							break;
						case SqlAuthenticationMethod.ActiveDirectoryInteractive:
							b2 = 3;
							break;
						case SqlAuthenticationMethod.ActiveDirectoryServicePrincipal:
							b2 = 1;
							break;
						case SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow:
							b2 = 3;
							break;
						case SqlAuthenticationMethod.ActiveDirectoryManagedIdentity:
						case SqlAuthenticationMethod.ActiveDirectoryMSI:
							b2 = 3;
							break;
						case SqlAuthenticationMethod.ActiveDirectoryDefault:
							b2 = 3;
							break;
						}
						this._physicalStateObj.WriteByte(b2);
					}
				}
				else
				{
					this.WriteInt(fedAuthFeatureData.accessToken.Length, this._physicalStateObj);
					this._physicalStateObj.WriteByteArray(fedAuthFeatureData.accessToken, fedAuthFeatureData.accessToken.Length, 0, true, null);
				}
			}
			return num2;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00058660 File Offset: 0x00056860
		internal int WriteDataClassificationFeatureRequest(bool write)
		{
			int num = 6;
			if (write)
			{
				this._physicalStateObj.WriteByte(9);
				this.WriteInt(1, this._physicalStateObj);
				this._physicalStateObj.WriteByte(2);
			}
			return num;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0005869C File Offset: 0x0005689C
		internal int WriteTceFeatureRequest(bool write)
		{
			int num = 6;
			if (write)
			{
				this._physicalStateObj.WriteByte(4);
				this.WriteInt(1, this._physicalStateObj);
				this._physicalStateObj.WriteByte(3);
			}
			return num;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x000586D4 File Offset: 0x000568D4
		internal int WriteGlobalTransactionsFeatureRequest(bool write)
		{
			int num = 5;
			if (write)
			{
				this._physicalStateObj.WriteByte(5);
				this.WriteInt(0, this._physicalStateObj);
			}
			return num;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00058700 File Offset: 0x00056900
		internal int WriteAzureSQLSupportFeatureRequest(bool write)
		{
			int num = 6;
			if (write)
			{
				this._physicalStateObj.WriteByte(8);
				this.WriteInt(TdsParser.s_FeatureExtDataAzureSQLSupportFeatureRequest.Length, this._physicalStateObj);
				this._physicalStateObj.WriteByteArray(TdsParser.s_FeatureExtDataAzureSQLSupportFeatureRequest, TdsParser.s_FeatureExtDataAzureSQLSupportFeatureRequest.Length, 0, true, null);
			}
			return num;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00058750 File Offset: 0x00056950
		internal int WriteUTF8SupportFeatureRequest(bool write)
		{
			int num = 5;
			if (write)
			{
				this._physicalStateObj.WriteByte(10);
				this.WriteInt(0, this._physicalStateObj);
			}
			return num;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00058780 File Offset: 0x00056980
		internal int WriteSQLDNSCachingFeatureRequest(bool write)
		{
			int num = 5;
			if (write)
			{
				this._physicalStateObj.WriteByte(11);
				this.WriteInt(0, this._physicalStateObj);
			}
			return num;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x000587B0 File Offset: 0x000569B0
		internal void TdsLogin(SqlLogin rec, TdsEnums.FeatureExtension requestedFeatures, SessionData recoverySessionData, FederatedAuthenticationFeatureExtensionData fedAuthFeatureExtensionData, SqlClientOriginalNetworkAddressInfo originalNetworkAddressInfo, SqlConnectionEncryptOption encrypt)
		{
			this._physicalStateObj.SetTimeoutSeconds(rec.timeout);
			this._connHandler.TimeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.LoginBegin);
			this._connHandler.TimeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth);
			if (originalNetworkAddressInfo != null)
			{
				SNINativeMethodWrapper.CTAIPProviderInfo ctaipproviderInfo = default(SNINativeMethodWrapper.CTAIPProviderInfo);
				ctaipproviderInfo.originalNetworkAddress = originalNetworkAddressInfo.Address.GetAddressBytes();
				ctaipproviderInfo.fromDataSecurityProxy = originalNetworkAddressInfo.IsFromDataSecurityProxy;
				uint num = SNINativeMethodWrapper.SNIAddProvider(this._physicalStateObj.Handle, SNINativeMethodWrapper.ProviderEnum.CTAIP_PROV, ctaipproviderInfo);
				if (num != 0U)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				try
				{
				}
				finally
				{
					this._physicalStateObj.ClearAllWritePackets();
				}
			}
			byte[] array = null;
			byte[] array2 = null;
			bool flag = requestedFeatures > TdsEnums.FeatureExtension.None;
			string text;
			int num2;
			if (rec.credential != null)
			{
				text = rec.credential.UserId;
				num2 = rec.credential.Password.Length * 2;
			}
			else
			{
				text = rec.userName;
				array = TdsParserStaticMethods.ObfuscatePassword(rec.password);
				num2 = array.Length;
			}
			int num3;
			if (rec.newSecurePassword != null)
			{
				num3 = rec.newSecurePassword.Length * 2;
			}
			else
			{
				array2 = TdsParserStaticMethods.ObfuscatePassword(rec.newPassword);
				num3 = array2.Length;
			}
			this._physicalStateObj._outputMessageType = 16;
			int num4 = 94;
			string text2 = "Framework Microsoft SqlClient Data Provider";
			byte[] array3;
			uint num5;
			int num6;
			checked
			{
				num4 += (rec.hostName.Length + rec.applicationName.Length + rec.serverName.Length + text2.Length + rec.language.Length + rec.database.Length + rec.attachDBFilename.Length) * 2;
				if (flag)
				{
					num4 += 4;
				}
				array3 = null;
				num5 = 0U;
				if (!rec.useSSPI && !this._connHandler._federatedAuthenticationInfoRequested && !this._connHandler._federatedAuthenticationRequested)
				{
					num4 += text.Length * 2 + num2 + num3;
				}
				else if (rec.useSSPI)
				{
					array3 = new byte[TdsParser.s_maxSSPILength];
					num5 = TdsParser.s_maxSSPILength;
					this._physicalStateObj.SniContext = SniContext.Snix_LoginSspi;
					this.SSPIData(null, 0U, array3, ref num5);
					if (num5 > 2147483647U)
					{
						throw SQL.InvalidSSPIPacketSize();
					}
					this._physicalStateObj.SniContext = SniContext.Snix_Login;
					num4 += (int)num5;
				}
				num6 = num4;
				if (flag)
				{
					if ((requestedFeatures & TdsEnums.FeatureExtension.SessionRecovery) != TdsEnums.FeatureExtension.None)
					{
						num4 += this.WriteSessionRecoveryFeatureRequest(recoverySessionData, false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.FedAuth) != TdsEnums.FeatureExtension.None)
					{
						num4 += this.WriteFedAuthFeatureRequest(fedAuthFeatureExtensionData, false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.Tce) != TdsEnums.FeatureExtension.None)
					{
						num4 += this.WriteTceFeatureRequest(false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.GlobalTransactions) != TdsEnums.FeatureExtension.None)
					{
						num4 += this.WriteGlobalTransactionsFeatureRequest(false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.AzureSQLSupport) != TdsEnums.FeatureExtension.None)
					{
						num4 += this.WriteAzureSQLSupportFeatureRequest(false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.DataClassification) != TdsEnums.FeatureExtension.None)
					{
						num4 += this.WriteDataClassificationFeatureRequest(false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.UTF8Support) != TdsEnums.FeatureExtension.None)
					{
						num4 += this.WriteUTF8SupportFeatureRequest(false);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.SQLDNSCaching) != TdsEnums.FeatureExtension.None)
					{
						num4 += this.WriteSQLDNSCachingFeatureRequest(false);
					}
					num4++;
				}
			}
			try
			{
				this.WriteInt(num4, this._physicalStateObj);
				if (recoverySessionData == null)
				{
					if (encrypt == SqlConnectionEncryptOption.Strict)
					{
						this.WriteInt(134217728, this._physicalStateObj);
					}
					else
					{
						this.WriteInt(1946157060, this._physicalStateObj);
					}
				}
				else
				{
					this.WriteUnsignedInt(recoverySessionData._tdsVersion, this._physicalStateObj);
				}
				this.WriteInt(rec.packetSize, this._physicalStateObj);
				this.WriteInt(100663296, this._physicalStateObj);
				this.WriteInt(TdsParserStaticMethods.GetCurrentProcessIdForTdsLoginOnly(), this._physicalStateObj);
				this.WriteInt(0, this._physicalStateObj);
				int num7 = 0;
				num7 |= 32;
				num7 |= 64;
				num7 |= 128;
				num7 |= 256;
				num7 |= 512;
				if (rec.useReplication)
				{
					num7 |= 12288;
				}
				if (rec.useSSPI)
				{
					num7 |= 32768;
				}
				if (rec.readOnlyIntent)
				{
					num7 |= 2097152;
				}
				if (!ADP.IsEmpty(rec.newPassword) || (rec.newSecurePassword != null && rec.newSecurePassword.Length != 0))
				{
					num7 |= 16777216;
				}
				if (rec.userInstance)
				{
					num7 |= 67108864;
				}
				if (flag)
				{
					num7 |= 268435456;
				}
				this.WriteInt(num7, this._physicalStateObj);
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParser.TdsLogin|ADV> {0}, TDS Login7 flags = {1}:", this.ObjectID, num7);
				this.WriteInt(0, this._physicalStateObj);
				this.WriteInt(0, this._physicalStateObj);
				int num8 = 94;
				this.WriteShort(num8, this._physicalStateObj);
				this.WriteShort(rec.hostName.Length, this._physicalStateObj);
				num8 += rec.hostName.Length * 2;
				if (!rec.useSSPI && !this._connHandler._federatedAuthenticationInfoRequested && !this._connHandler._federatedAuthenticationRequested)
				{
					this.WriteShort(num8, this._physicalStateObj);
					this.WriteShort(text.Length, this._physicalStateObj);
					num8 += text.Length * 2;
					this.WriteShort(num8, this._physicalStateObj);
					this.WriteShort(num2 / 2, this._physicalStateObj);
					num8 += num2;
				}
				else
				{
					this.WriteShort(0, this._physicalStateObj);
					this.WriteShort(0, this._physicalStateObj);
					this.WriteShort(0, this._physicalStateObj);
					this.WriteShort(0, this._physicalStateObj);
				}
				this.WriteShort(num8, this._physicalStateObj);
				this.WriteShort(rec.applicationName.Length, this._physicalStateObj);
				num8 += rec.applicationName.Length * 2;
				this.WriteShort(num8, this._physicalStateObj);
				this.WriteShort(rec.serverName.Length, this._physicalStateObj);
				num8 += rec.serverName.Length * 2;
				this.WriteShort(num8, this._physicalStateObj);
				if (flag)
				{
					this.WriteShort(4, this._physicalStateObj);
					num8 += 4;
				}
				else
				{
					this.WriteShort(0, this._physicalStateObj);
				}
				this.WriteShort(num8, this._physicalStateObj);
				this.WriteShort(text2.Length, this._physicalStateObj);
				num8 += text2.Length * 2;
				this.WriteShort(num8, this._physicalStateObj);
				this.WriteShort(rec.language.Length, this._physicalStateObj);
				num8 += rec.language.Length * 2;
				this.WriteShort(num8, this._physicalStateObj);
				this.WriteShort(rec.database.Length, this._physicalStateObj);
				num8 += rec.database.Length * 2;
				if (TdsParser.s_nicAddress == null)
				{
					TdsParser.s_nicAddress = TdsParserStaticMethods.GetNetworkPhysicalAddressForTdsLoginOnly();
				}
				this._physicalStateObj.WriteByteArray(TdsParser.s_nicAddress, TdsParser.s_nicAddress.Length, 0, true, null);
				this.WriteShort(num8, this._physicalStateObj);
				if (rec.useSSPI)
				{
					this.WriteShort((int)num5, this._physicalStateObj);
					num8 += (int)num5;
				}
				else
				{
					this.WriteShort(0, this._physicalStateObj);
				}
				this.WriteShort(num8, this._physicalStateObj);
				this.WriteShort(rec.attachDBFilename.Length, this._physicalStateObj);
				num8 += rec.attachDBFilename.Length * 2;
				this.WriteShort(num8, this._physicalStateObj);
				this.WriteShort(num3 / 2, this._physicalStateObj);
				this.WriteInt(0, this._physicalStateObj);
				this.WriteString(rec.hostName, this._physicalStateObj, true);
				if (!rec.useSSPI && !this._connHandler._federatedAuthenticationInfoRequested && !this._connHandler._federatedAuthenticationRequested)
				{
					this.WriteString(text, this._physicalStateObj, true);
					this._physicalStateObj._tracePasswordOffset = this._physicalStateObj._outBytesUsed;
					this._physicalStateObj._tracePasswordLength = num2;
					if (rec.credential != null)
					{
						this._physicalStateObj.WriteSecureString(rec.credential.Password);
					}
					else
					{
						this._physicalStateObj.WriteByteArray(array, num2, 0, true, null);
					}
				}
				this.WriteString(rec.applicationName, this._physicalStateObj, true);
				this.WriteString(rec.serverName, this._physicalStateObj, true);
				if (flag)
				{
					this.WriteInt(num6, this._physicalStateObj);
				}
				this.WriteString(text2, this._physicalStateObj, true);
				this.WriteString(rec.language, this._physicalStateObj, true);
				this.WriteString(rec.database, this._physicalStateObj, true);
				if (rec.useSSPI)
				{
					this._physicalStateObj.WriteByteArray(array3, (int)num5, 0, true, null);
				}
				this.WriteString(rec.attachDBFilename, this._physicalStateObj, true);
				if (!rec.useSSPI && !this._connHandler._federatedAuthenticationInfoRequested && !this._connHandler._federatedAuthenticationRequested)
				{
					this._physicalStateObj._traceChangePasswordOffset = this._physicalStateObj._outBytesUsed;
					this._physicalStateObj._traceChangePasswordLength = num3;
					if (rec.newSecurePassword != null)
					{
						this._physicalStateObj.WriteSecureString(rec.newSecurePassword);
					}
					else
					{
						this._physicalStateObj.WriteByteArray(array2, num3, 0, true, null);
					}
				}
				if (flag)
				{
					if ((requestedFeatures & TdsEnums.FeatureExtension.SessionRecovery) != TdsEnums.FeatureExtension.None)
					{
						this.WriteSessionRecoveryFeatureRequest(recoverySessionData, true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.FedAuth) != TdsEnums.FeatureExtension.None)
					{
						SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.TdsLogin|SEC> Sending federated authentication feature request");
						this.WriteFedAuthFeatureRequest(fedAuthFeatureExtensionData, true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.Tce) != TdsEnums.FeatureExtension.None)
					{
						this.WriteTceFeatureRequest(true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.GlobalTransactions) != TdsEnums.FeatureExtension.None)
					{
						this.WriteGlobalTransactionsFeatureRequest(true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.AzureSQLSupport) != TdsEnums.FeatureExtension.None)
					{
						this.WriteAzureSQLSupportFeatureRequest(true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.DataClassification) != TdsEnums.FeatureExtension.None)
					{
						this.WriteDataClassificationFeatureRequest(true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.UTF8Support) != TdsEnums.FeatureExtension.None)
					{
						this.WriteUTF8SupportFeatureRequest(true);
					}
					if ((requestedFeatures & TdsEnums.FeatureExtension.SQLDNSCaching) != TdsEnums.FeatureExtension.None)
					{
						this.WriteSQLDNSCachingFeatureRequest(true);
					}
					this._physicalStateObj.WriteByte(byte.MaxValue);
				}
			}
			catch (Exception ex)
			{
				if (ADP.IsCatchableExceptionType(ex))
				{
					this._physicalStateObj.ResetPacketCounters();
					this._physicalStateObj.ResetBuffer();
				}
				throw;
			}
			this._physicalStateObj.WritePacket(1, false);
			this._physicalStateObj.ResetSecurePasswordsInfomation();
			this._physicalStateObj._pendingData = true;
			this._physicalStateObj._messageStatus = 0;
			if (originalNetworkAddressInfo != null)
			{
				uint num9 = SNINativeMethodWrapper.SNIRemoveProvider(this._physicalStateObj.Handle, SNINativeMethodWrapper.ProviderEnum.CTAIP_PROV);
				if (num9 != 0U)
				{
					this._physicalStateObj.AddError(this.ProcessSNIError(this._physicalStateObj));
					this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
				}
				try
				{
				}
				finally
				{
					this._physicalStateObj.ClearAllWritePackets();
				}
			}
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00059248 File Offset: 0x00057448
		internal void SendFedAuthToken(SqlFedAuthToken fedAuthToken)
		{
			SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.SendFedAuthToken|SEC> Sending federated authentication token");
			this._physicalStateObj._outputMessageType = 8;
			byte[] accessToken = fedAuthToken.accessToken;
			this.WriteUnsignedInt((uint)(accessToken.Length + 4), this._physicalStateObj);
			this.WriteUnsignedInt((uint)accessToken.Length, this._physicalStateObj);
			this._physicalStateObj.WriteByteArray(accessToken, accessToken.Length, 0, true, null);
			this._physicalStateObj.WritePacket(1, false);
			this._physicalStateObj._pendingData = true;
			this._physicalStateObj._messageStatus = 0;
			this._connHandler._federatedAuthenticationRequested = true;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x000592DC File Offset: 0x000574DC
		private void SSPIData(byte[] receivedBuff, uint receivedLength, byte[] sendBuff, ref uint sendLength)
		{
			this.SNISSPIData(receivedBuff, receivedLength, sendBuff, ref sendLength);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x000592E9 File Offset: 0x000574E9
		private void SNISSPIData(byte[] receivedBuff, uint receivedLength, byte[] sendBuff, ref uint sendLength)
		{
			if (receivedBuff == null)
			{
				receivedLength = 0U;
			}
			if (SNINativeMethodWrapper.SNISecGenClientContext(this._physicalStateObj.Handle, receivedBuff, receivedLength, sendBuff, ref sendLength, this._sniSpnBuffer) != 0U)
			{
				this.SSPIError(SQLMessage.SSPIGenerateError(), "GenClientContext");
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00059320 File Offset: 0x00057520
		private void ProcessSSPI(int receivedLength)
		{
			SniContext sniContext = this._physicalStateObj.SniContext;
			this._physicalStateObj.SniContext = SniContext.Snix_ProcessSspi;
			byte[] array = new byte[receivedLength];
			if (!this._physicalStateObj.TryReadByteArray(array, receivedLength))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			byte[] array2 = new byte[TdsParser.s_maxSSPILength];
			uint num = TdsParser.s_maxSSPILength;
			this.SSPIData(array, (uint)receivedLength, array2, ref num);
			this._physicalStateObj.WriteByteArray(array2, (int)num, 0, true, null);
			this._physicalStateObj._outputMessageType = 17;
			this._physicalStateObj.WritePacket(1, false);
			this._physicalStateObj.SniContext = sniContext;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x000593C4 File Offset: 0x000575C4
		private void SSPIError(string error, string procedure)
		{
			this._physicalStateObj.AddError(new SqlError(0, 0, 11, this._server, error, procedure, 0, null));
			this.ThrowExceptionAndWarning(this._physicalStateObj, false, false);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00059400 File Offset: 0x00057600
		private void LoadSSPILibrary()
		{
			if (!TdsParser.s_fSSPILoaded)
			{
				object obj = TdsParser.s_tdsParserLock;
				lock (obj)
				{
					if (!TdsParser.s_fSSPILoaded)
					{
						uint num = 0U;
						if (SNINativeMethodWrapper.SNISecInitPackage(ref num) != 0U)
						{
							this.SSPIError(SQLMessage.SSPIInitializeError(), "InitSSPIPackage");
						}
						TdsParser.s_maxSSPILength = num;
						TdsParser.s_fSSPILoaded = true;
					}
				}
			}
			if (TdsParser.s_maxSSPILength > 2147483647U)
			{
				throw SQL.InvalidSSPIPacketSize();
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00059484 File Offset: 0x00057684
		internal byte[] GetDTCAddress(int timeout, TdsParserStateObject stateObj)
		{
			byte[] array = null;
			using (SqlDataReader sqlDataReader = this.TdsExecuteTransactionManagerRequest(null, TdsEnums.TransactionManagerRequestType.GetDTCAddress, null, TdsEnums.TransactionManagerIsolationLevel.Unspecified, timeout, null, stateObj, true))
			{
				if (sqlDataReader != null && sqlDataReader.Read())
				{
					long bytes = sqlDataReader.GetBytes(0, 0L, null, 0, 0);
					if (bytes <= 2147483647L)
					{
						int num = (int)bytes;
						array = new byte[num];
						sqlDataReader.GetBytes(0, 0L, array, 0, num);
					}
				}
			}
			return array;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x000594F8 File Offset: 0x000576F8
		internal void PropagateDistributedTransaction(byte[] buffer, int timeout, TdsParserStateObject stateObj)
		{
			this.TdsExecuteTransactionManagerRequest(buffer, TdsEnums.TransactionManagerRequestType.Propagate, null, TdsEnums.TransactionManagerIsolationLevel.Unspecified, timeout, null, stateObj, true);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00059514 File Offset: 0x00057714
		internal SqlDataReader TdsExecuteTransactionManagerRequest(byte[] buffer, TdsEnums.TransactionManagerRequestType request, string transactionName, TdsEnums.TransactionManagerIsolationLevel isoLevel, int timeout, SqlInternalTransaction transaction, TdsParserStateObject stateObj, bool isDelegateControlRequest)
		{
			if (TdsParserState.Broken == this.State || this.State == TdsParserState.Closed)
			{
				return null;
			}
			bool threadHasParserLockForClose = this._connHandler.ThreadHasParserLockForClose;
			if (!threadHasParserLockForClose)
			{
				this._connHandler._parserLock.Wait(false);
				this._connHandler.ThreadHasParserLockForClose = true;
			}
			bool asyncWrite = this._asyncWrite;
			SqlDataReader sqlDataReader2;
			try
			{
				this._asyncWrite = false;
				if (!isDelegateControlRequest)
				{
					this._connHandler.CheckEnlistedTransactionBinding();
				}
				stateObj._outputMessageType = 14;
				stateObj.SetTimeoutSeconds(timeout);
				stateObj.SniContext = SniContext.Snix_Execute;
				if (this._is2005)
				{
					this.WriteInt(22, stateObj);
					this.WriteInt(18, stateObj);
					this.WriteMarsHeaderData(stateObj, this._currentTransaction);
				}
				this.WriteShort((int)((short)request), stateObj);
				bool flag = false;
				switch (request)
				{
				case TdsEnums.TransactionManagerRequestType.GetDTCAddress:
					this.WriteShort(0, stateObj);
					flag = true;
					break;
				case TdsEnums.TransactionManagerRequestType.Propagate:
					if (buffer != null)
					{
						this.WriteShort(buffer.Length, stateObj);
						stateObj.WriteByteArray(buffer, buffer.Length, 0, true, null);
					}
					else
					{
						this.WriteShort(0, stateObj);
					}
					break;
				case TdsEnums.TransactionManagerRequestType.Begin:
					if (this._currentTransaction != transaction)
					{
						this.PendingTransaction = transaction;
					}
					stateObj.WriteByte((byte)isoLevel);
					stateObj.WriteByte((byte)(transactionName.Length * 2));
					this.WriteString(transactionName, stateObj, true);
					break;
				case TdsEnums.TransactionManagerRequestType.Commit:
					stateObj.WriteByte(0);
					stateObj.WriteByte(0);
					break;
				case TdsEnums.TransactionManagerRequestType.Rollback:
					stateObj.WriteByte((byte)(transactionName.Length * 2));
					this.WriteString(transactionName, stateObj, true);
					stateObj.WriteByte(0);
					break;
				case TdsEnums.TransactionManagerRequestType.Save:
					stateObj.WriteByte((byte)(transactionName.Length * 2));
					this.WriteString(transactionName, stateObj, true);
					break;
				}
				Task task = stateObj.WritePacket(1, false);
				stateObj._pendingData = true;
				stateObj._messageStatus = 0;
				SqlDataReader sqlDataReader = null;
				stateObj.SniContext = SniContext.Snix_Read;
				if (flag)
				{
					sqlDataReader = new SqlDataReader(null, CommandBehavior.Default);
					sqlDataReader.Bind(stateObj);
					_SqlMetaDataSet metaData = sqlDataReader.MetaData;
				}
				else
				{
					this.Run(RunBehavior.UntilDone, null, null, null, stateObj);
				}
				if ((request == TdsEnums.TransactionManagerRequestType.Begin || request == TdsEnums.TransactionManagerRequestType.Propagate) && (transaction == null || transaction.TransactionId != this._retainedTransactionId))
				{
					this._retainedTransactionId = 0L;
				}
				sqlDataReader2 = sqlDataReader;
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				this.FailureCleanup(stateObj, ex);
				throw;
			}
			finally
			{
				this._pendingTransaction = null;
				this._asyncWrite = asyncWrite;
				if (!threadHasParserLockForClose)
				{
					this._connHandler.ThreadHasParserLockForClose = false;
					this._connHandler._parserLock.Release();
				}
			}
			return sqlDataReader2;
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x000597C8 File Offset: 0x000579C8
		internal void FailureCleanup(TdsParserStateObject stateObj, Exception e)
		{
			int outputPacketNumber = (int)stateObj._outputPacketNumber;
			SqlClientEventSource.Log.TryTraceEvent<Exception>("<sc.TdsParser.FailureCleanup|ERR> Exception caught on ExecuteXXX: '{0}'", e);
			if (stateObj.HasOpenResult)
			{
				stateObj.DecrementOpenResultCount();
			}
			stateObj.ResetBuffer();
			stateObj.ResetPacketCounters();
			if (outputPacketNumber != 1 && this._state == TdsParserState.OpenLoggedIn)
			{
				bool threadHasParserLockForClose = this._connHandler.ThreadHasParserLockForClose;
				try
				{
					this._connHandler.ThreadHasParserLockForClose = true;
					stateObj.SendAttention(false, false);
					this.ProcessAttention(stateObj);
				}
				finally
				{
					this._connHandler.ThreadHasParserLockForClose = threadHasParserLockForClose;
				}
			}
			SqlClientEventSource.Log.TryTraceEvent("<sc.TdsParser.FailureCleanup|ERR> Exception rethrown.");
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00059868 File Offset: 0x00057A68
		internal Task TdsExecuteSQLBatch(string text, int timeout, SqlNotificationRequest notificationRequest, TdsParserStateObject stateObj, bool sync, bool callerHasConnectionLock = false, byte[] enclavePackage = null)
		{
			if (TdsParserState.Broken == this.State || this.State == TdsParserState.Closed)
			{
				return null;
			}
			if (stateObj.BcpLock)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			bool flag = !callerHasConnectionLock && !this._connHandler.ThreadHasParserLockForClose;
			bool flag2 = false;
			if (flag)
			{
				this._connHandler._parserLock.Wait(!sync);
				flag2 = true;
			}
			this._asyncWrite = !sync;
			Task task2;
			try
			{
				if (this._state == TdsParserState.Closed || this._state == TdsParserState.Broken)
				{
					throw ADP.ClosedConnectionError();
				}
				this._connHandler.CheckEnlistedTransactionBinding();
				stateObj.SetTimeoutSeconds(timeout);
				if (!this._fMARS && this._physicalStateObj.HasOpenResult)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.TdsParser.TdsExecuteSQLBatch|ERR> Potential multi-threaded misuse of connection, non-MARs connection with an open result {0}", this.ObjectID);
				}
				stateObj.SniContext = SniContext.Snix_Execute;
				if (this._is2005)
				{
					this.WriteRPCBatchHeaders(stateObj, notificationRequest);
				}
				stateObj._outputMessageType = 1;
				this.WriteEnclaveInfo(stateObj, enclavePackage);
				this.WriteString(text, text.Length, 0, stateObj, true);
				Task task = stateObj.ExecuteFlush();
				if (task == null)
				{
					stateObj.SniContext = SniContext.Snix_Read;
					task2 = null;
				}
				else
				{
					bool taskReleaseConnectionLock = flag2;
					flag2 = false;
					task2 = task.ContinueWith(delegate(Task t)
					{
						try
						{
							if (t.IsFaulted)
							{
								this.FailureCleanup(stateObj, t.Exception.InnerException);
								throw t.Exception.InnerException;
							}
							stateObj.SniContext = SniContext.Snix_Read;
						}
						finally
						{
							if (taskReleaseConnectionLock)
							{
								this._connHandler._parserLock.Release();
							}
						}
					}, TaskScheduler.Default);
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				this.FailureCleanup(stateObj, ex);
				throw;
			}
			finally
			{
				if (flag2)
				{
					this._connHandler._parserLock.Release();
				}
			}
			return task2;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x00059A48 File Offset: 0x00057C48
		internal Task TdsExecuteRPC(SqlCommand cmd, _SqlRPC[] rpcArray, int timeout, bool inSchema, SqlNotificationRequest notificationRequest, TdsParserStateObject stateObj, bool isCommandProc, bool sync = true, TaskCompletionSource<object> completion = null, int startRpc = 0, int startParam = 0)
		{
			bool flag = completion == null;
			bool flag2 = false;
			Task task6;
			try
			{
				_SqlRPC sqlRPC = null;
				if (flag)
				{
					this._connHandler._parserLock.Wait(!sync);
					flag2 = true;
				}
				try
				{
					if (TdsParserState.Broken == this.State || this.State == TdsParserState.Closed)
					{
						throw ADP.ClosedConnectionError();
					}
					if (flag)
					{
						this._asyncWrite = !sync;
						this._connHandler.CheckEnlistedTransactionBinding();
						stateObj.SetTimeoutSeconds(timeout);
						if (!this._fMARS && this._physicalStateObj.HasOpenResult)
						{
							SqlClientEventSource.Log.TryTraceEvent<int>("<sc.TdsParser.TdsExecuteRPC|ERR> Potential multi-threaded misuse of connection, non-MARs connection with an open result {0}", this.ObjectID);
						}
						stateObj.SniContext = SniContext.Snix_Execute;
						if (this._is2005)
						{
							this.WriteRPCBatchHeaders(stateObj, notificationRequest);
						}
						stateObj._outputMessageType = 3;
					}
					Action<Exception, object> <>9__1;
					int num8;
					int ii;
					for (ii = startRpc; ii < rpcArray.Length; ii = num8 + 1)
					{
						sqlRPC = rpcArray[ii];
						if (startParam == 0 || ii > startRpc)
						{
							if (sqlRPC.ProcID != 0 && this._is2000)
							{
								this.WriteShort(65535, stateObj);
								this.WriteShort((int)((short)sqlRPC.ProcID), stateObj);
							}
							else
							{
								int num = sqlRPC.rpcName.Length;
								this.WriteShort(num, stateObj);
								this.WriteString(sqlRPC.rpcName, num, 0, stateObj, true);
							}
							this.WriteShort((int)((short)sqlRPC.options), stateObj);
							byte[] array = ((cmd.enclavePackage != null) ? cmd.enclavePackage.EnclavePackageBytes : null);
							this.WriteEnclaveInfo(stateObj, array);
						}
						int num2 = sqlRPC.userParamCount + sqlRPC.systemParamCount;
						bool flag3 = SqlClientEventSource.Log.IsAdvancedTraceOn();
						bool enableOptimizedParameterBinding = cmd.EnableOptimizedParameterBinding;
						int i;
						for (i = ((ii == startRpc) ? startParam : 0); i < num2; i = num8 + 1)
						{
							byte b = 0;
							SqlParameter parameterByIndex = sqlRPC.GetParameterByIndex(i, out b);
							if (parameterByIndex == null)
							{
								break;
							}
							ParameterDirection direction = parameterByIndex.Direction;
							if (parameterByIndex.ForceColumnEncryption && cmd.ColumnEncryptionSetting != SqlCommandColumnEncryptionSetting.Enabled && (cmd.ColumnEncryptionSetting != SqlCommandColumnEncryptionSetting.UseConnectionSetting || !cmd.Connection.IsColumnEncryptionSettingEnabled))
							{
								throw SQL.ParamInvalidForceColumnEncryptionSetting(parameterByIndex.ParameterName, sqlRPC.GetCommandTextOrRpcName());
							}
							if (parameterByIndex.ForceColumnEncryption && parameterByIndex.CipherMetadata == null && (direction == ParameterDirection.Input || direction == ParameterDirection.InputOutput))
							{
								throw SQL.ParamUnExpectedEncryptionMetadata(parameterByIndex.ParameterName, sqlRPC.GetCommandTextOrRpcName());
							}
							if (enableOptimizedParameterBinding && (direction == ParameterDirection.Output || direction == ParameterDirection.InputOutput))
							{
								throw SQL.ParameterDirectionInvalidForOptimizedBinding(parameterByIndex.ParameterName);
							}
							parameterByIndex.Validate(i, isCommandProc);
							MetaType metaType = parameterByIndex.InternalMetaType;
							if (metaType.Is2008Type)
							{
								this.WriteSmiParameter(parameterByIndex, i, (b & 2) > 0, stateObj, enableOptimizedParameterBinding, flag3);
							}
							else
							{
								if ((!this._is2000 && !metaType.Is70Supported) || (!this._is2005 && !metaType.Is80Supported) || (!this._is2008 && !metaType.Is90Supported))
								{
									throw ADP.VersionDoesNotSupportDataType(metaType.TypeName);
								}
								object obj = null;
								bool flag4 = true;
								bool flag5 = false;
								bool flag6 = false;
								if (direction == ParameterDirection.Output)
								{
									flag5 = parameterByIndex.ParameterIsSqlType;
									parameterByIndex.Value = null;
									parameterByIndex.ParameterIsSqlType = flag5;
								}
								else
								{
									obj = parameterByIndex.GetCoercedValue();
									flag4 = parameterByIndex.IsNull;
									if (!flag4)
									{
										flag5 = parameterByIndex.CoercedValueIsSqlType;
										flag6 = parameterByIndex.CoercedValueIsDataFeed;
									}
								}
								this.WriteParameterName(parameterByIndex.ParameterNameFixed, stateObj, enableOptimizedParameterBinding);
								stateObj.WriteByte(b);
								int num3 = (metaType.IsSizeInCharacters ? (parameterByIndex.GetParameterSize() * 2) : parameterByIndex.GetParameterSize());
								int num4;
								if (metaType.TDSType != 240)
								{
									num4 = parameterByIndex.GetActualSize();
								}
								else
								{
									num4 = 0;
								}
								byte b2 = 0;
								byte b3 = 0;
								if (metaType.SqlDbType == SqlDbType.Decimal)
								{
									b2 = parameterByIndex.GetActualPrecision();
									b3 = parameterByIndex.GetActualScale();
									if (b2 > 38)
									{
										throw SQL.PrecisionValueOutOfRange(b2);
									}
									if (!flag4)
									{
										if (flag5)
										{
											obj = TdsParser.AdjustSqlDecimalScale((SqlDecimal)obj, (int)b3);
											if (b2 != 0 && b2 < ((SqlDecimal)obj).Precision)
											{
												throw ADP.ParameterValueOutOfRange((SqlDecimal)obj);
											}
										}
										else
										{
											obj = TdsParser.AdjustDecimalScale((decimal)obj, (int)b3);
											SqlDecimal sqlDecimal = new SqlDecimal((decimal)obj);
											if (b2 != 0 && b2 < sqlDecimal.Precision)
											{
												throw ADP.ParameterValueOutOfRange((decimal)obj);
											}
										}
									}
								}
								bool flag7 = (b & 8) > 0;
								SqlColumnEncryptionInputParameterInfo sqlColumnEncryptionInputParameterInfo = null;
								if (flag7)
								{
									byte[] array2 = null;
									if (!flag4)
									{
										try
										{
											byte[] array3;
											if (flag5)
											{
												array3 = this.SerializeUnencryptedSqlValue(obj, metaType, num4, parameterByIndex.Offset, parameterByIndex.NormalizationRuleVersion, stateObj);
											}
											else
											{
												array3 = this.SerializeUnencryptedValue(obj, metaType, parameterByIndex.GetActualScale(), num4, parameterByIndex.Offset, flag6, parameterByIndex.NormalizationRuleVersion, stateObj);
											}
											array2 = SqlSecurityUtility.EncryptWithKey(array3, parameterByIndex.CipherMetadata, this._connHandler.Connection, cmd);
											goto IL_0661;
										}
										catch (Exception ex)
										{
											throw SQL.ParamEncryptionFailed(parameterByIndex.ParameterName, null, ex);
										}
										goto IL_065E;
									}
									goto IL_065E;
									IL_0661:
									metaType = MetaType.MetaMaxVarBinary;
									num3 = -1;
									num4 = ((array2 == null) ? 0 : array2.Length);
									sqlColumnEncryptionInputParameterInfo = new SqlColumnEncryptionInputParameterInfo(parameterByIndex.GetMetadataForTypeInfo(), parameterByIndex.CipherMetadata);
									obj = array2;
									flag5 = false;
									goto IL_0694;
									IL_065E:
									array2 = null;
									goto IL_0661;
								}
								IL_0694:
								stateObj.WriteByte(metaType.NullableType);
								if (metaType.TDSType == 98)
								{
									this.WriteSqlVariantValue(flag5 ? MetaType.GetComValueFromSqlVariant(obj) : obj, parameterByIndex.GetActualSize(), parameterByIndex.Offset, stateObj, true);
								}
								else
								{
									int num5 = 0;
									int num6 = 0;
									if (metaType.IsAnsiType)
									{
										if (!flag4 && !flag6)
										{
											string text;
											if (flag5)
											{
												if (obj is SqlString)
												{
													text = ((SqlString)obj).Value;
												}
												else
												{
													text = new string(((SqlChars)obj).Value);
												}
											}
											else
											{
												text = (string)obj;
											}
											num5 = this.GetEncodingCharLength(text, num4, parameterByIndex.Offset, this._defaultEncoding);
										}
										if (metaType.IsPlp)
										{
											this.WriteShort(65535, stateObj);
										}
										else
										{
											num6 = ((num3 > num5) ? num3 : num5);
											if (num6 == 0)
											{
												if (metaType.IsNCharType)
												{
													num6 = 2;
												}
												else
												{
													num6 = 1;
												}
											}
											this.WriteParameterVarLen(metaType, num6, false, stateObj, false);
										}
									}
									else if (metaType.SqlDbType == SqlDbType.Timestamp)
									{
										this.WriteParameterVarLen(metaType, 8, false, stateObj, false);
									}
									else if (metaType.SqlDbType == SqlDbType.Udt)
									{
										byte[] array4 = null;
										Format format = Format.Native;
										int num7 = (this.Is2008OrNewer ? int.MaxValue : 32767);
										if (!flag4)
										{
											byte[] array5 = obj as byte[];
											if (array5 != null)
											{
												array4 = array5;
											}
											else
											{
												SqlBytes sqlBytes = obj as SqlBytes;
												if (sqlBytes != null)
												{
													StorageState storage = sqlBytes.Storage;
													if (storage != StorageState.Buffer)
													{
														if (storage - StorageState.Stream <= 1)
														{
															array4 = sqlBytes.Value;
														}
													}
													else
													{
														array4 = sqlBytes.Buffer;
													}
												}
												else
												{
													array4 = this._connHandler.Connection.GetBytes(obj, out format, out num6);
												}
											}
											num3 = array4.Length;
											if (num3 < 0 || (num3 >= num7 && num6 != -1))
											{
												throw SQL.UDTInvalidSize(num6, num7);
											}
										}
										string[] array6 = SqlParameter.ParseTypeName(parameterByIndex.UdtTypeName, true);
										if (!ADP.IsEmpty(array6[0]) && 255 < array6[0].Length)
										{
											throw ADP.ArgumentOutOfRange("names");
										}
										if (!ADP.IsEmpty(array6[1]) && 255 < array6[array6.Length - 2].Length)
										{
											throw ADP.ArgumentOutOfRange("names");
										}
										if (255 < array6[2].Length)
										{
											throw ADP.ArgumentOutOfRange("names");
										}
										this.WriteUDTMetaData(obj, array6[0], array6[1], array6[2], stateObj);
										if (!flag4)
										{
											this.WriteUnsignedLong((ulong)((long)array4.Length), stateObj);
											if (array4.Length != 0)
											{
												this.WriteInt(array4.Length, stateObj);
												stateObj.WriteByteArray(array4, array4.Length, 0, true, null);
											}
											this.WriteInt(0, stateObj);
											goto IL_0F6A;
										}
										this.WriteUnsignedLong(ulong.MaxValue, stateObj);
										goto IL_0F6A;
									}
									else if (metaType.IsPlp)
									{
										if (metaType.SqlDbType != SqlDbType.Xml)
										{
											this.WriteShort(65535, stateObj);
										}
									}
									else if (!metaType.IsVarTime && metaType.SqlDbType != SqlDbType.Date)
									{
										num6 = ((num3 > num4) ? num3 : num4);
										if (num6 == 0 && this.Is2005OrNewer)
										{
											if (metaType.IsNCharType)
											{
												num6 = 2;
											}
											else
											{
												num6 = 1;
											}
										}
										this.WriteParameterVarLen(metaType, num6, false, stateObj, false);
									}
									if (metaType.SqlDbType == SqlDbType.Decimal)
									{
										if (b2 == 0)
										{
											if (this._is2000)
											{
												stateObj.WriteByte(29);
											}
											else
											{
												stateObj.WriteByte(28);
											}
										}
										else
										{
											stateObj.WriteByte(b2);
										}
										stateObj.WriteByte(b3);
									}
									else if (metaType.IsVarTime)
									{
										stateObj.WriteByte(parameterByIndex.GetActualScale());
									}
									if (this._is2005 && metaType.SqlDbType == SqlDbType.Xml)
									{
										if (!string.IsNullOrEmpty(parameterByIndex.XmlSchemaCollectionDatabase) || !string.IsNullOrEmpty(parameterByIndex.XmlSchemaCollectionOwningSchema) || !string.IsNullOrEmpty(parameterByIndex.XmlSchemaCollectionName))
										{
											stateObj.WriteByte(1);
											if (!string.IsNullOrEmpty(parameterByIndex.XmlSchemaCollectionDatabase))
											{
												int num = parameterByIndex.XmlSchemaCollectionDatabase.Length;
												stateObj.WriteByte((byte)num);
												this.WriteString(parameterByIndex.XmlSchemaCollectionDatabase, num, 0, stateObj, true);
											}
											else
											{
												stateObj.WriteByte(0);
											}
											if (!string.IsNullOrEmpty(parameterByIndex.XmlSchemaCollectionOwningSchema))
											{
												int num = parameterByIndex.XmlSchemaCollectionOwningSchema.Length;
												stateObj.WriteByte((byte)num);
												this.WriteString(parameterByIndex.XmlSchemaCollectionOwningSchema, num, 0, stateObj, true);
											}
											else
											{
												stateObj.WriteByte(0);
											}
											if (!string.IsNullOrEmpty(parameterByIndex.XmlSchemaCollectionName))
											{
												int num = parameterByIndex.XmlSchemaCollectionName.Length;
												this.WriteShort((int)((short)num), stateObj);
												this.WriteString(parameterByIndex.XmlSchemaCollectionName, num, 0, stateObj, true);
											}
											else
											{
												this.WriteShort(0, stateObj);
											}
										}
										else
										{
											stateObj.WriteByte(0);
										}
									}
									else if (this._is2000 && metaType.IsCharType)
									{
										SqlCollation sqlCollation = ((parameterByIndex.Collation != null) ? parameterByIndex.Collation : this._defaultCollation);
										this.WriteUnsignedInt(sqlCollation._info, stateObj);
										stateObj.WriteByte(sqlCollation._sortId);
									}
									if (num5 == 0)
									{
										this.WriteParameterVarLen(metaType, num4, flag4, stateObj, flag6);
									}
									else
									{
										this.WriteParameterVarLen(metaType, num5, flag4, stateObj, flag6);
									}
									Task task = null;
									if (!flag4)
									{
										if (flag5)
										{
											task = this.WriteSqlValue(obj, metaType, num4, num5, parameterByIndex.Offset, stateObj);
										}
										else
										{
											task = this.WriteValue(obj, metaType, flag7 ? 0 : parameterByIndex.GetActualScale(), num4, num5, flag7 ? 0 : parameterByIndex.Offset, stateObj, flag7 ? 0 : parameterByIndex.Size, flag6);
										}
									}
									if (flag7)
									{
										task = this.WriteEncryptionMetadata(task, sqlColumnEncryptionInputParameterInfo, stateObj);
									}
									if (!sync)
									{
										if (task == null)
										{
											task = stateObj.WaitForAccumulatedWrites();
										}
										if (task != null)
										{
											Task task2 = null;
											if (completion == null)
											{
												completion = new TaskCompletionSource<object>();
												task2 = completion.Task;
											}
											Task task3 = task;
											TaskCompletionSource<object> completion2 = completion;
											Action<object> action = delegate(object state)
											{
												TdsParser tdsParser = (TdsParser)state;
												this.TdsExecuteRPC(cmd, rpcArray, timeout, inSchema, notificationRequest, stateObj, isCommandProc, sync, completion, ii, i + 1);
											};
											Action<Exception, object> action2;
											if ((action2 = <>9__1) == null)
											{
												action2 = (<>9__1 = delegate(Exception exc, object state)
												{
													((TdsParser)state).TdsExecuteRPC_OnFailure(exc, stateObj);
												});
											}
											AsyncHelper.ContinueTaskWithState(task3, completion2, this, action, action2, null, null, this._connHandler, null);
											if (flag2)
											{
												task2.ContinueWith(delegate(Task _, object state)
												{
													((TdsParser)state)._connHandler._parserLock.Release();
												}, this, TaskScheduler.Default);
												flag2 = false;
											}
											return task2;
										}
									}
								}
							}
							IL_0F6A:
							num8 = i;
						}
						if (ii < rpcArray.Length - 1)
						{
							if (this._is2005)
							{
								stateObj.WriteByte(byte.MaxValue);
							}
							else
							{
								stateObj.WriteByte(128);
							}
						}
						num8 = ii;
					}
					Task task4 = stateObj.ExecuteFlush();
					if (task4 != null)
					{
						Task task5 = null;
						if (completion == null)
						{
							completion = new TaskCompletionSource<object>();
							task5 = completion.Task;
						}
						bool taskReleaseConnectionLock = flag2;
						task4.ContinueWith(delegate(Task tsk)
						{
							this.ExecuteFlushTaskCallback(tsk, stateObj, completion, taskReleaseConnectionLock);
						}, TaskScheduler.Default);
						flag2 = false;
						return task5;
					}
				}
				catch (Exception ex2)
				{
					this.FailureCleanup(stateObj, ex2);
					throw;
				}
				this.FinalizeExecuteRPC(stateObj);
				if (completion != null)
				{
					completion.SetResult(null);
				}
				task6 = null;
			}
			catch (Exception ex3)
			{
				this.FinalizeExecuteRPC(stateObj);
				if (completion == null)
				{
					throw;
				}
				completion.SetException(ex3);
				task6 = null;
			}
			finally
			{
				if (flag2)
				{
					this._connHandler._parserLock.Release();
				}
			}
			return task6;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0005ABBC File Offset: 0x00058DBC
		private void WriteEnclaveInfo(TdsParserStateObject stateObj, byte[] enclavePackage)
		{
			if (this.TceVersionSupported >= 2)
			{
				if (enclavePackage != null)
				{
					this.WriteShort((int)((short)enclavePackage.Length), stateObj);
					stateObj.WriteByteArray(enclavePackage, enclavePackage.Length, 0, true, null);
					return;
				}
				this.WriteShort(0, stateObj);
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0005ABEC File Offset: 0x00058DEC
		private void FinalizeExecuteRPC(TdsParserStateObject stateObj)
		{
			stateObj.SniContext = SniContext.Snix_Read;
			this._asyncWrite = false;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0005AC00 File Offset: 0x00058E00
		private void TdsExecuteRPC_OnFailure(Exception exc, TdsParserStateObject stateObj)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.FailureCleanup(stateObj, exc);
			}
			catch (OutOfMemoryException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				this._connHandler.DoomThisConnection();
				throw;
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0005AC70 File Offset: 0x00058E70
		private void ExecuteFlushTaskCallback(Task tsk, TdsParserStateObject stateObj, TaskCompletionSource<object> completion, bool releaseConnectionLock)
		{
			try
			{
				this.FinalizeExecuteRPC(stateObj);
				if (tsk.Exception != null)
				{
					Exception ex = tsk.Exception.InnerException;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this.FailureCleanup(stateObj, tsk.Exception);
					}
					catch (OutOfMemoryException ex2)
					{
						this._connHandler.DoomThisConnection();
						completion.SetException(ex2);
						throw;
					}
					catch (StackOverflowException ex3)
					{
						this._connHandler.DoomThisConnection();
						completion.SetException(ex3);
						throw;
					}
					catch (ThreadAbortException ex4)
					{
						this._connHandler.DoomThisConnection();
						completion.SetException(ex4);
						throw;
					}
					catch (Exception ex5)
					{
						ex = ex5;
					}
					completion.SetException(ex);
				}
				else
				{
					completion.SetResult(null);
				}
			}
			finally
			{
				if (releaseConnectionLock)
				{
					this._connHandler._parserLock.Release();
				}
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0005AD5C File Offset: 0x00058F5C
		private void WriteParameterName(string parameterName, TdsParserStateObject stateObj, bool isAnonymous)
		{
			if (!isAnonymous && !string.IsNullOrEmpty(parameterName))
			{
				int num = parameterName.Length & 255;
				stateObj.WriteByte((byte)num);
				this.WriteString(parameterName, num, 0, stateObj, true);
				return;
			}
			stateObj.WriteByte(0);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0005ADA0 File Offset: 0x00058FA0
		private void WriteSmiParameter(SqlParameter param, int paramIndex, bool sendDefault, TdsParserStateObject stateObj, bool isAnonymous, bool advancedTraceIsOn)
		{
			ParameterPeekAheadValue parameterPeekAheadValue;
			SmiParameterMetaData smiParameterMetaData = param.MetaDataForSmi(out parameterPeekAheadValue);
			if (!this._is2008)
			{
				MetaType metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(smiParameterMetaData.SqlDbType, smiParameterMetaData.IsMultiValued);
				throw ADP.VersionDoesNotSupportDataType(metaTypeFromSqlDbType.TypeName);
			}
			object obj;
			ExtendedClrTypeCode extendedClrTypeCode;
			if (sendDefault)
			{
				if (SqlDbType.Structured == smiParameterMetaData.SqlDbType && smiParameterMetaData.IsMultiValued)
				{
					obj = TdsParser.__tvpEmptyValue;
					extendedClrTypeCode = ExtendedClrTypeCode.IEnumerableOfSqlDataRecord;
				}
				else
				{
					obj = null;
					extendedClrTypeCode = ExtendedClrTypeCode.DBNull;
				}
			}
			else if (param.Direction == ParameterDirection.Output)
			{
				bool parameterIsSqlType = param.ParameterIsSqlType;
				param.Value = null;
				obj = null;
				extendedClrTypeCode = ExtendedClrTypeCode.DBNull;
				param.ParameterIsSqlType = parameterIsSqlType;
			}
			else
			{
				obj = param.GetCoercedValue();
				extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(smiParameterMetaData.SqlDbType, smiParameterMetaData.IsMultiValued, obj, null, 210UL);
			}
			int num = ((sendDefault > false) ? 1 : 0);
			if (advancedTraceIsOn)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, string, int, string>("<sc.TdsParser.WriteSmiParameter|ADV> {0}, Sending parameter '{1}', default flag={2}, metadata:{3}", this.ObjectID, param.ParameterName, num, smiParameterMetaData.TraceString(3));
			}
			this.WriteSmiParameterMetaData(smiParameterMetaData, sendDefault, isAnonymous, stateObj);
			TdsParameterSetter tdsParameterSetter = new TdsParameterSetter(stateObj, smiParameterMetaData);
			ValueUtilsSmi.SetCompatibleValueV200(new SmiEventSink_Default(), tdsParameterSetter, 0, smiParameterMetaData, obj, extendedClrTypeCode, param.Offset, parameterPeekAheadValue);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0005AEA8 File Offset: 0x000590A8
		private void WriteSmiParameterMetaData(SmiParameterMetaData metaData, bool sendDefault, bool isAnonymous, TdsParserStateObject stateObj)
		{
			byte b = 0;
			if (ParameterDirection.Output == metaData.Direction || ParameterDirection.InputOutput == metaData.Direction)
			{
				b |= 1;
			}
			if (sendDefault)
			{
				b |= 2;
			}
			this.WriteParameterName(metaData.Name, stateObj, isAnonymous);
			stateObj.WriteByte(b);
			this.WriteSmiTypeInfo(metaData, stateObj);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0005AEF8 File Offset: 0x000590F8
		private void WriteSmiTypeInfo(SmiExtendedMetaData metaData, TdsParserStateObject stateObj)
		{
			checked
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					stateObj.WriteByte(38);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Binary:
					stateObj.WriteByte(173);
					this.WriteUnsignedShort((ushort)metaData.MaxLength, stateObj);
					return;
				case SqlDbType.Bit:
					stateObj.WriteByte(104);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Char:
					stateObj.WriteByte(175);
					this.WriteUnsignedShort((ushort)metaData.MaxLength, stateObj);
					this.WriteUnsignedInt(this._defaultCollation._info, stateObj);
					stateObj.WriteByte(this._defaultCollation._sortId);
					return;
				case SqlDbType.DateTime:
					stateObj.WriteByte(111);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Decimal:
					stateObj.WriteByte(108);
					stateObj.WriteByte((byte)MetaType.MetaDecimal.FixedLength);
					stateObj.WriteByte((metaData.Precision == 0) ? 1 : metaData.Precision);
					stateObj.WriteByte(metaData.Scale);
					return;
				case SqlDbType.Float:
					stateObj.WriteByte(109);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Image:
					stateObj.WriteByte(165);
					this.WriteUnsignedShort(ushort.MaxValue, stateObj);
					return;
				case SqlDbType.Int:
					stateObj.WriteByte(38);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Money:
					stateObj.WriteByte(110);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.NChar:
					stateObj.WriteByte(239);
					this.WriteUnsignedShort((ushort)(metaData.MaxLength * 2L), stateObj);
					this.WriteUnsignedInt(this._defaultCollation._info, stateObj);
					stateObj.WriteByte(this._defaultCollation._sortId);
					return;
				case SqlDbType.NText:
					stateObj.WriteByte(231);
					this.WriteUnsignedShort(ushort.MaxValue, stateObj);
					this.WriteUnsignedInt(this._defaultCollation._info, stateObj);
					stateObj.WriteByte(this._defaultCollation._sortId);
					return;
				case SqlDbType.NVarChar:
					stateObj.WriteByte(231);
					if (-1L == metaData.MaxLength)
					{
						this.WriteUnsignedShort(ushort.MaxValue, stateObj);
					}
					else
					{
						this.WriteUnsignedShort((ushort)(metaData.MaxLength * 2L), stateObj);
					}
					this.WriteUnsignedInt(this._defaultCollation._info, stateObj);
					stateObj.WriteByte(this._defaultCollation._sortId);
					return;
				case SqlDbType.Real:
					stateObj.WriteByte(109);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.UniqueIdentifier:
					stateObj.WriteByte(36);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.SmallDateTime:
					stateObj.WriteByte(111);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.SmallInt:
					stateObj.WriteByte(38);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.SmallMoney:
					stateObj.WriteByte(110);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.Text:
					stateObj.WriteByte(167);
					this.WriteUnsignedShort(ushort.MaxValue, stateObj);
					this.WriteUnsignedInt(this._defaultCollation._info, stateObj);
					stateObj.WriteByte(this._defaultCollation._sortId);
					return;
				case SqlDbType.Timestamp:
					stateObj.WriteByte(173);
					this.WriteShort((int)metaData.MaxLength, stateObj);
					return;
				case SqlDbType.TinyInt:
					stateObj.WriteByte(38);
					stateObj.WriteByte((byte)metaData.MaxLength);
					return;
				case SqlDbType.VarBinary:
					stateObj.WriteByte(165);
					this.WriteUnsignedShort(unchecked((ushort)metaData.MaxLength), stateObj);
					return;
				case SqlDbType.VarChar:
					stateObj.WriteByte(167);
					this.WriteUnsignedShort(unchecked((ushort)metaData.MaxLength), stateObj);
					this.WriteUnsignedInt(this._defaultCollation._info, stateObj);
					stateObj.WriteByte(this._defaultCollation._sortId);
					return;
				case SqlDbType.Variant:
					stateObj.WriteByte(98);
					this.WriteInt((int)metaData.MaxLength, stateObj);
					return;
				case (SqlDbType)24:
				case (SqlDbType)26:
				case (SqlDbType)27:
				case (SqlDbType)28:
					break;
				case SqlDbType.Xml:
					stateObj.WriteByte(241);
					if (ADP.IsEmpty(metaData.TypeSpecificNamePart1) && ADP.IsEmpty(metaData.TypeSpecificNamePart2) && ADP.IsEmpty(metaData.TypeSpecificNamePart3))
					{
						stateObj.WriteByte(0);
						return;
					}
					stateObj.WriteByte(1);
					this.WriteIdentifier(metaData.TypeSpecificNamePart1, stateObj);
					this.WriteIdentifier(metaData.TypeSpecificNamePart2, stateObj);
					this.WriteIdentifierWithShortLength(metaData.TypeSpecificNamePart3, stateObj);
					return;
				case SqlDbType.Udt:
					stateObj.WriteByte(240);
					this.WriteIdentifier(metaData.TypeSpecificNamePart1, stateObj);
					this.WriteIdentifier(metaData.TypeSpecificNamePart2, stateObj);
					this.WriteIdentifier(metaData.TypeSpecificNamePart3, stateObj);
					return;
				case SqlDbType.Structured:
					if (metaData.IsMultiValued)
					{
						this.WriteTvpTypeInfo(metaData, stateObj);
						return;
					}
					break;
				case SqlDbType.Date:
					stateObj.WriteByte(40);
					return;
				case SqlDbType.Time:
					stateObj.WriteByte(41);
					stateObj.WriteByte(metaData.Scale);
					return;
				case SqlDbType.DateTime2:
					stateObj.WriteByte(42);
					stateObj.WriteByte(metaData.Scale);
					return;
				case SqlDbType.DateTimeOffset:
					stateObj.WriteByte(43);
					stateObj.WriteByte(metaData.Scale);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0005B3E0 File Offset: 0x000595E0
		private void WriteTvpTypeInfo(SmiExtendedMetaData metaData, TdsParserStateObject stateObj)
		{
			stateObj.WriteByte(243);
			this.WriteIdentifier(metaData.TypeSpecificNamePart1, stateObj);
			this.WriteIdentifier(metaData.TypeSpecificNamePart2, stateObj);
			this.WriteIdentifier(metaData.TypeSpecificNamePart3, stateObj);
			if (metaData.FieldMetaData.Count == 0)
			{
				this.WriteUnsignedShort(ushort.MaxValue, stateObj);
			}
			else
			{
				this.WriteUnsignedShort(checked((ushort)metaData.FieldMetaData.Count), stateObj);
				SmiDefaultFieldsProperty smiDefaultFieldsProperty = (SmiDefaultFieldsProperty)metaData.ExtendedProperties[SmiPropertySelector.DefaultFields];
				for (int i = 0; i < metaData.FieldMetaData.Count; i++)
				{
					this.WriteTvpColumnMetaData(metaData.FieldMetaData[i], smiDefaultFieldsProperty[i], stateObj);
				}
				this.WriteTvpOrderUnique(metaData, stateObj);
			}
			stateObj.WriteByte(0);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0005B4A0 File Offset: 0x000596A0
		private void WriteTvpColumnMetaData(SmiExtendedMetaData md, bool isDefault, TdsParserStateObject stateObj)
		{
			if (SqlDbType.Timestamp == md.SqlDbType)
			{
				this.WriteUnsignedInt(80U, stateObj);
			}
			else
			{
				this.WriteUnsignedInt(0U, stateObj);
			}
			ushort num = 1;
			if (isDefault)
			{
				num |= 512;
			}
			this.WriteUnsignedShort(num, stateObj);
			this.WriteSmiTypeInfo(md, stateObj);
			this.WriteIdentifier(null, stateObj);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0005B4F0 File Offset: 0x000596F0
		private void WriteTvpOrderUnique(SmiExtendedMetaData metaData, TdsParserStateObject stateObj)
		{
			SmiOrderProperty smiOrderProperty = (SmiOrderProperty)metaData.ExtendedProperties[SmiPropertySelector.SortOrder];
			SmiUniqueKeyProperty smiUniqueKeyProperty = (SmiUniqueKeyProperty)metaData.ExtendedProperties[SmiPropertySelector.UniqueKey];
			List<TdsParser.TdsOrderUnique> list = new List<TdsParser.TdsOrderUnique>(metaData.FieldMetaData.Count);
			for (int i = 0; i < metaData.FieldMetaData.Count; i++)
			{
				byte b = 0;
				SmiOrderProperty.SmiColumnOrder smiColumnOrder = smiOrderProperty[i];
				if (smiColumnOrder._order == SortOrder.Ascending)
				{
					b = 1;
				}
				else if (SortOrder.Descending == smiColumnOrder._order)
				{
					b = 2;
				}
				if (smiUniqueKeyProperty[i])
				{
					b |= 4;
				}
				if (b != 0)
				{
					list.Add(new TdsParser.TdsOrderUnique(checked((short)(i + 1)), b));
				}
			}
			if (0 < list.Count)
			{
				stateObj.WriteByte(16);
				this.WriteShort(list.Count, stateObj);
				foreach (TdsParser.TdsOrderUnique tdsOrderUnique in list)
				{
					this.WriteShort((int)tdsOrderUnique.ColumnOrdinal, stateObj);
					stateObj.WriteByte(tdsOrderUnique.Flags);
				}
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0005B60C File Offset: 0x0005980C
		internal Task WriteBulkCopyDone(TdsParserStateObject stateObj)
		{
			if (this.State != TdsParserState.OpenNotLoggedIn && this.State != TdsParserState.OpenLoggedIn)
			{
				throw ADP.ClosedConnectionError();
			}
			stateObj.WriteByte(253);
			this.WriteShort(0, stateObj);
			this.WriteShort(0, stateObj);
			this.WriteInt(0, stateObj);
			stateObj._pendingData = true;
			stateObj._messageStatus = 0;
			return stateObj.WritePacket(1, false);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0005B66C File Offset: 0x0005986C
		internal void LoadColumnEncryptionKeys(_SqlMetaDataSet metadataCollection, SqlConnection connection, SqlCommand command = null)
		{
			if (this._serverSupportsColumnEncryption && this.ShouldEncryptValuesForBulkCopy())
			{
				for (int i = 0; i < metadataCollection.Length; i++)
				{
					if (metadataCollection[i] != null)
					{
						_SqlMetaData sqlMetaData = metadataCollection[i];
						if (sqlMetaData.isEncrypted)
						{
							SqlSecurityUtility.DecryptSymmetricKey(sqlMetaData.cipherMD, connection, command);
						}
					}
				}
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0005B6C0 File Offset: 0x000598C0
		internal void WriteEncryptionEntries(ref SqlTceCipherInfoTable cekTable, TdsParserStateObject stateObj)
		{
			for (int i = 0; i < cekTable.Size; i++)
			{
				this.WriteInt(cekTable[i].DatabaseId, stateObj);
				this.WriteInt(cekTable[i].CekId, stateObj);
				this.WriteInt(cekTable[i].CekVersion, stateObj);
				stateObj.WriteByteArray(cekTable[i].CekMdVersion, 8, 0, true, null);
				stateObj.WriteByte(0);
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0005B73C File Offset: 0x0005993C
		internal void WriteCekTable(_SqlMetaDataSet metadataCollection, TdsParserStateObject stateObj)
		{
			if (!this._serverSupportsColumnEncryption)
			{
				return;
			}
			if (metadataCollection.cekTable == null || !this.ShouldEncryptValuesForBulkCopy())
			{
				this.WriteShort(0, stateObj);
				return;
			}
			SqlTceCipherInfoTable cekTable = metadataCollection.cekTable;
			ushort num = (ushort)cekTable.Size;
			this.WriteShort((int)num, stateObj);
			this.WriteEncryptionEntries(ref cekTable, stateObj);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0005B78C File Offset: 0x0005998C
		internal void WriteTceUserTypeAndTypeInfo(SqlMetaDataPriv mdPriv, TdsParserStateObject stateObj)
		{
			this.WriteInt(0, stateObj);
			stateObj.WriteByte(mdPriv.tdsType);
			SqlDbType type = mdPriv.type;
			if (type != SqlDbType.Decimal)
			{
				if (type != SqlDbType.Date)
				{
					if (type - SqlDbType.Time <= 2)
					{
						stateObj.WriteByte(mdPriv.scale);
						return;
					}
					this.WriteTokenLength(mdPriv.tdsType, mdPriv.length, stateObj);
					if (mdPriv.metaType.IsCharType && this._is2000)
					{
						this.WriteUnsignedInt(mdPriv.collation._info, stateObj);
						stateObj.WriteByte(mdPriv.collation._sortId);
					}
				}
				return;
			}
			this.WriteTokenLength(mdPriv.tdsType, mdPriv.length, stateObj);
			stateObj.WriteByte(mdPriv.precision);
			stateObj.WriteByte(mdPriv.scale);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0005B850 File Offset: 0x00059A50
		internal void WriteCryptoMetadata(_SqlMetaData md, TdsParserStateObject stateObj)
		{
			if (!this._serverSupportsColumnEncryption || !md.isEncrypted || !this.ShouldEncryptValuesForBulkCopy())
			{
				return;
			}
			this.WriteShort((int)md.cipherMD.CekTableOrdinal, stateObj);
			this.WriteTceUserTypeAndTypeInfo(md.baseTI, stateObj);
			stateObj.WriteByte(md.cipherMD.CipherAlgorithmId);
			if (md.cipherMD.CipherAlgorithmId == 0)
			{
				stateObj.WriteByte((byte)md.cipherMD.CipherAlgorithmName.Length);
				this.WriteString(md.cipherMD.CipherAlgorithmName, stateObj, true);
			}
			stateObj.WriteByte(md.cipherMD.EncryptionType);
			stateObj.WriteByte(md.cipherMD.NormalizationRuleVersion);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0005B900 File Offset: 0x00059B00
		internal void WriteBulkCopyMetaData(_SqlMetaDataSet metadataCollection, int count, TdsParserStateObject stateObj)
		{
			if (this.State != TdsParserState.OpenNotLoggedIn && this.State != TdsParserState.OpenLoggedIn)
			{
				throw ADP.ClosedConnectionError();
			}
			stateObj.WriteByte(129);
			this.WriteShort(count, stateObj);
			this.WriteCekTable(metadataCollection, stateObj);
			for (int i = 0; i < metadataCollection.Length; i++)
			{
				if (metadataCollection[i] != null)
				{
					_SqlMetaData sqlMetaData = metadataCollection[i];
					if (this.Is2005OrNewer)
					{
						this.WriteInt(0, stateObj);
					}
					else
					{
						this.WriteShort(0, stateObj);
					}
					ushort num = (ushort)(sqlMetaData.Updatability << 2);
					num |= ((sqlMetaData.IsNullable > false) ? 1 : 0);
					num |= (sqlMetaData.IsIdentity ? 16 : 0);
					if (this._serverSupportsColumnEncryption && this.ShouldEncryptValuesForBulkCopy())
					{
						num |= (sqlMetaData.isEncrypted ? 2048 : 0);
					}
					this.WriteShort((int)num, stateObj);
					SqlDbType type = sqlMetaData.type;
					if (type != SqlDbType.Decimal)
					{
						switch (type)
						{
						case SqlDbType.Xml:
							stateObj.WriteByteArray(TdsParser.s_xmlMetadataSubstituteSequence, TdsParser.s_xmlMetadataSubstituteSequence.Length, 0, true, null);
							goto IL_01F3;
						case SqlDbType.Udt:
							stateObj.WriteByte(165);
							this.WriteTokenLength(165, sqlMetaData.length, stateObj);
							goto IL_01F3;
						case SqlDbType.Date:
							stateObj.WriteByte(sqlMetaData.tdsType);
							goto IL_01F3;
						case SqlDbType.Time:
						case SqlDbType.DateTime2:
						case SqlDbType.DateTimeOffset:
							stateObj.WriteByte(sqlMetaData.tdsType);
							stateObj.WriteByte(sqlMetaData.scale);
							goto IL_01F3;
						}
						stateObj.WriteByte(sqlMetaData.tdsType);
						this.WriteTokenLength(sqlMetaData.tdsType, sqlMetaData.length, stateObj);
						if (sqlMetaData.metaType.IsCharType && this._is2000)
						{
							this.WriteUnsignedInt(sqlMetaData.collation._info, stateObj);
							stateObj.WriteByte(sqlMetaData.collation._sortId);
						}
					}
					else
					{
						stateObj.WriteByte(sqlMetaData.tdsType);
						this.WriteTokenLength(sqlMetaData.tdsType, sqlMetaData.length, stateObj);
						stateObj.WriteByte(sqlMetaData.precision);
						stateObj.WriteByte(sqlMetaData.scale);
					}
					IL_01F3:
					if (sqlMetaData.metaType.IsLong && !sqlMetaData.metaType.IsPlp)
					{
						this.WriteShort(sqlMetaData.tableName.Length, stateObj);
						this.WriteString(sqlMetaData.tableName, stateObj, true);
					}
					this.WriteCryptoMetadata(sqlMetaData, stateObj);
					stateObj.WriteByte((byte)sqlMetaData.column.Length);
					this.WriteString(sqlMetaData.column, stateObj, true);
				}
			}
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0005BB74 File Offset: 0x00059D74
		internal bool ShouldEncryptValuesForBulkCopy()
		{
			return this._connHandler != null && this._connHandler.ConnectionOptions != null && SqlConnectionColumnEncryptionSetting.Enabled == this._connHandler.ConnectionOptions.ColumnEncryptionSetting;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0005BBA4 File Offset: 0x00059DA4
		internal object EncryptColumnValue(object value, SqlMetaDataPriv metadata, string column, TdsParserStateObject stateObj, bool isDataFeed, bool isSqlType)
		{
			if (isDataFeed)
			{
				SQL.StreamNotSupportOnEncryptedColumn(column);
			}
			byte nullableType = metadata.baseTI.metaType.NullableType;
			int num;
			if (nullableType <= 167)
			{
				if (nullableType <= 99)
				{
					switch (nullableType)
					{
					case 34:
						break;
					case 35:
						goto IL_00F4;
					case 36:
						num = 16;
						goto IL_01B4;
					default:
						if (nullableType != 99)
						{
							goto IL_01A8;
						}
						goto IL_0156;
					}
				}
				else if (nullableType != 165)
				{
					if (nullableType != 167)
					{
						goto IL_01A8;
					}
					goto IL_00F4;
				}
			}
			else if (nullableType <= 175)
			{
				if (nullableType != 173)
				{
					if (nullableType != 175)
					{
						goto IL_01A8;
					}
					goto IL_00F4;
				}
			}
			else
			{
				if (nullableType != 231 && nullableType != 239)
				{
					goto IL_01A8;
				}
				goto IL_0156;
			}
			num = (isSqlType ? ((SqlBinary)value).Length : ((byte[])value).Length);
			if (metadata.baseTI.length > 0 && num > metadata.baseTI.length)
			{
				num = metadata.baseTI.length;
				goto IL_01B4;
			}
			goto IL_01B4;
			IL_00F4:
			if (this._defaultEncoding == null)
			{
				this.ThrowUnsupportedCollationEncountered(null);
			}
			string text = (isSqlType ? ((SqlString)value).Value : ((string)value));
			num = this._defaultEncoding.GetByteCount(text);
			if (metadata.baseTI.length > 0 && num > metadata.baseTI.length)
			{
				num = metadata.baseTI.length;
				goto IL_01B4;
			}
			goto IL_01B4;
			IL_0156:
			num = (isSqlType ? ((SqlString)value).Value.Length : ((string)value).Length) * 2;
			if (metadata.baseTI.length > 0 && num > metadata.baseTI.length)
			{
				num = metadata.baseTI.length;
				goto IL_01B4;
			}
			goto IL_01B4;
			IL_01A8:
			num = metadata.baseTI.length;
			IL_01B4:
			byte[] array;
			if (isSqlType)
			{
				array = this.SerializeUnencryptedSqlValue(value, metadata.baseTI.metaType, num, 0, metadata.cipherMD.NormalizationRuleVersion, stateObj);
			}
			else
			{
				array = this.SerializeUnencryptedValue(value, metadata.baseTI.metaType, metadata.baseTI.scale, num, 0, isDataFeed, metadata.cipherMD.NormalizationRuleVersion, stateObj);
			}
			return SqlSecurityUtility.EncryptWithKey(array, metadata.cipherMD, this._connHandler.Connection, null);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0005BDD4 File Offset: 0x00059FD4
		internal Task WriteBulkCopyValue(object value, SqlMetaDataPriv metadata, TdsParserStateObject stateObj, bool isSqlType, bool isDataFeed, bool isNull)
		{
			Encoding defaultEncoding = this._defaultEncoding;
			SqlCollation defaultCollation = this._defaultCollation;
			int defaultCodePage = this._defaultCodePage;
			int defaultLCID = this._defaultLCID;
			Task task = null;
			Task task2 = null;
			if (this.State != TdsParserState.OpenNotLoggedIn && this.State != TdsParserState.OpenLoggedIn)
			{
				throw ADP.ClosedConnectionError();
			}
			try
			{
				if (metadata.encoding != null)
				{
					this._defaultEncoding = metadata.encoding;
				}
				if (metadata.collation != null)
				{
					if (metadata.collation.IsUTF8)
					{
						this._defaultEncoding = Encoding.UTF8;
					}
					this._defaultCollation = metadata.collation;
					this._defaultLCID = this._defaultCollation.LCID;
				}
				this._defaultCodePage = metadata.codePage;
				MetaType metaType = metadata.metaType;
				int num = 0;
				int num2 = 0;
				if (isNull)
				{
					if (metaType.IsPlp && (metaType.NullableType != 240 || metaType.IsLong))
					{
						this.WriteLong(-1L, stateObj);
					}
					else if (!metaType.IsFixed && !metaType.IsLong && !metaType.IsVarTime)
					{
						this.WriteShort(65535, stateObj);
					}
					else
					{
						stateObj.WriteByte(0);
					}
					return task;
				}
				if (!isDataFeed)
				{
					byte nullableType = metaType.NullableType;
					if (nullableType <= 167)
					{
						if (nullableType <= 99)
						{
							switch (nullableType)
							{
							case 34:
								break;
							case 35:
								goto IL_01DF;
							case 36:
								num = 16;
								goto IL_029D;
							default:
								if (nullableType != 99)
								{
									goto IL_0295;
								}
								goto IL_022A;
							}
						}
						else if (nullableType != 165)
						{
							if (nullableType != 167)
							{
								goto IL_0295;
							}
							goto IL_01DF;
						}
					}
					else if (nullableType <= 175)
					{
						if (nullableType != 173)
						{
							if (nullableType != 175)
							{
								goto IL_0295;
							}
							goto IL_01DF;
						}
					}
					else
					{
						if (nullableType == 231)
						{
							goto IL_022A;
						}
						switch (nullableType)
						{
						case 239:
							goto IL_022A;
						case 240:
							break;
						case 241:
							if (value is XmlReader)
							{
								value = MetaType.GetStringFromXml((XmlReader)value);
							}
							num = (isSqlType ? ((SqlString)value).Value.Length : ((string)value).Length) * 2;
							goto IL_029D;
						default:
							goto IL_0295;
						}
					}
					num = (isSqlType ? ((SqlBinary)value).Length : ((byte[])value).Length);
					goto IL_029D;
					IL_01DF:
					if (this._defaultEncoding == null)
					{
						this.ThrowUnsupportedCollationEncountered(null);
					}
					string text;
					if (isSqlType)
					{
						text = ((SqlString)value).Value;
					}
					else
					{
						text = (string)value;
					}
					num = text.Length;
					num2 = this._defaultEncoding.GetByteCount(text);
					goto IL_029D;
					IL_022A:
					num = (isSqlType ? ((SqlString)value).Value.Length : ((string)value).Length) * 2;
					goto IL_029D;
					IL_0295:
					num = metadata.length;
				}
				IL_029D:
				if (metaType.IsLong)
				{
					SqlDbType sqlDbType = metaType.SqlDbType;
					if (sqlDbType <= SqlDbType.NVarChar)
					{
						if (sqlDbType != SqlDbType.Image && sqlDbType != SqlDbType.NText)
						{
							if (sqlDbType != SqlDbType.NVarChar)
							{
								goto IL_0341;
							}
							goto IL_031E;
						}
					}
					else if (sqlDbType <= SqlDbType.VarChar)
					{
						if (sqlDbType != SqlDbType.Text)
						{
							if (sqlDbType - SqlDbType.VarBinary > 1)
							{
								goto IL_0341;
							}
							goto IL_031E;
						}
					}
					else
					{
						if (sqlDbType != SqlDbType.Xml && sqlDbType != SqlDbType.Udt)
						{
							goto IL_0341;
						}
						goto IL_031E;
					}
					stateObj.WriteByteArray(TdsParser.s_longDataHeader, TdsParser.s_longDataHeader.Length, 0, true, null);
					this.WriteTokenLength(metadata.tdsType, (num2 == 0) ? num : num2, stateObj);
					goto IL_0341;
					IL_031E:
					this.WriteUnsignedLong(18446744073709551614UL, stateObj);
				}
				else
				{
					this.WriteTokenLength(metadata.tdsType, (num2 == 0) ? num : num2, stateObj);
				}
				IL_0341:
				if (isSqlType)
				{
					task2 = this.WriteSqlValue(value, metaType, num, num2, 0, stateObj);
				}
				else if (metaType.SqlDbType != SqlDbType.Udt || metaType.IsLong)
				{
					task2 = this.WriteValue(value, metaType, metadata.scale, num, num2, 0, stateObj, metadata.length, isDataFeed);
					if (task2 == null && this._asyncWrite)
					{
						task2 = stateObj.WaitForAccumulatedWrites();
					}
				}
				else
				{
					this.WriteShort(num, stateObj);
					task2 = stateObj.WriteByteArray((byte[])value, num, 0, true, null);
				}
				if (task2 != null)
				{
					task = this.WriteBulkCopyValueSetupContinuation(task2, defaultEncoding, defaultCollation, defaultCodePage, defaultLCID);
				}
			}
			finally
			{
				if (task2 == null)
				{
					this._defaultEncoding = defaultEncoding;
					this._defaultCollation = defaultCollation;
					this._defaultCodePage = defaultCodePage;
					this._defaultLCID = defaultLCID;
				}
			}
			return task;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0005C1F4 File Offset: 0x0005A3F4
		private Task WriteBulkCopyValueSetupContinuation(Task internalWriteTask, Encoding saveEncoding, SqlCollation saveCollation, int saveCodePage, int saveLCID)
		{
			return internalWriteTask.ContinueWith<Task>(delegate(Task t)
			{
				this._defaultEncoding = saveEncoding;
				this._defaultCollation = saveCollation;
				this._defaultCodePage = saveCodePage;
				this._defaultLCID = saveLCID;
				return t;
			}, TaskScheduler.Default).Unwrap();
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0005C248 File Offset: 0x0005A448
		private void WriteMarsHeaderData(TdsParserStateObject stateObj, SqlInternalTransaction transaction)
		{
			this.WriteShort(2, stateObj);
			if (transaction != null && transaction.TransactionId != 0L)
			{
				this.WriteLong(transaction.TransactionId, stateObj);
				this.WriteInt(stateObj.IncrementAndObtainOpenResultCount(transaction), stateObj);
				return;
			}
			this.WriteLong(this._retainedTransactionId, stateObj);
			this.WriteInt(stateObj.IncrementAndObtainOpenResultCount(null), stateObj);
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0005C2A0 File Offset: 0x0005A4A0
		private int GetNotificationHeaderSize(SqlNotificationRequest notificationRequest)
		{
			if (notificationRequest == null)
			{
				return 0;
			}
			string userData = notificationRequest.UserData;
			string options = notificationRequest.Options;
			int timeout = notificationRequest.Timeout;
			if (userData == null)
			{
				throw ADP.ArgumentNull("CallbackId");
			}
			if (65535 < userData.Length)
			{
				throw ADP.ArgumentOutOfRange("CallbackId");
			}
			if (options == null)
			{
				throw ADP.ArgumentNull("Service");
			}
			if (65535 < options.Length)
			{
				throw ADP.ArgumentOutOfRange("Service");
			}
			if (-1 > timeout)
			{
				throw ADP.ArgumentOutOfRange("Timeout");
			}
			int num = 8 + userData.Length * 2 + 2 + options.Length * 2;
			if (timeout > 0)
			{
				num += 4;
			}
			return num;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0005C344 File Offset: 0x0005A544
		private void WriteQueryNotificationHeaderData(SqlNotificationRequest notificationRequest, TdsParserStateObject stateObj)
		{
			string userData = notificationRequest.UserData;
			string options = notificationRequest.Options;
			int timeout = notificationRequest.Timeout;
			SqlClientEventSource.Log.TryNotificationTraceEvent<string, string, int>("<sc.TdsParser.WriteQueryNotificationHeader|DEP> NotificationRequest: userData: '{0}', options: '{1}', timeout: '{2}'", notificationRequest.UserData, notificationRequest.Options, notificationRequest.Timeout);
			this.WriteShort(1, stateObj);
			this.WriteShort(userData.Length * 2, stateObj);
			this.WriteString(userData, stateObj, true);
			this.WriteShort(options.Length * 2, stateObj);
			this.WriteString(options, stateObj, true);
			if (timeout > 0)
			{
				this.WriteInt(timeout, stateObj);
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0005C3D0 File Offset: 0x0005A5D0
		private void WriteTraceHeaderData(TdsParserStateObject stateObj)
		{
			ActivityCorrelator.ActivityId activityId = ActivityCorrelator.Current;
			this.WriteShort(3, stateObj);
			stateObj.WriteByteArray(activityId.Id.ToByteArray(), 16, 0, true, null);
			this.WriteUnsignedInt(activityId.Sequence, stateObj);
			SqlClientEventSource.Log.TryTraceEvent<ActivityCorrelator.ActivityId>("<sc.TdsParser.WriteTraceHeaderData|INFO> ActivityID {0}", activityId);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0005C424 File Offset: 0x0005A624
		private void WriteRPCBatchHeaders(TdsParserStateObject stateObj, SqlNotificationRequest notificationRequest)
		{
			int notificationHeaderSize = this.GetNotificationHeaderSize(notificationRequest);
			int num = (this.IncludeTraceHeader ? (22 + notificationHeaderSize + 26) : (22 + notificationHeaderSize));
			this.WriteInt(num, stateObj);
			this.WriteInt(18, stateObj);
			this.WriteMarsHeaderData(stateObj, this.CurrentTransaction);
			if (notificationHeaderSize != 0)
			{
				this.WriteInt(notificationHeaderSize, stateObj);
				this.WriteQueryNotificationHeaderData(notificationRequest, stateObj);
			}
			if (this.IncludeTraceHeader)
			{
				this.WriteInt(26, stateObj);
				this.WriteTraceHeaderData(stateObj);
			}
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0005C498 File Offset: 0x0005A698
		private void WriteTokenLength(byte token, int length, TdsParserStateObject stateObj)
		{
			int num = 0;
			if (this._is2005)
			{
				if (240 == token)
				{
					num = 8;
				}
				else if (token == 241)
				{
					num = 8;
				}
			}
			if (num == 0)
			{
				int num2 = (int)(token & 48);
				if (num2 <= 16)
				{
					if (num2 != 0)
					{
						if (num2 != 16)
						{
							goto IL_0065;
						}
						num = 0;
						goto IL_0065;
					}
				}
				else if (num2 != 32)
				{
					if (num2 == 48)
					{
						num = 0;
						goto IL_0065;
					}
					goto IL_0065;
				}
				if ((token & 128) != 0)
				{
					num = 2;
				}
				else if ((token & 12) == 0)
				{
					num = 4;
				}
				else
				{
					num = 1;
				}
				IL_0065:
				switch (num)
				{
				case 1:
					stateObj.WriteByte((byte)length);
					return;
				case 2:
					this.WriteShort(length, stateObj);
					return;
				case 3:
					break;
				case 4:
					this.WriteInt(length, stateObj);
					return;
				default:
					if (num != 8)
					{
						return;
					}
					this.WriteShort(65535, stateObj);
					break;
				}
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0005C550 File Offset: 0x0005A750
		private bool IsBOMNeeded(MetaType type, object value)
		{
			if (type.NullableType == 241)
			{
				Type type2 = value.GetType();
				if (type2 == typeof(SqlString))
				{
					if (!((SqlString)value).IsNull && ((SqlString)value).Value.Length > 0 && (((SqlString)value).Value[0] & 'ÿ') != 'ÿ')
					{
						return true;
					}
				}
				else if (type2 == typeof(string) && ((string)value).Length > 0)
				{
					if (value != null && (((string)value)[0] & 'ÿ') != 'ÿ')
					{
						return true;
					}
				}
				else if (type2 == typeof(SqlXml))
				{
					if (!((SqlXml)value).IsNull)
					{
						return true;
					}
				}
				else if (type2 == typeof(XmlDataFeed))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0005C649 File Offset: 0x0005A849
		private Task GetTerminationTask(Task unterminatedWriteTask, object value, MetaType type, int actualLength, TdsParserStateObject stateObj, bool isDataFeed)
		{
			if (!type.IsPlp || (actualLength <= 0 && !isDataFeed))
			{
				return unterminatedWriteTask;
			}
			if (unterminatedWriteTask == null)
			{
				this.WriteInt(0, stateObj);
				return null;
			}
			return AsyncHelper.CreateContinuationTask<int, TdsParserStateObject>(unterminatedWriteTask, new Action<int, TdsParserStateObject>(this.WriteInt), 0, stateObj, this._connHandler, null);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0005C689 File Offset: 0x0005A889
		private Task WriteSqlValue(object value, MetaType type, int actualLength, int codePageByteSize, int offset, TdsParserStateObject stateObj)
		{
			return this.GetTerminationTask(this.WriteUnterminatedSqlValue(value, type, actualLength, codePageByteSize, offset, stateObj), value, type, actualLength, stateObj, false);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0005C6A8 File Offset: 0x0005A8A8
		private Task WriteUnterminatedSqlValue(object value, MetaType type, int actualLength, int codePageByteSize, int offset, TdsParserStateObject stateObj)
		{
			byte nullableType = type.NullableType;
			if (nullableType <= 165)
			{
				if (nullableType <= 99)
				{
					switch (nullableType)
					{
					case 34:
						break;
					case 35:
						goto IL_024A;
					case 36:
					{
						byte[] array;
						if (value is Guid)
						{
							array = ((Guid)value).ToByteArray();
						}
						else
						{
							array = ((SqlGuid)value).ToByteArray();
						}
						stateObj.WriteByteArray(array, actualLength, 0, true, null);
						goto IL_03E1;
					}
					case 37:
						goto IL_03E1;
					case 38:
						if (type.FixedLength == 1)
						{
							stateObj.WriteByte(((SqlByte)value).Value);
							goto IL_03E1;
						}
						if (type.FixedLength == 2)
						{
							this.WriteShort((int)((SqlInt16)value).Value, stateObj);
							goto IL_03E1;
						}
						if (type.FixedLength == 4)
						{
							this.WriteInt(((SqlInt32)value).Value, stateObj);
							goto IL_03E1;
						}
						this.WriteLong(((SqlInt64)value).Value, stateObj);
						goto IL_03E1;
					default:
						if (nullableType != 99)
						{
							goto IL_03E1;
						}
						goto IL_02AD;
					}
				}
				else
				{
					switch (nullableType)
					{
					case 104:
						if (((SqlBoolean)value).Value)
						{
							stateObj.WriteByte(1);
							goto IL_03E1;
						}
						stateObj.WriteByte(0);
						goto IL_03E1;
					case 105:
					case 106:
					case 107:
						goto IL_03E1;
					case 108:
						this.WriteSqlDecimal((SqlDecimal)value, stateObj);
						goto IL_03E1;
					case 109:
						if (type.FixedLength == 4)
						{
							this.WriteFloat(((SqlSingle)value).Value, stateObj);
							goto IL_03E1;
						}
						this.WriteDouble(((SqlDouble)value).Value, stateObj);
						goto IL_03E1;
					case 110:
						this.WriteSqlMoney((SqlMoney)value, type.FixedLength, stateObj);
						goto IL_03E1;
					case 111:
					{
						SqlDateTime sqlDateTime = (SqlDateTime)value;
						if (type.FixedLength != 4)
						{
							this.WriteInt(sqlDateTime.DayTicks, stateObj);
							this.WriteInt(sqlDateTime.TimeTicks, stateObj);
							goto IL_03E1;
						}
						if (0 > sqlDateTime.DayTicks || sqlDateTime.DayTicks > 65535)
						{
							throw SQL.SmallDateTimeOverflow(sqlDateTime.ToString());
						}
						this.WriteShort(sqlDateTime.DayTicks, stateObj);
						this.WriteShort(sqlDateTime.TimeTicks / SqlDateTime.SQLTicksPerMinute, stateObj);
						goto IL_03E1;
					}
					default:
						if (nullableType != 165)
						{
							goto IL_03E1;
						}
						break;
					}
				}
			}
			else if (nullableType <= 173)
			{
				if (nullableType == 167)
				{
					goto IL_024A;
				}
				if (nullableType != 173)
				{
					goto IL_03E1;
				}
			}
			else
			{
				if (nullableType == 175)
				{
					goto IL_024A;
				}
				if (nullableType == 231)
				{
					goto IL_02AD;
				}
				switch (nullableType)
				{
				case 239:
				case 241:
					goto IL_02AD;
				case 240:
					throw SQL.UDTUnexpectedResult(value.GetType().AssemblyQualifiedName);
				default:
					goto IL_03E1;
				}
			}
			if (type.IsPlp)
			{
				this.WriteInt(actualLength, stateObj);
			}
			if (value is SqlBinary)
			{
				return stateObj.WriteByteArray(((SqlBinary)value).Value, actualLength, offset, false, null);
			}
			return stateObj.WriteByteArray(((SqlBytes)value).Value, actualLength, offset, false, null);
			IL_024A:
			if (type.IsPlp)
			{
				this.WriteInt(codePageByteSize, stateObj);
			}
			if (value is SqlChars)
			{
				string text = new string(((SqlChars)value).Value);
				return this.WriteEncodingChar(text, actualLength, offset, this._defaultEncoding, stateObj, false);
			}
			return this.WriteEncodingChar(((SqlString)value).Value, actualLength, offset, this._defaultEncoding, stateObj, false);
			IL_02AD:
			if (type.IsPlp)
			{
				if (this.IsBOMNeeded(type, value))
				{
					this.WriteInt(actualLength + 2, stateObj);
					this.WriteShort(65279, stateObj);
				}
				else
				{
					this.WriteInt(actualLength, stateObj);
				}
			}
			if (actualLength != 0)
			{
				actualLength >>= 1;
			}
			if (value is SqlChars)
			{
				return this.WriteCharArray(((SqlChars)value).Value, actualLength, offset, stateObj, false);
			}
			return this.WriteString(((SqlString)value).Value, actualLength, offset, stateObj, false);
			IL_03E1:
			return null;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0005CA98 File Offset: 0x0005AC98
		private async Task WriteXmlFeed(XmlDataFeed feed, TdsParserStateObject stateObj, bool needBom, Encoding encoding, int size)
		{
			byte[] array = null;
			if (!needBom)
			{
				array = encoding.GetPreamble();
			}
			TdsParser.ConstrainedTextWriter writer = new TdsParser.ConstrainedTextWriter(new StreamWriter(new TdsParser.TdsOutputStream(this, stateObj, array), encoding), size);
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.CloseOutput = false;
			xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
			if (this._asyncWrite)
			{
				xmlWriterSettings.Async = true;
			}
			XmlWriter ww = XmlWriter.Create(writer, xmlWriterSettings);
			if (feed._source.ReadState == ReadState.Initial)
			{
				feed._source.Read();
			}
			while (!feed._source.EOF && !writer.IsComplete)
			{
				if (feed._source.NodeType == XmlNodeType.XmlDeclaration)
				{
					feed._source.Read();
				}
				else if (this._asyncWrite)
				{
					await ww.WriteNodeAsync(feed._source, true).ConfigureAwait(false);
				}
				else
				{
					ww.WriteNode(feed._source, true);
				}
			}
			if (this._asyncWrite)
			{
				await ww.FlushAsync().ConfigureAwait(false);
			}
			else
			{
				ww.Flush();
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0005CB08 File Offset: 0x0005AD08
		private async Task WriteTextFeed(TextDataFeed feed, Encoding encoding, bool needBom, TdsParserStateObject stateObj, int size)
		{
			char[] inBuff = new char[4096];
			encoding = encoding ?? new UnicodeEncoding(false, false);
			TdsParser.ConstrainedTextWriter writer = new TdsParser.ConstrainedTextWriter(new StreamWriter(new TdsParser.TdsOutputStream(this, stateObj, null), encoding), size);
			if (needBom)
			{
				if (this._asyncWrite)
				{
					await writer.WriteAsync('\ufeff').ConfigureAwait(false);
				}
				else
				{
					writer.Write('\ufeff');
				}
			}
			int nWritten = 0;
			do
			{
				int nRead = 0;
				if (this._asyncWrite)
				{
					nRead = await feed._source.ReadBlockAsync(inBuff, 0, 4096).ConfigureAwait(false);
				}
				else
				{
					nRead = feed._source.ReadBlock(inBuff, 0, 4096);
				}
				if (nRead == 0)
				{
					break;
				}
				if (this._asyncWrite)
				{
					await writer.WriteAsync(inBuff, 0, nRead).ConfigureAwait(false);
				}
				else
				{
					writer.Write(inBuff, 0, nRead);
				}
				nWritten += nRead;
			}
			while (!writer.IsComplete);
			if (this._asyncWrite)
			{
				await writer.FlushAsync().ConfigureAwait(false);
			}
			else
			{
				writer.Flush();
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0005CB78 File Offset: 0x0005AD78
		private async Task WriteStreamFeed(StreamDataFeed feed, TdsParserStateObject stateObj, int len)
		{
			TdsParser.TdsOutputStream output = new TdsParser.TdsOutputStream(this, stateObj, null);
			byte[] buff = new byte[4096];
			int nWritten = 0;
			do
			{
				int nRead = 0;
				int num = 4096;
				if (len > 0 && nWritten + num > len)
				{
					num = len - nWritten;
				}
				if (this._asyncWrite)
				{
					int num2 = await feed._source.ReadAsync(buff, 0, num).ConfigureAwait(false);
					nRead = num2;
				}
				else
				{
					nRead = feed._source.Read(buff, 0, num);
				}
				if (nRead == 0)
				{
					break;
				}
				if (this._asyncWrite)
				{
					await output.WriteAsync(buff, 0, nRead).ConfigureAwait(false);
				}
				else
				{
					output.Write(buff, 0, nRead);
				}
				nWritten += nRead;
			}
			while (len <= 0 || nWritten < len);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0005CBD4 File Offset: 0x0005ADD4
		private Task NullIfCompletedWriteTask(Task task)
		{
			if (task == null)
			{
				return null;
			}
			switch (task.Status)
			{
			case TaskStatus.RanToCompletion:
				return null;
			case TaskStatus.Canceled:
				throw SQL.OperationCancelled();
			case TaskStatus.Faulted:
				throw task.Exception.InnerException;
			default:
				return task;
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0005CC18 File Offset: 0x0005AE18
		private Task WriteValue(object value, MetaType type, byte scale, int actualLength, int encodingByteSize, int offset, TdsParserStateObject stateObj, int paramSize, bool isDataFeed)
		{
			return this.GetTerminationTask(this.WriteUnterminatedValue(value, type, scale, actualLength, encodingByteSize, offset, stateObj, paramSize, isDataFeed), value, type, actualLength, stateObj, isDataFeed);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0005CC48 File Offset: 0x0005AE48
		private Task WriteUnterminatedValue(object value, MetaType type, byte scale, int actualLength, int encodingByteSize, int offset, TdsParserStateObject stateObj, int paramSize, bool isDataFeed)
		{
			byte nullableType = type.NullableType;
			if (nullableType <= 165)
			{
				if (nullableType <= 99)
				{
					switch (nullableType)
					{
					case 34:
						break;
					case 35:
						goto IL_01F8;
					case 36:
					{
						byte[] array = ((Guid)value).ToByteArray();
						stateObj.WriteByteArray(array, actualLength, 0, true, null);
						goto IL_0460;
					}
					case 37:
					case 39:
						goto IL_0460;
					case 38:
						if (type.FixedLength == 1)
						{
							stateObj.WriteByte((byte)value);
							goto IL_0460;
						}
						if (type.FixedLength == 2)
						{
							this.WriteShort((int)((short)value), stateObj);
							goto IL_0460;
						}
						if (type.FixedLength == 4)
						{
							this.WriteInt((int)value, stateObj);
							goto IL_0460;
						}
						this.WriteLong((long)value, stateObj);
						goto IL_0460;
					case 40:
						this.WriteDate((DateTime)value, stateObj);
						goto IL_0460;
					case 41:
						if (scale > 7)
						{
							throw SQL.TimeScaleValueOutOfRange(scale);
						}
						this.WriteTime((TimeSpan)value, scale, actualLength, stateObj);
						goto IL_0460;
					case 42:
						if (scale > 7)
						{
							throw SQL.TimeScaleValueOutOfRange(scale);
						}
						this.WriteDateTime2((DateTime)value, scale, actualLength, stateObj);
						goto IL_0460;
					case 43:
						this.WriteDateTimeOffset((DateTimeOffset)value, scale, actualLength, stateObj);
						goto IL_0460;
					default:
						if (nullableType != 99)
						{
							goto IL_0460;
						}
						goto IL_0287;
					}
				}
				else
				{
					switch (nullableType)
					{
					case 104:
						if ((bool)value)
						{
							stateObj.WriteByte(1);
							goto IL_0460;
						}
						stateObj.WriteByte(0);
						goto IL_0460;
					case 105:
					case 106:
					case 107:
						goto IL_0460;
					case 108:
						this.WriteDecimal((decimal)value, stateObj);
						goto IL_0460;
					case 109:
						if (type.FixedLength == 4)
						{
							this.WriteFloat((float)value, stateObj);
							goto IL_0460;
						}
						this.WriteDouble((double)value, stateObj);
						goto IL_0460;
					case 110:
						this.WriteCurrency((decimal)value, type.FixedLength, stateObj);
						goto IL_0460;
					case 111:
					{
						TdsDateTime tdsDateTime = MetaType.FromDateTime((DateTime)value, (byte)type.FixedLength);
						if (type.FixedLength != 4)
						{
							this.WriteInt(tdsDateTime.days, stateObj);
							this.WriteInt(tdsDateTime.time, stateObj);
							goto IL_0460;
						}
						if (0 > tdsDateTime.days || tdsDateTime.days > 65535)
						{
							throw SQL.SmallDateTimeOverflow(MetaType.ToDateTime(tdsDateTime.days, tdsDateTime.time, 4).ToString(CultureInfo.InvariantCulture));
						}
						this.WriteShort(tdsDateTime.days, stateObj);
						this.WriteShort(tdsDateTime.time, stateObj);
						goto IL_0460;
					}
					default:
						if (nullableType != 165)
						{
							goto IL_0460;
						}
						break;
					}
				}
			}
			else if (nullableType <= 173)
			{
				if (nullableType == 167)
				{
					goto IL_01F8;
				}
				if (nullableType != 173)
				{
					goto IL_0460;
				}
			}
			else
			{
				if (nullableType == 175)
				{
					goto IL_01F8;
				}
				if (nullableType == 231)
				{
					goto IL_0287;
				}
				switch (nullableType)
				{
				case 239:
				case 241:
					goto IL_0287;
				case 240:
					break;
				default:
					goto IL_0460;
				}
			}
			if (isDataFeed)
			{
				return this.NullIfCompletedWriteTask(this.WriteStreamFeed((StreamDataFeed)value, stateObj, paramSize));
			}
			if (type.IsPlp)
			{
				this.WriteInt(actualLength, stateObj);
			}
			return stateObj.WriteByteArray((byte[])value, actualLength, offset, false, null);
			IL_01F8:
			if (isDataFeed)
			{
				TextDataFeed textDataFeed = value as TextDataFeed;
				if (textDataFeed == null)
				{
					return this.NullIfCompletedWriteTask(this.WriteXmlFeed((XmlDataFeed)value, stateObj, true, this._defaultEncoding, paramSize));
				}
				return this.NullIfCompletedWriteTask(this.WriteTextFeed(textDataFeed, this._defaultEncoding, false, stateObj, paramSize));
			}
			else
			{
				if (type.IsPlp)
				{
					this.WriteInt(encodingByteSize, stateObj);
				}
				if (value is byte[])
				{
					return stateObj.WriteByteArray((byte[])value, actualLength, 0, false, null);
				}
				return this.WriteEncodingChar((string)value, actualLength, offset, this._defaultEncoding, stateObj, false);
			}
			IL_0287:
			if (isDataFeed)
			{
				TextDataFeed textDataFeed2 = value as TextDataFeed;
				if (textDataFeed2 == null)
				{
					return this.NullIfCompletedWriteTask(this.WriteXmlFeed((XmlDataFeed)value, stateObj, this.IsBOMNeeded(type, value), Encoding.Unicode, paramSize));
				}
				return this.NullIfCompletedWriteTask(this.WriteTextFeed(textDataFeed2, null, this.IsBOMNeeded(type, value), stateObj, paramSize));
			}
			else
			{
				if (type.IsPlp)
				{
					if (this.IsBOMNeeded(type, value))
					{
						this.WriteInt(actualLength + 2, stateObj);
						this.WriteShort(65279, stateObj);
					}
					else
					{
						this.WriteInt(actualLength, stateObj);
					}
				}
				if (value is byte[])
				{
					return stateObj.WriteByteArray((byte[])value, actualLength, 0, false, null);
				}
				actualLength >>= 1;
				return this.WriteString((string)value, actualLength, offset, stateObj, false);
			}
			IL_0460:
			return null;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0005D0B6 File Offset: 0x0005B2B6
		private Task WriteEncryptionMetadata(Task terminatedWriteTask, SqlColumnEncryptionInputParameterInfo columnEncryptionParameterInfo, TdsParserStateObject stateObj)
		{
			if (terminatedWriteTask == null)
			{
				this.WriteEncryptionMetadata(columnEncryptionParameterInfo, stateObj);
				return null;
			}
			return AsyncHelper.CreateContinuationTask<SqlColumnEncryptionInputParameterInfo, TdsParserStateObject>(terminatedWriteTask, new Action<SqlColumnEncryptionInputParameterInfo, TdsParserStateObject>(this.WriteEncryptionMetadata), columnEncryptionParameterInfo, stateObj, this._connHandler, null);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0005D0E0 File Offset: 0x0005B2E0
		private void WriteEncryptionMetadata(SqlColumnEncryptionInputParameterInfo columnEncryptionParameterInfo, TdsParserStateObject stateObj)
		{
			this.WriteSmiTypeInfo(columnEncryptionParameterInfo.ParameterMetadata, stateObj);
			stateObj.WriteByteArray(columnEncryptionParameterInfo.SerializedWireFormat, columnEncryptionParameterInfo.SerializedWireFormat.Length, 0, true, null);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0005D108 File Offset: 0x0005B308
		private byte[] SerializeUnencryptedValue(object value, MetaType type, byte scale, int actualLength, int offset, bool isDataFeed, byte normalizationVersion, TdsParserStateObject stateObj)
		{
			if (normalizationVersion != 1)
			{
				throw SQL.UnsupportedNormalizationVersion(normalizationVersion);
			}
			byte nullableType = type.NullableType;
			if (nullableType <= 165)
			{
				if (nullableType <= 99)
				{
					switch (nullableType)
					{
					case 34:
						break;
					case 35:
						goto IL_01A6;
					case 36:
						return ((Guid)value).ToByteArray();
					case 37:
					case 39:
						goto IL_03C5;
					case 38:
						if (type.FixedLength == 1)
						{
							return this.SerializeLong((long)((ulong)((byte)value)), stateObj);
						}
						if (type.FixedLength == 2)
						{
							return this.SerializeLong((long)((short)value), stateObj);
						}
						if (type.FixedLength == 4)
						{
							return this.SerializeLong((long)((int)value), stateObj);
						}
						return this.SerializeLong((long)value, stateObj);
					case 40:
						return this.SerializeDate((DateTime)value);
					case 41:
						if (scale > 7)
						{
							throw SQL.TimeScaleValueOutOfRange(scale);
						}
						return this.SerializeTime((TimeSpan)value, scale, actualLength);
					case 42:
						if (scale > 7)
						{
							throw SQL.TimeScaleValueOutOfRange(scale);
						}
						return this.SerializeDateTime2((DateTime)value, scale, actualLength);
					case 43:
						if (scale > 7)
						{
							throw SQL.TimeScaleValueOutOfRange(scale);
						}
						return this.SerializeDateTimeOffset((DateTimeOffset)value, scale, actualLength);
					default:
						if (nullableType != 99)
						{
							goto IL_03C5;
						}
						goto IL_01E2;
					}
				}
				else
				{
					switch (nullableType)
					{
					case 104:
						return this.SerializeLong(((bool)value > false) ? 1L : 0L, stateObj);
					case 105:
					case 106:
					case 107:
						goto IL_03C5;
					case 108:
						return this.SerializeDecimal((decimal)value, stateObj);
					case 109:
						if (type.FixedLength == 4)
						{
							return this.SerializeFloat((float)value);
						}
						return this.SerializeDouble((double)value);
					case 110:
						return this.SerializeCurrency((decimal)value, type.FixedLength, stateObj);
					case 111:
					{
						TdsDateTime tdsDateTime = MetaType.FromDateTime((DateTime)value, (byte)type.FixedLength);
						if (type.FixedLength != 4)
						{
							if (stateObj._bLongBytes == null)
							{
								stateObj._bLongBytes = new byte[8];
							}
							byte[] bLongBytes = stateObj._bLongBytes;
							int num = 0;
							byte[] array = this.SerializeInt(tdsDateTime.days, stateObj);
							Buffer.BlockCopy(array, 0, bLongBytes, num, 4);
							num += 4;
							array = this.SerializeInt(tdsDateTime.time, stateObj);
							Buffer.BlockCopy(array, 0, bLongBytes, num, 4);
							return bLongBytes;
						}
						if (0 > tdsDateTime.days || tdsDateTime.days > 65535)
						{
							throw SQL.SmallDateTimeOverflow(MetaType.ToDateTime(tdsDateTime.days, tdsDateTime.time, 4).ToString(CultureInfo.InvariantCulture));
						}
						if (stateObj._bIntBytes == null)
						{
							stateObj._bIntBytes = new byte[4];
						}
						byte[] bIntBytes = stateObj._bIntBytes;
						int num2 = 0;
						byte[] array2 = this.SerializeShort(tdsDateTime.days, stateObj);
						Buffer.BlockCopy(array2, 0, bIntBytes, num2, 2);
						num2 += 2;
						array2 = this.SerializeShort(tdsDateTime.time, stateObj);
						Buffer.BlockCopy(array2, 0, bIntBytes, num2, 2);
						return bIntBytes;
					}
					default:
						if (nullableType != 165)
						{
							goto IL_03C5;
						}
						break;
					}
				}
			}
			else if (nullableType <= 173)
			{
				if (nullableType == 167)
				{
					goto IL_01A6;
				}
				if (nullableType != 173)
				{
					goto IL_03C5;
				}
			}
			else
			{
				if (nullableType == 175)
				{
					goto IL_01A6;
				}
				if (nullableType == 231)
				{
					goto IL_01E2;
				}
				switch (nullableType)
				{
				case 239:
				case 241:
					goto IL_01E2;
				case 240:
					break;
				default:
					goto IL_03C5;
				}
			}
			byte[] array3 = new byte[actualLength];
			Buffer.BlockCopy((byte[])value, offset, array3, 0, actualLength);
			return array3;
			IL_01A6:
			if (value is byte[])
			{
				byte[] array4 = new byte[actualLength];
				Buffer.BlockCopy((byte[])value, 0, array4, 0, actualLength);
				return array4;
			}
			return this.SerializeEncodingChar((string)value, actualLength, offset, this._defaultEncoding);
			IL_01E2:
			if (value is byte[])
			{
				byte[] array5 = new byte[actualLength];
				Buffer.BlockCopy((byte[])value, 0, array5, 0, actualLength);
				return array5;
			}
			actualLength >>= 1;
			return this.SerializeString((string)value, actualLength, offset);
			IL_03C5:
			throw SQL.UnsupportedDatatypeEncryption(type.TypeName);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0005D4E8 File Offset: 0x0005B6E8
		private byte[] SerializeUnencryptedSqlValue(object value, MetaType type, int actualLength, int offset, byte normalizationVersion, TdsParserStateObject stateObj)
		{
			if (normalizationVersion != 1)
			{
				throw SQL.UnsupportedNormalizationVersion(normalizationVersion);
			}
			byte nullableType = type.NullableType;
			if (nullableType <= 167)
			{
				if (nullableType <= 99)
				{
					switch (nullableType)
					{
					case 34:
						break;
					case 35:
						goto IL_01FB;
					case 36:
						return ((SqlGuid)value).ToByteArray();
					case 37:
						goto IL_03BA;
					case 38:
						if (type.FixedLength == 1)
						{
							return this.SerializeLong((long)((ulong)((SqlByte)value).Value), stateObj);
						}
						if (type.FixedLength == 2)
						{
							return this.SerializeLong((long)((SqlInt16)value).Value, stateObj);
						}
						if (type.FixedLength == 4)
						{
							return this.SerializeLong((long)((SqlInt32)value).Value, stateObj);
						}
						return this.SerializeLong(((SqlInt64)value).Value, stateObj);
					default:
						if (nullableType != 99)
						{
							goto IL_03BA;
						}
						goto IL_0246;
					}
				}
				else
				{
					switch (nullableType)
					{
					case 104:
						return this.SerializeLong((((SqlBoolean)value).Value > false) ? 1L : 0L, stateObj);
					case 105:
					case 106:
					case 107:
						goto IL_03BA;
					case 108:
						return this.SerializeSqlDecimal((SqlDecimal)value, stateObj);
					case 109:
						if (type.FixedLength == 4)
						{
							return this.SerializeFloat(((SqlSingle)value).Value);
						}
						return this.SerializeDouble(((SqlDouble)value).Value);
					case 110:
						return this.SerializeSqlMoney((SqlMoney)value, type.FixedLength, stateObj);
					case 111:
					{
						SqlDateTime sqlDateTime = (SqlDateTime)value;
						if (type.FixedLength != 4)
						{
							if (stateObj._bLongBytes == null)
							{
								stateObj._bLongBytes = new byte[8];
							}
							byte[] bLongBytes = stateObj._bLongBytes;
							int num = 0;
							byte[] array = this.SerializeInt(sqlDateTime.DayTicks, stateObj);
							Buffer.BlockCopy(array, 0, bLongBytes, num, 4);
							num += 4;
							array = this.SerializeInt(sqlDateTime.TimeTicks, stateObj);
							Buffer.BlockCopy(array, 0, bLongBytes, num, 4);
							return bLongBytes;
						}
						if (0 > sqlDateTime.DayTicks || sqlDateTime.DayTicks > 65535)
						{
							throw SQL.SmallDateTimeOverflow(sqlDateTime.ToString());
						}
						if (stateObj._bIntBytes == null)
						{
							stateObj._bIntBytes = new byte[4];
						}
						byte[] bIntBytes = stateObj._bIntBytes;
						int num2 = 0;
						byte[] array2 = this.SerializeShort(sqlDateTime.DayTicks, stateObj);
						Buffer.BlockCopy(array2, 0, bIntBytes, num2, 2);
						num2 += 2;
						array2 = this.SerializeShort(sqlDateTime.TimeTicks / SqlDateTime.SQLTicksPerMinute, stateObj);
						Buffer.BlockCopy(array2, 0, bIntBytes, num2, 2);
						return bIntBytes;
					}
					default:
						if (nullableType != 165)
						{
							if (nullableType != 167)
							{
								goto IL_03BA;
							}
							goto IL_01FB;
						}
						break;
					}
				}
			}
			else if (nullableType <= 175)
			{
				if (nullableType != 173)
				{
					if (nullableType != 175)
					{
						goto IL_03BA;
					}
					goto IL_01FB;
				}
			}
			else
			{
				if (nullableType != 231 && nullableType != 239 && nullableType != 241)
				{
					goto IL_03BA;
				}
				goto IL_0246;
			}
			byte[] array3 = new byte[actualLength];
			if (value is SqlBinary)
			{
				Buffer.BlockCopy(((SqlBinary)value).Value, offset, array3, 0, actualLength);
			}
			else
			{
				Buffer.BlockCopy(((SqlBytes)value).Value, offset, array3, 0, actualLength);
			}
			return array3;
			IL_01FB:
			if (value is SqlChars)
			{
				string text = new string(((SqlChars)value).Value);
				return this.SerializeEncodingChar(text, actualLength, offset, this._defaultEncoding);
			}
			return this.SerializeEncodingChar(((SqlString)value).Value, actualLength, offset, this._defaultEncoding);
			IL_0246:
			if (actualLength != 0)
			{
				actualLength >>= 1;
			}
			if (value is SqlChars)
			{
				return this.SerializeCharArray(((SqlChars)value).Value, actualLength, offset);
			}
			return this.SerializeString(((SqlString)value).Value, actualLength, offset);
			IL_03BA:
			throw SQL.UnsupportedDatatypeEncryption(type.TypeName);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0005D8BC File Offset: 0x0005BABC
		internal void WriteParameterVarLen(MetaType type, int size, bool isNull, TdsParserStateObject stateObj, bool unknownLength = false)
		{
			if (type.IsLong)
			{
				if (isNull)
				{
					if (type.IsPlp)
					{
						this.WriteLong(-1L, stateObj);
						return;
					}
					this.WriteInt(-1, stateObj);
					return;
				}
				else
				{
					if (type.NullableType == 241 || unknownLength)
					{
						this.WriteUnsignedLong(18446744073709551614UL, stateObj);
						return;
					}
					if (type.IsPlp)
					{
						this.WriteLong((long)size, stateObj);
						return;
					}
					this.WriteInt(size, stateObj);
					return;
				}
			}
			else if (type.IsVarTime)
			{
				if (isNull)
				{
					stateObj.WriteByte(0);
					return;
				}
				stateObj.WriteByte((byte)size);
				return;
			}
			else if (!type.IsFixed)
			{
				if (isNull)
				{
					this.WriteShort(65535, stateObj);
					return;
				}
				this.WriteShort(size, stateObj);
				return;
			}
			else
			{
				if (isNull)
				{
					stateObj.WriteByte(0);
					return;
				}
				stateObj.WriteByte((byte)(type.FixedLength & 255));
				return;
			}
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0005D990 File Offset: 0x0005BB90
		private bool TryReadPlpUnicodeCharsChunk(char[] buff, int offst, int len, TdsParserStateObject stateObj, out int charsRead)
		{
			if (stateObj._longlenleft == 0UL)
			{
				charsRead = 0;
				return true;
			}
			charsRead = len;
			if (stateObj._longlenleft >> 1 < (ulong)((long)len))
			{
				charsRead = (int)(stateObj._longlenleft >> 1);
			}
			for (int i = 0; i < charsRead; i++)
			{
				if (!stateObj.TryReadChar(out buff[offst + i]))
				{
					return false;
				}
			}
			stateObj._longlenleft -= (ulong)((ulong)((long)charsRead) << 1);
			return true;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0005DA04 File Offset: 0x0005BC04
		internal int ReadPlpUnicodeChars(ref char[] buff, int offst, int len, TdsParserStateObject stateObj)
		{
			int num;
			if (!this.TryReadPlpUnicodeChars(ref buff, offst, len, stateObj, out num))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return num;
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0005DA2C File Offset: 0x0005BC2C
		internal bool TryReadPlpUnicodeChars(ref char[] buff, int offst, int len, TdsParserStateObject stateObj, out int totalCharsRead)
		{
			int num = 0;
			if (stateObj._longlen == 0UL)
			{
				totalCharsRead = 0;
				return true;
			}
			int i = len;
			if (buff == null && stateObj._longlen != 18446744073709551614UL)
			{
				buff = new char[Math.Min((int)stateObj._longlen, len)];
			}
			if (stateObj._longlenleft == 0UL)
			{
				ulong num2;
				if (!stateObj.TryReadPlpLength(false, out num2))
				{
					totalCharsRead = 0;
					return false;
				}
				if (stateObj._longlenleft == 0UL)
				{
					totalCharsRead = 0;
					return true;
				}
			}
			totalCharsRead = 0;
			while (i > 0)
			{
				num = (int)Math.Min(stateObj._longlenleft + 1UL >> 1, (ulong)((long)i));
				if (buff == null || buff.Length < offst + num)
				{
					char[] array = new char[offst + num];
					if (buff != null)
					{
						Buffer.BlockCopy(buff, 0, array, 0, offst * 2);
					}
					buff = array;
				}
				if (num > 0)
				{
					if (!this.TryReadPlpUnicodeCharsChunk(buff, offst, num, stateObj, out num))
					{
						return false;
					}
					i -= num;
					offst += num;
					totalCharsRead += num;
				}
				if (stateObj._longlenleft == 1UL && i > 0)
				{
					byte b;
					if (!stateObj.TryReadByte(out b))
					{
						return false;
					}
					stateObj._longlenleft -= 1UL;
					ulong num3;
					if (!stateObj.TryReadPlpLength(false, out num3))
					{
						return false;
					}
					byte b2;
					if (!stateObj.TryReadByte(out b2))
					{
						return false;
					}
					stateObj._longlenleft -= 1UL;
					buff[offst] = (char)(((int)(b2 & byte.MaxValue) << 8) + (int)(b & byte.MaxValue));
					checked
					{
						offst++;
					}
					num++;
					i--;
					totalCharsRead++;
				}
				ulong num4;
				if (stateObj._longlenleft == 0UL && !stateObj.TryReadPlpLength(false, out num4))
				{
					return false;
				}
				if (stateObj._longlenleft == 0UL)
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0005DBB8 File Offset: 0x0005BDB8
		internal int ReadPlpAnsiChars(ref char[] buff, int offst, int len, SqlMetaDataPriv metadata, TdsParserStateObject stateObj)
		{
			int num = 0;
			if (stateObj._longlen == 0UL)
			{
				return 0;
			}
			int i = len;
			if (stateObj._longlenleft == 0UL)
			{
				stateObj.ReadPlpLength(false);
				if (stateObj._longlenleft == 0UL)
				{
					stateObj._plpdecoder = null;
					return 0;
				}
			}
			if (stateObj._plpdecoder == null)
			{
				Encoding encoding = metadata.encoding;
				if (encoding == null)
				{
					if (this._defaultEncoding == null)
					{
						this.ThrowUnsupportedCollationEncountered(stateObj);
					}
					encoding = this._defaultEncoding;
				}
				stateObj._plpdecoder = encoding.GetDecoder();
			}
			while (i > 0)
			{
				int num2 = (int)Math.Min(stateObj._longlenleft, (ulong)((long)i));
				if (stateObj._bTmp == null || stateObj._bTmp.Length < num2)
				{
					stateObj._bTmp = new byte[num2];
				}
				num2 = stateObj.ReadPlpBytesChunk(stateObj._bTmp, 0, num2);
				int chars = stateObj._plpdecoder.GetChars(stateObj._bTmp, 0, num2, buff, offst);
				i -= chars;
				offst += chars;
				num += chars;
				if (stateObj._longlenleft == 0UL)
				{
					stateObj.ReadPlpLength(false);
				}
				if (stateObj._longlenleft == 0UL)
				{
					stateObj._plpdecoder = null;
					break;
				}
			}
			return num;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0005DCDC File Offset: 0x0005BEDC
		internal ulong SkipPlpValue(ulong cb, TdsParserStateObject stateObj)
		{
			ulong num;
			if (!this.TrySkipPlpValue(cb, stateObj, out num))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return num;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0005DD00 File Offset: 0x0005BF00
		internal bool TrySkipPlpValue(ulong cb, TdsParserStateObject stateObj, out ulong totalBytesSkipped)
		{
			totalBytesSkipped = 0UL;
			ulong num;
			if (stateObj._longlenleft == 0UL && !stateObj.TryReadPlpLength(false, out num))
			{
				return false;
			}
			while (totalBytesSkipped < cb && stateObj._longlenleft > 0UL)
			{
				int num2;
				if (stateObj._longlenleft > 2147483647UL)
				{
					num2 = int.MaxValue;
				}
				else
				{
					num2 = (int)stateObj._longlenleft;
				}
				num2 = ((cb - totalBytesSkipped < (ulong)((long)num2)) ? ((int)(cb - totalBytesSkipped)) : num2);
				if (!stateObj.TrySkipBytes(num2))
				{
					return false;
				}
				stateObj._longlenleft -= (ulong)((long)num2);
				totalBytesSkipped += (ulong)((long)num2);
				ulong num3;
				if (stateObj._longlenleft == 0UL && !stateObj.TryReadPlpLength(false, out num3))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0005DD9B File Offset: 0x0005BF9B
		internal ulong PlpBytesLeft(TdsParserStateObject stateObj)
		{
			if (stateObj._longlen != 0UL && stateObj._longlenleft == 0UL)
			{
				stateObj.ReadPlpLength(false);
			}
			return stateObj._longlenleft;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0005DDBB File Offset: 0x0005BFBB
		internal bool TryPlpBytesLeft(TdsParserStateObject stateObj, out ulong left)
		{
			if (stateObj._longlen != 0UL && stateObj._longlenleft == 0UL && !stateObj.TryReadPlpLength(false, out left))
			{
				return false;
			}
			left = stateObj._longlenleft;
			return true;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0005DDE2 File Offset: 0x0005BFE2
		internal ulong PlpBytesTotalLength(TdsParserStateObject stateObj)
		{
			if (stateObj._longlen == 18446744073709551614UL)
			{
				return ulong.MaxValue;
			}
			if (stateObj._longlen == 18446744073709551615UL)
			{
				return 0UL;
			}
			return stateObj._longlen;
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0005DE08 File Offset: 0x0005C008
		internal string TraceString()
		{
			return string.Format(null, "\n\t         _physicalStateObj = {0}\n\t         _pMarsPhysicalConObj = {1}\n\t         _state = {2}\n\t         _server = {3}\n\t         _fResetConnection = {4}\n\t         _defaultCollation = {5}\n\t         _defaultCodePage = {6}\n\t         _defaultLCID = {7}\n\t         _defaultEncoding = {8}\n\t         _encryptionOption = {9}\n\t         _currentTransaction = {10}\n\t         _pendingTransaction = {11}\n\t         _retainedTransactionId = {12}\n\t         _nonTransactedOpenResultCount = {13}\n\t         _connHandler = {14}\n\t         _fMARS = {15}\n\t         _sessionPool = {16}\n\t         _is2000 = {17}\n\t         _is2000SP1 = {18}\n\t         _is2005 = {19}\n\t         _sniSpnBuffer = {20}\n\t         _errors = {21}\n\t         _warnings = {22}\n\t         _attentionErrors = {23}\n\t         _attentionWarnings = {24}\n\t         _statistics = {25}\n\t         _statisticsIsInTransaction = {26}\n\t         _fPreserveTransaction = {27}         _fParallel = {28}", new object[]
			{
				(this._physicalStateObj == null) ? "(null)" : this._physicalStateObj.ObjectID.ToString(null),
				(this._pMarsPhysicalConObj == null) ? "(null)" : this._pMarsPhysicalConObj.ObjectID.ToString(null),
				this._state,
				this._server,
				this._fResetConnection ? bool.TrueString : bool.FalseString,
				(this._defaultCollation == null) ? "(null)" : this._defaultCollation.TraceString(),
				this._defaultCodePage,
				this._defaultLCID,
				this.TraceObjectClass(this._defaultEncoding),
				this._encryptionOption,
				(this._currentTransaction == null) ? "(null)" : this._currentTransaction.TraceString(),
				(this._pendingTransaction == null) ? "(null)" : this._pendingTransaction.TraceString(),
				this._retainedTransactionId,
				this._nonTransactedOpenResultCount,
				(this._connHandler == null) ? "(null)" : this._connHandler.ObjectID.ToString(null),
				this._fMARS ? bool.TrueString : bool.FalseString,
				(this._sessionPool == null) ? "(null)" : this._sessionPool.TraceString(),
				this._is2000 ? bool.TrueString : bool.FalseString,
				this._is2000SP1 ? bool.TrueString : bool.FalseString,
				this._is2005 ? bool.TrueString : bool.FalseString,
				(this._sniSpnBuffer == null) ? "(null)" : this._sniSpnBuffer.Length.ToString(null),
				(this._physicalStateObj != null) ? "(null)" : this._physicalStateObj.ErrorCount.ToString(null),
				(this._physicalStateObj != null) ? "(null)" : this._physicalStateObj.WarningCount.ToString(null),
				(this._physicalStateObj != null) ? "(null)" : this._physicalStateObj.PreAttentionErrorCount.ToString(null),
				(this._physicalStateObj != null) ? "(null)" : this._physicalStateObj.PreAttentionWarningCount.ToString(null),
				(this._statistics == null) ? bool.TrueString : bool.FalseString,
				this._statisticsIsInTransaction ? bool.TrueString : bool.FalseString,
				this._fPreserveTransaction ? bool.TrueString : bool.FalseString,
				(this._connHandler == null) ? "(null)" : this._connHandler.ConnectionOptions.MultiSubnetFailover.ToString(null),
				(this._connHandler == null) ? "(null)" : this._connHandler.ConnectionOptions.TransparentNetworkIPResolution.ToString(null)
			});
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0005E15B File Offset: 0x0005C35B
		private string TraceObjectClass(object instance)
		{
			if (instance == null)
			{
				return "(null)";
			}
			return instance.GetType().ToString();
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0005E174 File Offset: 0x0005C374
		private static IntPtr ClientCertificateDelegate(IntPtr ptrContext)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(ptrContext);
			IntPtr intPtr;
			try
			{
				ClientCertificateRetrievalCallback clientCertificateRetrievalCallback = (ClientCertificateRetrievalCallback)gchandle.Target;
				X509Certificate2 x509Certificate = clientCertificateRetrievalCallback();
				if (x509Certificate != null)
				{
					intPtr = x509Certificate.Handle;
				}
				else
				{
					intPtr = IntPtr.Zero;
				}
			}
			catch
			{
				intPtr = IntPtr.Zero;
			}
			return intPtr;
		}

		// Token: 0x040007EC RID: 2028
		private static int _objectTypeCount;

		// Token: 0x040007ED RID: 2029
		private readonly SqlClientLogger _logger = new SqlClientLogger();

		// Token: 0x040007EE RID: 2030
		internal readonly int _objectID = Interlocked.Increment(ref TdsParser._objectTypeCount);

		// Token: 0x040007EF RID: 2031
		private static Task completedTask;

		// Token: 0x040007F0 RID: 2032
		internal TdsParserStateObject _physicalStateObj;

		// Token: 0x040007F1 RID: 2033
		internal TdsParserStateObject _pMarsPhysicalConObj;

		// Token: 0x040007F2 RID: 2034
		private const int constBinBufferSize = 4096;

		// Token: 0x040007F3 RID: 2035
		private const int constTextBufferSize = 4096;

		// Token: 0x040007F4 RID: 2036
		private const string enableTruncateSwitch = "Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal";

		// Token: 0x040007F5 RID: 2037
		internal TdsParserState _state;

		// Token: 0x040007F6 RID: 2038
		private string _server = "";

		// Token: 0x040007F7 RID: 2039
		internal volatile bool _fResetConnection;

		// Token: 0x040007F8 RID: 2040
		internal volatile bool _fPreserveTransaction;

		// Token: 0x040007F9 RID: 2041
		private SqlCollation _defaultCollation;

		// Token: 0x040007FA RID: 2042
		private int _defaultCodePage;

		// Token: 0x040007FB RID: 2043
		private int _defaultLCID;

		// Token: 0x040007FC RID: 2044
		internal Encoding _defaultEncoding;

		// Token: 0x040007FD RID: 2045
		private static EncryptionOptions _sniSupportedEncryptionOption = SNILoadHandle.SingletonInstance.Options;

		// Token: 0x040007FE RID: 2046
		private static SNINativeMethodWrapper.SqlClientCertificateDelegate _clientCertificateCallback = new SNINativeMethodWrapper.SqlClientCertificateDelegate(TdsParser.ClientCertificateDelegate);

		// Token: 0x040007FF RID: 2047
		private EncryptionOptions _encryptionOption = TdsParser._sniSupportedEncryptionOption;

		// Token: 0x04000800 RID: 2048
		private SqlInternalTransaction _currentTransaction;

		// Token: 0x04000801 RID: 2049
		private SqlInternalTransaction _pendingTransaction;

		// Token: 0x04000802 RID: 2050
		private long _retainedTransactionId;

		// Token: 0x04000803 RID: 2051
		private int _nonTransactedOpenResultCount;

		// Token: 0x04000804 RID: 2052
		private SqlInternalConnectionTds _connHandler;

		// Token: 0x04000805 RID: 2053
		private bool _fMARS;

		// Token: 0x04000806 RID: 2054
		internal bool _loginWithFailover;

		// Token: 0x04000807 RID: 2055
		internal AutoResetEvent _resetConnectionEvent;

		// Token: 0x04000808 RID: 2056
		internal TdsParserSessionPool _sessionPool;

		// Token: 0x04000809 RID: 2057
		private bool _is2000;

		// Token: 0x0400080A RID: 2058
		private bool _is2000SP1;

		// Token: 0x0400080B RID: 2059
		private bool _is2005;

		// Token: 0x0400080C RID: 2060
		private bool _is2008;

		// Token: 0x0400080D RID: 2061
		private bool _is2012;

		// Token: 0x0400080E RID: 2062
		private bool _is2022;

		// Token: 0x0400080F RID: 2063
		private byte[] _sniSpnBuffer;

		// Token: 0x04000810 RID: 2064
		private SqlStatistics _statistics;

		// Token: 0x04000811 RID: 2065
		private bool _statisticsIsInTransaction;

		// Token: 0x04000812 RID: 2066
		private static byte[] s_nicAddress;

		// Token: 0x04000813 RID: 2067
		private static bool s_fSSPILoaded = false;

		// Token: 0x04000814 RID: 2068
		private static volatile uint s_maxSSPILength = 0U;

		// Token: 0x04000815 RID: 2069
		private static readonly byte[] s_longDataHeader = new byte[]
		{
			16, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue
		};

		// Token: 0x04000816 RID: 2070
		private static object s_tdsParserLock = new object();

		// Token: 0x04000817 RID: 2071
		private const int ATTENTION_TIMEOUT = 5000;

		// Token: 0x04000818 RID: 2072
		private static readonly byte[] s_xmlMetadataSubstituteSequence = new byte[] { 231, byte.MaxValue, byte.MaxValue, 0, 0, 0, 0, 0 };

		// Token: 0x04000819 RID: 2073
		private const int GUID_SIZE = 16;

		// Token: 0x0400081A RID: 2074
		private byte[] _tempGuidBytes;

		// Token: 0x0400081B RID: 2075
		internal bool _asyncWrite;

		// Token: 0x0400081C RID: 2076
		private bool _serverSupportsColumnEncryption;

		// Token: 0x0400081D RID: 2077
		private static readonly byte[] s_FeatureExtDataAzureSQLSupportFeatureRequest = new byte[] { 1 };

		// Token: 0x04000824 RID: 2084
		private SqlCollation _cachedCollation;

		// Token: 0x04000825 RID: 2085
		private static readonly IEnumerable<Microsoft.Data.SqlClient.Server.SqlDataRecord> __tvpEmptyValue = new List<Microsoft.Data.SqlClient.Server.SqlDataRecord>().AsReadOnly();

		// Token: 0x04000826 RID: 2086
		private const ulong _indeterminateSize = 18446744073709551615UL;

		// Token: 0x04000827 RID: 2087
		private const string StateTraceFormatString = "\n\t         _physicalStateObj = {0}\n\t         _pMarsPhysicalConObj = {1}\n\t         _state = {2}\n\t         _server = {3}\n\t         _fResetConnection = {4}\n\t         _defaultCollation = {5}\n\t         _defaultCodePage = {6}\n\t         _defaultLCID = {7}\n\t         _defaultEncoding = {8}\n\t         _encryptionOption = {9}\n\t         _currentTransaction = {10}\n\t         _pendingTransaction = {11}\n\t         _retainedTransactionId = {12}\n\t         _nonTransactedOpenResultCount = {13}\n\t         _connHandler = {14}\n\t         _fMARS = {15}\n\t         _sessionPool = {16}\n\t         _is2000 = {17}\n\t         _is2000SP1 = {18}\n\t         _is2005 = {19}\n\t         _sniSpnBuffer = {20}\n\t         _errors = {21}\n\t         _warnings = {22}\n\t         _attentionErrors = {23}\n\t         _attentionWarnings = {24}\n\t         _statistics = {25}\n\t         _statisticsIsInTransaction = {26}\n\t         _fPreserveTransaction = {27}         _fParallel = {28}";

		// Token: 0x02000253 RID: 595
		internal struct ReliabilitySection
		{
			// Token: 0x06001EE7 RID: 7911 RVA: 0x0000BB08 File Offset: 0x00009D08
			[Conditional("DEBUG")]
			internal void Start()
			{
			}

			// Token: 0x06001EE8 RID: 7912 RVA: 0x0000BB08 File Offset: 0x00009D08
			[Conditional("DEBUG")]
			internal void Stop()
			{
			}

			// Token: 0x06001EE9 RID: 7913 RVA: 0x0000BB08 File Offset: 0x00009D08
			[Conditional("DEBUG")]
			internal static void Assert(string message)
			{
			}
		}

		// Token: 0x02000254 RID: 596
		private class TdsOrderUnique
		{
			// Token: 0x06001EEA RID: 7914 RVA: 0x0007E241 File Offset: 0x0007C441
			internal TdsOrderUnique(short ordinal, byte flags)
			{
				this.ColumnOrdinal = ordinal;
				this.Flags = flags;
			}

			// Token: 0x0400169C RID: 5788
			internal short ColumnOrdinal;

			// Token: 0x0400169D RID: 5789
			internal byte Flags;
		}

		// Token: 0x02000255 RID: 597
		private class TdsOutputStream : Stream
		{
			// Token: 0x06001EEB RID: 7915 RVA: 0x0007E257 File Offset: 0x0007C457
			public TdsOutputStream(TdsParser parser, TdsParserStateObject stateObj, byte[] preambleToStrip)
			{
				this._parser = parser;
				this._stateObj = stateObj;
				this._preambleToStrip = preambleToStrip;
			}

			// Token: 0x17000A40 RID: 2624
			// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0001996E File Offset: 0x00017B6E
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000A41 RID: 2625
			// (get) Token: 0x06001EED RID: 7917 RVA: 0x0001996E File Offset: 0x00017B6E
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000A42 RID: 2626
			// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0000EBAD File Offset: 0x0000CDAD
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001EEF RID: 7919 RVA: 0x0000BB08 File Offset: 0x00009D08
			public override void Flush()
			{
			}

			// Token: 0x17000A43 RID: 2627
			// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x00034508 File Offset: 0x00032708
			public override long Length
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000A44 RID: 2628
			// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x00034508 File Offset: 0x00032708
			// (set) Token: 0x06001EF2 RID: 7922 RVA: 0x00034508 File Offset: 0x00032708
			public override long Position
			{
				get
				{
					throw new NotSupportedException();
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06001EF3 RID: 7923 RVA: 0x00034508 File Offset: 0x00032708
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001EF4 RID: 7924 RVA: 0x00034508 File Offset: 0x00032708
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001EF5 RID: 7925 RVA: 0x00034508 File Offset: 0x00032708
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001EF6 RID: 7926 RVA: 0x0007E274 File Offset: 0x0007C474
			private void StripPreamble(byte[] buffer, ref int offset, ref int count)
			{
				if (this._preambleToStrip != null && count >= this._preambleToStrip.Length)
				{
					for (int i = 0; i < this._preambleToStrip.Length; i++)
					{
						if (this._preambleToStrip[i] != buffer[i])
						{
							this._preambleToStrip = null;
							return;
						}
					}
					offset += this._preambleToStrip.Length;
					count -= this._preambleToStrip.Length;
				}
				this._preambleToStrip = null;
			}

			// Token: 0x06001EF7 RID: 7927 RVA: 0x0007E2DE File Offset: 0x0007C4DE
			public override void Write(byte[] buffer, int offset, int count)
			{
				TdsParser.TdsOutputStream.ValidateWriteParameters(buffer, offset, count);
				this.StripPreamble(buffer, ref offset, ref count);
				if (count > 0)
				{
					this._parser.WriteInt(count, this._stateObj);
					this._stateObj.WriteByteArray(buffer, count, offset, true, null);
				}
			}

			// Token: 0x06001EF8 RID: 7928 RVA: 0x0007E31C File Offset: 0x0007C51C
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				TdsParser.TdsOutputStream.ValidateWriteParameters(buffer, offset, count);
				this.StripPreamble(buffer, ref offset, ref count);
				RuntimeHelpers.PrepareConstrainedRegions();
				Task task2;
				try
				{
					Task task = null;
					if (count > 0)
					{
						this._parser.WriteInt(count, this._stateObj);
						task = this._stateObj.WriteByteArray(buffer, count, offset, false, null);
					}
					if (task == null)
					{
						task2 = TdsParser.CompletedTask;
					}
					else
					{
						task2 = task;
					}
				}
				catch (OutOfMemoryException)
				{
					this._parser._connHandler.DoomThisConnection();
					throw;
				}
				catch (StackOverflowException)
				{
					this._parser._connHandler.DoomThisConnection();
					throw;
				}
				catch (ThreadAbortException)
				{
					this._parser._connHandler.DoomThisConnection();
					throw;
				}
				return task2;
			}

			// Token: 0x06001EF9 RID: 7929 RVA: 0x0007E3DC File Offset: 0x0007C5DC
			internal static void ValidateWriteParameters(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw ADP.ArgumentNull("buffer");
				}
				if (offset < 0)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				if (count < 0)
				{
					throw ADP.ArgumentOutOfRange("count");
				}
				try
				{
					if (checked(offset + count) > buffer.Length)
					{
						throw ExceptionBuilder.InvalidOffsetLength();
					}
				}
				catch (OverflowException)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}

			// Token: 0x0400169E RID: 5790
			private TdsParser _parser;

			// Token: 0x0400169F RID: 5791
			private TdsParserStateObject _stateObj;

			// Token: 0x040016A0 RID: 5792
			private byte[] _preambleToStrip;
		}

		// Token: 0x02000256 RID: 598
		private class ConstrainedTextWriter : TextWriter
		{
			// Token: 0x06001EFA RID: 7930 RVA: 0x0007E440 File Offset: 0x0007C640
			public ConstrainedTextWriter(TextWriter next, int size)
			{
				this._next = next;
				this._size = size;
				this._written = 0;
				if (this._size < 1)
				{
					this._size = int.MaxValue;
				}
			}

			// Token: 0x17000A45 RID: 2629
			// (get) Token: 0x06001EFB RID: 7931 RVA: 0x0007E471 File Offset: 0x0007C671
			public bool IsComplete
			{
				get
				{
					return this._size > 0 && this._written >= this._size;
				}
			}

			// Token: 0x17000A46 RID: 2630
			// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0007E48F File Offset: 0x0007C68F
			public override Encoding Encoding
			{
				get
				{
					return this._next.Encoding;
				}
			}

			// Token: 0x06001EFD RID: 7933 RVA: 0x0007E49C File Offset: 0x0007C69C
			public override void Flush()
			{
				this._next.Flush();
			}

			// Token: 0x06001EFE RID: 7934 RVA: 0x0007E4A9 File Offset: 0x0007C6A9
			public override Task FlushAsync()
			{
				return this._next.FlushAsync();
			}

			// Token: 0x06001EFF RID: 7935 RVA: 0x0007E4B6 File Offset: 0x0007C6B6
			public override void Write(char value)
			{
				if (this._written < this._size)
				{
					this._next.Write(value);
					this._written++;
				}
			}

			// Token: 0x06001F00 RID: 7936 RVA: 0x0007E4E0 File Offset: 0x0007C6E0
			public override void Write(char[] buffer, int index, int count)
			{
				TdsParser.ConstrainedTextWriter.ValidateWriteParameters(buffer, index, count);
				count = Math.Min(this._size - this._written, count);
				if (count > 0)
				{
					this._next.Write(buffer, index, count);
				}
				this._written += count;
			}

			// Token: 0x06001F01 RID: 7937 RVA: 0x0007E51F File Offset: 0x0007C71F
			public override Task WriteAsync(char value)
			{
				if (this._written < this._size)
				{
					this._written++;
					return this._next.WriteAsync(value);
				}
				return TdsParser.CompletedTask;
			}

			// Token: 0x06001F02 RID: 7938 RVA: 0x0007E550 File Offset: 0x0007C750
			public override Task WriteAsync(char[] buffer, int index, int count)
			{
				TdsParser.ConstrainedTextWriter.ValidateWriteParameters(buffer, index, count);
				count = Math.Min(this._size - this._written, count);
				if (count > 0)
				{
					this._written += count;
					return this._next.WriteAsync(buffer, index, count);
				}
				return TdsParser.CompletedTask;
			}

			// Token: 0x06001F03 RID: 7939 RVA: 0x0007E5A0 File Offset: 0x0007C7A0
			public override Task WriteAsync(string value)
			{
				return base.WriteAsync(value.ToCharArray());
			}

			// Token: 0x06001F04 RID: 7940 RVA: 0x0007E5B0 File Offset: 0x0007C7B0
			internal static void ValidateWriteParameters(char[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw ADP.ArgumentNull("buffer");
				}
				if (offset < 0)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				if (count < 0)
				{
					throw ADP.ArgumentOutOfRange("count");
				}
				try
				{
					if (checked(offset + count) > buffer.Length)
					{
						throw ExceptionBuilder.InvalidOffsetLength();
					}
				}
				catch (OverflowException)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}

			// Token: 0x040016A1 RID: 5793
			private TextWriter _next;

			// Token: 0x040016A2 RID: 5794
			private int _size;

			// Token: 0x040016A3 RID: 5795
			private int _written;
		}
	}
}
