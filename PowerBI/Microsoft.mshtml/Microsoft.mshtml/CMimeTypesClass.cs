using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200079D RID: 1949
	[ClassInterface(0)]
	[TypeLibType(2)]
	[Guid("3050F3FE-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class CMimeTypesClass : IHTMLMimeTypesCollection, CMimeTypes
	{
		// Token: 0x0600D4BD RID: 54461
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern CMimeTypesClass();

		// Token: 0x1700464E RID: 17998
		// (get) Token: 0x0600D4BE RID: 54462
		[DispId(1)]
		public virtual extern int length
		{
			[DispId(1)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
