using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001D8 RID: 472
	[Guid("3050F524-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLLinkElementClass))]
	[ComImport]
	public interface HTMLLinkElement : DispHTMLLinkElement, HTMLLinkElementEvents_Event
	{
	}
}
