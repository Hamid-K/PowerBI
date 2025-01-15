using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000461 RID: 1121
	[ComEventInterface(typeof(HTMLLabelEvents2\u0000), typeof(HTMLLabelEvents2_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLLabelEvents2_Event
	{
		// Token: 0x140007B7 RID: 1975
		// (add) Token: 0x0600471C RID: 18204
		// (remove) Token: 0x0600471D RID: 18205
		event HTMLLabelEvents2_onhelpEventHandler onhelp;

		// Token: 0x140007B8 RID: 1976
		// (add) Token: 0x0600471E RID: 18206
		// (remove) Token: 0x0600471F RID: 18207
		event HTMLLabelEvents2_onclickEventHandler onclick;

		// Token: 0x140007B9 RID: 1977
		// (add) Token: 0x06004720 RID: 18208
		// (remove) Token: 0x06004721 RID: 18209
		event HTMLLabelEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x140007BA RID: 1978
		// (add) Token: 0x06004722 RID: 18210
		// (remove) Token: 0x06004723 RID: 18211
		event HTMLLabelEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x140007BB RID: 1979
		// (add) Token: 0x06004724 RID: 18212
		// (remove) Token: 0x06004725 RID: 18213
		event HTMLLabelEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x140007BC RID: 1980
		// (add) Token: 0x06004726 RID: 18214
		// (remove) Token: 0x06004727 RID: 18215
		event HTMLLabelEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x140007BD RID: 1981
		// (add) Token: 0x06004728 RID: 18216
		// (remove) Token: 0x06004729 RID: 18217
		event HTMLLabelEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x140007BE RID: 1982
		// (add) Token: 0x0600472A RID: 18218
		// (remove) Token: 0x0600472B RID: 18219
		event HTMLLabelEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x140007BF RID: 1983
		// (add) Token: 0x0600472C RID: 18220
		// (remove) Token: 0x0600472D RID: 18221
		event HTMLLabelEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x140007C0 RID: 1984
		// (add) Token: 0x0600472E RID: 18222
		// (remove) Token: 0x0600472F RID: 18223
		event HTMLLabelEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x140007C1 RID: 1985
		// (add) Token: 0x06004730 RID: 18224
		// (remove) Token: 0x06004731 RID: 18225
		event HTMLLabelEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x140007C2 RID: 1986
		// (add) Token: 0x06004732 RID: 18226
		// (remove) Token: 0x06004733 RID: 18227
		event HTMLLabelEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x140007C3 RID: 1987
		// (add) Token: 0x06004734 RID: 18228
		// (remove) Token: 0x06004735 RID: 18229
		event HTMLLabelEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x140007C4 RID: 1988
		// (add) Token: 0x06004736 RID: 18230
		// (remove) Token: 0x06004737 RID: 18231
		event HTMLLabelEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x140007C5 RID: 1989
		// (add) Token: 0x06004738 RID: 18232
		// (remove) Token: 0x06004739 RID: 18233
		event HTMLLabelEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x140007C6 RID: 1990
		// (add) Token: 0x0600473A RID: 18234
		// (remove) Token: 0x0600473B RID: 18235
		event HTMLLabelEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x140007C7 RID: 1991
		// (add) Token: 0x0600473C RID: 18236
		// (remove) Token: 0x0600473D RID: 18237
		event HTMLLabelEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x140007C8 RID: 1992
		// (add) Token: 0x0600473E RID: 18238
		// (remove) Token: 0x0600473F RID: 18239
		event HTMLLabelEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x140007C9 RID: 1993
		// (add) Token: 0x06004740 RID: 18240
		// (remove) Token: 0x06004741 RID: 18241
		event HTMLLabelEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x140007CA RID: 1994
		// (add) Token: 0x06004742 RID: 18242
		// (remove) Token: 0x06004743 RID: 18243
		event HTMLLabelEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x140007CB RID: 1995
		// (add) Token: 0x06004744 RID: 18244
		// (remove) Token: 0x06004745 RID: 18245
		event HTMLLabelEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x140007CC RID: 1996
		// (add) Token: 0x06004746 RID: 18246
		// (remove) Token: 0x06004747 RID: 18247
		event HTMLLabelEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x140007CD RID: 1997
		// (add) Token: 0x06004748 RID: 18248
		// (remove) Token: 0x06004749 RID: 18249
		event HTMLLabelEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x140007CE RID: 1998
		// (add) Token: 0x0600474A RID: 18250
		// (remove) Token: 0x0600474B RID: 18251
		event HTMLLabelEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x140007CF RID: 1999
		// (add) Token: 0x0600474C RID: 18252
		// (remove) Token: 0x0600474D RID: 18253
		event HTMLLabelEvents2_onscrollEventHandler onscroll;

		// Token: 0x140007D0 RID: 2000
		// (add) Token: 0x0600474E RID: 18254
		// (remove) Token: 0x0600474F RID: 18255
		event HTMLLabelEvents2_onfocusEventHandler onfocus;

		// Token: 0x140007D1 RID: 2001
		// (add) Token: 0x06004750 RID: 18256
		// (remove) Token: 0x06004751 RID: 18257
		event HTMLLabelEvents2_onblurEventHandler onblur;

		// Token: 0x140007D2 RID: 2002
		// (add) Token: 0x06004752 RID: 18258
		// (remove) Token: 0x06004753 RID: 18259
		event HTMLLabelEvents2_onresizeEventHandler onresize;

		// Token: 0x140007D3 RID: 2003
		// (add) Token: 0x06004754 RID: 18260
		// (remove) Token: 0x06004755 RID: 18261
		event HTMLLabelEvents2_ondragEventHandler ondrag;

		// Token: 0x140007D4 RID: 2004
		// (add) Token: 0x06004756 RID: 18262
		// (remove) Token: 0x06004757 RID: 18263
		event HTMLLabelEvents2_ondragendEventHandler ondragend;

		// Token: 0x140007D5 RID: 2005
		// (add) Token: 0x06004758 RID: 18264
		// (remove) Token: 0x06004759 RID: 18265
		event HTMLLabelEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x140007D6 RID: 2006
		// (add) Token: 0x0600475A RID: 18266
		// (remove) Token: 0x0600475B RID: 18267
		event HTMLLabelEvents2_ondragoverEventHandler ondragover;

		// Token: 0x140007D7 RID: 2007
		// (add) Token: 0x0600475C RID: 18268
		// (remove) Token: 0x0600475D RID: 18269
		event HTMLLabelEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x140007D8 RID: 2008
		// (add) Token: 0x0600475E RID: 18270
		// (remove) Token: 0x0600475F RID: 18271
		event HTMLLabelEvents2_ondropEventHandler ondrop;

		// Token: 0x140007D9 RID: 2009
		// (add) Token: 0x06004760 RID: 18272
		// (remove) Token: 0x06004761 RID: 18273
		event HTMLLabelEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x140007DA RID: 2010
		// (add) Token: 0x06004762 RID: 18274
		// (remove) Token: 0x06004763 RID: 18275
		event HTMLLabelEvents2_oncutEventHandler oncut;

		// Token: 0x140007DB RID: 2011
		// (add) Token: 0x06004764 RID: 18276
		// (remove) Token: 0x06004765 RID: 18277
		event HTMLLabelEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140007DC RID: 2012
		// (add) Token: 0x06004766 RID: 18278
		// (remove) Token: 0x06004767 RID: 18279
		event HTMLLabelEvents2_oncopyEventHandler oncopy;

		// Token: 0x140007DD RID: 2013
		// (add) Token: 0x06004768 RID: 18280
		// (remove) Token: 0x06004769 RID: 18281
		event HTMLLabelEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140007DE RID: 2014
		// (add) Token: 0x0600476A RID: 18282
		// (remove) Token: 0x0600476B RID: 18283
		event HTMLLabelEvents2_onpasteEventHandler onpaste;

		// Token: 0x140007DF RID: 2015
		// (add) Token: 0x0600476C RID: 18284
		// (remove) Token: 0x0600476D RID: 18285
		event HTMLLabelEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140007E0 RID: 2016
		// (add) Token: 0x0600476E RID: 18286
		// (remove) Token: 0x0600476F RID: 18287
		event HTMLLabelEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140007E1 RID: 2017
		// (add) Token: 0x06004770 RID: 18288
		// (remove) Token: 0x06004771 RID: 18289
		event HTMLLabelEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140007E2 RID: 2018
		// (add) Token: 0x06004772 RID: 18290
		// (remove) Token: 0x06004773 RID: 18291
		event HTMLLabelEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x140007E3 RID: 2019
		// (add) Token: 0x06004774 RID: 18292
		// (remove) Token: 0x06004775 RID: 18293
		event HTMLLabelEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140007E4 RID: 2020
		// (add) Token: 0x06004776 RID: 18294
		// (remove) Token: 0x06004777 RID: 18295
		event HTMLLabelEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140007E5 RID: 2021
		// (add) Token: 0x06004778 RID: 18296
		// (remove) Token: 0x06004779 RID: 18297
		event HTMLLabelEvents2_onpageEventHandler onpage;

		// Token: 0x140007E6 RID: 2022
		// (add) Token: 0x0600477A RID: 18298
		// (remove) Token: 0x0600477B RID: 18299
		event HTMLLabelEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x140007E7 RID: 2023
		// (add) Token: 0x0600477C RID: 18300
		// (remove) Token: 0x0600477D RID: 18301
		event HTMLLabelEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140007E8 RID: 2024
		// (add) Token: 0x0600477E RID: 18302
		// (remove) Token: 0x0600477F RID: 18303
		event HTMLLabelEvents2_onactivateEventHandler onactivate;

		// Token: 0x140007E9 RID: 2025
		// (add) Token: 0x06004780 RID: 18304
		// (remove) Token: 0x06004781 RID: 18305
		event HTMLLabelEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x140007EA RID: 2026
		// (add) Token: 0x06004782 RID: 18306
		// (remove) Token: 0x06004783 RID: 18307
		event HTMLLabelEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140007EB RID: 2027
		// (add) Token: 0x06004784 RID: 18308
		// (remove) Token: 0x06004785 RID: 18309
		event HTMLLabelEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140007EC RID: 2028
		// (add) Token: 0x06004786 RID: 18310
		// (remove) Token: 0x06004787 RID: 18311
		event HTMLLabelEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x140007ED RID: 2029
		// (add) Token: 0x06004788 RID: 18312
		// (remove) Token: 0x06004789 RID: 18313
		event HTMLLabelEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x140007EE RID: 2030
		// (add) Token: 0x0600478A RID: 18314
		// (remove) Token: 0x0600478B RID: 18315
		event HTMLLabelEvents2_onmoveEventHandler onmove;

		// Token: 0x140007EF RID: 2031
		// (add) Token: 0x0600478C RID: 18316
		// (remove) Token: 0x0600478D RID: 18317
		event HTMLLabelEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140007F0 RID: 2032
		// (add) Token: 0x0600478E RID: 18318
		// (remove) Token: 0x0600478F RID: 18319
		event HTMLLabelEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x140007F1 RID: 2033
		// (add) Token: 0x06004790 RID: 18320
		// (remove) Token: 0x06004791 RID: 18321
		event HTMLLabelEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x140007F2 RID: 2034
		// (add) Token: 0x06004792 RID: 18322
		// (remove) Token: 0x06004793 RID: 18323
		event HTMLLabelEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x140007F3 RID: 2035
		// (add) Token: 0x06004794 RID: 18324
		// (remove) Token: 0x06004795 RID: 18325
		event HTMLLabelEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x140007F4 RID: 2036
		// (add) Token: 0x06004796 RID: 18326
		// (remove) Token: 0x06004797 RID: 18327
		event HTMLLabelEvents2_onmousewheelEventHandler onmousewheel;
	}
}
