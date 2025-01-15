using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001E5 RID: 485
	[TypeLibType(4160)]
	[Guid("3050F645-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLSubmitData
	{
		// Token: 0x06001BF0 RID: 7152
		[DispId(1012)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void appendNameValuePair([MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string value = "");

		// Token: 0x06001BF1 RID: 7153
		[DispId(1013)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void appendNameFilePair([MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string filename = "");

		// Token: 0x06001BF2 RID: 7154
		[DispId(1014)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void appendItemSeparator();
	}
}
