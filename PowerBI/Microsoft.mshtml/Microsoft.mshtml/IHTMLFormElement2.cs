using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001E3 RID: 483
	[TypeLibType(4160)]
	[Guid("3050F4F6-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLFormElement2
	{
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06001BED RID: 7149
		// (set) Token: 0x06001BEC RID: 7148
		[DispId(1011)]
		string acceptCharset
		{
			[DispId(1011)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1011)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06001BEE RID: 7150
		[DispId(1505)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object urns([MarshalAs(27)] [In] object urn);
	}
}
