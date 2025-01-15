using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000038 RID: 56
	public enum ReportOperation
	{
		// Token: 0x04000199 RID: 409
		Delete,
		// Token: 0x0400019A RID: 410
		ExecuteAndView,
		// Token: 0x0400019B RID: 411
		ReadProperties,
		// Token: 0x0400019C RID: 412
		UpdateProperties,
		// Token: 0x0400019D RID: 413
		UpdateParameters,
		// Token: 0x0400019E RID: 414
		ReadDatasource,
		// Token: 0x0400019F RID: 415
		UpdateDatasource,
		// Token: 0x040001A0 RID: 416
		ReadReportDefinition,
		// Token: 0x040001A1 RID: 417
		UpdateReportDefinition,
		// Token: 0x040001A2 RID: 418
		CreateSubscription,
		// Token: 0x040001A3 RID: 419
		DeleteSubscription,
		// Token: 0x040001A4 RID: 420
		ReadSubscription,
		// Token: 0x040001A5 RID: 421
		ReadAuthorizationPolicy,
		// Token: 0x040001A6 RID: 422
		UpdateDeleteAuthorizationPolicy,
		// Token: 0x040001A7 RID: 423
		UpdateSubscription,
		// Token: 0x040001A8 RID: 424
		CreateAnySubscription,
		// Token: 0x040001A9 RID: 425
		DeleteAnySubscription,
		// Token: 0x040001AA RID: 426
		ReadAnySubscription,
		// Token: 0x040001AB RID: 427
		UpdateAnySubscription,
		// Token: 0x040001AC RID: 428
		UpdatePolicy,
		// Token: 0x040001AD RID: 429
		ReadPolicy,
		// Token: 0x040001AE RID: 430
		DeleteHistory,
		// Token: 0x040001AF RID: 431
		ListHistory,
		// Token: 0x040001B0 RID: 432
		CreateResource,
		// Token: 0x040001B1 RID: 433
		CreateSnapshot,
		// Token: 0x040001B2 RID: 434
		Execute,
		// Token: 0x040001B3 RID: 435
		CreateLink,
		// Token: 0x040001B4 RID: 436
		Comment,
		// Token: 0x040001B5 RID: 437
		ManageComments
	}
}
