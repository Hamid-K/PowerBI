using System;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x0200314A RID: 12618
	internal class ValidationErrorEventArgs : EventArgs
	{
		// Token: 0x0601B5B6 RID: 112054 RVA: 0x00376B22 File Offset: 0x00374D22
		internal ValidationErrorEventArgs(ValidationErrorInfo validatoinError)
		{
			this.ValidationErrorInfo = validatoinError;
		}

		// Token: 0x170099AC RID: 39340
		// (get) Token: 0x0601B5B7 RID: 112055 RVA: 0x00376B31 File Offset: 0x00374D31
		// (set) Token: 0x0601B5B8 RID: 112056 RVA: 0x00376B39 File Offset: 0x00374D39
		internal ValidationErrorInfo ValidationErrorInfo { get; set; }
	}
}
