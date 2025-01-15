using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007E7 RID: 2023
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLDocumentEvents\u0000), typeof(HTMLDocumentEvents_EventProvider\u0000))]
	public interface HTMLDocumentEvents_Event
	{
		// Token: 0x14001AA9 RID: 6825
		// (add) Token: 0x0600DDAB RID: 56747
		// (remove) Token: 0x0600DDAC RID: 56748
		event HTMLDocumentEvents_onhelpEventHandler onhelp;

		// Token: 0x14001AAA RID: 6826
		// (add) Token: 0x0600DDAD RID: 56749
		// (remove) Token: 0x0600DDAE RID: 56750
		event HTMLDocumentEvents_onclickEventHandler onclick;

		// Token: 0x14001AAB RID: 6827
		// (add) Token: 0x0600DDAF RID: 56751
		// (remove) Token: 0x0600DDB0 RID: 56752
		event HTMLDocumentEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14001AAC RID: 6828
		// (add) Token: 0x0600DDB1 RID: 56753
		// (remove) Token: 0x0600DDB2 RID: 56754
		event HTMLDocumentEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14001AAD RID: 6829
		// (add) Token: 0x0600DDB3 RID: 56755
		// (remove) Token: 0x0600DDB4 RID: 56756
		event HTMLDocumentEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14001AAE RID: 6830
		// (add) Token: 0x0600DDB5 RID: 56757
		// (remove) Token: 0x0600DDB6 RID: 56758
		event HTMLDocumentEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14001AAF RID: 6831
		// (add) Token: 0x0600DDB7 RID: 56759
		// (remove) Token: 0x0600DDB8 RID: 56760
		event HTMLDocumentEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14001AB0 RID: 6832
		// (add) Token: 0x0600DDB9 RID: 56761
		// (remove) Token: 0x0600DDBA RID: 56762
		event HTMLDocumentEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14001AB1 RID: 6833
		// (add) Token: 0x0600DDBB RID: 56763
		// (remove) Token: 0x0600DDBC RID: 56764
		event HTMLDocumentEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14001AB2 RID: 6834
		// (add) Token: 0x0600DDBD RID: 56765
		// (remove) Token: 0x0600DDBE RID: 56766
		event HTMLDocumentEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14001AB3 RID: 6835
		// (add) Token: 0x0600DDBF RID: 56767
		// (remove) Token: 0x0600DDC0 RID: 56768
		event HTMLDocumentEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14001AB4 RID: 6836
		// (add) Token: 0x0600DDC1 RID: 56769
		// (remove) Token: 0x0600DDC2 RID: 56770
		event HTMLDocumentEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001AB5 RID: 6837
		// (add) Token: 0x0600DDC3 RID: 56771
		// (remove) Token: 0x0600DDC4 RID: 56772
		event HTMLDocumentEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001AB6 RID: 6838
		// (add) Token: 0x0600DDC5 RID: 56773
		// (remove) Token: 0x0600DDC6 RID: 56774
		event HTMLDocumentEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14001AB7 RID: 6839
		// (add) Token: 0x0600DDC7 RID: 56775
		// (remove) Token: 0x0600DDC8 RID: 56776
		event HTMLDocumentEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14001AB8 RID: 6840
		// (add) Token: 0x0600DDC9 RID: 56777
		// (remove) Token: 0x0600DDCA RID: 56778
		event HTMLDocumentEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14001AB9 RID: 6841
		// (add) Token: 0x0600DDCB RID: 56779
		// (remove) Token: 0x0600DDCC RID: 56780
		event HTMLDocumentEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14001ABA RID: 6842
		// (add) Token: 0x0600DDCD RID: 56781
		// (remove) Token: 0x0600DDCE RID: 56782
		event HTMLDocumentEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14001ABB RID: 6843
		// (add) Token: 0x0600DDCF RID: 56783
		// (remove) Token: 0x0600DDD0 RID: 56784
		event HTMLDocumentEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001ABC RID: 6844
		// (add) Token: 0x0600DDD1 RID: 56785
		// (remove) Token: 0x0600DDD2 RID: 56786
		event HTMLDocumentEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001ABD RID: 6845
		// (add) Token: 0x0600DDD3 RID: 56787
		// (remove) Token: 0x0600DDD4 RID: 56788
		event HTMLDocumentEvents_onstopEventHandler onstop;

		// Token: 0x14001ABE RID: 6846
		// (add) Token: 0x0600DDD5 RID: 56789
		// (remove) Token: 0x0600DDD6 RID: 56790
		event HTMLDocumentEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001ABF RID: 6847
		// (add) Token: 0x0600DDD7 RID: 56791
		// (remove) Token: 0x0600DDD8 RID: 56792
		event HTMLDocumentEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14001AC0 RID: 6848
		// (add) Token: 0x0600DDD9 RID: 56793
		// (remove) Token: 0x0600DDDA RID: 56794
		event HTMLDocumentEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14001AC1 RID: 6849
		// (add) Token: 0x0600DDDB RID: 56795
		// (remove) Token: 0x0600DDDC RID: 56796
		event HTMLDocumentEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14001AC2 RID: 6850
		// (add) Token: 0x0600DDDD RID: 56797
		// (remove) Token: 0x0600DDDE RID: 56798
		event HTMLDocumentEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001AC3 RID: 6851
		// (add) Token: 0x0600DDDF RID: 56799
		// (remove) Token: 0x0600DDE0 RID: 56800
		event HTMLDocumentEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001AC4 RID: 6852
		// (add) Token: 0x0600DDE1 RID: 56801
		// (remove) Token: 0x0600DDE2 RID: 56802
		event HTMLDocumentEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001AC5 RID: 6853
		// (add) Token: 0x0600DDE3 RID: 56803
		// (remove) Token: 0x0600DDE4 RID: 56804
		event HTMLDocumentEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14001AC6 RID: 6854
		// (add) Token: 0x0600DDE5 RID: 56805
		// (remove) Token: 0x0600DDE6 RID: 56806
		event HTMLDocumentEvents_onselectionchangeEventHandler onselectionchange;

		// Token: 0x14001AC7 RID: 6855
		// (add) Token: 0x0600DDE7 RID: 56807
		// (remove) Token: 0x0600DDE8 RID: 56808
		event HTMLDocumentEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001AC8 RID: 6856
		// (add) Token: 0x0600DDE9 RID: 56809
		// (remove) Token: 0x0600DDEA RID: 56810
		event HTMLDocumentEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14001AC9 RID: 6857
		// (add) Token: 0x0600DDEB RID: 56811
		// (remove) Token: 0x0600DDEC RID: 56812
		event HTMLDocumentEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14001ACA RID: 6858
		// (add) Token: 0x0600DDED RID: 56813
		// (remove) Token: 0x0600DDEE RID: 56814
		event HTMLDocumentEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14001ACB RID: 6859
		// (add) Token: 0x0600DDEF RID: 56815
		// (remove) Token: 0x0600DDF0 RID: 56816
		event HTMLDocumentEvents_onactivateEventHandler onactivate;

		// Token: 0x14001ACC RID: 6860
		// (add) Token: 0x0600DDF1 RID: 56817
		// (remove) Token: 0x0600DDF2 RID: 56818
		event HTMLDocumentEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14001ACD RID: 6861
		// (add) Token: 0x0600DDF3 RID: 56819
		// (remove) Token: 0x0600DDF4 RID: 56820
		event HTMLDocumentEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001ACE RID: 6862
		// (add) Token: 0x0600DDF5 RID: 56821
		// (remove) Token: 0x0600DDF6 RID: 56822
		event HTMLDocumentEvents_onbeforedeactivateEventHandler onbeforedeactivate;
	}
}
