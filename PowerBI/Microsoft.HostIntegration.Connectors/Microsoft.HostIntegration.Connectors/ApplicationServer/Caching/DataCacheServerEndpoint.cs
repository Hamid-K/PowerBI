using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000A6 RID: 166
	public class DataCacheServerEndpoint
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x00013F79 File Offset: 0x00012179
		public DataCacheServerEndpoint(string hostName, int cachePort)
		{
			this._hostName = hostName;
			this._cachePort = cachePort;
			this._uriString = Utility.GetServiceUri(hostName, cachePort, TransportProtocol.NetTcp);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00013F9D File Offset: 0x0001219D
		public string HostName
		{
			get
			{
				return this._hostName;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00013FA5 File Offset: 0x000121A5
		public int CachePort
		{
			get
			{
				return this._cachePort;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00013FAD File Offset: 0x000121AD
		internal string UriString
		{
			get
			{
				return this._uriString;
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013FB8 File Offset: 0x000121B8
		public override bool Equals(object obj)
		{
			DataCacheServerEndpoint dataCacheServerEndpoint = obj as DataCacheServerEndpoint;
			return this.UriString.Equals(dataCacheServerEndpoint.UriString);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00013FDD File Offset: 0x000121DD
		public override int GetHashCode()
		{
			return this._hostName.GetHashCode() ^ this._cachePort;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00013FF1 File Offset: 0x000121F1
		internal bool IsServerEndpointWellFormed()
		{
			return Uri.IsWellFormedUriString(this._uriString, UriKind.Absolute);
		}

		// Token: 0x04000305 RID: 773
		private string _hostName;

		// Token: 0x04000306 RID: 774
		private int _cachePort;

		// Token: 0x04000307 RID: 775
		private string _uriString;
	}
}
