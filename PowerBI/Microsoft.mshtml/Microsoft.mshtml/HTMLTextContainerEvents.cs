using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000275 RID: 629
	[Guid("1FF6AA72-5842-11CF-A707-00AA00C0098D")]
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[ComImport]
	public interface HTMLTextContainerEvents
	{
		// Token: 0x06002743 RID: 10051
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp();

		// Token: 0x06002744 RID: 10052
		[DispId(-600)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onclick();

		// Token: 0x06002745 RID: 10053
		[DispId(-601)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondblclick();

		// Token: 0x06002746 RID: 10054
		[DispId(-603)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onkeypress();

		// Token: 0x06002747 RID: 10055
		[DispId(-602)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeydown();

		// Token: 0x06002748 RID: 10056
		[DispId(-604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeyup();

		// Token: 0x06002749 RID: 10057
		[DispId(-2147418103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseout();

		// Token: 0x0600274A RID: 10058
		[DispId(-2147418104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseover();

		// Token: 0x0600274B RID: 10059
		[DispId(-606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousemove();

		// Token: 0x0600274C RID: 10060
		[DispId(-605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousedown();

		// Token: 0x0600274D RID: 10061
		[DispId(-607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseup();

		// Token: 0x0600274E RID: 10062
		[DispId(-2147418100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onselectstart();

		// Token: 0x0600274F RID: 10063
		[DispId(-2147418095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfilterchange();

		// Token: 0x06002750 RID: 10064
		[DispId(-2147418101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragstart();

		// Token: 0x06002751 RID: 10065
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate();

		// Token: 0x06002752 RID: 10066
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate();

		// Token: 0x06002753 RID: 10067
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate();

		// Token: 0x06002754 RID: 10068
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit();

		// Token: 0x06002755 RID: 10069
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter();

		// Token: 0x06002756 RID: 10070
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged();

		// Token: 0x06002757 RID: 10071
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable();

		// Token: 0x06002758 RID: 10072
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete();

		// Token: 0x06002759 RID: 10073
		[DispId(-2147418094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlosecapture();

		// Token: 0x0600275A RID: 10074
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpropertychange();

		// Token: 0x0600275B RID: 10075
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll();

		// Token: 0x0600275C RID: 10076
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus();

		// Token: 0x0600275D RID: 10077
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur();

		// Token: 0x0600275E RID: 10078
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize();

		// Token: 0x0600275F RID: 10079
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrag();

		// Token: 0x06002760 RID: 10080
		[DispId(-2147418091)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragend();

		// Token: 0x06002761 RID: 10081
		[DispId(-2147418090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragenter();

		// Token: 0x06002762 RID: 10082
		[DispId(-2147418089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragover();

		// Token: 0x06002763 RID: 10083
		[DispId(-2147418088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragleave();

		// Token: 0x06002764 RID: 10084
		[DispId(-2147418087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrop();

		// Token: 0x06002765 RID: 10085
		[DispId(-2147418083)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecut();

		// Token: 0x06002766 RID: 10086
		[DispId(-2147418086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncut();

		// Token: 0x06002767 RID: 10087
		[DispId(-2147418082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecopy();

		// Token: 0x06002768 RID: 10088
		[DispId(-2147418085)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncopy();

		// Token: 0x06002769 RID: 10089
		[DispId(-2147418081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforepaste();

		// Token: 0x0600276A RID: 10090
		[DispId(-2147418084)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onpaste();

		// Token: 0x0600276B RID: 10091
		[DispId(1023)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontextmenu();

		// Token: 0x0600276C RID: 10092
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete();

		// Token: 0x0600276D RID: 10093
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted();

		// Token: 0x0600276E RID: 10094
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange();

		// Token: 0x0600276F RID: 10095
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange();

		// Token: 0x06002770 RID: 10096
		[DispId(1027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onbeforeeditfocus();

		// Token: 0x06002771 RID: 10097
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlayoutcomplete();

		// Token: 0x06002772 RID: 10098
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpage();

		// Token: 0x06002773 RID: 10099
		[DispId(1034)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforedeactivate();

		// Token: 0x06002774 RID: 10100
		[DispId(1047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeactivate();

		// Token: 0x06002775 RID: 10101
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmove();

		// Token: 0x06002776 RID: 10102
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontrolselect();

		// Token: 0x06002777 RID: 10103
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmovestart();

		// Token: 0x06002778 RID: 10104
		[DispId(1039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmoveend();

		// Token: 0x06002779 RID: 10105
		[DispId(1040)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onresizestart();

		// Token: 0x0600277A RID: 10106
		[DispId(1041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresizeend();

		// Token: 0x0600277B RID: 10107
		[DispId(1042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseenter();

		// Token: 0x0600277C RID: 10108
		[DispId(1043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseleave();

		// Token: 0x0600277D RID: 10109
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmousewheel();

		// Token: 0x0600277E RID: 10110
		[DispId(1044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onactivate();

		// Token: 0x0600277F RID: 10111
		[DispId(1045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondeactivate();

		// Token: 0x06002780 RID: 10112
		[DispId(1048)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusin();

		// Token: 0x06002781 RID: 10113
		[DispId(1049)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusout();

		// Token: 0x06002782 RID: 10114
		[DispId(1001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onchange();

		// Token: 0x06002783 RID: 10115
		[DispId(1006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onselect();
	}
}
