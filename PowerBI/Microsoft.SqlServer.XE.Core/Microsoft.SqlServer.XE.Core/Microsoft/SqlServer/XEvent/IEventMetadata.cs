using System;
using System.Collections.ObjectModel;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000020 RID: 32
	public interface IEventMetadata : IXEObjectMetadata
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008F RID: 143
		ReadOnlyCollection<IEventFieldMetadata> Fields { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000090 RID: 144
		Guid UUID { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000091 RID: 145
		int Version { get; }
	}
}
