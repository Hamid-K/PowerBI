using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000076 RID: 118
	[ClassInterface(0)]
	[TypeLibType(2)]
	[Guid("3050F80E-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLDOMImplementationClass : DispHTMLDOMImplementation, HTMLDOMImplementation, IHTMLDOMImplementation
	{
		// Token: 0x06000BC9 RID: 3017
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLDOMImplementationClass();

		// Token: 0x06000BCA RID: 3018
		[DispId(1000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasFeature([MarshalAs(19)] [In] string bstrfeature, [MarshalAs(27)] [In] [Optional] object version);

		// Token: 0x06000BCB RID: 3019
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMImplementation_hasFeature([MarshalAs(19)] [In] string bstrfeature, [MarshalAs(27)] [In] [Optional] object version);
	}
}
