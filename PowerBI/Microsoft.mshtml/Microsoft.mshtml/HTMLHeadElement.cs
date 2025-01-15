using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200077D RID: 1917
	[Guid("3050F561-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLHeadElementClass))]
	[ComImport]
	public interface HTMLHeadElement : DispHTMLHeadElement, HTMLElementEvents_Event
	{
	}
}
