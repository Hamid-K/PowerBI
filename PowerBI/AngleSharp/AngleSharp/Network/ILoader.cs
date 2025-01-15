using System;
using System.Collections.Generic;

namespace AngleSharp.Network
{
	// Token: 0x02000092 RID: 146
	public interface ILoader
	{
		// Token: 0x0600046E RID: 1134
		IEnumerable<IDownload> GetDownloads();
	}
}
