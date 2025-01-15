using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000279 RID: 633
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F616-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLImgEvents2
	{
		// Token: 0x0600279D RID: 10141
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600279E RID: 10142
		[DispId(-600)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onclick([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600279F RID: 10143
		[DispId(-601)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondblclick([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027A0 RID: 10144
		[DispId(-603)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onkeypress([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027A1 RID: 10145
		[DispId(-602)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeydown([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027A2 RID: 10146
		[DispId(-604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeyup([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027A3 RID: 10147
		[DispId(-2147418103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseout([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027A4 RID: 10148
		[DispId(-2147418104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseover([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027A5 RID: 10149
		[DispId(-606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousemove([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027A6 RID: 10150
		[DispId(-605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousedown([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027A7 RID: 10151
		[DispId(-607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseup([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027A8 RID: 10152
		[DispId(-2147418100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onselectstart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027A9 RID: 10153
		[DispId(-2147418095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfilterchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027AA RID: 10154
		[DispId(-2147418101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragstart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027AB RID: 10155
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027AC RID: 10156
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027AD RID: 10157
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027AE RID: 10158
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027AF RID: 10159
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027B0 RID: 10160
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027B1 RID: 10161
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027B2 RID: 10162
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027B3 RID: 10163
		[DispId(-2147418094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlosecapture([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027B4 RID: 10164
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpropertychange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027B5 RID: 10165
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027B6 RID: 10166
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027B7 RID: 10167
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027B8 RID: 10168
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027B9 RID: 10169
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrag([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027BA RID: 10170
		[DispId(-2147418091)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027BB RID: 10171
		[DispId(-2147418090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027BC RID: 10172
		[DispId(-2147418089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragover([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027BD RID: 10173
		[DispId(-2147418088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragleave([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027BE RID: 10174
		[DispId(-2147418087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrop([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027BF RID: 10175
		[DispId(-2147418083)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecut([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027C0 RID: 10176
		[DispId(-2147418086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncut([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027C1 RID: 10177
		[DispId(-2147418082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecopy([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027C2 RID: 10178
		[DispId(-2147418085)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncopy([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027C3 RID: 10179
		[DispId(-2147418081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforepaste([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027C4 RID: 10180
		[DispId(-2147418084)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onpaste([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027C5 RID: 10181
		[DispId(1023)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontextmenu([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027C6 RID: 10182
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027C7 RID: 10183
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027C8 RID: 10184
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027C9 RID: 10185
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027CA RID: 10186
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlayoutcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027CB RID: 10187
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpage([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027CC RID: 10188
		[DispId(1042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027CD RID: 10189
		[DispId(1043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseleave([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027CE RID: 10190
		[DispId(1044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027CF RID: 10191
		[DispId(1045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027D0 RID: 10192
		[DispId(1034)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforedeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027D1 RID: 10193
		[DispId(1047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027D2 RID: 10194
		[DispId(1048)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusin([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027D3 RID: 10195
		[DispId(1049)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusout([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027D4 RID: 10196
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmove([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027D5 RID: 10197
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontrolselect([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027D6 RID: 10198
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmovestart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027D7 RID: 10199
		[DispId(1039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmoveend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027D8 RID: 10200
		[DispId(1040)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onresizestart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027D9 RID: 10201
		[DispId(1041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresizeend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027DA RID: 10202
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmousewheel([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027DB RID: 10203
		[DispId(1003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onload([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027DC RID: 10204
		[DispId(1002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onerror([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x060027DD RID: 10205
		[DispId(1000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onabort([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);
	}
}
