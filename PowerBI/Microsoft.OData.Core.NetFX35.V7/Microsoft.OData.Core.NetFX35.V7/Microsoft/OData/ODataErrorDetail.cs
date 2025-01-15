using System;

namespace Microsoft.OData
{
	// Token: 0x0200001F RID: 31
	public sealed class ODataErrorDetail
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003F84 File Offset: 0x00002184
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003F8C File Offset: 0x0000218C
		public string ErrorCode { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003F95 File Offset: 0x00002195
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003F9D File Offset: 0x0000219D
		public string Message { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003FA6 File Offset: 0x000021A6
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003FAE File Offset: 0x000021AE
		public string Target { get; set; }
	}
}
