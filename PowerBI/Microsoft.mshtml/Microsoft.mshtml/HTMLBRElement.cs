using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004C6 RID: 1222
	[Guid("3050F53A-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLBRElementClass))]
	[ComImport]
	public interface HTMLBRElement : DispHTMLBRElement, HTMLElementEvents_Event
	{
	}
}
