using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200006D RID: 109
	[TypeLibType(4160)]
	[Guid("3050F809-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDOMTextNode2
	{
		// Token: 0x06000B1E RID: 2846
		[DispId(1004)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string substringData([In] int offset, [In] int Count);

		// Token: 0x06000B1F RID: 2847
		[DispId(1005)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void appendData([MarshalAs(19)] [In] string bstrstring);

		// Token: 0x06000B20 RID: 2848
		[DispId(1006)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void insertData([In] int offset, [MarshalAs(19)] [In] string bstrstring);

		// Token: 0x06000B21 RID: 2849
		[DispId(1007)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void deleteData([In] int offset, [In] int Count);

		// Token: 0x06000B22 RID: 2850
		[DispId(1008)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void replaceData([In] int offset, [In] int Count, [MarshalAs(19)] [In] string bstrstring);
	}
}
