using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000394 RID: 916
	[Guid("3050F512-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLFontElementClass))]
	[ComImport]
	public interface HTMLFontElement : DispHTMLFontElement, HTMLElementEvents_Event
	{
	}
}
