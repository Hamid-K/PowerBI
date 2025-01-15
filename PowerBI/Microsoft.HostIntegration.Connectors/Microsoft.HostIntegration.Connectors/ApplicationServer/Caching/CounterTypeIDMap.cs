using System;
using System.Diagnostics.PerformanceData;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000356 RID: 854
	internal struct CounterTypeIDMap
	{
		// Token: 0x06001E1E RID: 7710 RVA: 0x0005A266 File Offset: 0x00058466
		public CounterTypeIDMap(string name, CounterType type, int IDInManifest)
		{
			this.Name = name;
			this.Type = type;
			this.ID = IDInManifest;
		}

		// Token: 0x040010F1 RID: 4337
		public string Name;

		// Token: 0x040010F2 RID: 4338
		public CounterType Type;

		// Token: 0x040010F3 RID: 4339
		public int ID;
	}
}
