using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000796 RID: 1942
	[Guid("3050F3FC-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLMimeTypesCollection
	{
		// Token: 0x17004632 RID: 17970
		// (get) Token: 0x0600D484 RID: 54404
		[DispId(1)]
		int length
		{
			[DispId(1)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
