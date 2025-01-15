using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004BF RID: 1215
	[TypeLibType(4160)]
	[Guid("3050F1F3-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDTElement
	{
		// Token: 0x170021F5 RID: 8693
		// (get) Token: 0x0600661A RID: 26138
		// (set) Token: 0x06006619 RID: 26137
		[DispId(-2147413107)]
		bool noWrap
		{
			[DispId(-2147413107)]
			[TypeLibFunc(20)]
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
