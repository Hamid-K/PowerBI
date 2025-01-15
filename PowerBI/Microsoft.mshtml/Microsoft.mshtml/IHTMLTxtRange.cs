using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001DC RID: 476
	[Guid("3050F220-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTxtRange
	{
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06001B2A RID: 6954
		[DispId(1003)]
		string htmlText
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06001B2C RID: 6956
		// (set) Token: 0x06001B2B RID: 6955
		[DispId(1004)]
		string text
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06001B2D RID: 6957
		[DispId(1006)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement parentElement();

		// Token: 0x06001B2E RID: 6958
		[DispId(1008)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTxtRange duplicate();

		// Token: 0x06001B2F RID: 6959
		[DispId(1010)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool inRange([MarshalAs(28)] [In] IHTMLTxtRange range);

		// Token: 0x06001B30 RID: 6960
		[DispId(1011)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool isEqual([MarshalAs(28)] [In] IHTMLTxtRange range);

		// Token: 0x06001B31 RID: 6961
		[DispId(1012)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void scrollIntoView([In] bool fStart = true);

		// Token: 0x06001B32 RID: 6962
		[DispId(1013)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void collapse([In] bool Start = true);

		// Token: 0x06001B33 RID: 6963
		[DispId(1014)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool expand([MarshalAs(19)] [In] string Unit);

		// Token: 0x06001B34 RID: 6964
		[DispId(1015)]
		[MethodImpl(4096, MethodCodeType = 3)]
		int move([MarshalAs(19)] [In] string Unit, [In] int Count = 1);

		// Token: 0x06001B35 RID: 6965
		[DispId(1016)]
		[MethodImpl(4096, MethodCodeType = 3)]
		int moveStart([MarshalAs(19)] [In] string Unit, [In] int Count = 1);

		// Token: 0x06001B36 RID: 6966
		[DispId(1017)]
		[MethodImpl(4096, MethodCodeType = 3)]
		int moveEnd([MarshalAs(19)] [In] string Unit, [In] int Count = 1);

		// Token: 0x06001B37 RID: 6967
		[DispId(1024)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void select();

		// Token: 0x06001B38 RID: 6968
		[DispId(1026)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void pasteHTML([MarshalAs(19)] [In] string html);

		// Token: 0x06001B39 RID: 6969
		[DispId(1001)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void moveToElementText([MarshalAs(28)] [In] IHTMLElement element);

		// Token: 0x06001B3A RID: 6970
		[DispId(1025)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void setEndPoint([MarshalAs(19)] [In] string how, [MarshalAs(28)] [In] IHTMLTxtRange SourceRange);

		// Token: 0x06001B3B RID: 6971
		[DispId(1018)]
		[MethodImpl(4096, MethodCodeType = 3)]
		int compareEndPoints([MarshalAs(19)] [In] string how, [MarshalAs(28)] [In] IHTMLTxtRange SourceRange);

		// Token: 0x06001B3C RID: 6972
		[DispId(1019)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool findText([MarshalAs(19)] [In] string String, [In] int Count = 1073741823, [In] int Flags = 0);

		// Token: 0x06001B3D RID: 6973
		[DispId(1020)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void moveToPoint([In] int x, [In] int y);

		// Token: 0x06001B3E RID: 6974
		[DispId(1021)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string getBookmark();

		// Token: 0x06001B3F RID: 6975
		[DispId(1009)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool moveToBookmark([MarshalAs(19)] [In] string Bookmark);

		// Token: 0x06001B40 RID: 6976
		[DispId(1027)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandSupported([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06001B41 RID: 6977
		[DispId(1028)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandEnabled([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06001B42 RID: 6978
		[DispId(1029)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandState([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06001B43 RID: 6979
		[DispId(1030)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandIndeterm([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06001B44 RID: 6980
		[DispId(1031)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string queryCommandText([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06001B45 RID: 6981
		[DispId(1032)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object queryCommandValue([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06001B46 RID: 6982
		[DispId(1033)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool execCommand([MarshalAs(19)] [In] string cmdID, [In] bool showUI = false, [MarshalAs(27)] [In] [Optional] object value);

		// Token: 0x06001B47 RID: 6983
		[DispId(1034)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool execCommandShowHelp([MarshalAs(19)] [In] string cmdID);
	}
}
