using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000C7 RID: 199
	[Serializable]
	internal class TransportElement : CommonTransportElement, ISerializable
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0001719B File Offset: 0x0001539B
		public TransportElement()
		{
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000171A3 File Offset: 0x000153A3
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x000171B5 File Offset: 0x000153B5
		[ConfigurationProperty("maxConnectionsHigh", IsRequired = false, DefaultValue = 2000)]
		public int MaxConnectionsHigh
		{
			get
			{
				return (int)base["maxConnectionsHigh"];
			}
			set
			{
				base["maxConnectionsHigh"] = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x000171C8 File Offset: 0x000153C8
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x000171DA File Offset: 0x000153DA
		[ConfigurationProperty("maxConnectionsLow", IsRequired = false, DefaultValue = 1000)]
		public int MaxConnectionsLow
		{
			get
			{
				return (int)base["maxConnectionsLow"];
			}
			set
			{
				base["maxConnectionsLow"] = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x000171ED File Offset: 0x000153ED
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x000171FF File Offset: 0x000153FF
		[ConfigurationProperty("listenBacklog", IsRequired = false, DefaultValue = -1)]
		public int ListenBacklog
		{
			get
			{
				return (int)base["listenBacklog"];
			}
			set
			{
				base["listenBacklog"] = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00017212 File Offset: 0x00015412
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00017224 File Offset: 0x00015424
		[ConfigurationProperty("maxPendingConnections", IsRequired = false, DefaultValue = -1)]
		public int MaxPendingConnections
		{
			get
			{
				return (int)base["maxPendingConnections"];
			}
			set
			{
				base["maxPendingConnections"] = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00017237 File Offset: 0x00015437
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00017249 File Offset: 0x00015449
		[ConfigurationProperty("maxPendingAccepts", IsRequired = false, DefaultValue = -1)]
		public int MaxPendingAccepts
		{
			get
			{
				return (int)base["maxPendingAccepts"];
			}
			set
			{
				base["maxPendingAccepts"] = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001725C File Offset: 0x0001545C
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0001726E File Offset: 0x0001546E
		[ConfigurationProperty("maxServerToServerConnections", DefaultValue = 5, IsRequired = false)]
		[IntegerValidator(MinValue = 1, MaxValue = 100)]
		public int MaxServerToServerConnections
		{
			get
			{
				return (int)base["maxServerToServerConnections"];
			}
			set
			{
				base["maxServerToServerConnections"] = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00017281 File Offset: 0x00015481
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00017293 File Offset: 0x00015493
		[ConfigurationProperty("serverToServerReceiveTimeout", IsRequired = false, DefaultValue = -1)]
		public int ServerToServerReceiveTimeout
		{
			get
			{
				return (int)base["serverToServerReceiveTimeout"];
			}
			set
			{
				base["serverToServerReceiveTimeout"] = value;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000172A8 File Offset: 0x000154A8
		protected TransportElement(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			Version version = null;
			if (context.Context != null)
			{
				SerializationContext serializationContext = context.Context as SerializationContext;
				version = serializationContext.StoreVersion;
			}
			this.MaxPendingAccepts = (int)info.GetValue("maxPendingAccepts", typeof(int));
			this.ListenBacklog = (int)info.GetValue("listenBacklog", typeof(int));
			this.MaxPendingConnections = (int)info.GetValue("maxPendingConnections", typeof(int));
			this.MaxConnectionsHigh = (int)info.GetValue("maxConnectionsHigh", typeof(int));
			this.MaxConnectionsLow = (int)info.GetValue("maxConnectionsLow", typeof(int));
			try
			{
				this.MaxServerToServerConnections = (int)info.GetValue("maxServerToServerConnections", typeof(int));
			}
			catch (SerializationException)
			{
				this.MaxServerToServerConnections = 5;
			}
			if (ConfigManager.IsStoreVersionHigherThan2000(version))
			{
				this.ServerToServerReceiveTimeout = (int)info.GetValue("serverToServerReceiveTimeout", typeof(int));
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000173E0 File Offset: 0x000155E0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("maxPendingAccepts", this.MaxPendingAccepts);
			info.AddValue("listenBacklog", this.ListenBacklog);
			info.AddValue("maxPendingConnections", this.MaxPendingConnections);
			info.AddValue("maxConnectionsHigh", this.MaxConnectionsHigh);
			info.AddValue("maxConnectionsLow", this.MaxConnectionsLow);
			info.AddValue("maxServerToServerConnections", this.MaxServerToServerConnections);
			info.AddValue("serverToServerReceiveTimeout", this.ServerToServerReceiveTimeout);
		}

		// Token: 0x04000398 RID: 920
		internal const string MAX_PENDING_ACCEPTS = "maxPendingAccepts";

		// Token: 0x04000399 RID: 921
		internal const string LISTEN_BACKLOG = "listenBacklog";

		// Token: 0x0400039A RID: 922
		internal const string MAX_PENDING_CONNECTIONS = "maxPendingConnections";

		// Token: 0x0400039B RID: 923
		internal const string MAX_CONNECTIONS_HIGH = "maxConnectionsHigh";

		// Token: 0x0400039C RID: 924
		internal const string MAX_CONNECTIONS_LOW = "maxConnectionsLow";

		// Token: 0x0400039D RID: 925
		internal const string MAX_S2S_CONNECTIONS = "maxServerToServerConnections";

		// Token: 0x0400039E RID: 926
		internal const string S2S_RECEIVE_TIMEOUT = "serverToServerReceiveTimeout";

		// Token: 0x0400039F RID: 927
		internal const int MAX_S2S_CONNECTIONS_DEFAULT = 5;
	}
}
