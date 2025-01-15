using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200077E RID: 1918
	[InterfaceType(2)]
	[Guid("3050F516-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLTitleElement
	{
		// Token: 0x0600B7BB RID: 47035
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600B7BC RID: 47036
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600B7BD RID: 47037
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17003B69 RID: 15209
		// (get) Token: 0x0600B7BF RID: 47039
		// (set) Token: 0x0600B7BE RID: 47038
		[DispId(-2147417111)]
		string className
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

		// Token: 0x17003B6A RID: 15210
		// (get) Token: 0x0600B7C1 RID: 47041
		// (set) Token: 0x0600B7C0 RID: 47040
		[DispId(-2147417110)]
		string id
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

		// Token: 0x17003B6B RID: 15211
		// (get) Token: 0x0600B7C2 RID: 47042
		[DispId(-2147417108)]
		string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003B6C RID: 15212
		// (get) Token: 0x0600B7C3 RID: 47043
		[DispId(-2147418104)]
		IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003B6D RID: 15213
		// (get) Token: 0x0600B7C4 RID: 47044
		[DispId(-2147418038)]
		IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003B6E RID: 15214
		// (get) Token: 0x0600B7C6 RID: 47046
		// (set) Token: 0x0600B7C5 RID: 47045
		[DispId(-2147412099)]
		object onhelp
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

		// Token: 0x17003B6F RID: 15215
		// (get) Token: 0x0600B7C8 RID: 47048
		// (set) Token: 0x0600B7C7 RID: 47047
		[DispId(-2147412104)]
		object onclick
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

		// Token: 0x17003B70 RID: 15216
		// (get) Token: 0x0600B7CA RID: 47050
		// (set) Token: 0x0600B7C9 RID: 47049
		[DispId(-2147412103)]
		object ondblclick
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

		// Token: 0x17003B71 RID: 15217
		// (get) Token: 0x0600B7CC RID: 47052
		// (set) Token: 0x0600B7CB RID: 47051
		[DispId(-2147412107)]
		object onkeydown
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

		// Token: 0x17003B72 RID: 15218
		// (get) Token: 0x0600B7CE RID: 47054
		// (set) Token: 0x0600B7CD RID: 47053
		[DispId(-2147412106)]
		object onkeyup
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

		// Token: 0x17003B73 RID: 15219
		// (get) Token: 0x0600B7D0 RID: 47056
		// (set) Token: 0x0600B7CF RID: 47055
		[DispId(-2147412105)]
		object onkeypress
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

		// Token: 0x17003B74 RID: 15220
		// (get) Token: 0x0600B7D2 RID: 47058
		// (set) Token: 0x0600B7D1 RID: 47057
		[DispId(-2147412111)]
		object onmouseout
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

		// Token: 0x17003B75 RID: 15221
		// (get) Token: 0x0600B7D4 RID: 47060
		// (set) Token: 0x0600B7D3 RID: 47059
		[DispId(-2147412112)]
		object onmouseover
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

		// Token: 0x17003B76 RID: 15222
		// (get) Token: 0x0600B7D6 RID: 47062
		// (set) Token: 0x0600B7D5 RID: 47061
		[DispId(-2147412108)]
		object onmousemove
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

		// Token: 0x17003B77 RID: 15223
		// (get) Token: 0x0600B7D8 RID: 47064
		// (set) Token: 0x0600B7D7 RID: 47063
		[DispId(-2147412110)]
		object onmousedown
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

		// Token: 0x17003B78 RID: 15224
		// (get) Token: 0x0600B7DA RID: 47066
		// (set) Token: 0x0600B7D9 RID: 47065
		[DispId(-2147412109)]
		object onmouseup
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

		// Token: 0x17003B79 RID: 15225
		// (get) Token: 0x0600B7DB RID: 47067
		[DispId(-2147417094)]
		object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003B7A RID: 15226
		// (get) Token: 0x0600B7DD RID: 47069
		// (set) Token: 0x0600B7DC RID: 47068
		[DispId(-2147418043)]
		string title
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

		// Token: 0x17003B7B RID: 15227
		// (get) Token: 0x0600B7DF RID: 47071
		// (set) Token: 0x0600B7DE RID: 47070
		[DispId(-2147413012)]
		string language
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

		// Token: 0x17003B7C RID: 15228
		// (get) Token: 0x0600B7E1 RID: 47073
		// (set) Token: 0x0600B7E0 RID: 47072
		[DispId(-2147412075)]
		object onselectstart
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

		// Token: 0x0600B7E2 RID: 47074
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600B7E3 RID: 47075
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17003B7D RID: 15229
		// (get) Token: 0x0600B7E4 RID: 47076
		[DispId(-2147417088)]
		int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003B7E RID: 15230
		// (get) Token: 0x0600B7E5 RID: 47077
		[DispId(-2147417087)]
		object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003B7F RID: 15231
		// (get) Token: 0x0600B7E7 RID: 47079
		// (set) Token: 0x0600B7E6 RID: 47078
		[DispId(-2147413103)]
		string lang
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

		// Token: 0x17003B80 RID: 15232
		// (get) Token: 0x0600B7E8 RID: 47080
		[DispId(-2147417104)]
		int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003B81 RID: 15233
		// (get) Token: 0x0600B7E9 RID: 47081
		[DispId(-2147417103)]
		int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003B82 RID: 15234
		// (get) Token: 0x0600B7EA RID: 47082
		[DispId(-2147417102)]
		int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003B83 RID: 15235
		// (get) Token: 0x0600B7EB RID: 47083
		[DispId(-2147417101)]
		int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003B84 RID: 15236
		// (get) Token: 0x0600B7EC RID: 47084
		[DispId(-2147417100)]
		IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003B85 RID: 15237
		// (get) Token: 0x0600B7EE RID: 47086
		// (set) Token: 0x0600B7ED RID: 47085
		[DispId(-2147417086)]
		string innerHTML
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

		// Token: 0x17003B86 RID: 15238
		// (get) Token: 0x0600B7F0 RID: 47088
		// (set) Token: 0x0600B7EF RID: 47087
		[DispId(-2147417085)]
		string innerText
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

		// Token: 0x17003B87 RID: 15239
		// (get) Token: 0x0600B7F2 RID: 47090
		// (set) Token: 0x0600B7F1 RID: 47089
		[DispId(-2147417084)]
		string outerHTML
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

		// Token: 0x17003B88 RID: 15240
		// (get) Token: 0x0600B7F4 RID: 47092
		// (set) Token: 0x0600B7F3 RID: 47091
		[DispId(-2147417083)]
		string outerText
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

		// Token: 0x0600B7F5 RID: 47093
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600B7F6 RID: 47094
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17003B89 RID: 15241
		// (get) Token: 0x0600B7F7 RID: 47095
		[DispId(-2147417080)]
		IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003B8A RID: 15242
		// (get) Token: 0x0600B7F8 RID: 47096
		[DispId(-2147417078)]
		bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600B7F9 RID: 47097
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void click();

		// Token: 0x17003B8B RID: 15243
		// (get) Token: 0x0600B7FA RID: 47098
		[DispId(-2147417077)]
		IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003B8C RID: 15244
		// (get) Token: 0x0600B7FC RID: 47100
		// (set) Token: 0x0600B7FB RID: 47099
		[DispId(-2147412077)]
		object ondragstart
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

		// Token: 0x0600B7FD RID: 47101
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x17003B8D RID: 15245
		// (get) Token: 0x0600B7FF RID: 47103
		// (set) Token: 0x0600B7FE RID: 47102
		[DispId(-2147412091)]
		object onbeforeupdate
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

		// Token: 0x17003B8E RID: 15246
		// (get) Token: 0x0600B801 RID: 47105
		// (set) Token: 0x0600B800 RID: 47104
		[DispId(-2147412090)]
		object onafterupdate
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

		// Token: 0x17003B8F RID: 15247
		// (get) Token: 0x0600B803 RID: 47107
		// (set) Token: 0x0600B802 RID: 47106
		[DispId(-2147412074)]
		object onerrorupdate
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

		// Token: 0x17003B90 RID: 15248
		// (get) Token: 0x0600B805 RID: 47109
		// (set) Token: 0x0600B804 RID: 47108
		[DispId(-2147412094)]
		object onrowexit
		{
			[DispId(-2147412094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003B91 RID: 15249
		// (get) Token: 0x0600B807 RID: 47111
		// (set) Token: 0x0600B806 RID: 47110
		[DispId(-2147412093)]
		object onrowenter
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

		// Token: 0x17003B92 RID: 15250
		// (get) Token: 0x0600B809 RID: 47113
		// (set) Token: 0x0600B808 RID: 47112
		[DispId(-2147412072)]
		object ondatasetchanged
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

		// Token: 0x17003B93 RID: 15251
		// (get) Token: 0x0600B80B RID: 47115
		// (set) Token: 0x0600B80A RID: 47114
		[DispId(-2147412071)]
		object ondataavailable
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

		// Token: 0x17003B94 RID: 15252
		// (get) Token: 0x0600B80D RID: 47117
		// (set) Token: 0x0600B80C RID: 47116
		[DispId(-2147412070)]
		object ondatasetcomplete
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

		// Token: 0x17003B95 RID: 15253
		// (get) Token: 0x0600B80F RID: 47119
		// (set) Token: 0x0600B80E RID: 47118
		[DispId(-2147412069)]
		object onfilterchange
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

		// Token: 0x17003B96 RID: 15254
		// (get) Token: 0x0600B810 RID: 47120
		[DispId(-2147417075)]
		object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003B97 RID: 15255
		// (get) Token: 0x0600B811 RID: 47121
		[DispId(-2147417074)]
		object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003B98 RID: 15256
		// (get) Token: 0x0600B812 RID: 47122
		[DispId(-2147417073)]
		string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600B813 RID: 47123
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setCapture([In] bool containerCapture = true);

		// Token: 0x0600B814 RID: 47124
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void releaseCapture();

		// Token: 0x17003B99 RID: 15257
		// (get) Token: 0x0600B816 RID: 47126
		// (set) Token: 0x0600B815 RID: 47125
		[DispId(-2147412066)]
		object onlosecapture
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

		// Token: 0x0600B817 RID: 47127
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600B818 RID: 47128
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17003B9A RID: 15258
		// (get) Token: 0x0600B81A RID: 47130
		// (set) Token: 0x0600B819 RID: 47129
		[DispId(-2147412081)]
		object onscroll
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

		// Token: 0x17003B9B RID: 15259
		// (get) Token: 0x0600B81C RID: 47132
		// (set) Token: 0x0600B81B RID: 47131
		[DispId(-2147412063)]
		object ondrag
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

		// Token: 0x17003B9C RID: 15260
		// (get) Token: 0x0600B81E RID: 47134
		// (set) Token: 0x0600B81D RID: 47133
		[DispId(-2147412062)]
		object ondragend
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

		// Token: 0x17003B9D RID: 15261
		// (get) Token: 0x0600B820 RID: 47136
		// (set) Token: 0x0600B81F RID: 47135
		[DispId(-2147412061)]
		object ondragenter
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

		// Token: 0x17003B9E RID: 15262
		// (get) Token: 0x0600B822 RID: 47138
		// (set) Token: 0x0600B821 RID: 47137
		[DispId(-2147412060)]
		object ondragover
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

		// Token: 0x17003B9F RID: 15263
		// (get) Token: 0x0600B824 RID: 47140
		// (set) Token: 0x0600B823 RID: 47139
		[DispId(-2147412059)]
		object ondragleave
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

		// Token: 0x17003BA0 RID: 15264
		// (get) Token: 0x0600B826 RID: 47142
		// (set) Token: 0x0600B825 RID: 47141
		[DispId(-2147412058)]
		object ondrop
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

		// Token: 0x17003BA1 RID: 15265
		// (get) Token: 0x0600B828 RID: 47144
		// (set) Token: 0x0600B827 RID: 47143
		[DispId(-2147412054)]
		object onbeforecut
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

		// Token: 0x17003BA2 RID: 15266
		// (get) Token: 0x0600B82A RID: 47146
		// (set) Token: 0x0600B829 RID: 47145
		[DispId(-2147412057)]
		object oncut
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

		// Token: 0x17003BA3 RID: 15267
		// (get) Token: 0x0600B82C RID: 47148
		// (set) Token: 0x0600B82B RID: 47147
		[DispId(-2147412053)]
		object onbeforecopy
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

		// Token: 0x17003BA4 RID: 15268
		// (get) Token: 0x0600B82E RID: 47150
		// (set) Token: 0x0600B82D RID: 47149
		[DispId(-2147412056)]
		object oncopy
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

		// Token: 0x17003BA5 RID: 15269
		// (get) Token: 0x0600B830 RID: 47152
		// (set) Token: 0x0600B82F RID: 47151
		[DispId(-2147412052)]
		object onbeforepaste
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

		// Token: 0x17003BA6 RID: 15270
		// (get) Token: 0x0600B832 RID: 47154
		// (set) Token: 0x0600B831 RID: 47153
		[DispId(-2147412055)]
		object onpaste
		{
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003BA7 RID: 15271
		// (get) Token: 0x0600B833 RID: 47155
		[DispId(-2147417105)]
		IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003BA8 RID: 15272
		// (get) Token: 0x0600B835 RID: 47157
		// (set) Token: 0x0600B834 RID: 47156
		[DispId(-2147412065)]
		object onpropertychange
		{
			[DispId(-2147412065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600B836 RID: 47158
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRectCollection getClientRects();

		// Token: 0x0600B837 RID: 47159
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRect getBoundingClientRect();

		// Token: 0x0600B838 RID: 47160
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600B839 RID: 47161
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600B83A RID: 47162
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17003BA9 RID: 15273
		// (get) Token: 0x0600B83C RID: 47164
		// (set) Token: 0x0600B83B RID: 47163
		[DispId(-2147418097)]
		short tabIndex
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

		// Token: 0x0600B83D RID: 47165
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void focus();

		// Token: 0x17003BAA RID: 15274
		// (get) Token: 0x0600B83F RID: 47167
		// (set) Token: 0x0600B83E RID: 47166
		[DispId(-2147416107)]
		string accessKey
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

		// Token: 0x17003BAB RID: 15275
		// (get) Token: 0x0600B841 RID: 47169
		// (set) Token: 0x0600B840 RID: 47168
		[DispId(-2147412097)]
		object onblur
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

		// Token: 0x17003BAC RID: 15276
		// (get) Token: 0x0600B843 RID: 47171
		// (set) Token: 0x0600B842 RID: 47170
		[DispId(-2147412098)]
		object onfocus
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

		// Token: 0x17003BAD RID: 15277
		// (get) Token: 0x0600B845 RID: 47173
		// (set) Token: 0x0600B844 RID: 47172
		[DispId(-2147412076)]
		object onresize
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

		// Token: 0x0600B846 RID: 47174
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void blur();

		// Token: 0x0600B847 RID: 47175
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600B848 RID: 47176
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17003BAE RID: 15278
		// (get) Token: 0x0600B849 RID: 47177
		[DispId(-2147416093)]
		int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BAF RID: 15279
		// (get) Token: 0x0600B84A RID: 47178
		[DispId(-2147416092)]
		int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BB0 RID: 15280
		// (get) Token: 0x0600B84B RID: 47179
		[DispId(-2147416091)]
		int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BB1 RID: 15281
		// (get) Token: 0x0600B84C RID: 47180
		[DispId(-2147416090)]
		int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600B84D RID: 47181
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600B84E RID: 47182
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17003BB2 RID: 15282
		// (get) Token: 0x0600B84F RID: 47183
		[DispId(-2147412996)]
		object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003BB3 RID: 15283
		// (get) Token: 0x0600B851 RID: 47185
		// (set) Token: 0x0600B850 RID: 47184
		[DispId(-2147412087)]
		object onreadystatechange
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

		// Token: 0x17003BB4 RID: 15284
		// (get) Token: 0x0600B853 RID: 47187
		// (set) Token: 0x0600B852 RID: 47186
		[DispId(-2147412050)]
		object onrowsdelete
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

		// Token: 0x17003BB5 RID: 15285
		// (get) Token: 0x0600B855 RID: 47189
		// (set) Token: 0x0600B854 RID: 47188
		[DispId(-2147412049)]
		object onrowsinserted
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

		// Token: 0x17003BB6 RID: 15286
		// (get) Token: 0x0600B857 RID: 47191
		// (set) Token: 0x0600B856 RID: 47190
		[DispId(-2147412048)]
		object oncellchange
		{
			[DispId(-2147412048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003BB7 RID: 15287
		// (get) Token: 0x0600B859 RID: 47193
		// (set) Token: 0x0600B858 RID: 47192
		[DispId(-2147412995)]
		string dir
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

		// Token: 0x0600B85A RID: 47194
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createControlRange();

		// Token: 0x17003BB8 RID: 15288
		// (get) Token: 0x0600B85B RID: 47195
		[DispId(-2147417055)]
		int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BB9 RID: 15289
		// (get) Token: 0x0600B85C RID: 47196
		[DispId(-2147417054)]
		int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BBA RID: 15290
		// (get) Token: 0x0600B85E RID: 47198
		// (set) Token: 0x0600B85D RID: 47197
		[DispId(-2147417053)]
		int scrollTop
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

		// Token: 0x17003BBB RID: 15291
		// (get) Token: 0x0600B860 RID: 47200
		// (set) Token: 0x0600B85F RID: 47199
		[DispId(-2147417052)]
		int scrollLeft
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

		// Token: 0x0600B861 RID: 47201
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void clearAttributes();

		// Token: 0x17003BBC RID: 15292
		// (get) Token: 0x0600B863 RID: 47203
		// (set) Token: 0x0600B862 RID: 47202
		[DispId(-2147412047)]
		object oncontextmenu
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

		// Token: 0x0600B864 RID: 47204
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600B865 RID: 47205
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600B866 RID: 47206
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600B867 RID: 47207
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17003BBD RID: 15293
		// (get) Token: 0x0600B868 RID: 47208
		[DispId(-2147417040)]
		bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600B869 RID: 47209
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600B86A RID: 47210
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeBehavior([In] int cookie);

		// Token: 0x17003BBE RID: 15294
		// (get) Token: 0x0600B86B RID: 47211
		[DispId(-2147417048)]
		IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003BBF RID: 15295
		// (get) Token: 0x0600B86C RID: 47212
		[DispId(-2147417030)]
		object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003BC0 RID: 15296
		// (get) Token: 0x0600B86E RID: 47214
		// (set) Token: 0x0600B86D RID: 47213
		[DispId(-2147417029)]
		string tagUrn
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

		// Token: 0x17003BC1 RID: 15297
		// (get) Token: 0x0600B870 RID: 47216
		// (set) Token: 0x0600B86F RID: 47215
		[DispId(-2147412043)]
		object onbeforeeditfocus
		{
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003BC2 RID: 15298
		// (get) Token: 0x0600B871 RID: 47217
		[DispId(-2147417028)]
		int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600B872 RID: 47218
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600B873 RID: 47219
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17003BC3 RID: 15299
		// (get) Token: 0x0600B874 RID: 47220
		[DispId(-2147417015)]
		bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BC4 RID: 15300
		// (get) Token: 0x0600B875 RID: 47221
		[DispId(-2147417014)]
		bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BC5 RID: 15301
		// (get) Token: 0x0600B877 RID: 47223
		// (set) Token: 0x0600B876 RID: 47222
		[DispId(-2147412039)]
		object onlayoutcomplete
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

		// Token: 0x17003BC6 RID: 15302
		// (get) Token: 0x0600B879 RID: 47225
		// (set) Token: 0x0600B878 RID: 47224
		[DispId(-2147412038)]
		object onpage
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

		// Token: 0x17003BC7 RID: 15303
		// (get) Token: 0x0600B87B RID: 47227
		// (set) Token: 0x0600B87A RID: 47226
		[DispId(-2147417012)]
		bool inflateBlock
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

		// Token: 0x17003BC8 RID: 15304
		// (get) Token: 0x0600B87D RID: 47229
		// (set) Token: 0x0600B87C RID: 47228
		[DispId(-2147412035)]
		object onbeforedeactivate
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

		// Token: 0x0600B87E RID: 47230
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setActive();

		// Token: 0x17003BC9 RID: 15305
		// (get) Token: 0x0600B880 RID: 47232
		// (set) Token: 0x0600B87F RID: 47231
		[DispId(-2147412950)]
		string contentEditable
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

		// Token: 0x17003BCA RID: 15306
		// (get) Token: 0x0600B881 RID: 47233
		[DispId(-2147417010)]
		bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BCB RID: 15307
		// (get) Token: 0x0600B883 RID: 47235
		// (set) Token: 0x0600B882 RID: 47234
		[DispId(-2147412949)]
		bool hideFocus
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

		// Token: 0x17003BCC RID: 15308
		// (get) Token: 0x0600B885 RID: 47237
		// (set) Token: 0x0600B884 RID: 47236
		[DispId(-2147418036)]
		bool disabled
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

		// Token: 0x17003BCD RID: 15309
		// (get) Token: 0x0600B886 RID: 47238
		[DispId(-2147417007)]
		bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BCE RID: 15310
		// (get) Token: 0x0600B888 RID: 47240
		// (set) Token: 0x0600B887 RID: 47239
		[DispId(-2147412034)]
		object onmove
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

		// Token: 0x17003BCF RID: 15311
		// (get) Token: 0x0600B88A RID: 47242
		// (set) Token: 0x0600B889 RID: 47241
		[DispId(-2147412033)]
		object oncontrolselect
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

		// Token: 0x0600B88B RID: 47243
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17003BD0 RID: 15312
		// (get) Token: 0x0600B88D RID: 47245
		// (set) Token: 0x0600B88C RID: 47244
		[DispId(-2147412029)]
		object onresizestart
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

		// Token: 0x17003BD1 RID: 15313
		// (get) Token: 0x0600B88F RID: 47247
		// (set) Token: 0x0600B88E RID: 47246
		[DispId(-2147412028)]
		object onresizeend
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

		// Token: 0x17003BD2 RID: 15314
		// (get) Token: 0x0600B891 RID: 47249
		// (set) Token: 0x0600B890 RID: 47248
		[DispId(-2147412031)]
		object onmovestart
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

		// Token: 0x17003BD3 RID: 15315
		// (get) Token: 0x0600B893 RID: 47251
		// (set) Token: 0x0600B892 RID: 47250
		[DispId(-2147412030)]
		object onmoveend
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

		// Token: 0x17003BD4 RID: 15316
		// (get) Token: 0x0600B895 RID: 47253
		// (set) Token: 0x0600B894 RID: 47252
		[DispId(-2147412027)]
		object onmouseenter
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

		// Token: 0x17003BD5 RID: 15317
		// (get) Token: 0x0600B897 RID: 47255
		// (set) Token: 0x0600B896 RID: 47254
		[DispId(-2147412026)]
		object onmouseleave
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

		// Token: 0x17003BD6 RID: 15318
		// (get) Token: 0x0600B899 RID: 47257
		// (set) Token: 0x0600B898 RID: 47256
		[DispId(-2147412025)]
		object onactivate
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

		// Token: 0x17003BD7 RID: 15319
		// (get) Token: 0x0600B89B RID: 47259
		// (set) Token: 0x0600B89A RID: 47258
		[DispId(-2147412024)]
		object ondeactivate
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

		// Token: 0x0600B89C RID: 47260
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool dragDrop();

		// Token: 0x17003BD8 RID: 15320
		// (get) Token: 0x0600B89D RID: 47261
		[DispId(-2147417004)]
		int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BD9 RID: 15321
		// (get) Token: 0x0600B89F RID: 47263
		// (set) Token: 0x0600B89E RID: 47262
		[DispId(-2147412036)]
		object onmousewheel
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

		// Token: 0x0600B8A0 RID: 47264
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void normalize();

		// Token: 0x0600B8A1 RID: 47265
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600B8A2 RID: 47266
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600B8A3 RID: 47267
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17003BDA RID: 15322
		// (get) Token: 0x0600B8A5 RID: 47269
		// (set) Token: 0x0600B8A4 RID: 47268
		[DispId(-2147412022)]
		object onbeforeactivate
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

		// Token: 0x17003BDB RID: 15323
		// (get) Token: 0x0600B8A7 RID: 47271
		// (set) Token: 0x0600B8A6 RID: 47270
		[DispId(-2147412021)]
		object onfocusin
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

		// Token: 0x17003BDC RID: 15324
		// (get) Token: 0x0600B8A9 RID: 47273
		// (set) Token: 0x0600B8A8 RID: 47272
		[DispId(-2147412020)]
		object onfocusout
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

		// Token: 0x17003BDD RID: 15325
		// (get) Token: 0x0600B8AA RID: 47274
		[DispId(-2147417058)]
		int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BDE RID: 15326
		// (get) Token: 0x0600B8AB RID: 47275
		[DispId(-2147417057)]
		string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003BDF RID: 15327
		// (get) Token: 0x0600B8AC RID: 47276
		[DispId(-2147417066)]
		int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003BE0 RID: 15328
		// (get) Token: 0x0600B8AD RID: 47277
		[DispId(-2147417065)]
		IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600B8AE RID: 47278
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool hasChildNodes();

		// Token: 0x17003BE1 RID: 15329
		// (get) Token: 0x0600B8AF RID: 47279
		[DispId(-2147417063)]
		object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003BE2 RID: 15330
		// (get) Token: 0x0600B8B0 RID: 47280
		[DispId(-2147417062)]
		object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600B8B1 RID: 47281
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600B8B2 RID: 47282
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600B8B3 RID: 47283
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600B8B4 RID: 47284
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600B8B5 RID: 47285
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600B8B6 RID: 47286
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600B8B7 RID: 47287
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600B8B8 RID: 47288
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17003BE3 RID: 15331
		// (get) Token: 0x0600B8B9 RID: 47289
		[DispId(-2147417038)]
		string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003BE4 RID: 15332
		// (get) Token: 0x0600B8BB RID: 47291
		// (set) Token: 0x0600B8BA RID: 47290
		[DispId(-2147417037)]
		object nodeValue
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

		// Token: 0x17003BE5 RID: 15333
		// (get) Token: 0x0600B8BC RID: 47292
		[DispId(-2147417036)]
		IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003BE6 RID: 15334
		// (get) Token: 0x0600B8BD RID: 47293
		[DispId(-2147417035)]
		IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003BE7 RID: 15335
		// (get) Token: 0x0600B8BE RID: 47294
		[DispId(-2147417034)]
		IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003BE8 RID: 15336
		// (get) Token: 0x0600B8BF RID: 47295
		[DispId(-2147417033)]
		IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003BE9 RID: 15337
		// (get) Token: 0x0600B8C0 RID: 47296
		[DispId(-2147416999)]
		object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003BEA RID: 15338
		// (get) Token: 0x0600B8C2 RID: 47298
		// (set) Token: 0x0600B8C1 RID: 47297
		[DispId(-2147413011)]
		string text
		{
			[DispId(-2147413011)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147413011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}
	}
}
