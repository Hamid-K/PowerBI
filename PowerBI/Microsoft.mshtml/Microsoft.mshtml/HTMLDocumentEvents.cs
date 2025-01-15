using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007E0 RID: 2016
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F260-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLDocumentEvents
	{
		// Token: 0x0600DA40 RID: 55872
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp();

		// Token: 0x0600DA41 RID: 55873
		[DispId(-600)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onclick();

		// Token: 0x0600DA42 RID: 55874
		[DispId(-601)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondblclick();

		// Token: 0x0600DA43 RID: 55875
		[DispId(-602)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeydown();

		// Token: 0x0600DA44 RID: 55876
		[DispId(-604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onkeyup();

		// Token: 0x0600DA45 RID: 55877
		[DispId(-603)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onkeypress();

		// Token: 0x0600DA46 RID: 55878
		[DispId(-605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousedown();

		// Token: 0x0600DA47 RID: 55879
		[DispId(-606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmousemove();

		// Token: 0x0600DA48 RID: 55880
		[DispId(-607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseup();

		// Token: 0x0600DA49 RID: 55881
		[DispId(-2147418103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseout();

		// Token: 0x0600DA4A RID: 55882
		[DispId(-2147418104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onmouseover();

		// Token: 0x0600DA4B RID: 55883
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange();

		// Token: 0x0600DA4C RID: 55884
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate();

		// Token: 0x0600DA4D RID: 55885
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate();

		// Token: 0x0600DA4E RID: 55886
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit();

		// Token: 0x0600DA4F RID: 55887
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter();

		// Token: 0x0600DA50 RID: 55888
		[DispId(-2147418101)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool ondragstart();

		// Token: 0x0600DA51 RID: 55889
		[DispId(-2147418100)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onselectstart();

		// Token: 0x0600DA52 RID: 55890
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate();

		// Token: 0x0600DA53 RID: 55891
		[DispId(1023)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontextmenu();

		// Token: 0x0600DA54 RID: 55892
		[DispId(1026)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onstop();

		// Token: 0x0600DA55 RID: 55893
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete();

		// Token: 0x0600DA56 RID: 55894
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted();

		// Token: 0x0600DA57 RID: 55895
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange();

		// Token: 0x0600DA58 RID: 55896
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onpropertychange();

		// Token: 0x0600DA59 RID: 55897
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged();

		// Token: 0x0600DA5A RID: 55898
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable();

		// Token: 0x0600DA5B RID: 55899
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete();

		// Token: 0x0600DA5C RID: 55900
		[DispId(1027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onbeforeeditfocus();

		// Token: 0x0600DA5D RID: 55901
		[DispId(1037)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onselectionchange();

		// Token: 0x0600DA5E RID: 55902
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool oncontrolselect();

		// Token: 0x0600DA5F RID: 55903
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onmousewheel();

		// Token: 0x0600DA60 RID: 55904
		[DispId(1048)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusin();

		// Token: 0x0600DA61 RID: 55905
		[DispId(1049)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocusout();

		// Token: 0x0600DA62 RID: 55906
		[DispId(1044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onactivate();

		// Token: 0x0600DA63 RID: 55907
		[DispId(1045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondeactivate();

		// Token: 0x0600DA64 RID: 55908
		[DispId(1047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeactivate();

		// Token: 0x0600DA65 RID: 55909
		[DispId(1034)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforedeactivate();
	}
}
