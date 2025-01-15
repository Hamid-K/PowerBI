using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200027D RID: 637
	[Guid("3050F38E-98B5-11CF-BB82-00AA00BDCE0B")]
	[DefaultMember("create")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLImageElementFactory
	{
		// Token: 0x06002852 RID: 10322
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLImgElement create([MarshalAs(27)] [In] [Optional] object width, [MarshalAs(27)] [In] [Optional] object height);
	}
}
