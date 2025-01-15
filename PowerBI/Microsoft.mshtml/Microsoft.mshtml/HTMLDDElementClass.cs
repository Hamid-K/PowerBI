using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004BD RID: 1213
	[ComSourceInterfaces("mshtml.HTMLElementEvents\0mshtml.HTMLElementEvents2\0\0")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F27F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLDDElementClass : DispHTMLDDElement, HTMLDDElement, HTMLElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDDElement, HTMLElementEvents2_Event
	{
		// Token: 0x0600630D RID: 25357
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLDDElementClass();

		// Token: 0x0600630E RID: 25358
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600630F RID: 25359
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06006310 RID: 25360
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x06006312 RID: 25362
		// (set) Token: 0x06006311 RID: 25361
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

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x06006314 RID: 25364
		// (set) Token: 0x06006313 RID: 25363
		[DispId(-2147417110)]
		public virtual extern string id
		{
			[TypeLibFunc(4)]
			[DispId(-2147417110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x06006315 RID: 25365
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x06006316 RID: 25366
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x06006317 RID: 25367
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x06006319 RID: 25369
		// (set) Token: 0x06006318 RID: 25368
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

		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x0600631B RID: 25371
		// (set) Token: 0x0600631A RID: 25370
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

		// Token: 0x170020F8 RID: 8440
		// (get) Token: 0x0600631D RID: 25373
		// (set) Token: 0x0600631C RID: 25372
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

		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x0600631F RID: 25375
		// (set) Token: 0x0600631E RID: 25374
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

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x06006321 RID: 25377
		// (set) Token: 0x06006320 RID: 25376
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

		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x06006323 RID: 25379
		// (set) Token: 0x06006322 RID: 25378
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

		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x06006325 RID: 25381
		// (set) Token: 0x06006324 RID: 25380
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

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x06006327 RID: 25383
		// (set) Token: 0x06006326 RID: 25382
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

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x06006329 RID: 25385
		// (set) Token: 0x06006328 RID: 25384
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

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x0600632B RID: 25387
		// (set) Token: 0x0600632A RID: 25386
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

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x0600632D RID: 25389
		// (set) Token: 0x0600632C RID: 25388
		[DispId(-2147412109)]
		public virtual extern object onmouseup
		{
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x0600632E RID: 25390
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x06006330 RID: 25392
		// (set) Token: 0x0600632F RID: 25391
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

		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x06006332 RID: 25394
		// (set) Token: 0x06006331 RID: 25393
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002104 RID: 8452
		// (get) Token: 0x06006334 RID: 25396
		// (set) Token: 0x06006333 RID: 25395
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

		// Token: 0x06006335 RID: 25397
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06006336 RID: 25398
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x06006337 RID: 25399
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x06006338 RID: 25400
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x0600633A RID: 25402
		// (set) Token: 0x06006339 RID: 25401
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

		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x0600633B RID: 25403
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002109 RID: 8457
		// (get) Token: 0x0600633C RID: 25404
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x0600633D RID: 25405
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700210B RID: 8459
		// (get) Token: 0x0600633E RID: 25406
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700210C RID: 8460
		// (get) Token: 0x0600633F RID: 25407
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700210D RID: 8461
		// (get) Token: 0x06006341 RID: 25409
		// (set) Token: 0x06006340 RID: 25408
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

		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x06006343 RID: 25411
		// (set) Token: 0x06006342 RID: 25410
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

		// Token: 0x1700210F RID: 8463
		// (get) Token: 0x06006345 RID: 25413
		// (set) Token: 0x06006344 RID: 25412
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

		// Token: 0x17002110 RID: 8464
		// (get) Token: 0x06006347 RID: 25415
		// (set) Token: 0x06006346 RID: 25414
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

		// Token: 0x06006348 RID: 25416
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06006349 RID: 25417
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17002111 RID: 8465
		// (get) Token: 0x0600634A RID: 25418
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002112 RID: 8466
		// (get) Token: 0x0600634B RID: 25419
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600634C RID: 25420
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17002113 RID: 8467
		// (get) Token: 0x0600634D RID: 25421
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002114 RID: 8468
		// (get) Token: 0x0600634F RID: 25423
		// (set) Token: 0x0600634E RID: 25422
		[DispId(-2147412077)]
		public virtual extern object ondragstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06006350 RID: 25424
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17002115 RID: 8469
		// (get) Token: 0x06006352 RID: 25426
		// (set) Token: 0x06006351 RID: 25425
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

		// Token: 0x17002116 RID: 8470
		// (get) Token: 0x06006354 RID: 25428
		// (set) Token: 0x06006353 RID: 25427
		[DispId(-2147412090)]
		public virtual extern object onafterupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002117 RID: 8471
		// (get) Token: 0x06006356 RID: 25430
		// (set) Token: 0x06006355 RID: 25429
		[DispId(-2147412074)]
		public virtual extern object onerrorupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002118 RID: 8472
		// (get) Token: 0x06006358 RID: 25432
		// (set) Token: 0x06006357 RID: 25431
		[DispId(-2147412094)]
		public virtual extern object onrowexit
		{
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002119 RID: 8473
		// (get) Token: 0x0600635A RID: 25434
		// (set) Token: 0x06006359 RID: 25433
		[DispId(-2147412093)]
		public virtual extern object onrowenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700211A RID: 8474
		// (get) Token: 0x0600635C RID: 25436
		// (set) Token: 0x0600635B RID: 25435
		[DispId(-2147412072)]
		public virtual extern object ondatasetchanged
		{
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700211B RID: 8475
		// (get) Token: 0x0600635E RID: 25438
		// (set) Token: 0x0600635D RID: 25437
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

		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x06006360 RID: 25440
		// (set) Token: 0x0600635F RID: 25439
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

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x06006362 RID: 25442
		// (set) Token: 0x06006361 RID: 25441
		[DispId(-2147412069)]
		public virtual extern object onfilterchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x06006363 RID: 25443
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x06006364 RID: 25444
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x06006365 RID: 25445
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06006366 RID: 25446
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06006367 RID: 25447
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x06006369 RID: 25449
		// (set) Token: 0x06006368 RID: 25448
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

		// Token: 0x0600636A RID: 25450
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600636B RID: 25451
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x0600636D RID: 25453
		// (set) Token: 0x0600636C RID: 25452
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002123 RID: 8483
		// (get) Token: 0x0600636F RID: 25455
		// (set) Token: 0x0600636E RID: 25454
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

		// Token: 0x17002124 RID: 8484
		// (get) Token: 0x06006371 RID: 25457
		// (set) Token: 0x06006370 RID: 25456
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

		// Token: 0x17002125 RID: 8485
		// (get) Token: 0x06006373 RID: 25459
		// (set) Token: 0x06006372 RID: 25458
		[DispId(-2147412061)]
		public virtual extern object ondragenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002126 RID: 8486
		// (get) Token: 0x06006375 RID: 25461
		// (set) Token: 0x06006374 RID: 25460
		[DispId(-2147412060)]
		public virtual extern object ondragover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002127 RID: 8487
		// (get) Token: 0x06006377 RID: 25463
		// (set) Token: 0x06006376 RID: 25462
		[DispId(-2147412059)]
		public virtual extern object ondragleave
		{
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002128 RID: 8488
		// (get) Token: 0x06006379 RID: 25465
		// (set) Token: 0x06006378 RID: 25464
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

		// Token: 0x17002129 RID: 8489
		// (get) Token: 0x0600637B RID: 25467
		// (set) Token: 0x0600637A RID: 25466
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

		// Token: 0x1700212A RID: 8490
		// (get) Token: 0x0600637D RID: 25469
		// (set) Token: 0x0600637C RID: 25468
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700212B RID: 8491
		// (get) Token: 0x0600637F RID: 25471
		// (set) Token: 0x0600637E RID: 25470
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

		// Token: 0x1700212C RID: 8492
		// (get) Token: 0x06006381 RID: 25473
		// (set) Token: 0x06006380 RID: 25472
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

		// Token: 0x1700212D RID: 8493
		// (get) Token: 0x06006383 RID: 25475
		// (set) Token: 0x06006382 RID: 25474
		[DispId(-2147412052)]
		public virtual extern object onbeforepaste
		{
			[DispId(-2147412052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700212E RID: 8494
		// (get) Token: 0x06006385 RID: 25477
		// (set) Token: 0x06006384 RID: 25476
		[DispId(-2147412055)]
		public virtual extern object onpaste
		{
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700212F RID: 8495
		// (get) Token: 0x06006386 RID: 25478
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002130 RID: 8496
		// (get) Token: 0x06006388 RID: 25480
		// (set) Token: 0x06006387 RID: 25479
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

		// Token: 0x06006389 RID: 25481
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x0600638A RID: 25482
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x0600638B RID: 25483
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600638C RID: 25484
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600638D RID: 25485
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17002131 RID: 8497
		// (get) Token: 0x0600638F RID: 25487
		// (set) Token: 0x0600638E RID: 25486
		[DispId(-2147418097)]
		public virtual extern short tabIndex
		{
			[TypeLibFunc(20)]
			[DispId(-2147418097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06006390 RID: 25488
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17002132 RID: 8498
		// (get) Token: 0x06006392 RID: 25490
		// (set) Token: 0x06006391 RID: 25489
		[DispId(-2147416107)]
		public virtual extern string accessKey
		{
			[DispId(-2147416107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147416107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002133 RID: 8499
		// (get) Token: 0x06006394 RID: 25492
		// (set) Token: 0x06006393 RID: 25491
		[DispId(-2147412097)]
		public virtual extern object onblur
		{
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002134 RID: 8500
		// (get) Token: 0x06006396 RID: 25494
		// (set) Token: 0x06006395 RID: 25493
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002135 RID: 8501
		// (get) Token: 0x06006398 RID: 25496
		// (set) Token: 0x06006397 RID: 25495
		[DispId(-2147412076)]
		public virtual extern object onresize
		{
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06006399 RID: 25497
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600639A RID: 25498
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600639B RID: 25499
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x0600639C RID: 25500
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002137 RID: 8503
		// (get) Token: 0x0600639D RID: 25501
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x0600639E RID: 25502
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x0600639F RID: 25503
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060063A0 RID: 25504
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060063A1 RID: 25505
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x060063A2 RID: 25506
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x060063A4 RID: 25508
		// (set) Token: 0x060063A3 RID: 25507
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

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x060063A6 RID: 25510
		// (set) Token: 0x060063A5 RID: 25509
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

		// Token: 0x1700213D RID: 8509
		// (get) Token: 0x060063A8 RID: 25512
		// (set) Token: 0x060063A7 RID: 25511
		[DispId(-2147412049)]
		public virtual extern object onrowsinserted
		{
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700213E RID: 8510
		// (get) Token: 0x060063AA RID: 25514
		// (set) Token: 0x060063A9 RID: 25513
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

		// Token: 0x1700213F RID: 8511
		// (get) Token: 0x060063AC RID: 25516
		// (set) Token: 0x060063AB RID: 25515
		[DispId(-2147412995)]
		public virtual extern string dir
		{
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x060063AD RID: 25517
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17002140 RID: 8512
		// (get) Token: 0x060063AE RID: 25518
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002141 RID: 8513
		// (get) Token: 0x060063AF RID: 25519
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002142 RID: 8514
		// (get) Token: 0x060063B1 RID: 25521
		// (set) Token: 0x060063B0 RID: 25520
		[DispId(-2147417053)]
		public virtual extern int scrollTop
		{
			[DispId(-2147417053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002143 RID: 8515
		// (get) Token: 0x060063B3 RID: 25523
		// (set) Token: 0x060063B2 RID: 25522
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

		// Token: 0x060063B4 RID: 25524
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17002144 RID: 8516
		// (get) Token: 0x060063B6 RID: 25526
		// (set) Token: 0x060063B5 RID: 25525
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

		// Token: 0x060063B7 RID: 25527
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060063B8 RID: 25528
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060063B9 RID: 25529
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060063BA RID: 25530
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17002145 RID: 8517
		// (get) Token: 0x060063BB RID: 25531
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060063BC RID: 25532
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060063BD RID: 25533
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17002146 RID: 8518
		// (get) Token: 0x060063BE RID: 25534
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002147 RID: 8519
		// (get) Token: 0x060063BF RID: 25535
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002148 RID: 8520
		// (get) Token: 0x060063C1 RID: 25537
		// (set) Token: 0x060063C0 RID: 25536
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

		// Token: 0x17002149 RID: 8521
		// (get) Token: 0x060063C3 RID: 25539
		// (set) Token: 0x060063C2 RID: 25538
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

		// Token: 0x1700214A RID: 8522
		// (get) Token: 0x060063C4 RID: 25540
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060063C5 RID: 25541
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060063C6 RID: 25542
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x1700214B RID: 8523
		// (get) Token: 0x060063C7 RID: 25543
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700214C RID: 8524
		// (get) Token: 0x060063C8 RID: 25544
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700214D RID: 8525
		// (get) Token: 0x060063CA RID: 25546
		// (set) Token: 0x060063C9 RID: 25545
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

		// Token: 0x1700214E RID: 8526
		// (get) Token: 0x060063CC RID: 25548
		// (set) Token: 0x060063CB RID: 25547
		[DispId(-2147412038)]
		public virtual extern object onpage
		{
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700214F RID: 8527
		// (get) Token: 0x060063CE RID: 25550
		// (set) Token: 0x060063CD RID: 25549
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

		// Token: 0x17002150 RID: 8528
		// (get) Token: 0x060063D0 RID: 25552
		// (set) Token: 0x060063CF RID: 25551
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

		// Token: 0x060063D1 RID: 25553
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17002151 RID: 8529
		// (get) Token: 0x060063D3 RID: 25555
		// (set) Token: 0x060063D2 RID: 25554
		[DispId(-2147412950)]
		public virtual extern string contentEditable
		{
			[DispId(-2147412950)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002152 RID: 8530
		// (get) Token: 0x060063D4 RID: 25556
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002153 RID: 8531
		// (get) Token: 0x060063D6 RID: 25558
		// (set) Token: 0x060063D5 RID: 25557
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

		// Token: 0x17002154 RID: 8532
		// (get) Token: 0x060063D8 RID: 25560
		// (set) Token: 0x060063D7 RID: 25559
		[DispId(-2147418036)]
		public virtual extern bool disabled
		{
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002155 RID: 8533
		// (get) Token: 0x060063D9 RID: 25561
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002156 RID: 8534
		// (get) Token: 0x060063DB RID: 25563
		// (set) Token: 0x060063DA RID: 25562
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

		// Token: 0x17002157 RID: 8535
		// (get) Token: 0x060063DD RID: 25565
		// (set) Token: 0x060063DC RID: 25564
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

		// Token: 0x060063DE RID: 25566
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17002158 RID: 8536
		// (get) Token: 0x060063E0 RID: 25568
		// (set) Token: 0x060063DF RID: 25567
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

		// Token: 0x17002159 RID: 8537
		// (get) Token: 0x060063E2 RID: 25570
		// (set) Token: 0x060063E1 RID: 25569
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

		// Token: 0x1700215A RID: 8538
		// (get) Token: 0x060063E4 RID: 25572
		// (set) Token: 0x060063E3 RID: 25571
		[DispId(-2147412031)]
		public virtual extern object onmovestart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700215B RID: 8539
		// (get) Token: 0x060063E6 RID: 25574
		// (set) Token: 0x060063E5 RID: 25573
		[DispId(-2147412030)]
		public virtual extern object onmoveend
		{
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700215C RID: 8540
		// (get) Token: 0x060063E8 RID: 25576
		// (set) Token: 0x060063E7 RID: 25575
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

		// Token: 0x1700215D RID: 8541
		// (get) Token: 0x060063EA RID: 25578
		// (set) Token: 0x060063E9 RID: 25577
		[DispId(-2147412026)]
		public virtual extern object onmouseleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700215E RID: 8542
		// (get) Token: 0x060063EC RID: 25580
		// (set) Token: 0x060063EB RID: 25579
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

		// Token: 0x1700215F RID: 8543
		// (get) Token: 0x060063EE RID: 25582
		// (set) Token: 0x060063ED RID: 25581
		[DispId(-2147412024)]
		public virtual extern object ondeactivate
		{
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060063EF RID: 25583
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17002160 RID: 8544
		// (get) Token: 0x060063F0 RID: 25584
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002161 RID: 8545
		// (get) Token: 0x060063F2 RID: 25586
		// (set) Token: 0x060063F1 RID: 25585
		[DispId(-2147412036)]
		public virtual extern object onmousewheel
		{
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060063F3 RID: 25587
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x060063F4 RID: 25588
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x060063F5 RID: 25589
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x060063F6 RID: 25590
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17002162 RID: 8546
		// (get) Token: 0x060063F8 RID: 25592
		// (set) Token: 0x060063F7 RID: 25591
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

		// Token: 0x17002163 RID: 8547
		// (get) Token: 0x060063FA RID: 25594
		// (set) Token: 0x060063F9 RID: 25593
		[DispId(-2147412021)]
		public virtual extern object onfocusin
		{
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002164 RID: 8548
		// (get) Token: 0x060063FC RID: 25596
		// (set) Token: 0x060063FB RID: 25595
		[DispId(-2147412020)]
		public virtual extern object onfocusout
		{
			[DispId(-2147412020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002165 RID: 8549
		// (get) Token: 0x060063FD RID: 25597
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002166 RID: 8550
		// (get) Token: 0x060063FE RID: 25598
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x060063FF RID: 25599
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x06006400 RID: 25600
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06006401 RID: 25601
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x06006402 RID: 25602
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x06006403 RID: 25603
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06006404 RID: 25604
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06006405 RID: 25605
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06006406 RID: 25606
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06006407 RID: 25607
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06006408 RID: 25608
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06006409 RID: 25609
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600640A RID: 25610
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600640B RID: 25611
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x0600640C RID: 25612
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x0600640E RID: 25614
		// (set) Token: 0x0600640D RID: 25613
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

		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x0600640F RID: 25615
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x06006410 RID: 25616
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x06006411 RID: 25617
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002170 RID: 8560
		// (get) Token: 0x06006412 RID: 25618
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002171 RID: 8561
		// (get) Token: 0x06006413 RID: 25619
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002172 RID: 8562
		// (get) Token: 0x06006415 RID: 25621
		// (set) Token: 0x06006414 RID: 25620
		[DispId(-2147413107)]
		public virtual extern bool noWrap
		{
			[DispId(-2147413107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06006416 RID: 25622
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06006417 RID: 25623
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06006418 RID: 25624
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17002173 RID: 8563
		// (get) Token: 0x0600641A RID: 25626
		// (set) Token: 0x06006419 RID: 25625
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

		// Token: 0x17002174 RID: 8564
		// (get) Token: 0x0600641C RID: 25628
		// (set) Token: 0x0600641B RID: 25627
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

		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x0600641D RID: 25629
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x0600641E RID: 25630
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x0600641F RID: 25631
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x06006421 RID: 25633
		// (set) Token: 0x06006420 RID: 25632
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

		// Token: 0x17002179 RID: 8569
		// (get) Token: 0x06006423 RID: 25635
		// (set) Token: 0x06006422 RID: 25634
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

		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x06006425 RID: 25637
		// (set) Token: 0x06006424 RID: 25636
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

		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x06006427 RID: 25639
		// (set) Token: 0x06006426 RID: 25638
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

		// Token: 0x1700217C RID: 8572
		// (get) Token: 0x06006429 RID: 25641
		// (set) Token: 0x06006428 RID: 25640
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

		// Token: 0x1700217D RID: 8573
		// (get) Token: 0x0600642B RID: 25643
		// (set) Token: 0x0600642A RID: 25642
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

		// Token: 0x1700217E RID: 8574
		// (get) Token: 0x0600642D RID: 25645
		// (set) Token: 0x0600642C RID: 25644
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

		// Token: 0x1700217F RID: 8575
		// (get) Token: 0x0600642F RID: 25647
		// (set) Token: 0x0600642E RID: 25646
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

		// Token: 0x17002180 RID: 8576
		// (get) Token: 0x06006431 RID: 25649
		// (set) Token: 0x06006430 RID: 25648
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

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x06006433 RID: 25651
		// (set) Token: 0x06006432 RID: 25650
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

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x06006435 RID: 25653
		// (set) Token: 0x06006434 RID: 25652
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

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x06006436 RID: 25654
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x06006438 RID: 25656
		// (set) Token: 0x06006437 RID: 25655
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

		// Token: 0x17002185 RID: 8581
		// (get) Token: 0x0600643A RID: 25658
		// (set) Token: 0x06006439 RID: 25657
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

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x0600643C RID: 25660
		// (set) Token: 0x0600643B RID: 25659
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

		// Token: 0x0600643D RID: 25661
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600643E RID: 25662
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x0600643F RID: 25663
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x06006440 RID: 25664
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x06006442 RID: 25666
		// (set) Token: 0x06006441 RID: 25665
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

		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x06006443 RID: 25667
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700218B RID: 8587
		// (get) Token: 0x06006444 RID: 25668
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700218C RID: 8588
		// (get) Token: 0x06006445 RID: 25669
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700218D RID: 8589
		// (get) Token: 0x06006446 RID: 25670
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x06006447 RID: 25671
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x06006449 RID: 25673
		// (set) Token: 0x06006448 RID: 25672
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

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x0600644B RID: 25675
		// (set) Token: 0x0600644A RID: 25674
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

		// Token: 0x17002191 RID: 8593
		// (get) Token: 0x0600644D RID: 25677
		// (set) Token: 0x0600644C RID: 25676
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

		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x0600644F RID: 25679
		// (set) Token: 0x0600644E RID: 25678
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

		// Token: 0x06006450 RID: 25680
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06006451 RID: 25681
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x06006452 RID: 25682
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x06006453 RID: 25683
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06006454 RID: 25684
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x06006455 RID: 25685
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x06006457 RID: 25687
		// (set) Token: 0x06006456 RID: 25686
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

		// Token: 0x06006458 RID: 25688
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x0600645A RID: 25690
		// (set) Token: 0x06006459 RID: 25689
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

		// Token: 0x17002198 RID: 8600
		// (get) Token: 0x0600645C RID: 25692
		// (set) Token: 0x0600645B RID: 25691
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

		// Token: 0x17002199 RID: 8601
		// (get) Token: 0x0600645E RID: 25694
		// (set) Token: 0x0600645D RID: 25693
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

		// Token: 0x1700219A RID: 8602
		// (get) Token: 0x06006460 RID: 25696
		// (set) Token: 0x0600645F RID: 25695
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

		// Token: 0x1700219B RID: 8603
		// (get) Token: 0x06006462 RID: 25698
		// (set) Token: 0x06006461 RID: 25697
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

		// Token: 0x1700219C RID: 8604
		// (get) Token: 0x06006464 RID: 25700
		// (set) Token: 0x06006463 RID: 25699
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

		// Token: 0x1700219D RID: 8605
		// (get) Token: 0x06006466 RID: 25702
		// (set) Token: 0x06006465 RID: 25701
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

		// Token: 0x1700219E RID: 8606
		// (get) Token: 0x06006468 RID: 25704
		// (set) Token: 0x06006467 RID: 25703
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

		// Token: 0x1700219F RID: 8607
		// (get) Token: 0x0600646A RID: 25706
		// (set) Token: 0x06006469 RID: 25705
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

		// Token: 0x170021A0 RID: 8608
		// (get) Token: 0x0600646B RID: 25707
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170021A1 RID: 8609
		// (get) Token: 0x0600646C RID: 25708
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170021A2 RID: 8610
		// (get) Token: 0x0600646D RID: 25709
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600646E RID: 25710
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600646F RID: 25711
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x170021A3 RID: 8611
		// (get) Token: 0x06006471 RID: 25713
		// (set) Token: 0x06006470 RID: 25712
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

		// Token: 0x06006472 RID: 25714
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06006473 RID: 25715
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170021A4 RID: 8612
		// (get) Token: 0x06006475 RID: 25717
		// (set) Token: 0x06006474 RID: 25716
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

		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x06006477 RID: 25719
		// (set) Token: 0x06006476 RID: 25718
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

		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x06006479 RID: 25721
		// (set) Token: 0x06006478 RID: 25720
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

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x0600647B RID: 25723
		// (set) Token: 0x0600647A RID: 25722
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

		// Token: 0x170021A8 RID: 8616
		// (get) Token: 0x0600647D RID: 25725
		// (set) Token: 0x0600647C RID: 25724
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

		// Token: 0x170021A9 RID: 8617
		// (get) Token: 0x0600647F RID: 25727
		// (set) Token: 0x0600647E RID: 25726
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

		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x06006481 RID: 25729
		// (set) Token: 0x06006480 RID: 25728
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

		// Token: 0x170021AB RID: 8619
		// (get) Token: 0x06006483 RID: 25731
		// (set) Token: 0x06006482 RID: 25730
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

		// Token: 0x170021AC RID: 8620
		// (get) Token: 0x06006485 RID: 25733
		// (set) Token: 0x06006484 RID: 25732
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

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x06006487 RID: 25735
		// (set) Token: 0x06006486 RID: 25734
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

		// Token: 0x170021AE RID: 8622
		// (get) Token: 0x06006489 RID: 25737
		// (set) Token: 0x06006488 RID: 25736
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

		// Token: 0x170021AF RID: 8623
		// (get) Token: 0x0600648B RID: 25739
		// (set) Token: 0x0600648A RID: 25738
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

		// Token: 0x170021B0 RID: 8624
		// (get) Token: 0x0600648D RID: 25741
		// (set) Token: 0x0600648C RID: 25740
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

		// Token: 0x170021B1 RID: 8625
		// (get) Token: 0x0600648E RID: 25742
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170021B2 RID: 8626
		// (get) Token: 0x06006490 RID: 25744
		// (set) Token: 0x0600648F RID: 25743
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

		// Token: 0x06006491 RID: 25745
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06006492 RID: 25746
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06006493 RID: 25747
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06006494 RID: 25748
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06006495 RID: 25749
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170021B3 RID: 8627
		// (get) Token: 0x06006497 RID: 25751
		// (set) Token: 0x06006496 RID: 25750
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

		// Token: 0x06006498 RID: 25752
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x170021B4 RID: 8628
		// (get) Token: 0x0600649A RID: 25754
		// (set) Token: 0x06006499 RID: 25753
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

		// Token: 0x170021B5 RID: 8629
		// (get) Token: 0x0600649C RID: 25756
		// (set) Token: 0x0600649B RID: 25755
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

		// Token: 0x170021B6 RID: 8630
		// (get) Token: 0x0600649E RID: 25758
		// (set) Token: 0x0600649D RID: 25757
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

		// Token: 0x170021B7 RID: 8631
		// (get) Token: 0x060064A0 RID: 25760
		// (set) Token: 0x0600649F RID: 25759
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

		// Token: 0x060064A1 RID: 25761
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x060064A2 RID: 25762
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060064A3 RID: 25763
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170021B8 RID: 8632
		// (get) Token: 0x060064A4 RID: 25764
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021B9 RID: 8633
		// (get) Token: 0x060064A5 RID: 25765
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021BA RID: 8634
		// (get) Token: 0x060064A6 RID: 25766
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021BB RID: 8635
		// (get) Token: 0x060064A7 RID: 25767
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060064A8 RID: 25768
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060064A9 RID: 25769
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170021BC RID: 8636
		// (get) Token: 0x060064AA RID: 25770
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170021BD RID: 8637
		// (get) Token: 0x060064AC RID: 25772
		// (set) Token: 0x060064AB RID: 25771
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

		// Token: 0x170021BE RID: 8638
		// (get) Token: 0x060064AE RID: 25774
		// (set) Token: 0x060064AD RID: 25773
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

		// Token: 0x170021BF RID: 8639
		// (get) Token: 0x060064B0 RID: 25776
		// (set) Token: 0x060064AF RID: 25775
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

		// Token: 0x170021C0 RID: 8640
		// (get) Token: 0x060064B2 RID: 25778
		// (set) Token: 0x060064B1 RID: 25777
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

		// Token: 0x170021C1 RID: 8641
		// (get) Token: 0x060064B4 RID: 25780
		// (set) Token: 0x060064B3 RID: 25779
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

		// Token: 0x060064B5 RID: 25781
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x170021C2 RID: 8642
		// (get) Token: 0x060064B6 RID: 25782
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021C3 RID: 8643
		// (get) Token: 0x060064B7 RID: 25783
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021C4 RID: 8644
		// (get) Token: 0x060064B9 RID: 25785
		// (set) Token: 0x060064B8 RID: 25784
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

		// Token: 0x170021C5 RID: 8645
		// (get) Token: 0x060064BB RID: 25787
		// (set) Token: 0x060064BA RID: 25786
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

		// Token: 0x060064BC RID: 25788
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x060064BD RID: 25789
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x170021C6 RID: 8646
		// (get) Token: 0x060064BF RID: 25791
		// (set) Token: 0x060064BE RID: 25790
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

		// Token: 0x060064C0 RID: 25792
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060064C1 RID: 25793
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060064C2 RID: 25794
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060064C3 RID: 25795
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170021C7 RID: 8647
		// (get) Token: 0x060064C4 RID: 25796
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060064C5 RID: 25797
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060064C6 RID: 25798
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x170021C8 RID: 8648
		// (get) Token: 0x060064C7 RID: 25799
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170021C9 RID: 8649
		// (get) Token: 0x060064C8 RID: 25800
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170021CA RID: 8650
		// (get) Token: 0x060064CA RID: 25802
		// (set) Token: 0x060064C9 RID: 25801
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

		// Token: 0x170021CB RID: 8651
		// (get) Token: 0x060064CC RID: 25804
		// (set) Token: 0x060064CB RID: 25803
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

		// Token: 0x170021CC RID: 8652
		// (get) Token: 0x060064CD RID: 25805
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060064CE RID: 25806
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060064CF RID: 25807
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170021CD RID: 8653
		// (get) Token: 0x060064D0 RID: 25808
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021CE RID: 8654
		// (get) Token: 0x060064D1 RID: 25809
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x060064D3 RID: 25811
		// (set) Token: 0x060064D2 RID: 25810
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

		// Token: 0x170021D0 RID: 8656
		// (get) Token: 0x060064D5 RID: 25813
		// (set) Token: 0x060064D4 RID: 25812
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

		// Token: 0x170021D1 RID: 8657
		// (get) Token: 0x060064D7 RID: 25815
		// (set) Token: 0x060064D6 RID: 25814
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

		// Token: 0x170021D2 RID: 8658
		// (get) Token: 0x060064D9 RID: 25817
		// (set) Token: 0x060064D8 RID: 25816
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

		// Token: 0x060064DA RID: 25818
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x170021D3 RID: 8659
		// (get) Token: 0x060064DC RID: 25820
		// (set) Token: 0x060064DB RID: 25819
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

		// Token: 0x170021D4 RID: 8660
		// (get) Token: 0x060064DD RID: 25821
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x060064DF RID: 25823
		// (set) Token: 0x060064DE RID: 25822
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

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x060064E1 RID: 25825
		// (set) Token: 0x060064E0 RID: 25824
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

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x060064E2 RID: 25826
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x060064E4 RID: 25828
		// (set) Token: 0x060064E3 RID: 25827
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

		// Token: 0x170021D9 RID: 8665
		// (get) Token: 0x060064E6 RID: 25830
		// (set) Token: 0x060064E5 RID: 25829
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

		// Token: 0x060064E7 RID: 25831
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170021DA RID: 8666
		// (get) Token: 0x060064E9 RID: 25833
		// (set) Token: 0x060064E8 RID: 25832
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

		// Token: 0x170021DB RID: 8667
		// (get) Token: 0x060064EB RID: 25835
		// (set) Token: 0x060064EA RID: 25834
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

		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x060064ED RID: 25837
		// (set) Token: 0x060064EC RID: 25836
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

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x060064EF RID: 25839
		// (set) Token: 0x060064EE RID: 25838
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

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x060064F1 RID: 25841
		// (set) Token: 0x060064F0 RID: 25840
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

		// Token: 0x170021DF RID: 8671
		// (get) Token: 0x060064F3 RID: 25843
		// (set) Token: 0x060064F2 RID: 25842
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

		// Token: 0x170021E0 RID: 8672
		// (get) Token: 0x060064F5 RID: 25845
		// (set) Token: 0x060064F4 RID: 25844
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

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x060064F7 RID: 25847
		// (set) Token: 0x060064F6 RID: 25846
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

		// Token: 0x060064F8 RID: 25848
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x170021E2 RID: 8674
		// (get) Token: 0x060064F9 RID: 25849
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021E3 RID: 8675
		// (get) Token: 0x060064FB RID: 25851
		// (set) Token: 0x060064FA RID: 25850
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

		// Token: 0x060064FC RID: 25852
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x060064FD RID: 25853
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x060064FE RID: 25854
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x060064FF RID: 25855
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x170021E4 RID: 8676
		// (get) Token: 0x06006501 RID: 25857
		// (set) Token: 0x06006500 RID: 25856
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

		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x06006503 RID: 25859
		// (set) Token: 0x06006502 RID: 25858
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

		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x06006505 RID: 25861
		// (set) Token: 0x06006504 RID: 25860
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

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x06006506 RID: 25862
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x06006507 RID: 25863
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170021E9 RID: 8681
		// (get) Token: 0x06006508 RID: 25864
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170021EA RID: 8682
		// (get) Token: 0x06006509 RID: 25865
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600650A RID: 25866
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x0600650B RID: 25867
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x0600650C RID: 25868
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600650D RID: 25869
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600650E RID: 25870
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600650F RID: 25871
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06006510 RID: 25872
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06006511 RID: 25873
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06006512 RID: 25874
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06006513 RID: 25875
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06006514 RID: 25876
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170021ED RID: 8685
		// (get) Token: 0x06006515 RID: 25877
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170021EE RID: 8686
		// (get) Token: 0x06006517 RID: 25879
		// (set) Token: 0x06006516 RID: 25878
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

		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x06006518 RID: 25880
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x06006519 RID: 25881
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170021F1 RID: 8689
		// (get) Token: 0x0600651A RID: 25882
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170021F2 RID: 8690
		// (get) Token: 0x0600651B RID: 25883
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170021F3 RID: 8691
		// (get) Token: 0x0600651C RID: 25884
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x0600651E RID: 25886
		// (set) Token: 0x0600651D RID: 25885
		public virtual extern bool IHTMLDDElement_noWrap
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x14000B64 RID: 2916
		// (add) Token: 0x0600651F RID: 25887
		// (remove) Token: 0x06006520 RID: 25888
		public virtual extern event HTMLElementEvents_onhelpEventHandler HTMLElementEvents_Event_onhelp;

		// Token: 0x14000B65 RID: 2917
		// (add) Token: 0x06006521 RID: 25889
		// (remove) Token: 0x06006522 RID: 25890
		public virtual extern event HTMLElementEvents_onclickEventHandler HTMLElementEvents_Event_onclick;

		// Token: 0x14000B66 RID: 2918
		// (add) Token: 0x06006523 RID: 25891
		// (remove) Token: 0x06006524 RID: 25892
		public virtual extern event HTMLElementEvents_ondblclickEventHandler HTMLElementEvents_Event_ondblclick;

		// Token: 0x14000B67 RID: 2919
		// (add) Token: 0x06006525 RID: 25893
		// (remove) Token: 0x06006526 RID: 25894
		public virtual extern event HTMLElementEvents_onkeypressEventHandler HTMLElementEvents_Event_onkeypress;

		// Token: 0x14000B68 RID: 2920
		// (add) Token: 0x06006527 RID: 25895
		// (remove) Token: 0x06006528 RID: 25896
		public virtual extern event HTMLElementEvents_onkeydownEventHandler HTMLElementEvents_Event_onkeydown;

		// Token: 0x14000B69 RID: 2921
		// (add) Token: 0x06006529 RID: 25897
		// (remove) Token: 0x0600652A RID: 25898
		public virtual extern event HTMLElementEvents_onkeyupEventHandler HTMLElementEvents_Event_onkeyup;

		// Token: 0x14000B6A RID: 2922
		// (add) Token: 0x0600652B RID: 25899
		// (remove) Token: 0x0600652C RID: 25900
		public virtual extern event HTMLElementEvents_onmouseoutEventHandler HTMLElementEvents_Event_onmouseout;

		// Token: 0x14000B6B RID: 2923
		// (add) Token: 0x0600652D RID: 25901
		// (remove) Token: 0x0600652E RID: 25902
		public virtual extern event HTMLElementEvents_onmouseoverEventHandler HTMLElementEvents_Event_onmouseover;

		// Token: 0x14000B6C RID: 2924
		// (add) Token: 0x0600652F RID: 25903
		// (remove) Token: 0x06006530 RID: 25904
		public virtual extern event HTMLElementEvents_onmousemoveEventHandler HTMLElementEvents_Event_onmousemove;

		// Token: 0x14000B6D RID: 2925
		// (add) Token: 0x06006531 RID: 25905
		// (remove) Token: 0x06006532 RID: 25906
		public virtual extern event HTMLElementEvents_onmousedownEventHandler HTMLElementEvents_Event_onmousedown;

		// Token: 0x14000B6E RID: 2926
		// (add) Token: 0x06006533 RID: 25907
		// (remove) Token: 0x06006534 RID: 25908
		public virtual extern event HTMLElementEvents_onmouseupEventHandler HTMLElementEvents_Event_onmouseup;

		// Token: 0x14000B6F RID: 2927
		// (add) Token: 0x06006535 RID: 25909
		// (remove) Token: 0x06006536 RID: 25910
		public virtual extern event HTMLElementEvents_onselectstartEventHandler HTMLElementEvents_Event_onselectstart;

		// Token: 0x14000B70 RID: 2928
		// (add) Token: 0x06006537 RID: 25911
		// (remove) Token: 0x06006538 RID: 25912
		public virtual extern event HTMLElementEvents_onfilterchangeEventHandler HTMLElementEvents_Event_onfilterchange;

		// Token: 0x14000B71 RID: 2929
		// (add) Token: 0x06006539 RID: 25913
		// (remove) Token: 0x0600653A RID: 25914
		public virtual extern event HTMLElementEvents_ondragstartEventHandler HTMLElementEvents_Event_ondragstart;

		// Token: 0x14000B72 RID: 2930
		// (add) Token: 0x0600653B RID: 25915
		// (remove) Token: 0x0600653C RID: 25916
		public virtual extern event HTMLElementEvents_onbeforeupdateEventHandler HTMLElementEvents_Event_onbeforeupdate;

		// Token: 0x14000B73 RID: 2931
		// (add) Token: 0x0600653D RID: 25917
		// (remove) Token: 0x0600653E RID: 25918
		public virtual extern event HTMLElementEvents_onafterupdateEventHandler HTMLElementEvents_Event_onafterupdate;

		// Token: 0x14000B74 RID: 2932
		// (add) Token: 0x0600653F RID: 25919
		// (remove) Token: 0x06006540 RID: 25920
		public virtual extern event HTMLElementEvents_onerrorupdateEventHandler HTMLElementEvents_Event_onerrorupdate;

		// Token: 0x14000B75 RID: 2933
		// (add) Token: 0x06006541 RID: 25921
		// (remove) Token: 0x06006542 RID: 25922
		public virtual extern event HTMLElementEvents_onrowexitEventHandler HTMLElementEvents_Event_onrowexit;

		// Token: 0x14000B76 RID: 2934
		// (add) Token: 0x06006543 RID: 25923
		// (remove) Token: 0x06006544 RID: 25924
		public virtual extern event HTMLElementEvents_onrowenterEventHandler HTMLElementEvents_Event_onrowenter;

		// Token: 0x14000B77 RID: 2935
		// (add) Token: 0x06006545 RID: 25925
		// (remove) Token: 0x06006546 RID: 25926
		public virtual extern event HTMLElementEvents_ondatasetchangedEventHandler HTMLElementEvents_Event_ondatasetchanged;

		// Token: 0x14000B78 RID: 2936
		// (add) Token: 0x06006547 RID: 25927
		// (remove) Token: 0x06006548 RID: 25928
		public virtual extern event HTMLElementEvents_ondataavailableEventHandler HTMLElementEvents_Event_ondataavailable;

		// Token: 0x14000B79 RID: 2937
		// (add) Token: 0x06006549 RID: 25929
		// (remove) Token: 0x0600654A RID: 25930
		public virtual extern event HTMLElementEvents_ondatasetcompleteEventHandler HTMLElementEvents_Event_ondatasetcomplete;

		// Token: 0x14000B7A RID: 2938
		// (add) Token: 0x0600654B RID: 25931
		// (remove) Token: 0x0600654C RID: 25932
		public virtual extern event HTMLElementEvents_onlosecaptureEventHandler HTMLElementEvents_Event_onlosecapture;

		// Token: 0x14000B7B RID: 2939
		// (add) Token: 0x0600654D RID: 25933
		// (remove) Token: 0x0600654E RID: 25934
		public virtual extern event HTMLElementEvents_onpropertychangeEventHandler HTMLElementEvents_Event_onpropertychange;

		// Token: 0x14000B7C RID: 2940
		// (add) Token: 0x0600654F RID: 25935
		// (remove) Token: 0x06006550 RID: 25936
		public virtual extern event HTMLElementEvents_onscrollEventHandler HTMLElementEvents_Event_onscroll;

		// Token: 0x14000B7D RID: 2941
		// (add) Token: 0x06006551 RID: 25937
		// (remove) Token: 0x06006552 RID: 25938
		public virtual extern event HTMLElementEvents_onfocusEventHandler HTMLElementEvents_Event_onfocus;

		// Token: 0x14000B7E RID: 2942
		// (add) Token: 0x06006553 RID: 25939
		// (remove) Token: 0x06006554 RID: 25940
		public virtual extern event HTMLElementEvents_onblurEventHandler HTMLElementEvents_Event_onblur;

		// Token: 0x14000B7F RID: 2943
		// (add) Token: 0x06006555 RID: 25941
		// (remove) Token: 0x06006556 RID: 25942
		public virtual extern event HTMLElementEvents_onresizeEventHandler HTMLElementEvents_Event_onresize;

		// Token: 0x14000B80 RID: 2944
		// (add) Token: 0x06006557 RID: 25943
		// (remove) Token: 0x06006558 RID: 25944
		public virtual extern event HTMLElementEvents_ondragEventHandler HTMLElementEvents_Event_ondrag;

		// Token: 0x14000B81 RID: 2945
		// (add) Token: 0x06006559 RID: 25945
		// (remove) Token: 0x0600655A RID: 25946
		public virtual extern event HTMLElementEvents_ondragendEventHandler HTMLElementEvents_Event_ondragend;

		// Token: 0x14000B82 RID: 2946
		// (add) Token: 0x0600655B RID: 25947
		// (remove) Token: 0x0600655C RID: 25948
		public virtual extern event HTMLElementEvents_ondragenterEventHandler HTMLElementEvents_Event_ondragenter;

		// Token: 0x14000B83 RID: 2947
		// (add) Token: 0x0600655D RID: 25949
		// (remove) Token: 0x0600655E RID: 25950
		public virtual extern event HTMLElementEvents_ondragoverEventHandler HTMLElementEvents_Event_ondragover;

		// Token: 0x14000B84 RID: 2948
		// (add) Token: 0x0600655F RID: 25951
		// (remove) Token: 0x06006560 RID: 25952
		public virtual extern event HTMLElementEvents_ondragleaveEventHandler HTMLElementEvents_Event_ondragleave;

		// Token: 0x14000B85 RID: 2949
		// (add) Token: 0x06006561 RID: 25953
		// (remove) Token: 0x06006562 RID: 25954
		public virtual extern event HTMLElementEvents_ondropEventHandler HTMLElementEvents_Event_ondrop;

		// Token: 0x14000B86 RID: 2950
		// (add) Token: 0x06006563 RID: 25955
		// (remove) Token: 0x06006564 RID: 25956
		public virtual extern event HTMLElementEvents_onbeforecutEventHandler HTMLElementEvents_Event_onbeforecut;

		// Token: 0x14000B87 RID: 2951
		// (add) Token: 0x06006565 RID: 25957
		// (remove) Token: 0x06006566 RID: 25958
		public virtual extern event HTMLElementEvents_oncutEventHandler HTMLElementEvents_Event_oncut;

		// Token: 0x14000B88 RID: 2952
		// (add) Token: 0x06006567 RID: 25959
		// (remove) Token: 0x06006568 RID: 25960
		public virtual extern event HTMLElementEvents_onbeforecopyEventHandler HTMLElementEvents_Event_onbeforecopy;

		// Token: 0x14000B89 RID: 2953
		// (add) Token: 0x06006569 RID: 25961
		// (remove) Token: 0x0600656A RID: 25962
		public virtual extern event HTMLElementEvents_oncopyEventHandler HTMLElementEvents_Event_oncopy;

		// Token: 0x14000B8A RID: 2954
		// (add) Token: 0x0600656B RID: 25963
		// (remove) Token: 0x0600656C RID: 25964
		public virtual extern event HTMLElementEvents_onbeforepasteEventHandler HTMLElementEvents_Event_onbeforepaste;

		// Token: 0x14000B8B RID: 2955
		// (add) Token: 0x0600656D RID: 25965
		// (remove) Token: 0x0600656E RID: 25966
		public virtual extern event HTMLElementEvents_onpasteEventHandler HTMLElementEvents_Event_onpaste;

		// Token: 0x14000B8C RID: 2956
		// (add) Token: 0x0600656F RID: 25967
		// (remove) Token: 0x06006570 RID: 25968
		public virtual extern event HTMLElementEvents_oncontextmenuEventHandler HTMLElementEvents_Event_oncontextmenu;

		// Token: 0x14000B8D RID: 2957
		// (add) Token: 0x06006571 RID: 25969
		// (remove) Token: 0x06006572 RID: 25970
		public virtual extern event HTMLElementEvents_onrowsdeleteEventHandler HTMLElementEvents_Event_onrowsdelete;

		// Token: 0x14000B8E RID: 2958
		// (add) Token: 0x06006573 RID: 25971
		// (remove) Token: 0x06006574 RID: 25972
		public virtual extern event HTMLElementEvents_onrowsinsertedEventHandler HTMLElementEvents_Event_onrowsinserted;

		// Token: 0x14000B8F RID: 2959
		// (add) Token: 0x06006575 RID: 25973
		// (remove) Token: 0x06006576 RID: 25974
		public virtual extern event HTMLElementEvents_oncellchangeEventHandler HTMLElementEvents_Event_oncellchange;

		// Token: 0x14000B90 RID: 2960
		// (add) Token: 0x06006577 RID: 25975
		// (remove) Token: 0x06006578 RID: 25976
		public virtual extern event HTMLElementEvents_onreadystatechangeEventHandler HTMLElementEvents_Event_onreadystatechange;

		// Token: 0x14000B91 RID: 2961
		// (add) Token: 0x06006579 RID: 25977
		// (remove) Token: 0x0600657A RID: 25978
		public virtual extern event HTMLElementEvents_onbeforeeditfocusEventHandler HTMLElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14000B92 RID: 2962
		// (add) Token: 0x0600657B RID: 25979
		// (remove) Token: 0x0600657C RID: 25980
		public virtual extern event HTMLElementEvents_onlayoutcompleteEventHandler HTMLElementEvents_Event_onlayoutcomplete;

		// Token: 0x14000B93 RID: 2963
		// (add) Token: 0x0600657D RID: 25981
		// (remove) Token: 0x0600657E RID: 25982
		public virtual extern event HTMLElementEvents_onpageEventHandler HTMLElementEvents_Event_onpage;

		// Token: 0x14000B94 RID: 2964
		// (add) Token: 0x0600657F RID: 25983
		// (remove) Token: 0x06006580 RID: 25984
		public virtual extern event HTMLElementEvents_onbeforedeactivateEventHandler HTMLElementEvents_Event_onbeforedeactivate;

		// Token: 0x14000B95 RID: 2965
		// (add) Token: 0x06006581 RID: 25985
		// (remove) Token: 0x06006582 RID: 25986
		public virtual extern event HTMLElementEvents_onbeforeactivateEventHandler HTMLElementEvents_Event_onbeforeactivate;

		// Token: 0x14000B96 RID: 2966
		// (add) Token: 0x06006583 RID: 25987
		// (remove) Token: 0x06006584 RID: 25988
		public virtual extern event HTMLElementEvents_onmoveEventHandler HTMLElementEvents_Event_onmove;

		// Token: 0x14000B97 RID: 2967
		// (add) Token: 0x06006585 RID: 25989
		// (remove) Token: 0x06006586 RID: 25990
		public virtual extern event HTMLElementEvents_oncontrolselectEventHandler HTMLElementEvents_Event_oncontrolselect;

		// Token: 0x14000B98 RID: 2968
		// (add) Token: 0x06006587 RID: 25991
		// (remove) Token: 0x06006588 RID: 25992
		public virtual extern event HTMLElementEvents_onmovestartEventHandler HTMLElementEvents_Event_onmovestart;

		// Token: 0x14000B99 RID: 2969
		// (add) Token: 0x06006589 RID: 25993
		// (remove) Token: 0x0600658A RID: 25994
		public virtual extern event HTMLElementEvents_onmoveendEventHandler HTMLElementEvents_Event_onmoveend;

		// Token: 0x14000B9A RID: 2970
		// (add) Token: 0x0600658B RID: 25995
		// (remove) Token: 0x0600658C RID: 25996
		public virtual extern event HTMLElementEvents_onresizestartEventHandler HTMLElementEvents_Event_onresizestart;

		// Token: 0x14000B9B RID: 2971
		// (add) Token: 0x0600658D RID: 25997
		// (remove) Token: 0x0600658E RID: 25998
		public virtual extern event HTMLElementEvents_onresizeendEventHandler HTMLElementEvents_Event_onresizeend;

		// Token: 0x14000B9C RID: 2972
		// (add) Token: 0x0600658F RID: 25999
		// (remove) Token: 0x06006590 RID: 26000
		public virtual extern event HTMLElementEvents_onmouseenterEventHandler HTMLElementEvents_Event_onmouseenter;

		// Token: 0x14000B9D RID: 2973
		// (add) Token: 0x06006591 RID: 26001
		// (remove) Token: 0x06006592 RID: 26002
		public virtual extern event HTMLElementEvents_onmouseleaveEventHandler HTMLElementEvents_Event_onmouseleave;

		// Token: 0x14000B9E RID: 2974
		// (add) Token: 0x06006593 RID: 26003
		// (remove) Token: 0x06006594 RID: 26004
		public virtual extern event HTMLElementEvents_onmousewheelEventHandler HTMLElementEvents_Event_onmousewheel;

		// Token: 0x14000B9F RID: 2975
		// (add) Token: 0x06006595 RID: 26005
		// (remove) Token: 0x06006596 RID: 26006
		public virtual extern event HTMLElementEvents_onactivateEventHandler HTMLElementEvents_Event_onactivate;

		// Token: 0x14000BA0 RID: 2976
		// (add) Token: 0x06006597 RID: 26007
		// (remove) Token: 0x06006598 RID: 26008
		public virtual extern event HTMLElementEvents_ondeactivateEventHandler HTMLElementEvents_Event_ondeactivate;

		// Token: 0x14000BA1 RID: 2977
		// (add) Token: 0x06006599 RID: 26009
		// (remove) Token: 0x0600659A RID: 26010
		public virtual extern event HTMLElementEvents_onfocusinEventHandler HTMLElementEvents_Event_onfocusin;

		// Token: 0x14000BA2 RID: 2978
		// (add) Token: 0x0600659B RID: 26011
		// (remove) Token: 0x0600659C RID: 26012
		public virtual extern event HTMLElementEvents_onfocusoutEventHandler HTMLElementEvents_Event_onfocusout;

		// Token: 0x14000BA3 RID: 2979
		// (add) Token: 0x0600659D RID: 26013
		// (remove) Token: 0x0600659E RID: 26014
		public virtual extern event HTMLElementEvents2_onhelpEventHandler HTMLElementEvents2_Event_onhelp;

		// Token: 0x14000BA4 RID: 2980
		// (add) Token: 0x0600659F RID: 26015
		// (remove) Token: 0x060065A0 RID: 26016
		public virtual extern event HTMLElementEvents2_onclickEventHandler HTMLElementEvents2_Event_onclick;

		// Token: 0x14000BA5 RID: 2981
		// (add) Token: 0x060065A1 RID: 26017
		// (remove) Token: 0x060065A2 RID: 26018
		public virtual extern event HTMLElementEvents2_ondblclickEventHandler HTMLElementEvents2_Event_ondblclick;

		// Token: 0x14000BA6 RID: 2982
		// (add) Token: 0x060065A3 RID: 26019
		// (remove) Token: 0x060065A4 RID: 26020
		public virtual extern event HTMLElementEvents2_onkeypressEventHandler HTMLElementEvents2_Event_onkeypress;

		// Token: 0x14000BA7 RID: 2983
		// (add) Token: 0x060065A5 RID: 26021
		// (remove) Token: 0x060065A6 RID: 26022
		public virtual extern event HTMLElementEvents2_onkeydownEventHandler HTMLElementEvents2_Event_onkeydown;

		// Token: 0x14000BA8 RID: 2984
		// (add) Token: 0x060065A7 RID: 26023
		// (remove) Token: 0x060065A8 RID: 26024
		public virtual extern event HTMLElementEvents2_onkeyupEventHandler HTMLElementEvents2_Event_onkeyup;

		// Token: 0x14000BA9 RID: 2985
		// (add) Token: 0x060065A9 RID: 26025
		// (remove) Token: 0x060065AA RID: 26026
		public virtual extern event HTMLElementEvents2_onmouseoutEventHandler HTMLElementEvents2_Event_onmouseout;

		// Token: 0x14000BAA RID: 2986
		// (add) Token: 0x060065AB RID: 26027
		// (remove) Token: 0x060065AC RID: 26028
		public virtual extern event HTMLElementEvents2_onmouseoverEventHandler HTMLElementEvents2_Event_onmouseover;

		// Token: 0x14000BAB RID: 2987
		// (add) Token: 0x060065AD RID: 26029
		// (remove) Token: 0x060065AE RID: 26030
		public virtual extern event HTMLElementEvents2_onmousemoveEventHandler HTMLElementEvents2_Event_onmousemove;

		// Token: 0x14000BAC RID: 2988
		// (add) Token: 0x060065AF RID: 26031
		// (remove) Token: 0x060065B0 RID: 26032
		public virtual extern event HTMLElementEvents2_onmousedownEventHandler HTMLElementEvents2_Event_onmousedown;

		// Token: 0x14000BAD RID: 2989
		// (add) Token: 0x060065B1 RID: 26033
		// (remove) Token: 0x060065B2 RID: 26034
		public virtual extern event HTMLElementEvents2_onmouseupEventHandler HTMLElementEvents2_Event_onmouseup;

		// Token: 0x14000BAE RID: 2990
		// (add) Token: 0x060065B3 RID: 26035
		// (remove) Token: 0x060065B4 RID: 26036
		public virtual extern event HTMLElementEvents2_onselectstartEventHandler HTMLElementEvents2_Event_onselectstart;

		// Token: 0x14000BAF RID: 2991
		// (add) Token: 0x060065B5 RID: 26037
		// (remove) Token: 0x060065B6 RID: 26038
		public virtual extern event HTMLElementEvents2_onfilterchangeEventHandler HTMLElementEvents2_Event_onfilterchange;

		// Token: 0x14000BB0 RID: 2992
		// (add) Token: 0x060065B7 RID: 26039
		// (remove) Token: 0x060065B8 RID: 26040
		public virtual extern event HTMLElementEvents2_ondragstartEventHandler HTMLElementEvents2_Event_ondragstart;

		// Token: 0x14000BB1 RID: 2993
		// (add) Token: 0x060065B9 RID: 26041
		// (remove) Token: 0x060065BA RID: 26042
		public virtual extern event HTMLElementEvents2_onbeforeupdateEventHandler HTMLElementEvents2_Event_onbeforeupdate;

		// Token: 0x14000BB2 RID: 2994
		// (add) Token: 0x060065BB RID: 26043
		// (remove) Token: 0x060065BC RID: 26044
		public virtual extern event HTMLElementEvents2_onafterupdateEventHandler HTMLElementEvents2_Event_onafterupdate;

		// Token: 0x14000BB3 RID: 2995
		// (add) Token: 0x060065BD RID: 26045
		// (remove) Token: 0x060065BE RID: 26046
		public virtual extern event HTMLElementEvents2_onerrorupdateEventHandler HTMLElementEvents2_Event_onerrorupdate;

		// Token: 0x14000BB4 RID: 2996
		// (add) Token: 0x060065BF RID: 26047
		// (remove) Token: 0x060065C0 RID: 26048
		public virtual extern event HTMLElementEvents2_onrowexitEventHandler HTMLElementEvents2_Event_onrowexit;

		// Token: 0x14000BB5 RID: 2997
		// (add) Token: 0x060065C1 RID: 26049
		// (remove) Token: 0x060065C2 RID: 26050
		public virtual extern event HTMLElementEvents2_onrowenterEventHandler HTMLElementEvents2_Event_onrowenter;

		// Token: 0x14000BB6 RID: 2998
		// (add) Token: 0x060065C3 RID: 26051
		// (remove) Token: 0x060065C4 RID: 26052
		public virtual extern event HTMLElementEvents2_ondatasetchangedEventHandler HTMLElementEvents2_Event_ondatasetchanged;

		// Token: 0x14000BB7 RID: 2999
		// (add) Token: 0x060065C5 RID: 26053
		// (remove) Token: 0x060065C6 RID: 26054
		public virtual extern event HTMLElementEvents2_ondataavailableEventHandler HTMLElementEvents2_Event_ondataavailable;

		// Token: 0x14000BB8 RID: 3000
		// (add) Token: 0x060065C7 RID: 26055
		// (remove) Token: 0x060065C8 RID: 26056
		public virtual extern event HTMLElementEvents2_ondatasetcompleteEventHandler HTMLElementEvents2_Event_ondatasetcomplete;

		// Token: 0x14000BB9 RID: 3001
		// (add) Token: 0x060065C9 RID: 26057
		// (remove) Token: 0x060065CA RID: 26058
		public virtual extern event HTMLElementEvents2_onlosecaptureEventHandler HTMLElementEvents2_Event_onlosecapture;

		// Token: 0x14000BBA RID: 3002
		// (add) Token: 0x060065CB RID: 26059
		// (remove) Token: 0x060065CC RID: 26060
		public virtual extern event HTMLElementEvents2_onpropertychangeEventHandler HTMLElementEvents2_Event_onpropertychange;

		// Token: 0x14000BBB RID: 3003
		// (add) Token: 0x060065CD RID: 26061
		// (remove) Token: 0x060065CE RID: 26062
		public virtual extern event HTMLElementEvents2_onscrollEventHandler HTMLElementEvents2_Event_onscroll;

		// Token: 0x14000BBC RID: 3004
		// (add) Token: 0x060065CF RID: 26063
		// (remove) Token: 0x060065D0 RID: 26064
		public virtual extern event HTMLElementEvents2_onfocusEventHandler HTMLElementEvents2_Event_onfocus;

		// Token: 0x14000BBD RID: 3005
		// (add) Token: 0x060065D1 RID: 26065
		// (remove) Token: 0x060065D2 RID: 26066
		public virtual extern event HTMLElementEvents2_onblurEventHandler HTMLElementEvents2_Event_onblur;

		// Token: 0x14000BBE RID: 3006
		// (add) Token: 0x060065D3 RID: 26067
		// (remove) Token: 0x060065D4 RID: 26068
		public virtual extern event HTMLElementEvents2_onresizeEventHandler HTMLElementEvents2_Event_onresize;

		// Token: 0x14000BBF RID: 3007
		// (add) Token: 0x060065D5 RID: 26069
		// (remove) Token: 0x060065D6 RID: 26070
		public virtual extern event HTMLElementEvents2_ondragEventHandler HTMLElementEvents2_Event_ondrag;

		// Token: 0x14000BC0 RID: 3008
		// (add) Token: 0x060065D7 RID: 26071
		// (remove) Token: 0x060065D8 RID: 26072
		public virtual extern event HTMLElementEvents2_ondragendEventHandler HTMLElementEvents2_Event_ondragend;

		// Token: 0x14000BC1 RID: 3009
		// (add) Token: 0x060065D9 RID: 26073
		// (remove) Token: 0x060065DA RID: 26074
		public virtual extern event HTMLElementEvents2_ondragenterEventHandler HTMLElementEvents2_Event_ondragenter;

		// Token: 0x14000BC2 RID: 3010
		// (add) Token: 0x060065DB RID: 26075
		// (remove) Token: 0x060065DC RID: 26076
		public virtual extern event HTMLElementEvents2_ondragoverEventHandler HTMLElementEvents2_Event_ondragover;

		// Token: 0x14000BC3 RID: 3011
		// (add) Token: 0x060065DD RID: 26077
		// (remove) Token: 0x060065DE RID: 26078
		public virtual extern event HTMLElementEvents2_ondragleaveEventHandler HTMLElementEvents2_Event_ondragleave;

		// Token: 0x14000BC4 RID: 3012
		// (add) Token: 0x060065DF RID: 26079
		// (remove) Token: 0x060065E0 RID: 26080
		public virtual extern event HTMLElementEvents2_ondropEventHandler HTMLElementEvents2_Event_ondrop;

		// Token: 0x14000BC5 RID: 3013
		// (add) Token: 0x060065E1 RID: 26081
		// (remove) Token: 0x060065E2 RID: 26082
		public virtual extern event HTMLElementEvents2_onbeforecutEventHandler HTMLElementEvents2_Event_onbeforecut;

		// Token: 0x14000BC6 RID: 3014
		// (add) Token: 0x060065E3 RID: 26083
		// (remove) Token: 0x060065E4 RID: 26084
		public virtual extern event HTMLElementEvents2_oncutEventHandler HTMLElementEvents2_Event_oncut;

		// Token: 0x14000BC7 RID: 3015
		// (add) Token: 0x060065E5 RID: 26085
		// (remove) Token: 0x060065E6 RID: 26086
		public virtual extern event HTMLElementEvents2_onbeforecopyEventHandler HTMLElementEvents2_Event_onbeforecopy;

		// Token: 0x14000BC8 RID: 3016
		// (add) Token: 0x060065E7 RID: 26087
		// (remove) Token: 0x060065E8 RID: 26088
		public virtual extern event HTMLElementEvents2_oncopyEventHandler HTMLElementEvents2_Event_oncopy;

		// Token: 0x14000BC9 RID: 3017
		// (add) Token: 0x060065E9 RID: 26089
		// (remove) Token: 0x060065EA RID: 26090
		public virtual extern event HTMLElementEvents2_onbeforepasteEventHandler HTMLElementEvents2_Event_onbeforepaste;

		// Token: 0x14000BCA RID: 3018
		// (add) Token: 0x060065EB RID: 26091
		// (remove) Token: 0x060065EC RID: 26092
		public virtual extern event HTMLElementEvents2_onpasteEventHandler HTMLElementEvents2_Event_onpaste;

		// Token: 0x14000BCB RID: 3019
		// (add) Token: 0x060065ED RID: 26093
		// (remove) Token: 0x060065EE RID: 26094
		public virtual extern event HTMLElementEvents2_oncontextmenuEventHandler HTMLElementEvents2_Event_oncontextmenu;

		// Token: 0x14000BCC RID: 3020
		// (add) Token: 0x060065EF RID: 26095
		// (remove) Token: 0x060065F0 RID: 26096
		public virtual extern event HTMLElementEvents2_onrowsdeleteEventHandler HTMLElementEvents2_Event_onrowsdelete;

		// Token: 0x14000BCD RID: 3021
		// (add) Token: 0x060065F1 RID: 26097
		// (remove) Token: 0x060065F2 RID: 26098
		public virtual extern event HTMLElementEvents2_onrowsinsertedEventHandler HTMLElementEvents2_Event_onrowsinserted;

		// Token: 0x14000BCE RID: 3022
		// (add) Token: 0x060065F3 RID: 26099
		// (remove) Token: 0x060065F4 RID: 26100
		public virtual extern event HTMLElementEvents2_oncellchangeEventHandler HTMLElementEvents2_Event_oncellchange;

		// Token: 0x14000BCF RID: 3023
		// (add) Token: 0x060065F5 RID: 26101
		// (remove) Token: 0x060065F6 RID: 26102
		public virtual extern event HTMLElementEvents2_onreadystatechangeEventHandler HTMLElementEvents2_Event_onreadystatechange;

		// Token: 0x14000BD0 RID: 3024
		// (add) Token: 0x060065F7 RID: 26103
		// (remove) Token: 0x060065F8 RID: 26104
		public virtual extern event HTMLElementEvents2_onlayoutcompleteEventHandler HTMLElementEvents2_Event_onlayoutcomplete;

		// Token: 0x14000BD1 RID: 3025
		// (add) Token: 0x060065F9 RID: 26105
		// (remove) Token: 0x060065FA RID: 26106
		public virtual extern event HTMLElementEvents2_onpageEventHandler HTMLElementEvents2_Event_onpage;

		// Token: 0x14000BD2 RID: 3026
		// (add) Token: 0x060065FB RID: 26107
		// (remove) Token: 0x060065FC RID: 26108
		public virtual extern event HTMLElementEvents2_onmouseenterEventHandler HTMLElementEvents2_Event_onmouseenter;

		// Token: 0x14000BD3 RID: 3027
		// (add) Token: 0x060065FD RID: 26109
		// (remove) Token: 0x060065FE RID: 26110
		public virtual extern event HTMLElementEvents2_onmouseleaveEventHandler HTMLElementEvents2_Event_onmouseleave;

		// Token: 0x14000BD4 RID: 3028
		// (add) Token: 0x060065FF RID: 26111
		// (remove) Token: 0x06006600 RID: 26112
		public virtual extern event HTMLElementEvents2_onactivateEventHandler HTMLElementEvents2_Event_onactivate;

		// Token: 0x14000BD5 RID: 3029
		// (add) Token: 0x06006601 RID: 26113
		// (remove) Token: 0x06006602 RID: 26114
		public virtual extern event HTMLElementEvents2_ondeactivateEventHandler HTMLElementEvents2_Event_ondeactivate;

		// Token: 0x14000BD6 RID: 3030
		// (add) Token: 0x06006603 RID: 26115
		// (remove) Token: 0x06006604 RID: 26116
		public virtual extern event HTMLElementEvents2_onbeforedeactivateEventHandler HTMLElementEvents2_Event_onbeforedeactivate;

		// Token: 0x14000BD7 RID: 3031
		// (add) Token: 0x06006605 RID: 26117
		// (remove) Token: 0x06006606 RID: 26118
		public virtual extern event HTMLElementEvents2_onbeforeactivateEventHandler HTMLElementEvents2_Event_onbeforeactivate;

		// Token: 0x14000BD8 RID: 3032
		// (add) Token: 0x06006607 RID: 26119
		// (remove) Token: 0x06006608 RID: 26120
		public virtual extern event HTMLElementEvents2_onfocusinEventHandler HTMLElementEvents2_Event_onfocusin;

		// Token: 0x14000BD9 RID: 3033
		// (add) Token: 0x06006609 RID: 26121
		// (remove) Token: 0x0600660A RID: 26122
		public virtual extern event HTMLElementEvents2_onfocusoutEventHandler HTMLElementEvents2_Event_onfocusout;

		// Token: 0x14000BDA RID: 3034
		// (add) Token: 0x0600660B RID: 26123
		// (remove) Token: 0x0600660C RID: 26124
		public virtual extern event HTMLElementEvents2_onmoveEventHandler HTMLElementEvents2_Event_onmove;

		// Token: 0x14000BDB RID: 3035
		// (add) Token: 0x0600660D RID: 26125
		// (remove) Token: 0x0600660E RID: 26126
		public virtual extern event HTMLElementEvents2_oncontrolselectEventHandler HTMLElementEvents2_Event_oncontrolselect;

		// Token: 0x14000BDC RID: 3036
		// (add) Token: 0x0600660F RID: 26127
		// (remove) Token: 0x06006610 RID: 26128
		public virtual extern event HTMLElementEvents2_onmovestartEventHandler HTMLElementEvents2_Event_onmovestart;

		// Token: 0x14000BDD RID: 3037
		// (add) Token: 0x06006611 RID: 26129
		// (remove) Token: 0x06006612 RID: 26130
		public virtual extern event HTMLElementEvents2_onmoveendEventHandler HTMLElementEvents2_Event_onmoveend;

		// Token: 0x14000BDE RID: 3038
		// (add) Token: 0x06006613 RID: 26131
		// (remove) Token: 0x06006614 RID: 26132
		public virtual extern event HTMLElementEvents2_onresizestartEventHandler HTMLElementEvents2_Event_onresizestart;

		// Token: 0x14000BDF RID: 3039
		// (add) Token: 0x06006615 RID: 26133
		// (remove) Token: 0x06006616 RID: 26134
		public virtual extern event HTMLElementEvents2_onresizeendEventHandler HTMLElementEvents2_Event_onresizeend;

		// Token: 0x14000BE0 RID: 3040
		// (add) Token: 0x06006617 RID: 26135
		// (remove) Token: 0x06006618 RID: 26136
		public virtual extern event HTMLElementEvents2_onmousewheelEventHandler HTMLElementEvents2_Event_onmousewheel;
	}
}
