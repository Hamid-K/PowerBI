using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D5D RID: 3421
	[ComSourceInterfaces("mshtml.HTMLOptionButtonElementEvents\0\0")]
	[TypeLibType(2)]
	[Guid("3050F2BE-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[ComImport]
	public class HTMLOptionButtonElementClass : DispIHTMLOptionButtonElement, HTMLOptionButtonElement, HTMLOptionButtonElementEvents_Event, IHTMLOptionButtonElement, IHTMLControlElement, IHTMLElement, IHTMLDatabinding
	{
		// Token: 0x06016FE6 RID: 94182
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLOptionButtonElementClass();

		// Token: 0x06016FE7 RID: 94183
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06016FE8 RID: 94184
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06016FE9 RID: 94185
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17007A6F RID: 31343
		// (get) Token: 0x06016FEB RID: 94187
		// (set) Token: 0x06016FEA RID: 94186
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

		// Token: 0x17007A70 RID: 31344
		// (get) Token: 0x06016FED RID: 94189
		// (set) Token: 0x06016FEC RID: 94188
		[DispId(-2147417110)]
		public virtual extern string id
		{
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007A71 RID: 31345
		// (get) Token: 0x06016FEE RID: 94190
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007A72 RID: 31346
		// (get) Token: 0x06016FEF RID: 94191
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007A73 RID: 31347
		// (get) Token: 0x06016FF0 RID: 94192
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007A74 RID: 31348
		// (get) Token: 0x06016FF2 RID: 94194
		// (set) Token: 0x06016FF1 RID: 94193
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

		// Token: 0x17007A75 RID: 31349
		// (get) Token: 0x06016FF4 RID: 94196
		// (set) Token: 0x06016FF3 RID: 94195
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

		// Token: 0x17007A76 RID: 31350
		// (get) Token: 0x06016FF6 RID: 94198
		// (set) Token: 0x06016FF5 RID: 94197
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

		// Token: 0x17007A77 RID: 31351
		// (get) Token: 0x06016FF8 RID: 94200
		// (set) Token: 0x06016FF7 RID: 94199
		[DispId(-2147412107)]
		public virtual extern object onkeydown
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

		// Token: 0x17007A78 RID: 31352
		// (get) Token: 0x06016FFA RID: 94202
		// (set) Token: 0x06016FF9 RID: 94201
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

		// Token: 0x17007A79 RID: 31353
		// (get) Token: 0x06016FFC RID: 94204
		// (set) Token: 0x06016FFB RID: 94203
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

		// Token: 0x17007A7A RID: 31354
		// (get) Token: 0x06016FFE RID: 94206
		// (set) Token: 0x06016FFD RID: 94205
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

		// Token: 0x17007A7B RID: 31355
		// (get) Token: 0x06017000 RID: 94208
		// (set) Token: 0x06016FFF RID: 94207
		[DispId(-2147412112)]
		public virtual extern object onmouseover
		{
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007A7C RID: 31356
		// (get) Token: 0x06017002 RID: 94210
		// (set) Token: 0x06017001 RID: 94209
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

		// Token: 0x17007A7D RID: 31357
		// (get) Token: 0x06017004 RID: 94212
		// (set) Token: 0x06017003 RID: 94211
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

		// Token: 0x17007A7E RID: 31358
		// (get) Token: 0x06017006 RID: 94214
		// (set) Token: 0x06017005 RID: 94213
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

		// Token: 0x17007A7F RID: 31359
		// (get) Token: 0x06017007 RID: 94215
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007A80 RID: 31360
		// (get) Token: 0x06017009 RID: 94217
		// (set) Token: 0x06017008 RID: 94216
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

		// Token: 0x17007A81 RID: 31361
		// (get) Token: 0x0601700B RID: 94219
		// (set) Token: 0x0601700A RID: 94218
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

		// Token: 0x17007A82 RID: 31362
		// (get) Token: 0x0601700D RID: 94221
		// (set) Token: 0x0601700C RID: 94220
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

		// Token: 0x0601700E RID: 94222
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0601700F RID: 94223
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17007A83 RID: 31363
		// (get) Token: 0x06017010 RID: 94224
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007A84 RID: 31364
		// (get) Token: 0x06017011 RID: 94225
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007A85 RID: 31365
		// (get) Token: 0x06017013 RID: 94227
		// (set) Token: 0x06017012 RID: 94226
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

		// Token: 0x17007A86 RID: 31366
		// (get) Token: 0x06017014 RID: 94228
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007A87 RID: 31367
		// (get) Token: 0x06017015 RID: 94229
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007A88 RID: 31368
		// (get) Token: 0x06017016 RID: 94230
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007A89 RID: 31369
		// (get) Token: 0x06017017 RID: 94231
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007A8A RID: 31370
		// (get) Token: 0x06017018 RID: 94232
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007A8B RID: 31371
		// (get) Token: 0x0601701A RID: 94234
		// (set) Token: 0x06017019 RID: 94233
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

		// Token: 0x17007A8C RID: 31372
		// (get) Token: 0x0601701C RID: 94236
		// (set) Token: 0x0601701B RID: 94235
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

		// Token: 0x17007A8D RID: 31373
		// (get) Token: 0x0601701E RID: 94238
		// (set) Token: 0x0601701D RID: 94237
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

		// Token: 0x17007A8E RID: 31374
		// (get) Token: 0x06017020 RID: 94240
		// (set) Token: 0x0601701F RID: 94239
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

		// Token: 0x06017021 RID: 94241
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06017022 RID: 94242
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17007A8F RID: 31375
		// (get) Token: 0x06017023 RID: 94243
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007A90 RID: 31376
		// (get) Token: 0x06017024 RID: 94244
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06017025 RID: 94245
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17007A91 RID: 31377
		// (get) Token: 0x06017026 RID: 94246
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007A92 RID: 31378
		// (get) Token: 0x06017028 RID: 94248
		// (set) Token: 0x06017027 RID: 94247
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

		// Token: 0x06017029 RID: 94249
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17007A93 RID: 31379
		// (get) Token: 0x0601702B RID: 94251
		// (set) Token: 0x0601702A RID: 94250
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

		// Token: 0x17007A94 RID: 31380
		// (get) Token: 0x0601702D RID: 94253
		// (set) Token: 0x0601702C RID: 94252
		[DispId(-2147412090)]
		public virtual extern object onafterupdate
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

		// Token: 0x17007A95 RID: 31381
		// (get) Token: 0x0601702F RID: 94255
		// (set) Token: 0x0601702E RID: 94254
		[DispId(-2147412074)]
		public virtual extern object onerrorupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007A96 RID: 31382
		// (get) Token: 0x06017031 RID: 94257
		// (set) Token: 0x06017030 RID: 94256
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

		// Token: 0x17007A97 RID: 31383
		// (get) Token: 0x06017033 RID: 94259
		// (set) Token: 0x06017032 RID: 94258
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

		// Token: 0x17007A98 RID: 31384
		// (get) Token: 0x06017035 RID: 94261
		// (set) Token: 0x06017034 RID: 94260
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

		// Token: 0x17007A99 RID: 31385
		// (get) Token: 0x06017037 RID: 94263
		// (set) Token: 0x06017036 RID: 94262
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

		// Token: 0x17007A9A RID: 31386
		// (get) Token: 0x06017039 RID: 94265
		// (set) Token: 0x06017038 RID: 94264
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

		// Token: 0x17007A9B RID: 31387
		// (get) Token: 0x0601703B RID: 94267
		// (set) Token: 0x0601703A RID: 94266
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

		// Token: 0x17007A9C RID: 31388
		// (get) Token: 0x0601703C RID: 94268
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007A9D RID: 31389
		// (get) Token: 0x0601703D RID: 94269
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007A9E RID: 31390
		// (get) Token: 0x0601703F RID: 94271
		// (set) Token: 0x0601703E RID: 94270
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

		// Token: 0x06017040 RID: 94272
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17007A9F RID: 31391
		// (get) Token: 0x06017042 RID: 94274
		// (set) Token: 0x06017041 RID: 94273
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

		// Token: 0x17007AA0 RID: 31392
		// (get) Token: 0x06017044 RID: 94276
		// (set) Token: 0x06017043 RID: 94275
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

		// Token: 0x17007AA1 RID: 31393
		// (get) Token: 0x06017046 RID: 94278
		// (set) Token: 0x06017045 RID: 94277
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

		// Token: 0x17007AA2 RID: 31394
		// (get) Token: 0x06017048 RID: 94280
		// (set) Token: 0x06017047 RID: 94279
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

		// Token: 0x06017049 RID: 94281
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0601704A RID: 94282
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0601704B RID: 94283
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17007AA3 RID: 31395
		// (get) Token: 0x0601704C RID: 94284
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007AA4 RID: 31396
		// (get) Token: 0x0601704D RID: 94285
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007AA5 RID: 31397
		// (get) Token: 0x0601704E RID: 94286
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007AA6 RID: 31398
		// (get) Token: 0x0601704F RID: 94287
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007AA7 RID: 31399
		// (get) Token: 0x06017051 RID: 94289
		// (set) Token: 0x06017050 RID: 94288
		[DispId(-2147413011)]
		public virtual extern string value
		{
			[DispId(-2147413011)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413011)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007AA8 RID: 31400
		// (get) Token: 0x06017052 RID: 94290
		[DispId(2000)]
		public virtual extern string type
		{
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007AA9 RID: 31401
		// (get) Token: 0x06017054 RID: 94292
		// (set) Token: 0x06017053 RID: 94291
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007AAA RID: 31402
		// (get) Token: 0x06017056 RID: 94294
		// (set) Token: 0x06017055 RID: 94293
		[DispId(2009)]
		public virtual extern bool @checked
		{
			[TypeLibFunc(4)]
			[DispId(2009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[DispId(2009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17007AAB RID: 31403
		// (get) Token: 0x06017058 RID: 94296
		// (set) Token: 0x06017057 RID: 94295
		[DispId(2008)]
		public virtual extern bool defaultChecked
		{
			[TypeLibFunc(4)]
			[DispId(2008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[DispId(2008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17007AAC RID: 31404
		// (get) Token: 0x0601705A RID: 94298
		// (set) Token: 0x06017059 RID: 94297
		[DispId(-2147412082)]
		public virtual extern object onchange
		{
			[DispId(-2147412082)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412082)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007AAD RID: 31405
		// (get) Token: 0x0601705C RID: 94300
		// (set) Token: 0x0601705B RID: 94299
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

		// Token: 0x17007AAE RID: 31406
		// (get) Token: 0x0601705E RID: 94302
		// (set) Token: 0x0601705D RID: 94301
		[DispId(2001)]
		public virtual extern bool status
		{
			[DispId(2001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17007AAF RID: 31407
		// (get) Token: 0x06017060 RID: 94304
		// (set) Token: 0x0601705F RID: 94303
		[DispId(2007)]
		public virtual extern bool indeterminate
		{
			[TypeLibFunc(4)]
			[DispId(2007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[DispId(2007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17007AB0 RID: 31408
		// (get) Token: 0x06017061 RID: 94305
		[DispId(-2147416108)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007AB1 RID: 31409
		// (get) Token: 0x06017063 RID: 94307
		// (set) Token: 0x06017062 RID: 94306
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

		// Token: 0x17007AB2 RID: 31410
		// (get) Token: 0x06017065 RID: 94309
		// (set) Token: 0x06017064 RID: 94308
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

		// Token: 0x17007AB3 RID: 31411
		// (get) Token: 0x06017067 RID: 94311
		// (set) Token: 0x06017066 RID: 94310
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

		// Token: 0x17007AB4 RID: 31412
		// (get) Token: 0x06017069 RID: 94313
		// (set) Token: 0x06017068 RID: 94312
		public virtual extern string IHTMLOptionButtonElement_value
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

		// Token: 0x17007AB5 RID: 31413
		// (get) Token: 0x0601706A RID: 94314
		public virtual extern string IHTMLOptionButtonElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007AB6 RID: 31414
		// (get) Token: 0x0601706C RID: 94316
		// (set) Token: 0x0601706B RID: 94315
		public virtual extern string IHTMLOptionButtonElement_name
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

		// Token: 0x17007AB7 RID: 31415
		// (get) Token: 0x0601706E RID: 94318
		// (set) Token: 0x0601706D RID: 94317
		public virtual extern bool IHTMLOptionButtonElement_checked
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007AB8 RID: 31416
		// (get) Token: 0x06017070 RID: 94320
		// (set) Token: 0x0601706F RID: 94319
		public virtual extern bool IHTMLOptionButtonElement_defaultChecked
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007AB9 RID: 31417
		// (get) Token: 0x06017072 RID: 94322
		// (set) Token: 0x06017071 RID: 94321
		public virtual extern object IHTMLOptionButtonElement_onchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007ABA RID: 31418
		// (get) Token: 0x06017074 RID: 94324
		// (set) Token: 0x06017073 RID: 94323
		public virtual extern bool IHTMLOptionButtonElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007ABB RID: 31419
		// (get) Token: 0x06017076 RID: 94326
		// (set) Token: 0x06017075 RID: 94325
		public virtual extern bool IHTMLOptionButtonElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007ABC RID: 31420
		// (get) Token: 0x06017078 RID: 94328
		// (set) Token: 0x06017077 RID: 94327
		public virtual extern bool IHTMLOptionButtonElement_indeterminate
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007ABD RID: 31421
		// (get) Token: 0x06017079 RID: 94329
		public virtual extern IHTMLFormElement IHTMLOptionButtonElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007ABE RID: 31422
		// (get) Token: 0x0601707B RID: 94331
		// (set) Token: 0x0601707A RID: 94330
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

		// Token: 0x0601707C RID: 94332
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17007ABF RID: 31423
		// (get) Token: 0x0601707E RID: 94334
		// (set) Token: 0x0601707D RID: 94333
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

		// Token: 0x17007AC0 RID: 31424
		// (get) Token: 0x06017080 RID: 94336
		// (set) Token: 0x0601707F RID: 94335
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

		// Token: 0x17007AC1 RID: 31425
		// (get) Token: 0x06017082 RID: 94338
		// (set) Token: 0x06017081 RID: 94337
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

		// Token: 0x17007AC2 RID: 31426
		// (get) Token: 0x06017084 RID: 94340
		// (set) Token: 0x06017083 RID: 94339
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

		// Token: 0x06017085 RID: 94341
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06017086 RID: 94342
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06017087 RID: 94343
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17007AC3 RID: 31427
		// (get) Token: 0x06017088 RID: 94344
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007AC4 RID: 31428
		// (get) Token: 0x06017089 RID: 94345
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007AC5 RID: 31429
		// (get) Token: 0x0601708A RID: 94346
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007AC6 RID: 31430
		// (get) Token: 0x0601708B RID: 94347
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601708C RID: 94348
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0601708D RID: 94349
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0601708E RID: 94350
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17007AC7 RID: 31431
		// (get) Token: 0x06017090 RID: 94352
		// (set) Token: 0x0601708F RID: 94351
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

		// Token: 0x17007AC8 RID: 31432
		// (get) Token: 0x06017092 RID: 94354
		// (set) Token: 0x06017091 RID: 94353
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

		// Token: 0x17007AC9 RID: 31433
		// (get) Token: 0x06017093 RID: 94355
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007ACA RID: 31434
		// (get) Token: 0x06017094 RID: 94356
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007ACB RID: 31435
		// (get) Token: 0x06017095 RID: 94357
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007ACC RID: 31436
		// (get) Token: 0x06017097 RID: 94359
		// (set) Token: 0x06017096 RID: 94358
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

		// Token: 0x17007ACD RID: 31437
		// (get) Token: 0x06017099 RID: 94361
		// (set) Token: 0x06017098 RID: 94360
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

		// Token: 0x17007ACE RID: 31438
		// (get) Token: 0x0601709B RID: 94363
		// (set) Token: 0x0601709A RID: 94362
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

		// Token: 0x17007ACF RID: 31439
		// (get) Token: 0x0601709D RID: 94365
		// (set) Token: 0x0601709C RID: 94364
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

		// Token: 0x17007AD0 RID: 31440
		// (get) Token: 0x0601709F RID: 94367
		// (set) Token: 0x0601709E RID: 94366
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

		// Token: 0x17007AD1 RID: 31441
		// (get) Token: 0x060170A1 RID: 94369
		// (set) Token: 0x060170A0 RID: 94368
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

		// Token: 0x17007AD2 RID: 31442
		// (get) Token: 0x060170A3 RID: 94371
		// (set) Token: 0x060170A2 RID: 94370
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

		// Token: 0x17007AD3 RID: 31443
		// (get) Token: 0x060170A5 RID: 94373
		// (set) Token: 0x060170A4 RID: 94372
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

		// Token: 0x17007AD4 RID: 31444
		// (get) Token: 0x060170A7 RID: 94375
		// (set) Token: 0x060170A6 RID: 94374
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

		// Token: 0x17007AD5 RID: 31445
		// (get) Token: 0x060170A9 RID: 94377
		// (set) Token: 0x060170A8 RID: 94376
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

		// Token: 0x17007AD6 RID: 31446
		// (get) Token: 0x060170AB RID: 94379
		// (set) Token: 0x060170AA RID: 94378
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

		// Token: 0x17007AD7 RID: 31447
		// (get) Token: 0x060170AC RID: 94380
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007AD8 RID: 31448
		// (get) Token: 0x060170AE RID: 94382
		// (set) Token: 0x060170AD RID: 94381
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

		// Token: 0x17007AD9 RID: 31449
		// (get) Token: 0x060170B0 RID: 94384
		// (set) Token: 0x060170AF RID: 94383
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

		// Token: 0x17007ADA RID: 31450
		// (get) Token: 0x060170B2 RID: 94386
		// (set) Token: 0x060170B1 RID: 94385
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

		// Token: 0x060170B3 RID: 94387
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060170B4 RID: 94388
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17007ADB RID: 31451
		// (get) Token: 0x060170B5 RID: 94389
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007ADC RID: 31452
		// (get) Token: 0x060170B6 RID: 94390
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007ADD RID: 31453
		// (get) Token: 0x060170B8 RID: 94392
		// (set) Token: 0x060170B7 RID: 94391
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

		// Token: 0x17007ADE RID: 31454
		// (get) Token: 0x060170B9 RID: 94393
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007ADF RID: 31455
		// (get) Token: 0x060170BA RID: 94394
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007AE0 RID: 31456
		// (get) Token: 0x060170BB RID: 94395
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007AE1 RID: 31457
		// (get) Token: 0x060170BC RID: 94396
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007AE2 RID: 31458
		// (get) Token: 0x060170BD RID: 94397
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007AE3 RID: 31459
		// (get) Token: 0x060170BF RID: 94399
		// (set) Token: 0x060170BE RID: 94398
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

		// Token: 0x17007AE4 RID: 31460
		// (get) Token: 0x060170C1 RID: 94401
		// (set) Token: 0x060170C0 RID: 94400
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

		// Token: 0x17007AE5 RID: 31461
		// (get) Token: 0x060170C3 RID: 94403
		// (set) Token: 0x060170C2 RID: 94402
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

		// Token: 0x17007AE6 RID: 31462
		// (get) Token: 0x060170C5 RID: 94405
		// (set) Token: 0x060170C4 RID: 94404
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

		// Token: 0x060170C6 RID: 94406
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060170C7 RID: 94407
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17007AE7 RID: 31463
		// (get) Token: 0x060170C8 RID: 94408
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007AE8 RID: 31464
		// (get) Token: 0x060170C9 RID: 94409
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060170CA RID: 94410
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17007AE9 RID: 31465
		// (get) Token: 0x060170CB RID: 94411
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007AEA RID: 31466
		// (get) Token: 0x060170CD RID: 94413
		// (set) Token: 0x060170CC RID: 94412
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

		// Token: 0x060170CE RID: 94414
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17007AEB RID: 31467
		// (get) Token: 0x060170D0 RID: 94416
		// (set) Token: 0x060170CF RID: 94415
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

		// Token: 0x17007AEC RID: 31468
		// (get) Token: 0x060170D2 RID: 94418
		// (set) Token: 0x060170D1 RID: 94417
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

		// Token: 0x17007AED RID: 31469
		// (get) Token: 0x060170D4 RID: 94420
		// (set) Token: 0x060170D3 RID: 94419
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

		// Token: 0x17007AEE RID: 31470
		// (get) Token: 0x060170D6 RID: 94422
		// (set) Token: 0x060170D5 RID: 94421
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

		// Token: 0x17007AEF RID: 31471
		// (get) Token: 0x060170D8 RID: 94424
		// (set) Token: 0x060170D7 RID: 94423
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

		// Token: 0x17007AF0 RID: 31472
		// (get) Token: 0x060170DA RID: 94426
		// (set) Token: 0x060170D9 RID: 94425
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

		// Token: 0x17007AF1 RID: 31473
		// (get) Token: 0x060170DC RID: 94428
		// (set) Token: 0x060170DB RID: 94427
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

		// Token: 0x17007AF2 RID: 31474
		// (get) Token: 0x060170DE RID: 94430
		// (set) Token: 0x060170DD RID: 94429
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

		// Token: 0x17007AF3 RID: 31475
		// (get) Token: 0x060170E0 RID: 94432
		// (set) Token: 0x060170DF RID: 94431
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

		// Token: 0x17007AF4 RID: 31476
		// (get) Token: 0x060170E1 RID: 94433
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007AF5 RID: 31477
		// (get) Token: 0x060170E2 RID: 94434
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007AF6 RID: 31478
		// (get) Token: 0x060170E4 RID: 94436
		// (set) Token: 0x060170E3 RID: 94435
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

		// Token: 0x17007AF7 RID: 31479
		// (get) Token: 0x060170E6 RID: 94438
		// (set) Token: 0x060170E5 RID: 94437
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

		// Token: 0x17007AF8 RID: 31480
		// (get) Token: 0x060170E8 RID: 94440
		// (set) Token: 0x060170E7 RID: 94439
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

		// Token: 0x14002C7A RID: 11386
		// (add) Token: 0x060170E9 RID: 94441
		// (remove) Token: 0x060170EA RID: 94442
		public virtual extern event HTMLOptionButtonElementEvents_onhelpEventHandler HTMLOptionButtonElementEvents_Event_onhelp;

		// Token: 0x14002C7B RID: 11387
		// (add) Token: 0x060170EB RID: 94443
		// (remove) Token: 0x060170EC RID: 94444
		public virtual extern event HTMLOptionButtonElementEvents_onclickEventHandler HTMLOptionButtonElementEvents_Event_onclick;

		// Token: 0x14002C7C RID: 11388
		// (add) Token: 0x060170ED RID: 94445
		// (remove) Token: 0x060170EE RID: 94446
		public virtual extern event HTMLOptionButtonElementEvents_ondblclickEventHandler HTMLOptionButtonElementEvents_Event_ondblclick;

		// Token: 0x14002C7D RID: 11389
		// (add) Token: 0x060170EF RID: 94447
		// (remove) Token: 0x060170F0 RID: 94448
		public virtual extern event HTMLOptionButtonElementEvents_onkeypressEventHandler HTMLOptionButtonElementEvents_Event_onkeypress;

		// Token: 0x14002C7E RID: 11390
		// (add) Token: 0x060170F1 RID: 94449
		// (remove) Token: 0x060170F2 RID: 94450
		public virtual extern event HTMLOptionButtonElementEvents_onkeydownEventHandler HTMLOptionButtonElementEvents_Event_onkeydown;

		// Token: 0x14002C7F RID: 11391
		// (add) Token: 0x060170F3 RID: 94451
		// (remove) Token: 0x060170F4 RID: 94452
		public virtual extern event HTMLOptionButtonElementEvents_onkeyupEventHandler HTMLOptionButtonElementEvents_Event_onkeyup;

		// Token: 0x14002C80 RID: 11392
		// (add) Token: 0x060170F5 RID: 94453
		// (remove) Token: 0x060170F6 RID: 94454
		public virtual extern event HTMLOptionButtonElementEvents_onmouseoutEventHandler HTMLOptionButtonElementEvents_Event_onmouseout;

		// Token: 0x14002C81 RID: 11393
		// (add) Token: 0x060170F7 RID: 94455
		// (remove) Token: 0x060170F8 RID: 94456
		public virtual extern event HTMLOptionButtonElementEvents_onmouseoverEventHandler HTMLOptionButtonElementEvents_Event_onmouseover;

		// Token: 0x14002C82 RID: 11394
		// (add) Token: 0x060170F9 RID: 94457
		// (remove) Token: 0x060170FA RID: 94458
		public virtual extern event HTMLOptionButtonElementEvents_onmousemoveEventHandler HTMLOptionButtonElementEvents_Event_onmousemove;

		// Token: 0x14002C83 RID: 11395
		// (add) Token: 0x060170FB RID: 94459
		// (remove) Token: 0x060170FC RID: 94460
		public virtual extern event HTMLOptionButtonElementEvents_onmousedownEventHandler HTMLOptionButtonElementEvents_Event_onmousedown;

		// Token: 0x14002C84 RID: 11396
		// (add) Token: 0x060170FD RID: 94461
		// (remove) Token: 0x060170FE RID: 94462
		public virtual extern event HTMLOptionButtonElementEvents_onmouseupEventHandler HTMLOptionButtonElementEvents_Event_onmouseup;

		// Token: 0x14002C85 RID: 11397
		// (add) Token: 0x060170FF RID: 94463
		// (remove) Token: 0x06017100 RID: 94464
		public virtual extern event HTMLOptionButtonElementEvents_onselectstartEventHandler HTMLOptionButtonElementEvents_Event_onselectstart;

		// Token: 0x14002C86 RID: 11398
		// (add) Token: 0x06017101 RID: 94465
		// (remove) Token: 0x06017102 RID: 94466
		public virtual extern event HTMLOptionButtonElementEvents_onfilterchangeEventHandler HTMLOptionButtonElementEvents_Event_onfilterchange;

		// Token: 0x14002C87 RID: 11399
		// (add) Token: 0x06017103 RID: 94467
		// (remove) Token: 0x06017104 RID: 94468
		public virtual extern event HTMLOptionButtonElementEvents_ondragstartEventHandler HTMLOptionButtonElementEvents_Event_ondragstart;

		// Token: 0x14002C88 RID: 11400
		// (add) Token: 0x06017105 RID: 94469
		// (remove) Token: 0x06017106 RID: 94470
		public virtual extern event HTMLOptionButtonElementEvents_onbeforeupdateEventHandler HTMLOptionButtonElementEvents_Event_onbeforeupdate;

		// Token: 0x14002C89 RID: 11401
		// (add) Token: 0x06017107 RID: 94471
		// (remove) Token: 0x06017108 RID: 94472
		public virtual extern event HTMLOptionButtonElementEvents_onafterupdateEventHandler HTMLOptionButtonElementEvents_Event_onafterupdate;

		// Token: 0x14002C8A RID: 11402
		// (add) Token: 0x06017109 RID: 94473
		// (remove) Token: 0x0601710A RID: 94474
		public virtual extern event HTMLOptionButtonElementEvents_onerrorupdateEventHandler HTMLOptionButtonElementEvents_Event_onerrorupdate;

		// Token: 0x14002C8B RID: 11403
		// (add) Token: 0x0601710B RID: 94475
		// (remove) Token: 0x0601710C RID: 94476
		public virtual extern event HTMLOptionButtonElementEvents_onrowexitEventHandler HTMLOptionButtonElementEvents_Event_onrowexit;

		// Token: 0x14002C8C RID: 11404
		// (add) Token: 0x0601710D RID: 94477
		// (remove) Token: 0x0601710E RID: 94478
		public virtual extern event HTMLOptionButtonElementEvents_onrowenterEventHandler HTMLOptionButtonElementEvents_Event_onrowenter;

		// Token: 0x14002C8D RID: 11405
		// (add) Token: 0x0601710F RID: 94479
		// (remove) Token: 0x06017110 RID: 94480
		public virtual extern event HTMLOptionButtonElementEvents_ondatasetchangedEventHandler HTMLOptionButtonElementEvents_Event_ondatasetchanged;

		// Token: 0x14002C8E RID: 11406
		// (add) Token: 0x06017111 RID: 94481
		// (remove) Token: 0x06017112 RID: 94482
		public virtual extern event HTMLOptionButtonElementEvents_ondataavailableEventHandler HTMLOptionButtonElementEvents_Event_ondataavailable;

		// Token: 0x14002C8F RID: 11407
		// (add) Token: 0x06017113 RID: 94483
		// (remove) Token: 0x06017114 RID: 94484
		public virtual extern event HTMLOptionButtonElementEvents_ondatasetcompleteEventHandler HTMLOptionButtonElementEvents_Event_ondatasetcomplete;

		// Token: 0x14002C90 RID: 11408
		// (add) Token: 0x06017115 RID: 94485
		// (remove) Token: 0x06017116 RID: 94486
		public virtual extern event HTMLOptionButtonElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14002C91 RID: 11409
		// (add) Token: 0x06017117 RID: 94487
		// (remove) Token: 0x06017118 RID: 94488
		public virtual extern event HTMLOptionButtonElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14002C92 RID: 11410
		// (add) Token: 0x06017119 RID: 94489
		// (remove) Token: 0x0601711A RID: 94490
		public virtual extern event HTMLOptionButtonElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14002C93 RID: 11411
		// (add) Token: 0x0601711B RID: 94491
		// (remove) Token: 0x0601711C RID: 94492
		public virtual extern event HTMLOptionButtonElementEvents_onfocusEventHandler HTMLOptionButtonElementEvents_Event_onfocus;

		// Token: 0x14002C94 RID: 11412
		// (add) Token: 0x0601711D RID: 94493
		// (remove) Token: 0x0601711E RID: 94494
		public virtual extern event HTMLOptionButtonElementEvents_onblurEventHandler HTMLOptionButtonElementEvents_Event_onblur;

		// Token: 0x14002C95 RID: 11413
		// (add) Token: 0x0601711F RID: 94495
		// (remove) Token: 0x06017120 RID: 94496
		public virtual extern event HTMLOptionButtonElementEvents_onresizeEventHandler HTMLOptionButtonElementEvents_Event_onresize;

		// Token: 0x14002C96 RID: 11414
		// (add) Token: 0x06017121 RID: 94497
		// (remove) Token: 0x06017122 RID: 94498
		public virtual extern event HTMLOptionButtonElementEvents_ondragEventHandler ondrag;

		// Token: 0x14002C97 RID: 11415
		// (add) Token: 0x06017123 RID: 94499
		// (remove) Token: 0x06017124 RID: 94500
		public virtual extern event HTMLOptionButtonElementEvents_ondragendEventHandler ondragend;

		// Token: 0x14002C98 RID: 11416
		// (add) Token: 0x06017125 RID: 94501
		// (remove) Token: 0x06017126 RID: 94502
		public virtual extern event HTMLOptionButtonElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14002C99 RID: 11417
		// (add) Token: 0x06017127 RID: 94503
		// (remove) Token: 0x06017128 RID: 94504
		public virtual extern event HTMLOptionButtonElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x14002C9A RID: 11418
		// (add) Token: 0x06017129 RID: 94505
		// (remove) Token: 0x0601712A RID: 94506
		public virtual extern event HTMLOptionButtonElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14002C9B RID: 11419
		// (add) Token: 0x0601712B RID: 94507
		// (remove) Token: 0x0601712C RID: 94508
		public virtual extern event HTMLOptionButtonElementEvents_ondropEventHandler ondrop;

		// Token: 0x14002C9C RID: 11420
		// (add) Token: 0x0601712D RID: 94509
		// (remove) Token: 0x0601712E RID: 94510
		public virtual extern event HTMLOptionButtonElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002C9D RID: 11421
		// (add) Token: 0x0601712F RID: 94511
		// (remove) Token: 0x06017130 RID: 94512
		public virtual extern event HTMLOptionButtonElementEvents_oncutEventHandler oncut;

		// Token: 0x14002C9E RID: 11422
		// (add) Token: 0x06017131 RID: 94513
		// (remove) Token: 0x06017132 RID: 94514
		public virtual extern event HTMLOptionButtonElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002C9F RID: 11423
		// (add) Token: 0x06017133 RID: 94515
		// (remove) Token: 0x06017134 RID: 94516
		public virtual extern event HTMLOptionButtonElementEvents_oncopyEventHandler oncopy;

		// Token: 0x14002CA0 RID: 11424
		// (add) Token: 0x06017135 RID: 94517
		// (remove) Token: 0x06017136 RID: 94518
		public virtual extern event HTMLOptionButtonElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14002CA1 RID: 11425
		// (add) Token: 0x06017137 RID: 94519
		// (remove) Token: 0x06017138 RID: 94520
		public virtual extern event HTMLOptionButtonElementEvents_onpasteEventHandler onpaste;

		// Token: 0x14002CA2 RID: 11426
		// (add) Token: 0x06017139 RID: 94521
		// (remove) Token: 0x0601713A RID: 94522
		public virtual extern event HTMLOptionButtonElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14002CA3 RID: 11427
		// (add) Token: 0x0601713B RID: 94523
		// (remove) Token: 0x0601713C RID: 94524
		public virtual extern event HTMLOptionButtonElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14002CA4 RID: 11428
		// (add) Token: 0x0601713D RID: 94525
		// (remove) Token: 0x0601713E RID: 94526
		public virtual extern event HTMLOptionButtonElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14002CA5 RID: 11429
		// (add) Token: 0x0601713F RID: 94527
		// (remove) Token: 0x06017140 RID: 94528
		public virtual extern event HTMLOptionButtonElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14002CA6 RID: 11430
		// (add) Token: 0x06017141 RID: 94529
		// (remove) Token: 0x06017142 RID: 94530
		public virtual extern event HTMLOptionButtonElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14002CA7 RID: 11431
		// (add) Token: 0x06017143 RID: 94531
		// (remove) Token: 0x06017144 RID: 94532
		public virtual extern event HTMLOptionButtonElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14002CA8 RID: 11432
		// (add) Token: 0x06017145 RID: 94533
		// (remove) Token: 0x06017146 RID: 94534
		public virtual extern event HTMLOptionButtonElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14002CA9 RID: 11433
		// (add) Token: 0x06017147 RID: 94535
		// (remove) Token: 0x06017148 RID: 94536
		public virtual extern event HTMLOptionButtonElementEvents_onpageEventHandler onpage;

		// Token: 0x14002CAA RID: 11434
		// (add) Token: 0x06017149 RID: 94537
		// (remove) Token: 0x0601714A RID: 94538
		public virtual extern event HTMLOptionButtonElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002CAB RID: 11435
		// (add) Token: 0x0601714B RID: 94539
		// (remove) Token: 0x0601714C RID: 94540
		public virtual extern event HTMLOptionButtonElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002CAC RID: 11436
		// (add) Token: 0x0601714D RID: 94541
		// (remove) Token: 0x0601714E RID: 94542
		public virtual extern event HTMLOptionButtonElementEvents_onmoveEventHandler onmove;

		// Token: 0x14002CAD RID: 11437
		// (add) Token: 0x0601714F RID: 94543
		// (remove) Token: 0x06017150 RID: 94544
		public virtual extern event HTMLOptionButtonElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002CAE RID: 11438
		// (add) Token: 0x06017151 RID: 94545
		// (remove) Token: 0x06017152 RID: 94546
		public virtual extern event HTMLOptionButtonElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14002CAF RID: 11439
		// (add) Token: 0x06017153 RID: 94547
		// (remove) Token: 0x06017154 RID: 94548
		public virtual extern event HTMLOptionButtonElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14002CB0 RID: 11440
		// (add) Token: 0x06017155 RID: 94549
		// (remove) Token: 0x06017156 RID: 94550
		public virtual extern event HTMLOptionButtonElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14002CB1 RID: 11441
		// (add) Token: 0x06017157 RID: 94551
		// (remove) Token: 0x06017158 RID: 94552
		public virtual extern event HTMLOptionButtonElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14002CB2 RID: 11442
		// (add) Token: 0x06017159 RID: 94553
		// (remove) Token: 0x0601715A RID: 94554
		public virtual extern event HTMLOptionButtonElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14002CB3 RID: 11443
		// (add) Token: 0x0601715B RID: 94555
		// (remove) Token: 0x0601715C RID: 94556
		public virtual extern event HTMLOptionButtonElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14002CB4 RID: 11444
		// (add) Token: 0x0601715D RID: 94557
		// (remove) Token: 0x0601715E RID: 94558
		public virtual extern event HTMLOptionButtonElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14002CB5 RID: 11445
		// (add) Token: 0x0601715F RID: 94559
		// (remove) Token: 0x06017160 RID: 94560
		public virtual extern event HTMLOptionButtonElementEvents_onactivateEventHandler onactivate;

		// Token: 0x14002CB6 RID: 11446
		// (add) Token: 0x06017161 RID: 94561
		// (remove) Token: 0x06017162 RID: 94562
		public virtual extern event HTMLOptionButtonElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002CB7 RID: 11447
		// (add) Token: 0x06017163 RID: 94563
		// (remove) Token: 0x06017164 RID: 94564
		public virtual extern event HTMLOptionButtonElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14002CB8 RID: 11448
		// (add) Token: 0x06017165 RID: 94565
		// (remove) Token: 0x06017166 RID: 94566
		public virtual extern event HTMLOptionButtonElementEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14002CB9 RID: 11449
		// (add) Token: 0x06017167 RID: 94567
		// (remove) Token: 0x06017168 RID: 94568
		public virtual extern event HTMLOptionButtonElementEvents_onchangeEventHandler HTMLOptionButtonElementEvents_Event_onchange;

		// Token: 0x14002CBA RID: 11450
		// (add) Token: 0x06017169 RID: 94569
		// (remove) Token: 0x0601716A RID: 94570
		public virtual extern event HTMLOptionButtonElementEvents_onselectEventHandler onselect;

		// Token: 0x14002CBB RID: 11451
		// (add) Token: 0x0601716B RID: 94571
		// (remove) Token: 0x0601716C RID: 94572
		public virtual extern event HTMLOptionButtonElementEvents_onloadEventHandler onload;

		// Token: 0x14002CBC RID: 11452
		// (add) Token: 0x0601716D RID: 94573
		// (remove) Token: 0x0601716E RID: 94574
		public virtual extern event HTMLOptionButtonElementEvents_onerrorEventHandler onerror;

		// Token: 0x14002CBD RID: 11453
		// (add) Token: 0x0601716F RID: 94575
		// (remove) Token: 0x06017170 RID: 94576
		public virtual extern event HTMLOptionButtonElementEvents_onabortEventHandler onabort;
	}
}
