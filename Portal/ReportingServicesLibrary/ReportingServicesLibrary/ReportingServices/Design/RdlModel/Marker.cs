using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003B2 RID: 946
	public class Marker
	{
		// Token: 0x06001EC0 RID: 7872 RVA: 0x000025F4 File Offset: 0x000007F4
		public Marker()
		{
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0007D975 File Offset: 0x0007BB75
		public Marker(Marker.Types type, Unit size, Style style)
		{
			this.Type = type;
			this.Size = size;
			this.Style = style;
		}

		// Token: 0x04000D36 RID: 3382
		[DefaultValue(Marker.Types.None)]
		public Marker.Types Type;

		// Token: 0x04000D37 RID: 3383
		[DefaultValue(typeof(Unit), "")]
		public Unit Size;

		// Token: 0x04000D38 RID: 3384
		public Style Style;

		// Token: 0x0200050D RID: 1293
		public enum Types
		{
			// Token: 0x04001243 RID: 4675
			None,
			// Token: 0x04001244 RID: 4676
			Auto,
			// Token: 0x04001245 RID: 4677
			Square,
			// Token: 0x04001246 RID: 4678
			Circle,
			// Token: 0x04001247 RID: 4679
			Diamond,
			// Token: 0x04001248 RID: 4680
			Triangle,
			// Token: 0x04001249 RID: 4681
			Cross
		}
	}
}
