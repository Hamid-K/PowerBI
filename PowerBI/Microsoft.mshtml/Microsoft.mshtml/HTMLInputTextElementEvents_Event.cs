using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000585 RID: 1413
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLInputTextElementEvents\u0000), typeof(HTMLInputTextElementEvents_EventProvider\u0000))]
	public interface HTMLInputTextElementEvents_Event
	{
		// Token: 0x14001154 RID: 4436
		// (add) Token: 0x0600943B RID: 37947
		// (remove) Token: 0x0600943C RID: 37948
		event HTMLInputTextElementEvents_onhelpEventHandler onhelp;

		// Token: 0x14001155 RID: 4437
		// (add) Token: 0x0600943D RID: 37949
		// (remove) Token: 0x0600943E RID: 37950
		event HTMLInputTextElementEvents_onclickEventHandler onclick;

		// Token: 0x14001156 RID: 4438
		// (add) Token: 0x0600943F RID: 37951
		// (remove) Token: 0x06009440 RID: 37952
		event HTMLInputTextElementEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14001157 RID: 4439
		// (add) Token: 0x06009441 RID: 37953
		// (remove) Token: 0x06009442 RID: 37954
		event HTMLInputTextElementEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14001158 RID: 4440
		// (add) Token: 0x06009443 RID: 37955
		// (remove) Token: 0x06009444 RID: 37956
		event HTMLInputTextElementEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14001159 RID: 4441
		// (add) Token: 0x06009445 RID: 37957
		// (remove) Token: 0x06009446 RID: 37958
		event HTMLInputTextElementEvents_onkeyupEventHandler onkeyup;

		// Token: 0x1400115A RID: 4442
		// (add) Token: 0x06009447 RID: 37959
		// (remove) Token: 0x06009448 RID: 37960
		event HTMLInputTextElementEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x1400115B RID: 4443
		// (add) Token: 0x06009449 RID: 37961
		// (remove) Token: 0x0600944A RID: 37962
		event HTMLInputTextElementEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x1400115C RID: 4444
		// (add) Token: 0x0600944B RID: 37963
		// (remove) Token: 0x0600944C RID: 37964
		event HTMLInputTextElementEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x1400115D RID: 4445
		// (add) Token: 0x0600944D RID: 37965
		// (remove) Token: 0x0600944E RID: 37966
		event HTMLInputTextElementEvents_onmousedownEventHandler onmousedown;

		// Token: 0x1400115E RID: 4446
		// (add) Token: 0x0600944F RID: 37967
		// (remove) Token: 0x06009450 RID: 37968
		event HTMLInputTextElementEvents_onmouseupEventHandler onmouseup;

		// Token: 0x1400115F RID: 4447
		// (add) Token: 0x06009451 RID: 37969
		// (remove) Token: 0x06009452 RID: 37970
		event HTMLInputTextElementEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14001160 RID: 4448
		// (add) Token: 0x06009453 RID: 37971
		// (remove) Token: 0x06009454 RID: 37972
		event HTMLInputTextElementEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14001161 RID: 4449
		// (add) Token: 0x06009455 RID: 37973
		// (remove) Token: 0x06009456 RID: 37974
		event HTMLInputTextElementEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14001162 RID: 4450
		// (add) Token: 0x06009457 RID: 37975
		// (remove) Token: 0x06009458 RID: 37976
		event HTMLInputTextElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001163 RID: 4451
		// (add) Token: 0x06009459 RID: 37977
		// (remove) Token: 0x0600945A RID: 37978
		event HTMLInputTextElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14001164 RID: 4452
		// (add) Token: 0x0600945B RID: 37979
		// (remove) Token: 0x0600945C RID: 37980
		event HTMLInputTextElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001165 RID: 4453
		// (add) Token: 0x0600945D RID: 37981
		// (remove) Token: 0x0600945E RID: 37982
		event HTMLInputTextElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14001166 RID: 4454
		// (add) Token: 0x0600945F RID: 37983
		// (remove) Token: 0x06009460 RID: 37984
		event HTMLInputTextElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14001167 RID: 4455
		// (add) Token: 0x06009461 RID: 37985
		// (remove) Token: 0x06009462 RID: 37986
		event HTMLInputTextElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001168 RID: 4456
		// (add) Token: 0x06009463 RID: 37987
		// (remove) Token: 0x06009464 RID: 37988
		event HTMLInputTextElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001169 RID: 4457
		// (add) Token: 0x06009465 RID: 37989
		// (remove) Token: 0x06009466 RID: 37990
		event HTMLInputTextElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x1400116A RID: 4458
		// (add) Token: 0x06009467 RID: 37991
		// (remove) Token: 0x06009468 RID: 37992
		event HTMLInputTextElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x1400116B RID: 4459
		// (add) Token: 0x06009469 RID: 37993
		// (remove) Token: 0x0600946A RID: 37994
		event HTMLInputTextElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x1400116C RID: 4460
		// (add) Token: 0x0600946B RID: 37995
		// (remove) Token: 0x0600946C RID: 37996
		event HTMLInputTextElementEvents_onscrollEventHandler onscroll;

		// Token: 0x1400116D RID: 4461
		// (add) Token: 0x0600946D RID: 37997
		// (remove) Token: 0x0600946E RID: 37998
		event HTMLInputTextElementEvents_onfocusEventHandler onfocus;

		// Token: 0x1400116E RID: 4462
		// (add) Token: 0x0600946F RID: 37999
		// (remove) Token: 0x06009470 RID: 38000
		event HTMLInputTextElementEvents_onblurEventHandler onblur;

		// Token: 0x1400116F RID: 4463
		// (add) Token: 0x06009471 RID: 38001
		// (remove) Token: 0x06009472 RID: 38002
		event HTMLInputTextElementEvents_onresizeEventHandler onresize;

		// Token: 0x14001170 RID: 4464
		// (add) Token: 0x06009473 RID: 38003
		// (remove) Token: 0x06009474 RID: 38004
		event HTMLInputTextElementEvents_ondragEventHandler ondrag;

		// Token: 0x14001171 RID: 4465
		// (add) Token: 0x06009475 RID: 38005
		// (remove) Token: 0x06009476 RID: 38006
		event HTMLInputTextElementEvents_ondragendEventHandler ondragend;

		// Token: 0x14001172 RID: 4466
		// (add) Token: 0x06009477 RID: 38007
		// (remove) Token: 0x06009478 RID: 38008
		event HTMLInputTextElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14001173 RID: 4467
		// (add) Token: 0x06009479 RID: 38009
		// (remove) Token: 0x0600947A RID: 38010
		event HTMLInputTextElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x14001174 RID: 4468
		// (add) Token: 0x0600947B RID: 38011
		// (remove) Token: 0x0600947C RID: 38012
		event HTMLInputTextElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14001175 RID: 4469
		// (add) Token: 0x0600947D RID: 38013
		// (remove) Token: 0x0600947E RID: 38014
		event HTMLInputTextElementEvents_ondropEventHandler ondrop;

		// Token: 0x14001176 RID: 4470
		// (add) Token: 0x0600947F RID: 38015
		// (remove) Token: 0x06009480 RID: 38016
		event HTMLInputTextElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14001177 RID: 4471
		// (add) Token: 0x06009481 RID: 38017
		// (remove) Token: 0x06009482 RID: 38018
		event HTMLInputTextElementEvents_oncutEventHandler oncut;

		// Token: 0x14001178 RID: 4472
		// (add) Token: 0x06009483 RID: 38019
		// (remove) Token: 0x06009484 RID: 38020
		event HTMLInputTextElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001179 RID: 4473
		// (add) Token: 0x06009485 RID: 38021
		// (remove) Token: 0x06009486 RID: 38022
		event HTMLInputTextElementEvents_oncopyEventHandler oncopy;

		// Token: 0x1400117A RID: 4474
		// (add) Token: 0x06009487 RID: 38023
		// (remove) Token: 0x06009488 RID: 38024
		event HTMLInputTextElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x1400117B RID: 4475
		// (add) Token: 0x06009489 RID: 38025
		// (remove) Token: 0x0600948A RID: 38026
		event HTMLInputTextElementEvents_onpasteEventHandler onpaste;

		// Token: 0x1400117C RID: 4476
		// (add) Token: 0x0600948B RID: 38027
		// (remove) Token: 0x0600948C RID: 38028
		event HTMLInputTextElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x1400117D RID: 4477
		// (add) Token: 0x0600948D RID: 38029
		// (remove) Token: 0x0600948E RID: 38030
		event HTMLInputTextElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x1400117E RID: 4478
		// (add) Token: 0x0600948F RID: 38031
		// (remove) Token: 0x06009490 RID: 38032
		event HTMLInputTextElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400117F RID: 4479
		// (add) Token: 0x06009491 RID: 38033
		// (remove) Token: 0x06009492 RID: 38034
		event HTMLInputTextElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14001180 RID: 4480
		// (add) Token: 0x06009493 RID: 38035
		// (remove) Token: 0x06009494 RID: 38036
		event HTMLInputTextElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001181 RID: 4481
		// (add) Token: 0x06009495 RID: 38037
		// (remove) Token: 0x06009496 RID: 38038
		event HTMLInputTextElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14001182 RID: 4482
		// (add) Token: 0x06009497 RID: 38039
		// (remove) Token: 0x06009498 RID: 38040
		event HTMLInputTextElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14001183 RID: 4483
		// (add) Token: 0x06009499 RID: 38041
		// (remove) Token: 0x0600949A RID: 38042
		event HTMLInputTextElementEvents_onpageEventHandler onpage;

		// Token: 0x14001184 RID: 4484
		// (add) Token: 0x0600949B RID: 38043
		// (remove) Token: 0x0600949C RID: 38044
		event HTMLInputTextElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14001185 RID: 4485
		// (add) Token: 0x0600949D RID: 38045
		// (remove) Token: 0x0600949E RID: 38046
		event HTMLInputTextElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001186 RID: 4486
		// (add) Token: 0x0600949F RID: 38047
		// (remove) Token: 0x060094A0 RID: 38048
		event HTMLInputTextElementEvents_onmoveEventHandler onmove;

		// Token: 0x14001187 RID: 4487
		// (add) Token: 0x060094A1 RID: 38049
		// (remove) Token: 0x060094A2 RID: 38050
		event HTMLInputTextElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001188 RID: 4488
		// (add) Token: 0x060094A3 RID: 38051
		// (remove) Token: 0x060094A4 RID: 38052
		event HTMLInputTextElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14001189 RID: 4489
		// (add) Token: 0x060094A5 RID: 38053
		// (remove) Token: 0x060094A6 RID: 38054
		event HTMLInputTextElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x1400118A RID: 4490
		// (add) Token: 0x060094A7 RID: 38055
		// (remove) Token: 0x060094A8 RID: 38056
		event HTMLInputTextElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x1400118B RID: 4491
		// (add) Token: 0x060094A9 RID: 38057
		// (remove) Token: 0x060094AA RID: 38058
		event HTMLInputTextElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x1400118C RID: 4492
		// (add) Token: 0x060094AB RID: 38059
		// (remove) Token: 0x060094AC RID: 38060
		event HTMLInputTextElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x1400118D RID: 4493
		// (add) Token: 0x060094AD RID: 38061
		// (remove) Token: 0x060094AE RID: 38062
		event HTMLInputTextElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x1400118E RID: 4494
		// (add) Token: 0x060094AF RID: 38063
		// (remove) Token: 0x060094B0 RID: 38064
		event HTMLInputTextElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x1400118F RID: 4495
		// (add) Token: 0x060094B1 RID: 38065
		// (remove) Token: 0x060094B2 RID: 38066
		event HTMLInputTextElementEvents_onactivateEventHandler onactivate;

		// Token: 0x14001190 RID: 4496
		// (add) Token: 0x060094B3 RID: 38067
		// (remove) Token: 0x060094B4 RID: 38068
		event HTMLInputTextElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14001191 RID: 4497
		// (add) Token: 0x060094B5 RID: 38069
		// (remove) Token: 0x060094B6 RID: 38070
		event HTMLInputTextElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14001192 RID: 4498
		// (add) Token: 0x060094B7 RID: 38071
		// (remove) Token: 0x060094B8 RID: 38072
		event HTMLInputTextElementEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14001193 RID: 4499
		// (add) Token: 0x060094B9 RID: 38073
		// (remove) Token: 0x060094BA RID: 38074
		event HTMLInputTextElementEvents_onchangeEventHandler onchange;

		// Token: 0x14001194 RID: 4500
		// (add) Token: 0x060094BB RID: 38075
		// (remove) Token: 0x060094BC RID: 38076
		event HTMLInputTextElementEvents_onselectEventHandler onselect;

		// Token: 0x14001195 RID: 4501
		// (add) Token: 0x060094BD RID: 38077
		// (remove) Token: 0x060094BE RID: 38078
		event HTMLInputTextElementEvents_onloadEventHandler onload;

		// Token: 0x14001196 RID: 4502
		// (add) Token: 0x060094BF RID: 38079
		// (remove) Token: 0x060094C0 RID: 38080
		event HTMLInputTextElementEvents_onerrorEventHandler onerror;

		// Token: 0x14001197 RID: 4503
		// (add) Token: 0x060094C1 RID: 38081
		// (remove) Token: 0x060094C2 RID: 38082
		event HTMLInputTextElementEvents_onabortEventHandler onabort;
	}
}
