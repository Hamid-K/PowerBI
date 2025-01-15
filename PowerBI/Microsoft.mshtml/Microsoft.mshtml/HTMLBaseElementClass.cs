using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000785 RID: 1925
	[ComSourceInterfaces("mshtml.HTMLElementEvents\0mshtml.HTMLElementEvents2\0\0")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F276-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLBaseElementClass : DispHTMLBaseElement, HTMLBaseElement, HTMLElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLBaseElement, HTMLElementEvents2_Event
	{
		// Token: 0x0600C10B RID: 49419
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLBaseElementClass();

		// Token: 0x0600C10C RID: 49420
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600C10D RID: 49421
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600C10E RID: 49422
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17003F07 RID: 16135
		// (get) Token: 0x0600C110 RID: 49424
		// (set) Token: 0x0600C10F RID: 49423
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

		// Token: 0x17003F08 RID: 16136
		// (get) Token: 0x0600C112 RID: 49426
		// (set) Token: 0x0600C111 RID: 49425
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

		// Token: 0x17003F09 RID: 16137
		// (get) Token: 0x0600C113 RID: 49427
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003F0A RID: 16138
		// (get) Token: 0x0600C114 RID: 49428
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F0B RID: 16139
		// (get) Token: 0x0600C115 RID: 49429
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F0C RID: 16140
		// (get) Token: 0x0600C117 RID: 49431
		// (set) Token: 0x0600C116 RID: 49430
		[DispId(-2147412099)]
		public virtual extern object onhelp
		{
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F0D RID: 16141
		// (get) Token: 0x0600C119 RID: 49433
		// (set) Token: 0x0600C118 RID: 49432
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

		// Token: 0x17003F0E RID: 16142
		// (get) Token: 0x0600C11B RID: 49435
		// (set) Token: 0x0600C11A RID: 49434
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

		// Token: 0x17003F0F RID: 16143
		// (get) Token: 0x0600C11D RID: 49437
		// (set) Token: 0x0600C11C RID: 49436
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

		// Token: 0x17003F10 RID: 16144
		// (get) Token: 0x0600C11F RID: 49439
		// (set) Token: 0x0600C11E RID: 49438
		[DispId(-2147412106)]
		public virtual extern object onkeyup
		{
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F11 RID: 16145
		// (get) Token: 0x0600C121 RID: 49441
		// (set) Token: 0x0600C120 RID: 49440
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

		// Token: 0x17003F12 RID: 16146
		// (get) Token: 0x0600C123 RID: 49443
		// (set) Token: 0x0600C122 RID: 49442
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F13 RID: 16147
		// (get) Token: 0x0600C125 RID: 49445
		// (set) Token: 0x0600C124 RID: 49444
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

		// Token: 0x17003F14 RID: 16148
		// (get) Token: 0x0600C127 RID: 49447
		// (set) Token: 0x0600C126 RID: 49446
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

		// Token: 0x17003F15 RID: 16149
		// (get) Token: 0x0600C129 RID: 49449
		// (set) Token: 0x0600C128 RID: 49448
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

		// Token: 0x17003F16 RID: 16150
		// (get) Token: 0x0600C12B RID: 49451
		// (set) Token: 0x0600C12A RID: 49450
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

		// Token: 0x17003F17 RID: 16151
		// (get) Token: 0x0600C12C RID: 49452
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003F18 RID: 16152
		// (get) Token: 0x0600C12E RID: 49454
		// (set) Token: 0x0600C12D RID: 49453
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

		// Token: 0x17003F19 RID: 16153
		// (get) Token: 0x0600C130 RID: 49456
		// (set) Token: 0x0600C12F RID: 49455
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

		// Token: 0x17003F1A RID: 16154
		// (get) Token: 0x0600C132 RID: 49458
		// (set) Token: 0x0600C131 RID: 49457
		[DispId(-2147412075)]
		public virtual extern object onselectstart
		{
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600C133 RID: 49459
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600C134 RID: 49460
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17003F1B RID: 16155
		// (get) Token: 0x0600C135 RID: 49461
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F1C RID: 16156
		// (get) Token: 0x0600C136 RID: 49462
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003F1D RID: 16157
		// (get) Token: 0x0600C138 RID: 49464
		// (set) Token: 0x0600C137 RID: 49463
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

		// Token: 0x17003F1E RID: 16158
		// (get) Token: 0x0600C139 RID: 49465
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F1F RID: 16159
		// (get) Token: 0x0600C13A RID: 49466
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F20 RID: 16160
		// (get) Token: 0x0600C13B RID: 49467
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F21 RID: 16161
		// (get) Token: 0x0600C13C RID: 49468
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F22 RID: 16162
		// (get) Token: 0x0600C13D RID: 49469
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F23 RID: 16163
		// (get) Token: 0x0600C13F RID: 49471
		// (set) Token: 0x0600C13E RID: 49470
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

		// Token: 0x17003F24 RID: 16164
		// (get) Token: 0x0600C141 RID: 49473
		// (set) Token: 0x0600C140 RID: 49472
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

		// Token: 0x17003F25 RID: 16165
		// (get) Token: 0x0600C143 RID: 49475
		// (set) Token: 0x0600C142 RID: 49474
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

		// Token: 0x17003F26 RID: 16166
		// (get) Token: 0x0600C145 RID: 49477
		// (set) Token: 0x0600C144 RID: 49476
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

		// Token: 0x0600C146 RID: 49478
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600C147 RID: 49479
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17003F27 RID: 16167
		// (get) Token: 0x0600C148 RID: 49480
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F28 RID: 16168
		// (get) Token: 0x0600C149 RID: 49481
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C14A RID: 49482
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17003F29 RID: 16169
		// (get) Token: 0x0600C14B RID: 49483
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F2A RID: 16170
		// (get) Token: 0x0600C14D RID: 49485
		// (set) Token: 0x0600C14C RID: 49484
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

		// Token: 0x0600C14E RID: 49486
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17003F2B RID: 16171
		// (get) Token: 0x0600C150 RID: 49488
		// (set) Token: 0x0600C14F RID: 49487
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

		// Token: 0x17003F2C RID: 16172
		// (get) Token: 0x0600C152 RID: 49490
		// (set) Token: 0x0600C151 RID: 49489
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

		// Token: 0x17003F2D RID: 16173
		// (get) Token: 0x0600C154 RID: 49492
		// (set) Token: 0x0600C153 RID: 49491
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

		// Token: 0x17003F2E RID: 16174
		// (get) Token: 0x0600C156 RID: 49494
		// (set) Token: 0x0600C155 RID: 49493
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

		// Token: 0x17003F2F RID: 16175
		// (get) Token: 0x0600C158 RID: 49496
		// (set) Token: 0x0600C157 RID: 49495
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

		// Token: 0x17003F30 RID: 16176
		// (get) Token: 0x0600C15A RID: 49498
		// (set) Token: 0x0600C159 RID: 49497
		[DispId(-2147412072)]
		public virtual extern object ondatasetchanged
		{
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F31 RID: 16177
		// (get) Token: 0x0600C15C RID: 49500
		// (set) Token: 0x0600C15B RID: 49499
		[DispId(-2147412071)]
		public virtual extern object ondataavailable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F32 RID: 16178
		// (get) Token: 0x0600C15E RID: 49502
		// (set) Token: 0x0600C15D RID: 49501
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

		// Token: 0x17003F33 RID: 16179
		// (get) Token: 0x0600C160 RID: 49504
		// (set) Token: 0x0600C15F RID: 49503
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

		// Token: 0x17003F34 RID: 16180
		// (get) Token: 0x0600C161 RID: 49505
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003F35 RID: 16181
		// (get) Token: 0x0600C162 RID: 49506
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003F36 RID: 16182
		// (get) Token: 0x0600C163 RID: 49507
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600C164 RID: 49508
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0600C165 RID: 49509
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17003F37 RID: 16183
		// (get) Token: 0x0600C167 RID: 49511
		// (set) Token: 0x0600C166 RID: 49510
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

		// Token: 0x0600C168 RID: 49512
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600C169 RID: 49513
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17003F38 RID: 16184
		// (get) Token: 0x0600C16B RID: 49515
		// (set) Token: 0x0600C16A RID: 49514
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F39 RID: 16185
		// (get) Token: 0x0600C16D RID: 49517
		// (set) Token: 0x0600C16C RID: 49516
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

		// Token: 0x17003F3A RID: 16186
		// (get) Token: 0x0600C16F RID: 49519
		// (set) Token: 0x0600C16E RID: 49518
		[DispId(-2147412062)]
		public virtual extern object ondragend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F3B RID: 16187
		// (get) Token: 0x0600C171 RID: 49521
		// (set) Token: 0x0600C170 RID: 49520
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

		// Token: 0x17003F3C RID: 16188
		// (get) Token: 0x0600C173 RID: 49523
		// (set) Token: 0x0600C172 RID: 49522
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

		// Token: 0x17003F3D RID: 16189
		// (get) Token: 0x0600C175 RID: 49525
		// (set) Token: 0x0600C174 RID: 49524
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

		// Token: 0x17003F3E RID: 16190
		// (get) Token: 0x0600C177 RID: 49527
		// (set) Token: 0x0600C176 RID: 49526
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

		// Token: 0x17003F3F RID: 16191
		// (get) Token: 0x0600C179 RID: 49529
		// (set) Token: 0x0600C178 RID: 49528
		[DispId(-2147412054)]
		public virtual extern object onbeforecut
		{
			[DispId(-2147412054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F40 RID: 16192
		// (get) Token: 0x0600C17B RID: 49531
		// (set) Token: 0x0600C17A RID: 49530
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

		// Token: 0x17003F41 RID: 16193
		// (get) Token: 0x0600C17D RID: 49533
		// (set) Token: 0x0600C17C RID: 49532
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

		// Token: 0x17003F42 RID: 16194
		// (get) Token: 0x0600C17F RID: 49535
		// (set) Token: 0x0600C17E RID: 49534
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

		// Token: 0x17003F43 RID: 16195
		// (get) Token: 0x0600C181 RID: 49537
		// (set) Token: 0x0600C180 RID: 49536
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

		// Token: 0x17003F44 RID: 16196
		// (get) Token: 0x0600C183 RID: 49539
		// (set) Token: 0x0600C182 RID: 49538
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

		// Token: 0x17003F45 RID: 16197
		// (get) Token: 0x0600C184 RID: 49540
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F46 RID: 16198
		// (get) Token: 0x0600C186 RID: 49542
		// (set) Token: 0x0600C185 RID: 49541
		[DispId(-2147412065)]
		public virtual extern object onpropertychange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600C187 RID: 49543
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x0600C188 RID: 49544
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x0600C189 RID: 49545
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600C18A RID: 49546
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600C18B RID: 49547
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17003F47 RID: 16199
		// (get) Token: 0x0600C18D RID: 49549
		// (set) Token: 0x0600C18C RID: 49548
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

		// Token: 0x0600C18E RID: 49550
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17003F48 RID: 16200
		// (get) Token: 0x0600C190 RID: 49552
		// (set) Token: 0x0600C18F RID: 49551
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

		// Token: 0x17003F49 RID: 16201
		// (get) Token: 0x0600C192 RID: 49554
		// (set) Token: 0x0600C191 RID: 49553
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

		// Token: 0x17003F4A RID: 16202
		// (get) Token: 0x0600C194 RID: 49556
		// (set) Token: 0x0600C193 RID: 49555
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

		// Token: 0x17003F4B RID: 16203
		// (get) Token: 0x0600C196 RID: 49558
		// (set) Token: 0x0600C195 RID: 49557
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

		// Token: 0x0600C197 RID: 49559
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600C198 RID: 49560
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600C199 RID: 49561
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17003F4C RID: 16204
		// (get) Token: 0x0600C19A RID: 49562
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F4D RID: 16205
		// (get) Token: 0x0600C19B RID: 49563
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F4E RID: 16206
		// (get) Token: 0x0600C19C RID: 49564
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F4F RID: 16207
		// (get) Token: 0x0600C19D RID: 49565
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C19E RID: 49566
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600C19F RID: 49567
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17003F50 RID: 16208
		// (get) Token: 0x0600C1A0 RID: 49568
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003F51 RID: 16209
		// (get) Token: 0x0600C1A2 RID: 49570
		// (set) Token: 0x0600C1A1 RID: 49569
		[DispId(-2147412087)]
		public virtual extern object onreadystatechange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F52 RID: 16210
		// (get) Token: 0x0600C1A4 RID: 49572
		// (set) Token: 0x0600C1A3 RID: 49571
		[DispId(-2147412050)]
		public virtual extern object onrowsdelete
		{
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F53 RID: 16211
		// (get) Token: 0x0600C1A6 RID: 49574
		// (set) Token: 0x0600C1A5 RID: 49573
		[DispId(-2147412049)]
		public virtual extern object onrowsinserted
		{
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F54 RID: 16212
		// (get) Token: 0x0600C1A8 RID: 49576
		// (set) Token: 0x0600C1A7 RID: 49575
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

		// Token: 0x17003F55 RID: 16213
		// (get) Token: 0x0600C1AA RID: 49578
		// (set) Token: 0x0600C1A9 RID: 49577
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

		// Token: 0x0600C1AB RID: 49579
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17003F56 RID: 16214
		// (get) Token: 0x0600C1AC RID: 49580
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F57 RID: 16215
		// (get) Token: 0x0600C1AD RID: 49581
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F58 RID: 16216
		// (get) Token: 0x0600C1AF RID: 49583
		// (set) Token: 0x0600C1AE RID: 49582
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

		// Token: 0x17003F59 RID: 16217
		// (get) Token: 0x0600C1B1 RID: 49585
		// (set) Token: 0x0600C1B0 RID: 49584
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

		// Token: 0x0600C1B2 RID: 49586
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17003F5A RID: 16218
		// (get) Token: 0x0600C1B4 RID: 49588
		// (set) Token: 0x0600C1B3 RID: 49587
		[DispId(-2147412047)]
		public virtual extern object oncontextmenu
		{
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600C1B5 RID: 49589
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600C1B6 RID: 49590
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600C1B7 RID: 49591
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600C1B8 RID: 49592
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17003F5B RID: 16219
		// (get) Token: 0x0600C1B9 RID: 49593
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C1BA RID: 49594
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600C1BB RID: 49595
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17003F5C RID: 16220
		// (get) Token: 0x0600C1BC RID: 49596
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F5D RID: 16221
		// (get) Token: 0x0600C1BD RID: 49597
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003F5E RID: 16222
		// (get) Token: 0x0600C1BF RID: 49599
		// (set) Token: 0x0600C1BE RID: 49598
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

		// Token: 0x17003F5F RID: 16223
		// (get) Token: 0x0600C1C1 RID: 49601
		// (set) Token: 0x0600C1C0 RID: 49600
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

		// Token: 0x17003F60 RID: 16224
		// (get) Token: 0x0600C1C2 RID: 49602
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C1C3 RID: 49603
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600C1C4 RID: 49604
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17003F61 RID: 16225
		// (get) Token: 0x0600C1C5 RID: 49605
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F62 RID: 16226
		// (get) Token: 0x0600C1C6 RID: 49606
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F63 RID: 16227
		// (get) Token: 0x0600C1C8 RID: 49608
		// (set) Token: 0x0600C1C7 RID: 49607
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

		// Token: 0x17003F64 RID: 16228
		// (get) Token: 0x0600C1CA RID: 49610
		// (set) Token: 0x0600C1C9 RID: 49609
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

		// Token: 0x17003F65 RID: 16229
		// (get) Token: 0x0600C1CC RID: 49612
		// (set) Token: 0x0600C1CB RID: 49611
		[DispId(-2147417012)]
		public virtual extern bool inflateBlock
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17003F66 RID: 16230
		// (get) Token: 0x0600C1CE RID: 49614
		// (set) Token: 0x0600C1CD RID: 49613
		[DispId(-2147412035)]
		public virtual extern object onbeforedeactivate
		{
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600C1CF RID: 49615
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17003F67 RID: 16231
		// (get) Token: 0x0600C1D1 RID: 49617
		// (set) Token: 0x0600C1D0 RID: 49616
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

		// Token: 0x17003F68 RID: 16232
		// (get) Token: 0x0600C1D2 RID: 49618
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F69 RID: 16233
		// (get) Token: 0x0600C1D4 RID: 49620
		// (set) Token: 0x0600C1D3 RID: 49619
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

		// Token: 0x17003F6A RID: 16234
		// (get) Token: 0x0600C1D6 RID: 49622
		// (set) Token: 0x0600C1D5 RID: 49621
		[DispId(-2147418036)]
		public virtual extern bool disabled
		{
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17003F6B RID: 16235
		// (get) Token: 0x0600C1D7 RID: 49623
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F6C RID: 16236
		// (get) Token: 0x0600C1D9 RID: 49625
		// (set) Token: 0x0600C1D8 RID: 49624
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

		// Token: 0x17003F6D RID: 16237
		// (get) Token: 0x0600C1DB RID: 49627
		// (set) Token: 0x0600C1DA RID: 49626
		[DispId(-2147412033)]
		public virtual extern object oncontrolselect
		{
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600C1DC RID: 49628
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17003F6E RID: 16238
		// (get) Token: 0x0600C1DE RID: 49630
		// (set) Token: 0x0600C1DD RID: 49629
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

		// Token: 0x17003F6F RID: 16239
		// (get) Token: 0x0600C1E0 RID: 49632
		// (set) Token: 0x0600C1DF RID: 49631
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

		// Token: 0x17003F70 RID: 16240
		// (get) Token: 0x0600C1E2 RID: 49634
		// (set) Token: 0x0600C1E1 RID: 49633
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

		// Token: 0x17003F71 RID: 16241
		// (get) Token: 0x0600C1E4 RID: 49636
		// (set) Token: 0x0600C1E3 RID: 49635
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

		// Token: 0x17003F72 RID: 16242
		// (get) Token: 0x0600C1E6 RID: 49638
		// (set) Token: 0x0600C1E5 RID: 49637
		[DispId(-2147412027)]
		public virtual extern object onmouseenter
		{
			[DispId(-2147412027)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412027)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F73 RID: 16243
		// (get) Token: 0x0600C1E8 RID: 49640
		// (set) Token: 0x0600C1E7 RID: 49639
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

		// Token: 0x17003F74 RID: 16244
		// (get) Token: 0x0600C1EA RID: 49642
		// (set) Token: 0x0600C1E9 RID: 49641
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F75 RID: 16245
		// (get) Token: 0x0600C1EC RID: 49644
		// (set) Token: 0x0600C1EB RID: 49643
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

		// Token: 0x0600C1ED RID: 49645
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17003F76 RID: 16246
		// (get) Token: 0x0600C1EE RID: 49646
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F77 RID: 16247
		// (get) Token: 0x0600C1F0 RID: 49648
		// (set) Token: 0x0600C1EF RID: 49647
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

		// Token: 0x0600C1F1 RID: 49649
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600C1F2 RID: 49650
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600C1F3 RID: 49651
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600C1F4 RID: 49652
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17003F78 RID: 16248
		// (get) Token: 0x0600C1F6 RID: 49654
		// (set) Token: 0x0600C1F5 RID: 49653
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

		// Token: 0x17003F79 RID: 16249
		// (get) Token: 0x0600C1F8 RID: 49656
		// (set) Token: 0x0600C1F7 RID: 49655
		[DispId(-2147412021)]
		public virtual extern object onfocusin
		{
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003F7A RID: 16250
		// (get) Token: 0x0600C1FA RID: 49658
		// (set) Token: 0x0600C1F9 RID: 49657
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

		// Token: 0x17003F7B RID: 16251
		// (get) Token: 0x0600C1FB RID: 49659
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F7C RID: 16252
		// (get) Token: 0x0600C1FC RID: 49660
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003F7D RID: 16253
		// (get) Token: 0x0600C1FD RID: 49661
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F7E RID: 16254
		// (get) Token: 0x0600C1FE RID: 49662
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600C1FF RID: 49663
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17003F7F RID: 16255
		// (get) Token: 0x0600C200 RID: 49664
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003F80 RID: 16256
		// (get) Token: 0x0600C201 RID: 49665
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600C202 RID: 49666
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600C203 RID: 49667
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600C204 RID: 49668
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600C205 RID: 49669
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600C206 RID: 49670
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600C207 RID: 49671
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600C208 RID: 49672
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600C209 RID: 49673
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17003F81 RID: 16257
		// (get) Token: 0x0600C20A RID: 49674
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003F82 RID: 16258
		// (get) Token: 0x0600C20C RID: 49676
		// (set) Token: 0x0600C20B RID: 49675
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

		// Token: 0x17003F83 RID: 16259
		// (get) Token: 0x0600C20D RID: 49677
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F84 RID: 16260
		// (get) Token: 0x0600C20E RID: 49678
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F85 RID: 16261
		// (get) Token: 0x0600C20F RID: 49679
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F86 RID: 16262
		// (get) Token: 0x0600C210 RID: 49680
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F87 RID: 16263
		// (get) Token: 0x0600C211 RID: 49681
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003F88 RID: 16264
		// (get) Token: 0x0600C213 RID: 49683
		// (set) Token: 0x0600C212 RID: 49682
		[DispId(1003)]
		public virtual extern string href
		{
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003F89 RID: 16265
		// (get) Token: 0x0600C215 RID: 49685
		// (set) Token: 0x0600C214 RID: 49684
		[DispId(1004)]
		public virtual extern string target
		{
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600C216 RID: 49686
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600C217 RID: 49687
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600C218 RID: 49688
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17003F8A RID: 16266
		// (get) Token: 0x0600C21A RID: 49690
		// (set) Token: 0x0600C219 RID: 49689
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

		// Token: 0x17003F8B RID: 16267
		// (get) Token: 0x0600C21C RID: 49692
		// (set) Token: 0x0600C21B RID: 49691
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

		// Token: 0x17003F8C RID: 16268
		// (get) Token: 0x0600C21D RID: 49693
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003F8D RID: 16269
		// (get) Token: 0x0600C21E RID: 49694
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F8E RID: 16270
		// (get) Token: 0x0600C21F RID: 49695
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003F8F RID: 16271
		// (get) Token: 0x0600C221 RID: 49697
		// (set) Token: 0x0600C220 RID: 49696
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

		// Token: 0x17003F90 RID: 16272
		// (get) Token: 0x0600C223 RID: 49699
		// (set) Token: 0x0600C222 RID: 49698
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

		// Token: 0x17003F91 RID: 16273
		// (get) Token: 0x0600C225 RID: 49701
		// (set) Token: 0x0600C224 RID: 49700
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

		// Token: 0x17003F92 RID: 16274
		// (get) Token: 0x0600C227 RID: 49703
		// (set) Token: 0x0600C226 RID: 49702
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

		// Token: 0x17003F93 RID: 16275
		// (get) Token: 0x0600C229 RID: 49705
		// (set) Token: 0x0600C228 RID: 49704
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

		// Token: 0x17003F94 RID: 16276
		// (get) Token: 0x0600C22B RID: 49707
		// (set) Token: 0x0600C22A RID: 49706
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

		// Token: 0x17003F95 RID: 16277
		// (get) Token: 0x0600C22D RID: 49709
		// (set) Token: 0x0600C22C RID: 49708
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

		// Token: 0x17003F96 RID: 16278
		// (get) Token: 0x0600C22F RID: 49711
		// (set) Token: 0x0600C22E RID: 49710
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

		// Token: 0x17003F97 RID: 16279
		// (get) Token: 0x0600C231 RID: 49713
		// (set) Token: 0x0600C230 RID: 49712
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

		// Token: 0x17003F98 RID: 16280
		// (get) Token: 0x0600C233 RID: 49715
		// (set) Token: 0x0600C232 RID: 49714
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

		// Token: 0x17003F99 RID: 16281
		// (get) Token: 0x0600C235 RID: 49717
		// (set) Token: 0x0600C234 RID: 49716
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

		// Token: 0x17003F9A RID: 16282
		// (get) Token: 0x0600C236 RID: 49718
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003F9B RID: 16283
		// (get) Token: 0x0600C238 RID: 49720
		// (set) Token: 0x0600C237 RID: 49719
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

		// Token: 0x17003F9C RID: 16284
		// (get) Token: 0x0600C23A RID: 49722
		// (set) Token: 0x0600C239 RID: 49721
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

		// Token: 0x17003F9D RID: 16285
		// (get) Token: 0x0600C23C RID: 49724
		// (set) Token: 0x0600C23B RID: 49723
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

		// Token: 0x0600C23D RID: 49725
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600C23E RID: 49726
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17003F9E RID: 16286
		// (get) Token: 0x0600C23F RID: 49727
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003F9F RID: 16287
		// (get) Token: 0x0600C240 RID: 49728
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003FA0 RID: 16288
		// (get) Token: 0x0600C242 RID: 49730
		// (set) Token: 0x0600C241 RID: 49729
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

		// Token: 0x17003FA1 RID: 16289
		// (get) Token: 0x0600C243 RID: 49731
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FA2 RID: 16290
		// (get) Token: 0x0600C244 RID: 49732
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FA3 RID: 16291
		// (get) Token: 0x0600C245 RID: 49733
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FA4 RID: 16292
		// (get) Token: 0x0600C246 RID: 49734
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FA5 RID: 16293
		// (get) Token: 0x0600C247 RID: 49735
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003FA6 RID: 16294
		// (get) Token: 0x0600C249 RID: 49737
		// (set) Token: 0x0600C248 RID: 49736
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

		// Token: 0x17003FA7 RID: 16295
		// (get) Token: 0x0600C24B RID: 49739
		// (set) Token: 0x0600C24A RID: 49738
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

		// Token: 0x17003FA8 RID: 16296
		// (get) Token: 0x0600C24D RID: 49741
		// (set) Token: 0x0600C24C RID: 49740
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

		// Token: 0x17003FA9 RID: 16297
		// (get) Token: 0x0600C24F RID: 49743
		// (set) Token: 0x0600C24E RID: 49742
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

		// Token: 0x0600C250 RID: 49744
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600C251 RID: 49745
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17003FAA RID: 16298
		// (get) Token: 0x0600C252 RID: 49746
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003FAB RID: 16299
		// (get) Token: 0x0600C253 RID: 49747
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C254 RID: 49748
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17003FAC RID: 16300
		// (get) Token: 0x0600C255 RID: 49749
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003FAD RID: 16301
		// (get) Token: 0x0600C257 RID: 49751
		// (set) Token: 0x0600C256 RID: 49750
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

		// Token: 0x0600C258 RID: 49752
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17003FAE RID: 16302
		// (get) Token: 0x0600C25A RID: 49754
		// (set) Token: 0x0600C259 RID: 49753
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

		// Token: 0x17003FAF RID: 16303
		// (get) Token: 0x0600C25C RID: 49756
		// (set) Token: 0x0600C25B RID: 49755
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

		// Token: 0x17003FB0 RID: 16304
		// (get) Token: 0x0600C25E RID: 49758
		// (set) Token: 0x0600C25D RID: 49757
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

		// Token: 0x17003FB1 RID: 16305
		// (get) Token: 0x0600C260 RID: 49760
		// (set) Token: 0x0600C25F RID: 49759
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

		// Token: 0x17003FB2 RID: 16306
		// (get) Token: 0x0600C262 RID: 49762
		// (set) Token: 0x0600C261 RID: 49761
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

		// Token: 0x17003FB3 RID: 16307
		// (get) Token: 0x0600C264 RID: 49764
		// (set) Token: 0x0600C263 RID: 49763
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

		// Token: 0x17003FB4 RID: 16308
		// (get) Token: 0x0600C266 RID: 49766
		// (set) Token: 0x0600C265 RID: 49765
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

		// Token: 0x17003FB5 RID: 16309
		// (get) Token: 0x0600C268 RID: 49768
		// (set) Token: 0x0600C267 RID: 49767
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

		// Token: 0x17003FB6 RID: 16310
		// (get) Token: 0x0600C26A RID: 49770
		// (set) Token: 0x0600C269 RID: 49769
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

		// Token: 0x17003FB7 RID: 16311
		// (get) Token: 0x0600C26B RID: 49771
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003FB8 RID: 16312
		// (get) Token: 0x0600C26C RID: 49772
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003FB9 RID: 16313
		// (get) Token: 0x0600C26D RID: 49773
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600C26E RID: 49774
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600C26F RID: 49775
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17003FBA RID: 16314
		// (get) Token: 0x0600C271 RID: 49777
		// (set) Token: 0x0600C270 RID: 49776
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

		// Token: 0x0600C272 RID: 49778
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600C273 RID: 49779
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17003FBB RID: 16315
		// (get) Token: 0x0600C275 RID: 49781
		// (set) Token: 0x0600C274 RID: 49780
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

		// Token: 0x17003FBC RID: 16316
		// (get) Token: 0x0600C277 RID: 49783
		// (set) Token: 0x0600C276 RID: 49782
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

		// Token: 0x17003FBD RID: 16317
		// (get) Token: 0x0600C279 RID: 49785
		// (set) Token: 0x0600C278 RID: 49784
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

		// Token: 0x17003FBE RID: 16318
		// (get) Token: 0x0600C27B RID: 49787
		// (set) Token: 0x0600C27A RID: 49786
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

		// Token: 0x17003FBF RID: 16319
		// (get) Token: 0x0600C27D RID: 49789
		// (set) Token: 0x0600C27C RID: 49788
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

		// Token: 0x17003FC0 RID: 16320
		// (get) Token: 0x0600C27F RID: 49791
		// (set) Token: 0x0600C27E RID: 49790
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

		// Token: 0x17003FC1 RID: 16321
		// (get) Token: 0x0600C281 RID: 49793
		// (set) Token: 0x0600C280 RID: 49792
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

		// Token: 0x17003FC2 RID: 16322
		// (get) Token: 0x0600C283 RID: 49795
		// (set) Token: 0x0600C282 RID: 49794
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

		// Token: 0x17003FC3 RID: 16323
		// (get) Token: 0x0600C285 RID: 49797
		// (set) Token: 0x0600C284 RID: 49796
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

		// Token: 0x17003FC4 RID: 16324
		// (get) Token: 0x0600C287 RID: 49799
		// (set) Token: 0x0600C286 RID: 49798
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

		// Token: 0x17003FC5 RID: 16325
		// (get) Token: 0x0600C289 RID: 49801
		// (set) Token: 0x0600C288 RID: 49800
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

		// Token: 0x17003FC6 RID: 16326
		// (get) Token: 0x0600C28B RID: 49803
		// (set) Token: 0x0600C28A RID: 49802
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

		// Token: 0x17003FC7 RID: 16327
		// (get) Token: 0x0600C28D RID: 49805
		// (set) Token: 0x0600C28C RID: 49804
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

		// Token: 0x17003FC8 RID: 16328
		// (get) Token: 0x0600C28E RID: 49806
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003FC9 RID: 16329
		// (get) Token: 0x0600C290 RID: 49808
		// (set) Token: 0x0600C28F RID: 49807
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

		// Token: 0x0600C291 RID: 49809
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600C292 RID: 49810
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0600C293 RID: 49811
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600C294 RID: 49812
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600C295 RID: 49813
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17003FCA RID: 16330
		// (get) Token: 0x0600C297 RID: 49815
		// (set) Token: 0x0600C296 RID: 49814
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

		// Token: 0x0600C298 RID: 49816
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17003FCB RID: 16331
		// (get) Token: 0x0600C29A RID: 49818
		// (set) Token: 0x0600C299 RID: 49817
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

		// Token: 0x17003FCC RID: 16332
		// (get) Token: 0x0600C29C RID: 49820
		// (set) Token: 0x0600C29B RID: 49819
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

		// Token: 0x17003FCD RID: 16333
		// (get) Token: 0x0600C29E RID: 49822
		// (set) Token: 0x0600C29D RID: 49821
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

		// Token: 0x17003FCE RID: 16334
		// (get) Token: 0x0600C2A0 RID: 49824
		// (set) Token: 0x0600C29F RID: 49823
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

		// Token: 0x0600C2A1 RID: 49825
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600C2A2 RID: 49826
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600C2A3 RID: 49827
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17003FCF RID: 16335
		// (get) Token: 0x0600C2A4 RID: 49828
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FD0 RID: 16336
		// (get) Token: 0x0600C2A5 RID: 49829
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FD1 RID: 16337
		// (get) Token: 0x0600C2A6 RID: 49830
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FD2 RID: 16338
		// (get) Token: 0x0600C2A7 RID: 49831
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C2A8 RID: 49832
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600C2A9 RID: 49833
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17003FD3 RID: 16339
		// (get) Token: 0x0600C2AA RID: 49834
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003FD4 RID: 16340
		// (get) Token: 0x0600C2AC RID: 49836
		// (set) Token: 0x0600C2AB RID: 49835
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

		// Token: 0x17003FD5 RID: 16341
		// (get) Token: 0x0600C2AE RID: 49838
		// (set) Token: 0x0600C2AD RID: 49837
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

		// Token: 0x17003FD6 RID: 16342
		// (get) Token: 0x0600C2B0 RID: 49840
		// (set) Token: 0x0600C2AF RID: 49839
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

		// Token: 0x17003FD7 RID: 16343
		// (get) Token: 0x0600C2B2 RID: 49842
		// (set) Token: 0x0600C2B1 RID: 49841
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

		// Token: 0x17003FD8 RID: 16344
		// (get) Token: 0x0600C2B4 RID: 49844
		// (set) Token: 0x0600C2B3 RID: 49843
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

		// Token: 0x0600C2B5 RID: 49845
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17003FD9 RID: 16345
		// (get) Token: 0x0600C2B6 RID: 49846
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FDA RID: 16346
		// (get) Token: 0x0600C2B7 RID: 49847
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FDB RID: 16347
		// (get) Token: 0x0600C2B9 RID: 49849
		// (set) Token: 0x0600C2B8 RID: 49848
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

		// Token: 0x17003FDC RID: 16348
		// (get) Token: 0x0600C2BB RID: 49851
		// (set) Token: 0x0600C2BA RID: 49850
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

		// Token: 0x0600C2BC RID: 49852
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600C2BD RID: 49853
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17003FDD RID: 16349
		// (get) Token: 0x0600C2BF RID: 49855
		// (set) Token: 0x0600C2BE RID: 49854
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

		// Token: 0x0600C2C0 RID: 49856
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600C2C1 RID: 49857
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600C2C2 RID: 49858
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600C2C3 RID: 49859
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17003FDE RID: 16350
		// (get) Token: 0x0600C2C4 RID: 49860
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C2C5 RID: 49861
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600C2C6 RID: 49862
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17003FDF RID: 16351
		// (get) Token: 0x0600C2C7 RID: 49863
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003FE0 RID: 16352
		// (get) Token: 0x0600C2C8 RID: 49864
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003FE1 RID: 16353
		// (get) Token: 0x0600C2CA RID: 49866
		// (set) Token: 0x0600C2C9 RID: 49865
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

		// Token: 0x17003FE2 RID: 16354
		// (get) Token: 0x0600C2CC RID: 49868
		// (set) Token: 0x0600C2CB RID: 49867
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

		// Token: 0x17003FE3 RID: 16355
		// (get) Token: 0x0600C2CD RID: 49869
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C2CE RID: 49870
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600C2CF RID: 49871
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17003FE4 RID: 16356
		// (get) Token: 0x0600C2D0 RID: 49872
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FE5 RID: 16357
		// (get) Token: 0x0600C2D1 RID: 49873
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FE6 RID: 16358
		// (get) Token: 0x0600C2D3 RID: 49875
		// (set) Token: 0x0600C2D2 RID: 49874
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

		// Token: 0x17003FE7 RID: 16359
		// (get) Token: 0x0600C2D5 RID: 49877
		// (set) Token: 0x0600C2D4 RID: 49876
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

		// Token: 0x17003FE8 RID: 16360
		// (get) Token: 0x0600C2D7 RID: 49879
		// (set) Token: 0x0600C2D6 RID: 49878
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

		// Token: 0x17003FE9 RID: 16361
		// (get) Token: 0x0600C2D9 RID: 49881
		// (set) Token: 0x0600C2D8 RID: 49880
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

		// Token: 0x0600C2DA RID: 49882
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17003FEA RID: 16362
		// (get) Token: 0x0600C2DC RID: 49884
		// (set) Token: 0x0600C2DB RID: 49883
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

		// Token: 0x17003FEB RID: 16363
		// (get) Token: 0x0600C2DD RID: 49885
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FEC RID: 16364
		// (get) Token: 0x0600C2DF RID: 49887
		// (set) Token: 0x0600C2DE RID: 49886
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

		// Token: 0x17003FED RID: 16365
		// (get) Token: 0x0600C2E1 RID: 49889
		// (set) Token: 0x0600C2E0 RID: 49888
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

		// Token: 0x17003FEE RID: 16366
		// (get) Token: 0x0600C2E2 RID: 49890
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FEF RID: 16367
		// (get) Token: 0x0600C2E4 RID: 49892
		// (set) Token: 0x0600C2E3 RID: 49891
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

		// Token: 0x17003FF0 RID: 16368
		// (get) Token: 0x0600C2E6 RID: 49894
		// (set) Token: 0x0600C2E5 RID: 49893
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

		// Token: 0x0600C2E7 RID: 49895
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17003FF1 RID: 16369
		// (get) Token: 0x0600C2E9 RID: 49897
		// (set) Token: 0x0600C2E8 RID: 49896
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

		// Token: 0x17003FF2 RID: 16370
		// (get) Token: 0x0600C2EB RID: 49899
		// (set) Token: 0x0600C2EA RID: 49898
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

		// Token: 0x17003FF3 RID: 16371
		// (get) Token: 0x0600C2ED RID: 49901
		// (set) Token: 0x0600C2EC RID: 49900
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

		// Token: 0x17003FF4 RID: 16372
		// (get) Token: 0x0600C2EF RID: 49903
		// (set) Token: 0x0600C2EE RID: 49902
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

		// Token: 0x17003FF5 RID: 16373
		// (get) Token: 0x0600C2F1 RID: 49905
		// (set) Token: 0x0600C2F0 RID: 49904
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

		// Token: 0x17003FF6 RID: 16374
		// (get) Token: 0x0600C2F3 RID: 49907
		// (set) Token: 0x0600C2F2 RID: 49906
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

		// Token: 0x17003FF7 RID: 16375
		// (get) Token: 0x0600C2F5 RID: 49909
		// (set) Token: 0x0600C2F4 RID: 49908
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

		// Token: 0x17003FF8 RID: 16376
		// (get) Token: 0x0600C2F7 RID: 49911
		// (set) Token: 0x0600C2F6 RID: 49910
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

		// Token: 0x0600C2F8 RID: 49912
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17003FF9 RID: 16377
		// (get) Token: 0x0600C2F9 RID: 49913
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FFA RID: 16378
		// (get) Token: 0x0600C2FB RID: 49915
		// (set) Token: 0x0600C2FA RID: 49914
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

		// Token: 0x0600C2FC RID: 49916
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x0600C2FD RID: 49917
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600C2FE RID: 49918
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600C2FF RID: 49919
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17003FFB RID: 16379
		// (get) Token: 0x0600C301 RID: 49921
		// (set) Token: 0x0600C300 RID: 49920
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

		// Token: 0x17003FFC RID: 16380
		// (get) Token: 0x0600C303 RID: 49923
		// (set) Token: 0x0600C302 RID: 49922
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

		// Token: 0x17003FFD RID: 16381
		// (get) Token: 0x0600C305 RID: 49925
		// (set) Token: 0x0600C304 RID: 49924
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

		// Token: 0x17003FFE RID: 16382
		// (get) Token: 0x0600C306 RID: 49926
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003FFF RID: 16383
		// (get) Token: 0x0600C307 RID: 49927
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004000 RID: 16384
		// (get) Token: 0x0600C308 RID: 49928
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004001 RID: 16385
		// (get) Token: 0x0600C309 RID: 49929
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600C30A RID: 49930
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17004002 RID: 16386
		// (get) Token: 0x0600C30B RID: 49931
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004003 RID: 16387
		// (get) Token: 0x0600C30C RID: 49932
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600C30D RID: 49933
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600C30E RID: 49934
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600C30F RID: 49935
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600C310 RID: 49936
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600C311 RID: 49937
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600C312 RID: 49938
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600C313 RID: 49939
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600C314 RID: 49940
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004004 RID: 16388
		// (get) Token: 0x0600C315 RID: 49941
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004005 RID: 16389
		// (get) Token: 0x0600C317 RID: 49943
		// (set) Token: 0x0600C316 RID: 49942
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

		// Token: 0x17004006 RID: 16390
		// (get) Token: 0x0600C318 RID: 49944
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004007 RID: 16391
		// (get) Token: 0x0600C319 RID: 49945
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004008 RID: 16392
		// (get) Token: 0x0600C31A RID: 49946
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004009 RID: 16393
		// (get) Token: 0x0600C31B RID: 49947
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700400A RID: 16394
		// (get) Token: 0x0600C31C RID: 49948
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700400B RID: 16395
		// (get) Token: 0x0600C31E RID: 49950
		// (set) Token: 0x0600C31D RID: 49949
		public virtual extern string IHTMLBaseElement_href
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

		// Token: 0x1700400C RID: 16396
		// (get) Token: 0x0600C320 RID: 49952
		// (set) Token: 0x0600C31F RID: 49951
		public virtual extern string IHTMLBaseElement_target
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

		// Token: 0x140017AA RID: 6058
		// (add) Token: 0x0600C321 RID: 49953
		// (remove) Token: 0x0600C322 RID: 49954
		public virtual extern event HTMLElementEvents_onhelpEventHandler HTMLElementEvents_Event_onhelp;

		// Token: 0x140017AB RID: 6059
		// (add) Token: 0x0600C323 RID: 49955
		// (remove) Token: 0x0600C324 RID: 49956
		public virtual extern event HTMLElementEvents_onclickEventHandler HTMLElementEvents_Event_onclick;

		// Token: 0x140017AC RID: 6060
		// (add) Token: 0x0600C325 RID: 49957
		// (remove) Token: 0x0600C326 RID: 49958
		public virtual extern event HTMLElementEvents_ondblclickEventHandler HTMLElementEvents_Event_ondblclick;

		// Token: 0x140017AD RID: 6061
		// (add) Token: 0x0600C327 RID: 49959
		// (remove) Token: 0x0600C328 RID: 49960
		public virtual extern event HTMLElementEvents_onkeypressEventHandler HTMLElementEvents_Event_onkeypress;

		// Token: 0x140017AE RID: 6062
		// (add) Token: 0x0600C329 RID: 49961
		// (remove) Token: 0x0600C32A RID: 49962
		public virtual extern event HTMLElementEvents_onkeydownEventHandler HTMLElementEvents_Event_onkeydown;

		// Token: 0x140017AF RID: 6063
		// (add) Token: 0x0600C32B RID: 49963
		// (remove) Token: 0x0600C32C RID: 49964
		public virtual extern event HTMLElementEvents_onkeyupEventHandler HTMLElementEvents_Event_onkeyup;

		// Token: 0x140017B0 RID: 6064
		// (add) Token: 0x0600C32D RID: 49965
		// (remove) Token: 0x0600C32E RID: 49966
		public virtual extern event HTMLElementEvents_onmouseoutEventHandler HTMLElementEvents_Event_onmouseout;

		// Token: 0x140017B1 RID: 6065
		// (add) Token: 0x0600C32F RID: 49967
		// (remove) Token: 0x0600C330 RID: 49968
		public virtual extern event HTMLElementEvents_onmouseoverEventHandler HTMLElementEvents_Event_onmouseover;

		// Token: 0x140017B2 RID: 6066
		// (add) Token: 0x0600C331 RID: 49969
		// (remove) Token: 0x0600C332 RID: 49970
		public virtual extern event HTMLElementEvents_onmousemoveEventHandler HTMLElementEvents_Event_onmousemove;

		// Token: 0x140017B3 RID: 6067
		// (add) Token: 0x0600C333 RID: 49971
		// (remove) Token: 0x0600C334 RID: 49972
		public virtual extern event HTMLElementEvents_onmousedownEventHandler HTMLElementEvents_Event_onmousedown;

		// Token: 0x140017B4 RID: 6068
		// (add) Token: 0x0600C335 RID: 49973
		// (remove) Token: 0x0600C336 RID: 49974
		public virtual extern event HTMLElementEvents_onmouseupEventHandler HTMLElementEvents_Event_onmouseup;

		// Token: 0x140017B5 RID: 6069
		// (add) Token: 0x0600C337 RID: 49975
		// (remove) Token: 0x0600C338 RID: 49976
		public virtual extern event HTMLElementEvents_onselectstartEventHandler HTMLElementEvents_Event_onselectstart;

		// Token: 0x140017B6 RID: 6070
		// (add) Token: 0x0600C339 RID: 49977
		// (remove) Token: 0x0600C33A RID: 49978
		public virtual extern event HTMLElementEvents_onfilterchangeEventHandler HTMLElementEvents_Event_onfilterchange;

		// Token: 0x140017B7 RID: 6071
		// (add) Token: 0x0600C33B RID: 49979
		// (remove) Token: 0x0600C33C RID: 49980
		public virtual extern event HTMLElementEvents_ondragstartEventHandler HTMLElementEvents_Event_ondragstart;

		// Token: 0x140017B8 RID: 6072
		// (add) Token: 0x0600C33D RID: 49981
		// (remove) Token: 0x0600C33E RID: 49982
		public virtual extern event HTMLElementEvents_onbeforeupdateEventHandler HTMLElementEvents_Event_onbeforeupdate;

		// Token: 0x140017B9 RID: 6073
		// (add) Token: 0x0600C33F RID: 49983
		// (remove) Token: 0x0600C340 RID: 49984
		public virtual extern event HTMLElementEvents_onafterupdateEventHandler HTMLElementEvents_Event_onafterupdate;

		// Token: 0x140017BA RID: 6074
		// (add) Token: 0x0600C341 RID: 49985
		// (remove) Token: 0x0600C342 RID: 49986
		public virtual extern event HTMLElementEvents_onerrorupdateEventHandler HTMLElementEvents_Event_onerrorupdate;

		// Token: 0x140017BB RID: 6075
		// (add) Token: 0x0600C343 RID: 49987
		// (remove) Token: 0x0600C344 RID: 49988
		public virtual extern event HTMLElementEvents_onrowexitEventHandler HTMLElementEvents_Event_onrowexit;

		// Token: 0x140017BC RID: 6076
		// (add) Token: 0x0600C345 RID: 49989
		// (remove) Token: 0x0600C346 RID: 49990
		public virtual extern event HTMLElementEvents_onrowenterEventHandler HTMLElementEvents_Event_onrowenter;

		// Token: 0x140017BD RID: 6077
		// (add) Token: 0x0600C347 RID: 49991
		// (remove) Token: 0x0600C348 RID: 49992
		public virtual extern event HTMLElementEvents_ondatasetchangedEventHandler HTMLElementEvents_Event_ondatasetchanged;

		// Token: 0x140017BE RID: 6078
		// (add) Token: 0x0600C349 RID: 49993
		// (remove) Token: 0x0600C34A RID: 49994
		public virtual extern event HTMLElementEvents_ondataavailableEventHandler HTMLElementEvents_Event_ondataavailable;

		// Token: 0x140017BF RID: 6079
		// (add) Token: 0x0600C34B RID: 49995
		// (remove) Token: 0x0600C34C RID: 49996
		public virtual extern event HTMLElementEvents_ondatasetcompleteEventHandler HTMLElementEvents_Event_ondatasetcomplete;

		// Token: 0x140017C0 RID: 6080
		// (add) Token: 0x0600C34D RID: 49997
		// (remove) Token: 0x0600C34E RID: 49998
		public virtual extern event HTMLElementEvents_onlosecaptureEventHandler HTMLElementEvents_Event_onlosecapture;

		// Token: 0x140017C1 RID: 6081
		// (add) Token: 0x0600C34F RID: 49999
		// (remove) Token: 0x0600C350 RID: 50000
		public virtual extern event HTMLElementEvents_onpropertychangeEventHandler HTMLElementEvents_Event_onpropertychange;

		// Token: 0x140017C2 RID: 6082
		// (add) Token: 0x0600C351 RID: 50001
		// (remove) Token: 0x0600C352 RID: 50002
		public virtual extern event HTMLElementEvents_onscrollEventHandler HTMLElementEvents_Event_onscroll;

		// Token: 0x140017C3 RID: 6083
		// (add) Token: 0x0600C353 RID: 50003
		// (remove) Token: 0x0600C354 RID: 50004
		public virtual extern event HTMLElementEvents_onfocusEventHandler HTMLElementEvents_Event_onfocus;

		// Token: 0x140017C4 RID: 6084
		// (add) Token: 0x0600C355 RID: 50005
		// (remove) Token: 0x0600C356 RID: 50006
		public virtual extern event HTMLElementEvents_onblurEventHandler HTMLElementEvents_Event_onblur;

		// Token: 0x140017C5 RID: 6085
		// (add) Token: 0x0600C357 RID: 50007
		// (remove) Token: 0x0600C358 RID: 50008
		public virtual extern event HTMLElementEvents_onresizeEventHandler HTMLElementEvents_Event_onresize;

		// Token: 0x140017C6 RID: 6086
		// (add) Token: 0x0600C359 RID: 50009
		// (remove) Token: 0x0600C35A RID: 50010
		public virtual extern event HTMLElementEvents_ondragEventHandler HTMLElementEvents_Event_ondrag;

		// Token: 0x140017C7 RID: 6087
		// (add) Token: 0x0600C35B RID: 50011
		// (remove) Token: 0x0600C35C RID: 50012
		public virtual extern event HTMLElementEvents_ondragendEventHandler HTMLElementEvents_Event_ondragend;

		// Token: 0x140017C8 RID: 6088
		// (add) Token: 0x0600C35D RID: 50013
		// (remove) Token: 0x0600C35E RID: 50014
		public virtual extern event HTMLElementEvents_ondragenterEventHandler HTMLElementEvents_Event_ondragenter;

		// Token: 0x140017C9 RID: 6089
		// (add) Token: 0x0600C35F RID: 50015
		// (remove) Token: 0x0600C360 RID: 50016
		public virtual extern event HTMLElementEvents_ondragoverEventHandler HTMLElementEvents_Event_ondragover;

		// Token: 0x140017CA RID: 6090
		// (add) Token: 0x0600C361 RID: 50017
		// (remove) Token: 0x0600C362 RID: 50018
		public virtual extern event HTMLElementEvents_ondragleaveEventHandler HTMLElementEvents_Event_ondragleave;

		// Token: 0x140017CB RID: 6091
		// (add) Token: 0x0600C363 RID: 50019
		// (remove) Token: 0x0600C364 RID: 50020
		public virtual extern event HTMLElementEvents_ondropEventHandler HTMLElementEvents_Event_ondrop;

		// Token: 0x140017CC RID: 6092
		// (add) Token: 0x0600C365 RID: 50021
		// (remove) Token: 0x0600C366 RID: 50022
		public virtual extern event HTMLElementEvents_onbeforecutEventHandler HTMLElementEvents_Event_onbeforecut;

		// Token: 0x140017CD RID: 6093
		// (add) Token: 0x0600C367 RID: 50023
		// (remove) Token: 0x0600C368 RID: 50024
		public virtual extern event HTMLElementEvents_oncutEventHandler HTMLElementEvents_Event_oncut;

		// Token: 0x140017CE RID: 6094
		// (add) Token: 0x0600C369 RID: 50025
		// (remove) Token: 0x0600C36A RID: 50026
		public virtual extern event HTMLElementEvents_onbeforecopyEventHandler HTMLElementEvents_Event_onbeforecopy;

		// Token: 0x140017CF RID: 6095
		// (add) Token: 0x0600C36B RID: 50027
		// (remove) Token: 0x0600C36C RID: 50028
		public virtual extern event HTMLElementEvents_oncopyEventHandler HTMLElementEvents_Event_oncopy;

		// Token: 0x140017D0 RID: 6096
		// (add) Token: 0x0600C36D RID: 50029
		// (remove) Token: 0x0600C36E RID: 50030
		public virtual extern event HTMLElementEvents_onbeforepasteEventHandler HTMLElementEvents_Event_onbeforepaste;

		// Token: 0x140017D1 RID: 6097
		// (add) Token: 0x0600C36F RID: 50031
		// (remove) Token: 0x0600C370 RID: 50032
		public virtual extern event HTMLElementEvents_onpasteEventHandler HTMLElementEvents_Event_onpaste;

		// Token: 0x140017D2 RID: 6098
		// (add) Token: 0x0600C371 RID: 50033
		// (remove) Token: 0x0600C372 RID: 50034
		public virtual extern event HTMLElementEvents_oncontextmenuEventHandler HTMLElementEvents_Event_oncontextmenu;

		// Token: 0x140017D3 RID: 6099
		// (add) Token: 0x0600C373 RID: 50035
		// (remove) Token: 0x0600C374 RID: 50036
		public virtual extern event HTMLElementEvents_onrowsdeleteEventHandler HTMLElementEvents_Event_onrowsdelete;

		// Token: 0x140017D4 RID: 6100
		// (add) Token: 0x0600C375 RID: 50037
		// (remove) Token: 0x0600C376 RID: 50038
		public virtual extern event HTMLElementEvents_onrowsinsertedEventHandler HTMLElementEvents_Event_onrowsinserted;

		// Token: 0x140017D5 RID: 6101
		// (add) Token: 0x0600C377 RID: 50039
		// (remove) Token: 0x0600C378 RID: 50040
		public virtual extern event HTMLElementEvents_oncellchangeEventHandler HTMLElementEvents_Event_oncellchange;

		// Token: 0x140017D6 RID: 6102
		// (add) Token: 0x0600C379 RID: 50041
		// (remove) Token: 0x0600C37A RID: 50042
		public virtual extern event HTMLElementEvents_onreadystatechangeEventHandler HTMLElementEvents_Event_onreadystatechange;

		// Token: 0x140017D7 RID: 6103
		// (add) Token: 0x0600C37B RID: 50043
		// (remove) Token: 0x0600C37C RID: 50044
		public virtual extern event HTMLElementEvents_onbeforeeditfocusEventHandler HTMLElementEvents_Event_onbeforeeditfocus;

		// Token: 0x140017D8 RID: 6104
		// (add) Token: 0x0600C37D RID: 50045
		// (remove) Token: 0x0600C37E RID: 50046
		public virtual extern event HTMLElementEvents_onlayoutcompleteEventHandler HTMLElementEvents_Event_onlayoutcomplete;

		// Token: 0x140017D9 RID: 6105
		// (add) Token: 0x0600C37F RID: 50047
		// (remove) Token: 0x0600C380 RID: 50048
		public virtual extern event HTMLElementEvents_onpageEventHandler HTMLElementEvents_Event_onpage;

		// Token: 0x140017DA RID: 6106
		// (add) Token: 0x0600C381 RID: 50049
		// (remove) Token: 0x0600C382 RID: 50050
		public virtual extern event HTMLElementEvents_onbeforedeactivateEventHandler HTMLElementEvents_Event_onbeforedeactivate;

		// Token: 0x140017DB RID: 6107
		// (add) Token: 0x0600C383 RID: 50051
		// (remove) Token: 0x0600C384 RID: 50052
		public virtual extern event HTMLElementEvents_onbeforeactivateEventHandler HTMLElementEvents_Event_onbeforeactivate;

		// Token: 0x140017DC RID: 6108
		// (add) Token: 0x0600C385 RID: 50053
		// (remove) Token: 0x0600C386 RID: 50054
		public virtual extern event HTMLElementEvents_onmoveEventHandler HTMLElementEvents_Event_onmove;

		// Token: 0x140017DD RID: 6109
		// (add) Token: 0x0600C387 RID: 50055
		// (remove) Token: 0x0600C388 RID: 50056
		public virtual extern event HTMLElementEvents_oncontrolselectEventHandler HTMLElementEvents_Event_oncontrolselect;

		// Token: 0x140017DE RID: 6110
		// (add) Token: 0x0600C389 RID: 50057
		// (remove) Token: 0x0600C38A RID: 50058
		public virtual extern event HTMLElementEvents_onmovestartEventHandler HTMLElementEvents_Event_onmovestart;

		// Token: 0x140017DF RID: 6111
		// (add) Token: 0x0600C38B RID: 50059
		// (remove) Token: 0x0600C38C RID: 50060
		public virtual extern event HTMLElementEvents_onmoveendEventHandler HTMLElementEvents_Event_onmoveend;

		// Token: 0x140017E0 RID: 6112
		// (add) Token: 0x0600C38D RID: 50061
		// (remove) Token: 0x0600C38E RID: 50062
		public virtual extern event HTMLElementEvents_onresizestartEventHandler HTMLElementEvents_Event_onresizestart;

		// Token: 0x140017E1 RID: 6113
		// (add) Token: 0x0600C38F RID: 50063
		// (remove) Token: 0x0600C390 RID: 50064
		public virtual extern event HTMLElementEvents_onresizeendEventHandler HTMLElementEvents_Event_onresizeend;

		// Token: 0x140017E2 RID: 6114
		// (add) Token: 0x0600C391 RID: 50065
		// (remove) Token: 0x0600C392 RID: 50066
		public virtual extern event HTMLElementEvents_onmouseenterEventHandler HTMLElementEvents_Event_onmouseenter;

		// Token: 0x140017E3 RID: 6115
		// (add) Token: 0x0600C393 RID: 50067
		// (remove) Token: 0x0600C394 RID: 50068
		public virtual extern event HTMLElementEvents_onmouseleaveEventHandler HTMLElementEvents_Event_onmouseleave;

		// Token: 0x140017E4 RID: 6116
		// (add) Token: 0x0600C395 RID: 50069
		// (remove) Token: 0x0600C396 RID: 50070
		public virtual extern event HTMLElementEvents_onmousewheelEventHandler HTMLElementEvents_Event_onmousewheel;

		// Token: 0x140017E5 RID: 6117
		// (add) Token: 0x0600C397 RID: 50071
		// (remove) Token: 0x0600C398 RID: 50072
		public virtual extern event HTMLElementEvents_onactivateEventHandler HTMLElementEvents_Event_onactivate;

		// Token: 0x140017E6 RID: 6118
		// (add) Token: 0x0600C399 RID: 50073
		// (remove) Token: 0x0600C39A RID: 50074
		public virtual extern event HTMLElementEvents_ondeactivateEventHandler HTMLElementEvents_Event_ondeactivate;

		// Token: 0x140017E7 RID: 6119
		// (add) Token: 0x0600C39B RID: 50075
		// (remove) Token: 0x0600C39C RID: 50076
		public virtual extern event HTMLElementEvents_onfocusinEventHandler HTMLElementEvents_Event_onfocusin;

		// Token: 0x140017E8 RID: 6120
		// (add) Token: 0x0600C39D RID: 50077
		// (remove) Token: 0x0600C39E RID: 50078
		public virtual extern event HTMLElementEvents_onfocusoutEventHandler HTMLElementEvents_Event_onfocusout;

		// Token: 0x140017E9 RID: 6121
		// (add) Token: 0x0600C39F RID: 50079
		// (remove) Token: 0x0600C3A0 RID: 50080
		public virtual extern event HTMLElementEvents2_onhelpEventHandler HTMLElementEvents2_Event_onhelp;

		// Token: 0x140017EA RID: 6122
		// (add) Token: 0x0600C3A1 RID: 50081
		// (remove) Token: 0x0600C3A2 RID: 50082
		public virtual extern event HTMLElementEvents2_onclickEventHandler HTMLElementEvents2_Event_onclick;

		// Token: 0x140017EB RID: 6123
		// (add) Token: 0x0600C3A3 RID: 50083
		// (remove) Token: 0x0600C3A4 RID: 50084
		public virtual extern event HTMLElementEvents2_ondblclickEventHandler HTMLElementEvents2_Event_ondblclick;

		// Token: 0x140017EC RID: 6124
		// (add) Token: 0x0600C3A5 RID: 50085
		// (remove) Token: 0x0600C3A6 RID: 50086
		public virtual extern event HTMLElementEvents2_onkeypressEventHandler HTMLElementEvents2_Event_onkeypress;

		// Token: 0x140017ED RID: 6125
		// (add) Token: 0x0600C3A7 RID: 50087
		// (remove) Token: 0x0600C3A8 RID: 50088
		public virtual extern event HTMLElementEvents2_onkeydownEventHandler HTMLElementEvents2_Event_onkeydown;

		// Token: 0x140017EE RID: 6126
		// (add) Token: 0x0600C3A9 RID: 50089
		// (remove) Token: 0x0600C3AA RID: 50090
		public virtual extern event HTMLElementEvents2_onkeyupEventHandler HTMLElementEvents2_Event_onkeyup;

		// Token: 0x140017EF RID: 6127
		// (add) Token: 0x0600C3AB RID: 50091
		// (remove) Token: 0x0600C3AC RID: 50092
		public virtual extern event HTMLElementEvents2_onmouseoutEventHandler HTMLElementEvents2_Event_onmouseout;

		// Token: 0x140017F0 RID: 6128
		// (add) Token: 0x0600C3AD RID: 50093
		// (remove) Token: 0x0600C3AE RID: 50094
		public virtual extern event HTMLElementEvents2_onmouseoverEventHandler HTMLElementEvents2_Event_onmouseover;

		// Token: 0x140017F1 RID: 6129
		// (add) Token: 0x0600C3AF RID: 50095
		// (remove) Token: 0x0600C3B0 RID: 50096
		public virtual extern event HTMLElementEvents2_onmousemoveEventHandler HTMLElementEvents2_Event_onmousemove;

		// Token: 0x140017F2 RID: 6130
		// (add) Token: 0x0600C3B1 RID: 50097
		// (remove) Token: 0x0600C3B2 RID: 50098
		public virtual extern event HTMLElementEvents2_onmousedownEventHandler HTMLElementEvents2_Event_onmousedown;

		// Token: 0x140017F3 RID: 6131
		// (add) Token: 0x0600C3B3 RID: 50099
		// (remove) Token: 0x0600C3B4 RID: 50100
		public virtual extern event HTMLElementEvents2_onmouseupEventHandler HTMLElementEvents2_Event_onmouseup;

		// Token: 0x140017F4 RID: 6132
		// (add) Token: 0x0600C3B5 RID: 50101
		// (remove) Token: 0x0600C3B6 RID: 50102
		public virtual extern event HTMLElementEvents2_onselectstartEventHandler HTMLElementEvents2_Event_onselectstart;

		// Token: 0x140017F5 RID: 6133
		// (add) Token: 0x0600C3B7 RID: 50103
		// (remove) Token: 0x0600C3B8 RID: 50104
		public virtual extern event HTMLElementEvents2_onfilterchangeEventHandler HTMLElementEvents2_Event_onfilterchange;

		// Token: 0x140017F6 RID: 6134
		// (add) Token: 0x0600C3B9 RID: 50105
		// (remove) Token: 0x0600C3BA RID: 50106
		public virtual extern event HTMLElementEvents2_ondragstartEventHandler HTMLElementEvents2_Event_ondragstart;

		// Token: 0x140017F7 RID: 6135
		// (add) Token: 0x0600C3BB RID: 50107
		// (remove) Token: 0x0600C3BC RID: 50108
		public virtual extern event HTMLElementEvents2_onbeforeupdateEventHandler HTMLElementEvents2_Event_onbeforeupdate;

		// Token: 0x140017F8 RID: 6136
		// (add) Token: 0x0600C3BD RID: 50109
		// (remove) Token: 0x0600C3BE RID: 50110
		public virtual extern event HTMLElementEvents2_onafterupdateEventHandler HTMLElementEvents2_Event_onafterupdate;

		// Token: 0x140017F9 RID: 6137
		// (add) Token: 0x0600C3BF RID: 50111
		// (remove) Token: 0x0600C3C0 RID: 50112
		public virtual extern event HTMLElementEvents2_onerrorupdateEventHandler HTMLElementEvents2_Event_onerrorupdate;

		// Token: 0x140017FA RID: 6138
		// (add) Token: 0x0600C3C1 RID: 50113
		// (remove) Token: 0x0600C3C2 RID: 50114
		public virtual extern event HTMLElementEvents2_onrowexitEventHandler HTMLElementEvents2_Event_onrowexit;

		// Token: 0x140017FB RID: 6139
		// (add) Token: 0x0600C3C3 RID: 50115
		// (remove) Token: 0x0600C3C4 RID: 50116
		public virtual extern event HTMLElementEvents2_onrowenterEventHandler HTMLElementEvents2_Event_onrowenter;

		// Token: 0x140017FC RID: 6140
		// (add) Token: 0x0600C3C5 RID: 50117
		// (remove) Token: 0x0600C3C6 RID: 50118
		public virtual extern event HTMLElementEvents2_ondatasetchangedEventHandler HTMLElementEvents2_Event_ondatasetchanged;

		// Token: 0x140017FD RID: 6141
		// (add) Token: 0x0600C3C7 RID: 50119
		// (remove) Token: 0x0600C3C8 RID: 50120
		public virtual extern event HTMLElementEvents2_ondataavailableEventHandler HTMLElementEvents2_Event_ondataavailable;

		// Token: 0x140017FE RID: 6142
		// (add) Token: 0x0600C3C9 RID: 50121
		// (remove) Token: 0x0600C3CA RID: 50122
		public virtual extern event HTMLElementEvents2_ondatasetcompleteEventHandler HTMLElementEvents2_Event_ondatasetcomplete;

		// Token: 0x140017FF RID: 6143
		// (add) Token: 0x0600C3CB RID: 50123
		// (remove) Token: 0x0600C3CC RID: 50124
		public virtual extern event HTMLElementEvents2_onlosecaptureEventHandler HTMLElementEvents2_Event_onlosecapture;

		// Token: 0x14001800 RID: 6144
		// (add) Token: 0x0600C3CD RID: 50125
		// (remove) Token: 0x0600C3CE RID: 50126
		public virtual extern event HTMLElementEvents2_onpropertychangeEventHandler HTMLElementEvents2_Event_onpropertychange;

		// Token: 0x14001801 RID: 6145
		// (add) Token: 0x0600C3CF RID: 50127
		// (remove) Token: 0x0600C3D0 RID: 50128
		public virtual extern event HTMLElementEvents2_onscrollEventHandler HTMLElementEvents2_Event_onscroll;

		// Token: 0x14001802 RID: 6146
		// (add) Token: 0x0600C3D1 RID: 50129
		// (remove) Token: 0x0600C3D2 RID: 50130
		public virtual extern event HTMLElementEvents2_onfocusEventHandler HTMLElementEvents2_Event_onfocus;

		// Token: 0x14001803 RID: 6147
		// (add) Token: 0x0600C3D3 RID: 50131
		// (remove) Token: 0x0600C3D4 RID: 50132
		public virtual extern event HTMLElementEvents2_onblurEventHandler HTMLElementEvents2_Event_onblur;

		// Token: 0x14001804 RID: 6148
		// (add) Token: 0x0600C3D5 RID: 50133
		// (remove) Token: 0x0600C3D6 RID: 50134
		public virtual extern event HTMLElementEvents2_onresizeEventHandler HTMLElementEvents2_Event_onresize;

		// Token: 0x14001805 RID: 6149
		// (add) Token: 0x0600C3D7 RID: 50135
		// (remove) Token: 0x0600C3D8 RID: 50136
		public virtual extern event HTMLElementEvents2_ondragEventHandler HTMLElementEvents2_Event_ondrag;

		// Token: 0x14001806 RID: 6150
		// (add) Token: 0x0600C3D9 RID: 50137
		// (remove) Token: 0x0600C3DA RID: 50138
		public virtual extern event HTMLElementEvents2_ondragendEventHandler HTMLElementEvents2_Event_ondragend;

		// Token: 0x14001807 RID: 6151
		// (add) Token: 0x0600C3DB RID: 50139
		// (remove) Token: 0x0600C3DC RID: 50140
		public virtual extern event HTMLElementEvents2_ondragenterEventHandler HTMLElementEvents2_Event_ondragenter;

		// Token: 0x14001808 RID: 6152
		// (add) Token: 0x0600C3DD RID: 50141
		// (remove) Token: 0x0600C3DE RID: 50142
		public virtual extern event HTMLElementEvents2_ondragoverEventHandler HTMLElementEvents2_Event_ondragover;

		// Token: 0x14001809 RID: 6153
		// (add) Token: 0x0600C3DF RID: 50143
		// (remove) Token: 0x0600C3E0 RID: 50144
		public virtual extern event HTMLElementEvents2_ondragleaveEventHandler HTMLElementEvents2_Event_ondragleave;

		// Token: 0x1400180A RID: 6154
		// (add) Token: 0x0600C3E1 RID: 50145
		// (remove) Token: 0x0600C3E2 RID: 50146
		public virtual extern event HTMLElementEvents2_ondropEventHandler HTMLElementEvents2_Event_ondrop;

		// Token: 0x1400180B RID: 6155
		// (add) Token: 0x0600C3E3 RID: 50147
		// (remove) Token: 0x0600C3E4 RID: 50148
		public virtual extern event HTMLElementEvents2_onbeforecutEventHandler HTMLElementEvents2_Event_onbeforecut;

		// Token: 0x1400180C RID: 6156
		// (add) Token: 0x0600C3E5 RID: 50149
		// (remove) Token: 0x0600C3E6 RID: 50150
		public virtual extern event HTMLElementEvents2_oncutEventHandler HTMLElementEvents2_Event_oncut;

		// Token: 0x1400180D RID: 6157
		// (add) Token: 0x0600C3E7 RID: 50151
		// (remove) Token: 0x0600C3E8 RID: 50152
		public virtual extern event HTMLElementEvents2_onbeforecopyEventHandler HTMLElementEvents2_Event_onbeforecopy;

		// Token: 0x1400180E RID: 6158
		// (add) Token: 0x0600C3E9 RID: 50153
		// (remove) Token: 0x0600C3EA RID: 50154
		public virtual extern event HTMLElementEvents2_oncopyEventHandler HTMLElementEvents2_Event_oncopy;

		// Token: 0x1400180F RID: 6159
		// (add) Token: 0x0600C3EB RID: 50155
		// (remove) Token: 0x0600C3EC RID: 50156
		public virtual extern event HTMLElementEvents2_onbeforepasteEventHandler HTMLElementEvents2_Event_onbeforepaste;

		// Token: 0x14001810 RID: 6160
		// (add) Token: 0x0600C3ED RID: 50157
		// (remove) Token: 0x0600C3EE RID: 50158
		public virtual extern event HTMLElementEvents2_onpasteEventHandler HTMLElementEvents2_Event_onpaste;

		// Token: 0x14001811 RID: 6161
		// (add) Token: 0x0600C3EF RID: 50159
		// (remove) Token: 0x0600C3F0 RID: 50160
		public virtual extern event HTMLElementEvents2_oncontextmenuEventHandler HTMLElementEvents2_Event_oncontextmenu;

		// Token: 0x14001812 RID: 6162
		// (add) Token: 0x0600C3F1 RID: 50161
		// (remove) Token: 0x0600C3F2 RID: 50162
		public virtual extern event HTMLElementEvents2_onrowsdeleteEventHandler HTMLElementEvents2_Event_onrowsdelete;

		// Token: 0x14001813 RID: 6163
		// (add) Token: 0x0600C3F3 RID: 50163
		// (remove) Token: 0x0600C3F4 RID: 50164
		public virtual extern event HTMLElementEvents2_onrowsinsertedEventHandler HTMLElementEvents2_Event_onrowsinserted;

		// Token: 0x14001814 RID: 6164
		// (add) Token: 0x0600C3F5 RID: 50165
		// (remove) Token: 0x0600C3F6 RID: 50166
		public virtual extern event HTMLElementEvents2_oncellchangeEventHandler HTMLElementEvents2_Event_oncellchange;

		// Token: 0x14001815 RID: 6165
		// (add) Token: 0x0600C3F7 RID: 50167
		// (remove) Token: 0x0600C3F8 RID: 50168
		public virtual extern event HTMLElementEvents2_onreadystatechangeEventHandler HTMLElementEvents2_Event_onreadystatechange;

		// Token: 0x14001816 RID: 6166
		// (add) Token: 0x0600C3F9 RID: 50169
		// (remove) Token: 0x0600C3FA RID: 50170
		public virtual extern event HTMLElementEvents2_onlayoutcompleteEventHandler HTMLElementEvents2_Event_onlayoutcomplete;

		// Token: 0x14001817 RID: 6167
		// (add) Token: 0x0600C3FB RID: 50171
		// (remove) Token: 0x0600C3FC RID: 50172
		public virtual extern event HTMLElementEvents2_onpageEventHandler HTMLElementEvents2_Event_onpage;

		// Token: 0x14001818 RID: 6168
		// (add) Token: 0x0600C3FD RID: 50173
		// (remove) Token: 0x0600C3FE RID: 50174
		public virtual extern event HTMLElementEvents2_onmouseenterEventHandler HTMLElementEvents2_Event_onmouseenter;

		// Token: 0x14001819 RID: 6169
		// (add) Token: 0x0600C3FF RID: 50175
		// (remove) Token: 0x0600C400 RID: 50176
		public virtual extern event HTMLElementEvents2_onmouseleaveEventHandler HTMLElementEvents2_Event_onmouseleave;

		// Token: 0x1400181A RID: 6170
		// (add) Token: 0x0600C401 RID: 50177
		// (remove) Token: 0x0600C402 RID: 50178
		public virtual extern event HTMLElementEvents2_onactivateEventHandler HTMLElementEvents2_Event_onactivate;

		// Token: 0x1400181B RID: 6171
		// (add) Token: 0x0600C403 RID: 50179
		// (remove) Token: 0x0600C404 RID: 50180
		public virtual extern event HTMLElementEvents2_ondeactivateEventHandler HTMLElementEvents2_Event_ondeactivate;

		// Token: 0x1400181C RID: 6172
		// (add) Token: 0x0600C405 RID: 50181
		// (remove) Token: 0x0600C406 RID: 50182
		public virtual extern event HTMLElementEvents2_onbeforedeactivateEventHandler HTMLElementEvents2_Event_onbeforedeactivate;

		// Token: 0x1400181D RID: 6173
		// (add) Token: 0x0600C407 RID: 50183
		// (remove) Token: 0x0600C408 RID: 50184
		public virtual extern event HTMLElementEvents2_onbeforeactivateEventHandler HTMLElementEvents2_Event_onbeforeactivate;

		// Token: 0x1400181E RID: 6174
		// (add) Token: 0x0600C409 RID: 50185
		// (remove) Token: 0x0600C40A RID: 50186
		public virtual extern event HTMLElementEvents2_onfocusinEventHandler HTMLElementEvents2_Event_onfocusin;

		// Token: 0x1400181F RID: 6175
		// (add) Token: 0x0600C40B RID: 50187
		// (remove) Token: 0x0600C40C RID: 50188
		public virtual extern event HTMLElementEvents2_onfocusoutEventHandler HTMLElementEvents2_Event_onfocusout;

		// Token: 0x14001820 RID: 6176
		// (add) Token: 0x0600C40D RID: 50189
		// (remove) Token: 0x0600C40E RID: 50190
		public virtual extern event HTMLElementEvents2_onmoveEventHandler HTMLElementEvents2_Event_onmove;

		// Token: 0x14001821 RID: 6177
		// (add) Token: 0x0600C40F RID: 50191
		// (remove) Token: 0x0600C410 RID: 50192
		public virtual extern event HTMLElementEvents2_oncontrolselectEventHandler HTMLElementEvents2_Event_oncontrolselect;

		// Token: 0x14001822 RID: 6178
		// (add) Token: 0x0600C411 RID: 50193
		// (remove) Token: 0x0600C412 RID: 50194
		public virtual extern event HTMLElementEvents2_onmovestartEventHandler HTMLElementEvents2_Event_onmovestart;

		// Token: 0x14001823 RID: 6179
		// (add) Token: 0x0600C413 RID: 50195
		// (remove) Token: 0x0600C414 RID: 50196
		public virtual extern event HTMLElementEvents2_onmoveendEventHandler HTMLElementEvents2_Event_onmoveend;

		// Token: 0x14001824 RID: 6180
		// (add) Token: 0x0600C415 RID: 50197
		// (remove) Token: 0x0600C416 RID: 50198
		public virtual extern event HTMLElementEvents2_onresizestartEventHandler HTMLElementEvents2_Event_onresizestart;

		// Token: 0x14001825 RID: 6181
		// (add) Token: 0x0600C417 RID: 50199
		// (remove) Token: 0x0600C418 RID: 50200
		public virtual extern event HTMLElementEvents2_onresizeendEventHandler HTMLElementEvents2_Event_onresizeend;

		// Token: 0x14001826 RID: 6182
		// (add) Token: 0x0600C419 RID: 50201
		// (remove) Token: 0x0600C41A RID: 50202
		public virtual extern event HTMLElementEvents2_onmousewheelEventHandler HTMLElementEvents2_Event_onmousewheel;
	}
}
