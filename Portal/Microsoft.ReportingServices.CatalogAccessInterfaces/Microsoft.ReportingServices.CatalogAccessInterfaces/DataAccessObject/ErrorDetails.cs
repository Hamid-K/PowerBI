using System;

namespace Microsoft.ReportingServices.CatalogAccess.DataAccessObject
{
	// Token: 0x02000021 RID: 33
	public sealed class ErrorDetails
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000028EE File Offset: 0x00000AEE
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000028F6 File Offset: 0x00000AF6
		public int ErrorCode { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000028FF File Offset: 0x00000AFF
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00002907 File Offset: 0x00000B07
		public string Message { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00002910 File Offset: 0x00000B10
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00002918 File Offset: 0x00000B18
		public string CallStack { get; set; }
	}
}
