using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000959 RID: 2393
	[DefaultMember("href")]
	[ClassInterface(0)]
	[TypeLibType(2)]
	[ComSourceInterfaces("mshtml.HTMLAreaEvents\0mshtml.HTMLAreaEvents2\0\0")]
	[Guid("3050F283-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLAreaElementClass : DispHTMLAreaElement, HTMLAreaElement, HTMLAreaEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLAreaElement, HTMLAreaEvents2_Event
	{
		// Token: 0x0600EDFB RID: 60923
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLAreaElementClass();

		// Token: 0x0600EDFC RID: 60924
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600EDFD RID: 60925
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600EDFE RID: 60926
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17004DED RID: 19949
		// (get) Token: 0x0600EE00 RID: 60928
		// (set) Token: 0x0600EDFF RID: 60927
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

		// Token: 0x17004DEE RID: 19950
		// (get) Token: 0x0600EE02 RID: 60930
		// (set) Token: 0x0600EE01 RID: 60929
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

		// Token: 0x17004DEF RID: 19951
		// (get) Token: 0x0600EE03 RID: 60931
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004DF0 RID: 19952
		// (get) Token: 0x0600EE04 RID: 60932
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004DF1 RID: 19953
		// (get) Token: 0x0600EE05 RID: 60933
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004DF2 RID: 19954
		// (get) Token: 0x0600EE07 RID: 60935
		// (set) Token: 0x0600EE06 RID: 60934
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

		// Token: 0x17004DF3 RID: 19955
		// (get) Token: 0x0600EE09 RID: 60937
		// (set) Token: 0x0600EE08 RID: 60936
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

		// Token: 0x17004DF4 RID: 19956
		// (get) Token: 0x0600EE0B RID: 60939
		// (set) Token: 0x0600EE0A RID: 60938
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

		// Token: 0x17004DF5 RID: 19957
		// (get) Token: 0x0600EE0D RID: 60941
		// (set) Token: 0x0600EE0C RID: 60940
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

		// Token: 0x17004DF6 RID: 19958
		// (get) Token: 0x0600EE0F RID: 60943
		// (set) Token: 0x0600EE0E RID: 60942
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

		// Token: 0x17004DF7 RID: 19959
		// (get) Token: 0x0600EE11 RID: 60945
		// (set) Token: 0x0600EE10 RID: 60944
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

		// Token: 0x17004DF8 RID: 19960
		// (get) Token: 0x0600EE13 RID: 60947
		// (set) Token: 0x0600EE12 RID: 60946
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

		// Token: 0x17004DF9 RID: 19961
		// (get) Token: 0x0600EE15 RID: 60949
		// (set) Token: 0x0600EE14 RID: 60948
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

		// Token: 0x17004DFA RID: 19962
		// (get) Token: 0x0600EE17 RID: 60951
		// (set) Token: 0x0600EE16 RID: 60950
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

		// Token: 0x17004DFB RID: 19963
		// (get) Token: 0x0600EE19 RID: 60953
		// (set) Token: 0x0600EE18 RID: 60952
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

		// Token: 0x17004DFC RID: 19964
		// (get) Token: 0x0600EE1B RID: 60955
		// (set) Token: 0x0600EE1A RID: 60954
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

		// Token: 0x17004DFD RID: 19965
		// (get) Token: 0x0600EE1C RID: 60956
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004DFE RID: 19966
		// (get) Token: 0x0600EE1E RID: 60958
		// (set) Token: 0x0600EE1D RID: 60957
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

		// Token: 0x17004DFF RID: 19967
		// (get) Token: 0x0600EE20 RID: 60960
		// (set) Token: 0x0600EE1F RID: 60959
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E00 RID: 19968
		// (get) Token: 0x0600EE22 RID: 60962
		// (set) Token: 0x0600EE21 RID: 60961
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

		// Token: 0x0600EE23 RID: 60963
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600EE24 RID: 60964
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17004E01 RID: 19969
		// (get) Token: 0x0600EE25 RID: 60965
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E02 RID: 19970
		// (get) Token: 0x0600EE26 RID: 60966
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004E03 RID: 19971
		// (get) Token: 0x0600EE28 RID: 60968
		// (set) Token: 0x0600EE27 RID: 60967
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

		// Token: 0x17004E04 RID: 19972
		// (get) Token: 0x0600EE29 RID: 60969
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E05 RID: 19973
		// (get) Token: 0x0600EE2A RID: 60970
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E06 RID: 19974
		// (get) Token: 0x0600EE2B RID: 60971
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E07 RID: 19975
		// (get) Token: 0x0600EE2C RID: 60972
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E08 RID: 19976
		// (get) Token: 0x0600EE2D RID: 60973
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E09 RID: 19977
		// (get) Token: 0x0600EE2F RID: 60975
		// (set) Token: 0x0600EE2E RID: 60974
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

		// Token: 0x17004E0A RID: 19978
		// (get) Token: 0x0600EE31 RID: 60977
		// (set) Token: 0x0600EE30 RID: 60976
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

		// Token: 0x17004E0B RID: 19979
		// (get) Token: 0x0600EE33 RID: 60979
		// (set) Token: 0x0600EE32 RID: 60978
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

		// Token: 0x17004E0C RID: 19980
		// (get) Token: 0x0600EE35 RID: 60981
		// (set) Token: 0x0600EE34 RID: 60980
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

		// Token: 0x0600EE36 RID: 60982
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600EE37 RID: 60983
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17004E0D RID: 19981
		// (get) Token: 0x0600EE38 RID: 60984
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E0E RID: 19982
		// (get) Token: 0x0600EE39 RID: 60985
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600EE3A RID: 60986
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17004E0F RID: 19983
		// (get) Token: 0x0600EE3B RID: 60987
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E10 RID: 19984
		// (get) Token: 0x0600EE3D RID: 60989
		// (set) Token: 0x0600EE3C RID: 60988
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

		// Token: 0x0600EE3E RID: 60990
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17004E11 RID: 19985
		// (get) Token: 0x0600EE40 RID: 60992
		// (set) Token: 0x0600EE3F RID: 60991
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

		// Token: 0x17004E12 RID: 19986
		// (get) Token: 0x0600EE42 RID: 60994
		// (set) Token: 0x0600EE41 RID: 60993
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

		// Token: 0x17004E13 RID: 19987
		// (get) Token: 0x0600EE44 RID: 60996
		// (set) Token: 0x0600EE43 RID: 60995
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

		// Token: 0x17004E14 RID: 19988
		// (get) Token: 0x0600EE46 RID: 60998
		// (set) Token: 0x0600EE45 RID: 60997
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

		// Token: 0x17004E15 RID: 19989
		// (get) Token: 0x0600EE48 RID: 61000
		// (set) Token: 0x0600EE47 RID: 60999
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

		// Token: 0x17004E16 RID: 19990
		// (get) Token: 0x0600EE4A RID: 61002
		// (set) Token: 0x0600EE49 RID: 61001
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

		// Token: 0x17004E17 RID: 19991
		// (get) Token: 0x0600EE4C RID: 61004
		// (set) Token: 0x0600EE4B RID: 61003
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

		// Token: 0x17004E18 RID: 19992
		// (get) Token: 0x0600EE4E RID: 61006
		// (set) Token: 0x0600EE4D RID: 61005
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

		// Token: 0x17004E19 RID: 19993
		// (get) Token: 0x0600EE50 RID: 61008
		// (set) Token: 0x0600EE4F RID: 61007
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

		// Token: 0x17004E1A RID: 19994
		// (get) Token: 0x0600EE51 RID: 61009
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004E1B RID: 19995
		// (get) Token: 0x0600EE52 RID: 61010
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004E1C RID: 19996
		// (get) Token: 0x0600EE53 RID: 61011
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600EE54 RID: 61012
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0600EE55 RID: 61013
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17004E1D RID: 19997
		// (get) Token: 0x0600EE57 RID: 61015
		// (set) Token: 0x0600EE56 RID: 61014
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

		// Token: 0x0600EE58 RID: 61016
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600EE59 RID: 61017
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17004E1E RID: 19998
		// (get) Token: 0x0600EE5B RID: 61019
		// (set) Token: 0x0600EE5A RID: 61018
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

		// Token: 0x17004E1F RID: 19999
		// (get) Token: 0x0600EE5D RID: 61021
		// (set) Token: 0x0600EE5C RID: 61020
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

		// Token: 0x17004E20 RID: 20000
		// (get) Token: 0x0600EE5F RID: 61023
		// (set) Token: 0x0600EE5E RID: 61022
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

		// Token: 0x17004E21 RID: 20001
		// (get) Token: 0x0600EE61 RID: 61025
		// (set) Token: 0x0600EE60 RID: 61024
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

		// Token: 0x17004E22 RID: 20002
		// (get) Token: 0x0600EE63 RID: 61027
		// (set) Token: 0x0600EE62 RID: 61026
		[DispId(-2147412060)]
		public virtual extern object ondragover
		{
			[DispId(-2147412060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004E23 RID: 20003
		// (get) Token: 0x0600EE65 RID: 61029
		// (set) Token: 0x0600EE64 RID: 61028
		[DispId(-2147412059)]
		public virtual extern object ondragleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004E24 RID: 20004
		// (get) Token: 0x0600EE67 RID: 61031
		// (set) Token: 0x0600EE66 RID: 61030
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

		// Token: 0x17004E25 RID: 20005
		// (get) Token: 0x0600EE69 RID: 61033
		// (set) Token: 0x0600EE68 RID: 61032
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

		// Token: 0x17004E26 RID: 20006
		// (get) Token: 0x0600EE6B RID: 61035
		// (set) Token: 0x0600EE6A RID: 61034
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

		// Token: 0x17004E27 RID: 20007
		// (get) Token: 0x0600EE6D RID: 61037
		// (set) Token: 0x0600EE6C RID: 61036
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

		// Token: 0x17004E28 RID: 20008
		// (get) Token: 0x0600EE6F RID: 61039
		// (set) Token: 0x0600EE6E RID: 61038
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

		// Token: 0x17004E29 RID: 20009
		// (get) Token: 0x0600EE71 RID: 61041
		// (set) Token: 0x0600EE70 RID: 61040
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

		// Token: 0x17004E2A RID: 20010
		// (get) Token: 0x0600EE73 RID: 61043
		// (set) Token: 0x0600EE72 RID: 61042
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

		// Token: 0x17004E2B RID: 20011
		// (get) Token: 0x0600EE74 RID: 61044
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E2C RID: 20012
		// (get) Token: 0x0600EE76 RID: 61046
		// (set) Token: 0x0600EE75 RID: 61045
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

		// Token: 0x0600EE77 RID: 61047
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x0600EE78 RID: 61048
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x0600EE79 RID: 61049
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600EE7A RID: 61050
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600EE7B RID: 61051
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17004E2D RID: 20013
		// (get) Token: 0x0600EE7D RID: 61053
		// (set) Token: 0x0600EE7C RID: 61052
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

		// Token: 0x0600EE7E RID: 61054
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17004E2E RID: 20014
		// (get) Token: 0x0600EE80 RID: 61056
		// (set) Token: 0x0600EE7F RID: 61055
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

		// Token: 0x17004E2F RID: 20015
		// (get) Token: 0x0600EE82 RID: 61058
		// (set) Token: 0x0600EE81 RID: 61057
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

		// Token: 0x17004E30 RID: 20016
		// (get) Token: 0x0600EE84 RID: 61060
		// (set) Token: 0x0600EE83 RID: 61059
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

		// Token: 0x17004E31 RID: 20017
		// (get) Token: 0x0600EE86 RID: 61062
		// (set) Token: 0x0600EE85 RID: 61061
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

		// Token: 0x0600EE87 RID: 61063
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600EE88 RID: 61064
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600EE89 RID: 61065
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17004E32 RID: 20018
		// (get) Token: 0x0600EE8A RID: 61066
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E33 RID: 20019
		// (get) Token: 0x0600EE8B RID: 61067
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E34 RID: 20020
		// (get) Token: 0x0600EE8C RID: 61068
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E35 RID: 20021
		// (get) Token: 0x0600EE8D RID: 61069
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600EE8E RID: 61070
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600EE8F RID: 61071
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17004E36 RID: 20022
		// (get) Token: 0x0600EE90 RID: 61072
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004E37 RID: 20023
		// (get) Token: 0x0600EE92 RID: 61074
		// (set) Token: 0x0600EE91 RID: 61073
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

		// Token: 0x17004E38 RID: 20024
		// (get) Token: 0x0600EE94 RID: 61076
		// (set) Token: 0x0600EE93 RID: 61075
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

		// Token: 0x17004E39 RID: 20025
		// (get) Token: 0x0600EE96 RID: 61078
		// (set) Token: 0x0600EE95 RID: 61077
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

		// Token: 0x17004E3A RID: 20026
		// (get) Token: 0x0600EE98 RID: 61080
		// (set) Token: 0x0600EE97 RID: 61079
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

		// Token: 0x17004E3B RID: 20027
		// (get) Token: 0x0600EE9A RID: 61082
		// (set) Token: 0x0600EE99 RID: 61081
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

		// Token: 0x0600EE9B RID: 61083
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17004E3C RID: 20028
		// (get) Token: 0x0600EE9C RID: 61084
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E3D RID: 20029
		// (get) Token: 0x0600EE9D RID: 61085
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E3E RID: 20030
		// (get) Token: 0x0600EE9F RID: 61087
		// (set) Token: 0x0600EE9E RID: 61086
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

		// Token: 0x17004E3F RID: 20031
		// (get) Token: 0x0600EEA1 RID: 61089
		// (set) Token: 0x0600EEA0 RID: 61088
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

		// Token: 0x0600EEA2 RID: 61090
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17004E40 RID: 20032
		// (get) Token: 0x0600EEA4 RID: 61092
		// (set) Token: 0x0600EEA3 RID: 61091
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

		// Token: 0x0600EEA5 RID: 61093
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600EEA6 RID: 61094
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600EEA7 RID: 61095
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600EEA8 RID: 61096
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17004E41 RID: 20033
		// (get) Token: 0x0600EEA9 RID: 61097
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600EEAA RID: 61098
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600EEAB RID: 61099
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17004E42 RID: 20034
		// (get) Token: 0x0600EEAC RID: 61100
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E43 RID: 20035
		// (get) Token: 0x0600EEAD RID: 61101
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004E44 RID: 20036
		// (get) Token: 0x0600EEAF RID: 61103
		// (set) Token: 0x0600EEAE RID: 61102
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

		// Token: 0x17004E45 RID: 20037
		// (get) Token: 0x0600EEB1 RID: 61105
		// (set) Token: 0x0600EEB0 RID: 61104
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

		// Token: 0x17004E46 RID: 20038
		// (get) Token: 0x0600EEB2 RID: 61106
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600EEB3 RID: 61107
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600EEB4 RID: 61108
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17004E47 RID: 20039
		// (get) Token: 0x0600EEB5 RID: 61109
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E48 RID: 20040
		// (get) Token: 0x0600EEB6 RID: 61110
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E49 RID: 20041
		// (get) Token: 0x0600EEB8 RID: 61112
		// (set) Token: 0x0600EEB7 RID: 61111
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

		// Token: 0x17004E4A RID: 20042
		// (get) Token: 0x0600EEBA RID: 61114
		// (set) Token: 0x0600EEB9 RID: 61113
		[DispId(-2147412038)]
		public virtual extern object onpage
		{
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004E4B RID: 20043
		// (get) Token: 0x0600EEBC RID: 61116
		// (set) Token: 0x0600EEBB RID: 61115
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

		// Token: 0x17004E4C RID: 20044
		// (get) Token: 0x0600EEBE RID: 61118
		// (set) Token: 0x0600EEBD RID: 61117
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

		// Token: 0x0600EEBF RID: 61119
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17004E4D RID: 20045
		// (get) Token: 0x0600EEC1 RID: 61121
		// (set) Token: 0x0600EEC0 RID: 61120
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

		// Token: 0x17004E4E RID: 20046
		// (get) Token: 0x0600EEC2 RID: 61122
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E4F RID: 20047
		// (get) Token: 0x0600EEC4 RID: 61124
		// (set) Token: 0x0600EEC3 RID: 61123
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

		// Token: 0x17004E50 RID: 20048
		// (get) Token: 0x0600EEC6 RID: 61126
		// (set) Token: 0x0600EEC5 RID: 61125
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

		// Token: 0x17004E51 RID: 20049
		// (get) Token: 0x0600EEC7 RID: 61127
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E52 RID: 20050
		// (get) Token: 0x0600EEC9 RID: 61129
		// (set) Token: 0x0600EEC8 RID: 61128
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

		// Token: 0x17004E53 RID: 20051
		// (get) Token: 0x0600EECB RID: 61131
		// (set) Token: 0x0600EECA RID: 61130
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

		// Token: 0x0600EECC RID: 61132
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17004E54 RID: 20052
		// (get) Token: 0x0600EECE RID: 61134
		// (set) Token: 0x0600EECD RID: 61133
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

		// Token: 0x17004E55 RID: 20053
		// (get) Token: 0x0600EED0 RID: 61136
		// (set) Token: 0x0600EECF RID: 61135
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

		// Token: 0x17004E56 RID: 20054
		// (get) Token: 0x0600EED2 RID: 61138
		// (set) Token: 0x0600EED1 RID: 61137
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

		// Token: 0x17004E57 RID: 20055
		// (get) Token: 0x0600EED4 RID: 61140
		// (set) Token: 0x0600EED3 RID: 61139
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

		// Token: 0x17004E58 RID: 20056
		// (get) Token: 0x0600EED6 RID: 61142
		// (set) Token: 0x0600EED5 RID: 61141
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

		// Token: 0x17004E59 RID: 20057
		// (get) Token: 0x0600EED8 RID: 61144
		// (set) Token: 0x0600EED7 RID: 61143
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

		// Token: 0x17004E5A RID: 20058
		// (get) Token: 0x0600EEDA RID: 61146
		// (set) Token: 0x0600EED9 RID: 61145
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

		// Token: 0x17004E5B RID: 20059
		// (get) Token: 0x0600EEDC RID: 61148
		// (set) Token: 0x0600EEDB RID: 61147
		[DispId(-2147412024)]
		public virtual extern object ondeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600EEDD RID: 61149
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17004E5C RID: 20060
		// (get) Token: 0x0600EEDE RID: 61150
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E5D RID: 20061
		// (get) Token: 0x0600EEE0 RID: 61152
		// (set) Token: 0x0600EEDF RID: 61151
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

		// Token: 0x0600EEE1 RID: 61153
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600EEE2 RID: 61154
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600EEE3 RID: 61155
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600EEE4 RID: 61156
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17004E5E RID: 20062
		// (get) Token: 0x0600EEE6 RID: 61158
		// (set) Token: 0x0600EEE5 RID: 61157
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

		// Token: 0x17004E5F RID: 20063
		// (get) Token: 0x0600EEE8 RID: 61160
		// (set) Token: 0x0600EEE7 RID: 61159
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

		// Token: 0x17004E60 RID: 20064
		// (get) Token: 0x0600EEEA RID: 61162
		// (set) Token: 0x0600EEE9 RID: 61161
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

		// Token: 0x17004E61 RID: 20065
		// (get) Token: 0x0600EEEB RID: 61163
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E62 RID: 20066
		// (get) Token: 0x0600EEEC RID: 61164
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004E63 RID: 20067
		// (get) Token: 0x0600EEED RID: 61165
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E64 RID: 20068
		// (get) Token: 0x0600EEEE RID: 61166
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600EEEF RID: 61167
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17004E65 RID: 20069
		// (get) Token: 0x0600EEF0 RID: 61168
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004E66 RID: 20070
		// (get) Token: 0x0600EEF1 RID: 61169
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600EEF2 RID: 61170
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600EEF3 RID: 61171
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600EEF4 RID: 61172
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600EEF5 RID: 61173
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600EEF6 RID: 61174
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600EEF7 RID: 61175
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600EEF8 RID: 61176
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600EEF9 RID: 61177
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004E67 RID: 20071
		// (get) Token: 0x0600EEFA RID: 61178
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004E68 RID: 20072
		// (get) Token: 0x0600EEFC RID: 61180
		// (set) Token: 0x0600EEFB RID: 61179
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

		// Token: 0x17004E69 RID: 20073
		// (get) Token: 0x0600EEFD RID: 61181
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E6A RID: 20074
		// (get) Token: 0x0600EEFE RID: 61182
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E6B RID: 20075
		// (get) Token: 0x0600EEFF RID: 61183
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E6C RID: 20076
		// (get) Token: 0x0600EF00 RID: 61184
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E6D RID: 20077
		// (get) Token: 0x0600EF01 RID: 61185
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004E6E RID: 20078
		// (get) Token: 0x0600EF03 RID: 61187
		// (set) Token: 0x0600EF02 RID: 61186
		[DispId(1001)]
		public virtual extern string shape
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E6F RID: 20079
		// (get) Token: 0x0600EF05 RID: 61189
		// (set) Token: 0x0600EF04 RID: 61188
		[DispId(1002)]
		public virtual extern string coords
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E70 RID: 20080
		// (get) Token: 0x0600EF07 RID: 61191
		// (set) Token: 0x0600EF06 RID: 61190
		[DispId(0)]
		[IndexerName("href")]
		public virtual extern string href
		{
			[DispId(0)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(0)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E71 RID: 20081
		// (get) Token: 0x0600EF09 RID: 61193
		// (set) Token: 0x0600EF08 RID: 61192
		[DispId(1004)]
		public virtual extern string target
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E72 RID: 20082
		// (get) Token: 0x0600EF0B RID: 61195
		// (set) Token: 0x0600EF0A RID: 61194
		[DispId(1005)]
		public virtual extern string alt
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E73 RID: 20083
		// (get) Token: 0x0600EF0D RID: 61197
		// (set) Token: 0x0600EF0C RID: 61196
		[DispId(1006)]
		public virtual extern bool noHref
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004E74 RID: 20084
		// (get) Token: 0x0600EF0F RID: 61199
		// (set) Token: 0x0600EF0E RID: 61198
		[DispId(1007)]
		public virtual extern string host
		{
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E75 RID: 20085
		// (get) Token: 0x0600EF11 RID: 61201
		// (set) Token: 0x0600EF10 RID: 61200
		[DispId(1008)]
		public virtual extern string hostname
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E76 RID: 20086
		// (get) Token: 0x0600EF13 RID: 61203
		// (set) Token: 0x0600EF12 RID: 61202
		[DispId(1009)]
		public virtual extern string pathname
		{
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E77 RID: 20087
		// (get) Token: 0x0600EF15 RID: 61205
		// (set) Token: 0x0600EF14 RID: 61204
		[DispId(1010)]
		public virtual extern string port
		{
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E78 RID: 20088
		// (get) Token: 0x0600EF17 RID: 61207
		// (set) Token: 0x0600EF16 RID: 61206
		[DispId(1011)]
		public virtual extern string protocol
		{
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004E79 RID: 20089
		// (get) Token: 0x0600EF19 RID: 61209
		// (set) Token: 0x0600EF18 RID: 61208
		[DispId(1012)]
		public virtual extern string search
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

		// Token: 0x17004E7A RID: 20090
		// (get) Token: 0x0600EF1B RID: 61211
		// (set) Token: 0x0600EF1A RID: 61210
		[DispId(1013)]
		public virtual extern string hash
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

		// Token: 0x0600EF1C RID: 61212
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600EF1D RID: 61213
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600EF1E RID: 61214
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17004E7B RID: 20091
		// (get) Token: 0x0600EF20 RID: 61216
		// (set) Token: 0x0600EF1F RID: 61215
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

		// Token: 0x17004E7C RID: 20092
		// (get) Token: 0x0600EF22 RID: 61218
		// (set) Token: 0x0600EF21 RID: 61217
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

		// Token: 0x17004E7D RID: 20093
		// (get) Token: 0x0600EF23 RID: 61219
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004E7E RID: 20094
		// (get) Token: 0x0600EF24 RID: 61220
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E7F RID: 20095
		// (get) Token: 0x0600EF25 RID: 61221
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E80 RID: 20096
		// (get) Token: 0x0600EF27 RID: 61223
		// (set) Token: 0x0600EF26 RID: 61222
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

		// Token: 0x17004E81 RID: 20097
		// (get) Token: 0x0600EF29 RID: 61225
		// (set) Token: 0x0600EF28 RID: 61224
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

		// Token: 0x17004E82 RID: 20098
		// (get) Token: 0x0600EF2B RID: 61227
		// (set) Token: 0x0600EF2A RID: 61226
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

		// Token: 0x17004E83 RID: 20099
		// (get) Token: 0x0600EF2D RID: 61229
		// (set) Token: 0x0600EF2C RID: 61228
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

		// Token: 0x17004E84 RID: 20100
		// (get) Token: 0x0600EF2F RID: 61231
		// (set) Token: 0x0600EF2E RID: 61230
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

		// Token: 0x17004E85 RID: 20101
		// (get) Token: 0x0600EF31 RID: 61233
		// (set) Token: 0x0600EF30 RID: 61232
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

		// Token: 0x17004E86 RID: 20102
		// (get) Token: 0x0600EF33 RID: 61235
		// (set) Token: 0x0600EF32 RID: 61234
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

		// Token: 0x17004E87 RID: 20103
		// (get) Token: 0x0600EF35 RID: 61237
		// (set) Token: 0x0600EF34 RID: 61236
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

		// Token: 0x17004E88 RID: 20104
		// (get) Token: 0x0600EF37 RID: 61239
		// (set) Token: 0x0600EF36 RID: 61238
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

		// Token: 0x17004E89 RID: 20105
		// (get) Token: 0x0600EF39 RID: 61241
		// (set) Token: 0x0600EF38 RID: 61240
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

		// Token: 0x17004E8A RID: 20106
		// (get) Token: 0x0600EF3B RID: 61243
		// (set) Token: 0x0600EF3A RID: 61242
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

		// Token: 0x17004E8B RID: 20107
		// (get) Token: 0x0600EF3C RID: 61244
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004E8C RID: 20108
		// (get) Token: 0x0600EF3E RID: 61246
		// (set) Token: 0x0600EF3D RID: 61245
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

		// Token: 0x17004E8D RID: 20109
		// (get) Token: 0x0600EF40 RID: 61248
		// (set) Token: 0x0600EF3F RID: 61247
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

		// Token: 0x17004E8E RID: 20110
		// (get) Token: 0x0600EF42 RID: 61250
		// (set) Token: 0x0600EF41 RID: 61249
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

		// Token: 0x0600EF43 RID: 61251
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600EF44 RID: 61252
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17004E8F RID: 20111
		// (get) Token: 0x0600EF45 RID: 61253
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E90 RID: 20112
		// (get) Token: 0x0600EF46 RID: 61254
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004E91 RID: 20113
		// (get) Token: 0x0600EF48 RID: 61256
		// (set) Token: 0x0600EF47 RID: 61255
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

		// Token: 0x17004E92 RID: 20114
		// (get) Token: 0x0600EF49 RID: 61257
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E93 RID: 20115
		// (get) Token: 0x0600EF4A RID: 61258
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E94 RID: 20116
		// (get) Token: 0x0600EF4B RID: 61259
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E95 RID: 20117
		// (get) Token: 0x0600EF4C RID: 61260
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004E96 RID: 20118
		// (get) Token: 0x0600EF4D RID: 61261
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E97 RID: 20119
		// (get) Token: 0x0600EF4F RID: 61263
		// (set) Token: 0x0600EF4E RID: 61262
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

		// Token: 0x17004E98 RID: 20120
		// (get) Token: 0x0600EF51 RID: 61265
		// (set) Token: 0x0600EF50 RID: 61264
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

		// Token: 0x17004E99 RID: 20121
		// (get) Token: 0x0600EF53 RID: 61267
		// (set) Token: 0x0600EF52 RID: 61266
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

		// Token: 0x17004E9A RID: 20122
		// (get) Token: 0x0600EF55 RID: 61269
		// (set) Token: 0x0600EF54 RID: 61268
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

		// Token: 0x0600EF56 RID: 61270
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600EF57 RID: 61271
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17004E9B RID: 20123
		// (get) Token: 0x0600EF58 RID: 61272
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E9C RID: 20124
		// (get) Token: 0x0600EF59 RID: 61273
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600EF5A RID: 61274
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17004E9D RID: 20125
		// (get) Token: 0x0600EF5B RID: 61275
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004E9E RID: 20126
		// (get) Token: 0x0600EF5D RID: 61277
		// (set) Token: 0x0600EF5C RID: 61276
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

		// Token: 0x0600EF5E RID: 61278
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17004E9F RID: 20127
		// (get) Token: 0x0600EF60 RID: 61280
		// (set) Token: 0x0600EF5F RID: 61279
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

		// Token: 0x17004EA0 RID: 20128
		// (get) Token: 0x0600EF62 RID: 61282
		// (set) Token: 0x0600EF61 RID: 61281
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

		// Token: 0x17004EA1 RID: 20129
		// (get) Token: 0x0600EF64 RID: 61284
		// (set) Token: 0x0600EF63 RID: 61283
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

		// Token: 0x17004EA2 RID: 20130
		// (get) Token: 0x0600EF66 RID: 61286
		// (set) Token: 0x0600EF65 RID: 61285
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

		// Token: 0x17004EA3 RID: 20131
		// (get) Token: 0x0600EF68 RID: 61288
		// (set) Token: 0x0600EF67 RID: 61287
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

		// Token: 0x17004EA4 RID: 20132
		// (get) Token: 0x0600EF6A RID: 61290
		// (set) Token: 0x0600EF69 RID: 61289
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

		// Token: 0x17004EA5 RID: 20133
		// (get) Token: 0x0600EF6C RID: 61292
		// (set) Token: 0x0600EF6B RID: 61291
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

		// Token: 0x17004EA6 RID: 20134
		// (get) Token: 0x0600EF6E RID: 61294
		// (set) Token: 0x0600EF6D RID: 61293
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

		// Token: 0x17004EA7 RID: 20135
		// (get) Token: 0x0600EF70 RID: 61296
		// (set) Token: 0x0600EF6F RID: 61295
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

		// Token: 0x17004EA8 RID: 20136
		// (get) Token: 0x0600EF71 RID: 61297
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004EA9 RID: 20137
		// (get) Token: 0x0600EF72 RID: 61298
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004EAA RID: 20138
		// (get) Token: 0x0600EF73 RID: 61299
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600EF74 RID: 61300
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600EF75 RID: 61301
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17004EAB RID: 20139
		// (get) Token: 0x0600EF77 RID: 61303
		// (set) Token: 0x0600EF76 RID: 61302
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

		// Token: 0x0600EF78 RID: 61304
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600EF79 RID: 61305
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17004EAC RID: 20140
		// (get) Token: 0x0600EF7B RID: 61307
		// (set) Token: 0x0600EF7A RID: 61306
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

		// Token: 0x17004EAD RID: 20141
		// (get) Token: 0x0600EF7D RID: 61309
		// (set) Token: 0x0600EF7C RID: 61308
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

		// Token: 0x17004EAE RID: 20142
		// (get) Token: 0x0600EF7F RID: 61311
		// (set) Token: 0x0600EF7E RID: 61310
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

		// Token: 0x17004EAF RID: 20143
		// (get) Token: 0x0600EF81 RID: 61313
		// (set) Token: 0x0600EF80 RID: 61312
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

		// Token: 0x17004EB0 RID: 20144
		// (get) Token: 0x0600EF83 RID: 61315
		// (set) Token: 0x0600EF82 RID: 61314
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

		// Token: 0x17004EB1 RID: 20145
		// (get) Token: 0x0600EF85 RID: 61317
		// (set) Token: 0x0600EF84 RID: 61316
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

		// Token: 0x17004EB2 RID: 20146
		// (get) Token: 0x0600EF87 RID: 61319
		// (set) Token: 0x0600EF86 RID: 61318
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

		// Token: 0x17004EB3 RID: 20147
		// (get) Token: 0x0600EF89 RID: 61321
		// (set) Token: 0x0600EF88 RID: 61320
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

		// Token: 0x17004EB4 RID: 20148
		// (get) Token: 0x0600EF8B RID: 61323
		// (set) Token: 0x0600EF8A RID: 61322
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

		// Token: 0x17004EB5 RID: 20149
		// (get) Token: 0x0600EF8D RID: 61325
		// (set) Token: 0x0600EF8C RID: 61324
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

		// Token: 0x17004EB6 RID: 20150
		// (get) Token: 0x0600EF8F RID: 61327
		// (set) Token: 0x0600EF8E RID: 61326
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

		// Token: 0x17004EB7 RID: 20151
		// (get) Token: 0x0600EF91 RID: 61329
		// (set) Token: 0x0600EF90 RID: 61328
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

		// Token: 0x17004EB8 RID: 20152
		// (get) Token: 0x0600EF93 RID: 61331
		// (set) Token: 0x0600EF92 RID: 61330
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

		// Token: 0x17004EB9 RID: 20153
		// (get) Token: 0x0600EF94 RID: 61332
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004EBA RID: 20154
		// (get) Token: 0x0600EF96 RID: 61334
		// (set) Token: 0x0600EF95 RID: 61333
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

		// Token: 0x0600EF97 RID: 61335
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600EF98 RID: 61336
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0600EF99 RID: 61337
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600EF9A RID: 61338
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600EF9B RID: 61339
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17004EBB RID: 20155
		// (get) Token: 0x0600EF9D RID: 61341
		// (set) Token: 0x0600EF9C RID: 61340
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

		// Token: 0x0600EF9E RID: 61342
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17004EBC RID: 20156
		// (get) Token: 0x0600EFA0 RID: 61344
		// (set) Token: 0x0600EF9F RID: 61343
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

		// Token: 0x17004EBD RID: 20157
		// (get) Token: 0x0600EFA2 RID: 61346
		// (set) Token: 0x0600EFA1 RID: 61345
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

		// Token: 0x17004EBE RID: 20158
		// (get) Token: 0x0600EFA4 RID: 61348
		// (set) Token: 0x0600EFA3 RID: 61347
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

		// Token: 0x17004EBF RID: 20159
		// (get) Token: 0x0600EFA6 RID: 61350
		// (set) Token: 0x0600EFA5 RID: 61349
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

		// Token: 0x0600EFA7 RID: 61351
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600EFA8 RID: 61352
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600EFA9 RID: 61353
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17004EC0 RID: 20160
		// (get) Token: 0x0600EFAA RID: 61354
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004EC1 RID: 20161
		// (get) Token: 0x0600EFAB RID: 61355
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004EC2 RID: 20162
		// (get) Token: 0x0600EFAC RID: 61356
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004EC3 RID: 20163
		// (get) Token: 0x0600EFAD RID: 61357
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600EFAE RID: 61358
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600EFAF RID: 61359
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17004EC4 RID: 20164
		// (get) Token: 0x0600EFB0 RID: 61360
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004EC5 RID: 20165
		// (get) Token: 0x0600EFB2 RID: 61362
		// (set) Token: 0x0600EFB1 RID: 61361
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

		// Token: 0x17004EC6 RID: 20166
		// (get) Token: 0x0600EFB4 RID: 61364
		// (set) Token: 0x0600EFB3 RID: 61363
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

		// Token: 0x17004EC7 RID: 20167
		// (get) Token: 0x0600EFB6 RID: 61366
		// (set) Token: 0x0600EFB5 RID: 61365
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

		// Token: 0x17004EC8 RID: 20168
		// (get) Token: 0x0600EFB8 RID: 61368
		// (set) Token: 0x0600EFB7 RID: 61367
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

		// Token: 0x17004EC9 RID: 20169
		// (get) Token: 0x0600EFBA RID: 61370
		// (set) Token: 0x0600EFB9 RID: 61369
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

		// Token: 0x0600EFBB RID: 61371
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17004ECA RID: 20170
		// (get) Token: 0x0600EFBC RID: 61372
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004ECB RID: 20171
		// (get) Token: 0x0600EFBD RID: 61373
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004ECC RID: 20172
		// (get) Token: 0x0600EFBF RID: 61375
		// (set) Token: 0x0600EFBE RID: 61374
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

		// Token: 0x17004ECD RID: 20173
		// (get) Token: 0x0600EFC1 RID: 61377
		// (set) Token: 0x0600EFC0 RID: 61376
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

		// Token: 0x0600EFC2 RID: 61378
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600EFC3 RID: 61379
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17004ECE RID: 20174
		// (get) Token: 0x0600EFC5 RID: 61381
		// (set) Token: 0x0600EFC4 RID: 61380
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

		// Token: 0x0600EFC6 RID: 61382
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600EFC7 RID: 61383
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600EFC8 RID: 61384
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600EFC9 RID: 61385
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17004ECF RID: 20175
		// (get) Token: 0x0600EFCA RID: 61386
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600EFCB RID: 61387
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600EFCC RID: 61388
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17004ED0 RID: 20176
		// (get) Token: 0x0600EFCD RID: 61389
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004ED1 RID: 20177
		// (get) Token: 0x0600EFCE RID: 61390
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004ED2 RID: 20178
		// (get) Token: 0x0600EFD0 RID: 61392
		// (set) Token: 0x0600EFCF RID: 61391
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

		// Token: 0x17004ED3 RID: 20179
		// (get) Token: 0x0600EFD2 RID: 61394
		// (set) Token: 0x0600EFD1 RID: 61393
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

		// Token: 0x17004ED4 RID: 20180
		// (get) Token: 0x0600EFD3 RID: 61395
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600EFD4 RID: 61396
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600EFD5 RID: 61397
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17004ED5 RID: 20181
		// (get) Token: 0x0600EFD6 RID: 61398
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004ED6 RID: 20182
		// (get) Token: 0x0600EFD7 RID: 61399
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004ED7 RID: 20183
		// (get) Token: 0x0600EFD9 RID: 61401
		// (set) Token: 0x0600EFD8 RID: 61400
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

		// Token: 0x17004ED8 RID: 20184
		// (get) Token: 0x0600EFDB RID: 61403
		// (set) Token: 0x0600EFDA RID: 61402
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

		// Token: 0x17004ED9 RID: 20185
		// (get) Token: 0x0600EFDD RID: 61405
		// (set) Token: 0x0600EFDC RID: 61404
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

		// Token: 0x17004EDA RID: 20186
		// (get) Token: 0x0600EFDF RID: 61407
		// (set) Token: 0x0600EFDE RID: 61406
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

		// Token: 0x0600EFE0 RID: 61408
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17004EDB RID: 20187
		// (get) Token: 0x0600EFE2 RID: 61410
		// (set) Token: 0x0600EFE1 RID: 61409
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

		// Token: 0x17004EDC RID: 20188
		// (get) Token: 0x0600EFE3 RID: 61411
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004EDD RID: 20189
		// (get) Token: 0x0600EFE5 RID: 61413
		// (set) Token: 0x0600EFE4 RID: 61412
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

		// Token: 0x17004EDE RID: 20190
		// (get) Token: 0x0600EFE7 RID: 61415
		// (set) Token: 0x0600EFE6 RID: 61414
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

		// Token: 0x17004EDF RID: 20191
		// (get) Token: 0x0600EFE8 RID: 61416
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004EE0 RID: 20192
		// (get) Token: 0x0600EFEA RID: 61418
		// (set) Token: 0x0600EFE9 RID: 61417
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

		// Token: 0x17004EE1 RID: 20193
		// (get) Token: 0x0600EFEC RID: 61420
		// (set) Token: 0x0600EFEB RID: 61419
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

		// Token: 0x0600EFED RID: 61421
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17004EE2 RID: 20194
		// (get) Token: 0x0600EFEF RID: 61423
		// (set) Token: 0x0600EFEE RID: 61422
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

		// Token: 0x17004EE3 RID: 20195
		// (get) Token: 0x0600EFF1 RID: 61425
		// (set) Token: 0x0600EFF0 RID: 61424
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

		// Token: 0x17004EE4 RID: 20196
		// (get) Token: 0x0600EFF3 RID: 61427
		// (set) Token: 0x0600EFF2 RID: 61426
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

		// Token: 0x17004EE5 RID: 20197
		// (get) Token: 0x0600EFF5 RID: 61429
		// (set) Token: 0x0600EFF4 RID: 61428
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

		// Token: 0x17004EE6 RID: 20198
		// (get) Token: 0x0600EFF7 RID: 61431
		// (set) Token: 0x0600EFF6 RID: 61430
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

		// Token: 0x17004EE7 RID: 20199
		// (get) Token: 0x0600EFF9 RID: 61433
		// (set) Token: 0x0600EFF8 RID: 61432
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

		// Token: 0x17004EE8 RID: 20200
		// (get) Token: 0x0600EFFB RID: 61435
		// (set) Token: 0x0600EFFA RID: 61434
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

		// Token: 0x17004EE9 RID: 20201
		// (get) Token: 0x0600EFFD RID: 61437
		// (set) Token: 0x0600EFFC RID: 61436
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

		// Token: 0x0600EFFE RID: 61438
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17004EEA RID: 20202
		// (get) Token: 0x0600EFFF RID: 61439
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004EEB RID: 20203
		// (get) Token: 0x0600F001 RID: 61441
		// (set) Token: 0x0600F000 RID: 61440
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

		// Token: 0x0600F002 RID: 61442
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x0600F003 RID: 61443
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600F004 RID: 61444
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600F005 RID: 61445
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17004EEC RID: 20204
		// (get) Token: 0x0600F007 RID: 61447
		// (set) Token: 0x0600F006 RID: 61446
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

		// Token: 0x17004EED RID: 20205
		// (get) Token: 0x0600F009 RID: 61449
		// (set) Token: 0x0600F008 RID: 61448
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

		// Token: 0x17004EEE RID: 20206
		// (get) Token: 0x0600F00B RID: 61451
		// (set) Token: 0x0600F00A RID: 61450
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

		// Token: 0x17004EEF RID: 20207
		// (get) Token: 0x0600F00C RID: 61452
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004EF0 RID: 20208
		// (get) Token: 0x0600F00D RID: 61453
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004EF1 RID: 20209
		// (get) Token: 0x0600F00E RID: 61454
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004EF2 RID: 20210
		// (get) Token: 0x0600F00F RID: 61455
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600F010 RID: 61456
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17004EF3 RID: 20211
		// (get) Token: 0x0600F011 RID: 61457
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004EF4 RID: 20212
		// (get) Token: 0x0600F012 RID: 61458
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600F013 RID: 61459
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600F014 RID: 61460
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600F015 RID: 61461
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600F016 RID: 61462
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600F017 RID: 61463
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600F018 RID: 61464
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600F019 RID: 61465
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600F01A RID: 61466
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004EF5 RID: 20213
		// (get) Token: 0x0600F01B RID: 61467
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004EF6 RID: 20214
		// (get) Token: 0x0600F01D RID: 61469
		// (set) Token: 0x0600F01C RID: 61468
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

		// Token: 0x17004EF7 RID: 20215
		// (get) Token: 0x0600F01E RID: 61470
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004EF8 RID: 20216
		// (get) Token: 0x0600F01F RID: 61471
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004EF9 RID: 20217
		// (get) Token: 0x0600F020 RID: 61472
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004EFA RID: 20218
		// (get) Token: 0x0600F021 RID: 61473
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004EFB RID: 20219
		// (get) Token: 0x0600F022 RID: 61474
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004EFC RID: 20220
		// (get) Token: 0x0600F024 RID: 61476
		// (set) Token: 0x0600F023 RID: 61475
		public virtual extern string IHTMLAreaElement_shape
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004EFD RID: 20221
		// (get) Token: 0x0600F026 RID: 61478
		// (set) Token: 0x0600F025 RID: 61477
		public virtual extern string IHTMLAreaElement_coords
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004EFE RID: 20222
		// (get) Token: 0x0600F028 RID: 61480
		// (set) Token: 0x0600F027 RID: 61479
		public virtual extern string IHTMLAreaElement_href
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004EFF RID: 20223
		// (get) Token: 0x0600F02A RID: 61482
		// (set) Token: 0x0600F029 RID: 61481
		public virtual extern string IHTMLAreaElement_target
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004F00 RID: 20224
		// (get) Token: 0x0600F02C RID: 61484
		// (set) Token: 0x0600F02B RID: 61483
		public virtual extern string IHTMLAreaElement_alt
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004F01 RID: 20225
		// (get) Token: 0x0600F02E RID: 61486
		// (set) Token: 0x0600F02D RID: 61485
		public virtual extern bool IHTMLAreaElement_noHref
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004F02 RID: 20226
		// (get) Token: 0x0600F030 RID: 61488
		// (set) Token: 0x0600F02F RID: 61487
		public virtual extern string IHTMLAreaElement_host
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004F03 RID: 20227
		// (get) Token: 0x0600F032 RID: 61490
		// (set) Token: 0x0600F031 RID: 61489
		public virtual extern string IHTMLAreaElement_hostname
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004F04 RID: 20228
		// (get) Token: 0x0600F034 RID: 61492
		// (set) Token: 0x0600F033 RID: 61491
		public virtual extern string IHTMLAreaElement_pathname
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004F05 RID: 20229
		// (get) Token: 0x0600F036 RID: 61494
		// (set) Token: 0x0600F035 RID: 61493
		public virtual extern string IHTMLAreaElement_port
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004F06 RID: 20230
		// (get) Token: 0x0600F038 RID: 61496
		// (set) Token: 0x0600F037 RID: 61495
		public virtual extern string IHTMLAreaElement_protocol
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004F07 RID: 20231
		// (get) Token: 0x0600F03A RID: 61498
		// (set) Token: 0x0600F039 RID: 61497
		public virtual extern string IHTMLAreaElement_search
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004F08 RID: 20232
		// (get) Token: 0x0600F03C RID: 61500
		// (set) Token: 0x0600F03B RID: 61499
		public virtual extern string IHTMLAreaElement_hash
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004F09 RID: 20233
		// (get) Token: 0x0600F03E RID: 61502
		// (set) Token: 0x0600F03D RID: 61501
		public virtual extern object IHTMLAreaElement_onblur
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004F0A RID: 20234
		// (get) Token: 0x0600F040 RID: 61504
		// (set) Token: 0x0600F03F RID: 61503
		public virtual extern object IHTMLAreaElement_onfocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004F0B RID: 20235
		// (get) Token: 0x0600F042 RID: 61506
		// (set) Token: 0x0600F041 RID: 61505
		public virtual extern short IHTMLAreaElement_tabIndex
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x0600F043 RID: 61507
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLAreaElement_focus();

		// Token: 0x0600F044 RID: 61508
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLAreaElement_blur();

		// Token: 0x14001CFD RID: 7421
		// (add) Token: 0x0600F045 RID: 61509
		// (remove) Token: 0x0600F046 RID: 61510
		public virtual extern event HTMLAreaEvents_onhelpEventHandler HTMLAreaEvents_Event_onhelp;

		// Token: 0x14001CFE RID: 7422
		// (add) Token: 0x0600F047 RID: 61511
		// (remove) Token: 0x0600F048 RID: 61512
		public virtual extern event HTMLAreaEvents_onclickEventHandler HTMLAreaEvents_Event_onclick;

		// Token: 0x14001CFF RID: 7423
		// (add) Token: 0x0600F049 RID: 61513
		// (remove) Token: 0x0600F04A RID: 61514
		public virtual extern event HTMLAreaEvents_ondblclickEventHandler HTMLAreaEvents_Event_ondblclick;

		// Token: 0x14001D00 RID: 7424
		// (add) Token: 0x0600F04B RID: 61515
		// (remove) Token: 0x0600F04C RID: 61516
		public virtual extern event HTMLAreaEvents_onkeypressEventHandler HTMLAreaEvents_Event_onkeypress;

		// Token: 0x14001D01 RID: 7425
		// (add) Token: 0x0600F04D RID: 61517
		// (remove) Token: 0x0600F04E RID: 61518
		public virtual extern event HTMLAreaEvents_onkeydownEventHandler HTMLAreaEvents_Event_onkeydown;

		// Token: 0x14001D02 RID: 7426
		// (add) Token: 0x0600F04F RID: 61519
		// (remove) Token: 0x0600F050 RID: 61520
		public virtual extern event HTMLAreaEvents_onkeyupEventHandler HTMLAreaEvents_Event_onkeyup;

		// Token: 0x14001D03 RID: 7427
		// (add) Token: 0x0600F051 RID: 61521
		// (remove) Token: 0x0600F052 RID: 61522
		public virtual extern event HTMLAreaEvents_onmouseoutEventHandler HTMLAreaEvents_Event_onmouseout;

		// Token: 0x14001D04 RID: 7428
		// (add) Token: 0x0600F053 RID: 61523
		// (remove) Token: 0x0600F054 RID: 61524
		public virtual extern event HTMLAreaEvents_onmouseoverEventHandler HTMLAreaEvents_Event_onmouseover;

		// Token: 0x14001D05 RID: 7429
		// (add) Token: 0x0600F055 RID: 61525
		// (remove) Token: 0x0600F056 RID: 61526
		public virtual extern event HTMLAreaEvents_onmousemoveEventHandler HTMLAreaEvents_Event_onmousemove;

		// Token: 0x14001D06 RID: 7430
		// (add) Token: 0x0600F057 RID: 61527
		// (remove) Token: 0x0600F058 RID: 61528
		public virtual extern event HTMLAreaEvents_onmousedownEventHandler HTMLAreaEvents_Event_onmousedown;

		// Token: 0x14001D07 RID: 7431
		// (add) Token: 0x0600F059 RID: 61529
		// (remove) Token: 0x0600F05A RID: 61530
		public virtual extern event HTMLAreaEvents_onmouseupEventHandler HTMLAreaEvents_Event_onmouseup;

		// Token: 0x14001D08 RID: 7432
		// (add) Token: 0x0600F05B RID: 61531
		// (remove) Token: 0x0600F05C RID: 61532
		public virtual extern event HTMLAreaEvents_onselectstartEventHandler HTMLAreaEvents_Event_onselectstart;

		// Token: 0x14001D09 RID: 7433
		// (add) Token: 0x0600F05D RID: 61533
		// (remove) Token: 0x0600F05E RID: 61534
		public virtual extern event HTMLAreaEvents_onfilterchangeEventHandler HTMLAreaEvents_Event_onfilterchange;

		// Token: 0x14001D0A RID: 7434
		// (add) Token: 0x0600F05F RID: 61535
		// (remove) Token: 0x0600F060 RID: 61536
		public virtual extern event HTMLAreaEvents_ondragstartEventHandler HTMLAreaEvents_Event_ondragstart;

		// Token: 0x14001D0B RID: 7435
		// (add) Token: 0x0600F061 RID: 61537
		// (remove) Token: 0x0600F062 RID: 61538
		public virtual extern event HTMLAreaEvents_onbeforeupdateEventHandler HTMLAreaEvents_Event_onbeforeupdate;

		// Token: 0x14001D0C RID: 7436
		// (add) Token: 0x0600F063 RID: 61539
		// (remove) Token: 0x0600F064 RID: 61540
		public virtual extern event HTMLAreaEvents_onafterupdateEventHandler HTMLAreaEvents_Event_onafterupdate;

		// Token: 0x14001D0D RID: 7437
		// (add) Token: 0x0600F065 RID: 61541
		// (remove) Token: 0x0600F066 RID: 61542
		public virtual extern event HTMLAreaEvents_onerrorupdateEventHandler HTMLAreaEvents_Event_onerrorupdate;

		// Token: 0x14001D0E RID: 7438
		// (add) Token: 0x0600F067 RID: 61543
		// (remove) Token: 0x0600F068 RID: 61544
		public virtual extern event HTMLAreaEvents_onrowexitEventHandler HTMLAreaEvents_Event_onrowexit;

		// Token: 0x14001D0F RID: 7439
		// (add) Token: 0x0600F069 RID: 61545
		// (remove) Token: 0x0600F06A RID: 61546
		public virtual extern event HTMLAreaEvents_onrowenterEventHandler HTMLAreaEvents_Event_onrowenter;

		// Token: 0x14001D10 RID: 7440
		// (add) Token: 0x0600F06B RID: 61547
		// (remove) Token: 0x0600F06C RID: 61548
		public virtual extern event HTMLAreaEvents_ondatasetchangedEventHandler HTMLAreaEvents_Event_ondatasetchanged;

		// Token: 0x14001D11 RID: 7441
		// (add) Token: 0x0600F06D RID: 61549
		// (remove) Token: 0x0600F06E RID: 61550
		public virtual extern event HTMLAreaEvents_ondataavailableEventHandler HTMLAreaEvents_Event_ondataavailable;

		// Token: 0x14001D12 RID: 7442
		// (add) Token: 0x0600F06F RID: 61551
		// (remove) Token: 0x0600F070 RID: 61552
		public virtual extern event HTMLAreaEvents_ondatasetcompleteEventHandler HTMLAreaEvents_Event_ondatasetcomplete;

		// Token: 0x14001D13 RID: 7443
		// (add) Token: 0x0600F071 RID: 61553
		// (remove) Token: 0x0600F072 RID: 61554
		public virtual extern event HTMLAreaEvents_onlosecaptureEventHandler HTMLAreaEvents_Event_onlosecapture;

		// Token: 0x14001D14 RID: 7444
		// (add) Token: 0x0600F073 RID: 61555
		// (remove) Token: 0x0600F074 RID: 61556
		public virtual extern event HTMLAreaEvents_onpropertychangeEventHandler HTMLAreaEvents_Event_onpropertychange;

		// Token: 0x14001D15 RID: 7445
		// (add) Token: 0x0600F075 RID: 61557
		// (remove) Token: 0x0600F076 RID: 61558
		public virtual extern event HTMLAreaEvents_onscrollEventHandler HTMLAreaEvents_Event_onscroll;

		// Token: 0x14001D16 RID: 7446
		// (add) Token: 0x0600F077 RID: 61559
		// (remove) Token: 0x0600F078 RID: 61560
		public virtual extern event HTMLAreaEvents_onfocusEventHandler HTMLAreaEvents_Event_onfocus;

		// Token: 0x14001D17 RID: 7447
		// (add) Token: 0x0600F079 RID: 61561
		// (remove) Token: 0x0600F07A RID: 61562
		public virtual extern event HTMLAreaEvents_onblurEventHandler HTMLAreaEvents_Event_onblur;

		// Token: 0x14001D18 RID: 7448
		// (add) Token: 0x0600F07B RID: 61563
		// (remove) Token: 0x0600F07C RID: 61564
		public virtual extern event HTMLAreaEvents_onresizeEventHandler HTMLAreaEvents_Event_onresize;

		// Token: 0x14001D19 RID: 7449
		// (add) Token: 0x0600F07D RID: 61565
		// (remove) Token: 0x0600F07E RID: 61566
		public virtual extern event HTMLAreaEvents_ondragEventHandler HTMLAreaEvents_Event_ondrag;

		// Token: 0x14001D1A RID: 7450
		// (add) Token: 0x0600F07F RID: 61567
		// (remove) Token: 0x0600F080 RID: 61568
		public virtual extern event HTMLAreaEvents_ondragendEventHandler HTMLAreaEvents_Event_ondragend;

		// Token: 0x14001D1B RID: 7451
		// (add) Token: 0x0600F081 RID: 61569
		// (remove) Token: 0x0600F082 RID: 61570
		public virtual extern event HTMLAreaEvents_ondragenterEventHandler HTMLAreaEvents_Event_ondragenter;

		// Token: 0x14001D1C RID: 7452
		// (add) Token: 0x0600F083 RID: 61571
		// (remove) Token: 0x0600F084 RID: 61572
		public virtual extern event HTMLAreaEvents_ondragoverEventHandler HTMLAreaEvents_Event_ondragover;

		// Token: 0x14001D1D RID: 7453
		// (add) Token: 0x0600F085 RID: 61573
		// (remove) Token: 0x0600F086 RID: 61574
		public virtual extern event HTMLAreaEvents_ondragleaveEventHandler HTMLAreaEvents_Event_ondragleave;

		// Token: 0x14001D1E RID: 7454
		// (add) Token: 0x0600F087 RID: 61575
		// (remove) Token: 0x0600F088 RID: 61576
		public virtual extern event HTMLAreaEvents_ondropEventHandler HTMLAreaEvents_Event_ondrop;

		// Token: 0x14001D1F RID: 7455
		// (add) Token: 0x0600F089 RID: 61577
		// (remove) Token: 0x0600F08A RID: 61578
		public virtual extern event HTMLAreaEvents_onbeforecutEventHandler HTMLAreaEvents_Event_onbeforecut;

		// Token: 0x14001D20 RID: 7456
		// (add) Token: 0x0600F08B RID: 61579
		// (remove) Token: 0x0600F08C RID: 61580
		public virtual extern event HTMLAreaEvents_oncutEventHandler HTMLAreaEvents_Event_oncut;

		// Token: 0x14001D21 RID: 7457
		// (add) Token: 0x0600F08D RID: 61581
		// (remove) Token: 0x0600F08E RID: 61582
		public virtual extern event HTMLAreaEvents_onbeforecopyEventHandler HTMLAreaEvents_Event_onbeforecopy;

		// Token: 0x14001D22 RID: 7458
		// (add) Token: 0x0600F08F RID: 61583
		// (remove) Token: 0x0600F090 RID: 61584
		public virtual extern event HTMLAreaEvents_oncopyEventHandler HTMLAreaEvents_Event_oncopy;

		// Token: 0x14001D23 RID: 7459
		// (add) Token: 0x0600F091 RID: 61585
		// (remove) Token: 0x0600F092 RID: 61586
		public virtual extern event HTMLAreaEvents_onbeforepasteEventHandler HTMLAreaEvents_Event_onbeforepaste;

		// Token: 0x14001D24 RID: 7460
		// (add) Token: 0x0600F093 RID: 61587
		// (remove) Token: 0x0600F094 RID: 61588
		public virtual extern event HTMLAreaEvents_onpasteEventHandler HTMLAreaEvents_Event_onpaste;

		// Token: 0x14001D25 RID: 7461
		// (add) Token: 0x0600F095 RID: 61589
		// (remove) Token: 0x0600F096 RID: 61590
		public virtual extern event HTMLAreaEvents_oncontextmenuEventHandler HTMLAreaEvents_Event_oncontextmenu;

		// Token: 0x14001D26 RID: 7462
		// (add) Token: 0x0600F097 RID: 61591
		// (remove) Token: 0x0600F098 RID: 61592
		public virtual extern event HTMLAreaEvents_onrowsdeleteEventHandler HTMLAreaEvents_Event_onrowsdelete;

		// Token: 0x14001D27 RID: 7463
		// (add) Token: 0x0600F099 RID: 61593
		// (remove) Token: 0x0600F09A RID: 61594
		public virtual extern event HTMLAreaEvents_onrowsinsertedEventHandler HTMLAreaEvents_Event_onrowsinserted;

		// Token: 0x14001D28 RID: 7464
		// (add) Token: 0x0600F09B RID: 61595
		// (remove) Token: 0x0600F09C RID: 61596
		public virtual extern event HTMLAreaEvents_oncellchangeEventHandler HTMLAreaEvents_Event_oncellchange;

		// Token: 0x14001D29 RID: 7465
		// (add) Token: 0x0600F09D RID: 61597
		// (remove) Token: 0x0600F09E RID: 61598
		public virtual extern event HTMLAreaEvents_onreadystatechangeEventHandler HTMLAreaEvents_Event_onreadystatechange;

		// Token: 0x14001D2A RID: 7466
		// (add) Token: 0x0600F09F RID: 61599
		// (remove) Token: 0x0600F0A0 RID: 61600
		public virtual extern event HTMLAreaEvents_onbeforeeditfocusEventHandler HTMLAreaEvents_Event_onbeforeeditfocus;

		// Token: 0x14001D2B RID: 7467
		// (add) Token: 0x0600F0A1 RID: 61601
		// (remove) Token: 0x0600F0A2 RID: 61602
		public virtual extern event HTMLAreaEvents_onlayoutcompleteEventHandler HTMLAreaEvents_Event_onlayoutcomplete;

		// Token: 0x14001D2C RID: 7468
		// (add) Token: 0x0600F0A3 RID: 61603
		// (remove) Token: 0x0600F0A4 RID: 61604
		public virtual extern event HTMLAreaEvents_onpageEventHandler HTMLAreaEvents_Event_onpage;

		// Token: 0x14001D2D RID: 7469
		// (add) Token: 0x0600F0A5 RID: 61605
		// (remove) Token: 0x0600F0A6 RID: 61606
		public virtual extern event HTMLAreaEvents_onbeforedeactivateEventHandler HTMLAreaEvents_Event_onbeforedeactivate;

		// Token: 0x14001D2E RID: 7470
		// (add) Token: 0x0600F0A7 RID: 61607
		// (remove) Token: 0x0600F0A8 RID: 61608
		public virtual extern event HTMLAreaEvents_onbeforeactivateEventHandler HTMLAreaEvents_Event_onbeforeactivate;

		// Token: 0x14001D2F RID: 7471
		// (add) Token: 0x0600F0A9 RID: 61609
		// (remove) Token: 0x0600F0AA RID: 61610
		public virtual extern event HTMLAreaEvents_onmoveEventHandler HTMLAreaEvents_Event_onmove;

		// Token: 0x14001D30 RID: 7472
		// (add) Token: 0x0600F0AB RID: 61611
		// (remove) Token: 0x0600F0AC RID: 61612
		public virtual extern event HTMLAreaEvents_oncontrolselectEventHandler HTMLAreaEvents_Event_oncontrolselect;

		// Token: 0x14001D31 RID: 7473
		// (add) Token: 0x0600F0AD RID: 61613
		// (remove) Token: 0x0600F0AE RID: 61614
		public virtual extern event HTMLAreaEvents_onmovestartEventHandler HTMLAreaEvents_Event_onmovestart;

		// Token: 0x14001D32 RID: 7474
		// (add) Token: 0x0600F0AF RID: 61615
		// (remove) Token: 0x0600F0B0 RID: 61616
		public virtual extern event HTMLAreaEvents_onmoveendEventHandler HTMLAreaEvents_Event_onmoveend;

		// Token: 0x14001D33 RID: 7475
		// (add) Token: 0x0600F0B1 RID: 61617
		// (remove) Token: 0x0600F0B2 RID: 61618
		public virtual extern event HTMLAreaEvents_onresizestartEventHandler HTMLAreaEvents_Event_onresizestart;

		// Token: 0x14001D34 RID: 7476
		// (add) Token: 0x0600F0B3 RID: 61619
		// (remove) Token: 0x0600F0B4 RID: 61620
		public virtual extern event HTMLAreaEvents_onresizeendEventHandler HTMLAreaEvents_Event_onresizeend;

		// Token: 0x14001D35 RID: 7477
		// (add) Token: 0x0600F0B5 RID: 61621
		// (remove) Token: 0x0600F0B6 RID: 61622
		public virtual extern event HTMLAreaEvents_onmouseenterEventHandler HTMLAreaEvents_Event_onmouseenter;

		// Token: 0x14001D36 RID: 7478
		// (add) Token: 0x0600F0B7 RID: 61623
		// (remove) Token: 0x0600F0B8 RID: 61624
		public virtual extern event HTMLAreaEvents_onmouseleaveEventHandler HTMLAreaEvents_Event_onmouseleave;

		// Token: 0x14001D37 RID: 7479
		// (add) Token: 0x0600F0B9 RID: 61625
		// (remove) Token: 0x0600F0BA RID: 61626
		public virtual extern event HTMLAreaEvents_onmousewheelEventHandler HTMLAreaEvents_Event_onmousewheel;

		// Token: 0x14001D38 RID: 7480
		// (add) Token: 0x0600F0BB RID: 61627
		// (remove) Token: 0x0600F0BC RID: 61628
		public virtual extern event HTMLAreaEvents_onactivateEventHandler HTMLAreaEvents_Event_onactivate;

		// Token: 0x14001D39 RID: 7481
		// (add) Token: 0x0600F0BD RID: 61629
		// (remove) Token: 0x0600F0BE RID: 61630
		public virtual extern event HTMLAreaEvents_ondeactivateEventHandler HTMLAreaEvents_Event_ondeactivate;

		// Token: 0x14001D3A RID: 7482
		// (add) Token: 0x0600F0BF RID: 61631
		// (remove) Token: 0x0600F0C0 RID: 61632
		public virtual extern event HTMLAreaEvents_onfocusinEventHandler HTMLAreaEvents_Event_onfocusin;

		// Token: 0x14001D3B RID: 7483
		// (add) Token: 0x0600F0C1 RID: 61633
		// (remove) Token: 0x0600F0C2 RID: 61634
		public virtual extern event HTMLAreaEvents_onfocusoutEventHandler HTMLAreaEvents_Event_onfocusout;

		// Token: 0x14001D3C RID: 7484
		// (add) Token: 0x0600F0C3 RID: 61635
		// (remove) Token: 0x0600F0C4 RID: 61636
		public virtual extern event HTMLAreaEvents2_onhelpEventHandler HTMLAreaEvents2_Event_onhelp;

		// Token: 0x14001D3D RID: 7485
		// (add) Token: 0x0600F0C5 RID: 61637
		// (remove) Token: 0x0600F0C6 RID: 61638
		public virtual extern event HTMLAreaEvents2_onclickEventHandler HTMLAreaEvents2_Event_onclick;

		// Token: 0x14001D3E RID: 7486
		// (add) Token: 0x0600F0C7 RID: 61639
		// (remove) Token: 0x0600F0C8 RID: 61640
		public virtual extern event HTMLAreaEvents2_ondblclickEventHandler HTMLAreaEvents2_Event_ondblclick;

		// Token: 0x14001D3F RID: 7487
		// (add) Token: 0x0600F0C9 RID: 61641
		// (remove) Token: 0x0600F0CA RID: 61642
		public virtual extern event HTMLAreaEvents2_onkeypressEventHandler HTMLAreaEvents2_Event_onkeypress;

		// Token: 0x14001D40 RID: 7488
		// (add) Token: 0x0600F0CB RID: 61643
		// (remove) Token: 0x0600F0CC RID: 61644
		public virtual extern event HTMLAreaEvents2_onkeydownEventHandler HTMLAreaEvents2_Event_onkeydown;

		// Token: 0x14001D41 RID: 7489
		// (add) Token: 0x0600F0CD RID: 61645
		// (remove) Token: 0x0600F0CE RID: 61646
		public virtual extern event HTMLAreaEvents2_onkeyupEventHandler HTMLAreaEvents2_Event_onkeyup;

		// Token: 0x14001D42 RID: 7490
		// (add) Token: 0x0600F0CF RID: 61647
		// (remove) Token: 0x0600F0D0 RID: 61648
		public virtual extern event HTMLAreaEvents2_onmouseoutEventHandler HTMLAreaEvents2_Event_onmouseout;

		// Token: 0x14001D43 RID: 7491
		// (add) Token: 0x0600F0D1 RID: 61649
		// (remove) Token: 0x0600F0D2 RID: 61650
		public virtual extern event HTMLAreaEvents2_onmouseoverEventHandler HTMLAreaEvents2_Event_onmouseover;

		// Token: 0x14001D44 RID: 7492
		// (add) Token: 0x0600F0D3 RID: 61651
		// (remove) Token: 0x0600F0D4 RID: 61652
		public virtual extern event HTMLAreaEvents2_onmousemoveEventHandler HTMLAreaEvents2_Event_onmousemove;

		// Token: 0x14001D45 RID: 7493
		// (add) Token: 0x0600F0D5 RID: 61653
		// (remove) Token: 0x0600F0D6 RID: 61654
		public virtual extern event HTMLAreaEvents2_onmousedownEventHandler HTMLAreaEvents2_Event_onmousedown;

		// Token: 0x14001D46 RID: 7494
		// (add) Token: 0x0600F0D7 RID: 61655
		// (remove) Token: 0x0600F0D8 RID: 61656
		public virtual extern event HTMLAreaEvents2_onmouseupEventHandler HTMLAreaEvents2_Event_onmouseup;

		// Token: 0x14001D47 RID: 7495
		// (add) Token: 0x0600F0D9 RID: 61657
		// (remove) Token: 0x0600F0DA RID: 61658
		public virtual extern event HTMLAreaEvents2_onselectstartEventHandler HTMLAreaEvents2_Event_onselectstart;

		// Token: 0x14001D48 RID: 7496
		// (add) Token: 0x0600F0DB RID: 61659
		// (remove) Token: 0x0600F0DC RID: 61660
		public virtual extern event HTMLAreaEvents2_onfilterchangeEventHandler HTMLAreaEvents2_Event_onfilterchange;

		// Token: 0x14001D49 RID: 7497
		// (add) Token: 0x0600F0DD RID: 61661
		// (remove) Token: 0x0600F0DE RID: 61662
		public virtual extern event HTMLAreaEvents2_ondragstartEventHandler HTMLAreaEvents2_Event_ondragstart;

		// Token: 0x14001D4A RID: 7498
		// (add) Token: 0x0600F0DF RID: 61663
		// (remove) Token: 0x0600F0E0 RID: 61664
		public virtual extern event HTMLAreaEvents2_onbeforeupdateEventHandler HTMLAreaEvents2_Event_onbeforeupdate;

		// Token: 0x14001D4B RID: 7499
		// (add) Token: 0x0600F0E1 RID: 61665
		// (remove) Token: 0x0600F0E2 RID: 61666
		public virtual extern event HTMLAreaEvents2_onafterupdateEventHandler HTMLAreaEvents2_Event_onafterupdate;

		// Token: 0x14001D4C RID: 7500
		// (add) Token: 0x0600F0E3 RID: 61667
		// (remove) Token: 0x0600F0E4 RID: 61668
		public virtual extern event HTMLAreaEvents2_onerrorupdateEventHandler HTMLAreaEvents2_Event_onerrorupdate;

		// Token: 0x14001D4D RID: 7501
		// (add) Token: 0x0600F0E5 RID: 61669
		// (remove) Token: 0x0600F0E6 RID: 61670
		public virtual extern event HTMLAreaEvents2_onrowexitEventHandler HTMLAreaEvents2_Event_onrowexit;

		// Token: 0x14001D4E RID: 7502
		// (add) Token: 0x0600F0E7 RID: 61671
		// (remove) Token: 0x0600F0E8 RID: 61672
		public virtual extern event HTMLAreaEvents2_onrowenterEventHandler HTMLAreaEvents2_Event_onrowenter;

		// Token: 0x14001D4F RID: 7503
		// (add) Token: 0x0600F0E9 RID: 61673
		// (remove) Token: 0x0600F0EA RID: 61674
		public virtual extern event HTMLAreaEvents2_ondatasetchangedEventHandler HTMLAreaEvents2_Event_ondatasetchanged;

		// Token: 0x14001D50 RID: 7504
		// (add) Token: 0x0600F0EB RID: 61675
		// (remove) Token: 0x0600F0EC RID: 61676
		public virtual extern event HTMLAreaEvents2_ondataavailableEventHandler HTMLAreaEvents2_Event_ondataavailable;

		// Token: 0x14001D51 RID: 7505
		// (add) Token: 0x0600F0ED RID: 61677
		// (remove) Token: 0x0600F0EE RID: 61678
		public virtual extern event HTMLAreaEvents2_ondatasetcompleteEventHandler HTMLAreaEvents2_Event_ondatasetcomplete;

		// Token: 0x14001D52 RID: 7506
		// (add) Token: 0x0600F0EF RID: 61679
		// (remove) Token: 0x0600F0F0 RID: 61680
		public virtual extern event HTMLAreaEvents2_onlosecaptureEventHandler HTMLAreaEvents2_Event_onlosecapture;

		// Token: 0x14001D53 RID: 7507
		// (add) Token: 0x0600F0F1 RID: 61681
		// (remove) Token: 0x0600F0F2 RID: 61682
		public virtual extern event HTMLAreaEvents2_onpropertychangeEventHandler HTMLAreaEvents2_Event_onpropertychange;

		// Token: 0x14001D54 RID: 7508
		// (add) Token: 0x0600F0F3 RID: 61683
		// (remove) Token: 0x0600F0F4 RID: 61684
		public virtual extern event HTMLAreaEvents2_onscrollEventHandler HTMLAreaEvents2_Event_onscroll;

		// Token: 0x14001D55 RID: 7509
		// (add) Token: 0x0600F0F5 RID: 61685
		// (remove) Token: 0x0600F0F6 RID: 61686
		public virtual extern event HTMLAreaEvents2_onfocusEventHandler HTMLAreaEvents2_Event_onfocus;

		// Token: 0x14001D56 RID: 7510
		// (add) Token: 0x0600F0F7 RID: 61687
		// (remove) Token: 0x0600F0F8 RID: 61688
		public virtual extern event HTMLAreaEvents2_onblurEventHandler HTMLAreaEvents2_Event_onblur;

		// Token: 0x14001D57 RID: 7511
		// (add) Token: 0x0600F0F9 RID: 61689
		// (remove) Token: 0x0600F0FA RID: 61690
		public virtual extern event HTMLAreaEvents2_onresizeEventHandler HTMLAreaEvents2_Event_onresize;

		// Token: 0x14001D58 RID: 7512
		// (add) Token: 0x0600F0FB RID: 61691
		// (remove) Token: 0x0600F0FC RID: 61692
		public virtual extern event HTMLAreaEvents2_ondragEventHandler HTMLAreaEvents2_Event_ondrag;

		// Token: 0x14001D59 RID: 7513
		// (add) Token: 0x0600F0FD RID: 61693
		// (remove) Token: 0x0600F0FE RID: 61694
		public virtual extern event HTMLAreaEvents2_ondragendEventHandler HTMLAreaEvents2_Event_ondragend;

		// Token: 0x14001D5A RID: 7514
		// (add) Token: 0x0600F0FF RID: 61695
		// (remove) Token: 0x0600F100 RID: 61696
		public virtual extern event HTMLAreaEvents2_ondragenterEventHandler HTMLAreaEvents2_Event_ondragenter;

		// Token: 0x14001D5B RID: 7515
		// (add) Token: 0x0600F101 RID: 61697
		// (remove) Token: 0x0600F102 RID: 61698
		public virtual extern event HTMLAreaEvents2_ondragoverEventHandler HTMLAreaEvents2_Event_ondragover;

		// Token: 0x14001D5C RID: 7516
		// (add) Token: 0x0600F103 RID: 61699
		// (remove) Token: 0x0600F104 RID: 61700
		public virtual extern event HTMLAreaEvents2_ondragleaveEventHandler HTMLAreaEvents2_Event_ondragleave;

		// Token: 0x14001D5D RID: 7517
		// (add) Token: 0x0600F105 RID: 61701
		// (remove) Token: 0x0600F106 RID: 61702
		public virtual extern event HTMLAreaEvents2_ondropEventHandler HTMLAreaEvents2_Event_ondrop;

		// Token: 0x14001D5E RID: 7518
		// (add) Token: 0x0600F107 RID: 61703
		// (remove) Token: 0x0600F108 RID: 61704
		public virtual extern event HTMLAreaEvents2_onbeforecutEventHandler HTMLAreaEvents2_Event_onbeforecut;

		// Token: 0x14001D5F RID: 7519
		// (add) Token: 0x0600F109 RID: 61705
		// (remove) Token: 0x0600F10A RID: 61706
		public virtual extern event HTMLAreaEvents2_oncutEventHandler HTMLAreaEvents2_Event_oncut;

		// Token: 0x14001D60 RID: 7520
		// (add) Token: 0x0600F10B RID: 61707
		// (remove) Token: 0x0600F10C RID: 61708
		public virtual extern event HTMLAreaEvents2_onbeforecopyEventHandler HTMLAreaEvents2_Event_onbeforecopy;

		// Token: 0x14001D61 RID: 7521
		// (add) Token: 0x0600F10D RID: 61709
		// (remove) Token: 0x0600F10E RID: 61710
		public virtual extern event HTMLAreaEvents2_oncopyEventHandler HTMLAreaEvents2_Event_oncopy;

		// Token: 0x14001D62 RID: 7522
		// (add) Token: 0x0600F10F RID: 61711
		// (remove) Token: 0x0600F110 RID: 61712
		public virtual extern event HTMLAreaEvents2_onbeforepasteEventHandler HTMLAreaEvents2_Event_onbeforepaste;

		// Token: 0x14001D63 RID: 7523
		// (add) Token: 0x0600F111 RID: 61713
		// (remove) Token: 0x0600F112 RID: 61714
		public virtual extern event HTMLAreaEvents2_onpasteEventHandler HTMLAreaEvents2_Event_onpaste;

		// Token: 0x14001D64 RID: 7524
		// (add) Token: 0x0600F113 RID: 61715
		// (remove) Token: 0x0600F114 RID: 61716
		public virtual extern event HTMLAreaEvents2_oncontextmenuEventHandler HTMLAreaEvents2_Event_oncontextmenu;

		// Token: 0x14001D65 RID: 7525
		// (add) Token: 0x0600F115 RID: 61717
		// (remove) Token: 0x0600F116 RID: 61718
		public virtual extern event HTMLAreaEvents2_onrowsdeleteEventHandler HTMLAreaEvents2_Event_onrowsdelete;

		// Token: 0x14001D66 RID: 7526
		// (add) Token: 0x0600F117 RID: 61719
		// (remove) Token: 0x0600F118 RID: 61720
		public virtual extern event HTMLAreaEvents2_onrowsinsertedEventHandler HTMLAreaEvents2_Event_onrowsinserted;

		// Token: 0x14001D67 RID: 7527
		// (add) Token: 0x0600F119 RID: 61721
		// (remove) Token: 0x0600F11A RID: 61722
		public virtual extern event HTMLAreaEvents2_oncellchangeEventHandler HTMLAreaEvents2_Event_oncellchange;

		// Token: 0x14001D68 RID: 7528
		// (add) Token: 0x0600F11B RID: 61723
		// (remove) Token: 0x0600F11C RID: 61724
		public virtual extern event HTMLAreaEvents2_onreadystatechangeEventHandler HTMLAreaEvents2_Event_onreadystatechange;

		// Token: 0x14001D69 RID: 7529
		// (add) Token: 0x0600F11D RID: 61725
		// (remove) Token: 0x0600F11E RID: 61726
		public virtual extern event HTMLAreaEvents2_onlayoutcompleteEventHandler HTMLAreaEvents2_Event_onlayoutcomplete;

		// Token: 0x14001D6A RID: 7530
		// (add) Token: 0x0600F11F RID: 61727
		// (remove) Token: 0x0600F120 RID: 61728
		public virtual extern event HTMLAreaEvents2_onpageEventHandler HTMLAreaEvents2_Event_onpage;

		// Token: 0x14001D6B RID: 7531
		// (add) Token: 0x0600F121 RID: 61729
		// (remove) Token: 0x0600F122 RID: 61730
		public virtual extern event HTMLAreaEvents2_onmouseenterEventHandler HTMLAreaEvents2_Event_onmouseenter;

		// Token: 0x14001D6C RID: 7532
		// (add) Token: 0x0600F123 RID: 61731
		// (remove) Token: 0x0600F124 RID: 61732
		public virtual extern event HTMLAreaEvents2_onmouseleaveEventHandler HTMLAreaEvents2_Event_onmouseleave;

		// Token: 0x14001D6D RID: 7533
		// (add) Token: 0x0600F125 RID: 61733
		// (remove) Token: 0x0600F126 RID: 61734
		public virtual extern event HTMLAreaEvents2_onactivateEventHandler HTMLAreaEvents2_Event_onactivate;

		// Token: 0x14001D6E RID: 7534
		// (add) Token: 0x0600F127 RID: 61735
		// (remove) Token: 0x0600F128 RID: 61736
		public virtual extern event HTMLAreaEvents2_ondeactivateEventHandler HTMLAreaEvents2_Event_ondeactivate;

		// Token: 0x14001D6F RID: 7535
		// (add) Token: 0x0600F129 RID: 61737
		// (remove) Token: 0x0600F12A RID: 61738
		public virtual extern event HTMLAreaEvents2_onbeforedeactivateEventHandler HTMLAreaEvents2_Event_onbeforedeactivate;

		// Token: 0x14001D70 RID: 7536
		// (add) Token: 0x0600F12B RID: 61739
		// (remove) Token: 0x0600F12C RID: 61740
		public virtual extern event HTMLAreaEvents2_onbeforeactivateEventHandler HTMLAreaEvents2_Event_onbeforeactivate;

		// Token: 0x14001D71 RID: 7537
		// (add) Token: 0x0600F12D RID: 61741
		// (remove) Token: 0x0600F12E RID: 61742
		public virtual extern event HTMLAreaEvents2_onfocusinEventHandler HTMLAreaEvents2_Event_onfocusin;

		// Token: 0x14001D72 RID: 7538
		// (add) Token: 0x0600F12F RID: 61743
		// (remove) Token: 0x0600F130 RID: 61744
		public virtual extern event HTMLAreaEvents2_onfocusoutEventHandler HTMLAreaEvents2_Event_onfocusout;

		// Token: 0x14001D73 RID: 7539
		// (add) Token: 0x0600F131 RID: 61745
		// (remove) Token: 0x0600F132 RID: 61746
		public virtual extern event HTMLAreaEvents2_onmoveEventHandler HTMLAreaEvents2_Event_onmove;

		// Token: 0x14001D74 RID: 7540
		// (add) Token: 0x0600F133 RID: 61747
		// (remove) Token: 0x0600F134 RID: 61748
		public virtual extern event HTMLAreaEvents2_oncontrolselectEventHandler HTMLAreaEvents2_Event_oncontrolselect;

		// Token: 0x14001D75 RID: 7541
		// (add) Token: 0x0600F135 RID: 61749
		// (remove) Token: 0x0600F136 RID: 61750
		public virtual extern event HTMLAreaEvents2_onmovestartEventHandler HTMLAreaEvents2_Event_onmovestart;

		// Token: 0x14001D76 RID: 7542
		// (add) Token: 0x0600F137 RID: 61751
		// (remove) Token: 0x0600F138 RID: 61752
		public virtual extern event HTMLAreaEvents2_onmoveendEventHandler HTMLAreaEvents2_Event_onmoveend;

		// Token: 0x14001D77 RID: 7543
		// (add) Token: 0x0600F139 RID: 61753
		// (remove) Token: 0x0600F13A RID: 61754
		public virtual extern event HTMLAreaEvents2_onresizestartEventHandler HTMLAreaEvents2_Event_onresizestart;

		// Token: 0x14001D78 RID: 7544
		// (add) Token: 0x0600F13B RID: 61755
		// (remove) Token: 0x0600F13C RID: 61756
		public virtual extern event HTMLAreaEvents2_onresizeendEventHandler HTMLAreaEvents2_Event_onresizeend;

		// Token: 0x14001D79 RID: 7545
		// (add) Token: 0x0600F13D RID: 61757
		// (remove) Token: 0x0600F13E RID: 61758
		public virtual extern event HTMLAreaEvents2_onmousewheelEventHandler HTMLAreaEvents2_Event_onmousewheel;
	}
}
