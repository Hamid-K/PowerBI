using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000399 RID: 921
	internal class SerializationContext
	{
		// Token: 0x060020B1 RID: 8369 RVA: 0x00063FDC File Offset: 0x000621DC
		internal SerializationContext(Version storeVersion, ClientVersionInfo clientVersionInfo)
		{
			this._storeVersion = storeVersion;
			this._clientVersionInfo = clientVersionInfo;
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x00063FF2 File Offset: 0x000621F2
		internal Version StoreVersion
		{
			get
			{
				return this._storeVersion;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x00063FFA File Offset: 0x000621FA
		internal ClientVersionInfo ClientVersionInfo
		{
			get
			{
				return this._clientVersionInfo;
			}
		}

		// Token: 0x04001320 RID: 4896
		private readonly Version _storeVersion;

		// Token: 0x04001321 RID: 4897
		private readonly ClientVersionInfo _clientVersionInfo;
	}
}
