using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004BA RID: 1210
	[CoClass(typeof(HTMLDivElementClass))]
	[Guid("3050F50C-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLDivElement : DispHTMLDivElement, HTMLElementEvents_Event
	{
	}
}
