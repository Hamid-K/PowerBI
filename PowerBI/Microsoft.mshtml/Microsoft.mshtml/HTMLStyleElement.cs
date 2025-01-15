using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C7D RID: 3197
	[CoClass(typeof(HTMLStyleElementClass))]
	[Guid("3050F511-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLStyleElement : DispHTMLStyleElement, HTMLStyleElementEvents_Event
	{
	}
}
