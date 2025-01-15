using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200079E RID: 1950
	[Guid("3050F3FC-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(CMimeTypesClass))]
	[ComImport]
	public interface CMimeTypes : IHTMLMimeTypesCollection
	{
	}
}
