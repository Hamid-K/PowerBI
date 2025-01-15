using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020008D5 RID: 2261
	[ComEventInterface(typeof(HTMLMapEvents\u0000), typeof(HTMLMapEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLMapEvents_Event
	{
		// Token: 0x14001C80 RID: 7296
		// (add) Token: 0x0600EA48 RID: 59976
		// (remove) Token: 0x0600EA49 RID: 59977
		event HTMLMapEvents_onhelpEventHandler onhelp;

		// Token: 0x14001C81 RID: 7297
		// (add) Token: 0x0600EA4A RID: 59978
		// (remove) Token: 0x0600EA4B RID: 59979
		event HTMLMapEvents_onclickEventHandler onclick;

		// Token: 0x14001C82 RID: 7298
		// (add) Token: 0x0600EA4C RID: 59980
		// (remove) Token: 0x0600EA4D RID: 59981
		event HTMLMapEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14001C83 RID: 7299
		// (add) Token: 0x0600EA4E RID: 59982
		// (remove) Token: 0x0600EA4F RID: 59983
		event HTMLMapEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14001C84 RID: 7300
		// (add) Token: 0x0600EA50 RID: 59984
		// (remove) Token: 0x0600EA51 RID: 59985
		event HTMLMapEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14001C85 RID: 7301
		// (add) Token: 0x0600EA52 RID: 59986
		// (remove) Token: 0x0600EA53 RID: 59987
		event HTMLMapEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14001C86 RID: 7302
		// (add) Token: 0x0600EA54 RID: 59988
		// (remove) Token: 0x0600EA55 RID: 59989
		event HTMLMapEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14001C87 RID: 7303
		// (add) Token: 0x0600EA56 RID: 59990
		// (remove) Token: 0x0600EA57 RID: 59991
		event HTMLMapEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14001C88 RID: 7304
		// (add) Token: 0x0600EA58 RID: 59992
		// (remove) Token: 0x0600EA59 RID: 59993
		event HTMLMapEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14001C89 RID: 7305
		// (add) Token: 0x0600EA5A RID: 59994
		// (remove) Token: 0x0600EA5B RID: 59995
		event HTMLMapEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14001C8A RID: 7306
		// (add) Token: 0x0600EA5C RID: 59996
		// (remove) Token: 0x0600EA5D RID: 59997
		event HTMLMapEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14001C8B RID: 7307
		// (add) Token: 0x0600EA5E RID: 59998
		// (remove) Token: 0x0600EA5F RID: 59999
		event HTMLMapEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14001C8C RID: 7308
		// (add) Token: 0x0600EA60 RID: 60000
		// (remove) Token: 0x0600EA61 RID: 60001
		event HTMLMapEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14001C8D RID: 7309
		// (add) Token: 0x0600EA62 RID: 60002
		// (remove) Token: 0x0600EA63 RID: 60003
		event HTMLMapEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14001C8E RID: 7310
		// (add) Token: 0x0600EA64 RID: 60004
		// (remove) Token: 0x0600EA65 RID: 60005
		event HTMLMapEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001C8F RID: 7311
		// (add) Token: 0x0600EA66 RID: 60006
		// (remove) Token: 0x0600EA67 RID: 60007
		event HTMLMapEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14001C90 RID: 7312
		// (add) Token: 0x0600EA68 RID: 60008
		// (remove) Token: 0x0600EA69 RID: 60009
		event HTMLMapEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001C91 RID: 7313
		// (add) Token: 0x0600EA6A RID: 60010
		// (remove) Token: 0x0600EA6B RID: 60011
		event HTMLMapEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14001C92 RID: 7314
		// (add) Token: 0x0600EA6C RID: 60012
		// (remove) Token: 0x0600EA6D RID: 60013
		event HTMLMapEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14001C93 RID: 7315
		// (add) Token: 0x0600EA6E RID: 60014
		// (remove) Token: 0x0600EA6F RID: 60015
		event HTMLMapEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001C94 RID: 7316
		// (add) Token: 0x0600EA70 RID: 60016
		// (remove) Token: 0x0600EA71 RID: 60017
		event HTMLMapEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001C95 RID: 7317
		// (add) Token: 0x0600EA72 RID: 60018
		// (remove) Token: 0x0600EA73 RID: 60019
		event HTMLMapEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001C96 RID: 7318
		// (add) Token: 0x0600EA74 RID: 60020
		// (remove) Token: 0x0600EA75 RID: 60021
		event HTMLMapEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14001C97 RID: 7319
		// (add) Token: 0x0600EA76 RID: 60022
		// (remove) Token: 0x0600EA77 RID: 60023
		event HTMLMapEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14001C98 RID: 7320
		// (add) Token: 0x0600EA78 RID: 60024
		// (remove) Token: 0x0600EA79 RID: 60025
		event HTMLMapEvents_onscrollEventHandler onscroll;

		// Token: 0x14001C99 RID: 7321
		// (add) Token: 0x0600EA7A RID: 60026
		// (remove) Token: 0x0600EA7B RID: 60027
		event HTMLMapEvents_onfocusEventHandler onfocus;

		// Token: 0x14001C9A RID: 7322
		// (add) Token: 0x0600EA7C RID: 60028
		// (remove) Token: 0x0600EA7D RID: 60029
		event HTMLMapEvents_onblurEventHandler onblur;

		// Token: 0x14001C9B RID: 7323
		// (add) Token: 0x0600EA7E RID: 60030
		// (remove) Token: 0x0600EA7F RID: 60031
		event HTMLMapEvents_onresizeEventHandler onresize;

		// Token: 0x14001C9C RID: 7324
		// (add) Token: 0x0600EA80 RID: 60032
		// (remove) Token: 0x0600EA81 RID: 60033
		event HTMLMapEvents_ondragEventHandler ondrag;

		// Token: 0x14001C9D RID: 7325
		// (add) Token: 0x0600EA82 RID: 60034
		// (remove) Token: 0x0600EA83 RID: 60035
		event HTMLMapEvents_ondragendEventHandler ondragend;

		// Token: 0x14001C9E RID: 7326
		// (add) Token: 0x0600EA84 RID: 60036
		// (remove) Token: 0x0600EA85 RID: 60037
		event HTMLMapEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14001C9F RID: 7327
		// (add) Token: 0x0600EA86 RID: 60038
		// (remove) Token: 0x0600EA87 RID: 60039
		event HTMLMapEvents_ondragoverEventHandler ondragover;

		// Token: 0x14001CA0 RID: 7328
		// (add) Token: 0x0600EA88 RID: 60040
		// (remove) Token: 0x0600EA89 RID: 60041
		event HTMLMapEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14001CA1 RID: 7329
		// (add) Token: 0x0600EA8A RID: 60042
		// (remove) Token: 0x0600EA8B RID: 60043
		event HTMLMapEvents_ondropEventHandler ondrop;

		// Token: 0x14001CA2 RID: 7330
		// (add) Token: 0x0600EA8C RID: 60044
		// (remove) Token: 0x0600EA8D RID: 60045
		event HTMLMapEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14001CA3 RID: 7331
		// (add) Token: 0x0600EA8E RID: 60046
		// (remove) Token: 0x0600EA8F RID: 60047
		event HTMLMapEvents_oncutEventHandler oncut;

		// Token: 0x14001CA4 RID: 7332
		// (add) Token: 0x0600EA90 RID: 60048
		// (remove) Token: 0x0600EA91 RID: 60049
		event HTMLMapEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001CA5 RID: 7333
		// (add) Token: 0x0600EA92 RID: 60050
		// (remove) Token: 0x0600EA93 RID: 60051
		event HTMLMapEvents_oncopyEventHandler oncopy;

		// Token: 0x14001CA6 RID: 7334
		// (add) Token: 0x0600EA94 RID: 60052
		// (remove) Token: 0x0600EA95 RID: 60053
		event HTMLMapEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14001CA7 RID: 7335
		// (add) Token: 0x0600EA96 RID: 60054
		// (remove) Token: 0x0600EA97 RID: 60055
		event HTMLMapEvents_onpasteEventHandler onpaste;

		// Token: 0x14001CA8 RID: 7336
		// (add) Token: 0x0600EA98 RID: 60056
		// (remove) Token: 0x0600EA99 RID: 60057
		event HTMLMapEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001CA9 RID: 7337
		// (add) Token: 0x0600EA9A RID: 60058
		// (remove) Token: 0x0600EA9B RID: 60059
		event HTMLMapEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001CAA RID: 7338
		// (add) Token: 0x0600EA9C RID: 60060
		// (remove) Token: 0x0600EA9D RID: 60061
		event HTMLMapEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14001CAB RID: 7339
		// (add) Token: 0x0600EA9E RID: 60062
		// (remove) Token: 0x0600EA9F RID: 60063
		event HTMLMapEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14001CAC RID: 7340
		// (add) Token: 0x0600EAA0 RID: 60064
		// (remove) Token: 0x0600EAA1 RID: 60065
		event HTMLMapEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001CAD RID: 7341
		// (add) Token: 0x0600EAA2 RID: 60066
		// (remove) Token: 0x0600EAA3 RID: 60067
		event HTMLMapEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14001CAE RID: 7342
		// (add) Token: 0x0600EAA4 RID: 60068
		// (remove) Token: 0x0600EAA5 RID: 60069
		event HTMLMapEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14001CAF RID: 7343
		// (add) Token: 0x0600EAA6 RID: 60070
		// (remove) Token: 0x0600EAA7 RID: 60071
		event HTMLMapEvents_onpageEventHandler onpage;

		// Token: 0x14001CB0 RID: 7344
		// (add) Token: 0x0600EAA8 RID: 60072
		// (remove) Token: 0x0600EAA9 RID: 60073
		event HTMLMapEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14001CB1 RID: 7345
		// (add) Token: 0x0600EAAA RID: 60074
		// (remove) Token: 0x0600EAAB RID: 60075
		event HTMLMapEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001CB2 RID: 7346
		// (add) Token: 0x0600EAAC RID: 60076
		// (remove) Token: 0x0600EAAD RID: 60077
		event HTMLMapEvents_onmoveEventHandler onmove;

		// Token: 0x14001CB3 RID: 7347
		// (add) Token: 0x0600EAAE RID: 60078
		// (remove) Token: 0x0600EAAF RID: 60079
		event HTMLMapEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001CB4 RID: 7348
		// (add) Token: 0x0600EAB0 RID: 60080
		// (remove) Token: 0x0600EAB1 RID: 60081
		event HTMLMapEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14001CB5 RID: 7349
		// (add) Token: 0x0600EAB2 RID: 60082
		// (remove) Token: 0x0600EAB3 RID: 60083
		event HTMLMapEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14001CB6 RID: 7350
		// (add) Token: 0x0600EAB4 RID: 60084
		// (remove) Token: 0x0600EAB5 RID: 60085
		event HTMLMapEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14001CB7 RID: 7351
		// (add) Token: 0x0600EAB6 RID: 60086
		// (remove) Token: 0x0600EAB7 RID: 60087
		event HTMLMapEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14001CB8 RID: 7352
		// (add) Token: 0x0600EAB8 RID: 60088
		// (remove) Token: 0x0600EAB9 RID: 60089
		event HTMLMapEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14001CB9 RID: 7353
		// (add) Token: 0x0600EABA RID: 60090
		// (remove) Token: 0x0600EABB RID: 60091
		event HTMLMapEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14001CBA RID: 7354
		// (add) Token: 0x0600EABC RID: 60092
		// (remove) Token: 0x0600EABD RID: 60093
		event HTMLMapEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14001CBB RID: 7355
		// (add) Token: 0x0600EABE RID: 60094
		// (remove) Token: 0x0600EABF RID: 60095
		event HTMLMapEvents_onactivateEventHandler onactivate;

		// Token: 0x14001CBC RID: 7356
		// (add) Token: 0x0600EAC0 RID: 60096
		// (remove) Token: 0x0600EAC1 RID: 60097
		event HTMLMapEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14001CBD RID: 7357
		// (add) Token: 0x0600EAC2 RID: 60098
		// (remove) Token: 0x0600EAC3 RID: 60099
		event HTMLMapEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14001CBE RID: 7358
		// (add) Token: 0x0600EAC4 RID: 60100
		// (remove) Token: 0x0600EAC5 RID: 60101
		event HTMLMapEvents_onfocusoutEventHandler onfocusout;
	}
}
