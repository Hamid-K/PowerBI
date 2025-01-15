using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200099A RID: 2458
	[ComEventInterface(typeof(HTMLAreaEvents2\u0000), typeof(HTMLAreaEvents2_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLAreaEvents2_Event
	{
		// Token: 0x14001DB9 RID: 7609
		// (add) Token: 0x0600F23B RID: 62011
		// (remove) Token: 0x0600F23C RID: 62012
		event HTMLAreaEvents2_onhelpEventHandler onhelp;

		// Token: 0x14001DBA RID: 7610
		// (add) Token: 0x0600F23D RID: 62013
		// (remove) Token: 0x0600F23E RID: 62014
		event HTMLAreaEvents2_onclickEventHandler onclick;

		// Token: 0x14001DBB RID: 7611
		// (add) Token: 0x0600F23F RID: 62015
		// (remove) Token: 0x0600F240 RID: 62016
		event HTMLAreaEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x14001DBC RID: 7612
		// (add) Token: 0x0600F241 RID: 62017
		// (remove) Token: 0x0600F242 RID: 62018
		event HTMLAreaEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x14001DBD RID: 7613
		// (add) Token: 0x0600F243 RID: 62019
		// (remove) Token: 0x0600F244 RID: 62020
		event HTMLAreaEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x14001DBE RID: 7614
		// (add) Token: 0x0600F245 RID: 62021
		// (remove) Token: 0x0600F246 RID: 62022
		event HTMLAreaEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x14001DBF RID: 7615
		// (add) Token: 0x0600F247 RID: 62023
		// (remove) Token: 0x0600F248 RID: 62024
		event HTMLAreaEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x14001DC0 RID: 7616
		// (add) Token: 0x0600F249 RID: 62025
		// (remove) Token: 0x0600F24A RID: 62026
		event HTMLAreaEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x14001DC1 RID: 7617
		// (add) Token: 0x0600F24B RID: 62027
		// (remove) Token: 0x0600F24C RID: 62028
		event HTMLAreaEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x14001DC2 RID: 7618
		// (add) Token: 0x0600F24D RID: 62029
		// (remove) Token: 0x0600F24E RID: 62030
		event HTMLAreaEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x14001DC3 RID: 7619
		// (add) Token: 0x0600F24F RID: 62031
		// (remove) Token: 0x0600F250 RID: 62032
		event HTMLAreaEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x14001DC4 RID: 7620
		// (add) Token: 0x0600F251 RID: 62033
		// (remove) Token: 0x0600F252 RID: 62034
		event HTMLAreaEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x14001DC5 RID: 7621
		// (add) Token: 0x0600F253 RID: 62035
		// (remove) Token: 0x0600F254 RID: 62036
		event HTMLAreaEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14001DC6 RID: 7622
		// (add) Token: 0x0600F255 RID: 62037
		// (remove) Token: 0x0600F256 RID: 62038
		event HTMLAreaEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x14001DC7 RID: 7623
		// (add) Token: 0x0600F257 RID: 62039
		// (remove) Token: 0x0600F258 RID: 62040
		event HTMLAreaEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001DC8 RID: 7624
		// (add) Token: 0x0600F259 RID: 62041
		// (remove) Token: 0x0600F25A RID: 62042
		event HTMLAreaEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x14001DC9 RID: 7625
		// (add) Token: 0x0600F25B RID: 62043
		// (remove) Token: 0x0600F25C RID: 62044
		event HTMLAreaEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001DCA RID: 7626
		// (add) Token: 0x0600F25D RID: 62045
		// (remove) Token: 0x0600F25E RID: 62046
		event HTMLAreaEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x14001DCB RID: 7627
		// (add) Token: 0x0600F25F RID: 62047
		// (remove) Token: 0x0600F260 RID: 62048
		event HTMLAreaEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x14001DCC RID: 7628
		// (add) Token: 0x0600F261 RID: 62049
		// (remove) Token: 0x0600F262 RID: 62050
		event HTMLAreaEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001DCD RID: 7629
		// (add) Token: 0x0600F263 RID: 62051
		// (remove) Token: 0x0600F264 RID: 62052
		event HTMLAreaEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001DCE RID: 7630
		// (add) Token: 0x0600F265 RID: 62053
		// (remove) Token: 0x0600F266 RID: 62054
		event HTMLAreaEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001DCF RID: 7631
		// (add) Token: 0x0600F267 RID: 62055
		// (remove) Token: 0x0600F268 RID: 62056
		event HTMLAreaEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14001DD0 RID: 7632
		// (add) Token: 0x0600F269 RID: 62057
		// (remove) Token: 0x0600F26A RID: 62058
		event HTMLAreaEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14001DD1 RID: 7633
		// (add) Token: 0x0600F26B RID: 62059
		// (remove) Token: 0x0600F26C RID: 62060
		event HTMLAreaEvents2_onscrollEventHandler onscroll;

		// Token: 0x14001DD2 RID: 7634
		// (add) Token: 0x0600F26D RID: 62061
		// (remove) Token: 0x0600F26E RID: 62062
		event HTMLAreaEvents2_onfocusEventHandler onfocus;

		// Token: 0x14001DD3 RID: 7635
		// (add) Token: 0x0600F26F RID: 62063
		// (remove) Token: 0x0600F270 RID: 62064
		event HTMLAreaEvents2_onblurEventHandler onblur;

		// Token: 0x14001DD4 RID: 7636
		// (add) Token: 0x0600F271 RID: 62065
		// (remove) Token: 0x0600F272 RID: 62066
		event HTMLAreaEvents2_onresizeEventHandler onresize;

		// Token: 0x14001DD5 RID: 7637
		// (add) Token: 0x0600F273 RID: 62067
		// (remove) Token: 0x0600F274 RID: 62068
		event HTMLAreaEvents2_ondragEventHandler ondrag;

		// Token: 0x14001DD6 RID: 7638
		// (add) Token: 0x0600F275 RID: 62069
		// (remove) Token: 0x0600F276 RID: 62070
		event HTMLAreaEvents2_ondragendEventHandler ondragend;

		// Token: 0x14001DD7 RID: 7639
		// (add) Token: 0x0600F277 RID: 62071
		// (remove) Token: 0x0600F278 RID: 62072
		event HTMLAreaEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x14001DD8 RID: 7640
		// (add) Token: 0x0600F279 RID: 62073
		// (remove) Token: 0x0600F27A RID: 62074
		event HTMLAreaEvents2_ondragoverEventHandler ondragover;

		// Token: 0x14001DD9 RID: 7641
		// (add) Token: 0x0600F27B RID: 62075
		// (remove) Token: 0x0600F27C RID: 62076
		event HTMLAreaEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x14001DDA RID: 7642
		// (add) Token: 0x0600F27D RID: 62077
		// (remove) Token: 0x0600F27E RID: 62078
		event HTMLAreaEvents2_ondropEventHandler ondrop;

		// Token: 0x14001DDB RID: 7643
		// (add) Token: 0x0600F27F RID: 62079
		// (remove) Token: 0x0600F280 RID: 62080
		event HTMLAreaEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x14001DDC RID: 7644
		// (add) Token: 0x0600F281 RID: 62081
		// (remove) Token: 0x0600F282 RID: 62082
		event HTMLAreaEvents2_oncutEventHandler oncut;

		// Token: 0x14001DDD RID: 7645
		// (add) Token: 0x0600F283 RID: 62083
		// (remove) Token: 0x0600F284 RID: 62084
		event HTMLAreaEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001DDE RID: 7646
		// (add) Token: 0x0600F285 RID: 62085
		// (remove) Token: 0x0600F286 RID: 62086
		event HTMLAreaEvents2_oncopyEventHandler oncopy;

		// Token: 0x14001DDF RID: 7647
		// (add) Token: 0x0600F287 RID: 62087
		// (remove) Token: 0x0600F288 RID: 62088
		event HTMLAreaEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14001DE0 RID: 7648
		// (add) Token: 0x0600F289 RID: 62089
		// (remove) Token: 0x0600F28A RID: 62090
		event HTMLAreaEvents2_onpasteEventHandler onpaste;

		// Token: 0x14001DE1 RID: 7649
		// (add) Token: 0x0600F28B RID: 62091
		// (remove) Token: 0x0600F28C RID: 62092
		event HTMLAreaEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001DE2 RID: 7650
		// (add) Token: 0x0600F28D RID: 62093
		// (remove) Token: 0x0600F28E RID: 62094
		event HTMLAreaEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001DE3 RID: 7651
		// (add) Token: 0x0600F28F RID: 62095
		// (remove) Token: 0x0600F290 RID: 62096
		event HTMLAreaEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14001DE4 RID: 7652
		// (add) Token: 0x0600F291 RID: 62097
		// (remove) Token: 0x0600F292 RID: 62098
		event HTMLAreaEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x14001DE5 RID: 7653
		// (add) Token: 0x0600F293 RID: 62099
		// (remove) Token: 0x0600F294 RID: 62100
		event HTMLAreaEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001DE6 RID: 7654
		// (add) Token: 0x0600F295 RID: 62101
		// (remove) Token: 0x0600F296 RID: 62102
		event HTMLAreaEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14001DE7 RID: 7655
		// (add) Token: 0x0600F297 RID: 62103
		// (remove) Token: 0x0600F298 RID: 62104
		event HTMLAreaEvents2_onpageEventHandler onpage;

		// Token: 0x14001DE8 RID: 7656
		// (add) Token: 0x0600F299 RID: 62105
		// (remove) Token: 0x0600F29A RID: 62106
		event HTMLAreaEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x14001DE9 RID: 7657
		// (add) Token: 0x0600F29B RID: 62107
		// (remove) Token: 0x0600F29C RID: 62108
		event HTMLAreaEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14001DEA RID: 7658
		// (add) Token: 0x0600F29D RID: 62109
		// (remove) Token: 0x0600F29E RID: 62110
		event HTMLAreaEvents2_onactivateEventHandler onactivate;

		// Token: 0x14001DEB RID: 7659
		// (add) Token: 0x0600F29F RID: 62111
		// (remove) Token: 0x0600F2A0 RID: 62112
		event HTMLAreaEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x14001DEC RID: 7660
		// (add) Token: 0x0600F2A1 RID: 62113
		// (remove) Token: 0x0600F2A2 RID: 62114
		event HTMLAreaEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14001DED RID: 7661
		// (add) Token: 0x0600F2A3 RID: 62115
		// (remove) Token: 0x0600F2A4 RID: 62116
		event HTMLAreaEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001DEE RID: 7662
		// (add) Token: 0x0600F2A5 RID: 62117
		// (remove) Token: 0x0600F2A6 RID: 62118
		event HTMLAreaEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x14001DEF RID: 7663
		// (add) Token: 0x0600F2A7 RID: 62119
		// (remove) Token: 0x0600F2A8 RID: 62120
		event HTMLAreaEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x14001DF0 RID: 7664
		// (add) Token: 0x0600F2A9 RID: 62121
		// (remove) Token: 0x0600F2AA RID: 62122
		event HTMLAreaEvents2_onmoveEventHandler onmove;

		// Token: 0x14001DF1 RID: 7665
		// (add) Token: 0x0600F2AB RID: 62123
		// (remove) Token: 0x0600F2AC RID: 62124
		event HTMLAreaEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001DF2 RID: 7666
		// (add) Token: 0x0600F2AD RID: 62125
		// (remove) Token: 0x0600F2AE RID: 62126
		event HTMLAreaEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x14001DF3 RID: 7667
		// (add) Token: 0x0600F2AF RID: 62127
		// (remove) Token: 0x0600F2B0 RID: 62128
		event HTMLAreaEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x14001DF4 RID: 7668
		// (add) Token: 0x0600F2B1 RID: 62129
		// (remove) Token: 0x0600F2B2 RID: 62130
		event HTMLAreaEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x14001DF5 RID: 7669
		// (add) Token: 0x0600F2B3 RID: 62131
		// (remove) Token: 0x0600F2B4 RID: 62132
		event HTMLAreaEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x14001DF6 RID: 7670
		// (add) Token: 0x0600F2B5 RID: 62133
		// (remove) Token: 0x0600F2B6 RID: 62134
		event HTMLAreaEvents2_onmousewheelEventHandler onmousewheel;
	}
}
