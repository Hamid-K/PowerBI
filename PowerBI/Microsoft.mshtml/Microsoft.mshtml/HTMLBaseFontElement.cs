using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000790 RID: 1936
	[Guid("3050F504-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLBaseFontElementClass))]
	[ComImport]
	public interface HTMLBaseFontElement : DispHTMLBaseFontElement, HTMLElementEvents_Event
	{
	}
}
