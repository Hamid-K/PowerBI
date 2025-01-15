using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000955 RID: 2389
	[InterfaceType(2)]
	[Guid("3050F611-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLAreaEvents2
	{
		// Token: 0x0600EC3C RID: 60476
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC3D RID: 60477
		[DispId(-600)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onclick([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC3E RID: 60478
		[DispId(-601)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondblclick([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC3F RID: 60479
		[DispId(-603)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onkeypress([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC40 RID: 60480
		[DispId(-602)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeydown([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC41 RID: 60481
		[DispId(-604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeyup([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC42 RID: 60482
		[DispId(-2147418103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseout([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC43 RID: 60483
		[DispId(-2147418104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseover([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC44 RID: 60484
		[DispId(-606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousemove([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC45 RID: 60485
		[DispId(-605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousedown([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC46 RID: 60486
		[DispId(-607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseup([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC47 RID: 60487
		[DispId(-2147418100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onselectstart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC48 RID: 60488
		[DispId(-2147418095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfilterchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC49 RID: 60489
		[DispId(-2147418101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragstart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4A RID: 60490
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4B RID: 60491
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4C RID: 60492
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4D RID: 60493
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4E RID: 60494
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC4F RID: 60495
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC50 RID: 60496
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC51 RID: 60497
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC52 RID: 60498
		[DispId(-2147418094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlosecapture([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC53 RID: 60499
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpropertychange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC54 RID: 60500
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC55 RID: 60501
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC56 RID: 60502
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC57 RID: 60503
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC58 RID: 60504
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrag([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC59 RID: 60505
		[DispId(-2147418091)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5A RID: 60506
		[DispId(-2147418090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5B RID: 60507
		[DispId(-2147418089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragover([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5C RID: 60508
		[DispId(-2147418088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragleave([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5D RID: 60509
		[DispId(-2147418087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrop([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5E RID: 60510
		[DispId(-2147418083)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecut([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC5F RID: 60511
		[DispId(-2147418086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncut([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC60 RID: 60512
		[DispId(-2147418082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecopy([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC61 RID: 60513
		[DispId(-2147418085)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncopy([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC62 RID: 60514
		[DispId(-2147418081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforepaste([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC63 RID: 60515
		[DispId(-2147418084)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onpaste([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC64 RID: 60516
		[DispId(1023)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontextmenu([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC65 RID: 60517
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC66 RID: 60518
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC67 RID: 60519
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC68 RID: 60520
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC69 RID: 60521
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlayoutcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6A RID: 60522
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpage([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6B RID: 60523
		[DispId(1042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6C RID: 60524
		[DispId(1043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseleave([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6D RID: 60525
		[DispId(1044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6E RID: 60526
		[DispId(1045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC6F RID: 60527
		[DispId(1034)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforedeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC70 RID: 60528
		[DispId(1047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC71 RID: 60529
		[DispId(1048)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusin([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC72 RID: 60530
		[DispId(1049)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusout([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC73 RID: 60531
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmove([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC74 RID: 60532
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontrolselect([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC75 RID: 60533
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmovestart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC76 RID: 60534
		[DispId(1039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmoveend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC77 RID: 60535
		[DispId(1040)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onresizestart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC78 RID: 60536
		[DispId(1041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresizeend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600EC79 RID: 60537
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmousewheel([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);
	}
}
