using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200014E RID: 334
	[InterfaceType(2)]
	[Guid("3050F61D-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLLinkElementEvents2
	{
		// Token: 0x06001431 RID: 5169
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001432 RID: 5170
		[DispId(-600)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onclick([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001433 RID: 5171
		[DispId(-601)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondblclick([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001434 RID: 5172
		[DispId(-603)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onkeypress([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001435 RID: 5173
		[DispId(-602)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeydown([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001436 RID: 5174
		[DispId(-604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeyup([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001437 RID: 5175
		[DispId(-2147418103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseout([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001438 RID: 5176
		[DispId(-2147418104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseover([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001439 RID: 5177
		[DispId(-606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousemove([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600143A RID: 5178
		[DispId(-605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousedown([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600143B RID: 5179
		[DispId(-607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseup([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600143C RID: 5180
		[DispId(-2147418100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onselectstart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600143D RID: 5181
		[DispId(-2147418095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfilterchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600143E RID: 5182
		[DispId(-2147418101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragstart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600143F RID: 5183
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001440 RID: 5184
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001441 RID: 5185
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001442 RID: 5186
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001443 RID: 5187
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001444 RID: 5188
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001445 RID: 5189
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001446 RID: 5190
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001447 RID: 5191
		[DispId(-2147418094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlosecapture([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001448 RID: 5192
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpropertychange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001449 RID: 5193
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600144A RID: 5194
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600144B RID: 5195
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600144C RID: 5196
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600144D RID: 5197
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrag([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600144E RID: 5198
		[DispId(-2147418091)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600144F RID: 5199
		[DispId(-2147418090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001450 RID: 5200
		[DispId(-2147418089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragover([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001451 RID: 5201
		[DispId(-2147418088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragleave([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001452 RID: 5202
		[DispId(-2147418087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrop([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001453 RID: 5203
		[DispId(-2147418083)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecut([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001454 RID: 5204
		[DispId(-2147418086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncut([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001455 RID: 5205
		[DispId(-2147418082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecopy([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001456 RID: 5206
		[DispId(-2147418085)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncopy([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001457 RID: 5207
		[DispId(-2147418081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforepaste([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001458 RID: 5208
		[DispId(-2147418084)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onpaste([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001459 RID: 5209
		[DispId(1023)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontextmenu([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600145A RID: 5210
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600145B RID: 5211
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600145C RID: 5212
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600145D RID: 5213
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600145E RID: 5214
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlayoutcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600145F RID: 5215
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpage([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001460 RID: 5216
		[DispId(1042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001461 RID: 5217
		[DispId(1043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseleave([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001462 RID: 5218
		[DispId(1044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001463 RID: 5219
		[DispId(1045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001464 RID: 5220
		[DispId(1034)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforedeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001465 RID: 5221
		[DispId(1047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001466 RID: 5222
		[DispId(1048)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusin([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001467 RID: 5223
		[DispId(1049)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusout([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001468 RID: 5224
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmove([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001469 RID: 5225
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontrolselect([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600146A RID: 5226
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmovestart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600146B RID: 5227
		[DispId(1039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmoveend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600146C RID: 5228
		[DispId(1040)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onresizestart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600146D RID: 5229
		[DispId(1041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresizeend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600146E RID: 5230
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmousewheel([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600146F RID: 5231
		[DispId(1003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onload([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06001470 RID: 5232
		[DispId(1002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onerror([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);
	}
}
