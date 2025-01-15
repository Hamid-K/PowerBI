using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200007A RID: 122
	public interface IMapTileServerConfiguration
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000359 RID: 857
		int MaxConnections { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600035A RID: 858
		int Timeout { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600035B RID: 859
		string AppID { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600035C RID: 860
		bool Enabled { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600035D RID: 861
		MapTileCacheLevel CacheLevel { get; }
	}
}
