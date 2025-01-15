using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D0A RID: 3338
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLDocumentEvents\0\0")]
	[Guid("D48A6EC9-6A4A-11CF-94A7-444553540000")]
	[ComImport]
	public class OldHTMLDocumentClass : DispHTMLDocument, OldHTMLDocument, HTMLDocumentEvents_Event, IHTMLDocument2, IHTMLDocument3
	{
		// Token: 0x0601643A RID: 91194
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern OldHTMLDocumentClass();

		// Token: 0x17007631 RID: 30257
		// (get) Token: 0x0601643B RID: 91195
		[DispId(1001)]
		public virtual extern object Script
		{
			[DispId(1001)]
			[TypeLibFunc(1088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007632 RID: 30258
		// (get) Token: 0x0601643C RID: 91196
		[DispId(1003)]
		public virtual extern IHTMLElementCollection all
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007633 RID: 30259
		// (get) Token: 0x0601643D RID: 91197
		[DispId(1004)]
		public virtual extern IHTMLElement body
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007634 RID: 30260
		// (get) Token: 0x0601643E RID: 91198
		[DispId(1005)]
		public virtual extern IHTMLElement activeElement
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007635 RID: 30261
		// (get) Token: 0x0601643F RID: 91199
		[DispId(1011)]
		public virtual extern IHTMLElementCollection images
		{
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007636 RID: 30262
		// (get) Token: 0x06016440 RID: 91200
		[DispId(1008)]
		public virtual extern IHTMLElementCollection applets
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007637 RID: 30263
		// (get) Token: 0x06016441 RID: 91201
		[DispId(1009)]
		public virtual extern IHTMLElementCollection links
		{
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007638 RID: 30264
		// (get) Token: 0x06016442 RID: 91202
		[DispId(1010)]
		public virtual extern IHTMLElementCollection forms
		{
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007639 RID: 30265
		// (get) Token: 0x06016443 RID: 91203
		[DispId(1007)]
		public virtual extern IHTMLElementCollection anchors
		{
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700763A RID: 30266
		// (get) Token: 0x06016445 RID: 91205
		// (set) Token: 0x06016444 RID: 91204
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

		// Token: 0x1700763B RID: 30267
		// (get) Token: 0x06016446 RID: 91206
		[DispId(1013)]
		public virtual extern IHTMLElementCollection scripts
		{
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700763C RID: 30268
		// (get) Token: 0x06016448 RID: 91208
		// (set) Token: 0x06016447 RID: 91207
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

		// Token: 0x1700763D RID: 30269
		// (get) Token: 0x06016449 RID: 91209
		[DispId(1017)]
		public virtual extern IHTMLSelectionObject selection
		{
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700763E RID: 30270
		// (get) Token: 0x0601644A RID: 91210
		[DispId(1018)]
		public virtual extern string readyState
		{
			[TypeLibFunc(4)]
			[DispId(1018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700763F RID: 30271
		// (get) Token: 0x0601644B RID: 91211
		[DispId(1019)]
		public virtual extern FramesCollection frames
		{
			[DispId(1019)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007640 RID: 30272
		// (get) Token: 0x0601644C RID: 91212
		[DispId(1015)]
		public virtual extern IHTMLElementCollection embeds
		{
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007641 RID: 30273
		// (get) Token: 0x0601644D RID: 91213
		[DispId(1021)]
		public virtual extern IHTMLElementCollection plugins
		{
			[DispId(1021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007642 RID: 30274
		// (get) Token: 0x0601644F RID: 91215
		// (set) Token: 0x0601644E RID: 91214
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

		// Token: 0x17007643 RID: 30275
		// (get) Token: 0x06016451 RID: 91217
		// (set) Token: 0x06016450 RID: 91216
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

		// Token: 0x17007644 RID: 30276
		// (get) Token: 0x06016453 RID: 91219
		// (set) Token: 0x06016452 RID: 91218
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

		// Token: 0x17007645 RID: 30277
		// (get) Token: 0x06016455 RID: 91221
		// (set) Token: 0x06016454 RID: 91220
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

		// Token: 0x17007646 RID: 30278
		// (get) Token: 0x06016457 RID: 91223
		// (set) Token: 0x06016456 RID: 91222
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

		// Token: 0x17007647 RID: 30279
		// (get) Token: 0x06016458 RID: 91224
		[DispId(1027)]
		public virtual extern string referrer
		{
			[DispId(1027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007648 RID: 30280
		// (get) Token: 0x06016459 RID: 91225
		[DispId(1026)]
		public virtual extern HTMLLocation location
		{
			[DispId(1026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007649 RID: 30281
		// (get) Token: 0x0601645A RID: 91226
		[DispId(1028)]
		public virtual extern string lastModified
		{
			[DispId(1028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700764A RID: 30282
		// (get) Token: 0x0601645C RID: 91228
		// (set) Token: 0x0601645B RID: 91227
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

		// Token: 0x1700764B RID: 30283
		// (get) Token: 0x0601645E RID: 91230
		// (set) Token: 0x0601645D RID: 91229
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

		// Token: 0x1700764C RID: 30284
		// (get) Token: 0x06016460 RID: 91232
		// (set) Token: 0x0601645F RID: 91231
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

		// Token: 0x1700764D RID: 30285
		// (get) Token: 0x06016462 RID: 91234
		// (set) Token: 0x06016461 RID: 91233
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

		// Token: 0x1700764E RID: 30286
		// (get) Token: 0x06016464 RID: 91236
		// (set) Token: 0x06016463 RID: 91235
		[DispId(1032)]
		public virtual extern string charset
		{
			[DispId(1032)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[DispId(1032)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700764F RID: 30287
		// (get) Token: 0x06016466 RID: 91238
		// (set) Token: 0x06016465 RID: 91237
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

		// Token: 0x17007650 RID: 30288
		// (get) Token: 0x06016467 RID: 91239
		[DispId(1041)]
		public virtual extern string mimeType
		{
			[DispId(1041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007651 RID: 30289
		// (get) Token: 0x06016468 RID: 91240
		[DispId(1042)]
		public virtual extern string fileSize
		{
			[DispId(1042)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007652 RID: 30290
		// (get) Token: 0x06016469 RID: 91241
		[DispId(1043)]
		public virtual extern string fileCreatedDate
		{
			[DispId(1043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007653 RID: 30291
		// (get) Token: 0x0601646A RID: 91242
		[DispId(1044)]
		public virtual extern string fileModifiedDate
		{
			[DispId(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007654 RID: 30292
		// (get) Token: 0x0601646B RID: 91243
		[DispId(1045)]
		public virtual extern string fileUpdatedDate
		{
			[DispId(1045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007655 RID: 30293
		// (get) Token: 0x0601646C RID: 91244
		[DispId(1046)]
		public virtual extern string security
		{
			[DispId(1046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007656 RID: 30294
		// (get) Token: 0x0601646D RID: 91245
		[DispId(1047)]
		public virtual extern string protocol
		{
			[DispId(1047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007657 RID: 30295
		// (get) Token: 0x0601646E RID: 91246
		[DispId(1048)]
		public virtual extern string nameProp
		{
			[DispId(1048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0601646F RID: 91247
		[DispId(1054)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void write([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x06016470 RID: 91248
		[DispId(1055)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void writeln([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x06016471 RID: 91249
		[DispId(1056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object open([MarshalAs(19)] [In] string url = "text/html", [MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object features, [MarshalAs(27)] [In] [Optional] object replace);

		// Token: 0x06016472 RID: 91250
		[DispId(1057)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void close();

		// Token: 0x06016473 RID: 91251
		[DispId(1058)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clear();

		// Token: 0x06016474 RID: 91252
		[DispId(1059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool queryCommandSupported([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016475 RID: 91253
		[DispId(1060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool queryCommandEnabled([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016476 RID: 91254
		[DispId(1061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool queryCommandState([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016477 RID: 91255
		[DispId(1062)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool queryCommandIndeterm([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016478 RID: 91256
		[DispId(1063)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string queryCommandText([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016479 RID: 91257
		[DispId(1064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object queryCommandValue([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0601647A RID: 91258
		[DispId(1065)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool execCommand([MarshalAs(19)] [In] string cmdID, [In] bool showUI = false, [MarshalAs(27)] [In] [Optional] object value);

		// Token: 0x0601647B RID: 91259
		[DispId(1066)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool execCommandShowHelp([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0601647C RID: 91260
		[DispId(1067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement createElement([MarshalAs(19)] [In] string eTag);

		// Token: 0x17007658 RID: 30296
		// (get) Token: 0x0601647E RID: 91262
		// (set) Token: 0x0601647D RID: 91261
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

		// Token: 0x17007659 RID: 30297
		// (get) Token: 0x06016480 RID: 91264
		// (set) Token: 0x0601647F RID: 91263
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

		// Token: 0x1700765A RID: 30298
		// (get) Token: 0x06016482 RID: 91266
		// (set) Token: 0x06016481 RID: 91265
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

		// Token: 0x1700765B RID: 30299
		// (get) Token: 0x06016484 RID: 91268
		// (set) Token: 0x06016483 RID: 91267
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

		// Token: 0x1700765C RID: 30300
		// (get) Token: 0x06016486 RID: 91270
		// (set) Token: 0x06016485 RID: 91269
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

		// Token: 0x1700765D RID: 30301
		// (get) Token: 0x06016488 RID: 91272
		// (set) Token: 0x06016487 RID: 91271
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

		// Token: 0x1700765E RID: 30302
		// (get) Token: 0x0601648A RID: 91274
		// (set) Token: 0x06016489 RID: 91273
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

		// Token: 0x1700765F RID: 30303
		// (get) Token: 0x0601648C RID: 91276
		// (set) Token: 0x0601648B RID: 91275
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

		// Token: 0x17007660 RID: 30304
		// (get) Token: 0x0601648E RID: 91278
		// (set) Token: 0x0601648D RID: 91277
		[DispId(-2147412108)]
		public virtual extern object onmousemove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007661 RID: 30305
		// (get) Token: 0x06016490 RID: 91280
		// (set) Token: 0x0601648F RID: 91279
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

		// Token: 0x17007662 RID: 30306
		// (get) Token: 0x06016492 RID: 91282
		// (set) Token: 0x06016491 RID: 91281
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

		// Token: 0x17007663 RID: 30307
		// (get) Token: 0x06016494 RID: 91284
		// (set) Token: 0x06016493 RID: 91283
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

		// Token: 0x17007664 RID: 30308
		// (get) Token: 0x06016496 RID: 91286
		// (set) Token: 0x06016495 RID: 91285
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

		// Token: 0x17007665 RID: 30309
		// (get) Token: 0x06016498 RID: 91288
		// (set) Token: 0x06016497 RID: 91287
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

		// Token: 0x17007666 RID: 30310
		// (get) Token: 0x0601649A RID: 91290
		// (set) Token: 0x06016499 RID: 91289
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

		// Token: 0x17007667 RID: 30311
		// (get) Token: 0x0601649C RID: 91292
		// (set) Token: 0x0601649B RID: 91291
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

		// Token: 0x17007668 RID: 30312
		// (get) Token: 0x0601649E RID: 91294
		// (set) Token: 0x0601649D RID: 91293
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

		// Token: 0x0601649F RID: 91295
		[DispId(1068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement elementFromPoint([In] int x, [In] int y);

		// Token: 0x17007669 RID: 30313
		// (get) Token: 0x060164A0 RID: 91296
		[DispId(1034)]
		public virtual extern IHTMLWindow2 parentWindow
		{
			[DispId(1034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700766A RID: 30314
		// (get) Token: 0x060164A1 RID: 91297
		[DispId(1069)]
		public virtual extern HTMLStyleSheetsCollection styleSheets
		{
			[DispId(1069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700766B RID: 30315
		// (get) Token: 0x060164A3 RID: 91299
		// (set) Token: 0x060164A2 RID: 91298
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

		// Token: 0x1700766C RID: 30316
		// (get) Token: 0x060164A5 RID: 91301
		// (set) Token: 0x060164A4 RID: 91300
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

		// Token: 0x060164A6 RID: 91302
		[DispId(1070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x060164A7 RID: 91303
		[DispId(1071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLStyleSheet createStyleSheet([MarshalAs(19)] [In] string bstrHref = "", [In] int lIndex = -1);

		// Token: 0x060164A8 RID: 91304
		[DispId(1072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x060164A9 RID: 91305
		[DispId(1073)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void recalc([In] bool fForce = false);

		// Token: 0x060164AA RID: 91306
		[DispId(1074)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode createTextNode([MarshalAs(19)] [In] string text);

		// Token: 0x1700766D RID: 30317
		// (get) Token: 0x060164AB RID: 91307
		[DispId(1075)]
		public virtual extern IHTMLElement documentElement
		{
			[DispId(1075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700766E RID: 30318
		// (get) Token: 0x060164AC RID: 91308
		[DispId(1077)]
		public virtual extern string uniqueID
		{
			[DispId(1077)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060164AD RID: 91309
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060164AE RID: 91310
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700766F RID: 30319
		// (get) Token: 0x060164B0 RID: 91312
		// (set) Token: 0x060164AF RID: 91311
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

		// Token: 0x17007670 RID: 30320
		// (get) Token: 0x060164B2 RID: 91314
		// (set) Token: 0x060164B1 RID: 91313
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

		// Token: 0x17007671 RID: 30321
		// (get) Token: 0x060164B4 RID: 91316
		// (set) Token: 0x060164B3 RID: 91315
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

		// Token: 0x17007672 RID: 30322
		// (get) Token: 0x060164B6 RID: 91318
		// (set) Token: 0x060164B5 RID: 91317
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

		// Token: 0x17007673 RID: 30323
		// (get) Token: 0x060164B8 RID: 91320
		// (set) Token: 0x060164B7 RID: 91319
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

		// Token: 0x17007674 RID: 30324
		// (get) Token: 0x060164BA RID: 91322
		// (set) Token: 0x060164B9 RID: 91321
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

		// Token: 0x17007675 RID: 30325
		// (get) Token: 0x060164BC RID: 91324
		// (set) Token: 0x060164BB RID: 91323
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

		// Token: 0x17007676 RID: 30326
		// (get) Token: 0x060164BE RID: 91326
		// (set) Token: 0x060164BD RID: 91325
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

		// Token: 0x17007677 RID: 30327
		// (get) Token: 0x060164C0 RID: 91328
		// (set) Token: 0x060164BF RID: 91327
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

		// Token: 0x17007678 RID: 30328
		// (get) Token: 0x060164C2 RID: 91330
		// (set) Token: 0x060164C1 RID: 91329
		[DispId(-2147412044)]
		public virtual extern object onstop
		{
			[DispId(-2147412044)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060164C3 RID: 91331
		[DispId(1076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDocument2 createDocumentFragment();

		// Token: 0x17007679 RID: 30329
		// (get) Token: 0x060164C4 RID: 91332
		[DispId(1078)]
		public virtual extern IHTMLDocument2 parentDocument
		{
			[TypeLibFunc(65)]
			[DispId(1078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700767A RID: 30330
		// (get) Token: 0x060164C6 RID: 91334
		// (set) Token: 0x060164C5 RID: 91333
		[DispId(1079)]
		public virtual extern bool enableDownload
		{
			[TypeLibFunc(65)]
			[DispId(1079)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(65)]
			[DispId(1079)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700767B RID: 30331
		// (get) Token: 0x060164C8 RID: 91336
		// (set) Token: 0x060164C7 RID: 91335
		[DispId(1080)]
		public virtual extern string baseUrl
		{
			[TypeLibFunc(65)]
			[DispId(1080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(65)]
			[DispId(1080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700767C RID: 30332
		// (get) Token: 0x060164CA RID: 91338
		// (set) Token: 0x060164C9 RID: 91337
		[DispId(1082)]
		public virtual extern bool inheritStyleSheets
		{
			[TypeLibFunc(65)]
			[DispId(1082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1082)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700767D RID: 30333
		// (get) Token: 0x060164CC RID: 91340
		// (set) Token: 0x060164CB RID: 91339
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

		// Token: 0x060164CD RID: 91341
		[DispId(1086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByName([MarshalAs(19)] [In] string v);

		// Token: 0x060164CE RID: 91342
		[DispId(1088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement getElementById([MarshalAs(19)] [In] string v);

		// Token: 0x060164CF RID: 91343
		[DispId(1087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060164D0 RID: 91344
		[DispId(1089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x060164D1 RID: 91345
		[DispId(1090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasFocus();

		// Token: 0x1700767E RID: 30334
		// (get) Token: 0x060164D3 RID: 91347
		// (set) Token: 0x060164D2 RID: 91346
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

		// Token: 0x1700767F RID: 30335
		// (get) Token: 0x060164D4 RID: 91348
		[DispId(1091)]
		public virtual extern object namespaces
		{
			[DispId(1091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x060164D5 RID: 91349
		[DispId(1092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDocument2 createDocumentFromUrl([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(19)] [In] string bstrOptions);

		// Token: 0x17007680 RID: 30336
		// (get) Token: 0x060164D7 RID: 91351
		// (set) Token: 0x060164D6 RID: 91350
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

		// Token: 0x060164D8 RID: 91352
		[DispId(1094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLEventObj CreateEventObject([MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x060164D9 RID: 91353
		[DispId(1095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x060164DA RID: 91354
		[DispId(1096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRenderStyle createRenderStyle([MarshalAs(19)] [In] string v);

		// Token: 0x17007681 RID: 30337
		// (get) Token: 0x060164DC RID: 91356
		// (set) Token: 0x060164DB RID: 91355
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

		// Token: 0x17007682 RID: 30338
		// (get) Token: 0x060164DD RID: 91357
		[DispId(1097)]
		public virtual extern string URLUnencoded
		{
			[DispId(1097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007683 RID: 30339
		// (get) Token: 0x060164DF RID: 91359
		// (set) Token: 0x060164DE RID: 91358
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

		// Token: 0x17007684 RID: 30340
		// (get) Token: 0x060164E0 RID: 91360
		[DispId(1098)]
		public virtual extern IHTMLDOMNode doctype
		{
			[DispId(1098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007685 RID: 30341
		// (get) Token: 0x060164E1 RID: 91361
		[DispId(1099)]
		public virtual extern IHTMLDOMImplementation implementation
		{
			[DispId(1099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060164E2 RID: 91362
		[DispId(1100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute createAttribute([MarshalAs(19)] [In] string bstrattrName);

		// Token: 0x060164E3 RID: 91363
		[DispId(1101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode createComment([MarshalAs(19)] [In] string bstrdata);

		// Token: 0x17007686 RID: 30342
		// (get) Token: 0x060164E5 RID: 91365
		// (set) Token: 0x060164E4 RID: 91364
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

		// Token: 0x17007687 RID: 30343
		// (get) Token: 0x060164E7 RID: 91367
		// (set) Token: 0x060164E6 RID: 91366
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

		// Token: 0x17007688 RID: 30344
		// (get) Token: 0x060164E9 RID: 91369
		// (set) Token: 0x060164E8 RID: 91368
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

		// Token: 0x17007689 RID: 30345
		// (get) Token: 0x060164EB RID: 91371
		// (set) Token: 0x060164EA RID: 91370
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

		// Token: 0x1700768A RID: 30346
		// (get) Token: 0x060164ED RID: 91373
		// (set) Token: 0x060164EC RID: 91372
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

		// Token: 0x1700768B RID: 30347
		// (get) Token: 0x060164EF RID: 91375
		// (set) Token: 0x060164EE RID: 91374
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

		// Token: 0x1700768C RID: 30348
		// (get) Token: 0x060164F0 RID: 91376
		[DispId(1102)]
		public virtual extern string compatMode
		{
			[DispId(1102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700768D RID: 30349
		// (get) Token: 0x060164F1 RID: 91377
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700768E RID: 30350
		// (get) Token: 0x060164F2 RID: 91378
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060164F3 RID: 91379
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x1700768F RID: 30351
		// (get) Token: 0x060164F4 RID: 91380
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007690 RID: 30352
		// (get) Token: 0x060164F5 RID: 91381
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x060164F6 RID: 91382
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x060164F7 RID: 91383
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060164F8 RID: 91384
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060164F9 RID: 91385
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x060164FA RID: 91386
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x060164FB RID: 91387
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x060164FC RID: 91388
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x060164FD RID: 91389
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17007691 RID: 30353
		// (get) Token: 0x060164FE RID: 91390
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007692 RID: 30354
		// (get) Token: 0x06016500 RID: 91392
		// (set) Token: 0x060164FF RID: 91391
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

		// Token: 0x17007693 RID: 30355
		// (get) Token: 0x06016501 RID: 91393
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007694 RID: 30356
		// (get) Token: 0x06016502 RID: 91394
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007695 RID: 30357
		// (get) Token: 0x06016503 RID: 91395
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007696 RID: 30358
		// (get) Token: 0x06016504 RID: 91396
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007697 RID: 30359
		// (get) Token: 0x06016505 RID: 91397
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007698 RID: 30360
		// (get) Token: 0x06016506 RID: 91398
		public virtual extern object IHTMLDocument2_Script
		{
			[TypeLibFunc(1088)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007699 RID: 30361
		// (get) Token: 0x06016507 RID: 91399
		public virtual extern IHTMLElementCollection IHTMLDocument2_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700769A RID: 30362
		// (get) Token: 0x06016508 RID: 91400
		public virtual extern IHTMLElement IHTMLDocument2_body
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700769B RID: 30363
		// (get) Token: 0x06016509 RID: 91401
		public virtual extern IHTMLElement IHTMLDocument2_activeElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700769C RID: 30364
		// (get) Token: 0x0601650A RID: 91402
		public virtual extern IHTMLElementCollection IHTMLDocument2_images
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700769D RID: 30365
		// (get) Token: 0x0601650B RID: 91403
		public virtual extern IHTMLElementCollection IHTMLDocument2_applets
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700769E RID: 30366
		// (get) Token: 0x0601650C RID: 91404
		public virtual extern IHTMLElementCollection IHTMLDocument2_links
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700769F RID: 30367
		// (get) Token: 0x0601650D RID: 91405
		public virtual extern IHTMLElementCollection IHTMLDocument2_forms
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076A0 RID: 30368
		// (get) Token: 0x0601650E RID: 91406
		public virtual extern IHTMLElementCollection IHTMLDocument2_anchors
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076A1 RID: 30369
		// (get) Token: 0x06016510 RID: 91408
		// (set) Token: 0x0601650F RID: 91407
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

		// Token: 0x170076A2 RID: 30370
		// (get) Token: 0x06016511 RID: 91409
		public virtual extern IHTMLElementCollection IHTMLDocument2_scripts
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076A3 RID: 30371
		// (get) Token: 0x06016513 RID: 91411
		// (set) Token: 0x06016512 RID: 91410
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

		// Token: 0x170076A4 RID: 30372
		// (get) Token: 0x06016514 RID: 91412
		public virtual extern IHTMLSelectionObject IHTMLDocument2_selection
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076A5 RID: 30373
		// (get) Token: 0x06016515 RID: 91413
		public virtual extern string IHTMLDocument2_readyState
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076A6 RID: 30374
		// (get) Token: 0x06016516 RID: 91414
		public virtual extern FramesCollection IHTMLDocument2_frames
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076A7 RID: 30375
		// (get) Token: 0x06016517 RID: 91415
		public virtual extern IHTMLElementCollection IHTMLDocument2_embeds
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076A8 RID: 30376
		// (get) Token: 0x06016518 RID: 91416
		public virtual extern IHTMLElementCollection IHTMLDocument2_plugins
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076A9 RID: 30377
		// (get) Token: 0x0601651A RID: 91418
		// (set) Token: 0x06016519 RID: 91417
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

		// Token: 0x170076AA RID: 30378
		// (get) Token: 0x0601651C RID: 91420
		// (set) Token: 0x0601651B RID: 91419
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

		// Token: 0x170076AB RID: 30379
		// (get) Token: 0x0601651E RID: 91422
		// (set) Token: 0x0601651D RID: 91421
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

		// Token: 0x170076AC RID: 30380
		// (get) Token: 0x06016520 RID: 91424
		// (set) Token: 0x0601651F RID: 91423
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

		// Token: 0x170076AD RID: 30381
		// (get) Token: 0x06016522 RID: 91426
		// (set) Token: 0x06016521 RID: 91425
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

		// Token: 0x170076AE RID: 30382
		// (get) Token: 0x06016523 RID: 91427
		public virtual extern string IHTMLDocument2_referrer
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076AF RID: 30383
		// (get) Token: 0x06016524 RID: 91428
		public virtual extern HTMLLocation IHTMLDocument2_location
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076B0 RID: 30384
		// (get) Token: 0x06016525 RID: 91429
		public virtual extern string IHTMLDocument2_lastModified
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076B1 RID: 30385
		// (get) Token: 0x06016527 RID: 91431
		// (set) Token: 0x06016526 RID: 91430
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

		// Token: 0x170076B2 RID: 30386
		// (get) Token: 0x06016529 RID: 91433
		// (set) Token: 0x06016528 RID: 91432
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

		// Token: 0x170076B3 RID: 30387
		// (get) Token: 0x0601652B RID: 91435
		// (set) Token: 0x0601652A RID: 91434
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

		// Token: 0x170076B4 RID: 30388
		// (get) Token: 0x0601652D RID: 91437
		// (set) Token: 0x0601652C RID: 91436
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

		// Token: 0x170076B5 RID: 30389
		// (get) Token: 0x0601652F RID: 91439
		// (set) Token: 0x0601652E RID: 91438
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

		// Token: 0x170076B6 RID: 30390
		// (get) Token: 0x06016531 RID: 91441
		// (set) Token: 0x06016530 RID: 91440
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

		// Token: 0x170076B7 RID: 30391
		// (get) Token: 0x06016532 RID: 91442
		public virtual extern string IHTMLDocument2_mimeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076B8 RID: 30392
		// (get) Token: 0x06016533 RID: 91443
		public virtual extern string IHTMLDocument2_fileSize
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076B9 RID: 30393
		// (get) Token: 0x06016534 RID: 91444
		public virtual extern string IHTMLDocument2_fileCreatedDate
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076BA RID: 30394
		// (get) Token: 0x06016535 RID: 91445
		public virtual extern string IHTMLDocument2_fileModifiedDate
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076BB RID: 30395
		// (get) Token: 0x06016536 RID: 91446
		public virtual extern string IHTMLDocument2_fileUpdatedDate
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076BC RID: 30396
		// (get) Token: 0x06016537 RID: 91447
		public virtual extern string IHTMLDocument2_security
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076BD RID: 30397
		// (get) Token: 0x06016538 RID: 91448
		public virtual extern string IHTMLDocument2_protocol
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076BE RID: 30398
		// (get) Token: 0x06016539 RID: 91449
		public virtual extern string IHTMLDocument2_nameProp
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0601653A RID: 91450
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument2_write([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x0601653B RID: 91451
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument2_writeln([MarshalAs(29, SafeArraySubType = 12)] [In] params object[] psarray);

		// Token: 0x0601653C RID: 91452
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLDocument2_open([MarshalAs(19)] [In] string url = "text/html", [MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object features, [MarshalAs(27)] [In] [Optional] object replace);

		// Token: 0x0601653D RID: 91453
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument2_close();

		// Token: 0x0601653E RID: 91454
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument2_clear();

		// Token: 0x0601653F RID: 91455
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_queryCommandSupported([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016540 RID: 91456
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_queryCommandEnabled([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016541 RID: 91457
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_queryCommandState([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016542 RID: 91458
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_queryCommandIndeterm([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016543 RID: 91459
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLDocument2_queryCommandText([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016544 RID: 91460
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLDocument2_queryCommandValue([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016545 RID: 91461
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_execCommand([MarshalAs(19)] [In] string cmdID, [In] bool showUI = false, [MarshalAs(27)] [In] [Optional] object value);

		// Token: 0x06016546 RID: 91462
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument2_execCommandShowHelp([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06016547 RID: 91463
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLDocument2_createElement([MarshalAs(19)] [In] string eTag);

		// Token: 0x170076BF RID: 30399
		// (get) Token: 0x06016549 RID: 91465
		// (set) Token: 0x06016548 RID: 91464
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

		// Token: 0x170076C0 RID: 30400
		// (get) Token: 0x0601654B RID: 91467
		// (set) Token: 0x0601654A RID: 91466
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

		// Token: 0x170076C1 RID: 30401
		// (get) Token: 0x0601654D RID: 91469
		// (set) Token: 0x0601654C RID: 91468
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

		// Token: 0x170076C2 RID: 30402
		// (get) Token: 0x0601654F RID: 91471
		// (set) Token: 0x0601654E RID: 91470
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

		// Token: 0x170076C3 RID: 30403
		// (get) Token: 0x06016551 RID: 91473
		// (set) Token: 0x06016550 RID: 91472
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

		// Token: 0x170076C4 RID: 30404
		// (get) Token: 0x06016553 RID: 91475
		// (set) Token: 0x06016552 RID: 91474
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

		// Token: 0x170076C5 RID: 30405
		// (get) Token: 0x06016555 RID: 91477
		// (set) Token: 0x06016554 RID: 91476
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

		// Token: 0x170076C6 RID: 30406
		// (get) Token: 0x06016557 RID: 91479
		// (set) Token: 0x06016556 RID: 91478
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

		// Token: 0x170076C7 RID: 30407
		// (get) Token: 0x06016559 RID: 91481
		// (set) Token: 0x06016558 RID: 91480
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

		// Token: 0x170076C8 RID: 30408
		// (get) Token: 0x0601655B RID: 91483
		// (set) Token: 0x0601655A RID: 91482
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

		// Token: 0x170076C9 RID: 30409
		// (get) Token: 0x0601655D RID: 91485
		// (set) Token: 0x0601655C RID: 91484
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

		// Token: 0x170076CA RID: 30410
		// (get) Token: 0x0601655F RID: 91487
		// (set) Token: 0x0601655E RID: 91486
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

		// Token: 0x170076CB RID: 30411
		// (get) Token: 0x06016561 RID: 91489
		// (set) Token: 0x06016560 RID: 91488
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

		// Token: 0x170076CC RID: 30412
		// (get) Token: 0x06016563 RID: 91491
		// (set) Token: 0x06016562 RID: 91490
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

		// Token: 0x170076CD RID: 30413
		// (get) Token: 0x06016565 RID: 91493
		// (set) Token: 0x06016564 RID: 91492
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

		// Token: 0x170076CE RID: 30414
		// (get) Token: 0x06016567 RID: 91495
		// (set) Token: 0x06016566 RID: 91494
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

		// Token: 0x170076CF RID: 30415
		// (get) Token: 0x06016569 RID: 91497
		// (set) Token: 0x06016568 RID: 91496
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

		// Token: 0x0601656A RID: 91498
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLDocument2_elementFromPoint([In] int x, [In] int y);

		// Token: 0x170076D0 RID: 30416
		// (get) Token: 0x0601656B RID: 91499
		public virtual extern IHTMLWindow2 IHTMLDocument2_parentWindow
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076D1 RID: 30417
		// (get) Token: 0x0601656C RID: 91500
		public virtual extern HTMLStyleSheetsCollection IHTMLDocument2_styleSheets
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076D2 RID: 30418
		// (get) Token: 0x0601656E RID: 91502
		// (set) Token: 0x0601656D RID: 91501
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

		// Token: 0x170076D3 RID: 30419
		// (get) Token: 0x06016570 RID: 91504
		// (set) Token: 0x0601656F RID: 91503
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

		// Token: 0x06016571 RID: 91505
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLDocument2_toString();

		// Token: 0x06016572 RID: 91506
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLStyleSheet IHTMLDocument2_createStyleSheet([MarshalAs(19)] [In] string bstrHref = "", [In] int lIndex = -1);

		// Token: 0x06016573 RID: 91507
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument3_releaseCapture();

		// Token: 0x06016574 RID: 91508
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument3_recalc([In] bool fForce = false);

		// Token: 0x06016575 RID: 91509
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDocument3_createTextNode([MarshalAs(19)] [In] string text);

		// Token: 0x170076D4 RID: 30420
		// (get) Token: 0x06016576 RID: 91510
		public virtual extern IHTMLElement IHTMLDocument3_documentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076D5 RID: 30421
		// (get) Token: 0x06016577 RID: 91511
		public virtual extern string IHTMLDocument3_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06016578 RID: 91512
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDocument3_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06016579 RID: 91513
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDocument3_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170076D6 RID: 30422
		// (get) Token: 0x0601657B RID: 91515
		// (set) Token: 0x0601657A RID: 91514
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

		// Token: 0x170076D7 RID: 30423
		// (get) Token: 0x0601657D RID: 91517
		// (set) Token: 0x0601657C RID: 91516
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

		// Token: 0x170076D8 RID: 30424
		// (get) Token: 0x0601657F RID: 91519
		// (set) Token: 0x0601657E RID: 91518
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

		// Token: 0x170076D9 RID: 30425
		// (get) Token: 0x06016581 RID: 91521
		// (set) Token: 0x06016580 RID: 91520
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

		// Token: 0x170076DA RID: 30426
		// (get) Token: 0x06016583 RID: 91523
		// (set) Token: 0x06016582 RID: 91522
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

		// Token: 0x170076DB RID: 30427
		// (get) Token: 0x06016585 RID: 91525
		// (set) Token: 0x06016584 RID: 91524
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

		// Token: 0x170076DC RID: 30428
		// (get) Token: 0x06016587 RID: 91527
		// (set) Token: 0x06016586 RID: 91526
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

		// Token: 0x170076DD RID: 30429
		// (get) Token: 0x06016589 RID: 91529
		// (set) Token: 0x06016588 RID: 91528
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

		// Token: 0x170076DE RID: 30430
		// (get) Token: 0x0601658B RID: 91531
		// (set) Token: 0x0601658A RID: 91530
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

		// Token: 0x170076DF RID: 30431
		// (get) Token: 0x0601658D RID: 91533
		// (set) Token: 0x0601658C RID: 91532
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

		// Token: 0x0601658E RID: 91534
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDocument2 IHTMLDocument3_createDocumentFragment();

		// Token: 0x170076E0 RID: 30432
		// (get) Token: 0x0601658F RID: 91535
		public virtual extern IHTMLDocument2 IHTMLDocument3_parentDocument
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076E1 RID: 30433
		// (get) Token: 0x06016591 RID: 91537
		// (set) Token: 0x06016590 RID: 91536
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

		// Token: 0x170076E2 RID: 30434
		// (get) Token: 0x06016593 RID: 91539
		// (set) Token: 0x06016592 RID: 91538
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

		// Token: 0x170076E3 RID: 30435
		// (get) Token: 0x06016594 RID: 91540
		public virtual extern object IHTMLDocument3_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170076E4 RID: 30436
		// (get) Token: 0x06016596 RID: 91542
		// (set) Token: 0x06016595 RID: 91541
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

		// Token: 0x170076E5 RID: 30437
		// (get) Token: 0x06016598 RID: 91544
		// (set) Token: 0x06016597 RID: 91543
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

		// Token: 0x06016599 RID: 91545
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLDocument3_getElementsByName([MarshalAs(19)] [In] string v);

		// Token: 0x0601659A RID: 91546
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLDocument3_getElementById([MarshalAs(19)] [In] string v);

		// Token: 0x0601659B RID: 91547
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLDocument3_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x14002B08 RID: 11016
		// (add) Token: 0x0601659C RID: 91548
		// (remove) Token: 0x0601659D RID: 91549
		public virtual extern event HTMLDocumentEvents_onhelpEventHandler HTMLDocumentEvents_Event_onhelp;

		// Token: 0x14002B09 RID: 11017
		// (add) Token: 0x0601659E RID: 91550
		// (remove) Token: 0x0601659F RID: 91551
		public virtual extern event HTMLDocumentEvents_onclickEventHandler HTMLDocumentEvents_Event_onclick;

		// Token: 0x14002B0A RID: 11018
		// (add) Token: 0x060165A0 RID: 91552
		// (remove) Token: 0x060165A1 RID: 91553
		public virtual extern event HTMLDocumentEvents_ondblclickEventHandler HTMLDocumentEvents_Event_ondblclick;

		// Token: 0x14002B0B RID: 11019
		// (add) Token: 0x060165A2 RID: 91554
		// (remove) Token: 0x060165A3 RID: 91555
		public virtual extern event HTMLDocumentEvents_onkeydownEventHandler HTMLDocumentEvents_Event_onkeydown;

		// Token: 0x14002B0C RID: 11020
		// (add) Token: 0x060165A4 RID: 91556
		// (remove) Token: 0x060165A5 RID: 91557
		public virtual extern event HTMLDocumentEvents_onkeyupEventHandler HTMLDocumentEvents_Event_onkeyup;

		// Token: 0x14002B0D RID: 11021
		// (add) Token: 0x060165A6 RID: 91558
		// (remove) Token: 0x060165A7 RID: 91559
		public virtual extern event HTMLDocumentEvents_onkeypressEventHandler HTMLDocumentEvents_Event_onkeypress;

		// Token: 0x14002B0E RID: 11022
		// (add) Token: 0x060165A8 RID: 91560
		// (remove) Token: 0x060165A9 RID: 91561
		public virtual extern event HTMLDocumentEvents_onmousedownEventHandler HTMLDocumentEvents_Event_onmousedown;

		// Token: 0x14002B0F RID: 11023
		// (add) Token: 0x060165AA RID: 91562
		// (remove) Token: 0x060165AB RID: 91563
		public virtual extern event HTMLDocumentEvents_onmousemoveEventHandler HTMLDocumentEvents_Event_onmousemove;

		// Token: 0x14002B10 RID: 11024
		// (add) Token: 0x060165AC RID: 91564
		// (remove) Token: 0x060165AD RID: 91565
		public virtual extern event HTMLDocumentEvents_onmouseupEventHandler HTMLDocumentEvents_Event_onmouseup;

		// Token: 0x14002B11 RID: 11025
		// (add) Token: 0x060165AE RID: 91566
		// (remove) Token: 0x060165AF RID: 91567
		public virtual extern event HTMLDocumentEvents_onmouseoutEventHandler HTMLDocumentEvents_Event_onmouseout;

		// Token: 0x14002B12 RID: 11026
		// (add) Token: 0x060165B0 RID: 91568
		// (remove) Token: 0x060165B1 RID: 91569
		public virtual extern event HTMLDocumentEvents_onmouseoverEventHandler HTMLDocumentEvents_Event_onmouseover;

		// Token: 0x14002B13 RID: 11027
		// (add) Token: 0x060165B2 RID: 91570
		// (remove) Token: 0x060165B3 RID: 91571
		public virtual extern event HTMLDocumentEvents_onreadystatechangeEventHandler HTMLDocumentEvents_Event_onreadystatechange;

		// Token: 0x14002B14 RID: 11028
		// (add) Token: 0x060165B4 RID: 91572
		// (remove) Token: 0x060165B5 RID: 91573
		public virtual extern event HTMLDocumentEvents_onbeforeupdateEventHandler HTMLDocumentEvents_Event_onbeforeupdate;

		// Token: 0x14002B15 RID: 11029
		// (add) Token: 0x060165B6 RID: 91574
		// (remove) Token: 0x060165B7 RID: 91575
		public virtual extern event HTMLDocumentEvents_onafterupdateEventHandler HTMLDocumentEvents_Event_onafterupdate;

		// Token: 0x14002B16 RID: 11030
		// (add) Token: 0x060165B8 RID: 91576
		// (remove) Token: 0x060165B9 RID: 91577
		public virtual extern event HTMLDocumentEvents_onrowexitEventHandler HTMLDocumentEvents_Event_onrowexit;

		// Token: 0x14002B17 RID: 11031
		// (add) Token: 0x060165BA RID: 91578
		// (remove) Token: 0x060165BB RID: 91579
		public virtual extern event HTMLDocumentEvents_onrowenterEventHandler HTMLDocumentEvents_Event_onrowenter;

		// Token: 0x14002B18 RID: 11032
		// (add) Token: 0x060165BC RID: 91580
		// (remove) Token: 0x060165BD RID: 91581
		public virtual extern event HTMLDocumentEvents_ondragstartEventHandler HTMLDocumentEvents_Event_ondragstart;

		// Token: 0x14002B19 RID: 11033
		// (add) Token: 0x060165BE RID: 91582
		// (remove) Token: 0x060165BF RID: 91583
		public virtual extern event HTMLDocumentEvents_onselectstartEventHandler HTMLDocumentEvents_Event_onselectstart;

		// Token: 0x14002B1A RID: 11034
		// (add) Token: 0x060165C0 RID: 91584
		// (remove) Token: 0x060165C1 RID: 91585
		public virtual extern event HTMLDocumentEvents_onerrorupdateEventHandler HTMLDocumentEvents_Event_onerrorupdate;

		// Token: 0x14002B1B RID: 11035
		// (add) Token: 0x060165C2 RID: 91586
		// (remove) Token: 0x060165C3 RID: 91587
		public virtual extern event HTMLDocumentEvents_oncontextmenuEventHandler HTMLDocumentEvents_Event_oncontextmenu;

		// Token: 0x14002B1C RID: 11036
		// (add) Token: 0x060165C4 RID: 91588
		// (remove) Token: 0x060165C5 RID: 91589
		public virtual extern event HTMLDocumentEvents_onstopEventHandler HTMLDocumentEvents_Event_onstop;

		// Token: 0x14002B1D RID: 11037
		// (add) Token: 0x060165C6 RID: 91590
		// (remove) Token: 0x060165C7 RID: 91591
		public virtual extern event HTMLDocumentEvents_onrowsdeleteEventHandler HTMLDocumentEvents_Event_onrowsdelete;

		// Token: 0x14002B1E RID: 11038
		// (add) Token: 0x060165C8 RID: 91592
		// (remove) Token: 0x060165C9 RID: 91593
		public virtual extern event HTMLDocumentEvents_onrowsinsertedEventHandler HTMLDocumentEvents_Event_onrowsinserted;

		// Token: 0x14002B1F RID: 11039
		// (add) Token: 0x060165CA RID: 91594
		// (remove) Token: 0x060165CB RID: 91595
		public virtual extern event HTMLDocumentEvents_oncellchangeEventHandler HTMLDocumentEvents_Event_oncellchange;

		// Token: 0x14002B20 RID: 11040
		// (add) Token: 0x060165CC RID: 91596
		// (remove) Token: 0x060165CD RID: 91597
		public virtual extern event HTMLDocumentEvents_onpropertychangeEventHandler HTMLDocumentEvents_Event_onpropertychange;

		// Token: 0x14002B21 RID: 11041
		// (add) Token: 0x060165CE RID: 91598
		// (remove) Token: 0x060165CF RID: 91599
		public virtual extern event HTMLDocumentEvents_ondatasetchangedEventHandler HTMLDocumentEvents_Event_ondatasetchanged;

		// Token: 0x14002B22 RID: 11042
		// (add) Token: 0x060165D0 RID: 91600
		// (remove) Token: 0x060165D1 RID: 91601
		public virtual extern event HTMLDocumentEvents_ondataavailableEventHandler HTMLDocumentEvents_Event_ondataavailable;

		// Token: 0x14002B23 RID: 11043
		// (add) Token: 0x060165D2 RID: 91602
		// (remove) Token: 0x060165D3 RID: 91603
		public virtual extern event HTMLDocumentEvents_ondatasetcompleteEventHandler HTMLDocumentEvents_Event_ondatasetcomplete;

		// Token: 0x14002B24 RID: 11044
		// (add) Token: 0x060165D4 RID: 91604
		// (remove) Token: 0x060165D5 RID: 91605
		public virtual extern event HTMLDocumentEvents_onbeforeeditfocusEventHandler HTMLDocumentEvents_Event_onbeforeeditfocus;

		// Token: 0x14002B25 RID: 11045
		// (add) Token: 0x060165D6 RID: 91606
		// (remove) Token: 0x060165D7 RID: 91607
		public virtual extern event HTMLDocumentEvents_onselectionchangeEventHandler HTMLDocumentEvents_Event_onselectionchange;

		// Token: 0x14002B26 RID: 11046
		// (add) Token: 0x060165D8 RID: 91608
		// (remove) Token: 0x060165D9 RID: 91609
		public virtual extern event HTMLDocumentEvents_oncontrolselectEventHandler HTMLDocumentEvents_Event_oncontrolselect;

		// Token: 0x14002B27 RID: 11047
		// (add) Token: 0x060165DA RID: 91610
		// (remove) Token: 0x060165DB RID: 91611
		public virtual extern event HTMLDocumentEvents_onmousewheelEventHandler HTMLDocumentEvents_Event_onmousewheel;

		// Token: 0x14002B28 RID: 11048
		// (add) Token: 0x060165DC RID: 91612
		// (remove) Token: 0x060165DD RID: 91613
		public virtual extern event HTMLDocumentEvents_onfocusinEventHandler HTMLDocumentEvents_Event_onfocusin;

		// Token: 0x14002B29 RID: 11049
		// (add) Token: 0x060165DE RID: 91614
		// (remove) Token: 0x060165DF RID: 91615
		public virtual extern event HTMLDocumentEvents_onfocusoutEventHandler HTMLDocumentEvents_Event_onfocusout;

		// Token: 0x14002B2A RID: 11050
		// (add) Token: 0x060165E0 RID: 91616
		// (remove) Token: 0x060165E1 RID: 91617
		public virtual extern event HTMLDocumentEvents_onactivateEventHandler HTMLDocumentEvents_Event_onactivate;

		// Token: 0x14002B2B RID: 11051
		// (add) Token: 0x060165E2 RID: 91618
		// (remove) Token: 0x060165E3 RID: 91619
		public virtual extern event HTMLDocumentEvents_ondeactivateEventHandler HTMLDocumentEvents_Event_ondeactivate;

		// Token: 0x14002B2C RID: 11052
		// (add) Token: 0x060165E4 RID: 91620
		// (remove) Token: 0x060165E5 RID: 91621
		public virtual extern event HTMLDocumentEvents_onbeforeactivateEventHandler HTMLDocumentEvents_Event_onbeforeactivate;

		// Token: 0x14002B2D RID: 11053
		// (add) Token: 0x060165E6 RID: 91622
		// (remove) Token: 0x060165E7 RID: 91623
		public virtual extern event HTMLDocumentEvents_onbeforedeactivateEventHandler HTMLDocumentEvents_Event_onbeforedeactivate;
	}
}
