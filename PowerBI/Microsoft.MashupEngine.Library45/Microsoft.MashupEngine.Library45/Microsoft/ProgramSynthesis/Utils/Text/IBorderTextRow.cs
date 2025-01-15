using System;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000537 RID: 1335
	public interface IBorderTextRow : ITextRow
	{
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001E18 RID: 7704
		char Dash { get; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001E19 RID: 7705
		// (set) Token: 0x06001E1A RID: 7706
		bool External { get; set; }

		// Token: 0x06001E1B RID: 7707
		char DoubleJunction(ITextColumn col, ITextRow row);

		// Token: 0x06001E1C RID: 7708
		char SingleJunction(ITextColumn col, ITextRow row);
	}
}
