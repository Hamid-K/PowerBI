using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000577 RID: 1399
	[InterfaceType(2)]
	[Guid("3050F2A7-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLInputTextElementEvents
	{
		// Token: 0x06008BDF RID: 35807
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp();

		// Token: 0x06008BE0 RID: 35808
		[DispId(-600)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onclick();

		// Token: 0x06008BE1 RID: 35809
		[DispId(-601)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondblclick();

		// Token: 0x06008BE2 RID: 35810
		[DispId(-603)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onkeypress();

		// Token: 0x06008BE3 RID: 35811
		[DispId(-602)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeydown();

		// Token: 0x06008BE4 RID: 35812
		[DispId(-604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeyup();

		// Token: 0x06008BE5 RID: 35813
		[DispId(-2147418103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseout();

		// Token: 0x06008BE6 RID: 35814
		[DispId(-2147418104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseover();

		// Token: 0x06008BE7 RID: 35815
		[DispId(-606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousemove();

		// Token: 0x06008BE8 RID: 35816
		[DispId(-605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousedown();

		// Token: 0x06008BE9 RID: 35817
		[DispId(-607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseup();

		// Token: 0x06008BEA RID: 35818
		[DispId(-2147418100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onselectstart();

		// Token: 0x06008BEB RID: 35819
		[DispId(-2147418095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfilterchange();

		// Token: 0x06008BEC RID: 35820
		[DispId(-2147418101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragstart();

		// Token: 0x06008BED RID: 35821
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate();

		// Token: 0x06008BEE RID: 35822
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate();

		// Token: 0x06008BEF RID: 35823
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate();

		// Token: 0x06008BF0 RID: 35824
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit();

		// Token: 0x06008BF1 RID: 35825
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter();

		// Token: 0x06008BF2 RID: 35826
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged();

		// Token: 0x06008BF3 RID: 35827
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable();

		// Token: 0x06008BF4 RID: 35828
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete();

		// Token: 0x06008BF5 RID: 35829
		[DispId(-2147418094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlosecapture();

		// Token: 0x06008BF6 RID: 35830
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpropertychange();

		// Token: 0x06008BF7 RID: 35831
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll();

		// Token: 0x06008BF8 RID: 35832
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus();

		// Token: 0x06008BF9 RID: 35833
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur();

		// Token: 0x06008BFA RID: 35834
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize();

		// Token: 0x06008BFB RID: 35835
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrag();

		// Token: 0x06008BFC RID: 35836
		[DispId(-2147418091)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragend();

		// Token: 0x06008BFD RID: 35837
		[DispId(-2147418090)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragenter();

		// Token: 0x06008BFE RID: 35838
		[DispId(-2147418089)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragover();

		// Token: 0x06008BFF RID: 35839
		[DispId(-2147418088)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondragleave();

		// Token: 0x06008C00 RID: 35840
		[DispId(-2147418087)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondrop();

		// Token: 0x06008C01 RID: 35841
		[DispId(-2147418083)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecut();

		// Token: 0x06008C02 RID: 35842
		[DispId(-2147418086)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncut();

		// Token: 0x06008C03 RID: 35843
		[DispId(-2147418082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforecopy();

		// Token: 0x06008C04 RID: 35844
		[DispId(-2147418085)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncopy();

		// Token: 0x06008C05 RID: 35845
		[DispId(-2147418081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforepaste();

		// Token: 0x06008C06 RID: 35846
		[DispId(-2147418084)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onpaste();

		// Token: 0x06008C07 RID: 35847
		[DispId(1023)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontextmenu();

		// Token: 0x06008C08 RID: 35848
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete();

		// Token: 0x06008C09 RID: 35849
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted();

		// Token: 0x06008C0A RID: 35850
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange();

		// Token: 0x06008C0B RID: 35851
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange();

		// Token: 0x06008C0C RID: 35852
		[DispId(1027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onbeforeeditfocus();

		// Token: 0x06008C0D RID: 35853
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onlayoutcomplete();

		// Token: 0x06008C0E RID: 35854
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpage();

		// Token: 0x06008C0F RID: 35855
		[DispId(1034)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforedeactivate();

		// Token: 0x06008C10 RID: 35856
		[DispId(1047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeactivate();

		// Token: 0x06008C11 RID: 35857
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmove();

		// Token: 0x06008C12 RID: 35858
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontrolselect();

		// Token: 0x06008C13 RID: 35859
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmovestart();

		// Token: 0x06008C14 RID: 35860
		[DispId(1039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmoveend();

		// Token: 0x06008C15 RID: 35861
		[DispId(1040)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onresizestart();

		// Token: 0x06008C16 RID: 35862
		[DispId(1041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresizeend();

		// Token: 0x06008C17 RID: 35863
		[DispId(1042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseenter();

		// Token: 0x06008C18 RID: 35864
		[DispId(1043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseleave();

		// Token: 0x06008C19 RID: 35865
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmousewheel();

		// Token: 0x06008C1A RID: 35866
		[DispId(1044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onactivate();

		// Token: 0x06008C1B RID: 35867
		[DispId(1045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondeactivate();

		// Token: 0x06008C1C RID: 35868
		[DispId(1048)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusin();

		// Token: 0x06008C1D RID: 35869
		[DispId(1049)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusout();

		// Token: 0x06008C1E RID: 35870
		[DispId(1001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onchange();

		// Token: 0x06008C1F RID: 35871
		[DispId(1006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onselect();

		// Token: 0x06008C20 RID: 35872
		[DispId(1003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onload();

		// Token: 0x06008C21 RID: 35873
		[DispId(1002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onerror();

		// Token: 0x06008C22 RID: 35874
		[DispId(1000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onabort();
	}
}
