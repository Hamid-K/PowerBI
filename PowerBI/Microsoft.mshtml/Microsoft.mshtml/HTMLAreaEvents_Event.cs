using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200095A RID: 2394
	[ComEventInterface(typeof(HTMLAreaEvents\u0000), typeof(HTMLAreaEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLAreaEvents_Event
	{
		// Token: 0x14001D7A RID: 7546
		// (add) Token: 0x0600F13F RID: 61759
		// (remove) Token: 0x0600F140 RID: 61760
		event HTMLAreaEvents_onhelpEventHandler onhelp;

		// Token: 0x14001D7B RID: 7547
		// (add) Token: 0x0600F141 RID: 61761
		// (remove) Token: 0x0600F142 RID: 61762
		event HTMLAreaEvents_onclickEventHandler onclick;

		// Token: 0x14001D7C RID: 7548
		// (add) Token: 0x0600F143 RID: 61763
		// (remove) Token: 0x0600F144 RID: 61764
		event HTMLAreaEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14001D7D RID: 7549
		// (add) Token: 0x0600F145 RID: 61765
		// (remove) Token: 0x0600F146 RID: 61766
		event HTMLAreaEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14001D7E RID: 7550
		// (add) Token: 0x0600F147 RID: 61767
		// (remove) Token: 0x0600F148 RID: 61768
		event HTMLAreaEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14001D7F RID: 7551
		// (add) Token: 0x0600F149 RID: 61769
		// (remove) Token: 0x0600F14A RID: 61770
		event HTMLAreaEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14001D80 RID: 7552
		// (add) Token: 0x0600F14B RID: 61771
		// (remove) Token: 0x0600F14C RID: 61772
		event HTMLAreaEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14001D81 RID: 7553
		// (add) Token: 0x0600F14D RID: 61773
		// (remove) Token: 0x0600F14E RID: 61774
		event HTMLAreaEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14001D82 RID: 7554
		// (add) Token: 0x0600F14F RID: 61775
		// (remove) Token: 0x0600F150 RID: 61776
		event HTMLAreaEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14001D83 RID: 7555
		// (add) Token: 0x0600F151 RID: 61777
		// (remove) Token: 0x0600F152 RID: 61778
		event HTMLAreaEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14001D84 RID: 7556
		// (add) Token: 0x0600F153 RID: 61779
		// (remove) Token: 0x0600F154 RID: 61780
		event HTMLAreaEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14001D85 RID: 7557
		// (add) Token: 0x0600F155 RID: 61781
		// (remove) Token: 0x0600F156 RID: 61782
		event HTMLAreaEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14001D86 RID: 7558
		// (add) Token: 0x0600F157 RID: 61783
		// (remove) Token: 0x0600F158 RID: 61784
		event HTMLAreaEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14001D87 RID: 7559
		// (add) Token: 0x0600F159 RID: 61785
		// (remove) Token: 0x0600F15A RID: 61786
		event HTMLAreaEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14001D88 RID: 7560
		// (add) Token: 0x0600F15B RID: 61787
		// (remove) Token: 0x0600F15C RID: 61788
		event HTMLAreaEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001D89 RID: 7561
		// (add) Token: 0x0600F15D RID: 61789
		// (remove) Token: 0x0600F15E RID: 61790
		event HTMLAreaEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14001D8A RID: 7562
		// (add) Token: 0x0600F15F RID: 61791
		// (remove) Token: 0x0600F160 RID: 61792
		event HTMLAreaEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001D8B RID: 7563
		// (add) Token: 0x0600F161 RID: 61793
		// (remove) Token: 0x0600F162 RID: 61794
		event HTMLAreaEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14001D8C RID: 7564
		// (add) Token: 0x0600F163 RID: 61795
		// (remove) Token: 0x0600F164 RID: 61796
		event HTMLAreaEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14001D8D RID: 7565
		// (add) Token: 0x0600F165 RID: 61797
		// (remove) Token: 0x0600F166 RID: 61798
		event HTMLAreaEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001D8E RID: 7566
		// (add) Token: 0x0600F167 RID: 61799
		// (remove) Token: 0x0600F168 RID: 61800
		event HTMLAreaEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001D8F RID: 7567
		// (add) Token: 0x0600F169 RID: 61801
		// (remove) Token: 0x0600F16A RID: 61802
		event HTMLAreaEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001D90 RID: 7568
		// (add) Token: 0x0600F16B RID: 61803
		// (remove) Token: 0x0600F16C RID: 61804
		event HTMLAreaEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14001D91 RID: 7569
		// (add) Token: 0x0600F16D RID: 61805
		// (remove) Token: 0x0600F16E RID: 61806
		event HTMLAreaEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14001D92 RID: 7570
		// (add) Token: 0x0600F16F RID: 61807
		// (remove) Token: 0x0600F170 RID: 61808
		event HTMLAreaEvents_onscrollEventHandler onscroll;

		// Token: 0x14001D93 RID: 7571
		// (add) Token: 0x0600F171 RID: 61809
		// (remove) Token: 0x0600F172 RID: 61810
		event HTMLAreaEvents_onfocusEventHandler onfocus;

		// Token: 0x14001D94 RID: 7572
		// (add) Token: 0x0600F173 RID: 61811
		// (remove) Token: 0x0600F174 RID: 61812
		event HTMLAreaEvents_onblurEventHandler onblur;

		// Token: 0x14001D95 RID: 7573
		// (add) Token: 0x0600F175 RID: 61813
		// (remove) Token: 0x0600F176 RID: 61814
		event HTMLAreaEvents_onresizeEventHandler onresize;

		// Token: 0x14001D96 RID: 7574
		// (add) Token: 0x0600F177 RID: 61815
		// (remove) Token: 0x0600F178 RID: 61816
		event HTMLAreaEvents_ondragEventHandler ondrag;

		// Token: 0x14001D97 RID: 7575
		// (add) Token: 0x0600F179 RID: 61817
		// (remove) Token: 0x0600F17A RID: 61818
		event HTMLAreaEvents_ondragendEventHandler ondragend;

		// Token: 0x14001D98 RID: 7576
		// (add) Token: 0x0600F17B RID: 61819
		// (remove) Token: 0x0600F17C RID: 61820
		event HTMLAreaEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14001D99 RID: 7577
		// (add) Token: 0x0600F17D RID: 61821
		// (remove) Token: 0x0600F17E RID: 61822
		event HTMLAreaEvents_ondragoverEventHandler ondragover;

		// Token: 0x14001D9A RID: 7578
		// (add) Token: 0x0600F17F RID: 61823
		// (remove) Token: 0x0600F180 RID: 61824
		event HTMLAreaEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14001D9B RID: 7579
		// (add) Token: 0x0600F181 RID: 61825
		// (remove) Token: 0x0600F182 RID: 61826
		event HTMLAreaEvents_ondropEventHandler ondrop;

		// Token: 0x14001D9C RID: 7580
		// (add) Token: 0x0600F183 RID: 61827
		// (remove) Token: 0x0600F184 RID: 61828
		event HTMLAreaEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14001D9D RID: 7581
		// (add) Token: 0x0600F185 RID: 61829
		// (remove) Token: 0x0600F186 RID: 61830
		event HTMLAreaEvents_oncutEventHandler oncut;

		// Token: 0x14001D9E RID: 7582
		// (add) Token: 0x0600F187 RID: 61831
		// (remove) Token: 0x0600F188 RID: 61832
		event HTMLAreaEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001D9F RID: 7583
		// (add) Token: 0x0600F189 RID: 61833
		// (remove) Token: 0x0600F18A RID: 61834
		event HTMLAreaEvents_oncopyEventHandler oncopy;

		// Token: 0x14001DA0 RID: 7584
		// (add) Token: 0x0600F18B RID: 61835
		// (remove) Token: 0x0600F18C RID: 61836
		event HTMLAreaEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14001DA1 RID: 7585
		// (add) Token: 0x0600F18D RID: 61837
		// (remove) Token: 0x0600F18E RID: 61838
		event HTMLAreaEvents_onpasteEventHandler onpaste;

		// Token: 0x14001DA2 RID: 7586
		// (add) Token: 0x0600F18F RID: 61839
		// (remove) Token: 0x0600F190 RID: 61840
		event HTMLAreaEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001DA3 RID: 7587
		// (add) Token: 0x0600F191 RID: 61841
		// (remove) Token: 0x0600F192 RID: 61842
		event HTMLAreaEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001DA4 RID: 7588
		// (add) Token: 0x0600F193 RID: 61843
		// (remove) Token: 0x0600F194 RID: 61844
		event HTMLAreaEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14001DA5 RID: 7589
		// (add) Token: 0x0600F195 RID: 61845
		// (remove) Token: 0x0600F196 RID: 61846
		event HTMLAreaEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14001DA6 RID: 7590
		// (add) Token: 0x0600F197 RID: 61847
		// (remove) Token: 0x0600F198 RID: 61848
		event HTMLAreaEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001DA7 RID: 7591
		// (add) Token: 0x0600F199 RID: 61849
		// (remove) Token: 0x0600F19A RID: 61850
		event HTMLAreaEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14001DA8 RID: 7592
		// (add) Token: 0x0600F19B RID: 61851
		// (remove) Token: 0x0600F19C RID: 61852
		event HTMLAreaEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14001DA9 RID: 7593
		// (add) Token: 0x0600F19D RID: 61853
		// (remove) Token: 0x0600F19E RID: 61854
		event HTMLAreaEvents_onpageEventHandler onpage;

		// Token: 0x14001DAA RID: 7594
		// (add) Token: 0x0600F19F RID: 61855
		// (remove) Token: 0x0600F1A0 RID: 61856
		event HTMLAreaEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14001DAB RID: 7595
		// (add) Token: 0x0600F1A1 RID: 61857
		// (remove) Token: 0x0600F1A2 RID: 61858
		event HTMLAreaEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001DAC RID: 7596
		// (add) Token: 0x0600F1A3 RID: 61859
		// (remove) Token: 0x0600F1A4 RID: 61860
		event HTMLAreaEvents_onmoveEventHandler onmove;

		// Token: 0x14001DAD RID: 7597
		// (add) Token: 0x0600F1A5 RID: 61861
		// (remove) Token: 0x0600F1A6 RID: 61862
		event HTMLAreaEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001DAE RID: 7598
		// (add) Token: 0x0600F1A7 RID: 61863
		// (remove) Token: 0x0600F1A8 RID: 61864
		event HTMLAreaEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14001DAF RID: 7599
		// (add) Token: 0x0600F1A9 RID: 61865
		// (remove) Token: 0x0600F1AA RID: 61866
		event HTMLAreaEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14001DB0 RID: 7600
		// (add) Token: 0x0600F1AB RID: 61867
		// (remove) Token: 0x0600F1AC RID: 61868
		event HTMLAreaEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14001DB1 RID: 7601
		// (add) Token: 0x0600F1AD RID: 61869
		// (remove) Token: 0x0600F1AE RID: 61870
		event HTMLAreaEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14001DB2 RID: 7602
		// (add) Token: 0x0600F1AF RID: 61871
		// (remove) Token: 0x0600F1B0 RID: 61872
		event HTMLAreaEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14001DB3 RID: 7603
		// (add) Token: 0x0600F1B1 RID: 61873
		// (remove) Token: 0x0600F1B2 RID: 61874
		event HTMLAreaEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14001DB4 RID: 7604
		// (add) Token: 0x0600F1B3 RID: 61875
		// (remove) Token: 0x0600F1B4 RID: 61876
		event HTMLAreaEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14001DB5 RID: 7605
		// (add) Token: 0x0600F1B5 RID: 61877
		// (remove) Token: 0x0600F1B6 RID: 61878
		event HTMLAreaEvents_onactivateEventHandler onactivate;

		// Token: 0x14001DB6 RID: 7606
		// (add) Token: 0x0600F1B7 RID: 61879
		// (remove) Token: 0x0600F1B8 RID: 61880
		event HTMLAreaEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14001DB7 RID: 7607
		// (add) Token: 0x0600F1B9 RID: 61881
		// (remove) Token: 0x0600F1BA RID: 61882
		event HTMLAreaEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14001DB8 RID: 7608
		// (add) Token: 0x0600F1BB RID: 61883
		// (remove) Token: 0x0600F1BC RID: 61884
		event HTMLAreaEvents_onfocusoutEventHandler onfocusout;
	}
}
