using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A96 RID: 2710
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLScriptEvents\u0000), typeof(HTMLScriptEvents_EventProvider\u0000))]
	public interface HTMLScriptEvents_Event
	{
		// Token: 0x140022E4 RID: 8932
		// (add) Token: 0x06012111 RID: 74001
		// (remove) Token: 0x06012112 RID: 74002
		event HTMLScriptEvents_onhelpEventHandler onhelp;

		// Token: 0x140022E5 RID: 8933
		// (add) Token: 0x06012113 RID: 74003
		// (remove) Token: 0x06012114 RID: 74004
		event HTMLScriptEvents_onclickEventHandler onclick;

		// Token: 0x140022E6 RID: 8934
		// (add) Token: 0x06012115 RID: 74005
		// (remove) Token: 0x06012116 RID: 74006
		event HTMLScriptEvents_ondblclickEventHandler ondblclick;

		// Token: 0x140022E7 RID: 8935
		// (add) Token: 0x06012117 RID: 74007
		// (remove) Token: 0x06012118 RID: 74008
		event HTMLScriptEvents_onkeypressEventHandler onkeypress;

		// Token: 0x140022E8 RID: 8936
		// (add) Token: 0x06012119 RID: 74009
		// (remove) Token: 0x0601211A RID: 74010
		event HTMLScriptEvents_onkeydownEventHandler onkeydown;

		// Token: 0x140022E9 RID: 8937
		// (add) Token: 0x0601211B RID: 74011
		// (remove) Token: 0x0601211C RID: 74012
		event HTMLScriptEvents_onkeyupEventHandler onkeyup;

		// Token: 0x140022EA RID: 8938
		// (add) Token: 0x0601211D RID: 74013
		// (remove) Token: 0x0601211E RID: 74014
		event HTMLScriptEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x140022EB RID: 8939
		// (add) Token: 0x0601211F RID: 74015
		// (remove) Token: 0x06012120 RID: 74016
		event HTMLScriptEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x140022EC RID: 8940
		// (add) Token: 0x06012121 RID: 74017
		// (remove) Token: 0x06012122 RID: 74018
		event HTMLScriptEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x140022ED RID: 8941
		// (add) Token: 0x06012123 RID: 74019
		// (remove) Token: 0x06012124 RID: 74020
		event HTMLScriptEvents_onmousedownEventHandler onmousedown;

		// Token: 0x140022EE RID: 8942
		// (add) Token: 0x06012125 RID: 74021
		// (remove) Token: 0x06012126 RID: 74022
		event HTMLScriptEvents_onmouseupEventHandler onmouseup;

		// Token: 0x140022EF RID: 8943
		// (add) Token: 0x06012127 RID: 74023
		// (remove) Token: 0x06012128 RID: 74024
		event HTMLScriptEvents_onselectstartEventHandler onselectstart;

		// Token: 0x140022F0 RID: 8944
		// (add) Token: 0x06012129 RID: 74025
		// (remove) Token: 0x0601212A RID: 74026
		event HTMLScriptEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x140022F1 RID: 8945
		// (add) Token: 0x0601212B RID: 74027
		// (remove) Token: 0x0601212C RID: 74028
		event HTMLScriptEvents_ondragstartEventHandler ondragstart;

		// Token: 0x140022F2 RID: 8946
		// (add) Token: 0x0601212D RID: 74029
		// (remove) Token: 0x0601212E RID: 74030
		event HTMLScriptEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x140022F3 RID: 8947
		// (add) Token: 0x0601212F RID: 74031
		// (remove) Token: 0x06012130 RID: 74032
		event HTMLScriptEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x140022F4 RID: 8948
		// (add) Token: 0x06012131 RID: 74033
		// (remove) Token: 0x06012132 RID: 74034
		event HTMLScriptEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x140022F5 RID: 8949
		// (add) Token: 0x06012133 RID: 74035
		// (remove) Token: 0x06012134 RID: 74036
		event HTMLScriptEvents_onrowexitEventHandler onrowexit;

		// Token: 0x140022F6 RID: 8950
		// (add) Token: 0x06012135 RID: 74037
		// (remove) Token: 0x06012136 RID: 74038
		event HTMLScriptEvents_onrowenterEventHandler onrowenter;

		// Token: 0x140022F7 RID: 8951
		// (add) Token: 0x06012137 RID: 74039
		// (remove) Token: 0x06012138 RID: 74040
		event HTMLScriptEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x140022F8 RID: 8952
		// (add) Token: 0x06012139 RID: 74041
		// (remove) Token: 0x0601213A RID: 74042
		event HTMLScriptEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x140022F9 RID: 8953
		// (add) Token: 0x0601213B RID: 74043
		// (remove) Token: 0x0601213C RID: 74044
		event HTMLScriptEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x140022FA RID: 8954
		// (add) Token: 0x0601213D RID: 74045
		// (remove) Token: 0x0601213E RID: 74046
		event HTMLScriptEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x140022FB RID: 8955
		// (add) Token: 0x0601213F RID: 74047
		// (remove) Token: 0x06012140 RID: 74048
		event HTMLScriptEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x140022FC RID: 8956
		// (add) Token: 0x06012141 RID: 74049
		// (remove) Token: 0x06012142 RID: 74050
		event HTMLScriptEvents_onscrollEventHandler onscroll;

		// Token: 0x140022FD RID: 8957
		// (add) Token: 0x06012143 RID: 74051
		// (remove) Token: 0x06012144 RID: 74052
		event HTMLScriptEvents_onfocusEventHandler onfocus;

		// Token: 0x140022FE RID: 8958
		// (add) Token: 0x06012145 RID: 74053
		// (remove) Token: 0x06012146 RID: 74054
		event HTMLScriptEvents_onblurEventHandler onblur;

		// Token: 0x140022FF RID: 8959
		// (add) Token: 0x06012147 RID: 74055
		// (remove) Token: 0x06012148 RID: 74056
		event HTMLScriptEvents_onresizeEventHandler onresize;

		// Token: 0x14002300 RID: 8960
		// (add) Token: 0x06012149 RID: 74057
		// (remove) Token: 0x0601214A RID: 74058
		event HTMLScriptEvents_ondragEventHandler ondrag;

		// Token: 0x14002301 RID: 8961
		// (add) Token: 0x0601214B RID: 74059
		// (remove) Token: 0x0601214C RID: 74060
		event HTMLScriptEvents_ondragendEventHandler ondragend;

		// Token: 0x14002302 RID: 8962
		// (add) Token: 0x0601214D RID: 74061
		// (remove) Token: 0x0601214E RID: 74062
		event HTMLScriptEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14002303 RID: 8963
		// (add) Token: 0x0601214F RID: 74063
		// (remove) Token: 0x06012150 RID: 74064
		event HTMLScriptEvents_ondragoverEventHandler ondragover;

		// Token: 0x14002304 RID: 8964
		// (add) Token: 0x06012151 RID: 74065
		// (remove) Token: 0x06012152 RID: 74066
		event HTMLScriptEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14002305 RID: 8965
		// (add) Token: 0x06012153 RID: 74067
		// (remove) Token: 0x06012154 RID: 74068
		event HTMLScriptEvents_ondropEventHandler ondrop;

		// Token: 0x14002306 RID: 8966
		// (add) Token: 0x06012155 RID: 74069
		// (remove) Token: 0x06012156 RID: 74070
		event HTMLScriptEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002307 RID: 8967
		// (add) Token: 0x06012157 RID: 74071
		// (remove) Token: 0x06012158 RID: 74072
		event HTMLScriptEvents_oncutEventHandler oncut;

		// Token: 0x14002308 RID: 8968
		// (add) Token: 0x06012159 RID: 74073
		// (remove) Token: 0x0601215A RID: 74074
		event HTMLScriptEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002309 RID: 8969
		// (add) Token: 0x0601215B RID: 74075
		// (remove) Token: 0x0601215C RID: 74076
		event HTMLScriptEvents_oncopyEventHandler oncopy;

		// Token: 0x1400230A RID: 8970
		// (add) Token: 0x0601215D RID: 74077
		// (remove) Token: 0x0601215E RID: 74078
		event HTMLScriptEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x1400230B RID: 8971
		// (add) Token: 0x0601215F RID: 74079
		// (remove) Token: 0x06012160 RID: 74080
		event HTMLScriptEvents_onpasteEventHandler onpaste;

		// Token: 0x1400230C RID: 8972
		// (add) Token: 0x06012161 RID: 74081
		// (remove) Token: 0x06012162 RID: 74082
		event HTMLScriptEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x1400230D RID: 8973
		// (add) Token: 0x06012163 RID: 74083
		// (remove) Token: 0x06012164 RID: 74084
		event HTMLScriptEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x1400230E RID: 8974
		// (add) Token: 0x06012165 RID: 74085
		// (remove) Token: 0x06012166 RID: 74086
		event HTMLScriptEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400230F RID: 8975
		// (add) Token: 0x06012167 RID: 74087
		// (remove) Token: 0x06012168 RID: 74088
		event HTMLScriptEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14002310 RID: 8976
		// (add) Token: 0x06012169 RID: 74089
		// (remove) Token: 0x0601216A RID: 74090
		event HTMLScriptEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14002311 RID: 8977
		// (add) Token: 0x0601216B RID: 74091
		// (remove) Token: 0x0601216C RID: 74092
		event HTMLScriptEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14002312 RID: 8978
		// (add) Token: 0x0601216D RID: 74093
		// (remove) Token: 0x0601216E RID: 74094
		event HTMLScriptEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14002313 RID: 8979
		// (add) Token: 0x0601216F RID: 74095
		// (remove) Token: 0x06012170 RID: 74096
		event HTMLScriptEvents_onpageEventHandler onpage;

		// Token: 0x14002314 RID: 8980
		// (add) Token: 0x06012171 RID: 74097
		// (remove) Token: 0x06012172 RID: 74098
		event HTMLScriptEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002315 RID: 8981
		// (add) Token: 0x06012173 RID: 74099
		// (remove) Token: 0x06012174 RID: 74100
		event HTMLScriptEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002316 RID: 8982
		// (add) Token: 0x06012175 RID: 74101
		// (remove) Token: 0x06012176 RID: 74102
		event HTMLScriptEvents_onmoveEventHandler onmove;

		// Token: 0x14002317 RID: 8983
		// (add) Token: 0x06012177 RID: 74103
		// (remove) Token: 0x06012178 RID: 74104
		event HTMLScriptEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002318 RID: 8984
		// (add) Token: 0x06012179 RID: 74105
		// (remove) Token: 0x0601217A RID: 74106
		event HTMLScriptEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14002319 RID: 8985
		// (add) Token: 0x0601217B RID: 74107
		// (remove) Token: 0x0601217C RID: 74108
		event HTMLScriptEvents_onmoveendEventHandler onmoveend;

		// Token: 0x1400231A RID: 8986
		// (add) Token: 0x0601217D RID: 74109
		// (remove) Token: 0x0601217E RID: 74110
		event HTMLScriptEvents_onresizestartEventHandler onresizestart;

		// Token: 0x1400231B RID: 8987
		// (add) Token: 0x0601217F RID: 74111
		// (remove) Token: 0x06012180 RID: 74112
		event HTMLScriptEvents_onresizeendEventHandler onresizeend;

		// Token: 0x1400231C RID: 8988
		// (add) Token: 0x06012181 RID: 74113
		// (remove) Token: 0x06012182 RID: 74114
		event HTMLScriptEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x1400231D RID: 8989
		// (add) Token: 0x06012183 RID: 74115
		// (remove) Token: 0x06012184 RID: 74116
		event HTMLScriptEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x1400231E RID: 8990
		// (add) Token: 0x06012185 RID: 74117
		// (remove) Token: 0x06012186 RID: 74118
		event HTMLScriptEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x1400231F RID: 8991
		// (add) Token: 0x06012187 RID: 74119
		// (remove) Token: 0x06012188 RID: 74120
		event HTMLScriptEvents_onactivateEventHandler onactivate;

		// Token: 0x14002320 RID: 8992
		// (add) Token: 0x06012189 RID: 74121
		// (remove) Token: 0x0601218A RID: 74122
		event HTMLScriptEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002321 RID: 8993
		// (add) Token: 0x0601218B RID: 74123
		// (remove) Token: 0x0601218C RID: 74124
		event HTMLScriptEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14002322 RID: 8994
		// (add) Token: 0x0601218D RID: 74125
		// (remove) Token: 0x0601218E RID: 74126
		event HTMLScriptEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14002323 RID: 8995
		// (add) Token: 0x0601218F RID: 74127
		// (remove) Token: 0x06012190 RID: 74128
		event HTMLScriptEvents_onerrorEventHandler onerror;
	}
}
