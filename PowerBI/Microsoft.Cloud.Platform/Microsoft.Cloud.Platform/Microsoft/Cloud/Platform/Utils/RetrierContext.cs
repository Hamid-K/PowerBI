using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000236 RID: 566
	public class RetrierContext
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00033176 File Offset: 0x00031376
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x0003317E File Offset: 0x0003137E
		public int CurrentAttemptNumber { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00033187 File Offset: 0x00031387
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x0003318F File Offset: 0x0003138F
		public int MaxAttemptsNumber { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00033198 File Offset: 0x00031398
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x000331A0 File Offset: 0x000313A0
		public int NonFailureRetries { get; set; }

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000331A9 File Offset: 0x000313A9
		public RetrierContext(int maxRetry)
		{
			this.CurrentAttemptNumber = 0;
			this.NonFailureRetries = 0;
			this.MaxAttemptsNumber = maxRetry;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000331C6 File Offset: 0x000313C6
		public RetrierContext(RetrierContext other)
		{
			this.CurrentAttemptNumber = other.CurrentAttemptNumber;
			this.NonFailureRetries = other.NonFailureRetries;
			this.MaxAttemptsNumber = other.MaxAttemptsNumber;
		}
	}
}
