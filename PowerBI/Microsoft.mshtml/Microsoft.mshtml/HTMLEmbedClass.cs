using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000849 RID: 2121
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLControlElementEvents\0mshtml.HTMLControlElementEvents2\0\0")]
	[TypeLibType(2)]
	[Guid("3050F25D-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLEmbedClass : DispHTMLEmbed, HTMLEmbed, HTMLControlElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLControlElement, IHTMLEmbedElement, HTMLControlElementEvents2_Event
	{
		// Token: 0x0600E062 RID: 57442
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLEmbedClass();

		// Token: 0x0600E063 RID: 57443
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600E064 RID: 57444
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600E065 RID: 57445
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17004AA5 RID: 19109
		// (get) Token: 0x0600E067 RID: 57447
		// (set) Token: 0x0600E066 RID: 57446
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

		// Token: 0x17004AA6 RID: 19110
		// (get) Token: 0x0600E069 RID: 57449
		// (set) Token: 0x0600E068 RID: 57448
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

		// Token: 0x17004AA7 RID: 19111
		// (get) Token: 0x0600E06A RID: 57450
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004AA8 RID: 19112
		// (get) Token: 0x0600E06B RID: 57451
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004AA9 RID: 19113
		// (get) Token: 0x0600E06C RID: 57452
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004AAA RID: 19114
		// (get) Token: 0x0600E06E RID: 57454
		// (set) Token: 0x0600E06D RID: 57453
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

		// Token: 0x17004AAB RID: 19115
		// (get) Token: 0x0600E070 RID: 57456
		// (set) Token: 0x0600E06F RID: 57455
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

		// Token: 0x17004AAC RID: 19116
		// (get) Token: 0x0600E072 RID: 57458
		// (set) Token: 0x0600E071 RID: 57457
		[DispId(-2147412103)]
		public virtual extern object ondblclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004AAD RID: 19117
		// (get) Token: 0x0600E074 RID: 57460
		// (set) Token: 0x0600E073 RID: 57459
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

		// Token: 0x17004AAE RID: 19118
		// (get) Token: 0x0600E076 RID: 57462
		// (set) Token: 0x0600E075 RID: 57461
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

		// Token: 0x17004AAF RID: 19119
		// (get) Token: 0x0600E078 RID: 57464
		// (set) Token: 0x0600E077 RID: 57463
		[DispId(-2147412105)]
		public virtual extern object onkeypress
		{
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004AB0 RID: 19120
		// (get) Token: 0x0600E07A RID: 57466
		// (set) Token: 0x0600E079 RID: 57465
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

		// Token: 0x17004AB1 RID: 19121
		// (get) Token: 0x0600E07C RID: 57468
		// (set) Token: 0x0600E07B RID: 57467
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

		// Token: 0x17004AB2 RID: 19122
		// (get) Token: 0x0600E07E RID: 57470
		// (set) Token: 0x0600E07D RID: 57469
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

		// Token: 0x17004AB3 RID: 19123
		// (get) Token: 0x0600E080 RID: 57472
		// (set) Token: 0x0600E07F RID: 57471
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

		// Token: 0x17004AB4 RID: 19124
		// (get) Token: 0x0600E082 RID: 57474
		// (set) Token: 0x0600E081 RID: 57473
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

		// Token: 0x17004AB5 RID: 19125
		// (get) Token: 0x0600E083 RID: 57475
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004AB6 RID: 19126
		// (get) Token: 0x0600E085 RID: 57477
		// (set) Token: 0x0600E084 RID: 57476
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

		// Token: 0x17004AB7 RID: 19127
		// (get) Token: 0x0600E087 RID: 57479
		// (set) Token: 0x0600E086 RID: 57478
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

		// Token: 0x17004AB8 RID: 19128
		// (get) Token: 0x0600E089 RID: 57481
		// (set) Token: 0x0600E088 RID: 57480
		[DispId(-2147412075)]
		public virtual extern object onselectstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600E08A RID: 57482
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600E08B RID: 57483
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17004AB9 RID: 19129
		// (get) Token: 0x0600E08C RID: 57484
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004ABA RID: 19130
		// (get) Token: 0x0600E08D RID: 57485
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004ABB RID: 19131
		// (get) Token: 0x0600E08F RID: 57487
		// (set) Token: 0x0600E08E RID: 57486
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

		// Token: 0x17004ABC RID: 19132
		// (get) Token: 0x0600E090 RID: 57488
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004ABD RID: 19133
		// (get) Token: 0x0600E091 RID: 57489
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004ABE RID: 19134
		// (get) Token: 0x0600E092 RID: 57490
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004ABF RID: 19135
		// (get) Token: 0x0600E093 RID: 57491
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004AC0 RID: 19136
		// (get) Token: 0x0600E094 RID: 57492
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004AC1 RID: 19137
		// (get) Token: 0x0600E096 RID: 57494
		// (set) Token: 0x0600E095 RID: 57493
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

		// Token: 0x17004AC2 RID: 19138
		// (get) Token: 0x0600E098 RID: 57496
		// (set) Token: 0x0600E097 RID: 57495
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

		// Token: 0x17004AC3 RID: 19139
		// (get) Token: 0x0600E09A RID: 57498
		// (set) Token: 0x0600E099 RID: 57497
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

		// Token: 0x17004AC4 RID: 19140
		// (get) Token: 0x0600E09C RID: 57500
		// (set) Token: 0x0600E09B RID: 57499
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

		// Token: 0x0600E09D RID: 57501
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600E09E RID: 57502
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17004AC5 RID: 19141
		// (get) Token: 0x0600E09F RID: 57503
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004AC6 RID: 19142
		// (get) Token: 0x0600E0A0 RID: 57504
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E0A1 RID: 57505
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17004AC7 RID: 19143
		// (get) Token: 0x0600E0A2 RID: 57506
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004AC8 RID: 19144
		// (get) Token: 0x0600E0A4 RID: 57508
		// (set) Token: 0x0600E0A3 RID: 57507
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

		// Token: 0x0600E0A5 RID: 57509
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17004AC9 RID: 19145
		// (get) Token: 0x0600E0A7 RID: 57511
		// (set) Token: 0x0600E0A6 RID: 57510
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

		// Token: 0x17004ACA RID: 19146
		// (get) Token: 0x0600E0A9 RID: 57513
		// (set) Token: 0x0600E0A8 RID: 57512
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

		// Token: 0x17004ACB RID: 19147
		// (get) Token: 0x0600E0AB RID: 57515
		// (set) Token: 0x0600E0AA RID: 57514
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

		// Token: 0x17004ACC RID: 19148
		// (get) Token: 0x0600E0AD RID: 57517
		// (set) Token: 0x0600E0AC RID: 57516
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

		// Token: 0x17004ACD RID: 19149
		// (get) Token: 0x0600E0AF RID: 57519
		// (set) Token: 0x0600E0AE RID: 57518
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

		// Token: 0x17004ACE RID: 19150
		// (get) Token: 0x0600E0B1 RID: 57521
		// (set) Token: 0x0600E0B0 RID: 57520
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

		// Token: 0x17004ACF RID: 19151
		// (get) Token: 0x0600E0B3 RID: 57523
		// (set) Token: 0x0600E0B2 RID: 57522
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

		// Token: 0x17004AD0 RID: 19152
		// (get) Token: 0x0600E0B5 RID: 57525
		// (set) Token: 0x0600E0B4 RID: 57524
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

		// Token: 0x17004AD1 RID: 19153
		// (get) Token: 0x0600E0B7 RID: 57527
		// (set) Token: 0x0600E0B6 RID: 57526
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

		// Token: 0x17004AD2 RID: 19154
		// (get) Token: 0x0600E0B8 RID: 57528
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004AD3 RID: 19155
		// (get) Token: 0x0600E0B9 RID: 57529
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004AD4 RID: 19156
		// (get) Token: 0x0600E0BA RID: 57530
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600E0BB RID: 57531
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0600E0BC RID: 57532
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17004AD5 RID: 19157
		// (get) Token: 0x0600E0BE RID: 57534
		// (set) Token: 0x0600E0BD RID: 57533
		[DispId(-2147412066)]
		public virtual extern object onlosecapture
		{
			[TypeLibFunc(20)]
			[DispId(-2147412066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412066)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600E0BF RID: 57535
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600E0C0 RID: 57536
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17004AD6 RID: 19158
		// (get) Token: 0x0600E0C2 RID: 57538
		// (set) Token: 0x0600E0C1 RID: 57537
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

		// Token: 0x17004AD7 RID: 19159
		// (get) Token: 0x0600E0C4 RID: 57540
		// (set) Token: 0x0600E0C3 RID: 57539
		[DispId(-2147412063)]
		public virtual extern object ondrag
		{
			[DispId(-2147412063)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004AD8 RID: 19160
		// (get) Token: 0x0600E0C6 RID: 57542
		// (set) Token: 0x0600E0C5 RID: 57541
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

		// Token: 0x17004AD9 RID: 19161
		// (get) Token: 0x0600E0C8 RID: 57544
		// (set) Token: 0x0600E0C7 RID: 57543
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

		// Token: 0x17004ADA RID: 19162
		// (get) Token: 0x0600E0CA RID: 57546
		// (set) Token: 0x0600E0C9 RID: 57545
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

		// Token: 0x17004ADB RID: 19163
		// (get) Token: 0x0600E0CC RID: 57548
		// (set) Token: 0x0600E0CB RID: 57547
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

		// Token: 0x17004ADC RID: 19164
		// (get) Token: 0x0600E0CE RID: 57550
		// (set) Token: 0x0600E0CD RID: 57549
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

		// Token: 0x17004ADD RID: 19165
		// (get) Token: 0x0600E0D0 RID: 57552
		// (set) Token: 0x0600E0CF RID: 57551
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

		// Token: 0x17004ADE RID: 19166
		// (get) Token: 0x0600E0D2 RID: 57554
		// (set) Token: 0x0600E0D1 RID: 57553
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

		// Token: 0x17004ADF RID: 19167
		// (get) Token: 0x0600E0D4 RID: 57556
		// (set) Token: 0x0600E0D3 RID: 57555
		[DispId(-2147412053)]
		public virtual extern object onbeforecopy
		{
			[DispId(-2147412053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004AE0 RID: 19168
		// (get) Token: 0x0600E0D6 RID: 57558
		// (set) Token: 0x0600E0D5 RID: 57557
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

		// Token: 0x17004AE1 RID: 19169
		// (get) Token: 0x0600E0D8 RID: 57560
		// (set) Token: 0x0600E0D7 RID: 57559
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

		// Token: 0x17004AE2 RID: 19170
		// (get) Token: 0x0600E0DA RID: 57562
		// (set) Token: 0x0600E0D9 RID: 57561
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

		// Token: 0x17004AE3 RID: 19171
		// (get) Token: 0x0600E0DB RID: 57563
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004AE4 RID: 19172
		// (get) Token: 0x0600E0DD RID: 57565
		// (set) Token: 0x0600E0DC RID: 57564
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

		// Token: 0x0600E0DE RID: 57566
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x0600E0DF RID: 57567
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x0600E0E0 RID: 57568
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600E0E1 RID: 57569
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600E0E2 RID: 57570
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17004AE5 RID: 19173
		// (get) Token: 0x0600E0E4 RID: 57572
		// (set) Token: 0x0600E0E3 RID: 57571
		[DispId(-2147418097)]
		public virtual extern short tabIndex
		{
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600E0E5 RID: 57573
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17004AE6 RID: 19174
		// (get) Token: 0x0600E0E7 RID: 57575
		// (set) Token: 0x0600E0E6 RID: 57574
		[DispId(-2147416107)]
		public virtual extern string accessKey
		{
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147416107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004AE7 RID: 19175
		// (get) Token: 0x0600E0E9 RID: 57577
		// (set) Token: 0x0600E0E8 RID: 57576
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

		// Token: 0x17004AE8 RID: 19176
		// (get) Token: 0x0600E0EB RID: 57579
		// (set) Token: 0x0600E0EA RID: 57578
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

		// Token: 0x17004AE9 RID: 19177
		// (get) Token: 0x0600E0ED RID: 57581
		// (set) Token: 0x0600E0EC RID: 57580
		[DispId(-2147412076)]
		public virtual extern object onresize
		{
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600E0EE RID: 57582
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600E0EF RID: 57583
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600E0F0 RID: 57584
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17004AEA RID: 19178
		// (get) Token: 0x0600E0F1 RID: 57585
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004AEB RID: 19179
		// (get) Token: 0x0600E0F2 RID: 57586
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004AEC RID: 19180
		// (get) Token: 0x0600E0F3 RID: 57587
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004AED RID: 19181
		// (get) Token: 0x0600E0F4 RID: 57588
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E0F5 RID: 57589
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600E0F6 RID: 57590
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17004AEE RID: 19182
		// (get) Token: 0x0600E0F7 RID: 57591
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004AEF RID: 19183
		// (get) Token: 0x0600E0F9 RID: 57593
		// (set) Token: 0x0600E0F8 RID: 57592
		[DispId(-2147412087)]
		public virtual extern object onreadystatechange
		{
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004AF0 RID: 19184
		// (get) Token: 0x0600E0FB RID: 57595
		// (set) Token: 0x0600E0FA RID: 57594
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

		// Token: 0x17004AF1 RID: 19185
		// (get) Token: 0x0600E0FD RID: 57597
		// (set) Token: 0x0600E0FC RID: 57596
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

		// Token: 0x17004AF2 RID: 19186
		// (get) Token: 0x0600E0FF RID: 57599
		// (set) Token: 0x0600E0FE RID: 57598
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

		// Token: 0x17004AF3 RID: 19187
		// (get) Token: 0x0600E101 RID: 57601
		// (set) Token: 0x0600E100 RID: 57600
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

		// Token: 0x0600E102 RID: 57602
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17004AF4 RID: 19188
		// (get) Token: 0x0600E103 RID: 57603
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004AF5 RID: 19189
		// (get) Token: 0x0600E104 RID: 57604
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004AF6 RID: 19190
		// (get) Token: 0x0600E106 RID: 57606
		// (set) Token: 0x0600E105 RID: 57605
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

		// Token: 0x17004AF7 RID: 19191
		// (get) Token: 0x0600E108 RID: 57608
		// (set) Token: 0x0600E107 RID: 57607
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

		// Token: 0x0600E109 RID: 57609
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17004AF8 RID: 19192
		// (get) Token: 0x0600E10B RID: 57611
		// (set) Token: 0x0600E10A RID: 57610
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

		// Token: 0x0600E10C RID: 57612
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600E10D RID: 57613
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600E10E RID: 57614
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600E10F RID: 57615
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17004AF9 RID: 19193
		// (get) Token: 0x0600E110 RID: 57616
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E111 RID: 57617
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600E112 RID: 57618
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17004AFA RID: 19194
		// (get) Token: 0x0600E113 RID: 57619
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004AFB RID: 19195
		// (get) Token: 0x0600E114 RID: 57620
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004AFC RID: 19196
		// (get) Token: 0x0600E116 RID: 57622
		// (set) Token: 0x0600E115 RID: 57621
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

		// Token: 0x17004AFD RID: 19197
		// (get) Token: 0x0600E118 RID: 57624
		// (set) Token: 0x0600E117 RID: 57623
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

		// Token: 0x17004AFE RID: 19198
		// (get) Token: 0x0600E119 RID: 57625
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E11A RID: 57626
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600E11B RID: 57627
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17004AFF RID: 19199
		// (get) Token: 0x0600E11C RID: 57628
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B00 RID: 19200
		// (get) Token: 0x0600E11D RID: 57629
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B01 RID: 19201
		// (get) Token: 0x0600E11F RID: 57631
		// (set) Token: 0x0600E11E RID: 57630
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

		// Token: 0x17004B02 RID: 19202
		// (get) Token: 0x0600E121 RID: 57633
		// (set) Token: 0x0600E120 RID: 57632
		[DispId(-2147412038)]
		public virtual extern object onpage
		{
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004B03 RID: 19203
		// (get) Token: 0x0600E123 RID: 57635
		// (set) Token: 0x0600E122 RID: 57634
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

		// Token: 0x17004B04 RID: 19204
		// (get) Token: 0x0600E125 RID: 57637
		// (set) Token: 0x0600E124 RID: 57636
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

		// Token: 0x0600E126 RID: 57638
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17004B05 RID: 19205
		// (get) Token: 0x0600E128 RID: 57640
		// (set) Token: 0x0600E127 RID: 57639
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

		// Token: 0x17004B06 RID: 19206
		// (get) Token: 0x0600E129 RID: 57641
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B07 RID: 19207
		// (get) Token: 0x0600E12B RID: 57643
		// (set) Token: 0x0600E12A RID: 57642
		[DispId(-2147412949)]
		public virtual extern bool hideFocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004B08 RID: 19208
		// (get) Token: 0x0600E12D RID: 57645
		// (set) Token: 0x0600E12C RID: 57644
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

		// Token: 0x17004B09 RID: 19209
		// (get) Token: 0x0600E12E RID: 57646
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B0A RID: 19210
		// (get) Token: 0x0600E130 RID: 57648
		// (set) Token: 0x0600E12F RID: 57647
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

		// Token: 0x17004B0B RID: 19211
		// (get) Token: 0x0600E132 RID: 57650
		// (set) Token: 0x0600E131 RID: 57649
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

		// Token: 0x0600E133 RID: 57651
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17004B0C RID: 19212
		// (get) Token: 0x0600E135 RID: 57653
		// (set) Token: 0x0600E134 RID: 57652
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

		// Token: 0x17004B0D RID: 19213
		// (get) Token: 0x0600E137 RID: 57655
		// (set) Token: 0x0600E136 RID: 57654
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

		// Token: 0x17004B0E RID: 19214
		// (get) Token: 0x0600E139 RID: 57657
		// (set) Token: 0x0600E138 RID: 57656
		[DispId(-2147412031)]
		public virtual extern object onmovestart
		{
			[DispId(-2147412031)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004B0F RID: 19215
		// (get) Token: 0x0600E13B RID: 57659
		// (set) Token: 0x0600E13A RID: 57658
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

		// Token: 0x17004B10 RID: 19216
		// (get) Token: 0x0600E13D RID: 57661
		// (set) Token: 0x0600E13C RID: 57660
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

		// Token: 0x17004B11 RID: 19217
		// (get) Token: 0x0600E13F RID: 57663
		// (set) Token: 0x0600E13E RID: 57662
		[DispId(-2147412026)]
		public virtual extern object onmouseleave
		{
			[DispId(-2147412026)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004B12 RID: 19218
		// (get) Token: 0x0600E141 RID: 57665
		// (set) Token: 0x0600E140 RID: 57664
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

		// Token: 0x17004B13 RID: 19219
		// (get) Token: 0x0600E143 RID: 57667
		// (set) Token: 0x0600E142 RID: 57666
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

		// Token: 0x0600E144 RID: 57668
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17004B14 RID: 19220
		// (get) Token: 0x0600E145 RID: 57669
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B15 RID: 19221
		// (get) Token: 0x0600E147 RID: 57671
		// (set) Token: 0x0600E146 RID: 57670
		[DispId(-2147412036)]
		public virtual extern object onmousewheel
		{
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600E148 RID: 57672
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600E149 RID: 57673
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600E14A RID: 57674
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600E14B RID: 57675
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17004B16 RID: 19222
		// (get) Token: 0x0600E14D RID: 57677
		// (set) Token: 0x0600E14C RID: 57676
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

		// Token: 0x17004B17 RID: 19223
		// (get) Token: 0x0600E14F RID: 57679
		// (set) Token: 0x0600E14E RID: 57678
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

		// Token: 0x17004B18 RID: 19224
		// (get) Token: 0x0600E151 RID: 57681
		// (set) Token: 0x0600E150 RID: 57680
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

		// Token: 0x17004B19 RID: 19225
		// (get) Token: 0x0600E152 RID: 57682
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B1A RID: 19226
		// (get) Token: 0x0600E153 RID: 57683
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004B1B RID: 19227
		// (get) Token: 0x0600E154 RID: 57684
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B1C RID: 19228
		// (get) Token: 0x0600E155 RID: 57685
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600E156 RID: 57686
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17004B1D RID: 19229
		// (get) Token: 0x0600E157 RID: 57687
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004B1E RID: 19230
		// (get) Token: 0x0600E158 RID: 57688
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600E159 RID: 57689
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600E15A RID: 57690
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600E15B RID: 57691
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600E15C RID: 57692
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600E15D RID: 57693
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600E15E RID: 57694
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600E15F RID: 57695
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600E160 RID: 57696
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004B1F RID: 19231
		// (get) Token: 0x0600E161 RID: 57697
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004B20 RID: 19232
		// (get) Token: 0x0600E163 RID: 57699
		// (set) Token: 0x0600E162 RID: 57698
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

		// Token: 0x17004B21 RID: 19233
		// (get) Token: 0x0600E164 RID: 57700
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B22 RID: 19234
		// (get) Token: 0x0600E165 RID: 57701
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B23 RID: 19235
		// (get) Token: 0x0600E166 RID: 57702
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B24 RID: 19236
		// (get) Token: 0x0600E167 RID: 57703
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B25 RID: 19237
		// (get) Token: 0x0600E168 RID: 57704
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004B26 RID: 19238
		// (get) Token: 0x0600E16A RID: 57706
		// (set) Token: 0x0600E169 RID: 57705
		[DispId(-2147415102)]
		public virtual extern string hidden
		{
			[DispId(-2147415102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004B27 RID: 19239
		// (get) Token: 0x0600E16B RID: 57707
		[DispId(-2147415108)]
		public virtual extern string palette
		{
			[DispId(-2147415108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004B28 RID: 19240
		// (get) Token: 0x0600E16C RID: 57708
		[DispId(-2147415107)]
		public virtual extern string pluginspage
		{
			[DispId(-2147415107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004B29 RID: 19241
		// (get) Token: 0x0600E16E RID: 57710
		// (set) Token: 0x0600E16D RID: 57709
		[DispId(-2147415106)]
		public virtual extern string src
		{
			[DispId(-2147415106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004B2A RID: 19242
		// (get) Token: 0x0600E170 RID: 57712
		// (set) Token: 0x0600E16F RID: 57711
		[DispId(-2147415104)]
		public virtual extern string units
		{
			[DispId(-2147415104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004B2B RID: 19243
		// (get) Token: 0x0600E172 RID: 57714
		// (set) Token: 0x0600E171 RID: 57713
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004B2C RID: 19244
		// (get) Token: 0x0600E174 RID: 57716
		// (set) Token: 0x0600E173 RID: 57715
		[DispId(-2147418107)]
		public virtual extern object width
		{
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004B2D RID: 19245
		// (get) Token: 0x0600E176 RID: 57718
		// (set) Token: 0x0600E175 RID: 57717
		[DispId(-2147418106)]
		public virtual extern object height
		{
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600E177 RID: 57719
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600E178 RID: 57720
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600E179 RID: 57721
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17004B2E RID: 19246
		// (get) Token: 0x0600E17B RID: 57723
		// (set) Token: 0x0600E17A RID: 57722
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

		// Token: 0x17004B2F RID: 19247
		// (get) Token: 0x0600E17D RID: 57725
		// (set) Token: 0x0600E17C RID: 57724
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

		// Token: 0x17004B30 RID: 19248
		// (get) Token: 0x0600E17E RID: 57726
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004B31 RID: 19249
		// (get) Token: 0x0600E17F RID: 57727
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B32 RID: 19250
		// (get) Token: 0x0600E180 RID: 57728
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B33 RID: 19251
		// (get) Token: 0x0600E182 RID: 57730
		// (set) Token: 0x0600E181 RID: 57729
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

		// Token: 0x17004B34 RID: 19252
		// (get) Token: 0x0600E184 RID: 57732
		// (set) Token: 0x0600E183 RID: 57731
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

		// Token: 0x17004B35 RID: 19253
		// (get) Token: 0x0600E186 RID: 57734
		// (set) Token: 0x0600E185 RID: 57733
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

		// Token: 0x17004B36 RID: 19254
		// (get) Token: 0x0600E188 RID: 57736
		// (set) Token: 0x0600E187 RID: 57735
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

		// Token: 0x17004B37 RID: 19255
		// (get) Token: 0x0600E18A RID: 57738
		// (set) Token: 0x0600E189 RID: 57737
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

		// Token: 0x17004B38 RID: 19256
		// (get) Token: 0x0600E18C RID: 57740
		// (set) Token: 0x0600E18B RID: 57739
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

		// Token: 0x17004B39 RID: 19257
		// (get) Token: 0x0600E18E RID: 57742
		// (set) Token: 0x0600E18D RID: 57741
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

		// Token: 0x17004B3A RID: 19258
		// (get) Token: 0x0600E190 RID: 57744
		// (set) Token: 0x0600E18F RID: 57743
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

		// Token: 0x17004B3B RID: 19259
		// (get) Token: 0x0600E192 RID: 57746
		// (set) Token: 0x0600E191 RID: 57745
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

		// Token: 0x17004B3C RID: 19260
		// (get) Token: 0x0600E194 RID: 57748
		// (set) Token: 0x0600E193 RID: 57747
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

		// Token: 0x17004B3D RID: 19261
		// (get) Token: 0x0600E196 RID: 57750
		// (set) Token: 0x0600E195 RID: 57749
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

		// Token: 0x17004B3E RID: 19262
		// (get) Token: 0x0600E197 RID: 57751
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004B3F RID: 19263
		// (get) Token: 0x0600E199 RID: 57753
		// (set) Token: 0x0600E198 RID: 57752
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

		// Token: 0x17004B40 RID: 19264
		// (get) Token: 0x0600E19B RID: 57755
		// (set) Token: 0x0600E19A RID: 57754
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

		// Token: 0x17004B41 RID: 19265
		// (get) Token: 0x0600E19D RID: 57757
		// (set) Token: 0x0600E19C RID: 57756
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

		// Token: 0x0600E19E RID: 57758
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600E19F RID: 57759
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17004B42 RID: 19266
		// (get) Token: 0x0600E1A0 RID: 57760
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B43 RID: 19267
		// (get) Token: 0x0600E1A1 RID: 57761
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004B44 RID: 19268
		// (get) Token: 0x0600E1A3 RID: 57763
		// (set) Token: 0x0600E1A2 RID: 57762
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

		// Token: 0x17004B45 RID: 19269
		// (get) Token: 0x0600E1A4 RID: 57764
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B46 RID: 19270
		// (get) Token: 0x0600E1A5 RID: 57765
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B47 RID: 19271
		// (get) Token: 0x0600E1A6 RID: 57766
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B48 RID: 19272
		// (get) Token: 0x0600E1A7 RID: 57767
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B49 RID: 19273
		// (get) Token: 0x0600E1A8 RID: 57768
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B4A RID: 19274
		// (get) Token: 0x0600E1AA RID: 57770
		// (set) Token: 0x0600E1A9 RID: 57769
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

		// Token: 0x17004B4B RID: 19275
		// (get) Token: 0x0600E1AC RID: 57772
		// (set) Token: 0x0600E1AB RID: 57771
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

		// Token: 0x17004B4C RID: 19276
		// (get) Token: 0x0600E1AE RID: 57774
		// (set) Token: 0x0600E1AD RID: 57773
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

		// Token: 0x17004B4D RID: 19277
		// (get) Token: 0x0600E1B0 RID: 57776
		// (set) Token: 0x0600E1AF RID: 57775
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

		// Token: 0x0600E1B1 RID: 57777
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600E1B2 RID: 57778
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17004B4E RID: 19278
		// (get) Token: 0x0600E1B3 RID: 57779
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B4F RID: 19279
		// (get) Token: 0x0600E1B4 RID: 57780
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E1B5 RID: 57781
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17004B50 RID: 19280
		// (get) Token: 0x0600E1B6 RID: 57782
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B51 RID: 19281
		// (get) Token: 0x0600E1B8 RID: 57784
		// (set) Token: 0x0600E1B7 RID: 57783
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

		// Token: 0x0600E1B9 RID: 57785
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17004B52 RID: 19282
		// (get) Token: 0x0600E1BB RID: 57787
		// (set) Token: 0x0600E1BA RID: 57786
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

		// Token: 0x17004B53 RID: 19283
		// (get) Token: 0x0600E1BD RID: 57789
		// (set) Token: 0x0600E1BC RID: 57788
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

		// Token: 0x17004B54 RID: 19284
		// (get) Token: 0x0600E1BF RID: 57791
		// (set) Token: 0x0600E1BE RID: 57790
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

		// Token: 0x17004B55 RID: 19285
		// (get) Token: 0x0600E1C1 RID: 57793
		// (set) Token: 0x0600E1C0 RID: 57792
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

		// Token: 0x17004B56 RID: 19286
		// (get) Token: 0x0600E1C3 RID: 57795
		// (set) Token: 0x0600E1C2 RID: 57794
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

		// Token: 0x17004B57 RID: 19287
		// (get) Token: 0x0600E1C5 RID: 57797
		// (set) Token: 0x0600E1C4 RID: 57796
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

		// Token: 0x17004B58 RID: 19288
		// (get) Token: 0x0600E1C7 RID: 57799
		// (set) Token: 0x0600E1C6 RID: 57798
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

		// Token: 0x17004B59 RID: 19289
		// (get) Token: 0x0600E1C9 RID: 57801
		// (set) Token: 0x0600E1C8 RID: 57800
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

		// Token: 0x17004B5A RID: 19290
		// (get) Token: 0x0600E1CB RID: 57803
		// (set) Token: 0x0600E1CA RID: 57802
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

		// Token: 0x17004B5B RID: 19291
		// (get) Token: 0x0600E1CC RID: 57804
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004B5C RID: 19292
		// (get) Token: 0x0600E1CD RID: 57805
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004B5D RID: 19293
		// (get) Token: 0x0600E1CE RID: 57806
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600E1CF RID: 57807
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600E1D0 RID: 57808
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17004B5E RID: 19294
		// (get) Token: 0x0600E1D2 RID: 57810
		// (set) Token: 0x0600E1D1 RID: 57809
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

		// Token: 0x0600E1D3 RID: 57811
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600E1D4 RID: 57812
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17004B5F RID: 19295
		// (get) Token: 0x0600E1D6 RID: 57814
		// (set) Token: 0x0600E1D5 RID: 57813
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

		// Token: 0x17004B60 RID: 19296
		// (get) Token: 0x0600E1D8 RID: 57816
		// (set) Token: 0x0600E1D7 RID: 57815
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

		// Token: 0x17004B61 RID: 19297
		// (get) Token: 0x0600E1DA RID: 57818
		// (set) Token: 0x0600E1D9 RID: 57817
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

		// Token: 0x17004B62 RID: 19298
		// (get) Token: 0x0600E1DC RID: 57820
		// (set) Token: 0x0600E1DB RID: 57819
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

		// Token: 0x17004B63 RID: 19299
		// (get) Token: 0x0600E1DE RID: 57822
		// (set) Token: 0x0600E1DD RID: 57821
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

		// Token: 0x17004B64 RID: 19300
		// (get) Token: 0x0600E1E0 RID: 57824
		// (set) Token: 0x0600E1DF RID: 57823
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

		// Token: 0x17004B65 RID: 19301
		// (get) Token: 0x0600E1E2 RID: 57826
		// (set) Token: 0x0600E1E1 RID: 57825
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

		// Token: 0x17004B66 RID: 19302
		// (get) Token: 0x0600E1E4 RID: 57828
		// (set) Token: 0x0600E1E3 RID: 57827
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

		// Token: 0x17004B67 RID: 19303
		// (get) Token: 0x0600E1E6 RID: 57830
		// (set) Token: 0x0600E1E5 RID: 57829
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

		// Token: 0x17004B68 RID: 19304
		// (get) Token: 0x0600E1E8 RID: 57832
		// (set) Token: 0x0600E1E7 RID: 57831
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

		// Token: 0x17004B69 RID: 19305
		// (get) Token: 0x0600E1EA RID: 57834
		// (set) Token: 0x0600E1E9 RID: 57833
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

		// Token: 0x17004B6A RID: 19306
		// (get) Token: 0x0600E1EC RID: 57836
		// (set) Token: 0x0600E1EB RID: 57835
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

		// Token: 0x17004B6B RID: 19307
		// (get) Token: 0x0600E1EE RID: 57838
		// (set) Token: 0x0600E1ED RID: 57837
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

		// Token: 0x17004B6C RID: 19308
		// (get) Token: 0x0600E1EF RID: 57839
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B6D RID: 19309
		// (get) Token: 0x0600E1F1 RID: 57841
		// (set) Token: 0x0600E1F0 RID: 57840
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

		// Token: 0x0600E1F2 RID: 57842
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600E1F3 RID: 57843
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0600E1F4 RID: 57844
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600E1F5 RID: 57845
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600E1F6 RID: 57846
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17004B6E RID: 19310
		// (get) Token: 0x0600E1F8 RID: 57848
		// (set) Token: 0x0600E1F7 RID: 57847
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

		// Token: 0x0600E1F9 RID: 57849
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17004B6F RID: 19311
		// (get) Token: 0x0600E1FB RID: 57851
		// (set) Token: 0x0600E1FA RID: 57850
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

		// Token: 0x17004B70 RID: 19312
		// (get) Token: 0x0600E1FD RID: 57853
		// (set) Token: 0x0600E1FC RID: 57852
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

		// Token: 0x17004B71 RID: 19313
		// (get) Token: 0x0600E1FF RID: 57855
		// (set) Token: 0x0600E1FE RID: 57854
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

		// Token: 0x17004B72 RID: 19314
		// (get) Token: 0x0600E201 RID: 57857
		// (set) Token: 0x0600E200 RID: 57856
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

		// Token: 0x0600E202 RID: 57858
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600E203 RID: 57859
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600E204 RID: 57860
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17004B73 RID: 19315
		// (get) Token: 0x0600E205 RID: 57861
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B74 RID: 19316
		// (get) Token: 0x0600E206 RID: 57862
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B75 RID: 19317
		// (get) Token: 0x0600E207 RID: 57863
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B76 RID: 19318
		// (get) Token: 0x0600E208 RID: 57864
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E209 RID: 57865
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600E20A RID: 57866
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17004B77 RID: 19319
		// (get) Token: 0x0600E20B RID: 57867
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004B78 RID: 19320
		// (get) Token: 0x0600E20D RID: 57869
		// (set) Token: 0x0600E20C RID: 57868
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

		// Token: 0x17004B79 RID: 19321
		// (get) Token: 0x0600E20F RID: 57871
		// (set) Token: 0x0600E20E RID: 57870
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

		// Token: 0x17004B7A RID: 19322
		// (get) Token: 0x0600E211 RID: 57873
		// (set) Token: 0x0600E210 RID: 57872
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

		// Token: 0x17004B7B RID: 19323
		// (get) Token: 0x0600E213 RID: 57875
		// (set) Token: 0x0600E212 RID: 57874
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

		// Token: 0x17004B7C RID: 19324
		// (get) Token: 0x0600E215 RID: 57877
		// (set) Token: 0x0600E214 RID: 57876
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

		// Token: 0x0600E216 RID: 57878
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17004B7D RID: 19325
		// (get) Token: 0x0600E217 RID: 57879
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B7E RID: 19326
		// (get) Token: 0x0600E218 RID: 57880
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B7F RID: 19327
		// (get) Token: 0x0600E21A RID: 57882
		// (set) Token: 0x0600E219 RID: 57881
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

		// Token: 0x17004B80 RID: 19328
		// (get) Token: 0x0600E21C RID: 57884
		// (set) Token: 0x0600E21B RID: 57883
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

		// Token: 0x0600E21D RID: 57885
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600E21E RID: 57886
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17004B81 RID: 19329
		// (get) Token: 0x0600E220 RID: 57888
		// (set) Token: 0x0600E21F RID: 57887
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

		// Token: 0x0600E221 RID: 57889
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600E222 RID: 57890
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600E223 RID: 57891
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600E224 RID: 57892
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17004B82 RID: 19330
		// (get) Token: 0x0600E225 RID: 57893
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E226 RID: 57894
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600E227 RID: 57895
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17004B83 RID: 19331
		// (get) Token: 0x0600E228 RID: 57896
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004B84 RID: 19332
		// (get) Token: 0x0600E229 RID: 57897
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004B85 RID: 19333
		// (get) Token: 0x0600E22B RID: 57899
		// (set) Token: 0x0600E22A RID: 57898
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

		// Token: 0x17004B86 RID: 19334
		// (get) Token: 0x0600E22D RID: 57901
		// (set) Token: 0x0600E22C RID: 57900
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

		// Token: 0x17004B87 RID: 19335
		// (get) Token: 0x0600E22E RID: 57902
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E22F RID: 57903
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600E230 RID: 57904
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17004B88 RID: 19336
		// (get) Token: 0x0600E231 RID: 57905
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B89 RID: 19337
		// (get) Token: 0x0600E232 RID: 57906
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B8A RID: 19338
		// (get) Token: 0x0600E234 RID: 57908
		// (set) Token: 0x0600E233 RID: 57907
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

		// Token: 0x17004B8B RID: 19339
		// (get) Token: 0x0600E236 RID: 57910
		// (set) Token: 0x0600E235 RID: 57909
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

		// Token: 0x17004B8C RID: 19340
		// (get) Token: 0x0600E238 RID: 57912
		// (set) Token: 0x0600E237 RID: 57911
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

		// Token: 0x17004B8D RID: 19341
		// (get) Token: 0x0600E23A RID: 57914
		// (set) Token: 0x0600E239 RID: 57913
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

		// Token: 0x0600E23B RID: 57915
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17004B8E RID: 19342
		// (get) Token: 0x0600E23D RID: 57917
		// (set) Token: 0x0600E23C RID: 57916
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

		// Token: 0x17004B8F RID: 19343
		// (get) Token: 0x0600E23E RID: 57918
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B90 RID: 19344
		// (get) Token: 0x0600E240 RID: 57920
		// (set) Token: 0x0600E23F RID: 57919
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

		// Token: 0x17004B91 RID: 19345
		// (get) Token: 0x0600E242 RID: 57922
		// (set) Token: 0x0600E241 RID: 57921
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

		// Token: 0x17004B92 RID: 19346
		// (get) Token: 0x0600E243 RID: 57923
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B93 RID: 19347
		// (get) Token: 0x0600E245 RID: 57925
		// (set) Token: 0x0600E244 RID: 57924
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

		// Token: 0x17004B94 RID: 19348
		// (get) Token: 0x0600E247 RID: 57927
		// (set) Token: 0x0600E246 RID: 57926
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

		// Token: 0x0600E248 RID: 57928
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17004B95 RID: 19349
		// (get) Token: 0x0600E24A RID: 57930
		// (set) Token: 0x0600E249 RID: 57929
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

		// Token: 0x17004B96 RID: 19350
		// (get) Token: 0x0600E24C RID: 57932
		// (set) Token: 0x0600E24B RID: 57931
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

		// Token: 0x17004B97 RID: 19351
		// (get) Token: 0x0600E24E RID: 57934
		// (set) Token: 0x0600E24D RID: 57933
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

		// Token: 0x17004B98 RID: 19352
		// (get) Token: 0x0600E250 RID: 57936
		// (set) Token: 0x0600E24F RID: 57935
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

		// Token: 0x17004B99 RID: 19353
		// (get) Token: 0x0600E252 RID: 57938
		// (set) Token: 0x0600E251 RID: 57937
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

		// Token: 0x17004B9A RID: 19354
		// (get) Token: 0x0600E254 RID: 57940
		// (set) Token: 0x0600E253 RID: 57939
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

		// Token: 0x17004B9B RID: 19355
		// (get) Token: 0x0600E256 RID: 57942
		// (set) Token: 0x0600E255 RID: 57941
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

		// Token: 0x17004B9C RID: 19356
		// (get) Token: 0x0600E258 RID: 57944
		// (set) Token: 0x0600E257 RID: 57943
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

		// Token: 0x0600E259 RID: 57945
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17004B9D RID: 19357
		// (get) Token: 0x0600E25A RID: 57946
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004B9E RID: 19358
		// (get) Token: 0x0600E25C RID: 57948
		// (set) Token: 0x0600E25B RID: 57947
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

		// Token: 0x0600E25D RID: 57949
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x0600E25E RID: 57950
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600E25F RID: 57951
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600E260 RID: 57952
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17004B9F RID: 19359
		// (get) Token: 0x0600E262 RID: 57954
		// (set) Token: 0x0600E261 RID: 57953
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

		// Token: 0x17004BA0 RID: 19360
		// (get) Token: 0x0600E264 RID: 57956
		// (set) Token: 0x0600E263 RID: 57955
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

		// Token: 0x17004BA1 RID: 19361
		// (get) Token: 0x0600E266 RID: 57958
		// (set) Token: 0x0600E265 RID: 57957
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

		// Token: 0x17004BA2 RID: 19362
		// (get) Token: 0x0600E267 RID: 57959
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004BA3 RID: 19363
		// (get) Token: 0x0600E268 RID: 57960
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004BA4 RID: 19364
		// (get) Token: 0x0600E269 RID: 57961
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004BA5 RID: 19365
		// (get) Token: 0x0600E26A RID: 57962
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600E26B RID: 57963
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17004BA6 RID: 19366
		// (get) Token: 0x0600E26C RID: 57964
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004BA7 RID: 19367
		// (get) Token: 0x0600E26D RID: 57965
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600E26E RID: 57966
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600E26F RID: 57967
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600E270 RID: 57968
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600E271 RID: 57969
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600E272 RID: 57970
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600E273 RID: 57971
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600E274 RID: 57972
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600E275 RID: 57973
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004BA8 RID: 19368
		// (get) Token: 0x0600E276 RID: 57974
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004BA9 RID: 19369
		// (get) Token: 0x0600E278 RID: 57976
		// (set) Token: 0x0600E277 RID: 57975
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

		// Token: 0x17004BAA RID: 19370
		// (get) Token: 0x0600E279 RID: 57977
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004BAB RID: 19371
		// (get) Token: 0x0600E27A RID: 57978
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004BAC RID: 19372
		// (get) Token: 0x0600E27B RID: 57979
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004BAD RID: 19373
		// (get) Token: 0x0600E27C RID: 57980
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004BAE RID: 19374
		// (get) Token: 0x0600E27D RID: 57981
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004BAF RID: 19375
		// (get) Token: 0x0600E27F RID: 57983
		// (set) Token: 0x0600E27E RID: 57982
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

		// Token: 0x0600E280 RID: 57984
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17004BB0 RID: 19376
		// (get) Token: 0x0600E282 RID: 57986
		// (set) Token: 0x0600E281 RID: 57985
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

		// Token: 0x17004BB1 RID: 19377
		// (get) Token: 0x0600E284 RID: 57988
		// (set) Token: 0x0600E283 RID: 57987
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

		// Token: 0x17004BB2 RID: 19378
		// (get) Token: 0x0600E286 RID: 57990
		// (set) Token: 0x0600E285 RID: 57989
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

		// Token: 0x17004BB3 RID: 19379
		// (get) Token: 0x0600E288 RID: 57992
		// (set) Token: 0x0600E287 RID: 57991
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

		// Token: 0x0600E289 RID: 57993
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x0600E28A RID: 57994
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600E28B RID: 57995
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17004BB4 RID: 19380
		// (get) Token: 0x0600E28C RID: 57996
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004BB5 RID: 19381
		// (get) Token: 0x0600E28D RID: 57997
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004BB6 RID: 19382
		// (get) Token: 0x0600E28E RID: 57998
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004BB7 RID: 19383
		// (get) Token: 0x0600E28F RID: 57999
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004BB8 RID: 19384
		// (get) Token: 0x0600E291 RID: 58001
		// (set) Token: 0x0600E290 RID: 58000
		public virtual extern string IHTMLEmbedElement_hidden
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004BB9 RID: 19385
		// (get) Token: 0x0600E292 RID: 58002
		public virtual extern string IHTMLEmbedElement_palette
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004BBA RID: 19386
		// (get) Token: 0x0600E293 RID: 58003
		public virtual extern string IHTMLEmbedElement_pluginspage
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004BBB RID: 19387
		// (get) Token: 0x0600E295 RID: 58005
		// (set) Token: 0x0600E294 RID: 58004
		public virtual extern string IHTMLEmbedElement_src
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004BBC RID: 19388
		// (get) Token: 0x0600E297 RID: 58007
		// (set) Token: 0x0600E296 RID: 58006
		public virtual extern string IHTMLEmbedElement_units
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004BBD RID: 19389
		// (get) Token: 0x0600E299 RID: 58009
		// (set) Token: 0x0600E298 RID: 58008
		public virtual extern string IHTMLEmbedElement_name
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

		// Token: 0x17004BBE RID: 19390
		// (get) Token: 0x0600E29B RID: 58011
		// (set) Token: 0x0600E29A RID: 58010
		public virtual extern object IHTMLEmbedElement_width
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004BBF RID: 19391
		// (get) Token: 0x0600E29D RID: 58013
		// (set) Token: 0x0600E29C RID: 58012
		public virtual extern object IHTMLEmbedElement_height
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x14001B09 RID: 6921
		// (add) Token: 0x0600E29E RID: 58014
		// (remove) Token: 0x0600E29F RID: 58015
		public virtual extern event HTMLControlElementEvents_onhelpEventHandler HTMLControlElementEvents_Event_onhelp;

		// Token: 0x14001B0A RID: 6922
		// (add) Token: 0x0600E2A0 RID: 58016
		// (remove) Token: 0x0600E2A1 RID: 58017
		public virtual extern event HTMLControlElementEvents_onclickEventHandler HTMLControlElementEvents_Event_onclick;

		// Token: 0x14001B0B RID: 6923
		// (add) Token: 0x0600E2A2 RID: 58018
		// (remove) Token: 0x0600E2A3 RID: 58019
		public virtual extern event HTMLControlElementEvents_ondblclickEventHandler HTMLControlElementEvents_Event_ondblclick;

		// Token: 0x14001B0C RID: 6924
		// (add) Token: 0x0600E2A4 RID: 58020
		// (remove) Token: 0x0600E2A5 RID: 58021
		public virtual extern event HTMLControlElementEvents_onkeypressEventHandler HTMLControlElementEvents_Event_onkeypress;

		// Token: 0x14001B0D RID: 6925
		// (add) Token: 0x0600E2A6 RID: 58022
		// (remove) Token: 0x0600E2A7 RID: 58023
		public virtual extern event HTMLControlElementEvents_onkeydownEventHandler HTMLControlElementEvents_Event_onkeydown;

		// Token: 0x14001B0E RID: 6926
		// (add) Token: 0x0600E2A8 RID: 58024
		// (remove) Token: 0x0600E2A9 RID: 58025
		public virtual extern event HTMLControlElementEvents_onkeyupEventHandler HTMLControlElementEvents_Event_onkeyup;

		// Token: 0x14001B0F RID: 6927
		// (add) Token: 0x0600E2AA RID: 58026
		// (remove) Token: 0x0600E2AB RID: 58027
		public virtual extern event HTMLControlElementEvents_onmouseoutEventHandler HTMLControlElementEvents_Event_onmouseout;

		// Token: 0x14001B10 RID: 6928
		// (add) Token: 0x0600E2AC RID: 58028
		// (remove) Token: 0x0600E2AD RID: 58029
		public virtual extern event HTMLControlElementEvents_onmouseoverEventHandler HTMLControlElementEvents_Event_onmouseover;

		// Token: 0x14001B11 RID: 6929
		// (add) Token: 0x0600E2AE RID: 58030
		// (remove) Token: 0x0600E2AF RID: 58031
		public virtual extern event HTMLControlElementEvents_onmousemoveEventHandler HTMLControlElementEvents_Event_onmousemove;

		// Token: 0x14001B12 RID: 6930
		// (add) Token: 0x0600E2B0 RID: 58032
		// (remove) Token: 0x0600E2B1 RID: 58033
		public virtual extern event HTMLControlElementEvents_onmousedownEventHandler HTMLControlElementEvents_Event_onmousedown;

		// Token: 0x14001B13 RID: 6931
		// (add) Token: 0x0600E2B2 RID: 58034
		// (remove) Token: 0x0600E2B3 RID: 58035
		public virtual extern event HTMLControlElementEvents_onmouseupEventHandler HTMLControlElementEvents_Event_onmouseup;

		// Token: 0x14001B14 RID: 6932
		// (add) Token: 0x0600E2B4 RID: 58036
		// (remove) Token: 0x0600E2B5 RID: 58037
		public virtual extern event HTMLControlElementEvents_onselectstartEventHandler HTMLControlElementEvents_Event_onselectstart;

		// Token: 0x14001B15 RID: 6933
		// (add) Token: 0x0600E2B6 RID: 58038
		// (remove) Token: 0x0600E2B7 RID: 58039
		public virtual extern event HTMLControlElementEvents_onfilterchangeEventHandler HTMLControlElementEvents_Event_onfilterchange;

		// Token: 0x14001B16 RID: 6934
		// (add) Token: 0x0600E2B8 RID: 58040
		// (remove) Token: 0x0600E2B9 RID: 58041
		public virtual extern event HTMLControlElementEvents_ondragstartEventHandler HTMLControlElementEvents_Event_ondragstart;

		// Token: 0x14001B17 RID: 6935
		// (add) Token: 0x0600E2BA RID: 58042
		// (remove) Token: 0x0600E2BB RID: 58043
		public virtual extern event HTMLControlElementEvents_onbeforeupdateEventHandler HTMLControlElementEvents_Event_onbeforeupdate;

		// Token: 0x14001B18 RID: 6936
		// (add) Token: 0x0600E2BC RID: 58044
		// (remove) Token: 0x0600E2BD RID: 58045
		public virtual extern event HTMLControlElementEvents_onafterupdateEventHandler HTMLControlElementEvents_Event_onafterupdate;

		// Token: 0x14001B19 RID: 6937
		// (add) Token: 0x0600E2BE RID: 58046
		// (remove) Token: 0x0600E2BF RID: 58047
		public virtual extern event HTMLControlElementEvents_onerrorupdateEventHandler HTMLControlElementEvents_Event_onerrorupdate;

		// Token: 0x14001B1A RID: 6938
		// (add) Token: 0x0600E2C0 RID: 58048
		// (remove) Token: 0x0600E2C1 RID: 58049
		public virtual extern event HTMLControlElementEvents_onrowexitEventHandler HTMLControlElementEvents_Event_onrowexit;

		// Token: 0x14001B1B RID: 6939
		// (add) Token: 0x0600E2C2 RID: 58050
		// (remove) Token: 0x0600E2C3 RID: 58051
		public virtual extern event HTMLControlElementEvents_onrowenterEventHandler HTMLControlElementEvents_Event_onrowenter;

		// Token: 0x14001B1C RID: 6940
		// (add) Token: 0x0600E2C4 RID: 58052
		// (remove) Token: 0x0600E2C5 RID: 58053
		public virtual extern event HTMLControlElementEvents_ondatasetchangedEventHandler HTMLControlElementEvents_Event_ondatasetchanged;

		// Token: 0x14001B1D RID: 6941
		// (add) Token: 0x0600E2C6 RID: 58054
		// (remove) Token: 0x0600E2C7 RID: 58055
		public virtual extern event HTMLControlElementEvents_ondataavailableEventHandler HTMLControlElementEvents_Event_ondataavailable;

		// Token: 0x14001B1E RID: 6942
		// (add) Token: 0x0600E2C8 RID: 58056
		// (remove) Token: 0x0600E2C9 RID: 58057
		public virtual extern event HTMLControlElementEvents_ondatasetcompleteEventHandler HTMLControlElementEvents_Event_ondatasetcomplete;

		// Token: 0x14001B1F RID: 6943
		// (add) Token: 0x0600E2CA RID: 58058
		// (remove) Token: 0x0600E2CB RID: 58059
		public virtual extern event HTMLControlElementEvents_onlosecaptureEventHandler HTMLControlElementEvents_Event_onlosecapture;

		// Token: 0x14001B20 RID: 6944
		// (add) Token: 0x0600E2CC RID: 58060
		// (remove) Token: 0x0600E2CD RID: 58061
		public virtual extern event HTMLControlElementEvents_onpropertychangeEventHandler HTMLControlElementEvents_Event_onpropertychange;

		// Token: 0x14001B21 RID: 6945
		// (add) Token: 0x0600E2CE RID: 58062
		// (remove) Token: 0x0600E2CF RID: 58063
		public virtual extern event HTMLControlElementEvents_onscrollEventHandler HTMLControlElementEvents_Event_onscroll;

		// Token: 0x14001B22 RID: 6946
		// (add) Token: 0x0600E2D0 RID: 58064
		// (remove) Token: 0x0600E2D1 RID: 58065
		public virtual extern event HTMLControlElementEvents_onfocusEventHandler HTMLControlElementEvents_Event_onfocus;

		// Token: 0x14001B23 RID: 6947
		// (add) Token: 0x0600E2D2 RID: 58066
		// (remove) Token: 0x0600E2D3 RID: 58067
		public virtual extern event HTMLControlElementEvents_onblurEventHandler HTMLControlElementEvents_Event_onblur;

		// Token: 0x14001B24 RID: 6948
		// (add) Token: 0x0600E2D4 RID: 58068
		// (remove) Token: 0x0600E2D5 RID: 58069
		public virtual extern event HTMLControlElementEvents_onresizeEventHandler HTMLControlElementEvents_Event_onresize;

		// Token: 0x14001B25 RID: 6949
		// (add) Token: 0x0600E2D6 RID: 58070
		// (remove) Token: 0x0600E2D7 RID: 58071
		public virtual extern event HTMLControlElementEvents_ondragEventHandler HTMLControlElementEvents_Event_ondrag;

		// Token: 0x14001B26 RID: 6950
		// (add) Token: 0x0600E2D8 RID: 58072
		// (remove) Token: 0x0600E2D9 RID: 58073
		public virtual extern event HTMLControlElementEvents_ondragendEventHandler HTMLControlElementEvents_Event_ondragend;

		// Token: 0x14001B27 RID: 6951
		// (add) Token: 0x0600E2DA RID: 58074
		// (remove) Token: 0x0600E2DB RID: 58075
		public virtual extern event HTMLControlElementEvents_ondragenterEventHandler HTMLControlElementEvents_Event_ondragenter;

		// Token: 0x14001B28 RID: 6952
		// (add) Token: 0x0600E2DC RID: 58076
		// (remove) Token: 0x0600E2DD RID: 58077
		public virtual extern event HTMLControlElementEvents_ondragoverEventHandler HTMLControlElementEvents_Event_ondragover;

		// Token: 0x14001B29 RID: 6953
		// (add) Token: 0x0600E2DE RID: 58078
		// (remove) Token: 0x0600E2DF RID: 58079
		public virtual extern event HTMLControlElementEvents_ondragleaveEventHandler HTMLControlElementEvents_Event_ondragleave;

		// Token: 0x14001B2A RID: 6954
		// (add) Token: 0x0600E2E0 RID: 58080
		// (remove) Token: 0x0600E2E1 RID: 58081
		public virtual extern event HTMLControlElementEvents_ondropEventHandler HTMLControlElementEvents_Event_ondrop;

		// Token: 0x14001B2B RID: 6955
		// (add) Token: 0x0600E2E2 RID: 58082
		// (remove) Token: 0x0600E2E3 RID: 58083
		public virtual extern event HTMLControlElementEvents_onbeforecutEventHandler HTMLControlElementEvents_Event_onbeforecut;

		// Token: 0x14001B2C RID: 6956
		// (add) Token: 0x0600E2E4 RID: 58084
		// (remove) Token: 0x0600E2E5 RID: 58085
		public virtual extern event HTMLControlElementEvents_oncutEventHandler HTMLControlElementEvents_Event_oncut;

		// Token: 0x14001B2D RID: 6957
		// (add) Token: 0x0600E2E6 RID: 58086
		// (remove) Token: 0x0600E2E7 RID: 58087
		public virtual extern event HTMLControlElementEvents_onbeforecopyEventHandler HTMLControlElementEvents_Event_onbeforecopy;

		// Token: 0x14001B2E RID: 6958
		// (add) Token: 0x0600E2E8 RID: 58088
		// (remove) Token: 0x0600E2E9 RID: 58089
		public virtual extern event HTMLControlElementEvents_oncopyEventHandler HTMLControlElementEvents_Event_oncopy;

		// Token: 0x14001B2F RID: 6959
		// (add) Token: 0x0600E2EA RID: 58090
		// (remove) Token: 0x0600E2EB RID: 58091
		public virtual extern event HTMLControlElementEvents_onbeforepasteEventHandler HTMLControlElementEvents_Event_onbeforepaste;

		// Token: 0x14001B30 RID: 6960
		// (add) Token: 0x0600E2EC RID: 58092
		// (remove) Token: 0x0600E2ED RID: 58093
		public virtual extern event HTMLControlElementEvents_onpasteEventHandler HTMLControlElementEvents_Event_onpaste;

		// Token: 0x14001B31 RID: 6961
		// (add) Token: 0x0600E2EE RID: 58094
		// (remove) Token: 0x0600E2EF RID: 58095
		public virtual extern event HTMLControlElementEvents_oncontextmenuEventHandler HTMLControlElementEvents_Event_oncontextmenu;

		// Token: 0x14001B32 RID: 6962
		// (add) Token: 0x0600E2F0 RID: 58096
		// (remove) Token: 0x0600E2F1 RID: 58097
		public virtual extern event HTMLControlElementEvents_onrowsdeleteEventHandler HTMLControlElementEvents_Event_onrowsdelete;

		// Token: 0x14001B33 RID: 6963
		// (add) Token: 0x0600E2F2 RID: 58098
		// (remove) Token: 0x0600E2F3 RID: 58099
		public virtual extern event HTMLControlElementEvents_onrowsinsertedEventHandler HTMLControlElementEvents_Event_onrowsinserted;

		// Token: 0x14001B34 RID: 6964
		// (add) Token: 0x0600E2F4 RID: 58100
		// (remove) Token: 0x0600E2F5 RID: 58101
		public virtual extern event HTMLControlElementEvents_oncellchangeEventHandler HTMLControlElementEvents_Event_oncellchange;

		// Token: 0x14001B35 RID: 6965
		// (add) Token: 0x0600E2F6 RID: 58102
		// (remove) Token: 0x0600E2F7 RID: 58103
		public virtual extern event HTMLControlElementEvents_onreadystatechangeEventHandler HTMLControlElementEvents_Event_onreadystatechange;

		// Token: 0x14001B36 RID: 6966
		// (add) Token: 0x0600E2F8 RID: 58104
		// (remove) Token: 0x0600E2F9 RID: 58105
		public virtual extern event HTMLControlElementEvents_onbeforeeditfocusEventHandler HTMLControlElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14001B37 RID: 6967
		// (add) Token: 0x0600E2FA RID: 58106
		// (remove) Token: 0x0600E2FB RID: 58107
		public virtual extern event HTMLControlElementEvents_onlayoutcompleteEventHandler HTMLControlElementEvents_Event_onlayoutcomplete;

		// Token: 0x14001B38 RID: 6968
		// (add) Token: 0x0600E2FC RID: 58108
		// (remove) Token: 0x0600E2FD RID: 58109
		public virtual extern event HTMLControlElementEvents_onpageEventHandler HTMLControlElementEvents_Event_onpage;

		// Token: 0x14001B39 RID: 6969
		// (add) Token: 0x0600E2FE RID: 58110
		// (remove) Token: 0x0600E2FF RID: 58111
		public virtual extern event HTMLControlElementEvents_onbeforedeactivateEventHandler HTMLControlElementEvents_Event_onbeforedeactivate;

		// Token: 0x14001B3A RID: 6970
		// (add) Token: 0x0600E300 RID: 58112
		// (remove) Token: 0x0600E301 RID: 58113
		public virtual extern event HTMLControlElementEvents_onbeforeactivateEventHandler HTMLControlElementEvents_Event_onbeforeactivate;

		// Token: 0x14001B3B RID: 6971
		// (add) Token: 0x0600E302 RID: 58114
		// (remove) Token: 0x0600E303 RID: 58115
		public virtual extern event HTMLControlElementEvents_onmoveEventHandler HTMLControlElementEvents_Event_onmove;

		// Token: 0x14001B3C RID: 6972
		// (add) Token: 0x0600E304 RID: 58116
		// (remove) Token: 0x0600E305 RID: 58117
		public virtual extern event HTMLControlElementEvents_oncontrolselectEventHandler HTMLControlElementEvents_Event_oncontrolselect;

		// Token: 0x14001B3D RID: 6973
		// (add) Token: 0x0600E306 RID: 58118
		// (remove) Token: 0x0600E307 RID: 58119
		public virtual extern event HTMLControlElementEvents_onmovestartEventHandler HTMLControlElementEvents_Event_onmovestart;

		// Token: 0x14001B3E RID: 6974
		// (add) Token: 0x0600E308 RID: 58120
		// (remove) Token: 0x0600E309 RID: 58121
		public virtual extern event HTMLControlElementEvents_onmoveendEventHandler HTMLControlElementEvents_Event_onmoveend;

		// Token: 0x14001B3F RID: 6975
		// (add) Token: 0x0600E30A RID: 58122
		// (remove) Token: 0x0600E30B RID: 58123
		public virtual extern event HTMLControlElementEvents_onresizestartEventHandler HTMLControlElementEvents_Event_onresizestart;

		// Token: 0x14001B40 RID: 6976
		// (add) Token: 0x0600E30C RID: 58124
		// (remove) Token: 0x0600E30D RID: 58125
		public virtual extern event HTMLControlElementEvents_onresizeendEventHandler HTMLControlElementEvents_Event_onresizeend;

		// Token: 0x14001B41 RID: 6977
		// (add) Token: 0x0600E30E RID: 58126
		// (remove) Token: 0x0600E30F RID: 58127
		public virtual extern event HTMLControlElementEvents_onmouseenterEventHandler HTMLControlElementEvents_Event_onmouseenter;

		// Token: 0x14001B42 RID: 6978
		// (add) Token: 0x0600E310 RID: 58128
		// (remove) Token: 0x0600E311 RID: 58129
		public virtual extern event HTMLControlElementEvents_onmouseleaveEventHandler HTMLControlElementEvents_Event_onmouseleave;

		// Token: 0x14001B43 RID: 6979
		// (add) Token: 0x0600E312 RID: 58130
		// (remove) Token: 0x0600E313 RID: 58131
		public virtual extern event HTMLControlElementEvents_onmousewheelEventHandler HTMLControlElementEvents_Event_onmousewheel;

		// Token: 0x14001B44 RID: 6980
		// (add) Token: 0x0600E314 RID: 58132
		// (remove) Token: 0x0600E315 RID: 58133
		public virtual extern event HTMLControlElementEvents_onactivateEventHandler HTMLControlElementEvents_Event_onactivate;

		// Token: 0x14001B45 RID: 6981
		// (add) Token: 0x0600E316 RID: 58134
		// (remove) Token: 0x0600E317 RID: 58135
		public virtual extern event HTMLControlElementEvents_ondeactivateEventHandler HTMLControlElementEvents_Event_ondeactivate;

		// Token: 0x14001B46 RID: 6982
		// (add) Token: 0x0600E318 RID: 58136
		// (remove) Token: 0x0600E319 RID: 58137
		public virtual extern event HTMLControlElementEvents_onfocusinEventHandler HTMLControlElementEvents_Event_onfocusin;

		// Token: 0x14001B47 RID: 6983
		// (add) Token: 0x0600E31A RID: 58138
		// (remove) Token: 0x0600E31B RID: 58139
		public virtual extern event HTMLControlElementEvents_onfocusoutEventHandler HTMLControlElementEvents_Event_onfocusout;

		// Token: 0x14001B48 RID: 6984
		// (add) Token: 0x0600E31C RID: 58140
		// (remove) Token: 0x0600E31D RID: 58141
		public virtual extern event HTMLControlElementEvents2_onhelpEventHandler HTMLControlElementEvents2_Event_onhelp;

		// Token: 0x14001B49 RID: 6985
		// (add) Token: 0x0600E31E RID: 58142
		// (remove) Token: 0x0600E31F RID: 58143
		public virtual extern event HTMLControlElementEvents2_onclickEventHandler HTMLControlElementEvents2_Event_onclick;

		// Token: 0x14001B4A RID: 6986
		// (add) Token: 0x0600E320 RID: 58144
		// (remove) Token: 0x0600E321 RID: 58145
		public virtual extern event HTMLControlElementEvents2_ondblclickEventHandler HTMLControlElementEvents2_Event_ondblclick;

		// Token: 0x14001B4B RID: 6987
		// (add) Token: 0x0600E322 RID: 58146
		// (remove) Token: 0x0600E323 RID: 58147
		public virtual extern event HTMLControlElementEvents2_onkeypressEventHandler HTMLControlElementEvents2_Event_onkeypress;

		// Token: 0x14001B4C RID: 6988
		// (add) Token: 0x0600E324 RID: 58148
		// (remove) Token: 0x0600E325 RID: 58149
		public virtual extern event HTMLControlElementEvents2_onkeydownEventHandler HTMLControlElementEvents2_Event_onkeydown;

		// Token: 0x14001B4D RID: 6989
		// (add) Token: 0x0600E326 RID: 58150
		// (remove) Token: 0x0600E327 RID: 58151
		public virtual extern event HTMLControlElementEvents2_onkeyupEventHandler HTMLControlElementEvents2_Event_onkeyup;

		// Token: 0x14001B4E RID: 6990
		// (add) Token: 0x0600E328 RID: 58152
		// (remove) Token: 0x0600E329 RID: 58153
		public virtual extern event HTMLControlElementEvents2_onmouseoutEventHandler HTMLControlElementEvents2_Event_onmouseout;

		// Token: 0x14001B4F RID: 6991
		// (add) Token: 0x0600E32A RID: 58154
		// (remove) Token: 0x0600E32B RID: 58155
		public virtual extern event HTMLControlElementEvents2_onmouseoverEventHandler HTMLControlElementEvents2_Event_onmouseover;

		// Token: 0x14001B50 RID: 6992
		// (add) Token: 0x0600E32C RID: 58156
		// (remove) Token: 0x0600E32D RID: 58157
		public virtual extern event HTMLControlElementEvents2_onmousemoveEventHandler HTMLControlElementEvents2_Event_onmousemove;

		// Token: 0x14001B51 RID: 6993
		// (add) Token: 0x0600E32E RID: 58158
		// (remove) Token: 0x0600E32F RID: 58159
		public virtual extern event HTMLControlElementEvents2_onmousedownEventHandler HTMLControlElementEvents2_Event_onmousedown;

		// Token: 0x14001B52 RID: 6994
		// (add) Token: 0x0600E330 RID: 58160
		// (remove) Token: 0x0600E331 RID: 58161
		public virtual extern event HTMLControlElementEvents2_onmouseupEventHandler HTMLControlElementEvents2_Event_onmouseup;

		// Token: 0x14001B53 RID: 6995
		// (add) Token: 0x0600E332 RID: 58162
		// (remove) Token: 0x0600E333 RID: 58163
		public virtual extern event HTMLControlElementEvents2_onselectstartEventHandler HTMLControlElementEvents2_Event_onselectstart;

		// Token: 0x14001B54 RID: 6996
		// (add) Token: 0x0600E334 RID: 58164
		// (remove) Token: 0x0600E335 RID: 58165
		public virtual extern event HTMLControlElementEvents2_onfilterchangeEventHandler HTMLControlElementEvents2_Event_onfilterchange;

		// Token: 0x14001B55 RID: 6997
		// (add) Token: 0x0600E336 RID: 58166
		// (remove) Token: 0x0600E337 RID: 58167
		public virtual extern event HTMLControlElementEvents2_ondragstartEventHandler HTMLControlElementEvents2_Event_ondragstart;

		// Token: 0x14001B56 RID: 6998
		// (add) Token: 0x0600E338 RID: 58168
		// (remove) Token: 0x0600E339 RID: 58169
		public virtual extern event HTMLControlElementEvents2_onbeforeupdateEventHandler HTMLControlElementEvents2_Event_onbeforeupdate;

		// Token: 0x14001B57 RID: 6999
		// (add) Token: 0x0600E33A RID: 58170
		// (remove) Token: 0x0600E33B RID: 58171
		public virtual extern event HTMLControlElementEvents2_onafterupdateEventHandler HTMLControlElementEvents2_Event_onafterupdate;

		// Token: 0x14001B58 RID: 7000
		// (add) Token: 0x0600E33C RID: 58172
		// (remove) Token: 0x0600E33D RID: 58173
		public virtual extern event HTMLControlElementEvents2_onerrorupdateEventHandler HTMLControlElementEvents2_Event_onerrorupdate;

		// Token: 0x14001B59 RID: 7001
		// (add) Token: 0x0600E33E RID: 58174
		// (remove) Token: 0x0600E33F RID: 58175
		public virtual extern event HTMLControlElementEvents2_onrowexitEventHandler HTMLControlElementEvents2_Event_onrowexit;

		// Token: 0x14001B5A RID: 7002
		// (add) Token: 0x0600E340 RID: 58176
		// (remove) Token: 0x0600E341 RID: 58177
		public virtual extern event HTMLControlElementEvents2_onrowenterEventHandler HTMLControlElementEvents2_Event_onrowenter;

		// Token: 0x14001B5B RID: 7003
		// (add) Token: 0x0600E342 RID: 58178
		// (remove) Token: 0x0600E343 RID: 58179
		public virtual extern event HTMLControlElementEvents2_ondatasetchangedEventHandler HTMLControlElementEvents2_Event_ondatasetchanged;

		// Token: 0x14001B5C RID: 7004
		// (add) Token: 0x0600E344 RID: 58180
		// (remove) Token: 0x0600E345 RID: 58181
		public virtual extern event HTMLControlElementEvents2_ondataavailableEventHandler HTMLControlElementEvents2_Event_ondataavailable;

		// Token: 0x14001B5D RID: 7005
		// (add) Token: 0x0600E346 RID: 58182
		// (remove) Token: 0x0600E347 RID: 58183
		public virtual extern event HTMLControlElementEvents2_ondatasetcompleteEventHandler HTMLControlElementEvents2_Event_ondatasetcomplete;

		// Token: 0x14001B5E RID: 7006
		// (add) Token: 0x0600E348 RID: 58184
		// (remove) Token: 0x0600E349 RID: 58185
		public virtual extern event HTMLControlElementEvents2_onlosecaptureEventHandler HTMLControlElementEvents2_Event_onlosecapture;

		// Token: 0x14001B5F RID: 7007
		// (add) Token: 0x0600E34A RID: 58186
		// (remove) Token: 0x0600E34B RID: 58187
		public virtual extern event HTMLControlElementEvents2_onpropertychangeEventHandler HTMLControlElementEvents2_Event_onpropertychange;

		// Token: 0x14001B60 RID: 7008
		// (add) Token: 0x0600E34C RID: 58188
		// (remove) Token: 0x0600E34D RID: 58189
		public virtual extern event HTMLControlElementEvents2_onscrollEventHandler HTMLControlElementEvents2_Event_onscroll;

		// Token: 0x14001B61 RID: 7009
		// (add) Token: 0x0600E34E RID: 58190
		// (remove) Token: 0x0600E34F RID: 58191
		public virtual extern event HTMLControlElementEvents2_onfocusEventHandler HTMLControlElementEvents2_Event_onfocus;

		// Token: 0x14001B62 RID: 7010
		// (add) Token: 0x0600E350 RID: 58192
		// (remove) Token: 0x0600E351 RID: 58193
		public virtual extern event HTMLControlElementEvents2_onblurEventHandler HTMLControlElementEvents2_Event_onblur;

		// Token: 0x14001B63 RID: 7011
		// (add) Token: 0x0600E352 RID: 58194
		// (remove) Token: 0x0600E353 RID: 58195
		public virtual extern event HTMLControlElementEvents2_onresizeEventHandler HTMLControlElementEvents2_Event_onresize;

		// Token: 0x14001B64 RID: 7012
		// (add) Token: 0x0600E354 RID: 58196
		// (remove) Token: 0x0600E355 RID: 58197
		public virtual extern event HTMLControlElementEvents2_ondragEventHandler HTMLControlElementEvents2_Event_ondrag;

		// Token: 0x14001B65 RID: 7013
		// (add) Token: 0x0600E356 RID: 58198
		// (remove) Token: 0x0600E357 RID: 58199
		public virtual extern event HTMLControlElementEvents2_ondragendEventHandler HTMLControlElementEvents2_Event_ondragend;

		// Token: 0x14001B66 RID: 7014
		// (add) Token: 0x0600E358 RID: 58200
		// (remove) Token: 0x0600E359 RID: 58201
		public virtual extern event HTMLControlElementEvents2_ondragenterEventHandler HTMLControlElementEvents2_Event_ondragenter;

		// Token: 0x14001B67 RID: 7015
		// (add) Token: 0x0600E35A RID: 58202
		// (remove) Token: 0x0600E35B RID: 58203
		public virtual extern event HTMLControlElementEvents2_ondragoverEventHandler HTMLControlElementEvents2_Event_ondragover;

		// Token: 0x14001B68 RID: 7016
		// (add) Token: 0x0600E35C RID: 58204
		// (remove) Token: 0x0600E35D RID: 58205
		public virtual extern event HTMLControlElementEvents2_ondragleaveEventHandler HTMLControlElementEvents2_Event_ondragleave;

		// Token: 0x14001B69 RID: 7017
		// (add) Token: 0x0600E35E RID: 58206
		// (remove) Token: 0x0600E35F RID: 58207
		public virtual extern event HTMLControlElementEvents2_ondropEventHandler HTMLControlElementEvents2_Event_ondrop;

		// Token: 0x14001B6A RID: 7018
		// (add) Token: 0x0600E360 RID: 58208
		// (remove) Token: 0x0600E361 RID: 58209
		public virtual extern event HTMLControlElementEvents2_onbeforecutEventHandler HTMLControlElementEvents2_Event_onbeforecut;

		// Token: 0x14001B6B RID: 7019
		// (add) Token: 0x0600E362 RID: 58210
		// (remove) Token: 0x0600E363 RID: 58211
		public virtual extern event HTMLControlElementEvents2_oncutEventHandler HTMLControlElementEvents2_Event_oncut;

		// Token: 0x14001B6C RID: 7020
		// (add) Token: 0x0600E364 RID: 58212
		// (remove) Token: 0x0600E365 RID: 58213
		public virtual extern event HTMLControlElementEvents2_onbeforecopyEventHandler HTMLControlElementEvents2_Event_onbeforecopy;

		// Token: 0x14001B6D RID: 7021
		// (add) Token: 0x0600E366 RID: 58214
		// (remove) Token: 0x0600E367 RID: 58215
		public virtual extern event HTMLControlElementEvents2_oncopyEventHandler HTMLControlElementEvents2_Event_oncopy;

		// Token: 0x14001B6E RID: 7022
		// (add) Token: 0x0600E368 RID: 58216
		// (remove) Token: 0x0600E369 RID: 58217
		public virtual extern event HTMLControlElementEvents2_onbeforepasteEventHandler HTMLControlElementEvents2_Event_onbeforepaste;

		// Token: 0x14001B6F RID: 7023
		// (add) Token: 0x0600E36A RID: 58218
		// (remove) Token: 0x0600E36B RID: 58219
		public virtual extern event HTMLControlElementEvents2_onpasteEventHandler HTMLControlElementEvents2_Event_onpaste;

		// Token: 0x14001B70 RID: 7024
		// (add) Token: 0x0600E36C RID: 58220
		// (remove) Token: 0x0600E36D RID: 58221
		public virtual extern event HTMLControlElementEvents2_oncontextmenuEventHandler HTMLControlElementEvents2_Event_oncontextmenu;

		// Token: 0x14001B71 RID: 7025
		// (add) Token: 0x0600E36E RID: 58222
		// (remove) Token: 0x0600E36F RID: 58223
		public virtual extern event HTMLControlElementEvents2_onrowsdeleteEventHandler HTMLControlElementEvents2_Event_onrowsdelete;

		// Token: 0x14001B72 RID: 7026
		// (add) Token: 0x0600E370 RID: 58224
		// (remove) Token: 0x0600E371 RID: 58225
		public virtual extern event HTMLControlElementEvents2_onrowsinsertedEventHandler HTMLControlElementEvents2_Event_onrowsinserted;

		// Token: 0x14001B73 RID: 7027
		// (add) Token: 0x0600E372 RID: 58226
		// (remove) Token: 0x0600E373 RID: 58227
		public virtual extern event HTMLControlElementEvents2_oncellchangeEventHandler HTMLControlElementEvents2_Event_oncellchange;

		// Token: 0x14001B74 RID: 7028
		// (add) Token: 0x0600E374 RID: 58228
		// (remove) Token: 0x0600E375 RID: 58229
		public virtual extern event HTMLControlElementEvents2_onreadystatechangeEventHandler HTMLControlElementEvents2_Event_onreadystatechange;

		// Token: 0x14001B75 RID: 7029
		// (add) Token: 0x0600E376 RID: 58230
		// (remove) Token: 0x0600E377 RID: 58231
		public virtual extern event HTMLControlElementEvents2_onlayoutcompleteEventHandler HTMLControlElementEvents2_Event_onlayoutcomplete;

		// Token: 0x14001B76 RID: 7030
		// (add) Token: 0x0600E378 RID: 58232
		// (remove) Token: 0x0600E379 RID: 58233
		public virtual extern event HTMLControlElementEvents2_onpageEventHandler HTMLControlElementEvents2_Event_onpage;

		// Token: 0x14001B77 RID: 7031
		// (add) Token: 0x0600E37A RID: 58234
		// (remove) Token: 0x0600E37B RID: 58235
		public virtual extern event HTMLControlElementEvents2_onmouseenterEventHandler HTMLControlElementEvents2_Event_onmouseenter;

		// Token: 0x14001B78 RID: 7032
		// (add) Token: 0x0600E37C RID: 58236
		// (remove) Token: 0x0600E37D RID: 58237
		public virtual extern event HTMLControlElementEvents2_onmouseleaveEventHandler HTMLControlElementEvents2_Event_onmouseleave;

		// Token: 0x14001B79 RID: 7033
		// (add) Token: 0x0600E37E RID: 58238
		// (remove) Token: 0x0600E37F RID: 58239
		public virtual extern event HTMLControlElementEvents2_onactivateEventHandler HTMLControlElementEvents2_Event_onactivate;

		// Token: 0x14001B7A RID: 7034
		// (add) Token: 0x0600E380 RID: 58240
		// (remove) Token: 0x0600E381 RID: 58241
		public virtual extern event HTMLControlElementEvents2_ondeactivateEventHandler HTMLControlElementEvents2_Event_ondeactivate;

		// Token: 0x14001B7B RID: 7035
		// (add) Token: 0x0600E382 RID: 58242
		// (remove) Token: 0x0600E383 RID: 58243
		public virtual extern event HTMLControlElementEvents2_onbeforedeactivateEventHandler HTMLControlElementEvents2_Event_onbeforedeactivate;

		// Token: 0x14001B7C RID: 7036
		// (add) Token: 0x0600E384 RID: 58244
		// (remove) Token: 0x0600E385 RID: 58245
		public virtual extern event HTMLControlElementEvents2_onbeforeactivateEventHandler HTMLControlElementEvents2_Event_onbeforeactivate;

		// Token: 0x14001B7D RID: 7037
		// (add) Token: 0x0600E386 RID: 58246
		// (remove) Token: 0x0600E387 RID: 58247
		public virtual extern event HTMLControlElementEvents2_onfocusinEventHandler HTMLControlElementEvents2_Event_onfocusin;

		// Token: 0x14001B7E RID: 7038
		// (add) Token: 0x0600E388 RID: 58248
		// (remove) Token: 0x0600E389 RID: 58249
		public virtual extern event HTMLControlElementEvents2_onfocusoutEventHandler HTMLControlElementEvents2_Event_onfocusout;

		// Token: 0x14001B7F RID: 7039
		// (add) Token: 0x0600E38A RID: 58250
		// (remove) Token: 0x0600E38B RID: 58251
		public virtual extern event HTMLControlElementEvents2_onmoveEventHandler HTMLControlElementEvents2_Event_onmove;

		// Token: 0x14001B80 RID: 7040
		// (add) Token: 0x0600E38C RID: 58252
		// (remove) Token: 0x0600E38D RID: 58253
		public virtual extern event HTMLControlElementEvents2_oncontrolselectEventHandler HTMLControlElementEvents2_Event_oncontrolselect;

		// Token: 0x14001B81 RID: 7041
		// (add) Token: 0x0600E38E RID: 58254
		// (remove) Token: 0x0600E38F RID: 58255
		public virtual extern event HTMLControlElementEvents2_onmovestartEventHandler HTMLControlElementEvents2_Event_onmovestart;

		// Token: 0x14001B82 RID: 7042
		// (add) Token: 0x0600E390 RID: 58256
		// (remove) Token: 0x0600E391 RID: 58257
		public virtual extern event HTMLControlElementEvents2_onmoveendEventHandler HTMLControlElementEvents2_Event_onmoveend;

		// Token: 0x14001B83 RID: 7043
		// (add) Token: 0x0600E392 RID: 58258
		// (remove) Token: 0x0600E393 RID: 58259
		public virtual extern event HTMLControlElementEvents2_onresizestartEventHandler HTMLControlElementEvents2_Event_onresizestart;

		// Token: 0x14001B84 RID: 7044
		// (add) Token: 0x0600E394 RID: 58260
		// (remove) Token: 0x0600E395 RID: 58261
		public virtual extern event HTMLControlElementEvents2_onresizeendEventHandler HTMLControlElementEvents2_Event_onresizeend;

		// Token: 0x14001B85 RID: 7045
		// (add) Token: 0x0600E396 RID: 58262
		// (remove) Token: 0x0600E397 RID: 58263
		public virtual extern event HTMLControlElementEvents2_onmousewheelEventHandler HTMLControlElementEvents2_Event_onmousewheel;
	}
}
