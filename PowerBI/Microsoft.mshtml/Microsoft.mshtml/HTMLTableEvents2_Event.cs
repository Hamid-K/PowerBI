using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A44 RID: 2628
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLTableEvents2\u0000), typeof(HTMLTableEvents2_EventProvider\u0000))]
	public interface HTMLTableEvents2_Event
	{
		// Token: 0x1400202F RID: 8239
		// (add) Token: 0x060109E5 RID: 68069
		// (remove) Token: 0x060109E6 RID: 68070
		event HTMLTableEvents2_onhelpEventHandler onhelp;

		// Token: 0x14002030 RID: 8240
		// (add) Token: 0x060109E7 RID: 68071
		// (remove) Token: 0x060109E8 RID: 68072
		event HTMLTableEvents2_onclickEventHandler onclick;

		// Token: 0x14002031 RID: 8241
		// (add) Token: 0x060109E9 RID: 68073
		// (remove) Token: 0x060109EA RID: 68074
		event HTMLTableEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x14002032 RID: 8242
		// (add) Token: 0x060109EB RID: 68075
		// (remove) Token: 0x060109EC RID: 68076
		event HTMLTableEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x14002033 RID: 8243
		// (add) Token: 0x060109ED RID: 68077
		// (remove) Token: 0x060109EE RID: 68078
		event HTMLTableEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x14002034 RID: 8244
		// (add) Token: 0x060109EF RID: 68079
		// (remove) Token: 0x060109F0 RID: 68080
		event HTMLTableEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x14002035 RID: 8245
		// (add) Token: 0x060109F1 RID: 68081
		// (remove) Token: 0x060109F2 RID: 68082
		event HTMLTableEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x14002036 RID: 8246
		// (add) Token: 0x060109F3 RID: 68083
		// (remove) Token: 0x060109F4 RID: 68084
		event HTMLTableEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x14002037 RID: 8247
		// (add) Token: 0x060109F5 RID: 68085
		// (remove) Token: 0x060109F6 RID: 68086
		event HTMLTableEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x14002038 RID: 8248
		// (add) Token: 0x060109F7 RID: 68087
		// (remove) Token: 0x060109F8 RID: 68088
		event HTMLTableEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x14002039 RID: 8249
		// (add) Token: 0x060109F9 RID: 68089
		// (remove) Token: 0x060109FA RID: 68090
		event HTMLTableEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x1400203A RID: 8250
		// (add) Token: 0x060109FB RID: 68091
		// (remove) Token: 0x060109FC RID: 68092
		event HTMLTableEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x1400203B RID: 8251
		// (add) Token: 0x060109FD RID: 68093
		// (remove) Token: 0x060109FE RID: 68094
		event HTMLTableEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x1400203C RID: 8252
		// (add) Token: 0x060109FF RID: 68095
		// (remove) Token: 0x06010A00 RID: 68096
		event HTMLTableEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x1400203D RID: 8253
		// (add) Token: 0x06010A01 RID: 68097
		// (remove) Token: 0x06010A02 RID: 68098
		event HTMLTableEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x1400203E RID: 8254
		// (add) Token: 0x06010A03 RID: 68099
		// (remove) Token: 0x06010A04 RID: 68100
		event HTMLTableEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x1400203F RID: 8255
		// (add) Token: 0x06010A05 RID: 68101
		// (remove) Token: 0x06010A06 RID: 68102
		event HTMLTableEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14002040 RID: 8256
		// (add) Token: 0x06010A07 RID: 68103
		// (remove) Token: 0x06010A08 RID: 68104
		event HTMLTableEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x14002041 RID: 8257
		// (add) Token: 0x06010A09 RID: 68105
		// (remove) Token: 0x06010A0A RID: 68106
		event HTMLTableEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x14002042 RID: 8258
		// (add) Token: 0x06010A0B RID: 68107
		// (remove) Token: 0x06010A0C RID: 68108
		event HTMLTableEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14002043 RID: 8259
		// (add) Token: 0x06010A0D RID: 68109
		// (remove) Token: 0x06010A0E RID: 68110
		event HTMLTableEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x14002044 RID: 8260
		// (add) Token: 0x06010A0F RID: 68111
		// (remove) Token: 0x06010A10 RID: 68112
		event HTMLTableEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14002045 RID: 8261
		// (add) Token: 0x06010A11 RID: 68113
		// (remove) Token: 0x06010A12 RID: 68114
		event HTMLTableEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14002046 RID: 8262
		// (add) Token: 0x06010A13 RID: 68115
		// (remove) Token: 0x06010A14 RID: 68116
		event HTMLTableEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14002047 RID: 8263
		// (add) Token: 0x06010A15 RID: 68117
		// (remove) Token: 0x06010A16 RID: 68118
		event HTMLTableEvents2_onscrollEventHandler onscroll;

		// Token: 0x14002048 RID: 8264
		// (add) Token: 0x06010A17 RID: 68119
		// (remove) Token: 0x06010A18 RID: 68120
		event HTMLTableEvents2_onfocusEventHandler onfocus;

		// Token: 0x14002049 RID: 8265
		// (add) Token: 0x06010A19 RID: 68121
		// (remove) Token: 0x06010A1A RID: 68122
		event HTMLTableEvents2_onblurEventHandler onblur;

		// Token: 0x1400204A RID: 8266
		// (add) Token: 0x06010A1B RID: 68123
		// (remove) Token: 0x06010A1C RID: 68124
		event HTMLTableEvents2_onresizeEventHandler onresize;

		// Token: 0x1400204B RID: 8267
		// (add) Token: 0x06010A1D RID: 68125
		// (remove) Token: 0x06010A1E RID: 68126
		event HTMLTableEvents2_ondragEventHandler ondrag;

		// Token: 0x1400204C RID: 8268
		// (add) Token: 0x06010A1F RID: 68127
		// (remove) Token: 0x06010A20 RID: 68128
		event HTMLTableEvents2_ondragendEventHandler ondragend;

		// Token: 0x1400204D RID: 8269
		// (add) Token: 0x06010A21 RID: 68129
		// (remove) Token: 0x06010A22 RID: 68130
		event HTMLTableEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x1400204E RID: 8270
		// (add) Token: 0x06010A23 RID: 68131
		// (remove) Token: 0x06010A24 RID: 68132
		event HTMLTableEvents2_ondragoverEventHandler ondragover;

		// Token: 0x1400204F RID: 8271
		// (add) Token: 0x06010A25 RID: 68133
		// (remove) Token: 0x06010A26 RID: 68134
		event HTMLTableEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x14002050 RID: 8272
		// (add) Token: 0x06010A27 RID: 68135
		// (remove) Token: 0x06010A28 RID: 68136
		event HTMLTableEvents2_ondropEventHandler ondrop;

		// Token: 0x14002051 RID: 8273
		// (add) Token: 0x06010A29 RID: 68137
		// (remove) Token: 0x06010A2A RID: 68138
		event HTMLTableEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002052 RID: 8274
		// (add) Token: 0x06010A2B RID: 68139
		// (remove) Token: 0x06010A2C RID: 68140
		event HTMLTableEvents2_oncutEventHandler oncut;

		// Token: 0x14002053 RID: 8275
		// (add) Token: 0x06010A2D RID: 68141
		// (remove) Token: 0x06010A2E RID: 68142
		event HTMLTableEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002054 RID: 8276
		// (add) Token: 0x06010A2F RID: 68143
		// (remove) Token: 0x06010A30 RID: 68144
		event HTMLTableEvents2_oncopyEventHandler oncopy;

		// Token: 0x14002055 RID: 8277
		// (add) Token: 0x06010A31 RID: 68145
		// (remove) Token: 0x06010A32 RID: 68146
		event HTMLTableEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14002056 RID: 8278
		// (add) Token: 0x06010A33 RID: 68147
		// (remove) Token: 0x06010A34 RID: 68148
		event HTMLTableEvents2_onpasteEventHandler onpaste;

		// Token: 0x14002057 RID: 8279
		// (add) Token: 0x06010A35 RID: 68149
		// (remove) Token: 0x06010A36 RID: 68150
		event HTMLTableEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14002058 RID: 8280
		// (add) Token: 0x06010A37 RID: 68151
		// (remove) Token: 0x06010A38 RID: 68152
		event HTMLTableEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14002059 RID: 8281
		// (add) Token: 0x06010A39 RID: 68153
		// (remove) Token: 0x06010A3A RID: 68154
		event HTMLTableEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400205A RID: 8282
		// (add) Token: 0x06010A3B RID: 68155
		// (remove) Token: 0x06010A3C RID: 68156
		event HTMLTableEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x1400205B RID: 8283
		// (add) Token: 0x06010A3D RID: 68157
		// (remove) Token: 0x06010A3E RID: 68158
		event HTMLTableEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x1400205C RID: 8284
		// (add) Token: 0x06010A3F RID: 68159
		// (remove) Token: 0x06010A40 RID: 68160
		event HTMLTableEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x1400205D RID: 8285
		// (add) Token: 0x06010A41 RID: 68161
		// (remove) Token: 0x06010A42 RID: 68162
		event HTMLTableEvents2_onpageEventHandler onpage;

		// Token: 0x1400205E RID: 8286
		// (add) Token: 0x06010A43 RID: 68163
		// (remove) Token: 0x06010A44 RID: 68164
		event HTMLTableEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x1400205F RID: 8287
		// (add) Token: 0x06010A45 RID: 68165
		// (remove) Token: 0x06010A46 RID: 68166
		event HTMLTableEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14002060 RID: 8288
		// (add) Token: 0x06010A47 RID: 68167
		// (remove) Token: 0x06010A48 RID: 68168
		event HTMLTableEvents2_onactivateEventHandler onactivate;

		// Token: 0x14002061 RID: 8289
		// (add) Token: 0x06010A49 RID: 68169
		// (remove) Token: 0x06010A4A RID: 68170
		event HTMLTableEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002062 RID: 8290
		// (add) Token: 0x06010A4B RID: 68171
		// (remove) Token: 0x06010A4C RID: 68172
		event HTMLTableEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002063 RID: 8291
		// (add) Token: 0x06010A4D RID: 68173
		// (remove) Token: 0x06010A4E RID: 68174
		event HTMLTableEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002064 RID: 8292
		// (add) Token: 0x06010A4F RID: 68175
		// (remove) Token: 0x06010A50 RID: 68176
		event HTMLTableEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x14002065 RID: 8293
		// (add) Token: 0x06010A51 RID: 68177
		// (remove) Token: 0x06010A52 RID: 68178
		event HTMLTableEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x14002066 RID: 8294
		// (add) Token: 0x06010A53 RID: 68179
		// (remove) Token: 0x06010A54 RID: 68180
		event HTMLTableEvents2_onmoveEventHandler onmove;

		// Token: 0x14002067 RID: 8295
		// (add) Token: 0x06010A55 RID: 68181
		// (remove) Token: 0x06010A56 RID: 68182
		event HTMLTableEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002068 RID: 8296
		// (add) Token: 0x06010A57 RID: 68183
		// (remove) Token: 0x06010A58 RID: 68184
		event HTMLTableEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x14002069 RID: 8297
		// (add) Token: 0x06010A59 RID: 68185
		// (remove) Token: 0x06010A5A RID: 68186
		event HTMLTableEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x1400206A RID: 8298
		// (add) Token: 0x06010A5B RID: 68187
		// (remove) Token: 0x06010A5C RID: 68188
		event HTMLTableEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x1400206B RID: 8299
		// (add) Token: 0x06010A5D RID: 68189
		// (remove) Token: 0x06010A5E RID: 68190
		event HTMLTableEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x1400206C RID: 8300
		// (add) Token: 0x06010A5F RID: 68191
		// (remove) Token: 0x06010A60 RID: 68192
		event HTMLTableEvents2_onmousewheelEventHandler onmousewheel;
	}
}
