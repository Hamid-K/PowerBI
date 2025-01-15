using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000525 RID: 1317
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLSelectElementEvents2\u0000), typeof(HTMLSelectElementEvents2_EventProvider\u0000))]
	public interface HTMLSelectElementEvents2_Event
	{
		// Token: 0x14000F8E RID: 3982
		// (add) Token: 0x060084FF RID: 34047
		// (remove) Token: 0x06008500 RID: 34048
		event HTMLSelectElementEvents2_onhelpEventHandler onhelp;

		// Token: 0x14000F8F RID: 3983
		// (add) Token: 0x06008501 RID: 34049
		// (remove) Token: 0x06008502 RID: 34050
		event HTMLSelectElementEvents2_onclickEventHandler onclick;

		// Token: 0x14000F90 RID: 3984
		// (add) Token: 0x06008503 RID: 34051
		// (remove) Token: 0x06008504 RID: 34052
		event HTMLSelectElementEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x14000F91 RID: 3985
		// (add) Token: 0x06008505 RID: 34053
		// (remove) Token: 0x06008506 RID: 34054
		event HTMLSelectElementEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x14000F92 RID: 3986
		// (add) Token: 0x06008507 RID: 34055
		// (remove) Token: 0x06008508 RID: 34056
		event HTMLSelectElementEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x14000F93 RID: 3987
		// (add) Token: 0x06008509 RID: 34057
		// (remove) Token: 0x0600850A RID: 34058
		event HTMLSelectElementEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x14000F94 RID: 3988
		// (add) Token: 0x0600850B RID: 34059
		// (remove) Token: 0x0600850C RID: 34060
		event HTMLSelectElementEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x14000F95 RID: 3989
		// (add) Token: 0x0600850D RID: 34061
		// (remove) Token: 0x0600850E RID: 34062
		event HTMLSelectElementEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x14000F96 RID: 3990
		// (add) Token: 0x0600850F RID: 34063
		// (remove) Token: 0x06008510 RID: 34064
		event HTMLSelectElementEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x14000F97 RID: 3991
		// (add) Token: 0x06008511 RID: 34065
		// (remove) Token: 0x06008512 RID: 34066
		event HTMLSelectElementEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x14000F98 RID: 3992
		// (add) Token: 0x06008513 RID: 34067
		// (remove) Token: 0x06008514 RID: 34068
		event HTMLSelectElementEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x14000F99 RID: 3993
		// (add) Token: 0x06008515 RID: 34069
		// (remove) Token: 0x06008516 RID: 34070
		event HTMLSelectElementEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x14000F9A RID: 3994
		// (add) Token: 0x06008517 RID: 34071
		// (remove) Token: 0x06008518 RID: 34072
		event HTMLSelectElementEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14000F9B RID: 3995
		// (add) Token: 0x06008519 RID: 34073
		// (remove) Token: 0x0600851A RID: 34074
		event HTMLSelectElementEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x14000F9C RID: 3996
		// (add) Token: 0x0600851B RID: 34075
		// (remove) Token: 0x0600851C RID: 34076
		event HTMLSelectElementEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14000F9D RID: 3997
		// (add) Token: 0x0600851D RID: 34077
		// (remove) Token: 0x0600851E RID: 34078
		event HTMLSelectElementEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x14000F9E RID: 3998
		// (add) Token: 0x0600851F RID: 34079
		// (remove) Token: 0x06008520 RID: 34080
		event HTMLSelectElementEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14000F9F RID: 3999
		// (add) Token: 0x06008521 RID: 34081
		// (remove) Token: 0x06008522 RID: 34082
		event HTMLSelectElementEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x14000FA0 RID: 4000
		// (add) Token: 0x06008523 RID: 34083
		// (remove) Token: 0x06008524 RID: 34084
		event HTMLSelectElementEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x14000FA1 RID: 4001
		// (add) Token: 0x06008525 RID: 34085
		// (remove) Token: 0x06008526 RID: 34086
		event HTMLSelectElementEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14000FA2 RID: 4002
		// (add) Token: 0x06008527 RID: 34087
		// (remove) Token: 0x06008528 RID: 34088
		event HTMLSelectElementEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x14000FA3 RID: 4003
		// (add) Token: 0x06008529 RID: 34089
		// (remove) Token: 0x0600852A RID: 34090
		event HTMLSelectElementEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14000FA4 RID: 4004
		// (add) Token: 0x0600852B RID: 34091
		// (remove) Token: 0x0600852C RID: 34092
		event HTMLSelectElementEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14000FA5 RID: 4005
		// (add) Token: 0x0600852D RID: 34093
		// (remove) Token: 0x0600852E RID: 34094
		event HTMLSelectElementEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14000FA6 RID: 4006
		// (add) Token: 0x0600852F RID: 34095
		// (remove) Token: 0x06008530 RID: 34096
		event HTMLSelectElementEvents2_onscrollEventHandler onscroll;

		// Token: 0x14000FA7 RID: 4007
		// (add) Token: 0x06008531 RID: 34097
		// (remove) Token: 0x06008532 RID: 34098
		event HTMLSelectElementEvents2_onfocusEventHandler onfocus;

		// Token: 0x14000FA8 RID: 4008
		// (add) Token: 0x06008533 RID: 34099
		// (remove) Token: 0x06008534 RID: 34100
		event HTMLSelectElementEvents2_onblurEventHandler onblur;

		// Token: 0x14000FA9 RID: 4009
		// (add) Token: 0x06008535 RID: 34101
		// (remove) Token: 0x06008536 RID: 34102
		event HTMLSelectElementEvents2_onresizeEventHandler onresize;

		// Token: 0x14000FAA RID: 4010
		// (add) Token: 0x06008537 RID: 34103
		// (remove) Token: 0x06008538 RID: 34104
		event HTMLSelectElementEvents2_ondragEventHandler ondrag;

		// Token: 0x14000FAB RID: 4011
		// (add) Token: 0x06008539 RID: 34105
		// (remove) Token: 0x0600853A RID: 34106
		event HTMLSelectElementEvents2_ondragendEventHandler ondragend;

		// Token: 0x14000FAC RID: 4012
		// (add) Token: 0x0600853B RID: 34107
		// (remove) Token: 0x0600853C RID: 34108
		event HTMLSelectElementEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x14000FAD RID: 4013
		// (add) Token: 0x0600853D RID: 34109
		// (remove) Token: 0x0600853E RID: 34110
		event HTMLSelectElementEvents2_ondragoverEventHandler ondragover;

		// Token: 0x14000FAE RID: 4014
		// (add) Token: 0x0600853F RID: 34111
		// (remove) Token: 0x06008540 RID: 34112
		event HTMLSelectElementEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x14000FAF RID: 4015
		// (add) Token: 0x06008541 RID: 34113
		// (remove) Token: 0x06008542 RID: 34114
		event HTMLSelectElementEvents2_ondropEventHandler ondrop;

		// Token: 0x14000FB0 RID: 4016
		// (add) Token: 0x06008543 RID: 34115
		// (remove) Token: 0x06008544 RID: 34116
		event HTMLSelectElementEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x14000FB1 RID: 4017
		// (add) Token: 0x06008545 RID: 34117
		// (remove) Token: 0x06008546 RID: 34118
		event HTMLSelectElementEvents2_oncutEventHandler oncut;

		// Token: 0x14000FB2 RID: 4018
		// (add) Token: 0x06008547 RID: 34119
		// (remove) Token: 0x06008548 RID: 34120
		event HTMLSelectElementEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14000FB3 RID: 4019
		// (add) Token: 0x06008549 RID: 34121
		// (remove) Token: 0x0600854A RID: 34122
		event HTMLSelectElementEvents2_oncopyEventHandler oncopy;

		// Token: 0x14000FB4 RID: 4020
		// (add) Token: 0x0600854B RID: 34123
		// (remove) Token: 0x0600854C RID: 34124
		event HTMLSelectElementEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14000FB5 RID: 4021
		// (add) Token: 0x0600854D RID: 34125
		// (remove) Token: 0x0600854E RID: 34126
		event HTMLSelectElementEvents2_onpasteEventHandler onpaste;

		// Token: 0x14000FB6 RID: 4022
		// (add) Token: 0x0600854F RID: 34127
		// (remove) Token: 0x06008550 RID: 34128
		event HTMLSelectElementEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14000FB7 RID: 4023
		// (add) Token: 0x06008551 RID: 34129
		// (remove) Token: 0x06008552 RID: 34130
		event HTMLSelectElementEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14000FB8 RID: 4024
		// (add) Token: 0x06008553 RID: 34131
		// (remove) Token: 0x06008554 RID: 34132
		event HTMLSelectElementEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14000FB9 RID: 4025
		// (add) Token: 0x06008555 RID: 34133
		// (remove) Token: 0x06008556 RID: 34134
		event HTMLSelectElementEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x14000FBA RID: 4026
		// (add) Token: 0x06008557 RID: 34135
		// (remove) Token: 0x06008558 RID: 34136
		event HTMLSelectElementEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14000FBB RID: 4027
		// (add) Token: 0x06008559 RID: 34137
		// (remove) Token: 0x0600855A RID: 34138
		event HTMLSelectElementEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14000FBC RID: 4028
		// (add) Token: 0x0600855B RID: 34139
		// (remove) Token: 0x0600855C RID: 34140
		event HTMLSelectElementEvents2_onpageEventHandler onpage;

		// Token: 0x14000FBD RID: 4029
		// (add) Token: 0x0600855D RID: 34141
		// (remove) Token: 0x0600855E RID: 34142
		event HTMLSelectElementEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x14000FBE RID: 4030
		// (add) Token: 0x0600855F RID: 34143
		// (remove) Token: 0x06008560 RID: 34144
		event HTMLSelectElementEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14000FBF RID: 4031
		// (add) Token: 0x06008561 RID: 34145
		// (remove) Token: 0x06008562 RID: 34146
		event HTMLSelectElementEvents2_onactivateEventHandler onactivate;

		// Token: 0x14000FC0 RID: 4032
		// (add) Token: 0x06008563 RID: 34147
		// (remove) Token: 0x06008564 RID: 34148
		event HTMLSelectElementEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x14000FC1 RID: 4033
		// (add) Token: 0x06008565 RID: 34149
		// (remove) Token: 0x06008566 RID: 34150
		event HTMLSelectElementEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14000FC2 RID: 4034
		// (add) Token: 0x06008567 RID: 34151
		// (remove) Token: 0x06008568 RID: 34152
		event HTMLSelectElementEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14000FC3 RID: 4035
		// (add) Token: 0x06008569 RID: 34153
		// (remove) Token: 0x0600856A RID: 34154
		event HTMLSelectElementEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x14000FC4 RID: 4036
		// (add) Token: 0x0600856B RID: 34155
		// (remove) Token: 0x0600856C RID: 34156
		event HTMLSelectElementEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x14000FC5 RID: 4037
		// (add) Token: 0x0600856D RID: 34157
		// (remove) Token: 0x0600856E RID: 34158
		event HTMLSelectElementEvents2_onmoveEventHandler onmove;

		// Token: 0x14000FC6 RID: 4038
		// (add) Token: 0x0600856F RID: 34159
		// (remove) Token: 0x06008570 RID: 34160
		event HTMLSelectElementEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14000FC7 RID: 4039
		// (add) Token: 0x06008571 RID: 34161
		// (remove) Token: 0x06008572 RID: 34162
		event HTMLSelectElementEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x14000FC8 RID: 4040
		// (add) Token: 0x06008573 RID: 34163
		// (remove) Token: 0x06008574 RID: 34164
		event HTMLSelectElementEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x14000FC9 RID: 4041
		// (add) Token: 0x06008575 RID: 34165
		// (remove) Token: 0x06008576 RID: 34166
		event HTMLSelectElementEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x14000FCA RID: 4042
		// (add) Token: 0x06008577 RID: 34167
		// (remove) Token: 0x06008578 RID: 34168
		event HTMLSelectElementEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x14000FCB RID: 4043
		// (add) Token: 0x06008579 RID: 34169
		// (remove) Token: 0x0600857A RID: 34170
		event HTMLSelectElementEvents2_onmousewheelEventHandler onmousewheel;

		// Token: 0x14000FCC RID: 4044
		// (add) Token: 0x0600857B RID: 34171
		// (remove) Token: 0x0600857C RID: 34172
		event HTMLSelectElementEvents2_onchangeEventHandler onchange;
	}
}
