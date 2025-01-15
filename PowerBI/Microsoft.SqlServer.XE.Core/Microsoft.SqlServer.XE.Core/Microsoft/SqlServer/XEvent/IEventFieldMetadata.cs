using System;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x0200001D RID: 29
	public interface IEventFieldMetadata
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000087 RID: 135
		string Name { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000088 RID: 136
		Type Type { get; }
	}
}
