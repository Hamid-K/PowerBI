using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A95 RID: 2709
	[TypeLibType(2)]
	[ComSourceInterfaces("mshtml.HTMLScriptEvents\0mshtml.HTMLScriptEvents2\0\0")]
	[ClassInterface(0)]
	[Guid("3050F28C-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLScriptElementClass : DispHTMLScriptElement, HTMLScriptElement, HTMLScriptEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLScriptElement, IHTMLScriptElement2, HTMLScriptEvents2_Event
	{
		// Token: 0x06011DE4 RID: 73188
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLScriptElementClass();

		// Token: 0x06011DE5 RID: 73189
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06011DE6 RID: 73190
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06011DE7 RID: 73191
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17005EA9 RID: 24233
		// (get) Token: 0x06011DE9 RID: 73193
		// (set) Token: 0x06011DE8 RID: 73192
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

		// Token: 0x17005EAA RID: 24234
		// (get) Token: 0x06011DEB RID: 73195
		// (set) Token: 0x06011DEA RID: 73194
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

		// Token: 0x17005EAB RID: 24235
		// (get) Token: 0x06011DEC RID: 73196
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005EAC RID: 24236
		// (get) Token: 0x06011DED RID: 73197
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005EAD RID: 24237
		// (get) Token: 0x06011DEE RID: 73198
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005EAE RID: 24238
		// (get) Token: 0x06011DF0 RID: 73200
		// (set) Token: 0x06011DEF RID: 73199
		[DispId(-2147412099)]
		public virtual extern object onhelp
		{
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005EAF RID: 24239
		// (get) Token: 0x06011DF2 RID: 73202
		// (set) Token: 0x06011DF1 RID: 73201
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

		// Token: 0x17005EB0 RID: 24240
		// (get) Token: 0x06011DF4 RID: 73204
		// (set) Token: 0x06011DF3 RID: 73203
		[DispId(-2147412103)]
		public virtual extern object ondblclick
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

		// Token: 0x17005EB1 RID: 24241
		// (get) Token: 0x06011DF6 RID: 73206
		// (set) Token: 0x06011DF5 RID: 73205
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

		// Token: 0x17005EB2 RID: 24242
		// (get) Token: 0x06011DF8 RID: 73208
		// (set) Token: 0x06011DF7 RID: 73207
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

		// Token: 0x17005EB3 RID: 24243
		// (get) Token: 0x06011DFA RID: 73210
		// (set) Token: 0x06011DF9 RID: 73209
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

		// Token: 0x17005EB4 RID: 24244
		// (get) Token: 0x06011DFC RID: 73212
		// (set) Token: 0x06011DFB RID: 73211
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

		// Token: 0x17005EB5 RID: 24245
		// (get) Token: 0x06011DFE RID: 73214
		// (set) Token: 0x06011DFD RID: 73213
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

		// Token: 0x17005EB6 RID: 24246
		// (get) Token: 0x06011E00 RID: 73216
		// (set) Token: 0x06011DFF RID: 73215
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

		// Token: 0x17005EB7 RID: 24247
		// (get) Token: 0x06011E02 RID: 73218
		// (set) Token: 0x06011E01 RID: 73217
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

		// Token: 0x17005EB8 RID: 24248
		// (get) Token: 0x06011E04 RID: 73220
		// (set) Token: 0x06011E03 RID: 73219
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

		// Token: 0x17005EB9 RID: 24249
		// (get) Token: 0x06011E05 RID: 73221
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005EBA RID: 24250
		// (get) Token: 0x06011E07 RID: 73223
		// (set) Token: 0x06011E06 RID: 73222
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

		// Token: 0x17005EBB RID: 24251
		// (get) Token: 0x06011E09 RID: 73225
		// (set) Token: 0x06011E08 RID: 73224
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

		// Token: 0x17005EBC RID: 24252
		// (get) Token: 0x06011E0B RID: 73227
		// (set) Token: 0x06011E0A RID: 73226
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

		// Token: 0x06011E0C RID: 73228
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06011E0D RID: 73229
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17005EBD RID: 24253
		// (get) Token: 0x06011E0E RID: 73230
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005EBE RID: 24254
		// (get) Token: 0x06011E0F RID: 73231
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17005EBF RID: 24255
		// (get) Token: 0x06011E11 RID: 73233
		// (set) Token: 0x06011E10 RID: 73232
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

		// Token: 0x17005EC0 RID: 24256
		// (get) Token: 0x06011E12 RID: 73234
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005EC1 RID: 24257
		// (get) Token: 0x06011E13 RID: 73235
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005EC2 RID: 24258
		// (get) Token: 0x06011E14 RID: 73236
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005EC3 RID: 24259
		// (get) Token: 0x06011E15 RID: 73237
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005EC4 RID: 24260
		// (get) Token: 0x06011E16 RID: 73238
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005EC5 RID: 24261
		// (get) Token: 0x06011E18 RID: 73240
		// (set) Token: 0x06011E17 RID: 73239
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

		// Token: 0x17005EC6 RID: 24262
		// (get) Token: 0x06011E1A RID: 73242
		// (set) Token: 0x06011E19 RID: 73241
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

		// Token: 0x17005EC7 RID: 24263
		// (get) Token: 0x06011E1C RID: 73244
		// (set) Token: 0x06011E1B RID: 73243
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

		// Token: 0x17005EC8 RID: 24264
		// (get) Token: 0x06011E1E RID: 73246
		// (set) Token: 0x06011E1D RID: 73245
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

		// Token: 0x06011E1F RID: 73247
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06011E20 RID: 73248
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17005EC9 RID: 24265
		// (get) Token: 0x06011E21 RID: 73249
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005ECA RID: 24266
		// (get) Token: 0x06011E22 RID: 73250
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06011E23 RID: 73251
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17005ECB RID: 24267
		// (get) Token: 0x06011E24 RID: 73252
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005ECC RID: 24268
		// (get) Token: 0x06011E26 RID: 73254
		// (set) Token: 0x06011E25 RID: 73253
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

		// Token: 0x06011E27 RID: 73255
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17005ECD RID: 24269
		// (get) Token: 0x06011E29 RID: 73257
		// (set) Token: 0x06011E28 RID: 73256
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

		// Token: 0x17005ECE RID: 24270
		// (get) Token: 0x06011E2B RID: 73259
		// (set) Token: 0x06011E2A RID: 73258
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

		// Token: 0x17005ECF RID: 24271
		// (get) Token: 0x06011E2D RID: 73261
		// (set) Token: 0x06011E2C RID: 73260
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

		// Token: 0x17005ED0 RID: 24272
		// (get) Token: 0x06011E2F RID: 73263
		// (set) Token: 0x06011E2E RID: 73262
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

		// Token: 0x17005ED1 RID: 24273
		// (get) Token: 0x06011E31 RID: 73265
		// (set) Token: 0x06011E30 RID: 73264
		[DispId(-2147412093)]
		public virtual extern object onrowenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005ED2 RID: 24274
		// (get) Token: 0x06011E33 RID: 73267
		// (set) Token: 0x06011E32 RID: 73266
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

		// Token: 0x17005ED3 RID: 24275
		// (get) Token: 0x06011E35 RID: 73269
		// (set) Token: 0x06011E34 RID: 73268
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

		// Token: 0x17005ED4 RID: 24276
		// (get) Token: 0x06011E37 RID: 73271
		// (set) Token: 0x06011E36 RID: 73270
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

		// Token: 0x17005ED5 RID: 24277
		// (get) Token: 0x06011E39 RID: 73273
		// (set) Token: 0x06011E38 RID: 73272
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

		// Token: 0x17005ED6 RID: 24278
		// (get) Token: 0x06011E3A RID: 73274
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005ED7 RID: 24279
		// (get) Token: 0x06011E3B RID: 73275
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005ED8 RID: 24280
		// (get) Token: 0x06011E3C RID: 73276
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06011E3D RID: 73277
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06011E3E RID: 73278
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17005ED9 RID: 24281
		// (get) Token: 0x06011E40 RID: 73280
		// (set) Token: 0x06011E3F RID: 73279
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

		// Token: 0x06011E41 RID: 73281
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06011E42 RID: 73282
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17005EDA RID: 24282
		// (get) Token: 0x06011E44 RID: 73284
		// (set) Token: 0x06011E43 RID: 73283
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005EDB RID: 24283
		// (get) Token: 0x06011E46 RID: 73286
		// (set) Token: 0x06011E45 RID: 73285
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

		// Token: 0x17005EDC RID: 24284
		// (get) Token: 0x06011E48 RID: 73288
		// (set) Token: 0x06011E47 RID: 73287
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

		// Token: 0x17005EDD RID: 24285
		// (get) Token: 0x06011E4A RID: 73290
		// (set) Token: 0x06011E49 RID: 73289
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

		// Token: 0x17005EDE RID: 24286
		// (get) Token: 0x06011E4C RID: 73292
		// (set) Token: 0x06011E4B RID: 73291
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

		// Token: 0x17005EDF RID: 24287
		// (get) Token: 0x06011E4E RID: 73294
		// (set) Token: 0x06011E4D RID: 73293
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

		// Token: 0x17005EE0 RID: 24288
		// (get) Token: 0x06011E50 RID: 73296
		// (set) Token: 0x06011E4F RID: 73295
		[DispId(-2147412058)]
		public virtual extern object ondrop
		{
			[DispId(-2147412058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005EE1 RID: 24289
		// (get) Token: 0x06011E52 RID: 73298
		// (set) Token: 0x06011E51 RID: 73297
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

		// Token: 0x17005EE2 RID: 24290
		// (get) Token: 0x06011E54 RID: 73300
		// (set) Token: 0x06011E53 RID: 73299
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

		// Token: 0x17005EE3 RID: 24291
		// (get) Token: 0x06011E56 RID: 73302
		// (set) Token: 0x06011E55 RID: 73301
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

		// Token: 0x17005EE4 RID: 24292
		// (get) Token: 0x06011E58 RID: 73304
		// (set) Token: 0x06011E57 RID: 73303
		[DispId(-2147412056)]
		public virtual extern object oncopy
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

		// Token: 0x17005EE5 RID: 24293
		// (get) Token: 0x06011E5A RID: 73306
		// (set) Token: 0x06011E59 RID: 73305
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

		// Token: 0x17005EE6 RID: 24294
		// (get) Token: 0x06011E5C RID: 73308
		// (set) Token: 0x06011E5B RID: 73307
		[DispId(-2147412055)]
		public virtual extern object onpaste
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

		// Token: 0x17005EE7 RID: 24295
		// (get) Token: 0x06011E5D RID: 73309
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005EE8 RID: 24296
		// (get) Token: 0x06011E5F RID: 73311
		// (set) Token: 0x06011E5E RID: 73310
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

		// Token: 0x06011E60 RID: 73312
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06011E61 RID: 73313
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06011E62 RID: 73314
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06011E63 RID: 73315
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06011E64 RID: 73316
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17005EE9 RID: 24297
		// (get) Token: 0x06011E66 RID: 73318
		// (set) Token: 0x06011E65 RID: 73317
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

		// Token: 0x06011E67 RID: 73319
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17005EEA RID: 24298
		// (get) Token: 0x06011E69 RID: 73321
		// (set) Token: 0x06011E68 RID: 73320
		[DispId(-2147416107)]
		public virtual extern string accessKey
		{
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005EEB RID: 24299
		// (get) Token: 0x06011E6B RID: 73323
		// (set) Token: 0x06011E6A RID: 73322
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

		// Token: 0x17005EEC RID: 24300
		// (get) Token: 0x06011E6D RID: 73325
		// (set) Token: 0x06011E6C RID: 73324
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

		// Token: 0x17005EED RID: 24301
		// (get) Token: 0x06011E6F RID: 73327
		// (set) Token: 0x06011E6E RID: 73326
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

		// Token: 0x06011E70 RID: 73328
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06011E71 RID: 73329
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06011E72 RID: 73330
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17005EEE RID: 24302
		// (get) Token: 0x06011E73 RID: 73331
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005EEF RID: 24303
		// (get) Token: 0x06011E74 RID: 73332
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005EF0 RID: 24304
		// (get) Token: 0x06011E75 RID: 73333
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005EF1 RID: 24305
		// (get) Token: 0x06011E76 RID: 73334
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06011E77 RID: 73335
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06011E78 RID: 73336
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17005EF2 RID: 24306
		// (get) Token: 0x06011E79 RID: 73337
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17005EF3 RID: 24307
		// (get) Token: 0x06011E7B RID: 73339
		// (set) Token: 0x06011E7A RID: 73338
		[DispId(-2147412087)]
		public virtual extern object onreadystatechange
		{
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005EF4 RID: 24308
		// (get) Token: 0x06011E7D RID: 73341
		// (set) Token: 0x06011E7C RID: 73340
		[DispId(-2147412050)]
		public virtual extern object onrowsdelete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005EF5 RID: 24309
		// (get) Token: 0x06011E7F RID: 73343
		// (set) Token: 0x06011E7E RID: 73342
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

		// Token: 0x17005EF6 RID: 24310
		// (get) Token: 0x06011E81 RID: 73345
		// (set) Token: 0x06011E80 RID: 73344
		[DispId(-2147412048)]
		public virtual extern object oncellchange
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

		// Token: 0x17005EF7 RID: 24311
		// (get) Token: 0x06011E83 RID: 73347
		// (set) Token: 0x06011E82 RID: 73346
		[DispId(-2147412995)]
		public virtual extern string dir
		{
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06011E84 RID: 73348
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17005EF8 RID: 24312
		// (get) Token: 0x06011E85 RID: 73349
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005EF9 RID: 24313
		// (get) Token: 0x06011E86 RID: 73350
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005EFA RID: 24314
		// (get) Token: 0x06011E88 RID: 73352
		// (set) Token: 0x06011E87 RID: 73351
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

		// Token: 0x17005EFB RID: 24315
		// (get) Token: 0x06011E8A RID: 73354
		// (set) Token: 0x06011E89 RID: 73353
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

		// Token: 0x06011E8B RID: 73355
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17005EFC RID: 24316
		// (get) Token: 0x06011E8D RID: 73357
		// (set) Token: 0x06011E8C RID: 73356
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

		// Token: 0x06011E8E RID: 73358
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06011E8F RID: 73359
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06011E90 RID: 73360
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06011E91 RID: 73361
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17005EFD RID: 24317
		// (get) Token: 0x06011E92 RID: 73362
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06011E93 RID: 73363
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06011E94 RID: 73364
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17005EFE RID: 24318
		// (get) Token: 0x06011E95 RID: 73365
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005EFF RID: 24319
		// (get) Token: 0x06011E96 RID: 73366
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005F00 RID: 24320
		// (get) Token: 0x06011E98 RID: 73368
		// (set) Token: 0x06011E97 RID: 73367
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

		// Token: 0x17005F01 RID: 24321
		// (get) Token: 0x06011E9A RID: 73370
		// (set) Token: 0x06011E99 RID: 73369
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

		// Token: 0x17005F02 RID: 24322
		// (get) Token: 0x06011E9B RID: 73371
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06011E9C RID: 73372
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06011E9D RID: 73373
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17005F03 RID: 24323
		// (get) Token: 0x06011E9E RID: 73374
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F04 RID: 24324
		// (get) Token: 0x06011E9F RID: 73375
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F05 RID: 24325
		// (get) Token: 0x06011EA1 RID: 73377
		// (set) Token: 0x06011EA0 RID: 73376
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

		// Token: 0x17005F06 RID: 24326
		// (get) Token: 0x06011EA3 RID: 73379
		// (set) Token: 0x06011EA2 RID: 73378
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

		// Token: 0x17005F07 RID: 24327
		// (get) Token: 0x06011EA5 RID: 73381
		// (set) Token: 0x06011EA4 RID: 73380
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

		// Token: 0x17005F08 RID: 24328
		// (get) Token: 0x06011EA7 RID: 73383
		// (set) Token: 0x06011EA6 RID: 73382
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

		// Token: 0x06011EA8 RID: 73384
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17005F09 RID: 24329
		// (get) Token: 0x06011EAA RID: 73386
		// (set) Token: 0x06011EA9 RID: 73385
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

		// Token: 0x17005F0A RID: 24330
		// (get) Token: 0x06011EAB RID: 73387
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F0B RID: 24331
		// (get) Token: 0x06011EAD RID: 73389
		// (set) Token: 0x06011EAC RID: 73388
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

		// Token: 0x17005F0C RID: 24332
		// (get) Token: 0x06011EAF RID: 73391
		// (set) Token: 0x06011EAE RID: 73390
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

		// Token: 0x17005F0D RID: 24333
		// (get) Token: 0x06011EB0 RID: 73392
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F0E RID: 24334
		// (get) Token: 0x06011EB2 RID: 73394
		// (set) Token: 0x06011EB1 RID: 73393
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

		// Token: 0x17005F0F RID: 24335
		// (get) Token: 0x06011EB4 RID: 73396
		// (set) Token: 0x06011EB3 RID: 73395
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

		// Token: 0x06011EB5 RID: 73397
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17005F10 RID: 24336
		// (get) Token: 0x06011EB7 RID: 73399
		// (set) Token: 0x06011EB6 RID: 73398
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

		// Token: 0x17005F11 RID: 24337
		// (get) Token: 0x06011EB9 RID: 73401
		// (set) Token: 0x06011EB8 RID: 73400
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

		// Token: 0x17005F12 RID: 24338
		// (get) Token: 0x06011EBB RID: 73403
		// (set) Token: 0x06011EBA RID: 73402
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

		// Token: 0x17005F13 RID: 24339
		// (get) Token: 0x06011EBD RID: 73405
		// (set) Token: 0x06011EBC RID: 73404
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

		// Token: 0x17005F14 RID: 24340
		// (get) Token: 0x06011EBF RID: 73407
		// (set) Token: 0x06011EBE RID: 73406
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

		// Token: 0x17005F15 RID: 24341
		// (get) Token: 0x06011EC1 RID: 73409
		// (set) Token: 0x06011EC0 RID: 73408
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

		// Token: 0x17005F16 RID: 24342
		// (get) Token: 0x06011EC3 RID: 73411
		// (set) Token: 0x06011EC2 RID: 73410
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

		// Token: 0x17005F17 RID: 24343
		// (get) Token: 0x06011EC5 RID: 73413
		// (set) Token: 0x06011EC4 RID: 73412
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

		// Token: 0x06011EC6 RID: 73414
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17005F18 RID: 24344
		// (get) Token: 0x06011EC7 RID: 73415
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F19 RID: 24345
		// (get) Token: 0x06011EC9 RID: 73417
		// (set) Token: 0x06011EC8 RID: 73416
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

		// Token: 0x06011ECA RID: 73418
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06011ECB RID: 73419
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06011ECC RID: 73420
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06011ECD RID: 73421
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17005F1A RID: 24346
		// (get) Token: 0x06011ECF RID: 73423
		// (set) Token: 0x06011ECE RID: 73422
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

		// Token: 0x17005F1B RID: 24347
		// (get) Token: 0x06011ED1 RID: 73425
		// (set) Token: 0x06011ED0 RID: 73424
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

		// Token: 0x17005F1C RID: 24348
		// (get) Token: 0x06011ED3 RID: 73427
		// (set) Token: 0x06011ED2 RID: 73426
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

		// Token: 0x17005F1D RID: 24349
		// (get) Token: 0x06011ED4 RID: 73428
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F1E RID: 24350
		// (get) Token: 0x06011ED5 RID: 73429
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005F1F RID: 24351
		// (get) Token: 0x06011ED6 RID: 73430
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F20 RID: 24352
		// (get) Token: 0x06011ED7 RID: 73431
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06011ED8 RID: 73432
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17005F21 RID: 24353
		// (get) Token: 0x06011ED9 RID: 73433
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005F22 RID: 24354
		// (get) Token: 0x06011EDA RID: 73434
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06011EDB RID: 73435
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06011EDC RID: 73436
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06011EDD RID: 73437
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06011EDE RID: 73438
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06011EDF RID: 73439
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06011EE0 RID: 73440
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06011EE1 RID: 73441
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06011EE2 RID: 73442
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17005F23 RID: 24355
		// (get) Token: 0x06011EE3 RID: 73443
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005F24 RID: 24356
		// (get) Token: 0x06011EE5 RID: 73445
		// (set) Token: 0x06011EE4 RID: 73444
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

		// Token: 0x17005F25 RID: 24357
		// (get) Token: 0x06011EE6 RID: 73446
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F26 RID: 24358
		// (get) Token: 0x06011EE7 RID: 73447
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F27 RID: 24359
		// (get) Token: 0x06011EE8 RID: 73448
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F28 RID: 24360
		// (get) Token: 0x06011EE9 RID: 73449
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F29 RID: 24361
		// (get) Token: 0x06011EEA RID: 73450
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005F2A RID: 24362
		// (get) Token: 0x06011EEC RID: 73452
		// (set) Token: 0x06011EEB RID: 73451
		[DispId(1001)]
		public virtual extern string src
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

		// Token: 0x17005F2B RID: 24363
		// (get) Token: 0x06011EEE RID: 73454
		// (set) Token: 0x06011EED RID: 73453
		[DispId(1004)]
		public virtual extern string htmlFor
		{
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005F2C RID: 24364
		// (get) Token: 0x06011EF0 RID: 73456
		// (set) Token: 0x06011EEF RID: 73455
		[DispId(1005)]
		public virtual extern string @event
		{
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005F2D RID: 24365
		// (get) Token: 0x06011EF2 RID: 73458
		// (set) Token: 0x06011EF1 RID: 73457
		[DispId(1006)]
		public virtual extern string text
		{
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1006)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005F2E RID: 24366
		// (get) Token: 0x06011EF4 RID: 73460
		// (set) Token: 0x06011EF3 RID: 73459
		[DispId(1007)]
		public virtual extern bool defer
		{
			[TypeLibFunc(20)]
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17005F2F RID: 24367
		// (get) Token: 0x06011EF6 RID: 73462
		// (set) Token: 0x06011EF5 RID: 73461
		[DispId(-2147412083)]
		public virtual extern object onerror
		{
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005F30 RID: 24368
		// (get) Token: 0x06011EF8 RID: 73464
		// (set) Token: 0x06011EF7 RID: 73463
		[DispId(1009)]
		public virtual extern string type
		{
			[DispId(1009)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005F31 RID: 24369
		// (get) Token: 0x06011EFA RID: 73466
		// (set) Token: 0x06011EF9 RID: 73465
		[DispId(1010)]
		public virtual extern string charset
		{
			[DispId(1010)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06011EFB RID: 73467
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06011EFC RID: 73468
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06011EFD RID: 73469
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17005F32 RID: 24370
		// (get) Token: 0x06011EFF RID: 73471
		// (set) Token: 0x06011EFE RID: 73470
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

		// Token: 0x17005F33 RID: 24371
		// (get) Token: 0x06011F01 RID: 73473
		// (set) Token: 0x06011F00 RID: 73472
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

		// Token: 0x17005F34 RID: 24372
		// (get) Token: 0x06011F02 RID: 73474
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005F35 RID: 24373
		// (get) Token: 0x06011F03 RID: 73475
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F36 RID: 24374
		// (get) Token: 0x06011F04 RID: 73476
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F37 RID: 24375
		// (get) Token: 0x06011F06 RID: 73478
		// (set) Token: 0x06011F05 RID: 73477
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

		// Token: 0x17005F38 RID: 24376
		// (get) Token: 0x06011F08 RID: 73480
		// (set) Token: 0x06011F07 RID: 73479
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

		// Token: 0x17005F39 RID: 24377
		// (get) Token: 0x06011F0A RID: 73482
		// (set) Token: 0x06011F09 RID: 73481
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

		// Token: 0x17005F3A RID: 24378
		// (get) Token: 0x06011F0C RID: 73484
		// (set) Token: 0x06011F0B RID: 73483
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

		// Token: 0x17005F3B RID: 24379
		// (get) Token: 0x06011F0E RID: 73486
		// (set) Token: 0x06011F0D RID: 73485
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

		// Token: 0x17005F3C RID: 24380
		// (get) Token: 0x06011F10 RID: 73488
		// (set) Token: 0x06011F0F RID: 73487
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

		// Token: 0x17005F3D RID: 24381
		// (get) Token: 0x06011F12 RID: 73490
		// (set) Token: 0x06011F11 RID: 73489
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

		// Token: 0x17005F3E RID: 24382
		// (get) Token: 0x06011F14 RID: 73492
		// (set) Token: 0x06011F13 RID: 73491
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

		// Token: 0x17005F3F RID: 24383
		// (get) Token: 0x06011F16 RID: 73494
		// (set) Token: 0x06011F15 RID: 73493
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

		// Token: 0x17005F40 RID: 24384
		// (get) Token: 0x06011F18 RID: 73496
		// (set) Token: 0x06011F17 RID: 73495
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

		// Token: 0x17005F41 RID: 24385
		// (get) Token: 0x06011F1A RID: 73498
		// (set) Token: 0x06011F19 RID: 73497
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

		// Token: 0x17005F42 RID: 24386
		// (get) Token: 0x06011F1B RID: 73499
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005F43 RID: 24387
		// (get) Token: 0x06011F1D RID: 73501
		// (set) Token: 0x06011F1C RID: 73500
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

		// Token: 0x17005F44 RID: 24388
		// (get) Token: 0x06011F1F RID: 73503
		// (set) Token: 0x06011F1E RID: 73502
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

		// Token: 0x17005F45 RID: 24389
		// (get) Token: 0x06011F21 RID: 73505
		// (set) Token: 0x06011F20 RID: 73504
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

		// Token: 0x06011F22 RID: 73506
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06011F23 RID: 73507
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17005F46 RID: 24390
		// (get) Token: 0x06011F24 RID: 73508
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F47 RID: 24391
		// (get) Token: 0x06011F25 RID: 73509
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17005F48 RID: 24392
		// (get) Token: 0x06011F27 RID: 73511
		// (set) Token: 0x06011F26 RID: 73510
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

		// Token: 0x17005F49 RID: 24393
		// (get) Token: 0x06011F28 RID: 73512
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F4A RID: 24394
		// (get) Token: 0x06011F29 RID: 73513
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F4B RID: 24395
		// (get) Token: 0x06011F2A RID: 73514
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F4C RID: 24396
		// (get) Token: 0x06011F2B RID: 73515
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F4D RID: 24397
		// (get) Token: 0x06011F2C RID: 73516
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F4E RID: 24398
		// (get) Token: 0x06011F2E RID: 73518
		// (set) Token: 0x06011F2D RID: 73517
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

		// Token: 0x17005F4F RID: 24399
		// (get) Token: 0x06011F30 RID: 73520
		// (set) Token: 0x06011F2F RID: 73519
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

		// Token: 0x17005F50 RID: 24400
		// (get) Token: 0x06011F32 RID: 73522
		// (set) Token: 0x06011F31 RID: 73521
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

		// Token: 0x17005F51 RID: 24401
		// (get) Token: 0x06011F34 RID: 73524
		// (set) Token: 0x06011F33 RID: 73523
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

		// Token: 0x06011F35 RID: 73525
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06011F36 RID: 73526
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17005F52 RID: 24402
		// (get) Token: 0x06011F37 RID: 73527
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F53 RID: 24403
		// (get) Token: 0x06011F38 RID: 73528
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06011F39 RID: 73529
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17005F54 RID: 24404
		// (get) Token: 0x06011F3A RID: 73530
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F55 RID: 24405
		// (get) Token: 0x06011F3C RID: 73532
		// (set) Token: 0x06011F3B RID: 73531
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

		// Token: 0x06011F3D RID: 73533
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17005F56 RID: 24406
		// (get) Token: 0x06011F3F RID: 73535
		// (set) Token: 0x06011F3E RID: 73534
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

		// Token: 0x17005F57 RID: 24407
		// (get) Token: 0x06011F41 RID: 73537
		// (set) Token: 0x06011F40 RID: 73536
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

		// Token: 0x17005F58 RID: 24408
		// (get) Token: 0x06011F43 RID: 73539
		// (set) Token: 0x06011F42 RID: 73538
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

		// Token: 0x17005F59 RID: 24409
		// (get) Token: 0x06011F45 RID: 73541
		// (set) Token: 0x06011F44 RID: 73540
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

		// Token: 0x17005F5A RID: 24410
		// (get) Token: 0x06011F47 RID: 73543
		// (set) Token: 0x06011F46 RID: 73542
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

		// Token: 0x17005F5B RID: 24411
		// (get) Token: 0x06011F49 RID: 73545
		// (set) Token: 0x06011F48 RID: 73544
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

		// Token: 0x17005F5C RID: 24412
		// (get) Token: 0x06011F4B RID: 73547
		// (set) Token: 0x06011F4A RID: 73546
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

		// Token: 0x17005F5D RID: 24413
		// (get) Token: 0x06011F4D RID: 73549
		// (set) Token: 0x06011F4C RID: 73548
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

		// Token: 0x17005F5E RID: 24414
		// (get) Token: 0x06011F4F RID: 73551
		// (set) Token: 0x06011F4E RID: 73550
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

		// Token: 0x17005F5F RID: 24415
		// (get) Token: 0x06011F50 RID: 73552
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005F60 RID: 24416
		// (get) Token: 0x06011F51 RID: 73553
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005F61 RID: 24417
		// (get) Token: 0x06011F52 RID: 73554
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06011F53 RID: 73555
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06011F54 RID: 73556
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17005F62 RID: 24418
		// (get) Token: 0x06011F56 RID: 73558
		// (set) Token: 0x06011F55 RID: 73557
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

		// Token: 0x06011F57 RID: 73559
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06011F58 RID: 73560
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17005F63 RID: 24419
		// (get) Token: 0x06011F5A RID: 73562
		// (set) Token: 0x06011F59 RID: 73561
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

		// Token: 0x17005F64 RID: 24420
		// (get) Token: 0x06011F5C RID: 73564
		// (set) Token: 0x06011F5B RID: 73563
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

		// Token: 0x17005F65 RID: 24421
		// (get) Token: 0x06011F5E RID: 73566
		// (set) Token: 0x06011F5D RID: 73565
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

		// Token: 0x17005F66 RID: 24422
		// (get) Token: 0x06011F60 RID: 73568
		// (set) Token: 0x06011F5F RID: 73567
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

		// Token: 0x17005F67 RID: 24423
		// (get) Token: 0x06011F62 RID: 73570
		// (set) Token: 0x06011F61 RID: 73569
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

		// Token: 0x17005F68 RID: 24424
		// (get) Token: 0x06011F64 RID: 73572
		// (set) Token: 0x06011F63 RID: 73571
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

		// Token: 0x17005F69 RID: 24425
		// (get) Token: 0x06011F66 RID: 73574
		// (set) Token: 0x06011F65 RID: 73573
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

		// Token: 0x17005F6A RID: 24426
		// (get) Token: 0x06011F68 RID: 73576
		// (set) Token: 0x06011F67 RID: 73575
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

		// Token: 0x17005F6B RID: 24427
		// (get) Token: 0x06011F6A RID: 73578
		// (set) Token: 0x06011F69 RID: 73577
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

		// Token: 0x17005F6C RID: 24428
		// (get) Token: 0x06011F6C RID: 73580
		// (set) Token: 0x06011F6B RID: 73579
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

		// Token: 0x17005F6D RID: 24429
		// (get) Token: 0x06011F6E RID: 73582
		// (set) Token: 0x06011F6D RID: 73581
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

		// Token: 0x17005F6E RID: 24430
		// (get) Token: 0x06011F70 RID: 73584
		// (set) Token: 0x06011F6F RID: 73583
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

		// Token: 0x17005F6F RID: 24431
		// (get) Token: 0x06011F72 RID: 73586
		// (set) Token: 0x06011F71 RID: 73585
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

		// Token: 0x17005F70 RID: 24432
		// (get) Token: 0x06011F73 RID: 73587
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F71 RID: 24433
		// (get) Token: 0x06011F75 RID: 73589
		// (set) Token: 0x06011F74 RID: 73588
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

		// Token: 0x06011F76 RID: 73590
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06011F77 RID: 73591
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06011F78 RID: 73592
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06011F79 RID: 73593
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06011F7A RID: 73594
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17005F72 RID: 24434
		// (get) Token: 0x06011F7C RID: 73596
		// (set) Token: 0x06011F7B RID: 73595
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

		// Token: 0x06011F7D RID: 73597
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17005F73 RID: 24435
		// (get) Token: 0x06011F7F RID: 73599
		// (set) Token: 0x06011F7E RID: 73598
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

		// Token: 0x17005F74 RID: 24436
		// (get) Token: 0x06011F81 RID: 73601
		// (set) Token: 0x06011F80 RID: 73600
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

		// Token: 0x17005F75 RID: 24437
		// (get) Token: 0x06011F83 RID: 73603
		// (set) Token: 0x06011F82 RID: 73602
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

		// Token: 0x17005F76 RID: 24438
		// (get) Token: 0x06011F85 RID: 73605
		// (set) Token: 0x06011F84 RID: 73604
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

		// Token: 0x06011F86 RID: 73606
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06011F87 RID: 73607
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06011F88 RID: 73608
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17005F77 RID: 24439
		// (get) Token: 0x06011F89 RID: 73609
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F78 RID: 24440
		// (get) Token: 0x06011F8A RID: 73610
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F79 RID: 24441
		// (get) Token: 0x06011F8B RID: 73611
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F7A RID: 24442
		// (get) Token: 0x06011F8C RID: 73612
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06011F8D RID: 73613
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06011F8E RID: 73614
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17005F7B RID: 24443
		// (get) Token: 0x06011F8F RID: 73615
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17005F7C RID: 24444
		// (get) Token: 0x06011F91 RID: 73617
		// (set) Token: 0x06011F90 RID: 73616
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

		// Token: 0x17005F7D RID: 24445
		// (get) Token: 0x06011F93 RID: 73619
		// (set) Token: 0x06011F92 RID: 73618
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

		// Token: 0x17005F7E RID: 24446
		// (get) Token: 0x06011F95 RID: 73621
		// (set) Token: 0x06011F94 RID: 73620
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

		// Token: 0x17005F7F RID: 24447
		// (get) Token: 0x06011F97 RID: 73623
		// (set) Token: 0x06011F96 RID: 73622
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

		// Token: 0x17005F80 RID: 24448
		// (get) Token: 0x06011F99 RID: 73625
		// (set) Token: 0x06011F98 RID: 73624
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

		// Token: 0x06011F9A RID: 73626
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17005F81 RID: 24449
		// (get) Token: 0x06011F9B RID: 73627
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F82 RID: 24450
		// (get) Token: 0x06011F9C RID: 73628
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F83 RID: 24451
		// (get) Token: 0x06011F9E RID: 73630
		// (set) Token: 0x06011F9D RID: 73629
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

		// Token: 0x17005F84 RID: 24452
		// (get) Token: 0x06011FA0 RID: 73632
		// (set) Token: 0x06011F9F RID: 73631
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

		// Token: 0x06011FA1 RID: 73633
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06011FA2 RID: 73634
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17005F85 RID: 24453
		// (get) Token: 0x06011FA4 RID: 73636
		// (set) Token: 0x06011FA3 RID: 73635
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

		// Token: 0x06011FA5 RID: 73637
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06011FA6 RID: 73638
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06011FA7 RID: 73639
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06011FA8 RID: 73640
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17005F86 RID: 24454
		// (get) Token: 0x06011FA9 RID: 73641
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06011FAA RID: 73642
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06011FAB RID: 73643
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17005F87 RID: 24455
		// (get) Token: 0x06011FAC RID: 73644
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005F88 RID: 24456
		// (get) Token: 0x06011FAD RID: 73645
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005F89 RID: 24457
		// (get) Token: 0x06011FAF RID: 73647
		// (set) Token: 0x06011FAE RID: 73646
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

		// Token: 0x17005F8A RID: 24458
		// (get) Token: 0x06011FB1 RID: 73649
		// (set) Token: 0x06011FB0 RID: 73648
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

		// Token: 0x17005F8B RID: 24459
		// (get) Token: 0x06011FB2 RID: 73650
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06011FB3 RID: 73651
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06011FB4 RID: 73652
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17005F8C RID: 24460
		// (get) Token: 0x06011FB5 RID: 73653
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F8D RID: 24461
		// (get) Token: 0x06011FB6 RID: 73654
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F8E RID: 24462
		// (get) Token: 0x06011FB8 RID: 73656
		// (set) Token: 0x06011FB7 RID: 73655
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

		// Token: 0x17005F8F RID: 24463
		// (get) Token: 0x06011FBA RID: 73658
		// (set) Token: 0x06011FB9 RID: 73657
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

		// Token: 0x17005F90 RID: 24464
		// (get) Token: 0x06011FBC RID: 73660
		// (set) Token: 0x06011FBB RID: 73659
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

		// Token: 0x17005F91 RID: 24465
		// (get) Token: 0x06011FBE RID: 73662
		// (set) Token: 0x06011FBD RID: 73661
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

		// Token: 0x06011FBF RID: 73663
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17005F92 RID: 24466
		// (get) Token: 0x06011FC1 RID: 73665
		// (set) Token: 0x06011FC0 RID: 73664
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

		// Token: 0x17005F93 RID: 24467
		// (get) Token: 0x06011FC2 RID: 73666
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F94 RID: 24468
		// (get) Token: 0x06011FC4 RID: 73668
		// (set) Token: 0x06011FC3 RID: 73667
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

		// Token: 0x17005F95 RID: 24469
		// (get) Token: 0x06011FC6 RID: 73670
		// (set) Token: 0x06011FC5 RID: 73669
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

		// Token: 0x17005F96 RID: 24470
		// (get) Token: 0x06011FC7 RID: 73671
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005F97 RID: 24471
		// (get) Token: 0x06011FC9 RID: 73673
		// (set) Token: 0x06011FC8 RID: 73672
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

		// Token: 0x17005F98 RID: 24472
		// (get) Token: 0x06011FCB RID: 73675
		// (set) Token: 0x06011FCA RID: 73674
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

		// Token: 0x06011FCC RID: 73676
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17005F99 RID: 24473
		// (get) Token: 0x06011FCE RID: 73678
		// (set) Token: 0x06011FCD RID: 73677
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

		// Token: 0x17005F9A RID: 24474
		// (get) Token: 0x06011FD0 RID: 73680
		// (set) Token: 0x06011FCF RID: 73679
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

		// Token: 0x17005F9B RID: 24475
		// (get) Token: 0x06011FD2 RID: 73682
		// (set) Token: 0x06011FD1 RID: 73681
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

		// Token: 0x17005F9C RID: 24476
		// (get) Token: 0x06011FD4 RID: 73684
		// (set) Token: 0x06011FD3 RID: 73683
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

		// Token: 0x17005F9D RID: 24477
		// (get) Token: 0x06011FD6 RID: 73686
		// (set) Token: 0x06011FD5 RID: 73685
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

		// Token: 0x17005F9E RID: 24478
		// (get) Token: 0x06011FD8 RID: 73688
		// (set) Token: 0x06011FD7 RID: 73687
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

		// Token: 0x17005F9F RID: 24479
		// (get) Token: 0x06011FDA RID: 73690
		// (set) Token: 0x06011FD9 RID: 73689
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

		// Token: 0x17005FA0 RID: 24480
		// (get) Token: 0x06011FDC RID: 73692
		// (set) Token: 0x06011FDB RID: 73691
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

		// Token: 0x06011FDD RID: 73693
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17005FA1 RID: 24481
		// (get) Token: 0x06011FDE RID: 73694
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005FA2 RID: 24482
		// (get) Token: 0x06011FE0 RID: 73696
		// (set) Token: 0x06011FDF RID: 73695
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

		// Token: 0x06011FE1 RID: 73697
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06011FE2 RID: 73698
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06011FE3 RID: 73699
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06011FE4 RID: 73700
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17005FA3 RID: 24483
		// (get) Token: 0x06011FE6 RID: 73702
		// (set) Token: 0x06011FE5 RID: 73701
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

		// Token: 0x17005FA4 RID: 24484
		// (get) Token: 0x06011FE8 RID: 73704
		// (set) Token: 0x06011FE7 RID: 73703
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

		// Token: 0x17005FA5 RID: 24485
		// (get) Token: 0x06011FEA RID: 73706
		// (set) Token: 0x06011FE9 RID: 73705
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

		// Token: 0x17005FA6 RID: 24486
		// (get) Token: 0x06011FEB RID: 73707
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005FA7 RID: 24487
		// (get) Token: 0x06011FEC RID: 73708
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005FA8 RID: 24488
		// (get) Token: 0x06011FED RID: 73709
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005FA9 RID: 24489
		// (get) Token: 0x06011FEE RID: 73710
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06011FEF RID: 73711
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17005FAA RID: 24490
		// (get) Token: 0x06011FF0 RID: 73712
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005FAB RID: 24491
		// (get) Token: 0x06011FF1 RID: 73713
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06011FF2 RID: 73714
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06011FF3 RID: 73715
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06011FF4 RID: 73716
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06011FF5 RID: 73717
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06011FF6 RID: 73718
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06011FF7 RID: 73719
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06011FF8 RID: 73720
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06011FF9 RID: 73721
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17005FAC RID: 24492
		// (get) Token: 0x06011FFA RID: 73722
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005FAD RID: 24493
		// (get) Token: 0x06011FFC RID: 73724
		// (set) Token: 0x06011FFB RID: 73723
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

		// Token: 0x17005FAE RID: 24494
		// (get) Token: 0x06011FFD RID: 73725
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005FAF RID: 24495
		// (get) Token: 0x06011FFE RID: 73726
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005FB0 RID: 24496
		// (get) Token: 0x06011FFF RID: 73727
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005FB1 RID: 24497
		// (get) Token: 0x06012000 RID: 73728
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005FB2 RID: 24498
		// (get) Token: 0x06012001 RID: 73729
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005FB3 RID: 24499
		// (get) Token: 0x06012003 RID: 73731
		// (set) Token: 0x06012002 RID: 73730
		public virtual extern string IHTMLScriptElement_src
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

		// Token: 0x17005FB4 RID: 24500
		// (get) Token: 0x06012005 RID: 73733
		// (set) Token: 0x06012004 RID: 73732
		public virtual extern string IHTMLScriptElement_htmlFor
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

		// Token: 0x17005FB5 RID: 24501
		// (get) Token: 0x06012007 RID: 73735
		// (set) Token: 0x06012006 RID: 73734
		public virtual extern string IHTMLScriptElement_event
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

		// Token: 0x17005FB6 RID: 24502
		// (get) Token: 0x06012009 RID: 73737
		// (set) Token: 0x06012008 RID: 73736
		public virtual extern string IHTMLScriptElement_text
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

		// Token: 0x17005FB7 RID: 24503
		// (get) Token: 0x0601200B RID: 73739
		// (set) Token: 0x0601200A RID: 73738
		public virtual extern bool IHTMLScriptElement_defer
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17005FB8 RID: 24504
		// (get) Token: 0x0601200C RID: 73740
		public virtual extern string IHTMLScriptElement_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005FB9 RID: 24505
		// (get) Token: 0x0601200E RID: 73742
		// (set) Token: 0x0601200D RID: 73741
		public virtual extern object IHTMLScriptElement_onerror
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005FBA RID: 24506
		// (get) Token: 0x06012010 RID: 73744
		// (set) Token: 0x0601200F RID: 73743
		public virtual extern string IHTMLScriptElement_type
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

		// Token: 0x17005FBB RID: 24507
		// (get) Token: 0x06012012 RID: 73746
		// (set) Token: 0x06012011 RID: 73745
		public virtual extern string IHTMLScriptElement2_charset
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

		// Token: 0x14002265 RID: 8805
		// (add) Token: 0x06012013 RID: 73747
		// (remove) Token: 0x06012014 RID: 73748
		public virtual extern event HTMLScriptEvents_onhelpEventHandler HTMLScriptEvents_Event_onhelp;

		// Token: 0x14002266 RID: 8806
		// (add) Token: 0x06012015 RID: 73749
		// (remove) Token: 0x06012016 RID: 73750
		public virtual extern event HTMLScriptEvents_onclickEventHandler HTMLScriptEvents_Event_onclick;

		// Token: 0x14002267 RID: 8807
		// (add) Token: 0x06012017 RID: 73751
		// (remove) Token: 0x06012018 RID: 73752
		public virtual extern event HTMLScriptEvents_ondblclickEventHandler HTMLScriptEvents_Event_ondblclick;

		// Token: 0x14002268 RID: 8808
		// (add) Token: 0x06012019 RID: 73753
		// (remove) Token: 0x0601201A RID: 73754
		public virtual extern event HTMLScriptEvents_onkeypressEventHandler HTMLScriptEvents_Event_onkeypress;

		// Token: 0x14002269 RID: 8809
		// (add) Token: 0x0601201B RID: 73755
		// (remove) Token: 0x0601201C RID: 73756
		public virtual extern event HTMLScriptEvents_onkeydownEventHandler HTMLScriptEvents_Event_onkeydown;

		// Token: 0x1400226A RID: 8810
		// (add) Token: 0x0601201D RID: 73757
		// (remove) Token: 0x0601201E RID: 73758
		public virtual extern event HTMLScriptEvents_onkeyupEventHandler HTMLScriptEvents_Event_onkeyup;

		// Token: 0x1400226B RID: 8811
		// (add) Token: 0x0601201F RID: 73759
		// (remove) Token: 0x06012020 RID: 73760
		public virtual extern event HTMLScriptEvents_onmouseoutEventHandler HTMLScriptEvents_Event_onmouseout;

		// Token: 0x1400226C RID: 8812
		// (add) Token: 0x06012021 RID: 73761
		// (remove) Token: 0x06012022 RID: 73762
		public virtual extern event HTMLScriptEvents_onmouseoverEventHandler HTMLScriptEvents_Event_onmouseover;

		// Token: 0x1400226D RID: 8813
		// (add) Token: 0x06012023 RID: 73763
		// (remove) Token: 0x06012024 RID: 73764
		public virtual extern event HTMLScriptEvents_onmousemoveEventHandler HTMLScriptEvents_Event_onmousemove;

		// Token: 0x1400226E RID: 8814
		// (add) Token: 0x06012025 RID: 73765
		// (remove) Token: 0x06012026 RID: 73766
		public virtual extern event HTMLScriptEvents_onmousedownEventHandler HTMLScriptEvents_Event_onmousedown;

		// Token: 0x1400226F RID: 8815
		// (add) Token: 0x06012027 RID: 73767
		// (remove) Token: 0x06012028 RID: 73768
		public virtual extern event HTMLScriptEvents_onmouseupEventHandler HTMLScriptEvents_Event_onmouseup;

		// Token: 0x14002270 RID: 8816
		// (add) Token: 0x06012029 RID: 73769
		// (remove) Token: 0x0601202A RID: 73770
		public virtual extern event HTMLScriptEvents_onselectstartEventHandler HTMLScriptEvents_Event_onselectstart;

		// Token: 0x14002271 RID: 8817
		// (add) Token: 0x0601202B RID: 73771
		// (remove) Token: 0x0601202C RID: 73772
		public virtual extern event HTMLScriptEvents_onfilterchangeEventHandler HTMLScriptEvents_Event_onfilterchange;

		// Token: 0x14002272 RID: 8818
		// (add) Token: 0x0601202D RID: 73773
		// (remove) Token: 0x0601202E RID: 73774
		public virtual extern event HTMLScriptEvents_ondragstartEventHandler HTMLScriptEvents_Event_ondragstart;

		// Token: 0x14002273 RID: 8819
		// (add) Token: 0x0601202F RID: 73775
		// (remove) Token: 0x06012030 RID: 73776
		public virtual extern event HTMLScriptEvents_onbeforeupdateEventHandler HTMLScriptEvents_Event_onbeforeupdate;

		// Token: 0x14002274 RID: 8820
		// (add) Token: 0x06012031 RID: 73777
		// (remove) Token: 0x06012032 RID: 73778
		public virtual extern event HTMLScriptEvents_onafterupdateEventHandler HTMLScriptEvents_Event_onafterupdate;

		// Token: 0x14002275 RID: 8821
		// (add) Token: 0x06012033 RID: 73779
		// (remove) Token: 0x06012034 RID: 73780
		public virtual extern event HTMLScriptEvents_onerrorupdateEventHandler HTMLScriptEvents_Event_onerrorupdate;

		// Token: 0x14002276 RID: 8822
		// (add) Token: 0x06012035 RID: 73781
		// (remove) Token: 0x06012036 RID: 73782
		public virtual extern event HTMLScriptEvents_onrowexitEventHandler HTMLScriptEvents_Event_onrowexit;

		// Token: 0x14002277 RID: 8823
		// (add) Token: 0x06012037 RID: 73783
		// (remove) Token: 0x06012038 RID: 73784
		public virtual extern event HTMLScriptEvents_onrowenterEventHandler HTMLScriptEvents_Event_onrowenter;

		// Token: 0x14002278 RID: 8824
		// (add) Token: 0x06012039 RID: 73785
		// (remove) Token: 0x0601203A RID: 73786
		public virtual extern event HTMLScriptEvents_ondatasetchangedEventHandler HTMLScriptEvents_Event_ondatasetchanged;

		// Token: 0x14002279 RID: 8825
		// (add) Token: 0x0601203B RID: 73787
		// (remove) Token: 0x0601203C RID: 73788
		public virtual extern event HTMLScriptEvents_ondataavailableEventHandler HTMLScriptEvents_Event_ondataavailable;

		// Token: 0x1400227A RID: 8826
		// (add) Token: 0x0601203D RID: 73789
		// (remove) Token: 0x0601203E RID: 73790
		public virtual extern event HTMLScriptEvents_ondatasetcompleteEventHandler HTMLScriptEvents_Event_ondatasetcomplete;

		// Token: 0x1400227B RID: 8827
		// (add) Token: 0x0601203F RID: 73791
		// (remove) Token: 0x06012040 RID: 73792
		public virtual extern event HTMLScriptEvents_onlosecaptureEventHandler HTMLScriptEvents_Event_onlosecapture;

		// Token: 0x1400227C RID: 8828
		// (add) Token: 0x06012041 RID: 73793
		// (remove) Token: 0x06012042 RID: 73794
		public virtual extern event HTMLScriptEvents_onpropertychangeEventHandler HTMLScriptEvents_Event_onpropertychange;

		// Token: 0x1400227D RID: 8829
		// (add) Token: 0x06012043 RID: 73795
		// (remove) Token: 0x06012044 RID: 73796
		public virtual extern event HTMLScriptEvents_onscrollEventHandler HTMLScriptEvents_Event_onscroll;

		// Token: 0x1400227E RID: 8830
		// (add) Token: 0x06012045 RID: 73797
		// (remove) Token: 0x06012046 RID: 73798
		public virtual extern event HTMLScriptEvents_onfocusEventHandler HTMLScriptEvents_Event_onfocus;

		// Token: 0x1400227F RID: 8831
		// (add) Token: 0x06012047 RID: 73799
		// (remove) Token: 0x06012048 RID: 73800
		public virtual extern event HTMLScriptEvents_onblurEventHandler HTMLScriptEvents_Event_onblur;

		// Token: 0x14002280 RID: 8832
		// (add) Token: 0x06012049 RID: 73801
		// (remove) Token: 0x0601204A RID: 73802
		public virtual extern event HTMLScriptEvents_onresizeEventHandler HTMLScriptEvents_Event_onresize;

		// Token: 0x14002281 RID: 8833
		// (add) Token: 0x0601204B RID: 73803
		// (remove) Token: 0x0601204C RID: 73804
		public virtual extern event HTMLScriptEvents_ondragEventHandler HTMLScriptEvents_Event_ondrag;

		// Token: 0x14002282 RID: 8834
		// (add) Token: 0x0601204D RID: 73805
		// (remove) Token: 0x0601204E RID: 73806
		public virtual extern event HTMLScriptEvents_ondragendEventHandler HTMLScriptEvents_Event_ondragend;

		// Token: 0x14002283 RID: 8835
		// (add) Token: 0x0601204F RID: 73807
		// (remove) Token: 0x06012050 RID: 73808
		public virtual extern event HTMLScriptEvents_ondragenterEventHandler HTMLScriptEvents_Event_ondragenter;

		// Token: 0x14002284 RID: 8836
		// (add) Token: 0x06012051 RID: 73809
		// (remove) Token: 0x06012052 RID: 73810
		public virtual extern event HTMLScriptEvents_ondragoverEventHandler HTMLScriptEvents_Event_ondragover;

		// Token: 0x14002285 RID: 8837
		// (add) Token: 0x06012053 RID: 73811
		// (remove) Token: 0x06012054 RID: 73812
		public virtual extern event HTMLScriptEvents_ondragleaveEventHandler HTMLScriptEvents_Event_ondragleave;

		// Token: 0x14002286 RID: 8838
		// (add) Token: 0x06012055 RID: 73813
		// (remove) Token: 0x06012056 RID: 73814
		public virtual extern event HTMLScriptEvents_ondropEventHandler HTMLScriptEvents_Event_ondrop;

		// Token: 0x14002287 RID: 8839
		// (add) Token: 0x06012057 RID: 73815
		// (remove) Token: 0x06012058 RID: 73816
		public virtual extern event HTMLScriptEvents_onbeforecutEventHandler HTMLScriptEvents_Event_onbeforecut;

		// Token: 0x14002288 RID: 8840
		// (add) Token: 0x06012059 RID: 73817
		// (remove) Token: 0x0601205A RID: 73818
		public virtual extern event HTMLScriptEvents_oncutEventHandler HTMLScriptEvents_Event_oncut;

		// Token: 0x14002289 RID: 8841
		// (add) Token: 0x0601205B RID: 73819
		// (remove) Token: 0x0601205C RID: 73820
		public virtual extern event HTMLScriptEvents_onbeforecopyEventHandler HTMLScriptEvents_Event_onbeforecopy;

		// Token: 0x1400228A RID: 8842
		// (add) Token: 0x0601205D RID: 73821
		// (remove) Token: 0x0601205E RID: 73822
		public virtual extern event HTMLScriptEvents_oncopyEventHandler HTMLScriptEvents_Event_oncopy;

		// Token: 0x1400228B RID: 8843
		// (add) Token: 0x0601205F RID: 73823
		// (remove) Token: 0x06012060 RID: 73824
		public virtual extern event HTMLScriptEvents_onbeforepasteEventHandler HTMLScriptEvents_Event_onbeforepaste;

		// Token: 0x1400228C RID: 8844
		// (add) Token: 0x06012061 RID: 73825
		// (remove) Token: 0x06012062 RID: 73826
		public virtual extern event HTMLScriptEvents_onpasteEventHandler HTMLScriptEvents_Event_onpaste;

		// Token: 0x1400228D RID: 8845
		// (add) Token: 0x06012063 RID: 73827
		// (remove) Token: 0x06012064 RID: 73828
		public virtual extern event HTMLScriptEvents_oncontextmenuEventHandler HTMLScriptEvents_Event_oncontextmenu;

		// Token: 0x1400228E RID: 8846
		// (add) Token: 0x06012065 RID: 73829
		// (remove) Token: 0x06012066 RID: 73830
		public virtual extern event HTMLScriptEvents_onrowsdeleteEventHandler HTMLScriptEvents_Event_onrowsdelete;

		// Token: 0x1400228F RID: 8847
		// (add) Token: 0x06012067 RID: 73831
		// (remove) Token: 0x06012068 RID: 73832
		public virtual extern event HTMLScriptEvents_onrowsinsertedEventHandler HTMLScriptEvents_Event_onrowsinserted;

		// Token: 0x14002290 RID: 8848
		// (add) Token: 0x06012069 RID: 73833
		// (remove) Token: 0x0601206A RID: 73834
		public virtual extern event HTMLScriptEvents_oncellchangeEventHandler HTMLScriptEvents_Event_oncellchange;

		// Token: 0x14002291 RID: 8849
		// (add) Token: 0x0601206B RID: 73835
		// (remove) Token: 0x0601206C RID: 73836
		public virtual extern event HTMLScriptEvents_onreadystatechangeEventHandler HTMLScriptEvents_Event_onreadystatechange;

		// Token: 0x14002292 RID: 8850
		// (add) Token: 0x0601206D RID: 73837
		// (remove) Token: 0x0601206E RID: 73838
		public virtual extern event HTMLScriptEvents_onbeforeeditfocusEventHandler HTMLScriptEvents_Event_onbeforeeditfocus;

		// Token: 0x14002293 RID: 8851
		// (add) Token: 0x0601206F RID: 73839
		// (remove) Token: 0x06012070 RID: 73840
		public virtual extern event HTMLScriptEvents_onlayoutcompleteEventHandler HTMLScriptEvents_Event_onlayoutcomplete;

		// Token: 0x14002294 RID: 8852
		// (add) Token: 0x06012071 RID: 73841
		// (remove) Token: 0x06012072 RID: 73842
		public virtual extern event HTMLScriptEvents_onpageEventHandler HTMLScriptEvents_Event_onpage;

		// Token: 0x14002295 RID: 8853
		// (add) Token: 0x06012073 RID: 73843
		// (remove) Token: 0x06012074 RID: 73844
		public virtual extern event HTMLScriptEvents_onbeforedeactivateEventHandler HTMLScriptEvents_Event_onbeforedeactivate;

		// Token: 0x14002296 RID: 8854
		// (add) Token: 0x06012075 RID: 73845
		// (remove) Token: 0x06012076 RID: 73846
		public virtual extern event HTMLScriptEvents_onbeforeactivateEventHandler HTMLScriptEvents_Event_onbeforeactivate;

		// Token: 0x14002297 RID: 8855
		// (add) Token: 0x06012077 RID: 73847
		// (remove) Token: 0x06012078 RID: 73848
		public virtual extern event HTMLScriptEvents_onmoveEventHandler HTMLScriptEvents_Event_onmove;

		// Token: 0x14002298 RID: 8856
		// (add) Token: 0x06012079 RID: 73849
		// (remove) Token: 0x0601207A RID: 73850
		public virtual extern event HTMLScriptEvents_oncontrolselectEventHandler HTMLScriptEvents_Event_oncontrolselect;

		// Token: 0x14002299 RID: 8857
		// (add) Token: 0x0601207B RID: 73851
		// (remove) Token: 0x0601207C RID: 73852
		public virtual extern event HTMLScriptEvents_onmovestartEventHandler HTMLScriptEvents_Event_onmovestart;

		// Token: 0x1400229A RID: 8858
		// (add) Token: 0x0601207D RID: 73853
		// (remove) Token: 0x0601207E RID: 73854
		public virtual extern event HTMLScriptEvents_onmoveendEventHandler HTMLScriptEvents_Event_onmoveend;

		// Token: 0x1400229B RID: 8859
		// (add) Token: 0x0601207F RID: 73855
		// (remove) Token: 0x06012080 RID: 73856
		public virtual extern event HTMLScriptEvents_onresizestartEventHandler HTMLScriptEvents_Event_onresizestart;

		// Token: 0x1400229C RID: 8860
		// (add) Token: 0x06012081 RID: 73857
		// (remove) Token: 0x06012082 RID: 73858
		public virtual extern event HTMLScriptEvents_onresizeendEventHandler HTMLScriptEvents_Event_onresizeend;

		// Token: 0x1400229D RID: 8861
		// (add) Token: 0x06012083 RID: 73859
		// (remove) Token: 0x06012084 RID: 73860
		public virtual extern event HTMLScriptEvents_onmouseenterEventHandler HTMLScriptEvents_Event_onmouseenter;

		// Token: 0x1400229E RID: 8862
		// (add) Token: 0x06012085 RID: 73861
		// (remove) Token: 0x06012086 RID: 73862
		public virtual extern event HTMLScriptEvents_onmouseleaveEventHandler HTMLScriptEvents_Event_onmouseleave;

		// Token: 0x1400229F RID: 8863
		// (add) Token: 0x06012087 RID: 73863
		// (remove) Token: 0x06012088 RID: 73864
		public virtual extern event HTMLScriptEvents_onmousewheelEventHandler HTMLScriptEvents_Event_onmousewheel;

		// Token: 0x140022A0 RID: 8864
		// (add) Token: 0x06012089 RID: 73865
		// (remove) Token: 0x0601208A RID: 73866
		public virtual extern event HTMLScriptEvents_onactivateEventHandler HTMLScriptEvents_Event_onactivate;

		// Token: 0x140022A1 RID: 8865
		// (add) Token: 0x0601208B RID: 73867
		// (remove) Token: 0x0601208C RID: 73868
		public virtual extern event HTMLScriptEvents_ondeactivateEventHandler HTMLScriptEvents_Event_ondeactivate;

		// Token: 0x140022A2 RID: 8866
		// (add) Token: 0x0601208D RID: 73869
		// (remove) Token: 0x0601208E RID: 73870
		public virtual extern event HTMLScriptEvents_onfocusinEventHandler HTMLScriptEvents_Event_onfocusin;

		// Token: 0x140022A3 RID: 8867
		// (add) Token: 0x0601208F RID: 73871
		// (remove) Token: 0x06012090 RID: 73872
		public virtual extern event HTMLScriptEvents_onfocusoutEventHandler HTMLScriptEvents_Event_onfocusout;

		// Token: 0x140022A4 RID: 8868
		// (add) Token: 0x06012091 RID: 73873
		// (remove) Token: 0x06012092 RID: 73874
		public virtual extern event HTMLScriptEvents_onerrorEventHandler HTMLScriptEvents_Event_onerror;

		// Token: 0x140022A5 RID: 8869
		// (add) Token: 0x06012093 RID: 73875
		// (remove) Token: 0x06012094 RID: 73876
		public virtual extern event HTMLScriptEvents2_onhelpEventHandler HTMLScriptEvents2_Event_onhelp;

		// Token: 0x140022A6 RID: 8870
		// (add) Token: 0x06012095 RID: 73877
		// (remove) Token: 0x06012096 RID: 73878
		public virtual extern event HTMLScriptEvents2_onclickEventHandler HTMLScriptEvents2_Event_onclick;

		// Token: 0x140022A7 RID: 8871
		// (add) Token: 0x06012097 RID: 73879
		// (remove) Token: 0x06012098 RID: 73880
		public virtual extern event HTMLScriptEvents2_ondblclickEventHandler HTMLScriptEvents2_Event_ondblclick;

		// Token: 0x140022A8 RID: 8872
		// (add) Token: 0x06012099 RID: 73881
		// (remove) Token: 0x0601209A RID: 73882
		public virtual extern event HTMLScriptEvents2_onkeypressEventHandler HTMLScriptEvents2_Event_onkeypress;

		// Token: 0x140022A9 RID: 8873
		// (add) Token: 0x0601209B RID: 73883
		// (remove) Token: 0x0601209C RID: 73884
		public virtual extern event HTMLScriptEvents2_onkeydownEventHandler HTMLScriptEvents2_Event_onkeydown;

		// Token: 0x140022AA RID: 8874
		// (add) Token: 0x0601209D RID: 73885
		// (remove) Token: 0x0601209E RID: 73886
		public virtual extern event HTMLScriptEvents2_onkeyupEventHandler HTMLScriptEvents2_Event_onkeyup;

		// Token: 0x140022AB RID: 8875
		// (add) Token: 0x0601209F RID: 73887
		// (remove) Token: 0x060120A0 RID: 73888
		public virtual extern event HTMLScriptEvents2_onmouseoutEventHandler HTMLScriptEvents2_Event_onmouseout;

		// Token: 0x140022AC RID: 8876
		// (add) Token: 0x060120A1 RID: 73889
		// (remove) Token: 0x060120A2 RID: 73890
		public virtual extern event HTMLScriptEvents2_onmouseoverEventHandler HTMLScriptEvents2_Event_onmouseover;

		// Token: 0x140022AD RID: 8877
		// (add) Token: 0x060120A3 RID: 73891
		// (remove) Token: 0x060120A4 RID: 73892
		public virtual extern event HTMLScriptEvents2_onmousemoveEventHandler HTMLScriptEvents2_Event_onmousemove;

		// Token: 0x140022AE RID: 8878
		// (add) Token: 0x060120A5 RID: 73893
		// (remove) Token: 0x060120A6 RID: 73894
		public virtual extern event HTMLScriptEvents2_onmousedownEventHandler HTMLScriptEvents2_Event_onmousedown;

		// Token: 0x140022AF RID: 8879
		// (add) Token: 0x060120A7 RID: 73895
		// (remove) Token: 0x060120A8 RID: 73896
		public virtual extern event HTMLScriptEvents2_onmouseupEventHandler HTMLScriptEvents2_Event_onmouseup;

		// Token: 0x140022B0 RID: 8880
		// (add) Token: 0x060120A9 RID: 73897
		// (remove) Token: 0x060120AA RID: 73898
		public virtual extern event HTMLScriptEvents2_onselectstartEventHandler HTMLScriptEvents2_Event_onselectstart;

		// Token: 0x140022B1 RID: 8881
		// (add) Token: 0x060120AB RID: 73899
		// (remove) Token: 0x060120AC RID: 73900
		public virtual extern event HTMLScriptEvents2_onfilterchangeEventHandler HTMLScriptEvents2_Event_onfilterchange;

		// Token: 0x140022B2 RID: 8882
		// (add) Token: 0x060120AD RID: 73901
		// (remove) Token: 0x060120AE RID: 73902
		public virtual extern event HTMLScriptEvents2_ondragstartEventHandler HTMLScriptEvents2_Event_ondragstart;

		// Token: 0x140022B3 RID: 8883
		// (add) Token: 0x060120AF RID: 73903
		// (remove) Token: 0x060120B0 RID: 73904
		public virtual extern event HTMLScriptEvents2_onbeforeupdateEventHandler HTMLScriptEvents2_Event_onbeforeupdate;

		// Token: 0x140022B4 RID: 8884
		// (add) Token: 0x060120B1 RID: 73905
		// (remove) Token: 0x060120B2 RID: 73906
		public virtual extern event HTMLScriptEvents2_onafterupdateEventHandler HTMLScriptEvents2_Event_onafterupdate;

		// Token: 0x140022B5 RID: 8885
		// (add) Token: 0x060120B3 RID: 73907
		// (remove) Token: 0x060120B4 RID: 73908
		public virtual extern event HTMLScriptEvents2_onerrorupdateEventHandler HTMLScriptEvents2_Event_onerrorupdate;

		// Token: 0x140022B6 RID: 8886
		// (add) Token: 0x060120B5 RID: 73909
		// (remove) Token: 0x060120B6 RID: 73910
		public virtual extern event HTMLScriptEvents2_onrowexitEventHandler HTMLScriptEvents2_Event_onrowexit;

		// Token: 0x140022B7 RID: 8887
		// (add) Token: 0x060120B7 RID: 73911
		// (remove) Token: 0x060120B8 RID: 73912
		public virtual extern event HTMLScriptEvents2_onrowenterEventHandler HTMLScriptEvents2_Event_onrowenter;

		// Token: 0x140022B8 RID: 8888
		// (add) Token: 0x060120B9 RID: 73913
		// (remove) Token: 0x060120BA RID: 73914
		public virtual extern event HTMLScriptEvents2_ondatasetchangedEventHandler HTMLScriptEvents2_Event_ondatasetchanged;

		// Token: 0x140022B9 RID: 8889
		// (add) Token: 0x060120BB RID: 73915
		// (remove) Token: 0x060120BC RID: 73916
		public virtual extern event HTMLScriptEvents2_ondataavailableEventHandler HTMLScriptEvents2_Event_ondataavailable;

		// Token: 0x140022BA RID: 8890
		// (add) Token: 0x060120BD RID: 73917
		// (remove) Token: 0x060120BE RID: 73918
		public virtual extern event HTMLScriptEvents2_ondatasetcompleteEventHandler HTMLScriptEvents2_Event_ondatasetcomplete;

		// Token: 0x140022BB RID: 8891
		// (add) Token: 0x060120BF RID: 73919
		// (remove) Token: 0x060120C0 RID: 73920
		public virtual extern event HTMLScriptEvents2_onlosecaptureEventHandler HTMLScriptEvents2_Event_onlosecapture;

		// Token: 0x140022BC RID: 8892
		// (add) Token: 0x060120C1 RID: 73921
		// (remove) Token: 0x060120C2 RID: 73922
		public virtual extern event HTMLScriptEvents2_onpropertychangeEventHandler HTMLScriptEvents2_Event_onpropertychange;

		// Token: 0x140022BD RID: 8893
		// (add) Token: 0x060120C3 RID: 73923
		// (remove) Token: 0x060120C4 RID: 73924
		public virtual extern event HTMLScriptEvents2_onscrollEventHandler HTMLScriptEvents2_Event_onscroll;

		// Token: 0x140022BE RID: 8894
		// (add) Token: 0x060120C5 RID: 73925
		// (remove) Token: 0x060120C6 RID: 73926
		public virtual extern event HTMLScriptEvents2_onfocusEventHandler HTMLScriptEvents2_Event_onfocus;

		// Token: 0x140022BF RID: 8895
		// (add) Token: 0x060120C7 RID: 73927
		// (remove) Token: 0x060120C8 RID: 73928
		public virtual extern event HTMLScriptEvents2_onblurEventHandler HTMLScriptEvents2_Event_onblur;

		// Token: 0x140022C0 RID: 8896
		// (add) Token: 0x060120C9 RID: 73929
		// (remove) Token: 0x060120CA RID: 73930
		public virtual extern event HTMLScriptEvents2_onresizeEventHandler HTMLScriptEvents2_Event_onresize;

		// Token: 0x140022C1 RID: 8897
		// (add) Token: 0x060120CB RID: 73931
		// (remove) Token: 0x060120CC RID: 73932
		public virtual extern event HTMLScriptEvents2_ondragEventHandler HTMLScriptEvents2_Event_ondrag;

		// Token: 0x140022C2 RID: 8898
		// (add) Token: 0x060120CD RID: 73933
		// (remove) Token: 0x060120CE RID: 73934
		public virtual extern event HTMLScriptEvents2_ondragendEventHandler HTMLScriptEvents2_Event_ondragend;

		// Token: 0x140022C3 RID: 8899
		// (add) Token: 0x060120CF RID: 73935
		// (remove) Token: 0x060120D0 RID: 73936
		public virtual extern event HTMLScriptEvents2_ondragenterEventHandler HTMLScriptEvents2_Event_ondragenter;

		// Token: 0x140022C4 RID: 8900
		// (add) Token: 0x060120D1 RID: 73937
		// (remove) Token: 0x060120D2 RID: 73938
		public virtual extern event HTMLScriptEvents2_ondragoverEventHandler HTMLScriptEvents2_Event_ondragover;

		// Token: 0x140022C5 RID: 8901
		// (add) Token: 0x060120D3 RID: 73939
		// (remove) Token: 0x060120D4 RID: 73940
		public virtual extern event HTMLScriptEvents2_ondragleaveEventHandler HTMLScriptEvents2_Event_ondragleave;

		// Token: 0x140022C6 RID: 8902
		// (add) Token: 0x060120D5 RID: 73941
		// (remove) Token: 0x060120D6 RID: 73942
		public virtual extern event HTMLScriptEvents2_ondropEventHandler HTMLScriptEvents2_Event_ondrop;

		// Token: 0x140022C7 RID: 8903
		// (add) Token: 0x060120D7 RID: 73943
		// (remove) Token: 0x060120D8 RID: 73944
		public virtual extern event HTMLScriptEvents2_onbeforecutEventHandler HTMLScriptEvents2_Event_onbeforecut;

		// Token: 0x140022C8 RID: 8904
		// (add) Token: 0x060120D9 RID: 73945
		// (remove) Token: 0x060120DA RID: 73946
		public virtual extern event HTMLScriptEvents2_oncutEventHandler HTMLScriptEvents2_Event_oncut;

		// Token: 0x140022C9 RID: 8905
		// (add) Token: 0x060120DB RID: 73947
		// (remove) Token: 0x060120DC RID: 73948
		public virtual extern event HTMLScriptEvents2_onbeforecopyEventHandler HTMLScriptEvents2_Event_onbeforecopy;

		// Token: 0x140022CA RID: 8906
		// (add) Token: 0x060120DD RID: 73949
		// (remove) Token: 0x060120DE RID: 73950
		public virtual extern event HTMLScriptEvents2_oncopyEventHandler HTMLScriptEvents2_Event_oncopy;

		// Token: 0x140022CB RID: 8907
		// (add) Token: 0x060120DF RID: 73951
		// (remove) Token: 0x060120E0 RID: 73952
		public virtual extern event HTMLScriptEvents2_onbeforepasteEventHandler HTMLScriptEvents2_Event_onbeforepaste;

		// Token: 0x140022CC RID: 8908
		// (add) Token: 0x060120E1 RID: 73953
		// (remove) Token: 0x060120E2 RID: 73954
		public virtual extern event HTMLScriptEvents2_onpasteEventHandler HTMLScriptEvents2_Event_onpaste;

		// Token: 0x140022CD RID: 8909
		// (add) Token: 0x060120E3 RID: 73955
		// (remove) Token: 0x060120E4 RID: 73956
		public virtual extern event HTMLScriptEvents2_oncontextmenuEventHandler HTMLScriptEvents2_Event_oncontextmenu;

		// Token: 0x140022CE RID: 8910
		// (add) Token: 0x060120E5 RID: 73957
		// (remove) Token: 0x060120E6 RID: 73958
		public virtual extern event HTMLScriptEvents2_onrowsdeleteEventHandler HTMLScriptEvents2_Event_onrowsdelete;

		// Token: 0x140022CF RID: 8911
		// (add) Token: 0x060120E7 RID: 73959
		// (remove) Token: 0x060120E8 RID: 73960
		public virtual extern event HTMLScriptEvents2_onrowsinsertedEventHandler HTMLScriptEvents2_Event_onrowsinserted;

		// Token: 0x140022D0 RID: 8912
		// (add) Token: 0x060120E9 RID: 73961
		// (remove) Token: 0x060120EA RID: 73962
		public virtual extern event HTMLScriptEvents2_oncellchangeEventHandler HTMLScriptEvents2_Event_oncellchange;

		// Token: 0x140022D1 RID: 8913
		// (add) Token: 0x060120EB RID: 73963
		// (remove) Token: 0x060120EC RID: 73964
		public virtual extern event HTMLScriptEvents2_onreadystatechangeEventHandler HTMLScriptEvents2_Event_onreadystatechange;

		// Token: 0x140022D2 RID: 8914
		// (add) Token: 0x060120ED RID: 73965
		// (remove) Token: 0x060120EE RID: 73966
		public virtual extern event HTMLScriptEvents2_onlayoutcompleteEventHandler HTMLScriptEvents2_Event_onlayoutcomplete;

		// Token: 0x140022D3 RID: 8915
		// (add) Token: 0x060120EF RID: 73967
		// (remove) Token: 0x060120F0 RID: 73968
		public virtual extern event HTMLScriptEvents2_onpageEventHandler HTMLScriptEvents2_Event_onpage;

		// Token: 0x140022D4 RID: 8916
		// (add) Token: 0x060120F1 RID: 73969
		// (remove) Token: 0x060120F2 RID: 73970
		public virtual extern event HTMLScriptEvents2_onmouseenterEventHandler HTMLScriptEvents2_Event_onmouseenter;

		// Token: 0x140022D5 RID: 8917
		// (add) Token: 0x060120F3 RID: 73971
		// (remove) Token: 0x060120F4 RID: 73972
		public virtual extern event HTMLScriptEvents2_onmouseleaveEventHandler HTMLScriptEvents2_Event_onmouseleave;

		// Token: 0x140022D6 RID: 8918
		// (add) Token: 0x060120F5 RID: 73973
		// (remove) Token: 0x060120F6 RID: 73974
		public virtual extern event HTMLScriptEvents2_onactivateEventHandler HTMLScriptEvents2_Event_onactivate;

		// Token: 0x140022D7 RID: 8919
		// (add) Token: 0x060120F7 RID: 73975
		// (remove) Token: 0x060120F8 RID: 73976
		public virtual extern event HTMLScriptEvents2_ondeactivateEventHandler HTMLScriptEvents2_Event_ondeactivate;

		// Token: 0x140022D8 RID: 8920
		// (add) Token: 0x060120F9 RID: 73977
		// (remove) Token: 0x060120FA RID: 73978
		public virtual extern event HTMLScriptEvents2_onbeforedeactivateEventHandler HTMLScriptEvents2_Event_onbeforedeactivate;

		// Token: 0x140022D9 RID: 8921
		// (add) Token: 0x060120FB RID: 73979
		// (remove) Token: 0x060120FC RID: 73980
		public virtual extern event HTMLScriptEvents2_onbeforeactivateEventHandler HTMLScriptEvents2_Event_onbeforeactivate;

		// Token: 0x140022DA RID: 8922
		// (add) Token: 0x060120FD RID: 73981
		// (remove) Token: 0x060120FE RID: 73982
		public virtual extern event HTMLScriptEvents2_onfocusinEventHandler HTMLScriptEvents2_Event_onfocusin;

		// Token: 0x140022DB RID: 8923
		// (add) Token: 0x060120FF RID: 73983
		// (remove) Token: 0x06012100 RID: 73984
		public virtual extern event HTMLScriptEvents2_onfocusoutEventHandler HTMLScriptEvents2_Event_onfocusout;

		// Token: 0x140022DC RID: 8924
		// (add) Token: 0x06012101 RID: 73985
		// (remove) Token: 0x06012102 RID: 73986
		public virtual extern event HTMLScriptEvents2_onmoveEventHandler HTMLScriptEvents2_Event_onmove;

		// Token: 0x140022DD RID: 8925
		// (add) Token: 0x06012103 RID: 73987
		// (remove) Token: 0x06012104 RID: 73988
		public virtual extern event HTMLScriptEvents2_oncontrolselectEventHandler HTMLScriptEvents2_Event_oncontrolselect;

		// Token: 0x140022DE RID: 8926
		// (add) Token: 0x06012105 RID: 73989
		// (remove) Token: 0x06012106 RID: 73990
		public virtual extern event HTMLScriptEvents2_onmovestartEventHandler HTMLScriptEvents2_Event_onmovestart;

		// Token: 0x140022DF RID: 8927
		// (add) Token: 0x06012107 RID: 73991
		// (remove) Token: 0x06012108 RID: 73992
		public virtual extern event HTMLScriptEvents2_onmoveendEventHandler HTMLScriptEvents2_Event_onmoveend;

		// Token: 0x140022E0 RID: 8928
		// (add) Token: 0x06012109 RID: 73993
		// (remove) Token: 0x0601210A RID: 73994
		public virtual extern event HTMLScriptEvents2_onresizestartEventHandler HTMLScriptEvents2_Event_onresizestart;

		// Token: 0x140022E1 RID: 8929
		// (add) Token: 0x0601210B RID: 73995
		// (remove) Token: 0x0601210C RID: 73996
		public virtual extern event HTMLScriptEvents2_onresizeendEventHandler HTMLScriptEvents2_Event_onresizeend;

		// Token: 0x140022E2 RID: 8930
		// (add) Token: 0x0601210D RID: 73997
		// (remove) Token: 0x0601210E RID: 73998
		public virtual extern event HTMLScriptEvents2_onmousewheelEventHandler HTMLScriptEvents2_Event_onmousewheel;

		// Token: 0x140022E3 RID: 8931
		// (add) Token: 0x0601210F RID: 73999
		// (remove) Token: 0x06012110 RID: 74000
		public virtual extern event HTMLScriptEvents2_onerrorEventHandler HTMLScriptEvents2_Event_onerror;
	}
}
