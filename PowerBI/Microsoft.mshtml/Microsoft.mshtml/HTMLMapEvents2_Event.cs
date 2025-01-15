using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000915 RID: 2325
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLMapEvents2\u0000), typeof(HTMLMapEvents2_EventProvider\u0000))]
	public interface HTMLMapEvents2_Event
	{
		// Token: 0x14001CBF RID: 7359
		// (add) Token: 0x0600EB44 RID: 60228
		// (remove) Token: 0x0600EB45 RID: 60229
		event HTMLMapEvents2_onhelpEventHandler onhelp;

		// Token: 0x14001CC0 RID: 7360
		// (add) Token: 0x0600EB46 RID: 60230
		// (remove) Token: 0x0600EB47 RID: 60231
		event HTMLMapEvents2_onclickEventHandler onclick;

		// Token: 0x14001CC1 RID: 7361
		// (add) Token: 0x0600EB48 RID: 60232
		// (remove) Token: 0x0600EB49 RID: 60233
		event HTMLMapEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x14001CC2 RID: 7362
		// (add) Token: 0x0600EB4A RID: 60234
		// (remove) Token: 0x0600EB4B RID: 60235
		event HTMLMapEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x14001CC3 RID: 7363
		// (add) Token: 0x0600EB4C RID: 60236
		// (remove) Token: 0x0600EB4D RID: 60237
		event HTMLMapEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x14001CC4 RID: 7364
		// (add) Token: 0x0600EB4E RID: 60238
		// (remove) Token: 0x0600EB4F RID: 60239
		event HTMLMapEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x14001CC5 RID: 7365
		// (add) Token: 0x0600EB50 RID: 60240
		// (remove) Token: 0x0600EB51 RID: 60241
		event HTMLMapEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x14001CC6 RID: 7366
		// (add) Token: 0x0600EB52 RID: 60242
		// (remove) Token: 0x0600EB53 RID: 60243
		event HTMLMapEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x14001CC7 RID: 7367
		// (add) Token: 0x0600EB54 RID: 60244
		// (remove) Token: 0x0600EB55 RID: 60245
		event HTMLMapEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x14001CC8 RID: 7368
		// (add) Token: 0x0600EB56 RID: 60246
		// (remove) Token: 0x0600EB57 RID: 60247
		event HTMLMapEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x14001CC9 RID: 7369
		// (add) Token: 0x0600EB58 RID: 60248
		// (remove) Token: 0x0600EB59 RID: 60249
		event HTMLMapEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x14001CCA RID: 7370
		// (add) Token: 0x0600EB5A RID: 60250
		// (remove) Token: 0x0600EB5B RID: 60251
		event HTMLMapEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x14001CCB RID: 7371
		// (add) Token: 0x0600EB5C RID: 60252
		// (remove) Token: 0x0600EB5D RID: 60253
		event HTMLMapEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14001CCC RID: 7372
		// (add) Token: 0x0600EB5E RID: 60254
		// (remove) Token: 0x0600EB5F RID: 60255
		event HTMLMapEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x14001CCD RID: 7373
		// (add) Token: 0x0600EB60 RID: 60256
		// (remove) Token: 0x0600EB61 RID: 60257
		event HTMLMapEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001CCE RID: 7374
		// (add) Token: 0x0600EB62 RID: 60258
		// (remove) Token: 0x0600EB63 RID: 60259
		event HTMLMapEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x14001CCF RID: 7375
		// (add) Token: 0x0600EB64 RID: 60260
		// (remove) Token: 0x0600EB65 RID: 60261
		event HTMLMapEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001CD0 RID: 7376
		// (add) Token: 0x0600EB66 RID: 60262
		// (remove) Token: 0x0600EB67 RID: 60263
		event HTMLMapEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x14001CD1 RID: 7377
		// (add) Token: 0x0600EB68 RID: 60264
		// (remove) Token: 0x0600EB69 RID: 60265
		event HTMLMapEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x14001CD2 RID: 7378
		// (add) Token: 0x0600EB6A RID: 60266
		// (remove) Token: 0x0600EB6B RID: 60267
		event HTMLMapEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001CD3 RID: 7379
		// (add) Token: 0x0600EB6C RID: 60268
		// (remove) Token: 0x0600EB6D RID: 60269
		event HTMLMapEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001CD4 RID: 7380
		// (add) Token: 0x0600EB6E RID: 60270
		// (remove) Token: 0x0600EB6F RID: 60271
		event HTMLMapEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001CD5 RID: 7381
		// (add) Token: 0x0600EB70 RID: 60272
		// (remove) Token: 0x0600EB71 RID: 60273
		event HTMLMapEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14001CD6 RID: 7382
		// (add) Token: 0x0600EB72 RID: 60274
		// (remove) Token: 0x0600EB73 RID: 60275
		event HTMLMapEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14001CD7 RID: 7383
		// (add) Token: 0x0600EB74 RID: 60276
		// (remove) Token: 0x0600EB75 RID: 60277
		event HTMLMapEvents2_onscrollEventHandler onscroll;

		// Token: 0x14001CD8 RID: 7384
		// (add) Token: 0x0600EB76 RID: 60278
		// (remove) Token: 0x0600EB77 RID: 60279
		event HTMLMapEvents2_onfocusEventHandler onfocus;

		// Token: 0x14001CD9 RID: 7385
		// (add) Token: 0x0600EB78 RID: 60280
		// (remove) Token: 0x0600EB79 RID: 60281
		event HTMLMapEvents2_onblurEventHandler onblur;

		// Token: 0x14001CDA RID: 7386
		// (add) Token: 0x0600EB7A RID: 60282
		// (remove) Token: 0x0600EB7B RID: 60283
		event HTMLMapEvents2_onresizeEventHandler onresize;

		// Token: 0x14001CDB RID: 7387
		// (add) Token: 0x0600EB7C RID: 60284
		// (remove) Token: 0x0600EB7D RID: 60285
		event HTMLMapEvents2_ondragEventHandler ondrag;

		// Token: 0x14001CDC RID: 7388
		// (add) Token: 0x0600EB7E RID: 60286
		// (remove) Token: 0x0600EB7F RID: 60287
		event HTMLMapEvents2_ondragendEventHandler ondragend;

		// Token: 0x14001CDD RID: 7389
		// (add) Token: 0x0600EB80 RID: 60288
		// (remove) Token: 0x0600EB81 RID: 60289
		event HTMLMapEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x14001CDE RID: 7390
		// (add) Token: 0x0600EB82 RID: 60290
		// (remove) Token: 0x0600EB83 RID: 60291
		event HTMLMapEvents2_ondragoverEventHandler ondragover;

		// Token: 0x14001CDF RID: 7391
		// (add) Token: 0x0600EB84 RID: 60292
		// (remove) Token: 0x0600EB85 RID: 60293
		event HTMLMapEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x14001CE0 RID: 7392
		// (add) Token: 0x0600EB86 RID: 60294
		// (remove) Token: 0x0600EB87 RID: 60295
		event HTMLMapEvents2_ondropEventHandler ondrop;

		// Token: 0x14001CE1 RID: 7393
		// (add) Token: 0x0600EB88 RID: 60296
		// (remove) Token: 0x0600EB89 RID: 60297
		event HTMLMapEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x14001CE2 RID: 7394
		// (add) Token: 0x0600EB8A RID: 60298
		// (remove) Token: 0x0600EB8B RID: 60299
		event HTMLMapEvents2_oncutEventHandler oncut;

		// Token: 0x14001CE3 RID: 7395
		// (add) Token: 0x0600EB8C RID: 60300
		// (remove) Token: 0x0600EB8D RID: 60301
		event HTMLMapEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001CE4 RID: 7396
		// (add) Token: 0x0600EB8E RID: 60302
		// (remove) Token: 0x0600EB8F RID: 60303
		event HTMLMapEvents2_oncopyEventHandler oncopy;

		// Token: 0x14001CE5 RID: 7397
		// (add) Token: 0x0600EB90 RID: 60304
		// (remove) Token: 0x0600EB91 RID: 60305
		event HTMLMapEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14001CE6 RID: 7398
		// (add) Token: 0x0600EB92 RID: 60306
		// (remove) Token: 0x0600EB93 RID: 60307
		event HTMLMapEvents2_onpasteEventHandler onpaste;

		// Token: 0x14001CE7 RID: 7399
		// (add) Token: 0x0600EB94 RID: 60308
		// (remove) Token: 0x0600EB95 RID: 60309
		event HTMLMapEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001CE8 RID: 7400
		// (add) Token: 0x0600EB96 RID: 60310
		// (remove) Token: 0x0600EB97 RID: 60311
		event HTMLMapEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001CE9 RID: 7401
		// (add) Token: 0x0600EB98 RID: 60312
		// (remove) Token: 0x0600EB99 RID: 60313
		event HTMLMapEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14001CEA RID: 7402
		// (add) Token: 0x0600EB9A RID: 60314
		// (remove) Token: 0x0600EB9B RID: 60315
		event HTMLMapEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x14001CEB RID: 7403
		// (add) Token: 0x0600EB9C RID: 60316
		// (remove) Token: 0x0600EB9D RID: 60317
		event HTMLMapEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001CEC RID: 7404
		// (add) Token: 0x0600EB9E RID: 60318
		// (remove) Token: 0x0600EB9F RID: 60319
		event HTMLMapEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14001CED RID: 7405
		// (add) Token: 0x0600EBA0 RID: 60320
		// (remove) Token: 0x0600EBA1 RID: 60321
		event HTMLMapEvents2_onpageEventHandler onpage;

		// Token: 0x14001CEE RID: 7406
		// (add) Token: 0x0600EBA2 RID: 60322
		// (remove) Token: 0x0600EBA3 RID: 60323
		event HTMLMapEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x14001CEF RID: 7407
		// (add) Token: 0x0600EBA4 RID: 60324
		// (remove) Token: 0x0600EBA5 RID: 60325
		event HTMLMapEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14001CF0 RID: 7408
		// (add) Token: 0x0600EBA6 RID: 60326
		// (remove) Token: 0x0600EBA7 RID: 60327
		event HTMLMapEvents2_onactivateEventHandler onactivate;

		// Token: 0x14001CF1 RID: 7409
		// (add) Token: 0x0600EBA8 RID: 60328
		// (remove) Token: 0x0600EBA9 RID: 60329
		event HTMLMapEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x14001CF2 RID: 7410
		// (add) Token: 0x0600EBAA RID: 60330
		// (remove) Token: 0x0600EBAB RID: 60331
		event HTMLMapEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14001CF3 RID: 7411
		// (add) Token: 0x0600EBAC RID: 60332
		// (remove) Token: 0x0600EBAD RID: 60333
		event HTMLMapEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001CF4 RID: 7412
		// (add) Token: 0x0600EBAE RID: 60334
		// (remove) Token: 0x0600EBAF RID: 60335
		event HTMLMapEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x14001CF5 RID: 7413
		// (add) Token: 0x0600EBB0 RID: 60336
		// (remove) Token: 0x0600EBB1 RID: 60337
		event HTMLMapEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x14001CF6 RID: 7414
		// (add) Token: 0x0600EBB2 RID: 60338
		// (remove) Token: 0x0600EBB3 RID: 60339
		event HTMLMapEvents2_onmoveEventHandler onmove;

		// Token: 0x14001CF7 RID: 7415
		// (add) Token: 0x0600EBB4 RID: 60340
		// (remove) Token: 0x0600EBB5 RID: 60341
		event HTMLMapEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001CF8 RID: 7416
		// (add) Token: 0x0600EBB6 RID: 60342
		// (remove) Token: 0x0600EBB7 RID: 60343
		event HTMLMapEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x14001CF9 RID: 7417
		// (add) Token: 0x0600EBB8 RID: 60344
		// (remove) Token: 0x0600EBB9 RID: 60345
		event HTMLMapEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x14001CFA RID: 7418
		// (add) Token: 0x0600EBBA RID: 60346
		// (remove) Token: 0x0600EBBB RID: 60347
		event HTMLMapEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x14001CFB RID: 7419
		// (add) Token: 0x0600EBBC RID: 60348
		// (remove) Token: 0x0600EBBD RID: 60349
		event HTMLMapEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x14001CFC RID: 7420
		// (add) Token: 0x0600EBBE RID: 60350
		// (remove) Token: 0x0600EBBF RID: 60351
		event HTMLMapEvents2_onmousewheelEventHandler onmousewheel;
	}
}
