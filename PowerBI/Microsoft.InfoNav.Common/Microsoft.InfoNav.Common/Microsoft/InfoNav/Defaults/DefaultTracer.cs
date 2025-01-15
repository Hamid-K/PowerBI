using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.InfoNav.Defaults
{
	// Token: 0x0200003C RID: 60
	[ImmutableObject(true)]
	internal sealed class DefaultTracer : ITracer
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x000082DF File Offset: 0x000064DF
		private DefaultTracer()
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000082E7 File Offset: 0x000064E7
		public bool ShouldTrace(TraceLevel level)
		{
			return false;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000082EA File Offset: 0x000064EA
		public void TraceFatal(string message)
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000082EC File Offset: 0x000064EC
		public void TraceFatal(string format, object arg0)
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000082EE File Offset: 0x000064EE
		public void TraceFatal(string format, object arg0, object arg1)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000082F0 File Offset: 0x000064F0
		public void TraceFatal(string format, object arg0, object arg1, object arg2)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000082F2 File Offset: 0x000064F2
		public void TraceFatal(string format, object arg0, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000082F4 File Offset: 0x000064F4
		public void TraceError(string message)
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000082F6 File Offset: 0x000064F6
		public void TraceError(string format, object arg0)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000082F8 File Offset: 0x000064F8
		public void TraceError(string format, object arg0, object arg1)
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000082FA File Offset: 0x000064FA
		public void TraceError(string format, object arg0, object arg1, object arg2)
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000082FC File Offset: 0x000064FC
		public void TraceError(string format, object arg0, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000082FE File Offset: 0x000064FE
		public void TraceWarning(string message)
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008300 File Offset: 0x00006500
		public void TraceWarning(string format, object arg0)
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008302 File Offset: 0x00006502
		public void TraceWarning(string format, object arg0, object arg1)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00008304 File Offset: 0x00006504
		public void TraceWarning(string format, object arg0, object arg1, object arg2)
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008306 File Offset: 0x00006506
		public void TraceWarning(string format, object arg0, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008308 File Offset: 0x00006508
		public void TraceInformation(string message)
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000830A File Offset: 0x0000650A
		public void TraceInformation(string format, object arg0)
		{
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000830C File Offset: 0x0000650C
		public void TraceInformation(string format, object arg0, object arg1)
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000830E File Offset: 0x0000650E
		public void TraceInformation(string format, object arg0, object arg1, object arg2)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00008310 File Offset: 0x00006510
		public void TraceInformation(string format, object arg0, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008312 File Offset: 0x00006512
		public void TraceVerbose(string message)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008314 File Offset: 0x00006514
		public void TraceVerbose(string format, object arg0)
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008316 File Offset: 0x00006516
		public void TraceVerbose(string format, object arg0, object arg1)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008318 File Offset: 0x00006518
		public void TraceVerbose(string format, object arg0, object arg1, object arg2)
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000831A File Offset: 0x0000651A
		public void TraceVerbose(string format, object arg0, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000831C File Offset: 0x0000651C
		public void SanitizedTrace(TraceLevel level, string format, params string[] args)
		{
		}

		// Token: 0x04000098 RID: 152
		internal static readonly DefaultTracer Instance = new DefaultTracer();
	}
}
