using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BE7 RID: 3047
	[ComSourceInterfaces("mshtml.HTMLTextContainerEvents\0mshtml.HTMLTextContainerEvents2\0\0")]
	[ClassInterface(0)]
	[Guid("3050F3E6-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLSpanFlowClass : DispHTMLSpanFlow, HTMLSpanFlow, HTMLTextContainerEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLTextContainer, IHTMLSpanFlow, HTMLTextContainerEvents2_Event
	{
		// Token: 0x06014F11 RID: 85777
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLSpanFlowClass();

		// Token: 0x06014F12 RID: 85778
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06014F13 RID: 85779
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06014F14 RID: 85780
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17006F85 RID: 28549
		// (get) Token: 0x06014F16 RID: 85782
		// (set) Token: 0x06014F15 RID: 85781
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

		// Token: 0x17006F86 RID: 28550
		// (get) Token: 0x06014F18 RID: 85784
		// (set) Token: 0x06014F17 RID: 85783
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

		// Token: 0x17006F87 RID: 28551
		// (get) Token: 0x06014F19 RID: 85785
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006F88 RID: 28552
		// (get) Token: 0x06014F1A RID: 85786
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006F89 RID: 28553
		// (get) Token: 0x06014F1B RID: 85787
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006F8A RID: 28554
		// (get) Token: 0x06014F1D RID: 85789
		// (set) Token: 0x06014F1C RID: 85788
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

		// Token: 0x17006F8B RID: 28555
		// (get) Token: 0x06014F1F RID: 85791
		// (set) Token: 0x06014F1E RID: 85790
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

		// Token: 0x17006F8C RID: 28556
		// (get) Token: 0x06014F21 RID: 85793
		// (set) Token: 0x06014F20 RID: 85792
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

		// Token: 0x17006F8D RID: 28557
		// (get) Token: 0x06014F23 RID: 85795
		// (set) Token: 0x06014F22 RID: 85794
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

		// Token: 0x17006F8E RID: 28558
		// (get) Token: 0x06014F25 RID: 85797
		// (set) Token: 0x06014F24 RID: 85796
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

		// Token: 0x17006F8F RID: 28559
		// (get) Token: 0x06014F27 RID: 85799
		// (set) Token: 0x06014F26 RID: 85798
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

		// Token: 0x17006F90 RID: 28560
		// (get) Token: 0x06014F29 RID: 85801
		// (set) Token: 0x06014F28 RID: 85800
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

		// Token: 0x17006F91 RID: 28561
		// (get) Token: 0x06014F2B RID: 85803
		// (set) Token: 0x06014F2A RID: 85802
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

		// Token: 0x17006F92 RID: 28562
		// (get) Token: 0x06014F2D RID: 85805
		// (set) Token: 0x06014F2C RID: 85804
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

		// Token: 0x17006F93 RID: 28563
		// (get) Token: 0x06014F2F RID: 85807
		// (set) Token: 0x06014F2E RID: 85806
		[DispId(-2147412110)]
		public virtual extern object onmousedown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006F94 RID: 28564
		// (get) Token: 0x06014F31 RID: 85809
		// (set) Token: 0x06014F30 RID: 85808
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

		// Token: 0x17006F95 RID: 28565
		// (get) Token: 0x06014F32 RID: 85810
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006F96 RID: 28566
		// (get) Token: 0x06014F34 RID: 85812
		// (set) Token: 0x06014F33 RID: 85811
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

		// Token: 0x17006F97 RID: 28567
		// (get) Token: 0x06014F36 RID: 85814
		// (set) Token: 0x06014F35 RID: 85813
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

		// Token: 0x17006F98 RID: 28568
		// (get) Token: 0x06014F38 RID: 85816
		// (set) Token: 0x06014F37 RID: 85815
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

		// Token: 0x06014F39 RID: 85817
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06014F3A RID: 85818
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17006F99 RID: 28569
		// (get) Token: 0x06014F3B RID: 85819
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006F9A RID: 28570
		// (get) Token: 0x06014F3C RID: 85820
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17006F9B RID: 28571
		// (get) Token: 0x06014F3E RID: 85822
		// (set) Token: 0x06014F3D RID: 85821
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

		// Token: 0x17006F9C RID: 28572
		// (get) Token: 0x06014F3F RID: 85823
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006F9D RID: 28573
		// (get) Token: 0x06014F40 RID: 85824
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006F9E RID: 28574
		// (get) Token: 0x06014F41 RID: 85825
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006F9F RID: 28575
		// (get) Token: 0x06014F42 RID: 85826
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FA0 RID: 28576
		// (get) Token: 0x06014F43 RID: 85827
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006FA1 RID: 28577
		// (get) Token: 0x06014F45 RID: 85829
		// (set) Token: 0x06014F44 RID: 85828
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

		// Token: 0x17006FA2 RID: 28578
		// (get) Token: 0x06014F47 RID: 85831
		// (set) Token: 0x06014F46 RID: 85830
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

		// Token: 0x17006FA3 RID: 28579
		// (get) Token: 0x06014F49 RID: 85833
		// (set) Token: 0x06014F48 RID: 85832
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

		// Token: 0x17006FA4 RID: 28580
		// (get) Token: 0x06014F4B RID: 85835
		// (set) Token: 0x06014F4A RID: 85834
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

		// Token: 0x06014F4C RID: 85836
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06014F4D RID: 85837
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17006FA5 RID: 28581
		// (get) Token: 0x06014F4E RID: 85838
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006FA6 RID: 28582
		// (get) Token: 0x06014F4F RID: 85839
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06014F50 RID: 85840
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17006FA7 RID: 28583
		// (get) Token: 0x06014F51 RID: 85841
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006FA8 RID: 28584
		// (get) Token: 0x06014F53 RID: 85843
		// (set) Token: 0x06014F52 RID: 85842
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

		// Token: 0x06014F54 RID: 85844
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17006FA9 RID: 28585
		// (get) Token: 0x06014F56 RID: 85846
		// (set) Token: 0x06014F55 RID: 85845
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

		// Token: 0x17006FAA RID: 28586
		// (get) Token: 0x06014F58 RID: 85848
		// (set) Token: 0x06014F57 RID: 85847
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

		// Token: 0x17006FAB RID: 28587
		// (get) Token: 0x06014F5A RID: 85850
		// (set) Token: 0x06014F59 RID: 85849
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

		// Token: 0x17006FAC RID: 28588
		// (get) Token: 0x06014F5C RID: 85852
		// (set) Token: 0x06014F5B RID: 85851
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

		// Token: 0x17006FAD RID: 28589
		// (get) Token: 0x06014F5E RID: 85854
		// (set) Token: 0x06014F5D RID: 85853
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

		// Token: 0x17006FAE RID: 28590
		// (get) Token: 0x06014F60 RID: 85856
		// (set) Token: 0x06014F5F RID: 85855
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

		// Token: 0x17006FAF RID: 28591
		// (get) Token: 0x06014F62 RID: 85858
		// (set) Token: 0x06014F61 RID: 85857
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

		// Token: 0x17006FB0 RID: 28592
		// (get) Token: 0x06014F64 RID: 85860
		// (set) Token: 0x06014F63 RID: 85859
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

		// Token: 0x17006FB1 RID: 28593
		// (get) Token: 0x06014F66 RID: 85862
		// (set) Token: 0x06014F65 RID: 85861
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

		// Token: 0x17006FB2 RID: 28594
		// (get) Token: 0x06014F67 RID: 85863
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006FB3 RID: 28595
		// (get) Token: 0x06014F68 RID: 85864
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006FB4 RID: 28596
		// (get) Token: 0x06014F69 RID: 85865
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06014F6A RID: 85866
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06014F6B RID: 85867
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17006FB5 RID: 28597
		// (get) Token: 0x06014F6D RID: 85869
		// (set) Token: 0x06014F6C RID: 85868
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

		// Token: 0x06014F6E RID: 85870
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06014F6F RID: 85871
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17006FB6 RID: 28598
		// (get) Token: 0x06014F71 RID: 85873
		// (set) Token: 0x06014F70 RID: 85872
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

		// Token: 0x17006FB7 RID: 28599
		// (get) Token: 0x06014F73 RID: 85875
		// (set) Token: 0x06014F72 RID: 85874
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

		// Token: 0x17006FB8 RID: 28600
		// (get) Token: 0x06014F75 RID: 85877
		// (set) Token: 0x06014F74 RID: 85876
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

		// Token: 0x17006FB9 RID: 28601
		// (get) Token: 0x06014F77 RID: 85879
		// (set) Token: 0x06014F76 RID: 85878
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

		// Token: 0x17006FBA RID: 28602
		// (get) Token: 0x06014F79 RID: 85881
		// (set) Token: 0x06014F78 RID: 85880
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

		// Token: 0x17006FBB RID: 28603
		// (get) Token: 0x06014F7B RID: 85883
		// (set) Token: 0x06014F7A RID: 85882
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

		// Token: 0x17006FBC RID: 28604
		// (get) Token: 0x06014F7D RID: 85885
		// (set) Token: 0x06014F7C RID: 85884
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

		// Token: 0x17006FBD RID: 28605
		// (get) Token: 0x06014F7F RID: 85887
		// (set) Token: 0x06014F7E RID: 85886
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

		// Token: 0x17006FBE RID: 28606
		// (get) Token: 0x06014F81 RID: 85889
		// (set) Token: 0x06014F80 RID: 85888
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

		// Token: 0x17006FBF RID: 28607
		// (get) Token: 0x06014F83 RID: 85891
		// (set) Token: 0x06014F82 RID: 85890
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

		// Token: 0x17006FC0 RID: 28608
		// (get) Token: 0x06014F85 RID: 85893
		// (set) Token: 0x06014F84 RID: 85892
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

		// Token: 0x17006FC1 RID: 28609
		// (get) Token: 0x06014F87 RID: 85895
		// (set) Token: 0x06014F86 RID: 85894
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

		// Token: 0x17006FC2 RID: 28610
		// (get) Token: 0x06014F89 RID: 85897
		// (set) Token: 0x06014F88 RID: 85896
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

		// Token: 0x17006FC3 RID: 28611
		// (get) Token: 0x06014F8A RID: 85898
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006FC4 RID: 28612
		// (get) Token: 0x06014F8C RID: 85900
		// (set) Token: 0x06014F8B RID: 85899
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

		// Token: 0x06014F8D RID: 85901
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06014F8E RID: 85902
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06014F8F RID: 85903
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06014F90 RID: 85904
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06014F91 RID: 85905
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17006FC5 RID: 28613
		// (get) Token: 0x06014F93 RID: 85907
		// (set) Token: 0x06014F92 RID: 85906
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

		// Token: 0x06014F94 RID: 85908
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17006FC6 RID: 28614
		// (get) Token: 0x06014F96 RID: 85910
		// (set) Token: 0x06014F95 RID: 85909
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

		// Token: 0x17006FC7 RID: 28615
		// (get) Token: 0x06014F98 RID: 85912
		// (set) Token: 0x06014F97 RID: 85911
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

		// Token: 0x17006FC8 RID: 28616
		// (get) Token: 0x06014F9A RID: 85914
		// (set) Token: 0x06014F99 RID: 85913
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

		// Token: 0x17006FC9 RID: 28617
		// (get) Token: 0x06014F9C RID: 85916
		// (set) Token: 0x06014F9B RID: 85915
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

		// Token: 0x06014F9D RID: 85917
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06014F9E RID: 85918
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06014F9F RID: 85919
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17006FCA RID: 28618
		// (get) Token: 0x06014FA0 RID: 85920
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FCB RID: 28619
		// (get) Token: 0x06014FA1 RID: 85921
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FCC RID: 28620
		// (get) Token: 0x06014FA2 RID: 85922
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FCD RID: 28621
		// (get) Token: 0x06014FA3 RID: 85923
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06014FA4 RID: 85924
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06014FA5 RID: 85925
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17006FCE RID: 28622
		// (get) Token: 0x06014FA6 RID: 85926
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17006FCF RID: 28623
		// (get) Token: 0x06014FA8 RID: 85928
		// (set) Token: 0x06014FA7 RID: 85927
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

		// Token: 0x17006FD0 RID: 28624
		// (get) Token: 0x06014FAA RID: 85930
		// (set) Token: 0x06014FA9 RID: 85929
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

		// Token: 0x17006FD1 RID: 28625
		// (get) Token: 0x06014FAC RID: 85932
		// (set) Token: 0x06014FAB RID: 85931
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

		// Token: 0x17006FD2 RID: 28626
		// (get) Token: 0x06014FAE RID: 85934
		// (set) Token: 0x06014FAD RID: 85933
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

		// Token: 0x17006FD3 RID: 28627
		// (get) Token: 0x06014FB0 RID: 85936
		// (set) Token: 0x06014FAF RID: 85935
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

		// Token: 0x06014FB1 RID: 85937
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17006FD4 RID: 28628
		// (get) Token: 0x06014FB2 RID: 85938
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FD5 RID: 28629
		// (get) Token: 0x06014FB3 RID: 85939
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FD6 RID: 28630
		// (get) Token: 0x06014FB5 RID: 85941
		// (set) Token: 0x06014FB4 RID: 85940
		[DispId(-2147417053)]
		public virtual extern int scrollTop
		{
			[DispId(-2147417053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17006FD7 RID: 28631
		// (get) Token: 0x06014FB7 RID: 85943
		// (set) Token: 0x06014FB6 RID: 85942
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

		// Token: 0x06014FB8 RID: 85944
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17006FD8 RID: 28632
		// (get) Token: 0x06014FBA RID: 85946
		// (set) Token: 0x06014FB9 RID: 85945
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

		// Token: 0x06014FBB RID: 85947
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06014FBC RID: 85948
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06014FBD RID: 85949
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06014FBE RID: 85950
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17006FD9 RID: 28633
		// (get) Token: 0x06014FBF RID: 85951
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06014FC0 RID: 85952
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06014FC1 RID: 85953
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17006FDA RID: 28634
		// (get) Token: 0x06014FC2 RID: 85954
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006FDB RID: 28635
		// (get) Token: 0x06014FC3 RID: 85955
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006FDC RID: 28636
		// (get) Token: 0x06014FC5 RID: 85957
		// (set) Token: 0x06014FC4 RID: 85956
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

		// Token: 0x17006FDD RID: 28637
		// (get) Token: 0x06014FC7 RID: 85959
		// (set) Token: 0x06014FC6 RID: 85958
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

		// Token: 0x17006FDE RID: 28638
		// (get) Token: 0x06014FC8 RID: 85960
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06014FC9 RID: 85961
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06014FCA RID: 85962
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17006FDF RID: 28639
		// (get) Token: 0x06014FCB RID: 85963
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FE0 RID: 28640
		// (get) Token: 0x06014FCC RID: 85964
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FE1 RID: 28641
		// (get) Token: 0x06014FCE RID: 85966
		// (set) Token: 0x06014FCD RID: 85965
		[DispId(-2147412039)]
		public virtual extern object onlayoutcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006FE2 RID: 28642
		// (get) Token: 0x06014FD0 RID: 85968
		// (set) Token: 0x06014FCF RID: 85967
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

		// Token: 0x17006FE3 RID: 28643
		// (get) Token: 0x06014FD2 RID: 85970
		// (set) Token: 0x06014FD1 RID: 85969
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

		// Token: 0x17006FE4 RID: 28644
		// (get) Token: 0x06014FD4 RID: 85972
		// (set) Token: 0x06014FD3 RID: 85971
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

		// Token: 0x06014FD5 RID: 85973
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17006FE5 RID: 28645
		// (get) Token: 0x06014FD7 RID: 85975
		// (set) Token: 0x06014FD6 RID: 85974
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

		// Token: 0x17006FE6 RID: 28646
		// (get) Token: 0x06014FD8 RID: 85976
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FE7 RID: 28647
		// (get) Token: 0x06014FDA RID: 85978
		// (set) Token: 0x06014FD9 RID: 85977
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

		// Token: 0x17006FE8 RID: 28648
		// (get) Token: 0x06014FDC RID: 85980
		// (set) Token: 0x06014FDB RID: 85979
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

		// Token: 0x17006FE9 RID: 28649
		// (get) Token: 0x06014FDD RID: 85981
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FEA RID: 28650
		// (get) Token: 0x06014FDF RID: 85983
		// (set) Token: 0x06014FDE RID: 85982
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

		// Token: 0x17006FEB RID: 28651
		// (get) Token: 0x06014FE1 RID: 85985
		// (set) Token: 0x06014FE0 RID: 85984
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

		// Token: 0x06014FE2 RID: 85986
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17006FEC RID: 28652
		// (get) Token: 0x06014FE4 RID: 85988
		// (set) Token: 0x06014FE3 RID: 85987
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

		// Token: 0x17006FED RID: 28653
		// (get) Token: 0x06014FE6 RID: 85990
		// (set) Token: 0x06014FE5 RID: 85989
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

		// Token: 0x17006FEE RID: 28654
		// (get) Token: 0x06014FE8 RID: 85992
		// (set) Token: 0x06014FE7 RID: 85991
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

		// Token: 0x17006FEF RID: 28655
		// (get) Token: 0x06014FEA RID: 85994
		// (set) Token: 0x06014FE9 RID: 85993
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

		// Token: 0x17006FF0 RID: 28656
		// (get) Token: 0x06014FEC RID: 85996
		// (set) Token: 0x06014FEB RID: 85995
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

		// Token: 0x17006FF1 RID: 28657
		// (get) Token: 0x06014FEE RID: 85998
		// (set) Token: 0x06014FED RID: 85997
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

		// Token: 0x17006FF2 RID: 28658
		// (get) Token: 0x06014FF0 RID: 86000
		// (set) Token: 0x06014FEF RID: 85999
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

		// Token: 0x17006FF3 RID: 28659
		// (get) Token: 0x06014FF2 RID: 86002
		// (set) Token: 0x06014FF1 RID: 86001
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

		// Token: 0x06014FF3 RID: 86003
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17006FF4 RID: 28660
		// (get) Token: 0x06014FF4 RID: 86004
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FF5 RID: 28661
		// (get) Token: 0x06014FF6 RID: 86006
		// (set) Token: 0x06014FF5 RID: 86005
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

		// Token: 0x06014FF7 RID: 86007
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06014FF8 RID: 86008
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06014FF9 RID: 86009
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06014FFA RID: 86010
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17006FF6 RID: 28662
		// (get) Token: 0x06014FFC RID: 86012
		// (set) Token: 0x06014FFB RID: 86011
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

		// Token: 0x17006FF7 RID: 28663
		// (get) Token: 0x06014FFE RID: 86014
		// (set) Token: 0x06014FFD RID: 86013
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

		// Token: 0x17006FF8 RID: 28664
		// (get) Token: 0x06015000 RID: 86016
		// (set) Token: 0x06014FFF RID: 86015
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

		// Token: 0x17006FF9 RID: 28665
		// (get) Token: 0x06015001 RID: 86017
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FFA RID: 28666
		// (get) Token: 0x06015002 RID: 86018
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006FFB RID: 28667
		// (get) Token: 0x06015003 RID: 86019
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006FFC RID: 28668
		// (get) Token: 0x06015004 RID: 86020
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06015005 RID: 86021
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17006FFD RID: 28669
		// (get) Token: 0x06015006 RID: 86022
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006FFE RID: 28670
		// (get) Token: 0x06015007 RID: 86023
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06015008 RID: 86024
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06015009 RID: 86025
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0601500A RID: 86026
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0601500B RID: 86027
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0601500C RID: 86028
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0601500D RID: 86029
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0601500E RID: 86030
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0601500F RID: 86031
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17006FFF RID: 28671
		// (get) Token: 0x06015010 RID: 86032
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007000 RID: 28672
		// (get) Token: 0x06015012 RID: 86034
		// (set) Token: 0x06015011 RID: 86033
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

		// Token: 0x17007001 RID: 28673
		// (get) Token: 0x06015013 RID: 86035
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007002 RID: 28674
		// (get) Token: 0x06015014 RID: 86036
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007003 RID: 28675
		// (get) Token: 0x06015015 RID: 86037
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007004 RID: 28676
		// (get) Token: 0x06015016 RID: 86038
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007005 RID: 28677
		// (get) Token: 0x06015017 RID: 86039
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007006 RID: 28678
		// (get) Token: 0x06015019 RID: 86041
		// (set) Token: 0x06015018 RID: 86040
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

		// Token: 0x17007007 RID: 28679
		// (get) Token: 0x0601501B RID: 86043
		// (set) Token: 0x0601501A RID: 86042
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

		// Token: 0x17007008 RID: 28680
		// (get) Token: 0x0601501D RID: 86045
		// (set) Token: 0x0601501C RID: 86044
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

		// Token: 0x17007009 RID: 28681
		// (get) Token: 0x0601501F RID: 86047
		// (set) Token: 0x0601501E RID: 86046
		[DispId(-2147418039)]
		public virtual extern string align
		{
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06015020 RID: 86048
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06015021 RID: 86049
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06015022 RID: 86050
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700700A RID: 28682
		// (get) Token: 0x06015024 RID: 86052
		// (set) Token: 0x06015023 RID: 86051
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

		// Token: 0x1700700B RID: 28683
		// (get) Token: 0x06015026 RID: 86054
		// (set) Token: 0x06015025 RID: 86053
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

		// Token: 0x1700700C RID: 28684
		// (get) Token: 0x06015027 RID: 86055
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700700D RID: 28685
		// (get) Token: 0x06015028 RID: 86056
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700700E RID: 28686
		// (get) Token: 0x06015029 RID: 86057
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700700F RID: 28687
		// (get) Token: 0x0601502B RID: 86059
		// (set) Token: 0x0601502A RID: 86058
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

		// Token: 0x17007010 RID: 28688
		// (get) Token: 0x0601502D RID: 86061
		// (set) Token: 0x0601502C RID: 86060
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

		// Token: 0x17007011 RID: 28689
		// (get) Token: 0x0601502F RID: 86063
		// (set) Token: 0x0601502E RID: 86062
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

		// Token: 0x17007012 RID: 28690
		// (get) Token: 0x06015031 RID: 86065
		// (set) Token: 0x06015030 RID: 86064
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

		// Token: 0x17007013 RID: 28691
		// (get) Token: 0x06015033 RID: 86067
		// (set) Token: 0x06015032 RID: 86066
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

		// Token: 0x17007014 RID: 28692
		// (get) Token: 0x06015035 RID: 86069
		// (set) Token: 0x06015034 RID: 86068
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

		// Token: 0x17007015 RID: 28693
		// (get) Token: 0x06015037 RID: 86071
		// (set) Token: 0x06015036 RID: 86070
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

		// Token: 0x17007016 RID: 28694
		// (get) Token: 0x06015039 RID: 86073
		// (set) Token: 0x06015038 RID: 86072
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

		// Token: 0x17007017 RID: 28695
		// (get) Token: 0x0601503B RID: 86075
		// (set) Token: 0x0601503A RID: 86074
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

		// Token: 0x17007018 RID: 28696
		// (get) Token: 0x0601503D RID: 86077
		// (set) Token: 0x0601503C RID: 86076
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

		// Token: 0x17007019 RID: 28697
		// (get) Token: 0x0601503F RID: 86079
		// (set) Token: 0x0601503E RID: 86078
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

		// Token: 0x1700701A RID: 28698
		// (get) Token: 0x06015040 RID: 86080
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700701B RID: 28699
		// (get) Token: 0x06015042 RID: 86082
		// (set) Token: 0x06015041 RID: 86081
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

		// Token: 0x1700701C RID: 28700
		// (get) Token: 0x06015044 RID: 86084
		// (set) Token: 0x06015043 RID: 86083
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

		// Token: 0x1700701D RID: 28701
		// (get) Token: 0x06015046 RID: 86086
		// (set) Token: 0x06015045 RID: 86085
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

		// Token: 0x06015047 RID: 86087
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06015048 RID: 86088
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x1700701E RID: 28702
		// (get) Token: 0x06015049 RID: 86089
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700701F RID: 28703
		// (get) Token: 0x0601504A RID: 86090
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007020 RID: 28704
		// (get) Token: 0x0601504C RID: 86092
		// (set) Token: 0x0601504B RID: 86091
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

		// Token: 0x17007021 RID: 28705
		// (get) Token: 0x0601504D RID: 86093
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007022 RID: 28706
		// (get) Token: 0x0601504E RID: 86094
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007023 RID: 28707
		// (get) Token: 0x0601504F RID: 86095
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007024 RID: 28708
		// (get) Token: 0x06015050 RID: 86096
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007025 RID: 28709
		// (get) Token: 0x06015051 RID: 86097
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007026 RID: 28710
		// (get) Token: 0x06015053 RID: 86099
		// (set) Token: 0x06015052 RID: 86098
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

		// Token: 0x17007027 RID: 28711
		// (get) Token: 0x06015055 RID: 86101
		// (set) Token: 0x06015054 RID: 86100
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

		// Token: 0x17007028 RID: 28712
		// (get) Token: 0x06015057 RID: 86103
		// (set) Token: 0x06015056 RID: 86102
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

		// Token: 0x17007029 RID: 28713
		// (get) Token: 0x06015059 RID: 86105
		// (set) Token: 0x06015058 RID: 86104
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

		// Token: 0x0601505A RID: 86106
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0601505B RID: 86107
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x1700702A RID: 28714
		// (get) Token: 0x0601505C RID: 86108
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700702B RID: 28715
		// (get) Token: 0x0601505D RID: 86109
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601505E RID: 86110
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x1700702C RID: 28716
		// (get) Token: 0x0601505F RID: 86111
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700702D RID: 28717
		// (get) Token: 0x06015061 RID: 86113
		// (set) Token: 0x06015060 RID: 86112
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

		// Token: 0x06015062 RID: 86114
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x1700702E RID: 28718
		// (get) Token: 0x06015064 RID: 86116
		// (set) Token: 0x06015063 RID: 86115
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

		// Token: 0x1700702F RID: 28719
		// (get) Token: 0x06015066 RID: 86118
		// (set) Token: 0x06015065 RID: 86117
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

		// Token: 0x17007030 RID: 28720
		// (get) Token: 0x06015068 RID: 86120
		// (set) Token: 0x06015067 RID: 86119
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

		// Token: 0x17007031 RID: 28721
		// (get) Token: 0x0601506A RID: 86122
		// (set) Token: 0x06015069 RID: 86121
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

		// Token: 0x17007032 RID: 28722
		// (get) Token: 0x0601506C RID: 86124
		// (set) Token: 0x0601506B RID: 86123
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

		// Token: 0x17007033 RID: 28723
		// (get) Token: 0x0601506E RID: 86126
		// (set) Token: 0x0601506D RID: 86125
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

		// Token: 0x17007034 RID: 28724
		// (get) Token: 0x06015070 RID: 86128
		// (set) Token: 0x0601506F RID: 86127
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

		// Token: 0x17007035 RID: 28725
		// (get) Token: 0x06015072 RID: 86130
		// (set) Token: 0x06015071 RID: 86129
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

		// Token: 0x17007036 RID: 28726
		// (get) Token: 0x06015074 RID: 86132
		// (set) Token: 0x06015073 RID: 86131
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

		// Token: 0x17007037 RID: 28727
		// (get) Token: 0x06015075 RID: 86133
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007038 RID: 28728
		// (get) Token: 0x06015076 RID: 86134
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007039 RID: 28729
		// (get) Token: 0x06015077 RID: 86135
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06015078 RID: 86136
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06015079 RID: 86137
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x1700703A RID: 28730
		// (get) Token: 0x0601507B RID: 86139
		// (set) Token: 0x0601507A RID: 86138
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

		// Token: 0x0601507C RID: 86140
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0601507D RID: 86141
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700703B RID: 28731
		// (get) Token: 0x0601507F RID: 86143
		// (set) Token: 0x0601507E RID: 86142
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

		// Token: 0x1700703C RID: 28732
		// (get) Token: 0x06015081 RID: 86145
		// (set) Token: 0x06015080 RID: 86144
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

		// Token: 0x1700703D RID: 28733
		// (get) Token: 0x06015083 RID: 86147
		// (set) Token: 0x06015082 RID: 86146
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

		// Token: 0x1700703E RID: 28734
		// (get) Token: 0x06015085 RID: 86149
		// (set) Token: 0x06015084 RID: 86148
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

		// Token: 0x1700703F RID: 28735
		// (get) Token: 0x06015087 RID: 86151
		// (set) Token: 0x06015086 RID: 86150
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

		// Token: 0x17007040 RID: 28736
		// (get) Token: 0x06015089 RID: 86153
		// (set) Token: 0x06015088 RID: 86152
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

		// Token: 0x17007041 RID: 28737
		// (get) Token: 0x0601508B RID: 86155
		// (set) Token: 0x0601508A RID: 86154
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

		// Token: 0x17007042 RID: 28738
		// (get) Token: 0x0601508D RID: 86157
		// (set) Token: 0x0601508C RID: 86156
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

		// Token: 0x17007043 RID: 28739
		// (get) Token: 0x0601508F RID: 86159
		// (set) Token: 0x0601508E RID: 86158
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

		// Token: 0x17007044 RID: 28740
		// (get) Token: 0x06015091 RID: 86161
		// (set) Token: 0x06015090 RID: 86160
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

		// Token: 0x17007045 RID: 28741
		// (get) Token: 0x06015093 RID: 86163
		// (set) Token: 0x06015092 RID: 86162
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

		// Token: 0x17007046 RID: 28742
		// (get) Token: 0x06015095 RID: 86165
		// (set) Token: 0x06015094 RID: 86164
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

		// Token: 0x17007047 RID: 28743
		// (get) Token: 0x06015097 RID: 86167
		// (set) Token: 0x06015096 RID: 86166
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

		// Token: 0x17007048 RID: 28744
		// (get) Token: 0x06015098 RID: 86168
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007049 RID: 28745
		// (get) Token: 0x0601509A RID: 86170
		// (set) Token: 0x06015099 RID: 86169
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

		// Token: 0x0601509B RID: 86171
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0601509C RID: 86172
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0601509D RID: 86173
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0601509E RID: 86174
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0601509F RID: 86175
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700704A RID: 28746
		// (get) Token: 0x060150A1 RID: 86177
		// (set) Token: 0x060150A0 RID: 86176
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

		// Token: 0x060150A2 RID: 86178
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x1700704B RID: 28747
		// (get) Token: 0x060150A4 RID: 86180
		// (set) Token: 0x060150A3 RID: 86179
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

		// Token: 0x1700704C RID: 28748
		// (get) Token: 0x060150A6 RID: 86182
		// (set) Token: 0x060150A5 RID: 86181
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

		// Token: 0x1700704D RID: 28749
		// (get) Token: 0x060150A8 RID: 86184
		// (set) Token: 0x060150A7 RID: 86183
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

		// Token: 0x1700704E RID: 28750
		// (get) Token: 0x060150AA RID: 86186
		// (set) Token: 0x060150A9 RID: 86185
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

		// Token: 0x060150AB RID: 86187
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x060150AC RID: 86188
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060150AD RID: 86189
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700704F RID: 28751
		// (get) Token: 0x060150AE RID: 86190
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007050 RID: 28752
		// (get) Token: 0x060150AF RID: 86191
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007051 RID: 28753
		// (get) Token: 0x060150B0 RID: 86192
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007052 RID: 28754
		// (get) Token: 0x060150B1 RID: 86193
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060150B2 RID: 86194
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060150B3 RID: 86195
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17007053 RID: 28755
		// (get) Token: 0x060150B4 RID: 86196
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007054 RID: 28756
		// (get) Token: 0x060150B6 RID: 86198
		// (set) Token: 0x060150B5 RID: 86197
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

		// Token: 0x17007055 RID: 28757
		// (get) Token: 0x060150B8 RID: 86200
		// (set) Token: 0x060150B7 RID: 86199
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

		// Token: 0x17007056 RID: 28758
		// (get) Token: 0x060150BA RID: 86202
		// (set) Token: 0x060150B9 RID: 86201
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

		// Token: 0x17007057 RID: 28759
		// (get) Token: 0x060150BC RID: 86204
		// (set) Token: 0x060150BB RID: 86203
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

		// Token: 0x17007058 RID: 28760
		// (get) Token: 0x060150BE RID: 86206
		// (set) Token: 0x060150BD RID: 86205
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

		// Token: 0x060150BF RID: 86207
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17007059 RID: 28761
		// (get) Token: 0x060150C0 RID: 86208
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700705A RID: 28762
		// (get) Token: 0x060150C1 RID: 86209
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700705B RID: 28763
		// (get) Token: 0x060150C3 RID: 86211
		// (set) Token: 0x060150C2 RID: 86210
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

		// Token: 0x1700705C RID: 28764
		// (get) Token: 0x060150C5 RID: 86213
		// (set) Token: 0x060150C4 RID: 86212
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

		// Token: 0x060150C6 RID: 86214
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x060150C7 RID: 86215
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x1700705D RID: 28765
		// (get) Token: 0x060150C9 RID: 86217
		// (set) Token: 0x060150C8 RID: 86216
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

		// Token: 0x060150CA RID: 86218
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060150CB RID: 86219
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060150CC RID: 86220
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060150CD RID: 86221
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x1700705E RID: 28766
		// (get) Token: 0x060150CE RID: 86222
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060150CF RID: 86223
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060150D0 RID: 86224
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x1700705F RID: 28767
		// (get) Token: 0x060150D1 RID: 86225
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007060 RID: 28768
		// (get) Token: 0x060150D2 RID: 86226
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007061 RID: 28769
		// (get) Token: 0x060150D4 RID: 86228
		// (set) Token: 0x060150D3 RID: 86227
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

		// Token: 0x17007062 RID: 28770
		// (get) Token: 0x060150D6 RID: 86230
		// (set) Token: 0x060150D5 RID: 86229
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

		// Token: 0x17007063 RID: 28771
		// (get) Token: 0x060150D7 RID: 86231
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060150D8 RID: 86232
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060150D9 RID: 86233
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17007064 RID: 28772
		// (get) Token: 0x060150DA RID: 86234
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007065 RID: 28773
		// (get) Token: 0x060150DB RID: 86235
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007066 RID: 28774
		// (get) Token: 0x060150DD RID: 86237
		// (set) Token: 0x060150DC RID: 86236
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

		// Token: 0x17007067 RID: 28775
		// (get) Token: 0x060150DF RID: 86239
		// (set) Token: 0x060150DE RID: 86238
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

		// Token: 0x17007068 RID: 28776
		// (get) Token: 0x060150E1 RID: 86241
		// (set) Token: 0x060150E0 RID: 86240
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

		// Token: 0x17007069 RID: 28777
		// (get) Token: 0x060150E3 RID: 86243
		// (set) Token: 0x060150E2 RID: 86242
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

		// Token: 0x060150E4 RID: 86244
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x1700706A RID: 28778
		// (get) Token: 0x060150E6 RID: 86246
		// (set) Token: 0x060150E5 RID: 86245
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

		// Token: 0x1700706B RID: 28779
		// (get) Token: 0x060150E7 RID: 86247
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700706C RID: 28780
		// (get) Token: 0x060150E9 RID: 86249
		// (set) Token: 0x060150E8 RID: 86248
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

		// Token: 0x1700706D RID: 28781
		// (get) Token: 0x060150EB RID: 86251
		// (set) Token: 0x060150EA RID: 86250
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

		// Token: 0x1700706E RID: 28782
		// (get) Token: 0x060150EC RID: 86252
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700706F RID: 28783
		// (get) Token: 0x060150EE RID: 86254
		// (set) Token: 0x060150ED RID: 86253
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

		// Token: 0x17007070 RID: 28784
		// (get) Token: 0x060150F0 RID: 86256
		// (set) Token: 0x060150EF RID: 86255
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

		// Token: 0x060150F1 RID: 86257
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17007071 RID: 28785
		// (get) Token: 0x060150F3 RID: 86259
		// (set) Token: 0x060150F2 RID: 86258
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

		// Token: 0x17007072 RID: 28786
		// (get) Token: 0x060150F5 RID: 86261
		// (set) Token: 0x060150F4 RID: 86260
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

		// Token: 0x17007073 RID: 28787
		// (get) Token: 0x060150F7 RID: 86263
		// (set) Token: 0x060150F6 RID: 86262
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

		// Token: 0x17007074 RID: 28788
		// (get) Token: 0x060150F9 RID: 86265
		// (set) Token: 0x060150F8 RID: 86264
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

		// Token: 0x17007075 RID: 28789
		// (get) Token: 0x060150FB RID: 86267
		// (set) Token: 0x060150FA RID: 86266
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

		// Token: 0x17007076 RID: 28790
		// (get) Token: 0x060150FD RID: 86269
		// (set) Token: 0x060150FC RID: 86268
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

		// Token: 0x17007077 RID: 28791
		// (get) Token: 0x060150FF RID: 86271
		// (set) Token: 0x060150FE RID: 86270
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

		// Token: 0x17007078 RID: 28792
		// (get) Token: 0x06015101 RID: 86273
		// (set) Token: 0x06015100 RID: 86272
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

		// Token: 0x06015102 RID: 86274
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17007079 RID: 28793
		// (get) Token: 0x06015103 RID: 86275
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700707A RID: 28794
		// (get) Token: 0x06015105 RID: 86277
		// (set) Token: 0x06015104 RID: 86276
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

		// Token: 0x06015106 RID: 86278
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06015107 RID: 86279
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06015108 RID: 86280
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06015109 RID: 86281
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700707B RID: 28795
		// (get) Token: 0x0601510B RID: 86283
		// (set) Token: 0x0601510A RID: 86282
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

		// Token: 0x1700707C RID: 28796
		// (get) Token: 0x0601510D RID: 86285
		// (set) Token: 0x0601510C RID: 86284
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

		// Token: 0x1700707D RID: 28797
		// (get) Token: 0x0601510F RID: 86287
		// (set) Token: 0x0601510E RID: 86286
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

		// Token: 0x1700707E RID: 28798
		// (get) Token: 0x06015110 RID: 86288
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700707F RID: 28799
		// (get) Token: 0x06015111 RID: 86289
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007080 RID: 28800
		// (get) Token: 0x06015112 RID: 86290
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007081 RID: 28801
		// (get) Token: 0x06015113 RID: 86291
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06015114 RID: 86292
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17007082 RID: 28802
		// (get) Token: 0x06015115 RID: 86293
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007083 RID: 28803
		// (get) Token: 0x06015116 RID: 86294
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06015117 RID: 86295
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06015118 RID: 86296
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06015119 RID: 86297
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0601511A RID: 86298
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0601511B RID: 86299
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0601511C RID: 86300
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0601511D RID: 86301
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0601511E RID: 86302
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17007084 RID: 28804
		// (get) Token: 0x0601511F RID: 86303
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007085 RID: 28805
		// (get) Token: 0x06015121 RID: 86305
		// (set) Token: 0x06015120 RID: 86304
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

		// Token: 0x17007086 RID: 28806
		// (get) Token: 0x06015122 RID: 86306
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007087 RID: 28807
		// (get) Token: 0x06015123 RID: 86307
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007088 RID: 28808
		// (get) Token: 0x06015124 RID: 86308
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007089 RID: 28809
		// (get) Token: 0x06015125 RID: 86309
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700708A RID: 28810
		// (get) Token: 0x06015126 RID: 86310
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700708B RID: 28811
		// (get) Token: 0x06015128 RID: 86312
		// (set) Token: 0x06015127 RID: 86311
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

		// Token: 0x1700708C RID: 28812
		// (get) Token: 0x0601512A RID: 86314
		// (set) Token: 0x06015129 RID: 86313
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

		// Token: 0x1700708D RID: 28813
		// (get) Token: 0x0601512C RID: 86316
		// (set) Token: 0x0601512B RID: 86315
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

		// Token: 0x1700708E RID: 28814
		// (get) Token: 0x0601512E RID: 86318
		// (set) Token: 0x0601512D RID: 86317
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

		// Token: 0x0601512F RID: 86319
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x1700708F RID: 28815
		// (get) Token: 0x06015131 RID: 86321
		// (set) Token: 0x06015130 RID: 86320
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

		// Token: 0x17007090 RID: 28816
		// (get) Token: 0x06015133 RID: 86323
		// (set) Token: 0x06015132 RID: 86322
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

		// Token: 0x17007091 RID: 28817
		// (get) Token: 0x06015135 RID: 86325
		// (set) Token: 0x06015134 RID: 86324
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

		// Token: 0x17007092 RID: 28818
		// (get) Token: 0x06015137 RID: 86327
		// (set) Token: 0x06015136 RID: 86326
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

		// Token: 0x06015138 RID: 86328
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06015139 RID: 86329
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0601513A RID: 86330
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17007093 RID: 28819
		// (get) Token: 0x0601513B RID: 86331
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007094 RID: 28820
		// (get) Token: 0x0601513C RID: 86332
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007095 RID: 28821
		// (get) Token: 0x0601513D RID: 86333
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007096 RID: 28822
		// (get) Token: 0x0601513E RID: 86334
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601513F RID: 86335
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTextContainer_createControlRange();

		// Token: 0x17007097 RID: 28823
		// (get) Token: 0x06015140 RID: 86336
		public virtual extern int IHTMLTextContainer_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007098 RID: 28824
		// (get) Token: 0x06015141 RID: 86337
		public virtual extern int IHTMLTextContainer_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007099 RID: 28825
		// (get) Token: 0x06015143 RID: 86339
		// (set) Token: 0x06015142 RID: 86338
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

		// Token: 0x1700709A RID: 28826
		// (get) Token: 0x06015145 RID: 86341
		// (set) Token: 0x06015144 RID: 86340
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

		// Token: 0x1700709B RID: 28827
		// (get) Token: 0x06015147 RID: 86343
		// (set) Token: 0x06015146 RID: 86342
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

		// Token: 0x1700709C RID: 28828
		// (get) Token: 0x06015149 RID: 86345
		// (set) Token: 0x06015148 RID: 86344
		public virtual extern string IHTMLSpanFlow_align
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

		// Token: 0x14002889 RID: 10377
		// (add) Token: 0x0601514A RID: 86346
		// (remove) Token: 0x0601514B RID: 86347
		public virtual extern event HTMLTextContainerEvents_onhelpEventHandler HTMLTextContainerEvents_Event_onhelp;

		// Token: 0x1400288A RID: 10378
		// (add) Token: 0x0601514C RID: 86348
		// (remove) Token: 0x0601514D RID: 86349
		public virtual extern event HTMLTextContainerEvents_onclickEventHandler HTMLTextContainerEvents_Event_onclick;

		// Token: 0x1400288B RID: 10379
		// (add) Token: 0x0601514E RID: 86350
		// (remove) Token: 0x0601514F RID: 86351
		public virtual extern event HTMLTextContainerEvents_ondblclickEventHandler HTMLTextContainerEvents_Event_ondblclick;

		// Token: 0x1400288C RID: 10380
		// (add) Token: 0x06015150 RID: 86352
		// (remove) Token: 0x06015151 RID: 86353
		public virtual extern event HTMLTextContainerEvents_onkeypressEventHandler HTMLTextContainerEvents_Event_onkeypress;

		// Token: 0x1400288D RID: 10381
		// (add) Token: 0x06015152 RID: 86354
		// (remove) Token: 0x06015153 RID: 86355
		public virtual extern event HTMLTextContainerEvents_onkeydownEventHandler HTMLTextContainerEvents_Event_onkeydown;

		// Token: 0x1400288E RID: 10382
		// (add) Token: 0x06015154 RID: 86356
		// (remove) Token: 0x06015155 RID: 86357
		public virtual extern event HTMLTextContainerEvents_onkeyupEventHandler HTMLTextContainerEvents_Event_onkeyup;

		// Token: 0x1400288F RID: 10383
		// (add) Token: 0x06015156 RID: 86358
		// (remove) Token: 0x06015157 RID: 86359
		public virtual extern event HTMLTextContainerEvents_onmouseoutEventHandler HTMLTextContainerEvents_Event_onmouseout;

		// Token: 0x14002890 RID: 10384
		// (add) Token: 0x06015158 RID: 86360
		// (remove) Token: 0x06015159 RID: 86361
		public virtual extern event HTMLTextContainerEvents_onmouseoverEventHandler HTMLTextContainerEvents_Event_onmouseover;

		// Token: 0x14002891 RID: 10385
		// (add) Token: 0x0601515A RID: 86362
		// (remove) Token: 0x0601515B RID: 86363
		public virtual extern event HTMLTextContainerEvents_onmousemoveEventHandler HTMLTextContainerEvents_Event_onmousemove;

		// Token: 0x14002892 RID: 10386
		// (add) Token: 0x0601515C RID: 86364
		// (remove) Token: 0x0601515D RID: 86365
		public virtual extern event HTMLTextContainerEvents_onmousedownEventHandler HTMLTextContainerEvents_Event_onmousedown;

		// Token: 0x14002893 RID: 10387
		// (add) Token: 0x0601515E RID: 86366
		// (remove) Token: 0x0601515F RID: 86367
		public virtual extern event HTMLTextContainerEvents_onmouseupEventHandler HTMLTextContainerEvents_Event_onmouseup;

		// Token: 0x14002894 RID: 10388
		// (add) Token: 0x06015160 RID: 86368
		// (remove) Token: 0x06015161 RID: 86369
		public virtual extern event HTMLTextContainerEvents_onselectstartEventHandler HTMLTextContainerEvents_Event_onselectstart;

		// Token: 0x14002895 RID: 10389
		// (add) Token: 0x06015162 RID: 86370
		// (remove) Token: 0x06015163 RID: 86371
		public virtual extern event HTMLTextContainerEvents_onfilterchangeEventHandler HTMLTextContainerEvents_Event_onfilterchange;

		// Token: 0x14002896 RID: 10390
		// (add) Token: 0x06015164 RID: 86372
		// (remove) Token: 0x06015165 RID: 86373
		public virtual extern event HTMLTextContainerEvents_ondragstartEventHandler HTMLTextContainerEvents_Event_ondragstart;

		// Token: 0x14002897 RID: 10391
		// (add) Token: 0x06015166 RID: 86374
		// (remove) Token: 0x06015167 RID: 86375
		public virtual extern event HTMLTextContainerEvents_onbeforeupdateEventHandler HTMLTextContainerEvents_Event_onbeforeupdate;

		// Token: 0x14002898 RID: 10392
		// (add) Token: 0x06015168 RID: 86376
		// (remove) Token: 0x06015169 RID: 86377
		public virtual extern event HTMLTextContainerEvents_onafterupdateEventHandler HTMLTextContainerEvents_Event_onafterupdate;

		// Token: 0x14002899 RID: 10393
		// (add) Token: 0x0601516A RID: 86378
		// (remove) Token: 0x0601516B RID: 86379
		public virtual extern event HTMLTextContainerEvents_onerrorupdateEventHandler HTMLTextContainerEvents_Event_onerrorupdate;

		// Token: 0x1400289A RID: 10394
		// (add) Token: 0x0601516C RID: 86380
		// (remove) Token: 0x0601516D RID: 86381
		public virtual extern event HTMLTextContainerEvents_onrowexitEventHandler HTMLTextContainerEvents_Event_onrowexit;

		// Token: 0x1400289B RID: 10395
		// (add) Token: 0x0601516E RID: 86382
		// (remove) Token: 0x0601516F RID: 86383
		public virtual extern event HTMLTextContainerEvents_onrowenterEventHandler HTMLTextContainerEvents_Event_onrowenter;

		// Token: 0x1400289C RID: 10396
		// (add) Token: 0x06015170 RID: 86384
		// (remove) Token: 0x06015171 RID: 86385
		public virtual extern event HTMLTextContainerEvents_ondatasetchangedEventHandler HTMLTextContainerEvents_Event_ondatasetchanged;

		// Token: 0x1400289D RID: 10397
		// (add) Token: 0x06015172 RID: 86386
		// (remove) Token: 0x06015173 RID: 86387
		public virtual extern event HTMLTextContainerEvents_ondataavailableEventHandler HTMLTextContainerEvents_Event_ondataavailable;

		// Token: 0x1400289E RID: 10398
		// (add) Token: 0x06015174 RID: 86388
		// (remove) Token: 0x06015175 RID: 86389
		public virtual extern event HTMLTextContainerEvents_ondatasetcompleteEventHandler HTMLTextContainerEvents_Event_ondatasetcomplete;

		// Token: 0x1400289F RID: 10399
		// (add) Token: 0x06015176 RID: 86390
		// (remove) Token: 0x06015177 RID: 86391
		public virtual extern event HTMLTextContainerEvents_onlosecaptureEventHandler HTMLTextContainerEvents_Event_onlosecapture;

		// Token: 0x140028A0 RID: 10400
		// (add) Token: 0x06015178 RID: 86392
		// (remove) Token: 0x06015179 RID: 86393
		public virtual extern event HTMLTextContainerEvents_onpropertychangeEventHandler HTMLTextContainerEvents_Event_onpropertychange;

		// Token: 0x140028A1 RID: 10401
		// (add) Token: 0x0601517A RID: 86394
		// (remove) Token: 0x0601517B RID: 86395
		public virtual extern event HTMLTextContainerEvents_onscrollEventHandler HTMLTextContainerEvents_Event_onscroll;

		// Token: 0x140028A2 RID: 10402
		// (add) Token: 0x0601517C RID: 86396
		// (remove) Token: 0x0601517D RID: 86397
		public virtual extern event HTMLTextContainerEvents_onfocusEventHandler HTMLTextContainerEvents_Event_onfocus;

		// Token: 0x140028A3 RID: 10403
		// (add) Token: 0x0601517E RID: 86398
		// (remove) Token: 0x0601517F RID: 86399
		public virtual extern event HTMLTextContainerEvents_onblurEventHandler HTMLTextContainerEvents_Event_onblur;

		// Token: 0x140028A4 RID: 10404
		// (add) Token: 0x06015180 RID: 86400
		// (remove) Token: 0x06015181 RID: 86401
		public virtual extern event HTMLTextContainerEvents_onresizeEventHandler HTMLTextContainerEvents_Event_onresize;

		// Token: 0x140028A5 RID: 10405
		// (add) Token: 0x06015182 RID: 86402
		// (remove) Token: 0x06015183 RID: 86403
		public virtual extern event HTMLTextContainerEvents_ondragEventHandler HTMLTextContainerEvents_Event_ondrag;

		// Token: 0x140028A6 RID: 10406
		// (add) Token: 0x06015184 RID: 86404
		// (remove) Token: 0x06015185 RID: 86405
		public virtual extern event HTMLTextContainerEvents_ondragendEventHandler HTMLTextContainerEvents_Event_ondragend;

		// Token: 0x140028A7 RID: 10407
		// (add) Token: 0x06015186 RID: 86406
		// (remove) Token: 0x06015187 RID: 86407
		public virtual extern event HTMLTextContainerEvents_ondragenterEventHandler HTMLTextContainerEvents_Event_ondragenter;

		// Token: 0x140028A8 RID: 10408
		// (add) Token: 0x06015188 RID: 86408
		// (remove) Token: 0x06015189 RID: 86409
		public virtual extern event HTMLTextContainerEvents_ondragoverEventHandler HTMLTextContainerEvents_Event_ondragover;

		// Token: 0x140028A9 RID: 10409
		// (add) Token: 0x0601518A RID: 86410
		// (remove) Token: 0x0601518B RID: 86411
		public virtual extern event HTMLTextContainerEvents_ondragleaveEventHandler HTMLTextContainerEvents_Event_ondragleave;

		// Token: 0x140028AA RID: 10410
		// (add) Token: 0x0601518C RID: 86412
		// (remove) Token: 0x0601518D RID: 86413
		public virtual extern event HTMLTextContainerEvents_ondropEventHandler HTMLTextContainerEvents_Event_ondrop;

		// Token: 0x140028AB RID: 10411
		// (add) Token: 0x0601518E RID: 86414
		// (remove) Token: 0x0601518F RID: 86415
		public virtual extern event HTMLTextContainerEvents_onbeforecutEventHandler HTMLTextContainerEvents_Event_onbeforecut;

		// Token: 0x140028AC RID: 10412
		// (add) Token: 0x06015190 RID: 86416
		// (remove) Token: 0x06015191 RID: 86417
		public virtual extern event HTMLTextContainerEvents_oncutEventHandler HTMLTextContainerEvents_Event_oncut;

		// Token: 0x140028AD RID: 10413
		// (add) Token: 0x06015192 RID: 86418
		// (remove) Token: 0x06015193 RID: 86419
		public virtual extern event HTMLTextContainerEvents_onbeforecopyEventHandler HTMLTextContainerEvents_Event_onbeforecopy;

		// Token: 0x140028AE RID: 10414
		// (add) Token: 0x06015194 RID: 86420
		// (remove) Token: 0x06015195 RID: 86421
		public virtual extern event HTMLTextContainerEvents_oncopyEventHandler HTMLTextContainerEvents_Event_oncopy;

		// Token: 0x140028AF RID: 10415
		// (add) Token: 0x06015196 RID: 86422
		// (remove) Token: 0x06015197 RID: 86423
		public virtual extern event HTMLTextContainerEvents_onbeforepasteEventHandler HTMLTextContainerEvents_Event_onbeforepaste;

		// Token: 0x140028B0 RID: 10416
		// (add) Token: 0x06015198 RID: 86424
		// (remove) Token: 0x06015199 RID: 86425
		public virtual extern event HTMLTextContainerEvents_onpasteEventHandler HTMLTextContainerEvents_Event_onpaste;

		// Token: 0x140028B1 RID: 10417
		// (add) Token: 0x0601519A RID: 86426
		// (remove) Token: 0x0601519B RID: 86427
		public virtual extern event HTMLTextContainerEvents_oncontextmenuEventHandler HTMLTextContainerEvents_Event_oncontextmenu;

		// Token: 0x140028B2 RID: 10418
		// (add) Token: 0x0601519C RID: 86428
		// (remove) Token: 0x0601519D RID: 86429
		public virtual extern event HTMLTextContainerEvents_onrowsdeleteEventHandler HTMLTextContainerEvents_Event_onrowsdelete;

		// Token: 0x140028B3 RID: 10419
		// (add) Token: 0x0601519E RID: 86430
		// (remove) Token: 0x0601519F RID: 86431
		public virtual extern event HTMLTextContainerEvents_onrowsinsertedEventHandler HTMLTextContainerEvents_Event_onrowsinserted;

		// Token: 0x140028B4 RID: 10420
		// (add) Token: 0x060151A0 RID: 86432
		// (remove) Token: 0x060151A1 RID: 86433
		public virtual extern event HTMLTextContainerEvents_oncellchangeEventHandler HTMLTextContainerEvents_Event_oncellchange;

		// Token: 0x140028B5 RID: 10421
		// (add) Token: 0x060151A2 RID: 86434
		// (remove) Token: 0x060151A3 RID: 86435
		public virtual extern event HTMLTextContainerEvents_onreadystatechangeEventHandler HTMLTextContainerEvents_Event_onreadystatechange;

		// Token: 0x140028B6 RID: 10422
		// (add) Token: 0x060151A4 RID: 86436
		// (remove) Token: 0x060151A5 RID: 86437
		public virtual extern event HTMLTextContainerEvents_onbeforeeditfocusEventHandler HTMLTextContainerEvents_Event_onbeforeeditfocus;

		// Token: 0x140028B7 RID: 10423
		// (add) Token: 0x060151A6 RID: 86438
		// (remove) Token: 0x060151A7 RID: 86439
		public virtual extern event HTMLTextContainerEvents_onlayoutcompleteEventHandler HTMLTextContainerEvents_Event_onlayoutcomplete;

		// Token: 0x140028B8 RID: 10424
		// (add) Token: 0x060151A8 RID: 86440
		// (remove) Token: 0x060151A9 RID: 86441
		public virtual extern event HTMLTextContainerEvents_onpageEventHandler HTMLTextContainerEvents_Event_onpage;

		// Token: 0x140028B9 RID: 10425
		// (add) Token: 0x060151AA RID: 86442
		// (remove) Token: 0x060151AB RID: 86443
		public virtual extern event HTMLTextContainerEvents_onbeforedeactivateEventHandler HTMLTextContainerEvents_Event_onbeforedeactivate;

		// Token: 0x140028BA RID: 10426
		// (add) Token: 0x060151AC RID: 86444
		// (remove) Token: 0x060151AD RID: 86445
		public virtual extern event HTMLTextContainerEvents_onbeforeactivateEventHandler HTMLTextContainerEvents_Event_onbeforeactivate;

		// Token: 0x140028BB RID: 10427
		// (add) Token: 0x060151AE RID: 86446
		// (remove) Token: 0x060151AF RID: 86447
		public virtual extern event HTMLTextContainerEvents_onmoveEventHandler HTMLTextContainerEvents_Event_onmove;

		// Token: 0x140028BC RID: 10428
		// (add) Token: 0x060151B0 RID: 86448
		// (remove) Token: 0x060151B1 RID: 86449
		public virtual extern event HTMLTextContainerEvents_oncontrolselectEventHandler HTMLTextContainerEvents_Event_oncontrolselect;

		// Token: 0x140028BD RID: 10429
		// (add) Token: 0x060151B2 RID: 86450
		// (remove) Token: 0x060151B3 RID: 86451
		public virtual extern event HTMLTextContainerEvents_onmovestartEventHandler HTMLTextContainerEvents_Event_onmovestart;

		// Token: 0x140028BE RID: 10430
		// (add) Token: 0x060151B4 RID: 86452
		// (remove) Token: 0x060151B5 RID: 86453
		public virtual extern event HTMLTextContainerEvents_onmoveendEventHandler HTMLTextContainerEvents_Event_onmoveend;

		// Token: 0x140028BF RID: 10431
		// (add) Token: 0x060151B6 RID: 86454
		// (remove) Token: 0x060151B7 RID: 86455
		public virtual extern event HTMLTextContainerEvents_onresizestartEventHandler HTMLTextContainerEvents_Event_onresizestart;

		// Token: 0x140028C0 RID: 10432
		// (add) Token: 0x060151B8 RID: 86456
		// (remove) Token: 0x060151B9 RID: 86457
		public virtual extern event HTMLTextContainerEvents_onresizeendEventHandler HTMLTextContainerEvents_Event_onresizeend;

		// Token: 0x140028C1 RID: 10433
		// (add) Token: 0x060151BA RID: 86458
		// (remove) Token: 0x060151BB RID: 86459
		public virtual extern event HTMLTextContainerEvents_onmouseenterEventHandler HTMLTextContainerEvents_Event_onmouseenter;

		// Token: 0x140028C2 RID: 10434
		// (add) Token: 0x060151BC RID: 86460
		// (remove) Token: 0x060151BD RID: 86461
		public virtual extern event HTMLTextContainerEvents_onmouseleaveEventHandler HTMLTextContainerEvents_Event_onmouseleave;

		// Token: 0x140028C3 RID: 10435
		// (add) Token: 0x060151BE RID: 86462
		// (remove) Token: 0x060151BF RID: 86463
		public virtual extern event HTMLTextContainerEvents_onmousewheelEventHandler HTMLTextContainerEvents_Event_onmousewheel;

		// Token: 0x140028C4 RID: 10436
		// (add) Token: 0x060151C0 RID: 86464
		// (remove) Token: 0x060151C1 RID: 86465
		public virtual extern event HTMLTextContainerEvents_onactivateEventHandler HTMLTextContainerEvents_Event_onactivate;

		// Token: 0x140028C5 RID: 10437
		// (add) Token: 0x060151C2 RID: 86466
		// (remove) Token: 0x060151C3 RID: 86467
		public virtual extern event HTMLTextContainerEvents_ondeactivateEventHandler HTMLTextContainerEvents_Event_ondeactivate;

		// Token: 0x140028C6 RID: 10438
		// (add) Token: 0x060151C4 RID: 86468
		// (remove) Token: 0x060151C5 RID: 86469
		public virtual extern event HTMLTextContainerEvents_onfocusinEventHandler HTMLTextContainerEvents_Event_onfocusin;

		// Token: 0x140028C7 RID: 10439
		// (add) Token: 0x060151C6 RID: 86470
		// (remove) Token: 0x060151C7 RID: 86471
		public virtual extern event HTMLTextContainerEvents_onfocusoutEventHandler HTMLTextContainerEvents_Event_onfocusout;

		// Token: 0x140028C8 RID: 10440
		// (add) Token: 0x060151C8 RID: 86472
		// (remove) Token: 0x060151C9 RID: 86473
		public virtual extern event HTMLTextContainerEvents_onchangeEventHandler onchange;

		// Token: 0x140028C9 RID: 10441
		// (add) Token: 0x060151CA RID: 86474
		// (remove) Token: 0x060151CB RID: 86475
		public virtual extern event HTMLTextContainerEvents_onselectEventHandler onselect;

		// Token: 0x140028CA RID: 10442
		// (add) Token: 0x060151CC RID: 86476
		// (remove) Token: 0x060151CD RID: 86477
		public virtual extern event HTMLTextContainerEvents2_onhelpEventHandler HTMLTextContainerEvents2_Event_onhelp;

		// Token: 0x140028CB RID: 10443
		// (add) Token: 0x060151CE RID: 86478
		// (remove) Token: 0x060151CF RID: 86479
		public virtual extern event HTMLTextContainerEvents2_onclickEventHandler HTMLTextContainerEvents2_Event_onclick;

		// Token: 0x140028CC RID: 10444
		// (add) Token: 0x060151D0 RID: 86480
		// (remove) Token: 0x060151D1 RID: 86481
		public virtual extern event HTMLTextContainerEvents2_ondblclickEventHandler HTMLTextContainerEvents2_Event_ondblclick;

		// Token: 0x140028CD RID: 10445
		// (add) Token: 0x060151D2 RID: 86482
		// (remove) Token: 0x060151D3 RID: 86483
		public virtual extern event HTMLTextContainerEvents2_onkeypressEventHandler HTMLTextContainerEvents2_Event_onkeypress;

		// Token: 0x140028CE RID: 10446
		// (add) Token: 0x060151D4 RID: 86484
		// (remove) Token: 0x060151D5 RID: 86485
		public virtual extern event HTMLTextContainerEvents2_onkeydownEventHandler HTMLTextContainerEvents2_Event_onkeydown;

		// Token: 0x140028CF RID: 10447
		// (add) Token: 0x060151D6 RID: 86486
		// (remove) Token: 0x060151D7 RID: 86487
		public virtual extern event HTMLTextContainerEvents2_onkeyupEventHandler HTMLTextContainerEvents2_Event_onkeyup;

		// Token: 0x140028D0 RID: 10448
		// (add) Token: 0x060151D8 RID: 86488
		// (remove) Token: 0x060151D9 RID: 86489
		public virtual extern event HTMLTextContainerEvents2_onmouseoutEventHandler HTMLTextContainerEvents2_Event_onmouseout;

		// Token: 0x140028D1 RID: 10449
		// (add) Token: 0x060151DA RID: 86490
		// (remove) Token: 0x060151DB RID: 86491
		public virtual extern event HTMLTextContainerEvents2_onmouseoverEventHandler HTMLTextContainerEvents2_Event_onmouseover;

		// Token: 0x140028D2 RID: 10450
		// (add) Token: 0x060151DC RID: 86492
		// (remove) Token: 0x060151DD RID: 86493
		public virtual extern event HTMLTextContainerEvents2_onmousemoveEventHandler HTMLTextContainerEvents2_Event_onmousemove;

		// Token: 0x140028D3 RID: 10451
		// (add) Token: 0x060151DE RID: 86494
		// (remove) Token: 0x060151DF RID: 86495
		public virtual extern event HTMLTextContainerEvents2_onmousedownEventHandler HTMLTextContainerEvents2_Event_onmousedown;

		// Token: 0x140028D4 RID: 10452
		// (add) Token: 0x060151E0 RID: 86496
		// (remove) Token: 0x060151E1 RID: 86497
		public virtual extern event HTMLTextContainerEvents2_onmouseupEventHandler HTMLTextContainerEvents2_Event_onmouseup;

		// Token: 0x140028D5 RID: 10453
		// (add) Token: 0x060151E2 RID: 86498
		// (remove) Token: 0x060151E3 RID: 86499
		public virtual extern event HTMLTextContainerEvents2_onselectstartEventHandler HTMLTextContainerEvents2_Event_onselectstart;

		// Token: 0x140028D6 RID: 10454
		// (add) Token: 0x060151E4 RID: 86500
		// (remove) Token: 0x060151E5 RID: 86501
		public virtual extern event HTMLTextContainerEvents2_onfilterchangeEventHandler HTMLTextContainerEvents2_Event_onfilterchange;

		// Token: 0x140028D7 RID: 10455
		// (add) Token: 0x060151E6 RID: 86502
		// (remove) Token: 0x060151E7 RID: 86503
		public virtual extern event HTMLTextContainerEvents2_ondragstartEventHandler HTMLTextContainerEvents2_Event_ondragstart;

		// Token: 0x140028D8 RID: 10456
		// (add) Token: 0x060151E8 RID: 86504
		// (remove) Token: 0x060151E9 RID: 86505
		public virtual extern event HTMLTextContainerEvents2_onbeforeupdateEventHandler HTMLTextContainerEvents2_Event_onbeforeupdate;

		// Token: 0x140028D9 RID: 10457
		// (add) Token: 0x060151EA RID: 86506
		// (remove) Token: 0x060151EB RID: 86507
		public virtual extern event HTMLTextContainerEvents2_onafterupdateEventHandler HTMLTextContainerEvents2_Event_onafterupdate;

		// Token: 0x140028DA RID: 10458
		// (add) Token: 0x060151EC RID: 86508
		// (remove) Token: 0x060151ED RID: 86509
		public virtual extern event HTMLTextContainerEvents2_onerrorupdateEventHandler HTMLTextContainerEvents2_Event_onerrorupdate;

		// Token: 0x140028DB RID: 10459
		// (add) Token: 0x060151EE RID: 86510
		// (remove) Token: 0x060151EF RID: 86511
		public virtual extern event HTMLTextContainerEvents2_onrowexitEventHandler HTMLTextContainerEvents2_Event_onrowexit;

		// Token: 0x140028DC RID: 10460
		// (add) Token: 0x060151F0 RID: 86512
		// (remove) Token: 0x060151F1 RID: 86513
		public virtual extern event HTMLTextContainerEvents2_onrowenterEventHandler HTMLTextContainerEvents2_Event_onrowenter;

		// Token: 0x140028DD RID: 10461
		// (add) Token: 0x060151F2 RID: 86514
		// (remove) Token: 0x060151F3 RID: 86515
		public virtual extern event HTMLTextContainerEvents2_ondatasetchangedEventHandler HTMLTextContainerEvents2_Event_ondatasetchanged;

		// Token: 0x140028DE RID: 10462
		// (add) Token: 0x060151F4 RID: 86516
		// (remove) Token: 0x060151F5 RID: 86517
		public virtual extern event HTMLTextContainerEvents2_ondataavailableEventHandler HTMLTextContainerEvents2_Event_ondataavailable;

		// Token: 0x140028DF RID: 10463
		// (add) Token: 0x060151F6 RID: 86518
		// (remove) Token: 0x060151F7 RID: 86519
		public virtual extern event HTMLTextContainerEvents2_ondatasetcompleteEventHandler HTMLTextContainerEvents2_Event_ondatasetcomplete;

		// Token: 0x140028E0 RID: 10464
		// (add) Token: 0x060151F8 RID: 86520
		// (remove) Token: 0x060151F9 RID: 86521
		public virtual extern event HTMLTextContainerEvents2_onlosecaptureEventHandler HTMLTextContainerEvents2_Event_onlosecapture;

		// Token: 0x140028E1 RID: 10465
		// (add) Token: 0x060151FA RID: 86522
		// (remove) Token: 0x060151FB RID: 86523
		public virtual extern event HTMLTextContainerEvents2_onpropertychangeEventHandler HTMLTextContainerEvents2_Event_onpropertychange;

		// Token: 0x140028E2 RID: 10466
		// (add) Token: 0x060151FC RID: 86524
		// (remove) Token: 0x060151FD RID: 86525
		public virtual extern event HTMLTextContainerEvents2_onscrollEventHandler HTMLTextContainerEvents2_Event_onscroll;

		// Token: 0x140028E3 RID: 10467
		// (add) Token: 0x060151FE RID: 86526
		// (remove) Token: 0x060151FF RID: 86527
		public virtual extern event HTMLTextContainerEvents2_onfocusEventHandler HTMLTextContainerEvents2_Event_onfocus;

		// Token: 0x140028E4 RID: 10468
		// (add) Token: 0x06015200 RID: 86528
		// (remove) Token: 0x06015201 RID: 86529
		public virtual extern event HTMLTextContainerEvents2_onblurEventHandler HTMLTextContainerEvents2_Event_onblur;

		// Token: 0x140028E5 RID: 10469
		// (add) Token: 0x06015202 RID: 86530
		// (remove) Token: 0x06015203 RID: 86531
		public virtual extern event HTMLTextContainerEvents2_onresizeEventHandler HTMLTextContainerEvents2_Event_onresize;

		// Token: 0x140028E6 RID: 10470
		// (add) Token: 0x06015204 RID: 86532
		// (remove) Token: 0x06015205 RID: 86533
		public virtual extern event HTMLTextContainerEvents2_ondragEventHandler HTMLTextContainerEvents2_Event_ondrag;

		// Token: 0x140028E7 RID: 10471
		// (add) Token: 0x06015206 RID: 86534
		// (remove) Token: 0x06015207 RID: 86535
		public virtual extern event HTMLTextContainerEvents2_ondragendEventHandler HTMLTextContainerEvents2_Event_ondragend;

		// Token: 0x140028E8 RID: 10472
		// (add) Token: 0x06015208 RID: 86536
		// (remove) Token: 0x06015209 RID: 86537
		public virtual extern event HTMLTextContainerEvents2_ondragenterEventHandler HTMLTextContainerEvents2_Event_ondragenter;

		// Token: 0x140028E9 RID: 10473
		// (add) Token: 0x0601520A RID: 86538
		// (remove) Token: 0x0601520B RID: 86539
		public virtual extern event HTMLTextContainerEvents2_ondragoverEventHandler HTMLTextContainerEvents2_Event_ondragover;

		// Token: 0x140028EA RID: 10474
		// (add) Token: 0x0601520C RID: 86540
		// (remove) Token: 0x0601520D RID: 86541
		public virtual extern event HTMLTextContainerEvents2_ondragleaveEventHandler HTMLTextContainerEvents2_Event_ondragleave;

		// Token: 0x140028EB RID: 10475
		// (add) Token: 0x0601520E RID: 86542
		// (remove) Token: 0x0601520F RID: 86543
		public virtual extern event HTMLTextContainerEvents2_ondropEventHandler HTMLTextContainerEvents2_Event_ondrop;

		// Token: 0x140028EC RID: 10476
		// (add) Token: 0x06015210 RID: 86544
		// (remove) Token: 0x06015211 RID: 86545
		public virtual extern event HTMLTextContainerEvents2_onbeforecutEventHandler HTMLTextContainerEvents2_Event_onbeforecut;

		// Token: 0x140028ED RID: 10477
		// (add) Token: 0x06015212 RID: 86546
		// (remove) Token: 0x06015213 RID: 86547
		public virtual extern event HTMLTextContainerEvents2_oncutEventHandler HTMLTextContainerEvents2_Event_oncut;

		// Token: 0x140028EE RID: 10478
		// (add) Token: 0x06015214 RID: 86548
		// (remove) Token: 0x06015215 RID: 86549
		public virtual extern event HTMLTextContainerEvents2_onbeforecopyEventHandler HTMLTextContainerEvents2_Event_onbeforecopy;

		// Token: 0x140028EF RID: 10479
		// (add) Token: 0x06015216 RID: 86550
		// (remove) Token: 0x06015217 RID: 86551
		public virtual extern event HTMLTextContainerEvents2_oncopyEventHandler HTMLTextContainerEvents2_Event_oncopy;

		// Token: 0x140028F0 RID: 10480
		// (add) Token: 0x06015218 RID: 86552
		// (remove) Token: 0x06015219 RID: 86553
		public virtual extern event HTMLTextContainerEvents2_onbeforepasteEventHandler HTMLTextContainerEvents2_Event_onbeforepaste;

		// Token: 0x140028F1 RID: 10481
		// (add) Token: 0x0601521A RID: 86554
		// (remove) Token: 0x0601521B RID: 86555
		public virtual extern event HTMLTextContainerEvents2_onpasteEventHandler HTMLTextContainerEvents2_Event_onpaste;

		// Token: 0x140028F2 RID: 10482
		// (add) Token: 0x0601521C RID: 86556
		// (remove) Token: 0x0601521D RID: 86557
		public virtual extern event HTMLTextContainerEvents2_oncontextmenuEventHandler HTMLTextContainerEvents2_Event_oncontextmenu;

		// Token: 0x140028F3 RID: 10483
		// (add) Token: 0x0601521E RID: 86558
		// (remove) Token: 0x0601521F RID: 86559
		public virtual extern event HTMLTextContainerEvents2_onrowsdeleteEventHandler HTMLTextContainerEvents2_Event_onrowsdelete;

		// Token: 0x140028F4 RID: 10484
		// (add) Token: 0x06015220 RID: 86560
		// (remove) Token: 0x06015221 RID: 86561
		public virtual extern event HTMLTextContainerEvents2_onrowsinsertedEventHandler HTMLTextContainerEvents2_Event_onrowsinserted;

		// Token: 0x140028F5 RID: 10485
		// (add) Token: 0x06015222 RID: 86562
		// (remove) Token: 0x06015223 RID: 86563
		public virtual extern event HTMLTextContainerEvents2_oncellchangeEventHandler HTMLTextContainerEvents2_Event_oncellchange;

		// Token: 0x140028F6 RID: 10486
		// (add) Token: 0x06015224 RID: 86564
		// (remove) Token: 0x06015225 RID: 86565
		public virtual extern event HTMLTextContainerEvents2_onreadystatechangeEventHandler HTMLTextContainerEvents2_Event_onreadystatechange;

		// Token: 0x140028F7 RID: 10487
		// (add) Token: 0x06015226 RID: 86566
		// (remove) Token: 0x06015227 RID: 86567
		public virtual extern event HTMLTextContainerEvents2_onlayoutcompleteEventHandler HTMLTextContainerEvents2_Event_onlayoutcomplete;

		// Token: 0x140028F8 RID: 10488
		// (add) Token: 0x06015228 RID: 86568
		// (remove) Token: 0x06015229 RID: 86569
		public virtual extern event HTMLTextContainerEvents2_onpageEventHandler HTMLTextContainerEvents2_Event_onpage;

		// Token: 0x140028F9 RID: 10489
		// (add) Token: 0x0601522A RID: 86570
		// (remove) Token: 0x0601522B RID: 86571
		public virtual extern event HTMLTextContainerEvents2_onmouseenterEventHandler HTMLTextContainerEvents2_Event_onmouseenter;

		// Token: 0x140028FA RID: 10490
		// (add) Token: 0x0601522C RID: 86572
		// (remove) Token: 0x0601522D RID: 86573
		public virtual extern event HTMLTextContainerEvents2_onmouseleaveEventHandler HTMLTextContainerEvents2_Event_onmouseleave;

		// Token: 0x140028FB RID: 10491
		// (add) Token: 0x0601522E RID: 86574
		// (remove) Token: 0x0601522F RID: 86575
		public virtual extern event HTMLTextContainerEvents2_onactivateEventHandler HTMLTextContainerEvents2_Event_onactivate;

		// Token: 0x140028FC RID: 10492
		// (add) Token: 0x06015230 RID: 86576
		// (remove) Token: 0x06015231 RID: 86577
		public virtual extern event HTMLTextContainerEvents2_ondeactivateEventHandler HTMLTextContainerEvents2_Event_ondeactivate;

		// Token: 0x140028FD RID: 10493
		// (add) Token: 0x06015232 RID: 86578
		// (remove) Token: 0x06015233 RID: 86579
		public virtual extern event HTMLTextContainerEvents2_onbeforedeactivateEventHandler HTMLTextContainerEvents2_Event_onbeforedeactivate;

		// Token: 0x140028FE RID: 10494
		// (add) Token: 0x06015234 RID: 86580
		// (remove) Token: 0x06015235 RID: 86581
		public virtual extern event HTMLTextContainerEvents2_onbeforeactivateEventHandler HTMLTextContainerEvents2_Event_onbeforeactivate;

		// Token: 0x140028FF RID: 10495
		// (add) Token: 0x06015236 RID: 86582
		// (remove) Token: 0x06015237 RID: 86583
		public virtual extern event HTMLTextContainerEvents2_onfocusinEventHandler HTMLTextContainerEvents2_Event_onfocusin;

		// Token: 0x14002900 RID: 10496
		// (add) Token: 0x06015238 RID: 86584
		// (remove) Token: 0x06015239 RID: 86585
		public virtual extern event HTMLTextContainerEvents2_onfocusoutEventHandler HTMLTextContainerEvents2_Event_onfocusout;

		// Token: 0x14002901 RID: 10497
		// (add) Token: 0x0601523A RID: 86586
		// (remove) Token: 0x0601523B RID: 86587
		public virtual extern event HTMLTextContainerEvents2_onmoveEventHandler HTMLTextContainerEvents2_Event_onmove;

		// Token: 0x14002902 RID: 10498
		// (add) Token: 0x0601523C RID: 86588
		// (remove) Token: 0x0601523D RID: 86589
		public virtual extern event HTMLTextContainerEvents2_oncontrolselectEventHandler HTMLTextContainerEvents2_Event_oncontrolselect;

		// Token: 0x14002903 RID: 10499
		// (add) Token: 0x0601523E RID: 86590
		// (remove) Token: 0x0601523F RID: 86591
		public virtual extern event HTMLTextContainerEvents2_onmovestartEventHandler HTMLTextContainerEvents2_Event_onmovestart;

		// Token: 0x14002904 RID: 10500
		// (add) Token: 0x06015240 RID: 86592
		// (remove) Token: 0x06015241 RID: 86593
		public virtual extern event HTMLTextContainerEvents2_onmoveendEventHandler HTMLTextContainerEvents2_Event_onmoveend;

		// Token: 0x14002905 RID: 10501
		// (add) Token: 0x06015242 RID: 86594
		// (remove) Token: 0x06015243 RID: 86595
		public virtual extern event HTMLTextContainerEvents2_onresizestartEventHandler HTMLTextContainerEvents2_Event_onresizestart;

		// Token: 0x14002906 RID: 10502
		// (add) Token: 0x06015244 RID: 86596
		// (remove) Token: 0x06015245 RID: 86597
		public virtual extern event HTMLTextContainerEvents2_onresizeendEventHandler HTMLTextContainerEvents2_Event_onresizeend;

		// Token: 0x14002907 RID: 10503
		// (add) Token: 0x06015246 RID: 86598
		// (remove) Token: 0x06015247 RID: 86599
		public virtual extern event HTMLTextContainerEvents2_onmousewheelEventHandler HTMLTextContainerEvents2_Event_onmousewheel;

		// Token: 0x14002908 RID: 10504
		// (add) Token: 0x06015248 RID: 86600
		// (remove) Token: 0x06015249 RID: 86601
		public virtual extern event HTMLTextContainerEvents2_onchangeEventHandler HTMLTextContainerEvents2_Event_onchange;

		// Token: 0x14002909 RID: 10505
		// (add) Token: 0x0601524A RID: 86602
		// (remove) Token: 0x0601524B RID: 86603
		public virtual extern event HTMLTextContainerEvents2_onselectEventHandler HTMLTextContainerEvents2_Event_onselect;
	}
}
