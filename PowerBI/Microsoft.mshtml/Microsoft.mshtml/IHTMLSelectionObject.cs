using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000567 RID: 1383
	[Guid("3050F25A-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLSelectionObject
	{
		// Token: 0x060085FB RID: 34299
		[DispId(1001)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createRange();

		// Token: 0x060085FC RID: 34300
		[DispId(1002)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void empty();

		// Token: 0x060085FD RID: 34301
		[DispId(1003)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void clear();

		// Token: 0x17002CF8 RID: 11512
		// (get) Token: 0x060085FE RID: 34302
		[DispId(1004)]
		string type
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
