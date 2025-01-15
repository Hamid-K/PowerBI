using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200041A RID: 1050
	[CoClass(typeof(HTMLAnchorElementClass))]
	[Guid("3050F502-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLAnchorElement : DispHTMLAnchorElement, HTMLAnchorEvents_Event
	{
	}
}
