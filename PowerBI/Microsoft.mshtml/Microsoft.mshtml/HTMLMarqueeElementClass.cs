using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020006E4 RID: 1764
	[ClassInterface(0)]
	[Guid("3050F2B9-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComSourceInterfaces("mshtml.HTMLMarqueeElementEvents\0mshtml.HTMLMarqueeElementEvents2\0\0")]
	[ComImport]
	public class HTMLMarqueeElementClass : DispHTMLMarqueeElement, HTMLMarqueeElement, HTMLMarqueeElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLTextContainer, IHTMLMarqueeElement, HTMLMarqueeElementEvents2_Event
	{
		// Token: 0x0600A9DB RID: 43483
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLMarqueeElementClass();

		// Token: 0x0600A9DC RID: 43484
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600A9DD RID: 43485
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600A9DE RID: 43486
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700371C RID: 14108
		// (get) Token: 0x0600A9E0 RID: 43488
		// (set) Token: 0x0600A9DF RID: 43487
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

		// Token: 0x1700371D RID: 14109
		// (get) Token: 0x0600A9E2 RID: 43490
		// (set) Token: 0x0600A9E1 RID: 43489
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

		// Token: 0x1700371E RID: 14110
		// (get) Token: 0x0600A9E3 RID: 43491
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700371F RID: 14111
		// (get) Token: 0x0600A9E4 RID: 43492
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003720 RID: 14112
		// (get) Token: 0x0600A9E5 RID: 43493
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003721 RID: 14113
		// (get) Token: 0x0600A9E7 RID: 43495
		// (set) Token: 0x0600A9E6 RID: 43494
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

		// Token: 0x17003722 RID: 14114
		// (get) Token: 0x0600A9E9 RID: 43497
		// (set) Token: 0x0600A9E8 RID: 43496
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

		// Token: 0x17003723 RID: 14115
		// (get) Token: 0x0600A9EB RID: 43499
		// (set) Token: 0x0600A9EA RID: 43498
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

		// Token: 0x17003724 RID: 14116
		// (get) Token: 0x0600A9ED RID: 43501
		// (set) Token: 0x0600A9EC RID: 43500
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

		// Token: 0x17003725 RID: 14117
		// (get) Token: 0x0600A9EF RID: 43503
		// (set) Token: 0x0600A9EE RID: 43502
		[DispId(-2147412106)]
		public virtual extern object onkeyup
		{
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003726 RID: 14118
		// (get) Token: 0x0600A9F1 RID: 43505
		// (set) Token: 0x0600A9F0 RID: 43504
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

		// Token: 0x17003727 RID: 14119
		// (get) Token: 0x0600A9F3 RID: 43507
		// (set) Token: 0x0600A9F2 RID: 43506
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

		// Token: 0x17003728 RID: 14120
		// (get) Token: 0x0600A9F5 RID: 43509
		// (set) Token: 0x0600A9F4 RID: 43508
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

		// Token: 0x17003729 RID: 14121
		// (get) Token: 0x0600A9F7 RID: 43511
		// (set) Token: 0x0600A9F6 RID: 43510
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

		// Token: 0x1700372A RID: 14122
		// (get) Token: 0x0600A9F9 RID: 43513
		// (set) Token: 0x0600A9F8 RID: 43512
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

		// Token: 0x1700372B RID: 14123
		// (get) Token: 0x0600A9FB RID: 43515
		// (set) Token: 0x0600A9FA RID: 43514
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

		// Token: 0x1700372C RID: 14124
		// (get) Token: 0x0600A9FC RID: 43516
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700372D RID: 14125
		// (get) Token: 0x0600A9FE RID: 43518
		// (set) Token: 0x0600A9FD RID: 43517
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

		// Token: 0x1700372E RID: 14126
		// (get) Token: 0x0600AA00 RID: 43520
		// (set) Token: 0x0600A9FF RID: 43519
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

		// Token: 0x1700372F RID: 14127
		// (get) Token: 0x0600AA02 RID: 43522
		// (set) Token: 0x0600AA01 RID: 43521
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

		// Token: 0x0600AA03 RID: 43523
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600AA04 RID: 43524
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17003730 RID: 14128
		// (get) Token: 0x0600AA05 RID: 43525
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003731 RID: 14129
		// (get) Token: 0x0600AA06 RID: 43526
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003732 RID: 14130
		// (get) Token: 0x0600AA08 RID: 43528
		// (set) Token: 0x0600AA07 RID: 43527
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

		// Token: 0x17003733 RID: 14131
		// (get) Token: 0x0600AA09 RID: 43529
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003734 RID: 14132
		// (get) Token: 0x0600AA0A RID: 43530
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003735 RID: 14133
		// (get) Token: 0x0600AA0B RID: 43531
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003736 RID: 14134
		// (get) Token: 0x0600AA0C RID: 43532
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003737 RID: 14135
		// (get) Token: 0x0600AA0D RID: 43533
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003738 RID: 14136
		// (get) Token: 0x0600AA0F RID: 43535
		// (set) Token: 0x0600AA0E RID: 43534
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

		// Token: 0x17003739 RID: 14137
		// (get) Token: 0x0600AA11 RID: 43537
		// (set) Token: 0x0600AA10 RID: 43536
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

		// Token: 0x1700373A RID: 14138
		// (get) Token: 0x0600AA13 RID: 43539
		// (set) Token: 0x0600AA12 RID: 43538
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

		// Token: 0x1700373B RID: 14139
		// (get) Token: 0x0600AA15 RID: 43541
		// (set) Token: 0x0600AA14 RID: 43540
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

		// Token: 0x0600AA16 RID: 43542
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600AA17 RID: 43543
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x1700373C RID: 14140
		// (get) Token: 0x0600AA18 RID: 43544
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700373D RID: 14141
		// (get) Token: 0x0600AA19 RID: 43545
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600AA1A RID: 43546
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x1700373E RID: 14142
		// (get) Token: 0x0600AA1B RID: 43547
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700373F RID: 14143
		// (get) Token: 0x0600AA1D RID: 43549
		// (set) Token: 0x0600AA1C RID: 43548
		[DispId(-2147412077)]
		public virtual extern object ondragstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600AA1E RID: 43550
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17003740 RID: 14144
		// (get) Token: 0x0600AA20 RID: 43552
		// (set) Token: 0x0600AA1F RID: 43551
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

		// Token: 0x17003741 RID: 14145
		// (get) Token: 0x0600AA22 RID: 43554
		// (set) Token: 0x0600AA21 RID: 43553
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

		// Token: 0x17003742 RID: 14146
		// (get) Token: 0x0600AA24 RID: 43556
		// (set) Token: 0x0600AA23 RID: 43555
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

		// Token: 0x17003743 RID: 14147
		// (get) Token: 0x0600AA26 RID: 43558
		// (set) Token: 0x0600AA25 RID: 43557
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

		// Token: 0x17003744 RID: 14148
		// (get) Token: 0x0600AA28 RID: 43560
		// (set) Token: 0x0600AA27 RID: 43559
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

		// Token: 0x17003745 RID: 14149
		// (get) Token: 0x0600AA2A RID: 43562
		// (set) Token: 0x0600AA29 RID: 43561
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

		// Token: 0x17003746 RID: 14150
		// (get) Token: 0x0600AA2C RID: 43564
		// (set) Token: 0x0600AA2B RID: 43563
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

		// Token: 0x17003747 RID: 14151
		// (get) Token: 0x0600AA2E RID: 43566
		// (set) Token: 0x0600AA2D RID: 43565
		[DispId(-2147412070)]
		public virtual extern object ondatasetcomplete
		{
			[DispId(-2147412070)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412070)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003748 RID: 14152
		// (get) Token: 0x0600AA30 RID: 43568
		// (set) Token: 0x0600AA2F RID: 43567
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

		// Token: 0x17003749 RID: 14153
		// (get) Token: 0x0600AA31 RID: 43569
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700374A RID: 14154
		// (get) Token: 0x0600AA32 RID: 43570
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700374B RID: 14155
		// (get) Token: 0x0600AA33 RID: 43571
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600AA34 RID: 43572
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0600AA35 RID: 43573
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x1700374C RID: 14156
		// (get) Token: 0x0600AA37 RID: 43575
		// (set) Token: 0x0600AA36 RID: 43574
		[DispId(-2147412066)]
		public virtual extern object onlosecapture
		{
			[DispId(-2147412066)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600AA38 RID: 43576
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600AA39 RID: 43577
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700374D RID: 14157
		// (get) Token: 0x0600AA3B RID: 43579
		// (set) Token: 0x0600AA3A RID: 43578
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

		// Token: 0x1700374E RID: 14158
		// (get) Token: 0x0600AA3D RID: 43581
		// (set) Token: 0x0600AA3C RID: 43580
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

		// Token: 0x1700374F RID: 14159
		// (get) Token: 0x0600AA3F RID: 43583
		// (set) Token: 0x0600AA3E RID: 43582
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

		// Token: 0x17003750 RID: 14160
		// (get) Token: 0x0600AA41 RID: 43585
		// (set) Token: 0x0600AA40 RID: 43584
		[DispId(-2147412061)]
		public virtual extern object ondragenter
		{
			[DispId(-2147412061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003751 RID: 14161
		// (get) Token: 0x0600AA43 RID: 43587
		// (set) Token: 0x0600AA42 RID: 43586
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

		// Token: 0x17003752 RID: 14162
		// (get) Token: 0x0600AA45 RID: 43589
		// (set) Token: 0x0600AA44 RID: 43588
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

		// Token: 0x17003753 RID: 14163
		// (get) Token: 0x0600AA47 RID: 43591
		// (set) Token: 0x0600AA46 RID: 43590
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

		// Token: 0x17003754 RID: 14164
		// (get) Token: 0x0600AA49 RID: 43593
		// (set) Token: 0x0600AA48 RID: 43592
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

		// Token: 0x17003755 RID: 14165
		// (get) Token: 0x0600AA4B RID: 43595
		// (set) Token: 0x0600AA4A RID: 43594
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

		// Token: 0x17003756 RID: 14166
		// (get) Token: 0x0600AA4D RID: 43597
		// (set) Token: 0x0600AA4C RID: 43596
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

		// Token: 0x17003757 RID: 14167
		// (get) Token: 0x0600AA4F RID: 43599
		// (set) Token: 0x0600AA4E RID: 43598
		[DispId(-2147412056)]
		public virtual extern object oncopy
		{
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003758 RID: 14168
		// (get) Token: 0x0600AA51 RID: 43601
		// (set) Token: 0x0600AA50 RID: 43600
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

		// Token: 0x17003759 RID: 14169
		// (get) Token: 0x0600AA53 RID: 43603
		// (set) Token: 0x0600AA52 RID: 43602
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

		// Token: 0x1700375A RID: 14170
		// (get) Token: 0x0600AA54 RID: 43604
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700375B RID: 14171
		// (get) Token: 0x0600AA56 RID: 43606
		// (set) Token: 0x0600AA55 RID: 43605
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

		// Token: 0x0600AA57 RID: 43607
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x0600AA58 RID: 43608
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x0600AA59 RID: 43609
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600AA5A RID: 43610
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600AA5B RID: 43611
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700375C RID: 14172
		// (get) Token: 0x0600AA5D RID: 43613
		// (set) Token: 0x0600AA5C RID: 43612
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

		// Token: 0x0600AA5E RID: 43614
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x1700375D RID: 14173
		// (get) Token: 0x0600AA60 RID: 43616
		// (set) Token: 0x0600AA5F RID: 43615
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

		// Token: 0x1700375E RID: 14174
		// (get) Token: 0x0600AA62 RID: 43618
		// (set) Token: 0x0600AA61 RID: 43617
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

		// Token: 0x1700375F RID: 14175
		// (get) Token: 0x0600AA64 RID: 43620
		// (set) Token: 0x0600AA63 RID: 43619
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

		// Token: 0x17003760 RID: 14176
		// (get) Token: 0x0600AA66 RID: 43622
		// (set) Token: 0x0600AA65 RID: 43621
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

		// Token: 0x0600AA67 RID: 43623
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600AA68 RID: 43624
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600AA69 RID: 43625
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17003761 RID: 14177
		// (get) Token: 0x0600AA6A RID: 43626
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003762 RID: 14178
		// (get) Token: 0x0600AA6B RID: 43627
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003763 RID: 14179
		// (get) Token: 0x0600AA6C RID: 43628
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003764 RID: 14180
		// (get) Token: 0x0600AA6D RID: 43629
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600AA6E RID: 43630
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600AA6F RID: 43631
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17003765 RID: 14181
		// (get) Token: 0x0600AA70 RID: 43632
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003766 RID: 14182
		// (get) Token: 0x0600AA72 RID: 43634
		// (set) Token: 0x0600AA71 RID: 43633
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

		// Token: 0x17003767 RID: 14183
		// (get) Token: 0x0600AA74 RID: 43636
		// (set) Token: 0x0600AA73 RID: 43635
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

		// Token: 0x17003768 RID: 14184
		// (get) Token: 0x0600AA76 RID: 43638
		// (set) Token: 0x0600AA75 RID: 43637
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

		// Token: 0x17003769 RID: 14185
		// (get) Token: 0x0600AA78 RID: 43640
		// (set) Token: 0x0600AA77 RID: 43639
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

		// Token: 0x1700376A RID: 14186
		// (get) Token: 0x0600AA7A RID: 43642
		// (set) Token: 0x0600AA79 RID: 43641
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

		// Token: 0x0600AA7B RID: 43643
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x1700376B RID: 14187
		// (get) Token: 0x0600AA7C RID: 43644
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700376C RID: 14188
		// (get) Token: 0x0600AA7D RID: 43645
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700376D RID: 14189
		// (get) Token: 0x0600AA7F RID: 43647
		// (set) Token: 0x0600AA7E RID: 43646
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

		// Token: 0x1700376E RID: 14190
		// (get) Token: 0x0600AA81 RID: 43649
		// (set) Token: 0x0600AA80 RID: 43648
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

		// Token: 0x0600AA82 RID: 43650
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x1700376F RID: 14191
		// (get) Token: 0x0600AA84 RID: 43652
		// (set) Token: 0x0600AA83 RID: 43651
		[DispId(-2147412047)]
		public virtual extern object oncontextmenu
		{
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600AA85 RID: 43653
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600AA86 RID: 43654
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600AA87 RID: 43655
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600AA88 RID: 43656
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17003770 RID: 14192
		// (get) Token: 0x0600AA89 RID: 43657
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600AA8A RID: 43658
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600AA8B RID: 43659
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17003771 RID: 14193
		// (get) Token: 0x0600AA8C RID: 43660
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003772 RID: 14194
		// (get) Token: 0x0600AA8D RID: 43661
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003773 RID: 14195
		// (get) Token: 0x0600AA8F RID: 43663
		// (set) Token: 0x0600AA8E RID: 43662
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

		// Token: 0x17003774 RID: 14196
		// (get) Token: 0x0600AA91 RID: 43665
		// (set) Token: 0x0600AA90 RID: 43664
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

		// Token: 0x17003775 RID: 14197
		// (get) Token: 0x0600AA92 RID: 43666
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600AA93 RID: 43667
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600AA94 RID: 43668
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17003776 RID: 14198
		// (get) Token: 0x0600AA95 RID: 43669
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003777 RID: 14199
		// (get) Token: 0x0600AA96 RID: 43670
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003778 RID: 14200
		// (get) Token: 0x0600AA98 RID: 43672
		// (set) Token: 0x0600AA97 RID: 43671
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

		// Token: 0x17003779 RID: 14201
		// (get) Token: 0x0600AA9A RID: 43674
		// (set) Token: 0x0600AA99 RID: 43673
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

		// Token: 0x1700377A RID: 14202
		// (get) Token: 0x0600AA9C RID: 43676
		// (set) Token: 0x0600AA9B RID: 43675
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

		// Token: 0x1700377B RID: 14203
		// (get) Token: 0x0600AA9E RID: 43678
		// (set) Token: 0x0600AA9D RID: 43677
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

		// Token: 0x0600AA9F RID: 43679
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x1700377C RID: 14204
		// (get) Token: 0x0600AAA1 RID: 43681
		// (set) Token: 0x0600AAA0 RID: 43680
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

		// Token: 0x1700377D RID: 14205
		// (get) Token: 0x0600AAA2 RID: 43682
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700377E RID: 14206
		// (get) Token: 0x0600AAA4 RID: 43684
		// (set) Token: 0x0600AAA3 RID: 43683
		[DispId(-2147412949)]
		public virtual extern bool hideFocus
		{
			[DispId(-2147412949)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700377F RID: 14207
		// (get) Token: 0x0600AAA6 RID: 43686
		// (set) Token: 0x0600AAA5 RID: 43685
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

		// Token: 0x17003780 RID: 14208
		// (get) Token: 0x0600AAA7 RID: 43687
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003781 RID: 14209
		// (get) Token: 0x0600AAA9 RID: 43689
		// (set) Token: 0x0600AAA8 RID: 43688
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

		// Token: 0x17003782 RID: 14210
		// (get) Token: 0x0600AAAB RID: 43691
		// (set) Token: 0x0600AAAA RID: 43690
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

		// Token: 0x0600AAAC RID: 43692
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17003783 RID: 14211
		// (get) Token: 0x0600AAAE RID: 43694
		// (set) Token: 0x0600AAAD RID: 43693
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

		// Token: 0x17003784 RID: 14212
		// (get) Token: 0x0600AAB0 RID: 43696
		// (set) Token: 0x0600AAAF RID: 43695
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

		// Token: 0x17003785 RID: 14213
		// (get) Token: 0x0600AAB2 RID: 43698
		// (set) Token: 0x0600AAB1 RID: 43697
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

		// Token: 0x17003786 RID: 14214
		// (get) Token: 0x0600AAB4 RID: 43700
		// (set) Token: 0x0600AAB3 RID: 43699
		[DispId(-2147412030)]
		public virtual extern object onmoveend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003787 RID: 14215
		// (get) Token: 0x0600AAB6 RID: 43702
		// (set) Token: 0x0600AAB5 RID: 43701
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

		// Token: 0x17003788 RID: 14216
		// (get) Token: 0x0600AAB8 RID: 43704
		// (set) Token: 0x0600AAB7 RID: 43703
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

		// Token: 0x17003789 RID: 14217
		// (get) Token: 0x0600AABA RID: 43706
		// (set) Token: 0x0600AAB9 RID: 43705
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

		// Token: 0x1700378A RID: 14218
		// (get) Token: 0x0600AABC RID: 43708
		// (set) Token: 0x0600AABB RID: 43707
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

		// Token: 0x0600AABD RID: 43709
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x1700378B RID: 14219
		// (get) Token: 0x0600AABE RID: 43710
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700378C RID: 14220
		// (get) Token: 0x0600AAC0 RID: 43712
		// (set) Token: 0x0600AABF RID: 43711
		[DispId(-2147412036)]
		public virtual extern object onmousewheel
		{
			[TypeLibFunc(20)]
			[DispId(-2147412036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600AAC1 RID: 43713
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600AAC2 RID: 43714
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600AAC3 RID: 43715
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600AAC4 RID: 43716
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700378D RID: 14221
		// (get) Token: 0x0600AAC6 RID: 43718
		// (set) Token: 0x0600AAC5 RID: 43717
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

		// Token: 0x1700378E RID: 14222
		// (get) Token: 0x0600AAC8 RID: 43720
		// (set) Token: 0x0600AAC7 RID: 43719
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

		// Token: 0x1700378F RID: 14223
		// (get) Token: 0x0600AACA RID: 43722
		// (set) Token: 0x0600AAC9 RID: 43721
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

		// Token: 0x17003790 RID: 14224
		// (get) Token: 0x0600AACB RID: 43723
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003791 RID: 14225
		// (get) Token: 0x0600AACC RID: 43724
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003792 RID: 14226
		// (get) Token: 0x0600AACD RID: 43725
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003793 RID: 14227
		// (get) Token: 0x0600AACE RID: 43726
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600AACF RID: 43727
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17003794 RID: 14228
		// (get) Token: 0x0600AAD0 RID: 43728
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003795 RID: 14229
		// (get) Token: 0x0600AAD1 RID: 43729
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600AAD2 RID: 43730
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600AAD3 RID: 43731
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600AAD4 RID: 43732
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600AAD5 RID: 43733
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600AAD6 RID: 43734
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600AAD7 RID: 43735
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600AAD8 RID: 43736
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600AAD9 RID: 43737
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17003796 RID: 14230
		// (get) Token: 0x0600AADA RID: 43738
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003797 RID: 14231
		// (get) Token: 0x0600AADC RID: 43740
		// (set) Token: 0x0600AADB RID: 43739
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

		// Token: 0x17003798 RID: 14232
		// (get) Token: 0x0600AADD RID: 43741
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003799 RID: 14233
		// (get) Token: 0x0600AADE RID: 43742
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700379A RID: 14234
		// (get) Token: 0x0600AADF RID: 43743
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700379B RID: 14235
		// (get) Token: 0x0600AAE0 RID: 43744
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700379C RID: 14236
		// (get) Token: 0x0600AAE1 RID: 43745
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700379D RID: 14237
		// (get) Token: 0x0600AAE3 RID: 43747
		// (set) Token: 0x0600AAE2 RID: 43746
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

		// Token: 0x1700379E RID: 14238
		// (get) Token: 0x0600AAE5 RID: 43749
		// (set) Token: 0x0600AAE4 RID: 43748
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

		// Token: 0x1700379F RID: 14239
		// (get) Token: 0x0600AAE7 RID: 43751
		// (set) Token: 0x0600AAE6 RID: 43750
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

		// Token: 0x170037A0 RID: 14240
		// (get) Token: 0x0600AAE9 RID: 43753
		// (set) Token: 0x0600AAE8 RID: 43752
		[DispId(-501)]
		public virtual extern object bgColor
		{
			[DispId(-501)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170037A1 RID: 14241
		// (get) Token: 0x0600AAEB RID: 43755
		// (set) Token: 0x0600AAEA RID: 43754
		[DispId(6000)]
		public virtual extern int scrollDelay
		{
			[DispId(6000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(6000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170037A2 RID: 14242
		// (get) Token: 0x0600AAED RID: 43757
		// (set) Token: 0x0600AAEC RID: 43756
		[DispId(6001)]
		public virtual extern string direction
		{
			[DispId(6001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(6001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170037A3 RID: 14243
		// (get) Token: 0x0600AAEF RID: 43759
		// (set) Token: 0x0600AAEE RID: 43758
		[DispId(6002)]
		public virtual extern string behavior
		{
			[DispId(6002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(6002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170037A4 RID: 14244
		// (get) Token: 0x0600AAF1 RID: 43761
		// (set) Token: 0x0600AAF0 RID: 43760
		[DispId(6003)]
		public virtual extern int scrollAmount
		{
			[DispId(6003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(6003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170037A5 RID: 14245
		// (get) Token: 0x0600AAF3 RID: 43763
		// (set) Token: 0x0600AAF2 RID: 43762
		[DispId(6004)]
		public virtual extern int loop
		{
			[DispId(6004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(6004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170037A6 RID: 14246
		// (get) Token: 0x0600AAF5 RID: 43765
		// (set) Token: 0x0600AAF4 RID: 43764
		[DispId(6005)]
		public virtual extern int vspace
		{
			[DispId(6005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(6005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170037A7 RID: 14247
		// (get) Token: 0x0600AAF7 RID: 43767
		// (set) Token: 0x0600AAF6 RID: 43766
		[DispId(6006)]
		public virtual extern int hspace
		{
			[DispId(6006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(6006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170037A8 RID: 14248
		// (get) Token: 0x0600AAF9 RID: 43769
		// (set) Token: 0x0600AAF8 RID: 43768
		[DispId(-2147412086)]
		public virtual extern object onfinish
		{
			[TypeLibFunc(20)]
			[DispId(-2147412086)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412086)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170037A9 RID: 14249
		// (get) Token: 0x0600AAFB RID: 43771
		// (set) Token: 0x0600AAFA RID: 43770
		[DispId(-2147412085)]
		public virtual extern object onstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412085)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170037AA RID: 14250
		// (get) Token: 0x0600AAFD RID: 43773
		// (set) Token: 0x0600AAFC RID: 43772
		[DispId(-2147412092)]
		public virtual extern object onbounce
		{
			[TypeLibFunc(20)]
			[DispId(-2147412092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170037AB RID: 14251
		// (get) Token: 0x0600AAFF RID: 43775
		// (set) Token: 0x0600AAFE RID: 43774
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

		// Token: 0x170037AC RID: 14252
		// (get) Token: 0x0600AB01 RID: 43777
		// (set) Token: 0x0600AB00 RID: 43776
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

		// Token: 0x170037AD RID: 14253
		// (get) Token: 0x0600AB03 RID: 43779
		// (set) Token: 0x0600AB02 RID: 43778
		[DispId(6007)]
		public virtual extern bool trueSpeed
		{
			[DispId(6007)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(6007)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600AB04 RID: 43780
		[DispId(6010)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void Start();

		// Token: 0x0600AB05 RID: 43781
		[DispId(6011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void stop();

		// Token: 0x0600AB06 RID: 43782
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600AB07 RID: 43783
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600AB08 RID: 43784
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170037AE RID: 14254
		// (get) Token: 0x0600AB0A RID: 43786
		// (set) Token: 0x0600AB09 RID: 43785
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

		// Token: 0x170037AF RID: 14255
		// (get) Token: 0x0600AB0C RID: 43788
		// (set) Token: 0x0600AB0B RID: 43787
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

		// Token: 0x170037B0 RID: 14256
		// (get) Token: 0x0600AB0D RID: 43789
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170037B1 RID: 14257
		// (get) Token: 0x0600AB0E RID: 43790
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170037B2 RID: 14258
		// (get) Token: 0x0600AB0F RID: 43791
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170037B3 RID: 14259
		// (get) Token: 0x0600AB11 RID: 43793
		// (set) Token: 0x0600AB10 RID: 43792
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

		// Token: 0x170037B4 RID: 14260
		// (get) Token: 0x0600AB13 RID: 43795
		// (set) Token: 0x0600AB12 RID: 43794
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

		// Token: 0x170037B5 RID: 14261
		// (get) Token: 0x0600AB15 RID: 43797
		// (set) Token: 0x0600AB14 RID: 43796
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

		// Token: 0x170037B6 RID: 14262
		// (get) Token: 0x0600AB17 RID: 43799
		// (set) Token: 0x0600AB16 RID: 43798
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

		// Token: 0x170037B7 RID: 14263
		// (get) Token: 0x0600AB19 RID: 43801
		// (set) Token: 0x0600AB18 RID: 43800
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

		// Token: 0x170037B8 RID: 14264
		// (get) Token: 0x0600AB1B RID: 43803
		// (set) Token: 0x0600AB1A RID: 43802
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

		// Token: 0x170037B9 RID: 14265
		// (get) Token: 0x0600AB1D RID: 43805
		// (set) Token: 0x0600AB1C RID: 43804
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

		// Token: 0x170037BA RID: 14266
		// (get) Token: 0x0600AB1F RID: 43807
		// (set) Token: 0x0600AB1E RID: 43806
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

		// Token: 0x170037BB RID: 14267
		// (get) Token: 0x0600AB21 RID: 43809
		// (set) Token: 0x0600AB20 RID: 43808
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

		// Token: 0x170037BC RID: 14268
		// (get) Token: 0x0600AB23 RID: 43811
		// (set) Token: 0x0600AB22 RID: 43810
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

		// Token: 0x170037BD RID: 14269
		// (get) Token: 0x0600AB25 RID: 43813
		// (set) Token: 0x0600AB24 RID: 43812
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

		// Token: 0x170037BE RID: 14270
		// (get) Token: 0x0600AB26 RID: 43814
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170037BF RID: 14271
		// (get) Token: 0x0600AB28 RID: 43816
		// (set) Token: 0x0600AB27 RID: 43815
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

		// Token: 0x170037C0 RID: 14272
		// (get) Token: 0x0600AB2A RID: 43818
		// (set) Token: 0x0600AB29 RID: 43817
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

		// Token: 0x170037C1 RID: 14273
		// (get) Token: 0x0600AB2C RID: 43820
		// (set) Token: 0x0600AB2B RID: 43819
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

		// Token: 0x0600AB2D RID: 43821
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600AB2E RID: 43822
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170037C2 RID: 14274
		// (get) Token: 0x0600AB2F RID: 43823
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170037C3 RID: 14275
		// (get) Token: 0x0600AB30 RID: 43824
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170037C4 RID: 14276
		// (get) Token: 0x0600AB32 RID: 43826
		// (set) Token: 0x0600AB31 RID: 43825
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

		// Token: 0x170037C5 RID: 14277
		// (get) Token: 0x0600AB33 RID: 43827
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170037C6 RID: 14278
		// (get) Token: 0x0600AB34 RID: 43828
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170037C7 RID: 14279
		// (get) Token: 0x0600AB35 RID: 43829
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170037C8 RID: 14280
		// (get) Token: 0x0600AB36 RID: 43830
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170037C9 RID: 14281
		// (get) Token: 0x0600AB37 RID: 43831
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170037CA RID: 14282
		// (get) Token: 0x0600AB39 RID: 43833
		// (set) Token: 0x0600AB38 RID: 43832
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

		// Token: 0x170037CB RID: 14283
		// (get) Token: 0x0600AB3B RID: 43835
		// (set) Token: 0x0600AB3A RID: 43834
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

		// Token: 0x170037CC RID: 14284
		// (get) Token: 0x0600AB3D RID: 43837
		// (set) Token: 0x0600AB3C RID: 43836
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

		// Token: 0x170037CD RID: 14285
		// (get) Token: 0x0600AB3F RID: 43839
		// (set) Token: 0x0600AB3E RID: 43838
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

		// Token: 0x0600AB40 RID: 43840
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600AB41 RID: 43841
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170037CE RID: 14286
		// (get) Token: 0x0600AB42 RID: 43842
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170037CF RID: 14287
		// (get) Token: 0x0600AB43 RID: 43843
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600AB44 RID: 43844
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x170037D0 RID: 14288
		// (get) Token: 0x0600AB45 RID: 43845
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170037D1 RID: 14289
		// (get) Token: 0x0600AB47 RID: 43847
		// (set) Token: 0x0600AB46 RID: 43846
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

		// Token: 0x0600AB48 RID: 43848
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x170037D2 RID: 14290
		// (get) Token: 0x0600AB4A RID: 43850
		// (set) Token: 0x0600AB49 RID: 43849
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

		// Token: 0x170037D3 RID: 14291
		// (get) Token: 0x0600AB4C RID: 43852
		// (set) Token: 0x0600AB4B RID: 43851
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

		// Token: 0x170037D4 RID: 14292
		// (get) Token: 0x0600AB4E RID: 43854
		// (set) Token: 0x0600AB4D RID: 43853
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

		// Token: 0x170037D5 RID: 14293
		// (get) Token: 0x0600AB50 RID: 43856
		// (set) Token: 0x0600AB4F RID: 43855
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

		// Token: 0x170037D6 RID: 14294
		// (get) Token: 0x0600AB52 RID: 43858
		// (set) Token: 0x0600AB51 RID: 43857
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

		// Token: 0x170037D7 RID: 14295
		// (get) Token: 0x0600AB54 RID: 43860
		// (set) Token: 0x0600AB53 RID: 43859
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

		// Token: 0x170037D8 RID: 14296
		// (get) Token: 0x0600AB56 RID: 43862
		// (set) Token: 0x0600AB55 RID: 43861
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

		// Token: 0x170037D9 RID: 14297
		// (get) Token: 0x0600AB58 RID: 43864
		// (set) Token: 0x0600AB57 RID: 43863
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

		// Token: 0x170037DA RID: 14298
		// (get) Token: 0x0600AB5A RID: 43866
		// (set) Token: 0x0600AB59 RID: 43865
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

		// Token: 0x170037DB RID: 14299
		// (get) Token: 0x0600AB5B RID: 43867
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170037DC RID: 14300
		// (get) Token: 0x0600AB5C RID: 43868
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170037DD RID: 14301
		// (get) Token: 0x0600AB5D RID: 43869
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600AB5E RID: 43870
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600AB5F RID: 43871
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x170037DE RID: 14302
		// (get) Token: 0x0600AB61 RID: 43873
		// (set) Token: 0x0600AB60 RID: 43872
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

		// Token: 0x0600AB62 RID: 43874
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600AB63 RID: 43875
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170037DF RID: 14303
		// (get) Token: 0x0600AB65 RID: 43877
		// (set) Token: 0x0600AB64 RID: 43876
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

		// Token: 0x170037E0 RID: 14304
		// (get) Token: 0x0600AB67 RID: 43879
		// (set) Token: 0x0600AB66 RID: 43878
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

		// Token: 0x170037E1 RID: 14305
		// (get) Token: 0x0600AB69 RID: 43881
		// (set) Token: 0x0600AB68 RID: 43880
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

		// Token: 0x170037E2 RID: 14306
		// (get) Token: 0x0600AB6B RID: 43883
		// (set) Token: 0x0600AB6A RID: 43882
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

		// Token: 0x170037E3 RID: 14307
		// (get) Token: 0x0600AB6D RID: 43885
		// (set) Token: 0x0600AB6C RID: 43884
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

		// Token: 0x170037E4 RID: 14308
		// (get) Token: 0x0600AB6F RID: 43887
		// (set) Token: 0x0600AB6E RID: 43886
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

		// Token: 0x170037E5 RID: 14309
		// (get) Token: 0x0600AB71 RID: 43889
		// (set) Token: 0x0600AB70 RID: 43888
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

		// Token: 0x170037E6 RID: 14310
		// (get) Token: 0x0600AB73 RID: 43891
		// (set) Token: 0x0600AB72 RID: 43890
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

		// Token: 0x170037E7 RID: 14311
		// (get) Token: 0x0600AB75 RID: 43893
		// (set) Token: 0x0600AB74 RID: 43892
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

		// Token: 0x170037E8 RID: 14312
		// (get) Token: 0x0600AB77 RID: 43895
		// (set) Token: 0x0600AB76 RID: 43894
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

		// Token: 0x170037E9 RID: 14313
		// (get) Token: 0x0600AB79 RID: 43897
		// (set) Token: 0x0600AB78 RID: 43896
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

		// Token: 0x170037EA RID: 14314
		// (get) Token: 0x0600AB7B RID: 43899
		// (set) Token: 0x0600AB7A RID: 43898
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

		// Token: 0x170037EB RID: 14315
		// (get) Token: 0x0600AB7D RID: 43901
		// (set) Token: 0x0600AB7C RID: 43900
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

		// Token: 0x170037EC RID: 14316
		// (get) Token: 0x0600AB7E RID: 43902
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170037ED RID: 14317
		// (get) Token: 0x0600AB80 RID: 43904
		// (set) Token: 0x0600AB7F RID: 43903
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

		// Token: 0x0600AB81 RID: 43905
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600AB82 RID: 43906
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0600AB83 RID: 43907
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600AB84 RID: 43908
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600AB85 RID: 43909
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170037EE RID: 14318
		// (get) Token: 0x0600AB87 RID: 43911
		// (set) Token: 0x0600AB86 RID: 43910
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

		// Token: 0x0600AB88 RID: 43912
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x170037EF RID: 14319
		// (get) Token: 0x0600AB8A RID: 43914
		// (set) Token: 0x0600AB89 RID: 43913
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

		// Token: 0x170037F0 RID: 14320
		// (get) Token: 0x0600AB8C RID: 43916
		// (set) Token: 0x0600AB8B RID: 43915
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

		// Token: 0x170037F1 RID: 14321
		// (get) Token: 0x0600AB8E RID: 43918
		// (set) Token: 0x0600AB8D RID: 43917
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

		// Token: 0x170037F2 RID: 14322
		// (get) Token: 0x0600AB90 RID: 43920
		// (set) Token: 0x0600AB8F RID: 43919
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

		// Token: 0x0600AB91 RID: 43921
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600AB92 RID: 43922
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600AB93 RID: 43923
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170037F3 RID: 14323
		// (get) Token: 0x0600AB94 RID: 43924
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170037F4 RID: 14324
		// (get) Token: 0x0600AB95 RID: 43925
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170037F5 RID: 14325
		// (get) Token: 0x0600AB96 RID: 43926
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170037F6 RID: 14326
		// (get) Token: 0x0600AB97 RID: 43927
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600AB98 RID: 43928
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600AB99 RID: 43929
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170037F7 RID: 14327
		// (get) Token: 0x0600AB9A RID: 43930
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170037F8 RID: 14328
		// (get) Token: 0x0600AB9C RID: 43932
		// (set) Token: 0x0600AB9B RID: 43931
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

		// Token: 0x170037F9 RID: 14329
		// (get) Token: 0x0600AB9E RID: 43934
		// (set) Token: 0x0600AB9D RID: 43933
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

		// Token: 0x170037FA RID: 14330
		// (get) Token: 0x0600ABA0 RID: 43936
		// (set) Token: 0x0600AB9F RID: 43935
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

		// Token: 0x170037FB RID: 14331
		// (get) Token: 0x0600ABA2 RID: 43938
		// (set) Token: 0x0600ABA1 RID: 43937
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

		// Token: 0x170037FC RID: 14332
		// (get) Token: 0x0600ABA4 RID: 43940
		// (set) Token: 0x0600ABA3 RID: 43939
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

		// Token: 0x0600ABA5 RID: 43941
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x170037FD RID: 14333
		// (get) Token: 0x0600ABA6 RID: 43942
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170037FE RID: 14334
		// (get) Token: 0x0600ABA7 RID: 43943
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170037FF RID: 14335
		// (get) Token: 0x0600ABA9 RID: 43945
		// (set) Token: 0x0600ABA8 RID: 43944
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

		// Token: 0x17003800 RID: 14336
		// (get) Token: 0x0600ABAB RID: 43947
		// (set) Token: 0x0600ABAA RID: 43946
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

		// Token: 0x0600ABAC RID: 43948
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600ABAD RID: 43949
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17003801 RID: 14337
		// (get) Token: 0x0600ABAF RID: 43951
		// (set) Token: 0x0600ABAE RID: 43950
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

		// Token: 0x0600ABB0 RID: 43952
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600ABB1 RID: 43953
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600ABB2 RID: 43954
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600ABB3 RID: 43955
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17003802 RID: 14338
		// (get) Token: 0x0600ABB4 RID: 43956
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600ABB5 RID: 43957
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600ABB6 RID: 43958
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17003803 RID: 14339
		// (get) Token: 0x0600ABB7 RID: 43959
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003804 RID: 14340
		// (get) Token: 0x0600ABB8 RID: 43960
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003805 RID: 14341
		// (get) Token: 0x0600ABBA RID: 43962
		// (set) Token: 0x0600ABB9 RID: 43961
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

		// Token: 0x17003806 RID: 14342
		// (get) Token: 0x0600ABBC RID: 43964
		// (set) Token: 0x0600ABBB RID: 43963
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

		// Token: 0x17003807 RID: 14343
		// (get) Token: 0x0600ABBD RID: 43965
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600ABBE RID: 43966
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600ABBF RID: 43967
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17003808 RID: 14344
		// (get) Token: 0x0600ABC0 RID: 43968
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003809 RID: 14345
		// (get) Token: 0x0600ABC1 RID: 43969
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700380A RID: 14346
		// (get) Token: 0x0600ABC3 RID: 43971
		// (set) Token: 0x0600ABC2 RID: 43970
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

		// Token: 0x1700380B RID: 14347
		// (get) Token: 0x0600ABC5 RID: 43973
		// (set) Token: 0x0600ABC4 RID: 43972
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

		// Token: 0x1700380C RID: 14348
		// (get) Token: 0x0600ABC7 RID: 43975
		// (set) Token: 0x0600ABC6 RID: 43974
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

		// Token: 0x1700380D RID: 14349
		// (get) Token: 0x0600ABC9 RID: 43977
		// (set) Token: 0x0600ABC8 RID: 43976
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

		// Token: 0x0600ABCA RID: 43978
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x1700380E RID: 14350
		// (get) Token: 0x0600ABCC RID: 43980
		// (set) Token: 0x0600ABCB RID: 43979
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

		// Token: 0x1700380F RID: 14351
		// (get) Token: 0x0600ABCD RID: 43981
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003810 RID: 14352
		// (get) Token: 0x0600ABCF RID: 43983
		// (set) Token: 0x0600ABCE RID: 43982
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

		// Token: 0x17003811 RID: 14353
		// (get) Token: 0x0600ABD1 RID: 43985
		// (set) Token: 0x0600ABD0 RID: 43984
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

		// Token: 0x17003812 RID: 14354
		// (get) Token: 0x0600ABD2 RID: 43986
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003813 RID: 14355
		// (get) Token: 0x0600ABD4 RID: 43988
		// (set) Token: 0x0600ABD3 RID: 43987
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

		// Token: 0x17003814 RID: 14356
		// (get) Token: 0x0600ABD6 RID: 43990
		// (set) Token: 0x0600ABD5 RID: 43989
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

		// Token: 0x0600ABD7 RID: 43991
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17003815 RID: 14357
		// (get) Token: 0x0600ABD9 RID: 43993
		// (set) Token: 0x0600ABD8 RID: 43992
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

		// Token: 0x17003816 RID: 14358
		// (get) Token: 0x0600ABDB RID: 43995
		// (set) Token: 0x0600ABDA RID: 43994
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

		// Token: 0x17003817 RID: 14359
		// (get) Token: 0x0600ABDD RID: 43997
		// (set) Token: 0x0600ABDC RID: 43996
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

		// Token: 0x17003818 RID: 14360
		// (get) Token: 0x0600ABDF RID: 43999
		// (set) Token: 0x0600ABDE RID: 43998
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

		// Token: 0x17003819 RID: 14361
		// (get) Token: 0x0600ABE1 RID: 44001
		// (set) Token: 0x0600ABE0 RID: 44000
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

		// Token: 0x1700381A RID: 14362
		// (get) Token: 0x0600ABE3 RID: 44003
		// (set) Token: 0x0600ABE2 RID: 44002
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

		// Token: 0x1700381B RID: 14363
		// (get) Token: 0x0600ABE5 RID: 44005
		// (set) Token: 0x0600ABE4 RID: 44004
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

		// Token: 0x1700381C RID: 14364
		// (get) Token: 0x0600ABE7 RID: 44007
		// (set) Token: 0x0600ABE6 RID: 44006
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

		// Token: 0x0600ABE8 RID: 44008
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x1700381D RID: 14365
		// (get) Token: 0x0600ABE9 RID: 44009
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700381E RID: 14366
		// (get) Token: 0x0600ABEB RID: 44011
		// (set) Token: 0x0600ABEA RID: 44010
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

		// Token: 0x0600ABEC RID: 44012
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x0600ABED RID: 44013
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600ABEE RID: 44014
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600ABEF RID: 44015
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700381F RID: 14367
		// (get) Token: 0x0600ABF1 RID: 44017
		// (set) Token: 0x0600ABF0 RID: 44016
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

		// Token: 0x17003820 RID: 14368
		// (get) Token: 0x0600ABF3 RID: 44019
		// (set) Token: 0x0600ABF2 RID: 44018
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

		// Token: 0x17003821 RID: 14369
		// (get) Token: 0x0600ABF5 RID: 44021
		// (set) Token: 0x0600ABF4 RID: 44020
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

		// Token: 0x17003822 RID: 14370
		// (get) Token: 0x0600ABF6 RID: 44022
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003823 RID: 14371
		// (get) Token: 0x0600ABF7 RID: 44023
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003824 RID: 14372
		// (get) Token: 0x0600ABF8 RID: 44024
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003825 RID: 14373
		// (get) Token: 0x0600ABF9 RID: 44025
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600ABFA RID: 44026
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17003826 RID: 14374
		// (get) Token: 0x0600ABFB RID: 44027
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003827 RID: 14375
		// (get) Token: 0x0600ABFC RID: 44028
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600ABFD RID: 44029
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600ABFE RID: 44030
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600ABFF RID: 44031
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600AC00 RID: 44032
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600AC01 RID: 44033
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600AC02 RID: 44034
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600AC03 RID: 44035
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600AC04 RID: 44036
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17003828 RID: 14376
		// (get) Token: 0x0600AC05 RID: 44037
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003829 RID: 14377
		// (get) Token: 0x0600AC07 RID: 44039
		// (set) Token: 0x0600AC06 RID: 44038
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

		// Token: 0x1700382A RID: 14378
		// (get) Token: 0x0600AC08 RID: 44040
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700382B RID: 14379
		// (get) Token: 0x0600AC09 RID: 44041
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700382C RID: 14380
		// (get) Token: 0x0600AC0A RID: 44042
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700382D RID: 14381
		// (get) Token: 0x0600AC0B RID: 44043
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700382E RID: 14382
		// (get) Token: 0x0600AC0C RID: 44044
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700382F RID: 14383
		// (get) Token: 0x0600AC0E RID: 44046
		// (set) Token: 0x0600AC0D RID: 44045
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

		// Token: 0x17003830 RID: 14384
		// (get) Token: 0x0600AC10 RID: 44048
		// (set) Token: 0x0600AC0F RID: 44047
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

		// Token: 0x17003831 RID: 14385
		// (get) Token: 0x0600AC12 RID: 44050
		// (set) Token: 0x0600AC11 RID: 44049
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

		// Token: 0x17003832 RID: 14386
		// (get) Token: 0x0600AC14 RID: 44052
		// (set) Token: 0x0600AC13 RID: 44051
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

		// Token: 0x0600AC15 RID: 44053
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17003833 RID: 14387
		// (get) Token: 0x0600AC17 RID: 44055
		// (set) Token: 0x0600AC16 RID: 44054
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

		// Token: 0x17003834 RID: 14388
		// (get) Token: 0x0600AC19 RID: 44057
		// (set) Token: 0x0600AC18 RID: 44056
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

		// Token: 0x17003835 RID: 14389
		// (get) Token: 0x0600AC1B RID: 44059
		// (set) Token: 0x0600AC1A RID: 44058
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

		// Token: 0x17003836 RID: 14390
		// (get) Token: 0x0600AC1D RID: 44061
		// (set) Token: 0x0600AC1C RID: 44060
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

		// Token: 0x0600AC1E RID: 44062
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x0600AC1F RID: 44063
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600AC20 RID: 44064
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17003837 RID: 14391
		// (get) Token: 0x0600AC21 RID: 44065
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003838 RID: 14392
		// (get) Token: 0x0600AC22 RID: 44066
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003839 RID: 14393
		// (get) Token: 0x0600AC23 RID: 44067
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700383A RID: 14394
		// (get) Token: 0x0600AC24 RID: 44068
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600AC25 RID: 44069
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTextContainer_createControlRange();

		// Token: 0x1700383B RID: 14395
		// (get) Token: 0x0600AC26 RID: 44070
		public virtual extern int IHTMLTextContainer_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700383C RID: 14396
		// (get) Token: 0x0600AC27 RID: 44071
		public virtual extern int IHTMLTextContainer_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700383D RID: 14397
		// (get) Token: 0x0600AC29 RID: 44073
		// (set) Token: 0x0600AC28 RID: 44072
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

		// Token: 0x1700383E RID: 14398
		// (get) Token: 0x0600AC2B RID: 44075
		// (set) Token: 0x0600AC2A RID: 44074
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

		// Token: 0x1700383F RID: 14399
		// (get) Token: 0x0600AC2D RID: 44077
		// (set) Token: 0x0600AC2C RID: 44076
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

		// Token: 0x17003840 RID: 14400
		// (get) Token: 0x0600AC2F RID: 44079
		// (set) Token: 0x0600AC2E RID: 44078
		public virtual extern object IHTMLMarqueeElement_bgColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003841 RID: 14401
		// (get) Token: 0x0600AC31 RID: 44081
		// (set) Token: 0x0600AC30 RID: 44080
		public virtual extern int IHTMLMarqueeElement_scrollDelay
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003842 RID: 14402
		// (get) Token: 0x0600AC33 RID: 44083
		// (set) Token: 0x0600AC32 RID: 44082
		public virtual extern string IHTMLMarqueeElement_direction
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003843 RID: 14403
		// (get) Token: 0x0600AC35 RID: 44085
		// (set) Token: 0x0600AC34 RID: 44084
		public virtual extern string IHTMLMarqueeElement_behavior
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003844 RID: 14404
		// (get) Token: 0x0600AC37 RID: 44087
		// (set) Token: 0x0600AC36 RID: 44086
		public virtual extern int IHTMLMarqueeElement_scrollAmount
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003845 RID: 14405
		// (get) Token: 0x0600AC39 RID: 44089
		// (set) Token: 0x0600AC38 RID: 44088
		public virtual extern int IHTMLMarqueeElement_loop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003846 RID: 14406
		// (get) Token: 0x0600AC3B RID: 44091
		// (set) Token: 0x0600AC3A RID: 44090
		public virtual extern int IHTMLMarqueeElement_vspace
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003847 RID: 14407
		// (get) Token: 0x0600AC3D RID: 44093
		// (set) Token: 0x0600AC3C RID: 44092
		public virtual extern int IHTMLMarqueeElement_hspace
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003848 RID: 14408
		// (get) Token: 0x0600AC3F RID: 44095
		// (set) Token: 0x0600AC3E RID: 44094
		public virtual extern object IHTMLMarqueeElement_onfinish
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003849 RID: 14409
		// (get) Token: 0x0600AC41 RID: 44097
		// (set) Token: 0x0600AC40 RID: 44096
		public virtual extern object IHTMLMarqueeElement_onstart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700384A RID: 14410
		// (get) Token: 0x0600AC43 RID: 44099
		// (set) Token: 0x0600AC42 RID: 44098
		public virtual extern object IHTMLMarqueeElement_onbounce
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700384B RID: 14411
		// (get) Token: 0x0600AC45 RID: 44101
		// (set) Token: 0x0600AC44 RID: 44100
		public virtual extern object IHTMLMarqueeElement_width
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700384C RID: 14412
		// (get) Token: 0x0600AC47 RID: 44103
		// (set) Token: 0x0600AC46 RID: 44102
		public virtual extern object IHTMLMarqueeElement_height
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700384D RID: 14413
		// (get) Token: 0x0600AC49 RID: 44105
		// (set) Token: 0x0600AC48 RID: 44104
		public virtual extern bool IHTMLMarqueeElement_trueSpeed
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x0600AC4A RID: 44106
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLMarqueeElement_Start();

		// Token: 0x0600AC4B RID: 44107
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLMarqueeElement_stop();

		// Token: 0x140014A8 RID: 5288
		// (add) Token: 0x0600AC4C RID: 44108
		// (remove) Token: 0x0600AC4D RID: 44109
		public virtual extern event HTMLMarqueeElementEvents_onhelpEventHandler HTMLMarqueeElementEvents_Event_onhelp;

		// Token: 0x140014A9 RID: 5289
		// (add) Token: 0x0600AC4E RID: 44110
		// (remove) Token: 0x0600AC4F RID: 44111
		public virtual extern event HTMLMarqueeElementEvents_onclickEventHandler HTMLMarqueeElementEvents_Event_onclick;

		// Token: 0x140014AA RID: 5290
		// (add) Token: 0x0600AC50 RID: 44112
		// (remove) Token: 0x0600AC51 RID: 44113
		public virtual extern event HTMLMarqueeElementEvents_ondblclickEventHandler HTMLMarqueeElementEvents_Event_ondblclick;

		// Token: 0x140014AB RID: 5291
		// (add) Token: 0x0600AC52 RID: 44114
		// (remove) Token: 0x0600AC53 RID: 44115
		public virtual extern event HTMLMarqueeElementEvents_onkeypressEventHandler HTMLMarqueeElementEvents_Event_onkeypress;

		// Token: 0x140014AC RID: 5292
		// (add) Token: 0x0600AC54 RID: 44116
		// (remove) Token: 0x0600AC55 RID: 44117
		public virtual extern event HTMLMarqueeElementEvents_onkeydownEventHandler HTMLMarqueeElementEvents_Event_onkeydown;

		// Token: 0x140014AD RID: 5293
		// (add) Token: 0x0600AC56 RID: 44118
		// (remove) Token: 0x0600AC57 RID: 44119
		public virtual extern event HTMLMarqueeElementEvents_onkeyupEventHandler HTMLMarqueeElementEvents_Event_onkeyup;

		// Token: 0x140014AE RID: 5294
		// (add) Token: 0x0600AC58 RID: 44120
		// (remove) Token: 0x0600AC59 RID: 44121
		public virtual extern event HTMLMarqueeElementEvents_onmouseoutEventHandler HTMLMarqueeElementEvents_Event_onmouseout;

		// Token: 0x140014AF RID: 5295
		// (add) Token: 0x0600AC5A RID: 44122
		// (remove) Token: 0x0600AC5B RID: 44123
		public virtual extern event HTMLMarqueeElementEvents_onmouseoverEventHandler HTMLMarqueeElementEvents_Event_onmouseover;

		// Token: 0x140014B0 RID: 5296
		// (add) Token: 0x0600AC5C RID: 44124
		// (remove) Token: 0x0600AC5D RID: 44125
		public virtual extern event HTMLMarqueeElementEvents_onmousemoveEventHandler HTMLMarqueeElementEvents_Event_onmousemove;

		// Token: 0x140014B1 RID: 5297
		// (add) Token: 0x0600AC5E RID: 44126
		// (remove) Token: 0x0600AC5F RID: 44127
		public virtual extern event HTMLMarqueeElementEvents_onmousedownEventHandler HTMLMarqueeElementEvents_Event_onmousedown;

		// Token: 0x140014B2 RID: 5298
		// (add) Token: 0x0600AC60 RID: 44128
		// (remove) Token: 0x0600AC61 RID: 44129
		public virtual extern event HTMLMarqueeElementEvents_onmouseupEventHandler HTMLMarqueeElementEvents_Event_onmouseup;

		// Token: 0x140014B3 RID: 5299
		// (add) Token: 0x0600AC62 RID: 44130
		// (remove) Token: 0x0600AC63 RID: 44131
		public virtual extern event HTMLMarqueeElementEvents_onselectstartEventHandler HTMLMarqueeElementEvents_Event_onselectstart;

		// Token: 0x140014B4 RID: 5300
		// (add) Token: 0x0600AC64 RID: 44132
		// (remove) Token: 0x0600AC65 RID: 44133
		public virtual extern event HTMLMarqueeElementEvents_onfilterchangeEventHandler HTMLMarqueeElementEvents_Event_onfilterchange;

		// Token: 0x140014B5 RID: 5301
		// (add) Token: 0x0600AC66 RID: 44134
		// (remove) Token: 0x0600AC67 RID: 44135
		public virtual extern event HTMLMarqueeElementEvents_ondragstartEventHandler HTMLMarqueeElementEvents_Event_ondragstart;

		// Token: 0x140014B6 RID: 5302
		// (add) Token: 0x0600AC68 RID: 44136
		// (remove) Token: 0x0600AC69 RID: 44137
		public virtual extern event HTMLMarqueeElementEvents_onbeforeupdateEventHandler HTMLMarqueeElementEvents_Event_onbeforeupdate;

		// Token: 0x140014B7 RID: 5303
		// (add) Token: 0x0600AC6A RID: 44138
		// (remove) Token: 0x0600AC6B RID: 44139
		public virtual extern event HTMLMarqueeElementEvents_onafterupdateEventHandler HTMLMarqueeElementEvents_Event_onafterupdate;

		// Token: 0x140014B8 RID: 5304
		// (add) Token: 0x0600AC6C RID: 44140
		// (remove) Token: 0x0600AC6D RID: 44141
		public virtual extern event HTMLMarqueeElementEvents_onerrorupdateEventHandler HTMLMarqueeElementEvents_Event_onerrorupdate;

		// Token: 0x140014B9 RID: 5305
		// (add) Token: 0x0600AC6E RID: 44142
		// (remove) Token: 0x0600AC6F RID: 44143
		public virtual extern event HTMLMarqueeElementEvents_onrowexitEventHandler HTMLMarqueeElementEvents_Event_onrowexit;

		// Token: 0x140014BA RID: 5306
		// (add) Token: 0x0600AC70 RID: 44144
		// (remove) Token: 0x0600AC71 RID: 44145
		public virtual extern event HTMLMarqueeElementEvents_onrowenterEventHandler HTMLMarqueeElementEvents_Event_onrowenter;

		// Token: 0x140014BB RID: 5307
		// (add) Token: 0x0600AC72 RID: 44146
		// (remove) Token: 0x0600AC73 RID: 44147
		public virtual extern event HTMLMarqueeElementEvents_ondatasetchangedEventHandler HTMLMarqueeElementEvents_Event_ondatasetchanged;

		// Token: 0x140014BC RID: 5308
		// (add) Token: 0x0600AC74 RID: 44148
		// (remove) Token: 0x0600AC75 RID: 44149
		public virtual extern event HTMLMarqueeElementEvents_ondataavailableEventHandler HTMLMarqueeElementEvents_Event_ondataavailable;

		// Token: 0x140014BD RID: 5309
		// (add) Token: 0x0600AC76 RID: 44150
		// (remove) Token: 0x0600AC77 RID: 44151
		public virtual extern event HTMLMarqueeElementEvents_ondatasetcompleteEventHandler HTMLMarqueeElementEvents_Event_ondatasetcomplete;

		// Token: 0x140014BE RID: 5310
		// (add) Token: 0x0600AC78 RID: 44152
		// (remove) Token: 0x0600AC79 RID: 44153
		public virtual extern event HTMLMarqueeElementEvents_onlosecaptureEventHandler HTMLMarqueeElementEvents_Event_onlosecapture;

		// Token: 0x140014BF RID: 5311
		// (add) Token: 0x0600AC7A RID: 44154
		// (remove) Token: 0x0600AC7B RID: 44155
		public virtual extern event HTMLMarqueeElementEvents_onpropertychangeEventHandler HTMLMarqueeElementEvents_Event_onpropertychange;

		// Token: 0x140014C0 RID: 5312
		// (add) Token: 0x0600AC7C RID: 44156
		// (remove) Token: 0x0600AC7D RID: 44157
		public virtual extern event HTMLMarqueeElementEvents_onscrollEventHandler HTMLMarqueeElementEvents_Event_onscroll;

		// Token: 0x140014C1 RID: 5313
		// (add) Token: 0x0600AC7E RID: 44158
		// (remove) Token: 0x0600AC7F RID: 44159
		public virtual extern event HTMLMarqueeElementEvents_onfocusEventHandler HTMLMarqueeElementEvents_Event_onfocus;

		// Token: 0x140014C2 RID: 5314
		// (add) Token: 0x0600AC80 RID: 44160
		// (remove) Token: 0x0600AC81 RID: 44161
		public virtual extern event HTMLMarqueeElementEvents_onblurEventHandler HTMLMarqueeElementEvents_Event_onblur;

		// Token: 0x140014C3 RID: 5315
		// (add) Token: 0x0600AC82 RID: 44162
		// (remove) Token: 0x0600AC83 RID: 44163
		public virtual extern event HTMLMarqueeElementEvents_onresizeEventHandler HTMLMarqueeElementEvents_Event_onresize;

		// Token: 0x140014C4 RID: 5316
		// (add) Token: 0x0600AC84 RID: 44164
		// (remove) Token: 0x0600AC85 RID: 44165
		public virtual extern event HTMLMarqueeElementEvents_ondragEventHandler HTMLMarqueeElementEvents_Event_ondrag;

		// Token: 0x140014C5 RID: 5317
		// (add) Token: 0x0600AC86 RID: 44166
		// (remove) Token: 0x0600AC87 RID: 44167
		public virtual extern event HTMLMarqueeElementEvents_ondragendEventHandler HTMLMarqueeElementEvents_Event_ondragend;

		// Token: 0x140014C6 RID: 5318
		// (add) Token: 0x0600AC88 RID: 44168
		// (remove) Token: 0x0600AC89 RID: 44169
		public virtual extern event HTMLMarqueeElementEvents_ondragenterEventHandler HTMLMarqueeElementEvents_Event_ondragenter;

		// Token: 0x140014C7 RID: 5319
		// (add) Token: 0x0600AC8A RID: 44170
		// (remove) Token: 0x0600AC8B RID: 44171
		public virtual extern event HTMLMarqueeElementEvents_ondragoverEventHandler HTMLMarqueeElementEvents_Event_ondragover;

		// Token: 0x140014C8 RID: 5320
		// (add) Token: 0x0600AC8C RID: 44172
		// (remove) Token: 0x0600AC8D RID: 44173
		public virtual extern event HTMLMarqueeElementEvents_ondragleaveEventHandler HTMLMarqueeElementEvents_Event_ondragleave;

		// Token: 0x140014C9 RID: 5321
		// (add) Token: 0x0600AC8E RID: 44174
		// (remove) Token: 0x0600AC8F RID: 44175
		public virtual extern event HTMLMarqueeElementEvents_ondropEventHandler HTMLMarqueeElementEvents_Event_ondrop;

		// Token: 0x140014CA RID: 5322
		// (add) Token: 0x0600AC90 RID: 44176
		// (remove) Token: 0x0600AC91 RID: 44177
		public virtual extern event HTMLMarqueeElementEvents_onbeforecutEventHandler HTMLMarqueeElementEvents_Event_onbeforecut;

		// Token: 0x140014CB RID: 5323
		// (add) Token: 0x0600AC92 RID: 44178
		// (remove) Token: 0x0600AC93 RID: 44179
		public virtual extern event HTMLMarqueeElementEvents_oncutEventHandler HTMLMarqueeElementEvents_Event_oncut;

		// Token: 0x140014CC RID: 5324
		// (add) Token: 0x0600AC94 RID: 44180
		// (remove) Token: 0x0600AC95 RID: 44181
		public virtual extern event HTMLMarqueeElementEvents_onbeforecopyEventHandler HTMLMarqueeElementEvents_Event_onbeforecopy;

		// Token: 0x140014CD RID: 5325
		// (add) Token: 0x0600AC96 RID: 44182
		// (remove) Token: 0x0600AC97 RID: 44183
		public virtual extern event HTMLMarqueeElementEvents_oncopyEventHandler HTMLMarqueeElementEvents_Event_oncopy;

		// Token: 0x140014CE RID: 5326
		// (add) Token: 0x0600AC98 RID: 44184
		// (remove) Token: 0x0600AC99 RID: 44185
		public virtual extern event HTMLMarqueeElementEvents_onbeforepasteEventHandler HTMLMarqueeElementEvents_Event_onbeforepaste;

		// Token: 0x140014CF RID: 5327
		// (add) Token: 0x0600AC9A RID: 44186
		// (remove) Token: 0x0600AC9B RID: 44187
		public virtual extern event HTMLMarqueeElementEvents_onpasteEventHandler HTMLMarqueeElementEvents_Event_onpaste;

		// Token: 0x140014D0 RID: 5328
		// (add) Token: 0x0600AC9C RID: 44188
		// (remove) Token: 0x0600AC9D RID: 44189
		public virtual extern event HTMLMarqueeElementEvents_oncontextmenuEventHandler HTMLMarqueeElementEvents_Event_oncontextmenu;

		// Token: 0x140014D1 RID: 5329
		// (add) Token: 0x0600AC9E RID: 44190
		// (remove) Token: 0x0600AC9F RID: 44191
		public virtual extern event HTMLMarqueeElementEvents_onrowsdeleteEventHandler HTMLMarqueeElementEvents_Event_onrowsdelete;

		// Token: 0x140014D2 RID: 5330
		// (add) Token: 0x0600ACA0 RID: 44192
		// (remove) Token: 0x0600ACA1 RID: 44193
		public virtual extern event HTMLMarqueeElementEvents_onrowsinsertedEventHandler HTMLMarqueeElementEvents_Event_onrowsinserted;

		// Token: 0x140014D3 RID: 5331
		// (add) Token: 0x0600ACA2 RID: 44194
		// (remove) Token: 0x0600ACA3 RID: 44195
		public virtual extern event HTMLMarqueeElementEvents_oncellchangeEventHandler HTMLMarqueeElementEvents_Event_oncellchange;

		// Token: 0x140014D4 RID: 5332
		// (add) Token: 0x0600ACA4 RID: 44196
		// (remove) Token: 0x0600ACA5 RID: 44197
		public virtual extern event HTMLMarqueeElementEvents_onreadystatechangeEventHandler HTMLMarqueeElementEvents_Event_onreadystatechange;

		// Token: 0x140014D5 RID: 5333
		// (add) Token: 0x0600ACA6 RID: 44198
		// (remove) Token: 0x0600ACA7 RID: 44199
		public virtual extern event HTMLMarqueeElementEvents_onbeforeeditfocusEventHandler HTMLMarqueeElementEvents_Event_onbeforeeditfocus;

		// Token: 0x140014D6 RID: 5334
		// (add) Token: 0x0600ACA8 RID: 44200
		// (remove) Token: 0x0600ACA9 RID: 44201
		public virtual extern event HTMLMarqueeElementEvents_onlayoutcompleteEventHandler HTMLMarqueeElementEvents_Event_onlayoutcomplete;

		// Token: 0x140014D7 RID: 5335
		// (add) Token: 0x0600ACAA RID: 44202
		// (remove) Token: 0x0600ACAB RID: 44203
		public virtual extern event HTMLMarqueeElementEvents_onpageEventHandler HTMLMarqueeElementEvents_Event_onpage;

		// Token: 0x140014D8 RID: 5336
		// (add) Token: 0x0600ACAC RID: 44204
		// (remove) Token: 0x0600ACAD RID: 44205
		public virtual extern event HTMLMarqueeElementEvents_onbeforedeactivateEventHandler HTMLMarqueeElementEvents_Event_onbeforedeactivate;

		// Token: 0x140014D9 RID: 5337
		// (add) Token: 0x0600ACAE RID: 44206
		// (remove) Token: 0x0600ACAF RID: 44207
		public virtual extern event HTMLMarqueeElementEvents_onbeforeactivateEventHandler HTMLMarqueeElementEvents_Event_onbeforeactivate;

		// Token: 0x140014DA RID: 5338
		// (add) Token: 0x0600ACB0 RID: 44208
		// (remove) Token: 0x0600ACB1 RID: 44209
		public virtual extern event HTMLMarqueeElementEvents_onmoveEventHandler HTMLMarqueeElementEvents_Event_onmove;

		// Token: 0x140014DB RID: 5339
		// (add) Token: 0x0600ACB2 RID: 44210
		// (remove) Token: 0x0600ACB3 RID: 44211
		public virtual extern event HTMLMarqueeElementEvents_oncontrolselectEventHandler HTMLMarqueeElementEvents_Event_oncontrolselect;

		// Token: 0x140014DC RID: 5340
		// (add) Token: 0x0600ACB4 RID: 44212
		// (remove) Token: 0x0600ACB5 RID: 44213
		public virtual extern event HTMLMarqueeElementEvents_onmovestartEventHandler HTMLMarqueeElementEvents_Event_onmovestart;

		// Token: 0x140014DD RID: 5341
		// (add) Token: 0x0600ACB6 RID: 44214
		// (remove) Token: 0x0600ACB7 RID: 44215
		public virtual extern event HTMLMarqueeElementEvents_onmoveendEventHandler HTMLMarqueeElementEvents_Event_onmoveend;

		// Token: 0x140014DE RID: 5342
		// (add) Token: 0x0600ACB8 RID: 44216
		// (remove) Token: 0x0600ACB9 RID: 44217
		public virtual extern event HTMLMarqueeElementEvents_onresizestartEventHandler HTMLMarqueeElementEvents_Event_onresizestart;

		// Token: 0x140014DF RID: 5343
		// (add) Token: 0x0600ACBA RID: 44218
		// (remove) Token: 0x0600ACBB RID: 44219
		public virtual extern event HTMLMarqueeElementEvents_onresizeendEventHandler HTMLMarqueeElementEvents_Event_onresizeend;

		// Token: 0x140014E0 RID: 5344
		// (add) Token: 0x0600ACBC RID: 44220
		// (remove) Token: 0x0600ACBD RID: 44221
		public virtual extern event HTMLMarqueeElementEvents_onmouseenterEventHandler HTMLMarqueeElementEvents_Event_onmouseenter;

		// Token: 0x140014E1 RID: 5345
		// (add) Token: 0x0600ACBE RID: 44222
		// (remove) Token: 0x0600ACBF RID: 44223
		public virtual extern event HTMLMarqueeElementEvents_onmouseleaveEventHandler HTMLMarqueeElementEvents_Event_onmouseleave;

		// Token: 0x140014E2 RID: 5346
		// (add) Token: 0x0600ACC0 RID: 44224
		// (remove) Token: 0x0600ACC1 RID: 44225
		public virtual extern event HTMLMarqueeElementEvents_onmousewheelEventHandler HTMLMarqueeElementEvents_Event_onmousewheel;

		// Token: 0x140014E3 RID: 5347
		// (add) Token: 0x0600ACC2 RID: 44226
		// (remove) Token: 0x0600ACC3 RID: 44227
		public virtual extern event HTMLMarqueeElementEvents_onactivateEventHandler HTMLMarqueeElementEvents_Event_onactivate;

		// Token: 0x140014E4 RID: 5348
		// (add) Token: 0x0600ACC4 RID: 44228
		// (remove) Token: 0x0600ACC5 RID: 44229
		public virtual extern event HTMLMarqueeElementEvents_ondeactivateEventHandler HTMLMarqueeElementEvents_Event_ondeactivate;

		// Token: 0x140014E5 RID: 5349
		// (add) Token: 0x0600ACC6 RID: 44230
		// (remove) Token: 0x0600ACC7 RID: 44231
		public virtual extern event HTMLMarqueeElementEvents_onfocusinEventHandler HTMLMarqueeElementEvents_Event_onfocusin;

		// Token: 0x140014E6 RID: 5350
		// (add) Token: 0x0600ACC8 RID: 44232
		// (remove) Token: 0x0600ACC9 RID: 44233
		public virtual extern event HTMLMarqueeElementEvents_onfocusoutEventHandler HTMLMarqueeElementEvents_Event_onfocusout;

		// Token: 0x140014E7 RID: 5351
		// (add) Token: 0x0600ACCA RID: 44234
		// (remove) Token: 0x0600ACCB RID: 44235
		public virtual extern event HTMLMarqueeElementEvents_onchangeEventHandler onchange;

		// Token: 0x140014E8 RID: 5352
		// (add) Token: 0x0600ACCC RID: 44236
		// (remove) Token: 0x0600ACCD RID: 44237
		public virtual extern event HTMLMarqueeElementEvents_onselectEventHandler onselect;

		// Token: 0x140014E9 RID: 5353
		// (add) Token: 0x0600ACCE RID: 44238
		// (remove) Token: 0x0600ACCF RID: 44239
		public virtual extern event HTMLMarqueeElementEvents_onbounceEventHandler HTMLMarqueeElementEvents_Event_onbounce;

		// Token: 0x140014EA RID: 5354
		// (add) Token: 0x0600ACD0 RID: 44240
		// (remove) Token: 0x0600ACD1 RID: 44241
		public virtual extern event HTMLMarqueeElementEvents_onfinishEventHandler HTMLMarqueeElementEvents_Event_onfinish;

		// Token: 0x140014EB RID: 5355
		// (add) Token: 0x0600ACD2 RID: 44242
		// (remove) Token: 0x0600ACD3 RID: 44243
		public virtual extern event HTMLMarqueeElementEvents_onstartEventHandler HTMLMarqueeElementEvents_Event_onstart;

		// Token: 0x140014EC RID: 5356
		// (add) Token: 0x0600ACD4 RID: 44244
		// (remove) Token: 0x0600ACD5 RID: 44245
		public virtual extern event HTMLMarqueeElementEvents2_onhelpEventHandler HTMLMarqueeElementEvents2_Event_onhelp;

		// Token: 0x140014ED RID: 5357
		// (add) Token: 0x0600ACD6 RID: 44246
		// (remove) Token: 0x0600ACD7 RID: 44247
		public virtual extern event HTMLMarqueeElementEvents2_onclickEventHandler HTMLMarqueeElementEvents2_Event_onclick;

		// Token: 0x140014EE RID: 5358
		// (add) Token: 0x0600ACD8 RID: 44248
		// (remove) Token: 0x0600ACD9 RID: 44249
		public virtual extern event HTMLMarqueeElementEvents2_ondblclickEventHandler HTMLMarqueeElementEvents2_Event_ondblclick;

		// Token: 0x140014EF RID: 5359
		// (add) Token: 0x0600ACDA RID: 44250
		// (remove) Token: 0x0600ACDB RID: 44251
		public virtual extern event HTMLMarqueeElementEvents2_onkeypressEventHandler HTMLMarqueeElementEvents2_Event_onkeypress;

		// Token: 0x140014F0 RID: 5360
		// (add) Token: 0x0600ACDC RID: 44252
		// (remove) Token: 0x0600ACDD RID: 44253
		public virtual extern event HTMLMarqueeElementEvents2_onkeydownEventHandler HTMLMarqueeElementEvents2_Event_onkeydown;

		// Token: 0x140014F1 RID: 5361
		// (add) Token: 0x0600ACDE RID: 44254
		// (remove) Token: 0x0600ACDF RID: 44255
		public virtual extern event HTMLMarqueeElementEvents2_onkeyupEventHandler HTMLMarqueeElementEvents2_Event_onkeyup;

		// Token: 0x140014F2 RID: 5362
		// (add) Token: 0x0600ACE0 RID: 44256
		// (remove) Token: 0x0600ACE1 RID: 44257
		public virtual extern event HTMLMarqueeElementEvents2_onmouseoutEventHandler HTMLMarqueeElementEvents2_Event_onmouseout;

		// Token: 0x140014F3 RID: 5363
		// (add) Token: 0x0600ACE2 RID: 44258
		// (remove) Token: 0x0600ACE3 RID: 44259
		public virtual extern event HTMLMarqueeElementEvents2_onmouseoverEventHandler HTMLMarqueeElementEvents2_Event_onmouseover;

		// Token: 0x140014F4 RID: 5364
		// (add) Token: 0x0600ACE4 RID: 44260
		// (remove) Token: 0x0600ACE5 RID: 44261
		public virtual extern event HTMLMarqueeElementEvents2_onmousemoveEventHandler HTMLMarqueeElementEvents2_Event_onmousemove;

		// Token: 0x140014F5 RID: 5365
		// (add) Token: 0x0600ACE6 RID: 44262
		// (remove) Token: 0x0600ACE7 RID: 44263
		public virtual extern event HTMLMarqueeElementEvents2_onmousedownEventHandler HTMLMarqueeElementEvents2_Event_onmousedown;

		// Token: 0x140014F6 RID: 5366
		// (add) Token: 0x0600ACE8 RID: 44264
		// (remove) Token: 0x0600ACE9 RID: 44265
		public virtual extern event HTMLMarqueeElementEvents2_onmouseupEventHandler HTMLMarqueeElementEvents2_Event_onmouseup;

		// Token: 0x140014F7 RID: 5367
		// (add) Token: 0x0600ACEA RID: 44266
		// (remove) Token: 0x0600ACEB RID: 44267
		public virtual extern event HTMLMarqueeElementEvents2_onselectstartEventHandler HTMLMarqueeElementEvents2_Event_onselectstart;

		// Token: 0x140014F8 RID: 5368
		// (add) Token: 0x0600ACEC RID: 44268
		// (remove) Token: 0x0600ACED RID: 44269
		public virtual extern event HTMLMarqueeElementEvents2_onfilterchangeEventHandler HTMLMarqueeElementEvents2_Event_onfilterchange;

		// Token: 0x140014F9 RID: 5369
		// (add) Token: 0x0600ACEE RID: 44270
		// (remove) Token: 0x0600ACEF RID: 44271
		public virtual extern event HTMLMarqueeElementEvents2_ondragstartEventHandler HTMLMarqueeElementEvents2_Event_ondragstart;

		// Token: 0x140014FA RID: 5370
		// (add) Token: 0x0600ACF0 RID: 44272
		// (remove) Token: 0x0600ACF1 RID: 44273
		public virtual extern event HTMLMarqueeElementEvents2_onbeforeupdateEventHandler HTMLMarqueeElementEvents2_Event_onbeforeupdate;

		// Token: 0x140014FB RID: 5371
		// (add) Token: 0x0600ACF2 RID: 44274
		// (remove) Token: 0x0600ACF3 RID: 44275
		public virtual extern event HTMLMarqueeElementEvents2_onafterupdateEventHandler HTMLMarqueeElementEvents2_Event_onafterupdate;

		// Token: 0x140014FC RID: 5372
		// (add) Token: 0x0600ACF4 RID: 44276
		// (remove) Token: 0x0600ACF5 RID: 44277
		public virtual extern event HTMLMarqueeElementEvents2_onerrorupdateEventHandler HTMLMarqueeElementEvents2_Event_onerrorupdate;

		// Token: 0x140014FD RID: 5373
		// (add) Token: 0x0600ACF6 RID: 44278
		// (remove) Token: 0x0600ACF7 RID: 44279
		public virtual extern event HTMLMarqueeElementEvents2_onrowexitEventHandler HTMLMarqueeElementEvents2_Event_onrowexit;

		// Token: 0x140014FE RID: 5374
		// (add) Token: 0x0600ACF8 RID: 44280
		// (remove) Token: 0x0600ACF9 RID: 44281
		public virtual extern event HTMLMarqueeElementEvents2_onrowenterEventHandler HTMLMarqueeElementEvents2_Event_onrowenter;

		// Token: 0x140014FF RID: 5375
		// (add) Token: 0x0600ACFA RID: 44282
		// (remove) Token: 0x0600ACFB RID: 44283
		public virtual extern event HTMLMarqueeElementEvents2_ondatasetchangedEventHandler HTMLMarqueeElementEvents2_Event_ondatasetchanged;

		// Token: 0x14001500 RID: 5376
		// (add) Token: 0x0600ACFC RID: 44284
		// (remove) Token: 0x0600ACFD RID: 44285
		public virtual extern event HTMLMarqueeElementEvents2_ondataavailableEventHandler HTMLMarqueeElementEvents2_Event_ondataavailable;

		// Token: 0x14001501 RID: 5377
		// (add) Token: 0x0600ACFE RID: 44286
		// (remove) Token: 0x0600ACFF RID: 44287
		public virtual extern event HTMLMarqueeElementEvents2_ondatasetcompleteEventHandler HTMLMarqueeElementEvents2_Event_ondatasetcomplete;

		// Token: 0x14001502 RID: 5378
		// (add) Token: 0x0600AD00 RID: 44288
		// (remove) Token: 0x0600AD01 RID: 44289
		public virtual extern event HTMLMarqueeElementEvents2_onlosecaptureEventHandler HTMLMarqueeElementEvents2_Event_onlosecapture;

		// Token: 0x14001503 RID: 5379
		// (add) Token: 0x0600AD02 RID: 44290
		// (remove) Token: 0x0600AD03 RID: 44291
		public virtual extern event HTMLMarqueeElementEvents2_onpropertychangeEventHandler HTMLMarqueeElementEvents2_Event_onpropertychange;

		// Token: 0x14001504 RID: 5380
		// (add) Token: 0x0600AD04 RID: 44292
		// (remove) Token: 0x0600AD05 RID: 44293
		public virtual extern event HTMLMarqueeElementEvents2_onscrollEventHandler HTMLMarqueeElementEvents2_Event_onscroll;

		// Token: 0x14001505 RID: 5381
		// (add) Token: 0x0600AD06 RID: 44294
		// (remove) Token: 0x0600AD07 RID: 44295
		public virtual extern event HTMLMarqueeElementEvents2_onfocusEventHandler HTMLMarqueeElementEvents2_Event_onfocus;

		// Token: 0x14001506 RID: 5382
		// (add) Token: 0x0600AD08 RID: 44296
		// (remove) Token: 0x0600AD09 RID: 44297
		public virtual extern event HTMLMarqueeElementEvents2_onblurEventHandler HTMLMarqueeElementEvents2_Event_onblur;

		// Token: 0x14001507 RID: 5383
		// (add) Token: 0x0600AD0A RID: 44298
		// (remove) Token: 0x0600AD0B RID: 44299
		public virtual extern event HTMLMarqueeElementEvents2_onresizeEventHandler HTMLMarqueeElementEvents2_Event_onresize;

		// Token: 0x14001508 RID: 5384
		// (add) Token: 0x0600AD0C RID: 44300
		// (remove) Token: 0x0600AD0D RID: 44301
		public virtual extern event HTMLMarqueeElementEvents2_ondragEventHandler HTMLMarqueeElementEvents2_Event_ondrag;

		// Token: 0x14001509 RID: 5385
		// (add) Token: 0x0600AD0E RID: 44302
		// (remove) Token: 0x0600AD0F RID: 44303
		public virtual extern event HTMLMarqueeElementEvents2_ondragendEventHandler HTMLMarqueeElementEvents2_Event_ondragend;

		// Token: 0x1400150A RID: 5386
		// (add) Token: 0x0600AD10 RID: 44304
		// (remove) Token: 0x0600AD11 RID: 44305
		public virtual extern event HTMLMarqueeElementEvents2_ondragenterEventHandler HTMLMarqueeElementEvents2_Event_ondragenter;

		// Token: 0x1400150B RID: 5387
		// (add) Token: 0x0600AD12 RID: 44306
		// (remove) Token: 0x0600AD13 RID: 44307
		public virtual extern event HTMLMarqueeElementEvents2_ondragoverEventHandler HTMLMarqueeElementEvents2_Event_ondragover;

		// Token: 0x1400150C RID: 5388
		// (add) Token: 0x0600AD14 RID: 44308
		// (remove) Token: 0x0600AD15 RID: 44309
		public virtual extern event HTMLMarqueeElementEvents2_ondragleaveEventHandler HTMLMarqueeElementEvents2_Event_ondragleave;

		// Token: 0x1400150D RID: 5389
		// (add) Token: 0x0600AD16 RID: 44310
		// (remove) Token: 0x0600AD17 RID: 44311
		public virtual extern event HTMLMarqueeElementEvents2_ondropEventHandler HTMLMarqueeElementEvents2_Event_ondrop;

		// Token: 0x1400150E RID: 5390
		// (add) Token: 0x0600AD18 RID: 44312
		// (remove) Token: 0x0600AD19 RID: 44313
		public virtual extern event HTMLMarqueeElementEvents2_onbeforecutEventHandler HTMLMarqueeElementEvents2_Event_onbeforecut;

		// Token: 0x1400150F RID: 5391
		// (add) Token: 0x0600AD1A RID: 44314
		// (remove) Token: 0x0600AD1B RID: 44315
		public virtual extern event HTMLMarqueeElementEvents2_oncutEventHandler HTMLMarqueeElementEvents2_Event_oncut;

		// Token: 0x14001510 RID: 5392
		// (add) Token: 0x0600AD1C RID: 44316
		// (remove) Token: 0x0600AD1D RID: 44317
		public virtual extern event HTMLMarqueeElementEvents2_onbeforecopyEventHandler HTMLMarqueeElementEvents2_Event_onbeforecopy;

		// Token: 0x14001511 RID: 5393
		// (add) Token: 0x0600AD1E RID: 44318
		// (remove) Token: 0x0600AD1F RID: 44319
		public virtual extern event HTMLMarqueeElementEvents2_oncopyEventHandler HTMLMarqueeElementEvents2_Event_oncopy;

		// Token: 0x14001512 RID: 5394
		// (add) Token: 0x0600AD20 RID: 44320
		// (remove) Token: 0x0600AD21 RID: 44321
		public virtual extern event HTMLMarqueeElementEvents2_onbeforepasteEventHandler HTMLMarqueeElementEvents2_Event_onbeforepaste;

		// Token: 0x14001513 RID: 5395
		// (add) Token: 0x0600AD22 RID: 44322
		// (remove) Token: 0x0600AD23 RID: 44323
		public virtual extern event HTMLMarqueeElementEvents2_onpasteEventHandler HTMLMarqueeElementEvents2_Event_onpaste;

		// Token: 0x14001514 RID: 5396
		// (add) Token: 0x0600AD24 RID: 44324
		// (remove) Token: 0x0600AD25 RID: 44325
		public virtual extern event HTMLMarqueeElementEvents2_oncontextmenuEventHandler HTMLMarqueeElementEvents2_Event_oncontextmenu;

		// Token: 0x14001515 RID: 5397
		// (add) Token: 0x0600AD26 RID: 44326
		// (remove) Token: 0x0600AD27 RID: 44327
		public virtual extern event HTMLMarqueeElementEvents2_onrowsdeleteEventHandler HTMLMarqueeElementEvents2_Event_onrowsdelete;

		// Token: 0x14001516 RID: 5398
		// (add) Token: 0x0600AD28 RID: 44328
		// (remove) Token: 0x0600AD29 RID: 44329
		public virtual extern event HTMLMarqueeElementEvents2_onrowsinsertedEventHandler HTMLMarqueeElementEvents2_Event_onrowsinserted;

		// Token: 0x14001517 RID: 5399
		// (add) Token: 0x0600AD2A RID: 44330
		// (remove) Token: 0x0600AD2B RID: 44331
		public virtual extern event HTMLMarqueeElementEvents2_oncellchangeEventHandler HTMLMarqueeElementEvents2_Event_oncellchange;

		// Token: 0x14001518 RID: 5400
		// (add) Token: 0x0600AD2C RID: 44332
		// (remove) Token: 0x0600AD2D RID: 44333
		public virtual extern event HTMLMarqueeElementEvents2_onreadystatechangeEventHandler HTMLMarqueeElementEvents2_Event_onreadystatechange;

		// Token: 0x14001519 RID: 5401
		// (add) Token: 0x0600AD2E RID: 44334
		// (remove) Token: 0x0600AD2F RID: 44335
		public virtual extern event HTMLMarqueeElementEvents2_onlayoutcompleteEventHandler HTMLMarqueeElementEvents2_Event_onlayoutcomplete;

		// Token: 0x1400151A RID: 5402
		// (add) Token: 0x0600AD30 RID: 44336
		// (remove) Token: 0x0600AD31 RID: 44337
		public virtual extern event HTMLMarqueeElementEvents2_onpageEventHandler HTMLMarqueeElementEvents2_Event_onpage;

		// Token: 0x1400151B RID: 5403
		// (add) Token: 0x0600AD32 RID: 44338
		// (remove) Token: 0x0600AD33 RID: 44339
		public virtual extern event HTMLMarqueeElementEvents2_onmouseenterEventHandler HTMLMarqueeElementEvents2_Event_onmouseenter;

		// Token: 0x1400151C RID: 5404
		// (add) Token: 0x0600AD34 RID: 44340
		// (remove) Token: 0x0600AD35 RID: 44341
		public virtual extern event HTMLMarqueeElementEvents2_onmouseleaveEventHandler HTMLMarqueeElementEvents2_Event_onmouseleave;

		// Token: 0x1400151D RID: 5405
		// (add) Token: 0x0600AD36 RID: 44342
		// (remove) Token: 0x0600AD37 RID: 44343
		public virtual extern event HTMLMarqueeElementEvents2_onactivateEventHandler HTMLMarqueeElementEvents2_Event_onactivate;

		// Token: 0x1400151E RID: 5406
		// (add) Token: 0x0600AD38 RID: 44344
		// (remove) Token: 0x0600AD39 RID: 44345
		public virtual extern event HTMLMarqueeElementEvents2_ondeactivateEventHandler HTMLMarqueeElementEvents2_Event_ondeactivate;

		// Token: 0x1400151F RID: 5407
		// (add) Token: 0x0600AD3A RID: 44346
		// (remove) Token: 0x0600AD3B RID: 44347
		public virtual extern event HTMLMarqueeElementEvents2_onbeforedeactivateEventHandler HTMLMarqueeElementEvents2_Event_onbeforedeactivate;

		// Token: 0x14001520 RID: 5408
		// (add) Token: 0x0600AD3C RID: 44348
		// (remove) Token: 0x0600AD3D RID: 44349
		public virtual extern event HTMLMarqueeElementEvents2_onbeforeactivateEventHandler HTMLMarqueeElementEvents2_Event_onbeforeactivate;

		// Token: 0x14001521 RID: 5409
		// (add) Token: 0x0600AD3E RID: 44350
		// (remove) Token: 0x0600AD3F RID: 44351
		public virtual extern event HTMLMarqueeElementEvents2_onfocusinEventHandler HTMLMarqueeElementEvents2_Event_onfocusin;

		// Token: 0x14001522 RID: 5410
		// (add) Token: 0x0600AD40 RID: 44352
		// (remove) Token: 0x0600AD41 RID: 44353
		public virtual extern event HTMLMarqueeElementEvents2_onfocusoutEventHandler HTMLMarqueeElementEvents2_Event_onfocusout;

		// Token: 0x14001523 RID: 5411
		// (add) Token: 0x0600AD42 RID: 44354
		// (remove) Token: 0x0600AD43 RID: 44355
		public virtual extern event HTMLMarqueeElementEvents2_onmoveEventHandler HTMLMarqueeElementEvents2_Event_onmove;

		// Token: 0x14001524 RID: 5412
		// (add) Token: 0x0600AD44 RID: 44356
		// (remove) Token: 0x0600AD45 RID: 44357
		public virtual extern event HTMLMarqueeElementEvents2_oncontrolselectEventHandler HTMLMarqueeElementEvents2_Event_oncontrolselect;

		// Token: 0x14001525 RID: 5413
		// (add) Token: 0x0600AD46 RID: 44358
		// (remove) Token: 0x0600AD47 RID: 44359
		public virtual extern event HTMLMarqueeElementEvents2_onmovestartEventHandler HTMLMarqueeElementEvents2_Event_onmovestart;

		// Token: 0x14001526 RID: 5414
		// (add) Token: 0x0600AD48 RID: 44360
		// (remove) Token: 0x0600AD49 RID: 44361
		public virtual extern event HTMLMarqueeElementEvents2_onmoveendEventHandler HTMLMarqueeElementEvents2_Event_onmoveend;

		// Token: 0x14001527 RID: 5415
		// (add) Token: 0x0600AD4A RID: 44362
		// (remove) Token: 0x0600AD4B RID: 44363
		public virtual extern event HTMLMarqueeElementEvents2_onresizestartEventHandler HTMLMarqueeElementEvents2_Event_onresizestart;

		// Token: 0x14001528 RID: 5416
		// (add) Token: 0x0600AD4C RID: 44364
		// (remove) Token: 0x0600AD4D RID: 44365
		public virtual extern event HTMLMarqueeElementEvents2_onresizeendEventHandler HTMLMarqueeElementEvents2_Event_onresizeend;

		// Token: 0x14001529 RID: 5417
		// (add) Token: 0x0600AD4E RID: 44366
		// (remove) Token: 0x0600AD4F RID: 44367
		public virtual extern event HTMLMarqueeElementEvents2_onmousewheelEventHandler HTMLMarqueeElementEvents2_Event_onmousewheel;

		// Token: 0x1400152A RID: 5418
		// (add) Token: 0x0600AD50 RID: 44368
		// (remove) Token: 0x0600AD51 RID: 44369
		public virtual extern event HTMLMarqueeElementEvents2_onchangeEventHandler HTMLMarqueeElementEvents2_Event_onchange;

		// Token: 0x1400152B RID: 5419
		// (add) Token: 0x0600AD52 RID: 44370
		// (remove) Token: 0x0600AD53 RID: 44371
		public virtual extern event HTMLMarqueeElementEvents2_onselectEventHandler HTMLMarqueeElementEvents2_Event_onselect;

		// Token: 0x1400152C RID: 5420
		// (add) Token: 0x0600AD54 RID: 44372
		// (remove) Token: 0x0600AD55 RID: 44373
		public virtual extern event HTMLMarqueeElementEvents2_onbounceEventHandler HTMLMarqueeElementEvents2_Event_onbounce;

		// Token: 0x1400152D RID: 5421
		// (add) Token: 0x0600AD56 RID: 44374
		// (remove) Token: 0x0600AD57 RID: 44375
		public virtual extern event HTMLMarqueeElementEvents2_onfinishEventHandler HTMLMarqueeElementEvents2_Event_onfinish;

		// Token: 0x1400152E RID: 5422
		// (add) Token: 0x0600AD58 RID: 44376
		// (remove) Token: 0x0600AD59 RID: 44377
		public virtual extern event HTMLMarqueeElementEvents2_onstartEventHandler HTMLMarqueeElementEvents2_Event_onstart;
	}
}
