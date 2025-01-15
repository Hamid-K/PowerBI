using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BF6 RID: 3062
	[Guid("3050F3CB-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(2)]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLStyleElementEvents
	{
		// Token: 0x06015B5C RID: 88924
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp();

		// Token: 0x06015B5D RID: 88925
		[DispId(-600)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onclick();

		// Token: 0x06015B5E RID: 88926
		[DispId(-601)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondblclick();

		// Token: 0x06015B5F RID: 88927
		[DispId(-603)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onkeypress();

		// Token: 0x06015B60 RID: 88928
		[DispId(-602)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeydown();

		// Token: 0x06015B61 RID: 88929
		[DispId(-604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeyup();

		// Token: 0x06015B62 RID: 88930
		[DispId(-2147418103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseout();

		// Token: 0x06015B63 RID: 88931
		[DispId(-2147418104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseover();

		// Token: 0x06015B64 RID: 88932
		[DispId(-606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousemove();

		// Token: 0x06015B65 RID: 88933
		[DispId(-605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousedown();

		// Token: 0x06015B66 RID: 88934
		[DispId(-607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseup();

		// Token: 0x06015B67 RID: 88935
		[DispId(-2147418100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onselectstart();

		// Token: 0x06015B68 RID: 88936
		[DispId(-2147418095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfilterchange();

		// Token: 0x06015B69 RID: 88937
		[DispId(-2147418101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragstart();

		// Token: 0x06015B6A RID: 88938
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate();

		// Token: 0x06015B6B RID: 88939
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate();

		// Token: 0x06015B6C RID: 88940
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate();

		// Token: 0x06015B6D RID: 88941
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit();

		// Token: 0x06015B6E RID: 88942
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter();

		// Token: 0x06015B6F RID: 88943
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged();

		// Token: 0x06015B70 RID: 88944
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable();

		// Token: 0x06015B71 RID: 88945
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete();

		// Token: 0x06015B72 RID: 88946
		[DispId(-2147418094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlosecapture();

		// Token: 0x06015B73 RID: 88947
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpropertychange();

		// Token: 0x06015B74 RID: 88948
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll();

		// Token: 0x06015B75 RID: 88949
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus();

		// Token: 0x06015B76 RID: 88950
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur();

		// Token: 0x06015B77 RID: 88951
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize();

		// Token: 0x06015B78 RID: 88952
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrag();

		// Token: 0x06015B79 RID: 88953
		[DispId(-2147418091)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragend();

		// Token: 0x06015B7A RID: 88954
		[DispId(-2147418090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragenter();

		// Token: 0x06015B7B RID: 88955
		[DispId(-2147418089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragover();

		// Token: 0x06015B7C RID: 88956
		[DispId(-2147418088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragleave();

		// Token: 0x06015B7D RID: 88957
		[DispId(-2147418087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrop();

		// Token: 0x06015B7E RID: 88958
		[DispId(-2147418083)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecut();

		// Token: 0x06015B7F RID: 88959
		[DispId(-2147418086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncut();

		// Token: 0x06015B80 RID: 88960
		[DispId(-2147418082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecopy();

		// Token: 0x06015B81 RID: 88961
		[DispId(-2147418085)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncopy();

		// Token: 0x06015B82 RID: 88962
		[DispId(-2147418081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforepaste();

		// Token: 0x06015B83 RID: 88963
		[DispId(-2147418084)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onpaste();

		// Token: 0x06015B84 RID: 88964
		[DispId(1023)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontextmenu();

		// Token: 0x06015B85 RID: 88965
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete();

		// Token: 0x06015B86 RID: 88966
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted();

		// Token: 0x06015B87 RID: 88967
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange();

		// Token: 0x06015B88 RID: 88968
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange();

		// Token: 0x06015B89 RID: 88969
		[DispId(1027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onbeforeeditfocus();

		// Token: 0x06015B8A RID: 88970
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlayoutcomplete();

		// Token: 0x06015B8B RID: 88971
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpage();

		// Token: 0x06015B8C RID: 88972
		[DispId(1034)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforedeactivate();

		// Token: 0x06015B8D RID: 88973
		[DispId(1047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeactivate();

		// Token: 0x06015B8E RID: 88974
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmove();

		// Token: 0x06015B8F RID: 88975
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontrolselect();

		// Token: 0x06015B90 RID: 88976
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmovestart();

		// Token: 0x06015B91 RID: 88977
		[DispId(1039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmoveend();

		// Token: 0x06015B92 RID: 88978
		[DispId(1040)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onresizestart();

		// Token: 0x06015B93 RID: 88979
		[DispId(1041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresizeend();

		// Token: 0x06015B94 RID: 88980
		[DispId(1042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseenter();

		// Token: 0x06015B95 RID: 88981
		[DispId(1043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseleave();

		// Token: 0x06015B96 RID: 88982
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmousewheel();

		// Token: 0x06015B97 RID: 88983
		[DispId(1044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onactivate();

		// Token: 0x06015B98 RID: 88984
		[DispId(1045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondeactivate();

		// Token: 0x06015B99 RID: 88985
		[DispId(1048)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusin();

		// Token: 0x06015B9A RID: 88986
		[DispId(1049)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusout();

		// Token: 0x06015B9B RID: 88987
		[DispId(1003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onload();

		// Token: 0x06015B9C RID: 88988
		[DispId(1002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onerror();
	}
}
