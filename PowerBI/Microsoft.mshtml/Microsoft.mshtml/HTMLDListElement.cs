using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004CA RID: 1226
	[CoClass(typeof(HTMLDListElementClass))]
	[Guid("3050F53B-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLDListElement : DispHTMLDListElement, HTMLElementEvents_Event
	{
	}
}
