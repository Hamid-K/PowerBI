using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000030 RID: 48
	public interface ISpatial
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600017C RID: 380
		CoordinateSystem CoordinateSystem { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600017D RID: 381
		bool IsEmpty { get; }
	}
}
