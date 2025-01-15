using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DA4 RID: 3492
	[Guid("3050F51D-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(htmlInputImageClass))]
	[ComImport]
	public interface htmlInputImage : DispIHTMLInputImage, HTMLInputImageEvents_Event
	{
	}
}
