using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009E9 RID: 2537
	[Guid("3050F26E-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLElementEvents\0\0")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLPhraseElementClass : DispHTMLPhraseElement, HTMLPhraseElement, HTMLElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLPhraseElement, IHTMLPhraseElement2
	{
		// Token: 0x0600FCC0 RID: 64704
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLPhraseElementClass();

		// Token: 0x0600FCC1 RID: 64705
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600FCC2 RID: 64706
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600FCC3 RID: 64707
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170052BD RID: 21181
		// (get) Token: 0x0600FCC5 RID: 64709
		// (set) Token: 0x0600FCC4 RID: 64708
		[DispId(-2147417111)]
		public virtual extern string className
		{
			[DispId(-2147417111)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170052BE RID: 21182
		// (get) Token: 0x0600FCC7 RID: 64711
		// (set) Token: 0x0600FCC6 RID: 64710
		[DispId(-2147417110)]
		public virtual extern string id
		{
			[TypeLibFunc(4)]
			[DispId(-2147417110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170052BF RID: 21183
		// (get) Token: 0x0600FCC8 RID: 64712
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170052C0 RID: 21184
		// (get) Token: 0x0600FCC9 RID: 64713
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170052C1 RID: 21185
		// (get) Token: 0x0600FCCA RID: 64714
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170052C2 RID: 21186
		// (get) Token: 0x0600FCCC RID: 64716
		// (set) Token: 0x0600FCCB RID: 64715
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

		// Token: 0x170052C3 RID: 21187
		// (get) Token: 0x0600FCCE RID: 64718
		// (set) Token: 0x0600FCCD RID: 64717
		[DispId(-2147412104)]
		public virtual extern object onclick
		{
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170052C4 RID: 21188
		// (get) Token: 0x0600FCD0 RID: 64720
		// (set) Token: 0x0600FCCF RID: 64719
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

		// Token: 0x170052C5 RID: 21189
		// (get) Token: 0x0600FCD2 RID: 64722
		// (set) Token: 0x0600FCD1 RID: 64721
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

		// Token: 0x170052C6 RID: 21190
		// (get) Token: 0x0600FCD4 RID: 64724
		// (set) Token: 0x0600FCD3 RID: 64723
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

		// Token: 0x170052C7 RID: 21191
		// (get) Token: 0x0600FCD6 RID: 64726
		// (set) Token: 0x0600FCD5 RID: 64725
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

		// Token: 0x170052C8 RID: 21192
		// (get) Token: 0x0600FCD8 RID: 64728
		// (set) Token: 0x0600FCD7 RID: 64727
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

		// Token: 0x170052C9 RID: 21193
		// (get) Token: 0x0600FCDA RID: 64730
		// (set) Token: 0x0600FCD9 RID: 64729
		[DispId(-2147412112)]
		public virtual extern object onmouseover
		{
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170052CA RID: 21194
		// (get) Token: 0x0600FCDC RID: 64732
		// (set) Token: 0x0600FCDB RID: 64731
		[DispId(-2147412108)]
		public virtual extern object onmousemove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170052CB RID: 21195
		// (get) Token: 0x0600FCDE RID: 64734
		// (set) Token: 0x0600FCDD RID: 64733
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

		// Token: 0x170052CC RID: 21196
		// (get) Token: 0x0600FCE0 RID: 64736
		// (set) Token: 0x0600FCDF RID: 64735
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

		// Token: 0x170052CD RID: 21197
		// (get) Token: 0x0600FCE1 RID: 64737
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170052CE RID: 21198
		// (get) Token: 0x0600FCE3 RID: 64739
		// (set) Token: 0x0600FCE2 RID: 64738
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

		// Token: 0x170052CF RID: 21199
		// (get) Token: 0x0600FCE5 RID: 64741
		// (set) Token: 0x0600FCE4 RID: 64740
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

		// Token: 0x170052D0 RID: 21200
		// (get) Token: 0x0600FCE7 RID: 64743
		// (set) Token: 0x0600FCE6 RID: 64742
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

		// Token: 0x0600FCE8 RID: 64744
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600FCE9 RID: 64745
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170052D1 RID: 21201
		// (get) Token: 0x0600FCEA RID: 64746
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170052D2 RID: 21202
		// (get) Token: 0x0600FCEB RID: 64747
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170052D3 RID: 21203
		// (get) Token: 0x0600FCED RID: 64749
		// (set) Token: 0x0600FCEC RID: 64748
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

		// Token: 0x170052D4 RID: 21204
		// (get) Token: 0x0600FCEE RID: 64750
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170052D5 RID: 21205
		// (get) Token: 0x0600FCEF RID: 64751
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170052D6 RID: 21206
		// (get) Token: 0x0600FCF0 RID: 64752
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170052D7 RID: 21207
		// (get) Token: 0x0600FCF1 RID: 64753
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170052D8 RID: 21208
		// (get) Token: 0x0600FCF2 RID: 64754
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170052D9 RID: 21209
		// (get) Token: 0x0600FCF4 RID: 64756
		// (set) Token: 0x0600FCF3 RID: 64755
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

		// Token: 0x170052DA RID: 21210
		// (get) Token: 0x0600FCF6 RID: 64758
		// (set) Token: 0x0600FCF5 RID: 64757
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

		// Token: 0x170052DB RID: 21211
		// (get) Token: 0x0600FCF8 RID: 64760
		// (set) Token: 0x0600FCF7 RID: 64759
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

		// Token: 0x170052DC RID: 21212
		// (get) Token: 0x0600FCFA RID: 64762
		// (set) Token: 0x0600FCF9 RID: 64761
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

		// Token: 0x0600FCFB RID: 64763
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600FCFC RID: 64764
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170052DD RID: 21213
		// (get) Token: 0x0600FCFD RID: 64765
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170052DE RID: 21214
		// (get) Token: 0x0600FCFE RID: 64766
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600FCFF RID: 64767
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x170052DF RID: 21215
		// (get) Token: 0x0600FD00 RID: 64768
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170052E0 RID: 21216
		// (get) Token: 0x0600FD02 RID: 64770
		// (set) Token: 0x0600FD01 RID: 64769
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

		// Token: 0x0600FD03 RID: 64771
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x170052E1 RID: 21217
		// (get) Token: 0x0600FD05 RID: 64773
		// (set) Token: 0x0600FD04 RID: 64772
		[DispId(-2147412091)]
		public virtual extern object onbeforeupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170052E2 RID: 21218
		// (get) Token: 0x0600FD07 RID: 64775
		// (set) Token: 0x0600FD06 RID: 64774
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

		// Token: 0x170052E3 RID: 21219
		// (get) Token: 0x0600FD09 RID: 64777
		// (set) Token: 0x0600FD08 RID: 64776
		[DispId(-2147412074)]
		public virtual extern object onerrorupdate
		{
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170052E4 RID: 21220
		// (get) Token: 0x0600FD0B RID: 64779
		// (set) Token: 0x0600FD0A RID: 64778
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

		// Token: 0x170052E5 RID: 21221
		// (get) Token: 0x0600FD0D RID: 64781
		// (set) Token: 0x0600FD0C RID: 64780
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

		// Token: 0x170052E6 RID: 21222
		// (get) Token: 0x0600FD0F RID: 64783
		// (set) Token: 0x0600FD0E RID: 64782
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

		// Token: 0x170052E7 RID: 21223
		// (get) Token: 0x0600FD11 RID: 64785
		// (set) Token: 0x0600FD10 RID: 64784
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

		// Token: 0x170052E8 RID: 21224
		// (get) Token: 0x0600FD13 RID: 64787
		// (set) Token: 0x0600FD12 RID: 64786
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

		// Token: 0x170052E9 RID: 21225
		// (get) Token: 0x0600FD15 RID: 64789
		// (set) Token: 0x0600FD14 RID: 64788
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

		// Token: 0x170052EA RID: 21226
		// (get) Token: 0x0600FD16 RID: 64790
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170052EB RID: 21227
		// (get) Token: 0x0600FD17 RID: 64791
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170052EC RID: 21228
		// (get) Token: 0x0600FD18 RID: 64792
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600FD19 RID: 64793
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0600FD1A RID: 64794
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x170052ED RID: 21229
		// (get) Token: 0x0600FD1C RID: 64796
		// (set) Token: 0x0600FD1B RID: 64795
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

		// Token: 0x0600FD1D RID: 64797
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600FD1E RID: 64798
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170052EE RID: 21230
		// (get) Token: 0x0600FD20 RID: 64800
		// (set) Token: 0x0600FD1F RID: 64799
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

		// Token: 0x170052EF RID: 21231
		// (get) Token: 0x0600FD22 RID: 64802
		// (set) Token: 0x0600FD21 RID: 64801
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

		// Token: 0x170052F0 RID: 21232
		// (get) Token: 0x0600FD24 RID: 64804
		// (set) Token: 0x0600FD23 RID: 64803
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

		// Token: 0x170052F1 RID: 21233
		// (get) Token: 0x0600FD26 RID: 64806
		// (set) Token: 0x0600FD25 RID: 64805
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

		// Token: 0x170052F2 RID: 21234
		// (get) Token: 0x0600FD28 RID: 64808
		// (set) Token: 0x0600FD27 RID: 64807
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

		// Token: 0x170052F3 RID: 21235
		// (get) Token: 0x0600FD2A RID: 64810
		// (set) Token: 0x0600FD29 RID: 64809
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

		// Token: 0x170052F4 RID: 21236
		// (get) Token: 0x0600FD2C RID: 64812
		// (set) Token: 0x0600FD2B RID: 64811
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

		// Token: 0x170052F5 RID: 21237
		// (get) Token: 0x0600FD2E RID: 64814
		// (set) Token: 0x0600FD2D RID: 64813
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

		// Token: 0x170052F6 RID: 21238
		// (get) Token: 0x0600FD30 RID: 64816
		// (set) Token: 0x0600FD2F RID: 64815
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

		// Token: 0x170052F7 RID: 21239
		// (get) Token: 0x0600FD32 RID: 64818
		// (set) Token: 0x0600FD31 RID: 64817
		[DispId(-2147412053)]
		public virtual extern object onbeforecopy
		{
			[DispId(-2147412053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170052F8 RID: 21240
		// (get) Token: 0x0600FD34 RID: 64820
		// (set) Token: 0x0600FD33 RID: 64819
		[DispId(-2147412056)]
		public virtual extern object oncopy
		{
			[DispId(-2147412056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170052F9 RID: 21241
		// (get) Token: 0x0600FD36 RID: 64822
		// (set) Token: 0x0600FD35 RID: 64821
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

		// Token: 0x170052FA RID: 21242
		// (get) Token: 0x0600FD38 RID: 64824
		// (set) Token: 0x0600FD37 RID: 64823
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

		// Token: 0x170052FB RID: 21243
		// (get) Token: 0x0600FD39 RID: 64825
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170052FC RID: 21244
		// (get) Token: 0x0600FD3B RID: 64827
		// (set) Token: 0x0600FD3A RID: 64826
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

		// Token: 0x0600FD3C RID: 64828
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x0600FD3D RID: 64829
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x0600FD3E RID: 64830
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600FD3F RID: 64831
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600FD40 RID: 64832
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170052FD RID: 21245
		// (get) Token: 0x0600FD42 RID: 64834
		// (set) Token: 0x0600FD41 RID: 64833
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

		// Token: 0x0600FD43 RID: 64835
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x170052FE RID: 21246
		// (get) Token: 0x0600FD45 RID: 64837
		// (set) Token: 0x0600FD44 RID: 64836
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

		// Token: 0x170052FF RID: 21247
		// (get) Token: 0x0600FD47 RID: 64839
		// (set) Token: 0x0600FD46 RID: 64838
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

		// Token: 0x17005300 RID: 21248
		// (get) Token: 0x0600FD49 RID: 64841
		// (set) Token: 0x0600FD48 RID: 64840
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

		// Token: 0x17005301 RID: 21249
		// (get) Token: 0x0600FD4B RID: 64843
		// (set) Token: 0x0600FD4A RID: 64842
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

		// Token: 0x0600FD4C RID: 64844
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600FD4D RID: 64845
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600FD4E RID: 64846
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17005302 RID: 21250
		// (get) Token: 0x0600FD4F RID: 64847
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005303 RID: 21251
		// (get) Token: 0x0600FD50 RID: 64848
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005304 RID: 21252
		// (get) Token: 0x0600FD51 RID: 64849
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005305 RID: 21253
		// (get) Token: 0x0600FD52 RID: 64850
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600FD53 RID: 64851
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600FD54 RID: 64852
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17005306 RID: 21254
		// (get) Token: 0x0600FD55 RID: 64853
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17005307 RID: 21255
		// (get) Token: 0x0600FD57 RID: 64855
		// (set) Token: 0x0600FD56 RID: 64854
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

		// Token: 0x17005308 RID: 21256
		// (get) Token: 0x0600FD59 RID: 64857
		// (set) Token: 0x0600FD58 RID: 64856
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

		// Token: 0x17005309 RID: 21257
		// (get) Token: 0x0600FD5B RID: 64859
		// (set) Token: 0x0600FD5A RID: 64858
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

		// Token: 0x1700530A RID: 21258
		// (get) Token: 0x0600FD5D RID: 64861
		// (set) Token: 0x0600FD5C RID: 64860
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

		// Token: 0x1700530B RID: 21259
		// (get) Token: 0x0600FD5F RID: 64863
		// (set) Token: 0x0600FD5E RID: 64862
		[DispId(-2147412995)]
		public virtual extern string dir
		{
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600FD60 RID: 64864
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x1700530C RID: 21260
		// (get) Token: 0x0600FD61 RID: 64865
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700530D RID: 21261
		// (get) Token: 0x0600FD62 RID: 64866
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700530E RID: 21262
		// (get) Token: 0x0600FD64 RID: 64868
		// (set) Token: 0x0600FD63 RID: 64867
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

		// Token: 0x1700530F RID: 21263
		// (get) Token: 0x0600FD66 RID: 64870
		// (set) Token: 0x0600FD65 RID: 64869
		[DispId(-2147417052)]
		public virtual extern int scrollLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600FD67 RID: 64871
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17005310 RID: 21264
		// (get) Token: 0x0600FD69 RID: 64873
		// (set) Token: 0x0600FD68 RID: 64872
		[DispId(-2147412047)]
		public virtual extern object oncontextmenu
		{
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600FD6A RID: 64874
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600FD6B RID: 64875
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600FD6C RID: 64876
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600FD6D RID: 64877
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17005311 RID: 21265
		// (get) Token: 0x0600FD6E RID: 64878
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600FD6F RID: 64879
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600FD70 RID: 64880
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17005312 RID: 21266
		// (get) Token: 0x0600FD71 RID: 64881
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005313 RID: 21267
		// (get) Token: 0x0600FD72 RID: 64882
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005314 RID: 21268
		// (get) Token: 0x0600FD74 RID: 64884
		// (set) Token: 0x0600FD73 RID: 64883
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

		// Token: 0x17005315 RID: 21269
		// (get) Token: 0x0600FD76 RID: 64886
		// (set) Token: 0x0600FD75 RID: 64885
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

		// Token: 0x17005316 RID: 21270
		// (get) Token: 0x0600FD77 RID: 64887
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600FD78 RID: 64888
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600FD79 RID: 64889
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17005317 RID: 21271
		// (get) Token: 0x0600FD7A RID: 64890
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005318 RID: 21272
		// (get) Token: 0x0600FD7B RID: 64891
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005319 RID: 21273
		// (get) Token: 0x0600FD7D RID: 64893
		// (set) Token: 0x0600FD7C RID: 64892
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

		// Token: 0x1700531A RID: 21274
		// (get) Token: 0x0600FD7F RID: 64895
		// (set) Token: 0x0600FD7E RID: 64894
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

		// Token: 0x1700531B RID: 21275
		// (get) Token: 0x0600FD81 RID: 64897
		// (set) Token: 0x0600FD80 RID: 64896
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

		// Token: 0x1700531C RID: 21276
		// (get) Token: 0x0600FD83 RID: 64899
		// (set) Token: 0x0600FD82 RID: 64898
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

		// Token: 0x0600FD84 RID: 64900
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x1700531D RID: 21277
		// (get) Token: 0x0600FD86 RID: 64902
		// (set) Token: 0x0600FD85 RID: 64901
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

		// Token: 0x1700531E RID: 21278
		// (get) Token: 0x0600FD87 RID: 64903
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700531F RID: 21279
		// (get) Token: 0x0600FD89 RID: 64905
		// (set) Token: 0x0600FD88 RID: 64904
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

		// Token: 0x17005320 RID: 21280
		// (get) Token: 0x0600FD8B RID: 64907
		// (set) Token: 0x0600FD8A RID: 64906
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

		// Token: 0x17005321 RID: 21281
		// (get) Token: 0x0600FD8C RID: 64908
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005322 RID: 21282
		// (get) Token: 0x0600FD8E RID: 64910
		// (set) Token: 0x0600FD8D RID: 64909
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

		// Token: 0x17005323 RID: 21283
		// (get) Token: 0x0600FD90 RID: 64912
		// (set) Token: 0x0600FD8F RID: 64911
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

		// Token: 0x0600FD91 RID: 64913
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17005324 RID: 21284
		// (get) Token: 0x0600FD93 RID: 64915
		// (set) Token: 0x0600FD92 RID: 64914
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

		// Token: 0x17005325 RID: 21285
		// (get) Token: 0x0600FD95 RID: 64917
		// (set) Token: 0x0600FD94 RID: 64916
		[DispId(-2147412028)]
		public virtual extern object onresizeend
		{
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005326 RID: 21286
		// (get) Token: 0x0600FD97 RID: 64919
		// (set) Token: 0x0600FD96 RID: 64918
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

		// Token: 0x17005327 RID: 21287
		// (get) Token: 0x0600FD99 RID: 64921
		// (set) Token: 0x0600FD98 RID: 64920
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

		// Token: 0x17005328 RID: 21288
		// (get) Token: 0x0600FD9B RID: 64923
		// (set) Token: 0x0600FD9A RID: 64922
		[DispId(-2147412027)]
		public virtual extern object onmouseenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005329 RID: 21289
		// (get) Token: 0x0600FD9D RID: 64925
		// (set) Token: 0x0600FD9C RID: 64924
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

		// Token: 0x1700532A RID: 21290
		// (get) Token: 0x0600FD9F RID: 64927
		// (set) Token: 0x0600FD9E RID: 64926
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700532B RID: 21291
		// (get) Token: 0x0600FDA1 RID: 64929
		// (set) Token: 0x0600FDA0 RID: 64928
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

		// Token: 0x0600FDA2 RID: 64930
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x1700532C RID: 21292
		// (get) Token: 0x0600FDA3 RID: 64931
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700532D RID: 21293
		// (get) Token: 0x0600FDA5 RID: 64933
		// (set) Token: 0x0600FDA4 RID: 64932
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

		// Token: 0x0600FDA6 RID: 64934
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600FDA7 RID: 64935
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600FDA8 RID: 64936
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600FDA9 RID: 64937
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700532E RID: 21294
		// (get) Token: 0x0600FDAB RID: 64939
		// (set) Token: 0x0600FDAA RID: 64938
		[DispId(-2147412022)]
		public virtual extern object onbeforeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700532F RID: 21295
		// (get) Token: 0x0600FDAD RID: 64941
		// (set) Token: 0x0600FDAC RID: 64940
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

		// Token: 0x17005330 RID: 21296
		// (get) Token: 0x0600FDAF RID: 64943
		// (set) Token: 0x0600FDAE RID: 64942
		[DispId(-2147412020)]
		public virtual extern object onfocusout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005331 RID: 21297
		// (get) Token: 0x0600FDB0 RID: 64944
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005332 RID: 21298
		// (get) Token: 0x0600FDB1 RID: 64945
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005333 RID: 21299
		// (get) Token: 0x0600FDB2 RID: 64946
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005334 RID: 21300
		// (get) Token: 0x0600FDB3 RID: 64947
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600FDB4 RID: 64948
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17005335 RID: 21301
		// (get) Token: 0x0600FDB5 RID: 64949
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005336 RID: 21302
		// (get) Token: 0x0600FDB6 RID: 64950
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600FDB7 RID: 64951
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600FDB8 RID: 64952
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600FDB9 RID: 64953
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600FDBA RID: 64954
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600FDBB RID: 64955
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600FDBC RID: 64956
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600FDBD RID: 64957
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600FDBE RID: 64958
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17005337 RID: 21303
		// (get) Token: 0x0600FDBF RID: 64959
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005338 RID: 21304
		// (get) Token: 0x0600FDC1 RID: 64961
		// (set) Token: 0x0600FDC0 RID: 64960
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

		// Token: 0x17005339 RID: 21305
		// (get) Token: 0x0600FDC2 RID: 64962
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700533A RID: 21306
		// (get) Token: 0x0600FDC3 RID: 64963
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700533B RID: 21307
		// (get) Token: 0x0600FDC4 RID: 64964
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700533C RID: 21308
		// (get) Token: 0x0600FDC5 RID: 64965
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700533D RID: 21309
		// (get) Token: 0x0600FDC6 RID: 64966
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700533E RID: 21310
		// (get) Token: 0x0600FDC8 RID: 64968
		// (set) Token: 0x0600FDC7 RID: 64967
		[DispId(1001)]
		public virtual extern string cite
		{
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700533F RID: 21311
		// (get) Token: 0x0600FDCA RID: 64970
		// (set) Token: 0x0600FDC9 RID: 64969
		[DispId(1002)]
		public virtual extern string dateTime
		{
			[DispId(1002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600FDCB RID: 64971
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600FDCC RID: 64972
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600FDCD RID: 64973
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17005340 RID: 21312
		// (get) Token: 0x0600FDCF RID: 64975
		// (set) Token: 0x0600FDCE RID: 64974
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

		// Token: 0x17005341 RID: 21313
		// (get) Token: 0x0600FDD1 RID: 64977
		// (set) Token: 0x0600FDD0 RID: 64976
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

		// Token: 0x17005342 RID: 21314
		// (get) Token: 0x0600FDD2 RID: 64978
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005343 RID: 21315
		// (get) Token: 0x0600FDD3 RID: 64979
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005344 RID: 21316
		// (get) Token: 0x0600FDD4 RID: 64980
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005345 RID: 21317
		// (get) Token: 0x0600FDD6 RID: 64982
		// (set) Token: 0x0600FDD5 RID: 64981
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

		// Token: 0x17005346 RID: 21318
		// (get) Token: 0x0600FDD8 RID: 64984
		// (set) Token: 0x0600FDD7 RID: 64983
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

		// Token: 0x17005347 RID: 21319
		// (get) Token: 0x0600FDDA RID: 64986
		// (set) Token: 0x0600FDD9 RID: 64985
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

		// Token: 0x17005348 RID: 21320
		// (get) Token: 0x0600FDDC RID: 64988
		// (set) Token: 0x0600FDDB RID: 64987
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

		// Token: 0x17005349 RID: 21321
		// (get) Token: 0x0600FDDE RID: 64990
		// (set) Token: 0x0600FDDD RID: 64989
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

		// Token: 0x1700534A RID: 21322
		// (get) Token: 0x0600FDE0 RID: 64992
		// (set) Token: 0x0600FDDF RID: 64991
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

		// Token: 0x1700534B RID: 21323
		// (get) Token: 0x0600FDE2 RID: 64994
		// (set) Token: 0x0600FDE1 RID: 64993
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

		// Token: 0x1700534C RID: 21324
		// (get) Token: 0x0600FDE4 RID: 64996
		// (set) Token: 0x0600FDE3 RID: 64995
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

		// Token: 0x1700534D RID: 21325
		// (get) Token: 0x0600FDE6 RID: 64998
		// (set) Token: 0x0600FDE5 RID: 64997
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

		// Token: 0x1700534E RID: 21326
		// (get) Token: 0x0600FDE8 RID: 65000
		// (set) Token: 0x0600FDE7 RID: 64999
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

		// Token: 0x1700534F RID: 21327
		// (get) Token: 0x0600FDEA RID: 65002
		// (set) Token: 0x0600FDE9 RID: 65001
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

		// Token: 0x17005350 RID: 21328
		// (get) Token: 0x0600FDEB RID: 65003
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005351 RID: 21329
		// (get) Token: 0x0600FDED RID: 65005
		// (set) Token: 0x0600FDEC RID: 65004
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

		// Token: 0x17005352 RID: 21330
		// (get) Token: 0x0600FDEF RID: 65007
		// (set) Token: 0x0600FDEE RID: 65006
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

		// Token: 0x17005353 RID: 21331
		// (get) Token: 0x0600FDF1 RID: 65009
		// (set) Token: 0x0600FDF0 RID: 65008
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

		// Token: 0x0600FDF2 RID: 65010
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600FDF3 RID: 65011
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17005354 RID: 21332
		// (get) Token: 0x0600FDF4 RID: 65012
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005355 RID: 21333
		// (get) Token: 0x0600FDF5 RID: 65013
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17005356 RID: 21334
		// (get) Token: 0x0600FDF7 RID: 65015
		// (set) Token: 0x0600FDF6 RID: 65014
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

		// Token: 0x17005357 RID: 21335
		// (get) Token: 0x0600FDF8 RID: 65016
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005358 RID: 21336
		// (get) Token: 0x0600FDF9 RID: 65017
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005359 RID: 21337
		// (get) Token: 0x0600FDFA RID: 65018
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700535A RID: 21338
		// (get) Token: 0x0600FDFB RID: 65019
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700535B RID: 21339
		// (get) Token: 0x0600FDFC RID: 65020
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700535C RID: 21340
		// (get) Token: 0x0600FDFE RID: 65022
		// (set) Token: 0x0600FDFD RID: 65021
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

		// Token: 0x1700535D RID: 21341
		// (get) Token: 0x0600FE00 RID: 65024
		// (set) Token: 0x0600FDFF RID: 65023
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

		// Token: 0x1700535E RID: 21342
		// (get) Token: 0x0600FE02 RID: 65026
		// (set) Token: 0x0600FE01 RID: 65025
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

		// Token: 0x1700535F RID: 21343
		// (get) Token: 0x0600FE04 RID: 65028
		// (set) Token: 0x0600FE03 RID: 65027
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

		// Token: 0x0600FE05 RID: 65029
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600FE06 RID: 65030
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17005360 RID: 21344
		// (get) Token: 0x0600FE07 RID: 65031
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005361 RID: 21345
		// (get) Token: 0x0600FE08 RID: 65032
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600FE09 RID: 65033
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17005362 RID: 21346
		// (get) Token: 0x0600FE0A RID: 65034
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005363 RID: 21347
		// (get) Token: 0x0600FE0C RID: 65036
		// (set) Token: 0x0600FE0B RID: 65035
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

		// Token: 0x0600FE0D RID: 65037
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17005364 RID: 21348
		// (get) Token: 0x0600FE0F RID: 65039
		// (set) Token: 0x0600FE0E RID: 65038
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

		// Token: 0x17005365 RID: 21349
		// (get) Token: 0x0600FE11 RID: 65041
		// (set) Token: 0x0600FE10 RID: 65040
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

		// Token: 0x17005366 RID: 21350
		// (get) Token: 0x0600FE13 RID: 65043
		// (set) Token: 0x0600FE12 RID: 65042
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

		// Token: 0x17005367 RID: 21351
		// (get) Token: 0x0600FE15 RID: 65045
		// (set) Token: 0x0600FE14 RID: 65044
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

		// Token: 0x17005368 RID: 21352
		// (get) Token: 0x0600FE17 RID: 65047
		// (set) Token: 0x0600FE16 RID: 65046
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

		// Token: 0x17005369 RID: 21353
		// (get) Token: 0x0600FE19 RID: 65049
		// (set) Token: 0x0600FE18 RID: 65048
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

		// Token: 0x1700536A RID: 21354
		// (get) Token: 0x0600FE1B RID: 65051
		// (set) Token: 0x0600FE1A RID: 65050
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

		// Token: 0x1700536B RID: 21355
		// (get) Token: 0x0600FE1D RID: 65053
		// (set) Token: 0x0600FE1C RID: 65052
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

		// Token: 0x1700536C RID: 21356
		// (get) Token: 0x0600FE1F RID: 65055
		// (set) Token: 0x0600FE1E RID: 65054
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

		// Token: 0x1700536D RID: 21357
		// (get) Token: 0x0600FE20 RID: 65056
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700536E RID: 21358
		// (get) Token: 0x0600FE21 RID: 65057
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700536F RID: 21359
		// (get) Token: 0x0600FE22 RID: 65058
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600FE23 RID: 65059
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600FE24 RID: 65060
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17005370 RID: 21360
		// (get) Token: 0x0600FE26 RID: 65062
		// (set) Token: 0x0600FE25 RID: 65061
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

		// Token: 0x0600FE27 RID: 65063
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600FE28 RID: 65064
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17005371 RID: 21361
		// (get) Token: 0x0600FE2A RID: 65066
		// (set) Token: 0x0600FE29 RID: 65065
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

		// Token: 0x17005372 RID: 21362
		// (get) Token: 0x0600FE2C RID: 65068
		// (set) Token: 0x0600FE2B RID: 65067
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

		// Token: 0x17005373 RID: 21363
		// (get) Token: 0x0600FE2E RID: 65070
		// (set) Token: 0x0600FE2D RID: 65069
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

		// Token: 0x17005374 RID: 21364
		// (get) Token: 0x0600FE30 RID: 65072
		// (set) Token: 0x0600FE2F RID: 65071
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

		// Token: 0x17005375 RID: 21365
		// (get) Token: 0x0600FE32 RID: 65074
		// (set) Token: 0x0600FE31 RID: 65073
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

		// Token: 0x17005376 RID: 21366
		// (get) Token: 0x0600FE34 RID: 65076
		// (set) Token: 0x0600FE33 RID: 65075
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

		// Token: 0x17005377 RID: 21367
		// (get) Token: 0x0600FE36 RID: 65078
		// (set) Token: 0x0600FE35 RID: 65077
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

		// Token: 0x17005378 RID: 21368
		// (get) Token: 0x0600FE38 RID: 65080
		// (set) Token: 0x0600FE37 RID: 65079
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

		// Token: 0x17005379 RID: 21369
		// (get) Token: 0x0600FE3A RID: 65082
		// (set) Token: 0x0600FE39 RID: 65081
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

		// Token: 0x1700537A RID: 21370
		// (get) Token: 0x0600FE3C RID: 65084
		// (set) Token: 0x0600FE3B RID: 65083
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

		// Token: 0x1700537B RID: 21371
		// (get) Token: 0x0600FE3E RID: 65086
		// (set) Token: 0x0600FE3D RID: 65085
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

		// Token: 0x1700537C RID: 21372
		// (get) Token: 0x0600FE40 RID: 65088
		// (set) Token: 0x0600FE3F RID: 65087
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

		// Token: 0x1700537D RID: 21373
		// (get) Token: 0x0600FE42 RID: 65090
		// (set) Token: 0x0600FE41 RID: 65089
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

		// Token: 0x1700537E RID: 21374
		// (get) Token: 0x0600FE43 RID: 65091
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700537F RID: 21375
		// (get) Token: 0x0600FE45 RID: 65093
		// (set) Token: 0x0600FE44 RID: 65092
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

		// Token: 0x0600FE46 RID: 65094
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600FE47 RID: 65095
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0600FE48 RID: 65096
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600FE49 RID: 65097
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600FE4A RID: 65098
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17005380 RID: 21376
		// (get) Token: 0x0600FE4C RID: 65100
		// (set) Token: 0x0600FE4B RID: 65099
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

		// Token: 0x0600FE4D RID: 65101
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17005381 RID: 21377
		// (get) Token: 0x0600FE4F RID: 65103
		// (set) Token: 0x0600FE4E RID: 65102
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

		// Token: 0x17005382 RID: 21378
		// (get) Token: 0x0600FE51 RID: 65105
		// (set) Token: 0x0600FE50 RID: 65104
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

		// Token: 0x17005383 RID: 21379
		// (get) Token: 0x0600FE53 RID: 65107
		// (set) Token: 0x0600FE52 RID: 65106
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

		// Token: 0x17005384 RID: 21380
		// (get) Token: 0x0600FE55 RID: 65109
		// (set) Token: 0x0600FE54 RID: 65108
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

		// Token: 0x0600FE56 RID: 65110
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600FE57 RID: 65111
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600FE58 RID: 65112
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17005385 RID: 21381
		// (get) Token: 0x0600FE59 RID: 65113
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005386 RID: 21382
		// (get) Token: 0x0600FE5A RID: 65114
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005387 RID: 21383
		// (get) Token: 0x0600FE5B RID: 65115
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005388 RID: 21384
		// (get) Token: 0x0600FE5C RID: 65116
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600FE5D RID: 65117
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600FE5E RID: 65118
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17005389 RID: 21385
		// (get) Token: 0x0600FE5F RID: 65119
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700538A RID: 21386
		// (get) Token: 0x0600FE61 RID: 65121
		// (set) Token: 0x0600FE60 RID: 65120
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

		// Token: 0x1700538B RID: 21387
		// (get) Token: 0x0600FE63 RID: 65123
		// (set) Token: 0x0600FE62 RID: 65122
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

		// Token: 0x1700538C RID: 21388
		// (get) Token: 0x0600FE65 RID: 65125
		// (set) Token: 0x0600FE64 RID: 65124
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

		// Token: 0x1700538D RID: 21389
		// (get) Token: 0x0600FE67 RID: 65127
		// (set) Token: 0x0600FE66 RID: 65126
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

		// Token: 0x1700538E RID: 21390
		// (get) Token: 0x0600FE69 RID: 65129
		// (set) Token: 0x0600FE68 RID: 65128
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

		// Token: 0x0600FE6A RID: 65130
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x1700538F RID: 21391
		// (get) Token: 0x0600FE6B RID: 65131
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005390 RID: 21392
		// (get) Token: 0x0600FE6C RID: 65132
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005391 RID: 21393
		// (get) Token: 0x0600FE6E RID: 65134
		// (set) Token: 0x0600FE6D RID: 65133
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

		// Token: 0x17005392 RID: 21394
		// (get) Token: 0x0600FE70 RID: 65136
		// (set) Token: 0x0600FE6F RID: 65135
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

		// Token: 0x0600FE71 RID: 65137
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600FE72 RID: 65138
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17005393 RID: 21395
		// (get) Token: 0x0600FE74 RID: 65140
		// (set) Token: 0x0600FE73 RID: 65139
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

		// Token: 0x0600FE75 RID: 65141
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600FE76 RID: 65142
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600FE77 RID: 65143
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600FE78 RID: 65144
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17005394 RID: 21396
		// (get) Token: 0x0600FE79 RID: 65145
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600FE7A RID: 65146
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600FE7B RID: 65147
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17005395 RID: 21397
		// (get) Token: 0x0600FE7C RID: 65148
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005396 RID: 21398
		// (get) Token: 0x0600FE7D RID: 65149
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005397 RID: 21399
		// (get) Token: 0x0600FE7F RID: 65151
		// (set) Token: 0x0600FE7E RID: 65150
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

		// Token: 0x17005398 RID: 21400
		// (get) Token: 0x0600FE81 RID: 65153
		// (set) Token: 0x0600FE80 RID: 65152
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

		// Token: 0x17005399 RID: 21401
		// (get) Token: 0x0600FE82 RID: 65154
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600FE83 RID: 65155
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600FE84 RID: 65156
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x1700539A RID: 21402
		// (get) Token: 0x0600FE85 RID: 65157
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700539B RID: 21403
		// (get) Token: 0x0600FE86 RID: 65158
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700539C RID: 21404
		// (get) Token: 0x0600FE88 RID: 65160
		// (set) Token: 0x0600FE87 RID: 65159
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

		// Token: 0x1700539D RID: 21405
		// (get) Token: 0x0600FE8A RID: 65162
		// (set) Token: 0x0600FE89 RID: 65161
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

		// Token: 0x1700539E RID: 21406
		// (get) Token: 0x0600FE8C RID: 65164
		// (set) Token: 0x0600FE8B RID: 65163
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

		// Token: 0x1700539F RID: 21407
		// (get) Token: 0x0600FE8E RID: 65166
		// (set) Token: 0x0600FE8D RID: 65165
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

		// Token: 0x0600FE8F RID: 65167
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x170053A0 RID: 21408
		// (get) Token: 0x0600FE91 RID: 65169
		// (set) Token: 0x0600FE90 RID: 65168
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

		// Token: 0x170053A1 RID: 21409
		// (get) Token: 0x0600FE92 RID: 65170
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170053A2 RID: 21410
		// (get) Token: 0x0600FE94 RID: 65172
		// (set) Token: 0x0600FE93 RID: 65171
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

		// Token: 0x170053A3 RID: 21411
		// (get) Token: 0x0600FE96 RID: 65174
		// (set) Token: 0x0600FE95 RID: 65173
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

		// Token: 0x170053A4 RID: 21412
		// (get) Token: 0x0600FE97 RID: 65175
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170053A5 RID: 21413
		// (get) Token: 0x0600FE99 RID: 65177
		// (set) Token: 0x0600FE98 RID: 65176
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

		// Token: 0x170053A6 RID: 21414
		// (get) Token: 0x0600FE9B RID: 65179
		// (set) Token: 0x0600FE9A RID: 65178
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

		// Token: 0x0600FE9C RID: 65180
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170053A7 RID: 21415
		// (get) Token: 0x0600FE9E RID: 65182
		// (set) Token: 0x0600FE9D RID: 65181
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

		// Token: 0x170053A8 RID: 21416
		// (get) Token: 0x0600FEA0 RID: 65184
		// (set) Token: 0x0600FE9F RID: 65183
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

		// Token: 0x170053A9 RID: 21417
		// (get) Token: 0x0600FEA2 RID: 65186
		// (set) Token: 0x0600FEA1 RID: 65185
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

		// Token: 0x170053AA RID: 21418
		// (get) Token: 0x0600FEA4 RID: 65188
		// (set) Token: 0x0600FEA3 RID: 65187
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

		// Token: 0x170053AB RID: 21419
		// (get) Token: 0x0600FEA6 RID: 65190
		// (set) Token: 0x0600FEA5 RID: 65189
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

		// Token: 0x170053AC RID: 21420
		// (get) Token: 0x0600FEA8 RID: 65192
		// (set) Token: 0x0600FEA7 RID: 65191
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

		// Token: 0x170053AD RID: 21421
		// (get) Token: 0x0600FEAA RID: 65194
		// (set) Token: 0x0600FEA9 RID: 65193
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

		// Token: 0x170053AE RID: 21422
		// (get) Token: 0x0600FEAC RID: 65196
		// (set) Token: 0x0600FEAB RID: 65195
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

		// Token: 0x0600FEAD RID: 65197
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x170053AF RID: 21423
		// (get) Token: 0x0600FEAE RID: 65198
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170053B0 RID: 21424
		// (get) Token: 0x0600FEB0 RID: 65200
		// (set) Token: 0x0600FEAF RID: 65199
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

		// Token: 0x0600FEB1 RID: 65201
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x0600FEB2 RID: 65202
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600FEB3 RID: 65203
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600FEB4 RID: 65204
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x170053B1 RID: 21425
		// (get) Token: 0x0600FEB6 RID: 65206
		// (set) Token: 0x0600FEB5 RID: 65205
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

		// Token: 0x170053B2 RID: 21426
		// (get) Token: 0x0600FEB8 RID: 65208
		// (set) Token: 0x0600FEB7 RID: 65207
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

		// Token: 0x170053B3 RID: 21427
		// (get) Token: 0x0600FEBA RID: 65210
		// (set) Token: 0x0600FEB9 RID: 65209
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

		// Token: 0x170053B4 RID: 21428
		// (get) Token: 0x0600FEBB RID: 65211
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170053B5 RID: 21429
		// (get) Token: 0x0600FEBC RID: 65212
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170053B6 RID: 21430
		// (get) Token: 0x0600FEBD RID: 65213
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170053B7 RID: 21431
		// (get) Token: 0x0600FEBE RID: 65214
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600FEBF RID: 65215
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x170053B8 RID: 21432
		// (get) Token: 0x0600FEC0 RID: 65216
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170053B9 RID: 21433
		// (get) Token: 0x0600FEC1 RID: 65217
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600FEC2 RID: 65218
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600FEC3 RID: 65219
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600FEC4 RID: 65220
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600FEC5 RID: 65221
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600FEC6 RID: 65222
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600FEC7 RID: 65223
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600FEC8 RID: 65224
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600FEC9 RID: 65225
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170053BA RID: 21434
		// (get) Token: 0x0600FECA RID: 65226
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170053BB RID: 21435
		// (get) Token: 0x0600FECC RID: 65228
		// (set) Token: 0x0600FECB RID: 65227
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

		// Token: 0x170053BC RID: 21436
		// (get) Token: 0x0600FECD RID: 65229
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170053BD RID: 21437
		// (get) Token: 0x0600FECE RID: 65230
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170053BE RID: 21438
		// (get) Token: 0x0600FECF RID: 65231
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170053BF RID: 21439
		// (get) Token: 0x0600FED0 RID: 65232
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170053C0 RID: 21440
		// (get) Token: 0x0600FED1 RID: 65233
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170053C1 RID: 21441
		// (get) Token: 0x0600FED3 RID: 65235
		// (set) Token: 0x0600FED2 RID: 65234
		public virtual extern string IHTMLPhraseElement2_cite
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

		// Token: 0x170053C2 RID: 21442
		// (get) Token: 0x0600FED5 RID: 65237
		// (set) Token: 0x0600FED4 RID: 65236
		public virtual extern string IHTMLPhraseElement2_dateTime
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

		// Token: 0x14001EF5 RID: 7925
		// (add) Token: 0x0600FED6 RID: 65238
		// (remove) Token: 0x0600FED7 RID: 65239
		public virtual extern event HTMLElementEvents_onhelpEventHandler HTMLElementEvents_Event_onhelp;

		// Token: 0x14001EF6 RID: 7926
		// (add) Token: 0x0600FED8 RID: 65240
		// (remove) Token: 0x0600FED9 RID: 65241
		public virtual extern event HTMLElementEvents_onclickEventHandler HTMLElementEvents_Event_onclick;

		// Token: 0x14001EF7 RID: 7927
		// (add) Token: 0x0600FEDA RID: 65242
		// (remove) Token: 0x0600FEDB RID: 65243
		public virtual extern event HTMLElementEvents_ondblclickEventHandler HTMLElementEvents_Event_ondblclick;

		// Token: 0x14001EF8 RID: 7928
		// (add) Token: 0x0600FEDC RID: 65244
		// (remove) Token: 0x0600FEDD RID: 65245
		public virtual extern event HTMLElementEvents_onkeypressEventHandler HTMLElementEvents_Event_onkeypress;

		// Token: 0x14001EF9 RID: 7929
		// (add) Token: 0x0600FEDE RID: 65246
		// (remove) Token: 0x0600FEDF RID: 65247
		public virtual extern event HTMLElementEvents_onkeydownEventHandler HTMLElementEvents_Event_onkeydown;

		// Token: 0x14001EFA RID: 7930
		// (add) Token: 0x0600FEE0 RID: 65248
		// (remove) Token: 0x0600FEE1 RID: 65249
		public virtual extern event HTMLElementEvents_onkeyupEventHandler HTMLElementEvents_Event_onkeyup;

		// Token: 0x14001EFB RID: 7931
		// (add) Token: 0x0600FEE2 RID: 65250
		// (remove) Token: 0x0600FEE3 RID: 65251
		public virtual extern event HTMLElementEvents_onmouseoutEventHandler HTMLElementEvents_Event_onmouseout;

		// Token: 0x14001EFC RID: 7932
		// (add) Token: 0x0600FEE4 RID: 65252
		// (remove) Token: 0x0600FEE5 RID: 65253
		public virtual extern event HTMLElementEvents_onmouseoverEventHandler HTMLElementEvents_Event_onmouseover;

		// Token: 0x14001EFD RID: 7933
		// (add) Token: 0x0600FEE6 RID: 65254
		// (remove) Token: 0x0600FEE7 RID: 65255
		public virtual extern event HTMLElementEvents_onmousemoveEventHandler HTMLElementEvents_Event_onmousemove;

		// Token: 0x14001EFE RID: 7934
		// (add) Token: 0x0600FEE8 RID: 65256
		// (remove) Token: 0x0600FEE9 RID: 65257
		public virtual extern event HTMLElementEvents_onmousedownEventHandler HTMLElementEvents_Event_onmousedown;

		// Token: 0x14001EFF RID: 7935
		// (add) Token: 0x0600FEEA RID: 65258
		// (remove) Token: 0x0600FEEB RID: 65259
		public virtual extern event HTMLElementEvents_onmouseupEventHandler HTMLElementEvents_Event_onmouseup;

		// Token: 0x14001F00 RID: 7936
		// (add) Token: 0x0600FEEC RID: 65260
		// (remove) Token: 0x0600FEED RID: 65261
		public virtual extern event HTMLElementEvents_onselectstartEventHandler HTMLElementEvents_Event_onselectstart;

		// Token: 0x14001F01 RID: 7937
		// (add) Token: 0x0600FEEE RID: 65262
		// (remove) Token: 0x0600FEEF RID: 65263
		public virtual extern event HTMLElementEvents_onfilterchangeEventHandler HTMLElementEvents_Event_onfilterchange;

		// Token: 0x14001F02 RID: 7938
		// (add) Token: 0x0600FEF0 RID: 65264
		// (remove) Token: 0x0600FEF1 RID: 65265
		public virtual extern event HTMLElementEvents_ondragstartEventHandler HTMLElementEvents_Event_ondragstart;

		// Token: 0x14001F03 RID: 7939
		// (add) Token: 0x0600FEF2 RID: 65266
		// (remove) Token: 0x0600FEF3 RID: 65267
		public virtual extern event HTMLElementEvents_onbeforeupdateEventHandler HTMLElementEvents_Event_onbeforeupdate;

		// Token: 0x14001F04 RID: 7940
		// (add) Token: 0x0600FEF4 RID: 65268
		// (remove) Token: 0x0600FEF5 RID: 65269
		public virtual extern event HTMLElementEvents_onafterupdateEventHandler HTMLElementEvents_Event_onafterupdate;

		// Token: 0x14001F05 RID: 7941
		// (add) Token: 0x0600FEF6 RID: 65270
		// (remove) Token: 0x0600FEF7 RID: 65271
		public virtual extern event HTMLElementEvents_onerrorupdateEventHandler HTMLElementEvents_Event_onerrorupdate;

		// Token: 0x14001F06 RID: 7942
		// (add) Token: 0x0600FEF8 RID: 65272
		// (remove) Token: 0x0600FEF9 RID: 65273
		public virtual extern event HTMLElementEvents_onrowexitEventHandler HTMLElementEvents_Event_onrowexit;

		// Token: 0x14001F07 RID: 7943
		// (add) Token: 0x0600FEFA RID: 65274
		// (remove) Token: 0x0600FEFB RID: 65275
		public virtual extern event HTMLElementEvents_onrowenterEventHandler HTMLElementEvents_Event_onrowenter;

		// Token: 0x14001F08 RID: 7944
		// (add) Token: 0x0600FEFC RID: 65276
		// (remove) Token: 0x0600FEFD RID: 65277
		public virtual extern event HTMLElementEvents_ondatasetchangedEventHandler HTMLElementEvents_Event_ondatasetchanged;

		// Token: 0x14001F09 RID: 7945
		// (add) Token: 0x0600FEFE RID: 65278
		// (remove) Token: 0x0600FEFF RID: 65279
		public virtual extern event HTMLElementEvents_ondataavailableEventHandler HTMLElementEvents_Event_ondataavailable;

		// Token: 0x14001F0A RID: 7946
		// (add) Token: 0x0600FF00 RID: 65280
		// (remove) Token: 0x0600FF01 RID: 65281
		public virtual extern event HTMLElementEvents_ondatasetcompleteEventHandler HTMLElementEvents_Event_ondatasetcomplete;

		// Token: 0x14001F0B RID: 7947
		// (add) Token: 0x0600FF02 RID: 65282
		// (remove) Token: 0x0600FF03 RID: 65283
		public virtual extern event HTMLElementEvents_onlosecaptureEventHandler HTMLElementEvents_Event_onlosecapture;

		// Token: 0x14001F0C RID: 7948
		// (add) Token: 0x0600FF04 RID: 65284
		// (remove) Token: 0x0600FF05 RID: 65285
		public virtual extern event HTMLElementEvents_onpropertychangeEventHandler HTMLElementEvents_Event_onpropertychange;

		// Token: 0x14001F0D RID: 7949
		// (add) Token: 0x0600FF06 RID: 65286
		// (remove) Token: 0x0600FF07 RID: 65287
		public virtual extern event HTMLElementEvents_onscrollEventHandler HTMLElementEvents_Event_onscroll;

		// Token: 0x14001F0E RID: 7950
		// (add) Token: 0x0600FF08 RID: 65288
		// (remove) Token: 0x0600FF09 RID: 65289
		public virtual extern event HTMLElementEvents_onfocusEventHandler HTMLElementEvents_Event_onfocus;

		// Token: 0x14001F0F RID: 7951
		// (add) Token: 0x0600FF0A RID: 65290
		// (remove) Token: 0x0600FF0B RID: 65291
		public virtual extern event HTMLElementEvents_onblurEventHandler HTMLElementEvents_Event_onblur;

		// Token: 0x14001F10 RID: 7952
		// (add) Token: 0x0600FF0C RID: 65292
		// (remove) Token: 0x0600FF0D RID: 65293
		public virtual extern event HTMLElementEvents_onresizeEventHandler HTMLElementEvents_Event_onresize;

		// Token: 0x14001F11 RID: 7953
		// (add) Token: 0x0600FF0E RID: 65294
		// (remove) Token: 0x0600FF0F RID: 65295
		public virtual extern event HTMLElementEvents_ondragEventHandler HTMLElementEvents_Event_ondrag;

		// Token: 0x14001F12 RID: 7954
		// (add) Token: 0x0600FF10 RID: 65296
		// (remove) Token: 0x0600FF11 RID: 65297
		public virtual extern event HTMLElementEvents_ondragendEventHandler HTMLElementEvents_Event_ondragend;

		// Token: 0x14001F13 RID: 7955
		// (add) Token: 0x0600FF12 RID: 65298
		// (remove) Token: 0x0600FF13 RID: 65299
		public virtual extern event HTMLElementEvents_ondragenterEventHandler HTMLElementEvents_Event_ondragenter;

		// Token: 0x14001F14 RID: 7956
		// (add) Token: 0x0600FF14 RID: 65300
		// (remove) Token: 0x0600FF15 RID: 65301
		public virtual extern event HTMLElementEvents_ondragoverEventHandler HTMLElementEvents_Event_ondragover;

		// Token: 0x14001F15 RID: 7957
		// (add) Token: 0x0600FF16 RID: 65302
		// (remove) Token: 0x0600FF17 RID: 65303
		public virtual extern event HTMLElementEvents_ondragleaveEventHandler HTMLElementEvents_Event_ondragleave;

		// Token: 0x14001F16 RID: 7958
		// (add) Token: 0x0600FF18 RID: 65304
		// (remove) Token: 0x0600FF19 RID: 65305
		public virtual extern event HTMLElementEvents_ondropEventHandler HTMLElementEvents_Event_ondrop;

		// Token: 0x14001F17 RID: 7959
		// (add) Token: 0x0600FF1A RID: 65306
		// (remove) Token: 0x0600FF1B RID: 65307
		public virtual extern event HTMLElementEvents_onbeforecutEventHandler HTMLElementEvents_Event_onbeforecut;

		// Token: 0x14001F18 RID: 7960
		// (add) Token: 0x0600FF1C RID: 65308
		// (remove) Token: 0x0600FF1D RID: 65309
		public virtual extern event HTMLElementEvents_oncutEventHandler HTMLElementEvents_Event_oncut;

		// Token: 0x14001F19 RID: 7961
		// (add) Token: 0x0600FF1E RID: 65310
		// (remove) Token: 0x0600FF1F RID: 65311
		public virtual extern event HTMLElementEvents_onbeforecopyEventHandler HTMLElementEvents_Event_onbeforecopy;

		// Token: 0x14001F1A RID: 7962
		// (add) Token: 0x0600FF20 RID: 65312
		// (remove) Token: 0x0600FF21 RID: 65313
		public virtual extern event HTMLElementEvents_oncopyEventHandler HTMLElementEvents_Event_oncopy;

		// Token: 0x14001F1B RID: 7963
		// (add) Token: 0x0600FF22 RID: 65314
		// (remove) Token: 0x0600FF23 RID: 65315
		public virtual extern event HTMLElementEvents_onbeforepasteEventHandler HTMLElementEvents_Event_onbeforepaste;

		// Token: 0x14001F1C RID: 7964
		// (add) Token: 0x0600FF24 RID: 65316
		// (remove) Token: 0x0600FF25 RID: 65317
		public virtual extern event HTMLElementEvents_onpasteEventHandler HTMLElementEvents_Event_onpaste;

		// Token: 0x14001F1D RID: 7965
		// (add) Token: 0x0600FF26 RID: 65318
		// (remove) Token: 0x0600FF27 RID: 65319
		public virtual extern event HTMLElementEvents_oncontextmenuEventHandler HTMLElementEvents_Event_oncontextmenu;

		// Token: 0x14001F1E RID: 7966
		// (add) Token: 0x0600FF28 RID: 65320
		// (remove) Token: 0x0600FF29 RID: 65321
		public virtual extern event HTMLElementEvents_onrowsdeleteEventHandler HTMLElementEvents_Event_onrowsdelete;

		// Token: 0x14001F1F RID: 7967
		// (add) Token: 0x0600FF2A RID: 65322
		// (remove) Token: 0x0600FF2B RID: 65323
		public virtual extern event HTMLElementEvents_onrowsinsertedEventHandler HTMLElementEvents_Event_onrowsinserted;

		// Token: 0x14001F20 RID: 7968
		// (add) Token: 0x0600FF2C RID: 65324
		// (remove) Token: 0x0600FF2D RID: 65325
		public virtual extern event HTMLElementEvents_oncellchangeEventHandler HTMLElementEvents_Event_oncellchange;

		// Token: 0x14001F21 RID: 7969
		// (add) Token: 0x0600FF2E RID: 65326
		// (remove) Token: 0x0600FF2F RID: 65327
		public virtual extern event HTMLElementEvents_onreadystatechangeEventHandler HTMLElementEvents_Event_onreadystatechange;

		// Token: 0x14001F22 RID: 7970
		// (add) Token: 0x0600FF30 RID: 65328
		// (remove) Token: 0x0600FF31 RID: 65329
		public virtual extern event HTMLElementEvents_onbeforeeditfocusEventHandler HTMLElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14001F23 RID: 7971
		// (add) Token: 0x0600FF32 RID: 65330
		// (remove) Token: 0x0600FF33 RID: 65331
		public virtual extern event HTMLElementEvents_onlayoutcompleteEventHandler HTMLElementEvents_Event_onlayoutcomplete;

		// Token: 0x14001F24 RID: 7972
		// (add) Token: 0x0600FF34 RID: 65332
		// (remove) Token: 0x0600FF35 RID: 65333
		public virtual extern event HTMLElementEvents_onpageEventHandler HTMLElementEvents_Event_onpage;

		// Token: 0x14001F25 RID: 7973
		// (add) Token: 0x0600FF36 RID: 65334
		// (remove) Token: 0x0600FF37 RID: 65335
		public virtual extern event HTMLElementEvents_onbeforedeactivateEventHandler HTMLElementEvents_Event_onbeforedeactivate;

		// Token: 0x14001F26 RID: 7974
		// (add) Token: 0x0600FF38 RID: 65336
		// (remove) Token: 0x0600FF39 RID: 65337
		public virtual extern event HTMLElementEvents_onbeforeactivateEventHandler HTMLElementEvents_Event_onbeforeactivate;

		// Token: 0x14001F27 RID: 7975
		// (add) Token: 0x0600FF3A RID: 65338
		// (remove) Token: 0x0600FF3B RID: 65339
		public virtual extern event HTMLElementEvents_onmoveEventHandler HTMLElementEvents_Event_onmove;

		// Token: 0x14001F28 RID: 7976
		// (add) Token: 0x0600FF3C RID: 65340
		// (remove) Token: 0x0600FF3D RID: 65341
		public virtual extern event HTMLElementEvents_oncontrolselectEventHandler HTMLElementEvents_Event_oncontrolselect;

		// Token: 0x14001F29 RID: 7977
		// (add) Token: 0x0600FF3E RID: 65342
		// (remove) Token: 0x0600FF3F RID: 65343
		public virtual extern event HTMLElementEvents_onmovestartEventHandler HTMLElementEvents_Event_onmovestart;

		// Token: 0x14001F2A RID: 7978
		// (add) Token: 0x0600FF40 RID: 65344
		// (remove) Token: 0x0600FF41 RID: 65345
		public virtual extern event HTMLElementEvents_onmoveendEventHandler HTMLElementEvents_Event_onmoveend;

		// Token: 0x14001F2B RID: 7979
		// (add) Token: 0x0600FF42 RID: 65346
		// (remove) Token: 0x0600FF43 RID: 65347
		public virtual extern event HTMLElementEvents_onresizestartEventHandler HTMLElementEvents_Event_onresizestart;

		// Token: 0x14001F2C RID: 7980
		// (add) Token: 0x0600FF44 RID: 65348
		// (remove) Token: 0x0600FF45 RID: 65349
		public virtual extern event HTMLElementEvents_onresizeendEventHandler HTMLElementEvents_Event_onresizeend;

		// Token: 0x14001F2D RID: 7981
		// (add) Token: 0x0600FF46 RID: 65350
		// (remove) Token: 0x0600FF47 RID: 65351
		public virtual extern event HTMLElementEvents_onmouseenterEventHandler HTMLElementEvents_Event_onmouseenter;

		// Token: 0x14001F2E RID: 7982
		// (add) Token: 0x0600FF48 RID: 65352
		// (remove) Token: 0x0600FF49 RID: 65353
		public virtual extern event HTMLElementEvents_onmouseleaveEventHandler HTMLElementEvents_Event_onmouseleave;

		// Token: 0x14001F2F RID: 7983
		// (add) Token: 0x0600FF4A RID: 65354
		// (remove) Token: 0x0600FF4B RID: 65355
		public virtual extern event HTMLElementEvents_onmousewheelEventHandler HTMLElementEvents_Event_onmousewheel;

		// Token: 0x14001F30 RID: 7984
		// (add) Token: 0x0600FF4C RID: 65356
		// (remove) Token: 0x0600FF4D RID: 65357
		public virtual extern event HTMLElementEvents_onactivateEventHandler HTMLElementEvents_Event_onactivate;

		// Token: 0x14001F31 RID: 7985
		// (add) Token: 0x0600FF4E RID: 65358
		// (remove) Token: 0x0600FF4F RID: 65359
		public virtual extern event HTMLElementEvents_ondeactivateEventHandler HTMLElementEvents_Event_ondeactivate;

		// Token: 0x14001F32 RID: 7986
		// (add) Token: 0x0600FF50 RID: 65360
		// (remove) Token: 0x0600FF51 RID: 65361
		public virtual extern event HTMLElementEvents_onfocusinEventHandler HTMLElementEvents_Event_onfocusin;

		// Token: 0x14001F33 RID: 7987
		// (add) Token: 0x0600FF52 RID: 65362
		// (remove) Token: 0x0600FF53 RID: 65363
		public virtual extern event HTMLElementEvents_onfocusoutEventHandler HTMLElementEvents_Event_onfocusout;
	}
}
