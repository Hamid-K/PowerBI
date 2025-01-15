using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003B4 RID: 948
	public class Title
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x000025F4 File Offset: 0x000007F4
		public Title()
		{
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0007D9AC File Offset: 0x0007BBAC
		public Title(string caption, Style style, Title.Positions position)
		{
			this.Caption = caption;
			this.Style = style;
			this.Position = position;
		}

		// Token: 0x04000D3B RID: 3387
		[DefaultValue("")]
		public string Caption;

		// Token: 0x04000D3C RID: 3388
		public Style Style;

		// Token: 0x04000D3D RID: 3389
		[DefaultValue(Title.Positions.Center)]
		public Title.Positions Position;

		// Token: 0x0200050F RID: 1295
		public enum Positions
		{
			// Token: 0x0400124E RID: 4686
			Center,
			// Token: 0x0400124F RID: 4687
			Near,
			// Token: 0x04001250 RID: 4688
			Far
		}
	}
}
