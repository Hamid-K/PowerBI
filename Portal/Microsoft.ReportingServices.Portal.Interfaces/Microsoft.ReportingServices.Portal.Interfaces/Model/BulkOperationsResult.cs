using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	// Token: 0x02000004 RID: 4
	public sealed class BulkOperationsResult
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static BulkOperationsResult Ok()
		{
			return new BulkOperationsResult(Enumerable.Empty<string>());
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		public static BulkOperationsResult WithFailures(IEnumerable<string> failedOperations)
		{
			return new BulkOperationsResult(failedOperations);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		private BulkOperationsResult(IEnumerable<string> failedOperations)
		{
			this.FailedOperations = failedOperations;
			this.HasErrors = failedOperations != null && failedOperations.Any<string>();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002085 File Offset: 0x00000285
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000208D File Offset: 0x0000028D
		public IEnumerable<string> FailedOperations { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002096 File Offset: 0x00000296
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000209E File Offset: 0x0000029E
		public bool HasErrors { get; set; }
	}
}
