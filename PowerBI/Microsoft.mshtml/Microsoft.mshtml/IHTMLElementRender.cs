using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000093 RID: 147
	[InterfaceType(1)]
	[Guid("3050F669-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLElementRender
	{
		// Token: 0x06000D0E RID: 3342
		[MethodImpl(4096, MethodCodeType = 3)]
		void DrawToDC([ComAliasName("mshtml.wireHDC")] [In] ref _RemotableHandle hdc);

		// Token: 0x06000D0F RID: 3343
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetDocumentPrinter([MarshalAs(19)] [In] string bstrPrinterName, [ComAliasName("mshtml.wireHDC")] [In] ref _RemotableHandle hdc);
	}
}
