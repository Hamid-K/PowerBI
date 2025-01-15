using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000696 RID: 1686
	[ComSourceInterfaces("mshtml.HTMLInputTextElementEvents\0mshtml.HTMLInputTextElementEvents2\0mshtml.HTMLTextContainerEvents\0mshtml.HTMLTextContainerEvents2\0\0")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F2AC-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLTextAreaElementClass : DispHTMLTextAreaElement, HTMLTextAreaElement, HTMLInputTextElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLTextContainer, IHTMLTextAreaElement, HTMLInputTextElementEvents2_Event, HTMLTextContainerEvents_Event, HTMLTextContainerEvents2_Event
	{
		// Token: 0x060099A1 RID: 39329
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLTextAreaElementClass();

		// Token: 0x060099A2 RID: 39330
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060099A3 RID: 39331
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060099A4 RID: 39332
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170031DE RID: 12766
		// (get) Token: 0x060099A6 RID: 39334
		// (set) Token: 0x060099A5 RID: 39333
		[DispId(-2147417111)]
		public virtual extern string className
		{
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170031DF RID: 12767
		// (get) Token: 0x060099A8 RID: 39336
		// (set) Token: 0x060099A7 RID: 39335
		[DispId(-2147417110)]
		public virtual extern string id
		{
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170031E0 RID: 12768
		// (get) Token: 0x060099A9 RID: 39337
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170031E1 RID: 12769
		// (get) Token: 0x060099AA RID: 39338
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170031E2 RID: 12770
		// (get) Token: 0x060099AB RID: 39339
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170031E3 RID: 12771
		// (get) Token: 0x060099AD RID: 39341
		// (set) Token: 0x060099AC RID: 39340
		[DispId(-2147412099)]
		public virtual extern object onhelp
		{
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031E4 RID: 12772
		// (get) Token: 0x060099AF RID: 39343
		// (set) Token: 0x060099AE RID: 39342
		[DispId(-2147412104)]
		public virtual extern object onclick
		{
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031E5 RID: 12773
		// (get) Token: 0x060099B1 RID: 39345
		// (set) Token: 0x060099B0 RID: 39344
		[DispId(-2147412103)]
		public virtual extern object ondblclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031E6 RID: 12774
		// (get) Token: 0x060099B3 RID: 39347
		// (set) Token: 0x060099B2 RID: 39346
		[DispId(-2147412107)]
		public virtual extern object onkeydown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031E7 RID: 12775
		// (get) Token: 0x060099B5 RID: 39349
		// (set) Token: 0x060099B4 RID: 39348
		[DispId(-2147412106)]
		public virtual extern object onkeyup
		{
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031E8 RID: 12776
		// (get) Token: 0x060099B7 RID: 39351
		// (set) Token: 0x060099B6 RID: 39350
		[DispId(-2147412105)]
		public virtual extern object onkeypress
		{
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031E9 RID: 12777
		// (get) Token: 0x060099B9 RID: 39353
		// (set) Token: 0x060099B8 RID: 39352
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031EA RID: 12778
		// (get) Token: 0x060099BB RID: 39355
		// (set) Token: 0x060099BA RID: 39354
		[DispId(-2147412112)]
		public virtual extern object onmouseover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031EB RID: 12779
		// (get) Token: 0x060099BD RID: 39357
		// (set) Token: 0x060099BC RID: 39356
		[DispId(-2147412108)]
		public virtual extern object onmousemove
		{
			[DispId(-2147412108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031EC RID: 12780
		// (get) Token: 0x060099BF RID: 39359
		// (set) Token: 0x060099BE RID: 39358
		[DispId(-2147412110)]
		public virtual extern object onmousedown
		{
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031ED RID: 12781
		// (get) Token: 0x060099C1 RID: 39361
		// (set) Token: 0x060099C0 RID: 39360
		[DispId(-2147412109)]
		public virtual extern object onmouseup
		{
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170031EE RID: 12782
		// (get) Token: 0x060099C2 RID: 39362
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170031EF RID: 12783
		// (get) Token: 0x060099C4 RID: 39364
		// (set) Token: 0x060099C3 RID: 39363
		[DispId(-2147418043)]
		public virtual extern string title
		{
			[DispId(-2147418043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170031F0 RID: 12784
		// (get) Token: 0x060099C6 RID: 39366
		// (set) Token: 0x060099C5 RID: 39365
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170031F1 RID: 12785
		// (get) Token: 0x060099C8 RID: 39368
		// (set) Token: 0x060099C7 RID: 39367
		[DispId(-2147412075)]
		public virtual extern object onselectstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060099C9 RID: 39369
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060099CA RID: 39370
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170031F2 RID: 12786
		// (get) Token: 0x060099CB RID: 39371
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170031F3 RID: 12787
		// (get) Token: 0x060099CC RID: 39372
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170031F4 RID: 12788
		// (get) Token: 0x060099CE RID: 39374
		// (set) Token: 0x060099CD RID: 39373
		[DispId(-2147413103)]
		public virtual extern string lang
		{
			[DispId(-2147413103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170031F5 RID: 12789
		// (get) Token: 0x060099CF RID: 39375
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170031F6 RID: 12790
		// (get) Token: 0x060099D0 RID: 39376
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170031F7 RID: 12791
		// (get) Token: 0x060099D1 RID: 39377
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170031F8 RID: 12792
		// (get) Token: 0x060099D2 RID: 39378
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170031F9 RID: 12793
		// (get) Token: 0x060099D3 RID: 39379
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170031FA RID: 12794
		// (get) Token: 0x060099D5 RID: 39381
		// (set) Token: 0x060099D4 RID: 39380
		[DispId(-2147417086)]
		public virtual extern string innerHTML
		{
			[DispId(-2147417086)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417086)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170031FB RID: 12795
		// (get) Token: 0x060099D7 RID: 39383
		// (set) Token: 0x060099D6 RID: 39382
		[DispId(-2147417085)]
		public virtual extern string innerText
		{
			[DispId(-2147417085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170031FC RID: 12796
		// (get) Token: 0x060099D9 RID: 39385
		// (set) Token: 0x060099D8 RID: 39384
		[DispId(-2147417084)]
		public virtual extern string outerHTML
		{
			[DispId(-2147417084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170031FD RID: 12797
		// (get) Token: 0x060099DB RID: 39387
		// (set) Token: 0x060099DA RID: 39386
		[DispId(-2147417083)]
		public virtual extern string outerText
		{
			[DispId(-2147417083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x060099DC RID: 39388
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060099DD RID: 39389
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170031FE RID: 12798
		// (get) Token: 0x060099DE RID: 39390
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170031FF RID: 12799
		// (get) Token: 0x060099DF RID: 39391
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060099E0 RID: 39392
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17003200 RID: 12800
		// (get) Token: 0x060099E1 RID: 39393
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003201 RID: 12801
		// (get) Token: 0x060099E3 RID: 39395
		// (set) Token: 0x060099E2 RID: 39394
		[DispId(-2147412077)]
		public virtual extern object ondragstart
		{
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060099E4 RID: 39396
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17003202 RID: 12802
		// (get) Token: 0x060099E6 RID: 39398
		// (set) Token: 0x060099E5 RID: 39397
		[DispId(-2147412091)]
		public virtual extern object onbeforeupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003203 RID: 12803
		// (get) Token: 0x060099E8 RID: 39400
		// (set) Token: 0x060099E7 RID: 39399
		[DispId(-2147412090)]
		public virtual extern object onafterupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003204 RID: 12804
		// (get) Token: 0x060099EA RID: 39402
		// (set) Token: 0x060099E9 RID: 39401
		[DispId(-2147412074)]
		public virtual extern object onerrorupdate
		{
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003205 RID: 12805
		// (get) Token: 0x060099EC RID: 39404
		// (set) Token: 0x060099EB RID: 39403
		[DispId(-2147412094)]
		public virtual extern object onrowexit
		{
			[DispId(-2147412094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003206 RID: 12806
		// (get) Token: 0x060099EE RID: 39406
		// (set) Token: 0x060099ED RID: 39405
		[DispId(-2147412093)]
		public virtual extern object onrowenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003207 RID: 12807
		// (get) Token: 0x060099F0 RID: 39408
		// (set) Token: 0x060099EF RID: 39407
		[DispId(-2147412072)]
		public virtual extern object ondatasetchanged
		{
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003208 RID: 12808
		// (get) Token: 0x060099F2 RID: 39410
		// (set) Token: 0x060099F1 RID: 39409
		[DispId(-2147412071)]
		public virtual extern object ondataavailable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412071)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003209 RID: 12809
		// (get) Token: 0x060099F4 RID: 39412
		// (set) Token: 0x060099F3 RID: 39411
		[DispId(-2147412070)]
		public virtual extern object ondatasetcomplete
		{
			[DispId(-2147412070)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700320A RID: 12810
		// (get) Token: 0x060099F6 RID: 39414
		// (set) Token: 0x060099F5 RID: 39413
		[DispId(-2147412069)]
		public virtual extern object onfilterchange
		{
			[DispId(-2147412069)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412069)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700320B RID: 12811
		// (get) Token: 0x060099F7 RID: 39415
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700320C RID: 12812
		// (get) Token: 0x060099F8 RID: 39416
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700320D RID: 12813
		// (get) Token: 0x060099F9 RID: 39417
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060099FA RID: 39418
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x060099FB RID: 39419
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x1700320E RID: 12814
		// (get) Token: 0x060099FD RID: 39421
		// (set) Token: 0x060099FC RID: 39420
		[DispId(-2147412066)]
		public virtual extern object onlosecapture
		{
			[TypeLibFunc(20)]
			[DispId(-2147412066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060099FE RID: 39422
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x060099FF RID: 39423
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700320F RID: 12815
		// (get) Token: 0x06009A01 RID: 39425
		// (set) Token: 0x06009A00 RID: 39424
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003210 RID: 12816
		// (get) Token: 0x06009A03 RID: 39427
		// (set) Token: 0x06009A02 RID: 39426
		[DispId(-2147412063)]
		public virtual extern object ondrag
		{
			[TypeLibFunc(20)]
			[DispId(-2147412063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412063)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003211 RID: 12817
		// (get) Token: 0x06009A05 RID: 39429
		// (set) Token: 0x06009A04 RID: 39428
		[DispId(-2147412062)]
		public virtual extern object ondragend
		{
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003212 RID: 12818
		// (get) Token: 0x06009A07 RID: 39431
		// (set) Token: 0x06009A06 RID: 39430
		[DispId(-2147412061)]
		public virtual extern object ondragenter
		{
			[DispId(-2147412061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003213 RID: 12819
		// (get) Token: 0x06009A09 RID: 39433
		// (set) Token: 0x06009A08 RID: 39432
		[DispId(-2147412060)]
		public virtual extern object ondragover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003214 RID: 12820
		// (get) Token: 0x06009A0B RID: 39435
		// (set) Token: 0x06009A0A RID: 39434
		[DispId(-2147412059)]
		public virtual extern object ondragleave
		{
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003215 RID: 12821
		// (get) Token: 0x06009A0D RID: 39437
		// (set) Token: 0x06009A0C RID: 39436
		[DispId(-2147412058)]
		public virtual extern object ondrop
		{
			[DispId(-2147412058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003216 RID: 12822
		// (get) Token: 0x06009A0F RID: 39439
		// (set) Token: 0x06009A0E RID: 39438
		[DispId(-2147412054)]
		public virtual extern object onbeforecut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003217 RID: 12823
		// (get) Token: 0x06009A11 RID: 39441
		// (set) Token: 0x06009A10 RID: 39440
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003218 RID: 12824
		// (get) Token: 0x06009A13 RID: 39443
		// (set) Token: 0x06009A12 RID: 39442
		[DispId(-2147412053)]
		public virtual extern object onbeforecopy
		{
			[TypeLibFunc(20)]
			[DispId(-2147412053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003219 RID: 12825
		// (get) Token: 0x06009A15 RID: 39445
		// (set) Token: 0x06009A14 RID: 39444
		[DispId(-2147412056)]
		public virtual extern object oncopy
		{
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700321A RID: 12826
		// (get) Token: 0x06009A17 RID: 39447
		// (set) Token: 0x06009A16 RID: 39446
		[DispId(-2147412052)]
		public virtual extern object onbeforepaste
		{
			[TypeLibFunc(20)]
			[DispId(-2147412052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700321B RID: 12827
		// (get) Token: 0x06009A19 RID: 39449
		// (set) Token: 0x06009A18 RID: 39448
		[DispId(-2147412055)]
		public virtual extern object onpaste
		{
			[TypeLibFunc(20)]
			[DispId(-2147412055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700321C RID: 12828
		// (get) Token: 0x06009A1A RID: 39450
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700321D RID: 12829
		// (get) Token: 0x06009A1C RID: 39452
		// (set) Token: 0x06009A1B RID: 39451
		[DispId(-2147412065)]
		public virtual extern object onpropertychange
		{
			[DispId(-2147412065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06009A1D RID: 39453
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06009A1E RID: 39454
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06009A1F RID: 39455
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06009A20 RID: 39456
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06009A21 RID: 39457
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700321E RID: 12830
		// (get) Token: 0x06009A23 RID: 39459
		// (set) Token: 0x06009A22 RID: 39458
		[DispId(-2147418097)]
		public virtual extern short tabIndex
		{
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06009A24 RID: 39460
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x1700321F RID: 12831
		// (get) Token: 0x06009A26 RID: 39462
		// (set) Token: 0x06009A25 RID: 39461
		[DispId(-2147416107)]
		public virtual extern string accessKey
		{
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003220 RID: 12832
		// (get) Token: 0x06009A28 RID: 39464
		// (set) Token: 0x06009A27 RID: 39463
		[DispId(-2147412097)]
		public virtual extern object onblur
		{
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003221 RID: 12833
		// (get) Token: 0x06009A2A RID: 39466
		// (set) Token: 0x06009A29 RID: 39465
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003222 RID: 12834
		// (get) Token: 0x06009A2C RID: 39468
		// (set) Token: 0x06009A2B RID: 39467
		[DispId(-2147412076)]
		public virtual extern object onresize
		{
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06009A2D RID: 39469
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06009A2E RID: 39470
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06009A2F RID: 39471
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17003223 RID: 12835
		// (get) Token: 0x06009A30 RID: 39472
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003224 RID: 12836
		// (get) Token: 0x06009A31 RID: 39473
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003225 RID: 12837
		// (get) Token: 0x06009A32 RID: 39474
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003226 RID: 12838
		// (get) Token: 0x06009A33 RID: 39475
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009A34 RID: 39476
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06009A35 RID: 39477
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17003227 RID: 12839
		// (get) Token: 0x06009A36 RID: 39478
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003228 RID: 12840
		// (get) Token: 0x06009A38 RID: 39480
		// (set) Token: 0x06009A37 RID: 39479
		[DispId(-2147412087)]
		public virtual extern object onreadystatechange
		{
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003229 RID: 12841
		// (get) Token: 0x06009A3A RID: 39482
		// (set) Token: 0x06009A39 RID: 39481
		[DispId(-2147412050)]
		public virtual extern object onrowsdelete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700322A RID: 12842
		// (get) Token: 0x06009A3C RID: 39484
		// (set) Token: 0x06009A3B RID: 39483
		[DispId(-2147412049)]
		public virtual extern object onrowsinserted
		{
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700322B RID: 12843
		// (get) Token: 0x06009A3E RID: 39486
		// (set) Token: 0x06009A3D RID: 39485
		[DispId(-2147412048)]
		public virtual extern object oncellchange
		{
			[DispId(-2147412048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700322C RID: 12844
		// (get) Token: 0x06009A40 RID: 39488
		// (set) Token: 0x06009A3F RID: 39487
		[DispId(-2147412995)]
		public virtual extern string dir
		{
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06009A41 RID: 39489
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x1700322D RID: 12845
		// (get) Token: 0x06009A42 RID: 39490
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700322E RID: 12846
		// (get) Token: 0x06009A43 RID: 39491
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700322F RID: 12847
		// (get) Token: 0x06009A45 RID: 39493
		// (set) Token: 0x06009A44 RID: 39492
		[DispId(-2147417053)]
		public virtual extern int scrollTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147417053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17003230 RID: 12848
		// (get) Token: 0x06009A47 RID: 39495
		// (set) Token: 0x06009A46 RID: 39494
		[DispId(-2147417052)]
		public virtual extern int scrollLeft
		{
			[DispId(-2147417052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06009A48 RID: 39496
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17003231 RID: 12849
		// (get) Token: 0x06009A4A RID: 39498
		// (set) Token: 0x06009A49 RID: 39497
		[DispId(-2147412047)]
		public virtual extern object oncontextmenu
		{
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06009A4B RID: 39499
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06009A4C RID: 39500
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06009A4D RID: 39501
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06009A4E RID: 39502
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17003232 RID: 12850
		// (get) Token: 0x06009A4F RID: 39503
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009A50 RID: 39504
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06009A51 RID: 39505
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17003233 RID: 12851
		// (get) Token: 0x06009A52 RID: 39506
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003234 RID: 12852
		// (get) Token: 0x06009A53 RID: 39507
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003235 RID: 12853
		// (get) Token: 0x06009A55 RID: 39509
		// (set) Token: 0x06009A54 RID: 39508
		[DispId(-2147417029)]
		public virtual extern string tagUrn
		{
			[DispId(-2147417029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003236 RID: 12854
		// (get) Token: 0x06009A57 RID: 39511
		// (set) Token: 0x06009A56 RID: 39510
		[DispId(-2147412043)]
		public virtual extern object onbeforeeditfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003237 RID: 12855
		// (get) Token: 0x06009A58 RID: 39512
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009A59 RID: 39513
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06009A5A RID: 39514
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17003238 RID: 12856
		// (get) Token: 0x06009A5B RID: 39515
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003239 RID: 12857
		// (get) Token: 0x06009A5C RID: 39516
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700323A RID: 12858
		// (get) Token: 0x06009A5E RID: 39518
		// (set) Token: 0x06009A5D RID: 39517
		[DispId(-2147412039)]
		public virtual extern object onlayoutcomplete
		{
			[DispId(-2147412039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700323B RID: 12859
		// (get) Token: 0x06009A60 RID: 39520
		// (set) Token: 0x06009A5F RID: 39519
		[DispId(-2147412038)]
		public virtual extern object onpage
		{
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700323C RID: 12860
		// (get) Token: 0x06009A62 RID: 39522
		// (set) Token: 0x06009A61 RID: 39521
		[DispId(-2147417012)]
		public virtual extern bool inflateBlock
		{
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700323D RID: 12861
		// (get) Token: 0x06009A64 RID: 39524
		// (set) Token: 0x06009A63 RID: 39523
		[DispId(-2147412035)]
		public virtual extern object onbeforedeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06009A65 RID: 39525
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x1700323E RID: 12862
		// (get) Token: 0x06009A67 RID: 39527
		// (set) Token: 0x06009A66 RID: 39526
		[DispId(-2147412950)]
		public virtual extern string contentEditable
		{
			[DispId(-2147412950)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412950)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700323F RID: 12863
		// (get) Token: 0x06009A68 RID: 39528
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003240 RID: 12864
		// (get) Token: 0x06009A6A RID: 39530
		// (set) Token: 0x06009A69 RID: 39529
		[DispId(-2147412949)]
		public virtual extern bool hideFocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412949)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17003241 RID: 12865
		// (get) Token: 0x06009A6C RID: 39532
		// (set) Token: 0x06009A6B RID: 39531
		[DispId(-2147418036)]
		public virtual extern bool disabled
		{
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17003242 RID: 12866
		// (get) Token: 0x06009A6D RID: 39533
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003243 RID: 12867
		// (get) Token: 0x06009A6F RID: 39535
		// (set) Token: 0x06009A6E RID: 39534
		[DispId(-2147412034)]
		public virtual extern object onmove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003244 RID: 12868
		// (get) Token: 0x06009A71 RID: 39537
		// (set) Token: 0x06009A70 RID: 39536
		[DispId(-2147412033)]
		public virtual extern object oncontrolselect
		{
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06009A72 RID: 39538
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17003245 RID: 12869
		// (get) Token: 0x06009A74 RID: 39540
		// (set) Token: 0x06009A73 RID: 39539
		[DispId(-2147412029)]
		public virtual extern object onresizestart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003246 RID: 12870
		// (get) Token: 0x06009A76 RID: 39542
		// (set) Token: 0x06009A75 RID: 39541
		[DispId(-2147412028)]
		public virtual extern object onresizeend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003247 RID: 12871
		// (get) Token: 0x06009A78 RID: 39544
		// (set) Token: 0x06009A77 RID: 39543
		[DispId(-2147412031)]
		public virtual extern object onmovestart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412031)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003248 RID: 12872
		// (get) Token: 0x06009A7A RID: 39546
		// (set) Token: 0x06009A79 RID: 39545
		[DispId(-2147412030)]
		public virtual extern object onmoveend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003249 RID: 12873
		// (get) Token: 0x06009A7C RID: 39548
		// (set) Token: 0x06009A7B RID: 39547
		[DispId(-2147412027)]
		public virtual extern object onmouseenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412027)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700324A RID: 12874
		// (get) Token: 0x06009A7E RID: 39550
		// (set) Token: 0x06009A7D RID: 39549
		[DispId(-2147412026)]
		public virtual extern object onmouseleave
		{
			[DispId(-2147412026)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412026)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700324B RID: 12875
		// (get) Token: 0x06009A80 RID: 39552
		// (set) Token: 0x06009A7F RID: 39551
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700324C RID: 12876
		// (get) Token: 0x06009A82 RID: 39554
		// (set) Token: 0x06009A81 RID: 39553
		[DispId(-2147412024)]
		public virtual extern object ondeactivate
		{
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06009A83 RID: 39555
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x1700324D RID: 12877
		// (get) Token: 0x06009A84 RID: 39556
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700324E RID: 12878
		// (get) Token: 0x06009A86 RID: 39558
		// (set) Token: 0x06009A85 RID: 39557
		[DispId(-2147412036)]
		public virtual extern object onmousewheel
		{
			[TypeLibFunc(20)]
			[DispId(-2147412036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06009A87 RID: 39559
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06009A88 RID: 39560
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06009A89 RID: 39561
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06009A8A RID: 39562
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700324F RID: 12879
		// (get) Token: 0x06009A8C RID: 39564
		// (set) Token: 0x06009A8B RID: 39563
		[DispId(-2147412022)]
		public virtual extern object onbeforeactivate
		{
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003250 RID: 12880
		// (get) Token: 0x06009A8E RID: 39566
		// (set) Token: 0x06009A8D RID: 39565
		[DispId(-2147412021)]
		public virtual extern object onfocusin
		{
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003251 RID: 12881
		// (get) Token: 0x06009A90 RID: 39568
		// (set) Token: 0x06009A8F RID: 39567
		[DispId(-2147412020)]
		public virtual extern object onfocusout
		{
			[DispId(-2147412020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003252 RID: 12882
		// (get) Token: 0x06009A91 RID: 39569
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003253 RID: 12883
		// (get) Token: 0x06009A92 RID: 39570
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003254 RID: 12884
		// (get) Token: 0x06009A93 RID: 39571
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003255 RID: 12885
		// (get) Token: 0x06009A94 RID: 39572
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06009A95 RID: 39573
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17003256 RID: 12886
		// (get) Token: 0x06009A96 RID: 39574
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003257 RID: 12887
		// (get) Token: 0x06009A97 RID: 39575
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06009A98 RID: 39576
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06009A99 RID: 39577
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06009A9A RID: 39578
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06009A9B RID: 39579
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06009A9C RID: 39580
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06009A9D RID: 39581
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06009A9E RID: 39582
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06009A9F RID: 39583
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17003258 RID: 12888
		// (get) Token: 0x06009AA0 RID: 39584
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003259 RID: 12889
		// (get) Token: 0x06009AA2 RID: 39586
		// (set) Token: 0x06009AA1 RID: 39585
		[DispId(-2147417037)]
		public virtual extern object nodeValue
		{
			[DispId(-2147417037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147417037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700325A RID: 12890
		// (get) Token: 0x06009AA3 RID: 39587
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700325B RID: 12891
		// (get) Token: 0x06009AA4 RID: 39588
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700325C RID: 12892
		// (get) Token: 0x06009AA5 RID: 39589
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700325D RID: 12893
		// (get) Token: 0x06009AA6 RID: 39590
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700325E RID: 12894
		// (get) Token: 0x06009AA7 RID: 39591
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700325F RID: 12895
		// (get) Token: 0x06009AA9 RID: 39593
		// (set) Token: 0x06009AA8 RID: 39592
		[DispId(-2147417091)]
		public virtual extern string dataFld
		{
			[DispId(-2147417091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003260 RID: 12896
		// (get) Token: 0x06009AAB RID: 39595
		// (set) Token: 0x06009AAA RID: 39594
		[DispId(-2147417090)]
		public virtual extern string dataSrc
		{
			[DispId(-2147417090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003261 RID: 12897
		// (get) Token: 0x06009AAD RID: 39597
		// (set) Token: 0x06009AAC RID: 39596
		[DispId(-2147417089)]
		public virtual extern string dataFormatAs
		{
			[DispId(-2147417089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003262 RID: 12898
		// (get) Token: 0x06009AAE RID: 39598
		[DispId(2000)]
		public virtual extern string type
		{
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003263 RID: 12899
		// (get) Token: 0x06009AB0 RID: 39600
		// (set) Token: 0x06009AAF RID: 39599
		[DispId(-2147413011)]
		public virtual extern string value
		{
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003264 RID: 12900
		// (get) Token: 0x06009AB2 RID: 39602
		// (set) Token: 0x06009AB1 RID: 39601
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003265 RID: 12901
		// (get) Token: 0x06009AB4 RID: 39604
		// (set) Token: 0x06009AB3 RID: 39603
		[DispId(2001)]
		public virtual extern object status
		{
			[DispId(2001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003266 RID: 12902
		// (get) Token: 0x06009AB5 RID: 39605
		[DispId(-2147416108)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003267 RID: 12903
		// (get) Token: 0x06009AB7 RID: 39607
		// (set) Token: 0x06009AB6 RID: 39606
		[DispId(-2147413029)]
		public virtual extern string defaultValue
		{
			[DispId(-2147413029)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147413029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06009AB8 RID: 39608
		[DispId(7005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void select();

		// Token: 0x17003268 RID: 12904
		// (get) Token: 0x06009ABA RID: 39610
		// (set) Token: 0x06009AB9 RID: 39609
		[DispId(-2147412082)]
		public virtual extern object onchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003269 RID: 12905
		// (get) Token: 0x06009ABC RID: 39612
		// (set) Token: 0x06009ABB RID: 39611
		[DispId(-2147412102)]
		public virtual extern object onselect
		{
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700326A RID: 12906
		// (get) Token: 0x06009ABE RID: 39614
		// (set) Token: 0x06009ABD RID: 39613
		[DispId(7004)]
		public virtual extern bool readOnly
		{
			[DispId(7004)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(7004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700326B RID: 12907
		// (get) Token: 0x06009AC0 RID: 39616
		// (set) Token: 0x06009ABF RID: 39615
		[DispId(7001)]
		public virtual extern int rows
		{
			[DispId(7001)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(7001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700326C RID: 12908
		// (get) Token: 0x06009AC2 RID: 39618
		// (set) Token: 0x06009AC1 RID: 39617
		[DispId(7002)]
		public virtual extern int cols
		{
			[DispId(7002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(7002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700326D RID: 12909
		// (get) Token: 0x06009AC4 RID: 39620
		// (set) Token: 0x06009AC3 RID: 39619
		[DispId(7003)]
		public virtual extern string wrap
		{
			[DispId(7003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(7003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06009AC5 RID: 39621
		[DispId(7006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange createTextRange();

		// Token: 0x06009AC6 RID: 39622
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06009AC7 RID: 39623
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06009AC8 RID: 39624
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700326E RID: 12910
		// (get) Token: 0x06009ACA RID: 39626
		// (set) Token: 0x06009AC9 RID: 39625
		public virtual extern string IHTMLElement_className
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700326F RID: 12911
		// (get) Token: 0x06009ACC RID: 39628
		// (set) Token: 0x06009ACB RID: 39627
		public virtual extern string IHTMLElement_id
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003270 RID: 12912
		// (get) Token: 0x06009ACD RID: 39629
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003271 RID: 12913
		// (get) Token: 0x06009ACE RID: 39630
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003272 RID: 12914
		// (get) Token: 0x06009ACF RID: 39631
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003273 RID: 12915
		// (get) Token: 0x06009AD1 RID: 39633
		// (set) Token: 0x06009AD0 RID: 39632
		public virtual extern object IHTMLElement_onhelp
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003274 RID: 12916
		// (get) Token: 0x06009AD3 RID: 39635
		// (set) Token: 0x06009AD2 RID: 39634
		public virtual extern object IHTMLElement_onclick
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003275 RID: 12917
		// (get) Token: 0x06009AD5 RID: 39637
		// (set) Token: 0x06009AD4 RID: 39636
		public virtual extern object IHTMLElement_ondblclick
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003276 RID: 12918
		// (get) Token: 0x06009AD7 RID: 39639
		// (set) Token: 0x06009AD6 RID: 39638
		public virtual extern object IHTMLElement_onkeydown
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003277 RID: 12919
		// (get) Token: 0x06009AD9 RID: 39641
		// (set) Token: 0x06009AD8 RID: 39640
		public virtual extern object IHTMLElement_onkeyup
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003278 RID: 12920
		// (get) Token: 0x06009ADB RID: 39643
		// (set) Token: 0x06009ADA RID: 39642
		public virtual extern object IHTMLElement_onkeypress
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003279 RID: 12921
		// (get) Token: 0x06009ADD RID: 39645
		// (set) Token: 0x06009ADC RID: 39644
		public virtual extern object IHTMLElement_onmouseout
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700327A RID: 12922
		// (get) Token: 0x06009ADF RID: 39647
		// (set) Token: 0x06009ADE RID: 39646
		public virtual extern object IHTMLElement_onmouseover
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700327B RID: 12923
		// (get) Token: 0x06009AE1 RID: 39649
		// (set) Token: 0x06009AE0 RID: 39648
		public virtual extern object IHTMLElement_onmousemove
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700327C RID: 12924
		// (get) Token: 0x06009AE3 RID: 39651
		// (set) Token: 0x06009AE2 RID: 39650
		public virtual extern object IHTMLElement_onmousedown
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700327D RID: 12925
		// (get) Token: 0x06009AE5 RID: 39653
		// (set) Token: 0x06009AE4 RID: 39652
		public virtual extern object IHTMLElement_onmouseup
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700327E RID: 12926
		// (get) Token: 0x06009AE6 RID: 39654
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700327F RID: 12927
		// (get) Token: 0x06009AE8 RID: 39656
		// (set) Token: 0x06009AE7 RID: 39655
		public virtual extern string IHTMLElement_title
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003280 RID: 12928
		// (get) Token: 0x06009AEA RID: 39658
		// (set) Token: 0x06009AE9 RID: 39657
		public virtual extern string IHTMLElement_language
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003281 RID: 12929
		// (get) Token: 0x06009AEC RID: 39660
		// (set) Token: 0x06009AEB RID: 39659
		public virtual extern object IHTMLElement_onselectstart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009AED RID: 39661
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06009AEE RID: 39662
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17003282 RID: 12930
		// (get) Token: 0x06009AEF RID: 39663
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003283 RID: 12931
		// (get) Token: 0x06009AF0 RID: 39664
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003284 RID: 12932
		// (get) Token: 0x06009AF2 RID: 39666
		// (set) Token: 0x06009AF1 RID: 39665
		public virtual extern string IHTMLElement_lang
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003285 RID: 12933
		// (get) Token: 0x06009AF3 RID: 39667
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003286 RID: 12934
		// (get) Token: 0x06009AF4 RID: 39668
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003287 RID: 12935
		// (get) Token: 0x06009AF5 RID: 39669
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003288 RID: 12936
		// (get) Token: 0x06009AF6 RID: 39670
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003289 RID: 12937
		// (get) Token: 0x06009AF7 RID: 39671
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700328A RID: 12938
		// (get) Token: 0x06009AF9 RID: 39673
		// (set) Token: 0x06009AF8 RID: 39672
		public virtual extern string IHTMLElement_innerHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700328B RID: 12939
		// (get) Token: 0x06009AFB RID: 39675
		// (set) Token: 0x06009AFA RID: 39674
		public virtual extern string IHTMLElement_innerText
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700328C RID: 12940
		// (get) Token: 0x06009AFD RID: 39677
		// (set) Token: 0x06009AFC RID: 39676
		public virtual extern string IHTMLElement_outerHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700328D RID: 12941
		// (get) Token: 0x06009AFF RID: 39679
		// (set) Token: 0x06009AFE RID: 39678
		public virtual extern string IHTMLElement_outerText
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06009B00 RID: 39680
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06009B01 RID: 39681
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x1700328E RID: 12942
		// (get) Token: 0x06009B02 RID: 39682
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700328F RID: 12943
		// (get) Token: 0x06009B03 RID: 39683
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009B04 RID: 39684
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17003290 RID: 12944
		// (get) Token: 0x06009B05 RID: 39685
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003291 RID: 12945
		// (get) Token: 0x06009B07 RID: 39687
		// (set) Token: 0x06009B06 RID: 39686
		public virtual extern object IHTMLElement_ondragstart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009B08 RID: 39688
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17003292 RID: 12946
		// (get) Token: 0x06009B0A RID: 39690
		// (set) Token: 0x06009B09 RID: 39689
		public virtual extern object IHTMLElement_onbeforeupdate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003293 RID: 12947
		// (get) Token: 0x06009B0C RID: 39692
		// (set) Token: 0x06009B0B RID: 39691
		public virtual extern object IHTMLElement_onafterupdate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003294 RID: 12948
		// (get) Token: 0x06009B0E RID: 39694
		// (set) Token: 0x06009B0D RID: 39693
		public virtual extern object IHTMLElement_onerrorupdate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003295 RID: 12949
		// (get) Token: 0x06009B10 RID: 39696
		// (set) Token: 0x06009B0F RID: 39695
		public virtual extern object IHTMLElement_onrowexit
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003296 RID: 12950
		// (get) Token: 0x06009B12 RID: 39698
		// (set) Token: 0x06009B11 RID: 39697
		public virtual extern object IHTMLElement_onrowenter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003297 RID: 12951
		// (get) Token: 0x06009B14 RID: 39700
		// (set) Token: 0x06009B13 RID: 39699
		public virtual extern object IHTMLElement_ondatasetchanged
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003298 RID: 12952
		// (get) Token: 0x06009B16 RID: 39702
		// (set) Token: 0x06009B15 RID: 39701
		public virtual extern object IHTMLElement_ondataavailable
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003299 RID: 12953
		// (get) Token: 0x06009B18 RID: 39704
		// (set) Token: 0x06009B17 RID: 39703
		public virtual extern object IHTMLElement_ondatasetcomplete
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700329A RID: 12954
		// (get) Token: 0x06009B1A RID: 39706
		// (set) Token: 0x06009B19 RID: 39705
		public virtual extern object IHTMLElement_onfilterchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700329B RID: 12955
		// (get) Token: 0x06009B1B RID: 39707
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700329C RID: 12956
		// (get) Token: 0x06009B1C RID: 39708
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700329D RID: 12957
		// (get) Token: 0x06009B1D RID: 39709
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06009B1E RID: 39710
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06009B1F RID: 39711
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x1700329E RID: 12958
		// (get) Token: 0x06009B21 RID: 39713
		// (set) Token: 0x06009B20 RID: 39712
		public virtual extern object IHTMLElement2_onlosecapture
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009B22 RID: 39714
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06009B23 RID: 39715
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700329F RID: 12959
		// (get) Token: 0x06009B25 RID: 39717
		// (set) Token: 0x06009B24 RID: 39716
		public virtual extern object IHTMLElement2_onscroll
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032A0 RID: 12960
		// (get) Token: 0x06009B27 RID: 39719
		// (set) Token: 0x06009B26 RID: 39718
		public virtual extern object IHTMLElement2_ondrag
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032A1 RID: 12961
		// (get) Token: 0x06009B29 RID: 39721
		// (set) Token: 0x06009B28 RID: 39720
		public virtual extern object IHTMLElement2_ondragend
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032A2 RID: 12962
		// (get) Token: 0x06009B2B RID: 39723
		// (set) Token: 0x06009B2A RID: 39722
		public virtual extern object IHTMLElement2_ondragenter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032A3 RID: 12963
		// (get) Token: 0x06009B2D RID: 39725
		// (set) Token: 0x06009B2C RID: 39724
		public virtual extern object IHTMLElement2_ondragover
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032A4 RID: 12964
		// (get) Token: 0x06009B2F RID: 39727
		// (set) Token: 0x06009B2E RID: 39726
		public virtual extern object IHTMLElement2_ondragleave
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032A5 RID: 12965
		// (get) Token: 0x06009B31 RID: 39729
		// (set) Token: 0x06009B30 RID: 39728
		public virtual extern object IHTMLElement2_ondrop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032A6 RID: 12966
		// (get) Token: 0x06009B33 RID: 39731
		// (set) Token: 0x06009B32 RID: 39730
		public virtual extern object IHTMLElement2_onbeforecut
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032A7 RID: 12967
		// (get) Token: 0x06009B35 RID: 39733
		// (set) Token: 0x06009B34 RID: 39732
		public virtual extern object IHTMLElement2_oncut
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032A8 RID: 12968
		// (get) Token: 0x06009B37 RID: 39735
		// (set) Token: 0x06009B36 RID: 39734
		public virtual extern object IHTMLElement2_onbeforecopy
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032A9 RID: 12969
		// (get) Token: 0x06009B39 RID: 39737
		// (set) Token: 0x06009B38 RID: 39736
		public virtual extern object IHTMLElement2_oncopy
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032AA RID: 12970
		// (get) Token: 0x06009B3B RID: 39739
		// (set) Token: 0x06009B3A RID: 39738
		public virtual extern object IHTMLElement2_onbeforepaste
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032AB RID: 12971
		// (get) Token: 0x06009B3D RID: 39741
		// (set) Token: 0x06009B3C RID: 39740
		public virtual extern object IHTMLElement2_onpaste
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032AC RID: 12972
		// (get) Token: 0x06009B3E RID: 39742
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170032AD RID: 12973
		// (get) Token: 0x06009B40 RID: 39744
		// (set) Token: 0x06009B3F RID: 39743
		public virtual extern object IHTMLElement2_onpropertychange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009B41 RID: 39745
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06009B42 RID: 39746
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06009B43 RID: 39747
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06009B44 RID: 39748
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06009B45 RID: 39749
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170032AE RID: 12974
		// (get) Token: 0x06009B47 RID: 39751
		// (set) Token: 0x06009B46 RID: 39750
		public virtual extern short IHTMLElement2_tabIndex
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06009B48 RID: 39752
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x170032AF RID: 12975
		// (get) Token: 0x06009B4A RID: 39754
		// (set) Token: 0x06009B49 RID: 39753
		public virtual extern string IHTMLElement2_accessKey
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170032B0 RID: 12976
		// (get) Token: 0x06009B4C RID: 39756
		// (set) Token: 0x06009B4B RID: 39755
		public virtual extern object IHTMLElement2_onblur
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032B1 RID: 12977
		// (get) Token: 0x06009B4E RID: 39758
		// (set) Token: 0x06009B4D RID: 39757
		public virtual extern object IHTMLElement2_onfocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032B2 RID: 12978
		// (get) Token: 0x06009B50 RID: 39760
		// (set) Token: 0x06009B4F RID: 39759
		public virtual extern object IHTMLElement2_onresize
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009B51 RID: 39761
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06009B52 RID: 39762
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06009B53 RID: 39763
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170032B3 RID: 12979
		// (get) Token: 0x06009B54 RID: 39764
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032B4 RID: 12980
		// (get) Token: 0x06009B55 RID: 39765
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032B5 RID: 12981
		// (get) Token: 0x06009B56 RID: 39766
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032B6 RID: 12982
		// (get) Token: 0x06009B57 RID: 39767
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009B58 RID: 39768
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06009B59 RID: 39769
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170032B7 RID: 12983
		// (get) Token: 0x06009B5A RID: 39770
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170032B8 RID: 12984
		// (get) Token: 0x06009B5C RID: 39772
		// (set) Token: 0x06009B5B RID: 39771
		public virtual extern object IHTMLElement2_onreadystatechange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032B9 RID: 12985
		// (get) Token: 0x06009B5E RID: 39774
		// (set) Token: 0x06009B5D RID: 39773
		public virtual extern object IHTMLElement2_onrowsdelete
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032BA RID: 12986
		// (get) Token: 0x06009B60 RID: 39776
		// (set) Token: 0x06009B5F RID: 39775
		public virtual extern object IHTMLElement2_onrowsinserted
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032BB RID: 12987
		// (get) Token: 0x06009B62 RID: 39778
		// (set) Token: 0x06009B61 RID: 39777
		public virtual extern object IHTMLElement2_oncellchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032BC RID: 12988
		// (get) Token: 0x06009B64 RID: 39780
		// (set) Token: 0x06009B63 RID: 39779
		public virtual extern string IHTMLElement2_dir
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06009B65 RID: 39781
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x170032BD RID: 12989
		// (get) Token: 0x06009B66 RID: 39782
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032BE RID: 12990
		// (get) Token: 0x06009B67 RID: 39783
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032BF RID: 12991
		// (get) Token: 0x06009B69 RID: 39785
		// (set) Token: 0x06009B68 RID: 39784
		public virtual extern int IHTMLElement2_scrollTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170032C0 RID: 12992
		// (get) Token: 0x06009B6B RID: 39787
		// (set) Token: 0x06009B6A RID: 39786
		public virtual extern int IHTMLElement2_scrollLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06009B6C RID: 39788
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06009B6D RID: 39789
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x170032C1 RID: 12993
		// (get) Token: 0x06009B6F RID: 39791
		// (set) Token: 0x06009B6E RID: 39790
		public virtual extern object IHTMLElement2_oncontextmenu
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009B70 RID: 39792
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06009B71 RID: 39793
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06009B72 RID: 39794
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06009B73 RID: 39795
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170032C2 RID: 12994
		// (get) Token: 0x06009B74 RID: 39796
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009B75 RID: 39797
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06009B76 RID: 39798
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x170032C3 RID: 12995
		// (get) Token: 0x06009B77 RID: 39799
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170032C4 RID: 12996
		// (get) Token: 0x06009B78 RID: 39800
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170032C5 RID: 12997
		// (get) Token: 0x06009B7A RID: 39802
		// (set) Token: 0x06009B79 RID: 39801
		public virtual extern string IHTMLElement2_tagUrn
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170032C6 RID: 12998
		// (get) Token: 0x06009B7C RID: 39804
		// (set) Token: 0x06009B7B RID: 39803
		public virtual extern object IHTMLElement2_onbeforeeditfocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032C7 RID: 12999
		// (get) Token: 0x06009B7D RID: 39805
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009B7E RID: 39806
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06009B7F RID: 39807
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170032C8 RID: 13000
		// (get) Token: 0x06009B80 RID: 39808
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032C9 RID: 13001
		// (get) Token: 0x06009B81 RID: 39809
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032CA RID: 13002
		// (get) Token: 0x06009B83 RID: 39811
		// (set) Token: 0x06009B82 RID: 39810
		public virtual extern object IHTMLElement3_onlayoutcomplete
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032CB RID: 13003
		// (get) Token: 0x06009B85 RID: 39813
		// (set) Token: 0x06009B84 RID: 39812
		public virtual extern object IHTMLElement3_onpage
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032CC RID: 13004
		// (get) Token: 0x06009B87 RID: 39815
		// (set) Token: 0x06009B86 RID: 39814
		public virtual extern bool IHTMLElement3_inflateBlock
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170032CD RID: 13005
		// (get) Token: 0x06009B89 RID: 39817
		// (set) Token: 0x06009B88 RID: 39816
		public virtual extern object IHTMLElement3_onbeforedeactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009B8A RID: 39818
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x170032CE RID: 13006
		// (get) Token: 0x06009B8C RID: 39820
		// (set) Token: 0x06009B8B RID: 39819
		public virtual extern string IHTMLElement3_contentEditable
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170032CF RID: 13007
		// (get) Token: 0x06009B8D RID: 39821
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032D0 RID: 13008
		// (get) Token: 0x06009B8F RID: 39823
		// (set) Token: 0x06009B8E RID: 39822
		public virtual extern bool IHTMLElement3_hideFocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170032D1 RID: 13009
		// (get) Token: 0x06009B91 RID: 39825
		// (set) Token: 0x06009B90 RID: 39824
		public virtual extern bool IHTMLElement3_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170032D2 RID: 13010
		// (get) Token: 0x06009B92 RID: 39826
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032D3 RID: 13011
		// (get) Token: 0x06009B94 RID: 39828
		// (set) Token: 0x06009B93 RID: 39827
		public virtual extern object IHTMLElement3_onmove
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032D4 RID: 13012
		// (get) Token: 0x06009B96 RID: 39830
		// (set) Token: 0x06009B95 RID: 39829
		public virtual extern object IHTMLElement3_oncontrolselect
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009B97 RID: 39831
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170032D5 RID: 13013
		// (get) Token: 0x06009B99 RID: 39833
		// (set) Token: 0x06009B98 RID: 39832
		public virtual extern object IHTMLElement3_onresizestart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032D6 RID: 13014
		// (get) Token: 0x06009B9B RID: 39835
		// (set) Token: 0x06009B9A RID: 39834
		public virtual extern object IHTMLElement3_onresizeend
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032D7 RID: 13015
		// (get) Token: 0x06009B9D RID: 39837
		// (set) Token: 0x06009B9C RID: 39836
		public virtual extern object IHTMLElement3_onmovestart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032D8 RID: 13016
		// (get) Token: 0x06009B9F RID: 39839
		// (set) Token: 0x06009B9E RID: 39838
		public virtual extern object IHTMLElement3_onmoveend
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032D9 RID: 13017
		// (get) Token: 0x06009BA1 RID: 39841
		// (set) Token: 0x06009BA0 RID: 39840
		public virtual extern object IHTMLElement3_onmouseenter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032DA RID: 13018
		// (get) Token: 0x06009BA3 RID: 39843
		// (set) Token: 0x06009BA2 RID: 39842
		public virtual extern object IHTMLElement3_onmouseleave
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032DB RID: 13019
		// (get) Token: 0x06009BA5 RID: 39845
		// (set) Token: 0x06009BA4 RID: 39844
		public virtual extern object IHTMLElement3_onactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032DC RID: 13020
		// (get) Token: 0x06009BA7 RID: 39847
		// (set) Token: 0x06009BA6 RID: 39846
		public virtual extern object IHTMLElement3_ondeactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009BA8 RID: 39848
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x170032DD RID: 13021
		// (get) Token: 0x06009BA9 RID: 39849
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032DE RID: 13022
		// (get) Token: 0x06009BAB RID: 39851
		// (set) Token: 0x06009BAA RID: 39850
		public virtual extern object IHTMLElement4_onmousewheel
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009BAC RID: 39852
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06009BAD RID: 39853
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06009BAE RID: 39854
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06009BAF RID: 39855
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x170032DF RID: 13023
		// (get) Token: 0x06009BB1 RID: 39857
		// (set) Token: 0x06009BB0 RID: 39856
		public virtual extern object IHTMLElement4_onbeforeactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032E0 RID: 13024
		// (get) Token: 0x06009BB3 RID: 39859
		// (set) Token: 0x06009BB2 RID: 39858
		public virtual extern object IHTMLElement4_onfocusin
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032E1 RID: 13025
		// (get) Token: 0x06009BB5 RID: 39861
		// (set) Token: 0x06009BB4 RID: 39860
		public virtual extern object IHTMLElement4_onfocusout
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032E2 RID: 13026
		// (get) Token: 0x06009BB6 RID: 39862
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032E3 RID: 13027
		// (get) Token: 0x06009BB7 RID: 39863
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170032E4 RID: 13028
		// (get) Token: 0x06009BB8 RID: 39864
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032E5 RID: 13029
		// (get) Token: 0x06009BB9 RID: 39865
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06009BBA RID: 39866
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x170032E6 RID: 13030
		// (get) Token: 0x06009BBB RID: 39867
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170032E7 RID: 13031
		// (get) Token: 0x06009BBC RID: 39868
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06009BBD RID: 39869
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06009BBE RID: 39870
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06009BBF RID: 39871
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06009BC0 RID: 39872
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06009BC1 RID: 39873
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06009BC2 RID: 39874
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06009BC3 RID: 39875
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06009BC4 RID: 39876
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170032E8 RID: 13032
		// (get) Token: 0x06009BC5 RID: 39877
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170032E9 RID: 13033
		// (get) Token: 0x06009BC7 RID: 39879
		// (set) Token: 0x06009BC6 RID: 39878
		public virtual extern object IHTMLDOMNode_nodeValue
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032EA RID: 13034
		// (get) Token: 0x06009BC8 RID: 39880
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170032EB RID: 13035
		// (get) Token: 0x06009BC9 RID: 39881
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170032EC RID: 13036
		// (get) Token: 0x06009BCA RID: 39882
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170032ED RID: 13037
		// (get) Token: 0x06009BCB RID: 39883
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170032EE RID: 13038
		// (get) Token: 0x06009BCC RID: 39884
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170032EF RID: 13039
		// (get) Token: 0x06009BCE RID: 39886
		// (set) Token: 0x06009BCD RID: 39885
		public virtual extern string IHTMLDatabinding_dataFld
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170032F0 RID: 13040
		// (get) Token: 0x06009BD0 RID: 39888
		// (set) Token: 0x06009BCF RID: 39887
		public virtual extern string IHTMLDatabinding_dataSrc
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170032F1 RID: 13041
		// (get) Token: 0x06009BD2 RID: 39890
		// (set) Token: 0x06009BD1 RID: 39889
		public virtual extern string IHTMLDatabinding_dataFormatAs
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170032F2 RID: 13042
		// (get) Token: 0x06009BD4 RID: 39892
		// (set) Token: 0x06009BD3 RID: 39891
		public virtual extern short IHTMLControlElement_tabIndex
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06009BD5 RID: 39893
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x170032F3 RID: 13043
		// (get) Token: 0x06009BD7 RID: 39895
		// (set) Token: 0x06009BD6 RID: 39894
		public virtual extern string IHTMLControlElement_accessKey
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170032F4 RID: 13044
		// (get) Token: 0x06009BD9 RID: 39897
		// (set) Token: 0x06009BD8 RID: 39896
		public virtual extern object IHTMLControlElement_onblur
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032F5 RID: 13045
		// (get) Token: 0x06009BDB RID: 39899
		// (set) Token: 0x06009BDA RID: 39898
		public virtual extern object IHTMLControlElement_onfocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170032F6 RID: 13046
		// (get) Token: 0x06009BDD RID: 39901
		// (set) Token: 0x06009BDC RID: 39900
		public virtual extern object IHTMLControlElement_onresize
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06009BDE RID: 39902
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06009BDF RID: 39903
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06009BE0 RID: 39904
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170032F7 RID: 13047
		// (get) Token: 0x06009BE1 RID: 39905
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032F8 RID: 13048
		// (get) Token: 0x06009BE2 RID: 39906
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032F9 RID: 13049
		// (get) Token: 0x06009BE3 RID: 39907
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032FA RID: 13050
		// (get) Token: 0x06009BE4 RID: 39908
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009BE5 RID: 39909
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTextContainer_createControlRange();

		// Token: 0x170032FB RID: 13051
		// (get) Token: 0x06009BE6 RID: 39910
		public virtual extern int IHTMLTextContainer_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032FC RID: 13052
		// (get) Token: 0x06009BE7 RID: 39911
		public virtual extern int IHTMLTextContainer_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170032FD RID: 13053
		// (get) Token: 0x06009BE9 RID: 39913
		// (set) Token: 0x06009BE8 RID: 39912
		public virtual extern int IHTMLTextContainer_scrollTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170032FE RID: 13054
		// (get) Token: 0x06009BEB RID: 39915
		// (set) Token: 0x06009BEA RID: 39914
		public virtual extern int IHTMLTextContainer_scrollLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170032FF RID: 13055
		// (get) Token: 0x06009BED RID: 39917
		// (set) Token: 0x06009BEC RID: 39916
		public virtual extern object IHTMLTextContainer_onscroll
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003300 RID: 13056
		// (get) Token: 0x06009BEE RID: 39918
		public virtual extern string IHTMLTextAreaElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003301 RID: 13057
		// (get) Token: 0x06009BF0 RID: 39920
		// (set) Token: 0x06009BEF RID: 39919
		public virtual extern string IHTMLTextAreaElement_value
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003302 RID: 13058
		// (get) Token: 0x06009BF2 RID: 39922
		// (set) Token: 0x06009BF1 RID: 39921
		public virtual extern string IHTMLTextAreaElement_name
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003303 RID: 13059
		// (get) Token: 0x06009BF4 RID: 39924
		// (set) Token: 0x06009BF3 RID: 39923
		public virtual extern object IHTMLTextAreaElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003304 RID: 13060
		// (get) Token: 0x06009BF6 RID: 39926
		// (set) Token: 0x06009BF5 RID: 39925
		public virtual extern bool IHTMLTextAreaElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003305 RID: 13061
		// (get) Token: 0x06009BF7 RID: 39927
		public virtual extern IHTMLFormElement IHTMLTextAreaElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003306 RID: 13062
		// (get) Token: 0x06009BF9 RID: 39929
		// (set) Token: 0x06009BF8 RID: 39928
		public virtual extern string IHTMLTextAreaElement_defaultValue
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06009BFA RID: 39930
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTextAreaElement_select();

		// Token: 0x17003307 RID: 13063
		// (get) Token: 0x06009BFC RID: 39932
		// (set) Token: 0x06009BFB RID: 39931
		public virtual extern object IHTMLTextAreaElement_onchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003308 RID: 13064
		// (get) Token: 0x06009BFE RID: 39934
		// (set) Token: 0x06009BFD RID: 39933
		public virtual extern object IHTMLTextAreaElement_onselect
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003309 RID: 13065
		// (get) Token: 0x06009C00 RID: 39936
		// (set) Token: 0x06009BFF RID: 39935
		public virtual extern bool IHTMLTextAreaElement_readOnly
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700330A RID: 13066
		// (get) Token: 0x06009C02 RID: 39938
		// (set) Token: 0x06009C01 RID: 39937
		public virtual extern int IHTMLTextAreaElement_rows
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700330B RID: 13067
		// (get) Token: 0x06009C04 RID: 39940
		// (set) Token: 0x06009C03 RID: 39939
		public virtual extern int IHTMLTextAreaElement_cols
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700330C RID: 13068
		// (get) Token: 0x06009C06 RID: 39942
		// (set) Token: 0x06009C05 RID: 39941
		public virtual extern string IHTMLTextAreaElement_wrap
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06009C07 RID: 39943
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange IHTMLTextAreaElement_createTextRange();

		// Token: 0x1400125E RID: 4702
		// (add) Token: 0x06009C08 RID: 39944
		// (remove) Token: 0x06009C09 RID: 39945
		public virtual extern event HTMLInputTextElementEvents_onhelpEventHandler HTMLInputTextElementEvents_Event_onhelp;

		// Token: 0x1400125F RID: 4703
		// (add) Token: 0x06009C0A RID: 39946
		// (remove) Token: 0x06009C0B RID: 39947
		public virtual extern event HTMLInputTextElementEvents_onclickEventHandler HTMLInputTextElementEvents_Event_onclick;

		// Token: 0x14001260 RID: 4704
		// (add) Token: 0x06009C0C RID: 39948
		// (remove) Token: 0x06009C0D RID: 39949
		public virtual extern event HTMLInputTextElementEvents_ondblclickEventHandler HTMLInputTextElementEvents_Event_ondblclick;

		// Token: 0x14001261 RID: 4705
		// (add) Token: 0x06009C0E RID: 39950
		// (remove) Token: 0x06009C0F RID: 39951
		public virtual extern event HTMLInputTextElementEvents_onkeypressEventHandler HTMLInputTextElementEvents_Event_onkeypress;

		// Token: 0x14001262 RID: 4706
		// (add) Token: 0x06009C10 RID: 39952
		// (remove) Token: 0x06009C11 RID: 39953
		public virtual extern event HTMLInputTextElementEvents_onkeydownEventHandler HTMLInputTextElementEvents_Event_onkeydown;

		// Token: 0x14001263 RID: 4707
		// (add) Token: 0x06009C12 RID: 39954
		// (remove) Token: 0x06009C13 RID: 39955
		public virtual extern event HTMLInputTextElementEvents_onkeyupEventHandler HTMLInputTextElementEvents_Event_onkeyup;

		// Token: 0x14001264 RID: 4708
		// (add) Token: 0x06009C14 RID: 39956
		// (remove) Token: 0x06009C15 RID: 39957
		public virtual extern event HTMLInputTextElementEvents_onmouseoutEventHandler HTMLInputTextElementEvents_Event_onmouseout;

		// Token: 0x14001265 RID: 4709
		// (add) Token: 0x06009C16 RID: 39958
		// (remove) Token: 0x06009C17 RID: 39959
		public virtual extern event HTMLInputTextElementEvents_onmouseoverEventHandler HTMLInputTextElementEvents_Event_onmouseover;

		// Token: 0x14001266 RID: 4710
		// (add) Token: 0x06009C18 RID: 39960
		// (remove) Token: 0x06009C19 RID: 39961
		public virtual extern event HTMLInputTextElementEvents_onmousemoveEventHandler HTMLInputTextElementEvents_Event_onmousemove;

		// Token: 0x14001267 RID: 4711
		// (add) Token: 0x06009C1A RID: 39962
		// (remove) Token: 0x06009C1B RID: 39963
		public virtual extern event HTMLInputTextElementEvents_onmousedownEventHandler HTMLInputTextElementEvents_Event_onmousedown;

		// Token: 0x14001268 RID: 4712
		// (add) Token: 0x06009C1C RID: 39964
		// (remove) Token: 0x06009C1D RID: 39965
		public virtual extern event HTMLInputTextElementEvents_onmouseupEventHandler HTMLInputTextElementEvents_Event_onmouseup;

		// Token: 0x14001269 RID: 4713
		// (add) Token: 0x06009C1E RID: 39966
		// (remove) Token: 0x06009C1F RID: 39967
		public virtual extern event HTMLInputTextElementEvents_onselectstartEventHandler HTMLInputTextElementEvents_Event_onselectstart;

		// Token: 0x1400126A RID: 4714
		// (add) Token: 0x06009C20 RID: 39968
		// (remove) Token: 0x06009C21 RID: 39969
		public virtual extern event HTMLInputTextElementEvents_onfilterchangeEventHandler HTMLInputTextElementEvents_Event_onfilterchange;

		// Token: 0x1400126B RID: 4715
		// (add) Token: 0x06009C22 RID: 39970
		// (remove) Token: 0x06009C23 RID: 39971
		public virtual extern event HTMLInputTextElementEvents_ondragstartEventHandler HTMLInputTextElementEvents_Event_ondragstart;

		// Token: 0x1400126C RID: 4716
		// (add) Token: 0x06009C24 RID: 39972
		// (remove) Token: 0x06009C25 RID: 39973
		public virtual extern event HTMLInputTextElementEvents_onbeforeupdateEventHandler HTMLInputTextElementEvents_Event_onbeforeupdate;

		// Token: 0x1400126D RID: 4717
		// (add) Token: 0x06009C26 RID: 39974
		// (remove) Token: 0x06009C27 RID: 39975
		public virtual extern event HTMLInputTextElementEvents_onafterupdateEventHandler HTMLInputTextElementEvents_Event_onafterupdate;

		// Token: 0x1400126E RID: 4718
		// (add) Token: 0x06009C28 RID: 39976
		// (remove) Token: 0x06009C29 RID: 39977
		public virtual extern event HTMLInputTextElementEvents_onerrorupdateEventHandler HTMLInputTextElementEvents_Event_onerrorupdate;

		// Token: 0x1400126F RID: 4719
		// (add) Token: 0x06009C2A RID: 39978
		// (remove) Token: 0x06009C2B RID: 39979
		public virtual extern event HTMLInputTextElementEvents_onrowexitEventHandler HTMLInputTextElementEvents_Event_onrowexit;

		// Token: 0x14001270 RID: 4720
		// (add) Token: 0x06009C2C RID: 39980
		// (remove) Token: 0x06009C2D RID: 39981
		public virtual extern event HTMLInputTextElementEvents_onrowenterEventHandler HTMLInputTextElementEvents_Event_onrowenter;

		// Token: 0x14001271 RID: 4721
		// (add) Token: 0x06009C2E RID: 39982
		// (remove) Token: 0x06009C2F RID: 39983
		public virtual extern event HTMLInputTextElementEvents_ondatasetchangedEventHandler HTMLInputTextElementEvents_Event_ondatasetchanged;

		// Token: 0x14001272 RID: 4722
		// (add) Token: 0x06009C30 RID: 39984
		// (remove) Token: 0x06009C31 RID: 39985
		public virtual extern event HTMLInputTextElementEvents_ondataavailableEventHandler HTMLInputTextElementEvents_Event_ondataavailable;

		// Token: 0x14001273 RID: 4723
		// (add) Token: 0x06009C32 RID: 39986
		// (remove) Token: 0x06009C33 RID: 39987
		public virtual extern event HTMLInputTextElementEvents_ondatasetcompleteEventHandler HTMLInputTextElementEvents_Event_ondatasetcomplete;

		// Token: 0x14001274 RID: 4724
		// (add) Token: 0x06009C34 RID: 39988
		// (remove) Token: 0x06009C35 RID: 39989
		public virtual extern event HTMLInputTextElementEvents_onlosecaptureEventHandler HTMLInputTextElementEvents_Event_onlosecapture;

		// Token: 0x14001275 RID: 4725
		// (add) Token: 0x06009C36 RID: 39990
		// (remove) Token: 0x06009C37 RID: 39991
		public virtual extern event HTMLInputTextElementEvents_onpropertychangeEventHandler HTMLInputTextElementEvents_Event_onpropertychange;

		// Token: 0x14001276 RID: 4726
		// (add) Token: 0x06009C38 RID: 39992
		// (remove) Token: 0x06009C39 RID: 39993
		public virtual extern event HTMLInputTextElementEvents_onscrollEventHandler HTMLInputTextElementEvents_Event_onscroll;

		// Token: 0x14001277 RID: 4727
		// (add) Token: 0x06009C3A RID: 39994
		// (remove) Token: 0x06009C3B RID: 39995
		public virtual extern event HTMLInputTextElementEvents_onfocusEventHandler HTMLInputTextElementEvents_Event_onfocus;

		// Token: 0x14001278 RID: 4728
		// (add) Token: 0x06009C3C RID: 39996
		// (remove) Token: 0x06009C3D RID: 39997
		public virtual extern event HTMLInputTextElementEvents_onblurEventHandler HTMLInputTextElementEvents_Event_onblur;

		// Token: 0x14001279 RID: 4729
		// (add) Token: 0x06009C3E RID: 39998
		// (remove) Token: 0x06009C3F RID: 39999
		public virtual extern event HTMLInputTextElementEvents_onresizeEventHandler HTMLInputTextElementEvents_Event_onresize;

		// Token: 0x1400127A RID: 4730
		// (add) Token: 0x06009C40 RID: 40000
		// (remove) Token: 0x06009C41 RID: 40001
		public virtual extern event HTMLInputTextElementEvents_ondragEventHandler HTMLInputTextElementEvents_Event_ondrag;

		// Token: 0x1400127B RID: 4731
		// (add) Token: 0x06009C42 RID: 40002
		// (remove) Token: 0x06009C43 RID: 40003
		public virtual extern event HTMLInputTextElementEvents_ondragendEventHandler HTMLInputTextElementEvents_Event_ondragend;

		// Token: 0x1400127C RID: 4732
		// (add) Token: 0x06009C44 RID: 40004
		// (remove) Token: 0x06009C45 RID: 40005
		public virtual extern event HTMLInputTextElementEvents_ondragenterEventHandler HTMLInputTextElementEvents_Event_ondragenter;

		// Token: 0x1400127D RID: 4733
		// (add) Token: 0x06009C46 RID: 40006
		// (remove) Token: 0x06009C47 RID: 40007
		public virtual extern event HTMLInputTextElementEvents_ondragoverEventHandler HTMLInputTextElementEvents_Event_ondragover;

		// Token: 0x1400127E RID: 4734
		// (add) Token: 0x06009C48 RID: 40008
		// (remove) Token: 0x06009C49 RID: 40009
		public virtual extern event HTMLInputTextElementEvents_ondragleaveEventHandler HTMLInputTextElementEvents_Event_ondragleave;

		// Token: 0x1400127F RID: 4735
		// (add) Token: 0x06009C4A RID: 40010
		// (remove) Token: 0x06009C4B RID: 40011
		public virtual extern event HTMLInputTextElementEvents_ondropEventHandler HTMLInputTextElementEvents_Event_ondrop;

		// Token: 0x14001280 RID: 4736
		// (add) Token: 0x06009C4C RID: 40012
		// (remove) Token: 0x06009C4D RID: 40013
		public virtual extern event HTMLInputTextElementEvents_onbeforecutEventHandler HTMLInputTextElementEvents_Event_onbeforecut;

		// Token: 0x14001281 RID: 4737
		// (add) Token: 0x06009C4E RID: 40014
		// (remove) Token: 0x06009C4F RID: 40015
		public virtual extern event HTMLInputTextElementEvents_oncutEventHandler HTMLInputTextElementEvents_Event_oncut;

		// Token: 0x14001282 RID: 4738
		// (add) Token: 0x06009C50 RID: 40016
		// (remove) Token: 0x06009C51 RID: 40017
		public virtual extern event HTMLInputTextElementEvents_onbeforecopyEventHandler HTMLInputTextElementEvents_Event_onbeforecopy;

		// Token: 0x14001283 RID: 4739
		// (add) Token: 0x06009C52 RID: 40018
		// (remove) Token: 0x06009C53 RID: 40019
		public virtual extern event HTMLInputTextElementEvents_oncopyEventHandler HTMLInputTextElementEvents_Event_oncopy;

		// Token: 0x14001284 RID: 4740
		// (add) Token: 0x06009C54 RID: 40020
		// (remove) Token: 0x06009C55 RID: 40021
		public virtual extern event HTMLInputTextElementEvents_onbeforepasteEventHandler HTMLInputTextElementEvents_Event_onbeforepaste;

		// Token: 0x14001285 RID: 4741
		// (add) Token: 0x06009C56 RID: 40022
		// (remove) Token: 0x06009C57 RID: 40023
		public virtual extern event HTMLInputTextElementEvents_onpasteEventHandler HTMLInputTextElementEvents_Event_onpaste;

		// Token: 0x14001286 RID: 4742
		// (add) Token: 0x06009C58 RID: 40024
		// (remove) Token: 0x06009C59 RID: 40025
		public virtual extern event HTMLInputTextElementEvents_oncontextmenuEventHandler HTMLInputTextElementEvents_Event_oncontextmenu;

		// Token: 0x14001287 RID: 4743
		// (add) Token: 0x06009C5A RID: 40026
		// (remove) Token: 0x06009C5B RID: 40027
		public virtual extern event HTMLInputTextElementEvents_onrowsdeleteEventHandler HTMLInputTextElementEvents_Event_onrowsdelete;

		// Token: 0x14001288 RID: 4744
		// (add) Token: 0x06009C5C RID: 40028
		// (remove) Token: 0x06009C5D RID: 40029
		public virtual extern event HTMLInputTextElementEvents_onrowsinsertedEventHandler HTMLInputTextElementEvents_Event_onrowsinserted;

		// Token: 0x14001289 RID: 4745
		// (add) Token: 0x06009C5E RID: 40030
		// (remove) Token: 0x06009C5F RID: 40031
		public virtual extern event HTMLInputTextElementEvents_oncellchangeEventHandler HTMLInputTextElementEvents_Event_oncellchange;

		// Token: 0x1400128A RID: 4746
		// (add) Token: 0x06009C60 RID: 40032
		// (remove) Token: 0x06009C61 RID: 40033
		public virtual extern event HTMLInputTextElementEvents_onreadystatechangeEventHandler HTMLInputTextElementEvents_Event_onreadystatechange;

		// Token: 0x1400128B RID: 4747
		// (add) Token: 0x06009C62 RID: 40034
		// (remove) Token: 0x06009C63 RID: 40035
		public virtual extern event HTMLInputTextElementEvents_onbeforeeditfocusEventHandler HTMLInputTextElementEvents_Event_onbeforeeditfocus;

		// Token: 0x1400128C RID: 4748
		// (add) Token: 0x06009C64 RID: 40036
		// (remove) Token: 0x06009C65 RID: 40037
		public virtual extern event HTMLInputTextElementEvents_onlayoutcompleteEventHandler HTMLInputTextElementEvents_Event_onlayoutcomplete;

		// Token: 0x1400128D RID: 4749
		// (add) Token: 0x06009C66 RID: 40038
		// (remove) Token: 0x06009C67 RID: 40039
		public virtual extern event HTMLInputTextElementEvents_onpageEventHandler HTMLInputTextElementEvents_Event_onpage;

		// Token: 0x1400128E RID: 4750
		// (add) Token: 0x06009C68 RID: 40040
		// (remove) Token: 0x06009C69 RID: 40041
		public virtual extern event HTMLInputTextElementEvents_onbeforedeactivateEventHandler HTMLInputTextElementEvents_Event_onbeforedeactivate;

		// Token: 0x1400128F RID: 4751
		// (add) Token: 0x06009C6A RID: 40042
		// (remove) Token: 0x06009C6B RID: 40043
		public virtual extern event HTMLInputTextElementEvents_onbeforeactivateEventHandler HTMLInputTextElementEvents_Event_onbeforeactivate;

		// Token: 0x14001290 RID: 4752
		// (add) Token: 0x06009C6C RID: 40044
		// (remove) Token: 0x06009C6D RID: 40045
		public virtual extern event HTMLInputTextElementEvents_onmoveEventHandler HTMLInputTextElementEvents_Event_onmove;

		// Token: 0x14001291 RID: 4753
		// (add) Token: 0x06009C6E RID: 40046
		// (remove) Token: 0x06009C6F RID: 40047
		public virtual extern event HTMLInputTextElementEvents_oncontrolselectEventHandler HTMLInputTextElementEvents_Event_oncontrolselect;

		// Token: 0x14001292 RID: 4754
		// (add) Token: 0x06009C70 RID: 40048
		// (remove) Token: 0x06009C71 RID: 40049
		public virtual extern event HTMLInputTextElementEvents_onmovestartEventHandler HTMLInputTextElementEvents_Event_onmovestart;

		// Token: 0x14001293 RID: 4755
		// (add) Token: 0x06009C72 RID: 40050
		// (remove) Token: 0x06009C73 RID: 40051
		public virtual extern event HTMLInputTextElementEvents_onmoveendEventHandler HTMLInputTextElementEvents_Event_onmoveend;

		// Token: 0x14001294 RID: 4756
		// (add) Token: 0x06009C74 RID: 40052
		// (remove) Token: 0x06009C75 RID: 40053
		public virtual extern event HTMLInputTextElementEvents_onresizestartEventHandler HTMLInputTextElementEvents_Event_onresizestart;

		// Token: 0x14001295 RID: 4757
		// (add) Token: 0x06009C76 RID: 40054
		// (remove) Token: 0x06009C77 RID: 40055
		public virtual extern event HTMLInputTextElementEvents_onresizeendEventHandler HTMLInputTextElementEvents_Event_onresizeend;

		// Token: 0x14001296 RID: 4758
		// (add) Token: 0x06009C78 RID: 40056
		// (remove) Token: 0x06009C79 RID: 40057
		public virtual extern event HTMLInputTextElementEvents_onmouseenterEventHandler HTMLInputTextElementEvents_Event_onmouseenter;

		// Token: 0x14001297 RID: 4759
		// (add) Token: 0x06009C7A RID: 40058
		// (remove) Token: 0x06009C7B RID: 40059
		public virtual extern event HTMLInputTextElementEvents_onmouseleaveEventHandler HTMLInputTextElementEvents_Event_onmouseleave;

		// Token: 0x14001298 RID: 4760
		// (add) Token: 0x06009C7C RID: 40060
		// (remove) Token: 0x06009C7D RID: 40061
		public virtual extern event HTMLInputTextElementEvents_onmousewheelEventHandler HTMLInputTextElementEvents_Event_onmousewheel;

		// Token: 0x14001299 RID: 4761
		// (add) Token: 0x06009C7E RID: 40062
		// (remove) Token: 0x06009C7F RID: 40063
		public virtual extern event HTMLInputTextElementEvents_onactivateEventHandler HTMLInputTextElementEvents_Event_onactivate;

		// Token: 0x1400129A RID: 4762
		// (add) Token: 0x06009C80 RID: 40064
		// (remove) Token: 0x06009C81 RID: 40065
		public virtual extern event HTMLInputTextElementEvents_ondeactivateEventHandler HTMLInputTextElementEvents_Event_ondeactivate;

		// Token: 0x1400129B RID: 4763
		// (add) Token: 0x06009C82 RID: 40066
		// (remove) Token: 0x06009C83 RID: 40067
		public virtual extern event HTMLInputTextElementEvents_onfocusinEventHandler HTMLInputTextElementEvents_Event_onfocusin;

		// Token: 0x1400129C RID: 4764
		// (add) Token: 0x06009C84 RID: 40068
		// (remove) Token: 0x06009C85 RID: 40069
		public virtual extern event HTMLInputTextElementEvents_onfocusoutEventHandler HTMLInputTextElementEvents_Event_onfocusout;

		// Token: 0x1400129D RID: 4765
		// (add) Token: 0x06009C86 RID: 40070
		// (remove) Token: 0x06009C87 RID: 40071
		public virtual extern event HTMLInputTextElementEvents_onchangeEventHandler HTMLInputTextElementEvents_Event_onchange;

		// Token: 0x1400129E RID: 4766
		// (add) Token: 0x06009C88 RID: 40072
		// (remove) Token: 0x06009C89 RID: 40073
		public virtual extern event HTMLInputTextElementEvents_onselectEventHandler HTMLInputTextElementEvents_Event_onselect;

		// Token: 0x1400129F RID: 4767
		// (add) Token: 0x06009C8A RID: 40074
		// (remove) Token: 0x06009C8B RID: 40075
		public virtual extern event HTMLInputTextElementEvents_onloadEventHandler onload;

		// Token: 0x140012A0 RID: 4768
		// (add) Token: 0x06009C8C RID: 40076
		// (remove) Token: 0x06009C8D RID: 40077
		public virtual extern event HTMLInputTextElementEvents_onerrorEventHandler onerror;

		// Token: 0x140012A1 RID: 4769
		// (add) Token: 0x06009C8E RID: 40078
		// (remove) Token: 0x06009C8F RID: 40079
		public virtual extern event HTMLInputTextElementEvents_onabortEventHandler onabort;

		// Token: 0x140012A2 RID: 4770
		// (add) Token: 0x06009C90 RID: 40080
		// (remove) Token: 0x06009C91 RID: 40081
		public virtual extern event HTMLInputTextElementEvents2_onhelpEventHandler HTMLInputTextElementEvents2_Event_onhelp;

		// Token: 0x140012A3 RID: 4771
		// (add) Token: 0x06009C92 RID: 40082
		// (remove) Token: 0x06009C93 RID: 40083
		public virtual extern event HTMLInputTextElementEvents2_onclickEventHandler HTMLInputTextElementEvents2_Event_onclick;

		// Token: 0x140012A4 RID: 4772
		// (add) Token: 0x06009C94 RID: 40084
		// (remove) Token: 0x06009C95 RID: 40085
		public virtual extern event HTMLInputTextElementEvents2_ondblclickEventHandler HTMLInputTextElementEvents2_Event_ondblclick;

		// Token: 0x140012A5 RID: 4773
		// (add) Token: 0x06009C96 RID: 40086
		// (remove) Token: 0x06009C97 RID: 40087
		public virtual extern event HTMLInputTextElementEvents2_onkeypressEventHandler HTMLInputTextElementEvents2_Event_onkeypress;

		// Token: 0x140012A6 RID: 4774
		// (add) Token: 0x06009C98 RID: 40088
		// (remove) Token: 0x06009C99 RID: 40089
		public virtual extern event HTMLInputTextElementEvents2_onkeydownEventHandler HTMLInputTextElementEvents2_Event_onkeydown;

		// Token: 0x140012A7 RID: 4775
		// (add) Token: 0x06009C9A RID: 40090
		// (remove) Token: 0x06009C9B RID: 40091
		public virtual extern event HTMLInputTextElementEvents2_onkeyupEventHandler HTMLInputTextElementEvents2_Event_onkeyup;

		// Token: 0x140012A8 RID: 4776
		// (add) Token: 0x06009C9C RID: 40092
		// (remove) Token: 0x06009C9D RID: 40093
		public virtual extern event HTMLInputTextElementEvents2_onmouseoutEventHandler HTMLInputTextElementEvents2_Event_onmouseout;

		// Token: 0x140012A9 RID: 4777
		// (add) Token: 0x06009C9E RID: 40094
		// (remove) Token: 0x06009C9F RID: 40095
		public virtual extern event HTMLInputTextElementEvents2_onmouseoverEventHandler HTMLInputTextElementEvents2_Event_onmouseover;

		// Token: 0x140012AA RID: 4778
		// (add) Token: 0x06009CA0 RID: 40096
		// (remove) Token: 0x06009CA1 RID: 40097
		public virtual extern event HTMLInputTextElementEvents2_onmousemoveEventHandler HTMLInputTextElementEvents2_Event_onmousemove;

		// Token: 0x140012AB RID: 4779
		// (add) Token: 0x06009CA2 RID: 40098
		// (remove) Token: 0x06009CA3 RID: 40099
		public virtual extern event HTMLInputTextElementEvents2_onmousedownEventHandler HTMLInputTextElementEvents2_Event_onmousedown;

		// Token: 0x140012AC RID: 4780
		// (add) Token: 0x06009CA4 RID: 40100
		// (remove) Token: 0x06009CA5 RID: 40101
		public virtual extern event HTMLInputTextElementEvents2_onmouseupEventHandler HTMLInputTextElementEvents2_Event_onmouseup;

		// Token: 0x140012AD RID: 4781
		// (add) Token: 0x06009CA6 RID: 40102
		// (remove) Token: 0x06009CA7 RID: 40103
		public virtual extern event HTMLInputTextElementEvents2_onselectstartEventHandler HTMLInputTextElementEvents2_Event_onselectstart;

		// Token: 0x140012AE RID: 4782
		// (add) Token: 0x06009CA8 RID: 40104
		// (remove) Token: 0x06009CA9 RID: 40105
		public virtual extern event HTMLInputTextElementEvents2_onfilterchangeEventHandler HTMLInputTextElementEvents2_Event_onfilterchange;

		// Token: 0x140012AF RID: 4783
		// (add) Token: 0x06009CAA RID: 40106
		// (remove) Token: 0x06009CAB RID: 40107
		public virtual extern event HTMLInputTextElementEvents2_ondragstartEventHandler HTMLInputTextElementEvents2_Event_ondragstart;

		// Token: 0x140012B0 RID: 4784
		// (add) Token: 0x06009CAC RID: 40108
		// (remove) Token: 0x06009CAD RID: 40109
		public virtual extern event HTMLInputTextElementEvents2_onbeforeupdateEventHandler HTMLInputTextElementEvents2_Event_onbeforeupdate;

		// Token: 0x140012B1 RID: 4785
		// (add) Token: 0x06009CAE RID: 40110
		// (remove) Token: 0x06009CAF RID: 40111
		public virtual extern event HTMLInputTextElementEvents2_onafterupdateEventHandler HTMLInputTextElementEvents2_Event_onafterupdate;

		// Token: 0x140012B2 RID: 4786
		// (add) Token: 0x06009CB0 RID: 40112
		// (remove) Token: 0x06009CB1 RID: 40113
		public virtual extern event HTMLInputTextElementEvents2_onerrorupdateEventHandler HTMLInputTextElementEvents2_Event_onerrorupdate;

		// Token: 0x140012B3 RID: 4787
		// (add) Token: 0x06009CB2 RID: 40114
		// (remove) Token: 0x06009CB3 RID: 40115
		public virtual extern event HTMLInputTextElementEvents2_onrowexitEventHandler HTMLInputTextElementEvents2_Event_onrowexit;

		// Token: 0x140012B4 RID: 4788
		// (add) Token: 0x06009CB4 RID: 40116
		// (remove) Token: 0x06009CB5 RID: 40117
		public virtual extern event HTMLInputTextElementEvents2_onrowenterEventHandler HTMLInputTextElementEvents2_Event_onrowenter;

		// Token: 0x140012B5 RID: 4789
		// (add) Token: 0x06009CB6 RID: 40118
		// (remove) Token: 0x06009CB7 RID: 40119
		public virtual extern event HTMLInputTextElementEvents2_ondatasetchangedEventHandler HTMLInputTextElementEvents2_Event_ondatasetchanged;

		// Token: 0x140012B6 RID: 4790
		// (add) Token: 0x06009CB8 RID: 40120
		// (remove) Token: 0x06009CB9 RID: 40121
		public virtual extern event HTMLInputTextElementEvents2_ondataavailableEventHandler HTMLInputTextElementEvents2_Event_ondataavailable;

		// Token: 0x140012B7 RID: 4791
		// (add) Token: 0x06009CBA RID: 40122
		// (remove) Token: 0x06009CBB RID: 40123
		public virtual extern event HTMLInputTextElementEvents2_ondatasetcompleteEventHandler HTMLInputTextElementEvents2_Event_ondatasetcomplete;

		// Token: 0x140012B8 RID: 4792
		// (add) Token: 0x06009CBC RID: 40124
		// (remove) Token: 0x06009CBD RID: 40125
		public virtual extern event HTMLInputTextElementEvents2_onlosecaptureEventHandler HTMLInputTextElementEvents2_Event_onlosecapture;

		// Token: 0x140012B9 RID: 4793
		// (add) Token: 0x06009CBE RID: 40126
		// (remove) Token: 0x06009CBF RID: 40127
		public virtual extern event HTMLInputTextElementEvents2_onpropertychangeEventHandler HTMLInputTextElementEvents2_Event_onpropertychange;

		// Token: 0x140012BA RID: 4794
		// (add) Token: 0x06009CC0 RID: 40128
		// (remove) Token: 0x06009CC1 RID: 40129
		public virtual extern event HTMLInputTextElementEvents2_onscrollEventHandler HTMLInputTextElementEvents2_Event_onscroll;

		// Token: 0x140012BB RID: 4795
		// (add) Token: 0x06009CC2 RID: 40130
		// (remove) Token: 0x06009CC3 RID: 40131
		public virtual extern event HTMLInputTextElementEvents2_onfocusEventHandler HTMLInputTextElementEvents2_Event_onfocus;

		// Token: 0x140012BC RID: 4796
		// (add) Token: 0x06009CC4 RID: 40132
		// (remove) Token: 0x06009CC5 RID: 40133
		public virtual extern event HTMLInputTextElementEvents2_onblurEventHandler HTMLInputTextElementEvents2_Event_onblur;

		// Token: 0x140012BD RID: 4797
		// (add) Token: 0x06009CC6 RID: 40134
		// (remove) Token: 0x06009CC7 RID: 40135
		public virtual extern event HTMLInputTextElementEvents2_onresizeEventHandler HTMLInputTextElementEvents2_Event_onresize;

		// Token: 0x140012BE RID: 4798
		// (add) Token: 0x06009CC8 RID: 40136
		// (remove) Token: 0x06009CC9 RID: 40137
		public virtual extern event HTMLInputTextElementEvents2_ondragEventHandler HTMLInputTextElementEvents2_Event_ondrag;

		// Token: 0x140012BF RID: 4799
		// (add) Token: 0x06009CCA RID: 40138
		// (remove) Token: 0x06009CCB RID: 40139
		public virtual extern event HTMLInputTextElementEvents2_ondragendEventHandler HTMLInputTextElementEvents2_Event_ondragend;

		// Token: 0x140012C0 RID: 4800
		// (add) Token: 0x06009CCC RID: 40140
		// (remove) Token: 0x06009CCD RID: 40141
		public virtual extern event HTMLInputTextElementEvents2_ondragenterEventHandler HTMLInputTextElementEvents2_Event_ondragenter;

		// Token: 0x140012C1 RID: 4801
		// (add) Token: 0x06009CCE RID: 40142
		// (remove) Token: 0x06009CCF RID: 40143
		public virtual extern event HTMLInputTextElementEvents2_ondragoverEventHandler HTMLInputTextElementEvents2_Event_ondragover;

		// Token: 0x140012C2 RID: 4802
		// (add) Token: 0x06009CD0 RID: 40144
		// (remove) Token: 0x06009CD1 RID: 40145
		public virtual extern event HTMLInputTextElementEvents2_ondragleaveEventHandler HTMLInputTextElementEvents2_Event_ondragleave;

		// Token: 0x140012C3 RID: 4803
		// (add) Token: 0x06009CD2 RID: 40146
		// (remove) Token: 0x06009CD3 RID: 40147
		public virtual extern event HTMLInputTextElementEvents2_ondropEventHandler HTMLInputTextElementEvents2_Event_ondrop;

		// Token: 0x140012C4 RID: 4804
		// (add) Token: 0x06009CD4 RID: 40148
		// (remove) Token: 0x06009CD5 RID: 40149
		public virtual extern event HTMLInputTextElementEvents2_onbeforecutEventHandler HTMLInputTextElementEvents2_Event_onbeforecut;

		// Token: 0x140012C5 RID: 4805
		// (add) Token: 0x06009CD6 RID: 40150
		// (remove) Token: 0x06009CD7 RID: 40151
		public virtual extern event HTMLInputTextElementEvents2_oncutEventHandler HTMLInputTextElementEvents2_Event_oncut;

		// Token: 0x140012C6 RID: 4806
		// (add) Token: 0x06009CD8 RID: 40152
		// (remove) Token: 0x06009CD9 RID: 40153
		public virtual extern event HTMLInputTextElementEvents2_onbeforecopyEventHandler HTMLInputTextElementEvents2_Event_onbeforecopy;

		// Token: 0x140012C7 RID: 4807
		// (add) Token: 0x06009CDA RID: 40154
		// (remove) Token: 0x06009CDB RID: 40155
		public virtual extern event HTMLInputTextElementEvents2_oncopyEventHandler HTMLInputTextElementEvents2_Event_oncopy;

		// Token: 0x140012C8 RID: 4808
		// (add) Token: 0x06009CDC RID: 40156
		// (remove) Token: 0x06009CDD RID: 40157
		public virtual extern event HTMLInputTextElementEvents2_onbeforepasteEventHandler HTMLInputTextElementEvents2_Event_onbeforepaste;

		// Token: 0x140012C9 RID: 4809
		// (add) Token: 0x06009CDE RID: 40158
		// (remove) Token: 0x06009CDF RID: 40159
		public virtual extern event HTMLInputTextElementEvents2_onpasteEventHandler HTMLInputTextElementEvents2_Event_onpaste;

		// Token: 0x140012CA RID: 4810
		// (add) Token: 0x06009CE0 RID: 40160
		// (remove) Token: 0x06009CE1 RID: 40161
		public virtual extern event HTMLInputTextElementEvents2_oncontextmenuEventHandler HTMLInputTextElementEvents2_Event_oncontextmenu;

		// Token: 0x140012CB RID: 4811
		// (add) Token: 0x06009CE2 RID: 40162
		// (remove) Token: 0x06009CE3 RID: 40163
		public virtual extern event HTMLInputTextElementEvents2_onrowsdeleteEventHandler HTMLInputTextElementEvents2_Event_onrowsdelete;

		// Token: 0x140012CC RID: 4812
		// (add) Token: 0x06009CE4 RID: 40164
		// (remove) Token: 0x06009CE5 RID: 40165
		public virtual extern event HTMLInputTextElementEvents2_onrowsinsertedEventHandler HTMLInputTextElementEvents2_Event_onrowsinserted;

		// Token: 0x140012CD RID: 4813
		// (add) Token: 0x06009CE6 RID: 40166
		// (remove) Token: 0x06009CE7 RID: 40167
		public virtual extern event HTMLInputTextElementEvents2_oncellchangeEventHandler HTMLInputTextElementEvents2_Event_oncellchange;

		// Token: 0x140012CE RID: 4814
		// (add) Token: 0x06009CE8 RID: 40168
		// (remove) Token: 0x06009CE9 RID: 40169
		public virtual extern event HTMLInputTextElementEvents2_onreadystatechangeEventHandler HTMLInputTextElementEvents2_Event_onreadystatechange;

		// Token: 0x140012CF RID: 4815
		// (add) Token: 0x06009CEA RID: 40170
		// (remove) Token: 0x06009CEB RID: 40171
		public virtual extern event HTMLInputTextElementEvents2_onlayoutcompleteEventHandler HTMLInputTextElementEvents2_Event_onlayoutcomplete;

		// Token: 0x140012D0 RID: 4816
		// (add) Token: 0x06009CEC RID: 40172
		// (remove) Token: 0x06009CED RID: 40173
		public virtual extern event HTMLInputTextElementEvents2_onpageEventHandler HTMLInputTextElementEvents2_Event_onpage;

		// Token: 0x140012D1 RID: 4817
		// (add) Token: 0x06009CEE RID: 40174
		// (remove) Token: 0x06009CEF RID: 40175
		public virtual extern event HTMLInputTextElementEvents2_onmouseenterEventHandler HTMLInputTextElementEvents2_Event_onmouseenter;

		// Token: 0x140012D2 RID: 4818
		// (add) Token: 0x06009CF0 RID: 40176
		// (remove) Token: 0x06009CF1 RID: 40177
		public virtual extern event HTMLInputTextElementEvents2_onmouseleaveEventHandler HTMLInputTextElementEvents2_Event_onmouseleave;

		// Token: 0x140012D3 RID: 4819
		// (add) Token: 0x06009CF2 RID: 40178
		// (remove) Token: 0x06009CF3 RID: 40179
		public virtual extern event HTMLInputTextElementEvents2_onactivateEventHandler HTMLInputTextElementEvents2_Event_onactivate;

		// Token: 0x140012D4 RID: 4820
		// (add) Token: 0x06009CF4 RID: 40180
		// (remove) Token: 0x06009CF5 RID: 40181
		public virtual extern event HTMLInputTextElementEvents2_ondeactivateEventHandler HTMLInputTextElementEvents2_Event_ondeactivate;

		// Token: 0x140012D5 RID: 4821
		// (add) Token: 0x06009CF6 RID: 40182
		// (remove) Token: 0x06009CF7 RID: 40183
		public virtual extern event HTMLInputTextElementEvents2_onbeforedeactivateEventHandler HTMLInputTextElementEvents2_Event_onbeforedeactivate;

		// Token: 0x140012D6 RID: 4822
		// (add) Token: 0x06009CF8 RID: 40184
		// (remove) Token: 0x06009CF9 RID: 40185
		public virtual extern event HTMLInputTextElementEvents2_onbeforeactivateEventHandler HTMLInputTextElementEvents2_Event_onbeforeactivate;

		// Token: 0x140012D7 RID: 4823
		// (add) Token: 0x06009CFA RID: 40186
		// (remove) Token: 0x06009CFB RID: 40187
		public virtual extern event HTMLInputTextElementEvents2_onfocusinEventHandler HTMLInputTextElementEvents2_Event_onfocusin;

		// Token: 0x140012D8 RID: 4824
		// (add) Token: 0x06009CFC RID: 40188
		// (remove) Token: 0x06009CFD RID: 40189
		public virtual extern event HTMLInputTextElementEvents2_onfocusoutEventHandler HTMLInputTextElementEvents2_Event_onfocusout;

		// Token: 0x140012D9 RID: 4825
		// (add) Token: 0x06009CFE RID: 40190
		// (remove) Token: 0x06009CFF RID: 40191
		public virtual extern event HTMLInputTextElementEvents2_onmoveEventHandler HTMLInputTextElementEvents2_Event_onmove;

		// Token: 0x140012DA RID: 4826
		// (add) Token: 0x06009D00 RID: 40192
		// (remove) Token: 0x06009D01 RID: 40193
		public virtual extern event HTMLInputTextElementEvents2_oncontrolselectEventHandler HTMLInputTextElementEvents2_Event_oncontrolselect;

		// Token: 0x140012DB RID: 4827
		// (add) Token: 0x06009D02 RID: 40194
		// (remove) Token: 0x06009D03 RID: 40195
		public virtual extern event HTMLInputTextElementEvents2_onmovestartEventHandler HTMLInputTextElementEvents2_Event_onmovestart;

		// Token: 0x140012DC RID: 4828
		// (add) Token: 0x06009D04 RID: 40196
		// (remove) Token: 0x06009D05 RID: 40197
		public virtual extern event HTMLInputTextElementEvents2_onmoveendEventHandler HTMLInputTextElementEvents2_Event_onmoveend;

		// Token: 0x140012DD RID: 4829
		// (add) Token: 0x06009D06 RID: 40198
		// (remove) Token: 0x06009D07 RID: 40199
		public virtual extern event HTMLInputTextElementEvents2_onresizestartEventHandler HTMLInputTextElementEvents2_Event_onresizestart;

		// Token: 0x140012DE RID: 4830
		// (add) Token: 0x06009D08 RID: 40200
		// (remove) Token: 0x06009D09 RID: 40201
		public virtual extern event HTMLInputTextElementEvents2_onresizeendEventHandler HTMLInputTextElementEvents2_Event_onresizeend;

		// Token: 0x140012DF RID: 4831
		// (add) Token: 0x06009D0A RID: 40202
		// (remove) Token: 0x06009D0B RID: 40203
		public virtual extern event HTMLInputTextElementEvents2_onmousewheelEventHandler HTMLInputTextElementEvents2_Event_onmousewheel;

		// Token: 0x140012E0 RID: 4832
		// (add) Token: 0x06009D0C RID: 40204
		// (remove) Token: 0x06009D0D RID: 40205
		public virtual extern event HTMLInputTextElementEvents2_onchangeEventHandler HTMLInputTextElementEvents2_Event_onchange;

		// Token: 0x140012E1 RID: 4833
		// (add) Token: 0x06009D0E RID: 40206
		// (remove) Token: 0x06009D0F RID: 40207
		public virtual extern event HTMLInputTextElementEvents2_onselectEventHandler HTMLInputTextElementEvents2_Event_onselect;

		// Token: 0x140012E2 RID: 4834
		// (add) Token: 0x06009D10 RID: 40208
		// (remove) Token: 0x06009D11 RID: 40209
		public virtual extern event HTMLInputTextElementEvents2_onloadEventHandler HTMLInputTextElementEvents2_Event_onload;

		// Token: 0x140012E3 RID: 4835
		// (add) Token: 0x06009D12 RID: 40210
		// (remove) Token: 0x06009D13 RID: 40211
		public virtual extern event HTMLInputTextElementEvents2_onerrorEventHandler HTMLInputTextElementEvents2_Event_onerror;

		// Token: 0x140012E4 RID: 4836
		// (add) Token: 0x06009D14 RID: 40212
		// (remove) Token: 0x06009D15 RID: 40213
		public virtual extern event HTMLInputTextElementEvents2_onabortEventHandler HTMLInputTextElementEvents2_Event_onabort;

		// Token: 0x140012E5 RID: 4837
		// (add) Token: 0x06009D16 RID: 40214
		// (remove) Token: 0x06009D17 RID: 40215
		public virtual extern event HTMLTextContainerEvents_onhelpEventHandler HTMLTextContainerEvents_Event_onhelp;

		// Token: 0x140012E6 RID: 4838
		// (add) Token: 0x06009D18 RID: 40216
		// (remove) Token: 0x06009D19 RID: 40217
		public virtual extern event HTMLTextContainerEvents_onclickEventHandler HTMLTextContainerEvents_Event_onclick;

		// Token: 0x140012E7 RID: 4839
		// (add) Token: 0x06009D1A RID: 40218
		// (remove) Token: 0x06009D1B RID: 40219
		public virtual extern event HTMLTextContainerEvents_ondblclickEventHandler HTMLTextContainerEvents_Event_ondblclick;

		// Token: 0x140012E8 RID: 4840
		// (add) Token: 0x06009D1C RID: 40220
		// (remove) Token: 0x06009D1D RID: 40221
		public virtual extern event HTMLTextContainerEvents_onkeypressEventHandler HTMLTextContainerEvents_Event_onkeypress;

		// Token: 0x140012E9 RID: 4841
		// (add) Token: 0x06009D1E RID: 40222
		// (remove) Token: 0x06009D1F RID: 40223
		public virtual extern event HTMLTextContainerEvents_onkeydownEventHandler HTMLTextContainerEvents_Event_onkeydown;

		// Token: 0x140012EA RID: 4842
		// (add) Token: 0x06009D20 RID: 40224
		// (remove) Token: 0x06009D21 RID: 40225
		public virtual extern event HTMLTextContainerEvents_onkeyupEventHandler HTMLTextContainerEvents_Event_onkeyup;

		// Token: 0x140012EB RID: 4843
		// (add) Token: 0x06009D22 RID: 40226
		// (remove) Token: 0x06009D23 RID: 40227
		public virtual extern event HTMLTextContainerEvents_onmouseoutEventHandler HTMLTextContainerEvents_Event_onmouseout;

		// Token: 0x140012EC RID: 4844
		// (add) Token: 0x06009D24 RID: 40228
		// (remove) Token: 0x06009D25 RID: 40229
		public virtual extern event HTMLTextContainerEvents_onmouseoverEventHandler HTMLTextContainerEvents_Event_onmouseover;

		// Token: 0x140012ED RID: 4845
		// (add) Token: 0x06009D26 RID: 40230
		// (remove) Token: 0x06009D27 RID: 40231
		public virtual extern event HTMLTextContainerEvents_onmousemoveEventHandler HTMLTextContainerEvents_Event_onmousemove;

		// Token: 0x140012EE RID: 4846
		// (add) Token: 0x06009D28 RID: 40232
		// (remove) Token: 0x06009D29 RID: 40233
		public virtual extern event HTMLTextContainerEvents_onmousedownEventHandler HTMLTextContainerEvents_Event_onmousedown;

		// Token: 0x140012EF RID: 4847
		// (add) Token: 0x06009D2A RID: 40234
		// (remove) Token: 0x06009D2B RID: 40235
		public virtual extern event HTMLTextContainerEvents_onmouseupEventHandler HTMLTextContainerEvents_Event_onmouseup;

		// Token: 0x140012F0 RID: 4848
		// (add) Token: 0x06009D2C RID: 40236
		// (remove) Token: 0x06009D2D RID: 40237
		public virtual extern event HTMLTextContainerEvents_onselectstartEventHandler HTMLTextContainerEvents_Event_onselectstart;

		// Token: 0x140012F1 RID: 4849
		// (add) Token: 0x06009D2E RID: 40238
		// (remove) Token: 0x06009D2F RID: 40239
		public virtual extern event HTMLTextContainerEvents_onfilterchangeEventHandler HTMLTextContainerEvents_Event_onfilterchange;

		// Token: 0x140012F2 RID: 4850
		// (add) Token: 0x06009D30 RID: 40240
		// (remove) Token: 0x06009D31 RID: 40241
		public virtual extern event HTMLTextContainerEvents_ondragstartEventHandler HTMLTextContainerEvents_Event_ondragstart;

		// Token: 0x140012F3 RID: 4851
		// (add) Token: 0x06009D32 RID: 40242
		// (remove) Token: 0x06009D33 RID: 40243
		public virtual extern event HTMLTextContainerEvents_onbeforeupdateEventHandler HTMLTextContainerEvents_Event_onbeforeupdate;

		// Token: 0x140012F4 RID: 4852
		// (add) Token: 0x06009D34 RID: 40244
		// (remove) Token: 0x06009D35 RID: 40245
		public virtual extern event HTMLTextContainerEvents_onafterupdateEventHandler HTMLTextContainerEvents_Event_onafterupdate;

		// Token: 0x140012F5 RID: 4853
		// (add) Token: 0x06009D36 RID: 40246
		// (remove) Token: 0x06009D37 RID: 40247
		public virtual extern event HTMLTextContainerEvents_onerrorupdateEventHandler HTMLTextContainerEvents_Event_onerrorupdate;

		// Token: 0x140012F6 RID: 4854
		// (add) Token: 0x06009D38 RID: 40248
		// (remove) Token: 0x06009D39 RID: 40249
		public virtual extern event HTMLTextContainerEvents_onrowexitEventHandler HTMLTextContainerEvents_Event_onrowexit;

		// Token: 0x140012F7 RID: 4855
		// (add) Token: 0x06009D3A RID: 40250
		// (remove) Token: 0x06009D3B RID: 40251
		public virtual extern event HTMLTextContainerEvents_onrowenterEventHandler HTMLTextContainerEvents_Event_onrowenter;

		// Token: 0x140012F8 RID: 4856
		// (add) Token: 0x06009D3C RID: 40252
		// (remove) Token: 0x06009D3D RID: 40253
		public virtual extern event HTMLTextContainerEvents_ondatasetchangedEventHandler HTMLTextContainerEvents_Event_ondatasetchanged;

		// Token: 0x140012F9 RID: 4857
		// (add) Token: 0x06009D3E RID: 40254
		// (remove) Token: 0x06009D3F RID: 40255
		public virtual extern event HTMLTextContainerEvents_ondataavailableEventHandler HTMLTextContainerEvents_Event_ondataavailable;

		// Token: 0x140012FA RID: 4858
		// (add) Token: 0x06009D40 RID: 40256
		// (remove) Token: 0x06009D41 RID: 40257
		public virtual extern event HTMLTextContainerEvents_ondatasetcompleteEventHandler HTMLTextContainerEvents_Event_ondatasetcomplete;

		// Token: 0x140012FB RID: 4859
		// (add) Token: 0x06009D42 RID: 40258
		// (remove) Token: 0x06009D43 RID: 40259
		public virtual extern event HTMLTextContainerEvents_onlosecaptureEventHandler HTMLTextContainerEvents_Event_onlosecapture;

		// Token: 0x140012FC RID: 4860
		// (add) Token: 0x06009D44 RID: 40260
		// (remove) Token: 0x06009D45 RID: 40261
		public virtual extern event HTMLTextContainerEvents_onpropertychangeEventHandler HTMLTextContainerEvents_Event_onpropertychange;

		// Token: 0x140012FD RID: 4861
		// (add) Token: 0x06009D46 RID: 40262
		// (remove) Token: 0x06009D47 RID: 40263
		public virtual extern event HTMLTextContainerEvents_onscrollEventHandler HTMLTextContainerEvents_Event_onscroll;

		// Token: 0x140012FE RID: 4862
		// (add) Token: 0x06009D48 RID: 40264
		// (remove) Token: 0x06009D49 RID: 40265
		public virtual extern event HTMLTextContainerEvents_onfocusEventHandler HTMLTextContainerEvents_Event_onfocus;

		// Token: 0x140012FF RID: 4863
		// (add) Token: 0x06009D4A RID: 40266
		// (remove) Token: 0x06009D4B RID: 40267
		public virtual extern event HTMLTextContainerEvents_onblurEventHandler HTMLTextContainerEvents_Event_onblur;

		// Token: 0x14001300 RID: 4864
		// (add) Token: 0x06009D4C RID: 40268
		// (remove) Token: 0x06009D4D RID: 40269
		public virtual extern event HTMLTextContainerEvents_onresizeEventHandler HTMLTextContainerEvents_Event_onresize;

		// Token: 0x14001301 RID: 4865
		// (add) Token: 0x06009D4E RID: 40270
		// (remove) Token: 0x06009D4F RID: 40271
		public virtual extern event HTMLTextContainerEvents_ondragEventHandler HTMLTextContainerEvents_Event_ondrag;

		// Token: 0x14001302 RID: 4866
		// (add) Token: 0x06009D50 RID: 40272
		// (remove) Token: 0x06009D51 RID: 40273
		public virtual extern event HTMLTextContainerEvents_ondragendEventHandler HTMLTextContainerEvents_Event_ondragend;

		// Token: 0x14001303 RID: 4867
		// (add) Token: 0x06009D52 RID: 40274
		// (remove) Token: 0x06009D53 RID: 40275
		public virtual extern event HTMLTextContainerEvents_ondragenterEventHandler HTMLTextContainerEvents_Event_ondragenter;

		// Token: 0x14001304 RID: 4868
		// (add) Token: 0x06009D54 RID: 40276
		// (remove) Token: 0x06009D55 RID: 40277
		public virtual extern event HTMLTextContainerEvents_ondragoverEventHandler HTMLTextContainerEvents_Event_ondragover;

		// Token: 0x14001305 RID: 4869
		// (add) Token: 0x06009D56 RID: 40278
		// (remove) Token: 0x06009D57 RID: 40279
		public virtual extern event HTMLTextContainerEvents_ondragleaveEventHandler HTMLTextContainerEvents_Event_ondragleave;

		// Token: 0x14001306 RID: 4870
		// (add) Token: 0x06009D58 RID: 40280
		// (remove) Token: 0x06009D59 RID: 40281
		public virtual extern event HTMLTextContainerEvents_ondropEventHandler HTMLTextContainerEvents_Event_ondrop;

		// Token: 0x14001307 RID: 4871
		// (add) Token: 0x06009D5A RID: 40282
		// (remove) Token: 0x06009D5B RID: 40283
		public virtual extern event HTMLTextContainerEvents_onbeforecutEventHandler HTMLTextContainerEvents_Event_onbeforecut;

		// Token: 0x14001308 RID: 4872
		// (add) Token: 0x06009D5C RID: 40284
		// (remove) Token: 0x06009D5D RID: 40285
		public virtual extern event HTMLTextContainerEvents_oncutEventHandler HTMLTextContainerEvents_Event_oncut;

		// Token: 0x14001309 RID: 4873
		// (add) Token: 0x06009D5E RID: 40286
		// (remove) Token: 0x06009D5F RID: 40287
		public virtual extern event HTMLTextContainerEvents_onbeforecopyEventHandler HTMLTextContainerEvents_Event_onbeforecopy;

		// Token: 0x1400130A RID: 4874
		// (add) Token: 0x06009D60 RID: 40288
		// (remove) Token: 0x06009D61 RID: 40289
		public virtual extern event HTMLTextContainerEvents_oncopyEventHandler HTMLTextContainerEvents_Event_oncopy;

		// Token: 0x1400130B RID: 4875
		// (add) Token: 0x06009D62 RID: 40290
		// (remove) Token: 0x06009D63 RID: 40291
		public virtual extern event HTMLTextContainerEvents_onbeforepasteEventHandler HTMLTextContainerEvents_Event_onbeforepaste;

		// Token: 0x1400130C RID: 4876
		// (add) Token: 0x06009D64 RID: 40292
		// (remove) Token: 0x06009D65 RID: 40293
		public virtual extern event HTMLTextContainerEvents_onpasteEventHandler HTMLTextContainerEvents_Event_onpaste;

		// Token: 0x1400130D RID: 4877
		// (add) Token: 0x06009D66 RID: 40294
		// (remove) Token: 0x06009D67 RID: 40295
		public virtual extern event HTMLTextContainerEvents_oncontextmenuEventHandler HTMLTextContainerEvents_Event_oncontextmenu;

		// Token: 0x1400130E RID: 4878
		// (add) Token: 0x06009D68 RID: 40296
		// (remove) Token: 0x06009D69 RID: 40297
		public virtual extern event HTMLTextContainerEvents_onrowsdeleteEventHandler HTMLTextContainerEvents_Event_onrowsdelete;

		// Token: 0x1400130F RID: 4879
		// (add) Token: 0x06009D6A RID: 40298
		// (remove) Token: 0x06009D6B RID: 40299
		public virtual extern event HTMLTextContainerEvents_onrowsinsertedEventHandler HTMLTextContainerEvents_Event_onrowsinserted;

		// Token: 0x14001310 RID: 4880
		// (add) Token: 0x06009D6C RID: 40300
		// (remove) Token: 0x06009D6D RID: 40301
		public virtual extern event HTMLTextContainerEvents_oncellchangeEventHandler HTMLTextContainerEvents_Event_oncellchange;

		// Token: 0x14001311 RID: 4881
		// (add) Token: 0x06009D6E RID: 40302
		// (remove) Token: 0x06009D6F RID: 40303
		public virtual extern event HTMLTextContainerEvents_onreadystatechangeEventHandler HTMLTextContainerEvents_Event_onreadystatechange;

		// Token: 0x14001312 RID: 4882
		// (add) Token: 0x06009D70 RID: 40304
		// (remove) Token: 0x06009D71 RID: 40305
		public virtual extern event HTMLTextContainerEvents_onbeforeeditfocusEventHandler HTMLTextContainerEvents_Event_onbeforeeditfocus;

		// Token: 0x14001313 RID: 4883
		// (add) Token: 0x06009D72 RID: 40306
		// (remove) Token: 0x06009D73 RID: 40307
		public virtual extern event HTMLTextContainerEvents_onlayoutcompleteEventHandler HTMLTextContainerEvents_Event_onlayoutcomplete;

		// Token: 0x14001314 RID: 4884
		// (add) Token: 0x06009D74 RID: 40308
		// (remove) Token: 0x06009D75 RID: 40309
		public virtual extern event HTMLTextContainerEvents_onpageEventHandler HTMLTextContainerEvents_Event_onpage;

		// Token: 0x14001315 RID: 4885
		// (add) Token: 0x06009D76 RID: 40310
		// (remove) Token: 0x06009D77 RID: 40311
		public virtual extern event HTMLTextContainerEvents_onbeforedeactivateEventHandler HTMLTextContainerEvents_Event_onbeforedeactivate;

		// Token: 0x14001316 RID: 4886
		// (add) Token: 0x06009D78 RID: 40312
		// (remove) Token: 0x06009D79 RID: 40313
		public virtual extern event HTMLTextContainerEvents_onbeforeactivateEventHandler HTMLTextContainerEvents_Event_onbeforeactivate;

		// Token: 0x14001317 RID: 4887
		// (add) Token: 0x06009D7A RID: 40314
		// (remove) Token: 0x06009D7B RID: 40315
		public virtual extern event HTMLTextContainerEvents_onmoveEventHandler HTMLTextContainerEvents_Event_onmove;

		// Token: 0x14001318 RID: 4888
		// (add) Token: 0x06009D7C RID: 40316
		// (remove) Token: 0x06009D7D RID: 40317
		public virtual extern event HTMLTextContainerEvents_oncontrolselectEventHandler HTMLTextContainerEvents_Event_oncontrolselect;

		// Token: 0x14001319 RID: 4889
		// (add) Token: 0x06009D7E RID: 40318
		// (remove) Token: 0x06009D7F RID: 40319
		public virtual extern event HTMLTextContainerEvents_onmovestartEventHandler HTMLTextContainerEvents_Event_onmovestart;

		// Token: 0x1400131A RID: 4890
		// (add) Token: 0x06009D80 RID: 40320
		// (remove) Token: 0x06009D81 RID: 40321
		public virtual extern event HTMLTextContainerEvents_onmoveendEventHandler HTMLTextContainerEvents_Event_onmoveend;

		// Token: 0x1400131B RID: 4891
		// (add) Token: 0x06009D82 RID: 40322
		// (remove) Token: 0x06009D83 RID: 40323
		public virtual extern event HTMLTextContainerEvents_onresizestartEventHandler HTMLTextContainerEvents_Event_onresizestart;

		// Token: 0x1400131C RID: 4892
		// (add) Token: 0x06009D84 RID: 40324
		// (remove) Token: 0x06009D85 RID: 40325
		public virtual extern event HTMLTextContainerEvents_onresizeendEventHandler HTMLTextContainerEvents_Event_onresizeend;

		// Token: 0x1400131D RID: 4893
		// (add) Token: 0x06009D86 RID: 40326
		// (remove) Token: 0x06009D87 RID: 40327
		public virtual extern event HTMLTextContainerEvents_onmouseenterEventHandler HTMLTextContainerEvents_Event_onmouseenter;

		// Token: 0x1400131E RID: 4894
		// (add) Token: 0x06009D88 RID: 40328
		// (remove) Token: 0x06009D89 RID: 40329
		public virtual extern event HTMLTextContainerEvents_onmouseleaveEventHandler HTMLTextContainerEvents_Event_onmouseleave;

		// Token: 0x1400131F RID: 4895
		// (add) Token: 0x06009D8A RID: 40330
		// (remove) Token: 0x06009D8B RID: 40331
		public virtual extern event HTMLTextContainerEvents_onmousewheelEventHandler HTMLTextContainerEvents_Event_onmousewheel;

		// Token: 0x14001320 RID: 4896
		// (add) Token: 0x06009D8C RID: 40332
		// (remove) Token: 0x06009D8D RID: 40333
		public virtual extern event HTMLTextContainerEvents_onactivateEventHandler HTMLTextContainerEvents_Event_onactivate;

		// Token: 0x14001321 RID: 4897
		// (add) Token: 0x06009D8E RID: 40334
		// (remove) Token: 0x06009D8F RID: 40335
		public virtual extern event HTMLTextContainerEvents_ondeactivateEventHandler HTMLTextContainerEvents_Event_ondeactivate;

		// Token: 0x14001322 RID: 4898
		// (add) Token: 0x06009D90 RID: 40336
		// (remove) Token: 0x06009D91 RID: 40337
		public virtual extern event HTMLTextContainerEvents_onfocusinEventHandler HTMLTextContainerEvents_Event_onfocusin;

		// Token: 0x14001323 RID: 4899
		// (add) Token: 0x06009D92 RID: 40338
		// (remove) Token: 0x06009D93 RID: 40339
		public virtual extern event HTMLTextContainerEvents_onfocusoutEventHandler HTMLTextContainerEvents_Event_onfocusout;

		// Token: 0x14001324 RID: 4900
		// (add) Token: 0x06009D94 RID: 40340
		// (remove) Token: 0x06009D95 RID: 40341
		public virtual extern event HTMLTextContainerEvents_onchangeEventHandler HTMLTextContainerEvents_Event_onchange;

		// Token: 0x14001325 RID: 4901
		// (add) Token: 0x06009D96 RID: 40342
		// (remove) Token: 0x06009D97 RID: 40343
		public virtual extern event HTMLTextContainerEvents_onselectEventHandler HTMLTextContainerEvents_Event_onselect;

		// Token: 0x14001326 RID: 4902
		// (add) Token: 0x06009D98 RID: 40344
		// (remove) Token: 0x06009D99 RID: 40345
		public virtual extern event HTMLTextContainerEvents2_onhelpEventHandler HTMLTextContainerEvents2_Event_onhelp;

		// Token: 0x14001327 RID: 4903
		// (add) Token: 0x06009D9A RID: 40346
		// (remove) Token: 0x06009D9B RID: 40347
		public virtual extern event HTMLTextContainerEvents2_onclickEventHandler HTMLTextContainerEvents2_Event_onclick;

		// Token: 0x14001328 RID: 4904
		// (add) Token: 0x06009D9C RID: 40348
		// (remove) Token: 0x06009D9D RID: 40349
		public virtual extern event HTMLTextContainerEvents2_ondblclickEventHandler HTMLTextContainerEvents2_Event_ondblclick;

		// Token: 0x14001329 RID: 4905
		// (add) Token: 0x06009D9E RID: 40350
		// (remove) Token: 0x06009D9F RID: 40351
		public virtual extern event HTMLTextContainerEvents2_onkeypressEventHandler HTMLTextContainerEvents2_Event_onkeypress;

		// Token: 0x1400132A RID: 4906
		// (add) Token: 0x06009DA0 RID: 40352
		// (remove) Token: 0x06009DA1 RID: 40353
		public virtual extern event HTMLTextContainerEvents2_onkeydownEventHandler HTMLTextContainerEvents2_Event_onkeydown;

		// Token: 0x1400132B RID: 4907
		// (add) Token: 0x06009DA2 RID: 40354
		// (remove) Token: 0x06009DA3 RID: 40355
		public virtual extern event HTMLTextContainerEvents2_onkeyupEventHandler HTMLTextContainerEvents2_Event_onkeyup;

		// Token: 0x1400132C RID: 4908
		// (add) Token: 0x06009DA4 RID: 40356
		// (remove) Token: 0x06009DA5 RID: 40357
		public virtual extern event HTMLTextContainerEvents2_onmouseoutEventHandler HTMLTextContainerEvents2_Event_onmouseout;

		// Token: 0x1400132D RID: 4909
		// (add) Token: 0x06009DA6 RID: 40358
		// (remove) Token: 0x06009DA7 RID: 40359
		public virtual extern event HTMLTextContainerEvents2_onmouseoverEventHandler HTMLTextContainerEvents2_Event_onmouseover;

		// Token: 0x1400132E RID: 4910
		// (add) Token: 0x06009DA8 RID: 40360
		// (remove) Token: 0x06009DA9 RID: 40361
		public virtual extern event HTMLTextContainerEvents2_onmousemoveEventHandler HTMLTextContainerEvents2_Event_onmousemove;

		// Token: 0x1400132F RID: 4911
		// (add) Token: 0x06009DAA RID: 40362
		// (remove) Token: 0x06009DAB RID: 40363
		public virtual extern event HTMLTextContainerEvents2_onmousedownEventHandler HTMLTextContainerEvents2_Event_onmousedown;

		// Token: 0x14001330 RID: 4912
		// (add) Token: 0x06009DAC RID: 40364
		// (remove) Token: 0x06009DAD RID: 40365
		public virtual extern event HTMLTextContainerEvents2_onmouseupEventHandler HTMLTextContainerEvents2_Event_onmouseup;

		// Token: 0x14001331 RID: 4913
		// (add) Token: 0x06009DAE RID: 40366
		// (remove) Token: 0x06009DAF RID: 40367
		public virtual extern event HTMLTextContainerEvents2_onselectstartEventHandler HTMLTextContainerEvents2_Event_onselectstart;

		// Token: 0x14001332 RID: 4914
		// (add) Token: 0x06009DB0 RID: 40368
		// (remove) Token: 0x06009DB1 RID: 40369
		public virtual extern event HTMLTextContainerEvents2_onfilterchangeEventHandler HTMLTextContainerEvents2_Event_onfilterchange;

		// Token: 0x14001333 RID: 4915
		// (add) Token: 0x06009DB2 RID: 40370
		// (remove) Token: 0x06009DB3 RID: 40371
		public virtual extern event HTMLTextContainerEvents2_ondragstartEventHandler HTMLTextContainerEvents2_Event_ondragstart;

		// Token: 0x14001334 RID: 4916
		// (add) Token: 0x06009DB4 RID: 40372
		// (remove) Token: 0x06009DB5 RID: 40373
		public virtual extern event HTMLTextContainerEvents2_onbeforeupdateEventHandler HTMLTextContainerEvents2_Event_onbeforeupdate;

		// Token: 0x14001335 RID: 4917
		// (add) Token: 0x06009DB6 RID: 40374
		// (remove) Token: 0x06009DB7 RID: 40375
		public virtual extern event HTMLTextContainerEvents2_onafterupdateEventHandler HTMLTextContainerEvents2_Event_onafterupdate;

		// Token: 0x14001336 RID: 4918
		// (add) Token: 0x06009DB8 RID: 40376
		// (remove) Token: 0x06009DB9 RID: 40377
		public virtual extern event HTMLTextContainerEvents2_onerrorupdateEventHandler HTMLTextContainerEvents2_Event_onerrorupdate;

		// Token: 0x14001337 RID: 4919
		// (add) Token: 0x06009DBA RID: 40378
		// (remove) Token: 0x06009DBB RID: 40379
		public virtual extern event HTMLTextContainerEvents2_onrowexitEventHandler HTMLTextContainerEvents2_Event_onrowexit;

		// Token: 0x14001338 RID: 4920
		// (add) Token: 0x06009DBC RID: 40380
		// (remove) Token: 0x06009DBD RID: 40381
		public virtual extern event HTMLTextContainerEvents2_onrowenterEventHandler HTMLTextContainerEvents2_Event_onrowenter;

		// Token: 0x14001339 RID: 4921
		// (add) Token: 0x06009DBE RID: 40382
		// (remove) Token: 0x06009DBF RID: 40383
		public virtual extern event HTMLTextContainerEvents2_ondatasetchangedEventHandler HTMLTextContainerEvents2_Event_ondatasetchanged;

		// Token: 0x1400133A RID: 4922
		// (add) Token: 0x06009DC0 RID: 40384
		// (remove) Token: 0x06009DC1 RID: 40385
		public virtual extern event HTMLTextContainerEvents2_ondataavailableEventHandler HTMLTextContainerEvents2_Event_ondataavailable;

		// Token: 0x1400133B RID: 4923
		// (add) Token: 0x06009DC2 RID: 40386
		// (remove) Token: 0x06009DC3 RID: 40387
		public virtual extern event HTMLTextContainerEvents2_ondatasetcompleteEventHandler HTMLTextContainerEvents2_Event_ondatasetcomplete;

		// Token: 0x1400133C RID: 4924
		// (add) Token: 0x06009DC4 RID: 40388
		// (remove) Token: 0x06009DC5 RID: 40389
		public virtual extern event HTMLTextContainerEvents2_onlosecaptureEventHandler HTMLTextContainerEvents2_Event_onlosecapture;

		// Token: 0x1400133D RID: 4925
		// (add) Token: 0x06009DC6 RID: 40390
		// (remove) Token: 0x06009DC7 RID: 40391
		public virtual extern event HTMLTextContainerEvents2_onpropertychangeEventHandler HTMLTextContainerEvents2_Event_onpropertychange;

		// Token: 0x1400133E RID: 4926
		// (add) Token: 0x06009DC8 RID: 40392
		// (remove) Token: 0x06009DC9 RID: 40393
		public virtual extern event HTMLTextContainerEvents2_onscrollEventHandler HTMLTextContainerEvents2_Event_onscroll;

		// Token: 0x1400133F RID: 4927
		// (add) Token: 0x06009DCA RID: 40394
		// (remove) Token: 0x06009DCB RID: 40395
		public virtual extern event HTMLTextContainerEvents2_onfocusEventHandler HTMLTextContainerEvents2_Event_onfocus;

		// Token: 0x14001340 RID: 4928
		// (add) Token: 0x06009DCC RID: 40396
		// (remove) Token: 0x06009DCD RID: 40397
		public virtual extern event HTMLTextContainerEvents2_onblurEventHandler HTMLTextContainerEvents2_Event_onblur;

		// Token: 0x14001341 RID: 4929
		// (add) Token: 0x06009DCE RID: 40398
		// (remove) Token: 0x06009DCF RID: 40399
		public virtual extern event HTMLTextContainerEvents2_onresizeEventHandler HTMLTextContainerEvents2_Event_onresize;

		// Token: 0x14001342 RID: 4930
		// (add) Token: 0x06009DD0 RID: 40400
		// (remove) Token: 0x06009DD1 RID: 40401
		public virtual extern event HTMLTextContainerEvents2_ondragEventHandler HTMLTextContainerEvents2_Event_ondrag;

		// Token: 0x14001343 RID: 4931
		// (add) Token: 0x06009DD2 RID: 40402
		// (remove) Token: 0x06009DD3 RID: 40403
		public virtual extern event HTMLTextContainerEvents2_ondragendEventHandler HTMLTextContainerEvents2_Event_ondragend;

		// Token: 0x14001344 RID: 4932
		// (add) Token: 0x06009DD4 RID: 40404
		// (remove) Token: 0x06009DD5 RID: 40405
		public virtual extern event HTMLTextContainerEvents2_ondragenterEventHandler HTMLTextContainerEvents2_Event_ondragenter;

		// Token: 0x14001345 RID: 4933
		// (add) Token: 0x06009DD6 RID: 40406
		// (remove) Token: 0x06009DD7 RID: 40407
		public virtual extern event HTMLTextContainerEvents2_ondragoverEventHandler HTMLTextContainerEvents2_Event_ondragover;

		// Token: 0x14001346 RID: 4934
		// (add) Token: 0x06009DD8 RID: 40408
		// (remove) Token: 0x06009DD9 RID: 40409
		public virtual extern event HTMLTextContainerEvents2_ondragleaveEventHandler HTMLTextContainerEvents2_Event_ondragleave;

		// Token: 0x14001347 RID: 4935
		// (add) Token: 0x06009DDA RID: 40410
		// (remove) Token: 0x06009DDB RID: 40411
		public virtual extern event HTMLTextContainerEvents2_ondropEventHandler HTMLTextContainerEvents2_Event_ondrop;

		// Token: 0x14001348 RID: 4936
		// (add) Token: 0x06009DDC RID: 40412
		// (remove) Token: 0x06009DDD RID: 40413
		public virtual extern event HTMLTextContainerEvents2_onbeforecutEventHandler HTMLTextContainerEvents2_Event_onbeforecut;

		// Token: 0x14001349 RID: 4937
		// (add) Token: 0x06009DDE RID: 40414
		// (remove) Token: 0x06009DDF RID: 40415
		public virtual extern event HTMLTextContainerEvents2_oncutEventHandler HTMLTextContainerEvents2_Event_oncut;

		// Token: 0x1400134A RID: 4938
		// (add) Token: 0x06009DE0 RID: 40416
		// (remove) Token: 0x06009DE1 RID: 40417
		public virtual extern event HTMLTextContainerEvents2_onbeforecopyEventHandler HTMLTextContainerEvents2_Event_onbeforecopy;

		// Token: 0x1400134B RID: 4939
		// (add) Token: 0x06009DE2 RID: 40418
		// (remove) Token: 0x06009DE3 RID: 40419
		public virtual extern event HTMLTextContainerEvents2_oncopyEventHandler HTMLTextContainerEvents2_Event_oncopy;

		// Token: 0x1400134C RID: 4940
		// (add) Token: 0x06009DE4 RID: 40420
		// (remove) Token: 0x06009DE5 RID: 40421
		public virtual extern event HTMLTextContainerEvents2_onbeforepasteEventHandler HTMLTextContainerEvents2_Event_onbeforepaste;

		// Token: 0x1400134D RID: 4941
		// (add) Token: 0x06009DE6 RID: 40422
		// (remove) Token: 0x06009DE7 RID: 40423
		public virtual extern event HTMLTextContainerEvents2_onpasteEventHandler HTMLTextContainerEvents2_Event_onpaste;

		// Token: 0x1400134E RID: 4942
		// (add) Token: 0x06009DE8 RID: 40424
		// (remove) Token: 0x06009DE9 RID: 40425
		public virtual extern event HTMLTextContainerEvents2_oncontextmenuEventHandler HTMLTextContainerEvents2_Event_oncontextmenu;

		// Token: 0x1400134F RID: 4943
		// (add) Token: 0x06009DEA RID: 40426
		// (remove) Token: 0x06009DEB RID: 40427
		public virtual extern event HTMLTextContainerEvents2_onrowsdeleteEventHandler HTMLTextContainerEvents2_Event_onrowsdelete;

		// Token: 0x14001350 RID: 4944
		// (add) Token: 0x06009DEC RID: 40428
		// (remove) Token: 0x06009DED RID: 40429
		public virtual extern event HTMLTextContainerEvents2_onrowsinsertedEventHandler HTMLTextContainerEvents2_Event_onrowsinserted;

		// Token: 0x14001351 RID: 4945
		// (add) Token: 0x06009DEE RID: 40430
		// (remove) Token: 0x06009DEF RID: 40431
		public virtual extern event HTMLTextContainerEvents2_oncellchangeEventHandler HTMLTextContainerEvents2_Event_oncellchange;

		// Token: 0x14001352 RID: 4946
		// (add) Token: 0x06009DF0 RID: 40432
		// (remove) Token: 0x06009DF1 RID: 40433
		public virtual extern event HTMLTextContainerEvents2_onreadystatechangeEventHandler HTMLTextContainerEvents2_Event_onreadystatechange;

		// Token: 0x14001353 RID: 4947
		// (add) Token: 0x06009DF2 RID: 40434
		// (remove) Token: 0x06009DF3 RID: 40435
		public virtual extern event HTMLTextContainerEvents2_onlayoutcompleteEventHandler HTMLTextContainerEvents2_Event_onlayoutcomplete;

		// Token: 0x14001354 RID: 4948
		// (add) Token: 0x06009DF4 RID: 40436
		// (remove) Token: 0x06009DF5 RID: 40437
		public virtual extern event HTMLTextContainerEvents2_onpageEventHandler HTMLTextContainerEvents2_Event_onpage;

		// Token: 0x14001355 RID: 4949
		// (add) Token: 0x06009DF6 RID: 40438
		// (remove) Token: 0x06009DF7 RID: 40439
		public virtual extern event HTMLTextContainerEvents2_onmouseenterEventHandler HTMLTextContainerEvents2_Event_onmouseenter;

		// Token: 0x14001356 RID: 4950
		// (add) Token: 0x06009DF8 RID: 40440
		// (remove) Token: 0x06009DF9 RID: 40441
		public virtual extern event HTMLTextContainerEvents2_onmouseleaveEventHandler HTMLTextContainerEvents2_Event_onmouseleave;

		// Token: 0x14001357 RID: 4951
		// (add) Token: 0x06009DFA RID: 40442
		// (remove) Token: 0x06009DFB RID: 40443
		public virtual extern event HTMLTextContainerEvents2_onactivateEventHandler HTMLTextContainerEvents2_Event_onactivate;

		// Token: 0x14001358 RID: 4952
		// (add) Token: 0x06009DFC RID: 40444
		// (remove) Token: 0x06009DFD RID: 40445
		public virtual extern event HTMLTextContainerEvents2_ondeactivateEventHandler HTMLTextContainerEvents2_Event_ondeactivate;

		// Token: 0x14001359 RID: 4953
		// (add) Token: 0x06009DFE RID: 40446
		// (remove) Token: 0x06009DFF RID: 40447
		public virtual extern event HTMLTextContainerEvents2_onbeforedeactivateEventHandler HTMLTextContainerEvents2_Event_onbeforedeactivate;

		// Token: 0x1400135A RID: 4954
		// (add) Token: 0x06009E00 RID: 40448
		// (remove) Token: 0x06009E01 RID: 40449
		public virtual extern event HTMLTextContainerEvents2_onbeforeactivateEventHandler HTMLTextContainerEvents2_Event_onbeforeactivate;

		// Token: 0x1400135B RID: 4955
		// (add) Token: 0x06009E02 RID: 40450
		// (remove) Token: 0x06009E03 RID: 40451
		public virtual extern event HTMLTextContainerEvents2_onfocusinEventHandler HTMLTextContainerEvents2_Event_onfocusin;

		// Token: 0x1400135C RID: 4956
		// (add) Token: 0x06009E04 RID: 40452
		// (remove) Token: 0x06009E05 RID: 40453
		public virtual extern event HTMLTextContainerEvents2_onfocusoutEventHandler HTMLTextContainerEvents2_Event_onfocusout;

		// Token: 0x1400135D RID: 4957
		// (add) Token: 0x06009E06 RID: 40454
		// (remove) Token: 0x06009E07 RID: 40455
		public virtual extern event HTMLTextContainerEvents2_onmoveEventHandler HTMLTextContainerEvents2_Event_onmove;

		// Token: 0x1400135E RID: 4958
		// (add) Token: 0x06009E08 RID: 40456
		// (remove) Token: 0x06009E09 RID: 40457
		public virtual extern event HTMLTextContainerEvents2_oncontrolselectEventHandler HTMLTextContainerEvents2_Event_oncontrolselect;

		// Token: 0x1400135F RID: 4959
		// (add) Token: 0x06009E0A RID: 40458
		// (remove) Token: 0x06009E0B RID: 40459
		public virtual extern event HTMLTextContainerEvents2_onmovestartEventHandler HTMLTextContainerEvents2_Event_onmovestart;

		// Token: 0x14001360 RID: 4960
		// (add) Token: 0x06009E0C RID: 40460
		// (remove) Token: 0x06009E0D RID: 40461
		public virtual extern event HTMLTextContainerEvents2_onmoveendEventHandler HTMLTextContainerEvents2_Event_onmoveend;

		// Token: 0x14001361 RID: 4961
		// (add) Token: 0x06009E0E RID: 40462
		// (remove) Token: 0x06009E0F RID: 40463
		public virtual extern event HTMLTextContainerEvents2_onresizestartEventHandler HTMLTextContainerEvents2_Event_onresizestart;

		// Token: 0x14001362 RID: 4962
		// (add) Token: 0x06009E10 RID: 40464
		// (remove) Token: 0x06009E11 RID: 40465
		public virtual extern event HTMLTextContainerEvents2_onresizeendEventHandler HTMLTextContainerEvents2_Event_onresizeend;

		// Token: 0x14001363 RID: 4963
		// (add) Token: 0x06009E12 RID: 40466
		// (remove) Token: 0x06009E13 RID: 40467
		public virtual extern event HTMLTextContainerEvents2_onmousewheelEventHandler HTMLTextContainerEvents2_Event_onmousewheel;

		// Token: 0x14001364 RID: 4964
		// (add) Token: 0x06009E14 RID: 40468
		// (remove) Token: 0x06009E15 RID: 40469
		public virtual extern event HTMLTextContainerEvents2_onchangeEventHandler HTMLTextContainerEvents2_Event_onchange;

		// Token: 0x14001365 RID: 4965
		// (add) Token: 0x06009E16 RID: 40470
		// (remove) Token: 0x06009E17 RID: 40471
		public virtual extern event HTMLTextContainerEvents2_onselectEventHandler HTMLTextContainerEvents2_Event_onselect;
	}
}
