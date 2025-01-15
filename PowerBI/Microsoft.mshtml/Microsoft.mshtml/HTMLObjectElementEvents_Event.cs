using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B24 RID: 2852
	[ComEventInterface(typeof(HTMLObjectElementEvents\u0000), typeof(HTMLObjectElementEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLObjectElementEvents_Event
	{
		// Token: 0x140023FA RID: 9210
		// (add) Token: 0x06012B6B RID: 76651
		// (remove) Token: 0x06012B6C RID: 76652
		event HTMLObjectElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x140023FB RID: 9211
		// (add) Token: 0x06012B6D RID: 76653
		// (remove) Token: 0x06012B6E RID: 76654
		event HTMLObjectElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x140023FC RID: 9212
		// (add) Token: 0x06012B6F RID: 76655
		// (remove) Token: 0x06012B70 RID: 76656
		event HTMLObjectElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x140023FD RID: 9213
		// (add) Token: 0x06012B71 RID: 76657
		// (remove) Token: 0x06012B72 RID: 76658
		event HTMLObjectElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x140023FE RID: 9214
		// (add) Token: 0x06012B73 RID: 76659
		// (remove) Token: 0x06012B74 RID: 76660
		event HTMLObjectElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x140023FF RID: 9215
		// (add) Token: 0x06012B75 RID: 76661
		// (remove) Token: 0x06012B76 RID: 76662
		event HTMLObjectElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14002400 RID: 9216
		// (add) Token: 0x06012B77 RID: 76663
		// (remove) Token: 0x06012B78 RID: 76664
		event HTMLObjectElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14002401 RID: 9217
		// (add) Token: 0x06012B79 RID: 76665
		// (remove) Token: 0x06012B7A RID: 76666
		event HTMLObjectElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14002402 RID: 9218
		// (add) Token: 0x06012B7B RID: 76667
		// (remove) Token: 0x06012B7C RID: 76668
		event HTMLObjectElementEvents_onerrorEventHandler onerror;

		// Token: 0x14002403 RID: 9219
		// (add) Token: 0x06012B7D RID: 76669
		// (remove) Token: 0x06012B7E RID: 76670
		event HTMLObjectElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14002404 RID: 9220
		// (add) Token: 0x06012B7F RID: 76671
		// (remove) Token: 0x06012B80 RID: 76672
		event HTMLObjectElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14002405 RID: 9221
		// (add) Token: 0x06012B81 RID: 76673
		// (remove) Token: 0x06012B82 RID: 76674
		event HTMLObjectElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14002406 RID: 9222
		// (add) Token: 0x06012B83 RID: 76675
		// (remove) Token: 0x06012B84 RID: 76676
		event HTMLObjectElementEvents_onreadystatechangeEventHandler onreadystatechange;
	}
}
