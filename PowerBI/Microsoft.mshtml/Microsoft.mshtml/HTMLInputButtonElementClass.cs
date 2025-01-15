using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D0F RID: 3343
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLButtonElementEvents\0\0")]
	[Guid("3050F2B4-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLInputButtonElementClass : DispIHTMLInputButtonElement, HTMLInputButtonElement, HTMLButtonElementEvents_Event, IHTMLInputButtonElement, IHTMLControlElement, IHTMLElement
	{
		// Token: 0x060168D3 RID: 92371
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLInputButtonElementClass();

		// Token: 0x060168D4 RID: 92372
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060168D5 RID: 92373
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060168D6 RID: 92374
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17007813 RID: 30739
		// (get) Token: 0x060168D8 RID: 92376
		// (set) Token: 0x060168D7 RID: 92375
		[DispId(-2147417111)]
		public virtual extern string className
		{
			[DispId(-2147417111)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417111)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007814 RID: 30740
		// (get) Token: 0x060168DA RID: 92378
		// (set) Token: 0x060168D9 RID: 92377
		[DispId(-2147417110)]
		public virtual extern string id
		{
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007815 RID: 30741
		// (get) Token: 0x060168DB RID: 92379
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007816 RID: 30742
		// (get) Token: 0x060168DC RID: 92380
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007817 RID: 30743
		// (get) Token: 0x060168DD RID: 92381
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007818 RID: 30744
		// (get) Token: 0x060168DF RID: 92383
		// (set) Token: 0x060168DE RID: 92382
		[DispId(-2147412099)]
		public virtual extern object onhelp
		{
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007819 RID: 30745
		// (get) Token: 0x060168E1 RID: 92385
		// (set) Token: 0x060168E0 RID: 92384
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

		// Token: 0x1700781A RID: 30746
		// (get) Token: 0x060168E3 RID: 92387
		// (set) Token: 0x060168E2 RID: 92386
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

		// Token: 0x1700781B RID: 30747
		// (get) Token: 0x060168E5 RID: 92389
		// (set) Token: 0x060168E4 RID: 92388
		[DispId(-2147412107)]
		public virtual extern object onkeydown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700781C RID: 30748
		// (get) Token: 0x060168E7 RID: 92391
		// (set) Token: 0x060168E6 RID: 92390
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

		// Token: 0x1700781D RID: 30749
		// (get) Token: 0x060168E9 RID: 92393
		// (set) Token: 0x060168E8 RID: 92392
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

		// Token: 0x1700781E RID: 30750
		// (get) Token: 0x060168EB RID: 92395
		// (set) Token: 0x060168EA RID: 92394
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

		// Token: 0x1700781F RID: 30751
		// (get) Token: 0x060168ED RID: 92397
		// (set) Token: 0x060168EC RID: 92396
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

		// Token: 0x17007820 RID: 30752
		// (get) Token: 0x060168EF RID: 92399
		// (set) Token: 0x060168EE RID: 92398
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

		// Token: 0x17007821 RID: 30753
		// (get) Token: 0x060168F1 RID: 92401
		// (set) Token: 0x060168F0 RID: 92400
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

		// Token: 0x17007822 RID: 30754
		// (get) Token: 0x060168F3 RID: 92403
		// (set) Token: 0x060168F2 RID: 92402
		[DispId(-2147412109)]
		public virtual extern object onmouseup
		{
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007823 RID: 30755
		// (get) Token: 0x060168F4 RID: 92404
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007824 RID: 30756
		// (get) Token: 0x060168F6 RID: 92406
		// (set) Token: 0x060168F5 RID: 92405
		[DispId(-2147418043)]
		public virtual extern string title
		{
			[TypeLibFunc(20)]
			[DispId(-2147418043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007825 RID: 30757
		// (get) Token: 0x060168F8 RID: 92408
		// (set) Token: 0x060168F7 RID: 92407
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

		// Token: 0x17007826 RID: 30758
		// (get) Token: 0x060168FA RID: 92410
		// (set) Token: 0x060168F9 RID: 92409
		[DispId(-2147412075)]
		public virtual extern object onselectstart
		{
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060168FB RID: 92411
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060168FC RID: 92412
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17007827 RID: 30759
		// (get) Token: 0x060168FD RID: 92413
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007828 RID: 30760
		// (get) Token: 0x060168FE RID: 92414
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007829 RID: 30761
		// (get) Token: 0x06016900 RID: 92416
		// (set) Token: 0x060168FF RID: 92415
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

		// Token: 0x1700782A RID: 30762
		// (get) Token: 0x06016901 RID: 92417
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700782B RID: 30763
		// (get) Token: 0x06016902 RID: 92418
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700782C RID: 30764
		// (get) Token: 0x06016903 RID: 92419
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700782D RID: 30765
		// (get) Token: 0x06016904 RID: 92420
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700782E RID: 30766
		// (get) Token: 0x06016905 RID: 92421
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700782F RID: 30767
		// (get) Token: 0x06016907 RID: 92423
		// (set) Token: 0x06016906 RID: 92422
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

		// Token: 0x17007830 RID: 30768
		// (get) Token: 0x06016909 RID: 92425
		// (set) Token: 0x06016908 RID: 92424
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

		// Token: 0x17007831 RID: 30769
		// (get) Token: 0x0601690B RID: 92427
		// (set) Token: 0x0601690A RID: 92426
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

		// Token: 0x17007832 RID: 30770
		// (get) Token: 0x0601690D RID: 92429
		// (set) Token: 0x0601690C RID: 92428
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

		// Token: 0x0601690E RID: 92430
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0601690F RID: 92431
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17007833 RID: 30771
		// (get) Token: 0x06016910 RID: 92432
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007834 RID: 30772
		// (get) Token: 0x06016911 RID: 92433
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016912 RID: 92434
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17007835 RID: 30773
		// (get) Token: 0x06016913 RID: 92435
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007836 RID: 30774
		// (get) Token: 0x06016915 RID: 92437
		// (set) Token: 0x06016914 RID: 92436
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

		// Token: 0x06016916 RID: 92438
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17007837 RID: 30775
		// (get) Token: 0x06016918 RID: 92440
		// (set) Token: 0x06016917 RID: 92439
		[DispId(-2147412091)]
		public virtual extern object onbeforeupdate
		{
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007838 RID: 30776
		// (get) Token: 0x0601691A RID: 92442
		// (set) Token: 0x06016919 RID: 92441
		[DispId(-2147412090)]
		public virtual extern object onafterupdate
		{
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007839 RID: 30777
		// (get) Token: 0x0601691C RID: 92444
		// (set) Token: 0x0601691B RID: 92443
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

		// Token: 0x1700783A RID: 30778
		// (get) Token: 0x0601691E RID: 92446
		// (set) Token: 0x0601691D RID: 92445
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

		// Token: 0x1700783B RID: 30779
		// (get) Token: 0x06016920 RID: 92448
		// (set) Token: 0x0601691F RID: 92447
		[DispId(-2147412093)]
		public virtual extern object onrowenter
		{
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700783C RID: 30780
		// (get) Token: 0x06016922 RID: 92450
		// (set) Token: 0x06016921 RID: 92449
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

		// Token: 0x1700783D RID: 30781
		// (get) Token: 0x06016924 RID: 92452
		// (set) Token: 0x06016923 RID: 92451
		[DispId(-2147412071)]
		public virtual extern object ondataavailable
		{
			[DispId(-2147412071)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700783E RID: 30782
		// (get) Token: 0x06016926 RID: 92454
		// (set) Token: 0x06016925 RID: 92453
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

		// Token: 0x1700783F RID: 30783
		// (get) Token: 0x06016928 RID: 92456
		// (set) Token: 0x06016927 RID: 92455
		[DispId(-2147412069)]
		public virtual extern object onfilterchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412069)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007840 RID: 30784
		// (get) Token: 0x06016929 RID: 92457
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007841 RID: 30785
		// (get) Token: 0x0601692A RID: 92458
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007842 RID: 30786
		// (get) Token: 0x0601692C RID: 92460
		// (set) Token: 0x0601692B RID: 92459
		[DispId(-2147418097)]
		public virtual extern short tabIndex
		{
			[TypeLibFunc(20)]
			[DispId(-2147418097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0601692D RID: 92461
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17007843 RID: 30787
		// (get) Token: 0x0601692F RID: 92463
		// (set) Token: 0x0601692E RID: 92462
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

		// Token: 0x17007844 RID: 30788
		// (get) Token: 0x06016931 RID: 92465
		// (set) Token: 0x06016930 RID: 92464
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

		// Token: 0x17007845 RID: 30789
		// (get) Token: 0x06016933 RID: 92467
		// (set) Token: 0x06016932 RID: 92466
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

		// Token: 0x17007846 RID: 30790
		// (get) Token: 0x06016935 RID: 92469
		// (set) Token: 0x06016934 RID: 92468
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

		// Token: 0x06016936 RID: 92470
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06016937 RID: 92471
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06016938 RID: 92472
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17007847 RID: 30791
		// (get) Token: 0x06016939 RID: 92473
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007848 RID: 30792
		// (get) Token: 0x0601693A RID: 92474
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007849 RID: 30793
		// (get) Token: 0x0601693B RID: 92475
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700784A RID: 30794
		// (get) Token: 0x0601693C RID: 92476
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700784B RID: 30795
		// (get) Token: 0x0601693D RID: 92477
		[DispId(2000)]
		public virtual extern string type
		{
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700784C RID: 30796
		// (get) Token: 0x0601693F RID: 92479
		// (set) Token: 0x0601693E RID: 92478
		[DispId(-2147413011)]
		public virtual extern string value
		{
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700784D RID: 30797
		// (get) Token: 0x06016941 RID: 92481
		// (set) Token: 0x06016940 RID: 92480
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700784E RID: 30798
		// (get) Token: 0x06016943 RID: 92483
		// (set) Token: 0x06016942 RID: 92482
		[DispId(2021)]
		public virtual extern object status
		{
			[DispId(2021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700784F RID: 30799
		// (get) Token: 0x06016945 RID: 92485
		// (set) Token: 0x06016944 RID: 92484
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

		// Token: 0x17007850 RID: 30800
		// (get) Token: 0x06016946 RID: 92486
		[DispId(-2147416108)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06016947 RID: 92487
		[DispId(2006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange createTextRange();

		// Token: 0x17007851 RID: 30801
		// (get) Token: 0x06016948 RID: 92488
		public virtual extern string IHTMLInputButtonElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007852 RID: 30802
		// (get) Token: 0x0601694A RID: 92490
		// (set) Token: 0x06016949 RID: 92489
		public virtual extern string IHTMLInputButtonElement_value
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

		// Token: 0x17007853 RID: 30803
		// (get) Token: 0x0601694C RID: 92492
		// (set) Token: 0x0601694B RID: 92491
		public virtual extern string IHTMLInputButtonElement_name
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

		// Token: 0x17007854 RID: 30804
		// (get) Token: 0x0601694E RID: 92494
		// (set) Token: 0x0601694D RID: 92493
		public virtual extern object IHTMLInputButtonElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007855 RID: 30805
		// (get) Token: 0x06016950 RID: 92496
		// (set) Token: 0x0601694F RID: 92495
		public virtual extern bool IHTMLInputButtonElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007856 RID: 30806
		// (get) Token: 0x06016951 RID: 92497
		public virtual extern IHTMLFormElement IHTMLInputButtonElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06016952 RID: 92498
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange IHTMLInputButtonElement_createTextRange();

		// Token: 0x17007857 RID: 30807
		// (get) Token: 0x06016954 RID: 92500
		// (set) Token: 0x06016953 RID: 92499
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

		// Token: 0x06016955 RID: 92501
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17007858 RID: 30808
		// (get) Token: 0x06016957 RID: 92503
		// (set) Token: 0x06016956 RID: 92502
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

		// Token: 0x17007859 RID: 30809
		// (get) Token: 0x06016959 RID: 92505
		// (set) Token: 0x06016958 RID: 92504
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

		// Token: 0x1700785A RID: 30810
		// (get) Token: 0x0601695B RID: 92507
		// (set) Token: 0x0601695A RID: 92506
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

		// Token: 0x1700785B RID: 30811
		// (get) Token: 0x0601695D RID: 92509
		// (set) Token: 0x0601695C RID: 92508
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

		// Token: 0x0601695E RID: 92510
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x0601695F RID: 92511
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06016960 RID: 92512
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700785C RID: 30812
		// (get) Token: 0x06016961 RID: 92513
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700785D RID: 30813
		// (get) Token: 0x06016962 RID: 92514
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700785E RID: 30814
		// (get) Token: 0x06016963 RID: 92515
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700785F RID: 30815
		// (get) Token: 0x06016964 RID: 92516
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016965 RID: 92517
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06016966 RID: 92518
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06016967 RID: 92519
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17007860 RID: 30816
		// (get) Token: 0x06016969 RID: 92521
		// (set) Token: 0x06016968 RID: 92520
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

		// Token: 0x17007861 RID: 30817
		// (get) Token: 0x0601696B RID: 92523
		// (set) Token: 0x0601696A RID: 92522
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

		// Token: 0x17007862 RID: 30818
		// (get) Token: 0x0601696C RID: 92524
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007863 RID: 30819
		// (get) Token: 0x0601696D RID: 92525
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007864 RID: 30820
		// (get) Token: 0x0601696E RID: 92526
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007865 RID: 30821
		// (get) Token: 0x06016970 RID: 92528
		// (set) Token: 0x0601696F RID: 92527
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

		// Token: 0x17007866 RID: 30822
		// (get) Token: 0x06016972 RID: 92530
		// (set) Token: 0x06016971 RID: 92529
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

		// Token: 0x17007867 RID: 30823
		// (get) Token: 0x06016974 RID: 92532
		// (set) Token: 0x06016973 RID: 92531
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

		// Token: 0x17007868 RID: 30824
		// (get) Token: 0x06016976 RID: 92534
		// (set) Token: 0x06016975 RID: 92533
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

		// Token: 0x17007869 RID: 30825
		// (get) Token: 0x06016978 RID: 92536
		// (set) Token: 0x06016977 RID: 92535
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

		// Token: 0x1700786A RID: 30826
		// (get) Token: 0x0601697A RID: 92538
		// (set) Token: 0x06016979 RID: 92537
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

		// Token: 0x1700786B RID: 30827
		// (get) Token: 0x0601697C RID: 92540
		// (set) Token: 0x0601697B RID: 92539
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

		// Token: 0x1700786C RID: 30828
		// (get) Token: 0x0601697E RID: 92542
		// (set) Token: 0x0601697D RID: 92541
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

		// Token: 0x1700786D RID: 30829
		// (get) Token: 0x06016980 RID: 92544
		// (set) Token: 0x0601697F RID: 92543
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

		// Token: 0x1700786E RID: 30830
		// (get) Token: 0x06016982 RID: 92546
		// (set) Token: 0x06016981 RID: 92545
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

		// Token: 0x1700786F RID: 30831
		// (get) Token: 0x06016984 RID: 92548
		// (set) Token: 0x06016983 RID: 92547
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

		// Token: 0x17007870 RID: 30832
		// (get) Token: 0x06016985 RID: 92549
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007871 RID: 30833
		// (get) Token: 0x06016987 RID: 92551
		// (set) Token: 0x06016986 RID: 92550
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

		// Token: 0x17007872 RID: 30834
		// (get) Token: 0x06016989 RID: 92553
		// (set) Token: 0x06016988 RID: 92552
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

		// Token: 0x17007873 RID: 30835
		// (get) Token: 0x0601698B RID: 92555
		// (set) Token: 0x0601698A RID: 92554
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

		// Token: 0x0601698C RID: 92556
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0601698D RID: 92557
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17007874 RID: 30836
		// (get) Token: 0x0601698E RID: 92558
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007875 RID: 30837
		// (get) Token: 0x0601698F RID: 92559
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007876 RID: 30838
		// (get) Token: 0x06016991 RID: 92561
		// (set) Token: 0x06016990 RID: 92560
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

		// Token: 0x17007877 RID: 30839
		// (get) Token: 0x06016992 RID: 92562
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007878 RID: 30840
		// (get) Token: 0x06016993 RID: 92563
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007879 RID: 30841
		// (get) Token: 0x06016994 RID: 92564
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700787A RID: 30842
		// (get) Token: 0x06016995 RID: 92565
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700787B RID: 30843
		// (get) Token: 0x06016996 RID: 92566
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700787C RID: 30844
		// (get) Token: 0x06016998 RID: 92568
		// (set) Token: 0x06016997 RID: 92567
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

		// Token: 0x1700787D RID: 30845
		// (get) Token: 0x0601699A RID: 92570
		// (set) Token: 0x06016999 RID: 92569
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

		// Token: 0x1700787E RID: 30846
		// (get) Token: 0x0601699C RID: 92572
		// (set) Token: 0x0601699B RID: 92571
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

		// Token: 0x1700787F RID: 30847
		// (get) Token: 0x0601699E RID: 92574
		// (set) Token: 0x0601699D RID: 92573
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

		// Token: 0x0601699F RID: 92575
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060169A0 RID: 92576
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17007880 RID: 30848
		// (get) Token: 0x060169A1 RID: 92577
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007881 RID: 30849
		// (get) Token: 0x060169A2 RID: 92578
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060169A3 RID: 92579
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17007882 RID: 30850
		// (get) Token: 0x060169A4 RID: 92580
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007883 RID: 30851
		// (get) Token: 0x060169A6 RID: 92582
		// (set) Token: 0x060169A5 RID: 92581
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

		// Token: 0x060169A7 RID: 92583
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17007884 RID: 30852
		// (get) Token: 0x060169A9 RID: 92585
		// (set) Token: 0x060169A8 RID: 92584
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

		// Token: 0x17007885 RID: 30853
		// (get) Token: 0x060169AB RID: 92587
		// (set) Token: 0x060169AA RID: 92586
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

		// Token: 0x17007886 RID: 30854
		// (get) Token: 0x060169AD RID: 92589
		// (set) Token: 0x060169AC RID: 92588
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

		// Token: 0x17007887 RID: 30855
		// (get) Token: 0x060169AF RID: 92591
		// (set) Token: 0x060169AE RID: 92590
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

		// Token: 0x17007888 RID: 30856
		// (get) Token: 0x060169B1 RID: 92593
		// (set) Token: 0x060169B0 RID: 92592
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

		// Token: 0x17007889 RID: 30857
		// (get) Token: 0x060169B3 RID: 92595
		// (set) Token: 0x060169B2 RID: 92594
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

		// Token: 0x1700788A RID: 30858
		// (get) Token: 0x060169B5 RID: 92597
		// (set) Token: 0x060169B4 RID: 92596
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

		// Token: 0x1700788B RID: 30859
		// (get) Token: 0x060169B7 RID: 92599
		// (set) Token: 0x060169B6 RID: 92598
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

		// Token: 0x1700788C RID: 30860
		// (get) Token: 0x060169B9 RID: 92601
		// (set) Token: 0x060169B8 RID: 92600
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

		// Token: 0x1700788D RID: 30861
		// (get) Token: 0x060169BA RID: 92602
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700788E RID: 30862
		// (get) Token: 0x060169BB RID: 92603
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x14002B6F RID: 11119
		// (add) Token: 0x060169BC RID: 92604
		// (remove) Token: 0x060169BD RID: 92605
		public virtual extern event HTMLButtonElementEvents_onhelpEventHandler HTMLButtonElementEvents_Event_onhelp;

		// Token: 0x14002B70 RID: 11120
		// (add) Token: 0x060169BE RID: 92606
		// (remove) Token: 0x060169BF RID: 92607
		public virtual extern event HTMLButtonElementEvents_onclickEventHandler HTMLButtonElementEvents_Event_onclick;

		// Token: 0x14002B71 RID: 11121
		// (add) Token: 0x060169C0 RID: 92608
		// (remove) Token: 0x060169C1 RID: 92609
		public virtual extern event HTMLButtonElementEvents_ondblclickEventHandler HTMLButtonElementEvents_Event_ondblclick;

		// Token: 0x14002B72 RID: 11122
		// (add) Token: 0x060169C2 RID: 92610
		// (remove) Token: 0x060169C3 RID: 92611
		public virtual extern event HTMLButtonElementEvents_onkeypressEventHandler HTMLButtonElementEvents_Event_onkeypress;

		// Token: 0x14002B73 RID: 11123
		// (add) Token: 0x060169C4 RID: 92612
		// (remove) Token: 0x060169C5 RID: 92613
		public virtual extern event HTMLButtonElementEvents_onkeydownEventHandler HTMLButtonElementEvents_Event_onkeydown;

		// Token: 0x14002B74 RID: 11124
		// (add) Token: 0x060169C6 RID: 92614
		// (remove) Token: 0x060169C7 RID: 92615
		public virtual extern event HTMLButtonElementEvents_onkeyupEventHandler HTMLButtonElementEvents_Event_onkeyup;

		// Token: 0x14002B75 RID: 11125
		// (add) Token: 0x060169C8 RID: 92616
		// (remove) Token: 0x060169C9 RID: 92617
		public virtual extern event HTMLButtonElementEvents_onmouseoutEventHandler HTMLButtonElementEvents_Event_onmouseout;

		// Token: 0x14002B76 RID: 11126
		// (add) Token: 0x060169CA RID: 92618
		// (remove) Token: 0x060169CB RID: 92619
		public virtual extern event HTMLButtonElementEvents_onmouseoverEventHandler HTMLButtonElementEvents_Event_onmouseover;

		// Token: 0x14002B77 RID: 11127
		// (add) Token: 0x060169CC RID: 92620
		// (remove) Token: 0x060169CD RID: 92621
		public virtual extern event HTMLButtonElementEvents_onmousemoveEventHandler HTMLButtonElementEvents_Event_onmousemove;

		// Token: 0x14002B78 RID: 11128
		// (add) Token: 0x060169CE RID: 92622
		// (remove) Token: 0x060169CF RID: 92623
		public virtual extern event HTMLButtonElementEvents_onmousedownEventHandler HTMLButtonElementEvents_Event_onmousedown;

		// Token: 0x14002B79 RID: 11129
		// (add) Token: 0x060169D0 RID: 92624
		// (remove) Token: 0x060169D1 RID: 92625
		public virtual extern event HTMLButtonElementEvents_onmouseupEventHandler HTMLButtonElementEvents_Event_onmouseup;

		// Token: 0x14002B7A RID: 11130
		// (add) Token: 0x060169D2 RID: 92626
		// (remove) Token: 0x060169D3 RID: 92627
		public virtual extern event HTMLButtonElementEvents_onselectstartEventHandler HTMLButtonElementEvents_Event_onselectstart;

		// Token: 0x14002B7B RID: 11131
		// (add) Token: 0x060169D4 RID: 92628
		// (remove) Token: 0x060169D5 RID: 92629
		public virtual extern event HTMLButtonElementEvents_onfilterchangeEventHandler HTMLButtonElementEvents_Event_onfilterchange;

		// Token: 0x14002B7C RID: 11132
		// (add) Token: 0x060169D6 RID: 92630
		// (remove) Token: 0x060169D7 RID: 92631
		public virtual extern event HTMLButtonElementEvents_ondragstartEventHandler HTMLButtonElementEvents_Event_ondragstart;

		// Token: 0x14002B7D RID: 11133
		// (add) Token: 0x060169D8 RID: 92632
		// (remove) Token: 0x060169D9 RID: 92633
		public virtual extern event HTMLButtonElementEvents_onbeforeupdateEventHandler HTMLButtonElementEvents_Event_onbeforeupdate;

		// Token: 0x14002B7E RID: 11134
		// (add) Token: 0x060169DA RID: 92634
		// (remove) Token: 0x060169DB RID: 92635
		public virtual extern event HTMLButtonElementEvents_onafterupdateEventHandler HTMLButtonElementEvents_Event_onafterupdate;

		// Token: 0x14002B7F RID: 11135
		// (add) Token: 0x060169DC RID: 92636
		// (remove) Token: 0x060169DD RID: 92637
		public virtual extern event HTMLButtonElementEvents_onerrorupdateEventHandler HTMLButtonElementEvents_Event_onerrorupdate;

		// Token: 0x14002B80 RID: 11136
		// (add) Token: 0x060169DE RID: 92638
		// (remove) Token: 0x060169DF RID: 92639
		public virtual extern event HTMLButtonElementEvents_onrowexitEventHandler HTMLButtonElementEvents_Event_onrowexit;

		// Token: 0x14002B81 RID: 11137
		// (add) Token: 0x060169E0 RID: 92640
		// (remove) Token: 0x060169E1 RID: 92641
		public virtual extern event HTMLButtonElementEvents_onrowenterEventHandler HTMLButtonElementEvents_Event_onrowenter;

		// Token: 0x14002B82 RID: 11138
		// (add) Token: 0x060169E2 RID: 92642
		// (remove) Token: 0x060169E3 RID: 92643
		public virtual extern event HTMLButtonElementEvents_ondatasetchangedEventHandler HTMLButtonElementEvents_Event_ondatasetchanged;

		// Token: 0x14002B83 RID: 11139
		// (add) Token: 0x060169E4 RID: 92644
		// (remove) Token: 0x060169E5 RID: 92645
		public virtual extern event HTMLButtonElementEvents_ondataavailableEventHandler HTMLButtonElementEvents_Event_ondataavailable;

		// Token: 0x14002B84 RID: 11140
		// (add) Token: 0x060169E6 RID: 92646
		// (remove) Token: 0x060169E7 RID: 92647
		public virtual extern event HTMLButtonElementEvents_ondatasetcompleteEventHandler HTMLButtonElementEvents_Event_ondatasetcomplete;

		// Token: 0x14002B85 RID: 11141
		// (add) Token: 0x060169E8 RID: 92648
		// (remove) Token: 0x060169E9 RID: 92649
		public virtual extern event HTMLButtonElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14002B86 RID: 11142
		// (add) Token: 0x060169EA RID: 92650
		// (remove) Token: 0x060169EB RID: 92651
		public virtual extern event HTMLButtonElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14002B87 RID: 11143
		// (add) Token: 0x060169EC RID: 92652
		// (remove) Token: 0x060169ED RID: 92653
		public virtual extern event HTMLButtonElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14002B88 RID: 11144
		// (add) Token: 0x060169EE RID: 92654
		// (remove) Token: 0x060169EF RID: 92655
		public virtual extern event HTMLButtonElementEvents_onfocusEventHandler HTMLButtonElementEvents_Event_onfocus;

		// Token: 0x14002B89 RID: 11145
		// (add) Token: 0x060169F0 RID: 92656
		// (remove) Token: 0x060169F1 RID: 92657
		public virtual extern event HTMLButtonElementEvents_onblurEventHandler HTMLButtonElementEvents_Event_onblur;

		// Token: 0x14002B8A RID: 11146
		// (add) Token: 0x060169F2 RID: 92658
		// (remove) Token: 0x060169F3 RID: 92659
		public virtual extern event HTMLButtonElementEvents_onresizeEventHandler HTMLButtonElementEvents_Event_onresize;

		// Token: 0x14002B8B RID: 11147
		// (add) Token: 0x060169F4 RID: 92660
		// (remove) Token: 0x060169F5 RID: 92661
		public virtual extern event HTMLButtonElementEvents_ondragEventHandler ondrag;

		// Token: 0x14002B8C RID: 11148
		// (add) Token: 0x060169F6 RID: 92662
		// (remove) Token: 0x060169F7 RID: 92663
		public virtual extern event HTMLButtonElementEvents_ondragendEventHandler ondragend;

		// Token: 0x14002B8D RID: 11149
		// (add) Token: 0x060169F8 RID: 92664
		// (remove) Token: 0x060169F9 RID: 92665
		public virtual extern event HTMLButtonElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14002B8E RID: 11150
		// (add) Token: 0x060169FA RID: 92666
		// (remove) Token: 0x060169FB RID: 92667
		public virtual extern event HTMLButtonElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x14002B8F RID: 11151
		// (add) Token: 0x060169FC RID: 92668
		// (remove) Token: 0x060169FD RID: 92669
		public virtual extern event HTMLButtonElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14002B90 RID: 11152
		// (add) Token: 0x060169FE RID: 92670
		// (remove) Token: 0x060169FF RID: 92671
		public virtual extern event HTMLButtonElementEvents_ondropEventHandler ondrop;

		// Token: 0x14002B91 RID: 11153
		// (add) Token: 0x06016A00 RID: 92672
		// (remove) Token: 0x06016A01 RID: 92673
		public virtual extern event HTMLButtonElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002B92 RID: 11154
		// (add) Token: 0x06016A02 RID: 92674
		// (remove) Token: 0x06016A03 RID: 92675
		public virtual extern event HTMLButtonElementEvents_oncutEventHandler oncut;

		// Token: 0x14002B93 RID: 11155
		// (add) Token: 0x06016A04 RID: 92676
		// (remove) Token: 0x06016A05 RID: 92677
		public virtual extern event HTMLButtonElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002B94 RID: 11156
		// (add) Token: 0x06016A06 RID: 92678
		// (remove) Token: 0x06016A07 RID: 92679
		public virtual extern event HTMLButtonElementEvents_oncopyEventHandler oncopy;

		// Token: 0x14002B95 RID: 11157
		// (add) Token: 0x06016A08 RID: 92680
		// (remove) Token: 0x06016A09 RID: 92681
		public virtual extern event HTMLButtonElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14002B96 RID: 11158
		// (add) Token: 0x06016A0A RID: 92682
		// (remove) Token: 0x06016A0B RID: 92683
		public virtual extern event HTMLButtonElementEvents_onpasteEventHandler onpaste;

		// Token: 0x14002B97 RID: 11159
		// (add) Token: 0x06016A0C RID: 92684
		// (remove) Token: 0x06016A0D RID: 92685
		public virtual extern event HTMLButtonElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14002B98 RID: 11160
		// (add) Token: 0x06016A0E RID: 92686
		// (remove) Token: 0x06016A0F RID: 92687
		public virtual extern event HTMLButtonElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14002B99 RID: 11161
		// (add) Token: 0x06016A10 RID: 92688
		// (remove) Token: 0x06016A11 RID: 92689
		public virtual extern event HTMLButtonElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14002B9A RID: 11162
		// (add) Token: 0x06016A12 RID: 92690
		// (remove) Token: 0x06016A13 RID: 92691
		public virtual extern event HTMLButtonElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14002B9B RID: 11163
		// (add) Token: 0x06016A14 RID: 92692
		// (remove) Token: 0x06016A15 RID: 92693
		public virtual extern event HTMLButtonElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14002B9C RID: 11164
		// (add) Token: 0x06016A16 RID: 92694
		// (remove) Token: 0x06016A17 RID: 92695
		public virtual extern event HTMLButtonElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14002B9D RID: 11165
		// (add) Token: 0x06016A18 RID: 92696
		// (remove) Token: 0x06016A19 RID: 92697
		public virtual extern event HTMLButtonElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14002B9E RID: 11166
		// (add) Token: 0x06016A1A RID: 92698
		// (remove) Token: 0x06016A1B RID: 92699
		public virtual extern event HTMLButtonElementEvents_onpageEventHandler onpage;

		// Token: 0x14002B9F RID: 11167
		// (add) Token: 0x06016A1C RID: 92700
		// (remove) Token: 0x06016A1D RID: 92701
		public virtual extern event HTMLButtonElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002BA0 RID: 11168
		// (add) Token: 0x06016A1E RID: 92702
		// (remove) Token: 0x06016A1F RID: 92703
		public virtual extern event HTMLButtonElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002BA1 RID: 11169
		// (add) Token: 0x06016A20 RID: 92704
		// (remove) Token: 0x06016A21 RID: 92705
		public virtual extern event HTMLButtonElementEvents_onmoveEventHandler onmove;

		// Token: 0x14002BA2 RID: 11170
		// (add) Token: 0x06016A22 RID: 92706
		// (remove) Token: 0x06016A23 RID: 92707
		public virtual extern event HTMLButtonElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002BA3 RID: 11171
		// (add) Token: 0x06016A24 RID: 92708
		// (remove) Token: 0x06016A25 RID: 92709
		public virtual extern event HTMLButtonElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14002BA4 RID: 11172
		// (add) Token: 0x06016A26 RID: 92710
		// (remove) Token: 0x06016A27 RID: 92711
		public virtual extern event HTMLButtonElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14002BA5 RID: 11173
		// (add) Token: 0x06016A28 RID: 92712
		// (remove) Token: 0x06016A29 RID: 92713
		public virtual extern event HTMLButtonElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14002BA6 RID: 11174
		// (add) Token: 0x06016A2A RID: 92714
		// (remove) Token: 0x06016A2B RID: 92715
		public virtual extern event HTMLButtonElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14002BA7 RID: 11175
		// (add) Token: 0x06016A2C RID: 92716
		// (remove) Token: 0x06016A2D RID: 92717
		public virtual extern event HTMLButtonElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14002BA8 RID: 11176
		// (add) Token: 0x06016A2E RID: 92718
		// (remove) Token: 0x06016A2F RID: 92719
		public virtual extern event HTMLButtonElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14002BA9 RID: 11177
		// (add) Token: 0x06016A30 RID: 92720
		// (remove) Token: 0x06016A31 RID: 92721
		public virtual extern event HTMLButtonElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14002BAA RID: 11178
		// (add) Token: 0x06016A32 RID: 92722
		// (remove) Token: 0x06016A33 RID: 92723
		public virtual extern event HTMLButtonElementEvents_onactivateEventHandler onactivate;

		// Token: 0x14002BAB RID: 11179
		// (add) Token: 0x06016A34 RID: 92724
		// (remove) Token: 0x06016A35 RID: 92725
		public virtual extern event HTMLButtonElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002BAC RID: 11180
		// (add) Token: 0x06016A36 RID: 92726
		// (remove) Token: 0x06016A37 RID: 92727
		public virtual extern event HTMLButtonElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14002BAD RID: 11181
		// (add) Token: 0x06016A38 RID: 92728
		// (remove) Token: 0x06016A39 RID: 92729
		public virtual extern event HTMLButtonElementEvents_onfocusoutEventHandler onfocusout;
	}
}
