using System;
using System.Collections.ObjectModel;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000023 RID: 35
	public interface IMapMetadata : IXEObjectMetadata
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000094 RID: 148
		ReadOnlyCollection<MapValue> Entries { get; }

		// Token: 0x17000022 RID: 34
		MapValue this[uint mapKey] { get; }
	}
}
