using System;

namespace Microsoft.OData
{
	// Token: 0x02000099 RID: 153
	public abstract class ODataServiceDocumentElement : ODataAnnotatable
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000FF4E File Offset: 0x0000E14E
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000FF56 File Offset: 0x0000E156
		public Uri Url { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000FF5F File Offset: 0x0000E15F
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0000FF67 File Offset: 0x0000E167
		public string Name { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000FF70 File Offset: 0x0000E170
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0000FF78 File Offset: 0x0000E178
		public string Title { get; set; }
	}
}
