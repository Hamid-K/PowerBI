using System;
using System.Diagnostics;

namespace Microsoft.OData.Core
{
	// Token: 0x0200017A RID: 378
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataInnerError
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x0003196E File Offset: 0x0002FB6E
		public ODataInnerError()
		{
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00031978 File Offset: 0x0002FB78
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

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x000319E1 File Offset: 0x0002FBE1
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x000319E9 File Offset: 0x0002FBE9
		public string Message { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x000319F2 File Offset: 0x0002FBF2
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x000319FA File Offset: 0x0002FBFA
		public string TypeName { get; set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00031A03 File Offset: 0x0002FC03
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x00031A0B File Offset: 0x0002FC0B
		public string StackTrace { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00031A14 File Offset: 0x0002FC14
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x00031A1C File Offset: 0x0002FC1C
		public ODataInnerError InnerError { get; set; }
	}
}
