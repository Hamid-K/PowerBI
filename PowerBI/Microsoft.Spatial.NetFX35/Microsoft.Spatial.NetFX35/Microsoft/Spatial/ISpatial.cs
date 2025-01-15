using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000016 RID: 22
	public interface ISpatial
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000E4 RID: 228
		CoordinateSystem CoordinateSystem { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000E5 RID: 229
		bool IsEmpty { get; }
	}
}
