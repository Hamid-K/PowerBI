using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004BB RID: 1211
	[TypeLibType(4160)]
	[Guid("3050F1F2-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDDElement
	{
		// Token: 0x1700206E RID: 8302
		// (get) Token: 0x06006204 RID: 25092
		// (set) Token: 0x06006203 RID: 25091
		[DispId(-2147413107)]
		bool noWrap
		{
			[TypeLibFunc(20)]
			[DispId(-2147413107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
