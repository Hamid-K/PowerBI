using System;
using System.Diagnostics;

namespace Microsoft.InfoNav
{
	// Token: 0x0200001C RID: 28
	public interface ITracer
	{
		// Token: 0x060001AB RID: 427
		bool ShouldTrace(TraceLevel level);

		// Token: 0x060001AC RID: 428
		void TraceFatal(string message);

		// Token: 0x060001AD RID: 429
		void TraceFatal(string format, object arg0);

		// Token: 0x060001AE RID: 430
		void TraceFatal(string format, object arg0, object arg1);

		// Token: 0x060001AF RID: 431
		void TraceFatal(string format, object arg0, object arg1, object arg2);

		// Token: 0x060001B0 RID: 432
		void TraceFatal(string format, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x060001B1 RID: 433
		void TraceError(string message);

		// Token: 0x060001B2 RID: 434
		void TraceError(string format, object arg0);

		// Token: 0x060001B3 RID: 435
		void TraceError(string format, object arg0, object arg1);

		// Token: 0x060001B4 RID: 436
		void TraceError(string format, object arg0, object arg1, object arg2);

		// Token: 0x060001B5 RID: 437
		void TraceError(string format, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x060001B6 RID: 438
		void TraceWarning(string message);

		// Token: 0x060001B7 RID: 439
		void TraceWarning(string format, object arg0);

		// Token: 0x060001B8 RID: 440
		void TraceWarning(string format, object arg0, object arg1);

		// Token: 0x060001B9 RID: 441
		void TraceWarning(string format, object arg0, object arg1, object arg2);

		// Token: 0x060001BA RID: 442
		void TraceWarning(string format, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x060001BB RID: 443
		void TraceInformation(string message);

		// Token: 0x060001BC RID: 444
		void TraceInformation(string format, object arg0);

		// Token: 0x060001BD RID: 445
		void TraceInformation(string format, object arg0, object arg1);

		// Token: 0x060001BE RID: 446
		void TraceInformation(string format, object arg0, object arg1, object arg2);

		// Token: 0x060001BF RID: 447
		void TraceInformation(string format, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x060001C0 RID: 448
		void TraceVerbose(string message);

		// Token: 0x060001C1 RID: 449
		void TraceVerbose(string format, object arg0);

		// Token: 0x060001C2 RID: 450
		void TraceVerbose(string format, object arg0, object arg1);

		// Token: 0x060001C3 RID: 451
		void TraceVerbose(string format, object arg0, object arg1, object arg2);

		// Token: 0x060001C4 RID: 452
		void TraceVerbose(string format, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x060001C5 RID: 453
		void SanitizedTrace(TraceLevel level, string format, params string[] args);
	}
}
