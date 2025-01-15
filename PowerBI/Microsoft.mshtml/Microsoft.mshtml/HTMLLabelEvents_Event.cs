using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000421 RID: 1057
	[ComEventInterface(typeof(HTMLLabelEvents\u0000), typeof(HTMLLabelEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLLabelEvents_Event
	{
		// Token: 0x14000778 RID: 1912
		// (add) Token: 0x06004620 RID: 17952
		// (remove) Token: 0x06004621 RID: 17953
		event HTMLLabelEvents_onhelpEventHandler onhelp;

		// Token: 0x14000779 RID: 1913
		// (add) Token: 0x06004622 RID: 17954
		// (remove) Token: 0x06004623 RID: 17955
		event HTMLLabelEvents_onclickEventHandler onclick;

		// Token: 0x1400077A RID: 1914
		// (add) Token: 0x06004624 RID: 17956
		// (remove) Token: 0x06004625 RID: 17957
		event HTMLLabelEvents_ondblclickEventHandler ondblclick;

		// Token: 0x1400077B RID: 1915
		// (add) Token: 0x06004626 RID: 17958
		// (remove) Token: 0x06004627 RID: 17959
		event HTMLLabelEvents_onkeypressEventHandler onkeypress;

		// Token: 0x1400077C RID: 1916
		// (add) Token: 0x06004628 RID: 17960
		// (remove) Token: 0x06004629 RID: 17961
		event HTMLLabelEvents_onkeydownEventHandler onkeydown;

		// Token: 0x1400077D RID: 1917
		// (add) Token: 0x0600462A RID: 17962
		// (remove) Token: 0x0600462B RID: 17963
		event HTMLLabelEvents_onkeyupEventHandler onkeyup;

		// Token: 0x1400077E RID: 1918
		// (add) Token: 0x0600462C RID: 17964
		// (remove) Token: 0x0600462D RID: 17965
		event HTMLLabelEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x1400077F RID: 1919
		// (add) Token: 0x0600462E RID: 17966
		// (remove) Token: 0x0600462F RID: 17967
		event HTMLLabelEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14000780 RID: 1920
		// (add) Token: 0x06004630 RID: 17968
		// (remove) Token: 0x06004631 RID: 17969
		event HTMLLabelEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14000781 RID: 1921
		// (add) Token: 0x06004632 RID: 17970
		// (remove) Token: 0x06004633 RID: 17971
		event HTMLLabelEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14000782 RID: 1922
		// (add) Token: 0x06004634 RID: 17972
		// (remove) Token: 0x06004635 RID: 17973
		event HTMLLabelEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14000783 RID: 1923
		// (add) Token: 0x06004636 RID: 17974
		// (remove) Token: 0x06004637 RID: 17975
		event HTMLLabelEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14000784 RID: 1924
		// (add) Token: 0x06004638 RID: 17976
		// (remove) Token: 0x06004639 RID: 17977
		event HTMLLabelEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14000785 RID: 1925
		// (add) Token: 0x0600463A RID: 17978
		// (remove) Token: 0x0600463B RID: 17979
		event HTMLLabelEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14000786 RID: 1926
		// (add) Token: 0x0600463C RID: 17980
		// (remove) Token: 0x0600463D RID: 17981
		event HTMLLabelEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14000787 RID: 1927
		// (add) Token: 0x0600463E RID: 17982
		// (remove) Token: 0x0600463F RID: 17983
		event HTMLLabelEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14000788 RID: 1928
		// (add) Token: 0x06004640 RID: 17984
		// (remove) Token: 0x06004641 RID: 17985
		event HTMLLabelEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14000789 RID: 1929
		// (add) Token: 0x06004642 RID: 17986
		// (remove) Token: 0x06004643 RID: 17987
		event HTMLLabelEvents_onrowexitEventHandler onrowexit;

		// Token: 0x1400078A RID: 1930
		// (add) Token: 0x06004644 RID: 17988
		// (remove) Token: 0x06004645 RID: 17989
		event HTMLLabelEvents_onrowenterEventHandler onrowenter;

		// Token: 0x1400078B RID: 1931
		// (add) Token: 0x06004646 RID: 17990
		// (remove) Token: 0x06004647 RID: 17991
		event HTMLLabelEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x1400078C RID: 1932
		// (add) Token: 0x06004648 RID: 17992
		// (remove) Token: 0x06004649 RID: 17993
		event HTMLLabelEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x1400078D RID: 1933
		// (add) Token: 0x0600464A RID: 17994
		// (remove) Token: 0x0600464B RID: 17995
		event HTMLLabelEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x1400078E RID: 1934
		// (add) Token: 0x0600464C RID: 17996
		// (remove) Token: 0x0600464D RID: 17997
		event HTMLLabelEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x1400078F RID: 1935
		// (add) Token: 0x0600464E RID: 17998
		// (remove) Token: 0x0600464F RID: 17999
		event HTMLLabelEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14000790 RID: 1936
		// (add) Token: 0x06004650 RID: 18000
		// (remove) Token: 0x06004651 RID: 18001
		event HTMLLabelEvents_onscrollEventHandler onscroll;

		// Token: 0x14000791 RID: 1937
		// (add) Token: 0x06004652 RID: 18002
		// (remove) Token: 0x06004653 RID: 18003
		event HTMLLabelEvents_onfocusEventHandler onfocus;

		// Token: 0x14000792 RID: 1938
		// (add) Token: 0x06004654 RID: 18004
		// (remove) Token: 0x06004655 RID: 18005
		event HTMLLabelEvents_onblurEventHandler onblur;

		// Token: 0x14000793 RID: 1939
		// (add) Token: 0x06004656 RID: 18006
		// (remove) Token: 0x06004657 RID: 18007
		event HTMLLabelEvents_onresizeEventHandler onresize;

		// Token: 0x14000794 RID: 1940
		// (add) Token: 0x06004658 RID: 18008
		// (remove) Token: 0x06004659 RID: 18009
		event HTMLLabelEvents_ondragEventHandler ondrag;

		// Token: 0x14000795 RID: 1941
		// (add) Token: 0x0600465A RID: 18010
		// (remove) Token: 0x0600465B RID: 18011
		event HTMLLabelEvents_ondragendEventHandler ondragend;

		// Token: 0x14000796 RID: 1942
		// (add) Token: 0x0600465C RID: 18012
		// (remove) Token: 0x0600465D RID: 18013
		event HTMLLabelEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14000797 RID: 1943
		// (add) Token: 0x0600465E RID: 18014
		// (remove) Token: 0x0600465F RID: 18015
		event HTMLLabelEvents_ondragoverEventHandler ondragover;

		// Token: 0x14000798 RID: 1944
		// (add) Token: 0x06004660 RID: 18016
		// (remove) Token: 0x06004661 RID: 18017
		event HTMLLabelEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14000799 RID: 1945
		// (add) Token: 0x06004662 RID: 18018
		// (remove) Token: 0x06004663 RID: 18019
		event HTMLLabelEvents_ondropEventHandler ondrop;

		// Token: 0x1400079A RID: 1946
		// (add) Token: 0x06004664 RID: 18020
		// (remove) Token: 0x06004665 RID: 18021
		event HTMLLabelEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x1400079B RID: 1947
		// (add) Token: 0x06004666 RID: 18022
		// (remove) Token: 0x06004667 RID: 18023
		event HTMLLabelEvents_oncutEventHandler oncut;

		// Token: 0x1400079C RID: 1948
		// (add) Token: 0x06004668 RID: 18024
		// (remove) Token: 0x06004669 RID: 18025
		event HTMLLabelEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x1400079D RID: 1949
		// (add) Token: 0x0600466A RID: 18026
		// (remove) Token: 0x0600466B RID: 18027
		event HTMLLabelEvents_oncopyEventHandler oncopy;

		// Token: 0x1400079E RID: 1950
		// (add) Token: 0x0600466C RID: 18028
		// (remove) Token: 0x0600466D RID: 18029
		event HTMLLabelEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x1400079F RID: 1951
		// (add) Token: 0x0600466E RID: 18030
		// (remove) Token: 0x0600466F RID: 18031
		event HTMLLabelEvents_onpasteEventHandler onpaste;

		// Token: 0x140007A0 RID: 1952
		// (add) Token: 0x06004670 RID: 18032
		// (remove) Token: 0x06004671 RID: 18033
		event HTMLLabelEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140007A1 RID: 1953
		// (add) Token: 0x06004672 RID: 18034
		// (remove) Token: 0x06004673 RID: 18035
		event HTMLLabelEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140007A2 RID: 1954
		// (add) Token: 0x06004674 RID: 18036
		// (remove) Token: 0x06004675 RID: 18037
		event HTMLLabelEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140007A3 RID: 1955
		// (add) Token: 0x06004676 RID: 18038
		// (remove) Token: 0x06004677 RID: 18039
		event HTMLLabelEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x140007A4 RID: 1956
		// (add) Token: 0x06004678 RID: 18040
		// (remove) Token: 0x06004679 RID: 18041
		event HTMLLabelEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140007A5 RID: 1957
		// (add) Token: 0x0600467A RID: 18042
		// (remove) Token: 0x0600467B RID: 18043
		event HTMLLabelEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x140007A6 RID: 1958
		// (add) Token: 0x0600467C RID: 18044
		// (remove) Token: 0x0600467D RID: 18045
		event HTMLLabelEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140007A7 RID: 1959
		// (add) Token: 0x0600467E RID: 18046
		// (remove) Token: 0x0600467F RID: 18047
		event HTMLLabelEvents_onpageEventHandler onpage;

		// Token: 0x140007A8 RID: 1960
		// (add) Token: 0x06004680 RID: 18048
		// (remove) Token: 0x06004681 RID: 18049
		event HTMLLabelEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140007A9 RID: 1961
		// (add) Token: 0x06004682 RID: 18050
		// (remove) Token: 0x06004683 RID: 18051
		event HTMLLabelEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140007AA RID: 1962
		// (add) Token: 0x06004684 RID: 18052
		// (remove) Token: 0x06004685 RID: 18053
		event HTMLLabelEvents_onmoveEventHandler onmove;

		// Token: 0x140007AB RID: 1963
		// (add) Token: 0x06004686 RID: 18054
		// (remove) Token: 0x06004687 RID: 18055
		event HTMLLabelEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140007AC RID: 1964
		// (add) Token: 0x06004688 RID: 18056
		// (remove) Token: 0x06004689 RID: 18057
		event HTMLLabelEvents_onmovestartEventHandler onmovestart;

		// Token: 0x140007AD RID: 1965
		// (add) Token: 0x0600468A RID: 18058
		// (remove) Token: 0x0600468B RID: 18059
		event HTMLLabelEvents_onmoveendEventHandler onmoveend;

		// Token: 0x140007AE RID: 1966
		// (add) Token: 0x0600468C RID: 18060
		// (remove) Token: 0x0600468D RID: 18061
		event HTMLLabelEvents_onresizestartEventHandler onresizestart;

		// Token: 0x140007AF RID: 1967
		// (add) Token: 0x0600468E RID: 18062
		// (remove) Token: 0x0600468F RID: 18063
		event HTMLLabelEvents_onresizeendEventHandler onresizeend;

		// Token: 0x140007B0 RID: 1968
		// (add) Token: 0x06004690 RID: 18064
		// (remove) Token: 0x06004691 RID: 18065
		event HTMLLabelEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x140007B1 RID: 1969
		// (add) Token: 0x06004692 RID: 18066
		// (remove) Token: 0x06004693 RID: 18067
		event HTMLLabelEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140007B2 RID: 1970
		// (add) Token: 0x06004694 RID: 18068
		// (remove) Token: 0x06004695 RID: 18069
		event HTMLLabelEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x140007B3 RID: 1971
		// (add) Token: 0x06004696 RID: 18070
		// (remove) Token: 0x06004697 RID: 18071
		event HTMLLabelEvents_onactivateEventHandler onactivate;

		// Token: 0x140007B4 RID: 1972
		// (add) Token: 0x06004698 RID: 18072
		// (remove) Token: 0x06004699 RID: 18073
		event HTMLLabelEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x140007B5 RID: 1973
		// (add) Token: 0x0600469A RID: 18074
		// (remove) Token: 0x0600469B RID: 18075
		event HTMLLabelEvents_onfocusinEventHandler onfocusin;

		// Token: 0x140007B6 RID: 1974
		// (add) Token: 0x0600469C RID: 18076
		// (remove) Token: 0x0600469D RID: 18077
		event HTMLLabelEvents_onfocusoutEventHandler onfocusout;
	}
}
