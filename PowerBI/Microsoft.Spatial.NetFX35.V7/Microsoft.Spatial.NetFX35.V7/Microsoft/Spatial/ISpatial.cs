using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200002C RID: 44
	public interface ISpatial
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000110 RID: 272
		CoordinateSystem CoordinateSystem { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000111 RID: 273
		bool IsEmpty { get; }
	}
}
