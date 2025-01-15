using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200011F RID: 287
	public sealed class ODataErrorDetail
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000275B5 File Offset: 0x000257B5
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x000275BD File Offset: 0x000257BD
		public string ErrorCode { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000275C6 File Offset: 0x000257C6
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x000275CE File Offset: 0x000257CE
		public string Message { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000275D7 File Offset: 0x000257D7
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x000275DF File Offset: 0x000257DF
		public string Target { get; set; }
	}
}
