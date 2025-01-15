using System;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x02000702 RID: 1794
	public enum XaReturnCode
	{
		// Token: 0x04002119 RID: 8473
		Ok,
		// Token: 0x0400211A RID: 8474
		Rollback = 100,
		// Token: 0x0400211B RID: 8475
		RollbackCommunicationFailure,
		// Token: 0x0400211C RID: 8476
		RollbackDeadlock,
		// Token: 0x0400211D RID: 8477
		RollbackIntegrity,
		// Token: 0x0400211E RID: 8478
		RollbackOther,
		// Token: 0x0400211F RID: 8479
		RollbackProtocol,
		// Token: 0x04002120 RID: 8480
		RollbackTimeout,
		// Token: 0x04002121 RID: 8481
		RollbackTransient,
		// Token: 0x04002122 RID: 8482
		CommitedSinglePhase = 1000,
		// Token: 0x04002123 RID: 8483
		NoMigrate = 9,
		// Token: 0x04002124 RID: 8484
		HeuristicHazard = 8,
		// Token: 0x04002125 RID: 8485
		HeuristicCommit = 7,
		// Token: 0x04002126 RID: 8486
		HeuristicRollback = 6,
		// Token: 0x04002127 RID: 8487
		HeuristicMix = 5,
		// Token: 0x04002128 RID: 8488
		Retry = 4,
		// Token: 0x04002129 RID: 8489
		ReadOnly = 3,
		// Token: 0x0400212A RID: 8490
		OutstandingAsynchronousOperation = -2,
		// Token: 0x0400212B RID: 8491
		ResourceManagerError = -3,
		// Token: 0x0400212C RID: 8492
		InvalidXid = -4,
		// Token: 0x0400212D RID: 8493
		InvalidArguments = -5,
		// Token: 0x0400212E RID: 8494
		Protocol = -6,
		// Token: 0x0400212F RID: 8495
		ResourceManagerUnavailable = -7,
		// Token: 0x04002130 RID: 8496
		DuplicateXid = -8,
		// Token: 0x04002131 RID: 8497
		WorkOutsideTransaction = -9
	}
}
