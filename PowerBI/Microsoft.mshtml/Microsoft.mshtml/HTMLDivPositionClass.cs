using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BDD RID: 3037
	[ComSourceInterfaces("mshtml.HTMLTextContainerEvents\0mshtml.HTMLTextContainerEvents2\0\0")]
	[ClassInterface(0)]
	[Guid("3050F249-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLDivPositionClass : DispHTMLDivPosition, HTMLDivPosition, HTMLTextContainerEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLTextContainer, IHTMLDivPosition, HTMLTextContainerEvents2_Event
	{
		// Token: 0x06014240 RID: 82496
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLDivPositionClass();

		// Token: 0x06014241 RID: 82497
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06014242 RID: 82498
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06014243 RID: 82499
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17006AB0 RID: 27312
		// (get) Token: 0x06014245 RID: 82501
		// (set) Token: 0x06014244 RID: 82500
		[DispId(-2147417111)]
		public virtual extern string className
		{
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417111)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006AB1 RID: 27313
		// (get) Token: 0x06014247 RID: 82503
		// (set) Token: 0x06014246 RID: 82502
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

		// Token: 0x17006AB2 RID: 27314
		// (get) Token: 0x06014248 RID: 82504
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006AB3 RID: 27315
		// (get) Token: 0x06014249 RID: 82505
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006AB4 RID: 27316
		// (get) Token: 0x0601424A RID: 82506
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006AB5 RID: 27317
		// (get) Token: 0x0601424C RID: 82508
		// (set) Token: 0x0601424B RID: 82507
		[DispId(-2147412099)]
		public virtual extern object onhelp
		{
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AB6 RID: 27318
		// (get) Token: 0x0601424E RID: 82510
		// (set) Token: 0x0601424D RID: 82509
		[DispId(-2147412104)]
		public virtual extern object onclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AB7 RID: 27319
		// (get) Token: 0x06014250 RID: 82512
		// (set) Token: 0x0601424F RID: 82511
		[DispId(-2147412103)]
		public virtual extern object ondblclick
		{
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AB8 RID: 27320
		// (get) Token: 0x06014252 RID: 82514
		// (set) Token: 0x06014251 RID: 82513
		[DispId(-2147412107)]
		public virtual extern object onkeydown
		{
			[DispId(-2147412107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AB9 RID: 27321
		// (get) Token: 0x06014254 RID: 82516
		// (set) Token: 0x06014253 RID: 82515
		[DispId(-2147412106)]
		public virtual extern object onkeyup
		{
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006ABA RID: 27322
		// (get) Token: 0x06014256 RID: 82518
		// (set) Token: 0x06014255 RID: 82517
		[DispId(-2147412105)]
		public virtual extern object onkeypress
		{
			[DispId(-2147412105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006ABB RID: 27323
		// (get) Token: 0x06014258 RID: 82520
		// (set) Token: 0x06014257 RID: 82519
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006ABC RID: 27324
		// (get) Token: 0x0601425A RID: 82522
		// (set) Token: 0x06014259 RID: 82521
		[DispId(-2147412112)]
		public virtual extern object onmouseover
		{
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006ABD RID: 27325
		// (get) Token: 0x0601425C RID: 82524
		// (set) Token: 0x0601425B RID: 82523
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

		// Token: 0x17006ABE RID: 27326
		// (get) Token: 0x0601425E RID: 82526
		// (set) Token: 0x0601425D RID: 82525
		[DispId(-2147412110)]
		public virtual extern object onmousedown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006ABF RID: 27327
		// (get) Token: 0x06014260 RID: 82528
		// (set) Token: 0x0601425F RID: 82527
		[DispId(-2147412109)]
		public virtual extern object onmouseup
		{
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AC0 RID: 27328
		// (get) Token: 0x06014261 RID: 82529
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006AC1 RID: 27329
		// (get) Token: 0x06014263 RID: 82531
		// (set) Token: 0x06014262 RID: 82530
		[DispId(-2147418043)]
		public virtual extern string title
		{
			[TypeLibFunc(20)]
			[DispId(-2147418043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006AC2 RID: 27330
		// (get) Token: 0x06014265 RID: 82533
		// (set) Token: 0x06014264 RID: 82532
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006AC3 RID: 27331
		// (get) Token: 0x06014267 RID: 82535
		// (set) Token: 0x06014266 RID: 82534
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

		// Token: 0x06014268 RID: 82536
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06014269 RID: 82537
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17006AC4 RID: 27332
		// (get) Token: 0x0601426A RID: 82538
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006AC5 RID: 27333
		// (get) Token: 0x0601426B RID: 82539
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17006AC6 RID: 27334
		// (get) Token: 0x0601426D RID: 82541
		// (set) Token: 0x0601426C RID: 82540
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

		// Token: 0x17006AC7 RID: 27335
		// (get) Token: 0x0601426E RID: 82542
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006AC8 RID: 27336
		// (get) Token: 0x0601426F RID: 82543
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006AC9 RID: 27337
		// (get) Token: 0x06014270 RID: 82544
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006ACA RID: 27338
		// (get) Token: 0x06014271 RID: 82545
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006ACB RID: 27339
		// (get) Token: 0x06014272 RID: 82546
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006ACC RID: 27340
		// (get) Token: 0x06014274 RID: 82548
		// (set) Token: 0x06014273 RID: 82547
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

		// Token: 0x17006ACD RID: 27341
		// (get) Token: 0x06014276 RID: 82550
		// (set) Token: 0x06014275 RID: 82549
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

		// Token: 0x17006ACE RID: 27342
		// (get) Token: 0x06014278 RID: 82552
		// (set) Token: 0x06014277 RID: 82551
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

		// Token: 0x17006ACF RID: 27343
		// (get) Token: 0x0601427A RID: 82554
		// (set) Token: 0x06014279 RID: 82553
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

		// Token: 0x0601427B RID: 82555
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0601427C RID: 82556
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17006AD0 RID: 27344
		// (get) Token: 0x0601427D RID: 82557
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006AD1 RID: 27345
		// (get) Token: 0x0601427E RID: 82558
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601427F RID: 82559
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17006AD2 RID: 27346
		// (get) Token: 0x06014280 RID: 82560
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006AD3 RID: 27347
		// (get) Token: 0x06014282 RID: 82562
		// (set) Token: 0x06014281 RID: 82561
		[DispId(-2147412077)]
		public virtual extern object ondragstart
		{
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06014283 RID: 82563
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17006AD4 RID: 27348
		// (get) Token: 0x06014285 RID: 82565
		// (set) Token: 0x06014284 RID: 82564
		[DispId(-2147412091)]
		public virtual extern object onbeforeupdate
		{
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AD5 RID: 27349
		// (get) Token: 0x06014287 RID: 82567
		// (set) Token: 0x06014286 RID: 82566
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

		// Token: 0x17006AD6 RID: 27350
		// (get) Token: 0x06014289 RID: 82569
		// (set) Token: 0x06014288 RID: 82568
		[DispId(-2147412074)]
		public virtual extern object onerrorupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AD7 RID: 27351
		// (get) Token: 0x0601428B RID: 82571
		// (set) Token: 0x0601428A RID: 82570
		[DispId(-2147412094)]
		public virtual extern object onrowexit
		{
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AD8 RID: 27352
		// (get) Token: 0x0601428D RID: 82573
		// (set) Token: 0x0601428C RID: 82572
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

		// Token: 0x17006AD9 RID: 27353
		// (get) Token: 0x0601428F RID: 82575
		// (set) Token: 0x0601428E RID: 82574
		[DispId(-2147412072)]
		public virtual extern object ondatasetchanged
		{
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006ADA RID: 27354
		// (get) Token: 0x06014291 RID: 82577
		// (set) Token: 0x06014290 RID: 82576
		[DispId(-2147412071)]
		public virtual extern object ondataavailable
		{
			[DispId(-2147412071)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412071)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006ADB RID: 27355
		// (get) Token: 0x06014293 RID: 82579
		// (set) Token: 0x06014292 RID: 82578
		[DispId(-2147412070)]
		public virtual extern object ondatasetcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006ADC RID: 27356
		// (get) Token: 0x06014295 RID: 82581
		// (set) Token: 0x06014294 RID: 82580
		[DispId(-2147412069)]
		public virtual extern object onfilterchange
		{
			[DispId(-2147412069)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006ADD RID: 27357
		// (get) Token: 0x06014296 RID: 82582
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006ADE RID: 27358
		// (get) Token: 0x06014297 RID: 82583
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006ADF RID: 27359
		// (get) Token: 0x06014298 RID: 82584
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06014299 RID: 82585
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0601429A RID: 82586
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17006AE0 RID: 27360
		// (get) Token: 0x0601429C RID: 82588
		// (set) Token: 0x0601429B RID: 82587
		[DispId(-2147412066)]
		public virtual extern object onlosecapture
		{
			[DispId(-2147412066)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412066)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0601429D RID: 82589
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0601429E RID: 82590
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17006AE1 RID: 27361
		// (get) Token: 0x060142A0 RID: 82592
		// (set) Token: 0x0601429F RID: 82591
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

		// Token: 0x17006AE2 RID: 27362
		// (get) Token: 0x060142A2 RID: 82594
		// (set) Token: 0x060142A1 RID: 82593
		[DispId(-2147412063)]
		public virtual extern object ondrag
		{
			[TypeLibFunc(20)]
			[DispId(-2147412063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AE3 RID: 27363
		// (get) Token: 0x060142A4 RID: 82596
		// (set) Token: 0x060142A3 RID: 82595
		[DispId(-2147412062)]
		public virtual extern object ondragend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AE4 RID: 27364
		// (get) Token: 0x060142A6 RID: 82598
		// (set) Token: 0x060142A5 RID: 82597
		[DispId(-2147412061)]
		public virtual extern object ondragenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AE5 RID: 27365
		// (get) Token: 0x060142A8 RID: 82600
		// (set) Token: 0x060142A7 RID: 82599
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

		// Token: 0x17006AE6 RID: 27366
		// (get) Token: 0x060142AA RID: 82602
		// (set) Token: 0x060142A9 RID: 82601
		[DispId(-2147412059)]
		public virtual extern object ondragleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AE7 RID: 27367
		// (get) Token: 0x060142AC RID: 82604
		// (set) Token: 0x060142AB RID: 82603
		[DispId(-2147412058)]
		public virtual extern object ondrop
		{
			[TypeLibFunc(20)]
			[DispId(-2147412058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AE8 RID: 27368
		// (get) Token: 0x060142AE RID: 82606
		// (set) Token: 0x060142AD RID: 82605
		[DispId(-2147412054)]
		public virtual extern object onbeforecut
		{
			[DispId(-2147412054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AE9 RID: 27369
		// (get) Token: 0x060142B0 RID: 82608
		// (set) Token: 0x060142AF RID: 82607
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AEA RID: 27370
		// (get) Token: 0x060142B2 RID: 82610
		// (set) Token: 0x060142B1 RID: 82609
		[DispId(-2147412053)]
		public virtual extern object onbeforecopy
		{
			[TypeLibFunc(20)]
			[DispId(-2147412053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AEB RID: 27371
		// (get) Token: 0x060142B4 RID: 82612
		// (set) Token: 0x060142B3 RID: 82611
		[DispId(-2147412056)]
		public virtual extern object oncopy
		{
			[DispId(-2147412056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AEC RID: 27372
		// (get) Token: 0x060142B6 RID: 82614
		// (set) Token: 0x060142B5 RID: 82613
		[DispId(-2147412052)]
		public virtual extern object onbeforepaste
		{
			[TypeLibFunc(20)]
			[DispId(-2147412052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AED RID: 27373
		// (get) Token: 0x060142B8 RID: 82616
		// (set) Token: 0x060142B7 RID: 82615
		[DispId(-2147412055)]
		public virtual extern object onpaste
		{
			[TypeLibFunc(20)]
			[DispId(-2147412055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AEE RID: 27374
		// (get) Token: 0x060142B9 RID: 82617
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006AEF RID: 27375
		// (get) Token: 0x060142BB RID: 82619
		// (set) Token: 0x060142BA RID: 82618
		[DispId(-2147412065)]
		public virtual extern object onpropertychange
		{
			[DispId(-2147412065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060142BC RID: 82620
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x060142BD RID: 82621
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x060142BE RID: 82622
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x060142BF RID: 82623
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x060142C0 RID: 82624
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17006AF0 RID: 27376
		// (get) Token: 0x060142C2 RID: 82626
		// (set) Token: 0x060142C1 RID: 82625
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

		// Token: 0x060142C3 RID: 82627
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17006AF1 RID: 27377
		// (get) Token: 0x060142C5 RID: 82629
		// (set) Token: 0x060142C4 RID: 82628
		[DispId(-2147416107)]
		public virtual extern string accessKey
		{
			[DispId(-2147416107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006AF2 RID: 27378
		// (get) Token: 0x060142C7 RID: 82631
		// (set) Token: 0x060142C6 RID: 82630
		[DispId(-2147412097)]
		public virtual extern object onblur
		{
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AF3 RID: 27379
		// (get) Token: 0x060142C9 RID: 82633
		// (set) Token: 0x060142C8 RID: 82632
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AF4 RID: 27380
		// (get) Token: 0x060142CB RID: 82635
		// (set) Token: 0x060142CA RID: 82634
		[DispId(-2147412076)]
		public virtual extern object onresize
		{
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060142CC RID: 82636
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x060142CD RID: 82637
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060142CE RID: 82638
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17006AF5 RID: 27381
		// (get) Token: 0x060142CF RID: 82639
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006AF6 RID: 27382
		// (get) Token: 0x060142D0 RID: 82640
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006AF7 RID: 27383
		// (get) Token: 0x060142D1 RID: 82641
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006AF8 RID: 27384
		// (get) Token: 0x060142D2 RID: 82642
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060142D3 RID: 82643
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060142D4 RID: 82644
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17006AF9 RID: 27385
		// (get) Token: 0x060142D5 RID: 82645
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17006AFA RID: 27386
		// (get) Token: 0x060142D7 RID: 82647
		// (set) Token: 0x060142D6 RID: 82646
		[DispId(-2147412087)]
		public virtual extern object onreadystatechange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AFB RID: 27387
		// (get) Token: 0x060142D9 RID: 82649
		// (set) Token: 0x060142D8 RID: 82648
		[DispId(-2147412050)]
		public virtual extern object onrowsdelete
		{
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AFC RID: 27388
		// (get) Token: 0x060142DB RID: 82651
		// (set) Token: 0x060142DA RID: 82650
		[DispId(-2147412049)]
		public virtual extern object onrowsinserted
		{
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AFD RID: 27389
		// (get) Token: 0x060142DD RID: 82653
		// (set) Token: 0x060142DC RID: 82652
		[DispId(-2147412048)]
		public virtual extern object oncellchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006AFE RID: 27390
		// (get) Token: 0x060142DF RID: 82655
		// (set) Token: 0x060142DE RID: 82654
		[DispId(-2147412995)]
		public virtual extern string dir
		{
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x060142E0 RID: 82656
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17006AFF RID: 27391
		// (get) Token: 0x060142E1 RID: 82657
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B00 RID: 27392
		// (get) Token: 0x060142E2 RID: 82658
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B01 RID: 27393
		// (get) Token: 0x060142E4 RID: 82660
		// (set) Token: 0x060142E3 RID: 82659
		[DispId(-2147417053)]
		public virtual extern int scrollTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147417053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17006B02 RID: 27394
		// (get) Token: 0x060142E6 RID: 82662
		// (set) Token: 0x060142E5 RID: 82661
		[DispId(-2147417052)]
		public virtual extern int scrollLeft
		{
			[DispId(-2147417052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x060142E7 RID: 82663
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17006B03 RID: 27395
		// (get) Token: 0x060142E9 RID: 82665
		// (set) Token: 0x060142E8 RID: 82664
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

		// Token: 0x060142EA RID: 82666
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060142EB RID: 82667
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060142EC RID: 82668
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060142ED RID: 82669
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17006B04 RID: 27396
		// (get) Token: 0x060142EE RID: 82670
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060142EF RID: 82671
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060142F0 RID: 82672
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17006B05 RID: 27397
		// (get) Token: 0x060142F1 RID: 82673
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B06 RID: 27398
		// (get) Token: 0x060142F2 RID: 82674
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006B07 RID: 27399
		// (get) Token: 0x060142F4 RID: 82676
		// (set) Token: 0x060142F3 RID: 82675
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

		// Token: 0x17006B08 RID: 27400
		// (get) Token: 0x060142F6 RID: 82678
		// (set) Token: 0x060142F5 RID: 82677
		[DispId(-2147412043)]
		public virtual extern object onbeforeeditfocus
		{
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B09 RID: 27401
		// (get) Token: 0x060142F7 RID: 82679
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060142F8 RID: 82680
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060142F9 RID: 82681
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17006B0A RID: 27402
		// (get) Token: 0x060142FA RID: 82682
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B0B RID: 27403
		// (get) Token: 0x060142FB RID: 82683
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B0C RID: 27404
		// (get) Token: 0x060142FD RID: 82685
		// (set) Token: 0x060142FC RID: 82684
		[DispId(-2147412039)]
		public virtual extern object onlayoutcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B0D RID: 27405
		// (get) Token: 0x060142FF RID: 82687
		// (set) Token: 0x060142FE RID: 82686
		[DispId(-2147412038)]
		public virtual extern object onpage
		{
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B0E RID: 27406
		// (get) Token: 0x06014301 RID: 82689
		// (set) Token: 0x06014300 RID: 82688
		[DispId(-2147417012)]
		public virtual extern bool inflateBlock
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17006B0F RID: 27407
		// (get) Token: 0x06014303 RID: 82691
		// (set) Token: 0x06014302 RID: 82690
		[DispId(-2147412035)]
		public virtual extern object onbeforedeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06014304 RID: 82692
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17006B10 RID: 27408
		// (get) Token: 0x06014306 RID: 82694
		// (set) Token: 0x06014305 RID: 82693
		[DispId(-2147412950)]
		public virtual extern string contentEditable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006B11 RID: 27409
		// (get) Token: 0x06014307 RID: 82695
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B12 RID: 27410
		// (get) Token: 0x06014309 RID: 82697
		// (set) Token: 0x06014308 RID: 82696
		[DispId(-2147412949)]
		public virtual extern bool hideFocus
		{
			[DispId(-2147412949)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412949)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17006B13 RID: 27411
		// (get) Token: 0x0601430B RID: 82699
		// (set) Token: 0x0601430A RID: 82698
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

		// Token: 0x17006B14 RID: 27412
		// (get) Token: 0x0601430C RID: 82700
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B15 RID: 27413
		// (get) Token: 0x0601430E RID: 82702
		// (set) Token: 0x0601430D RID: 82701
		[DispId(-2147412034)]
		public virtual extern object onmove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B16 RID: 27414
		// (get) Token: 0x06014310 RID: 82704
		// (set) Token: 0x0601430F RID: 82703
		[DispId(-2147412033)]
		public virtual extern object oncontrolselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06014311 RID: 82705
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17006B17 RID: 27415
		// (get) Token: 0x06014313 RID: 82707
		// (set) Token: 0x06014312 RID: 82706
		[DispId(-2147412029)]
		public virtual extern object onresizestart
		{
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B18 RID: 27416
		// (get) Token: 0x06014315 RID: 82709
		// (set) Token: 0x06014314 RID: 82708
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

		// Token: 0x17006B19 RID: 27417
		// (get) Token: 0x06014317 RID: 82711
		// (set) Token: 0x06014316 RID: 82710
		[DispId(-2147412031)]
		public virtual extern object onmovestart
		{
			[DispId(-2147412031)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412031)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B1A RID: 27418
		// (get) Token: 0x06014319 RID: 82713
		// (set) Token: 0x06014318 RID: 82712
		[DispId(-2147412030)]
		public virtual extern object onmoveend
		{
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B1B RID: 27419
		// (get) Token: 0x0601431B RID: 82715
		// (set) Token: 0x0601431A RID: 82714
		[DispId(-2147412027)]
		public virtual extern object onmouseenter
		{
			[DispId(-2147412027)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B1C RID: 27420
		// (get) Token: 0x0601431D RID: 82717
		// (set) Token: 0x0601431C RID: 82716
		[DispId(-2147412026)]
		public virtual extern object onmouseleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412026)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B1D RID: 27421
		// (get) Token: 0x0601431F RID: 82719
		// (set) Token: 0x0601431E RID: 82718
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B1E RID: 27422
		// (get) Token: 0x06014321 RID: 82721
		// (set) Token: 0x06014320 RID: 82720
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

		// Token: 0x06014322 RID: 82722
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17006B1F RID: 27423
		// (get) Token: 0x06014323 RID: 82723
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B20 RID: 27424
		// (get) Token: 0x06014325 RID: 82725
		// (set) Token: 0x06014324 RID: 82724
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

		// Token: 0x06014326 RID: 82726
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06014327 RID: 82727
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06014328 RID: 82728
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06014329 RID: 82729
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17006B21 RID: 27425
		// (get) Token: 0x0601432B RID: 82731
		// (set) Token: 0x0601432A RID: 82730
		[DispId(-2147412022)]
		public virtual extern object onbeforeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B22 RID: 27426
		// (get) Token: 0x0601432D RID: 82733
		// (set) Token: 0x0601432C RID: 82732
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

		// Token: 0x17006B23 RID: 27427
		// (get) Token: 0x0601432F RID: 82735
		// (set) Token: 0x0601432E RID: 82734
		[DispId(-2147412020)]
		public virtual extern object onfocusout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006B24 RID: 27428
		// (get) Token: 0x06014330 RID: 82736
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B25 RID: 27429
		// (get) Token: 0x06014331 RID: 82737
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006B26 RID: 27430
		// (get) Token: 0x06014332 RID: 82738
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B27 RID: 27431
		// (get) Token: 0x06014333 RID: 82739
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06014334 RID: 82740
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17006B28 RID: 27432
		// (get) Token: 0x06014335 RID: 82741
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006B29 RID: 27433
		// (get) Token: 0x06014336 RID: 82742
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06014337 RID: 82743
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06014338 RID: 82744
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06014339 RID: 82745
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0601433A RID: 82746
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0601433B RID: 82747
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0601433C RID: 82748
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0601433D RID: 82749
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0601433E RID: 82750
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17006B2A RID: 27434
		// (get) Token: 0x0601433F RID: 82751
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006B2B RID: 27435
		// (get) Token: 0x06014341 RID: 82753
		// (set) Token: 0x06014340 RID: 82752
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

		// Token: 0x17006B2C RID: 27436
		// (get) Token: 0x06014342 RID: 82754
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B2D RID: 27437
		// (get) Token: 0x06014343 RID: 82755
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B2E RID: 27438
		// (get) Token: 0x06014344 RID: 82756
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B2F RID: 27439
		// (get) Token: 0x06014345 RID: 82757
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B30 RID: 27440
		// (get) Token: 0x06014346 RID: 82758
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006B31 RID: 27441
		// (get) Token: 0x06014348 RID: 82760
		// (set) Token: 0x06014347 RID: 82759
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

		// Token: 0x17006B32 RID: 27442
		// (get) Token: 0x0601434A RID: 82762
		// (set) Token: 0x06014349 RID: 82761
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

		// Token: 0x17006B33 RID: 27443
		// (get) Token: 0x0601434C RID: 82764
		// (set) Token: 0x0601434B RID: 82763
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

		// Token: 0x17006B34 RID: 27444
		// (get) Token: 0x0601434E RID: 82766
		// (set) Token: 0x0601434D RID: 82765
		[DispId(-2147418039)]
		public virtual extern string align
		{
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0601434F RID: 82767
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06014350 RID: 82768
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06014351 RID: 82769
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17006B35 RID: 27445
		// (get) Token: 0x06014353 RID: 82771
		// (set) Token: 0x06014352 RID: 82770
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

		// Token: 0x17006B36 RID: 27446
		// (get) Token: 0x06014355 RID: 82773
		// (set) Token: 0x06014354 RID: 82772
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

		// Token: 0x17006B37 RID: 27447
		// (get) Token: 0x06014356 RID: 82774
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006B38 RID: 27448
		// (get) Token: 0x06014357 RID: 82775
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B39 RID: 27449
		// (get) Token: 0x06014358 RID: 82776
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B3A RID: 27450
		// (get) Token: 0x0601435A RID: 82778
		// (set) Token: 0x06014359 RID: 82777
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

		// Token: 0x17006B3B RID: 27451
		// (get) Token: 0x0601435C RID: 82780
		// (set) Token: 0x0601435B RID: 82779
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

		// Token: 0x17006B3C RID: 27452
		// (get) Token: 0x0601435E RID: 82782
		// (set) Token: 0x0601435D RID: 82781
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

		// Token: 0x17006B3D RID: 27453
		// (get) Token: 0x06014360 RID: 82784
		// (set) Token: 0x0601435F RID: 82783
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

		// Token: 0x17006B3E RID: 27454
		// (get) Token: 0x06014362 RID: 82786
		// (set) Token: 0x06014361 RID: 82785
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

		// Token: 0x17006B3F RID: 27455
		// (get) Token: 0x06014364 RID: 82788
		// (set) Token: 0x06014363 RID: 82787
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

		// Token: 0x17006B40 RID: 27456
		// (get) Token: 0x06014366 RID: 82790
		// (set) Token: 0x06014365 RID: 82789
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

		// Token: 0x17006B41 RID: 27457
		// (get) Token: 0x06014368 RID: 82792
		// (set) Token: 0x06014367 RID: 82791
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

		// Token: 0x17006B42 RID: 27458
		// (get) Token: 0x0601436A RID: 82794
		// (set) Token: 0x06014369 RID: 82793
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

		// Token: 0x17006B43 RID: 27459
		// (get) Token: 0x0601436C RID: 82796
		// (set) Token: 0x0601436B RID: 82795
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

		// Token: 0x17006B44 RID: 27460
		// (get) Token: 0x0601436E RID: 82798
		// (set) Token: 0x0601436D RID: 82797
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

		// Token: 0x17006B45 RID: 27461
		// (get) Token: 0x0601436F RID: 82799
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006B46 RID: 27462
		// (get) Token: 0x06014371 RID: 82801
		// (set) Token: 0x06014370 RID: 82800
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

		// Token: 0x17006B47 RID: 27463
		// (get) Token: 0x06014373 RID: 82803
		// (set) Token: 0x06014372 RID: 82802
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

		// Token: 0x17006B48 RID: 27464
		// (get) Token: 0x06014375 RID: 82805
		// (set) Token: 0x06014374 RID: 82804
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

		// Token: 0x06014376 RID: 82806
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06014377 RID: 82807
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17006B49 RID: 27465
		// (get) Token: 0x06014378 RID: 82808
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B4A RID: 27466
		// (get) Token: 0x06014379 RID: 82809
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17006B4B RID: 27467
		// (get) Token: 0x0601437B RID: 82811
		// (set) Token: 0x0601437A RID: 82810
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

		// Token: 0x17006B4C RID: 27468
		// (get) Token: 0x0601437C RID: 82812
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B4D RID: 27469
		// (get) Token: 0x0601437D RID: 82813
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B4E RID: 27470
		// (get) Token: 0x0601437E RID: 82814
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B4F RID: 27471
		// (get) Token: 0x0601437F RID: 82815
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B50 RID: 27472
		// (get) Token: 0x06014380 RID: 82816
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B51 RID: 27473
		// (get) Token: 0x06014382 RID: 82818
		// (set) Token: 0x06014381 RID: 82817
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

		// Token: 0x17006B52 RID: 27474
		// (get) Token: 0x06014384 RID: 82820
		// (set) Token: 0x06014383 RID: 82819
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

		// Token: 0x17006B53 RID: 27475
		// (get) Token: 0x06014386 RID: 82822
		// (set) Token: 0x06014385 RID: 82821
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

		// Token: 0x17006B54 RID: 27476
		// (get) Token: 0x06014388 RID: 82824
		// (set) Token: 0x06014387 RID: 82823
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

		// Token: 0x06014389 RID: 82825
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0601438A RID: 82826
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17006B55 RID: 27477
		// (get) Token: 0x0601438B RID: 82827
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B56 RID: 27478
		// (get) Token: 0x0601438C RID: 82828
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601438D RID: 82829
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17006B57 RID: 27479
		// (get) Token: 0x0601438E RID: 82830
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B58 RID: 27480
		// (get) Token: 0x06014390 RID: 82832
		// (set) Token: 0x0601438F RID: 82831
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

		// Token: 0x06014391 RID: 82833
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17006B59 RID: 27481
		// (get) Token: 0x06014393 RID: 82835
		// (set) Token: 0x06014392 RID: 82834
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

		// Token: 0x17006B5A RID: 27482
		// (get) Token: 0x06014395 RID: 82837
		// (set) Token: 0x06014394 RID: 82836
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

		// Token: 0x17006B5B RID: 27483
		// (get) Token: 0x06014397 RID: 82839
		// (set) Token: 0x06014396 RID: 82838
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

		// Token: 0x17006B5C RID: 27484
		// (get) Token: 0x06014399 RID: 82841
		// (set) Token: 0x06014398 RID: 82840
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

		// Token: 0x17006B5D RID: 27485
		// (get) Token: 0x0601439B RID: 82843
		// (set) Token: 0x0601439A RID: 82842
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

		// Token: 0x17006B5E RID: 27486
		// (get) Token: 0x0601439D RID: 82845
		// (set) Token: 0x0601439C RID: 82844
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

		// Token: 0x17006B5F RID: 27487
		// (get) Token: 0x0601439F RID: 82847
		// (set) Token: 0x0601439E RID: 82846
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

		// Token: 0x17006B60 RID: 27488
		// (get) Token: 0x060143A1 RID: 82849
		// (set) Token: 0x060143A0 RID: 82848
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

		// Token: 0x17006B61 RID: 27489
		// (get) Token: 0x060143A3 RID: 82851
		// (set) Token: 0x060143A2 RID: 82850
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

		// Token: 0x17006B62 RID: 27490
		// (get) Token: 0x060143A4 RID: 82852
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006B63 RID: 27491
		// (get) Token: 0x060143A5 RID: 82853
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006B64 RID: 27492
		// (get) Token: 0x060143A6 RID: 82854
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060143A7 RID: 82855
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x060143A8 RID: 82856
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17006B65 RID: 27493
		// (get) Token: 0x060143AA RID: 82858
		// (set) Token: 0x060143A9 RID: 82857
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

		// Token: 0x060143AB RID: 82859
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x060143AC RID: 82860
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17006B66 RID: 27494
		// (get) Token: 0x060143AE RID: 82862
		// (set) Token: 0x060143AD RID: 82861
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

		// Token: 0x17006B67 RID: 27495
		// (get) Token: 0x060143B0 RID: 82864
		// (set) Token: 0x060143AF RID: 82863
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

		// Token: 0x17006B68 RID: 27496
		// (get) Token: 0x060143B2 RID: 82866
		// (set) Token: 0x060143B1 RID: 82865
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

		// Token: 0x17006B69 RID: 27497
		// (get) Token: 0x060143B4 RID: 82868
		// (set) Token: 0x060143B3 RID: 82867
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

		// Token: 0x17006B6A RID: 27498
		// (get) Token: 0x060143B6 RID: 82870
		// (set) Token: 0x060143B5 RID: 82869
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

		// Token: 0x17006B6B RID: 27499
		// (get) Token: 0x060143B8 RID: 82872
		// (set) Token: 0x060143B7 RID: 82871
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

		// Token: 0x17006B6C RID: 27500
		// (get) Token: 0x060143BA RID: 82874
		// (set) Token: 0x060143B9 RID: 82873
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

		// Token: 0x17006B6D RID: 27501
		// (get) Token: 0x060143BC RID: 82876
		// (set) Token: 0x060143BB RID: 82875
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

		// Token: 0x17006B6E RID: 27502
		// (get) Token: 0x060143BE RID: 82878
		// (set) Token: 0x060143BD RID: 82877
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

		// Token: 0x17006B6F RID: 27503
		// (get) Token: 0x060143C0 RID: 82880
		// (set) Token: 0x060143BF RID: 82879
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

		// Token: 0x17006B70 RID: 27504
		// (get) Token: 0x060143C2 RID: 82882
		// (set) Token: 0x060143C1 RID: 82881
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

		// Token: 0x17006B71 RID: 27505
		// (get) Token: 0x060143C4 RID: 82884
		// (set) Token: 0x060143C3 RID: 82883
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

		// Token: 0x17006B72 RID: 27506
		// (get) Token: 0x060143C6 RID: 82886
		// (set) Token: 0x060143C5 RID: 82885
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

		// Token: 0x17006B73 RID: 27507
		// (get) Token: 0x060143C7 RID: 82887
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B74 RID: 27508
		// (get) Token: 0x060143C9 RID: 82889
		// (set) Token: 0x060143C8 RID: 82888
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

		// Token: 0x060143CA RID: 82890
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x060143CB RID: 82891
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x060143CC RID: 82892
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x060143CD RID: 82893
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x060143CE RID: 82894
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17006B75 RID: 27509
		// (get) Token: 0x060143D0 RID: 82896
		// (set) Token: 0x060143CF RID: 82895
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

		// Token: 0x060143D1 RID: 82897
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17006B76 RID: 27510
		// (get) Token: 0x060143D3 RID: 82899
		// (set) Token: 0x060143D2 RID: 82898
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

		// Token: 0x17006B77 RID: 27511
		// (get) Token: 0x060143D5 RID: 82901
		// (set) Token: 0x060143D4 RID: 82900
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

		// Token: 0x17006B78 RID: 27512
		// (get) Token: 0x060143D7 RID: 82903
		// (set) Token: 0x060143D6 RID: 82902
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

		// Token: 0x17006B79 RID: 27513
		// (get) Token: 0x060143D9 RID: 82905
		// (set) Token: 0x060143D8 RID: 82904
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

		// Token: 0x060143DA RID: 82906
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x060143DB RID: 82907
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060143DC RID: 82908
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17006B7A RID: 27514
		// (get) Token: 0x060143DD RID: 82909
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B7B RID: 27515
		// (get) Token: 0x060143DE RID: 82910
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B7C RID: 27516
		// (get) Token: 0x060143DF RID: 82911
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B7D RID: 27517
		// (get) Token: 0x060143E0 RID: 82912
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060143E1 RID: 82913
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060143E2 RID: 82914
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17006B7E RID: 27518
		// (get) Token: 0x060143E3 RID: 82915
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17006B7F RID: 27519
		// (get) Token: 0x060143E5 RID: 82917
		// (set) Token: 0x060143E4 RID: 82916
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

		// Token: 0x17006B80 RID: 27520
		// (get) Token: 0x060143E7 RID: 82919
		// (set) Token: 0x060143E6 RID: 82918
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

		// Token: 0x17006B81 RID: 27521
		// (get) Token: 0x060143E9 RID: 82921
		// (set) Token: 0x060143E8 RID: 82920
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

		// Token: 0x17006B82 RID: 27522
		// (get) Token: 0x060143EB RID: 82923
		// (set) Token: 0x060143EA RID: 82922
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

		// Token: 0x17006B83 RID: 27523
		// (get) Token: 0x060143ED RID: 82925
		// (set) Token: 0x060143EC RID: 82924
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

		// Token: 0x060143EE RID: 82926
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17006B84 RID: 27524
		// (get) Token: 0x060143EF RID: 82927
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B85 RID: 27525
		// (get) Token: 0x060143F0 RID: 82928
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B86 RID: 27526
		// (get) Token: 0x060143F2 RID: 82930
		// (set) Token: 0x060143F1 RID: 82929
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

		// Token: 0x17006B87 RID: 27527
		// (get) Token: 0x060143F4 RID: 82932
		// (set) Token: 0x060143F3 RID: 82931
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

		// Token: 0x060143F5 RID: 82933
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x060143F6 RID: 82934
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17006B88 RID: 27528
		// (get) Token: 0x060143F8 RID: 82936
		// (set) Token: 0x060143F7 RID: 82935
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

		// Token: 0x060143F9 RID: 82937
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060143FA RID: 82938
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060143FB RID: 82939
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060143FC RID: 82940
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17006B89 RID: 27529
		// (get) Token: 0x060143FD RID: 82941
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060143FE RID: 82942
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060143FF RID: 82943
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17006B8A RID: 27530
		// (get) Token: 0x06014400 RID: 82944
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006B8B RID: 27531
		// (get) Token: 0x06014401 RID: 82945
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006B8C RID: 27532
		// (get) Token: 0x06014403 RID: 82947
		// (set) Token: 0x06014402 RID: 82946
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

		// Token: 0x17006B8D RID: 27533
		// (get) Token: 0x06014405 RID: 82949
		// (set) Token: 0x06014404 RID: 82948
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

		// Token: 0x17006B8E RID: 27534
		// (get) Token: 0x06014406 RID: 82950
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06014407 RID: 82951
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06014408 RID: 82952
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17006B8F RID: 27535
		// (get) Token: 0x06014409 RID: 82953
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B90 RID: 27536
		// (get) Token: 0x0601440A RID: 82954
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B91 RID: 27537
		// (get) Token: 0x0601440C RID: 82956
		// (set) Token: 0x0601440B RID: 82955
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

		// Token: 0x17006B92 RID: 27538
		// (get) Token: 0x0601440E RID: 82958
		// (set) Token: 0x0601440D RID: 82957
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

		// Token: 0x17006B93 RID: 27539
		// (get) Token: 0x06014410 RID: 82960
		// (set) Token: 0x0601440F RID: 82959
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

		// Token: 0x17006B94 RID: 27540
		// (get) Token: 0x06014412 RID: 82962
		// (set) Token: 0x06014411 RID: 82961
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

		// Token: 0x06014413 RID: 82963
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17006B95 RID: 27541
		// (get) Token: 0x06014415 RID: 82965
		// (set) Token: 0x06014414 RID: 82964
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

		// Token: 0x17006B96 RID: 27542
		// (get) Token: 0x06014416 RID: 82966
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B97 RID: 27543
		// (get) Token: 0x06014418 RID: 82968
		// (set) Token: 0x06014417 RID: 82967
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

		// Token: 0x17006B98 RID: 27544
		// (get) Token: 0x0601441A RID: 82970
		// (set) Token: 0x06014419 RID: 82969
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

		// Token: 0x17006B99 RID: 27545
		// (get) Token: 0x0601441B RID: 82971
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006B9A RID: 27546
		// (get) Token: 0x0601441D RID: 82973
		// (set) Token: 0x0601441C RID: 82972
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

		// Token: 0x17006B9B RID: 27547
		// (get) Token: 0x0601441F RID: 82975
		// (set) Token: 0x0601441E RID: 82974
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

		// Token: 0x06014420 RID: 82976
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17006B9C RID: 27548
		// (get) Token: 0x06014422 RID: 82978
		// (set) Token: 0x06014421 RID: 82977
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

		// Token: 0x17006B9D RID: 27549
		// (get) Token: 0x06014424 RID: 82980
		// (set) Token: 0x06014423 RID: 82979
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

		// Token: 0x17006B9E RID: 27550
		// (get) Token: 0x06014426 RID: 82982
		// (set) Token: 0x06014425 RID: 82981
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

		// Token: 0x17006B9F RID: 27551
		// (get) Token: 0x06014428 RID: 82984
		// (set) Token: 0x06014427 RID: 82983
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

		// Token: 0x17006BA0 RID: 27552
		// (get) Token: 0x0601442A RID: 82986
		// (set) Token: 0x06014429 RID: 82985
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

		// Token: 0x17006BA1 RID: 27553
		// (get) Token: 0x0601442C RID: 82988
		// (set) Token: 0x0601442B RID: 82987
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

		// Token: 0x17006BA2 RID: 27554
		// (get) Token: 0x0601442E RID: 82990
		// (set) Token: 0x0601442D RID: 82989
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

		// Token: 0x17006BA3 RID: 27555
		// (get) Token: 0x06014430 RID: 82992
		// (set) Token: 0x0601442F RID: 82991
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

		// Token: 0x06014431 RID: 82993
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17006BA4 RID: 27556
		// (get) Token: 0x06014432 RID: 82994
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006BA5 RID: 27557
		// (get) Token: 0x06014434 RID: 82996
		// (set) Token: 0x06014433 RID: 82995
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

		// Token: 0x06014435 RID: 82997
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06014436 RID: 82998
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06014437 RID: 82999
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06014438 RID: 83000
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17006BA6 RID: 27558
		// (get) Token: 0x0601443A RID: 83002
		// (set) Token: 0x06014439 RID: 83001
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

		// Token: 0x17006BA7 RID: 27559
		// (get) Token: 0x0601443C RID: 83004
		// (set) Token: 0x0601443B RID: 83003
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

		// Token: 0x17006BA8 RID: 27560
		// (get) Token: 0x0601443E RID: 83006
		// (set) Token: 0x0601443D RID: 83005
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

		// Token: 0x17006BA9 RID: 27561
		// (get) Token: 0x0601443F RID: 83007
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006BAA RID: 27562
		// (get) Token: 0x06014440 RID: 83008
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006BAB RID: 27563
		// (get) Token: 0x06014441 RID: 83009
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006BAC RID: 27564
		// (get) Token: 0x06014442 RID: 83010
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06014443 RID: 83011
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17006BAD RID: 27565
		// (get) Token: 0x06014444 RID: 83012
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006BAE RID: 27566
		// (get) Token: 0x06014445 RID: 83013
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06014446 RID: 83014
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06014447 RID: 83015
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06014448 RID: 83016
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06014449 RID: 83017
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0601444A RID: 83018
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0601444B RID: 83019
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0601444C RID: 83020
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0601444D RID: 83021
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17006BAF RID: 27567
		// (get) Token: 0x0601444E RID: 83022
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006BB0 RID: 27568
		// (get) Token: 0x06014450 RID: 83024
		// (set) Token: 0x0601444F RID: 83023
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

		// Token: 0x17006BB1 RID: 27569
		// (get) Token: 0x06014451 RID: 83025
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006BB2 RID: 27570
		// (get) Token: 0x06014452 RID: 83026
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006BB3 RID: 27571
		// (get) Token: 0x06014453 RID: 83027
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006BB4 RID: 27572
		// (get) Token: 0x06014454 RID: 83028
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006BB5 RID: 27573
		// (get) Token: 0x06014455 RID: 83029
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006BB6 RID: 27574
		// (get) Token: 0x06014457 RID: 83031
		// (set) Token: 0x06014456 RID: 83030
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

		// Token: 0x17006BB7 RID: 27575
		// (get) Token: 0x06014459 RID: 83033
		// (set) Token: 0x06014458 RID: 83032
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

		// Token: 0x17006BB8 RID: 27576
		// (get) Token: 0x0601445B RID: 83035
		// (set) Token: 0x0601445A RID: 83034
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

		// Token: 0x17006BB9 RID: 27577
		// (get) Token: 0x0601445D RID: 83037
		// (set) Token: 0x0601445C RID: 83036
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

		// Token: 0x0601445E RID: 83038
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17006BBA RID: 27578
		// (get) Token: 0x06014460 RID: 83040
		// (set) Token: 0x0601445F RID: 83039
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

		// Token: 0x17006BBB RID: 27579
		// (get) Token: 0x06014462 RID: 83042
		// (set) Token: 0x06014461 RID: 83041
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

		// Token: 0x17006BBC RID: 27580
		// (get) Token: 0x06014464 RID: 83044
		// (set) Token: 0x06014463 RID: 83043
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

		// Token: 0x17006BBD RID: 27581
		// (get) Token: 0x06014466 RID: 83046
		// (set) Token: 0x06014465 RID: 83045
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

		// Token: 0x06014467 RID: 83047
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06014468 RID: 83048
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06014469 RID: 83049
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17006BBE RID: 27582
		// (get) Token: 0x0601446A RID: 83050
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006BBF RID: 27583
		// (get) Token: 0x0601446B RID: 83051
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006BC0 RID: 27584
		// (get) Token: 0x0601446C RID: 83052
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006BC1 RID: 27585
		// (get) Token: 0x0601446D RID: 83053
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601446E RID: 83054
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTextContainer_createControlRange();

		// Token: 0x17006BC2 RID: 27586
		// (get) Token: 0x0601446F RID: 83055
		public virtual extern int IHTMLTextContainer_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006BC3 RID: 27587
		// (get) Token: 0x06014470 RID: 83056
		public virtual extern int IHTMLTextContainer_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006BC4 RID: 27588
		// (get) Token: 0x06014472 RID: 83058
		// (set) Token: 0x06014471 RID: 83057
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

		// Token: 0x17006BC5 RID: 27589
		// (get) Token: 0x06014474 RID: 83060
		// (set) Token: 0x06014473 RID: 83059
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

		// Token: 0x17006BC6 RID: 27590
		// (get) Token: 0x06014476 RID: 83062
		// (set) Token: 0x06014475 RID: 83061
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

		// Token: 0x17006BC7 RID: 27591
		// (get) Token: 0x06014478 RID: 83064
		// (set) Token: 0x06014477 RID: 83063
		public virtual extern string IHTMLDivPosition_align
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

		// Token: 0x14002706 RID: 9990
		// (add) Token: 0x06014479 RID: 83065
		// (remove) Token: 0x0601447A RID: 83066
		public virtual extern event HTMLTextContainerEvents_onhelpEventHandler HTMLTextContainerEvents_Event_onhelp;

		// Token: 0x14002707 RID: 9991
		// (add) Token: 0x0601447B RID: 83067
		// (remove) Token: 0x0601447C RID: 83068
		public virtual extern event HTMLTextContainerEvents_onclickEventHandler HTMLTextContainerEvents_Event_onclick;

		// Token: 0x14002708 RID: 9992
		// (add) Token: 0x0601447D RID: 83069
		// (remove) Token: 0x0601447E RID: 83070
		public virtual extern event HTMLTextContainerEvents_ondblclickEventHandler HTMLTextContainerEvents_Event_ondblclick;

		// Token: 0x14002709 RID: 9993
		// (add) Token: 0x0601447F RID: 83071
		// (remove) Token: 0x06014480 RID: 83072
		public virtual extern event HTMLTextContainerEvents_onkeypressEventHandler HTMLTextContainerEvents_Event_onkeypress;

		// Token: 0x1400270A RID: 9994
		// (add) Token: 0x06014481 RID: 83073
		// (remove) Token: 0x06014482 RID: 83074
		public virtual extern event HTMLTextContainerEvents_onkeydownEventHandler HTMLTextContainerEvents_Event_onkeydown;

		// Token: 0x1400270B RID: 9995
		// (add) Token: 0x06014483 RID: 83075
		// (remove) Token: 0x06014484 RID: 83076
		public virtual extern event HTMLTextContainerEvents_onkeyupEventHandler HTMLTextContainerEvents_Event_onkeyup;

		// Token: 0x1400270C RID: 9996
		// (add) Token: 0x06014485 RID: 83077
		// (remove) Token: 0x06014486 RID: 83078
		public virtual extern event HTMLTextContainerEvents_onmouseoutEventHandler HTMLTextContainerEvents_Event_onmouseout;

		// Token: 0x1400270D RID: 9997
		// (add) Token: 0x06014487 RID: 83079
		// (remove) Token: 0x06014488 RID: 83080
		public virtual extern event HTMLTextContainerEvents_onmouseoverEventHandler HTMLTextContainerEvents_Event_onmouseover;

		// Token: 0x1400270E RID: 9998
		// (add) Token: 0x06014489 RID: 83081
		// (remove) Token: 0x0601448A RID: 83082
		public virtual extern event HTMLTextContainerEvents_onmousemoveEventHandler HTMLTextContainerEvents_Event_onmousemove;

		// Token: 0x1400270F RID: 9999
		// (add) Token: 0x0601448B RID: 83083
		// (remove) Token: 0x0601448C RID: 83084
		public virtual extern event HTMLTextContainerEvents_onmousedownEventHandler HTMLTextContainerEvents_Event_onmousedown;

		// Token: 0x14002710 RID: 10000
		// (add) Token: 0x0601448D RID: 83085
		// (remove) Token: 0x0601448E RID: 83086
		public virtual extern event HTMLTextContainerEvents_onmouseupEventHandler HTMLTextContainerEvents_Event_onmouseup;

		// Token: 0x14002711 RID: 10001
		// (add) Token: 0x0601448F RID: 83087
		// (remove) Token: 0x06014490 RID: 83088
		public virtual extern event HTMLTextContainerEvents_onselectstartEventHandler HTMLTextContainerEvents_Event_onselectstart;

		// Token: 0x14002712 RID: 10002
		// (add) Token: 0x06014491 RID: 83089
		// (remove) Token: 0x06014492 RID: 83090
		public virtual extern event HTMLTextContainerEvents_onfilterchangeEventHandler HTMLTextContainerEvents_Event_onfilterchange;

		// Token: 0x14002713 RID: 10003
		// (add) Token: 0x06014493 RID: 83091
		// (remove) Token: 0x06014494 RID: 83092
		public virtual extern event HTMLTextContainerEvents_ondragstartEventHandler HTMLTextContainerEvents_Event_ondragstart;

		// Token: 0x14002714 RID: 10004
		// (add) Token: 0x06014495 RID: 83093
		// (remove) Token: 0x06014496 RID: 83094
		public virtual extern event HTMLTextContainerEvents_onbeforeupdateEventHandler HTMLTextContainerEvents_Event_onbeforeupdate;

		// Token: 0x14002715 RID: 10005
		// (add) Token: 0x06014497 RID: 83095
		// (remove) Token: 0x06014498 RID: 83096
		public virtual extern event HTMLTextContainerEvents_onafterupdateEventHandler HTMLTextContainerEvents_Event_onafterupdate;

		// Token: 0x14002716 RID: 10006
		// (add) Token: 0x06014499 RID: 83097
		// (remove) Token: 0x0601449A RID: 83098
		public virtual extern event HTMLTextContainerEvents_onerrorupdateEventHandler HTMLTextContainerEvents_Event_onerrorupdate;

		// Token: 0x14002717 RID: 10007
		// (add) Token: 0x0601449B RID: 83099
		// (remove) Token: 0x0601449C RID: 83100
		public virtual extern event HTMLTextContainerEvents_onrowexitEventHandler HTMLTextContainerEvents_Event_onrowexit;

		// Token: 0x14002718 RID: 10008
		// (add) Token: 0x0601449D RID: 83101
		// (remove) Token: 0x0601449E RID: 83102
		public virtual extern event HTMLTextContainerEvents_onrowenterEventHandler HTMLTextContainerEvents_Event_onrowenter;

		// Token: 0x14002719 RID: 10009
		// (add) Token: 0x0601449F RID: 83103
		// (remove) Token: 0x060144A0 RID: 83104
		public virtual extern event HTMLTextContainerEvents_ondatasetchangedEventHandler HTMLTextContainerEvents_Event_ondatasetchanged;

		// Token: 0x1400271A RID: 10010
		// (add) Token: 0x060144A1 RID: 83105
		// (remove) Token: 0x060144A2 RID: 83106
		public virtual extern event HTMLTextContainerEvents_ondataavailableEventHandler HTMLTextContainerEvents_Event_ondataavailable;

		// Token: 0x1400271B RID: 10011
		// (add) Token: 0x060144A3 RID: 83107
		// (remove) Token: 0x060144A4 RID: 83108
		public virtual extern event HTMLTextContainerEvents_ondatasetcompleteEventHandler HTMLTextContainerEvents_Event_ondatasetcomplete;

		// Token: 0x1400271C RID: 10012
		// (add) Token: 0x060144A5 RID: 83109
		// (remove) Token: 0x060144A6 RID: 83110
		public virtual extern event HTMLTextContainerEvents_onlosecaptureEventHandler HTMLTextContainerEvents_Event_onlosecapture;

		// Token: 0x1400271D RID: 10013
		// (add) Token: 0x060144A7 RID: 83111
		// (remove) Token: 0x060144A8 RID: 83112
		public virtual extern event HTMLTextContainerEvents_onpropertychangeEventHandler HTMLTextContainerEvents_Event_onpropertychange;

		// Token: 0x1400271E RID: 10014
		// (add) Token: 0x060144A9 RID: 83113
		// (remove) Token: 0x060144AA RID: 83114
		public virtual extern event HTMLTextContainerEvents_onscrollEventHandler HTMLTextContainerEvents_Event_onscroll;

		// Token: 0x1400271F RID: 10015
		// (add) Token: 0x060144AB RID: 83115
		// (remove) Token: 0x060144AC RID: 83116
		public virtual extern event HTMLTextContainerEvents_onfocusEventHandler HTMLTextContainerEvents_Event_onfocus;

		// Token: 0x14002720 RID: 10016
		// (add) Token: 0x060144AD RID: 83117
		// (remove) Token: 0x060144AE RID: 83118
		public virtual extern event HTMLTextContainerEvents_onblurEventHandler HTMLTextContainerEvents_Event_onblur;

		// Token: 0x14002721 RID: 10017
		// (add) Token: 0x060144AF RID: 83119
		// (remove) Token: 0x060144B0 RID: 83120
		public virtual extern event HTMLTextContainerEvents_onresizeEventHandler HTMLTextContainerEvents_Event_onresize;

		// Token: 0x14002722 RID: 10018
		// (add) Token: 0x060144B1 RID: 83121
		// (remove) Token: 0x060144B2 RID: 83122
		public virtual extern event HTMLTextContainerEvents_ondragEventHandler HTMLTextContainerEvents_Event_ondrag;

		// Token: 0x14002723 RID: 10019
		// (add) Token: 0x060144B3 RID: 83123
		// (remove) Token: 0x060144B4 RID: 83124
		public virtual extern event HTMLTextContainerEvents_ondragendEventHandler HTMLTextContainerEvents_Event_ondragend;

		// Token: 0x14002724 RID: 10020
		// (add) Token: 0x060144B5 RID: 83125
		// (remove) Token: 0x060144B6 RID: 83126
		public virtual extern event HTMLTextContainerEvents_ondragenterEventHandler HTMLTextContainerEvents_Event_ondragenter;

		// Token: 0x14002725 RID: 10021
		// (add) Token: 0x060144B7 RID: 83127
		// (remove) Token: 0x060144B8 RID: 83128
		public virtual extern event HTMLTextContainerEvents_ondragoverEventHandler HTMLTextContainerEvents_Event_ondragover;

		// Token: 0x14002726 RID: 10022
		// (add) Token: 0x060144B9 RID: 83129
		// (remove) Token: 0x060144BA RID: 83130
		public virtual extern event HTMLTextContainerEvents_ondragleaveEventHandler HTMLTextContainerEvents_Event_ondragleave;

		// Token: 0x14002727 RID: 10023
		// (add) Token: 0x060144BB RID: 83131
		// (remove) Token: 0x060144BC RID: 83132
		public virtual extern event HTMLTextContainerEvents_ondropEventHandler HTMLTextContainerEvents_Event_ondrop;

		// Token: 0x14002728 RID: 10024
		// (add) Token: 0x060144BD RID: 83133
		// (remove) Token: 0x060144BE RID: 83134
		public virtual extern event HTMLTextContainerEvents_onbeforecutEventHandler HTMLTextContainerEvents_Event_onbeforecut;

		// Token: 0x14002729 RID: 10025
		// (add) Token: 0x060144BF RID: 83135
		// (remove) Token: 0x060144C0 RID: 83136
		public virtual extern event HTMLTextContainerEvents_oncutEventHandler HTMLTextContainerEvents_Event_oncut;

		// Token: 0x1400272A RID: 10026
		// (add) Token: 0x060144C1 RID: 83137
		// (remove) Token: 0x060144C2 RID: 83138
		public virtual extern event HTMLTextContainerEvents_onbeforecopyEventHandler HTMLTextContainerEvents_Event_onbeforecopy;

		// Token: 0x1400272B RID: 10027
		// (add) Token: 0x060144C3 RID: 83139
		// (remove) Token: 0x060144C4 RID: 83140
		public virtual extern event HTMLTextContainerEvents_oncopyEventHandler HTMLTextContainerEvents_Event_oncopy;

		// Token: 0x1400272C RID: 10028
		// (add) Token: 0x060144C5 RID: 83141
		// (remove) Token: 0x060144C6 RID: 83142
		public virtual extern event HTMLTextContainerEvents_onbeforepasteEventHandler HTMLTextContainerEvents_Event_onbeforepaste;

		// Token: 0x1400272D RID: 10029
		// (add) Token: 0x060144C7 RID: 83143
		// (remove) Token: 0x060144C8 RID: 83144
		public virtual extern event HTMLTextContainerEvents_onpasteEventHandler HTMLTextContainerEvents_Event_onpaste;

		// Token: 0x1400272E RID: 10030
		// (add) Token: 0x060144C9 RID: 83145
		// (remove) Token: 0x060144CA RID: 83146
		public virtual extern event HTMLTextContainerEvents_oncontextmenuEventHandler HTMLTextContainerEvents_Event_oncontextmenu;

		// Token: 0x1400272F RID: 10031
		// (add) Token: 0x060144CB RID: 83147
		// (remove) Token: 0x060144CC RID: 83148
		public virtual extern event HTMLTextContainerEvents_onrowsdeleteEventHandler HTMLTextContainerEvents_Event_onrowsdelete;

		// Token: 0x14002730 RID: 10032
		// (add) Token: 0x060144CD RID: 83149
		// (remove) Token: 0x060144CE RID: 83150
		public virtual extern event HTMLTextContainerEvents_onrowsinsertedEventHandler HTMLTextContainerEvents_Event_onrowsinserted;

		// Token: 0x14002731 RID: 10033
		// (add) Token: 0x060144CF RID: 83151
		// (remove) Token: 0x060144D0 RID: 83152
		public virtual extern event HTMLTextContainerEvents_oncellchangeEventHandler HTMLTextContainerEvents_Event_oncellchange;

		// Token: 0x14002732 RID: 10034
		// (add) Token: 0x060144D1 RID: 83153
		// (remove) Token: 0x060144D2 RID: 83154
		public virtual extern event HTMLTextContainerEvents_onreadystatechangeEventHandler HTMLTextContainerEvents_Event_onreadystatechange;

		// Token: 0x14002733 RID: 10035
		// (add) Token: 0x060144D3 RID: 83155
		// (remove) Token: 0x060144D4 RID: 83156
		public virtual extern event HTMLTextContainerEvents_onbeforeeditfocusEventHandler HTMLTextContainerEvents_Event_onbeforeeditfocus;

		// Token: 0x14002734 RID: 10036
		// (add) Token: 0x060144D5 RID: 83157
		// (remove) Token: 0x060144D6 RID: 83158
		public virtual extern event HTMLTextContainerEvents_onlayoutcompleteEventHandler HTMLTextContainerEvents_Event_onlayoutcomplete;

		// Token: 0x14002735 RID: 10037
		// (add) Token: 0x060144D7 RID: 83159
		// (remove) Token: 0x060144D8 RID: 83160
		public virtual extern event HTMLTextContainerEvents_onpageEventHandler HTMLTextContainerEvents_Event_onpage;

		// Token: 0x14002736 RID: 10038
		// (add) Token: 0x060144D9 RID: 83161
		// (remove) Token: 0x060144DA RID: 83162
		public virtual extern event HTMLTextContainerEvents_onbeforedeactivateEventHandler HTMLTextContainerEvents_Event_onbeforedeactivate;

		// Token: 0x14002737 RID: 10039
		// (add) Token: 0x060144DB RID: 83163
		// (remove) Token: 0x060144DC RID: 83164
		public virtual extern event HTMLTextContainerEvents_onbeforeactivateEventHandler HTMLTextContainerEvents_Event_onbeforeactivate;

		// Token: 0x14002738 RID: 10040
		// (add) Token: 0x060144DD RID: 83165
		// (remove) Token: 0x060144DE RID: 83166
		public virtual extern event HTMLTextContainerEvents_onmoveEventHandler HTMLTextContainerEvents_Event_onmove;

		// Token: 0x14002739 RID: 10041
		// (add) Token: 0x060144DF RID: 83167
		// (remove) Token: 0x060144E0 RID: 83168
		public virtual extern event HTMLTextContainerEvents_oncontrolselectEventHandler HTMLTextContainerEvents_Event_oncontrolselect;

		// Token: 0x1400273A RID: 10042
		// (add) Token: 0x060144E1 RID: 83169
		// (remove) Token: 0x060144E2 RID: 83170
		public virtual extern event HTMLTextContainerEvents_onmovestartEventHandler HTMLTextContainerEvents_Event_onmovestart;

		// Token: 0x1400273B RID: 10043
		// (add) Token: 0x060144E3 RID: 83171
		// (remove) Token: 0x060144E4 RID: 83172
		public virtual extern event HTMLTextContainerEvents_onmoveendEventHandler HTMLTextContainerEvents_Event_onmoveend;

		// Token: 0x1400273C RID: 10044
		// (add) Token: 0x060144E5 RID: 83173
		// (remove) Token: 0x060144E6 RID: 83174
		public virtual extern event HTMLTextContainerEvents_onresizestartEventHandler HTMLTextContainerEvents_Event_onresizestart;

		// Token: 0x1400273D RID: 10045
		// (add) Token: 0x060144E7 RID: 83175
		// (remove) Token: 0x060144E8 RID: 83176
		public virtual extern event HTMLTextContainerEvents_onresizeendEventHandler HTMLTextContainerEvents_Event_onresizeend;

		// Token: 0x1400273E RID: 10046
		// (add) Token: 0x060144E9 RID: 83177
		// (remove) Token: 0x060144EA RID: 83178
		public virtual extern event HTMLTextContainerEvents_onmouseenterEventHandler HTMLTextContainerEvents_Event_onmouseenter;

		// Token: 0x1400273F RID: 10047
		// (add) Token: 0x060144EB RID: 83179
		// (remove) Token: 0x060144EC RID: 83180
		public virtual extern event HTMLTextContainerEvents_onmouseleaveEventHandler HTMLTextContainerEvents_Event_onmouseleave;

		// Token: 0x14002740 RID: 10048
		// (add) Token: 0x060144ED RID: 83181
		// (remove) Token: 0x060144EE RID: 83182
		public virtual extern event HTMLTextContainerEvents_onmousewheelEventHandler HTMLTextContainerEvents_Event_onmousewheel;

		// Token: 0x14002741 RID: 10049
		// (add) Token: 0x060144EF RID: 83183
		// (remove) Token: 0x060144F0 RID: 83184
		public virtual extern event HTMLTextContainerEvents_onactivateEventHandler HTMLTextContainerEvents_Event_onactivate;

		// Token: 0x14002742 RID: 10050
		// (add) Token: 0x060144F1 RID: 83185
		// (remove) Token: 0x060144F2 RID: 83186
		public virtual extern event HTMLTextContainerEvents_ondeactivateEventHandler HTMLTextContainerEvents_Event_ondeactivate;

		// Token: 0x14002743 RID: 10051
		// (add) Token: 0x060144F3 RID: 83187
		// (remove) Token: 0x060144F4 RID: 83188
		public virtual extern event HTMLTextContainerEvents_onfocusinEventHandler HTMLTextContainerEvents_Event_onfocusin;

		// Token: 0x14002744 RID: 10052
		// (add) Token: 0x060144F5 RID: 83189
		// (remove) Token: 0x060144F6 RID: 83190
		public virtual extern event HTMLTextContainerEvents_onfocusoutEventHandler HTMLTextContainerEvents_Event_onfocusout;

		// Token: 0x14002745 RID: 10053
		// (add) Token: 0x060144F7 RID: 83191
		// (remove) Token: 0x060144F8 RID: 83192
		public virtual extern event HTMLTextContainerEvents_onchangeEventHandler onchange;

		// Token: 0x14002746 RID: 10054
		// (add) Token: 0x060144F9 RID: 83193
		// (remove) Token: 0x060144FA RID: 83194
		public virtual extern event HTMLTextContainerEvents_onselectEventHandler onselect;

		// Token: 0x14002747 RID: 10055
		// (add) Token: 0x060144FB RID: 83195
		// (remove) Token: 0x060144FC RID: 83196
		public virtual extern event HTMLTextContainerEvents2_onhelpEventHandler HTMLTextContainerEvents2_Event_onhelp;

		// Token: 0x14002748 RID: 10056
		// (add) Token: 0x060144FD RID: 83197
		// (remove) Token: 0x060144FE RID: 83198
		public virtual extern event HTMLTextContainerEvents2_onclickEventHandler HTMLTextContainerEvents2_Event_onclick;

		// Token: 0x14002749 RID: 10057
		// (add) Token: 0x060144FF RID: 83199
		// (remove) Token: 0x06014500 RID: 83200
		public virtual extern event HTMLTextContainerEvents2_ondblclickEventHandler HTMLTextContainerEvents2_Event_ondblclick;

		// Token: 0x1400274A RID: 10058
		// (add) Token: 0x06014501 RID: 83201
		// (remove) Token: 0x06014502 RID: 83202
		public virtual extern event HTMLTextContainerEvents2_onkeypressEventHandler HTMLTextContainerEvents2_Event_onkeypress;

		// Token: 0x1400274B RID: 10059
		// (add) Token: 0x06014503 RID: 83203
		// (remove) Token: 0x06014504 RID: 83204
		public virtual extern event HTMLTextContainerEvents2_onkeydownEventHandler HTMLTextContainerEvents2_Event_onkeydown;

		// Token: 0x1400274C RID: 10060
		// (add) Token: 0x06014505 RID: 83205
		// (remove) Token: 0x06014506 RID: 83206
		public virtual extern event HTMLTextContainerEvents2_onkeyupEventHandler HTMLTextContainerEvents2_Event_onkeyup;

		// Token: 0x1400274D RID: 10061
		// (add) Token: 0x06014507 RID: 83207
		// (remove) Token: 0x06014508 RID: 83208
		public virtual extern event HTMLTextContainerEvents2_onmouseoutEventHandler HTMLTextContainerEvents2_Event_onmouseout;

		// Token: 0x1400274E RID: 10062
		// (add) Token: 0x06014509 RID: 83209
		// (remove) Token: 0x0601450A RID: 83210
		public virtual extern event HTMLTextContainerEvents2_onmouseoverEventHandler HTMLTextContainerEvents2_Event_onmouseover;

		// Token: 0x1400274F RID: 10063
		// (add) Token: 0x0601450B RID: 83211
		// (remove) Token: 0x0601450C RID: 83212
		public virtual extern event HTMLTextContainerEvents2_onmousemoveEventHandler HTMLTextContainerEvents2_Event_onmousemove;

		// Token: 0x14002750 RID: 10064
		// (add) Token: 0x0601450D RID: 83213
		// (remove) Token: 0x0601450E RID: 83214
		public virtual extern event HTMLTextContainerEvents2_onmousedownEventHandler HTMLTextContainerEvents2_Event_onmousedown;

		// Token: 0x14002751 RID: 10065
		// (add) Token: 0x0601450F RID: 83215
		// (remove) Token: 0x06014510 RID: 83216
		public virtual extern event HTMLTextContainerEvents2_onmouseupEventHandler HTMLTextContainerEvents2_Event_onmouseup;

		// Token: 0x14002752 RID: 10066
		// (add) Token: 0x06014511 RID: 83217
		// (remove) Token: 0x06014512 RID: 83218
		public virtual extern event HTMLTextContainerEvents2_onselectstartEventHandler HTMLTextContainerEvents2_Event_onselectstart;

		// Token: 0x14002753 RID: 10067
		// (add) Token: 0x06014513 RID: 83219
		// (remove) Token: 0x06014514 RID: 83220
		public virtual extern event HTMLTextContainerEvents2_onfilterchangeEventHandler HTMLTextContainerEvents2_Event_onfilterchange;

		// Token: 0x14002754 RID: 10068
		// (add) Token: 0x06014515 RID: 83221
		// (remove) Token: 0x06014516 RID: 83222
		public virtual extern event HTMLTextContainerEvents2_ondragstartEventHandler HTMLTextContainerEvents2_Event_ondragstart;

		// Token: 0x14002755 RID: 10069
		// (add) Token: 0x06014517 RID: 83223
		// (remove) Token: 0x06014518 RID: 83224
		public virtual extern event HTMLTextContainerEvents2_onbeforeupdateEventHandler HTMLTextContainerEvents2_Event_onbeforeupdate;

		// Token: 0x14002756 RID: 10070
		// (add) Token: 0x06014519 RID: 83225
		// (remove) Token: 0x0601451A RID: 83226
		public virtual extern event HTMLTextContainerEvents2_onafterupdateEventHandler HTMLTextContainerEvents2_Event_onafterupdate;

		// Token: 0x14002757 RID: 10071
		// (add) Token: 0x0601451B RID: 83227
		// (remove) Token: 0x0601451C RID: 83228
		public virtual extern event HTMLTextContainerEvents2_onerrorupdateEventHandler HTMLTextContainerEvents2_Event_onerrorupdate;

		// Token: 0x14002758 RID: 10072
		// (add) Token: 0x0601451D RID: 83229
		// (remove) Token: 0x0601451E RID: 83230
		public virtual extern event HTMLTextContainerEvents2_onrowexitEventHandler HTMLTextContainerEvents2_Event_onrowexit;

		// Token: 0x14002759 RID: 10073
		// (add) Token: 0x0601451F RID: 83231
		// (remove) Token: 0x06014520 RID: 83232
		public virtual extern event HTMLTextContainerEvents2_onrowenterEventHandler HTMLTextContainerEvents2_Event_onrowenter;

		// Token: 0x1400275A RID: 10074
		// (add) Token: 0x06014521 RID: 83233
		// (remove) Token: 0x06014522 RID: 83234
		public virtual extern event HTMLTextContainerEvents2_ondatasetchangedEventHandler HTMLTextContainerEvents2_Event_ondatasetchanged;

		// Token: 0x1400275B RID: 10075
		// (add) Token: 0x06014523 RID: 83235
		// (remove) Token: 0x06014524 RID: 83236
		public virtual extern event HTMLTextContainerEvents2_ondataavailableEventHandler HTMLTextContainerEvents2_Event_ondataavailable;

		// Token: 0x1400275C RID: 10076
		// (add) Token: 0x06014525 RID: 83237
		// (remove) Token: 0x06014526 RID: 83238
		public virtual extern event HTMLTextContainerEvents2_ondatasetcompleteEventHandler HTMLTextContainerEvents2_Event_ondatasetcomplete;

		// Token: 0x1400275D RID: 10077
		// (add) Token: 0x06014527 RID: 83239
		// (remove) Token: 0x06014528 RID: 83240
		public virtual extern event HTMLTextContainerEvents2_onlosecaptureEventHandler HTMLTextContainerEvents2_Event_onlosecapture;

		// Token: 0x1400275E RID: 10078
		// (add) Token: 0x06014529 RID: 83241
		// (remove) Token: 0x0601452A RID: 83242
		public virtual extern event HTMLTextContainerEvents2_onpropertychangeEventHandler HTMLTextContainerEvents2_Event_onpropertychange;

		// Token: 0x1400275F RID: 10079
		// (add) Token: 0x0601452B RID: 83243
		// (remove) Token: 0x0601452C RID: 83244
		public virtual extern event HTMLTextContainerEvents2_onscrollEventHandler HTMLTextContainerEvents2_Event_onscroll;

		// Token: 0x14002760 RID: 10080
		// (add) Token: 0x0601452D RID: 83245
		// (remove) Token: 0x0601452E RID: 83246
		public virtual extern event HTMLTextContainerEvents2_onfocusEventHandler HTMLTextContainerEvents2_Event_onfocus;

		// Token: 0x14002761 RID: 10081
		// (add) Token: 0x0601452F RID: 83247
		// (remove) Token: 0x06014530 RID: 83248
		public virtual extern event HTMLTextContainerEvents2_onblurEventHandler HTMLTextContainerEvents2_Event_onblur;

		// Token: 0x14002762 RID: 10082
		// (add) Token: 0x06014531 RID: 83249
		// (remove) Token: 0x06014532 RID: 83250
		public virtual extern event HTMLTextContainerEvents2_onresizeEventHandler HTMLTextContainerEvents2_Event_onresize;

		// Token: 0x14002763 RID: 10083
		// (add) Token: 0x06014533 RID: 83251
		// (remove) Token: 0x06014534 RID: 83252
		public virtual extern event HTMLTextContainerEvents2_ondragEventHandler HTMLTextContainerEvents2_Event_ondrag;

		// Token: 0x14002764 RID: 10084
		// (add) Token: 0x06014535 RID: 83253
		// (remove) Token: 0x06014536 RID: 83254
		public virtual extern event HTMLTextContainerEvents2_ondragendEventHandler HTMLTextContainerEvents2_Event_ondragend;

		// Token: 0x14002765 RID: 10085
		// (add) Token: 0x06014537 RID: 83255
		// (remove) Token: 0x06014538 RID: 83256
		public virtual extern event HTMLTextContainerEvents2_ondragenterEventHandler HTMLTextContainerEvents2_Event_ondragenter;

		// Token: 0x14002766 RID: 10086
		// (add) Token: 0x06014539 RID: 83257
		// (remove) Token: 0x0601453A RID: 83258
		public virtual extern event HTMLTextContainerEvents2_ondragoverEventHandler HTMLTextContainerEvents2_Event_ondragover;

		// Token: 0x14002767 RID: 10087
		// (add) Token: 0x0601453B RID: 83259
		// (remove) Token: 0x0601453C RID: 83260
		public virtual extern event HTMLTextContainerEvents2_ondragleaveEventHandler HTMLTextContainerEvents2_Event_ondragleave;

		// Token: 0x14002768 RID: 10088
		// (add) Token: 0x0601453D RID: 83261
		// (remove) Token: 0x0601453E RID: 83262
		public virtual extern event HTMLTextContainerEvents2_ondropEventHandler HTMLTextContainerEvents2_Event_ondrop;

		// Token: 0x14002769 RID: 10089
		// (add) Token: 0x0601453F RID: 83263
		// (remove) Token: 0x06014540 RID: 83264
		public virtual extern event HTMLTextContainerEvents2_onbeforecutEventHandler HTMLTextContainerEvents2_Event_onbeforecut;

		// Token: 0x1400276A RID: 10090
		// (add) Token: 0x06014541 RID: 83265
		// (remove) Token: 0x06014542 RID: 83266
		public virtual extern event HTMLTextContainerEvents2_oncutEventHandler HTMLTextContainerEvents2_Event_oncut;

		// Token: 0x1400276B RID: 10091
		// (add) Token: 0x06014543 RID: 83267
		// (remove) Token: 0x06014544 RID: 83268
		public virtual extern event HTMLTextContainerEvents2_onbeforecopyEventHandler HTMLTextContainerEvents2_Event_onbeforecopy;

		// Token: 0x1400276C RID: 10092
		// (add) Token: 0x06014545 RID: 83269
		// (remove) Token: 0x06014546 RID: 83270
		public virtual extern event HTMLTextContainerEvents2_oncopyEventHandler HTMLTextContainerEvents2_Event_oncopy;

		// Token: 0x1400276D RID: 10093
		// (add) Token: 0x06014547 RID: 83271
		// (remove) Token: 0x06014548 RID: 83272
		public virtual extern event HTMLTextContainerEvents2_onbeforepasteEventHandler HTMLTextContainerEvents2_Event_onbeforepaste;

		// Token: 0x1400276E RID: 10094
		// (add) Token: 0x06014549 RID: 83273
		// (remove) Token: 0x0601454A RID: 83274
		public virtual extern event HTMLTextContainerEvents2_onpasteEventHandler HTMLTextContainerEvents2_Event_onpaste;

		// Token: 0x1400276F RID: 10095
		// (add) Token: 0x0601454B RID: 83275
		// (remove) Token: 0x0601454C RID: 83276
		public virtual extern event HTMLTextContainerEvents2_oncontextmenuEventHandler HTMLTextContainerEvents2_Event_oncontextmenu;

		// Token: 0x14002770 RID: 10096
		// (add) Token: 0x0601454D RID: 83277
		// (remove) Token: 0x0601454E RID: 83278
		public virtual extern event HTMLTextContainerEvents2_onrowsdeleteEventHandler HTMLTextContainerEvents2_Event_onrowsdelete;

		// Token: 0x14002771 RID: 10097
		// (add) Token: 0x0601454F RID: 83279
		// (remove) Token: 0x06014550 RID: 83280
		public virtual extern event HTMLTextContainerEvents2_onrowsinsertedEventHandler HTMLTextContainerEvents2_Event_onrowsinserted;

		// Token: 0x14002772 RID: 10098
		// (add) Token: 0x06014551 RID: 83281
		// (remove) Token: 0x06014552 RID: 83282
		public virtual extern event HTMLTextContainerEvents2_oncellchangeEventHandler HTMLTextContainerEvents2_Event_oncellchange;

		// Token: 0x14002773 RID: 10099
		// (add) Token: 0x06014553 RID: 83283
		// (remove) Token: 0x06014554 RID: 83284
		public virtual extern event HTMLTextContainerEvents2_onreadystatechangeEventHandler HTMLTextContainerEvents2_Event_onreadystatechange;

		// Token: 0x14002774 RID: 10100
		// (add) Token: 0x06014555 RID: 83285
		// (remove) Token: 0x06014556 RID: 83286
		public virtual extern event HTMLTextContainerEvents2_onlayoutcompleteEventHandler HTMLTextContainerEvents2_Event_onlayoutcomplete;

		// Token: 0x14002775 RID: 10101
		// (add) Token: 0x06014557 RID: 83287
		// (remove) Token: 0x06014558 RID: 83288
		public virtual extern event HTMLTextContainerEvents2_onpageEventHandler HTMLTextContainerEvents2_Event_onpage;

		// Token: 0x14002776 RID: 10102
		// (add) Token: 0x06014559 RID: 83289
		// (remove) Token: 0x0601455A RID: 83290
		public virtual extern event HTMLTextContainerEvents2_onmouseenterEventHandler HTMLTextContainerEvents2_Event_onmouseenter;

		// Token: 0x14002777 RID: 10103
		// (add) Token: 0x0601455B RID: 83291
		// (remove) Token: 0x0601455C RID: 83292
		public virtual extern event HTMLTextContainerEvents2_onmouseleaveEventHandler HTMLTextContainerEvents2_Event_onmouseleave;

		// Token: 0x14002778 RID: 10104
		// (add) Token: 0x0601455D RID: 83293
		// (remove) Token: 0x0601455E RID: 83294
		public virtual extern event HTMLTextContainerEvents2_onactivateEventHandler HTMLTextContainerEvents2_Event_onactivate;

		// Token: 0x14002779 RID: 10105
		// (add) Token: 0x0601455F RID: 83295
		// (remove) Token: 0x06014560 RID: 83296
		public virtual extern event HTMLTextContainerEvents2_ondeactivateEventHandler HTMLTextContainerEvents2_Event_ondeactivate;

		// Token: 0x1400277A RID: 10106
		// (add) Token: 0x06014561 RID: 83297
		// (remove) Token: 0x06014562 RID: 83298
		public virtual extern event HTMLTextContainerEvents2_onbeforedeactivateEventHandler HTMLTextContainerEvents2_Event_onbeforedeactivate;

		// Token: 0x1400277B RID: 10107
		// (add) Token: 0x06014563 RID: 83299
		// (remove) Token: 0x06014564 RID: 83300
		public virtual extern event HTMLTextContainerEvents2_onbeforeactivateEventHandler HTMLTextContainerEvents2_Event_onbeforeactivate;

		// Token: 0x1400277C RID: 10108
		// (add) Token: 0x06014565 RID: 83301
		// (remove) Token: 0x06014566 RID: 83302
		public virtual extern event HTMLTextContainerEvents2_onfocusinEventHandler HTMLTextContainerEvents2_Event_onfocusin;

		// Token: 0x1400277D RID: 10109
		// (add) Token: 0x06014567 RID: 83303
		// (remove) Token: 0x06014568 RID: 83304
		public virtual extern event HTMLTextContainerEvents2_onfocusoutEventHandler HTMLTextContainerEvents2_Event_onfocusout;

		// Token: 0x1400277E RID: 10110
		// (add) Token: 0x06014569 RID: 83305
		// (remove) Token: 0x0601456A RID: 83306
		public virtual extern event HTMLTextContainerEvents2_onmoveEventHandler HTMLTextContainerEvents2_Event_onmove;

		// Token: 0x1400277F RID: 10111
		// (add) Token: 0x0601456B RID: 83307
		// (remove) Token: 0x0601456C RID: 83308
		public virtual extern event HTMLTextContainerEvents2_oncontrolselectEventHandler HTMLTextContainerEvents2_Event_oncontrolselect;

		// Token: 0x14002780 RID: 10112
		// (add) Token: 0x0601456D RID: 83309
		// (remove) Token: 0x0601456E RID: 83310
		public virtual extern event HTMLTextContainerEvents2_onmovestartEventHandler HTMLTextContainerEvents2_Event_onmovestart;

		// Token: 0x14002781 RID: 10113
		// (add) Token: 0x0601456F RID: 83311
		// (remove) Token: 0x06014570 RID: 83312
		public virtual extern event HTMLTextContainerEvents2_onmoveendEventHandler HTMLTextContainerEvents2_Event_onmoveend;

		// Token: 0x14002782 RID: 10114
		// (add) Token: 0x06014571 RID: 83313
		// (remove) Token: 0x06014572 RID: 83314
		public virtual extern event HTMLTextContainerEvents2_onresizestartEventHandler HTMLTextContainerEvents2_Event_onresizestart;

		// Token: 0x14002783 RID: 10115
		// (add) Token: 0x06014573 RID: 83315
		// (remove) Token: 0x06014574 RID: 83316
		public virtual extern event HTMLTextContainerEvents2_onresizeendEventHandler HTMLTextContainerEvents2_Event_onresizeend;

		// Token: 0x14002784 RID: 10116
		// (add) Token: 0x06014575 RID: 83317
		// (remove) Token: 0x06014576 RID: 83318
		public virtual extern event HTMLTextContainerEvents2_onmousewheelEventHandler HTMLTextContainerEvents2_Event_onmousewheel;

		// Token: 0x14002785 RID: 10117
		// (add) Token: 0x06014577 RID: 83319
		// (remove) Token: 0x06014578 RID: 83320
		public virtual extern event HTMLTextContainerEvents2_onchangeEventHandler HTMLTextContainerEvents2_Event_onchange;

		// Token: 0x14002786 RID: 10118
		// (add) Token: 0x06014579 RID: 83321
		// (remove) Token: 0x0601457A RID: 83322
		public virtual extern event HTMLTextContainerEvents2_onselectEventHandler HTMLTextContainerEvents2_Event_onselect;
	}
}
