using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200027D RID: 637
	internal abstract class ClientRequest
	{
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060016A4 RID: 5796
		public abstract string SessionID { get; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060016A5 RID: 5797
		public abstract bool IsNew { get; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060016A6 RID: 5798
		// (set) Token: 0x060016A7 RID: 5799
		public abstract bool NeedSession { get; set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060016A8 RID: 5800
		// (set) Token: 0x060016A9 RID: 5801
		public abstract bool RedirectRequired { get; set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060016AA RID: 5802
		// (set) Token: 0x060016AB RID: 5803
		public abstract bool DontCache { get; set; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060016AC RID: 5804
		// (set) Token: 0x060016AD RID: 5805
		public abstract SessionReportItem SessionReport { get; set; }
	}
}
