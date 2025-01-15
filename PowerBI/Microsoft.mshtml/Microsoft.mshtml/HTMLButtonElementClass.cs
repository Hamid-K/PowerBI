using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200069D RID: 1693
	[ClassInterface(0)]
	[TypeLibType(2)]
	[ComSourceInterfaces("mshtml.HTMLButtonElementEvents\0mshtml.HTMLButtonElementEvents2\0\0")]
	[Guid("3050F2C6-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLButtonElementClass : DispHTMLButtonElement, HTMLButtonElement, HTMLButtonElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLTextContainer, IHTMLButtonElement, HTMLButtonElementEvents2_Event
	{
		// Token: 0x0600A3D1 RID: 41937
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLButtonElementClass();

		// Token: 0x0600A3D2 RID: 41938
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600A3D3 RID: 41939
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600A3D4 RID: 41940
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700355B RID: 13659
		// (get) Token: 0x0600A3D6 RID: 41942
		// (set) Token: 0x0600A3D5 RID: 41941
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

		// Token: 0x1700355C RID: 13660
		// (get) Token: 0x0600A3D8 RID: 41944
		// (set) Token: 0x0600A3D7 RID: 41943
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

		// Token: 0x1700355D RID: 13661
		// (get) Token: 0x0600A3D9 RID: 41945
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700355E RID: 13662
		// (get) Token: 0x0600A3DA RID: 41946
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700355F RID: 13663
		// (get) Token: 0x0600A3DB RID: 41947
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003560 RID: 13664
		// (get) Token: 0x0600A3DD RID: 41949
		// (set) Token: 0x0600A3DC RID: 41948
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

		// Token: 0x17003561 RID: 13665
		// (get) Token: 0x0600A3DF RID: 41951
		// (set) Token: 0x0600A3DE RID: 41950
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

		// Token: 0x17003562 RID: 13666
		// (get) Token: 0x0600A3E1 RID: 41953
		// (set) Token: 0x0600A3E0 RID: 41952
		[DispId(-2147412103)]
		public virtual extern object ondblclick
		{
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003563 RID: 13667
		// (get) Token: 0x0600A3E3 RID: 41955
		// (set) Token: 0x0600A3E2 RID: 41954
		[DispId(-2147412107)]
		public virtual extern object onkeydown
		{
			[DispId(-2147412107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003564 RID: 13668
		// (get) Token: 0x0600A3E5 RID: 41957
		// (set) Token: 0x0600A3E4 RID: 41956
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

		// Token: 0x17003565 RID: 13669
		// (get) Token: 0x0600A3E7 RID: 41959
		// (set) Token: 0x0600A3E6 RID: 41958
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

		// Token: 0x17003566 RID: 13670
		// (get) Token: 0x0600A3E9 RID: 41961
		// (set) Token: 0x0600A3E8 RID: 41960
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

		// Token: 0x17003567 RID: 13671
		// (get) Token: 0x0600A3EB RID: 41963
		// (set) Token: 0x0600A3EA RID: 41962
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

		// Token: 0x17003568 RID: 13672
		// (get) Token: 0x0600A3ED RID: 41965
		// (set) Token: 0x0600A3EC RID: 41964
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

		// Token: 0x17003569 RID: 13673
		// (get) Token: 0x0600A3EF RID: 41967
		// (set) Token: 0x0600A3EE RID: 41966
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

		// Token: 0x1700356A RID: 13674
		// (get) Token: 0x0600A3F1 RID: 41969
		// (set) Token: 0x0600A3F0 RID: 41968
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

		// Token: 0x1700356B RID: 13675
		// (get) Token: 0x0600A3F2 RID: 41970
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700356C RID: 13676
		// (get) Token: 0x0600A3F4 RID: 41972
		// (set) Token: 0x0600A3F3 RID: 41971
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

		// Token: 0x1700356D RID: 13677
		// (get) Token: 0x0600A3F6 RID: 41974
		// (set) Token: 0x0600A3F5 RID: 41973
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

		// Token: 0x1700356E RID: 13678
		// (get) Token: 0x0600A3F8 RID: 41976
		// (set) Token: 0x0600A3F7 RID: 41975
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

		// Token: 0x0600A3F9 RID: 41977
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600A3FA RID: 41978
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x1700356F RID: 13679
		// (get) Token: 0x0600A3FB RID: 41979
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003570 RID: 13680
		// (get) Token: 0x0600A3FC RID: 41980
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003571 RID: 13681
		// (get) Token: 0x0600A3FE RID: 41982
		// (set) Token: 0x0600A3FD RID: 41981
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

		// Token: 0x17003572 RID: 13682
		// (get) Token: 0x0600A3FF RID: 41983
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003573 RID: 13683
		// (get) Token: 0x0600A400 RID: 41984
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003574 RID: 13684
		// (get) Token: 0x0600A401 RID: 41985
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003575 RID: 13685
		// (get) Token: 0x0600A402 RID: 41986
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003576 RID: 13686
		// (get) Token: 0x0600A403 RID: 41987
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003577 RID: 13687
		// (get) Token: 0x0600A405 RID: 41989
		// (set) Token: 0x0600A404 RID: 41988
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

		// Token: 0x17003578 RID: 13688
		// (get) Token: 0x0600A407 RID: 41991
		// (set) Token: 0x0600A406 RID: 41990
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

		// Token: 0x17003579 RID: 13689
		// (get) Token: 0x0600A409 RID: 41993
		// (set) Token: 0x0600A408 RID: 41992
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

		// Token: 0x1700357A RID: 13690
		// (get) Token: 0x0600A40B RID: 41995
		// (set) Token: 0x0600A40A RID: 41994
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

		// Token: 0x0600A40C RID: 41996
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600A40D RID: 41997
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x1700357B RID: 13691
		// (get) Token: 0x0600A40E RID: 41998
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700357C RID: 13692
		// (get) Token: 0x0600A40F RID: 41999
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A410 RID: 42000
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x1700357D RID: 13693
		// (get) Token: 0x0600A411 RID: 42001
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700357E RID: 13694
		// (get) Token: 0x0600A413 RID: 42003
		// (set) Token: 0x0600A412 RID: 42002
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

		// Token: 0x0600A414 RID: 42004
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x1700357F RID: 13695
		// (get) Token: 0x0600A416 RID: 42006
		// (set) Token: 0x0600A415 RID: 42005
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

		// Token: 0x17003580 RID: 13696
		// (get) Token: 0x0600A418 RID: 42008
		// (set) Token: 0x0600A417 RID: 42007
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

		// Token: 0x17003581 RID: 13697
		// (get) Token: 0x0600A41A RID: 42010
		// (set) Token: 0x0600A419 RID: 42009
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

		// Token: 0x17003582 RID: 13698
		// (get) Token: 0x0600A41C RID: 42012
		// (set) Token: 0x0600A41B RID: 42011
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

		// Token: 0x17003583 RID: 13699
		// (get) Token: 0x0600A41E RID: 42014
		// (set) Token: 0x0600A41D RID: 42013
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

		// Token: 0x17003584 RID: 13700
		// (get) Token: 0x0600A420 RID: 42016
		// (set) Token: 0x0600A41F RID: 42015
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

		// Token: 0x17003585 RID: 13701
		// (get) Token: 0x0600A422 RID: 42018
		// (set) Token: 0x0600A421 RID: 42017
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

		// Token: 0x17003586 RID: 13702
		// (get) Token: 0x0600A424 RID: 42020
		// (set) Token: 0x0600A423 RID: 42019
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

		// Token: 0x17003587 RID: 13703
		// (get) Token: 0x0600A426 RID: 42022
		// (set) Token: 0x0600A425 RID: 42021
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

		// Token: 0x17003588 RID: 13704
		// (get) Token: 0x0600A427 RID: 42023
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003589 RID: 13705
		// (get) Token: 0x0600A428 RID: 42024
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700358A RID: 13706
		// (get) Token: 0x0600A429 RID: 42025
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600A42A RID: 42026
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0600A42B RID: 42027
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x1700358B RID: 13707
		// (get) Token: 0x0600A42D RID: 42029
		// (set) Token: 0x0600A42C RID: 42028
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

		// Token: 0x0600A42E RID: 42030
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600A42F RID: 42031
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700358C RID: 13708
		// (get) Token: 0x0600A431 RID: 42033
		// (set) Token: 0x0600A430 RID: 42032
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700358D RID: 13709
		// (get) Token: 0x0600A433 RID: 42035
		// (set) Token: 0x0600A432 RID: 42034
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

		// Token: 0x1700358E RID: 13710
		// (get) Token: 0x0600A435 RID: 42037
		// (set) Token: 0x0600A434 RID: 42036
		[DispId(-2147412062)]
		public virtual extern object ondragend
		{
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700358F RID: 13711
		// (get) Token: 0x0600A437 RID: 42039
		// (set) Token: 0x0600A436 RID: 42038
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

		// Token: 0x17003590 RID: 13712
		// (get) Token: 0x0600A439 RID: 42041
		// (set) Token: 0x0600A438 RID: 42040
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

		// Token: 0x17003591 RID: 13713
		// (get) Token: 0x0600A43B RID: 42043
		// (set) Token: 0x0600A43A RID: 42042
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

		// Token: 0x17003592 RID: 13714
		// (get) Token: 0x0600A43D RID: 42045
		// (set) Token: 0x0600A43C RID: 42044
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

		// Token: 0x17003593 RID: 13715
		// (get) Token: 0x0600A43F RID: 42047
		// (set) Token: 0x0600A43E RID: 42046
		[DispId(-2147412054)]
		public virtual extern object onbeforecut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003594 RID: 13716
		// (get) Token: 0x0600A441 RID: 42049
		// (set) Token: 0x0600A440 RID: 42048
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

		// Token: 0x17003595 RID: 13717
		// (get) Token: 0x0600A443 RID: 42051
		// (set) Token: 0x0600A442 RID: 42050
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

		// Token: 0x17003596 RID: 13718
		// (get) Token: 0x0600A445 RID: 42053
		// (set) Token: 0x0600A444 RID: 42052
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

		// Token: 0x17003597 RID: 13719
		// (get) Token: 0x0600A447 RID: 42055
		// (set) Token: 0x0600A446 RID: 42054
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

		// Token: 0x17003598 RID: 13720
		// (get) Token: 0x0600A449 RID: 42057
		// (set) Token: 0x0600A448 RID: 42056
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

		// Token: 0x17003599 RID: 13721
		// (get) Token: 0x0600A44A RID: 42058
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700359A RID: 13722
		// (get) Token: 0x0600A44C RID: 42060
		// (set) Token: 0x0600A44B RID: 42059
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

		// Token: 0x0600A44D RID: 42061
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x0600A44E RID: 42062
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x0600A44F RID: 42063
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600A450 RID: 42064
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600A451 RID: 42065
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700359B RID: 13723
		// (get) Token: 0x0600A453 RID: 42067
		// (set) Token: 0x0600A452 RID: 42066
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

		// Token: 0x0600A454 RID: 42068
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x1700359C RID: 13724
		// (get) Token: 0x0600A456 RID: 42070
		// (set) Token: 0x0600A455 RID: 42069
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

		// Token: 0x1700359D RID: 13725
		// (get) Token: 0x0600A458 RID: 42072
		// (set) Token: 0x0600A457 RID: 42071
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

		// Token: 0x1700359E RID: 13726
		// (get) Token: 0x0600A45A RID: 42074
		// (set) Token: 0x0600A459 RID: 42073
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

		// Token: 0x1700359F RID: 13727
		// (get) Token: 0x0600A45C RID: 42076
		// (set) Token: 0x0600A45B RID: 42075
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

		// Token: 0x0600A45D RID: 42077
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600A45E RID: 42078
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600A45F RID: 42079
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170035A0 RID: 13728
		// (get) Token: 0x0600A460 RID: 42080
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035A1 RID: 13729
		// (get) Token: 0x0600A461 RID: 42081
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035A2 RID: 13730
		// (get) Token: 0x0600A462 RID: 42082
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035A3 RID: 13731
		// (get) Token: 0x0600A463 RID: 42083
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A464 RID: 42084
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600A465 RID: 42085
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170035A4 RID: 13732
		// (get) Token: 0x0600A466 RID: 42086
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170035A5 RID: 13733
		// (get) Token: 0x0600A468 RID: 42088
		// (set) Token: 0x0600A467 RID: 42087
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

		// Token: 0x170035A6 RID: 13734
		// (get) Token: 0x0600A46A RID: 42090
		// (set) Token: 0x0600A469 RID: 42089
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

		// Token: 0x170035A7 RID: 13735
		// (get) Token: 0x0600A46C RID: 42092
		// (set) Token: 0x0600A46B RID: 42091
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

		// Token: 0x170035A8 RID: 13736
		// (get) Token: 0x0600A46E RID: 42094
		// (set) Token: 0x0600A46D RID: 42093
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

		// Token: 0x170035A9 RID: 13737
		// (get) Token: 0x0600A470 RID: 42096
		// (set) Token: 0x0600A46F RID: 42095
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

		// Token: 0x0600A471 RID: 42097
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x170035AA RID: 13738
		// (get) Token: 0x0600A472 RID: 42098
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035AB RID: 13739
		// (get) Token: 0x0600A473 RID: 42099
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035AC RID: 13740
		// (get) Token: 0x0600A475 RID: 42101
		// (set) Token: 0x0600A474 RID: 42100
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

		// Token: 0x170035AD RID: 13741
		// (get) Token: 0x0600A477 RID: 42103
		// (set) Token: 0x0600A476 RID: 42102
		[DispId(-2147417052)]
		public virtual extern int scrollLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600A478 RID: 42104
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x170035AE RID: 13742
		// (get) Token: 0x0600A47A RID: 42106
		// (set) Token: 0x0600A479 RID: 42105
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

		// Token: 0x0600A47B RID: 42107
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600A47C RID: 42108
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600A47D RID: 42109
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600A47E RID: 42110
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170035AF RID: 13743
		// (get) Token: 0x0600A47F RID: 42111
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A480 RID: 42112
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600A481 RID: 42113
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x170035B0 RID: 13744
		// (get) Token: 0x0600A482 RID: 42114
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170035B1 RID: 13745
		// (get) Token: 0x0600A483 RID: 42115
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170035B2 RID: 13746
		// (get) Token: 0x0600A485 RID: 42117
		// (set) Token: 0x0600A484 RID: 42116
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

		// Token: 0x170035B3 RID: 13747
		// (get) Token: 0x0600A487 RID: 42119
		// (set) Token: 0x0600A486 RID: 42118
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

		// Token: 0x170035B4 RID: 13748
		// (get) Token: 0x0600A488 RID: 42120
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A489 RID: 42121
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600A48A RID: 42122
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170035B5 RID: 13749
		// (get) Token: 0x0600A48B RID: 42123
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035B6 RID: 13750
		// (get) Token: 0x0600A48C RID: 42124
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035B7 RID: 13751
		// (get) Token: 0x0600A48E RID: 42126
		// (set) Token: 0x0600A48D RID: 42125
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

		// Token: 0x170035B8 RID: 13752
		// (get) Token: 0x0600A490 RID: 42128
		// (set) Token: 0x0600A48F RID: 42127
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

		// Token: 0x170035B9 RID: 13753
		// (get) Token: 0x0600A492 RID: 42130
		// (set) Token: 0x0600A491 RID: 42129
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

		// Token: 0x170035BA RID: 13754
		// (get) Token: 0x0600A494 RID: 42132
		// (set) Token: 0x0600A493 RID: 42131
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

		// Token: 0x0600A495 RID: 42133
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x170035BB RID: 13755
		// (get) Token: 0x0600A497 RID: 42135
		// (set) Token: 0x0600A496 RID: 42134
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

		// Token: 0x170035BC RID: 13756
		// (get) Token: 0x0600A498 RID: 42136
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035BD RID: 13757
		// (get) Token: 0x0600A49A RID: 42138
		// (set) Token: 0x0600A499 RID: 42137
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

		// Token: 0x170035BE RID: 13758
		// (get) Token: 0x0600A49C RID: 42140
		// (set) Token: 0x0600A49B RID: 42139
		[DispId(-2147418036)]
		public virtual extern bool disabled
		{
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170035BF RID: 13759
		// (get) Token: 0x0600A49D RID: 42141
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035C0 RID: 13760
		// (get) Token: 0x0600A49F RID: 42143
		// (set) Token: 0x0600A49E RID: 42142
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

		// Token: 0x170035C1 RID: 13761
		// (get) Token: 0x0600A4A1 RID: 42145
		// (set) Token: 0x0600A4A0 RID: 42144
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

		// Token: 0x0600A4A2 RID: 42146
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170035C2 RID: 13762
		// (get) Token: 0x0600A4A4 RID: 42148
		// (set) Token: 0x0600A4A3 RID: 42147
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

		// Token: 0x170035C3 RID: 13763
		// (get) Token: 0x0600A4A6 RID: 42150
		// (set) Token: 0x0600A4A5 RID: 42149
		[DispId(-2147412028)]
		public virtual extern object onresizeend
		{
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170035C4 RID: 13764
		// (get) Token: 0x0600A4A8 RID: 42152
		// (set) Token: 0x0600A4A7 RID: 42151
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

		// Token: 0x170035C5 RID: 13765
		// (get) Token: 0x0600A4AA RID: 42154
		// (set) Token: 0x0600A4A9 RID: 42153
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

		// Token: 0x170035C6 RID: 13766
		// (get) Token: 0x0600A4AC RID: 42156
		// (set) Token: 0x0600A4AB RID: 42155
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

		// Token: 0x170035C7 RID: 13767
		// (get) Token: 0x0600A4AE RID: 42158
		// (set) Token: 0x0600A4AD RID: 42157
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

		// Token: 0x170035C8 RID: 13768
		// (get) Token: 0x0600A4B0 RID: 42160
		// (set) Token: 0x0600A4AF RID: 42159
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

		// Token: 0x170035C9 RID: 13769
		// (get) Token: 0x0600A4B2 RID: 42162
		// (set) Token: 0x0600A4B1 RID: 42161
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

		// Token: 0x0600A4B3 RID: 42163
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x170035CA RID: 13770
		// (get) Token: 0x0600A4B4 RID: 42164
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035CB RID: 13771
		// (get) Token: 0x0600A4B6 RID: 42166
		// (set) Token: 0x0600A4B5 RID: 42165
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

		// Token: 0x0600A4B7 RID: 42167
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600A4B8 RID: 42168
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600A4B9 RID: 42169
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600A4BA RID: 42170
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x170035CC RID: 13772
		// (get) Token: 0x0600A4BC RID: 42172
		// (set) Token: 0x0600A4BB RID: 42171
		[DispId(-2147412022)]
		public virtual extern object onbeforeactivate
		{
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170035CD RID: 13773
		// (get) Token: 0x0600A4BE RID: 42174
		// (set) Token: 0x0600A4BD RID: 42173
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

		// Token: 0x170035CE RID: 13774
		// (get) Token: 0x0600A4C0 RID: 42176
		// (set) Token: 0x0600A4BF RID: 42175
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

		// Token: 0x170035CF RID: 13775
		// (get) Token: 0x0600A4C1 RID: 42177
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035D0 RID: 13776
		// (get) Token: 0x0600A4C2 RID: 42178
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170035D1 RID: 13777
		// (get) Token: 0x0600A4C3 RID: 42179
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035D2 RID: 13778
		// (get) Token: 0x0600A4C4 RID: 42180
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600A4C5 RID: 42181
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x170035D3 RID: 13779
		// (get) Token: 0x0600A4C6 RID: 42182
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170035D4 RID: 13780
		// (get) Token: 0x0600A4C7 RID: 42183
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600A4C8 RID: 42184
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600A4C9 RID: 42185
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600A4CA RID: 42186
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600A4CB RID: 42187
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600A4CC RID: 42188
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600A4CD RID: 42189
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600A4CE RID: 42190
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600A4CF RID: 42191
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170035D5 RID: 13781
		// (get) Token: 0x0600A4D0 RID: 42192
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170035D6 RID: 13782
		// (get) Token: 0x0600A4D2 RID: 42194
		// (set) Token: 0x0600A4D1 RID: 42193
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

		// Token: 0x170035D7 RID: 13783
		// (get) Token: 0x0600A4D3 RID: 42195
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170035D8 RID: 13784
		// (get) Token: 0x0600A4D4 RID: 42196
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170035D9 RID: 13785
		// (get) Token: 0x0600A4D5 RID: 42197
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170035DA RID: 13786
		// (get) Token: 0x0600A4D6 RID: 42198
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170035DB RID: 13787
		// (get) Token: 0x0600A4D7 RID: 42199
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170035DC RID: 13788
		// (get) Token: 0x0600A4D9 RID: 42201
		// (set) Token: 0x0600A4D8 RID: 42200
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

		// Token: 0x170035DD RID: 13789
		// (get) Token: 0x0600A4DB RID: 42203
		// (set) Token: 0x0600A4DA RID: 42202
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

		// Token: 0x170035DE RID: 13790
		// (get) Token: 0x0600A4DD RID: 42205
		// (set) Token: 0x0600A4DC RID: 42204
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

		// Token: 0x170035DF RID: 13791
		// (get) Token: 0x0600A4DE RID: 42206
		[DispId(2000)]
		public virtual extern string type
		{
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170035E0 RID: 13792
		// (get) Token: 0x0600A4E0 RID: 42208
		// (set) Token: 0x0600A4DF RID: 42207
		[DispId(-2147413011)]
		public virtual extern string value
		{
			[TypeLibFunc(20)]
			[DispId(-2147413011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170035E1 RID: 13793
		// (get) Token: 0x0600A4E2 RID: 42210
		// (set) Token: 0x0600A4E1 RID: 42209
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

		// Token: 0x170035E2 RID: 13794
		// (get) Token: 0x0600A4E4 RID: 42212
		// (set) Token: 0x0600A4E3 RID: 42211
		[DispId(8001)]
		public virtual extern object status
		{
			[DispId(8001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(8001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170035E3 RID: 13795
		// (get) Token: 0x0600A4E5 RID: 42213
		[DispId(-2147416108)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600A4E6 RID: 42214
		[DispId(8002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange createTextRange();

		// Token: 0x0600A4E7 RID: 42215
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600A4E8 RID: 42216
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600A4E9 RID: 42217
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170035E4 RID: 13796
		// (get) Token: 0x0600A4EB RID: 42219
		// (set) Token: 0x0600A4EA RID: 42218
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

		// Token: 0x170035E5 RID: 13797
		// (get) Token: 0x0600A4ED RID: 42221
		// (set) Token: 0x0600A4EC RID: 42220
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

		// Token: 0x170035E6 RID: 13798
		// (get) Token: 0x0600A4EE RID: 42222
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170035E7 RID: 13799
		// (get) Token: 0x0600A4EF RID: 42223
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170035E8 RID: 13800
		// (get) Token: 0x0600A4F0 RID: 42224
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170035E9 RID: 13801
		// (get) Token: 0x0600A4F2 RID: 42226
		// (set) Token: 0x0600A4F1 RID: 42225
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

		// Token: 0x170035EA RID: 13802
		// (get) Token: 0x0600A4F4 RID: 42228
		// (set) Token: 0x0600A4F3 RID: 42227
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

		// Token: 0x170035EB RID: 13803
		// (get) Token: 0x0600A4F6 RID: 42230
		// (set) Token: 0x0600A4F5 RID: 42229
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

		// Token: 0x170035EC RID: 13804
		// (get) Token: 0x0600A4F8 RID: 42232
		// (set) Token: 0x0600A4F7 RID: 42231
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

		// Token: 0x170035ED RID: 13805
		// (get) Token: 0x0600A4FA RID: 42234
		// (set) Token: 0x0600A4F9 RID: 42233
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

		// Token: 0x170035EE RID: 13806
		// (get) Token: 0x0600A4FC RID: 42236
		// (set) Token: 0x0600A4FB RID: 42235
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

		// Token: 0x170035EF RID: 13807
		// (get) Token: 0x0600A4FE RID: 42238
		// (set) Token: 0x0600A4FD RID: 42237
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

		// Token: 0x170035F0 RID: 13808
		// (get) Token: 0x0600A500 RID: 42240
		// (set) Token: 0x0600A4FF RID: 42239
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

		// Token: 0x170035F1 RID: 13809
		// (get) Token: 0x0600A502 RID: 42242
		// (set) Token: 0x0600A501 RID: 42241
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

		// Token: 0x170035F2 RID: 13810
		// (get) Token: 0x0600A504 RID: 42244
		// (set) Token: 0x0600A503 RID: 42243
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

		// Token: 0x170035F3 RID: 13811
		// (get) Token: 0x0600A506 RID: 42246
		// (set) Token: 0x0600A505 RID: 42245
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

		// Token: 0x170035F4 RID: 13812
		// (get) Token: 0x0600A507 RID: 42247
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170035F5 RID: 13813
		// (get) Token: 0x0600A509 RID: 42249
		// (set) Token: 0x0600A508 RID: 42248
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

		// Token: 0x170035F6 RID: 13814
		// (get) Token: 0x0600A50B RID: 42251
		// (set) Token: 0x0600A50A RID: 42250
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

		// Token: 0x170035F7 RID: 13815
		// (get) Token: 0x0600A50D RID: 42253
		// (set) Token: 0x0600A50C RID: 42252
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

		// Token: 0x0600A50E RID: 42254
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600A50F RID: 42255
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170035F8 RID: 13816
		// (get) Token: 0x0600A510 RID: 42256
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035F9 RID: 13817
		// (get) Token: 0x0600A511 RID: 42257
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170035FA RID: 13818
		// (get) Token: 0x0600A513 RID: 42259
		// (set) Token: 0x0600A512 RID: 42258
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

		// Token: 0x170035FB RID: 13819
		// (get) Token: 0x0600A514 RID: 42260
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035FC RID: 13820
		// (get) Token: 0x0600A515 RID: 42261
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035FD RID: 13821
		// (get) Token: 0x0600A516 RID: 42262
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035FE RID: 13822
		// (get) Token: 0x0600A517 RID: 42263
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170035FF RID: 13823
		// (get) Token: 0x0600A518 RID: 42264
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003600 RID: 13824
		// (get) Token: 0x0600A51A RID: 42266
		// (set) Token: 0x0600A519 RID: 42265
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

		// Token: 0x17003601 RID: 13825
		// (get) Token: 0x0600A51C RID: 42268
		// (set) Token: 0x0600A51B RID: 42267
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

		// Token: 0x17003602 RID: 13826
		// (get) Token: 0x0600A51E RID: 42270
		// (set) Token: 0x0600A51D RID: 42269
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

		// Token: 0x17003603 RID: 13827
		// (get) Token: 0x0600A520 RID: 42272
		// (set) Token: 0x0600A51F RID: 42271
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

		// Token: 0x0600A521 RID: 42273
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600A522 RID: 42274
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17003604 RID: 13828
		// (get) Token: 0x0600A523 RID: 42275
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003605 RID: 13829
		// (get) Token: 0x0600A524 RID: 42276
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A525 RID: 42277
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17003606 RID: 13830
		// (get) Token: 0x0600A526 RID: 42278
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003607 RID: 13831
		// (get) Token: 0x0600A528 RID: 42280
		// (set) Token: 0x0600A527 RID: 42279
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

		// Token: 0x0600A529 RID: 42281
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17003608 RID: 13832
		// (get) Token: 0x0600A52B RID: 42283
		// (set) Token: 0x0600A52A RID: 42282
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

		// Token: 0x17003609 RID: 13833
		// (get) Token: 0x0600A52D RID: 42285
		// (set) Token: 0x0600A52C RID: 42284
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

		// Token: 0x1700360A RID: 13834
		// (get) Token: 0x0600A52F RID: 42287
		// (set) Token: 0x0600A52E RID: 42286
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

		// Token: 0x1700360B RID: 13835
		// (get) Token: 0x0600A531 RID: 42289
		// (set) Token: 0x0600A530 RID: 42288
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

		// Token: 0x1700360C RID: 13836
		// (get) Token: 0x0600A533 RID: 42291
		// (set) Token: 0x0600A532 RID: 42290
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

		// Token: 0x1700360D RID: 13837
		// (get) Token: 0x0600A535 RID: 42293
		// (set) Token: 0x0600A534 RID: 42292
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

		// Token: 0x1700360E RID: 13838
		// (get) Token: 0x0600A537 RID: 42295
		// (set) Token: 0x0600A536 RID: 42294
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

		// Token: 0x1700360F RID: 13839
		// (get) Token: 0x0600A539 RID: 42297
		// (set) Token: 0x0600A538 RID: 42296
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

		// Token: 0x17003610 RID: 13840
		// (get) Token: 0x0600A53B RID: 42299
		// (set) Token: 0x0600A53A RID: 42298
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

		// Token: 0x17003611 RID: 13841
		// (get) Token: 0x0600A53C RID: 42300
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003612 RID: 13842
		// (get) Token: 0x0600A53D RID: 42301
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003613 RID: 13843
		// (get) Token: 0x0600A53E RID: 42302
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600A53F RID: 42303
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600A540 RID: 42304
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17003614 RID: 13844
		// (get) Token: 0x0600A542 RID: 42306
		// (set) Token: 0x0600A541 RID: 42305
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

		// Token: 0x0600A543 RID: 42307
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600A544 RID: 42308
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17003615 RID: 13845
		// (get) Token: 0x0600A546 RID: 42310
		// (set) Token: 0x0600A545 RID: 42309
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

		// Token: 0x17003616 RID: 13846
		// (get) Token: 0x0600A548 RID: 42312
		// (set) Token: 0x0600A547 RID: 42311
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

		// Token: 0x17003617 RID: 13847
		// (get) Token: 0x0600A54A RID: 42314
		// (set) Token: 0x0600A549 RID: 42313
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

		// Token: 0x17003618 RID: 13848
		// (get) Token: 0x0600A54C RID: 42316
		// (set) Token: 0x0600A54B RID: 42315
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

		// Token: 0x17003619 RID: 13849
		// (get) Token: 0x0600A54E RID: 42318
		// (set) Token: 0x0600A54D RID: 42317
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

		// Token: 0x1700361A RID: 13850
		// (get) Token: 0x0600A550 RID: 42320
		// (set) Token: 0x0600A54F RID: 42319
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

		// Token: 0x1700361B RID: 13851
		// (get) Token: 0x0600A552 RID: 42322
		// (set) Token: 0x0600A551 RID: 42321
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

		// Token: 0x1700361C RID: 13852
		// (get) Token: 0x0600A554 RID: 42324
		// (set) Token: 0x0600A553 RID: 42323
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

		// Token: 0x1700361D RID: 13853
		// (get) Token: 0x0600A556 RID: 42326
		// (set) Token: 0x0600A555 RID: 42325
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

		// Token: 0x1700361E RID: 13854
		// (get) Token: 0x0600A558 RID: 42328
		// (set) Token: 0x0600A557 RID: 42327
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

		// Token: 0x1700361F RID: 13855
		// (get) Token: 0x0600A55A RID: 42330
		// (set) Token: 0x0600A559 RID: 42329
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

		// Token: 0x17003620 RID: 13856
		// (get) Token: 0x0600A55C RID: 42332
		// (set) Token: 0x0600A55B RID: 42331
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

		// Token: 0x17003621 RID: 13857
		// (get) Token: 0x0600A55E RID: 42334
		// (set) Token: 0x0600A55D RID: 42333
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

		// Token: 0x17003622 RID: 13858
		// (get) Token: 0x0600A55F RID: 42335
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003623 RID: 13859
		// (get) Token: 0x0600A561 RID: 42337
		// (set) Token: 0x0600A560 RID: 42336
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

		// Token: 0x0600A562 RID: 42338
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600A563 RID: 42339
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0600A564 RID: 42340
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600A565 RID: 42341
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600A566 RID: 42342
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17003624 RID: 13860
		// (get) Token: 0x0600A568 RID: 42344
		// (set) Token: 0x0600A567 RID: 42343
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

		// Token: 0x0600A569 RID: 42345
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17003625 RID: 13861
		// (get) Token: 0x0600A56B RID: 42347
		// (set) Token: 0x0600A56A RID: 42346
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

		// Token: 0x17003626 RID: 13862
		// (get) Token: 0x0600A56D RID: 42349
		// (set) Token: 0x0600A56C RID: 42348
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

		// Token: 0x17003627 RID: 13863
		// (get) Token: 0x0600A56F RID: 42351
		// (set) Token: 0x0600A56E RID: 42350
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

		// Token: 0x17003628 RID: 13864
		// (get) Token: 0x0600A571 RID: 42353
		// (set) Token: 0x0600A570 RID: 42352
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

		// Token: 0x0600A572 RID: 42354
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600A573 RID: 42355
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600A574 RID: 42356
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17003629 RID: 13865
		// (get) Token: 0x0600A575 RID: 42357
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700362A RID: 13866
		// (get) Token: 0x0600A576 RID: 42358
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700362B RID: 13867
		// (get) Token: 0x0600A577 RID: 42359
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700362C RID: 13868
		// (get) Token: 0x0600A578 RID: 42360
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A579 RID: 42361
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600A57A RID: 42362
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700362D RID: 13869
		// (get) Token: 0x0600A57B RID: 42363
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700362E RID: 13870
		// (get) Token: 0x0600A57D RID: 42365
		// (set) Token: 0x0600A57C RID: 42364
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

		// Token: 0x1700362F RID: 13871
		// (get) Token: 0x0600A57F RID: 42367
		// (set) Token: 0x0600A57E RID: 42366
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

		// Token: 0x17003630 RID: 13872
		// (get) Token: 0x0600A581 RID: 42369
		// (set) Token: 0x0600A580 RID: 42368
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

		// Token: 0x17003631 RID: 13873
		// (get) Token: 0x0600A583 RID: 42371
		// (set) Token: 0x0600A582 RID: 42370
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

		// Token: 0x17003632 RID: 13874
		// (get) Token: 0x0600A585 RID: 42373
		// (set) Token: 0x0600A584 RID: 42372
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

		// Token: 0x0600A586 RID: 42374
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17003633 RID: 13875
		// (get) Token: 0x0600A587 RID: 42375
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003634 RID: 13876
		// (get) Token: 0x0600A588 RID: 42376
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003635 RID: 13877
		// (get) Token: 0x0600A58A RID: 42378
		// (set) Token: 0x0600A589 RID: 42377
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

		// Token: 0x17003636 RID: 13878
		// (get) Token: 0x0600A58C RID: 42380
		// (set) Token: 0x0600A58B RID: 42379
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

		// Token: 0x0600A58D RID: 42381
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600A58E RID: 42382
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17003637 RID: 13879
		// (get) Token: 0x0600A590 RID: 42384
		// (set) Token: 0x0600A58F RID: 42383
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

		// Token: 0x0600A591 RID: 42385
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600A592 RID: 42386
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600A593 RID: 42387
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600A594 RID: 42388
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17003638 RID: 13880
		// (get) Token: 0x0600A595 RID: 42389
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A596 RID: 42390
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600A597 RID: 42391
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17003639 RID: 13881
		// (get) Token: 0x0600A598 RID: 42392
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700363A RID: 13882
		// (get) Token: 0x0600A599 RID: 42393
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700363B RID: 13883
		// (get) Token: 0x0600A59B RID: 42395
		// (set) Token: 0x0600A59A RID: 42394
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

		// Token: 0x1700363C RID: 13884
		// (get) Token: 0x0600A59D RID: 42397
		// (set) Token: 0x0600A59C RID: 42396
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

		// Token: 0x1700363D RID: 13885
		// (get) Token: 0x0600A59E RID: 42398
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A59F RID: 42399
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600A5A0 RID: 42400
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x1700363E RID: 13886
		// (get) Token: 0x0600A5A1 RID: 42401
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700363F RID: 13887
		// (get) Token: 0x0600A5A2 RID: 42402
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003640 RID: 13888
		// (get) Token: 0x0600A5A4 RID: 42404
		// (set) Token: 0x0600A5A3 RID: 42403
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

		// Token: 0x17003641 RID: 13889
		// (get) Token: 0x0600A5A6 RID: 42406
		// (set) Token: 0x0600A5A5 RID: 42405
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

		// Token: 0x17003642 RID: 13890
		// (get) Token: 0x0600A5A8 RID: 42408
		// (set) Token: 0x0600A5A7 RID: 42407
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

		// Token: 0x17003643 RID: 13891
		// (get) Token: 0x0600A5AA RID: 42410
		// (set) Token: 0x0600A5A9 RID: 42409
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

		// Token: 0x0600A5AB RID: 42411
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17003644 RID: 13892
		// (get) Token: 0x0600A5AD RID: 42413
		// (set) Token: 0x0600A5AC RID: 42412
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

		// Token: 0x17003645 RID: 13893
		// (get) Token: 0x0600A5AE RID: 42414
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003646 RID: 13894
		// (get) Token: 0x0600A5B0 RID: 42416
		// (set) Token: 0x0600A5AF RID: 42415
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

		// Token: 0x17003647 RID: 13895
		// (get) Token: 0x0600A5B2 RID: 42418
		// (set) Token: 0x0600A5B1 RID: 42417
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

		// Token: 0x17003648 RID: 13896
		// (get) Token: 0x0600A5B3 RID: 42419
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003649 RID: 13897
		// (get) Token: 0x0600A5B5 RID: 42421
		// (set) Token: 0x0600A5B4 RID: 42420
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

		// Token: 0x1700364A RID: 13898
		// (get) Token: 0x0600A5B7 RID: 42423
		// (set) Token: 0x0600A5B6 RID: 42422
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

		// Token: 0x0600A5B8 RID: 42424
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x1700364B RID: 13899
		// (get) Token: 0x0600A5BA RID: 42426
		// (set) Token: 0x0600A5B9 RID: 42425
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

		// Token: 0x1700364C RID: 13900
		// (get) Token: 0x0600A5BC RID: 42428
		// (set) Token: 0x0600A5BB RID: 42427
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

		// Token: 0x1700364D RID: 13901
		// (get) Token: 0x0600A5BE RID: 42430
		// (set) Token: 0x0600A5BD RID: 42429
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

		// Token: 0x1700364E RID: 13902
		// (get) Token: 0x0600A5C0 RID: 42432
		// (set) Token: 0x0600A5BF RID: 42431
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

		// Token: 0x1700364F RID: 13903
		// (get) Token: 0x0600A5C2 RID: 42434
		// (set) Token: 0x0600A5C1 RID: 42433
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

		// Token: 0x17003650 RID: 13904
		// (get) Token: 0x0600A5C4 RID: 42436
		// (set) Token: 0x0600A5C3 RID: 42435
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

		// Token: 0x17003651 RID: 13905
		// (get) Token: 0x0600A5C6 RID: 42438
		// (set) Token: 0x0600A5C5 RID: 42437
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

		// Token: 0x17003652 RID: 13906
		// (get) Token: 0x0600A5C8 RID: 42440
		// (set) Token: 0x0600A5C7 RID: 42439
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

		// Token: 0x0600A5C9 RID: 42441
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17003653 RID: 13907
		// (get) Token: 0x0600A5CA RID: 42442
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003654 RID: 13908
		// (get) Token: 0x0600A5CC RID: 42444
		// (set) Token: 0x0600A5CB RID: 42443
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

		// Token: 0x0600A5CD RID: 42445
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x0600A5CE RID: 42446
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600A5CF RID: 42447
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600A5D0 RID: 42448
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17003655 RID: 13909
		// (get) Token: 0x0600A5D2 RID: 42450
		// (set) Token: 0x0600A5D1 RID: 42449
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

		// Token: 0x17003656 RID: 13910
		// (get) Token: 0x0600A5D4 RID: 42452
		// (set) Token: 0x0600A5D3 RID: 42451
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

		// Token: 0x17003657 RID: 13911
		// (get) Token: 0x0600A5D6 RID: 42454
		// (set) Token: 0x0600A5D5 RID: 42453
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

		// Token: 0x17003658 RID: 13912
		// (get) Token: 0x0600A5D7 RID: 42455
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003659 RID: 13913
		// (get) Token: 0x0600A5D8 RID: 42456
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700365A RID: 13914
		// (get) Token: 0x0600A5D9 RID: 42457
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700365B RID: 13915
		// (get) Token: 0x0600A5DA RID: 42458
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600A5DB RID: 42459
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x1700365C RID: 13916
		// (get) Token: 0x0600A5DC RID: 42460
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700365D RID: 13917
		// (get) Token: 0x0600A5DD RID: 42461
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600A5DE RID: 42462
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600A5DF RID: 42463
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600A5E0 RID: 42464
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600A5E1 RID: 42465
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600A5E2 RID: 42466
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600A5E3 RID: 42467
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600A5E4 RID: 42468
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600A5E5 RID: 42469
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x1700365E RID: 13918
		// (get) Token: 0x0600A5E6 RID: 42470
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700365F RID: 13919
		// (get) Token: 0x0600A5E8 RID: 42472
		// (set) Token: 0x0600A5E7 RID: 42471
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

		// Token: 0x17003660 RID: 13920
		// (get) Token: 0x0600A5E9 RID: 42473
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003661 RID: 13921
		// (get) Token: 0x0600A5EA RID: 42474
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003662 RID: 13922
		// (get) Token: 0x0600A5EB RID: 42475
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003663 RID: 13923
		// (get) Token: 0x0600A5EC RID: 42476
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003664 RID: 13924
		// (get) Token: 0x0600A5ED RID: 42477
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003665 RID: 13925
		// (get) Token: 0x0600A5EF RID: 42479
		// (set) Token: 0x0600A5EE RID: 42478
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

		// Token: 0x17003666 RID: 13926
		// (get) Token: 0x0600A5F1 RID: 42481
		// (set) Token: 0x0600A5F0 RID: 42480
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

		// Token: 0x17003667 RID: 13927
		// (get) Token: 0x0600A5F3 RID: 42483
		// (set) Token: 0x0600A5F2 RID: 42482
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

		// Token: 0x17003668 RID: 13928
		// (get) Token: 0x0600A5F5 RID: 42485
		// (set) Token: 0x0600A5F4 RID: 42484
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

		// Token: 0x0600A5F6 RID: 42486
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17003669 RID: 13929
		// (get) Token: 0x0600A5F8 RID: 42488
		// (set) Token: 0x0600A5F7 RID: 42487
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

		// Token: 0x1700366A RID: 13930
		// (get) Token: 0x0600A5FA RID: 42490
		// (set) Token: 0x0600A5F9 RID: 42489
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

		// Token: 0x1700366B RID: 13931
		// (get) Token: 0x0600A5FC RID: 42492
		// (set) Token: 0x0600A5FB RID: 42491
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

		// Token: 0x1700366C RID: 13932
		// (get) Token: 0x0600A5FE RID: 42494
		// (set) Token: 0x0600A5FD RID: 42493
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

		// Token: 0x0600A5FF RID: 42495
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x0600A600 RID: 42496
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600A601 RID: 42497
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700366D RID: 13933
		// (get) Token: 0x0600A602 RID: 42498
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700366E RID: 13934
		// (get) Token: 0x0600A603 RID: 42499
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700366F RID: 13935
		// (get) Token: 0x0600A604 RID: 42500
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003670 RID: 13936
		// (get) Token: 0x0600A605 RID: 42501
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A606 RID: 42502
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTextContainer_createControlRange();

		// Token: 0x17003671 RID: 13937
		// (get) Token: 0x0600A607 RID: 42503
		public virtual extern int IHTMLTextContainer_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003672 RID: 13938
		// (get) Token: 0x0600A608 RID: 42504
		public virtual extern int IHTMLTextContainer_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003673 RID: 13939
		// (get) Token: 0x0600A60A RID: 42506
		// (set) Token: 0x0600A609 RID: 42505
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

		// Token: 0x17003674 RID: 13940
		// (get) Token: 0x0600A60C RID: 42508
		// (set) Token: 0x0600A60B RID: 42507
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

		// Token: 0x17003675 RID: 13941
		// (get) Token: 0x0600A60E RID: 42510
		// (set) Token: 0x0600A60D RID: 42509
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

		// Token: 0x17003676 RID: 13942
		// (get) Token: 0x0600A60F RID: 42511
		public virtual extern string IHTMLButtonElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003677 RID: 13943
		// (get) Token: 0x0600A611 RID: 42513
		// (set) Token: 0x0600A610 RID: 42512
		public virtual extern string IHTMLButtonElement_value
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

		// Token: 0x17003678 RID: 13944
		// (get) Token: 0x0600A613 RID: 42515
		// (set) Token: 0x0600A612 RID: 42514
		public virtual extern string IHTMLButtonElement_name
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

		// Token: 0x17003679 RID: 13945
		// (get) Token: 0x0600A615 RID: 42517
		// (set) Token: 0x0600A614 RID: 42516
		public virtual extern object IHTMLButtonElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700367A RID: 13946
		// (get) Token: 0x0600A617 RID: 42519
		// (set) Token: 0x0600A616 RID: 42518
		public virtual extern bool IHTMLButtonElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700367B RID: 13947
		// (get) Token: 0x0600A618 RID: 42520
		public virtual extern IHTMLFormElement IHTMLButtonElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600A619 RID: 42521
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange IHTMLButtonElement_createTextRange();

		// Token: 0x140013ED RID: 5101
		// (add) Token: 0x0600A61A RID: 42522
		// (remove) Token: 0x0600A61B RID: 42523
		public virtual extern event HTMLButtonElementEvents_onhelpEventHandler HTMLButtonElementEvents_Event_onhelp;

		// Token: 0x140013EE RID: 5102
		// (add) Token: 0x0600A61C RID: 42524
		// (remove) Token: 0x0600A61D RID: 42525
		public virtual extern event HTMLButtonElementEvents_onclickEventHandler HTMLButtonElementEvents_Event_onclick;

		// Token: 0x140013EF RID: 5103
		// (add) Token: 0x0600A61E RID: 42526
		// (remove) Token: 0x0600A61F RID: 42527
		public virtual extern event HTMLButtonElementEvents_ondblclickEventHandler HTMLButtonElementEvents_Event_ondblclick;

		// Token: 0x140013F0 RID: 5104
		// (add) Token: 0x0600A620 RID: 42528
		// (remove) Token: 0x0600A621 RID: 42529
		public virtual extern event HTMLButtonElementEvents_onkeypressEventHandler HTMLButtonElementEvents_Event_onkeypress;

		// Token: 0x140013F1 RID: 5105
		// (add) Token: 0x0600A622 RID: 42530
		// (remove) Token: 0x0600A623 RID: 42531
		public virtual extern event HTMLButtonElementEvents_onkeydownEventHandler HTMLButtonElementEvents_Event_onkeydown;

		// Token: 0x140013F2 RID: 5106
		// (add) Token: 0x0600A624 RID: 42532
		// (remove) Token: 0x0600A625 RID: 42533
		public virtual extern event HTMLButtonElementEvents_onkeyupEventHandler HTMLButtonElementEvents_Event_onkeyup;

		// Token: 0x140013F3 RID: 5107
		// (add) Token: 0x0600A626 RID: 42534
		// (remove) Token: 0x0600A627 RID: 42535
		public virtual extern event HTMLButtonElementEvents_onmouseoutEventHandler HTMLButtonElementEvents_Event_onmouseout;

		// Token: 0x140013F4 RID: 5108
		// (add) Token: 0x0600A628 RID: 42536
		// (remove) Token: 0x0600A629 RID: 42537
		public virtual extern event HTMLButtonElementEvents_onmouseoverEventHandler HTMLButtonElementEvents_Event_onmouseover;

		// Token: 0x140013F5 RID: 5109
		// (add) Token: 0x0600A62A RID: 42538
		// (remove) Token: 0x0600A62B RID: 42539
		public virtual extern event HTMLButtonElementEvents_onmousemoveEventHandler HTMLButtonElementEvents_Event_onmousemove;

		// Token: 0x140013F6 RID: 5110
		// (add) Token: 0x0600A62C RID: 42540
		// (remove) Token: 0x0600A62D RID: 42541
		public virtual extern event HTMLButtonElementEvents_onmousedownEventHandler HTMLButtonElementEvents_Event_onmousedown;

		// Token: 0x140013F7 RID: 5111
		// (add) Token: 0x0600A62E RID: 42542
		// (remove) Token: 0x0600A62F RID: 42543
		public virtual extern event HTMLButtonElementEvents_onmouseupEventHandler HTMLButtonElementEvents_Event_onmouseup;

		// Token: 0x140013F8 RID: 5112
		// (add) Token: 0x0600A630 RID: 42544
		// (remove) Token: 0x0600A631 RID: 42545
		public virtual extern event HTMLButtonElementEvents_onselectstartEventHandler HTMLButtonElementEvents_Event_onselectstart;

		// Token: 0x140013F9 RID: 5113
		// (add) Token: 0x0600A632 RID: 42546
		// (remove) Token: 0x0600A633 RID: 42547
		public virtual extern event HTMLButtonElementEvents_onfilterchangeEventHandler HTMLButtonElementEvents_Event_onfilterchange;

		// Token: 0x140013FA RID: 5114
		// (add) Token: 0x0600A634 RID: 42548
		// (remove) Token: 0x0600A635 RID: 42549
		public virtual extern event HTMLButtonElementEvents_ondragstartEventHandler HTMLButtonElementEvents_Event_ondragstart;

		// Token: 0x140013FB RID: 5115
		// (add) Token: 0x0600A636 RID: 42550
		// (remove) Token: 0x0600A637 RID: 42551
		public virtual extern event HTMLButtonElementEvents_onbeforeupdateEventHandler HTMLButtonElementEvents_Event_onbeforeupdate;

		// Token: 0x140013FC RID: 5116
		// (add) Token: 0x0600A638 RID: 42552
		// (remove) Token: 0x0600A639 RID: 42553
		public virtual extern event HTMLButtonElementEvents_onafterupdateEventHandler HTMLButtonElementEvents_Event_onafterupdate;

		// Token: 0x140013FD RID: 5117
		// (add) Token: 0x0600A63A RID: 42554
		// (remove) Token: 0x0600A63B RID: 42555
		public virtual extern event HTMLButtonElementEvents_onerrorupdateEventHandler HTMLButtonElementEvents_Event_onerrorupdate;

		// Token: 0x140013FE RID: 5118
		// (add) Token: 0x0600A63C RID: 42556
		// (remove) Token: 0x0600A63D RID: 42557
		public virtual extern event HTMLButtonElementEvents_onrowexitEventHandler HTMLButtonElementEvents_Event_onrowexit;

		// Token: 0x140013FF RID: 5119
		// (add) Token: 0x0600A63E RID: 42558
		// (remove) Token: 0x0600A63F RID: 42559
		public virtual extern event HTMLButtonElementEvents_onrowenterEventHandler HTMLButtonElementEvents_Event_onrowenter;

		// Token: 0x14001400 RID: 5120
		// (add) Token: 0x0600A640 RID: 42560
		// (remove) Token: 0x0600A641 RID: 42561
		public virtual extern event HTMLButtonElementEvents_ondatasetchangedEventHandler HTMLButtonElementEvents_Event_ondatasetchanged;

		// Token: 0x14001401 RID: 5121
		// (add) Token: 0x0600A642 RID: 42562
		// (remove) Token: 0x0600A643 RID: 42563
		public virtual extern event HTMLButtonElementEvents_ondataavailableEventHandler HTMLButtonElementEvents_Event_ondataavailable;

		// Token: 0x14001402 RID: 5122
		// (add) Token: 0x0600A644 RID: 42564
		// (remove) Token: 0x0600A645 RID: 42565
		public virtual extern event HTMLButtonElementEvents_ondatasetcompleteEventHandler HTMLButtonElementEvents_Event_ondatasetcomplete;

		// Token: 0x14001403 RID: 5123
		// (add) Token: 0x0600A646 RID: 42566
		// (remove) Token: 0x0600A647 RID: 42567
		public virtual extern event HTMLButtonElementEvents_onlosecaptureEventHandler HTMLButtonElementEvents_Event_onlosecapture;

		// Token: 0x14001404 RID: 5124
		// (add) Token: 0x0600A648 RID: 42568
		// (remove) Token: 0x0600A649 RID: 42569
		public virtual extern event HTMLButtonElementEvents_onpropertychangeEventHandler HTMLButtonElementEvents_Event_onpropertychange;

		// Token: 0x14001405 RID: 5125
		// (add) Token: 0x0600A64A RID: 42570
		// (remove) Token: 0x0600A64B RID: 42571
		public virtual extern event HTMLButtonElementEvents_onscrollEventHandler HTMLButtonElementEvents_Event_onscroll;

		// Token: 0x14001406 RID: 5126
		// (add) Token: 0x0600A64C RID: 42572
		// (remove) Token: 0x0600A64D RID: 42573
		public virtual extern event HTMLButtonElementEvents_onfocusEventHandler HTMLButtonElementEvents_Event_onfocus;

		// Token: 0x14001407 RID: 5127
		// (add) Token: 0x0600A64E RID: 42574
		// (remove) Token: 0x0600A64F RID: 42575
		public virtual extern event HTMLButtonElementEvents_onblurEventHandler HTMLButtonElementEvents_Event_onblur;

		// Token: 0x14001408 RID: 5128
		// (add) Token: 0x0600A650 RID: 42576
		// (remove) Token: 0x0600A651 RID: 42577
		public virtual extern event HTMLButtonElementEvents_onresizeEventHandler HTMLButtonElementEvents_Event_onresize;

		// Token: 0x14001409 RID: 5129
		// (add) Token: 0x0600A652 RID: 42578
		// (remove) Token: 0x0600A653 RID: 42579
		public virtual extern event HTMLButtonElementEvents_ondragEventHandler HTMLButtonElementEvents_Event_ondrag;

		// Token: 0x1400140A RID: 5130
		// (add) Token: 0x0600A654 RID: 42580
		// (remove) Token: 0x0600A655 RID: 42581
		public virtual extern event HTMLButtonElementEvents_ondragendEventHandler HTMLButtonElementEvents_Event_ondragend;

		// Token: 0x1400140B RID: 5131
		// (add) Token: 0x0600A656 RID: 42582
		// (remove) Token: 0x0600A657 RID: 42583
		public virtual extern event HTMLButtonElementEvents_ondragenterEventHandler HTMLButtonElementEvents_Event_ondragenter;

		// Token: 0x1400140C RID: 5132
		// (add) Token: 0x0600A658 RID: 42584
		// (remove) Token: 0x0600A659 RID: 42585
		public virtual extern event HTMLButtonElementEvents_ondragoverEventHandler HTMLButtonElementEvents_Event_ondragover;

		// Token: 0x1400140D RID: 5133
		// (add) Token: 0x0600A65A RID: 42586
		// (remove) Token: 0x0600A65B RID: 42587
		public virtual extern event HTMLButtonElementEvents_ondragleaveEventHandler HTMLButtonElementEvents_Event_ondragleave;

		// Token: 0x1400140E RID: 5134
		// (add) Token: 0x0600A65C RID: 42588
		// (remove) Token: 0x0600A65D RID: 42589
		public virtual extern event HTMLButtonElementEvents_ondropEventHandler HTMLButtonElementEvents_Event_ondrop;

		// Token: 0x1400140F RID: 5135
		// (add) Token: 0x0600A65E RID: 42590
		// (remove) Token: 0x0600A65F RID: 42591
		public virtual extern event HTMLButtonElementEvents_onbeforecutEventHandler HTMLButtonElementEvents_Event_onbeforecut;

		// Token: 0x14001410 RID: 5136
		// (add) Token: 0x0600A660 RID: 42592
		// (remove) Token: 0x0600A661 RID: 42593
		public virtual extern event HTMLButtonElementEvents_oncutEventHandler HTMLButtonElementEvents_Event_oncut;

		// Token: 0x14001411 RID: 5137
		// (add) Token: 0x0600A662 RID: 42594
		// (remove) Token: 0x0600A663 RID: 42595
		public virtual extern event HTMLButtonElementEvents_onbeforecopyEventHandler HTMLButtonElementEvents_Event_onbeforecopy;

		// Token: 0x14001412 RID: 5138
		// (add) Token: 0x0600A664 RID: 42596
		// (remove) Token: 0x0600A665 RID: 42597
		public virtual extern event HTMLButtonElementEvents_oncopyEventHandler HTMLButtonElementEvents_Event_oncopy;

		// Token: 0x14001413 RID: 5139
		// (add) Token: 0x0600A666 RID: 42598
		// (remove) Token: 0x0600A667 RID: 42599
		public virtual extern event HTMLButtonElementEvents_onbeforepasteEventHandler HTMLButtonElementEvents_Event_onbeforepaste;

		// Token: 0x14001414 RID: 5140
		// (add) Token: 0x0600A668 RID: 42600
		// (remove) Token: 0x0600A669 RID: 42601
		public virtual extern event HTMLButtonElementEvents_onpasteEventHandler HTMLButtonElementEvents_Event_onpaste;

		// Token: 0x14001415 RID: 5141
		// (add) Token: 0x0600A66A RID: 42602
		// (remove) Token: 0x0600A66B RID: 42603
		public virtual extern event HTMLButtonElementEvents_oncontextmenuEventHandler HTMLButtonElementEvents_Event_oncontextmenu;

		// Token: 0x14001416 RID: 5142
		// (add) Token: 0x0600A66C RID: 42604
		// (remove) Token: 0x0600A66D RID: 42605
		public virtual extern event HTMLButtonElementEvents_onrowsdeleteEventHandler HTMLButtonElementEvents_Event_onrowsdelete;

		// Token: 0x14001417 RID: 5143
		// (add) Token: 0x0600A66E RID: 42606
		// (remove) Token: 0x0600A66F RID: 42607
		public virtual extern event HTMLButtonElementEvents_onrowsinsertedEventHandler HTMLButtonElementEvents_Event_onrowsinserted;

		// Token: 0x14001418 RID: 5144
		// (add) Token: 0x0600A670 RID: 42608
		// (remove) Token: 0x0600A671 RID: 42609
		public virtual extern event HTMLButtonElementEvents_oncellchangeEventHandler HTMLButtonElementEvents_Event_oncellchange;

		// Token: 0x14001419 RID: 5145
		// (add) Token: 0x0600A672 RID: 42610
		// (remove) Token: 0x0600A673 RID: 42611
		public virtual extern event HTMLButtonElementEvents_onreadystatechangeEventHandler HTMLButtonElementEvents_Event_onreadystatechange;

		// Token: 0x1400141A RID: 5146
		// (add) Token: 0x0600A674 RID: 42612
		// (remove) Token: 0x0600A675 RID: 42613
		public virtual extern event HTMLButtonElementEvents_onbeforeeditfocusEventHandler HTMLButtonElementEvents_Event_onbeforeeditfocus;

		// Token: 0x1400141B RID: 5147
		// (add) Token: 0x0600A676 RID: 42614
		// (remove) Token: 0x0600A677 RID: 42615
		public virtual extern event HTMLButtonElementEvents_onlayoutcompleteEventHandler HTMLButtonElementEvents_Event_onlayoutcomplete;

		// Token: 0x1400141C RID: 5148
		// (add) Token: 0x0600A678 RID: 42616
		// (remove) Token: 0x0600A679 RID: 42617
		public virtual extern event HTMLButtonElementEvents_onpageEventHandler HTMLButtonElementEvents_Event_onpage;

		// Token: 0x1400141D RID: 5149
		// (add) Token: 0x0600A67A RID: 42618
		// (remove) Token: 0x0600A67B RID: 42619
		public virtual extern event HTMLButtonElementEvents_onbeforedeactivateEventHandler HTMLButtonElementEvents_Event_onbeforedeactivate;

		// Token: 0x1400141E RID: 5150
		// (add) Token: 0x0600A67C RID: 42620
		// (remove) Token: 0x0600A67D RID: 42621
		public virtual extern event HTMLButtonElementEvents_onbeforeactivateEventHandler HTMLButtonElementEvents_Event_onbeforeactivate;

		// Token: 0x1400141F RID: 5151
		// (add) Token: 0x0600A67E RID: 42622
		// (remove) Token: 0x0600A67F RID: 42623
		public virtual extern event HTMLButtonElementEvents_onmoveEventHandler HTMLButtonElementEvents_Event_onmove;

		// Token: 0x14001420 RID: 5152
		// (add) Token: 0x0600A680 RID: 42624
		// (remove) Token: 0x0600A681 RID: 42625
		public virtual extern event HTMLButtonElementEvents_oncontrolselectEventHandler HTMLButtonElementEvents_Event_oncontrolselect;

		// Token: 0x14001421 RID: 5153
		// (add) Token: 0x0600A682 RID: 42626
		// (remove) Token: 0x0600A683 RID: 42627
		public virtual extern event HTMLButtonElementEvents_onmovestartEventHandler HTMLButtonElementEvents_Event_onmovestart;

		// Token: 0x14001422 RID: 5154
		// (add) Token: 0x0600A684 RID: 42628
		// (remove) Token: 0x0600A685 RID: 42629
		public virtual extern event HTMLButtonElementEvents_onmoveendEventHandler HTMLButtonElementEvents_Event_onmoveend;

		// Token: 0x14001423 RID: 5155
		// (add) Token: 0x0600A686 RID: 42630
		// (remove) Token: 0x0600A687 RID: 42631
		public virtual extern event HTMLButtonElementEvents_onresizestartEventHandler HTMLButtonElementEvents_Event_onresizestart;

		// Token: 0x14001424 RID: 5156
		// (add) Token: 0x0600A688 RID: 42632
		// (remove) Token: 0x0600A689 RID: 42633
		public virtual extern event HTMLButtonElementEvents_onresizeendEventHandler HTMLButtonElementEvents_Event_onresizeend;

		// Token: 0x14001425 RID: 5157
		// (add) Token: 0x0600A68A RID: 42634
		// (remove) Token: 0x0600A68B RID: 42635
		public virtual extern event HTMLButtonElementEvents_onmouseenterEventHandler HTMLButtonElementEvents_Event_onmouseenter;

		// Token: 0x14001426 RID: 5158
		// (add) Token: 0x0600A68C RID: 42636
		// (remove) Token: 0x0600A68D RID: 42637
		public virtual extern event HTMLButtonElementEvents_onmouseleaveEventHandler HTMLButtonElementEvents_Event_onmouseleave;

		// Token: 0x14001427 RID: 5159
		// (add) Token: 0x0600A68E RID: 42638
		// (remove) Token: 0x0600A68F RID: 42639
		public virtual extern event HTMLButtonElementEvents_onmousewheelEventHandler HTMLButtonElementEvents_Event_onmousewheel;

		// Token: 0x14001428 RID: 5160
		// (add) Token: 0x0600A690 RID: 42640
		// (remove) Token: 0x0600A691 RID: 42641
		public virtual extern event HTMLButtonElementEvents_onactivateEventHandler HTMLButtonElementEvents_Event_onactivate;

		// Token: 0x14001429 RID: 5161
		// (add) Token: 0x0600A692 RID: 42642
		// (remove) Token: 0x0600A693 RID: 42643
		public virtual extern event HTMLButtonElementEvents_ondeactivateEventHandler HTMLButtonElementEvents_Event_ondeactivate;

		// Token: 0x1400142A RID: 5162
		// (add) Token: 0x0600A694 RID: 42644
		// (remove) Token: 0x0600A695 RID: 42645
		public virtual extern event HTMLButtonElementEvents_onfocusinEventHandler HTMLButtonElementEvents_Event_onfocusin;

		// Token: 0x1400142B RID: 5163
		// (add) Token: 0x0600A696 RID: 42646
		// (remove) Token: 0x0600A697 RID: 42647
		public virtual extern event HTMLButtonElementEvents_onfocusoutEventHandler HTMLButtonElementEvents_Event_onfocusout;

		// Token: 0x1400142C RID: 5164
		// (add) Token: 0x0600A698 RID: 42648
		// (remove) Token: 0x0600A699 RID: 42649
		public virtual extern event HTMLButtonElementEvents2_onhelpEventHandler HTMLButtonElementEvents2_Event_onhelp;

		// Token: 0x1400142D RID: 5165
		// (add) Token: 0x0600A69A RID: 42650
		// (remove) Token: 0x0600A69B RID: 42651
		public virtual extern event HTMLButtonElementEvents2_onclickEventHandler HTMLButtonElementEvents2_Event_onclick;

		// Token: 0x1400142E RID: 5166
		// (add) Token: 0x0600A69C RID: 42652
		// (remove) Token: 0x0600A69D RID: 42653
		public virtual extern event HTMLButtonElementEvents2_ondblclickEventHandler HTMLButtonElementEvents2_Event_ondblclick;

		// Token: 0x1400142F RID: 5167
		// (add) Token: 0x0600A69E RID: 42654
		// (remove) Token: 0x0600A69F RID: 42655
		public virtual extern event HTMLButtonElementEvents2_onkeypressEventHandler HTMLButtonElementEvents2_Event_onkeypress;

		// Token: 0x14001430 RID: 5168
		// (add) Token: 0x0600A6A0 RID: 42656
		// (remove) Token: 0x0600A6A1 RID: 42657
		public virtual extern event HTMLButtonElementEvents2_onkeydownEventHandler HTMLButtonElementEvents2_Event_onkeydown;

		// Token: 0x14001431 RID: 5169
		// (add) Token: 0x0600A6A2 RID: 42658
		// (remove) Token: 0x0600A6A3 RID: 42659
		public virtual extern event HTMLButtonElementEvents2_onkeyupEventHandler HTMLButtonElementEvents2_Event_onkeyup;

		// Token: 0x14001432 RID: 5170
		// (add) Token: 0x0600A6A4 RID: 42660
		// (remove) Token: 0x0600A6A5 RID: 42661
		public virtual extern event HTMLButtonElementEvents2_onmouseoutEventHandler HTMLButtonElementEvents2_Event_onmouseout;

		// Token: 0x14001433 RID: 5171
		// (add) Token: 0x0600A6A6 RID: 42662
		// (remove) Token: 0x0600A6A7 RID: 42663
		public virtual extern event HTMLButtonElementEvents2_onmouseoverEventHandler HTMLButtonElementEvents2_Event_onmouseover;

		// Token: 0x14001434 RID: 5172
		// (add) Token: 0x0600A6A8 RID: 42664
		// (remove) Token: 0x0600A6A9 RID: 42665
		public virtual extern event HTMLButtonElementEvents2_onmousemoveEventHandler HTMLButtonElementEvents2_Event_onmousemove;

		// Token: 0x14001435 RID: 5173
		// (add) Token: 0x0600A6AA RID: 42666
		// (remove) Token: 0x0600A6AB RID: 42667
		public virtual extern event HTMLButtonElementEvents2_onmousedownEventHandler HTMLButtonElementEvents2_Event_onmousedown;

		// Token: 0x14001436 RID: 5174
		// (add) Token: 0x0600A6AC RID: 42668
		// (remove) Token: 0x0600A6AD RID: 42669
		public virtual extern event HTMLButtonElementEvents2_onmouseupEventHandler HTMLButtonElementEvents2_Event_onmouseup;

		// Token: 0x14001437 RID: 5175
		// (add) Token: 0x0600A6AE RID: 42670
		// (remove) Token: 0x0600A6AF RID: 42671
		public virtual extern event HTMLButtonElementEvents2_onselectstartEventHandler HTMLButtonElementEvents2_Event_onselectstart;

		// Token: 0x14001438 RID: 5176
		// (add) Token: 0x0600A6B0 RID: 42672
		// (remove) Token: 0x0600A6B1 RID: 42673
		public virtual extern event HTMLButtonElementEvents2_onfilterchangeEventHandler HTMLButtonElementEvents2_Event_onfilterchange;

		// Token: 0x14001439 RID: 5177
		// (add) Token: 0x0600A6B2 RID: 42674
		// (remove) Token: 0x0600A6B3 RID: 42675
		public virtual extern event HTMLButtonElementEvents2_ondragstartEventHandler HTMLButtonElementEvents2_Event_ondragstart;

		// Token: 0x1400143A RID: 5178
		// (add) Token: 0x0600A6B4 RID: 42676
		// (remove) Token: 0x0600A6B5 RID: 42677
		public virtual extern event HTMLButtonElementEvents2_onbeforeupdateEventHandler HTMLButtonElementEvents2_Event_onbeforeupdate;

		// Token: 0x1400143B RID: 5179
		// (add) Token: 0x0600A6B6 RID: 42678
		// (remove) Token: 0x0600A6B7 RID: 42679
		public virtual extern event HTMLButtonElementEvents2_onafterupdateEventHandler HTMLButtonElementEvents2_Event_onafterupdate;

		// Token: 0x1400143C RID: 5180
		// (add) Token: 0x0600A6B8 RID: 42680
		// (remove) Token: 0x0600A6B9 RID: 42681
		public virtual extern event HTMLButtonElementEvents2_onerrorupdateEventHandler HTMLButtonElementEvents2_Event_onerrorupdate;

		// Token: 0x1400143D RID: 5181
		// (add) Token: 0x0600A6BA RID: 42682
		// (remove) Token: 0x0600A6BB RID: 42683
		public virtual extern event HTMLButtonElementEvents2_onrowexitEventHandler HTMLButtonElementEvents2_Event_onrowexit;

		// Token: 0x1400143E RID: 5182
		// (add) Token: 0x0600A6BC RID: 42684
		// (remove) Token: 0x0600A6BD RID: 42685
		public virtual extern event HTMLButtonElementEvents2_onrowenterEventHandler HTMLButtonElementEvents2_Event_onrowenter;

		// Token: 0x1400143F RID: 5183
		// (add) Token: 0x0600A6BE RID: 42686
		// (remove) Token: 0x0600A6BF RID: 42687
		public virtual extern event HTMLButtonElementEvents2_ondatasetchangedEventHandler HTMLButtonElementEvents2_Event_ondatasetchanged;

		// Token: 0x14001440 RID: 5184
		// (add) Token: 0x0600A6C0 RID: 42688
		// (remove) Token: 0x0600A6C1 RID: 42689
		public virtual extern event HTMLButtonElementEvents2_ondataavailableEventHandler HTMLButtonElementEvents2_Event_ondataavailable;

		// Token: 0x14001441 RID: 5185
		// (add) Token: 0x0600A6C2 RID: 42690
		// (remove) Token: 0x0600A6C3 RID: 42691
		public virtual extern event HTMLButtonElementEvents2_ondatasetcompleteEventHandler HTMLButtonElementEvents2_Event_ondatasetcomplete;

		// Token: 0x14001442 RID: 5186
		// (add) Token: 0x0600A6C4 RID: 42692
		// (remove) Token: 0x0600A6C5 RID: 42693
		public virtual extern event HTMLButtonElementEvents2_onlosecaptureEventHandler HTMLButtonElementEvents2_Event_onlosecapture;

		// Token: 0x14001443 RID: 5187
		// (add) Token: 0x0600A6C6 RID: 42694
		// (remove) Token: 0x0600A6C7 RID: 42695
		public virtual extern event HTMLButtonElementEvents2_onpropertychangeEventHandler HTMLButtonElementEvents2_Event_onpropertychange;

		// Token: 0x14001444 RID: 5188
		// (add) Token: 0x0600A6C8 RID: 42696
		// (remove) Token: 0x0600A6C9 RID: 42697
		public virtual extern event HTMLButtonElementEvents2_onscrollEventHandler HTMLButtonElementEvents2_Event_onscroll;

		// Token: 0x14001445 RID: 5189
		// (add) Token: 0x0600A6CA RID: 42698
		// (remove) Token: 0x0600A6CB RID: 42699
		public virtual extern event HTMLButtonElementEvents2_onfocusEventHandler HTMLButtonElementEvents2_Event_onfocus;

		// Token: 0x14001446 RID: 5190
		// (add) Token: 0x0600A6CC RID: 42700
		// (remove) Token: 0x0600A6CD RID: 42701
		public virtual extern event HTMLButtonElementEvents2_onblurEventHandler HTMLButtonElementEvents2_Event_onblur;

		// Token: 0x14001447 RID: 5191
		// (add) Token: 0x0600A6CE RID: 42702
		// (remove) Token: 0x0600A6CF RID: 42703
		public virtual extern event HTMLButtonElementEvents2_onresizeEventHandler HTMLButtonElementEvents2_Event_onresize;

		// Token: 0x14001448 RID: 5192
		// (add) Token: 0x0600A6D0 RID: 42704
		// (remove) Token: 0x0600A6D1 RID: 42705
		public virtual extern event HTMLButtonElementEvents2_ondragEventHandler HTMLButtonElementEvents2_Event_ondrag;

		// Token: 0x14001449 RID: 5193
		// (add) Token: 0x0600A6D2 RID: 42706
		// (remove) Token: 0x0600A6D3 RID: 42707
		public virtual extern event HTMLButtonElementEvents2_ondragendEventHandler HTMLButtonElementEvents2_Event_ondragend;

		// Token: 0x1400144A RID: 5194
		// (add) Token: 0x0600A6D4 RID: 42708
		// (remove) Token: 0x0600A6D5 RID: 42709
		public virtual extern event HTMLButtonElementEvents2_ondragenterEventHandler HTMLButtonElementEvents2_Event_ondragenter;

		// Token: 0x1400144B RID: 5195
		// (add) Token: 0x0600A6D6 RID: 42710
		// (remove) Token: 0x0600A6D7 RID: 42711
		public virtual extern event HTMLButtonElementEvents2_ondragoverEventHandler HTMLButtonElementEvents2_Event_ondragover;

		// Token: 0x1400144C RID: 5196
		// (add) Token: 0x0600A6D8 RID: 42712
		// (remove) Token: 0x0600A6D9 RID: 42713
		public virtual extern event HTMLButtonElementEvents2_ondragleaveEventHandler HTMLButtonElementEvents2_Event_ondragleave;

		// Token: 0x1400144D RID: 5197
		// (add) Token: 0x0600A6DA RID: 42714
		// (remove) Token: 0x0600A6DB RID: 42715
		public virtual extern event HTMLButtonElementEvents2_ondropEventHandler HTMLButtonElementEvents2_Event_ondrop;

		// Token: 0x1400144E RID: 5198
		// (add) Token: 0x0600A6DC RID: 42716
		// (remove) Token: 0x0600A6DD RID: 42717
		public virtual extern event HTMLButtonElementEvents2_onbeforecutEventHandler HTMLButtonElementEvents2_Event_onbeforecut;

		// Token: 0x1400144F RID: 5199
		// (add) Token: 0x0600A6DE RID: 42718
		// (remove) Token: 0x0600A6DF RID: 42719
		public virtual extern event HTMLButtonElementEvents2_oncutEventHandler HTMLButtonElementEvents2_Event_oncut;

		// Token: 0x14001450 RID: 5200
		// (add) Token: 0x0600A6E0 RID: 42720
		// (remove) Token: 0x0600A6E1 RID: 42721
		public virtual extern event HTMLButtonElementEvents2_onbeforecopyEventHandler HTMLButtonElementEvents2_Event_onbeforecopy;

		// Token: 0x14001451 RID: 5201
		// (add) Token: 0x0600A6E2 RID: 42722
		// (remove) Token: 0x0600A6E3 RID: 42723
		public virtual extern event HTMLButtonElementEvents2_oncopyEventHandler HTMLButtonElementEvents2_Event_oncopy;

		// Token: 0x14001452 RID: 5202
		// (add) Token: 0x0600A6E4 RID: 42724
		// (remove) Token: 0x0600A6E5 RID: 42725
		public virtual extern event HTMLButtonElementEvents2_onbeforepasteEventHandler HTMLButtonElementEvents2_Event_onbeforepaste;

		// Token: 0x14001453 RID: 5203
		// (add) Token: 0x0600A6E6 RID: 42726
		// (remove) Token: 0x0600A6E7 RID: 42727
		public virtual extern event HTMLButtonElementEvents2_onpasteEventHandler HTMLButtonElementEvents2_Event_onpaste;

		// Token: 0x14001454 RID: 5204
		// (add) Token: 0x0600A6E8 RID: 42728
		// (remove) Token: 0x0600A6E9 RID: 42729
		public virtual extern event HTMLButtonElementEvents2_oncontextmenuEventHandler HTMLButtonElementEvents2_Event_oncontextmenu;

		// Token: 0x14001455 RID: 5205
		// (add) Token: 0x0600A6EA RID: 42730
		// (remove) Token: 0x0600A6EB RID: 42731
		public virtual extern event HTMLButtonElementEvents2_onrowsdeleteEventHandler HTMLButtonElementEvents2_Event_onrowsdelete;

		// Token: 0x14001456 RID: 5206
		// (add) Token: 0x0600A6EC RID: 42732
		// (remove) Token: 0x0600A6ED RID: 42733
		public virtual extern event HTMLButtonElementEvents2_onrowsinsertedEventHandler HTMLButtonElementEvents2_Event_onrowsinserted;

		// Token: 0x14001457 RID: 5207
		// (add) Token: 0x0600A6EE RID: 42734
		// (remove) Token: 0x0600A6EF RID: 42735
		public virtual extern event HTMLButtonElementEvents2_oncellchangeEventHandler HTMLButtonElementEvents2_Event_oncellchange;

		// Token: 0x14001458 RID: 5208
		// (add) Token: 0x0600A6F0 RID: 42736
		// (remove) Token: 0x0600A6F1 RID: 42737
		public virtual extern event HTMLButtonElementEvents2_onreadystatechangeEventHandler HTMLButtonElementEvents2_Event_onreadystatechange;

		// Token: 0x14001459 RID: 5209
		// (add) Token: 0x0600A6F2 RID: 42738
		// (remove) Token: 0x0600A6F3 RID: 42739
		public virtual extern event HTMLButtonElementEvents2_onlayoutcompleteEventHandler HTMLButtonElementEvents2_Event_onlayoutcomplete;

		// Token: 0x1400145A RID: 5210
		// (add) Token: 0x0600A6F4 RID: 42740
		// (remove) Token: 0x0600A6F5 RID: 42741
		public virtual extern event HTMLButtonElementEvents2_onpageEventHandler HTMLButtonElementEvents2_Event_onpage;

		// Token: 0x1400145B RID: 5211
		// (add) Token: 0x0600A6F6 RID: 42742
		// (remove) Token: 0x0600A6F7 RID: 42743
		public virtual extern event HTMLButtonElementEvents2_onmouseenterEventHandler HTMLButtonElementEvents2_Event_onmouseenter;

		// Token: 0x1400145C RID: 5212
		// (add) Token: 0x0600A6F8 RID: 42744
		// (remove) Token: 0x0600A6F9 RID: 42745
		public virtual extern event HTMLButtonElementEvents2_onmouseleaveEventHandler HTMLButtonElementEvents2_Event_onmouseleave;

		// Token: 0x1400145D RID: 5213
		// (add) Token: 0x0600A6FA RID: 42746
		// (remove) Token: 0x0600A6FB RID: 42747
		public virtual extern event HTMLButtonElementEvents2_onactivateEventHandler HTMLButtonElementEvents2_Event_onactivate;

		// Token: 0x1400145E RID: 5214
		// (add) Token: 0x0600A6FC RID: 42748
		// (remove) Token: 0x0600A6FD RID: 42749
		public virtual extern event HTMLButtonElementEvents2_ondeactivateEventHandler HTMLButtonElementEvents2_Event_ondeactivate;

		// Token: 0x1400145F RID: 5215
		// (add) Token: 0x0600A6FE RID: 42750
		// (remove) Token: 0x0600A6FF RID: 42751
		public virtual extern event HTMLButtonElementEvents2_onbeforedeactivateEventHandler HTMLButtonElementEvents2_Event_onbeforedeactivate;

		// Token: 0x14001460 RID: 5216
		// (add) Token: 0x0600A700 RID: 42752
		// (remove) Token: 0x0600A701 RID: 42753
		public virtual extern event HTMLButtonElementEvents2_onbeforeactivateEventHandler HTMLButtonElementEvents2_Event_onbeforeactivate;

		// Token: 0x14001461 RID: 5217
		// (add) Token: 0x0600A702 RID: 42754
		// (remove) Token: 0x0600A703 RID: 42755
		public virtual extern event HTMLButtonElementEvents2_onfocusinEventHandler HTMLButtonElementEvents2_Event_onfocusin;

		// Token: 0x14001462 RID: 5218
		// (add) Token: 0x0600A704 RID: 42756
		// (remove) Token: 0x0600A705 RID: 42757
		public virtual extern event HTMLButtonElementEvents2_onfocusoutEventHandler HTMLButtonElementEvents2_Event_onfocusout;

		// Token: 0x14001463 RID: 5219
		// (add) Token: 0x0600A706 RID: 42758
		// (remove) Token: 0x0600A707 RID: 42759
		public virtual extern event HTMLButtonElementEvents2_onmoveEventHandler HTMLButtonElementEvents2_Event_onmove;

		// Token: 0x14001464 RID: 5220
		// (add) Token: 0x0600A708 RID: 42760
		// (remove) Token: 0x0600A709 RID: 42761
		public virtual extern event HTMLButtonElementEvents2_oncontrolselectEventHandler HTMLButtonElementEvents2_Event_oncontrolselect;

		// Token: 0x14001465 RID: 5221
		// (add) Token: 0x0600A70A RID: 42762
		// (remove) Token: 0x0600A70B RID: 42763
		public virtual extern event HTMLButtonElementEvents2_onmovestartEventHandler HTMLButtonElementEvents2_Event_onmovestart;

		// Token: 0x14001466 RID: 5222
		// (add) Token: 0x0600A70C RID: 42764
		// (remove) Token: 0x0600A70D RID: 42765
		public virtual extern event HTMLButtonElementEvents2_onmoveendEventHandler HTMLButtonElementEvents2_Event_onmoveend;

		// Token: 0x14001467 RID: 5223
		// (add) Token: 0x0600A70E RID: 42766
		// (remove) Token: 0x0600A70F RID: 42767
		public virtual extern event HTMLButtonElementEvents2_onresizestartEventHandler HTMLButtonElementEvents2_Event_onresizestart;

		// Token: 0x14001468 RID: 5224
		// (add) Token: 0x0600A710 RID: 42768
		// (remove) Token: 0x0600A711 RID: 42769
		public virtual extern event HTMLButtonElementEvents2_onresizeendEventHandler HTMLButtonElementEvents2_Event_onresizeend;

		// Token: 0x14001469 RID: 5225
		// (add) Token: 0x0600A712 RID: 42770
		// (remove) Token: 0x0600A713 RID: 42771
		public virtual extern event HTMLButtonElementEvents2_onmousewheelEventHandler HTMLButtonElementEvents2_Event_onmousewheel;
	}
}
