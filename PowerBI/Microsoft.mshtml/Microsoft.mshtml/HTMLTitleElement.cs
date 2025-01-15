using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000780 RID: 1920
	[Guid("3050F516-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLTitleElementClass))]
	[ComImport]
	public interface HTMLTitleElement : DispHTMLTitleElement, HTMLElementEvents_Event
	{
	}
}
