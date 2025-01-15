using System;
using System.Diagnostics;

namespace Microsoft.Data.OData
{
	// Token: 0x0200023A RID: 570
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataInnerError
	{
		// Token: 0x06001158 RID: 4440 RVA: 0x00041DD7 File Offset: 0x0003FFD7
		public ODataInnerError()
		{
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00041DE0 File Offset: 0x0003FFE0
		public ODataInnerError(Exception exception)
		{
			ExceptionUtils.CheckArgumentNotNull<Exception>(exception, "exception");
			this.Message = exception.Message ?? string.Empty;
			this.TypeName = exception.GetType().FullName;
			this.StackTrace = exception.StackTrace;
			if (exception.InnerException != null)
			{
				this.InnerError = new ODataInnerError(exception.InnerException);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00041E49 File Offset: 0x00040049
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x00041E51 File Offset: 0x00040051
		public string Message { get; set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00041E5A File Offset: 0x0004005A
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x00041E62 File Offset: 0x00040062
		public string TypeName { get; set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00041E6B File Offset: 0x0004006B
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x00041E73 File Offset: 0x00040073
		public string StackTrace { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00041E7C File Offset: 0x0004007C
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x00041E84 File Offset: 0x00040084
		public ODataInnerError InnerError { get; set; }
	}
}
