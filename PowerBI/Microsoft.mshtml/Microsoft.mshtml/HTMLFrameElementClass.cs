using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BD0 RID: 3024
	[Guid("3050F314-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLControlElementEvents\0mshtml.HTMLControlElementEvents2\0\0")]
	[ComImport]
	public class HTMLFrameElementClass : DispHTMLFrameElement, HTMLFrameElement, HTMLControlElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLFrameBase, IHTMLFrameBase2, IHTMLFrameBase3, IHTMLFrameElement, IHTMLFrameElement2, HTMLControlElementEvents2_Event
	{
		// Token: 0x0601391B RID: 80155
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLFrameElementClass();

		// Token: 0x0601391C RID: 80156
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0601391D RID: 80157
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0601391E RID: 80158
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17006721 RID: 26401
		// (get) Token: 0x06013920 RID: 80160
		// (set) Token: 0x0601391F RID: 80159
		[DispId(-2147417111)]
		public virtual extern string className
		{
			[DispId(-2147417111)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006722 RID: 26402
		// (get) Token: 0x06013922 RID: 80162
		// (set) Token: 0x06013921 RID: 80161
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

		// Token: 0x17006723 RID: 26403
		// (get) Token: 0x06013923 RID: 80163
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006724 RID: 26404
		// (get) Token: 0x06013924 RID: 80164
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006725 RID: 26405
		// (get) Token: 0x06013925 RID: 80165
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006726 RID: 26406
		// (get) Token: 0x06013927 RID: 80167
		// (set) Token: 0x06013926 RID: 80166
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

		// Token: 0x17006727 RID: 26407
		// (get) Token: 0x06013929 RID: 80169
		// (set) Token: 0x06013928 RID: 80168
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

		// Token: 0x17006728 RID: 26408
		// (get) Token: 0x0601392B RID: 80171
		// (set) Token: 0x0601392A RID: 80170
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

		// Token: 0x17006729 RID: 26409
		// (get) Token: 0x0601392D RID: 80173
		// (set) Token: 0x0601392C RID: 80172
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

		// Token: 0x1700672A RID: 26410
		// (get) Token: 0x0601392F RID: 80175
		// (set) Token: 0x0601392E RID: 80174
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

		// Token: 0x1700672B RID: 26411
		// (get) Token: 0x06013931 RID: 80177
		// (set) Token: 0x06013930 RID: 80176
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

		// Token: 0x1700672C RID: 26412
		// (get) Token: 0x06013933 RID: 80179
		// (set) Token: 0x06013932 RID: 80178
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

		// Token: 0x1700672D RID: 26413
		// (get) Token: 0x06013935 RID: 80181
		// (set) Token: 0x06013934 RID: 80180
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

		// Token: 0x1700672E RID: 26414
		// (get) Token: 0x06013937 RID: 80183
		// (set) Token: 0x06013936 RID: 80182
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

		// Token: 0x1700672F RID: 26415
		// (get) Token: 0x06013939 RID: 80185
		// (set) Token: 0x06013938 RID: 80184
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

		// Token: 0x17006730 RID: 26416
		// (get) Token: 0x0601393B RID: 80187
		// (set) Token: 0x0601393A RID: 80186
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

		// Token: 0x17006731 RID: 26417
		// (get) Token: 0x0601393C RID: 80188
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006732 RID: 26418
		// (get) Token: 0x0601393E RID: 80190
		// (set) Token: 0x0601393D RID: 80189
		[DispId(-2147418043)]
		public virtual extern string title
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

		// Token: 0x17006733 RID: 26419
		// (get) Token: 0x06013940 RID: 80192
		// (set) Token: 0x0601393F RID: 80191
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006734 RID: 26420
		// (get) Token: 0x06013942 RID: 80194
		// (set) Token: 0x06013941 RID: 80193
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

		// Token: 0x06013943 RID: 80195
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06013944 RID: 80196
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17006735 RID: 26421
		// (get) Token: 0x06013945 RID: 80197
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006736 RID: 26422
		// (get) Token: 0x06013946 RID: 80198
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17006737 RID: 26423
		// (get) Token: 0x06013948 RID: 80200
		// (set) Token: 0x06013947 RID: 80199
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

		// Token: 0x17006738 RID: 26424
		// (get) Token: 0x06013949 RID: 80201
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006739 RID: 26425
		// (get) Token: 0x0601394A RID: 80202
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700673A RID: 26426
		// (get) Token: 0x0601394B RID: 80203
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700673B RID: 26427
		// (get) Token: 0x0601394C RID: 80204
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700673C RID: 26428
		// (get) Token: 0x0601394D RID: 80205
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700673D RID: 26429
		// (get) Token: 0x0601394F RID: 80207
		// (set) Token: 0x0601394E RID: 80206
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

		// Token: 0x1700673E RID: 26430
		// (get) Token: 0x06013951 RID: 80209
		// (set) Token: 0x06013950 RID: 80208
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

		// Token: 0x1700673F RID: 26431
		// (get) Token: 0x06013953 RID: 80211
		// (set) Token: 0x06013952 RID: 80210
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

		// Token: 0x17006740 RID: 26432
		// (get) Token: 0x06013955 RID: 80213
		// (set) Token: 0x06013954 RID: 80212
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

		// Token: 0x06013956 RID: 80214
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06013957 RID: 80215
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17006741 RID: 26433
		// (get) Token: 0x06013958 RID: 80216
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006742 RID: 26434
		// (get) Token: 0x06013959 RID: 80217
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601395A RID: 80218
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17006743 RID: 26435
		// (get) Token: 0x0601395B RID: 80219
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006744 RID: 26436
		// (get) Token: 0x0601395D RID: 80221
		// (set) Token: 0x0601395C RID: 80220
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

		// Token: 0x0601395E RID: 80222
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17006745 RID: 26437
		// (get) Token: 0x06013960 RID: 80224
		// (set) Token: 0x0601395F RID: 80223
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

		// Token: 0x17006746 RID: 26438
		// (get) Token: 0x06013962 RID: 80226
		// (set) Token: 0x06013961 RID: 80225
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

		// Token: 0x17006747 RID: 26439
		// (get) Token: 0x06013964 RID: 80228
		// (set) Token: 0x06013963 RID: 80227
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

		// Token: 0x17006748 RID: 26440
		// (get) Token: 0x06013966 RID: 80230
		// (set) Token: 0x06013965 RID: 80229
		[DispId(-2147412094)]
		public virtual extern object onrowexit
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

		// Token: 0x17006749 RID: 26441
		// (get) Token: 0x06013968 RID: 80232
		// (set) Token: 0x06013967 RID: 80231
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

		// Token: 0x1700674A RID: 26442
		// (get) Token: 0x0601396A RID: 80234
		// (set) Token: 0x06013969 RID: 80233
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

		// Token: 0x1700674B RID: 26443
		// (get) Token: 0x0601396C RID: 80236
		// (set) Token: 0x0601396B RID: 80235
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

		// Token: 0x1700674C RID: 26444
		// (get) Token: 0x0601396E RID: 80238
		// (set) Token: 0x0601396D RID: 80237
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

		// Token: 0x1700674D RID: 26445
		// (get) Token: 0x06013970 RID: 80240
		// (set) Token: 0x0601396F RID: 80239
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

		// Token: 0x1700674E RID: 26446
		// (get) Token: 0x06013971 RID: 80241
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700674F RID: 26447
		// (get) Token: 0x06013972 RID: 80242
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006750 RID: 26448
		// (get) Token: 0x06013973 RID: 80243
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06013974 RID: 80244
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06013975 RID: 80245
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17006751 RID: 26449
		// (get) Token: 0x06013977 RID: 80247
		// (set) Token: 0x06013976 RID: 80246
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

		// Token: 0x06013978 RID: 80248
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06013979 RID: 80249
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17006752 RID: 26450
		// (get) Token: 0x0601397B RID: 80251
		// (set) Token: 0x0601397A RID: 80250
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

		// Token: 0x17006753 RID: 26451
		// (get) Token: 0x0601397D RID: 80253
		// (set) Token: 0x0601397C RID: 80252
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

		// Token: 0x17006754 RID: 26452
		// (get) Token: 0x0601397F RID: 80255
		// (set) Token: 0x0601397E RID: 80254
		[DispId(-2147412062)]
		public virtual extern object ondragend
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

		// Token: 0x17006755 RID: 26453
		// (get) Token: 0x06013981 RID: 80257
		// (set) Token: 0x06013980 RID: 80256
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

		// Token: 0x17006756 RID: 26454
		// (get) Token: 0x06013983 RID: 80259
		// (set) Token: 0x06013982 RID: 80258
		[DispId(-2147412060)]
		public virtual extern object ondragover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006757 RID: 26455
		// (get) Token: 0x06013985 RID: 80261
		// (set) Token: 0x06013984 RID: 80260
		[DispId(-2147412059)]
		public virtual extern object ondragleave
		{
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006758 RID: 26456
		// (get) Token: 0x06013987 RID: 80263
		// (set) Token: 0x06013986 RID: 80262
		[DispId(-2147412058)]
		public virtual extern object ondrop
		{
			[TypeLibFunc(20)]
			[DispId(-2147412058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006759 RID: 26457
		// (get) Token: 0x06013989 RID: 80265
		// (set) Token: 0x06013988 RID: 80264
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

		// Token: 0x1700675A RID: 26458
		// (get) Token: 0x0601398B RID: 80267
		// (set) Token: 0x0601398A RID: 80266
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700675B RID: 26459
		// (get) Token: 0x0601398D RID: 80269
		// (set) Token: 0x0601398C RID: 80268
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

		// Token: 0x1700675C RID: 26460
		// (get) Token: 0x0601398F RID: 80271
		// (set) Token: 0x0601398E RID: 80270
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

		// Token: 0x1700675D RID: 26461
		// (get) Token: 0x06013991 RID: 80273
		// (set) Token: 0x06013990 RID: 80272
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

		// Token: 0x1700675E RID: 26462
		// (get) Token: 0x06013993 RID: 80275
		// (set) Token: 0x06013992 RID: 80274
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

		// Token: 0x1700675F RID: 26463
		// (get) Token: 0x06013994 RID: 80276
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006760 RID: 26464
		// (get) Token: 0x06013996 RID: 80278
		// (set) Token: 0x06013995 RID: 80277
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

		// Token: 0x06013997 RID: 80279
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06013998 RID: 80280
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06013999 RID: 80281
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0601399A RID: 80282
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0601399B RID: 80283
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17006761 RID: 26465
		// (get) Token: 0x0601399D RID: 80285
		// (set) Token: 0x0601399C RID: 80284
		[DispId(-2147418097)]
		public virtual extern short tabIndex
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

		// Token: 0x0601399E RID: 80286
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17006762 RID: 26466
		// (get) Token: 0x060139A0 RID: 80288
		// (set) Token: 0x0601399F RID: 80287
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

		// Token: 0x17006763 RID: 26467
		// (get) Token: 0x060139A2 RID: 80290
		// (set) Token: 0x060139A1 RID: 80289
		[DispId(-2147412097)]
		public virtual extern object onblur
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

		// Token: 0x17006764 RID: 26468
		// (get) Token: 0x060139A4 RID: 80292
		// (set) Token: 0x060139A3 RID: 80291
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

		// Token: 0x17006765 RID: 26469
		// (get) Token: 0x060139A6 RID: 80294
		// (set) Token: 0x060139A5 RID: 80293
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

		// Token: 0x060139A7 RID: 80295
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x060139A8 RID: 80296
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060139A9 RID: 80297
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17006766 RID: 26470
		// (get) Token: 0x060139AA RID: 80298
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006767 RID: 26471
		// (get) Token: 0x060139AB RID: 80299
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006768 RID: 26472
		// (get) Token: 0x060139AC RID: 80300
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006769 RID: 26473
		// (get) Token: 0x060139AD RID: 80301
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060139AE RID: 80302
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060139AF RID: 80303
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700676A RID: 26474
		// (get) Token: 0x060139B0 RID: 80304
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700676B RID: 26475
		// (get) Token: 0x060139B2 RID: 80306
		// (set) Token: 0x060139B1 RID: 80305
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

		// Token: 0x1700676C RID: 26476
		// (get) Token: 0x060139B4 RID: 80308
		// (set) Token: 0x060139B3 RID: 80307
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

		// Token: 0x1700676D RID: 26477
		// (get) Token: 0x060139B6 RID: 80310
		// (set) Token: 0x060139B5 RID: 80309
		[DispId(-2147412049)]
		public virtual extern object onrowsinserted
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

		// Token: 0x1700676E RID: 26478
		// (get) Token: 0x060139B8 RID: 80312
		// (set) Token: 0x060139B7 RID: 80311
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

		// Token: 0x1700676F RID: 26479
		// (get) Token: 0x060139BA RID: 80314
		// (set) Token: 0x060139B9 RID: 80313
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

		// Token: 0x060139BB RID: 80315
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17006770 RID: 26480
		// (get) Token: 0x060139BC RID: 80316
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006771 RID: 26481
		// (get) Token: 0x060139BD RID: 80317
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006772 RID: 26482
		// (get) Token: 0x060139BF RID: 80319
		// (set) Token: 0x060139BE RID: 80318
		[DispId(-2147417053)]
		public virtual extern int scrollTop
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

		// Token: 0x17006773 RID: 26483
		// (get) Token: 0x060139C1 RID: 80321
		// (set) Token: 0x060139C0 RID: 80320
		[DispId(-2147417052)]
		public virtual extern int scrollLeft
		{
			[DispId(-2147417052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x060139C2 RID: 80322
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17006774 RID: 26484
		// (get) Token: 0x060139C4 RID: 80324
		// (set) Token: 0x060139C3 RID: 80323
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

		// Token: 0x060139C5 RID: 80325
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060139C6 RID: 80326
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060139C7 RID: 80327
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060139C8 RID: 80328
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17006775 RID: 26485
		// (get) Token: 0x060139C9 RID: 80329
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060139CA RID: 80330
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060139CB RID: 80331
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17006776 RID: 26486
		// (get) Token: 0x060139CC RID: 80332
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006777 RID: 26487
		// (get) Token: 0x060139CD RID: 80333
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006778 RID: 26488
		// (get) Token: 0x060139CF RID: 80335
		// (set) Token: 0x060139CE RID: 80334
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

		// Token: 0x17006779 RID: 26489
		// (get) Token: 0x060139D1 RID: 80337
		// (set) Token: 0x060139D0 RID: 80336
		[DispId(-2147412043)]
		public virtual extern object onbeforeeditfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700677A RID: 26490
		// (get) Token: 0x060139D2 RID: 80338
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060139D3 RID: 80339
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060139D4 RID: 80340
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x1700677B RID: 26491
		// (get) Token: 0x060139D5 RID: 80341
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700677C RID: 26492
		// (get) Token: 0x060139D6 RID: 80342
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700677D RID: 26493
		// (get) Token: 0x060139D8 RID: 80344
		// (set) Token: 0x060139D7 RID: 80343
		[DispId(-2147412039)]
		public virtual extern object onlayoutcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700677E RID: 26494
		// (get) Token: 0x060139DA RID: 80346
		// (set) Token: 0x060139D9 RID: 80345
		[DispId(-2147412038)]
		public virtual extern object onpage
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

		// Token: 0x1700677F RID: 26495
		// (get) Token: 0x060139DC RID: 80348
		// (set) Token: 0x060139DB RID: 80347
		[DispId(-2147417012)]
		public virtual extern bool inflateBlock
		{
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17006780 RID: 26496
		// (get) Token: 0x060139DE RID: 80350
		// (set) Token: 0x060139DD RID: 80349
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

		// Token: 0x060139DF RID: 80351
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17006781 RID: 26497
		// (get) Token: 0x060139E1 RID: 80353
		// (set) Token: 0x060139E0 RID: 80352
		[DispId(-2147412950)]
		public virtual extern string contentEditable
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

		// Token: 0x17006782 RID: 26498
		// (get) Token: 0x060139E2 RID: 80354
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006783 RID: 26499
		// (get) Token: 0x060139E4 RID: 80356
		// (set) Token: 0x060139E3 RID: 80355
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

		// Token: 0x17006784 RID: 26500
		// (get) Token: 0x060139E6 RID: 80358
		// (set) Token: 0x060139E5 RID: 80357
		[DispId(-2147418036)]
		public virtual extern bool disabled
		{
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17006785 RID: 26501
		// (get) Token: 0x060139E7 RID: 80359
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006786 RID: 26502
		// (get) Token: 0x060139E9 RID: 80361
		// (set) Token: 0x060139E8 RID: 80360
		[DispId(-2147412034)]
		public virtual extern object onmove
		{
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006787 RID: 26503
		// (get) Token: 0x060139EB RID: 80363
		// (set) Token: 0x060139EA RID: 80362
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

		// Token: 0x060139EC RID: 80364
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17006788 RID: 26504
		// (get) Token: 0x060139EE RID: 80366
		// (set) Token: 0x060139ED RID: 80365
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

		// Token: 0x17006789 RID: 26505
		// (get) Token: 0x060139F0 RID: 80368
		// (set) Token: 0x060139EF RID: 80367
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

		// Token: 0x1700678A RID: 26506
		// (get) Token: 0x060139F2 RID: 80370
		// (set) Token: 0x060139F1 RID: 80369
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

		// Token: 0x1700678B RID: 26507
		// (get) Token: 0x060139F4 RID: 80372
		// (set) Token: 0x060139F3 RID: 80371
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

		// Token: 0x1700678C RID: 26508
		// (get) Token: 0x060139F6 RID: 80374
		// (set) Token: 0x060139F5 RID: 80373
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

		// Token: 0x1700678D RID: 26509
		// (get) Token: 0x060139F8 RID: 80376
		// (set) Token: 0x060139F7 RID: 80375
		[DispId(-2147412026)]
		public virtual extern object onmouseleave
		{
			[DispId(-2147412026)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700678E RID: 26510
		// (get) Token: 0x060139FA RID: 80378
		// (set) Token: 0x060139F9 RID: 80377
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700678F RID: 26511
		// (get) Token: 0x060139FC RID: 80380
		// (set) Token: 0x060139FB RID: 80379
		[DispId(-2147412024)]
		public virtual extern object ondeactivate
		{
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060139FD RID: 80381
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17006790 RID: 26512
		// (get) Token: 0x060139FE RID: 80382
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006791 RID: 26513
		// (get) Token: 0x06013A00 RID: 80384
		// (set) Token: 0x060139FF RID: 80383
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

		// Token: 0x06013A01 RID: 80385
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06013A02 RID: 80386
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06013A03 RID: 80387
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06013A04 RID: 80388
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17006792 RID: 26514
		// (get) Token: 0x06013A06 RID: 80390
		// (set) Token: 0x06013A05 RID: 80389
		[DispId(-2147412022)]
		public virtual extern object onbeforeactivate
		{
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006793 RID: 26515
		// (get) Token: 0x06013A08 RID: 80392
		// (set) Token: 0x06013A07 RID: 80391
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

		// Token: 0x17006794 RID: 26516
		// (get) Token: 0x06013A0A RID: 80394
		// (set) Token: 0x06013A09 RID: 80393
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

		// Token: 0x17006795 RID: 26517
		// (get) Token: 0x06013A0B RID: 80395
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006796 RID: 26518
		// (get) Token: 0x06013A0C RID: 80396
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006797 RID: 26519
		// (get) Token: 0x06013A0D RID: 80397
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006798 RID: 26520
		// (get) Token: 0x06013A0E RID: 80398
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06013A0F RID: 80399
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17006799 RID: 26521
		// (get) Token: 0x06013A10 RID: 80400
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700679A RID: 26522
		// (get) Token: 0x06013A11 RID: 80401
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06013A12 RID: 80402
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06013A13 RID: 80403
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06013A14 RID: 80404
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06013A15 RID: 80405
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06013A16 RID: 80406
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06013A17 RID: 80407
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06013A18 RID: 80408
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06013A19 RID: 80409
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x1700679B RID: 26523
		// (get) Token: 0x06013A1A RID: 80410
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700679C RID: 26524
		// (get) Token: 0x06013A1C RID: 80412
		// (set) Token: 0x06013A1B RID: 80411
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

		// Token: 0x1700679D RID: 26525
		// (get) Token: 0x06013A1D RID: 80413
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700679E RID: 26526
		// (get) Token: 0x06013A1E RID: 80414
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700679F RID: 26527
		// (get) Token: 0x06013A1F RID: 80415
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170067A0 RID: 26528
		// (get) Token: 0x06013A20 RID: 80416
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170067A1 RID: 26529
		// (get) Token: 0x06013A21 RID: 80417
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170067A2 RID: 26530
		// (get) Token: 0x06013A23 RID: 80419
		// (set) Token: 0x06013A22 RID: 80418
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

		// Token: 0x170067A3 RID: 26531
		// (get) Token: 0x06013A25 RID: 80421
		// (set) Token: 0x06013A24 RID: 80420
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

		// Token: 0x170067A4 RID: 26532
		// (get) Token: 0x06013A27 RID: 80423
		// (set) Token: 0x06013A26 RID: 80422
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

		// Token: 0x170067A5 RID: 26533
		// (get) Token: 0x06013A29 RID: 80425
		// (set) Token: 0x06013A28 RID: 80424
		[DispId(-2147415112)]
		public virtual extern string src
		{
			[DispId(-2147415112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170067A6 RID: 26534
		// (get) Token: 0x06013A2B RID: 80427
		// (set) Token: 0x06013A2A RID: 80426
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170067A7 RID: 26535
		// (get) Token: 0x06013A2D RID: 80429
		// (set) Token: 0x06013A2C RID: 80428
		[DispId(-2147415110)]
		public virtual extern object border
		{
			[DispId(-2147415110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147415110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170067A8 RID: 26536
		// (get) Token: 0x06013A2F RID: 80431
		// (set) Token: 0x06013A2E RID: 80430
		[DispId(-2147415109)]
		public virtual extern string frameBorder
		{
			[DispId(-2147415109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170067A9 RID: 26537
		// (get) Token: 0x06013A31 RID: 80433
		// (set) Token: 0x06013A30 RID: 80432
		[DispId(-2147415108)]
		public virtual extern object frameSpacing
		{
			[DispId(-2147415108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147415108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170067AA RID: 26538
		// (get) Token: 0x06013A33 RID: 80435
		// (set) Token: 0x06013A32 RID: 80434
		[DispId(-2147415107)]
		public virtual extern object marginWidth
		{
			[DispId(-2147415107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147415107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170067AB RID: 26539
		// (get) Token: 0x06013A35 RID: 80437
		// (set) Token: 0x06013A34 RID: 80436
		[DispId(-2147415106)]
		public virtual extern object marginHeight
		{
			[DispId(-2147415106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147415106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170067AC RID: 26540
		// (get) Token: 0x06013A37 RID: 80439
		// (set) Token: 0x06013A36 RID: 80438
		[DispId(-2147415105)]
		public virtual extern bool noResize
		{
			[DispId(-2147415105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147415105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170067AD RID: 26541
		// (get) Token: 0x06013A39 RID: 80441
		// (set) Token: 0x06013A38 RID: 80440
		[DispId(-2147415104)]
		public virtual extern string scrolling
		{
			[DispId(-2147415104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170067AE RID: 26542
		// (get) Token: 0x06013A3A RID: 80442
		[DispId(-2147415103)]
		public virtual extern IHTMLWindow2 contentWindow
		{
			[DispId(-2147415103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170067AF RID: 26543
		// (get) Token: 0x06013A3C RID: 80444
		// (set) Token: 0x06013A3B RID: 80443
		[DispId(-2147412080)]
		public virtual extern object onload
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

		// Token: 0x170067B0 RID: 26544
		// (get) Token: 0x06013A3E RID: 80446
		// (set) Token: 0x06013A3D RID: 80445
		[DispId(-2147412906)]
		public virtual extern bool allowTransparency
		{
			[DispId(-2147412906)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412906)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170067B1 RID: 26545
		// (get) Token: 0x06013A40 RID: 80448
		// (set) Token: 0x06013A3F RID: 80447
		[DispId(-2147415102)]
		public virtual extern string longDesc
		{
			[DispId(-2147415102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170067B2 RID: 26546
		// (get) Token: 0x06013A42 RID: 80450
		// (set) Token: 0x06013A41 RID: 80449
		[DispId(-2147414111)]
		public virtual extern object borderColor
		{
			[DispId(-2147414111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147414111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170067B3 RID: 26547
		// (get) Token: 0x06013A44 RID: 80452
		// (set) Token: 0x06013A43 RID: 80451
		[DispId(-2147418106)]
		public virtual extern object height
		{
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170067B4 RID: 26548
		// (get) Token: 0x06013A46 RID: 80454
		// (set) Token: 0x06013A45 RID: 80453
		[DispId(-2147418107)]
		public virtual extern object width
		{
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06013A47 RID: 80455
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06013A48 RID: 80456
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06013A49 RID: 80457
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170067B5 RID: 26549
		// (get) Token: 0x06013A4B RID: 80459
		// (set) Token: 0x06013A4A RID: 80458
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

		// Token: 0x170067B6 RID: 26550
		// (get) Token: 0x06013A4D RID: 80461
		// (set) Token: 0x06013A4C RID: 80460
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

		// Token: 0x170067B7 RID: 26551
		// (get) Token: 0x06013A4E RID: 80462
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170067B8 RID: 26552
		// (get) Token: 0x06013A4F RID: 80463
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170067B9 RID: 26553
		// (get) Token: 0x06013A50 RID: 80464
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170067BA RID: 26554
		// (get) Token: 0x06013A52 RID: 80466
		// (set) Token: 0x06013A51 RID: 80465
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

		// Token: 0x170067BB RID: 26555
		// (get) Token: 0x06013A54 RID: 80468
		// (set) Token: 0x06013A53 RID: 80467
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

		// Token: 0x170067BC RID: 26556
		// (get) Token: 0x06013A56 RID: 80470
		// (set) Token: 0x06013A55 RID: 80469
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

		// Token: 0x170067BD RID: 26557
		// (get) Token: 0x06013A58 RID: 80472
		// (set) Token: 0x06013A57 RID: 80471
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

		// Token: 0x170067BE RID: 26558
		// (get) Token: 0x06013A5A RID: 80474
		// (set) Token: 0x06013A59 RID: 80473
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

		// Token: 0x170067BF RID: 26559
		// (get) Token: 0x06013A5C RID: 80476
		// (set) Token: 0x06013A5B RID: 80475
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

		// Token: 0x170067C0 RID: 26560
		// (get) Token: 0x06013A5E RID: 80478
		// (set) Token: 0x06013A5D RID: 80477
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

		// Token: 0x170067C1 RID: 26561
		// (get) Token: 0x06013A60 RID: 80480
		// (set) Token: 0x06013A5F RID: 80479
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

		// Token: 0x170067C2 RID: 26562
		// (get) Token: 0x06013A62 RID: 80482
		// (set) Token: 0x06013A61 RID: 80481
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

		// Token: 0x170067C3 RID: 26563
		// (get) Token: 0x06013A64 RID: 80484
		// (set) Token: 0x06013A63 RID: 80483
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

		// Token: 0x170067C4 RID: 26564
		// (get) Token: 0x06013A66 RID: 80486
		// (set) Token: 0x06013A65 RID: 80485
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

		// Token: 0x170067C5 RID: 26565
		// (get) Token: 0x06013A67 RID: 80487
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170067C6 RID: 26566
		// (get) Token: 0x06013A69 RID: 80489
		// (set) Token: 0x06013A68 RID: 80488
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

		// Token: 0x170067C7 RID: 26567
		// (get) Token: 0x06013A6B RID: 80491
		// (set) Token: 0x06013A6A RID: 80490
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

		// Token: 0x170067C8 RID: 26568
		// (get) Token: 0x06013A6D RID: 80493
		// (set) Token: 0x06013A6C RID: 80492
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

		// Token: 0x06013A6E RID: 80494
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06013A6F RID: 80495
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170067C9 RID: 26569
		// (get) Token: 0x06013A70 RID: 80496
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170067CA RID: 26570
		// (get) Token: 0x06013A71 RID: 80497
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170067CB RID: 26571
		// (get) Token: 0x06013A73 RID: 80499
		// (set) Token: 0x06013A72 RID: 80498
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

		// Token: 0x170067CC RID: 26572
		// (get) Token: 0x06013A74 RID: 80500
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170067CD RID: 26573
		// (get) Token: 0x06013A75 RID: 80501
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170067CE RID: 26574
		// (get) Token: 0x06013A76 RID: 80502
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170067CF RID: 26575
		// (get) Token: 0x06013A77 RID: 80503
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170067D0 RID: 26576
		// (get) Token: 0x06013A78 RID: 80504
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170067D1 RID: 26577
		// (get) Token: 0x06013A7A RID: 80506
		// (set) Token: 0x06013A79 RID: 80505
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

		// Token: 0x170067D2 RID: 26578
		// (get) Token: 0x06013A7C RID: 80508
		// (set) Token: 0x06013A7B RID: 80507
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

		// Token: 0x170067D3 RID: 26579
		// (get) Token: 0x06013A7E RID: 80510
		// (set) Token: 0x06013A7D RID: 80509
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

		// Token: 0x170067D4 RID: 26580
		// (get) Token: 0x06013A80 RID: 80512
		// (set) Token: 0x06013A7F RID: 80511
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

		// Token: 0x06013A81 RID: 80513
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06013A82 RID: 80514
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170067D5 RID: 26581
		// (get) Token: 0x06013A83 RID: 80515
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170067D6 RID: 26582
		// (get) Token: 0x06013A84 RID: 80516
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06013A85 RID: 80517
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x170067D7 RID: 26583
		// (get) Token: 0x06013A86 RID: 80518
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170067D8 RID: 26584
		// (get) Token: 0x06013A88 RID: 80520
		// (set) Token: 0x06013A87 RID: 80519
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

		// Token: 0x06013A89 RID: 80521
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x170067D9 RID: 26585
		// (get) Token: 0x06013A8B RID: 80523
		// (set) Token: 0x06013A8A RID: 80522
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

		// Token: 0x170067DA RID: 26586
		// (get) Token: 0x06013A8D RID: 80525
		// (set) Token: 0x06013A8C RID: 80524
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

		// Token: 0x170067DB RID: 26587
		// (get) Token: 0x06013A8F RID: 80527
		// (set) Token: 0x06013A8E RID: 80526
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

		// Token: 0x170067DC RID: 26588
		// (get) Token: 0x06013A91 RID: 80529
		// (set) Token: 0x06013A90 RID: 80528
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

		// Token: 0x170067DD RID: 26589
		// (get) Token: 0x06013A93 RID: 80531
		// (set) Token: 0x06013A92 RID: 80530
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

		// Token: 0x170067DE RID: 26590
		// (get) Token: 0x06013A95 RID: 80533
		// (set) Token: 0x06013A94 RID: 80532
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

		// Token: 0x170067DF RID: 26591
		// (get) Token: 0x06013A97 RID: 80535
		// (set) Token: 0x06013A96 RID: 80534
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

		// Token: 0x170067E0 RID: 26592
		// (get) Token: 0x06013A99 RID: 80537
		// (set) Token: 0x06013A98 RID: 80536
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

		// Token: 0x170067E1 RID: 26593
		// (get) Token: 0x06013A9B RID: 80539
		// (set) Token: 0x06013A9A RID: 80538
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

		// Token: 0x170067E2 RID: 26594
		// (get) Token: 0x06013A9C RID: 80540
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170067E3 RID: 26595
		// (get) Token: 0x06013A9D RID: 80541
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170067E4 RID: 26596
		// (get) Token: 0x06013A9E RID: 80542
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06013A9F RID: 80543
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06013AA0 RID: 80544
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x170067E5 RID: 26597
		// (get) Token: 0x06013AA2 RID: 80546
		// (set) Token: 0x06013AA1 RID: 80545
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

		// Token: 0x06013AA3 RID: 80547
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06013AA4 RID: 80548
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170067E6 RID: 26598
		// (get) Token: 0x06013AA6 RID: 80550
		// (set) Token: 0x06013AA5 RID: 80549
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

		// Token: 0x170067E7 RID: 26599
		// (get) Token: 0x06013AA8 RID: 80552
		// (set) Token: 0x06013AA7 RID: 80551
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

		// Token: 0x170067E8 RID: 26600
		// (get) Token: 0x06013AAA RID: 80554
		// (set) Token: 0x06013AA9 RID: 80553
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

		// Token: 0x170067E9 RID: 26601
		// (get) Token: 0x06013AAC RID: 80556
		// (set) Token: 0x06013AAB RID: 80555
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

		// Token: 0x170067EA RID: 26602
		// (get) Token: 0x06013AAE RID: 80558
		// (set) Token: 0x06013AAD RID: 80557
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

		// Token: 0x170067EB RID: 26603
		// (get) Token: 0x06013AB0 RID: 80560
		// (set) Token: 0x06013AAF RID: 80559
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

		// Token: 0x170067EC RID: 26604
		// (get) Token: 0x06013AB2 RID: 80562
		// (set) Token: 0x06013AB1 RID: 80561
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

		// Token: 0x170067ED RID: 26605
		// (get) Token: 0x06013AB4 RID: 80564
		// (set) Token: 0x06013AB3 RID: 80563
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

		// Token: 0x170067EE RID: 26606
		// (get) Token: 0x06013AB6 RID: 80566
		// (set) Token: 0x06013AB5 RID: 80565
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

		// Token: 0x170067EF RID: 26607
		// (get) Token: 0x06013AB8 RID: 80568
		// (set) Token: 0x06013AB7 RID: 80567
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

		// Token: 0x170067F0 RID: 26608
		// (get) Token: 0x06013ABA RID: 80570
		// (set) Token: 0x06013AB9 RID: 80569
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

		// Token: 0x170067F1 RID: 26609
		// (get) Token: 0x06013ABC RID: 80572
		// (set) Token: 0x06013ABB RID: 80571
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

		// Token: 0x170067F2 RID: 26610
		// (get) Token: 0x06013ABE RID: 80574
		// (set) Token: 0x06013ABD RID: 80573
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

		// Token: 0x170067F3 RID: 26611
		// (get) Token: 0x06013ABF RID: 80575
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170067F4 RID: 26612
		// (get) Token: 0x06013AC1 RID: 80577
		// (set) Token: 0x06013AC0 RID: 80576
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

		// Token: 0x06013AC2 RID: 80578
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06013AC3 RID: 80579
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06013AC4 RID: 80580
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06013AC5 RID: 80581
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06013AC6 RID: 80582
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170067F5 RID: 26613
		// (get) Token: 0x06013AC8 RID: 80584
		// (set) Token: 0x06013AC7 RID: 80583
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

		// Token: 0x06013AC9 RID: 80585
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x170067F6 RID: 26614
		// (get) Token: 0x06013ACB RID: 80587
		// (set) Token: 0x06013ACA RID: 80586
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

		// Token: 0x170067F7 RID: 26615
		// (get) Token: 0x06013ACD RID: 80589
		// (set) Token: 0x06013ACC RID: 80588
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

		// Token: 0x170067F8 RID: 26616
		// (get) Token: 0x06013ACF RID: 80591
		// (set) Token: 0x06013ACE RID: 80590
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

		// Token: 0x170067F9 RID: 26617
		// (get) Token: 0x06013AD1 RID: 80593
		// (set) Token: 0x06013AD0 RID: 80592
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

		// Token: 0x06013AD2 RID: 80594
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06013AD3 RID: 80595
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06013AD4 RID: 80596
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170067FA RID: 26618
		// (get) Token: 0x06013AD5 RID: 80597
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170067FB RID: 26619
		// (get) Token: 0x06013AD6 RID: 80598
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170067FC RID: 26620
		// (get) Token: 0x06013AD7 RID: 80599
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170067FD RID: 26621
		// (get) Token: 0x06013AD8 RID: 80600
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06013AD9 RID: 80601
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06013ADA RID: 80602
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170067FE RID: 26622
		// (get) Token: 0x06013ADB RID: 80603
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170067FF RID: 26623
		// (get) Token: 0x06013ADD RID: 80605
		// (set) Token: 0x06013ADC RID: 80604
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

		// Token: 0x17006800 RID: 26624
		// (get) Token: 0x06013ADF RID: 80607
		// (set) Token: 0x06013ADE RID: 80606
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

		// Token: 0x17006801 RID: 26625
		// (get) Token: 0x06013AE1 RID: 80609
		// (set) Token: 0x06013AE0 RID: 80608
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

		// Token: 0x17006802 RID: 26626
		// (get) Token: 0x06013AE3 RID: 80611
		// (set) Token: 0x06013AE2 RID: 80610
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

		// Token: 0x17006803 RID: 26627
		// (get) Token: 0x06013AE5 RID: 80613
		// (set) Token: 0x06013AE4 RID: 80612
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

		// Token: 0x06013AE6 RID: 80614
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17006804 RID: 26628
		// (get) Token: 0x06013AE7 RID: 80615
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006805 RID: 26629
		// (get) Token: 0x06013AE8 RID: 80616
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006806 RID: 26630
		// (get) Token: 0x06013AEA RID: 80618
		// (set) Token: 0x06013AE9 RID: 80617
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

		// Token: 0x17006807 RID: 26631
		// (get) Token: 0x06013AEC RID: 80620
		// (set) Token: 0x06013AEB RID: 80619
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

		// Token: 0x06013AED RID: 80621
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06013AEE RID: 80622
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17006808 RID: 26632
		// (get) Token: 0x06013AF0 RID: 80624
		// (set) Token: 0x06013AEF RID: 80623
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

		// Token: 0x06013AF1 RID: 80625
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06013AF2 RID: 80626
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06013AF3 RID: 80627
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06013AF4 RID: 80628
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17006809 RID: 26633
		// (get) Token: 0x06013AF5 RID: 80629
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06013AF6 RID: 80630
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06013AF7 RID: 80631
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x1700680A RID: 26634
		// (get) Token: 0x06013AF8 RID: 80632
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700680B RID: 26635
		// (get) Token: 0x06013AF9 RID: 80633
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700680C RID: 26636
		// (get) Token: 0x06013AFB RID: 80635
		// (set) Token: 0x06013AFA RID: 80634
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

		// Token: 0x1700680D RID: 26637
		// (get) Token: 0x06013AFD RID: 80637
		// (set) Token: 0x06013AFC RID: 80636
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

		// Token: 0x1700680E RID: 26638
		// (get) Token: 0x06013AFE RID: 80638
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06013AFF RID: 80639
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06013B00 RID: 80640
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x1700680F RID: 26639
		// (get) Token: 0x06013B01 RID: 80641
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006810 RID: 26640
		// (get) Token: 0x06013B02 RID: 80642
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006811 RID: 26641
		// (get) Token: 0x06013B04 RID: 80644
		// (set) Token: 0x06013B03 RID: 80643
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

		// Token: 0x17006812 RID: 26642
		// (get) Token: 0x06013B06 RID: 80646
		// (set) Token: 0x06013B05 RID: 80645
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

		// Token: 0x17006813 RID: 26643
		// (get) Token: 0x06013B08 RID: 80648
		// (set) Token: 0x06013B07 RID: 80647
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

		// Token: 0x17006814 RID: 26644
		// (get) Token: 0x06013B0A RID: 80650
		// (set) Token: 0x06013B09 RID: 80649
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

		// Token: 0x06013B0B RID: 80651
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17006815 RID: 26645
		// (get) Token: 0x06013B0D RID: 80653
		// (set) Token: 0x06013B0C RID: 80652
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

		// Token: 0x17006816 RID: 26646
		// (get) Token: 0x06013B0E RID: 80654
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006817 RID: 26647
		// (get) Token: 0x06013B10 RID: 80656
		// (set) Token: 0x06013B0F RID: 80655
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

		// Token: 0x17006818 RID: 26648
		// (get) Token: 0x06013B12 RID: 80658
		// (set) Token: 0x06013B11 RID: 80657
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

		// Token: 0x17006819 RID: 26649
		// (get) Token: 0x06013B13 RID: 80659
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700681A RID: 26650
		// (get) Token: 0x06013B15 RID: 80661
		// (set) Token: 0x06013B14 RID: 80660
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

		// Token: 0x1700681B RID: 26651
		// (get) Token: 0x06013B17 RID: 80663
		// (set) Token: 0x06013B16 RID: 80662
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

		// Token: 0x06013B18 RID: 80664
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x1700681C RID: 26652
		// (get) Token: 0x06013B1A RID: 80666
		// (set) Token: 0x06013B19 RID: 80665
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

		// Token: 0x1700681D RID: 26653
		// (get) Token: 0x06013B1C RID: 80668
		// (set) Token: 0x06013B1B RID: 80667
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

		// Token: 0x1700681E RID: 26654
		// (get) Token: 0x06013B1E RID: 80670
		// (set) Token: 0x06013B1D RID: 80669
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

		// Token: 0x1700681F RID: 26655
		// (get) Token: 0x06013B20 RID: 80672
		// (set) Token: 0x06013B1F RID: 80671
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

		// Token: 0x17006820 RID: 26656
		// (get) Token: 0x06013B22 RID: 80674
		// (set) Token: 0x06013B21 RID: 80673
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

		// Token: 0x17006821 RID: 26657
		// (get) Token: 0x06013B24 RID: 80676
		// (set) Token: 0x06013B23 RID: 80675
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

		// Token: 0x17006822 RID: 26658
		// (get) Token: 0x06013B26 RID: 80678
		// (set) Token: 0x06013B25 RID: 80677
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

		// Token: 0x17006823 RID: 26659
		// (get) Token: 0x06013B28 RID: 80680
		// (set) Token: 0x06013B27 RID: 80679
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

		// Token: 0x06013B29 RID: 80681
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17006824 RID: 26660
		// (get) Token: 0x06013B2A RID: 80682
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006825 RID: 26661
		// (get) Token: 0x06013B2C RID: 80684
		// (set) Token: 0x06013B2B RID: 80683
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

		// Token: 0x06013B2D RID: 80685
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06013B2E RID: 80686
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06013B2F RID: 80687
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06013B30 RID: 80688
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17006826 RID: 26662
		// (get) Token: 0x06013B32 RID: 80690
		// (set) Token: 0x06013B31 RID: 80689
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

		// Token: 0x17006827 RID: 26663
		// (get) Token: 0x06013B34 RID: 80692
		// (set) Token: 0x06013B33 RID: 80691
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

		// Token: 0x17006828 RID: 26664
		// (get) Token: 0x06013B36 RID: 80694
		// (set) Token: 0x06013B35 RID: 80693
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

		// Token: 0x17006829 RID: 26665
		// (get) Token: 0x06013B37 RID: 80695
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700682A RID: 26666
		// (get) Token: 0x06013B38 RID: 80696
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700682B RID: 26667
		// (get) Token: 0x06013B39 RID: 80697
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700682C RID: 26668
		// (get) Token: 0x06013B3A RID: 80698
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06013B3B RID: 80699
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x1700682D RID: 26669
		// (get) Token: 0x06013B3C RID: 80700
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700682E RID: 26670
		// (get) Token: 0x06013B3D RID: 80701
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06013B3E RID: 80702
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06013B3F RID: 80703
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06013B40 RID: 80704
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06013B41 RID: 80705
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06013B42 RID: 80706
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06013B43 RID: 80707
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06013B44 RID: 80708
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06013B45 RID: 80709
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x1700682F RID: 26671
		// (get) Token: 0x06013B46 RID: 80710
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006830 RID: 26672
		// (get) Token: 0x06013B48 RID: 80712
		// (set) Token: 0x06013B47 RID: 80711
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

		// Token: 0x17006831 RID: 26673
		// (get) Token: 0x06013B49 RID: 80713
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006832 RID: 26674
		// (get) Token: 0x06013B4A RID: 80714
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006833 RID: 26675
		// (get) Token: 0x06013B4B RID: 80715
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006834 RID: 26676
		// (get) Token: 0x06013B4C RID: 80716
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006835 RID: 26677
		// (get) Token: 0x06013B4D RID: 80717
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006836 RID: 26678
		// (get) Token: 0x06013B4F RID: 80719
		// (set) Token: 0x06013B4E RID: 80718
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

		// Token: 0x17006837 RID: 26679
		// (get) Token: 0x06013B51 RID: 80721
		// (set) Token: 0x06013B50 RID: 80720
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

		// Token: 0x17006838 RID: 26680
		// (get) Token: 0x06013B53 RID: 80723
		// (set) Token: 0x06013B52 RID: 80722
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

		// Token: 0x17006839 RID: 26681
		// (get) Token: 0x06013B55 RID: 80725
		// (set) Token: 0x06013B54 RID: 80724
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

		// Token: 0x06013B56 RID: 80726
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x1700683A RID: 26682
		// (get) Token: 0x06013B58 RID: 80728
		// (set) Token: 0x06013B57 RID: 80727
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

		// Token: 0x1700683B RID: 26683
		// (get) Token: 0x06013B5A RID: 80730
		// (set) Token: 0x06013B59 RID: 80729
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

		// Token: 0x1700683C RID: 26684
		// (get) Token: 0x06013B5C RID: 80732
		// (set) Token: 0x06013B5B RID: 80731
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

		// Token: 0x1700683D RID: 26685
		// (get) Token: 0x06013B5E RID: 80734
		// (set) Token: 0x06013B5D RID: 80733
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

		// Token: 0x06013B5F RID: 80735
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06013B60 RID: 80736
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06013B61 RID: 80737
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700683E RID: 26686
		// (get) Token: 0x06013B62 RID: 80738
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700683F RID: 26687
		// (get) Token: 0x06013B63 RID: 80739
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006840 RID: 26688
		// (get) Token: 0x06013B64 RID: 80740
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006841 RID: 26689
		// (get) Token: 0x06013B65 RID: 80741
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006842 RID: 26690
		// (get) Token: 0x06013B67 RID: 80743
		// (set) Token: 0x06013B66 RID: 80742
		public virtual extern string IHTMLFrameBase_src
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006843 RID: 26691
		// (get) Token: 0x06013B69 RID: 80745
		// (set) Token: 0x06013B68 RID: 80744
		public virtual extern string IHTMLFrameBase_name
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006844 RID: 26692
		// (get) Token: 0x06013B6B RID: 80747
		// (set) Token: 0x06013B6A RID: 80746
		public virtual extern object IHTMLFrameBase_border
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006845 RID: 26693
		// (get) Token: 0x06013B6D RID: 80749
		// (set) Token: 0x06013B6C RID: 80748
		public virtual extern string IHTMLFrameBase_frameBorder
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006846 RID: 26694
		// (get) Token: 0x06013B6F RID: 80751
		// (set) Token: 0x06013B6E RID: 80750
		public virtual extern object IHTMLFrameBase_frameSpacing
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006847 RID: 26695
		// (get) Token: 0x06013B71 RID: 80753
		// (set) Token: 0x06013B70 RID: 80752
		public virtual extern object IHTMLFrameBase_marginWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006848 RID: 26696
		// (get) Token: 0x06013B73 RID: 80755
		// (set) Token: 0x06013B72 RID: 80754
		public virtual extern object IHTMLFrameBase_marginHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006849 RID: 26697
		// (get) Token: 0x06013B75 RID: 80757
		// (set) Token: 0x06013B74 RID: 80756
		public virtual extern bool IHTMLFrameBase_noResize
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700684A RID: 26698
		// (get) Token: 0x06013B77 RID: 80759
		// (set) Token: 0x06013B76 RID: 80758
		public virtual extern string IHTMLFrameBase_scrolling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700684B RID: 26699
		// (get) Token: 0x06013B78 RID: 80760
		public virtual extern IHTMLWindow2 IHTMLFrameBase2_contentWindow
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700684C RID: 26700
		// (get) Token: 0x06013B7A RID: 80762
		// (set) Token: 0x06013B79 RID: 80761
		public virtual extern object IHTMLFrameBase2_onload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700684D RID: 26701
		// (get) Token: 0x06013B7C RID: 80764
		// (set) Token: 0x06013B7B RID: 80763
		public virtual extern object IHTMLFrameBase2_onreadystatechange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700684E RID: 26702
		// (get) Token: 0x06013B7D RID: 80765
		public virtual extern string IHTMLFrameBase2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700684F RID: 26703
		// (get) Token: 0x06013B7F RID: 80767
		// (set) Token: 0x06013B7E RID: 80766
		public virtual extern bool IHTMLFrameBase2_allowTransparency
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17006850 RID: 26704
		// (get) Token: 0x06013B81 RID: 80769
		// (set) Token: 0x06013B80 RID: 80768
		public virtual extern string IHTMLFrameBase3_longDesc
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

		// Token: 0x17006851 RID: 26705
		// (get) Token: 0x06013B83 RID: 80771
		// (set) Token: 0x06013B82 RID: 80770
		public virtual extern object IHTMLFrameElement_borderColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006852 RID: 26706
		// (get) Token: 0x06013B85 RID: 80773
		// (set) Token: 0x06013B84 RID: 80772
		public virtual extern object IHTMLFrameElement2_height
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006853 RID: 26707
		// (get) Token: 0x06013B87 RID: 80775
		// (set) Token: 0x06013B86 RID: 80774
		public virtual extern object IHTMLFrameElement2_width
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1400260C RID: 9740
		// (add) Token: 0x06013B88 RID: 80776
		// (remove) Token: 0x06013B89 RID: 80777
		public virtual extern event HTMLControlElementEvents_onhelpEventHandler HTMLControlElementEvents_Event_onhelp;

		// Token: 0x1400260D RID: 9741
		// (add) Token: 0x06013B8A RID: 80778
		// (remove) Token: 0x06013B8B RID: 80779
		public virtual extern event HTMLControlElementEvents_onclickEventHandler HTMLControlElementEvents_Event_onclick;

		// Token: 0x1400260E RID: 9742
		// (add) Token: 0x06013B8C RID: 80780
		// (remove) Token: 0x06013B8D RID: 80781
		public virtual extern event HTMLControlElementEvents_ondblclickEventHandler HTMLControlElementEvents_Event_ondblclick;

		// Token: 0x1400260F RID: 9743
		// (add) Token: 0x06013B8E RID: 80782
		// (remove) Token: 0x06013B8F RID: 80783
		public virtual extern event HTMLControlElementEvents_onkeypressEventHandler HTMLControlElementEvents_Event_onkeypress;

		// Token: 0x14002610 RID: 9744
		// (add) Token: 0x06013B90 RID: 80784
		// (remove) Token: 0x06013B91 RID: 80785
		public virtual extern event HTMLControlElementEvents_onkeydownEventHandler HTMLControlElementEvents_Event_onkeydown;

		// Token: 0x14002611 RID: 9745
		// (add) Token: 0x06013B92 RID: 80786
		// (remove) Token: 0x06013B93 RID: 80787
		public virtual extern event HTMLControlElementEvents_onkeyupEventHandler HTMLControlElementEvents_Event_onkeyup;

		// Token: 0x14002612 RID: 9746
		// (add) Token: 0x06013B94 RID: 80788
		// (remove) Token: 0x06013B95 RID: 80789
		public virtual extern event HTMLControlElementEvents_onmouseoutEventHandler HTMLControlElementEvents_Event_onmouseout;

		// Token: 0x14002613 RID: 9747
		// (add) Token: 0x06013B96 RID: 80790
		// (remove) Token: 0x06013B97 RID: 80791
		public virtual extern event HTMLControlElementEvents_onmouseoverEventHandler HTMLControlElementEvents_Event_onmouseover;

		// Token: 0x14002614 RID: 9748
		// (add) Token: 0x06013B98 RID: 80792
		// (remove) Token: 0x06013B99 RID: 80793
		public virtual extern event HTMLControlElementEvents_onmousemoveEventHandler HTMLControlElementEvents_Event_onmousemove;

		// Token: 0x14002615 RID: 9749
		// (add) Token: 0x06013B9A RID: 80794
		// (remove) Token: 0x06013B9B RID: 80795
		public virtual extern event HTMLControlElementEvents_onmousedownEventHandler HTMLControlElementEvents_Event_onmousedown;

		// Token: 0x14002616 RID: 9750
		// (add) Token: 0x06013B9C RID: 80796
		// (remove) Token: 0x06013B9D RID: 80797
		public virtual extern event HTMLControlElementEvents_onmouseupEventHandler HTMLControlElementEvents_Event_onmouseup;

		// Token: 0x14002617 RID: 9751
		// (add) Token: 0x06013B9E RID: 80798
		// (remove) Token: 0x06013B9F RID: 80799
		public virtual extern event HTMLControlElementEvents_onselectstartEventHandler HTMLControlElementEvents_Event_onselectstart;

		// Token: 0x14002618 RID: 9752
		// (add) Token: 0x06013BA0 RID: 80800
		// (remove) Token: 0x06013BA1 RID: 80801
		public virtual extern event HTMLControlElementEvents_onfilterchangeEventHandler HTMLControlElementEvents_Event_onfilterchange;

		// Token: 0x14002619 RID: 9753
		// (add) Token: 0x06013BA2 RID: 80802
		// (remove) Token: 0x06013BA3 RID: 80803
		public virtual extern event HTMLControlElementEvents_ondragstartEventHandler HTMLControlElementEvents_Event_ondragstart;

		// Token: 0x1400261A RID: 9754
		// (add) Token: 0x06013BA4 RID: 80804
		// (remove) Token: 0x06013BA5 RID: 80805
		public virtual extern event HTMLControlElementEvents_onbeforeupdateEventHandler HTMLControlElementEvents_Event_onbeforeupdate;

		// Token: 0x1400261B RID: 9755
		// (add) Token: 0x06013BA6 RID: 80806
		// (remove) Token: 0x06013BA7 RID: 80807
		public virtual extern event HTMLControlElementEvents_onafterupdateEventHandler HTMLControlElementEvents_Event_onafterupdate;

		// Token: 0x1400261C RID: 9756
		// (add) Token: 0x06013BA8 RID: 80808
		// (remove) Token: 0x06013BA9 RID: 80809
		public virtual extern event HTMLControlElementEvents_onerrorupdateEventHandler HTMLControlElementEvents_Event_onerrorupdate;

		// Token: 0x1400261D RID: 9757
		// (add) Token: 0x06013BAA RID: 80810
		// (remove) Token: 0x06013BAB RID: 80811
		public virtual extern event HTMLControlElementEvents_onrowexitEventHandler HTMLControlElementEvents_Event_onrowexit;

		// Token: 0x1400261E RID: 9758
		// (add) Token: 0x06013BAC RID: 80812
		// (remove) Token: 0x06013BAD RID: 80813
		public virtual extern event HTMLControlElementEvents_onrowenterEventHandler HTMLControlElementEvents_Event_onrowenter;

		// Token: 0x1400261F RID: 9759
		// (add) Token: 0x06013BAE RID: 80814
		// (remove) Token: 0x06013BAF RID: 80815
		public virtual extern event HTMLControlElementEvents_ondatasetchangedEventHandler HTMLControlElementEvents_Event_ondatasetchanged;

		// Token: 0x14002620 RID: 9760
		// (add) Token: 0x06013BB0 RID: 80816
		// (remove) Token: 0x06013BB1 RID: 80817
		public virtual extern event HTMLControlElementEvents_ondataavailableEventHandler HTMLControlElementEvents_Event_ondataavailable;

		// Token: 0x14002621 RID: 9761
		// (add) Token: 0x06013BB2 RID: 80818
		// (remove) Token: 0x06013BB3 RID: 80819
		public virtual extern event HTMLControlElementEvents_ondatasetcompleteEventHandler HTMLControlElementEvents_Event_ondatasetcomplete;

		// Token: 0x14002622 RID: 9762
		// (add) Token: 0x06013BB4 RID: 80820
		// (remove) Token: 0x06013BB5 RID: 80821
		public virtual extern event HTMLControlElementEvents_onlosecaptureEventHandler HTMLControlElementEvents_Event_onlosecapture;

		// Token: 0x14002623 RID: 9763
		// (add) Token: 0x06013BB6 RID: 80822
		// (remove) Token: 0x06013BB7 RID: 80823
		public virtual extern event HTMLControlElementEvents_onpropertychangeEventHandler HTMLControlElementEvents_Event_onpropertychange;

		// Token: 0x14002624 RID: 9764
		// (add) Token: 0x06013BB8 RID: 80824
		// (remove) Token: 0x06013BB9 RID: 80825
		public virtual extern event HTMLControlElementEvents_onscrollEventHandler HTMLControlElementEvents_Event_onscroll;

		// Token: 0x14002625 RID: 9765
		// (add) Token: 0x06013BBA RID: 80826
		// (remove) Token: 0x06013BBB RID: 80827
		public virtual extern event HTMLControlElementEvents_onfocusEventHandler HTMLControlElementEvents_Event_onfocus;

		// Token: 0x14002626 RID: 9766
		// (add) Token: 0x06013BBC RID: 80828
		// (remove) Token: 0x06013BBD RID: 80829
		public virtual extern event HTMLControlElementEvents_onblurEventHandler HTMLControlElementEvents_Event_onblur;

		// Token: 0x14002627 RID: 9767
		// (add) Token: 0x06013BBE RID: 80830
		// (remove) Token: 0x06013BBF RID: 80831
		public virtual extern event HTMLControlElementEvents_onresizeEventHandler HTMLControlElementEvents_Event_onresize;

		// Token: 0x14002628 RID: 9768
		// (add) Token: 0x06013BC0 RID: 80832
		// (remove) Token: 0x06013BC1 RID: 80833
		public virtual extern event HTMLControlElementEvents_ondragEventHandler HTMLControlElementEvents_Event_ondrag;

		// Token: 0x14002629 RID: 9769
		// (add) Token: 0x06013BC2 RID: 80834
		// (remove) Token: 0x06013BC3 RID: 80835
		public virtual extern event HTMLControlElementEvents_ondragendEventHandler HTMLControlElementEvents_Event_ondragend;

		// Token: 0x1400262A RID: 9770
		// (add) Token: 0x06013BC4 RID: 80836
		// (remove) Token: 0x06013BC5 RID: 80837
		public virtual extern event HTMLControlElementEvents_ondragenterEventHandler HTMLControlElementEvents_Event_ondragenter;

		// Token: 0x1400262B RID: 9771
		// (add) Token: 0x06013BC6 RID: 80838
		// (remove) Token: 0x06013BC7 RID: 80839
		public virtual extern event HTMLControlElementEvents_ondragoverEventHandler HTMLControlElementEvents_Event_ondragover;

		// Token: 0x1400262C RID: 9772
		// (add) Token: 0x06013BC8 RID: 80840
		// (remove) Token: 0x06013BC9 RID: 80841
		public virtual extern event HTMLControlElementEvents_ondragleaveEventHandler HTMLControlElementEvents_Event_ondragleave;

		// Token: 0x1400262D RID: 9773
		// (add) Token: 0x06013BCA RID: 80842
		// (remove) Token: 0x06013BCB RID: 80843
		public virtual extern event HTMLControlElementEvents_ondropEventHandler HTMLControlElementEvents_Event_ondrop;

		// Token: 0x1400262E RID: 9774
		// (add) Token: 0x06013BCC RID: 80844
		// (remove) Token: 0x06013BCD RID: 80845
		public virtual extern event HTMLControlElementEvents_onbeforecutEventHandler HTMLControlElementEvents_Event_onbeforecut;

		// Token: 0x1400262F RID: 9775
		// (add) Token: 0x06013BCE RID: 80846
		// (remove) Token: 0x06013BCF RID: 80847
		public virtual extern event HTMLControlElementEvents_oncutEventHandler HTMLControlElementEvents_Event_oncut;

		// Token: 0x14002630 RID: 9776
		// (add) Token: 0x06013BD0 RID: 80848
		// (remove) Token: 0x06013BD1 RID: 80849
		public virtual extern event HTMLControlElementEvents_onbeforecopyEventHandler HTMLControlElementEvents_Event_onbeforecopy;

		// Token: 0x14002631 RID: 9777
		// (add) Token: 0x06013BD2 RID: 80850
		// (remove) Token: 0x06013BD3 RID: 80851
		public virtual extern event HTMLControlElementEvents_oncopyEventHandler HTMLControlElementEvents_Event_oncopy;

		// Token: 0x14002632 RID: 9778
		// (add) Token: 0x06013BD4 RID: 80852
		// (remove) Token: 0x06013BD5 RID: 80853
		public virtual extern event HTMLControlElementEvents_onbeforepasteEventHandler HTMLControlElementEvents_Event_onbeforepaste;

		// Token: 0x14002633 RID: 9779
		// (add) Token: 0x06013BD6 RID: 80854
		// (remove) Token: 0x06013BD7 RID: 80855
		public virtual extern event HTMLControlElementEvents_onpasteEventHandler HTMLControlElementEvents_Event_onpaste;

		// Token: 0x14002634 RID: 9780
		// (add) Token: 0x06013BD8 RID: 80856
		// (remove) Token: 0x06013BD9 RID: 80857
		public virtual extern event HTMLControlElementEvents_oncontextmenuEventHandler HTMLControlElementEvents_Event_oncontextmenu;

		// Token: 0x14002635 RID: 9781
		// (add) Token: 0x06013BDA RID: 80858
		// (remove) Token: 0x06013BDB RID: 80859
		public virtual extern event HTMLControlElementEvents_onrowsdeleteEventHandler HTMLControlElementEvents_Event_onrowsdelete;

		// Token: 0x14002636 RID: 9782
		// (add) Token: 0x06013BDC RID: 80860
		// (remove) Token: 0x06013BDD RID: 80861
		public virtual extern event HTMLControlElementEvents_onrowsinsertedEventHandler HTMLControlElementEvents_Event_onrowsinserted;

		// Token: 0x14002637 RID: 9783
		// (add) Token: 0x06013BDE RID: 80862
		// (remove) Token: 0x06013BDF RID: 80863
		public virtual extern event HTMLControlElementEvents_oncellchangeEventHandler HTMLControlElementEvents_Event_oncellchange;

		// Token: 0x14002638 RID: 9784
		// (add) Token: 0x06013BE0 RID: 80864
		// (remove) Token: 0x06013BE1 RID: 80865
		public virtual extern event HTMLControlElementEvents_onreadystatechangeEventHandler HTMLControlElementEvents_Event_onreadystatechange;

		// Token: 0x14002639 RID: 9785
		// (add) Token: 0x06013BE2 RID: 80866
		// (remove) Token: 0x06013BE3 RID: 80867
		public virtual extern event HTMLControlElementEvents_onbeforeeditfocusEventHandler HTMLControlElementEvents_Event_onbeforeeditfocus;

		// Token: 0x1400263A RID: 9786
		// (add) Token: 0x06013BE4 RID: 80868
		// (remove) Token: 0x06013BE5 RID: 80869
		public virtual extern event HTMLControlElementEvents_onlayoutcompleteEventHandler HTMLControlElementEvents_Event_onlayoutcomplete;

		// Token: 0x1400263B RID: 9787
		// (add) Token: 0x06013BE6 RID: 80870
		// (remove) Token: 0x06013BE7 RID: 80871
		public virtual extern event HTMLControlElementEvents_onpageEventHandler HTMLControlElementEvents_Event_onpage;

		// Token: 0x1400263C RID: 9788
		// (add) Token: 0x06013BE8 RID: 80872
		// (remove) Token: 0x06013BE9 RID: 80873
		public virtual extern event HTMLControlElementEvents_onbeforedeactivateEventHandler HTMLControlElementEvents_Event_onbeforedeactivate;

		// Token: 0x1400263D RID: 9789
		// (add) Token: 0x06013BEA RID: 80874
		// (remove) Token: 0x06013BEB RID: 80875
		public virtual extern event HTMLControlElementEvents_onbeforeactivateEventHandler HTMLControlElementEvents_Event_onbeforeactivate;

		// Token: 0x1400263E RID: 9790
		// (add) Token: 0x06013BEC RID: 80876
		// (remove) Token: 0x06013BED RID: 80877
		public virtual extern event HTMLControlElementEvents_onmoveEventHandler HTMLControlElementEvents_Event_onmove;

		// Token: 0x1400263F RID: 9791
		// (add) Token: 0x06013BEE RID: 80878
		// (remove) Token: 0x06013BEF RID: 80879
		public virtual extern event HTMLControlElementEvents_oncontrolselectEventHandler HTMLControlElementEvents_Event_oncontrolselect;

		// Token: 0x14002640 RID: 9792
		// (add) Token: 0x06013BF0 RID: 80880
		// (remove) Token: 0x06013BF1 RID: 80881
		public virtual extern event HTMLControlElementEvents_onmovestartEventHandler HTMLControlElementEvents_Event_onmovestart;

		// Token: 0x14002641 RID: 9793
		// (add) Token: 0x06013BF2 RID: 80882
		// (remove) Token: 0x06013BF3 RID: 80883
		public virtual extern event HTMLControlElementEvents_onmoveendEventHandler HTMLControlElementEvents_Event_onmoveend;

		// Token: 0x14002642 RID: 9794
		// (add) Token: 0x06013BF4 RID: 80884
		// (remove) Token: 0x06013BF5 RID: 80885
		public virtual extern event HTMLControlElementEvents_onresizestartEventHandler HTMLControlElementEvents_Event_onresizestart;

		// Token: 0x14002643 RID: 9795
		// (add) Token: 0x06013BF6 RID: 80886
		// (remove) Token: 0x06013BF7 RID: 80887
		public virtual extern event HTMLControlElementEvents_onresizeendEventHandler HTMLControlElementEvents_Event_onresizeend;

		// Token: 0x14002644 RID: 9796
		// (add) Token: 0x06013BF8 RID: 80888
		// (remove) Token: 0x06013BF9 RID: 80889
		public virtual extern event HTMLControlElementEvents_onmouseenterEventHandler HTMLControlElementEvents_Event_onmouseenter;

		// Token: 0x14002645 RID: 9797
		// (add) Token: 0x06013BFA RID: 80890
		// (remove) Token: 0x06013BFB RID: 80891
		public virtual extern event HTMLControlElementEvents_onmouseleaveEventHandler HTMLControlElementEvents_Event_onmouseleave;

		// Token: 0x14002646 RID: 9798
		// (add) Token: 0x06013BFC RID: 80892
		// (remove) Token: 0x06013BFD RID: 80893
		public virtual extern event HTMLControlElementEvents_onmousewheelEventHandler HTMLControlElementEvents_Event_onmousewheel;

		// Token: 0x14002647 RID: 9799
		// (add) Token: 0x06013BFE RID: 80894
		// (remove) Token: 0x06013BFF RID: 80895
		public virtual extern event HTMLControlElementEvents_onactivateEventHandler HTMLControlElementEvents_Event_onactivate;

		// Token: 0x14002648 RID: 9800
		// (add) Token: 0x06013C00 RID: 80896
		// (remove) Token: 0x06013C01 RID: 80897
		public virtual extern event HTMLControlElementEvents_ondeactivateEventHandler HTMLControlElementEvents_Event_ondeactivate;

		// Token: 0x14002649 RID: 9801
		// (add) Token: 0x06013C02 RID: 80898
		// (remove) Token: 0x06013C03 RID: 80899
		public virtual extern event HTMLControlElementEvents_onfocusinEventHandler HTMLControlElementEvents_Event_onfocusin;

		// Token: 0x1400264A RID: 9802
		// (add) Token: 0x06013C04 RID: 80900
		// (remove) Token: 0x06013C05 RID: 80901
		public virtual extern event HTMLControlElementEvents_onfocusoutEventHandler HTMLControlElementEvents_Event_onfocusout;

		// Token: 0x1400264B RID: 9803
		// (add) Token: 0x06013C06 RID: 80902
		// (remove) Token: 0x06013C07 RID: 80903
		public virtual extern event HTMLControlElementEvents2_onhelpEventHandler HTMLControlElementEvents2_Event_onhelp;

		// Token: 0x1400264C RID: 9804
		// (add) Token: 0x06013C08 RID: 80904
		// (remove) Token: 0x06013C09 RID: 80905
		public virtual extern event HTMLControlElementEvents2_onclickEventHandler HTMLControlElementEvents2_Event_onclick;

		// Token: 0x1400264D RID: 9805
		// (add) Token: 0x06013C0A RID: 80906
		// (remove) Token: 0x06013C0B RID: 80907
		public virtual extern event HTMLControlElementEvents2_ondblclickEventHandler HTMLControlElementEvents2_Event_ondblclick;

		// Token: 0x1400264E RID: 9806
		// (add) Token: 0x06013C0C RID: 80908
		// (remove) Token: 0x06013C0D RID: 80909
		public virtual extern event HTMLControlElementEvents2_onkeypressEventHandler HTMLControlElementEvents2_Event_onkeypress;

		// Token: 0x1400264F RID: 9807
		// (add) Token: 0x06013C0E RID: 80910
		// (remove) Token: 0x06013C0F RID: 80911
		public virtual extern event HTMLControlElementEvents2_onkeydownEventHandler HTMLControlElementEvents2_Event_onkeydown;

		// Token: 0x14002650 RID: 9808
		// (add) Token: 0x06013C10 RID: 80912
		// (remove) Token: 0x06013C11 RID: 80913
		public virtual extern event HTMLControlElementEvents2_onkeyupEventHandler HTMLControlElementEvents2_Event_onkeyup;

		// Token: 0x14002651 RID: 9809
		// (add) Token: 0x06013C12 RID: 80914
		// (remove) Token: 0x06013C13 RID: 80915
		public virtual extern event HTMLControlElementEvents2_onmouseoutEventHandler HTMLControlElementEvents2_Event_onmouseout;

		// Token: 0x14002652 RID: 9810
		// (add) Token: 0x06013C14 RID: 80916
		// (remove) Token: 0x06013C15 RID: 80917
		public virtual extern event HTMLControlElementEvents2_onmouseoverEventHandler HTMLControlElementEvents2_Event_onmouseover;

		// Token: 0x14002653 RID: 9811
		// (add) Token: 0x06013C16 RID: 80918
		// (remove) Token: 0x06013C17 RID: 80919
		public virtual extern event HTMLControlElementEvents2_onmousemoveEventHandler HTMLControlElementEvents2_Event_onmousemove;

		// Token: 0x14002654 RID: 9812
		// (add) Token: 0x06013C18 RID: 80920
		// (remove) Token: 0x06013C19 RID: 80921
		public virtual extern event HTMLControlElementEvents2_onmousedownEventHandler HTMLControlElementEvents2_Event_onmousedown;

		// Token: 0x14002655 RID: 9813
		// (add) Token: 0x06013C1A RID: 80922
		// (remove) Token: 0x06013C1B RID: 80923
		public virtual extern event HTMLControlElementEvents2_onmouseupEventHandler HTMLControlElementEvents2_Event_onmouseup;

		// Token: 0x14002656 RID: 9814
		// (add) Token: 0x06013C1C RID: 80924
		// (remove) Token: 0x06013C1D RID: 80925
		public virtual extern event HTMLControlElementEvents2_onselectstartEventHandler HTMLControlElementEvents2_Event_onselectstart;

		// Token: 0x14002657 RID: 9815
		// (add) Token: 0x06013C1E RID: 80926
		// (remove) Token: 0x06013C1F RID: 80927
		public virtual extern event HTMLControlElementEvents2_onfilterchangeEventHandler HTMLControlElementEvents2_Event_onfilterchange;

		// Token: 0x14002658 RID: 9816
		// (add) Token: 0x06013C20 RID: 80928
		// (remove) Token: 0x06013C21 RID: 80929
		public virtual extern event HTMLControlElementEvents2_ondragstartEventHandler HTMLControlElementEvents2_Event_ondragstart;

		// Token: 0x14002659 RID: 9817
		// (add) Token: 0x06013C22 RID: 80930
		// (remove) Token: 0x06013C23 RID: 80931
		public virtual extern event HTMLControlElementEvents2_onbeforeupdateEventHandler HTMLControlElementEvents2_Event_onbeforeupdate;

		// Token: 0x1400265A RID: 9818
		// (add) Token: 0x06013C24 RID: 80932
		// (remove) Token: 0x06013C25 RID: 80933
		public virtual extern event HTMLControlElementEvents2_onafterupdateEventHandler HTMLControlElementEvents2_Event_onafterupdate;

		// Token: 0x1400265B RID: 9819
		// (add) Token: 0x06013C26 RID: 80934
		// (remove) Token: 0x06013C27 RID: 80935
		public virtual extern event HTMLControlElementEvents2_onerrorupdateEventHandler HTMLControlElementEvents2_Event_onerrorupdate;

		// Token: 0x1400265C RID: 9820
		// (add) Token: 0x06013C28 RID: 80936
		// (remove) Token: 0x06013C29 RID: 80937
		public virtual extern event HTMLControlElementEvents2_onrowexitEventHandler HTMLControlElementEvents2_Event_onrowexit;

		// Token: 0x1400265D RID: 9821
		// (add) Token: 0x06013C2A RID: 80938
		// (remove) Token: 0x06013C2B RID: 80939
		public virtual extern event HTMLControlElementEvents2_onrowenterEventHandler HTMLControlElementEvents2_Event_onrowenter;

		// Token: 0x1400265E RID: 9822
		// (add) Token: 0x06013C2C RID: 80940
		// (remove) Token: 0x06013C2D RID: 80941
		public virtual extern event HTMLControlElementEvents2_ondatasetchangedEventHandler HTMLControlElementEvents2_Event_ondatasetchanged;

		// Token: 0x1400265F RID: 9823
		// (add) Token: 0x06013C2E RID: 80942
		// (remove) Token: 0x06013C2F RID: 80943
		public virtual extern event HTMLControlElementEvents2_ondataavailableEventHandler HTMLControlElementEvents2_Event_ondataavailable;

		// Token: 0x14002660 RID: 9824
		// (add) Token: 0x06013C30 RID: 80944
		// (remove) Token: 0x06013C31 RID: 80945
		public virtual extern event HTMLControlElementEvents2_ondatasetcompleteEventHandler HTMLControlElementEvents2_Event_ondatasetcomplete;

		// Token: 0x14002661 RID: 9825
		// (add) Token: 0x06013C32 RID: 80946
		// (remove) Token: 0x06013C33 RID: 80947
		public virtual extern event HTMLControlElementEvents2_onlosecaptureEventHandler HTMLControlElementEvents2_Event_onlosecapture;

		// Token: 0x14002662 RID: 9826
		// (add) Token: 0x06013C34 RID: 80948
		// (remove) Token: 0x06013C35 RID: 80949
		public virtual extern event HTMLControlElementEvents2_onpropertychangeEventHandler HTMLControlElementEvents2_Event_onpropertychange;

		// Token: 0x14002663 RID: 9827
		// (add) Token: 0x06013C36 RID: 80950
		// (remove) Token: 0x06013C37 RID: 80951
		public virtual extern event HTMLControlElementEvents2_onscrollEventHandler HTMLControlElementEvents2_Event_onscroll;

		// Token: 0x14002664 RID: 9828
		// (add) Token: 0x06013C38 RID: 80952
		// (remove) Token: 0x06013C39 RID: 80953
		public virtual extern event HTMLControlElementEvents2_onfocusEventHandler HTMLControlElementEvents2_Event_onfocus;

		// Token: 0x14002665 RID: 9829
		// (add) Token: 0x06013C3A RID: 80954
		// (remove) Token: 0x06013C3B RID: 80955
		public virtual extern event HTMLControlElementEvents2_onblurEventHandler HTMLControlElementEvents2_Event_onblur;

		// Token: 0x14002666 RID: 9830
		// (add) Token: 0x06013C3C RID: 80956
		// (remove) Token: 0x06013C3D RID: 80957
		public virtual extern event HTMLControlElementEvents2_onresizeEventHandler HTMLControlElementEvents2_Event_onresize;

		// Token: 0x14002667 RID: 9831
		// (add) Token: 0x06013C3E RID: 80958
		// (remove) Token: 0x06013C3F RID: 80959
		public virtual extern event HTMLControlElementEvents2_ondragEventHandler HTMLControlElementEvents2_Event_ondrag;

		// Token: 0x14002668 RID: 9832
		// (add) Token: 0x06013C40 RID: 80960
		// (remove) Token: 0x06013C41 RID: 80961
		public virtual extern event HTMLControlElementEvents2_ondragendEventHandler HTMLControlElementEvents2_Event_ondragend;

		// Token: 0x14002669 RID: 9833
		// (add) Token: 0x06013C42 RID: 80962
		// (remove) Token: 0x06013C43 RID: 80963
		public virtual extern event HTMLControlElementEvents2_ondragenterEventHandler HTMLControlElementEvents2_Event_ondragenter;

		// Token: 0x1400266A RID: 9834
		// (add) Token: 0x06013C44 RID: 80964
		// (remove) Token: 0x06013C45 RID: 80965
		public virtual extern event HTMLControlElementEvents2_ondragoverEventHandler HTMLControlElementEvents2_Event_ondragover;

		// Token: 0x1400266B RID: 9835
		// (add) Token: 0x06013C46 RID: 80966
		// (remove) Token: 0x06013C47 RID: 80967
		public virtual extern event HTMLControlElementEvents2_ondragleaveEventHandler HTMLControlElementEvents2_Event_ondragleave;

		// Token: 0x1400266C RID: 9836
		// (add) Token: 0x06013C48 RID: 80968
		// (remove) Token: 0x06013C49 RID: 80969
		public virtual extern event HTMLControlElementEvents2_ondropEventHandler HTMLControlElementEvents2_Event_ondrop;

		// Token: 0x1400266D RID: 9837
		// (add) Token: 0x06013C4A RID: 80970
		// (remove) Token: 0x06013C4B RID: 80971
		public virtual extern event HTMLControlElementEvents2_onbeforecutEventHandler HTMLControlElementEvents2_Event_onbeforecut;

		// Token: 0x1400266E RID: 9838
		// (add) Token: 0x06013C4C RID: 80972
		// (remove) Token: 0x06013C4D RID: 80973
		public virtual extern event HTMLControlElementEvents2_oncutEventHandler HTMLControlElementEvents2_Event_oncut;

		// Token: 0x1400266F RID: 9839
		// (add) Token: 0x06013C4E RID: 80974
		// (remove) Token: 0x06013C4F RID: 80975
		public virtual extern event HTMLControlElementEvents2_onbeforecopyEventHandler HTMLControlElementEvents2_Event_onbeforecopy;

		// Token: 0x14002670 RID: 9840
		// (add) Token: 0x06013C50 RID: 80976
		// (remove) Token: 0x06013C51 RID: 80977
		public virtual extern event HTMLControlElementEvents2_oncopyEventHandler HTMLControlElementEvents2_Event_oncopy;

		// Token: 0x14002671 RID: 9841
		// (add) Token: 0x06013C52 RID: 80978
		// (remove) Token: 0x06013C53 RID: 80979
		public virtual extern event HTMLControlElementEvents2_onbeforepasteEventHandler HTMLControlElementEvents2_Event_onbeforepaste;

		// Token: 0x14002672 RID: 9842
		// (add) Token: 0x06013C54 RID: 80980
		// (remove) Token: 0x06013C55 RID: 80981
		public virtual extern event HTMLControlElementEvents2_onpasteEventHandler HTMLControlElementEvents2_Event_onpaste;

		// Token: 0x14002673 RID: 9843
		// (add) Token: 0x06013C56 RID: 80982
		// (remove) Token: 0x06013C57 RID: 80983
		public virtual extern event HTMLControlElementEvents2_oncontextmenuEventHandler HTMLControlElementEvents2_Event_oncontextmenu;

		// Token: 0x14002674 RID: 9844
		// (add) Token: 0x06013C58 RID: 80984
		// (remove) Token: 0x06013C59 RID: 80985
		public virtual extern event HTMLControlElementEvents2_onrowsdeleteEventHandler HTMLControlElementEvents2_Event_onrowsdelete;

		// Token: 0x14002675 RID: 9845
		// (add) Token: 0x06013C5A RID: 80986
		// (remove) Token: 0x06013C5B RID: 80987
		public virtual extern event HTMLControlElementEvents2_onrowsinsertedEventHandler HTMLControlElementEvents2_Event_onrowsinserted;

		// Token: 0x14002676 RID: 9846
		// (add) Token: 0x06013C5C RID: 80988
		// (remove) Token: 0x06013C5D RID: 80989
		public virtual extern event HTMLControlElementEvents2_oncellchangeEventHandler HTMLControlElementEvents2_Event_oncellchange;

		// Token: 0x14002677 RID: 9847
		// (add) Token: 0x06013C5E RID: 80990
		// (remove) Token: 0x06013C5F RID: 80991
		public virtual extern event HTMLControlElementEvents2_onreadystatechangeEventHandler HTMLControlElementEvents2_Event_onreadystatechange;

		// Token: 0x14002678 RID: 9848
		// (add) Token: 0x06013C60 RID: 80992
		// (remove) Token: 0x06013C61 RID: 80993
		public virtual extern event HTMLControlElementEvents2_onlayoutcompleteEventHandler HTMLControlElementEvents2_Event_onlayoutcomplete;

		// Token: 0x14002679 RID: 9849
		// (add) Token: 0x06013C62 RID: 80994
		// (remove) Token: 0x06013C63 RID: 80995
		public virtual extern event HTMLControlElementEvents2_onpageEventHandler HTMLControlElementEvents2_Event_onpage;

		// Token: 0x1400267A RID: 9850
		// (add) Token: 0x06013C64 RID: 80996
		// (remove) Token: 0x06013C65 RID: 80997
		public virtual extern event HTMLControlElementEvents2_onmouseenterEventHandler HTMLControlElementEvents2_Event_onmouseenter;

		// Token: 0x1400267B RID: 9851
		// (add) Token: 0x06013C66 RID: 80998
		// (remove) Token: 0x06013C67 RID: 80999
		public virtual extern event HTMLControlElementEvents2_onmouseleaveEventHandler HTMLControlElementEvents2_Event_onmouseleave;

		// Token: 0x1400267C RID: 9852
		// (add) Token: 0x06013C68 RID: 81000
		// (remove) Token: 0x06013C69 RID: 81001
		public virtual extern event HTMLControlElementEvents2_onactivateEventHandler HTMLControlElementEvents2_Event_onactivate;

		// Token: 0x1400267D RID: 9853
		// (add) Token: 0x06013C6A RID: 81002
		// (remove) Token: 0x06013C6B RID: 81003
		public virtual extern event HTMLControlElementEvents2_ondeactivateEventHandler HTMLControlElementEvents2_Event_ondeactivate;

		// Token: 0x1400267E RID: 9854
		// (add) Token: 0x06013C6C RID: 81004
		// (remove) Token: 0x06013C6D RID: 81005
		public virtual extern event HTMLControlElementEvents2_onbeforedeactivateEventHandler HTMLControlElementEvents2_Event_onbeforedeactivate;

		// Token: 0x1400267F RID: 9855
		// (add) Token: 0x06013C6E RID: 81006
		// (remove) Token: 0x06013C6F RID: 81007
		public virtual extern event HTMLControlElementEvents2_onbeforeactivateEventHandler HTMLControlElementEvents2_Event_onbeforeactivate;

		// Token: 0x14002680 RID: 9856
		// (add) Token: 0x06013C70 RID: 81008
		// (remove) Token: 0x06013C71 RID: 81009
		public virtual extern event HTMLControlElementEvents2_onfocusinEventHandler HTMLControlElementEvents2_Event_onfocusin;

		// Token: 0x14002681 RID: 9857
		// (add) Token: 0x06013C72 RID: 81010
		// (remove) Token: 0x06013C73 RID: 81011
		public virtual extern event HTMLControlElementEvents2_onfocusoutEventHandler HTMLControlElementEvents2_Event_onfocusout;

		// Token: 0x14002682 RID: 9858
		// (add) Token: 0x06013C74 RID: 81012
		// (remove) Token: 0x06013C75 RID: 81013
		public virtual extern event HTMLControlElementEvents2_onmoveEventHandler HTMLControlElementEvents2_Event_onmove;

		// Token: 0x14002683 RID: 9859
		// (add) Token: 0x06013C76 RID: 81014
		// (remove) Token: 0x06013C77 RID: 81015
		public virtual extern event HTMLControlElementEvents2_oncontrolselectEventHandler HTMLControlElementEvents2_Event_oncontrolselect;

		// Token: 0x14002684 RID: 9860
		// (add) Token: 0x06013C78 RID: 81016
		// (remove) Token: 0x06013C79 RID: 81017
		public virtual extern event HTMLControlElementEvents2_onmovestartEventHandler HTMLControlElementEvents2_Event_onmovestart;

		// Token: 0x14002685 RID: 9861
		// (add) Token: 0x06013C7A RID: 81018
		// (remove) Token: 0x06013C7B RID: 81019
		public virtual extern event HTMLControlElementEvents2_onmoveendEventHandler HTMLControlElementEvents2_Event_onmoveend;

		// Token: 0x14002686 RID: 9862
		// (add) Token: 0x06013C7C RID: 81020
		// (remove) Token: 0x06013C7D RID: 81021
		public virtual extern event HTMLControlElementEvents2_onresizestartEventHandler HTMLControlElementEvents2_Event_onresizestart;

		// Token: 0x14002687 RID: 9863
		// (add) Token: 0x06013C7E RID: 81022
		// (remove) Token: 0x06013C7F RID: 81023
		public virtual extern event HTMLControlElementEvents2_onresizeendEventHandler HTMLControlElementEvents2_Event_onresizeend;

		// Token: 0x14002688 RID: 9864
		// (add) Token: 0x06013C80 RID: 81024
		// (remove) Token: 0x06013C81 RID: 81025
		public virtual extern event HTMLControlElementEvents2_onmousewheelEventHandler HTMLControlElementEvents2_Event_onmousewheel;
	}
}
