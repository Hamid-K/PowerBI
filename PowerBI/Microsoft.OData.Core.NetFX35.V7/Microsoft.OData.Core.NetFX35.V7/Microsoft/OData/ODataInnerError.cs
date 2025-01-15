using System;
using System.Diagnostics;

namespace Microsoft.OData
{
	// Token: 0x02000068 RID: 104
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataInnerError
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00002CFE File Offset: 0x00000EFE
		public ODataInnerError()
		{
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000A3B0 File Offset: 0x000085B0
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000A41A File Offset: 0x0000861A
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000A422 File Offset: 0x00008622
		public string Message { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000A42B File Offset: 0x0000862B
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000A433 File Offset: 0x00008633
		public string TypeName { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000A43C File Offset: 0x0000863C
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000A444 File Offset: 0x00008644
		public string StackTrace { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000A44D File Offset: 0x0000864D
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000A455 File Offset: 0x00008655
		public ODataInnerError InnerError { get; set; }
	}
}
