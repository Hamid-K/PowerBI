using System;
using System.Collections.ObjectModel;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000022 RID: 34
	public interface ITargetMetadata : IXEObjectMetadata
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000093 RID: 147
		ReadOnlyCollection<ICustomAttributeMetadata> Parameters { get; }
	}
}
