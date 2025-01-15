using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000583 RID: 1411
	[InterfaceType(2)]
	[Guid("3050F57D-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLInputElement
	{
		// Token: 0x06008DAC RID: 36268
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06008DAD RID: 36269
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06008DAE RID: 36270
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17002F06 RID: 12038
		// (get) Token: 0x06008DB0 RID: 36272
		// (set) Token: 0x06008DAF RID: 36271
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

		// Token: 0x17002F07 RID: 12039
		// (get) Token: 0x06008DB2 RID: 36274
		// (set) Token: 0x06008DB1 RID: 36273
		[DispId(-2147417110)]
		string id
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

		// Token: 0x17002F08 RID: 12040
		// (get) Token: 0x06008DB3 RID: 36275
		[DispId(-2147417108)]
		string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002F09 RID: 12041
		// (get) Token: 0x06008DB4 RID: 36276
		[DispId(-2147418104)]
		IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F0A RID: 12042
		// (get) Token: 0x06008DB5 RID: 36277
		[DispId(-2147418038)]
		IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F0B RID: 12043
		// (get) Token: 0x06008DB7 RID: 36279
		// (set) Token: 0x06008DB6 RID: 36278
		[DispId(-2147412099)]
		object onhelp
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

		// Token: 0x17002F0C RID: 12044
		// (get) Token: 0x06008DB9 RID: 36281
		// (set) Token: 0x06008DB8 RID: 36280
		[DispId(-2147412104)]
		object onclick
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

		// Token: 0x17002F0D RID: 12045
		// (get) Token: 0x06008DBB RID: 36283
		// (set) Token: 0x06008DBA RID: 36282
		[DispId(-2147412103)]
		object ondblclick
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

		// Token: 0x17002F0E RID: 12046
		// (get) Token: 0x06008DBD RID: 36285
		// (set) Token: 0x06008DBC RID: 36284
		[DispId(-2147412107)]
		object onkeydown
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

		// Token: 0x17002F0F RID: 12047
		// (get) Token: 0x06008DBF RID: 36287
		// (set) Token: 0x06008DBE RID: 36286
		[DispId(-2147412106)]
		object onkeyup
		{
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002F10 RID: 12048
		// (get) Token: 0x06008DC1 RID: 36289
		// (set) Token: 0x06008DC0 RID: 36288
		[DispId(-2147412105)]
		object onkeypress
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

		// Token: 0x17002F11 RID: 12049
		// (get) Token: 0x06008DC3 RID: 36291
		// (set) Token: 0x06008DC2 RID: 36290
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

		// Token: 0x17002F12 RID: 12050
		// (get) Token: 0x06008DC5 RID: 36293
		// (set) Token: 0x06008DC4 RID: 36292
		[DispId(-2147412112)]
		object onmouseover
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

		// Token: 0x17002F13 RID: 12051
		// (get) Token: 0x06008DC7 RID: 36295
		// (set) Token: 0x06008DC6 RID: 36294
		[DispId(-2147412108)]
		object onmousemove
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

		// Token: 0x17002F14 RID: 12052
		// (get) Token: 0x06008DC9 RID: 36297
		// (set) Token: 0x06008DC8 RID: 36296
		[DispId(-2147412110)]
		object onmousedown
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

		// Token: 0x17002F15 RID: 12053
		// (get) Token: 0x06008DCB RID: 36299
		// (set) Token: 0x06008DCA RID: 36298
		[DispId(-2147412109)]
		object onmouseup
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

		// Token: 0x17002F16 RID: 12054
		// (get) Token: 0x06008DCC RID: 36300
		[DispId(-2147417094)]
		object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002F17 RID: 12055
		// (get) Token: 0x06008DCE RID: 36302
		// (set) Token: 0x06008DCD RID: 36301
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

		// Token: 0x17002F18 RID: 12056
		// (get) Token: 0x06008DD0 RID: 36304
		// (set) Token: 0x06008DCF RID: 36303
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

		// Token: 0x17002F19 RID: 12057
		// (get) Token: 0x06008DD2 RID: 36306
		// (set) Token: 0x06008DD1 RID: 36305
		[DispId(-2147412075)]
		object onselectstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06008DD3 RID: 36307
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06008DD4 RID: 36308
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17002F1A RID: 12058
		// (get) Token: 0x06008DD5 RID: 36309
		[DispId(-2147417088)]
		int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F1B RID: 12059
		// (get) Token: 0x06008DD6 RID: 36310
		[DispId(-2147417087)]
		object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002F1C RID: 12060
		// (get) Token: 0x06008DD8 RID: 36312
		// (set) Token: 0x06008DD7 RID: 36311
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

		// Token: 0x17002F1D RID: 12061
		// (get) Token: 0x06008DD9 RID: 36313
		[DispId(-2147417104)]
		int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F1E RID: 12062
		// (get) Token: 0x06008DDA RID: 36314
		[DispId(-2147417103)]
		int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F1F RID: 12063
		// (get) Token: 0x06008DDB RID: 36315
		[DispId(-2147417102)]
		int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F20 RID: 12064
		// (get) Token: 0x06008DDC RID: 36316
		[DispId(-2147417101)]
		int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F21 RID: 12065
		// (get) Token: 0x06008DDD RID: 36317
		[DispId(-2147417100)]
		IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F22 RID: 12066
		// (get) Token: 0x06008DDF RID: 36319
		// (set) Token: 0x06008DDE RID: 36318
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

		// Token: 0x17002F23 RID: 12067
		// (get) Token: 0x06008DE1 RID: 36321
		// (set) Token: 0x06008DE0 RID: 36320
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

		// Token: 0x17002F24 RID: 12068
		// (get) Token: 0x06008DE3 RID: 36323
		// (set) Token: 0x06008DE2 RID: 36322
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

		// Token: 0x17002F25 RID: 12069
		// (get) Token: 0x06008DE5 RID: 36325
		// (set) Token: 0x06008DE4 RID: 36324
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

		// Token: 0x06008DE6 RID: 36326
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06008DE7 RID: 36327
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17002F26 RID: 12070
		// (get) Token: 0x06008DE8 RID: 36328
		[DispId(-2147417080)]
		IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F27 RID: 12071
		// (get) Token: 0x06008DE9 RID: 36329
		[DispId(-2147417078)]
		bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008DEA RID: 36330
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void click();

		// Token: 0x17002F28 RID: 12072
		// (get) Token: 0x06008DEB RID: 36331
		[DispId(-2147417077)]
		IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F29 RID: 12073
		// (get) Token: 0x06008DED RID: 36333
		// (set) Token: 0x06008DEC RID: 36332
		[DispId(-2147412077)]
		object ondragstart
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

		// Token: 0x06008DEE RID: 36334
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x17002F2A RID: 12074
		// (get) Token: 0x06008DF0 RID: 36336
		// (set) Token: 0x06008DEF RID: 36335
		[DispId(-2147412091)]
		object onbeforeupdate
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

		// Token: 0x17002F2B RID: 12075
		// (get) Token: 0x06008DF2 RID: 36338
		// (set) Token: 0x06008DF1 RID: 36337
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

		// Token: 0x17002F2C RID: 12076
		// (get) Token: 0x06008DF4 RID: 36340
		// (set) Token: 0x06008DF3 RID: 36339
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

		// Token: 0x17002F2D RID: 12077
		// (get) Token: 0x06008DF6 RID: 36342
		// (set) Token: 0x06008DF5 RID: 36341
		[DispId(-2147412094)]
		object onrowexit
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

		// Token: 0x17002F2E RID: 12078
		// (get) Token: 0x06008DF8 RID: 36344
		// (set) Token: 0x06008DF7 RID: 36343
		[DispId(-2147412093)]
		object onrowenter
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

		// Token: 0x17002F2F RID: 12079
		// (get) Token: 0x06008DFA RID: 36346
		// (set) Token: 0x06008DF9 RID: 36345
		[DispId(-2147412072)]
		object ondatasetchanged
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

		// Token: 0x17002F30 RID: 12080
		// (get) Token: 0x06008DFC RID: 36348
		// (set) Token: 0x06008DFB RID: 36347
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

		// Token: 0x17002F31 RID: 12081
		// (get) Token: 0x06008DFE RID: 36350
		// (set) Token: 0x06008DFD RID: 36349
		[DispId(-2147412070)]
		object ondatasetcomplete
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

		// Token: 0x17002F32 RID: 12082
		// (get) Token: 0x06008E00 RID: 36352
		// (set) Token: 0x06008DFF RID: 36351
		[DispId(-2147412069)]
		object onfilterchange
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

		// Token: 0x17002F33 RID: 12083
		// (get) Token: 0x06008E01 RID: 36353
		[DispId(-2147417075)]
		object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002F34 RID: 12084
		// (get) Token: 0x06008E02 RID: 36354
		[DispId(-2147417074)]
		object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002F35 RID: 12085
		// (get) Token: 0x06008E03 RID: 36355
		[DispId(-2147417073)]
		string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06008E04 RID: 36356
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setCapture([In] bool containerCapture = true);

		// Token: 0x06008E05 RID: 36357
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void releaseCapture();

		// Token: 0x17002F36 RID: 12086
		// (get) Token: 0x06008E07 RID: 36359
		// (set) Token: 0x06008E06 RID: 36358
		[DispId(-2147412066)]
		object onlosecapture
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

		// Token: 0x06008E08 RID: 36360
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06008E09 RID: 36361
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17002F37 RID: 12087
		// (get) Token: 0x06008E0B RID: 36363
		// (set) Token: 0x06008E0A RID: 36362
		[DispId(-2147412081)]
		object onscroll
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

		// Token: 0x17002F38 RID: 12088
		// (get) Token: 0x06008E0D RID: 36365
		// (set) Token: 0x06008E0C RID: 36364
		[DispId(-2147412063)]
		object ondrag
		{
			[TypeLibFunc(20)]
			[DispId(-2147412063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002F39 RID: 12089
		// (get) Token: 0x06008E0F RID: 36367
		// (set) Token: 0x06008E0E RID: 36366
		[DispId(-2147412062)]
		object ondragend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002F3A RID: 12090
		// (get) Token: 0x06008E11 RID: 36369
		// (set) Token: 0x06008E10 RID: 36368
		[DispId(-2147412061)]
		object ondragenter
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

		// Token: 0x17002F3B RID: 12091
		// (get) Token: 0x06008E13 RID: 36371
		// (set) Token: 0x06008E12 RID: 36370
		[DispId(-2147412060)]
		object ondragover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002F3C RID: 12092
		// (get) Token: 0x06008E15 RID: 36373
		// (set) Token: 0x06008E14 RID: 36372
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

		// Token: 0x17002F3D RID: 12093
		// (get) Token: 0x06008E17 RID: 36375
		// (set) Token: 0x06008E16 RID: 36374
		[DispId(-2147412058)]
		object ondrop
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

		// Token: 0x17002F3E RID: 12094
		// (get) Token: 0x06008E19 RID: 36377
		// (set) Token: 0x06008E18 RID: 36376
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

		// Token: 0x17002F3F RID: 12095
		// (get) Token: 0x06008E1B RID: 36379
		// (set) Token: 0x06008E1A RID: 36378
		[DispId(-2147412057)]
		object oncut
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

		// Token: 0x17002F40 RID: 12096
		// (get) Token: 0x06008E1D RID: 36381
		// (set) Token: 0x06008E1C RID: 36380
		[DispId(-2147412053)]
		object onbeforecopy
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

		// Token: 0x17002F41 RID: 12097
		// (get) Token: 0x06008E1F RID: 36383
		// (set) Token: 0x06008E1E RID: 36382
		[DispId(-2147412056)]
		object oncopy
		{
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002F42 RID: 12098
		// (get) Token: 0x06008E21 RID: 36385
		// (set) Token: 0x06008E20 RID: 36384
		[DispId(-2147412052)]
		object onbeforepaste
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

		// Token: 0x17002F43 RID: 12099
		// (get) Token: 0x06008E23 RID: 36387
		// (set) Token: 0x06008E22 RID: 36386
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

		// Token: 0x17002F44 RID: 12100
		// (get) Token: 0x06008E24 RID: 36388
		[DispId(-2147417105)]
		IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F45 RID: 12101
		// (get) Token: 0x06008E26 RID: 36390
		// (set) Token: 0x06008E25 RID: 36389
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

		// Token: 0x06008E27 RID: 36391
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRectCollection getClientRects();

		// Token: 0x06008E28 RID: 36392
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRect getBoundingClientRect();

		// Token: 0x06008E29 RID: 36393
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06008E2A RID: 36394
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06008E2B RID: 36395
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17002F46 RID: 12102
		// (get) Token: 0x06008E2D RID: 36397
		// (set) Token: 0x06008E2C RID: 36396
		[DispId(-2147418097)]
		short tabIndex
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

		// Token: 0x06008E2E RID: 36398
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void focus();

		// Token: 0x17002F47 RID: 12103
		// (get) Token: 0x06008E30 RID: 36400
		// (set) Token: 0x06008E2F RID: 36399
		[DispId(-2147416107)]
		string accessKey
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

		// Token: 0x17002F48 RID: 12104
		// (get) Token: 0x06008E32 RID: 36402
		// (set) Token: 0x06008E31 RID: 36401
		[DispId(-2147412097)]
		object onblur
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

		// Token: 0x17002F49 RID: 12105
		// (get) Token: 0x06008E34 RID: 36404
		// (set) Token: 0x06008E33 RID: 36403
		[DispId(-2147412098)]
		object onfocus
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

		// Token: 0x17002F4A RID: 12106
		// (get) Token: 0x06008E36 RID: 36406
		// (set) Token: 0x06008E35 RID: 36405
		[DispId(-2147412076)]
		object onresize
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

		// Token: 0x06008E37 RID: 36407
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void blur();

		// Token: 0x06008E38 RID: 36408
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06008E39 RID: 36409
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17002F4B RID: 12107
		// (get) Token: 0x06008E3A RID: 36410
		[DispId(-2147416093)]
		int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F4C RID: 12108
		// (get) Token: 0x06008E3B RID: 36411
		[DispId(-2147416092)]
		int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F4D RID: 12109
		// (get) Token: 0x06008E3C RID: 36412
		[DispId(-2147416091)]
		int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F4E RID: 12110
		// (get) Token: 0x06008E3D RID: 36413
		[DispId(-2147416090)]
		int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008E3E RID: 36414
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06008E3F RID: 36415
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17002F4F RID: 12111
		// (get) Token: 0x06008E40 RID: 36416
		[DispId(-2147412996)]
		object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002F50 RID: 12112
		// (get) Token: 0x06008E42 RID: 36418
		// (set) Token: 0x06008E41 RID: 36417
		[DispId(-2147412087)]
		object onreadystatechange
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

		// Token: 0x17002F51 RID: 12113
		// (get) Token: 0x06008E44 RID: 36420
		// (set) Token: 0x06008E43 RID: 36419
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

		// Token: 0x17002F52 RID: 12114
		// (get) Token: 0x06008E46 RID: 36422
		// (set) Token: 0x06008E45 RID: 36421
		[DispId(-2147412049)]
		object onrowsinserted
		{
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002F53 RID: 12115
		// (get) Token: 0x06008E48 RID: 36424
		// (set) Token: 0x06008E47 RID: 36423
		[DispId(-2147412048)]
		object oncellchange
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

		// Token: 0x17002F54 RID: 12116
		// (get) Token: 0x06008E4A RID: 36426
		// (set) Token: 0x06008E49 RID: 36425
		[DispId(-2147412995)]
		string dir
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

		// Token: 0x06008E4B RID: 36427
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createControlRange();

		// Token: 0x17002F55 RID: 12117
		// (get) Token: 0x06008E4C RID: 36428
		[DispId(-2147417055)]
		int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F56 RID: 12118
		// (get) Token: 0x06008E4D RID: 36429
		[DispId(-2147417054)]
		int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F57 RID: 12119
		// (get) Token: 0x06008E4F RID: 36431
		// (set) Token: 0x06008E4E RID: 36430
		[DispId(-2147417053)]
		int scrollTop
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

		// Token: 0x17002F58 RID: 12120
		// (get) Token: 0x06008E51 RID: 36433
		// (set) Token: 0x06008E50 RID: 36432
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

		// Token: 0x06008E52 RID: 36434
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void clearAttributes();

		// Token: 0x17002F59 RID: 12121
		// (get) Token: 0x06008E54 RID: 36436
		// (set) Token: 0x06008E53 RID: 36435
		[DispId(-2147412047)]
		object oncontextmenu
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

		// Token: 0x06008E55 RID: 36437
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06008E56 RID: 36438
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06008E57 RID: 36439
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06008E58 RID: 36440
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17002F5A RID: 12122
		// (get) Token: 0x06008E59 RID: 36441
		[DispId(-2147417040)]
		bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008E5A RID: 36442
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06008E5B RID: 36443
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeBehavior([In] int cookie);

		// Token: 0x17002F5B RID: 12123
		// (get) Token: 0x06008E5C RID: 36444
		[DispId(-2147417048)]
		IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F5C RID: 12124
		// (get) Token: 0x06008E5D RID: 36445
		[DispId(-2147417030)]
		object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002F5D RID: 12125
		// (get) Token: 0x06008E5F RID: 36447
		// (set) Token: 0x06008E5E RID: 36446
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

		// Token: 0x17002F5E RID: 12126
		// (get) Token: 0x06008E61 RID: 36449
		// (set) Token: 0x06008E60 RID: 36448
		[DispId(-2147412043)]
		object onbeforeeditfocus
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

		// Token: 0x17002F5F RID: 12127
		// (get) Token: 0x06008E62 RID: 36450
		[DispId(-2147417028)]
		int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008E63 RID: 36451
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06008E64 RID: 36452
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17002F60 RID: 12128
		// (get) Token: 0x06008E65 RID: 36453
		[DispId(-2147417015)]
		bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F61 RID: 12129
		// (get) Token: 0x06008E66 RID: 36454
		[DispId(-2147417014)]
		bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F62 RID: 12130
		// (get) Token: 0x06008E68 RID: 36456
		// (set) Token: 0x06008E67 RID: 36455
		[DispId(-2147412039)]
		object onlayoutcomplete
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

		// Token: 0x17002F63 RID: 12131
		// (get) Token: 0x06008E6A RID: 36458
		// (set) Token: 0x06008E69 RID: 36457
		[DispId(-2147412038)]
		object onpage
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

		// Token: 0x17002F64 RID: 12132
		// (get) Token: 0x06008E6C RID: 36460
		// (set) Token: 0x06008E6B RID: 36459
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

		// Token: 0x17002F65 RID: 12133
		// (get) Token: 0x06008E6E RID: 36462
		// (set) Token: 0x06008E6D RID: 36461
		[DispId(-2147412035)]
		object onbeforedeactivate
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

		// Token: 0x06008E6F RID: 36463
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setActive();

		// Token: 0x17002F66 RID: 12134
		// (get) Token: 0x06008E71 RID: 36465
		// (set) Token: 0x06008E70 RID: 36464
		[DispId(-2147412950)]
		string contentEditable
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

		// Token: 0x17002F67 RID: 12135
		// (get) Token: 0x06008E72 RID: 36466
		[DispId(-2147417010)]
		bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F68 RID: 12136
		// (get) Token: 0x06008E74 RID: 36468
		// (set) Token: 0x06008E73 RID: 36467
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

		// Token: 0x17002F69 RID: 12137
		// (get) Token: 0x06008E76 RID: 36470
		// (set) Token: 0x06008E75 RID: 36469
		[DispId(-2147418036)]
		bool disabled
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

		// Token: 0x17002F6A RID: 12138
		// (get) Token: 0x06008E77 RID: 36471
		[DispId(-2147417007)]
		bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F6B RID: 12139
		// (get) Token: 0x06008E79 RID: 36473
		// (set) Token: 0x06008E78 RID: 36472
		[DispId(-2147412034)]
		object onmove
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

		// Token: 0x17002F6C RID: 12140
		// (get) Token: 0x06008E7B RID: 36475
		// (set) Token: 0x06008E7A RID: 36474
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

		// Token: 0x06008E7C RID: 36476
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17002F6D RID: 12141
		// (get) Token: 0x06008E7E RID: 36478
		// (set) Token: 0x06008E7D RID: 36477
		[DispId(-2147412029)]
		object onresizestart
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

		// Token: 0x17002F6E RID: 12142
		// (get) Token: 0x06008E80 RID: 36480
		// (set) Token: 0x06008E7F RID: 36479
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

		// Token: 0x17002F6F RID: 12143
		// (get) Token: 0x06008E82 RID: 36482
		// (set) Token: 0x06008E81 RID: 36481
		[DispId(-2147412031)]
		object onmovestart
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

		// Token: 0x17002F70 RID: 12144
		// (get) Token: 0x06008E84 RID: 36484
		// (set) Token: 0x06008E83 RID: 36483
		[DispId(-2147412030)]
		object onmoveend
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

		// Token: 0x17002F71 RID: 12145
		// (get) Token: 0x06008E86 RID: 36486
		// (set) Token: 0x06008E85 RID: 36485
		[DispId(-2147412027)]
		object onmouseenter
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

		// Token: 0x17002F72 RID: 12146
		// (get) Token: 0x06008E88 RID: 36488
		// (set) Token: 0x06008E87 RID: 36487
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

		// Token: 0x17002F73 RID: 12147
		// (get) Token: 0x06008E8A RID: 36490
		// (set) Token: 0x06008E89 RID: 36489
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

		// Token: 0x17002F74 RID: 12148
		// (get) Token: 0x06008E8C RID: 36492
		// (set) Token: 0x06008E8B RID: 36491
		[DispId(-2147412024)]
		object ondeactivate
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

		// Token: 0x06008E8D RID: 36493
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool dragDrop();

		// Token: 0x17002F75 RID: 12149
		// (get) Token: 0x06008E8E RID: 36494
		[DispId(-2147417004)]
		int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F76 RID: 12150
		// (get) Token: 0x06008E90 RID: 36496
		// (set) Token: 0x06008E8F RID: 36495
		[DispId(-2147412036)]
		object onmousewheel
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

		// Token: 0x06008E91 RID: 36497
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void normalize();

		// Token: 0x06008E92 RID: 36498
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06008E93 RID: 36499
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06008E94 RID: 36500
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17002F77 RID: 12151
		// (get) Token: 0x06008E96 RID: 36502
		// (set) Token: 0x06008E95 RID: 36501
		[DispId(-2147412022)]
		object onbeforeactivate
		{
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002F78 RID: 12152
		// (get) Token: 0x06008E98 RID: 36504
		// (set) Token: 0x06008E97 RID: 36503
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

		// Token: 0x17002F79 RID: 12153
		// (get) Token: 0x06008E9A RID: 36506
		// (set) Token: 0x06008E99 RID: 36505
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

		// Token: 0x17002F7A RID: 12154
		// (get) Token: 0x06008E9B RID: 36507
		[DispId(-2147417058)]
		int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F7B RID: 12155
		// (get) Token: 0x06008E9C RID: 36508
		[DispId(-2147417057)]
		string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002F7C RID: 12156
		// (get) Token: 0x06008E9D RID: 36509
		[DispId(-2147417066)]
		int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002F7D RID: 12157
		// (get) Token: 0x06008E9E RID: 36510
		[DispId(-2147417065)]
		IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06008E9F RID: 36511
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool hasChildNodes();

		// Token: 0x17002F7E RID: 12158
		// (get) Token: 0x06008EA0 RID: 36512
		[DispId(-2147417063)]
		object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002F7F RID: 12159
		// (get) Token: 0x06008EA1 RID: 36513
		[DispId(-2147417062)]
		object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06008EA2 RID: 36514
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06008EA3 RID: 36515
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06008EA4 RID: 36516
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06008EA5 RID: 36517
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06008EA6 RID: 36518
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06008EA7 RID: 36519
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06008EA8 RID: 36520
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06008EA9 RID: 36521
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17002F80 RID: 12160
		// (get) Token: 0x06008EAA RID: 36522
		[DispId(-2147417038)]
		string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002F81 RID: 12161
		// (get) Token: 0x06008EAC RID: 36524
		// (set) Token: 0x06008EAB RID: 36523
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

		// Token: 0x17002F82 RID: 12162
		// (get) Token: 0x06008EAD RID: 36525
		[DispId(-2147417036)]
		IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F83 RID: 12163
		// (get) Token: 0x06008EAE RID: 36526
		[DispId(-2147417035)]
		IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F84 RID: 12164
		// (get) Token: 0x06008EAF RID: 36527
		[DispId(-2147417034)]
		IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F85 RID: 12165
		// (get) Token: 0x06008EB0 RID: 36528
		[DispId(-2147417033)]
		IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F86 RID: 12166
		// (get) Token: 0x06008EB1 RID: 36529
		[DispId(-2147416999)]
		object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002F87 RID: 12167
		// (get) Token: 0x06008EB3 RID: 36531
		// (set) Token: 0x06008EB2 RID: 36530
		[DispId(-2147417091)]
		string dataFld
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

		// Token: 0x17002F88 RID: 12168
		// (get) Token: 0x06008EB5 RID: 36533
		// (set) Token: 0x06008EB4 RID: 36532
		[DispId(-2147417090)]
		string dataSrc
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

		// Token: 0x17002F89 RID: 12169
		// (get) Token: 0x06008EB7 RID: 36535
		// (set) Token: 0x06008EB6 RID: 36534
		[DispId(-2147417089)]
		string dataFormatAs
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

		// Token: 0x17002F8A RID: 12170
		// (get) Token: 0x06008EB9 RID: 36537
		// (set) Token: 0x06008EB8 RID: 36536
		[DispId(2000)]
		string type
		{
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002F8B RID: 12171
		// (get) Token: 0x06008EBB RID: 36539
		// (set) Token: 0x06008EBA RID: 36538
		[DispId(-2147413011)]
		string value
		{
			[TypeLibFunc(20)]
			[DispId(-2147413011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002F8C RID: 12172
		// (get) Token: 0x06008EBD RID: 36541
		// (set) Token: 0x06008EBC RID: 36540
		[DispId(-2147418112)]
		string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002F8D RID: 12173
		// (get) Token: 0x06008EBF RID: 36543
		// (set) Token: 0x06008EBE RID: 36542
		[DispId(2001)]
		bool status
		{
			[DispId(2001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002F8E RID: 12174
		// (get) Token: 0x06008EC0 RID: 36544
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002F8F RID: 12175
		// (get) Token: 0x06008EC2 RID: 36546
		// (set) Token: 0x06008EC1 RID: 36545
		[DispId(2002)]
		int size
		{
			[DispId(2002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002F90 RID: 12176
		// (get) Token: 0x06008EC4 RID: 36548
		// (set) Token: 0x06008EC3 RID: 36547
		[DispId(2003)]
		int maxLength
		{
			[DispId(2003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06008EC5 RID: 36549
		[DispId(2004)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void select();

		// Token: 0x17002F91 RID: 12177
		// (get) Token: 0x06008EC7 RID: 36551
		// (set) Token: 0x06008EC6 RID: 36550
		[DispId(-2147412082)]
		object onchange
		{
			[DispId(-2147412082)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412082)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002F92 RID: 12178
		// (get) Token: 0x06008EC9 RID: 36553
		// (set) Token: 0x06008EC8 RID: 36552
		[DispId(-2147412102)]
		object onselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002F93 RID: 12179
		// (get) Token: 0x06008ECB RID: 36555
		// (set) Token: 0x06008ECA RID: 36554
		[DispId(-2147413029)]
		string defaultValue
		{
			[TypeLibFunc(84)]
			[DispId(-2147413029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147413029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002F94 RID: 12180
		// (get) Token: 0x06008ECD RID: 36557
		// (set) Token: 0x06008ECC RID: 36556
		[DispId(2005)]
		bool readOnly
		{
			[DispId(2005)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2005)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06008ECE RID: 36558
		[DispId(2006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTxtRange createTextRange();

		// Token: 0x17002F95 RID: 12181
		// (get) Token: 0x06008ED0 RID: 36560
		// (set) Token: 0x06008ECF RID: 36559
		[DispId(2007)]
		bool indeterminate
		{
			[DispId(2007)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2007)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002F96 RID: 12182
		// (get) Token: 0x06008ED2 RID: 36562
		// (set) Token: 0x06008ED1 RID: 36561
		[DispId(2008)]
		bool defaultChecked
		{
			[DispId(2008)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[DispId(2008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002F97 RID: 12183
		// (get) Token: 0x06008ED4 RID: 36564
		// (set) Token: 0x06008ED3 RID: 36563
		[DispId(2009)]
		bool @checked
		{
			[TypeLibFunc(4)]
			[DispId(2009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2009)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002F98 RID: 12184
		// (get) Token: 0x06008ED6 RID: 36566
		// (set) Token: 0x06008ED5 RID: 36565
		[DispId(2012)]
		object border
		{
			[DispId(2012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002F99 RID: 12185
		// (get) Token: 0x06008ED8 RID: 36568
		// (set) Token: 0x06008ED7 RID: 36567
		[DispId(2013)]
		int vspace
		{
			[DispId(2013)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002F9A RID: 12186
		// (get) Token: 0x06008EDA RID: 36570
		// (set) Token: 0x06008ED9 RID: 36569
		[DispId(2014)]
		int hspace
		{
			[DispId(2014)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2014)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002F9B RID: 12187
		// (get) Token: 0x06008EDC RID: 36572
		// (set) Token: 0x06008EDB RID: 36571
		[DispId(2010)]
		string alt
		{
			[DispId(2010)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2010)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002F9C RID: 12188
		// (get) Token: 0x06008EDE RID: 36574
		// (set) Token: 0x06008EDD RID: 36573
		[DispId(2011)]
		string src
		{
			[DispId(2011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002F9D RID: 12189
		// (get) Token: 0x06008EE0 RID: 36576
		// (set) Token: 0x06008EDF RID: 36575
		[DispId(2015)]
		string lowsrc
		{
			[TypeLibFunc(20)]
			[DispId(2015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2015)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002F9E RID: 12190
		// (get) Token: 0x06008EE2 RID: 36578
		// (set) Token: 0x06008EE1 RID: 36577
		[DispId(2016)]
		string vrml
		{
			[TypeLibFunc(20)]
			[DispId(2016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2016)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002F9F RID: 12191
		// (get) Token: 0x06008EE4 RID: 36580
		// (set) Token: 0x06008EE3 RID: 36579
		[DispId(2017)]
		string dynsrc
		{
			[TypeLibFunc(20)]
			[DispId(2017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2017)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002FA0 RID: 12192
		// (get) Token: 0x06008EE5 RID: 36581
		[DispId(2018)]
		bool complete
		{
			[DispId(2018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FA1 RID: 12193
		// (get) Token: 0x06008EE7 RID: 36583
		// (set) Token: 0x06008EE6 RID: 36582
		[DispId(2019)]
		object loop
		{
			[DispId(2019)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(2019)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002FA2 RID: 12194
		// (get) Token: 0x06008EE9 RID: 36585
		// (set) Token: 0x06008EE8 RID: 36584
		[DispId(-2147418039)]
		string align
		{
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002FA3 RID: 12195
		// (get) Token: 0x06008EEB RID: 36587
		// (set) Token: 0x06008EEA RID: 36586
		[DispId(-2147412080)]
		object onload
		{
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002FA4 RID: 12196
		// (get) Token: 0x06008EED RID: 36589
		// (set) Token: 0x06008EEC RID: 36588
		[DispId(-2147412083)]
		object onerror
		{
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002FA5 RID: 12197
		// (get) Token: 0x06008EEF RID: 36591
		// (set) Token: 0x06008EEE RID: 36590
		[DispId(-2147412084)]
		object onabort
		{
			[TypeLibFunc(20)]
			[DispId(-2147412084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002FA6 RID: 12198
		// (get) Token: 0x06008EF1 RID: 36593
		// (set) Token: 0x06008EF0 RID: 36592
		[DispId(-2147418107)]
		int width
		{
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002FA7 RID: 12199
		// (get) Token: 0x06008EF3 RID: 36595
		// (set) Token: 0x06008EF2 RID: 36594
		[DispId(-2147418106)]
		int height
		{
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002FA8 RID: 12200
		// (get) Token: 0x06008EF5 RID: 36597
		// (set) Token: 0x06008EF4 RID: 36596
		[DispId(2020)]
		string Start
		{
			[DispId(2020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002FA9 RID: 12201
		// (get) Token: 0x06008EF7 RID: 36599
		// (set) Token: 0x06008EF6 RID: 36598
		[DispId(2022)]
		string accept
		{
			[DispId(2022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002FAA RID: 12202
		// (get) Token: 0x06008EF9 RID: 36601
		// (set) Token: 0x06008EF8 RID: 36600
		[DispId(2023)]
		string useMap
		{
			[DispId(2023)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}
	}
}
