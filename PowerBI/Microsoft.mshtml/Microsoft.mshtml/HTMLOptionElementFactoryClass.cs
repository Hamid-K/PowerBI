using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200056F RID: 1391
	[Guid("3050F38D-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[DefaultMember("create")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLOptionElementFactoryClass : IHTMLOptionElementFactory, HTMLOptionElementFactory
	{
		// Token: 0x06008A56 RID: 35414
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLOptionElementFactoryClass();

		// Token: 0x06008A57 RID: 35415
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLOptionElement create([MarshalAs(27)] [In] [Optional] object text, [MarshalAs(27)] [In] [Optional] object value, [MarshalAs(27)] [In] [Optional] object defaultSelected, [MarshalAs(27)] [In] [Optional] object selected);
	}
}
