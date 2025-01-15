using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004C2 RID: 1218
	[CoClass(typeof(HTMLDTElementClass))]
	[Guid("3050F50D-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLDTElement : DispHTMLDTElement, HTMLElementEvents_Event
	{
	}
}
