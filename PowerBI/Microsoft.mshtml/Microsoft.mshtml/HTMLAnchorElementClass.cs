using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200039A RID: 922
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLAnchorEvents\0mshtml.HTMLAnchorEvents2\0\0")]
	[TypeLibType(2)]
	[Guid("3050F248-98B5-11CF-BB82-00AA00BDCE0B")]
	[DefaultMember("href")]
	[ComImport]
	public class HTMLAnchorElementClass : DispHTMLAnchorElement, HTMLAnchorElement, HTMLAnchorEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLAnchorElement, IHTMLAnchorElement2, HTMLAnchorEvents2_Event
	{
		// Token: 0x06003C0F RID: 15375
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLAnchorElementClass();

		// Token: 0x06003C10 RID: 15376
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06003C11 RID: 15377
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06003C12 RID: 15378
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x06003C14 RID: 15380
		// (set) Token: 0x06003C13 RID: 15379
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

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x06003C16 RID: 15382
		// (set) Token: 0x06003C15 RID: 15381
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

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06003C17 RID: 15383
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x06003C18 RID: 15384
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x06003C19 RID: 15385
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06003C1B RID: 15387
		// (set) Token: 0x06003C1A RID: 15386
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

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06003C1D RID: 15389
		// (set) Token: 0x06003C1C RID: 15388
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

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06003C1F RID: 15391
		// (set) Token: 0x06003C1E RID: 15390
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

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x06003C21 RID: 15393
		// (set) Token: 0x06003C20 RID: 15392
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

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x06003C23 RID: 15395
		// (set) Token: 0x06003C22 RID: 15394
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

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x06003C25 RID: 15397
		// (set) Token: 0x06003C24 RID: 15396
		[DispId(-2147412105)]
		public virtual extern object onkeypress
		{
			[DispId(-2147412105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x06003C27 RID: 15399
		// (set) Token: 0x06003C26 RID: 15398
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x06003C29 RID: 15401
		// (set) Token: 0x06003C28 RID: 15400
		[DispId(-2147412112)]
		public virtual extern object onmouseover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06003C2B RID: 15403
		// (set) Token: 0x06003C2A RID: 15402
		[DispId(-2147412108)]
		public virtual extern object onmousemove
		{
			[DispId(-2147412108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06003C2D RID: 15405
		// (set) Token: 0x06003C2C RID: 15404
		[DispId(-2147412110)]
		public virtual extern object onmousedown
		{
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06003C2F RID: 15407
		// (set) Token: 0x06003C2E RID: 15406
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

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06003C30 RID: 15408
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x06003C32 RID: 15410
		// (set) Token: 0x06003C31 RID: 15409
		[DispId(-2147418043)]
		public virtual extern string title
		{
			[DispId(-2147418043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x06003C34 RID: 15412
		// (set) Token: 0x06003C33 RID: 15411
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x06003C36 RID: 15414
		// (set) Token: 0x06003C35 RID: 15413
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

		// Token: 0x06003C37 RID: 15415
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06003C38 RID: 15416
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06003C39 RID: 15417
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06003C3A RID: 15418
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06003C3C RID: 15420
		// (set) Token: 0x06003C3B RID: 15419
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

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x06003C3D RID: 15421
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06003C3E RID: 15422
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06003C3F RID: 15423
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x06003C40 RID: 15424
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x06003C41 RID: 15425
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x06003C43 RID: 15427
		// (set) Token: 0x06003C42 RID: 15426
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

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x06003C45 RID: 15429
		// (set) Token: 0x06003C44 RID: 15428
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

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x06003C47 RID: 15431
		// (set) Token: 0x06003C46 RID: 15430
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

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x06003C49 RID: 15433
		// (set) Token: 0x06003C48 RID: 15432
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

		// Token: 0x06003C4A RID: 15434
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06003C4B RID: 15435
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06003C4C RID: 15436
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x06003C4D RID: 15437
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06003C4E RID: 15438
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x06003C4F RID: 15439
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x06003C51 RID: 15441
		// (set) Token: 0x06003C50 RID: 15440
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

		// Token: 0x06003C52 RID: 15442
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06003C54 RID: 15444
		// (set) Token: 0x06003C53 RID: 15443
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

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06003C56 RID: 15446
		// (set) Token: 0x06003C55 RID: 15445
		[DispId(-2147412090)]
		public virtual extern object onafterupdate
		{
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06003C58 RID: 15448
		// (set) Token: 0x06003C57 RID: 15447
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

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06003C5A RID: 15450
		// (set) Token: 0x06003C59 RID: 15449
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

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06003C5C RID: 15452
		// (set) Token: 0x06003C5B RID: 15451
		[DispId(-2147412093)]
		public virtual extern object onrowenter
		{
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x06003C5E RID: 15454
		// (set) Token: 0x06003C5D RID: 15453
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

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x06003C60 RID: 15456
		// (set) Token: 0x06003C5F RID: 15455
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

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x06003C62 RID: 15458
		// (set) Token: 0x06003C61 RID: 15457
		[DispId(-2147412070)]
		public virtual extern object ondatasetcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412070)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x06003C64 RID: 15460
		// (set) Token: 0x06003C63 RID: 15459
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

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06003C65 RID: 15461
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06003C66 RID: 15462
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06003C67 RID: 15463
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06003C68 RID: 15464
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06003C69 RID: 15465
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06003C6B RID: 15467
		// (set) Token: 0x06003C6A RID: 15466
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

		// Token: 0x06003C6C RID: 15468
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06003C6D RID: 15469
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06003C6F RID: 15471
		// (set) Token: 0x06003C6E RID: 15470
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

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06003C71 RID: 15473
		// (set) Token: 0x06003C70 RID: 15472
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

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06003C73 RID: 15475
		// (set) Token: 0x06003C72 RID: 15474
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

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06003C75 RID: 15477
		// (set) Token: 0x06003C74 RID: 15476
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

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06003C77 RID: 15479
		// (set) Token: 0x06003C76 RID: 15478
		[DispId(-2147412060)]
		public virtual extern object ondragover
		{
			[DispId(-2147412060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06003C79 RID: 15481
		// (set) Token: 0x06003C78 RID: 15480
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

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06003C7B RID: 15483
		// (set) Token: 0x06003C7A RID: 15482
		[DispId(-2147412058)]
		public virtual extern object ondrop
		{
			[TypeLibFunc(20)]
			[DispId(-2147412058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06003C7D RID: 15485
		// (set) Token: 0x06003C7C RID: 15484
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

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06003C7F RID: 15487
		// (set) Token: 0x06003C7E RID: 15486
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06003C81 RID: 15489
		// (set) Token: 0x06003C80 RID: 15488
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

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06003C83 RID: 15491
		// (set) Token: 0x06003C82 RID: 15490
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

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06003C85 RID: 15493
		// (set) Token: 0x06003C84 RID: 15492
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

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06003C87 RID: 15495
		// (set) Token: 0x06003C86 RID: 15494
		[DispId(-2147412055)]
		public virtual extern object onpaste
		{
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06003C88 RID: 15496
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06003C8A RID: 15498
		// (set) Token: 0x06003C89 RID: 15497
		[DispId(-2147412065)]
		public virtual extern object onpropertychange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06003C8B RID: 15499
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06003C8C RID: 15500
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06003C8D RID: 15501
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06003C8E RID: 15502
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06003C8F RID: 15503
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x06003C91 RID: 15505
		// (set) Token: 0x06003C90 RID: 15504
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

		// Token: 0x06003C92 RID: 15506
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x06003C94 RID: 15508
		// (set) Token: 0x06003C93 RID: 15507
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

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x06003C96 RID: 15510
		// (set) Token: 0x06003C95 RID: 15509
		[DispId(-2147412097)]
		public virtual extern object onblur
		{
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x06003C98 RID: 15512
		// (set) Token: 0x06003C97 RID: 15511
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06003C9A RID: 15514
		// (set) Token: 0x06003C99 RID: 15513
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

		// Token: 0x06003C9B RID: 15515
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06003C9C RID: 15516
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06003C9D RID: 15517
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06003C9E RID: 15518
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06003C9F RID: 15519
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06003CA0 RID: 15520
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x06003CA1 RID: 15521
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06003CA2 RID: 15522
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06003CA3 RID: 15523
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x06003CA4 RID: 15524
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x06003CA6 RID: 15526
		// (set) Token: 0x06003CA5 RID: 15525
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

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x06003CA8 RID: 15528
		// (set) Token: 0x06003CA7 RID: 15527
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

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x06003CAA RID: 15530
		// (set) Token: 0x06003CA9 RID: 15529
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

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x06003CAC RID: 15532
		// (set) Token: 0x06003CAB RID: 15531
		[DispId(-2147412048)]
		public virtual extern object oncellchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x06003CAE RID: 15534
		// (set) Token: 0x06003CAD RID: 15533
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

		// Token: 0x06003CAF RID: 15535
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x06003CB0 RID: 15536
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x06003CB1 RID: 15537
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x06003CB3 RID: 15539
		// (set) Token: 0x06003CB2 RID: 15538
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

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x06003CB5 RID: 15541
		// (set) Token: 0x06003CB4 RID: 15540
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

		// Token: 0x06003CB6 RID: 15542
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x06003CB8 RID: 15544
		// (set) Token: 0x06003CB7 RID: 15543
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

		// Token: 0x06003CB9 RID: 15545
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06003CBA RID: 15546
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06003CBB RID: 15547
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06003CBC RID: 15548
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x06003CBD RID: 15549
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06003CBE RID: 15550
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06003CBF RID: 15551
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x06003CC0 RID: 15552
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x06003CC1 RID: 15553
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x06003CC3 RID: 15555
		// (set) Token: 0x06003CC2 RID: 15554
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

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x06003CC5 RID: 15557
		// (set) Token: 0x06003CC4 RID: 15556
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

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x06003CC6 RID: 15558
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06003CC7 RID: 15559
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06003CC8 RID: 15560
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x06003CC9 RID: 15561
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x06003CCA RID: 15562
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x06003CCC RID: 15564
		// (set) Token: 0x06003CCB RID: 15563
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

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x06003CCE RID: 15566
		// (set) Token: 0x06003CCD RID: 15565
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

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x06003CD0 RID: 15568
		// (set) Token: 0x06003CCF RID: 15567
		[DispId(-2147417012)]
		public virtual extern bool inflateBlock
		{
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x06003CD2 RID: 15570
		// (set) Token: 0x06003CD1 RID: 15569
		[DispId(-2147412035)]
		public virtual extern object onbeforedeactivate
		{
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06003CD3 RID: 15571
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06003CD5 RID: 15573
		// (set) Token: 0x06003CD4 RID: 15572
		[DispId(-2147412950)]
		public virtual extern string contentEditable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412950)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06003CD6 RID: 15574
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x06003CD8 RID: 15576
		// (set) Token: 0x06003CD7 RID: 15575
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

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x06003CDA RID: 15578
		// (set) Token: 0x06003CD9 RID: 15577
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

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x06003CDB RID: 15579
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x06003CDD RID: 15581
		// (set) Token: 0x06003CDC RID: 15580
		[DispId(-2147412034)]
		public virtual extern object onmove
		{
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x06003CDF RID: 15583
		// (set) Token: 0x06003CDE RID: 15582
		[DispId(-2147412033)]
		public virtual extern object oncontrolselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06003CE0 RID: 15584
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x06003CE2 RID: 15586
		// (set) Token: 0x06003CE1 RID: 15585
		[DispId(-2147412029)]
		public virtual extern object onresizestart
		{
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x06003CE4 RID: 15588
		// (set) Token: 0x06003CE3 RID: 15587
		[DispId(-2147412028)]
		public virtual extern object onresizeend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x06003CE6 RID: 15590
		// (set) Token: 0x06003CE5 RID: 15589
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

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x06003CE8 RID: 15592
		// (set) Token: 0x06003CE7 RID: 15591
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

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x06003CEA RID: 15594
		// (set) Token: 0x06003CE9 RID: 15593
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

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x06003CEC RID: 15596
		// (set) Token: 0x06003CEB RID: 15595
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

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06003CEE RID: 15598
		// (set) Token: 0x06003CED RID: 15597
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

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06003CF0 RID: 15600
		// (set) Token: 0x06003CEF RID: 15599
		[DispId(-2147412024)]
		public virtual extern object ondeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06003CF1 RID: 15601
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06003CF2 RID: 15602
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06003CF4 RID: 15604
		// (set) Token: 0x06003CF3 RID: 15603
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

		// Token: 0x06003CF5 RID: 15605
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06003CF6 RID: 15606
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06003CF7 RID: 15607
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06003CF8 RID: 15608
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06003CFA RID: 15610
		// (set) Token: 0x06003CF9 RID: 15609
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

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x06003CFC RID: 15612
		// (set) Token: 0x06003CFB RID: 15611
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

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06003CFE RID: 15614
		// (set) Token: 0x06003CFD RID: 15613
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

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06003CFF RID: 15615
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06003D00 RID: 15616
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06003D01 RID: 15617
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06003D02 RID: 15618
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06003D03 RID: 15619
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06003D04 RID: 15620
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x06003D05 RID: 15621
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06003D06 RID: 15622
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06003D07 RID: 15623
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06003D08 RID: 15624
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06003D09 RID: 15625
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06003D0A RID: 15626
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06003D0B RID: 15627
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06003D0C RID: 15628
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06003D0D RID: 15629
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06003D0E RID: 15630
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06003D10 RID: 15632
		// (set) Token: 0x06003D0F RID: 15631
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

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06003D11 RID: 15633
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06003D12 RID: 15634
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06003D13 RID: 15635
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06003D14 RID: 15636
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06003D15 RID: 15637
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06003D17 RID: 15639
		// (set) Token: 0x06003D16 RID: 15638
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

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06003D19 RID: 15641
		// (set) Token: 0x06003D18 RID: 15640
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

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06003D1B RID: 15643
		// (set) Token: 0x06003D1A RID: 15642
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

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06003D1D RID: 15645
		// (set) Token: 0x06003D1C RID: 15644
		[DispId(0)]
		[IndexerName("href")]
		public virtual extern string href
		{
			[DispId(0)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(0)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06003D1F RID: 15647
		// (set) Token: 0x06003D1E RID: 15646
		[DispId(1003)]
		public virtual extern string target
		{
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06003D21 RID: 15649
		// (set) Token: 0x06003D20 RID: 15648
		[DispId(1005)]
		public virtual extern string rel
		{
			[TypeLibFunc(20)]
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06003D23 RID: 15651
		// (set) Token: 0x06003D22 RID: 15650
		[DispId(1006)]
		public virtual extern string rev
		{
			[DispId(1006)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06003D25 RID: 15653
		// (set) Token: 0x06003D24 RID: 15652
		[DispId(1007)]
		public virtual extern string urn
		{
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06003D27 RID: 15655
		// (set) Token: 0x06003D26 RID: 15654
		[DispId(1008)]
		public virtual extern string Methods
		{
			[TypeLibFunc(20)]
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1008)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x06003D29 RID: 15657
		// (set) Token: 0x06003D28 RID: 15656
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06003D2B RID: 15659
		// (set) Token: 0x06003D2A RID: 15658
		[DispId(1012)]
		public virtual extern string host
		{
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x06003D2D RID: 15661
		// (set) Token: 0x06003D2C RID: 15660
		[DispId(1013)]
		public virtual extern string hostname
		{
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06003D2F RID: 15663
		// (set) Token: 0x06003D2E RID: 15662
		[DispId(1014)]
		public virtual extern string pathname
		{
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x06003D31 RID: 15665
		// (set) Token: 0x06003D30 RID: 15664
		[DispId(1015)]
		public virtual extern string port
		{
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06003D33 RID: 15667
		// (set) Token: 0x06003D32 RID: 15666
		[DispId(1016)]
		public virtual extern string protocol
		{
			[DispId(1016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06003D35 RID: 15669
		// (set) Token: 0x06003D34 RID: 15668
		[DispId(1017)]
		public virtual extern string search
		{
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06003D37 RID: 15671
		// (set) Token: 0x06003D36 RID: 15670
		[DispId(1018)]
		public virtual extern string hash
		{
			[DispId(1018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06003D38 RID: 15672
		[DispId(1031)]
		public virtual extern string protocolLong
		{
			[DispId(1031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06003D39 RID: 15673
		[DispId(1030)]
		public virtual extern string mimeType
		{
			[DispId(1030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x06003D3A RID: 15674
		[DispId(1032)]
		public virtual extern string nameProp
		{
			[DispId(1032)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06003D3C RID: 15676
		// (set) Token: 0x06003D3B RID: 15675
		[DispId(1023)]
		public virtual extern string charset
		{
			[TypeLibFunc(20)]
			[DispId(1023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1023)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06003D3E RID: 15678
		// (set) Token: 0x06003D3D RID: 15677
		[DispId(1024)]
		public virtual extern string coords
		{
			[TypeLibFunc(20)]
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x06003D40 RID: 15680
		// (set) Token: 0x06003D3F RID: 15679
		[DispId(1025)]
		public virtual extern string hreflang
		{
			[TypeLibFunc(20)]
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x06003D42 RID: 15682
		// (set) Token: 0x06003D41 RID: 15681
		[DispId(1026)]
		public virtual extern string shape
		{
			[TypeLibFunc(20)]
			[DispId(1026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x06003D44 RID: 15684
		// (set) Token: 0x06003D43 RID: 15683
		[DispId(1027)]
		public virtual extern string type
		{
			[TypeLibFunc(20)]
			[DispId(1027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1027)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06003D45 RID: 15685
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06003D46 RID: 15686
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06003D47 RID: 15687
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x06003D49 RID: 15689
		// (set) Token: 0x06003D48 RID: 15688
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

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x06003D4B RID: 15691
		// (set) Token: 0x06003D4A RID: 15690
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

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06003D4C RID: 15692
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06003D4D RID: 15693
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06003D4E RID: 15694
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06003D50 RID: 15696
		// (set) Token: 0x06003D4F RID: 15695
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

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x06003D52 RID: 15698
		// (set) Token: 0x06003D51 RID: 15697
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

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06003D54 RID: 15700
		// (set) Token: 0x06003D53 RID: 15699
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

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06003D56 RID: 15702
		// (set) Token: 0x06003D55 RID: 15701
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

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x06003D58 RID: 15704
		// (set) Token: 0x06003D57 RID: 15703
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

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x06003D5A RID: 15706
		// (set) Token: 0x06003D59 RID: 15705
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

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06003D5C RID: 15708
		// (set) Token: 0x06003D5B RID: 15707
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

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06003D5E RID: 15710
		// (set) Token: 0x06003D5D RID: 15709
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

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06003D60 RID: 15712
		// (set) Token: 0x06003D5F RID: 15711
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

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06003D62 RID: 15714
		// (set) Token: 0x06003D61 RID: 15713
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

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x06003D64 RID: 15716
		// (set) Token: 0x06003D63 RID: 15715
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

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x06003D65 RID: 15717
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06003D67 RID: 15719
		// (set) Token: 0x06003D66 RID: 15718
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

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06003D69 RID: 15721
		// (set) Token: 0x06003D68 RID: 15720
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

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06003D6B RID: 15723
		// (set) Token: 0x06003D6A RID: 15722
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

		// Token: 0x06003D6C RID: 15724
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06003D6D RID: 15725
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06003D6E RID: 15726
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06003D6F RID: 15727
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06003D71 RID: 15729
		// (set) Token: 0x06003D70 RID: 15728
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

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x06003D72 RID: 15730
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06003D73 RID: 15731
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x06003D74 RID: 15732
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x06003D75 RID: 15733
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x06003D76 RID: 15734
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x06003D78 RID: 15736
		// (set) Token: 0x06003D77 RID: 15735
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

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x06003D7A RID: 15738
		// (set) Token: 0x06003D79 RID: 15737
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

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x06003D7C RID: 15740
		// (set) Token: 0x06003D7B RID: 15739
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

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06003D7E RID: 15742
		// (set) Token: 0x06003D7D RID: 15741
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

		// Token: 0x06003D7F RID: 15743
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06003D80 RID: 15744
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x06003D81 RID: 15745
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x06003D82 RID: 15746
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06003D83 RID: 15747
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x06003D84 RID: 15748
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x06003D86 RID: 15750
		// (set) Token: 0x06003D85 RID: 15749
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

		// Token: 0x06003D87 RID: 15751
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x06003D89 RID: 15753
		// (set) Token: 0x06003D88 RID: 15752
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

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x06003D8B RID: 15755
		// (set) Token: 0x06003D8A RID: 15754
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

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x06003D8D RID: 15757
		// (set) Token: 0x06003D8C RID: 15756
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

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x06003D8F RID: 15759
		// (set) Token: 0x06003D8E RID: 15758
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

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06003D91 RID: 15761
		// (set) Token: 0x06003D90 RID: 15760
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

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06003D93 RID: 15763
		// (set) Token: 0x06003D92 RID: 15762
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

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x06003D95 RID: 15765
		// (set) Token: 0x06003D94 RID: 15764
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

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x06003D97 RID: 15767
		// (set) Token: 0x06003D96 RID: 15766
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

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x06003D99 RID: 15769
		// (set) Token: 0x06003D98 RID: 15768
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

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06003D9A RID: 15770
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06003D9B RID: 15771
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06003D9C RID: 15772
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06003D9D RID: 15773
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06003D9E RID: 15774
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06003DA0 RID: 15776
		// (set) Token: 0x06003D9F RID: 15775
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

		// Token: 0x06003DA1 RID: 15777
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06003DA2 RID: 15778
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x06003DA4 RID: 15780
		// (set) Token: 0x06003DA3 RID: 15779
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

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x06003DA6 RID: 15782
		// (set) Token: 0x06003DA5 RID: 15781
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

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x06003DA8 RID: 15784
		// (set) Token: 0x06003DA7 RID: 15783
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

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x06003DAA RID: 15786
		// (set) Token: 0x06003DA9 RID: 15785
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

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x06003DAC RID: 15788
		// (set) Token: 0x06003DAB RID: 15787
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

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x06003DAE RID: 15790
		// (set) Token: 0x06003DAD RID: 15789
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

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x06003DB0 RID: 15792
		// (set) Token: 0x06003DAF RID: 15791
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

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x06003DB2 RID: 15794
		// (set) Token: 0x06003DB1 RID: 15793
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

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x06003DB4 RID: 15796
		// (set) Token: 0x06003DB3 RID: 15795
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

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x06003DB6 RID: 15798
		// (set) Token: 0x06003DB5 RID: 15797
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

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x06003DB8 RID: 15800
		// (set) Token: 0x06003DB7 RID: 15799
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

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x06003DBA RID: 15802
		// (set) Token: 0x06003DB9 RID: 15801
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

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06003DBC RID: 15804
		// (set) Token: 0x06003DBB RID: 15803
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

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x06003DBD RID: 15805
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x06003DBF RID: 15807
		// (set) Token: 0x06003DBE RID: 15806
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

		// Token: 0x06003DC0 RID: 15808
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06003DC1 RID: 15809
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06003DC2 RID: 15810
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06003DC3 RID: 15811
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06003DC4 RID: 15812
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x06003DC6 RID: 15814
		// (set) Token: 0x06003DC5 RID: 15813
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

		// Token: 0x06003DC7 RID: 15815
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06003DC9 RID: 15817
		// (set) Token: 0x06003DC8 RID: 15816
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

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x06003DCB RID: 15819
		// (set) Token: 0x06003DCA RID: 15818
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

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x06003DCD RID: 15821
		// (set) Token: 0x06003DCC RID: 15820
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

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x06003DCF RID: 15823
		// (set) Token: 0x06003DCE RID: 15822
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

		// Token: 0x06003DD0 RID: 15824
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06003DD1 RID: 15825
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06003DD2 RID: 15826
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x06003DD3 RID: 15827
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x06003DD4 RID: 15828
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x06003DD5 RID: 15829
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06003DD6 RID: 15830
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06003DD7 RID: 15831
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06003DD8 RID: 15832
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x06003DD9 RID: 15833
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06003DDB RID: 15835
		// (set) Token: 0x06003DDA RID: 15834
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

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x06003DDD RID: 15837
		// (set) Token: 0x06003DDC RID: 15836
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

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x06003DDF RID: 15839
		// (set) Token: 0x06003DDE RID: 15838
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

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x06003DE1 RID: 15841
		// (set) Token: 0x06003DE0 RID: 15840
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

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x06003DE3 RID: 15843
		// (set) Token: 0x06003DE2 RID: 15842
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

		// Token: 0x06003DE4 RID: 15844
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x06003DE5 RID: 15845
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x06003DE6 RID: 15846
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06003DE8 RID: 15848
		// (set) Token: 0x06003DE7 RID: 15847
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

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x06003DEA RID: 15850
		// (set) Token: 0x06003DE9 RID: 15849
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

		// Token: 0x06003DEB RID: 15851
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06003DEC RID: 15852
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x06003DEE RID: 15854
		// (set) Token: 0x06003DED RID: 15853
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

		// Token: 0x06003DEF RID: 15855
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06003DF0 RID: 15856
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06003DF1 RID: 15857
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06003DF2 RID: 15858
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x06003DF3 RID: 15859
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06003DF4 RID: 15860
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06003DF5 RID: 15861
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x06003DF6 RID: 15862
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x06003DF7 RID: 15863
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x06003DF9 RID: 15865
		// (set) Token: 0x06003DF8 RID: 15864
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

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06003DFB RID: 15867
		// (set) Token: 0x06003DFA RID: 15866
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

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06003DFC RID: 15868
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06003DFD RID: 15869
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06003DFE RID: 15870
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06003DFF RID: 15871
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x06003E00 RID: 15872
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x06003E02 RID: 15874
		// (set) Token: 0x06003E01 RID: 15873
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

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06003E04 RID: 15876
		// (set) Token: 0x06003E03 RID: 15875
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

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06003E06 RID: 15878
		// (set) Token: 0x06003E05 RID: 15877
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

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06003E08 RID: 15880
		// (set) Token: 0x06003E07 RID: 15879
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

		// Token: 0x06003E09 RID: 15881
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06003E0B RID: 15883
		// (set) Token: 0x06003E0A RID: 15882
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

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06003E0C RID: 15884
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06003E0E RID: 15886
		// (set) Token: 0x06003E0D RID: 15885
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

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06003E10 RID: 15888
		// (set) Token: 0x06003E0F RID: 15887
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

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06003E11 RID: 15889
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06003E13 RID: 15891
		// (set) Token: 0x06003E12 RID: 15890
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

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06003E15 RID: 15893
		// (set) Token: 0x06003E14 RID: 15892
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

		// Token: 0x06003E16 RID: 15894
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06003E18 RID: 15896
		// (set) Token: 0x06003E17 RID: 15895
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

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06003E1A RID: 15898
		// (set) Token: 0x06003E19 RID: 15897
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

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06003E1C RID: 15900
		// (set) Token: 0x06003E1B RID: 15899
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

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06003E1E RID: 15902
		// (set) Token: 0x06003E1D RID: 15901
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

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06003E20 RID: 15904
		// (set) Token: 0x06003E1F RID: 15903
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

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06003E22 RID: 15906
		// (set) Token: 0x06003E21 RID: 15905
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

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06003E24 RID: 15908
		// (set) Token: 0x06003E23 RID: 15907
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

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06003E26 RID: 15910
		// (set) Token: 0x06003E25 RID: 15909
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

		// Token: 0x06003E27 RID: 15911
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06003E28 RID: 15912
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06003E2A RID: 15914
		// (set) Token: 0x06003E29 RID: 15913
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

		// Token: 0x06003E2B RID: 15915
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06003E2C RID: 15916
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06003E2D RID: 15917
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06003E2E RID: 15918
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06003E30 RID: 15920
		// (set) Token: 0x06003E2F RID: 15919
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

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06003E32 RID: 15922
		// (set) Token: 0x06003E31 RID: 15921
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

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06003E34 RID: 15924
		// (set) Token: 0x06003E33 RID: 15923
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

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06003E35 RID: 15925
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06003E36 RID: 15926
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06003E37 RID: 15927
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06003E38 RID: 15928
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06003E39 RID: 15929
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06003E3A RID: 15930
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06003E3B RID: 15931
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06003E3C RID: 15932
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06003E3D RID: 15933
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06003E3E RID: 15934
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06003E3F RID: 15935
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06003E40 RID: 15936
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06003E41 RID: 15937
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06003E42 RID: 15938
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06003E43 RID: 15939
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06003E44 RID: 15940
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06003E46 RID: 15942
		// (set) Token: 0x06003E45 RID: 15941
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

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06003E47 RID: 15943
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06003E48 RID: 15944
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06003E49 RID: 15945
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06003E4A RID: 15946
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06003E4B RID: 15947
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06003E4D RID: 15949
		// (set) Token: 0x06003E4C RID: 15948
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

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06003E4F RID: 15951
		// (set) Token: 0x06003E4E RID: 15950
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

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06003E51 RID: 15953
		// (set) Token: 0x06003E50 RID: 15952
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

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06003E53 RID: 15955
		// (set) Token: 0x06003E52 RID: 15954
		public virtual extern string IHTMLAnchorElement_href
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

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06003E55 RID: 15957
		// (set) Token: 0x06003E54 RID: 15956
		public virtual extern string IHTMLAnchorElement_target
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

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06003E57 RID: 15959
		// (set) Token: 0x06003E56 RID: 15958
		public virtual extern string IHTMLAnchorElement_rel
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

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06003E59 RID: 15961
		// (set) Token: 0x06003E58 RID: 15960
		public virtual extern string IHTMLAnchorElement_rev
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

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06003E5B RID: 15963
		// (set) Token: 0x06003E5A RID: 15962
		public virtual extern string IHTMLAnchorElement_urn
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

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06003E5D RID: 15965
		// (set) Token: 0x06003E5C RID: 15964
		public virtual extern string IHTMLAnchorElement_Methods
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

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06003E5F RID: 15967
		// (set) Token: 0x06003E5E RID: 15966
		public virtual extern string IHTMLAnchorElement_name
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

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06003E61 RID: 15969
		// (set) Token: 0x06003E60 RID: 15968
		public virtual extern string IHTMLAnchorElement_host
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06003E63 RID: 15971
		// (set) Token: 0x06003E62 RID: 15970
		public virtual extern string IHTMLAnchorElement_hostname
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06003E65 RID: 15973
		// (set) Token: 0x06003E64 RID: 15972
		public virtual extern string IHTMLAnchorElement_pathname
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06003E67 RID: 15975
		// (set) Token: 0x06003E66 RID: 15974
		public virtual extern string IHTMLAnchorElement_port
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06003E69 RID: 15977
		// (set) Token: 0x06003E68 RID: 15976
		public virtual extern string IHTMLAnchorElement_protocol
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06003E6B RID: 15979
		// (set) Token: 0x06003E6A RID: 15978
		public virtual extern string IHTMLAnchorElement_search
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06003E6D RID: 15981
		// (set) Token: 0x06003E6C RID: 15980
		public virtual extern string IHTMLAnchorElement_hash
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06003E6F RID: 15983
		// (set) Token: 0x06003E6E RID: 15982
		public virtual extern object IHTMLAnchorElement_onblur
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

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06003E71 RID: 15985
		// (set) Token: 0x06003E70 RID: 15984
		public virtual extern object IHTMLAnchorElement_onfocus
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

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06003E73 RID: 15987
		// (set) Token: 0x06003E72 RID: 15986
		public virtual extern string IHTMLAnchorElement_accessKey
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

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06003E74 RID: 15988
		public virtual extern string IHTMLAnchorElement_protocolLong
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06003E75 RID: 15989
		public virtual extern string IHTMLAnchorElement_mimeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06003E76 RID: 15990
		public virtual extern string IHTMLAnchorElement_nameProp
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06003E78 RID: 15992
		// (set) Token: 0x06003E77 RID: 15991
		public virtual extern short IHTMLAnchorElement_tabIndex
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06003E79 RID: 15993
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLAnchorElement_focus();

		// Token: 0x06003E7A RID: 15994
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLAnchorElement_blur();

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06003E7C RID: 15996
		// (set) Token: 0x06003E7B RID: 15995
		public virtual extern string IHTMLAnchorElement2_charset
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

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06003E7E RID: 15998
		// (set) Token: 0x06003E7D RID: 15997
		public virtual extern string IHTMLAnchorElement2_coords
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

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06003E80 RID: 16000
		// (set) Token: 0x06003E7F RID: 15999
		public virtual extern string IHTMLAnchorElement2_hreflang
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

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06003E82 RID: 16002
		// (set) Token: 0x06003E81 RID: 16001
		public virtual extern string IHTMLAnchorElement2_shape
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

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06003E84 RID: 16004
		// (set) Token: 0x06003E83 RID: 16003
		public virtual extern string IHTMLAnchorElement2_type
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

		// Token: 0x14000601 RID: 1537
		// (add) Token: 0x06003E85 RID: 16005
		// (remove) Token: 0x06003E86 RID: 16006
		public virtual extern event HTMLAnchorEvents_onhelpEventHandler HTMLAnchorEvents_Event_onhelp;

		// Token: 0x14000602 RID: 1538
		// (add) Token: 0x06003E87 RID: 16007
		// (remove) Token: 0x06003E88 RID: 16008
		public virtual extern event HTMLAnchorEvents_onclickEventHandler HTMLAnchorEvents_Event_onclick;

		// Token: 0x14000603 RID: 1539
		// (add) Token: 0x06003E89 RID: 16009
		// (remove) Token: 0x06003E8A RID: 16010
		public virtual extern event HTMLAnchorEvents_ondblclickEventHandler HTMLAnchorEvents_Event_ondblclick;

		// Token: 0x14000604 RID: 1540
		// (add) Token: 0x06003E8B RID: 16011
		// (remove) Token: 0x06003E8C RID: 16012
		public virtual extern event HTMLAnchorEvents_onkeypressEventHandler HTMLAnchorEvents_Event_onkeypress;

		// Token: 0x14000605 RID: 1541
		// (add) Token: 0x06003E8D RID: 16013
		// (remove) Token: 0x06003E8E RID: 16014
		public virtual extern event HTMLAnchorEvents_onkeydownEventHandler HTMLAnchorEvents_Event_onkeydown;

		// Token: 0x14000606 RID: 1542
		// (add) Token: 0x06003E8F RID: 16015
		// (remove) Token: 0x06003E90 RID: 16016
		public virtual extern event HTMLAnchorEvents_onkeyupEventHandler HTMLAnchorEvents_Event_onkeyup;

		// Token: 0x14000607 RID: 1543
		// (add) Token: 0x06003E91 RID: 16017
		// (remove) Token: 0x06003E92 RID: 16018
		public virtual extern event HTMLAnchorEvents_onmouseoutEventHandler HTMLAnchorEvents_Event_onmouseout;

		// Token: 0x14000608 RID: 1544
		// (add) Token: 0x06003E93 RID: 16019
		// (remove) Token: 0x06003E94 RID: 16020
		public virtual extern event HTMLAnchorEvents_onmouseoverEventHandler HTMLAnchorEvents_Event_onmouseover;

		// Token: 0x14000609 RID: 1545
		// (add) Token: 0x06003E95 RID: 16021
		// (remove) Token: 0x06003E96 RID: 16022
		public virtual extern event HTMLAnchorEvents_onmousemoveEventHandler HTMLAnchorEvents_Event_onmousemove;

		// Token: 0x1400060A RID: 1546
		// (add) Token: 0x06003E97 RID: 16023
		// (remove) Token: 0x06003E98 RID: 16024
		public virtual extern event HTMLAnchorEvents_onmousedownEventHandler HTMLAnchorEvents_Event_onmousedown;

		// Token: 0x1400060B RID: 1547
		// (add) Token: 0x06003E99 RID: 16025
		// (remove) Token: 0x06003E9A RID: 16026
		public virtual extern event HTMLAnchorEvents_onmouseupEventHandler HTMLAnchorEvents_Event_onmouseup;

		// Token: 0x1400060C RID: 1548
		// (add) Token: 0x06003E9B RID: 16027
		// (remove) Token: 0x06003E9C RID: 16028
		public virtual extern event HTMLAnchorEvents_onselectstartEventHandler HTMLAnchorEvents_Event_onselectstart;

		// Token: 0x1400060D RID: 1549
		// (add) Token: 0x06003E9D RID: 16029
		// (remove) Token: 0x06003E9E RID: 16030
		public virtual extern event HTMLAnchorEvents_onfilterchangeEventHandler HTMLAnchorEvents_Event_onfilterchange;

		// Token: 0x1400060E RID: 1550
		// (add) Token: 0x06003E9F RID: 16031
		// (remove) Token: 0x06003EA0 RID: 16032
		public virtual extern event HTMLAnchorEvents_ondragstartEventHandler HTMLAnchorEvents_Event_ondragstart;

		// Token: 0x1400060F RID: 1551
		// (add) Token: 0x06003EA1 RID: 16033
		// (remove) Token: 0x06003EA2 RID: 16034
		public virtual extern event HTMLAnchorEvents_onbeforeupdateEventHandler HTMLAnchorEvents_Event_onbeforeupdate;

		// Token: 0x14000610 RID: 1552
		// (add) Token: 0x06003EA3 RID: 16035
		// (remove) Token: 0x06003EA4 RID: 16036
		public virtual extern event HTMLAnchorEvents_onafterupdateEventHandler HTMLAnchorEvents_Event_onafterupdate;

		// Token: 0x14000611 RID: 1553
		// (add) Token: 0x06003EA5 RID: 16037
		// (remove) Token: 0x06003EA6 RID: 16038
		public virtual extern event HTMLAnchorEvents_onerrorupdateEventHandler HTMLAnchorEvents_Event_onerrorupdate;

		// Token: 0x14000612 RID: 1554
		// (add) Token: 0x06003EA7 RID: 16039
		// (remove) Token: 0x06003EA8 RID: 16040
		public virtual extern event HTMLAnchorEvents_onrowexitEventHandler HTMLAnchorEvents_Event_onrowexit;

		// Token: 0x14000613 RID: 1555
		// (add) Token: 0x06003EA9 RID: 16041
		// (remove) Token: 0x06003EAA RID: 16042
		public virtual extern event HTMLAnchorEvents_onrowenterEventHandler HTMLAnchorEvents_Event_onrowenter;

		// Token: 0x14000614 RID: 1556
		// (add) Token: 0x06003EAB RID: 16043
		// (remove) Token: 0x06003EAC RID: 16044
		public virtual extern event HTMLAnchorEvents_ondatasetchangedEventHandler HTMLAnchorEvents_Event_ondatasetchanged;

		// Token: 0x14000615 RID: 1557
		// (add) Token: 0x06003EAD RID: 16045
		// (remove) Token: 0x06003EAE RID: 16046
		public virtual extern event HTMLAnchorEvents_ondataavailableEventHandler HTMLAnchorEvents_Event_ondataavailable;

		// Token: 0x14000616 RID: 1558
		// (add) Token: 0x06003EAF RID: 16047
		// (remove) Token: 0x06003EB0 RID: 16048
		public virtual extern event HTMLAnchorEvents_ondatasetcompleteEventHandler HTMLAnchorEvents_Event_ondatasetcomplete;

		// Token: 0x14000617 RID: 1559
		// (add) Token: 0x06003EB1 RID: 16049
		// (remove) Token: 0x06003EB2 RID: 16050
		public virtual extern event HTMLAnchorEvents_onlosecaptureEventHandler HTMLAnchorEvents_Event_onlosecapture;

		// Token: 0x14000618 RID: 1560
		// (add) Token: 0x06003EB3 RID: 16051
		// (remove) Token: 0x06003EB4 RID: 16052
		public virtual extern event HTMLAnchorEvents_onpropertychangeEventHandler HTMLAnchorEvents_Event_onpropertychange;

		// Token: 0x14000619 RID: 1561
		// (add) Token: 0x06003EB5 RID: 16053
		// (remove) Token: 0x06003EB6 RID: 16054
		public virtual extern event HTMLAnchorEvents_onscrollEventHandler HTMLAnchorEvents_Event_onscroll;

		// Token: 0x1400061A RID: 1562
		// (add) Token: 0x06003EB7 RID: 16055
		// (remove) Token: 0x06003EB8 RID: 16056
		public virtual extern event HTMLAnchorEvents_onfocusEventHandler HTMLAnchorEvents_Event_onfocus;

		// Token: 0x1400061B RID: 1563
		// (add) Token: 0x06003EB9 RID: 16057
		// (remove) Token: 0x06003EBA RID: 16058
		public virtual extern event HTMLAnchorEvents_onblurEventHandler HTMLAnchorEvents_Event_onblur;

		// Token: 0x1400061C RID: 1564
		// (add) Token: 0x06003EBB RID: 16059
		// (remove) Token: 0x06003EBC RID: 16060
		public virtual extern event HTMLAnchorEvents_onresizeEventHandler HTMLAnchorEvents_Event_onresize;

		// Token: 0x1400061D RID: 1565
		// (add) Token: 0x06003EBD RID: 16061
		// (remove) Token: 0x06003EBE RID: 16062
		public virtual extern event HTMLAnchorEvents_ondragEventHandler HTMLAnchorEvents_Event_ondrag;

		// Token: 0x1400061E RID: 1566
		// (add) Token: 0x06003EBF RID: 16063
		// (remove) Token: 0x06003EC0 RID: 16064
		public virtual extern event HTMLAnchorEvents_ondragendEventHandler HTMLAnchorEvents_Event_ondragend;

		// Token: 0x1400061F RID: 1567
		// (add) Token: 0x06003EC1 RID: 16065
		// (remove) Token: 0x06003EC2 RID: 16066
		public virtual extern event HTMLAnchorEvents_ondragenterEventHandler HTMLAnchorEvents_Event_ondragenter;

		// Token: 0x14000620 RID: 1568
		// (add) Token: 0x06003EC3 RID: 16067
		// (remove) Token: 0x06003EC4 RID: 16068
		public virtual extern event HTMLAnchorEvents_ondragoverEventHandler HTMLAnchorEvents_Event_ondragover;

		// Token: 0x14000621 RID: 1569
		// (add) Token: 0x06003EC5 RID: 16069
		// (remove) Token: 0x06003EC6 RID: 16070
		public virtual extern event HTMLAnchorEvents_ondragleaveEventHandler HTMLAnchorEvents_Event_ondragleave;

		// Token: 0x14000622 RID: 1570
		// (add) Token: 0x06003EC7 RID: 16071
		// (remove) Token: 0x06003EC8 RID: 16072
		public virtual extern event HTMLAnchorEvents_ondropEventHandler HTMLAnchorEvents_Event_ondrop;

		// Token: 0x14000623 RID: 1571
		// (add) Token: 0x06003EC9 RID: 16073
		// (remove) Token: 0x06003ECA RID: 16074
		public virtual extern event HTMLAnchorEvents_onbeforecutEventHandler HTMLAnchorEvents_Event_onbeforecut;

		// Token: 0x14000624 RID: 1572
		// (add) Token: 0x06003ECB RID: 16075
		// (remove) Token: 0x06003ECC RID: 16076
		public virtual extern event HTMLAnchorEvents_oncutEventHandler HTMLAnchorEvents_Event_oncut;

		// Token: 0x14000625 RID: 1573
		// (add) Token: 0x06003ECD RID: 16077
		// (remove) Token: 0x06003ECE RID: 16078
		public virtual extern event HTMLAnchorEvents_onbeforecopyEventHandler HTMLAnchorEvents_Event_onbeforecopy;

		// Token: 0x14000626 RID: 1574
		// (add) Token: 0x06003ECF RID: 16079
		// (remove) Token: 0x06003ED0 RID: 16080
		public virtual extern event HTMLAnchorEvents_oncopyEventHandler HTMLAnchorEvents_Event_oncopy;

		// Token: 0x14000627 RID: 1575
		// (add) Token: 0x06003ED1 RID: 16081
		// (remove) Token: 0x06003ED2 RID: 16082
		public virtual extern event HTMLAnchorEvents_onbeforepasteEventHandler HTMLAnchorEvents_Event_onbeforepaste;

		// Token: 0x14000628 RID: 1576
		// (add) Token: 0x06003ED3 RID: 16083
		// (remove) Token: 0x06003ED4 RID: 16084
		public virtual extern event HTMLAnchorEvents_onpasteEventHandler HTMLAnchorEvents_Event_onpaste;

		// Token: 0x14000629 RID: 1577
		// (add) Token: 0x06003ED5 RID: 16085
		// (remove) Token: 0x06003ED6 RID: 16086
		public virtual extern event HTMLAnchorEvents_oncontextmenuEventHandler HTMLAnchorEvents_Event_oncontextmenu;

		// Token: 0x1400062A RID: 1578
		// (add) Token: 0x06003ED7 RID: 16087
		// (remove) Token: 0x06003ED8 RID: 16088
		public virtual extern event HTMLAnchorEvents_onrowsdeleteEventHandler HTMLAnchorEvents_Event_onrowsdelete;

		// Token: 0x1400062B RID: 1579
		// (add) Token: 0x06003ED9 RID: 16089
		// (remove) Token: 0x06003EDA RID: 16090
		public virtual extern event HTMLAnchorEvents_onrowsinsertedEventHandler HTMLAnchorEvents_Event_onrowsinserted;

		// Token: 0x1400062C RID: 1580
		// (add) Token: 0x06003EDB RID: 16091
		// (remove) Token: 0x06003EDC RID: 16092
		public virtual extern event HTMLAnchorEvents_oncellchangeEventHandler HTMLAnchorEvents_Event_oncellchange;

		// Token: 0x1400062D RID: 1581
		// (add) Token: 0x06003EDD RID: 16093
		// (remove) Token: 0x06003EDE RID: 16094
		public virtual extern event HTMLAnchorEvents_onreadystatechangeEventHandler HTMLAnchorEvents_Event_onreadystatechange;

		// Token: 0x1400062E RID: 1582
		// (add) Token: 0x06003EDF RID: 16095
		// (remove) Token: 0x06003EE0 RID: 16096
		public virtual extern event HTMLAnchorEvents_onbeforeeditfocusEventHandler HTMLAnchorEvents_Event_onbeforeeditfocus;

		// Token: 0x1400062F RID: 1583
		// (add) Token: 0x06003EE1 RID: 16097
		// (remove) Token: 0x06003EE2 RID: 16098
		public virtual extern event HTMLAnchorEvents_onlayoutcompleteEventHandler HTMLAnchorEvents_Event_onlayoutcomplete;

		// Token: 0x14000630 RID: 1584
		// (add) Token: 0x06003EE3 RID: 16099
		// (remove) Token: 0x06003EE4 RID: 16100
		public virtual extern event HTMLAnchorEvents_onpageEventHandler HTMLAnchorEvents_Event_onpage;

		// Token: 0x14000631 RID: 1585
		// (add) Token: 0x06003EE5 RID: 16101
		// (remove) Token: 0x06003EE6 RID: 16102
		public virtual extern event HTMLAnchorEvents_onbeforedeactivateEventHandler HTMLAnchorEvents_Event_onbeforedeactivate;

		// Token: 0x14000632 RID: 1586
		// (add) Token: 0x06003EE7 RID: 16103
		// (remove) Token: 0x06003EE8 RID: 16104
		public virtual extern event HTMLAnchorEvents_onbeforeactivateEventHandler HTMLAnchorEvents_Event_onbeforeactivate;

		// Token: 0x14000633 RID: 1587
		// (add) Token: 0x06003EE9 RID: 16105
		// (remove) Token: 0x06003EEA RID: 16106
		public virtual extern event HTMLAnchorEvents_onmoveEventHandler HTMLAnchorEvents_Event_onmove;

		// Token: 0x14000634 RID: 1588
		// (add) Token: 0x06003EEB RID: 16107
		// (remove) Token: 0x06003EEC RID: 16108
		public virtual extern event HTMLAnchorEvents_oncontrolselectEventHandler HTMLAnchorEvents_Event_oncontrolselect;

		// Token: 0x14000635 RID: 1589
		// (add) Token: 0x06003EED RID: 16109
		// (remove) Token: 0x06003EEE RID: 16110
		public virtual extern event HTMLAnchorEvents_onmovestartEventHandler HTMLAnchorEvents_Event_onmovestart;

		// Token: 0x14000636 RID: 1590
		// (add) Token: 0x06003EEF RID: 16111
		// (remove) Token: 0x06003EF0 RID: 16112
		public virtual extern event HTMLAnchorEvents_onmoveendEventHandler HTMLAnchorEvents_Event_onmoveend;

		// Token: 0x14000637 RID: 1591
		// (add) Token: 0x06003EF1 RID: 16113
		// (remove) Token: 0x06003EF2 RID: 16114
		public virtual extern event HTMLAnchorEvents_onresizestartEventHandler HTMLAnchorEvents_Event_onresizestart;

		// Token: 0x14000638 RID: 1592
		// (add) Token: 0x06003EF3 RID: 16115
		// (remove) Token: 0x06003EF4 RID: 16116
		public virtual extern event HTMLAnchorEvents_onresizeendEventHandler HTMLAnchorEvents_Event_onresizeend;

		// Token: 0x14000639 RID: 1593
		// (add) Token: 0x06003EF5 RID: 16117
		// (remove) Token: 0x06003EF6 RID: 16118
		public virtual extern event HTMLAnchorEvents_onmouseenterEventHandler HTMLAnchorEvents_Event_onmouseenter;

		// Token: 0x1400063A RID: 1594
		// (add) Token: 0x06003EF7 RID: 16119
		// (remove) Token: 0x06003EF8 RID: 16120
		public virtual extern event HTMLAnchorEvents_onmouseleaveEventHandler HTMLAnchorEvents_Event_onmouseleave;

		// Token: 0x1400063B RID: 1595
		// (add) Token: 0x06003EF9 RID: 16121
		// (remove) Token: 0x06003EFA RID: 16122
		public virtual extern event HTMLAnchorEvents_onmousewheelEventHandler HTMLAnchorEvents_Event_onmousewheel;

		// Token: 0x1400063C RID: 1596
		// (add) Token: 0x06003EFB RID: 16123
		// (remove) Token: 0x06003EFC RID: 16124
		public virtual extern event HTMLAnchorEvents_onactivateEventHandler HTMLAnchorEvents_Event_onactivate;

		// Token: 0x1400063D RID: 1597
		// (add) Token: 0x06003EFD RID: 16125
		// (remove) Token: 0x06003EFE RID: 16126
		public virtual extern event HTMLAnchorEvents_ondeactivateEventHandler HTMLAnchorEvents_Event_ondeactivate;

		// Token: 0x1400063E RID: 1598
		// (add) Token: 0x06003EFF RID: 16127
		// (remove) Token: 0x06003F00 RID: 16128
		public virtual extern event HTMLAnchorEvents_onfocusinEventHandler HTMLAnchorEvents_Event_onfocusin;

		// Token: 0x1400063F RID: 1599
		// (add) Token: 0x06003F01 RID: 16129
		// (remove) Token: 0x06003F02 RID: 16130
		public virtual extern event HTMLAnchorEvents_onfocusoutEventHandler HTMLAnchorEvents_Event_onfocusout;

		// Token: 0x14000640 RID: 1600
		// (add) Token: 0x06003F03 RID: 16131
		// (remove) Token: 0x06003F04 RID: 16132
		public virtual extern event HTMLAnchorEvents2_onhelpEventHandler HTMLAnchorEvents2_Event_onhelp;

		// Token: 0x14000641 RID: 1601
		// (add) Token: 0x06003F05 RID: 16133
		// (remove) Token: 0x06003F06 RID: 16134
		public virtual extern event HTMLAnchorEvents2_onclickEventHandler HTMLAnchorEvents2_Event_onclick;

		// Token: 0x14000642 RID: 1602
		// (add) Token: 0x06003F07 RID: 16135
		// (remove) Token: 0x06003F08 RID: 16136
		public virtual extern event HTMLAnchorEvents2_ondblclickEventHandler HTMLAnchorEvents2_Event_ondblclick;

		// Token: 0x14000643 RID: 1603
		// (add) Token: 0x06003F09 RID: 16137
		// (remove) Token: 0x06003F0A RID: 16138
		public virtual extern event HTMLAnchorEvents2_onkeypressEventHandler HTMLAnchorEvents2_Event_onkeypress;

		// Token: 0x14000644 RID: 1604
		// (add) Token: 0x06003F0B RID: 16139
		// (remove) Token: 0x06003F0C RID: 16140
		public virtual extern event HTMLAnchorEvents2_onkeydownEventHandler HTMLAnchorEvents2_Event_onkeydown;

		// Token: 0x14000645 RID: 1605
		// (add) Token: 0x06003F0D RID: 16141
		// (remove) Token: 0x06003F0E RID: 16142
		public virtual extern event HTMLAnchorEvents2_onkeyupEventHandler HTMLAnchorEvents2_Event_onkeyup;

		// Token: 0x14000646 RID: 1606
		// (add) Token: 0x06003F0F RID: 16143
		// (remove) Token: 0x06003F10 RID: 16144
		public virtual extern event HTMLAnchorEvents2_onmouseoutEventHandler HTMLAnchorEvents2_Event_onmouseout;

		// Token: 0x14000647 RID: 1607
		// (add) Token: 0x06003F11 RID: 16145
		// (remove) Token: 0x06003F12 RID: 16146
		public virtual extern event HTMLAnchorEvents2_onmouseoverEventHandler HTMLAnchorEvents2_Event_onmouseover;

		// Token: 0x14000648 RID: 1608
		// (add) Token: 0x06003F13 RID: 16147
		// (remove) Token: 0x06003F14 RID: 16148
		public virtual extern event HTMLAnchorEvents2_onmousemoveEventHandler HTMLAnchorEvents2_Event_onmousemove;

		// Token: 0x14000649 RID: 1609
		// (add) Token: 0x06003F15 RID: 16149
		// (remove) Token: 0x06003F16 RID: 16150
		public virtual extern event HTMLAnchorEvents2_onmousedownEventHandler HTMLAnchorEvents2_Event_onmousedown;

		// Token: 0x1400064A RID: 1610
		// (add) Token: 0x06003F17 RID: 16151
		// (remove) Token: 0x06003F18 RID: 16152
		public virtual extern event HTMLAnchorEvents2_onmouseupEventHandler HTMLAnchorEvents2_Event_onmouseup;

		// Token: 0x1400064B RID: 1611
		// (add) Token: 0x06003F19 RID: 16153
		// (remove) Token: 0x06003F1A RID: 16154
		public virtual extern event HTMLAnchorEvents2_onselectstartEventHandler HTMLAnchorEvents2_Event_onselectstart;

		// Token: 0x1400064C RID: 1612
		// (add) Token: 0x06003F1B RID: 16155
		// (remove) Token: 0x06003F1C RID: 16156
		public virtual extern event HTMLAnchorEvents2_onfilterchangeEventHandler HTMLAnchorEvents2_Event_onfilterchange;

		// Token: 0x1400064D RID: 1613
		// (add) Token: 0x06003F1D RID: 16157
		// (remove) Token: 0x06003F1E RID: 16158
		public virtual extern event HTMLAnchorEvents2_ondragstartEventHandler HTMLAnchorEvents2_Event_ondragstart;

		// Token: 0x1400064E RID: 1614
		// (add) Token: 0x06003F1F RID: 16159
		// (remove) Token: 0x06003F20 RID: 16160
		public virtual extern event HTMLAnchorEvents2_onbeforeupdateEventHandler HTMLAnchorEvents2_Event_onbeforeupdate;

		// Token: 0x1400064F RID: 1615
		// (add) Token: 0x06003F21 RID: 16161
		// (remove) Token: 0x06003F22 RID: 16162
		public virtual extern event HTMLAnchorEvents2_onafterupdateEventHandler HTMLAnchorEvents2_Event_onafterupdate;

		// Token: 0x14000650 RID: 1616
		// (add) Token: 0x06003F23 RID: 16163
		// (remove) Token: 0x06003F24 RID: 16164
		public virtual extern event HTMLAnchorEvents2_onerrorupdateEventHandler HTMLAnchorEvents2_Event_onerrorupdate;

		// Token: 0x14000651 RID: 1617
		// (add) Token: 0x06003F25 RID: 16165
		// (remove) Token: 0x06003F26 RID: 16166
		public virtual extern event HTMLAnchorEvents2_onrowexitEventHandler HTMLAnchorEvents2_Event_onrowexit;

		// Token: 0x14000652 RID: 1618
		// (add) Token: 0x06003F27 RID: 16167
		// (remove) Token: 0x06003F28 RID: 16168
		public virtual extern event HTMLAnchorEvents2_onrowenterEventHandler HTMLAnchorEvents2_Event_onrowenter;

		// Token: 0x14000653 RID: 1619
		// (add) Token: 0x06003F29 RID: 16169
		// (remove) Token: 0x06003F2A RID: 16170
		public virtual extern event HTMLAnchorEvents2_ondatasetchangedEventHandler HTMLAnchorEvents2_Event_ondatasetchanged;

		// Token: 0x14000654 RID: 1620
		// (add) Token: 0x06003F2B RID: 16171
		// (remove) Token: 0x06003F2C RID: 16172
		public virtual extern event HTMLAnchorEvents2_ondataavailableEventHandler HTMLAnchorEvents2_Event_ondataavailable;

		// Token: 0x14000655 RID: 1621
		// (add) Token: 0x06003F2D RID: 16173
		// (remove) Token: 0x06003F2E RID: 16174
		public virtual extern event HTMLAnchorEvents2_ondatasetcompleteEventHandler HTMLAnchorEvents2_Event_ondatasetcomplete;

		// Token: 0x14000656 RID: 1622
		// (add) Token: 0x06003F2F RID: 16175
		// (remove) Token: 0x06003F30 RID: 16176
		public virtual extern event HTMLAnchorEvents2_onlosecaptureEventHandler HTMLAnchorEvents2_Event_onlosecapture;

		// Token: 0x14000657 RID: 1623
		// (add) Token: 0x06003F31 RID: 16177
		// (remove) Token: 0x06003F32 RID: 16178
		public virtual extern event HTMLAnchorEvents2_onpropertychangeEventHandler HTMLAnchorEvents2_Event_onpropertychange;

		// Token: 0x14000658 RID: 1624
		// (add) Token: 0x06003F33 RID: 16179
		// (remove) Token: 0x06003F34 RID: 16180
		public virtual extern event HTMLAnchorEvents2_onscrollEventHandler HTMLAnchorEvents2_Event_onscroll;

		// Token: 0x14000659 RID: 1625
		// (add) Token: 0x06003F35 RID: 16181
		// (remove) Token: 0x06003F36 RID: 16182
		public virtual extern event HTMLAnchorEvents2_onfocusEventHandler HTMLAnchorEvents2_Event_onfocus;

		// Token: 0x1400065A RID: 1626
		// (add) Token: 0x06003F37 RID: 16183
		// (remove) Token: 0x06003F38 RID: 16184
		public virtual extern event HTMLAnchorEvents2_onblurEventHandler HTMLAnchorEvents2_Event_onblur;

		// Token: 0x1400065B RID: 1627
		// (add) Token: 0x06003F39 RID: 16185
		// (remove) Token: 0x06003F3A RID: 16186
		public virtual extern event HTMLAnchorEvents2_onresizeEventHandler HTMLAnchorEvents2_Event_onresize;

		// Token: 0x1400065C RID: 1628
		// (add) Token: 0x06003F3B RID: 16187
		// (remove) Token: 0x06003F3C RID: 16188
		public virtual extern event HTMLAnchorEvents2_ondragEventHandler HTMLAnchorEvents2_Event_ondrag;

		// Token: 0x1400065D RID: 1629
		// (add) Token: 0x06003F3D RID: 16189
		// (remove) Token: 0x06003F3E RID: 16190
		public virtual extern event HTMLAnchorEvents2_ondragendEventHandler HTMLAnchorEvents2_Event_ondragend;

		// Token: 0x1400065E RID: 1630
		// (add) Token: 0x06003F3F RID: 16191
		// (remove) Token: 0x06003F40 RID: 16192
		public virtual extern event HTMLAnchorEvents2_ondragenterEventHandler HTMLAnchorEvents2_Event_ondragenter;

		// Token: 0x1400065F RID: 1631
		// (add) Token: 0x06003F41 RID: 16193
		// (remove) Token: 0x06003F42 RID: 16194
		public virtual extern event HTMLAnchorEvents2_ondragoverEventHandler HTMLAnchorEvents2_Event_ondragover;

		// Token: 0x14000660 RID: 1632
		// (add) Token: 0x06003F43 RID: 16195
		// (remove) Token: 0x06003F44 RID: 16196
		public virtual extern event HTMLAnchorEvents2_ondragleaveEventHandler HTMLAnchorEvents2_Event_ondragleave;

		// Token: 0x14000661 RID: 1633
		// (add) Token: 0x06003F45 RID: 16197
		// (remove) Token: 0x06003F46 RID: 16198
		public virtual extern event HTMLAnchorEvents2_ondropEventHandler HTMLAnchorEvents2_Event_ondrop;

		// Token: 0x14000662 RID: 1634
		// (add) Token: 0x06003F47 RID: 16199
		// (remove) Token: 0x06003F48 RID: 16200
		public virtual extern event HTMLAnchorEvents2_onbeforecutEventHandler HTMLAnchorEvents2_Event_onbeforecut;

		// Token: 0x14000663 RID: 1635
		// (add) Token: 0x06003F49 RID: 16201
		// (remove) Token: 0x06003F4A RID: 16202
		public virtual extern event HTMLAnchorEvents2_oncutEventHandler HTMLAnchorEvents2_Event_oncut;

		// Token: 0x14000664 RID: 1636
		// (add) Token: 0x06003F4B RID: 16203
		// (remove) Token: 0x06003F4C RID: 16204
		public virtual extern event HTMLAnchorEvents2_onbeforecopyEventHandler HTMLAnchorEvents2_Event_onbeforecopy;

		// Token: 0x14000665 RID: 1637
		// (add) Token: 0x06003F4D RID: 16205
		// (remove) Token: 0x06003F4E RID: 16206
		public virtual extern event HTMLAnchorEvents2_oncopyEventHandler HTMLAnchorEvents2_Event_oncopy;

		// Token: 0x14000666 RID: 1638
		// (add) Token: 0x06003F4F RID: 16207
		// (remove) Token: 0x06003F50 RID: 16208
		public virtual extern event HTMLAnchorEvents2_onbeforepasteEventHandler HTMLAnchorEvents2_Event_onbeforepaste;

		// Token: 0x14000667 RID: 1639
		// (add) Token: 0x06003F51 RID: 16209
		// (remove) Token: 0x06003F52 RID: 16210
		public virtual extern event HTMLAnchorEvents2_onpasteEventHandler HTMLAnchorEvents2_Event_onpaste;

		// Token: 0x14000668 RID: 1640
		// (add) Token: 0x06003F53 RID: 16211
		// (remove) Token: 0x06003F54 RID: 16212
		public virtual extern event HTMLAnchorEvents2_oncontextmenuEventHandler HTMLAnchorEvents2_Event_oncontextmenu;

		// Token: 0x14000669 RID: 1641
		// (add) Token: 0x06003F55 RID: 16213
		// (remove) Token: 0x06003F56 RID: 16214
		public virtual extern event HTMLAnchorEvents2_onrowsdeleteEventHandler HTMLAnchorEvents2_Event_onrowsdelete;

		// Token: 0x1400066A RID: 1642
		// (add) Token: 0x06003F57 RID: 16215
		// (remove) Token: 0x06003F58 RID: 16216
		public virtual extern event HTMLAnchorEvents2_onrowsinsertedEventHandler HTMLAnchorEvents2_Event_onrowsinserted;

		// Token: 0x1400066B RID: 1643
		// (add) Token: 0x06003F59 RID: 16217
		// (remove) Token: 0x06003F5A RID: 16218
		public virtual extern event HTMLAnchorEvents2_oncellchangeEventHandler HTMLAnchorEvents2_Event_oncellchange;

		// Token: 0x1400066C RID: 1644
		// (add) Token: 0x06003F5B RID: 16219
		// (remove) Token: 0x06003F5C RID: 16220
		public virtual extern event HTMLAnchorEvents2_onreadystatechangeEventHandler HTMLAnchorEvents2_Event_onreadystatechange;

		// Token: 0x1400066D RID: 1645
		// (add) Token: 0x06003F5D RID: 16221
		// (remove) Token: 0x06003F5E RID: 16222
		public virtual extern event HTMLAnchorEvents2_onlayoutcompleteEventHandler HTMLAnchorEvents2_Event_onlayoutcomplete;

		// Token: 0x1400066E RID: 1646
		// (add) Token: 0x06003F5F RID: 16223
		// (remove) Token: 0x06003F60 RID: 16224
		public virtual extern event HTMLAnchorEvents2_onpageEventHandler HTMLAnchorEvents2_Event_onpage;

		// Token: 0x1400066F RID: 1647
		// (add) Token: 0x06003F61 RID: 16225
		// (remove) Token: 0x06003F62 RID: 16226
		public virtual extern event HTMLAnchorEvents2_onmouseenterEventHandler HTMLAnchorEvents2_Event_onmouseenter;

		// Token: 0x14000670 RID: 1648
		// (add) Token: 0x06003F63 RID: 16227
		// (remove) Token: 0x06003F64 RID: 16228
		public virtual extern event HTMLAnchorEvents2_onmouseleaveEventHandler HTMLAnchorEvents2_Event_onmouseleave;

		// Token: 0x14000671 RID: 1649
		// (add) Token: 0x06003F65 RID: 16229
		// (remove) Token: 0x06003F66 RID: 16230
		public virtual extern event HTMLAnchorEvents2_onactivateEventHandler HTMLAnchorEvents2_Event_onactivate;

		// Token: 0x14000672 RID: 1650
		// (add) Token: 0x06003F67 RID: 16231
		// (remove) Token: 0x06003F68 RID: 16232
		public virtual extern event HTMLAnchorEvents2_ondeactivateEventHandler HTMLAnchorEvents2_Event_ondeactivate;

		// Token: 0x14000673 RID: 1651
		// (add) Token: 0x06003F69 RID: 16233
		// (remove) Token: 0x06003F6A RID: 16234
		public virtual extern event HTMLAnchorEvents2_onbeforedeactivateEventHandler HTMLAnchorEvents2_Event_onbeforedeactivate;

		// Token: 0x14000674 RID: 1652
		// (add) Token: 0x06003F6B RID: 16235
		// (remove) Token: 0x06003F6C RID: 16236
		public virtual extern event HTMLAnchorEvents2_onbeforeactivateEventHandler HTMLAnchorEvents2_Event_onbeforeactivate;

		// Token: 0x14000675 RID: 1653
		// (add) Token: 0x06003F6D RID: 16237
		// (remove) Token: 0x06003F6E RID: 16238
		public virtual extern event HTMLAnchorEvents2_onfocusinEventHandler HTMLAnchorEvents2_Event_onfocusin;

		// Token: 0x14000676 RID: 1654
		// (add) Token: 0x06003F6F RID: 16239
		// (remove) Token: 0x06003F70 RID: 16240
		public virtual extern event HTMLAnchorEvents2_onfocusoutEventHandler HTMLAnchorEvents2_Event_onfocusout;

		// Token: 0x14000677 RID: 1655
		// (add) Token: 0x06003F71 RID: 16241
		// (remove) Token: 0x06003F72 RID: 16242
		public virtual extern event HTMLAnchorEvents2_onmoveEventHandler HTMLAnchorEvents2_Event_onmove;

		// Token: 0x14000678 RID: 1656
		// (add) Token: 0x06003F73 RID: 16243
		// (remove) Token: 0x06003F74 RID: 16244
		public virtual extern event HTMLAnchorEvents2_oncontrolselectEventHandler HTMLAnchorEvents2_Event_oncontrolselect;

		// Token: 0x14000679 RID: 1657
		// (add) Token: 0x06003F75 RID: 16245
		// (remove) Token: 0x06003F76 RID: 16246
		public virtual extern event HTMLAnchorEvents2_onmovestartEventHandler HTMLAnchorEvents2_Event_onmovestart;

		// Token: 0x1400067A RID: 1658
		// (add) Token: 0x06003F77 RID: 16247
		// (remove) Token: 0x06003F78 RID: 16248
		public virtual extern event HTMLAnchorEvents2_onmoveendEventHandler HTMLAnchorEvents2_Event_onmoveend;

		// Token: 0x1400067B RID: 1659
		// (add) Token: 0x06003F79 RID: 16249
		// (remove) Token: 0x06003F7A RID: 16250
		public virtual extern event HTMLAnchorEvents2_onresizestartEventHandler HTMLAnchorEvents2_Event_onresizestart;

		// Token: 0x1400067C RID: 1660
		// (add) Token: 0x06003F7B RID: 16251
		// (remove) Token: 0x06003F7C RID: 16252
		public virtual extern event HTMLAnchorEvents2_onresizeendEventHandler HTMLAnchorEvents2_Event_onresizeend;

		// Token: 0x1400067D RID: 1661
		// (add) Token: 0x06003F7D RID: 16253
		// (remove) Token: 0x06003F7E RID: 16254
		public virtual extern event HTMLAnchorEvents2_onmousewheelEventHandler HTMLAnchorEvents2_Event_onmousewheel;
	}
}
