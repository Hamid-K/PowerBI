using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000396 RID: 918
	[Guid("3050F29D-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[ComImport]
	public interface HTMLAnchorEvents
	{
		// Token: 0x06003A68 RID: 14952
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp();

		// Token: 0x06003A69 RID: 14953
		[DispId(-600)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onclick();

		// Token: 0x06003A6A RID: 14954
		[DispId(-601)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondblclick();

		// Token: 0x06003A6B RID: 14955
		[DispId(-603)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onkeypress();

		// Token: 0x06003A6C RID: 14956
		[DispId(-602)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeydown();

		// Token: 0x06003A6D RID: 14957
		[DispId(-604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeyup();

		// Token: 0x06003A6E RID: 14958
		[DispId(-2147418103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseout();

		// Token: 0x06003A6F RID: 14959
		[DispId(-2147418104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseover();

		// Token: 0x06003A70 RID: 14960
		[DispId(-606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousemove();

		// Token: 0x06003A71 RID: 14961
		[DispId(-605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousedown();

		// Token: 0x06003A72 RID: 14962
		[DispId(-607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseup();

		// Token: 0x06003A73 RID: 14963
		[DispId(-2147418100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onselectstart();

		// Token: 0x06003A74 RID: 14964
		[DispId(-2147418095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfilterchange();

		// Token: 0x06003A75 RID: 14965
		[DispId(-2147418101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragstart();

		// Token: 0x06003A76 RID: 14966
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate();

		// Token: 0x06003A77 RID: 14967
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate();

		// Token: 0x06003A78 RID: 14968
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate();

		// Token: 0x06003A79 RID: 14969
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit();

		// Token: 0x06003A7A RID: 14970
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter();

		// Token: 0x06003A7B RID: 14971
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged();

		// Token: 0x06003A7C RID: 14972
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable();

		// Token: 0x06003A7D RID: 14973
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete();

		// Token: 0x06003A7E RID: 14974
		[DispId(-2147418094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlosecapture();

		// Token: 0x06003A7F RID: 14975
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpropertychange();

		// Token: 0x06003A80 RID: 14976
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll();

		// Token: 0x06003A81 RID: 14977
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus();

		// Token: 0x06003A82 RID: 14978
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur();

		// Token: 0x06003A83 RID: 14979
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize();

		// Token: 0x06003A84 RID: 14980
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrag();

		// Token: 0x06003A85 RID: 14981
		[DispId(-2147418091)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragend();

		// Token: 0x06003A86 RID: 14982
		[DispId(-2147418090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragenter();

		// Token: 0x06003A87 RID: 14983
		[DispId(-2147418089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragover();

		// Token: 0x06003A88 RID: 14984
		[DispId(-2147418088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragleave();

		// Token: 0x06003A89 RID: 14985
		[DispId(-2147418087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrop();

		// Token: 0x06003A8A RID: 14986
		[DispId(-2147418083)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecut();

		// Token: 0x06003A8B RID: 14987
		[DispId(-2147418086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncut();

		// Token: 0x06003A8C RID: 14988
		[DispId(-2147418082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecopy();

		// Token: 0x06003A8D RID: 14989
		[DispId(-2147418085)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncopy();

		// Token: 0x06003A8E RID: 14990
		[DispId(-2147418081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforepaste();

		// Token: 0x06003A8F RID: 14991
		[DispId(-2147418084)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onpaste();

		// Token: 0x06003A90 RID: 14992
		[DispId(1023)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontextmenu();

		// Token: 0x06003A91 RID: 14993
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete();

		// Token: 0x06003A92 RID: 14994
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted();

		// Token: 0x06003A93 RID: 14995
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange();

		// Token: 0x06003A94 RID: 14996
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange();

		// Token: 0x06003A95 RID: 14997
		[DispId(1027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onbeforeeditfocus();

		// Token: 0x06003A96 RID: 14998
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlayoutcomplete();

		// Token: 0x06003A97 RID: 14999
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpage();

		// Token: 0x06003A98 RID: 15000
		[DispId(1034)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforedeactivate();

		// Token: 0x06003A99 RID: 15001
		[DispId(1047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeactivate();

		// Token: 0x06003A9A RID: 15002
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmove();

		// Token: 0x06003A9B RID: 15003
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontrolselect();

		// Token: 0x06003A9C RID: 15004
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmovestart();

		// Token: 0x06003A9D RID: 15005
		[DispId(1039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmoveend();

		// Token: 0x06003A9E RID: 15006
		[DispId(1040)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onresizestart();

		// Token: 0x06003A9F RID: 15007
		[DispId(1041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresizeend();

		// Token: 0x06003AA0 RID: 15008
		[DispId(1042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseenter();

		// Token: 0x06003AA1 RID: 15009
		[DispId(1043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseleave();

		// Token: 0x06003AA2 RID: 15010
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmousewheel();

		// Token: 0x06003AA3 RID: 15011
		[DispId(1044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onactivate();

		// Token: 0x06003AA4 RID: 15012
		[DispId(1045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondeactivate();

		// Token: 0x06003AA5 RID: 15013
		[DispId(1048)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusin();

		// Token: 0x06003AA6 RID: 15014
		[DispId(1049)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusout();
	}
}
