using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007E6 RID: 2022
	[ComSourceInterfaces("mshtml.HTMLDocumentEvents\0mshtml.HTMLDocumentEvents2\0\0")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("25336920-03F9-11CF-8FD0-00AA00686F13")]
	[ComImport]
	public class HTMLDocumentClass : DispHTMLDocument, HTMLDocument, HTMLDocumentEvents_Event, IHTMLDocument2, IHTMLDocument3, IHTMLDocument4, IHTMLDocument5, IHTMLDOMNode, IHTMLDOMNode2, HTMLDocumentEvents2_Event
	{
		// Token: 0x0600DB7B RID: 56187
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLDocumentClass();

		// Token: 0x17004935 RID: 18741
		// (get) Token: 0x0600DB7C RID: 56188
		[DispId(1001)]
		public virtual extern object Script
		{
			[DispId(1001)]
			[TypeLibFunc(1088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004936 RID: 18742
		// (get) Token: 0x0600DB7D RID: 56189
		[DispId(1003)]
		public virtual extern IHTMLElementCollection all
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004937 RID: 18743
		// (get) Token: 0x0600DB7E RID: 56190
		[DispId(1004)]
		public virtual extern IHTMLElement body
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004938 RID: 18744
		// (get) Token: 0x0600DB7F RID: 56191
		[DispId(1005)]
		public virtual extern IHTMLElement activeElement
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004939 RID: 18745
		// (get) Token: 0x0600DB80 RID: 56192
		[DispId(1011)]
		public virtual extern IHTMLElementCollection images
		{
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700493A RID: 18746
		// (get) Token: 0x0600DB81 RID: 56193
		[DispId(1008)]
		public virtual extern IHTMLElementCollection applets
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700493B RID: 18747
		// (get) Token: 0x0600DB82 RID: 56194
		[DispId(1009)]
		public virtual extern IHTMLElementCollection links
		{
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700493C RID: 18748
		// (get) Token: 0x0600DB83 RID: 56195
		[DispId(1010)]
		public virtual extern IHTMLElementCollection forms
		{
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700493D RID: 18749
		// (get) Token: 0x0600DB84 RID: 56196
		[DispId(1007)]
		public virtual extern IHTMLElementCollection anchors
		{
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700493E RID: 18750
		// (get) Token: 0x0600DB86 RID: 56198
		// (set) Token: 0x0600DB85 RID: 56197
		[DispId(1012)]
		public virtual extern string title
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

		// Token: 0x1700493F RID: 18751
		// (get) Token: 0x0600DB87 RID: 56199
		[DispId(1013)]
		public virtual extern IHTMLElementCollection scripts
		{
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004940 RID: 18752
		// (get) Token: 0x0600DB89 RID: 56201
		// (set) Token: 0x0600DB88 RID: 56200
		[DispId(1014)]
		public virtual extern string designMode
		{
			[TypeLibFunc(64)]
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004941 RID: 18753
		// (get) Token: 0x0600DB8A RID: 56202
		[DispId(1017)]
		public virtual extern IHTMLSelectionObject selection
		{
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004942 RID: 18754
		// (get) Token: 0x0600DB8B RID: 56203
		[DispId(1018)]
		public virtual extern string readyState
		{
			[TypeLibFunc(4)]
			[DispId(1018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004943 RID: 18755
		// (get) Token: 0x0600DB8C RID: 56204
		[DispId(1019)]
		public virtual extern FramesCollection frames
		{
			[DispId(1019)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004944 RID: 18756
		// (get) Token: 0x0600DB8D RID: 56205
		[DispId(1015)]
		public virtual extern IHTMLElementCollection embeds
		{
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004945 RID: 18757
		// (get) Token: 0x0600DB8E RID: 56206
		[DispId(1021)]
		public virtual extern IHTMLElementCollection plugins
		{
			[DispId(1021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004946 RID: 18758
		// (get) Token: 0x0600DB90 RID: 56208
		// (set) Token: 0x0600DB8F RID: 56207
		[DispId(1022)]
		public virtual extern object alinkColor
		{
			[DispId(1022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004947 RID: 18759
		// (get) Token: 0x0600DB92 RID: 56210
		// (set) Token: 0x0600DB91 RID: 56209
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

		// Token: 0x17004948 RID: 18760
		// (get) Token: 0x0600DB94 RID: 56212
		// (set) Token: 0x0600DB93 RID: 56211
		[DispId(-2147413110)]
		public virtual extern object fgColor
		{
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004949 RID: 18761
		// (get) Token: 0x0600DB96 RID: 56214
		// (set) Token: 0x0600DB95 RID: 56213
		[DispId(1024)]
		public virtual extern object linkColor
		{
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700494A RID: 18762
		// (get) Token: 0x0600DB98 RID: 56216
		// (set) Token: 0x0600DB97 RID: 56215
		[DispId(1023)]
		public virtual extern object vlinkColor
		{
			[DispId(1023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700494B RID: 18763
		// (get) Token: 0x0600DB99 RID: 56217
		[DispId(1027)]
		public virtual extern string referrer
		{
			[DispId(1027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700494C RID: 18764
		// (get) Token: 0x0600DB9A RID: 56218
		[DispId(1026)]
		public virtual extern HTMLLocation location
		{
			[DispId(1026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700494D RID: 18765
		// (get) Token: 0x0600DB9B RID: 56219
		[DispId(1028)]
		public virtual extern string lastModified
		{
			[DispId(1028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700494E RID: 18766
		// (get) Token: 0x0600DB9D RID: 56221
		// (set) Token: 0x0600DB9C RID: 56220
		[DispId(1025)]
		public virtual extern string url
		{
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700494F RID: 18767
		// (get) Token: 0x0600DB9F RID: 56223
		// (set) Token: 0x0600DB9E RID: 56222
		[DispId(1029)]
		public virtual extern string domain
		{
			[DispId(1029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004950 RID: 18768
		// (get) Token: 0x0600DBA1 RID: 56225
		// (set) Token: 0x0600DBA0 RID: 56224
		[DispId(1030)]
		public virtual extern string cookie
		{
			[DispId(1030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004951 RID: 18769
		// (get) Token: 0x0600DBA3 RID: 56227
		// (set) Token: 0x0600DBA2 RID: 56226
		[DispId(1031)]
		public virtual extern bool expando
		{
			[DispId(1031)]
			[TypeLibFunc(68)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1031)]
			[TypeLibFunc(68)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004952 RID: 18770
		// (get) Token: 0x0600DBA5 RID: 56229
		// (set) Token: 0x0600DBA4 RID: 56228
		[DispId(1032)]
		public virtual extern string charset
		{
			[TypeLibFunc(64)]
			[DispId(1032)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1032)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004953 RID: 18771
		// (get) Token: 0x0600DBA7 RID: 56231
		// (set) Token: 0x0600DBA6 RID: 56230
		[DispId(1033)]
		public virtual extern string defaultCharset
		{
			[DispId(1033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004954 RID: 18772
		// (get) Token: 0x0600DBA8 RID: 56232
		[DispId(1041)]
		public virtual extern string mimeType
		{
			[DispId(1041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004955 RID: 18773
		// (get) Token: 0x0600DBA9 RID: 56233
		[DispId(1042)]
		public virtual extern string fileSize
		{
			[DispId(1042)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004956 RID: 18774
		// (get) Token: 0x0600DBAA RID: 56234
		[DispId(1043)]
		public virtual extern string fileCreatedDate
		{
			[DispId(1043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004957 RID: 18775
		// (get) Token: 0x0600DBAB RID: 56235
		[DispId(1044)]
		public virtual extern string fileModifiedDate
		{
			[DispId(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004958 RID: 18776
		// (get) Token: 0x0600DBAC RID: 56236
		[DispId(1045)]
		public virtual extern string fileUpdatedDate
		{
			[DispId(1045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004959 RID: 18777
		// (get) Token: 0x0600DBAD RID: 56237
		[DispId(1046)]
		public virtual extern string security
		{
			[DispId(1046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700495A RID: 18778
		// (get) Token: 0x0600DBAE RID: 56238
		[DispId(1047)]
		public virtual extern string protocol
		{
			[DispId(1047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700495B RID: 18779
		// (get) Token: 0x0600DBAF RID: 56239
		[DispId(1048)]
		public virtual extern string nameProp
		{
			[DispId(1048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600DBB0 RID: 56240
		[DispId(1054)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void write([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x0600DBB1 RID: 56241
		[DispId(1055)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void writeln([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x0600DBB2 RID: 56242
		[DispId(1056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object open([MarshalAs(19)] [In] string url = "text/html", [MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object features, [MarshalAs(27)] [In] [Optional] object replace);

		// Token: 0x0600DBB3 RID: 56243
		[DispId(1057)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void close();

		// Token: 0x0600DBB4 RID: 56244
		[DispId(1058)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clear();

		// Token: 0x0600DBB5 RID: 56245
		[DispId(1059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool queryCommandSupported([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DBB6 RID: 56246
		[DispId(1060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool queryCommandEnabled([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DBB7 RID: 56247
		[DispId(1061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool queryCommandState([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DBB8 RID: 56248
		[DispId(1062)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool queryCommandIndeterm([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DBB9 RID: 56249
		[DispId(1063)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string queryCommandText([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DBBA RID: 56250
		[DispId(1064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object queryCommandValue([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DBBB RID: 56251
		[DispId(1065)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool execCommand([MarshalAs(19)] [In] string cmdID, [In] bool showUI = false, [MarshalAs(27)] [In] [Optional] object value);

		// Token: 0x0600DBBC RID: 56252
		[DispId(1066)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool execCommandShowHelp([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DBBD RID: 56253
		[DispId(1067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement createElement([MarshalAs(19)] [In] string eTag);

		// Token: 0x1700495C RID: 18780
		// (get) Token: 0x0600DBBF RID: 56255
		// (set) Token: 0x0600DBBE RID: 56254
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

		// Token: 0x1700495D RID: 18781
		// (get) Token: 0x0600DBC1 RID: 56257
		// (set) Token: 0x0600DBC0 RID: 56256
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

		// Token: 0x1700495E RID: 18782
		// (get) Token: 0x0600DBC3 RID: 56259
		// (set) Token: 0x0600DBC2 RID: 56258
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

		// Token: 0x1700495F RID: 18783
		// (get) Token: 0x0600DBC5 RID: 56261
		// (set) Token: 0x0600DBC4 RID: 56260
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

		// Token: 0x17004960 RID: 18784
		// (get) Token: 0x0600DBC7 RID: 56263
		// (set) Token: 0x0600DBC6 RID: 56262
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

		// Token: 0x17004961 RID: 18785
		// (get) Token: 0x0600DBC9 RID: 56265
		// (set) Token: 0x0600DBC8 RID: 56264
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

		// Token: 0x17004962 RID: 18786
		// (get) Token: 0x0600DBCB RID: 56267
		// (set) Token: 0x0600DBCA RID: 56266
		[DispId(-2147412109)]
		public virtual extern object onmouseup
		{
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004963 RID: 18787
		// (get) Token: 0x0600DBCD RID: 56269
		// (set) Token: 0x0600DBCC RID: 56268
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

		// Token: 0x17004964 RID: 18788
		// (get) Token: 0x0600DBCF RID: 56271
		// (set) Token: 0x0600DBCE RID: 56270
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

		// Token: 0x17004965 RID: 18789
		// (get) Token: 0x0600DBD1 RID: 56273
		// (set) Token: 0x0600DBD0 RID: 56272
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004966 RID: 18790
		// (get) Token: 0x0600DBD3 RID: 56275
		// (set) Token: 0x0600DBD2 RID: 56274
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

		// Token: 0x17004967 RID: 18791
		// (get) Token: 0x0600DBD5 RID: 56277
		// (set) Token: 0x0600DBD4 RID: 56276
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

		// Token: 0x17004968 RID: 18792
		// (get) Token: 0x0600DBD7 RID: 56279
		// (set) Token: 0x0600DBD6 RID: 56278
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

		// Token: 0x17004969 RID: 18793
		// (get) Token: 0x0600DBD9 RID: 56281
		// (set) Token: 0x0600DBD8 RID: 56280
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

		// Token: 0x1700496A RID: 18794
		// (get) Token: 0x0600DBDB RID: 56283
		// (set) Token: 0x0600DBDA RID: 56282
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

		// Token: 0x1700496B RID: 18795
		// (get) Token: 0x0600DBDD RID: 56285
		// (set) Token: 0x0600DBDC RID: 56284
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

		// Token: 0x1700496C RID: 18796
		// (get) Token: 0x0600DBDF RID: 56287
		// (set) Token: 0x0600DBDE RID: 56286
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

		// Token: 0x0600DBE0 RID: 56288
		[DispId(1068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement elementFromPoint([In] int x, [In] int y);

		// Token: 0x1700496D RID: 18797
		// (get) Token: 0x0600DBE1 RID: 56289
		[DispId(1034)]
		public virtual extern IHTMLWindow2 parentWindow
		{
			[DispId(1034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700496E RID: 18798
		// (get) Token: 0x0600DBE2 RID: 56290
		[DispId(1069)]
		public virtual extern HTMLStyleSheetsCollection styleSheets
		{
			[DispId(1069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700496F RID: 18799
		// (get) Token: 0x0600DBE4 RID: 56292
		// (set) Token: 0x0600DBE3 RID: 56291
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

		// Token: 0x17004970 RID: 18800
		// (get) Token: 0x0600DBE6 RID: 56294
		// (set) Token: 0x0600DBE5 RID: 56293
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

		// Token: 0x0600DBE7 RID: 56295
		[DispId(1070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x0600DBE8 RID: 56296
		[DispId(1071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLStyleSheet createStyleSheet([MarshalAs(19)] [In] string bstrHref = "", [In] int lIndex = -1);

		// Token: 0x0600DBE9 RID: 56297
		[DispId(1072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x0600DBEA RID: 56298
		[DispId(1073)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void recalc([In] bool fForce = false);

		// Token: 0x0600DBEB RID: 56299
		[DispId(1074)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode createTextNode([MarshalAs(19)] [In] string text);

		// Token: 0x17004971 RID: 18801
		// (get) Token: 0x0600DBEC RID: 56300
		[DispId(1075)]
		public virtual extern IHTMLElement documentElement
		{
			[DispId(1075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004972 RID: 18802
		// (get) Token: 0x0600DBED RID: 56301
		[DispId(1077)]
		public virtual extern string uniqueID
		{
			[DispId(1077)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600DBEE RID: 56302
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600DBEF RID: 56303
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17004973 RID: 18803
		// (get) Token: 0x0600DBF1 RID: 56305
		// (set) Token: 0x0600DBF0 RID: 56304
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

		// Token: 0x17004974 RID: 18804
		// (get) Token: 0x0600DBF3 RID: 56307
		// (set) Token: 0x0600DBF2 RID: 56306
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

		// Token: 0x17004975 RID: 18805
		// (get) Token: 0x0600DBF5 RID: 56309
		// (set) Token: 0x0600DBF4 RID: 56308
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

		// Token: 0x17004976 RID: 18806
		// (get) Token: 0x0600DBF7 RID: 56311
		// (set) Token: 0x0600DBF6 RID: 56310
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

		// Token: 0x17004977 RID: 18807
		// (get) Token: 0x0600DBF9 RID: 56313
		// (set) Token: 0x0600DBF8 RID: 56312
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

		// Token: 0x17004978 RID: 18808
		// (get) Token: 0x0600DBFB RID: 56315
		// (set) Token: 0x0600DBFA RID: 56314
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

		// Token: 0x17004979 RID: 18809
		// (get) Token: 0x0600DBFD RID: 56317
		// (set) Token: 0x0600DBFC RID: 56316
		[DispId(-2147412065)]
		public virtual extern object onpropertychange
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

		// Token: 0x1700497A RID: 18810
		// (get) Token: 0x0600DBFF RID: 56319
		// (set) Token: 0x0600DBFE RID: 56318
		[DispId(-2147412995)]
		public virtual extern string dir
		{
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700497B RID: 18811
		// (get) Token: 0x0600DC01 RID: 56321
		// (set) Token: 0x0600DC00 RID: 56320
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

		// Token: 0x1700497C RID: 18812
		// (get) Token: 0x0600DC03 RID: 56323
		// (set) Token: 0x0600DC02 RID: 56322
		[DispId(-2147412044)]
		public virtual extern object onstop
		{
			[TypeLibFunc(20)]
			[DispId(-2147412044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412044)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600DC04 RID: 56324
		[DispId(1076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDocument2 createDocumentFragment();

		// Token: 0x1700497D RID: 18813
		// (get) Token: 0x0600DC05 RID: 56325
		[DispId(1078)]
		public virtual extern IHTMLDocument2 parentDocument
		{
			[DispId(1078)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700497E RID: 18814
		// (get) Token: 0x0600DC07 RID: 56327
		// (set) Token: 0x0600DC06 RID: 56326
		[DispId(1079)]
		public virtual extern bool enableDownload
		{
			[TypeLibFunc(65)]
			[DispId(1079)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1079)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700497F RID: 18815
		// (get) Token: 0x0600DC09 RID: 56329
		// (set) Token: 0x0600DC08 RID: 56328
		[DispId(1080)]
		public virtual extern string baseUrl
		{
			[DispId(1080)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(65)]
			[DispId(1080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004980 RID: 18816
		// (get) Token: 0x0600DC0B RID: 56331
		// (set) Token: 0x0600DC0A RID: 56330
		[DispId(1082)]
		public virtual extern bool inheritStyleSheets
		{
			[TypeLibFunc(65)]
			[DispId(1082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(65)]
			[DispId(1082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004981 RID: 18817
		// (get) Token: 0x0600DC0D RID: 56333
		// (set) Token: 0x0600DC0C RID: 56332
		[DispId(-2147412043)]
		public virtual extern object onbeforeeditfocus
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

		// Token: 0x0600DC0E RID: 56334
		[DispId(1086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByName([MarshalAs(19)] [In] string v);

		// Token: 0x0600DC0F RID: 56335
		[DispId(1088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement getElementById([MarshalAs(19)] [In] string v);

		// Token: 0x0600DC10 RID: 56336
		[DispId(1087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600DC11 RID: 56337
		[DispId(1089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x0600DC12 RID: 56338
		[DispId(1090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasFocus();

		// Token: 0x17004982 RID: 18818
		// (get) Token: 0x0600DC14 RID: 56340
		// (set) Token: 0x0600DC13 RID: 56339
		[DispId(-2147412032)]
		public virtual extern object onselectionchange
		{
			[DispId(-2147412032)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412032)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004983 RID: 18819
		// (get) Token: 0x0600DC15 RID: 56341
		[DispId(1091)]
		public virtual extern object namespaces
		{
			[DispId(1091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600DC16 RID: 56342
		[DispId(1092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDocument2 createDocumentFromUrl([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(19)] [In] string bstrOptions);

		// Token: 0x17004984 RID: 18820
		// (get) Token: 0x0600DC18 RID: 56344
		// (set) Token: 0x0600DC17 RID: 56343
		[DispId(1093)]
		public virtual extern string media
		{
			[DispId(1093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600DC19 RID: 56345
		[DispId(1094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLEventObj CreateEventObject([MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x0600DC1A RID: 56346
		[DispId(1095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x0600DC1B RID: 56347
		[DispId(1096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRenderStyle createRenderStyle([MarshalAs(19)] [In] string v);

		// Token: 0x17004985 RID: 18821
		// (get) Token: 0x0600DC1D RID: 56349
		// (set) Token: 0x0600DC1C RID: 56348
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

		// Token: 0x17004986 RID: 18822
		// (get) Token: 0x0600DC1E RID: 56350
		[DispId(1097)]
		public virtual extern string URLUnencoded
		{
			[DispId(1097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004987 RID: 18823
		// (get) Token: 0x0600DC20 RID: 56352
		// (set) Token: 0x0600DC1F RID: 56351
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

		// Token: 0x17004988 RID: 18824
		// (get) Token: 0x0600DC21 RID: 56353
		[DispId(1098)]
		public virtual extern IHTMLDOMNode doctype
		{
			[DispId(1098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004989 RID: 18825
		// (get) Token: 0x0600DC22 RID: 56354
		[DispId(1099)]
		public virtual extern IHTMLDOMImplementation implementation
		{
			[DispId(1099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600DC23 RID: 56355
		[DispId(1100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute createAttribute([MarshalAs(19)] [In] string bstrattrName);

		// Token: 0x0600DC24 RID: 56356
		[DispId(1101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode createComment([MarshalAs(19)] [In] string bstrdata);

		// Token: 0x1700498A RID: 18826
		// (get) Token: 0x0600DC26 RID: 56358
		// (set) Token: 0x0600DC25 RID: 56357
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

		// Token: 0x1700498B RID: 18827
		// (get) Token: 0x0600DC28 RID: 56360
		// (set) Token: 0x0600DC27 RID: 56359
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

		// Token: 0x1700498C RID: 18828
		// (get) Token: 0x0600DC2A RID: 56362
		// (set) Token: 0x0600DC29 RID: 56361
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

		// Token: 0x1700498D RID: 18829
		// (get) Token: 0x0600DC2C RID: 56364
		// (set) Token: 0x0600DC2B RID: 56363
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

		// Token: 0x1700498E RID: 18830
		// (get) Token: 0x0600DC2E RID: 56366
		// (set) Token: 0x0600DC2D RID: 56365
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

		// Token: 0x1700498F RID: 18831
		// (get) Token: 0x0600DC30 RID: 56368
		// (set) Token: 0x0600DC2F RID: 56367
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

		// Token: 0x17004990 RID: 18832
		// (get) Token: 0x0600DC31 RID: 56369
		[DispId(1102)]
		public virtual extern string compatMode
		{
			[DispId(1102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004991 RID: 18833
		// (get) Token: 0x0600DC32 RID: 56370
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004992 RID: 18834
		// (get) Token: 0x0600DC33 RID: 56371
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600DC34 RID: 56372
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17004993 RID: 18835
		// (get) Token: 0x0600DC35 RID: 56373
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004994 RID: 18836
		// (get) Token: 0x0600DC36 RID: 56374
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600DC37 RID: 56375
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600DC38 RID: 56376
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600DC39 RID: 56377
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600DC3A RID: 56378
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600DC3B RID: 56379
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600DC3C RID: 56380
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600DC3D RID: 56381
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600DC3E RID: 56382
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004995 RID: 18837
		// (get) Token: 0x0600DC3F RID: 56383
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004996 RID: 18838
		// (get) Token: 0x0600DC41 RID: 56385
		// (set) Token: 0x0600DC40 RID: 56384
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

		// Token: 0x17004997 RID: 18839
		// (get) Token: 0x0600DC42 RID: 56386
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004998 RID: 18840
		// (get) Token: 0x0600DC43 RID: 56387
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004999 RID: 18841
		// (get) Token: 0x0600DC44 RID: 56388
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700499A RID: 18842
		// (get) Token: 0x0600DC45 RID: 56389
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700499B RID: 18843
		// (get) Token: 0x0600DC46 RID: 56390
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700499C RID: 18844
		// (get) Token: 0x0600DC47 RID: 56391
		public virtual extern object IHTMLDocument2_Script
		{
			[TypeLibFunc(1088)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700499D RID: 18845
		// (get) Token: 0x0600DC48 RID: 56392
		public virtual extern IHTMLElementCollection IHTMLDocument2_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700499E RID: 18846
		// (get) Token: 0x0600DC49 RID: 56393
		public virtual extern IHTMLElement IHTMLDocument2_body
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700499F RID: 18847
		// (get) Token: 0x0600DC4A RID: 56394
		public virtual extern IHTMLElement IHTMLDocument2_activeElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049A0 RID: 18848
		// (get) Token: 0x0600DC4B RID: 56395
		public virtual extern IHTMLElementCollection IHTMLDocument2_images
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049A1 RID: 18849
		// (get) Token: 0x0600DC4C RID: 56396
		public virtual extern IHTMLElementCollection IHTMLDocument2_applets
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049A2 RID: 18850
		// (get) Token: 0x0600DC4D RID: 56397
		public virtual extern IHTMLElementCollection IHTMLDocument2_links
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049A3 RID: 18851
		// (get) Token: 0x0600DC4E RID: 56398
		public virtual extern IHTMLElementCollection IHTMLDocument2_forms
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049A4 RID: 18852
		// (get) Token: 0x0600DC4F RID: 56399
		public virtual extern IHTMLElementCollection IHTMLDocument2_anchors
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049A5 RID: 18853
		// (get) Token: 0x0600DC51 RID: 56401
		// (set) Token: 0x0600DC50 RID: 56400
		public virtual extern string IHTMLDocument2_title
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170049A6 RID: 18854
		// (get) Token: 0x0600DC52 RID: 56402
		public virtual extern IHTMLElementCollection IHTMLDocument2_scripts
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049A7 RID: 18855
		// (get) Token: 0x0600DC54 RID: 56404
		// (set) Token: 0x0600DC53 RID: 56403
		public virtual extern string IHTMLDocument2_designMode
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170049A8 RID: 18856
		// (get) Token: 0x0600DC55 RID: 56405
		public virtual extern IHTMLSelectionObject IHTMLDocument2_selection
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049A9 RID: 18857
		// (get) Token: 0x0600DC56 RID: 56406
		public virtual extern string IHTMLDocument2_readyState
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049AA RID: 18858
		// (get) Token: 0x0600DC57 RID: 56407
		public virtual extern FramesCollection IHTMLDocument2_frames
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049AB RID: 18859
		// (get) Token: 0x0600DC58 RID: 56408
		public virtual extern IHTMLElementCollection IHTMLDocument2_embeds
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049AC RID: 18860
		// (get) Token: 0x0600DC59 RID: 56409
		public virtual extern IHTMLElementCollection IHTMLDocument2_plugins
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049AD RID: 18861
		// (get) Token: 0x0600DC5B RID: 56411
		// (set) Token: 0x0600DC5A RID: 56410
		public virtual extern object IHTMLDocument2_alinkColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049AE RID: 18862
		// (get) Token: 0x0600DC5D RID: 56413
		// (set) Token: 0x0600DC5C RID: 56412
		public virtual extern object IHTMLDocument2_bgColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049AF RID: 18863
		// (get) Token: 0x0600DC5F RID: 56415
		// (set) Token: 0x0600DC5E RID: 56414
		public virtual extern object IHTMLDocument2_fgColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049B0 RID: 18864
		// (get) Token: 0x0600DC61 RID: 56417
		// (set) Token: 0x0600DC60 RID: 56416
		public virtual extern object IHTMLDocument2_linkColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049B1 RID: 18865
		// (get) Token: 0x0600DC63 RID: 56419
		// (set) Token: 0x0600DC62 RID: 56418
		public virtual extern object IHTMLDocument2_vlinkColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049B2 RID: 18866
		// (get) Token: 0x0600DC64 RID: 56420
		public virtual extern string IHTMLDocument2_referrer
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049B3 RID: 18867
		// (get) Token: 0x0600DC65 RID: 56421
		public virtual extern HTMLLocation IHTMLDocument2_location
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049B4 RID: 18868
		// (get) Token: 0x0600DC66 RID: 56422
		public virtual extern string IHTMLDocument2_lastModified
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049B5 RID: 18869
		// (get) Token: 0x0600DC68 RID: 56424
		// (set) Token: 0x0600DC67 RID: 56423
		public virtual extern string IHTMLDocument2_url
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170049B6 RID: 18870
		// (get) Token: 0x0600DC6A RID: 56426
		// (set) Token: 0x0600DC69 RID: 56425
		public virtual extern string IHTMLDocument2_domain
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170049B7 RID: 18871
		// (get) Token: 0x0600DC6C RID: 56428
		// (set) Token: 0x0600DC6B RID: 56427
		public virtual extern string IHTMLDocument2_cookie
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170049B8 RID: 18872
		// (get) Token: 0x0600DC6E RID: 56430
		// (set) Token: 0x0600DC6D RID: 56429
		public virtual extern bool IHTMLDocument2_expando
		{
			[TypeLibFunc(68)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(68)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170049B9 RID: 18873
		// (get) Token: 0x0600DC70 RID: 56432
		// (set) Token: 0x0600DC6F RID: 56431
		public virtual extern string IHTMLDocument2_charset
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170049BA RID: 18874
		// (get) Token: 0x0600DC72 RID: 56434
		// (set) Token: 0x0600DC71 RID: 56433
		public virtual extern string IHTMLDocument2_defaultCharset
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170049BB RID: 18875
		// (get) Token: 0x0600DC73 RID: 56435
		public virtual extern string IHTMLDocument2_mimeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049BC RID: 18876
		// (get) Token: 0x0600DC74 RID: 56436
		public virtual extern string IHTMLDocument2_fileSize
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049BD RID: 18877
		// (get) Token: 0x0600DC75 RID: 56437
		public virtual extern string IHTMLDocument2_fileCreatedDate
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049BE RID: 18878
		// (get) Token: 0x0600DC76 RID: 56438
		public virtual extern string IHTMLDocument2_fileModifiedDate
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049BF RID: 18879
		// (get) Token: 0x0600DC77 RID: 56439
		public virtual extern string IHTMLDocument2_fileUpdatedDate
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049C0 RID: 18880
		// (get) Token: 0x0600DC78 RID: 56440
		public virtual extern string IHTMLDocument2_security
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049C1 RID: 18881
		// (get) Token: 0x0600DC79 RID: 56441
		public virtual extern string IHTMLDocument2_protocol
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049C2 RID: 18882
		// (get) Token: 0x0600DC7A RID: 56442
		public virtual extern string IHTMLDocument2_nameProp
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600DC7B RID: 56443
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument2_write([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x0600DC7C RID: 56444
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument2_writeln([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x0600DC7D RID: 56445
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLDocument2_open([MarshalAs(19)] [In] string url = "text/html", [MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object features, [MarshalAs(27)] [In] [Optional] object replace);

		// Token: 0x0600DC7E RID: 56446
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument2_close();

		// Token: 0x0600DC7F RID: 56447
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument2_clear();

		// Token: 0x0600DC80 RID: 56448
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_queryCommandSupported([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DC81 RID: 56449
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_queryCommandEnabled([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DC82 RID: 56450
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_queryCommandState([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DC83 RID: 56451
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_queryCommandIndeterm([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DC84 RID: 56452
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLDocument2_queryCommandText([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DC85 RID: 56453
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLDocument2_queryCommandValue([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DC86 RID: 56454
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_execCommand([MarshalAs(19)] [In] string cmdID, [In] bool showUI = false, [MarshalAs(27)] [In] [Optional] object value);

		// Token: 0x0600DC87 RID: 56455
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_execCommandShowHelp([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600DC88 RID: 56456
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLDocument2_createElement([MarshalAs(19)] [In] string eTag);

		// Token: 0x170049C3 RID: 18883
		// (get) Token: 0x0600DC8A RID: 56458
		// (set) Token: 0x0600DC89 RID: 56457
		public virtual extern object IHTMLDocument2_onhelp
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049C4 RID: 18884
		// (get) Token: 0x0600DC8C RID: 56460
		// (set) Token: 0x0600DC8B RID: 56459
		public virtual extern object IHTMLDocument2_onclick
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049C5 RID: 18885
		// (get) Token: 0x0600DC8E RID: 56462
		// (set) Token: 0x0600DC8D RID: 56461
		public virtual extern object IHTMLDocument2_ondblclick
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049C6 RID: 18886
		// (get) Token: 0x0600DC90 RID: 56464
		// (set) Token: 0x0600DC8F RID: 56463
		public virtual extern object IHTMLDocument2_onkeyup
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049C7 RID: 18887
		// (get) Token: 0x0600DC92 RID: 56466
		// (set) Token: 0x0600DC91 RID: 56465
		public virtual extern object IHTMLDocument2_onkeydown
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049C8 RID: 18888
		// (get) Token: 0x0600DC94 RID: 56468
		// (set) Token: 0x0600DC93 RID: 56467
		public virtual extern object IHTMLDocument2_onkeypress
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049C9 RID: 18889
		// (get) Token: 0x0600DC96 RID: 56470
		// (set) Token: 0x0600DC95 RID: 56469
		public virtual extern object IHTMLDocument2_onmouseup
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049CA RID: 18890
		// (get) Token: 0x0600DC98 RID: 56472
		// (set) Token: 0x0600DC97 RID: 56471
		public virtual extern object IHTMLDocument2_onmousedown
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049CB RID: 18891
		// (get) Token: 0x0600DC9A RID: 56474
		// (set) Token: 0x0600DC99 RID: 56473
		public virtual extern object IHTMLDocument2_onmousemove
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049CC RID: 18892
		// (get) Token: 0x0600DC9C RID: 56476
		// (set) Token: 0x0600DC9B RID: 56475
		public virtual extern object IHTMLDocument2_onmouseout
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049CD RID: 18893
		// (get) Token: 0x0600DC9E RID: 56478
		// (set) Token: 0x0600DC9D RID: 56477
		public virtual extern object IHTMLDocument2_onmouseover
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049CE RID: 18894
		// (get) Token: 0x0600DCA0 RID: 56480
		// (set) Token: 0x0600DC9F RID: 56479
		public virtual extern object IHTMLDocument2_onreadystatechange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049CF RID: 18895
		// (get) Token: 0x0600DCA2 RID: 56482
		// (set) Token: 0x0600DCA1 RID: 56481
		public virtual extern object IHTMLDocument2_onafterupdate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049D0 RID: 18896
		// (get) Token: 0x0600DCA4 RID: 56484
		// (set) Token: 0x0600DCA3 RID: 56483
		public virtual extern object IHTMLDocument2_onrowexit
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049D1 RID: 18897
		// (get) Token: 0x0600DCA6 RID: 56486
		// (set) Token: 0x0600DCA5 RID: 56485
		public virtual extern object IHTMLDocument2_onrowenter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049D2 RID: 18898
		// (get) Token: 0x0600DCA8 RID: 56488
		// (set) Token: 0x0600DCA7 RID: 56487
		public virtual extern object IHTMLDocument2_ondragstart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049D3 RID: 18899
		// (get) Token: 0x0600DCAA RID: 56490
		// (set) Token: 0x0600DCA9 RID: 56489
		public virtual extern object IHTMLDocument2_onselectstart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600DCAB RID: 56491
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLDocument2_elementFromPoint([In] int x, [In] int y);

		// Token: 0x170049D4 RID: 18900
		// (get) Token: 0x0600DCAC RID: 56492
		public virtual extern IHTMLWindow2 IHTMLDocument2_parentWindow
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049D5 RID: 18901
		// (get) Token: 0x0600DCAD RID: 56493
		public virtual extern HTMLStyleSheetsCollection IHTMLDocument2_styleSheets
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049D6 RID: 18902
		// (get) Token: 0x0600DCAF RID: 56495
		// (set) Token: 0x0600DCAE RID: 56494
		public virtual extern object IHTMLDocument2_onbeforeupdate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049D7 RID: 18903
		// (get) Token: 0x0600DCB1 RID: 56497
		// (set) Token: 0x0600DCB0 RID: 56496
		public virtual extern object IHTMLDocument2_onerrorupdate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600DCB2 RID: 56498
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLDocument2_toString();

		// Token: 0x0600DCB3 RID: 56499
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLStyleSheet IHTMLDocument2_createStyleSheet([MarshalAs(19)] [In] string bstrHref = "", [In] int lIndex = -1);

		// Token: 0x0600DCB4 RID: 56500
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument3_releaseCapture();

		// Token: 0x0600DCB5 RID: 56501
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument3_recalc([In] bool fForce = false);

		// Token: 0x0600DCB6 RID: 56502
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDocument3_createTextNode([MarshalAs(19)] [In] string text);

		// Token: 0x170049D8 RID: 18904
		// (get) Token: 0x0600DCB7 RID: 56503
		public virtual extern IHTMLElement IHTMLDocument3_documentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049D9 RID: 18905
		// (get) Token: 0x0600DCB8 RID: 56504
		public virtual extern string IHTMLDocument3_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600DCB9 RID: 56505
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument3_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600DCBA RID: 56506
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument3_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170049DA RID: 18906
		// (get) Token: 0x0600DCBC RID: 56508
		// (set) Token: 0x0600DCBB RID: 56507
		public virtual extern object IHTMLDocument3_onrowsdelete
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049DB RID: 18907
		// (get) Token: 0x0600DCBE RID: 56510
		// (set) Token: 0x0600DCBD RID: 56509
		public virtual extern object IHTMLDocument3_onrowsinserted
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049DC RID: 18908
		// (get) Token: 0x0600DCC0 RID: 56512
		// (set) Token: 0x0600DCBF RID: 56511
		public virtual extern object IHTMLDocument3_oncellchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049DD RID: 18909
		// (get) Token: 0x0600DCC2 RID: 56514
		// (set) Token: 0x0600DCC1 RID: 56513
		public virtual extern object IHTMLDocument3_ondatasetchanged
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049DE RID: 18910
		// (get) Token: 0x0600DCC4 RID: 56516
		// (set) Token: 0x0600DCC3 RID: 56515
		public virtual extern object IHTMLDocument3_ondataavailable
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049DF RID: 18911
		// (get) Token: 0x0600DCC6 RID: 56518
		// (set) Token: 0x0600DCC5 RID: 56517
		public virtual extern object IHTMLDocument3_ondatasetcomplete
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049E0 RID: 18912
		// (get) Token: 0x0600DCC8 RID: 56520
		// (set) Token: 0x0600DCC7 RID: 56519
		public virtual extern object IHTMLDocument3_onpropertychange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049E1 RID: 18913
		// (get) Token: 0x0600DCCA RID: 56522
		// (set) Token: 0x0600DCC9 RID: 56521
		public virtual extern string IHTMLDocument3_dir
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170049E2 RID: 18914
		// (get) Token: 0x0600DCCC RID: 56524
		// (set) Token: 0x0600DCCB RID: 56523
		public virtual extern object IHTMLDocument3_oncontextmenu
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049E3 RID: 18915
		// (get) Token: 0x0600DCCE RID: 56526
		// (set) Token: 0x0600DCCD RID: 56525
		public virtual extern object IHTMLDocument3_onstop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600DCCF RID: 56527
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDocument2 IHTMLDocument3_createDocumentFragment();

		// Token: 0x170049E4 RID: 18916
		// (get) Token: 0x0600DCD0 RID: 56528
		public virtual extern IHTMLDocument2 IHTMLDocument3_parentDocument
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049E5 RID: 18917
		// (get) Token: 0x0600DCD2 RID: 56530
		// (set) Token: 0x0600DCD1 RID: 56529
		public virtual extern bool IHTMLDocument3_enableDownload
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170049E6 RID: 18918
		// (get) Token: 0x0600DCD4 RID: 56532
		// (set) Token: 0x0600DCD3 RID: 56531
		public virtual extern string IHTMLDocument3_baseUrl
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170049E7 RID: 18919
		// (get) Token: 0x0600DCD5 RID: 56533
		public virtual extern object IHTMLDocument3_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170049E8 RID: 18920
		// (get) Token: 0x0600DCD7 RID: 56535
		// (set) Token: 0x0600DCD6 RID: 56534
		public virtual extern bool IHTMLDocument3_inheritStyleSheets
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170049E9 RID: 18921
		// (get) Token: 0x0600DCD9 RID: 56537
		// (set) Token: 0x0600DCD8 RID: 56536
		public virtual extern object IHTMLDocument3_onbeforeeditfocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600DCDA RID: 56538
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLDocument3_getElementsByName([MarshalAs(19)] [In] string v);

		// Token: 0x0600DCDB RID: 56539
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLDocument3_getElementById([MarshalAs(19)] [In] string v);

		// Token: 0x0600DCDC RID: 56540
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLDocument3_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600DCDD RID: 56541
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument4_focus();

		// Token: 0x0600DCDE RID: 56542
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument4_hasFocus();

		// Token: 0x170049EA RID: 18922
		// (get) Token: 0x0600DCE0 RID: 56544
		// (set) Token: 0x0600DCDF RID: 56543
		public virtual extern object IHTMLDocument4_onselectionchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049EB RID: 18923
		// (get) Token: 0x0600DCE1 RID: 56545
		public virtual extern object IHTMLDocument4_namespaces
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600DCE2 RID: 56546
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDocument2 IHTMLDocument4_createDocumentFromUrl([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(19)] [In] string bstrOptions);

		// Token: 0x170049EC RID: 18924
		// (get) Token: 0x0600DCE4 RID: 56548
		// (set) Token: 0x0600DCE3 RID: 56547
		public virtual extern string IHTMLDocument4_media
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600DCE5 RID: 56549
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLEventObj IHTMLDocument4_CreateEventObject([MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x0600DCE6 RID: 56550
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument4_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x0600DCE7 RID: 56551
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRenderStyle IHTMLDocument4_createRenderStyle([MarshalAs(19)] [In] string v);

		// Token: 0x170049ED RID: 18925
		// (get) Token: 0x0600DCE9 RID: 56553
		// (set) Token: 0x0600DCE8 RID: 56552
		public virtual extern object IHTMLDocument4_oncontrolselect
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049EE RID: 18926
		// (get) Token: 0x0600DCEA RID: 56554
		public virtual extern string IHTMLDocument4_URLUnencoded
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049EF RID: 18927
		// (get) Token: 0x0600DCEC RID: 56556
		// (set) Token: 0x0600DCEB RID: 56555
		public virtual extern object IHTMLDocument5_onmousewheel
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049F0 RID: 18928
		// (get) Token: 0x0600DCED RID: 56557
		public virtual extern IHTMLDOMNode IHTMLDocument5_doctype
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170049F1 RID: 18929
		// (get) Token: 0x0600DCEE RID: 56558
		public virtual extern IHTMLDOMImplementation IHTMLDocument5_implementation
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600DCEF RID: 56559
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLDocument5_createAttribute([MarshalAs(19)] [In] string bstrattrName);

		// Token: 0x0600DCF0 RID: 56560
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDocument5_createComment([MarshalAs(19)] [In] string bstrdata);

		// Token: 0x170049F2 RID: 18930
		// (get) Token: 0x0600DCF2 RID: 56562
		// (set) Token: 0x0600DCF1 RID: 56561
		public virtual extern object IHTMLDocument5_onfocusin
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049F3 RID: 18931
		// (get) Token: 0x0600DCF4 RID: 56564
		// (set) Token: 0x0600DCF3 RID: 56563
		public virtual extern object IHTMLDocument5_onfocusout
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049F4 RID: 18932
		// (get) Token: 0x0600DCF6 RID: 56566
		// (set) Token: 0x0600DCF5 RID: 56565
		public virtual extern object IHTMLDocument5_onactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049F5 RID: 18933
		// (get) Token: 0x0600DCF8 RID: 56568
		// (set) Token: 0x0600DCF7 RID: 56567
		public virtual extern object IHTMLDocument5_ondeactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049F6 RID: 18934
		// (get) Token: 0x0600DCFA RID: 56570
		// (set) Token: 0x0600DCF9 RID: 56569
		public virtual extern object IHTMLDocument5_onbeforeactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049F7 RID: 18935
		// (get) Token: 0x0600DCFC RID: 56572
		// (set) Token: 0x0600DCFB RID: 56571
		public virtual extern object IHTMLDocument5_onbeforedeactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170049F8 RID: 18936
		// (get) Token: 0x0600DCFD RID: 56573
		public virtual extern string IHTMLDocument5_compatMode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049F9 RID: 18937
		// (get) Token: 0x0600DCFE RID: 56574
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170049FA RID: 18938
		// (get) Token: 0x0600DCFF RID: 56575
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600DD00 RID: 56576
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x170049FB RID: 18939
		// (get) Token: 0x0600DD01 RID: 56577
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170049FC RID: 18940
		// (get) Token: 0x0600DD02 RID: 56578
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600DD03 RID: 56579
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600DD04 RID: 56580
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600DD05 RID: 56581
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600DD06 RID: 56582
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600DD07 RID: 56583
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600DD08 RID: 56584
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600DD09 RID: 56585
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600DD0A RID: 56586
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170049FD RID: 18941
		// (get) Token: 0x0600DD0B RID: 56587
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170049FE RID: 18942
		// (get) Token: 0x0600DD0D RID: 56589
		// (set) Token: 0x0600DD0C RID: 56588
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

		// Token: 0x170049FF RID: 18943
		// (get) Token: 0x0600DD0E RID: 56590
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004A00 RID: 18944
		// (get) Token: 0x0600DD0F RID: 56591
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004A01 RID: 18945
		// (get) Token: 0x0600DD10 RID: 56592
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004A02 RID: 18946
		// (get) Token: 0x0600DD11 RID: 56593
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004A03 RID: 18947
		// (get) Token: 0x0600DD12 RID: 56594
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x14001A5D RID: 6749
		// (add) Token: 0x0600DD13 RID: 56595
		// (remove) Token: 0x0600DD14 RID: 56596
		public virtual extern event HTMLDocumentEvents_onhelpEventHandler HTMLDocumentEvents_Event_onhelp;

		// Token: 0x14001A5E RID: 6750
		// (add) Token: 0x0600DD15 RID: 56597
		// (remove) Token: 0x0600DD16 RID: 56598
		public virtual extern event HTMLDocumentEvents_onclickEventHandler HTMLDocumentEvents_Event_onclick;

		// Token: 0x14001A5F RID: 6751
		// (add) Token: 0x0600DD17 RID: 56599
		// (remove) Token: 0x0600DD18 RID: 56600
		public virtual extern event HTMLDocumentEvents_ondblclickEventHandler HTMLDocumentEvents_Event_ondblclick;

		// Token: 0x14001A60 RID: 6752
		// (add) Token: 0x0600DD19 RID: 56601
		// (remove) Token: 0x0600DD1A RID: 56602
		public virtual extern event HTMLDocumentEvents_onkeydownEventHandler HTMLDocumentEvents_Event_onkeydown;

		// Token: 0x14001A61 RID: 6753
		// (add) Token: 0x0600DD1B RID: 56603
		// (remove) Token: 0x0600DD1C RID: 56604
		public virtual extern event HTMLDocumentEvents_onkeyupEventHandler HTMLDocumentEvents_Event_onkeyup;

		// Token: 0x14001A62 RID: 6754
		// (add) Token: 0x0600DD1D RID: 56605
		// (remove) Token: 0x0600DD1E RID: 56606
		public virtual extern event HTMLDocumentEvents_onkeypressEventHandler HTMLDocumentEvents_Event_onkeypress;

		// Token: 0x14001A63 RID: 6755
		// (add) Token: 0x0600DD1F RID: 56607
		// (remove) Token: 0x0600DD20 RID: 56608
		public virtual extern event HTMLDocumentEvents_onmousedownEventHandler HTMLDocumentEvents_Event_onmousedown;

		// Token: 0x14001A64 RID: 6756
		// (add) Token: 0x0600DD21 RID: 56609
		// (remove) Token: 0x0600DD22 RID: 56610
		public virtual extern event HTMLDocumentEvents_onmousemoveEventHandler HTMLDocumentEvents_Event_onmousemove;

		// Token: 0x14001A65 RID: 6757
		// (add) Token: 0x0600DD23 RID: 56611
		// (remove) Token: 0x0600DD24 RID: 56612
		public virtual extern event HTMLDocumentEvents_onmouseupEventHandler HTMLDocumentEvents_Event_onmouseup;

		// Token: 0x14001A66 RID: 6758
		// (add) Token: 0x0600DD25 RID: 56613
		// (remove) Token: 0x0600DD26 RID: 56614
		public virtual extern event HTMLDocumentEvents_onmouseoutEventHandler HTMLDocumentEvents_Event_onmouseout;

		// Token: 0x14001A67 RID: 6759
		// (add) Token: 0x0600DD27 RID: 56615
		// (remove) Token: 0x0600DD28 RID: 56616
		public virtual extern event HTMLDocumentEvents_onmouseoverEventHandler HTMLDocumentEvents_Event_onmouseover;

		// Token: 0x14001A68 RID: 6760
		// (add) Token: 0x0600DD29 RID: 56617
		// (remove) Token: 0x0600DD2A RID: 56618
		public virtual extern event HTMLDocumentEvents_onreadystatechangeEventHandler HTMLDocumentEvents_Event_onreadystatechange;

		// Token: 0x14001A69 RID: 6761
		// (add) Token: 0x0600DD2B RID: 56619
		// (remove) Token: 0x0600DD2C RID: 56620
		public virtual extern event HTMLDocumentEvents_onbeforeupdateEventHandler HTMLDocumentEvents_Event_onbeforeupdate;

		// Token: 0x14001A6A RID: 6762
		// (add) Token: 0x0600DD2D RID: 56621
		// (remove) Token: 0x0600DD2E RID: 56622
		public virtual extern event HTMLDocumentEvents_onafterupdateEventHandler HTMLDocumentEvents_Event_onafterupdate;

		// Token: 0x14001A6B RID: 6763
		// (add) Token: 0x0600DD2F RID: 56623
		// (remove) Token: 0x0600DD30 RID: 56624
		public virtual extern event HTMLDocumentEvents_onrowexitEventHandler HTMLDocumentEvents_Event_onrowexit;

		// Token: 0x14001A6C RID: 6764
		// (add) Token: 0x0600DD31 RID: 56625
		// (remove) Token: 0x0600DD32 RID: 56626
		public virtual extern event HTMLDocumentEvents_onrowenterEventHandler HTMLDocumentEvents_Event_onrowenter;

		// Token: 0x14001A6D RID: 6765
		// (add) Token: 0x0600DD33 RID: 56627
		// (remove) Token: 0x0600DD34 RID: 56628
		public virtual extern event HTMLDocumentEvents_ondragstartEventHandler HTMLDocumentEvents_Event_ondragstart;

		// Token: 0x14001A6E RID: 6766
		// (add) Token: 0x0600DD35 RID: 56629
		// (remove) Token: 0x0600DD36 RID: 56630
		public virtual extern event HTMLDocumentEvents_onselectstartEventHandler HTMLDocumentEvents_Event_onselectstart;

		// Token: 0x14001A6F RID: 6767
		// (add) Token: 0x0600DD37 RID: 56631
		// (remove) Token: 0x0600DD38 RID: 56632
		public virtual extern event HTMLDocumentEvents_onerrorupdateEventHandler HTMLDocumentEvents_Event_onerrorupdate;

		// Token: 0x14001A70 RID: 6768
		// (add) Token: 0x0600DD39 RID: 56633
		// (remove) Token: 0x0600DD3A RID: 56634
		public virtual extern event HTMLDocumentEvents_oncontextmenuEventHandler HTMLDocumentEvents_Event_oncontextmenu;

		// Token: 0x14001A71 RID: 6769
		// (add) Token: 0x0600DD3B RID: 56635
		// (remove) Token: 0x0600DD3C RID: 56636
		public virtual extern event HTMLDocumentEvents_onstopEventHandler HTMLDocumentEvents_Event_onstop;

		// Token: 0x14001A72 RID: 6770
		// (add) Token: 0x0600DD3D RID: 56637
		// (remove) Token: 0x0600DD3E RID: 56638
		public virtual extern event HTMLDocumentEvents_onrowsdeleteEventHandler HTMLDocumentEvents_Event_onrowsdelete;

		// Token: 0x14001A73 RID: 6771
		// (add) Token: 0x0600DD3F RID: 56639
		// (remove) Token: 0x0600DD40 RID: 56640
		public virtual extern event HTMLDocumentEvents_onrowsinsertedEventHandler HTMLDocumentEvents_Event_onrowsinserted;

		// Token: 0x14001A74 RID: 6772
		// (add) Token: 0x0600DD41 RID: 56641
		// (remove) Token: 0x0600DD42 RID: 56642
		public virtual extern event HTMLDocumentEvents_oncellchangeEventHandler HTMLDocumentEvents_Event_oncellchange;

		// Token: 0x14001A75 RID: 6773
		// (add) Token: 0x0600DD43 RID: 56643
		// (remove) Token: 0x0600DD44 RID: 56644
		public virtual extern event HTMLDocumentEvents_onpropertychangeEventHandler HTMLDocumentEvents_Event_onpropertychange;

		// Token: 0x14001A76 RID: 6774
		// (add) Token: 0x0600DD45 RID: 56645
		// (remove) Token: 0x0600DD46 RID: 56646
		public virtual extern event HTMLDocumentEvents_ondatasetchangedEventHandler HTMLDocumentEvents_Event_ondatasetchanged;

		// Token: 0x14001A77 RID: 6775
		// (add) Token: 0x0600DD47 RID: 56647
		// (remove) Token: 0x0600DD48 RID: 56648
		public virtual extern event HTMLDocumentEvents_ondataavailableEventHandler HTMLDocumentEvents_Event_ondataavailable;

		// Token: 0x14001A78 RID: 6776
		// (add) Token: 0x0600DD49 RID: 56649
		// (remove) Token: 0x0600DD4A RID: 56650
		public virtual extern event HTMLDocumentEvents_ondatasetcompleteEventHandler HTMLDocumentEvents_Event_ondatasetcomplete;

		// Token: 0x14001A79 RID: 6777
		// (add) Token: 0x0600DD4B RID: 56651
		// (remove) Token: 0x0600DD4C RID: 56652
		public virtual extern event HTMLDocumentEvents_onbeforeeditfocusEventHandler HTMLDocumentEvents_Event_onbeforeeditfocus;

		// Token: 0x14001A7A RID: 6778
		// (add) Token: 0x0600DD4D RID: 56653
		// (remove) Token: 0x0600DD4E RID: 56654
		public virtual extern event HTMLDocumentEvents_onselectionchangeEventHandler HTMLDocumentEvents_Event_onselectionchange;

		// Token: 0x14001A7B RID: 6779
		// (add) Token: 0x0600DD4F RID: 56655
		// (remove) Token: 0x0600DD50 RID: 56656
		public virtual extern event HTMLDocumentEvents_oncontrolselectEventHandler HTMLDocumentEvents_Event_oncontrolselect;

		// Token: 0x14001A7C RID: 6780
		// (add) Token: 0x0600DD51 RID: 56657
		// (remove) Token: 0x0600DD52 RID: 56658
		public virtual extern event HTMLDocumentEvents_onmousewheelEventHandler HTMLDocumentEvents_Event_onmousewheel;

		// Token: 0x14001A7D RID: 6781
		// (add) Token: 0x0600DD53 RID: 56659
		// (remove) Token: 0x0600DD54 RID: 56660
		public virtual extern event HTMLDocumentEvents_onfocusinEventHandler HTMLDocumentEvents_Event_onfocusin;

		// Token: 0x14001A7E RID: 6782
		// (add) Token: 0x0600DD55 RID: 56661
		// (remove) Token: 0x0600DD56 RID: 56662
		public virtual extern event HTMLDocumentEvents_onfocusoutEventHandler HTMLDocumentEvents_Event_onfocusout;

		// Token: 0x14001A7F RID: 6783
		// (add) Token: 0x0600DD57 RID: 56663
		// (remove) Token: 0x0600DD58 RID: 56664
		public virtual extern event HTMLDocumentEvents_onactivateEventHandler HTMLDocumentEvents_Event_onactivate;

		// Token: 0x14001A80 RID: 6784
		// (add) Token: 0x0600DD59 RID: 56665
		// (remove) Token: 0x0600DD5A RID: 56666
		public virtual extern event HTMLDocumentEvents_ondeactivateEventHandler HTMLDocumentEvents_Event_ondeactivate;

		// Token: 0x14001A81 RID: 6785
		// (add) Token: 0x0600DD5B RID: 56667
		// (remove) Token: 0x0600DD5C RID: 56668
		public virtual extern event HTMLDocumentEvents_onbeforeactivateEventHandler HTMLDocumentEvents_Event_onbeforeactivate;

		// Token: 0x14001A82 RID: 6786
		// (add) Token: 0x0600DD5D RID: 56669
		// (remove) Token: 0x0600DD5E RID: 56670
		public virtual extern event HTMLDocumentEvents_onbeforedeactivateEventHandler HTMLDocumentEvents_Event_onbeforedeactivate;

		// Token: 0x14001A83 RID: 6787
		// (add) Token: 0x0600DD5F RID: 56671
		// (remove) Token: 0x0600DD60 RID: 56672
		public virtual extern event HTMLDocumentEvents2_onhelpEventHandler HTMLDocumentEvents2_Event_onhelp;

		// Token: 0x14001A84 RID: 6788
		// (add) Token: 0x0600DD61 RID: 56673
		// (remove) Token: 0x0600DD62 RID: 56674
		public virtual extern event HTMLDocumentEvents2_onclickEventHandler HTMLDocumentEvents2_Event_onclick;

		// Token: 0x14001A85 RID: 6789
		// (add) Token: 0x0600DD63 RID: 56675
		// (remove) Token: 0x0600DD64 RID: 56676
		public virtual extern event HTMLDocumentEvents2_ondblclickEventHandler HTMLDocumentEvents2_Event_ondblclick;

		// Token: 0x14001A86 RID: 6790
		// (add) Token: 0x0600DD65 RID: 56677
		// (remove) Token: 0x0600DD66 RID: 56678
		public virtual extern event HTMLDocumentEvents2_onkeydownEventHandler HTMLDocumentEvents2_Event_onkeydown;

		// Token: 0x14001A87 RID: 6791
		// (add) Token: 0x0600DD67 RID: 56679
		// (remove) Token: 0x0600DD68 RID: 56680
		public virtual extern event HTMLDocumentEvents2_onkeyupEventHandler HTMLDocumentEvents2_Event_onkeyup;

		// Token: 0x14001A88 RID: 6792
		// (add) Token: 0x0600DD69 RID: 56681
		// (remove) Token: 0x0600DD6A RID: 56682
		public virtual extern event HTMLDocumentEvents2_onkeypressEventHandler HTMLDocumentEvents2_Event_onkeypress;

		// Token: 0x14001A89 RID: 6793
		// (add) Token: 0x0600DD6B RID: 56683
		// (remove) Token: 0x0600DD6C RID: 56684
		public virtual extern event HTMLDocumentEvents2_onmousedownEventHandler HTMLDocumentEvents2_Event_onmousedown;

		// Token: 0x14001A8A RID: 6794
		// (add) Token: 0x0600DD6D RID: 56685
		// (remove) Token: 0x0600DD6E RID: 56686
		public virtual extern event HTMLDocumentEvents2_onmousemoveEventHandler HTMLDocumentEvents2_Event_onmousemove;

		// Token: 0x14001A8B RID: 6795
		// (add) Token: 0x0600DD6F RID: 56687
		// (remove) Token: 0x0600DD70 RID: 56688
		public virtual extern event HTMLDocumentEvents2_onmouseupEventHandler HTMLDocumentEvents2_Event_onmouseup;

		// Token: 0x14001A8C RID: 6796
		// (add) Token: 0x0600DD71 RID: 56689
		// (remove) Token: 0x0600DD72 RID: 56690
		public virtual extern event HTMLDocumentEvents2_onmouseoutEventHandler HTMLDocumentEvents2_Event_onmouseout;

		// Token: 0x14001A8D RID: 6797
		// (add) Token: 0x0600DD73 RID: 56691
		// (remove) Token: 0x0600DD74 RID: 56692
		public virtual extern event HTMLDocumentEvents2_onmouseoverEventHandler HTMLDocumentEvents2_Event_onmouseover;

		// Token: 0x14001A8E RID: 6798
		// (add) Token: 0x0600DD75 RID: 56693
		// (remove) Token: 0x0600DD76 RID: 56694
		public virtual extern event HTMLDocumentEvents2_onreadystatechangeEventHandler HTMLDocumentEvents2_Event_onreadystatechange;

		// Token: 0x14001A8F RID: 6799
		// (add) Token: 0x0600DD77 RID: 56695
		// (remove) Token: 0x0600DD78 RID: 56696
		public virtual extern event HTMLDocumentEvents2_onbeforeupdateEventHandler HTMLDocumentEvents2_Event_onbeforeupdate;

		// Token: 0x14001A90 RID: 6800
		// (add) Token: 0x0600DD79 RID: 56697
		// (remove) Token: 0x0600DD7A RID: 56698
		public virtual extern event HTMLDocumentEvents2_onafterupdateEventHandler HTMLDocumentEvents2_Event_onafterupdate;

		// Token: 0x14001A91 RID: 6801
		// (add) Token: 0x0600DD7B RID: 56699
		// (remove) Token: 0x0600DD7C RID: 56700
		public virtual extern event HTMLDocumentEvents2_onrowexitEventHandler HTMLDocumentEvents2_Event_onrowexit;

		// Token: 0x14001A92 RID: 6802
		// (add) Token: 0x0600DD7D RID: 56701
		// (remove) Token: 0x0600DD7E RID: 56702
		public virtual extern event HTMLDocumentEvents2_onrowenterEventHandler HTMLDocumentEvents2_Event_onrowenter;

		// Token: 0x14001A93 RID: 6803
		// (add) Token: 0x0600DD7F RID: 56703
		// (remove) Token: 0x0600DD80 RID: 56704
		public virtual extern event HTMLDocumentEvents2_ondragstartEventHandler HTMLDocumentEvents2_Event_ondragstart;

		// Token: 0x14001A94 RID: 6804
		// (add) Token: 0x0600DD81 RID: 56705
		// (remove) Token: 0x0600DD82 RID: 56706
		public virtual extern event HTMLDocumentEvents2_onselectstartEventHandler HTMLDocumentEvents2_Event_onselectstart;

		// Token: 0x14001A95 RID: 6805
		// (add) Token: 0x0600DD83 RID: 56707
		// (remove) Token: 0x0600DD84 RID: 56708
		public virtual extern event HTMLDocumentEvents2_onerrorupdateEventHandler HTMLDocumentEvents2_Event_onerrorupdate;

		// Token: 0x14001A96 RID: 6806
		// (add) Token: 0x0600DD85 RID: 56709
		// (remove) Token: 0x0600DD86 RID: 56710
		public virtual extern event HTMLDocumentEvents2_oncontextmenuEventHandler HTMLDocumentEvents2_Event_oncontextmenu;

		// Token: 0x14001A97 RID: 6807
		// (add) Token: 0x0600DD87 RID: 56711
		// (remove) Token: 0x0600DD88 RID: 56712
		public virtual extern event HTMLDocumentEvents2_onstopEventHandler HTMLDocumentEvents2_Event_onstop;

		// Token: 0x14001A98 RID: 6808
		// (add) Token: 0x0600DD89 RID: 56713
		// (remove) Token: 0x0600DD8A RID: 56714
		public virtual extern event HTMLDocumentEvents2_onrowsdeleteEventHandler HTMLDocumentEvents2_Event_onrowsdelete;

		// Token: 0x14001A99 RID: 6809
		// (add) Token: 0x0600DD8B RID: 56715
		// (remove) Token: 0x0600DD8C RID: 56716
		public virtual extern event HTMLDocumentEvents2_onrowsinsertedEventHandler HTMLDocumentEvents2_Event_onrowsinserted;

		// Token: 0x14001A9A RID: 6810
		// (add) Token: 0x0600DD8D RID: 56717
		// (remove) Token: 0x0600DD8E RID: 56718
		public virtual extern event HTMLDocumentEvents2_oncellchangeEventHandler HTMLDocumentEvents2_Event_oncellchange;

		// Token: 0x14001A9B RID: 6811
		// (add) Token: 0x0600DD8F RID: 56719
		// (remove) Token: 0x0600DD90 RID: 56720
		public virtual extern event HTMLDocumentEvents2_onpropertychangeEventHandler HTMLDocumentEvents2_Event_onpropertychange;

		// Token: 0x14001A9C RID: 6812
		// (add) Token: 0x0600DD91 RID: 56721
		// (remove) Token: 0x0600DD92 RID: 56722
		public virtual extern event HTMLDocumentEvents2_ondatasetchangedEventHandler HTMLDocumentEvents2_Event_ondatasetchanged;

		// Token: 0x14001A9D RID: 6813
		// (add) Token: 0x0600DD93 RID: 56723
		// (remove) Token: 0x0600DD94 RID: 56724
		public virtual extern event HTMLDocumentEvents2_ondataavailableEventHandler HTMLDocumentEvents2_Event_ondataavailable;

		// Token: 0x14001A9E RID: 6814
		// (add) Token: 0x0600DD95 RID: 56725
		// (remove) Token: 0x0600DD96 RID: 56726
		public virtual extern event HTMLDocumentEvents2_ondatasetcompleteEventHandler HTMLDocumentEvents2_Event_ondatasetcomplete;

		// Token: 0x14001A9F RID: 6815
		// (add) Token: 0x0600DD97 RID: 56727
		// (remove) Token: 0x0600DD98 RID: 56728
		public virtual extern event HTMLDocumentEvents2_onbeforeeditfocusEventHandler HTMLDocumentEvents2_Event_onbeforeeditfocus;

		// Token: 0x14001AA0 RID: 6816
		// (add) Token: 0x0600DD99 RID: 56729
		// (remove) Token: 0x0600DD9A RID: 56730
		public virtual extern event HTMLDocumentEvents2_onselectionchangeEventHandler HTMLDocumentEvents2_Event_onselectionchange;

		// Token: 0x14001AA1 RID: 6817
		// (add) Token: 0x0600DD9B RID: 56731
		// (remove) Token: 0x0600DD9C RID: 56732
		public virtual extern event HTMLDocumentEvents2_oncontrolselectEventHandler HTMLDocumentEvents2_Event_oncontrolselect;

		// Token: 0x14001AA2 RID: 6818
		// (add) Token: 0x0600DD9D RID: 56733
		// (remove) Token: 0x0600DD9E RID: 56734
		public virtual extern event HTMLDocumentEvents2_onmousewheelEventHandler HTMLDocumentEvents2_Event_onmousewheel;

		// Token: 0x14001AA3 RID: 6819
		// (add) Token: 0x0600DD9F RID: 56735
		// (remove) Token: 0x0600DDA0 RID: 56736
		public virtual extern event HTMLDocumentEvents2_onfocusinEventHandler HTMLDocumentEvents2_Event_onfocusin;

		// Token: 0x14001AA4 RID: 6820
		// (add) Token: 0x0600DDA1 RID: 56737
		// (remove) Token: 0x0600DDA2 RID: 56738
		public virtual extern event HTMLDocumentEvents2_onfocusoutEventHandler HTMLDocumentEvents2_Event_onfocusout;

		// Token: 0x14001AA5 RID: 6821
		// (add) Token: 0x0600DDA3 RID: 56739
		// (remove) Token: 0x0600DDA4 RID: 56740
		public virtual extern event HTMLDocumentEvents2_onactivateEventHandler HTMLDocumentEvents2_Event_onactivate;

		// Token: 0x14001AA6 RID: 6822
		// (add) Token: 0x0600DDA5 RID: 56741
		// (remove) Token: 0x0600DDA6 RID: 56742
		public virtual extern event HTMLDocumentEvents2_ondeactivateEventHandler HTMLDocumentEvents2_Event_ondeactivate;

		// Token: 0x14001AA7 RID: 6823
		// (add) Token: 0x0600DDA7 RID: 56743
		// (remove) Token: 0x0600DDA8 RID: 56744
		public virtual extern event HTMLDocumentEvents2_onbeforeactivateEventHandler HTMLDocumentEvents2_Event_onbeforeactivate;

		// Token: 0x14001AA8 RID: 6824
		// (add) Token: 0x0600DDA9 RID: 56745
		// (remove) Token: 0x0600DDAA RID: 56746
		public virtual extern event HTMLDocumentEvents2_onbeforedeactivateEventHandler HTMLDocumentEvents2_Event_onbeforedeactivate;
	}
}
