using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200041D RID: 1053
	[TypeLibType(4160)]
	[Guid("3050F32A-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLLabelElement
	{
		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x060041F1 RID: 16881
		// (set) Token: 0x060041F0 RID: 16880
		[DispId(1000)]
		string htmlFor
		{
			[TypeLibFunc(20)]
			[DispId(1000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1000)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x060041F3 RID: 16883
		// (set) Token: 0x060041F2 RID: 16882
		[DispId(-2147416107)]
		string accessKey
		{
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
