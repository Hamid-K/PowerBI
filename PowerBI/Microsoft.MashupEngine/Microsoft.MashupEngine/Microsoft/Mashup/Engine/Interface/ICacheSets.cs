using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000053 RID: 83
	public interface ICacheSets : IDisposable
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000158 RID: 344
		ICacheSet Metadata { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000159 RID: 345
		ICacheSet Data { get; }
	}
}
