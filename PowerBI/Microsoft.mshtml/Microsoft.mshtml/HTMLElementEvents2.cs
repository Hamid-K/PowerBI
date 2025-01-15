using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000081 RID: 129
	[InterfaceType(2)]
	[Guid("3050F60F-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLElementEvents2
	{
		// Token: 0x06000BF2 RID: 3058
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BF3 RID: 3059
		[DispId(-600)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onclick([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BF4 RID: 3060
		[DispId(-601)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondblclick([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BF5 RID: 3061
		[DispId(-603)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onkeypress([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BF6 RID: 3062
		[DispId(-602)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeydown([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BF7 RID: 3063
		[DispId(-604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeyup([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BF8 RID: 3064
		[DispId(-2147418103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseout([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BF9 RID: 3065
		[DispId(-2147418104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseover([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BFA RID: 3066
		[DispId(-606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousemove([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BFB RID: 3067
		[DispId(-605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousedown([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BFC RID: 3068
		[DispId(-607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseup([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BFD RID: 3069
		[DispId(-2147418100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onselectstart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BFE RID: 3070
		[DispId(-2147418095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfilterchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000BFF RID: 3071
		[DispId(-2147418101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragstart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C00 RID: 3072
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C01 RID: 3073
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C02 RID: 3074
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C03 RID: 3075
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C04 RID: 3076
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C05 RID: 3077
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C06 RID: 3078
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C07 RID: 3079
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C08 RID: 3080
		[DispId(-2147418094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlosecapture([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C09 RID: 3081
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpropertychange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C0A RID: 3082
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C0B RID: 3083
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C0C RID: 3084
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C0D RID: 3085
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C0E RID: 3086
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrag([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C0F RID: 3087
		[DispId(-2147418091)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C10 RID: 3088
		[DispId(-2147418090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C11 RID: 3089
		[DispId(-2147418089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragover([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C12 RID: 3090
		[DispId(-2147418088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragleave([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C13 RID: 3091
		[DispId(-2147418087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrop([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C14 RID: 3092
		[DispId(-2147418083)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecut([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C15 RID: 3093
		[DispId(-2147418086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncut([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C16 RID: 3094
		[DispId(-2147418082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecopy([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C17 RID: 3095
		[DispId(-2147418085)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncopy([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C18 RID: 3096
		[DispId(-2147418081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforepaste([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C19 RID: 3097
		[DispId(-2147418084)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onpaste([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C1A RID: 3098
		[DispId(1023)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontextmenu([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C1B RID: 3099
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C1C RID: 3100
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C1D RID: 3101
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C1E RID: 3102
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C1F RID: 3103
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlayoutcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C20 RID: 3104
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpage([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C21 RID: 3105
		[DispId(1042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C22 RID: 3106
		[DispId(1043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseleave([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C23 RID: 3107
		[DispId(1044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C24 RID: 3108
		[DispId(1045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C25 RID: 3109
		[DispId(1034)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforedeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C26 RID: 3110
		[DispId(1047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeactivate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C27 RID: 3111
		[DispId(1048)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusin([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C28 RID: 3112
		[DispId(1049)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusout([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C29 RID: 3113
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmove([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C2A RID: 3114
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontrolselect([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C2B RID: 3115
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmovestart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C2C RID: 3116
		[DispId(1039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmoveend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C2D RID: 3117
		[DispId(1040)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onresizestart([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C2E RID: 3118
		[DispId(1041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresizeend([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06000C2F RID: 3119
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmousewheel([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);
	}
}
