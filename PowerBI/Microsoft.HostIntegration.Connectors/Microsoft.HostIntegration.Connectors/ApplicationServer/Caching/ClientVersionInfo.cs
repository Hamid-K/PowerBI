using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002F1 RID: 753
	[DataContract(Name = "ClientVersionInfo", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class ClientVersionInfo
	{
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x0005555A File Offset: 0x0005375A
		internal static ClientVersionInfo Singleton
		{
			get
			{
				return ClientVersionInfo._singleton;
			}
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00055564 File Offset: 0x00053764
		protected ClientVersionInfo()
		{
			this._codeVersion = ConfigManager.CodeVersion;
			this._beginAllowedClientCodeVersionRange = ConfigManager.CodeVersion;
			this._endAllowedClientCodeVersionRange = ConfigManager.CodeVersion;
			this._allowedClientCodeVersions.Add(ConfigManager.CodeVersion);
			this._serverInfo = ConfigManager.ServerInfo;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000555C0 File Offset: 0x000537C0
		internal ClientVersionInfo(ClientVersionInfo info)
		{
			this._codeVersion = info.CodeVersion;
			this._beginAllowedClientCodeVersionRange = info.BeginAllowedClientCodeVersionRange;
			this._endAllowedClientCodeVersionRange = info.EndAllowedClientCodeVersionRange;
			this._allowedClientCodeVersions = info._allowedClientCodeVersions;
			this._serverInfo = info._serverInfo;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0005561C File Offset: 0x0005381C
		private static ClientVersionInfo GetInvalidClientVersionInfo()
		{
			return new ClientVersionInfo
			{
				_codeVersion = ClientVersionInfo._invalidVersion,
				_beginAllowedClientCodeVersionRange = ClientVersionInfo._invalidVersion,
				_endAllowedClientCodeVersionRange = ClientVersionInfo._invalidVersion
			};
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x00055651 File Offset: 0x00053851
		internal bool IsInvalid
		{
			get
			{
				return this.CodeVersion == ClientVersionInfo._invalidVersion && this.BeginAllowedClientCodeVersionRange == ClientVersionInfo._invalidVersion && this.EndAllowedClientCodeVersionRange == ClientVersionInfo._invalidVersion;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0005567C File Offset: 0x0005387C
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x00055684 File Offset: 0x00053884
		[DataMember]
		internal long CodeVersion
		{
			get
			{
				return this._codeVersion;
			}
			private set
			{
				this._codeVersion = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0005568D File Offset: 0x0005388D
		// (set) Token: 0x06001C43 RID: 7235 RVA: 0x00055695 File Offset: 0x00053895
		[DataMember]
		internal long BeginAllowedClientCodeVersionRange
		{
			get
			{
				return this._beginAllowedClientCodeVersionRange;
			}
			set
			{
				this._beginAllowedClientCodeVersionRange = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0005569E File Offset: 0x0005389E
		// (set) Token: 0x06001C45 RID: 7237 RVA: 0x000556A6 File Offset: 0x000538A6
		[DataMember]
		internal long EndAllowedClientCodeVersionRange
		{
			get
			{
				return this._endAllowedClientCodeVersionRange;
			}
			private set
			{
				this._endAllowedClientCodeVersionRange = value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x000556AF File Offset: 0x000538AF
		// (set) Token: 0x06001C47 RID: 7239 RVA: 0x000556B7 File Offset: 0x000538B7
		[DataMember]
		internal HashSet<long> AllowedClientCodeVersions
		{
			get
			{
				return this._allowedClientCodeVersions;
			}
			private set
			{
				this._allowedClientCodeVersions = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x000556C0 File Offset: 0x000538C0
		// (set) Token: 0x06001C49 RID: 7241 RVA: 0x000556C8 File Offset: 0x000538C8
		[DataMember]
		internal ServerInformation ServerInfo
		{
			get
			{
				return this._serverInfo;
			}
			private set
			{
				this._serverInfo = value;
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000556D4 File Offset: 0x000538D4
		internal void EditAllowedVersions(long beginAllowedVersion, long endAllowedVersion, List<long> otherSupportedClients)
		{
			lock (this)
			{
				this.BeginAllowedClientCodeVersionRange = beginAllowedVersion;
				this.EndAllowedClientCodeVersionRange = endAllowedVersion;
				this.AllowedClientCodeVersions.Clear();
				for (long num = beginAllowedVersion; num <= endAllowedVersion; num += 1L)
				{
					this.AllowedClientCodeVersions.Add(num);
				}
				this.AllowedClientCodeVersions.UnionWith(otherSupportedClients);
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00055748 File Offset: 0x00053948
		internal static bool VerifyClientVersion(ClientVersionInfo info, out ServerInformation serverInfo)
		{
			serverInfo = info.ServerInfo;
			return ClientVersionInfo.VerifyClientVersion(info);
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00055758 File Offset: 0x00053958
		internal static bool VerifyClientVersion(ClientVersionInfo info)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(ClientVersionInfo._logsource, "Server version info - {0}, client version info - {1}", new object[]
				{
					info,
					ClientVersionInfo.Singleton
				});
			}
			return ClientVersionInfo.Singleton.IsCompatible(info);
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0005579B File Offset: 0x0005399B
		private static ClientVersionInfo GetRemoteVersionInfo(Message message)
		{
			return message.GetBody<ClientVersionInfo>();
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000557A4 File Offset: 0x000539A4
		internal static Message SendServerVersionInfo(Message message, IChannelContainer container)
		{
			ClientVersionInfo remoteVersionInfo = ClientVersionInfo.GetRemoteVersionInfo(message);
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				int hashCode = container.Channel.GetHashCode();
				EventLogWriter.WriteInfo(ClientVersionInfo._logsource, "Remote version {0} for channel {1}.", new object[] { remoteVersionInfo, hashCode });
			}
			container.RemoteVersionInfo = remoteVersionInfo;
			if (CloudUtility.IsVASDeployment)
			{
				ServerInformation serverInformation = new ServerInformation();
				serverInformation.InstanceAddressInternal = CloudUtility.GetCurrentInternalEndpointUri();
				ClientVersionInfo.Singleton.ServerInfo = serverInformation;
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteWarning(ClientVersionInfo._logsource, "ClientVer Current Uri:{0}", new object[] { serverInformation.InstanceAddressInternal.ToString() });
				}
			}
			return Utility.CreateMessage(null, ClientVersionInfo.Singleton);
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00055855 File Offset: 0x00053A55
		private bool IsCompatible(ClientVersionInfo info)
		{
			return info._allowedClientCodeVersions != null && info._allowedClientCodeVersions.Contains(this._codeVersion);
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00055874 File Offset: 0x00053A74
		public override string ToString()
		{
			string text = string.Empty;
			if (this._allowedClientCodeVersions != null)
			{
				foreach (long num in this._allowedClientCodeVersions)
				{
					text += num;
					text += ',';
				}
			}
			return string.Format(CultureInfo.InvariantCulture, "[({0})]", new object[] { text });
		}

		// Token: 0x04000F09 RID: 3849
		private static ClientVersionInfo _singleton = new ClientVersionInfo();

		// Token: 0x04000F0A RID: 3850
		private static long _invalidVersion = long.MaxValue;

		// Token: 0x04000F0B RID: 3851
		internal static ClientVersionInfo Invalid = ClientVersionInfo.GetInvalidClientVersionInfo();

		// Token: 0x04000F0C RID: 3852
		private static string _logsource = "DistributedCache.ClientVersionInfo";

		// Token: 0x04000F0D RID: 3853
		protected long _codeVersion;

		// Token: 0x04000F0E RID: 3854
		private long _beginAllowedClientCodeVersionRange;

		// Token: 0x04000F0F RID: 3855
		private long _endAllowedClientCodeVersionRange;

		// Token: 0x04000F10 RID: 3856
		private HashSet<long> _allowedClientCodeVersions = new HashSet<long>();

		// Token: 0x04000F11 RID: 3857
		private ServerInformation _serverInfo;
	}
}
