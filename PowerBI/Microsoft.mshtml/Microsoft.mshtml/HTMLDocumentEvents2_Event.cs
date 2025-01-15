using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200080E RID: 2062
	[ComEventInterface(typeof(HTMLDocumentEvents2\u0000), typeof(HTMLDocumentEvents2_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLDocumentEvents2_Event
	{
		// Token: 0x14001ACF RID: 6863
		// (add) Token: 0x0600DE43 RID: 56899
		// (remove) Token: 0x0600DE44 RID: 56900
		event HTMLDocumentEvents2_onhelpEventHandler onhelp;

		// Token: 0x14001AD0 RID: 6864
		// (add) Token: 0x0600DE45 RID: 56901
		// (remove) Token: 0x0600DE46 RID: 56902
		event HTMLDocumentEvents2_onclickEventHandler onclick;

		// Token: 0x14001AD1 RID: 6865
		// (add) Token: 0x0600DE47 RID: 56903
		// (remove) Token: 0x0600DE48 RID: 56904
		event HTMLDocumentEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x14001AD2 RID: 6866
		// (add) Token: 0x0600DE49 RID: 56905
		// (remove) Token: 0x0600DE4A RID: 56906
		event HTMLDocumentEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x14001AD3 RID: 6867
		// (add) Token: 0x0600DE4B RID: 56907
		// (remove) Token: 0x0600DE4C RID: 56908
		event HTMLDocumentEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x14001AD4 RID: 6868
		// (add) Token: 0x0600DE4D RID: 56909
		// (remove) Token: 0x0600DE4E RID: 56910
		event HTMLDocumentEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x14001AD5 RID: 6869
		// (add) Token: 0x0600DE4F RID: 56911
		// (remove) Token: 0x0600DE50 RID: 56912
		event HTMLDocumentEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x14001AD6 RID: 6870
		// (add) Token: 0x0600DE51 RID: 56913
		// (remove) Token: 0x0600DE52 RID: 56914
		event HTMLDocumentEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x14001AD7 RID: 6871
		// (add) Token: 0x0600DE53 RID: 56915
		// (remove) Token: 0x0600DE54 RID: 56916
		event HTMLDocumentEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x14001AD8 RID: 6872
		// (add) Token: 0x0600DE55 RID: 56917
		// (remove) Token: 0x0600DE56 RID: 56918
		event HTMLDocumentEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x14001AD9 RID: 6873
		// (add) Token: 0x0600DE57 RID: 56919
		// (remove) Token: 0x0600DE58 RID: 56920
		event HTMLDocumentEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x14001ADA RID: 6874
		// (add) Token: 0x0600DE59 RID: 56921
		// (remove) Token: 0x0600DE5A RID: 56922
		event HTMLDocumentEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001ADB RID: 6875
		// (add) Token: 0x0600DE5B RID: 56923
		// (remove) Token: 0x0600DE5C RID: 56924
		event HTMLDocumentEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001ADC RID: 6876
		// (add) Token: 0x0600DE5D RID: 56925
		// (remove) Token: 0x0600DE5E RID: 56926
		event HTMLDocumentEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x14001ADD RID: 6877
		// (add) Token: 0x0600DE5F RID: 56927
		// (remove) Token: 0x0600DE60 RID: 56928
		event HTMLDocumentEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x14001ADE RID: 6878
		// (add) Token: 0x0600DE61 RID: 56929
		// (remove) Token: 0x0600DE62 RID: 56930
		event HTMLDocumentEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x14001ADF RID: 6879
		// (add) Token: 0x0600DE63 RID: 56931
		// (remove) Token: 0x0600DE64 RID: 56932
		event HTMLDocumentEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x14001AE0 RID: 6880
		// (add) Token: 0x0600DE65 RID: 56933
		// (remove) Token: 0x0600DE66 RID: 56934
		event HTMLDocumentEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x14001AE1 RID: 6881
		// (add) Token: 0x0600DE67 RID: 56935
		// (remove) Token: 0x0600DE68 RID: 56936
		event HTMLDocumentEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001AE2 RID: 6882
		// (add) Token: 0x0600DE69 RID: 56937
		// (remove) Token: 0x0600DE6A RID: 56938
		event HTMLDocumentEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001AE3 RID: 6883
		// (add) Token: 0x0600DE6B RID: 56939
		// (remove) Token: 0x0600DE6C RID: 56940
		event HTMLDocumentEvents2_onstopEventHandler onstop;

		// Token: 0x14001AE4 RID: 6884
		// (add) Token: 0x0600DE6D RID: 56941
		// (remove) Token: 0x0600DE6E RID: 56942
		event HTMLDocumentEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001AE5 RID: 6885
		// (add) Token: 0x0600DE6F RID: 56943
		// (remove) Token: 0x0600DE70 RID: 56944
		event HTMLDocumentEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14001AE6 RID: 6886
		// (add) Token: 0x0600DE71 RID: 56945
		// (remove) Token: 0x0600DE72 RID: 56946
		event HTMLDocumentEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x14001AE7 RID: 6887
		// (add) Token: 0x0600DE73 RID: 56947
		// (remove) Token: 0x0600DE74 RID: 56948
		event HTMLDocumentEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14001AE8 RID: 6888
		// (add) Token: 0x0600DE75 RID: 56949
		// (remove) Token: 0x0600DE76 RID: 56950
		event HTMLDocumentEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001AE9 RID: 6889
		// (add) Token: 0x0600DE77 RID: 56951
		// (remove) Token: 0x0600DE78 RID: 56952
		event HTMLDocumentEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001AEA RID: 6890
		// (add) Token: 0x0600DE79 RID: 56953
		// (remove) Token: 0x0600DE7A RID: 56954
		event HTMLDocumentEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001AEB RID: 6891
		// (add) Token: 0x0600DE7B RID: 56955
		// (remove) Token: 0x0600DE7C RID: 56956
		event HTMLDocumentEvents2_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14001AEC RID: 6892
		// (add) Token: 0x0600DE7D RID: 56957
		// (remove) Token: 0x0600DE7E RID: 56958
		event HTMLDocumentEvents2_onselectionchangeEventHandler onselectionchange;

		// Token: 0x14001AED RID: 6893
		// (add) Token: 0x0600DE7F RID: 56959
		// (remove) Token: 0x0600DE80 RID: 56960
		event HTMLDocumentEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001AEE RID: 6894
		// (add) Token: 0x0600DE81 RID: 56961
		// (remove) Token: 0x0600DE82 RID: 56962
		event HTMLDocumentEvents2_onmousewheelEventHandler onmousewheel;

		// Token: 0x14001AEF RID: 6895
		// (add) Token: 0x0600DE83 RID: 56963
		// (remove) Token: 0x0600DE84 RID: 56964
		event HTMLDocumentEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x14001AF0 RID: 6896
		// (add) Token: 0x0600DE85 RID: 56965
		// (remove) Token: 0x0600DE86 RID: 56966
		event HTMLDocumentEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x14001AF1 RID: 6897
		// (add) Token: 0x0600DE87 RID: 56967
		// (remove) Token: 0x0600DE88 RID: 56968
		event HTMLDocumentEvents2_onactivateEventHandler onactivate;

		// Token: 0x14001AF2 RID: 6898
		// (add) Token: 0x0600DE89 RID: 56969
		// (remove) Token: 0x0600DE8A RID: 56970
		event HTMLDocumentEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x14001AF3 RID: 6899
		// (add) Token: 0x0600DE8B RID: 56971
		// (remove) Token: 0x0600DE8C RID: 56972
		event HTMLDocumentEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001AF4 RID: 6900
		// (add) Token: 0x0600DE8D RID: 56973
		// (remove) Token: 0x0600DE8E RID: 56974
		event HTMLDocumentEvents2_onbeforedeactivateEventHandler onbeforedeactivate;
	}
}
