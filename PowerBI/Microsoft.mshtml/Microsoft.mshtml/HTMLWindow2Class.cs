using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007C2 RID: 1986
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLWindowEvents\0mshtml.HTMLWindowEvents2\0\0")]
	[DefaultMember("item")]
	[Guid("D48A6EC6-6A4A-11CF-94A7-444553540000")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLWindow2Class : DispHTMLWindow2, HTMLWindow2, HTMLWindowEvents_Event, IHTMLWindow2, IHTMLWindow3, IHTMLWindow4, HTMLWindowEvents2_Event
	{
		// Token: 0x0600D7C5 RID: 55237
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLWindow2Class();

		// Token: 0x0600D7C6 RID: 55238
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object item([MarshalAs(27)] [In] ref object pvarIndex);

		// Token: 0x170047EF RID: 18415
		// (get) Token: 0x0600D7C7 RID: 55239
		[DispId(1001)]
		public virtual extern int length
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047F0 RID: 18416
		// (get) Token: 0x0600D7C8 RID: 55240
		[DispId(1100)]
		public virtual extern FramesCollection frames
		{
			[DispId(1100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047F1 RID: 18417
		// (get) Token: 0x0600D7CA RID: 55242
		// (set) Token: 0x0600D7C9 RID: 55241
		[DispId(1101)]
		public virtual extern string defaultStatus
		{
			[DispId(1101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170047F2 RID: 18418
		// (get) Token: 0x0600D7CC RID: 55244
		// (set) Token: 0x0600D7CB RID: 55243
		[DispId(1102)]
		public virtual extern string status
		{
			[DispId(1102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600D7CD RID: 55245
		[DispId(1104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearTimeout([In] int timerID);

		// Token: 0x0600D7CE RID: 55246
		[DispId(1105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void alert([MarshalAs(19)] [In] string message = "");

		// Token: 0x0600D7CF RID: 55247
		[DispId(1110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool confirm([MarshalAs(19)] [In] string message = "");

		// Token: 0x0600D7D0 RID: 55248
		[DispId(1111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object prompt([MarshalAs(19)] [In] string message = "", [MarshalAs(19)] [In] string defstr = "undefined");

		// Token: 0x170047F3 RID: 18419
		// (get) Token: 0x0600D7D1 RID: 55249
		[DispId(1125)]
		public virtual extern HTMLImageElementFactory Image
		{
			[DispId(1125)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047F4 RID: 18420
		// (get) Token: 0x0600D7D2 RID: 55250
		[DispId(14)]
		public virtual extern HTMLLocation location
		{
			[DispId(14)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047F5 RID: 18421
		// (get) Token: 0x0600D7D3 RID: 55251
		[DispId(2)]
		public virtual extern HTMLHistory history
		{
			[DispId(2)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D7D4 RID: 55252
		[DispId(3)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void close();

		// Token: 0x170047F6 RID: 18422
		// (get) Token: 0x0600D7D6 RID: 55254
		// (set) Token: 0x0600D7D5 RID: 55253
		[DispId(4)]
		public virtual extern object opener
		{
			[DispId(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047F7 RID: 18423
		// (get) Token: 0x0600D7D7 RID: 55255
		[DispId(5)]
		public virtual extern HTMLNavigator navigator
		{
			[DispId(5)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047F8 RID: 18424
		// (get) Token: 0x0600D7D9 RID: 55257
		// (set) Token: 0x0600D7D8 RID: 55256
		[DispId(11)]
		public virtual extern string name
		{
			[DispId(11)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(11)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170047F9 RID: 18425
		// (get) Token: 0x0600D7DA RID: 55258
		[DispId(12)]
		public virtual extern IHTMLWindow2 parent
		{
			[DispId(12)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D7DB RID: 55259
		[DispId(13)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLWindow2 open([MarshalAs(19)] [In] string url = "", [MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string features = "", [In] bool replace = false);

		// Token: 0x170047FA RID: 18426
		// (get) Token: 0x0600D7DC RID: 55260
		[DispId(20)]
		public virtual extern IHTMLWindow2 self
		{
			[DispId(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047FB RID: 18427
		// (get) Token: 0x0600D7DD RID: 55261
		[DispId(21)]
		public virtual extern IHTMLWindow2 top
		{
			[DispId(21)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047FC RID: 18428
		// (get) Token: 0x0600D7DE RID: 55262
		[DispId(22)]
		public virtual extern IHTMLWindow2 window
		{
			[DispId(22)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D7DF RID: 55263
		[DispId(25)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void navigate([MarshalAs(19)] [In] string url);

		// Token: 0x170047FD RID: 18429
		// (get) Token: 0x0600D7E1 RID: 55265
		// (set) Token: 0x0600D7E0 RID: 55264
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

		// Token: 0x170047FE RID: 18430
		// (get) Token: 0x0600D7E3 RID: 55267
		// (set) Token: 0x0600D7E2 RID: 55266
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

		// Token: 0x170047FF RID: 18431
		// (get) Token: 0x0600D7E5 RID: 55269
		// (set) Token: 0x0600D7E4 RID: 55268
		[DispId(-2147412080)]
		public virtual extern object onload
		{
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004800 RID: 18432
		// (get) Token: 0x0600D7E7 RID: 55271
		// (set) Token: 0x0600D7E6 RID: 55270
		[DispId(-2147412073)]
		public virtual extern object onbeforeunload
		{
			[DispId(-2147412073)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412073)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004801 RID: 18433
		// (get) Token: 0x0600D7E9 RID: 55273
		// (set) Token: 0x0600D7E8 RID: 55272
		[DispId(-2147412079)]
		public virtual extern object onunload
		{
			[DispId(-2147412079)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412079)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004802 RID: 18434
		// (get) Token: 0x0600D7EB RID: 55275
		// (set) Token: 0x0600D7EA RID: 55274
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

		// Token: 0x17004803 RID: 18435
		// (get) Token: 0x0600D7ED RID: 55277
		// (set) Token: 0x0600D7EC RID: 55276
		[DispId(-2147412083)]
		public virtual extern object onerror
		{
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004804 RID: 18436
		// (get) Token: 0x0600D7EF RID: 55279
		// (set) Token: 0x0600D7EE RID: 55278
		[DispId(-2147412076)]
		public virtual extern object onresize
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

		// Token: 0x17004805 RID: 18437
		// (get) Token: 0x0600D7F1 RID: 55281
		// (set) Token: 0x0600D7F0 RID: 55280
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

		// Token: 0x17004806 RID: 18438
		// (get) Token: 0x0600D7F2 RID: 55282
		[DispId(1151)]
		public virtual extern IHTMLDocument2 document
		{
			[DispId(1151)]
			[TypeLibFunc(2)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004807 RID: 18439
		// (get) Token: 0x0600D7F3 RID: 55283
		[DispId(1152)]
		public virtual extern IHTMLEventObj @event
		{
			[DispId(1152)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004808 RID: 18440
		// (get) Token: 0x0600D7F4 RID: 55284
		[DispId(1153)]
		public virtual extern object _newEnum
		{
			[DispId(1153)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(25)]
			get;
		}

		// Token: 0x0600D7F5 RID: 55285
		[DispId(1154)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object showModalDialog([MarshalAs(19)] [In] string dialog, [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object varOptions);

		// Token: 0x0600D7F6 RID: 55286
		[DispId(1155)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void showHelp([MarshalAs(19)] [In] string helpURL, [MarshalAs(27)] [In] [Optional] object helpArg, [MarshalAs(19)] [In] string features = "");

		// Token: 0x17004809 RID: 18441
		// (get) Token: 0x0600D7F7 RID: 55287
		[DispId(1156)]
		public virtual extern IHTMLScreen screen
		{
			[DispId(1156)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700480A RID: 18442
		// (get) Token: 0x0600D7F8 RID: 55288
		[DispId(1157)]
		public virtual extern HTMLOptionElementFactory Option
		{
			[DispId(1157)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D7F9 RID: 55289
		[DispId(1158)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x1700480B RID: 18443
		// (get) Token: 0x0600D7FA RID: 55290
		[DispId(23)]
		public virtual extern bool closed
		{
			[DispId(23)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D7FB RID: 55291
		[DispId(1159)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600D7FC RID: 55292
		[DispId(1160)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scroll([In] int x, [In] int y);

		// Token: 0x1700480C RID: 18444
		// (get) Token: 0x0600D7FD RID: 55293
		[DispId(1161)]
		public virtual extern HTMLNavigator clientInformation
		{
			[DispId(1161)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D7FE RID: 55294
		[DispId(1163)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearInterval([In] int timerID);

		// Token: 0x1700480D RID: 18445
		// (get) Token: 0x0600D800 RID: 55296
		// (set) Token: 0x0600D7FF RID: 55295
		[DispId(1164)]
		public virtual extern object offscreenBuffering
		{
			[DispId(1164)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1164)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600D801 RID: 55297
		[DispId(1165)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object execScript([MarshalAs(19)] [In] string code, [MarshalAs(19)] [In] string language = "JScript");

		// Token: 0x0600D802 RID: 55298
		[DispId(1166)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x0600D803 RID: 55299
		[DispId(1167)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollBy([In] int x, [In] int y);

		// Token: 0x0600D804 RID: 55300
		[DispId(1168)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollTo([In] int x, [In] int y);

		// Token: 0x0600D805 RID: 55301
		[DispId(6)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void moveTo([In] int x, [In] int y);

		// Token: 0x0600D806 RID: 55302
		[DispId(7)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void moveBy([In] int x, [In] int y);

		// Token: 0x0600D807 RID: 55303
		[DispId(9)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void resizeTo([In] int x, [In] int y);

		// Token: 0x0600D808 RID: 55304
		[DispId(8)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void resizeBy([In] int x, [In] int y);

		// Token: 0x1700480E RID: 18446
		// (get) Token: 0x0600D809 RID: 55305
		[DispId(1169)]
		public virtual extern object external
		{
			[DispId(1169)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700480F RID: 18447
		// (get) Token: 0x0600D80A RID: 55306
		[DispId(1170)]
		public virtual extern int screenLeft
		{
			[DispId(1170)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004810 RID: 18448
		// (get) Token: 0x0600D80B RID: 55307
		[DispId(1171)]
		public virtual extern int screenTop
		{
			[DispId(1171)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D80C RID: 55308
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D80D RID: 55309
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D80E RID: 55310
		[DispId(1103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int setTimeout([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D80F RID: 55311
		[DispId(1162)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int setInterval([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D810 RID: 55312
		[DispId(1174)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void print();

		// Token: 0x17004811 RID: 18449
		// (get) Token: 0x0600D812 RID: 55314
		// (set) Token: 0x0600D811 RID: 55313
		[DispId(-2147412046)]
		public virtual extern object onbeforeprint
		{
			[TypeLibFunc(20)]
			[DispId(-2147412046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412046)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004812 RID: 18450
		// (get) Token: 0x0600D814 RID: 55316
		// (set) Token: 0x0600D813 RID: 55315
		[DispId(-2147412045)]
		public virtual extern object onafterprint
		{
			[TypeLibFunc(20)]
			[DispId(-2147412045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412045)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004813 RID: 18451
		// (get) Token: 0x0600D815 RID: 55317
		[DispId(1175)]
		public virtual extern IHTMLDataTransfer clipboardData
		{
			[DispId(1175)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D816 RID: 55318
		[DispId(1176)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLWindow2 showModelessDialog([MarshalAs(19)] [In] string url = "", [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object options);

		// Token: 0x0600D817 RID: 55319
		[DispId(1180)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createPopup([MarshalAs(27)] [In] [Optional] ref object varArgIn);

		// Token: 0x17004814 RID: 18452
		// (get) Token: 0x0600D818 RID: 55320
		[DispId(1181)]
		public virtual extern IHTMLFrameBase frameElement
		{
			[DispId(1181)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D819 RID: 55321
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLWindow2_item([MarshalAs(27)] [In] ref object pvarIndex);

		// Token: 0x17004815 RID: 18453
		// (get) Token: 0x0600D81A RID: 55322
		public virtual extern int IHTMLWindow2_length
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004816 RID: 18454
		// (get) Token: 0x0600D81B RID: 55323
		public virtual extern FramesCollection IHTMLWindow2_frames
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004817 RID: 18455
		// (get) Token: 0x0600D81D RID: 55325
		// (set) Token: 0x0600D81C RID: 55324
		public virtual extern string IHTMLWindow2_defaultStatus
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004818 RID: 18456
		// (get) Token: 0x0600D81F RID: 55327
		// (set) Token: 0x0600D81E RID: 55326
		public virtual extern string IHTMLWindow2_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600D820 RID: 55328
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int setTimeout([MarshalAs(19)] [In] string expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D821 RID: 55329
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_clearTimeout([In] int timerID);

		// Token: 0x0600D822 RID: 55330
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_alert([MarshalAs(19)] [In] string message = "");

		// Token: 0x0600D823 RID: 55331
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLWindow2_confirm([MarshalAs(19)] [In] string message = "");

		// Token: 0x0600D824 RID: 55332
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLWindow2_prompt([MarshalAs(19)] [In] string message = "", [MarshalAs(19)] [In] string defstr = "undefined");

		// Token: 0x17004819 RID: 18457
		// (get) Token: 0x0600D825 RID: 55333
		public virtual extern HTMLImageElementFactory IHTMLWindow2_Image
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700481A RID: 18458
		// (get) Token: 0x0600D826 RID: 55334
		public virtual extern HTMLLocation IHTMLWindow2_location
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700481B RID: 18459
		// (get) Token: 0x0600D827 RID: 55335
		public virtual extern HTMLHistory IHTMLWindow2_history
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D828 RID: 55336
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_close();

		// Token: 0x1700481C RID: 18460
		// (get) Token: 0x0600D82A RID: 55338
		// (set) Token: 0x0600D829 RID: 55337
		public virtual extern object IHTMLWindow2_opener
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700481D RID: 18461
		// (get) Token: 0x0600D82B RID: 55339
		public virtual extern HTMLNavigator IHTMLWindow2_navigator
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700481E RID: 18462
		// (get) Token: 0x0600D82D RID: 55341
		// (set) Token: 0x0600D82C RID: 55340
		public virtual extern string IHTMLWindow2_name
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700481F RID: 18463
		// (get) Token: 0x0600D82E RID: 55342
		public virtual extern IHTMLWindow2 IHTMLWindow2_parent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D82F RID: 55343
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLWindow2 IHTMLWindow2_open([MarshalAs(19)] [In] string url = "", [MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string features = "", [In] bool replace = false);

		// Token: 0x17004820 RID: 18464
		// (get) Token: 0x0600D830 RID: 55344
		public virtual extern IHTMLWindow2 IHTMLWindow2_self
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004821 RID: 18465
		// (get) Token: 0x0600D831 RID: 55345
		public virtual extern IHTMLWindow2 IHTMLWindow2_top
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004822 RID: 18466
		// (get) Token: 0x0600D832 RID: 55346
		public virtual extern IHTMLWindow2 IHTMLWindow2_window
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D833 RID: 55347
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_navigate([MarshalAs(19)] [In] string url);

		// Token: 0x17004823 RID: 18467
		// (get) Token: 0x0600D835 RID: 55349
		// (set) Token: 0x0600D834 RID: 55348
		public virtual extern object IHTMLWindow2_onfocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004824 RID: 18468
		// (get) Token: 0x0600D837 RID: 55351
		// (set) Token: 0x0600D836 RID: 55350
		public virtual extern object IHTMLWindow2_onblur
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004825 RID: 18469
		// (get) Token: 0x0600D839 RID: 55353
		// (set) Token: 0x0600D838 RID: 55352
		public virtual extern object IHTMLWindow2_onload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004826 RID: 18470
		// (get) Token: 0x0600D83B RID: 55355
		// (set) Token: 0x0600D83A RID: 55354
		public virtual extern object IHTMLWindow2_onbeforeunload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004827 RID: 18471
		// (get) Token: 0x0600D83D RID: 55357
		// (set) Token: 0x0600D83C RID: 55356
		public virtual extern object IHTMLWindow2_onunload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004828 RID: 18472
		// (get) Token: 0x0600D83F RID: 55359
		// (set) Token: 0x0600D83E RID: 55358
		public virtual extern object IHTMLWindow2_onhelp
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004829 RID: 18473
		// (get) Token: 0x0600D841 RID: 55361
		// (set) Token: 0x0600D840 RID: 55360
		public virtual extern object IHTMLWindow2_onerror
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700482A RID: 18474
		// (get) Token: 0x0600D843 RID: 55363
		// (set) Token: 0x0600D842 RID: 55362
		public virtual extern object IHTMLWindow2_onresize
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700482B RID: 18475
		// (get) Token: 0x0600D845 RID: 55365
		// (set) Token: 0x0600D844 RID: 55364
		public virtual extern object IHTMLWindow2_onscroll
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700482C RID: 18476
		// (get) Token: 0x0600D846 RID: 55366
		public virtual extern IHTMLDocument2 IHTMLWindow2_document
		{
			[TypeLibFunc(2)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700482D RID: 18477
		// (get) Token: 0x0600D847 RID: 55367
		public virtual extern IHTMLEventObj IHTMLWindow2_event
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700482E RID: 18478
		// (get) Token: 0x0600D848 RID: 55368
		public virtual extern object IHTMLWindow2__newEnum
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(25)]
			get;
		}

		// Token: 0x0600D849 RID: 55369
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLWindow2_showModalDialog([MarshalAs(19)] [In] string dialog, [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object varOptions);

		// Token: 0x0600D84A RID: 55370
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_showHelp([MarshalAs(19)] [In] string helpURL, [MarshalAs(27)] [In] [Optional] object helpArg, [MarshalAs(19)] [In] string features = "");

		// Token: 0x1700482F RID: 18479
		// (get) Token: 0x0600D84B RID: 55371
		public virtual extern IHTMLScreen IHTMLWindow2_screen
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004830 RID: 18480
		// (get) Token: 0x0600D84C RID: 55372
		public virtual extern HTMLOptionElementFactory IHTMLWindow2_Option
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D84D RID: 55373
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_focus();

		// Token: 0x17004831 RID: 18481
		// (get) Token: 0x0600D84E RID: 55374
		public virtual extern bool IHTMLWindow2_closed
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D84F RID: 55375
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_blur();

		// Token: 0x0600D850 RID: 55376
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_scroll([In] int x, [In] int y);

		// Token: 0x17004832 RID: 18482
		// (get) Token: 0x0600D851 RID: 55377
		public virtual extern HTMLNavigator IHTMLWindow2_clientInformation
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D852 RID: 55378
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int setInterval([MarshalAs(19)] [In] string expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D853 RID: 55379
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_clearInterval([In] int timerID);

		// Token: 0x17004833 RID: 18483
		// (get) Token: 0x0600D855 RID: 55381
		// (set) Token: 0x0600D854 RID: 55380
		public virtual extern object IHTMLWindow2_offscreenBuffering
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600D856 RID: 55382
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLWindow2_execScript([MarshalAs(19)] [In] string code, [MarshalAs(19)] [In] string language = "JScript");

		// Token: 0x0600D857 RID: 55383
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLWindow2_toString();

		// Token: 0x0600D858 RID: 55384
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_scrollBy([In] int x, [In] int y);

		// Token: 0x0600D859 RID: 55385
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_scrollTo([In] int x, [In] int y);

		// Token: 0x0600D85A RID: 55386
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_moveTo([In] int x, [In] int y);

		// Token: 0x0600D85B RID: 55387
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_moveBy([In] int x, [In] int y);

		// Token: 0x0600D85C RID: 55388
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_resizeTo([In] int x, [In] int y);

		// Token: 0x0600D85D RID: 55389
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow2_resizeBy([In] int x, [In] int y);

		// Token: 0x17004834 RID: 18484
		// (get) Token: 0x0600D85E RID: 55390
		public virtual extern object IHTMLWindow2_external
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004835 RID: 18485
		// (get) Token: 0x0600D85F RID: 55391
		public virtual extern int IHTMLWindow3_screenLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004836 RID: 18486
		// (get) Token: 0x0600D860 RID: 55392
		public virtual extern int IHTMLWindow3_screenTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D861 RID: 55393
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLWindow3_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D862 RID: 55394
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow3_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D863 RID: 55395
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int setTimeout([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D864 RID: 55396
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int setInterval([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D865 RID: 55397
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLWindow3_print();

		// Token: 0x17004837 RID: 18487
		// (get) Token: 0x0600D867 RID: 55399
		// (set) Token: 0x0600D866 RID: 55398
		public virtual extern object IHTMLWindow3_onbeforeprint
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004838 RID: 18488
		// (get) Token: 0x0600D869 RID: 55401
		// (set) Token: 0x0600D868 RID: 55400
		public virtual extern object IHTMLWindow3_onafterprint
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004839 RID: 18489
		// (get) Token: 0x0600D86A RID: 55402
		public virtual extern IHTMLDataTransfer IHTMLWindow3_clipboardData
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D86B RID: 55403
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLWindow2 IHTMLWindow3_showModelessDialog([MarshalAs(19)] [In] string url = "", [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object options);

		// Token: 0x0600D86C RID: 55404
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLWindow4_createPopup([MarshalAs(27)] [In] [Optional] ref object varArgIn);

		// Token: 0x1700483A RID: 18490
		// (get) Token: 0x0600D86D RID: 55405
		public virtual extern IHTMLFrameBase IHTMLWindow4_frameElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x14001A1B RID: 6683
		// (add) Token: 0x0600D86E RID: 55406
		// (remove) Token: 0x0600D86F RID: 55407
		public virtual extern event HTMLWindowEvents_onloadEventHandler HTMLWindowEvents_Event_onload;

		// Token: 0x14001A1C RID: 6684
		// (add) Token: 0x0600D870 RID: 55408
		// (remove) Token: 0x0600D871 RID: 55409
		public virtual extern event HTMLWindowEvents_onunloadEventHandler HTMLWindowEvents_Event_onunload;

		// Token: 0x14001A1D RID: 6685
		// (add) Token: 0x0600D872 RID: 55410
		// (remove) Token: 0x0600D873 RID: 55411
		public virtual extern event HTMLWindowEvents_onhelpEventHandler HTMLWindowEvents_Event_onhelp;

		// Token: 0x14001A1E RID: 6686
		// (add) Token: 0x0600D874 RID: 55412
		// (remove) Token: 0x0600D875 RID: 55413
		public virtual extern event HTMLWindowEvents_onfocusEventHandler HTMLWindowEvents_Event_onfocus;

		// Token: 0x14001A1F RID: 6687
		// (add) Token: 0x0600D876 RID: 55414
		// (remove) Token: 0x0600D877 RID: 55415
		public virtual extern event HTMLWindowEvents_onblurEventHandler HTMLWindowEvents_Event_onblur;

		// Token: 0x14001A20 RID: 6688
		// (add) Token: 0x0600D878 RID: 55416
		// (remove) Token: 0x0600D879 RID: 55417
		public virtual extern event HTMLWindowEvents_onerrorEventHandler HTMLWindowEvents_Event_onerror;

		// Token: 0x14001A21 RID: 6689
		// (add) Token: 0x0600D87A RID: 55418
		// (remove) Token: 0x0600D87B RID: 55419
		public virtual extern event HTMLWindowEvents_onresizeEventHandler HTMLWindowEvents_Event_onresize;

		// Token: 0x14001A22 RID: 6690
		// (add) Token: 0x0600D87C RID: 55420
		// (remove) Token: 0x0600D87D RID: 55421
		public virtual extern event HTMLWindowEvents_onscrollEventHandler HTMLWindowEvents_Event_onscroll;

		// Token: 0x14001A23 RID: 6691
		// (add) Token: 0x0600D87E RID: 55422
		// (remove) Token: 0x0600D87F RID: 55423
		public virtual extern event HTMLWindowEvents_onbeforeunloadEventHandler HTMLWindowEvents_Event_onbeforeunload;

		// Token: 0x14001A24 RID: 6692
		// (add) Token: 0x0600D880 RID: 55424
		// (remove) Token: 0x0600D881 RID: 55425
		public virtual extern event HTMLWindowEvents_onbeforeprintEventHandler HTMLWindowEvents_Event_onbeforeprint;

		// Token: 0x14001A25 RID: 6693
		// (add) Token: 0x0600D882 RID: 55426
		// (remove) Token: 0x0600D883 RID: 55427
		public virtual extern event HTMLWindowEvents_onafterprintEventHandler HTMLWindowEvents_Event_onafterprint;

		// Token: 0x14001A26 RID: 6694
		// (add) Token: 0x0600D884 RID: 55428
		// (remove) Token: 0x0600D885 RID: 55429
		public virtual extern event HTMLWindowEvents2_onloadEventHandler HTMLWindowEvents2_Event_onload;

		// Token: 0x14001A27 RID: 6695
		// (add) Token: 0x0600D886 RID: 55430
		// (remove) Token: 0x0600D887 RID: 55431
		public virtual extern event HTMLWindowEvents2_onunloadEventHandler HTMLWindowEvents2_Event_onunload;

		// Token: 0x14001A28 RID: 6696
		// (add) Token: 0x0600D888 RID: 55432
		// (remove) Token: 0x0600D889 RID: 55433
		public virtual extern event HTMLWindowEvents2_onhelpEventHandler HTMLWindowEvents2_Event_onhelp;

		// Token: 0x14001A29 RID: 6697
		// (add) Token: 0x0600D88A RID: 55434
		// (remove) Token: 0x0600D88B RID: 55435
		public virtual extern event HTMLWindowEvents2_onfocusEventHandler HTMLWindowEvents2_Event_onfocus;

		// Token: 0x14001A2A RID: 6698
		// (add) Token: 0x0600D88C RID: 55436
		// (remove) Token: 0x0600D88D RID: 55437
		public virtual extern event HTMLWindowEvents2_onblurEventHandler HTMLWindowEvents2_Event_onblur;

		// Token: 0x14001A2B RID: 6699
		// (add) Token: 0x0600D88E RID: 55438
		// (remove) Token: 0x0600D88F RID: 55439
		public virtual extern event HTMLWindowEvents2_onerrorEventHandler HTMLWindowEvents2_Event_onerror;

		// Token: 0x14001A2C RID: 6700
		// (add) Token: 0x0600D890 RID: 55440
		// (remove) Token: 0x0600D891 RID: 55441
		public virtual extern event HTMLWindowEvents2_onresizeEventHandler HTMLWindowEvents2_Event_onresize;

		// Token: 0x14001A2D RID: 6701
		// (add) Token: 0x0600D892 RID: 55442
		// (remove) Token: 0x0600D893 RID: 55443
		public virtual extern event HTMLWindowEvents2_onscrollEventHandler HTMLWindowEvents2_Event_onscroll;

		// Token: 0x14001A2E RID: 6702
		// (add) Token: 0x0600D894 RID: 55444
		// (remove) Token: 0x0600D895 RID: 55445
		public virtual extern event HTMLWindowEvents2_onbeforeunloadEventHandler HTMLWindowEvents2_Event_onbeforeunload;

		// Token: 0x14001A2F RID: 6703
		// (add) Token: 0x0600D896 RID: 55446
		// (remove) Token: 0x0600D897 RID: 55447
		public virtual extern event HTMLWindowEvents2_onbeforeprintEventHandler HTMLWindowEvents2_Event_onbeforeprint;

		// Token: 0x14001A30 RID: 6704
		// (add) Token: 0x0600D898 RID: 55448
		// (remove) Token: 0x0600D899 RID: 55449
		public virtual extern event HTMLWindowEvents2_onafterprintEventHandler HTMLWindowEvents2_Event_onafterprint;
	}
}
