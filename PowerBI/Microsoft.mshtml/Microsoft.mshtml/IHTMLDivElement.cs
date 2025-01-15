using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004B7 RID: 1207
	[Guid("3050F200-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLDivElement
	{
		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x06005CB7 RID: 23735
		// (set) Token: 0x06005CB6 RID: 23734
		[DispId(-2147418040)]
		string align
		{
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x06005CB9 RID: 23737
		// (set) Token: 0x06005CB8 RID: 23736
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
