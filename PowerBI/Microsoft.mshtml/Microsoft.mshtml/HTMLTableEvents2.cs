using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009EE RID: 2542
	[InterfaceType(2)]
	[Guid("3050F623-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLTableEvents2
	{
		// Token: 0x0601030A RID: 66314
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601030B RID: 66315
		[DispId(-600)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onclick([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601030C RID: 66316
		[DispId(-601)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondblclick([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601030D RID: 66317
		[DispId(-603)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onkeypress([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601030E RID: 66318
		[DispId(-602)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeydown([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601030F RID: 66319
		[DispId(-604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeyup([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010310 RID: 66320
		[DispId(-2147418103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseout([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010311 RID: 66321
		[DispId(-2147418104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseover([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010312 RID: 66322
		[DispId(-606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousemove([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010313 RID: 66323
		[DispId(-605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousedown([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010314 RID: 66324
		[DispId(-607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseup([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010315 RID: 66325
		[DispId(-2147418100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onselectstart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010316 RID: 66326
		[DispId(-2147418095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfilterchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010317 RID: 66327
		[DispId(-2147418101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragstart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010318 RID: 66328
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010319 RID: 66329
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601031A RID: 66330
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601031B RID: 66331
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601031C RID: 66332
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601031D RID: 66333
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601031E RID: 66334
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601031F RID: 66335
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010320 RID: 66336
		[DispId(-2147418094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlosecapture([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010321 RID: 66337
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpropertychange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010322 RID: 66338
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010323 RID: 66339
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010324 RID: 66340
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010325 RID: 66341
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010326 RID: 66342
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrag([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010327 RID: 66343
		[DispId(-2147418091)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010328 RID: 66344
		[DispId(-2147418090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010329 RID: 66345
		[DispId(-2147418089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragover([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601032A RID: 66346
		[DispId(-2147418088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragleave([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601032B RID: 66347
		[DispId(-2147418087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrop([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601032C RID: 66348
		[DispId(-2147418083)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecut([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601032D RID: 66349
		[DispId(-2147418086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncut([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601032E RID: 66350
		[DispId(-2147418082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecopy([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601032F RID: 66351
		[DispId(-2147418085)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncopy([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010330 RID: 66352
		[DispId(-2147418081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforepaste([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010331 RID: 66353
		[DispId(-2147418084)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onpaste([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010332 RID: 66354
		[DispId(1023)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontextmenu([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010333 RID: 66355
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010334 RID: 66356
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010335 RID: 66357
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010336 RID: 66358
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010337 RID: 66359
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlayoutcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010338 RID: 66360
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpage([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010339 RID: 66361
		[DispId(1042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601033A RID: 66362
		[DispId(1043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseleave([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601033B RID: 66363
		[DispId(1044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601033C RID: 66364
		[DispId(1045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601033D RID: 66365
		[DispId(1034)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforedeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601033E RID: 66366
		[DispId(1047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601033F RID: 66367
		[DispId(1048)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusin([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010340 RID: 66368
		[DispId(1049)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusout([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010341 RID: 66369
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmove([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010342 RID: 66370
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontrolselect([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010343 RID: 66371
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmovestart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010344 RID: 66372
		[DispId(1039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmoveend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010345 RID: 66373
		[DispId(1040)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onresizestart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010346 RID: 66374
		[DispId(1041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresizeend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06010347 RID: 66375
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmousewheel([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);
	}
}
