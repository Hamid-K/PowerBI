using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200056B RID: 1387
	[Guid("3050F38C-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[DefaultMember("create")]
	[ComImport]
	public interface IHTMLOptionElementFactory
	{
		// Token: 0x0600860E RID: 34318
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLOptionElement create([MarshalAs(27)] [In] [Optional] object text, [MarshalAs(27)] [In] [Optional] object value, [MarshalAs(27)] [In] [Optional] object defaultSelected, [MarshalAs(27)] [In] [Optional] object selected);
	}
}
