using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001693 RID: 5779
	public interface IUserMessage
	{
		// Token: 0x1700266A RID: 9834
		// (get) Token: 0x06009247 RID: 37447
		TextValue Message { get; }

		// Token: 0x1700266B RID: 9835
		// (get) Token: 0x06009248 RID: 37448
		ListValue Parameters { get; }
	}
}
