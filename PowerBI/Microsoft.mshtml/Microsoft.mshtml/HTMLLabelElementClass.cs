using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000420 RID: 1056
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLLabelEvents\0mshtml.HTMLLabelEvents2\0\0")]
	[TypeLibType(2)]
	[Guid("3050F32B-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLLabelElementClass : DispHTMLLabelElement, HTMLLabelElement, HTMLLabelEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLLabelElement, IHTMLLabelElement2, HTMLLabelEvents2_Event
	{
		// Token: 0x06004304 RID: 17156
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLLabelElementClass();

		// Token: 0x06004305 RID: 17157
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06004306 RID: 17158
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06004307 RID: 17159
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06004309 RID: 17161
		// (set) Token: 0x06004308 RID: 17160
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

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x0600430B RID: 17163
		// (set) Token: 0x0600430A RID: 17162
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

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x0600430C RID: 17164
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x0600430D RID: 17165
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x0600430E RID: 17166
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06004310 RID: 17168
		// (set) Token: 0x0600430F RID: 17167
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

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06004312 RID: 17170
		// (set) Token: 0x06004311 RID: 17169
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

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06004314 RID: 17172
		// (set) Token: 0x06004313 RID: 17171
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

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06004316 RID: 17174
		// (set) Token: 0x06004315 RID: 17173
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

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06004318 RID: 17176
		// (set) Token: 0x06004317 RID: 17175
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

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x0600431A RID: 17178
		// (set) Token: 0x06004319 RID: 17177
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

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x0600431C RID: 17180
		// (set) Token: 0x0600431B RID: 17179
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

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x0600431E RID: 17182
		// (set) Token: 0x0600431D RID: 17181
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

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06004320 RID: 17184
		// (set) Token: 0x0600431F RID: 17183
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

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06004322 RID: 17186
		// (set) Token: 0x06004321 RID: 17185
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

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06004324 RID: 17188
		// (set) Token: 0x06004323 RID: 17187
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

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06004325 RID: 17189
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06004327 RID: 17191
		// (set) Token: 0x06004326 RID: 17190
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

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06004329 RID: 17193
		// (set) Token: 0x06004328 RID: 17192
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

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x0600432B RID: 17195
		// (set) Token: 0x0600432A RID: 17194
		[DispId(-2147412075)]
		public virtual extern object onselectstart
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

		// Token: 0x0600432C RID: 17196
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600432D RID: 17197
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x0600432E RID: 17198
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x0600432F RID: 17199
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06004331 RID: 17201
		// (set) Token: 0x06004330 RID: 17200
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

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06004332 RID: 17202
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06004333 RID: 17203
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06004334 RID: 17204
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06004335 RID: 17205
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06004336 RID: 17206
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06004338 RID: 17208
		// (set) Token: 0x06004337 RID: 17207
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

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x0600433A RID: 17210
		// (set) Token: 0x06004339 RID: 17209
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

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x0600433C RID: 17212
		// (set) Token: 0x0600433B RID: 17211
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

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x0600433E RID: 17214
		// (set) Token: 0x0600433D RID: 17213
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

		// Token: 0x0600433F RID: 17215
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06004340 RID: 17216
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06004341 RID: 17217
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06004342 RID: 17218
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06004343 RID: 17219
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06004344 RID: 17220
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06004346 RID: 17222
		// (set) Token: 0x06004345 RID: 17221
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

		// Token: 0x06004347 RID: 17223
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06004349 RID: 17225
		// (set) Token: 0x06004348 RID: 17224
		[DispId(-2147412091)]
		public virtual extern object onbeforeupdate
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

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x0600434B RID: 17227
		// (set) Token: 0x0600434A RID: 17226
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

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x0600434D RID: 17229
		// (set) Token: 0x0600434C RID: 17228
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

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x0600434F RID: 17231
		// (set) Token: 0x0600434E RID: 17230
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

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06004351 RID: 17233
		// (set) Token: 0x06004350 RID: 17232
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

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06004353 RID: 17235
		// (set) Token: 0x06004352 RID: 17234
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

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06004355 RID: 17237
		// (set) Token: 0x06004354 RID: 17236
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

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06004357 RID: 17239
		// (set) Token: 0x06004356 RID: 17238
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

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06004359 RID: 17241
		// (set) Token: 0x06004358 RID: 17240
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

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x0600435A RID: 17242
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x0600435B RID: 17243
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x0600435C RID: 17244
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600435D RID: 17245
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0600435E RID: 17246
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06004360 RID: 17248
		// (set) Token: 0x0600435F RID: 17247
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

		// Token: 0x06004361 RID: 17249
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06004362 RID: 17250
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06004364 RID: 17252
		// (set) Token: 0x06004363 RID: 17251
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

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06004366 RID: 17254
		// (set) Token: 0x06004365 RID: 17253
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

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06004368 RID: 17256
		// (set) Token: 0x06004367 RID: 17255
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

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x0600436A RID: 17258
		// (set) Token: 0x06004369 RID: 17257
		[DispId(-2147412061)]
		public virtual extern object ondragenter
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

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x0600436C RID: 17260
		// (set) Token: 0x0600436B RID: 17259
		[DispId(-2147412060)]
		public virtual extern object ondragover
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

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x0600436E RID: 17262
		// (set) Token: 0x0600436D RID: 17261
		[DispId(-2147412059)]
		public virtual extern object ondragleave
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

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06004370 RID: 17264
		// (set) Token: 0x0600436F RID: 17263
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

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06004372 RID: 17266
		// (set) Token: 0x06004371 RID: 17265
		[DispId(-2147412054)]
		public virtual extern object onbeforecut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06004374 RID: 17268
		// (set) Token: 0x06004373 RID: 17267
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06004376 RID: 17270
		// (set) Token: 0x06004375 RID: 17269
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

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06004378 RID: 17272
		// (set) Token: 0x06004377 RID: 17271
		[DispId(-2147412056)]
		public virtual extern object oncopy
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

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x0600437A RID: 17274
		// (set) Token: 0x06004379 RID: 17273
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

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x0600437C RID: 17276
		// (set) Token: 0x0600437B RID: 17275
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

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x0600437D RID: 17277
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x0600437F RID: 17279
		// (set) Token: 0x0600437E RID: 17278
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

		// Token: 0x06004380 RID: 17280
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06004381 RID: 17281
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06004382 RID: 17282
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06004383 RID: 17283
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06004384 RID: 17284
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06004386 RID: 17286
		// (set) Token: 0x06004385 RID: 17285
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

		// Token: 0x06004387 RID: 17287
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06004389 RID: 17289
		// (set) Token: 0x06004388 RID: 17288
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

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x0600438B RID: 17291
		// (set) Token: 0x0600438A RID: 17290
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

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x0600438D RID: 17293
		// (set) Token: 0x0600438C RID: 17292
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x0600438F RID: 17295
		// (set) Token: 0x0600438E RID: 17294
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

		// Token: 0x06004390 RID: 17296
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06004391 RID: 17297
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06004392 RID: 17298
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06004393 RID: 17299
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06004394 RID: 17300
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06004395 RID: 17301
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06004396 RID: 17302
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06004397 RID: 17303
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06004398 RID: 17304
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06004399 RID: 17305
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x0600439B RID: 17307
		// (set) Token: 0x0600439A RID: 17306
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

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x0600439D RID: 17309
		// (set) Token: 0x0600439C RID: 17308
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

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x0600439F RID: 17311
		// (set) Token: 0x0600439E RID: 17310
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

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x060043A1 RID: 17313
		// (set) Token: 0x060043A0 RID: 17312
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

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x060043A3 RID: 17315
		// (set) Token: 0x060043A2 RID: 17314
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

		// Token: 0x060043A4 RID: 17316
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x060043A5 RID: 17317
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x060043A6 RID: 17318
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x060043A8 RID: 17320
		// (set) Token: 0x060043A7 RID: 17319
		[DispId(-2147417053)]
		public virtual extern int scrollTop
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

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x060043AA RID: 17322
		// (set) Token: 0x060043A9 RID: 17321
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

		// Token: 0x060043AB RID: 17323
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x060043AD RID: 17325
		// (set) Token: 0x060043AC RID: 17324
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

		// Token: 0x060043AE RID: 17326
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060043AF RID: 17327
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060043B0 RID: 17328
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060043B1 RID: 17329
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x060043B2 RID: 17330
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060043B3 RID: 17331
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060043B4 RID: 17332
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x060043B5 RID: 17333
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x060043B6 RID: 17334
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x060043B8 RID: 17336
		// (set) Token: 0x060043B7 RID: 17335
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

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x060043BA RID: 17338
		// (set) Token: 0x060043B9 RID: 17337
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

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x060043BB RID: 17339
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060043BC RID: 17340
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060043BD RID: 17341
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x060043BE RID: 17342
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x060043BF RID: 17343
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x060043C1 RID: 17345
		// (set) Token: 0x060043C0 RID: 17344
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

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x060043C3 RID: 17347
		// (set) Token: 0x060043C2 RID: 17346
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

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x060043C5 RID: 17349
		// (set) Token: 0x060043C4 RID: 17348
		[DispId(-2147417012)]
		public virtual extern bool inflateBlock
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x060043C7 RID: 17351
		// (set) Token: 0x060043C6 RID: 17350
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

		// Token: 0x060043C8 RID: 17352
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x060043CA RID: 17354
		// (set) Token: 0x060043C9 RID: 17353
		[DispId(-2147412950)]
		public virtual extern string contentEditable
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

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x060043CB RID: 17355
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x060043CD RID: 17357
		// (set) Token: 0x060043CC RID: 17356
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

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x060043CF RID: 17359
		// (set) Token: 0x060043CE RID: 17358
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

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x060043D0 RID: 17360
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x060043D2 RID: 17362
		// (set) Token: 0x060043D1 RID: 17361
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

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x060043D4 RID: 17364
		// (set) Token: 0x060043D3 RID: 17363
		[DispId(-2147412033)]
		public virtual extern object oncontrolselect
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

		// Token: 0x060043D5 RID: 17365
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x060043D7 RID: 17367
		// (set) Token: 0x060043D6 RID: 17366
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

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x060043D9 RID: 17369
		// (set) Token: 0x060043D8 RID: 17368
		[DispId(-2147412028)]
		public virtual extern object onresizeend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x060043DB RID: 17371
		// (set) Token: 0x060043DA RID: 17370
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

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x060043DD RID: 17373
		// (set) Token: 0x060043DC RID: 17372
		[DispId(-2147412030)]
		public virtual extern object onmoveend
		{
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x060043DF RID: 17375
		// (set) Token: 0x060043DE RID: 17374
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

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x060043E1 RID: 17377
		// (set) Token: 0x060043E0 RID: 17376
		[DispId(-2147412026)]
		public virtual extern object onmouseleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x060043E3 RID: 17379
		// (set) Token: 0x060043E2 RID: 17378
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x060043E5 RID: 17381
		// (set) Token: 0x060043E4 RID: 17380
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

		// Token: 0x060043E6 RID: 17382
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x060043E7 RID: 17383
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x060043E9 RID: 17385
		// (set) Token: 0x060043E8 RID: 17384
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

		// Token: 0x060043EA RID: 17386
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x060043EB RID: 17387
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x060043EC RID: 17388
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x060043ED RID: 17389
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x060043EF RID: 17391
		// (set) Token: 0x060043EE RID: 17390
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

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x060043F1 RID: 17393
		// (set) Token: 0x060043F0 RID: 17392
		[DispId(-2147412021)]
		public virtual extern object onfocusin
		{
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x060043F3 RID: 17395
		// (set) Token: 0x060043F2 RID: 17394
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

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x060043F4 RID: 17396
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x060043F5 RID: 17397
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x060043F6 RID: 17398
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x060043F7 RID: 17399
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060043F8 RID: 17400
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x060043F9 RID: 17401
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x060043FA RID: 17402
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x060043FB RID: 17403
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x060043FC RID: 17404
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060043FD RID: 17405
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060043FE RID: 17406
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x060043FF RID: 17407
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06004400 RID: 17408
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06004401 RID: 17409
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06004402 RID: 17410
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06004403 RID: 17411
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x06004405 RID: 17413
		// (set) Token: 0x06004404 RID: 17412
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

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06004406 RID: 17414
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06004407 RID: 17415
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06004408 RID: 17416
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06004409 RID: 17417
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x0600440A RID: 17418
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x0600440C RID: 17420
		// (set) Token: 0x0600440B RID: 17419
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

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x0600440E RID: 17422
		// (set) Token: 0x0600440D RID: 17421
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

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06004410 RID: 17424
		// (set) Token: 0x0600440F RID: 17423
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

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06004412 RID: 17426
		// (set) Token: 0x06004411 RID: 17425
		[DispId(1000)]
		public virtual extern string htmlFor
		{
			[TypeLibFunc(20)]
			[DispId(1000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06004413 RID: 17427
		[DispId(1002)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06004414 RID: 17428
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06004415 RID: 17429
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06004416 RID: 17430
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06004418 RID: 17432
		// (set) Token: 0x06004417 RID: 17431
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

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x0600441A RID: 17434
		// (set) Token: 0x06004419 RID: 17433
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

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x0600441B RID: 17435
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x0600441C RID: 17436
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x0600441D RID: 17437
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x0600441F RID: 17439
		// (set) Token: 0x0600441E RID: 17438
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

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06004421 RID: 17441
		// (set) Token: 0x06004420 RID: 17440
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

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06004423 RID: 17443
		// (set) Token: 0x06004422 RID: 17442
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

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x06004425 RID: 17445
		// (set) Token: 0x06004424 RID: 17444
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

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x06004427 RID: 17447
		// (set) Token: 0x06004426 RID: 17446
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

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x06004429 RID: 17449
		// (set) Token: 0x06004428 RID: 17448
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

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x0600442B RID: 17451
		// (set) Token: 0x0600442A RID: 17450
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

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x0600442D RID: 17453
		// (set) Token: 0x0600442C RID: 17452
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

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x0600442F RID: 17455
		// (set) Token: 0x0600442E RID: 17454
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

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06004431 RID: 17457
		// (set) Token: 0x06004430 RID: 17456
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

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06004433 RID: 17459
		// (set) Token: 0x06004432 RID: 17458
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

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06004434 RID: 17460
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06004436 RID: 17462
		// (set) Token: 0x06004435 RID: 17461
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

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06004438 RID: 17464
		// (set) Token: 0x06004437 RID: 17463
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

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x0600443A RID: 17466
		// (set) Token: 0x06004439 RID: 17465
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

		// Token: 0x0600443B RID: 17467
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600443C RID: 17468
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x0600443D RID: 17469
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x0600443E RID: 17470
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x06004440 RID: 17472
		// (set) Token: 0x0600443F RID: 17471
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

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x06004441 RID: 17473
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06004442 RID: 17474
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06004443 RID: 17475
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x06004444 RID: 17476
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06004445 RID: 17477
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06004447 RID: 17479
		// (set) Token: 0x06004446 RID: 17478
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

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06004449 RID: 17481
		// (set) Token: 0x06004448 RID: 17480
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

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x0600444B RID: 17483
		// (set) Token: 0x0600444A RID: 17482
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

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x0600444D RID: 17485
		// (set) Token: 0x0600444C RID: 17484
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

		// Token: 0x0600444E RID: 17486
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600444F RID: 17487
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06004450 RID: 17488
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06004451 RID: 17489
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06004452 RID: 17490
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06004453 RID: 17491
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06004455 RID: 17493
		// (set) Token: 0x06004454 RID: 17492
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

		// Token: 0x06004456 RID: 17494
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06004458 RID: 17496
		// (set) Token: 0x06004457 RID: 17495
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

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x0600445A RID: 17498
		// (set) Token: 0x06004459 RID: 17497
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

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x0600445C RID: 17500
		// (set) Token: 0x0600445B RID: 17499
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

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x0600445E RID: 17502
		// (set) Token: 0x0600445D RID: 17501
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

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06004460 RID: 17504
		// (set) Token: 0x0600445F RID: 17503
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

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06004462 RID: 17506
		// (set) Token: 0x06004461 RID: 17505
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

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06004464 RID: 17508
		// (set) Token: 0x06004463 RID: 17507
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

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06004466 RID: 17510
		// (set) Token: 0x06004465 RID: 17509
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

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06004468 RID: 17512
		// (set) Token: 0x06004467 RID: 17511
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

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06004469 RID: 17513
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x0600446A RID: 17514
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x0600446B RID: 17515
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600446C RID: 17516
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600446D RID: 17517
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x0600446F RID: 17519
		// (set) Token: 0x0600446E RID: 17518
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

		// Token: 0x06004470 RID: 17520
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06004471 RID: 17521
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06004473 RID: 17523
		// (set) Token: 0x06004472 RID: 17522
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

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06004475 RID: 17525
		// (set) Token: 0x06004474 RID: 17524
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

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06004477 RID: 17527
		// (set) Token: 0x06004476 RID: 17526
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

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06004479 RID: 17529
		// (set) Token: 0x06004478 RID: 17528
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

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x0600447B RID: 17531
		// (set) Token: 0x0600447A RID: 17530
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

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x0600447D RID: 17533
		// (set) Token: 0x0600447C RID: 17532
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

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x0600447F RID: 17535
		// (set) Token: 0x0600447E RID: 17534
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

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06004481 RID: 17537
		// (set) Token: 0x06004480 RID: 17536
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

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06004483 RID: 17539
		// (set) Token: 0x06004482 RID: 17538
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

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06004485 RID: 17541
		// (set) Token: 0x06004484 RID: 17540
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

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06004487 RID: 17543
		// (set) Token: 0x06004486 RID: 17542
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

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06004489 RID: 17545
		// (set) Token: 0x06004488 RID: 17544
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

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x0600448B RID: 17547
		// (set) Token: 0x0600448A RID: 17546
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

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x0600448C RID: 17548
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x0600448E RID: 17550
		// (set) Token: 0x0600448D RID: 17549
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

		// Token: 0x0600448F RID: 17551
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06004490 RID: 17552
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06004491 RID: 17553
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06004492 RID: 17554
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06004493 RID: 17555
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06004495 RID: 17557
		// (set) Token: 0x06004494 RID: 17556
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

		// Token: 0x06004496 RID: 17558
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06004498 RID: 17560
		// (set) Token: 0x06004497 RID: 17559
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

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x0600449A RID: 17562
		// (set) Token: 0x06004499 RID: 17561
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

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x0600449C RID: 17564
		// (set) Token: 0x0600449B RID: 17563
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

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x0600449E RID: 17566
		// (set) Token: 0x0600449D RID: 17565
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

		// Token: 0x0600449F RID: 17567
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x060044A0 RID: 17568
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060044A1 RID: 17569
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x060044A2 RID: 17570
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x060044A3 RID: 17571
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x060044A4 RID: 17572
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x060044A5 RID: 17573
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060044A6 RID: 17574
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060044A7 RID: 17575
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x060044A8 RID: 17576
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x060044AA RID: 17578
		// (set) Token: 0x060044A9 RID: 17577
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

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x060044AC RID: 17580
		// (set) Token: 0x060044AB RID: 17579
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

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x060044AE RID: 17582
		// (set) Token: 0x060044AD RID: 17581
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

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x060044B0 RID: 17584
		// (set) Token: 0x060044AF RID: 17583
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

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x060044B2 RID: 17586
		// (set) Token: 0x060044B1 RID: 17585
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

		// Token: 0x060044B3 RID: 17587
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x060044B4 RID: 17588
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x060044B5 RID: 17589
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x060044B7 RID: 17591
		// (set) Token: 0x060044B6 RID: 17590
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

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x060044B9 RID: 17593
		// (set) Token: 0x060044B8 RID: 17592
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

		// Token: 0x060044BA RID: 17594
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x060044BB RID: 17595
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x060044BD RID: 17597
		// (set) Token: 0x060044BC RID: 17596
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

		// Token: 0x060044BE RID: 17598
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060044BF RID: 17599
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060044C0 RID: 17600
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060044C1 RID: 17601
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x060044C2 RID: 17602
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060044C3 RID: 17603
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060044C4 RID: 17604
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x060044C5 RID: 17605
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x060044C6 RID: 17606
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x060044C8 RID: 17608
		// (set) Token: 0x060044C7 RID: 17607
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

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x060044CA RID: 17610
		// (set) Token: 0x060044C9 RID: 17609
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

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x060044CB RID: 17611
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060044CC RID: 17612
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060044CD RID: 17613
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x060044CE RID: 17614
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x060044CF RID: 17615
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x060044D1 RID: 17617
		// (set) Token: 0x060044D0 RID: 17616
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

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x060044D3 RID: 17619
		// (set) Token: 0x060044D2 RID: 17618
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

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x060044D5 RID: 17621
		// (set) Token: 0x060044D4 RID: 17620
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

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x060044D7 RID: 17623
		// (set) Token: 0x060044D6 RID: 17622
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

		// Token: 0x060044D8 RID: 17624
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x060044DA RID: 17626
		// (set) Token: 0x060044D9 RID: 17625
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

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x060044DB RID: 17627
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x060044DD RID: 17629
		// (set) Token: 0x060044DC RID: 17628
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

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x060044DF RID: 17631
		// (set) Token: 0x060044DE RID: 17630
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

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x060044E0 RID: 17632
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x060044E2 RID: 17634
		// (set) Token: 0x060044E1 RID: 17633
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

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x060044E4 RID: 17636
		// (set) Token: 0x060044E3 RID: 17635
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

		// Token: 0x060044E5 RID: 17637
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x060044E7 RID: 17639
		// (set) Token: 0x060044E6 RID: 17638
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

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x060044E9 RID: 17641
		// (set) Token: 0x060044E8 RID: 17640
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

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x060044EB RID: 17643
		// (set) Token: 0x060044EA RID: 17642
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

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x060044ED RID: 17645
		// (set) Token: 0x060044EC RID: 17644
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

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x060044EF RID: 17647
		// (set) Token: 0x060044EE RID: 17646
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

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x060044F1 RID: 17649
		// (set) Token: 0x060044F0 RID: 17648
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

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x060044F3 RID: 17651
		// (set) Token: 0x060044F2 RID: 17650
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

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x060044F5 RID: 17653
		// (set) Token: 0x060044F4 RID: 17652
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

		// Token: 0x060044F6 RID: 17654
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x060044F7 RID: 17655
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x060044F9 RID: 17657
		// (set) Token: 0x060044F8 RID: 17656
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

		// Token: 0x060044FA RID: 17658
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x060044FB RID: 17659
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x060044FC RID: 17660
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x060044FD RID: 17661
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x060044FF RID: 17663
		// (set) Token: 0x060044FE RID: 17662
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

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06004501 RID: 17665
		// (set) Token: 0x06004500 RID: 17664
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

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06004503 RID: 17667
		// (set) Token: 0x06004502 RID: 17666
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

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06004504 RID: 17668
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06004505 RID: 17669
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06004506 RID: 17670
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06004507 RID: 17671
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06004508 RID: 17672
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06004509 RID: 17673
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x0600450A RID: 17674
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600450B RID: 17675
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600450C RID: 17676
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600450D RID: 17677
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600450E RID: 17678
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600450F RID: 17679
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06004510 RID: 17680
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06004511 RID: 17681
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06004512 RID: 17682
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06004513 RID: 17683
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06004515 RID: 17685
		// (set) Token: 0x06004514 RID: 17684
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

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06004516 RID: 17686
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06004517 RID: 17687
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06004518 RID: 17688
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06004519 RID: 17689
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x0600451A RID: 17690
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x0600451C RID: 17692
		// (set) Token: 0x0600451B RID: 17691
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

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x0600451E RID: 17694
		// (set) Token: 0x0600451D RID: 17693
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

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06004520 RID: 17696
		// (set) Token: 0x0600451F RID: 17695
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

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06004522 RID: 17698
		// (set) Token: 0x06004521 RID: 17697
		public virtual extern string IHTMLLabelElement_htmlFor
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

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06004524 RID: 17700
		// (set) Token: 0x06004523 RID: 17699
		public virtual extern string IHTMLLabelElement_accessKey
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

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06004525 RID: 17701
		public virtual extern IHTMLFormElement IHTMLLabelElement2_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x140006FB RID: 1787
		// (add) Token: 0x06004526 RID: 17702
		// (remove) Token: 0x06004527 RID: 17703
		public virtual extern event HTMLLabelEvents_onhelpEventHandler HTMLLabelEvents_Event_onhelp;

		// Token: 0x140006FC RID: 1788
		// (add) Token: 0x06004528 RID: 17704
		// (remove) Token: 0x06004529 RID: 17705
		public virtual extern event HTMLLabelEvents_onclickEventHandler HTMLLabelEvents_Event_onclick;

		// Token: 0x140006FD RID: 1789
		// (add) Token: 0x0600452A RID: 17706
		// (remove) Token: 0x0600452B RID: 17707
		public virtual extern event HTMLLabelEvents_ondblclickEventHandler HTMLLabelEvents_Event_ondblclick;

		// Token: 0x140006FE RID: 1790
		// (add) Token: 0x0600452C RID: 17708
		// (remove) Token: 0x0600452D RID: 17709
		public virtual extern event HTMLLabelEvents_onkeypressEventHandler HTMLLabelEvents_Event_onkeypress;

		// Token: 0x140006FF RID: 1791
		// (add) Token: 0x0600452E RID: 17710
		// (remove) Token: 0x0600452F RID: 17711
		public virtual extern event HTMLLabelEvents_onkeydownEventHandler HTMLLabelEvents_Event_onkeydown;

		// Token: 0x14000700 RID: 1792
		// (add) Token: 0x06004530 RID: 17712
		// (remove) Token: 0x06004531 RID: 17713
		public virtual extern event HTMLLabelEvents_onkeyupEventHandler HTMLLabelEvents_Event_onkeyup;

		// Token: 0x14000701 RID: 1793
		// (add) Token: 0x06004532 RID: 17714
		// (remove) Token: 0x06004533 RID: 17715
		public virtual extern event HTMLLabelEvents_onmouseoutEventHandler HTMLLabelEvents_Event_onmouseout;

		// Token: 0x14000702 RID: 1794
		// (add) Token: 0x06004534 RID: 17716
		// (remove) Token: 0x06004535 RID: 17717
		public virtual extern event HTMLLabelEvents_onmouseoverEventHandler HTMLLabelEvents_Event_onmouseover;

		// Token: 0x14000703 RID: 1795
		// (add) Token: 0x06004536 RID: 17718
		// (remove) Token: 0x06004537 RID: 17719
		public virtual extern event HTMLLabelEvents_onmousemoveEventHandler HTMLLabelEvents_Event_onmousemove;

		// Token: 0x14000704 RID: 1796
		// (add) Token: 0x06004538 RID: 17720
		// (remove) Token: 0x06004539 RID: 17721
		public virtual extern event HTMLLabelEvents_onmousedownEventHandler HTMLLabelEvents_Event_onmousedown;

		// Token: 0x14000705 RID: 1797
		// (add) Token: 0x0600453A RID: 17722
		// (remove) Token: 0x0600453B RID: 17723
		public virtual extern event HTMLLabelEvents_onmouseupEventHandler HTMLLabelEvents_Event_onmouseup;

		// Token: 0x14000706 RID: 1798
		// (add) Token: 0x0600453C RID: 17724
		// (remove) Token: 0x0600453D RID: 17725
		public virtual extern event HTMLLabelEvents_onselectstartEventHandler HTMLLabelEvents_Event_onselectstart;

		// Token: 0x14000707 RID: 1799
		// (add) Token: 0x0600453E RID: 17726
		// (remove) Token: 0x0600453F RID: 17727
		public virtual extern event HTMLLabelEvents_onfilterchangeEventHandler HTMLLabelEvents_Event_onfilterchange;

		// Token: 0x14000708 RID: 1800
		// (add) Token: 0x06004540 RID: 17728
		// (remove) Token: 0x06004541 RID: 17729
		public virtual extern event HTMLLabelEvents_ondragstartEventHandler HTMLLabelEvents_Event_ondragstart;

		// Token: 0x14000709 RID: 1801
		// (add) Token: 0x06004542 RID: 17730
		// (remove) Token: 0x06004543 RID: 17731
		public virtual extern event HTMLLabelEvents_onbeforeupdateEventHandler HTMLLabelEvents_Event_onbeforeupdate;

		// Token: 0x1400070A RID: 1802
		// (add) Token: 0x06004544 RID: 17732
		// (remove) Token: 0x06004545 RID: 17733
		public virtual extern event HTMLLabelEvents_onafterupdateEventHandler HTMLLabelEvents_Event_onafterupdate;

		// Token: 0x1400070B RID: 1803
		// (add) Token: 0x06004546 RID: 17734
		// (remove) Token: 0x06004547 RID: 17735
		public virtual extern event HTMLLabelEvents_onerrorupdateEventHandler HTMLLabelEvents_Event_onerrorupdate;

		// Token: 0x1400070C RID: 1804
		// (add) Token: 0x06004548 RID: 17736
		// (remove) Token: 0x06004549 RID: 17737
		public virtual extern event HTMLLabelEvents_onrowexitEventHandler HTMLLabelEvents_Event_onrowexit;

		// Token: 0x1400070D RID: 1805
		// (add) Token: 0x0600454A RID: 17738
		// (remove) Token: 0x0600454B RID: 17739
		public virtual extern event HTMLLabelEvents_onrowenterEventHandler HTMLLabelEvents_Event_onrowenter;

		// Token: 0x1400070E RID: 1806
		// (add) Token: 0x0600454C RID: 17740
		// (remove) Token: 0x0600454D RID: 17741
		public virtual extern event HTMLLabelEvents_ondatasetchangedEventHandler HTMLLabelEvents_Event_ondatasetchanged;

		// Token: 0x1400070F RID: 1807
		// (add) Token: 0x0600454E RID: 17742
		// (remove) Token: 0x0600454F RID: 17743
		public virtual extern event HTMLLabelEvents_ondataavailableEventHandler HTMLLabelEvents_Event_ondataavailable;

		// Token: 0x14000710 RID: 1808
		// (add) Token: 0x06004550 RID: 17744
		// (remove) Token: 0x06004551 RID: 17745
		public virtual extern event HTMLLabelEvents_ondatasetcompleteEventHandler HTMLLabelEvents_Event_ondatasetcomplete;

		// Token: 0x14000711 RID: 1809
		// (add) Token: 0x06004552 RID: 17746
		// (remove) Token: 0x06004553 RID: 17747
		public virtual extern event HTMLLabelEvents_onlosecaptureEventHandler HTMLLabelEvents_Event_onlosecapture;

		// Token: 0x14000712 RID: 1810
		// (add) Token: 0x06004554 RID: 17748
		// (remove) Token: 0x06004555 RID: 17749
		public virtual extern event HTMLLabelEvents_onpropertychangeEventHandler HTMLLabelEvents_Event_onpropertychange;

		// Token: 0x14000713 RID: 1811
		// (add) Token: 0x06004556 RID: 17750
		// (remove) Token: 0x06004557 RID: 17751
		public virtual extern event HTMLLabelEvents_onscrollEventHandler HTMLLabelEvents_Event_onscroll;

		// Token: 0x14000714 RID: 1812
		// (add) Token: 0x06004558 RID: 17752
		// (remove) Token: 0x06004559 RID: 17753
		public virtual extern event HTMLLabelEvents_onfocusEventHandler HTMLLabelEvents_Event_onfocus;

		// Token: 0x14000715 RID: 1813
		// (add) Token: 0x0600455A RID: 17754
		// (remove) Token: 0x0600455B RID: 17755
		public virtual extern event HTMLLabelEvents_onblurEventHandler HTMLLabelEvents_Event_onblur;

		// Token: 0x14000716 RID: 1814
		// (add) Token: 0x0600455C RID: 17756
		// (remove) Token: 0x0600455D RID: 17757
		public virtual extern event HTMLLabelEvents_onresizeEventHandler HTMLLabelEvents_Event_onresize;

		// Token: 0x14000717 RID: 1815
		// (add) Token: 0x0600455E RID: 17758
		// (remove) Token: 0x0600455F RID: 17759
		public virtual extern event HTMLLabelEvents_ondragEventHandler HTMLLabelEvents_Event_ondrag;

		// Token: 0x14000718 RID: 1816
		// (add) Token: 0x06004560 RID: 17760
		// (remove) Token: 0x06004561 RID: 17761
		public virtual extern event HTMLLabelEvents_ondragendEventHandler HTMLLabelEvents_Event_ondragend;

		// Token: 0x14000719 RID: 1817
		// (add) Token: 0x06004562 RID: 17762
		// (remove) Token: 0x06004563 RID: 17763
		public virtual extern event HTMLLabelEvents_ondragenterEventHandler HTMLLabelEvents_Event_ondragenter;

		// Token: 0x1400071A RID: 1818
		// (add) Token: 0x06004564 RID: 17764
		// (remove) Token: 0x06004565 RID: 17765
		public virtual extern event HTMLLabelEvents_ondragoverEventHandler HTMLLabelEvents_Event_ondragover;

		// Token: 0x1400071B RID: 1819
		// (add) Token: 0x06004566 RID: 17766
		// (remove) Token: 0x06004567 RID: 17767
		public virtual extern event HTMLLabelEvents_ondragleaveEventHandler HTMLLabelEvents_Event_ondragleave;

		// Token: 0x1400071C RID: 1820
		// (add) Token: 0x06004568 RID: 17768
		// (remove) Token: 0x06004569 RID: 17769
		public virtual extern event HTMLLabelEvents_ondropEventHandler HTMLLabelEvents_Event_ondrop;

		// Token: 0x1400071D RID: 1821
		// (add) Token: 0x0600456A RID: 17770
		// (remove) Token: 0x0600456B RID: 17771
		public virtual extern event HTMLLabelEvents_onbeforecutEventHandler HTMLLabelEvents_Event_onbeforecut;

		// Token: 0x1400071E RID: 1822
		// (add) Token: 0x0600456C RID: 17772
		// (remove) Token: 0x0600456D RID: 17773
		public virtual extern event HTMLLabelEvents_oncutEventHandler HTMLLabelEvents_Event_oncut;

		// Token: 0x1400071F RID: 1823
		// (add) Token: 0x0600456E RID: 17774
		// (remove) Token: 0x0600456F RID: 17775
		public virtual extern event HTMLLabelEvents_onbeforecopyEventHandler HTMLLabelEvents_Event_onbeforecopy;

		// Token: 0x14000720 RID: 1824
		// (add) Token: 0x06004570 RID: 17776
		// (remove) Token: 0x06004571 RID: 17777
		public virtual extern event HTMLLabelEvents_oncopyEventHandler HTMLLabelEvents_Event_oncopy;

		// Token: 0x14000721 RID: 1825
		// (add) Token: 0x06004572 RID: 17778
		// (remove) Token: 0x06004573 RID: 17779
		public virtual extern event HTMLLabelEvents_onbeforepasteEventHandler HTMLLabelEvents_Event_onbeforepaste;

		// Token: 0x14000722 RID: 1826
		// (add) Token: 0x06004574 RID: 17780
		// (remove) Token: 0x06004575 RID: 17781
		public virtual extern event HTMLLabelEvents_onpasteEventHandler HTMLLabelEvents_Event_onpaste;

		// Token: 0x14000723 RID: 1827
		// (add) Token: 0x06004576 RID: 17782
		// (remove) Token: 0x06004577 RID: 17783
		public virtual extern event HTMLLabelEvents_oncontextmenuEventHandler HTMLLabelEvents_Event_oncontextmenu;

		// Token: 0x14000724 RID: 1828
		// (add) Token: 0x06004578 RID: 17784
		// (remove) Token: 0x06004579 RID: 17785
		public virtual extern event HTMLLabelEvents_onrowsdeleteEventHandler HTMLLabelEvents_Event_onrowsdelete;

		// Token: 0x14000725 RID: 1829
		// (add) Token: 0x0600457A RID: 17786
		// (remove) Token: 0x0600457B RID: 17787
		public virtual extern event HTMLLabelEvents_onrowsinsertedEventHandler HTMLLabelEvents_Event_onrowsinserted;

		// Token: 0x14000726 RID: 1830
		// (add) Token: 0x0600457C RID: 17788
		// (remove) Token: 0x0600457D RID: 17789
		public virtual extern event HTMLLabelEvents_oncellchangeEventHandler HTMLLabelEvents_Event_oncellchange;

		// Token: 0x14000727 RID: 1831
		// (add) Token: 0x0600457E RID: 17790
		// (remove) Token: 0x0600457F RID: 17791
		public virtual extern event HTMLLabelEvents_onreadystatechangeEventHandler HTMLLabelEvents_Event_onreadystatechange;

		// Token: 0x14000728 RID: 1832
		// (add) Token: 0x06004580 RID: 17792
		// (remove) Token: 0x06004581 RID: 17793
		public virtual extern event HTMLLabelEvents_onbeforeeditfocusEventHandler HTMLLabelEvents_Event_onbeforeeditfocus;

		// Token: 0x14000729 RID: 1833
		// (add) Token: 0x06004582 RID: 17794
		// (remove) Token: 0x06004583 RID: 17795
		public virtual extern event HTMLLabelEvents_onlayoutcompleteEventHandler HTMLLabelEvents_Event_onlayoutcomplete;

		// Token: 0x1400072A RID: 1834
		// (add) Token: 0x06004584 RID: 17796
		// (remove) Token: 0x06004585 RID: 17797
		public virtual extern event HTMLLabelEvents_onpageEventHandler HTMLLabelEvents_Event_onpage;

		// Token: 0x1400072B RID: 1835
		// (add) Token: 0x06004586 RID: 17798
		// (remove) Token: 0x06004587 RID: 17799
		public virtual extern event HTMLLabelEvents_onbeforedeactivateEventHandler HTMLLabelEvents_Event_onbeforedeactivate;

		// Token: 0x1400072C RID: 1836
		// (add) Token: 0x06004588 RID: 17800
		// (remove) Token: 0x06004589 RID: 17801
		public virtual extern event HTMLLabelEvents_onbeforeactivateEventHandler HTMLLabelEvents_Event_onbeforeactivate;

		// Token: 0x1400072D RID: 1837
		// (add) Token: 0x0600458A RID: 17802
		// (remove) Token: 0x0600458B RID: 17803
		public virtual extern event HTMLLabelEvents_onmoveEventHandler HTMLLabelEvents_Event_onmove;

		// Token: 0x1400072E RID: 1838
		// (add) Token: 0x0600458C RID: 17804
		// (remove) Token: 0x0600458D RID: 17805
		public virtual extern event HTMLLabelEvents_oncontrolselectEventHandler HTMLLabelEvents_Event_oncontrolselect;

		// Token: 0x1400072F RID: 1839
		// (add) Token: 0x0600458E RID: 17806
		// (remove) Token: 0x0600458F RID: 17807
		public virtual extern event HTMLLabelEvents_onmovestartEventHandler HTMLLabelEvents_Event_onmovestart;

		// Token: 0x14000730 RID: 1840
		// (add) Token: 0x06004590 RID: 17808
		// (remove) Token: 0x06004591 RID: 17809
		public virtual extern event HTMLLabelEvents_onmoveendEventHandler HTMLLabelEvents_Event_onmoveend;

		// Token: 0x14000731 RID: 1841
		// (add) Token: 0x06004592 RID: 17810
		// (remove) Token: 0x06004593 RID: 17811
		public virtual extern event HTMLLabelEvents_onresizestartEventHandler HTMLLabelEvents_Event_onresizestart;

		// Token: 0x14000732 RID: 1842
		// (add) Token: 0x06004594 RID: 17812
		// (remove) Token: 0x06004595 RID: 17813
		public virtual extern event HTMLLabelEvents_onresizeendEventHandler HTMLLabelEvents_Event_onresizeend;

		// Token: 0x14000733 RID: 1843
		// (add) Token: 0x06004596 RID: 17814
		// (remove) Token: 0x06004597 RID: 17815
		public virtual extern event HTMLLabelEvents_onmouseenterEventHandler HTMLLabelEvents_Event_onmouseenter;

		// Token: 0x14000734 RID: 1844
		// (add) Token: 0x06004598 RID: 17816
		// (remove) Token: 0x06004599 RID: 17817
		public virtual extern event HTMLLabelEvents_onmouseleaveEventHandler HTMLLabelEvents_Event_onmouseleave;

		// Token: 0x14000735 RID: 1845
		// (add) Token: 0x0600459A RID: 17818
		// (remove) Token: 0x0600459B RID: 17819
		public virtual extern event HTMLLabelEvents_onmousewheelEventHandler HTMLLabelEvents_Event_onmousewheel;

		// Token: 0x14000736 RID: 1846
		// (add) Token: 0x0600459C RID: 17820
		// (remove) Token: 0x0600459D RID: 17821
		public virtual extern event HTMLLabelEvents_onactivateEventHandler HTMLLabelEvents_Event_onactivate;

		// Token: 0x14000737 RID: 1847
		// (add) Token: 0x0600459E RID: 17822
		// (remove) Token: 0x0600459F RID: 17823
		public virtual extern event HTMLLabelEvents_ondeactivateEventHandler HTMLLabelEvents_Event_ondeactivate;

		// Token: 0x14000738 RID: 1848
		// (add) Token: 0x060045A0 RID: 17824
		// (remove) Token: 0x060045A1 RID: 17825
		public virtual extern event HTMLLabelEvents_onfocusinEventHandler HTMLLabelEvents_Event_onfocusin;

		// Token: 0x14000739 RID: 1849
		// (add) Token: 0x060045A2 RID: 17826
		// (remove) Token: 0x060045A3 RID: 17827
		public virtual extern event HTMLLabelEvents_onfocusoutEventHandler HTMLLabelEvents_Event_onfocusout;

		// Token: 0x1400073A RID: 1850
		// (add) Token: 0x060045A4 RID: 17828
		// (remove) Token: 0x060045A5 RID: 17829
		public virtual extern event HTMLLabelEvents2_onhelpEventHandler HTMLLabelEvents2_Event_onhelp;

		// Token: 0x1400073B RID: 1851
		// (add) Token: 0x060045A6 RID: 17830
		// (remove) Token: 0x060045A7 RID: 17831
		public virtual extern event HTMLLabelEvents2_onclickEventHandler HTMLLabelEvents2_Event_onclick;

		// Token: 0x1400073C RID: 1852
		// (add) Token: 0x060045A8 RID: 17832
		// (remove) Token: 0x060045A9 RID: 17833
		public virtual extern event HTMLLabelEvents2_ondblclickEventHandler HTMLLabelEvents2_Event_ondblclick;

		// Token: 0x1400073D RID: 1853
		// (add) Token: 0x060045AA RID: 17834
		// (remove) Token: 0x060045AB RID: 17835
		public virtual extern event HTMLLabelEvents2_onkeypressEventHandler HTMLLabelEvents2_Event_onkeypress;

		// Token: 0x1400073E RID: 1854
		// (add) Token: 0x060045AC RID: 17836
		// (remove) Token: 0x060045AD RID: 17837
		public virtual extern event HTMLLabelEvents2_onkeydownEventHandler HTMLLabelEvents2_Event_onkeydown;

		// Token: 0x1400073F RID: 1855
		// (add) Token: 0x060045AE RID: 17838
		// (remove) Token: 0x060045AF RID: 17839
		public virtual extern event HTMLLabelEvents2_onkeyupEventHandler HTMLLabelEvents2_Event_onkeyup;

		// Token: 0x14000740 RID: 1856
		// (add) Token: 0x060045B0 RID: 17840
		// (remove) Token: 0x060045B1 RID: 17841
		public virtual extern event HTMLLabelEvents2_onmouseoutEventHandler HTMLLabelEvents2_Event_onmouseout;

		// Token: 0x14000741 RID: 1857
		// (add) Token: 0x060045B2 RID: 17842
		// (remove) Token: 0x060045B3 RID: 17843
		public virtual extern event HTMLLabelEvents2_onmouseoverEventHandler HTMLLabelEvents2_Event_onmouseover;

		// Token: 0x14000742 RID: 1858
		// (add) Token: 0x060045B4 RID: 17844
		// (remove) Token: 0x060045B5 RID: 17845
		public virtual extern event HTMLLabelEvents2_onmousemoveEventHandler HTMLLabelEvents2_Event_onmousemove;

		// Token: 0x14000743 RID: 1859
		// (add) Token: 0x060045B6 RID: 17846
		// (remove) Token: 0x060045B7 RID: 17847
		public virtual extern event HTMLLabelEvents2_onmousedownEventHandler HTMLLabelEvents2_Event_onmousedown;

		// Token: 0x14000744 RID: 1860
		// (add) Token: 0x060045B8 RID: 17848
		// (remove) Token: 0x060045B9 RID: 17849
		public virtual extern event HTMLLabelEvents2_onmouseupEventHandler HTMLLabelEvents2_Event_onmouseup;

		// Token: 0x14000745 RID: 1861
		// (add) Token: 0x060045BA RID: 17850
		// (remove) Token: 0x060045BB RID: 17851
		public virtual extern event HTMLLabelEvents2_onselectstartEventHandler HTMLLabelEvents2_Event_onselectstart;

		// Token: 0x14000746 RID: 1862
		// (add) Token: 0x060045BC RID: 17852
		// (remove) Token: 0x060045BD RID: 17853
		public virtual extern event HTMLLabelEvents2_onfilterchangeEventHandler HTMLLabelEvents2_Event_onfilterchange;

		// Token: 0x14000747 RID: 1863
		// (add) Token: 0x060045BE RID: 17854
		// (remove) Token: 0x060045BF RID: 17855
		public virtual extern event HTMLLabelEvents2_ondragstartEventHandler HTMLLabelEvents2_Event_ondragstart;

		// Token: 0x14000748 RID: 1864
		// (add) Token: 0x060045C0 RID: 17856
		// (remove) Token: 0x060045C1 RID: 17857
		public virtual extern event HTMLLabelEvents2_onbeforeupdateEventHandler HTMLLabelEvents2_Event_onbeforeupdate;

		// Token: 0x14000749 RID: 1865
		// (add) Token: 0x060045C2 RID: 17858
		// (remove) Token: 0x060045C3 RID: 17859
		public virtual extern event HTMLLabelEvents2_onafterupdateEventHandler HTMLLabelEvents2_Event_onafterupdate;

		// Token: 0x1400074A RID: 1866
		// (add) Token: 0x060045C4 RID: 17860
		// (remove) Token: 0x060045C5 RID: 17861
		public virtual extern event HTMLLabelEvents2_onerrorupdateEventHandler HTMLLabelEvents2_Event_onerrorupdate;

		// Token: 0x1400074B RID: 1867
		// (add) Token: 0x060045C6 RID: 17862
		// (remove) Token: 0x060045C7 RID: 17863
		public virtual extern event HTMLLabelEvents2_onrowexitEventHandler HTMLLabelEvents2_Event_onrowexit;

		// Token: 0x1400074C RID: 1868
		// (add) Token: 0x060045C8 RID: 17864
		// (remove) Token: 0x060045C9 RID: 17865
		public virtual extern event HTMLLabelEvents2_onrowenterEventHandler HTMLLabelEvents2_Event_onrowenter;

		// Token: 0x1400074D RID: 1869
		// (add) Token: 0x060045CA RID: 17866
		// (remove) Token: 0x060045CB RID: 17867
		public virtual extern event HTMLLabelEvents2_ondatasetchangedEventHandler HTMLLabelEvents2_Event_ondatasetchanged;

		// Token: 0x1400074E RID: 1870
		// (add) Token: 0x060045CC RID: 17868
		// (remove) Token: 0x060045CD RID: 17869
		public virtual extern event HTMLLabelEvents2_ondataavailableEventHandler HTMLLabelEvents2_Event_ondataavailable;

		// Token: 0x1400074F RID: 1871
		// (add) Token: 0x060045CE RID: 17870
		// (remove) Token: 0x060045CF RID: 17871
		public virtual extern event HTMLLabelEvents2_ondatasetcompleteEventHandler HTMLLabelEvents2_Event_ondatasetcomplete;

		// Token: 0x14000750 RID: 1872
		// (add) Token: 0x060045D0 RID: 17872
		// (remove) Token: 0x060045D1 RID: 17873
		public virtual extern event HTMLLabelEvents2_onlosecaptureEventHandler HTMLLabelEvents2_Event_onlosecapture;

		// Token: 0x14000751 RID: 1873
		// (add) Token: 0x060045D2 RID: 17874
		// (remove) Token: 0x060045D3 RID: 17875
		public virtual extern event HTMLLabelEvents2_onpropertychangeEventHandler HTMLLabelEvents2_Event_onpropertychange;

		// Token: 0x14000752 RID: 1874
		// (add) Token: 0x060045D4 RID: 17876
		// (remove) Token: 0x060045D5 RID: 17877
		public virtual extern event HTMLLabelEvents2_onscrollEventHandler HTMLLabelEvents2_Event_onscroll;

		// Token: 0x14000753 RID: 1875
		// (add) Token: 0x060045D6 RID: 17878
		// (remove) Token: 0x060045D7 RID: 17879
		public virtual extern event HTMLLabelEvents2_onfocusEventHandler HTMLLabelEvents2_Event_onfocus;

		// Token: 0x14000754 RID: 1876
		// (add) Token: 0x060045D8 RID: 17880
		// (remove) Token: 0x060045D9 RID: 17881
		public virtual extern event HTMLLabelEvents2_onblurEventHandler HTMLLabelEvents2_Event_onblur;

		// Token: 0x14000755 RID: 1877
		// (add) Token: 0x060045DA RID: 17882
		// (remove) Token: 0x060045DB RID: 17883
		public virtual extern event HTMLLabelEvents2_onresizeEventHandler HTMLLabelEvents2_Event_onresize;

		// Token: 0x14000756 RID: 1878
		// (add) Token: 0x060045DC RID: 17884
		// (remove) Token: 0x060045DD RID: 17885
		public virtual extern event HTMLLabelEvents2_ondragEventHandler HTMLLabelEvents2_Event_ondrag;

		// Token: 0x14000757 RID: 1879
		// (add) Token: 0x060045DE RID: 17886
		// (remove) Token: 0x060045DF RID: 17887
		public virtual extern event HTMLLabelEvents2_ondragendEventHandler HTMLLabelEvents2_Event_ondragend;

		// Token: 0x14000758 RID: 1880
		// (add) Token: 0x060045E0 RID: 17888
		// (remove) Token: 0x060045E1 RID: 17889
		public virtual extern event HTMLLabelEvents2_ondragenterEventHandler HTMLLabelEvents2_Event_ondragenter;

		// Token: 0x14000759 RID: 1881
		// (add) Token: 0x060045E2 RID: 17890
		// (remove) Token: 0x060045E3 RID: 17891
		public virtual extern event HTMLLabelEvents2_ondragoverEventHandler HTMLLabelEvents2_Event_ondragover;

		// Token: 0x1400075A RID: 1882
		// (add) Token: 0x060045E4 RID: 17892
		// (remove) Token: 0x060045E5 RID: 17893
		public virtual extern event HTMLLabelEvents2_ondragleaveEventHandler HTMLLabelEvents2_Event_ondragleave;

		// Token: 0x1400075B RID: 1883
		// (add) Token: 0x060045E6 RID: 17894
		// (remove) Token: 0x060045E7 RID: 17895
		public virtual extern event HTMLLabelEvents2_ondropEventHandler HTMLLabelEvents2_Event_ondrop;

		// Token: 0x1400075C RID: 1884
		// (add) Token: 0x060045E8 RID: 17896
		// (remove) Token: 0x060045E9 RID: 17897
		public virtual extern event HTMLLabelEvents2_onbeforecutEventHandler HTMLLabelEvents2_Event_onbeforecut;

		// Token: 0x1400075D RID: 1885
		// (add) Token: 0x060045EA RID: 17898
		// (remove) Token: 0x060045EB RID: 17899
		public virtual extern event HTMLLabelEvents2_oncutEventHandler HTMLLabelEvents2_Event_oncut;

		// Token: 0x1400075E RID: 1886
		// (add) Token: 0x060045EC RID: 17900
		// (remove) Token: 0x060045ED RID: 17901
		public virtual extern event HTMLLabelEvents2_onbeforecopyEventHandler HTMLLabelEvents2_Event_onbeforecopy;

		// Token: 0x1400075F RID: 1887
		// (add) Token: 0x060045EE RID: 17902
		// (remove) Token: 0x060045EF RID: 17903
		public virtual extern event HTMLLabelEvents2_oncopyEventHandler HTMLLabelEvents2_Event_oncopy;

		// Token: 0x14000760 RID: 1888
		// (add) Token: 0x060045F0 RID: 17904
		// (remove) Token: 0x060045F1 RID: 17905
		public virtual extern event HTMLLabelEvents2_onbeforepasteEventHandler HTMLLabelEvents2_Event_onbeforepaste;

		// Token: 0x14000761 RID: 1889
		// (add) Token: 0x060045F2 RID: 17906
		// (remove) Token: 0x060045F3 RID: 17907
		public virtual extern event HTMLLabelEvents2_onpasteEventHandler HTMLLabelEvents2_Event_onpaste;

		// Token: 0x14000762 RID: 1890
		// (add) Token: 0x060045F4 RID: 17908
		// (remove) Token: 0x060045F5 RID: 17909
		public virtual extern event HTMLLabelEvents2_oncontextmenuEventHandler HTMLLabelEvents2_Event_oncontextmenu;

		// Token: 0x14000763 RID: 1891
		// (add) Token: 0x060045F6 RID: 17910
		// (remove) Token: 0x060045F7 RID: 17911
		public virtual extern event HTMLLabelEvents2_onrowsdeleteEventHandler HTMLLabelEvents2_Event_onrowsdelete;

		// Token: 0x14000764 RID: 1892
		// (add) Token: 0x060045F8 RID: 17912
		// (remove) Token: 0x060045F9 RID: 17913
		public virtual extern event HTMLLabelEvents2_onrowsinsertedEventHandler HTMLLabelEvents2_Event_onrowsinserted;

		// Token: 0x14000765 RID: 1893
		// (add) Token: 0x060045FA RID: 17914
		// (remove) Token: 0x060045FB RID: 17915
		public virtual extern event HTMLLabelEvents2_oncellchangeEventHandler HTMLLabelEvents2_Event_oncellchange;

		// Token: 0x14000766 RID: 1894
		// (add) Token: 0x060045FC RID: 17916
		// (remove) Token: 0x060045FD RID: 17917
		public virtual extern event HTMLLabelEvents2_onreadystatechangeEventHandler HTMLLabelEvents2_Event_onreadystatechange;

		// Token: 0x14000767 RID: 1895
		// (add) Token: 0x060045FE RID: 17918
		// (remove) Token: 0x060045FF RID: 17919
		public virtual extern event HTMLLabelEvents2_onlayoutcompleteEventHandler HTMLLabelEvents2_Event_onlayoutcomplete;

		// Token: 0x14000768 RID: 1896
		// (add) Token: 0x06004600 RID: 17920
		// (remove) Token: 0x06004601 RID: 17921
		public virtual extern event HTMLLabelEvents2_onpageEventHandler HTMLLabelEvents2_Event_onpage;

		// Token: 0x14000769 RID: 1897
		// (add) Token: 0x06004602 RID: 17922
		// (remove) Token: 0x06004603 RID: 17923
		public virtual extern event HTMLLabelEvents2_onmouseenterEventHandler HTMLLabelEvents2_Event_onmouseenter;

		// Token: 0x1400076A RID: 1898
		// (add) Token: 0x06004604 RID: 17924
		// (remove) Token: 0x06004605 RID: 17925
		public virtual extern event HTMLLabelEvents2_onmouseleaveEventHandler HTMLLabelEvents2_Event_onmouseleave;

		// Token: 0x1400076B RID: 1899
		// (add) Token: 0x06004606 RID: 17926
		// (remove) Token: 0x06004607 RID: 17927
		public virtual extern event HTMLLabelEvents2_onactivateEventHandler HTMLLabelEvents2_Event_onactivate;

		// Token: 0x1400076C RID: 1900
		// (add) Token: 0x06004608 RID: 17928
		// (remove) Token: 0x06004609 RID: 17929
		public virtual extern event HTMLLabelEvents2_ondeactivateEventHandler HTMLLabelEvents2_Event_ondeactivate;

		// Token: 0x1400076D RID: 1901
		// (add) Token: 0x0600460A RID: 17930
		// (remove) Token: 0x0600460B RID: 17931
		public virtual extern event HTMLLabelEvents2_onbeforedeactivateEventHandler HTMLLabelEvents2_Event_onbeforedeactivate;

		// Token: 0x1400076E RID: 1902
		// (add) Token: 0x0600460C RID: 17932
		// (remove) Token: 0x0600460D RID: 17933
		public virtual extern event HTMLLabelEvents2_onbeforeactivateEventHandler HTMLLabelEvents2_Event_onbeforeactivate;

		// Token: 0x1400076F RID: 1903
		// (add) Token: 0x0600460E RID: 17934
		// (remove) Token: 0x0600460F RID: 17935
		public virtual extern event HTMLLabelEvents2_onfocusinEventHandler HTMLLabelEvents2_Event_onfocusin;

		// Token: 0x14000770 RID: 1904
		// (add) Token: 0x06004610 RID: 17936
		// (remove) Token: 0x06004611 RID: 17937
		public virtual extern event HTMLLabelEvents2_onfocusoutEventHandler HTMLLabelEvents2_Event_onfocusout;

		// Token: 0x14000771 RID: 1905
		// (add) Token: 0x06004612 RID: 17938
		// (remove) Token: 0x06004613 RID: 17939
		public virtual extern event HTMLLabelEvents2_onmoveEventHandler HTMLLabelEvents2_Event_onmove;

		// Token: 0x14000772 RID: 1906
		// (add) Token: 0x06004614 RID: 17940
		// (remove) Token: 0x06004615 RID: 17941
		public virtual extern event HTMLLabelEvents2_oncontrolselectEventHandler HTMLLabelEvents2_Event_oncontrolselect;

		// Token: 0x14000773 RID: 1907
		// (add) Token: 0x06004616 RID: 17942
		// (remove) Token: 0x06004617 RID: 17943
		public virtual extern event HTMLLabelEvents2_onmovestartEventHandler HTMLLabelEvents2_Event_onmovestart;

		// Token: 0x14000774 RID: 1908
		// (add) Token: 0x06004618 RID: 17944
		// (remove) Token: 0x06004619 RID: 17945
		public virtual extern event HTMLLabelEvents2_onmoveendEventHandler HTMLLabelEvents2_Event_onmoveend;

		// Token: 0x14000775 RID: 1909
		// (add) Token: 0x0600461A RID: 17946
		// (remove) Token: 0x0600461B RID: 17947
		public virtual extern event HTMLLabelEvents2_onresizestartEventHandler HTMLLabelEvents2_Event_onresizestart;

		// Token: 0x14000776 RID: 1910
		// (add) Token: 0x0600461C RID: 17948
		// (remove) Token: 0x0600461D RID: 17949
		public virtual extern event HTMLLabelEvents2_onresizeendEventHandler HTMLLabelEvents2_Event_onresizeend;

		// Token: 0x14000777 RID: 1911
		// (add) Token: 0x0600461E RID: 17950
		// (remove) Token: 0x0600461F RID: 17951
		public virtual extern event HTMLLabelEvents2_onmousewheelEventHandler HTMLLabelEvents2_Event_onmousewheel;
	}
}
