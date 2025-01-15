using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Design
{
	// Token: 0x0200038E RID: 910
	public class GeneralValidationResult : ValidationResult
	{
		// Token: 0x06001E14 RID: 7700 RVA: 0x0007B120 File Offset: 0x00079320
		public GeneralValidationResult(object obj, bool isError, string message)
			: base(obj, isError)
		{
			if (message == null)
			{
				throw new ArgumentNullException();
			}
			this.Message = message;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0007B13A File Offset: 0x0007933A
		public override string ToString()
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("{0}: {1}", new object[]
			{
				this.Object.ToString(),
				this.Message
			});
		}

		// Token: 0x04000CBF RID: 3263
		public readonly string Message;
	}
}
