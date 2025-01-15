using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000653 RID: 1619
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLButtonElementEvents\u0000), typeof(HTMLButtonElementEvents_EventProvider\u0000))]
	public interface HTMLButtonElementEvents_Event
	{
		// Token: 0x1400121F RID: 4639
		// (add) Token: 0x06009767 RID: 38759
		// (remove) Token: 0x06009768 RID: 38760
		event HTMLButtonElementEvents_onhelpEventHandler onhelp;

		// Token: 0x14001220 RID: 4640
		// (add) Token: 0x06009769 RID: 38761
		// (remove) Token: 0x0600976A RID: 38762
		event HTMLButtonElementEvents_onclickEventHandler onclick;

		// Token: 0x14001221 RID: 4641
		// (add) Token: 0x0600976B RID: 38763
		// (remove) Token: 0x0600976C RID: 38764
		event HTMLButtonElementEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14001222 RID: 4642
		// (add) Token: 0x0600976D RID: 38765
		// (remove) Token: 0x0600976E RID: 38766
		event HTMLButtonElementEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14001223 RID: 4643
		// (add) Token: 0x0600976F RID: 38767
		// (remove) Token: 0x06009770 RID: 38768
		event HTMLButtonElementEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14001224 RID: 4644
		// (add) Token: 0x06009771 RID: 38769
		// (remove) Token: 0x06009772 RID: 38770
		event HTMLButtonElementEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14001225 RID: 4645
		// (add) Token: 0x06009773 RID: 38771
		// (remove) Token: 0x06009774 RID: 38772
		event HTMLButtonElementEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14001226 RID: 4646
		// (add) Token: 0x06009775 RID: 38773
		// (remove) Token: 0x06009776 RID: 38774
		event HTMLButtonElementEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14001227 RID: 4647
		// (add) Token: 0x06009777 RID: 38775
		// (remove) Token: 0x06009778 RID: 38776
		event HTMLButtonElementEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14001228 RID: 4648
		// (add) Token: 0x06009779 RID: 38777
		// (remove) Token: 0x0600977A RID: 38778
		event HTMLButtonElementEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14001229 RID: 4649
		// (add) Token: 0x0600977B RID: 38779
		// (remove) Token: 0x0600977C RID: 38780
		event HTMLButtonElementEvents_onmouseupEventHandler onmouseup;

		// Token: 0x1400122A RID: 4650
		// (add) Token: 0x0600977D RID: 38781
		// (remove) Token: 0x0600977E RID: 38782
		event HTMLButtonElementEvents_onselectstartEventHandler onselectstart;

		// Token: 0x1400122B RID: 4651
		// (add) Token: 0x0600977F RID: 38783
		// (remove) Token: 0x06009780 RID: 38784
		event HTMLButtonElementEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x1400122C RID: 4652
		// (add) Token: 0x06009781 RID: 38785
		// (remove) Token: 0x06009782 RID: 38786
		event HTMLButtonElementEvents_ondragstartEventHandler ondragstart;

		// Token: 0x1400122D RID: 4653
		// (add) Token: 0x06009783 RID: 38787
		// (remove) Token: 0x06009784 RID: 38788
		event HTMLButtonElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x1400122E RID: 4654
		// (add) Token: 0x06009785 RID: 38789
		// (remove) Token: 0x06009786 RID: 38790
		event HTMLButtonElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x1400122F RID: 4655
		// (add) Token: 0x06009787 RID: 38791
		// (remove) Token: 0x06009788 RID: 38792
		event HTMLButtonElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001230 RID: 4656
		// (add) Token: 0x06009789 RID: 38793
		// (remove) Token: 0x0600978A RID: 38794
		event HTMLButtonElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14001231 RID: 4657
		// (add) Token: 0x0600978B RID: 38795
		// (remove) Token: 0x0600978C RID: 38796
		event HTMLButtonElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14001232 RID: 4658
		// (add) Token: 0x0600978D RID: 38797
		// (remove) Token: 0x0600978E RID: 38798
		event HTMLButtonElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001233 RID: 4659
		// (add) Token: 0x0600978F RID: 38799
		// (remove) Token: 0x06009790 RID: 38800
		event HTMLButtonElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001234 RID: 4660
		// (add) Token: 0x06009791 RID: 38801
		// (remove) Token: 0x06009792 RID: 38802
		event HTMLButtonElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001235 RID: 4661
		// (add) Token: 0x06009793 RID: 38803
		// (remove) Token: 0x06009794 RID: 38804
		event HTMLButtonElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14001236 RID: 4662
		// (add) Token: 0x06009795 RID: 38805
		// (remove) Token: 0x06009796 RID: 38806
		event HTMLButtonElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14001237 RID: 4663
		// (add) Token: 0x06009797 RID: 38807
		// (remove) Token: 0x06009798 RID: 38808
		event HTMLButtonElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14001238 RID: 4664
		// (add) Token: 0x06009799 RID: 38809
		// (remove) Token: 0x0600979A RID: 38810
		event HTMLButtonElementEvents_onfocusEventHandler onfocus;

		// Token: 0x14001239 RID: 4665
		// (add) Token: 0x0600979B RID: 38811
		// (remove) Token: 0x0600979C RID: 38812
		event HTMLButtonElementEvents_onblurEventHandler onblur;

		// Token: 0x1400123A RID: 4666
		// (add) Token: 0x0600979D RID: 38813
		// (remove) Token: 0x0600979E RID: 38814
		event HTMLButtonElementEvents_onresizeEventHandler onresize;

		// Token: 0x1400123B RID: 4667
		// (add) Token: 0x0600979F RID: 38815
		// (remove) Token: 0x060097A0 RID: 38816
		event HTMLButtonElementEvents_ondragEventHandler ondrag;

		// Token: 0x1400123C RID: 4668
		// (add) Token: 0x060097A1 RID: 38817
		// (remove) Token: 0x060097A2 RID: 38818
		event HTMLButtonElementEvents_ondragendEventHandler ondragend;

		// Token: 0x1400123D RID: 4669
		// (add) Token: 0x060097A3 RID: 38819
		// (remove) Token: 0x060097A4 RID: 38820
		event HTMLButtonElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x1400123E RID: 4670
		// (add) Token: 0x060097A5 RID: 38821
		// (remove) Token: 0x060097A6 RID: 38822
		event HTMLButtonElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x1400123F RID: 4671
		// (add) Token: 0x060097A7 RID: 38823
		// (remove) Token: 0x060097A8 RID: 38824
		event HTMLButtonElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14001240 RID: 4672
		// (add) Token: 0x060097A9 RID: 38825
		// (remove) Token: 0x060097AA RID: 38826
		event HTMLButtonElementEvents_ondropEventHandler ondrop;

		// Token: 0x14001241 RID: 4673
		// (add) Token: 0x060097AB RID: 38827
		// (remove) Token: 0x060097AC RID: 38828
		event HTMLButtonElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14001242 RID: 4674
		// (add) Token: 0x060097AD RID: 38829
		// (remove) Token: 0x060097AE RID: 38830
		event HTMLButtonElementEvents_oncutEventHandler oncut;

		// Token: 0x14001243 RID: 4675
		// (add) Token: 0x060097AF RID: 38831
		// (remove) Token: 0x060097B0 RID: 38832
		event HTMLButtonElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001244 RID: 4676
		// (add) Token: 0x060097B1 RID: 38833
		// (remove) Token: 0x060097B2 RID: 38834
		event HTMLButtonElementEvents_oncopyEventHandler oncopy;

		// Token: 0x14001245 RID: 4677
		// (add) Token: 0x060097B3 RID: 38835
		// (remove) Token: 0x060097B4 RID: 38836
		event HTMLButtonElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14001246 RID: 4678
		// (add) Token: 0x060097B5 RID: 38837
		// (remove) Token: 0x060097B6 RID: 38838
		event HTMLButtonElementEvents_onpasteEventHandler onpaste;

		// Token: 0x14001247 RID: 4679
		// (add) Token: 0x060097B7 RID: 38839
		// (remove) Token: 0x060097B8 RID: 38840
		event HTMLButtonElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001248 RID: 4680
		// (add) Token: 0x060097B9 RID: 38841
		// (remove) Token: 0x060097BA RID: 38842
		event HTMLButtonElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001249 RID: 4681
		// (add) Token: 0x060097BB RID: 38843
		// (remove) Token: 0x060097BC RID: 38844
		event HTMLButtonElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400124A RID: 4682
		// (add) Token: 0x060097BD RID: 38845
		// (remove) Token: 0x060097BE RID: 38846
		event HTMLButtonElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x1400124B RID: 4683
		// (add) Token: 0x060097BF RID: 38847
		// (remove) Token: 0x060097C0 RID: 38848
		event HTMLButtonElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x1400124C RID: 4684
		// (add) Token: 0x060097C1 RID: 38849
		// (remove) Token: 0x060097C2 RID: 38850
		event HTMLButtonElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x1400124D RID: 4685
		// (add) Token: 0x060097C3 RID: 38851
		// (remove) Token: 0x060097C4 RID: 38852
		event HTMLButtonElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x1400124E RID: 4686
		// (add) Token: 0x060097C5 RID: 38853
		// (remove) Token: 0x060097C6 RID: 38854
		event HTMLButtonElementEvents_onpageEventHandler onpage;

		// Token: 0x1400124F RID: 4687
		// (add) Token: 0x060097C7 RID: 38855
		// (remove) Token: 0x060097C8 RID: 38856
		event HTMLButtonElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14001250 RID: 4688
		// (add) Token: 0x060097C9 RID: 38857
		// (remove) Token: 0x060097CA RID: 38858
		event HTMLButtonElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001251 RID: 4689
		// (add) Token: 0x060097CB RID: 38859
		// (remove) Token: 0x060097CC RID: 38860
		event HTMLButtonElementEvents_onmoveEventHandler onmove;

		// Token: 0x14001252 RID: 4690
		// (add) Token: 0x060097CD RID: 38861
		// (remove) Token: 0x060097CE RID: 38862
		event HTMLButtonElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001253 RID: 4691
		// (add) Token: 0x060097CF RID: 38863
		// (remove) Token: 0x060097D0 RID: 38864
		event HTMLButtonElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14001254 RID: 4692
		// (add) Token: 0x060097D1 RID: 38865
		// (remove) Token: 0x060097D2 RID: 38866
		event HTMLButtonElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14001255 RID: 4693
		// (add) Token: 0x060097D3 RID: 38867
		// (remove) Token: 0x060097D4 RID: 38868
		event HTMLButtonElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14001256 RID: 4694
		// (add) Token: 0x060097D5 RID: 38869
		// (remove) Token: 0x060097D6 RID: 38870
		event HTMLButtonElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14001257 RID: 4695
		// (add) Token: 0x060097D7 RID: 38871
		// (remove) Token: 0x060097D8 RID: 38872
		event HTMLButtonElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14001258 RID: 4696
		// (add) Token: 0x060097D9 RID: 38873
		// (remove) Token: 0x060097DA RID: 38874
		event HTMLButtonElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14001259 RID: 4697
		// (add) Token: 0x060097DB RID: 38875
		// (remove) Token: 0x060097DC RID: 38876
		event HTMLButtonElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x1400125A RID: 4698
		// (add) Token: 0x060097DD RID: 38877
		// (remove) Token: 0x060097DE RID: 38878
		event HTMLButtonElementEvents_onactivateEventHandler onactivate;

		// Token: 0x1400125B RID: 4699
		// (add) Token: 0x060097DF RID: 38879
		// (remove) Token: 0x060097E0 RID: 38880
		event HTMLButtonElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x1400125C RID: 4700
		// (add) Token: 0x060097E1 RID: 38881
		// (remove) Token: 0x060097E2 RID: 38882
		event HTMLButtonElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x1400125D RID: 4701
		// (add) Token: 0x060097E3 RID: 38883
		// (remove) Token: 0x060097E4 RID: 38884
		event HTMLButtonElementEvents_onfocusoutEventHandler onfocusout;
	}
}
