using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004B1 RID: 1201
	[Guid("3050F523-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLLIElementClass))]
	[ComImport]
	public interface HTMLLIElement : DispHTMLLIElement, HTMLElementEvents_Event
	{
	}
}
