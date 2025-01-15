using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x0200001B RID: 27
	public interface IMetadataGeneration
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600007A RID: 122
		ReadOnlyCollection<IPackage> Packages { get; }

		// Token: 0x0600007B RID: 123
		IPackage GetPackage(Guid moduleId, string name);

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600007C RID: 124
		KeyValuePair<Guid, ushort> GenerationId { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007D RID: 125
		int PointerSize { get; }
	}
}
