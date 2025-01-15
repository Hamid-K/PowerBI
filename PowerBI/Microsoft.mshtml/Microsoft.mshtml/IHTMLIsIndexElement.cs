using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000775 RID: 1909
	[TypeLibType(4160)]
	[Guid("3050F206-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLIsIndexElement
	{
		// Token: 0x17003859 RID: 14425
		// (get) Token: 0x0600AF8D RID: 44941
		// (set) Token: 0x0600AF8C RID: 44940
		[DispId(1010)]
		string prompt
		{
			[TypeLibFunc(20)]
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1010)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700385A RID: 14426
		// (get) Token: 0x0600AF8F RID: 44943
		// (set) Token: 0x0600AF8E RID: 44942
		[DispId(1011)]
		string action
		{
			[TypeLibFunc(20)]
			[DispId(1011)]
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
	}
}
