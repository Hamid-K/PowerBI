using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B32 RID: 2866
	[ComEventInterface(typeof(HTMLObjectElementEvents2\u0000), typeof(HTMLObjectElementEvents2_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLObjectElementEvents2_Event
	{
		// Token: 0x14002407 RID: 9223
		// (add) Token: 0x06012B9F RID: 76703
		// (remove) Token: 0x06012BA0 RID: 76704
		event HTMLObjectElementEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14002408 RID: 9224
		// (add) Token: 0x06012BA1 RID: 76705
		// (remove) Token: 0x06012BA2 RID: 76706
		event HTMLObjectElementEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x14002409 RID: 9225
		// (add) Token: 0x06012BA3 RID: 76707
		// (remove) Token: 0x06012BA4 RID: 76708
		event HTMLObjectElementEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x1400240A RID: 9226
		// (add) Token: 0x06012BA5 RID: 76709
		// (remove) Token: 0x06012BA6 RID: 76710
		event HTMLObjectElementEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x1400240B RID: 9227
		// (add) Token: 0x06012BA7 RID: 76711
		// (remove) Token: 0x06012BA8 RID: 76712
		event HTMLObjectElementEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x1400240C RID: 9228
		// (add) Token: 0x06012BA9 RID: 76713
		// (remove) Token: 0x06012BAA RID: 76714
		event HTMLObjectElementEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x1400240D RID: 9229
		// (add) Token: 0x06012BAB RID: 76715
		// (remove) Token: 0x06012BAC RID: 76716
		event HTMLObjectElementEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x1400240E RID: 9230
		// (add) Token: 0x06012BAD RID: 76717
		// (remove) Token: 0x06012BAE RID: 76718
		event HTMLObjectElementEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x1400240F RID: 9231
		// (add) Token: 0x06012BAF RID: 76719
		// (remove) Token: 0x06012BB0 RID: 76720
		event HTMLObjectElementEvents2_onerrorEventHandler onerror;

		// Token: 0x14002410 RID: 9232
		// (add) Token: 0x06012BB1 RID: 76721
		// (remove) Token: 0x06012BB2 RID: 76722
		event HTMLObjectElementEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14002411 RID: 9233
		// (add) Token: 0x06012BB3 RID: 76723
		// (remove) Token: 0x06012BB4 RID: 76724
		event HTMLObjectElementEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14002412 RID: 9234
		// (add) Token: 0x06012BB5 RID: 76725
		// (remove) Token: 0x06012BB6 RID: 76726
		event HTMLObjectElementEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x14002413 RID: 9235
		// (add) Token: 0x06012BB7 RID: 76727
		// (remove) Token: 0x06012BB8 RID: 76728
		event HTMLObjectElementEvents2_onreadystatechangeEventHandler onreadystatechange;
	}
}
