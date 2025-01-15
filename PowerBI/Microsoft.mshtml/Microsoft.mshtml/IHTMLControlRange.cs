using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000277 RID: 631
	[DefaultMember("item")]
	[Guid("3050F29C-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLControlRange
	{
		// Token: 0x0600278D RID: 10125
		[DispId(1002)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void select();

		// Token: 0x0600278E RID: 10126
		[DispId(1003)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void add([MarshalAs(28)] [In] IHTMLControlElement item);

		// Token: 0x0600278F RID: 10127
		[DispId(1004)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void remove([In] int index);

		// Token: 0x06002790 RID: 10128
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement item([In] int index);

		// Token: 0x06002791 RID: 10129
		[DispId(1006)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06002792 RID: 10130
		[DispId(1007)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandSupported([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06002793 RID: 10131
		[DispId(1008)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandEnabled([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06002794 RID: 10132
		[DispId(1009)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandState([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06002795 RID: 10133
		[DispId(1010)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool queryCommandIndeterm([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06002796 RID: 10134
		[DispId(1011)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string queryCommandText([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06002797 RID: 10135
		[DispId(1012)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object queryCommandValue([MarshalAs(19)] [In] string cmdID);

		// Token: 0x06002798 RID: 10136
		[DispId(1013)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool execCommand([MarshalAs(19)] [In] string cmdID, [In] bool showUI = false, [MarshalAs(27)] [In] [Optional] object value);

		// Token: 0x06002799 RID: 10137
		[DispId(1014)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool execCommandShowHelp([MarshalAs(19)] [In] string cmdID);

		// Token: 0x0600279A RID: 10138
		[DispId(1015)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement commonParentElement();

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600279B RID: 10139
		[DispId(1005)]
		int length
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
