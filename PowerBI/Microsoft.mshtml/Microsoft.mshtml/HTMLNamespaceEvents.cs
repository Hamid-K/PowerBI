using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CD4 RID: 3284
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F6BD-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLNamespaceEvents
	{
		// Token: 0x06016334 RID: 90932
		[DispId(-609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);
	}
}
