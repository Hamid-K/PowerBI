using System;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200073E RID: 1854
	public interface IRECPropertyPage
	{
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06003A1A RID: 14874
		// (set) Token: 0x06003A1B RID: 14875
		string Name { get; set; }

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06003A1C RID: 14876
		// (set) Token: 0x06003A1D RID: 14877
		int Order { get; set; }

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06003A1E RID: 14878
		// (set) Token: 0x06003A1F RID: 14879
		Guid Identifier { get; set; }

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06003A20 RID: 14880
		// (set) Token: 0x06003A21 RID: 14881
		string PropertyGUID { get; set; }
	}
}
