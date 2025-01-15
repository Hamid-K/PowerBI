using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	internal class CommonTransportElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x00015607 File Offset: 0x00013807
		public CommonTransportElement()
		{
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00016EC0 File Offset: 0x000150C0
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x00016ED2 File Offset: 0x000150D2
		[ConfigurationProperty("maxBufferPoolSize", IsRequired = false, DefaultValue = -1L)]
		public long MaxBufferPoolSize
		{
			get
			{
				return (long)base["maxBufferPoolSize"];
			}
			set
			{
				base["maxBufferPoolSize"] = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00016EE5 File Offset: 0x000150E5
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x00016EF7 File Offset: 0x000150F7
		[ConfigurationProperty("maxBufferSize", IsRequired = false, DefaultValue = -1)]
		public int MaxBufferSize
		{
			get
			{
				return (int)base["maxBufferSize"];
			}
			set
			{
				base["maxBufferSize"] = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00016F0A File Offset: 0x0001510A
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x00016F1C File Offset: 0x0001511C
		[ConfigurationProperty("connectionBufferSize", IsRequired = false, DefaultValue = -1)]
		public int ConnectionBufferSize
		{
			get
			{
				return (int)base["connectionBufferSize"];
			}
			set
			{
				base["connectionBufferSize"] = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00016F2F File Offset: 0x0001512F
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x00016F41 File Offset: 0x00015141
		[ConfigurationProperty("channelInitializationTimeout", IsRequired = false, DefaultValue = -1)]
		public int ChannelInitializationTimeout
		{
			get
			{
				return (int)base["channelInitializationTimeout"];
			}
			set
			{
				base["channelInitializationTimeout"] = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00016F54 File Offset: 0x00015154
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x00016F66 File Offset: 0x00015166
		[ConfigurationProperty("maxOutputDelay", IsRequired = false, DefaultValue = -1)]
		public int MaxOutputDelay
		{
			get
			{
				return (int)base["maxOutputDelay"];
			}
			set
			{
				base["maxOutputDelay"] = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00016F79 File Offset: 0x00015179
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x00016F8B File Offset: 0x0001518B
		[ConfigurationProperty("receiveTimeout", IsRequired = false, DefaultValue = -1)]
		public int ReceiveTimeout
		{
			get
			{
				return (int)base["receiveTimeout"];
			}
			set
			{
				base["receiveTimeout"] = value;
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00016FA0 File Offset: 0x000151A0
		protected CommonTransportElement(SerializationInfo info, StreamingContext context)
		{
			this.MaxBufferPoolSize = (long)info.GetValue("maxBufferPoolSize", typeof(long));
			this.MaxBufferSize = (int)info.GetValue("maxBufferSize", typeof(int));
			this.ConnectionBufferSize = (int)info.GetValue("connectionBufferSize", typeof(int));
			this.ChannelInitializationTimeout = (int)info.GetValue("channelInitializationTimeout", typeof(int));
			this.MaxOutputDelay = (int)info.GetValue("maxOutputDelay", typeof(int));
			this.ReceiveTimeout = (int)info.GetValue("receiveTimeout", typeof(int));
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00017074 File Offset: 0x00015274
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("maxBufferPoolSize", this.MaxBufferPoolSize);
			info.AddValue("maxBufferSize", this.MaxBufferSize);
			info.AddValue("connectionBufferSize", this.ConnectionBufferSize);
			info.AddValue("channelInitializationTimeout", this.ChannelInitializationTimeout);
			info.AddValue("maxOutputDelay", this.MaxOutputDelay);
			info.AddValue("receiveTimeout", this.ReceiveTimeout);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000170E8 File Offset: 0x000152E8
		internal DataCacheTransportProperties GetDataCacheTransportProperties()
		{
			DataCacheTransportProperties dataCacheTransportProperties = new DataCacheTransportProperties();
			if (this.MaxBufferPoolSize != -1L)
			{
				dataCacheTransportProperties.MaxBufferPoolSize = this.MaxBufferPoolSize;
			}
			if (this.MaxBufferSize != -1)
			{
				dataCacheTransportProperties.MaxBufferSize = this.MaxBufferSize;
			}
			if (this.ConnectionBufferSize != -1)
			{
				dataCacheTransportProperties.ConnectionBufferSize = this.ConnectionBufferSize;
			}
			if (this.ChannelInitializationTimeout != -1)
			{
				dataCacheTransportProperties.ChannelInitializationTimeout = TimeSpan.FromMilliseconds((double)this.ChannelInitializationTimeout);
			}
			if (this.MaxOutputDelay != -1)
			{
				dataCacheTransportProperties.MaxOutputDelay = TimeSpan.FromMilliseconds((double)this.MaxOutputDelay);
			}
			if (this.ReceiveTimeout != -1)
			{
				dataCacheTransportProperties.ReceiveTimeout = TimeSpan.FromMilliseconds((double)this.ReceiveTimeout);
			}
			return dataCacheTransportProperties;
		}

		// Token: 0x04000390 RID: 912
		internal const int UNINITIALIZED_VALUE = -1;

		// Token: 0x04000391 RID: 913
		internal const string MAX_POOLSIZE = "maxBufferPoolSize";

		// Token: 0x04000392 RID: 914
		internal const string MAX_BUFFERSIZE = "maxBufferSize";

		// Token: 0x04000393 RID: 915
		internal const string CONNECTION_BUFFER_SIZE = "connectionBufferSize";

		// Token: 0x04000394 RID: 916
		internal const string CHANNEL_INITIALIZATION_TIMEOUT = "channelInitializationTimeout";

		// Token: 0x04000395 RID: 917
		internal const string MAX_OUTPUT_DELAY = "maxOutputDelay";

		// Token: 0x04000396 RID: 918
		internal const string RECV_TIMEOUT = "receiveTimeout";

		// Token: 0x04000397 RID: 919
		internal static readonly TimeSpan UNINITIALIZED_TIMESPAN = new TimeSpan(-1L);
	}
}
