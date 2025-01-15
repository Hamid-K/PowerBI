using System;
using System.Globalization;

namespace Microsoft.OData
{
	// Token: 0x02000048 RID: 72
	public sealed class ODataErrorDetail
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00006537 File Offset: 0x00004737
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000653F File Offset: 0x0000473F
		public string ErrorCode { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00006548 File Offset: 0x00004748
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00006550 File Offset: 0x00004750
		public string Message { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00006559 File Offset: 0x00004759
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00006561 File Offset: 0x00004761
		public string Target { get; set; }

		// Token: 0x06000241 RID: 577 RVA: 0x0000656C File Offset: 0x0000476C
		internal string ToJson()
		{
			return string.Format(CultureInfo.InvariantCulture, "{{ \"errorcode\": \"{0}\", \"message\": \"{1}\", \"target\": \"{2}\" }}", new object[]
			{
				this.ErrorCode ?? "",
				this.Message ?? "",
				this.Target ?? ""
			});
		}
	}
}
