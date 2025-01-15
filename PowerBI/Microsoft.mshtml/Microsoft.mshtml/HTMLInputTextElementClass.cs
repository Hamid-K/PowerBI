using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D12 RID: 3346
	[Guid("3050F2AB-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLInputTextElementEvents\0\0")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLInputTextElementClass : DispIHTMLInputTextElement, HTMLInputTextElement, HTMLInputTextElementEvents_Event, IHTMLInputTextElement, IHTMLControlElement, IHTMLElement, IHTMLDatabinding
	{
		// Token: 0x06016AC1 RID: 92865
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLInputTextElementClass();

		// Token: 0x06016AC2 RID: 92866
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06016AC3 RID: 92867
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06016AC4 RID: 92868
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170078D6 RID: 30934
		// (get) Token: 0x06016AC6 RID: 92870
		// (set) Token: 0x06016AC5 RID: 92869
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

		// Token: 0x170078D7 RID: 30935
		// (get) Token: 0x06016AC8 RID: 92872
		// (set) Token: 0x06016AC7 RID: 92871
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

		// Token: 0x170078D8 RID: 30936
		// (get) Token: 0x06016AC9 RID: 92873
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170078D9 RID: 30937
		// (get) Token: 0x06016ACA RID: 92874
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170078DA RID: 30938
		// (get) Token: 0x06016ACB RID: 92875
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170078DB RID: 30939
		// (get) Token: 0x06016ACD RID: 92877
		// (set) Token: 0x06016ACC RID: 92876
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

		// Token: 0x170078DC RID: 30940
		// (get) Token: 0x06016ACF RID: 92879
		// (set) Token: 0x06016ACE RID: 92878
		[DispId(-2147412104)]
		public virtual extern object onclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170078DD RID: 30941
		// (get) Token: 0x06016AD1 RID: 92881
		// (set) Token: 0x06016AD0 RID: 92880
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

		// Token: 0x170078DE RID: 30942
		// (get) Token: 0x06016AD3 RID: 92883
		// (set) Token: 0x06016AD2 RID: 92882
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

		// Token: 0x170078DF RID: 30943
		// (get) Token: 0x06016AD5 RID: 92885
		// (set) Token: 0x06016AD4 RID: 92884
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

		// Token: 0x170078E0 RID: 30944
		// (get) Token: 0x06016AD7 RID: 92887
		// (set) Token: 0x06016AD6 RID: 92886
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

		// Token: 0x170078E1 RID: 30945
		// (get) Token: 0x06016AD9 RID: 92889
		// (set) Token: 0x06016AD8 RID: 92888
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

		// Token: 0x170078E2 RID: 30946
		// (get) Token: 0x06016ADB RID: 92891
		// (set) Token: 0x06016ADA RID: 92890
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

		// Token: 0x170078E3 RID: 30947
		// (get) Token: 0x06016ADD RID: 92893
		// (set) Token: 0x06016ADC RID: 92892
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

		// Token: 0x170078E4 RID: 30948
		// (get) Token: 0x06016ADF RID: 92895
		// (set) Token: 0x06016ADE RID: 92894
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

		// Token: 0x170078E5 RID: 30949
		// (get) Token: 0x06016AE1 RID: 92897
		// (set) Token: 0x06016AE0 RID: 92896
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

		// Token: 0x170078E6 RID: 30950
		// (get) Token: 0x06016AE2 RID: 92898
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170078E7 RID: 30951
		// (get) Token: 0x06016AE4 RID: 92900
		// (set) Token: 0x06016AE3 RID: 92899
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

		// Token: 0x170078E8 RID: 30952
		// (get) Token: 0x06016AE6 RID: 92902
		// (set) Token: 0x06016AE5 RID: 92901
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

		// Token: 0x170078E9 RID: 30953
		// (get) Token: 0x06016AE8 RID: 92904
		// (set) Token: 0x06016AE7 RID: 92903
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

		// Token: 0x06016AE9 RID: 92905
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06016AEA RID: 92906
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170078EA RID: 30954
		// (get) Token: 0x06016AEB RID: 92907
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170078EB RID: 30955
		// (get) Token: 0x06016AEC RID: 92908
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170078EC RID: 30956
		// (get) Token: 0x06016AEE RID: 92910
		// (set) Token: 0x06016AED RID: 92909
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

		// Token: 0x170078ED RID: 30957
		// (get) Token: 0x06016AEF RID: 92911
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170078EE RID: 30958
		// (get) Token: 0x06016AF0 RID: 92912
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170078EF RID: 30959
		// (get) Token: 0x06016AF1 RID: 92913
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170078F0 RID: 30960
		// (get) Token: 0x06016AF2 RID: 92914
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170078F1 RID: 30961
		// (get) Token: 0x06016AF3 RID: 92915
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170078F2 RID: 30962
		// (get) Token: 0x06016AF5 RID: 92917
		// (set) Token: 0x06016AF4 RID: 92916
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

		// Token: 0x170078F3 RID: 30963
		// (get) Token: 0x06016AF7 RID: 92919
		// (set) Token: 0x06016AF6 RID: 92918
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

		// Token: 0x170078F4 RID: 30964
		// (get) Token: 0x06016AF9 RID: 92921
		// (set) Token: 0x06016AF8 RID: 92920
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

		// Token: 0x170078F5 RID: 30965
		// (get) Token: 0x06016AFB RID: 92923
		// (set) Token: 0x06016AFA RID: 92922
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

		// Token: 0x06016AFC RID: 92924
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06016AFD RID: 92925
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170078F6 RID: 30966
		// (get) Token: 0x06016AFE RID: 92926
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170078F7 RID: 30967
		// (get) Token: 0x06016AFF RID: 92927
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016B00 RID: 92928
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x170078F8 RID: 30968
		// (get) Token: 0x06016B01 RID: 92929
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170078F9 RID: 30969
		// (get) Token: 0x06016B03 RID: 92931
		// (set) Token: 0x06016B02 RID: 92930
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

		// Token: 0x06016B04 RID: 92932
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x170078FA RID: 30970
		// (get) Token: 0x06016B06 RID: 92934
		// (set) Token: 0x06016B05 RID: 92933
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

		// Token: 0x170078FB RID: 30971
		// (get) Token: 0x06016B08 RID: 92936
		// (set) Token: 0x06016B07 RID: 92935
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

		// Token: 0x170078FC RID: 30972
		// (get) Token: 0x06016B0A RID: 92938
		// (set) Token: 0x06016B09 RID: 92937
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

		// Token: 0x170078FD RID: 30973
		// (get) Token: 0x06016B0C RID: 92940
		// (set) Token: 0x06016B0B RID: 92939
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

		// Token: 0x170078FE RID: 30974
		// (get) Token: 0x06016B0E RID: 92942
		// (set) Token: 0x06016B0D RID: 92941
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

		// Token: 0x170078FF RID: 30975
		// (get) Token: 0x06016B10 RID: 92944
		// (set) Token: 0x06016B0F RID: 92943
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

		// Token: 0x17007900 RID: 30976
		// (get) Token: 0x06016B12 RID: 92946
		// (set) Token: 0x06016B11 RID: 92945
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

		// Token: 0x17007901 RID: 30977
		// (get) Token: 0x06016B14 RID: 92948
		// (set) Token: 0x06016B13 RID: 92947
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

		// Token: 0x17007902 RID: 30978
		// (get) Token: 0x06016B16 RID: 92950
		// (set) Token: 0x06016B15 RID: 92949
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

		// Token: 0x17007903 RID: 30979
		// (get) Token: 0x06016B17 RID: 92951
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007904 RID: 30980
		// (get) Token: 0x06016B18 RID: 92952
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007905 RID: 30981
		// (get) Token: 0x06016B1A RID: 92954
		// (set) Token: 0x06016B19 RID: 92953
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

		// Token: 0x06016B1B RID: 92955
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17007906 RID: 30982
		// (get) Token: 0x06016B1D RID: 92957
		// (set) Token: 0x06016B1C RID: 92956
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

		// Token: 0x17007907 RID: 30983
		// (get) Token: 0x06016B1F RID: 92959
		// (set) Token: 0x06016B1E RID: 92958
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

		// Token: 0x17007908 RID: 30984
		// (get) Token: 0x06016B21 RID: 92961
		// (set) Token: 0x06016B20 RID: 92960
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

		// Token: 0x17007909 RID: 30985
		// (get) Token: 0x06016B23 RID: 92963
		// (set) Token: 0x06016B22 RID: 92962
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

		// Token: 0x06016B24 RID: 92964
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06016B25 RID: 92965
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06016B26 RID: 92966
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700790A RID: 30986
		// (get) Token: 0x06016B27 RID: 92967
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700790B RID: 30987
		// (get) Token: 0x06016B28 RID: 92968
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700790C RID: 30988
		// (get) Token: 0x06016B29 RID: 92969
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700790D RID: 30989
		// (get) Token: 0x06016B2A RID: 92970
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700790E RID: 30990
		// (get) Token: 0x06016B2B RID: 92971
		[DispId(2000)]
		public virtual extern string type
		{
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700790F RID: 30991
		// (get) Token: 0x06016B2D RID: 92973
		// (set) Token: 0x06016B2C RID: 92972
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

		// Token: 0x17007910 RID: 30992
		// (get) Token: 0x06016B2F RID: 92975
		// (set) Token: 0x06016B2E RID: 92974
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

		// Token: 0x17007911 RID: 30993
		// (get) Token: 0x06016B31 RID: 92977
		// (set) Token: 0x06016B30 RID: 92976
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

		// Token: 0x17007912 RID: 30994
		// (get) Token: 0x06016B33 RID: 92979
		// (set) Token: 0x06016B32 RID: 92978
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

		// Token: 0x17007913 RID: 30995
		// (get) Token: 0x06016B34 RID: 92980
		[DispId(-2147416108)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007914 RID: 30996
		// (get) Token: 0x06016B36 RID: 92982
		// (set) Token: 0x06016B35 RID: 92981
		[DispId(-2147413029)]
		public virtual extern string defaultValue
		{
			[DispId(-2147413029)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147413029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007915 RID: 30997
		// (get) Token: 0x06016B38 RID: 92984
		// (set) Token: 0x06016B37 RID: 92983
		[DispId(2002)]
		public virtual extern int size
		{
			[TypeLibFunc(20)]
			[DispId(2002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17007916 RID: 30998
		// (get) Token: 0x06016B3A RID: 92986
		// (set) Token: 0x06016B39 RID: 92985
		[DispId(2003)]
		public virtual extern int maxLength
		{
			[TypeLibFunc(20)]
			[DispId(2003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06016B3B RID: 92987
		[DispId(2004)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void select();

		// Token: 0x17007917 RID: 30999
		// (get) Token: 0x06016B3D RID: 92989
		// (set) Token: 0x06016B3C RID: 92988
		[DispId(-2147412082)]
		public virtual extern object onchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412082)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007918 RID: 31000
		// (get) Token: 0x06016B3F RID: 92991
		// (set) Token: 0x06016B3E RID: 92990
		[DispId(-2147412102)]
		public virtual extern object onselect
		{
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007919 RID: 31001
		// (get) Token: 0x06016B41 RID: 92993
		// (set) Token: 0x06016B40 RID: 92992
		[DispId(2005)]
		public virtual extern bool readOnly
		{
			[DispId(2005)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06016B42 RID: 92994
		[DispId(2006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange createTextRange();

		// Token: 0x1700791A RID: 31002
		// (get) Token: 0x06016B44 RID: 92996
		// (set) Token: 0x06016B43 RID: 92995
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

		// Token: 0x1700791B RID: 31003
		// (get) Token: 0x06016B46 RID: 92998
		// (set) Token: 0x06016B45 RID: 92997
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

		// Token: 0x1700791C RID: 31004
		// (get) Token: 0x06016B48 RID: 93000
		// (set) Token: 0x06016B47 RID: 92999
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

		// Token: 0x1700791D RID: 31005
		// (get) Token: 0x06016B49 RID: 93001
		public virtual extern string IHTMLInputTextElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700791E RID: 31006
		// (get) Token: 0x06016B4B RID: 93003
		// (set) Token: 0x06016B4A RID: 93002
		public virtual extern string IHTMLInputTextElement_value
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

		// Token: 0x1700791F RID: 31007
		// (get) Token: 0x06016B4D RID: 93005
		// (set) Token: 0x06016B4C RID: 93004
		public virtual extern string IHTMLInputTextElement_name
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

		// Token: 0x17007920 RID: 31008
		// (get) Token: 0x06016B4F RID: 93007
		// (set) Token: 0x06016B4E RID: 93006
		public virtual extern object IHTMLInputTextElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007921 RID: 31009
		// (get) Token: 0x06016B51 RID: 93009
		// (set) Token: 0x06016B50 RID: 93008
		public virtual extern bool IHTMLInputTextElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007922 RID: 31010
		// (get) Token: 0x06016B52 RID: 93010
		public virtual extern IHTMLFormElement IHTMLInputTextElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007923 RID: 31011
		// (get) Token: 0x06016B54 RID: 93012
		// (set) Token: 0x06016B53 RID: 93011
		public virtual extern string IHTMLInputTextElement_defaultValue
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007924 RID: 31012
		// (get) Token: 0x06016B56 RID: 93014
		// (set) Token: 0x06016B55 RID: 93013
		public virtual extern int IHTMLInputTextElement_size
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007925 RID: 31013
		// (get) Token: 0x06016B58 RID: 93016
		// (set) Token: 0x06016B57 RID: 93015
		public virtual extern int IHTMLInputTextElement_maxLength
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06016B59 RID: 93017
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLInputTextElement_select();

		// Token: 0x17007926 RID: 31014
		// (get) Token: 0x06016B5B RID: 93019
		// (set) Token: 0x06016B5A RID: 93018
		public virtual extern object IHTMLInputTextElement_onchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007927 RID: 31015
		// (get) Token: 0x06016B5D RID: 93021
		// (set) Token: 0x06016B5C RID: 93020
		public virtual extern object IHTMLInputTextElement_onselect
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007928 RID: 31016
		// (get) Token: 0x06016B5F RID: 93023
		// (set) Token: 0x06016B5E RID: 93022
		public virtual extern bool IHTMLInputTextElement_readOnly
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06016B60 RID: 93024
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange IHTMLInputTextElement_createTextRange();

		// Token: 0x17007929 RID: 31017
		// (get) Token: 0x06016B62 RID: 93026
		// (set) Token: 0x06016B61 RID: 93025
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

		// Token: 0x06016B63 RID: 93027
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x1700792A RID: 31018
		// (get) Token: 0x06016B65 RID: 93029
		// (set) Token: 0x06016B64 RID: 93028
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

		// Token: 0x1700792B RID: 31019
		// (get) Token: 0x06016B67 RID: 93031
		// (set) Token: 0x06016B66 RID: 93030
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

		// Token: 0x1700792C RID: 31020
		// (get) Token: 0x06016B69 RID: 93033
		// (set) Token: 0x06016B68 RID: 93032
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

		// Token: 0x1700792D RID: 31021
		// (get) Token: 0x06016B6B RID: 93035
		// (set) Token: 0x06016B6A RID: 93034
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

		// Token: 0x06016B6C RID: 93036
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06016B6D RID: 93037
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06016B6E RID: 93038
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700792E RID: 31022
		// (get) Token: 0x06016B6F RID: 93039
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700792F RID: 31023
		// (get) Token: 0x06016B70 RID: 93040
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007930 RID: 31024
		// (get) Token: 0x06016B71 RID: 93041
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007931 RID: 31025
		// (get) Token: 0x06016B72 RID: 93042
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016B73 RID: 93043
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06016B74 RID: 93044
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06016B75 RID: 93045
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17007932 RID: 31026
		// (get) Token: 0x06016B77 RID: 93047
		// (set) Token: 0x06016B76 RID: 93046
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

		// Token: 0x17007933 RID: 31027
		// (get) Token: 0x06016B79 RID: 93049
		// (set) Token: 0x06016B78 RID: 93048
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

		// Token: 0x17007934 RID: 31028
		// (get) Token: 0x06016B7A RID: 93050
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007935 RID: 31029
		// (get) Token: 0x06016B7B RID: 93051
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007936 RID: 31030
		// (get) Token: 0x06016B7C RID: 93052
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007937 RID: 31031
		// (get) Token: 0x06016B7E RID: 93054
		// (set) Token: 0x06016B7D RID: 93053
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

		// Token: 0x17007938 RID: 31032
		// (get) Token: 0x06016B80 RID: 93056
		// (set) Token: 0x06016B7F RID: 93055
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

		// Token: 0x17007939 RID: 31033
		// (get) Token: 0x06016B82 RID: 93058
		// (set) Token: 0x06016B81 RID: 93057
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

		// Token: 0x1700793A RID: 31034
		// (get) Token: 0x06016B84 RID: 93060
		// (set) Token: 0x06016B83 RID: 93059
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

		// Token: 0x1700793B RID: 31035
		// (get) Token: 0x06016B86 RID: 93062
		// (set) Token: 0x06016B85 RID: 93061
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

		// Token: 0x1700793C RID: 31036
		// (get) Token: 0x06016B88 RID: 93064
		// (set) Token: 0x06016B87 RID: 93063
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

		// Token: 0x1700793D RID: 31037
		// (get) Token: 0x06016B8A RID: 93066
		// (set) Token: 0x06016B89 RID: 93065
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

		// Token: 0x1700793E RID: 31038
		// (get) Token: 0x06016B8C RID: 93068
		// (set) Token: 0x06016B8B RID: 93067
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

		// Token: 0x1700793F RID: 31039
		// (get) Token: 0x06016B8E RID: 93070
		// (set) Token: 0x06016B8D RID: 93069
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

		// Token: 0x17007940 RID: 31040
		// (get) Token: 0x06016B90 RID: 93072
		// (set) Token: 0x06016B8F RID: 93071
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

		// Token: 0x17007941 RID: 31041
		// (get) Token: 0x06016B92 RID: 93074
		// (set) Token: 0x06016B91 RID: 93073
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

		// Token: 0x17007942 RID: 31042
		// (get) Token: 0x06016B93 RID: 93075
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007943 RID: 31043
		// (get) Token: 0x06016B95 RID: 93077
		// (set) Token: 0x06016B94 RID: 93076
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

		// Token: 0x17007944 RID: 31044
		// (get) Token: 0x06016B97 RID: 93079
		// (set) Token: 0x06016B96 RID: 93078
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

		// Token: 0x17007945 RID: 31045
		// (get) Token: 0x06016B99 RID: 93081
		// (set) Token: 0x06016B98 RID: 93080
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

		// Token: 0x06016B9A RID: 93082
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06016B9B RID: 93083
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17007946 RID: 31046
		// (get) Token: 0x06016B9C RID: 93084
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007947 RID: 31047
		// (get) Token: 0x06016B9D RID: 93085
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007948 RID: 31048
		// (get) Token: 0x06016B9F RID: 93087
		// (set) Token: 0x06016B9E RID: 93086
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

		// Token: 0x17007949 RID: 31049
		// (get) Token: 0x06016BA0 RID: 93088
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700794A RID: 31050
		// (get) Token: 0x06016BA1 RID: 93089
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700794B RID: 31051
		// (get) Token: 0x06016BA2 RID: 93090
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700794C RID: 31052
		// (get) Token: 0x06016BA3 RID: 93091
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700794D RID: 31053
		// (get) Token: 0x06016BA4 RID: 93092
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700794E RID: 31054
		// (get) Token: 0x06016BA6 RID: 93094
		// (set) Token: 0x06016BA5 RID: 93093
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

		// Token: 0x1700794F RID: 31055
		// (get) Token: 0x06016BA8 RID: 93096
		// (set) Token: 0x06016BA7 RID: 93095
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

		// Token: 0x17007950 RID: 31056
		// (get) Token: 0x06016BAA RID: 93098
		// (set) Token: 0x06016BA9 RID: 93097
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

		// Token: 0x17007951 RID: 31057
		// (get) Token: 0x06016BAC RID: 93100
		// (set) Token: 0x06016BAB RID: 93099
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

		// Token: 0x06016BAD RID: 93101
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06016BAE RID: 93102
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17007952 RID: 31058
		// (get) Token: 0x06016BAF RID: 93103
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007953 RID: 31059
		// (get) Token: 0x06016BB0 RID: 93104
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016BB1 RID: 93105
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17007954 RID: 31060
		// (get) Token: 0x06016BB2 RID: 93106
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007955 RID: 31061
		// (get) Token: 0x06016BB4 RID: 93108
		// (set) Token: 0x06016BB3 RID: 93107
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

		// Token: 0x06016BB5 RID: 93109
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17007956 RID: 31062
		// (get) Token: 0x06016BB7 RID: 93111
		// (set) Token: 0x06016BB6 RID: 93110
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

		// Token: 0x17007957 RID: 31063
		// (get) Token: 0x06016BB9 RID: 93113
		// (set) Token: 0x06016BB8 RID: 93112
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

		// Token: 0x17007958 RID: 31064
		// (get) Token: 0x06016BBB RID: 93115
		// (set) Token: 0x06016BBA RID: 93114
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

		// Token: 0x17007959 RID: 31065
		// (get) Token: 0x06016BBD RID: 93117
		// (set) Token: 0x06016BBC RID: 93116
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

		// Token: 0x1700795A RID: 31066
		// (get) Token: 0x06016BBF RID: 93119
		// (set) Token: 0x06016BBE RID: 93118
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

		// Token: 0x1700795B RID: 31067
		// (get) Token: 0x06016BC1 RID: 93121
		// (set) Token: 0x06016BC0 RID: 93120
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

		// Token: 0x1700795C RID: 31068
		// (get) Token: 0x06016BC3 RID: 93123
		// (set) Token: 0x06016BC2 RID: 93122
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

		// Token: 0x1700795D RID: 31069
		// (get) Token: 0x06016BC5 RID: 93125
		// (set) Token: 0x06016BC4 RID: 93124
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

		// Token: 0x1700795E RID: 31070
		// (get) Token: 0x06016BC7 RID: 93127
		// (set) Token: 0x06016BC6 RID: 93126
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

		// Token: 0x1700795F RID: 31071
		// (get) Token: 0x06016BC8 RID: 93128
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007960 RID: 31072
		// (get) Token: 0x06016BC9 RID: 93129
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007961 RID: 31073
		// (get) Token: 0x06016BCB RID: 93131
		// (set) Token: 0x06016BCA RID: 93130
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

		// Token: 0x17007962 RID: 31074
		// (get) Token: 0x06016BCD RID: 93133
		// (set) Token: 0x06016BCC RID: 93132
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

		// Token: 0x17007963 RID: 31075
		// (get) Token: 0x06016BCF RID: 93135
		// (set) Token: 0x06016BCE RID: 93134
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

		// Token: 0x14002BAE RID: 11182
		// (add) Token: 0x06016BD0 RID: 93136
		// (remove) Token: 0x06016BD1 RID: 93137
		public virtual extern event HTMLInputTextElementEvents_onhelpEventHandler HTMLInputTextElementEvents_Event_onhelp;

		// Token: 0x14002BAF RID: 11183
		// (add) Token: 0x06016BD2 RID: 93138
		// (remove) Token: 0x06016BD3 RID: 93139
		public virtual extern event HTMLInputTextElementEvents_onclickEventHandler HTMLInputTextElementEvents_Event_onclick;

		// Token: 0x14002BB0 RID: 11184
		// (add) Token: 0x06016BD4 RID: 93140
		// (remove) Token: 0x06016BD5 RID: 93141
		public virtual extern event HTMLInputTextElementEvents_ondblclickEventHandler HTMLInputTextElementEvents_Event_ondblclick;

		// Token: 0x14002BB1 RID: 11185
		// (add) Token: 0x06016BD6 RID: 93142
		// (remove) Token: 0x06016BD7 RID: 93143
		public virtual extern event HTMLInputTextElementEvents_onkeypressEventHandler HTMLInputTextElementEvents_Event_onkeypress;

		// Token: 0x14002BB2 RID: 11186
		// (add) Token: 0x06016BD8 RID: 93144
		// (remove) Token: 0x06016BD9 RID: 93145
		public virtual extern event HTMLInputTextElementEvents_onkeydownEventHandler HTMLInputTextElementEvents_Event_onkeydown;

		// Token: 0x14002BB3 RID: 11187
		// (add) Token: 0x06016BDA RID: 93146
		// (remove) Token: 0x06016BDB RID: 93147
		public virtual extern event HTMLInputTextElementEvents_onkeyupEventHandler HTMLInputTextElementEvents_Event_onkeyup;

		// Token: 0x14002BB4 RID: 11188
		// (add) Token: 0x06016BDC RID: 93148
		// (remove) Token: 0x06016BDD RID: 93149
		public virtual extern event HTMLInputTextElementEvents_onmouseoutEventHandler HTMLInputTextElementEvents_Event_onmouseout;

		// Token: 0x14002BB5 RID: 11189
		// (add) Token: 0x06016BDE RID: 93150
		// (remove) Token: 0x06016BDF RID: 93151
		public virtual extern event HTMLInputTextElementEvents_onmouseoverEventHandler HTMLInputTextElementEvents_Event_onmouseover;

		// Token: 0x14002BB6 RID: 11190
		// (add) Token: 0x06016BE0 RID: 93152
		// (remove) Token: 0x06016BE1 RID: 93153
		public virtual extern event HTMLInputTextElementEvents_onmousemoveEventHandler HTMLInputTextElementEvents_Event_onmousemove;

		// Token: 0x14002BB7 RID: 11191
		// (add) Token: 0x06016BE2 RID: 93154
		// (remove) Token: 0x06016BE3 RID: 93155
		public virtual extern event HTMLInputTextElementEvents_onmousedownEventHandler HTMLInputTextElementEvents_Event_onmousedown;

		// Token: 0x14002BB8 RID: 11192
		// (add) Token: 0x06016BE4 RID: 93156
		// (remove) Token: 0x06016BE5 RID: 93157
		public virtual extern event HTMLInputTextElementEvents_onmouseupEventHandler HTMLInputTextElementEvents_Event_onmouseup;

		// Token: 0x14002BB9 RID: 11193
		// (add) Token: 0x06016BE6 RID: 93158
		// (remove) Token: 0x06016BE7 RID: 93159
		public virtual extern event HTMLInputTextElementEvents_onselectstartEventHandler HTMLInputTextElementEvents_Event_onselectstart;

		// Token: 0x14002BBA RID: 11194
		// (add) Token: 0x06016BE8 RID: 93160
		// (remove) Token: 0x06016BE9 RID: 93161
		public virtual extern event HTMLInputTextElementEvents_onfilterchangeEventHandler HTMLInputTextElementEvents_Event_onfilterchange;

		// Token: 0x14002BBB RID: 11195
		// (add) Token: 0x06016BEA RID: 93162
		// (remove) Token: 0x06016BEB RID: 93163
		public virtual extern event HTMLInputTextElementEvents_ondragstartEventHandler HTMLInputTextElementEvents_Event_ondragstart;

		// Token: 0x14002BBC RID: 11196
		// (add) Token: 0x06016BEC RID: 93164
		// (remove) Token: 0x06016BED RID: 93165
		public virtual extern event HTMLInputTextElementEvents_onbeforeupdateEventHandler HTMLInputTextElementEvents_Event_onbeforeupdate;

		// Token: 0x14002BBD RID: 11197
		// (add) Token: 0x06016BEE RID: 93166
		// (remove) Token: 0x06016BEF RID: 93167
		public virtual extern event HTMLInputTextElementEvents_onafterupdateEventHandler HTMLInputTextElementEvents_Event_onafterupdate;

		// Token: 0x14002BBE RID: 11198
		// (add) Token: 0x06016BF0 RID: 93168
		// (remove) Token: 0x06016BF1 RID: 93169
		public virtual extern event HTMLInputTextElementEvents_onerrorupdateEventHandler HTMLInputTextElementEvents_Event_onerrorupdate;

		// Token: 0x14002BBF RID: 11199
		// (add) Token: 0x06016BF2 RID: 93170
		// (remove) Token: 0x06016BF3 RID: 93171
		public virtual extern event HTMLInputTextElementEvents_onrowexitEventHandler HTMLInputTextElementEvents_Event_onrowexit;

		// Token: 0x14002BC0 RID: 11200
		// (add) Token: 0x06016BF4 RID: 93172
		// (remove) Token: 0x06016BF5 RID: 93173
		public virtual extern event HTMLInputTextElementEvents_onrowenterEventHandler HTMLInputTextElementEvents_Event_onrowenter;

		// Token: 0x14002BC1 RID: 11201
		// (add) Token: 0x06016BF6 RID: 93174
		// (remove) Token: 0x06016BF7 RID: 93175
		public virtual extern event HTMLInputTextElementEvents_ondatasetchangedEventHandler HTMLInputTextElementEvents_Event_ondatasetchanged;

		// Token: 0x14002BC2 RID: 11202
		// (add) Token: 0x06016BF8 RID: 93176
		// (remove) Token: 0x06016BF9 RID: 93177
		public virtual extern event HTMLInputTextElementEvents_ondataavailableEventHandler HTMLInputTextElementEvents_Event_ondataavailable;

		// Token: 0x14002BC3 RID: 11203
		// (add) Token: 0x06016BFA RID: 93178
		// (remove) Token: 0x06016BFB RID: 93179
		public virtual extern event HTMLInputTextElementEvents_ondatasetcompleteEventHandler HTMLInputTextElementEvents_Event_ondatasetcomplete;

		// Token: 0x14002BC4 RID: 11204
		// (add) Token: 0x06016BFC RID: 93180
		// (remove) Token: 0x06016BFD RID: 93181
		public virtual extern event HTMLInputTextElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14002BC5 RID: 11205
		// (add) Token: 0x06016BFE RID: 93182
		// (remove) Token: 0x06016BFF RID: 93183
		public virtual extern event HTMLInputTextElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14002BC6 RID: 11206
		// (add) Token: 0x06016C00 RID: 93184
		// (remove) Token: 0x06016C01 RID: 93185
		public virtual extern event HTMLInputTextElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14002BC7 RID: 11207
		// (add) Token: 0x06016C02 RID: 93186
		// (remove) Token: 0x06016C03 RID: 93187
		public virtual extern event HTMLInputTextElementEvents_onfocusEventHandler HTMLInputTextElementEvents_Event_onfocus;

		// Token: 0x14002BC8 RID: 11208
		// (add) Token: 0x06016C04 RID: 93188
		// (remove) Token: 0x06016C05 RID: 93189
		public virtual extern event HTMLInputTextElementEvents_onblurEventHandler HTMLInputTextElementEvents_Event_onblur;

		// Token: 0x14002BC9 RID: 11209
		// (add) Token: 0x06016C06 RID: 93190
		// (remove) Token: 0x06016C07 RID: 93191
		public virtual extern event HTMLInputTextElementEvents_onresizeEventHandler HTMLInputTextElementEvents_Event_onresize;

		// Token: 0x14002BCA RID: 11210
		// (add) Token: 0x06016C08 RID: 93192
		// (remove) Token: 0x06016C09 RID: 93193
		public virtual extern event HTMLInputTextElementEvents_ondragEventHandler ondrag;

		// Token: 0x14002BCB RID: 11211
		// (add) Token: 0x06016C0A RID: 93194
		// (remove) Token: 0x06016C0B RID: 93195
		public virtual extern event HTMLInputTextElementEvents_ondragendEventHandler ondragend;

		// Token: 0x14002BCC RID: 11212
		// (add) Token: 0x06016C0C RID: 93196
		// (remove) Token: 0x06016C0D RID: 93197
		public virtual extern event HTMLInputTextElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14002BCD RID: 11213
		// (add) Token: 0x06016C0E RID: 93198
		// (remove) Token: 0x06016C0F RID: 93199
		public virtual extern event HTMLInputTextElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x14002BCE RID: 11214
		// (add) Token: 0x06016C10 RID: 93200
		// (remove) Token: 0x06016C11 RID: 93201
		public virtual extern event HTMLInputTextElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14002BCF RID: 11215
		// (add) Token: 0x06016C12 RID: 93202
		// (remove) Token: 0x06016C13 RID: 93203
		public virtual extern event HTMLInputTextElementEvents_ondropEventHandler ondrop;

		// Token: 0x14002BD0 RID: 11216
		// (add) Token: 0x06016C14 RID: 93204
		// (remove) Token: 0x06016C15 RID: 93205
		public virtual extern event HTMLInputTextElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002BD1 RID: 11217
		// (add) Token: 0x06016C16 RID: 93206
		// (remove) Token: 0x06016C17 RID: 93207
		public virtual extern event HTMLInputTextElementEvents_oncutEventHandler oncut;

		// Token: 0x14002BD2 RID: 11218
		// (add) Token: 0x06016C18 RID: 93208
		// (remove) Token: 0x06016C19 RID: 93209
		public virtual extern event HTMLInputTextElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002BD3 RID: 11219
		// (add) Token: 0x06016C1A RID: 93210
		// (remove) Token: 0x06016C1B RID: 93211
		public virtual extern event HTMLInputTextElementEvents_oncopyEventHandler oncopy;

		// Token: 0x14002BD4 RID: 11220
		// (add) Token: 0x06016C1C RID: 93212
		// (remove) Token: 0x06016C1D RID: 93213
		public virtual extern event HTMLInputTextElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14002BD5 RID: 11221
		// (add) Token: 0x06016C1E RID: 93214
		// (remove) Token: 0x06016C1F RID: 93215
		public virtual extern event HTMLInputTextElementEvents_onpasteEventHandler onpaste;

		// Token: 0x14002BD6 RID: 11222
		// (add) Token: 0x06016C20 RID: 93216
		// (remove) Token: 0x06016C21 RID: 93217
		public virtual extern event HTMLInputTextElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14002BD7 RID: 11223
		// (add) Token: 0x06016C22 RID: 93218
		// (remove) Token: 0x06016C23 RID: 93219
		public virtual extern event HTMLInputTextElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14002BD8 RID: 11224
		// (add) Token: 0x06016C24 RID: 93220
		// (remove) Token: 0x06016C25 RID: 93221
		public virtual extern event HTMLInputTextElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14002BD9 RID: 11225
		// (add) Token: 0x06016C26 RID: 93222
		// (remove) Token: 0x06016C27 RID: 93223
		public virtual extern event HTMLInputTextElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14002BDA RID: 11226
		// (add) Token: 0x06016C28 RID: 93224
		// (remove) Token: 0x06016C29 RID: 93225
		public virtual extern event HTMLInputTextElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14002BDB RID: 11227
		// (add) Token: 0x06016C2A RID: 93226
		// (remove) Token: 0x06016C2B RID: 93227
		public virtual extern event HTMLInputTextElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14002BDC RID: 11228
		// (add) Token: 0x06016C2C RID: 93228
		// (remove) Token: 0x06016C2D RID: 93229
		public virtual extern event HTMLInputTextElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14002BDD RID: 11229
		// (add) Token: 0x06016C2E RID: 93230
		// (remove) Token: 0x06016C2F RID: 93231
		public virtual extern event HTMLInputTextElementEvents_onpageEventHandler onpage;

		// Token: 0x14002BDE RID: 11230
		// (add) Token: 0x06016C30 RID: 93232
		// (remove) Token: 0x06016C31 RID: 93233
		public virtual extern event HTMLInputTextElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002BDF RID: 11231
		// (add) Token: 0x06016C32 RID: 93234
		// (remove) Token: 0x06016C33 RID: 93235
		public virtual extern event HTMLInputTextElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002BE0 RID: 11232
		// (add) Token: 0x06016C34 RID: 93236
		// (remove) Token: 0x06016C35 RID: 93237
		public virtual extern event HTMLInputTextElementEvents_onmoveEventHandler onmove;

		// Token: 0x14002BE1 RID: 11233
		// (add) Token: 0x06016C36 RID: 93238
		// (remove) Token: 0x06016C37 RID: 93239
		public virtual extern event HTMLInputTextElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002BE2 RID: 11234
		// (add) Token: 0x06016C38 RID: 93240
		// (remove) Token: 0x06016C39 RID: 93241
		public virtual extern event HTMLInputTextElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14002BE3 RID: 11235
		// (add) Token: 0x06016C3A RID: 93242
		// (remove) Token: 0x06016C3B RID: 93243
		public virtual extern event HTMLInputTextElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14002BE4 RID: 11236
		// (add) Token: 0x06016C3C RID: 93244
		// (remove) Token: 0x06016C3D RID: 93245
		public virtual extern event HTMLInputTextElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14002BE5 RID: 11237
		// (add) Token: 0x06016C3E RID: 93246
		// (remove) Token: 0x06016C3F RID: 93247
		public virtual extern event HTMLInputTextElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14002BE6 RID: 11238
		// (add) Token: 0x06016C40 RID: 93248
		// (remove) Token: 0x06016C41 RID: 93249
		public virtual extern event HTMLInputTextElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14002BE7 RID: 11239
		// (add) Token: 0x06016C42 RID: 93250
		// (remove) Token: 0x06016C43 RID: 93251
		public virtual extern event HTMLInputTextElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14002BE8 RID: 11240
		// (add) Token: 0x06016C44 RID: 93252
		// (remove) Token: 0x06016C45 RID: 93253
		public virtual extern event HTMLInputTextElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14002BE9 RID: 11241
		// (add) Token: 0x06016C46 RID: 93254
		// (remove) Token: 0x06016C47 RID: 93255
		public virtual extern event HTMLInputTextElementEvents_onactivateEventHandler onactivate;

		// Token: 0x14002BEA RID: 11242
		// (add) Token: 0x06016C48 RID: 93256
		// (remove) Token: 0x06016C49 RID: 93257
		public virtual extern event HTMLInputTextElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002BEB RID: 11243
		// (add) Token: 0x06016C4A RID: 93258
		// (remove) Token: 0x06016C4B RID: 93259
		public virtual extern event HTMLInputTextElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14002BEC RID: 11244
		// (add) Token: 0x06016C4C RID: 93260
		// (remove) Token: 0x06016C4D RID: 93261
		public virtual extern event HTMLInputTextElementEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14002BED RID: 11245
		// (add) Token: 0x06016C4E RID: 93262
		// (remove) Token: 0x06016C4F RID: 93263
		public virtual extern event HTMLInputTextElementEvents_onchangeEventHandler HTMLInputTextElementEvents_Event_onchange;

		// Token: 0x14002BEE RID: 11246
		// (add) Token: 0x06016C50 RID: 93264
		// (remove) Token: 0x06016C51 RID: 93265
		public virtual extern event HTMLInputTextElementEvents_onselectEventHandler HTMLInputTextElementEvents_Event_onselect;

		// Token: 0x14002BEF RID: 11247
		// (add) Token: 0x06016C52 RID: 93266
		// (remove) Token: 0x06016C53 RID: 93267
		public virtual extern event HTMLInputTextElementEvents_onloadEventHandler onload;

		// Token: 0x14002BF0 RID: 11248
		// (add) Token: 0x06016C54 RID: 93268
		// (remove) Token: 0x06016C55 RID: 93269
		public virtual extern event HTMLInputTextElementEvents_onerrorEventHandler onerror;

		// Token: 0x14002BF1 RID: 11249
		// (add) Token: 0x06016C56 RID: 93270
		// (remove) Token: 0x06016C57 RID: 93271
		public virtual extern event HTMLInputTextElementEvents_onabortEventHandler onabort;
	}
}
