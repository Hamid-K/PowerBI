using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200077A RID: 1914
	[Guid("3050F560-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLHtmlElementClass))]
	[ComImport]
	public interface HTMLHtmlElement : DispHTMLHtmlElement, HTMLElementEvents_Event
	{
	}
}
