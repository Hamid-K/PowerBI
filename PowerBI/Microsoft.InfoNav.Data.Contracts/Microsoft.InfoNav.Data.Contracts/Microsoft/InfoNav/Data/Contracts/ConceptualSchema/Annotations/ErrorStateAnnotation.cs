using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000130 RID: 304
	public sealed class ErrorStateAnnotation
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00010716 File Offset: 0x0000E916
		public static ErrorStateAnnotation True { get; } = new ErrorStateAnnotation(true);

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001071D File Offset: 0x0000E91D
		public static ErrorStateAnnotation False { get; } = new ErrorStateAnnotation(false);

		// Token: 0x060007F1 RID: 2033 RVA: 0x00010724 File Offset: 0x0000E924
		private ErrorStateAnnotation(bool isError)
		{
			this.IsError = isError;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00010733 File Offset: 0x0000E933
		public bool IsError { get; }
	}
}
