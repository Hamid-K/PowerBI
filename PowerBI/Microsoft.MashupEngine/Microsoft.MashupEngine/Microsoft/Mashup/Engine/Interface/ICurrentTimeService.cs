using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000038 RID: 56
	public interface ICurrentTimeService
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600012F RID: 303
		DateTime FixedUtcNow { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000130 RID: 304
		DateTime UtcNow { get; }
	}
}
